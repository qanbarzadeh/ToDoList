using Domain.Todo;
using System.Linq.Expressions;

namespace Application
{
    public interface ITodoTaskRepository
    {
        Task<List<TodoTask>> GetOverDueTasks();
        Task<List<TodoTask>> GetPendingTasks();
        Task<List<TodoTask>> GetAllTasks();
        Task AddTask(TodoTask item);
        Task<TodoTask> UpdateTask(TodoTask task);
        //Task<TodoTask> GetTaskById(TodoTask task);
        Task<TodoTask> GetTaskById(int id);  
    }
}