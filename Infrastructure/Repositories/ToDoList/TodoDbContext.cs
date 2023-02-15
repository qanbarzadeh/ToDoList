using Domain.Todo;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.ToDoList
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {

            Database.EnsureCreated();
        }
        public DbSet<TodoTask> TodoTasks  => Set<TodoTask>();
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoTask>()
                .HasKey(t => t.Id);           
        }
    }
}
