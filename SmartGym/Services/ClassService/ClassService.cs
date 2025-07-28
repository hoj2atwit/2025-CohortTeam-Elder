using System;
using Microsoft.EntityFrameworkCore;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class ClassService : IClassService
{

	private readonly IUnitOfWork _unitOfWork;
	public ClassService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	
	public async Task<Class> CreateClass(ClassDTO newClassData)
	{
		var trainer = await _unitOfWork.UserRepository.GetAsync(newClassData.TrainerId);
		if (trainer == null)
			throw new Exception("Trainer not found");

		var entity = new Class
		{
			Name = newClassData.Name,
			Schedule = newClassData.Schedule,
			Capacity = newClassData.Capacity,
			TrainerId = newClassData.TrainerId,
			CategoryId = newClassData.CategoryId
		};

		await _unitOfWork.ClassRepository.AddAsync(entity);
		await _unitOfWork.SaveAsync();
		return entity;
	}
	public async Task<bool> UpdateClassById(int id, ClassDTO newClassData)
	{
		var classEntity = await GetClassById(id);

		if (classEntity == null)
			return false;

		classEntity.Name = newClassData.Name;
		classEntity.Schedule = newClassData.Schedule;
		classEntity.Capacity = newClassData.Capacity;
		classEntity.TrainerId = newClassData.TrainerId;
		classEntity.CategoryId = newClassData.CategoryId;

		_unitOfWork.ClassRepository.Update(classEntity);
		await _unitOfWork.SaveAsync();
		return true;

	}
	public async Task<bool> DeleteClass(int id)
	{
		var classEntity = await GetClassById(id);
		if (classEntity == null)
			return false;

		_unitOfWork.ClassRepository.Delete(classEntity);
		await _unitOfWork.SaveAsync();
		return true;
	}

	//TODO: Make it a true async
	public async Task<List<ClassDTO>> GetAllClasses()
	{
		List<ClassDTO> classList = new();
		var classes = await _unitOfWork.ClassRepository.GetAsync();
		foreach (var item in classes)
		{
			var classDto = new ClassDTO();
			classDto.Id = item.Id;
			classDto.Name = item.Name;
			classDto.Schedule = item.Schedule;
			classDto.Capacity = item.Capacity;
			classDto.TrainerId = item.TrainerId;
			classDto.CategoryId = item.CategoryId;
			classList.Add(classDto);
		}
		return classList.ToList();
	}

	public async Task<Class> GetClassById(int id)
	{
		return await _unitOfWork.ClassRepository.GetAsync(id);
	}
	
}
