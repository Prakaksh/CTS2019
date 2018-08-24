using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CTS2019.Models
{
    public class BranchModel
    {
        public string BranchID { get; set; }
        [Required]
        public int CityCode { get; set; }
        [Required]
        public int BankCode { get; set; }
        [Required]
        public int BranchCode { get; set; }
        public string IFSCCode { get; set; }
        public string MICRName { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string BranchName { get; set; }
       
    }
}