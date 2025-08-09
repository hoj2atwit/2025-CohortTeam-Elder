using SmartGym.Models;

namespace SmartGym.Services;

public interface IBookingService
{
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
}
