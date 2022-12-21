using Domain.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.ComponentModel.DataAnnotations;
using Castle.DynamicProxy.Generators;

namespace Application.Tests
{
    public class ToDoListServiceTest
    {
        TaskDatabase taskDatabase = new TaskDatabase();

        [Fact]
        public async Task Creates_a_Task()
        {
            //arrange              
            var createTask = new CreatTask()
            {
                Title = "Hit the Gym",
                DueDate = DateTime.Now.AddDays(1)
            };


            //action 
            var todoListService = new ToDoTasksService(taskDatabase);
            todoListService.CreateTask(createTask);


            //assert 
            var tasks = await taskDatabase.GetTasks();
            Assert.NotEmpty(tasks);
            Assert.Collection(tasks, t =>
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
            var toDoList = new List<ToDoTask>()
            {
                new ToDoTask(){ Id = 1, Title = "T1",Completed = true},
                new ToDoTask(){ Id = 2, Title = "T2",Completed = false},
                new ToDoTask(){ Id = 3, Title = "T3",Completed = true},
                new ToDoTask(){ Id = 4, Title = "T4",Completed = false}
            };

            taskDatabase.AddTasks(toDoList);
            //action
            var toDoListService = new ToDoTasksService(taskDatabase);
            var pendingTasks = await toDoListService.GetPendingsTasks();

            //assert
            Assert.NotNull(pendingTasks);
            Assert.All(pendingTasks, t => Assert.False(t.Completed));
            Assert.Collection(pendingTasks,
                t => Assert.Equal(toDoList[1].Id, t.Id),
                t => Assert.Equal(toDoList[3].Id, t.Id));
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
            taskDatabase.AddTasks(toDoList);


            //action
            var toDoListService = new ToDoTasksService(taskDatabase);
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

        TaskDatabase taskDatabase = new TaskDatabase();

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
            var taskDatabase = new TaskDatabase();
            var toDo = new ToDoTask()
            {
                Id = 1,
                Title = "T1",
                Completed = false
            };
            taskDatabase.AddTask(toDo);
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
