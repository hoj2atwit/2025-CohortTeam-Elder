using SmartGym.Models;

namespace SmartGym.Services;

public interface IBookingService
{
	#region bookings

	Task<BookingDTO> CreateBooking(BookingPostDTO newBookingData);
	Task<BookingDTO?> UpdateBookingById(int id, BookingPatchDTO newBookingData);
	Task<BookingDTO?> UpdateBookingStatus(int id, int newStatus);
	Task<bool> DeleteBooking(int id);
	Task<List<BookingDTO>> GetAllBookings();
	Task<List<BookingDTO>> GetBookingByUserId(int userId);
	Task<bool> IsUserAlreadyBooked(int userId, int sessionId);
	Task<List<BookingDTO>> GetBookingsByClassId(int classId);
	Task<List<BookingDTO>> GetBookingsBySessionId(int sessionId);
	Task<int> CountBookingsForSession(int classId);
	Task<int> CountBookingsForClass(int classId);
	Task AutoCancelStaleBookings();
	Task<BookingDTO> GetBooking(int id);
	#endregion
	#region Sessions
	Task<ClassSessionDTO> GetClassSession(int sessionId);
	Task<List<ClassSessionDTO>> GetAllClassSessions();
	Task<ClassSessionDTO?> UpdateClassSession(int id, ClassSessionDTO sessionDto);
	Task<bool> DeleteClassSession(int id);
	#endregion

	#region Waitlist
	Task<List<WaitlistDTO>> GetFullWaitList(bool includeNestedClasses = false);
	Task<List<WaitlistDTO>> GetWaitlistBySession(int id, bool includeNestedClasses = false);
	Task<List<WaitlistDTO>> GetWaitlistByClassId(int classId, bool includeNestedClasses = false);
	Task<List<WaitlistDTO>> GetWaitlistByUser(int userId, bool includeNestedClasses = false);
	Task<WaitlistDTO?> UpdateWaitListRecord(int id, WaitlistDTO waitlistDto);
	Task<bool> DeleteFromWaitlist(int id);
	#endregion
}
