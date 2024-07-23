namespace Api.DTO.Absence
{
    public class AbsenceApprovalDTO
    {
        public int Id { get; set; }
        public int AbsenceId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string UserEmployeeName { get; set; } = String.Empty;
        public string UserPhotoUrl { get; set; } = String.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Approval { get; set; }
        public string Description { get; set; } = String.Empty;
        public int IsHRApproval { get; set; }
    }
}