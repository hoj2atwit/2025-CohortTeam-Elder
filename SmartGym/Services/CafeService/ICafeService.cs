using SmartGym.Models;

namespace SmartGym.Services;

public interface ICafeService
{
	Task<List<MenuItemsDTO>> GetFullMenu();
	Task<MenuItemsDTO> GetMenuItem(int itemId);
	Task<MenuItemsDTO?> UpdateMenuItem(int itemId, MenuItemsDTO menuItemDto);
	Task<bool> DeleteMenuItem(int itemId);
	// void GetMealPrepItems();
	// void GetCafeItemModifications();
	// void GetCurrentPromos();
}
