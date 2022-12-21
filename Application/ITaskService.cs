using Domain.Todo;

namespace Application
{
    public interface ITaskService
    {
        Task CreateTask(ToDoTask toDo);
        Task EditTask(ToDoTask toDo);
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingsTasks();
        Task<List<ToDoTask>> GetTasks(); 
    }
}