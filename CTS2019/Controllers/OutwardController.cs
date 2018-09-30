using CTS2019.Filters;
using CTS2019.Models;
using CTS2019.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS2019.Controllers
{
    public class OutwardController : BaseController
    {
        ImageDataContext objImageData = new ImageDataContext();
        List<string> columns = null;
        OutwardContext objContext;
        //Declare the Start and end offset legth of ImageUploadConfiguration File
        int SerialNoStart = 0, SerialNoEnd = 0, DateStart = 0, DateEnd = 0, BranchCodeStart = 0, TransStart = 0, TransEnd = 0, AccStart = 0, AccEnd = 0, AmtStart = 0, AmtEnd = 0,
        BranchCodeEnd = 0, NarationStart = 0, NarationEnd = 0, ImageFStart = 0, ImageFEnd = 0, ImageBStart = 0, ImageBEnd = 0, ImageGStart = 0, ImageGEnd = 0, SortCodeStart = 0, SortCodeEnd = 0, ChqNoEnd = 0, ChqNoStart = 0;

        // GET: Outward
        [HttpGet]
        [SessionTimeout]
        public ActionResult OutwardPage()
        {
            return View();
        }
        // GET: Outward
        [HttpGet]
        public ActionResult ImageUpload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadImage(List<HttpPostedFileBase> file, UploadImageModel obj)
        {
            HttpPostedFileBase MICRFile = null;
            string strMICRPath = "";
            
            UploadImageModel objImage;
            try
            {
                //Get Session information of Loggedin user
                UserInfo objUser = new UserInfo();// UserInfoGet();

                //Find text file for MICR information
                if (file.Count > 0)
                {
                    MICRFile = file.Where(x => x.ContentType.ToLower() == "text/plain").FirstOrDefault();
                    if (MICRFile != null && MICRFile.ContentLength > 0)
                    {
                        strMICRPath = MICRSave(MICRFile);
                    }
                }

                if (strMICRPath != "")
                {

                    List<string> listCheque = System.IO.File.ReadLines(strMICRPath)
                                        .Select(r => r.TrimEnd('\n')).ToList();
                    string line;
                    StreamReader file2 = new StreamReader(Server.MapPath("~/FileConfiguration/ImageUploadConfiguration.txt"));
                    while ((line = file2.ReadLine()) != null)
                    {
                        columns = line.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                    file2.Close();

                    if (columns != null)
                    {
                        AssignIndex();
                    }
                    else
                    {
                        throw new Exception("Cheque configuration is Missing. Please Contact your Administrator!!!");
                    }

                    List<UploadImageModel> ImageList = new List<UploadImageModel>();
                    List<UploadImageModel> FailedChequeList = new List<UploadImageModel>();

                    foreach (string chequeLine in listCheque)
                    {
                        if (!string.IsNullOrEmpty(chequeLine))
                        {
                            string[] strColumn = columns.Where(x => x.Contains("ChequeNo")).FirstOrDefault().Split('_');
                            objImage = new UploadImageModel();
                            
                            objImage.ChequeNo = chequeLine.Substring(ChqNoStart, ChqNoEnd);
                            objImage.SortCode = chequeLine.Substring(SortCodeStart, SortCodeEnd);
                            objImage.SerialNo = chequeLine.Substring(SerialNoStart, SerialNoEnd);
                            objImage.TransCode = chequeLine.Substring(TransStart, TransEnd);

                            objImage.imgFront = chequeLine.Substring(ImageFStart, ImageFEnd);
                            objImage.imgBack = chequeLine.Substring(ImageBStart, ImageBEnd);
                            objImage.imgGray = chequeLine.Substring(ImageGStart, ImageGEnd);

                            //When there is any mismatch of a Cheque with image
                            if (string.IsNullOrEmpty(objImage.imgFront) || string.IsNullOrEmpty(objImage.imgBack) || string.IsNullOrEmpty(objImage.imgGray))
                            {
                                FailedChequeList.Add(objImage);
                                continue;
                            }

                            //Get Binary Data of files
                            objImage.imgFrontByte = FileBinaryGet(file.Where(x => x.FileName.ToLower() == objImage.imgFront.ToLower()).FirstOrDefault());
                            objImage.imgBackByte = FileBinaryGet(file.Where(x => x.FileName.ToLower() == objImage.imgBack.ToLower()).FirstOrDefault());
                            objImage.imgGrayByte = FileBinaryGet(file.Where(x => x.FileName.ToLower() == objImage.imgGray.ToLower()).FirstOrDefault());

                            //When there is any mismatch of a Cheque with image
                            if (objImage.imgFrontByte == null || objImage.imgBackByte == null || objImage.imgGrayByte == null)
                            {
                                FailedChequeList.Add(objImage);
                                continue;
                            }
                          
                            objImage.Amount = Convert.ToInt64(chequeLine.Substring(AmtStart, AmtEnd));
                            objImage.AccountNo = chequeLine.Substring(AccStart, AccEnd);
                            objImage.PresentmentDate = chequeLine.Substring(DateStart, DateEnd);
                            objImage.BranchCode = chequeLine.Substring(BranchCodeStart, BranchCodeEnd);
                            objImage.Narration = chequeLine.Substring(NarationStart, NarationEnd);
                            objImage.BatchNo = obj.BatchNo;


                            //Sending to DBContext
                            objContext = new OutwardContext();
                            objContext.ChequeSave(objImage, objUser);

                            //Adding it to List
                            //ImageList.Add(objImage);
                        }
                    } // foreach end


                    ViewBag.FailedCheque = FailedChequeList;
                    //ViewBag.FileStatus = "File uploaded successfully.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = ex.Message;
                return View("OutwardPage");
                //return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            ViewBag.Result = "Success";
           
            return View("OutwardPage");
            //return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BatchNoGet()
        {
            int BatchNo = 100;
            objContext = new OutwardContext();
            try
            {
                BatchNo = objContext.BatchNoGet();
            }
            catch (Exception ex)
            {
                return Json("Failure");
            }
            return Json(BatchNo, JsonRequestBehavior.AllowGet);
        }


        //Uploading MICR to directory
        internal string MICRSave(HttpPostedFileBase file)
        {
            string strPath;
            try
            {
                FileInfo folderPath = new FileInfo(Server.MapPath("~/MICR/" + DateTime.Today.ToString("dd-MM-yyyy")));
                if (!folderPath.Exists)
                    Directory.CreateDirectory(folderPath.Directory.FullName);
                strPath = folderPath + (DateTime.Now.TimeOfDay.ToString().Replace(':', '-'));
                file.SaveAs(strPath);
            }
            catch (Exception)
            { throw; }
            return strPath;
        }

        internal byte[] FileBinaryGet(HttpPostedFileBase postedFile)
        {
            byte[] bytes = null;
            try
            {
                if (postedFile != null)
                {
                    //Read the uploaded File as Byte Array from FileUpload control.
                    Stream fs = postedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    return bytes;
                }
                else
                    return bytes;
            }
            catch (Exception)
            {
                    
                throw;
            }
        }

        internal string ChequeValueGet(string ChequeLine, string ChequeProperty)
        {
            string strValue = string.Empty;
            try
            {
                string strColumn = columns.Where(x => x.Contains(ChequeProperty)).FirstOrDefault();
                string[] strArray = (!string.IsNullOrEmpty(strColumn) ? strColumn.Split('_') : null);
                strValue = ChequeLine.Substring(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[2]));
            }
            catch (Exception)
            {
                throw;
            }
            return strValue;
        }

        internal void AssignIndex()
        {
            foreach (string c in columns)
            {
                string[] Temp = c.Split('_');

                switch (Temp[1])
                {
                    case "ChequeNo":
                        ChqNoStart = Convert.ToInt16(Temp[0]);
                        ChqNoEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "SortCode":
                        SortCodeStart = Convert.ToInt16(Temp[0]);
                        SortCodeEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "SerialNo":
                        SerialNoStart = Convert.ToInt16(Temp[0]);
                        SerialNoEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "TransCode":
                        TransStart = Convert.ToInt16(Temp[0]);
                        TransEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "Amount":
                        AmtStart = Convert.ToInt16(Temp[0]);
                        AmtEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "AccountNo":
                        AccStart = Convert.ToInt16(Temp[0]);
                        AccEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "PresentmentDate":
                        DateStart = Convert.ToInt16(Temp[0]);
                        DateEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "BranchCode":
                        BranchCodeStart = Convert.ToInt16(Temp[0]);
                        BranchCodeEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "Naration":
                        NarationStart = Convert.ToInt16(Temp[0]);
                        NarationEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "ImageF":
                        ImageFStart = Convert.ToInt16(Temp[0]);
                        ImageFEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "ImageB":
                        ImageBStart = Convert.ToInt16(Temp[0]);
                        ImageBEnd = Convert.ToInt16(Temp[2]);
                        break;
                    case "ImageG":
                        ImageGStart = Convert.ToInt16(Temp[0]);
                        ImageGEnd = Convert.ToInt16(Temp[2]);
                        break;
                }
            }
        }
    }
}