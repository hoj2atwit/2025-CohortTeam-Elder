using SmartGym.Models;

namespace SmartGym.Services;

public interface IClassService
{
	Task<ClassDTO> CreateClass(ClassPostDTO newClassData);
	Task<ClassDTO?> UpdateClassById(int id, ClassPatchDTO newClassData);
	Task<bool> DeleteClass(int id);
	Task<List<ClassDTO>> GetAllClasses();
	Task<ClassDTO> GetClassById(int id);
}
