using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using WIP_Report.Models;
using WIP_Report_Repository;
namespace WIP_Report.Controllers
{
    public class DashboardController : ApiController
    {
       // Repository rep = new Repository();

        public JsonResult GetData()
        {
            StringBuilder sb = new StringBuilder();
            DashboardModel dm = new DashboardModel();
            Output output = new Output();
            Repository rep = new Repository();
           
               // IEnumerable<DashboardModel> ct = rep.ReAllocationGetGridDataAngular();
                JsonResult result = new JsonResult();
                result.Data = "";
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
             //   result.MaxJsonLength = int.MaxValue;
                return result;



            
        }
    }
}
