
using Domain.Todo;
using System.Linq;

namespace Application.Tests
{
    public class TodoTasksService : ITodoTasksService
    {
        int lastId = 1;
        ITodoTaskRepository todoTaskRepository;
        public TodoTasksService(
            ITodoTaskRepository todoTaskRepository)
        {
            this.todoTaskRepository = todoTaskRepository;
        }
        public async Task<List<ToDoTask>> GetPendingsTasks()
        {
            var pendingTaskList = await todoTaskRepository.GetPendingTasks();

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

            await todoTaskRepository.AddTask(toDo);

            return toDo;
        }
        public async Task<List<ToDoTask>> GetOverDueTasks()
        {
            var overDueTasks = await todoTaskRepository.GetOverDueTasks();           
            return overDueTasks;
        }
    }
}