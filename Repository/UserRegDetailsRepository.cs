namespace SUD.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Data.Common;
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using SUD.BusinessEntities;
    using System.Globalization;
    using System.Data.SqlClient;
    using System.Configuration;
    using WIP_Report.Helpers;

    /// <summary>
    /// Provides data repository functions for UserRegDetails objects.
    /// </summary>
    public class UserRegDetailsRepository : DataRepositoryBase
    {
        private String databaseName;
        public UserRegDetailsRepository()
        {
            this.databaseName = "LocalSqlServer";// "SQLDataConnection";
        }
        public UserRegDetailsRepository(String databaseName)
        {
            this.databaseName = databaseName;
        }

        /// <summary>
        /// Retrieves the password of the user on the basis of LoginID
        /// </summary>
        public UserRegDetails GetUserByClientID(System.String clientID)
        {
            UserRegDetails userRegDetails = new UserRegDetails();

            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "aspnet_Membership_pGetUserDetailsByClientID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
            db.AddInParameter(dbCommand, "ClientID", DbType.String, clientID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    int userRegDetailsFirstNameIndex = dataReader.GetOrdinal("FirstName");
                    if (!dataReader.IsDBNull(userRegDetailsFirstNameIndex))
                    {
                        userRegDetails.FirstName = dataReader.GetValue(userRegDetailsFirstNameIndex).ToString();
                    }
                    int userRegDetailsEmailIndex = dataReader.GetOrdinal("Email");
                    if (!dataReader.IsDBNull(userRegDetailsEmailIndex))
                    {
                        userRegDetails.MailID = dataReader.GetValue(userRegDetailsEmailIndex).ToString();
                    }
                    int userRegDetailsMobilePINIndex = dataReader.GetOrdinal("MobilePIN");
                    if (!dataReader.IsDBNull(userRegDetailsMobilePINIndex))
                    {
                        userRegDetails.MobileNumber1 = dataReader.GetValue(userRegDetailsMobilePINIndex).ToString();
                    }
                    int userRegDetailsDOBIndex = dataReader.GetOrdinal("DOB");
                    if (!dataReader.IsDBNull(userRegDetailsDOBIndex))
                    {
                        userRegDetails.DateOfBirth = dataReader.GetValue(userRegDetailsDOBIndex).ToString();
                    }
                }
            }
            return userRegDetails;
        }


        /// <summary>
        /// Persists the User Action Details object.
        /// </summary>
        public System.String InsertUserActionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase(databaseName);
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        //Executing stored procedure - p_InsertUserRegDetails
                        sqlCommand = "aspnet_pUserActionDetails";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);



                        //This values should go as blank in the database

                        db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "UserName", DbType.String, userRegDetails.FirstName);
                        db.AddInParameter(dbCommand, "ActionType", DbType.String, userRegDetails.ActionType);
                        db.AddInParameter(dbCommand, "ActionResult", DbType.String, userRegDetails.ActionResult);

                        if (userRegDetails.ActionStartDateTime == DateTime.MinValue)
                        {
                            db.AddInParameter(dbCommand, "ActionStartDateTime", DbType.DateTime, DateTime.Now);
                        }
                        else
                        {
                            db.AddInParameter(dbCommand, "ActionStartDateTime", DbType.DateTime, userRegDetails.ActionStartDateTime);
                        }

                        if (userRegDetails.ActionEndDateTime == DateTime.MinValue)
                        {
                            db.AddInParameter(dbCommand, "ActionEndDateTime", DbType.DateTime, null);
                        }
                        else
                        {
                            db.AddInParameter(dbCommand, "ActionEndDateTime", DbType.DateTime, userRegDetails.ActionEndDateTime);
                        }

                        db.AddInParameter(dbCommand, "ActionSessionID", DbType.String, userRegDetails.ActionSessionID);

                        db.AddOutParameter(dbCommand, "ActionID", DbType.String, 50);
                        //--------------------------------------------------------------------------------


                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);

                        if (db.GetParameterValue(dbCommand, "@ActionID") != null)
                        {
                            userRegDetails.ActionID = (System.String)db.GetParameterValue(dbCommand, "@ActionID");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }

            return (System.String)userRegDetails.ActionID;
        }


        /// <summary>
        /// Gets FailedPasswordAttemptCount by client ID.   (Added by Srinivas Racherla on Aug18'2010)
        /// </summary> 
        public UserRegDetails GetFailedPasswordAttemptCount(System.String clientID)
        {
            UserRegDetails userRegDetails = new UserRegDetails();

            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "aspnet_Membership_GetFailedPasswordAttemptCount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ClientID", DbType.String, clientID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    int userRegDetailsFailedPasswordAttemptCountIndex = dataReader.GetOrdinal("FailedPasswordAttemptCount");
                    if (!dataReader.IsDBNull(userRegDetailsFailedPasswordAttemptCountIndex))
                    {
                        userRegDetails.FailedPasswordAttemptCount = Convert.ToInt32(dataReader.GetValue(userRegDetailsFailedPasswordAttemptCountIndex).ToString());
                    }

                }
            }
            return userRegDetails;
        }



        /// <summary>
        /// Implements the business action - Unlocks the ClientID.  (Added by Srinivas Racherla on Aug18'2010)
        /// </summary>
        public System.String UpdateMembershipForUnblockingClient(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                Database db = DatabaseFactory.CreateDatabase(databaseName);
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "aspnet_Membership_UnlockClientID";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);

                        //-----------------------------------------------------------


                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand);


                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.ActionID;
        }



        public String CheckIfPasswordChanged(UserRegDetails userRegDetails)
        {
            String PasswordChanged = string.Empty;
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {

                    //Retrieve database object
                    //Now fetch the new Password and send it to the user
                    //Database db = DatabaseFactory.CreateDatabase(databaseName);
                    String sqlCommand = "aspnet_Membership_pCheckIfPasswordChanged";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                    db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
                    db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
                    db.AddOutParameter(dbCommand, "PasswordChanged", DbType.String, 2);
                    db.ExecuteNonQuery(dbCommand);
                    
                    if (db.GetParameterValue(dbCommand, "@PasswordChanged") != null)
                    {
                        PasswordChanged = (System.String)db.GetParameterValue(dbCommand, "@PasswordChanged");
                    }
                    transaction.Commit();
                    connection.Close();
                }
                catch
                {
                    // Roll back the transaction.
                    transaction.Rollback();
                    connection.Close();
                    throw;
                }
            }
            return PasswordChanged;
        }
        public UserRegDetails GetUserInformation(string clientID)
        {
            UserRegDetails userRegDetails = new UserRegDetails();

            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "aspnet_Membership_pGetUserInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
            db.AddInParameter(dbCommand, "ClientID", DbType.String, clientID);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    int userRegDetailsEmailIndex = dataReader.GetOrdinal("Email");
                    if (!dataReader.IsDBNull(userRegDetailsEmailIndex))
                    {
                        userRegDetails.MailID = dataReader.GetValue(userRegDetailsEmailIndex).ToString();
                    }
                    int userRegDetailsLandNoIndex = dataReader.GetOrdinal("LandPhoneNumber");
                    if (!dataReader.IsDBNull(userRegDetailsLandNoIndex))
                    {
                        userRegDetails.LandLine = dataReader.GetValue(userRegDetailsLandNoIndex).ToString();
                    }
                    int userRegDetailsMobileIndex = dataReader.GetOrdinal("MobilePIN");
                    if (!dataReader.IsDBNull(userRegDetailsMobileIndex))
                    {
                        userRegDetails.MobileNumber1 = dataReader.GetValue(userRegDetailsMobileIndex).ToString();
                    }
                }
            }
            return userRegDetails;
        }

        public void ModifyPasswordChanged(UserRegDetails userRegDetails)
        {

            //Retrieve database object
            //Now fetch the new Password and send it to the user
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "aspnet_Membership_pModifyPasswordChanged";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
            db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
            db.ExecuteNonQuery(dbCommand);
        }

        public void ForgotPasswordChanged(UserRegDetails userRegDetails)
        {

            //Retrieve database object
            //Now fetch the new Password and send it to the user
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "aspnet_Membership_pForgotPasswordChanged";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
            db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Persists the UserRegDetails object.
        /// </summary>
        public System.String InsertUserRegDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase(databaseName);
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        //Executing stored procedure - p_InsertUserRegDetails
                        sqlCommand = "aspnet_Membership_CreateUser";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);



                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
                        db.AddInParameter(dbCommand, "UserName", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "Password", DbType.String, userRegDetails.NPassword);
                        db.AddInParameter(dbCommand, "PasswordSalt", DbType.String, userRegDetails.PasswordSalt);
                        db.AddInParameter(dbCommand, "MobilePIN", DbType.String, userRegDetails.MobileNumber1);
                        db.AddInParameter(dbCommand, "Email", DbType.String, userRegDetails.MailID);
                        db.AddInParameter(dbCommand, "PasswordQuestion", DbType.String, userRegDetails.PasswordQuestion);
                        db.AddInParameter(dbCommand, "PasswordAnswer", DbType.String, userRegDetails.PasswordAnswer);
                        db.AddInParameter(dbCommand, "IsApproved", DbType.Boolean, 1);

                        if (userRegDetails.CurrentTimeUtc == DateTime.MinValue)
                        {
                            db.AddInParameter(dbCommand, "CurrentTimeUtc", DbType.DateTime, DateTime.Now);
                        }
                        else
                        {
                            db.AddInParameter(dbCommand, "CurrentTimeUtc", DbType.DateTime, userRegDetails.CurrentTimeUtc);
                        }

                        if (userRegDetails.CreateDate == DateTime.MinValue)
                        {
                            db.AddInParameter(dbCommand, "CreateDate", DbType.DateTime, DateTime.Now);
                        }
                        else
                        {
                            db.AddInParameter(dbCommand, "CreateDate", DbType.DateTime, userRegDetails.CreateDate);
                        }

                        db.AddInParameter(dbCommand, "UniqueEmail", DbType.Int32, 0);
                        db.AddInParameter(dbCommand, "PasswordFormat", DbType.Int32, 1);
                        db.AddInParameter(dbCommand, "FirstName", DbType.String, userRegDetails.FirstName);
                        db.AddInParameter(dbCommand, "MiddleName", DbType.String, userRegDetails.MiddleName);
                        db.AddInParameter(dbCommand, "LastName", DbType.String, userRegDetails.LastName);
                        db.AddInParameter(dbCommand, "DOB", DbType.DateTime, DateTime.Parse(userRegDetails.DateOfBirth, CultureInfo.CreateSpecificCulture("en-GB")));
                        db.AddInParameter(dbCommand, "AddressLine1", DbType.String, "");
                        db.AddInParameter(dbCommand, "AddressLine2", DbType.String, "");
                        db.AddInParameter(dbCommand, "City", DbType.String, "");
                        db.AddInParameter(dbCommand, "State", DbType.String, "");
                        db.AddInParameter(dbCommand, "Country", DbType.String, "");
                        db.AddInParameter(dbCommand, "LandPhoneNumber", DbType.String, userRegDetails.LandLine);
                        db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "PolicyNumber", DbType.String, userRegDetails.PolicyNumber);
                        db.AddInParameter(dbCommand, "IsActive", DbType.Boolean, 1);
                        db.AddInParameter(dbCommand, "PasswordChanged", DbType.String, "N");

                        //--------------------------
                        //SqlParameter param = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
                        //param.Direction = ParameterDirection.Output;
                        //comm.Parameters.Add(param);
                        //---------------

                        Guid guid = Guid.NewGuid();
                        db.AddParameter(dbCommand, "UserId", DbType.Guid, ParameterDirection.Output, "UserId", DataRowVersion.Default, null);


                        //--------------------------------------------------------------------------------


                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "ClientID") != null)
                        {
                            userRegDetails.ClientID = (System.String)db.GetParameterValue(dbCommand, "ClientID");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }

            return (System.String)userRegDetails.ClientID;
        }
        /// <summary>
        /// Persists the UserRegDetails object.
        /// </summary>
        public void UpdateUserRegDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase(databaseName);
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        //Executing stored procedure - p_UpdateUserRegDetails

                        sqlCommand = "aspnet_Membership_UpdateUser";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
                        db.AddInParameter(dbCommand, "UserName", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "Email", DbType.String, userRegDetails.MailID);
                        db.AddInParameter(dbCommand, "Comment", DbType.String, "");
                        db.AddInParameter(dbCommand, "IsApproved", DbType.Boolean, 1);
                        db.AddInParameter(dbCommand, "LastLoginDate", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(dbCommand, "LastActivityDate", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(dbCommand, "UniqueEmail", DbType.Int32, 0);
                        db.AddInParameter(dbCommand, "MobilePIN", DbType.String, userRegDetails.MobileNumber1);
                        db.AddInParameter(dbCommand, "LandPhoneNumber", DbType.String, userRegDetails.LandLine);

                        if (userRegDetails.CurrentTimeUtc == DateTime.MinValue)
                        {
                            db.AddInParameter(dbCommand, "CurrentTimeUtc", DbType.DateTime, DateTime.Now);
                        }
                        else
                        {
                            db.AddInParameter(dbCommand, "CurrentTimeUtc", DbType.DateTime, userRegDetails.CurrentTimeUtc);
                        }

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();



                        /*------------------ Commented on 24-11-09 ---------------------------------
						sqlCommand = "p_UpdateUserRegDetails";
						dbCommand = db.GetStoredProcCommand(sqlCommand);
						db.AddInParameter(dbCommand, "pClientID", DbType.Int32, userRegDetails.ClientID);
						if(userRegDetails.CreateDate == DateTime.MinValue)
						{
							db.AddInParameter(dbCommand, "pCreateDate", DbType.DateTime, DBNull.Value);
						}
						else
						{
							db.AddInParameter(dbCommand, "pCreateDate", DbType.DateTime, userRegDetails.CreateDate);
						}
						if(userRegDetails.CurrentTimeUtc == DateTime.MinValue)
						{
							db.AddInParameter(dbCommand, "pCurrentTimeUtc", DbType.DateTime, DBNull.Value);
						}
						else
						{
							db.AddInParameter(dbCommand, "pCurrentTimeUtc", DbType.DateTime, userRegDetails.CurrentTimeUtc);
						}
                        db.AddInParameter(dbCommand, "pDateOfBirth", DbType.String, userRegDetails.DateOfBirth);					
						db.AddInParameter(dbCommand, "pFirstName", DbType.String, userRegDetails.FirstName);
						db.AddInParameter(dbCommand, "pLandLine", DbType.Int32, userRegDetails.LandLine);
						db.AddInParameter(dbCommand, "pLastName", DbType.String, userRegDetails.LastName);
						db.AddInParameter(dbCommand, "pLoginID", DbType.String, userRegDetails.LoginID);
						db.AddInParameter(dbCommand, "pMailID", DbType.String, userRegDetails.MailID);
						db.AddInParameter(dbCommand, "pMiddleName", DbType.String, userRegDetails.MiddleName);
						db.AddInParameter(dbCommand, "pMobileNumber1", DbType.Int32, userRegDetails.MobileNumber1);
						db.AddInParameter(dbCommand, "pMobileNumber2", DbType.Int32, userRegDetails.MobileNumber2);
						db.AddInParameter(dbCommand, "pPassword", DbType.Object, userRegDetails.Password);
						db.AddInParameter(dbCommand, "pPasswordAnswer", DbType.String, userRegDetails.PasswordAnswer);
						db.AddInParameter(dbCommand, "pPasswordQuestion", DbType.String, userRegDetails.PasswordQuestion);
						db.AddInParameter(dbCommand, "pPolicyNumber", DbType.String, userRegDetails.PolicyNumber);
						db.AddInParameter(dbCommand, "pSerialNumber", DbType.Int32, userRegDetails.SerialNumber);
						db.ExecuteNonQuery(dbCommand, transaction);
						// Commit the transaction.
						transaction.Commit();
						connection.Close();

                        ------------------------------ End Comment -----------------------------------*/
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }

        }
        /// <summary>
        /// Removes the UserRegDetails object.
        /// </summary>
        public void DeleteUserRegDetailsBySerialNumber(System.Int32 serialNumber)
        {
            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = String.Empty;
            DbCommand dbCommand = null;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    //Deleting existing records for UserRegDetails
                    sqlCommand = "p_DeleteUserRegDetailsBySerialNumber";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);
                    db.AddInParameter(dbCommand, "pSerialNumber", DbType.Int32, serialNumber);
                    db.ExecuteNonQuery(dbCommand);
                    // Commit the transaction.
                    transaction.Commit();
                    connection.Close();
                }
                catch
                {
                    // Roll back the transaction.
                    transaction.Rollback();
                    connection.Close();
                    throw;
                }
            }

        }
        /// <summary>
        /// Retrieves the UserRegDetails object.
        /// </summary>
        public List<UserRegDetails> FindAllUserRegDetails()
        {
            List<UserRegDetails> userRegDetailsList = new List<UserRegDetails>();

            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "p_FindAllUserRegDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    UserRegDetails userRegDetails = new UserRegDetails();
                    int userRegDetailsClientIDIndex = dataReader.GetOrdinal("ClientID");
                    if (!dataReader.IsDBNull(userRegDetailsClientIDIndex))
                    {
                        userRegDetails.ClientID = (System.String)dataReader.GetValue(userRegDetailsClientIDIndex);
                    }
                    int userRegDetailsCreateDateIndex = dataReader.GetOrdinal("CreateDate");
                    if (!dataReader.IsDBNull(userRegDetailsCreateDateIndex))
                    {
                        userRegDetails.CreateDate = (System.DateTime)dataReader.GetValue(userRegDetailsCreateDateIndex);
                    }
                    int userRegDetailsCurrentTimeUtcIndex = dataReader.GetOrdinal("CurrentTimeUtc");
                    if (!dataReader.IsDBNull(userRegDetailsCurrentTimeUtcIndex))
                    {
                        userRegDetails.CurrentTimeUtc = (System.DateTime)dataReader.GetValue(userRegDetailsCurrentTimeUtcIndex);
                    }
                    int userRegDetailsFirstNameIndex = dataReader.GetOrdinal("FirstName");
                    if (!dataReader.IsDBNull(userRegDetailsFirstNameIndex))
                    {
                        userRegDetails.FirstName = (System.String)dataReader.GetValue(userRegDetailsFirstNameIndex);
                    }

                    // DoB field type modified : from DateTime To String *****************************
                    int userRegDetailsDateOfBirthIndex = dataReader.GetOrdinal("DateOfBirth");
                    if (!dataReader.IsDBNull(userRegDetailsDateOfBirthIndex))
                    {
                        userRegDetails.DateOfBirth = (System.String)dataReader.GetValue(userRegDetailsDateOfBirthIndex);
                    }
                    // Dob field modification ended ***************************************************

                    int userRegDetailsLandLineIndex = dataReader.GetOrdinal("LandLine");
                    if (!dataReader.IsDBNull(userRegDetailsLandLineIndex))
                    {
                        userRegDetails.LandLine = (System.String)dataReader.GetValue(userRegDetailsLandLineIndex);
                    }
                    int userRegDetailsLastNameIndex = dataReader.GetOrdinal("LastName");
                    if (!dataReader.IsDBNull(userRegDetailsLastNameIndex))
                    {
                        userRegDetails.LastName = (System.String)dataReader.GetValue(userRegDetailsLastNameIndex);
                    }
                    int userRegDetailsLoginIDIndex = dataReader.GetOrdinal("LoginID");
                    if (!dataReader.IsDBNull(userRegDetailsLoginIDIndex))
                    {
                        userRegDetails.LoginID = (System.String)dataReader.GetValue(userRegDetailsLoginIDIndex);
                    }
                    int userRegDetailsMailIDIndex = dataReader.GetOrdinal("MailID");
                    if (!dataReader.IsDBNull(userRegDetailsMailIDIndex))
                    {
                        userRegDetails.MailID = (System.String)dataReader.GetValue(userRegDetailsMailIDIndex);
                    }
                    int userRegDetailsMiddleNameIndex = dataReader.GetOrdinal("MiddleName");
                    if (!dataReader.IsDBNull(userRegDetailsMiddleNameIndex))
                    {
                        userRegDetails.MiddleName = (System.String)dataReader.GetValue(userRegDetailsMiddleNameIndex);
                    }
                    int userRegDetailsMobileNumber1Index = dataReader.GetOrdinal("MobileNumber1");
                    if (!dataReader.IsDBNull(userRegDetailsMobileNumber1Index))
                    {
                        userRegDetails.MobileNumber1 = (System.String)dataReader.GetValue(userRegDetailsMobileNumber1Index);
                    }
                    int userRegDetailsMobileNumber2Index = dataReader.GetOrdinal("MobileNumber2");
                    if (!dataReader.IsDBNull(userRegDetailsMobileNumber2Index))
                    {
                        userRegDetails.MobileNumber2 = (System.String)dataReader.GetValue(userRegDetailsMobileNumber2Index);
                    }
                    int userRegDetailsPasswordIndex = dataReader.GetOrdinal("Password");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordIndex))
                    {
                        userRegDetails.Password = (System.Byte[])dataReader.GetValue(userRegDetailsPasswordIndex);
                    }
                    int userRegDetailsPasswordAnswerIndex = dataReader.GetOrdinal("PasswordAnswer");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordAnswerIndex))
                    {
                        userRegDetails.PasswordAnswer = (System.String)dataReader.GetValue(userRegDetailsPasswordAnswerIndex);
                    }
                    int userRegDetailsPasswordQuestionIndex = dataReader.GetOrdinal("PasswordQuestion");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordQuestionIndex))
                    {
                        userRegDetails.PasswordQuestion = (System.String)dataReader.GetValue(userRegDetailsPasswordQuestionIndex);
                    }
                    int userRegDetailsPolicyNumberIndex = dataReader.GetOrdinal("PolicyNumber");
                    if (!dataReader.IsDBNull(userRegDetailsPolicyNumberIndex))
                    {
                        userRegDetails.PolicyNumber = (System.String)dataReader.GetValue(userRegDetailsPolicyNumberIndex);
                    }
                    int userRegDetailsSerialNumberIndex = dataReader.GetOrdinal("SerialNumber");
                    if (!dataReader.IsDBNull(userRegDetailsSerialNumberIndex))
                    {
                        userRegDetails.SerialNumber = (System.Int32)dataReader.GetValue(userRegDetailsSerialNumberIndex);
                    }
                    userRegDetailsList.Add(userRegDetails);
                }

            }
            return userRegDetailsList;
        }
        /// <summary>
        /// Retrieves the UserRegDetails object.
        /// </summary>
        public List<UserRegDetails> FindUserRegDetailsBySerialNumber(System.Int32 serialNumber)
        {
            List<UserRegDetails> userRegDetailsList = new List<UserRegDetails>();

            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "p_FindUserRegDetailsBySerialNumber";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "pSerialNumber", DbType.Int32, serialNumber);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    UserRegDetails userRegDetails = new UserRegDetails();
                    int userRegDetailsClientIDIndex = dataReader.GetOrdinal("ClientID");
                    if (!dataReader.IsDBNull(userRegDetailsClientIDIndex))
                    {
                        userRegDetails.ClientID = (System.String)dataReader.GetValue(userRegDetailsClientIDIndex);
                    }
                    int userRegDetailsCreateDateIndex = dataReader.GetOrdinal("CreateDate");
                    if (!dataReader.IsDBNull(userRegDetailsCreateDateIndex))
                    {
                        userRegDetails.CreateDate = (System.DateTime)dataReader.GetValue(userRegDetailsCreateDateIndex);
                    }
                    int userRegDetailsCurrentTimeUtcIndex = dataReader.GetOrdinal("CurrentTimeUtc");
                    if (!dataReader.IsDBNull(userRegDetailsCurrentTimeUtcIndex))
                    {
                        userRegDetails.CurrentTimeUtc = (System.DateTime)dataReader.GetValue(userRegDetailsCurrentTimeUtcIndex);
                    }
                    int userRegDetailsDateOfBirthIndex = dataReader.GetOrdinal("DateOfBirth");
                    if (!dataReader.IsDBNull(userRegDetailsDateOfBirthIndex))
                    {
                        userRegDetails.DateOfBirth = (System.String)dataReader.GetValue(userRegDetailsDateOfBirthIndex);
                    }
                    int userRegDetailsFirstNameIndex = dataReader.GetOrdinal("FirstName");
                    if (!dataReader.IsDBNull(userRegDetailsFirstNameIndex))
                    {
                        userRegDetails.FirstName = (System.String)dataReader.GetValue(userRegDetailsFirstNameIndex);
                    }
                    int userRegDetailsLandLineIndex = dataReader.GetOrdinal("LandLine");
                    if (!dataReader.IsDBNull(userRegDetailsLandLineIndex))
                    {
                        userRegDetails.LandLine = (System.String)dataReader.GetValue(userRegDetailsLandLineIndex);
                    }
                    int userRegDetailsLastNameIndex = dataReader.GetOrdinal("LastName");
                    if (!dataReader.IsDBNull(userRegDetailsLastNameIndex))
                    {
                        userRegDetails.LastName = (System.String)dataReader.GetValue(userRegDetailsLastNameIndex);
                    }
                    int userRegDetailsLoginIDIndex = dataReader.GetOrdinal("LoginID");
                    if (!dataReader.IsDBNull(userRegDetailsLoginIDIndex))
                    {
                        userRegDetails.LoginID = (System.String)dataReader.GetValue(userRegDetailsLoginIDIndex);
                    }
                    int userRegDetailsMailIDIndex = dataReader.GetOrdinal("MailID");
                    if (!dataReader.IsDBNull(userRegDetailsMailIDIndex))
                    {
                        userRegDetails.MailID = (System.String)dataReader.GetValue(userRegDetailsMailIDIndex);
                    }
                    int userRegDetailsMiddleNameIndex = dataReader.GetOrdinal("MiddleName");
                    if (!dataReader.IsDBNull(userRegDetailsMiddleNameIndex))
                    {
                        userRegDetails.MiddleName = (System.String)dataReader.GetValue(userRegDetailsMiddleNameIndex);
                    }
                    int userRegDetailsMobileNumber1Index = dataReader.GetOrdinal("MobileNumber1");
                    if (!dataReader.IsDBNull(userRegDetailsMobileNumber1Index))
                    {
                        userRegDetails.MobileNumber1 = (System.String)dataReader.GetValue(userRegDetailsMobileNumber1Index);
                    }
                    int userRegDetailsMobileNumber2Index = dataReader.GetOrdinal("MobileNumber2");
                    if (!dataReader.IsDBNull(userRegDetailsMobileNumber2Index))
                    {
                        userRegDetails.MobileNumber2 = (System.String)dataReader.GetValue(userRegDetailsMobileNumber2Index);
                    }
                    int userRegDetailsPasswordIndex = dataReader.GetOrdinal("Password");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordIndex))
                    {
                        userRegDetails.Password = (System.Byte[])dataReader.GetValue(userRegDetailsPasswordIndex);
                    }
                    int userRegDetailsPasswordAnswerIndex = dataReader.GetOrdinal("PasswordAnswer");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordAnswerIndex))
                    {
                        userRegDetails.PasswordAnswer = (System.String)dataReader.GetValue(userRegDetailsPasswordAnswerIndex);
                    }
                    int userRegDetailsPasswordQuestionIndex = dataReader.GetOrdinal("PasswordQuestion");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordQuestionIndex))
                    {
                        userRegDetails.PasswordQuestion = (System.String)dataReader.GetValue(userRegDetailsPasswordQuestionIndex);
                    }
                    int userRegDetailsPolicyNumberIndex = dataReader.GetOrdinal("PolicyNumber");
                    if (!dataReader.IsDBNull(userRegDetailsPolicyNumberIndex))
                    {
                        userRegDetails.PolicyNumber = (System.String)dataReader.GetValue(userRegDetailsPolicyNumberIndex);
                    }
                    int userRegDetailsSerialNumberIndex = dataReader.GetOrdinal("SerialNumber");
                    if (!dataReader.IsDBNull(userRegDetailsSerialNumberIndex))
                    {
                        userRegDetails.SerialNumber = (System.Int32)dataReader.GetValue(userRegDetailsSerialNumberIndex);
                    }
                    userRegDetailsList.Add(userRegDetails);
                }

            }
            return userRegDetailsList;
        }
        /// <summary>
        /// Retrieves the password of the user on the basis of LoginID
        /// </summary>
        public UserRegDetails FindUserPwdByLoginID(System.String clientID)
        {
            UserRegDetails userRegDetails = new UserRegDetails();

            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "p_FindUserPwdByLoginName";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "pClientID", DbType.String, clientID);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    int userRegDetailsPasswordIndex = dataReader.GetOrdinal("Password");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordIndex))
                    {
                        userRegDetails.Password = (System.Byte[])dataReader.GetValue(userRegDetailsPasswordIndex);
                    }
                    int userRegDetailsFirstNameIndex = dataReader.GetOrdinal("FirstName");
                    if (!dataReader.IsDBNull(userRegDetailsFirstNameIndex))
                    {
                        userRegDetails.FirstName = dataReader.GetValue(userRegDetailsFirstNameIndex).ToString();
                    }
                    int userRegDetailsLastNameIndex = dataReader.GetOrdinal("LastName");
                    if (!dataReader.IsDBNull(userRegDetailsLastNameIndex))
                    {
                        userRegDetails.LastName = dataReader.GetValue(userRegDetailsLastNameIndex).ToString();
                    }
                }
            }
            return userRegDetails;
        }
        /// <summary>
        /// Checks the availability of the users loginID
        /// </summary>
        public Boolean CheckUserLoginID(System.String loginID)
        {
            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "p_CheckUserLoginID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "pLoginID", DbType.String, loginID);
            db.AddOutParameter(dbCommand, "pDuplicateLoginIDFound", DbType.Boolean, 100);
            db.ExecuteNonQuery(dbCommand);
            Boolean LoginIDExsist = true;

            if (db.GetParameterValue(dbCommand, "@pDuplicateLoginIDFound") != null)
            {
                LoginIDExsist = (System.Boolean)db.GetParameterValue(dbCommand, "pDuplicateLoginIDFound");
            }

            return LoginIDExsist;
        }
        /// <summary>
        /// Generates new password for the user after validating his Client ID and DoB
        /// </summary>
        //public String GenNewPwd4forgotPwdAfterValidation(System.Int32 ClientID, String DoB) //
        public String GenNewPwd4forgotPwdAfterValidation(UserRegDetails userRegDetails)
        {
            //Retrieve database object

            //Now fetch the new Password and send it to the user
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "aspnet_Membership_pGetEmailByName";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
            db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
            db.AddOutParameter(dbCommand, "Email", DbType.String, 256);
            db.AddOutParameter(dbCommand, "Password", DbType.String, 128);

            db.ExecuteNonQuery(dbCommand);
            String userMailID = string.Empty;
            String userPassword = string.Empty;

            if (db.GetParameterValue(dbCommand, "@Email") != null)
            {
                userMailID = (System.String)db.GetParameterValue(dbCommand, "@Email");
                userPassword = (System.String)db.GetParameterValue(dbCommand, "@Password");
            }

            return userMailID;


            /*--Commented on 24-11-09--
            String sqlCommand = "p_GenNewPwd4forgotPwdAfterValidation";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "pClientID", DbType.Int32, ClientID);
            db.AddInParameter(dbCommand, "pDoB", DbType.String, DoB);
            db.AddOutParameter(dbCommand, "pUserEMail", DbType.String, 100);
            db.ExecuteNonQuery(dbCommand);
            String userMailID = string.Empty;

            if (db.GetParameterValue(dbCommand, "@pUserEMail") != null)
            {
                userMailID = (System.String)db.GetParameterValue(dbCommand, "pUserEMail");
            }
            --End Commented on 24-11-09--*/

        }


       


        /// <summary>
        /// Sets newly generated password in db for the user after validating his Client ID and DoB
        /// </summary>
        public void SetNewPwd4forgotPwdAfterValidation(UserRegDetails userRegDetails)
        {
            //Retrieve database object
            //Database db = DatabaseFactory.CreateDatabase(databaseName);
            //String sqlCommand = "p_SetNewPwd4forgotPwdAfterValidation";
            //DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(dbCommand, "pClientID", DbType.Int32, ClientID);
            //db.AddInParameter(dbCommand, "pPassword", DbType.Binary, newPassword);
            //db.ExecuteNonQuery(dbCommand);            



            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String userMailID = string.Empty;

            //First Reset the Password
            String sqlCommand = "aspnet_Membership_SetPassword";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
            db.AddInParameter(dbCommand, "UserName", DbType.String, userRegDetails.ClientID);
            db.AddInParameter(dbCommand, "NewPassword", DbType.String, userRegDetails.NPassword);
            db.AddInParameter(dbCommand, "PasswordSalt", DbType.String, userRegDetails.PasswordSalt);

            if (userRegDetails.CurrentTimeUtc == DateTime.MinValue)
            {
                db.AddInParameter(dbCommand, "CurrentTimeUtc", DbType.DateTime, DateTime.Now);
            }
            else
            {
                db.AddInParameter(dbCommand, "CurrentTimeUtc", DbType.DateTime, userRegDetails.CurrentTimeUtc);
            }

            db.AddInParameter(dbCommand, "PasswordFormat", DbType.Int32, 1);

            int DBErrorcode = -555;

            DBErrorcode = db.ExecuteNonQuery(dbCommand);

        }
        /// <summary>
        /// Retrieves the password of the user on the basis of ClientID
        /// </summary>
        public UserRegDetails FindUserPwdByClientID(System.Int32 ClientID)
        {
            UserRegDetails userRegDetails = new UserRegDetails();

            //Retrieve database object
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "p_FindUserPwdByClientID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "pClientID", DbType.Int32, ClientID);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    int userRegDetailsPasswordIndex = dataReader.GetOrdinal("Password");
                    if (!dataReader.IsDBNull(userRegDetailsPasswordIndex))
                    {
                        userRegDetails.Password = (System.Byte[])dataReader.GetValue(userRegDetailsPasswordIndex);
                    }
                }
            }
            return userRegDetails;
        }

        /// <summary>
        /// Check if the clientid and dateofbirth is OK
        /// </summary>
        public string CheckIfClientExist(UserRegDetails userRegDetails)
        {
            Database db = DatabaseFactory.CreateDatabase(databaseName);
            String sqlCommand = "aspnet_Membership_pCheckIfClientExist";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "ApplicationName", DbType.String, "/");
            db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);

            //DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
            //dateTimeFormatInfo.ShortDatePattern = "dd/MM/yyyy";
            //db.AddInParameter(dbCommand, "DOB", DbType.DateTime, Convert.ToDateTime(userRegDetails.DateOfBirth));

            db.AddInParameter(dbCommand, "DOB", DbType.DateTime, DateTime.Parse(userRegDetails.DateOfBirth, CultureInfo.CreateSpecificCulture("en-GB")));
            db.AddOutParameter(dbCommand, "RecordExist", DbType.String, 2);

            db.ExecuteNonQuery(dbCommand);
            String RecordExist = string.Empty;

            if (db.GetParameterValue(dbCommand, "@RecordExist") != null)
            {
                RecordExist = (System.String)db.GetParameterValue(dbCommand, "@RecordExist");
            }

            return RecordExist;
        }


        //Added for CMS on 02-09-10-------------------------------------------------------------------------------------
        //Insertion for Userfeedback for both user and Customer---------------------------------------------------------
        public System.String InsertUserFeedbackDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "sp_Insert_CallersDetails";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);
                        //---------------------------------------------------------------------------------------                        
                        db.AddOutParameter(dbCommand, "CallID", DbType.Int32, 18);  //(dbCommand, "CallID", DbType.Int32, userRegDetails.CallID);
                        db.AddInParameter(dbCommand, "CustomerName", DbType.String, userRegDetails.Customer_Name);
                        db.AddInParameter(dbCommand, "CallerType", DbType.String, userRegDetails.Caller_Type);
                        db.AddInParameter(dbCommand, "CustomerEmail", DbType.String, userRegDetails.Customer_Email);
                        db.AddInParameter(dbCommand, "CustomerMobNo", DbType.String, userRegDetails.Customer_MobNo);
                        db.AddInParameter(dbCommand, "ApplicationNumber", DbType.String, userRegDetails.ApplicationNumber);
                        db.AddInParameter(dbCommand, "CustomerPolicyNo", DbType.String, userRegDetails.Customer_Policy_No);
                        db.AddInParameter(dbCommand, "CustomerID", DbType.String, userRegDetails.Customer_ID);
                        db.AddInParameter(dbCommand, "CallTypeID", DbType.Int32, userRegDetails.CallTypeID);
                        db.AddInParameter(dbCommand, "SubTypeID", DbType.Int32, userRegDetails.SubTypeID);
                        db.AddInParameter(dbCommand, "DepartmentType", DbType.String, userRegDetails.DepartmentType);
                        db.AddInParameter(dbCommand, "SeverityLevel", DbType.String, userRegDetails.Severity_Level);
                        db.AddInParameter(dbCommand, "CallReceiptDate", DbType.DateTime, userRegDetails.Call_Receipt_Date);
                        db.AddInParameter(dbCommand, "CallReceiptMode", DbType.String, userRegDetails.Call_Receipt_Mode);
                        db.AddInParameter(dbCommand, "QueryDescription", DbType.String, userRegDetails.Query_Desc);
                        db.AddInParameter(dbCommand, "OfficerContacted", DbType.String, userRegDetails.Officer_Contacted);
                        db.AddInParameter(dbCommand, "EarlierContactDate", DbType.DateTime, userRegDetails.Earlier_Contact_Date);
                        db.AddInParameter(dbCommand, "EarlierResolution", DbType.String, userRegDetails.Earlier_Resolution);
                        db.AddInParameter(dbCommand, "CurrentStatus", DbType.String, userRegDetails.Current_Status);
                        db.AddInParameter(dbCommand, "CallLogger", DbType.String, userRegDetails.CallLogger);
                        db.AddInParameter(dbCommand, "EarlierCallNumber", DbType.String, userRegDetails.EarlierCallNumber);
                        db.AddInParameter(dbCommand, "UserCode", DbType.String, userRegDetails.Code);
                        db.AddInParameter(dbCommand, "QueryType", DbType.String, userRegDetails.QueryType);
                        db.AddInParameter(dbCommand, "Bank", DbType.String, userRegDetails.Bank);
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "CallID") != null)
                        {
                            userRegDetails.CallID = (System.Int32)db.GetParameterValue(dbCommand, "CallID");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return Convert.ToString(userRegDetails.CallID);
        }
        //End of--------Insertion for Userfeedback for both user and Customer-------------------------------------------
        //To get SubType value------------------------------------------------------------------------------------------
        public DataTable GetSubTypeDetails(UserRegDetails userRegDetails)
        {
            DataTable dtSubType = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("proc_CMS_SubType", cn);
                cmd.Parameters.Add("@QueryType", SqlDbType.Char, 25).Value = userRegDetails.QueryType;
                cmd.Parameters.Add("@CallTypeID", SqlDbType.VarChar, 10).Value = userRegDetails.CallTypeID;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtSubType);
                cn.Close();
            }
            catch
            {
                dtSubType = null;
            }
            return dtSubType;
        }
        //End of ------To get SubType value-----------------------------------------------------------------------------
        //To get SubType value------------------------------------------------------------------------------------------
        public DataTable GetCallTypeDetail(UserRegDetails userRegDetails)
        {
            DataTable dtCallTye = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("proc_CMS_CallQueryType", cn);
                cmd.Parameters.Add("@QueryType", SqlDbType.Char, 25).Value = userRegDetails.QueryType;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtCallTye);
                cn.Close();
            }
            catch
            {
                dtCallTye = null;
            }
            return dtCallTye;
        }
        //End of --------To get SubType value---------------------------------------------------------------------------
        //Added for Serch User feedback---------------------------------------------------------------------------------
        public DataTable GetUserfeedbackSerchResult(UserRegDetails userRegDetails)//(string CallID, string EmailID)
        {
            DataTable dtUserfeedbackSerchResult = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("sp_Search_User_FeedBack", cn);
                cmd.Parameters.Add("@CallID", SqlDbType.NVarChar, 18).Value = userRegDetails.CallID.ToString(); //CallID;
                cmd.Parameters.Add("@EmailID", SqlDbType.NVarChar, 200).Value = userRegDetails.Customer_Email.ToString(); //EmailID;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtUserfeedbackSerchResult);
                cn.Close();
            }
            catch
            {
                dtUserfeedbackSerchResult = null;
            }
            return dtUserfeedbackSerchResult;
        }
        //End of -------Added for Serch User feedback-------------------------------------------------------------------
        //Added for Customer feeback search result----------------------------------------------------------------------
        public DataTable GetCustomerfeedbackSerchResult(UserRegDetails userRegDetails)
        {
            DataTable dtUserfeedbackSerchResult = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("sp_Search_FeedBack", cn);
                cmd.Parameters.Add("@CallType", SqlDbType.NVarChar, 18).Value = userRegDetails.CallTypeID;
                cmd.Parameters.Add("@CallID", SqlDbType.NVarChar, 18).Value = userRegDetails.CallID;

                cmd.Parameters.Add("@PolicyNo", SqlDbType.NVarChar, 15).Value = userRegDetails.PolicyNumber;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = userRegDetails.Current_Status;
                cmd.Parameters.Add("@FromDate", SqlDbType.NVarChar, 50).Value = userRegDetails.Feedback_FromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.NVarChar, 50).Value = userRegDetails.Feedback_ToDate;
                cmd.Parameters.Add("@ClientID", SqlDbType.NVarChar, 8).Value = userRegDetails.ClientID;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtUserfeedbackSerchResult);
                cn.Close();
            }
            catch
            {
                dtUserfeedbackSerchResult = null;
            }
            return dtUserfeedbackSerchResult;
        }
        //End of ------Added for Customer feeback search result---------------------------------------------------------
        //Get GetResolutionProvided-------------------------------------------------------------------------------------
        public DataTable GetResolutionProvidedDetails(UserRegDetails userRegDetails)    //(string CallID)
        {
            DataTable dtResolutionProvided = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("sp_CallDetails_ResolutionProvided", cn);
                cmd.Parameters.Add("@CallID", SqlDbType.NVarChar, 18).Value = userRegDetails.CallID.ToString(); //CallID;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtResolutionProvided);
                cn.Close();
            }
            catch
            {
                dtResolutionProvided = null;
            }
            return dtResolutionProvided;
        }
        //End of  GetResolutionProvided---------------------------------------------------------------------------------
        //End of ------Added for CMS on 02-09-10------------------------------------------------------------------------

        //----------------------------------  New Code on 01-Mar-2011  --------------------------------//
        //----------------------------------  New Code for Day 2 Deployment    ------------------------//

        /// <summary>
        /// Insert Record in the General Transaction Table.
        /// </summary>
        public System.String InsertGeneralTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {

                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("CP_ConnectionString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_General";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "PolicyNumber", DbType.String, userRegDetails.PolicyNumber);
                        db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "TransactionDate", DbType.DateTime, userRegDetails.TransactionDate);
                        db.AddInParameter(dbCommand, "TransactionType", DbType.String, userRegDetails.TransactionType);
                        db.AddInParameter(dbCommand, "TransactionComment", DbType.String, userRegDetails.TransactionComment);
                        db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, userRegDetails.UpdatedBy);
                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        db.AddInParameter(dbCommand, "TransactionResult", DbType.String, userRegDetails.TransactionResult);
                        db.AddInParameter(dbCommand, "Source", DbType.String, userRegDetails.Source);
                        db.AddInParameter(dbCommand, "Destination", DbType.String, userRegDetails.Destination);
                        if (userRegDetails.CRMId != "")
                            db.AddInParameter(dbCommand, "CRMID", DbType.String, userRegDetails.CRMId);
                        else
                            db.AddInParameter(dbCommand, "CRMID", DbType.String, "");
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Helpercls.LogError(ex);
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        /// <summary>
        /// Insert Record in the Premium Payment Table.
        /// </summary>
        public System.String InsertPremiumPaymentTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_PremiumPayment";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "PremiumDueAmount", DbType.Decimal, userRegDetails.PremiumDueAmount);
                        db.AddInParameter(dbCommand, "PremiumAmount", DbType.Decimal, userRegDetails.PremiumAmount);
                        db.AddInParameter(dbCommand, "ExistingMode", DbType.String, userRegDetails.ExistingMode);
                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        db.AddInParameter(dbCommand, "BankName", DbType.String, userRegDetails.BankName);
                        db.AddInParameter(dbCommand, "PaymentCode", DbType.String, userRegDetails.PaymentCode);
                        db.AddInParameter(dbCommand, "PaymentGateway", DbType.String, userRegDetails.PaymentGateway);
                        db.AddInParameter(dbCommand, "CurrentStatus", DbType.String, userRegDetails.CurrentStatus);

                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        /// <summary>
        /// Insert Record in the Fund Switch Table.
        /// </summary>
        public System.String InsertFundSwitchTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_FundSwitch";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);

                        db.AddInParameter(dbCommand, "FundName1", DbType.String, userRegDetails.FundName1);
                        db.AddInParameter(dbCommand, "FundValue1", DbType.Decimal, userRegDetails.FundValue1);
                        db.AddInParameter(dbCommand, "SwitchPercent1", DbType.Decimal, userRegDetails.SwitchPercent1);
                        db.AddInParameter(dbCommand, "SwitchAmount1", DbType.Decimal, userRegDetails.SwitchAmount1);
                        db.AddInParameter(dbCommand, "FundsToPercent1", DbType.Decimal, userRegDetails.FundsToPercent1);
                        db.AddInParameter(dbCommand, "FundsToAmount1", DbType.Decimal, userRegDetails.FundsToAmount1);

                        db.AddInParameter(dbCommand, "FundName2", DbType.String, userRegDetails.FundName2);
                        db.AddInParameter(dbCommand, "FundValue2", DbType.Decimal, userRegDetails.FundValue2);
                        db.AddInParameter(dbCommand, "SwitchPercent2", DbType.Decimal, userRegDetails.SwitchPercent2);
                        db.AddInParameter(dbCommand, "SwitchAmount2", DbType.Decimal, userRegDetails.SwitchAmount2);
                        db.AddInParameter(dbCommand, "FundsToPercent2", DbType.Decimal, userRegDetails.FundsToPercent2);
                        db.AddInParameter(dbCommand, "FundsToAmount2", DbType.Decimal, userRegDetails.FundsToAmount2);

                        db.AddInParameter(dbCommand, "FundName3", DbType.String, userRegDetails.FundName3);
                        db.AddInParameter(dbCommand, "FundValue3", DbType.Decimal, userRegDetails.FundValue3);
                        db.AddInParameter(dbCommand, "SwitchPercent3", DbType.Decimal, userRegDetails.SwitchPercent3);
                        db.AddInParameter(dbCommand, "SwitchAmount3", DbType.Decimal, userRegDetails.SwitchAmount3);
                        db.AddInParameter(dbCommand, "FundsToPercent3", DbType.Decimal, userRegDetails.FundsToPercent3);
                        db.AddInParameter(dbCommand, "FundsToAmount3", DbType.Decimal, userRegDetails.FundsToAmount3);

                        db.AddInParameter(dbCommand, "FundName4", DbType.String, userRegDetails.FundName4);
                        db.AddInParameter(dbCommand, "FundValue4", DbType.Decimal, userRegDetails.FundValue4);
                        db.AddInParameter(dbCommand, "SwitchPercent4", DbType.Decimal, userRegDetails.SwitchPercent4);
                        db.AddInParameter(dbCommand, "SwitchAmount4", DbType.Decimal, userRegDetails.SwitchAmount4);
                        db.AddInParameter(dbCommand, "FundsToPercent4", DbType.Decimal, userRegDetails.FundsToPercent4);
                        db.AddInParameter(dbCommand, "FundsToAmount4", DbType.Decimal, userRegDetails.FundsToAmount4);

                        db.AddInParameter(dbCommand, "FundName5", DbType.String, userRegDetails.FundName5);
                        db.AddInParameter(dbCommand, "FundValue5", DbType.Decimal, userRegDetails.FundValue5);
                        db.AddInParameter(dbCommand, "SwitchPercent5", DbType.Decimal, userRegDetails.SwitchPercent5);
                        db.AddInParameter(dbCommand, "SwitchAmount5", DbType.Decimal, userRegDetails.SwitchAmount5);
                        db.AddInParameter(dbCommand, "FundsToPercent5", DbType.Decimal, userRegDetails.FundsToPercent5);
                        db.AddInParameter(dbCommand, "FundsToAmount5", DbType.Decimal, userRegDetails.FundsToAmount5);

                        db.AddInParameter(dbCommand, "FundName6", DbType.String, userRegDetails.FundName6);
                        db.AddInParameter(dbCommand, "FundValue6", DbType.Decimal, userRegDetails.FundValue6);
                        db.AddInParameter(dbCommand, "SwitchPercent6", DbType.Decimal, userRegDetails.SwitchPercent6);
                        db.AddInParameter(dbCommand, "SwitchAmount6", DbType.Decimal, userRegDetails.SwitchAmount6);
                        db.AddInParameter(dbCommand, "FundsToPercent6", DbType.Decimal, userRegDetails.FundsToPercent6);
                        db.AddInParameter(dbCommand, "FundsToAmount6", DbType.Decimal, userRegDetails.FundsToAmount6);

                        db.AddInParameter(dbCommand, "FundName7", DbType.String, userRegDetails.FundName7);
                        db.AddInParameter(dbCommand, "FundValue7", DbType.Decimal, userRegDetails.FundValue7);
                        db.AddInParameter(dbCommand, "SwitchPercent7", DbType.Decimal, userRegDetails.SwitchPercent7);
                        db.AddInParameter(dbCommand, "SwitchAmount7", DbType.Decimal, userRegDetails.SwitchAmount7);
                        db.AddInParameter(dbCommand, "FundsToPercent7", DbType.Decimal, userRegDetails.FundsToPercent7);
                        db.AddInParameter(dbCommand, "FundsToAmount7", DbType.Decimal, userRegDetails.FundsToAmount7);

                        db.AddInParameter(dbCommand, "FundName8", DbType.String, userRegDetails.FundName8);
                        db.AddInParameter(dbCommand, "FundValue8", DbType.Decimal, userRegDetails.FundValue8);
                        db.AddInParameter(dbCommand, "SwitchPercent8", DbType.Decimal, userRegDetails.SwitchPercent8);
                        db.AddInParameter(dbCommand, "SwitchAmount8", DbType.Decimal, userRegDetails.SwitchAmount8);
                        db.AddInParameter(dbCommand, "FundsToPercent8", DbType.Decimal, userRegDetails.FundsToPercent8);
                        db.AddInParameter(dbCommand, "FundsToAmount8", DbType.Decimal, userRegDetails.FundsToAmount8);

                        db.AddInParameter(dbCommand, "FundName9", DbType.String, userRegDetails.FundName9);
                        db.AddInParameter(dbCommand, "FundValue9", DbType.Decimal, userRegDetails.FundValue9);
                        db.AddInParameter(dbCommand, "SwitchPercent9", DbType.Decimal, userRegDetails.SwitchPercent9);
                        db.AddInParameter(dbCommand, "SwitchAmount9", DbType.Decimal, userRegDetails.SwitchAmount9);
                        db.AddInParameter(dbCommand, "FundsToPercent9", DbType.Decimal, userRegDetails.FundsToPercent9);
                        db.AddInParameter(dbCommand, "FundsToAmount9", DbType.Decimal, userRegDetails.FundsToAmount9);

                        db.AddInParameter(dbCommand, "FundName10", DbType.String, userRegDetails.FundName10);
                        db.AddInParameter(dbCommand, "FundValue10", DbType.Decimal, userRegDetails.FundValue10);
                        db.AddInParameter(dbCommand, "SwitchPercent10", DbType.Decimal, userRegDetails.SwitchPercent10);
                        db.AddInParameter(dbCommand, "SwitchAmount10", DbType.Decimal, userRegDetails.SwitchAmount10);
                        db.AddInParameter(dbCommand, "FundsToPercent10", DbType.Decimal, userRegDetails.FundsToPercent10);
                        db.AddInParameter(dbCommand, "FundsToAmount10", DbType.Decimal, userRegDetails.FundsToAmount10);


                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        /// <summary>
        /// Insert Record in the Top Up Premium Table.
        /// </summary>
        public System.String InsertTopUpPremiumTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {

                //Retrieve database object.....................
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_TopUpPremium";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "TopUpEligibilityAmount", DbType.Decimal, userRegDetails.TopUpEligibilityAmount);
                        db.AddInParameter(dbCommand, "TopUpAmount", DbType.Decimal, userRegDetails.TopUpAmount);
                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        /// <summary>
        /// Insert Record in the Reinstatement Table.
        /// </summary>
        public System.String InsertReinstatementTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {

                // //Retrieve database object..........
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_Reinstatement";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "ReinstatementAmountDue", DbType.Decimal, userRegDetails.ReinstatementAmountDue);
                        db.AddInParameter(dbCommand, "ReinstatementAmount", DbType.Decimal, userRegDetails.ReinstatementAmount);
                        db.AddInParameter(dbCommand, "OutstandingPremium", DbType.Decimal, userRegDetails.OutstandingPremium);
                        db.AddInParameter(dbCommand, "ServicetaxAndEducationCessamount", DbType.Decimal, userRegDetails.ServicetaxAndEducationCessamount);
                        db.AddInParameter(dbCommand, "ReinstatementFee", DbType.Decimal, userRegDetails.ReinstatementFee);
                        db.AddInParameter(dbCommand, "AdjustmentAmount", DbType.Decimal, userRegDetails.AdjustmentAmount);
                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        db.AddInParameter(dbCommand, "BankName", DbType.String, userRegDetails.BankName);
                        db.AddInParameter(dbCommand, "PaymentCode", DbType.String, userRegDetails.PaymentCode);
                        db.AddInParameter(dbCommand, "PaymentGateway", DbType.String, userRegDetails.PaymentGateway);
                        db.AddInParameter(dbCommand, "CurrentStatus", DbType.String, userRegDetails.CurrentStatus);

                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }


        /// <summary>
        /// Insert Record in the Nominee Change Table.
        /// </summary>
        public System.String InsertNomineeChangeTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {

                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_NomineeChange";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "ExistingClientId", DbType.String, userRegDetails.ExistingClientId);
                        db.AddInParameter(dbCommand, "NewNomineeClientId", DbType.String, userRegDetails.NewNomineeClientId);
                        db.AddInParameter(dbCommand, "RelationshipWithInsured", DbType.String, userRegDetails.RelationshipWithInsured);
                        db.AddInParameter(dbCommand, "Gender", DbType.String, userRegDetails.Gender);
                        db.AddInParameter(dbCommand, "GivenName", DbType.String, userRegDetails.GivenName);
                        db.AddInParameter(dbCommand, "EffectiveDate", DbType.String, userRegDetails.EffectiveDate);
                        db.AddInParameter(dbCommand, "HouseTelephone", DbType.String, userRegDetails.HouseTelephone);
                        db.AddInParameter(dbCommand, "Line1", DbType.String, userRegDetails.Line1);
                        db.AddInParameter(dbCommand, "Line2", DbType.String, userRegDetails.Line2);
                        db.AddInParameter(dbCommand, "Line3", DbType.String, userRegDetails.Line3);
                        db.AddInParameter(dbCommand, "Married", DbType.String, userRegDetails.Married);
                        db.AddInParameter(dbCommand, "MobileNumber", DbType.String, userRegDetails.MobileNumber);
                        db.AddInParameter(dbCommand, "Nationality", DbType.String, userRegDetails.Nationality);
                        db.AddInParameter(dbCommand, "Occupation", DbType.String, userRegDetails.Occupation);
                        db.AddInParameter(dbCommand, "PIN", DbType.String, userRegDetails.PIN);
                        db.AddInParameter(dbCommand, "Salutation", DbType.String, userRegDetails.Salutation);
                        db.AddInParameter(dbCommand, "State", DbType.String, userRegDetails.State);
                        db.AddInParameter(dbCommand, "Street", DbType.String, userRegDetails.Street);
                        db.AddInParameter(dbCommand, "Surname", DbType.String, userRegDetails.Surname);
                        db.AddInParameter(dbCommand, "BirthDate", DbType.String, userRegDetails.BirthDate);
                        db.AddInParameter(dbCommand, "Country", DbType.String, userRegDetails.Country);
                        db.AddInParameter(dbCommand, "OptionalTelephone", DbType.String, userRegDetails.OptionalTelephone);
                        db.AddInParameter(dbCommand, "Email", DbType.String, userRegDetails.Email);
                        db.AddInParameter(dbCommand, "BusinessOrResidence", DbType.String, userRegDetails.BusinessOrResidence);
                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        /// <summary>
        /// Insert Record in the AppointeeDetails Table.
        /// </summary>
        public System.String InsertAppointeeTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {

                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_ApppointeeDetails";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "ExistingClientId", DbType.String, userRegDetails.ExistingClientId);
                        db.AddInParameter(dbCommand, "NewAppointeeClientId", DbType.String, userRegDetails.NewAppointeeClientId);
                        //db.AddInParameter(dbCommand, "RelationshipWithNominee", DbType.String, userRegDetails.RelationshipWithInsured);
                        db.AddInParameter(dbCommand, "RelationshipWithNominee", DbType.String, userRegDetails.RelationshipWithNominee);
                        db.AddInParameter(dbCommand, "Gender", DbType.String, userRegDetails.Gender);
                        db.AddInParameter(dbCommand, "GivenName", DbType.String, userRegDetails.GivenName);
                        db.AddInParameter(dbCommand, "EffectiveDate", DbType.String, userRegDetails.EffectiveDate);
                        db.AddInParameter(dbCommand, "HouseTelephone", DbType.String, userRegDetails.HouseTelephone);
                        db.AddInParameter(dbCommand, "Line1", DbType.String, userRegDetails.Line1);
                        db.AddInParameter(dbCommand, "Line2", DbType.String, userRegDetails.Line2);
                        db.AddInParameter(dbCommand, "Line3", DbType.String, userRegDetails.Line3);
                        db.AddInParameter(dbCommand, "Married", DbType.String, userRegDetails.Married);
                        db.AddInParameter(dbCommand, "MobileNumber", DbType.String, userRegDetails.MobileNumber);
                        db.AddInParameter(dbCommand, "Nationality", DbType.String, userRegDetails.Nationality);
                        db.AddInParameter(dbCommand, "Occupation", DbType.String, userRegDetails.Occupation);
                        db.AddInParameter(dbCommand, "PIN", DbType.String, userRegDetails.PIN);
                        db.AddInParameter(dbCommand, "Salutation", DbType.String, userRegDetails.Salutation);
                        db.AddInParameter(dbCommand, "State", DbType.String, userRegDetails.State);
                        db.AddInParameter(dbCommand, "Street", DbType.String, userRegDetails.Street);
                        db.AddInParameter(dbCommand, "Surname", DbType.String, userRegDetails.Surname);
                        db.AddInParameter(dbCommand, "BirthDate", DbType.String, userRegDetails.BirthDate);
                        db.AddInParameter(dbCommand, "Country", DbType.String, userRegDetails.Country);
                        db.AddInParameter(dbCommand, "OptionalTelephone", DbType.String, userRegDetails.OptionalTelephone);
                        db.AddInParameter(dbCommand, "Email", DbType.String, userRegDetails.Email);
                        db.AddInParameter(dbCommand, "BusinessOrResidence", DbType.String, userRegDetails.BusinessOrResidence);
                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        /// <summary>
        /// Insert Record in the Billing Mode Change Table.
        /// </summary>
        public System.String InsertBillingModeChangeTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {

                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_BillingModeChange";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "ExistingBillingMode", DbType.String, userRegDetails.ExistingBillingMode);
                        db.AddInParameter(dbCommand, "NewBillingMode", DbType.String, userRegDetails.NewBillingMode);
                        db.AddInParameter(dbCommand, "NewPremiumAmount", DbType.Decimal, userRegDetails.NewPremiumAmount);
                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        /// <summary>
        /// Insert Record in the Premium Redirection Table.
        /// </summary>
        public System.String InsertPremiumRedirectionTransactionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {

                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_PremiumRedirection";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);

                        db.AddInParameter(dbCommand, "FundName1", DbType.String, userRegDetails.FundName1);
                        db.AddInParameter(dbCommand, "FundValue1", DbType.Decimal, userRegDetails.FundsToAmount1);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage1", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage1);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage1", DbType.Decimal, userRegDetails.NewApportionmentPercentage1);

                        db.AddInParameter(dbCommand, "FundName2", DbType.String, userRegDetails.FundName2);
                        db.AddInParameter(dbCommand, "FundValue2", DbType.Decimal, userRegDetails.FundsToAmount2);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage2", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage2);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage2", DbType.Decimal, userRegDetails.NewApportionmentPercentage2);

                        db.AddInParameter(dbCommand, "FundName3", DbType.String, userRegDetails.FundName3);
                        db.AddInParameter(dbCommand, "FundValue3", DbType.Decimal, userRegDetails.FundsToAmount3);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage3", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage3);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage3", DbType.Decimal, userRegDetails.NewApportionmentPercentage3);

                        db.AddInParameter(dbCommand, "FundName4", DbType.String, userRegDetails.FundName4);
                        db.AddInParameter(dbCommand, "FundValue4", DbType.Decimal, userRegDetails.FundsToAmount4);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage4", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage4);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage4", DbType.Decimal, userRegDetails.NewApportionmentPercentage4);

                        db.AddInParameter(dbCommand, "FundName5", DbType.String, userRegDetails.FundName5);
                        db.AddInParameter(dbCommand, "FundValue5", DbType.Decimal, userRegDetails.FundsToAmount5);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage5", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage5);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage5", DbType.Decimal, userRegDetails.NewApportionmentPercentage5);

                        db.AddInParameter(dbCommand, "FundName6", DbType.String, userRegDetails.FundName6);
                        db.AddInParameter(dbCommand, "FundValue6", DbType.Decimal, userRegDetails.FundsToAmount6);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage6", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage6);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage6", DbType.Decimal, userRegDetails.NewApportionmentPercentage6);

                        db.AddInParameter(dbCommand, "FundName7", DbType.String, userRegDetails.FundName7);
                        db.AddInParameter(dbCommand, "FundValue7", DbType.Decimal, userRegDetails.FundsToAmount7);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage7", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage7);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage7", DbType.Decimal, userRegDetails.NewApportionmentPercentage7);

                        db.AddInParameter(dbCommand, "FundName8", DbType.String, userRegDetails.FundName8);
                        db.AddInParameter(dbCommand, "FundValue8", DbType.Decimal, userRegDetails.FundsToAmount8);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage8", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage8);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage8", DbType.Decimal, userRegDetails.NewApportionmentPercentage8);

                        db.AddInParameter(dbCommand, "FundName9", DbType.String, userRegDetails.FundName9);
                        db.AddInParameter(dbCommand, "FundValue9", DbType.Decimal, userRegDetails.FundsToAmount9);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage9", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage9);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage9", DbType.Decimal, userRegDetails.NewApportionmentPercentage9);

                        db.AddInParameter(dbCommand, "FundName10", DbType.String, userRegDetails.FundName10);
                        db.AddInParameter(dbCommand, "FundValue10", DbType.Decimal, userRegDetails.FundsToAmount10);
                        db.AddInParameter(dbCommand, "CurrentApportionmentPercentage10", DbType.Decimal, userRegDetails.CurrentApportionmentPercentage10);
                        db.AddInParameter(dbCommand, "NewApportionmentPercentage10", DbType.Decimal, userRegDetails.NewApportionmentPercentage10);

                        db.AddInParameter(dbCommand, "UpdatedDateTime", DbType.DateTime, userRegDetails.UpdatedDateTime);
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }


        /// <summary>
        /// Update user action details table for logout
        /// </summary>
        public System.String UpdateUserActionDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                Database db = DatabaseFactory.CreateDatabase(databaseName);
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "aspnet_pUpdateUserActionDetails";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "ActionType", DbType.String, userRegDetails.ActionType);

                        if (userRegDetails.ActionEndDateTime == DateTime.MinValue)
                        {
                            db.AddInParameter(dbCommand, "ActionEndDateTime", DbType.DateTime, null);
                        }
                        else
                        {
                            db.AddInParameter(dbCommand, "ActionEndDateTime", DbType.DateTime, userRegDetails.ActionEndDateTime);
                        }

                        db.AddInParameter(dbCommand, "ActionSessionID", DbType.String, userRegDetails.ActionSessionID);

                        db.AddOutParameter(dbCommand, "ActionID", DbType.String, 50);
                        //-----------------------------------------------------------


                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand);

                        if (db.GetParameterValue(dbCommand, "@ActionID") != null)
                        {
                            userRegDetails.ActionID = (System.String)db.GetParameterValue(dbCommand, "@ActionID");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.ActionID;
        }

        #region "This code is for MIS functionality---"
        
        /// <summary>
        /// Number of Pass / Fail Transactions per day
        /// </summary>
        /// <param name="TransactionResult">Pass/Fail</param>
        /// <param name="TransactionType">Fundswitch/PremiumRedirection/NomineeChange/Premium Payment/BillingModeChange/TopUpPremium/Reinstatement</param>
        /// <returns></returns>
        public int GetNumberOfStatus(string TransactionResult, string TransactionType, string TransactionDate)
        {
            Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
            String sqlCommand = String.Empty;
            DbCommand dbCommand = null;
            int rowCount;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    sqlCommand = "pro_GetNoOfPASSTranctionDetails";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    //This values should go as blank in the database
                    db.AddInParameter(dbCommand, "TransactionResult", DbType.String, TransactionResult);
                    db.AddInParameter(dbCommand, "TransactionType", DbType.String, TransactionType);
                    db.AddInParameter(dbCommand, "TransactionDate", DbType.String, TransactionDate);
                    rowCount = Convert.ToInt32(db.ExecuteScalar(dbCommand));


                    // Commit the transaction.
                    transaction.Commit();
                    connection.Close();
                }
                catch
                {
                    // Roll back the transaction.
                    transaction.Rollback();
                    connection.Close();
                    rowCount = -1;
                    throw;
                }
            }
            return rowCount;
        }


        /// <summary>
        /// Number of Pass  Transactions per day By Portal only
        /// </summary>
        /// <param name="TransactionResult">Pass/Fail</param>
        /// <param name="TransactionType">Fundswitch/PremiumRedirection/NomineeChange/Premium Payment/BillingModeChange/TopUpPremium/Reinstatement</param>
        /// <returns></returns>
        public int GetNumberOfStatusOfPassByPortal(string TransactionResult, string TransactionType, string TransactionDate, string UpdatedBy, string Destination)
        {
            Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
            String sqlCommand = String.Empty;
            DbCommand dbCommand = null;
            int rowCount;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    sqlCommand = "pro_GetNoOfOnlyPASSTranctionDetailsFromPortal";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    //This values should go as blank in the database
                    db.AddInParameter(dbCommand, "TransactionResult", DbType.String, TransactionResult);
                    db.AddInParameter(dbCommand, "TransactionType", DbType.String, TransactionType);
                    db.AddInParameter(dbCommand, "TransactionDate", DbType.String, TransactionDate);
                    db.AddInParameter(dbCommand, "UpdatedBy", DbType.String, UpdatedBy);
                    db.AddInParameter(dbCommand, "Destination", DbType.String, Destination);
                    rowCount = Convert.ToInt32(db.ExecuteScalar(dbCommand));


                    // Commit the transaction.
                    transaction.Commit();
                    connection.Close();
                }
                catch
                {
                    // Roll back the transaction.
                    transaction.Rollback();
                    connection.Close();
                    rowCount = -1;
                    throw;
                }
            }
            return rowCount;
        }

        /// <summary>
        /// Number of Pass / Fail Transactions per day
        /// </summary>
        /// <param name="TransactionType">Fundswitch/PremiumRedirection/NomineeChange/Premium Payment/BillingModeChange/TopUpPremium/Reinstatement</param>
        /// <returns></returns>
        public int GetNumberOfTotalTransaction(string TransactionType, string TransactionDate)
        {
            Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
            String sqlCommand = String.Empty;
            DbCommand dbCommand = null;
            int rowCount;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    sqlCommand = "pro_GetTotalNoOfTranction";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    //This values should go as blank in the database
                    db.AddInParameter(dbCommand, "TransactionType", DbType.String, TransactionType);
                    db.AddInParameter(dbCommand, "TransactionDate", DbType.String, TransactionDate);
                    rowCount = Convert.ToInt32(db.ExecuteScalar(dbCommand));


                    // Commit the transaction.
                    transaction.Commit();
                    connection.Close();
                }
                catch
                {
                    // Roll back the transaction.
                    transaction.Rollback();
                    connection.Close();
                    rowCount = -1;
                    throw;
                }
            }
            return rowCount;
        }

        public DataTable GetDetailsTransaction(string TransactionType, string TransactionResult, string TransactionDate)
        {
            DataTable dtDetailsTransaction = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("pro_GetFailTranctionDetails", cn);
                cmd.Parameters.Add("@TransactionResult", SqlDbType.NVarChar, 20).Value = TransactionResult;
                cmd.Parameters.Add("@TransactionType", SqlDbType.NVarChar, 20).Value = TransactionType;
                cmd.Parameters.Add("@TransactionDate", SqlDbType.NVarChar, 40).Value = TransactionDate;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtDetailsTransaction);
                cn.Close();
            }
            catch
            {
                dtDetailsTransaction = null;
            }

            return dtDetailsTransaction;
        }
        #endregion

        public int GetTransactionId(string stTransactionId)
        {
            Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
            String sqlCommand = String.Empty;
            DbCommand dbCommand = null;
            int rowCount;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    sqlCommand = "Get_transaction_ID";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    //This values should go as blank in the database
                    db.AddInParameter(dbCommand, "TransactionId", DbType.String, stTransactionId);
                    rowCount = Convert.ToInt32(db.ExecuteScalar(dbCommand));


                    // Commit the transaction.
                    transaction.Commit();
                    connection.Close();
                }
                catch
                {
                    // Roll back the transaction.
                    transaction.Rollback();
                    connection.Close();
                    rowCount = -1;
                    throw;
                }

            }
            return rowCount;
        }

        public DataTable GetDLifeAssureDetail(string Policy_Number, string Role)
        {
            DataTable dtDetailsLifeAssure = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("pro_GetLifeAssuredName", cn);
                cmd.Parameters.Add("@Policy_Number", SqlDbType.NVarChar, 10).Value = Policy_Number;
                cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = Role;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtDetailsLifeAssure);
                cn.Close();
            }
            catch
            {
                dtDetailsLifeAssure = null;
            }

            return dtDetailsLifeAssure;
        }

        public string GetMaxTransactionId()
        {
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(sqlConnectionString);
            cmd = new SqlCommand("transaction_ID_MAX", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            string id = cmd.ExecuteScalar().ToString();
            cn.Close();
            return id;
        }

        public string GetMinTransactionId()
        {
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection(sqlConnectionString);
            cmd = new SqlCommand("transaction_ID_MIN", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            string id = cmd.ExecuteScalar().ToString();
            cn.Close();
            return id;
        }

        //----------------------------------  End Of  Day 2 Deployment    -----------------------------//
        
        // --- Added for Life Assured is minor on 14th march 2011----------//
        public DataTable CheckLifeAssuredForNomineeChange(UserRegDetails userRegDetails)//(string CallID, string EmailID)
        {
            DataTable dtCheckLifeAssuredForNomineeChange = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("pro_Check_LifeAssured_For_NomineeChange", cn);
                cmd.Parameters.Add("@PolicyNumber", SqlDbType.NVarChar, 10).Value = userRegDetails.PolicyNumber.ToString(); //CallID;
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar).Value = "0";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtCheckLifeAssuredForNomineeChange);
                cn.Close();
            }
            catch
            {
                dtCheckLifeAssuredForNomineeChange = null;
            }
            return dtCheckLifeAssuredForNomineeChange;
        }
    
        // ---------- End of 14th March 2011 -----------//
    // -- Added on 29th March 2011 Nominee change is not allowed for the assignment Policy.. //


        public string CheckAssigneeForNomineeChange(UserRegDetails userRegDetails)//(string CallID, string EmailID)
        {
            string isAssigneePresent = string.Empty;

            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("pro_Check_Assignee_For_NomineeChange", cn);
                cmd.Parameters.Add("@PolicyNumber", SqlDbType.NVarChar, 10).Value = userRegDetails.PolicyNumber.ToString(); //CallID;
                //cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "0";
                SqlParameter parameter = cmd.Parameters.Add("@Status", SqlDbType.VarChar, 1);
                parameter.Direction = ParameterDirection.Output;

                cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //adapter.Fill(dtCheckAssigneeForNomineeChange);

                SqlDataReader myReader = cmd.ExecuteReader();
                isAssigneePresent = Convert.ToString(parameter.Value);

                cn.Close();
            }
            catch
            {
                isAssigneePresent = string.Empty;
            }
            return isAssigneePresent;
        }
              
        
        
        // -- End of 29th March 2011--//

        
 //---------------------------------------Start of Added on 23 -07-2012 to get Client Data for PAN card Validation
        public DataTable GetClientInfoDetails(string Client_ID)
        {
            DataTable dtUserDetails = new DataTable();
            try
            {
                string sqlConnectionString = 

                ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new 

                SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("pro_GetClientInfo", cn);
                cmd.Parameters.Add("@Client_ID", 

                SqlDbType.NVarChar, 10).Value = Client_ID;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new 

                SqlDataAdapter(cmd);
                adapter.Fill(dtUserDetails);
                cn.Close();
            }
            catch
            {
                dtUserDetails = null;

            }

            return dtUserDetails;
        }
        //---------------------------------------End of Added on 23-07-12 to get Client Data for PAN card Validation


        public DataTable GetPolicyDetails(string PolicyNumber)
        {
            DataTable dtPolicyDetails = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("pro_CMS_Grievance_Details", cn);
                cmd.Parameters.Add("@PolicyNo", SqlDbType.NVarChar, 10).Value = PolicyNumber;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtPolicyDetails);
                cn.Close();
            }
            catch
            {
                dtPolicyDetails = null;

            }

            return dtPolicyDetails;
        }


        #region[Added for Annuity plan details on 24 july 2013]
        public DataTable GetAnnuityDetails(string PolicyNo)
        {
            DataTable dtUserDetails = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["TransactionConnectingString"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("GetAnnuityDetails", cn);
                cmd.Parameters.Add("@Policy_Number", SqlDbType.NVarChar, 10).Value = PolicyNo;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtUserDetails);
                cn.Close();
            }
            catch
            {
                dtUserDetails = null;

            }

            return dtUserDetails;
        }

        #endregion

        /* BRS Email - New fuction added to get the User COntact details 19 Jun 2014 Starts here*/
        /// <summary>
        /// Get Client Contact details
        /// </summary>
        /// <param name="PolicyNumber"></param>
        /// <returns></returns>
        public DataTable GetClientContactDetails(string ClientID)
        {
            DataTable dtClientDetails = new DataTable();
            try
            {
                string sqlConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();
                SqlCommand cmd;
                SqlConnection cn = new SqlConnection(sqlConnectionString);
                cn.Open();
                cmd = new SqlCommand("pro_ClientDetails", cn);
                cmd.Parameters.Add("@ClientId", SqlDbType.NVarChar, 10).Value = ClientID;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtClientDetails);
                cn.Close();
            }
            catch
            {
                dtClientDetails = null;
            }

            return dtClientDetails;
        }
        /* BRS Email - New fuction added to get the User COntact details 28 May 2014 ends here*/

        public System.String InsertTransactionFailErrorMsg(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_ErrorResponse";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "ClientId", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "PolicyNumber", DbType.String , userRegDetails.PolicyNumber);
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String , userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "ErrorResponse", DbType.String, userRegDetails.TransFailErrorMsg);
                        db.AddInParameter(dbCommand, "PaymentGateway", DbType.String , userRegDetails.PaymentGateway);
                        db.AddInParameter(dbCommand, "TransactionType", DbType.String, userRegDetails.TransactionType);
 
                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }

        public System.String InsertEmailSMS_SentDetails(UserRegDetails userRegDetails)
        {
            if (userRegDetails != null)
            {
                //Retrieve database object
                Database db = DatabaseFactory.CreateDatabase("TransactionConnectingString");
                String sqlCommand = String.Empty;
                DbCommand dbCommand = null;

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        sqlCommand = "transaction_details_EmailSMSDetails";
                        dbCommand = db.GetStoredProcCommand(sqlCommand);

                        //This values should go as blank in the database
                        db.AddInParameter(dbCommand, "ClientID", DbType.String, userRegDetails.ClientID);
                        db.AddInParameter(dbCommand, "PolicyNumber", DbType.String, userRegDetails.PolicyNumber);
                        db.AddInParameter(dbCommand, "TransactionId", DbType.String, userRegDetails.TransactionID);
                        db.AddInParameter(dbCommand, "Alt_Email", DbType.String, userRegDetails.AltEmailId);
                        db.AddInParameter(dbCommand, "Alt_MobileNo", DbType.String, userRegDetails.AltMobileNo );                        
                        db.AddInParameter(dbCommand, "Email_FLag", DbType.Boolean, userRegDetails.EmailFlag );
                        db.AddInParameter(dbCommand, "SMS_Flag", DbType.Boolean, userRegDetails.SmsFlag );
                        db.AddInParameter(dbCommand, "Reg_Email", DbType.String, userRegDetails.RegEmailId );
                        db.AddInParameter(dbCommand, "Reg_MobileNo", DbType.String, userRegDetails.RegMobileNo);
                        db.AddInParameter(dbCommand, "TransactionType", DbType.String, userRegDetails.TransactionType);

                        //--------------------------------------------------------------------------------

                        int DBErrorcode = -555;

                        DBErrorcode = db.ExecuteNonQuery(dbCommand, transaction);
                        if (db.GetParameterValue(dbCommand, "TransactionId") != null)
                        {
                            userRegDetails.TransactionID = (System.String)db.GetParameterValue(dbCommand, "TransactionId");
                        }

                        // Commit the transaction.
                        transaction.Commit();
                        connection.Close();
                    }
                    catch
                    {
                        // Roll back the transaction.
                        transaction.Rollback();
                        connection.Close();
                        throw;
                    }
                }
            }
            return (System.String)userRegDetails.TransactionID;
        }
    }
}
