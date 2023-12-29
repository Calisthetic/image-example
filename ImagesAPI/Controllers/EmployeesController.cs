using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImagesAPI.Models.Db;
using ImagesAPI.Models.DTOs;

namespace ImagesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly _24wsImagesContext _context;

        public EmployeesController(_24wsImagesContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeesResponse>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var result = await _context.Employees.Include(x => x.Role).ToListAsync();
            return result.ConvertAll(x => new EmployeesResponse(x));
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeesResponse employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var current = await _context.Employees.FindAsync(employee.Id);
            if (current == null)
            {
                return BadRequest();
            }

            current.FirstName = employee.FirstName;
            current.SecondName = employee.SecondName;
            current.ThirdName = employee.ThirdName;
            if (employee.ImageFile != null)
            {
                current.ImageFile = Convert.FromBase64String(employee.ImageFile);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeesResponse employee)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set '_24wsImagesContext.Employees'  is null.");
            }
            _context.Employees.Add(new Employee()
            {
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                ThirdName = employee.ThirdName,
                ImageFile = employee.ImageFile is null ? null : Convert.FromBase64String(employee.ImageFile),
                RoleId = 1,
                Login = employee.Login,
                Password = employee.Password,
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
