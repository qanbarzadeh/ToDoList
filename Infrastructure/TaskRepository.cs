using Application;
using Domain.Todo;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TaskRepository : ITaskRepository
    //TaskRepository 
    {
        int lastId = 0;

        public List<ToDoTask> tasks = new List<ToDoTask>();

        public TaskRepository()
        {

        }
        public Task<List<ToDoTask>> GetPendingTaskList()
        {
            var pendingTasks = tasks.Where(
                 t => !t.Completed
                 ).OrderBy(t => t.Id).ToList();

            return Task.FromResult(pendingTasks);
        }

        public Task AddTask(ToDoTask item)
        {
            item.Id = ++lastId;
            tasks.Add(item);
            return Task.CompletedTask;
        }

        public Task<List<ToDoTask>> GetTasks()
        {
            return Task.FromResult(tasks);
        }

        //List OverDue taksk 
        public Task<List<ToDoTask>> GetOverDueTasks()
        {
            var overDueTasks = tasks.Where(
                t => !t.Completed && t.DueDate < DateTime.Now
            ).ToList();

            return Task.FromResult(overDueTasks);
        }

        public void AddTasks(List<ToDoTask> Tasks)
        {
            tasks.AddRange(Tasks);
        }

        public Task UpdateTask(ToDoTask task)
        {
            task.Title = task.Title;
            task.DueDate = task.DueDate;
            task.Completed = task.Completed;

            return Task.CompletedTask;
        }

        public ToDoTask GetTaskByID(int id)
        {
            var task = tasks.Where(t => t.Id == id).FirstOrDefault();
            if (task != null) { return task; }
            return new ToDoTask();
        }
    }
}