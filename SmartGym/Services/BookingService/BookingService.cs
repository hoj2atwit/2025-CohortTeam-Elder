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
      var classBooking = await _unitOfWork.BookingsRepository.GetAsync(b => b.ClassId == classId);
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
  /// <param name="classId"></param>
  /// <returns></returns>
  public async Task<bool> IsUserAlreadyBooked(int userId, int classId)
  {
    try
    {
      var booking = await _unitOfWork.BookingsRepository.GetAsync(b => b.UserId == userId && b.ClassId == classId);
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
      var classBookings = await _unitOfWork.BookingsRepository.GetAsync(b => b.ClassId == classId);
      return classBookings.Count();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error in CountBookingsForClass: {ex.Message}");
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
               b.Class.Schedule <= cutoff,
          includeProperties: "Class"
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
}
