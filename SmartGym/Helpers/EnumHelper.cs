using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SmartGym.Constants.Enums;

namespace SmartGym.Helpers
{
    public class EnumHelper
    {
		public static string GetDisplayName(Enum value)
		{
			var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
			var display = member?.GetCustomAttribute<DisplayAttribute>();
			return display != null ? display.Name : "Unknown";
		}
	}
}