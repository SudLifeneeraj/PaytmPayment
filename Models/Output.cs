using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIP_Report.Models
{
    public class Output
    {
        public string ResponseCode { get; set; }
        public string Response { get; set; }
        public string Description { get; set; }
        public string ApplicationNo { get; set; }
      
        //public List<ReportData> ReportData { get; set; }
    }


    public class LoginResponse
    {
        public string token { get; set; }
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
    }

    public class ReportData
    {
        public string Application_no { get; set; }
        // public string Inwarddate1 { get; set; }
        //public string Time_Inwarddate { get; set; }
        public string Premium_Amount { get; set; }
        //public string Inward_Location { get; set; }
        //public string STPCheckList { get; set; }
        //public string RecieverName { get; set; }
        public string PlanName { get; set; }
        //public string Premium_Mode { get; set; }
        //public string Bank_Branch_Code { get; set; }
        // public string RelativeBank { get; set; }
        // public string Partner_Code { get; set; }
        // public string Composite_code { get; set; }
        public string Bank_Name { get; set; }
        //public string Bank_Region { get; set; }
        //public string Bank_Zone { get; set; }
        //public string SUD_Branch_Office { get; set; }
        //public string SUD_Region { get; set; }
        //public string Zone { get; set; }
        //public string Sub_Region { get; set; }
        //public string Zonal_Business_Director { get; set; }
        //public string RH_Name { get; set; }
        //public string AH_TH_Name { get; set; }
        //public string Branch_BM { get; set; }
        //public string LH_Code { get; set; }
        //public string Branch_AM { get; set; }
        //public string SO_Code { get; set; }
        //public string Potential_Category { get; set; }
        //public string Branch_Category { get; set; }
        //public string Branch_Type { get; set; }
        //public string ContractNumber { get; set; }
        //public string ContractStatus { get; set; }
        public string Department { get; set; }
        //public string Task_Remarks { get; set; }
        public string Requirement_Code { get; set; }
        public string Requirement_Description { get; set; }
        //public string Number_Of_Requirements { get; set; }
        //public string Task_Date { get; set; }
        //public string receipt_No { get; set; }
        //public string Days { get; set; }
        public string Ageing { get; set; }
        //public string Location { get; set; }
        public string Customer_Code { get; set; }
        public string Customer_Name { get; set; }
        //public string Department1 { get; set; }
    }
}