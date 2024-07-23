
namespace Api.Queries
{
    public class PostulateQueries
    {
        public string Postulates { get; } = "SELECT P.Id, EL.Name AS EducationalLevelName, P.ExpectedSalary, P.FirstName, P.LastName, P.CellPhone, P.Email, '#076AE2' AS ColorHex, P.Career, P.Description, IF(P.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', P.PhotoUrl)) AS PhotoUrl, 0 AS Pages FROM Postulate P LEFT JOIN Educational_Level EL ON P.EducationalLevelId = EL.Id WHERE P.EmployeeId IS NULL";
        public string PostulateById { get; } = "SELECT P.Id, P.RecruiterUserId, P.FindOutId, PFO.Name AS FindOutName, P.DocTypeId, DT.Name AS DocTypeName, P.EducationalLevelId, EL.Name AS EducationalLevelName, P.OfferedSalary, P.ExpectedSalary, P.Doc, P.FirstName, P.LastName, P.Sex, P.BirthDate, P.Rh, P.Phone, P.CellPhone, P.Email, P.Career, P.Description, '#076AE2' AS ColorHex, IF(P.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', P.PhotoUrl)) AS PhotoUrl FROM Postulate P LEFT JOIN Postulate_Findout PFO ON P.FindOutId = PFO.Id LEFT JOIN Doc_Type DT ON P.DocTypeId = DT.Id LEFT JOIN Educational_Level EL ON P.EducationalLevelId = EL.Id WHERE P.EmployeeId IS NULL AND P.Id = @Id";
        public string UpdatePhotoUrl { get; } = "UPDATE Postulate SET PhotoUrl = @PhotoUrl WHERE Id = @Id";
        public string Add { get; } = "INSERT INTO Postulate (RecruiterUserId, FindOutId, DocTypeId, EducationalLevelId, OfferedSalary, ExpectedSalary, Doc, FirstName, LastName, Sex, BirthDate, Rh, Cellphone, Phone, Email, Career, Description) VALUES (@RecruiterUserId, @FindOutId, @DocTypeId, @EducationalLevelId, @OfferedSalary, @ExpectedSalary, @Doc, @FirstName, @LastName, @Sex, @BirthDate, @Rh, @Cellphone, @Phone, @Email, @Career, @Description); SELECT LAST_INSERT_ID();";
        public string Edit { get; } = "UPDATE Postulate SET RecruiterUserId = @RecruiterUserId, FindOutId = @FindOutId, DocTypeId = @DocTypeId, EducationalLevelId = @EducationalLevelId, OfferedSalary = @OfferedSalary, ExpectedSalary = @ExpectedSalary, Doc = @Doc, FirstName = @FirstName, LastName = @LastName, Sex = @Sex, BirthDate = @BirthDate, Rh = @Rh, Cellphone = @Cellphone, Phone = @Phone, Email = @Email, Career = @Career, Description = @Description WHERE Id = @Id";
        public string UpdateToEmployee { get; } = "UPDATE Postulate SET EmployeeId = @EmployeeId WHERE Id = @Id";
        public string UpdateToEmployeeRel { get; } = "UPDATE Postulate_Vacant_Rel SET IsEmployee = 1 WHERE PostulateId = @Id AND VacantId = @VacantId";
    }
}