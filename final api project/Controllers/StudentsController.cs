using final_api_project.Authorization;
using final_api_project.DBcontext;
using final_api_project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace final_api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;
        public StudentsController(UniversityContext context)
        {
            _context = context;
        }
        [HttpGet]
        [CheckPermission(Permission.ReadStudents)]
        public async Task<ActionResult<IEnumerable<Student>>> GetEmployees()
        {
            return await _context.Students.ToListAsync();
        }
        [HttpGet("{id}")]
        [CheckPermission(Permission.ReadStudents)]

        public async Task<ActionResult<Student>> GetEmployeebyid(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> PostEmployee(Student student)
        {
            
            await   _context.Students.AddAsync(student);
            student.Id = 0;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmployees", new  { name = student.Name , dateofbrith = student.Dateofbrith , email = student.Email , phone = student.Phone}, student);
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}