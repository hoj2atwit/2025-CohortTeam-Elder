using AutoMapper;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class CafeService : ICafeService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public CafeService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	/// <summary>
	/// Get the entire catalog of menu items
	/// </summary>
	/// <returns></returns>
	public async Task<List<MenuItemsDTO>> GetFullMenu()
	{
		try
		{
			var fullMenu = await _unitOfWork.MenuItemRepository.GetAsync();
			var fullMenuList = _mapper.Map<List<MenuItemsDTO>>(fullMenu);
			return fullMenuList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetFullMenu: {ex.Message}");
			return null;
		}
	}

	/// <summary>
	/// gets single menu item by id
	/// </summary>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public async Task<MenuItemsDTO> GetMenuItem(int itemId)
	{
		try
		{
			var menuItem = await _unitOfWork.MenuItemRepository.GetAsync(itemId);
			return _mapper.Map<MenuItemsDTO>(menuItem);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetMenuItem: {ex.Message}");
			return null;
		}
	}

	/// <summary>
	/// updates a menu item's details
	/// </summary>
	/// <param name="itemId"></param>
	/// <param name="menuItemDto"></param>
	/// <returns></returns>
	public async Task<MenuItemsDTO?> UpdateMenuItem(int itemId, MenuItemsDTO menuItemDto)
	{
		try
		{
			var menuItem = await _unitOfWork.MenuItemRepository.GetAsync(itemId);
			if (menuItem == null)
				return null;

			_mapper.Map(menuItemDto, menuItem);

			_unitOfWork.MenuItemRepository.Update(menuItem);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<MenuItemsDTO>(menuItem);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating menu item: {ex.Message}");
			return null;
		}
	}
	/// <summary>
	/// remove menu item from inventory
	/// </summary>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public async Task<bool> DeleteMenuItem(int itemId)
	{
		try
		{
			var menuItem = await _unitOfWork.MenuItemRepository.GetAsync(itemId);
			if (menuItem == null)
				return false;

			_unitOfWork.MenuItemRepository.Delete(menuItem);
			await _unitOfWork.SaveAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while deleting menu item: {ex.Message}");
			return false;
		}
	}

	public void GetCafeItemModifications()
	{
		throw new NotImplementedException();
	}

	public void GetCurrentPromos()
	{
		throw new NotImplementedException();
	}


	public void GetMealPrepItems()
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// Updates the menu image in the database and stores the image bytes.
	/// </summary>
	/// <param name="id">Menu item ID</param>
	/// <param name="imageRef">New image URL or file name</param>
	/// <param name="imageBytes">Image file bytes</param>
	/// <returns>True if update was successful, false otherwise</returns>
	public async Task<(bool Success, string Message)> UploadMenuImageBlob(int id, string imageRef, byte[] imageBytes, string guid)
	{
		try
		{
			var menuItem = await _unitOfWork.MenuItemRepository.GetAsync(id);
			if (menuItem == null)
				return (false, "Menu item not found");

			var existingImage = (await _unitOfWork.ImagesRepository
				.GetAsync(x => x.ImageRef != null && x.ImageRef.Contains(guid)))
				.FirstOrDefault();

			if (existingImage != null)
			{
				existingImage.ImageRef = imageRef;
				existingImage.Data = imageBytes;
				existingImage.UpdatedUtcDate = DateTime.UtcNow;
				_unitOfWork.ImagesRepository.Update(existingImage);
				menuItem.ImageRef = existingImage.ImageRef;
			}
			else
			{
				var newImage = new Images
				{
					ImageRef = imageRef,
					Data = imageBytes,
					IsUserImage = false,
					UpdatedUtcDate = DateTime.UtcNow
				};
				await _unitOfWork.ImagesRepository.AddAsync(newImage);
				menuItem.ImageRef = newImage.ImageRef;
			}

			_unitOfWork.MenuItemRepository.Update(menuItem);
			await _unitOfWork.SaveAsync();

			return (true, "Menu image uploaded and reference updated successfully");
		}
		catch (Exception ex)
		{
			return (false, $"Error while uploading menu image: {ex.Message}");
		}
	}
}
