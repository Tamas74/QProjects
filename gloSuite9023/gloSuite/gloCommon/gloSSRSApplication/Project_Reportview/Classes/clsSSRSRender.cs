using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using gloSSRSApplication.SSRS;
using gloSSRSApplication.rsExecService;
using gloSettings;



namespace gloSSRSApplication
{

     public class clsSSRSRender
    {

        private SSRS.ReportingService2005 rs;
        private rsExecService.ReportExecutionService rsExec;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        string ReportProtocol = string.Empty;
        string Reportserver = string.Empty;
        string Reportfolder = string.Empty;
        string VirtualDirectory = string.Empty;
        string CustomizedReportfolder = string.Empty;

        string dbConn = string.Empty;
        string dbServerName = string.Empty;
        string dbName = string.Empty;
        Boolean SQLAuth;
        string dbUser = string.Empty;
        string dbPassword = string.Empty;


        public clsSSRSRender(string _dbServerName, string _dbName, Boolean _SQLAuth, string _dbUser, string _dbPassword)
       {
           GeneralSettings oSetting = new GeneralSettings(appSettings["DataBaseConnectionString"].ToString());
           object oValue = new object();

           oSetting.GetSetting("ReportProtocol", out oValue);
           if (oValue != null)
           {
               ReportProtocol = oValue.ToString();
               oValue = null;
           }

           oSetting.GetSetting("ReportServer", out oValue);
           if (oValue != null)
           {   Reportserver = oValue.ToString();
               oValue = null;
           }

           oSetting.GetSetting("ReportFolder", out oValue);
           if (oValue != null)
           {   Reportfolder = oValue.ToString();
               oValue = null;
           }

           oSetting.GetSetting("ReportVirtualDirectory", out oValue);
           if (oValue != null)
           {   VirtualDirectory = oValue.ToString();
               oValue = null;
           }

           oSetting.GetSetting("CustomizedReportFolder", out oValue);
           if (oValue != null)
           {
               CustomizedReportfolder = oValue.ToString();
               oValue = null;
           }

           dbConn = appSettings["DataBaseConnectionString"].ToString();
           dbServerName = _dbServerName;
           dbName = _dbName;
           SQLAuth = _SQLAuth;
           dbUser = _dbUser;
           dbPassword = _dbPassword;

           oSetting.Dispose();
           oSetting = null;
       }

        public void SSRSGeneratePDF(string ReportName, string ParamName, String ParamValue,String PDFFileName)
        {
            Cursor.Current  = Cursors.WaitCursor;
            if ((!string.IsNullOrEmpty(ReportProtocol)) && (!string.IsNullOrEmpty(Reportserver)) && (!string.IsNullOrEmpty(Reportfolder)) && (!string.IsNullOrEmpty(VirtualDirectory)) && (!string.IsNullOrEmpty(CustomizedReportfolder)))
            {
                gloSSRS.Create_Datasource("dsEMR", "gloEMR", dbConn, dbServerName, dbName, SQLAuth, dbUser, dbPassword, true, ReportProtocol, Reportserver, CustomizedReportfolder, Reportfolder, VirtualDirectory);
            }
            else
            {
                gloSSRS.Create_Datasource("dsEMR", "gloEMR", dbConn, dbServerName, dbName, SQLAuth, dbUser, dbPassword, true);
            }
            
            rs = new SSRS.ReportingService2005();
            rsExec = new rsExecService.ReportExecutionService();
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Url = ReportProtocol + "://" + Reportserver + "/" + VirtualDirectory + "/ReportService2005.asmx";
            rsExec.Url = ReportProtocol + "://" + Reportserver + "/" + VirtualDirectory + "/ReportExecution2005.asmx";

            // Prepare Render arguments
            string historyID = null;
            string deviceInfo = null;
            string format = "PDF";
            Byte[] results;
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            rsExecService.Warning[] warnings = null;
            string[] streamIDs = null;

            // Default Path;
            string fileName = PDFFileName;//@"c:\samplereport.PDF"
            string _reportName = @"/" + Reportfolder + "/" + ReportName;

            string _historyID = null;
            bool _forRendering = false;

            SSRS.ParameterValue[] _values = null;
            SSRS.DataSourceCredentials[] _credentials = null;
            SSRS.ReportParameter[] _parameters = null;

            try
            {
                _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                rsExecService.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);

                if (ParamName != "")
                {
                    string[] SplitReportParamName = ParamName.Split(',');
                    string[] SplitReportParamValue = ParamValue.Split(',');

                    int ParamCount = SplitReportParamName.Length;
                    rsExecService.ParameterValue[] parameters = new rsExecService.ParameterValue[ParamCount];
                    if (_parameters.Length > 0)
                    {
                        for (int j = 0; j <= ParamCount - 1; j++)
                        {
                            parameters[j] = new rsExecService.ParameterValue();
                            parameters[j].Label = SplitReportParamName[j];
                            parameters[j].Name = SplitReportParamName[j];
                            parameters[j].Value = SplitReportParamValue[j].Replace("@", ",");
                        }
                    }
                    rsExec.SetExecutionParameters(parameters, "en-us");
                    SplitReportParamName = null;
                    SplitReportParamValue = null;
                    parameters = null;
                }
                results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                using (FileStream stream = File.OpenWrite(fileName))
                {
                    stream.Write(results, 0, results.Length);
                }
                if (rsExec != null)
                {
                    rsExec.Dispose();
                    rsExec = null;
                }
                if (rs != null)
                {
                    rs.Dispose();
                    rs = null;
                }
                Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                historyID = null;
                deviceInfo = null;
                format = null;
                results = null;
                encoding = null;
                mimeType = null;
                extension = null;
                warnings = null;
                streamIDs = null;

                fileName = null;
                _reportName = null;

                _historyID = null;

                _values = null;
                _credentials = null;
                _parameters = null;

                Cursor.Current = Cursors.Default;
            }

        }
    }

}
