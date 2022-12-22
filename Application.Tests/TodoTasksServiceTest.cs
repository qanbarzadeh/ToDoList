﻿using Domain.Todo;
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

            ToDoTask todoTask = new ToDoTask()
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
            Assert.True(createdTask.Id > 0);
            Assert.False(createdTask.Completed);
            Assert.Equal(todoTask.Title, createdTask.Title);
            Assert.Equal(todoTask.DueDate, createdTask.DueDate);
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
            var overdueTasks = new List<ToDoTask>()
            {
                new ToDoTask(){ Id = 2, Title = "T2",Completed = false, DueDate = now.AddDays(-1) },
                new ToDoTask(){ Id = 4, Title = "T4",Completed = false , DueDate = now.AddSeconds(-1)}                
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
       
    }

}
