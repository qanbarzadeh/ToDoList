using Domain.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Application.Tests
{
    public class ToDoListServiceTest
    {
        [Fact]
        public async Task Returns_NonPassedTasks()
        {
            var toDoListService = new ToDoListService();
            var pendingTasks = await toDoListService.GetPendingsTasks();

            Assert.NotNull(pendingTasks);
            Assert.Collection(pendingTasks, t => Assert.False(t.IsDone));  
        }
        
       
    }
}
