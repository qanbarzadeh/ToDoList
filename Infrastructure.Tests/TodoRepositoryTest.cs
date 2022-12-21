using Domain.Todo;
using Infrastructure.Repositories.ToDoList;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests
{
    public class TodoRepositoryTest : IDisposable
    {
        private readonly TodoDbContext dbContext;
        private readonly TodoTaskRepository repository;

        public TodoRepositoryTest()
        {
            var builder = new DbContextOptionsBuilder<TodoDbContext>();
            builder.UseInMemoryDatabase("AddsTaskToDatabase");

            dbContext = new TodoDbContext(builder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            repository = new TodoTaskRepository(dbContext);
        }

        [Fact]
        public async Task AddsTaskToDatabase()
        {
            //arrange
            var date = DateTime.Now;

            //action
            var task = new ToDoTask()
            {
                Title = "Test",
                DueDate = date,
                Completed = false,
            };
            await repository.AddTask(task);

            Assert.True(task.Id > 0);
            Assert.False(task.Completed);
            Assert.Equal("Test", task.Title);
            Assert.Equal(date, task.DueDate);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public async Task GetsOverDueTasks()
        {
            //arrange
            var now = DateTime.Now;

            var task = new ToDoTask()
            {
                Title = "Test",
                DueDate = now,
                Completed = false,
            };

            var tasks = new ToDoTask[]
               {
                    new ToDoTask { Title = "Test",Completed = false, DueDate = now.AddMinutes(-5)},
                    new ToDoTask { Title = "Test2",Completed = true, DueDate = now.AddMinutes(-5)},
                    new ToDoTask { Title = "Test3",Completed = false}
               };
            dbContext.ToDoTasks.AddRange(tasks);
            await dbContext.SaveChangesAsync();

            //action
            var overdueTasks = await repository.GetOverDueTasks();

            Assert.Collection(overdueTasks, overdueTask => Assert.Equal(tasks[0].Id, overdueTask.Id));
        }
    }
}
