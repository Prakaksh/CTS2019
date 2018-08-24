using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CTS2019.Models
{
    public class ImageDataModel
    {
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        [Required]
        public string File { get; set; }
        public string ChequeNo { get; set; }
        public string MICRCode { get; set; }
        public string SerialNo { get; set; }
        public string TransCode { get; set; }
        public string AccountType { get; set; }
        public long Amount { get; set; }
        public string PresentmentDate { get; set; }
        public string ChequeType { get; set; }
        public string BranchCode { get; set; }
        public string AccountNo { get; set; }
        public string Narration { get; set; }
        
    }
    public class Item
    {
        public List<ImageDataModel> ItemList { get; set; }
    }

    public class UploadImageModel
    {
        public string File { get; set; }
        public string ImageName  { get; set; }
        public string FileType { get; set; }
        public byte[] FrontBWImg { get; set; }
        public byte[] BackBWImg { get; set; }
        public byte[] FrontGrayImg { get; set; }
       
        
    }


}