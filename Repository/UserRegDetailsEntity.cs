namespace SUD.BusinessEntities
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	
	/// <summary>
	/// Represents a UserRegDetails object.
	/// </summary>
	public class UserRegDetails : BusinessEntityBase
	{
        private System.String clientID;
		public System.String ClientID
		{
			get{ return clientID; }
			set{ clientID = value; }
		}
        public string CRMId { get; set; }

        private System.Int32 failedPasswordAttemptCount;
        public System.Int32 FailedPasswordAttemptCount
        {
            get { return failedPasswordAttemptCount; }
            set { failedPasswordAttemptCount = value; }
        }

		private System.DateTime createDate;
		public System.DateTime CreateDate
		{
			get{ return createDate; }
			set{ createDate = value; }
		}

		private System.DateTime currentTimeUtc;
		public System.DateTime CurrentTimeUtc
		{
			get{ return currentTimeUtc; }
			set{ currentTimeUtc = value; }
		}

        private System.String dateOfBirth;
        public System.String DateOfBirth
		{
			get{ return dateOfBirth; }
			set{ dateOfBirth = value; }
		}

		private System.String firstName;
		public System.String FirstName
		{
			get{ return firstName; }
			set{ firstName = value; }
		}

        private System.String middleName;
        public System.String MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        private System.String lastName;
        public System.String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private System.String landLine;
        public System.String LandLine
		{
			get{ return landLine; }
			set{ landLine = value; }
		}

		private System.String loginID;
		public System.String LoginID
		{
			get{ return loginID; }
			set{ loginID = value; }
		}

		private System.String mailID;
		public System.String MailID
		{
			get{ return mailID; }
			set{ mailID = value; }
		}

		private System.String mobileNumber1;
        public System.String MobileNumber1
		{
			get{ return mobileNumber1; }
			set{ mobileNumber1 = value; }
		}

        private System.String mobileNumber2;
        public System.String MobileNumber2
		{
			get{ return mobileNumber2; }
			set{ mobileNumber2 = value; }
		}

		private System.Byte[] password;
		public System.Byte[] Password
		{
			get{ return password; }
			set{ password = value; }
		}
        //Added on 13-05
        private System.String source;
        public System.String Source
        {
            get { return source; }
            set { source = value; }
        }
        private System.String destination;
        public System.String Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        //End Added on 13-05
        #region Password
        private string strPassword;
        public string NPassword
        {
            get { return strPassword; }
            set { strPassword = value; }
        } 
        #endregion
        #region PasswordSalt
        private string strPasswordSalt;
        public string PasswordSalt
        {
            get { return strPasswordSalt; }
            set { strPasswordSalt = value; }
        } 
        #endregion
        private System.String passwordAnswer;
		public System.String PasswordAnswer
		{
			get{ return passwordAnswer; }
			set{ passwordAnswer = value; }
		}

		private System.String passwordQuestion;
		public System.String PasswordQuestion
		{
			get{ return passwordQuestion; }
			set{ passwordQuestion = value; }
		}

		private System.String policyNumber;
		public System.String PolicyNumber
		{
			get{ return policyNumber; }
			set{ policyNumber = value; }
		}

		private System.Int32 serialNumber;
		public System.Int32 SerialNumber
		{
			get{ return serialNumber; }
			set{ serialNumber = value; }
		}

        ///////////////////////////////////////////////////////////

        private System.String actionID;
        public System.String ActionID
        {
            get { return actionID; }
            set { actionID = value; }
        }

        private System.String actionType;
        public System.String ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }

        private System.String actionResult;
        public System.String ActionResult
        {
            get { return actionResult; }
            set { actionResult = value; }
        }

        private System.DateTime actionStartDateTime;
        public System.DateTime ActionStartDateTime
        {
            get { return actionStartDateTime; }
            set { actionStartDateTime = value; }
        }

        private System.DateTime actionEndDateTime;
        public System.DateTime ActionEndDateTime
        {
            get { return actionEndDateTime; }
            set { actionEndDateTime = value; }
        }

        private System.String actionSessionID;
        public System.String ActionSessionID
        {
            get { return actionSessionID; }
            set { actionSessionID = value; }
        }
        ///////////////////////////////////////////////////////////
        //////////  TRANSACTION OBJECTS ///////////////////////////

        private System.String transactionID;
        public System.String TransactionID
        {
            get { return transactionID; }
            set { transactionID = value; }
        }
       
        //Premium Payment ----------------------------
        private System.Decimal premiumDueAmount;
        public System.Decimal PremiumDueAmount
        {
            get { return premiumDueAmount; }
            set { premiumDueAmount = value; }
        }

        private System.Decimal premiumAmount;
        public System.Decimal PremiumAmount
        {
            get { return premiumAmount; }
            set { premiumAmount = value; }
        }

        private System.String existingMode;
        public System.String ExistingMode
        {
            get { return existingMode; }
            set { existingMode = value; }
        }
        //Premium Payment End ------------------------

        private System.DateTime transactionDate;
        public System.DateTime TransactionDate
        {
            get { return transactionDate; }
            set { transactionDate = value; }
        }

        private System.String transactionType;
        public System.String TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }
        }

        private System.String transactionComment;
        public System.String TransactionComment
        {
            get { return transactionComment; }
            set { transactionComment = value; }
        }

        private System.String updatedBy;
        public System.String UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        private System.DateTime updatedDateTime;
        public System.DateTime UpdatedDateTime
        {
            get { return updatedDateTime; }
            set { updatedDateTime = value; }
        }

        private System.String transactionResult;
        public System.String TransactionResult
        {
            get { return transactionResult; }
            set { transactionResult = value; }
        }

        //Fund Switch ----------------------------

        private System.String fundName1;
        public System.String FundName1
        {
            get { return fundName1; }
            set { fundName1 = value; }
        }

        private System.Decimal fundValue1;
        public System.Decimal FundValue1
        {
            get { return fundValue1; }
            set { fundValue1 = value; }
        }

        private System.Decimal switchPercent1;
        public System.Decimal SwitchPercent1
        {
            get { return switchPercent1; }
            set { switchPercent1 = value; }
        }

        private System.Decimal switchAmount1;
        public System.Decimal SwitchAmount1
        {
            get { return switchAmount1; }
            set { switchAmount1 = value; }
        }

        private System.Decimal fundsToPercent1;
        public System.Decimal FundsToPercent1
        {
            get { return fundsToPercent1; }
            set { fundsToPercent1 = value; }
        }

        private System.Decimal fundsToAmount1;
        public System.Decimal FundsToAmount1
        {
            get { return fundsToAmount1; }
            set { fundsToAmount1 = value; }
        }

        private System.String fundName2;
        public System.String FundName2
        {
            get { return fundName2; }
            set { fundName2 = value; }
        }

        private System.Decimal fundValue2;
        public System.Decimal FundValue2
        {
            get { return fundValue2; }
            set { fundValue2 = value; }
        }

        private System.Decimal switchPercent2;
        public System.Decimal SwitchPercent2
        {
            get { return switchPercent2; }
            set { switchPercent2 = value; }
        }

        private System.Decimal switchAmount2;
        public System.Decimal SwitchAmount2
        {
            get { return switchAmount2; }
            set { switchAmount2 = value; }
        }

        private System.Decimal fundsToPercent2;
        public System.Decimal FundsToPercent2
        {
            get { return fundsToPercent2; }
            set { fundsToPercent2 = value; }
        }

        private System.Decimal fundsToAmount2;
        public System.Decimal FundsToAmount2
        {
            get { return fundsToAmount2; }
            set { fundsToAmount2 = value; }
        }

        private System.String fundName3;
        public System.String FundName3
        {
            get { return fundName3; }
            set { fundName3 = value; }
        }

        private System.Decimal fundValue3;
        public System.Decimal FundValue3
        {
            get { return fundValue3; }
            set { fundValue3 = value; }
        }

        private System.Decimal switchPercent3;
        public System.Decimal SwitchPercent3
        {
            get { return switchPercent3; }
            set { switchPercent3 = value; }
        }

        private System.Decimal switchAmount3;
        public System.Decimal SwitchAmount3
        {
            get { return switchAmount3; }
            set { switchAmount3 = value; }
        }

        private System.Decimal fundsToPercent3;
        public System.Decimal FundsToPercent3
        {
            get { return fundsToPercent3; }
            set { fundsToPercent3 = value; }
        }

        private System.Decimal fundsToAmount3;
        public System.Decimal FundsToAmount3
        {
            get { return fundsToAmount3; }
            set { fundsToAmount3 = value; }
        }

        private System.String fundName4;
        public System.String FundName4
        {
            get { return fundName4; }
            set { fundName4 = value; }
        }

        private System.Decimal fundValue4;
        public System.Decimal FundValue4
        {
            get { return fundValue4; }
            set { fundValue4 = value; }
        }

        private System.Decimal switchPercent4;
        public System.Decimal SwitchPercent4
        {
            get { return switchPercent4; }
            set { switchPercent4 = value; }
        }

        private System.Decimal switchAmount4;
        public System.Decimal SwitchAmount4
        {
            get { return switchAmount4; }
            set { switchAmount4 = value; }
        }

        private System.Decimal fundsToPercent4;
        public System.Decimal FundsToPercent4
        {
            get { return fundsToPercent4; }
            set { fundsToPercent4 = value; }
        }

        private System.Decimal fundsToAmount4;
        public System.Decimal FundsToAmount4
        {
            get { return fundsToAmount4; }
            set { fundsToAmount4 = value; }
        }

        private System.String fundName5;
        public System.String FundName5
        {
            get { return fundName5; }
            set { fundName5 = value; }
        }

        private System.Decimal fundValue5;
        public System.Decimal FundValue5
        {
            get { return fundValue5; }
            set { fundValue5 = value; }
        }

        private System.Decimal switchPercent5;
        public System.Decimal SwitchPercent5
        {
            get { return switchPercent5; }
            set { switchPercent5 = value; }
        }

        private System.Decimal switchAmount5;
        public System.Decimal SwitchAmount5
        {
            get { return switchAmount5; }
            set { switchAmount5 = value; }
        }

        private System.Decimal fundsToPercent5;
        public System.Decimal FundsToPercent5
        {
            get { return fundsToPercent5; }
            set { fundsToPercent5 = value; }
        }

        private System.Decimal fundsToAmount5;
        public System.Decimal FundsToAmount5
        {
            get { return fundsToAmount5; }
            set { fundsToAmount5 = value; }
        }

        private System.String fundName6;
        public System.String FundName6
        {
            get { return fundName6; }
            set { fundName6 = value; }
        }

        private System.Decimal fundValue6;
        public System.Decimal FundValue6
        {
            get { return fundValue6; }
            set { fundValue6 = value; }
        }

        private System.Decimal switchPercent6;
        public System.Decimal SwitchPercent6
        {
            get { return switchPercent6; }
            set { switchPercent6 = value; }
        }

        private System.Decimal switchAmount6;
        public System.Decimal SwitchAmount6
        {
            get { return switchAmount6; }
            set { switchAmount6 = value; }
        }

        private System.Decimal fundsToPercent6;
        public System.Decimal FundsToPercent6
        {
            get { return fundsToPercent6; }
            set { fundsToPercent6 = value; }
        }

        private System.Decimal fundsToAmount6;
        public System.Decimal FundsToAmount6
        {
            get { return fundsToAmount6; }
            set { fundsToAmount6 = value; }
        }

        private System.String fundName7;
        public System.String FundName7
        {
            get { return fundName7; }
            set { fundName7 = value; }
        }

        private System.Decimal fundValue7;
        public System.Decimal FundValue7
        {
            get { return fundValue7; }
            set { fundValue7 = value; }
        }

        private System.Decimal switchPercent7;
        public System.Decimal SwitchPercent7
        {
            get { return switchPercent7; }
            set { switchPercent7 = value; }
        }

        private System.Decimal switchAmount7;
        public System.Decimal SwitchAmount7
        {
            get { return switchAmount7; }
            set { switchAmount7 = value; }
        }

        private System.Decimal fundsToPercent7;
        public System.Decimal FundsToPercent7
        {
            get { return fundsToPercent7; }
            set { fundsToPercent7 = value; }
        }

        private System.Decimal fundsToAmount7;
        public System.Decimal FundsToAmount7
        {
            get { return fundsToAmount7; }
            set { fundsToAmount7 = value; }
        }

        private System.String fundName8;
        public System.String FundName8
        {
            get { return fundName8; }
            set { fundName8 = value; }
        }

        private System.Decimal fundValue8;
        public System.Decimal FundValue8
        {
            get { return fundValue8; }
            set { fundValue8 = value; }
        }

        private System.Decimal switchPercent8;
        public System.Decimal SwitchPercent8
        {
            get { return switchPercent8; }
            set { switchPercent8 = value; }
        }

        private System.Decimal switchAmount8;
        public System.Decimal SwitchAmount8
        {
            get { return switchAmount8; }
            set { switchAmount8 = value; }
        }

        private System.Decimal fundsToPercent8;
        public System.Decimal FundsToPercent8
        {
            get { return fundsToPercent8; }
            set { fundsToPercent8 = value; }
        }

        private System.Decimal fundsToAmount8;
        public System.Decimal FundsToAmount8
        {
            get { return fundsToAmount8; }
            set { fundsToAmount8 = value; }
        }

        private System.String fundName9;
        public System.String FundName9
        {
            get { return fundName9; }
            set { fundName9 = value; }
        }

        private System.Decimal fundValue9;
        public System.Decimal FundValue9
        {
            get { return fundValue9; }
            set { fundValue9 = value; }
        }

        private System.Decimal switchPercent9;
        public System.Decimal SwitchPercent9
        {
            get { return switchPercent9; }
            set { switchPercent9 = value; }
        }

        private System.Decimal switchAmount9;
        public System.Decimal SwitchAmount9
        {
            get { return switchAmount9; }
            set { switchAmount9 = value; }
        }

        private System.Decimal fundsToPercent9;
        public System.Decimal FundsToPercent9
        {
            get { return fundsToPercent9; }
            set { fundsToPercent9 = value; }
        }

        private System.Decimal fundsToAmount9;
        public System.Decimal FundsToAmount9
        {
            get { return fundsToAmount9; }
            set { fundsToAmount9 = value; }
        }

        private System.String fundName10;
        public System.String FundName10
        {
            get { return fundName10; }
            set { fundName10 = value; }
        }

        private System.Decimal fundValue10;
        public System.Decimal FundValue10
        {
            get { return fundValue10; }
            set { fundValue10 = value; }
        }

        private System.Decimal switchPercent10;
        public System.Decimal SwitchPercent10
        {
            get { return switchPercent10; }
            set { switchPercent10 = value; }
        }

        private System.Decimal switchAmount10;
        public System.Decimal SwitchAmount10
        {
            get { return switchAmount10; }
            set { switchAmount10 = value; }
        }

        private System.Decimal fundsToPercent10;
        public System.Decimal FundsToPercent10
        {
            get { return fundsToPercent10; }
            set { fundsToPercent10 = value; }
        }

        private System.Decimal fundsToAmount10;
        public System.Decimal FundsToAmount10
        {
            get { return fundsToAmount10; }
            set { fundsToAmount10 = value; }
        }

        //----------------------------------------

        //-------------- Top Up Premium ----------

        private System.Decimal topUpEligibilityAmount;
        public System.Decimal TopUpEligibilityAmount
        {
            get { return topUpEligibilityAmount; }
            set { topUpEligibilityAmount = value; }
        }

        private System.Decimal topUpAmount;
        public System.Decimal TopUpAmount
        {
            get { return topUpAmount; }
            set { topUpAmount = value; }
        }

        //----------------------------------------

        //-------------- Reinstatement -----------

        private System.Decimal reinstatementAmountDue;
        public System.Decimal ReinstatementAmountDue
        {
            get { return reinstatementAmountDue; }
            set { reinstatementAmountDue = value; }
        }

        private System.Decimal reinstatementAmount;
        public System.Decimal ReinstatementAmount
        {
            get { return reinstatementAmount; }
            set { reinstatementAmount = value; }
        }
        private System.Decimal outstandingPremium;
        public System.Decimal OutstandingPremium
        {
            get { return outstandingPremium; }
            set { outstandingPremium = value; }
        }

        private System.Decimal servicetaxAndEducationCessamount;
        public System.Decimal ServicetaxAndEducationCessamount
        {
            get { return servicetaxAndEducationCessamount; }
            set { servicetaxAndEducationCessamount = value; }
        }

        private System.Decimal reinstatementFee;
        public System.Decimal ReinstatementFee
        {
            get { return reinstatementFee; }
            set { reinstatementFee = value; }
        }
        private System.Decimal adjustmentAmount;
        public System.Decimal AdjustmentAmount
        {
            get { return adjustmentAmount; }
            set { adjustmentAmount = value; }
        }
        //----------------------------------------

        //-------------- NomineeChange -----------

        private System.String existingNomineeClientId;
        public System.String ExistingNomineeClientId
        {
            get { return existingNomineeClientId; }
            set { existingNomineeClientId = value; }
        }               

        private System.String newNomineeClientId;
        public System.String NewNomineeClientId
        {
            get { return newNomineeClientId; }
            set { newNomineeClientId = value; }
        }
        //Added on 30-06-10
        private System.String existingClientId;
        public System.String ExistingClientId
        {
            get { return existingClientId; }
            set { existingClientId = value; }
        }

        private System.String newAppointeeClientId;
        public System.String NewAppointeeClientId
        {
            get { return newAppointeeClientId; }
            set { newAppointeeClientId = value; }
        }
        private System.String relationshipWithNominee;
        public System.String RelationshipWithNominee
        {
            get { return relationshipWithNominee; }
            set { relationshipWithNominee = value; }
        }
        //End of Added on 30-06-10
        private System.String relationshipWithInsured;
        public System.String RelationshipWithInsured
        {
            get { return relationshipWithInsured; }
            set { relationshipWithInsured = value; }
        }

        private System.String effectiveDate;
        public System.String EffectiveDate
        {
            get { return effectiveDate; }
            set { effectiveDate = value; }
        }

        private System.String gender;
        public System.String Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private System.String givenName;
        public System.String GivenName
        {
            get { return givenName; }
            set { givenName = value; }
        }

        private System.String houseTelephone;
        public System.String HouseTelephone
        {
            get { return houseTelephone; }
            set { houseTelephone = value; }
        }

        private System.String line1;
        public System.String Line1
        {
            get { return line1; }
            set { line1 = value; }
        }

        private System.String line2;
        public System.String Line2
        {
            get { return line2; }
            set { line2 = value; }
        }

        private System.String line3;
        public System.String Line3
        {
            get { return line3; }
            set { line3 = value; }
        }

        private System.String married;
        public System.String Married
        {
            get { return married; }
            set { married = value; }
        }

        private System.String mobileNumber;
        public System.String MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; }
        }

        private System.String nationality;
        public System.String Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        private System.String occupation;
        public System.String Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        private System.String pin;
        public System.String PIN
        {
            get { return pin; }
            set { pin = value; }
        }

        private System.String salutation;
        public System.String Salutation
        {
            get { return salutation; }
            set { salutation = value; }
        }

        private System.String state;
        public System.String State
        {
            get { return state; }
            set { state = value; }
        }

        private System.String street;
        public System.String Street
        {
            get { return street; }
            set { street = value; }
        }

        private System.String surname;
        public System.String Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        private System.String birthDate;
        public System.String BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        private System.String country;
        public System.String Country
        {
            get { return country; }
            set { country = value; }
        }

        private System.String optionalTelephone;
        public System.String OptionalTelephone
        {
            get { return optionalTelephone; }
            set { optionalTelephone = value; }
        }

        private System.String email;
        public System.String Email
        {
            get { return email; }
            set { email = value; }
        }

        private System.String businessOrResidence;
        public System.String BusinessOrResidence
        {
            get { return businessOrResidence; }
            set { businessOrResidence = value; }
        }
        //----------------------------------------

        //---------- BillingModeChange -----------

        private System.String existingBillingMode;
        public System.String ExistingBillingMode
        {
            get { return existingBillingMode; }
            set { existingBillingMode = value; }
        }

        private System.String newBillingMode;
        public System.String NewBillingMode
        {
            get { return newBillingMode; }
            set { newBillingMode = value; }
        }

        private System.Decimal newPremiumAmount;
        public System.Decimal NewPremiumAmount
        {
            get { return newPremiumAmount; }
            set { newPremiumAmount = value; }
        }
        //----------------------------------------

        //Premium Redirection --------------------

        private System.Decimal currentApportionmentPercentage1;
        public System.Decimal CurrentApportionmentPercentage1
        {
            get { return currentApportionmentPercentage1; }
            set { currentApportionmentPercentage1 = value; }
        }

        private System.Decimal newApportionmentPercentage1;
        public System.Decimal NewApportionmentPercentage1
        {
            get { return newApportionmentPercentage1; }
            set { newApportionmentPercentage1 = value; }
        }

        private System.Decimal currentApportionmentPercentage2;
        public System.Decimal CurrentApportionmentPercentage2
        {
            get { return currentApportionmentPercentage2; }
            set { currentApportionmentPercentage2 = value; }
        }

        private System.Decimal newApportionmentPercentage2;
        public System.Decimal NewApportionmentPercentage2
        {
            get { return newApportionmentPercentage2; }
            set { newApportionmentPercentage2 = value; }
        }

        private System.Decimal currentApportionmentPercentage3;
        public System.Decimal CurrentApportionmentPercentage3
        {
            get { return currentApportionmentPercentage3; }
            set { currentApportionmentPercentage3 = value; }
        }

        private System.Decimal newApportionmentPercentage3;
        public System.Decimal NewApportionmentPercentage3
        {
            get { return newApportionmentPercentage3; }
            set { newApportionmentPercentage3 = value; }
        }

        private System.Decimal currentApportionmentPercentage4;
        public System.Decimal CurrentApportionmentPercentage4
        {
            get { return currentApportionmentPercentage4; }
            set { currentApportionmentPercentage4 = value; }
        }

        private System.Decimal newApportionmentPercentage4;
        public System.Decimal NewApportionmentPercentage4
        {
            get { return newApportionmentPercentage4; }
            set { newApportionmentPercentage4 = value; }
        }

        private System.Decimal currentApportionmentPercentage5;
        public System.Decimal CurrentApportionmentPercentage5
        {
            get { return currentApportionmentPercentage5; }
            set { currentApportionmentPercentage5 = value; }
        }

        private System.Decimal newApportionmentPercentage5;
        public System.Decimal NewApportionmentPercentage5
        {
            get { return newApportionmentPercentage5; }
            set { newApportionmentPercentage5 = value; }
        }

        private System.Decimal currentApportionmentPercentage6;
        public System.Decimal CurrentApportionmentPercentage6
        {
            get { return currentApportionmentPercentage6; }
            set { currentApportionmentPercentage6 = value; }
        }

        private System.Decimal newApportionmentPercentage6;
        public System.Decimal NewApportionmentPercentage6
        {
            get { return newApportionmentPercentage6; }
            set { newApportionmentPercentage6 = value; }
        }

        private System.Decimal currentApportionmentPercentage7;
        public System.Decimal CurrentApportionmentPercentage7
        {
            get { return currentApportionmentPercentage7; }
            set { currentApportionmentPercentage7 = value; }
        }

        private System.Decimal newApportionmentPercentage7;
        public System.Decimal NewApportionmentPercentage7
        {
            get { return newApportionmentPercentage7; }
            set { newApportionmentPercentage7 = value; }
        }

        private System.Decimal currentApportionmentPercentage8;
        public System.Decimal CurrentApportionmentPercentage8
        {
            get { return currentApportionmentPercentage8; }
            set { currentApportionmentPercentage8 = value; }
        }

        private System.Decimal newApportionmentPercentage8;
        public System.Decimal NewApportionmentPercentage8
        {
            get { return newApportionmentPercentage8; }
            set { newApportionmentPercentage8 = value; }
        }

        private System.Decimal currentApportionmentPercentage9;
        public System.Decimal CurrentApportionmentPercentage9
        {
            get { return currentApportionmentPercentage9; }
            set { currentApportionmentPercentage9 = value; }
        }

        private System.Decimal newApportionmentPercentage9;
        public System.Decimal NewApportionmentPercentage9
        {
            get { return newApportionmentPercentage9; }
            set { newApportionmentPercentage9 = value; }
        }

        private System.Decimal currentApportionmentPercentage10;
        public System.Decimal CurrentApportionmentPercentage10
        {
            get { return currentApportionmentPercentage10; }
            set { currentApportionmentPercentage10 = value; }
        }

        private System.Decimal newApportionmentPercentage10;
        public System.Decimal NewApportionmentPercentage10
        {
            get { return newApportionmentPercentage10; }
            set { newApportionmentPercentage10 = value; }
        }

        ////Premium Redirection End -----------------------


        ////Added for CustomerFeedback module------------------------------------------------------------
        //private System.String callID;
        //public System.String CallID
        //{
        //    get { return callID; }
        //    set { callID = value; }
        //}

        //private System.String customer_Name;
        //public System.String Customer_Name
        //{
        //    get { return customer_Name; }
        //    set { customer_Name = value; }
        //}

        //private System.String caller_Type;
        //public System.String Caller_Type
        //{
        //    get { return caller_Type; }
        //    set { caller_Type = value; }
        //}

        //private System.String customer_Address;
        //public System.String Customer_Address
        //{
        //    get { return customer_Address; }
        //    set { customer_Address = value; }
        //}

        //private System.String customer_Email;
        //public System.String Customer_Email
        //{
        //    get { return customer_Email; }
        //    set { customer_Email = value; }
        //}

        //private System.String customer_MobNo;
        //public System.String Customer_MobNo
        //{
        //    get { return customer_MobNo; }
        //    set { customer_MobNo = value; }
        //}

        //private System.String customer_TeleNo;
        //public System.String Customer_TeleNo
        //{
        //    get { return customer_TeleNo; }
        //    set { customer_TeleNo = value; }
        //}

        //private System.String customer_Policy_No;
        //public System.String Customer_Policy_No
        //{
        //    get { return customer_Policy_No; }
        //    set { customer_Policy_No = value; }
        //}

        //private System.String customer_ID;
        //public System.String Customer_ID
        //{
        //    get { return customer_ID; }
        //    set { customer_ID = value; }
        //}

        //private System.String applicationNumber;
        //public System.String ApplicationNumber
        //{
        //    get { return applicationNumber; }
        //    set { applicationNumber = value; }
        //}

        //private System.String callTypeID;
        //public System.String CallTypeID
        //{
        //    get { return callTypeID; }
        //    set { callTypeID = value; }
        //}


        //private System.String subTypeID;
        //public System.String SubTypeID
        //{
        //    get { return subTypeID; }
        //    set { subTypeID = value; }
        //}
        //private System.String departmentType;
        //public System.String DepartmentType
        //{
        //    get { return departmentType; }
        //    set { departmentType = value; }
        //}

        //private System.String severity_Level;
        //public System.String Severity_Level
        //{
        //    get { return severity_Level; }
        //    set { severity_Level = value; }
        //}
        //private System.String call_Receipt_Date;
        //public System.String Call_Receipt_Date
        //{
        //    get { return call_Receipt_Date; }
        //    set { call_Receipt_Date = value; }
        //}
        //private System.String call_Receipt_Mode;
        //public System.String Call_Receipt_Mode
        //{
        //    get { return call_Receipt_Mode; }
        //    set { call_Receipt_Mode = value; }
        //}
        //private System.String officer_Contacted;
        //public System.String Officer_Contacted
        //{
        //    get { return officer_Contacted; }
        //    set { officer_Contacted = value; }
        //}
        //private System.String earlier_Contact_Date;
        //public System.String Earlier_Contact_Date
        //{
        //    get { return earlier_Contact_Date; }
        //    set { earlier_Contact_Date = value; }
        //}
        //private System.String earlier_Resolution;
        //public System.String Earlier_Resolution
        //{
        //    get { return earlier_Resolution; }
        //    set { earlier_Resolution = value; }
        //}

        //private System.String query_Desc;
        //public System.String Query_Desc
        //{
        //    get { return query_Desc; }
        //    set { query_Desc = value; }
        //}
        //private System.String followUpReq;
        //public System.String FollowUpReq
        //{
        //    get { return followUpReq; }
        //    set { followUpReq = value; }
        //}
        //private System.String followUP_Date;
        //public System.String FollowUP_Date
        //{
        //    get { return followUP_Date; }
        //    set { followUP_Date = value; }
        //}
        //private System.String resol_Provided;
        //public System.String Resol_Provided
        //{
        //    get { return resol_Provided; }
        //    set { resol_Provided = value; }
        //}
        //private System.String resol_Date;
        //public System.String Resol_Date
        //{
        //    get { return resol_Date; }
        //    set { resol_Date = value; }
        //}
        //private System.String updatedResol_Provided;
        //public System.String UpdatedResol_Provided
        //{
        //    get { return updatedResol_Provided; }
        //    set { updatedResol_Provided = value; }
        //}
        //private System.String updatedResol_Date;
        //public System.String UpdatedResol_Date
        //{
        //    get { return updatedResol_Date; }
        //    set { updatedResol_Date = value; }
        //}
        //private System.String current_Status;
        //public System.String Current_Status
        //{
        //    get { return current_Status; }
        //    set { current_Status = value; }
        //}
        //private System.String callLogger;
        //public System.String CallLogger
        //{
        //    get { return callLogger; }
        //    set { callLogger = value; }
        //}
        //private System.String callResolver;
        //public System.String CallResolver
        //{
        //    get { return callResolver; }
        //    set { callResolver = value; }
        //}
        //private System.String earlierCallNumber;
        //public System.String EarlierCallNumber
        //{
        //    get { return earlierCallNumber; }
        //    set { earlierCallNumber = value; }
        //}
        //private System.String code;
        //public System.String Code
        //{
        //    get { return code; }
        //    set { code = value; }
        //}

        //private System.String lastLevel;
        //public System.String LastLevel
        //{
        //    get { return lastLevel; }
        //    set { lastLevel = value; }
        //}
        //private System.String queryType;
        //public System.String QueryType
        //{
        //    get { return queryType; }
        //    set { queryType = value; }
        //}

        //private System.String escalationDate;
        //public System.String EscalationDate
        //{
        //    get { return escalationDate; }
        //    set { escalationDate = value; }
        //}
        //private System.String bank;
        //public System.String Bank
        //{
        //    get { return bank; }
        //    set { bank = value; }
        //}


        //private System.String fromDateVal;
        //public System.String FromDateVal
        //{
        //    get { return fromDateVal; }
        //    set { fromDateVal = value; }
        //}

        //private System.String toDateVal;
        //public System.String ToDateVal
        //{
        //    get { return toDateVal; }
        //    set { toDateVal = value; }
        //}

        //private System.String client_ID;
        //public System.String Client_ID
        //{
        //    get { return client_ID; }
        //    set { client_ID = value; }
        //}
        ////End of  Added for CustomerFeedback module------------------------------------------------------------    




        //Added for CMS on 02-09-10------------------------------------------------------------
        private System.Int32 callID;
        public System.Int32 CallID
        {
            get { return callID; }
            set { callID = value; }
        }
        private System.String customer_Name;
        public System.String Customer_Name
        {
            get { return customer_Name; }
            set { customer_Name = value; }
        }
        private System.String caller_Type;
        public System.String Caller_Type
        {
            get { return caller_Type; }
            set { caller_Type = value; }
        }
        private System.String customer_Address;
        public System.String Customer_Address
        {
            get { return customer_Address; }
            set { customer_Address = value; }
        }
        private System.String customer_Email;
        public System.String Customer_Email
        {
            get { return customer_Email; }
            set { customer_Email = value; }
        }
        private System.String customer_MobNo;
        public System.String Customer_MobNo
        {
            get { return customer_MobNo; }
            set { customer_MobNo = value; }
        }
        private System.String customer_TeleNo;
        public System.String Customer_TeleNo
        {
            get { return customer_TeleNo; }
            set { customer_TeleNo = value; }
        }
        private System.String customer_Policy_No;
        public System.String Customer_Policy_No
        {
            get { return customer_Policy_No; }
            set { customer_Policy_No = value; }
        }
        private System.String customer_ID;
        public System.String Customer_ID
        {
            get { return customer_ID; }
            set { customer_ID = value; }
        }
        private System.String applicationNumber;
        public System.String ApplicationNumber
        {
            get { return applicationNumber; }
            set { applicationNumber = value; }
        }
        private System.Int32 callTypeID;
        public System.Int32 CallTypeID
        {
            get { return callTypeID; }
            set { callTypeID = value; }
        }
        private System.Int32 subTypeID;
        public System.Int32 SubTypeID
        {
            get { return subTypeID; }
            set { subTypeID = value; }
        }
        private System.String departmentType;
        public System.String DepartmentType
        {
            get { return departmentType; }
            set { departmentType = value; }
        }
        private System.String severity_Level;
        public System.String Severity_Level
        {
            get { return severity_Level; }
            set { severity_Level = value; }
        }
        private System.DateTime call_Receipt_Date;
        public System.DateTime Call_Receipt_Date
        {
            get { return call_Receipt_Date; }
            set { call_Receipt_Date = value; }
        }
        private System.String call_Receipt_Mode;
        public System.String Call_Receipt_Mode
        {
            get { return call_Receipt_Mode; }
            set { call_Receipt_Mode = value; }
        }
        private System.String officer_Contacted;
        public System.String Officer_Contacted
        {
            get { return officer_Contacted; }
            set { officer_Contacted = value; }
        }
        private System.String earlier_Contact_Date;
        public System.String Earlier_Contact_Date
        {
            get { return earlier_Contact_Date; }
            set { earlier_Contact_Date = value; }
        }
        private System.String earlier_Resolution;
        public System.String Earlier_Resolution
        {
            get { return earlier_Resolution; }
            set { earlier_Resolution = value; }
        }
        private System.String query_Desc;
        public System.String Query_Desc
        {
            get { return query_Desc; }
            set { query_Desc = value; }
        }
        private System.String followUpReq;
        public System.String FollowUpReq
        {
            get { return followUpReq; }
            set { followUpReq = value; }
        }
        private System.String followUP_Date;
        public System.String FollowUP_Date
        {
            get { return followUP_Date; }
            set { followUP_Date = value; }
        }
        private System.String resol_Provided;
        public System.String Resol_Provided
        {
            get { return resol_Provided; }
            set { resol_Provided = value; }
        }
        private System.String resol_Date;
        public System.String Resol_Date
        {
            get { return resol_Date; }
            set { resol_Date = value; }
        }
        private System.String updatedResol_Provided;
        public System.String UpdatedResol_Provided
        {
            get { return updatedResol_Provided; }
            set { updatedResol_Provided = value; }
        }
        private System.String updatedResol_Date;
        public System.String UpdatedResol_Date
        {
            get { return updatedResol_Date; }
            set { updatedResol_Date = value; }
        }
        private System.String current_Status;
        public System.String Current_Status
        {
            get { return current_Status; }
            set { current_Status = value; }
        }
        private System.String callLogger;
        public System.String CallLogger
        {
            get { return callLogger; }
            set { callLogger = value; }
        }
        private System.String callResolver;
        public System.String CallResolver
        {
            get { return callResolver; }
            set { callResolver = value; }
        }
        private System.String earlierCallNumber;
        public System.String EarlierCallNumber
        {
            get { return earlierCallNumber; }
            set { earlierCallNumber = value; }
        }
        private System.String code;
        public System.String Code
        {
            get { return code; }
            set { code = value; }
        }
        private System.String lastLevel;
        public System.String LastLevel
        {
            get { return lastLevel; }
            set { lastLevel = value; }
        }
        private System.String queryType;
        public System.String QueryType
        {
            get { return queryType; }
            set { queryType = value; }
        }
        private System.String escalationDate;
        public System.String EscalationDate
        {
            get { return escalationDate; }
            set { escalationDate = value; }
        }
        private System.String bank;
        public System.String Bank
        {
            get { return bank; }
            set { bank = value; }
        }
        private System.String fromDateVal;
        public System.String FromDateVal
        {
            get { return fromDateVal; }
            set { fromDateVal = value; }
        }
        private System.String toDateVal;
        public System.String ToDateVal
        {
            get { return toDateVal; }
            set { toDateVal = value; }
        }
        private System.String client_ID;
        public System.String Client_ID
        {
            get { return client_ID; }
            set { client_ID = value; }
        }
        private System.String feedback_FromDate;
        public System.String Feedback_FromDate
        {
            get { return feedback_FromDate; }
            set { feedback_FromDate = value; }
        }
        private System.String feedback_ToDate;
        public System.String Feedback_ToDate
        {
            get { return feedback_ToDate; }
            set { feedback_ToDate = value; }
        }

        //End of ------Added for CMS on 02-09-10------------------------------------------------------------
        // for data entry in Premiumpaymnt reinstatement and topup-- on 28-01-2016
        private System.String bankName;
        public System.String BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        //paymentCode
        private System.String paymentCode;
        public System.String PaymentCode
        {
            get { return paymentCode; }
            set { paymentCode = value; }
        }
        private System.String paymentGateway;
        public System.String PaymentGateway
        {
            get { return paymentGateway; }
            set { paymentGateway = value; }
        }
        private System.String currentStatus;
        public System.String CurrentStatus
        {
            get { return currentStatus; }
            set { currentStatus = value; }
        }
        //Transaction Fail Error Response Msg code added on 05/02/2016
        private System.String transFailErrorMsg;
        public System.String TransFailErrorMsg
        {
            get { return transFailErrorMsg; }
            set { transFailErrorMsg = value; }
        }

        //Email and SMS Sent Details Insertion code added on 08/02/2016
        private System.String altEmailId;
        public System.String AltEmailId
        {
            get { return altEmailId; }
            set { altEmailId = value; }
        }

        private System.String altMobileNo;
        public System.String AltMobileNo
        {
            get { return altMobileNo; }
            set { altMobileNo = value; }
        }
        private System.String regEmailId;
        public System.String RegEmailId
        {
            get { return regEmailId; }
            set { regEmailId = value; }
        }
        private System.String regMobileNo;
        public System.String RegMobileNo
        {
            get { return regMobileNo; }
            set { regMobileNo = value; }
        }
        private System.Boolean  emailFlag;
        public System.Boolean EmailFlag
        {
            get { return emailFlag; }
            set { emailFlag = value; }
        }
        private System.Boolean smsFlag;
        public System.Boolean SmsFlag
        {
            get { return smsFlag; }
            set { smsFlag = value; }
        }

    }
	}

