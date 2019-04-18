using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlTypes;
using System.Windows.Forms;
namespace gloEmdeonInterface.Classes
{
    /// <summary>
    /// Class for report object and properties
    /// </summary>
    class clsSpiroReports : IDisposable
    {

        #region "Constructor and Destructor"

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
        public clsSpiroReports()
        {

        }
        ~clsSpiroReports()
        {
            Dispose(false);
        }

        #endregion "Constructor and Destructor"

        #region "Variables and Properties"

        Int64 edocumentId = 0;
        string _ErrorMessage = string.Empty;
        public Int64 DocumentId
        {
            get { return edocumentId; }
            set { edocumentId = value; }
        }
        byte[] docStream = null;

        public byte[] DocStream
        {
            get { return docStream; }
            set { docStream = value; }
        }
        string documentname = string.Empty;

        public string Documentname
        {
            get { return documentname; }
            set { documentname = value; }
        }
        Int64 patientId = 0;

        public Int64 PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }
        string sdocumentExtension = string.Empty;

        public string DocumentExtension
        {
            get { return sdocumentExtension; }
            set { sdocumentExtension = value; }
        }
        DateTime dtdocumentCreated;

        public DateTime dtDocumentCreated
        {
            get { return dtdocumentCreated; }
            set { dtdocumentCreated = value; }
        }
        string strMachineName = string.Empty;

        public string MachineName
        {
            get { return strMachineName; }
            set { strMachineName = value; }
        }
        Int64 intClinicId = 0;

        public Int64 ClinicId
        {
            get { return intClinicId; }
            set { intClinicId = value; }
        }
        Int64 visitId = 0;

        public Int64 VisitId
        {
            get { return visitId; }
            set { visitId = value; }
        }
        Int64 refernceId = 0;

        public Int64 RefernceId
        {
            get { return refernceId; }
            set { refernceId = value; }
        }
        Int64 OrderById = 0;

        public Int64 OrderedById
        {
            get { return OrderById; }
            set { OrderById = value; }
        }
        string strOrderByType = string.Empty;
        public string OrderByType
        {
            get { return strOrderByType; }
            set { strOrderByType = value;}
        }
        Int64 userId = 0;

        public Int64 UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        Int64 reviewerId = 0;

        public Int64 ReviewerId
        {
            get { return reviewerId; }
            set { reviewerId = value; }
        }
        string suserName = string.Empty;

        public string UserName
        {
            get { return suserName; }
            set { suserName = value; }
        }
        string sreviewer = string.Empty;

        public string Reviewer
        {
            get { return sreviewer; }
            set { sreviewer = value; }
        }

        Int64 documentdtlid = 0;

        public Int64 DocumentdtlId
        {
            get { return documentdtlid; }
            set { documentdtlid = value; }
        }
        DateTime dtdocumentModified;

        public DateTime dtDocumentModified
        {
            get { return dtdocumentModified; }
            set { dtdocumentModified = value; }
        }
        double dblWeightinkg = 0;

        public double Weightinkg
        {
            get { return dblWeightinkg; }
            set { dblWeightinkg = value; }
        }
        double dblHieghtincms = 0;

        public double Hieghtincms
        {
            get { return dblHieghtincms; }
            set { dblHieghtincms = value; }
        }
        Int64 raceCode = 0;

        public Int64 RaceCode
        {
            get { return raceCode; }
            set { raceCode = value; }
        }
        bool isSmoker = false;

        public bool IsSmoker
        {
            get { return isSmoker; }
            set { isSmoker = value; }
        }
        int noOfCigarsperDay = 0;

        public int NoOfCigarsperDay
        {
            get { return noOfCigarsperDay; }
            set { noOfCigarsperDay = value; }
        }
        int noofsmokingyears = 0;

        public int Noofsmokingyears
        {
            get { return noofsmokingyears; }
            set { noofsmokingyears = value; }
        }
        bool isquitsmoking = false;

        public bool Isquitsmoking
        {
            get { return isquitsmoking; }
            set { isquitsmoking = value; }
        }
        int noofquitsmokingyears = 0;

        public int Noofquitsmokingyears
        {
            get { return noofquitsmokingyears; }
            set { noofquitsmokingyears = value; }
        }
        Int64 dtlVisitId = 0;

        public Int64 VisitId_Dtl
        {
            get { return dtlVisitId; }
            set { dtlVisitId = value; }
        }
        String sStatus = String.Empty;

        public String Status
        {
            get { return sStatus; }
            set { sStatus = value; }
        }

        String sInterpretation = String.Empty;
        public string Interpretation
        {
            get { return sInterpretation; }
            set { sInterpretation = value; }
        }
        String sDiagnosis = String.Empty;
        public string Diagnosis
        {
            get { return sDiagnosis; }
            set { sDiagnosis = value; }
        }
        Int64 nTestOrderId = 0;
        public Int64 TestOrderId
        {
            get { return nTestOrderId; }
            set { nTestOrderId = value; }
        }
        string sTestOrderPrefix = string.Empty;
        public string TestOrderprefix
        {
            get { return sTestOrderPrefix; }
            set { sTestOrderPrefix = value; }
        }
        string sTestType = string.Empty;
        public string TestType
        {
            set
            {
                sTestType = value;
            }
            get
            {
                return sTestType;
            }
        }
        string sTestDetails = String.Empty;
        public string TestDetails
        {
            set
            {  sTestDetails = value; }
            get
            { return sTestDetails;   }
        }
        string sTestSource = String.Empty;
        public string TestSource
        {
            set
            { sTestSource = value; }
            get
            { return sTestSource; }
        }
        #endregion "Variables and Properties"
    }

    /// <summary>
    /// Class for managing Spiro report management (Save/Read)
    /// </summary>
    class clsSpiroReportmanager : IDisposable
    {
        #region "Global Variables"
        internal static int gBufferSize = 1048576;
        string _ErrorMessage = String.Empty;
        string strConnectionString = string.Empty;
        static String gstrMessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion "Global Variables"

        #region "Constructor & Destructor"
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
        public clsSpiroReportmanager(string connectionString)
        {
            strConnectionString = connectionString;
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            {
                gstrMessageBoxCaption = "gloEMR";
            }
        }
        ~clsSpiroReportmanager()
        {
            Dispose(false);
        }

        #endregion "Constructor & Destructor"

        #region "User Defined Functions"
        //functionality for saving and updating Spiro report in database...
        public void saveSpiroReport(clsSpiroReports objclsSpiroreport)
        {
            SqlConnection _objConnection = null;
            SqlCommand _objCommand = null;
            SqlTransaction _objSqlTransaction = null;
            SqlParameter _objSqlParamater = null;
            try
            {
                if (objclsSpiroreport != null)
                {
                    _objConnection = new SqlConnection(strConnectionString);
                    if (_objConnection != null)
                    {
                        //Insert/ Update DocumentContainer
                        _objConnection.Open();
                        _objSqlTransaction = _objConnection.BeginTransaction();

                        _objCommand = new SqlCommand("InUp_DocumentContainer", _objConnection);
                        _objCommand.CommandType = CommandType.StoredProcedure;
                        _objCommand.Transaction = _objSqlTransaction;

                        _objSqlParamater = new SqlParameter("@eDocumentId", SqlDbType.BigInt);
                        _objSqlParamater.Value = objclsSpiroreport.DocumentId;
                        _objSqlParamater.Direction = ParameterDirection.InputOutput;
                        _objCommand.Parameters.Add(_objSqlParamater);

                        if (_objSqlParamater != null)
                        {
                            _objSqlParamater = null;
                        }

                        _objCommand.Parameters.Add("@sDocumentName", SqlDbType.VarChar).Value = objclsSpiroreport.Documentname;
                        _objCommand.Parameters.Add("@nPatientId", SqlDbType.BigInt).Value = objclsSpiroreport.PatientId;
                        _objCommand.Parameters.Add("@eDocumentExtention", SqlDbType.VarChar).Value = objclsSpiroreport.DocumentExtension;
                        _objCommand.Parameters.Add("@dtDocumentCreated ", SqlDbType.DateTime).Value = objclsSpiroreport.dtDocumentCreated;
                        _objCommand.Parameters.Add("@MachineId", SqlDbType.VarChar).Value = objclsSpiroreport.MachineName;
                        _objCommand.Parameters.Add("@clinicId", SqlDbType.BigInt).Value = objclsSpiroreport.ClinicId;
                        _objCommand.Parameters.Add("@nVisitId ", SqlDbType.BigInt).Value = objclsSpiroreport.VisitId;
                        _objCommand.Parameters.Add("@nReferenceId", SqlDbType.BigInt).Value = objclsSpiroreport.RefernceId;
                        _objCommand.Parameters.Add("@nOrderById", SqlDbType.BigInt).Value = objclsSpiroreport.OrderedById;
                        _objCommand.Parameters.Add("@sOrderByType", SqlDbType.VarChar).Value = objclsSpiroreport.OrderByType;
                        _objCommand.Parameters.Add("@nUserID", SqlDbType.BigInt).Value = objclsSpiroreport.UserId;
                        _objCommand.Parameters.Add("@sUserName", SqlDbType.VarChar).Value = objclsSpiroreport.UserName;
                        _objCommand.Parameters.Add("@nReviewedById", SqlDbType.BigInt).Value = objclsSpiroreport.ReviewerId;
                        _objCommand.Parameters.Add("@sReviewer", SqlDbType.VarChar).Value = objclsSpiroreport.Reviewer;
                        _objCommand.Parameters.Add("@nTestOrderID", SqlDbType.BigInt).Value = objclsSpiroreport.TestOrderId;
                        _objCommand.Parameters.Add("@sTestOrderPrefix", SqlDbType.VarChar).Value = objclsSpiroreport.TestOrderprefix;
                        _objCommand.Parameters.Add("@sTestType", SqlDbType.VarChar).Value = objclsSpiroreport.TestType;
                        _objCommand.Parameters.Add("@sTestDetails", SqlDbType.VarChar).Value = objclsSpiroreport.TestDetails;
                        _objCommand.Parameters.Add("@sTestSource", SqlDbType.VarChar).Value = objclsSpiroreport.TestSource;
                         //@nTestOrderID numeric(18,0)=Null,
                        //@sTestOrderPrefix varchar(255)=Null

                        _objCommand.ExecuteNonQuery();

                        if (_objCommand.Parameters["@eDocumentId"].Value != null)
                        {
                            objclsSpiroreport.DocumentId = Convert.ToInt64(_objCommand.Parameters["@eDocumentId"].Value);
                        }
                        _objCommand.Parameters.Clear();
                        _objCommand.Dispose();
                        _objCommand = null;

                        //Insert/Update in DocumentDetails
                        _objCommand = new SqlCommand("InUp_DocumentDetails", _objConnection);
                        _objCommand.CommandType = CommandType.StoredProcedure;
                        _objCommand.Transaction = _objSqlTransaction;

                        _objSqlParamater = new SqlParameter("@eDocumentDetailsId", SqlDbType.BigInt);
                        _objSqlParamater.Value = objclsSpiroreport.DocumentdtlId;
                        _objSqlParamater.Direction = ParameterDirection.InputOutput;
                        _objCommand.Parameters.Add(_objSqlParamater);

                        if (_objSqlParamater != null)
                        {
                            _objSqlParamater = null;
                        }

                        _objCommand.Parameters.Add("@eDocumentId", SqlDbType.BigInt).Value = objclsSpiroreport.DocumentId;
                        _objCommand.Parameters.Add("@dtDocumentModified", SqlDbType.DateTime).Value = objclsSpiroreport.dtDocumentModified;
                        _objCommand.Parameters.Add("@dWeightinKg", SqlDbType.Decimal).Value = objclsSpiroreport.Weightinkg;
                        _objCommand.Parameters.Add("@dHeightinCm", SqlDbType.Decimal).Value = objclsSpiroreport.Hieghtincms;
                        _objCommand.Parameters.Add("@nRaceCatCode", SqlDbType.BigInt).Value = objclsSpiroreport.RaceCode;
                        _objCommand.Parameters.Add("@bIsSmoker", SqlDbType.Bit).Value = objclsSpiroreport.IsSmoker;
                        _objCommand.Parameters.Add("@nCigarsPerDay", SqlDbType.Int).Value = objclsSpiroreport.NoOfCigarsperDay;
                        _objCommand.Parameters.Add("@nNoOfSmokingYears", SqlDbType.Int).Value = objclsSpiroreport.Noofsmokingyears;
                        _objCommand.Parameters.Add("@bQuitSmoking", SqlDbType.Bit).Value = objclsSpiroreport.Isquitsmoking;
                        _objCommand.Parameters.Add("@nNoOfQuitSmokingYears", SqlDbType.Int).Value = objclsSpiroreport.Noofquitsmokingyears;
                        _objCommand.Parameters.Add("@nVisitId", SqlDbType.BigInt).Value = objclsSpiroreport.VisitId_Dtl;
                        _objCommand.Parameters.Add("@sStatus", SqlDbType.VarChar).Value = objclsSpiroreport.Status;
                        _objCommand.Parameters.Add("@sInterpretation", SqlDbType.VarChar).Value = objclsSpiroreport.Interpretation;
                        _objCommand.Parameters.Add("@sDiagnosis", SqlDbType.VarChar).Value = objclsSpiroreport.Diagnosis;

                        _objCommand.ExecuteNonQuery();

                        if (_objCommand.Parameters["@eDocumentDetailsId"].Value != null)
                            objclsSpiroreport.DocumentdtlId = Convert.ToInt64(_objCommand.Parameters["@eDocumentDetailsId"].Value);

                        // Insert Update Document Stream
                        _objCommand.Parameters.Clear();
                        _objCommand.Dispose();
                        _objCommand = null;

                        _objCommand = new SqlCommand("InUp_eDocument", _objConnection);
                        _objCommand.CommandType = CommandType.StoredProcedure;
                        _objCommand.Transaction = _objSqlTransaction;
                        _objCommand.Parameters.Add("@eDocumentId", SqlDbType.BigInt).Value = objclsSpiroreport.DocumentId;
                        _objCommand.Parameters.Add("@eDocumentStream", SqlDbType.VarBinary).Value = DBNull.Value;

                        _objCommand.ExecuteNonQuery();

                        //Save file Operation through FileStream
                        if (SaveFile(objclsSpiroreport.DocumentId, objclsSpiroreport.DocStream, _objSqlTransaction, objclsSpiroreport.ClinicId))
                        {
                            _objSqlTransaction.Commit();
                        }
                        else
                        {
                            _objSqlTransaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), gstrMessageBoxCaption);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.saveSpiroreport() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _objSqlTransaction.Rollback();
            }
            finally
            {

                if (_objSqlParamater != null)
                {
                    _objSqlParamater = null;
                }

                if (objclsSpiroreport != null)
                {
                    objclsSpiroreport.Dispose();
                    objclsSpiroreport = null;
                }
                if (_objConnection != null)
                {
                    _objConnection.Dispose();
                    _objConnection = null;
                }
                if (_objCommand != null)
                {
                    _objCommand.Parameters.Clear();
                    _objCommand.Dispose();
                    _objCommand = null;
                }
                if (_objSqlTransaction != null)
                {
                    _objSqlTransaction.Dispose();
                    _objSqlTransaction = null;
                }
            }
        }
        private static bool SaveFile(Int64 DocumentId, byte[] buffer, SqlTransaction txn, long ClinicID)
        {
           // const int BlockSize = 1024 * 512;
            FileStream source = null;
            SafeFileHandle handle = null;
            FileStream dest = null;
            bool _blnres = false;
            //string _ErrorMessage = "";
            //bool _HasError = false;
            //bool _result = true;
            try
            {


                handle = GetOutputFileHandle(DocumentId, txn, ClinicID);
                dest = new FileStream(handle, FileAccess.Write);
                dest.Write(buffer, 0, buffer.Length);
                //source = new FileStream(filename, FileMode.Open, FileAccess.Read);
                //byte[] buffer = new byte[BlockSize];
                //int bytesRead;

                //while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                //{
                //    dest.Write(buffer, 0, bytesRead);
                //}
                dest.Flush(); //We cannot able to write 3K file problem of the app's teams
                _blnres = true;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.SaveFile() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _blnres = false;
                //_HasError = true;
                //_ErrorMessage = ex.Message;
                //_result = false;
                //if (_ErrorMessage.Trim() != "")
                //{
                //    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                //    //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                //    _MessageString = "";
                //}
            }
            finally
            {

                if (source != null)
                {
                    source.Close();
                    source.Dispose();
                }
                //We cannot able to write 3K file problem of the app's teams
                if (dest != null)
                {
                    dest.Close();
                    dest.Dispose();
                }
                if (handle != null)
                {
                    handle.Close();
                    handle.Dispose();
                }
            }
            return _blnres;
        }
        private static SafeFileHandle GetOutputFileHandle(Int64 DocumentId, SqlTransaction txn, long ClinicID)
        {
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            SafeFileHandle handle = null;
            //string _ErrorMessage = "";

            try
            {
                string GetOutputFileInfoCmd = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT(), eDocumentStream.PathName()"
                    + " FROM dbo.e_Document WITH(NOLOCK) "
                    + " WHERE eDocumentID = " + DocumentId;

                cmd = new SqlCommand(GetOutputFileInfoCmd, txn.Connection, txn);

                string filePath;
                byte[] txnToken;

                using (rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    rdr.Read();
                    txnToken = rdr.GetSqlBinary(0).Value;
                    filePath = rdr.GetSqlString(1).Value;
                    rdr.Close();
                }
                handle = Cls_NativeSqlClient.GetSqlFilestreamHandle(filePath, Cls_NativeSqlClient.DesiredAccess.Write, txnToken);
            }
            catch (Exception ex)
            {
                //  System.Windows.Forms.MessageBox.Show(ex.Message);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.GetOutputFileHandle() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
                //_ErrorMessage = ex.Message;

                //if (_ErrorMessage.Trim() != "")
                //{
                //    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                //    //gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                //    _MessageString = "";
                //}
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (rdr != null)
                {
                    rdr.Dispose();
                }
            }

            return handle;
        }

        //functionality for reading spiro report from database...


        //public bool ConvertBinaryToFileNew(string FilePath, ref byte[] buffer, int bytesRead, ref bool _isFirstTimeOpen)
        //{
        //    bool _result = true;
        //    Byte[] byteRead = null;
        //    FileStream oFile = null;
        //    try
        //    {
        //        if (buffer == null)
        //        {
        //            _result = false;
        //            //_ErrorMessage = "Error due to buffer is empty";
        //        }
        //        else
        //        {
        //            if (_isFirstTimeOpen == true)
        //            {
        //                oFile = new FileStream(FilePath, FileMode.Create);
        //                _isFirstTimeOpen = false;
        //            }
        //            else
        //            {
        //                if (System.IO.File.Exists(FilePath))
        //                {
        //                    oFile = new FileStream(FilePath, FileMode.Append);
        //                }

        //            }
        //        }
        //        if (oFile == null)
        //        {
        //            _result = false;
        //            _ErrorMessage = "Error file object is null";

        //        }
        //        if (_result == true)
        //        {
        //            using (BinaryWriter bw = new BinaryWriter(oFile))
        //            {
        //                bw.Write(buffer, 0, bytesRead);
        //                bw.Flush();
        //                bw.Close();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _HasError = true;
        //        _ErrorMessage = ex.Message;
        //        _result = false;
        //    }
        //    finally
        //    {
        //        if (oFile != null) { oFile.Close(); oFile.Dispose(); }
        //        //Code added on 4rd Octomber 2008 By - Sagar Ghodke
        //        //               //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }
        //        if (_result == false)
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
        //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";

        //        }
        //    }
        //    return _result;
        //}


        public byte[] GetContainerStream(Int64 DocumentID)
        {

            string _strSQL = String.Empty;
            String filePath = String.Empty;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlTransaction transaction = null;
            SqlFileStream serverStream = null;
            byte[] buffer = null;
            try
            {
                //sqlConnection = new SqlConnection("Data Source=DEV07\\SQLSERVER2008;Initial Catalog=gloDEVICE;Integrated Security=True");
                sqlConnection = new SqlConnection(strConnectionString);
                sqlCommand = new SqlCommand();

                _strSQL = " SELECT eDocumentStream.PathName() AS filePath"
                        + " FROM dbo.e_Document WITH(NOLOCK) WHERE "
                        + " eDocumentID = " + DocumentID;

                sqlCommand.CommandText = _strSQL;
                sqlCommand.Connection = sqlConnection;
                sqlConnection.Open();

                Object pathObj = sqlCommand.ExecuteScalar();
                if (DBNull.Value != pathObj)
                    filePath = Convert.ToString(pathObj);
               
                transaction = sqlConnection.BeginTransaction("mainTranaction");
                sqlCommand.Transaction = transaction;

                sqlCommand.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";

                Object obj = sqlCommand.ExecuteScalar();
                byte[] txContext = (byte[])obj;

                //using ()
                try
                {
                    serverStream = new SqlFileStream(filePath, txContext, FileAccess.Read);
                    buffer = new byte[serverStream.Length];
                    serverStream.Read(buffer, 0, (int)serverStream.Length);
                    serverStream.Flush(); 
                   
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.GetContainerStream() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    throw ex;
                }
                finally
                {

                    if (serverStream != null)
                    {
                        serverStream.Close();
                        serverStream.Dispose();
                        serverStream = null;
                    }
                }

                transaction.Commit();
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.GetContainerStream() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                transaction.Rollback();
            }
            finally
            {
                if (transaction != null)
                { transaction.Dispose(); transaction = null; }

                if (sqlCommand != null)
                { sqlCommand.Dispose(); sqlCommand = null; }

                if (sqlConnection != null)
                {
                    if (sqlConnection.State != ConnectionState.Closed) { sqlConnection.Close(); }
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }
            return buffer;
            #region "Prevoius Commented Code"

            //{
            //    //serverStream.Seek(0L, SeekOrigin.Begin);
            //    int currentIndex = 0;
            //    buffer = new byte[gBufferSize];
            //    int bytesRead;
            //    int block = 0;
            //    long maxBytesToRead = serverStream.Length;
            //    long currentBytesToRead = gBufferSize;
            //    if (serverStream.Length > 0)
            //    {
            //        currentIndex = 0;
            //        do
            //        {
            //            //serverStream.Seek(currentIndex, SeekOrigin.Begin);
            //            block = block + 1;
            //            if (currentBytesToRead > maxBytesToRead)
            //            {
            //                currentBytesToRead = maxBytesToRead;
            //            }
            //            bytesRead = serverStream.Read(buffer, 0, (int)currentBytesToRead);

            //            if (bytesRead > 0)
            //            {
            //                _result = ConvertBinaryToFileNew(FilePath, ref buffer, bytesRead, ref _isFirstTime);

            //                if (_result == false)
            //                {
            //                    _ErrorMessage = "Error in the converting Binary to New file";
            //                    break;
            //                }
            //                currentIndex += bytesRead;
            //                maxBytesToRead -= bytesRead;
            //            }

            //        } while (bytesRead == gBufferSize);
            //    }
            //    serverStream.Close();
            //}
            //transaction.Commit();
            #endregion "Prevoius Commented Code"
        }
        public gloEmdeonInterface.Classes.clsSpiroReports getSpiroObject(Int64 lngDocumentId)
        {
            gloEmdeonInterface.Classes.clsSpiroReports objSpiro = null;
            DataTable dtSpiroReportDetails = null;
            gloDatabaseLayer.DBLayer objDBLayer = null;
            try
            {
                objDBLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                string strQuery = @"SELECT     TOP (1) e_DocumentContainer.eDocumentId, e_DocumentContainer.sDocumentName, e_DocumentContainer.nPatientId, 
                                  e_DocumentContainer.eDocumentExtention, e_DocumentContainer.dtDocumentCreated, e_DocumentContainer.MachineId, 
                                  e_DocumentContainer.ClinicId, e_DocumentContainer.nVisitId, e_DocumentContainer.nReferenceId, e_DocumentContainer.nOrderById, 
                                  e_DocumentContainer.nUserID, e_DocumentContainer.sUserName, e_DocumentContainer.nReviewedById, e_DocumentContainer.sReviewer, 
                                  e_DocumentDetails.eDocumentDetailsId, e_DocumentDetails.dtDocumentModified, e_DocumentDetails.dWeightinKg, 
                                  e_DocumentDetails.dHeightinCm, e_DocumentDetails.nRaceCatCode, e_DocumentDetails.bIsSmoker, e_DocumentDetails.nCigarsPerDay, 
                                  e_DocumentDetails.bQuitSmoking, e_DocumentDetails.nNoOfSmokingYears, e_DocumentDetails.nNoOfQuitSmokingYears, 
                                  e_DocumentDetails.nVisitId AS dtlVisitId, e_DocumentDetails.sStatus, e_DocumentDetails.sDiagnosis, e_DocumentContainer.sOrderByType, 
                                  e_DocumentContainer.nTestOrderID, e_DocumentContainer.sTestOrderPrefix, e_DocumentContainer.sTestType, 
                                  e_DocumentContainer.sTestDetails, e_DocumentContainer.sTestSource
                                FROM       e_DocumentContainer INNER JOIN
                                           e_DocumentDetails ON e_DocumentContainer.eDocumentId = e_DocumentDetails.eDocumentId
                                           Where      e_DocumentContainer.eDocumentId=" + lngDocumentId;
                objDBLayer.Connect(false);
                objDBLayer.Retrive_Query(strQuery, out dtSpiroReportDetails);

                if ((dtSpiroReportDetails != null) && (dtSpiroReportDetails.Rows.Count > 0))
                {
                    objSpiro = new clsSpiroReports();
                    objSpiro.DocumentId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["eDocumentId"]);
                    objSpiro.Documentname = Convert.ToString(dtSpiroReportDetails.Rows[0]["sDocumentName"]);
                    objSpiro.PatientId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nPatientId"]);
                    objSpiro.DocumentExtension = Convert.ToString(dtSpiroReportDetails.Rows[0]["eDocumentExtention"]);
                    objSpiro.dtDocumentCreated = Convert.ToDateTime(dtSpiroReportDetails.Rows[0]["dtDocumentCreated"]);
                    objSpiro.MachineName = Convert.ToString(dtSpiroReportDetails.Rows[0]["MachineId"]);
                    objSpiro.ClinicId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["ClinicId"]);
                    objSpiro.VisitId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nVisitId"]);
                    objSpiro.RefernceId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nReferenceId"]);
                    objSpiro.OrderedById = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nOrderById"]);
                    objSpiro.UserId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nUserID"]);
                    objSpiro.UserName = Convert.ToString(dtSpiroReportDetails.Rows[0]["sUserName"]);
                    objSpiro.ReviewerId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nReviewedById"]);
                    objSpiro.Reviewer = Convert.ToString(dtSpiroReportDetails.Rows[0]["sReviewer"]);

                    objSpiro.DocumentdtlId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["eDocumentDetailsId"]);
                    objSpiro.dtDocumentModified = Convert.ToDateTime(dtSpiroReportDetails.Rows[0]["dtDocumentModified"]);
                    objSpiro.Weightinkg = Convert.ToDouble(dtSpiroReportDetails.Rows[0]["dWeightinKg"]);
                    objSpiro.Hieghtincms = Convert.ToDouble(dtSpiroReportDetails.Rows[0]["dHeightinCm"]);
                    objSpiro.RaceCode = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nRaceCatCode"]);
                    objSpiro.IsSmoker = Convert.ToBoolean(dtSpiroReportDetails.Rows[0]["bIsSmoker"]);
                    objSpiro.NoOfCigarsperDay = Convert.ToInt32(dtSpiroReportDetails.Rows[0]["nCigarsPerDay"]);
                    objSpiro.Noofsmokingyears = Convert.ToInt32(dtSpiroReportDetails.Rows[0]["nNoOfSmokingYears"]);
                    objSpiro.Isquitsmoking = Convert.ToBoolean(dtSpiroReportDetails.Rows[0]["bQuitSmoking"]);
                    objSpiro.Noofquitsmokingyears = Convert.ToInt32(dtSpiroReportDetails.Rows[0]["nNoOfQuitSmokingYears"]);
                    objSpiro.VisitId_Dtl = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["dtlVisitId"]);
                    objSpiro.Status = Convert.ToString(dtSpiroReportDetails.Rows[0]["sStatus"]);
                    objSpiro.Diagnosis = Convert.ToString(dtSpiroReportDetails.Rows[0]["sDiagnosis"]);
                    objSpiro.OrderByType = Convert.ToString(dtSpiroReportDetails.Rows[0]["sOrderByType"]);
                    objSpiro.TestOrderId = Convert.ToInt64(dtSpiroReportDetails.Rows[0]["nTestOrderID"]);
                    objSpiro.TestOrderprefix = Convert.ToString(dtSpiroReportDetails.Rows[0]["sTestOrderPrefix"]);
                    objSpiro.TestType = Convert.ToString(dtSpiroReportDetails.Rows[0]["sTestType"]);
                    objSpiro.TestDetails = Convert.ToString(dtSpiroReportDetails.Rows[0]["sTestDetails"]);
                    objSpiro.TestSource = Convert.ToString(dtSpiroReportDetails.Rows[0]["sTestSource"]);                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.getSpiroObject() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                objSpiro = null;

            }
            finally
            {
                if (dtSpiroReportDetails != null)
                {
                    dtSpiroReportDetails.Dispose();
                    dtSpiroReportDetails = null;
                }
                if (objDBLayer != null)
                {
                    objDBLayer.Dispose();
                    objDBLayer = null;
                }
                //if (objSpiro != null)
                //{
                //    objSpiro.Dispose();
                //    objSpiro = null;
                //}
            }
            return objSpiro;
        }
        /// <summary>
        /// Updates starus of report
        /// </summary>
        /// <param name="DucumentID"></param>
        /// <param name="StatusMsg"></param>
        public void updateStatus(long DucumentID, string StatusMsg)
        {
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            String _SqlQury = string.Empty;
            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                _SqlQury = "UPDATE  [dbo].[e_DocumentDetails] SET [sStatus] = '" + StatusMsg + "'  WHERE [eDocumentId] =" + DucumentID + "";
                _oDbLayer.Connect(false);
                _oDbLayer.Execute_Query(_SqlQury);
                _oDbLayer.Disconnect();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString(), "gloEMR", MessageBoxButtons.OK,MessageBoxIcon.Error);
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.updateStatus() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }
                _SqlQury = string.Empty;
            }
        }

        public void GetPatientSmokingHistory(long nPatientId,ref bool isSmoker,ref int nOfCigars,ref int nOfSmokingYears,ref bool isQuitSmoking,ref int noOfQuitYears)
        {
            string strquery = string.Empty;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            DataTable dtSmokeHistory = null;
            try
            {
                strquery = @"SELECT TOP(1)    ISNULL(e_DocumentDetails.bIsSmoker,0)AS bIsSmoker, ISNULL(e_DocumentDetails.nCigarsPerDay,0)AS nCigarsPerDay, ISNULL(e_DocumentDetails.nNoOfSmokingYears,0)AS nNoOfSmokingYears , ISNULL(e_DocumentDetails.bQuitSmoking,0)AS bQuitSmoking, 
                                                ISNULL( e_DocumentDetails.nNoOfQuitSmokingYears,0)AS nNoOfQuitSmokingYears
                            FROM         e_DocumentContainer INNER JOIN
                                                  e_DocumentDetails ON e_DocumentContainer.eDocumentId = e_DocumentDetails.eDocumentId
                            WHERE nPatientId =" + nPatientId +@"
                            ORDER BY e_DocumentContainer.dtDocumentCreated DESC";
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                oDBLayer.Connect(false );
                oDBLayer.Retrive_Query (strquery,out dtSmokeHistory);

                if ((dtSmokeHistory != null) && (dtSmokeHistory.Rows.Count>0))
                {
                    isSmoker = Convert.ToBoolean(dtSmokeHistory.Rows[0]["bIsSmoker"]);
                    nOfCigars = Convert.ToInt32(dtSmokeHistory.Rows[0]["nCigarsPerDay"]);
                    nOfSmokingYears = Convert.ToInt32(dtSmokeHistory.Rows[0]["nNoOfSmokingYears"]);
                    isQuitSmoking = Convert.ToBoolean(dtSmokeHistory.Rows[0]["bQuitSmoking"]);
                    noOfQuitYears = Convert.ToInt32(dtSmokeHistory.Rows[0]["nNoOfQuitSmokingYears"]);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.GetPatientSmokingHistory() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
             
            }
            finally
            {
                strquery = string.Empty;
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                if (dtSmokeHistory != null)
                {
                    dtSmokeHistory.Dispose();
                    dtSmokeHistory = null;
                }

            }
        }
        public String GetInterpretationFromReport(long eDocumentId)
        {
            gloDatabaseLayer.DBLayer oDbLayer = null;
            string strInterpretation = string.Empty;
            try
            {
                oDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                oDbLayer.Connect(false);
                strInterpretation = Convert.ToString(oDbLayer.ExecuteScalar_Query("SELECT sInterpretation FROM e_DocumentDetails where eDocumentId=" + eDocumentId));
                oDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                strInterpretation = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroReportmanager.GetInterpretationFromReport() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDbLayer != null)
                {
                    oDbLayer.Dispose();
                    oDbLayer = null;
                }
            }
            return strInterpretation;
        }


        #endregion "User Defined Functions"
    }

    /// <summary>
    /// Class used for Lowlevel I/O Operation
    /// </summary>
    class Cls_NativeSqlClient
    {
        public enum DesiredAccess : uint
        {
            Read,
            Write,
            ReadWrite,
        }

        [DllImport("sqlncli10.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern SafeFileHandle OpenSqlFilestream(
           string path,
           uint access,
           uint options,
           byte[] txnToken,
           uint txnTokenLength,
           Sql64 allocationSize);

        [StructLayout(LayoutKind.Sequential)]
        private struct Sql64
        {
            public Int64 QuadPart;
            public Sql64(Int64 quadPart)
            {
                this.QuadPart = quadPart;
            }
        }

        public static SafeFileHandle GetSqlFilestreamHandle(string filePath, DesiredAccess access, byte[] txnToken)
        {

            SafeFileHandle handle = OpenSqlFilestream(
               filePath,
               (uint)access,
               0,
              txnToken,
               (uint)txnToken.Length,
               new Sql64(0));

            return handle;

        }
    }
    /// <summary>
    /// Class contains general methods used for spirometry tests
    /// </summary>
    public static class clsSpiroGeneralModule
    {

        public static bool bMultipleRace { get; set;}

        /// <summary>
        /// Function used to generate 18 digit unique id using gsp_GetUniqueId
        /// </summary>
        /// <param name="strConnstring"></param>
        /// <returns></returns>
        public static long GetSpUniqueID(string strConnstring)
        {
            long _UniqueId = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            object objResult = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnstring);
                oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Clear();

                oDBParameters.Add("@ID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                oDBLayer.Connect(false);
                oDBLayer.Execute("gsp_GetUniqueID", oDBParameters, out objResult);

                oDBLayer.Disconnect();

                oDBParameters.Clear();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.GetSpUniqueID() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                //{ MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK); }
            }
            finally
            {
                if (oDBParameters != null)
                { oDBParameters.Clear(); oDBParameters.Dispose(); oDBParameters = null; }

                if (oDBLayer != null)
                { oDBLayer.Disconnect(); oDBLayer.Dispose(); oDBLayer = null; }

            }

            _UniqueId = Convert.ToInt64(objResult);

            return _UniqueId;
        }

        public static long getVisitID(DateTime VisitDate, Int64 PatientID, string strConnectionstring)
        {
            object objResult = null; ;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionstring);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Clear();
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtVisitdate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@AppointmentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                long Machinid = GetPrefixTransactionId(PatientID, strConnectionstring);
                oDBParameters.Add("@MachineID", Machinid, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@flag", 0, ParameterDirection.Output, SqlDbType.Int);
                oDBParameters.Add("@VisitID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                oDBLayer.Connect(false);
                oDBLayer.Execute("gsp_INSERTVISITS", oDBParameters, out objResult);
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.getVisitID() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                if (oDBParameters != null)
                { oDBParameters.Clear(); oDBParameters.Dispose(); oDBParameters = null; }

            }
            return Convert.ToInt64(objResult);
        }

        // function to get mchin id
        public static long GetPrefixTransactionId(long PatientId, String strConnectionString)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            System.Int64 _Result = 0;

            System.DateTime _PatientDOB = default(System.DateTime);
            System.DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");
            string strID1 = "";
            string strID2 = "";
            string strID3 = "";
            TimeSpan oTs = default(TimeSpan);
            object _internalresult = null;
            string _strSql = "";

            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Clear();
                _strSql = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientId;
                oDBLayer.Connect(false);
                _internalresult = oDBLayer.ExecuteScalar_Query(_strSql);
                if (_internalresult != null)
                {
                    if (string.IsNullOrEmpty(_internalresult.ToString()))
                    {
                        _PatientDOB = Convert.ToDateTime(_internalresult);
                    }
                }

                oTs = new TimeSpan();
                oTs = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTs.Days.ToString().Replace("-", "");

                oTs = new TimeSpan();
                oTs = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTs.TotalSeconds).ToString().Replace("-", "");

                oTs = new TimeSpan();
                oTs = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTs.Days.ToString().Replace("-", "");
                _Result = Convert.ToInt64(strID1) + Convert.ToInt64(strID2) + Convert.ToInt64(strID3);
                _Result = Convert.ToInt64(_Result);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.GetPrefixTransactionId() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

                if (oDBParameters != null)
                { oDBParameters.Clear(); oDBParameters.Dispose(); oDBParameters = null; }

            }
            return _Result;
        }
       
        public static string  RetriveDeviceCon(Int64 _ClinicId,string strEmrConnectionstring)
            {
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            String _SqlQury = string.Empty;
            String _strConnection = string.Empty;
            DataTable dtdeviceconnection = null;
            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(strEmrConnectionstring);
                //_SqlQury = "select Top 1 sSettingsValue from Settings where sSettingsName ='GLODEVICESERVERNAME' and nClinicID =" + _ClinicID + " union select Top 1 sSettingsValue from Settings where sSettingsName ='GLODEVICEDBNAME' and nClinicID =" + _ClinicID + "";
                _SqlQury = "select top 1 (select sSettingsValue from Settings where UPPER(sSettingsName)='GLODEVICESERVERNAME' AND nClinicID ="+_ClinicId+") As DatabaseServer,(select sSettingsValue from Settings where UPPER(sSettingsName)='GLODEVICEDBNAME' And nClinicID ="+_ClinicId+")As DatabaseName From Settings ";
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive_Query(_SqlQury, out dtdeviceconnection);
                _oDbLayer.Disconnect();
                if ((dtdeviceconnection != null) && (dtdeviceconnection.Rows.Count > 0))
                {
                    if ((Convert.ToString(dtdeviceconnection.Rows[0]["DatabaseServer"]) != null) && (Convert.ToString(dtdeviceconnection.Rows[0]["DatabaseServer"]).Length > 0))
                    {
                        if ((Convert.ToString(dtdeviceconnection.Rows[0]["DatabaseName"]) != null) && (Convert.ToString(dtdeviceconnection.Rows[0]["DatabaseName"]).Length > 0))
                        {
                            _strConnection = "Data Source=" + Convert.ToString(dtdeviceconnection.Rows[0]["DatabaseServer"]) + ";Initial Catalog=" + Convert.ToString(dtdeviceconnection.Rows[0]["DatabaseName"]) + ";Integrated Security=True";
                        }
                        else
                        {
                            _strConnection = string.Empty;
                        }
                    }
                    else
                    {
                        _strConnection = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "gloEMR", MessageBoxButtons.OK);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.RetriveDeviceCon() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                dtdeviceconnection = null;
                _strConnection = String.Empty;
            }
            finally
            {
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }
                if (dtdeviceconnection != null)
                {
                    dtdeviceconnection.Dispose();
                    dtdeviceconnection = null;
                }
                _SqlQury = string.Empty;
            }

            return _strConnection;
        }
        public static string GetProviderDetails(Int64 nPatientId, ref Int64 nProviderId, string strEMRConnectionstring)
        {
            DataTable dtProviderDtl=null;
            string strProviderName = string.Empty;
            string strQuery = @"select Provider_MST.nProviderID, dbo.GET_NAME(Provider_MST.sFirstName,Provider_MST.sMiddleName,Provider_MST.sLastName) As ProviderName
                            from Patient INNER JOIN Provider_MST ON Patient.nProviderID=Provider_MST.nProviderID WHERE Patient.nPatientID=" + nPatientId;
            gloDatabaseLayer.DBLayer oDblayer = null;
            try
            {
                oDblayer = new gloDatabaseLayer.DBLayer(strEMRConnectionstring);
                oDblayer.Connect(false);
                oDblayer.Retrive_Query(strQuery, out dtProviderDtl);
                if((dtProviderDtl !=null)&&(dtProviderDtl.Rows.Count > 0))
                {
                    nProviderId = Convert.ToInt64(dtProviderDtl.Rows[0]["nProviderID"]);
                    strProviderName = Convert.ToString(dtProviderDtl.Rows[0]["ProviderName"]);
                }
            }
            catch (Exception ex)
            {
                nProviderId = 0;
                strProviderName = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.GetProviderDetails() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDblayer != null)
                {
                    oDblayer.Dispose();
                    oDblayer = null;
                }
                if (dtProviderDtl != null)
                {
                    dtProviderDtl.Dispose();
                    dtProviderDtl = null;
                }
                strQuery = string.Empty;
            }
            return strProviderName;
        }
        public static string readSpiroDeviceprefix(string strEMRConnectionString)
        {
            string strSpiroDevicePrefix = String.Empty;
            gloDatabaseLayer.DBLayer oDbLayer = null;
            try
            {
                oDbLayer = new gloDatabaseLayer.DBLayer(strEMRConnectionString);
                oDbLayer.Connect(false);
                strSpiroDevicePrefix = Convert.ToString(oDbLayer.ExecuteScalar_Query("Select ISNULL(sSettingsValue,'') from Settings where sSettingsName='SPIROMETRYDEVICEORDERPREFIX'"));
                oDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                strSpiroDevicePrefix = String.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.readSpiroDeviceprefix() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDbLayer != null)
                {
                    oDbLayer.Dispose();
                    oDbLayer = null;
                }
            }
            return strSpiroDevicePrefix;

        }
        public static Int64 GetNewTestOrderID(Int64 nPatientId, string strgloDeviceKey)
        {
            Int64 nOrderPrefixId = 0;
            gloDatabaseLayer.DBLayer oDbLayer = null;
            try
            {
                oDbLayer = new gloDatabaseLayer.DBLayer(strgloDeviceKey);
                oDbLayer.Connect(false);
                nOrderPrefixId=Convert.ToInt64(oDbLayer.ExecuteScalar_Query("SELECT ISNULL(MAX(nTestOrderID),0)+1 FROM dbo.e_DocumentContainer WHERE nPatientId=" + nPatientId));
                oDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                nOrderPrefixId = 0;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.GetNewTestOrderID() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDbLayer != null)
                {
                    oDbLayer.Dispose();
                    oDbLayer = null;
                }
            }
            return nOrderPrefixId;
        }

    }
    public class clsSpiroVitalsManagement
    {
        #region "Constructor & Destructor"
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
        public clsSpiroVitalsManagement()
        {
        }
        ~clsSpiroVitalsManagement()
        {
            Dispose(false);
        }
        #endregion "Constructor & Destructor"

        #region "User Defined Functions"
        public DataTable RetriveHeightWeight(string gloConnectionString, long _nPatinet_ID)
        {
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            DataTable _GetPationtData = null;
            String _SqlQury = string.Empty;
            Int64 nVisitId = gloEmdeonInterface.Classes.clsSpiroGeneralModule.getVisitID(DateTime.Now, _nPatinet_ID, gloConnectionString);
            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(gloConnectionString);
                _GetPationtData = new DataTable();
                _SqlQury = "SELECT Top 1 nVisitID,  nVitalID, sHeight, dHeightinCm, dWeightinlbs, dWeightinKg FROM dbo.Vitals WHERE  nPatientID = " + _nPatinet_ID + " AND nVisitID=" + nVisitId + " Order by dtVitalDate DESC ";
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive_Query(_SqlQury, out _GetPationtData);
                _oDbLayer.Disconnect();

            }
            catch (Exception ex)
            { System.Windows.Forms.MessageBox.Show(ex.Message.ToString(), "gloEMR", MessageBoxButtons.OK,MessageBoxIcon.Error);
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroVitalsManagement.RetriveHeightWeight() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }
                _SqlQury = string.Empty;
            }

            return _GetPationtData;

        }










        public bool AddVital(long VisitID, ref long VitalID, long PatientID, DateTime VitalDate, string sHeight, double WeightInLbs, double WeightInKg, double HeightinInch, double HeightinCm, string WeightinLbsOz, string gloConnectionString)
        {
            bool _AddVital = false;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(gloConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                //oDBParameters.Add("@nVitalID ", VitalID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                //oDBParameters.Add("@nVisitID", VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@dtVitalDate ", VitalDate, ParameterDirection.Input, SqlDbType.DateTime);
                //oDBParameters.Add("@sHeight", sHeight, ParameterDirection.Input, SqlDbType.VarChar, 10);
                //oDBParameters.Add("@dWeightinlbs", WeightInLbs, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dWeightChange ", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dBMI", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dWeightinKg", WeightInKg, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dTemperature ",DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dRespiratoryRate", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dPulsePerMinute", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dPulseOx", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dBloodPressureSittingMin ", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dBloodPressureSittingMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dBloodPressureStandingMin", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dBloodPressureStandingMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@sComments", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                //oDBParameters.Add("@MachineID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@dHeadCircumferance", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dStature ", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dTHRperMin", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dTHRperMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dTHRMin", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dTHRMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dTHR", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dHeightinInch", HeightinInch, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dHeightinCm", HeightinCm, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@sWeightinLbsOz", WeightinLbsOz, ParameterDirection.Input, SqlDbType.VarChar, 20);

                //oDBParameters.Add("@dTemperatureinCelcius", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@nPainLevel", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@dPEFR1", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dPEFR2", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dPEFR3", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dHeadCircuminInch", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@dStatureinInch", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);
                //oDBParameters.Add("@sSiteForBloodPressure", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar, 50);
                //oDBParameters.Add("@dtLastMenstrualPeriod", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                //oDBParameters.Add("@dNeckCircuminCm", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                //oDBParameters.Add("@dNeckCircuminCm", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                //oDBParameters.Add("@dNeckCircuminInch",DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                //oDBParameters.Add("@dLeftEyePressure", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                //oDBParameters.Add("@dRightEyePressure", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                //oDBParameters.Add("@bStatusLMPeriod", DBNull.Value, ParameterDirection.Input, SqlDbType.Bit);
                //oDBParameters.Add("@stranuser", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar, 50);
                //oDBParameters.Add("@nPainLevelWithMedication", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@nPainLevelWithoutMedication", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@nPainLevelWorst", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@nODIPercent", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);

                oDBParameters.Add("@nVitalID", VitalID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nVisitID", VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtVitalDate", VitalDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@sHeight", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar, 10);
                //sHeight WeightInLbs
                oDBParameters.Add("@dWeightinlbs", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dWeightChange", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dBMI", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dWeightinKg", WeightInKg, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dTemperature", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dRespiratoryRate", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dPulsePerMinute", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dPulseOx", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dBloodPressureSittingMin", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dBloodPressureSittingMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dBloodPressureStandingMin", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dBloodPressureStandingMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@sComments", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar, 255);
                long _machineid = 0;
                _machineid = gloEmdeonInterface.Classes.clsSpiroGeneralModule.GetPrefixTransactionId(PatientID, gloConnectionString);
                oDBParameters.Add("@MachineID", _machineid, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dHeadCircumferance", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dStature", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dTHRperMin", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dTHRperMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dTHRMin", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dTHRMax", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dTHR", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dHeightinInch", HeightinInch, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dHeightinCm", HeightinCm, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@sWeightinLbsOz", WeightinLbsOz, ParameterDirection.Input, SqlDbType.VarChar, 20);
                oDBParameters.Add("@dTemperatureinCelcius", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@nPainLevel", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dPEFR1", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dPEFR2", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dPEFR3", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dHeadCircuminInch", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dStatureinInch", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@sSiteForBloodPressure", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@dtLastMenstrualPeriod", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@dNeckCircuminCm", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dNeckCircuminInch", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dLeftEyePressure", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@dRightEyePressure", DBNull.Value, ParameterDirection.Input, SqlDbType.Float);
                oDBParameters.Add("@bStatusLMPeriod", 0, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@stranuser", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@nPainLevelWithMedication", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPainLevelWithoutMedication", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPainLevelWorst", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nODIPercent", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt);
                oDBLayer.Connect(false);
                oDBLayer.Execute("gsp_InUpVitals", oDBParameters);
                oDBLayer.Disconnect();
                VitalID = Convert.ToInt64(oDBParameters[0].Value);

                _AddVital = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroVitalsManagement.AddVital() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }

            return _AddVital;
        }
        #endregion "User Defined Functions"
    }

    public class clsCategoryMST : IDisposable
    {
        //@nCategoryId
        private long _categoryID;
        public long CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        //@sCategoryCode
        private string _categoryCode;
        public string CategoryCode
        {
            get { return _categoryCode; }
            set { _categoryCode = value; }
        }

        //@sCategoryName
        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        //@sCategoryType
        private string _categoryType;
        public string CategoryType
        {
            get { return _categoryType; }
            set { _categoryType = value; }
        }

        //@nClinicID
        private long _ClinicID;
        public long ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }



        #region "Constructor & Destructor"
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
        public clsCategoryMST(string connectionString)
        {
            _strConnectionString = connectionString;

        }
        ~clsCategoryMST()
        {
            Dispose(false);
        }

        #endregion "Constructor & Destructor"
        public string ConnectionString
        {
            get { return _strConnectionString; }
            set { _strConnectionString = value; }
        }
        string _strConnectionString = string.Empty;

        /// <summary>
        /// Function used to add spiromtry race in Category mst
        /// </summary>
        /// <param name="oclsCategoryMST"></param>
        /// <returns></returns>
        public long Add(clsCategoryMST oclsCategoryMST)
        {
            long _returnCatId = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nCategoryId", oclsCategoryMST.CategoryID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@sCategoryCode", oclsCategoryMST.CategoryCode, ParameterDirection.Input, SqlDbType.VarChar, 55);
                oDBParameters.Add("@sCategoryName", oclsCategoryMST.CategoryName, ParameterDirection.Input, SqlDbType.VarChar, 255);
                // oDBParameters.Add("@sCategoryDesc", sCategoryDesc, ParameterDirection.Input, SqlDbType.VarChar, 512);
                oDBParameters.Add("@nClinicID", oclsCategoryMST.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsBlocked", false, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@sCategoryType", oclsCategoryMST.CategoryType, ParameterDirection.Input, SqlDbType.VarChar, 50);
                //oDBParameters.Add("@nParentID", nParentID, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@sSystemCode", sSystemCode, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oDBLayer.Connect(false);
                oDBLayer.Execute("INUP_CategoryMST", oDBParameters);
                _returnCatId = Convert.ToInt64(oDBParameters[0].Value);
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error to retrive recored  " + ex.ToString());
                //obj.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.Add() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _returnCatId = 0;
            }
            finally
            {
                if (oDBLayer != null)
                { oDBLayer.Disconnect(); oDBLayer.Dispose(); oDBLayer = null; }

                if (oDBParameters != null)
                { oDBParameters.Clear(); oDBParameters.Dispose(); oDBParameters = null; }

            }
            return _returnCatId;
        }

       // public void isExistsCategoryName() { throw new Exception("Not Implemented."); }

        /// <summary>
        /// Function checks whether category raceName already present in database
        /// </summary>
        /// <param name="raceName"></param>
        /// <returns></returns>
        public bool isExistsCategoryName(string raceName, long CategoryID)
        {
            bool _retBoolean = false;
            long _retVal = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string _SqlQry = string.Empty;
            try
            {
                //_SqlQry = "SELECT  COUNT(nCategoryId) AS nCategoryId FROM  dbo.e_CategoryMST WHERE sCategoryName = '" + raceName.Trim().Replace("'", "''") + "'";
                if (CategoryID == 0)
                {
                    _SqlQry = "SELECT  COUNT(nCategoryId) AS nCategoryId FROM  dbo.e_CategoryMST WHERE sCategoryName = '" + raceName.Trim().Replace("'", "''") + "'";
                }
                else
                {
                    _SqlQry = "SELECT  COUNT(nCategoryId) AS nCategoryId FROM  dbo.e_CategoryMST WHERE sCategoryName = '" + raceName.Trim().Replace("'", "''") + "' and nCategoryId <> " + CategoryID + "";
                }
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBLayer.Connect(false);
                _retVal = Convert.ToInt32(oDBLayer.ExecuteScalar_Query(_SqlQry));
                if (_retVal > 0)
                {
                    _retBoolean = true;
                }
            }
            catch (Exception ex)
            { _retBoolean = false;
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.isExistsCategoryName() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                { oDBLayer.Disconnect(); oDBLayer.Dispose(); oDBLayer = null; }

                _SqlQry = string.Empty;
                _retVal = 0;

            }
            return _retBoolean;
        }

        /// <summary>
        /// Code added by RK to check if Race is duplicate on 20110530
        /// </summary>
        /// <param name="raceCode"></param>
        /// <param name="raceName"></param>
        /// <returns></returns>
        public bool isExistsCategoryCode(string raceCode)
        {
            bool _retBoolean = false;
            long _retVal = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string _SqlQry = string.Empty;
            try
            {
              //  _SqlQry = "SELECT  COUNT(nCategoryId) FROM  dbo.e_CategoryMST WHERE sCategoryCode = '" + raceCode.Trim().Replace("'", "''") + "' or sCategoryName='" + raceName.Trim().Replace("'", "''") + "'";
                _SqlQry = "SELECT  COUNT(nCategoryId) FROM  dbo.e_CategoryMST WHERE sCategoryCode = '" + raceCode.Trim().Replace("'", "''") + "'";
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBLayer.Connect(false);
                _retVal = Convert.ToInt32(oDBLayer.ExecuteScalar_Query(_SqlQry));
                if (_retVal > 0)
                {
                    _retBoolean = true;
                }
            }
            catch (Exception ex)
            { _retBoolean = false;
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.isExistsCategoryCode() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                { oDBLayer.Disconnect(); oDBLayer.Dispose(); oDBLayer = null; }

                _SqlQry = string.Empty;
                _retVal = 0;

            }
            return _retBoolean;
        }
        //End of Code added by RK to check if Race is duplicate on 20110530


















        /// <summary>
        /// Function used to retrieve information of specific spirometry race
        /// </summary>
        /// <param name="SpiroRaceName"></param>
        /// <param name="_CategoryID"></param>
        /// <returns></returns>
        /// 
        public String RetriveSpiroRace(out string SpiroRaceName, Int64 _CategoryID)
        {
            DataTable _RetriveSpiroRace = null;
            SpiroRaceName = String.Empty;
            string _strSpiroRaceCode = String.Empty;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nCategoryId", _CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCategoryType", "SPIRORACE", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBLayer.Connect(false);
                oDBLayer.Retrive("Get_CategoryMST", oDBParameters, out _RetriveSpiroRace);
                oDBLayer.Disconnect();
                if ((_RetriveSpiroRace != null) && (_RetriveSpiroRace.Rows.Count > 0))
                {
                    _strSpiroRaceCode = Convert.ToString(_RetriveSpiroRace.Rows[0][1]);
                    SpiroRaceName = Convert.ToString(_RetriveSpiroRace.Rows[0][2]);
                }
            }
            catch (Exception ex)
            {
                _strSpiroRaceCode = String.Empty;
                SpiroRaceName = String.Empty;
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error in retrive record  " + ex.ToString());
                //obj.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.RetriveSpiroRace(out string SpiroRaceName, Int64 _CategoryID) " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);


            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (_RetriveSpiroRace != null)
                {
                    _RetriveSpiroRace.Dispose();
                    _RetriveSpiroRace = null;
                }
            }
            return _strSpiroRaceCode;
        }
        /// <summary>
        /// Function to retrieve all spirometry races
        /// </summary>
        /// <returns></returns>
        public DataTable RetriveSpiroRace()
        {
            DataTable _RetriveSpiroRace = null;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nCategoryId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCategoryType", "SPIRORACE", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBLayer.Connect(false);
                oDBLayer.Retrive("Get_CategoryMST", oDBParameters, out _RetriveSpiroRace);
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error to retrive Recored  " + ex.ToString());
                //obj.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.RetriveSpiroRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);


            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
            return _RetriveSpiroRace;
        }
        /// <summary>
        /// Function for removing the entry from category mst
        /// </summary>
        /// <param name="CategoryID"></param>
        public void DeleteSpiroRace(long CategoryID)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nCategoryId", CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBLayer.Connect(false);
                oDBLayer.Execute("Remove_CategoryMST", oDBParameters);
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Deleteing Recored  " + ex.ToString());
                //obj.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.DeleteSpiroRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);


            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
        }
        public bool DeleteMappedRace(long RaceID)
        {
            bool _DeleteRaceFromMapeedRace = false;
            gloDatabaseLayer.DBLayer oDBlayer = null;
            string SqlQry = string.Empty;
            try
            {
                SqlQry = "DELETE FROM [dbo].[e_CategoryMapping] WHERE eCategoryId= " + RaceID + "";
                oDBlayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBlayer.Connect(false);
                oDBlayer.Execute_Query(SqlQry);
                oDBlayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error While Deleting Mapped Device Race" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                if (oDBlayer != null)
                {
                    oDBlayer.Dispose();
                    oDBlayer = null;
                }
                SqlQry = string.Empty;
            }
            return _DeleteRaceFromMapeedRace;
        }
        /// <summary>
        /// Function used to check whether spiro race is mapped  with EMR race
        /// </summary>
        /// <param name="RaceID"></param>
        /// <returns></returns>
        public bool IsMapped(long RaceID)
        {
            Int32 MappedRace = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string _SqlQry = string.Empty;
            try
            {
                _SqlQry = "SELECT COUNT(eCategoryId) AS Expr1  FROM  dbo.e_CategoryMapping where eCategoryId = " + RaceID + "";
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBLayer.Connect(false);
                MappedRace = Convert.ToInt32(oDBLayer.ExecuteScalar_Query(_SqlQry));
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                MappedRace = 0;
                // MessageBox.Show(ex.Message.ToString(), "gloEMR", MessageBoxButtons.OK);
                //gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                //obj.UpdateLog("Error In Deleteing Recored  " + ex.ToString());
                //obj.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.IsMapped() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

                _SqlQry = string.Empty;
            }

            if (MappedRace == 0)
                return false;
            else
                return true;

        }
        /// <summary>
        /// Get corrresponding spiro race code for EMR race id
        /// </summary>
        /// <param name="gloEMRRaceID"></param>
        /// <returns></returns>
        public string GetMappedSpiroRaceID(long gloEMRRaceID)
        {
            string _GetMappedSpiroRaceID = string.Empty;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string SqlQry = string.Empty;
            try
            {
                SqlQry = "select sCategoryCode from e_CategoryMST inner join e_CategoryMapping on e_CategoryMST.nCategoryId = e_CategoryMapping.eCategoryId  where e_CategoryMapping.ngloCategoryId = " + gloEMRRaceID + " ";
                oDBLayer = new gloDatabaseLayer.DBLayer(_strConnectionString);
                oDBLayer.Connect(false);
                _GetMappedSpiroRaceID = Convert.ToString(oDBLayer.ExecuteScalar_Query(SqlQry));
                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK,MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.GetMappedSpiroRaceID() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;

                }
                SqlQry = string.Empty;
            }

            return _GetMappedSpiroRaceID;
        }


        /// <summary>
        /// Function to retrieve all EMR race
        ///  </summary>
        /// <returns></returns>
        public DataTable RetrieveEMRRace()
        {
            DataTable _LoadEMRRace = null;
            string _SqlQry = string.Empty;
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            try
            {
                _SqlQry = "SELECT nCategoryID, sDescription FROM dbo.Category_MST WHERE sCategoryType IN ('Race','Race Specification')";
                 if (clsSpiroGeneralModule.bMultipleRace)
                     _SqlQry = _SqlQry + " UNION SELECT -1 ,'Declined to specify'";
                 _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive_Query(_SqlQry, out _LoadEMRRace);
                _oDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.RetrieveEMRRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                _SqlQry = string.Empty;
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }

            }
            return _LoadEMRRace;
        }



        /// <summary>
        /// Function to retrieve all mapped race
        ///  </summary>
        /// <returns></returns>
        public DataTable RetiriveMappedRace()
        {
            DataTable _RetiriveMappedRace = null;
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive("Get_MappedRace", out _RetiriveMappedRace);
                _oDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.RetiriveMappedRace() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }

            }
            return _RetiriveMappedRace;
        }



        /// <summary>
        /// funcaion used to delete mapping race
        /// </summary>
        /// <param name="gloEMRRaceID"></param>
        /// <param name="SpiroRaceID"></param>
        /// <returns></returns>
        public bool DeleteMapping(long EMRraceId, long SpiroRaceID)
        {
            bool _DeleteMapping = false;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBLayer.Connect(false);
                oDBParameters.Add("@eCategoryId", SpiroRaceID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ngloCategoryId", EMRraceId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBLayer.Execute("Remove_CategoryMapping", oDBParameters);
                oDBLayer.Disconnect();
                _DeleteMapping = true;

            }
            catch (Exception ex) 
            { _DeleteMapping = false;
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.DeleteMapping() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

            }
            return _DeleteMapping;

        }



        /// <summary>
        /// Save mapping race
        /// </summary>
        /// <param name="gloEMRRaceID"></param>
        /// <param name="SpiroRaceID"></param>
        /// <returns></returns>
        public void SaveMapping(long EMRRaceID, long SpiroRaceID)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@eCategoryId", SpiroRaceID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ngloCategoryId", EMRRaceID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBLayer.Connect(false);
                oDBLayer.Execute("INUP_CategoryMapping", oDBParameters);
                oDBLayer.Disconnect();
            }
            catch (Exception ex) 
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsCategoryMST.SaveMapping() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

            }
        }
    }

    public class clsSpiroTestMst : IDisposable
    {

        #region "Constructor and Destructor"

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

        public clsSpiroTestMst()
        {

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            {
                gstrMessageBoxCaption = "gloEMR";
            }

        }

        ~clsSpiroTestMst()
        {
            Dispose(false);
        }


        #endregion "Constructor and Destructor"

        #region "Declaration"
        private String gstrMessageBoxCaption = String.Empty;
        private String _strConnectionString = string.Empty;
        #endregion "Declaration"

        #region "Property"

        public string ConnectionString
        {
            get { return _strConnectionString; }
            set { _strConnectionString = value; }
        }






        #endregion "Property"


        #region "Function && Method"





        ////funcation retrive data of conducted test against this patient
        //public DataTable RetriveTestData(long PatientID, DateTime FromDate, DateTime ToDate, string TestType)
        //{
        //    DataTable _GetTestData = null;
        //    gloDatabaseLayer.DBLayer _oDbLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null;

        //    try
        //    {
        //        _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
        //        oDBParameters = new gloDatabaseLayer.DBParameters();
        //        oDBParameters.Add("@PatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDBParameters.Add("@FromDate", FromDate, ParameterDirection.Input, SqlDbType.Date);
        //        oDBParameters.Add("@ToDate", ToDate, ParameterDirection.Input, SqlDbType.Date);
        //        oDBParameters.Add("@TestType", TestType, ParameterDirection.Input, SqlDbType.VarChar);
        //        _oDbLayer.Connect(false);
        //        _oDbLayer.Retrive("Get_TestData", oDBParameters, out _GetTestData);
        //        _oDbLayer.Disconnect();
        //    }
        //    catch (Exception ex)
        //    { MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //    finally
        //    {
        //        if (_oDbLayer != null)
        //        {
        //            _oDbLayer.Dispose();
        //            _oDbLayer = null;
        //        }
        //    }
        //    return _GetTestData;
        //}

        //funcation retrive data of conducted test against this patient
        public DataTable RetriveTestData(long PatientID,bool UseDateRange, DateTime FromDate, DateTime ToDate,string TestType,long FromRange,long TORange)
        {
            DataTable _GetTestData = null;
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@PatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                if (UseDateRange)
                {
                    oDBParameters.Add("@FromDate", FromDate.Date, ParameterDirection.Input, SqlDbType.Date);
                    oDBParameters.Add("@ToDate", ToDate.Date, ParameterDirection.Input, SqlDbType.Date);
                }
                oDBParameters.Add("@TestType", TestType.Trim() , ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@FromRange", FromRange, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ToRange", TORange, ParameterDirection.Input, SqlDbType.BigInt);
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive("Get_TestData", oDBParameters, out _GetTestData);
                _oDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.RetriveTestData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;

                }
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }

            }
            return _GetTestData;
        }


        public Int32 RetriveTestDataCount(long PatientID, bool UseDateRange, DateTime FromDate, DateTime ToDate,String TestType)
        {
            Int32 _RetriveTestDataCount = 0;
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@PatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                if (UseDateRange)
                {
                    oDBParameters.Add("@FromDate", FromDate.Date, ParameterDirection.Input, SqlDbType.Date);
                    oDBParameters.Add("@ToDate", ToDate.Date, ParameterDirection.Input, SqlDbType.Date);
                }
                oDBParameters.Add("@TestType", TestType.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                _oDbLayer.Connect(false);
                Int32.TryParse(Convert.ToString(_oDbLayer.ExecuteScalar("Get_TestDataCount", oDBParameters)), out _RetriveTestDataCount); 
                _oDbLayer.Disconnect(); 
            }
            catch (Exception ex)
            { _RetriveTestDataCount = 0;
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.RetriveTestDataCount() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
           finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;

                }
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }

            }





            return _RetriveTestDataCount;

        }


        public string RetrivePagingSize(long ClinicID)
        {
            string _GetPagingValue = string.Empty;
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            String _SqlQury = string.Empty;
            try
            {
                _SqlQury = "SELECT [sSettingsValue] FROM [dbo].[Settings] where [sSettingsName]='SpiroUserPagingSize' and  [nClinicID]=" + ClinicID + " ";
                _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                _oDbLayer.Connect(false);
                _GetPagingValue = Convert.ToString(_oDbLayer.ExecuteScalar_Query(_SqlQury));
                _oDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                _GetPagingValue = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.RetrivePagingSize() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);


            }
            finally
            {
                _SqlQury = string.Empty;

                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null; 
                }
            }
            return _GetPagingValue;
        }

        // funaction to retrive orderby name from glo datatabse 
        public string GetOrdredBy(long OrederedID, string OrederedType)
        {

            string _GetReviewdBy = string.Empty;
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            String _SqlQury = string.Empty;
            try
            {
                if (OrederedType.Trim().ToUpper() == "Refferal".ToUpper())
                {
                    _SqlQury = "SELECT ISNULL(Contacts_MST.sFirstName, '') + ' ' + ISNULL(Contacts_MST.sMiddleName,'') + ' ' +ISNULL(Contacts_MST.sLastName,'') AS sLastName FROM Contacts_MST LEFT OUTER JOIN Contacts_Physician_DTL ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID WHERE(Contacts_MST.bIsBlocked = 0 OR Contacts_MST.bIsBlocked IS NULL) AND (Contacts_MST.sContactType = 'Physician') and Contacts_MST.nContactID= " + OrederedID + "";
                }
                else if (OrederedType.Trim().ToUpper() == "Provider".ToUpper())
                {
                    _SqlQury = "SELECT  ISNULL(sFirstName,'') + ' '+ ISNULL(Provider_MST.sMiddleName,'')  + ' ' + ISNULL(sLastName,'') From Provider_MST WHERE  bIsblocked='FALSE' and nProviderID= " + OrederedID + "";
                }
                else
                {
                    return string.Empty;
                }
                _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                _oDbLayer.Connect(false);
                _GetReviewdBy = Convert.ToString(_oDbLayer.ExecuteScalar_Query(_SqlQury));
                _oDbLayer.Disconnect();
            }
            catch (Exception ex)
            { _GetReviewdBy = string.Empty;
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.GetOrdredBy() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }
                _SqlQury = string.Empty;
            }
            return _GetReviewdBy;
        }


        // function to retrive patient information
        public DataTable GetPationtData(long _Patient_ID)
        {
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            DataTable _GetPationtData = null;
            //String _SqlQury = string.Empty;
            gloDatabaseLayer.DBParameters _oDbParametors = null;
            try
            {
                //_oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                //_GetPationtData = new DataTable();
                //_SqlQury = "SELECT dbo.Patient.sPatientCode,dbo.Patient.sLastName,dbo.Patient.sFirstName,dbo.Patient.sMiddleName , CONVERT( varchar(10),dbo.Patient.dtDOB,101)AS dtDOB , CASE dbo.Patient.sGender WHEN 'Male' THEN 1 WHEN 'Female' THEN 2 ELSE 0 END AS Gender,sRace as gloEMRRace, isnull((select top 1 nCategoryID from Category_MST where sCategoryType='Race' and sDescription=RTRIM(LTRIM(sRace))),0) as gloEMRRaceID From  dbo.Patient WHERE dbo.Patient.nPatientID = " + _Patient_ID + " ";
                //_oDbLayer.Connect(false);
                //_oDbLayer.Retrive_Query(_SqlQury, out _GetPationtData);
                //_oDbLayer.Disconnect();

                _GetPationtData = new DataTable();
                _oDbParametors = new gloDatabaseLayer.DBParameters();
                _oDbParametors.Add("@nPatientID", _Patient_ID, ParameterDirection.Input, SqlDbType.BigInt);
                _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                _oDbLayer.Connect(false);
                _oDbLayer.Retrive("dbo.SpiroDevice_GetPatientData", _oDbParametors, out _GetPationtData);
                if ((!clsSpiroGeneralModule.bMultipleRace) && (_GetPationtData != null && _GetPationtData.Rows.Count > 0))
                {
                    if (string.Compare( Convert.ToString(_GetPationtData.Rows[0]["gloEMRRace"]), "Declined To specify", true) == 0)
                    {
                        _GetPationtData.Rows[0]["gloEMRRace"] = "";
                        _GetPationtData.Rows[0]["gloEMRRaceID"] = 0;
                    }
                }
                _oDbLayer.Disconnect();

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.GetPationtData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (_oDbParametors != null)
                {
                    _oDbParametors.Dispose();
                    _oDbParametors = null;                        
                }
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }
               // _SqlQury = string.Empty;
            }

            return _GetPationtData;

        }



        public void UpdateInterpretation(long DocumentID, string Interpretation)
        {
            gloDatabaseLayer.DBLayer _oDbLayer = null;
            String _SqlQury = string.Empty;
            try
            {
                _oDbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                _SqlQury = "UPDATE  [dbo].[e_DocumentDetails] SET [sInterpretation] = '" + Interpretation.Replace("'","''")  + "'  WHERE [eDocumentId] =" + DocumentID + "";
                _oDbLayer.Connect(false);
                _oDbLayer.Execute_Query(_SqlQury);
                _oDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.UpdateInterpretation() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (_oDbLayer != null)
                {
                    _oDbLayer.Dispose();
                    _oDbLayer = null;
                }
                _SqlQury = string.Empty;
            }



        }

        #endregion "Function && Method"







    }

}
