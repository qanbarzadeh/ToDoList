using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Todo
{
    public class TodoTask
    {        
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; } = false;
        //public string status { get; set; } 
    }
}
