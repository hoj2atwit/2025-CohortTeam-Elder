using AutoMapper;
using SmartGym.Constants.Enums;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class BookingService : IBookingService
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	#region Bookings
	public async Task<BookingDTO> GetBooking(int id)
	{
		try
		{
			var booking = await _unitOfWork.BookingsRepository.GetAsync(id);
			var bookingDto = _mapper.Map<BookingDTO>(booking);
			return bookingDto;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetBooking: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// Get all existing bookings
	/// </summary>
	/// <returns></returns>
	public async Task<List<BookingDTO>> GetAllBookings()
	{
		try
		{
			var bookings = await _unitOfWork.BookingsRepository.GetAsync();
			var bookingsList = _mapper.Map<List<BookingDTO>>(bookings);
			return bookingsList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllBookings: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// Get bookings of an specific user
	/// </summary>
	/// <param name="userId"></param>
	/// <returns></returns>
	public async Task<List<BookingDTO>> GetBookingByUserId(int userId)
	{
		try
		{
			var userBooking = await _unitOfWork.BookingsRepository.GetAsync(b => b.UserId == userId);
			var userBookingsList = _mapper.Map<List<BookingDTO>>(userBooking);
			return userBookingsList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetBookingByUserId: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// Get bookings of an specific class
	/// </summary>
	/// <param name="classId"></param>
	/// <returns></returns>
	public async Task<List<BookingDTO>> GetBookingsByClassId(int classId)
	{
		try
		{
			var classBooking = await _unitOfWork.BookingsRepository.GetAsync(x => x.ClassSession.ClassId == classId);
			var classBookingsList = _mapper.Map<List<BookingDTO>>(classBooking);
			return classBookingsList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetBookingsByClassId: {ex.Message}");
			return null;
		}
	}

	/// <summary>
	/// Get bookings of an specific class, by session
	/// </summary>
	/// <param name="sessionId"></param>
	/// <returns></returns>
	public async Task<List<BookingDTO>> GetBookingsBySessionId(int sessionId)
	{
		try
		{
			var classBooking = await _unitOfWork.BookingsRepository.GetAsync(x => x.ClassSessionId == sessionId);
			var classBookingsList = _mapper.Map<List<BookingDTO>>(classBooking);
			return classBookingsList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetBookingsByClassId: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// Checking if a specific user is booked on a specific class
	/// </summary>
	/// <param name="userId"></param>
	/// <param name="sessionId"></param>
	/// <returns></returns>
	public async Task<bool> IsUserAlreadyBooked(int userId, int sessionId)
	{
		try
		{
			var booking = await _unitOfWork.BookingsRepository.GetAsync(b => b.UserId == userId && b.ClassSessionId == sessionId);
			return booking.Any();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in IsUserAlreadyBooked: {ex.Message}");
			return false;
		}
	}
	/// <summary>
	/// Checks how many bookings for a class (used to check capacity as well)
	/// </summary>
	/// <param name="classId"></param>
	/// <returns></returns>
	public async Task<int> CountBookingsForClass(int classId)
	{
		try
		{
			var classBooking = await _unitOfWork.BookingsRepository.GetAsync(classId);
			var classBookingsList = _mapper.Map<List<BookingDTO>>(classBooking);
			return classBookingsList.Count;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in CountBookingsForClass: {ex.Message}");
			return 0;
		}
	}
	public async Task<int> CountBookingsForSession(int sessionId)
	{
		try
		{
			var classBookings = await _unitOfWork.BookingsRepository.GetAsync(b => b.ClassSessionId == sessionId);
			return classBookings.Count();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in CountBookingsForSession: {ex.Message}");
			return 0;
		}
	}
	/// <summary>
	/// creates a new booking; controller handles a bunch of checks before creating
	/// </summary>
	/// <param name="newBookingData"></param>
	/// <returns></returns>
	public async Task<BookingDTO> CreateBooking(BookingPostDTO newBookingData)
	{
		try
		{
			Booking newBooking = _mapper.Map<Booking>(newBookingData);
			newBooking.Status = (int)BookingStatus.Pending;
			newBooking.CreatedAt = DateTime.UtcNow;
			await _unitOfWork.BookingsRepository.AddAsync(newBooking);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<BookingDTO>(newBooking);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in CreateBooking: {ex.Message}");
			throw;
		}
	}
	/// <summary>
	/// Updates a booking considering its Id
	/// </summary>
	/// <param name="id"></param>
	/// <param name="newBookingData"></param>
	/// <returns></returns>
	public async Task<BookingDTO?> UpdateBookingById(int id, BookingPatchDTO newBookingData)
	{
		try
		{
			var booking = await _unitOfWork.BookingsRepository.GetAsync(id);
			if (booking == null)
				return null;

			_mapper.Map(newBookingData, booking);
			_unitOfWork.BookingsRepository.Update(booking);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<BookingDTO>(booking);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating Booking: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// updates just the status of the booking (please refer to the BookingStatus Enum)
	/// </summary>
	/// <param name="id"></param>
	/// <param name="newStatus"></param>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public async Task<BookingDTO?> UpdateBookingStatus(int id, int newStatus)
	{
		try
		{
			var booking = await _unitOfWork.BookingsRepository.GetAsync(id);
			if (booking == null)
				return null;
			if (!Enum.IsDefined(typeof(BookingStatus), newStatus))
			{
				throw new ArgumentOutOfRangeException(nameof(newStatus), "Invalid status value.");
			}
			booking.Status = (BookingStatus)newStatus;
			_unitOfWork.BookingsRepository.Update(booking);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<BookingDTO>(booking);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating Booking Status: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// delets a booking
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<bool> DeleteBooking(int id)
	{
		try
		{
			var booking = await _unitOfWork.BookingsRepository.GetAsync(id);
			if (booking == null)
				return false;

			_unitOfWork.BookingsRepository.Delete(booking);
			await _unitOfWork.SaveAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while deleting booking: {ex.Message}");
			return false;
		}
	}
	/// <summary>
	/// Get pending bookings for classes starting within the next 10 minutes and mark them as NoShow
	/// </summary>
	/// <returns></returns>
	public async Task AutoCancelStaleBookings()
	{
		try
		{
			var now = DateTime.Now;
			var cutoff = now.AddMinutes(10);


			var staleBookings = await _unitOfWork.BookingsRepository.GetAsync(
				 b => b.Status == BookingStatus.Pending &&
						b.ClassSession.SessionDateTime <= cutoff,
				 includeProperties: "ClassSession"
			);

			foreach (var booking in staleBookings)
			{
				booking.Status = BookingStatus.NoShow;
				_unitOfWork.BookingsRepository.Update(booking);
			}

			await _unitOfWork.SaveAsync();

			Console.WriteLine($"{staleBookings.Count()} stale bookings marked as no show.");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in AutoCancelStaleBookings: {ex.Message}");
		}
	}
	#endregion

	#region Sessions
	public async Task<ClassSessionDTO> GetClassSession(int sessionId)
	{
		try
		{
			var session = await _unitOfWork.ClassSessionRepository.GetAsync(sessionId);
			var sessionDto = _mapper.Map<ClassSessionDTO>(session);
			return sessionDto;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetClassSession: {ex.Message}");
			return null;
		}
	}
	public async Task<List<ClassSessionDTO>> GetAllClassSessions()
	{
		try
		{
			var sessions = await _unitOfWork.ClassSessionRepository.GetAsync();
			var sessionDtos = _mapper.Map<List<ClassSessionDTO>>(sessions);
			return sessionDtos.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllClassSessions: {ex.Message}");
			return null;
		}
	}

	public async Task<ClassSessionDTO?> UpdateClassSession(int id, ClassSessionDTO sessionDto)
	{
		try
		{
			var session = await _unitOfWork.ClassSessionRepository.GetAsync(id);
			if (session == null)
				return null;

			_mapper.Map(sessionDto, session);
			_unitOfWork.ClassSessionRepository.Update(session);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<ClassSessionDTO>(session);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating ClassSession: {ex.Message}");
			return null;
		}
	}

	public async Task<bool> DeleteClassSession(int id)
	{
		try
		{
			var session = await _unitOfWork.ClassSessionRepository.GetAsync(id);
			if (session == null)
				return false;

			_unitOfWork.ClassSessionRepository.Delete(session);
			await _unitOfWork.SaveAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while deleting ClassSession: {ex.Message}");
			return false;
		}
	}
	#endregion

	#region Waitlists

	/// <summary>
	/// Gets entire
	/// </summary>
	/// <returns></returns>
	public async Task<WaitlistDTO> GetSingleWaitlistRecord(int id, bool includeNestedClasses = false)
	{
		try
		{
			var includeProps = includeNestedClasses ? "Member,Session,Session.Class" : "";
			var waitlist = await _unitOfWork.WaitlistRepository.GetAsync(x => x.Id == id, includeProperties: includeProps);
			var waitlistDto = _mapper.Map<WaitlistDTO>(waitlist.FirstOrDefault());
			return waitlistDto;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetSingleWaitlistRecord: {ex.Message}");
			return null;
		}
	}

	/// <summary>
	/// Gets entire
	/// </summary>
	/// <returns></returns>
	public async Task<List<WaitlistDTO>> GetFullWaitList(bool includeNestedClasses = false)
	{
		try
		{
			var includeProps = includeNestedClasses ? "Member,Session,Session.Class" : "";
			var waitlist = await _unitOfWork.WaitlistRepository.GetAsync(includeProperties: includeProps);
			var waitlistDtoList = _mapper.Map<List<WaitlistDTO>>(waitlist);
			return waitlistDtoList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetFullWaitList: {ex.Message}");
			return null;
		}
	}

	public async Task<List<WaitlistDTO>> GetWaitlistBySession(int id, bool includeNestedClasses = false)
	{
		try
		{
			var includeProps = includeNestedClasses ? "Member,Session,Session.Class" : "";
			var waitlist = await _unitOfWork.WaitlistRepository.GetAsync(x => x.Session.Id == id, includeProperties: includeProps);
			var waitlistDtoList = _mapper.Map<List<WaitlistDTO>>(waitlist);
			return waitlistDtoList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetWaitlistBySession: {ex.Message}");
			return null;
		}
	}

	public async Task<List<WaitlistDTO>> GetWaitlistByClassId(int classId, bool includeNestedClasses = false)
	{
		try
		{
			var includeProps = includeNestedClasses ? "Member,Session,Session.Class" : "";
			var waitlist = await _unitOfWork.WaitlistRepository.GetAsync(x => x.Session.ClassId == classId, includeProperties: includeProps);
			var waitlistDtoList = _mapper.Map<List<WaitlistDTO>>(waitlist);
			return waitlistDtoList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetWaitlistByClassId: {ex.Message}");
			return null;
		}
	}

	public async Task<List<WaitlistDTO>> GetWaitlistByUser(int userId, bool includeNestedClasses = false)
	{
		try
		{
			var includeProps = includeNestedClasses ? "Member,Session,Session.Class" : "";
			var waitlist = await _unitOfWork.WaitlistRepository.GetAsync(x => x.MemberId == userId, includeProperties: includeProps);
			var waitlistDtoList = _mapper.Map<List<WaitlistDTO>>(waitlist);
			return waitlistDtoList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetWaitlistByUser: {ex.Message}");
			return null;
		}
	}

	public async Task<WaitlistDTO?> UpdateWaitListRecord(int id, WaitlistDTO waitlistDto)
	{
		try
		{
			var waitlistRecord = await _unitOfWork.WaitlistRepository.GetAsync(id);
			if (waitlistRecord == null)
				return null;

			_mapper.Map(waitlistDto, waitlistRecord);
			_unitOfWork.WaitlistRepository.Update(waitlistRecord);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<WaitlistDTO>(waitlistRecord);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating waitlist record: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// This method will remove a record from the waitlist and automatically move each person up the queue
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public async Task<bool> DeleteFromWaitlist(int id)
	{
		try
		{
			var waitlistRecord = await _unitOfWork.WaitlistRepository.GetAsync(id);
			if (waitlistRecord == null)
				return false;

			int sessionId = waitlistRecord.SessionId;
			int deletedPosition = waitlistRecord.Position;

			_unitOfWork.WaitlistRepository.Delete(waitlistRecord);

			// Get all waitlist records that were behind
			var affectedRecords = await _unitOfWork.WaitlistRepository.GetAsync(
				w => w.SessionId == sessionId && w.Position > deletedPosition
			);

			// Decrement their positions
			foreach (var record in affectedRecords)
			{
				record.Position -= 1;
				_unitOfWork.WaitlistRepository.Update(record);
			}

			await _unitOfWork.SaveAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while deleting waitlist record: {ex.Message}");
			return false;
		}
	}
	#endregion
}
