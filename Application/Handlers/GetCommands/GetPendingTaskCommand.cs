using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR;

namespace Application.Handlers.GetTasks
{
    internal class GetPendingTaskCommand : IRequest<List<TodoTask>>
    {

    }
}
