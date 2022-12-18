using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ToDo
{
    internal class ToDoRepository : IToDoListRepository
    {
        public IQueryable<Domain.Todo.ToDo> ToDo => throw new NotImplementedException();

        //it should be add . delete , update 

        public Task CreateTask(string Title, DateTime duedate) //add 
        {
            throw new NotImplementedException();
        }

        public Task EditTask(string Title)  
        {
            throw new NotImplementedException();
        }

        public Task<List<Domain.Todo.ToDo>> GetOverDueTasks()
        {
            throw new NotImplementedException();
        }

        public Task<List<Domain.Todo.ToDo>> GetPendingTasks()
        {
            throw new NotImplementedException();
        }
    }
}
