using Domain.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Application.Tests
{
    public class ToDoListServiceTest
    {

        //given two taks one is pending, one is not pending when  GetPendingTasks() is called 
        //it should return the Pending task; 
        //[Fact]
        //public async Task Returns_NonPassedTasks()
        //{
        //    //arrange
        //    var taskDatabase = new TaskDatabase();
        //    taskDatabase.AddPendingTask();
        //    taskDatabase.AddCompletedTask(); 

        //    //action
        //    var toDoListService = new ToDoListService();
        //    var pendingTasks = await toDoListService.GetPendingsTasks();

        //    //assert
        //    Assert.NotNull(pendingTasks);
        //    Assert.Collection(pendingTasks, t => Assert.False(t.IsDone));  
        //}               
    }

    public  class TaskDatabaseTest
    {
        [Fact]
        public  async void DatabaseIsInitialyEmpty()
        {
            
            var taskDatabase = new TaskDatabase();
            var tasks = await  taskDatabase.GetTasks(); 
            
            Assert.Empty(tasks); 
        }

        //Give an empty database when a pending task is added
        //GetTask should return the task
        [Fact]
        public async Task DatabaseIsNotEmpty()
        {
            //arrange
            var taskDatabase = new TaskDatabase();
            taskDatabase.AddPendingTask();

            //action
            var tasks = await taskDatabase.GetTasks();

            //Assert
            Assert.NotEmpty(tasks); 

        }
    }
}
