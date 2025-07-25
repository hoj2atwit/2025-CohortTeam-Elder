namespace SmartGym.Models;

public class ClassDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Schedule { get; set; }
    public int Capacity { get; set; }
    public int TrainerId { get; set; }
    public int CategoryId { get; set; }
}