using Domain.Todo;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Application.Tests
{
    public class TaskDatabase
    {
        public List<ToDo> tasks = new List<ToDo>(); 
                
        public TaskDatabase()
        {            

        }
        public Task<List<ToDo>> GetPendingTaskList()
        {

            var pendingTasks = tasks.Where(t => t.Completed == false)
                .OrderBy(t => t.Id).ToList();
            return Task.FromResult(pendingTasks);
        }

        public void AddTask(ToDo item)
        {         
            tasks.Add(item);            
        }
        
        public   Task<List<ToDo>> GetTasks()
        {
            
            return  Task.FromResult(tasks); 
        }

        //List OverDue taksk 
        public Task<List<ToDo>> GetOverDueTasks()
        {
            var overDueTasks = tasks.Where(t => t.Completed == false
            && t.DueDate < DateTime.Now).ToList();
            return Task.FromResult(overDueTasks); 
        }

        internal void AddTasks(List<ToDo> Tasks)
        {
            tasks.AddRange(Tasks);
            
        }
    }
}