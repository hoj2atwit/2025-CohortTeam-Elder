namespace SmartGym.Models;

public class Class
{
 public int Id { get; set; }
 public string Name { get; set; }
 public DateTime Schedule { get; set; }
 public int Capacity { get; set; }
 public int TrainerId { get; set; }
 public User? Trainer { get; set; }
 public int CategoryId { get; set; }
 public Category? Category { get; set; }
 
}