namespace Api.Queries
{
    public class UserQueries
    {
        public string UserLogin { get; } = "SELECT U.Id, U.CreateAt, U.Available, U.UserName, U.Password FROM User_Login U WHERE U.UserName = @UserName AND U.Password = @Password";
        public string UserId { get; } = "SELECT U.Id FROM User_Login U WHERE U.UserName = @UserName";
        public string UserById { get; } = "SELECT U.Id, U.CreateAt, U.Available, U.UserName, '' AS Password, U.EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, J.Id AS JobId, J.Name AS JobName, D.Id AS DepartmentId, D.Name AS DepartmentName FROM User_Login U LEFT JOIN Employee E ON E.Id = U.EmployeeId LEFT JOIN Job J ON J.Id = E.JobId LEFT JOIN Department D ON D.Id = J.DepartmentId WHERE U.Id = @Id";
        public string Users { get; } = "SELECT U.Id, U.CreateAt, U.Available, U.UserName, '' AS Password, U.EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, J.Id AS JobId, J.Name AS JobName, D.Id AS DepartmentId, D.Name AS DepartmentName FROM User_Login U LEFT JOIN Employee E ON E.Id = U.EmployeeId LEFT JOIN Job J ON J.Id = E.JobId LEFT JOIN Department D ON D.Id = J.DepartmentId";
        public string SessionByToken { get; } = "SELECT S.Token, S.UserId, S.Created, S.Reload, S.IpAddress FROM Session S WHERE S.Token = @Token";
        public string AddSession { get; } = "INSERT INTO Session (Token, UserId, Created, Reload, IpAddress) VALUES (@Token, @UserId, @Created, @Reload, @IpAddress)";
        public string DeleteSession { get; } = "DELETE FROM Session WHERE Token = @Token";
    }
}