using System;
using System.Data;
using System.Data.SqlClient;

namespace Empresa.Data
{
    public class empresaData
    {
        public static string connectionString
                = "Data Source=DESKTOP-8797AMO\\SQLEXPRESS;Initial Catalog=empresa;Integrated Security=SSPI;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}



 
