
using Domain.Todo;

namespace Application.Tests
{
    public class TodoTasksService : ITodoTasksService
    {
        int lastId = 1;
        ITodoTaskRepository _taskDatabase;
        public TodoTasksService(ITodoTaskRepository taskDatabase)
        {
            _taskDatabase = taskDatabase;
        }
        public async Task<List<ToDoTask>> GetPendingsTasks()
        {
            var pendingTaskList = await _taskDatabase.GetPendingTaskList();

            return pendingTaskList;
        }
        public async Task<ToDoTask> CreateTask(CreatTask task)
        {
            ToDoTask toDo = new ToDoTask()
            {
                Id = lastId++,
                Title = "Test",
                DueDate = DateTime.Now.AddDays(1)
            };
            toDo.Title = task.Title;
            toDo.DueDate = task.DueDate;

            await _taskDatabase.AddTask(toDo);

            return toDo;
        }
        public async Task<List<ToDoTask>> GetOverDueTasks()
        {
            var overDueTasks = await _taskDatabase.GetOverDueTasks();
            return overDueTasks;
        }
    }
}