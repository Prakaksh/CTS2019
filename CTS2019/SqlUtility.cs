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
        private static readonly string StaffManagementConnectionString = SqlUtility.GetConnectionStringValue("CTSConnectionString");

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(StaffManagementConnectionString);

            connection.Open();
            return connection;
        }

        public static string GetConnectionStringValue(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}