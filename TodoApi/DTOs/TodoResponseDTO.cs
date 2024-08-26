using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class TodoResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
