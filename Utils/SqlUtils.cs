using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Api.Utils
{
    static public class SqlUtils
    {

        public static object? obtainValue(object? value)
        {
            return (value != null && value.ToString() != "") ? value : DBNull.Value;
        }

        public static SqlParameter obtainSqlParameter(string paramName, DateTime? value)
        {
            SqlParameter param = new SqlParameter(paramName, SqlDbType.DateTime);
            param.Value = obtainValue(value);
            return param;
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

        public static SqlParameter obtainSqlParameter(string paramName, string? value)
        {
            SqlParameter param = new SqlParameter(paramName, SqlDbType.NVarChar);
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

        public static SqlParameter obtainSqlParameter(string paramName, int? value)
        {
            SqlParameter param = new SqlParameter(paramName, SqlDbType.Int);
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



        public static SqlParameter obtainSqlParameter(string paramName, double? value)
        {
            SqlParameter param = new SqlParameter(paramName, SqlDbType.Money);
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

        public static SqlParameter obtainSqlParameter(string paramName, bool? value)
        {
            SqlParameter param = new SqlParameter(paramName, SqlDbType.Bit);
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