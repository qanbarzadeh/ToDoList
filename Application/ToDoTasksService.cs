
using Domain.Todo;
using Infrastructure;

namespace Application.Tests

{
    internal class ToDoTasksService
    {
        int lastId = 1;
        ITodoTaskDatabase _taskDatabase;  
        public ToDoTasksService(ITodoTaskDatabase taskDatabase)
        {
            _taskDatabase = taskDatabase;

        }
        public async  Task<List<ToDoTask>> GetPendingsTasks()
        {                     
            var pendingTaskList = await _taskDatabase.GetPendingTaskList();

            return pendingTaskList; 
        }
        public void CreateTask(CreatTask task)
        {
            ToDoTask toDo = new ToDoTask()
            {
                Id = lastId++,
                Title = "Test",
                DueDate = DateTime.Now.AddDays(1)
            };
            toDo.Title = task.Title;
            toDo.DueDate = task.DueDate; 

            _taskDatabase.AddTask(toDo);
        }
        public async Task<List<ToDoTask>> GetOverDueTasks()
        {
            var overDueTasks = await _taskDatabase.GetOverDueTasks();
            return overDueTasks; 
        }        
    }

    
}