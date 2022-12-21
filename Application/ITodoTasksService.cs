using Domain.Todo;

namespace Application.Tests
{
    internal interface ITodoTasksService
    {
        Task<ToDoTask> CreateTask(CreatTask task);
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingsTasks();
    }
}