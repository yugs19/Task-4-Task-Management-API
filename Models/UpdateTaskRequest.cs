using System.ComponentModel.DataAnnotations;

namespace Task4TaskApi.Models;

public sealed class UpdateTaskRequest
{
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
}
