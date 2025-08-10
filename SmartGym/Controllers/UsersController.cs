using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers()
		{
			var userList = await _service.GetAllUsers();
			return Ok(userList);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<UserDto>> GetUserById(int id)
		{
			var user = await _service.GetUserById(id);
			if (user == null)
				return NotFound();

			return user;
		}

		[HttpPost]
		public async Task<ActionResult<AppUser>> CreateUser(UserDto newUserData)
		{
			var newUser = await _service.CreateUser(newUserData);
			return CreatedAtAction(nameof(CreateUser), new { id = newUser.Id }, newUser);
		}

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
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByAccessPoint([FromQuery] int accessPoint, [FromQuery] bool includeUser = false)
		{
			var accessPointEnum = (AccessPoint)accessPoint;
			var checkins = await _service.GetCheckinsByAccessPoint(accessPointEnum, includeUser);
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
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByMethod([FromQuery] string method, [FromQuery] bool includeUser = false)
		{
			var checkins = await _service.GetCheckinsByMethod(method, includeUser);
			return Ok(checkins);
		}
	}
}
