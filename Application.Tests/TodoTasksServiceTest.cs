using Castle.DynamicProxy.Generators;
using Domain.Todo;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net.Http.Headers;

namespace Application.Tests
{
    public class TodoTasksServiceTest
    {
        Mock<ITodoTaskRepository> mockedTodoRepository = new Mock<ITodoTaskRepository>();

        [Fact]
        public async Task Creates_a_Task()
        {
            //arrange              
            var createTask = new CreatTask()
            {
                Title = "Hit the Gym",
                DueDate = DateTime.Now.AddDays(1)
            };

            TodoTask todoTask = new TodoTask()
            {
                Id = 1,
                Title = createTask.Title,
                Completed = false,
                DueDate = createTask.DueDate
            };
            mockedTodoRepository.Setup(m => m.AddTask(todoTask)).Returns(Task.CompletedTask);

            //action 

            var todoListService = new TodoTasksService(mockedTodoRepository.Object);
            var cancellationToken = new CancellationToken();
            var createdTask = await todoListService.CreateTask(createTask, cancellationToken);

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
       
    }

}
