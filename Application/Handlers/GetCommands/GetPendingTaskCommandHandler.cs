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
        ITodoTaskRepository _todoTaskRepository;
        public GetPendingTaskCommandHandler(ITodoTaskRepository todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }
        public Task<List<TodoTask>> Handle(GetPendingTaskCommand request, CancellationToken cancellationToken)
        {
            return _todoTaskRepository.GetPendingTasks();
        }
    }
}
