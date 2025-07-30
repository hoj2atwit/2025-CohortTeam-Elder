using System;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Services;

public class ClassService : IClassService
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ClassDTO> CreateClass(ClassPostDTO newClassData)
	{
		var trainer = await _unitOfWork.UserRepository.GetAsync(newClassData.TrainerId);
		if (trainer == null)
			throw new Exception("Trainer not found");
		Class newClass = _mapper.Map<Class>(newClassData);

		await _unitOfWork.ClassRepository.AddAsync(newClass);
		await _unitOfWork.SaveAsync();
		return _mapper.Map<ClassDTO>(newClass);
	}
	public async Task<ClassDTO?> UpdateClassById(int id, ClassPatchDTO newClassData)
	{
		var classEntity = await _unitOfWork.ClassRepository.GetAsync(id);
		if (classEntity == null)
			return null;

		_mapper.Map(newClassData, classEntity);

		_unitOfWork.ClassRepository.Update(classEntity);
		await _unitOfWork.SaveAsync();
		return _mapper.Map<ClassDTO>(classEntity);
	}
	public async Task<bool> DeleteClass(int id)
	{
		var classEntity = await _unitOfWork.ClassRepository.GetAsync(id);
		if (classEntity == null)
			return false;

		_unitOfWork.ClassRepository.Delete(classEntity);
		await _unitOfWork.SaveAsync();
		return true;
	}

	//TODO: Make it a true async
	public async Task<List<ClassDTO>> GetAllClasses()
	{
		var classes = await _unitOfWork.ClassRepository.GetAsync();
		var classList = _mapper.Map<List<ClassDTO>>(classes);
		return classList.ToList();
	}

	public async Task<ClassDTO> GetClassById(int id)
	{
		var classEntity = await _unitOfWork.ClassRepository.GetAsync(id);
		return _mapper.Map<ClassDTO>(classEntity);
	}
	// NOTE: This method returns a DTO intended for read/display purposes only.
	// Do NOT use this to retrieve entities for update, patch, or delete operations.
	// For internal logic, use _unitOfWork.ClassRepository.GetAsync(id) to get the actual tracked entity.

}
