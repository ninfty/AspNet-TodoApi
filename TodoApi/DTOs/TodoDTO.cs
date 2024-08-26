using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace TodoApi.DTOs
{
    public class TodoDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
