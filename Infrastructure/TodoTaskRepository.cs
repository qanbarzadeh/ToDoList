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
using System.Threading.Tasks.Sources;

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

        public async Task<List<TodoTask>> GetAllTasks()
        {
            return await dbContext.TodoTasks.ToListAsync(); 
        }

        public Task UpdateTask(TodoTask task) 
        {
            try
            {
                var todoTask = dbContext.TodoTasks.Find(task.Id);
                if (todoTask != null)
                {
                    todoTask.Title = task.Title;
                    todoTask.DueDate = task.DueDate;
                    todoTask.Completed = task.Completed;
                    dbContext.TodoTasks.Update(todoTask);
                    dbContext.SaveChanges();
                    
                }
            }
            catch (InvalidCastException ex)
            {                
                throw ex; 
            }

            return Task.CompletedTask;
        }   

        public async Task<TodoTask> GetTaskById(TodoTask task)
        {
            try
            {
                if (task is not null)
                {
                    var todoTask = await dbContext.TodoTasks.FindAsync(task.Id);
                    return todoTask; 
                }else
                {
                    return new TodoTask(); 
                }
                
            }catch (InvalidCastException ex)
            {
                throw ex;
            }
            
        }
    }
}
