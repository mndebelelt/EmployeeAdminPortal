using EmployeeAdminPortal.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;


namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EmployeesController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = applicationDbContext.Employees.ToList();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeByID(Guid id)
        {
            var employee = applicationDbContext.Employees.Find(id);

            if(employee == null)
            {
                return NotFound("Id was not found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Email = addEmployeeDto.Email,
                Name = addEmployeeDto.Name,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };
            applicationDbContext.Employees.Add(employeeEntity);
            applicationDbContext.SaveChanges();

            return Ok(employeeEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = applicationDbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;
            employee.Name   = updateEmployeeDto.Name;
            applicationDbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = applicationDbContext.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            applicationDbContext.Remove(employee);
            applicationDbContext.SaveChanges();

            return Ok("Employee deleted successfully");
        }
    }
}
