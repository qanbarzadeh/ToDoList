using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR;  

namespace Application.Handlers.Update
{
    public class UpdateTaskCommand : IRequest<TodoTask>
    {
        public  int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    

}
