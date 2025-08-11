using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SmartGym.Constants.Enums;

namespace SmartGym.Helpers
{
	public static class EnumHelper
	{
		/// <summary>
		/// helper method to grab display name from enum value
		/// </summary>
		/// <param name="value"></param>
		/// <returns>string</returns>
		public static string GetDisplayName(Enum value)
		{
			var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
			var display = member?.GetCustomAttribute<DisplayAttribute>();
			return display != null ? display.Name : "Unknown";
		}
		public static RoleId? GetRoleIdFromName(string roleName)
		{
			foreach (var value in Enum.GetValues(typeof(RoleId)).Cast<RoleId>())
			{
				var displayName = GetDisplayName(value);
				if (string.Equals(displayName, roleName, StringComparison.OrdinalIgnoreCase))
				{
					return value;
				}
			}
			return RoleId.Unknown;
		}
	}
}