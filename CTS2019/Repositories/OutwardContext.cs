using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using CTS2019.Models;
using Dapper;
namespace CTS2019.Repositories
{
    public class OutwardContext
    {
        IDbConnection sqlConnection;

        internal string ChequeSave(UploadImageModel obj, UserInfo objUser)
        {
            try
            {
                var Parameters = new DynamicParameters();
                Parameters.Add("Img_Front_Byte", obj.imgFrontByte);
                Parameters.Add("Img_Back_Byte", obj.imgBackByte);
                Parameters.Add("Img_Gray_Byte", obj.imgGrayByte);
                //Parameters.Add("MICR", "");
                Parameters.Add("BatchNo", obj.BatchNo);
                Parameters.Add("ChequeNo", obj.ChequeNo);
                Parameters.Add("SortCode", obj.SortCode);
                Parameters.Add("SerialNo", obj.SerialNo);
                Parameters.Add("TransCode", obj.TransCode);
                Parameters.Add("Amount", obj.Amount);
                Parameters.Add("AccountNo", obj.AccountNo);
                //Parameters.Add("BofdroutNo", obj.);
                //Parameters.Add("Bofddate", obj.);
                //Parameters.Add("ScannerName", obj.);
                //Parameters.Add("MachineID", obj.);
                //Parameters.Add("IQA_Value", obj.);
                //Parameters.Add("IQA_Result", obj.);
                //Parameters.Add("UVver", obj.);
                Parameters.Add("Img_Front_Byte", obj.imgFrontByte, DbType.Binary, ParameterDirection.Input, -1);
                Parameters.Add("Img_Back_Byte", obj.imgBackByte, DbType.Binary, ParameterDirection.Input, -1);
                Parameters.Add("Img_Gray_Byte", obj.imgGrayByte, DbType.Binary, ParameterDirection.Input, -1);


                //Storing into OutWorld DB
                using (sqlConnection = SqlUtility.GetConnection("Outward"))
                {
                    sqlConnection.Query("USP_UploadChequeInsertUpdate", Parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                //Storing into CTS2019 DB
                using (sqlConnection = SqlUtility.GetConnection("CTS"))
                {
                    sqlConnection.Query("USP_UploadChequeInsertUpdate", Parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "success";
        }


        internal int BatchNoGet()
        {
            int BatchNo = 100;
            try
            {

                using (sqlConnection = SqlUtility.GetConnection("Outward"))
                {
                    BatchNo= (int)sqlConnection.Query<int>("USP_BatchNoGet", null, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

            return BatchNo;
        }
    }
}