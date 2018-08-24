using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CTS2019.Models
{
    public class BankModel
    {

        public string BankID { get; set; }
        [Required]
        public int BankCode { get; set; }
        [Required]
        public string BankName { get; set; }
    }
}