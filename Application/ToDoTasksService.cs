
using Domain.Todo;

namespace Application.Tests
{
    public class TodoTasksService : ITodoTasksService
    {
        ITodoTaskRepository todoTaskRepository;
        public TodoTasksService(
            ITodoTaskRepository todoTaskRepository)
        {
            this.todoTaskRepository = todoTaskRepository;
        }

        public async Task<List<TodoTask>> GetPendingsTasks()
        {
            var pendingTaskList = await todoTaskRepository.GetPendingTasks();

            return pendingTaskList;
        }

        public async Task<TodoTask> CreateTask(CreatTask task, CancellationToken cancellationToken)
        {
            TodoTask toDo = new TodoTask()
            {
                Title = task.Title,
                DueDate = task.DueDate,
                Completed = false
            };

            await todoTaskRepository.AddTask(toDo);

            return toDo;
        }

        public async Task<List<TodoTask>> GetOverDueTasks()
        {
            return await todoTaskRepository.GetOverDueTasks();
        }
     
        public  async Task<TodoTask> GetTaskById(TodoTask task)
        {
            try
            {
                if (task is not null)
                {
                    return await todoTaskRepository.GetTaskById(task);
                }
                else
                {
                    return new TodoTask();
                }
            }catch(Exception ex)
            {
                throw; 
            }
        }

        public  Task UpdateTask(TodoTask task)
        {            
           todoTaskRepository.UpdateTask(task);
           return Task.CompletedTask; 
        }
    }
}
