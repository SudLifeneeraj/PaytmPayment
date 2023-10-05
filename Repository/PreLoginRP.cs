using WIP_Report.Helper;
using WIP_Report.Models;
using System;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace WIP_Report.Repository
{
    public class PreLoginRP
    {

        ErrorLog elog = new ErrorLog();
        PreLogin objLogin = new PreLogin();
        const string hashProvider = "SHA512Managed";
        const string providerName = "i:0#.f|aspnetsqlmembershipprovider|";
        string pswd = string.Empty;
        public int GetTransactionId(string stTransactionId)
        {

            using (SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString()))
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@TransactionId", stTransactionId);

                    return c.Query<int>("Get_transaction_ID", p, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                    // rowCount = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    return 0;
                }
            }
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
                    HelperClass.LogError(ex);
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
                    HelperClass.LogError(ex);
                    return 0;
                }
            }
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
            //StringBuilder builder = new StringBuilder();
            //builder.Append(RandomString(4, true));
            //builder.Append(RandomSpecialChar());
            //builder.Append(RandomNumber(100, 999));
            //return builder.ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(3, true));
            builder.Append(RandomString(1, false));
            builder.Append(RandomSpecialChar());
            builder.Append(RandomNumber(100, 999));

            return builder.ToString();
        }
        private string GetSaltValue(string C_ID, string Utype)
        {
            //  string pswd = PWD;
            string strSaltValue = GetPWDSalt(C_ID, Utype);
            // string strHashedPwd = EncodePassword(pswd, strSaltValue);

            return strSaltValue;
        }
        public string GetPWDSalt(string CID, string Utype)
        {
            // string strsalt = "";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                con.Open();
                try
                {
                    var para = new DynamicParameters();
                    para.Add("@ClientId", CID);
                    para.Add("@Utype", Utype);
                    return con.Query<string>("usp_LogUserPWDSalt", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();


                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }
            }

            // return strsalt;
        }



        private string GenerateSaltValue()
        {
            int SaltValueSize = 6;

            StringBuilder builder = new StringBuilder();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] salt = new byte[SaltValueSize];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(salt);

            foreach (byte outputByte in salt)
                builder.Append(outputByte.ToString("x2").ToUpper());

            return builder.ToString();
        }



        //public static string DecryptString(string key, string cipherText)
        //{
        //    byte[] iv = new byte[16];
        //    byte[] buffer = Convert.FromBase64String(cipherText);

        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = Encoding.UTF8.GetBytes(key);

        //        aes.IV = iv;

        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        //        using (MemoryStream memoryStream = new MemoryStream(buffer))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
        //                {
        //                    return streamReader.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}


        private string EncodePassword(string plainpass, string salt)
        {
            // var key = "72B56C6249387735732A29D3619C8684E8C44647DAB853B2";
            //var strvar = "S/0dPRRoD2CcaIMkXrPTqU4bACE=";
            //var inputstr = DecryptString(key, strvar);

            byte[] buffer1 = Encoding.Unicode.GetBytes(plainpass);
            byte[] buffer2 = Convert.FromBase64String(salt);
            byte[] buffer3 = new byte[buffer2.Length + buffer1.Length];
            byte[] buffer4 = null;
            System.Buffer.BlockCopy(buffer2, 0, buffer3, 0, buffer2.Length);
            System.Buffer.BlockCopy(buffer1, 0, buffer3, buffer2.Length, buffer1.Length);
            HashAlgorithm algorithm1 = HashAlgorithm.Create(Membership.HashAlgorithmType);
            buffer4 = algorithm1.ComputeHash(buffer3);
            return Convert.ToBase64String(buffer4);
            //byte[] buffer1 = Encoding.Unicode.GetBytes(plainpass);
            //byte[] buffer2 = Convert.FromBase64String(salt);
            //byte[] buffer3 = new byte[buffer2.Length + buffer1.Length];
            //byte[] buffer4 = null;
            //System.Buffer.BlockCopy(buffer2, 0, buffer3, 0, buffer2.Length);
            //System.Buffer.BlockCopy(buffer1, 0, buffer3, buffer2.Length, buffer1.Length);
            //HashAlgorithm algorithm1 = HashAlgorithm.Create("SHA256");

            //// HashAlgorithm algorithm1 = HashAlgorithm.Create(Membership.HashAlgorithmType);
            //buffer4 = algorithm1.ComputeHash(buffer3);
            //return Convert.ToBase64String(buffer4);
             //byte[] bytes = Encoding.Unicode.GetBytes(plainpass);
             //byte[] src = Encoding.Unicode.GetBytes(salt);
             //byte[] dst = new byte[src.Length + bytes.Length];
             //System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
             //System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
             //HashAlgorithm algorithm = HashAlgorithm.Create("SHA512Managed");
             //byte[] inArray = algorithm.ComputeHash(dst);
             //return Convert.ToBase64String(inArray);    
            ////// return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }
        #endregion
        public UserModel CheckUserByLoginId(PreLogin objLogin)
        {
            using (SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                //con.Close();
                con1.Open();
               // elog.LogData("PreLoginRP.output 24:" + Convert.ToString(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()) + "File:Website PremiumReceiptController: Message: SP call", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                try
                {
                    //string PWD = "sud@123";
                    //string strSaltValue = "0A93B1810BE1";

                    //string strHashedPwd = EncodePassword(PWD, strSaltValue);
                    string PWD = objLogin.Password;
                    string strSaltValue = GetSaltValue(objLogin.LoginId, objLogin.UserType);
                   // elog.LogData("PreLoginRP.output 267:strSaltValue: " + Convert.ToString(strSaltValue) + " File:Website PreLoginRP: Message: SP call", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                    if (strSaltValue != "")
                    {
                        string strHashedPwd = EncodePassword(PWD, strSaltValue);
                        
                        var para = new DynamicParameters();
                        para.Add("@LoginId1", objLogin.LoginId);
                        para.Add("@Pwd", strHashedPwd);
                        para.Add("@PWDSalt", strSaltValue);
                        para.Add("@UserIdType", "1");//objLogin.UserType);
                        // var user = con.Execute("usp_ValidateCustomerPortalUser", para, null, 0, CommandType.StoredProcedure);
                        //return con.Query<UserModel>("usp_ValidateCustomerPortalUser", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                        return con1.Query<UserModel>("usp_ValidateCustomerPortalUser_New", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                    }
                    else
                    {
                        return null;
                    }
                   // con.Close();
                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }
            }
            
        }

        public int Log_UserLogin_update(UserModel objuser)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                elog.LogData("PreLoginRP.output 433:" + Convert.ToString(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()) + "File:Website PremiumReceiptController: Message: SP call", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                try
                {
                    var para = new DynamicParameters();


                    para.Add("@ClientId", Convert.ToString(HttpContext.Current.Session["Client_ID"]));
                    // para.Add("@UserRole", Convert.ToString((object)objuser.Role));
                    // para.Add("@IP", IP);
                    // para.Add("@UserAgent", UserAgent);
                    //para.Add("@SessionId", SessionID);
                    var result = con.Query<int>("usp_LogUserLoginupdate", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                    return result;
                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return 0;
                }
            }
        }


        public UserModel CheckUserByLoginWOTP(PreLogin objLogin)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                elog.LogData("PreLoginRP.output 92:" + Convert.ToString(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()) + "File:Website PremiumReceiptController: Message: SP call", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                try
                {

                    var para = new DynamicParameters();
                    para.Add("@LoginId1", objLogin.LoginId);
                    para.Add("@UserIdType", objLogin.UserType);
                    // var user = con.Execute("usp_ValidateCustomerPortalUser", para, null, 0, CommandType.StoredProcedure);
                    //return con.Query<UserModel>("usp_ValidateCustomerPortalUser", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                    return con.Query<UserModel>("usp_ValidateCPUserWOTP", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }
            }
        }


        public IEnumerable<UserModel> CheckUserForForgotPWD(PreLogin objLogin)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                elog.LogData("PreLoginRP.output 119:" + Convert.ToString(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()) + "File:Website PremiumReceiptController: Message: SP call", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                try
                {

                    var para = new DynamicParameters();
                    para.Add("@LoginId1", objLogin.LoginId);
                    para.Add("@UserIdType", objLogin.UserType);
                    return con.Query<UserModel>("CP_Usercheck_ForgotPassword", para, null, true, 0, CommandType.StoredProcedure).ToList();
                    // var user = con.Execute("usp_ValidateCustomerPortalUser", para, null, 0, CommandType.StoredProcedure);
                    //return con.Query<UserModel>("usp_ValidateCustomerPortalUser", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                    // return con.Query<UserModel>("CP_Usercheck_ForgotPassword", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }
            }
        }





        public IEnumerable<UserModel> Log_UserLogin_Token(PreLogin objLogin)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                elog.LogData("PreLoginRP.output 119:" + Convert.ToString(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()) + "File:Website PremiumReceiptController: Message: SP call", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                try
                {

                    var para = new DynamicParameters();
                    para.Add("@ClientId", objLogin.LoginId);
                    para.Add("@token", objLogin.token);
                    return con.Query<UserModel>("UpdateTokenDataNew", para, null, true, 0, CommandType.StoredProcedure).ToList();
                    // var user = con.Execute("usp_ValidateCustomerPortalUser", para, null, 0, CommandType.StoredProcedure);
                    //return con.Query<UserModel>("usp_ValidateCustomerPortalUser", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                    // return con.Query<UserModel>("CP_Usercheck_ForgotPassword", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }
            }
        }





        public string checkOTP(string txtotp)
        {
            HttpContext.Current.Session["OTPstr"] = "00122";
            string chkOTP = "0";
            string teststr = Convert.ToString(HttpContext.Current.Session["OTPstr"]);
            if (Convert.ToString(HttpContext.Current.Session["OTPstr"]) != "" && HttpContext.Current.Session["OTPstr"].Equals(txtotp))
            {
                DateTime test1 = DateTime.Now;
                TimeSpan ts1;
                TimeSpan ts2 = TimeSpan.Parse("00:" + test1.ToString("mm:ss")); //"3:30"
                TimeSpan ts3 = TimeSpan.Parse(Convert.ToString(ConfigurationManager.AppSettings["PANCARDAadharOTPExpireTime"]));
                string cntaccess = "0";
                if (Convert.ToString(HttpContext.Current.Session["curtime"]) != "")
                {
                    ts1 = TimeSpan.Parse("00:" + Convert.ToString(HttpContext.Current.Session["curtime"])); //"1:35"
                    if ((ts2 - ts1) < ts3)
                        cntaccess = "1";

                }
                else
                {
                    ts1 = TimeSpan.Parse("00:00:00");
                    cntaccess = "2";
                }

                chkOTP = cntaccess;
            }
            else
            {
                chkOTP = "2";
            }
            return chkOTP;
        }


        public string GenarateOTP(PreLogin objLogin, string OTPFor)
        {
            var OTPcnt = 0;
            string strSmsStatus = "0";
            string strMailStatus = "0";
            /*strSmsStatus=3 3time otp sent, strSmsStatus=2 OTP expired, strSmsStatus=1 sent msg*/
            if (OTPFor == "")
            {
                OTPFor = "Forget Password";
            }
            try
            {
                int testotp = 0;
                if (OTPcnt <= 0)
                {
                    if (Convert.ToString(objLogin.Mobile) != null && Convert.ToString(objLogin.Mobile).Trim() != "")
                    {
                        testotp = getContactUpdateOTPcnt(objLogin.LoginId, OTPFor, objLogin.Mobile, "0");
                        OTPcnt = testotp;
                    }
                    else
                    {
                        testotp = getContactUpdateOTPcnt(objLogin.LoginId, OTPFor, objLogin.Email, "1");
                        OTPcnt = testotp;

                    }
                }
                else
                {
                    testotp = OTPcnt;

                }

                //int testotp = 

                if (testotp != -1)
                {
                    if (OTPcnt <= 2)
                    {
                        string strOTP = HelperClass.GetOTP();

                        if (strOTP != "")
                        {
                            if (Convert.ToString(objLogin.Mobile) != null && Convert.ToString(objLogin.Mobile).Trim() != "")
                            {
                                string SMSbody = Convert.ToString(ConfigurationManager.AppSettings["OTPtxtSMS"]);
                                //Send SMS Text
                                string OTPText = OTPFor;
                                SMSbody = SMSbody.Replace("#Aadhar#", OTPText).Replace("#otp#", Convert.ToString(strOTP)).Replace("#datetime#", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                string Response = HelperClass.sendSMSOTP(objLogin.Mobile, SMSbody);//"E001|Success";//Helpercls.sendSMSOTP(objLogin.Mobile, SMSbody);
                                if (Response == "E001|Success")
                                {
                                    strSmsStatus = "1";
                                   // HttpContext.Current.Session["OTPstr"] = strOTP;
                                    DateTime test = DateTime.Now;
                                    string curtime = test.ToString("mm:ss");
                                   // HttpContext.Current.Session["curtime"] = curtime;
                                    ContactUpdateOTPdetails(objLogin.LoginId, objLogin.LoginId, OTPFor, SMSbody, "", objLogin.Mobile, "0", "1");
                                }
                                //Send SMS text end
                            }
                            if (Convert.ToString(objLogin.Email).Trim() != "")
                            {

                                //Send Mail text
                                StringBuilder msgBody = new StringBuilder();
                                msgBody.AppendLine("<p>Dear " + objLogin.Customer_Name + ",</p>");
                                msgBody.AppendLine();
                                msgBody.AppendLine("<p>OTP for contact details change is  as follows:</p>");
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
                                strMailStatus = HelperClass.sendmail(objLogin.Email, txtSubject, msgBody);
                                //strMailStatus = "1";
                                // strOTP = "971436";
                                if (strMailStatus != "0")
                                {
                                   // HttpContext.Current.Session["OTPstr"] = strOTP;
                                    DateTime test = DateTime.Now;
                                    string curtime = test.ToString("mm:ss");
                                   // HttpContext.Current.Session["curtime"] = curtime;
                                   // HttpContext.Current.Session["OTPcnt"] = Convert.ToInt32(HttpContext.Current.Session["OTPcnt"]) + 1;
                                    ContactUpdateOTPdetails(objLogin.LoginId, objLogin.LoginId, OTPFor, msgBody.ToString(), objLogin.Email, "", "1", "0");
                                    strSmsStatus = "1";
                                }


                            }
                        }
                        else
                        {

                            //HttpContext.Current.Session["OTPstr"] = "";

                        }
                    }
                    else
                    {
                        strSmsStatus = "3";
                    }
                }
                else
                {
                    strSmsStatus = "4";
                }


            }
            catch (Exception ex)
            {
                // throw ex;
                HelperClass.LogError(ex);
                //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                //return "0";
            }
            return strSmsStatus;

        }

    }
}