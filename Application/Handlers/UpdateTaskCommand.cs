using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Todo;
using MediatR; 

namespace Application.Handlers
{
    public class UpdateTaskCommand : IRequest<TodoTask>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public UpdateTaskCommand(string title, string description, DateTime dueDate, bool isCompleted)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = isCompleted;
        }
    }
}
