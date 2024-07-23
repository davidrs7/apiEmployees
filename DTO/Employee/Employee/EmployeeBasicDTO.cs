using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO.Employee
{
    public class EmployeeBasicDTO
    {
        public int Id { get; set; } = 0;
        public string DepartmentName { get; set; } = String.Empty;
        public string JobName { get; set; } = String.Empty;
        public string ColorHex { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string CellPhone { get; set; } = string.Empty;
        public string Email { get; set; } = String.Empty;
        public string PhotoUrl { get; set; } = String.Empty;
        public int Pages { get; set; }
    }
}