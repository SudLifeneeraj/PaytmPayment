using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIP_Report.Models
{
    public class DashboardModel
    {
       
        public string Client_ID { get; set; }
        public string Policy_Number { get; set; }
        public string Policy_Holder_Name { get; set; }
        public string Premium_Amount { get; set; }
        public string Sum_Assured { get; set; }
        public string Product_Name { get; set; }
        public string Policy_Term { get; set; }
        public string Next_Premium_Due_Date { get; set; }
        public string Last_Premium_Paid_Date { get; set; }
        public string Policy_Commencement_Date { get; set; }
        public string Date_of_Maturity { get; set; }
        public string Agent_Code { get; set; }
        public string Year { get; set; }
        public string Policy_Status { get; set; }
        public string Existing_Modal_Premium { get; set; }
        public string Existing_Mode { get; set; }
        public string Total_Premium_Paid { get; set; }
        public string Total_Topup_Paid { get; set; }
        public string Policy_Lapse_Date { get; set; }
        public string Policy_Product_Code { get; set; }
        public string  Policy_Product_Category { get; set; }
        public string Paid_To_Date { get; set; }
        public string  Surrender_Value { get; set; }
        public string ETL_Date { get; set; }
        public string Total_Policy_Value { get; set; }
        public string Rider { get; set; }
        public string Sales_Unit_Code { get; set; }
        public string Premium_Cess_Term { get; set; }
        public string BTDATE { get; set; }
        public string Annuity_Start_Date { get; set; }
        public string Last_Annuity_Paid_Date { get; set; }
        public string Next_Payment_Date { get; set; }
        public string Annuity_Frequency { get; set; }
        public string Premium_Status { get; set; }
        public string Premium_Status_Code { get; set; }
        public string Policy_Status_Code { get; set; }
        public string Expr1 { get; set; }
        public string Salutation { get; set; }
        public string Customer_Name { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Street { get; set; }
        public string Line_1 { get; set; }
        public string Line_2 { get; set; }
        public string Line_3 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PIN { get; set; }
        public string Country { get; set; }
        public string Business_or_Residence { get; set; }
        public string  Mail_id { get; set; }
        public string Phone1 { get; set; }

        public string Phone2 { get; set; }
        public string Mobile_No { get; set; }
        public string Expr2 { get; set; }
        public string Marital_Status { get; set; }
        public string PanCard_Exemption_flag { get; set; }
        public string  Occupation_code { get; set; }
        public string Security_no { get; set; }
        public string  IDProof { get; set; }
        public string AddressProof { get; set; }
        public string IncomeProof { get; set; }
        public string Customer_Category { get; set; }
        public string Alternate_Email_Id { get; set; }
        public string Alternate_Mobile_No { get; set; }
        public string PAN_NUMBER { get; set; }
        public string Alternate_Address_Line_1 { get; set; }
        public string Alternate_Address_Line_2 { get; set; }
        public string Alternate_Address_Line_3 { get; set; }
        public string  Alternate_Address_City  { get; set; }
        public string Alternate_Address_State { get; set; }
        public string Alternate_Address_PIN { get; set; }
        public string Alternate_Address_Country { get; set; }


    }
   
    public class annutiyanexture
    {
        public string cnt { get; set; }
    }

    public class NotificationforMaturity
    {
        public string MaturityDate { get; set; }
    }
    public class UnclaimedAmount
    {
        public string amount { get; set; }
        public string MaturityDate { get; set; }
    }
}