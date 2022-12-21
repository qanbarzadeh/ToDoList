using Domain.Todo;

namespace Infrastructure
{
    public interface ITodoTaskDatabase
    {
        void AddTask(ToDoTask item);
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingTaskList();
        Task<List<ToDoTask>> GetTasks();
    }
}