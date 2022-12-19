
using Domain.Todo;

namespace Application.Tests

{
    internal class ToDoListService
    {
        TaskDatabase _taskDatabase;  
        public ToDoListService(TaskDatabase taskDatabase)
        {
            _taskDatabase = taskDatabase;

        }
        public async  Task<List<ToDo>> GetPendingsTasks()
        {                     
            var pendingTaskList = await _taskDatabase.GetPendingTaskList();

            return pendingTaskList; 
        }

        internal async Task<List<ToDo>> GetOverDueTasks()
        {
            var overDueTasks = await _taskDatabase.GetOverDueTasks();
            return overDueTasks; 
        }
    }
}