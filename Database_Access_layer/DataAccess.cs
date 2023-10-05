using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;
using WIP_Report.Helper;
using WIP_Report.Models;

namespace WIP_Report.Database_Access_layer
{
    public class DataAccess
    {
        public DataSet GetBiztalkPolicyDetails(string ClientID)
        {
            try
            {
                WebService_GetallPolicydetailsSoapClient objGetAllPolicyDetails = new WebService_GetallPolicydetailsSoapClient();
                Logininput objLogininput = new Logininput();
                objLogininput.ClientID = ClientID; // Convert.ToString(Session["Client_id"]);//lnLogin.UserName.Trim();
                objLogininput.GetPolicyFundDetails = "Y";
                //Common Method for All
                RetriveServiceInput objPolicyDetailsInput = new RetriveServiceInput();
                objPolicyDetailsInput.Item = objLogininput;
                RetriveServiceOutput objPolicyDetailsOutput = objGetAllPolicyDetails.Operation_1(objPolicyDetailsInput);
             //   elog.LogData("GetClientData.output: Received data from Biztalk File:Website objGetAllPolicyDetails.Operation_1: Message: DataAccess", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

              //  elog.LogData("GetClientData.output:" + GetDataSet(new XmlSerializer(typeof(RetriveServiceOutput)), objPolicyDetailsOutput) + "File:Website objGetAllPolicyDetails.Operation_1: Message: DataAccess", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                // Serializing the MyPolicyOutput object to Dataset
                DataSet dsMyPolicies = GetDataSet(new XmlSerializer(typeof(RetriveServiceOutput)), objPolicyDetailsOutput);
               // elog.LogData("GetClientData.output:" + dsMyPolicies.ToString() + "File:Website objGetAllPolicyDetails.Operation_1: Message: DataAccess", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                dsMyPolicies.Tables.Add(new DataTable());
                return dsMyPolicies;
            }
            catch (Exception ex)
            {
              //  Helpercls.LogError(ex);
                return null;
            }
        }
        private string GenerateFileName()
        {
            return System.Configuration.ConfigurationManager.AppSettings["XMLFilePath"] + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond + ".XML";
        }
        public DataSet GetDataSet(XmlSerializer serializer, object res)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(GetJobReportResponseType));
            DataSet ds = new DataSet();
            try
            {

                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, res);
                    string strFileName = GenerateFileName();
                    using (FileStream f = new FileStream(strFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {

                        stream.Position = 0;

                        //ds.ReadXmlSchema(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Cmb.Notifications.UI.NotificationObjects.xsd"));
                        ds.ReadXml(stream);
                        stream.WriteTo(f);
                     //   elog.LogData("DataAccess File:creation", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        f.Close();
                        stream.Close();
                     //   elog.LogData("DataAccess File:Close", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));
                        File.Delete(strFileName);
                      //  elog.LogData("DataAccess File:Deleted", Convert.ToString(ConfigurationSettings.AppSettings["ErrorLogFolderPath"]));

                    }
                }

            }
            catch (Exception ex)
            {
             //   Helpercls.LogError(ex);
                // elog.LogData(ex.Message.ToString(), ConfigurationSettings.AppSettings["ErrorLogFolderPath"].ToString());
            }
            return ds;
        }

        public IEnumerable<UnclaimedAmount> Unclaimedamount(string ClientId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var para = new DynamicParameters();
                    para.Add("@PolicyHolderName", "");
                    para.Add("@DOB", "");
                    para.Add("@PolicyNo", "");
                    para.Add("@ApplicationNo", "");
                    para.Add("@PanNo", "");
                    para.Add("@AadharNo", "");
                    para.Add("@ClientId", ClientId);

                    return con.Query<UnclaimedAmount>("pro_claimedAmount", para, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    return null;
                }
            }
        }

        public IEnumerable<NotificationforMaturity> NotificationforMaturity(string ClientId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var para = new DynamicParameters();
                    para.Add("@PolicyHolderName", "");
                    para.Add("@DOB", "");
                    para.Add("@PolicyNo", "");
                    para.Add("@ApplicationNo", "");
                    para.Add("@PanNo", "");
                    para.Add("@AadharNo", "");
                    para.Add("@ClientId", ClientId);

                    return con.Query<NotificationforMaturity>("pro_NotificationforMaturity", para, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    return null;
                }
            }
        }

        public IEnumerable<annutiyanexture> pro_annutiyanexture(string ClientId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var para = new DynamicParameters();
                    para.Add("@PolicyHolderName", "");
                    para.Add("@DOB", "");
                    para.Add("@PolicyNo", "");
                    para.Add("@ApplicationNo", "");
                    para.Add("@PanNo", "");
                    para.Add("@AadharNo", "");
                    para.Add("@ClientId", ClientId);

                    return con.Query<annutiyanexture>("pro_annutiyanexture", para, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    return null;
                }
            }
        }

        public IEnumerable<CRMInfo> CRMcallinfo(string Client_Id, string type)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CP_ConnectionString"].ToString()))
            {
                try
                {
                    var crmpolicy = new DynamicParameters();
                    crmpolicy.Add("@Client_Id", Client_Id);
                    if (type == "Profile")
                        return con.Query<CRMInfo>("_GetCRMcallInfo", crmpolicy, null, true, 0, CommandType.StoredProcedure).ToList();
                    else
                        return con.Query<CRMInfo>("_GetCRMcallInfo_policy", crmpolicy, null, true, 0, CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    HelperClass.LogError(ex);
                    //  Helpercls.LogError(ex);
                    return null;
                }
            }
        }


        public DataSet serviceData(IEnumerable<CRMInfo> strdata)
        {
            DataSet dt = new DataSet(typeof(CRMInfo).Name);
            dt.Tables.Add(new DataTable());
            if (strdata != null)
            {
                // DataSet dt = new DataSet(typeof(CRMInfo).Name);
                // dt.Tables.Add(new DataTable());
                //Get all the properties
                PropertyInfo[] Props = typeof(CRMInfo).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names
                    dt.Tables[0].Columns.Add(prop.Name, type);
                }
                foreach (CRMInfo item in strdata)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dt.Tables[0].Rows.Add(values);
                }

            }
            else { dt = null; }
            return dt;

        }


    }
}