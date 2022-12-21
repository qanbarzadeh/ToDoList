using Domain;
using Domain.Todo;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.ToDoList;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Application;
using Infrastructure;

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

            //var summaries = new[]
            //{
            //"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            //};


            //Create a Todo Task
            app.MapPost("/todolist/createTask", async (ITodoTasksService todoTaskService,CreatTask creatTask, CancellationToken cancellationToken) =>
            {
               var task = await todoTaskService.CreateTask(creatTask, cancellationToken);

                return Results.Created($"/todolist/{task.Id}", task);
            });

            ////Get list of Todo Tasks
            //app.MapGet("/todolist/listtasks", async (ToDoDb db) => );

            ////Get list of complemeted Todo tasks
            //app.MapGet("/todolist/completed", async (ToDoDb db) => );

            ////Get list of pending tasks
            //app.MapGet("/todolist/pending", async (ToDoDb db) =>)); 

            ////Get list of overdue tasks
            //app.MapGet("/todolist/overdue", async (ToDoDb db) =>);

            //Edit a Task
            //app.MapPut("/todolist/{id}", async (int id, ToDoDb db, ToDoTask target) =>                
            //{
            //    var todo = await db.ToDoTasks.FindAsync(id);
            //    if (todo == null) return Results.NotFound();

            //    todo.Title = target.Title; 
            //    todo.DueDate = target.DueDate;
            //    todo.IsTaskComplete = target.IsTaskComplete;
            //    await db.SaveChangesAsync();
            //    return Results.NoContent(); 
            //}); 




            //app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            //{
            //    var forecast = Enumerable.Range(1, 5).Select(index =>
            //        new WeatherForecast
            //        {
            //            Date = DateTime.Now.AddDays(index),
            //            TemperatureC = Random.Shared.Next(-20, 55),
            //            Summary = summaries[Random.Shared.Next(summaries.Length)]
            //        })
            //        .ToArray();
            //    return forecast;
            //})
            //.WithName("GetWeatherForecast");

            app.Run();
        }
    }
 }




