using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTS2019
{
    public class SqlUtility
    {
        private static readonly string CTSConnectionString = SqlUtility.GetConnectionStringValue("CTSConnectionString");
        private static readonly string OutWardConnectionString = SqlUtility.GetConnectionStringValue("OutwardConnectionString");
        
        public static SqlConnection GetConnection(string strConnection="")
        {
            SqlConnection connection;
            if (strConnection == "CTS") {
                connection = new SqlConnection(CTSConnectionString);
                connection.Open();
            }
            else
            {
                connection = new SqlConnection(OutWardConnectionString);
                connection.Open();
            }
            return connection;
        }

        public static string GetConnectionStringValue(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}