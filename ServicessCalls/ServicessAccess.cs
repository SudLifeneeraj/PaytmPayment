using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using WIP_Report.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using System.Web.Configuration;
using WIP_Report.Helpers;
using System.IO;
//using objcrmfile = WIP_Report.CRMFile_upload_services;
using System.Xml;
using WIP_Report.CP_PersonalClientModi;
//using WIP_Report.Repository;
using WIP_Report.Helper;
using WIP_Report.Repository;

namespace Customer_portal.ServicessCalls
{
    public class ServicessAccess
    {
        PreLoginRP objLoginRP = new PreLoginRP();
        PreLogin objLogin = new PreLogin();
        ErrorLog elog = new ErrorLog();
        /// <summary>
        /// Get crm call type and sub type
        /// </summary>
        /// <param name="Requesttype"></param>
        /// <returns>CRM request type CT/ST and CT/ST desc</returns>
        public IEnumerable<CRMCallLoginfo> _GetCRMcalllogdata(string Requesttype)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RequestType", Requesttype);
                    return c.Query<CRMCallLoginfo>("_GetCRMCallLogData", p, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }





        /// <summary>
        /// saving crm call log data in to data base with crm id, omni call log data with below params
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="PolicyNo"></param>
        /// <param name="Requesttype"></param>
        /// <param name="category"></param>
        /// <param name="calltypeid"></param>
        /// <param name="calltypesesc"></param>
        /// <param name="subtypeid"></param>
        /// <param name="subtypedcse"></param>
        /// <param name="unserinputtext"></param>
        /// <param name="createcallcrmid"></param>
        /// <param name="updationcrmcallid"></param>
        /// <param name="createcallomniid"></param>
        /// <param name="omniuploadfilename"></param>
        /// <returns>Save data into data base table name(ClientServices_Log)</returns>
        public int _Createcrmcallinfo(string clientid, string PolicyNo, string Requesttype, string category, string calltypeid, string calltypesesc,
                                                             string subtypeid, string subtypedcse, string unserinputtext, string createcallcrmid, string updationcrmcallid,
                                                             string createcallomniid, string omniuploadfilename, string LAResponse)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@ClientID", clientid);
                    p.Add("@PolicyNo", PolicyNo);
                    p.Add("@RequestType", Requesttype);
                    p.Add("@Category", category);
                    p.Add("@CallTypeID", calltypeid);
                    p.Add("@CallTypeDesc", calltypesesc);
                    p.Add("@SubTypeID", subtypeid);
                    p.Add("@SubTypeDesc", subtypedcse);
                    p.Add("@UserInputText", unserinputtext);
                    p.Add("@CreateCallCRMID", createcallcrmid);
                    p.Add("@UpdationCallCRMID", updationcrmcallid); //used this column to store CRMCallUpdate Response
                    p.Add("@CreateCallOmniID", createcallomniid);
                    p.Add("@OmniUplodedFileName", omniuploadfilename);
                    p.Add("@LAResponse", LAResponse);//used this column to store LA Response
                    p.Add("@CallCreatedDate", "");
                    p.Add("@CallUpdationDate", "");
                    return c.Query<int>("_InsertCRMcallCreationandupdation", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Crm call creation with below params
        /// </summary>
        /// <param name="CustomerName"></param>
        /// <param name="Description"></param>
        /// <param name="CallType"></param>
        /// <param name="SubType"></param>
        /// <returns>crm id</returns>
        public string CRMCraeteCall( string CustomerName, string Description, string CallType, string SubType, string policyn, string Client_ID)
        {
            if (policyn.Trim() == "")
            {
                HttpContext.Current.Session["PolicyNo"] = GetPolicyforCRM(Convert.ToString(HttpContext.Current.Session["Client_ID"]));
            }
            else
            {
              //  HttpContext.Current.Session["PolicyNo"] = policyn;
            }
            string strResult;
            try
            {
                WIP_Report.CRMCallCreation.FeedbackClient objFeedbackSvc = new WIP_Report.CRMCallCreation.FeedbackClient();
                WIP_Report.CRMCallCreation.RequestQuery objRequestQuery = new WIP_Report.CRMCallCreation.RequestQuery();
                objRequestQuery.ApplicationNo = WebConfigurationManager.AppSettings["ApplicationNo"];
                objRequestQuery.Bank = WebConfigurationManager.AppSettings["Bank"];
                objRequestQuery.CallSubType = SubType;
                objRequestQuery.CallType = CallType;
                objRequestQuery.ClientId = Client_ID;// Convert.ToString(HttpContext.Current.Session["Client_ID"]);//"50012300";
                objRequestQuery.LoginId = Client_ID;// Convert.ToString(HttpContext.Current.Session["Client_ID"]);// "50115011";
                objRequestQuery.PolicyNo = policyn;// Session["PolicyNo"].tostring();//WebConfigurationManager.AppSettings["ApplicationNo"];
                objRequestQuery.ContactNo1 = "";
                objRequestQuery.ContactNo2 = "";
                objRequestQuery.CustomerName = CustomerName;
                objRequestQuery.Description = Description;
                objRequestQuery.EmailId = "";
                objRequestQuery.QueryType = WebConfigurationManager.AppSettings["QueryType"];
                objRequestQuery.SeverityLevel = WebConfigurationManager.AppSettings["SeverityLevel"];
                objRequestQuery.SudRegion = "";
                objRequestQuery.TypeOfCaller = "";
                objRequestQuery.TypeOfPortal = WebConfigurationManager.AppSettings["TypeOfPortal"];
                strResult = objFeedbackSvc.CreateRequestQuery(objRequestQuery);
            }
            catch (Exception ex)
            {
                Helpercls.LogError(ex);
                strResult = ex.Message;
            }
            return strResult;
        }
        /// <summary>
        /// Active crm call count with clientid
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="callType"></param>
        /// <param name="SubType"></param>
        /// <returns></returns>
        public int CheckCRMActiveCallCnt(string Queryname)
        {
            try
            {
                using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
                {

                    var p = new DynamicParameters();
                    IEnumerable<CRMCallLoginfo> ctst = _GetCRMcalllogdata(Queryname);
                    if (ctst.Count() > 0)
                    {
                        foreach (var crmctst in ctst)
                        {

                            p.Add("@Client_ID", Convert.ToString(HttpContext.Current.Session["Client_ID"]));
                            p.Add("@CRMCallType", crmctst.CallTypeID);
                            p.Add("@CRMSubType", crmctst.SubTypeID);


                        }

                    }

                    return c.Query<int>("GetCRMActiveCall_cnt", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Helpercls.LogError(ex);
                return 1;
            }
        }

        /// <summary>
        /// Update crm call with crm id
        /// </summary>
        /// <param name="callid"></param>
        /// <returns></returns>
        public string CRMcallupdate(string callid)
        {
            //try
            //{
            System.Xml.Linq.XElement xmlTree = System.Xml.Linq.XElement.Parse("<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope' xmlns:tem='http://tempuri.org/'><soap:Header/><soap:Body><tem:SUDCallUpdation><tem:xml><soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'><soap:Body><CallID>" + callid + "</CallID><ResolutionStatus>Request Processed</ResolutionStatus><ResolutionDescription>Request Processed</ResolutionDescription></soap:Body></soap:Envelope></tem:xml></tem:SUDCallUpdation></soap:Body></soap:Envelope>");
            WIP_Report.CRMCallUpdation.Service1SoapClient ctn = new WIP_Report.CRMCallUpdation.Service1SoapClient();
            xmlTree = ctn.SUDCallUpdation(xmlTree);
            var reader = xmlTree.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
            //}
            //catch (Exception ex)
            //{
            //    Helpercls.LogError(ex);
            //}
            //return reader.ReadInnerXml();
        }

        /// <summary>
        /// CRM call Creation and call updation, CRM call log...., 
        /// if you don't want to update crm call please pass crmcallupdation false,
        /// by deafault crm call updation is true
        /// </summary>
        /// <param name="queryname"></param>
        /// <param name="crmcallupdation"></param>
        /// <returns></returns>
     
       

        //Neeraj//

        public IEnumerable<DownloadModel> _CreatecrmcallinfoAddress(string clientid, string PolicyNo, string Requesttype, string category, string calltypeid, string calltypesesc,
                                                        string subtypeid, string subtypedcse, string unserinputtext, string createcallcrmid, string updationcrmcallid,
                                                        string createcallomniid, string omniuploadfilename, string LAResponse)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@ClientID", clientid);
                    p.Add("@PolicyNo", PolicyNo);
                    p.Add("@RequestType", Requesttype);
                    p.Add("@Category", category);
                    p.Add("@CallTypeID", calltypeid);
                    p.Add("@CallTypeDesc", calltypesesc);
                    p.Add("@SubTypeID", subtypeid);
                    p.Add("@SubTypeDesc", subtypedcse);
                    p.Add("@UserInputText", unserinputtext);
                    p.Add("@CreateCallCRMID", createcallcrmid);
                    p.Add("@UpdationCallCRMID", updationcrmcallid); //used this column to store CRMCallUpdate Response
                    p.Add("@CreateCallOmniID", createcallomniid);
                    p.Add("@OmniUplodedFileName", omniuploadfilename);
                    p.Add("@LAResponse", LAResponse);//used this column to store LA Response
                    p.Add("@CallCreatedDate", "");
                    p.Add("@CallUpdationDate", "");
                    // return c.Query<int>("_InsertCRMcallCreationandupdation", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                    return c.Query<DownloadModel>("_InsertCRMcallCreationandupdation", p, null, true, 0, CommandType.StoredProcedure).ToList();
                   // return ite;
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    return null;
                }
            }
        }

        //neeraj//

        public string CrmCallCreateupdationandsave_payment(string queryname, string requesttype, bool crmcallupdation, string policyn, string reqDes, string LARes,string Client_ID)
        {

            // crmcallupdation = true;
            string response = "";
            var crmupdate = "";

            try
            {
                if (!string.IsNullOrEmpty(queryname))
                {
                    IEnumerable<CRMCallLoginfo> ctst = _GetCRMcalllogdata(queryname);
                    if (ctst.Count() > 0)
                    {
                        foreach (var crmctst in ctst)
                        {
                            response = CRMCraeteCall("", reqDes, crmctst.CallTypeID, crmctst.SubTypeID, policyn,  Client_ID);

                            if (!string.IsNullOrEmpty(response) && response != "Object reference not set to an instance of an object")
                            {

                                if (crmcallupdation == true && LARes == "Success")
                                {
                                    crmupdate = CRMcallupdate(response);
                                }

                            }
                            else
                            {
                                response = "Technical Error. Please try after sometime.";
                            }
                            var ctn = _Createcrmcallinfo(Client_ID, policyn, requesttype, crmctst.RCategary, crmctst.CallTypeID, crmctst.CallTypeDes, crmctst.SubTypeID, crmctst.SubTypeDes, reqDes, response, crmupdate, "", "", LARes);
                        }

                    }
                    else
                    {
                        response = "CTST Not exit in Database";
                    }

                }
                else
                {
                    response = "Request type is not correct";
                }
            }
            catch (Exception mvr)
            {
                Helpercls.LogError(mvr);
                response = mvr.Message;
            }


            return response;
        }


        public int ContactUpdateOTPdetails(string client_id, string PolicyNo, string OtpSendFor, string OTPText, string OTPSEmail, string @OTPSMobile, string @OTPResponseEmail, string OTPResponseMob)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@Client_ID", client_id);
                    p.Add("@PolicyNo", PolicyNo);
                    p.Add("@OtpSendFor", OtpSendFor);
                    p.Add("@OTPText", OTPText);
                    p.Add("@OTPSEmail", OTPSEmail);
                    p.Add("@OTPSMobile", OTPSMobile);
                    p.Add("@OTPResponseEmail", OTPResponseEmail);
                    p.Add("@OTPResponseMob", OTPResponseMob);

                    return c.Query<int>("addOTPMaster", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    return 0;
                }
            }
        }
        public int getContactUpdateOTPcnt(string client_id, string OtpSendFor, string OTPOn, string OtpTyp)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@Client_ID", client_id);
                    p.Add("@OtpSendFor", OtpSendFor);
                    p.Add("@OTPSOn", OTPOn);
                    p.Add("@OtPType", OtpTyp);



                    return c.Query<int>("GetOTP_cnt", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Send OTP to mobile and email
        /// </summary>
        /// <param name="tomobile"></param>
        /// <param name="toemail"></param>
        /// <param name="otpsubject"></param>
        /// <returns>Setting Otp in session</returns>
        public string OTPsendphoneemail(string tomobile, string toemail, string otpsubject, string otptype)
        {
            // HttpContext.Current.Session["Customer_Name"] = "venky";
            //HttpContext.Current.Session["Email_otp"] = "";
            //HttpContext.Current.Session["Mobile_OTP"] = "";
            if (otptype == "email")
            {
                objLogin.Email = toemail;
                // Session["strtype"] = "Client email";
            }
            else if (otptype == "alemail")
            {
                objLogin.Email = toemail;
                // Session["strtype"] = "Alternate Client email";
            }
            else if (otptype == "phone")
            {
                objLogin.Mobile = tomobile;

                //Session["strtype"] = "Client Mobile";
            }
            else if (otptype == "alphone")
            {
                objLogin.Mobile = tomobile;
                //Session["strtype"] = "Alternate Client Mobile";
            }
            // Convert.ToString(HttpContext.Current.Session["Email"]);
            /*objLogin.Mobile = tomobile;*/ // Convert.ToString(HttpContext.Current.Session["Mobile"]);
            objLogin.Customer_Name = Convert.ToString(HttpContext.Current.Session["Customer_Name"]);
            var responsetytpe = "0";
            string strSmsStatus = "0";
            string strMailStatus = "0";
            string contactEd = "";
            string contactEd1 = "";
            //if (objLogin.Mobile!="")
            //{
            //    contactEd = objLogin.Mobile;
            //    contactEd1 = "Edit-Mobile";
            //}
            //else
            //{
            //    contactEd = objLogin.Email;
            //    contactEd1 = "Edit-Email";
            //}
            try
            {
                //string strOTP = objLoginRP.GenarateOTP(objLogin);

                /*strSmsStatus=3 3time otp sent, strSmsStatus=2 OTP expired, strSmsStatus=1 sent msg*/
                try
                {

                    //Send Mail text
                    StringBuilder msgBody = new StringBuilder();
                    int testotp = 0;

                    if (Convert.ToString(objLogin.Mobile) != null && Convert.ToString(objLogin.Mobile).Trim() != "")
                    {
                        if (Convert.ToInt32(HttpContext.Current.Session["OTPcntm"]) <= 0)
                        {
                            contactEd = objLogin.Mobile;
                            contactEd1 = "Edit-Mobile";
                            testotp = getContactUpdateOTPcnt(Convert.ToString(HttpContext.Current.Session["Client_ID"]), contactEd1, objLogin.Mobile, "0");
                            HttpContext.Current.Session["OTPcnt"] = testotp;
                            HttpContext.Current.Session["OTPcntm"] = testotp;

                        }
                        else
                        {
                            testotp = Convert.ToInt32(HttpContext.Current.Session["OTPcnt"]);

                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(HttpContext.Current.Session["OTPcntp"]) <= 0)
                        {
                            contactEd = objLogin.Email;
                            contactEd1 = "Edit-Email";
                            testotp = getContactUpdateOTPcnt(Convert.ToString(HttpContext.Current.Session["Client_ID"]), contactEd1, objLogin.Email, "1");
                            HttpContext.Current.Session["OTPcnt"] = testotp;
                            HttpContext.Current.Session["OTPcntp"] = testotp;

                        }
                        else
                        {
                            testotp = Convert.ToInt32(HttpContext.Current.Session["OTPcnt"]);

                        }
                    }


                    string OTPText = "Edit-" + contactEd;
                    //int testotp = 

                    //if (testotp != -1)
                    //{

                    //    if (Convert.ToInt32(HttpContext.Current.Session["OTPcnt"]) <= 2)
                    //{
                    string strOTP = Helpercls.GetOTP();

                    if (strOTP != "")
                    {
                        if (Convert.ToString(objLogin.Mobile) != null && Convert.ToString(objLogin.Mobile).Trim() != "")
                        {
                            if (Convert.ToInt32(HttpContext.Current.Session["OTPcntm"]) <= 2)
                            {
                                if (Convert.ToString(objLogin.Mobile).Trim() != "")
                                {
                                    string SMSbody = Convert.ToString(ConfigurationManager.AppSettings["OTPtxtSMS"]);
                                    //Send SMS Text
                                    // string OTPText = "Edit-"+ contactEd;
                                    SMSbody = SMSbody.Replace("#Aadhar#", OTPText).Replace("#otp#", Convert.ToString(strOTP)).Replace("#datetime#", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                    string Response = Helpercls.sendSMSOTP(objLogin.Mobile, SMSbody);//"E001|Success";//Helpercls.sendSMSOTP(objLogin.Mobile, SMSbody);
                                    if (Response == "E001|Success")
                                    {
                                        strSmsStatus = "1";
                                        HttpContext.Current.Session["OTPstr"] = strOTP;
                                        DateTime test = DateTime.Now;
                                        string curtime = test.ToString("mm:ss");
                                        HttpContext.Current.Session["curtime"] = curtime;
                                        ContactUpdateOTPdetails(Convert.ToString(HttpContext.Current.Session["Client_ID"]), Convert.ToString(HttpContext.Current.Session["PolicyNo"]), "Edit-Mobile", SMSbody, "", objLogin.Mobile, "0", "1");
                                    }
                                    //Send SMS text end
                                    HttpContext.Current.Session["OTPcntm"] = Convert.ToInt32(HttpContext.Current.Session["OTPcntm"]) + 1;
                                }

                            }

                            else
                            {
                                responsetytpe = "3";
                            }
                        }
                        if (Convert.ToString(objLogin.Email).Trim() != "")
                        {
                            if (Convert.ToInt32(HttpContext.Current.Session["OTPcntp"]) <= 2)
                            {
                                if (Convert.ToString(objLogin.Email).Trim() != "")
                                {
                                    //string OTPText = "Edit-" + contactEd;
                                    ////Send Mail text
                                    //StringBuilder msgBody = new StringBuilder();
                                    msgBody.AppendLine("<p>Dear " + objLogin.Customer_Name + ",</p>");
                                    msgBody.AppendLine();
                                    msgBody.AppendLine("<p>OTP for generating new password is  as follows:</p>");
                                    msgBody.AppendLine();
                                    //msgBody.AppendLine(String.Format("UserID ID : {0}", System.Convert.ToInt32(txtClientId.Text)));
                                    msgBody.AppendLine(String.Format("OTP : {0}", strOTP));
                                    msgBody.AppendLine();
                                    msgBody.AppendLine("<p>For security reasons, please do not share your login credentials with others.</p>");
                                    msgBody.AppendLine();
                                    msgBody.AppendLine("<p>For any clarification in your Policy/Profile details, please get in touch with SUD Life - Customer Care.</p>");
                                    msgBody.AppendLine();
                                    msgBody.AppendLine("<p>Thanks,");
                                    //msgBody.AppendLine();
                                    msgBody.AppendLine("<br>SUD Life - Customer Care</p>");
                                    //Send Mail text end
                                    string txtSubject = ConfigurationManager.AppSettings["MailSentSubjectPwd"].ToString();
                                    strMailStatus = Helpercls.sendmail(objLogin.Email, txtSubject, msgBody);
                                    //strMailStatus = "1";
                                    // strOTP = "971436";
                                    if (strMailStatus != "0")
                                    {
                                        HttpContext.Current.Session["OTPstr"] = strOTP;
                                        DateTime test = DateTime.Now;
                                        string curtime = test.ToString("mm:ss");
                                        HttpContext.Current.Session["curtime"] = curtime;
                                        ContactUpdateOTPdetails(Convert.ToString(HttpContext.Current.Session["Client_ID"]), Convert.ToString(HttpContext.Current.Session["PolicyNo"]), "Edit-Email", msgBody.ToString(), objLogin.Email, "", "1", "0");
                                        strSmsStatus = "1";
                                    }

                                    HttpContext.Current.Session["OTPcntp"] = Convert.ToInt32(HttpContext.Current.Session["OTPcntp"]) + 1;
                                }

                            }

                            else
                            {
                                responsetytpe = "3";
                            }
                        }
                        //  HttpContext.Current.Session["OTPcnt1"] = Convert.ToInt32(HttpContext.Current.Session["OTPcnt1"]) + 1;


                    }
                    else
                    {

                        HttpContext.Current.Session["OTPstr"] = "";

                    }
                    //}
                    //else
                    //{
                    //    responsetytpe = "3";
                    //}
                    //}
                    //else
                    //{
                    //    strSmsStatus = "4";
                    //}

                }
                catch (Exception ex)
                {
                    // throw ex;
                    Helpercls.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return "0";
                }
                // return strSmsStatus;


            }
            catch (Exception ex)
            {
                Helpercls.LogError(ex);
                responsetytpe = ex.Message;
            }
            if (strSmsStatus != "0")
            {
                responsetytpe = "1";
            }
            return responsetytpe;
        }

        /// <summary>
        /// convert file to Base64 using file URL
        /// </summary>
        /// <param name="fileurl"></param>
        /// <returns>Base 64 data</returns>
        public string FileBase64(string fileurl)
        {
            string imgdata = "";
            if (File.Exists(fileurl))
            {
                FileStream filebase = null;
                try
                {
                    filebase = new FileStream(fileurl, FileMode.Open, FileAccess.Read);
                    byte[] basedata = new byte[(Int32)filebase.Length];
                    filebase.Read(basedata, 0, Convert.ToInt32(filebase.Length));
                    imgdata = Convert.ToBase64String(basedata);
                }
                catch (FileNotFoundException exfile)
                {
                    Helpercls.LogError(exfile);
                    return exfile.Message;
                }
            }
            return imgdata;
        }


        /// <summary>
        /// sending files to Omni services with below param
        /// </summary>
        /// <param name="filedata"></param>
        /// <param name="crmcallid"></param>
        /// <returns>uploading files in omni services</returns>
       

        /// <summary>
        /// Send files to CRM file upload services with below params
        /// </summary>
        /// <param name="crmid"></param>
        /// <param name="filebase64"></param>
        /// <param name="body"></param>
        /// <param name="filename"></param>
        /// <param name="subject"></param>
        /// <returns>uploading files in CRM file upload services</returns>
       
        public string AddLogEmailContact(string Client_ID, string CategoryName, string UserInput, string PolicyNo, string CRMID, string BiztalkResponse,
           string CRMMainTableID)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@ClientId", Client_ID);
                    p.Add("@PolicyNo", PolicyNo);
                    p.Add("@CRMID", CRMID);

                    p.Add("@CategoryName", CategoryName);
                    p.Add("@UserInput", UserInput);
                     p.Add("@BiztalkResponse", BiztalkResponse);
                    p.Add("@CrmBaseId", CRMMainTableID);


                    return c.Query<string>("_AddLogEmailContact", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    return null;
                }
            }
        }



        public string SMSMaster(string Client_ID, string PolicyNo , string UserInput, string CRMID, string CRMMainTableID)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@ClientId", Client_ID);
                    p.Add("@PolicyNo", PolicyNo);
                    p.Add("@CRMID", CRMID);

                  //  p.Add("@CategoryName", CategoryName);
                    p.Add("@UserInput", UserInput);
                    //p.Add("@BiztalkResponse", BiztalkResponse);
                    p.Add("@CrmBaseId", CRMMainTableID);


                    return c.Query<string>("_AddSMSSave", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    return null;
                }
            }
        }


        public string GetPolicyforCRM(string Client_ID)
        {
            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@Client_Id", Client_ID);
                    return c.Query<string>("_GetPolicyforCRM", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    return null;
                }
            }
        }

     
    }
}