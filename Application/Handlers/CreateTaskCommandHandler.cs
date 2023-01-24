using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR; // MediatR
namespace Application.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, BasicTask>
    {
        private readonly ITodoTasksService _todoTaskService; 

        public CreateTaskCommandHandler(ITodoTasksService todoTasksService)
        {
            _todoTaskService = todoTasksService; 
        }
        public Task<TodoTask> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            _todoTaskService.CreateTask(request, cancellationToken); 

            
        }
    }
}
