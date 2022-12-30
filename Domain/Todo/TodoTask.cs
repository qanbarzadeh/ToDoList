using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Domain.Todo
{
    public class TodoTask : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(256, ErrorMessage = "Title must be no more than 256 characters")]
        public string Title { get; set; }
        public bool Completed { get; set; }        
        public DateTime? DueDate { get; set; }
        

        public IEnumerable<ValidationResult> Validate(ValidationContext task)
        {
            if (string.IsNullOrEmpty(Title))
            {
                yield return new ValidationResult("Title is required", new[] { nameof(Title) });
            }
            if (DueDate < DateTime.Now)
            {
                yield return new ValidationResult("Duedate must be in the future", new[] { nameof(DueDate) });
            }
        }
    }       
}
