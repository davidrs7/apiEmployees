
namespace Api.Queries
{
    public class ConfigurationQueries
    {
        public string ConfigurationItems { get; } = "SELECT C.ConfigKey, C.UserId, C.Updated, C.Value1, C.Value2, C.ListType, CL.Item AS ListItem, CL.Value AS ListValue FROM Configuration C LEFT JOIN Configuration_List CL ON C.ConfigKey = CL.ConfigKey WHERE C.ConfigKey = @ConfigKey";
        public string ConfigurationItemValue { get; } = "SELECT C.ConfigKey, C.UserId, C.Updated, C.Value1, C.Value2, C.ListType, CL.Item AS ListItem, CL.Value AS ListValue FROM Configuration C INNER JOIN Configuration_List CL ON C.ConfigKey = CL.ConfigKey WHERE C.ConfigKey = @ConfigKey AND CL.Value = @Value";
        public string ItemAdd { get; } = "INSERT INTO Configuration_List (ConfigKey, Item, Value) VALUES (@ConfigKey, @Item, @Value)";
        public string ItemDelete { get; } = "DELETE FROM Configuration_List WHERE ConfigKey = @ConfigKey AND Item = @Item AND Value = @Value";
        public string SearchConfig { get; } = "SELECT COUNT(*) FROM Configuration C WHERE C.ConfigKey = @ConfigKey";
        public string ConfigAdd { get; } = "INSERT INTO Configuration (ConfigKey, UserId, Updated, Value1, Value2, ListType) VALUES (@ConfigKey, @UserId, @Updated, @Value1, @Value2, @ListType)";
        public string ConfigEdit { get; } = "UPDATE Configuration SET UserId = @UserId, Updated = @Updated, Value1 = @Value1, Value2 = @Value2, ListType = @ListType WHERE ConfigKey = @ConfigKey";
        public string UsersByDepartments { get; } = "SELECT U.Id, U.UserName, U.EmployeeId, E.Name, IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, E.JobId, J.Name AS JobName, J.DepartmentId, D.Name AS DepartmentName, IF(U.Id IN (@Ins), 1, 0) AS IsSelected FROM User_Login U INNER JOIN Employee E ON U.EmployeeId = E.Id INNER JOIN Job J ON E.JobId = J.Id INNER JOIN Department D ON J.DepartmentId = D.Id WHERE D.Id IN (@Ids)";
    }
}