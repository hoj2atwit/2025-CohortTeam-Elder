using System.ComponentModel.DataAnnotations;

namespace SmartGym.Constants;

public enum CheckinMethod
{
	[Display(Name = "qr")]
	QR,
	[Display(Name = "desk")]
	Desk,
	[Display(Name = "app")]
	App
}
