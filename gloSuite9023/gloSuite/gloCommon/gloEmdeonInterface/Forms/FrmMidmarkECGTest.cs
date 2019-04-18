using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloUserControlLibrary;
using gloEmdeonInterface.Classes;
using System.IO;

namespace gloEmdeonInterface.Forms
{
    public partial class FrmMidmarkECGTest : Form
    {

        # region "Form Variables"
        private AxECGACTIVEXLib.AxECGActiveX axECGActiveX1;
        public AxERCACTIVEXLib.AxERCActiveX axERCActiveX1;
        private long _nPatinet_ID = 0;
        private long _nECGID = 0;
        private long _nDocumentID = 0;
        private long _nloginUserId = 1;
        private long _ClinicID = 0;
        private int _nReportlegth = 0;
        private string _sDeviceConnectionString = string.Empty;
        private string _sgloEMRConnectionString = string.Empty;
        private string _sTestID = string.Empty;
        private string _sloginUserName = "Admin";
        private string _sReportFilePath = string.Empty;
        private bool _bIsReportDataChnaged =false ;
        private bool _bIsSDKInstalled = false;
        private ERCRetCode m_ReportType = ERCRetCode.ERC_NO_REPORT;
        private bool _bIsDataChnaged = false;
        private string _gstrMessageBoxCaption = "gloEMR";
        #endregion

        # region "enums & Properties"

        private enum ECGRetCode
        {
            ECG_SUCCESS = 0,
            ECG_INVALID_PASSWORD,
            ECG_NO_PATIENT_DATA,
            ECG_ALREADY_RUNNING,
            ECG_DOB_FORMAT_ERROR,
            ECG_NO_REPORT,
            ECG_WRONG_REPORT_TYPE,
            ECG_FILE_WRITE_ERROR,
            ECG_BP_ERROR,
            ECG_WEIGHT_ERROR,
            ECG_HEIGHT_ERROR,
            ECG_PRINTER_SET_ERROR
        };
        
        private enum ERCRetCode
        {
            ERC_SUCCESS = 0,
            ERC_NO_REPORT,
            ERC_FILE_READ_ERROR,
            ERC_FORMAT_ERROR,
            ERC_ECG_REPORT,
            ERC_RR_REPORT,
            ERC_UNSUPPORTED_REPORT_TYPE,
            ERC_EMAIL_NOT_INSTALLED,
            ERC_EMAIL_FILE_WRITE_ERROR,
            ERC_FILE_WRITE_ERROR,
            ERC_ARRAY_FORMAT_ERROR,
            ERC_ALREADY_DISPLAYED,
            ERC_PRINTER_SET_ERROR,
            ERC_SPIRO_CAL_PRINT_ERROR,
            ERC_SPIRO_REPORT,
            ERC_NOT_ECG_REPORT,
            ERC_TOO_MANY_REPORTS,
            ERC_HOLTER_REPORT,
            ERC_DOB_FORMAT_ERROR,
            ERC_NO_PRINT_PERMISSION,
            ERC_REPORT_NOT_REVIEWED,
            ERC_NOT_SPIRO_REPORT,
        };

        public bool IsDataChanged
        {
            get { return _bIsDataChnaged; }
            set { _bIsDataChnaged = value; }
        }

        public long ECGID
        {
            get { return _nECGID; }
            set { _nECGID = value; }
        }

        #endregion

        #region FormCntrolEvents 

        public FrmMidmarkECGTest(long nECGID, long Patinet_ID, string DeviceConnectionString, string gloEMRConnectionString, string TestID)
        {

            InitializeComponent();
            
            try
            {

                _nPatinet_ID = Patinet_ID;

                ECGID = nECGID;

                _sDeviceConnectionString = DeviceConnectionString;

                _sgloEMRConnectionString = gloEMRConnectionString;

                _sTestID = TestID;

         
                if (CreateInstanceOFECGActivex() && CreateInstanceOFERCActivex())
                               
                    _bIsSDKInstalled = true;
                else
                   _bIsSDKInstalled = false;

                IntiliaseglobalVaribales();
           
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.FrmMidmarkECGTest()" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
                _bIsSDKInstalled = false;
          }
          
        }

        private void FrmMidmarkECGTest_Load(object sender, EventArgs e)
        {
            try
            {
                Application.DoEvents();

                Control.CheckForIllegalCrossThreadCalls = false;

                Application.DoEvents();
                              
                Application.DoEvents();
                backgroundWorker1.RunWorkerAsync();
                Application.DoEvents();
                
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.FrmMidmarkECGTest_Load()" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
        }

        private void tlbbtnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {         
            try
            {
                Application.DoEvents();

                if (_bIsSDKInstalled)
                {
                   
                    Application.DoEvents();

                    //check for perform new test
                    if (_nECGID <= 0 && _sTestID.Trim().Length <= 0)
                    {
                        NewECGTest();

                    }
                    //check for perform review test
                    else if (_nECGID > 0 && _sTestID.Trim().Length > 0)
                    {
                        Application.DoEvents();
                        ViewECGTest();
                        return;
                    }

                }
                else
                {
                    lblMainMsg.Text = "Failed To Connect Midmark IQ ECG Device";
                    LblToolMsg.Text = "Midmark IQ ECG Device SDK is not Installed";
                    MessageBox.Show("Midmark IQ ECG Device SDK is not Installed", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.backgroundWorker1_DoWork()" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (_bIsSDKInstalled)
            //{
            //    lblMainMsg.Text = "Connected To Midmark IQ ECG Device";
            //    LblToolMsg.Text = "";

            //}
        }
        
        #endregion

      

        #region "ActivexEvents"

        private void axECGActiveX1_ECGReportReady(object sender, EventArgs e)
        {
            try
            {
                _nReportlegth = axECGActiveX1.GetECGReportLength();
               
                if (_nReportlegth > 0)
                {

                    m_ReportType = ERCRetCode.ERC_ECG_REPORT;

                    axECGActiveX1.GetECGReportToMemory();

                    _sReportFilePath = GetReportFilePath();

                    axECGActiveX1.GetECGReportToFile(_sReportFilePath);

                }
                else
                {
                    _nReportlegth = 0;
                    _sReportFilePath = string.Empty;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.axECGActiveX1_ECGReportReady()" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _nReportlegth = 0;
                _sReportFilePath = string.Empty;
                ex = null;
            }
         

        }

        private void axECGActiveX1_RRReportReady(object sender, EventArgs e)
        {
            //n_Reportlegth = axECGActiveX1.GetRRReportLength();
            //if (n_Reportlegth > 0)
            //{
            //    m_ReportType = ERCRetCode.ERC_RR_REPORT;
            //    axECGActiveX1.GetRRReportToMemory(); 

            //}

        }
        
        private void axECGActiveX1_ECGEnding(object sender, EventArgs e)
        {
            int iRet = 0;
            try
            {

                if (_nReportlegth > 0)
                {

                    if (m_ReportType == ERCRetCode.ERC_ECG_REPORT)
                    {

                        try { iRet = axERCActiveX1.SetReportDataDirect(axECGActiveX1.GetECGReportToMemory()); }  catch (Exception) { iRet = (int)ERCRetCode.ERC_NO_REPORT; }

                        if (iRet != (int)ERCRetCode.ERC_SUCCESS && File.Exists(_sReportFilePath))
                        {
                            iRet = axERCActiveX1.SetReportDataFromFile(_sReportFilePath);
                            File.Delete(_sReportFilePath);
                            _sReportFilePath = string.Empty;

                        }

                    }
                    //else if (m_ReportType == ERCRetCode.ERC_RR_REPORT)
                    //{
                    //    iRet = axERCActiveX1.SetReportDataDirect(axECGActiveX1.GetRRReportToMemory());
                    //}
                    if (iRet == (int)ERCRetCode.ERC_SUCCESS)
                    {
                        if (axERCActiveX1.GetReportType() == (int)ERCRetCode.ERC_ECG_REPORT)
                        {
                            axERCActiveX1.SetPermissions(true, true, true, true);
                            axERCActiveX1.SetReviewedBy(_sloginUserName);
                            axERCActiveX1.SetReviewDate(DateTime.Now);
                            axERCActiveX1.SetEditPermissions(1, 1, 1, 1, 0, 0, 0);
                            axERCActiveX1.StartECGReview();
                        }
                    }                  

                }
                else
                {
                    this.Close();  
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.axECGActiveX1_ECGEnding()" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
         
        }

        private void axERCActiveX1_ReportDataChanged(object sender, EventArgs e)
        {
             _bIsReportDataChnaged = true;
        }

        private void axERCActiveX1_ReviewEnding(object sender, EventArgs e)
        {
            string PR = string.Empty;
            string QT = string.Empty;
            string QTC = string.Empty;
            string QRSDuration = string.Empty;
            string Paxis = string.Empty;
            string QRSaxis = string.Empty;
            string Taxis = string.Empty;
            string sInterpretation = string.Empty;
            try
            {
                if (_nECGID <= 0)
                {
                    lblMainMsg.Text = "Retriving Midmark IQ ECG Test Result";
                    LblToolMsg.Text = "Please Wait.........................";
                    MainPanel.Refresh();  
                    _sReportFilePath = GetReportFilePath();
                    axERCActiveX1.GetReportDataToFile(_sReportFilePath);
                    PR = Convert.ToString(axERCActiveX1.GetECGPRInterval());
                    QT = Convert.ToString(axERCActiveX1.GetECGQTInterval());
                    QTC = Convert.ToString(axERCActiveX1.GetECGQTcInterval());
                    QRSDuration = Convert.ToString(axERCActiveX1.GetECGQRSDuration());
                    Paxis = Convert.ToString(axERCActiveX1.GetECGPWaveAxis());
                    QRSaxis = Convert.ToString(axERCActiveX1.GetECGQrsWaveAxis());
                    Taxis = Convert.ToString(axERCActiveX1.GetECGTWaveAxis());
                    sInterpretation = Convert.ToString(axERCActiveX1.GetECGDiagnosticStatements());
                    ECGID = SaveUpdateMidmarkECGReport(_sReportFilePath, PR, QT, QTC, QRSDuration, Paxis, QRSaxis, Taxis, sInterpretation);
                    if (ECGID > 0)
                       IsDataChanged = true;
                    else
                        IsDataChanged = false;
                 }
                else if (_nECGID > 0 && _bIsReportDataChnaged)
                {
                    lblMainMsg.Text = "Retriving Midmark IQ ECG Test Result";
                    LblToolMsg.Text = "Please Wait.........................";
                    MainPanel.Refresh();  
                    _sReportFilePath = GetReportFilePath();
                    axERCActiveX1.GetReportDataToFile(_sReportFilePath);
                    PR = Convert.ToString(axERCActiveX1.GetECGPRInterval());
                    QT = Convert.ToString(axERCActiveX1.GetECGQTInterval());
                    QTC = Convert.ToString(axERCActiveX1.GetECGQTcInterval());
                    QRSDuration = Convert.ToString(axERCActiveX1.GetECGQRSDuration());
                    Paxis = Convert.ToString(axERCActiveX1.GetECGPWaveAxis());
                    QRSaxis = Convert.ToString(axERCActiveX1.GetECGQrsWaveAxis());
                    Taxis = Convert.ToString(axERCActiveX1.GetECGTWaveAxis());
                    sInterpretation = Convert.ToString(axERCActiveX1.GetECGDiagnosticStatements());
                    ECGID=SaveUpdateMidmarkECGReport(_sReportFilePath, PR, QT, QTC, QRSDuration, Paxis, QRSaxis, Taxis, sInterpretation);
                    if (ECGID > 0)
                        IsDataChanged = true;
                    else
                        IsDataChanged = false;
                }
                else
                {
                    IsDataChanged = false;
                }
               
                this.Close();      
            }
            catch (Exception)
            {
            }
            finally
            {
                 PR = string.Empty;
                 QT = string.Empty;
                 QTC = string.Empty;
                 QRSDuration = string.Empty;
                 Paxis = string.Empty;
                 QRSaxis = string.Empty;
                 Taxis = string.Empty;
                 sInterpretation = string.Empty;

            }

        }
        
        #endregion

        #region "Function && Methods"

       private bool IntiliaseglobalVaribales()
        {
            bool Result = false;

                try
                {
                    _gstrMessageBoxCaption = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MessageBOXCaption"]);

                    if (_gstrMessageBoxCaption.Trim().Length <= 0)

                        _gstrMessageBoxCaption = "gloEMR"; 
                }
                catch (Exception)
                {
                    _gstrMessageBoxCaption = "gloEMR"; 
                }

               try
                {
                    long.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UserID"]), out _nloginUserId);

                   _sloginUserName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UserName"]);
                    
                    if (_nloginUserId <= 0 || _sloginUserName.Trim().Length <= 0)
                    {
                        _nloginUserId = 1;
                        _sloginUserName = "Admin";
                    }

                }
                catch (Exception)
                {
                    _nloginUserId = 1;
                    _sloginUserName = "Admin";
                }
                try
                {
                    long.TryParse(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ClinicID"]), out _ClinicID);
                    if (_ClinicID <= 0)
                        _ClinicID = 1;
                }
                catch (Exception)
                {
                    _ClinicID = 1;
                }
            return Result;
        }

       private bool CreateInstanceOFECGActivex()
        {
            bool Result = false;
            System.ComponentModel.ComponentResourceManager resources =null;
            try
            {
                resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMidmarkECGTest)); ;
                this.axECGActiveX1 = new AxECGACTIVEXLib.AxECGActiveX();
                ((System.ComponentModel.ISupportInitialize)(this.axECGActiveX1)).BeginInit();
                this.SuspendLayout();
                this.axECGActiveX1.Enabled = true;
                this.axECGActiveX1.Location = new System.Drawing.Point(208, 122);
                this.axECGActiveX1.Name = "axECGActiveX1";
                this.axECGActiveX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axECGActiveX1.OcxState")));
                this.axECGActiveX1.Size = new System.Drawing.Size(100, 50);
                this.axECGActiveX1.TabIndex = 1;
                this.axECGActiveX1.ECGReportReady += new System.EventHandler(this.axECGActiveX1_ECGReportReady);
                this.axECGActiveX1.RRReportReady += new System.EventHandler(this.axECGActiveX1_RRReportReady);
                this.axECGActiveX1.ECGEnding  += new System.EventHandler(this.axECGActiveX1_ECGEnding);
                ((System.ComponentModel.ISupportInitialize)(this.axECGActiveX1)).EndInit();
                this.Controls.Add(this.axECGActiveX1);
                this.ResumeLayout(false);

                Result = true;
            }
            catch (Exception)
            {
                Result = false;
            }
            finally
            {
                resources = null;
            }

            return Result;
        }

        private bool CreateInstanceOFERCActivex()
        {
            bool _InitializeERCActiveXComponent = false;
            System.ComponentModel.ComponentResourceManager resources = null;
            try
            {
                resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMidmarkECGTest));
                this.axERCActiveX1 = new AxERCACTIVEXLib.AxERCActiveX();
                ((System.ComponentModel.ISupportInitialize)(this.axERCActiveX1)).BeginInit();
                this.SuspendLayout();
                this.axERCActiveX1.Enabled = true;
                this.axERCActiveX1.Visible = false;
                this.axERCActiveX1.Location = new System.Drawing.Point(900, 13);
                this.axERCActiveX1.Name = "axERCActiveX1";
                this.axERCActiveX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axERCActiveX1.OcxState")));
                this.axERCActiveX1.Size = new System.Drawing.Size(53, 26);
                this.axERCActiveX1.TabIndex = 40;
                this.axERCActiveX1.ReportDataChanged += new System.EventHandler(this.axERCActiveX1_ReportDataChanged);
                this.axERCActiveX1.ReviewEnding += new System.EventHandler(this.axERCActiveX1_ReviewEnding);
                this.Controls.Add(this.axERCActiveX1);
                ((System.ComponentModel.ISupportInitialize)(this.axERCActiveX1)).EndInit();
                this.ResumeLayout(false);
                _InitializeERCActiveXComponent = true;

            }
            catch (Exception)
            {
                this.Controls.Remove(this.axERCActiveX1);
                axERCActiveX1.Dispose();
                _InitializeERCActiveXComponent = false;
            }
            finally
            {
                if (resources != null)
                {
                    resources = null;
                }

            }
            return _InitializeERCActiveXComponent;

        }

        private bool ConnectToDeviceDatabase()
        {
            Boolean _bResult = false;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            try
            {
                if (_sDeviceConnectionString.Trim().Length > 0)
                {
                    oDBLayer = new gloDatabaseLayer.DBLayer(_sDeviceConnectionString);
                    _bResult = oDBLayer.CheckConnection();
                    oDBLayer.Disconnect();
                    
                }
                else
                {
                    _bResult = false;
                }

            }
            catch (Exception)
            {
                _bResult = false;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
            }
            return _bResult;
        }
        
        private bool NewECGTest()
        {
            bool _bResult = false;
            int iRet = 0;
            DataTable dtGetPatientData = null;
            try
            {

                if (!ConnectToDeviceDatabase())
                {
                    lblMainMsg.Text = "Failed To Connect Midmark IQ ECG Device";
                    LblToolMsg.Text = "Unable To Create Midmark IQ ECG Test, Device Database Credentials are invalid.";
                    MessageBox.Show("Unable to Create Midmark IQ ECG Test " + System.Environment.NewLine + " Device Database Credentials are invalid.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _bResult = false;
                    return _bResult;
                }

                dtGetPatientData = RetrivePatientData(_nPatinet_ID);

                lblMainMsg.Text = "Connected To Midmark IQ ECG Device";
                LblToolMsg.Text = "";

                if (dtGetPatientData != null && dtGetPatientData.Rows.Count > 0)
                {

                    iRet = axECGActiveX1.SetPatientData(Convert.ToString(dtGetPatientData.Rows[0]["sLastName"]), Convert.ToString(dtGetPatientData.Rows[0]["sFirstName"]), Convert.ToString(dtGetPatientData.Rows[0]["sMiddleName"]), Convert.ToString(dtGetPatientData.Rows[0]["sPatientCode"]), Convert.ToString(dtGetPatientData.Rows[0]["dtDOB"]), Convert.ToInt32(dtGetPatientData.Rows[0]["Gender"].ToString()));

                    if (iRet == (int)ECGRetCode.ECG_DOB_FORMAT_ERROR)
                    {
                        lblMainMsg.Text = "Failed To Connect Midmark IQ ECG Device";
                        LblToolMsg.Text = "Unable to Create New ECG test due to Invalid date of birth for patient";
                        MessageBox.Show("Invalid date of birth", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _bResult = false;
                    }
                    else
                    {
                        axECGActiveX1.SetTechnicianName(_sloginUserName);
                        axECGActiveX1.SetPhysician(_sloginUserName);
                        iRet = axECGActiveX1.StartECG("ipmhcgjttc");
                        if (iRet == (int)ECGRetCode.ECG_SUCCESS)
                            _bResult = true;
                        else
                            _bResult = false;

                    }
                }
                else
                {
                    lblMainMsg.Text = "Failed To Connect Midmark IQ ECG Device";
                    LblToolMsg.Text = "Unable to Create New ECG test due to patient demographic data is not present";
                    MessageBox.Show("Unable to Create New ECG test " + Environment.NewLine + " Patient Demographic data is not present", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _bResult = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.NewECGTest() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _bResult = false;
                ex = null;
            }
            finally
            {
                iRet = 0;
                if (dtGetPatientData != null)
                {
                    dtGetPatientData.Dispose();
                    dtGetPatientData = null;
                }
              
            }
            return _bResult; 
        }

        private bool ViewECGTest()
        {
            bool _Result = false;
            ClsMidmarkECGLayer objClsMidmarkECGLayer = null;
            clsSpiroReports ObjclsSpiroReports = null;
            DataTable dtGetPatientData = null;
            try
            {

                if (! ConnectToDeviceDatabase())
                {
                     lblMainMsg.Text = "Failed To Connect Midmark IQ ECG Device";
                     LblToolMsg.Text = "Unable to View Midmark IQ ECG Test, Device Database Credentials are invalid." ;
                     MessageBox.Show("Unable to Review Midmark ECG Test," + System.Environment.NewLine + " Device Database Credentials are invalid.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Result = false;
                     return _Result ;
                }

                long.TryParse(_sTestID, out _nDocumentID);


                lblMainMsg.Text = "Connected To Midmark IQ ECG Device";
                LblToolMsg.Text = "";

                objClsMidmarkECGLayer = new ClsMidmarkECGLayer();
                ObjclsSpiroReports = new clsSpiroReports();
                objClsMidmarkECGLayer.sDeviceConnectionAtring = _sDeviceConnectionString;
                objClsMidmarkECGLayer.sgloEMRConnectionString = _sgloEMRConnectionString;
                ObjclsSpiroReports = objClsMidmarkECGLayer.RetriveMidmarkECGTest(_nDocumentID);
                dtGetPatientData = RetrivePatientData(_nPatinet_ID);
                if (ObjclsSpiroReports != null && ObjclsSpiroReports.DocumentId >= 0)
                {
                    axERCActiveX1.SetReportDataDirect(ObjclsSpiroReports.DocStream);
                    axERCActiveX1.SetPermissions(true, true, true, true);
                    axERCActiveX1.SetReviewedBy(_sloginUserName);
                    axERCActiveX1.SetReviewDate(DateTime.Now);
                    axERCActiveX1.SetEditPermissions(1, 1, 1, 1, 0, 0, 0);
                    if (dtGetPatientData != null && dtGetPatientData.Rows.Count > 0)
                    {
                        axERCActiveX1.SetPatientData(Convert.ToString(dtGetPatientData.Rows[0]["sLastName"]), Convert.ToString(dtGetPatientData.Rows[0]["sFirstName"]), Convert.ToString(dtGetPatientData.Rows[0]["sMiddleName"]), Convert.ToString(dtGetPatientData.Rows[0]["sPatientCode"]), Convert.ToString(dtGetPatientData.Rows[0]["dtDOB"]), Convert.ToInt32(dtGetPatientData.Rows[0]["Gender"].ToString()));
                    }
                    axERCActiveX1.StartReview();
                    _Result = true;
                }
                else
                {
                    lblMainMsg.Text = "Failed To Connect Midmark IQ ECG Device";
                    LblToolMsg.Text = "Unable to Review Midmark ECG Test,Test information is not present in Device Database";
                    MessageBox.Show("Unable to Review Midmark ECG Test," + System.Environment.NewLine + " Test information is not present in Device Database", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Result = false;
                  
                }          

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.NewECGTest() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                _Result = false;
                ex = null;
            }
            finally
            {
                if (objClsMidmarkECGLayer != null)
                {
                    objClsMidmarkECGLayer.Dispose();
                    objClsMidmarkECGLayer = null; 
                }
                if (ObjclsSpiroReports != null)
                {
                    ObjclsSpiroReports.Dispose();
                    ObjclsSpiroReports = null;
                }
                if (dtGetPatientData != null)
                {
                    dtGetPatientData.Dispose();
                    dtGetPatientData = null;
                }
            }
           return _Result;

        }

        private DataTable RetrivePatientData(long _nPatientID)
        {
            DataTable dtResult=null;
            ClsMidmarkECGLayer objClsMidmarkECGLayer = null;
            try
            {
                objClsMidmarkECGLayer = new ClsMidmarkECGLayer();
                objClsMidmarkECGLayer.sgloEMRConnectionString = _sgloEMRConnectionString;
                dtResult = objClsMidmarkECGLayer.GetPationtData(_nPatientID);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.RetrivePatientData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                dtResult = null;
                ex = null;
            }
            finally
            {
                if (objClsMidmarkECGLayer != null)
                {
                    objClsMidmarkECGLayer.Dispose();
                    objClsMidmarkECGLayer = null;
                }
            }
           return dtResult ;            
        }
        
        private String GetReportFilePath()
        {          
            string _sFilePath =string.Empty;
            try
            {               
              // create filename filename  
                _sFilePath = gloSettings.FolderSettings.AppTempFolderPath + Convert.ToString(_nPatinet_ID) + "_" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".CAR";

              // check file exists with same name
              if (File.Exists(_sFilePath))
                  File.Delete(_sFilePath);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in FrmMidmarkECGTest.RetrivePatientData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
             _sFilePath =string.Empty;
                ex = null;
            }
            return _sFilePath; 

        }

        private long SaveUpdateMidmarkECGReport(string ReportFilePath, string PR, string QT, string QTC, string QRSDuration, string Paxis, string QRSaxis, string Taxis, string sInterpretention)
        {
            long _SaveUpdateMidmarkECGReport = 0;
            ClsMidmarkECGLayer objClsMidmarkECGLayer = null;
            try
            {
                if (File.Exists(ReportFilePath))
                {
                    objClsMidmarkECGLayer = new ClsMidmarkECGLayer();
                    objClsMidmarkECGLayer.sDeviceConnectionAtring = _sDeviceConnectionString;
                    objClsMidmarkECGLayer.sgloEMRConnectionString = _sgloEMRConnectionString;
                    objClsMidmarkECGLayer.ECGID = _nECGID;
                    objClsMidmarkECGLayer.DocumentID = _nDocumentID;
                    objClsMidmarkECGLayer.VisitID = 0;
                    objClsMidmarkECGLayer.PatientID = _nPatinet_ID;
                    objClsMidmarkECGLayer.ClinicID = _ClinicID;
                    objClsMidmarkECGLayer.LoginUserID = _nloginUserId;
                    objClsMidmarkECGLayer.LoginUserName = _sloginUserName;
                    objClsMidmarkECGLayer.PR = PR;
                    objClsMidmarkECGLayer.QT = QT;
                    objClsMidmarkECGLayer.QTC = QTC;
                    objClsMidmarkECGLayer.QRSDuration = QRSDuration;
                    objClsMidmarkECGLayer.Paxis = Paxis;
                    objClsMidmarkECGLayer.QRSaxis = QRSaxis;
                    objClsMidmarkECGLayer.Taxis = Taxis;
                    objClsMidmarkECGLayer.DocumentID = 0;
                    objClsMidmarkECGLayer.ReportFilePath = ReportFilePath;
                    objClsMidmarkECGLayer.Interpretation = sInterpretention;
                    objClsMidmarkECGLayer.DocumentName = _nPatinet_ID + "_" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".CAR";
                    objClsMidmarkECGLayer.SaveMidmarkECGReport();
                    _SaveUpdateMidmarkECGReport = objClsMidmarkECGLayer.ECGID;

                } //if file exists             

            }
            catch (Exception)
            {
                _SaveUpdateMidmarkECGReport = 0;
            }
            finally
            {
                if (objClsMidmarkECGLayer != null)
                {
                    objClsMidmarkECGLayer.Dispose();
                    objClsMidmarkECGLayer = null;
                }

            }
            return _SaveUpdateMidmarkECGReport;

        }

        
        #endregion

       
    
    }
}
