namespace Api.DTO.Employee
{
    public class EmployeeMergeDTO
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public string? JobName { get; set; } = String.Empty;
        public string? JobProfile { get; set; } = String.Empty;
        public int? GeneralId { get; set; }
        public int? AcademicId { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; } = String.Empty;
        public int? MaritalStatusId { get; set; }
        public string? MaritalStatusName { get; set; } = String.Empty;
        public string? ColorHex { get; set; } = String.Empty;
        public int? DocTypeId { get; set; }
        public string? DocTypeName { get; set; } = String.Empty;
        public int? DocIssueCityId { get; set; }
        public string? DocIssueCityName { get; set; } = String.Empty;
        public int? ContractTypeId { get; set; }
        public string? ContractTypeName { get; set; } = String.Empty;
        public int? BankingEntityId { get; set; }
        public string? BankingEntityName { get; set; } = String.Empty;
        public int? JobCityId { get; set; }
        public string? JobCityName { get; set; } = String.Empty;
        public string? Doc { get; set; } = String.Empty;
        public DateTime? DocIssueDate { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? Sex { get; set; } = String.Empty;
        public DateTime? BirthDate { get; set; }
        public string? Rh { get; set; } = String.Empty;
        public string? CorpCellPhone { get; set; } = String.Empty;
        public string? CellPhone { get; set; } = String.Empty;
        public string? Phone { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public DateTime? EmploymentDate { get; set; }
        public string? BankAccount { get; set; } = String.Empty;
        public string? BankAccountType { get; set; } = String.Empty;
        public bool? HasVaccine { get; set; }
        public string? VaccineMaker { get; set; } = String.Empty;
        public int? VaccineDose { get; set; }
        public bool? HasVaccineBooster { get; set; }
        public string? PhotoUrl { get; set; } = String.Empty;
        public IFormFile? Photo { get; set; }
    }
}