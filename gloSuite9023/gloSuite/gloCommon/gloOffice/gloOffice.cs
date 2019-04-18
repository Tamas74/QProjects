using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using gloWord;

namespace gloOffice
{
    /// <summary>
    ///  To Do the Differen Operations on the Templates
    /// </summary>
    public class gloTemplate : IDisposable
    {
        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Distructor"

        public gloTemplate(string DatabaseConnectionString)
        {
            _databaseConnectionString = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    
                    
                }
            }
            disposed = true;
        }

        ~gloTemplate()
        {
            Dispose(false);
        }

        #endregion

        #region  " Variable Declarations "
        String _databaseConnectionString = "";
        String _MessageBoxCaption = String.Empty;
        #endregion " Variable Declarations "

        private Int64 _PatientID;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        private String _PatientName;
        public String PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }
        private Int64 _TemplateID;
        public Int64 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        private String _TemplateName;
        public String TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }

        private Int64 _CategoryID;
        public Int64 CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private String _CategoryName;
        public String CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private String _TemplateFilePath;
        public String TemplateFilePath
        {
            get { return _TemplateFilePath; }
            set { _TemplateFilePath = value; }
        }

        private Int64 _ProviderID;
        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }

        private Int64 _PrimeryID;
        public Int64 PrimeryID
        {
            get { return _PrimeryID; }
            set { _PrimeryID = value; }
        }

        private Int64 _AppointmentID;
        public Int64 AppointmentID
        {
            get { return _AppointmentID; }
            set { _AppointmentID = value; }
        }

        private Int64 _DocumentCategory;
        public Int64 DocumentCategory
        {
            get { return _DocumentCategory; }
            set { _DocumentCategory = value; }
        }

        private Int64 _ClinicID;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
        // VisitID property procedure added for pulling history dataDictionary
        private Int64 _VisitID;
        public Int64 VisitID
        {
            get { return _VisitID; }
            set { _VisitID = value; }
        }

        private Int64 _FromDate;
        public Int64 FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        private Int64 _ToDate;
        public Int64 ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        //code added by SaiKrishna for PAF On 04-21-2011
        private Int64 _nPAccountID;
        public Int64 nPAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }

        private Int64 _TransactionMstID;
        public Int64 TransactionMstID
        {
            get { return _TransactionMstID; }
            set { _TransactionMstID = value; }
        }

        private Int64 _TransactionID;
        public Int64 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }

        private Boolean  _IsPatientHaveMultipleAccounts;
        public Boolean  IsPatientHaveMultipleAccounts
        {
            get { return _IsPatientHaveMultipleAccounts; }
            set { _IsPatientHaveMultipleAccounts = value; }
        }
        private Boolean _IsTemplateContainsPatientAccountFields;

        public Boolean IsTemplateContainsPatientAccountFields
        {
            get { return _IsTemplateContainsPatientAccountFields; }
            set { _IsTemplateContainsPatientAccountFields = value; }
        }
        private String _AppoinmentTime;
        public String AppoinmentTime
        {
            get { return _AppoinmentTime; }
            set { _AppoinmentTime = value; }
        }

        private Boolean _isFromDashboradAppt = false;
        public Boolean isFromDashboradAppt
        {
            get { return _isFromDashboradAppt; }
            set { _isFromDashboradAppt = value; }
        }


        public DataTable Get_DataDictionaryTables()
        {
            DataTable dt = null;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                oDB.Connect(false);
                oDB.Retrive_Query("Select Distinct sTableCaption FROM DataDictionary_MST WITH (NOLOCK) Order by sTableCaption ", out dt);
                oDB.Disconnect();
                oDB.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public DataTable Get_DataDictionaryFields(string TableCaption)
        {
            DataTable dt = null;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                oDB.Connect(false);
                oDB.Retrive_Query("Select Distinct nDictionaryID, sFieldName, sTableName, sCaption, sTableCaption  FROM DataDictionary_MST WITH (NOLOCK) WHERE sTableCaption = '" + TableCaption + "' Order by sCaption, sTableCaption ", out dt);
                oDB.Disconnect();
                oDB.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        public void PrintTemplate(Int64 _TransactionID, ref gloWord.LoadAndCloseWord myLoadWord)
        {
            DataTable dttemp = null;//SLR: new is not needed
            object Visible = false;
            try
            {
                dttemp = GetPatientTemplate(_TransactionID);

                if (dttemp != null && dttemp.Rows.Count > 0)
                {
                    //nPatientID, nTemplateID , sTemplateName , nFromDate, nToDate, nProviderID, iTemplate, nCount, nClinicID
                    PatientID = Convert.ToInt64(dttemp.Rows[0]["nPatientID"].ToString());
                    PrimeryID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
                    CategoryID = Convert.ToInt64(dttemp.Rows[0]["nCategoryID"]);
                    CategoryName = dttemp.Rows[0]["sCategoryName"].ToString();
                    TemplateID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
                    TemplateName = dttemp.Rows[0]["sTemplateName"].ToString();
                    FromDate = Convert.ToInt32(dttemp.Rows[0]["nFromDate"]);

                    //Set the File to control
                    string strNewDocumentName = "";
                    strNewDocumentName = gloOffice.Supporting.NewDocumentName();

                    object objTemplateDocument;

                    if (!string.IsNullOrEmpty(dttemp.Rows[0]["iTemplate"].ToString()))
                    {
                        objTemplateDocument = dttemp.Rows[0]["iTemplate"];

                        //Bug #63771: 00000624: Patient Statement Print
                        //When you click the 'Print' button, nothing happens at all.
                        //While click on Reprint batch nothing is happens.
                        if (TemplateName.Contains("PatientStatement"))
                        { strNewDocumentName = strNewDocumentName.Replace(".docx", ".doc"); }

                        ConvertBinaryToFile(objTemplateDocument, strNewDocumentName);

                        Wd.Document oTemp = myLoadWord.LoadWordApplication(strNewDocumentName); // oWordApp.Documents.Open(strNewDocumentName);

                        //oTemp.Application.Options.PrintBackground = true;
                        //oTemp.PrintOut(Background: true);
                        gloWord.LoadAndCloseWord.PrintWordDocument(ref oTemp, false,false,PatientID);
                        myLoadWord.CloseWordOnly(ref oTemp);

                        //oTemp.Close(SaveChanges:false);
                        //try
                        //{
                        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(oTemp);
                        //    oTemp = null;
                        //}
                        //catch
                        //{
                        //}
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.PrintLog(Ex.ToString());
                Ex = null;
            }
            finally
            {
                dttemp.Dispose(); dttemp = null;
                
                //ogloTemplate.Dispose(); 
            }
        }

        // COMMENTED BY SUDHIR - 20090123

        //public bool SaveTemplate(Int64 TemplateID, String TemplateName, Int64 CategoryID, Int64 ProviderID, String TemplateFilePath)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    oDB.Connect(false);

        //    oDBParameters.Add("@TemplateID", TemplateID, ParameterDirection.Input, SqlDbType.BigInt);
        //    oDBParameters.Add("@TemplateName", TemplateName, ParameterDirection.Input, SqlDbType.VarChar);
        //    oDBParameters.Add("@CategoryID", CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
        //    oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);

        //    gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
        //    Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(TemplateFilePath);
        //    ogloTemplate = null;
        //    //Byte[] oTemplate = gloOffice.Supporting.ConvertFileToBinary(TemplateFilePath);
        //    oDBParameters.Add("@Description", oTemplate, ParameterDirection.Input, SqlDbType.Image);
        //    oDBParameters.Add("@MachineID", 123, ParameterDirection.Input, SqlDbType.BigInt);

        //    oDB.Execute("gsp_InUpTemplateGallery_MST", oDBParameters);
        //    oDB.Disconnect();
        //    oDB.Dispose();
        //    //    [gsp_InUpTemplateGallery_MST]
        //    //    @TemplateID numeric(18,0),
        //    //    @TemplateName varchar(50),
        //    //    @CategoryID numeric(18,0),
        //    //    @ProviderID numeric(18,0),
        //    //    @Description image,
        //    //    @MachineID Numeric(18,0)=0
        //    return true;

        //}

        public Int64 SaveTemplate(Int64 TemplateID, String TemplateName, Int64 CategoryID, String CategoryName, Int64 ProviderID, String TemplateFilePath, String Bibliographicinfo, String BibliographicDeveloper)
        {
            Object oResult = null;
            Int64 _Result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            oDBParameters.Add("@TemplateID", TemplateID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            oDBParameters.Add("@TemplateName", TemplateName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@CategoryID", CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@CategoryName", CategoryName, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                      
            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(TemplateFilePath);
            ogloTemplate.Dispose();
            ogloTemplate = null;
            //Byte[] oTemplate = gloOffice.Supporting.ConvertFileToBinary(TemplateFilePath);
            oDBParameters.Add("@Description", oTemplate, ParameterDirection.Input, SqlDbType.Image);
            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

            oDBParameters.Add("@Bibliographicinfo", Bibliographicinfo, ParameterDirection.Input, SqlDbType.VarChar);
            oDBParameters.Add("@BibliographicDeveloper", BibliographicDeveloper, ParameterDirection.Input, SqlDbType.VarChar);


            oDB.Execute("gsp_InUpTemplateGallery_MST", oDBParameters, out oResult);
            oDB.Disconnect();
            oDB.Dispose();

            if (oResult != null && Convert.ToString(oResult) != "")
                _Result = Convert.ToInt64(oResult);

            return _Result;

        }

        // 00000601 :  Import templates from gloPM  not show in gloEMR
        // get category ID when importing/Updating the template in gloPM
        public Int64 GetCategoryID(string _CategoryName)
        {
            Int64 _CategoryId = 0;
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            oDB.Connect(false);
            try
            {
                string _SQLQuery = "select nCategoryID from Category_MST where sDescription = '" + _CategoryName.ToUpper().Replace("'", "''") + "'  ";

                oDB.Retrive_Query(_SQLQuery, out _result);

                if (_result != null)
                {
                    _CategoryId = Convert.ToInt64(_result.Rows[0]["nCategoryID"]);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _CategoryId;
        }
        // END

        //public DataTable GetTemplates(Int64 CategoryID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    oDB.Connect(false);
        //    string strSQL = "";
        //    DataTable dt = new DataTable();

        //    // Sudhir - 20090122 COMMENTED.  //

        //    //strSQL = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
        //    //             "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1)  " +
        //    //             " + ISNULL(Provider_MST.sLastName, '') AS sProviderName " +
        //    //             " FROM  TemplateGallery_MST LEFT OUTER JOIN " +
        //    //             " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
        //    //             " WHERE  TemplateGallery_MST.nCategoryID = " + CategoryID + " ";

        //    // NEW QUERY RETURNS CATEGORY NAME ALSO      , TemplateGallery_MST.sCategoryName as CategoryName
        //    //strSQL = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
        //    //             "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1)  " +
        //    //             " + ISNULL(Provider_MST.sLastName, '') AS sProviderName, Category_MST.sDescription AS CategoryName" +
        //    //             " FROM  TemplateGallery_MST LEFT OUTER JOIN " +
        //    //             " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
        //    //             " INNER JOIN Category_MST ON Category_MST.nCategoryID = TemplateGallery_MST.nCategoryID" +
        //    //             " WHERE  TemplateGallery_MST.nCategoryID = " + CategoryID +
        //    //             " AND	Category_MST.sCategoryType='Template'";

        //    strSQL = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, TemplateGallery_MST.nCategoryID, " +
        //                 "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1)  " +
        //                 " + ISNULL(Provider_MST.sLastName, '') AS sProviderName, TemplateGallery_MST.sCategoryName as CategoryName " +
        //                 " FROM  TemplateGallery_MST LEFT OUTER JOIN " +
        //                 " Provider_MST ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
        //                 " WHERE  TemplateGallery_MST.nCategoryID = " + CategoryID + " ";

        //    oDB.Retrive_Query(strSQL, out  dt);
        //    oDB.Disconnect();
        //    oDB.Dispose();
        //    return dt;
        //}

        public DataTable GetTemplates(String CategoryName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            string strSQL = "";
            DataTable dt = null;

            strSQL = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName,  " +
                          "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1)  " +
                          " + ISNULL(Provider_MST.sLastName, '') AS sProviderName, TemplateGallery_MST.sCategoryName as CategoryName " +
                          " FROM  TemplateGallery_MST WITH (NOLOCK) LEFT OUTER JOIN " +
                          " Provider_MST WITH (NOLOCK) ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID " +
                          " WHERE  UPPER(TemplateGallery_MST.sCategoryName) = '" + CategoryName.ToUpper().Replace("'", "''") + "' Order By sTemplateName ";

            oDB.Retrive_Query(strSQL, out  dt);
            oDB.Disconnect();
            oDB.Dispose();
            return dt;
        }

        public DataTable GetAllTemplates()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            string strSQL = "";
            DataTable dt = null;
            strSQL = " SELECT  TemplateGallery_MST.nTemplateID, ISNULL(TemplateGallery_MST.sTemplateName, '') AS sTemplateName, " +
                         "  TemplateGallery_MST.nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1)  " +
                         " + ISNULL(Provider_MST.sLastName, '') AS sProviderName, TemplateGallery_MST.sCategoryName as CategoryName " +
                         " FROM  TemplateGallery_MST WITH (NOLOCK) LEFT OUTER JOIN " +
                         " Provider_MST WITH (NOLOCK) ON TemplateGallery_MST.nProviderID = Provider_MST.nProviderID ";


            oDB.Retrive_Query(strSQL, out  dt);
            oDB.Disconnect();
            oDB.Dispose();
            return dt;
        }

        public DataTable GetSingleTemplate(Int64 TemplateID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            string strSQL = "";
            DataTable dt = null;
            //strSQL = " SELECT isnull(nTemplateID,0) as nTemplatID, ISNULL(sTemplateName,'') as sTemplateName, " +
            //         " ISNULL(nCategoryID,0) as nCategoryID, ISNULL(nProviderID,0) as nProviderID,  ISNULL(sDescription,null) as sDescription " +
            //         " FROM TemplateGallery_MST" +
            //         " WHERE nTemplateID = " + TemplateID + " ";

            strSQL = " SELECT isnull(nTemplateID,0) as nTemplatID, ISNULL(sTemplateName,'') as sTemplateName, " +
                     " sCategoryName AS CategoryName, ISNULL(nProviderID,0) as nProviderID,  ISNULL(sDescription,null) as sDescription ,ISNULL(sBibliographicinfo,'') AS sBibliographicinfo, ISNULL(sBibliographicDeveloper,'') AS sBibliographicDeveloper  " +
                     " FROM TemplateGallery_MST WITH (NOLOCK) " +
                     " WHERE nTemplateID = " + TemplateID + " ";


            oDB.Retrive_Query(strSQL, out  dt);
            oDB.Disconnect();
            oDB.Dispose();
            return dt;
        }

        public bool DeleteTemplate(Int64 TemplateID)
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                string strSQL = "";
             //   DataTable dt = null;
                strSQL = "DELETE FROM TemplateGallery_MST WHERE nTemplateID = " + TemplateID;
                oDB.Execute_Query(strSQL);
                oDB.Disconnect();
                oDB.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public DataTable GetTemplateDetails(Int64 nTemplateID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dtTemplateDetails = null;
            try
            {
                oDB.Connect(false);
                string strSQL = "";
                strSQL = "SELECT ISNULL(sTemplateName,'') AS sTemplateName, ISNULL(nCategoryID,0) AS nCategoryID, ISNULL(sCategoryName,'') AS sCategoryName, nProviderID FROM TemplateGallery_MST WITH (NOLOCK) WHERE nTemplateID = " + TemplateID;
                oDB.Retrive_Query(strSQL, out dtTemplateDetails);
                oDB.Disconnect();
                oDB.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dtTemplateDetails;
        }


        public DataTable GetTemplateDetails_FollowUp(Int64 nTemplateID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dtTemplateDetails = null;
            try
            {
                oDB.Connect(false);
                string strSQL = "";
                strSQL = "SELECT ISNULL(sTemplateName,'') AS sTemplateName, ISNULL(nCategoryID,0) AS nCategoryID, ISNULL(sCategoryName,'') AS sCategoryName, nProviderID FROM TemplateGallery_MST WITH (NOLOCK) WHERE nTemplateID = " + nTemplateID;
                oDB.Retrive_Query(strSQL, out dtTemplateDetails);
                oDB.Disconnect();
                oDB.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dtTemplateDetails;
        }

        public Byte[] ConvertFileToBinary(string FilePath)
        {
            System.IO.FileStream oFileStream = null;
            BinaryReader oBinaryRead = null;
            Byte[] byteRead = null;
            try
            {
                if (System.IO.File.Exists(FilePath))
                {
                    //oFileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous);
                    oFileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    oBinaryRead = new BinaryReader(oFileStream);
                    byteRead = oBinaryRead.ReadBytes(Convert.ToInt32(oFileStream.Length));

                    //-------------------------------------------
                    //Changes made on 20080719 - By Sagar Ghodke 
                    //To release the File
                    if (oFileStream != null)
                    {
                        oFileStream.Close();
                        oFileStream.Dispose();
                        oFileStream = null;
                    }

                    if (oBinaryRead != null)
                    {
                        oBinaryRead.Close();
                        oBinaryRead.Dispose();
                        oBinaryRead = null;
                    }
                    //end Changes - 20080719
                    //-------------------------------------------

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return byteRead;
        }

        public bool ConvertBinaryToFile(object StreamData, string FilePath)
        {
            bool _result = false;
            //  string _FilePath = FilePath;
            try
            {
                if (StreamData != null && StreamData != DBNull.Value)
                {
                    Byte[] byteRead = (byte[])StreamData;
                  //  MemoryStream oDataStream = new MemoryStream(byteRead);
                    FileStream oFile = new FileStream(FilePath, FileMode.Create);
                    oFile.Write(byteRead, 0, byteRead.Length);
                    // oDataStream.WriteTo(oFile);
                    //oDataStream.Close();
                    //oDataStream.Dispose();
                    //oDataStream = null;
                    oFile.Close();
                    oFile.Dispose();
                    oFile = null;
                    // oFile.Dispose();
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _result;
        }

        //public System.Data.DataTable GetList(String CategoryType)
        //{
        //    System.Data.DataTable _result = new System.Data.DataTable();

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    oDB.Connect(false);
        //    try
        //    {
        //        //string _sqlQuery = "SELECT nCategoryID, sDescription, sCategoryType FROM Category_MST ";
        //        //
        //        string _sqlQuery = "SELECT nCategoryID, sDescription, sCategoryType FROM Category_MST WHERE sCategoryType ='" + CategoryType + "' AND bIsBlocked = '" + false + "' AND nClinicID = " + this.ClinicID + " ";
        //        //
        //        oDB.Retrive_Query(_sqlQuery, out _result);
        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        ex.ERROR_Log(ex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //    return _result;
        //}

        public System.Data.DataTable GetTemplateCategoryList()
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            oDB.Connect(false);
            try
            {
                //Bug No: 5189
                //string _sqlQuery = "SELECT sDescription AS CategoryName FROM Category_MST WHERE sCategoryType='Template' UNION SELECT sCategoryName AS CategoryName FROM TemplateGallery_MST";
                //string _sqlQuery = "SELECT isnull(sDescription,'') AS CategoryName FROM Category_MST WHERE sCategoryType='Template' UNION SELECT isnull(sCategoryName,'') AS CategoryName FROM TemplateGallery_MST";
                //string _sqlQuery = "SELECT ISNULL(sDescription,'') AS CategoryName, nCategoryID AS CategoryID FROM Category_MST WHERE sCategoryType = 'Template' " +
                //        " UNION SELECT ISNULL(sCategoryName,'') AS CategoryName, nCategoryID AS CategoryID FROM TemplateGallery_MST " +
                //        " WHERE ISNULL(sCategoryName,'') NOT IN (SELECT ISNULL(sDescription,'') FROM Category_MST WHERE sCategoryType = 'Template')";

                string _sqlQuery = "SELECT ISNULL(sDescription,'') AS CategoryName, nCategoryID AS CategoryID FROM Category_MST WITH (NOLOCK) WHERE sCategoryType = 'Template' " +
                        " UNION SELECT ISNULL(sCategoryName,'') AS CategoryName, nCategoryID AS CategoryID FROM TemplateGallery_MST WITH (NOLOCK) " +
                        " WHERE ISNULL(sCategoryName,'') NOT IN (SELECT ISNULL(sDescription,'') FROM Category_MST WITH (NOLOCK) WHERE sCategoryType = 'Template') AND ISNULL(sCategoryName,'') <> '' ";
                
                
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        //Code Added by Mayuri:20091208
        //To retrive BatchNames and display those in treeview control
        public System.Data.DataTable GetBatchNames()
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            oDB.Connect(false);
            try
            {
                //string _sqlQuery = "SELECT sDescription AS CategoryName FROM Category_MST WHERE sCategoryType='Template' UNION SELECT sCategoryName AS CategoryName FROM TemplateGallery_MST";
                //string _sqlQuery = "select sBatchName,nBatchPateintStatMSTID from BL_Batch_PatientStatement_Mst order by sBatchName";
                string _sqlQuery = "select ISNULL(sBatchName,'') as sBatchName,ISNULL(nBatchPateintStatMSTID,0) as nBatchPateintStatMSTID,ISNULL(dtCreateDate,0) as dtCreateDate,isnull(BL_Batch_PatientStatement_Mst.bIsVoid,0) As bIsVoid from BL_Batch_PatientStatement_Mst WITH (NOLOCK) order by dtCreateDate desc";
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        //End code Added by Mayuri:20091208
        // Sudhir - 20090123
        public bool IsTemplateNamePresent(String TemplateName, String CategoryName, Int64 nProviderID)
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                Object _result;
                oDB.Connect(false);
                String strQuery = "";
                TemplateName = TemplateName.Replace("'", "''");
                strQuery = "SELECT COUNT(*) FROM TemplateGallery_MST WITH (NOLOCK) WHERE sTemplateName='" + TemplateName.Replace("'", "''") +
                    "' AND sCategoryName = '" + CategoryName.Replace("'", "''") + "' AND nTemplateID <> " + _TemplateID + " AND nProviderID = " + nProviderID + "";
                _result = oDB.ExecuteScalar_Query(strQuery);
                oDB.Disconnect();
                oDB.Dispose();
                if (Convert.ToInt32(_result) > 0)
                { return true; }
                else
                { return false; }
            }
            catch (Exception)// Ex)
            {
                //Ex.ToString();
                //Ex = null;
                return true;
            }

        }

        //__ ___ ____ _____ 
        # region " Patient Templates "

        public Int64 SavePatientTemplate(Int64 TransactionID)
        {
            //@nTransactionID	NUMERIC(18,0), 
            //@nPatientID	NUMERIC(18,0),  
            //@nFromDate	NUMERIC(18,0),  
            //@nToDate	NUMERIC(18,0), 
            //@nTemplateID	NUMERIC(18,0), 
            //@sTemplateName	VARCHAR(50),  
            //@nProviderID	NUMERIC(18,0), 
            //@iTemplate	IMAGE,  
            //@nClinicID	NUMERIC(18,0)

            //gsp_INUP_PatientTemplate

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            oDBParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nFromDate", _FromDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nToDate", _ToDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nTemplateID", _TemplateID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@sTemplateName", _TemplateName, ParameterDirection.Input, SqlDbType.VarChar, 50);
            oDBParameters.Add("@nCategoryID", _CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@sCategoryName", _CategoryName, ParameterDirection.Input, SqlDbType.VarChar, 50);
            oDBParameters.Add("@nProviderID", _ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(_PatientID), ParameterDirection.Input, SqlDbType.BigInt);

            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(_TemplateFilePath);
            ogloTemplate.Dispose();
            ogloTemplate = null;
            //Byte[] oTemplate = gloOffice.Supporting.ConvertFileToBinary(TemplateFilePath);
            oDBParameters.Add("@iTemplate", oTemplate, ParameterDirection.Input, SqlDbType.Image);

            //code added by SaiKrishna 04-21-2011
            oDBParameters.Add("nPAccountID", _nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);


            Object oResult;
            oDB.Execute("PA_sp_INUP_PatientTemplate", oDBParameters, out oResult);
            oDB.Disconnect();
            oDB.Dispose();
            //    [gsp_InUpTemplateGallery_MST]
            //    @TemplateID numeric(18,0),
            //    @TemplateName varchar(50),
            //    @CategoryID numeric(18,0),
            //    @ProviderID numeric(18,0),
            //    @Description image,
            //    @MachineID Numeric(18,0)=0
            if (oResult != null && oResult.ToString() != "")
            {
                return Convert.ToInt64(oResult);
            }
            else
            {
                return 0;
            }

        }

        public Int64 SavePatientTemplate(Int64 TransactionID, gloOffice.gloTemplate template)
        {
            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);

            Int64 FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
            Int64 ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);

            if ((Convert.ToInt64(template.CategoryID) == 0) || (Convert.ToString(template.CategoryName) == ""))
            {
                DataTable dtTemplateDetails = null;
                dtTemplateDetails = ogloTemplate.GetTemplateDetails_FollowUp(template.TemplateID);
                if (dtTemplateDetails != null && dtTemplateDetails.Rows.Count > 0)
                {
                    template.TemplateName = dtTemplateDetails.Rows[0]["sTemplateName"].ToString();
                    template.CategoryID = Convert.ToInt64(dtTemplateDetails.Rows[0]["nCategoryID"]);
                    template.CategoryName = dtTemplateDetails.Rows[0]["sCategoryName"].ToString();
                }
                if (dtTemplateDetails != null)
                {
                    dtTemplateDetails.Dispose();
                    dtTemplateDetails = null;
                }
            }
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.InputOutput, SqlDbType.BigInt);
            oDBParameters.Add("@nPatientID", template.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nFromDate", FromDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nToDate", ToDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nTemplateID", template.TemplateID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@sTemplateName", template.TemplateName, ParameterDirection.Input, SqlDbType.VarChar, 50);
            oDBParameters.Add("@nCategoryID", template.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@sCategoryName", template.CategoryName, ParameterDirection.Input, SqlDbType.VarChar, 50);
            oDBParameters.Add("@nProviderID", _ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nClinicID", template.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(template.PatientID), ParameterDirection.Input, SqlDbType.BigInt);

            //gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(template.TemplateFilePath);
            ogloTemplate.Dispose();
            ogloTemplate = null;
            //Byte[] oTemplate = gloOffice.Supporting.ConvertFileToBinary(TemplateFilePath);
            oDBParameters.Add("@iTemplate", oTemplate, ParameterDirection.Input, SqlDbType.Image);

            //code added by SaiKrishna 04-21-2011
            oDBParameters.Add("nPAccountID", template.nPAccountID, ParameterDirection.Input, SqlDbType.BigInt);


            Object oResult;
            oDB.Execute("PA_sp_INUP_PatientTemplate", oDBParameters, out oResult);
            oDB.Disconnect();
            oDB.Dispose();
            //    [gsp_InUpTemplateGallery_MST]
            //    @TemplateID numeric(18,0),
            //    @TemplateName varchar(50),
            //    @CategoryID numeric(18,0),
            //    @ProviderID numeric(18,0),
            //    @Description image,
            //    @MachineID Numeric(18,0)=0
            if (oResult != null && oResult.ToString() != "")
            {
                return Convert.ToInt64(oResult);
            }
            else
            {
                return 0;
            }

        }

        public DataTable GetPatientTemplate(Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            string strSQL = "";
            DataTable dt = null;
            strSQL = " SELECT isnull(nPatientID,0) as nPatientID, isnull(nCategoryID,0) AS nCategoryID , ISNULL(sCategoryName,'') as sCategoryName , isnull(nTemplateID,0) AS nTemplateID , ISNULL(sTemplateName,'') as sTemplateName , nFromDate, nToDate, ISNULL(nProviderID,0) AS nProviderID, ISNULL(iTemplate,null) AS iTemplate, ISNULL(nCount,1) AS nCount, ISNULL(nClinicID,0) AS nClinicID " +
                     " FROM PatientTemplates WITH (NOLOCK) " +
                     " WHERE nTransactionID = " + TransactionID + " ";
            oDB.Retrive_Query(strSQL, out  dt);
            oDB.Disconnect();
            oDB.Dispose();
            return dt;
        }

        # endregion " Patient Templates"

        public DataTable GetAssociation(AssociationCategories associationCategory)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable _dtAssociation = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT DISTINCT ISNULL(TemplateGallery_Association.nTemplateCategoryID, 0) AS nTemplateCategoryID, ISNULL(TemplateGallery_Association.nTemplateID, 0) "
                + " AS nTemplateID, ISNULL(TemplateGallery_Association.nProviderID, 0) AS nProviderID,  "
                + " ISNULL(TemplateGallery_Association.sTemplateCategoryName, '') AS sDescription,  "
                + " ISNULL(TemplateGallery_MST.sTemplateName,'') AS sTemplateName, ISNULL(TemplateGallery_Association.bIsDefault,0) AS bIsDefault "
                + " FROM         TemplateGallery_Association WITH (NOLOCK) INNER JOIN "
                + " TemplateGallery_MST WITH (NOLOCK) ON TemplateGallery_Association.nTemplateID = TemplateGallery_MST.nTemplateID "
                + " WHERE (TemplateGallery_Association.nClinicID = " + this.ClinicID + ") "
                + " AND (TemplateGallery_Association.nAssociatedCategoryID = " + associationCategory.GetHashCode() + ") Order by sDescription,sTemplateName";

                oDB.Retrive_Query(_sqlQuery, out _dtAssociation);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.Message, ShowMessageBox: true); ex = null; }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
               // if (_dtAssociation != null) { _dtAssociation.Dispose(); }
            }

            return _dtAssociation;

        }
        
    }

    /// <summary>
    ///  Colllection of the Templates
    /// </summary>
    public class Templates : IDisposable
    {
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Distructor"

        public Templates()
        {
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
        }

        private Int64 _ClinicID;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~Templates()
        {
            Dispose(false);
        }

        #endregion

    }

    /// <summary>
    ///  To have different Properties of Template
    /// </summary>
    public class Template : IDisposable
    {
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Distructor"

        public Template()
        {

        }

        private Int64 _ClinicID;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~Template()
        {
            Dispose(false);
        }

        #endregion


    }

    /// <summary>
    ///  To have different Properties of Template
    /// </summary>
    public static class Supporting
    {
        public static Wd.Application gblTempApp;
        public static Wd.Application _gblWordApplication;
        public static Wd.Application WdApplication;

        private static String _databaseconnectionString = "";
        public static String DataBaseConnectionString
        {
            get { return _databaseconnectionString; }
            set { _databaseconnectionString = value; }
        }

        private static String _NewDocumentName = "";

        private static Int64 _PatientID = 0;
        public static Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
        // VisitID property procedure added for pulling history dataDictionary
        private static Int64 _nVisitID = 0;
        public static Int64 VisitID
        {
            get { return _nVisitID; }
            set { _nVisitID = value; }
        }

        private static Int64 _CategoryID = 0;
        public static Int64 CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }


        private static Int64 _AppointmentID = 0;
        public static Int64 AppointmentID
        {
            get { return _AppointmentID; }
            set { _AppointmentID = value; }
        }

        private static Int64 _PrimaryID = 0;
        public static Int64 PrimaryID
        {
            get { return _PrimaryID; }
            set { _PrimaryID = value; }
        }

        private static Int64 _DocumentCategory = 0;
        public static Int64 DocumentCategory
        {
            get { return _DocumentCategory; }
            set { _DocumentCategory = value; }
        }

        private static Wd.Document _oCurDoc;
        public static Wd.Document CurrentDocument
        {
            get { return _oCurDoc; }
            set { _oCurDoc = value; }
        }

        private static Int64 _ProviderID = 0;
        public static Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }

        private static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private static Int64 _ClinicID = 1;
        public static Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        private static Int64 _FieldID1 = 0;
        public static Int64 FieldID1
        {
            get { return _FieldID1; }
            set { _FieldID1 = value; }
        }

        private static Int64 _FieldID2 = 0;
        public static Int64 FieldID2
        {
            get { return _FieldID2; }
            set { _FieldID2 = value; }
        }

        private static Int64 _FieldID3 = 0;
        public static Int64 FieldID3
        {
            get { return _FieldID3; }
            set { _FieldID3 = value; }
        }
        
        /// <summary>
        /// This can be use as the Date of Service
        /// </summary>
        private static Int64 _FromDate = 0;
        public static Int64 FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        private static Int64 _ToDate = 0;
        public static Int64 ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private static Boolean _isFromBatchPrint = false;

        public static Boolean isFromBatchPrint
        {
            get { return _isFromBatchPrint; }
            set { _isFromBatchPrint = value; }
        }
        private static DataTable dtFlowSheet;

        //public static void Print(gloOffice.gloTemplate template, bool IsFromAppointmentTab, Int64 AccountID = 0, string databasestring = "")
        //{
        //    Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
        //    wordApplication = new Microsoft.Office.Interop.Word.Application();
        //    object missing_new = Type.Missing;
        //    object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
        //    object templatename = gloSettings.FolderSettings.AppTempFolderPath;
        // //   gloOffice.gloTemplate _gloTemplate = null;
        //    //foreach (gloOffice.gloTemplate template in gloTemplates)
        //    //{
        //        try
        //        {
        //            gloOffice.Supporting.DataBaseConnectionString = databasestring;
        //            gloOffice.Supporting.PatientID = template.PatientID;

        //            gloOffice.Supporting.PrimaryID = template.TemplateID;
        //            AccountID = template.nPAccountID;

        //            gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
        //            gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

        //            //Create New Document in Word
        //            object missing = System.Reflection.Missing.Value;
        //            //object fileName = gloOffice.Supporting.GenerateDocumentFile();
        //            String strFileName = gloOffice.Supporting.NewDocumentName();
        //            try
        //            {
        //                System.IO.File.Copy(template.TemplateFilePath, strFileName.ToString());
        //            }
        //            catch (Exception ex)
        //            {
        //                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

        //            }
        //           object fileName = strFileName;

        //            object newTemplate = false;
        //            object docType = 0;
        //            object isVisible = true;

        //            // Create a new Document, by calling the Add function in the Documents collection
        //            Microsoft.Office.Interop.Word.Document aDoc = wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);

        //            gloOffice.Supporting.PrimaryID = template.PrimeryID;
        //            gloOffice.Supporting.WdApplication = wordApplication;
        //            gloOffice.Supporting.CurrentDocument = aDoc;

        //            System.Windows.Forms.Application.DoEvents();
        //            gloOffice.Supporting.isFromBatchPrint = true;
        //            gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
        //            gloOffice.Supporting.isFromBatchPrint = false;
        //            //gloWord.gloWord.CurrentDoc = aDoc;
        //            //gloWord.gloWord.CleanupDocument();
        //            //aDoc = gloWord.gloWord.CurrentDoc;
        //            gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);  
        //            // need to see the created document, so make it visible
        //            //wordApplication.Visible = true;
        //            //aDoc.Activate();
        //            //object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
        //            //object oFileName = (object)template.TemplateFilePath;
        //            //aDoc.SaveAs(oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
        //            DocumentPrintOut(ref aDoc);
        //            try
        //            {
        //                aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);

        //                if (aDoc != null)
        //                {
        //                    System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);
        //                    aDoc = null;
        //                }
        //            }
        //            catch
        //            {
        //            }
        //            try
        //            {
        //                if (System.IO.File.Exists(strFileName.ToString()))
        //                {
        //                    System.IO.File.Delete(strFileName.ToString());
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
        //            }
        //        }
        //        catch (System.Runtime.InteropServices.COMException ex)
        //        {
        //            System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
        //        }
        //        finally
        //        {
        //            gloOffice.Supporting.isFromBatchPrint = false;
        //        }
        //    //}
        //    wordApplication.Application.Quit(ref saveOptions, ref missing_new, ref missing_new);

        //    try
        //    {
        //        if (wordApplication != null)
        //        {
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApplication);
        //            wordApplication = null;
        //        }
        //    }
        //    catch
        //    {
        //    }
 
        //    GC.Collect(); // force final cleanup!
        //    GC.WaitForPendingFinalizers();

        //    //if (isTemplateSkipped)
        //    //{
        //    //    foreach (PatientMessage oMsg in oPatientMessages)
        //    //    {
        //    //        strExcludedTemplates.Append(System.Environment.NewLine);
        //    //        strExcludedTemplates.Append(oMsg.Message);
        //    //    }
        //    //    frmgloMessageBox oMsgForm = new frmgloMessageBox();
        //    //    oMsgForm.Text = _messageBoxCaption;
        //    //   oMsgForm.Setmessage(strExcludedTemplates);
        //    //   oMsgForm.ShowDialog(ParentForm); 

        //    //    // MessageBox.Show(strExcludedTemplates.ToString(),_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //}
        //}
        public static void Print(ref gloWord.LoadAndCloseWord myLoadWord, gloOffice.gloTemplate template, bool IsFromAppointmentTab, Int64 AccountID = 0, string databasestring = "")
        {
          //  Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            bool toQuit = false;
            if (myLoadWord == null)
            {
                myLoadWord = new gloWord.LoadAndCloseWord();
                toQuit = true;
            }
            object missing_new = Type.Missing;
            object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            object templatename = gloSettings.FolderSettings.AppTempFolderPath;
            //   gloOffice.gloTemplate _gloTemplate = null;
            //foreach (gloOffice.gloTemplate template in gloTemplates)
            //{
            try
            {
                gloOffice.Supporting.DataBaseConnectionString = databasestring;
                gloOffice.Supporting.PatientID = template.PatientID;

                gloOffice.Supporting.PrimaryID = template.TemplateID;
                AccountID = template.nPAccountID;

                gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"));

                //Create New Document in Word
                object missing = System.Reflection.Missing.Value;
                //object fileName = gloOffice.Supporting.GenerateDocumentFile();
                String strFileName = gloOffice.Supporting.NewDocumentName();
                try
                {
                    System.IO.File.Copy(template.TemplateFilePath, strFileName.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                }
                object fileName = strFileName;

                object newTemplate = false;
                object docType = 0;
                object isVisible = true;

                // Create a new Document, by calling the Add function in the Documents collection
                Microsoft.Office.Interop.Word.Document aDoc = myLoadWord.LoadWordApplication(strFileName); // wordApplication.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);

                gloOffice.Supporting.PrimaryID = template.PrimeryID;
                gloOffice.Supporting.WdApplication = aDoc.Application;
                gloOffice.Supporting.CurrentDocument = aDoc;

                System.Windows.Forms.Application.DoEvents();
                gloOffice.Supporting.isFromBatchPrint = true;
                gloOffice.Supporting.GetFormFieldDataRevised(ref aDoc, null, AccountID);
                gloOffice.Supporting.isFromBatchPrint = false;
                //gloWord.gloWord.CurrentDoc = aDoc;
                //gloWord.gloWord.CleanupDocument();
                //aDoc = gloWord.gloWord.CurrentDoc;
                gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);  
                // need to see the created document, so make it visible
                //wordApplication.Visible = true;
                //aDoc.Activate();
                //object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                //object oFileName = (object)template.TemplateFilePath;
                //aDoc.SaveAs(oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Sent Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                DocumentPrintOut(ref aDoc);
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Finished Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Closed Word Called for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", aDoc.FullName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                myLoadWord.CloseWordOnly(ref aDoc);
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Closed Word Finished for file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
             //   GC.Collect();
             //   GC.WaitForPendingFinalizers();
                
                try
                {
                    if (System.IO.File.Exists(strFileName.ToString()))
                    {
                        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleting Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                        System.IO.File.Delete(strFileName.ToString());
                        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("Deleted Word file to Printout {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.PrintLog(String.Format("File not found for Delete {0} : UserID {1} : UserName {2} : LoginProviderID {3} ", strFileName, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                    ex = null;
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.PrintLog(strException: ex.ToString(), ShowMessageBox: false);
                ex = null;
            }
            finally
            {
                gloOffice.Supporting.isFromBatchPrint = false;
            }
            //}
            if (toQuit)
            {
                myLoadWord.CloseApplicationOnly();
                myLoadWord = null;
                //GC.Collect(); // force final cleanup!
                //GC.WaitForPendingFinalizers();
            }
           

            //if (isTemplateSkipped)
            //{
            //    foreach (PatientMessage oMsg in oPatientMessages)
            //    {
            //        strExcludedTemplates.Append(System.Environment.NewLine);
            //        strExcludedTemplates.Append(oMsg.Message);
            //    }
            //    frmgloMessageBox oMsgForm = new frmgloMessageBox();
            //    oMsgForm.Text = _messageBoxCaption;
            //   oMsgForm.Setmessage(strExcludedTemplates);
            //   oMsgForm.ShowDialog(ParentForm); 

            //    // MessageBox.Show(strExcludedTemplates.ToString(),_messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        private static void DocumentPrintOut(ref Wd.Document CurrentDocument)
        {

            object Background = true;
            object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
            object Copies = 1;
            object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
            object PrintToFile = false;
            object Collate = false;
            object ActivePrinterMacGX = Type.Missing;
            object ManualDuplexPrint = false;
            object PrintZoomColumn = 1;
            object PrintZoomRow = 1;
            object missing = Type.Missing;

            //CurrentDocument.Application.Options.PrintBackground = true;
            //CurrentDocument.PrintOut(ref Background, ref missing, ref missing, ref missing,
            //    ref missing, ref missing, ref missing, ref Copies,
            //    ref missing, ref missing, ref PrintToFile, ref Collate,
            //    ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
            //    ref PrintZoomRow, ref missing, ref missing);

            //System.Threading.Thread.Sleep(1000);
            gloWord.LoadAndCloseWord.PrintDocument(ref CurrentDocument, ref Background, ref missing, ref missing, ref missing,
                     ref missing, ref missing, ref missing, ref Copies,
                     ref missing, ref missing, ref PrintToFile, ref Collate,
                     ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                     ref PrintZoomRow, ref missing, ref missing);
        }

        private static string GetLoginUserName(string sLoginName)
        {
            string sResult = sLoginName;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(Convert.ToString(appSettings["DatabaseConnectionString"]));
                gloDatabaseLayer.DBParameters oPara = new gloDatabaseLayer.DBParameters();
                DataTable dtUser = null;

                oDB.Connect(false);
                oPara.Add("@LoginName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_GetLoginProviderDetails", oPara, out dtUser);
                oDB.Disconnect();
                oDB.Dispose();

                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    if (dtUser.Rows[0]["ProviderName"].ToString().Trim() != "")
                        sResult = dtUser.Rows[0]["ProviderName"].ToString().Trim();
                    else if (dtUser.Rows[0]["UserName"].ToString().Trim() != "")
                        sResult = dtUser.Rows[0]["UserName"].ToString().Trim();
                    else
                        sResult = sLoginName;
                }
                if (dtUser != null)
                {
                    dtUser.Dispose();
                    dtUser = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return sResult;
        }

        public static String NewDocumentName()
        {

            //String _Path = Application.StartupPath + "\\" + "Temp";
            String _Path = gloSettings.FolderSettings.AppTempFolderPath;
           // String strExtention = ".docx";
           // DateTime _dtCurrentDateTime = DateTime.Now;
           // String _tPath = "";

           // Int16 i = 0;
            if (Directory.Exists(_Path) == false)
            {
                Directory.CreateDirectory(_Path);
            }
            return gloGlobal.clsFileExtensions.NewDocumentName(_Path, ".docx", "MMddyyyyHHmmssffff");
            //_NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt") + " " + _dtCurrentDateTime.Millisecond.ToString() + strExtention;
            ////_Path = _Path + "\\" + _NewDocumentName;   // COMMENTED BY SUDHIR 20090121 //
            //_tPath = _Path + _NewDocumentName;  // _tPath USED FOR VALIDATION. //
            //while ( (File.Exists(_tPath) == true) && ( i < Int16.MaxValue) )
            //{
            //    i += 1;
            //    _NewDocumentName = _dtCurrentDateTime.ToString("MM dd yyyy - hh mm ss tt") + " " + _dtCurrentDateTime.Millisecond.ToString() + "_" + i.ToString() + strExtention;
            //    _tPath = _Path + _NewDocumentName;  // CHANGED THIS LINE, AS IT WAS PRODUCING WRONG FILE PATH. //
            //}
            //return _tPath;

        }
        //static bool firstTime = true;
        //static Stopwatch myWatch = null;
        //static DateTime myTime ;
        //static string getUniqueID()
        //{
            
        //    if ( (firstTime == true) || (myWatch ==null) )
        //    {
        //        firstTime = false;
        //        myTime = System.DateTime.Now;
        //        myWatch = new Stopwatch();
        //        myWatch.Start();
        //    }
        //    TimeSpan TmSp = new TimeSpan(myTime.Ticks + myWatch.ElapsedTicks);
        //    return TmSp.Ticks.ToString();
        //}
        //private static Stopwatch myWatch = null;
        //private static bool firstTime = true;
        //private static DateTime myTime = DateTime.Now;
        public static string getUniqueID()
        {

            //string _returnUnqueID = "";
            //if (myWatch == null)
            //{
            //    myWatch = new Stopwatch();
            //    firstTime = true;
            //}

            //if (firstTime == true)
            //{
            //    firstTime = false;
            //    myTime = DateTime.Now;
            //    myWatch.Start();
            //}
            //TimeSpan TmSp = new TimeSpan(myTime.Ticks + myWatch.ElapsedTicks);
            //_returnUnqueID = TmSp.Ticks.ToString();
            //return _returnUnqueID;
            return gloGlobal.clsFileExtensions.GetUniqueDateString();
        }

        public static string GetDataFrom_DB(string strField, string HelpText)
        {
            if (strField.Contains(".") == false)
            {
                return "";
            }
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            //String gloPMTempFolderPath = Application.StartupPath + "\\Temp\\";
            String gloPMTempFolderPath = gloSettings.FolderSettings.AppTempFolderPath;
            

            string strData = "";
            Int64 ResultDataType = 0;
            string strDataCol = "";
            Int32 filecnt = 0;
            long localPrimaryID = 0;
            localPrimaryID = _PrimaryID;
            try
            {
                //@sFields varchar(500),    
                //@nPatientID NUMERIC(18,0),    
                //@nPrimaryID Numeric(18,0), --This can be nExamId or ReferralsId or TestId or UserId    
                //@nAppintmentID Numeric(18,0),    
                //@nVisitID
                //@DocumentCategory int,    
                //@nFieldId1 numeric(18,0)=0,    
                //@nFieldId2 numeric(18,0)=0,    
                //@nFieldId3

                // FLOWSHEET SINGLE ROW //
                string strTempField = "";
                if (strField.Contains("|SingleRow") == true)
                { strTempField = strField.Replace("|SingleRow", ""); }
                else
                { strTempField = strField; }

                // FAX //
                if (strField.StartsWith("FAX"))
                {
                    strDataCol = strData + "|" + ResultDataType.ToString();
                    return strDataCol;
                }
                // END FAX //


                // SUDHIR 20090806 // FETCH CLOSEST POSSIBLE APPOINTMENT ID //
                //if (_PrimaryID == 0 && (strField.EndsWith("CurrentAppointment") || strField.StartsWith("AS_Appointment_DTL")))
                //{
                if (gloOffice.Supporting._isFromBatchPrint == true)
                {

                }
                else if ((strField.EndsWith("CurrentAppointment") || strField.StartsWith("AS_Appointment_DTL")))
                {
                    if (ToDate == 0)
                    {
                        ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                    }
                    //string _Query = " SELECT TOP 1 nMSTAppointmentID, dtStartDate FROM AS_Appointment_MST WITH (NOLOCK) " +
                    //    " WHERE dtStartDate <= " + ToDate + " AND nPatientID = " + _PatientID + " ORDER BY dtStartDate DESC";
                    string _Query = "";

                    // Problem 27 : Appointment date liquid link is no longer pulling 
                    // if the patient has no deleted appointments on the same day
                    _Query = "SELECT TOP 1 nDTLAppointmentID, AS_Appointment_MST.dtStartDate FROM AS_Appointment_MST INNER JOIN Patient ON AS_Appointment_MST.nPatientID = Patient.nPatientID INNER JOIN AS_Appointment_DTL ON  AS_Appointment_MST.nMSTAppointmentID =AS_Appointment_DTL.nMSTAppointmentID  WHERE AS_Appointment_DTL.dtStartDate = " + ToDate + " AND AS_Appointment_MST.nPatientID = " + _PatientID + "  AND AS_Appointment_DTL.nRefFlag =0 AND AS_Appointment_DTL.nUsedStatus not in (6,7) ORDER BY AS_Appointment_DTL.dtStartDate ,AS_Appointment_DTL.dtStartTime";
                    object oResult = null;
                    oDB.Connect(false);
                    oResult = oDB.ExecuteScalar_Query(_Query);
                    if (oResult != null && oResult.ToString() != "")
                    {
                        //_PrimaryID = Convert.ToInt64(oResult);
                        localPrimaryID = Convert.ToInt64(oResult);
                    }
                    oDB.Disconnect();
                }
                //Code Comented by dipak 20100527
                //oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionString);
                //oDBParameters.Add("@sFields", strTempField, ParameterDirection.Input, SqlDbType.VarChar, 500);
                //oDBParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@nPrimaryID", _PrimaryID, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@nAppintmentID", _AppointmentID, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@nVisitID", 0, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@nProviderID", _ProviderID, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@DocumentCategory", _DocumentCategory, ParameterDirection.Input, SqlDbType.VarChar, 500);
                //oDBParameters.Add("@nFromDate", _FromDate, ParameterDirection.Input, SqlDbType.Int);
                //oDBParameters.Add("@nToDate", _ToDate, ParameterDirection.Input, SqlDbType.Int);
                //oDBParameters.Add("@nFieldId1", 0, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@nFieldId2", 0, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDBParameters.Add("@nFieldId3", 0, ParameterDirection.Input, SqlDbType.BigInt, 18);
                //oDB.Connect(false);
                //end code comented by dipak

                DataTable oResultTable = new DataTable();
                //oDB.Retrive("GetFormFieldsData", oDBParameters, out oResultTable);

                //code added by dipak for replacement of above commented code as it throws exception while fetch data dictionary fields 
                SqlConnection _sqlConnection = new SqlConnection(_databaseconnectionString);
                SqlCommand _sqlCommand = new SqlCommand();
                SqlParameter osqlParameter = new SqlParameter();
                SqlDataAdapter objDA = new SqlDataAdapter();
                osqlParameter.ParameterName = "@sFields";
                osqlParameter.SqlDbType = SqlDbType.VarChar;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = strTempField;
                osqlParameter.Size = 500;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nPatientID";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = _PatientID;
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "nPrimaryID";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                //osqlParameter.Value = _PrimaryID;
                osqlParameter.Value = localPrimaryID;
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nAppintmentID";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = _AppointmentID;
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nVisitID";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;

                // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                // We were sending Visit Id as zero, so history liquid links was not populating in check-in template
                // Now passing visit iD to pull history liquid links
                // osqlParameter.Value = 0;
                osqlParameter.Value = _nVisitID; 
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nProviderID";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = _ProviderID;
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nClinicID";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = _ClinicID;
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@DocumentCategory";
                osqlParameter.SqlDbType = SqlDbType.VarChar;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = _DocumentCategory;
                osqlParameter.Size = 500;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nFromDate";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = _FromDate;

                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nToDate";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = _ToDate;

                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nFieldId1";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                //  ElseIf strFields.StartsWith("pa_accounts.") Or strFields.StartsWith("PA_Accounts_Patients.") Or strFields.StartsWith("Patient_OtherContacts.") Then
                //    oParamater.Value = DocumentCriteria.FieldID1
                //End If
                if (strField.StartsWith("pa_accounts.") || strField.StartsWith("PA_Accounts_Patients.") || strField.StartsWith("pa_accounts_Billing.") || strField.StartsWith("pa_accounts_PatientLastClaimDiag."))
                {
                    osqlParameter.Value = FieldID1;
                }
                else
                {
                    osqlParameter.Value = 0;
                }
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nFieldId2";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = FieldID2;
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@nFieldId3";
                osqlParameter.SqlDbType = SqlDbType.BigInt;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = FieldID3;
                osqlParameter.Size = 18;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;

                //Bug #61724: 00000605 : When using the Liquid Link for "Time" it pulls in Eastern Time instead of the practice's local time
                osqlParameter = new System.Data.SqlClient.SqlParameter();
                osqlParameter.ParameterName = "@TodaysDate";
                osqlParameter.SqlDbType = SqlDbType.DateTime;
                osqlParameter.Direction = ParameterDirection.Input;
                osqlParameter.Value = DateTime.Now;
                _sqlCommand.Parameters.Add(osqlParameter);
                osqlParameter = null;
                //---End -- Bug #61724

                _sqlCommand.Connection = _sqlConnection;

                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "GetFormFieldsData";


                objDA.SelectCommand = _sqlCommand;
                objDA.Fill(oResultTable);


                _sqlConnection.Dispose();

                if (_sqlCommand != null)
                {
                    _sqlCommand.Parameters.Clear();
                    _sqlCommand.Dispose();
                    _sqlCommand = null;
                }
            
                osqlParameter = null;
                objDA.Dispose();

                bool myPhoto = false;

                if (oResultTable != null)
                {
                    if (oResultTable.Rows.Count > 0)
                    {
                        if (strField.Contains("Narration") | strField.Contains("LM_LabResult") | strField.Contains("imgSignature") | strField.Contains("imgClinicLogo") | strField.Contains("iPhoto"))
                        {

                            for (Int32 i = 0; i <= oResultTable.Rows.Count - 1; i++)
                            {
                                myPhoto = false;
                                // For j As Int32 = 0 To oResultTable.Columns.Count - 1 
                                if (oResultTable.Rows[i][0] != null && oResultTable.Rows[i][0] != DBNull.Value)
                                {
                                    string strFileName = null;
                                    if (Convert.ToString(oResultTable.Rows[i][1]) == "2")
                                    {
                                        filecnt = filecnt + 1;
                                        if (strField.Contains("Narration"))
                                        {
                                            strFileName = gloPMTempFolderPath + getUniqueID() + "Narration.txt";
                                        }
                                        else
                                        {
                                            strFileName = gloPMTempFolderPath + getUniqueID() + "Flowsheet" + filecnt + ".txt";
                                        }
                                    }
                                    else
                                    {
                                        if (strField == "Provider.imgSignature")
                                        {
                                            strFileName = gloPMTempFolderPath + getUniqueID() + "imgSignature.bmp";
                                        }
                                        else if (strField == "User_MST.imgSignature")
                                        {
                                            strFileName = gloPMTempFolderPath + getUniqueID() + "imgCoSignature.bmp";
                                        }
                                        else if (strField == "Patient_Cards.iCard|Driving")
                                        {
                                            strFileName = gloPMTempFolderPath + getUniqueID() + "imgDrivingLicense.bmp";
                                        }
                                        else if (strField == "Patient_Cards.iCard|Insurance")
                                        {
                                            strFileName = gloPMTempFolderPath + getUniqueID() + "imgInsuranceCard.bmp";
                                        }
                                        else
                                        {
                                            myPhoto = strField.Contains("iPhoto");
                                            strFileName = gloPMTempFolderPath + getUniqueID() + "imgPhoto.bmp";

                                        }
                                    }
                                    strData = strFileName;

                                    //'Save contents in file 
                                    if (File.Exists(strFileName))
                                    {
                                        File.Delete(strFileName);
                                    }
                                    strFileName = GenerateFile(oResultTable.Rows[i][0], strFileName, myPhoto);

                                    if (oResultTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        ResultDataType = Convert.ToInt64(oResultTable.Rows[i][1]);
                                    }
                                }
                                //Next 
                            }
                        }
                        else if (strField.Contains("iCard")) 
                        {
                            for (Int32 i = 0; i <= oResultTable.Rows.Count - 1; i++)
                            {
                                myPhoto = false;
                                string strFileName = null;
                                if ((Convert.ToString(oResultTable.Rows[i][0]) != null) && (Convert.ToString(oResultTable.Rows[i][0]) != ""))
                                {
                                    if (strField == "Patient_Cards.iCard |Driving")
                                    {
                                        strFileName = gloPMTempFolderPath + getUniqueID() + "imgDrivingLicense.bmp";
                                    }
                                    else if (strField == "Patient_Cards.iCard |Insurance")
                                    {
                                        strFileName = gloPMTempFolderPath + getUniqueID() + "imgInsuranceCard.bmp";
                                    }
                                    strData = strFileName;

                                    //'Save contents in file 
                                    if (File.Exists(strFileName))
                                    {
                                        File.Delete(strFileName);
                                    }
                                    strFileName = GenerateFile(oResultTable.Rows[i][0], strFileName, myPhoto);                                                                       
                                }
                                if ((Convert.ToString(oResultTable.Rows[i][1]) != null) && (Convert.ToString(oResultTable.Rows[i][1]) != ""))
                                {
                                    if (strField == "Patient_Cards.iCard |Driving")
                                    {
                                        strFileName = gloPMTempFolderPath + getUniqueID() + "imgDrivingLicense_Back.bmp";
                                    }
                                    else if (strField == "Patient_Cards.iCard |Insurance")
                                    {
                                        strFileName = gloPMTempFolderPath + getUniqueID() + "imgInsuranceCard_Back.bmp";
                                    }
                                    strData = strData + "~" + strFileName;

                                    //'Save contents in file 
                                    if (File.Exists(strFileName))
                                    {
                                        File.Delete(strFileName);
                                    }
                                    strFileName = GenerateFile(oResultTable.Rows[i][1], strFileName, myPhoto);
                                }
                                if (oResultTable.Rows[i][2].ToString().Trim() != "")
                                {
                                    ResultDataType = Convert.ToInt64(oResultTable.Rows[i][2]);
                                }
                            }
                        }

                        else if (strField.Contains("FlowSheet"))
                        {
                            gloDatabaseLayer.DBLayer oDBTemp = new gloDatabaseLayer.DBLayer(_databaseconnectionString);
                            string _FlowSheetName = strField.Substring(strField.IndexOf("|") + 1, (strField.Length - strField.IndexOf("|")) - 1);
                            _FlowSheetName = _FlowSheetName.Replace("|SingleRow", "");
                            string Query = "SELECT nTotalCols FROM FlowSheet1 WHERE sFlowSheetName = '" + _FlowSheetName + "' AND nPatientID = " + _PatientID + "";

                            oDBTemp.Connect(false);
                            Object objColums = oDBTemp.ExecuteScalar_Query(Query);
                            oDBTemp.Disconnect();
                            oDBTemp.Dispose();
                            oDBTemp = null;
                            Int32 nColumnCount = Convert.ToInt32(objColums);

                            //Fill DataColumns
                            dtFlowSheet = new DataTable();
                            for (int j = 0; j <= nColumnCount - 1; j++)
                            {
                                dtFlowSheet.Columns.Add(Convert.ToString(oResultTable.Rows[j]["sFieldName"]));
                            }

                            //Fill All Data to dtFlowSheet
                            //Read each value from database and store as a datarow.
                            Int32 nRow = 0;

                            if (strField.Contains("|SingleRow") == true)
                            {
                                nRow = oResultTable.Rows.Count - nColumnCount;
                            }

                            DataRow newRow;
                            while (nRow < oResultTable.Rows.Count)
                            {
                                newRow = dtFlowSheet.NewRow();
                                for (Int32 i = 0; i <= dtFlowSheet.Columns.Count - 1; i++)
                                {
                                    newRow[i] = oResultTable.Rows[nRow]["sValue"];
                                    nRow += 1;
                                }
                                dtFlowSheet.Rows.Add(newRow);
                            }
                            strData = "FlowSheet";
                            //For FlowSheet To Create Table
                            ResultDataType = 6;
                        }
                        else if (strField.Contains("dtClaimServiceLine") || strField.Contains("dtClaimCharges"))
                        {
                            //   dtFlowSheet = new DataTable();
                            dtFlowSheet = oResultTable.Copy();
                            //   strData = "dtClaimServiceLine";
                            strData = strField.Split('.')[1];

                            //For FlowSheet To Create Table
                            ResultDataType = 6;
                        }
                        else
                        {
                            for (Int32 i = 0; i <= oResultTable.Rows.Count - 1; i++)
                            {
                                // Bug #29300: Check-in Templates >> Application is showing the Time Format in Last Seen Liquid link
                                string myStringData = string.Empty;
                                object myObj = oResultTable.Rows[i][0];

                                if (myObj != null)
                                {
                                    //Problem : 00000283
                                    //Reason : For first time value loaded in strData. As strdata is not null for second time and myStringData is blank, So remainng value not append to strData.
                                    //Description : If-else for myobj to check for datatime is taken out and assign value to myStringData.
                                    if (myObj is DateTime)
                                    {
                                        DateTime dtStrData = (DateTime)myObj;

                                        if (dtStrData.TimeOfDay == default(System.TimeSpan))
                                        { myStringData = dtStrData.ToShortDateString(); }
                                        else
                                        { myStringData = dtStrData.ToString(); }
                                    }
                                    else
                                    { myStringData = myObj.ToString(); }

                                    if (strData == "")
                                    {
                                        strData = myStringData;
                                        ResultDataType = Convert.ToInt64(oResultTable.Rows[i][1]);
                                    }
                                    else
                                    {
                                        strData = strData + "\v" + myStringData;
                                    }
                                }
                            }
                        }
                    }

                    oResultTable.Dispose();
                    oResultTable = null;

                }


                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //       strData = dt.Rows[0][0].ToString() ;
                //       ResultDataType = Convert.ToInt64(dt.Rows[0][1]);
                //    }
                //                        else
                //    {
                //        string str ="11"; \\"\v"
                //        strData = strData + Char.Parse(str) + dt.Rows[0][0].ToString();

                //    }
                //}
                strDataCol = strData + "|" + ResultDataType.ToString();


            }
            catch (Exception ex)// ex)
            {
                // Sudhir - 20090121 - TO WRITE ERROR LOG //
                String _strError = "Error at FormField : " + strField + ex.ToString();
                gloAuditTrail.gloAuditTrail.PrintLog(_strError , false);
                //MessageBox.Show(HelpText + " = " + ex.ToString());
                //ex.ToString();
                _strError = string.Empty;
                ex = null;
            }
            return strDataCol;
        }

        private static string GenerateFile(object cntFromDB, string strFileName,bool myphoto)
        {
            if (cntFromDB != null)
            {
                if (cntFromDB.ToString().Trim() != "")
                {
                    byte[] content = (byte[])(cntFromDB);
                    if (myphoto == true)
                    {
                        //SLR: Commented to have form static control
                        //gloPictureBox.gloPictureBox MyPictureBoxControl = new gloPictureBox.gloPictureBox();
                        //MyPictureBoxControl.byteImage = content;
                        //System.Drawing.Image PatientPhoto = MyPictureBoxControl.copyFrame(true);
                        //PatientPhoto.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        //PatientPhoto.Dispose();
                        //PatientPhoto = null;
                        //MyPictureBoxControl.Dispose();
                        //MyPictureBoxControl = null;

                        System.Drawing.Image PatientPhoto = gloPictureBox.gloImage.GetImage(content, true); ;
                        PatientPhoto.Save(strFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        PatientPhoto.Dispose();
                        PatientPhoto = null;
                       
                        return strFileName;

                    }
                    else
                    {
                    //    MemoryStream stream = new MemoryStream(content);
                        
                        System.IO.FileStream oFile = new System.IO.FileStream(strFileName, System.IO.FileMode.Create);
                        oFile.Write(content, 0, content.Length);
                        //  stream.WriteTo(oFile);
                        oFile.Close();
                        oFile.Dispose();
                        oFile = null;
                        //stream.Close();
                        //stream.Dispose();
                        //stream = null;
                        return strFileName;
                    }
                }
                else
                {
                    return null;

                }
            }
            else
            {
                return null;

            }
        }

        //Added New Function For the case GLO2010-0007587
        public static void GetFormFieldDataRevised(ref Wd.Document currentDocument, ArrayList arrTables,Int64 _AccountID=0)
        {
            try
            {
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("GetFormFieldDataRevised BEGIN for document : {0} ; _AccountID : {1} ; PatientID : {2} : UserID {3} : UserName {4} : LoginProviderID {5}", currentDocument.FullName, _AccountID, _PatientID, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
                currentDocument.Activate();
                currentDocument.Application.ActiveDocument.FormFields.Shaded = false;
                //Make shading false 

                //  _DocumentType = DocumentType;
                string _HelpText = null;
                _HelpText = "None";//  = GetHelpText(DocumentType);

                //string strfieldsvalcol = null;
                //Collection of values 
                Array strDataCols = default(Array);
                //To split collection values with value & Flag 
                string strData = null;
                //Split data will be stored in this variable 

                if ((currentDocument == null) == true)
                {
                    return;
                    //Return Nothing 
                }

                //  Wd.FormField aField = default(Wd.FormField);
                //Form field Variable 
                Boolean isGuarantorAlreadySelected = false;
                FieldID1 = 0; 
                foreach (Wd.FormField aField in currentDocument.FormFields)
                {
                    switch (aField.Type)
                    {
                        case Wd.WdFieldType.wdFieldFormTextInput:

                            //' Compare table name with FormField status text 
                            string[] strTableName = aField.StatusText.Split('.');
                            if (!string.IsNullOrEmpty(strTableName[0]))
                            {
                                if (strTableName[0] == _HelpText || _HelpText == "None")
                                {

                                    if (aField.StatusText != "")
                                    {
                                        if (strTableName.Length >= 2)
                                        {
                                            if (strTableName[1] != "iPhoto" && strTableName[1] != "imgSignature" && strTableName[1] != "iCard|Driving" && strTableName[1] != "iCard|Insurance")
                                            {
                                                if (currentDocument.Application.ActiveDocument.ProtectionType == Wd.WdProtectionType.wdNoProtection)
                                                {
                                                    //if (gblnWordColorHighlight)
                                                    //{
                                                    //    aField.Range.HighlightColorIndex = gblnWordBackColor;
                                                    //}
                                                    //else
                                                    //{
                                                    //    aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight;
                                                    //}
                                                    aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight;
                                                }
                                            }
                                        }

                                        //strData = clsExam.getData_New(_DocumentCriteria.PatientID, Replace(aField.StatusText, "+", "+space(2)+"), _DocumentCriteria.ExamId, _DocumentCriteria.VisitId) 
                                        // SUDHIR 20090314 - PATIENT STATEMENT 
                                        if (strTableName[0] == "PatientStatement")
                                        {
                                            DataTable dtTransaction;
                                            DataTable dtLinePayment;
                                            DataTable dtLinePaymentDetails;
                                            String FileName = "";
                                            object missing = System.Reflection.Missing.Value;

                                            gloPatientStatement oPS = new gloPatientStatement(_databaseconnectionString);
                                            oPS.GetPatientStatement(_PatientID, _ClinicID, out dtTransaction, out dtLinePayment, out dtLinePaymentDetails);
                                            if (oPS != null)
                                            {
                                                oPS.Dispose();
                                                oPS = null;
                                            }
                                            if (dtTransaction != null && dtTransaction.Rows.Count > 0)
                                            {

                                                frmWd_PatientStatement oFrm = new frmWd_PatientStatement(_databaseconnectionString, dtTransaction, dtLinePayment, dtLinePaymentDetails, Convert.ToInt64(strTableName[1]));
                                                //oFrm.ShowDialog();
                                                FileName = oFrm.FileStatement;
                                                WdApplication = currentDocument.Application;
                                                Object objRange = "";
                                                Object objBool = false;
                                                Object oWdUnits = (object)Wd.WdUnits.wdCharacter;
                                                Object oCount = (object)1;
                                                currentDocument.Select();
                                                aField.Application.Selection.Select();
                                                aField.Select();
                                                aField.Result = "";
                                                //currentDocument.Application.Selection.Collapse(ref missing);
                                                //currentDocument.Application.Selection.MoveRight(ref oWdUnits, ref oCount, ref missing);
                                                currentDocument.Application.Selection.InsertFile(FileName, ref objRange, ref objBool, ref objBool, ref objBool);
                                                oFrm.Dispose();
                                                oFrm = null;
                                            }
                                            if (dtTransaction != null)
                                            {
                                                dtTransaction.Dispose();
                                                dtTransaction = null;
                                            }
                                            if (dtLinePayment != null)
                                            {
                                                dtLinePayment.Dispose();
                                                dtLinePayment = null;
                                            }
                                            if (dtLinePaymentDetails != null)
                                            {
                                                dtLinePaymentDetails.Dispose();
                                                dtLinePaymentDetails = null;
                                            }
                                        }
                                        else
                                        {






                                            if (aField.StatusText.StartsWith("User_MST"))
                                            {
                                                if (aField.StatusText == "User_MST.sLoginName")
                                                {
                                                    if (appSettings["UserName"] != "")
                                                        aField.Result = Convert.ToString(appSettings["UserName"]);
                                                }
                                                else if (aField.StatusText.StartsWith("User_MST.sFirstName"))
                                                {

                                                    aField.Result = GetLoginUserName(Convert.ToString(appSettings["UserName"]));
                                                }
                                                continue;
                                            }

                                            if (aField.StatusText.StartsWith("PA_Accounts_Patients") | aField.StatusText.StartsWith("pa_accounts") | aField.StatusText.StartsWith("pa_accounts_Billing") | aField.StatusText.StartsWith("pa_accounts_PatientLastClaimDiag"))
                                            {
                                                if (_AccountID == 0)
                                                {
                                                    if ((isGuarantorAlreadySelected == false))
                                                    {
                                                        frmSelectPatientGuarantor ofrmSelectPatientGuarantor = new frmSelectPatientGuarantor(_PatientID, _ClinicID);
                                                        clsSelectPatientGuarantor oClsSelectPatientGuarantor = new clsSelectPatientGuarantor(_PatientID, _ClinicID);
                                                        DataTable dtAccounts = null;
                                                        dtAccounts = oClsSelectPatientGuarantor.GetPatientAccounts(_PatientID, _ClinicID);
                                                        if ((dtAccounts.Rows.Count == 1))
                                                        {
                                                            FieldID1 = Convert.ToInt64(dtAccounts.Rows[0]["nPAccountID"].ToString());
                                                            isGuarantorAlreadySelected = true;
                                                        }
                                                        else if ((dtAccounts.Rows.Count > 1))
                                                        {
                                                            ofrmSelectPatientGuarantor.ShowDialog();
                                                            //aField.Result = ofrmSelectPatientGuarantor.SelectedGuarantor
                                                            if (ofrmSelectPatientGuarantor.DialogResult == DialogResult.OK)
                                                            {
                                                                FieldID1 = ofrmSelectPatientGuarantor.SelectedAccount;
                                                            }
                                                            else
                                                            {
                                                                FieldID1 = -1;
                                                            }
                                                            isGuarantorAlreadySelected = true;
                                                        }
                                                        else
                                                        {
                                                            FieldID1 = 0;
                                                        }
                                                        if ((ofrmSelectPatientGuarantor == null) == false)
                                                        {
                                                            ofrmSelectPatientGuarantor.Dispose();
                                                            ofrmSelectPatientGuarantor = null;
                                                        }
                                                        if ((oClsSelectPatientGuarantor == null) == false)
                                                        {
                                                            oClsSelectPatientGuarantor.Dispose();
                                                            oClsSelectPatientGuarantor = null;
                                                        }
                                                        if (dtAccounts != null)
                                                        {
                                                            dtAccounts.Dispose();
                                                            dtAccounts = null;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    FieldID1 = _AccountID;
                                                }


                                            }
                                            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                                            // We were sending Visit Id as zero, so history liquid links was not populating in check-in template
                                            if (aField.StatusText.StartsWith("History") && aField.StatusText.EndsWith("Allergy") == false)
                                            {
                                                aField.StatusText = aField.StatusText.Replace("+", "+':'+");
                                            }
                                            
                                            strData = GetDataFrom_DB(aField.StatusText.Replace("+", "+space(1)+"), aField.HelpText);

                                            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                                            // We were sending Visit Id as zero, so history liquid links was not populating in check-in template
                                            if (aField.StatusText.StartsWith("History") && aField.StatusText.EndsWith("Allergy") == false)
                                            {
                                                strData = strData.Replace(":  ", "").Replace(": ", "").Replace(" :  : ", "").Replace(" :  ", "").Replace(": |", "|").Replace(":  |", "|").Replace(" :  :  |", "|").Replace(" :  |", "|");
                                            }
                                            
                                            strDataCols = strData.Split('|');

                                            gloWord.gloWord.CurrentDoc = currentDocument;
                                            if (arrTables == null) // To Fetch Value of All FormFields.
                                            {
                                                gloWord.gloWord.GetFormFieldData(strDataCols, aField, dtFlowSheet);
                                            }
                                            else // To Fetch Value of Selected FormFields only.
                                            {
                                                if (arrTables.Contains(strTableName[0]) == true)
                                                {
                                                    gloWord.gloWord.GetFormFieldData(strDataCols, aField, dtFlowSheet);
                                                }
                                            }
                                        }
                                    }
                                    //break;
                                }
                            }
                            break;
                    }
                }
                gloAuditTrail.gloAuditTrail.PrintLog(String.Format("GetFormFieldDataRevised DONE for document  : {0} ; _AccountID : {1} ; PatientID : {2} : UserID {3} : UserName {4} : LoginProviderID {5}", currentDocument.FullName, _AccountID, _PatientID, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, gloGlobal.gloPMGlobal.LoginProviderID));
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                Ex = null;
            }

        }
        //End of code Added New Function For the case GLO2010-0007587


        public static void GetFormFieldData(ArrayList arrTables,Int64  AccountID =0)
        {
            try
            {
                _oCurDoc.Application.ActiveDocument.FormFields.Shaded = false;
                //Make shading false 

                //  _DocumentType = DocumentType;
                string _HelpText = null;
                _HelpText = "None";//  = GetHelpText(DocumentType);

                //string strfieldsvalcol = null;
                //Collection of values 
                Array strDataCols = default(Array);
                //To split collection values with value & Flag 
                string strData = null;
                //Split data will be stored in this variable 

                if ((_oCurDoc == null) == true)
                {
                    return;
                    //Return Nothing 
                }

                //  Wd.FormField aField = default(Wd.FormField);
                //Form field Variable 
                Boolean isGuarantorAlreadySelected = false;
                FieldID1 = 0; 
                foreach (Wd.FormField aField in _oCurDoc.FormFields)
                {
                    switch (aField.Type)
                    {
                        case Wd.WdFieldType.wdFieldFormTextInput:

                            //' Compare table name with FormField status text 
                            string[] strTableName = aField.StatusText.Split('.');
                            if (!string.IsNullOrEmpty(strTableName[0]))
                            {
                                if (strTableName[0] == _HelpText || _HelpText == "None")
                                {

                                    if (aField.StatusText != "")
                                    {
                                        if (strTableName.Length >= 2)
                                        {
                                            if (strTableName[1] != "iPhoto" && strTableName[1] != "imgSignature" && strTableName[1] != "iCard|Driving" && strTableName[1] != "iCard|Insurance")
                                            {
                                                if (_oCurDoc.Application.ActiveDocument.ProtectionType == Wd.WdProtectionType.wdNoProtection)
                                                {
                                                    //if (gblnWordColorHighlight)
                                                    //{
                                                    //    aField.Range.HighlightColorIndex = gblnWordBackColor;
                                                    //}
                                                    //else
                                                    //{
                                                    //    aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight;
                                                    //}
                                                    aField.Range.HighlightColorIndex = Wd.WdColorIndex.wdNoHighlight;
                                                }
                                            }
                                        }

                                        //strData = clsExam.getData_New(_DocumentCriteria.PatientID, Replace(aField.StatusText, "+", "+space(2)+"), _DocumentCriteria.ExamId, _DocumentCriteria.VisitId) 
                                        // SUDHIR 20090314 - PATIENT STATEMENT 
                                        if (strTableName[0] == "PatientStatement")
                                        {
                                            DataTable dtTransaction;
                                            DataTable dtLinePayment;
                                            DataTable dtLinePaymentDetails;
                                            String FileName = "";
                                            object missing = System.Reflection.Missing.Value;

                                            gloPatientStatement oPS = new gloPatientStatement(_databaseconnectionString);
                                            oPS.GetPatientStatement(_PatientID, _ClinicID, out dtTransaction, out dtLinePayment, out dtLinePaymentDetails);
                                            if (oPS != null)
                                            {
                                                oPS.Dispose();
                                                oPS = null;
                                            }
                                            if (dtTransaction != null && dtTransaction.Rows.Count > 0)
                                            {

                                                frmWd_PatientStatement oFrm = new frmWd_PatientStatement(_databaseconnectionString, dtTransaction, dtLinePayment, dtLinePaymentDetails, Convert.ToInt64(strTableName[1]));
                                                //oFrm.ShowDialog();
                                                FileName = oFrm.FileStatement;
                                                WdApplication = _oCurDoc.Application;
                                                Object objRange = "";
                                                Object objBool = false;
                                                Object oWdUnits = (object)Wd.WdUnits.wdCharacter;
                                                Object oCount = (object)1;
                                                _oCurDoc.Select();
                                                aField.Application.Selection.Select();
                                                aField.Select();
                                                aField.Result = "";
                                                //_oCurDoc.Application.Selection.Collapse(ref missing);
                                                //_oCurDoc.Application.Selection.MoveRight(ref oWdUnits, ref oCount, ref missing);
                                                _oCurDoc.Application.Selection.InsertFile(FileName, ref objRange, ref objBool, ref objBool, ref objBool);
                                                oFrm.Dispose();
                                                oFrm = null;
                                            }
                                            if (dtTransaction != null)
                                            {
                                                dtTransaction.Dispose();
                                                dtTransaction = null;
                                            }
                                            if (dtLinePayment != null)
                                            {
                                                dtLinePayment.Dispose();
                                                dtLinePayment = null;
                                            }
                                            if (dtLinePaymentDetails != null)
                                            {
                                                dtLinePaymentDetails.Dispose();
                                                dtLinePaymentDetails = null;
                                            }
                                        }
                                        else
                                        {

                                            if (aField.StatusText.StartsWith("User_MST"))
                                            {
                                                if (aField.StatusText == "User_MST.sLoginName")
                                                {
                                                    if (appSettings["UserName"] != "")
                                                        aField.Result = Convert.ToString(appSettings["UserName"]);
                                                }
                                                else if (aField.StatusText.StartsWith("User_MST.sFirstName"))
                                                {

                                                    aField.Result = GetLoginUserName(Convert.ToString(appSettings["UserName"]));
                                                }
                                                continue;
                                            }
                                            if (aField.StatusText.StartsWith("PA_Accounts_Patients") | aField.StatusText.StartsWith("pa_accounts") | aField.StatusText.StartsWith("pa_accounts_Billing") | aField.StatusText.StartsWith("pa_accounts_PatientLastClaimDiag"))
                                            {
                                                if (AccountID == 0)
                                                {
                                                    if ((isGuarantorAlreadySelected == false))
                                                    {
                                                        frmSelectPatientGuarantor ofrmSelectPatientGuarantor = new frmSelectPatientGuarantor(_PatientID, _ClinicID);
                                                        clsSelectPatientGuarantor oClsSelectPatientGuarantor = new clsSelectPatientGuarantor(_PatientID, _ClinicID);
                                                        DataTable dtAccounts = null;
                                                        dtAccounts = oClsSelectPatientGuarantor.GetPatientAccounts(_PatientID, _ClinicID);
                                                        if ((dtAccounts.Rows.Count == 1))
                                                        {
                                                            FieldID1 = Convert.ToInt64(dtAccounts.Rows[0]["nPAccountID"].ToString());
                                                            isGuarantorAlreadySelected = true;
                                                        }
                                                        else if ((dtAccounts.Rows.Count > 1))
                                                        {
                                                            ofrmSelectPatientGuarantor.ShowDialog(ofrmSelectPatientGuarantor.Parent);
                                                            //aField.Result = ofrmSelectPatientGuarantor.SelectedGuarantor
                                                            FieldID1 = ofrmSelectPatientGuarantor.SelectedAccount;
                                                            isGuarantorAlreadySelected = true;
                                                        }
                                                        else
                                                        {
                                                            FieldID1 = 0;
                                                        }
                                                        if ((ofrmSelectPatientGuarantor == null) == false)
                                                        {
                                                            ofrmSelectPatientGuarantor.Dispose();
                                                            ofrmSelectPatientGuarantor = null;
                                                        }
                                                        if ((oClsSelectPatientGuarantor == null) == false)
                                                        {
                                                            oClsSelectPatientGuarantor.Dispose();
                                                            oClsSelectPatientGuarantor = null;
                                                        }
                                                        if (dtAccounts != null)
                                                        {
                                                            dtAccounts.Dispose();
                                                            dtAccounts = null;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    FieldID1 = AccountID;
                                                }

                                            }
                                            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                                            // We were sending Visit Id as zero, so history liquid links was not populating in check-in template
                                            if (aField.StatusText.StartsWith("History") && aField.StatusText.EndsWith("Allergy") == false)
                                            {
                                                aField.StatusText = aField.StatusText.Replace("+", "+':'+");
                                            }
                                            
                                            strData = GetDataFrom_DB(aField.StatusText.Replace("+", "+space(1)+"), aField.HelpText);

                                            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
                                            // We were sending Visit Id as zero, so history liquid links was not populating in check-in template                                            if (aField.StatusText.StartsWith("History") && aField.StatusText.EndsWith("Allergy") == false)
                                            {
                                                strData = strData.Replace(":  ", "").Replace(": ", "").Replace(" :  : ", "").Replace(" :  ", "").Replace(": |", "|").Replace(":  |", "|").Replace(" :  :  |", "|").Replace(" :  |", "|");
                                            }
                                            strDataCols = strData.Split('|');

                                            gloWord.gloWord.CurrentDoc = _oCurDoc;
                                            if (arrTables == null) // To Fetch Value of All FormFields.
                                            {
                                                gloWord.gloWord.GetFormFieldData(strDataCols, aField, dtFlowSheet);
                                            }
                                            else // To Fetch Value of Selected FormFields only.
                                            {
                                                if (arrTables.Contains(strTableName[0]) == true)
                                                {
                                                    gloWord.gloWord.GetFormFieldData(strDataCols, aField, dtFlowSheet);
                                                }
                                            }
                                        }
                                       

                                       
                                    }
                                    //break;
                                }
                            }
                            break;
                    }
                }
                // aField = null;
                // set nothing 
                //strfieldsvalcol = null;
                //Set nothing 
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }

        }

        public static string GenerateDocumentFile()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionString);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            _NewDocumentName = "";

            string strSQL = "";
            DataTable dt = null;
            object oResult = null;

            strSQL = " SELECT  ISNULL(sDescription,'') as sDescription FROM TemplateGallery_MST WITH (NOLOCK) WHERE nTemplateID = " + _PrimaryID + "";
            oDB.Connect(false);
            oDB.Retrive_Query(strSQL, out  dt);
            oDB.Disconnect();
            oDB.Dispose();

            if (dt == null)
            {
                _NewDocumentName = "";
            }
            else if (dt.Rows.Count <= 0)
            {
                _NewDocumentName = "";
            }
            else if (dt.Rows.Count > 0)
            {
                oResult = dt.Rows[0]["sDescription"];
                _NewDocumentName = NewDocumentName();
                gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionString);
                if (ogloTemplate.ConvertBinaryToFile(oResult, _NewDocumentName) == true)
                {
                    ogloTemplate.Dispose();
                    ogloTemplate = null;
                    dt.Dispose();
                    dt = null;
                    return _NewDocumentName;
                }
                else
                {
                    _NewDocumentName = "";
                }
                ogloTemplate.Dispose();
                ogloTemplate = null;
            }
            if (dt != null)
            {
                dt.Dispose();
                dt = null;
            }
            return _NewDocumentName;
        }
        public static string GenerateDocumentFile(Int64 nTemplateID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionString);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            _NewDocumentName = "";

            string strSQL = "";
            DataTable dt = null;
            object oResult = null;

            strSQL = " SELECT  ISNULL(sDescription,'') as sDescription FROM TemplateGallery_MST WITH (NOLOCK) WHERE nTemplateID = " + nTemplateID + "";
            oDB.Connect(false);
            oDB.Retrive_Query(strSQL, out  dt);
            oDB.Disconnect();
            oDB.Dispose();

            if (dt == null)
            {
                _NewDocumentName = "";
            }
            else if (dt.Rows.Count <= 0)
            {
                _NewDocumentName = "";
            }
            else if (dt.Rows.Count > 0)
            {
                oResult = dt.Rows[0]["sDescription"];
                _NewDocumentName = NewDocumentName();
                gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionString);
                if (ogloTemplate.ConvertBinaryToFile(oResult, _NewDocumentName) == true)
                {
                    ogloTemplate.Dispose();
                    ogloTemplate = null;
                    dt.Dispose();
                    dt = null;
                    return _NewDocumentName;
                }
                else
                {
                    _NewDocumentName = "";
                }
                ogloTemplate.Dispose();
                ogloTemplate = null;
            }
            if (dt != null)
            {
                dt.Dispose();
                dt = null;
            }
            return _NewDocumentName;
        }

        //public static Byte[] ConvertFileToBinary(string FilePath)
        //{
        //    System.IO.FileStream oFileStream = null;
        //    BinaryReader oBinaryRead = null;
        //    Byte[] byteRead = null;
        //    try
        //    {
        //        if (System.IO.File.Exists(FilePath))
        //        {
        //            //oFileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous);
        //            oFileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        //            oBinaryRead = new BinaryReader(oFileStream);
        //            byteRead = oBinaryRead.ReadBytes(Convert.ToInt32(oFileStream.Length));

        //            //-------------------------------------------
        //            //Changes made on 20080719 - By Sagar Ghodke 
        //            //To release the File
        //            if (oFileStream != null)
        //            {
        //                oFileStream.Close();
        //                oFileStream.Dispose();
        //                oFileStream = null;
        //            }

        //            if (oBinaryRead != null)
        //            {
        //                oBinaryRead.Close();
        //                oBinaryRead = null;
        //            }
        //            //end Changes - 20080719
        //            //-------------------------------------------

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }
        //    return byteRead;
        //}

        //public static bool ConvertBinaryToFile(object StreamData, string FilePath)
        //{
        //    bool _result = false;
        //    //  string _FilePath = FilePath;
        //    try
        //    {
        //        if (StreamData != null)
        //        {
        //            Byte[] byteRead = (byte[])StreamData;
        //            MemoryStream oDataStream = new MemoryStream(byteRead);
        //            FileStream oFile = new FileStream(FilePath, FileMode.Create);
        //            oDataStream.WriteTo(oFile);
        //            oFile.Close();
        //            _result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return _result;
        //}

        public static bool EmptygloPMTempFolder()
        {
            //String _Path = Application.StartupPath + "\\" + "Temp";
            String _Path = gloSettings.FolderSettings.AppTempFolderPath;
            if (System.IO.Directory.Exists(_Path) == true)
            {
                //System.IO.DirectoryInfo oDirectoryInfo = new System.IO.DirectoryInfo(_Path);
                //System.IO.FileInfo[] oFiles = null;
                //oFiles = oDirectoryInfo.GetFiles();
                //if (oFiles != null)
                //{
                //    if (oFiles.Length > 0)
                //    {
                //        foreach (System.IO.FileInfo oFile in oFiles)
                //        {
                //            try
                //            {
                //                if (oFile.Attributes != FileAttributes.Normal)
                //                {
                //                    oFile.Attributes = FileAttributes.Normal;
                //                }
                //                oFile.Delete();
                //            }
                //            catch (Exception ex)
                //            {
                //                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                //            }
                //        }
                //    }
                //}

                //System.IO.DirectoryInfo oDirInfo = new System.IO.DirectoryInfo(_Path);

                //if (oDirInfo.GetDirectories().Length == 0 && oDirInfo.GetFiles().Length == 0)
                //{
                //Added By Mitesh to Delete Temp folder with subDir ,Date 20101004
                try
                {
                    System.IO.Directory.Delete(_Path,true);
                }
                catch (IOException ioEX)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ioEX.ToString(),false);
                }
               // }



            }
            return true;
        }

        [System.STAThread()]
        public static void InitializaWord()
        {
            //'Kill the Word instances that are not properly killed 
            KillOtherWord();
            //'Initialse the gloEMR Word Instance 
            if (!CheckWord())
            {
                _gblWordApplication = new Wd.Application();
                _gblWordApplication.Visible = false;
            }
        }

        //[System.STAThread()]
        //public static void KillOtherWord()
        //{
        //    System.Diagnostics.Process[] wdPro = null;
        //    wdPro = System.Diagnostics.Process.GetProcessesByName("WINWORD");
        //    try
        //    {
        //        if (wdPro.Length > 0)
        //        {
        //            //Kill all the Word processes that are not been used for gloEMR or External
        //            int myCount = 0;



        //            foreach (Process tempProc in wdPro)
        //            {
        //                myCount = myCount + 1;
        //                Wd.Application exitTempApp = null;
        //                try
        //                {
        //                    exitTempApp = (Wd.Application)(Microsoft.VisualBasic.Interaction.GetObject(null, "Word.Application"));

        //                }
        //                catch //(Exception ex)
        //                {
        //                }


        //                if ((exitTempApp != null))
        //                {
        //                    if ((exitTempApp.Visible == false | exitTempApp.Documents.Count == 0))
        //                    {
        //                        //'Kill the word instance that was invisible and release te refernce
        //                        string wdAppCaption = exitTempApp.Caption;
        //                        object omissing = System.Reflection.Missing.Value;
        //                        //'Kill the word instance that was invisible and release te refernce 
        //                        exitTempApp.Quit(ref omissing, ref omissing, ref omissing);

        //                        bool releaseComObject = false;
        //                        foreach (Process Proc in Process.GetProcessesByName("WINWORD"))
        //                        {
        //                            if (!string.IsNullOrEmpty(Proc.MainWindowTitle))
        //                            {
        //                                if (Proc.MainWindowTitle.Contains(wdAppCaption))
        //                                {
        //                                    Proc.CloseMainWindow();
        //                                    if ((myCount == wdPro.Length))
        //                                    {
        //                                        Marshal.FinalReleaseComObject(exitTempApp);
        //                                        releaseComObject = true;
        //                                    }
        //                                    else
        //                                    {
        //                                        Marshal.ReleaseComObject(exitTempApp);
        //                                        releaseComObject = true;
        //                                    }
        //                                    break; // TODO: might not be correct. Was : Exit For
        //                                }
        //                            }
        //                        }
        //                        if ((releaseComObject == false))
        //                        {
        //                            if ((myCount == wdPro.Length))
        //                            {
        //                                Marshal.FinalReleaseComObject(exitTempApp);
        //                                releaseComObject = true;
        //                            }
        //                            else
        //                            {
        //                                Marshal.ReleaseComObject(exitTempApp);
        //                                releaseComObject = true;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //        return;
        //    }
        //    finally
        //    {
        //        if (wdPro != null)
        //        {

        //            wdPro = null;
        //        }
        //    }

        //}
        /// <summary>
        /// To exit the Unwanted WinWord processes for optimization
        /// </summary>
        /// <remarks></remarks>
        [System.STAThread()]

        public static void KillOtherWord()
        {
            //List<System.Diagnostics.Process> lstProcesses = new List<System.Diagnostics.Process>();

            //int session = Process.GetCurrentProcess().SessionId;

            //foreach (Process word_process in Process.GetProcessesByName("WINWORD"))
            //{
            //    if ((word_process.SessionId == session))
            //    {
            //        lstProcesses.Add(word_process);
            //        break; 
            //    }
            //}

            //try
            //{
            //    //'Get the no of word instances processes running
            //    if (lstProcesses.Count > 0)
            //    {
            //        //Kill all the Word processes that are not been used for gloPM or gloEMR or External
            //        Wd.Application exitTempApp = null;

            //        try
            //        {
            //            exitTempApp = (Wd.Application)(Microsoft.VisualBasic.Interaction.GetObject(null, "Word.Application"));

            //        }
            //        catch 
            //        {
            //        }

            //        if (exitTempApp != null)
            //        {
            //            bool noActiveDocument = (exitTempApp.Documents == null);
            //            if (noActiveDocument == false)
            //            {
            //                noActiveDocument = exitTempApp.Documents.Count == 0;
            //            }

            //            if ((exitTempApp.Visible == false) || noActiveDocument)
            //            {
            //                try
            //                {
            //                    object mysaveoptions = (object)Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            //                    exitTempApp.Quit(SaveChanges: mysaveoptions);

            //                }
            //                catch 
            //                {
            //                }
            //                if (noActiveDocument)
            //                {
            //                    //SLR: Following some time hangs and hence writen to kill rather than exit.
            //                    //try
            //                    //{
            //                    //    Marshal.ReleaseComObject(exitTempApp);
            //                    //}
            //                    //catch
            //                    //{
            //                    //}
            //                    Application.DoEvents();
            //                    lstProcesses.Clear();
            //                    foreach (Process word_process in Process.GetProcessesByName("WINWORD")) //Reinitialize looks up processes
            //                    {
            //                        if ((word_process.SessionId == session))
            //                        {
            //                            lstProcesses.Add(word_process);
            //                            break;
            //                        }
            //                    }
            //                    foreach (Process Proc in lstProcesses)
            //                    {
            //                        try
            //                        {
            //                            Proc.CloseMainWindow();

            //                        }
            //                        catch
            //                        {
            //                        }
            //                        Application.DoEvents();
            //                        try
            //                        {
            //                            Proc.Close();
            //                        }
            //                        catch
            //                        {
            //                        }
            //                        Application.DoEvents();
            //                        try
            //                        {
            //                            Proc.Kill();
            //                        }
            //                        catch 
            //                        {
            //                        }
            //                        break; 
            //                    }

            //                }
            //            }
            //        }


            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at function KillOtherWord of gloOffice: " + ex.ToString() + " " + ex.InnerException.ToString(), false);
            //    return;
            //}
            //finally
            //{
            //    if (lstProcesses != null)
            //    {
            //        lstProcesses.Clear();
            //        lstProcesses = null;
            //    }
            //}
            gloWord.gloWord.KillOtherWord();
        }
        public static bool CheckWord()
        {
            //int session = Process.GetCurrentProcess().SessionId;

            //foreach (Process word_process in Process.GetProcessesByName("WINWORD"))
            //{
            //    if ((word_process.SessionId == session))
            //    {
            //        return true;
            //    }
            //}
            //return false;
            return gloWord.gloWord.CheckWord();
            //System.Diagnostics.Process[] wdPro = null;
            //wdPro = System.Diagnostics.Process.GetProcessesByName("WINWORD");
            //if (wdPro.Length > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public static bool IsDuplicateFormFields(ArrayList arrFieldsToAdd)
        {
            ArrayList arrFields = new ArrayList();
            try
            {
                if ((_oCurDoc == null) == true)
                {
                    return false;
                    //Return Nothing 
                }

                foreach (Wd.FormField aField in _oCurDoc.FormFields)
                {
                    arrFields.Add(aField.StatusText);
                }

                for (int i = 0; i < arrFieldsToAdd.Count; i++)
                {
                    if (arrFields.Contains(((gloGeneralNode.gloGeneralNode)arrFieldsToAdd[i]).Code) == true)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            return false;
        }

        public static bool UpdateFormFields()
        {
            try
            {
                if (_oCurDoc.FormFields == null)
                { return false; }

                foreach (Wd.FormField oField in _oCurDoc.FormFields)
                {
                    if (oField.Type == Wd.WdFieldType.wdFieldFormTextInput)
                    {
                        string[] strTableName = oField.StatusText.Split('.');
                        if (strTableName[0] != null && strTableName[0] != "")
                        {
                            #region " CoPay & AdvancePayment Update "
                            if (strTableName[0] == "BL_Transaction_CoPay_MST" || strTableName[0] == "BL_Transaction_AdvancePayment")
                            {
                                if (strTableName[1] != null)
                                {
                                    switch (strTableName[1])
                                    {
                                        case "nAppointmentDate":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.nAppointmentDate";
                                            break;
                                        case "nTransactionDate":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.nTransactionDate";
                                            break;
                                        case "dCoPayAmount":
                                        case "dPaymentAmount":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.dAdvPayAmount";
                                            break;
                                        case "nPaymentMode":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.nPaymentMode";
                                            break;
                                        case "sCrdNoMnyOrdNoChkNo":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.sCrdNoMnyOrdNoChkNo";
                                            break;
                                        case "sCardType":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.sCardType";
                                            break;
                                        case "nCrdExpChkMnyOrdDate":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.nCrdExpChkMnyOrdDate";
                                            break;
                                        case "sSecurityNo":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.sSecurityNo";
                                            break;
                                        case "sCPTCode":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.sCPTCode";
                                            break;
                                        case "sDxCode":
                                            oField.StatusText = "BL_Transaction_AdvancePayment_MST.sDxCode";
                                            break;
                                    }
                                }
                            }
                            #endregion

                            #region " AutoClaim & Workers Comp Update "
                            else if (strTableName[0] == "Patient")
                            {
                                if (strTableName[1] != null)
                                {
                                    switch (strTableName[1])
                                    {
                                        case "bIsAutoClaim":
                                            oField.StatusText = "Patient_WorkersComp.sClaimno|bAutoClaim";
                                            break;
                                        case "bIsWorkersComp":
                                            oField.StatusText = "Patient_WorkersComp.sClaimno|bWorkersComp";
                                            break;
                                        case "sAutoClaimNo":
                                            oField.StatusText = "Patient_WorkersComp.sClaimno|AutoClaim";
                                            break;
                                        case "sWorkersCompClaimNo":
                                            oField.StatusText = "Patient_WorkersComp.sClaimno|WorkersComp";
                                            break;
                                    }
                                }
                            }
                            #endregion

                            #region " AutoClaim & Workers Comp Update "
                            else if (strTableName[0] == "BL_EOBPayment_MST")
                            {
                                if (strTableName[1] != null)
                                {
                                    switch (strTableName[1])
                                    {
                                        case "sCheckNumber":
                                            oField.StatusText = "Credits.sReceiptNo";
                                            break;
                                        case "nCheckDate":
                                            oField.StatusText = "Credits.dtReceiptDate";
                                            break;
                                        case "nCheckAmount":
                                            oField.StatusText = "Credits.dReceiptAmount";
                                            break;
                                       
                                        }
                                    }
                                }
                           #endregion
                            #region "Account # for Family Accounting "
                            else if (strTableName[0] == "PA_Accounts_Patients")
                            {
                                if (strTableName[1] != null)
                                {
                                    switch (strTableName[1])
                                    {
                                        case "sAccountNo +' - '+pa_accounts":
                                            oField.StatusText = "PA_Accounts_Patients.sAccountNo";
                                            break;
                                    }
                                }
                            }
                            #endregion
                            #region " Remove Expired FormFields "
                            else
                            {
                                if (IsFormFieldExpired(oField.StatusText) == true)
                                {
                                    oField.Delete();
                                }

                            }
                            #endregion
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Modify, "Error while Updating FormFields : " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
        }

        private static bool IsFormFieldExpired(string statusText)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionString);
            bool _Result = false;
            try
            {
               string query = " SELECT COUNT(*) FROM DataDictionary_MST WITH (NOLOCK) WHERE sFieldName = '" + statusText.Replace("'", "''").ToString()  + "' ";
                object oResult;
                oDB.Connect(false);
                oResult = oDB.ExecuteScalar_Query(query);
                if (Convert.ToInt32(oResult) <= 0)
                {
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                _Result = false;
                throw;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return _Result;
        }
    }

    /// <summary>
    ///  To Do the Differen Operations on the Patient Statement
    /// </summary>
    public class gloPatientStatement : IDisposable
    {
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region " Constructor & Distructor "

        public gloPatientStatement(string DatabaseConnectionString)
        {
            _databaseConnectionString = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~gloPatientStatement()
        {
            Dispose(false);
        }

        #endregion

        #region  " Variable Declarations "
        String _databaseConnectionString = "";
        String _MessageBoxCaption = String.Empty;
        #endregion " Variable Declarations "

        #region " Public Properties "
        private Int64 _PatientID;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        private Int64 _TemplateID;
        public Int64 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        private String _TemplateName;
        public String TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }

        private String _TemplateFilePath;
        public String TemplateFilePath
        {
            get { return _TemplateFilePath; }
            set { _TemplateFilePath = value; }
        }

        private Int64 _ProviderID;
        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }

        private Int64 _PrimeryID;
        public Int64 PrimeryID
        {
            get { return _PrimeryID; }
            set { _PrimeryID = value; }
        }

        private Int64 _ClinicID;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        private Int64 _FromDate;
        public Int64 FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        private Int64 _ToDate;
        public Int64 ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion

        public DataTable GetSingleTemplate(Int64 TemplateID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;
            try
            {
                oDB.Connect(false);
                string strSQL = "";

                strSQL = " SELECT isnull(nTemplateID,0) as nTemplatID, ISNULL(sTemplateName,'') as sTemplateName, " +
                         " ISNULL(nProviderID,0) as nProviderID,  ISNULL(sDescription,null) as sDescription " +
                         " FROM DataDictionary_DTL WITH (NOLOCK) " +
                         " WHERE nTemplateID = " + TemplateID + " ";


                oDB.Retrive_Query(strSQL, out  dt);
                oDB.Disconnect();
                oDB.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return dt;
            }

        }

        public bool SaveTemplate(Int64 TemplateID, String TemplateName, Int64 Type, Int64 ProviderID, String TemplateFilePath)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@TemplateID", TemplateID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@TemplateName", TemplateName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nType", Type, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);

                gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
                Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(TemplateFilePath);
                ogloTemplate.Dispose();
                ogloTemplate = null;

                oDBParameters.Add("@Description", oTemplate, ParameterDirection.Input, SqlDbType.Image);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("gsp_InUpPatientStatement", oDBParameters);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public void GetPatientStatement(Int64 PatientID, Int64 ClinicID, out DataTable dtTransaction, out DataTable dtLinesPayment, out DataTable dtLinesPaymentDetails)
        {
            #region " Variable Declarations "

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtTrasaction = new DataTable();
            DataTable _dtLinesPayment = new DataTable();
            DataTable _dtLinePaymentDetails = new DataTable();
            DataTable _dtTransactionIds = null;
            Int64 _BillingTransactionID = 0;
            Int64 _LineNo = 0;
            Int64 _LineDetailId = 0;
            DataTable _dtTempTable = null;
            DataTable _dtDetailsTemp = null;
            string _sqlQuery = "";

            #endregion

            try
            {
                oDB.Connect(false);

                #region " Get Transaction ID's againts Patient "

                _sqlQuery = "SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST WITH (NOLOCK)  " +
                                  "WHERE BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_MST.nTransactionID IS NOT NULL AND BL_Transaction_MST.nTransactionID > 0 " +
                                  "ORDER BY BL_Transaction_MST.nTransactionID";
                oDB.Retrive_Query(_sqlQuery, out _dtTransactionIds);

                #endregion " Get Transaction ID's againts Patient "

                if (_dtTransactionIds != null && _dtTransactionIds.Rows.Count > 0)
                {
                    for (int nTrnIDCntr = 0; nTrnIDCntr <= _dtTransactionIds.Rows.Count - 1; nTrnIDCntr++)
                    {
                        #region "Retrive Billing Master Transaction"

                        _BillingTransactionID = Convert.ToInt64(_dtTransactionIds.Rows[nTrnIDCntr]["nTransactionID"].ToString());

                        if (_BillingTransactionID > 0)
                        {
                            oDBParameters.Clear();
                            oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Retrive("BL_SELECT_Transaction_MST", oDBParameters, out _dtTempTable);
                            if (_dtTempTable != null && _dtTempTable.Rows.Count > 0)
                            {
                                _dtTrasaction.Merge(_dtTempTable, true);
                                _dtTrasaction.AcceptChanges();
                                
                                

                                #region " Load TransactionLines & Payments "

                                oDBParameters.Clear();
                                oDBParameters.Add("@nTransactionID", _BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                                _dtTempTable.Dispose();
                                _dtTempTable = null;

                                oDB.Retrive("BL_SELECT_Transaction_Lines_PaidNBalance", oDBParameters, out _dtTempTable);
                                oDBParameters.Clear();

                                if (_dtTempTable != null && _dtTempTable.Rows.Count > 0)
                                {
                                    _dtLinesPayment.Merge(_dtTempTable, true);
                                    _dtLinesPayment.AcceptChanges();
                                  
                                    for (int lineIndex = 0; lineIndex < _dtTempTable.Rows.Count; lineIndex++)
                                    {
                                        _LineNo = Convert.ToInt64(_dtTempTable.Rows[lineIndex]["nTransactionLineNo"]);
                                        _LineDetailId = Convert.ToInt64(_dtTempTable.Rows[lineIndex]["nTransactionDetailID"]);


                                        #region " Line Payment Details "

                                        _sqlQuery =
                                        " SELECT ISNULL(BL_Transaction_Payment_DTL.nPaymentTransactionID,0) AS nPaymentTransactionID, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nPaymentTransactionDetailID,0) AS nPaymentTransactionDetailID, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nPatientID,0) AS nPatientID, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nPaymentDate,0) AS nPaymentDate, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nBillingTransactionID,0) AS nBillingTransactionID, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nBillingTransactionDetailID,0) AS nBillingTransactionDetailID,  " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nBillingTransactionLineNo,0) AS nBillingTransactionLineNo, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nClaimNo,0) AS nClaimNo, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.sCPTCode,'') AS sCPTCode,  " +
                                        " ISNULL(BL_Transaction_Payment_DTL.sCPTDescription,'') AS sCPTDescription, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.dAllowedAmt,0) AS dAllowedAmt, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.dCurrentPaymentAmt,0) AS dCurrentPaymentAmt,  " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nTransactionType,0) AS nTransactionType, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nTransactionType,0) AS nTransactionType, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nPaymentMode,0) AS nPaymentMode, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.sCrdNoMnyOrdNoChkNo,'') AS sCrdNoMnyOrdNoChkNo, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nCrdExpChkMnyOrdDate,0) AS nCrdExpChkMnyOrdDate, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.sSecurityNo,'') AS sSecurityNo, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.sCardType,'') AS sCardType, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nPaymentInsuranceID,0) AS nPaymentInsuranceID, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nPayerModeID,0) AS nPayerModeID, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nPaymentTransactionLineStatus,0) AS nPaymentTransactionLineStatus, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nClinicID,0) AS nClinicID, " +
                                        " ISNULL(BL_Transaction_Payment_DTL.nCurrentPaymentCopayID,0) AS nCurrentPaymentCopayID, " +
                                        " ISNULL(PatientInsurance_DTL.sInsuranceName,'') AS InsuranceName " +
                                        " FROM BL_Transaction_Payment_DTL WITH (NOLOCK) LEFT OUTER JOIN " +
                                        " PatientInsurance_DTL WITH (NOLOCK) ON BL_Transaction_Payment_DTL.nPaymentInsuranceID = PatientInsurance_DTL.nInsuranceID" +
                                        " WHERE nBillingTransactionID = " + _BillingTransactionID + " AND nBillingTransactionDetailID =" + _LineDetailId + " " +
                                        " ORDER BY nPaymentDate ";

                                        oDB.Retrive_Query(_sqlQuery, out _dtDetailsTemp);
                                        if (_dtDetailsTemp != null && _dtDetailsTemp.Rows.Count > 0)
                                        {
                                            _dtLinePaymentDetails.Merge(_dtDetailsTemp);
                                            _dtLinePaymentDetails.AcceptChanges();
                                            _dtDetailsTemp.Dispose();
                                            _dtDetailsTemp = null;
                                        }
                                        if (_dtDetailsTemp != null)
                                        {
                                            _dtDetailsTemp.Dispose();
                                            _dtDetailsTemp = null;
                                        }
                                        #endregion

                                        _LineNo = 0;
                                        _LineDetailId = 0;

                                    }
                                 
                                }
                                if (_dtTempTable != null)
                                {
                                    _dtTempTable.Dispose();
                                    _dtTempTable = null;
                                }
                                //_dtTempTable = new DataTable();

                                #endregion " Load TransactionLines & Payments "

                                _BillingTransactionID = 0;
                            }
                            if (_dtTempTable != null)
                            {
                                _dtTempTable.Dispose();
                                _dtTempTable = null;
                            }
                        }

                        #endregion
                    }
                }
                
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.Message); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtTransactionIds != null) { _dtTransactionIds.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }

            dtTransaction = _dtTrasaction;
            dtLinesPayment = _dtLinesPayment;
            dtLinesPaymentDetails = _dtLinePaymentDetails;
        }

    }
}
