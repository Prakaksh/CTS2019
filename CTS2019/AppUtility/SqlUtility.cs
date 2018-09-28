using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTS2019.AppUtility
{
    public class SqlUtility
    {
        private static readonly string StaffManagementConnectionString = SqlUtility.GetConnectionStringValue("OutwardConnectionString");

        public static SqlConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(StaffManagementConnectionString);

                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string GetConnectionStringValue(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static string ResponseStatusGet(int key)
        {
            string result = string.Empty;
            try
            {
                switch (key)
                {
                    case 1:
                        result = "success";
                        break;
                    case 0:
                        result = "failure";
                        break;
                    default:
                        result = string.Empty;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}