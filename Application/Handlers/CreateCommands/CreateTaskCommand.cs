using MediatR; // MediatR
using Domain.Todo;

namespace Application.Handlers.CreateCommands

{
    public class CreateTaskCommand : IRequest<TodoTask>
    {
        //public int id { get; set;  }
        public string Title { get; set; }
        //public string Completed { get; set; }
        //public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public CreateTaskCommand(string title, DateTime dueDate)
        {
            //this.id = id;
            Title = title;
            //this.Completed = completed;
            //this.Description = description;
            DueDate = dueDate;
        }
    }
}