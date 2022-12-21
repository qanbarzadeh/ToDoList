
using Domain.Todo;
using System.Linq;

namespace Application.Tests
{
    public class TodoTasksService : ITodoTasksService
    {
        int lastId = 1;
        ITodoTaskRepository _taskDatabase;
        public TodoTasksService(
            ITodoTaskRepository taskDatabase)
        {
            _taskDatabase = taskDatabase;
        }
        public async Task<List<ToDoTask>> GetPendingsTasks()
        {
            var pendingTaskList = await _taskDatabase.GetPendingTasks();

            return pendingTaskList;
        }
        public async Task<ToDoTask> CreateTask(CreatTask task,CancellationToken cancellationToken)
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