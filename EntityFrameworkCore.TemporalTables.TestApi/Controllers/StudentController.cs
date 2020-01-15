using System;
using EntityFrameworkCore.TemporalTables.TestApi.Models;
using EntityFrameworkCore.TemporalTables.TestApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.TemporalTables.Extensions;

namespace EntityFrameworkCore.TemporalTables.TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly Context _context;

        public StudentController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        }

        [HttpGet("{id}/history")]
        public IQueryable<Student> GetStudentHistory(int id)
        {
            return  _context.Students.Between(DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Where(s => s.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        [HttpPut]
        public async Task<ActionResult<Student>> UpdateStudent([FromBody] Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
