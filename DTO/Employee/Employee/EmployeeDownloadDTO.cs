namespace Api.DTO.Employee
{
    public class EmployeeDownloadDTO
    {
        public int Id { get; set; }
        public string? JobName { get; set; } = String.Empty;
        public string? StatusName { get; set; } = String.Empty;
        public string? MaritalStatusName { get; set; } = String.Empty;
        public string? DocTypeName { get; set; } = String.Empty;
        public string? DocIssueCityName { get; set; } = String.Empty;
        public string? ContractTypeName { get; set; } = String.Empty;
        public string? BankingEntityName { get; set; } = String.Empty;
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
        public string? CityName { get; set; } = String.Empty;
        public string? HousingTypeName { get; set; } = String.Empty;
        public string? TransportationName { get; set; } = String.Empty;
        public string? EmergencyContactName { get; set; } = String.Empty;
        public string? EmergencyContactPhone { get; set; } = String.Empty;
        public string? EmergencyContactRelationship { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string? Neighborhood { get; set; } = String.Empty;
        public int? HousingTime { get; set; }
        public int? SocioeconomicStatus { get; set; }
        public string? LicensePlate { get; set; } = String.Empty;
        public string? VehicleMark { get; set; } = String.Empty;
        public string? VehicleModel { get; set; } = String.Empty;
        public string? LicenseNumber { get; set; } = String.Empty;
        public string? LicenseCategory { get; set; } = String.Empty;
        public DateTime? LicenseValidity { get; set; }
        public DateTime? SOATExpirationDate { get; set; }
        public DateTime? RTMExpirationDate { get; set; }
        public string? VehicleOwnerName { get; set; } = String.Empty;
        public string? ContributorType { get; set; } = String.Empty;
        public string? Eps { get; set; } = String.Empty;
        public string? Arl { get; set; } = String.Empty;
        public string? Afp { get; set; } = String.Empty;
        public string? RecommendedBy { get; set; } = String.Empty;
        public string? Description { get; set; } = String.Empty;
        public string? EducationalLevelName { get; set; } = String.Empty;
        public string? Career { get; set; } = String.Empty;
    }
}