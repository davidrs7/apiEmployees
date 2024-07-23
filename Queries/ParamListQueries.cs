
namespace Api.Queries
{
    public class ParamListQueries
    {
        public string DocType { get; } = "SELECT DT.Id, DT.Available, DT.Name FROM Doc_Type DT";
        public string City { get; } = "SELECT CT.Id, CT.Available, CT.Name FROM City CT";
        public string MaritalStatus { get; } = "SELECT MS.Id, MS.Available, MS.Name FROM Marital_Status MS";
        public string HousingType { get; } = "SELECT HT.Id, HT.Available, HT.Name FROM Housing_Type HT";
        public string EducationalLevel { get; } = "SELECT EL.Id, EL.Available, EL.Name FROM Educational_Level EL";
        public string EmployeeStatus { get; } = "SELECT ES.Id, ES.Available, ES.Name FROM Employee_Status ES";
        public string ContractType { get; } = "SELECT CT.Id, CT.Available, CT.Name FROM Contract_Type CT";
        public string BankingEntity { get; } = "SELECT BE.Id, BE.Available, BE.Name FROM Banking_Entity BE";
        public string Transportation { get; } = "SELECT TP.Id, TP.Available, TP.Name FROM Transportation TP";
        public string VacantStatus { get; } = "SELECT VS.Id, VS.Available, VS.Name FROM Vacant_Status VS";
        public string PostulateFindOut { get; } = "SELECT PFO.Id, PFO.Available, PFO.Name FROM Postulate_Findout PFO";
        public string JobSkills { get; } = "SELECT JS.Id, JS.Available, JS.Name FROM Job_Skills JS";
        public string Department { get; } = "SELECT D.Id, 1 AS Available, D.Name FROM Department D";
        public string Job { get; } = "SELECT J.Id, 1 AS Available, J.Name FROM Job J";
        public string AbsenceType { get; } = "SELECT AT.Id, AT.Available, AT.Name FROM Absence_Type AT";
    }
}