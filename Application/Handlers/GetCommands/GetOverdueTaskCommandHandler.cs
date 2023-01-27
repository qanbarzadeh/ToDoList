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
    public class GetOverdueTaskCommandHandler : IRequestHandler<GetOverDueTaskCommand, List<TodoTask>>
    {
        private readonly ITodoTasksService _todoTaskService;
        public GetOverdueTaskCommandHandler(ITodoTasksService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }
    
        public async Task<List<TodoTask>> Handle(GetOverDueTaskCommand request, CancellationToken cancellationToken)
        {
            return await _todoTaskService.GetOverDueTasks();
        }
    }
}
