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
		/// gets all checkins for the given user id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("checkins/{id:int}")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetUserCheckins(int id)
		{
			var checkinList = await _service.GetUserCheckins(id);

			return Ok(checkinList);
		}
		/// <summary>
		/// gets entire history of checkins
		/// </summary>
		/// <returns></returns>
		[HttpGet("checkins")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetAllUserCheckins()
		{
			var checkinList = await _service.GetAllUserCheckins();
			return Ok(checkinList);
		}

		/// <summary>
		/// Gets user checkins filtered by access point.
		/// </summary>
		/// <param name="accessPoint"></param>
		/// <returns></returns>
		[HttpGet("checkins/by-access-point")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByAccessPoint([FromQuery] int accessPoint)
		{
			var accessPointEnum = (AccessPoint)accessPoint;
			var checkins = await _service.GetCheckinsByAccessPoint(accessPointEnum);
			return Ok(checkins);
		}

		/// <summary>
		/// Gets user checkins filtered by time range.
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		[HttpGet("checkins/by-time")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByTime([FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
		{
			var checkins = await _service.GetCheckinsByTime(startTime, endTime);
			return Ok(checkins);
		}

		/// <summary>
		/// Gets user checkins filtered by checkin method.
		/// </summary>
		/// <param name="method"></param>
		/// <returns></returns>
		[HttpGet("checkins/by-method")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetCheckinsByMethod([FromQuery] string method)
		{
			var checkins = await _service.GetCheckinsByMethod(method);
			return Ok(checkins);
		}
	}
}
