using Domain.Todo;

namespace Application
{
    public interface ITodoTasksService
    {
        Task<ToDoTask> CreateTask(CreatTask task, CancellationToken cancellationToken);
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingsTasks();
    }
}