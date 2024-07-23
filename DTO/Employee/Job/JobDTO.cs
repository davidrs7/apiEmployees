using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTO.Job
{
    public class JobDTO
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = String.Empty;
        public int ApproveId { get; set; }
        public int ReportId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Profile { get; set; } = String.Empty;
        public string Functions { get; set; } = String.Empty;
    }
}