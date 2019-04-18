using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class FrmWelChallynECG : Form
    {

        #region "local variables"
        
      
        private long nPatientID = 0;
        private long nUserId = 0;
        private long nClicnicID =1;
        private String sTestID = String.Empty;
        private string gloConnectionString = string.Empty;
        private long nClinicid = 1;
        private string[] SelectedTest = null;
        private CcBase.ITests TL;
        private long _nECGID = 0;

        #endregion "local variables"

        #region "ProPerties/enum"
        private TestType _TestType;
        public enum TestType { NewTest, UpdateTest, GetAlltest,PrintTest};
        private bool _IsECGTestDataConducted = false;
        public bool IsECGTestDataChanged
        {
            get { return _IsECGTestDataConducted; }
            set { _IsECGTestDataConducted = value; }
        }
        public long nECGID
        {
            get { return _nECGID;}
            set { _nECGID = value; }
        }
        #endregion "ProPerties/enum"
        
        #region "WelchAllyn Variables"
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        CCMDW.MDWClass MDW = null;
        CCMDW._ApplicationEvents_OnNewTestEventHandler OnNewTestDelegate;
        CCMDW._ApplicationEvents_OnNewInterpretationEventHandler OnNewInterpDelegate;
        CCMDW._ApplicationEvents_OnQuitEventHandler OnQuitDelegate;
        CCMDW._ApplicationEvents_OnUpdateTestEventHandler OnUpdateTestDelegate;

        #endregion "WelchAllyn Variables"
        
        #region "Form Events"

        public FrmWelChallynECG(TestType TestType, long PatientID, string TestID, long nLoginUSerId,long ClinicID, string GloEMRConnectionString)
        {
            InitializeComponent();
            _TestType = TestType;
            gloConnectionString = GloEMRConnectionString;
            nPatientID = PatientID;
            sTestID = TestID;
            nUserId = nLoginUSerId;
            
        }


        private void FrmWelChallynECG_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            switch (_TestType)
            {
                case TestType.NewTest:
                    {
                        StartNewECGTest();
                        break;
                    }
                case TestType.UpdateTest:
                    {
                        WA_UpdateTest();
                        break;
                    }
                case TestType.GetAlltest:
                    {
                        WA_GetAllTest();
                        break;
                    }
                case TestType.PrintTest:
                    {
                        WA_PrintTest();
                        break; 
                    }
            }
            
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmWelChallynECG_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MDW != null)
            {
                System.Diagnostics.Process P = null;
                System.Diagnostics.Process[] Prcs = null;
                try
                {
                    Prcs = System.Diagnostics.Process.GetProcessesByName("mdw");
                    if (Prcs != null)
                    {
                        if (Prcs.Length > 0)
                        {
                            P = Prcs[0];
                        }
                    }
                    //P = System.Diagnostics.Process.GetProcessesByName("mdw")[0];
                    if (P != null)
                    {
                        if (MessageBox.Show("You are about to close WelchAllyn Application, Do you want to continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                P.Kill();
                            }
                            else
                            {
                                e.Cancel = false;
                                e.Cancel = true;
                                MDW.Restore();  
                                MDW.Maximize();
                                MDW.BringToFront();
                                Microsoft.VisualBasic.Interaction.AppActivate("WelchAllyn CardioPerfect");
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
                    if (P != null)
                    {
                        P.Dispose();
                        P = null;
                    }
                }

            }


        }


        #endregion  "Form Events"


        #region "Functions"


        public void StartNewECGTest()
        {
          try
            {
                if (Invoke_WelchAllynDevice())
                {
                    AddToProcess();
                    MDW.Minimize(); 
                    MDW.RecordTest(CcBase.TestType.ttECG);
                 }
                else
                {
                    MDW = null;
                    WA_Quit();   
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }

        }

        private void WA_PrintTest()
        {
            try
            {
                if (Invoke_WelchAllynDevice())
                {
                    MDW.Minimize();
                  //  this.TopMost = true;
                    this.BringToFront();
                    MDW.SelectTest(sTestID);
                    MDW.Print();
                    System.Threading.Thread.Sleep(1000);    
                    WA_Quit(); 
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }

        }
        
        private void WA_UpdateTest()
        {
            CcBase.ITest Test=null;
            try
            {
                if (Invoke_WelchAllynDevice())
                {
                    AddToProcess();
                    MDW.Minimize();
                    MDW.SelectTest(sTestID);
                    Test = MDW.CurrentTest();
                    //test is deleted 
                    if (Test == null)
                    {
                        MessageBox.Show("Record not found.The selected ECG test record may deleted from source system.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MDW = null;
                        WA_Quit();
                    }
                    else
                    {
                        //test is present 
                        MDW.Restore();
                        MDW.BringToFront(); 
                        MDW.Maximize();
                    }
                }
                else
                {
                    MDW = null;
                    WA_Quit();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;


            }
            finally
            {
                Test=null; 
            }
           
        }

        private void WA_Quit()
        {
            try
            {
                  MDW.Quit();   
            }
            catch (Exception)
            {
                RemoveFromProcess();
                MDW = null;  
                this.Close(); 
            }

        }

        private bool Invoke_WelchAllynDevice()
        {
            bool _IsdeviceCalled = false;
            DataTable dtGetPatientdemogrphicData = null;
            String PatientCode = string.Empty;
            String LastName = string.Empty;
            String MidName = string.Empty;
            String FirstName = string.Empty;
            DateTime BirthDate=default(DateTime);
            String DeviceUser = String.Empty;
            String DevicePassword = String.Empty;
            string Gender = string.Empty;
            Classes.ClsWelchAllynECGLayer ObjWelchallynlayer = null;
            try
            {
                RemoveFromProcess();
                ObjWelchallynlayer = new Classes.ClsWelchAllynECGLayer(gloConnectionString);
                dtGetPatientdemogrphicData = ObjWelchallynlayer.WA_GetPationtDemograpicData(nPatientID);
                ObjWelchallynlayer.Get_LoginDetails(nUserId, ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice , out DeviceUser, out DevicePassword);    
                if (dtGetPatientdemogrphicData != null && dtGetPatientdemogrphicData.Rows.Count == 1)
                {
                    LastName = dtGetPatientdemogrphicData.Rows[0]["sLastName"].ToString();
                    MidName = dtGetPatientdemogrphicData.Rows[0]["sMiddleName"].ToString();
                    FirstName = dtGetPatientdemogrphicData.Rows[0]["sFirstName"].ToString();
                    DateTime.TryParse(dtGetPatientdemogrphicData.Rows[0]["dtDOB"].ToString(), out BirthDate);
                    Gender = dtGetPatientdemogrphicData.Rows[0]["sGender"].ToString();
                    PatientCode = ObjWelchallynlayer.Retrive_ExternalPatientCode(nPatientID);
                    if (PatientCode.Trim().Length <= 0)
                    {
                         PatientCode = dtGetPatientdemogrphicData.Rows[0]["sPatientCode"].ToString();
                         if (!ObjWelchallynlayer.InsertExternalID(nPatientID, PatientCode))
                         {
                             _IsdeviceCalled = false;
                             return _IsdeviceCalled;
                         }

                    }
                    if (!WA_Invoke())
                    {
                        
                       _IsdeviceCalled = false;
                        return _IsdeviceCalled;

                    }

                    AddToProcess();
                    if (!WA_Login(DeviceUser, DevicePassword))
                    {  
                        _IsdeviceCalled = false;
                        return _IsdeviceCalled;
                    }
                    MDW.Minimize(); 
                    WA_RegisterPatient(PatientCode, FirstName, MidName, LastName, BirthDate, Gender);
                    MDW.SelectPatient(PatientCode);
                    MDW.LockPatient();
                    _IsdeviceCalled = true;
                    return _IsdeviceCalled;

                }
                else
                {
                    _IsdeviceCalled = false;
                }



            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);   
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
                _IsdeviceCalled = false;
            }
           finally
            {
                PatientCode = string.Empty;
                LastName = string.Empty;
                MidName = string.Empty;
                FirstName = string.Empty;
                if (dtGetPatientdemogrphicData != null)
                {
                    dtGetPatientdemogrphicData.Dispose();
                    dtGetPatientdemogrphicData = null;
                }
                if (ObjWelchallynlayer != null)
                {
                    ObjWelchallynlayer.Dispose();
                    ObjWelchallynlayer = null;
                }
                if (_IsdeviceCalled)
                {
                    lblMainMsg.Text = "Connected to WelchAllyn ECG Device.";
                    LblToolMsg.Text = "You must close WelchAllyn application to access gloEMR"; 
                }
             }
            return _IsdeviceCalled; 
        }
        
        private void WA_GetAllTest()
        {
            if (Invoke_WelchAllynDevice())
            {
                AddToProcess(); 
                MDW.Minimize();
                FrmWelChallynECGLoadAllTest ObjFrmWelChallynECGLoadAllTest = null;
                try
                {
                    TL = MDW.GetTests(MDW.Patient);
                    ObjFrmWelChallynECGLoadAllTest = new FrmWelChallynECGLoadAllTest(TL, nPatientID, nClicnicID, gloConnectionString);
                    ObjFrmWelChallynECGLoadAllTest.ShowDialog(this);
                    //MakeApplicationFocus();
                    SelectedTest = ObjFrmWelChallynECGLoadAllTest.SelectedTest;
                    pbBar.Maximum = ObjFrmWelChallynECGLoadAllTest.NoOfTestSelected;
                    if (ObjFrmWelChallynECGLoadAllTest.NoOfTestSelected > 0)
                    {
                        //MakeApplicationFocus();
                        Application.DoEvents();
                        bgWorker.RunWorkerAsync();
                        Application.DoEvents();

                    }
                    else
                    {
                        MDW = null;
                        WA_Quit();            
                    }
                }
                catch (Exception)
                {
                    MDW = null;
                    WA_Quit();   
                }
                finally
                {
                    if (ObjFrmWelChallynECGLoadAllTest != null)
                    {
                        ObjFrmWelChallynECGLoadAllTest.Dispose();
                        ObjFrmWelChallynECGLoadAllTest = null;
                    }
                }
            }
            else
            {
                MDW = null;
                WA_Quit();
            }

         
        }

        private void MakeApplicationFocus()
        {
            try
            {
                Microsoft.VisualBasic.Interaction.AppActivate(Application.ProductName);
                foreach (Form myForm in Application.OpenForms)
                {

                    if (myForm.TopMost)
                    {
                        myForm.TopMost = false;
                    }

                }
                this.Focus();  
                this.BringToFront();

                this.TopMost = true; 
            }
            catch (Exception)
            {
            }
        }

        private bool IsPresent(string[] selectedTests, string TestID)
        {
            bool _IsPresent = false;
            try
            {
                if (selectedTests != null)
                {

                    for (int i = 0; i < selectedTests.Length ; i++)
                    {
                        if (string.Compare(TestID, Convert.ToString(selectedTests[i])) == 0)
                        {
                            _IsPresent = true;
                            return _IsPresent;
                        }

                    }


                }
            }
            catch (Exception)
            {
                _IsPresent = false;
            }

            return _IsPresent;

        }
        
        private void AddToProcess()
        {
            System.Diagnostics.Process P=null;
            System.Diagnostics.Process[] Prcs = null;
            try
            {
                Prcs = System.Diagnostics.Process.GetProcessesByName("mdw");
                if (Prcs != null)
                {
                    if (Prcs.Length > 0)
                    {
                        P = Prcs[0];
                    }
                }
                if (P != null)
                {
                    P.WaitForInputIdle();
                    SetParent(P.MainWindowHandle, this.panel2.Handle);
                }
            }
            catch (Exception)
            { }
            finally
            {
                P = null;
            }
        }
        
        private void RemoveFromProcess()
        {
            System.Diagnostics.Process P=null;
            System.Diagnostics.Process[] Prcs = null;
            try
            {
                Prcs = System.Diagnostics.Process.GetProcessesByName("mdw");
                if (Prcs != null)
                {
                    if (Prcs.Length > 0)
                    {
                        P = Prcs[0];
                    }
                }
                if (P != null)
                {
                    P.Kill(); 
                }
            }
            catch (Exception)
            { }
            finally
            {
                P = null;
            }

        }
      
        private bool WA_Invoke()
        {
            bool _WA_Invoke = false;
            try
            {

                try
                {
                    MDW = (CCMDW.MDWClass)Marshal.CreateWrapperOfType(Marshal.GetActiveObject("CCMDW.MDW"), typeof(CCMDW.MDWClass));
                    _WA_Invoke = true;
                }
                catch (Exception)
                {
                    try
                    {
                        MDW = new CCMDW.MDWClass();
                        _WA_Invoke = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("WelchAllyn ECG SDK is not installed on this machine","gloEMR",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        _WA_Invoke = false;
                    }
                }
                if (_WA_Invoke)
                {
                    MDW.Restore();

                    while (!MDW.AppReady)
                    {
                        Thread.Sleep(500);
                    }

                    OnNewInterpDelegate = new CCMDW._ApplicationEvents_OnNewInterpretationEventHandler(MDW_OnNewInterpretation);
                    MDW.OnNewInterpretation += OnNewInterpDelegate;

                    OnNewTestDelegate = new CCMDW._ApplicationEvents_OnNewTestEventHandler(MDW_OnNewTest);
                    MDW.OnNewTest += OnNewTestDelegate;

                    OnQuitDelegate = new CCMDW._ApplicationEvents_OnQuitEventHandler(MDW_OnQuit);
                    MDW.OnQuit += OnQuitDelegate;


                    OnUpdateTestDelegate = new CCMDW._ApplicationEvents_OnUpdateTestEventHandler(MDW_OnUpdateTest);
                    MDW.OnUpdateTest += OnUpdateTestDelegate;
                    _WA_Invoke = true;
                }
            }
            catch (Exception ex)
            {
                _WA_Invoke = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }

            return _WA_Invoke;
        }

        private bool WA_Login(string DeviceUser,string Password)
        {
            bool _WA_Login = false;
            try
            {
                if (DeviceUser.Trim().Length <= 0 || Password.Trim().Length <= 0)
                {
                    _WA_Login = false;
                    MessageBox.Show("user not Configured");  
                }
                else
                {
                     MDW.Login(DeviceUser, Password);
                     if (MDW.LoggedOn)
                     {
                         AddToProcess();
                         _WA_Login=true; 
                     }
                     else
                     {
                         //MessageBox.Show("user not Configured");
                         _WA_Login = false;
                     }
                 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
                _WA_Login = false; 
            }

            return _WA_Login;
        }

        private bool WA_RegisterPatient(String PatientCode, String FirstName, String MiddleName, String LastName, DateTime BirthDate, String gender)
        {
            bool _WA_RegisterPatient = false;
            CcBase.PatientClass Objpatient;
            try
            {
                if (MDW.PatientExists(PatientCode))
                {
                    MDW.SelectPatient(PatientCode.Trim());
                    Objpatient = (CcBase.PatientClass)Marshal.CreateWrapperOfType(MDW.Patient, typeof(CcBase.PatientClass));
                }
                else
                {
                    Objpatient = (CcBase.PatientClass)Marshal.CreateWrapperOfType(new CcBase.PatientClass(), typeof(CcBase.PatientClass));
                }

                Objpatient.Number = PatientCode;

                Objpatient.Last = LastName;
                Objpatient.First = FirstName;
                Objpatient.Middle = MiddleName;
                Objpatient.Birthdate = BirthDate;
                switch (gender.Trim().ToUpper())
                {
                    case "FEMALE":
                        Objpatient.Gender = CcBase.Gender.gFemale;
                        break;
                    case "MALE":
                        Objpatient.Gender = CcBase.Gender.gMale;
                        break;
                    default:
                        Objpatient.Gender = CcBase.Gender.gUnknown;
                        break;

                }
                MDW.AddPatient(Objpatient);
                MDW.SelectPatient(PatientCode);
                _WA_RegisterPatient = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
                _WA_RegisterPatient = false;
            }
            finally
            {
                Objpatient = null;
            }
            return _WA_RegisterPatient;

        }

        private bool WA_SaveUPdateECGTest(CcBase.ITest Test,long nnECGID )
        {
            bool _WA_SaveUPdateECGTest = false;

            if ((Test != null))
            {
                nECGID = nnECGID;
                CcBase.IPersistent2 IPt = null;
                CcEcg.ECG ecg = null;
                long nVisitID = 0;
                long nExamID = 0;
                DateTime GivenDate = DateTime.Now;
                long nGroupID = 0;
                string CPTCode = string.Empty;
                string TestType = "ECG";
                string ECGPerform = string.Empty;
                DateTime OrderDate = DateTime.Now;
                string P = string.Empty;
                string QRS = string.Empty;
                string QT = string.Empty;
                string QTC = string.Empty;
                string QRSAxis = string.Empty;
                string PAxis = string.Empty;
                string TAxis = string.Empty;
                string Inerpretation = string.Empty;
                string OrderInPhysician = string.Empty;
                string ReviewInPhysician = string.Empty;
                DateTime ReviewDate = DateTime.Now;
                bool AddDt = true;
                long MachineID = 0;
                string OrderId = string.Empty;
                string TestId = string.Empty;
                string ExternalCode = "Test Completed";
                string device = "WelchAllyn";
                DataTable DtECGTestInfrmation = null;
                Classes.ClsWelchAllynECGLayer ObjClsWelchAllynECGLayer = null;
                try
                {
                    IPt = (CcBase.IPersistent2)Test;
                    ecg = (CcEcg.ECG)Test;

                 
                    if ((IPt != null) & (ecg != null))
                    {
                   
                        ObjClsWelchAllynECGLayer = new Classes.ClsWelchAllynECGLayer(gloConnectionString);

                        if (nECGID == 0)
                        {
                            nECGID = ObjClsWelchAllynECGLayer.WA_ECGID(IPt.GetIdAsString());
                        }

                        if (nECGID != 0)
                        {
                          
                            DtECGTestInfrmation = ObjClsWelchAllynECGLayer.WA_GetECGTestInformation(nECGID, nPatientID);
                            if (DtECGTestInfrmation != null && DtECGTestInfrmation.Rows.Count == 1)
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
                              
                            }
                        }

                        OrderDate = ecg.DateTime;
                        GivenDate = ecg.DateTime;
                        if (nVisitID == 0)
                        {
                            nVisitID = ObjClsWelchAllynECGLayer.getVisitID(ecg.DateTime, nPatientID, gloConnectionString);
                            
                        }
                       

                        TestId = IPt.GetIdAsString();
                        OrderId = TestId;
                        P = Convert.ToString(ecg.PQDuration() / 1000);
                        QRS = Convert.ToString(ecg.QRSDuration() / 1000);
                        QT = Convert.ToString(ecg.QTDuration() / 1000);
                        QTC = Convert.ToString(ecg.QTcDuration(CcEcg.QTcMethod.qtcHodges) / 1000);
                        QRSAxis = Convert.ToString(ecg.QRS.Axis);
                        PAxis = Convert.ToString(ecg.P.Axis);
                        TAxis = Convert.ToString(ecg.T.Axis);
                     
                        if (Test.Interpretations.Count > 0)
                        {
                            Inerpretation = Test.Interpretations[Test.Interpretations.Count - 1].Text;
                        }

                        nECGID = ObjClsWelchAllynECGLayer.WA_SaveUPdateECGTest(nECGID, nPatientID, nExamID, nVisitID, nClinicid, GivenDate, nGroupID, CPTCode, TestType, ECGPerform, OrderDate, P, QT, QTC, QRS, PAxis, QRSAxis, TAxis, Inerpretation, OrderInPhysician, ReviewInPhysician, ReviewDate, AddDt, MachineID, OrderId, TestId, ExternalCode, device);

                        if (nECGID > 0)
                        {
                            _WA_SaveUPdateECGTest = true;
                        }
                        else
                        {
                            _WA_SaveUPdateECGTest = false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    ex = null;
                    _WA_SaveUPdateECGTest = false;
                }
                finally
                {
                    //intps = null;
                    IPt = null;
                    ecg = null;
                    nVisitID = 0;
                    nExamID = 0;
                    GivenDate = default(DateTime);
                    nGroupID = 0;
                    CPTCode = string.Empty;
                    TestType = string.Empty;
                    ECGPerform = string.Empty;
                    OrderDate = default(DateTime);
                    P = string.Empty;
                    QRS = string.Empty;
                    QT = string.Empty;
                    QTC = string.Empty;
                    QRSAxis = string.Empty;
                    PAxis = string.Empty;
                    TAxis = string.Empty;
                    Inerpretation = string.Empty;
                    OrderInPhysician = string.Empty;
                    ReviewInPhysician = string.Empty;
                    ReviewDate = default(DateTime);
                    AddDt = true;
                    MachineID = 0;
                    OrderId = string.Empty;
                    TestId = string.Empty;
                    ExternalCode = string.Empty;
                    device = string.Empty;
                    if (DtECGTestInfrmation != null)
                    {
                        DtECGTestInfrmation.Dispose();
                        DtECGTestInfrmation = null;
                    }
                }

            }
            return _WA_SaveUPdateECGTest;
        }

        private bool WA_InsertExternalCode(long PatientID, string PatientCode)
        {
            bool _WA_InsertExternalCode = false;
            ClsWelchAllynECGLayer ObjClsWelchAllynECGLayer = null;
            try
            {
                ObjClsWelchAllynECGLayer = new ClsWelchAllynECGLayer(gloConnectionString);
                _WA_InsertExternalCode=ObjClsWelchAllynECGLayer.InsertExternalID(PatientID, PatientCode); 

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
                _WA_InsertExternalCode = false;
            }
            finally
            {
                if (ObjClsWelchAllynECGLayer != null)
                {
                    ObjClsWelchAllynECGLayer.Dispose();
                    ObjClsWelchAllynECGLayer = null;
                }

            }
            
            return _WA_InsertExternalCode;
        }


        #endregion "Functions"



        #region  "WelchAllyn Events"

        private void MDW_OnNewInterpretation(CcBase.ITest Test)
        {
            try
            {
                if (Test != null)
                {
                    if (Test.TestType == CcBase.TestType.ttECG)
                    {
                        WA_SaveUPdateECGTest(Test,0);
                        IsECGTestDataChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
        }

        private void MDW_OnNewTest(CcBase.ITest Test)
        {
            //CcBase.IPersistent2 IPt = null;
            try
            {
                if (Test != null)
                {
                    if (Test.TestType == CcBase.TestType.ttECG)
                    {
                        WA_SaveUPdateECGTest(Test,0);
              //          IPt = (CcBase.IPersistent2)Test;
                        if (MDW != null)
                        {
                //           MDW.SelectTest(IPt.GetIdAsString());   
                            MDW.Restore();  
                            MDW.Maximize();
                            MDW.BringToFront();

                        }
                        IsECGTestDataChanged = true;
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
                //IPt = null;
            }
         

        }

        private void MDW_OnQuit()
        {
            System.Diagnostics.Process P = null;
            System.Diagnostics.Process[] Prcs = null;
            try
            {
                Prcs = System.Diagnostics.Process.GetProcessesByName("mdw");
                if (Prcs != null)
                {
                    if (Prcs.Length > 0)
                    {
                        P = Prcs[0];
                    }
                }
                if (P != null)
                {
                    P.Kill();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (P != null)
                {
                    P.Dispose();
                    P = null;
                }
            }
            MDW = null;
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }



        private void MDW_OnUpdateTest(CcBase.ITest Test)
        {
            try
            {           
                if (Test != null)
                {
                    if (Test.TestType == CcBase.TestType.ttECG)
                    {
                        WA_SaveUPdateECGTest(Test,0);
                        IsECGTestDataChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
        }
              


        #endregion "WelchAllyn Events"

        private void btn_InternetFailed_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }



        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
           DataTable dtECGID = null;
           CcBase.IPersistent2 IPt = null;
           long nECGID = 0;
           panel1.Visible = false;
           panel1.SendToBack(); 
           panel3.Visible = true;
           panel3.Dock = DockStyle.Fill;
           panel3.BringToFront();
           try
           {
               dtECGID = ClsWelchAllynECGLayerGenral.Retrive_ECGID(nClicnicID, nPatientID, gloConnectionString);
               foreach (CcBase.ITest Test in TL)
               {
                   try
                   {
                       if (Test.TestType == CcBase.TestType.ttECG)
                       {
                           IPt = (CcBase.IPersistent2)Test;
                           bgWorker.ReportProgress(pbBar.Value);
                           if (IsPresent(SelectedTest, IPt.GetIdAsString()))
                           {
                               nECGID = ClsWelchAllynECGLayerGenral.Get_ECGID(dtECGID, IPt.GetIdAsString());
                               MDW.SelectTest(IPt.GetIdAsString());
                               WA_SaveUPdateECGTest(MDW.CurrentTest(), nECGID);
                               IsECGTestDataChanged = true;
                               bgWorker.ReportProgress(pbBar.Value + 1);
                               System.Threading.Thread.Sleep(1000);
                           } // end IsPresent

                       } // end if test type ECG
                   }
                   catch (Exception)
                   {}

               } // end for
               MDW = null;
               WA_Quit();
           }
           catch (Exception ex)
           {
               MDW = null;
               WA_Quit();
               gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
               ex = null;
           }
           finally
           {
               IPt = null;
               nECGID = 0;
               if (dtECGID != null)
               {
                   dtECGID.Dispose();
                   dtECGID = null;
               }
              
           }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Application.DoEvents();    
            pbBar.Value = e.ProgressPercentage;
            lblGetAllTest.Refresh();
            if (e.ProgressPercentage <= pbBar.Maximum)
            {
                lblGetAllTest.Text = "Importing/updating ECG test(s) " + pbBar.Value + " of " + pbBar.Maximum + "";
                lblGetAllTest.Refresh();
            }
          
            Application.DoEvents(); 
            
        } 


    }
}
