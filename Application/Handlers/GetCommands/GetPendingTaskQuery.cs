using Domain.Todo;
using MediatR;

namespace Application.Handlers.GetTasks
{
    public class GetPendingTaskQuery : IRequest<List<TodoTask>>
    {
    }
}
