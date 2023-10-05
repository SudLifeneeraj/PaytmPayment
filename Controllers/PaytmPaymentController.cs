using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
//using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
//using Microsoft.Practices.EnterpriseLibrary.Common;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
//using Microsoft.Security.Application;
using System.Diagnostics;
using System.Net.Mail;
//using SUD.BusinessEntities;
//using System.Threading;
using System.Resources;
using System.Globalization;
using System.Xml.Serialization;
//using Microsoft.SharePoint;
using System.Data.SqlClient;
//using SUD.UserInterface;
using System.ServiceModel.Activation;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net;
//using paytm;
using WIP_Report.Models;
using WIP_Report.Helper;
using System.Web.Http;
using ComputeHash;
using WIP_Report.Database_Access_layer;
//using SUD.BusinessEntities;
using SUD.BusinessLogic;
using SUD.UserInterface;
using WIP_Report.Helpers;
using SUD.BusinessEntities;
using Customer_portal.ServicessCalls;

namespace WIP_Report.Controllers
{
    public class PaytmPaymentController : ApiController
    {
        GetallDetailsResult objPay = new GetallDetailsResult();
        protected bool rethrow = false;
        Helpercls objHelper = new Helpercls();
        HelperClass objHelpeclassr = new HelperClass();
        ErrorLog elog = new ErrorLog();
        ServicessAccess objservaccess = new ServicessAccess();
        string lblCurrentDateVal;
        string lblPolicyNoVal;
        string lblPolicyHolderNameVal;
        string lblPremiumAmountValue;
        string lblTransactionIDValue;
        string lblTransactionIDValueN;
        int Revival180;
        string lblReinstatement;
        string strProductName;
        string strPaymentFrequency;
        string NextPremiumDueDate;
        string CRMId;
        string strPaymentType = string.Empty;
        bool emailsent = false;
        bool SMS_Sent = false;
        string json = string.Empty;
        string DOB;
        string PolcyNoinput;
        string Request_ID;
        string MobileNo;
        string EMail;
        string VendorName;
        string checksum;
        string PC;
        string ClientId;
        // string CLIENTID;
        string PolicyNumber;
        string policyNo;
        string PolicyInfo;
        WCFProperty Policyinput1 = new WCFProperty();

        #region Paytm Variables
        string strResponsemsgPaytm = string.Empty;
        //  string strmsgPaytmSUBS_ID = string.Empty;
        // string strMsgPaytmMID = string.Empty;
        // string strMsgPaytmTXNID = string.Empty;
        //  string strMsgPaytmORDERID = string.Empty;
        string strMsgPaytmBANKTXNID = string.Empty;
        string strMsgPaytmTXNAMOUNT = string.Empty;
        string strMsgPaytmCURRENCY = string.Empty;
        string strMsgPaytmSTATUS = string.Empty;
        //  string strMsgPaytmRESPCODE = string.Empty;
        string strMsgPaytmRESPMSG = string.Empty;
        string strMsgPaytmTXNDATE = string.Empty;
        string strMsgPaytmGATEWAYNAME = string.Empty;
        string strMsgPaytmBANKNAME = string.Empty;
        // string strMsgPaytmPAYMENTMODE = string.Empty;
        string strMsgPaytmPROMO_CAMP_ID = string.Empty;
        // string strMsgPaytmPROMO_STATUS = string.Empty;
        // string strMsgPaytmPROMO_RESPCODE = string.Empty;
        string strMsgPaytmCHECKSUMHASH = string.Empty;
        // string strMsgPaytmAdditionalInfo1 = string.Empty;
        //  string strMsgPaytmAdditionalInfo2 = string.Empty;
        string PayType = string.Empty;
        string lblPaymentGatewayResultValue = string.Empty;
        string bankName = string.Empty;
        string paymentCode = string.Empty;
        string paymentGateway = string.Empty;
        string currentStatus = string.Empty;
        string ReinstatementFee = string.Empty;
        string OutstandingPremium = string.Empty;
        string ServiceTaxEducationCess = string.Empty;
        string AdjustmentAmount = string.Empty;
        string TransactionIDPremiumPayment = string.Empty;
        string RefreshClickChk = string.Empty;
        string TransactionIDReinstatement = string.Empty;
        string Existing_Mode = string.Empty;
        string PaymentType = string.Empty;
        string PayableAmount = string.Empty;
        string ActionReinstatement = string.Empty;
        string AlternateEmailId = string.Empty;
        string AlternateMobileNo = string.Empty;
        string RegisteredEmailId = string.Empty;
        string RegisteredMobileNo = string.Empty;
        // string SUBS_ID;
        // string MID;
        // string TXNID;
        // string ORDERID;
        string BANKTXNID;
        string TXNAMOUNT;
        string CURRENCY;
        string STATUS;
        // string RESPCODE;
        string RESPMSG;
        string TXNDATE;
        string GATEWAYNAME;
        string BANKNAME;
        //  string PAYMENTMODE;
        string TRAN_Type;
        // string PROMO_STATUS;
        // string PROMO_RESPCODE;
        string CHECKSUMHASH;
        //  string MERC_UNQ_REF;
        string PolicyStatus;
        string TotalInput;

        //Reinstatement
        string lblFrequency;
        string lblPaidFromDate;
        string lblPaidToDate;
        string lblOutstandingPremium;
        string lblServiceTaxEducationCess;
        string lblReinstatementFee;
        string lblAdjustmentAmount;
        string lblReinstatementAmount;





        string lblFrequencyVal;
        string lblPaidFromDateVal;
        string lblPaidToDateVal;
        string lblOutstandingPremiumVal;
        string lblServiceTaxEducationCessVal;
        string lbllblReinstatementFee;
        string lblAdjustmentAmountVal;
        string lblReinstatementAmountVal;
        string lblProjectedPaidToDateVal;
        string lblLifeAssuredVal;


        #endregion



        [System.Web.Http.HttpPost]


        // public WCFProperty GetallDetails(string PolcyNoi, string ClientIdi, string Request_IDi, string MobileNoi, string EMaili, string VendorNamei, string PCi, string checksumi, string BANKTXNIDi, string TXNAMOUNTi, string TransactionCurrentcyi, string TransactionStatusi, string RESPMSGi, string TXNDATEi, string GATEWAYNAMEi, string BANKNAMEi, string TRAN_Typei, string PayableAmounti)
        public WCFProperty GetallDetails(Input input)



        // public WCFProperty GetallDetails(string PolcyNoi, string ClientIdi, string Request_IDi, string MobileNoi, string EMaili, string VendorNamei, string PCi, string checksumi, string SUBS_IDi, string MIDi, string TXNIDi, string ORDERIDi, string BANKTXNIDi, string TXNAMOUNTi, string CURRENCYi, string STATUSi, string RESPCODEi, string RESPMSGi, string TXNDATEi, string GATEWAYNAMEi, string BANKNAMEi, string PAYMENTMODEi, string PROMO_CAMP_IDi, string PROMO_STATUSi, string PROMO_RESPCODEi, string MERC_UNQ_REFi, string TransactionIDPremiumPaymenti, string TransactionIDReinstatementi, string PayableAmounti)
        //   public WCFProperty GetallDetails(Stream request)
        {
            ////  string resultstr="";

            //  //MemoryStream stream1 = new MemoryStream();
            //  //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(WCFProperty));
            //  //ser.WriteObject(stream1, request);
            //  //stream1.Position = 0;
            //  StreamReader sr = new StreamReader(request); 
            //      using (var reader = sr)
            //      {
            //       json = reader.ReadToEnd();
            //      }
            //      elog.LogData("stream :" + json.ToString() + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Inside Try ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

            //      // string[] PaytmInputStr = json.Split('=,&');
            // string[] PaytmInputStr = json.Split(new Char[] { '=', '&' });  //to split many char like { =,&} we take char array
            //  //string[] PaytmInputStr = json.Split(new Char[] { ':', ',' });  //to split many char like { =,&} we take char array
            //      // string last = tokens[tokens.Length - 1];
            //if (PaytmInputStr.Length >= 0)
            //{
            //elog.LogData("PaytmInputStr :" + PaytmInputStr.Length + "Policy Number :" + request.ToString() + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Inside Try ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

            //PolcyNoinput = PaytmInputStr[1];
            //ClientId = PaytmInputStr[3];
            //Request_ID = PaytmInputStr[5];
            //MobileNo = PaytmInputStr[7];
            //EMail = PaytmInputStr[9];
            //VendorName = PaytmInputStr[11];
            //PC = PaytmInputStr[13];
            //checksum = PaytmInputStr[15];
            PolcyNoinput = input.PolcyNoi;
            // DOB = DOBi;
            Request_ID = input.Request_IDi;
            ClientId = input.ClientIdi;
            MobileNo = input.MobileNoi;
            EMail = input.EMaili;
            VendorName = input.VendorNamei;
            PC = input.PCi;
            checksum = input.checksumi;
            //SUBS_ID = SUBS_IDi;
            //MID = MIDi;
            // TXNID = TXNIDi;
            //ORDERID = ORDERIDi;
            BANKTXNID = input.BANKTXNIDi;
            TXNAMOUNT = input.PayableAmounti;
            CURRENCY = input.CURRENCYi;
            STATUS = input.STATUSi;
            //RESPCODE = RESPCODEi;
            RESPMSG = input.RESPMSGi;
            TXNDATE = input.TXNDATEi;
            GATEWAYNAME = input.GATEWAYNAMEi;
            BANKNAME = input.BANKNAMEi;
            //PAYMENTMODE = PAYMENTMODEi;
            TRAN_Type = input.TRAN_Typei;
            // PROMO_STATUS = PROMO_STATUSi;
            // PROMO_RESPCODE = PROMO_RESPCODEi;
            CHECKSUMHASH = input.checksumi;
            //   MERC_UNQ_REF = MERC_UNQ_REFi;
            //  TransactionIDPremiumPayment = TransactionIDPremiumPaymenti;
            //  TransactionIDReinstatement = TransactionIDReinstatementi;
            PayableAmount = input.PayableAmounti;

            //}
            ////string inputstr = "";
            ////if (PC == "29")
            ////    inputstr = "" + PolcyNoinput + "|" + DOB + "|" + Request_ID + "|" + MobileNo + "|" + EMail + "|" + VendorName + "|" + PC + "|" + SUBS_ID + "|" + MID + "|" + TXNID + "|" + ORDERID + "|" + BANKTXNID + "|" + TXNAMOUNT + "|" + CURRENCY + "|" + STATUS + "|" + RESPCODE + "|" + RESPMSG + "|" + TXNDATE + "|" + GATEWAYNAME + "|" + BANKNAME + "|" + PAYMENTMODE + "|" + PROMO_CAMP_ID + "|" + PROMO_STATUS + "|" + PROMO_RESPCODE + "|" + CHECKSUMHASH + "|" + MERC_UNQ_REF + "|" + TransactionIDPremiumPayment + "|" + TransactionIDReinstatement + "|" + PayableAmount + "|";
            ////else
            ////    inputstr = "" + PolcyNoinput + "|" + DOB + "|" + Request_ID + "|" + MobileNo + "|" + EMail + "|" + VendorName + "|" + PC + "|" + SUBS_ID + "|" + MID + "|" + TXNID + "|" + ORDERID + "|" + BANKTXNID + "|" + TXNAMOUNT + "|" + CURRENCY + "|" + STATUS + "|" + RESPCODE + "|" + RESPMSG + "|" + TXNDATE + "|" + GATEWAYNAME + "|" + BANKNAME + "|" + PAYMENTMODE + "|" + PROMO_CAMP_ID + "|" + PROMO_STATUS + "|" + PROMO_RESPCODE + "|" + CHECKSUMHASH + "|" + MERC_UNQ_REF + "|" + TransactionIDPremiumPayment + "|" + TransactionIDReinstatement + "|" + PayableAmount + "|";

            ////string str = ComputeHashData.CalculateMD5Hash(inputstr);
            //int cnt = 0;
            //if ((MobileNo != "" && EMail != "") && PC == "7")
            //{
            //   cnt = 1;
            //}
            //else if((MobileNo == "" && EMail == "") && PC == "5")
            //{
            //    cnt = 1;
            //}
            //else if ((MobileNo != "" && EMail == "") && PC == "6")
            //{
            //    cnt = 1;
            //}
            //else if ((MobileNo == "" && EMail != "") && PC == "6")
            //{
            //    cnt = 1;
            //}
            //else
            //{
            //    cnt = 0;
            //}
            //if (cnt == 1)
            //{
            string inputstr = "";
            if (PC == "7")
                inputstr = "" + PolcyNoinput + "|" + ClientId + "|" + Request_ID + "|" + MobileNo + "|" + EMail + "|" + VendorName + "|" + PC + "|";
            else
                inputstr = "" + PolcyNoinput + "|" + ClientId + "|" + Request_ID + "|" + MobileNo + "|" + EMail + "|" + VendorName + "|" + PC + "|";

            string str = ComputeHashData.CalculateMD5Hash(inputstr);
            elog.LogData("File:Website PolicyUseInfo.cs:, Fuction: checksum:" + str + ", inputstr:" + inputstr + "", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            Policyinput1.GetallDetailsResult = objPay;
            if (ComputeHashData.ValidateMD5HashData(inputstr, checksum) == true)
            {
                try
                {
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Inside Try ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    //if (Thread.CurrentThread.CurrentCulture.Name == "en-US")
                    //   ViewState["CultureName"] = "/en-US";
                    //else if (Thread.CurrentThread.CurrentCulture.Name == "hi-IN")
                    //    ViewState["CultureName"] = "/hi-IN";
                    //HttpCookie reqCookies = HttpContext.Current.Request.Cookies["userInfo"];
                    //if (reqCookies != null)
                    //{
                    //    ClientId = reqCookies["ClientID"].ToString();
                    //}

                    if (ClientId != null)
                    {
                        DataSet dsMyPolicies = getClientPolicyDetails(ClientId);





                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message: Client Id not null and getClientPolicyDetails Success Client Id value :  " + Convert.ToString(dsMyPolicies.Tables[0].Rows[0]["Client_ID"]), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        if (ClientId == Convert.ToString(dsMyPolicies.Tables[0].Rows[0]["Client_ID"]))
                        {
                            elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message: Client Id from service and session are same", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                            DataView dv = dsMyPolicies.Tables[0].DefaultView;
                            if (dv != null && dv.Count > 0)
                            {
                                #region[Assign Value]
                                elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message: dv not null" + Convert.ToString(dv.Count), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                for (int i = 0; i < dv.Count; i++)
                                {
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message: dv data Values :PolicyHolderName :" + Convert.ToString(dv[i].Row["PolicyHolderName"]) + "PolicyNumber :" + Convert.ToString(dv[i].Row["PolicyNumber"]) + "ProductName :" + Convert.ToString(dv[i].Row["ProductName"]), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    elog.LogData("session policy number :" + PolcyNoinput, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    elog.LogData("dv policy number :" + dv[i].Row["PolicyNumber"].ToString(), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    if (dv[i].Row["PolicyNumber"].ToString() == PolcyNoinput)
                                    {
                                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message: Policy Number from dv and session are same", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                        lblCurrentDateVal = DateTime.Now.ToString("dd/MM/yyyy");
                                        lblPolicyHolderNameVal = dv[i].Row["PolicyHolderName"].ToString();
                                        lblPolicyNoVal = dv[i].Row["PolicyNumber"].ToString(); //dv[0].Row["PolicyNumber"].ToString();//PolcyNoinput;
                                        strProductName = dv[i].Row["ProductName"].ToString();
                                        strPaymentFrequency = dv[i].Row["Existing_Mode"].ToString();
                                        /*BRS Mail - Added to get Due Date 19 Jun 2014 Starts here*/
                                        DateTime dtmDueDate = Convert.ToDateTime(dv[i].Row["NextPremiumDueDate"]);
                                        NextPremiumDueDate = dtmDueDate.Date.ToString("dd.MM.yyyy");
                                        // if (lblPolicyNoVal == "")
                                        //    lblPolicyNoVal = PolcyNoinput;
                                        break;
                                    }
                                    // elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message: dv data Values :PolicyHolderName :" + Convert.ToString(dv[i].Row["PolicyHolderName"]) + "PolicyNumber :" + Convert.ToString(dv[i].Row["PolicyNumber"]) + "ProductName :" + Convert.ToString(dv[i].Row["ProductName"]), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


                                }
                                #endregion


                                #region Paytm Response Parameter check

                                //if (!string.IsNullOrEmpty(SUBS_ID))
                                //{
                                //    strmsgPaytmSUBS_ID = SUBS_ID;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strmsgPaytmSUBS_ID :" + Convert.ToString(strmsgPaytmSUBS_ID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                //if (!string.IsNullOrEmpty(MID))
                                //{
                                //    strMsgPaytmMID = MID;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmMID :" + Convert.ToString(strMsgPaytmMID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                //if (!string.IsNullOrEmpty(TXNID))
                                //{
                                //    strMsgPaytmTXNID = TXNID;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmTXNID :" + Convert.ToString(strMsgPaytmTXNID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                //if (!string.IsNullOrEmpty(ORDERID))
                                //{
                                //    strMsgPaytmORDERID = ORDERID;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmORDERID :" + Convert.ToString(strMsgPaytmORDERID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                if (!string.IsNullOrEmpty(BANKTXNID))
                                {
                                    strMsgPaytmBANKTXNID = BANKTXNID;
                                    lblTransactionIDValueN = strMsgPaytmBANKTXNID;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmBANKTXNID :" + Convert.ToString(strMsgPaytmBANKTXNID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                if (!string.IsNullOrEmpty(TXNAMOUNT))
                                {
                                    strMsgPaytmTXNAMOUNT = TXNAMOUNT;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmTXNAMOUNT :" + Convert.ToString(strMsgPaytmTXNAMOUNT), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                if (!string.IsNullOrEmpty(CURRENCY))
                                {
                                    strMsgPaytmCURRENCY = CURRENCY;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmCURRENCY :" + Convert.ToString(strMsgPaytmCURRENCY), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                if (!string.IsNullOrEmpty(RESPMSG))
                                {
                                    strMsgPaytmSTATUS = RESPMSG.ToUpper();
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmSTATUS :" + Convert.ToString(strMsgPaytmSTATUS), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                //if (!string.IsNullOrEmpty(RESPCODE))
                                //{
                                //    strMsgPaytmRESPCODE = RESPCODE;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmRESPCODE:" + Convert.ToString(strMsgPaytmRESPCODE), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                if (!string.IsNullOrEmpty(STATUS))
                                {
                                    strMsgPaytmRESPMSG = STATUS;
                                    //strMsgPaytmSTATUS = RESPMSG;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmRESPMSG :" + Convert.ToString(strMsgPaytmRESPMSG), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                if (!string.IsNullOrEmpty(TXNDATE))
                                {
                                    strMsgPaytmTXNDATE = TXNDATE;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmTXNDATE :" + Convert.ToString(strMsgPaytmTXNDATE), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                if (!string.IsNullOrEmpty(TXNAMOUNT))
                                {
                                    strMsgPaytmTXNAMOUNT = TXNAMOUNT;
                                    lblPremiumAmountValue = strMsgPaytmTXNAMOUNT;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmTXNAMOUNT :" + Convert.ToString(strMsgPaytmTXNAMOUNT), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                if (!string.IsNullOrEmpty(GATEWAYNAME))
                                {
                                    strMsgPaytmGATEWAYNAME = GATEWAYNAME;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmGATEWAYNAME :" + Convert.ToString(strMsgPaytmGATEWAYNAME), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                if (!string.IsNullOrEmpty(BANKNAME))
                                {
                                    strMsgPaytmBANKNAME = BANKNAME;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmBANKNAME :" + Convert.ToString(strMsgPaytmBANKNAME), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                //if (!string.IsNullOrEmpty(PAYMENTMODE))
                                //{
                                //    strMsgPaytmPAYMENTMODE = PAYMENTMODE;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmPAYMENTMODE :" + Convert.ToString(strMsgPaytmPAYMENTMODE), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                if (!string.IsNullOrEmpty(TRAN_Type))
                                {
                                    strMsgPaytmPROMO_CAMP_ID = TRAN_Type;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmPROMO_CAMP_ID :" + Convert.ToString(strMsgPaytmPROMO_CAMP_ID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                //if (!string.IsNullOrEmpty(PROMO_STATUS))
                                //{
                                //    strMsgPaytmPROMO_STATUS = PROMO_STATUS;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmPROMO_STATUS :" + Convert.ToString(strMsgPaytmPROMO_STATUS), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                //if (!string.IsNullOrEmpty(PROMO_RESPCODE))
                                //{
                                //    strMsgPaytmPROMO_RESPCODE = PROMO_RESPCODE;
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmPROMO_RESPCODE :" + Convert.ToString(strMsgPaytmPROMO_RESPCODE), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                if (!string.IsNullOrEmpty(CHECKSUMHASH))
                                {
                                    strMsgPaytmCHECKSUMHASH = CHECKSUMHASH;
                                    elog.LogData("Client ID :" + ClientId + "Checksum VALUE=" + strMsgPaytmCHECKSUMHASH + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmCHECKSUMHASH :" + Convert.ToString(strMsgPaytmCHECKSUMHASH), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                }
                                //if (!string.IsNullOrEmpty(MERC_UNQ_REF))
                                //{
                                //    string strMsgPaytmAdditionalInfo = MERC_UNQ_REF;
                                //    string[] strMsgPaytmAdditionalInfoarr = new string[3];
                                //    strMsgPaytmAdditionalInfoarr = strMsgPaytmAdditionalInfo.Split('|');
                                //    strMsgPaytmAdditionalInfo1 = strMsgPaytmAdditionalInfoarr[0];
                                //    strMsgPaytmAdditionalInfo2 = strMsgPaytmAdditionalInfoarr[1];

                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response value for strMsgPaytmAdditionalInfo1 :" + Convert.ToString(strMsgPaytmAdditionalInfo1) + "strMsgPaytmAdditionalInfo2 :" + Convert.ToString(strMsgPaytmAdditionalInfo2), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //}
                                //  strResponsemsgPaytm = Convert.ToString(strmsgPaytmSUBS_ID) + "|" + Convert.ToString(strMsgPaytmMID) + "|" + Convert.ToString(strMsgPaytmTXNID) + "|" + Convert.ToString(strMsgPaytmORDERID) + "|" + Convert.ToString(strMsgPaytmBANKTXNID) + "|" + Convert.ToString(strMsgPaytmTXNAMOUNT) + "|" + Convert.ToString(strMsgPaytmCURRENCY) + "|" + Convert.ToString(strMsgPaytmSTATUS) + "|" + Convert.ToString(strMsgPaytmRESPCODE) + "|" + Convert.ToString(strMsgPaytmRESPMSG) + "|" + Convert.ToString(strMsgPaytmTXNDATE) + "|" + Convert.ToString(strMsgPaytmGATEWAYNAME) + "|" + Convert.ToString(strMsgPaytmBANKNAME) + "|" + Convert.ToString(strMsgPaytmPAYMENTMODE) + "|" + Convert.ToString(strMsgPaytmPROMO_CAMP_ID) + "|" + Convert.ToString(strMsgPaytmPROMO_STATUS) + "|" + Convert.ToString(strMsgPaytmPROMO_RESPCODE) + "|" + Convert.ToString(strMsgPaytmCHECKSUMHASH) + "|" + Convert.ToString(strMsgPaytmAdditionalInfo1) + "|" + Convert.ToString(strMsgPaytmAdditionalInfo2);

                                strResponsemsgPaytm = Convert.ToString(ClientId) + "|" + Convert.ToString(PolcyNoinput) + "|" + Convert.ToString(BANKTXNID) + "|" + Convert.ToString(Request_ID) + "|" + Convert.ToString(strMsgPaytmTXNAMOUNT) + "|" + Convert.ToString(strMsgPaytmCURRENCY) + "|" + Convert.ToString(strMsgPaytmSTATUS) + "|" + Convert.ToString(strMsgPaytmRESPMSG) + "|" + Convert.ToString(strMsgPaytmTXNDATE) + "|" + Convert.ToString(strMsgPaytmGATEWAYNAME) + "|" + Convert.ToString(strMsgPaytmBANKNAME) + "|" + Convert.ToString(strMsgPaytmPROMO_CAMP_ID) + "|" + Convert.ToString(strMsgPaytmCHECKSUMHASH) + "|" + Convert.ToString(PC);
                                //strResponsemsgPaytm = Convert.ToString(strmsgPaytmSUBS_ID) + "|" + Convert.ToString(strMsgPaytmMID) + "|" + Convert.ToString(strMsgPaytmTXNID) + "|" + Convert.ToString(strMsgPaytmORDERID) + "|" + Convert.ToString(strMsgPaytmBANKTXNID) + "|" + Convert.ToString(strMsgPaytmTXNAMOUNT) + "|" + Convert.ToString(strMsgPaytmCURRENCY) + "|" + Convert.ToString(strMsgPaytmSTATUS) + "|" + Convert.ToString(strMsgPaytmRESPCODE) + "|" + Convert.ToString(strMsgPaytmRESPMSG) + "|" + Convert.ToString(strMsgPaytmTXNDATE) + "|" + Convert.ToString(strMsgPaytmTXNAMOUNT) + "|" + Convert.ToString(strMsgPaytmGATEWAYNAME) + "|" + Convert.ToString(strMsgPaytmBANKNAME) + "|" + Convert.ToString(strMsgPaytmPAYMENTMODE) + "|" + Convert.ToString(strMsgPaytmPROMO_CAMP_ID) + "|" + Convert.ToString(strMsgPaytmPROMO_STATUS) + "|" + Convert.ToString(strMsgPaytmPROMO_RESPCODE) + "|" + Convert.ToString(strMsgPaytmCHECKSUMHASH) ;
                                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: OnLoad" + "Message: Response for strResponsemsgPaytm 391:" + Convert.ToString(strResponsemsgPaytm), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                #endregion

                                //Add else if condition to process paytm transaction
                                string strMsg = string.Empty;
                                //if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["msg"]))
                                //{
                                //    strMsg = HttpContext.Current.Request.Form["msg"];
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Payment Getway message for Billdesk response website :" + Convert.ToString(strMsg), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //    BillDeskTransaction(strMsg);
                                //}
                                // else 
                                if (!string.IsNullOrEmpty(strResponsemsgPaytm))
                                {
                                    bool checksumValid;
                                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Payment Getway message for Paytm response website :" + Convert.ToString(strResponsemsgPaytm), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    checksumValid = true;
                                    #region Paytm Checksum Verification
                                    //Check response send by paytm and verify checksum

                                    //Dictionary<string, string> parameters = new Dictionary<string, string>();
                                    //string paytmChecksum = "";
                                    ////bool checksumValid;
                                    //string merchantKey = ConfigurationManager.AppSettings["MarchentKey"];// "bnWMJQA0P88Yt1vh";
                                    //foreach (string key in HttpContext.Current.Request.Form.Keys)
                                    //{
                                    //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Inside Paytm checksum", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    //    parameters.Add(key.Trim(), HttpContext.Current.Request.Form[key].Trim());
                                    //    elog.LogData("key.Trim() :" + key.Trim() + "Request.Form[key] :" + HttpContext.Current.Request.Form[key].Trim(), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    //}
                                    //if (parameters.ContainsKey("CHECKSUMHASH"))
                                    //{
                                    //    paytmChecksum = parameters["CHECKSUMHASH"];
                                    //    elog.LogData("paytmChecksum:" + paytmChecksum, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    //    parameters.Remove("CHECKSUMHASH");
                                    //}
                                    //if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum)==true)
                                    //{
                                    // string inputstr = "";


                                    if (PC == "7")
                                        inputstr = "" + PolcyNoinput + "|" + ClientId + "|" + Request_ID + "|" + MobileNo + "|" + EMail + "|" + VendorName + "|" + PC + "|";
                                    else
                                        inputstr = "" + PolcyNoinput + "|" + ClientId + "|" + Request_ID + "|" + MobileNo + "|" + EMail + "|" + VendorName + "|" + PC + "|";

                                    str = ComputeHashData.CalculateMD5Hash(inputstr);
                                    elog.LogData("File:Website PolicyUseInfo.cs:, Fuction: checksum:" + str + ", inputstr:" + inputstr + "", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                                    if (ComputeHashData.ValidateMD5HashData(inputstr, checksum) == true)
                                    {
                                        checksumValid = true;
                                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Checksum is Valid" + Convert.ToString(checksumValid), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    }
                                    else
                                    {
                                        checksumValid = false;
                                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Checksum is InValid" + Convert.ToString(checksumValid), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                    }
                                    #endregion

                                    if (checksumValid == true)
                                    {

                                        try
                                        {
                                            lblTransactionIDValue = GetTransactionID();

                                            if (Convert.ToString(lblTransactionIDValueN) != "" && Convert.ToString(lblTransactionIDValue) != "")
                                            {
                                                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: Submit button" + "Message: Inside Submit button", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                                string strPaymentType = PaymentType;
                                                string policyInfo = lblPolicyNoVal + "|" + lblPremiumAmountValue + "|" + strPaymentType + "|" + lblTransactionIDValue;
                                                // Session["policyInfo"] = policyInfo;

                                                // crm neeraj

                                                string CRMResponse = "";// objservaccess.CrmCallCreateupdationandsave_payment(input.TRAN_Typei, input.TRAN_Typei + "-payment", true, lblPolicyNoVal, policyInfo, strResponsemsgPaytm,input.ClientIdi);

                                                // crm neeraj
                                                UserRegDetails userRegDetails = new UserRegDetails();
                                                CRMId = CRMResponse;
                                                PaytmTransaction(strResponsemsgPaytm);
                                                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: Submit button" + "Message: function call PaytmTransaction :" + policyInfo, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                                //GeneratePdf(policyInfo);

                                            }
                                            else
                                            {
                                                objPay.AuthStatus = "N";
                                                objPay.ErrorStatus = "0";
                                                objPay.StatusMsg = "FAILURE";
                                            }
                                            // resultstr="Y";
                                        }
                                        catch (Exception ex)
                                        {
                                            //ExceptionPolicy.HandleException(ex, ConfigurationManager.AppSettings["DaichiUIPolicy"].ToString());
                                            elog.LogData("btnSubmit_Click catch" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: Submit button" + "Message:Inside Submit Catch:" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                            // HttpContext.Current.Response.Redirect(ViewState["CultureName"].ToString() + "/Pages/ErrorReport.aspx?ErrorCode=1.1");
                                            // resultstr = "N";
                                            objPay.AuthStatus = "N";
                                            objPay.ErrorStatus = "0";
                                            objPay.StatusMsg = "FAILURE";// strMsgPaytmSTATUS;
                                        }
                                    }
                                }
                                //else if (!string.IsNullOrEmpty(strmesgforBOI))
                                //{
                                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Payment Getway message for BOI response website :" + Convert.ToString(strmesgforBOI), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                                //    BOIProcessTransaction(strmesgforBOI);
                                //}
                            }
                        }

                    }



                }

                catch (Exception ex)
                {
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File:Website PremiumPaymentGatewayConfirmation.cs Function : OnLoad Message:Inside onload catch:" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    //  ExceptionPolicy.HandleException(ex, ConfigurationManager.AppSettings["DaichiUIPolicy"].ToString());
                    //  HttpContext.Current.Response.Redirect(ViewState["CultureName"].ToString() + "/Pages/ErrorReport.aspx?ErrorCode=1.1");

                }
                // return resultstr;

            }
            else
            {
                // strMsgPaytmRESPMSG = "Invalid Checksum";
                objPay.StatusMsg = "Invalid Checksum";
                // return resultstr;
            }
            //}
            //else
            //{
            //    // strMsgPaytmRESPMSG = "Invalid Checksum";
            //    Policyinput1.StatusMsg = "Invalid Parameter Count";
            //    // return resultstr;
            //}
            // return resultstr;
            //   Policyinput1.StatusMsg = strMsgPaytmSTATUS;
            return Policyinput1;
        }
        //}

        public void UpdateProcCall()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString());
            SqlCommand cmd = new SqlCommand("Proc_UpdateValidatePolicyNo_Master", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Request_ID", Request_ID);
            cmd.Parameters.AddWithValue("@Client_Id", ClientId);
            cmd.Parameters.AddWithValue("@Policy_Number", PolcyNoinput);


            //SqlParameter p2 = cmd.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 50);
            //p2.Value = txtMobileNo.Text;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        #region Paytm Transaction Method

        /*Get Mobile No fro fact client */
        public static string GetPolicyStatus(string Policyno)
        {
            DataTable dt = new DataTable();
            string rtnStrPStatus = "";
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(sqlConnectionString);
            cn.Open();
            cmd = new SqlCommand("pro_GetPolicyStatus", cn);
            cmd.Parameters.Add("@Policyno", SqlDbType.VarChar, 10).Value = Convert.ToString(Policyno);//"00000002";

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            cn.Close();

            if (dt != null)
            {
                //elog.LogData("ClientID :" + Convert.ToString(Session["ClientID"]) + "File:Website UpdateCustomerContact_Detail.cs:, Fuction: Inside try" + "Message: dt not null", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                if (dt.Rows.Count > 0)
                {

                    //  elog.LogData("Client ID  before ActionReinstatement:" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: Inside ReinstatementServiceCall Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


                    rtnStrPStatus = dt.Rows[0]["Premium_Status"].ToString();


                }
            }
            // elog.LogData("ClientID :" + Convert.ToString(Session["ClientID"]) + "File:Website UpdateCustomerContact_Detail.cs:, Fuction: Inside try" + "Message:dt null", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            return rtnStrPStatus;

        }
        /*end Get Mobile No fro fact client*/
        private void PaytmTransaction(string strResponsemsgPaytm)
        {

            #region[To avoid Refresh entry]
            //if (HttpContext.Current.Session["RefreshClickChk"] != null)
            //{
            //    if (HttpContext.Current.Session["RefreshClickChk"].ToString() == strResponsemsgPaytm)
            //    {
            if (strMsgPaytmBANKTXNID.ToString() != null)
                lblTransactionIDValueN = strMsgPaytmBANKTXNID.ToString();
            if (PayableAmount.ToString() != null)
                lblPremiumAmountValue = PayableAmount.ToString();
            //        strResponsemsgPaytm = null;

            //}
            //else
            //{
            //   // HttpContext.Current.Session["RefreshClickChk"] = null;
            //}
            //}
            #endregion
            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside PaytmTransaction Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            string paymentGatewayResult = string.Empty;
            string[] splitmsg = new string[26];
            string source = string.Empty;
            string destination = string.Empty;

            #region[To avoid Refresh entry]
            //if (HttpContext.Current.Session["RefreshClickChk"] != null)
            //{



            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside if Session[RefreshClickChk]", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            if (strResponsemsgPaytm != "")
            {
                splitmsg = strResponsemsgPaytm.Split('|');
                //  string paymentType = splitmsg[18];
                string paymentType = splitmsg[11];
                //if (paymentType == "Revival Payment")
                if (paymentType == "Revival")
                    paymentType = "Reinstatement";
                else if (paymentType == "Premium")
                    paymentType = "Premium Payment";


                strPaymentType = paymentType;

                if (paymentType == "Premium Payment")
                {
                    if (strMsgPaytmBANKTXNID != null)
                        lblTransactionIDValueN = strMsgPaytmBANKTXNID;
                    if (PayableAmount != null)
                        lblPremiumAmountValue = PayableAmount;
                }
                else if (paymentType == "Reinstatement")
                {
                    //PolicyStatus = GetPolicyStatus(PolicyNumber);
                    if (strMsgPaytmBANKTXNID != null)
                        lblTransactionIDValueN = strMsgPaytmBANKTXNID.ToString();
                    if (PayableAmount != null)
                        lblPremiumAmountValue = PayableAmount.ToString();
                }
                //strResponsemsgPaytm = null;
            }
            //}
            //else
            //{
            //    HttpContext.Current.Session["RefreshClickChk"] = null;
            //}
            //  }
            #endregion

            if (!string.IsNullOrEmpty(strResponsemsgPaytm))
            {
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside PaytmTransaction Response for strResponsemsgPaytm :" + Convert.ToString(strResponsemsgPaytm), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                splitmsg = strResponsemsgPaytm.Split('|');
                // string paymentType = splitmsg[18];
                string paymentType = splitmsg[11];
                if (paymentType == "Revival")
                    paymentType = "Reinstatement";
                else if (paymentType == "Premium")
                    paymentType = "Premium Payment";

                string Orderidcheck = string.Empty;

                if (paymentType == "Premium Payment")
                {
                    if (strMsgPaytmBANKTXNID != null)
                    {
                        Orderidcheck = strMsgPaytmBANKTXNID;//"123456780";//TransactionIDPremiumPayment.ToString();
                        TransactionIDPremiumPayment = lblTransactionIDValue;
                    }
                }
                else if (paymentType == "Reinstatement")
                {
                    // PolicyStatus = GetPolicyStatus(PolicyNumber);
                    if (strMsgPaytmBANKTXNID.ToString() != null)
                    {

                        TransactionIDReinstatement = lblTransactionIDValue;
                        Orderidcheck = strMsgPaytmBANKTXNID;
                    }
                }

                if (strMsgPaytmBANKTXNID.Equals(Orderidcheck) && strMsgPaytmTXNAMOUNT.Equals(Convert.ToString(PayableAmount)) && strMsgPaytmSTATUS.Equals("SUCCESS"))
                {
                    paymentGatewayResult = "PASS";
                    elog.LogData("Client ID 673:" + ClientId + "Policy Number :" + lblPolicyNoVal + "" + strMsgPaytmBANKTXNID.Equals(Orderidcheck) + "," + strMsgPaytmTXNAMOUNT.Equals(Convert.ToString(PayableAmount)) + "," + strMsgPaytmSTATUS.Equals("SUCCESS") + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Pass" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    elog.LogData("Orderidcheck 674:" + Orderidcheck + ",strMsgPaytmBANKTXNID :" + strMsgPaytmBANKTXNID + ",strMsgPaytmTXNAMOUNT :" + strMsgPaytmTXNAMOUNT + ",PayableAmount:" + PayableAmount + ",strMsgPaytmSTATUS : " + strMsgPaytmSTATUS + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Pass" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                }
                else
                {
                    paymentGatewayResult = "FAIL";
                    elog.LogData("Client ID 680:" + ClientId + "Policy Number :" + lblPolicyNoVal + " " + strMsgPaytmBANKTXNID.Equals(Orderidcheck) + "," + strMsgPaytmTXNAMOUNT.Equals(Convert.ToString(PayableAmount)) + "," + strMsgPaytmSTATUS.Equals("SUCCESS") + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Fail :" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    elog.LogData("Orderidcheck 681:" + Orderidcheck + ",strMsgPaytmBANKTXNID :" + strMsgPaytmBANKTXNID + ",strMsgPaytmTXNAMOUNT :" + strMsgPaytmTXNAMOUNT + ",PayableAmount:" + PayableAmount + ",strMsgPaytmSTATUS : " + strMsgPaytmSTATUS + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Pass" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                }

                lblPaymentGatewayResultValue = paymentGatewayResult;
                bool individualInsertionFlag = true;
                bankName = splitmsg[10];
                paymentCode = "PPI";//splitmsg[11];

                strPaymentType = paymentType;
                if (paymentType == "Premium Payment")
                {
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside Premium Payment PaymentType" + paymentType, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    string stResponse = string.Empty;
                    string PremiumStatusMsg = string.Empty;

                    if (strMsgPaytmBANKTXNID != null)
                        lblTransactionIDValueN = strMsgPaytmBANKTXNID.ToString();
                    if (PayableAmount != null)
                        lblPremiumAmountValue = PayableAmount.ToString();

                    source = "Portal";
                    destination = "Paytm";
                    paymentGateway = "Paytm";
                    currentStatus = "PToG";

                    if (paymentGatewayResult == "PASS")
                    {
                        PremiumPaymentTransationInsertion("PremiumPayment", strResponsemsgPaytm, paymentGatewayResult, source, destination, individualInsertionFlag, bankName, paymentCode, paymentGateway, currentStatus);
                        elog.LogData("paymentCode :" + paymentCode + "Policy Number 706:" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message:'Portal to Paytm' entry successfull", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    }
                    if (paymentGatewayResult == "PASS")
                    {
                        lblPaymentGatewayResultValue = ConfigurationManager.AppSettings["PremiumPaymentTransactionSucess"];
                        //PayType = "P";
                        PayType = ConfigurationManager.AppSettings["PayType_PayTM"];

                        // neeraj
                        stResponse = ""; PremiumPaymentServiceCall(out PremiumStatusMsg);
                        //
                        
                        ////  btnSubmit.Visible = true;

                        source = "Portal";
                        destination = "Biztalk";
                        individualInsertionFlag = false;
                        lblReinstatement = "Payment Gateway Confirmation: Successful";
                        PremiumPaymentTransationInsertion("PremiumPayment", stResponse, PremiumStatusMsg, source, destination, individualInsertionFlag, bankName, paymentCode, paymentGateway, currentStatus);
                        elog.LogData("paymentCode :" + paymentCode + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message:'Portal to Biztalk' entry successfull", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    }
                    else
                    {
                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Fail No Entry to database" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        lblReinstatement = "Payment Gateway Confirmation: Un-successful";
                        lblPaymentGatewayResultValue = ConfigurationManager.AppSettings["PremiumPaymentTransactionFail"];
                        lblTransactionIDValueN = "";
                        // btnSubmit.Visible = false;
                        TransactionFailErrorMsgInsertion(paymentType, strResponsemsgPaytm, paymentGateway);
                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Error Message insertion entry made to database", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    }
                }
                else if (paymentType == "Reinstatement")
                {
                    //neeraj

                    DataView dv = null;
                    DataView dvClientData = null;
                    DataView dvRevival = null;
                    DataAccess objGetPaymentD = new DataAccess();
                    DataSet dsMyPolicies = objGetPaymentD.GetBiztalkPolicyDetails(ClientId);  //objSwitch.GetDataSet(new XmlSerializer(typeof(RetriveServiceOutput)), objPolicyDetailsOutput);


                    if (dsMyPolicies != null & dsMyPolicies.Tables[0].Rows.Count > 0)
                    {

                        //        HttpContext.Current.Session["MyPolicies"] = dsMyPolicies;
                        dv = dsMyPolicies.Tables[0].DefaultView;
                        dvClientData = dsMyPolicies.Tables[0].DefaultView;
                        dvRevival = dsMyPolicies.Tables[0].DefaultView;
                    }


                    if (!string.IsNullOrEmpty(dv[0].Row["NextPremiumDueDate"].ToString()))
                    {
                        DateTime dtNextPremiumDueDate = Convert.ToDateTime(dv[0].Row["NextPremiumDueDate"].ToString());
                        //       lblPremiumDueDateVal = DateTime.Parse(Convert.ToString(dv[0].Row["NextPremiumDueDate"])).ToString("dd/MM/yyyy");
                        //     HttpContext.Current.Session["NextPremiumDueDatee"] = Convert.ToDateTime(dv[0].Row["NextPremiumDueDate"].ToString());
                        //      strnewlblPremiumDueDateVal = DateTime.Parse(Convert.ToString(dv[0].Row["NextPremiumDueDate"])).ToString("yyyy-MM-dd");
                        DateTime dtCurrentDate = DateTime.Now;
                        //Added on 29-09-10------------
                        //DateTime ExpectedDuedate = dtNextPremiumDueDate.AddMonths(6);
                        //DateTime ExpectedDuedate = dtNextPremiumDueDate.AddMonths(Convert.ToInt32(ConfigurationManager.AppSettings["RevivalPeriod"]));
                        //////------------------
                        //if (dtCurrentDate <= ExpectedDuedate)
                        //{
                        //revival 180 days removed instead that added below msg and link 4/1/2019 by Shri
                        DateTime ExpectedDuedateNew = dtNextPremiumDueDate.AddMonths(6);
                        //elog.LogData("ExpectedDuedateNew:" + ExpectedDuedateNew + "Policy Number :" + Convert.ToString(Session["policyNumber"]) + "File:Website PayOnlinePremiumPaymentNew.cs: Function :Revival Payment, Message:Inside Non Linked" + Convert.ToString(ProductCategory), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        if (dtCurrentDate > ExpectedDuedateNew)
                        {
                            //elog.LogData("ExpectedDuedateNew:" + ExpectedDuedateNew + "Policy Number :" + Convert.ToString(Session["policyNumber"]) + "File:Website PayOnlinePremiumPaymentNew.cs: Function :Revival Payment, Message:Inside Non Linked" + Convert.ToString(ProductCategory), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                            Revival180 = 1;

                            //elog.LogData("ExpectedDuedateNew:" + lblRevivalcase + "Policy Number :" + Convert.ToString(Session["policyNumber"]) + "File:Website PayOnlinePremiumPaymentNew.cs: Function :Revival Payment, Message:Inside Non Linked" + Convert.ToString(ProductCategory), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


                        }
                        else
                        {
                            Revival180 = 0;

                        }





                        // }
                    }



                    #region --Revival Payment--
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside Revival Payment, PaymentType" + paymentType, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    string stServiceResponse = string.Empty;
                    string ReinstatementStatusMsg = string.Empty;
                    string PremiumStatusMsg = string.Empty;
                    if (TransactionIDReinstatement != null)
                        lblTransactionIDValueN = TransactionIDReinstatement.ToString();
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message:TransactionIDReinstatement not null  " + TransactionIDReinstatement.ToString(), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    if (PayableAmount != null)
                        lblPremiumAmountValue = PayableAmount.ToString();
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message:Revival PayableAmount not null" + PayableAmount.ToString(), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    source = "Portal";
                    destination = "Paytm";
                    paymentGateway = "Paytm";
                    currentStatus = "PToG";

                    individualInsertionFlag = true;
                    // Next one line is added on Sep2'2010 by Srinivas                                
                    if (paymentGatewayResult == "PASS")
                    {
                        ReinstatementTransactionInsertion("Reinstatement", strResponsemsgPaytm, paymentGatewayResult, source, destination, individualInsertionFlag, bankName, paymentCode, paymentGateway, currentStatus, CRMId);
                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message:'Portal to Paytm' entry successfull", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    }
                    //Call service
                    if (paymentGatewayResult == "PASS")
                    {
                        lblPaymentGatewayResultValue = ConfigurationManager.AppSettings["ReinstatementTransactionSucess"];
                        // PayType = "P";
                        PayType = ConfigurationManager.AppSettings["PayType_PayTM"];


                        //  stServiceResponse = ReinstatementServiceCall(out ReinstatementStatusMsg);

                        if (Revival180 == 1)
                        {
                            stServiceResponse = PremiumPaymentServiceCall(out PremiumStatusMsg);
                        }
                        else
                        {
                            stServiceResponse = ReinstatementServiceCall(out ReinstatementStatusMsg);
                        }


                        ////  btnSubmit.Visible = true;
                        //  //Insert into tansaction table
                        source = "Portal";
                        destination = "Biztalk";
                        individualInsertionFlag = false;
                        lblReinstatement = "Revival Payment: Successful";//Added on 07-Dec-2010  "Reinstatement Payment: Successful";//Added on 08-10-10
                        ReinstatementTransactionInsertion("Reinstatement", stServiceResponse, ReinstatementStatusMsg, source, destination, individualInsertionFlag, bankName, paymentCode, paymentGateway, currentStatus, CRMId);
                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message:'Portal to Biztalk' entry successfull", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    }
                    else
                    {
                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Fail No Entry to database" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        lblReinstatement = "Revival Payment: Un-successful";//Added on 07-Dec-2010  "Reinstatement Payment: Un-successful";//Added on 08-10-10
                        elog.LogData("After revival  " + Convert.ToString(lblReinstatement), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        lblPaymentGatewayResultValue = ConfigurationManager.AppSettings["ReinstatementTransactionFail"];
                        TransactionFailErrorMsgInsertion(paymentType, strResponsemsgPaytm, paymentGateway);
                        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Error Message insertion entry made to database", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        // Next one line is added on Sep2'2010 by Srinivas
                        lblTransactionIDValueN = "";
                        //btnSubmit.Visible = false;
                    }
                    #endregion
                }
                //  HttpContext.Current.Session["RefreshClickChk"] = strResponsemsgPaytm.ToString();
                elog.LogData("strResponsemsgPaytm 795 :" + strResponsemsgPaytm.ToString() + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Fail No Entry to database" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                if (paymentGatewayResult == "PASS")
                {
                    elog.LogData("strResponsemsgPaytm 799 :paymentGatewayResult:" + paymentGatewayResult + " = :" + paymentGatewayResult + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Fail No Entry to database" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    #region -- get Client Email and Mobile no--
                    string strUserEMail;
                    string strUserMobile;
                    if (EMail == "" && MobileNo == "")
                    {
                        string connectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["TransactionConnectingString"]);
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        sqlCon.Open();
                        string query = "select Mail_id,Mobile_No from Fact_Client_Details where Client_ID= '" + ClientId + "'";
                        DataTable dt = new DataTable();
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            strUserEMail = Convert.ToString(dt.Rows[0]["Mail_id"]);
                            strUserMobile = Convert.ToString(dt.Rows[0]["Mobile_No"]);
                        }
                        else
                        {
                            strUserEMail = "";
                            strUserMobile = "";
                        }
                        sqlCon.Close();
                        elog.LogData("strResponsemsgPaytm 825 :strUserEMail:" + strUserEMail + " strUserMobile :" + strUserMobile + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Fail No Entry to database" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    }
                    else
                    {
                        strUserEMail = EMail;
                        strUserMobile = MobileNo;
                        elog.LogData("strResponsemsgPaytm 834 :strUserEMail:" + strUserEMail + " strUserMobile :" + strUserMobile + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Inside paymentgatewayresult Fail No Entry to database" + Convert.ToString(paymentGatewayResult), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    }
                    //DataTable dtClientInfo = new DataTable();
                    //UserRegDetailsActions userDBO = new UserRegDetailsActions();
                    //dtClientInfo = userDBO.GetClientContactDetails(ClientId);

                    //string strUserEMail = dtClientInfo.Rows[0]["Email"].ToString(); // "shridevi.swami@sudlife.in";//
                    //string strUserMobile = dtClientInfo.Rows[0]["MobilePin"].ToString(); //"9758837347";//
                    #endregion
                    elog.LogData("strResponsemsgPaytm 844 :EMail:" + Convert.ToString(lblPolicyNoVal) + ", " + Convert.ToString(strProductName) + ", " + Convert.ToString(strPaymentFrequency) + ", " + Convert.ToString(paymentType) + ", " + Convert.ToString(lblPolicyHolderNameVal) + ", " + Convert.ToString(lblPremiumAmountValue) + ", " + Convert.ToString(NextPremiumDueDate) + ", " + Convert.ToString(lblTransactionIDValueN) + ", " + Convert.ToString(strUserMobile) + ", " + Convert.ToString(strUserEMail) + ", " + Convert.ToString(ConfigurationManager.AppSettings["MailSentFrom"]) + " strUserMobile :" + strUserMobile + "", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    //email neeraj

                    SendReceiptOnMail(ClientId, Convert.ToString(lblPolicyNoVal), Convert.ToString(strProductName), Convert.ToString(strPaymentFrequency), Convert.ToString(paymentType), Convert.ToString(lblPolicyHolderNameVal), Convert.ToString(lblPremiumAmountValue), Convert.ToString(NextPremiumDueDate), Convert.ToString(lblTransactionIDValueN), Convert.ToString(strUserMobile), Convert.ToString(strUserEMail), Convert.ToString(ConfigurationManager.AppSettings["MailSentFrom"]));
                    SendReceiptOnSMS(strUserMobile, lblPolicyNoVal, lblPremiumAmountValue);
                    EmailSMS_SentDetailsInsertion(strUserEMail, strUserMobile, SMS_Sent, emailsent, paymentType);

                    //email neeraj
                }




                //if (paymentGatewayResult == "PASS")
                //{
                //    #region -- get Client Email and Mobile no--

                //    string strUserEMail = "";
                //    string strUserMobile = "";
                //    string strAlternateEmailId =EMail;
                //    string strAlternateMobileNo = MobileNo;
                //    string registeredEmailId = RegisteredEmailId.ToString();
                //    string registeredMobileNo = RegisteredMobileNo.ToString();

                //    if (strAlternateEmailId != null && strAlternateEmailId != string.Empty)
                //    {
                //        strUserEMail = strAlternateEmailId;
                //        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Mail sent to Alternate Email:" + AlternateEmailId, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //    }
                //    else
                //    {
                //        strUserEMail = registeredEmailId;
                //        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Mail sent to Registered Email:" + registeredEmailId, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //    }
                //    if (strAlternateMobileNo != null && strAlternateMobileNo != string.Empty)
                //    {
                //        strUserMobile = strAlternateMobileNo;
                //        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Message sent to Alternate Mobile:" + AlternateMobileNo, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //    }
                //    else
                //    {
                //        strUserMobile = registeredMobileNo;
                //        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Message sent to Alternate Mobile:" + registeredMobileNo, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //    }
                //    if (strUserEMail != null)
                //    {
                //        SendReceiptOnMail(lblPolicyNoVal, strProductName, strPaymentFrequency, paymentType, lblPolicyHolderNameVal, lblPremiumAmountValue, NextPremiumDueDate, lblTransactionIDValue, strUserMobile, strUserEMail, ConfigurationManager.AppSettings["MailSentFrom"].ToString());
                //    }
                //    if (strUserMobile != null)
                //    {
                //        SendReceiptOnSMS(strUserMobile, lblPolicyNoVal, lblPremiumAmountValue);
                //    }
                //    //Code to store Email and SMS Sent Details added on 08/02/2016
                //    EmailSMS_SentDetailsInsertion(strAlternateEmailId, strAlternateMobileNo, registeredEmailId, registeredMobileNo, SMS_Sent, emailsent, paymentType);
                //    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PaytmTransaction Method" + "Message: Email SMS Sent details entry made to database ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //    #endregion
                //}
            }

        }
        #endregion

        private void PremiumPaymentTransationInsertion(string paymentType, string stResponse, string PremiumStatusMsg, string Sourse, string Destination, bool individualInsertionFlag, string bankName, string paymentCode, string paymentGateway, string currentStatus)
        {
            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentTransationInsertion Method" + "Message: Inside PremiumPaymentTransationInsertion Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            UserRegDetailsActions objUserRegDetailsAction = new UserRegDetailsActions();
            UserRegDetails objUserRegDetails = new UserRegDetails();

            objUserRegDetails.TransactionID = lblTransactionIDValue;
            objUserRegDetails.PolicyNumber = lblPolicyNoVal;
            if (ClientId != null)
                objUserRegDetails.ClientID = ClientId;
            else
                objUserRegDetails.ClientID = string.Empty;
            objUserRegDetails.TransactionDate = DateTime.Now;
            objUserRegDetails.TransactionType = paymentType + " APP";
            objUserRegDetails.TransactionComment = stResponse;
            objUserRegDetails.UpdatedBy = "PaytmApp";
            objUserRegDetails.UpdatedDateTime = DateTime.Now;
            objUserRegDetails.Source = Sourse;
            objUserRegDetails.Destination = Destination;
            objUserRegDetails.TransactionResult = PremiumStatusMsg;
            objUserRegDetails.PremiumDueAmount = Convert.ToDecimal(lblPremiumAmountValue);
            objUserRegDetails.PremiumAmount = Convert.ToDecimal(lblPremiumAmountValue);
            //to add paymentcode and bank name
            objUserRegDetails.BankName = bankName;
            objUserRegDetails.PaymentCode = paymentCode;
            objUserRegDetails.PaymentGateway = paymentGateway;
            objUserRegDetails.CurrentStatus = currentStatus;
            Existing_Mode = strPaymentFrequency;

            if (Existing_Mode != null)

                objUserRegDetails.ExistingMode = Existing_Mode.ToString();
            else
                objUserRegDetails.ExistingMode = string.Empty;
            string PremiumPaymentresult = string.Empty;
            PremiumPaymentresult = objUserRegDetailsAction.InsertGeneralTransactionDetails(objUserRegDetails);
            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentTransationInsertion Method" + "Message: Transaction_General table entry successful", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


            if ((PremiumPaymentresult == objUserRegDetails.TransactionID) && (individualInsertionFlag == true))
            {
                PremiumPaymentresult = objUserRegDetailsAction.InsertPremiumPaymentTransactionDetails(objUserRegDetails);
                UpdateProcCall();
                objPay.AuthStatus = "Y";
                objPay.ErrorStatus = "1";
                objPay.StatusMsg = "SUCCESS";// strMsgPaytmSTATUS;
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentTransationInsertion Method" + "Message: Premium table entry successful", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }

        }

        private void ReinstatementTransactionInsertion(string paymentType, string stServiceResponse, string ReinstatementStatusMsg, string Sourse, string Destination, bool individualInsertionFlag, string bankName, string paymentCode, string paymentGateway, string currentStatus, string CRMId)
        {
            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementTransactionInsertion Method" + "Message: Inside ReinstatementTransactionInsertion Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            UserRegDetailsActions objUserRegDetailsAction = new UserRegDetailsActions();
            UserRegDetails objUserRegDetails = new UserRegDetails();

            ReinstatementFee = "100";
            OutstandingPremium = "2000";
            ServiceTaxEducationCess = "200";
            AdjustmentAmount = "50";


            objUserRegDetails.TransactionID = lblTransactionIDValue;
            if (ClientId != null)
                objUserRegDetails.ClientID = ClientId;
            else
                objUserRegDetails.ClientID = string.Empty;
            objUserRegDetails.PolicyNumber = lblPolicyNoVal;
            objUserRegDetails.TransactionDate = DateTime.Now;
            objUserRegDetails.TransactionType = paymentType + " APP";
            objUserRegDetails.TransactionComment = stServiceResponse;
            objUserRegDetails.UpdatedBy = "Portal";
            objUserRegDetails.UpdatedDateTime = DateTime.Now;
            objUserRegDetails.Source = Sourse;
            objUserRegDetails.Destination = Destination;
            objUserRegDetails.TransactionResult = ReinstatementStatusMsg;
            //to add paymentcode and bank name
            objUserRegDetails.BankName = bankName;
            objUserRegDetails.PaymentCode = paymentCode;
            objUserRegDetails.PaymentGateway = paymentGateway;
            objUserRegDetails.CurrentStatus = currentStatus;
            objUserRegDetails.CRMId = CRMId;

            if (!string.IsNullOrEmpty(lblPremiumAmountValue))
                objUserRegDetails.ReinstatementAmountDue = Convert.ToDecimal(lblPremiumAmountValue);
            else
                objUserRegDetails.ReinstatementAmountDue = 0.00M;
            if (!string.IsNullOrEmpty(lblPremiumAmountValue))
                objUserRegDetails.ReinstatementAmount = Convert.ToDecimal(lblPremiumAmountValue);
            else
                objUserRegDetails.ReinstatementAmount = 0.00M;

            if (ReinstatementFee != null)
                objUserRegDetails.ReinstatementFee = Convert.ToDecimal(ReinstatementFee);
            else
                objUserRegDetails.ReinstatementFee = 0.00M;

            if (OutstandingPremium != null)
                objUserRegDetails.OutstandingPremium = Convert.ToDecimal(OutstandingPremium);
            else
                objUserRegDetails.OutstandingPremium = 0.00M;
            if (ServiceTaxEducationCess != null)
                objUserRegDetails.ServicetaxAndEducationCessamount = Convert.ToDecimal(ServiceTaxEducationCess);
            else
                objUserRegDetails.ServicetaxAndEducationCessamount = 0.00M;
            if (AdjustmentAmount != null)
                objUserRegDetails.AdjustmentAmount = Convert.ToDecimal(AdjustmentAmount);
            else
                objUserRegDetails.AdjustmentAmount = 0.00M;
            ////.....................

            string resultReinstatement = string.Empty;
            resultReinstatement = objUserRegDetailsAction.InsertGeneralTransactionDetails(objUserRegDetails);
            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementTransactionInsertion Method" + "Message: Transaction_General table entry successful", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

            if ((resultReinstatement == objUserRegDetails.TransactionID) && (individualInsertionFlag == true))
            {
                resultReinstatement = objUserRegDetailsAction.InsertReinstatementTransactionDetails(objUserRegDetails);
                UpdateProcCall();
                objPay.AuthStatus = "Y";
                objPay.ErrorStatus = "1";
                objPay.StatusMsg = "SUCCESS";// strMsgPaytmSTATUS;
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementTransactionInsertion Method" + "Message: Revival table entry successful", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }
        }

        private string PremiumPaymentServiceCall(out string PremiumStatusMsg)
        {
            elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentServiceCall Method" + "Message: Inside PremiumPaymentServiceCall Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            string biztalkResponse = string.Empty;
            bool isPremiumPaymentSuccess = true;
            try
            {

              //  BOsTesting_Orchestration_1_PremiumPaymentRequestSoapClient client = new BOsTesting_Orchestration_1_PremiumPaymentRequestSoapClient();
              //  PremiumPaymentInput premiumPayment = new PremiumPaymentInput();
                PremiumRecieptNew.BizTalkServiceInstance client = new PremiumRecieptNew.BizTalkServiceInstance();
                //  BOsTesting_Orchestration_1_PremiumPaymentRequestSoapClient client = new BOsTesting_Orchestration_1_PremiumPaymentRequestSoapClient();
                PremiumRecieptNew.PremiumPaymentInput premiumPayment = new PremiumRecieptNew.PremiumPaymentInput();

                premiumPayment.PolicyNumber = lblPolicyNoVal;

                premiumPayment.PremiumAmount = lblPremiumAmountValue;
                premiumPayment.PremiumPaymentFlag = "Y";
                premiumPayment.PayType = PayType;
                if (TransactionIDPremiumPayment.ToString() != null)
                    premiumPayment.TransactionId = TransactionIDPremiumPayment.ToString();
                premiumPayment.PaymentGatewayResult = "Success";
                premiumPayment.BGENSR289DESCRN = "Portal";


                elog.LogData("Client ID :" + ClientId + "Policy Number 1014 :" + premiumPayment.PolicyNumber + ",Amt" + premiumPayment.PremiumAmount + ",type" + premiumPayment.PayType + ",TransactionId" + premiumPayment.TransactionId + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentServiceCall Method" + "Message: PremiumPaymentServiceCall Success ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


                PremiumRecieptNew.PortalInput input = new PremiumRecieptNew.PortalInput();
                input.Item = premiumPayment;
                //PortalOutput output = 
                client.PremiumPaymentRequest(input);
                //string msgDescription = output.ResponsefromBiztalk.MessageDescription;
                //string stTransactionId = output.ResponsefromBiztalk.TransactionId;
                elog.LogData("Reciept Created:" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentServiceCall Method" + "Message: PremiumPaymentServiceCall Success ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                biztalkResponse = ConfigurationManager.AppSettings["PremiumPaymentTransactionSucess"];
                isPremiumPaymentSuccess = true;
            }
            catch (Exception ex)
            {
                isPremiumPaymentSuccess = false;
                biztalkResponse = ex.Message;
                elog.LogData("error in reciept :" + ex.Message + ",Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentServiceCall Method" + "Message: Inside PremiumPaymentServiceCall Catch, Message :" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }
            finally
            {
                if (isPremiumPaymentSuccess == true)
                {
                    PremiumStatusMsg = "WIP";
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentServiceCall Method" + "Message: Inside PremiumPaymentServiceCall finally , PremiumStatusMsg : " + Convert.ToString(PremiumStatusMsg), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                }
                else
                {
                    PremiumStatusMsg = "FAIL";
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentServiceCall Method" + "Message: Inside PremiumPaymentServiceCall finally , PremiumStatusMsg : " + Convert.ToString(PremiumStatusMsg), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                }
            }
            return biztalkResponse;
        }

        private string ReinstatementServiceCall(out string ReinstatementStatusMsg)
        {
            elog.LogData("Client ID  before ActionReinstatement:" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: Inside ReinstatementServiceCall Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            PolicyStatus = GetPolicyStatus(lblPolicyNoVal);
            elog.LogData("Client ID  before ActionReinstatement:" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: Inside ReinstatementServiceCall Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            string stResponse = string.Empty;
            bool isReinstatementSuccess = true;
            try
            {
                // elog.LogData("ActionReinstatement Result :" + Session["ActionReinstatement"].ToString() + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: ReinstatementServiceCall Success ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //BOsTesting_Reinstatement_ReinstatementPortSoapClient client = new BOsTesting_Reinstatement_ReinstatementPortSoapClient();
                //Reinstatement reinst = new Reinstatement();

                ReinstatementReciept.BizTalkServiceInstance client = new ReinstatementReciept.BizTalkServiceInstance();
                ReinstatementReciept.Reinstatement reinst = new ReinstatementReciept.Reinstatement();

                reinst.ContractNumber = lblPolicyNoVal;
                if (TransactionIDReinstatement.ToString() != null)
                    reinst.TransactionId = TransactionIDReinstatement.ToString();
                if (PayableAmount.ToString() != null)
                    reinst.PremiumAmount = PayableAmount.ToString();
                reinst.PayType = PayType;
                elog.LogData("Client ID :" + ClientId + "Policy Number 1076 :" + reinst.ContractNumber + ",Amt" + reinst.PremiumAmount + ",type" + reinst.PayType + ",TransactionId" + reinst.TransactionId + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: PremiumPaymentServiceCall Method" + "Message: PremiumPaymentServiceCall Success ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                if (PolicyStatus == "Premium Discontinuance")
                {
                    reinst.Action = "C";
                    elog.LogData("INSIDE PDPD REINSTATEMENT SECTION", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    // GetReinstatement(lblPolicyNoVal); //Calls BizTalk Services here !!!


                }
                else
                {
                    reinst.Action = "A";
                    elog.LogData("INSIDE ELSE getReinstatmentByPDPDServices", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    //  GetReinstatement_Old(policyNumber); //Calls BizTalk Services here !!!
                    //getReinstatmentByPDPDServices(lblPolicyNoVal);

                }
                //if (Session["ActionReinstatement"] != null)
                //{
                //    reinst.Action = Session["ActionReinstatement"].ToString();
                //}
                reinst.BGENSR289DESCRN = "Portal";
                elog.LogData("ActionReinstatement Result :" + reinst.Action + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: ReinstatementServiceCall Success ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                reinst.PaymentGatewayResult = "Success";
                elog.LogData("PayableAmount revival inside ReinstatementServiceCall " + Convert.ToString(reinst.PremiumAmount), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //PortalOutput output = 
                client.ReinstatementRequestandResponse(reinst);
                //string msgDescription = output.ResponsefromBiztalk.MessageDescription;
                //string stTransactionId = output.ResponsefromBiztalk.TransactionId;
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: ReinstatementServiceCall Success ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                stResponse = ConfigurationManager.AppSettings["ReinstatementTransactionSucess"];
                isReinstatementSuccess = true;
            }
            catch (Exception ex)
            {
                isReinstatementSuccess = false;
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: Inside ReinstatementServiceCall Catch, Message :" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                stResponse = ex.Message;
            }
            finally
            {
                if (isReinstatementSuccess == true)
                {
                    ReinstatementStatusMsg = "WIP";
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: Inside ReinstatementServiceCall finally , ReinstatementStatusMsg : " + Convert.ToString(ReinstatementStatusMsg), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                }
                else
                {
                    ReinstatementStatusMsg = "FAIL";
                    elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: ReinstatementServiceCall Method" + "Message: Inside ReinstatementServiceCall finally , ReinstatementStatusMsg : " + Convert.ToString(ReinstatementStatusMsg), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                }
            }
            return stResponse;
        }

        private string GetTransactionID()
        {


            elog.LogData("inside GetTransactionID ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

            UserRegDetailsActions objUserRegDetailsAction = new UserRegDetailsActions();
            string stTrasactionId = objUserRegDetailsAction.GenerateTransactionID();
            elog.LogData("inside GetTransactionID ID=" + stTrasactionId, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

            try
            {
                //if (StatusMsg != "No Policy exists")
                // {

                elog.LogData("inside GetTransactionID InsertDB=" + stTrasactionId, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString());
                SqlCommand cmd = new SqlCommand("Proc_InsertTransactionDetails_PaymentAppTempData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@TransactionId", stTrasactionId);
                cmd.Parameters.AddWithValue("@TransIdWCFApp", lblTransactionIDValueN);
                cmd.Parameters.AddWithValue("@Policy_Number", lblPolicyNoVal);
                cmd.Parameters.AddWithValue("@Client_Id", ClientId);
                cmd.Parameters.AddWithValue("@Request_ID", Request_ID);
                if (lblPremiumAmountValue != "")
                    cmd.Parameters.AddWithValue("@Premium_Amount", Convert.ToDecimal(lblPremiumAmountValue));
                else
                    cmd.Parameters.AddWithValue("@Premium_Amount", 0);

                if (MobileNo != "" && MobileNo != null)
                    cmd.Parameters.AddWithValue("@MobileNo", MobileNo);
                else
                    cmd.Parameters.AddWithValue("@MobileNo", "");
                if (EMail != "" && EMail != null)
                    cmd.Parameters.AddWithValue("@EMail", EMail);
                else
                    cmd.Parameters.AddWithValue("@EMail", "");
                cmd.Parameters.AddWithValue("@VendorName", "PayTmAPP");
                cmd.Parameters.AddWithValue("@PC", PC);
                cmd.Parameters.AddWithValue("@checksum", checksum);
                cmd.Parameters.AddWithValue("@IsPaymentDoneFlag", "0");
                TotalInput = "" + PolcyNoinput + "|" + ClientId + "|" + Request_ID + "|" + MobileNo + "|" + EMail + "|" + VendorName + "|" + PC + "|" + BANKTXNID + "|" + TXNAMOUNT + "|" + CURRENCY + "|" + STATUS + "|" + RESPMSG + "|" + TXNDATE + "|" + GATEWAYNAME + "|" + BANKNAME + "|" + CHECKSUMHASH + "|" + lblTransactionIDValueN + "|" + stTrasactionId + "|" + PayableAmount + "|";
                cmd.Parameters.AddWithValue("@InputString", TotalInput);

                //SqlParameter p2 = cmd.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 50);
                //p2.Value = txtMobileNo.Text;
                cmd.ExecuteNonQuery();
                elog.LogData("inside GetTransactionID After Insert ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                conn.Close();
            }
            catch (Exception ex)
            {
                // rethrow = ExceptionPolicy.HandleException(ex, Convert.ToString(ConfigurationManager.AppSettings["DaichiUIPolicy"]));
                elog.LogData("Client ID :" + Convert.ToString(ClientId) + "Policy Number :" + Convert.ToString(policyNo) + "inside GetTransactionID" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                HttpContext.Current.Session["ErrMsg"] = Convert.ToString(SUDUtility.SystemError_Msg);//"System Error";//Added on 3-09-10
            }
            finally
            {
                //Response.Redirect(ViewState["CultureName"].ToString() + "/Pages/Message.aspx?ErrorCode=1.9");
            }

            return stTrasactionId;
            elog.LogData("inside GetTransactionID return ID " + stTrasactionId, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
        }

        //public DataSet getClientPolicyDetails(string ClientId)
        //{
        //    DataSet dt = new DataSet();
        //    try
        //    {
        //        elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: getClientPolicyDetails " + "Message: Inside getClientPolicyDetails", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
        //        WebService_GetallPolicydetailsSoapClient objGetAllPolicyDetails = new WebService_GetallPolicydetailsSoapClient();

        //        Logininput objLogininput = new Logininput();
        //        objLogininput.ClientID = ClientId;
        //        objLogininput.GetPolicyFundDetails = "Y";



        //        //Common Method for All
        //        RetriveServiceInput objPolicyDetailsInput = new RetriveServiceInput();
        //        objPolicyDetailsInput.Item = objLogininput;

        //        RetriveServiceOutput objPolicyDetailsOutput = objGetAllPolicyDetails.Operation_1(objPolicyDetailsInput);

        //        // Serializing the MyPolicyOutput object to Dataset
        //        dt = objHelpeclassr.GetDataSet(new XmlSerializer(typeof(RetriveServiceOutput)), objPolicyDetailsOutput);
        //        elog.LogData("Client ID dt data 1349:" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: getClientPolicyDetails " + "Message: WebService_GetallPolicydetailsSoapClient Call success", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));






        //    }
        //    catch (Exception e)
        //    {
        //        elog.LogData("Client ID 1354:" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: getClientPolicyDetails " + "Message: Inside getClientPolicyDetails catch" + Convert.ToString(e.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
        //        ErrorLog Log = new ErrorLog();
        //        elog.LogData(e.Message.ToString(), ConfigurationSettings.AppSettings["ErrorLogFolderPath"].ToString());
        //    }
        //    return dt;
        //}



        public DataSet getClientPolicyDetails(string ClientID)
        {
            DataSet dsPolicy = new DataSet();
            try
            {
                string sqlConnectionString =

                ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new

                SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("GetPolicyDetailsOfclient", cn);
                cmd.Parameters.Add("@Client_ID", SqlDbType.NVarChar, 10).Value = ClientID;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dsPolicy);
                cn.Close();
            }
            catch (Exception ex)
            {
                dsPolicy = null;

            }

            return dsPolicy;
        }



        #region -- BRS Email and SMS ----
        private void SendReceiptOnMail(string ClientId, string strPolicyNo, string strProductName, string strPaymentFrequency, string strPolicyType, string strPolicyHolder, string strAmount, string strDueDate, string strTrazID, string strMobile, string strSendTo, string strSendFrom)
        {
            string mailSent = string.Empty;
            string strClientID = string.Empty;
            string formatted = "";
            // using (SPSite spoSite = new SPSite(ConfigurationSettings.AppSettings["NOforwordslashSiteURL"].ToString() + "/en-US/"))
            //{
            //   using (SPWeb web = spoSite.OpenWeb())
            // {
            /*Online Premium Payment-CRMDC201 - Added on 24 Sep 2014 - Starts here*/
            #region --Address and Contact Detais--
            string strAddress = string.Empty;

            string strAddress1 = string.Empty;
            string strAddress2 = string.Empty;
            string strAddress3 = string.Empty;
            string strState = string.Empty;
            string strZipCode = string.Empty;
            string strCity = string.Empty;
            string strSalutation = string.Empty;

            string strStreet = string.Empty;

            //get Client ID
            if (ClientId != null)
                strClientID = ClientId;// HttpContext.Current.Session["ClientID"].ToString();

            DataTable dtClientDetails = new DataTable();
            UserRegDetailsActions userBO = new UserRegDetailsActions();
            dtClientDetails = userBO.GetClientInfoDetails(strClientID);


            if (dtClientDetails.Rows.Count > 0)
            {
                strStreet = Convert.ToString(dtClientDetails.Rows[0]["Street"]);

                strAddress1 = Convert.ToString(dtClientDetails.Rows[0]["Line_1"]);
                strAddress2 = Convert.ToString(dtClientDetails.Rows[0]["Line_2"]);
                strAddress3 = Convert.ToString(dtClientDetails.Rows[0]["Line_3"]);
                strState = Convert.ToString(dtClientDetails.Rows[0]["State"]);
                strZipCode = Convert.ToString(dtClientDetails.Rows[0]["PIN"]);
                strCity = Convert.ToString(dtClientDetails.Rows[0]["City"]);
                strSalutation = Convert.ToString(dtClientDetails.Rows[0]["Salutation"]);
            }


            //Address1
            if (!string.IsNullOrEmpty(strStreet))
                strAddress = strAddress + strStreet + "<br>";
            //Address2

            //Address1
            if (!string.IsNullOrEmpty(strAddress1))
                strAddress = strAddress + strAddress1 + "<br>";
            //Address2
            if (!string.IsNullOrEmpty(strAddress2))
                strAddress = strAddress + strAddress2 + "<br>";
            //Address3
            if (!string.IsNullOrEmpty(strAddress3))
                strAddress = strAddress + strAddress3 + "<br>";
            //State
            if (!string.IsNullOrEmpty(strState))
            {
                if (!string.IsNullOrEmpty(strZipCode))
                    strAddress = strAddress + strState + "-";
                else
                    strAddress = strAddress + strState + "<br>";
            }

            //PinCode
            if (!string.IsNullOrEmpty(strZipCode))
                strAddress = strAddress + strZipCode + "<br>";
            //City
            if (!string.IsNullOrEmpty(strCity))
                strAddress = strAddress + strCity + "<br>";
            //Mobile
            if (!string.IsNullOrEmpty(strMobile))
                strAddress = strAddress + strMobile + "<br>";
            //Email
            strAddress = strAddress + strSendTo + "<br>";


            #endregion
            ///*Online Premium Payment-CRMDC201 - Added on 24 Sep 2014 - Ends here*/
            //#region -- Get Email Body from List  --
            //// Build a query.
            //SPQuery query = new SPQuery();
            //query.Query = string.Concat(
            //          "<Where><Eq>",
            //             "<FieldRef Name='Title'/>",
            //             "<Value Type='Text'>Payment Receipt</Value>",
            //          "</Eq></Where>");
            //query.ViewFields = string.Concat(
            //               "<FieldRef Name='Title' />",
            //               "<FieldRef Name='EmailBody' />",
            //               "<FieldRef Name='Subject' />");
            //query.ViewFieldsOnly = true; // Fetch only the data that we need.

            //// Get Email Boday Fro List
            ////string listUrl = web.ServerRelativeUrl + "/lists/tasks";
            //SPList spoEmailConfigList = web.Lists["EmailConfig"];
            //SPListItemCollection items = spoEmailConfigList.GetItems(query);
            //#endregion
            DateTime dt = DateTime.Now.Date;
            string strMailBody = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["EmailBodyPath"])))
            {
                strMailBody = reader.ReadToEnd();
            }

            //body = body.Replace("#ImageLogo#", ConfigurationManager.AppSettings["ImgLogo"]);
            ////formatted = dt.ToString("dd/MM/yyyy");
            ////body = body.Replace("#currentdate#", Convert.ToString(formatted));
            ////body = body.Replace("#policyholdername#", Gatewayresobj.CustomerName);
            ////body = body.Replace("#address#", strAddress);
            ////body = body.Replace("#policyno#", resobj.PolicyApplicationNo);
            ////body = body.Replace("#paymenttype#", strPolicyType);

            if (strPolicyType == "Top Up Premium Payment")
                strPolicyType = "Top Up Payment";


            //string strMailBody = string.Empty;
            string strSubject = string.Empty;

            #region --Repalce the #code fron body--
            // Print the details.
            //  foreach (SPListItem item in items)
            //{
            #region -- Create Subject --
            strSubject = ConfigurationManager.AppSettings["PaymentMail_Subject"];//item["Subject"].ToString();
            strSubject = strSubject.Replace("#policyno#", strPolicyNo);
            strSubject = strSubject.Replace("#policytype#", strPolicyType);
            #endregion
            #region -- Create Mail Body --
            strMailBody = //item["EmailBody"].ToString();
                          //Current Date
            strMailBody = strMailBody.Replace("#currentdate#", DateTime.Now.ToString("dd.MM.yyyy"));
            /*Online Premium Payment-CRMDC201 - Added on 24 Sep 2014 - Starts here*/
            //Address
            strMailBody = strMailBody.Replace("#address#", strAddress);
            /*Online Premium Payment-CRMDC201 - Added on 24 Sep 2014 - Ends here*/

            //User name
            strMailBody = strMailBody.Replace("#policyholdername#", strSalutation + "&nbsp;" + strPolicyHolder);

            //Policy No
            strMailBody = strMailBody.Replace("#policyno#", strPolicyNo);
            //Policy Type
            strMailBody = strMailBody.Replace("#paymenttype#", strPolicyType);

            //Amount
            strMailBody = strMailBody.Replace("#preamount#", strAmount);
            //DueDate
            strMailBody = strMailBody.Replace("#duedate#", strDueDate);

            /*Online Premium Payment-CRMDC201 - Added on 24 Sep 2014 - Starts here*/
            //Product Name
            strMailBody = strMailBody.Replace("#productname#", strProductName);
            //Mode
            strMailBody = strMailBody.Replace("#paymentfrequency#", strPaymentFrequency);
            /*Online Premium Payment-CRMDC201 - Added on 24 Sep 2014 - Ends here*/

            //Tranzaction ID
            strMailBody = strMailBody.Replace("#transactionid#", strTrazID);
            //Add Iange Logo
            strMailBody = strMailBody.Replace("#ImageLogo#", "<img src='" + ConfigurationSettings.AppSettings["SiteURL"].ToString() + ConfigurationSettings.AppSettings["PDFReceiptLogoPath"].ToString() + "' alt='' style='border-bottom: 0px; border-left: 0px; border-top: 0px; border-right: 0px'/>");
            ////strMailBody = strMailBody.Replace("#ImageLogo#", "<img src='" + ConfigurationSettings.AppSettings["SiteURL"].ToString() + ConfigurationSettings.AppSettings["PDFReceiptLogoPath"].ToString() + "' alt='' style='border-bottom: 0px; border-left: 0px; border-top: 0px; border-right: 0px'/>");
            ////LinkedResource inline = new LinkedResource("C:\\inetpub\\wwwroot\\wss\\VirtualDirectories\\80\\img\\logo.jpg", MediaTypeNames.Image.Jpeg);
            #endregion
            //  HttpContext.Current.Session["PdfBoady"] = strMailBody;
            //Send Mail
            mailSent = Helpers.Helpercls.BizSendEmail(strSendTo, strSendFrom, strSubject, strMailBody, "");
            elog.LogData("Client ID :" + ClientId + "Policy Number :" + policyNo + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: SendReceiptOnMail " + "Message: Mail sent" + Convert.ToString(mailSent), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            elog.LogData("mailSent : " + Convert.ToString(mailSent), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            if (Convert.ToString(mailSent) == "Success")
            {
                emailsent = true;
            }
            // }
            #endregion
            // }
            // }
        }




        private void SendReceiptOnSMS(string strRecipientNo, string strPolicyNo, string strAmount)
        {
            try
            {
                elog.LogData("Inside SendReceiptOnSMS : " + Convert.ToString(strRecipientNo), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                string strSMSBody = string.Empty;
                strSMSBody = ConfigurationSettings.AppSettings["SMSPaymentReceipt"].ToString();
                elog.LogData("Inside strSMSBody : " + Convert.ToString(strSMSBody), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                //replace policy No
                strSMSBody = strSMSBody.Replace("#policyno#", strPolicyNo);
                elog.LogData("Inside policyno : " + Convert.ToString(strPolicyNo), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                //replace policyType
                strSMSBody = strSMSBody.Replace("#amount#", strAmount);
                elog.LogData("Inside amount : " + Convert.ToString(strAmount), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


                string strISSMSSend = SUDUtility.SendSMS(strSMSBody, strRecipientNo, "Customer Care");
                elog.LogData("strISSMSSend : " + Convert.ToString(strISSMSSend), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                if (Convert.ToString(SMS_Sent) == "Success")
                {
                    SMS_Sent = true;
                }
                elog.LogData("SMS_Sent : " + Convert.ToString(SMS_Sent) + ", strISSMSSend:" + strISSMSSend, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

            }
            catch (Exception ex)
            {
                elog.LogData(ex.Message.ToString(), ConfigurationSettings.AppSettings["ErrorLogFolderPath"].ToString());
            }
        }
        #endregion

        #region Error Msg Insertion
        private void TransactionFailErrorMsgInsertion(string paymentType, string strMsg, string paymentGateway)
        {
            try
            {
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: TransactionFailErrorMsgInsertion Method" + "Message: Inside TransactionFailErrorMsgInsertion Method", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                UserRegDetailsActions objUserRegDetailsAction = new UserRegDetailsActions();
                UserRegDetails objUserRegDetails = new UserRegDetails();
                if (paymentType == "Premium Payment")
                {
                    if (TransactionIDPremiumPayment != null)
                        objUserRegDetails.TransactionID = TransactionIDPremiumPayment.ToString();
                }
                else if (paymentType == "Reinstatement")
                {
                    if (TransactionIDReinstatement != null)
                        objUserRegDetails.TransactionID = TransactionIDReinstatement.ToString();
                }

                //objUserRegDetails.TransactionID = Convert.ToString(TransactionIDReinstatement); 
                objUserRegDetails.PolicyNumber = lblPolicyNoVal;
                if (ClientId != null)
                    objUserRegDetails.ClientID = ClientId;
                else
                    objUserRegDetails.ClientID = string.Empty;

                objUserRegDetails.TransactionDate = DateTime.Now;
                objUserRegDetails.TransactionType = paymentType + " APP";
                //objUserRegDetails.TransactionComment = stResponse;
                objUserRegDetails.PaymentGateway = paymentGateway;
                objUserRegDetails.TransFailErrorMsg = strMsg;
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: TransactionFailErrorMsgInsertion Method" + "Message: Values TransFailErrorMsg :" + Convert.ToString(objUserRegDetails.TransFailErrorMsg) + "TransactionType :" + Convert.ToString(objUserRegDetails.TransactionType) + "TransactionDate :" + Convert.ToString(objUserRegDetails.TransactionDate) + "PaymentGateway :" + Convert.ToString(objUserRegDetails.PaymentGateway) + "TransactionID :" + Convert.ToString(objUserRegDetails.TransactionID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                string TransFailErrorMessage = string.Empty;
                TransFailErrorMessage = objUserRegDetailsAction.InsertTransactionFailErrorMsg(objUserRegDetails);
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: TransactionFailErrorMsgInsertion Method" + "Message:TransactionFailErrorMsgInsertion Success", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }
            catch (Exception ex)
            {
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + lblPolicyNoVal + "File:Website PremiumPaymentGatewayConfirmation.cs:, Fuction: TransactionFailErrorMsgInsertion Catch" + "Message:Inside TransactionFailErrorMsgInsertion Catch Message :" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }
        }
        #endregion

        #region Email and SMS Sent Details Insertion
        public void EmailSMS_SentDetailsInsertion(string registeredEmailId, string registeredMobileNo, bool strSMS_Sent, bool stremailsent, string paymentType)
        {
            try
            {
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File: CP PaymentGatewayConfirmation.cs:, Fuction: EmailSMS_SentDetailsInsertion" + "Message:Inside EmailSMS_SentDetailsInsertion:", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                UserRegDetailsActions objUserRegDetailsAction = new UserRegDetailsActions();
                UserRegDetails objUserRegDetails = new UserRegDetails();
                if (paymentType == "Premium Payment")
                {
                    if (TransactionIDPremiumPayment != null)
                        objUserRegDetails.TransactionID = TransactionIDPremiumPayment;
                }
                else if (paymentType == "Reinstatement")
                {
                    if (TransactionIDReinstatement != null)
                        objUserRegDetails.TransactionID = TransactionIDReinstatement.ToString();
                }
                //else if (paymentType == "Top Up Premium Payment")
                //{
                //    if (TransactionIDTopUp != null)
                //        objUserRegDetails.TransactionID = TransactionIDTopUp.ToString();
                //}

                //objUserRegDetails.TransactionID = Convert.ToString(TransactionIDReinstatement); 
                objUserRegDetails.PolicyNumber = lblPolicyNoVal;
                if (ClientId != null)
                    objUserRegDetails.ClientID = ClientId.ToString();
                else
                    objUserRegDetails.ClientID = string.Empty;

                objUserRegDetails.TransactionDate = DateTime.Now;
                objUserRegDetails.TransactionType = paymentType;
                //objUserRegDetails.TransactionComment = stResponse;
                objUserRegDetails.AltEmailId = "NA";
                objUserRegDetails.AltMobileNo = "NA";
                objUserRegDetails.EmailFlag = stremailsent;
                objUserRegDetails.SmsFlag = strSMS_Sent;
                objUserRegDetails.RegEmailId = registeredEmailId;
                objUserRegDetails.RegMobileNo = registeredMobileNo;
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File: CP PaymentGatewayConfirmation.cs:, Fuction: EmailSMS_SentDetailsInsertion" + "Message: Values AltMobileNo:" + Convert.ToString(objUserRegDetails.AltMobileNo) + "TransactionType :" + Convert.ToString(objUserRegDetails.TransactionType) + "EmailFlag :" + Convert.ToString(objUserRegDetails.EmailFlag) + "RegEmailId :" + Convert.ToString(objUserRegDetails.RegEmailId) + "TransactionID :" + Convert.ToString(objUserRegDetails.TransactionID), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                string EmailSMSDetails = string.Empty;
                EmailSMSDetails = objUserRegDetailsAction.InsertEmailSMS_SentDetails(objUserRegDetails);
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File: CP PaymentGatewayConfirmation.cs:, Fuction: EmailSMS_SentDetailsInsertion" + "Message:EmailSMS_SentDetailsInsertion Success ,EmailSMSDetails :" + Convert.ToString(EmailSMSDetails), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }
            catch (Exception ex)
            {
                elog.LogData("Client ID :" + ClientId + "Policy Number :" + PolcyNoinput + "File: CP PaymentGatewayConfirmation.cs:, Fuction: EmailSMS_SentDetailsInsertion catch " + "Message:EmailSMS_SentDetailsInsertion catch Message:" + Convert.ToString(ex.Message), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }
        }
        #endregion Email and SMS Sent Details Insertion

        #region getReinstatmentByPDPDService
        private void getReinstatmentByPDPDServices(string policyNumber)
        {
            try
            {

                PDPDPortalInputPDPDReinstatement ObjPDPD = new PDPDPortalInputPDPDReinstatement(); //DONE
                if (TransactionIDReinstatement != null)
                    ObjPDPD.TransactionId = TransactionIDReinstatement.ToString();

                PDPDPortalInput ObjPDPDInput = new PDPDPortalInput();
                ObjPDPDInput.PDPDReinstatement = ObjPDPD;
                ObjPDPDInput.PDPDReinstatement.EFFDATE = "20180330";//DateTime.Now.ToString("yyyyMMdd"); //DateTime.Now.AddDays(+1).ToString("yyyyMMdd"); //DateTime.Now.ToString("yyyyMMdd");
                ObjPDPDInput.PDPDReinstatement.Action = "B";
                ObjPDPDInput.PDPDReinstatement.Contractnumber = policyNumber;

                elog.LogData(" ReinstatmentByPDPDServices ObjPDPD.Contractnumber is here!!!:" + ObjPDPDInput.PDPDReinstatement, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                Module_1_ReinstatementforPDPD_ReinstatementforPDPDReqResPort clientPDPD = new Module_1_ReinstatementforPDPD_ReinstatementforPDPDReqResPort();
                PDPDPortalOutput outputPDPD = clientPDPD.Operation_1(ObjPDPDInput);
                elog.LogData("ReinstatmentByPDPDServices After outputPDPD ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                // CODE TEST STARTS HERE ReinstatmentByPDPDServices
                if (outputPDPD != null)
                {
                    if (!string.IsNullOrEmpty(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768OSBAL))
                    {
                        lblOutstandingPremiumVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768OSBAL));// Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768OSBAL) + Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768STAXAMT01));//OutstandingPremium //DONE
                        elog.LogData("WebSite  : lblOutstandingPremiumVal=" + lblOutstandingPremiumVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        lbllblReinstatementFee = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768ZRINSFEEC)); //Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768ZRINSFEEC) + Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BEGNSJ768ZSTAXRINSC));//ReinstatementFee //DONE
                        elog.LogData("WebSite :lbllblReinstatementFee=" + lbllblReinstatementFee, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        lblServiceTaxEducationCessVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768XCHGRCVR) + Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768TOLER));//ServiceTaxEducationCess should be xcharge+toarance
                        elog.LogData("WebSite  : lblServiceTaxEducationCessVal=" + lblServiceTaxEducationCessVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        lblAdjustmentAmountVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768HSUSPENSE));//AdjustmentAmount
                        elog.LogData("WebSite: lblAdjustmentAmountVal=" + lblAdjustmentAmountVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        //adding extra fields like Tolerance,X-Charge,Suspense Amount 5/16/2017

                        lblReinstatementAmountVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768TOTAL));
                        elog.LogData("WebSite : lblReinstatementAmountVal=" + lblReinstatementAmountVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));



                        if (!string.IsNullOrEmpty(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768BTDATE))
                        {
                            string strPaidFromDate = outputPDPD.PDPDResponsefromBiztalk.BGENSJ768BTDATE;//PaidFromDate
                            lblPaidFromDateVal = strPaidFromDate.Substring(6, 2) + " / " + strPaidFromDate.Substring(4, 2) + " / " + strPaidFromDate.Substring(0, 4);
                        }
                        if (!string.IsNullOrEmpty(outputPDPD.PDPDResponsefromBiztalk.BGENSJ768PTDATE))//PaidToDate
                        {
                            string strPaidToDate = outputPDPD.PDPDResponsefromBiztalk.BGENSJ768PTDATE;

                            lblPaidToDateVal = strPaidToDate.Substring(6, 2) + " / " + strPaidToDate.Substring(4, 2) + " / " + strPaidToDate.Substring(0, 4);
                        }

                    }
                    else
                    {
                        ServiceErrorMsg();
                        elog.LogData("BGENSJ768OSBAL value is null" + outputPDPD.PDPDResponsefromBiztalk.BGENSJ768OSBAL, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                ServiceErrorMsg();
                elog.LogData("Exception Error Message" + ex.Message, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                return;
            }

        }
        #endregion

        #region GetReinstatement FOR PD-PD starts here
        private void GetReinstatement(string policyNumber)
        {
            try
            {
                //ReinstatementAmount amount = new ReinstatementAmount();
                //amount.ContractNumber = policyNumber;
                //amount.ReinstatementAmountFlag = "Y";
                //if (TransactionIDReinstatement != null)
                //    amount.TransactionId = TransactionIDReinstatement.ToString();
                //ReinstatementAmountGet.PortalInput input = new ReinstatementAmountGet.PortalInput();
                //input.Item = amount;
                ////ReinstatementAmountGet.PortalOutputResponsefromBiztalk responseBiztalk = new ReinstatementAmountGet.PortalOutputResponsefromBiztalk();

                //ReinstatementAmountGet.BOsTesting_BizTalk_Orchestration_PortalDay2ReqestResponsePortSoapClient client = new ReinstatementAmountGet.BOsTesting_BizTalk_Orchestration_PortalDay2ReqestResponsePortSoapClient();
                //  ReinstatementAmountGet.ReinstatementAmountResponse responce=new ReinstatementAmountGet.ReinstatementAmountResponse(client.ReinstatementAmount(input));

                //  ReinstatementAmountGet.PortalOutput output = responce.PortalOutput;
                ////output.ResponsefromBiztalk = responce.PortalOutput;
                //Added on 13-05
                elog.LogData("WEB : Inside GetReinstatement", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //OLD  ReinstatementAmountGet.ReinstatementAmount amount = new ReinstatementAmountGet.ReinstatementAmount();

                //--test--- PD--PD

                PDPDPortalInputPDPDReinstatement ObjPDPD = new PDPDPortalInputPDPDReinstatement(); //DONE





                //OLD    amount.ContractNumber = policyNumber;


                //OLD amount.ReinstatementAmountFlag = "Y";
                if (TransactionIDReinstatement != null)
                    //OLD  amount.TransactionId = TransactionIDReinstatement.ToString();
                    ObjPDPD.TransactionId = TransactionIDReinstatement.ToString();

                elog.LogData("Inside TransactionIDReinstatement" + Convert.ToString(TransactionIDReinstatement), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //OLD ReinstatementAmountGet.PortalInput input = new ReinstatementAmountGet.PortalInput();
                PDPDPortalInput ObjPDPDInput = new PDPDPortalInput();
                //OLD input.Item = amount;
                //ReinstatementAmountGet.PortalOutputResponsefromBiztalk responseBiztalk = new ReinstatementAmountGet.PortalOutputResponsefromBiztalk();
                ObjPDPDInput.PDPDReinstatement = ObjPDPD;
                ObjPDPDInput.PDPDReinstatement.EFFDATE = "20180330"; //DateTime.Now.ToString("yyyyMMdd"); //DateTime.Now.AddDays(+1).ToString("yyyyMMdd"); //DateTime.Now.ToString("yyyyMMdd");
                ObjPDPDInput.PDPDReinstatement.Action = "D";
                ObjPDPDInput.PDPDReinstatement.Contractnumber = policyNumber;
                elog.LogData(" WEBSITE :: ObjPDPD.Contractnumber is here!!!:" + ObjPDPDInput.PDPDReinstatement, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //OLD   ReinstatementAmountGet.BOsTesting_BizTalk_Orchestration_PortalDay2ReqestResponsePortSoapClient client = new ReinstatementAmountGet.BOsTesting_BizTalk_Orchestration_PortalDay2ReqestResponsePortSoapClient();
                // ReinstatementAmountGet.ReinstatementAmountResponse responce=new //ReinstatementAmountGet.ReinstatementAmountResponse(client.ReinstatementAmount(input));
                Module_1_ReinstatementforPDPD_ReinstatementforPDPDReqResPort clientPDPD = new Module_1_ReinstatementforPDPD_ReinstatementforPDPDReqResPort();

                //OLD ReinstatementAmountGet.PortalOutput output = client.ReinstatementAmount(input);
                PDPDPortalOutput outputPDPD = clientPDPD.Operation_1(ObjPDPDInput);
                elog.LogData(" website After outputPDPD ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //End
                #region  COMMENTED BLOCK FOR PD-PD TRIAL WEBSITE
                //if (output != null)
                //{
                //    elog.LogData("Inside GetReinstatement if", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //    if (!string.IsNullOrEmpty(output.ResponsefromBiztalk.OutstandingPremium))
                //    {
                //        lblOutstandingPremiumVal = output.ResponsefromBiztalk.OutstandingPremium;
                //        lblServiceTaxEducationCessVal = output.ResponsefromBiztalk.ServiceTaxandEductionCessAmount;
                //        lbllblReinstatementFee = output.ResponsefromBiztalk.ReinstatementFee;
                //        lblAdjustmentAmountVal = output.ResponsefromBiztalk.AdjustmentAmount;
                //        lblReinstatementAmountVal = output.ResponsefromBiztalk.ReinstatementAmount;

                //        if (!string.IsNullOrEmpty(output.ResponsefromBiztalk.PaidFromDate))
                //        {
                //            string strPaidFromDate = output.ResponsefromBiztalk.PaidFromDate;
                //            lblPaidFromDateVal = strPaidFromDate.Substring(6, 2) + " / " + strPaidFromDate.Substring(4, 2) + " / " + strPaidFromDate.Substring(0, 4);
                //        }
                //        if (!string.IsNullOrEmpty(output.ResponsefromBiztalk.PaidToDate))
                //        {
                //            string strPaidToDate = output.ResponsefromBiztalk.PaidToDate;
                //            lblPaidToDateVal = strPaidToDate.Substring(6, 2) + " / " + strPaidToDate.Substring(4, 2) + " / " + strPaidToDate.Substring(0, 4);
                //        }
                //    }
                //    else
                //    {
                //        elog.LogData("Inside In GetReinstatement ELse", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                //        ServiceErrorMsg();
                //        return;
                //    }
                //}//end of IF (output != null)
                #endregion
                if (outputPDPD != null)
                {
                    elog.LogData("WEB Inside outputPDPD != null", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    if (!string.IsNullOrEmpty(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769OSBAL))//ResponsefromBiztalk.OutstandingPremium
                    {
                        elog.LogData("Inside WEB  if (!string ", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        lblOutstandingPremiumVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769OSBAL)); //Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769OSBAL) + Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769STAXAMT01));//OutstandingPremium
                        elog.LogData("WebSite : lblOutstandingPremiumVal=" + lblOutstandingPremiumVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        lblServiceTaxEducationCessVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769XCHGRCVR) + Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769TOLER)); //outputPDPD.PDPDResponsefromBiztalk.BGENSJ769STAXAMT01;//ServiceTaxandEductionCessAmount
                        elog.LogData("WebSite : lblServiceTaxEducationCessVal=" + lblServiceTaxEducationCessVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        lbllblReinstatementFee = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769HREINSTFEE));//ReinstatementFee
                        elog.LogData("WebSite : lbllblReinstatementFee=" + lbllblReinstatementFee, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        // lblAdjustmentAmountVal = output.ResponsefromBiztalk.AdjustmentAmount;
                        lblAdjustmentAmountVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769HSUSPENSE));//AdjustmentAmount
                        elog.LogData("WebSite : lblAdjustmentAmountVal=" + lblAdjustmentAmountVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                        //lblReinstatementAmountVal = output.ResponsefromBiztalk.ReinstatementAmount;
                        //adding extra fields like Tolerance,X-Charge,Suspense Amount 5/16/2017



                        // lblReinstatementAmountVal = outputPDPD.PDPDResponsefromBiztalk.BGENSJ769TOAMOUNT;//ReinstatementAmount
                        lblReinstatementAmountVal = Convert.ToString(Convert.ToDecimal(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769TOTAL));//ReinstatementAmount
                        elog.LogData("WebSite : lblReinstatementAmountVal=" + lblReinstatementAmountVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


                        #region //PaidFromDate and PaidToDate Newly included PDPD BO



                        if (!string.IsNullOrEmpty(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769BTDATE))
                        {
                            string strPaidFromDate = outputPDPD.PDPDResponsefromBiztalk.BGENSJ769BTDATE;//PaidFromDate
                            elog.LogData("WebSite :lblPaidFromDateVal" + lblPaidFromDateVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                            lblPaidFromDateVal = strPaidFromDate.Substring(6, 2) + " / " + strPaidFromDate.Substring(4, 2) + " / " + strPaidFromDate.Substring(0, 4);
                        }
                        if (!string.IsNullOrEmpty(outputPDPD.PDPDResponsefromBiztalk.BGENSJ769PTDATE))
                        {
                            string strPaidToDate = outputPDPD.PDPDResponsefromBiztalk.BGENSJ769PTDATE;//PaidToDate

                            lblPaidToDateVal = strPaidToDate.Substring(6, 2) + " / " + strPaidToDate.Substring(4, 2) + " / " + strPaidToDate.Substring(0, 4);
                            elog.LogData("WebSite :lblPaidToDateVal=" + lblPaidToDateVal, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        }


                    }
                    #endregion
                    else
                    {
                        elog.LogData("BGENSJ769OSBAL is null", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        ServiceErrorMsg();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                ServiceErrorMsg();
                elog.LogData("Inside GetReinstatement Catch" + ex.Message, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                return;
            }
        }
        #endregion GetReinstatement FOR PD-PD ends here!!!!!!!!

        private void ServiceErrorMsg()
        {
            ////Commented on 03-09-10--------------------------------------------------------
            //divErrorMessage = (HtmlGenericControl)this.FindControl("divErrorMessage");
            //divErrorMessage.Visible = true;
            //divMain = (HtmlGenericControl)this.FindControl("divMain");
            //divMain.Visible = false;
            //lblErrorMsg.Text = ConfigurationManager.AppSettings["ReinstateGetServiceMsg"];
            //return;
            ////End of Commented on 03-09-10--------------------------------------------------------
            HttpContext.Current.Session["ErrMsg"] = ConfigurationManager.AppSettings["ReinstateGetServiceMsg"];
            elog.LogData("Client ID :" + Convert.ToString(ClientId) + "Policy Number :" + Convert.ToString(HttpContext.Current.Session["ErrMsg"]) + "File:Website PayOnlinePremiumPaymentNew.cs: Function :ServiceErrorMsg, Message :Inside ServiceErrorMsg" + Convert.ToString(HttpContext.Current.Session["ErrMsg"]), Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            //Response.Redirect(ViewState["CultureName"].ToString() + "/Pages/ErrorMessage.aspx");

        }
    }
}
