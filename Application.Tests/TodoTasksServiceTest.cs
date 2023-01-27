using Domain.Todo;
using Moq;
using Application.Handlers.CreateCommands;
using MediatR;
using Microsoft.VisualBasic;
using Application.Handlers.GetTasks;

namespace Application.Tests
{
    public class TodoTasksServiceTest
    {
        Mock<ITodoTaskRepository> mockedTodoRepository = new Mock<ITodoTaskRepository>();
        Mock<ITodoTasksService> mockedTodoService = new Mock<ITodoTasksService>();
        //Mock<IMediator> mockedMediatR = new Mock<IMediator>(); 


        [Fact]
        public async Task CreateTaskCommand_ShouldCreateNewTask()
        {

            //Arrange
            var title = "Test meidiatR task";
            var dueDate = DateTime.Now.AddDays(1);
            var request = new CreateTaskCommand(title, dueDate); 
            var basicTask = new BasicTask
            {
                Title = title,
                DueDate = dueDate
            };

            var todoTask = new TodoTask
            {
                Id = 1,
                Title = basicTask.Title,
                DueDate = basicTask.DueDate,
                Completed = false
            };

            
            mockedTodoService.Setup(x => x.CreateTask(It.IsAny<BasicTask>(), It.IsAny<CancellationToken>())).ReturnsAsync(todoTask);
            var handler = new CreateTaskCommandHandler(mockedTodoService.Object);
            var CancellationToken =  new CancellationToken();

            //Act
            var result = await handler.Handle(request,CancellationToken); 
           
            // Assert
            mockedTodoService.Verify(x => x.CreateTask(It.IsAny<BasicTask>(), CancellationToken.None));
            Assert.NotNull(result);
            Assert.Equal(basicTask.Title, result.Title);
            Assert.Equal(basicTask.DueDate, result.DueDate);
        }

        [Fact]
        public async Task GetPendingTasksCommand_ShouldGetPendingTask()
        {
            //Arrange
            var title = "Test meidiatR task";
            var dueDate = DateTime.Now.AddDays(1);
            var request = new CreateTaskCommand(title, dueDate);

            var taskList = new List<TodoTask>()
            {

               new TodoTask
                {
                    Id = 1,
                    Title = title,
                    DueDate = dueDate,
                    Completed = false
                }
            };
            var command = new GetPendingTaskCommand(); 
            mockedTodoRepository.Setup(x => x.GetPendingTasks()).ReturnsAsync(taskList);             
            var service =  new  TodoTasksService(mockedTodoRepository.Object);
            var handler =  new  GetPendingTaskCommandHandler(service);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert 
            Assert.NotNull(result);
            Assert.IsType<List<TodoTask>>(result);
            Assert.Equal(taskList, result);
            mockedTodoRepository.Verify(x => x.GetPendingTasks(), Times.Once);             
        }
      

        [Fact]
        public async Task Creates_a_Task()
        {
            //arrange              
            var basicTask = new BasicTask()
            {
                Title = "Hit the Gym",
                DueDate = DateTime.Now.AddDays(1)
            };

            TodoTask todoTask = new TodoTask()
            {
                Id = 1,
                Title = basicTask.Title,
                Completed = false,
                DueDate = basicTask.DueDate
            };
            mockedTodoRepository.Setup(m => m.AddTask(todoTask)).Returns(Task.CompletedTask);

            //action 

            var todoListService = new TodoTasksService(mockedTodoRepository.Object);
            var cancellationToken = new CancellationToken();
            var createdTask = await todoListService.CreateTask(basicTask, cancellationToken);

            //assert                       
            Assert.False(createdTask.Completed);
            Assert.Equal(todoTask.Title, createdTask.Title);
            Assert.Equal(todoTask.DueDate, createdTask.DueDate);
        }
        [Fact]
        public async Task GetsPendingTasks()
        {
            //arrange            
            var mockedPendingTasks = new List<TodoTask>()
            {
                new TodoTask(){ Id = 2, Title = "T2",Completed = false},
                new TodoTask(){ Id = 4, Title = "T4",Completed = false}
            };

            //var mockedRepository = new Mock<ITodoTaskRepository>();
            mockedTodoRepository.Setup(c => c.GetPendingTasks()).Returns(Task.FromResult(mockedPendingTasks));

            //action
            var toDoListService = new TodoTasksService(mockedTodoRepository.Object);
            var pendingTasks = await toDoListService.GetPendingsTasks();

            //assert
            Assert.NotNull(pendingTasks);
            Assert.All(pendingTasks, t => Assert.False(t.Completed));
            Assert.Collection(pendingTasks,
                t => Assert.Equal(pendingTasks[0].Id, t.Id),
                t => Assert.Equal(pendingTasks[1].Id, t.Id));
        }
        [Fact]
        public async Task GetsOverdueTasks()
        {

            var now = DateTime.Now;

            //arrange
            var overdueTasks = new List<TodoTask>()
            {
                new TodoTask(){ Id = 2, Title = "T2",Completed = false, DueDate = now.AddDays(-1) },
                new TodoTask(){ Id = 4, Title = "T4",Completed = false , DueDate = now.AddSeconds(-1)}
            };
            mockedTodoRepository.Setup(m => m.GetOverDueTasks()).Returns(Task.FromResult(overdueTasks));

            var toDoListService = new TodoTasksService(mockedTodoRepository.Object);

            //act
            var overDueTasks = await toDoListService.GetOverDueTasks();

            //Assert
            Assert.NotEmpty(overDueTasks);
            Assert.All(overDueTasks, t =>
            {
                //Assert.True(now > t.DueDate);
                Assert.False(t.Completed);
            });
        }
        [Fact]
        public async Task Updates_a_Todo_Task()
        {
            //Arrange            
            var toDoTask = new TodoTask()
            {
                Id = 1,
                Title = "Test",
                DueDate = DateTime.Now.AddDays(1)
            };
            var updatedTodoTask = new TodoTask()
            {
                Id = 1,
                Title = "Updated",
                DueDate = DateTime.Now.AddDays(2),
                Completed = false
            };
            mockedTodoRepository.Setup(m => m.UpdateTask(It.IsAny<TodoTask>())).Returns(Task.FromResult(updatedTodoTask));
            CancellationToken token = new();

            //Action
            var todoTaskService = new TodoTasksService(mockedTodoRepository.Object);
            var updated = await todoTaskService.UpdateTask(updatedTodoTask);

            //Assert
            Assert.True(updated.Id == 1);
            Assert.True(updated.Completed == false);
            Assert.Equal(updated.Title, updatedTodoTask.Title);
            Assert.Equal(updated.DueDate, updatedTodoTask.DueDate);
        }

        /// <summary>
        /// throws exception if duedate is less that today date 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public Task Duedate_should_not_less_than_Today_date()
        {
            //arrange
            var basicTask = new BasicTask()
            {
                Title = "Test",
                DueDate = DateTime.Now.AddDays(-1)
            };
            //Action
            var todoTaskService = new TodoTasksService(mockedTodoRepository.Object);
            var cancellationToken = new CancellationToken();
            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => todoTaskService.CreateTask(basicTask, cancellationToken));
            return Task.CompletedTask;
        }

    }


}
