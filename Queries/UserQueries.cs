namespace Api.Queries
{
    public class UserQueries
    {
        public string UserLogin { get; } = "SELECT U.Id, U.CreateAt, U.Available, U.UserName, U.Password FROM user_login U WHERE U.UserName = @UserName AND U.Password = @Password";
        public string UserId { get; } = "SELECT U.Id FROM user_login U WHERE U.UserName = @UserName";
        public string UserById { get; } = "SELECT U.Id, U.CreateAt, U.Available, U.UserName, '' AS Password, U.EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, J.Id AS JobId, J.Name AS JobName, D.Id AS DepartmentId, D.Name AS DepartmentName FROM user_login U LEFT JOIN employee E ON E.Id = U.EmployeeId LEFT JOIN job J ON J.Id = E.JobId LEFT JOIN department D ON D.Id = J.DepartmentId WHERE U.Id = @Id";

        public string userByIdOpcional { get; } = "SELECT U.UsuarioID as Id, U.FechaCreacion as CreateAt, U.Estado as Available, U.Nombre as UserName, '' AS Password, U.usuarioIdOpcional as EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl,J.CargoID AS JobId, J.Nombre AS JobName, D.RolID AS DepartmentId, D.Nombre AS DepartmentName FROM usuarios U LEFT JOIN employee E ON E.Id = U.usuarioIdOpcional LEFT JOIN cargos J ON J.CargoID = E.JobId LEFT JOIN roles D ON D.RolID = U.RolID WHERE U.usuarioIdOpcional = @Id";

        public string Users { get; } = "SELECT U.UsuarioID Id, U.FechaCreacion CreateAt, U.Estado Available, U.Nombre UserName, '' AS Password, U.UsuarioIdOpcional EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, J.CargoID AS JobId , J.Nombre AS JobName, D.RolID AS DepartmentId, D.Nombre AS DepartmentName     FROM usuarios U LEFT JOIN employee E ON E.Id = U.UsuarioIdOpcional  LEFT JOIN cargos J ON J.CargoID = E.JobId  LEFT JOIN roles D ON D.rolID = U.rolID";
        public string SessionByToken { get; } = "SELECT S.Token, S.UserId, S.Created, S.Reload, S.IpAddress FROM session S WHERE S.Token = @Token";
        public string AddSession { get; } = "INSERT INTO session (Token, UserId, Created, Reload, IpAddress) VALUES (@Token, @UserId, @Created, @Reload, @IpAddress)";
        public string DeleteSession { get; } = "DELETE FROM session WHERE Token = @Token";
    }
}