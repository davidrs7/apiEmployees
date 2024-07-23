using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO.Department
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string ColorHex { get; set; } = String.Empty;
        public bool ChangeColorHex { get; set; } = false;
        public int employeesCount { get; set; }
    }
}