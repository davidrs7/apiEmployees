using System;

namespace Api.DTO.JobGoal
{
    public class JobGoalDTO
    {
        public int Id { get; set; }
        public int JobGoalHeaderId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaderEmployeeId { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime LastUpdateAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Finish { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int Weigh { get; set; }
        public DateTime EvaluationDate { get; set; }
        public string Comments { get; set; } = String.Empty;
        public  int AdvancePerc { get; set; }
    }
}
