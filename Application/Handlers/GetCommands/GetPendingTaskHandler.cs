using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR;

namespace Application.Handlers.GetTasks
{
    public class GetPendingTaskHandler : IRequestHandler<GetPendingTaskQuery, List<TodoTask>>
    {
        private readonly ITodoTasksService _todoTasksService; // ITodoTasksService
        public GetPendingTaskHandler(ITodoTasksService todoTasksService)
        {
            _todoTasksService = todoTasksService;
        }
        
        public Task<List<TodoTask>> Handle(GetPendingTaskQuery request, CancellationToken cancellationToken)
        {
            return _todoTasksService.GetPendingsTasks();
        }
    }
}
