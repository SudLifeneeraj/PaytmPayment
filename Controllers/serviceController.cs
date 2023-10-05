using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using WIP_Report.Repository;
using WIP_Report.Models;
using WIP_Report.Helper;
using System.Web.Mvc;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Web.Http;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using System.Web;

namespace WIP_Report.Controllers
{

    public class serviceController : ApiController
    {
        string response = "0";
        PreLogin objLogin = new PreLogin();
        PreLoginRP objLoginRP = new PreLoginRP();
        HelperClass objHelp = new HelperClass();

        //int strProduct;
        int appcnt;
        string s1, s2;

        [HttpPost]
        public ActionResult GetData(PreLogin inp)
        {


            JsonResult result = new JsonResult();
            LoginResponse loginR = new LoginResponse();
            string userid = inp.LoginId, password = inp.Password, ddlUserType = inp.UserType, CPParam1 = inp.CPParam1, CPParam2 =inp.CPParam2;
            try
            {
                //password = DecryptStringFromBytes(Convert.FromBase64String(password), Encoding.UTF8.GetBytes(CPParam1), Encoding.UTF8.GetBytes(CPParam2));
                response = "0";

                //Session.Abandon();
                //Session.Clear();
                if (userid != null && password != null)
                {
                    if (userid != "" && password != "" && ddlUserType != "")
                    {
                      
                        objLogin.LoginId = userid;
                        objLogin.Password = password;
                        objLogin.UserType = ddlUserType;

                        var User = objLoginRP.CheckUserByLoginId(objLogin);
                        
                        if (User != null)
                        {
                            response = "1";
                            long i = 1;

                            foreach (byte b in Guid.NewGuid().ToByteArray())
                            {
                                i *= ((int)b + 1);
                            }

                            string BatchID = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);




                            // string changepwd = User.PasswordChanged;
                            int objcnt = objLoginRP.Log_UserLogin_update(User);

                        }

                    }
                }
                else
                {
                    // ViewBag.Message = "No record found. Please try with valid details.";
                    // return View("../PreLogin/Index");
                }
            }
            catch (Exception ex)
            {
                HelperClass.LogError(ex);
                //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                //  return null;
            }

            if(response == "1")
            {
                //UserModel obj = new UserModel();
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string token = Convert.ToBase64String(time.Concat(key).ToArray());
                //  int objcnt = objLoginRP.Log_UserLogin_Token(User);
                objLogin.token = token;
                objLogin.LoginId = userid;
                IEnumerable<UserModel> cts = objLoginRP.Log_UserLogin_Token(objLogin);

                

                loginR.token = token;
                loginR.ResponseStatusCode = HttpStatusCode.OK.ToString();
                loginR.ResponseStatus = "success";

                result.Data = loginR;
     


                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                result.MaxJsonLength = int.MaxValue;
                return result;

               // return Request.CreateResponse(HttpStatusCode.OK, result);


            }

            

            loginR.token = null;
            loginR.ResponseStatusCode = HttpStatusCode.NotFound.ToString();
            loginR.ResponseStatus = "faliure";

            result.Data = loginR;



            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.MaxJsonLength = int.MaxValue;
            return result;


            // return Request.CreateResponse(HttpStatusCode.OK, response); 
            

        }

        [HttpPost]
        public ActionResult LoginWOTPcheck(string userid, string ddlUserType, string UserOTP)
        {
            string StrTm = "0";
            JsonResult result = new JsonResult();
            if (UserOTP != "")
            {
                if (/*Convert.ToString(Session["OTPstr"])*/ UserOTP == UserOTP)
                {
                    StrTm = objLoginRP.checkOTP(UserOTP);
                    if (StrTm == "1")
                    {
                        if (userid != "" && ddlUserType != "")
                        {
                            objLogin.LoginId = userid;
                            objLogin.UserType = ddlUserType;

                            var User = objLoginRP.CheckUserByLoginWOTP(objLogin);
                            if (User != null)
                            {

                                //Session["Client_ID"] = User.ClientID;
                                //Session["PolicyNo"] = User.Policy_Number;
                                //Session["Customer_Name"] = User.Customer_Name;
                                //Session["LastLoginDate"] = User.LastLoginDate;
                                //Session["Mobile"] = User.MobilePIN; //"8960098555";// User.MobilePIN;
                                //Session["Email"] = User.Email;

                                result.Data = User;

                                // Session["OTPcnt"] = 0;

                            }

                        }


                    }
                    else
                    {
                        //ViewBag.Message = String.Format("OTP is incorrect or Expired.");
                        StrTm = "3";
                    }
                    // ViewBag.Message = "Your application description page.";

                    //return View();
                }
                else
                {
                    //ViewBag.Message = String.Format("Please enter Correct OTP.");
                    StrTm = "2";

                }
            }
            else
            {
                //ViewBag.Message = String.Format("Please enter  OTP.");
                StrTm = "1";

            }
            if (StrTm != "0")
            {
                result.Data = StrTm;
            }
            //return View("../PreLogin/Index");
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }


        [HttpPost]
        public JsonResult LoginWOTP(string userid, string ddlUserType)
        {
            try
            {
                JsonResult result = new JsonResult();

                if (userid != "" && ddlUserType != "")
                {
                    objLogin.LoginId = userid;
                    objLogin.UserType = ddlUserType;
                    IEnumerable<UserModel> cts = objLoginRP.CheckUserForForgotPWD(objLogin);
                    result.Data = cts;

                    foreach (var ct in cts)
                    {
                        objLogin.Email = ct.Email;
                        objLogin.Mobile = ct.MobilePIN;
                        objLogin.Customer_Name = ct.Customer_Name;
                        //objLogin.LoginId = ct.LoginId;
                        // Do something else with the property's value
                    }

                    if (objLogin.Email != null || objLogin.Mobile != null)
                    {
                        //  result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        //  Session["OTPcnt"] = 0;
                        System.Web.Script.Serialization.JavaScriptSerializer jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                        if (ddlUserType == "3" || ddlUserType == "4")
                        {

                            //JavaScriptSerializer serializer = new JavaScriptSerializer();
                            // var result1 = ct; //serializer.Deserialize<UserModel>(serializer.Serialize(result.Data));
                            if (Convert.ToInt32(HttpContext.Current.Session["OTPcnt"]) <= 2)
                            {
                                if (objLogin.Email != "" || objLogin.Mobile != "" && objLogin.Customer_Name != "")
                                {

                                    result.Data = SendOTP(objLogin.Email, objLogin.Mobile, objLogin.Customer_Name, "LoginWOTP");

                                    //result.Data = strOTP;
                                    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                                }
                            }

                            else
                            {
                                result.Data = 3;
                                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                            }
                        }
                    }
                    else
                    {
                        result.Data = 0;
                        result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    }

                }
                //}
                //else
                //{
                //    result.Data = 3;
                //    result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                //}
                return result;
            }
            catch (Exception ex)
            {
                HelperClass.LogError(ex);
                //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                return null;
            }
        }

        [HttpPost]
        public JsonResult SendOTP(string Email, string MobilePIN, string Customer_Name, string OTPfor)
        {
            //Session["OTPcnt"] = 0;
            JsonResult result = new JsonResult();
            if (Email != "" || MobilePIN != "" && Customer_Name != "")
            {
                objLogin.Email = Email;
                objLogin.Mobile = MobilePIN;
                objLogin.Customer_Name = Customer_Name;

                string strOTP = objLoginRP.GenarateOTP(objLogin, OTPfor);

                result.Data = strOTP;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            }
            return result;
        }
       // [HttpPost]
        public JsonResult CheckOTP(string OTP)
        {
            JsonResult result = new JsonResult();
            if (OTP != "")
            {
                string strOTP;
                strOTP = objLoginRP.checkOTP(OTP);

                result.Data = strOTP;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
            return result;

        }


    }
}
