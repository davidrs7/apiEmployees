using System;

namespace Api.DTO.Job
{
    public class JobGoalHeaderDTO
    {
        public int Id { get; set; }
        public int Available { get; set; }
        public string Name { get; set; } = String.Empty;
    }
}
