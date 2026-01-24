using PayrollCalculator.Application.Interfaces.Repositories;
using PayrollCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollCalculator.Application.UseCases.Employee
{
    public class CreateEmployeeUseCase
    {
        private readonly IEmployeeRepository _repository;

        public CreateEmployeeUseCase(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(PayrollCalculator.Domain.Entities.Employee employee)
        {
            await _repository.AddAsync(employee);
        }
    }
}
