using Domain.Todo;
using MediatR; // MediatR
namespace Application.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TodoTask>
    {
        private readonly ITodoTasksService _todoTaskService;

        public CreateTaskCommandHandler(ITodoTasksService todoTasksService)
        {
            _todoTaskService = todoTasksService;
        }
        public async Task<TodoTask> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = new BasicTask
            {
                Title = "New Task by MediatR!",
                DueDate = DateTime.Now.AddDays(1)
            };

            return await _todoTaskService.CreateTask(newTask,cancellationToken);  
        }
    }
}

