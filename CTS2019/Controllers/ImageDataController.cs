using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS2019.Repositories;
using CTS2019.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CTS2019.Controllers
{
    public class ImageDataController : Controller
    {
        ImageDataContext objImageData = new ImageDataContext();
        [HttpGet]
        public ActionResult ImageDataUpload()
        {
            
            return View();
        }
        // GET: ImageDataUpload
        [HttpPost]
        public ActionResult ImageDataUpload(HttpPostedFileBase File, UploadImageModel ImageData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Method 1 Get file details from current request    
                    if (Request.Files.Count > 0)
                    {
                        // Checking Directory exist or not if not exist create the folder inside the server root 
                        FileInfo fileInfo = new FileInfo(Server.MapPath("~/uploadedfile/"));
                        if (!fileInfo.Exists)
                            Directory.CreateDirectory(fileInfo.Directory.FullName);
                     

                        var Inputfile = Request.Files[0];

                        if (Inputfile != null && Inputfile.ContentLength > 0)
                        {


                            // var UploadedfileName = Path.GetFileName(Inputfile.FileName);
                            string type = Inputfile.ContentType;

                            if (type == "text/plain")
                            {
                                var filename = Path.GetFileName(Inputfile.FileName);
                                Inputfile.SaveAs(Path.Combine(Server.MapPath("~/uploadedfile/"), Path.GetFileName(Inputfile.FileName)));

                                var TextData = System.IO.File.ReadLines(Server.MapPath("~/uploadedfile/" + filename)).Select(r => r.TrimEnd('\n'))
                          .Select(line => line.Split('|'))
                          .ToList();
                                List<UploadImageModel> ImageDataList = new List<UploadImageModel>();
                                foreach (string[] line in TextData)
                                {
                                    if (line[0] != "" && line[0] != null)
                                    {
                                        ImageData = new UploadImageModel();
                                        ImageData.ImageName = line[0].Replace("F","").Replace("B","").Replace("G","");
                                        ImageData.ChequeNo = line[1];
                                        ImageData.SortCode = line[2];
                                        ImageData.SerialNo = line[3];
                                        ImageData.TransCode = line[4];
                                        ImageData.AccountType = line[5];
                                        ImageData.Amount = Convert.ToInt64(line[6]);
                                        ImageData.PresentmentDate = line[7];
                                        ImageData.ChequeType = line[8];
                                        ImageData.BranchCode = line[9];
                                        ImageData.AccountNo = line[10];
                                        ImageData.Narration = line[11];

                                        ImageDataList.Add(ImageData);
                                    }
                                }
                                string result = objImageData.UploadImageData(ImageDataList);
                            }
                            else
                            {
                                ViewBag.FileStatus = "Error while file uploading"; ;
                            }

                        }

                        // call the context 
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception ex)
                {
                    ViewBag.FileStatus = "Error while file uploading"; ;
                }

            }

            return View();
        }


        [HttpGet]
        public ActionResult UploadImage()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadImage(List<HttpPostedFileBase> file, UploadImage obj)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //Method 1 Get file details from current request    
                    if (Request.Files.Count >= 0)
                    {
                        // Checking Directory exist or not if not exist create the folder inside the server root 
                        FileInfo fileInfo = new FileInfo(Server.MapPath("~/uploadedfile/"));
                        if (!fileInfo.Exists)
                            Directory.CreateDirectory(fileInfo.Directory.FullName);
                        List<UploadImage> ImageDataList1 = new List<UploadImage>();
                        List<UploadImageModel> ImageDataListBack = new List<UploadImageModel>();
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var Inputfile = Request.Files[i];


                            //var Inputfile = Request.Files[0];

                            if (Inputfile != null && Inputfile.ContentLength > 0)
                            {
                                List<UploadImageModel> ImageDataList = new List<UploadImageModel>();



                                // var UploadedfileName = Path.GetFileName(Inputfile.FileName);
                                string type = Inputfile.ContentType;

                                if (type == "image/jpg" || type == "image/jpeg" || type == "image/tiff" || type == "image/tif")
                                {
                                    var filename = Path.GetFileName(Inputfile.FileName);
                                    obj = new UploadImage();
                                   // var Filetype = filename.Contains("F") ? "F" : "B";
                                   // obj.ImageName = filename.Replace("F", "").Replace("B", "").Replace("G", "");
                                    string extension = Path.GetExtension(filename).ToLower();
                                    if (extension == ".tiff" || extension == ".tif")
                                    {
                                        if (filename.Contains("F"))
                                        {
                                            
                                            obj.FileType = "F";
                                            obj.ImageName = filename.Replace("F", "").Replace("B", "").Replace("G", "").Replace(".tiff", "").Replace(".tif", "");

                                            obj.FrontBWImg = new byte[Inputfile.ContentLength]; // file1 to store image in binary formate  
                                            Inputfile.InputStream.Read(obj.FrontBWImg, 0, Inputfile.ContentLength);
                                           
                                        }
                                       
                                        if (filename.Contains("B"))
                                        {
                                            obj.FileType = "B";
                                            obj.ImageName = filename.Replace("F", "").Replace("B", "").Replace("G", "").Replace(".tiff", "").Replace(".tif", "");

                                            obj.BackBWImg = new byte[Inputfile.ContentLength]; // file1 to store image in binary formate  
                                            Inputfile.InputStream.Read(obj.BackBWImg, 0, Inputfile.ContentLength);
                                            //ImageDataListBack.Add(obj);
                                            //string resultBackImage = objImageData.UploadImage(ImageDataListBack);
                                        }

                                    }
                                    if (extension == ".jpeg" || extension == ".jpg")
                                    {
                                        if (filename.Contains("G"))
                                        {
                                            obj.FileType = "G";
                                            obj.ImageName = filename.Replace("G", "").Replace(".jpeg","").Replace(".jpg","");

                                            obj.FrontGrayImg = new byte[Inputfile.ContentLength]; // file1 to store image in binary formate  
                                            Inputfile.InputStream.Read(obj.FrontGrayImg, 0, Inputfile.ContentLength);
                                        }
                                    }
                                    if (obj.ImageName!=null)
                                    ImageDataList1.Add(obj);
                                }
                                
                            }
                        }
                        string result = objImageData.UploadImage(ImageDataList1);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception ex)
                {

                    ViewBag.FileStatus = "Error while file uploading."; ;
                }


            }
            return View();
        }

      
    }
}