using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CTS2019.Models
{
    public class UploadImageModel
    {
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        //[Required]
        public string File { get; set; }
        public string imgFront { get; set; }
        public string imgBack { get; set; }
        public string imgGray { get; set; }
        public byte[] imgFrontByte { get; set; }
        public byte[] imgBackByte { get; set; }
        public byte[] imgGrayByte  { get; set; }

        public string ChequeNo { get; set; }
        public string SortCode { get; set; }
        public string SerialNo { get; set; }
        public string TransCode { get; set; }
        public string AccountType { get; set; }
        public long Amount { get; set; }
        public string PresentmentDate { get; set; }
        public string ChequeType { get; set; }
        public string BranchCode { get; set; }
        public string AccountNo { get; set; }
        public string Narration { get; set; }
        //public List<UploadImage> Images { get; set; }
       
    }
    public class UploadImage
    {
        public string File { get; set; }
        public string ImageName { get; set; }
        public string FileType { get; set; }
        public byte[] FrontBWImg { get; set; }
        public byte[] BackBWImg { get; set; }
        public byte[] FrontGrayImg { get; set; }

    }
}