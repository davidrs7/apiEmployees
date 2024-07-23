using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO.Department;

namespace Api.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDTO>> Departments();
        Task<DepartmentDTO> DepartmentById(int departmentId);
        Task Add(DepartmentDTO departmentAdd);
        Task Edit(DepartmentDTO departmentEdit);
    }
}