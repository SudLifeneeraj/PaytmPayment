using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WIP_Report.Models
{
    public class PreLogin
    {
       // [Required, AllowHtml]
        public string LoginId { get; set; }

        [Required]
       // [AllowHtml]
        [DataType(DataType.Password)]

        public string otp { get; set; }
        public string token { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string CPParam1 { get; set; }
        public string CPParam2 { get; set; }
        public bool rememberme { get; set; }

       // [Required, AllowHtml]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Customer_Name { get; set; }

        public string PasswordSalt { get; set; }

    }
}