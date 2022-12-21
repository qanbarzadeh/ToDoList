using Domain.Todo;

namespace Application
{
    public interface IToDoListRepository 
    {
        public IQueryable<ToDoTask> ToDo { get; }
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingTasks();
        Task CreateTask(string Title, DateTime duedate);
        Task EditTask(string Title); 
       
    }
    
}
