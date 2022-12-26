using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Domain.Todo
{
    public class TodoTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(256)]
        public string Title { get; set; }
        public DateTime? DueDate { get; set; } = DateTime.MinValue; 
        public bool Completed { get; set; } = false;
       
    }



}
