using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums;

public enum BookingStatus
{
  [Display(Name = "Pending")]
  Pending = 0,
  [Display(Name = "Confirmed")]
  Confirmed = 1,
  [Display(Name = "Completed")]
  Completed = 2,
  [Display(Name = "No Show")]
  NoShow = 3,
  [Display(Name = "Expired")]
  Expired = 4,
  [Display(Name = "Cancelled")]
  Cancelled = 5
}
