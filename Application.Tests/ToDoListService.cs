
using Domain.Todo;

namespace Application.Tests

{
    internal class ToDoListService
    {
        public ToDoListService()
        {
        }

        public Task<List<ToDo>> GetPendingsTasks()
        {
            var todoList = new List<ToDo>()
             {
                 new ToDo()
                 {
                     IsDone = false

                 }                 
             };
            return Task.FromResult(todoList); 
        }
    }
}