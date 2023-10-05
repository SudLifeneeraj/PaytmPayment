using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WIP_Report.Models;
using WIP_Report_Repository;

namespace WIP_Report.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            Input input = new Input();


          //  input.FullName = "neera";
           // input.Gender = "M";




            return View();
        }
	}
}