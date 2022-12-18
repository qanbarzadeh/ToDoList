using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo; 



namespace Application
{
    public interface IToDoListRepository //Tentity 
    
    {
        public IQueryable<ToDo> ToDo { get; }
        Task<List<ToDo>> GetOverDueTasks();
        Task<List<ToDo>> GetPendingTasks();
        Task CreateTask(string Title, DateTime duedate);
        Task EditTask(string Title); 
        //todo imeplenmt this methods in Infrastructure layer 
    }
    
}
