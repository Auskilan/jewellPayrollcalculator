using Microsoft.AspNetCore.Mvc;
using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;

namespace payrollcalculator.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
            return Ok();
        }
    }
}
