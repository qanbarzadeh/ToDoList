using Application;
using Domain.Todo;
using Infrastructure.Repositories.ToDoList;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System.Net;
using System.Text;
using System.Text.Json;
using ToDoList;

namespace TodoTasksTest
{
    public class CreateTodoTaskTest : IDisposable
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;
        private readonly IServiceScope scope;
        private readonly TodoDbContext dbContext;

        private readonly DatabaseFacade database;

        public CreateTodoTaskTest()
        {
            factory = new WebApplicationFactory<Program>();
            client = factory.CreateClient();
            options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            scope = factory.Services.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
            database = dbContext.Database;

            database.EnsureCreated();
        }

        [Fact]
        public async Task CreatesTodoTask()
        {
            var now = DateTime.UtcNow;
            var createTask = new BasicTask()
            {
                Title = "Test",
                DueDate = now,
            };
            var createBody = new StringContent(
                JsonSerializer.Serialize(createTask),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("/todoTasks", createBody);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            var todoTask = JsonSerializer.Deserialize<TodoTask>(responseContent, options);
            Assert.NotNull(todoTask);
            Assert.True(todoTask?.Id > 0);
            Assert.Equal(createTask.Title, todoTask.Title);
            Assert.Equal(createTask.DueDate, todoTask.DueDate);

        }

        [Fact]
        public async Task GetsOverdueTasks()
        {
            var now = DateTime.Now;
            var tasks = new List<TodoTask>()
            {
                new TodoTask()
                {
                    Title = "Test",
                    DueDate= now.AddDays(-1)
                },
                new TodoTask()
                {
                    Title = "Test not overdue",
                    DueDate = now.AddDays(1)
                }
            };
            dbContext.TodoTasks.AddRange(tasks);
            await dbContext.SaveChangesAsync();

            var response = await client.GetAsync("/todoTasks/overdue");
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            var overdueTasks = JsonSerializer.Deserialize<List<TodoTask>>(responseContent, options);
            Assert.Collection(overdueTasks,
                t =>
                {
                    Assert.Equal(tasks[0].Id, t.Id);
                    Assert.Equal(tasks[0].Title, t.Title);
                });
        }

        [Fact]
        public async Task UpdatesTodoTask()
        {
            var now = DateTime.Now;
            var task =
                new TodoTask()
                {
                    Title = "Test not overdue",
                    DueDate = now.AddDays(1)
                };
            
            dbContext.TodoTasks.Add(task);
            await dbContext.SaveChangesAsync();

            //Act
            var updatingTask = new TodoTask()
            {
                Id = task.Id,
                Completed = true,
                Title = "updated title"
            };
            var requestContent = new StringContent(JsonSerializer.Serialize(updatingTask), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/todoTasks", requestContent);
            var updateResponseContent = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode);
            var updatedTask = JsonSerializer.Deserialize<TodoTask>(updateResponseContent, options);
            Assert.NotNull(updatedTask);
            Assert.Equal(task.Id, updatingTask.Id);
            Assert.Equal(updatingTask.Title, updatedTask.Title);
            Assert.Equal(updatingTask.DueDate, updatedTask.DueDate);

        }
        public void Dispose()
        {
            database.EnsureDeleted();
        }
    }
}
