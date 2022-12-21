using Domain.Todo;

namespace Application
{
    public interface ITaskRepository
    {
        Task AddTask(ToDoTask toDo);
      public void AddTasks(List<ToDoTask> toDoList);
        Task<List<ToDoTask>> GetOverDueTasks();
        Task<List<ToDoTask>> GetPendingTaskList();
        ToDoTask GetTaskByID(int id);
       public Task<List<ToDoTask>> GetTasks();
        Task UpdateTask(ToDoTask toDo);
    }
}