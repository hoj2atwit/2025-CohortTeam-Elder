using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SmartGym.Data;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassSessionController : ControllerBase
{
	private readonly IBookingService _bookingService;
	private readonly IUnitOfWork _unitOfWork;

	public ClassSessionController(IBookingService bookingService, IUnitOfWork unitOfWork)
	{
		_bookingService = bookingService;
		_unitOfWork = unitOfWork;
	}
	[HttpGet("sessions/{sessionId:int}")]
	public async Task<ActionResult<ClassSessionDTO>> GetClassSession(int sessionId)
	{
		try
		{
			var sessionDto = await _bookingService.GetClassSession(sessionId);
			if (sessionDto == null)
				return NotFound();

			return Ok(sessionDto);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetClassSession: {ex.Message}");
			return StatusCode(500, "Error retrieving class session.");
		}
	}
	[HttpGet("sessions")]
	public async Task<ActionResult<List<ClassSessionDTO>>> GetAllClassSessions()
	{
		try
		{
			var sessionDtos = await _bookingService.GetAllClassSessions();
			return Ok(sessionDtos);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllClassSessions: {ex.Message}");
			return StatusCode(500, "Error retrieving class sessions.");
		}
	}
	[Authorize(Roles = "Admin, Staff")]
	[HttpPatch("sessions/{id:int}")]
	public async Task<ActionResult<ClassSessionDTO>> UpdateClassSession(int id, [FromBody] ClassSessionDTO sessionDto)
	{
		try
		{
			var updatedSession = await _bookingService.UpdateClassSession(id, sessionDto);
			if (updatedSession == null)
				return NotFound();

			return Ok(updatedSession);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating ClassSession: {ex.Message}");
			return StatusCode(500, "Error updating class session.");
		}
	}
	[Authorize(Roles = "Admin, Staff")]
	[HttpDelete("sessions/{id:int}")]
	public async Task<IActionResult> DeleteClassSession(int id)
	{
		try
		{
			var deleted = await _bookingService.DeleteClassSession(id);
			if (!deleted)
				return NotFound();

			return NoContent();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while deleting ClassSession: {ex.Message}");
			return StatusCode(500, "Error deleting class session.");
		}
	}
}