using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Queries
{
    public class DepartmentQueries
    {
        public string Departments { get; } = "SELECT D.Id, D.Name, C.Hex AS ColorHex, COUNT(E.Id) AS EmployeesCount FROM department D INNER JOIN color C ON D.ColorId = C.Id LEFT JOIN job J ON J.DepartmentId = D.Id LEFT JOIN employee E ON E.JobId = J.Id GROUP BY D.Id, D.Name, C.Hex";
        public string DepartmentsIntegracion { get; } = "select rol.RolID as Id, rol.Nombre as Name, color.Hex AS ColorHex, COUNT(usu.UsuarioID) AS EmployeesCount from roles  as rol left join usuarios as usu on usu.RolId = rol.RolID inner join color as color on rol.ColorId = color.Id GROUP BY rol.RolId, rol.Nombre, color.Hex;";
        public string Department { get; } = "SELECT D.Id, D.Name, C.Hex AS ColorHex, COUNT(E.Id) AS EmployeesCount FROM department D INNER JOIN color C ON D.ColorId = C.Id LEFT JOIN job J ON J.DepartmentId = D.Id LEFT JOIN employee E ON E.JobId = J.Id WHERE D.Id = @Id GROUP BY D.Id, D.Name, C.Hex";
        public string DepartmentAdd { get; } = "INSERT INTO department (ColorId, Name) VALUES(@ColorId, @Name)";
        public string DepartmentEditName { get; } = "UPDATE department SET Name = @Name WHERE Id = @Id";
        public string DepartmentEditColor { get; } = "UPDATE department SET ColorId = @ColorId WHERE Id = @Id";
        public string ColorAdd { get; } = "INSERT INTO color (Available, Hex) VALUES(0, @Hex); SELECT LAST_INSERT_ID();";
    }
}