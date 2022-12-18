
using Domain.Todo;

namespace Application.Tests

{
    internal class ToDoListService
    {

        public ToDoListService()
        {
        }

        public async  Task<List<ToDo>> GetPendingsTasks()
        {
            var taskDb = new TaskDatabase();

            var pendingTaskList = await  taskDb.GetPendingTaskList();

            return pendingTaskList; 
        }
    }
}