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
  // Task<bool> DeleteBooking(int id);
  // Task<List<BookingDTO>> GetAllBookings();
  // Task<List<BookingDTO>> GetBookingByUserId(int userId);
  // Task<bool> IsUserAlreadyBooked(int userId, int classId);
  // Task<List<BookingDTO>> GetBookingsByClassId(int classId);
  // Task<int> CountBookingsForClass(int classId);
  // Task AutoCancelStaleBookings();
}
