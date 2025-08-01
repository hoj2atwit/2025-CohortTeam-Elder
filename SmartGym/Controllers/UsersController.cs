using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
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
		public async Task<ActionResult<User>> CreateUser(UserDto newUserData)
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

		[HttpGet("checkins/{id:int}")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetUserCheckins(int id)
		{
			var checkinList = await _service.GetUserCheckins(id);

			return Ok(checkinList);
		}
		[HttpGet("checkins")]
		public async Task<ActionResult<IEnumerable<CheckinDTO>>> GetAllUserCheckins()
		{
			var checkinList = await _service.GetAllUserCheckins();
			return Ok(checkinList);
		}
	}
}
