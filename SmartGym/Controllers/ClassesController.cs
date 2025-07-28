using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGym.Data;
using SmartGym.Models;
using SmartGym.Services;

namespace SmartGym.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
	private readonly IClassService _service;

	public ClassesController(IClassService service)
	{
		_service = service;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Class>>> GetAllClasses()
	{
		var classList = await _service.GetAllClasses();
		return Ok(classList);
	}

	[HttpGet("{id:int}")]
	public async Task<ActionResult<Class>> GetClassById(int id)
	{
		var classItem = await _service.GetClassById(id);
		if (classItem == null)
			return NotFound();

		return classItem;
	}

	[HttpPost]
	public async Task<ActionResult<Class>> CreateClass([FromBody] ClassDTO newClassData)
	{
		var created = await _service.CreateClass(newClassData);
		return CreatedAtAction(nameof(GetClassById), new { id = created.Id }, created);
	}

	[HttpPatch("{id:int}")]
	public async Task<IActionResult> UpdateClassById(int id, ClassDTO classDto)
	{
		if (id != classDto.Id)
			return BadRequest();

		// var classEntity = await _service.GetClassById(id);
		// if (classEntity == null)
		// 	return NotFound();

		var patched = await _service.UpdateClassById(id, classDto);

		if (patched)
			return Ok(patched);
		else
			return StatusCode(500); //TODO: Return more detail why api call failed
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteClass(int id)
	{
		var removed = await _service.DeleteClass(id);
		if (!removed)
			return NotFound();
		return Ok();
	}
}