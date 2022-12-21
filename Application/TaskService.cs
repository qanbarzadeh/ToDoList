
using Domain.Todo;

namespace Application

{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {

            this.taskRepository = taskRepository; //it shouldnt be referenced here , not clean 
        }

        public async Task<List<ToDoTask>> GetPendingsTasks()
        {
            var pendingTaskList = await taskRepository.GetPendingTaskList();

            return pendingTaskList;
        }

        public async Task CreateTask(ToDoTask toDo)
        {
            await taskRepository.AddTask(toDo);
        }

        public async Task<List<ToDoTask>> GetOverDueTasks()
        {
            var overDueTasks = await taskRepository.GetOverDueTasks();
            return overDueTasks;
        }

        public async Task EditTask(ToDoTask toDo)
        {
            await taskRepository.UpdateTask(toDo);
        }

        public Task<List<ToDoTask>> GetTasks()
        {
            var tasks =  taskRepository.GetTasks();
            return tasks;            
        }

        
    }
}