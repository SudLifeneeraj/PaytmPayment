using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WIP_Report.Models
{
    public class CRMCallLoginfo
    {
        public string RequestType { get; set; }
        public string RCategary { get; set; }
        public string CallTypeID { get; set; }
        public string CallTypeDes { get; set; }
        public string SubTypeID { get; set; }
        public string SubTypeDes { get; set; }
        public string SFlag { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class CRMInfo
    {
        public string StatusCode { get; set; }
        public string CRM_Call_Id { get; set; }
        public string Policy_Number { get; set; }
        public string Customer_Name { get; set; }
        public DateTime Call_Creation_Date { get; set; }
        public string Call_created_by { get; set; }
        public string Department { get; set; }
        public string Call_category { get; set; }
        public string Call_Type { get; set; }
        public string Sub_Type { get; set; }
        public string Call_Description { get; set; }
        public DateTime Call_Closure_Date { get; set; }
        public string Call_Status { get; set; }
        public string Product_Name { get; set; }
        public string State_code { get; set; }
        public string count_flag_Y { get; set; }
        public string count_flag_N { get; set; }
    }
    public class Clientservicescalllog
    {
        public string ClientID { get; set; }
        public string PolicyNo { get; set; }
        public string RequestType { get; set; }
        public string Category { get; set; }
        public string CallTypeID { get; set; }
        public string CallTypeDesc { get; set; }
        public string SubTypeID { get; set; }
        public string SubTypeDesc { get; set; }
        public string UserInputText { get; set; }
        public string CreateCallCRMID { get; set; }
        public string UpdationCallCRMID { get; set; }
        public string CreateCallOmniID { get; set; }
        public string OmniUplodedFileName { get; set; }
    }
    public class servicecallcreatedata
    {
        public string clientid;
        public string PolicyNo;
        public string Requesttype;
        public string category;
        public string calltypeid;
        public string calltypesesc;
        public string subtypeid;
        public string subtypedcse;
        public string unserinputtext;
        public string createcallcrmid;
        public string updationcrmcallid;
        public string createcallomniid;
        public string omniuploadfilename;
    }
    public class CRMCalllogPolicyDetails
    {
        public string CRM_Call_Id { get; set; }
        public string Policy_Number { get; set; }
        public string Call_Type { get; set; }
        public string Sub_Type { get; set; }
        public DateTime Call_Closure_Date { get; set; }
        public string Call_Status { get; set; }
        public string State_code { get; set; }
    }
    public class crmcallctst
    {
        public string CallTypeID { get; set; }
        public string SubTypeID { get; set; }
    }
}