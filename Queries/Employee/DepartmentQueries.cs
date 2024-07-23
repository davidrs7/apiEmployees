using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Queries
{
    public class DepartmentQueries
    {
        public string Departments { get; } = "SELECT D.Id, D.Name, C.Hex AS ColorHex, COUNT(E.Id) AS EmployeesCount FROM Department D INNER JOIN Color C ON D.ColorId = C.Id LEFT JOIN Job J ON J.DepartmentId = D.Id LEFT JOIN Employee E ON E.JobId = J.Id GROUP BY D.Id, D.Name, C.Hex";
        public string DepartmentsIntegracion { get; } = "select rol.RolID as Id, rol.Nombre as Name, Color.Hex AS ColorHex, COUNT(usu.UsuarioID) AS EmployeesCount from Roles  as rol left join Usuarios as usu on usu.RolId = rol.RolID inner join color as color on rol.ColorId = color.Id GROUP BY rol.RolId, rol.Nombre, Color.Hex;";
        public string Department { get; } = "SELECT D.Id, D.Name, C.Hex AS ColorHex, COUNT(E.Id) AS EmployeesCount FROM Department D INNER JOIN Color C ON D.ColorId = C.Id LEFT JOIN Job J ON J.DepartmentId = D.Id LEFT JOIN Employee E ON E.JobId = J.Id WHERE D.Id = @Id GROUP BY D.Id, D.Name, C.Hex";
        public string DepartmentAdd { get; } = "INSERT INTO Department (ColorId, Name) VALUES(@ColorId, @Name)";
        public string DepartmentEditName { get; } = "UPDATE Department SET Name = @Name WHERE Id = @Id";
        public string DepartmentEditColor { get; } = "UPDATE Department SET ColorId = @ColorId WHERE Id = @Id";
        public string ColorAdd { get; } = "INSERT INTO Color (Available, Hex) VALUES(0, @Hex); SELECT LAST_INSERT_ID();";
    }
}