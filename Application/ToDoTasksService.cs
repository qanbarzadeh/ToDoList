
using Domain.Todo;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<ToDoTask> CreateTask(CreatTask task, CancellationToken cancellationToken)
        {
            ToDoTask toDo = new ToDoTask()
            {
                Id = lastId++,
                Title = task.Title,
                DueDate = task.DueDate,
                Completed = false
            };

            await todoTaskRepository.AddTask(toDo);

            return toDo;
        }

        public async Task<List<ToDoTask>> GetOverDueTasks()
        {
            return await todoTaskRepository.GetOverDueTasks();
        }
    }
}