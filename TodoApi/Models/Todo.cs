using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Todo
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Secret { get; set; }
}