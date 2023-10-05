using System;
using System.Collections.Generic;
using System.Text;

namespace ValidPolicy_paytmappIntegration
{
    public static class SUDUtility
    {
        public const string UserAction_UserRegistration = "UserRegistration";
        public const string UserAction_UserLogin = "UserLogin";
        public const string UserAction_ForgotPassword = "ForgotPassword";
        public const string UserAction_ChangeAddress = "ChangeAddress";
        public const string UserAction_PremiumReceipt = "PremiumReceipt";

        public const string UserAction_Successfull = "Y";
        public const string UserAction_UnSuccessfull = "N";


        //------------------  Added for Feedback (CMS) on 02-Sep-2010  ------------------------//

        public const string BizTalk_Successful = "Successful";
        public const string BizTalk_UnSuccessful = "UnSuccessful";
        public const string BizTalk_PASS = "PASS";
        public const string Portal_Feedback = "Website";
        public const string Feedback_Status_Open = "Open";
        public const string Feedback_Saved = "  successfully saved...";
        public const string Feedback_QueryType_CG = "Complaints/Grievances";
        public const string Feedback_SeverityLevel_Medium = "Medium";
        public const string Feedback_Bank_Others = "Others";
        public const string Feedback_CallID = "  Call ID: ";
        //Added on 13-09-10------------------------------------------------
        public const string Customer_Feedback_CallerType = "Customer";
        public const string User_Feedback_CallerType = "Customer";
        public const string Customer_Feedback_CallLogger = "Website";
        public const string User_Feedback_CallLogger = "Website";
        //End of Added on 13-09-10-------------------------------------------
        //-------------------------------------------------------------------------------------//



        //------------------- New Code on 01 March 2011 ---------------------------------------//
        //-------------------New Code for Day 2 Deployment ------------------------------------//


        //-------------------------Common for all--------------------//
        public const string BizTalk_FAIL = "FAIL";
        public const string BizTalk_WIP = "WIP";

        public const string SystemError_Msg = "System Error";
        public const string ErrMsg_Initialy = "No Error";

        //public const string UpdatedBy = "Portal";
        //public const string Source = "Portal";
        //public const string Destination = "BizTalk";

        public const string ServerdownErrMsg = "Sorry!! Server not available... Please try after some time.";

        //--------------End of Common for all--------------------//

        //------------ULIPPolicy----------------------------------------//
        public const string ULIPP_FW_PIPage_MsgDisplay = "Please select the policy for Fund Switch. The details about your policies are shown as below:";
        public const string ULIPP_FW_PIPage_InterfaceName_Display = "Fund Switch";
        public const string ULIPP_FW_PIPage_NavigationLink_Display = "Fund Switch";
        public const string ULIPP_PR_PIPage_MsgDisplay = "Please select the policy for Premium Redirection. The details about your policies are shown as below:";
        public const string ULIPP_PR_PIPage_InterfaceName_Display = "Premium Redirection";
        public const string ULIPP_PR_PIPage_NavigationLink_Display = "Premium Redirection";
        public const string ULIPP_BM_PIPage_MsgDisplay = "Please select the policy for Mode Change. The details about your policies are shown as below:";
        public const string ULIPP_BM_PIPage_InterfaceName_Display = "Mode Change";
        public const string ULIPP_BM_PIPage_NavigationLink_Display = "Mode Change";
        public const string ULIPP_NC_PIPage_MsgDisplay = "Please select the policy for Nominee Change. The details about your policies are shown as below:";
        public const string ULIPP_NC_PIPage_InterfaceName_Display = "Nominee Change";
        public const string ULIPP_NC_PIPage_NavigationLink_Display = "Nominee Change";
        public const string ULIPP_FS_PR_BM_InforceULIP_Check = "This option is available for Inforce ULIP policies.";
        public const string ULIPP_FS_InforceULIP_Check = "Fund Switch option is available for Inforce ULIP Policies only.";
        public const string ULIPP_PR_InforceULIP_Check = "Premium Redirection option is available for Inforce ULIP Policies only.";
        public const string ULIPP_BM_InforceULIP_Check = "Mode change can be done for only Inforce ULIP Policies.";
        //public const string ULIPP_NC_Inforce_Check = "This option is available for Inforce policies.";
        public const string ULIPP_NC_Inforce_Check = "Nominee change can be done only for Inforce Policies.";

        //-- Added below line for Nominee Change Life Assured is Minor On 14th March 2011//

        public const string ULIPP_NC_LA_Minor = "Nominee change is not allowed when Life Assured is minor.";

        //-- End of 14th MArch 2011-----//

        //-- Added on 29th March 2011 below for " Nominee changes is not allowed for the Assignment Policies" --//


        public const string ULIPP_NC_NOTALWD_AS = "Nominee change is not allowed for the Assignment policies";


        //--- End of 29th March 2011-----//


        //-------------end of ULIPPolicy---------------------------------//

        //-----------------FundSwitch----------------------------//
        public const string FS_EmptyData = "No Records Found";
        public const string FS_Validation_NextPremiumDueDate = "Sorry!! You cannot perform Fund Switching. Please pay the Premium first to get the facility.";

        public const string FS_SwitchPercentHundred_Check = "Switch % can't be greater than 100 against any fund.";
        public const string FS_SwitchPercentPositive_Check = "Please enter positive Numeric Value in Switch %.";
        //public const string FS_SwitchPercentTenThousand_Check = "You need to switch more fund as the total of fund switching amount is less than Rs.10000/=";
        public const string FS_SwitchPercentTenThousand_Check = "Minimum amount allowed in fund switch is Rs. 10,000";
        public const string FS_FundToPercentPositive_Check = "Please enter positive Numeric Value in Fund To%.";
        public const string FS_FundToPercentHundredCheck = "Please check your total Fund To %. It should be equal to 100";
        public const string FS_FundToPercent_MinimumValueCheck = "Please invest minimum 10% in each of your desired funds";
        public const string FS_TransactionType = "FundSwitch";

        public const string FS_UpdatedBy = "Portal";
        public const string FS_Source = "Portal";
        public const string FS_Destination = "BizTalk";
        //-----------End of FundSwitch.cs----------------------------//


        //-------------PremiumRedirection--------------------------//
        public const string PR_ErrorMsg_SinglePremiumCheck = "Sorry!! Premium Redirection is not allowed for Single Premium Policy.";
        public const string PR_ErrorMsg_NextPremiumDueDate = "Sorry!! You cannot perform Premium Redirection. Please pay the Premium first to get the facility.";
        public const string PR_EmptyData = "No Records Found";
        public const string PR_NewApportionmentEmptyCheck = "Please enter values in New Apportionment% column.";
        public const string PR_NewApportionmentNumericCheck = "Please enter numeric values in New Apportionment % column.";
        public const string PR_NewApportionmentPositiveNumericCheck = "Negative value are not allowed.";
        public const string PR_NewApportionmentIndivisualValueCheck = "Please invest minimum 10% in each of your desired funds";
        public const string PR_NewApportionmentHundredCheck = "New Apportionment % against any fund should not exceed 100%.";
        public const string PR_TotalNewApportionmentHundredCheck = "Please check your total New Apportionment %. It should be equal to 100";
        public const string PR_TransactionType = "PremiumRedirection";

        public const string PR_UpdatedBy = "Portal";
        public const string PR_Source = "Portal";
        public const string PR_Destination = "BizTalk";

        //------------Enf of PremiumRedirection---------------------//
        //----------------BillModeChange-----------------------------//
        public const string BM_Role = "Life Assured";
        public const string BM_Validation_EM_SinglePremium = "Sorry!! Bill mode change is applicable only for Yearly, Half Yearly and Quarterly modes.";
        public const string BM_Validation_NextPremiumDueDate = "Sorry!! Please pay your due premium first to avail Bill Mode Change facility.";
        public const string BM_Validation_EM_HalfYearlyToYearly = "Sorry!! you are not allowed for mode change request as both the existing half-yearly premiums (2 Half yearly premiums) of the policy year are not paid yet.";
        public const string BM_Validation_EM_QuarterlyToYearly = "Sorry!! you are not allowed for mode change request as all the existing quarterly premiums (4 Quarterly premiums) of the policy year are not paid yet.";
        public const string BM_Validation_EM_QuaterlyToHalfYearly = "Sorry!! you are not allowed for mode change request as you have not yet paid two quarterly premiums of the policy year.";

        public const string BM_TransactionType = "BillingModeChange";

        public const string BM_UpdatedBy = "Portal";
        public const string BM_Source = "Portal";
        public const string BM_Destination = "BizTalk";
        public const string BM_U12_ModeChangeNotAllowed = "Sorry!! Bill mode change is not allowed for Dhan Suraksha Express";
        //-------------------End of BillModeChange---------------------//

        //-----------------NomineeChange----------------------------//
        public const string NC_InforceCheck = "Nominee change can be done on inforce policies through portal.";
        public const string NC_TransactionType = "NomineeChange";//Same use in Appointee while Transaction insertion

        public const string NC_UpdatedBy = "Portal";//Same use in Appointee while Transaction insertion
        public const string NC_Source = "Portal";//Same use in Appointee while Transaction insertion
        public const string NC_Destination = "BizTalk";//Same use in Appointee while Transaction insertion
        public const string NC_CheckPolicyValidation_PT = "Nominee change cannot be done for Prabhat Tara Product.";
        public const string NC_CheckPolicyValidation_NPT = "Nominee change cannot be done for New Prabhat Tara Product.";
        public const string NC_CheckPolicyValidation_NPT3 = "Nominee change cannot be done for Prabhat Tara 3 Product.";


        //-----------------End of NomineeChange----------------------------//

        //--------------AppointeeDetails------------------------------------//
        public const string AD_TransactionType = "AppointeeDetails";

        public const string AD_UpdatedBy = "Portal";
        public const string AD_Source = "Portal";
        public const string AD_Destination = "BizTalk";
        public const string AD_WEBSITE = "WEBSITE";
        public const string AD_CUSTPORT = "CUSTOMERPORTAL";
        //----------------End of AppointeeDetails---------------------------//

        //----------------PremiumPaymentPolicy-----------------------------//

        public const string PPP_PolicyValidationMsg = "There are no policies applicable for Premium Payment.";
        public const string PPP_InfoceCheck = "Premium Payment is applicable for Inforce policies.";
        public const string PP_Payment_PAN = "Require PAN Card No. before proceeding with payment. Please contact customer care for further assistance.";
        //--------------------end of PremiumPaymentPolicy-------------------//

        //---------------PremiumPayment-----------------------------------//
        public const string PP_Validation_EM = "Sorry!! You cannot perform Premium Payment through online. Online Payment is allowed for Yearly, Half Yearly and Quarterly mode.";
        public const string PP_PaymentType = "Premium Payment";
        //------------------End of PremiumPayment------------------------//

        //------------TopUpPremiumPolicy-------------------------------//
        public const string TUPP_InforceULIPCheck = "This option is available only for the Inforce ULIP policies.";
        public const string TUPP_DhanSurakhsha = "Top Up not allowed for Dhan Suraksha Products/U09/U10"; //"Top Up is not allowed for Dhan Surakhsha";
        //------------End of TopUpPremiumPolicy-------------------------------//

        //--------------TopUpPremium------------------------------------//
        public const string TUP_FiveThousandCheck = "Eligible Top-up Premium for your Policy is less than Minimum Top-up Premium of Rs.5000.";
        public const string TUP_NextPremiumDueDateCheck = "Sorry!! You cannot perform Top UP Premium. Please pay your Premium First.";
        public const string TUP_PaymentType = "Top Up Premium Payment";
        public const string TUP_KYCCheck = "KYC details not available. Contact SUD’s nearest branch for further details.";
        public const string TUP_AmountCheck = "Top Up amount should be less than 50,000.";
        //---------End of TopUpPremium------------------------------------//

        //-----------------ReinstatementPolicy---------------------------//
        //public const string RP_Validation_LapsCheck = "This option is not applicable to you.";
        public const string RP_Validation_LapsCheck = "Revival option is available for Lapsed Policies only."; //"Reinstatement option is available for Lapsed Policies only.";
        //-----------End of ReinstatementPolicy---------------------------//

        //--------------Reinstatement---------------------------------------//
        //public const string R_OneEightDayCheck= "Your Policy has been lapsed for more than 6 months. You cannot reinstate your policy  through online. Please contact nearest Branch Office.";
        public const string R_OneEightDayCheck = "Your Policy has been lapsed for more than 6 months. You cannot revive your policy through portal. Kindly contact customer care on 022-39546300 or Email to customercare@sudlife.in"; //"Your Policy has been lapsed for more than 6 months. You cannot reinstate your policy through portal. Kindly contact customer care on 022-39546300.";
        public const string R_NextPremiumDueDateCheck = "Your Policy has been lapsed for more then 6 months.You cannot revive your policy  through online."; //"Your Policy has been lapsed for more then 6 months.You cannot reinstate your policy  through online.";
        public const string R_PaymentType = "Reinstatement";
        public const string Rv_PaymentType = "Revival Payment";
        //---------------End of Reinstatement---------------------------//




        //------------------- End Of Day 2 Deployment -----------------------------------------//



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
            builder.Append(RandomString(4, true));
            builder.Append(RandomSpecialChar());
            builder.Append(RandomNumber(100, 999));
            return builder.ToString();
        }
        #endregion



        ///*BRS Email- Send mail and SMS fuctions - added on 19 Jun 2014 Starts herer*/
        ///// <summar y>
        ///// Send Mail to Using using Biztalk Service
        ///// </summary>
        ///// <param name="strSendTo"></param>
        ///// <param name="strFrom"></param>
        ///// <param name="strSubject"></param>
        ///// <param name="strBody"></param>
        ///// <param name="downloadBytes"></param>
        ///// <returns>Mail send status string</returns>
        //public static string SendEmail(string strSendTo, string strFrom, string strSubject, string strBody, string strFilePath)
        //{
        //    //System.IO.MemoryStream msFile = new System.IO.MemoryStream(downloadBytes);
        //    SendingMailSoapClientNew.SendingMail sendingmail = new SendingMailSoapClientNew.SendingMail();
        //    SendingMailSoapClientNew.SendMail2User_ServiceSoapClient service = new SendingMailSoapClientNew.SendMail2User_ServiceSoapClient();
        //    SendingMailSoapClientNew.SendMailInput objMailInput = new SendingMailSoapClientNew.SendMailInput();
        //    //ConfigurationManager
        //    objMailInput.From = strFrom;
        //    objMailInput.To = strSendTo;
        //    objMailInput.Subject = strSubject;
        //    objMailInput.Body = strBody;
        //    objMailInput.Path = strFilePath;

        //    sendingmail = service.Operation_1(objMailInput);
        //    // 
        //    return sendingmail.MailSend.MailStatus;
        //}


        ///// <summary>
        ///// Send SMS using Service
        ///// </summary>
        ///// <param name="strSMSBody"></param>
        ///// <param name="strRecipientNo"></param>
        ///// <param name="strSender"></param>
        ///// <returns></returns>
        //public static string SendSMS(string strSMSBody, string strRecipientNo, string strSender)
        //{

        //    WebService_SMSSoapClient objSMSClient = new WebService_SMSSoapClient();
        //    SMSInput objSMSInput = new SMSInput();
        //    objSMSInput.Message = strSMSBody;
        //    objSMSInput.Flag = "0";
        //    objSMSInput.Mobile_Number = strRecipientNo;
        //    objSMSInput.Received_From = strSender; //"Customer Care";

        //    InsertSMSInfo objSMSOutput = new InsertSMSInfo();
        //    objSMSOutput = objSMSClient.SMSRequestResponsePort(objSMSInput);
        //    InsertSMSInfoInsertStatus objSMSStatus = objSMSOutput.InsertStatus;

        //    return objSMSStatus.Status;

        //}
        ///*BRS Email- Send mail and SMS fuctions - added on 28 may 2014 Ends herer*/
    }
}