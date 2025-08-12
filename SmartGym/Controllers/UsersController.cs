using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartGym.Constants;
using SmartGym.Constants.Enums;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _service;

		public UsersController(IUserService service)
		{
			_service = service;
		}

		[HttpPost("checkin")]
		public async Task<IActionResult> CheckInUser([FromBody] UserDto user, [FromQuery] AccessPoint accessPoint, [FromQuery] CheckinMethod checkinMethod)
		{
			var result = await _service.CheckInUser(user, accessPoint, checkinMethod);
			if (result)
				return Ok();
			return StatusCode(500, "Failed to check in user.");
		}
		[Authorize(Roles = "Admin, Staff")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
		{
			var userList = await _service.GetAllUsers();
			return Ok(userList);
		}
		[Authorize(Roles = "Admin, Staff")]
		[HttpGet("{id:int}")]
		public async Task<ActionResult<UserDto>> GetUserById(int id)
		{
			var user = await _service.GetUserById(id);
			if (user == null)
				return NotFound();

			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<ActionResult<AppUser>> CreateUser(UserDto newUserData)
		{
			var newUser = await _service.CreateUser(newUserData);
			return CreatedAtAction(nameof(CreateUser), new { id = newUser.Id }, newUser);
		}
		[Authorize(Roles = "Admin, Staff")]
		[HttpPatch("{id:int}")]
		public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
		{
			if (id != userDto.Id)
				return BadRequest();
			var patched = await _service.UpdateUser(id, userDto);

			if (patched != null)
				return Ok(patched);
			else
				return StatusCode(500);
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var removed = await _service.DeleteUser(id);
			if (!removed)
				return NotFound();
			return Ok();
		}
		/// <summary>
		/// Gets all checkins for the given user id.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="includeUser"></param>
		/// <returns></returns>
		[HttpGet("checkins/{id:int}")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetUserCheckins(int id, [FromQuery] bool includeUser = false)
		{
			var checkinList = await _service.GetUserCheckins(id, includeUser);
			return Ok(checkinList);
		}

		/// <summary>
		/// Gets entire history of checkins.
		/// </summary>
		/// <param name="includeUser"></param>
		/// <returns></returns>
		[HttpGet("checkins")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetAllUserCheckins([FromQuery] bool includeUser = false)
		{
			var checkinList = await _service.GetAllUserCheckins(includeUser);
			return Ok(checkinList);
		}

		/// <summary>
		/// Gets user checkins filtered by access point.
		/// </summary>
		/// <param name="accessPoint"></param>
		/// <param name="includeUser"></param>
		/// <returns></returns>
		[HttpGet("checkins/by-access-point")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByAccessPoint([FromQuery] AccessPoint accessPoint, [FromQuery] bool includeUser = false)
		{
			var checkins = await _service.GetCheckinsByAccessPoint(accessPoint, includeUser);
			return Ok(checkins);
		}

		/// <summary>
		/// Gets user checkins filtered by time range.
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="includeUser"></param>
		/// <returns></returns>
		[HttpGet("checkins/by-time")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByTime([FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] bool includeUser = false)
		{
			var checkins = await _service.GetCheckinsByTime(startTime, endTime, includeUser);
			return Ok(checkins);
		}

		/// <summary>
		/// Gets user checkins filtered by checkin method.
		/// </summary>
		/// <param name="method"></param>
		/// <param name="includeUser"></param>
		/// <returns></returns>
		[HttpGet("checkins/by-method")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByMethod([FromQuery] CheckinMethod method, [FromQuery] bool includeUser = false)
		{
			var checkins = await _service.GetCheckinsByMethod(method, includeUser);
			return Ok(checkins);
		}


		/// <summary>
		/// gets all account history entries
		/// <param name="includeUser"></param>
		/// </summary>
		[HttpGet("account-history")]
		public async Task<ActionResult<IEnumerable<AccountHistoryDTO>>> GetAccountHistory([FromQuery] bool includeUser = false)
		{
			var history = await _service.GetAccHistory(includeUser);
			return Ok(history);
		}

		/// <summary>
		/// ets account history entries filtered by user status
		/// <param name="includeUser"></param>
		/// </summary>
		[HttpGet("account-history/status")]
		public async Task<ActionResult<IEnumerable<AccountHistoryDTO>>> GetAccountHistoryByStatus([FromQuery] UserStatus userStatus, [FromQuery] bool includeUser = false)
		{
			var history = await _service.GetAccHistoryByStatus(userStatus, includeUser);
			return Ok(history);
		}

		/// <summary>
		/// gets account history entries filtered by date range
		/// <param name="includeUser"></param>
		/// </summary>
		[HttpGet("account-history/dates")]
		public async Task<ActionResult<IEnumerable<AccountHistoryDTO>>> GetAccountHistoryByDates([FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] bool includeUser = false)
		{
			var history = await _service.GetAccHistoryByDates(startTime, endTime, includeUser);
			return Ok(history);
		}

		/// <summary>
		/// gets account history entries for a specific user
		/// <param name="includeUser"></param>
		/// </summary>
		[HttpGet("usr-account-history/{id:int}")]
		public async Task<ActionResult<IEnumerable<AccountHistoryDTO>>> GetAccountHistoryByUser(int id, [FromQuery] bool includeUser = false)
		{
			var history = await _service.GetAccHistoryByUser(id, includeUser);
			return Ok(history);
		}
		/// <summary>
		/// Gets account history entries filtered by user role.
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		[HttpGet("account-history/role")]
		public async Task<ActionResult<IEnumerable<AccountHistoryDTO>>> GetAccountHistoryByRole([FromQuery] RoleId roleId)
		{
			var history = await _service.GetAccHistoryByRole(roleId);
			return Ok(history);
		}
	}
}
