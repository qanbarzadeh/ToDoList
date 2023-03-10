using Domain;
using Domain.Todo;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.ToDoList;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Application;
using Infrastructure;
using System.Text.Json;
using System.Net;
using MediatR; // MediatR
using Application.Handlers.GetTasks;
using Application.Registrars;
using Application.Handlers.CreateCommands;
using Application.Handlers.Update;
using Microsoft.Extensions.Configuration;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);                        
            // Add services to the container            

            MediatRRegistrar.Register(builder.Services); 
            builder.Services.AddTodoTaskServices();
            builder.Services.AddTodoTaskRepositories();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("localcs")));

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();                

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors("AllowAll");

            //Create a Todo Task
            app.MapPost("crateToDoTaskEndPoint", async (IMediator mediator, CreateTaskCommand command ,CancellationToken cancellationToken) =>
            {
                var task = await mediator.Send(command, cancellationToken);
                return Results.Created($"/todolist/{task.Id}",task); 
            }); 

                                                           
            ////Get list of pending tasks        
            ///using Mediator 
            app.MapGet("/pendingTasks", async (IMediator mediator, CancellationToken token) =>
            {
                var pendingTasks = await mediator.Send(new GetPendingTaskQuery(), token);
                if (pendingTasks == null || pendingTasks.Count == 0)
                {
                    return Results.Content("There were no pending tasks!");

                }
                return Results.Json(pendingTasks); 
            });

            //app.MapGet("/pendingTasks", async (ITodoTasksService todoTaskService, CancellationToken token) =>
            //{

            //    var pendingTasks = await todoTaskService.GetPendingsTasks();
            //    if(pendingTasks == null || pendingTasks.Count == 0 )
            //    {
            //        return Results.Content("There were no pending tasks!"); 

            //    }
            //    return Results.Json(pendingTasks);

            //}); 

            app.MapGet("getoverdue", async (IMediator mediator, CancellationToken token) =>
            {
                var overdueTasks = await mediator.Send(new GetOverDueTaskQuery(), token);
                if (overdueTasks == null || overdueTasks.Count == 0)
                {
                    return Results.Content("There were no overdue tasks!");

                }
                return Results.Json(overdueTasks);
            });

            ////Get list of overdue tasks
            //app.MapGet("/todoTasks/overdue", async (ITodoTasksService todoTaskService, CancellationToken cancellationToken) =>
            //{
            //    var overdueTasks = await todoTaskService.GetOverDueTasks();
            //    if(overdueTasks == null || overdueTasks.Count == 0 )
            //    {
            //        return Results.Content("There were no overdue tasks!"); 
            //    }
            //    return Results.Ok(overdueTasks);
            //});0


            //update a task
            app.MapPut("/updateTask", async (IMediator mediator, UpdateTaskCommand command, CancellationToken cancellationToken) =>
            {
                var task = await mediator.Send(command, cancellationToken);
                return Results.Ok(task);
            });

            //app.MapPut("/todoTasks", async (ITodoTasksService todoTaskService, TodoTask task, CancellationToken token) =>
            //{
            //    var updatedTask = await todoTaskService.UpdateTask(task) ;
            //    return Results.Ok(updatedTask);
            //});          

            app.Run();
        }
    }
 }




