using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Management;
using gloEMRGeneralLibrary.gloEMRDatabase;
using Microsoft.VisualBasic;
using System.Net;
using gloCommunity.SPAuthentication;
using System.Web.Services.Protocols;

namespace gloCommunity.Classes
{
    public static class clsGeneral
    {
        public static Boolean Isopen = false;
        public static string WebFolder = "";
        public static string ClinicWebFolder = "";
        public static string WebSite = "";
        public static string ClinicRepository = "";
        public static string GlobalRepository = "";
        public static string ClinicXmlFolder = "";
        //'"Global Association Repository"
        public static string WebGlobalXmlFolder = "";
        public static string WebUserXmlFolder = "";
        //'"dev110:26078"
        public static string gstrCommsrv = "";
        //'Added for SharePoint server setting
        public static string gstrSharepointSrvNm = "";
        public static string gstrSharepointSiteNm = "";
        public static string gstrVti_Bin = "";
        public static string gstrListSvc = "";
        public static string gstrSiteDataSvc = "";
        public static string gstr_Layouts = "";
        public static bool IsCommunityLiquidData = false;
        public static bool IsCommunitySmartDx = false;
        public static string Webpath = "";//"http://" + gstrSharepointSrvNm + "/" + gstrSharepointSiteNm + "/";
        public static string gstrgloEMRStartupPath = "";
        public static string gstrgloTempFolder = "";
        public static string gstrClinicName = "";
        public static string gstrDomainName1 = "";
        public static string gstrMessageBoxCaption = "gloEMR";
        public static string gstrsmdxflnm = "";
        public static string gstrTskMlflnm = "";
        public static string gstrSmartCPTflnm = "";
        public static string gstrSmartOrderflnm = "";
        public static string gstrHistoryflnm = "";
        public static string gstrDmSetupflnm = "";
        public static string gstrIMSetupflnm = "";
        public static string gstrCVSetupflnm = "";
        public static bool isCVDownload = false;
        public static string strDemographic = "";
        public static string strVitals = "";
        public static string strhistory = "";
        public static string strDrugs = "";
        public static string strRadiology = "";

        public static string strLab = "";
        public static int clinicID = 1;
        public static string gstrblconfflnm = "";
        public static string gstrappconfflnm = "";
        public static string gstrformglry = "";
        public static string gstrLiquidDataFNm = "";
        public static string gstrflowshflnm = "";
        //public static Font gFont = new Font("Tahoma", 9, FontStyle.Regular);
        //public static Font gFont_SMALL = new Font("Tahoma", 8.25f, FontStyle.Regular);
        //public static Font gFont_SMALL_BOLD = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (Convert.ToByte((0))));
        //public static Font gFont_BOLD = new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
        public static Int64 gClinicID = 1;
        public static string BizTalkServerConnection;

        private static string _EMRConnectionString = "";
        private static string _DMSConnectionString = "";
        public static long gnClinicID = 0;

        public static string gstrServiceNamespace = "";
        public static string gstrDomainName = "";
        public static string gstrCommunityWebUrl = "";
        public static string gstrMgmtServiceReply = "";
        public static string gstrADFSServer = "";
        public static string gstrSharePointAutthPage = "";
        public static string gstrSharePointSiteReply = "";
        public static string gstrACSRelyingPartyurl = "";
        public static CookieContainer oCookie = null;
        public static string SamlToken = string.Empty;
        public static bool gblnIscommunityStaging = true;
        public static long gnLoginID = 0;
        //Added for gloCommunity Form authentication on 20120730
        public static string gstrgloCommunityAuthentication = string.Empty;
        public static string gstrGCUserName = string.Empty;
        public static string gstrGCPassword = string.Empty;
        public static string gstrAuthenticationWSAddress = string.Empty;
        public static Cookie oFormCookie = null;
        //End
        public enum ControlType
        {
            None = 0,
            CheckBox = 1,
            Text = 2
        }

        public enum CategoryType
        {
            None = 0,
            General = 1,
            Hitory = 2,
            Physical_Examination = 3,
            Medical_Decision_Making = 4,
            HPI = 5,
            Management_option = 6,
            Labs = 7,
            X_Ray_Radiology = 8,
            Other_Diagonsis_Tests = 9,
            ROS = 10,
            DB_History = 11
        }
        public static string EMRConnectionString
        {
            get { return _EMRConnectionString; }
            set { _EMRConnectionString = value; }
        }

        public static string DMSConnectionString
        {
            get { return _DMSConnectionString; }
            set { _DMSConnectionString = value; }
        }

        public static string getTemplateName(string ID, string Tname)
        {

            string strFileName = null;
            string nID = null;
            try
            {
                ///''''''''''''''''''''''''        
                strFileName = getFileName(Tname);
                ///''''''''''''''''''''''''
                nID = ID;
                DataBaseLayer oDB = new DataBaseLayer();
                DBParameter oParamater = default(DBParameter);

                DataTable dt = new DataTable();
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.BigInt;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@ID";
                oParamater.Value = System.Convert.ToInt64(nID);
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                dt = oDB.GetDataTable("get_Template");

                if (dt != null && dt.Rows.Count >= 1)
                    strFileName = GenerateFile(dt.Rows[0][0], strFileName);
                else
                    strFileName = "";

                return strFileName;
            }
            catch //(Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                strFileName = "";
            }
            finally
            {

            }
            return strFileName;
        }

        public static string GenerateFile(object cntFromDB, string strFileName)
        {
            try
            {
                if (cntFromDB != System.DBNull.Value)
                {
                    byte[] content = (byte[])cntFromDB;
                    //MemoryStream stream = new MemoryStream(content);
                    System.IO.FileStream oFile = new System.IO.FileStream(strFileName, System.IO.FileMode.Create);
                    oFile.Write(content, 0, content.Length);
                    //stream.WriteTo(oFile);
                    oFile.Close();
                    oFile.Dispose();
                    oFile = null;
                    return strFileName;
                }
                else
                    strFileName = ""; //Value is DBNull

            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                strFileName = "";
            }
            return strFileName;
        }

        public static string getFileName(string Tname)
        {
            try
            {
                string _Path = "";
                //if (GetOSInfo() == false)
                //{
                //    _Path = gstrgloEMRStartupPath + "\\Temp";
                //}
                //else
                //{
                //    _Path = gstrgloEMRStartupPath + gstrgloTempFolder;
                //}

                _Path = gloSettings.FolderSettings.AppTempFolderPath;

                //Code start - Added by kanchan on 20120217 to handle special characters . \ / : * ? " < > | # { } % ~ & in template name
                // Tname = Tname.Replace(".", "");
                // Tname = Tname.Replace("/", "");
                // Tname = Tname.Replace("\\", "");
                Tname = Tname.Replace(".", "_").Replace("\\", "_").Replace("/", "_").Replace(":", "_").Replace("*", "_").Replace("?", "_").Replace("\"", "_").Replace("&", "_");
                Tname = Tname.Replace("<", "_").Replace(">", "_").Replace("|", "_").Replace("#", "_").Replace("{", "_").Replace("}", "_").Replace("%", "_").Replace("~", "_").Replace("-", "_");
                //Code start - Added by kanchan on 20120206 to solve issue with '.' in template name
                Tname = Tname.TrimStart(' ');


                Tname = _Path + "\\" + Tname + ".docx";



                if (File.Exists(Tname) == true)
                {
                    File.Delete(Tname);
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());

            }
            finally
            {
            }
            return Tname;
        }

        public static void UpdateLog(string strerrormsg)
        {
            try
            {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter(gstrgloEMRStartupPath + "\\gloCommunityLog.txt", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "\t\t" + strerrormsg);
                objFile.Close();
                objFile = null;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public static long GetPrefixTransactionID()
        {
            Random oRan = new Random();
            return oRan.Next(1, Int32.MaxValue);
        }

        public static void CheckAuthenticatedCookie()
        {
            if (clsGeneral.oCookie == null)
            {
                Authentication oAuth = new gloCommunity.Classes.Authentication();
                oAuth.GetSucurityToken();
            }
            else if (clsGeneral.oCookie.GetCookies(new Uri(clsGeneral.gstrCommunityWebUrl)).Count == 0)
            {
                Authentication oAuth = new gloCommunity.Classes.Authentication();
                oAuth.FillCookieContainer();
            }
        }

        //added by seema as on 20 feb 2012
        public static Boolean getInstance(string formName, string frmtitle)
        {
            try
            {

                Isopen = false;


                foreach (Form f in Application.OpenForms)
                {
                    if ((f.Name == formName) && (f.Text == frmtitle))
                    {

                        Isopen = true;
                        f.BringToFront();
                        return Isopen;
                    }

                }
                return Isopen;
            }
            catch //(Exception ex)
            {
                return Isopen;
            }


        }

        //Added for access gloCommunity using Form authentication on 20120730
        public static Cookie QueryToSharePoint(string authenticationWSAddress, string userName, string password)
        {
            Cookie cookie = null;
            try
            {
                gloCommunity.SPAuthentication.Authentication spAuthentication = new gloCommunity.SPAuthentication.Authentication();
                spAuthentication.Url = authenticationWSAddress;
                spAuthentication.CookieContainer = new CookieContainer();

                //Try to login to SharePoint site with Form based authentication
                LoginResult loginResult = spAuthentication.Login(userName, password);
                cookie = new Cookie();
                //If login is successful
                if (loginResult.ErrorCode == LoginErrorCode.NoError)
                {
                    //Get the cookie collection from the authentication web service
                    CookieCollection cookies = spAuthentication.CookieContainer.GetCookies(new Uri(spAuthentication.Url));
                    //Get the specific cookie which contains the security token
                    cookie = cookies[loginResult.CookieName];
                    oFormCookie = cookie;
                }
            }
            catch //(SoapException exp)
            {
                //return "SOAP ERROR: " + Environment.NewLine + exp.Message;
            }
            //catch (Exception exp)
            //{
            //    //return "ERROR: " + Environment.NewLine + exp.Message;
            //}

            return cookie;
        }
        //End
    }
}
