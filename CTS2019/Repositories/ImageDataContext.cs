using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CTS2019.Models;
using Dapper;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;

namespace CTS2019.Repositories
{
    public class ImageDataContext
    {
        IDbConnection sqlConnection;

        public string UploadImageData(List<ImageDataModel> insertImageData)
        {
            string status = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[11] {
                                        new DataColumn("ImageName",typeof(string)),
                                        new DataColumn("ChequeNo",typeof(string)),
                                        new DataColumn("MICRCode",typeof(string)),
                                        new DataColumn("SerialNo",typeof(string)),
                                        new DataColumn("TransCode",typeof(string)),
                                        new DataColumn("Amount",typeof(Int64)),
                                        new DataColumn("PresentmentDate",typeof(string)),
                                        new DataColumn("ChequeType",typeof(string)),
                                        new DataColumn("BranchCode",typeof(string)),
                                        new DataColumn("AccountNo",typeof(string)),
                                        new DataColumn("Narration",typeof(string))

                    });

            foreach (ImageDataModel item in insertImageData)
            {
                dt.Rows.Add(item.ImageName, item.ChequeNo, item.MICRCode, item.SerialNo, item.TransCode, item.Amount,
                    item.PresentmentDate, item.ChequeType, item.BranchCode, item.AccountNo, item.Narration);
            }
            var Parameters = new DynamicParameters();

            Parameters.Add("@SOURCE", dt.AsTableValuedParameter("UDT_ImageData"));
           
            try
            {
                using (sqlConnection = SqlUtility.GetConnection())
                {
                    var res = sqlConnection.Query<int>("ImageDataInsert", Parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    status = AppUtility.getStatus(Convert.ToInt32(res)).ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return status;
        }

        public string UploadImage(List<UploadImageModel> insertImage)
        {
            string status = string.Empty;
            DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[5] {
            //                            new DataColumn("ImageName",typeof(string)),
            //                            new DataColumn("FileType",typeof(char)),
            //                            new DataColumn("FrontBWImg",typeof(byte[])),
            //                            new DataColumn("BackBWImg",typeof(byte[])),
            //                            new DataColumn("FrontGrayImg",typeof(byte[])),


            //        });

            foreach (UploadImageModel item in insertImage)
            {
                var Parameters = new DynamicParameters();

                try
                {
                    //_memorystream.Seek(0, SeekOrigin.Begin);
                    Parameters.Add("@FileType", item.FileType);
                    Parameters.Add("@ImageName", item.ImageName);
                    Parameters.Add("@FrontGrayImg", item.FrontGrayImg, DbType.Binary, ParameterDirection.Input, -1);
                    //Parameters.Add("@Photo", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                    Parameters.Add("@BackBWImg", item.BackBWImg, DbType.Binary, ParameterDirection.Input, -1);

                    Parameters.Add("@FrontBWImg", item.FrontBWImg, DbType.Binary, ParameterDirection.Input, -1);
                    //Parameters.Add("@BackBWImg", item.BackBWImg);
                    //A.Add("@A", anexoBytes, dbType: DbType.Binary, direction: ParameterDirection.Input);
                    //A.Get<DbType>("@A");
                    //Parameters.Add("@BackBWImg", item.BackBWImg,dbType: DbType.Binary, direction: ParameterDirection.Input);


                    using (sqlConnection = SqlUtility.GetConnection())
                    {
                        var res = sqlConnection.Query<int>("USP_ImageUpdate", Parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                        status = AppUtility.getStatus(Convert.ToInt32(res)).ToString();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //dt.Rows.Add(item.ImageName, item.FileType, item.FrontBWImg, item.BackBWImg, item.FrontGrayImg);
            }
            //var Parameters = new DynamicParameters();

          //  Parameters.Add("@SOURCE", dt.AsTableValuedParameter("UDT_UploadImage1"));

            //try
            //{
            //    using (sqlConnection = SqlUtility.GetConnection())
            //    {
            //        var res = sqlConnection.Query<int>("USP_ImageUpload", Parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            //        status = AppUtility.getStatus(Convert.ToInt32(res)).ToString();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return status;
        }

        public List<UploadImageModel> GetImage()
        {
            string status = string.Empty;
           
                var Parameters = new DynamicParameters();


            using (sqlConnection = SqlUtility.GetConnection())
            {

                List<UploadImageModel> res = sqlConnection.Query<UploadImageModel>("USP_GetImage", Parameters, commandType: CommandType.StoredProcedure).ToList();
                //byte[] bitmap = res.FrontBWImg;
                return res;
           
                
            }
            //FileInfo fileInfo = new FileInfo(Server.MapPath("~/uploadedfile/"));
            // using (Image image = Image.FromStream(new MemoryStream(bitmap)))
            // {
            //     image.Save("output.jpg", ImageFormat.Jpeg);  // Or Png
            // }
            // BinaryReader br = new BinaryReader(fs);
            // byte[] bytes = br.ReadBytes((Int32)fs.Length);

            // //Save the Byte Array as File.
            // string filePath = "~/Files/" + Path.GetFileName("hcs")+".jpg";
            //     File.WriteAllBytes(Server.MapPath(filePath), bytes);
        }

               
            
        }
    }
    