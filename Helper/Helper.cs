using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;
using System.Data;
using WIP_Report.Models;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using WIP_Report.Database_Access_layer;
using WIP_Report.Database_Access_layer;
using System.Net.Mail;

namespace WIP_Report.Helpers
{
    public class Helpercls
    {
        DataAccess objaccess = new DataAccess();
        public static string formatted = "";


        public static string RandomIdGen()
        {
            int vtransactionId;
            Random rand = new Random();
            int vYear = Convert.ToInt32(DateTime.Today.Year.ToString().Substring(3, 1));
            string vMonth = System.DateTime.Today.Month.ToString("#00");
            vtransactionId = rand.Next(00000, 99999);
            string VYearStr = Convert.ToString(vYear);
            string VMonthStr = Convert.ToString(vMonth);
            string vtransactionIdStr = Convert.ToString(vtransactionId);
            string vstrTrans = String.Concat(VYearStr, VMonthStr, vtransactionIdStr);
            if (vstrTrans.Length != 8)
            {
                vtransactionIdStr = 0 + "" + Convert.ToString(vtransactionId);
                vstrTrans = String.Concat(VYearStr, VMonthStr, vtransactionIdStr);

            }
            string vstrTransId = vstrTrans;
            return vstrTransId;
            //int vtransactionId;
            //Random rand = new Random();
            //int vYear = Convert.ToInt32(DateTime.Today.Year.ToString().Substring(3, 1));
            //string vMonth = System.DateTime.Today.Month.ToString("#00");
            //vtransactionId = rand.Next(00000, 99999);
            //string VYearStr = Convert.ToString(vYear);
            //string VMonthStr = Convert.ToString(vMonth);
            //string vtransactionIdStr = Convert.ToString(vtransactionId);
            //string vstrTrans = String.Concat(VYearStr, VMonthStr, vtransactionIdStr);
            //string vstrTransId = vstrTrans;
            //return vstrTransId;
        }



        public static string SendSMS_Service(string Mobile, string SMSbody)
        {
            #region Code for sending SMS with web service
            // Mobile = "9819610430,9969274317,9158837347,8960098555";//"9158837347";
            // Mobile = "9158837347";
            WebService_SMSSoapClient objSMSClient = new WebService_SMSSoapClient();
            SMSInput objSMSInput = new SMSInput();
            objSMSInput.Message = SMSbody.ToString();
            objSMSInput.Flag = "0";
            objSMSInput.Mobile_Number = Mobile;
            objSMSInput.Received_From = "Customer Care";
            InsertSMSInfo objSMSOutput = new InsertSMSInfo();
            objSMSOutput = objSMSClient.SMSRequestResponsePort(objSMSInput);
            InsertSMSInfoInsertStatus objSMSStatus = objSMSOutput.InsertStatus;
            string strSmsStatus = objSMSStatus.Status;
            // elog.LogData("ProfileCRMTableid--" + CrmBaseId, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));


            RegitratioEmailDatasms(strSmsStatus, SMSbody.ToString(), Mobile, "Customer Care");
            #endregion

            return strSmsStatus;
        }
        public static string CRMCallUpdate(string CRMID, string Statustxt)
        {
            string Responsetxt = "";
            //CRMCallUpdation.Service1SoapClient objcallupdate = new CRMCallUpdation.Service1SoapClient();
            //CRMCallUpdation.SUDCallUpdationRequest objtxt = new CRMCallUpdation.SUDCallUpdationRequest();

            //objcallupdate.SUDCallUpdation()

            return Responsetxt;
        }
        public static string sendSMSOTP(string Mobile, string SMSbody)
        {
            string Response;
            //  Mobile = "8960098555";//"9819610430,9969274317,9158837347";
            // Mobile = Mobile; //"9158837347";
            SendOTP.OTPClient objSMS = new SendOTP.OTPClient();
            return Response = objSMS.SendMessage(Mobile, SMSbody);


        }
        public static void RegitratioEmailDatasms(string strMailStatus, string MailBody, string toEmail, string From)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var para = new DynamicParameters();
                    para.Add("@strMailStatus", strMailStatus);
                    para.Add("@MailBody", MailBody);
                    para.Add("@toEmail", toEmail);
                    para.Add("@From", From);


                    con.Query<UserModel>("pro_RegitratioEmailData", para, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    // return null;
                }
            }
        }

        public static void RegitratioEmailData(string strMailStatus, StringBuilder MailBody, string toEmail, string From)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var para = new DynamicParameters();
                    para.Add("@strMailStatus", strMailStatus);
                    para.Add("@MailBody", "");
                    para.Add("@toEmail", toEmail);
                    para.Add("@From", From);


                    con.Query<UserModel>("pro_RegitratioEmailData", para, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                    // return null;
                }
            }
        }

        public static string sendmail(string toEmail, string txtSubject, StringBuilder MailBody)
        {
            string strMailStatus = "0";
            try
            {

                SendingMailSoapClientNew.SendingMail objsendmail = new SendingMailSoapClientNew.SendingMail();
                SendingMailSoapClientNew.SendMail2User_Service service = new SendingMailSoapClientNew.SendMail2User_Service();
                SendingMailSoapClientNew.SendMailInput objMailInput = new SendingMailSoapClientNew.SendMailInput();
                objMailInput.From = ConfigurationManager.AppSettings["MailSentFrom"].ToString();
                objMailInput.To = toEmail;  //"shridevi.swami@sudlife.in";// "shridevi.swami@sudlife.in";//
                // objMailInput.To = "Aparna.Bhattacharjee@sudlife.in,Rupali.ratwadkar@sudlife.in,shridevi.swami@sudlife.in,prajakata.nalawade@sudlife.in,poornima.sharma@sudlife.in,nayan.mhadase@sudlife.in";// "shridevi.swami@sudlife.in";//toEmail;
                objMailInput.Subject = txtSubject;
                objMailInput.Body = MailBody.ToString();
                objMailInput.Path = "";
                objsendmail = service.Operation_1(objMailInput);
                strMailStatus = objsendmail.MailSend.MailStatus;
                RegitratioEmailData(strMailStatus, MailBody, toEmail, objMailInput.From);

                if (strMailStatus == "Success")
                { strMailStatus = "1"; }
            }
            catch
            {
                // strMailStatus = "0";
                strMailStatus = "1";
            }
            return strMailStatus;
        }

        public static void LogError(Exception exception)
        {
            if (ConfigurationManager.AppSettings["EnableErrorLog"].ToString().ToUpper() == "Y")
            {
                string conString = ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ConnectionString;
                SqlConnection con = null;
                try
                {
                    StringBuilder sbExceptionMessage = new StringBuilder();
                    do
                    {
                        sbExceptionMessage.Append("Exception Type" + Environment.NewLine);
                        sbExceptionMessage.Append(exception.GetType().Name);
                        sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
                        sbExceptionMessage.Append("Message" + Environment.NewLine);
                        sbExceptionMessage.Append(exception.Message + Environment.NewLine + Environment.NewLine);
                        sbExceptionMessage.Append("Stack Trace" + Environment.NewLine);
                        sbExceptionMessage.Append(exception.StackTrace + Environment.NewLine + Environment.NewLine);

                        exception = exception.InnerException;
                    }
                    while (exception != null);

                    con = new SqlConnection(conString);
                    SqlCommand cmd = new SqlCommand("usp_ErrorLog", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter("@ExceptionMessage", sbExceptionMessage.ToString());
                    cmd.Parameters.Add(parameter);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    Helpercls.LogError(ex);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }
        public static string GetOTP()
        {
            string numbers = "1234567890";

            string characters = numbers;
            //if (rbType.SelectedItem.Value == "1")
            //{
            //    characters += alphabets + small_alphabets + numbers;
            //}
            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }

            return otp;
        }


        #region "Password Generation of specific format"

        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private static string RandomSpecialChar()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            if (random.Next(34, 36) == 35)
                ch = Convert.ToChar(35);
            else
                ch = Convert.ToChar(64);
            builder.Append(ch);
            return builder.ToString();
        }

        public static string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, true));
            builder.Append(RandomString(1, false));
            builder.Append(RandomSpecialChar());
            builder.Append(RandomNumber(100, 999));

            return builder.ToString();
        }
        #endregion



        public static string PremiumReceipt(string Policy, string StatementType, string FromDate, string Todate, string yearbox)
        {



            return "";
        }


        public static string GetUnitStatement(string Policy, string StatementType, string FromDate, string Todate, string yearbox)
        {
            string lnk = "";
            try
            {
                Policy = Policy;// "00755501";

                string valPolicy = "";
                string valYear = "";
                string valMonth = "";
                int statusCount;
                DataTable dtfundValue = new DataTable();
                string FundStmtURLs = Convert.ToString(ConfigurationSettings.AppSettings["FundStmtURL"]);

                if (FromDate != "")
                {
                    string sMonth1 = FromDate.Substring(0, 2);

                    string sMonth = sMonth1.Replace("/", "");


                    string sYear = FromDate.Substring(FromDate.Length - 4);

                    valPolicy = Policy.Trim();
                    valYear = sYear.Trim();
                    valMonth = sMonth.Trim();

                    lnk = FundStmtURLs + "&policy_no=" + valPolicy + "&key=7&month=" + valMonth + "&year=" + valYear + "&login=CustomerPortal";
                }
                else
                {
                    yearbox = yearbox + "-" + Convert.ToString(Convert.ToInt32(yearbox) + 1);
                    valPolicy = Policy;
                    valYear = yearbox;
                    //  valYear = valYear.Substring(0, 4);
                    valMonth = "0";
                    //  lblStatus.Text = "";
                    lnk = FundStmtURLs + "&policy_no=" + valPolicy + "&key=6&financial_year=" + valYear + "&login=CustomerPortal";
                }

                //string SPFundValue = "SP_FundStatement";
                ////int status;
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString());
                //SqlCommand cmd;
                //con.Open();
                //cmd = new SqlCommand(SPFundValue, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@PolicyNumber", valPolicy);
                //cmd.Parameters.AddWithValue("@Month", valMonth);
                //cmd.Parameters.AddWithValue("@Year", valYear);
                //SqlDataReader rd;
                //rd = cmd.ExecuteReader();
                //if (rd.HasRows)
                //{
                //    while (rd.Read())
                //    {
                //        statusCount = Convert.ToInt32(rd["cnt"].ToString());
                //        if (statusCount > 0)
                //        {

                // lblStatus.Text = "";
                StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");
                // sb.Append(@"window.open('" + lnk + "', '_self', 'scrollbars=yes,width=500,height=500,resizable=yes');");
                sb.Append(@"window.location.assign('" + lnk + "');");
                sb.Append(@"</script>");
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "ajax", sb.ToString(), false);
                //lnk = "N";                                                                                                  
                //        }
                //        else
                //        {
                //            // lblStatus.Text = "Unit statement cannot be generated for this policy.";
                //        }

                //    }

                //}
                // rd.Close();
                // con.Close();
            }
            catch (Exception ex)
            {
                Helpercls.LogError(ex);
                // log.LogData("Fundstatement.cs:254,Inside catch:" + ex.Message, Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
            }
            return lnk;
        }

        public static string EPolicydownload(string Policy, string StatementType, string FromDate, string Todate, string yearbox)
        {
            var Response = System.Web.HttpContext.Current.Response;
            try
            {
                Policy = "90354060";

                string selectedEPolicy = Policy.ToString() + ".pdf";

                string docIndexx = "";
                string cs = ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString();

                SqlConnection con = new SqlConnection(cs);

                SqlCommand cmd;

                con.Open();

                cmd = new SqlCommand("NG_SUD_Fetch_ImageIndex", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PolicyNumber", Policy);  //Here policyNo is application no.

                SqlDataReader rd;
                rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {


                    while (rd.Read())
                    {

                        docIndexx = rd.GetValue(0).ToString();
                        // string docIndex = "17392665";
                        string filename = selectedEPolicy;

                        //string EndPointurl = "http://10.1.22.7:8080/axis2/services/NGOImageEnableServiceImp";
                        string EndPointurl = Convert.ToString(ConfigurationSettings.AppSettings["EpolicyEnpointURL"]);
                        string CabinetName = Convert.ToString(ConfigurationSettings.AppSettings["EpolicyCabinetName"]);
                        string JtsAddress = Convert.ToString(ConfigurationSettings.AppSettings["EpolicyJtsAddress"]);
                        string PortId = Convert.ToString(ConfigurationSettings.AppSettings["EpolicyPortId"]);
                        string SiteId = Convert.ToString(ConfigurationSettings.AppSettings["EpolicySiteId"]);
                        string VolumeId = Convert.ToString(ConfigurationSettings.AppSettings["EpolicyVolumeId"]);
                        String xmlRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:prov=\"http://provider.ws.jts.omni.newgen.com\" xmlns:xsd=\"http://getdoc.bdo.ws.jts.omni.newgen.com/xsd\"><soapenv:Header/><soapenv:Body>"
                                    + "<prov:downloadDocInFile>"
                                    + "<prov:param0>"
                                    + "<xsd:cabinetName>" + CabinetName + "</xsd:cabinetName>"
                                    + "<xsd:docIndex>" + docIndexx + "</xsd:docIndex>"
                                    + "<xsd:fileName>" + filename + "</xsd:fileName>"
                                    + "<xsd:jtsAddress>" + JtsAddress + "</xsd:jtsAddress>"
                                    + "<xsd:portId>" + PortId + "</xsd:portId>"
                                    + "<xsd:siteId>" + SiteId + "</xsd:siteId>"
                                    + "<xsd:volumeId>" + VolumeId + "</xsd:volumeId>"
                                    + "</prov:param0>"
                                    + "</prov:downloadDocInFile></soapenv:Body></soapenv:Envelope>";



                        string xmlContent = xmlRequest;
                        string URL = EndPointurl;

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                        request.Method = "POST";
                        request.ContentType = "text/xml; encoding='utf-8'";
                        byte[] bytes;
                        bytes = System.Text.Encoding.ASCII.GetBytes(xmlRequest);
                        request.ContentLength = bytes.Length;
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(bytes, 0, bytes.Length);
                        requestStream.Close();

                        try
                        {

                            using (var webResponse = request.GetResponse())
                            using (var webStream = webResponse.GetResponseStream())
                            {
                                if (webStream != null)
                                {

                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.ContentType = "application/pdf";
                                    Response.AddHeader("Content-Disposition", "attachment; filename=" + selectedEPolicy);
                                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                    webStream.CopyTo(Response.OutputStream);
                                    //  EPolicyLogInsertion();
                                    Response.Flush();
                                    Response.End();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Helpercls.LogError(ex);
                            Response.Write(ex.Message);


                        }


                    }
                }
                else
                {
                    // lblEPolicyMsg.Visible = true;
                    //  lblEPolicyMsg.Text = "No E-Policy Found.";


                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);


            }


            return "";
        }

        /*BRS Email- Send mail and SMS fuctions - added on 19 Jun 2014 Starts herer*/
        /// <summar y>
        /// Send Mail to Using using Biztalk Service
        /// </summary>
        /// <param name="strSendTo"></param>
        /// <param name="strFrom"></param>
        /// <param name="strSubject"></param>
        /// <param name="strBody"></param>
        /// <param name="downloadBytes"></param>
        /// <returns>Mail send status string</returns>
        public static string BizSendEmail1(string strSendTo, string strFrom, string strSubject, string strBody, string strFilePath)
        {
            //System.IO.MemoryStream msFile = new System.IO.MemoryStream(downloadBytes);
            SendingMailSoapClientNew.SendingMail sendingmail = new SendingMailSoapClientNew.SendingMail();
            SendingMailSoapClientNew.SendMail2User_Service service = new SendingMailSoapClientNew.SendMail2User_Service();
            SendingMailSoapClientNew.SendMailInput objMailInput = new SendingMailSoapClientNew.SendMailInput();
            //ConfigurationManager
            objMailInput.From = strFrom;
            objMailInput.To = strSendTo; //"shridevi.swami@sudlife.in";
            // objMailInput.To = "Aparna.Bhattacharjee@sudlife.in,Rupali.ratwadkar@sudlife.in,shridevi.swami@sudlife.in,prajakata.nalawade@sudlife.in,poornima.sharma@sudlife.in,nayan.mhadase@sudlife.in";// "shridevi.swami@sudlife.in";//toEmail;
            objMailInput.Subject = strSubject;
            objMailInput.Body = strBody;
            objMailInput.Path = strFilePath;

            sendingmail = service.Operation_1(objMailInput);
            // 
            return sendingmail.MailSend.MailStatus;
        }


        public static string BizSendEmail(string strSendTo, string strSendFrom, string strSubject, string strMailBody, string newval)
        {

            string tmpStringContent = string.Empty;
            string strMailStatus = string.Empty;
            DateTime dt = DateTime.Now;
            string EmailSubject = ConfigurationManager.AppSettings["SuccessfulMailSubject"];

            try
            {
                using (MailMessage messageObject = new MailMessage())
                {

                    // MailMessage messageObject = new MailMessage();

                    //string formatted = dt.ToString("dd-MM-yyyy");
                    //string htmlString = @"<html><body> Dear " + username + " ," +
                    //  "<br><br>Thank you for sharing your information. We have received your request for enrolment and the same will be processed shortly. Your request id is " + RequestID + "." +
                    //  "<br><br><b>Note:</b>This is an automated mail. Please do not reply to this mail." +
                    //  "<br><br> " +
                    //  "Thanks & Regards,<br>SUDLife </body></ html > ";

                    //string to = useremail;
                    //string bcc = ConfigurationManager.AppSettings["BCC"];
                    string fmail = ConfigurationManager.AppSettings["MailSentFrom"];

                    messageObject.To.Add(strSendTo);
                    //messageObject.Bcc.Add(bcc);
                    messageObject.From = new MailAddress(fmail);
                    messageObject.Subject = strSubject + "  " + Convert.ToString(formatted);
                    messageObject.IsBodyHtml = true;
                    messageObject.Body = strMailBody;

                    //

                    //messageObject.Attachments.Add(new Attachment(@filepath));
                    // your remote SMTP server IP.

                    // string sSmtpServer = "";
                    string sSmtpServer = "", sUser = "", sPWD = "";
                    sSmtpServer = ConfigurationManager.AppSettings["SMTPServer"];
                    //sUser = ConfigurationManager.AppSettings["SMTP_Username"];
                    //sPWD = ConfigurationManager.AppSettings["SMTP_Password"];
                    SmtpClient a = new SmtpClient();
                    a.Host = sSmtpServer;
                    //a.Credentials = new System.Net.NetworkCredential(sUser, sPWD);
                    a.Send(messageObject);
                }
                strMailStatus = "Success";

            }
            catch (Exception ex)
            {
                strMailStatus = "Failure";
                throw new Exception(ex.Message);
            }
            return strMailStatus;
        }


        /// <summary>
        /// Send SMS using Service
        /// </summary>
        /// <param name="strSMSBody"></param>
        /// <param name="strRecipientNo"></param>
        /// <param name="strSender"></param>
        /// <returns></returns>
        /// 


        //public static string PopulateBody(GenericGatewayCallResponse resobj,string strAddress)
        //{
        //    DateTime dt = DateTime.Now.Date;
        //    string body = string.Empty;
        //    using (StreamReader reader = new StreamReader(ConfigurationManager.AppSettings["EmailBodyPath"]))
        //    {
        //        body = reader.ReadToEnd();
        //    }

        //    body = body.Replace("#ImageLogo#", ConfigurationManager.AppSettings["ImgLogo"]);
        //    formatted = dt.ToString("dd/MM/yyyy");
        //    body = body.Replace("#currentdate#", Convert.ToString(formatted));
        //    body = body.Replace("#policyholdername#", resobj.CustomerName);
        //    body = body.Replace("#address#", strAddress);
        //    body = body.Replace("#policyno#", resobj.PolicyApplicationNo);
        //    body = body.Replace("#paymenttype#", strPolicyType);
        //    //StringBuilder html = new StringBuilder();
        //    //string EmailBody = html.ToString();

        //    //body = body.Replace("{Body}", EmailBody);

        //   // Msg.Subject = EmailSubject + "  " + Convert.ToString(formatted);


        //    return body;
        //}

    }
}