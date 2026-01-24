using Microsoft.AspNetCore.Mvc;
using PayrollCalculator.Application.UseCases.Employee;
using PayrollCalculator.Domain.Entities;

namespace payrollcalculator.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly CreateEmployeeUseCase _useCase;

        public EmployeeController(CreateEmployeeUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _useCase.Execute(employee);
            return Ok();
        }
    }
}
