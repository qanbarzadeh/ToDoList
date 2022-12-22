using Domain.Todo;
using System.Linq.Expressions;

namespace Application
{
    public interface ITodoTaskRepository
    {
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingTasks();
        Task<List<ToDoTask>> GetAllTasks();
        Task AddTask(ToDoTask item); 
       
    }
}