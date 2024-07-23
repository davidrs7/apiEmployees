namespace Api.Queries
{
    public class JobQueries
    {
        public string Jobs { get; } = "SELECT J.Id, J.Name, COUNT(E.Id) AS Employees FROM Job J LEFT JOIN Employee E ON J.Id = E.JobId GROUP BY J.Id, J.Name";

        public string JobsIntegracion { get; } = "SELECT J.CargoID as Id, J.Nombre as Name, COUNT(E.Id) AS Employees FROM Cargos J LEFT JOIN Employee E ON J.CargoID = E.JobId GROUP BY J.CargoID, J.Nombre;";
        public string Job { get; } = "SELECT J.Id, J.DepartmentId, J.ApproveId, J.ReportId, J.Name, J.Profile, J.Functions FROM Job J WHERE J.Id = @Id";
    }
}