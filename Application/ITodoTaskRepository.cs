using Domain.Todo;
using System.Linq.Expressions;

namespace Application
{
    public interface ITodoTaskRepository
    {
        Task AddTask(ToDoTask item);
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingTasks();       
    }
}