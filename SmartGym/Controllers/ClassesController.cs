using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly SmartGymContext _context;

    public ClassesController(SmartGymContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Class>>> GetAllClasses()
    {
        return await _context.Classes.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Class>> GetClassById(int id)
    {
        var classItem = await _context.Classes.FindAsync(id);
        if (classItem == null)
            return NotFound();

        return classItem;
    }

    [HttpPost]
    public async Task<ActionResult<Class>> CreateClass(ClassDTO newClassData)
    {
        var newClass = new Class
        {
            Name = newClassData.Name,
            Schedule = newClassData.Schedule,
            Capacity = newClassData.Capacity,
            TrainerId = newClassData.TrainerId,
            // CategoryId = newClassData.CategoryId
        };

        _context.Classes.Add(newClass);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClassById), new { id = newClass.Id }, newClass);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateClassById(int id, ClassDTO patchData)
    {
        if (id != patchData.Id)
            return BadRequest();

        var classToPatch = await _context.Classes.FindAsync(id);
        if (classToPatch == null)
        {
            return NotFound();
        }

        classToPatch.Name = patchData.Name;
        classToPatch.Schedule = patchData.Schedule;
        classToPatch.Capacity = patchData.Capacity;
        classToPatch.TrainerId = patchData.TrainerId;
        // classToPatch.CategoryId = patchData.CategoryId;

        //are we using AutoMapper or something to deserialize?

        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        var classItem = await _context.Classes.FindAsync(id);
        if (classItem == null)
            return NotFound();

        _context.Classes.Remove(classItem);
        await _context.SaveChangesAsync();

        return Ok();
    }
}