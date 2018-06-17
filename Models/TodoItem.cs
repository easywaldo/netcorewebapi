using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Name should be shorter than 50 character")]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsComplete { get; set; }
    }
}   