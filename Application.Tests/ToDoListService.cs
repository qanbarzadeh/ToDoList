
using Domain.Todo;

namespace Application.Tests

{
    internal class ToDoListService
    {
        int lastId = 1;
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

        public void CreateTask(CreatTask task)
        {
            ToDo toDo = new ToDo()
            {
                Id = lastId++,
                Title = "Test",
                DueDate = DateTime.Now.AddDays(1)
            };
            toDo.Title = task.Title;
            toDo.DueDate = task.DueDate; 

            _taskDatabase.AddTask(toDo);
        }

        internal async Task<List<ToDo>> GetOverDueTasks()
        {
            var overDueTasks = await _taskDatabase.GetOverDueTasks();
            return overDueTasks; 
        }

        
    }

    
}