using Microsoft.AspNetCore.Mvc;
using SmartGym.Data;
using SmartGym.Models;

namespace SmartGym.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController: ControllerBase
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
    public async Task<ActionResult<Class>> GetClassesById(int id)
    {
        var classItem = await _context.Classes.FindAsync(id);
        if (classItem == null)
            return NotFound();
        
        return classItem;
    }
}