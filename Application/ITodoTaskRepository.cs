using Domain.Todo;

namespace Application
{
    public interface ITodoTaskRepository
    {
        Task AddTask(ToDoTask item);
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingTaskList();
        Task<List<ToDoTask>> GetTasks();
    }
}