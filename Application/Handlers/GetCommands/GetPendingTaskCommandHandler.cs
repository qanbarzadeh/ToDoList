using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR;

namespace Application.Handlers.GetTasks
{
    public class GetPendingTaskCommandHandler : IRequestHandler<GetPendingTaskCommand, List<TodoTask>>
    {
        private readonly ITodoTasksService _todoTasksService; // ITodoTasksService
        public GetPendingTaskCommandHandler(ITodoTasksService todoTasksService)
        {
            _todoTasksService = todoTasksService;
        }
        
        public Task<List<TodoTask>> Handle(GetPendingTaskCommand request, CancellationToken cancellationToken)
        {
            return _todoTasksService.GetPendingsTasks();
        }
    }
}
