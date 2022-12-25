using Domain.Todo;

namespace Application
{
    public interface ITodoTasksService
    {
        Task<TodoTask> CreateTask(CreatTask task, CancellationToken cancellationToken);
        Task<List<TodoTask>> GetOverDueTasks();
        Task<List<TodoTask>> GetPendingsTasks();
        Task<TodoTask> GetTaskById(TodoTask task);
        Task UpdateTask(TodoTask task); 

        

    }
}