using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIP_Report.Models
{
    public class WCFProperty
    {



        public GetallDetailsResult GetallDetailsResult { get; set; }

    }
        public class GetallDetailsResult
    {

            public string StatusMsg { get; set; }
            public string AuthStatus { get; set; }
            public string ErrorStatus { get; set; }
        }
       
     
    

}