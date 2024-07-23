namespace Api.DTO.Employee
{
    public class EmployeeGeneralDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; } = String.Empty;
        public int? HousingTypeId { get; set; }
        public string? HousingTypeName { get; set; } = String.Empty;
        public int? TransportationId { get; set; }
        public string? TransportationName { get; set; } = String.Empty;
        public string? EmergencyContactName { get; set; } = String.Empty;
        public string? EmergencyContactPhone { get; set; } = String.Empty;
        public string? EmergencyContactRelationship { get; set; } = String.Empty;
        public int? Dependents { get; set; }
        public int? DependentsUnder9 { get; set; }
        public DateTime? DependentBirthDate { get; set; }
        public string? Address { get; set; } = String.Empty;
        public string? Neighborhood { get; set; } = String.Empty;
        public int? HousingTime { get; set; }
        public int? SocioeconomicStatus { get; set; }
        public string? Name { get; set; } = String.Empty;
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
        public int? Sons { get; set;}
    }
}