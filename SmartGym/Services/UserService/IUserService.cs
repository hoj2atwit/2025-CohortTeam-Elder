using SmartGym.Constants;
using SmartGym.Constants.Enums;
using SmartGym.Models;

namespace SmartGym.Services;

public interface IUserService
{
    Task<bool> CheckInUser(UserDto user, AccessPoint accessPoint, CheckinMethod checkinMethod);
	Task<UserDto> CreateUser(UserDto newUserData);
	Task<UserDto> GetUserById(int id);
	Task<List<UserDto>> GetAllUsers();
	Task<List<CheckinDTO>> GetUserCheckins(int id, bool includeUser = false);
	Task<List<CheckinDTO>> GetAllUserCheckins(bool includeUser = false);
	void GetUserPaymentMethod(AppUser user);
	void GetTrafficData(DateTime date);
	void GetTrafficData(DateTime startDate, DateTime endDate);
	Task<UserDto?> UpdateUser(int id, UserDto userDto);
	Task<bool> DeleteUser(int id);
	Task<(bool Success, string Message)> UploadUserImageBlob(int id, string imageRef, byte[] imageBytes, string guid);
	Task<List<CheckinDTO>> GetCheckinsByAccessPoint(AccessPoint accessPoint, bool includeUser = false);
	Task<List<CheckinDTO>> GetCheckinsByTime(DateTime startTime, DateTime endTime, bool includeUser = false);
	Task<List<CheckinDTO>> GetCheckinsByMethod(CheckinMethod method, bool includeUser = false);
	Task<List<AccountHistoryDTO>> GetAccHistory(bool includeUser = false);
	Task<List<AccountHistoryDTO>> GetAccHistoryByStatus(UserStatus userStatus, bool includeUser = false);
	Task<List<AccountHistoryDTO>> GetAccHistoryByDates(DateTime startTime, DateTime endTime, bool includeUser = false);
	Task<List<AccountHistoryDTO>> GetAccHistoryByUser(int id, bool includeUser = false);
	Task<List<AccountHistoryDTO>> GetAccHistoryByRole(RoleId roleId);

}
