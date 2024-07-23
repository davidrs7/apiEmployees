namespace Api.DTO.Vacant
{
    public class VacantPostulateRelDTO
    {
        public int Id { get; set; }
        public int PostulateId { get; set; }
        public int VacantStatusId { get; set; }
        public string VacantStatusName { get; set; } = String.Empty;
        public int ContractTypeId { get; set; }
        public string ContractTypeName { get; set; } = String.Empty;
        public int JobId { get; set; }
        public string JobName { get; set; } = String.Empty;
        public string JobColorHex { get; set; } = String.Empty;
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int VacantNum { get; set; }
        public string Description { get; set; } = String.Empty;
        public int? VacantsCount { get; set; }
        public int Active { get; set; }
    }
}