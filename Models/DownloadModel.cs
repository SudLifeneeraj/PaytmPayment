using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WIP_Report.Models
{
    public class DownloadModel
    {
        public string PolicyNo { get; set; }
        public string LinkType { get; set; }
        public string LONGDESC { get; set; }
        public string ITEMITEM { get; set; }
        public string Customer_Name { get; set; }
        public string Mail_id { get; set; }
        public string Mobile { get; set; }
        

        public string Client_ID { get; set; }
        public string Salutation { get; set; }
        public string Policy_Number { get; set; }
        public string Product_Name { get; set; }
        public string plan_type { get; set; }
        public string CategoryName { get; set; }
        public string FileName { get; set; }
        public string FileURL { get; set; }
        public string FileName1 { get; set; }
        public string FileURL1 { get; set; }
        public string filelistdata { get; set; }
        public string GetPolicyFundDetails { get; set; }
        public string Item { get; set; }
        public string PolicyHolderName { get; set; }
        public string ProductType { get; set; }


        public string State_ID { get; set; }
        public string State { get; set; }


    }
  
}