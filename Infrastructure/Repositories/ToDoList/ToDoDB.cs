using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.ToDoList
{
    public class ToDoDb : DbContext
    {
        public ToDoDb(DbContextOptions<ToDoDb> options) : base(options) { }

        public DbSet<ToDoTask> ToDoTasks  => Set<ToDoTask>();

    }
}
