using Application;
using Domain.Todo;
using Infrastructure.Repositories.ToDoList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly TodoDbContext dbContext;

        public TodoTaskRepository(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    
        public async  Task AddTask(TodoTask item)
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
            return await dbContext.TodoTasks.Where(t => !t.Completed && (!t.DueDate.HasValue || t.DueDate > DateTime.Now)).ToListAsync();
        }

        async Task<List<TodoTask>> ITodoTaskRepository.GetAllTasks()
        {
            return await dbContext.TodoTasks.ToListAsync(); 
        }
    }
}
