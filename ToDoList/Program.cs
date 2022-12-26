using Domain;
using Domain.Todo;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.ToDoList;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Application;
using Infrastructure;
using System.Text.Json;
using System.Net;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddTodoTaskServices();
            builder.Services.AddTodoTaskRepositories();

            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("ToDoListDb"));
            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();
          
            //Create a Todo Task
            app.MapPost("/todoTasks", async (ITodoTasksService todoTaskService,CreatTask creatTask, CancellationToken cancellationToken) =>
            {
               var task = await todoTaskService.CreateTask(creatTask, cancellationToken);
                return Results.Created($"/todolist/{task.Id}", task);
            });

            ////Get list of pending tasks        
            app.MapGet("/pendingTasks", async (ITodoTasksService todoTaskService, CancellationToken token) =>
            {
                
                var pendingTasks = await todoTaskService.GetPendingsTasks();
                if(pendingTasks == null || pendingTasks.Count == 0 )
                {
                    return Results.Content("There were no pending tasks!"); 

                }
                return Results.Json(pendingTasks);
                    
            }); 
            
            //Get list of overdue tasks
            app.MapGet("/todoTasks/overdue", async (ITodoTasksService todoTaskService, CancellationToken cancellationToken) =>
            {
                var overdueTasks = await todoTaskService.GetOverDueTasks();
                if(overdueTasks == null || overdueTasks.Count == 0 )
                {
                    return Results.Content("There were no overdue tasks!"); 
                }
                return Results.Ok(overdueTasks);
            });

            app.MapPut("/todoTasks", async (ITodoTasksService todoTaskService, TodoTask task, CancellationToken token) =>
            {
                var updatedTask = await todoTaskService.UpdateTask(task);
                return Results.Ok(updatedTask);
            });          
            app.Run();
        }
    }
 }




