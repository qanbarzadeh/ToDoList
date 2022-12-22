using Domain.Todo;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;

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
            var toDoTask = new ToDoTask()
            {
                Title = "Play all the games",
                DueDate = DateTime.Now.AddDays(3)
            };
                  
            //action 
            mockedTodoRepository.Setup(m => m.AddTask(createTask)).Returns(Task.CompletedTask);
            var todoListService = new TodoTasksService(mockedTodoRepository.Object);
            var cancellationToken = new CancellationToken();
            await todoListService.CreateTask(createTask, cancellationToken);
            var taskList =  await mockedTodoRepository.Object.GetAllTasks(); 

            //assert 

            Assert.NotEmpty(taskList);
            Assert.Collection(taskList, t =>
            {
                Assert.NotNull(t.Title);
                Assert.Equal(createTask.Title, t.Title);
                Assert.NotNull(t.DueDate);
            });
        }

        [Fact]
        public async Task GetsPendingTasks()
        {

            //arrange            
            var mockedPendingTasks = new List<ToDoTask>()
            {
                new ToDoTask(){ Id = 2, Title = "T2",Completed = false},
                new ToDoTask(){ Id = 4, Title = "T4",Completed = false}
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
            var toDoList = new List<ToDoTask>()
            {
                new ToDoTask(){ Id = 1, Title = "T1",Completed = true},
                new ToDoTask(){ Id = 2, Title = "T2",Completed = false, DueDate = now.AddDays(-1) },
                new ToDoTask(){ Id = 3, Title = "T3",Completed = true , DueDate = now.AddDays(-1) },
                new ToDoTask(){ Id = 4, Title = "T4",Completed = false , DueDate = now.AddSeconds(-1)},
                new ToDoTask(){ Id = 5, Title = "T5",Completed = false , DueDate = now.AddDays(+2)}
            };
            //taskDatabase.AddTasks(toDoList);

            //action
            var toDoListService = new TodoTasksService(mockedTodoRepository.Object);
            var overDueTasks = await toDoListService.GetOverDueTasks();

            //Assert
            Assert.NotEmpty(overDueTasks);
            Assert.All(overDueTasks, t =>
            {
                Assert.True(now > t.DueDate);
                Assert.False(t.Completed);
            });
        }
        public static ToDoTask CreateCompletedTask()
        {
            var Todo = new ToDoTask()
            {
                Completed = true
            };
            return Todo;
        }
    }

    public class TaskDatabaseTest
    {

        FakeTodoTaskDatabase taskDatabase = new ();

        [Fact]
        public async void DatabaseIsInitialyEmpty()
        {


            var tasks = await taskDatabase.GetTasks();

            Assert.Empty(tasks);
        }

        //Give an empty database when a pending task is added
        //GetTask should return the task
        [Fact]
        public async Task ShouldAddaPendingTask()
        {
            //arrange
            var taskDatabase = new FakeTodoTaskDatabase();
            var toDo = new ToDoTask()
            {
                Id = 1,
                Title = "T1",
                Completed = false
            };
            await taskDatabase.AddTask(toDo);
            //action
            var tasks = await taskDatabase.GetTasks();

            //Assert
            Assert.NotEmpty(tasks);
            Assert.Collection(tasks, t =>
            {
                Assert.False(t.Completed);
                Assert.Equal(toDo.Id, t.Id);
                Assert.Equal(toDo.Title, t.Title);
            });
        }
    }
}
