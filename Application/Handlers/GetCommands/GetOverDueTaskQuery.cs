using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR;

namespace Application.Handlers.GetTasks
{
    public class GetOverDueTaskQuery : IRequest<List<TodoTask>> { }
    
}
