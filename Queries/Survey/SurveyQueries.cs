
namespace Api.Queries
{
    public class SurveyQueries
    {
        public string Surveys { get; } = "SELECT Id, Available, Name, Description FROM survey";
        public string SurveyById { get; } = "SELECT Id, Available, Name, Description FROM survey WHERE Id = @Id";
        public string Add { get; } = "INSERT INTO survey (Available, Name, Description) VALUES (@Available, @Name, @Description);SELECT LAST_INSERT_ID();";
        public string Edit { get; } = "UPDATE survey SET Available = @Available, Name = @Name, Description = @Description WHERE Id = @Id";
        public string HeadersBySurvey { get; } = "SELECT SH.Id, S.Id AS SurveyId, S.Available, S.Name, S.Description, SH.Started, SH.Finished, SH.Title FROM survey S INNER JOIN survey_header SH ON S.Id = SH.SurveyId WHERE S.Id = @SurveyId ORDER BY SH.Finished DESC";
        public string HeadersByUser { get; } = "SELECT DISTINCT SH.Id, S.Id AS SurveyId ,  SR.UserId, S.Available , S.Name, S.Description , SH.Started, SH.Finished, SH.Title,IF(SRE.id IS NULL, 0, 1) AS IsAnswered,  IF(SRE.id IS NULL, 0, 0) AS Draft FROM survey S INNER JOIN survey_header SH ON S.Id = SH.SurveyId INNER JOIN survey_user_login_rel SR ON SR.SurveyHeaderId =  SH.Id LEFT JOIN survey_responses SRE ON SRE.survey_id = SH.SurveyId and SRE.survey_header_id =  SH.Id  WHERE SH.Started <= NOW() AND SH.Finished >= NOW() AND SR.UserId = @UserId";
        public string HeaderBySurveyAndUser { get; } = "SELECT DISTINCT SH.Id, S.Id AS SurveyId, SULR.UserId, S.Available, S.Name, S.Description, SH.Started, SH.Finished, SH.Title, IF(SR.Id IS NULL, 0, 1) AS IsAnswered, IF(SR.Id IS NULL, 0, SR.Draft) AS Draft FROM survey S INNER JOIN survey_header SH ON S.Id = SH.SurveyId INNER JOIN survey_user_login_rel SULR ON (SH.Id = SULR.SurveyHeaderId AND SULR.Active = 1) LEFT JOIN survey_response SR ON SR.SurveyUserId = SULR.Id WHERE SH.Started <= NOW() AND SH.Finished >= NOW() AND SULR.UserId = @UserId AND S.Id = @SurveyId";
        public string AddHeader { get; } = "INSERT INTO survey_header (SurveyId, Started, Finished, Title) VALUES (@SurveyId, @Started, @Finished, @Title);SELECT LAST_INSERT_ID();";
        public string EditHeader { get; } = "UPDATE survey_header SET SurveyId = @SurveyId, Started = @Started, Finished = @Finished, Title = @Title WHERE Id = @Id";
        public string Fields { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM survey_field";
        public string FieldsBySurvey { get; } = "SELECT SF.Id, SF.Available, SF.Name, SF.FieldType, SF.IsRequired, SF.Config FROM survey_field SF INNER JOIN survey_field_rel SFR ON SF.Id = SFR.SurveyFieldId WHERE SFR.SurveyId = @SurveyId";
        public string FieldsByHeader { get; } = "";
        public string FieldById { get; } = "SELECT Id, Available, Name, FieldType, IsRequired, Config FROM survey_field WHERE Id = @Id";
        public string FieldAdd { get; } = "INSERT INTO survey_field (Available, Name, FieldType, IsRequired, Config) VALUES (@Available, @Name, @FieldType, @IsRequired, @Config)";
        public string FieldEdit { get; } = "UPDATE survey_field SET Available = @Available, Name = @Name, FieldType = @FieldType, IsRequired = @IsRequired, Config = @Config WHERE Id = @Id";
        public string FieldRelAdd { get; } = "INSERT INTO survey_field_rel (SurveyId, SurveyFieldId, Active, Weight) VALUES (@SurveyId, @SurveyFieldId, @Active, @Weight)";
        public string FieldRelEdit { get; } = "UPDATE survey_field_rel SET Active = @Active, Weight = @Weight WHERE SurveyId = @SurveyId AND SurveyFieldId = @SurveyFieldId";
        public string UsersByHeader { get; } = "SELECT U.UsuarioID Id, SH.SurveyId, SH.Id AS HeaderId, SR.Id AS ResponseId, U.FechaCreacion CreateAt, U.Estado Available, U.Nombre UserName, '' AS Password, U.UsuarioIdOpcional EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, J.Nombre AS JobName, D.Nombre AS DepartmentName FROM usuarios U INNER JOIN survey_user_login_rel SULR ON SULR.UserId = U.UsuarioID INNER JOIN survey_header SH ON SH.Id = SULR.SurveyHeaderId LEFT JOIN survey_responses SR ON SR.user_id = SULR.Id LEFT JOIN employee E ON E.Id = U.UsuarioIdOpcional LEFT JOIN cargos J ON J.CargoID = E.JobId LEFT JOIN roles D ON D.RolID = U.RolID WHERE SH.Id = @HeaderId";
        public string AddUserRel { get; } = "INSERT INTO survey_user_login_rel (SurveyHeaderId, UserId, Active) VALUES (@HeaderId, @UserId, 1)";
        public string AddUserRelByDepartment { get; } = "INSERT INTO survey_user_login_rel (SurveyHeaderId, UserId, Active) SELECT @HeaderId AS SurveyHeaderId, U.UsuarioID Id, 1 AS Active FROM usuarios U INNER JOIN employee E ON E.Id = U.UsuarioIdOpcional INNER JOIN  cargos J ON J.CargoID = E.JobId INNER JOIN roles D ON D.RolID = U.rolID LEFT JOIN survey_user_login_rel SULR ON SULR.UserId = U.UsuarioID WHERE SULR.SurveyHeaderId <> @HeaderId AND D.RolID = @ParamId";
        public string AddUserRelByJob { get; } = "INSERT INTO survey_user_login_rel (SurveyHeaderId, UserId, Active) SELECT @HeaderId AS SurveyHeaderId, U.UsuarioID Id, 1 AS Active FROM usuarios U INNER JOIN Employee E ON E.Id = U.UsuarioIdOpcional INNER JOIN  cargos J ON J.CargoID = E.JobId  LEFT JOIN survey_user_login_rel SULR ON SULR.UserId = U.UsuarioID WHERE SULR.SurveyHeaderId <> @HeaderId AND J.CargoID = @ParamId";
        public string AddUserRelByCity { get; } = "INSERT INTO survey_user_login_rel (SurveyHeaderId, UserId, Active) SELECT @HeaderId AS SurveyHeaderId, U.UsuarioID Id, 1 AS Active FROM usuarios U INNER JOIN Employee E ON E.Id = U.UsuarioIdOpcional INNER JOIN City C ON C.Id = E.JobCityId LEFT JOIN survey_user_login_rel SULR ON SULR.UserId = U.UsuarioID WHERE SULR.SurveyHeaderId <> @HeaderId AND C.Id = @ParamId";
        public string DeleteUserRel { get; } = "DELETE FROM survey_user_login_rel WHERE SurveyHeaderId = @HeaderId AND UserId = @UserId";
    }
}
