using Domain.Todo;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.ToDoList
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        public DbSet<ToDoTask> ToDoTasks  => Set<ToDoTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoTask>()
                .HasKey(t => t.Id);           
        }
    }
}
