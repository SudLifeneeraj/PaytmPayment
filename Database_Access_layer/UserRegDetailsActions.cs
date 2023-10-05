
namespace SUD.BusinessLogic
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    //using Microsoft.Practices.EnterpriseLibrary.Validation;
    //using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    //using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;
	//using SUD.BusinessEntities;
	using SUD.DataAccess;
    using System.Data;
    using WIP_Report.Models;
    using SUD.BusinessEntities;

    /// <summary>
    /// Provides business actions related to UserRegDetails object.
    /// </summary>
    public class UserRegDetailsActions : BusinessLogicBase
	{
        /// <summary>
        /// Implements the business action - InsertUserActionDetails.
        /// </summary>
        public System.String InsertUserActionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertUserActionDetails(userRegDetails);
            return result;
        }

        /// <summary>
        /// Gets FailedPasswordAttemptCount by client ID. (Added by Srinivas Racherla on Aug18'2010)
        /// </summary>      
        public UserRegDetails GetFailedPasswordAttemptCount(string ClientID)
        {
            //Process object data
            UserRegDetails result = null;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.GetFailedPasswordAttemptCount(ClientID);
            return result;
        }


        /// <summary>
        /// Implements the business action - Unlocks the ClientID.  (Added by Srinivas Racherla on Aug18'2010)
        /// </summary>
        public System.String UpdateMembershipForUnblockingClient(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.UpdateMembershipForUnblockingClient(userRegDetails);
            return result;
        }


		/// <summary>
		/// Implements the business action - InsertUserRegDetails.
		/// </summary>
		public System.String InsertUserRegDetails(UserRegDetails userRegDetails)
		{
			//Validate object data
			//ValidationResults validationResults = Validation.ValidateFromConfiguration(userRegDetails);
			//if (!validationResults.IsValid)
			//{
            //    ApplicationException validationException = new ApplicationException("Validation Failed.");
            //    foreach (ValidationResult validationResult in validationResults)
            //    {
            //        validationException.Data.Add(validationResult.Key, validationResult.Message);
            //    }
            //    throw validationException;
            //}
			//Process object data
			System.String result = string.Empty;
			UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertUserRegDetails(userRegDetails);
			return result;
		}

		/// <summary>
		/// Implements the business action - UpdateUserRegDetails.
		/// </summary>
		public void UpdateUserRegDetails(UserRegDetails userRegDetails)
		{
			//Validate object data
            //ValidationResults validationResults = Validation.ValidateFromConfiguration(userRegDetails);
            //if (!validationResults.IsValid)
            //{
            //    ApplicationException validationException = new ApplicationException("Validation Failed.");
            //    foreach (ValidationResult validationResult in validationResults)
            //    {
            //        validationException.Data.Add(validationResult.Key, validationResult.Message);
            //    }
            //    throw validationException;
            //}
			//Process object data
			UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
			userRegDetailsRepository.UpdateUserRegDetails( userRegDetails);
		}

		/// <summary>
		/// Implements the business action - DeleteUserRegDetailsBySerialNumber.
		/// </summary>
		public void DeleteUserRegDetailsBySerialNumber(System.Int32 pSerialNumber)
		{
			//Process object data
			UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
			userRegDetailsRepository.DeleteUserRegDetailsBySerialNumber( pSerialNumber);
		}

		/// <summary>
		/// Implements the business action - FindAllUserRegDetails.
		/// </summary>
		public List<UserRegDetails> FindAllUserRegDetails()
		{
			//Process object data
			List<UserRegDetails> result = null;
			UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
			result = userRegDetailsRepository.FindAllUserRegDetails();
			return result;
		}

		/// <summary>
		/// Implements the business action - FindUserRegDetailsBySerialNumber.
		/// </summary>
		public List<UserRegDetails> FindUserRegDetailsBySerialNumber(System.Int32 pSerialNumber)
		{
			//Process object data
			List<UserRegDetails> result = null;
			UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
			result = userRegDetailsRepository.FindUserRegDetailsBySerialNumber( pSerialNumber);
			return result;
		}

        /// <summary>
        /// Implements the business action - FindUserPwdByLoginID.
        /// </summary>
        public UserRegDetails FindUserPwdByLoginID(System.String clientID)
        {
            //Process object data
            UserRegDetails result = null;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.FindUserPwdByLoginID(clientID);
            return result;
        }

        /// <summary>
        /// Implements the business action - CheckUserLoginID.
        /// </summary>
        public Boolean CheckUserLoginID(System.String loginID)
        {
            //Process object data
            Boolean result = true;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.CheckUserLoginID(loginID);
            return result;
        }

        /// <summary>
        /// Generate new password for the user after validating his UserID, ClientID and DOB
        /// </summary>
        /// Developer assumes UserId == LoginId
        //public String GenNewPwd4forgotPwdAfterValidation(System.Int32 ClientID, String DoB) //UserRegDetails userRegDetails
        public String GenNewPwd4forgotPwdAfterValidation(UserRegDetails userRegDetails)
        {
            //Process object data
            String result = string.Empty;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.GenNewPwd4forgotPwdAfterValidation(userRegDetails);
            return result;
        }

        /// <summary>
        /// Get user details by client ID.
        /// </summary>
        /// Developer assumes UserId == LoginId
        //public String GenNewPwd4forgotPwdAfterValidation(System.Int32 ClientID, String DoB) //UserRegDetails userRegDetails
        public UserRegDetails GetUserByClientID(string ClientID)
        {
            //Process object data
            UserRegDetails result = null;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.GetUserByClientID(ClientID);
            return result;
        }



        /// <summary>
        /// Sets new password for the user in db after validating his ClientID and DOB
        /// </summary>
        public void SetNewPwd4forgotPwdAfterValidation(UserRegDetails userRegDetails)
        {           
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            userRegDetailsRepository.SetNewPwd4forgotPwdAfterValidation(userRegDetails);
        }

        /// <summary>
        /// Implements the business action - FindUserPwdByClientID.
        /// </summary>
        public UserRegDetails FindUserPwdByClientID(System.Int32 ClientID)
        {
            //Process object data
            UserRegDetails result = null;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.FindUserPwdByClientID(ClientID);
            return result;
        }
        public string CheckIfPasswordChanged(UserRegDetails userRegDetails)
        {
            String result = string.Empty;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.CheckIfPasswordChanged(userRegDetails);
            return result;
        }
        public UserRegDetails GetUserInformation(string clientID)
        {
            UserRegDetails result = null;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.GetUserInformation(clientID);
            return result;
        }
        public void ModifyPasswordChanged(UserRegDetails userRegDetails)
        {            
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            userRegDetailsRepository.ModifyPasswordChanged(userRegDetails);            
        }
        public void ForgotPasswordChanged(UserRegDetails userRegDetails)
        {
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            userRegDetailsRepository.ForgotPasswordChanged(userRegDetails);
        }

        /// <summary>
        /// Check if the First time the logged in user has changed the default password or not
        /// </summary>
        public string CheckIfClientExist(UserRegDetails userRegDetails)
        {
            String result = string.Empty;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.CheckIfClientExist(userRegDetails);
            return result;
        }


        //Added for CMS on 02-09-10-------------------------------------------------------------------------------------------
        //Insertion for Userfeedback for both user and Customer---------------------------------------------------------------
        public System.String InsertUserFeedback(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertUserFeedbackDetails(userRegDetails);
            return result;
        }
        //End of ------Insertion for Userfeedback for both user and Customer--------------------------------------------------
        //To get SubType value------------------------------------------------------------------------------------------------
        public DataTable GetSubType(UserRegDetails userRegDetails)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetSubTypeDetails(userRegDetails);
            return dt;
        }
        //End of -----To get SubType value------------------------------------------------------------------------------------
        //To Get CallType value-----------------------------------------------------------------------------------------------
        public DataTable GetCallType(UserRegDetails userRegDetails)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetCallTypeDetail(userRegDetails);
            return dt;
        }
        //End of -----To Get CallType value-----------------------------------------------------------------------------------
        //Added for Serch User feedback---------------------------------------------------------------------------------------
        public DataTable GetUserfeedbackSerchResultDetails(UserRegDetails userRegDetails)//(string CallID, string EmailID)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetUserfeedbackSerchResult(userRegDetails);//(CallID, EmailID);
            return dt;
        }
        //End of ------Added for Serch User feedback--------------------------------------------------------------------------
        //Added for Customer feeback search result----------------------------------------------------------------------------
        public DataTable GetCustomerfeedbackSerchResultDetails(UserRegDetails userRegDetails)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetCustomerfeedbackSerchResult(userRegDetails);
            return dt;
        }
        //End of -----Added for Customer feeback search result----------------------------------------------------------------
        //Get GetResolutionProvided-------------------------------------------------------------------------------------------
        public DataTable GetResolutionProvided(UserRegDetails userRegDetails)    //(string CallID)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetResolutionProvidedDetails(userRegDetails);    //(CallID);
            return dt;
        }
        //End of GetResolutionProvided----------------------------------------------------------------------------------------
        //End of ------Added for CMS on 02-09-10------------------------------------------------------------------------------

        //----------------------------------  New Code on 01-Mar-2011  --------------------------------//
        //----------------------------------  New Code for Day 2 Deployment    ------------------------//

        /// <summary>
        /// Implements the business action - InsertGeneralTransactionDetails.
        /// </summary>
        public System.String InsertGeneralTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertGeneralTransactionDetails(userRegDetails);
            return result;
        }

        /// <summary>
        /// Implements the business action - InsertPremiumPaymentTransactionDetails.
        /// </summary>
        public System.String InsertPremiumPaymentTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertPremiumPaymentTransactionDetails(userRegDetails);
            return result;
        }

        /// <summary>
        /// Implements the business action - InsertFundSwitchTransactionDetails.
        /// </summary>
        public System.String InsertFundSwitchTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertFundSwitchTransactionDetails(userRegDetails);
            return result;
        }

        /// <summary>
        /// Implements the business action - InsertTopUpPremiumTransactionDetails.
        /// </summary>
        public System.String InsertTopUpPremiumTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertTopUpPremiumTransactionDetails(userRegDetails);
            return result;
        }
        /// <summary>
        /// Implements the business action - InsertReinstatementTransactionDetails.
        /// </summary>
        public System.String InsertReinstatementTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertReinstatementTransactionDetails(userRegDetails);
            return result;
        }
        /// <summary>
        /// Implements the business action - InsertNomineeChangeTransactionDetails.
        /// </summary>
        public System.String InsertNomineeChangeTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertNomineeChangeTransactionDetails(userRegDetails);
            return result;
        }
        /// <summary>
        /// Implements the business action - InsertNomineeChangeTransactionDetails.
        /// </summary>
        public System.String InsertAppointeeTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertAppointeeTransactionDetails(userRegDetails);
            return result;
        }

        /// <summary>
        /// Implements the business action - InsertBillingModeChangeTransactionDetails.
        /// </summary>
        public System.String InsertBillingModeChangeTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertBillingModeChangeTransactionDetails(userRegDetails);
            return result;
        }
        /// <summary>
        /// Implements the business action - InsertPremiumRedirectionTransactionDetails.
        /// </summary>
        public System.String InsertPremiumRedirectionTransactionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertPremiumRedirectionTransactionDetails(userRegDetails);
            return result;
        }
        /// <summary>
        /// Implements the business action - UpdateUserActionDetails.
        /// </summary>
        public System.String UpdateUserActionDetails(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.UpdateUserActionDetails(userRegDetails);
            return result;
        }
        #region "This code is for MIS functionality---"
        public int GetTransactonStatusFrequency(string TransactionResult, string TransactionType, string TransactionDate)
        {
            int frequency = 0;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            frequency = userRegDetailsRepository.GetNumberOfStatus(TransactionResult, TransactionType, TransactionDate);
            return frequency;
        }
        public int GetTransactonPassStatusFrequency(string TransactionResult, string TransactionType, string TransactionDate, string UpdatedBy, string Destination)
        {
            int frequency = 0;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            frequency = userRegDetailsRepository.GetNumberOfStatusOfPassByPortal(TransactionResult, TransactionType, TransactionDate, UpdatedBy, Destination);
            return frequency;
        }
        public int TotalNoOfTransactions(string TransactionType, string TransactionDate)
        {
            int total = 0;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            total = userRegDetailsRepository.GetNumberOfTotalTransaction(TransactionType, TransactionDate);
            return total;
        }
        public DataTable GeteFailTransactionDetail(string TransactionType, string TransactionResult, string TransactionDate)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetDetailsTransaction(TransactionType, TransactionResult, TransactionDate);
            return dt;
        }
        #endregion
        public DataTable GetDetailsOfLifeAssured(string Policy_Number, string Role)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetDLifeAssureDetail(Policy_Number, Role);
            return dt;
        }
        public string GenerateTransactionID()
        {
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            string strTransactionID = string.Empty;
            int transactionId;
            //if (!string.IsNullOrEmpty(strTransactionID))
            //{
            //    transactionId = Convert.ToInt32(strTransactionID);
            //    if (transactionId < 99999999)
            //    {
            //        transactionId = Convert.ToInt32(strTransactionID) + 1;
            //    }
            //    else
            //    {
            //        strTransactionID = userRegDetailsRepository.GetMinTransactionId();
            //        if (!string.IsNullOrEmpty(strTransactionID))
            //        {
            //            transactionId = Convert.ToInt32(strTransactionID);
            //            transactionId = Convert.ToInt32(strTransactionID) - 1;
            //        }
            //    }
            //}
            //else
            //{

            int rowCount = 1;
            while (strTransactionID.Length < 8 && rowCount != 0)
            {
                transactionId = RandomIdGen();
                strTransactionID = transactionId.ToString("00000000");
                rowCount = userRegDetailsRepository.GetTransactionId(strTransactionID);

            }

            //}
            return strTransactionID;
        }

        private static int RandomIdGen()
        {
            int transactionId;
            Random rand = new Random();
            transactionId = rand.Next(00000000, 99999999);
            return transactionId;
        }

        //----------------------------------  End Of  Day 2 Deployment    -----------------------------//
        //------------- Added the below code for the Life Assured is Minor On 14th March 2011 -----//

        public DataTable CheckLifeAssuredForNomineeChange(UserRegDetails userRegDetails)//(string PolicyNumber)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.CheckLifeAssuredForNomineeChange(userRegDetails);//(PolicyNumber);
            return dt;
        }

        //------ End of 14th march 2011-------//
       

        // Added on 29th March 2001 - Nominee change is not allowed for Assignment policies //

        
        public string CheckAssigneeForNomineeChange(UserRegDetails userRegDetails)//(string PolicyNumber)
        {
            string isAssigneePresent = string.Empty;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            isAssigneePresent = userRegDetailsRepository.CheckAssigneeForNomineeChange(userRegDetails);//(PolicyNumber);
            return isAssigneePresent;
        }

        // End of 29th MArch 2011 -----//

        //---------------------------------------Start of Added on 23 -07-2012 to get Client Data for PAN card Validation
        public DataTable GetClientInfoDetails(string Client_ID)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository =new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetClientInfoDetails(Client_ID);
            return dt;
        }
        //---------------------------------------End of Added on 23 -07-2012 to get Client Data for PAN card Validation

        public DataTable GetPolicyDetails(string PolicyNumber)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetPolicyDetails(PolicyNumber);
            return dt;
        }

        public DataTable GetAnnuityDetails(string PolicyNo)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetAnnuityDetails(PolicyNo);
            return dt;
        }


        /* BRS Email - New fuction added to get the User COntact details 19 Jun 2014 Starts here*/
        /// <summary>
        /// Get Client Contact Details 
        /// </summary>
        /// <param name="Client_ID"></param>
        /// <returns></returns>
        public DataTable GetClientContactDetails(string Client_ID)
        {
            DataTable dt = new DataTable();
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            dt = userRegDetailsRepository.GetClientContactDetails(Client_ID);
            return dt;
        }
        /* BRS Email - New fuction added to get the User COntact details 28 May 2014 ends here*/

        //To insert transaction fail error message into table
        public System.String InsertTransactionFailErrorMsg(UserRegDetails userRegDetails)
        {
            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();            
            result = userRegDetailsRepository.InsertTransactionFailErrorMsg(userRegDetails);      
            return result;
        }
        // end here To insert transaction fail error message into table

        //To insert email and sms sent details into table
        public System.String InsertEmailSMS_SentDetails(UserRegDetails userRegDetails)
        {

            System.String result;
            UserRegDetailsRepository userRegDetailsRepository = new UserRegDetailsRepository();
            result = userRegDetailsRepository.InsertEmailSMS_SentDetails(userRegDetails);
            return result;
        }
        // end here To insert email and sms sent details into table
	}
}
