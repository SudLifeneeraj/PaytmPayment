using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Threading;
using WIP_Report.Helper;

namespace WIP_Report.Helper
{
    public class ErrorLog
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        #region variables
        private string ErrorFileName = "", pathToErrorFile = "", FolderPath = "";
        #endregion

        #region constructor
        public ErrorLog()
        {

        }
        #endregion

        #region LogData
        public void LogData(string strMessage, string errorPath)
        {
            ErrorFileName = DateTime.Now.ToString("yyyyMMdd") + "_SUDCPSiteLog" + ".csv";

            if (errorPath == string.Empty)
            {
                pathToErrorFile = FolderPath + @"\" + ErrorFileName;
            }
            else
            {
                pathToErrorFile = errorPath + @"\" + ErrorFileName;
            }

            try
            {
                // Set Status to Locked
                _readWriteLock.EnterWriteLock();

                if (File.Exists(pathToErrorFile))
                {

                    using (StreamWriter sw = File.AppendText(pathToErrorFile))
                    {
                        sw.WriteLine(strMessage.Trim() + "," + DateTime.Now.ToString(CultureInfo.CurrentUICulture) + Environment.NewLine);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(pathToErrorFile))
                    {
                        sw.WriteLine(strMessage.Trim() + "," + DateTime.Now.ToString(CultureInfo.CurrentUICulture) + Environment.NewLine);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                HelperClass.LogError(ex);

                // throw;
            }
            finally
            {
                // Release lock
                _readWriteLock.ExitWriteLock();
            }


        }
        #endregion
    }
}