using Domain.Todo;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.ToDoList
{
    public class ToDoDb : DbContext
    {
        public ToDoDb(DbContextOptions<ToDoDb> options) : base(options) { }
        public DbSet<ToDoTask> ToDoTasks  => Set<ToDoTask>();
    }
}
