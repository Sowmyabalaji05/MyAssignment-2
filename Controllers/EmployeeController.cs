
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ofz1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ofz1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeController(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            return await _dbContext.Employees.ToListAsync();
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<Employees>> GetEmployeeById(int employeeId)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        [HttpPost]
      
        public async Task<ActionResult<Employees>> AddEmployee(Employees employees)
        {

            employees.LastUpdated = DateTime.UtcNow;
            _dbContext.Employees.Add(employees);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetEmployees), new { id = employees.EmployeeId }, employees);
        }

        [HttpPut("{employeeId}")]
        public async Task<ActionResult<Employees>> PutEmployee(int employeeId, [FromBody] Employees updatedEmployee)
        {
            // Log the received employeeId and updatedEmployee.EmployeeId for debugging
            Console.WriteLine($"Received employeeId: {employeeId}");
            Console.WriteLine($"Updated employeeId: {updatedEmployee.EmployeeId}");

            if (employeeId != updatedEmployee.EmployeeId)
            {
                // Log the mismatch for debugging
                Console.WriteLine("EmployeeId mismatch detected.");

                // Set the EmployeeId from the URL parameter
                updatedEmployee.EmployeeId = employeeId;
            }

            try
            {
                var employeeToUpdate = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
                if (employeeToUpdate == null)
                {
                    return NotFound();
                }

                // Update employee details
                employeeToUpdate.EmployeeName = updatedEmployee.EmployeeName;
                employeeToUpdate.Email = updatedEmployee.Email;
                employeeToUpdate.ManagerID = updatedEmployee.ManagerID;
                employeeToUpdate.ManagerName = updatedEmployee.ManagerName;
                employeeToUpdate.IsActive = updatedEmployee.IsActive;
                employeeToUpdate.LastUpdated = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                if (!EmployeeAvailable(employeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
        
        private bool EmployeeAvailable(int employeeId)
        {
            return (_dbContext.Employees?.Any(x => x.EmployeeId == employeeId)).GetValueOrDefault();
        }
        [HttpDelete("{employeeId}")]
        public async Task<ActionResult>DeleteEmployee(int employeeId)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            
            
            if(employee == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
       

    }
}

