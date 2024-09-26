
namespace Api.Queries
{
    public class ParamListQueries
    {
        public string DocType { get; } = "SELECT DT.Id, DT.Available, DT.Name FROM doc_type DT";
        public string City { get; } = "SELECT CT.Id, CT.Available, CT.Name FROM city CT";
        public string MaritalStatus { get; } = "SELECT MS.Id, MS.Available, MS.Name FROM marital_status MS";
        public string HousingType { get; } = "SELECT HT.Id, HT.Available, HT.Name FROM housing_type HT";
        public string EducationalLevel { get; } = "SELECT EL.Id, EL.Available, EL.Name FROM educational_level EL";
        public string EmployeeStatus { get; } = "SELECT ES.Id, ES.Available, ES.Name FROM employee_status ES";
        public string ContractType { get; } = "SELECT CT.Id, CT.Available, CT.Name FROM contract_type CT";
        public string BankingEntity { get; } = "SELECT BE.Id, BE.Available, BE.Name FROM banking_entity BE";
        public string Transportation { get; } = "SELECT TP.Id, TP.Available, TP.Name FROM transportation TP";
        public string VacantStatus { get; } = "SELECT VS.Id, VS.Available, VS.Name FROM vacant_status VS";
        public string PostulateFindOut { get; } = "SELECT PFO.Id, PFO.Available, PFO.Name FROM postulate_findout PFO";
        public string JobSkills { get; } = "SELECT JS.Id, JS.Available, JS.Name FROM job_Skills JS";
        public string Department { get; } = "SELECT D.RolID Id, 1 AS Available, D.Nombre Name FROM roles D";
        public string Job { get; } = "SELECT J.CargoID Id, 1 AS Available, J.Nombre Name FROM cargos J";
        public string AbsenceType { get; } = "SELECT AT.Id, AT.Available, AT.Name FROM absence_type AT";
    }
}