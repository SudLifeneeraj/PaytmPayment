using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WIP_Report.Models;
using Dapper;
namespace WIP_Report_Repository
{
    public class Repository
    {
        //public void GetData(Input input)
        //{
        //    //if (ConfigurationManager.AppSettings["EnableReqResLog"].ToString().ToUpper() == "Y")
        //    //{
        //        string conString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //        SqlConnection con = null;
        //        try
        //        {
        //            con = new SqlConnection(conString);
        //            SqlCommand cmd = new SqlCommand("usp_GetData", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@CodeType", input.CodeType);
        //            cmd.Parameters.AddWithValue("@Code", input.Code);
        //            cmd.CommandTimeout = 600;
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            //LogError(ex);
        //        }
        //        finally
        //        {
        //            if (con.State == ConnectionState.Open)
        //            {
        //                con.Close();
        //            }
        //        }
        //    //}

        //}



      
      



       






      




        public IEnumerable<Input> GetPlaceofStudyCountry(string PlaceofStudyCountry)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
            {
                try
                {
                    var paramater = new DynamicParameters();
                    paramater.Add("@PlaceofStudyCountry", PlaceofStudyCountry);

                    return con.Query<Input>("usp_CheckAllMasterPlaceofStudyCountry", paramater, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    // Helper.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }

            }
        }

        public IEnumerable<Input> GetNaomneemastery(string RelationshipwithLifeAssured)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
            {
                try
                {
                    var paramater = new DynamicParameters();
                    paramater.Add("@RelationshipwithLifeAssured", RelationshipwithLifeAssured);

                    return con.Query<Input>("usp_CheckAllNomneedetails", paramater, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    // Helper.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }

            }
        }


        public IEnumerable<Input> GetAppointeeRelation(string RelationshipwithNominee)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
            {
                try
                {
                    var paramater = new DynamicParameters();
                    paramater.Add("@RelationshipwithNominee", RelationshipwithNominee);

                    return con.Query<Input>("usp_CheckAllApoentee", paramater, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    // Helper.LogError(ex);
                    //Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                    return null;
                }

            }
        }



       
    }
}