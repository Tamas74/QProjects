using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using gloEDocumentV3;

namespace gloEmdeonInterface.Forms
{
    public partial class frmWelchAllynECG_Local : Form
    {
        private string sRemoteWelchAllynECGPath = @"\\tsclient\Y\ForWelchAllynECG"; //"\\tsclient\Y\ForWelchAllynECG"    "Y:\\ForWelchAllynECG"
        private System.Windows.Forms.Timer Timer = null;

        private FrmWelChallynECG.TestType _TestType;
        private Int64 nPatientID = 0;
        private string sTestID = null;
        private Int64 nUserID = 0;
        private Int64 nClinicID = 0;
        private string sConnectionString = null;
        private Int64 _nECGID = 0;

        private Int64 nDMSCategoryID = 0;
        private string sDMSCategory = "WelchAllyn ECG";

        public Int64 nECGID { get; set; }
        public bool IsECGTestDataChanged { get; set; }

        public frmWelchAllynECG_Local(FrmWelChallynECG.TestType TestType, Int64 PatientID, string TestID, Int64 LoginUserId, Int64 ClinicID, string GloEMRConnectionString, Int64 ECGID)
        {
            InitializeComponent();

            _TestType = TestType;
            nPatientID = PatientID;
            sTestID = TestID;
            nUserID = LoginUserId;
            nClinicID = ClinicID;
            sConnectionString = GloEMRConnectionString;
            _nECGID = ECGID;
        }

        private void frmWelchAllynECG_Local_Load(object sender, EventArgs e)
        {
            try
            {
                GetDMSCategory();
                RequestRemoteWelchAllynECG();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
        }

        private void RequestRemoteWelchAllynECG()
        {
            ClsWelchAllynECGLayer ObjWelchallynlayer = null;
            DataTable dtGetPatientdemogrphicData = null;
            WelchAllynECGRequest oWelchAllynECGRequest = null;
            PatientECGDetails oPatientECGDetails = null;
            DateTime BirthDate = default(DateTime);
            string DeviceUser = String.Empty;
            string DevicePassword = String.Empty;

            try
            {
                ObjWelchallynlayer = new ClsWelchAllynECGLayer(sConnectionString);
                dtGetPatientdemogrphicData = ObjWelchallynlayer.WA_GetPationtDemograpicData(nPatientID);

                if (dtGetPatientdemogrphicData != null && dtGetPatientdemogrphicData.Rows.Count > 0)
                {
                    oWelchAllynECGRequest = new WelchAllynECGRequest();

                    oWelchAllynECGRequest.LastName = dtGetPatientdemogrphicData.Rows[0]["sLastName"].ToString();
                    oWelchAllynECGRequest.MiddleName = dtGetPatientdemogrphicData.Rows[0]["sMiddleName"].ToString();
                    oWelchAllynECGRequest.FirstName = dtGetPatientdemogrphicData.Rows[0]["sFirstName"].ToString();

                    DateTime.TryParse(dtGetPatientdemogrphicData.Rows[0]["dtDOB"].ToString(), out BirthDate);
                    oWelchAllynECGRequest.BirthDate = BirthDate;

                    oWelchAllynECGRequest.Gender = dtGetPatientdemogrphicData.Rows[0]["sGender"].ToString();

                    oWelchAllynECGRequest.PatientCode = ObjWelchallynlayer.Retrive_ExternalPatientCode(nPatientID);
                    oWelchAllynECGRequest.PatientID = nPatientID;

                    if (string.IsNullOrEmpty(oWelchAllynECGRequest.PatientCode))
                    {
                        oWelchAllynECGRequest.PatientCode = dtGetPatientdemogrphicData.Rows[0]["sPatientCode"].ToString();

                        if (!ObjWelchallynlayer.InsertExternalID(nPatientID, oWelchAllynECGRequest.PatientCode))
                        {
                            return;
                        }
                    }

                    ObjWelchallynlayer.Get_LoginDetails(nUserID, ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice, out DeviceUser, out DevicePassword);
                    oWelchAllynECGRequest.DeviceUser = DeviceUser;

                    if (!string.IsNullOrEmpty(DevicePassword))
                    {
                        gloSecurity.ClsEncryption oEncryption = new gloSecurity.ClsEncryption();
                        oWelchAllynECGRequest.DevicePassword = oEncryption.EncryptToBase64String(DevicePassword, "12345678");
                        oEncryption.Dispose();
                        oEncryption = null;
                    }

                    oWelchAllynECGRequest.TestType = _TestType;

                    if (_TestType == FrmWelChallynECG.TestType.GetAlltest)
                    {
                        DataTable dtECGID = ClsWelchAllynECGLayerGenral.Retrive_ECGID(nClinicID, nPatientID, sConnectionString);

                        if (dtECGID != null && dtECGID.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtECGID.Rows.Count; i++)
                            {
                                oPatientECGDetails = new PatientECGDetails();

                                oPatientECGDetails.ECGID = Convert.ToInt64(dtECGID.Rows[i]["nECGID"]);
                                oPatientECGDetails.TestId = Convert.ToString(dtECGID.Rows[i]["sTestId"]);

                                oWelchAllynECGRequest.PatientECGCollection.Add(oPatientECGDetails);
                            }
                        }

                        if (dtECGID != null)
                        {
                            dtECGID.Dispose();
                            dtECGID = null;
                        }
                    }
                    else
                    {
                        oPatientECGDetails = new PatientECGDetails();

                        oPatientECGDetails.ECGID = _nECGID;
                        oPatientECGDetails.TestId = sTestID;

                        oWelchAllynECGRequest.PatientECGCollection.Add(oPatientECGDetails);
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = Int32.MaxValue;

                    string sWelchAllynECGRequest = serializer.Serialize(oWelchAllynECGRequest);

                    serializer = null;

                    if (!string.IsNullOrEmpty(sWelchAllynECGRequest))
                    {
                        if (!Directory.Exists(sRemoteWelchAllynECGPath + @"\Inbox"))
                        {
                            Directory.CreateDirectory(sRemoteWelchAllynECGPath + @"\Inbox");
                        }

                        if (File.Exists(sRemoteWelchAllynECGPath + @"\Inbox" + @"\WelchAllynECGRequest.txt"))
                        {
                            try
                            {
                                File.Delete(sRemoteWelchAllynECGPath + @"\Inbox" + @"\WelchAllynECGRequest.txt");
                            }
                            catch (Exception)
                            { }
                        }

                        StreamWriter oFile = new StreamWriter(sRemoteWelchAllynECGPath + @"\Inbox" + @"\WelchAllynECGRequest.txt", false);
                        oFile.Write(sWelchAllynECGRequest);
                        oFile.Close();
                        oFile.Dispose();
                        oFile = null;
                        sWelchAllynECGRequest = null;

                        Timer = new System.Windows.Forms.Timer();
                        Timer.Tick += new EventHandler(Timer_Tick);
                        
                        Timer.Interval = 500;
                        Timer.Enabled = true;
                        Timer.Start();

                        lblMainMsg.Text = "Connected to WelchAllyn ECG Device.";
                        LblToolMsg.Text = "You must close WelchAllyn application to access gloEMR"; 
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                ObjWelchallynlayer = null;
                oWelchAllynECGRequest = null;
                oPatientECGDetails = null;

                if (dtGetPatientdemogrphicData != null)
                {
                    dtGetPatientdemogrphicData.Dispose();
                    dtGetPatientdemogrphicData = null;
                }

                DeviceUser = null;
                DevicePassword = null;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Timer.Enabled = false;

                GetWelchAllynECGResponse();

                if (File.Exists(sRemoteWelchAllynECGPath + @"\Inbox" + @"\WelchAllynECGRequest.txt"))
                {
                    Timer.Enabled = true;
                }
                else
                {
                    if (Timer != null)
                    {
                        Timer.Dispose();
                        Timer = null;
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
        }

        private void GetWelchAllynECGResponse()
        {
            WelchAllynECGResponse oWelchAllynECGResponse = null;
            ClsWelchAllynECGLayer ObjClsWelchAllynECGLayer = null;
            gloEDocumentV3.eDocManager.eDocManager oDocManager = null;

            try
            {
                if (File.Exists(sRemoteWelchAllynECGPath + @"\Outbox" + @"\WelchAllynECGResponse.txt"))
                {
                    LblToolMsg.Text = "Please wait. Importing test data from WelchAllyn application.";
                    btn_InternetFailed.Enabled = false;
                    this.Refresh();

                    if (gloGlobal.clsMISC.WaitForFileToBeReady(sRemoteWelchAllynECGPath + @"\Outbox" + @"\WelchAllynECGResponse.txt", 50, 200))
                    {
                        string sFileData = File.ReadAllText(sRemoteWelchAllynECGPath + @"\Outbox" + @"\WelchAllynECGResponse.txt");

                        if (!string.IsNullOrEmpty(sFileData))
                        {
                            oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
                            oWelchAllynECGResponse = new WelchAllynECGResponse();

                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            serializer.MaxJsonLength = Int32.MaxValue;

                            oWelchAllynECGResponse = serializer.Deserialize<WelchAllynECGResponse>(sFileData);

                            serializer = null;
                            sFileData = null;

                            if (oWelchAllynECGResponse != null && oWelchAllynECGResponse.WelchAllynECGCollection.Count > 0)
                            {
                                ObjClsWelchAllynECGLayer = new ClsWelchAllynECGLayer(sConnectionString);

                                for (int i = 0; i < oWelchAllynECGResponse.WelchAllynECGCollection.Count; i++)
                                {
                                    Int64 nVisitID = 0;
                                    DateTime GivenDate = DateTime.Now;
                                    DateTime OrderDate = DateTime.Now;
                                    string P = string.Empty;
                                    string QRS = string.Empty;
                                    string QT = string.Empty;
                                    string QTC = string.Empty;
                                    string QRSAxis = string.Empty;
                                    string PAxis = string.Empty;
                                    string TAxis = string.Empty;
                                    string Inerpretation = string.Empty;
                                    string OrderId = string.Empty;
                                    string TestId = string.Empty;
                                    string ECGDocumentName = string.Empty;
                                    Int64 nContainerID = 0;
                                    Int64 nDocumentID = 0;

                                    Int64 nECG_ID = ObjClsWelchAllynECGLayer.WA_ECGID(oWelchAllynECGResponse.WelchAllynECGCollection[i].TestId);

                                    if (nECG_ID > 0)
                                    {
                                        DataTable DtECGTestInfrmation = ObjClsWelchAllynECGLayer.WA_GetECGTestInformation(nECG_ID, oWelchAllynECGResponse.PatientID);

                                        if (DtECGTestInfrmation != null && DtECGTestInfrmation.Rows.Count > 0)
                                        {
                                            long.TryParse(DtECGTestInfrmation.Rows[0]["nVisitID"].ToString(), out nVisitID);
                                            DateTime.TryParse(DtECGTestInfrmation.Rows[0]["dtGivenDate"].ToString(), out GivenDate);
                                            DateTime.TryParse(DtECGTestInfrmation.Rows[0]["dtOrderDate"].ToString(), out OrderDate);
                                            P = DtECGTestInfrmation.Rows[0]["sPR"].ToString();
                                            QRS = DtECGTestInfrmation.Rows[0]["sORSDuration"].ToString();
                                            QT = DtECGTestInfrmation.Rows[0]["sQT"].ToString();
                                            QTC = DtECGTestInfrmation.Rows[0]["sQTc"].ToString();
                                            QRSAxis = DtECGTestInfrmation.Rows[0]["sQRSAxis"].ToString();
                                            PAxis = DtECGTestInfrmation.Rows[0]["sPAxis"].ToString();
                                            TAxis = DtECGTestInfrmation.Rows[0]["sTAxis"].ToString();
                                            Inerpretation = DtECGTestInfrmation.Rows[0]["sECGInterpretation"].ToString();
                                            OrderId = DtECGTestInfrmation.Rows[0]["sOrderId"].ToString();
                                            TestId = DtECGTestInfrmation.Rows[0]["sTestId"].ToString();
                                            nDocumentID = Convert.ToInt64(DtECGTestInfrmation.Rows[0]["nDMSDocumentID"]);
                                        }

                                        if (DtECGTestInfrmation != null)
                                        {
                                            DtECGTestInfrmation.Dispose();
                                            DtECGTestInfrmation = null;
                                        }
                                    }

                                    OrderDate = Convert.ToDateTime(oWelchAllynECGResponse.WelchAllynECGCollection[i].OrderDate);
                                    GivenDate = Convert.ToDateTime(oWelchAllynECGResponse.WelchAllynECGCollection[i].OrderDate);

                                    if (nVisitID == 0)
                                    {
                                        nVisitID = ObjClsWelchAllynECGLayer.getVisitID(OrderDate, oWelchAllynECGResponse.PatientID, sConnectionString);
                                    }

                                    TestId = oWelchAllynECGResponse.WelchAllynECGCollection[i].TestId;
                                    OrderId = TestId;
                                    P = oWelchAllynECGResponse.WelchAllynECGCollection[i].PR;
                                    QRS = oWelchAllynECGResponse.WelchAllynECGCollection[i].QRSDuration;
                                    QT = oWelchAllynECGResponse.WelchAllynECGCollection[i].QT;
                                    QTC = oWelchAllynECGResponse.WelchAllynECGCollection[i].QTC;
                                    QRSAxis = oWelchAllynECGResponse.WelchAllynECGCollection[i].QRSAxis;
                                    PAxis = oWelchAllynECGResponse.WelchAllynECGCollection[i].PAxis;
                                    TAxis = oWelchAllynECGResponse.WelchAllynECGCollection[i].TAxis;

                                    if (!string.IsNullOrEmpty(oWelchAllynECGResponse.WelchAllynECGCollection[i].ECGInterpretation))
                                    {
                                        Inerpretation = oWelchAllynECGResponse.WelchAllynECGCollection[i].ECGInterpretation;
                                    }

                                    ECGDocumentName = oWelchAllynECGResponse.WelchAllynECGCollection[i].ECGDocumentName;

                                    if (!string.IsNullOrEmpty(ECGDocumentName))
                                    {
                                        if (File.Exists(sRemoteWelchAllynECGPath + @"\Outbox\" + ECGDocumentName))
                                        {
                                            ArrayList oSourceDocuments = new ArrayList();
                                            FileInfo oFile = new FileInfo(sRemoteWelchAllynECGPath + @"\Outbox\" + ECGDocumentName);
                                            string sTempDocPath = string.Empty;

                                            if (oFile.Extension.ToUpper() == ".PDF")
                                            {
                                                sTempDocPath = Path.Combine(gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath, ECGDocumentName);

                                                if (!Directory.Exists(gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath))
                                                {
                                                    Directory.CreateDirectory(gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath);
                                                }

                                                oFile.MoveTo(sTempDocPath);
                                                oSourceDocuments.Add(sTempDocPath);
                                            }

                                            oFile = null;

                                            Int64 nOldDocumentID = nDocumentID;

                                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - START" + " " + DateTime.Now.TimeOfDay);
                                            oDocManager.ImportSplit(oWelchAllynECGResponse.PatientID, oSourceDocuments, OrderDate.ToString("MM dd yyyy hh mm ss tt"), nDMSCategoryID, sDMSCategory, DateTime.Now.Year.ToString(), gloEDocumentV3.eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month), nClinicID, out nContainerID, out nDocumentID, false, false);
                                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - FINISHED" + " " + DateTime.Now.TimeOfDay);

                                            if (nDocumentID > 0 && nOldDocumentID > 0)
                                            {
                                                DeleteDMSDocument(oWelchAllynECGResponse.PatientID, nOldDocumentID);
                                            }

                                            oSourceDocuments = null;

                                            if (File.Exists(sTempDocPath))
                                            {
                                                try
                                                {
                                                    File.Delete(sTempDocPath);
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            }

                                            sTempDocPath = null;
                                        }
                                    }

                                    nECGID = ObjClsWelchAllynECGLayer.WA_SaveUPdateECGTest(nECG_ID, oWelchAllynECGResponse.PatientID, 0, nVisitID, nClinicID, GivenDate, 0, String.Empty, "ECG", String.Empty, OrderDate, P, QT, QTC, QRS, PAxis, QRSAxis, TAxis, Inerpretation, String.Empty, String.Empty, DateTime.Now, true, 0, OrderId, TestId, "Test Completed", "WelchAllyn", nDocumentID);

                                    IsECGTestDataChanged = true;

                                    nVisitID = 0;
                                    GivenDate = DateTime.Now;
                                    OrderDate = DateTime.Now;
                                    P = null;
                                    QRS = null;
                                    QT = null;
                                    QTC = null;
                                    QRSAxis = null;
                                    PAxis = null;
                                    TAxis = null;
                                    Inerpretation = null;
                                    OrderId = null;
                                    TestId = null;
                                    ECGDocumentName = null;
                                    nContainerID = 0;
                                    nDocumentID = 0;
                                }
                            }
                        }

                        try
                        {
                            File.Delete(sRemoteWelchAllynECGPath + @"\Outbox" + @"\WelchAllynECGResponse.txt");
                        }
                        catch (Exception)
                        { }

                    }
                }

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                LblToolMsg.Text = "You must close WelchAllyn application to access gloEMR";
                btn_InternetFailed.Enabled = true;
                this.Refresh();

                oWelchAllynECGResponse = null;

                if (ObjClsWelchAllynECGLayer != null)
                {
                    ObjClsWelchAllynECGLayer.Dispose();
                    ObjClsWelchAllynECGLayer = null;
                }
               
            }
        }

        private void DeleteDMSDocument(Int64 PatientID, Int64 DocumentID)
        {
            SqlConnection _sqlDMSConnnetion = null;
            SqlTransaction _sqlDMSTransaction = null;
            SqlParameter _sqlDMSParameter = null;
            SqlCommand _sqlDMSCommand = null;
            string _sqlQuery = "";

            try
            {
                _sqlDMSConnnetion = new SqlConnection(gloEDocV3Admin.GetDMSConnectionString(sConnectionString));
                
                if (_sqlDMSConnnetion != null)
                {
                    _sqlDMSConnnetion.Open();

                    _sqlDMSTransaction = _sqlDMSConnnetion.BeginTransaction();

                    _sqlQuery = " DELETE FROM eDocument_Details_V3 WITH(ROWLOCK) WHERE PatientID = " + PatientID + " " +
                                                " AND eDocumentID = " + DocumentID + " AND ClinicID = " + nClinicID + "";
                    //To delete the data from the Container table
                    _sqlQuery = _sqlQuery + "; DELETE FROM eDocument_Container_V3 WITH(READPAST) WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + nClinicID + "";

                    _sqlDMSCommand = new SqlCommand(_sqlQuery, _sqlDMSConnnetion, _sqlDMSTransaction);
                    _sqlDMSCommand.ExecuteNonQuery();
                    if (_sqlDMSCommand != null)
                    {
                        _sqlDMSCommand.Parameters.Clear();
                        _sqlDMSCommand.Dispose();
                        _sqlDMSCommand = null;
                    }

                    _sqlQuery = "DELETE FROM eDocument_Pages_V3 WITH(READPAST) WHERE " +
                                "eDocumentID = " + DocumentID + " AND ClinicID = " + nClinicID + "";

                    _sqlDMSCommand = new SqlCommand(_sqlQuery, _sqlDMSConnnetion, _sqlDMSTransaction);
                    _sqlDMSCommand.ExecuteNonQuery();
                    if (_sqlDMSCommand != null)
                    {
                        _sqlDMSCommand.Parameters.Clear();
                        _sqlDMSCommand.Dispose();
                        _sqlDMSCommand = null;
                    }

                    _sqlQuery = "DELETE FROM eDocument_NTAO_V3 WITH(READPAST) WHERE " +
                                "eDocumentID = " + DocumentID + " AND ClinicID = " + nClinicID + "";

                    _sqlDMSCommand = new SqlCommand(_sqlQuery, _sqlDMSConnnetion, _sqlDMSTransaction);
                    _sqlDMSCommand.ExecuteNonQuery();
                    if (_sqlDMSCommand != null)
                    {
                        _sqlDMSCommand.Parameters.Clear();
                        _sqlDMSCommand.Dispose();
                        _sqlDMSCommand = null;
                    }

                    _sqlDMSTransaction.Commit();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                _sqlDMSTransaction.Rollback();
            }
            finally
            {
                _sqlQuery = null;

                if (_sqlDMSParameter != null)
                {
                    _sqlDMSParameter = null;
                }
                if (_sqlDMSCommand != null)
                {
                    _sqlDMSCommand.Dispose();
                    _sqlDMSCommand = null;
                }
                if (_sqlDMSTransaction != null)
                {
                    _sqlDMSTransaction.Dispose();
                    _sqlDMSTransaction = null;
                }
                if (_sqlDMSConnnetion != null)
                {
                    _sqlDMSConnnetion.Dispose();
                    _sqlDMSConnnetion = null;
                }

            }
        }
        
        private void btn_InternetFailed_Click(object sender, EventArgs e)
        {
            if (File.Exists(sRemoteWelchAllynECGPath + @"\Inbox" + @"\WelchAllynECGRequest.txt"))
            {
                MessageBox.Show("Please close the WelchAllyn Application first on local machine to get disconnected.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);   
            }
            else
            {
                this.Close();
            }
            
        }

        private void GetDMSCategory()
        {
            gloEDocumentV3.eDocManager.eDocManager oDocManager = null;
            gloSettings.GeneralSettings oSettings = null;
            object oResult = null;

            try
            {
                oSettings = new gloSettings.GeneralSettings(sConnectionString);

                oSettings.GetSetting("WelchAllyn ECG Category", out oResult);

                if (!string.IsNullOrEmpty(Convert.ToString(oResult)))
                {
                    sDMSCategory = Convert.ToString(oResult);
                }

                nDMSCategoryID = gloEDocumentV3.eDocManager.eDocValidator.GetCategoryId(sDMSCategory, nClinicID);

                if (nDMSCategoryID <= 0)
                {
                    oDocManager = new gloEDocumentV3.eDocManager.eDocManager();

                    nDMSCategoryID = oDocManager.AddCategory(sDMSCategory);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                oResult = null;

                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }

                if (oDocManager != null)
                {
                    oDocManager.Dispose();
                    oDocManager = null;
                }

            }
            
        }
      

    }
}
