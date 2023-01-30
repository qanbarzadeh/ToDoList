using Application;
using Domain.Todo;
using Infrastructure.Repositories.ToDoList;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly TodoDbContext dbContext;

        public TodoTaskRepository(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddTask(TodoTask item)
        {
            dbContext.Add(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<TodoTask>> GetOverDueTasks()
        {
            return await dbContext.TodoTasks.Where(t => !t.Completed && t.DueDate < DateTime.Now).ToListAsync();
        }

        public async Task<List<TodoTask>> GetPendingTasks()
        {            
            return await dbContext.TodoTasks.Where(t => (t.DueDate > DateTime.Now || t.DueDate == null) && !t.Completed).ToListAsync();                        
        }

        public async Task<List<TodoTask>> GetAllTasks()
        {
            return await dbContext.TodoTasks.ToListAsync();
        }

        public async Task<TodoTask> UpdateTask(TodoTask updatingTask)
        {
            var task = await dbContext.TodoTasks.FindAsync(updatingTask.Id);

            if (task is null)
                throw RepositoryErrors.NotFound(updatingTask.Id);

            task.Title = updatingTask.Title;
            task.Completed = updatingTask.Completed;
            task.DueDate = updatingTask.DueDate;

            await dbContext.SaveChangesAsync();
            return task;
        }      
        public async Task<TodoTask> GetTaskById(int id)
        {
            return await dbContext.TodoTasks.FindAsync(id);
        }
    }
}
