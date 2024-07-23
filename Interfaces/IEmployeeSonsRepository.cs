using Api.DTO.Employee;
using System;

namespace Api.Interfaces;
public interface IEmployeeSonsRepository
{
    Task<IEnumerable<EmployeeSonsDTO>> Sons(int employeeId);
}
