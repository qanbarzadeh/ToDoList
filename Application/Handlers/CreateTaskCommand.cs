using MediatR; // MediatR
using Domain.Todo;

namespace Application.Handlers
    
{
    public class CreateTaskCommand : IRequest<TodoTask>
    {
        //public int id { get; set;  }
        public string Title { get; set; }
        //public string Completed { get; set; }
        //public string Description { get; set; }
        public string DueDate { get; set; }
        
        public CreateTaskCommand(string title, string dueDate)
        {
            //this.id = id;
            this.Title = title;
            //this.Completed = completed;
            //this.Description = description;
            this.DueDate = dueDate;
        }
    }
}