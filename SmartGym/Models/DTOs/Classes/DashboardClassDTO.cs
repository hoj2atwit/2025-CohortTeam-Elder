namespace SmartGym.Models;

public class DashboardClassDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Schedule { get; set; }
    public int Capacity { get; set; }
    public int TrainerId { get; set; }
    public string TrainerName { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public int SpotsLeft => Capacity; // For now, assuming no bookings are counted
}
