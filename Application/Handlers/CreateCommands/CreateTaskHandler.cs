using Domain.Todo;
using MediatR; // MediatR

namespace Application.Handlers.CreateCommands
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TodoTask>
    {
        private readonly ITodoTasksService _todoTaskService;

        public CreateTaskHandler(ITodoTasksService todoTasksService)
        {
            _todoTaskService = todoTasksService;
        }
        public async Task<TodoTask> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = new BasicTask
            {
                Title = request.Title,
                DueDate = request.DueDate

            };

            return await _todoTaskService.CreateTask(newTask, cancellationToken);
        }
    }
}

