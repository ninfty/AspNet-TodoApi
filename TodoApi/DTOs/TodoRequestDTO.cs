using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class TodoRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsComplete { get; set; }
    }
}
