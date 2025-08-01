using SmartGym.Models;

namespace SmartGym.Services;

public interface IClassService
{
	Task<Class> CreateClass(ClassDTO newClassData);
	Task<bool> UpdateClassById(int id, ClassDTO newClassData);
	Task<bool> DeleteClass(int id);
	Task<List<ClassDTO>> GetAllClasses();
	Task<Class> GetClassById(int id);
}
