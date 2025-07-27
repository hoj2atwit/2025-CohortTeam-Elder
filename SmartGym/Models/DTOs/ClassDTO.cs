using System.ComponentModel.DataAnnotations;

namespace SmartGym.Models;

public class ClassDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Schedule is required")]
    public DateTime Schedule { get; set; }

    [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Trainer ID is required")]
    public int TrainerId { get; set; }
    public int CategoryId { get; set; }
}