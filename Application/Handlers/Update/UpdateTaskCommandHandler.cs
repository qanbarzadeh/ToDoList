using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR;

namespace Application.Handlers.Update
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand,TodoTask>
    {
        private readonly ITodoTasksService _todoTaskService;


        public UpdateTaskCommandHandler(ITodoTasksService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }
        public async  Task<TodoTask> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var updatedTask = new TodoTask()
            {
                Id = request.Id,
                Title = request.Title,
                DueDate = request.DueDate,
                Completed = request.IsCompleted
            };
            return await _todoTaskService.UpdateTask(updatedTask, cancellationToken);
        }
    }
}
