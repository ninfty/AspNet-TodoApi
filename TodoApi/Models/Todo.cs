using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Todo
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Secret { get; set; }
}