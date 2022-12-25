
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
        
        public async Task<TodoTask> UpdateTask(TodoTask updatingTask)
        {            
           var task = await todoTaskRepository.GetTaskById(updatingTask.Id);              
            if (task is null)
                throw Errors.NotFound(updatingTask.Id);

           return await todoTaskRepository.UpdateTask(updatingTask);           
        }

        public async Task<TodoTask> GetTaskByID(int id)
        {
            return await todoTaskRepository.GetTaskById(id);
        }
    }
}
