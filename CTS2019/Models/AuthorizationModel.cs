using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CTS2019.Models
{
    public class AuthorizationModel
    {
        [Required]
        public string AuthorizationID { get; set; }
    }
}