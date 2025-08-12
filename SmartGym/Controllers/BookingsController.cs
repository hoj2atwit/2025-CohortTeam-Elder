using Microsoft.AspNetCore.Mvc;
using SmartGym.Data;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
	private readonly IBookingService _bookingService;
	private readonly IUnitOfWork _unitOfWork;

	public BookingsController(IBookingService bookingService, IUnitOfWork unitOfWork)
	{
		_bookingService = bookingService;
		_unitOfWork = unitOfWork;
	}

	[HttpGet]
	public async Task<ActionResult<List<BookingDTO>>> GetAll([FromQuery] bool includeNestedClasses = false)
	{
		var bookings = await _bookingService.GetAllBookings(includeNestedClasses);
		return Ok(bookings);
	}

	[HttpGet("user/{userId:int}")]
	public async Task<ActionResult<List<BookingDTO>>> GetByUserId(int userId)
	{
		var bookings = await _bookingService.GetBookingByUserId(userId);
		return Ok(bookings);
	}

	[HttpGet("class/{classId:int}")]
	public async Task<ActionResult<List<BookingDTO>>> GetByClassId(int classId)
	{
		var bookings = await _bookingService.GetBookingsByClassId(classId);
		return Ok(bookings);
	}
	[HttpGet("session/{sessionId:int}")]
	public async Task<ActionResult<List<BookingDTO>>> GetBySessionId(int sessionId)
	{
		var bookings = await _bookingService.GetBookingsBySessionId(sessionId);
		return Ok(bookings);
	}

	[HttpGet("check")]
	public async Task<ActionResult<bool>> IsUserAlreadyBooked(int userId, int classId)
	{
		var result = await _bookingService.IsUserAlreadyBooked(userId, classId);
		return Ok(result);
	}

	[HttpGet("count/{sessionId:int}")]
	public async Task<ActionResult<int>> CountBookingsForSession(int sessionId)
	{
		var count = await _bookingService.CountBookingsForSession(sessionId);
		return Ok(count);
	}

  [HttpPost]
  public async Task<IActionResult> BookClass([FromBody] BookingPostDTO bookingData)
  {
    var classList = await _unitOfWork.ClassRepository.GetAsync(c => c.Id == bookingData.ClassSessionId);
    var classItem = classList.FirstOrDefault();
    if (classItem == null)
      return NotFound("Class not found");

		var alreadyBooked = await _bookingService.IsUserAlreadyBooked(bookingData.UserId, bookingData.ClassSessionId);
		if (alreadyBooked)
			return BadRequest("User is already booked for this class");

		var alreadyOnWaitlist = await _bookingService.GetWaitlistBySession(bookingData.ClassSessionId);
		if (alreadyOnWaitlist.Any(x => x.MemberId == bookingData.UserId))
			return BadRequest("User is already on the waitlist for this class");

		var currentBookings = await _bookingService.CountBookingsForSession(bookingData.ClassSessionId);
		if (currentBookings >= classItem.MaxCapacity)
		{
			await _bookingService.CreateBooking(bookingData);
			return BadRequest("Class is full, user has been added to waitlist.");
		}

		var newBooking = await _bookingService.CreateBooking(bookingData);
		return CreatedAtAction(nameof(GetByUserId), new { userId = bookingData.UserId }, newBooking);
	}

  [HttpPatch("{id:int}")]
  public async Task<ActionResult<BookingDTO>> Update(int id, [FromBody] BookingPatchDTO patch)
  {
    var updated = await _bookingService.UpdateBookingById(id, patch);
    if (updated == null)
      return NotFound();
    return Ok(updated);
  }

	[HttpPatch("{id:int}/status/{newStatus:int}")]
	public async Task<ActionResult<BookingDTO>> UpdateStatus(int id, int newStatus)
	{
		var updated = await _bookingService.UpdateBookingStatus(id, newStatus);
		if (updated == null)
			return NotFound();
		return Ok(updated);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteBooking(int id)
	{
		var result = await _bookingService.DeleteBooking(id);
		if (!result)
			return NotFound();
		return NoContent();
	}

	[HttpPost("autocancel")]
	public async Task<IActionResult> AutoCancel()
	{
		await _bookingService.AutoCancelStaleBookings();
		return Ok("Stale bookings processed.");
	}

}