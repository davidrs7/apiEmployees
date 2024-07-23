
namespace Api.Queries
{
    public class VacantQueries
    {
        public string Vacants { get; } = "SELECT    V.Id,    V.VacantStatusId,    VS.Name AS VacantStatusName,    V.ContractTypeId,    CT.Name AS ContractTypeName,    V.JobId,    J.Name AS JobName,    '#076AE2' AS JobColorHex,    V.UserId,    V.Created,    V.Updated,    V.VacantNum,    V.Description,    IF(COUNT(PVR.Active) = 0, 0, SUM(CAST(PVR.Active AS UNSIGNED))) AS VacantsCount,    IF(COUNT(PVR.IsEmployee) = 0, 0, SUM(CAST(PVR.IsEmployee AS UNSIGNED))) AS EmployeesCount FROM    Vacant V INNER JOIN    Job J ON V.JobId = J.Id LEFT JOIN    Postulate_Vacant_Rel PVR ON (V.Id = PVR.VacantId AND PVR.Active = 1) LEFT JOIN    Vacant_Status VS ON V.VacantStatusId = VS.Id LEFT JOIN    Contract_Type CT ON V.ContractTypeId = CT.Id WHERE    VS.Available = 1    OR V.Updated >= DATE_SUB(CURRENT_TIMESTAMP, INTERVAL 2 DAY) GROUP BY    V.Id,    V.VacantStatusId,    VS.Name,    V.ContractTypeId,    CT.Name,    V.JobId,    J.Name,    V.UserId,    V.Created,    V.Updated,    V.VacantNum,    V.Description ORDER BY    V.Updated DESC;";
        public string VacantById { get; } = "SELECT      V.Id,      V.VacantStatusId,      VS.Name AS VacantStatusName,      V.ContractTypeId,      CT.Name AS ContractTypeName,      V.JobId,      J.Name AS JobName,      '#076AE2' AS JobColorHex,      V.UserId,      V.Created,      V.Updated,      V.VacantNum,      V.Description,      IF(COUNT(PVR.Active) = 0, 0, SUM(CAST(PVR.Active AS UNSIGNED))) AS VacantsCount,      IF(COUNT(PVR.IsEmployee) = 0, 0, SUM(CAST(PVR.IsEmployee AS UNSIGNED))) AS EmployeesCount  FROM      Vacant V  INNER JOIN      Job J ON V.JobId = J.Id  LEFT JOIN      Postulate_Vacant_Rel PVR ON (V.Id = PVR.VacantId AND PVR.Active = 1)  LEFT JOIN      Vacant_Status VS ON V.VacantStatusId = VS.Id  LEFT JOIN      Contract_Type CT ON V.ContractTypeId = CT.Id  WHERE      V.Id = @Id      AND (VS.Available = 1 OR V.Updated >= DATE_SUB(CURRENT_TIMESTAMP, INTERVAL 2 DAY))  GROUP BY      V.Id,      V.VacantStatusId,      VS.Name,      V.ContractTypeId,      CT.Name,      V.JobId,      J.Name,      V.UserId,      V.Created,      V.Updated,      V.VacantNum,      V.Description  ORDER BY      V.Updated DESC; ";
        public string Add { get; } = "INSERT INTO Vacant (VacantStatusId, ContractTypeId, JobId, UserId, Created, Updated, VacantNum, Description) VALUES (@VacantStatusId, @ContractTypeId, @JobId, @UserId, @Created, @Updated, @VacantNum, @Description); SELECT LAST_INSERT_ID();";
        public string Edit { get; } = "UPDATE Vacant SET VacantStatusId = @VacantStatusId, ContractTypeId = @ContractTypeId, JobId = @JobId, UserId = @UserId, Updated = @Updated, VacantNum = @VacantNum, Description = @Description WHERE Id = @Id";
        public string VacantSteps { get; } = "SELECT S.Id, VSR.VacantId, S.Available, S.Name, S.Description, VSR.Active, VSR.Weight, VSR.IsRequired FROM Step S INNER JOIN Vacant_Step_Rel VSR ON S.Id = VSR.StepId WHERE VSR.VacantId = @VacantId ORDER BY VSR.Weight";
        public string VacantStepsByPostulateRel { get; } = "SELECT S.Id, VSR.VacantId, S.Available, S.Name, S.Description, VSR.Active, VSR.Weight, VSR.IsRequired, PVSR.Approved, PVSR.Reason FROM Step S INNER JOIN Vacant_Step_Rel VSR ON S.Id = VSR.StepId LEFT JOIN Postulate_Vacant_Rel PVR ON (VSR.VacantId = PVR.VacantId AND PVR.PostulateId = @PostulateId) LEFT JOIN Postulate_Vacant_Step_Rel PVSR ON (PVR.Id = PVSR.PostulateVacantRelId AND S.Id = PVSR.StepId) WHERE VSR.VacantId = @VacantId AND VSR.Active = 1 ORDER BY VSR.Weight";
        public string VacantsByPostulate { get; } = "SELECT V.Id, PVR.PostulateId, V.VacantStatusId, VS.Name AS VacantStatusName, V.ContractTypeId, CT.Name AS ContractTypeName, V.JobId, J.Name AS JobName, '#076AE2' AS JobColorHex, V.UserId, V.Created, V.Updated, V.VacantNum, V.Description, PVR.Active FROM Vacant V INNER JOIN Job J ON V.JobId = J.Id LEFT JOIN Postulate_Vacant_Rel PVR ON V.Id = PVR.VacantId LEFT JOIN Vacant_Status VS ON V.VacantStatusId = VS.Id LEFT JOIN Contract_Type CT ON V.ContractTypeId = CT.Id WHERE PVR.PostulateId = @PostulateId ORDER BY V.Created DESC";
        public string VacantsByVacantRel { get; } = "SELECT V.Id, PVR.PostulateId, V.VacantStatusId, VS.Name AS VacantStatusName, V.ContractTypeId, CT.Name AS ContractTypeName, V.JobId, J.Name AS JobName, '#076AE2' AS JobColorHex, V.UserId, V.Created, V.Updated, V.VacantNum, V.Description, PVR.Active FROM Vacant V INNER JOIN Job J ON V.JobId = J.Id LEFT JOIN Postulate_Vacant_Rel PVR ON V.Id = PVR.VacantId LEFT JOIN Vacant_Status VS ON V.VacantStatusId = VS.Id LEFT JOIN Contract_Type CT ON V.ContractTypeId = CT.Id WHERE PVR.VacantId = @VacantId ORDER BY V.Created DESC";
        public string StepRelAdd { get; } = "INSERT INTO Vacant_Step_Rel (StepId, VacantId, Active, Weight, IsRequired) VALUES (@StepId, @VacantId, @Active, @Weight, @IsRequired)";
        public string StepRelEdit { get; } = "UPDATE Vacant_Step_Rel SET Active = @Active, Weight = @Weight, IsRequired = @IsRequired WHERE StepId = @StepId AND VacantId = @VacantId";
        public string StepPostRelAdd { get; } = "INSERT INTO Postulate_Vacant_Step_Rel (StepId, PostulateVacantRelId, Created, Updated, Approved, Reason) VALUES (@StepId, @PostulateVacantRelId, @Created, @Updated, @Approved, @Reason)";
        public string StepPostRelEdit { get; } = "UPDATE Postulate_Vacant_Step_Rel SET Updated = @Updated, Approved = @Approved, Reason = @Reason WHERE StepId = @StepId AND PostulateVacantRelId = @PostulateVacantRelId";
        public string StepPostRelFindId { get; } = "SELECT Id FROM Postulate_Vacant_Rel WHERE VacantId = @VacantId AND PostulateId = @PostulateId";
        public string AddVacantPostulateRel { get; } = "INSERT INTO Postulate_Vacant_Rel (VacantId, PostulateId, Active, IsEmployee) VALUES (@VacantId, @PostulateId, @Active, 0)";
        public string HistVacants { get; } = "SELECT      V.Id,      V.VacantStatusId,      VS.Name AS VacantStatusName,      V.ContractTypeId,      CT.Name AS ContractTypeName,      V.JobId,      J.Name AS JobName,      '#076AE2' AS JobColorHex,      V.UserId,      V.Created,      V.Updated,      V.VacantNum,      V.Description,      IF(COUNT(PVR.Active) = 0, 0, SUM(CAST(PVR.Active AS UNSIGNED))) AS VacantsCount,      IF(COUNT(PVR.IsEmployee) = 0, 0, SUM(CAST(PVR.IsEmployee AS UNSIGNED))) AS EmployeesCount  FROM      Vacant V  INNER JOIN      Job J ON V.JobId = J.Id  LEFT JOIN      Postulate_Vacant_Rel PVR ON (V.Id = PVR.VacantId AND PVR.Active = 1)  LEFT JOIN      Vacant_Status VS ON V.VacantStatusId = VS.Id  LEFT JOIN      Contract_Type CT ON V.ContractTypeId = CT.Id  WHERE      VS.Available = 0      AND V.Updated < DATE_SUB(CURRENT_TIMESTAMP, INTERVAL 2 DAY)  GROUP BY      V.Id,      V.VacantStatusId,      VS.Name,      V.ContractTypeId,      CT.Name,      V.JobId,      J.Name,      V.UserId,      V.Created,      V.Updated,      V.VacantNum,      V.Description  ORDER BY      V.Updated DESC; ";
        public string HistVacantById { get; } = "SELECT      V.Id,      V.VacantStatusId,      VS.Name AS VacantStatusName,      V.ContractTypeId,      CT.Name AS ContractTypeName,      V.JobId,      J.Name AS JobName,      '#076AE2' AS JobColorHex,      V.UserId,      V.Created,      V.Updated,      V.VacantNum,      V.Description,      IF(COUNT(PVR.Active) = 0, 0, SUM(CAST(PVR.Active AS UNSIGNED))) AS VacantsCount,      IF(COUNT(PVR.IsEmployee) = 0, 0, SUM(CAST(PVR.IsEmployee AS UNSIGNED))) AS EmployeesCount  FROM      Vacant V  INNER JOIN      Job J ON V.JobId = J.Id  LEFT JOIN      Postulate_Vacant_Rel PVR ON (V.Id = PVR.VacantId AND PVR.Active = 1)  LEFT JOIN      Vacant_Status VS ON V.VacantStatusId = VS.Id  LEFT JOIN      Contract_Type CT ON V.ContractTypeId = CT.Id  WHERE      V.Id = @Id      AND VS.Available = 0      AND V.Updated < DATE_SUB(CURRENT_TIMESTAMP, INTERVAL 2 DAY)  GROUP BY      V.Id,      V.VacantStatusId,      VS.Name,      V.ContractTypeId,      CT.Name,      V.JobId,      J.Name,      V.UserId,      V.Created,      V.Updated,      V.VacantNum,      V.Description  ORDER BY      V.Updated DESC; ";
    }
}