using Microsoft.AspNetCore.Mvc;
using SmartGym.Data;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("[controller]")]
public class WaitlistController : ControllerBase
{
	private readonly IBookingService _bookingService;
	private readonly IUnitOfWork _unitOfWork;

	public WaitlistController(IBookingService bookingService, IUnitOfWork unitOfWork)
	{
		_bookingService = bookingService;
		_unitOfWork = unitOfWork;
	}

	[HttpGet]
	public async Task<ActionResult<List<WaitlistDTO>>> GetAll([FromQuery] bool includeNestedClasses = false)
	{
		var waitlist = await _bookingService.GetFullWaitList(includeNestedClasses);
		if (waitlist == null)
			return StatusCode(500, "Error retrieving waitlist.");
		return Ok(waitlist);
	}

	[HttpGet("session/{sessionId:int}")]
	public async Task<ActionResult<List<WaitlistDTO>>> GetBySession(int sessionId, [FromQuery] bool includeNestedClasses = false)
	{
		var waitlist = await _bookingService.GetWaitlistBySession(sessionId, includeNestedClasses);
		if (waitlist == null)
			return StatusCode(500, "Error retrieving waitlist for session.");
		return Ok(waitlist);
	}

	[HttpGet("class/{classId:int}")]
	public async Task<ActionResult<List<WaitlistDTO>>> GetByClassId(int classId, [FromQuery] bool includeNestedClasses = false)
	{
		var waitlist = await _bookingService.GetWaitlistByClassId(classId, includeNestedClasses);
		if (waitlist == null)
			return StatusCode(500, "Error retrieving waitlist for class.");
		return Ok(waitlist);
	}

	[HttpGet("user/{userId:int}")]
	public async Task<ActionResult<List<WaitlistDTO>>> GetByUser(int userId, [FromQuery] bool includeNestedClasses = false)
	{
		var waitlist = await _bookingService.GetWaitlistByUser(userId, includeNestedClasses);
		if (waitlist == null)
			return StatusCode(500, "Error retrieving waitlist for user.");
		return Ok(waitlist);
	}

	[HttpPatch("{id:int}")]
	public async Task<ActionResult<WaitlistDTO>> Update(int id, [FromBody] WaitlistDTO waitlistDto)
	{
		var updated = await _bookingService.UpdateWaitListRecord(id, waitlistDto);
		if (updated == null)
			return NotFound();
		return Ok(updated);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _bookingService.DeleteFromWaitlist(id);
		if (!result)
			return NotFound();
		return NoContent();
	}
}