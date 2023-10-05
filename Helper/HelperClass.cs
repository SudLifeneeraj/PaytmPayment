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
using System.Xml.Serialization;

namespace WIP_Report.Helper
{
    public class HelperClass
    {
        public static string formatted = "";
        private string GenerateFileName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["XMLFilePath"] + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond + ".XML";
        }

        /// <summary>
        /// This function generates DataSet from TIBCO webservice response and logs response 
        /// </summary> 
        public DataSet GetDataSet(XmlSerializer serializer, object res)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(GetJobReportResponseType));
            DataSet ds = new DataSet();
            try
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, res);
                    string strFileName = GenerateFileName();
                    using (FileStream f = new FileStream(strFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {

                        stream.Position = 0;

                        //ds.ReadXmlSchema(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Cmb.Notifications.UI.NotificationObjects.xsd"));
                        ds.ReadXml(stream);
                        stream.WriteTo(f);
                        f.Close();
                        stream.Close();
                        File.Delete(strFileName);

                    }
                }

            }
            catch (Exception ex)
            {
              //  elog.LogData(ex.Message.ToString(), ConfigurationSettings.AppSettings["ErrorLogFolderPath"].ToString());
            }
            return ds;
        }


        public static string sendSMSOTP(string Mobile, string SMSbody)
        {
            string Response;
            //  Mobile = "8960098555";//"9819610430,9969274317,9158837347";
            // Mobile = Mobile; //"9158837347";
            SendOTP.OTPClient objSMS = new SendOTP.OTPClient();
            return Response = objSMS.SendMessage(Mobile, SMSbody);


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

    }
}