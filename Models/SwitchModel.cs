using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WIP_Report.Models
{
    public class SwitchModel
    {
        public string error { get; set; }

        public string ExistingBillingMode { get; set; }
        public string NewBillingMode { get; set; }

        public Decimal ExistingApportionment { get; set; }
        public Decimal NewApportionment { get; set; }

        public Decimal ExistingApportionment1 { get; set; }
        public Decimal NewApportionment1 { get; set; }
        public Decimal ExistingApportionment2 { get; set; }
        public Decimal NewApportionment2 { get; set; }
        public Decimal ExistingApportionment3 { get; set; }
        public Decimal NewApportionment3 { get; set; }
        public Decimal ExistingApportionment4 { get; set; }
        public Decimal NewApportionment4 { get; set; }
        public Decimal ExistingApportionment5 { get; set; }
        public Decimal NewApportionment5 { get; set; }

        public Decimal ExistingApportionment6 { get; set; }
        public Decimal NewApportionment6 { get; set; }
        public Decimal ExistingApportionment7 { get; set; }
        public Decimal NewApportionment7 { get; set; }
        public Decimal ExistingApportionment8 { get; set; }
        public Decimal NewApportionment8 { get; set; }
        public Decimal ExistingApportionment9 { get; set; }
        public Decimal NewApportionment9 { get; set; }
        public Decimal ExistingApportionment10 { get; set; }
        public Decimal NewApportionment10 { get; set; }
   

        public string FundNAME { get; set; }
        public string Nav { get; set; }
        public string Noofunits { get; set; }
        public string FundValue { get; set; }
        public Decimal Switch { get; set; }
        public string Switchamount { get; set; }
        public Decimal FundTo { get; set; }
        public string FundTOamount { get; set; }

        public string TransactionResult { get; set; }
        public string TransactionID { get; set; }
        public string ClientID { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionComment { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        public string CRMID { get; set; }

        public string FundName1 { get; set; }
        public Decimal FundValue1 { get; set; }
        public Decimal SwitchPercent1 { get; set; }
        public Decimal SwitchAmount1 { get; set; }
        public Decimal FundsToPercent1 { get; set; }
        public Decimal FundsToAmount1 { get; set; }


        public string FundName2 { get; set; }
        public Decimal FundValue2 { get; set; }
        public Decimal SwitchPercent2 { get; set; }
        public Decimal SwitchAmount2 { get; set; }
        public Decimal FundsToPercent2 { get; set; }
        public Decimal FundsToAmount2 { get; set; }

        public string FundName3 { get; set; }
        public Decimal FundValue3 { get; set; }
        public Decimal SwitchPercent3 { get; set; }
        public Decimal SwitchAmount3 { get; set; }
        public Decimal FundsToPercent3 { get; set; }
        public Decimal FundsToAmount3 { get; set; }

        public string FundName4 { get; set; }
        public Decimal FundValue4 { get; set; }
        public Decimal SwitchPercent4 { get; set; }
        public Decimal SwitchAmount4 { get; set; }
        public Decimal FundsToPercent4 { get; set; }
        public Decimal FundsToAmount4 { get; set; }

        public string FundName5 { get; set; }
        public Decimal FundValue5 { get; set; }
        public Decimal SwitchPercent5 { get; set; }
        public Decimal SwitchAmount5 { get; set; }
        public Decimal FundsToPercent5 { get; set; }
        public Decimal FundsToAmount5 { get; set; }

        public string FundName6 { get; set; }
        public Decimal FundValue6 { get; set; }
        public Decimal SwitchPercent6 { get; set; }
        public Decimal SwitchAmount6 { get; set; }
        public Decimal FundsToPercent6 { get; set; }
        public Decimal FundsToAmount6 { get; set; }

        public string FundName7 { get; set; }
        public Decimal FundValue7 { get; set; }
        public Decimal SwitchPercent7 { get; set; }
        public Decimal SwitchAmount7 { get; set; }
        public Decimal FundsToPercent7 { get; set; }
        public Decimal FundsToAmount7 { get; set; }

        public string FundName8 { get; set; }
        public Decimal FundValue8 { get; set; }
        public Decimal SwitchPercent8 { get; set; }
        public Decimal SwitchAmount8 { get; set; }
        public Decimal FundsToPercent8 { get; set; }
        public Decimal FundsToAmount8 { get; set; }

        public string FundName9 { get; set; }
        public Decimal FundValue9 { get; set; }
        public Decimal SwitchPercent9 { get; set; }
        public Decimal SwitchAmount9 { get; set; }
        public Decimal FundsToPercent9 { get; set; }
        public Decimal FundsToAmount9 { get; set; }

        public string FundName10 { get; set; }
        public Decimal FundValue10 { get; set; }
        public Decimal SwitchPercent10 { get; set; }
        public Decimal SwitchAmount10 { get; set; }
        public Decimal FundsToPercent10 { get; set; }
        public Decimal FundsToAmount10 { get; set; }
        
        public int TrnsCnt { get; set; }
        public string TrnsId { get; set; }

        public string BizTalkResponseStatus { get; set; }
        public string sessionTransId { get; set; }
    }

}