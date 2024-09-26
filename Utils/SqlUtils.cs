using System.Data;
using System.Data.SqlClient; 
using MySqlConnector;

namespace Api.Utils
{
    static public class SqlUtils
    {

        public static object? obtainValue(object? value)
        {
            return (value != null && value.ToString() != "") ? value : DBNull.Value;
        }
         

        /* 
         Autor: David Rodriguez 
         Fecha: Jun 2024
         Desc:  Migración Sql server a Mysql server         
         */ 
        public static MySqlParameter obtainMySqlParameter(string paramName, DateTime? value)
        {
            MySqlParameter param = new MySqlParameter(paramName, MySqlDbType.DateTime);
            param.Value = obtainValue(value);
            return param;
        }

      
        /* 
        Autor: David Rodriguez 
        Fecha: Jun 2024
        Desc:  Migración Sql server a Mysql server         
        */
        public static MySqlParameter obtainMySqlParameter(string paramName, string? value)
        {
            MySqlParameter param = new MySqlParameter(paramName, MySqlDbType.LongText);
            param.Value = obtainValue(value);
            return param;
        }

    
        /* 
        Autor: David Rodriguez 
        Fecha: Jun 2024
        Desc:  Migración Sql server a Mysql server         
        */
        public static MySqlParameter obtainMySqlParameter(string paramName, int? value)
        {
            MySqlParameter param = new MySqlParameter(paramName, MySqlDbType.Int32);
            param.Value = obtainValue(value);
            return param;
        }


         
        /* 
        Autor: David Rodriguez 
        Fecha: Jun 2024
        Desc:  Migración Sql server a Mysql server         
        */
        public static MySqlParameter obtainMySqlParameter(string paramName, double? value)
        {
            MySqlParameter param = new MySqlParameter(paramName, MySqlDbType.Decimal);
            param.Value = obtainValue(value);
            return param;
        }

      
        /* 
         Autor: David Rodriguez 
         Fecha: Jun 2024
         Desc:  Migración Sql server a Mysql server         
         */
        public static MySqlParameter obtainMySqlParameter(string paramName, bool? value)
        {
            MySqlParameter param = new MySqlParameter(paramName, MySqlDbType.Bit);
            param.Value = obtainValue(value);
            return param;
        }
    }
}