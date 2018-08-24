using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CTS2019.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace CTS2019.Repositories
{
    public class BankContext
    {
        private SqlConnection sqlConnection;

        public List<BankModel> GetBankDetails()
        {
            try
            {
                using (sqlConnection = SqlUtility.GetConnection())
                {
                    List<BankModel> queryValues = sqlConnection.Query<BankModel>("USP_GetBank", commandType: CommandType.StoredProcedure).ToList();
                    return queryValues;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string AddBank(BankModel objBank)
        {
            try
            {

                var com = new DynamicParameters();
                com.Add("@BankCode", objBank.BankCode);
                com.Add("@BankName", objBank.BankName);
                using (sqlConnection = SqlUtility.GetConnection())
                {
                    var queryValues = sqlConnection.Query<int>("USP_AddBank",com, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return AppUtility.getStatus(Convert.ToInt32(queryValues));
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<BranchModel> GetBranchDetails()
        {
            try
            {
                using (sqlConnection = SqlUtility.GetConnection())
                {
                    List<BranchModel> queryValues = sqlConnection.Query<BranchModel>("USP_GetBranch", commandType: CommandType.StoredProcedure).ToList();
                    return queryValues;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string AddBranch(BranchModel objBranch)
        {
            try
            {

                var com = new DynamicParameters();
                com.Add("@CityCode", objBranch.CityCode);
                com.Add("@BankCode", objBranch.BankCode);
                com.Add("@BranchCode", objBranch.BranchCode);
                com.Add("@IFSCCode", objBranch.IFSCCode);
                com.Add("@MICRName", objBranch.MICRName);
                com.Add("@BankName", objBranch.BankName);
                com.Add("@BranchName", objBranch.BranchName);
                using (sqlConnection = SqlUtility.GetConnection())
                {
                    var queryValues = sqlConnection.Query<int>("USP_AddBranch", commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return AppUtility.getStatus(Convert.ToInt32(queryValues));
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}