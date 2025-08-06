using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants.Enums;

public enum BookingStatus
{
  [Display(Name = "Pending")]
  Pending = 0,
  [Display(Name = "Confirmed")]
  Confirmed = 1,
  [Display(Name = "Completed")]
  Completed = 3,
  [Display(Name = "No Show")]
  NoShow = 4,
  [Display(Name = "Expired")]
  Expired = 5,
  [Display(Name = "Cancelled")]
  Cancelled = 6
}
