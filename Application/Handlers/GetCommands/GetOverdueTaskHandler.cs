using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Handlers.GetTasks;
using Domain.Todo;
using MediatR; // MediatR

namespace Application.Handlers.GetCommands
{
    public class GetOverdueTaskHandler : IRequestHandler<GetOverDueTaskQuery, List<TodoTask>>
    {
        private readonly ITodoTasksService _todoTaskService;
        public GetOverdueTaskHandler(ITodoTasksService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }
    
        public async Task<List<TodoTask>> Handle(GetOverDueTaskQuery request, CancellationToken cancellationToken)
        {
            return await _todoTaskService.GetOverDueTasks();
        }
    }
}
