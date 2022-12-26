
using Domain.Todo;
using Microsoft.VisualBasic;

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
            var duedate = task.DueDate.HasValue ? task.DueDate : null;          
            if (duedate.HasValue && duedate.Value.Date < DateTime.Now.Date)
            {
                throw new TodoTaskException("Due date cannot be smaller than today's date");
            }

            TodoTask toDo = new TodoTask()
            {
                Title = task.Title,
                DueDate = duedate,
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
           return await todoTaskRepository.UpdateTask(updatingTask);           
        }

        public async Task<TodoTask> GetTaskByID(int id)
        {
            return await todoTaskRepository.GetTaskById(id);
        }
    }
}
