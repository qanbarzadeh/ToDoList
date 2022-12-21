using Domain.Todo;
using System.Linq.Expressions;

namespace Application.Tests
{
    internal class FakeTodoTaskDatabase : ITodoTaskRepository
    {
        public List<ToDoTask> tasks = new List<ToDoTask>();

        public FakeTodoTaskDatabase()
        {

        }
     
        public Task AddTask(ToDoTask item)
        {
            tasks.Add(item);
            return Task.CompletedTask;
        }

        public Task<List<ToDoTask>> GetTasks()
        {
            return Task.FromResult(tasks);
        }
            

        internal void AddTasks(List<ToDoTask> Tasks)
        {
            tasks.AddRange(Tasks);

        }    

        public Task<List<ToDoTask>> GetOverDueTasks()
        {
            return Task.FromResult(tasks.Where(t => !t.Completed && t.DueDate > DateTime.Now).ToList());
        }

        public Task<List<ToDoTask>> GetPendingTaskList()
        {
            throw new NotImplementedException();
        }
    }
}