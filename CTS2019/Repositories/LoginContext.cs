using CTS2019.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CTS2019.Repositories
{
    public class LoginContext
    {
        IDbConnection sqlConnection;

        public UserInfo GetLogin(Login obj)
        {
            //string result = "";
            UserInfo objUser = new UserInfo();
            try
            {
                using (sqlConnection = SqlUtility.GetConnection("CTS"))
                {
                    var com = new DynamicParameters();
                    com.Add("@UserName", obj.UserName);
                    com.Add("@Password", obj.Password);
                    objUser = sqlConnection.Query<UserInfo>("usp_UserAuthenticate", com, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return objUser;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}