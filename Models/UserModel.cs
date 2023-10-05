using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIP_Report.Models
{
    public class UserModel
    {
        public int ClientID { get; set; }

        public int Token { get; set; }

        public string Policy_Number { get; set; }

        public string Email { get; set; }

        public string MobilePIN { get; set; }

        public string Customer_Name { get; set; }

        public string LoginId { get; set; }

        public string LastLoginDate { get; set; }

        public string Premium_Status_Code { get; set; }

        public string Policy_Status_Code { get; set; }
        public string Mail_id { get; set; }

        public string Mobile_No { get; set; }
        public string PasswordChanged { get; set; }


        // public string Policy_Number { get; set; }
        public string Policy_Holder_Name { get; set; }
        public string Address { get; set; }
        public string ContactNO { get; set; }
        public string amount { get; set; }
        public string MaturityDate { get; set; }
        public string cnt { get; set; }
    }
}