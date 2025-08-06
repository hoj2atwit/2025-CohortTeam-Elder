using SmartGym.Models;

namespace SmartGym.Services;

public interface ICafeService
{
	Task<List<MenuItemsDTO>> GetFullMenu();
	Task<MenuItemsDTO> GetMenuItem(int itemId);
	Task<MenuItemsDTO?> UpdateMenuItem(int itemId, MenuItemsDTO menuItemDto);
	Task<bool> DeleteMenuItem(int itemId);
	Task<(bool Success, string Message)> UploadMenuImageBlob(int id, string imageRef, byte[] imageBytes, string guid);

	// void GetMealPrepItems();
	// void GetCafeItemModifications();
	// void GetCurrentPromos();
}
