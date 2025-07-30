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

	//given a partial Class object (post DTO) received from body, create a class
	//class creation requires a trainer to be assigned
	//data from body will be mapped to a new Class entity before being stored in the database
	public async Task<ClassDTO> CreateClass(ClassPostDTO newClassData)
	{
		try
		{
			var trainer = await _unitOfWork.UserRepository.GetAsync(newClassData.TrainerId);
			if (trainer == null)
				throw new Exception("Trainer not found");
			Class newClass = _mapper.Map<Class>(newClassData);

			await _unitOfWork.ClassRepository.AddAsync(newClass);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<ClassDTO>(newClass);

		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in CreateClass: {ex.Message}");
			throw;
		}
	}
	//given an specific id (from url), and request body (patch dto), a class can be partially updated (patch, not put) 
	//new class data is mapped to the existing class entity before db storage
	public async Task<ClassDTO?> UpdateClassById(int id, ClassPatchDTO newClassData)
	{
		try
		{
			var classEntity = await _unitOfWork.ClassRepository.GetAsync(id);
			if (classEntity == null)
				return null;

			_mapper.Map(newClassData, classEntity);

			_unitOfWork.ClassRepository.Update(classEntity);
			await _unitOfWork.SaveAsync();
			return _mapper.Map<ClassDTO>(classEntity);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while updating class: {ex.Message}");
			return null;
		}
	}
	//given the ID from the url, the method finds the class entity and deletes it
	public async Task<bool> DeleteClass(int id)
	{
		try
		{
			var classEntity = await _unitOfWork.ClassRepository.GetAsync(id);
			if (classEntity == null)
				return false;

			_unitOfWork.ClassRepository.Delete(classEntity);
			await _unitOfWork.SaveAsync();
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error while deleting class: {ex.Message}");
			return false;
		}
	}

	//gets a list of all of the created classes
	public async Task<List<ClassDTO>> GetAllClasses()
	{
		try
		{
			var classes = await _unitOfWork.ClassRepository.GetAsync();
			var classList = _mapper.Map<List<ClassDTO>>(classes);
			return classList.ToList();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetAllClasses: {ex.Message}");
			return new List<ClassDTO>();
		}
	}

	//given and ID from the URL, the method returns a specific Class; before displaying to the user, the entity is mapped to a DTO to be displayed to the user, protecting db layer (see comments below)
	public async Task<ClassDTO> GetClassById(int id)
	{
		try
		{
			var classEntity = await _unitOfWork.ClassRepository.GetAsync(id);
			return _mapper.Map<ClassDTO>(classEntity);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error in GetClassById: {ex.Message}");
			return null;
		}
	}
	// NOTE: This method returns a DTO intended for read/display purposes only.
	// Do NOT use this to retrieve entities for update, patch, or delete operations.
	// For internal logic, use _unitOfWork.ClassRepository.GetAsync(id) to get the actual tracked entity.

}
