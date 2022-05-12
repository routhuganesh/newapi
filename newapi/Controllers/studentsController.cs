using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newapi.Data;
using newapi.Models;

namespace newapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentsController : ControllerBase
    {
        private readonly studentDbcontext _context;

        public studentsController(studentDbcontext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<student>>> Getstudent()
        {
          if (_context.student == null)
          {
              return NotFound();
          }
            return await _context.student.ToListAsync();
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<student>> Getstudent(int id)
        {
          if (_context.student == null)
          {
              return NotFound();
          }
            var student = await _context.student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstudent(int id, student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!studentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<student>> Poststudent(student student)
        {
          if (_context.student == null)
          {
              return Problem("Entity set 'studentDbcontext.student'  is null.");
          }
            _context.student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getstudent", new { id = student.Id }, student);
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestudent(int id)
        {
            if (_context.student == null)
            {
                return NotFound();
            }
            var student = await _context.student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool studentExists(int id)
        {
            return (_context.student?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
