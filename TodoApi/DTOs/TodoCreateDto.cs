using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs {
    public class TodoCreateDto {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Range(1, 3)]
        public int Priority { get; set; }
    }
}
