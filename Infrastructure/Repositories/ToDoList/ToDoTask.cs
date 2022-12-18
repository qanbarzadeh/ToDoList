using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ToDoList
{
    public class ToDoTask
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = 10; 
        public string Title { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public bool IsTaskComplete { get; set; } = false; 

    }
}
 