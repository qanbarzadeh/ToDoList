﻿using Domain.Todo;


namespace Application.Tests
{
    public class TaskDatabase
    {
        public List<ToDoTask> tasks = new List<ToDoTask>(); 
                
        public TaskDatabase()
        {            

        }
        public Task<List<ToDoTask>> GetPendingTaskList()
        {
            var pendingTasks = tasks.Where(t => t.Completed == false)
                .OrderBy(t => t.Id).ToList();
            return Task.FromResult(pendingTasks);
        }

        public void AddTask(ToDoTask item)
        {         
            tasks.Add(item);            
        }
        
        public   Task<List<ToDoTask>> GetTasks()
        {            
            return  Task.FromResult(tasks); 
        }

        //List OverDue taksk 
        public Task<List<ToDoTask>> GetOverDueTasks()
        {
            var overDueTasks = tasks.Where(t => t.Completed == false
            && t.DueDate < DateTime.Now).ToList();
            return Task.FromResult(overDueTasks); 
        }

        internal void AddTasks(List<ToDoTask> Tasks)
        {
            tasks.AddRange(Tasks);
            
        }
    }
}