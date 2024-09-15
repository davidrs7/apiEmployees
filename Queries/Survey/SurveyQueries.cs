
namespace Api.Queries
{
    public class SurveyQueries
    {
        public string Surveys { get; } = "SELECT Id, Available, Name, Description FROM Survey";
        public string SurveyById { get; } = "SELECT Id, Available, Name, Description FROM Survey WHERE Id = @Id";
        public string Add { get; } = "INSERT INTO Survey (Available, Name, Description) VALUES (@Available, @Name, @Description);SELECT LAST_INSERT_ID();";
        public string Edit { get; } = "UPDATE Survey SET Available = @Available, Name = @Name, Description = @Description WHERE Id = @Id";
        public string HeadersBySurvey { get; } = "SELECT SH.Id, S.Id AS SurveyId, S.Available, S.Name, S.Description, SH.Started, SH.Finished, SH.Title FROM Survey S INNER JOIN Survey_Header SH ON S.Id = SH.SurveyId WHERE S.Id = @SurveyId ORDER BY SH.Finished DESC";
        public string HeadersByUser { get; } = "SELECT DISTINCT SH.Id, S.Id AS SurveyId ,  SR.UserId, S.Available , S.Name, S.Description , SH.Started, SH.Finished, SH.Title,IF(SRE.id IS NULL, 0, 1) AS IsAnswered,  IF(SRE.id IS NULL, 0, 0) AS Draft FROM Survey S INNER JOIN Survey_Header SH ON S.Id = SH.SurveyId INNER JOIN survey_user_login_rel SR ON SR.SurveyHeaderId =  SH.Id LEFT JOIN Survey_Responses SRE ON SRE.survey_id = SH.SurveyId and SRE.survey_header_id =  SH.Id  WHERE SH.Started <= NOW() AND SH.Finished >= NOW() AND SR.UserId = @UserId";
        public string HeaderBySurveyAndUser { get; } = "SELECT DISTINCT SH.Id, S.Id AS SurveyId, SULR.UserId, S.Available, S.Name, S.Description, SH.Started, SH.Finished, SH.Title, IF(SR.Id IS NULL, 0, 1) AS IsAnswered, IF(SR.Id IS NULL, 0, SR.Draft) AS Draft FROM Survey S INNER JOIN Survey_Header SH ON S.Id = SH.SurveyId INNER JOIN Survey_User_Login_Rel SULR ON (SH.Id = SULR.SurveyHeaderId AND SULR.Active = 1) LEFT JOIN Survey_Response SR ON SR.SurveyUserId = SULR.Id WHERE SH.Started <= NOW() AND SH.Finished >= NOW() AND SULR.UserId = @UserId AND S.Id = @SurveyId";
        public string AddHeader { get; } = "INSERT INTO Survey_Header (SurveyId, Started, Finished, Title) VALUES (@SurveyId, @Started, @Finished, @Title);SELECT LAST_INSERT_ID();";
        public string EditHeader { get; } = "UPDATE Survey_Header SET SurveyId = @SurveyId, Started = @Started, Finished = @Finished, Title = @Title WHERE Id = @Id";
        public string Fields { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM Survey_Field";
        public string FieldsBySurvey { get; } = "SELECT SF.Id, SF.Available, SF.Name, SF.FieldType, SF.IsRequired, SF.Config FROM Survey_Field SF INNER JOIN Survey_Field_Rel SFR ON SF.Id = SFR.SurveyFieldId WHERE SFR.SurveyId = @SurveyId";
        public string FieldsByHeader { get; } = "";
        public string FieldById { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM Survey_Field WHERE Id = @Id";
        public string FieldAdd { get; } = "INSERT INTO Survey_Field (Available, Name, FieldType, IsRequired, Config) VALUES (@Available, @Name, @FieldType, @IsRequired, @Config)";
        public string FieldEdit { get; } = "UPDATE Survey_Field SET Available = @Available, Name = @Name, FieldType = @FieldType, IsRequired = @IsRequired, Config = @Config WHERE Id = @Id";
        public string FieldRelAdd { get; } = "INSERT INTO Survey_Field_Rel (SurveyId, SurveyFieldId, Active, Weight) VALUES (@SurveyId, @SurveyFieldId, @Active, @Weight)";
        public string FieldRelEdit { get; } = "UPDATE Survey_Field_Rel SET Active = @Active, Weight = @Weight WHERE SurveyId = @SurveyId AND SurveyFieldId = @SurveyFieldId";
        public string UsersByHeader { get; } = "SELECT U.Id, SH.SurveyId, SH.Id AS HeaderId, SR.Id AS ResponseId, U.CreateAt, U.Available, U.UserName, '' AS Password, U.EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, J.Name AS JobName, D.Name AS DepartmentName FROM User_Login U INNER JOIN Survey_User_Login_Rel SULR ON SULR.UserId = U.Id INNER JOIN Survey_Header SH ON SH.Id = SULR.SurveyHeaderId LEFT JOIN Survey_Response SR ON SR.SurveyUserId = SULR.Id LEFT JOIN Employee E ON E.Id = U.EmployeeId LEFT JOIN Job J ON J.Id = E.JobId LEFT JOIN Department D ON D.Id = J.DepartmentId WHERE SH.Id = @HeaderId";
        public string AddUserRel { get; } = "INSERT INTO Survey_User_Login_Rel (SurveyHeaderId, UserId, Active) VALUES (@HeaderId, @UserId, 1)";
        public string AddUserRelByDepartment { get; } = "INSERT INTO Survey_User_Login_Rel (SurveyHeaderId, UserId, Active) SELECT @HeaderId AS SurveyHeaderId, U.Id, 1 AS Active FROM User_Login U INNER JOIN Employee E ON E.Id = U.EmployeeId INNER JOIN Job J ON J.Id = E.JobId INNER JOIN Department D ON D.Id = J.DepartmentId LEFT JOIN Survey_User_Login_Rel SULR ON SULR.UserId = U.Id WHERE SULR.Id IS NULL AND D.Id = @ParamId";
        public string AddUserRelByJob { get; } = "INSERT INTO Survey_User_Login_Rel (SurveyHeaderId, UserId, Active) SELECT @HeaderId AS SurveyHeaderId, U.Id, 1 AS Active FROM User_Login U INNER JOIN Employee E ON E.Id = U.EmployeeId INNER JOIN Job J ON J.Id = E.JobId LEFT JOIN Survey_User_Login_Rel SULR ON SULR.UserId = U.Id WHERE SULR.Id IS NULL AND J.Id = @ParamId";
        public string AddUserRelByCity { get; } = "INSERT INTO Survey_User_Login_Rel (SurveyHeaderId, UserId, Active) SELECT @HeaderId AS SurveyHeaderId, U.Id, 1 AS Active FROM User_Login U INNER JOIN Employee E ON E.Id = U.EmployeeId INNER JOIN City C ON C.Id = E.JobCityId LEFT JOIN Survey_User_Login_Rel SULR ON SULR.UserId = U.Id WHERE SULR.Id IS NULL AND C.Id = @ParamId";
        public string DeleteUserRel { get; } = "DELETE FROM Survey_User_Login_Rel WHERE SurveyHeaderId = @HeaderId AND UserId = @UserId";
    }
}
