using Application.Handlers;
using Domain.Todo;

namespace Application
{
    public interface ITodoTasksService
    {
        Task<TodoTask> CreateTask(BasicTask task, CancellationToken cancellationToken);
        
        Task<List<TodoTask>> GetOverDueTasks();
        Task<List<TodoTask>> GetPendingsTasks();
        //Task<TodoTask> GetTaskById(TodoTask task);
        Task<TodoTask> GetTaskByID(int id);
        Task<TodoTask> UpdateTask(TodoTask task); 

        

    }
}