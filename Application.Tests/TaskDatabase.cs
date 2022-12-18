using Domain.Todo;
using System.Runtime.InteropServices;

namespace Application.Tests
{
    internal class TaskDatabase
    {
        List<ToDo> tasks = new List<ToDo>(); 

        public TaskDatabase()
        {
        }


        //public void AddCompletedTask()
        // {
        //     var todoList = new List<ToDo>()
        //      {
        //          new ToDo()
        //          {

        //              IsDone = true
        //          }

        //      };

        // }

        public Task<List<ToDo>> GetPendingTaskList()
        {
            var todoList = new List<ToDo>()
             {
                 new ToDo()
                 {

                     IsDone = false
                 },
                 new ToDo()
                 {
                     IsDone = false
                 }
             };
            return Task.FromResult(todoList);
        }

        internal void AddPendingTask()
        {
            tasks.Add(new ToDo { IsDone = false }); 
        }
        public  Task<List<ToDo>> GetTasks()
        {
            
            return  Task.FromResult(tasks); 
        }
    }
}