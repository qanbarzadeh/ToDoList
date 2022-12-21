﻿using Application;
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
        public async Task AddTask(ToDoTask item)
        {
           dbContext.Add(item);
           await dbContext.SaveChangesAsync();
        }

        public IQueryable<ToDoTask> GetAllTasks()
        {
            return dbContext.ToDoTasks;

        }
        public async Task<List<ToDoTask>> GetOverDueTasks()
        {
            return await dbContext.ToDoTasks.Where(t => !t.Completed && t.DueDate < DateTime.Now).ToListAsync();
        }

        public Task<List<ToDoTask>> GetPendingTaskList()
        {
            throw new NotImplementedException();
        }


    }
}
