using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics.Design;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using gloBilling;
using gloContacts;
using gloReports;
using gloPM.Forms;
using System.Resources;
using gloCardScanning;
using gloSettings;
using gloSSRSApplication;
using System.Threading;
using System.Text.RegularExpressions;
using gloGlobal;
using gloEMR.Help;
using gloPM.Classes;
using C1.Win.C1FlexGrid;
using gloTaskMail;
using gloPMGeneral;
using gloGallery;
using gloPatientPortalCommon;
using System.Windows.Interop;
using System.Linq;
using System.Diagnostics;


namespace gloPM
{
    public partial class frmDashBoardMain : gloAUSLibrary.MasterForm
    {
        public delegate void HotKeyPressedEventHandler(object sender, HotKeyPressedEventArgs e);
        public event HotKeyPressedEventHandler HotKeyPressed;
        gloWord.WordDialogBoxBackgroundCloser myDialogCloser = null;
        ArrayList myExclusionStrings = new ArrayList();

        public frmDashBoardMain()
        {
            this.Opacity = 0;

            InitializeComponent();
            try
            {
                m_hotKeys = new HotKeyCollection(this);
                SetHelpBuilder();
                RegisterHotKey();
            }
            catch
            {

            }

        }
        ToolTip ToolTip1 = null;
        public void SetHelpBuilder()
        {
            if (Program.gstrHelpProvider == "Client")
            {
                gloEMR.Help.HelpComponent.blnbuildmode = false;
                this.HelpComponent1.Mode = HelpComponent.ProviderMode.Client;
            }
            else
            {
                gloEMR.Help.HelpComponent.blnbuildmode = true;
                this.HelpComponent1.Mode = HelpComponent.ProviderMode.Builder;
            }
        }

        //Context Menu Events of Patient DTL:: at bottom For Prior Authorization,Copay&Coverage-
        Int64 tempInsId = 0;
        String tempInsName = "";
        decimal copayAmount = 0;
        decimal coveragePercent = 0;
        //MaheshB For Search
        bool _SearchFlag = false;
        bool _AppClick = false;
        public bool _Iscalprocessstarted = true;
        //Mitesh For report Type PM/EMR
        private string _sReportType = "gloPM";
        private bool _IsColored = false;
        public bool _IsEnableSSRSReports = true;
        bool _IsExemptFromReport = false;
        //bool _isBadDebtWithLockChart = false;
        System.Diagnostics.Process calProcess;
        Font Font_Regular = gloGlobal.clsgloFont.gFont;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        Font Font_Bold = gloGlobal.clsgloFont.gFont_BOLD;//new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        Font Strikeout = gloGlobal.clsgloFont.gFont_STRIKEOUT;//new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        // For LockScreen
        public struct LastInputInfo
        {
            public Int32 cbSize;
            public Int32 dwTime;
        }
        //
        // public bool HasExited { get; }
        //

        [DllImport("User32.dll")]
        public static extern Int32 GetLastInputInfo(ref LastInputInfo liLastInputInfo);
        [DllImport("winmm.dll")]
        public static extern Int64 timeGetTime();
        //[DllImport("kernel32.dll")]
        //public static extern Int64 GetTickCount();
        //
        public Boolean IsSingleSignOn { get; set; }
        //bool blnAlwaysSyncPatient = true;
        string SyncPatientSetting = null;
        #region "Internal Variables & Declaration"

        C1.Win.C1FlexGrid.CellStyle style;

        private Int64 _PatientProviderId = 0;

        private Int64 _CurrentPatientId = 0;
        private Int64 _ChkOutPatientId = 0; //Bug : 00000775: Patient Card. Used separate variable for right click -> check out scenario
        private Int64 ReturnPatientId = 0;
        private bool _IsExitCall = false;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public System.Collections.ArrayList arrDashBoardToolStrip = null;
        public System.Collections.ArrayList arrPatientdetailsToolStrip = null;
        ArrayList nBlinkingCells = new ArrayList();

        //gloListControl.gloListControl oListControl;
        gloPatient.PatientCards oPatientCards = new gloPatient.PatientCards();
        gloUserRights.ClsgloUserRights oClsgloUserRights = null;
        //Reminder
        gloReminder.gloReminder ogloReminder = new gloReminder.gloReminder();
        frmCopayDistributionList CopayDistribution = new frmCopayDistributionList();
        MdiClient mdiClient = null;
        private bool _IsFromAppointment = false;
        // Enumerator for Patient Details Information
        enum PatientDetails
        {
            PatientInsurance = 1,
            PatientAppointments = 2,
            PatientReferrals = 3,
            PatientProcedures = 4,
            PatientBilling = 5,
            PatientBalance = 6,
            PriorAuthorization = 7,
            PatientCases = 8,
            PatientTasks = 9,
            PatientDocuments = 10,
            PatientNYWorkersCompForms = 11
        }


        # region Grid Columns

        //added for patient task in patient detail panel

        const int Col_View_taskID = 0;
        const int Col_View_FollowUpID = 1;
        const int Col_View_PriorityID = 2;
        const int Col_View_Priority = 3;
        const int Col_View_Subject = 4;
        const int Col_View_Owner = 5;
        const int Col_View_Status = 6;
        const int Col_View_DueDate = 7;
        const int Col_View_startdate = 8;
        const int Col_View_ProviderName = 9;
        const int Col_View_Description = 10;
        const int Col_View_FollowUpIcon = 11;


        # endregion Grid Columns

        #region " Column Constants View Docs"

        const int COL_D_CAT_ID = 0;
        const int COL_D_CAT_NAME = 1;
        const int COL_D_CAT_NOTEFLAG = 2;
        const int COL_D_CAT_EXTRAFLAG = 3;
        const int COL_D_CAT_SOURCEMACHINE = 4;
        const int COL_D_CAT_SYSTEMFOLDER = 5;
        const int COL_D_CAT_CONTAINER = 6;
        const int COL_D_CAT_CATEGORY = 7;
        const int COL_D_CAT_PATIENTID = 8;
        const int COL_D_CAT_YEAR = 9;
        const int COL_D_CAT_MONTH = 10;
        const int COL_D_CAT_SOURCEBIN = 11;
        const int COL_D_CAT_INUSED = 12;
        const int COL_D_CAT_USEDMACHINE = 13;
        const int COL_D_CAT_USEDTYPE = 14;
        const int COL_D_CAT_PATH = 15;
        const int COL_D_CAT_COLTYPE = 16;
        const int COL_D_CAT_FILENAME = 17;
        const int COL_D_CAT_MACHINEID = 18;
        const int COL_D_CAT_VERSIONNO = 19;
        const int COL_D_CAT_ISREVIWED = 20;
        const int COL_D_CAT_REVIWEDFLAG = 21;
        const int COL_View_CategoryHidden = 22;
        const int COL_View_Category = 23;
        const int COL_View_Month = 24;
        const int COL_View_DocumentName = 25;
        const int COL_View_NOTEFLAG = 26;
        const int COL_View_REVIWEDFLAG = 27;
        const int Col_view_Count = 28;

        #endregion " Column Constants "
        // By Default Select the Patient Isurance

        PatientDetails _SelcetedPatient = PatientDetails.PatientInsurance;
        private const int SB_BOTH = 3;
        private const int WM_NCCALCSIZE = 0x83;
        #endregion

        #region "Variables for CM"

        public Thread thrdQueue = null;

        #endregion


        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            try
            {



                if ((m.Msg == UnmanagedMethods.WM_ACTIVATEAPP))
                {
                    //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Initialize, " HOT Key WM_ACTIVATEAPP " + m.Msg + " - " + m.WParam.ToInt32 + " - " + m.ToString + " - timerLockScreen.Enabled =" + timerLockScreen.Enabled, gloAuditTrail.ActivityOutCome.Success);
                    //UpdateVoiceLog(" HOT Key WM_ACTIVATEAPP " + m.Msg + " - " + m.WParam.ToInt32 + " - " + m.ToString + " - timerLockScreen.Enabled =" + timerLockScreen.Enabled);
                    if (m.WParam.ToInt32() == 1 & timerLockScreen.Enabled == false)
                    {
                        //UpdateLog("WndProc: If m.WParam.ToInt32() = 1 And timerLockScreen.Enabled = False Then");
                        UnRegisterMyHotKey();
                    }
                    else if (m.WParam.ToInt32() == 1)
                    {
                        //UpdateVoiceLog(" HOT Key WM_ACTIVATEAPP " + m.Msg + " - " + m.WParam.ToInt32 + " - " + m.ToString + " RegisterMyHotKey ");
                        //UpdateLog("WndProc: ElseIf m.WParam.ToInt32() = 1 Then");
                        RegisterHotKey();
                        //EnableProtection();
                    }
                    else if (m.WParam.ToInt32() == 0)
                    {
                        //UpdateVoiceLog(" HOT Key WM_ACTIVATEAPP " + m.Msg + " - " + m.WParam.ToInt32 + " - " + m.ToString + " UnRegisterMyHotKey ");
                        //UpdateLog("WndProc: ElseIf m.WParam.ToInt32() = 0 Then  , timerLockScreen.Enabled =" + timerLockScreen.Enabled);
                        UnRegisterMyHotKey();
                        //DisableProtection();
                    }


                }
                else if ((m.Msg == UnmanagedMethods.WM_HOTKEY))
                {
                    //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Initialize, " HOT Key WM_HOTKEY " + m.Msg + " -- " + m.ToString, gloAuditTrail.ActivityOutCome.Success);
                    //UpdateVoiceLog(" HOT Key WM_HOTKEY " + m.Msg + " -- " + m.ToString);
                    int hotKeyId = m.WParam.ToInt32();
                    switch (hotKeyId)
                    {
                        case UnmanagedMethods.IDHOT_SNAPDESKTOP:
                            //System.EventArgs e = new System.EventArgs();
                            //if (PrintDesktopPressed != null) {
                            //	PrintDesktopPressed(this, e);
                            //}
                            //int ii = 0;

                            break;
                        case UnmanagedMethods.IDHOT_SNAPWINDOW:
                            //	System.EventArgs e = new System.EventArgs();
                            //	if (PrintWindowPressed != null) {
                            //	PrintWindowPressed(this, e);
                            //}
                            //int kk = 0;

                            break;
                        default:
                            //	UpdateLog("WndProc: ElseIf (m.Msg = UnmanagedMethods.WM_HOTKEY) Then, Case Else");
                            //HotKey htk = default(HotKey);
                            try
                            {
                                foreach (HotKey htk in m_hotKeys)
                                {
                                    if ((htk.AtomId.Equals(m.WParam)))
                                    {
                                        HotKeyPressedEventArgs e = new HotKeyPressedEventArgs(htk);
                                        if (HotKeyPressed != null)
                                        {
                                            HotKeyPressed(this, e);
                                        }
                                        //SLR: FRee e
                                        if (e != null)
                                        {
                                            e = null;
                                        }
                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }
                            }
                            catch
                            {

                            }

                            break;
                    }
                }




            }
            catch
            {
            }






            if (mdiClient != null)
            {
                try
                {
                    ShowScrollBar(mdiClient.Handle, SB_BOTH, 0 /*Hide the ScrollBars*/);
                    //  if(m.WParam.ToInt32()==1)
                    //   RegisterHotKey();
                    // 
                }

                catch { }

            }










        }







        public void UnRegisterMyHotKey()
        {
            //' If HOT Keys are Register then UnRegister HOT Keys
            //  if (IsActivated == true)
            // {
            try
            {
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Dispose, "UnRegisterHotKey started", gloAuditTrail.ActivityOutCome.Success);
                //UpdateVoiceLog("UnRegisterHotKey started ")

                this.HotKeyPressed -= hotKey_Pressed;


                HotKeys.Clear();
                // IsActivated = false;
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Dispose, "UnRegisterHotKey finished ", gloAuditTrail.ActivityOutCome.Success);
                //UpdateVoiceLog("UnRegisterHotKey finished ")
            }
            catch
            {

            }
          
        }
        
        public void SelectRecentVisitedPatient(Int64 SelectedPatientID)
        {
            if (_CurrentPatientId != SelectedPatientID)
            {
                _CurrentPatientId = SelectedPatientID;
                FillSelectedPatient();
               
              
                gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
                gloCntrlPatient.PatientID = _CurrentPatientId;

                gloCntrlPatient.HighlightPatient(_CurrentPatientId);
            }
          }


        private void GetMedicalCategoryImage(DataTable dtMedCat = null)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            DataTable dtMedColor = null;
            string strcolor = "";
            string strheaderColor = string.Empty;
            string strbottompanelcolr = string.Empty;

            try
            {
                var _with1 = oDB;
                oDB.Connect(false);
                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@tvpMedicalCategory", dtMedCat, ParameterDirection.Input, SqlDbType.Structured);
                oPara.Add("@PatientId", _CurrentPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPatientMedicalCategoryColor", oPara, out dtMedColor);
                oDB.Disconnect();
                if ((dtMedColor != null))
                {
                    if ((dtMedColor.Rows.Count > 0))
                    {
                        strcolor = Convert.ToString(dtMedColor.Rows[0]["ImageColor"]);
                        strheaderColor = Convert.ToString(dtMedColor.Rows[0]["DashboardDemoHeaderColor"]);
                        strbottompanelcolr = Convert.ToString(dtMedColor.Rows[0]["DashboardDemoBottomColor"]);
                    }
                }

                if ((!string.IsNullOrEmpty(strheaderColor.Trim())))
                {
                    pnlPatient_Demo.InnerContainerFormatStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(strheaderColor);
                    pnlPatient_Demo.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical;
                    pnlPatient_Demo.InnerContainerFormatStyle.BackColorGradient = System.Drawing.ColorTranslator.FromHtml(strbottompanelcolr);

                }
                else
                {
                    pnlPatient_Demo.InnerContainerFormatStyle.BackColor = Color.FromArgb(255, 255, 255);
                    pnlPatient_Demo.InnerContainerFormatStyle.BackColorGradient = Color.FromArgb(227, 241, 255);
                }

                if (strcolor.Contains("Dark"))
                {
                    SetLabelColorForDarkBlue(true);
                }
                else
                {
                    SetLabelColorForDarkBlue(false);
                }
            }
            catch (Exception)// ex)
            {
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if ((dtMedColor != null))
                {
                    dtMedColor.Dispose();
                    dtMedColor = null;
                }
                if ((dtMedCat != null))
                {
                    dtMedCat.Dispose();
                    dtMedCat = null;
                }
                if (oPara != null)
                {
                    oPara.Clear();
                    oPara = null;
                }
            }
        }

        private void SetLabelColorForDarkBlue(bool blndrkblue)
        {
            if (lblPatMedCat.Text == "")
            {
                lblPD_Age.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Code.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Name.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Address.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_DateofBirth.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Gender.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_HomePhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_MobilePhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_FaxPhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Email.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Pharmacy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lbl_PD_Status.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Physician.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Referral.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PCP_Mobile.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PCP_Phone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lbl_PD_Status1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");

                lblLanguage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEthinicity.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblRace.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PCP_Phone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PCP_Mobile.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Referral.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Physician.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Pharmacy.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblProvider.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEMMobile.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEMPhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEMContact.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Email.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_FaxPhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_MobilePhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_HomePhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Code.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Name.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Code.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblMedCatCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblstCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblLanguageCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEthinicityCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblRaceCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PCP_PhoneCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PCP_MobileCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_ReferralCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PhysicianCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_PharmacyCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblProviderCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEMMobileCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEMPhoneCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblEMContactCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_EmailCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_FaxPhoneCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_MobilePhoneCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_HomePhoneCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblGenderCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_DOBCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Code.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Name.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPD_Code.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                gb_Demographics.FormatStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                gb_Demographics.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPatMedCat.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblBusinessCenter.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblWorkPhone.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblOccupation.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblOccupationCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblWorkPhoneCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblBusinessCenterCaption.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblPrimaryInsurance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lblSecondaryInsurance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                lbl_TertiaryInsurance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                Label92.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                Label91.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
                Label90.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1f497d");
            }
            else
            {
                if (blndrkblue == true)
                {
                    lblPD_Age.ForeColor = Color.White;
                    lblPD_Code.ForeColor = Color.White;
                    lblPD_Name.ForeColor = Color.White;
                    lblPD_Address.ForeColor = Color.White;
                    lblPD_DateofBirth.ForeColor = Color.White;
                    lblPD_Gender.ForeColor = Color.White;
                    lblPD_HomePhone.ForeColor = Color.White;
                    lblPD_MobilePhone.ForeColor = Color.White;
                    lblPD_FaxPhone.ForeColor = Color.White;
                    lblPD_Email.ForeColor = Color.White;
                    lblPD_Pharmacy.ForeColor = Color.White;
                    lbl_PD_Status.ForeColor = Color.White;
                    lblPD_Physician.ForeColor = Color.White;
                    lblPD_Referral.ForeColor = Color.White;
                    lblPD_PCP_Mobile.ForeColor = Color.White;
                    lblPD_PCP_Phone.ForeColor = Color.White;
                    lbl_PD_Status1.ForeColor = Color.White;


                    lblLanguage.ForeColor = Color.White;
                    lblEthinicity.ForeColor = Color.White;
                    lblRace.ForeColor = Color.White;
                    lblPD_PCP_Phone.ForeColor = Color.White;
                    lblPD_PCP_Mobile.ForeColor = Color.White;
                    lblPD_Referral.ForeColor = Color.White;
                    lblPD_Physician.ForeColor = Color.White;
                    lblPD_Pharmacy.ForeColor = Color.White;
                    lblProvider.ForeColor = Color.White;
                    lblEMMobile.ForeColor = Color.White;
                    lblEMPhone.ForeColor = Color.White;
                    lblEMContact.ForeColor = Color.White;
                    lblPD_Email.ForeColor = Color.White;
                    lblPD_FaxPhone.ForeColor = Color.White;
                    lblPD_MobilePhone.ForeColor = Color.White;
                    lblPD_HomePhone.ForeColor = Color.White;
                    lblPD_Code.ForeColor = Color.White;
                    lblPD_Name.ForeColor = Color.White;
                    lblPD_Code.ForeColor = Color.White;
                    lblMedCatCaption.ForeColor = Color.White;
                    lblstCaption.ForeColor = Color.White;
                    lblLanguageCaption.ForeColor = Color.White;
                    lblEthinicityCaption.ForeColor = Color.White;
                    lblRaceCaption.ForeColor = Color.White;
                    lblPD_PCP_PhoneCaption.ForeColor = Color.White;
                    lblPD_PCP_MobileCaption.ForeColor = Color.White;
                    lblPD_ReferralCaption.ForeColor = Color.White;
                    lblPD_PhysicianCaption.ForeColor = Color.White;
                    lblPD_PharmacyCaption.ForeColor = Color.White;
                    lblProviderCaption.ForeColor = Color.White;
                    lblEMMobileCaption.ForeColor = Color.White;
                    lblEMPhoneCaption.ForeColor = Color.White;
                    lblEMContactCaption.ForeColor = Color.White;
                    lblPD_EmailCaption.ForeColor = Color.White;
                    lblPD_FaxPhoneCaption.ForeColor = Color.White;
                    lblPD_MobilePhoneCaption.ForeColor = Color.White;
                    lblPD_HomePhoneCaption.ForeColor = Color.White;
                    lblGenderCaption.ForeColor = Color.White;
                    lblPD_DOBCaption.ForeColor = Color.White;
                    lblPD_Code.ForeColor = Color.White;
                    lblPD_Name.ForeColor = Color.White;
                    lblPD_Code.ForeColor = Color.White;
                    gb_Demographics.FormatStyle.ForeColor = Color.White;
                    gb_Demographics.BorderColor = Color.White;
                    lblPatMedCat.ForeColor = Color.White;
                    lblBusinessCenter.ForeColor = Color.White;
                    lblWorkPhone.ForeColor = Color.White;
                    lblOccupation.ForeColor = Color.White;
                    lblOccupationCaption.ForeColor = Color.White;
                    lblWorkPhoneCaption.ForeColor = Color.White;
                    lblBusinessCenterCaption.ForeColor = Color.White;
                    lblPrimaryInsurance.ForeColor = Color.White;
                    lblSecondaryInsurance.ForeColor = Color.White;
                    lbl_TertiaryInsurance.ForeColor = Color.White;
                    Label92.ForeColor = Color.White;
                    Label91.ForeColor = Color.White;
                    Label90.ForeColor = Color.White;
                }
                else
                {
                    lblPD_Age.ForeColor = Color.Black;
                    lblPD_Code.ForeColor = Color.Black;
                    lblPD_Name.ForeColor = Color.Black;
                    lblPD_Address.ForeColor = Color.Black;
                    lblPD_DateofBirth.ForeColor = Color.Black;
                    lblPD_Gender.ForeColor = Color.Black;
                    lblPD_HomePhone.ForeColor = Color.Black;
                    lblPD_MobilePhone.ForeColor = Color.Black;
                    lblPD_FaxPhone.ForeColor = Color.Black;
                    lblPD_Email.ForeColor = Color.Black;
                    lblPD_Pharmacy.ForeColor = Color.Black;
                    lbl_PD_Status.ForeColor = Color.Black;
                    lblPD_Physician.ForeColor = Color.Black;
                    lblPD_Referral.ForeColor = Color.Black;
                    lblPD_PCP_Mobile.ForeColor = Color.Black;
                    lblPD_PCP_Phone.ForeColor = Color.Black;
                    lbl_PD_Status1.ForeColor = Color.Black;

                    lblLanguage.ForeColor = Color.Black;
                    lblEthinicity.ForeColor = Color.Black;
                    lblRace.ForeColor = Color.Black;
                    lblPD_PCP_Phone.ForeColor = Color.Black;
                    lblPD_PCP_Mobile.ForeColor = Color.Black;
                    lblPD_Referral.ForeColor = Color.Black;
                    lblPD_Physician.ForeColor = Color.Black;
                    lblPD_Pharmacy.ForeColor = Color.Black;
                    lblProvider.ForeColor = Color.Black;
                    lblEMMobile.ForeColor = Color.Black;
                    lblEMPhone.ForeColor = Color.Black;
                    lblEMContact.ForeColor = Color.Black;
                    lblPD_Email.ForeColor = Color.Black;
                    lblPD_FaxPhone.ForeColor = Color.Black;
                    lblPD_MobilePhone.ForeColor = Color.Black;
                    lblPD_HomePhone.ForeColor = Color.Black;
                    lblPD_Code.ForeColor = Color.Black;
                    lblPD_Name.ForeColor = Color.Black;
                    lblPD_Code.ForeColor = Color.Black;
                    lblMedCatCaption.ForeColor = Color.Black;
                    lblstCaption.ForeColor = Color.Black;
                    lblLanguageCaption.ForeColor = Color.Black;
                    lblEthinicityCaption.ForeColor = Color.Black;
                    lblRaceCaption.ForeColor = Color.Black;
                    lblPD_PCP_PhoneCaption.ForeColor = Color.Black;
                    lblPD_PCP_MobileCaption.ForeColor = Color.Black;
                    lblPD_ReferralCaption.ForeColor = Color.Black;
                    lblPD_PhysicianCaption.ForeColor = Color.Black;
                    lblPD_PharmacyCaption.ForeColor = Color.Black;
                    lblProviderCaption.ForeColor = Color.Black;
                    lblEMMobileCaption.ForeColor = Color.Black;
                    lblEMPhoneCaption.ForeColor = Color.Black;
                    lblEMContactCaption.ForeColor = Color.Black;
                    lblPD_EmailCaption.ForeColor = Color.Black;
                    lblPD_FaxPhoneCaption.ForeColor = Color.Black;
                    lblPD_MobilePhoneCaption.ForeColor = Color.Black;
                    lblPD_HomePhoneCaption.ForeColor = Color.Black;
                    lblGenderCaption.ForeColor = Color.Black;
                    lblPD_DOBCaption.ForeColor = Color.Black;
                    lblPD_Code.ForeColor = Color.Black;
                    lblPD_Name.ForeColor = Color.Black;
                    lblPD_Code.ForeColor = Color.Black;
                    gb_Demographics.FormatStyle.ForeColor = Color.Black;
                    gb_Demographics.BorderColor = Color.Black;
                    lblPatMedCat.ForeColor = Color.Black;
                    lblBusinessCenter.ForeColor = Color.Black;
                    lblWorkPhone.ForeColor = Color.Black;
                    lblOccupation.ForeColor = Color.Black;
                    lblOccupationCaption.ForeColor = Color.Black;
                    lblWorkPhoneCaption.ForeColor = Color.Black;
                    lblBusinessCenterCaption.ForeColor = Color.Black;
                    lblPrimaryInsurance.ForeColor = Color.Black;
                    lblSecondaryInsurance.ForeColor = Color.Black;
                    lbl_TertiaryInsurance.ForeColor = Color.Black;
                    Label92.ForeColor = Color.Black;
                    Label91.ForeColor = Color.Black;
                    Label90.ForeColor = Color.Black;
                }
            }
        }




        //'Start'GLO2010-0007047[BJMC]: Webcam image too small
        //'gloPicutreBoxControl
        // private byte[] _myPictureBoxControl = null;
        // private System.Drawing.Bitmap _iPhoto = null;
        //public byte[] MyPictureBoxControl
        //{
        //    get { return _myPictureBoxControl; }
        //    set { _myPictureBoxControl = value; }
        //}
        //public System.Drawing.Bitmap PatientPhoto
        //{
        //    get { return _iPhoto; }
        //    set { _iPhoto = value; }
        //}
        //'End'GLO2010-0007047[BJMC]: Webcam image too small



        #region "Form Events and Main Menu Code"
        private void frmDashBoardMain_Load(object sender, EventArgs e)
        {
            frmLoadDashboard ofrm = new frmLoadDashboard();


            DataTable _dtWordExclusionSettings = null;

            try
            {
                //Bug #55199: 00000517 : PM dashboard performance issue
                //Added changes to resolve the performance issue.
                this.Hide();

                if (gloGlobal.gloTSPrint.TerminalServer() == "RDP")
                {
                    mnuSetting_RefreshDevicesPrinters.Enabled = true;
                }
                else
                {
                    mnuSetting_RefreshDevicesPrinters.Enabled = false;
                }

                #region "loading application settings"
                ofrm.lblDescription.Text = "Loading application settings...";
                ofrm.Show();
                #endregion

                #region "loading patients"
                ofrm.lblDescription.Text = "Loading patients...";
                ofrm.SuspendLayout();
                ofrm.Refresh();
                ofrm.ResumeLayout();
                SetPatientList(Program.gnPatientID);
                #endregion

                #region "loading patient details"
                ofrm.lblDescription.Text = "Loading patients details...";
                ofrm.SuspendLayout();
                ofrm.Refresh();
                ofrm.ResumeLayout();
                //if (appSettings["PatientID"] == null)
                //{
                //    appSettings["PatientID"] = Convert.ToString(_CurrentPatientId);
                //}
                DesignGridPatientAlerts();
                GetPatientDemographicsSettings();
                FillSelectedPatient();
                #endregion




                #region "loading patient status"
                ofrm.lblDescription.Text = "Loading patient status...";
                ofrm.SuspendLayout();
                ofrm.Refresh();
                ofrm.ResumeLayout();

                FillPatientStatus();
                #endregion

                #region "loading user settings"
                ofrm.lblDescription.Text = "Loading user settings...";
                ofrm.SuspendLayout();
                ofrm.Refresh();
                ofrm.ResumeLayout();

                AssignUserRights();
                #endregion

                #region "loading toolbar settings"
                ofrm.lblDescription.Text = "Loading Toolbar settings...";
                SetToolBar();
                #endregion

                #region "loading display settings"
                ofrm.lblDescription.Text = "Loading display settings...";
                ofrm.SuspendLayout();
                ofrm.Refresh();
                ofrm.ResumeLayout();
                LoadDisplaySettings(false);
                SaveDisplaySettings(false);
                #endregion


                GetMedicalCategoryImage();


                #region "initializing word"

                try
                {
                    ofrm.lblDescription.Text = "Initializing Word...";
                    ofrm.Refresh();
                    gloWord.gloWord.InitializeWord();

                    _dtWordExclusionSettings = gloGlobal.gloPMGlobal.GetWordExclusionSettings();
                    if (_dtWordExclusionSettings != null && _dtWordExclusionSettings.Rows.Count > 0)
                    {
                        if (myExclusionStrings != null) { myExclusionStrings.Clear(); }
                        else { myExclusionStrings = new ArrayList(); }

                        for (int tableIndex = 0; tableIndex < _dtWordExclusionSettings.Rows.Count; tableIndex++)
                        {
                            if (Convert.ToString(_dtWordExclusionSettings.Rows[tableIndex]["sValue"]).Trim() != "")
                            {
                                myExclusionStrings.Add(Convert.ToString(_dtWordExclusionSettings.Rows[tableIndex]["sValue"]).ToLower());
                            }
                        }

                        if (myExclusionStrings != null && myExclusionStrings.Count > 0)
                        {
                            myDialogCloser = new gloWord.WordDialogBoxBackgroundCloser(myExclusionStrings);
                        }
                    }
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                #endregion

                #region "initializing report server"
                try
                {
                    ofrm.lblDescription.Text = "Initializing Report Server...";
                    ofrm.SuspendLayout();
                    ofrm.Refresh();
                    ofrm.ResumeLayout();
                    InitializeReportServer();

                    //Load SSRS Menu
                    Load_SSRSMenu();
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                #endregion

                #region "initializing templates"

                ofrm.lblDescription.Text = "Loading Templates ...";
                ofrm.SuspendLayout();
                ofrm.Refresh();
                ofrm.ResumeLayout();
                Fill_Templates(cmnuPatientItem_Template);

                #endregion "initializing templates"

                FillAppointmentStatusCombo();
                FillProviderCombo();
                LoadBatchEligibilitySettings();
                GetSyncPatientSetting();//Get if User specific setting is present
                //Show reminders 
                Boolean _IsReminderOn = true;
                //TODO : Read Reminder settings 
                if (_IsReminderOn == true)
                {
                    ogloReminder.Start();
                }
                LoadDMSSettings();
                //Start Timers
                tmr_Dashboard.Enabled = true;

                CheckMultipleRaceSettings();

                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                if (oSecurity.isPatientLock(gloCntrlPatient.PatientID, false))
                {
                    _CurrentPatientId = 0;
                    EnableDisableLockedPatientButtons(false);
                }
                //else if (oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, true))
                //{
                //    _CurrentPatientId = 0;
                //    EnableDisableLockedPatientButtons(true);
                //    EnableDisableBadDebtPatientButtons(false);
                //}
                if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }

                #region "Enable/Disable WC Form Tool Strip Tab"

                ShowWCFormsToolsrtipButton();

                #endregion

                #region "Enable/Disable RCM DMS Tool Strip Tab"
                //Added By Rohini Kulthe 20160127 For RCM DMS user rights implementations.
                if (gloPMGlobal.UserName.Trim() != "")
                {
                    if (oClsgloUserRights == null)
                    { oClsgloUserRights = new gloUserRights.ClsgloUserRights(gloPMGlobal.DatabaseConnectionString); }

                    oClsgloUserRights.CheckForUserRights(gloPMGlobal.UserName);
                    tsb_RCMDocs.Visible = oClsgloUserRights.RCMDocuments;
                    mnuEdit_RCMCagetory.Visible = oClsgloUserRights.RCMCategory;
                }
                else
                { gloAuditTrail.gloAuditTrail.ExceptionLog("gloPMGlobal is not initialized properly for [UserName] property", false); }

                #endregion

                #region "Load Printer Settings"

                gloPrintDialog.clsPrinterSettings.LoadPrinters(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID);

                #endregion

                #region "Cleargage Setting"

                ShowHideCleargageFileHistory();

                #endregion

                SetLicenseModule();
                ShowHideMergeScheduledAction();
                ShowCollectionAgencyRefundMenu();
                ClsAutoCoapyDistributionList.CheckInstanceForloginSameUser();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_dtWordExclusionSettings != null) { _dtWordExclusionSettings.Dispose(); _dtWordExclusionSettings = null; }


                ofrm.Hide();
                ofrm.Dispose();
                this.Show();
                this.Opacity = 100;
            }
        }

        private void ShowHideMergeScheduledAction()
        {
            gloSettings.GeneralSettings _oSettings = null;
            object _obj = null;

            try
            {
                _oSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                _oSettings.GetSetting("MergeScheduledActions", 0, 1, out _obj);
                {
                    if (_obj != null && Convert.ToString(_obj).Trim().Length > 0)
                    {
                        mnuTools_MergeScheduledActions.Visible = Convert.ToBoolean(_obj);
                    }

                }
                _oSettings = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while showing toolstrip button for Workers Comp Forms :" + ex.ToString(), true);
                ex = null;
            }
            finally
            {
                _oSettings = null;
                _obj = null;
            }
        }

        private void ShowCollectionAgencyRefundMenu()
        {
            try
            {
                if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                { mnuGo_CollectionAgencyRefund.Visible = true; }
                else
                { mnuGo_CollectionAgencyRefund.Visible = false; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while showing toolstrip button for Collection agency refund :" + ex.ToString(), true);
                ex = null;
            }

        }


        #region "Function to enable/Disable Worker Comp Tab"

        private void ShowWCFormsToolsrtipButton()
        {
            gloSettings.GeneralSettings _oSettings = null;
            object _obj = null;

            try
            {
                _oSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                _oSettings.GetSetting("EnableWorkersCompForms", 0, 1, out _obj);
                {
                    bool _ShowWCbutton = false;
                    if (_obj != null && Convert.ToString(_obj).Trim().Length > 0)
                    {
                        _ShowWCbutton = Convert.ToBoolean(_obj);
                    }
                    this.tsb_NYWCForms.Enabled = _ShowWCbutton;
                    this.tsb_NYWCForms.Visible = _ShowWCbutton;
                    if (!_ShowWCbutton)
                    {
                        ts_PatientDetail.Items.Remove(tsb_NYWCForms);
                    }
                }
                _oSettings = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while showing toolstrip button for Workers Comp Forms :" + ex.ToString(), true);
                ex = null;
            }
            finally
            {
                _oSettings = null;
                _obj = null;
            }
        }

        #endregion

        //Bug #55199: 00000517 : PM dashboard performance issue
        //Created a seperate function & moved all the code from shown event to here.
        private void InitializeReportServer()
        {
            gloSettings.GeneralSettings oSetting = null;
            try
            {
                System.Threading.Tasks.Parallel.Invoke(new Action(UpdateSSRSDataSource));

                //RETRIVING SEETING. 
                oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                object oValue = null;
                oSetting.GetSetting("EnableSSRSReports", out oValue);
                if (oValue != null)
                {
                    _IsEnableSSRSReports = Convert.ToBoolean(oValue.ToString());
                    oValue = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oSetting != null)
                {
                    oSetting.Dispose();
                }
            }
        }


        private void frmDashBoardMain_Shown(object sender, EventArgs e)
        {

            try
            {

                //Set ContextMenu()
                ContextMenu cm = new ContextMenu();
                lblPD_Name.ContextMenu = cm;
                lblPD_MobilePhone.ContextMenu = cm;
                lblPD_HomePhone.ContextMenu = cm;
                lblPD_FaxPhone.ContextMenu = cm;
                lblPD_Email.ContextMenu = cm;
                lblPD_DateofBirth.ContextMenu = cm;
                lblPD_Address.ContextMenu = cm;
                lblPD_Pharmacy.ContextMenu = cm;
                lblPD_Physician.ContextMenu = cm;
                lblPD_Referral.ContextMenu = cm;
                lblProvider.ContextMenu = cm;

                mnuReports_ReportingTools.Visible = Program.gblnShowReportDesigner;
                mnuReports_Reports.Visible = Program.gblnShowReportDesigner;


                //Show Details on status bar 
                UpdateStatusBar(true);


                //Set tooltip for scan card functionality  buttons
                ToolTip1 = new System.Windows.Forms.ToolTip();
                //SLR: This also has to be freed during dispose or closing method: hence make this variable static in global and use accordingly.
                ToolTip1.SetToolTip(this.btnPC_ScanCard, " Scan Card  ");
                ToolTip1.SetToolTip(this.btnPC_MoveNext, " Move Next ");
                ToolTip1.SetToolTip(this.btnPC_PrintCards, " Print Cards ");
                ToolTip1.SetToolTip(this.btnPC_MovePrevious, " Move Previous  ");
                ToolTip1.SetToolTip(this.btnPC_MoveLast, " Move Last ");
                ToolTip1.SetToolTip(this.btnPC_DeleteCard, " Delete Card ");
                ToolTip1.SetToolTip(this.btnPC_MoveFirst, " Move First ");


                //Set Lock Screen Time
                if (Program.gLockScreenTime <= 4)
                {
                    timerLockScreen.Interval = 2000;
                }
                else
                {
                    timerLockScreen.Interval = Convert.ToInt32(((Program.gLockScreenTime / 2) - 1) * 60000);
                }
                timerLockScreen.Enabled = true;

                //Find the MdiClient in the MdiWindow
                foreach (Control c in this.Controls)
                {
                    if (c is MdiClient)
                    {
                        mdiClient = c as MdiClient;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

            }
        }

        private void UpdateSSRSDataSource()
        {
            try
            {
                gloSSRS.Create_Datasource("dsPM", "gloPM", gloPMGlobal.DatabaseConnectionString, Program.gSQLServerName, Program.gDatabase, !Program.gWindowsAuthentication, Program.gLoginUser, Program.gLoginPassword, true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void CheckMultipleRaceSettings()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
            object oValue = null;
            try
            {
                oSetting.GetSetting("ENABLE MU2 FEATURES", out oValue);
                if (oValue != null)
                {
                    if (oValue.ToString() == "1")
                    {
                        gloEMRAdminSettings.globlnEnableMultipleRaceFeatures = true;
                    }
                    else
                    {
                        gloEMRAdminSettings.globlnEnableMultipleRaceFeatures = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oSetting != null)
                {
                    oSetting.Dispose();
                    oSetting = null;
                }

                if (oValue != null)
                {

                    oValue = null;
                }
            }
        }


        # region "Function For SSRS Menu"

        ToolStripMenuItem Toolitems = default(ToolStripMenuItem);

        protected DataSet PDataset(string select_statement)
        {
            SqlConnection _con = new SqlConnection(gloPMGlobal.DatabaseConnectionString);
            SqlDataAdapter ad = new SqlDataAdapter(select_statement, _con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            _con.Close();
            _con.Dispose();
            ad.Dispose();
            return ds;
        }

        public void Load_SSRSMenu()
        {
            DataSet PrSet = PDataset("SELECT ReportsParentID,ReportsID,ReportName,ReportFileName FROM SSRSReports_MST where ReportType = '" + _sReportType + "'  Order By Reportsortorder");
            //trv_Reports.Nodes.Clear();
            try
            {

                foreach (DataRow dr in PrSet.Tables[0].Rows)
                {
                    if ((decimal)dr["ReportsParentID"] != 0 && (decimal)dr["ReportsParentID"] == 1)
                    {
                        Toolitems = new ToolStripMenuItem();
                        Toolitems.Text = dr["ReportName"].ToString();
                        Toolitems.Tag = dr["ReportFileName"].ToString();
                        Toolitems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                        if (Convert.ToString(dr["ReportFileName"]) == "")
                        {
                            Toolitems.Image = global::gloPM.Properties.Resources.SSRS_Report1;
                        }
                        else
                        {
                            Toolitems.Image = global::gloPM.Properties.Resources.New_Report;
                        }

                        Toolitems.ImageAlign = ContentAlignment.MiddleCenter;
                        Toolitems.TextImageRelation = TextImageRelation.ImageBeforeText;
                        Toolitems.Click += new EventHandler(MenuItemClickHandler);
                        if (Convert.ToString(dr["ReportName"]) == "TRIARQ Admin Reports" && oClsgloUserRights.TRIARQAdminReport == true)
                        {
                            mnuReports.DropDownItems.Add(Toolitems);
                            FillChildMenu(Toolitems, dr["ReportsID"].ToString());
                        }
                        else if (Convert.ToString(dr["ReportName"]) != "TRIARQ Admin Reports")
                        {
                            mnuReports.DropDownItems.Add(Toolitems);
                            FillChildMenu(Toolitems, dr["ReportsID"].ToString());
                        }
                    }
                    //else
                    //{
                    //    FillChildMenu(Toolitems, dr["ReportsID"].ToString());
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                //SLR: Finaly free PrSet
                if (PrSet != null)
                {
                    PrSet.Dispose();
                }
            }
        }

        public int FillChildMenu(ToolStripMenuItem parent, string IID)
        {
            ToolStripMenuItem child = default(ToolStripMenuItem);
            DataSet ds = null;
            try
            {

                ds = PDataset("SELECT ReportsParentID,ReportsID,ReportName,ReportFileName FROM SSRSReports_MST WHERE ReportsParentID =" + IID + " And ReportType = '" + _sReportType + "' Order By Reportsortorder  ");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        child = new ToolStripMenuItem();
                        child.Text = dr["ReportName"].ToString().Trim();
                        if (Convert.ToString(dr["ReportFileName"]) == "")
                        {
                            child.Image = global::gloPM.Properties.Resources.SSRS_Report1;
                        }
                        else
                        {
                            child.Image = global::gloPM.Properties.Resources.New_Report;
                        }
                        child.ImageAlign = ContentAlignment.MiddleCenter;
                        child.TextImageRelation = TextImageRelation.ImageBeforeText;
                        child.Tag = dr["ReportFileName"].ToString().Trim();
                        string temp = dr["ReportsID"].ToString();
                        child.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                        child.Click += new EventHandler(MenuItemClickHandler);
                        parent.DropDownItems.Add(child);
                        FillChildMenu(child, temp);
                    }
                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return 0;
            }
            finally
            {
                //SLR: Finally free ds
                if (ds != null)
                {
                    ds.Dispose();
                }
            }
        }

        #region "Menu Item Click Event"

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            if (Convert.ToString(clickedItem.Tag) != "")
            {
                if (clickedItem.OwnerItem.Text == "TRIARQ Admin Reports")
                {
                    using (frmEmergencyAccess ofrm = new frmEmergencyAccess(clickedItem.OwnerItem.Text))
                    {
                        if (ofrm.ShowDialog(this) == DialogResult.OK)
                        {
                            ShowSSRSReport(clickedItem.Text, clickedItem.Text, false, null);
                        }
                    }
                }
                else
                {
                    ShowSSRSReport(clickedItem.Text, clickedItem.Text, false, null);
                }
            }
        }

        //}
        #endregion



        # endregion

        public void LoadBatchEligibilitySettings()
        {
            DataSet ds = null;
            try
            {
                ds = PDataset("SELECT IsNull(BL_ClearingHouse_DTL.bEnableBatchEligibilty,'false') as bEnableBatchEligibilty " +
                                      " FROM BL_ClearingHouse_MST INNER JOIN " +
                                      " BL_ClearingHouse_DTL ON BL_ClearingHouse_MST.nClearingHouseID = BL_ClearingHouse_DTL.nClearingHouseID " +
                                      " WHERE     (BL_ClearingHouse_MST.bIsDefault = 1); " +
                                      " Select ISNULL(sSettingsValue,'false') as EnableBatchEligibilityFromView from Settings  " +
                                      " where sSettingsName = 'EnableBatchEligibilityFromView' ");
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["bEnableBatchEligibilty"]) == true)
                    { batchEligibilityActivityToolStripMenuItem.Visible = true; }
                    else
                    {
                        batchEligibilityActivityToolStripMenuItem.Visible = false;
                    }
                }
                else
                {
                    batchEligibilityActivityToolStripMenuItem.Visible = false;
                }

                if (ds != null && ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(ds.Tables[1].Rows[0]["EnableBatchEligibilityFromView"]) == true)
                    { mnuBatch_Eligibility.Visible = true; }
                    else
                    {
                        mnuBatch_Eligibility.Visible = false;
                    }
                }
                else
                {
                    mnuBatch_Eligibility.Visible = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
            }

        }

        private void SetPatientList(Int64 nLastPatient)
        {

            try
            {
                gloCntrlPatient.GridRowSelect_Click -= new gloPatient.PatientListControl.GridRowSelectHandler(gloCntrlPatient_GridRowSelect_Click);
                gloCntrlPatient.Grid_MouseDown -= new gloPatient.PatientListControl.GridMouseDownHandler(gloCntrlPatient_Grid_MouseDown);
                gloCntrlPatient.Grid_DoubleClick -= new gloPatient.PatientListControl.GridDoubleClick(gloCntrlPatient_Grid_DoubleClick);
                gloCntrlPatient.onRecpatientEventclick -= new gloPatient.PatientListControl.onRecpatientClick(SelectRecentVisitedPatient);
            }
            catch
            {
            }

            gloCntrlPatient.GridRowSelect_Click += new gloPatient.PatientListControl.GridRowSelectHandler(gloCntrlPatient_GridRowSelect_Click);
            gloCntrlPatient.Grid_MouseDown += new gloPatient.PatientListControl.GridMouseDownHandler(gloCntrlPatient_Grid_MouseDown);
            gloCntrlPatient.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(gloCntrlPatient_Grid_DoubleClick);
            gloCntrlPatient.onRecpatientEventclick += new gloPatient.PatientListControl.onRecpatientClick(SelectRecentVisitedPatient);
            //Fill Patients - Start
            gloCntrlPatient.DatabaseConnection = gloPMGlobal.DatabaseConnectionString;
            gloCntrlPatient.ShowOKCancel(false);

            //Check if last saved patientID is not empty. 
            //If not set the currentpatientID to the last saved one.
            if (nLastPatient != 0)
            {
                gloCntrlPatient.SelectedPatientID = Convert.ToInt64(nLastPatient);
                gloCntrlPatient.PatientID = Convert.ToInt64(nLastPatient);
            }

            // fill all Patients
            Int64 nPatientCount;
            nPatientCount = gloCntrlPatient.FillPatients();

            _CurrentPatientId = gloCntrlPatient.SelectedPatientID;
        }

        private void AssignUserRights()
        {
            try
            {
                if (gloPMGlobal.UserName.Trim() != "")
                {
                    //SLR: Free exisitng memory before allocating once more
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(gloPMGlobal.DatabaseConnectionString);
                    oClsgloUserRights.CheckForUserRights(gloPMGlobal.UserName);

                    //Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal"
                    //added to show activation report menu.
                    gloSettings.GeneralSettings objSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                    object _IsPatientPortalEnable = null;
                    objSetting.GetSetting("PatientPortalEnabled", out _IsPatientPortalEnable);

                    //Bug #68665: V8020: "String not recognized as valid boolean "shown in logging in to PM application
                    //Remove Convert.ToBoolean as setting may be null or '' gives exception.
                    if (_IsPatientPortalEnable.ToString().ToLower() == "true")
                    {
                        mnuPatientActivationtReport.Visible = true;
                    }
                    else
                    {
                        mnuPatientActivationtReport.Visible = false;
                    }

                    objSetting.Dispose();
                    objSetting = null;

                    _IsPatientPortalEnable = null;

                    //MaheshB
                    if (tsb_PaymentPatient.Visible != false)
                    {
                        tsb_PaymentPatient.Visible = true;
                    }

                    gloPMGlobal.AssociatedProvider = oClsgloUserRights.AssociatedProviderSignature;

                    #region " ToolStrip Button Rights "

                    if (tsb_Calendar.Visible != false)
                    {
                        tsb_Calendar.Visible = oClsgloUserRights.Calender;
                    }
                    if (tsb_Advance.Visible != false)
                    {
                        tsb_Advance.Visible = oClsgloUserRights.Advance;
                    }
                    if (tsb_Appointment.Visible != false)
                    {
                        tsb_Appointment.Visible = oClsgloUserRights.Appointment;
                    }
                    if (tsb_Billing.Visible != false)
                    {
                        tsb_Billing.Visible = oClsgloUserRights.Charges;
                    }
                    if (tsb_BillingBatch.Visible != false)
                    {
                        tsb_BillingBatch.Visible = oClsgloUserRights.Batch;
                    }
                    if (tsb_Exit.Visible != false)
                    {
                        tsb_Exit.Visible = oClsgloUserRights.Exit;
                    }
                    if (tsb_LockScreen.Visible != false)
                    {
                        tsb_LockScreen.Visible = oClsgloUserRights.Lock;
                    }
                    //if (tsb_PatBalance.Visible != false)
                    //{
                    //    tsb_PatBalance.Visible = oClsgloUserRights.Balance;
                    //}
                    if (tsb_PatientModification.Visible != false)
                    {
                        tsb_PatientModification.Visible = oClsgloUserRights.ModifyPatient;
                    }
                    if (tsb_PatientRegistration.Visible != false)
                    {
                        tsb_PatientRegistration.Visible = oClsgloUserRights.NewPatient;
                    }
                    //if (tsb_PatLedger.Visible != false)
                    //{
                    //    tsb_PatLedger.Visible = oClsgloUserRights.Ledger;
                    //}
                    if (tsb_PatientStatment.Visible != false)
                    {
                        tsb_PatientStatment.Visible = oClsgloUserRights.PatStatement;
                    }
                    //tsb_PatientScan.Visible = oClsgloUserRights.CardScanning;
                    //Commented By MaheshB for Payment Button visible.
                    //if (tsb_Payment.Visible != false)
                    //{
                    //    tsb_Payment.Visible = oClsgloUserRights.Payment;
                    //}
                    if (tsb_Remittance.Visible != false)
                    {
                        tsb_Remittance.Visible = oClsgloUserRights.Remittance;
                    }
                    //************************************************************
                    //Commented And Added By Debasish Das on 17th March 2010
                    //************************************************************
                    //if (btnPC_ScanCard.Visible == false)
                    //{
                    btnPC_ScanCard.Visible = oClsgloUserRights.CardScanning;
                    //}
                    //************************ Ends Here **************************

                    //tsb_Calculator.Visible = true;

                    // Problem# -00000117 : Assing the user right for calender and Lock screen and check still calender is disabled.
                    // Commented the "if" condition for " Calender,Appointment,Task" .
                    //if (btn_Calendar.Enabled != false)
                    //{
                    btn_Calendar.Enabled = oClsgloUserRights.Calender;
                    //}
                    // if (btn_Appointment.Enabled != false)
                    //{
                    btn_Appointment.Enabled = oClsgloUserRights.Appointment;
                    //}
                    // if (btn_Tasks.Enabled != false)
                    // {
                    btn_Tasks.Enabled = oClsgloUserRights.Tasks;
                    //}
                    // End.
                    //if (tsb_ScanDocs.Visible != false)
                    //{
                    tsb_ScanDocs.Visible = oClsgloUserRights.ScanDocuments;
                    //}
                    //MaheshB
                    //tsb_PaymentInsurance.Visible = true;
                    //tsb_ShowDashboard.Visible = true;
                    ////MaheshB
                    //if (tsb_PaymentPatient.Visible != false)
                    //{
                    //tsb_PaymentPatient.Visible = true;
                    //}
                    #endregion

                    #region " File Menu "
                    if (mnuFile_Exit.Visible != false)
                    {
                        mnuFile_Exit.Visible = oClsgloUserRights.Exit;
                    }
                    if (mnuFile_Lock.Visible != false)
                    {
                        mnuFile_Lock.Visible = oClsgloUserRights.Lock;
                    }

                    #endregion

                    #region " Edit Menu "
                    if (mnuEdit_TemplateAssociation.Visible != false)
                    {
                        mnuEdit_TemplateAssociation.Visible = oClsgloUserRights.TemplateAssociation;
                    }
                    if (mnuView_TemplateGallary.Visible != false)
                    {
                        mnuView_TemplateGallary.Visible = oClsgloUserRights.TemplateGallery;
                    }


                    #endregion

                    #region " View Menu  "

                    if (mnuView_Schedule.Visible != false)
                    {
                        mnuView_Schedule.Visible = oClsgloUserRights.Schedule;
                    }
                    if (mnuView_Tasks.Visible != false)
                    {
                        mnuView_Tasks.Visible = oClsgloUserRights.Tasks;
                    }
                    if (mnuView_PatientTemplates.Visible != false)
                    {
                        mnuView_PatientTemplates.Visible = oClsgloUserRights.PatientForms;
                    }


                    #endregion

                    #region " Go Menu "

                    if (mnuGo_Appointment.Visible != false)
                    {
                        mnuGo_Appointment.Visible = oClsgloUserRights.Appointment;
                    }

                    if (mnuGo_CardScanning.Visible != false)
                    {
                        mnuGo_CardScanning.Visible = oClsgloUserRights.CardScanning;
                    }
                    if (mnuGo_ModifyPatient.Visible != false)
                    {
                        mnuGo_ModifyPatient.Visible = oClsgloUserRights.ModifyPatient;
                    }
                    if (mnuGo_NewPatient.Visible != false)
                    {
                        mnuGo_NewPatient.Visible = oClsgloUserRights.NewPatient;
                    }
                    if (mnuGo_Payment.Visible != false)
                    {
                        mnuGo_Payment.Visible = oClsgloUserRights.Payment;
                    }
                    if (mnuGo_Schedule.Visible != false)
                    {
                        mnuGo_Schedule.Visible = oClsgloUserRights.NewSchedule;
                    }
                    if (mnuGo_Schedule.Visible != false)
                    {
                        mnuGo_Schedule.Visible = oClsgloUserRights.NewSchedule;
                    }
                    if (mnuGo_Billing.Visible != false)
                    {
                        mnuGo_Billing.Visible = oClsgloUserRights.Charges;
                    }
                    if (mnuGo_BatchProcessing.Visible != false)
                    {
                        mnuGo_BatchProcessing.Visible = oClsgloUserRights.Batch;
                    }
                    //if (mnuGo_ScanDocument.Visible != false)
                    //{
                    mnuGo_ScanDocument.Visible = oClsgloUserRights.ScanDocuments;
                    //}
                    //mnuGo_ClaimProcessing.Visible=oClsgloUserRights.

                    #endregion

                    #region " Reports Menu  "

                    #region " MIS Reports "

                    //Commented By Shweta as Menu has deleted 20100217
                    //if (mnu_MISReports_AgingAnalysis.Visible != false)
                    //{
                    //    mnu_MISReports_AgingAnalysis.Visible = oClsgloUserRights.ASumary;
                    //}
                    //if (mnu_MISReports_AgingSummaryByInsuranceCarrier.Visible != false)
                    //{
                    //    mnu_MISReports_AgingSummaryByInsuranceCarrier.Visible = oClsgloUserRights.ASumaryByInsCarrier;
                    //}
                    //if (mnu_MISReports_AgingSummaryByPatient.Visible != false)
                    //{
                    //    mnu_MISReports_AgingSummaryByPatient.Visible = oClsgloUserRights.ASumaryByPat;
                    //}
                    //End Commenting

                    if (mnu_MISReports_PatientPaymentHistory.Visible != false)
                    {
                        mnu_MISReports_PatientPaymentHistory.Visible = oClsgloUserRights.PatPayHistory;
                    }
                    //if (mnu_MISReports_Patientstatement.Visible != false)
                    //{
                    //    mnu_MISReports_Patientstatement.Visible = oClsgloUserRights.PatStatement;
                    //}
                    if (mnu_MISReports_PatientTransactionHistory.Visible != false)
                    {
                        mnu_MISReports_PatientTransactionHistory.Visible = oClsgloUserRights.PatTransHistory;
                    }
                    if (mnu_MISReports_ProductionAnalysisandTrendsByMonth.Visible != false)
                    {
                        mnu_MISReports_ProductionAnalysisandTrendsByMonth.Visible = oClsgloUserRights.ProdAnalysisByTrendsByMonth;
                    }
                    if (mnu_MISReports_ProductionByDate.Visible != false)
                    {
                        mnu_MISReports_ProductionByDate.Visible = oClsgloUserRights.ProdAnalysisByFacility;
                    }
                    if (mnu_MISReports_ProductionByDate.Visible != false)
                    {
                        mnu_MISReports_ProductionByDate.Visible = oClsgloUserRights.ProdAnalysisByPG;
                    }
                    if (mnu_MISReports_ProductionByDate.Visible != false)
                    {
                        mnu_MISReports_ProductionByDate.Visible = oClsgloUserRights.ProdByDate;
                    }
                    if (mnu_MISReports_ProductionByDoctor.Visible != false)
                    {
                        mnu_MISReports_ProductionByDoctor.Visible = oClsgloUserRights.ProdByDoctor;
                    }
                    if (mnu_MISReports_ProductionByFacility.Visible != false)
                    {
                        mnu_MISReports_ProductionByFacility.Visible = oClsgloUserRights.ProdByFacility;
                    }
                    if (mnu_MISReports_ProductionByFacilityByPatient.Visible != false)
                    {
                        mnu_MISReports_ProductionByFacilityByPatient.Visible = oClsgloUserRights.ProdByFacilityByPatSummary;
                    }
                    if (mnu_MISReports_ProductionByFacilityByPatientDetail.Visible != false)
                    {
                        mnu_MISReports_ProductionByFacilityByPatientDetail.Visible = oClsgloUserRights.ProdByFacilityByPatDetail;
                    }
                    if (mnu_MISReports_ProductionByInsuranceCarrier.Visible != false)
                    {
                        mnu_MISReports_ProductionByInsuranceCarrier.Visible = oClsgloUserRights.ProdByInsCarrier;
                    }
                    if (mnu_MISReports_ProductionByMonth.Visible != false)
                    {
                        mnu_MISReports_ProductionByMonth.Visible = oClsgloUserRights.ProdByMonth;
                    }
                    if (mnu_MISReports_ProductionByMonth.Visible != false)
                    {
                        mnu_MISReports_ProductionByMonth.Visible = oClsgloUserRights.ProdByMonth;
                    }
                    if (mnu_MISReports_ProductionByPhysicianGroup.Visible != false)
                    {
                        mnu_MISReports_ProductionByPhysicianGroup.Visible = oClsgloUserRights.ProdByPhysicianGroup;
                    }
                    if (mnu_MISReports_ProductionByPhysicianGroup.Visible != false)
                    {
                        mnu_MISReports_ProductionByPhysicianGroup.Visible = oClsgloUserRights.ProdByPhysicianGroup;
                    }
                    if (mnu_MISReports_ProductionByPhysicianGroup.Visible != false)
                    {
                        mnu_MISReports_ProductionByPhysicianGroup.Visible = oClsgloUserRights.ProdByPhysicianGroup;
                    }
                    if (mnu_MISReports_ProductionByProcedureCode.Visible != false)
                    {
                        mnu_MISReports_ProductionByProcedureCode.Visible = oClsgloUserRights.ProdByProcCode;
                    }
                    if (mnu_MISReports_ProductionByProcedureGroup.Visible != false)
                    {
                        mnu_MISReports_ProductionByProcedureGroup.Visible = oClsgloUserRights.ProdByProcGroup;
                    }
                    if (mnu_MISReports_ProductionTrendsByProcedureGrop.Visible != false)
                    {
                        mnu_MISReports_ProductionTrendsByProcedureGrop.Visible = oClsgloUserRights.ProdTrendsByProcGroup;
                    }
                    if (mnu_MISReports_ReimbursementByCPTByInsurance.Visible != false)
                    {
                        mnu_MISReports_ReimbursementByCPTByInsurance.Visible = oClsgloUserRights.ReimbByCPTByInsCarrier;
                    }
                    if (mnu_MISReports_ReimbursementByDoctorByInsurance.Visible != false)
                    {
                        mnu_MISReports_ReimbursementByDoctorByInsurance.Visible = oClsgloUserRights.ReimbByDocByInsCarrier;
                    }
                    if (mnu_MISReports_ReimbursementByInsuranceByCPT.Visible != false)
                    {
                        mnu_MISReports_ReimbursementByInsuranceByCPT.Visible = oClsgloUserRights.ReimbByInsCarrierByCPT;
                    }
                    if (mnu_MISReports_ReimbursementByInsuranceCarrier.Visible != false)
                    {
                        mnu_MISReports_ReimbursementByInsuranceCarrier.Visible = oClsgloUserRights.ReimbByInsCarrier;
                    }



                    if (mnu_MISReports_ReimbursementByInsuranceForCPT.Visible != false)
                    {
                        mnu_MISReports_ReimbursementByInsuranceForCPT.Visible = oClsgloUserRights.ReimbByInsCarrierForCPT;
                    }

                    if (mnu_MISReports_ReimbursementByMonth.Visible != false)
                    {
                        mnu_MISReports_ReimbursementByMonth.Visible = oClsgloUserRights.ReimbByMonth;
                    }

                    if (mnu_MISReports_ReimbursementByMonthDetail.Visible != false)
                    {
                        mnu_MISReports_ReimbursementByMonthDetail.Visible = oClsgloUserRights.ReimbByMonthByAcc;
                    }

                    if (mnu_MISReports_ReimbursementDetailsByAccount.Visible != false)
                    {
                        mnu_MISReports_ReimbursementDetailsByAccount.Visible = oClsgloUserRights.ReimbDetailsByAcc;
                    }

                    //Added By Shweta 20100217
                    //if (mnu_MISReports_Aging.Visible != false)
                    //{
                    //    mnu_MISReports_Aging.Visible = oClsgloUserRights.Aging;
                    ////}
                    //    mnu_MISReports_FinancialSummary.Visible = oClsgloUserRights.Aging;

                    //    //Added By Shweta 20100503
                    //    //if (mnuGo_PatientFinancialInfo.Visible != false)
                    //    //{
                    //    mnuGo_PatientAccount.Visible = oClsgloUserRights.PatientAccount;
                    //    //}
                    //    mnu_MISReports_FinancialSummary.Visible = oClsgloUserRights.FinancialSummary;



                    #endregion




                    if (mnu_rpt_Appointments.Visible != false)
                    {
                        mnu_rpt_Appointments.Visible = oClsgloUserRights.Appointments;
                    }


                    if (mnu_rpt_NoShowAppointments.Visible != false)
                    {
                        mnu_rpt_NoShowAppointments.Visible = oClsgloUserRights.CancelAppointments;
                    }



                    if (mnuReports_AuditTrail.Visible != false)
                    {
                        mnuReports_AuditTrail.Visible = oClsgloUserRights.AuditTrial;
                    }




                    if (mnuReports_gatewayEDI.Visible != false)
                    {
                        mnuReports_gatewayEDI.Visible = oClsgloUserRights.GatewayEDI;
                    }
                    //mnuReports_DailyCollectionReport.Visible=oClsgloUserRights.


                    if (mnuReports_PatientVsEstablishedReport.Visible != false)
                    {
                        mnuReports_PatientVsEstablishedReport.Visible = oClsgloUserRights.NewPatvsEstPat;
                    }
                    //mnuReports_Graphs.Visible = oClsgloUserRight.Graphs;
                    //mnuReports_OverdueInsurancePayment.Visible = oClsgloUserRights.OverdueInsPay;





                    if (mnuReports_PrintList.Visible != false)
                    {
                        mnuReports_PrintList.Visible = oClsgloUserRights.PrintList;
                    }




                    if (mnuReports_ProviderReferral_Patients.Visible != false)
                    {
                        mnuReports_ProviderReferral_Patients.Visible = oClsgloUserRights.ProvRefPat;
                    }



                    if (mnuReportsRefund.Visible != false)
                    {
                        mnuReportsRefund.Visible = oClsgloUserRights.Refund;
                    }




                    if (mnuReportsTransactionHistory.Visible != false)
                    {
                        mnuReportsTransactionHistory.Visible = oClsgloUserRights.TransHistory;
                    }


                    if (mnuRpt_BatchPrint.Visible != false)
                    {
                        mnuRpt_BatchPrint.Visible = oClsgloUserRights.BatchPrintTemp;
                    }



                    if (mnu_MissingChargesReport.Visible != false)
                    {
                        mnu_MissingChargesReport.Visible = oClsgloUserRights.MissingCharges;
                    }




                    if (mnu_PatientRecall.Visible != false)
                    {
                        mnu_PatientRecall.Visible = oClsgloUserRights.PatRecall;
                    }


                    if (mnu_patientReport.Visible != false)
                    {
                        mnu_patientReport.Visible = oClsgloUserRights.PatReport;
                    }



                    if (mnuTransactionHistoryAnalysis.Visible != false)
                    {
                        mnuTransactionHistoryAnalysis.Visible = oClsgloUserRights.TransHistoryAnalysis;
                    }




                    if (mnuTransactionNotes.Visible != false)
                    {
                        mnuTransactionNotes.Visible = oClsgloUserRights.TransNotes;
                    }




                    if (mnu_ChargesPayments.Visible != false)
                    {
                        mnu_ChargesPayments.Visible = oClsgloUserRights.CashFlowReport;
                    }




                    if (mnu_MonthEndReport.Visible != false)
                    {
                        mnu_MonthEndReport.Visible = oClsgloUserRights.ChargesSummaryReport;
                    }


                    if (mnuReports_ZeroBalancePatient.Visible != false)
                    {
                        mnuReports_ZeroBalancePatient.Visible = oClsgloUserRights.PatBalance;
                    }



                    if (mnu_MISReports_ReimbursementDetailsByAccount.Visible != false)
                    {
                        mnu_MISReports_ReimbursementDetailsByAccount.Visible = oClsgloUserRights.ReimbDetailsByAcc;

                    }

                    if (mnuReports_ChargeEditReport.Visible == false)
                    {
                        mnuReports_ChargeEditReport.Visible = oClsgloUserRights.ChargeEditReport;
                    }


                    //Added By Shweta 20100217
                    //if (mnu_InsuranceCompanySetup_Company_Category.Visible != false)
                    //{
                    //mnu_InsuranceCompanySetup_Company_Category.Visible = oClsgloUserRights.InsuranceCompanySetup;
                    ////}
                    ////To hide the main menu if submenus visibility set to false
                    //if (oClsgloUserRights.InsuranceCompanySetup == false)
                    //{
                    //    mnu_InsuranceCompanySetup.Visible = false;                    
                    //}

                    #endregion



                    #region " Tools Menu "


                    if (mnuTools_CardImage.Visible != false)
                    {
                        mnuTools_CardImage.Visible = oClsgloUserRights.CardImage;
                    }



                    if (mnuTools_Export.Visible != false)
                    {
                        mnuTools_Export.Visible = oClsgloUserRights.ExportTemplates;
                    }




                    if (mnuTools_Import.Visible != false)
                    {
                        mnuTools_Import.Visible = oClsgloUserRights.ImportTemplates;
                    }


                    if (mnuTools_MergePatient.Visible != false)
                    {
                        mnuTools_MergePatient.Visible = oClsgloUserRights.MergePatient;
                    }

                    //mnuTools_Synchronize.Visible = oClsgloUserRights.Syncronize;

                    if (mnuTools_UpdateTemplates.Visible != false)
                    {
                        mnuTools_UpdateTemplates.Visible = oClsgloUserRights.UpdateTemplates;
                    }


                    //added by mahesh s on 2011-06-27(yyyy-mm-dd) for check merge account feature true/false.
                    gloPatient.gloAccount objgloAccount = new gloPatient.gloAccount(gloPMGlobal.DatabaseConnectionString);
                    if (objgloAccount != null)
                    {
                        //Patient Account Feature and Merge Account Feature both are true then only Merge Patient Account will visible.
                        if (objgloAccount.GetPatientAccountFeatureSetting() && objgloAccount.GetMergeAccountFeatureSetting() && oClsgloUserRights.MergePatientAccount)
                        {
                            mnuTools_MergePatientAccount.Visible = true;
                        }
                        else
                            mnuTools_MergePatientAccount.Visible = false;

                        objgloAccount.Dispose(); //SLR: inside if
                    }

                    #endregion




                    #region " Settings Menu "


                    if (mnuSetting_CardScanner.Visible != false)
                    {
                        mnuSetting_CardScanner.Visible = oClsgloUserRights.CardScanner;
                    }



                    if (mnuSetting_Customization.Visible != false)
                    {
                        mnuSetting_Customization.Visible = oClsgloUserRights.CustomizeToolBar;
                    }




                    if (mnuSetting_DefaultDashboard.Visible != false)
                    {
                        mnuSetting_DefaultDashboard.Visible = oClsgloUserRights.RestoreDashBoard;
                    }




                    if (mnuSetting_SystemSetting.Visible != false)
                    {
                        mnuSetting_SystemSetting.Visible = oClsgloUserRights.SystemSetting;
                    }




                    if (mnuSetting_Theme2003.Visible != false)
                    {
                        mnuSetting_Theme2003.Visible = oClsgloUserRights.Office2003Theme;
                    }


                    if (mnuSetting_Theme2003Dark.Visible != false)
                    {
                        mnuSetting_Theme2003Dark.Visible = oClsgloUserRights.Office2003DarkTheme;
                    }



                    if (mnuSetting_Theme2007.Visible != false)
                    {
                        mnuSetting_Theme2007.Visible = oClsgloUserRights.Office2007Theme;
                    }




                    #endregion




                    #region " Help Menu "

                    if (mnuHelp_AboutgloPMS.Visible != false)
                    {
                        mnuHelp_AboutgloPMS.Visible = oClsgloUserRights.About;
                    }




                    if (mnuHelp_Contents.Visible != false)
                    {
                        mnuHelp_Contents.Visible = oClsgloUserRights.Contents;
                    }


                    if (mnuHelp_HowDoI.Visible != false)
                    {
                        mnuHelp_HowDoI.Visible = oClsgloUserRights.HowDoI;
                    }



                    if (mnuHelp_Search.Visible != false)
                    {
                        mnuHelp_Search.Visible = oClsgloUserRights.Search;
                    }





                    //mnuHelp_TechnicalSupport.Visible = oClsgloUserRights.TechnicalSupport;

                    #endregion

                    #region Patient Details
                    //tsb_PD_Billing.Visible = oClsgloUserRights.B;
                    //if (tsb_PD_Cases.Visible != false)
                    //{
                    //    tsb_PD_Cases.Visible = oClsgloUserRights.Balance;
                    //}
                    //if (tsb_PDViewDocument.Visible != false)
                    //{
                    tsb_PDViewDocument.Visible = oClsgloUserRights.ViewDocuments;
                    //}
                    #endregion



                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        private void SetLicenseModule()
        {
            try
            {

                // License Check
                List<object> _ToolStrip = new List<object>();
                _ToolStrip.Add(this.tsb_Appointment);
                _ToolStrip.Add(this.tsb_PatientRegistration);
                _ToolStrip.Add(this.tsb_PatientModification);
                _ToolStrip.Add(this.tsb_Billing);
                _ToolStrip.Add(this.mnuGo_NewPatient);
                _ToolStrip.Add(this.mnuGo_ModifyPatient);
                _ToolStrip.Add(this.mnuGo_Appointment);
                _ToolStrip.Add(this.mnuGo_Billing);
                base.FormControls = null;
                //if (gloGlobal.gloPMGlobal.LoginProviderID == 0) { base.strProviderID = _PatientProviderId.ToString(); }                
                base.FormControls = _ToolStrip.ToArray();
                base.SetChildFormControls();
                _ToolStrip = null;
                // end License Check
                //tsb_Billing.Enabled = bEnableModule;
                //tsb_Appointment.Enabled = bEnableModule;
                //tsb_PatientRegistration.Enabled = bEnableModule;
                //tsb_PaymentPatient.Enabled = bEnableModule;
                //tsb_BillingBatch.Enabled = bEnableModule;
                ////// gblnIsValidLicense
                ////tlbbtn_NewPatient.Enabled = bEnableModule;
                ////tlbbtn_Prescription.Enabled = bEnableModule;
                ////tlbbtn_Sechedule.Enabled = bEnableModule;
                ////mnuPatientRegistration.Enabled = bEnableModule;
                ////mnuPatientPrescription.Enabled = bEnableModule;
                ////tlbbtn_Calender.Enabled = bEnableModule;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateStatusBar(bool bIsFormLoad = false)
        {
            try
            {
                if (appSettings["UserName"] != null)
                {
                    sslbl_Login.Text = Convert.ToString(appSettings["UserName"]) + "    " + Convert.ToString(Strings.Format(System.DateTime.Now, "Medium Time"));
                }
                if (Program.gSQLServerName != null)
                {

                    sslbl_Database.Text = Convert.ToString(Program.gSQLServerName) + "    " + Convert.ToString(Program.gDatabase);
                }
                //sslbl_Version.Text = Application.ProductVersion;

                //...TODO - Load from database
                // 20100105 Mahesh Nawal Get the version no from the database.

                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                DataTable dtversion; //SLR: new not needed
                dtversion = oSetting.GetSetting("gloPMApplicationVersion", 0);
                if (dtversion != null && dtversion.Rows.Count > 0)
                {
                    sslbl_Version.Text = dtversion.Rows[0]["sSettingsValue"].ToString();
                }
                //20100630 Version set
                sslbl_Version.Text = Application.ProductVersion;
                //sslbl_Version.Text = "1.2.0.0";

                sslbl_LastModifiedDate.Text = "Last Modified Date " + String.Format("{0:MM/dd/yyyy h:m:s tt}", File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName));

                //SLR: Finaly free oSetting, dtversion
                if (oSetting != null)
                {
                    oSetting.Dispose();
                }
                if (dtversion != null)
                {
                    dtversion.Dispose();
                }
                if (IsSingleSignOn && bIsFormLoad)
                {
                    sslbl_SingleSignOn.Visible = true;
                    tmrSingleSignOn.Enabled = true;
                }
                else
                {
                    sslbl_SingleSignOn.Visible = false;
                    tmrSingleSignOn.Enabled = false;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }


        private void frmDashBoardMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (PendingMessageQueue())
                {
                    UnRegisterMyHotKey();

                    if (_IsExitCall == false)
                    {
                        if (bIsShowSplashScreen)
                        {
                            DashboardClosingEvent();
                            this.Hide();
                            foreach (Form item in Application.OpenForms)
                            {
                                if (item.Name == "frmSplash")
                                {
                                    item.Show();
                                    ((gloPM.frmSplash)(item)).txtUserName.Clear();
                                    ((gloPM.frmSplash)(item)).txtPassword.Clear();
                                    ((gloPM.frmSplash)(item)).lblPleaseWait.Visible = false;
                                    ((gloPM.frmSplash)(item)).txtUserName.Focus();
                                }
                            }
                        }
                        else
                        {


                            if (MessageBox.Show("Are you sure you want to close the application?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                e.Cancel = true;
                                _IsExitCall = false;

                            }
                            else
                            {
                                #region "Commented code"
                                //FileStream oFileStream = new FileStream(Application.StartupPath + "\\LayOutSetting.XML", FileMode.OpenOrCreate);
                                //uiPanelManager1.SaveLayoutFile(oFileStream, Janus.Windows.UI.Dock.PersistMode.All);
                                //oFileStream.Close();
                                //oFileStream = null;
                                //gloSecurity.gloUser ogloUser = new gloSecurity.gloUser(gloPMGlobal.DatabaseConnectionString);
                                //ogloUser.AddUserDiaplaySettings(Application.StartupPath + "\\LayOutSetting.XML");

                                //20100104  Mahesh Nawal Add the logic for closing the calculate 
                                //Bug ID -  160
                                //if (_Iscalprocessstarted == false)
                                //{
                                //    foreach (System.Diagnostics.Process clsProcess in System.Diagnostics.Process.GetProcesses())
                                //    {
                                //        if (clsProcess.ProcessName.StartsWith("calc"))
                                //        {
                                //            if (Convert.ToString(clsProcess.Id) == Convert.ToString(calProcess.Id))
                                //            {
                                //                clsProcess.Kill();
                                //            }
                                //        }
                                //    }
                                //}

                                //gloCntrlPatient.SaveColumnWidth();

                                //SaveDisplaySettings(false);

                                //// Save General Patient Search Settings
                                //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                                //string _sqlQuery = " Delete  FROM Settings WHERE sSettingsName = 'DashboardPatientGeneralSearchEnabled' AND nUserID = " + gloPMGlobal.UserID + " AND nClinicID = " + gloPMGlobal.ClinicID + " AND nUserClinicFlag = 2";
                                //oDB.Connect(false);
                                //oDB.Execute_Query(_sqlQuery);
                                //_sqlQuery = "Select Max(nSettingsID)+1 As nSettingsID From Settings ";
                                //object _sqlresult = null;
                                //_sqlresult = oDB.ExecuteScalar_Query(_sqlQuery);
                                //if (_sqlresult != null && _sqlresult.ToString() != "")
                                //{
                                //    string SettingValue = "0";
                                //    if (gloCntrlPatient.GetGeneralSearch == true)
                                //    { SettingValue = "1"; }
                                //    else { SettingValue = "0"; }
                                //    _sqlQuery = " INSERT INTO Settings (nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag ) "
                                //         + " Values(" + Convert.ToInt64(_sqlresult.ToString()) + ",'DashboardPatientGeneralSearchEnabled', '" + SettingValue + "'," + gloPMGlobal.ClinicID + "," + gloPMGlobal.UserID + ",2 )";

                                //    oDB.Execute_Query(_sqlQuery);
                                //}
                                //oDB.Disconnect();
                                /////SLR: odb.dispose, sqlresult.dipsoe,
                                //oDB.Dispose();
                                //_sqlresult = null;
                                //foreach (Form f in this.MdiChildren)
                                //{
                                //    f.Close();
                                //}


                                //if (myExclusionStrings != null) { myExclusionStrings.Clear(); myExclusionStrings = null; }

                                //if (myDialogCloser != null)
                                //{
                                //    // if (myDialogCloser.strExclusionStrings != null) { myDialogCloser.strExclusionStrings.Clear(); }
                                //    myDialogCloser.Dispose(); myDialogCloser = null;
                                //}

                                //gloWord.gloWord.ExitWord();
                                //gloOffice.Supporting.EmptygloPMTempFolder();
                                ////SLR: on Form close or dispose method, we need to free myPictureBoxControl, _iPhoto, m_hotKeys, nBlinkingCells, oPatientCards, ogloReminder, oClsgloUserRights
                                ////if (MyPictureBoxControl != null) { MyPictureBoxControl = null; }
                                ////if (_iPhoto != null) { _iPhoto.Dispose(); _iPhoto = null; }
                                //if (m_hotKeys != null) { m_hotKeys = null; }
                                //if (nBlinkingCells != null) { nBlinkingCells = null; }
                                //if (oPatientCards != null) { oPatientCards.Dispose(); oPatientCards = null; }
                                //if (ogloReminder != null) { ogloReminder.Dispose(); ogloReminder = null; }
                                //if (oClsgloUserRights != null) { oClsgloUserRights.Dispose(); oClsgloUserRights = null; }

                                ////SLR: Free all global variables declared in the class and then
                                //if (arrDashBoardToolStrip != null)
                                //{
                                //    arrDashBoardToolStrip = null;
                                //}
                                //if (arrPatientdetailsToolStrip != null)
                                //{
                                //    arrPatientdetailsToolStrip = null;
                                //}
                                //Font_Bold.Dispose();
                                //Font_Regular.Dispose();
                                //if (ToolTip1 != null)
                                //{
                                //    ToolTip1.Dispose();
                                //}
                                //_IsExitCall = true;
                                //Application.Exit(); 
                                #endregion
                                ClsAutoCoapyDistributionList.RemoveInstanceForUser();
                                DashboardClosingEvent();
                                if (myDialogCloser != null)
                                {
                                    // if (myDialogCloser.strExclusionStrings != null) { myDialogCloser.strExclusionStrings.Clear(); }
                                    myDialogCloser.Dispose(); myDialogCloser = null;
                                }
                                Font_Bold.Dispose();
                                Font_Regular.Dispose();
                                _IsExitCall = true;
                                Application.Exit();
                            }
                        }
                    }
                }
                else 
                {
                    e.Cancel = true; _IsExitCall = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //IdleTrackerTerm();
        }

        private void DashboardClosingEvent()
        {
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            if (_Iscalprocessstarted == false)
            {
                foreach (System.Diagnostics.Process clsProcess in System.Diagnostics.Process.GetProcesses())
                {
                    if (clsProcess.ProcessName.StartsWith("calc"))
                    {
                        if (Convert.ToString(clsProcess.Id) == Convert.ToString(calProcess.Id))
                        {
                            clsProcess.Kill();
                        }
                    }
                }
            }
            gloCntrlPatient.SaveColumnWidth();
            SaveDisplaySettings(false);

            // Save General Patient Search Settings
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = " Delete  FROM Settings WHERE sSettingsName = 'DashboardPatientGeneralSearchEnabled' AND nUserID = " + gloPMGlobal.UserID + " AND nClinicID = " + gloPMGlobal.ClinicID + " AND nUserClinicFlag = 2";
            oDB.Connect(false);
            oDB.Execute_Query(_sqlQuery);
            _sqlQuery = "Select Max(nSettingsID)+1 As nSettingsID From Settings ";
            object _sqlresult = null;
            _sqlresult = oDB.ExecuteScalar_Query(_sqlQuery);
            if (_sqlresult != null && _sqlresult.ToString() != "")
            {
                string SettingValue = "0";
                if (gloCntrlPatient.GetGeneralSearch == true)
                { SettingValue = "1"; }
                else { SettingValue = "0"; }
                _sqlQuery = " INSERT INTO Settings (nSettingsID, sSettingsName, sSettingsValue, nClinicID, nUserID, nUserClinicFlag ) "
                     + " Values(" + Convert.ToInt64(_sqlresult.ToString()) + ",'DashboardPatientGeneralSearchEnabled', '" + SettingValue + "'," + gloPMGlobal.ClinicID + "," + gloPMGlobal.UserID + ",2 )";

                oDB.Execute_Query(_sqlQuery);
            }
            oDB.Disconnect();
            ///SLR: odb.dispose, sqlresult.dipsoe,
            oDB.Dispose();
            _sqlresult = null;

            foreach (Form f in this.MdiChildren)
            {
                f.Close();
                Application.DoEvents();
            }

            if (myExclusionStrings != null) { myExclusionStrings.Clear(); myExclusionStrings = null; }

            //if (myDialogCloser != null)
            //{
            //    // if (myDialogCloser.strExclusionStrings != null) { myDialogCloser.strExclusionStrings.Clear(); }
            //    myDialogCloser.Dispose(); myDialogCloser = null;
            //}
            gloWord.gloWord.ExitWord();
            gloOffice.Supporting.EmptygloPMTempFolder();

            if (m_hotKeys != null) { m_hotKeys = null; }
            if (nBlinkingCells != null) { nBlinkingCells = null; }
            if (oPatientCards != null) { oPatientCards.Dispose(); oPatientCards = null; }
            if (ogloReminder != null) { ogloReminder.Dispose(); ogloReminder = null; }
            if (oClsgloUserRights != null) { oClsgloUserRights.Dispose(); oClsgloUserRights = null; }

            //SLR: Free all global variables declared in the class and then
            if (arrDashBoardToolStrip != null)
            {
                arrDashBoardToolStrip = null;
            }
            if (arrPatientdetailsToolStrip != null)
            {
                arrPatientdetailsToolStrip = null;
            }
            //font_bold.dispose();
            //font_regular.dispose();
            if (ToolTip1 != null)
            {
                ToolTip1.Dispose();
            }
        }

        private bool _isShowHideInProgress = false;
        private void ShowHideMainMenu(bool show, bool showNavigator)
        {
            try
            {
                if (_isShowHideInProgress == false)
                {
                    _isShowHideInProgress = true;

                    this.SuspendLayout();

                    pnlPatient_Demo.Visible = show;
                    pnlCards.Visible = show;
                    if (pnlPatient_Demo.ParentGroup != null) { pnlPatient_Demo.ParentGroup.Visible = show; }
                    pnlPatient_UpComingAppointments.Visible = show;
                    pnlPatient_Details.Visible = show;
                    uipnlPatient_Alert.Visible = showNavigator;
                    pnlPatient_List.Visible = show;

                    if (pnlPatient_Details.DockState == Janus.Windows.UI.Dock.PanelDockState.Floating)
                    {
                        if (pnlPatient_Details.ParentGroup == null)
                            pnlPatient_Details.Parent.Visible = show;
                        else
                            pnlPatient_Details.ParentGroup.Parent.Visible = show;
                    }

                    if (pnlPatient_UpComingAppointments.DockState == Janus.Windows.UI.Dock.PanelDockState.Floating)
                    {
                        if (pnlPatient_UpComingAppointments.ParentGroup == null)
                            pnlPatient_UpComingAppointments.Parent.Visible = show;
                        else
                            pnlPatient_UpComingAppointments.ParentGroup.Parent.Visible = show;
                    }

                    if (pnlPatient_Demographics.DockState == Janus.Windows.UI.Dock.PanelDockState.Floating)
                    {
                        if (pnlPatient_Demographics.ParentGroup == null)
                            pnlPatient_Demographics.Parent.Visible = show;
                        else
                            pnlPatient_Demographics.ParentGroup.Parent.Visible = show;
                    }

                    if (pnlCards.DockState == Janus.Windows.UI.Dock.PanelDockState.Floating)
                    {
                        if (pnlCards.ParentGroup == null)
                            pnlCards.Parent.Visible = show;
                        else
                            pnlCards.ParentGroup.Parent.Visible = show;
                    }

                    if (pnlPatient_Demo.DockState == Janus.Windows.UI.Dock.PanelDockState.Floating)
                    {
                        if (pnlPatient_Demo.ParentGroup == null)
                            pnlPatient_Demo.Parent.Visible = show;
                        else
                            pnlPatient_Demo.ParentGroup.Parent.Visible = show;
                    }

                    if (uipnlPatient_Alert.DockState == Janus.Windows.UI.Dock.PanelDockState.Floating)
                    {
                        if (uipnlPatient_Alert.ParentGroup == null)
                            uipnlPatient_Alert.Parent.Visible = show;
                        else
                            uipnlPatient_Alert.ParentGroup.Parent.Visible = show;
                    }
                    if (this.ActiveMdiChild == null)
                    {
                        mnuCopayDist_ByCharge.Enabled = true;
                        mnuCopayDist_ByAccount.Enabled = true;
                    }
                    //else if (this.ActivateMdiChild( )
                    //{
                    //    mnuCopayDist_ByCharge.Enabled = true;
                    //    mnuCopayDist_ByAccount.Enabled = true;
                    //}
                    this.ResumeLayout();
                    _isShowHideInProgress = false;
                }
            }
            catch
            {
                _isShowHideInProgress = false;
            }
        }
        //private void EnableDisableBadDebtPatientButtons(bool showButtons)
        //{
        //    tsb_Billing.Enabled = showButtons;
        //    tsb_Appointment.Enabled = showButtons;
        //    mnuGo_Billing.Enabled = showButtons;
        //    mnuGo_Appointment.Enabled = showButtons;
        //}
        //private void EnableDisableBadDebtPatientButtonswithLockChart(bool showButtons)
        //{
        //    tsb_Billing.Enabled = showButtons;
        //   // tsb_Appointment.Enabled = showButtons;
        //    mnuGo_Billing.Enabled = showButtons;
        //    //mnuGo_Appointment.Enabled = showButtons;
        //}

        private void EnableDisableLockedPatientButtons(bool showButtons)
        {
            // MODIFY PATIENT //
            tsb_PatientModification.Enabled = showButtons;
            mnuGo_ModifyPatient.Enabled = showButtons;

            // APPOINTMENTS //
            tsb_Appointment.Enabled = showButtons;
            mnuGo_Appointment.Enabled = showButtons;

            // PATIENT PAYMENT //
            tsb_PaymentPatient.Enabled = showButtons;
            mnuGo_PaymentPatient.Enabled = showButtons;

            // ADVANCE //
            tsb_Advance.Enabled = showButtons;

            // PATIENT ACCOUNT //
            tsb_PatLedger.Enabled = showButtons;
            mnuGo_PatientLedger.Enabled = showButtons;

            // LEDGER //
            mnuView_PatientTemplates.Enabled = showButtons;

            // PATIENT FORM //
            mnuGo_EOBLedger.Enabled = showButtons;

            //DMS
            tsb_ScanDocs.Enabled = showButtons;
            mnuGo_ScanDocument.Enabled = showButtons;
            mnuView_Documents.Enabled = showButtons;
            SetLicenseModule();
        }

        private void OnPatientPaymentClicked(object sender, EventArgs e, TaskChangeEventArg e2)
        {
            Int64 IntuitBillPayPatientID = 0;
            try
            {
                IntuitBillPayPatientID = GetIntuitBillPayPatientOfTask(e2.TaskID);
                gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(IntuitBillPayPatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Copay);
                frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                frmPatientPaymentV2.ShowInTaskbar = false;
                frmPatientPaymentV2.IBPAuthNumber = e2.IBPAuthNumber;
                frmPatientPaymentV2.IBPCardType = e2.IBPCardType;
                frmPatientPaymentV2.IBPCheckamount = e2.IBPCheckamount;
                frmPatientPaymentV2.IBPReferenceNumber = e2.IBPReferenceNumber;
                frmPatientPaymentV2.IsIntuitBillPay = e2.IsIntuitBillPay;
                frmPatientPaymentV2.IBPTaskID = e2.TaskID;
                frmPatientPaymentV2.ShowDialog(this);
                e2.IBPToken = frmPatientPaymentV2.IsTaskCompleted;
                frmPatientPaymentV2.Dispose();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }

        }

        #endregion

        #region "Patient Events from Main Menu"
        void gloCntrlPatient_GridRowSelect_Click(object sender, DataGridViewCellEventArgs e)
        {


            //20100106 Mahesh Nawal Add code for Unnecessarily display the scroll bar in Patient details panel while changing the patient 

            c1PatientDetails.ScrollBars = System.Windows.Forms.ScrollBars.None;
            // SUDHIR 20100212 // IF METHOD CALLED FROM OTHER FUNCTIONS LIKE PATIENT STATUS //
            if (e == null)
            {
                gloCntrlPatient.ClearSearch();
            }

            Int64 nPrevSelectedPatient = _CurrentPatientId;
            //appSettings["PatientID"] = Convert.ToString(gloCntrlPatient.PatientID);
            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
            if (oSecurity.isPatientLock(gloCntrlPatient.PatientID, false))
            {
                _CurrentPatientId = 0;
                EnableDisableLockedPatientButtons(false);
            }
            else
            {
                _CurrentPatientId = gloCntrlPatient.PatientID;
                tsb_Billing.Enabled = true;
                mnuGo_Billing.Enabled = true;
                EnableDisableLockedPatientButtons(true);
            }

            //Fill Patient Information only if patient is changed. 
            if (nPrevSelectedPatient != _CurrentPatientId || nPrevSelectedPatient == 0)
            {
                FillSelectedPatient();
            }

            //if (!oSecurity.isPatientLock(gloCntrlPatient.PatientID, false) && oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, true))
            // {

            //     EnableDisableLockedPatientButtons(true);
            //     EnableDisableBadDebtPatientButtons(false);
            //     appSettings["PatientID"] = Convert.ToString(0);
            //     _CurrentPatientId = gloCntrlPatient.PatientID;
            // }
            //else if (oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, true))
            //   {

            //    oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
            //    if (oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, true))
            //       {
            //           pnlBadDebt.Visible = true;
            //       }
            //       else
            //       {
            //           pnlBadDebt.Visible = false;
            //       }   

            //       _isBadDebtWithLockChart = true;
            //       EnableDisableBadDebtPatientButtonswithLockChart(false);
            //       appSettings["PatientID"] = Convert.ToString(0);
            //       _CurrentPatientId = gloCntrlPatient.PatientID;
            //   }
            // else
            // {
            //     appSettings["PatientID"] = Convert.ToString(_CurrentPatientId);
            //     _isBadDebtWithLockChart = false;
            // }

            if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }


            Cursor.Current = Cursors.WaitCursor;
            // _SearchFlag = true;
            gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
            Cursor.Current = Cursors.Default;

            appSettings["PatientID"] = Convert.ToString(_CurrentPatientId);
            c1PatientDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        }

        void gloCntrlPatient_Grid_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //Resolved Bug no. 93692 :: gloPM->Right Click Patient->Select Template->Gives Exception
                _IsFromAppointment = false;
                cmnuPatientItem_Template.Visible = true;
                cmnuPatientItem_PatientAlert.Visible = true;
                cmnuPatientItem_Cases.Visible = true;
                gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(Program.GetConnectionString());

                object oResultAllowEditablePatientCode;
                ogloSettings.GetSetting("Allow-Editable Patient Code", out oResultAllowEditablePatientCode);

                Int32 _AllowEditablePatientCode = 0;
                if (oResultAllowEditablePatientCode != null && oResultAllowEditablePatientCode.ToString() != "")
                {
                    _AllowEditablePatientCode = Convert.ToInt32(oResultAllowEditablePatientCode);
                }
                if (_AllowEditablePatientCode != 0)
                {
                    cmnuPatientItem_SaveAsCopy.Visible = true;
                }
                else
                {
                    cmnuPatientItem_SaveAsCopy.Visible = false;
                }
                //SLR: Free oglosetting, oResultAllowEditablePatientCode
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                }
                if (oResultAllowEditablePatientCode != null)
                {
                    oResultAllowEditablePatientCode = null;
                }
                FillCheckInCheckOutMenu();
                gloCntrlPatient.ContextMenuStrip = cmnu_PatientList;

                if ((_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased") && (Convert.ToString(appSettings["BreakTheGlass"]) != "" || appSettings["BreakTheGlass"].ToUpper() == "FALSE"))
                {
                    string strName = "Allow Emergency Access of Patient Chart";
                    foreach (ToolStripMenuItem Exisingitem in this.cmnu_PatientList.Items)
                    {
                        if (Exisingitem.Text == strName)
                        {
                            Exisingitem.Visible = true;
                            return;
                        }
                    }
                    System.Drawing.Image img = global::gloPM.Properties.Resources.Password_Policy1;
                    ToolStripMenuItem item = new ToolStripMenuItem(strName, img);
                    item.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                    item.Name = "cmnuPatientItem_EmergencyAccess";
                    item.Click += new System.EventHandler(cmnuPatientItem_EmergencyAccess_Click);
                    item.Visible = true;
                    cmnu_PatientList.Items.Add(item);
                }
                else
                {
                    string strName = "Allow Emergency Access of Patient Chart";
                    foreach (ToolStripMenuItem item in this.cmnu_PatientList.Items)
                    {
                        if (item.Text == strName)
                        {
                            //SLR: before removing, remove event handler,
                            try
                            {
                                item.Click -= new System.EventHandler(cmnuPatientItem_EmergencyAccess_Click);
                            }
                            catch
                            {
                            }
                            this.cmnu_PatientList.Items.Remove(item);
                            //SLR: Dispose the item also
                            item.Dispose();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void gloCntrlPatient_Grid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Modify Patient on Double Click
            try
            {


                if (e.RowIndex >= 0)
                {
                    //Added By MaheshB
                    gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                    if (_CurrentPatientId == 0)
                    {
                        if (oSecurity != null)
                        {
                            oSecurity.Dispose();
                        }
                        return;
                    }

                    if (appSettings["UserName"] != null)
                    {
                        if (appSettings["UserName"] != "")
                        { gloPMGlobal.UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                        else
                        { gloPMGlobal.UserName = ""; }
                    }
                    else
                    { gloPMGlobal.UserName = ""; }
                    //
                    if (gloPMGlobal.UserName.Trim() != "")
                    {
                        //SLR: Free exisitng memory before allocating once more
                        if (oClsgloUserRights != null)
                        {
                            oClsgloUserRights.Dispose();
                            oClsgloUserRights = null;
                        }
                        oClsgloUserRights = new gloUserRights.ClsgloUserRights(gloPMGlobal.DatabaseConnectionString);
                        oClsgloUserRights.CheckForUserRights(gloPMGlobal.UserName);
                        if (oClsgloUserRights.ModifyPatient)
                        {
                            tsb_PatientModification_Click(null, null);
                        }
                    }
                    //SLR: Finally free oSecurity, 
                    if (oSecurity != null)
                    {
                        oSecurity.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Fill_Templates(ToolStripMenuItem cmnuTemplate)
        {
            //SLR: before clearing, free cmnuTemplateItem_Click event handler, one by one dispose and then fianlly
            try
            {
                for (int oCatCount = cmnuTemplate.DropDownItems.Count - 1; oCatCount >= 0; oCatCount--)
                {
                    try
                    {
                        ToolStripMenuItem oCatMenuItem = cmnuTemplate.DropDownItems[oCatCount] as ToolStripMenuItem;
                        for (int oSubCount = oCatMenuItem.DropDownItems.Count - 1; oSubCount >= 0; oSubCount--)
                        {
                            try
                            {
                                ToolStripMenuItem oTemplateItem = oCatMenuItem.DropDownItems[oCatCount] as ToolStripMenuItem;
                                oTemplateItem.Click -= new EventHandler(cmnuTemplateItem_Click);
                                oTemplateItem.DropDownItems.Clear();
                                try
                                {
                                    oTemplateItem.Dispose();
                                }
                                catch
                                {
                                }
                            }
                            catch
                            {
                            }
                        }
                        oCatMenuItem.DropDownItems.Clear();
                        try
                        {
                            oCatMenuItem.Dispose();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }

                //                cmnuTemplate.Click -= new EventHandler(cmnuTemplateItem_Click);
            }
            catch
            {
            }
            cmnuTemplate.DropDownItems.Clear();

            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);

            DataTable dtCategories = null;
            DataTable dtTemplates = null;

            String CategoryName = "";
            _IsFromAppointment = false;
            try
            {
                oDB.Connect(false);

                //Get All Category
                dtCategories = ogloTemplate.GetTemplateCategoryList();

                //Get All Templates
                string _sqlQuery = " SELECT  TemplateGallery_MST.nTemplateID, TemplateGallery_MST.sTemplateName AS sTemplateName, TemplateGallery_MST.nCategoryID, TemplateGallery_MST.sCategoryName " +
                                   " FROM  TemplateGallery_MST ";


                oDB.Retrive_Query(_sqlQuery, out dtTemplates);

                if (dtCategories != null)
                {
                    if (dtCategories.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCategories.Rows.Count; i++)
                        {
                            ToolStripMenuItem oCatMenuItem = new ToolStripMenuItem();
                            CategoryName = dtCategories.Rows[i]["CategoryName"].ToString();
                            oCatMenuItem.Text = CategoryName;
                            oCatMenuItem.ForeColor = Color.FromArgb(31, 73, 125);

                            // COMMENTED BY SUDHIR - 20090127 -- AS CategoryID refferance is removed. //

                            DataRow[] drSelectedTemplates = dtTemplates.Select(" sCategoryName = '" + CategoryName.Replace("'", "''") + "'");


                            if (drSelectedTemplates != null && drSelectedTemplates.Length > 0)
                            {
                                for (int j = 0; j < drSelectedTemplates.Length; j++)
                                {
                                    ToolStripMenuItem oTemplateItem = new ToolStripMenuItem();
                                    oTemplateItem.Text = Convert.ToString(drSelectedTemplates[j]["sTemplateName"]);
                                    oTemplateItem.Tag = Convert.ToString(drSelectedTemplates[j]["nTemplateID"]);
                                    oTemplateItem.ForeColor = Color.FromArgb(31, 73, 125);
                                    oCatMenuItem.DropDownItems.Add(oTemplateItem);
                                    oTemplateItem.Click += new EventHandler(cmnuTemplateItem_Click);
                                }
                            }

                            drSelectedTemplates = null;
                            //Template - Finish
                            cmnuTemplate.DropDownItems.Add(oCatMenuItem);

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (dtTemplates != null) { dtTemplates.Dispose(); }
                if (dtCategories != null) { dtCategories.Dispose(); }
            }
        }
        private void clearAssociationMenus(ToolStripMenuItem oCatMenuItem)
        {
            for (int i = oCatMenuItem.DropDownItems.Count - 1; i >= 0; i--)
            {

                try
                {
                    ToolStripMenuItem oTemplateItem = oCatMenuItem.DropDownItems[i] as ToolStripMenuItem;
                    oTemplateItem.Click -= new EventHandler(cmnuTemplateItem_Click);

                    oCatMenuItem.DropDownItems.RemoveAt(i);
                    try
                    {
                        oTemplateItem.Dispose();
                    }
                    catch
                    {
                    }
                }
                catch
                {

                }

            }

        }
        private ToolStripMenuItem Get_AssociationTemplates(gloOffice.AssociationCategories oAssociateCategory)
        {
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dtTemplates; //SLR: New is not needed
            ToolStripMenuItem oCatMenuItem = new ToolStripMenuItem();
            _IsFromAppointment = true;
            try
            {
                oDB.Connect(false);
                dtTemplates = ogloTemplate.GetAssociation(oAssociateCategory);


                oCatMenuItem.Text = oAssociateCategory.ToString();
                if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                {
                    ToolStripMenuItem oTemplateItem;
                    for (int j = 0; j < dtTemplates.Rows.Count; j++)
                    {
                        oTemplateItem = new ToolStripMenuItem();
                        oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                        oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                        oTemplateItem.ForeColor = Color.FromArgb(31, 73, 125);
                        oCatMenuItem.DropDownItems.Add(oTemplateItem);
                        oTemplateItem.Click += new EventHandler(cmnuTemplateItem_Click);
                        oTemplateItem = null;
                    }
                }
                if (dtTemplates != null) { dtTemplates.Dispose(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                //SLR: Free ogloTemplate
                if (ogloTemplate != null)
                {
                    ogloTemplate.Dispose();
                }
            }
            return oCatMenuItem;
        }

        private void cmnuTemplateItem_Click(object sender, EventArgs e)
        {

            // IF CONDITION BY SUDHIR 20101029 //
            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
            {
                //Bug #81090: 00000879: deceased patient status
                MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            //const int COL_MSTAPTID = 0;
            const int COL_APTDTL = 1;
            const int COL_DATE = 3;

            if (sender != null)
            {
                ToolStripMenuItem cmnuTemplateItem; // = new ToolStripMenuItem(); //SLR: new not needed
                cmnuTemplateItem = (ToolStripMenuItem)sender;

                gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(gloPMGlobal.DatabaseConnectionString);
                ogloTemplate.CategoryID = Convert.ToInt64(cmnuTemplateItem.OwnerItem.Tag);
                ogloTemplate.CategoryName = cmnuTemplateItem.OwnerItem.Text;
                ogloTemplate.TemplateID = Convert.ToInt64(cmnuTemplateItem.Tag);
                if (_IsFromAppointment == true)
                    ogloTemplate.PrimeryID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.Row, COL_APTDTL));//0;// Convert.ToInt64(cmnuTemplateItem.Tag);
                else
                    ogloTemplate.PrimeryID = 0;// Convert.ToInt64(cmnuTemplateItem.Tag);
                ogloTemplate.TemplateName = cmnuTemplateItem.Text;
                ogloTemplate.PatientID = Convert.ToInt64(_CurrentPatientId);
                ogloTemplate.ClinicID = gloPMGlobal.ClinicID;
                //Resolved Bug no. 93692 :: gloPM->Right Click Patient->Select Template->Gives Exception
                if (_IsFromAppointment == true)
                {
                    ogloTemplate.FromDate = gloDateMaster.gloDate.DateAsNumber(c1PatientDetails.GetData(c1PatientDetails.Row, COL_DATE).ToString());
                    //'Bug #92723: 00001067: Appointment 
                    ogloTemplate.ToDate = gloDateMaster.gloDate.DateAsNumber(c1PatientDetails.GetData(c1PatientDetails.Row, COL_DATE).ToString());
                }
                gloOffice.frmWd_PatientTemplate frm;
                //SLR: Can we not check for exisitng frm if any and create only if not existing?
                if (_IsFromAppointment == true)
                {
                    frm = new gloOffice.frmWd_PatientTemplate(gloPMGlobal.DatabaseConnectionString, _IsFromAppointment, ogloTemplate);
                }
                else
                {
                    frm = new gloOffice.frmWd_PatientTemplate(gloPMGlobal.DatabaseConnectionString, ogloTemplate);
                }
                frm.Text = cmnuTemplateItem.Text;
                frm.MdiParent = this;
                frm.Show();
                ShowHideMainMenu(false, false);
                frm.WindowState = FormWindowState.Maximized;
                //SLR: Free ogloTemplate
                if (ogloTemplate != null)
                {
                    ogloTemplate.Dispose();
                }
            }

        }

        private void FillSelectedPatient()
        {
            Cursor.Current = Cursors.WaitCursor;
            FillDemographicInformation();
            FillCardInformation();
            FillAlerts();
            showPatientDetails();
            ShowEligibilityCheck();


            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
            {
                c1CopayAlert.Visible = false;
                c1EligibilityCheck.Visible = false;
                c1PatientAlerts.Visible = false;
            }
            else
            {
                c1CopayAlert.Visible = true;
                c1EligibilityCheck.Visible = true;
                c1PatientAlerts.Visible = true;
            }

            Cursor.Current = Cursors.Default;
        }

        private void FillDemographicInformation()
        {
            lblPD_Name.Text = "";
            lblPD_Code.Text = "";
            lblPD_DateofBirth.Text = "";
            lblPD_Address.Text = "";
            lblPD_HomePhone.Text = "";
            lblPD_MobilePhone.Text = "";
            lblPD_Email.Text = "";
            lblPD_FaxPhone.Text = "";
            picPD_Photo.Image = null;
            lblPD_Pharmacy.Text = "";
            lblPD_Physician.Text = "";
            lblPD_Referral.Text = "";
            lblProvider.Text = "";
            lblPD_PCP_Mobile.Text = "";
            lblPD_PCP_Phone.Text = "";

            lblRace.Text = "";
            lblEthinicity.Text = "";
            lblLanguage.Text = "";
            lblPatStatus.Text = "";

            lblOccupation.Text = "";
            lblWorkPhone.Text = "";
            lblBusinessCenter.Text = "";
            lblPrimaryInsurance.Text = "";
            lblSecondaryInsurance.Text = "";
            lbl_TertiaryInsurance.Text = "";

            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            DataTable dtPatient = null;

            //7022Items: Home Billing
            //Bug #47238: EMR - When Zip code Setting is off for 9 digit then 9 digit displayed on Patient Demographics
            //Create object used to get setting from database
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
            object oUseAreaCode = null;

            try
            {

                if (_CurrentPatientId != 0)
                {
                    dtPatient = ogloPatient.GetPatientDemographics(_CurrentPatientId);
                }
                else
                {
                    if (appSettings["CurrentPatientStatus"] == "Deceased")
                    {
                        dtPatient = ogloPatient.GetPatientDemographics(gloCntrlPatient.PatientID);
                    }
                    else
                    {
                        return;
                    }
                }
                //
                if (dtPatient == null || dtPatient.Rows.Count < 1)
                {
                    return;
                }

                _PatientProviderId = Convert.ToInt64(dtPatient.Rows[0]["nProviderID"]);
                //Adding Patient Demographic Information to Quick Review panel on dash board.
                lblPD_DateofBirth.Text = Convert.ToString(dtPatient.Rows[0]["dtDOB"]);
                lblPD_Age.Text = FormatAge(Convert.ToDateTime(dtPatient.Rows[0]["dtDOB"]));
                lblPD_Referral.Text = Convert.ToString(dtPatient.Rows[0]["sReferral"]);
                lblPD_Physician.Text = Convert.ToString(dtPatient.Rows[0]["sPCPName"]);
                lblPD_Pharmacy.Text = Convert.ToString(dtPatient.Rows[0]["sPharmacy"]);
                lblProvider.Text = Convert.ToString(dtPatient.Rows[0]["sProviderName"]);
                _IsExemptFromReport = Convert.ToBoolean(dtPatient.Rows[0]["nExemptFromReport"]);
                if (lblPD_Physician.Text.Trim() != "")
                {
                    lblPD_PCP_Phone.Text = Convert.ToString(dtPatient.Rows[0]["sPCPPhone"]);
                    lblPD_PCP_Mobile.Text = Convert.ToString(dtPatient.Rows[0]["sPCPMobile"]);
                }
                /////---------

                lblPD_Gender.Text = Convert.ToString(dtPatient.Rows[0]["sGender"]);


                lblRace.Text = Convert.ToString(dtPatient.Rows[0]["sRace"]);
                lblEthinicity.Text = Convert.ToString(dtPatient.Rows[0]["sEthn"]);
                lblLanguage.Text = Convert.ToString(dtPatient.Rows[0]["SLang"]);


                lblPD_HomePhone.Text = Convert.ToString(dtPatient.Rows[0]["sPhone"]);
                lblPD_MobilePhone.Text = Convert.ToString(dtPatient.Rows[0]["sMobile"]);
                lblPD_FaxPhone.Text = Convert.ToString(dtPatient.Rows[0]["sFAX"]);
                lblPD_Email.Text = Convert.ToString(dtPatient.Rows[0]["sEmail"]);
                lblEMContact.Text = Convert.ToString(dtPatient.Rows[0]["sEmergencyContact"]);
                lblEMPhone.Text = Convert.ToString(dtPatient.Rows[0]["sEmergencyPhone"]);
                lblEMMobile.Text = Convert.ToString(dtPatient.Rows[0]["sEmergencyMobile"]);

                // Problem# - 00000495 :middle name not showing in PM  dashboard.
                lblPD_Name.Text = Convert.ToString(dtPatient.Rows[0]["sFirstName"]) + " " + Convert.ToString(dtPatient.Rows[0]["sMiddleName"]) + " " + Convert.ToString(dtPatient.Rows[0]["sLastName"]);
                lblPD_Code.Text = Convert.ToString(dtPatient.Rows[0]["sPatientCode"]);
                //lblPD_DOB.Text = (oPatient.DemographicsDetail.PatientDOB.Date).ToString("MM-dd-yyyy");
                string _strAddress = "";
                _strAddress = Convert.ToString(dtPatient.Rows[0]["sAddressLine1"]).Trim() + Environment.NewLine;
                if (Convert.ToString(dtPatient.Rows[0]["sAddressLine2"]).Trim() != "")
                {
                    _strAddress = _strAddress + Convert.ToString(dtPatient.Rows[0]["sAddressLine2"]).Trim() + Environment.NewLine;
                }


                if (Convert.ToString(dtPatient.Rows[0]["sCity"]).Trim() != "")
                    _strAddress = _strAddress + Convert.ToString(dtPatient.Rows[0]["sCity"]).Trim() + ", ";
                if (Convert.ToString(dtPatient.Rows[0]["sState"]).Trim() != "")
                    _strAddress = _strAddress + Convert.ToString(dtPatient.Rows[0]["sState"]).Trim() + " ";
                if (Convert.ToString(dtPatient.Rows[0]["sZIP"]).Trim() != "")
                {
                    _strAddress = _strAddress + Convert.ToString(dtPatient.Rows[0]["sZIP"]).Trim();
                    //7022Items: Home Billing
                    //Bug #47238: EMR - When Zip code Setting is off for 9 digit then 9 digit displayed on Patient Demographics
                    //Get setting from database and accordingly shows area code in patient demographics on dashboard.
                    oSettings.GetSetting("USEAREACODEFORPATIENT", out oUseAreaCode);
                    if (Convert.ToBoolean(Convert.ToInt16(oUseAreaCode)) == true)
                    {
                        //7022Items: Home Billing If codition is added to show area code alongwith zip if patient country is US.
                        if (Convert.ToString(dtPatient.Rows[0]["sCountry"]).Trim() == "US")
                        {
                            //7022Items: Home Billing Area code appended with zip code
                            if (Convert.ToString(dtPatient.Rows[0]["sAreaCode"]).Trim() != "")
                            {
                                _strAddress = _strAddress + "-" + Convert.ToString(dtPatient.Rows[0]["sAreaCode"]).Trim();
                            }
                        }
                    }


                }
                lblPD_Address.Text = _strAddress;
                lblPatStatus.Text = Convert.ToString(dtPatient.Rows[0]["sPatientStatus"]).Trim();
                if (lblPatStatus.Text == "Deceased")
                {
                    lblPatStatus.ForeColor = Color.Red;
                }
                else
                {
                    lblPatStatus.ForeColor = Color.FromArgb(255, 31, 73, 125);
                }
                lblPatMedCat.Text = Convert.ToString(dtPatient.Rows[0]["MedicalCategory"]).Trim();

                lblOccupation.Text = Convert.ToString(dtPatient.Rows[0]["sOccupation"]).Trim();
                lblWorkPhone.Text = Convert.ToString(dtPatient.Rows[0]["sWorkPhone"]).Trim();
                lblBusinessCenter.Text = Convert.ToString(dtPatient.Rows[0]["BusinessCenter"]).Trim();

                lblPrimaryInsurance.Text = Convert.ToString(dtPatient.Rows[0]["PrimaryIns"]).Trim();
                lblSecondaryInsurance.Text = Convert.ToString(dtPatient.Rows[0]["SecondaryIns"]).Trim();
                lbl_TertiaryInsurance.Text = Convert.ToString(dtPatient.Rows[0]["TertiaryIns"]).Trim();

                //10475/Start/Dashboard >> Application does not display patient image/photo in demographics panel
                if ((dtPatient.Rows[0]["iPhoto"] != DBNull.Value))
                {
                    //SLR: Added a static function to retrive the same.

                    //byte[] MyPictureBoxControl = (byte[])(dtPatient.Rows[0]["iPhoto"]);
                    //gloPictureBox.gloPictureBox myPixBx = new gloPictureBox.gloPictureBox();
                    //myPixBx.byteImage = MyPictureBoxControl;
                    //System.Drawing.Image PatientPhoto =  myPixBx.copyFrame(new Rectangle(0, 0, picPD_Photo.Width, picPD_Photo.Height), true);
                    //myPixBx.Dispose();
                    //myPixBx = null;
                    picPD_Photo.BackgroundImageLayout = ImageLayout.Stretch;
                    picPD_Photo.SizeMode = PictureBoxSizeMode.CenterImage;
                    picPD_Photo.Image = gloPictureBox.gloImage.GetImage((byte[])(dtPatient.Rows[0]["iPhoto"]), picPD_Photo.Width, picPD_Photo.Height);
                    //picPD_Photo.Image =(Image) aspectratio(PatientPhoto);

                }

                picPD_Photo.BackgroundImage = null;
                if (picPD_Photo.Image == null)
                {
                    if (lblPD_Gender.Text == "Male")
                    {
                        picPD_Photo.BackgroundImage = gloPM.Properties.Resources.MalePatient;
                    }
                    else if (lblPD_Gender.Text == "Female")
                    {
                        picPD_Photo.BackgroundImage = gloPM.Properties.Resources.FemalePatient;
                    }
                }
                picPD_Photo.BackgroundImageLayout = ImageLayout.Stretch;
                GetMedicalCategoryImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                ogloPatient.Dispose();
                //7022Items: Home Billing
                //Bug #47238: EMR - When Zip code Setting is off for 9 digit then 9 digit displayed on Patient Demographics
                //Dispose object used to get setting from database
                oSettings.Dispose();
                //SLR: Free oUserAreaCode, dtPatient
                if (oUseAreaCode != null)
                {
                    oUseAreaCode = null;
                }
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                }
                SetLicenseModule();
            }
        }

        public string FormatAge(DateTime BirthDate)
        {
            string _AgeStr = "";
            try
            {
                // Compute the difference between BirthDate 'CODE FROM gloPM
                //year and end year. 
                bool IsBirthDateLeap = false;
                int years = DateTime.Now.Year - BirthDate.Year;
                int months = 0;
                int days = 0;
                //Test if BirthDay for LeapYear.
                if (BirthDate.Day == 29 & BirthDate.Month == 2)
                {
                    IsBirthDateLeap = true;
                }
                // Check if the last year was a full year. 
                if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
                {
                    years -= 1;
                }
                BirthDate = BirthDate.AddYears(years);
                // Now we know BirthDate <= end and the diff between them 
                // is < 1 year. 
                if (BirthDate.Year == DateTime.Now.Year)
                {
                    months = DateTime.Now.Month - BirthDate.Month;
                }
                else
                {
                    months = (12 - BirthDate.Month) + DateTime.Now.Month;
                }
                // Check if the last month was a full month. 
                if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
                {
                    months -= 1;
                }
                BirthDate = BirthDate.AddMonths(months);
                // Now we know that BirthDate < end and is within 1 month 
                // of each other. 
                days = (DateTime.Now - BirthDate).Days;

                //To Adjust Age if BirthDate is 29th Feb in leap year
                if (IsBirthDateLeap == true)
                {
                    //'Sequence of following IF code is too important.. DON'T MODIFY
                    days -= 1;
                    if (DateTime.Now.Day == 29 & DateTime.Now.Month == 2)
                    {
                        days += 1;
                    }
                    else if (DateTime.Now.Year % 4 == 0)
                    {
                        days += 1;
                    }
                    if (days < 0 & DateTime.Now.Year % 4 != 0)
                    {
                        days = 30;
                        months = months - 1;
                        if (months < 0)
                        {
                            months = 11;
                            years = years - 1;
                        }
                    }
                    if (months == 12)
                    {
                        days = 30;
                        months = 11;

                    }
                }


                //Return years & " years " & months & " months " & days & " days"
                //Following code to display age in Numeric and Text
                //Dim age As New AgeDetail
                //age.Age = years & " Years " & months & " Months " & days & " Days"
                //' Cases

                //'20081119   ''Following Code to Store ExactAge in String
                //if (gblShowAgeInDays == true & gblAgeLimit >= DateDiff(DateInterval.Day, (System.DateTime)_BDate, System.DateTime.Now.Date))
                //{

                if (years == 0)
                {
                    if (months == 0)
                    {
                        if (days <= 1)
                        {
                            _AgeStr = days + " Day";
                        }
                        else
                        {
                            _AgeStr = days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = months + " Month";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = months + " Months";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = months + " Months " + days + " Days";
                        }
                    }
                }
                else if (years == 1)
                {
                    if (months == 0)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year " + months + " Month ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year " + months + " Months ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + months + " Months " + days + " Days";
                        }
                    }
                }
                else if (years > 1)
                {
                    if (months == 0)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years " + months + " Month";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years " + months + " Months";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + months + " Months " + days + " Days";
                        }
                    }
                }
                //}
                //else
                //{
                //    //ShowAgeInDay is False OR AgeLimit less than Settings.
                //    if (years == 0)
                //    {
                //        //Added by pravin on 11/25/2008
                //        //                If months = 0 And months = 1 Then
                //        if (months == 1)
                //        {
                //            _AgeStr = months + " Month";
                //        }
                //        else if (months > 1)
                //        {
                //            _AgeStr = months + " Months";
                //        }
                //    }
                //    else if (years == 1)
                //    {
                //        if (months == 0)
                //        {
                //            _AgeStr = years + " Year ";
                //        }
                //        else if (months == 1)
                //        {
                //            _AgeStr = years + " Year " + months + " Month ";
                //        }
                //        else if (months > 1)
                //        {
                //            _AgeStr = years + " Year " + months + " Months ";
                //        }
                //    }
                //    else if (years > 1)
                //    {
                //        if (months == 0)
                //        {
                //            _AgeStr = years + " Years ";
                //        }
                //        else if (months == 1)
                //        {
                //            _AgeStr = years + " Years " + months + " Month ";
                //        }
                //        else if (months > 1)
                //        {
                //            _AgeStr = years + " Years " + months + " Months ";
                //        }
                //    }
                //    //Added by pravin if age in days  11/25/2008
                //    if (years == 0 & months == 0)
                //    {
                //        if (days <= 1)
                //        {
                //            _AgeStr = days + " Day";
                //        }
                //        else
                //        {

                //            _AgeStr = days + " Days";
                //        }

                //    }
                //}

            }
            catch //(Exception ex)
            {
                _AgeStr = "";
            }
            return _AgeStr;
        }

        private void FillCardInformation()
        {
            oPatientCards.Clear();

            picPC_Cards.Image = null;
            picPC_Cards.Tag = null;

            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            try
            {
                //SLR: FRee memory before allocating again
                if (oPatientCards != null)
                {
                    oPatientCards = null;
                }
                oPatientCards = ogloPatient.GetSinglePatientCard(_CurrentPatientId);
                if (oPatientCards.Count > 0)
                {
                    picPC_Cards.Image = oPatientCards[0].CardImage;
                    picPC_Cards.Tag = 0;

                    lblScannedDate.Text = "Scanned : " + oPatientCards[0].ScannedDateTime.ToString("MM/dd/yyyy");
                }
                else
                {
                    lblScannedDate.Text = "";
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ogloPatient.Dispose();
            }
        }

        private void GetPatientDemographicsSettings()
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
            object value = null;
            try
            {
                pnl_Demo_DOB.Visible = false;
                pnl_Demo_Gender.Visible = false;
                pnl_HomePhone.Visible = false;
                pnl_Demo_Mobile.Visible = false;
                pnl_Fax.Visible = false;
                pnl_Email.Visible = false;
                pnl_EmContacts.Visible = false;
                pnl_EmPhone.Visible = false;
                pnl_EMmobile.Visible = false;
                pnl_PD_Pharmacy.Visible = false;
                pnl_PD_Referral.Visible = false;
                pnl_PD_Physician.Visible = false;
                pnl_PD_Provider.Visible = false;
                //Sandip Darade 20091202
                pnl_PD_PCPMobile.Visible = false;
                pnl_PD_PCPPhone.Visible = false;

                pnl_Race.Visible = false;
                pnl_Ethinicity.Visible = false;
                pnl_Language.Visible = false;
                pnl_PatStatus.Visible = false;
                pnl_MedCat.Visible = false;

                pnl_Occupation.Visible = false;
                pnl_WorkPhone.Visible = false;
                pnlBusinessCenter.Visible = false;
                pnl_PrimaryInsurance.Visible = false;
                pnl_SecondaryInsurance.Visible = false;
                pnl_TertiaryInsurance.Visible = false;

                //ogloSettings.GetSetting("Patient Demographics", out value);
                ogloSettings.GetSetting("Patient Demographics PM", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out value);

                if (value != null)
                {
                    if (Convert.ToString(value).Trim() != "")
                    {
                        string[] PatientDemographics = Convert.ToString(value).Trim().Split(',');
                        for (int i = 0; i < PatientDemographics.Length; i++)
                        {

                            switch (PatientDemographics[i].Trim())
                            {

                                case "Race":
                                    {
                                        pnl_Race.Visible = true;
                                        pnl_Race.BringToFront();
                                    }
                                    break;

                                case "Ethnicity":
                                    {
                                        pnl_Ethinicity.Visible = true;
                                        pnl_Ethinicity.BringToFront();
                                    }
                                    break;

                                case "Language":
                                    {
                                        pnl_Language.Visible = true;
                                        pnl_Language.BringToFront();
                                    }
                                    break;


                                case "DOB":
                                    {
                                        pnl_Demo_DOB.Visible = true;
                                        pnl_Demo_DOB.BringToFront();
                                    }
                                    break;

                                case "Gender":
                                    {
                                        pnl_Demo_Gender.Visible = true;
                                        pnl_Demo_Gender.BringToFront();

                                    }
                                    break;
                                case "Phone":
                                    {
                                        pnl_HomePhone.Visible = true;
                                        pnl_HomePhone.BringToFront();
                                    }
                                    break;
                                case "Mobile":
                                    {
                                        pnl_Demo_Mobile.Visible = true;
                                        pnl_Demo_Mobile.BringToFront();
                                    }
                                    break;
                                case "Fax":
                                    {
                                        pnl_Fax.Visible = true;
                                        pnl_Fax.BringToFront();
                                    }
                                    break;
                                case "Email":
                                    {
                                        pnl_Email.Visible = true;
                                        pnl_Email.BringToFront();
                                    }
                                    break;
                                case "Emergency Contact":
                                    {
                                        pnl_EmContacts.Visible = true;
                                        pnl_EmContacts.BringToFront();
                                    }
                                    break;
                                case "Emergency Phone":
                                    {
                                        pnl_EmPhone.Visible = true;
                                        pnl_EmPhone.BringToFront();
                                    }
                                    break;
                                case "Emergency Mobile":
                                    {
                                        pnl_EMmobile.Visible = true;
                                        pnl_EMmobile.BringToFront();
                                    }
                                    break;
                                case "Provider":
                                    {
                                        pnl_PD_Provider.Visible = true;
                                        pnl_PD_Provider.BringToFront();
                                    }
                                    break;
                                case "Pharmacy":
                                    {
                                        pnl_PD_Pharmacy.Visible = true;
                                        pnl_PD_Pharmacy.BringToFront();
                                    }
                                    break;
                                case "PCP Phone":
                                    {
                                        pnl_PD_PCPPhone.Visible = true;
                                        pnl_PD_PCPPhone.BringToFront();
                                    }
                                    break;
                                case "PCP Mobile":
                                    {
                                        pnl_PD_PCPMobile.Visible = true;
                                        pnl_PD_PCPMobile.BringToFront();
                                    }
                                    break;
                                case "Primary Care Physician":
                                    {
                                        pnl_PD_Physician.Visible = true;
                                        pnl_PD_Physician.BringToFront();
                                    }
                                    break;
                                case "Primary Insurance":
                                    {
                                        pnl_PrimaryInsurance.Visible = true;
                                        pnl_PrimaryInsurance.BringToFront();
                                    }
                                    break;
                                case "Secondary Insurance":
                                    {
                                        pnl_SecondaryInsurance.Visible = true;
                                        pnl_SecondaryInsurance.BringToFront();
                                    }
                                    break;
                                case "Tertiary Insurance":
                                    {
                                        pnl_TertiaryInsurance.Visible = true;
                                        pnl_TertiaryInsurance.BringToFront();
                                    }
                                    break;
                                case "Referral":
                                    {
                                        pnl_PD_Referral.Visible = true;
                                        pnl_PD_Referral.BringToFront();
                                    }
                                    break;
                                case "Status":
                                    {
                                        pnl_PatStatus.Visible = true;
                                        pnl_PatStatus.BringToFront();
                                    }
                                    break;
                                case "Medical Category":
                                    {
                                        pnl_MedCat.Visible = true;
                                        pnl_MedCat.BringToFront();
                                    }
                                    break;
                                case "Occupation":
                                    {
                                        pnl_Occupation.Visible = true;
                                        pnl_Occupation.BringToFront();
                                    }
                                    break;
                                case "Work Phone":
                                    {
                                        pnl_WorkPhone.Visible = true;
                                        pnl_WorkPhone.BringToFront();
                                    }
                                    break;
                                case "Business Center":
                                    {
                                        pnlBusinessCenter.Visible = true;
                                        pnlBusinessCenter.BringToFront();
                                    }
                                    break;

                                default:
                                    break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ogloSettings.Dispose();
                if (value != null)
                {
                    value = null;
                }
            }
        }

        #endregion

        # region " Patient Alerts/ Copay / Eligibility "

        private const int COL_ALERTID = 0;
        private const int COL_ALERTNAME = 1;
        private const int COL_ALERTCOLOR = 2;
        private const int COL_ALERTSTATUS = 3;
        private const int COL_ALERTTYPE = 4;
        private const int COL_PATIENTID = 5;
        private const int COL_COUNT = 6;


        private void DesignGridPatientAlerts()
        {
            try
            {
                c1PatientAlerts.Cols.Count = COL_COUNT;
                c1PatientAlerts.Rows.Count = 1;


                c1PatientAlerts.SetData(0, COL_ALERTID, "nAlertID ");
                c1PatientAlerts.SetData(0, COL_ALERTNAME, "Alerts");
                c1PatientAlerts.SetData(0, COL_ALERTTYPE, "Alert Type");
                c1PatientAlerts.SetData(0, COL_ALERTSTATUS, "Status");
                c1PatientAlerts.SetData(0, COL_PATIENTID, "PatientID");
                c1PatientAlerts.SetData(0, COL_ALERTCOLOR, "Alert Color");


                c1PatientAlerts.Cols[COL_ALERTID].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTNAME].Visible = true;
                c1PatientAlerts.Cols[COL_ALERTTYPE].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTSTATUS].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].Visible = false;
                c1PatientAlerts.Cols[COL_PATIENTID].Visible = false;

                c1PatientAlerts.Cols[COL_ALERTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTSTATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientAlerts.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                int nWidth = pnlPatientAlertMain.Width;
                c1PatientAlerts.Cols[COL_ALERTID].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTNAME].Width = (int)(0.99 * (nWidth));
                c1PatientAlerts.Cols[COL_ALERTSTATUS].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTTYPE].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].Width = 0;
                c1PatientAlerts.Cols[COL_PATIENTID].Width = 0;

                //c1PatientAlerts.Cols[COL_ALERTID].AllowEditing = true; 
                //c1PatientAlerts.Cols[COL_ALERTNAME].AllowEditing = true;
                //c1PatientAlerts.Cols[COL_ALERTTYPE].AllowEditing = true;
                //c1PatientAlerts.Cols[COL_ALERTCOLOR].AllowEditing = true;
                //c1PatientAlerts.Cols[COL_ALERTSTATUS].AllowEditing=true;
                //c1PatientAlerts.Cols[COL_PATIENTID].AllowEditing = true;



                c1PatientAlerts.AllowEditing = false;
                c1PatientAlerts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
                c1PatientAlerts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                c1PatientAlerts.Rows[0].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTNAME].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
                c1PatientAlerts.Cols[COL_ALERTNAME].ImageAndText = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FillAlerts()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParams = new gloDatabaseLayer.DBParameters();

            DataSet _dsAlerts = null;

            DataTable dtLastSeen = null;
            DataTable dtPatAlerts = null;
            DataTable dtNotes = null;
            DataTable dtCopayAlert = null;
            DataTable dtLastPatPmt = null;
            DataTable dtGlobalPeriodCount = null;
            DataRow _drPayments = null;


            int COL_COPAYALERT_INSID = 0;
            int COL_COPAYALERT_INSNAME = 1;
            int COL_COPAYALERT_COPAYAMT = 2;
            int COL_COPAYALERT_ALERTTEXT = 3;
            int COL_COPAYALERT_COUNT = 4;



            int _rowIndex = 0;
            c1PatientAlerts.Rows.Count = 1;
            //SLR: Free before making once more new
            if (nBlinkingCells != null)
            {
                nBlinkingCells = null;
            }
            nBlinkingCells = new ArrayList();
            C1.Win.C1FlexGrid.Node nd;

            pnlCopayAlert.Visible = false;
            pnlPatientAlertMain.Visible = false;
            //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
            //if (_CurrentPatientId == 0)
            //{
            //    if (appSettings["PatientID"] != null)
            //    {
            //        if (oSecurity.isBadDebtPatient(Convert.ToInt64(appSettings["PatientID"]), true))
            //        {
            //            _CurrentPatientId = Convert.ToInt64(appSettings["PatientID"]);
            //        }
            //    }
            //}




            try
            {
                oParams.Add("@PatientID", _CurrentPatientId, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);

                oDB.Retrive("GET_DashboardAlerts", oParams, out _dsAlerts);
                oDB.Disconnect();

                oParams.Clear();

                if (_dsAlerts != null)
                {
                    dtLastSeen = _dsAlerts.Tables[0];
                    dtPatAlerts = _dsAlerts.Tables[1];
                    dtNotes = _dsAlerts.Tables[2];
                    dtCopayAlert = _dsAlerts.Tables[3];
                    dtLastPatPmt = _dsAlerts.Tables[4];
                    dtGlobalPeriodCount = _dsAlerts.Tables[5];
                }

                //c1PatientAlerts.BringToFront();
                c1PatientAlerts.Visible = true;

                #region "Last Seen Date "

                string LastSeendate;
                if (dtLastSeen != null && dtLastSeen.Rows.Count > 0)
                {
                    LastSeendate = Convert.ToString(dtLastSeen.Rows[0]["LastSeenDate"]);
                    if (LastSeendate.Length > 0)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Last Seen On " + Convert.ToDateTime(LastSeendate).ToString("MM/dd/yyyy"));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");

                    }
                }
                #endregion "Last Seen Date "

                #region "Global Period Alert "


                if (dtGlobalPeriodCount != null && dtGlobalPeriodCount.Rows.Count > 0)
                {
                    if (dtGlobalPeriodCount.Rows[0][0].ToString() != "0")
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Global Period in Effect");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                    }
                }

                #endregion "Global Period Alert "

                #region "Patient Alerts"

                if (dtPatAlerts != null && dtPatAlerts.Rows.Count > 0)
                {
                    c1PatientAlerts.Rows.Add();
                    _rowIndex = c1PatientAlerts.Rows.Count - 1;
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Alerts ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                    c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");

                    for (int i = 0; i < dtPatAlerts.Rows.Count; i++)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;

                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, Convert.ToInt64(dtPatAlerts.Rows[i]["nAlertID"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "    " + Convert.ToString(dtPatAlerts.Rows[i]["sAlertName"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, Convert.ToString(dtPatAlerts.Rows[i]["nAlertType"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, Convert.ToString(dtPatAlerts.Rows[i]["bAlertStatus"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, Convert.ToString(dtPatAlerts.Rows[i]["sAlertColor"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, _CurrentPatientId);
                    }
                }

                #endregion "Patient Alerts"

                #region "Notes"

                if (dtNotes != null && dtNotes.Rows.Count > 0)
                {
                    c1PatientAlerts.Rows.Add();
                    _rowIndex = c1PatientAlerts.Rows.Count - 1;
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Calendar Notes ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                    c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");

                    for (int i = 0; i < dtNotes.Rows.Count; i++)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "    " + Convert.ToString(dtNotes.Rows[i]["sNotes"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                    }
                }

                #endregion "Notes"

                c1PatientAlerts.Select(c1PatientAlerts.Rows.Count - 1, COL_ALERTNAME);
                if (c1PatientAlerts.Rows.Count == 1)
                { c1PatientAlerts.Visible = false; }

                //--------------------------------------------------------------------

                #region "Copay Alert"


                AddStyle();

                c1CopayAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
                c1CopayAlert.Cols.Count = COL_COPAYALERT_COUNT;

                c1CopayAlert.Height = 19;
                pnlCopayAlert.Height = 25;

                c1CopayAlert.Rows.Count = 0;
                c1CopayAlert.AllowEditing = false;
                c1CopayAlert.Tree.Column = 3;
                c1CopayAlert.Tree.LineColor = Color.Transparent;

                decimal damount = 0;
                if (dtCopayAlert != null && dtCopayAlert.Rows.Count > 0)
                {
                    if (dtCopayAlert.Rows.Count > 1)
                    {
                        if (Convert.ToDecimal(dtCopayAlert.Compute("sum(nCopay)", "")) > 0)
                        {
                            //-------------------------------------------------------------------------------------------------
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                            c1CopayAlert.Rows.Add();

                            //c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Height = 30;
                            c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                            nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                            nd.Level = 0;

                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");

                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Copay");
                            //-------------------------------------------------------------------------------------------------
                        }

                        for (int iCount = 0; iCount <= dtCopayAlert.Rows.Count - 1; iCount++)
                        {

                            if (Convert.ToDecimal(dtCopayAlert.Rows[iCount]["nCopay"]) != 0)
                            {

                                //-------------------------------------------------------------------------------------------------
                                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                                c1CopayAlert.Rows.Add();

                                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                                nd.Level = 1;

                                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemBold");

                                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, Convert.ToString(dtCopayAlert.Rows[iCount]["sInsuranceName"]));
                                //--------------------------------------------------------------------------------------------------

                                //--------------------------------------------------------------------------------------------------
                                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                                c1CopayAlert.Rows.Add();

                                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                                nd.Level = 1;

                                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");

                                damount = Convert.ToDecimal(dtCopayAlert.Rows[iCount]["nCopay"]);
                                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Expected Copay :  $" + (damount).ToString("N2").Trim());
                                nBlinkingCells.Add(c1CopayAlert.Rows.Count - 1);
                                //-------------------------------------------------------------------------------------------------
                                if (c1CopayAlert.Rows.Count > 0)
                                {
                                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                                }
                            }
                            //if (c1CopayAlert.Rows.Count > 0)
                            //{
                            //    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                            //}

                        }


                    }
                    else
                    {
                        if (Convert.ToDecimal(dtCopayAlert.Rows[0]["nCopay"]) != 0)
                        {
                            //-------------------------------------------------------------------------------------------------
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                            c1CopayAlert.Rows.Add();

                            c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                            nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                            nd.Level = 0;

                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");

                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Copay");
                            //-------------------------------------------------------------------------------------------------

                            //--------------------------------------------------------------------------------------------------
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                            c1CopayAlert.Rows.Add();

                            c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                            nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                            nd.Level = 1;

                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");

                            damount = Convert.ToDecimal(dtCopayAlert.Rows[0]["nCopay"]);
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Expected Copay :  $" + (damount).ToString("N2").Trim());
                            nBlinkingCells.Add(c1CopayAlert.Rows.Count - 1);
                            //-------------------------------------------------------------------------------------------------


                        }

                    }

                }




                #endregion

                #region "Last Payment"
                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                c1CopayAlert.Rows.Add();
                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                nd.Level = 0;

                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");
                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Balance");


                if (dtLastPatPmt != null && dtLastPatPmt.Rows.Count > 0)
                {
                    damount = Convert.ToDecimal(dtLastPatPmt.Rows[0]["dReceiptAmount"]);
                    if (damount != 0)
                    {
                        c1CopayAlert.Height = c1CopayAlert.Height + 19;
                        pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                        c1CopayAlert.Rows.Add();

                        c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                        nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                        nd.Level = 1;

                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");

                        //c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Last Pat. Pmt: " + Convert.ToDateTime(_dtLastPatPmt.Rows[0]["dtCreatedDateTime"]).ToString("MM/dd/yy") + " $" + (damount).ToString("N2").Trim());
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Last Pat. Pmt: " + Convert.ToDateTime(dtLastPatPmt.Rows[0]["dtCreatedDateTime"]).ToString("MM/dd/yyyy"));

                        c1CopayAlert.Rows.Add();

                        c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                        nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                        nd.Level = 1;

                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "                     $" + (damount).ToString("N2").Trim());
                    }
                }
                #endregion

                #region "Patient Balances"

                //gloBilling.EOBPayment.gloEOBPaymentPatient ogloAdvancePayment = new gloBilling.EOBPayment.gloEOBPaymentPatient(gloPMGlobal.DatabaseConnectionString);

                //_drPayments = ogloAdvancePayment.GetPatientBalances(_CurrentPatientId);

                //ogloAdvancePayment.Dispose();

                gloAccountsV2.gloPatientPaymentV2 ogloAdvancePayment = new gloAccountsV2.gloPatientPaymentV2();

                _drPayments = ogloAdvancePayment.GetPatientBalances(_CurrentPatientId);

                ogloAdvancePayment.Dispose();


                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                c1CopayAlert.Rows.Add();

                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                nd.Level = 1;


                if (_drPayments != null)
                {

                    if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                    {
                        damount = Convert.ToDecimal(_drPayments["InsuranceDue"]) + (Convert.ToDecimal(_drPayments["PatientDue"]) + Convert.ToDecimal(_drPayments["BadDebt"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]));
                    }
                    else
                    {
                        damount = Convert.ToDecimal(_drPayments["InsuranceDue"]) + (Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]));
                    }
                    if (damount == 0)
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                    }
                }
                else
                {
                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");

                    damount = 0;
                    c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                }
                //-------------------------------------------------------------------------------------------------

                //-------------------------------------------------------------------------------------------------

                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;
                c1CopayAlert.Rows.Add();

                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                nd.Level = 1;

                if (_drPayments != null)
                {
                    damount = Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]);
                    if (damount == 0)
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                    }
                }
                else
                {
                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");
                    damount = 0;
                    c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                }

                if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                {
                    c1CopayAlert.Height = c1CopayAlert.Height + 19;
                    pnlCopayAlert.Height = pnlCopayAlert.Height + 19;
                    c1CopayAlert.Rows.Add();
                    c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                    nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                    nd.Level = 1;
                    if (_drPayments != null)
                    {
                        damount = Convert.ToDecimal(_drPayments["BadDebt"]);
                        if (damount == 0)
                        {
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultLastChildItem");
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Bad Debt: $" + (damount).ToString("N2").Trim());
                        }
                        else
                        {
                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "BadDebtItem");
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Bad Debt: $" + (damount).ToString("N2").Trim());
                        }
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultLastChildItem");
                        damount = 0;
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Bad Debt: $" + (damount).ToString("N2").Trim());
                    }

                    gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                    if (oSecurity.isBadDebtPatient(_CurrentPatientId, true))
                    {
                        pnlBadDebt.Visible = true;
                    }
                    else
                    {
                        pnlBadDebt.Visible = false;
                    }
                }
                #endregion

                c1CopayAlert.Cols[COL_COPAYALERT_INSID].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_INSNAME].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_COPAYAMT].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_ALERTTEXT].Visible = true;
                c1CopayAlert.Row = -1;
                c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1CopayAlert.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;

                pnlCopayAlert.Height = (c1CopayAlert.Rows.Count * 19) + 6;



                //if (oSecurity.isBadDebtPatient(_CurrentPatientId, true))
                //{
                //    if (appSettings["PatientID"] == null)
                //    {
                //        _CurrentPatientId = 0;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParams != null) { oParams.Dispose(); }
                if (dtNotes != null) { dtNotes.Dispose(); }
                if (dtPatAlerts != null) { dtPatAlerts.Dispose(); }
                if (dtLastSeen != null) { dtLastSeen.Dispose(); }
                //SLR: Free dtGlobalPeriodCount
                if (dtGlobalPeriodCount != null)
                {
                    dtGlobalPeriodCount.Dispose();
                }
                if (_dsAlerts != null) { _dsAlerts.Dispose(); }

                pnlCopayAlert.Visible = true;
                pnlPatientAlertMain.Visible = true;
            }

        }

        private void AddStyle()
        {
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
            Color AlertColor = Color.Red;
            object oValue = null;

            oSettings.GetSetting("BlinkingAlert", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out oValue);
            if (oValue != null && Convert.ToString(oValue) != "")
            {
                if (Convert.ToBoolean(oValue)) { tmrCopayAlertBlink.Start(); }
                else
                {
                    tmrCopayAlertBlink.Stop();
                    if (c1CopayAlert.Rows.Count > 1 && c1CopayAlert.Cols.Count > 1)
                    {
                        c1CopayAlert.SetCellStyle(1, 3, "ChildItemRegular");
                    }
                    _IsColored = false;
                }
            }
            else
            {
                tmrCopayAlertBlink.Stop();
                if (c1CopayAlert.Rows.Count > 1 && c1CopayAlert.Cols.Count > 1)
                {
                    c1CopayAlert.SetCellStyle(1, 3, "ChildItemRegular");
                }
                _IsColored = false;
            }

            oSettings.GetSetting("AlertColor", gloPMGlobal.UserID, gloPMGlobal.ClinicID, out oValue);
            if (oValue != null && Convert.ToString(oValue) != "")
            {
                if (oValue.ToString() == "-1")  //code added to replace white color with blue for PM Alert, v8022 PRD change bugid 70117
                {
                    oValue = "-14726787";
                }
                AlertColor = Color.FromArgb(Convert.ToInt32(oValue));
            }
            else
            {
                AlertColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            }
            //SLR: Create fonts once on load and free while close.

            addCopayAlertStyle("Default");
            style.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            style.ForeColor = Color.White;
            style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Width = 1;


            //style = c1CopayAlert.Styles.Add("Node");

            addCopayAlertStyle("Node");
            style.Font = Font_Bold; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            //style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(122)))), ((int)(((byte)(35)))));
            style.BackColor = System.Drawing.Color.FromArgb(131, 167, 215);
            style.ForeColor = Color.White;
            //style.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(243)))), ((int)(((byte)(206)))));
            style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);//System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(122)))), ((int)(((byte)(35)))));
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Width = 1;



            //  style = c1CopayAlert.Styles.Add("ChildItemBold");
            addCopayAlertStyle("ChildItemBold");
            style.Font = Font_Bold; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            style.ForeColor = AlertColor;//System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));
            style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(240)))), ((int)(((byte)(186)))));

            //style = c1CopayAlert.Styles.Add("ChildItemRegular");
            addCopayAlertStyle("ChildItemRegular");
            style.Font = Font_Regular; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            style.ForeColor = AlertColor;//System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));            
            style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(240)))), ((int)(((byte)(186)))));
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Width = 1;

            //style = c1CopayAlert.Styles.Add("ChildItemRegularRevised");
            addCopayAlertStyle("ChildItemRegularRevised");
            style.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            style.ForeColor = AlertColor;//System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));            
            style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);//System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(240)))), ((int)(((byte)(186)))));
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Width = 1;


            //style = c1CopayAlert.Styles.Add("LastChildItem");
            addCopayAlertStyle("LastChildItem");
            style.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            style.ForeColor = AlertColor; //System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215); //System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(122)))), ((int)(((byte)(35)))));


            //   style = c1CopayAlert.Styles.Add("DefaultChildItemRegular");
            addCopayAlertStyle("DefaultChildItemRegular");
            style.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Width = 1;

            //    style = c1CopayAlert.Styles.Add("DefaultLastChildItem");
            addCopayAlertStyle("DefaultLastChildItem");
            style.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            style.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);

            addCopayAlertStyle("BadDebtItem");
            style.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            style.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);//System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(244)))), ((int)(((byte)(188)))));
            style.ForeColor = System.Drawing.Color.Red; //System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(75)))), ((int)(((byte)(0)))));
            style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal;
            style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215);

            //SLR: FRee oSetting, oValue
            if (oSettings != null)
            {
                oSettings.Dispose();
            }
            if (oValue != null)
            {
                oValue = null;
            }
        }

        private void addCopayAlertStyle(String defaultString)
        {
            try
            {
                if (c1CopayAlert.Styles.Contains(defaultString))
                {
                    style = c1CopayAlert.Styles[defaultString];
                }
                else
                {
                    style = c1CopayAlert.Styles.Add(defaultString);
                }
            }
            catch
            {
                style = c1CopayAlert.Styles.Add(defaultString);
            }
        }


        //Not Used.. delete this method once alerts are verified
        private void ShowCopayAlert()
        {
            int COL_COPAYALERT_INSID = 0;
            int COL_COPAYALERT_INSNAME = 1;
            int COL_COPAYALERT_COPAYAMT = 2;
            int COL_COPAYALERT_ALERTTEXT = 3;
            int COL_COPAYALERT_COUNT = 4;
            decimal damount = 0;

            DataTable _dtCopayAlert = null;
            DataTable _dtLastPatPmt = null;
            DataRow _drPayments = null;
            //SLR: Before making new, free exisitng one
            if (nBlinkingCells != null)
            {
                nBlinkingCells = null;
            }
            nBlinkingCells = new ArrayList();
            C1.Win.C1FlexGrid.Node nd;

            try
            {
                pnlCopayAlert.Visible = false;
                pnlPatientAlertMain.Visible = false;
                AddStyle();

                if (c1PatientAlerts.Rows.Count == 1)
                {
                    c1PatientAlerts.Visible = false;
                }
                else
                {
                    c1PatientAlerts.Visible = true;
                }


                //c1CopayAlert.Visible = false;
                c1CopayAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
                c1CopayAlert.Cols.Count = COL_COPAYALERT_COUNT;
                //c1CopayAlert.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
                //gloBilling.EOBPayment.gloEOBPaymentPatient ogloAdvancePayment = new gloBilling.EOBPayment.gloEOBPaymentPatient(gloPMGlobal.DatabaseConnectionString);
                gloAccountsV2.gloPatientPaymentV2 ogloAdvancePayment = new gloAccountsV2.gloPatientPaymentV2();

                c1CopayAlert.Height = 19;
                pnlCopayAlert.Height = 25;

                c1CopayAlert.Rows.Count = 0;
                //pnlCopayAlert.Visible = true;
                c1CopayAlert.AllowEditing = false;
                c1CopayAlert.Tree.Column = 3;
                c1CopayAlert.Tree.LineColor = Color.Transparent;


                _dtCopayAlert = ogloAdvancePayment.GetExpectedCopayAmt(_CurrentPatientId);


                if (_dtCopayAlert != null && _dtCopayAlert.Rows.Count > 0)
                {

                    if (_dtCopayAlert.Rows.Count > 1)
                    {
                        if (Convert.ToDecimal(_dtCopayAlert.Compute("sum(nCopay)", "")) > 0)
                        {
                            //-------------------------------------------------------------------------------------------------
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                            c1CopayAlert.Rows.Add();

                            //c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Height = 30;
                            c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                            nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                            nd.Level = 0;

                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");

                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Copay");
                            //-------------------------------------------------------------------------------------------------
                        }

                        for (int iCount = 0; iCount <= _dtCopayAlert.Rows.Count - 1; iCount++)
                        {

                            if (Convert.ToDecimal(_dtCopayAlert.Rows[iCount]["nCopay"]) != 0)
                            {

                                //-------------------------------------------------------------------------------------------------
                                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                                c1CopayAlert.Rows.Add();

                                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                                nd.Level = 1;

                                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemBold");

                                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, Convert.ToString(_dtCopayAlert.Rows[iCount]["sInsuranceName"]));
                                //--------------------------------------------------------------------------------------------------

                                //--------------------------------------------------------------------------------------------------
                                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                                c1CopayAlert.Rows.Add();

                                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                                nd.Level = 1;

                                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");

                                damount = Convert.ToDecimal(_dtCopayAlert.Rows[iCount]["nCopay"]);
                                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Expected Copay :  $" + (damount).ToString("N2").Trim());
                                nBlinkingCells.Add(c1CopayAlert.Rows.Count - 1);
                                //-------------------------------------------------------------------------------------------------
                            }
                            if (c1CopayAlert.Rows.Count > 0)
                            {
                                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                            }

                        }


                    }
                    else
                    {
                        if (Convert.ToDecimal(_dtCopayAlert.Rows[0]["nCopay"]) != 0)
                        {
                            //-------------------------------------------------------------------------------------------------
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                            c1CopayAlert.Rows.Add();

                            c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                            nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                            nd.Level = 0;

                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");

                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Copay");
                            //-------------------------------------------------------------------------------------------------

                            //--------------------------------------------------------------------------------------------------
                            c1CopayAlert.Height = c1CopayAlert.Height + 19;
                            pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                            c1CopayAlert.Rows.Add();

                            c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                            nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                            nd.Level = 1;

                            c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");

                            damount = Convert.ToDecimal(_dtCopayAlert.Rows[0]["nCopay"]);
                            c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Expected Copay :  $" + (damount).ToString("N2").Trim());
                            nBlinkingCells.Add(c1CopayAlert.Rows.Count - 1);
                            //-------------------------------------------------------------------------------------------------


                        }

                    }

                }
                //-------------------------------------------------------------------------------------------------
                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                c1CopayAlert.Rows.Add();

                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                nd.Level = 0;

                c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Node");

                c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Balance");
                //-------------------------------------------------------------------------------------------------

                //-------------------------------------------------------------------------------------------------

                _dtLastPatPmt = ogloAdvancePayment.GetLastPatientPmtAmt(_CurrentPatientId);

                if (_dtLastPatPmt.Rows.Count > 0)
                {
                    damount = Convert.ToDecimal(_dtLastPatPmt.Rows[0]["nCheckAmount"]);
                    if (damount != 0)
                    {
                        c1CopayAlert.Height = c1CopayAlert.Height + 19;
                        pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                        c1CopayAlert.Rows.Add();

                        c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                        nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                        nd.Level = 1;

                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");

                        //c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Last Pat. Pmt: " + Convert.ToDateTime(_dtLastPatPmt.Rows[0]["dtCreatedDateTime"]).ToString("MM/dd/yy") + " $" + (damount).ToString("N2").Trim());
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Last Pat. Pmt: " + Convert.ToDateTime(_dtLastPatPmt.Rows[0]["dtCreatedDateTime"]).ToString("MM/dd/yyyy"));

                        c1CopayAlert.Rows.Add();

                        c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                        nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                        nd.Level = 1;

                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");
                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "                     $" + (damount).ToString("N2").Trim());
                    }
                }
                //else
                //{
                //    damount = 0;
                //    c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Last Pat. Pmt: $" + (damount).ToString("N2").Trim());
                //}

                //-------------------------------------------------------------------------------------------------

                _drPayments = ogloAdvancePayment.GetPatientBalances(_CurrentPatientId);

                //-------------------------------------------------------------------------------------------------

                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;

                c1CopayAlert.Rows.Add();

                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                nd.Level = 1;


                if (_drPayments != null)
                {


                    damount = Convert.ToDecimal(_drPayments["InsuranceDue"]) + (Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]));
                    if (damount == 0)
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");

                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "ChildItemRegular");

                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                    }
                }
                else
                {
                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultChildItemRegular");

                    damount = 0;
                    c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Total Bal: $" + (damount).ToString("N2").Trim());
                }
                //-------------------------------------------------------------------------------------------------

                //-------------------------------------------------------------------------------------------------

                c1CopayAlert.Height = c1CopayAlert.Height + 19;
                pnlCopayAlert.Height = pnlCopayAlert.Height + 19;
                c1CopayAlert.Rows.Add();

                c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].IsNode = true;
                nd = c1CopayAlert.Rows[c1CopayAlert.Rows.Count - 1].Node;
                nd.Level = 1;



                if (_drPayments != null)
                {

                    damount = Convert.ToDecimal(_drPayments["PatientDue"]) - Convert.ToDecimal(_drPayments["AvailableReserve"]);
                    if (damount == 0)
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultLastChildItem");

                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                    }
                    else
                    {
                        c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");

                        c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                    }

                }
                else
                {
                    c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "DefaultLastChildItem");

                    damount = 0;
                    c1CopayAlert.SetData(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "Patient Due: $" + (damount).ToString("N2").Trim());
                }

                //c1CopayAlert.SetCellStyle(c1CopayAlert.Rows.Count - 1, COL_COPAYALERT_ALERTTEXT, "LastChildItem");
                //-------------------------------------------------------------------------------------------------


                c1CopayAlert.Cols[COL_COPAYALERT_INSID].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_INSNAME].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_COPAYAMT].Visible = false;
                c1CopayAlert.Cols[COL_COPAYALERT_ALERTTEXT].Visible = true;
                c1CopayAlert.Row = -1;

                c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1CopayAlert.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;



                pnlCopayAlert.Height = (c1CopayAlert.Rows.Count * 19) + 6;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //c1CopayAlert.Visible = true;
                pnlCopayAlert.Visible = true;
                pnlPatientAlertMain.Visible = true;
                if (_dtCopayAlert != null)
                {
                    _dtCopayAlert.Dispose();
                    _dtCopayAlert = null;
                }
                if (_dtLastPatPmt != null)
                {
                    _dtLastPatPmt.Dispose();
                    _dtLastPatPmt = null;
                }
                if (_drPayments != null)
                {
                    _drPayments = null;
                }

            }
        }

        private void c1CopayAlert_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1CopayAlert != null && c1CopayAlert.Rows.Count > 0)
                {
                    if (c1CopayAlert.GetData(0, 0) != null && Convert.ToString(c1CopayAlert.GetData(0, 0)).Trim() != "")
                    {
                        Int64 _insId = 0;
                        string _insName = "";
                        decimal _copayAmt = 0;

                        _insId = Convert.ToInt64(c1CopayAlert.GetData(0, 0));
                        _insName = Convert.ToString(c1CopayAlert.GetData(0, 1));
                        _copayAmt = Convert.ToDecimal(c1CopayAlert.GetData(0, 2));

                        //gloBilling.frmAdvancePayment ofrmAdvancePayment = new frmAdvancePayment(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId, _insName, _insId, _copayAmt, PaymentOtherType.Copay);
                        //ofrmAdvancePayment.StartPosition = FormStartPosition.CenterScreen;
                        //ofrmAdvancePayment.ShowDialog();
                        //ofrmAdvancePayment.Dispose();


                        //gloBilling.frmPaymentPatient oPaymentInsurace = new frmPaymentPatient(_CurrentPatientId, false, 0, 0, 0, 0, EOBPaymentSubType.Copay);
                        gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(_CurrentPatientId, false, 0, 0, 0, 0, EOBPaymentSubType.Copay);
                        frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                        frmPatientPaymentV2.ShowInTaskbar = false;
                        frmPatientPaymentV2.ShowDialog(this);
                        frmPatientPaymentV2.Dispose();

                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void ShowEligibilityCheck()
        {

            int COL_ELIGIBILITYCHECK_INSID = 0;
            //int COL_ELIGIBILITYCHECK_INSNAME = 1;
            //int COL_ELIGIBILITYCHECK_COPAYAMT = 2;
            //int COL_ELIGIBILITYCHECK_ALERTTEXT = 3;
            int COL_ELIGIBILITYCHECK_COUNT = 1;
            int rowIndex = 0;
            string strAlert = "";

            try
            {
                //tmrCopayAlert.Stop();
                c1EligibilityCheck.Cols.Count = COL_ELIGIBILITYCHECK_COUNT;
                //c1EligibilityCheck.Rows.Count = 1;

                //gloAdvancePayment ogloAdvancePayment = new gloAdvancePayment(gloPMGlobal.DatabaseConnectionString);
                DataTable _dtEligibilityCheck;  //SLR: New is not needed
                _dtEligibilityCheck = GetEligibilityDate(_CurrentPatientId);

                if (_dtEligibilityCheck != null && _dtEligibilityCheck.Rows.Count > 0 && _dtEligibilityCheck.Rows[0]["dtEligibilityCheck"].ToString() != "")
                {
                    pnlEligibilityCheck.Visible = true;

                    //SLR: Create font once.........................................................................................................
                    c1EligibilityCheck.AllowEditing = false;
                    C1.Win.C1FlexGrid.CellStyle cs = null; //c1CopayAlert.Styles.Add("Blink");
                    try
                    {
                        if (c1CopayAlert.Styles.Contains("Blink"))
                        {
                            cs = c1CopayAlert.Styles["Blink"];
                        }
                        else
                        {
                            cs = c1CopayAlert.Styles.Add("Blink");
                        }
                    }
                    catch
                    {
                        cs = c1CopayAlert.Styles.Add("Blink");
                    }

                    cs.Font = Font_Bold; // new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    c1EligibilityCheck.Rows.Count = 0;


                    for (int i = 0; i < _dtEligibilityCheck.Rows.Count; i++)
                    {
                        c1EligibilityCheck.Rows.Add();
                        rowIndex = c1EligibilityCheck.Rows.Count - 1;
                        strAlert = "";
                        strAlert = "Eligibility Check was done for ";
                        strAlert = strAlert + Convert.ToString(_dtEligibilityCheck.Rows[0]["sPayerName"]) + " " + "On ";
                        strAlert = strAlert + Convert.ToString(Convert.ToDateTime(_dtEligibilityCheck.Rows[i]["dtEligibilityCheck"]).Date.ToShortDateString());
                        // solving sales force case - GLO2010-0008403
                        //strAlert = strAlert + " " + "at " + Convert.ToDateTime(_dtEligibilityCheck.Rows[i]["dtEligibilityCheck"]).TimeOfDay.ToString().Remove(8);
                        strAlert = strAlert + " " + "at " + String.Format("{0:t}", Convert.ToDateTime(_dtEligibilityCheck.Rows[i]["dtEligibilityCheck"]));
                        // end 
                        c1EligibilityCheck.SetData(rowIndex, COL_ELIGIBILITYCHECK_INSID, strAlert);
                        c1EligibilityCheck.SetCellStyle(rowIndex, COL_ELIGIBILITYCHECK_INSID, cs);
                    }

                    c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                }
                else
                {
                    pnlEligibilityCheck.Visible = false;
                }
                //SLR: Finally free ogloAdvancePayment, dtEligibilityCheck
                if (_dtEligibilityCheck != null)
                {
                    _dtEligibilityCheck.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private DataTable GetEligibilityDate(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dtEligibility = null;
            try
            {
                oDB.Connect(false);
                //string query = "Select max(dtEligibilityCheck) as dtEligibilityCheck,sPayerName from BL_EligibilityResponse_MST where nPatientID='" + PatientId + "' group by sPayerName";
                string query = "Select top 1 dtEligibilityCheck,sPayerName from BL_EligibilityResponse_MST where nPatientID=" + PatientId + "  order by dtEligibilityCheck desc";
                oDB.Retrive_Query(query, out dtEligibility);
                if (dtEligibility.Rows.Count > 0 && dtEligibility.Rows[0]["dtEligibilityCheck"].ToString() != "")
                {
                    //return "Eligibility Check was done On '" + Convert.ToDateTime(dtEligibility.Rows[0]["dtEligibilityCheck"]).Date.ToShortDateString() + "' at  '" + Convert.ToDateTime(dtEligibility.Rows[0]["dtEligibilityCheck"]).TimeOfDay.ToString().Remove(8) + "'";
                    oDB.Disconnect();
                    return dtEligibility;

                }
                else
                {
                    oDB.Disconnect();
                    return null;

                }

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        private Int64 GetIntuitBillPayPatientOfTask(Int64 TaskId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Int64 nIntuitBillPayPatientID = 0;

            object _result = null;
            string _sqlQuery = string.Empty;

            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT DISTINCT " +
                            "        ISNULL(nPatientID, 0) AS nPatientID " +
                            " FROM    TM_TaskMST " +
                            " WHERE   nTaskID = " + TaskId;

                _result = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_result != null && Convert.ToString(_result) != "")
                {
                    if (Convert.ToInt64(_result) > 0)
                    {
                        nIntuitBillPayPatientID = Convert.ToInt64(_result);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                //SLR: FRee result
                if (_result != null)
                {
                    _result = null;
                }
            }
            return nIntuitBillPayPatientID;
        }
        #endregion

        #region "Patient CheckIn CheckOut"

        private void cmnuPatientItem_CheckIn_Click(object sender, EventArgs e)
        {
            // IF CONDITION BY SUDHIR 20101029 //
            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
            {
                //Bug #81090: 00000879: deceased patient status
                MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable odtPARequired = null;
            DataTable dtIsPA = null;
            try
            {
                oDB.Connect(false);
                string strSQL = "";
                if (Convert.ToString(((ToolStripItem)sender).Tag) != "")
                {
                    String[] IDs = Convert.ToString(((ToolStripItem)sender).Tag).Split(',');
                    Int64 MasterAppointmentID = Convert.ToInt64(IDs[0]);
                    Int64 DetailAppointmentID = Convert.ToInt64(IDs[1]);


                    strSQL = "SELECT ISNULL(bIsPARequired,0) FROM AS_Appointment_DTL WHERE nMSTAppointmentID=" + MasterAppointmentID + " AND nDTLAppointmentID=" + DetailAppointmentID;
                    oDB.Retrive_Query(strSQL, out dtIsPA);
                    if (dtIsPA != null)
                    {
                        if (dtIsPA.Rows[0][0].ToString().Trim() == "True")
                        {
                            strSQL = "SELECT ISNULL(PatientPriorAuthorization_Transaction.nAuthorizationNo,0) AS nAuthorizationNo  "
                                    + " FROM PatientPriorAuthorization_Transaction "
                                    + " INNER JOIN AS_Appointment_DTL "
                                    + " ON PatientPriorAuthorization_Transaction.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID "
                                    + " WHERE PatientPriorAuthorization_Transaction.nMSTAppointmentID = " + MasterAppointmentID;

                            oDB.Retrive_Query(strSQL, out odtPARequired);
                            if (odtPARequired == null)
                            {
                                MessageBox.Show("Warning – Today's appointment requires authorization but authorization is not found.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                            else
                            {
                                if (odtPARequired.Rows.Count == 0)
                                {
                                    MessageBox.Show("Warning – Today's appointment requires authorization but authorization is not found.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }

                        }
                    }

                    gloPMGeneral.frmCheckIn ofrmCheckIn = new gloPMGeneral.frmCheckIn(_CurrentPatientId, MasterAppointmentID, DetailAppointmentID, gloPMGlobal.DatabaseConnectionString);
                    ofrmCheckIn.ShowDialog(this);
                    ofrmCheckIn.Dispose();
                }
                FillPatientStatus();
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SLR: Finally disconnect odb, free oDB, dtISPA, odtPARequired
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dtIsPA != null)
                {
                    dtIsPA.Dispose();
                }
                if (odtPARequired != null)
                {
                    odtPARequired.Dispose();
                }
            }
        }

        private void cmnuPatientItem_CheckOut_Click(object sender, EventArgs e)
        {
            DataTable dtTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);

            try
            {
                oDB.Connect(false);
                string strSQLQuery = string.Empty;

                // IF CONDITION BY SUDHIR 20101029 //
                //Bug : 00000775: Patient Card. Used separate variable for right click -> check out scenario
                if (_ChkOutPatientId == 0)
                {
                    //Bug #81090: 00000879: deceased patient status
                    MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (Convert.ToString(((ToolStripItem)sender).Tag) != "")
                {
                    String[] IDs = Convert.ToString(((ToolStripItem)sender).Tag).Split(',');
                    Int64 MasterAppointmentID = Convert.ToInt64(IDs[0]);
                    Int64 DetailAppointmentID = Convert.ToInt64(IDs[1]);

                    DateTime dtStartDate = new DateTime();
                    if (IDs.Length > 2)
                    {
                        dtStartDate = Convert.ToDateTime(gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(IDs[2])));
                    }
                    else
                    {
                        strSQLQuery = "SELECT dtStartDate FROM AS_Appointment_DTL WHERE nMSTAppointmentID=" + MasterAppointmentID + " AND nDTLAppointmentID=" + DetailAppointmentID;
                        oDB.Retrive_Query(strSQLQuery, out dtTable);
                        if (dtTable != null)
                        {
                            if (dtTable.Rows.Count > 0)
                            {
                                dtStartDate = Convert.ToDateTime(gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(dtTable.Rows[0][0])));
                            }
                        }

                    }

                    gloPMGeneral.PatientStatus oPatientStatus = new gloPMGeneral.PatientStatus(gloPMGlobal.DatabaseConnectionString);

                    //Added By MaheshB 
                    //Bug : 00000775: Patient Card. Used separate variable for right click -> check out scenario
                    oPatientStatus.PatientID = _ChkOutPatientId;
                    //**
                    oPatientStatus.patientStatusDate = DateTime.Now;
                    oPatientStatus.TimeIn = "";
                    oPatientStatus.TimeOut = DateTime.Now.ToLocalTime().ToShortTimeString();
                    oPatientStatus.Location = "";
                    oPatientStatus.Status = "";
                    oPatientStatus.TrackingStatus = 4; // 0=None ,3=CheckIn, 4=CheckOut
                    oPatientStatus.MasterAppointmentID = MasterAppointmentID;
                    oPatientStatus.DetailAppointmentID = DetailAppointmentID;
                    oPatientStatus.StartAppointmentDate = dtStartDate;

                    //Bug : 00000775: Patient Card. Used separate variable for right click -> check out scenario
                    DisplayGlobalPeriodMessage(_ChkOutPatientId);

                    oPatientStatus.PatientCheckOut();
                    //SLR:Finally disconnect odb, free odb, dtable, oPatientStatus
                    if (oPatientStatus != null)
                    {
                        oPatientStatus.Dispose();
                    }
                }
                FillPatientStatus();
                showPatientDetails();


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SLR:Finally disconnect odb, free odb, dtable, oPatientStatus
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dtTable != null)
                {
                    dtTable.Dispose();
                }
            }
        }
        private void DisplayGlobalPeriodMessage(long _patientId)
        {
            PatientStatus objPatientStatus = new PatientStatus(gloPMGlobal.DatabaseConnectionString);

            DataTable _dtGlobalPeriod = null;
            _dtGlobalPeriod = objPatientStatus.GetGlobalPeriods_ForAlter(_patientId);

            if (_dtGlobalPeriod != null)
            {
                if (_dtGlobalPeriod.Rows.Count == 1)
                {
                    String _strMessage = "Today’s visit falls within a Global Period:"
                                     + Environment.NewLine + "CPT : " + _dtGlobalPeriod.Rows[0]["CPT"].ToString().Trim()
                                     + Environment.NewLine + "Dates : " + _dtGlobalPeriod.Rows[0]["Dates"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Provider"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Provider : " + _dtGlobalPeriod.Rows[0]["Provider"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Insurance"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Insurance : " + _dtGlobalPeriod.Rows[0]["Insurance"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Reminder"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Reminder : " + _dtGlobalPeriod.Rows[0]["Reminder"].ToString().Trim();
                    if (_dtGlobalPeriod.Rows[0]["Notes"].ToString().Trim() != "")
                        _strMessage = _strMessage + Environment.NewLine + "Comment : " + _dtGlobalPeriod.Rows[0]["Notes"].ToString().Trim();
                    MessageBox.Show(_strMessage, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_dtGlobalPeriod.Rows.Count > 1)
                {
                    MessageBox.Show("Today’s visit falls within MULTIPLE Global Periods.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //SLR: Finally free objPatientStatus, dtGlobalPeriod
            if (objPatientStatus != null)
            {
                objPatientStatus.Dispose();
            }
            if (_dtGlobalPeriod != null)
            {
                _dtGlobalPeriod.Dispose();
            }

        }

        private void FillCheckInCheckOutMenu()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable odtAppointment = null;
            //DataTable odtTracking = null;
            try
            {
                cmnuPatientItem_CheckIn.Visible = false;
                cmnuPatientItem_CheckOut.Visible = false;

                clearCheckinCheckoutMenus();

                oDB.Connect(false);
                string strSQL = "";

                #region "Fill Check out Appointments menu"


                //Get Checked in appointment for selected patient 
                strSQL = "SELECT	AS_Appointment_DTL.nMSTAppointmentID AS nMSTAppointmentID,AS_Appointment_DTL.nDTLAppointmentID AS nDTLAppointmentID,AS_Appointment_DTL.dtStartDate as dtStartDate,"
                + " ISNULL(AS_Appointment_MST.sAppointmentTypeDesc,'')  AS sAppointmentType,AS_Appointment_DTL.dtStartTime, AS_Appointment_DTL.dtEndTime "
                + " FROM AS_Appointment_DTL INNER JOIN AS_Appointment_MST  "
                + " ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID "
                + " WHERE AS_Appointment_DTL.dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString())
                + " AND AS_Appointment_DTL.nClinicID = " + gloPMGlobal.ClinicID + "  "
                + " AND AS_Appointment_DTL.nRefID = 0  AND AS_Appointment_DTL.nUsedStatus = 3 "
                + " AND AS_Appointment_MST.nPatientID = " + _CurrentPatientId + "";

                oDB.Retrive_Query(strSQL, out odtAppointment);

                if (odtAppointment != null && odtAppointment.Rows.Count > 0)
                {
                    for (int i = 0; i < odtAppointment.Rows.Count; i++)
                    {
                        ToolStripItem oToolStripItemCheckOut = new ToolStripMenuItem();
                        oToolStripItemCheckOut.Text = "Checkout : (" + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(odtAppointment.Rows[i]["dtStartTime"])).ToShortTimeString() + " " + Convert.ToString(odtAppointment.Rows[i]["sAppointmentType"]) + ")";
                        oToolStripItemCheckOut.Name = "Checkout";
                        oToolStripItemCheckOut.Image = cmnuPatientItem_CheckOut.Image;
                        //oToolStripItemCheckOut.Tag = Convert.ToString(odtAppointment.Rows[i]["nMSTAppointmentID"]) + "," + Convert.ToString(odtAppointment.Rows[i]["nDTLAppointmentID"]);
                        oToolStripItemCheckOut.Tag = Convert.ToString(odtAppointment.Rows[i]["nMSTAppointmentID"]) + "," + Convert.ToString(odtAppointment.Rows[i]["nDTLAppointmentID"]) + "," + Convert.ToString(odtAppointment.Rows[i]["dtStartDate"]);
                        oToolStripItemCheckOut.Click += new EventHandler(cmnuPatientItem_CheckOut_Click);
                        cmnu_PatientList.Items.Add(oToolStripItemCheckOut);
                        _ChkOutPatientId = _CurrentPatientId;
                    }
                }

                #endregion

                #region "Fill Check In Items"
                //SLR:Free exisitng memory and then
                if (odtAppointment != null)
                {
                    odtAppointment.Dispose();
                    odtAppointment = null;
                }
                //odtAppointment = new DataTable();

                // Get Today's Appointments wich are not checked In
                strSQL = "SELECT AS_Appointment_DTL.nMSTAppointmentID AS nMSTAppointmentID, AS_Appointment_DTL.nDTLAppointmentID AS nDTLAppointmentID, "
                + " ISNULL(AS_Appointment_MST.sAppointmentTypeDesc,'')  AS sAppointmentType,AS_Appointment_DTL.dtStartTime AS dtStartTime, AS_Appointment_DTL.dtEndTime AS dtEndTime "
                + " FROM AS_Appointment_DTL INNER JOIN AS_Appointment_MST ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID  "
                + " WHERE  "
                + " AS_Appointment_MST.nPatientID = " + _CurrentPatientId + " "
                + " AND AS_Appointment_DTL.dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString()) + " "
                + " AND AS_Appointment_DTL.nClinicID = 1 AND ISNULL(AS_Appointment_DTL.nRefID,0)= 0  "
                + " AND (AS_Appointment_DTL.nUsedStatus = 1 OR AS_Appointment_DTL.nUsedStatus = 2) ";

                oDB.Retrive_Query(strSQL, out odtAppointment);

                if (odtAppointment != null && odtAppointment.Rows.Count > 0)
                {
                    for (int i = 0; i < odtAppointment.Rows.Count; i++)
                    {
                        ToolStripItem oToolStripItemCheckIn = new ToolStripMenuItem();
                        oToolStripItemCheckIn.Text = "Checkin : (" + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(odtAppointment.Rows[i]["dtStartTime"])).ToShortTimeString() + " " + Convert.ToString(odtAppointment.Rows[i]["sAppointmentType"]) + ")";
                        oToolStripItemCheckIn.Name = "Checkin";
                        oToolStripItemCheckIn.Image = cmnuPatientItem_CheckIn.Image;
                        oToolStripItemCheckIn.Tag = Convert.ToString(odtAppointment.Rows[i]["nMSTAppointmentID"]) + "," + Convert.ToString(odtAppointment.Rows[i]["nDTLAppointmentID"]);
                        oToolStripItemCheckIn.Click += new EventHandler(cmnuPatientItem_CheckIn_Click);
                        cmnu_PatientList.Items.Add(oToolStripItemCheckIn);
                    }
                }

                #endregion


                oDB.Disconnect();
                //SLR: Dipsoe and then
                oDB.Dispose();
                oDB = null;
                //SLR: Free odtAppointment
                if (odtAppointment != null)
                {
                    odtAppointment.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void clearCheckinCheckoutMenus()
        {
            for (int i = cmnu_PatientList.Items.Count - 1; i >= 0; i--)
            {

                if (Convert.ToString(cmnu_PatientList.Items[i].Name) == "Checkin")
                {
                    try
                    {
                        ToolStripItem oToolStripItemCheckIn = cmnu_PatientList.Items[i];
                        oToolStripItemCheckIn.Click -= new EventHandler(cmnuPatientItem_CheckIn_Click);
                        cmnu_PatientList.Items.RemoveAt(i);
                        try
                        {
                            oToolStripItemCheckIn.Dispose();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    if (Convert.ToString(cmnu_PatientList.Items[i].Name) == "Checkout")
                    {
                        try
                        {
                            ToolStripItem oToolStripItemCheckout = cmnu_PatientList.Items[i];
                            oToolStripItemCheckout.Click -= new EventHandler(cmnuPatientItem_CheckOut_Click);
                            cmnu_PatientList.Items.RemoveAt(i);
                            try
                            {
                                oToolStripItemCheckout.Dispose();
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                        }
                    }

                }

                //if (Convert.ToString(cmnu_PatientList.Items[i].Name) == "Checkin" || Convert.ToString(cmnu_PatientList.Items[i].Name) == "Checkout")
                //{
                //    //SLR: get the toolstripmenu, free its event handler, remove at and then dipsose
                //    try
                //    {
                //        cmnu_PatientList.Items[i].Click -= new System.EventHandler(cmnuPatientItem_EmergencyAccess_Click);
                //    }
                //    catch 
                //    {
                //        // Blank Catch intentionally added.
                //    }
                //    //Bug #53505: 7040 - gloPM - Exception on Dashboard once Check in done for Appointment
                //    // RemoveAt  commented to resolve the bug.
                //    //cmnu_PatientList.Items.RemoveAt(i);
                //    cmnu_PatientList.Items[i].Dispose();
                //}
            }
        }

        private void pnlPatient_UpComingAppointmentsContainer_Resize(object sender, EventArgs e)
        {
            try
            {
                int _cWidth = pnlPatient_UpComingAppointmentsContainer.Width;
                c1PatientStatus.Cols[COL_PATSTATUSPATIENTNAME].Width = (int)(_cWidth * 0.45);
                c1PatientStatus.Cols[COL_PATSTATUS_APPSTARTTIME].Width = (int)(_cWidth * 0.25);
                c1PatientStatus.Cols[COL_PATSTATUSAPPTTYPE].Width = (int)(_cWidth * 0.30);
            }
            catch (Exception) //ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void c1PatientStatus_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                c1PatientStatus.ContextMenuStrip = null;
                c1PatientStatus.Row = c1PatientStatus.HitTest(e.Location).Row;
                if (c1PatientStatus.Row > 0 &&
                    c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUSPATIENTID) != null &&
                    Convert.ToInt64(c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUSPATIENTID)) > 0)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        c1PatientStatus.ContextMenu = null;
                        cmnuPatientItem_CheckIn.Visible = false;
                        cmnuPatientItem_CheckOut.Visible = false;
                        cmnuPatientItem_Template.Visible = false;
                        cmnuPatientItem_PatientAlert.Visible = false;
                        cmnuPatientItem_Cases.Visible = false;

                        if (c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUS_APPIDMST) != null &&
                            Convert.ToInt64(c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUS_APPIDMST)) > 0)
                        {
                            //for (int i = cmnu_PatientList.Items.Count - 1; i >= 0; i--)
                            //{
                            //    if (Convert.ToString(cmnu_PatientList.Items[i].Name) == "Checkin" || Convert.ToString(cmnu_PatientList.Items[i].Name) == "Checkout")
                            //    {
                            //        //SLR: get the toolstripmenu, free its event handler, remove at and then dipsose
                            //        try
                            //        {
                            //            cmnu_PatientList.Items[i].Click -= new System.EventHandler(cmnuPatientItem_EmergencyAccess_Click);
                            //        }
                            //        catch
                            //        {
                            //        }
                            //        //Bug #53505: 7040 - gloPM - Exception on Dashboard once Check in done for Appointment
                            //        //RemoveAt  commented to resolve the bug.
                            //        //cmnu_PatientList.Items.RemoveAt(i);
                            //        cmnu_PatientList.Items[i].Dispose();
                            //    }
                            //}
                            clearCheckinCheckoutMenus();
                            cmnuPatientItem_CheckOut.Visible = true;
                            //Bug : 00000775: Patient Card. Used separate variable for right click -> check out scenario
                            //_CurrentPatientId = Convert.ToInt64(c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUSPATIENTID));
                            _ChkOutPatientId = Convert.ToInt64(c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUSPATIENTID));
                            cmnuPatientItem_CheckOut.Tag = Convert.ToString(c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUS_APPIDMST)) + "," + Convert.ToString(c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUS_APPIDDTL));
                            c1PatientStatus.ContextMenuStrip = cmnu_PatientList;

                            gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                            SetEmergencyAccessMenu();
                            if (oSecurity.isPatientLock(_ChkOutPatientId, false))
                            {
                                cmnuPatientItem_CheckOut.Visible = false;
                                c1PatientStatus.ContextMenuStrip.Items["cmnuPatientItem_EmergencyAccess"].Visible = true;
                            }
                            else
                            { c1PatientStatus.ContextMenuStrip.Items["cmnuPatientItem_EmergencyAccess"].Visible = false; }
                            if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }


                        }
                        //else
                        //{

                        //}
                    }
                    else  //Bug : 00000775: Patient Card. Added else to change selected patient for left click only.
                    {
                        if (c1PatientStatus.Row > 0)
                        {
                            Int64 PatientIdOnStatusGrid = Convert.ToInt64(c1PatientStatus.GetData(c1PatientStatus.Row, COL_PATSTATUSPATIENTID));


                            if (_CurrentPatientId != PatientIdOnStatusGrid)
                            {

                                gloCntrlPatient.PatientID = PatientIdOnStatusGrid;
                                gloCntrlPatient.SelectPatient(PatientIdOnStatusGrid);

                                //_CurrentPatientId is Set by Patient Select Event called after gloCntrlPatient.SelectPatient();
                                //Do not set _CurrentPatientId before SelectPatient Some logic is implemented in Event
                                _CurrentPatientId = PatientIdOnStatusGrid;

                            }
                        }
                    }
                }


            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        private void SetEmergencyAccessMenu()
        {
            try
            {
                string strName = "Allow Emergency Access of Patient Chart";
                foreach (ToolStripMenuItem Exisingitem in this.cmnu_PatientList.Items)
                {
                    if (Exisingitem.Text == strName)
                    {
                        Exisingitem.Visible = true;
                        return;
                    }
                }
                System.Drawing.Image img = global::gloPM.Properties.Resources.Password_Policy1;
                ToolStripMenuItem item = new ToolStripMenuItem(strName, img);
                item.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
                item.Name = "cmnuPatientItem_EmergencyAccess";
                item.Click += new System.EventHandler(cmnuPatientItem_EmergencyAccess_Click);
                item.Visible = true;
                cmnu_PatientList.Items.Add(item);
            }
            catch (Exception)
            {

            }
        }

        private void c1PatientStatus_OwnerDrawCell(object sender, C1.Win.C1FlexGrid.OwnerDrawCellEventArgs e)
        {
            try
            {
                //if (e.Style.BackColor != c1PatientStatus.Styles.Highlight.BackColor)
                //{
                if (e.Row > 0)
                {
                    if (c1PatientStatus.GetData(e.Row, 0) != null)
                    {
                        e.Image = _picBkg.Image;
                    }
                }
                //}
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        //C1 Patient Status Const
        const int COL_PATSTATUSPATIENTID = 0;
        const int COL_PATSTATUSPATIENTNAME = 1;
        const int COL_PATSTATUSSTARTTIME = 2;
        const int COL_PATSTATUS_APPIDMST = 3;
        const int COL_PATSTATUS_APPIDDTL = 4;
        const int COL_PATSTATUS_APPSTARTTIME = 5;
        const int COL_PATSTATUSTEXT = 6;
        const int COL_PATSTATUSAPPTTYPE = 7;
        const int COL_PATSTATUS_FORMATTED_TEXT = 8;
        const int COL_PATSTATUSCOUNT = 9;

        public void FillPatientStatus()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dtPatientStatus = null;
            //DataTable dtPatients;            
            try
            {

                #region "Design Grid"
                c1PatientStatus.DataSource = null;
                c1PatientStatus.Rows.Fixed = 1;
                c1PatientStatus.Rows.Count = 1;
                c1PatientStatus.Cols.Count = COL_PATSTATUSCOUNT;

                c1PatientStatus.AllowEditing = false;

                c1PatientStatus.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
                c1PatientStatus.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
                c1PatientStatus.Styles.Normal.WordWrap = true;
                c1PatientStatus.Styles.Normal.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;


                c1PatientStatus.SetData(0, COL_PATSTATUSPATIENTID, "PatientID");
                c1PatientStatus.SetData(0, COL_PATSTATUSPATIENTNAME, "Patient Name");
                c1PatientStatus.SetData(0, COL_PATSTATUS_APPIDMST, "Master AppointmentID");
                c1PatientStatus.SetData(0, COL_PATSTATUS_APPIDDTL, "Detail AppointmentID");
                c1PatientStatus.SetData(0, COL_PATSTATUS_APPSTARTTIME, "Time");
                c1PatientStatus.SetData(0, COL_PATSTATUSTEXT, "Status");
                c1PatientStatus.SetData(0, COL_PATSTATUSAPPTTYPE, "Type");
                c1PatientStatus.SetData(0, COL_PATSTATUS_FORMATTED_TEXT, "Patient Status");

                c1PatientStatus.Cols[COL_PATSTATUSPATIENTID].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUSPATIENTNAME].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUS_APPIDMST].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUS_APPIDDTL].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUSSTARTTIME].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUSTEXT].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUSAPPTTYPE].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUS_APPSTARTTIME].Visible = false;
                c1PatientStatus.Cols[COL_PATSTATUS_FORMATTED_TEXT].Visible = true;

                int _cWidth = pnlPatient_UpComingAppointmentsContainer.Width;
                //c1PatientStatus.Cols[COL_PATSTATUSSTARTTIME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //c1PatientStatus.Cols[COL_PATSTATUSPATIENTNAME].Width = (int)(_cWidth * 0.45);
                //c1PatientStatus.Cols[COL_PATSTATUS_APPSTARTTIME].Width = (int)(_cWidth * 0.25);
                //c1PatientStatus.Cols[COL_PATSTATUSAPPTTYPE].Width = (int)(_cWidth * 0.30) - 1;
                c1PatientStatus.Cols[COL_PATSTATUSPATIENTID].Width = 0;
                c1PatientStatus.Cols[COL_PATSTATUSPATIENTNAME].Width = 0;
                c1PatientStatus.Cols[COL_PATSTATUS_APPIDMST].Width = 0;
                c1PatientStatus.Cols[COL_PATSTATUS_APPIDDTL].Width = 0;
                c1PatientStatus.Cols[COL_PATSTATUSSTARTTIME].Width = 0;
                c1PatientStatus.Cols[COL_PATSTATUSTEXT].Width = 0;
                c1PatientStatus.Cols[COL_PATSTATUSAPPTTYPE].Width = 0;
                c1PatientStatus.Cols[COL_PATSTATUS_FORMATTED_TEXT].Width = _cWidth;

                //C1.Win.C1FlexGrid.CellStyle csBold = c1PatientStatus.Styles.Add("csBold");
                //csBold.ForeColor = Color.FromArgb(31, 73, 125);
                //csBold.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //csBold.WordWrap = true;
                //c1PatientStatus.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;

                c1PatientStatus.Rows[0].Visible = false;

                C1.Win.C1FlexGrid.CellStyle csNormal = null;// c1PatientStatus.Styles.Add("csNormal");
                try
                {
                    if (c1PatientStatus.Styles.Contains("csNormal"))
                    {
                        csNormal = c1PatientStatus.Styles["csNormal"];
                    }
                    else
                    {
                        csNormal = c1PatientStatus.Styles.Add("csNormal");
                    }
                }
                catch
                {
                    csNormal = c1PatientStatus.Styles.Add("csNormal");
                }
                csNormal.ForeColor = Color.FromArgb(31, 73, 125);
                //SLR: Create font once at the formload and free at formclose
                csNormal.Font = Font_Regular; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csNormal.WordWrap = true;

                c1PatientStatus.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;

                #endregion

                dtPatientStatus = GetCheckinAppointmet();

                if (dtPatientStatus != null)
                {
                    for (int i = 0; i < dtPatientStatus.Rows.Count; i++)
                    {
                        if (dtPatientStatus != null && dtPatientStatus.Rows.Count > 0)
                        {
                            //if (dtPatientStatus.Rows[i]["nTrackingStatus"].ToString() == "3") //Check In
                            // {
                            C1.Win.C1FlexGrid.Row r;
                            r = c1PatientStatus.Rows.Add();

                            c1PatientStatus.SetData(r.Index, COL_PATSTATUSPATIENTID, dtPatientStatus.Rows[i]["nPatientID"].ToString());
                            c1PatientStatus.SetData(r.Index, COL_PATSTATUSPATIENTNAME, dtPatientStatus.Rows[i]["PatientName"].ToString());
                            c1PatientStatus.SetData(r.Index, COL_PATSTATUS_APPIDMST, dtPatientStatus.Rows[i]["nMSTAppointmentID"].ToString());
                            c1PatientStatus.SetData(r.Index, COL_PATSTATUS_APPIDDTL, dtPatientStatus.Rows[i]["nDTLAppointmentID"].ToString());
                            c1PatientStatus.SetData(r.Index, COL_PATSTATUS_APPSTARTTIME, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtPatientStatus.Rows[i]["dtStartTime"])).ToShortTimeString());
                            c1PatientStatus.SetData(r.Index, COL_PATSTATUSAPPTTYPE, dtPatientStatus.Rows[i]["sAppointmentType"].ToString());
                            c1PatientStatus.SetData(r.Index, COL_PATSTATUSTEXT, "Checkin");

                            string _StatusString = "";

                            _StatusString = "  Name : " + Convert.ToString(dtPatientStatus.Rows[i]["PatientName"]) + "" + Environment.NewLine + "" +
                            "   Time : " + gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(dtPatientStatus.Rows[i]["dtStartTime"])).ToShortTimeString() + "";
                            if (Convert.ToString(dtPatientStatus.Rows[0]["sAppointmentType"]) != "")
                            {
                                _StatusString += Environment.NewLine + "   Type : " + Convert.ToString(dtPatientStatus.Rows[i]["sAppointmentType"]) + "";
                            }
                            _StatusString += Environment.NewLine + "   Status : Checkin ";

                            c1PatientStatus.SetData(r.Index, COL_PATSTATUS_FORMATTED_TEXT, _StatusString);
                            c1PatientStatus.SetCellStyle(r.Index, COL_PATSTATUS_FORMATTED_TEXT, csNormal);
                            c1PatientStatus.Rows[r.Index].Height = 65;

                            r = c1PatientStatus.Rows.Add();
                            c1PatientStatus.Rows[r.Index].Height = 1;
                        }

                        //}

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                //throw;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();

                }
                //SLR: Free dtPatientStatus
                if (dtPatientStatus != null)
                {
                    dtPatientStatus.Dispose();
                }

            }

        }

        public DataTable GetCheckinAppointmet()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            oDB.Connect(false);
            DataTable dt = null;
            try
            {

                gloDatabaseLayer.DBParameters oParam = new gloDatabaseLayer.DBParameters();
                oParam.Add("@clinicId", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GetAllCheckInPatient", oParam, out dt);

                //SLR: free oParam
                oParam.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return dt;

        }

        #endregion

        #region "Toolbar Events"

        private void tsb_PatientRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                bool _IsClose = false;
                gloPatient.gloPatient oPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
                oPatient.ShowPatientRegistration(0, out _CurrentPatientId, out _IsClose, this);
                if (_CurrentPatientId > 0)
                {
                    gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
                    gloCntrlPatient.PatientID = _CurrentPatientId;
                    if (appSettings["HL7SENDOUTBOUNDGLOPM"] != null && appSettings["SendPatientDetails"] != null)
                    {
                        if (appSettings["HL7SENDOUTBOUNDGLOPM"] != "" && appSettings["SendPatientDetails"] != "")
                        {
                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOPM"])) == true) && (Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                            {
                                if (_IsClose == false)
                                {
                                    InsertInMessageQueue("A04", _CurrentPatientId, _CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
                                }
                            }
                        }
                    }
                }
                gloCntrlPatient.FillPatients();
                FillSelectedPatient();
                gloCntrlPatient.Refresh();
                gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
                oPatient.Dispose();
                oPatient = null;
                gloCntrlPatient.Select();
                //isBadDebtPatient(_CurrentPatientId, false);
                DataGridViewCellEventArgs ev = new DataGridViewCellEventArgs(0, 0);
                gloCntrlPatient_GridRowSelect_Click(sender, ev);
                //SLR: FRee ev
                ev = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_PatientModification_Click(object sender, EventArgs e)
        {
            try
            {
                if (gloCntrlPatient.PatientID != 0)
                {

                    bool _IsClose = false;
                    gloPatient.gloPatient oPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
                    oPatient.ShowPatientRegistration(SyncPatient(), out _CurrentPatientId, out _IsClose, this);
                    // oPatient.ShowPatientRegistration(gloCntrlPatient.PatientID, out _CurrentPatientId, out _IsClose, this);
                    gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
                    gloCntrlPatient.FillPatients();
                    FillSelectedPatient();
                    gloCntrlPatient.Refresh();
                    gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
                    oPatient.Dispose();
                    gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                    if (oSecurity.isPatientLock(_CurrentPatientId, false))
                    {
                        _CurrentPatientId = 0;
                    }

                    //if (!oSecurity.isPatientLock(gloCntrlPatient.PatientID, false) && oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, false))
                    //{
                    //    _CurrentPatientId = 0;
                    //    EnableDisableLockedPatientButtons(true);
                    //    EnableDisableBadDebtPatientButtons(false);
                    //    appSettings["PatientID"] = Convert.ToString(0);
                    //    _CurrentPatientId = gloCntrlPatient.PatientID;
                    //}
                    //else if (oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, true))
                    //{

                    oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                    if (oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, true))
                    {
                        pnlBadDebt.Visible = true;
                    }
                    else
                    {
                        pnlBadDebt.Visible = false;
                    }
                    //    _CurrentPatientId = 0;
                    //_isBadDebtWithLockChart = true;
                    //    EnableDisableBadDebtPatientButtonswithLockChart(false);
                    //    appSettings["PatientID"] = Convert.ToString(0);
                    //    _CurrentPatientId = gloCntrlPatient.PatientID;
                    //}
                    //else
                    //{
                    //    EnableDisableBadDebtPatientButtons(true);
                    //    _CurrentPatientId = gloCntrlPatient.PatientID;
                    //    _isBadDebtWithLockChart = false;
                    //    appSettings["PatientID"] = Convert.ToString(_CurrentPatientId);
                    //}

                    //SLR: FRee oSecurity
                    oSecurity.Dispose();
                    appSettings["PatientID"] = Convert.ToString(_CurrentPatientId);
                    showPatientDetails();
                    if (appSettings["HL7SENDOUTBOUNDGLOPM"] != null && appSettings["SendPatientDetails"] != null)
                    {
                        if (appSettings["HL7SENDOUTBOUNDGLOPM"] != "" && appSettings["SendPatientDetails"] != "")
                        {
                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOPM"])) == true) && (Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                            {
                                if (_IsClose == false)
                                {
                                    InsertInMessageQueue("A08", _CurrentPatientId, _CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
                                }
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please select a patient to modify.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_PatientScan_Click(object sender, EventArgs e)
        {
            //Scan new Patient 
            gloCardScanning.gloCardScanning oScan = new gloCardScanning.gloCardScanning(gloPMGlobal.DatabaseConnectionString);
            try
            {
                //string _CardRootPath = Application.StartupPath;
                string _CardRootPath = appSettings["StartupPath"].ToString();
                oScan.ScanCard(_CardRootPath, 0, this);
                gloCntrlPatient.FillPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oScan.Dispose();
            }
        }
        private void tsb_Calendar_Click(object sender, EventArgs e)
        {
            try
            {
                gloAppointmentScheduling.gloAppointment oAppointment = new gloAppointmentScheduling.gloAppointment(gloPMGlobal.DatabaseConnectionString);
                //added by chetan for Appointment report changes
                oAppointment.gstrSQLServerName = Program.gSQLServerName;
                oAppointment.gstrDatabaseName = Program.gDatabase;
                oAppointment.gblnSQLAuthentication = !Program.gWindowsAuthentication;
                oAppointment.gstrSQLUser = Program.gLoginUser;
                oAppointment.gstrSQLPassword = Program.gLoginPassword;
                oAppointment.gstrCaption = "gloPM";
                oAppointment.Calendar_Closed += new gloAppointmentScheduling.gloAppointment.CalendarClosed(oAppointment_Calendar_Closed);
                oAppointment.ShowAppointmentView(false, this);
                //SLR: Remove handler and then
                //oAppointment.Dispose();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void oAppointment_Calendar_Closed(object sender, EventArgs e)
        {
            FillPatientStatus();
            showPatientDetails();
        }

        private void tsb_Schedule_Click(object sender, EventArgs e)
        {
            try
            {

                gloAppointmentScheduling.gloSchedule oSchedule = new gloAppointmentScheduling.gloSchedule(gloPMGlobal.DatabaseConnectionString);
                oSchedule.ShowScheduleView(this);
                ShowHideMainMenu(false, false);
                oSchedule.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_Appointment_Click(object sender, EventArgs e)
        {
            try
            {
                using (gloPatient.gloPatient objPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString))
                {
                    // GLO2011-0011970
                    // If patient status is legal pending then don't allow him to create / modify appointment
                    if (objPatient.IsLegalPending(gloCntrlPatient.PatientID))
                    {
                        MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not create an appointment for this patient.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //Validation for TemplateAppointment creation
                    gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);

                    if (ogloSettings.GetSettingUserSpecific("RegisterTemplateAppointmentOnly", gloPMGlobal.UserID, gloPMGlobal.ClinicID) == true)
                    {
                        MessageBox.Show("New appointments can only be set during established template times. This setting can be changed. Please contact your administrator for more information.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (ogloSettings != null) { ogloSettings.Dispose(); }

                    //----

                    gloAppointmentScheduling.gloAppointment oAppointment = new gloAppointmentScheduling.gloAppointment(gloPMGlobal.DatabaseConnectionString);

                    oAppointment.ShowAppointment(SyncPatient(), this);
                    //oAppointment.ShowAppointment(gloCntrlPatient.PatientID, this);

                    oAppointment.Dispose();
                    showPatientDetails();
                    FillAlerts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool CheckMenuItemPresent(string name)
        {
            return this.mnuFile.DropDownItems.ContainsKey(name);
        }

        private bool CheckBatchPrintProcessRunning()
        {

            try
            {
                bool flag = true;
                List<string> lstform = new List<string>();
                foreach (Form oFrm in System.Windows.Forms.Application.OpenForms)
                {

                    if ((oFrm.Name == "frmgloPrintWordProgressController") || (oFrm.Name == "frmgloPrintBatchPatientStatementController") || (oFrm.Name == "frmgloPrintSetupDateActionTemplate"))
                    {

                        if (flag == true)
                        {
                            DialogResult dg = MessageBox.Show("Background printing is in progress. Do you want to cancel the printing?", "gloPM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if ((dg == DialogResult.Yes))
                            {
                                lstform.Add(oFrm.Name);
                                //oFrm.Close();
                                flag = false;
                                // break; // TODO: might not be correct. Was : Exit For
                            }
                            else
                            {
                                oFrm.Visible = true;

                                return true;
                                //break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        else
                        {
                            //   oFrm.Close();
                            lstform.Add(oFrm.Name);
                            flag = false;
                        }
                    }
                }

                if (lstform.Count > 0)
                {
                    int cnt = 0;
                    while (cnt < lstform.Count)
                    {
                        Form frm = Application.OpenForms[Convert.ToString(lstform[cnt])];
                        frm.Close();
                        cnt += 1;
                    }
                    lstform.Clear();
                }
                lstform = null;
                return false;

            }
            catch (Exception)
            {
                //ex = null;
                return false;
            }

        }


        public void PrintMenuEventclick(bool blnAddMenu = true, string formname = "")
        {
            if ((blnAddMenu == true))
            {
                ToolStripMenuItem oBatchPrintMenu = new ToolStripMenuItem();

                bool itemexists = false;
                oBatchPrintMenu.ForeColor = Color.FromArgb(31, 73, 125);
                // Dim fnt As New Font("Tahoma", 8.25)
                oBatchPrintMenu.Font = gloGlobal.clsgloFont.gFont_SMALL;
                oBatchPrintMenu.Tag = "";
                //   oBatchPrintMenu.Image = Resources.BackgroundPrinting.ToBitmap();
                if (formname == "frmgloPrintWordProgressController")
                {
                    oBatchPrintMenu.Click += oBatchPrintMenuProgressControllerClick;
                    oBatchPrintMenu.Text = "Appointment Batch Print";
                    itemexists = CheckMenuItemPresent("Appointment Batch Print");
                }
                if (formname == "frmgloPrintBatchPatientStatementController")
                {
                    oBatchPrintMenu.Text = "Statement Batch Print";
                    oBatchPrintMenu.Click += oBatchPrintPatientStatementControllerClick;
                    itemexists = CheckMenuItemPresent("Statement Batch Print");
                }
                if (formname == "frmgloPrintSetupDateActionTemplate")
                {
                    oBatchPrintMenu.Text = "Claim Template Print";
                    oBatchPrintMenu.Click += oBatchPrintSetupDateActionTemplate;
                    itemexists = CheckMenuItemPresent("Claim Template Print");
                }
                if (itemexists == false)
                {
                    this.mnuFile.DropDownItems.Insert(this.mnuFile.DropDownItems.Count - 1, oBatchPrintMenu);
                }
            }
            else
            {
                for (int iCount = this.mnuFile.DropDownItems.Count - 1; iCount >= 0; iCount += -1)
                {
                    ToolStripMenuItem oBatchPrintMenu = (ToolStripMenuItem)this.mnuFile.DropDownItems[iCount];

                    if (((oBatchPrintMenu == null) == false))
                    {

                        if (((oBatchPrintMenu.Text != null)))
                        {

                            if ((oBatchPrintMenu.Text == "Appointment Batch Print"))
                            {
                                try
                                {
                                    oBatchPrintMenu.Click -= oBatchPrintMenuProgressControllerClick;


                                }
                                catch //(Exception ex)
                                {
                                }
                                try
                                {
                                    this.mnuFile.DropDownItems.Remove(oBatchPrintMenu);


                                }
                                catch //(Exception ex)
                                {
                                }
                                try
                                {
                                    oBatchPrintMenu.Dispose();
                                    oBatchPrintMenu = null;

                                }
                                catch //(Exception ex)
                                {
                                }
                                return;
                            }


                            if ((oBatchPrintMenu.Text == "Statement Batch Print"))
                            {
                                try
                                {
                                    oBatchPrintMenu.Click -= oBatchPrintPatientStatementControllerClick;


                                }
                                catch //(Exception ex)
                                {
                                }
                                try
                                {
                                    this.mnuFile.DropDownItems.Remove(oBatchPrintMenu);


                                }
                                catch //(Exception ex)
                                {
                                }
                                try
                                {
                                    oBatchPrintMenu.Dispose();
                                    oBatchPrintMenu = null;

                                }
                                catch //(Exception ex)
                                {
                                }
                                return;
                            }


                            if ((oBatchPrintMenu.Text == "Claim Template Print"))
                            {
                                try
                                {
                                    oBatchPrintMenu.Click -= oBatchPrintSetupDateActionTemplate;


                                }
                                catch //(Exception ex)
                                {
                                }
                                try
                                {
                                    this.mnuFile.DropDownItems.Remove(oBatchPrintMenu);


                                }
                                catch //(Exception ex)
                                {
                                }
                                try
                                {
                                    oBatchPrintMenu.Dispose();
                                    oBatchPrintMenu = null;

                                }
                                catch //(Exception ex)
                                {
                                }
                                return;
                            }
                        }
                    }
                }
            }

        }

        private void oBatchPrintMenuProgressControllerClick(object obj, EventArgs eventArgs)
        {
            try
            {
                int x = 0;
                int y = 0;
                Rectangle r = default(Rectangle);

                foreach (Form oFrm in System.Windows.Forms.Application.OpenForms)
                {
                    if (oFrm.Name == "frmgloPrintWordProgressController")
                    {
                        oFrm.WindowState = FormWindowState.Normal;
                        oFrm.TopMost = true;
                        r = Screen.PrimaryScreen.WorkingArea;
                        x = r.Width - oFrm.Width;
                        y = r.Height - oFrm.Height;
                        x = Convert.ToInt32(x / 2);
                        y = Convert.ToInt32(y / 2);
                        oFrm.Location = new Point(x, y);
                        //r = null;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

            }
            catch //(Exception ex)
            {
                //ex = null;

            }
        }



        private void oBatchPrintPatientStatementControllerClick(object obj, EventArgs eventArgs)
        {
            try
            {
                int x = 0;
                int y = 0;
                Rectangle r = default(Rectangle);

                foreach (Form oFrm in System.Windows.Forms.Application.OpenForms)
                {
                    if (oFrm.Name == "frmgloPrintBatchPatientStatementController")
                    {
                        oFrm.WindowState = FormWindowState.Normal;
                        oFrm.TopMost = true;
                        r = Screen.PrimaryScreen.WorkingArea;
                        x = r.Width - oFrm.Width;
                        y = r.Height - oFrm.Height;
                        x = Convert.ToInt32(x / 2);
                        y = Convert.ToInt32(y / 2);
                        oFrm.Location = new Point(x, y);
                        //r = null;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

            }
            catch //(Exception ex)
            {
                //ex = null;

            }
        }

        private void oBatchPrintSetupDateActionTemplate(object obj, EventArgs eventArgs)
        {
            try
            {
                int x = 0;
                int y = 0;
                Rectangle r = default(Rectangle);

                foreach (Form oFrm in System.Windows.Forms.Application.OpenForms)
                {
                    if (oFrm.Name == "frmgloPrintSetupDateActionTemplate")
                    {
                        oFrm.WindowState = FormWindowState.Normal;
                        oFrm.TopMost = true;
                        r = Screen.PrimaryScreen.WorkingArea;
                        x = r.Width - oFrm.Width;
                        y = r.Height - oFrm.Height;
                        x = Convert.ToInt32(x / 2);
                        y = Convert.ToInt32(y / 2);
                        oFrm.Location = new Point(x, y);
                        //r = null;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

            }
            catch //(Exception ex)
            {
                //ex = null;

            }
        }


        private void tsb_Billing_Click(object sender, EventArgs e)
        {
            
            try
            {


                if (gloCntrlPatient.PatientID != 0)
                {
                    if (base.SetChildFormModules("tsb_Billing_Click", "Create charges", _PatientProviderId.ToString()) != true)
                    {
                        frmBillingTransaction ofrmBillingTransaction = frmBillingTransaction.GetInstance(SyncPatient("Charges"));

                        //frmBillingTransaction ofrmBillingTransaction = frmBillingTransaction.GetInstance(gloCntrlPatient.PatientID);
                        ofrmBillingTransaction.MdiParent = this;
                        ofrmBillingTransaction.WindowState = FormWindowState.Maximized;
                        ofrmBillingTransaction.Show();
                        ShowHideMainMenu(false, false);

                        if (ofrmBillingTransaction.IsEMRTreatmentBind != true && ofrmBillingTransaction.IsOnlineChargeBind != true)
                        {
                            gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments frmPatientAppointment = null;
                            frmPatientAppointment = new gloPMGeneral.gloAppointmentsChargesLinking.frmPatientAppointments();
                            frmPatientAppointment._IsOnLoadform = true;
                            DataSet dtPatientAppointment = gloCharges.GetMissingChargeAppointments(ofrmBillingTransaction.PatientID, 0,0);
                            frmPatientAppointment.PatientAppointments = dtPatientAppointment;
                            frmPatientAppointment.nPatientID = ofrmBillingTransaction.PatientID;
                            if (dtPatientAppointment.Tables.Count>1)
                            {
                                if (Convert.ToInt32(dtPatientAppointment.Tables[1].Rows[0][0]) == 1)
                                {
                                    if (dtPatientAppointment.Tables[0].Rows.Count > 0)
                                    {

                                        frmPatientAppointment.ShowDialog(this);

                                        if (frmPatientAppointment.AppointmentID != 0)
                                        {
                                            ofrmBillingTransaction.GetPatientMissingCharges(frmPatientAppointment);
                                        }
                                    }
                                }
                            }
                            if (dtPatientAppointment != null)
                            {
                                dtPatientAppointment.Dispose();
                                dtPatientAppointment = null;
                            }
                        }

                       
                    }

                }
                //FillSelectedPatient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void tsb_BillingBatch_Click(object sender, EventArgs e)
        {

            try
            {
                if (gloCntrlPatient.PatientID != 0)
                {
                    pnlPatient_UpComingAppointments.DockState = Janus.Windows.UI.Dock.PanelDockState.Docked;
                    frmBillingBatch_New oViewBilling = frmBillingBatch_New.GetInstance();
                    oViewBilling.enableThread -= new frmBillingBatch_New.PrintCM(EnableThreadQueue);
                    oViewBilling.enableThread += new frmBillingBatch_New.PrintCM(EnableThreadQueue);
                    oViewBilling.MdiParent = this;
                    oViewBilling.WindowState = FormWindowState.Maximized;
                    oViewBilling.Show();                   
                    ShowHideMainMenu(false, false);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void tsb_Payment_Click(object sender, EventArgs e)
        {

            //gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
            //ogloBilling.ShowBillingPayment(gloCntrlPatient.PatientID, this);
            //ShowHideMainMenu(false, false);
        }

        private void tsb_PaymentPatient_Click(object sender, EventArgs e)
        {
            try
            {

                gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(SyncPatient(), false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                //  gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(gloCntrlPatient.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
                frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                frmPatientPaymentV2.ShowInTaskbar = false;
                frmPatientPaymentV2.ShowDialog(this);
                frmPatientPaymentV2.Dispose();
                ShowHideMainMenu(false, false);
                FillSelectedPatient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_PaymentInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                gloAccountsV2.frmInsurancePaymentV2 oPaymentInsurace = new gloAccountsV2.frmInsurancePaymentV2(gloPMGlobal.DatabaseConnectionString);
                oPaymentInsurace.StartPosition = FormStartPosition.CenterScreen;
                oPaymentInsurace.WindowState = FormWindowState.Maximized;
                oPaymentInsurace.ShowInTaskbar = false;
                oPaymentInsurace.ShowDialog(this);

                // SUDHIR 20100323 // TO REFRESH BATCH DETAILS AFTER INSURANCE PAYMENT DONE //
                //gloBilling.frmBillingBatch_New ofrm = gloBilling.frmBillingBatch_New.GetInstance();
                //if (ofrm != null)
                //    ofrm.RefreshBatchGrid();
                // END SUDHIR //

                oPaymentInsurace.Dispose();

                FillSelectedPatient();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsb_ERAPayment_Click(object sender, EventArgs e)
        {
            mnuGo_ERAPayment_Click(null, null);
        }

        private void tsb_PatBalance_Click(object sender, EventArgs e)
        {

            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
            ogloBilling.ShowPatientBalance(gloCntrlPatient.PatientID, this);
            ShowHideMainMenu(false, false);
            //SLR: FRee oglobilling
            if (ogloBilling != null)
            {
                ogloBilling.Dispose();
            }
        }

        private void tsb_PatLedger_Click(object sender, EventArgs e)
        {

            gloAccountsV2.frmPatientFinancialViewV2 oPatientFinancialView = gloAccountsV2.frmPatientFinancialViewV2.GetInstance(SyncPatient("Patient Account"));
            // gloAccountsV2.frmPatientFinancialViewV2 oPatientFinancialView = gloAccountsV2.frmPatientFinancialViewV2.GetInstance(gloCntrlPatient.PatientID);
            oPatientFinancialView.WindowState = FormWindowState.Maximized;
            oPatientFinancialView.MdiParent = this;
            oPatientFinancialView.Show();
            ShowHideMainMenu(false, false);
        }

        private void tsb_Remittance_Click(object sender, EventArgs e)
        {
            try
            {
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_ApptBook_Click(object sender, EventArgs e)
        {
            mnuEdit_AppointmentBook_Click(sender, e);
        }

        private void tsb_BillingBook_Click(object sender, EventArgs e)
        {
            mnuEdit_BillingBook_Click(sender, e);
        }

        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            //Saving the last selected Patient to file.
            if (PendingMessageQueue())
            {
                if (CheckBatchPrintProcessRunning() == false)
                {
                    gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                    oSettings.WriteSettings_XML(oSettings.ProfilePatientSettings, "PATIENTID", Convert.ToString(gloCntrlPatient.SelectedPatientID));

                    //SLR: FRee oSettings
                    oSettings.Dispose();

                    this.Close();
                }
            }
            // Application.Exit();
        }

        private void tsb_ShowDashboard_Click(object sender, EventArgs e)
        {
            FillPatientStatus();
            FillSelectedPatient();
            ShowHideMainMenu(true, true);
        }

        private void tsb_LockScreen_Click(object sender, EventArgs e)
        {
            LockScreen();
            gloCntrlPatient.FillPatients();
            UpdateStatusBar();
            //Sandip Darade 20091201
            //bug ID 0000195
            //Dashboard settings for login user 
            SetToolBar();

            Form[] frmChild = this.MdiChildren;

            if (frmChild.Length > 0)
            {
                foreach (Form frm in frmChild)
                {
                    if (frm.Name == "frmBillingTransaction")
                    {
                        frmBillingTransaction objfrmBillingTransaction = (frmBillingTransaction)frm;
                        objfrmBillingTransaction.SetDataUserWise();
                    }
                }
            }
        }

        private void tsb_Advance_Click(object sender, EventArgs e)
        {
            try
            {
                // gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(gloCntrlPatient.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Advance);
                gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(SyncPatient(), false, 0, 0, 0, 0, EOBPaymentSubType.Advance);
                frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                frmPatientPaymentV2.ShowInTaskbar = false;
                frmPatientPaymentV2.ShowDialog(this);
                frmPatientPaymentV2.Dispose();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void tsb_Calculator_Click(object sender, EventArgs e)
        {
            //Code Added by Mayuri:20091204
            //To fix issue:#286-Dashboard > allow to open calculator multile times 
            try
            {

                if ((_Iscalprocessstarted == true) || (calProcess == null))
                {
                    calProcess = System.Diagnostics.Process.Start("calc");
                    _Iscalprocessstarted = false;

                }
                else
                {
                    calProcess.Refresh();
                    if (calProcess.HasExited == true)
                    {
                        calProcess = System.Diagnostics.Process.Start("calc");
                        _Iscalprocessstarted = false;
                    }

                }



            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            //End code Added by Mayuri:20091204



        }

        private void tsb_ScanDocs_Click(object sender, EventArgs e)
        {
            ShowDMS();



        }
        #endregion




        #region "Menu Events"

        #region "File Menu"

        private void mnuFile_New_Click(object sender, EventArgs e)
        {

            ////Quick Patient Registration
            //try
            //{
            //    //test code
            //    gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            //    ogloPatient.ShowQuickPatientRegistration(out _CurrentPatientId);
            //    if (_CurrentPatientId > 0)
            //    {
            //        gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
            //        gloCntrlPatient.PatientID = _CurrentPatientId;
            //    }
            //    gloCntrlPatient.FillPatients();
            //    FillSelectedPatient();
            //    gloCntrlPatient.Refresh();
            //    gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
            //    ogloPatient.Dispose();
            //    gloCntrlPatient.Select();
            //    DataGridViewCellEventArgs ev = new DataGridViewCellEventArgs(0, 0);
            //    gloCntrlPatient_GridRowSelect_Click(sender, ev);
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void mnuFile_Modify_Click(object sender, EventArgs e)
        {

        }

        private void mnuFile_Delete_Click(object sender, EventArgs e)
        {

        }

        private void mnuFile_Refresh_Click(object sender, EventArgs e)
        {

        }

        private void mnuFile_Close_Click(object sender, EventArgs e)
        {

        }

        private void mnuFile_Lock_Click(object sender, EventArgs e)
        {
            LockScreen();
            //To set the patient data columns for login user 
            gloCntrlPatient.FillPatients();
            UpdateStatusBar();
        }

        private void mnuFile_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckBatchPrintProcessRunning() == false)
                {
                    tsb_Exit_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Edit Menu"

        private void mnuEdit_Guarantor_Click(object sender, EventArgs e)
        {

        }

        private void mnuEdit_Insurance_Click(object sender, EventArgs e)
        {

        }

        private void mnuEdit_Contacts_Click(object sender, EventArgs e)
        {
            try
            {

                gloContacts.gloContact ogloContacts = new gloContacts.gloContact(gloPMGlobal.DatabaseConnectionString);
                ogloContacts.ShowContactView(this);
                ogloContacts.Dispose();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnuEdit_AppointmentBook_Click(object sender, EventArgs e)
        {
            try
            {

                gloAppointmentBook.gloAppointmentBook oAppointmentBook = new gloAppointmentBook.gloAppointmentBook(gloPMGlobal.DatabaseConnectionString);
                oAppointmentBook.ShowAppointmentView(this);
                oAppointmentBook.Dispose();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuEdit_BillingBook_Click(object sender, EventArgs e)
        {
            try
            {
                string _EMRdatabaseconnectionstring = "";

                if (appSettings["EMRConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }
                gloBilling.gloBilling oBillingBook = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, _EMRdatabaseconnectionstring);
                oBillingBook.ShowBillingBookView(this);
                oBillingBook.Dispose();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuEdit_TaskMailBook_Click(object sender, EventArgs e)
        {
            try
            {

                gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(gloPMGlobal.DatabaseConnectionString);
                oTaskMail.ShowTaskMailBook(this);
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SLR: Finally free oTaskMail

            }
        }

        #endregion

        #region "Go Menu"

        private void mnuGo_NewPatient_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_PatientRegistration_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_ModifyPatient_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_PatientModification_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_CardScanning_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_PatientScan_Click(sender, e);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void mnuGo_Appointment_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_Appointment_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_Schedule_Click(object sender, EventArgs e)
        {
            try
            {
                gloAppointmentScheduling.gloSchedule oSchedule = new gloAppointmentScheduling.gloSchedule(gloPMGlobal.DatabaseConnectionString);
                oSchedule.ShowSchedule();
                oSchedule.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_Billing_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_Billing_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_BatchProcessing_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_BillingBatch_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_ClaimProcessing_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_Payment_Click(object sender, EventArgs e)
        {
            try
            {
                //gloBilling.frmPaymentInsurace oPaymentInsurace = new frmPaymentInsurace(gloCntrlPatient.PatientID, false, 0, 0, 0, 0);
                //oPaymentInsurace.WindowState = FormWindowState.Maximized;
                //oPaymentInsurace.ShowInTaskbar = false;
                //oPaymentInsurace.ShowDialog();
                //ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_PaymentPatient_Click(object sender, EventArgs e)
        {
            try
            {

                //gloBilling.frmPaymentPatient oPaymentInsurace = new frmPaymentPatient(gloCntrlPatient.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(SyncPatient(), false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                // gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(gloCntrlPatient.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
                frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                frmPatientPaymentV2.ShowInTaskbar = false;
                frmPatientPaymentV2.ShowDialog(this);
                frmPatientPaymentV2.Dispose();
                ShowHideMainMenu(false, false);
                FillSelectedPatient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_PaymentInsurace_Click(object sender, EventArgs e)
        {
            try
            {
                //gloBilling.Payment.frmInsurancePayment oPaymentInsurace = new gloBilling.Payment.frmInsurancePayment();
                gloAccountsV2.frmInsurancePaymentV2 oPaymentInsurace = new gloAccountsV2.frmInsurancePaymentV2(gloPMGlobal.DatabaseConnectionString);
                oPaymentInsurace.StartPosition = FormStartPosition.CenterScreen;
                oPaymentInsurace.WindowState = FormWindowState.Maximized;
                oPaymentInsurace.ShowInTaskbar = false;
                oPaymentInsurace.ShowDialog(this);
                oPaymentInsurace.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_ClosedJournals_Click(object sender, EventArgs e)
        {
            try
            {
                //ShowHideMainMenu(false, false);
                //frmBillingClosedJournals ofrmBillingClosedJournals = new frmBillingClosedJournals(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID, gloPMGlobal.UserName);
                //frmBillingPaymentTray ofrmBillingClosedJournals = frmBillingPaymentTray.GetInstance(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID, gloPMGlobal.UserName);
                //ofrmBillingClosedJournals.WindowState = FormWindowState.Maximized;
                //ofrmBillingClosedJournals.MdiParent = this;
                //ofrmBillingClosedJournals.Show();

                //MaheshB 20091109 As Per Told By PramodN
                //ShowHideMainMenu(false, false);
                //frmEOBPaymentTray ofrmEOBPaymentTray = frmEOBPaymentTray.GetInstance(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID, gloPMGlobal.UserName);
                //ofrmEOBPaymentTray.WindowState = FormWindowState.Maximized;
                //ofrmEOBPaymentTray.MdiParent = this;
                //ofrmEOBPaymentTray.Show();
                //ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuGo_ChargesTray_Click(object sender, EventArgs e)
        {
            try
            {
                //ShowHideMainMenu(false, false);
                //frmBillingChargesTray ofrmBillingChargesTray = frmBillingChargesTray.GetInstance(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID, gloPMGlobal.UserName);
                //ofrmBillingChargesTray.WindowState = FormWindowState.Maximized;
                //ofrmBillingChargesTray.MdiParent = this;
                //ofrmBillingChargesTray.Show();
                //ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnuGo_PatientStatementNotes_Click(object sender, EventArgs e)
        {
            ////gloBilling.frmPatientStatementNotes oPSN = new gloBilling.frmPatientStatementNotes(gloPMGlobal.DatabaseConnectionString, gloCntrlPatient.PatientID);
            ////try
            ////{
            ////    oPSN.ShowDialog();
            ////    oPSN.Dispose();
            ////    oPSN = null;
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}

        }

        private void mnuGo_ERAPayment_Click(object sender, EventArgs e)
        {
            try
            {
                gloBilling.gloERA.frmERAPayment ofrmERA = gloBilling.gloERA.frmERAPayment.GetInstance();
                ofrmERA.WindowState = FormWindowState.Maximized;
                ofrmERA.MdiParent = this;
                ofrmERA.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void mnuEdit_ICD9CPTGallery_Click(object sender, EventArgs e)
        {

        }

        private void mnuGo_CopayDistributionList_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    mnuCopayDist_ByAccount.Enabled = true;
            //    mnuCopayDist_ByCharge.Enabled = true;
            //    gloBilling.frmCopayDistributionList ofrmCopayDistributionList = gloBilling.frmCopayDistributionList.GetInstance();
            //    if (ofrmCopayDistributionList != null)
            //    {
            //        ofrmCopayDistributionList.WindowState = FormWindowState.Maximized;
            //        ofrmCopayDistributionList.MdiParent = this;
            //        ofrmCopayDistributionList.Show();
            //        ShowHideMainMenu(false, false);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void mnuGo_ReservesDistributionList_Click(object sender, EventArgs e)
        {
            try
            {
                gloBilling.frmReservesDistributionList ofrmCopayDistributionList = new frmReservesDistributionList();
                ofrmCopayDistributionList.WindowState = FormWindowState.Maximized;
                ofrmCopayDistributionList.MdiParent = this;
                ofrmCopayDistributionList.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "View Menu"

        private void mnuView_Insurance_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuView_Contacts_Click(object sender, EventArgs e)
        {

        }

        private void mnuView_Appointment_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_Calendar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuView_Schedule_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_Schedule_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuView_Reminders_Click(object sender, EventArgs e)
        {
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void mnuView_Billing_Click(object sender, EventArgs e)
        {
            try
            {
                if (gloCntrlPatient.PatientID != 0)
                {
                    pnlPatient_UpComingAppointments.DockState = Janus.Windows.UI.Dock.PanelDockState.Docked;
                    frmBillingBatch_New oViewBilling = frmBillingBatch_New.GetInstance();
                    oViewBilling.enableThread -= new frmBillingBatch_New.PrintCM(EnableThreadQueue);
                    oViewBilling.enableThread += new frmBillingBatch_New.PrintCM(EnableThreadQueue);
                    oViewBilling.MdiParent = this;
                    oViewBilling.WindowState = FormWindowState.Maximized;                 
                    oViewBilling.Show();
                    ShowHideMainMenu(false, false);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void mnuView_BatchProcessing_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuView_ClaimProcessing_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuView_Payment_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuView_TemplateGallary_Click(object sender, EventArgs e)
        {
            gloOffice.frmViewTemplateGallery frm = gloOffice.frmViewTemplateGallery.GetInstance(gloPMGlobal.DatabaseConnectionString);
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            //SLR: Since it is a getinstance, free exisitng event handler, before adding new event handler
            try
            {
                frm.CloseButton_Click -= new gloOffice.frmViewTemplateGallery.CloseButtonClick(frmViewTemplateGallery_Closeing);
            }
            catch
            {
            }
            frm.CloseButton_Click += new gloOffice.frmViewTemplateGallery.CloseButtonClick(frmViewTemplateGallery_Closeing);
            frm.Show();
            frm.ShowInTaskbar = false;
            frm.StartPosition = FormStartPosition.CenterParent;
            ShowHideMainMenu(false, false);
        }

        void frmViewTemplateGallery_Closeing(object sender, EventArgs e)
        {
            // Fill Template Menu displayed on Right Click
            // To reflect changes made from TemplateGallery

            Fill_Templates(cmnuPatientItem_Template);
        }

        private void mnuView_PatientBatchStatement_Click(object sender, EventArgs e)
        {
            //gloOffice.frmWd_BatchPatientStatement

            gloBilling.frmWd_BatchPatientStatement ofrmViewPatientStatement = gloBilling.frmWd_BatchPatientStatement.GetInstance(_CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
            //ShowHideMainMenu(false, false);
            ofrmViewPatientStatement.MdiParent = this;
            ofrmViewPatientStatement.ShowInTaskbar = false;
            ofrmViewPatientStatement.StartPosition = FormStartPosition.CenterParent;
            ofrmViewPatientStatement.WindowState = FormWindowState.Maximized;
            ofrmViewPatientStatement.Show();
            ShowHideMainMenu(false, false); //Mantis bugs no: 1258
        }

        #endregion

        #region "Reports Menu"

        private void mnuReports_ReportingTools_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    gloReportDesigner.frmPMSReportDesigner oRpt = new gloReportDesigner.frmPMSReportDesigner(Program.gSQLServerName, Program.gDatabase);

            //    oRpt.DBConnectionString = gloPMGlobal.DatabaseConnectionString;
            //    if (oRpt.GetDataModel().Trim() != "" && oRpt.IsBlankReportExist() == true)
            //    {
            //        pnl_ts_Main.Visible = false;
            //        ShowHideMainMenu(false, false);
            //        oRpt.MdiParent = this;
            //        oRpt.WindowState = FormWindowState.Maximized;
            //        oRpt.ShowInTaskbar = false;
            //        oRpt.StartPosition = FormStartPosition.CenterParent;
            //        oRpt.Show();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    pnl_ts_Main.Visible = true;
            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void mnuReports_Reports_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //ShowHideMainMenu(false, false);
            //    gloReportDesigner.frmExistingReport oExistingRpts = new gloReportDesigner.frmExistingReport();
            //    if (oExistingRpts.GetExistingReportsCount() > 0)
            //    {
            //        oExistingRpts.DBConnectionString = gloPMGlobal.DatabaseConnectionString;
            //        //  oRpt.WindowState = FormWindowState.Maximized;
            //        //  oRpt.MdiParent = this;
            //        oExistingRpts.StartPosition = FormStartPosition.CenterScreen;
            //        oExistingRpts.ShowDialog();
            //    }

            //    //
            //    if (oExistingRpts.ReportName != "")
            //    {
            //        gloReportDesigner.frmPMSReportDesigner oRpt = new gloReportDesigner.frmPMSReportDesigner(Program.gSQLServerName, Program.gDatabase);

            //        oRpt.DBConnectionString = gloPMGlobal.DatabaseConnectionString;
            //        ShowHideMainMenu(false, false);
            //        oRpt.MdiParent = this;
            //        oRpt.WindowState = FormWindowState.Maximized;
            //        oRpt.ShowInTaskbar = false;
            //        oRpt.StartPosition = FormStartPosition.CenterParent;
            //        oRpt.Show();
            //    }

            //    oExistingRpts = null;


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void mnuReports_PracticeAnalysis_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_OvreduePatientPayment_Click(object sender, EventArgs e)
        {
            try
            {
                //frmRpt_Overdue ofrmRpt_Overdue = new frmRpt_Overdue(gloPMGlobal.DatabaseConnectionString);
                //ofrmRpt_Overdue.MdiParent = this;
                //ofrmRpt_Overdue.WindowState = FormWindowState.Maximized;
                //ofrmRpt_Overdue.ShowInTaskbar = false;
                //ofrmRpt_Overdue.StartPosition = FormStartPosition.CenterParent;
                //ofrmRpt_Overdue.Show();
                //ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_OverdueInsurancePayment_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_PatientVsEstablishedReport_Click(object sender, EventArgs e)
        {
            try
            {
                //Commented By Pramod Nair For Implementing Crystal Reports
                //frmRpt_NewPatientvsEstablishedPatient oNewVsEstRPT = new frmRpt_NewPatientvsEstablishedPatient(gloPMGlobal.DatabaseConnectionString);
                //ShowHideMainMenu(false, false);
                //oNewVsEstRPT.MdiParent = this;
                //oNewVsEstRPT.WindowState = FormWindowState.Maximized;
                //oNewVsEstRPT.ShowInTaskbar = false;
                //oNewVsEstRPT.StartPosition = FormStartPosition.CenterParent;
                //oNewVsEstRPT.Show();

                //Added By Pramod Nair For Implementing Crystal Reports
                gloReports.C1Reports.frmRpt_NewPatvsEstPat oNewPatvsEstPat = new gloReports.C1Reports.frmRpt_NewPatvsEstPat(gloPMGlobal.DatabaseConnectionString);
                oNewPatvsEstPat.MdiParent = this;
                oNewPatvsEstPat.WindowState = FormWindowState.Maximized;
                oNewPatvsEstPat.ShowInTaskbar = false;
                oNewPatvsEstPat.StartPosition = FormStartPosition.CenterParent;
                oNewPatvsEstPat.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnu_Reports_PatientByDOBReport_Click(object sender, EventArgs e)
        {
            try
            {

                //Added By Pramod Nair For Implementing Crystal Reports
                gloReports.C1Reports.frmRpt_PatientByDOB oPatientByDOB = new gloReports.C1Reports.frmRpt_PatientByDOB(gloPMGlobal.DatabaseConnectionString);
                oPatientByDOB.MdiParent = this;
                oPatientByDOB.WindowState = FormWindowState.Maximized;
                oPatientByDOB.ShowInTaskbar = false;
                oPatientByDOB.StartPosition = FormStartPosition.CenterParent;
                oPatientByDOB.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnu_Reports_DuplicateCliam_Click(object sender, EventArgs e)
        {
            try
            {

                //Added By Mukesh Patel For Duplicate Cliam Report
                gloBilling.frmRpt_DuplicateClaimReport oDuplicateCliam = new gloBilling.frmRpt_DuplicateClaimReport(gloPMGlobal.DatabaseConnectionString);
                oDuplicateCliam.MdiParent = this;
                oDuplicateCliam.WindowState = FormWindowState.Maximized;
                oDuplicateCliam.ShowInTaskbar = false;
                oDuplicateCliam.StartPosition = FormStartPosition.CenterParent;
                oDuplicateCliam.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_PrintLabels_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_PrintLabels_Patient_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_PrintLabels_Insurance_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_PrintLabels_Contacts_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_PrintLabels_Employies_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_PrintList_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_Graphs_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_ProviderReferral_Patients_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReferralProviderPatients oRPPatientsRPT = new frmRpt_ReferralProviderPatients(gloPMGlobal.DatabaseConnectionString);
                oRPPatientsRPT.MdiParent = this;
                oRPPatientsRPT.WindowState = FormWindowState.Maximized;
                oRPPatientsRPT.ShowInTaskbar = false;
                oRPPatientsRPT.StartPosition = FormStartPosition.CenterParent;
                oRPPatientsRPT.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        #endregion

        #region "Tools Menu"

        private void mnuTools_ClinicSetup_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuTools_Provider_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuTools_Employies_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuTools_Import_Click(object sender, EventArgs e)
        {
            try
            {
                //gloOffice.frmWd_ImportTemplates frm = new gloOffice.frmWd_ImportTemplates(gloPMGlobal.DatabaseConnectionString);
                gloOffice.frmWd_ImportTemplates frm = gloOffice.frmWd_ImportTemplates.GetInstance(gloPMGlobal.DatabaseConnectionString);
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                //SLR: since it is a getinstance, free the eventhandler, before adding new..
                try
                {
                    frm.CloseButton_Click -= new gloOffice.frmWd_ImportTemplates.CloseButtonClick(frmWd_ImportTemplates_Closed);
                }
                catch
                {
                }
                frm.CloseButton_Click += new gloOffice.frmWd_ImportTemplates.CloseButtonClick(frmWd_ImportTemplates_Closed);
                frm.Show();
                frm.ShowInTaskbar = false;
                frm.StartPosition = FormStartPosition.CenterParent;
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void frmWd_ImportTemplates_Closed(object sender, EventArgs e)
        {

            // Fill Template Menu displayed on Right Click 
            // To show Templates which are imported
            Fill_Templates(cmnuPatientItem_Template);
        }

        private void mnuTools_Export_Click(object sender, EventArgs e)
        {
            try
            {
                //gloOffice.frmWd_ExportTemplates frm = new gloOffice.frmWd_ExportTemplates(gloPMGlobal.DatabaseConnectionString);
                gloOffice.frmWd_ExportTemplates frm = gloOffice.frmWd_ExportTemplates.GetInstance(gloPMGlobal.DatabaseConnectionString);
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                frm.ShowInTaskbar = false;
                frm.StartPosition = FormStartPosition.CenterParent;
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuTools_UpdateTemplates_Click(object sender, EventArgs e)
        {
            try
            {
                gloOffice.frmUpdateTemplates oFrm = new gloOffice.frmUpdateTemplates(gloPMGlobal.DatabaseConnectionString);
                oFrm.ShowDialog(this);
                oFrm.Dispose();
                oFrm = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuTools_MergePatient_Click(object sender, EventArgs e)
        {
            try
            {
                gloPatient.frmMergePatients ofrm = new gloPatient.frmMergePatients(gloPMGlobal.DatabaseConnectionString);
                ofrm.ShowDialog(this);
                ofrm.Dispose();
                gloCntrlPatient.FillPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuTools_Synchronize_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSetting_Theme2003_Click(object sender, EventArgs e)
        {
            uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            //  pnlPatient_Status.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient_UpComingAppointments.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient_Details.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient_Demo.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient.BackgroundImage = global ::gloPM.Properties.Resources.Img_2003Header;
            pnlPatient.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void mnuSetting_Theme2003Dark_Click(object sender, EventArgs e)
        {
            uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            //pnlPatient_Status.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            pnlPatient_UpComingAppointments.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            pnlPatient_Details.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            pnlPatient_Demo.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            pnlPatient.BackgroundImage = global ::gloPM.Properties.Resources.Img_2003DarkHeader;
            pnlPatient.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void mnuSetting_Theme2007_Click(object sender, EventArgs e)
        {
            uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            //pnlPatient_Status.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient_UpComingAppointments.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient_Details.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient_Demo.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.UseDefault;
            pnlPatient.BackgroundImage = global ::gloPM.Properties.Resources.Img_2007Header;
            pnlPatient.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void blockUnblockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gloSettings.gloBlockUnblock ogloBlockUnblock = new gloSettings.gloBlockUnblock(gloPMGlobal.DatabaseConnectionString);
            try
            {
                ShowHideMainMenu(false, false);
                ogloBlockUnblock.ShowViewBlockedItems(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ogloBlockUnblock.Dispose();
            }

        }

        private void mnuTools_MergeScheduledActions_Click(object sender, EventArgs e)
        {
            gloBilling.Collections.frmMergeScheduledActions ofrmScheduledAction = new gloBilling.Collections.frmMergeScheduledActions(gloPMGlobal.DatabaseConnectionString);
            ofrmScheduledAction.ShowDialog(this);
            ofrmScheduledAction.Dispose();
            ofrmScheduledAction = null;
        }
        #endregion

        #region "Security Menu"

        private void mnuSecurity_UserManagement_Click(object sender, EventArgs e)
        {
            try
            {
                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                oSecurity.ShowUserView(this);
                //SLR: Dispose oSecurity
                oSecurity.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSecurity_SystemManagemnet_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSecurity_PasswordPolicy_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSecurity_PatientLog_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSecurity_Forms_Click(object sender, EventArgs e)
        {
            try
            {
                //Temp code Added by Saket for Setup reminder               
                //gloReminder.frmSetupReminder ofrm = new gloReminder.frmSetupReminder(gloPMGlobal.DatabaseConnectionString);
                //ofrm.ShowDialog();
                //ofrm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Setting Menu"

        private void mnuSetting_DefaultPatientSetting_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSetting_SystemSetting_Click(object sender, EventArgs e)
        {
            try
            {
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                oSettings.LockScreenTime = Program.gLockScreenTime.ToString();
                oSettings.DBConnectionString = gloPMGlobal.DatabaseConnectionString;
                oSettings.ShowGeneralSettings(this);
                Program.gLockScreenTime = Convert.ToInt32(oSettings.LockScreenTime);
                if (Convert.ToInt32(oSettings.LockScreenTime) <= 4)
                {
                    timerLockScreen.Interval = Convert.ToInt32(oSettings.LockScreenTime) * 60000;
                }
                else
                {
                    timerLockScreen.Interval = ((Convert.ToInt32(oSettings.LockScreenTime) / 2) - 1) * 60000;
                }
                GetPatientDemographicsSettings();
                gloCntrlPatient.FillPatients();



                AddStyle();
                oSettings.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSetting_Appointment_Click(object sender, EventArgs e)
        {

        }

        private void mnuSetting_Schedule_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSetting_Billing_Click(object sender, EventArgs e)
        {
            try
            {
                //gloBilling.frmBillingCaseSettings ofrmBillingCaseSettings = new gloBilling.frmBillingCaseSettings();
                //ofrmBillingCaseSettings.StartPosition = FormStartPosition.CenterScreen;
                //ofrmBillingCaseSettings.ShowInTaskbar = false;
                //ofrmBillingCaseSettings.ShowDialog();

                //gloBilling.frmBillingSettings ofrmSetting = new gloBilling.frmBillingSettings();
                //ofrmSetting.StartPosition = FormStartPosition.CenterScreen;
                //ofrmSetting.ShowInTaskbar = false;
                //ofrmSetting.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSetting_CardScanner_Click(object sender, EventArgs e)
        {

            try
            {
                gloCardScanning.frmSetupScannerSettings ofrmScanSettings = new gloCardScanning.frmSetupScannerSettings(gloPMGlobal.DatabaseConnectionString);
                ofrmScanSettings.ShowDialog(this);
                ofrmScanSettings.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSetting_Printer_Click(object sender, EventArgs e)
        {
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void mnuSetting_DefaultDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDisplaySettings(true);
                SaveDisplaySettings(false);
                GetMedicalCategoryImage();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion

        #region "Help Menu"

        private void mnuHelp_HowDoI_Click(object sender, EventArgs e)
        {
            try
            {
                string helpFileName = System.IO.Path.Combine(Application.StartupPath, "help\\gloPM_User_Manual.chm");
                if (System.IO.File.Exists(helpFileName))
                {
                    Help.ShowHelp(this, "file://" + helpFileName, "Welcome_to_gloPM_Help_Files.htm");
                    Help.ShowHelp(this, "file://" + helpFileName, HelpNavigator.TableOfContents, "Welcome_to_gloPM_Help_Files.htm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuHelp_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string helpFileName = System.IO.Path.Combine(Application.StartupPath, "help\\gloPM_User_Manual.chm");
                if (System.IO.File.Exists(helpFileName))
                {
                    Help.ShowHelp(this, "file://" + helpFileName, "Welcome_to_gloPM_Help_Files.htm");
                    Help.ShowHelp(this, "file://" + helpFileName, HelpNavigator.Find, "Welcome_to_gloPM_Help_Files.htm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuHelp_Contents_Click(object sender, EventArgs e)
        {
            try
            {
                string helpFileName = System.IO.Path.Combine(Application.StartupPath, "help\\gloPM_User_Manual.chm");
                if (System.IO.File.Exists(helpFileName))
                {
                    Help.ShowHelp(this, "file://" + helpFileName, "Welcome_to_gloPM_Help_Files.htm");
                    Help.ShowHelp(this, "file://" + helpFileName, HelpNavigator.TableOfContents, "Welcome_to_gloPM_Help_Files.htm");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuHelp_TechnicalSupport_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuHelp_Version_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuHelp_License_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuHelp_AboutgloPMS_Click(object sender, EventArgs e)
        {
            try
            {


                // Form frm = new gloTransparentScreen.gloAboutBox(gloTransparentScreen.Properties.Resources.gloPMLOGO);
                frmgloAboutUs frm = new frmgloAboutUs();
                frm.ShowInTaskbar = false;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog(this);
                frm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuSupport_Click(object sender, EventArgs e)
        {
            try
            {
                gloGlobal.Support.frmSupport frm = new gloGlobal.Support.frmSupport(gloGlobal.gloPMGlobal.AusID, gloGlobal.gloPMGlobal.ApplicationVersion, Program.gDatabase, gloGlobal.gloPMGlobal.UserName);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region "Patient Details Toolbar Events"

        private void ts_PatientDetail_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem != null && Convert.ToString(e.ClickedItem).Contains("Appointments"))
            {
                lblStatus.Text = "Status :";
                label62.Text = "Provider :";
                FillAppointmentStatusCombo();
                FillProviderCombo();
            }
            if (e.ClickedItem != null && Convert.ToString(e.ClickedItem).Contains("View Documents"))
            {
                lblStatus.Text = "Year ";
                label62.Text = "Month ";
                cmbStatus.DataSource = null;
                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("All");
                cmbStatus.SelectedIndex = 0;
                FillMonthListInProviderCombo();
                cmbProvider.SelectedIndex = 0;
            }
        }

        private void tsb_NYWCForms_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PatientNYWorkersCompForms;
                showPatientDetails();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsb_PD_Insurance_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PatientInsurance;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsb_PD_Appointments_Click(object sender, EventArgs e)
        {
            try
            {
                _SelcetedPatient = PatientDetails.PatientAppointments;
                _AppClick = true;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;
                _SearchFlag = false;
                _AppClick = false;
                pnlSearchFilter.Visible = true;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {

            }
        }

        private void tsb_PD_Referral_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PatientReferrals;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsb_PD_Procedure_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PatientProcedures;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsb_PD_Billing_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PatientBilling;
                showPatientDetails();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsb_PD_Cases_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PatientCases;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsb_PatientTasks_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PatientTasks;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void showPatientDetails()
        {
            this.tsb_PD_Insurance.BackgroundImage = null;
            this.tsb_PD_Procedure.BackgroundImage = null;
            this.tsb_PD_Billing.BackgroundImage = null;
            this.tsb_PD_Appointments.BackgroundImage = null;
            this.tsb_PD_Referral.BackgroundImage = null;
            this.tsb_PD_Cases.BackgroundImage = null;
            this.tsb_PD_PriorAuthorization.BackgroundImage = null;
            this.tsb_PatientTasks.BackgroundImage = null;
            this.tsb_PDViewDocument.BackgroundImage = null;
            this.tsb_NYWCForms.BackgroundImage = null;
            try
            {

                // Detach the Data From c1PatientDetails to Bind new Data with it
                c1PatientDetails.DataSource = null;
                //Code added on 31st March 2008 by Sagar Ghodke
                c1PatientDetails.AllowEditing = false;
                c1PatientDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1PatientDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
                c1PatientDetails.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;
                this.pnlSearchFilter.Visible = false;
                switch (_SelcetedPatient)
                {
                    case PatientDetails.PatientInsurance:
                        {
                            this.tsb_PD_Insurance.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPatientInsurance(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientAppointments:
                        {
                            this.tsb_PD_Appointments.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPatientAppointments(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientBilling:
                        {
                            this.tsb_PD_Billing.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPatientBilling(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientReferrals:
                        {
                            this.tsb_PD_Referral.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPatientReferrals(_CurrentPatientId);
                        }
                        break;

                    case PatientDetails.PriorAuthorization:
                        {
                            this.tsb_PD_PriorAuthorization.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPriorAuthorization(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientCases:
                        {
                            this.tsb_PD_Cases.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillCases(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientTasks:
                        {
                            this.tsb_PatientTasks.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPatientTasks(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientDocuments:
                        {
                            this.tsb_PDViewDocument.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPatientDocuments(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientNYWorkersCompForms:
                        {
                            this.tsb_NYWCForms.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
                            FillPatientWorkerCompsForms(_CurrentPatientId);
                        }
                        break;
                } // Switch
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {

            }
        }

        private void FillPatientWorkerCompsForms(long _CurrentPatientId)
        {
            c1PatientDetails.DataSource = null;

            c1PatientDetails.Clear();
            ////Set the column Names.                              
            //c1PatientDetails.Cols.Count = 17;
            c1PatientDetails.Cols.Count = 18;
            c1PatientDetails.Rows.Count = 1;

            c1PatientDetails.SetData(0, 0, "Form ID");
            c1PatientDetails.SetData(0, 1, "Date Of Injury");
            c1PatientDetails.SetData(0, 2, "Form Type");
            c1PatientDetails.SetData(0, 3, "Patient ID");
            c1PatientDetails.SetData(0, 4, "Claim #");
            c1PatientDetails.SetData(0, 5, "DOS");
            c1PatientDetails.SetData(0, 6, "Close Date");
            c1PatientDetails.SetData(0, 7, "Created By");
            c1PatientDetails.SetData(0, 8, "Created On");
            c1PatientDetails.SetData(0, 9, "Modified By");
            c1PatientDetails.SetData(0, 10, "Modified On");
            c1PatientDetails.SetData(0, 11, "Exam ID");
            c1PatientDetails.SetData(0, 12, "ClaimTransactionMasterID");
            c1PatientDetails.SetData(0, 13, "ClaimTransactionID");
            c1PatientDetails.SetData(0, 14, "Last Modified By ID");
            c1PatientDetails.SetData(0, 15, "Form Type ID");

            c1PatientDetails.SetData(0, 16, "SortClaim");
            c1PatientDetails.SetData(0, 17, "SortSubClaim");

            c1PatientDetails.Cols[0].Visible = false;
            c1PatientDetails.Cols[1].Visible = true;
            c1PatientDetails.Cols[2].Visible = true;
            c1PatientDetails.Cols[3].Visible = false;
            c1PatientDetails.Cols[4].Visible = true;
            c1PatientDetails.Cols[5].Visible = true;
            c1PatientDetails.Cols[6].Visible = false;
            c1PatientDetails.Cols[7].Visible = true;
            c1PatientDetails.Cols[8].Visible = true;
            c1PatientDetails.Cols[9].Visible = true;
            c1PatientDetails.Cols[10].Visible = true;
            c1PatientDetails.Cols[11].Visible = false;
            c1PatientDetails.Cols[12].Visible = false;
            c1PatientDetails.Cols[13].Visible = false;
            c1PatientDetails.Cols[14].Visible = false;
            c1PatientDetails.Cols[15].Visible = false;
            c1PatientDetails.Cols[16].Visible = false;
            c1PatientDetails.Cols[17].Visible = false;

            for (int i = 0; i <= 17; i++)
            {
                c1PatientDetails.Cols[i].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[i].AllowEditing = false;
            }

            c1PatientDetails.ExtendLastCol = true;
            //c1PatientDetails.Cols[6].DataType = typeof(System.DateTime);
            //c1PatientDetails.Cols[6].Format = "MM/dd/yyyy";



            bool _designWidth = false;
            //try
            //{
            //    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
            //    _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPatientBilling, gloPMGlobal.UserID);
            //    oSetting.Dispose();
            //}
            //catch (Exception) // ex)
            //{

            //}
            if (_designWidth == false)
            {
                //int _width = (c1PatientDetails.Width - 60) / 32;

                c1PatientDetails.Cols[0].Width = 100;
                c1PatientDetails.Cols[1].Width = 100;
                c1PatientDetails.Cols[2].Width = 80;
                c1PatientDetails.Cols[3].Width = 90;
                c1PatientDetails.Cols[4].Width = 80;
                c1PatientDetails.Cols[5].Width = 100;
                c1PatientDetails.Cols[6].Width = 150;
                c1PatientDetails.Cols[7].Width = 100;
                c1PatientDetails.Cols[8].Width = 100;
                c1PatientDetails.Cols[9].Width = 100;
                c1PatientDetails.Cols[10].Width = 100;
                c1PatientDetails.Cols[11].Width = 150;
                c1PatientDetails.Cols[12].Width = 150;
                c1PatientDetails.Cols[13].Width = 150;
                c1PatientDetails.Cols[14].Width = 150;
                c1PatientDetails.Cols[15].Width = 10;

                c1PatientDetails.Cols[16].Width = 0;
                c1PatientDetails.Cols[17].Width = 0;
            }

            // Retrieve Patient NY Forms list from the database for selected patient in Dashboard.
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParams = null;
            DataTable dtTransaction = null;
            try
            {
                oDB.Connect(false);
                oDBParams = new gloDatabaseLayer.DBParameters();
                oDBParams.Add("@PatientID", _CurrentPatientId, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("gsp_GetDataForWorkersCompForms_Dashboard",
                    oDBParams,
                    out dtTransaction);
                oDBParams.Clear();
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
                if (oDBParams != null) { oDBParams.Clear(); oDBParams = null; }
            }

            // Set data to the Patient NY Forms list to patient details panel on Dashboard
            Int32 RowIndex = 0;
            if (dtTransaction != null)
            {
                gloGlobal.gloPMGlobal.SplitClaimColumn(dtTransaction, dtTransaction.Columns.IndexOf("ClaimNo"));

                for (int i = 0; i < dtTransaction.Rows.Count; i++)
                {
                    c1PatientDetails.Rows.Add();
                    RowIndex = c1PatientDetails.Rows.Count - 1;

                    c1PatientDetails.SetData(RowIndex, 0, dtTransaction.Rows[i]["FormId"].ToString());
                    c1PatientDetails.SetData(RowIndex, 1, dtTransaction.Rows[i]["nInjuryDate"].ToString());
                    c1PatientDetails.SetData(RowIndex, 2, dtTransaction.Rows[i]["NYFormTypeDisplay"].ToString());
                    c1PatientDetails.SetData(RowIndex, 3, dtTransaction.Rows[i]["PatientId"].ToString());
                    c1PatientDetails.SetData(RowIndex, 4, dtTransaction.Rows[i]["ClaimNo"].ToString());
                    c1PatientDetails.SetData(RowIndex, 5, dtTransaction.Rows[i]["DOS"].ToString());
                    c1PatientDetails.SetData(RowIndex, 6, dtTransaction.Rows[i]["CloseDate"]);
                    c1PatientDetails.SetData(RowIndex, 7, dtTransaction.Rows[i]["CreatedBy"]);
                    c1PatientDetails.SetData(RowIndex, 8, dtTransaction.Rows[i]["CreatedOn"]);
                    c1PatientDetails.SetData(RowIndex, 9, dtTransaction.Rows[i]["LastModifiedByName"]);
                    c1PatientDetails.SetData(RowIndex, 10, dtTransaction.Rows[i]["LastModifiedDate"]);
                    c1PatientDetails.SetData(RowIndex, 11, dtTransaction.Rows[i]["ExamId"]);
                    c1PatientDetails.SetData(RowIndex, 12, dtTransaction.Rows[i]["ClaimTransactionMasterID"]);
                    c1PatientDetails.SetData(RowIndex, 13, dtTransaction.Rows[i]["ClaimTransactionID"]);
                    c1PatientDetails.SetData(RowIndex, 14, dtTransaction.Rows[i]["LastModifiedBy"]);
                    c1PatientDetails.SetData(RowIndex, 15, dtTransaction.Rows[i]["NYFormTypeID"]);

                    c1PatientDetails.SetData(RowIndex, 16, dtTransaction.Rows[i]["SortClaim"]);
                    c1PatientDetails.SetData(RowIndex, 17, dtTransaction.Rows[i]["SortSubClaim"]);
                }
            }

            if (dtTransaction != null)
            {
                dtTransaction.Dispose(); dtTransaction = null;
            }
        }

        private void FillPatientReferrals(long _CurrentPatientId)
        {
            DataTable dt_Reffrals;

            //Get the Patient Reffrals.
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            dt_Reffrals = ogloPatient.getPatientReffrals(_CurrentPatientId);
            c1PatientDetails.DataSource = null;

            c1PatientDetails.Clear();
            //Set the column Names.                              
            c1PatientDetails.Cols.Count = 10;
            c1PatientDetails.Rows.Count = 1;


            c1PatientDetails.SetData(0, 0, "Referral Name");
            c1PatientDetails.SetData(0, 1, "Address");
            c1PatientDetails.SetData(0, 2, "City");
            c1PatientDetails.SetData(0, 3, "State");
            c1PatientDetails.SetData(0, 4, "Zip");
            c1PatientDetails.SetData(0, 5, "Phone");
            c1PatientDetails.SetData(0, 6, "Fax");
            c1PatientDetails.SetData(0, 7, "Email");
            c1PatientDetails.SetData(0, 8, "Patient Detail ID");
            c1PatientDetails.SetData(0, 9, "Contact ID");


            if (dt_Reffrals != null)
            {
                //For each row in DataTable add the row to datagrid. 

                for (int i = 0; i < dt_Reffrals.Rows.Count; i++)
                {

                    c1PatientDetails.Rows.Add();

                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 0, "");

                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 0, dt_Reffrals.Rows[i]["sFirstName"] + " " + dt_Reffrals.Rows[i]["sMiddleName"] + " " + dt_Reffrals.Rows[i]["sLastName"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 1, dt_Reffrals.Rows[i]["sAddressLine1"] + " " + dt_Reffrals.Rows[i]["sAddressLine2"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 2, dt_Reffrals.Rows[i]["sCity"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 3, dt_Reffrals.Rows[i]["sState"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 4, dt_Reffrals.Rows[i]["sZip"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 5, dt_Reffrals.Rows[i]["sPhone"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 6, dt_Reffrals.Rows[i]["sFax"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 7, dt_Reffrals.Rows[i]["sEmail"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 8, dt_Reffrals.Rows[i]["nPatientDetailID"]);
                    c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 9, dt_Reffrals.Rows[i]["nContactId"]);


                }
            }


            c1PatientDetails.Cols[0].AllowEditing = false;
            c1PatientDetails.Cols[1].AllowEditing = false;
            c1PatientDetails.Cols[2].AllowEditing = false;
            c1PatientDetails.Cols[3].AllowEditing = false;
            c1PatientDetails.Cols[4].AllowEditing = false;
            c1PatientDetails.Cols[5].AllowEditing = false;
            c1PatientDetails.Cols[6].AllowEditing = false;
            c1PatientDetails.Cols[7].AllowEditing = false;
            c1PatientDetails.Cols[8].AllowEditing = false;
            c1PatientDetails.Cols[9].AllowEditing = false;

            c1PatientDetails.Cols[8].Visible = false;
            c1PatientDetails.Cols[9].Visible = false;


            c1PatientDetails.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[6].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[7].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            bool _designWidth = false;
            try
            {
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPatientReferrals, gloPMGlobal.UserID);
                oSetting.Dispose();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            if (_designWidth == false)
            {
                int _width = (c1PatientDetails.Width - 15) / 10;

                c1PatientDetails.Cols[0].Width = _width * 2;
                c1PatientDetails.Cols[1].Width = _width * 2;
                c1PatientDetails.Cols[2].Width = _width * 1;
                c1PatientDetails.Cols[3].Width = _width * 1;
                c1PatientDetails.Cols[4].Width = _width * 1;
                c1PatientDetails.Cols[5].Width = _width * 1;
                c1PatientDetails.Cols[6].Width = _width * 1;
                c1PatientDetails.Cols[7].Width = _width * 1;
                c1PatientDetails.Cols[8].Visible = false;
                c1PatientDetails.Cols[9].Visible = false;

            }
            //SLR: Free ogloPatient, dtRefereals
            if (ogloPatient != null)
            {
                ogloPatient.Dispose();
            }
            if (dt_Reffrals != null)
            {
                dt_Reffrals.Dispose();
            }
        }
        private void FillPriorAuthorization(Int64 PatientID)
        {
            c1PatientDetails.DataSource = null;
            c1PatientDetails.Clear();
            ////Set the column Names.                              
            c1PatientDetails.Cols.Count = 13;
            c1PatientDetails.Rows.Count = 1;
            c1PatientDetails.SetData(0, 0, "PriorAuthorizationID");
            c1PatientDetails.SetData(0, 1, "Auth #");
            c1PatientDetails.SetData(0, 2, "Referring");
            c1PatientDetails.SetData(0, 3, "Insurance");
            c1PatientDetails.SetData(0, 4, "ReferralID");
            c1PatientDetails.SetData(0, 5, "IsTrackAuthLimit");
            c1PatientDetails.SetData(0, 6, "Note");
            c1PatientDetails.SetData(0, 7, "Start");
            c1PatientDetails.SetData(0, 8, "Expiration");
            c1PatientDetails.SetData(0, 9, "#Visits Allowed");
            c1PatientDetails.SetData(0, 10, "PatientName");
            c1PatientDetails.SetData(0, 11, "Visits Remain.");
            c1PatientDetails.SetData(0, 12, "IsInActive");


            c1PatientDetails.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[6].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[7].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[8].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[9].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[10].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[11].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[12].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            c1PatientDetails.Cols[0].AllowEditing = false;
            c1PatientDetails.Cols[1].AllowEditing = false;
            c1PatientDetails.Cols[2].AllowEditing = false;
            c1PatientDetails.Cols[3].AllowEditing = false;
            c1PatientDetails.Cols[4].AllowEditing = false;
            c1PatientDetails.Cols[5].AllowEditing = false;
            c1PatientDetails.Cols[6].AllowEditing = false;
            c1PatientDetails.Cols[7].AllowEditing = false;
            c1PatientDetails.Cols[8].AllowEditing = false;
            c1PatientDetails.Cols[9].AllowEditing = false;
            c1PatientDetails.Cols[10].AllowEditing = false;
            c1PatientDetails.Cols[11].AllowEditing = false;
            c1PatientDetails.Cols[12].AllowEditing = false;

            c1PatientDetails.Cols[0].Visible = false;
            c1PatientDetails.Cols[4].Visible = false;
            c1PatientDetails.Cols[5].Visible = false;
            c1PatientDetails.Cols[9].Visible = false;
            c1PatientDetails.Cols[10].Visible = false;
            c1PatientDetails.Cols[12].Visible = false;

            c1PatientDetails.Cols[7].DataType = typeof(System.DateTime);
            c1PatientDetails.Cols[8].DataType = typeof(System.DateTime);
            c1PatientDetails.Cols[7].Format = "MM/dd/yyyy";
            c1PatientDetails.Cols[8].Format = "MM/dd/yyyy";

            bool _designWidth = false;
            try
            {
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPMPriorAuthorization, gloPMGlobal.UserID);
                oSetting.Dispose();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            if (_designWidth == false)
            {
                int _width = (c1PatientDetails.Width - 60 / 8);

                c1PatientDetails.Cols[1].Width = 100;
                c1PatientDetails.Cols[2].Width = 125;
                c1PatientDetails.Cols[3].Width = 125;
                c1PatientDetails.Cols[4].Width = 0;
                c1PatientDetails.Cols[5].Width = 0;
                c1PatientDetails.Cols[6].Width = 225;
                c1PatientDetails.Cols[7].Width = 75;
                c1PatientDetails.Cols[8].Width = 75;
                c1PatientDetails.Cols[9].Width = 0;
                c1PatientDetails.Cols[10].Width = 110;
                c1PatientDetails.Cols[11].Width = 100;
                c1PatientDetails.Cols[12].Width = 125;
            }

            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            try
            {
                DataTable dtTransaction = null;
                dtTransaction = ogloPatient.GetPatientPriorAuthorization(PatientID, gloPMGlobal.ClinicID, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString()));

                if (dtTransaction != null)
                {
                    for (int i = 0; i < dtTransaction.Rows.Count; i++)
                    {
                        c1PatientDetails.Rows.Add();
                        Int32 RowIndex = c1PatientDetails.Rows.Count - 1;

                        c1PatientDetails.SetData(RowIndex, 0, dtTransaction.Rows[i]["nPriorAuthorizationID"].ToString());
                        c1PatientDetails.SetData(RowIndex, 1, dtTransaction.Rows[i]["sPriorAuthorizationNo"].ToString());
                        c1PatientDetails.SetData(RowIndex, 2, dtTransaction.Rows[i]["sReferralName"].ToString());
                        c1PatientDetails.SetData(RowIndex, 3, dtTransaction.Rows[i]["sInsuranceName"].ToString());
                        c1PatientDetails.SetData(RowIndex, 4, dtTransaction.Rows[i]["nReferralID"].ToString());
                        c1PatientDetails.SetData(RowIndex, 5, dtTransaction.Rows[i]["bIsTrackAuthLimit"].ToString());
                        c1PatientDetails.SetData(RowIndex, 6, dtTransaction.Rows[i]["sAuthorizationNote"].ToString());
                        //c1PatientDetails.SetData(RowIndex, 7, dtTransaction.Rows[i]["nStartDate"].ToString());
                        //c1PatientDetails.SetData(RowIndex, 8, dtTransaction.Rows[i]["nExpDate"].ToString());
                        if (Convert.ToString(dtTransaction.Rows[i]["nStartDate"]) != "" && dtTransaction.Rows[i]["nStartDate"] != DBNull.Value)
                        {
                            c1PatientDetails.SetData(RowIndex, 7, dtTransaction.Rows[i]["nStartDate"].ToString());
                        }
                        if (Convert.ToString(dtTransaction.Rows[i]["nExpDate"]) != "" && dtTransaction.Rows[i]["nExpDate"] != DBNull.Value)
                        {
                            c1PatientDetails.SetData(RowIndex, 8, dtTransaction.Rows[i]["nExpDate"].ToString());
                        }
                        c1PatientDetails.SetData(RowIndex, 9, dtTransaction.Rows[i]["nVisitsAllowed"].ToString());
                        c1PatientDetails.SetData(RowIndex, 10, dtTransaction.Rows[i]["PatientName"].ToString());
                        c1PatientDetails.SetData(RowIndex, 11, dtTransaction.Rows[i]["VisitsRemained"].ToString());
                        c1PatientDetails.SetData(RowIndex, 12, dtTransaction.Rows[i]["bIsInActive"].ToString());

                    }
                    //SLR: Finally free ogloPatient, dtTRancasction
                    dtTransaction.Dispose();
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //SLR: Finally free ogloPatient, dtTRancasction
                if (ogloPatient != null)
                {
                    ogloPatient.Dispose();
                }

            }
        }

        private void FillCases(Int64 PatientID)
        {
            c1PatientDetails.DataSource = null;
            c1PatientDetails.Clear();
            ////Set the column Names.                              
            c1PatientDetails.Cols.Count = 13;
            c1PatientDetails.Rows.Count = 1;
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            try
            {
                DataTable dtTransaction = null;
                dtTransaction = ogloPatient.GetPatientCases(PatientID, gloPMGlobal.ClinicID, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString()));
                c1PatientDetails.DataSource = dtTransaction;

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //SLR: FRee ogloPatient
                if (ogloPatient != null)
                {
                    ogloPatient.Dispose();
                }
            }
            c1PatientDetails.SetData(0, 0, "nid");
            c1PatientDetails.SetData(0, 1, "Case Name");
            c1PatientDetails.SetData(0, 2, "AccidentType");
            c1PatientDetails.SetData(0, 3, "Claim #");
            c1PatientDetails.SetData(0, 4, "Start Date");
            c1PatientDetails.SetData(0, 5, "End Date");
            c1PatientDetails.SetData(0, 6, "Diag");
            c1PatientDetails.SetData(0, 7, "Facility");
            c1PatientDetails.SetData(0, 8, "Auth #");
            c1PatientDetails.SetData(0, 9, "Referring Provider");
            c1PatientDetails.SetData(0, 10, "Rpt Category ");
            //c1PatientDetails.SetData(0, 9, "AccidentType");
            c1PatientDetails.SetData(0, 11, "Note");


            c1PatientDetails.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[6].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[7].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[8].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[9].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[10].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[11].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            c1PatientDetails.Cols[0].AllowEditing = false;
            c1PatientDetails.Cols[1].AllowEditing = false;
            c1PatientDetails.Cols[2].AllowEditing = false;
            c1PatientDetails.Cols[3].AllowEditing = false;
            c1PatientDetails.Cols[4].AllowEditing = false;
            c1PatientDetails.Cols[5].AllowEditing = false;
            c1PatientDetails.Cols[6].AllowEditing = false;
            c1PatientDetails.Cols[7].AllowEditing = false;
            c1PatientDetails.Cols[8].AllowEditing = false;
            c1PatientDetails.Cols[9].AllowEditing = false;
            c1PatientDetails.Cols[10].AllowEditing = false;
            c1PatientDetails.Cols[11].AllowEditing = false;

            c1PatientDetails.Cols[0].Visible = false;

            c1PatientDetails.Cols[4].DataType = typeof(System.DateTime);
            c1PatientDetails.Cols[5].DataType = typeof(System.DateTime);
            c1PatientDetails.Cols[4].Format = "MM/dd/yyyy";
            c1PatientDetails.Cols[5].Format = "MM/dd/yyyy";

            bool _designWidth = false;
            try
            {
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPatientCases, gloPMGlobal.UserID);
                oSetting.Dispose();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            if (_designWidth == false)
            {
                int _width = (c1PatientDetails.Width - 60 / 8);

                c1PatientDetails.Cols[1].Width = 175;
                c1PatientDetails.Cols[2].Width = 75;
                c1PatientDetails.Cols[3].Width = 75;
                c1PatientDetails.Cols[4].Width = 75;
                c1PatientDetails.Cols[5].Width = 75;
                c1PatientDetails.Cols[6].Width = 175;
                c1PatientDetails.Cols[7].Width = 140;
                c1PatientDetails.Cols[8].Width = 75;
                c1PatientDetails.Cols[9].Width = 140;
                c1PatientDetails.Cols[10].Width = 100;
                c1PatientDetails.Cols[11].Width = 100;
            }


        }

        private void FillPatientTasks(Int64 PatientId)
        {
            DataTable dtTask = null; //SLR: new is not needed
            gloTaskMail.gloTask ogloTasks = new gloTaskMail.gloTask(gloPMGlobal.DatabaseConnectionString);
            try
            {
                c1PatientDetails.DataSource = null;
                c1PatientDetails.Clear();

                C1.Win.C1FlexGrid.CellStyle csSubiect = null; // c1PatientDetails.Styles.Add("cs_Subiect");
                try
                {
                    if (c1PatientDetails.Styles.Contains("cs_Subiect"))
                    {
                        csSubiect = c1PatientDetails.Styles["cs_Subiect"];
                    }
                    else
                    {
                        csSubiect = c1PatientDetails.Styles.Add("cs_Subiect");
                    }
                }
                catch
                {
                    csSubiect = c1PatientDetails.Styles.Add("cs_Subiect");
                }
                //SLR: PLease create font once...............................................
                csSubiect.Font = Strikeout; // new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                c1PatientDetails.Cols.Count = 12;
                c1PatientDetails.Rows.Count = 1;
                c1PatientDetails.SetData(0, Col_View_taskID, "TaskID");
                c1PatientDetails.SetData(0, Col_View_PriorityID, "PriorityID");
                c1PatientDetails.SetData(0, Col_View_FollowUpID, "FollowUpID");
                c1PatientDetails.SetData(0, Col_View_Priority, "");
                c1PatientDetails.SetData(0, Col_View_Subject, "Subject");
                c1PatientDetails.SetData(0, Col_View_Owner, "Owner");
                c1PatientDetails.SetData(0, Col_View_Status, "Status");
                c1PatientDetails.SetData(0, Col_View_DueDate, "Due Date");
                c1PatientDetails.SetData(0, Col_View_startdate, "Start Date");
                c1PatientDetails.SetData(0, Col_View_ProviderName, "Provider");
                c1PatientDetails.SetData(0, Col_View_Description, "Description");
                c1PatientDetails.SetData(0, Col_View_FollowUpIcon, "");

                c1PatientDetails.Cols[Col_View_taskID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_FollowUpID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_PriorityID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_Priority].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;
                c1PatientDetails.Cols[Col_View_Subject].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_Owner].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_Status].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_DueDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_startdate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_ProviderName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[Col_View_Description].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                c1PatientDetails.Cols[Col_View_taskID].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_FollowUpID].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_PriorityID].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_Priority].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_Subject].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_Owner].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_Status].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_DueDate].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_startdate].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_ProviderName].AllowEditing = false;
                c1PatientDetails.Cols[Col_View_Description].AllowEditing = false;

                c1PatientDetails.Cols[Col_View_Priority].AllowResizing = false;
                c1PatientDetails.Cols[Col_View_FollowUpIcon].AllowResizing = false;


                c1PatientDetails.Cols[Col_View_taskID].Visible = false;
                c1PatientDetails.Cols[Col_View_FollowUpID].Visible = false;
                c1PatientDetails.Cols[Col_View_PriorityID].Visible = false;

                bool _designWidth = false;
                try
                {
                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                    _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPatientTasks, gloPMGlobal.UserID);
                    oSetting.Dispose();
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                if (_designWidth == false)
                {
                    c1PatientDetails.Cols[Col_View_taskID].Width = 0;
                    c1PatientDetails.Cols[Col_View_FollowUpID].Width = 0;
                    c1PatientDetails.Cols[Col_View_PriorityID].Width = 0;
                    c1PatientDetails.Cols[Col_View_Priority].Width = 20;
                    c1PatientDetails.Cols[Col_View_Subject].Width = 180;
                    c1PatientDetails.Cols[Col_View_Owner].Width = 100;
                    c1PatientDetails.Cols[Col_View_Status].Width = 100;
                    c1PatientDetails.Cols[Col_View_DueDate].Width = 80;
                    c1PatientDetails.Cols[Col_View_startdate].Width = 80;
                    c1PatientDetails.Cols[Col_View_ProviderName].Width = 170;
                    c1PatientDetails.Cols[Col_View_Description].Width = 350;
                    c1PatientDetails.Cols[Col_View_FollowUpIcon].Width = 20;
                }

                System.Drawing.Image img = null;
                System.Drawing.Image img1 = null;
                System.Drawing.Image img2 = null;
                System.Drawing.Image img3 = null;
                System.Drawing.Image img4 = null;
                System.Drawing.Image img5 = null;

                img = global::gloPM.Properties.Resources.High_PriorityRed;
                img1 = global::gloPM.Properties.Resources.Low_Priority;
                img2 = global::gloPM.Properties.Resources.Today;
                img3 = global::gloPM.Properties.Resources.Tommorow;
                img4 = global::gloPM.Properties.Resources.No_Date;
                img5 = global::gloPM.Properties.Resources.Flag_Yellow;

                C1.Win.C1FlexGrid.CellStyle csTaskIcon = null; // c1PatientDetails.Styles.Add("csTaskIcon");
                try
                {
                    if (c1PatientDetails.Styles.Contains("csTaskIcon"))
                    {
                        csTaskIcon = c1PatientDetails.Styles["csTaskIcon"];
                    }
                    else
                    {
                        csTaskIcon = c1PatientDetails.Styles.Add("csTaskIcon");
                    }
                }
                catch
                {
                    csTaskIcon = c1PatientDetails.Styles.Add("csTaskIcon");
                }

                if (PatientId > 0)
                {
                    dtTask = ogloTasks.GetPatientTasksList(PatientId);
                }

                if (dtTask.Rows.Count > 0)
                {

                    for (int i = 0; i <= dtTask.Rows.Count - 1; i++)
                    {
                        c1PatientDetails.Rows.Add();
                        Int32 RowIndex = c1PatientDetails.Rows.Count - 1;

                        c1PatientDetails.SetData(RowIndex, Col_View_taskID, dtTask.Rows[i]["TaskID"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_PriorityID, dtTask.Rows[i]["PriorityID"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_FollowUpID, dtTask.Rows[i]["FollowUpID"].ToString());
                        switch (dtTask.Rows[i]["Priority"].ToString())
                        {
                            case "High Priority":
                                c1PatientDetails.SetCellImage(RowIndex, Col_View_Priority, img);
                                c1PatientDetails.SetData(RowIndex, Col_View_Priority, "High Priority");
                                break;
                            case "Normal Priority":

                                break;
                            case "Low Priority":
                                c1PatientDetails.SetCellImage(RowIndex, Col_View_Priority, img1);
                                c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, Col_View_Priority, "Low Priority");
                                break;
                            default:
                                break;
                            //' Default Normal
                            //c1PatientDetails.SetCellImage(RowIndex, 3, imgList_Common.Images(1))
                            //c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, 3, "Normal Priority")
                        }

                        c1PatientDetails.SetData(RowIndex, Col_View_Subject, dtTask.Rows[i]["Subject"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_Owner, dtTask.Rows[i]["Owner"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_DueDate, dtTask.Rows[i]["DueDate"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_startdate, dtTask.Rows[i]["StartDate"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_ProviderName, dtTask.Rows[i]["ProviderName"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_Description, dtTask.Rows[i]["Description"].ToString());
                        c1PatientDetails.SetData(RowIndex, Col_View_Status, dtTask.Rows[i]["Status"].ToString());
                        if (Convert.ToString(dtTask.Rows[i]["Followup"]).ToUpper() == "TODAY")
                        {
                            // img = global::gloTaskMail.Properties.Resources.Today;
                            c1PatientDetails.SetCellImage(RowIndex, Col_View_FollowUpIcon, img2);
                            c1PatientDetails.SetCellStyle(RowIndex, Col_View_FollowUpIcon, csTaskIcon);
                            c1PatientDetails.SetData(RowIndex, Col_View_FollowUpIcon, "TODAY");
                        }
                        else if (Convert.ToString(dtTask.Rows[i]["Followup"]).ToUpper() == "TOMMOROW")
                        {
                            // img = global::gloTaskMail.Properties.Resources.Tommorow;
                            c1PatientDetails.SetCellImage(RowIndex, Col_View_FollowUpIcon, img3);
                            c1PatientDetails.SetCellStyle(RowIndex, Col_View_FollowUpIcon, csTaskIcon);
                            c1PatientDetails.SetData(RowIndex, Col_View_FollowUpIcon, "TOMMOROW");
                        }
                        else if (Convert.ToString(dtTask.Rows[i]["Followup"]).ToUpper() == "NO DATE")
                        {
                            // img = global::gloTaskMail.Properties.Resources.No_Date;
                            c1PatientDetails.SetCellImage(RowIndex, Col_View_FollowUpIcon, img4);
                            c1PatientDetails.SetCellStyle(RowIndex, Col_View_FollowUpIcon, csTaskIcon);
                            c1PatientDetails.SetData(RowIndex, Col_View_FollowUpIcon, "NO DATE");
                        }
                        else
                        {
                            // img = global::gloTaskMail.Properties.Resources.Flag_Yellow;
                            c1PatientDetails.SetCellImage(RowIndex, Col_View_FollowUpIcon, img5);
                            c1PatientDetails.SetCellStyle(RowIndex, Col_View_FollowUpIcon, csTaskIcon);
                            c1PatientDetails.SetData(RowIndex, Col_View_FollowUpIcon, Convert.ToString(dtTask.Rows[i]["Followup"]).ToUpper());
                        }

                        if (Convert.ToString(dtTask.Rows[i]["Status"]).ToUpper() == "COMPLETED")
                        {
                            this.c1PatientDetails.Rows[RowIndex].Style = csSubiect;
                        }

                    }
                }



            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //SLR: Fianlly free oglotasks, dtTasks
                if (ogloTasks != null)
                {
                    ogloTasks.Dispose();
                }
                if (dtTask != null)
                {
                    dtTask.Dispose();
                }
            }
        }

        private void FillPatientBilling(Int64 PatientID)
        {
            c1PatientDetails.DataSource = null;

            c1PatientDetails.Clear();
            ////Set the column Names.                              
            c1PatientDetails.Cols.Count = 13;
            c1PatientDetails.Rows.Count = 1;

            c1PatientDetails.SetData(0, 0, "No.");
            c1PatientDetails.SetData(0, 1, "Date");
            c1PatientDetails.SetData(0, 2, "Claim #");
            c1PatientDetails.SetData(0, 3, "CPT");
            c1PatientDetails.SetData(0, 4, "Dx1");
            c1PatientDetails.SetData(0, 5, "Dx2");
            c1PatientDetails.SetData(0, 6, "Dx3");
            c1PatientDetails.SetData(0, 7, "Dx4");
            c1PatientDetails.SetData(0, 8, "M1"); //By Suraj (mantis bug no: 876)
            c1PatientDetails.SetData(0, 9, "M2"); //By Suraj (mantis bug no: 876)
            c1PatientDetails.SetData(0, 10, "Charges");

            c1PatientDetails.SetData(0, 11, "Unit");
            c1PatientDetails.SetData(0, 12, "Total");
            c1PatientDetails.Cols[2].DataType = typeof(System.Int64);

            c1PatientDetails.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[6].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[7].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[8].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[9].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1PatientDetails.Cols[10].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1PatientDetails.Cols[11].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1PatientDetails.Cols[12].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

            c1PatientDetails.Cols[0].AllowEditing = false;
            c1PatientDetails.Cols[1].AllowEditing = false;
            c1PatientDetails.Cols[2].AllowEditing = false;
            c1PatientDetails.Cols[3].AllowEditing = false;
            c1PatientDetails.Cols[4].AllowEditing = false;
            c1PatientDetails.Cols[5].AllowEditing = false;
            c1PatientDetails.Cols[6].AllowEditing = false;
            c1PatientDetails.Cols[7].AllowEditing = false;
            c1PatientDetails.Cols[8].AllowEditing = false;
            c1PatientDetails.Cols[9].AllowEditing = false;
            c1PatientDetails.Cols[10].AllowEditing = false;
            c1PatientDetails.Cols[11].AllowEditing = false;
            c1PatientDetails.Cols[12].AllowEditing = false;

            c1PatientDetails.Cols[0].Visible = false;

            // set the datatype of unit column to int
            c1PatientDetails.Cols[11].DataType = typeof(System.Decimal);
            c1PatientDetails.Cols[11].Format = "#############0.####";
            // end
            C1.Win.C1FlexGrid.CellStyle csCurrencyStyle = null; // c1PatientDetails.Styles.Add("cs_CurrencyStyle");
            try
            {
                if (c1PatientDetails.Styles.Contains("cs_CurrencyStyle"))
                {
                    csCurrencyStyle = c1PatientDetails.Styles["cs_CurrencyStyle"];
                }
                else
                {
                    csCurrencyStyle = c1PatientDetails.Styles.Add("cs_CurrencyStyle");
                }
            }
            catch
            {
                csCurrencyStyle = c1PatientDetails.Styles.Add("cs_CurrencyStyle");
            }
            csCurrencyStyle.DataType = typeof(System.Decimal);
            csCurrencyStyle.Format = "c";
            c1PatientDetails.Cols[10].Style = csCurrencyStyle;
            c1PatientDetails.Cols[12].Style = csCurrencyStyle;


            c1PatientDetails.Cols[1].DataType = typeof(System.DateTime);
            c1PatientDetails.Cols[1].Format = "MM/dd/yyyy";



            bool _designWidth = false;
            try
            {
                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPatientBilling, gloPMGlobal.UserID);
                oSetting.Dispose();
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            if (_designWidth == false)
            {
                int _width = (c1PatientDetails.Width - 60) / 32;

                //c1PatientDetails.Cols[0].Width = 35;
                //c1PatientDetails.Cols[3].Width = _width * 3;
                //c1PatientDetails.Cols[4].Width = _width * 3;
                //c1PatientDetails.Cols[5].Width = _width * 5;
                //c1PatientDetails.Cols[6].Width = _width * 6;
                //c1PatientDetails.Cols[7].Width = _width * 3;
                //c1PatientDetails.Cols[8].Width = _width * 3;
                //c1PatientDetails.Cols[9].Width = _width * 3;
                //c1PatientDetails.Cols[10].Width = _width * 3;
                //c1PatientDetails.Cols[11].Width = _width * 4;
            }

            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
            DataTable dtTransaction = ogloBilling.GetPatientTransactions(PatientID);

            if (dtTransaction != null)
            {
                for (int i = 0; i < dtTransaction.Rows.Count; i++)
                {
                    c1PatientDetails.Rows.Add();
                    Int32 RowIndex = c1PatientDetails.Rows.Count - 1;

                    c1PatientDetails.SetData(RowIndex, 0, i);
                    c1PatientDetails.SetData(RowIndex, 1, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtTransaction.Rows[i]["nFromDate"])));
                    c1PatientDetails.SetData(RowIndex, 2, dtTransaction.Rows[i]["nClaimNo"].ToString().PadLeft(5, '0'));
                    c1PatientDetails.SetData(RowIndex, 3, dtTransaction.Rows[i]["sCPTCode"].ToString() + "-" + dtTransaction.Rows[i]["sCPTDescription"].ToString());
                    c1PatientDetails.SetData(RowIndex, 4, dtTransaction.Rows[i]["sDx1Code"].ToString());// + "-" + dtTransaction.Rows[i]["sDx1Description"].ToString());
                    c1PatientDetails.SetData(RowIndex, 5, dtTransaction.Rows[i]["sDx2Code"].ToString());// + "-" + dtTransaction.Rows[i]["sDx2Description"].ToString());
                    c1PatientDetails.SetData(RowIndex, 6, dtTransaction.Rows[i]["sDx3Code"].ToString());// + "-" + dtTransaction.Rows[i]["sDx3Description"].ToString());
                    c1PatientDetails.SetData(RowIndex, 7, dtTransaction.Rows[i]["sDx4Code"].ToString());// + "-" + dtTransaction.Rows[i]["sDx4Description"].ToString());
                    c1PatientDetails.SetData(RowIndex, 8, dtTransaction.Rows[i]["sMod1Code"].ToString());// + "-" + dtTransaction.Rows[i]["sMod1Description"].ToString());
                    c1PatientDetails.SetData(RowIndex, 9, dtTransaction.Rows[i]["sMod2Code"].ToString());// + "-" + dtTransaction.Rows[i]["sMod2Description"].ToString());
                    c1PatientDetails.SetData(RowIndex, 10, dtTransaction.Rows[i]["dCharges"].ToString());

                    c1PatientDetails.SetData(RowIndex, 11, dtTransaction.Rows[i]["dUnit"]);
                    c1PatientDetails.SetData(RowIndex, 12, dtTransaction.Rows[i]["dTotal"].ToString());

                }
            }
            //SLR: FRee ogloBilling, dtTransactions
            if (ogloBilling != null)
            {
                ogloBilling.Dispose();
            }
            if (dtTransaction != null)
            {
                dtTransaction.Dispose();
            }
        }
        private void FillPatientAppointments(Int64 PatientID)
        {
            c1PatientDetails.Visible = false;

            try
            {
                gloAppointmentScheduling.gloAppointment ogloAppointment = new gloAppointmentScheduling.gloAppointment(gloPMGlobal.DatabaseConnectionString);

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                oDB.Connect(false);
                DataTable dt = null;

                gloDatabaseLayer.DBParameters oParam = new gloDatabaseLayer.DBParameters();
                oParam.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParam.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParam.Add("@nExtemptFromReport", _IsExemptFromReport, ParameterDirection.Input, SqlDbType.Bit);

                oDB.Retrive("gSP_GetPatientAppointments", oParam, out dt);

                oDB.Disconnect();
                //SLR: Dispose and then
                oDB.Dispose();
                oDB = null;
                //SLR: Free oParam
                oParam.Dispose();

                #region Search

                _SelcetedPatient = PatientDetails.PatientAppointments;

                if (_SearchFlag == true)
                {
                    DataView dvFilter; //SLR: new not needed
                    dvFilter = dt.DefaultView;

                    string strFilter = "";

                    if (dtpToDate.Enabled == true && dtpFromDate.Enabled == true)
                    {
                        strFilter = "dtstartDate >=  " + gloDateMaster.gloDate.DateAsNumber(dtpFromDate.Value.Date.ToString()) + " AND  dtstartDate <=  " + gloDateMaster.gloDate.DateAsNumber(dtpToDate.Value.Date.ToString()) + "";
                    }
                    // FILTER DATAVIEW ''
                    if (cmbStatus.Text != "All" && cmbProvider.Text != "All") // FILTER BOTH ''
                    {
                        // FILTER STATUS ''
                        if (strFilter == "")
                            strFilter = " nUsedStatus = " + cmbStatus.SelectedValue + " ";
                        else
                            strFilter = strFilter + " AND nUsedStatus = " + cmbStatus.SelectedValue + " ";
                        // '' FILTER USER ''
                        //strFilter = strFilter + " AND Provider = '" + cmbProvider.Text + "'";
                        strFilter = strFilter + " AND Provider LIKE '%" + cmbProvider.Text.Replace("'", "''") + "%'";

                    }
                    else if (cmbStatus.Text != "All" && cmbProvider.Text == "All") //'' FILTER ONLY STATUS ''
                    {
                        if (strFilter == "")
                            strFilter = " nUsedStatus = " + cmbStatus.SelectedValue + " ";
                        else
                            strFilter = strFilter + " AND nUsedStatus = " + cmbStatus.SelectedValue + " ";

                    }
                    else if (cmbStatus.Text == "All" && cmbProvider.Text != "All") //'' FILTER ONLY Provider ''
                    {
                        // strFilter = strFilter + " AND Provider = '" + cmbProvider.Text + "'";

                        if (strFilter == "")
                            strFilter = " Provider LIKE '%" + cmbProvider.Text.Replace("'", "''") + "%'";
                        else
                            strFilter = strFilter + " AND Provider LIKE '%" + cmbProvider.Text.Replace("'", "''") + "%'";

                    }

                    dvFilter.RowFilter = strFilter;

                    dt = dvFilter.ToTable().Copy();
                    //SLR: Free dvFilter
                    dvFilter.Dispose();
                }

                //Added by Mayuri:20110209-Appointment optimzation-directly bind date to c1


                #endregion

                #region " Grid Design "

                c1PatientDetails.DataSource = null;
                c1PatientDetails.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                this.pnlSearchFilter.Visible = true;

                #region " Column Constants "


                const int COL_MSTAPTID = 0;
                const int COL_APTDTL = 1; //DTLAppointmentID
                const int COL_DATE = 2;
                const int COL_DATE1 = 3;
                const int COL_STARTTIME = 4;
                const int COL_ENDTIME = 5;
                const int COL_LOCATION = 6;
                const int COL_DEPARTMENT = 7;
                const int COL_ASBASEID = 8;
                const int COL_APPTYPE = 9;
                const int COL_NOTES = 10;
                const int COL_PROVIDER = 11;
                const int COL_USEDSTATUS = 12;
                const int COL_MASTERAPPMETHOD = 13;
                const int COL_APPMETHOD = 14;
                const int COL_LINENUMBER = 15;
                const int COL_ASBASEFLAG = 16;
                //const int COL_CLINICID = 16;
                const int COL_STATUS = 17;

                //GLO2011-0011959 - Multiple days appointment
                //'Added new column EndDate for multiple day's appointment
                const int COL_EndDate = 18;


                const int COL_COUNT = 18;
                c1PatientDetails.DataSource = null;
                c1PatientDetails.Rows.Count = 1;
                c1PatientDetails.Cols.Count = COL_COUNT;

                #endregion " Column Constants "

                c1PatientDetails.ExtendLastCol = false;
                c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1PatientDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;


                //Bind data table to grid 
                if (dt != null)
                {
                    c1PatientDetails.DataSource = dt;
                }


                //Set column width as per prev settings 
                bool _designWidth = false;
                try
                {
                    gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                    _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPatientAppointments, gloPMGlobal.UserID);
                    oSetting.Dispose();
                }
                catch (Exception) // ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                if (_designWidth == false)
                {
                    int _width = c1PatientDetails.Width - 17;

                    c1PatientDetails.Cols[COL_MSTAPTID].Width = 0;
                    c1PatientDetails.Cols[COL_APTDTL].Width = 0;
                    // c1PatientDetails.Cols[COL_DATE].Width = Convert.ToInt32(_width * 0.1);
                    c1PatientDetails.Cols[COL_DATE].Width = 0;
                    c1PatientDetails.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.1);
                    c1PatientDetails.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.1);
                    c1PatientDetails.Cols[COL_LOCATION].Width = Convert.ToInt32(_width * 0.1);
                    c1PatientDetails.Cols[COL_DEPARTMENT].Width = Convert.ToInt32(_width * 0.1);
                    c1PatientDetails.Cols[COL_APPTYPE].Width = Convert.ToInt32(_width * 0.15);
                    c1PatientDetails.Cols[COL_ASBASEID].Width = 0;
                    c1PatientDetails.Cols[COL_PROVIDER].Width = Convert.ToInt32(_width * 0.15);
                    c1PatientDetails.Cols[COL_NOTES].Width = Convert.ToInt32(_width * 0.2);
                    c1PatientDetails.Cols[COL_USEDSTATUS].Width = 0;
                    c1PatientDetails.Cols[COL_STATUS].Width = Convert.ToInt32(_width * 0.1);
                    c1PatientDetails.Cols[COL_DATE1].Width = Convert.ToInt32(_width * 0.1);
                    c1PatientDetails.Cols[COL_EndDate].Width = 0;
                }

                C1.Win.C1FlexGrid.CellStyle csWordWrap = null;
                try
                {
                    if (c1PatientDetails.Styles.Contains("csWordWrap"))
                    {
                        csWordWrap = c1PatientDetails.Styles["csWordWrap"];
                    }
                    else
                    {
                        csWordWrap = c1PatientDetails.Styles.Add("csWordWrap");
                    }
                }
                catch
                {
                    csWordWrap = c1PatientDetails.Styles.Add("csWordWrap");
                }
                csWordWrap.WordWrap = true;


                c1PatientDetails.SetData(0, COL_MSTAPTID, "Master Appointment ID");
                c1PatientDetails.SetData(0, COL_APTDTL, "Appointment Detail ID");
                c1PatientDetails.SetData(0, COL_DATE, "Date");
                c1PatientDetails.SetData(0, COL_STARTTIME, "StartTime");
                c1PatientDetails.SetData(0, COL_ENDTIME, "EndTime");
                c1PatientDetails.SetData(0, COL_LOCATION, "Location");
                c1PatientDetails.SetData(0, COL_DEPARTMENT, "Department");
                c1PatientDetails.SetData(0, COL_APPTYPE, "Appointment Type");
                c1PatientDetails.SetData(0, COL_ASBASEID, "Provider ID");
                c1PatientDetails.SetData(0, COL_NOTES, "Notes");
                c1PatientDetails.SetData(0, COL_PROVIDER, "Provider");
                c1PatientDetails.SetData(0, COL_USEDSTATUS, "Used Status");
                c1PatientDetails.SetData(0, COL_MASTERAPPMETHOD, "Master Method");
                c1PatientDetails.SetData(0, COL_APPMETHOD, "App method");
                c1PatientDetails.SetData(0, COL_LINENUMBER, "Line No");
                c1PatientDetails.SetData(0, COL_ASBASEFLAG, "BASE FLAG");
                // c1PatientDetails.SetData(0, COL_CLINICID, "Clinic ID");
                c1PatientDetails.SetData(0, COL_STATUS, "Status");
                c1PatientDetails.SetData(0, COL_DATE1, "Date");
                //End


                c1PatientDetails.Cols[COL_MSTAPTID].Visible = false;
                c1PatientDetails.Cols[COL_APTDTL].Visible = false;
                c1PatientDetails.Cols[COL_DATE].Visible = false;
                c1PatientDetails.Cols[COL_STARTTIME].Visible = true;
                c1PatientDetails.Cols[COL_ENDTIME].Visible = true;
                c1PatientDetails.Cols[COL_LOCATION].Visible = true;
                c1PatientDetails.Cols[COL_DEPARTMENT].Visible = true;
                c1PatientDetails.Cols[COL_APPTYPE].Visible = true;
                c1PatientDetails.Cols[COL_ASBASEID].Visible = false;
                c1PatientDetails.Cols[COL_PROVIDER].Visible = true;
                c1PatientDetails.Cols[COL_NOTES].Visible = true;
                c1PatientDetails.Cols[COL_USEDSTATUS].Visible = false;
                c1PatientDetails.Cols[COL_MASTERAPPMETHOD].Visible = false;
                c1PatientDetails.Cols[COL_APPMETHOD].Visible = false;
                c1PatientDetails.Cols[COL_LINENUMBER].Visible = false;
                //c1PatientDetails.Cols[COL_CLINICID].Visible = false;
                c1PatientDetails.Cols[COL_ASBASEFLAG].Visible = false;
                c1PatientDetails.Cols[COL_STATUS].Visible = true;
                c1PatientDetails.Cols[COL_DATE1].Visible = true;

                //GLO2011-0011959 - Multiple days appointment
                //Patient Details - Appointment Added new column for multiple day's appointment and setting visible property

                c1PatientDetails.Cols[COL_EndDate].Visible = false;



                //end
                c1PatientDetails.Cols[COL_MSTAPTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_APTDTL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_STARTTIME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_ENDTIME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_LOCATION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_DEPARTMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_APPTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_ASBASEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_PROVIDER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_NOTES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_USEDSTATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_STATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_DATE1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_MSTAPTID].AllowEditing = false;
                c1PatientDetails.Cols[COL_APTDTL].AllowEditing = false;
                c1PatientDetails.Cols[COL_DATE].AllowEditing = false;
                c1PatientDetails.Cols[COL_STARTTIME].AllowEditing = false;
                c1PatientDetails.Cols[COL_ENDTIME].AllowEditing = false;
                c1PatientDetails.Cols[COL_LOCATION].AllowEditing = false;
                c1PatientDetails.Cols[COL_DEPARTMENT].AllowEditing = false;
                c1PatientDetails.Cols[COL_APPTYPE].AllowEditing = false;
                c1PatientDetails.Cols[COL_ASBASEID].AllowEditing = false;
                c1PatientDetails.Cols[COL_PROVIDER].AllowEditing = false;
                c1PatientDetails.Cols[COL_NOTES].AllowEditing = false;
                c1PatientDetails.Cols[COL_USEDSTATUS].AllowEditing = false;
                c1PatientDetails.Cols[COL_MASTERAPPMETHOD].AllowEditing = false;
                c1PatientDetails.Cols[COL_APPMETHOD].AllowEditing = false;
                c1PatientDetails.Cols[COL_LINENUMBER].AllowEditing = false;
                //c1PatientDetails.Cols[COL_CLINICID].AllowEditing = false;
                c1PatientDetails.Cols[COL_STATUS].AllowEditing = false;
                c1PatientDetails.Cols[COL_DATE1].AllowEditing = false;
                //5076 DateFormat change.
                //c1PatientDetails.Cols[COL_DATE].DataType = typeof(System.DateTime);
                //c1PatientDetails.Cols[COL_DATE].Format = "MM/dd/yyyy";
                //Mayuri
                c1PatientDetails.Cols[COL_DATE1].DataType = typeof(System.DateTime);
                c1PatientDetails.Cols[COL_DATE1].Format = "MM/dd/yyyy";


                c1PatientDetails.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
                c1PatientDetails.Cols[COL_STARTTIME].Format = "h:mm tt";

                c1PatientDetails.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
                c1PatientDetails.Cols[COL_ENDTIME].Format = "h:mm tt";


                #endregion " Grid Design "


            }




            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                c1PatientDetails.Visible = true;

            }
        }
        private void FillPatientInsurance(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {

                DataTable dt_ins = null;

                #region "Get Insurances "

                string _sqlQuery = " SELECT nInsuranceID,  "
                     + " ISNULL(sInsuranceName, '') AS InsuranceName,     "
                     + " CASE ISNULL(nInsuranceFlag,0)     "
                     + " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'      "
                     + " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'       "
                     + " ELSE '' END  AS sInsuranceFlag,      "
                     + " ISNULL(sSubscriberID, '')  AS sSubscriberID,     "
                     + " ISNULL(sSubscriberPolicy#, '') AS sSubscriberPolicy,     "
                     + " ISNULL(sGroup, '') AS sGroup,      "
                     + " dbo.formatPhone(sInsurancePhone,0),   "
                     + " ISNULL(nDeductableamount, 0) AS Deductableamount,     "
                     + " ISNULL(nCoveragePercent, 0) AS CoveragePercent,      "
                     + " ISNULL(nCoPay, 0) AS CoPay,      "
                     + " CASE ISNULL(nInsuranceFlag,0)     "
                     + " WHEN 0 THEN 4     "
                     + " ELSE nInsuranceFlag END  AS SortOrder   ,dtReviewedDateTime, sEligibiltyInsuranceNote  "
                     + " FROM PatientInsurance_DTL      "
                     + " WHERE nPatientID= " + PatientID + "  ORDER BY SortOrder ";


                oDB.Connect(false);

                dt_ins = null;
                oDB.Retrive_Query(_sqlQuery, out dt_ins);
                oDB.Disconnect();

                #endregion

                if (dt_ins != null)
                {
                    c1PatientDetails.Clear();
                    c1PatientDetails.Cols.Count = dt_ins.Columns.Count;
                    c1PatientDetails.DataSource = dt_ins.Copy().DefaultView;

                    c1PatientDetails.SetData(0, 0, "InsuranceID");
                    c1PatientDetails.SetData(0, 1, "Insurance Name");
                    c1PatientDetails.SetData(0, 2, "Type");
                    c1PatientDetails.SetData(0, 3, "Insurance ID");
                    c1PatientDetails.SetData(0, 4, "Policy");
                    c1PatientDetails.SetData(0, 5, "Group");
                    c1PatientDetails.SetData(0, 6, "Insurance Phone");
                    c1PatientDetails.SetData(0, 7, "Deductible Amount");
                    c1PatientDetails.SetData(0, 8, "Co-Insurance %");
                    c1PatientDetails.SetData(0, 9, "Copay");
                    c1PatientDetails.SetData(0, 11, "Last Reviewed");
                    c1PatientDetails.SetData(0, 12, "Benefit Note");


                    c1PatientDetails.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[5].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[6].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[7].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    c1PatientDetails.Cols[8].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    c1PatientDetails.Cols[9].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    c1PatientDetails.Cols[11].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    c1PatientDetails.Cols[12].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


                    c1PatientDetails.Cols[0].AllowEditing = false;
                    c1PatientDetails.Cols[1].AllowEditing = false;
                    c1PatientDetails.Cols[2].AllowEditing = false;
                    c1PatientDetails.Cols[3].AllowEditing = false;
                    c1PatientDetails.Cols[4].AllowEditing = false;
                    c1PatientDetails.Cols[5].AllowEditing = false;
                    c1PatientDetails.Cols[6].AllowEditing = false;
                    c1PatientDetails.Cols[7].AllowEditing = false;
                    c1PatientDetails.Cols[8].AllowEditing = false;
                    c1PatientDetails.Cols[9].AllowEditing = false;
                    c1PatientDetails.Cols[11].AllowEditing = false;
                    c1PatientDetails.Cols[12].AllowEditing = false;


                    bool _designWidth = false;
                    try
                    {
                        gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                        _designWidth = oSetting.LoadGridColumnWidth(c1PatientDetails, gloSettings.ModuleOfGridColumn.DashBoardPatientInsurance, gloPMGlobal.UserID);
                        oSetting.Dispose();
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                    }
                    if (_designWidth == false)
                    {
                        int _width = (c1PatientDetails.Width - 15) / 12;


                        c1PatientDetails.Cols[0].Width = 0;    //InsuranceID 
                        c1PatientDetails.Cols[1].Width = Convert.ToInt32(_width * 3); //Ins Name
                        c1PatientDetails.Cols[2].Width = Convert.ToInt32(_width * 1.2); //Type
                        c1PatientDetails.Cols[3].Width = Convert.ToInt32(_width * 1.2); //Sub ID
                        c1PatientDetails.Cols[4].Width = 0; //Policy
                        c1PatientDetails.Cols[5].Width = Convert.ToInt32(_width * 1.2); //Group
                        c1PatientDetails.Cols[6].Width = Convert.ToInt32(_width * 1.5); //Phone 
                        c1PatientDetails.Cols[7].Width = Convert.ToInt32(_width * 1.2); //Deduct
                        c1PatientDetails.Cols[8].Width = Convert.ToInt32(_width * 1.2); //Copay Per
                        c1PatientDetails.Cols[9].Width = Convert.ToInt32(_width * 1.2); //Copay
                        c1PatientDetails.Cols[10].Width = 0;
                        c1PatientDetails.Cols[11].Width = Convert.ToInt32(_width * 1.2); //Copay
                        c1PatientDetails.Cols[12].Width = Convert.ToInt32(_width * 1.2); //Copay

                    }

                    c1PatientDetails.Cols[0].Visible = false;
                    c1PatientDetails.Cols[4].Visible = false;
                    c1PatientDetails.Cols[10].Visible = false;
                    c1PatientDetails.Cols[11].DataType = typeof(System.DateTime);
                    c1PatientDetails.Cols[11].Format = "MM/dd/yyyy";

                    C1.Win.C1FlexGrid.CellStyle csCurrencyStyle = null; // c1PatientDetails.Styles.Add("cs_CurrencyStyle");

                    try
                    {
                        if (c1PatientDetails.Styles.Contains("cs_CurrencyStyle"))
                        {
                            csCurrencyStyle = c1PatientDetails.Styles["cs_CurrencyStyle"];
                        }
                        else
                        {
                            csCurrencyStyle = c1PatientDetails.Styles.Add("cs_CurrencyStyle");
                        }
                    }
                    catch
                    {
                        csCurrencyStyle = c1PatientDetails.Styles.Add("cs_CurrencyStyle");
                    }
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";

                    c1PatientDetails.Cols[7].Style = csCurrencyStyle;
                    c1PatientDetails.Cols[9].Style = csCurrencyStyle;

                }
                //SLR: FRee dtIns
                if (dt_ins != null)
                {
                    dt_ins.Dispose();
                    dt_ins = null;
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }


        //MaheshB Fill Search Comboes
        private void FillAppointmentStatusCombo()
        {
            this.cmbStatus.SelectedIndexChanged -= new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            cmbStatus.DataSource = null;
            DataTable dtStatus = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                string _strSQL = "SELECT ISNULL(nAppointmentStatusID,0) AS  nAppointmentStatusID  ,ISNULL(sAppointmentStatus,'') AS sAppointmentStatus  from AB_AppointmentStatus WHERE bIsBlocked ='False'";
                oDB.Retrive_Query(_strSQL, out dtStatus);
                oDB.Disconnect();
                cmbStatus.DataSource = null;
                cmbStatus.Items.Clear();
                DataRow r = null;
                r = dtStatus.NewRow();
                r["sAppointmentStatus"] = "All";
                r["nAppointmentStatusID"] = 0;
                dtStatus.Rows.InsertAt(r, 0);

                cmbStatus.DataSource = dtStatus;
                cmbStatus.DisplayMember = dtStatus.Columns["sAppointmentStatus"].ColumnName.ToString();
                cmbStatus.ValueMember = dtStatus.Columns["nAppointmentStatusID"].ColumnName.ToString();

                if (cmbStatus.Items.Count > 0)
                {
                    cmbStatus.SelectedIndex = 0;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                //MessageBox.Show(M, ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            }

        }

        //MaheshB
        private void FillProviderCombo()
        {

            try
            {
                this.cmbProvider.SelectedIndexChanged -= new System.EventHandler(this.cmbProvider_SelectedIndexChanged);

                DataTable dt = null; //SLR: new is not needed

                dt = gloPMMasters.GetProviders();

                cmbProvider.DataSource = null;
                cmbProvider.Items.Clear();

                DataRow r = null;
                r = dt.NewRow();
                r["sProviderName"] = "All";
                r["nProviderID"] = 0;
                dt.Rows.InsertAt(r, 0);
                //cmbProvider.Items.Insert(0,)

                string strProviderName = string.Empty;
                cmbProvider.DataSource = dt;
                cmbProvider.DisplayMember = Convert.ToString(dt.Columns["sProviderName"].ColumnName);
                cmbProvider.ValueMember = Convert.ToString(dt.Columns["nProviderID"].ColumnName);

                if (cmbProvider.Items.Count > 0)
                {
                    cmbProvider.SelectedIndex = 0;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                //throw;
            }
            finally
            {
                this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            }


        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show("From date should not exceed To date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FromToDateChange();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show("To date should not precede From date.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FromToDateChange();
        }

        private void FromToDateChange()
        {
            if (_AppClick == false)
            {
                switch (_SelcetedPatient)
                {
                    case PatientDetails.PatientDocuments:
                        {
                            FillPatientDocuments(_CurrentPatientId);
                        }
                        break;
                    case PatientDetails.PatientAppointments:
                        {
                            _SearchFlag = true;
                            _SelcetedPatient = PatientDetails.PatientInsurance;
                            FillPatientAppointments(_CurrentPatientId);
                        }
                        break;
                }

            }
        }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FromToDateChange();
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            FromToDateChange();
        }

        private void c1PatientDetails_MouseDown(object sender, MouseEventArgs e)
        {

            try
            {

                for (int i = cmnu_PatientDetails.Items.Count - 1; i >= 0; i--)
                {
                    if (cmnu_PatientDetails.Items[i].Text == "Check In Templates" || cmnu_PatientDetails.Items[i].Text == "Appointment Letters Templates" || cmnu_PatientDetails.Items[i].Text == "View History")
                    {

                        try
                        {
                            ToolStripMenuItem oCatMenuItem = cmnu_PatientDetails.Items[i] as ToolStripMenuItem;
                            clearAssociationMenus(oCatMenuItem);

                            if (oCatMenuItem.Text == "View History")
                            {
                                oCatMenuItem.Click -= new System.EventHandler(this.mnuItem_ViewApptHistory_Click);
                            }
                            cmnu_PatientDetails.Items.RemoveAt(i);
                            try
                            {
                                oCatMenuItem.Dispose();
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                        }
                    }

                }

                //Added By Pramod Nair For Disabling the Context Menu if there are no rights
                if (gloPMGlobal.UserName.Trim() != "")
                {
                    //SLR: before making once more new, free the exisitng memory
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(gloPMGlobal.DatabaseConnectionString);
                    oClsgloUserRights.CheckForUserRights(gloPMGlobal.UserName);
                }

                //if (c1PatientDetails.Rows.Count > 1 && c1PatientDetails.HitTest(e.X, e.Y).Row >= 1)
                //if (c1PatientDetails.Rows.Count > 1)
                if (c1PatientDetails.Rows.Count >= 1)
                {
                    Int32 tempRow = 0;
                    tempRow = c1PatientDetails.HitTest(e.X, e.Y).Row;
                    if (tempRow == -1)
                    {
                        c1PatientDetails.ContextMenuStrip = null;
                        return;
                    }
                    c1PatientDetails.Row = tempRow;
                    if (e.Button == MouseButtons.Right)
                    {
                        if (_SelcetedPatient == PatientDetails.PatientInsurance && c1PatientDetails.Rows[tempRow].Index > 0) //if it is child node
                        {
                            //c1PatientDetails.RowSel = tempRow;
                            c1PatientDetails.Row = tempRow;
                            tempInsId = Convert.ToInt64(c1PatientDetails.GetData(tempRow, 0));
                            tempInsName = Convert.ToString(c1PatientDetails.GetData(tempRow, 1));

                            //16 - Copay , 17 - Coverage column
                            if (c1PatientDetails.GetData(tempRow, c1PatientDetails.Cols["CoPay"].Index) != null)
                            { copayAmount = Convert.ToDecimal(c1PatientDetails.GetData(tempRow, c1PatientDetails.Cols["CoPay"].Index)); }
                            if (c1PatientDetails.GetData(tempRow, c1PatientDetails.Cols["CoveragePercent"].Index) != null)
                            { coveragePercent = Convert.ToDecimal(c1PatientDetails.GetData(tempRow, c1PatientDetails.Cols["CoveragePercent"].Index)); }

                            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                            DataTable dtEligibility = null; //SLR: new is not neeed

                            if (tempInsId != 0)
                            {
                                c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                                mnuItem_Authorization.Visible = true;
                                mnuItem_eligibilityCheck.Visible = true;

                                dtEligibility = oSetting.GetSetting("EligibilityCheckTest", 1);
                                if (dtEligibility.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(dtEligibility.Rows[0]["sSettingsValue"]) == 1)
                                    {
                                        mnuItem_eligibilityCheckTest.Visible = true;
                                    }
                                    else
                                    {
                                        mnuItem_eligibilityCheckTest.Visible = false;
                                    }
                                }

                                mnuItem_copay.Visible = true;
                                mnuItem_Ledger.Visible = false;
                                mnuItem_ModifyCharges.Visible = false;
                                mnuItem_Payment.Visible = false;
                                mnuItem_coverage.Visible = true;
                                mnuItem_Modify.Visible = true;
                                mnuItem_Add.Visible = false;
                                mnuItem_RemoveRef.Visible = false;
                                mnuItem_ViewBenefits.Visible = true;
                                mnuItem_NewWCForm.Visible = false;
                                mnuItem_ModifyWCForm.Visible = false;

                                //Added By Pramod For Checking For Access Rights
                                mnuItem_Modify.Enabled = oClsgloUserRights.ModifyPatient;

                            }//if (tempTaskId > 0)
                            else
                            {
                                c1PatientDetails.ContextMenuStrip = null;
                            }
                            //SLR: Free oSettings, dtELigibility
                            oSetting.Dispose();
                            if (dtEligibility != null)
                            {
                                dtEligibility.Dispose();
                            }

                        }
                        else if (_SelcetedPatient == PatientDetails.PatientInsurance && c1PatientDetails.Rows[tempRow].Index > 0)
                        {
                            c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                            mnuItem_Authorization.Visible = false;
                            mnuItem_eligibilityCheck.Visible = false;
                            mnuItem_eligibilityCheckTest.Visible = false;
                            mnuItem_copay.Visible = true;
                            mnuItem_coverage.Visible = true;
                            mnuItem_ModifyCharges.Visible = false;
                            mnuItem_Ledger.Visible = false;
                            mnuItem_Payment.Visible = false;
                            mnuItem_Modify.Visible = false;

                            mnuItem_Add.Visible = false;
                            mnuItem_RemoveRef.Visible = false;
                            mnuItem_ViewBenefits.Visible = false;

                            mnuItem_NewWCForm.Visible = false;
                            mnuItem_ModifyWCForm.Visible = false;

                        }
                        else if ((_SelcetedPatient == PatientDetails.PatientBalance || _SelcetedPatient == PatientDetails.PatientBilling) && c1PatientDetails.Rows[tempRow].Index > 0)
                        {
                            c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                            mnuItem_Authorization.Visible = false;
                            mnuItem_eligibilityCheck.Visible = false;
                            mnuItem_eligibilityCheckTest.Visible = false;
                            mnuItem_copay.Visible = false;
                            mnuItem_Ledger.Visible = true;
                            mnuItem_Payment.Visible = true;
                            mnuItem_coverage.Visible = false;
                            mnuItem_ModifyCharges.Visible = true;
                            mnuItem_Add.Visible = false;
                            mnuItem_RemoveRef.Visible = false;
                            mnuItem_Modify.Visible = false;
                            mnuItem_ViewBenefits.Visible = false;

                            mnuItem_NewWCForm.Visible = false;
                            mnuItem_ModifyWCForm.Visible = false;

                        }
                        else if ((_SelcetedPatient == PatientDetails.PatientAppointments) && c1PatientDetails.Rows[tempRow].Index > 0)
                        {
                            c1PatientDetails.RowSel = tempRow;
                            c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                            mnuItem_Authorization.Visible = false;
                            mnuItem_eligibilityCheck.Visible = false;
                            mnuItem_eligibilityCheckTest.Visible = false;
                            mnuItem_ModifyCharges.Visible = false;
                            mnuItem_copay.Visible = false;
                            mnuItem_Ledger.Visible = false;
                            mnuItem_Payment.Visible = false;
                            mnuItem_Modify.Visible = true;
                            mnuItem_coverage.Visible = false;

                            mnuItem_Add.Visible = false;
                            mnuItem_RemoveRef.Visible = false;
                            mnuItem_ViewBenefits.Visible = false;

                            mnuItem_NewWCForm.Visible = false;
                            mnuItem_ModifyWCForm.Visible = false;


                            //Added By Pramod For Checking For Access Rights
                            mnuItem_Modify.Enabled = oClsgloUserRights.Appointment;

                            ToolStripMenuItem mnuItem_ChkIn_temp;
                            mnuItem_ChkIn_temp = Get_AssociationTemplates(gloOffice.AssociationCategories.CheckIn);
                            mnuItem_ChkIn_temp.Text = "Check In Templates";
                            mnuItem_ChkIn_temp.ForeColor = Color.FromArgb(31, 73, 125);
                            mnuItem_ChkIn_temp.Image = imgList_ApptPrint.Images[0];
                            cmnu_PatientDetails.Items.Add(mnuItem_ChkIn_temp);

                            ToolStripMenuItem mnuItem_ApptLetter_temp;
                            mnuItem_ApptLetter_temp = Get_AssociationTemplates(gloOffice.AssociationCategories.AppointmentLetters);
                            mnuItem_ApptLetter_temp.Text = "Appointment Letters Templates";
                            mnuItem_ApptLetter_temp.ForeColor = Color.FromArgb(31, 73, 125);
                            mnuItem_ApptLetter_temp.Image = imgList_ApptPrint.Images[1];
                            cmnu_PatientDetails.Items.Add(mnuItem_ApptLetter_temp);

                            ToolStripMenuItem mnuItem_ViewApptHistory;
                            mnuItem_ViewApptHistory = Get_AssociationTemplates(gloOffice.AssociationCategories.ViewApptHistory);
                            mnuItem_ViewApptHistory.Text = "View History";
                            mnuItem_ViewApptHistory.ForeColor = Color.FromArgb(31, 73, 125);
                            mnuItem_ViewApptHistory.Image = imgList_ApptPrint.Images[5];
                            cmnu_PatientDetails.Items.Add(mnuItem_ViewApptHistory);
                            mnuItem_ViewApptHistory.Click += new System.EventHandler(this.mnuItem_ViewApptHistory_Click);
                        }
                        else if ((_SelcetedPatient == PatientDetails.PriorAuthorization) && c1PatientDetails.Rows[tempRow].Index > 0)
                        {
                            c1PatientDetails.RowSel = tempRow;
                            c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                            mnuItem_Authorization.Visible = false;
                            mnuItem_eligibilityCheck.Visible = false;
                            mnuItem_ModifyCharges.Visible = false;
                            mnuItem_eligibilityCheckTest.Visible = false;
                            mnuItem_copay.Visible = false;
                            mnuItem_Ledger.Visible = false;
                            mnuItem_Payment.Visible = false;
                            mnuItem_Modify.Visible = true;
                            mnuItem_coverage.Visible = false;

                            mnuItem_Add.Visible = false;
                            mnuItem_RemoveRef.Visible = false;
                            mnuItem_ViewBenefits.Visible = false;

                            mnuItem_NewWCForm.Visible = false;
                            mnuItem_ModifyWCForm.Visible = false;

                            //Added By Pramod For Checking For Access Rights
                            mnuItem_Modify.Enabled = oClsgloUserRights.Appointment;
                        }
                        else if (_SelcetedPatient == PatientDetails.PatientCases)
                        {
                            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
                            {
                                return;

                            }
                            c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                            c1PatientDetails.Row = tempRow;
                            mnuItem_Add.Visible = true;
                            mnuItem_Authorization.Visible = false;
                            mnuItem_ModifyCharges.Visible = false;
                            mnuItem_eligibilityCheck.Visible = false;
                            mnuItem_eligibilityCheckTest.Visible = false;
                            mnuItem_copay.Visible = false;
                            mnuItem_Ledger.Visible = false;
                            mnuItem_Payment.Visible = false;
                            mnuItem_coverage.Visible = false;
                            mnuItem_RemoveRef.Visible = false;
                            if (c1PatientDetails.Rows[tempRow].Index > 0)
                                mnuItem_Modify.Visible = true;
                            else
                                mnuItem_Modify.Visible = false;
                            mnuItem_ViewBenefits.Visible = false;

                            mnuItem_NewWCForm.Visible = false;
                            mnuItem_ModifyWCForm.Visible = false;

                        }
                        else if (_SelcetedPatient == PatientDetails.PatientTasks)
                        {
                            c1PatientDetails.ContextMenuStrip = null;
                            if (tempRow > 0)
                            {
                                gloTask ogloTask = new gloTask(gloPMGlobal.DatabaseConnectionString);
                                TaskAssign oTaskAssign = null; //= new TaskAssign(); //SLR: new is not neeed
                                Int64 tempTaskId = 0;
                                tempTaskId = Convert.ToInt64(c1PatientDetails.GetData(tempRow, Col_View_taskID));//153939276303256796 147733067786806363 172827699470486034
                                if (tempTaskId > 0)
                                {
                                    oTaskAssign = ogloTask.GetTaskAssign(tempTaskId, gloPMGlobal.UserID);
                                    if (oTaskAssign != null)
                                    {
                                        Int32 reqstatus = Convert.ToInt32(oTaskAssign.AcceptRejectHold);
                                        if (reqstatus == 3)
                                        {
                                            if (Convert.ToString(c1PatientDetails.GetData(tempRow, Col_View_Owner)).Trim().ToLower() != gloPMGlobal.UserName.Trim().ToLower())
                                            {
                                                c1PatientDetails.ContextMenuStrip = null;
                                                MessageBox.Show("You are not the owner of this task.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                c1PatientDetails.ContextMenuStrip = cm_Task;
                                                cmu_AcceptTask.Visible = true;
                                                cmu_DeclineTask.Visible = true;
                                                cmu_MarkCompleted.Visible = false;
                                                cmu_FollowUp.Visible = false;
                                                cmu_Delete.Visible = false;
                                                cmu_Complete.Visible = false;
                                                cmu_OpenTask.Visible = false;
                                                cmu_Priority.Visible = false;

                                                mnuItem_NewWCForm.Visible = false;
                                                mnuItem_ModifyWCForm.Visible = false;

                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToString(c1PatientDetails.GetData(tempRow, Col_View_Owner)).Trim().ToLower() != gloPMGlobal.UserName.Trim().ToLower())
                                            {
                                                c1PatientDetails.ContextMenuStrip = null;
                                                MessageBox.Show("You are not the owner of this task.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                c1PatientDetails.ContextMenuStrip = cm_Task;
                                                cmu_AcceptTask.Visible = false;
                                                cmu_DeclineTask.Visible = false;
                                                cmu_MarkCompleted.Visible = true;
                                                cmu_FollowUp.Visible = true;
                                                cmu_Delete.Visible = true;
                                                cmu_Complete.Visible = true;
                                                cmu_OpenTask.Visible = true;
                                                cmu_Priority.Visible = true;

                                                mnuItem_NewWCForm.Visible = false;
                                                mnuItem_ModifyWCForm.Visible = false;


                                                FillPriorityMenu();
                                                FillFolowupMenu();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("You are not the owner of this task.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }//if (tempTaskId > 0)
                                else
                                {
                                    c1PatientDetails.ContextMenuStrip = null;
                                }
                                //SLR: Free oglotask, oTasksAssing
                                if (ogloTask != null)
                                {
                                    ogloTask.Dispose();
                                }
                                if (oTaskAssign != null)
                                {
                                    oTaskAssign.Dispose();
                                }
                            }
                        }

                        else if ((_SelcetedPatient == PatientDetails.PatientReferrals))
                        {
                            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
                            {
                                return;

                            }
                            c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                            c1PatientDetails.Row = tempRow;
                            if (c1PatientDetails.Rows[tempRow].Index > 0)
                            {

                                mnuItem_Authorization.Visible = false;
                                mnuItem_eligibilityCheck.Visible = false;
                                mnuItem_eligibilityCheckTest.Visible = false;
                                mnuItem_copay.Visible = false;
                                mnuItem_Ledger.Visible = false;
                                mnuItem_ModifyCharges.Visible = false;
                                mnuItem_Payment.Visible = false;
                                mnuItem_coverage.Visible = false;

                                //Commented By Pramod Nair For Disabling the Context menus if No Rights
                                //mnuItem_Modify.Visible = true;
                                //mnuItem_AddRef.Visible = true;
                                //mnuItem_RemoveRef.Visible = true;

                                //Added By Pramod Nair For Checking for user Rights

                                mnuItem_Modify.Visible = true;
                                mnuItem_Add.Visible = true;
                                mnuItem_RemoveRef.Visible = true;

                                mnuItem_NewWCForm.Visible = false;
                                mnuItem_ModifyWCForm.Visible = false;

                                mnuItem_Modify.Enabled = oClsgloUserRights.Contacts_Physician;
                                mnuItem_Add.Enabled = oClsgloUserRights.ModifyPatient;
                                mnuItem_RemoveRef.Enabled = oClsgloUserRights.ModifyPatient;
                                mnuItem_ViewBenefits.Visible = false;
                            }
                            else if (c1PatientDetails.Rows[tempRow].Index == 0)
                            {
                                mnuItem_Authorization.Visible = false;
                                mnuItem_eligibilityCheck.Visible = false;
                                mnuItem_eligibilityCheckTest.Visible = false;
                                mnuItem_copay.Visible = false;
                                mnuItem_Ledger.Visible = false;
                                mnuItem_ModifyCharges.Visible = false;
                                mnuItem_Payment.Visible = false;
                                mnuItem_coverage.Visible = false;
                                mnuItem_Modify.Visible = false;
                                mnuItem_Add.Visible = true;
                                mnuItem_RemoveRef.Visible = false;
                                mnuItem_ViewBenefits.Visible = false;

                                mnuItem_NewWCForm.Visible = false;
                                mnuItem_ModifyWCForm.Visible = false;


                                //Added By Pramod For Checking For Access Rights
                                mnuItem_Add.Enabled = oClsgloUserRights.ModifyPatient;
                            }
                            else if (c1PatientDetails.Rows.Count <= 1)
                            {

                                mnuItem_Authorization.Visible = false;
                                mnuItem_eligibilityCheck.Visible = false;
                                mnuItem_eligibilityCheckTest.Visible = false;
                                mnuItem_copay.Visible = false;
                                mnuItem_Ledger.Visible = false;
                                mnuItem_ModifyCharges.Visible = false;
                                mnuItem_Payment.Visible = false;
                                mnuItem_coverage.Visible = false;
                                mnuItem_Modify.Visible = false;
                                mnuItem_Add.Visible = true;
                                mnuItem_RemoveRef.Visible = false;
                                mnuItem_ViewBenefits.Visible = false;

                                mnuItem_NewWCForm.Visible = false;
                                mnuItem_ModifyWCForm.Visible = false;

                                //Added By Pramod For Checking For Access Rights
                                mnuItem_Add.Enabled = oClsgloUserRights.ModifyPatient;


                            }
                        }
                        else if ((_SelcetedPatient == PatientDetails.PatientNYWorkersCompForms))
                        {
                            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
                            {
                                return;

                            }
                            c1PatientDetails.ContextMenuStrip = cmnu_PatientDetails;
                            c1PatientDetails.Row = tempRow;
                            if (c1PatientDetails.Rows[tempRow].Index > 0)
                            {

                                mnuItem_Authorization.Visible = false;
                                mnuItem_eligibilityCheck.Visible = false;
                                mnuItem_eligibilityCheckTest.Visible = false;
                                mnuItem_copay.Visible = false;
                                mnuItem_Ledger.Visible = false;
                                mnuItem_ModifyCharges.Visible = false;
                                mnuItem_Payment.Visible = false;
                                mnuItem_coverage.Visible = false;

                                //Commented By Pramod Nair For Disabling the Context menus if No Rights
                                //mnuItem_Modify.Visible = true;
                                //mnuItem_AddRef.Visible = true;
                                //mnuItem_RemoveRef.Visible = true;

                                //Added By Pramod Nair For Checking for user Rights

                                mnuItem_Modify.Visible = false;
                                mnuItem_Add.Visible = false;
                                mnuItem_RemoveRef.Visible = false;
                                mnuItem_NewWCForm.Visible = false;
                                mnuItem_ModifyWCForm.Visible = true;

                                mnuItem_Modify.Enabled = oClsgloUserRights.Contacts_Physician;
                                mnuItem_Add.Enabled = oClsgloUserRights.ModifyPatient;
                                mnuItem_RemoveRef.Enabled = oClsgloUserRights.ModifyPatient;
                                mnuItem_ViewBenefits.Visible = false;
                            }
                            else if (c1PatientDetails.Rows[tempRow].Index == 0)
                            {
                                mnuItem_Authorization.Visible = false;
                                mnuItem_eligibilityCheck.Visible = false;
                                mnuItem_eligibilityCheckTest.Visible = false;
                                mnuItem_copay.Visible = false;
                                mnuItem_Ledger.Visible = false;
                                mnuItem_ModifyCharges.Visible = false;
                                mnuItem_Payment.Visible = false;
                                mnuItem_coverage.Visible = false;
                                mnuItem_Modify.Visible = false;
                                mnuItem_Add.Visible = false;
                                mnuItem_RemoveRef.Visible = false;
                                mnuItem_ViewBenefits.Visible = false;
                                mnuItem_NewWCForm.Visible = true;
                                mnuItem_ModifyWCForm.Visible = false;

                                //Added By Pramod For Checking For Access Rights
                                mnuItem_Add.Enabled = oClsgloUserRights.ModifyPatient;
                            }
                            else if (c1PatientDetails.Rows.Count <= 1)
                            {

                                mnuItem_Authorization.Visible = false;
                                mnuItem_eligibilityCheck.Visible = false;
                                mnuItem_eligibilityCheckTest.Visible = false;
                                mnuItem_copay.Visible = false;
                                mnuItem_Ledger.Visible = false;
                                mnuItem_ModifyCharges.Visible = false;
                                mnuItem_Payment.Visible = false;
                                mnuItem_coverage.Visible = false;
                                mnuItem_Modify.Visible = false;
                                mnuItem_Add.Visible = false;
                                mnuItem_RemoveRef.Visible = false;
                                mnuItem_ViewBenefits.Visible = false;
                                mnuItem_NewWCForm.Visible = true;
                                mnuItem_ModifyWCForm.Visible = false;

                                //Added By Pramod For Checking For Access Rights
                                mnuItem_Add.Enabled = oClsgloUserRights.ModifyPatient;


                            }
                        }

                        else
                        {
                            c1PatientDetails.ContextMenuStrip = null;

                        }
                    }
                    else
                    {
                        c1PatientDetails.ContextMenuStrip = null;
                    }
                }//if (c1ViewTask.HitTest(e.X, e.Y).Row > 1)
                else
                {
                    c1PatientDetails.ContextMenuStrip = null;
                }

                //MaheshB
                mnuItem_coverage.Visible = false;
                mnuItem_Payment.Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmu_MarkCompleted_Click(object sender, EventArgs e)
        {
            gloTask ogloTask = new gloTask(gloPMGlobal.DatabaseConnectionString);
            Int64 _taskId = 0;
            try
            {
                if (c1PatientDetails != null && c1PatientDetails.Rows.Count > 1)
                {
                    if (c1PatientDetails.RowSel > 0)
                    {
                        _taskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));
                        if (_taskId > 0)
                        {
                            ogloTask.ModifyTaskComplete(_taskId, 100);
                            FillPatientTasks(_CurrentPatientId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                //SLR: Free ogloTask
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                }
            }
        }

        private void cmu_Delete_Click(object sender, EventArgs e)
        {
            //Int64 id = 0;
            gloTask ogloTask = new gloTask(gloPMGlobal.DatabaseConnectionString);
            DataTable dt = null; //SLR: new is not needed
            try
            {
                //Added By MaheshB 
                if (c1PatientDetails.Rows.Count <= 1)
                {
                    return;
                }
                DialogResult dr = MessageBox.Show("Are you sure you want to delete task ?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    Int64 tempTaskId = 0;
                    // Int64 assignToId = 0;

                    if (c1PatientDetails != null && c1PatientDetails.Rows.Count > 1)
                    {
                        if (c1PatientDetails.RowSel > 0)
                        {
                            tempTaskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));
                            if (ogloTask.CanDeleteTask(tempTaskId) == true)
                            {
                                ogloTask.DeleteRequestedTask(tempTaskId, gloPMGlobal.UserID);
                                dt = ogloTask.get_multiTask(tempTaskId);
                                if (dt.Rows.Count == 0)
                                {
                                    ogloTask.DeleteTask(tempTaskId);
                                }

                                FillPatientTasks(_CurrentPatientId);
                            }
                            else
                            {
                                MessageBox.Show("Cannot delete message . The message is assigned and is on hold", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                //SLR: Finally free oglotask, dt
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        private void cmu_OpenTask_Click(object sender, EventArgs e)
        {
            Int64 tempTaskId = 0;
            try
            {
                if (c1PatientDetails.Rows.Count <= 1)
                {
                    return;
                }
                tempTaskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));
                gloTaskMail.frmTask ofrmTask = new gloTaskMail.frmTask(gloPMGlobal.DatabaseConnectionString, tempTaskId);
                ofrmTask.OnPatientPaymentClicked += new frmTask.PatientPaymentHandler(OnPatientPaymentClicked);
                ofrmTask.IsEMREnable = true;
                ofrmTask.IsOpenfromView = true;
                ofrmTask.ShowDialog(this);
                //SLR: Free event handler and then
                ofrmTask.OnPatientPaymentClicked -= new frmTask.PatientPaymentHandler(OnPatientPaymentClicked);
                ofrmTask.Dispose();
                FillPatientTasks(_CurrentPatientId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
            }
        }

        private void cmu_AcceptTask_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Accept selected task requests?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }//If yes then proceed.


            gloTask ogloTask = new gloTask(gloPMGlobal.DatabaseConnectionString);
            Int64 tempTaskId = 0;
            Task oTask = null; // = new Task(); //SLR: new not needed
            TaskAssign oTaskAssign = null; // = new TaskAssign(); //SLR: new not needed

            try
            {
                tempTaskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));//TaskID
                oTask = ogloTask.GetTask(tempTaskId);

                Int64 _nAssignFromId = 0;
                oTaskAssign = ogloTask.GetTaskAssign(tempTaskId, gloPMGlobal.UserID);
                if (oTaskAssign != null)
                {
                    _nAssignFromId = oTaskAssign.AssignFromID;
                }
                if (oTask != null)
                {
                    //Clear the previous assignments for this Task
                    oTask.Assignment.Clear();

                    oTask.TaskID = 0;
                    oTask.UserID = gloPMGlobal.UserID;
                    oTask.OwnerID = gloPMGlobal.UserID;


                    oTaskAssign.TaskID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));//TaskID;
                    oTaskAssign.AssignToID = gloPMGlobal.UserID;//AssignTo ID;
                    oTaskAssign.AssignFromID = _nAssignFromId;//AssignFrom  ID;
                    oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self;
                    oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept;
                    oTask.Assignment.Add(oTaskAssign);
                }

                Int64 _result = ogloTask.Add(oTask);

                if (_result > 0)
                {
                    if (ogloTask.AcceptTask(tempTaskId, gloPMGlobal.UserID))
                    {
                        //--------Saket 
                        //Activate Reminder for this Task if reminder is present 
                        gloReminder.Reminder oReminder = new gloReminder.Reminder();
                        oReminder.ActivateTaskReminder(gloPMGlobal.UserID, tempTaskId, _result);
                        //-------------
                        //SLR: Free oReminder
                        oReminder.Dispose();
                    }
                }

                else
                {
                    MessageBox.Show("ERROR : Record not added.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                FillPatientTasks(_CurrentPatientId);
                //design_c1Task();
                //fill_c1Task();      
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            finally
            {
                ogloTask.Dispose();
                oTask.Dispose();
                oTaskAssign.Dispose();

            }
        }

        private void cmu_DeclineTask_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show("Decline selected task requests?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            gloTask ogloTask = new gloTask(gloPMGlobal.DatabaseConnectionString);
            Int64 tempTaskId = 0;
            try
            {

                tempTaskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));//TaskID
                //Decline Task Request if selected for decline
                if (!ogloTask.DeclineTask(tempTaskId, gloPMGlobal.UserID))
                {
                    MessageBox.Show("Error : Decline unsuccessful.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                FillPatientTasks(_CurrentPatientId);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (ogloTask != null)
                {
                    ogloTask.Dispose();
                }
            }

        }

        private void smn_Zero_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _complete = -1;
            Int64 _taskId = 0;
            Int64 _statusId = 0;

            try
            {
                if (((ToolStripMenuItem)sender).Tag != null && ((ToolStripMenuItem)sender).Tag.ToString() != "")
                {
                    _complete = Convert.ToInt64(((ToolStripMenuItem)sender).Tag.ToString());
                    if (_complete != -1)
                    {
                        if (c1PatientDetails != null && c1PatientDetails.Rows.Count > 1)
                        {
                            if (c1PatientDetails.RowSel > 0)
                            {
                                _taskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));
                                if (_taskId > 0)
                                {
                                    if (_complete == 0)
                                    {
                                        _statusId = Convert.ToInt64(gloTaskMail.frmTask.StatusType.NotStarted.GetHashCode());
                                    }
                                    else if (_complete > 0 && _complete < 100)
                                    {
                                        _statusId = Convert.ToInt64(gloTaskMail.frmTask.StatusType.InProgress.GetHashCode());
                                    }
                                    else if (_complete == 100)
                                    {
                                        _statusId = Convert.ToInt64(gloTaskMail.frmTask.StatusType.Completed.GetHashCode());
                                    }

                                    oDB.Connect(false);
                                    _sqlQuery = "UPDATE TM_Task_Progress SET dComplete = " + _complete + ", nStatusID = " + _statusId + " where nTaskID = " + _taskId + "";
                                    oDB.Execute_Query(_sqlQuery);
                                    FillPatientTasks(_CurrentPatientId);
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
        }

        private void FillFolowupMenu()
        {
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(gloPMGlobal.DatabaseConnectionString);
            gloTasksMails.Common.Followups oFollowUps = null; // = new gloTasksMails.Common.Followups(); //SLR: new not needed
            //System.Drawing.Image img = null; //SLR: Remove this declaring and assign directly images. Else this will occupy one more memory space which is not needed

            try
            {
                //SLR: Before clearing one by one clear events, remove at , dispose and then                
                for (int i = cmu_FollowUp.DropDownItems.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        ToolStripItem oFolloupSubMenuItem = cmu_FollowUp.DropDownItems[i];
                        oFolloupSubMenuItem.Click -= new EventHandler(oFolloupSubMenuItem_Click);
                        cmu_FollowUp.DropDownItems.RemoveAt(i);
                        try
                        {
                            oFolloupSubMenuItem.Dispose();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }
                cmu_FollowUp.DropDownItems.Clear();

                oFollowUps = oTaskMail.GetFollowUps();

                if (oFollowUps != null && oFollowUps.Count > 0)
                {
                    for (int i = 0; i < oFollowUps.Count; i++)
                    {
                        ToolStripItem oFolloupSubMenuItem = new ToolStripMenuItem();
                        oFolloupSubMenuItem.Text = oFollowUps[i].Description;
                        oFolloupSubMenuItem.Name = oFollowUps[i].Description;
                        oFolloupSubMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                        //SLR: Create font only once....and free at end...
                        oFolloupSubMenuItem.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        oFolloupSubMenuItem.Tag = oFollowUps[i].ID;
                        if (oFollowUps[i].Description.ToUpper() == "TODAY")
                        {
                            System.Drawing.Image img = global::gloPM.Properties.Resources.Today;
                            oFolloupSubMenuItem.Image = img;
                        }
                        else if (oFollowUps[i].Description.ToUpper() == "TOMMOROW")
                        {
                            System.Drawing.Image img = global::gloPM.Properties.Resources.Tommorow;
                            oFolloupSubMenuItem.Image = img;
                        }
                        else if (oFollowUps[i].Description.ToUpper() == "NO DATE")
                        {
                            System.Drawing.Image img = global::gloPM.Properties.Resources.No_Date;
                            oFolloupSubMenuItem.Image = img;
                        }
                        else
                        {
                            System.Drawing.Image img = global::gloPM.Properties.Resources.Flag_Yellow;
                            oFolloupSubMenuItem.Image = img;
                        }
                        oFolloupSubMenuItem.Click += new EventHandler(oFolloupSubMenuItem_Click);

                        cmu_FollowUp.DropDownItems.Add(oFolloupSubMenuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oTaskMail != null) { oTaskMail.Dispose(); }
                if (oFollowUps != null) { oFollowUps.Dispose(); }
            }
        }

        private void FillPriorityMenu()
        {
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(gloPMGlobal.DatabaseConnectionString);
            gloTasksMails.Common.Priorities oPriorities = null; //= new gloTasksMails.Common.Priorities(); //SLR: new is not needed
            //System.Drawing.Image img = null; //SLR: Remove this declaring and assign directly images. Else this will occupy one more memory space which is not needed

            try
            {
                //SLR: before clearing, remove event, remove at and dispose
                //while (cmu_Priority.DropDownItems.Count > 0)
                //{
                //    cmu_Priority.DropDownItems[0].Dispose();
                //}

                for (int i = cmu_Priority.DropDownItems.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        ToolStripItem oPrioritySubMenuItem = cmu_Priority.DropDownItems[i];
                        oPrioritySubMenuItem.Click -= new EventHandler(oPrioritySubMenuItem_Click);
                        cmu_Priority.DropDownItems.RemoveAt(i);
                        try
                        {
                            oPrioritySubMenuItem.Dispose();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                    }
                }
                cmu_Priority.DropDownItems.Clear();
                oPriorities = oTaskMail.GetPriorities();
                if (oPriorities != null && oPriorities.Count > 0)
                {
                    for (int i = 0; i < oPriorities.Count; i++)
                    {
                        ToolStripItem oPrioritySubMenuItem = new ToolStripMenuItem();
                        oPrioritySubMenuItem.Text = oPriorities[i].Description;
                        oPrioritySubMenuItem.Name = oPriorities[i].Description;
                        oPrioritySubMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                        //SLR: Create font once.....and free at end......
                        oPrioritySubMenuItem.Font = Font_Regular; // new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        oPrioritySubMenuItem.Tag = oPriorities[i].ID;
                        if (oPriorities[i].PriorityLevel == 1)
                        {
                            System.Drawing.Image img = global::gloPM.Properties.Resources.High_PriorityRed;
                            oPrioritySubMenuItem.Image = img;
                            oPrioritySubMenuItem.ImageAlign = ContentAlignment.MiddleCenter;
                        }
                        else if (oPriorities[i].PriorityLevel == 2)
                        {
                            // img = global::gloTaskMail.Properties.Resources.Normal_Priority;
                            // oPrioritySubMenuItem.Image = img;
                            // oPrioritySubMenuItem.ImageAlign = ContentAlignment.MiddleCenter;
                        }
                        else if (oPriorities[i].PriorityLevel == 3)
                        {
                            System.Drawing.Image img = global::gloPM.Properties.Resources.Low_Priority;
                            oPrioritySubMenuItem.Image = img;
                            oPrioritySubMenuItem.ImageAlign = ContentAlignment.MiddleCenter;
                        }
                        oPrioritySubMenuItem.Click += new EventHandler(oPrioritySubMenuItem_Click);
                        cmu_Priority.DropDownItems.Add(oPrioritySubMenuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                //SLR: FRee otaskmail, oPriorities
                if (oTaskMail != null)
                {
                    oTaskMail.Dispose();
                }
                if (oPriorities != null)
                {
                    oPriorities.Dispose();
                }
            }
        }

        void oPrioritySubMenuItem_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            Int64 _priorityId = 0;
            Int64 _taskId = 0;
            string _sqlQuery = "";

            try
            {
                if (((ToolStripMenuItem)sender).Tag != null && ((ToolStripMenuItem)sender).Tag.ToString() != "")
                {
                    _priorityId = Convert.ToInt64(((ToolStripMenuItem)sender).Tag.ToString());
                    if (_priorityId > 0)
                    {
                        if (c1PatientDetails != null && c1PatientDetails.Rows.Count > 1)
                        {
                            if (c1PatientDetails.RowSel > 0)
                            {
                                _taskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));
                                if (_taskId > 0)
                                {
                                    oDB.Connect(false);
                                    _sqlQuery = "UPDATE TM_TaskMST SET nPriorityID = " + _priorityId + " WHERE nTaskID = " + _taskId + "";
                                    oDB.Execute_Query(_sqlQuery);
                                    //design_c1Task();
                                    FillPatientTasks(_CurrentPatientId);
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        void oFolloupSubMenuItem_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            Int64 _followupId = 0;
            Int64 _taskId = 0;
            string _sqlQuery = "";

            try
            {
                if (((ToolStripMenuItem)sender).Tag != null && ((ToolStripMenuItem)sender).Tag.ToString() != "")
                {
                    _followupId = Convert.ToInt64(((ToolStripMenuItem)sender).Tag.ToString());
                    if (_followupId > 0)
                    {
                        if (c1PatientDetails != null && c1PatientDetails.Rows.Count > 0)
                        {
                            if (c1PatientDetails.RowSel > 0)
                            {
                                _taskId = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, Col_View_taskID));
                                if (_taskId > 0)
                                {
                                    oDB.Connect(false);
                                    _sqlQuery = "UPDATE TM_TaskMST SET nFollowUpID = " + _followupId + " WHERE nTaskID = " + _taskId + " ";
                                    oDB.Execute_Query(_sqlQuery);
                                    //design_c1Task();
                                    FillPatientTasks(_CurrentPatientId);
                                    oDB.Disconnect();
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void c1PatientDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mnuItem_Modify_Click(null, null);
        }

        //**Context Menu Events of Patient DTL:
        private void mnuItem_copay_Click(object sender, EventArgs e)
        {
            try
            {

                gloAccountsV2.frmPatientPaymentV2 oPaymentInsurace = new gloAccountsV2.frmPatientPaymentV2(gloCntrlPatient.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Copay);
                oPaymentInsurace.StartPosition = FormStartPosition.CenterScreen;
                oPaymentInsurace.WindowState = FormWindowState.Maximized;
                oPaymentInsurace.ShowInTaskbar = false;
                oPaymentInsurace.ShowDialog(this);
                oPaymentInsurace.Dispose();
                FillAlerts();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void mnuItem_coverage_Click(object sender, EventArgs e)
        {
            try
            {
                //Code Changes made on 20090416 By - Sagar Ghodke
                //Code chages to combine Copay,AdvanePayment & Coverage functionality
                //Below commented code is existing 

                ////gloBilling.frmCoPay ofrmCoPay = new gloBilling.frmCoPay(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId, tempInsName, tempInsId, PaymentOtherType.InsuranceCoverage);
                //gloBilling.frmCoPay ofrmCoPay = new gloBilling.frmCoPay(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId, tempInsName, tempInsId, PaymentOtherType.InsuranceCoverage,coveragePercent);
                //ofrmCoPay.ShowDialog();

                if (_CurrentPatientId > 0)
                {
                    //gloBilling.frmAdvancePayment ofrmAdvancePayment = new frmAdvancePayment(gloPMGlobal.DatabaseConnectionString, gloCntrlPatient.PatientID, tempInsName, tempInsId, coveragePercent, PaymentOtherType.InsuranceCoverage);
                    //ofrmAdvancePayment.StartPosition = FormStartPosition.CenterScreen;
                    //ofrmAdvancePayment.ShowDialog();
                    //ofrmAdvancePayment.Dispose();
                }
                //End Code Changes 20090416,Sagar Ghodke

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

       public int SelectedRowsel=0;
        private void mnuItem_ModifyCharges_Click(object sender, EventArgs e)
        {
            Boolean _IsModified = false;


            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
            Int64 ParamTransactionId = 0;
            ParamTransactionId = GetClaimTransactionID(Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 2)), "", false);
            if (ParamTransactionId != 0)
                _IsModified = ogloBilling.ShowModifyCharges(_CurrentPatientId, ParamTransactionId, false, true, this);

            SelectedRowsel = c1PatientDetails.RowSel;
            if (_IsModified)
            {
                FillPatientBilling(_CurrentPatientId);
            }
            //SLR: FRee ogloBilling
            if (ogloBilling != null)
            {
                ogloBilling.Dispose();
            }
            
        }

        public long GetClaimTransactionID(Int64 _nClaimNo, string _subclaimNo, bool _isVoid)
        {
            #region "To Fetch the TransactionID of Claim"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable _dtTransID = null;
            gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBPatameters.Add("@nClaimno", _nClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@sSubClaimno", _subclaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBPatameters.Add("@bIsVoid", _isVoid, ParameterDirection.Input, SqlDbType.Bit);

                _dtTransID = null;
                oDB.Retrive("BL_Get_TransactionID_V2", oDBPatameters, out _dtTransID);
                if (_dtTransID != null && _dtTransID.Rows.Count > 0)
                {
                    return Convert.ToInt64(_dtTransID.Rows[0]["nTransactionID"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBPatameters != null)
                {
                    oDBPatameters.Dispose();
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (_dtTransID != null)
                {
                    _dtTransID.Dispose();
                }
            }
            #endregion
        }
        private void mnuItem_Ledger_Click(object sender, EventArgs e)
        {
            try
            {
                ////gloBilling.frmRpt_PatientLedger ofrmPatientLedger = new gloBilling.frmRpt_PatientLedger(_CurrentPatientId);
                ////ofrmPatientLedger.ShowDialog();
                ////ofrmPatientLedger.Dispose();
                ////ShowHideMainMenu(false, false);
                ////gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
                ////ogloBilling.ShowPatientLedger(gloCntrlPatient.PatientID, this);

                //frmEOBLedger ofrmEOBLedger = new frmEOBLedger(_CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
                //ShowHideMainMenu(false, false);
                //ofrmEOBLedger.MdiParent = this;
                //ofrmEOBLedger.WindowState = FormWindowState.Maximized;
                //ofrmEOBLedger.ShowInTaskbar = false;
                //ofrmEOBLedger.StartPosition = FormStartPosition.CenterParent;
                //ofrmEOBLedger.Show();

                gloAccountsV2.frmPatientFinancialViewV2 oPatientFinancialView = gloAccountsV2.frmPatientFinancialViewV2.GetInstance(gloCntrlPatient.PatientID);
                oPatientFinancialView.WindowState = FormWindowState.Maximized;
                oPatientFinancialView.MdiParent = this;
                oPatientFinancialView.ShowInTaskbar = false;
                oPatientFinancialView.StartPosition = FormStartPosition.CenterParent;
                oPatientFinancialView.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void mnuItem_Payment_Click(object sender, EventArgs e)
        {
            //if (c1PatientDetails.Rows.Count > 0)
            //{
            //    if (c1PatientDetails.RowSel > 0)
            //    {
            //        if (c1PatientDetails.GetData(c1PatientDetails.RowSel, 2) != null && c1PatientDetails.GetData(c1PatientDetails.RowSel, 2).ToString() != "")
            //        {
            //            Int64 _ClaimNo = 0;
            //            _ClaimNo = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 2).ToString());
            //            if (_ClaimNo > 0)
            //            {
            //                ShowHideMainMenu(false, false);
            //                gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
            //                ogloBilling.ShowBillingPayment(gloCntrlPatient.PatientID, 0, _ClaimNo, this);
            //            }
            //        }
            //    }
            //}
        }
        private void mnuItem_ViewApptHistory_Click(object sender, EventArgs e)
        {
            gloAppointmentScheduling.frmViewApptHistory ofrmViewAppointmentHistory = new gloAppointmentScheduling.frmViewApptHistory(gloPMGlobal.DatabaseConnectionString);
            ofrmViewAppointmentHistory = new gloAppointmentScheduling.frmViewApptHistory(gloPMGlobal.DatabaseConnectionString);
            ofrmViewAppointmentHistory.AppointmentID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.Row, 0).ToString());
            ofrmViewAppointmentHistory.ShowDialog(this);
            ofrmViewAppointmentHistory.Dispose();
        }
        private void mnuItem_Modify_Click(object sender, EventArgs e)
        {
            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
                return;
            try
            {
                //Added By Pramod Nair  For Checking the Access Rights
                if (gloPMGlobal.UserName.Trim() != "")
                {
                    //SLR: FRee before allocating once more memory
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(gloPMGlobal.DatabaseConnectionString);
                    oClsgloUserRights.CheckForUserRights(gloPMGlobal.UserName);
                }
                //End

                if (_SelcetedPatient == PatientDetails.PatientInsurance)
                {

                    //Added By Pramod Nair  For Checking the Access Rights
                    if (oClsgloUserRights.ModifyPatient)
                    {
                        bool _ReturnIsClose = false;
                        gloPatient.gloPatient oPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
                        oPatient.ShowPatientRegistration(gloCntrlPatient.PatientID, gloPatient.ModifyPatientDetailType.Insurance, out _CurrentPatientId, out _ReturnIsClose, this);
                        gloCntrlPatient.SelectedPatientID = _CurrentPatientId;

                        #region " HL7 "

                        if (appSettings["HL7SENDOUTBOUNDGLOPM"] != null && appSettings["SendPatientDetails"] != null)
                        {
                            if (appSettings["HL7SENDOUTBOUNDGLOPM"] != "" && appSettings["SendPatientDetails"] != "")
                            {
                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOPM"])) == true) && (Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                {
                                    if (_ReturnIsClose == false)
                                    {
                                        InsertInMessageQueue("A08", _CurrentPatientId, _CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
                                    }
                                }
                            }
                        }

                        #endregion

                        gloCntrlPatient.FillPatients();
                        FillSelectedPatient();
                        gloCntrlPatient.Refresh();
                        gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
                        oPatient.Dispose();
                        //isBadDebtPatient(_CurrentPatientId, false);
                        FillPatientInsurance(_CurrentPatientId);
                    }

                }
                else if (_SelcetedPatient == PatientDetails.PatientAppointments)
                {

                    #region " Column Constants "

                    // const int COL_MSTAPTID = 0;
                    // const int COL_APTDTL = 1;
                    // const int COL_DATE = 2;
                    // const int COL_STARTTIME = 3;
                    ////const int COL_ENDTIME = 4;
                    // const int COL_LOCATION = 5;
                    // const int COL_DEPARTMENT = 6;
                    // const int COL_ASBASEID = 7;
                    ////const int COL_APPTYPE = 8;
                    // //const int COL_NOTES = 9;
                    // const int COL_PROVIDER = 10;
                    // const int COL_USEDSTATUS = 11;
                    //// const int COL_MASTERAPPMETHOD = 12;
                    // const int COL_APPMETHOD = 13;
                    // const int COL_LINENUMBER = 14;
                    //const int COL_CLINICID = 15;
                    //Added on 20110317
                    const int COL_MSTAPTID = 0;
                    const int COL_APTDTL = 1;
                    const int COL_DATE = 3;
                    const int COL_STARTTIME = 4;
                    //const int COL_ENDTIME = 5;
                    const int COL_LOCATION = 6;
                    const int COL_DEPARTMENT = 7;
                    const int COL_ASBASEID = 8;
                    //const int COL_APPTYPE = 9;
                    //const int COL_NOTES = 10;
                    const int COL_PROVIDER = 11;
                    const int COL_USEDSTATUS = 12;
                    // const int COL_MASTERAPPMETHOD = 13;
                    const int COL_APPMETHOD = 14;
                    const int COL_LINENUMBER = 15;
                    //const int COL_CLINICID = 15;

                    //End 20110317

                    #endregion " Column Constants "

                    // GLO2011-0011970
                    // If patient status is legal pending then don't allow him to create / modify appointment
                    using (gloPatient.gloPatient objPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString))
                    {
                        if (objPatient.IsLegalPending(gloCntrlPatient.PatientID))
                        {
                            MessageBox.Show("The status of the patient is 'Legal Pending'." + Environment.NewLine + " You can not modify an appointment for this patient.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    gloAppointmentScheduling.SetAppointmentParameter oAppParameters = new gloAppointmentScheduling.SetAppointmentParameter();
                    gloAppointmentScheduling.frmSetupAppointment oSetupAppointment = new gloAppointmentScheduling.frmSetupAppointment(gloPMGlobal.DatabaseConnectionString);

                    if (Convert.ToDateTime(c1PatientDetails.GetData(c1PatientDetails.Row, COL_DATE)).Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Cannot modify past appointment.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        oAppParameters.Dispose();
                        oAppParameters = null;
                        oSetupAppointment.Dispose();
                        oSetupAppointment = null;
                        return;
                    }


                    gloAppointmentScheduling.ASUsedStatus _AppStatus = gloAppointmentScheduling.ASUsedStatus.Unknown5;
                    _AppStatus = (gloAppointmentScheduling.ASUsedStatus)Convert.ToInt32(c1PatientDetails.GetData(c1PatientDetails.Row, COL_USEDSTATUS));

                    if (_AppStatus == gloAppointmentScheduling.ASUsedStatus.Cancel)
                    {
                        if (MessageBox.Show("This appointment is cancelled. Do you want to modify the appointment note?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            oAppParameters.Dispose();
                            oAppParameters = null;
                            oSetupAppointment.Dispose();
                            oSetupAppointment = null;
                            return;
                        }
                    }

                    if (_AppStatus == gloAppointmentScheduling.ASUsedStatus.NoShow)
                    {
                        if (MessageBox.Show("The status of this appointment is 'No Show'. Do you want to modify the appointment note?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            oAppParameters.Dispose();
                            oAppParameters = null;
                            oSetupAppointment.Dispose();
                            oSetupAppointment = null;
                            return;
                        }
                    }


                    if (_AppStatus == gloAppointmentScheduling.ASUsedStatus.CheckOut)
                    {
                        //MessageBox.Show("Patient is checked-out.  Cannot modify this appointment.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //ADDED BY SHUBHANGI 20110119
                        gloPatient.gloPatient oPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
                        string _PatientName = oPatient.GetPatientName(_CurrentPatientId);
                        //SLR: Free oPatient,
                        oPatient.Dispose();
                        DialogResult dlg = MessageBox.Show("Patient " + _PatientName + "  is already checked out.  Are you sure you wish to modify this appointment?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dlg == DialogResult.No)
                        {
                            oAppParameters.Dispose();
                            oAppParameters = null;
                            oSetupAppointment.Dispose();
                            oSetupAppointment = null;
                            return;
                        }
                        else if (dlg == DialogResult.Cancel)
                        {
                            oAppParameters.Dispose();
                            oAppParameters = null;
                            oSetupAppointment.Dispose();
                            oSetupAppointment = null;
                            return;
                        }

                    }
                    //else if (_AppStatus == gloAppointmentScheduling.ASUsedStatus.CheckIn)
                    //{
                    //    MessageBox.Show("Patient is checked-in.  Cannot modify this appointment.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    else if (_AppStatus == gloAppointmentScheduling.ASUsedStatus.Delete)
                    {
                        MessageBox.Show("Cannot modify this appointment. Appointment is deleted ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        oAppParameters.Dispose();
                        oAppParameters = null;
                        oSetupAppointment.Dispose();
                        oSetupAppointment = null;
                        return;
                    }



                    //Added By Pramod Nair  For Checking the Access Rights
                    if (oClsgloUserRights.Appointment)
                    {
                        #region "Find for Single/Recurrence Option"
                        gloAppointmentScheduling.SingleRecurrence _SinRecCriteria = gloAppointmentScheduling.SingleRecurrence.Single;
                        _SinRecCriteria = (gloAppointmentScheduling.SingleRecurrence)c1PatientDetails.GetData(c1PatientDetails.Row, COL_APPMETHOD);
                        //SelectedForModDel

                        oAppParameters.AddTrue_ModifyFalse_Flag = false;
                        if (_SinRecCriteria == gloAppointmentScheduling.SingleRecurrence.Single)
                        {
                            oAppParameters.ModifyAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.Single;
                            oAppParameters.ModifyMasterAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.Single;
                            oAppParameters.ModifySingleAppointmentFromReccurence = false;
                        }
                        else
                        {

                            oAppParameters.ModifyAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.Single;
                            oAppParameters.ModifyMasterAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.Recurrence;
                            oAppParameters.ModifySingleAppointmentFromReccurence = true;

                        }
                        #endregion

                        #region "Set Appointment Parameter"
                        oAppParameters.MasterAppointmentID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.Row, COL_MSTAPTID));
                        oAppParameters.AppointmentID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.Row, COL_APTDTL));
                        oAppParameters.AppointmentFlag = gloAppointmentScheduling.AppointmentScheduleFlag.Appointment;
                        oAppParameters.AppointmentTypeID = 0;
                        oAppParameters.AppointmentTypeCode = "";
                        oAppParameters.AppointmentTypeDesc = "";
                        oAppParameters.ProviderID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.Row, COL_ASBASEID));
                        oAppParameters.ProviderName = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.Row, COL_PROVIDER));
                        oAppParameters.ProblemTypes = null;
                        oAppParameters.Resources = null;
                        oAppParameters.PatientID = 0;
                        #region "Setup as per selection criteria in above method"
                        //oAppParameters.AddTrue_ModifyFalse_Flag = true;
                        //oAppParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                        //oAppParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                        //oAppParameters.ModifySingleAppointmentFromReccurence = false;
                        #endregion
                        oAppParameters.Location = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.Row, COL_LOCATION));
                        oAppParameters.Department = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.Row, COL_DEPARTMENT));
                        oAppParameters.StartDate = Convert.ToDateTime(c1PatientDetails.GetData(c1PatientDetails.Row, COL_DATE));
                        oAppParameters.StartTime = Convert.ToDateTime(c1PatientDetails.GetData(c1PatientDetails.Row, COL_STARTTIME));
                        oAppParameters.Duration = 0;// Convert.ToDateTime(c1PatientDetails.GetData(c1PatientDetails.Row, COL_ENDTIME)).Subtract(Convert.ToDateTime(c1PatientDetails.GetData(c1PatientDetails.Row, COL_STARTTIME)));
                        oAppParameters.ClinicID = gloPMGlobal.ClinicID;
                        oAppParameters.LineNumber = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.Row, COL_LINENUMBER));
                        oAppParameters.LoadParameters = false;
                        oAppParameters.Usedstatus = (Int64)_AppStatus;

                        #endregion

                        oSetupAppointment.SetAppointmentParameters = oAppParameters;
                        oSetupAppointment.MasterAppointmentId = oAppParameters.MasterAppointmentID;
                        oSetupAppointment.DetailAppointmentId = oAppParameters.AppointmentID;
                        oSetupAppointment.ShowDialog(this);
                        //SLR: Free oAppParameters
                        if (oAppParameters != null)
                        {
                            oAppParameters.Dispose();
                        }
                        //SLR: FRee oSetupAppointment
                        if (oSetupAppointment != null)
                        {
                            oSetupAppointment.Dispose();
                        }
                        FillPatientAppointments(_CurrentPatientId);
                        //ADDED BY SHUBHANGI 20110119
                        FillPatientStatus();
                    }
                }
                else if (_SelcetedPatient == PatientDetails.PatientReferrals)
                {
                    Int64 _contactID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 9));
                    frmSetupPhysician ofrmModifyContact = new frmSetupPhysician(_contactID, gloPMGlobal.DatabaseConnectionString);
                    ofrmModifyContact.ShowDialog(this);
                    ofrmModifyContact.Dispose();
                    FillPatientReferrals(_CurrentPatientId);

                }
                else if (_SelcetedPatient == PatientDetails.PriorAuthorization)
                {
                    Int64 _SelectedAuthID = 0;
                    if (c1PatientDetails.RowSel > 0)
                    {
                        _SelectedAuthID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 0));

                        gloPMGeneral.frmModifyAuthorization OBJPrior = new gloPMGeneral.frmModifyAuthorization(gloPMGlobal.DatabaseConnectionString, _SelectedAuthID);
                        OBJPrior.ShowDialog(this);
                        OBJPrior.BringToFront();
                        //SLR: Dispose ObjPrior
                        OBJPrior.Dispose();
                        tsb_PD_PriorAuthorization_Click(null, null);
                    }
                }
                else if (_SelcetedPatient == PatientDetails.PatientCases)
                {
                    Int64 _SelectedCaseID = 0;
                    if (c1PatientDetails.RowSel > 0)
                    {
                        _SelectedCaseID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 0));
                        frmSetupCases OfrmSetupCases = new frmSetupCases(_CurrentPatientId, _SelectedCaseID);
                        OfrmSetupCases.ShowDialog(this);
                        //SLR: Dispose ofrmsetupcases
                        OfrmSetupCases.Dispose();
                        FillCases(_CurrentPatientId);
                    }
                }
                else if (_SelcetedPatient == PatientDetails.PatientTasks)
                {
                    c1PatientDetails.ContextMenuStrip = null;
                    if (c1PatientDetails.RowSel > 0)
                    {
                        gloTask ogloTask = new gloTask(gloPMGlobal.DatabaseConnectionString);
                        TaskAssign oTaskAssign = null; //= new TaskAssign(); //SLR: new is not needed
                        Int64 tempTaskId = 0;
                        int tempRow = c1PatientDetails.RowSel;
                        tempTaskId = Convert.ToInt64(c1PatientDetails.GetData(tempRow, Col_View_taskID));
                        if (tempTaskId > 0)
                        {
                            oTaskAssign = ogloTask.GetTaskAssign(tempTaskId, gloPMGlobal.UserID);
                            if (oTaskAssign != null)
                            {
                                Int32 reqstatus = Convert.ToInt32(oTaskAssign.AcceptRejectHold);
                                if (reqstatus == 3)
                                {
                                    if (Convert.ToString(c1PatientDetails.GetData(tempRow, Col_View_Owner)).Trim().ToLower() == gloPMGlobal.UserName.Trim().ToLower())
                                    {
                                        cmu_OpenTask_Click(null, null);
                                    }
                                    else
                                    {
                                        MessageBox.Show("You are not the owner of this task.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    if (Convert.ToString(c1PatientDetails.GetData(tempRow, Col_View_Owner)).Trim().ToLower() == gloPMGlobal.UserName.Trim().ToLower())
                                    {
                                        cmu_OpenTask_Click(null, null);
                                    }
                                    else
                                    {
                                        MessageBox.Show("You are not the owner of this task.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("You are not the owner of this task.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }//if (tempTaskId > 0) 
                        if (ogloTask != null)
                        {
                            ogloTask.Dispose();
                        }
                        if (oTaskAssign != null)
                        {
                            oTaskAssign.Dispose();
                        }
                    }
                }
                else if (_SelcetedPatient == PatientDetails.PatientDocuments)
                {
                    if (c1PatientDetails.RowSel > 0)
                    {
                        ShowDMS(true, true, true, true);
                    }

                }
                else if (_SelcetedPatient == PatientDetails.PatientNYWorkersCompForms)
                {
                    //frmWorkerCompFormViewer frmWorkerComp_C4 = null;
                    //frmWorkerCompFormViewer_C42 frmWorkerComp_C42 = null;

                    try
                    {
                        Int64 _SelectedTransactionID = 0;
                        Int64 _SelectedTransactionmasterID = 0;
                        Int64 _SelectedFormID = 0;
                        Int64 _SelectedFormType = 0;

                        if (c1PatientDetails.RowSel > 0)
                        {
                            _SelectedTransactionID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 13));
                            _SelectedTransactionmasterID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 12));
                            _SelectedFormID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 0));

                            _SelectedFormType = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 15));

                            if (_SelectedFormType == (Int64)WorkersCompFormTypes.C4)
                            {
                                using (frmWorkerCompFormViewer frmWorkerComp_C4 = new frmWorkerCompFormViewer(_CurrentPatientId, _SelectedTransactionID, _SelectedTransactionmasterID, gloPMGlobal.DatabaseConnectionString, _SelectedFormID))
                                {
                                    frmWorkerComp_C4.ClaimNo = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.RowSel, 4));
                                    frmWorkerComp_C4.ShowDialog(this);
                                    //  frmWorkerComp_C4 = null;
                                }
                            }
                            else if (_SelectedFormType == (Int64)WorkersCompFormTypes.C42)
                            {
                                using (frmWorkerCompFormViewer_C42 frmWorkerComp_C42 = new frmWorkerCompFormViewer_C42(_CurrentPatientId, _SelectedTransactionID, _SelectedTransactionmasterID, gloPMGlobal.DatabaseConnectionString, _SelectedFormID))
                                {
                                    frmWorkerComp_C42.ClaimNo = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.RowSel, 4));
                                    frmWorkerComp_C42.ShowDialog(this);
                                    //  frmWorkerComp_C42 = null;
                                }
                            }
                            else if (_SelectedFormType == (Int64)WorkersCompFormTypes.C4Auth)
                            {
                                using (frmWorkerCompFormViewer_C4Auth frmWorkerComp_C4Auth = new frmWorkerCompFormViewer_C4Auth(_CurrentPatientId, _SelectedTransactionID, _SelectedTransactionmasterID, gloPMGlobal.DatabaseConnectionString, _SelectedFormID))
                                {
                                    frmWorkerComp_C4Auth.ClaimNo = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.RowSel, 4));
                                    frmWorkerComp_C4Auth.ShowDialog(this);
                                    //  frmWorkerComp_C4Auth = null;
                                }
                            }
                            else if (_SelectedFormType == (Int64)WorkersCompFormTypes.MG2)
                            {
                                using (frmWorkerCompFormViewer_MG2 frmWorkerComp_MG2 = new frmWorkerCompFormViewer_MG2(_CurrentPatientId, _SelectedTransactionID, _SelectedTransactionmasterID, gloPMGlobal.DatabaseConnectionString, _SelectedFormID))
                                {
                                    frmWorkerComp_MG2.ClaimNo = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.RowSel, 4));
                                    frmWorkerComp_MG2.ShowDialog(this);
                                    //  frmWorkerComp_C4Auth = null;
                                }
                            }
                            else if (_SelectedFormType == (Int64)WorkersCompFormTypes.MG21)
                            {
                                using (frmWorkerCompFormViewer_MG21 frmWorkerComp_MG21 = new frmWorkerCompFormViewer_MG21(_CurrentPatientId, _SelectedTransactionID, _SelectedTransactionmasterID, gloPMGlobal.DatabaseConnectionString, _SelectedFormID))
                                {
                                    frmWorkerComp_MG21.ClaimNo = Convert.ToString(c1PatientDetails.GetData(c1PatientDetails.RowSel, 4));
                                    frmWorkerComp_MG21.ShowDialog(this);
                                    //  frmWorkerComp_C4Auth = null;
                                }
                            }

                        }

                        FillPatientWorkerCompsForms(_CurrentPatientId);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    { //frmWorkerComp_C4 = null; frmWorkerComp_C42 = null; 
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void eligibilityCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////Code to check eligibility of Provider to Insurance.
            //frmEligibilityForm ofrmEligibilityForm = new frmEligibilityForm(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId, tempInsId);
            //ofrmEligibilityForm.ShowDialog();
            //ofrmEligibilityForm.Dispose();

            gloPatient.EiligiblityData EData = null;
            Int64 _patientProviderid = 0;
            Int64 _insuranceContactid = 0;
            gloSettings.GeneralSettings oSettings = null;
            Object _objResult = null;
            string _result = "";
            int nANSIVersion = 0;

            try
            {
                //SLR: Free previoisly allocated memory before allocating again or declare locally?
                gloPatient.gloPatientEiligibility ogloEiligibility = new gloPatient.gloPatientEiligibility(gloPMGlobal.DatabaseConnectionString);
                _patientProviderid = ogloEiligibility.GetPatientProviderID(gloCntrlPatient.PatientID);
                _insuranceContactid = ogloEiligibility.GetInsuranceContactID(tempInsId, gloCntrlPatient.PatientID);


                this.Cursor = Cursors.WaitCursor;
                if (Convert.ToString(_CurrentPatientId).Trim() != "" && Convert.ToString(_patientProviderid).Trim() != ""
                    && Convert.ToString(tempInsId).Trim() != "" && Convert.ToString(_insuranceContactid).Trim() != "")
                {
                    EData = ogloEiligibility.GetEiligibilityData(gloCntrlPatient.PatientID, _patientProviderid, tempInsId, _insuranceContactid);

                    oSettings = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                    oSettings.GetSetting("INSURANCEELIGIBILITY", out _objResult);
                    _result = Convert.ToString(_objResult);

                    nANSIVersion = oSettings.getANSIVersion(_insuranceContactid, "ELIGIBILITY", gloPMGlobal.ClinicID);
                    //SLR: Free oSettings, _objSresult
                    oSettings.Dispose();
                    if (_objResult != null)
                    {
                        _objResult = null;
                    }

                    if (EData != null)
                    {
                        if (_result == "" || _result == "BYCODE")
                        {
                            if (nANSIVersion == 0)
                            {
                                MessageBox.Show("Eligibility Requests ANSI Version has not been set. Eligibility may not proceed. Please review in gloPM Admin.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (nANSIVersion == (int)ANSIVersions.ANSI_4010)
                            {
                                ogloEiligibility.EDIGeneration_270(EData);
                            }
                            else if (nANSIVersion == (int)ANSIVersions.ANSI_5010)
                            {
                                ogloEiligibility.EDI5010Generation_270(EData, ANSIVersions.ANSI_5010);
                            }
                        }
                        else if (_result == "BYSERVICE")
                        {

                            if (nANSIVersion == 0)
                            {
                                MessageBox.Show("Eligibility Requests ANSI Version has not been set.Eligibility may not proceed.Please review in gloPM Admin.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (nANSIVersion == (Int64)ANSIVersions.ANSI_4010)
                            {
                                ogloEiligibility.EDIGeneration_270(EData, tempInsId, _patientProviderid);
                            }
                        }
                    }
                }
                if (ogloEiligibility != null) { ogloEiligibility.Dispose(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (EData != null) { EData.Dispose(); }
            }
        }

        private void mnuItem_Authorization_Click(object sender, EventArgs e)
        {
            //_CurrentPatientId, c1PatientDetails.GetData(,0), c1PatientDetails.GetData(,1)
            //     gloPMGeneral.frmPriorAuthorization ofrmPriorAuthorization = new gloPMGeneral.frmPriorAuthorization(_CurrentPatientId, tempInsName, tempInsId, gloPMGlobal.DatabaseConnectionString);
            gloPMGeneral.frmSetupAuthorization ofrmPriorAuthorization = new gloPMGeneral.frmSetupAuthorization(gloCntrlPatient.PatientID);

            try
            {
                ofrmPriorAuthorization.ShowDialog(this);
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                //SLR: Finaly dispose ofrmPriorAuthorization
                if (ofrmPriorAuthorization != null)
                {
                    ofrmPriorAuthorization.Dispose();
                }
            }
        }

        #region Context Menu evnts for Referrals on Patient detail

        private void mnuItem_AddRef_Click(object sender, EventArgs e)
        {
            if (_SelcetedPatient == PatientDetails.PatientReferrals)
            {
                try
                {


                    frmAddReferrals ofrmAddReferrals = new frmAddReferrals(gloPMGlobal.DatabaseConnectionString, gloCntrlPatient.PatientID, c1PatientDetails);
                    ofrmAddReferrals.ShowDialog(this);
                    ofrmAddReferrals.Dispose();
                    FillPatientReferrals(gloCntrlPatient.PatientID);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                }
            }
            else if (_SelcetedPatient == PatientDetails.PatientCases)
            {
                frmSetupCases ofrmSetupCases = new frmSetupCases(gloCntrlPatient.PatientID);
                ofrmSetupCases.ShowDialog(this);
                ofrmSetupCases.Dispose();
                FillCases(gloCntrlPatient.PatientID);
            }
        }

        private void mnuItem_RemoveRef_Click(object sender, EventArgs e)
        {
            String strSql = "";
            Int64 _nContactID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 9));
            Int64 _nPatientDetailID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 8));
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(Program.GetConnectionString());
            try
            {
                oDB.Connect(false);
                int _result = 0;
                if (MessageBox.Show("Are you sure you want to delete this referral ?  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    strSql = "DELETE FROM Patient_DTL WHERE nContactId = '" + _nContactID + "' AND nPatientDetailID ='" + _nPatientDetailID + "' AND nPatientID='" + _CurrentPatientId + "' ";
                    _result = oDB.Execute_Query(strSql);
                }

                if (_result > 0)
                {
                    FillPatientReferrals(_CurrentPatientId);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //SLR: Finaly dispose odb
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        #endregion Context Menu evnts for Referrals on Patient detail

        private void c1PatientDetails_AfterResizeColumn(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                gloSettings.ModuleOfGridColumn _CurrentGridModule = gloSettings.ModuleOfGridColumn.Dashboard;
                switch (_SelcetedPatient)
                {
                    case PatientDetails.PatientInsurance:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientInsurance;
                        }
                        break;
                    case PatientDetails.PatientAppointments:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientAppointments;
                        }
                        break;
                    case PatientDetails.PatientBilling:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientBilling;
                        }
                        break;
                    case PatientDetails.PatientBalance:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientBalance;
                        }
                        break;
                    case PatientDetails.PatientProcedures:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientProcedures;
                        }
                        break;
                    case PatientDetails.PatientReferrals:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientReferrals;
                        }
                        break;
                    case PatientDetails.PriorAuthorization:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPMPriorAuthorization;
                        }
                        break;
                    case PatientDetails.PatientCases:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientCases;
                        }
                        break;
                    case PatientDetails.PatientTasks:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientTasks;
                        }
                        break;
                    case PatientDetails.PatientNYWorkersCompForms:
                        {
                            _CurrentGridModule = gloSettings.ModuleOfGridColumn.DashBoardPatientNYWorkersCompForms;
                        }
                        break;

                } // Switch

                gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                oSetting.SaveGridColumnWidth(c1PatientDetails, _CurrentGridModule, gloPMGlobal.UserID);
                oSetting.Dispose();
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion

        #region "Patient Card Events"

        private void btnPC_ScanCard_Click(object sender, EventArgs e)
        {
            //to scan against existing patient
            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
            {
                return;
            }
            string _CardRootPath = Application.StartupPath;

            gloCardScanning.gloCardScanning oCardScanning = new gloCardScanning.gloCardScanning(gloPMGlobal.DatabaseConnectionString);
            oCardScanning.ScanCard(_CardRootPath, gloCntrlPatient.PatientID, this);
            //SLR: Dispose and then
            oCardScanning.Dispose();
            oCardScanning = null;
            try
            {
                //to Fill Saved Card images. 
                FillCardInformation();
                //---x----
                //gloCardScanning.frmCardImage oCardImage = new gloCardScanning.frmCardImage(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
                //oCardImage.StartPosition = FormStartPosition.CenterParent;
                //oCardImage.ShowDialog();
                //oCardImage = null;
                //FillCardInformation();
                //FillDemographicInformation(); //added on 19/05/2010 update/refresh patient photo
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnPC_DeleteCard_Click(object sender, EventArgs e)
        {
            if (picPC_Cards.Tag != null)
            {
                //if (Convert.ToInt16(picPC_Cards.Tag.ToString()) > 0)
                {
                    gloCardScanning.gloCardScanning oCardScanning = new gloCardScanning.gloCardScanning(gloPMGlobal.DatabaseConnectionString);
                    if (oCardScanning.DeleteCard(false, gloCntrlPatient.PatientID, oPatientCards[Convert.ToInt16(picPC_Cards.Tag.ToString())].CardNumber) == true)
                    {
                        oPatientCards.RemoveAt(Convert.ToInt16(picPC_Cards.Tag.ToString()));
                    }
                    FillCardInformation();
                    oCardScanning.Dispose();
                }
            }
        }


        int iCount = 0;
        bool UniformSize = false;
        private void btnPC_PrintCards_Click(object sender, EventArgs e)
        {
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            UniformSize = ogloPatient.readCardPrintSetting();
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        oPatientCards = ogloPatient.GetPatientCards(_CurrentPatientId);
                    }

                    if (gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        Dictionary<String, Byte[]> dictImages = ogloPatient.PrintToBitmapsWithDPi(8.5f, 11f, 600, 600, oPatientCards);

                        string fileName = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".zip", "MMddyyyyHHmmssffff");
                        List<gloPrintDialog.gloPrintProgressController.DocumentInfo> lstDocs = new List<gloPrintDialog.gloPrintProgressController.DocumentInfo>();
                        List<string> ZipedFiles = gloGlobal.gloTSPrint.ZipAllBytes(dictImages, fileName, gloGlobal.gloTSPrint.NoOfPages);
                        for (int i = 0; i <= ZipedFiles.Count - 1; i++)
                        {
                            gloPrintDialog.gloPrintProgressController.DocumentInfo DocInfo = new gloPrintDialog.gloPrintProgressController.DocumentInfo();
                            DocInfo.PdfFileName = ZipedFiles[i];
                            DocInfo.SrcFileName = ZipedFiles[i];
                            DocInfo.footerInfo = null;
                            lstDocs.Add(DocInfo);
                        }
                        SendForPrint(lstDocs);
                    }
                    else
                    {
                        if (printDialog1.ShowDialog(this) == DialogResult.OK)
                        {
                            //set printer settings on printdocument object
                            printDoc.PrinterSettings = printDialog1.PrinterSettings;
                            iCount = 0;
                            //print...
                            printDoc.Print();
                        }
                    }

                    //SLR: Only if there was a new , then we need to dispose..
                    ////SLR: Dispose printDialog1
                    //if (printDialog1 != null)
                    //{
                    //    printDialog1.Dispose();
                    //}
                }
            }
            if (ogloPatient != null)
            {
                ogloPatient.Dispose();
            }
        }

        private void SendForPrint(List<gloPrintDialog.gloPrintProgressController.DocumentInfo> lstDocs)
        {
            gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

            try
            {
                gloPrintDialog.gloExtendedPrinterSettings extendedPrinterSettings = new gloPrintDialog.gloExtendedPrinterSettings();
                extendedPrinterSettings.IsShowProgress = false;
                extendedPrinterSettings.IsBackGroundPrint = true;
                ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(lstDocs, null, extendedPrinterSettings, blnUseEMFForSSRS: true);
                ogloPrintProgressController.bIsFromScanDoc = true;
                ogloPrintProgressController.ShowProgress(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;

            }
            finally
            {
            }
        }

        //private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    try
        //    {
        //        int y = 3;

        //        if (!(oPatientCards == null))
        //        {
        //            for (Int16 i = 0; i < oPatientCards.Count; i++)
        //            {
        //                if (oPatientCards[i].CardImage != null)
        //                {
        //                    //e.Graphics.DrawImage(oPatientCards[i].CardImage, new Point(3, y));
        //                    // y = y + 250;
        //                    // e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

        //                    e.Graphics.DrawImage(oPatientCards[i].CardImage, new Point(3, y));
        //                    //y = y + e.PageBounds.Width;
        //                    y = y + 250;

        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}



        #region "Print page Event"

        private void printDoc_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                double cardHeight = 0;
                double cardWidth = 0;
                //Dim Offset As Int64 = 10


                bool exitfor = false;
                Rectangle MyBounds = e.MarginBounds;

                Size MySize = new Size((int)e.Graphics.DpiX, (int)e.Graphics.DpiY);


                Rectangle MarginRect = new Rectangle(2, 2, 108, 72);
                //Changed margin width to increase card size
                if (UniformSize)
                {
                    MarginRect.Width = 57;
                }

                MarginRect.Y = MarginRect.Y * MySize.Height / 72;
                MarginRect.X = MarginRect.X * MySize.Width / 72;
                MarginRect.Height = MarginRect.Height * MySize.Height / 72;
                MarginRect.Width = MarginRect.Width * MySize.Width / 72;

                MyBounds.Y = MyBounds.Y + MarginRect.Top;
                MyBounds.X = MyBounds.X + MarginRect.Left;
                MyBounds.Height = MyBounds.Height * MySize.Height / 72 - (2 * MarginRect.Height);
                MyBounds.Width = MyBounds.Width * MySize.Width / 72 - (2 * MarginRect.Width);
                int _NewHorzDivision = 0;
                int _NewVertDivision = 0;
                if (UniformSize)
                {
                    _NewHorzDivision = (MyBounds.Width - MarginRect.X) / 2;
                    _NewVertDivision = (MyBounds.Height - (3 * MarginRect.Y)) / 4;
                }


                Int64 y = MyBounds.Top;

                if ((oPatientCards != null))
                {
                    int jCount = -1;
                    for (int i = iCount; i <= oPatientCards.Count - 1; i++)
                    {
                        if (oPatientCards[i].CardImage != null)
                        {
                            jCount++;
                            System.Drawing.Image thisImage = oPatientCards[i].CardImage;
                            GraphicsUnit units = e.Graphics.PageUnit; //graphicsunits
                            RectangleF thisImageBound = thisImage.GetBounds(ref units);
                            e.Graphics.PageUnit = units;



                            cardHeight = thisImageBound.Height * MySize.Height / thisImage.VerticalResolution;
                            cardWidth = thisImageBound.Width * MySize.Width / thisImage.HorizontalResolution;
                            int x_Page = MyBounds.X;
                            double y_addition = _NewVertDivision;

                            if (UniformSize)
                            {
                                double ScaleX = _NewHorzDivision / cardWidth;
                                double ScaleY = _NewVertDivision / cardHeight;
                                if (ScaleX > ScaleY)
                                {
                                    cardHeight = _NewVertDivision;
                                    cardWidth = cardWidth * ScaleY;
                                }
                                else
                                {
                                    cardWidth = _NewHorzDivision;
                                    cardHeight = cardHeight * ScaleX;
                                }
                                if ((jCount % 2) == 1)
                                {
                                    x_Page += MarginRect.X + _NewHorzDivision;
                                }
                                else
                                {
                                    y_addition = -MarginRect.Top;
                                }
                            }
                            else
                            {
                                if ((cardWidth > MyBounds.Width) | (cardHeight > MyBounds.Height))
                                {
                                    double ScaleX = MyBounds.Width / cardWidth;
                                    double ScaleY = MyBounds.Height / cardHeight;
                                    if ((ScaleX > ScaleY))
                                    {
                                        cardHeight = MyBounds.Height;
                                        cardWidth = cardWidth * ScaleY;
                                    }
                                    else
                                    {
                                        cardWidth = MyBounds.Width;
                                        cardHeight = cardHeight * ScaleX;
                                    }
                                }
                                y_addition = cardHeight;
                            }
                            if (((cardHeight + y) >= MyBounds.Bottom) || (jCount == 8))
                            {
                                if ((y == MyBounds.Top))
                                {
                                }
                                else
                                {
                                    e.HasMorePages = true;
                                    iCount = i;
                                    exitfor = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }

                            }



                            e.Graphics.DrawImage(thisImage, x_Page, y, Convert.ToInt64(cardWidth), Convert.ToInt64(cardHeight));

                            y += Convert.ToInt64(y_addition) + Convert.ToInt64(MarginRect.Top);


                        }
                    }

                }
                if ((exitfor == false))
                {
                    e.HasMorePages = false;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            //PrintCard(sender, e)
        }

        #endregion "Print page Event"


        private void btnPC_MoveLast_Click(object sender, EventArgs e)
        {
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        oPatientCards = ogloPatient.GetPatientCards(_CurrentPatientId);
                    }
                    //SLR: if count >=1 then
                    if (oPatientCards.Count > 0)
                    {
                        picPC_Cards.Image = oPatientCards[oPatientCards.Count - 1].CardImage;
                        //Added By mitesh 
                        lblScannedDate.Text = "Scanned : " + oPatientCards[oPatientCards.Count - 1].ScannedDateTime.ToString("MM/dd/yyyy");
                        //---
                        picPC_Cards.Tag = oPatientCards.Count - 1;
                    }
                }
            }
            if (ogloPatient != null)
            {
                ogloPatient.Dispose();
            }
        }

        private void btnPC_MoveNext_Click(object sender, EventArgs e)
        {
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        oPatientCards = ogloPatient.GetPatientCards(_CurrentPatientId);
                    }
                    //SLR: and if count >=1 then
                    if ((oPatientCards.Count > 0) && (Convert.ToInt16(picPC_Cards.Tag.ToString()) + 1) <= (oPatientCards.Count - 1))
                    {
                        picPC_Cards.Image = oPatientCards[Convert.ToInt16(picPC_Cards.Tag.ToString()) + 1].CardImage;
                        //Added By mitesh                       
                        lblScannedDate.Text = "Scanned : " + oPatientCards[Convert.ToInt16(picPC_Cards.Tag.ToString()) + 1].ScannedDateTime.ToString("MM/dd/yyyy");
                        //---
                        picPC_Cards.Tag = Convert.ToInt16(picPC_Cards.Tag.ToString()) + 1;

                    }
                }
            }
            if (ogloPatient != null)
            {
                ogloPatient.Dispose();
            }
        }

        private void btnPC_MovePrevious_Click(object sender, EventArgs e)
        {
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        oPatientCards = ogloPatient.GetPatientCards(_CurrentPatientId);
                    }
                    if ((oPatientCards.Count > 0) && (Convert.ToInt16(picPC_Cards.Tag.ToString()) - 1) >= 0) //SLR: and if count >=1 then
                    {
                        picPC_Cards.Image = oPatientCards[Convert.ToInt16(picPC_Cards.Tag.ToString()) - 1].CardImage;
                        //Added By mitesh 

                        lblScannedDate.Text = "Scanned : " + oPatientCards[Convert.ToInt16(picPC_Cards.Tag.ToString()) - 1].ScannedDateTime.ToString("MM/dd/yyyy");

                        //---
                        picPC_Cards.Tag = Convert.ToInt16(picPC_Cards.Tag.ToString()) - 1;

                    }
                }
            }
            if (ogloPatient != null)
            {
                ogloPatient.Dispose();
            }
        }

        private void btnPC_MoveFirst_Click(object sender, EventArgs e)
        {
            gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);

            if (!(oPatientCards == null))
            {
                if (oPatientCards.Count > 0)
                {
                    if (oPatientCards.Count == 1)
                    {
                        oPatientCards.Clear();
                        oPatientCards = ogloPatient.GetPatientCards(_CurrentPatientId);
                    }
                    //SLR: if count >=1 then
                    if (oPatientCards.Count > 0)
                    {
                        picPC_Cards.Image = oPatientCards[0].CardImage;
                        picPC_Cards.Tag = 0;
                        //Added By mitesh                                     
                        lblScannedDate.Text = "Scanned : " + oPatientCards[0].ScannedDateTime.ToString("MM/dd/yyyy");
                        //---
                    }
                }
            }
            if (ogloPatient != null)
            {
                ogloPatient.Dispose();
            }
        }



        #endregion

        #region "Patient Demographics Events"

        private void btnPD_Update_Click(object sender, EventArgs e)
        {

        }

        private void picPD_Photo_DoubleClick(object sender, EventArgs e)
        {

        }

        #endregion

        private void tmr_Dashboard_Tick(object sender, EventArgs e)
        {
            //gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();

            try
            {
                if (this.MdiChildren.Length <= 0)
                {
                    if (Convert.ToString(appSettings["IsPatientAdded"]) != "" && Convert.ToBoolean(appSettings["IsPatientAdded"]) == true)
                    {
                        appSettings["IsPatientAdded"] = "False";

                        gloCntrlPatient.FillPatients();
                        gloCntrlPatient.Refresh();
                        gloCntrlPatient.SelectedPatientID = _CurrentPatientId;
                    }

                    pnl_ts_Main.Visible = true;
                    ShowHideMainMenu(true, true);
                }
                //Added By Debasish Das on 22nd Jun 2010
                //Checking Wheather Claim is Modified or Not


                if (gloGlobal.gloPMGlobal.ChargesIsClaimModified == true)
                {
                    showPatientDetails();
                    c1PatientDetails.Focus();
                    c1PatientDetails.Select(SelectedRowsel, 0, true);
                }

                if (gloGlobal.gloPMGlobal.ChargesIsClaimModified == true ||
                    gloGlobal.gloPMGlobal.PatientFinancialViewIsModified == true ||
                    gloGlobal.gloPMGlobal.PatientAlertIsPatientAlertModified == true)
                {
                    FillAlerts();
                }



                //if (Convert.ToString(oSettings.ReadSettings_XML("Charges", "IsClaimModified")) != "" && Convert.ToBoolean(oSettings.ReadSettings_XML("Charges", "IsClaimModified")) == true)
                //{
                //    oSettings.WriteSettings_XML("Charges", "IsClaimModified", Boolean.FalseString);
                //    showPatientDetails();
                //    FillAlerts();
                //}
                //if (Convert.ToString(oSettings.ReadSettings_XML("PatientFinancialView", "IsModified")) != "" && Convert.ToBoolean(oSettings.ReadSettings_XML("PatientFinancialView", "IsModified")) == true)
                //{
                //    oSettings.WriteSettings_XML("PatientFinancialView", "IsModified", Boolean.FalseString);
                //    FillAlerts();
                //}

                ////**

                ////Added By Debasish Das on 20th Nov 2010
                ////Checking Wheather PatientAlert is Modified or Not

                //if (Convert.ToString(oSettings.ReadSettings_XML("PatientAlert", "IsPatientAlertModified")) != "" && Convert.ToBoolean(oSettings.ReadSettings_XML("PatientAlert", "IsPatientAlertModified")) == true)
                //{
                //    oSettings.WriteSettings_XML("PatientAlert", "IsPatientAlertModified", Boolean.FalseString);
                //    //showPatientDetails();
                //    FillAlerts();
                //}
                ////**
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                gloGlobal.gloPMGlobal.ChargesIsClaimModified = false;
                gloGlobal.gloPMGlobal.PatientFinancialViewIsModified = false;
                gloGlobal.gloPMGlobal.PatientAlertIsPatientAlertModified = false;

                //if (oSettings != null) { oSettings.Dispose(); }
            }

        }

        #region "Lock Screen"

        public void Activity_Log(string strLogMessage)
        {
            //System.IO.StreamWriter objFile = new System.IO.StreamWriter(Application.StartupPath + "\\gloPMS_Log.log", true);

            try
            {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter(appSettings["StartupPath"].ToString() + "\\gloPMS_Log.log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "   " + strLogMessage + '\n');
                objFile.Close();
                //SLR: DIpsose and then
                objFile.Dispose();
                objFile = null;
            }
            catch //(Exception ex)
            { }

            //MessageBox.Show("Error while accessing Database. Please click on Help to view log.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, Application.StartupPath + "\\gloPMS_ERRORLog.log");
        }

        private void timerLockScreen_Tick(object sender, EventArgs e)
        {
            try
            {
                LastInputInfo info = new LastInputInfo();
                info.cbSize = Strings.Len(info);
                Int32 res;
                res = GetLastInputInfo(ref info);
                Int64 idealTime;
                idealTime = Convert.ToInt64(timeGetTime() - info.dwTime);

                // label29.Text = Convert.ToString(timeGetTime() - info.dwTime) + ", timerLockScreen.Interval=" + timerLockScreen.Interval.ToString();
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true);
                Activity_Log("- timerLockScreen_Tick = " + idealTime);
                Activity_Log("- LockScreen Time = " + Convert.ToString(Program.gLockScreenTime * 60000));
                // GetRegistryvalueforHelp();
                if (gloRegistrySetting.GetRegistryValue("AutoLockEnable") != null)
                {
                    if (gloRegistrySetting.GetRegistryValue("AutoLockEnable").ToString() == "1")
                    {
                        gloRegistrySetting.CloseRegistryKey();
                        if (Environment.TickCount - info.dwTime >= Program.gLockScreenTime * 60000)
                        {
                            timerLockScreen.Stop();
                            timerLockScreen.Enabled = false;
                            //MessageBox.Show("No Action found - " + (timeGetTime() - info.dwTime));
                            LockScreen();
                            UpdateStatusBar();
                            timerLockScreen.Enabled = true;
                            timerLockScreen.Start();
                        }
                    }
                    else
                    {
                        gloRegistrySetting.CloseRegistryKey();
                    }
                }
                else
                {
                    gloRegistrySetting.CloseRegistryKey();
                    if (Environment.TickCount - info.dwTime >= Program.gLockScreenTime * 60000)
                    {
                        timerLockScreen.Stop();
                        timerLockScreen.Enabled = false;
                        //MessageBox.Show("No Action found - " + (timeGetTime() - info.dwTime));
                        LockScreen();
                        UpdateStatusBar();
                        timerLockScreen.Enabled = true;
                        timerLockScreen.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool LockScreen()
        {
            try
            {
                appSettings["BreakTheGlass"] = "False";
                System.IntPtr anyDialogOpened = IntPtr.Zero;  //Bug #95903 to hide dialog window
                //Save current User Display setting  
                gloCntrlPatient.SaveColumnWidth();
                //gloStripControl.
                SaveDisplaySettings(false);
                if (gloPatient.frmSetupPatient.hshPatData != null)
                {
                    gloPatient.frmSetupPatient.hshPatData.Clear(); 
                }

                frmLockScreen frm = new frmLockScreen();
                frm.WindowState = FormWindowState.Maximized;

                // added by sandip dhakane 20100719 solving problem of two instances when application is in autolock mode.(mantisID-862)
                //frm.ShowInTaskbar = true;
                //this.Visible = false;
                //frm.BringToFront();

                if (frm.ShowInTaskbar == true)
                {
                    frm.ShowInTaskbar = false;
                    this.Visible = false;
                    frm.BringToFront();
                }

                // frm.BringToFront();
                this.SendToBack();
                // end code

                tmr_Dashboard.Enabled = false;
                ogloReminder.Stop();

                //Clear All Cache to refresh the cache
                gloGlobal.gloPMMasters.ClearCache(gloPMMasters.MasterType.All);

                //Clear Rules Cache
                gloCharges.ClearRulesCached();

                //Problem : 00000253 : Lock Screen Issue
                //Issue : User can see the forms after lock screen.
                //Change : Hide all open forms and bring back them to original state after user re-login.
                try
                {
                    anyDialogOpened = gloWord.WordDialogBoxBackgroundCloser.IsDialogWindowOpened();    //Bug #95903 to hide dialog window
                }
                catch
                {

                }
                if (anyDialogOpened != IntPtr.Zero)
                {
                    gloWord.WordDialogBoxBackgroundCloser.HideWindow(anyDialogOpened);    //Bug #95903 to hide dialog window
                }
                string activeForm = string.Empty;

                if (Form.ActiveForm != null)
                {
                    activeForm = Form.ActiveForm.Name;
                }

                foreach (Form _frm in Application.OpenForms)
                {
                    if (_frm.Name != "frmSplash" && _frm.IsMdiChild == false && _frm.Visible == true)
                    {
                        _frm.Visible = false;
                    }
                }

                gloUIControlLibrary.WPFForms.frmCodeGuide frmCodeGuide = gloUIControlLibrary.WPFForms.frmCodeGuide.CheckInstance();

                if (frmCodeGuide != null)
                { frmCodeGuide.Visibility = System.Windows.Visibility.Hidden; }

                gloUIControlLibrary.WPFForms.frmICDCodeGallery frmICDCodeGallery = gloUIControlLibrary.WPFForms.frmICDCodeGallery.CheckFormOpen();

                if (frmICDCodeGallery != null)
                {
                    frmICDCodeGallery.Visibility = System.Windows.Visibility.Hidden;
                    frmICDCodeGallery.WindowState = System.Windows.WindowState.Minimized;
                }

                frm.ShowInTaskbar = true;
                frm.ShowDialog(this);
                //SLR: FRee frm
                frm.Dispose();
                //this.Visible = true;

                if (frmCodeGuide != null)
                {
                    frmCodeGuide.Visibility = System.Windows.Visibility.Visible;
                    frmCodeGuide = null;
                }

                if (frmICDCodeGallery != null)
                {
                    frmICDCodeGallery.Visibility = System.Windows.Visibility.Visible;
                    frmICDCodeGallery.WindowState = System.Windows.WindowState.Maximized;
                    frmICDCodeGallery = null;
                }
                if (anyDialogOpened != IntPtr.Zero)  //Bug #95903 to show dialog window
                {
                    gloWord.WordDialogBoxBackgroundCloser.ForceWindowIntoForeground(anyDialogOpened);
                }
                //Start : Code changes done by  Sagar Ghodke, Jan 22, 2013
                //Against issue no. 43684
                //Made copy of Form collection to perform the looping

                List<Form> _OpenFormList = new List<Form>();
                foreach (Form _from in Application.OpenForms)
                { _OpenFormList.Add(_from); }

                //foreach (Form _frm in Application.OpenForms)
                foreach (Form _frm in _OpenFormList)
                {
                    if (_frm.Name != "frmSplash" && _frm.IsMdiChild == false && _frm.Visible == false)
                    {
                        _frm.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(activeForm) && _frm.Name.Equals(activeForm))
                    {
                        _frm.BringToFront();
                    }
                }
                _OpenFormList.Clear();
                _OpenFormList = null;



                //Finish : Code changes done by  Sagar Ghodke, Jan 22, 2013

                //Load Log in users display settings 
                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    { gloPMGlobal.UserID = Convert.ToInt64(appSettings["UserID"]); }
                }

                //Get User Name
                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    { gloPMGlobal.UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                    else
                    { gloPMGlobal.UserName = ""; }
                }
                else
                { gloPMGlobal.UserName = ""; }

                Form[] oForms = this.MdiChildren;

                if (oForms.Length == 0)
                {
                    LoadDisplaySettings(false);
                }

                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                if (oSecurity.isPatientLock(gloCntrlPatient.PatientID, false))
                {
                    _CurrentPatientId = 0;
                    EnableDisableLockedPatientButtons(false);
                }
                //else if (oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, false))
                //{
                //    _CurrentPatientId = 0;
                //    EnableDisableLockedPatientButtons(true);
                //    EnableDisableBadDebtPatientButtons(false);
                //}
                if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }

                //To Show Patient details Grid Settings as per Log in user
                showPatientDetails();


                if (this.MdiChildren.Length > 0)
                {
                    ShowHideMainMenu(false, false);
                }
                tmr_Dashboard.Enabled = true;
                ogloReminder.Start();


                //MaheshB
                GetPatientDemographicsSettings();
                FillSelectedPatient();

                //Assign User Rights 20090720
                AssignUserRights();

                //Check Patient Sync Settings for Current User
                GetSyncPatientSetting();
                SetLicenseModule();
                ShowCollectionAgencyRefundMenu();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        private void c1PatientDetails_RowColChange(object sender, EventArgs e)
        {
            int col = c1PatientDetails.ColSel;
        }

        private void mnuReports_ClaimStatus_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    ShowHideMainMenu(false, false);
            //    pnl_ts_Main.Visible = true;
            //    gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");  
            //    ogloBilling.ShowReportClaimStatus(this);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            try
            {
                //Added By Mukesh Patel For Implementing Discrepancy Report / Claim Status Reports
                ShowHideMainMenu(false, false);
                gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, "");
                ogloBilling.ShowReportClaimStatusNew(this);
                //SLR: Finally free ogloBilling
                ogloBilling.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void claimsReviewedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling("", "");
            ogloBilling.ShowReportListofClaimsReviwed(this);
            ShowHideMainMenu(false, false);
            //SLR: Finally free ogloBilling
            ogloBilling.Dispose();
        }

        //private void reportsListToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ShowHideMainMenu(false, false);
        //    pnl_ts_Main.Visible = true;
        //    gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling("", "");
        //    ogloBilling.ShowReportListofReports(this);
        //}

        private void viewMyReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/Reports/reportlist.aspx", "View My Reports");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }


        private void RegisterHotKey()
        {
            try
            {
                //GLO2012-0016207 : User is unable to log back into the gloSuite from the 'Screen Saver' the after the password is entered the login screen returnes.
                try
                {
                    this.HotKeyPressed -= new HotKeyPressedEventHandler(hotKey_Pressed);
                }
                catch
                {
                    //sometimes it gives error, that's why blank catch
                }


                this.HotKeyPressed += new HotKeyPressedEventHandler(hotKey_Pressed);

                HotKey htk = new HotKey("gloHelp", Keys.F1, HotKey.HotKeyModifiers.MOD_NONE);

                this.HotKeys.Add(htk);
            }
            catch
            {

            }

        }



        private void hotKey_Pressed(object sender, HotKeyPressedEventArgs e)
        {
            // UpdateLog("Me.WindowState " + this.WindowState.ToString);
            try
            {
                if (this.WindowState != FormWindowState.Minimized & this.Name != "frmLockScreen")
                {
                    // bool blnExamChild = false;
                    HotKey h = (HotKey)(e.HotKey);
                    if (h.KeyCode.ToString() == "F1")
                        ShowHelp();
                }
            }
            catch
            {

            }
        }
        private HotKeyCollection m_hotKeys;
        public HotKeyCollection HotKeys
        {
            get { return m_hotKeys; }
        }




        private void ShowHelp()
        {

            try
            {
                if (Program.gstrHelpProvider == "Client")
                {
                    this.HelpComponent1.Mode = HelpComponent.ProviderMode.Client;
                }
                else
                {
                    this.HelpComponent1.Mode = HelpComponent.ProviderMode.Builder;
                }



                this.HelpComponent1.ShowHelp(this);


            }
            catch
            {

            }

        }

        private void listOfClaimsRecivedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/claimsreceivedsearch.aspx", "List Of Claims Recived");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void myFilesRecivedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/FilesReceived?DaysBack=3", "My Files Recived");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void staffProductivityToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/staffprodrpt/staffprodrpt.aspx", "Staff Productivity");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void patientSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/patientsearch.aspx", "Patient Search");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void providerSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/providersearch.aspx", "Provider Search");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void insurerSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/insurersearch.aspx", "Insurer Search");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void secondarySearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/remittancedisplay/secondarysearch.aspx", "Secondary Search");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void advancedSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/advancedsearch.aspx", "Advanced Search");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void realtimeClaimStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/realtimecsi", "Realtime Claim Status");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void cCISuspensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/cci", "CCI Suspensions");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void rejectionAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/rejectanalysis.aspx", "Rejection Analysis");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void transactionSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/transSummary.aspx", "TransactionSummary");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void safetyNetReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/safetynet.aspx", "Safety Net Report");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void claimsFileReconcliToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/claimstatus/claimfilesearch.aspx", "Claims File Reconcilation");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void bestPracticesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_ListofClaimsReviewed ofrmRpt_ListofClaimsReviewed = new gloBilling.frmRpt_ListofClaimsReviewed("https://www.gatewayedi.com/gedi/help/bestPractices.aspx", "Best Practices");
            //ofrmRpt_ListofClaimsReviewed.MdiParent = this;
            //ofrmRpt_ListofClaimsReviewed.WindowState = FormWindowState.Maximized;
            //ofrmRpt_ListofClaimsReviewed.Show();
            //ShowHideMainMenu(false, false);
            //ofrmRpt_ListofClaimsReviewed.StartPosition = FormStartPosition.CenterParent;
        }

        private void mnuReportsTransactionHistory_Click(object sender, EventArgs e)
        {

            //Commented By Pramod Nair For Implementing Crystal Reports
            //ShowHideMainMenu(false, false);
            //pnl_ts_Main.Visible = true;
            //gloBilling.frmRpt_TransactionHistory ofrmRpt_TransactionHistory = new gloBilling.frmRpt_TransactionHistory(gloPMGlobal.DatabaseConnectionString);

            //ShowHideMainMenu(false, false);
            //ofrmRpt_TransactionHistory.MdiParent = this;
            //ofrmRpt_TransactionHistory.WindowState = FormWindowState.Maximized;
            //ofrmRpt_TransactionHistory.ShowInTaskbar = false;
            //ofrmRpt_TransactionHistory.StartPosition = FormStartPosition.CenterParent;
            //ofrmRpt_TransactionHistory.Show();



            //Added By Pramod Nair For Implementing Crystal Reports
            //gloReports.C1Reports.frmRpt_TransactionHistory oTransactionHistory = new gloReports.C1Reports.frmRpt_TransactionHistory(gloPMGlobal.DatabaseConnectionString);

            //oTransactionHistory.MdiParent = this;
            //oTransactionHistory.WindowState = FormWindowState.Maximized;
            //oTransactionHistory.ShowInTaskbar = false;
            //oTransactionHistory.StartPosition = FormStartPosition.CenterParent;
            //oTransactionHistory.Show();
            //ShowHideMainMenu(false, false);

        }

        private void mnu_rpt_ChargePaymentSummaryReport_Click(object sender, EventArgs e)
        {
            if (_IsEnableSSRSReports == false)
            {
                gloReports.C1Reports.frmRpt_DailyChargesPaySummary oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyChargesPaySummary(gloPMGlobal.DatabaseConnectionString);
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
            {
                gloReports.C1Reports.frmRpt_DailyChargesPaySummary_SSRS oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyChargesPaySummary_SSRS(gloPMGlobal.DatabaseConnectionString);

                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
        }

        private void mnu_PatientRecall_Click(object sender, EventArgs e)
        {
            gloAppointmentScheduling.frmPatientRecall ofrm = new gloAppointmentScheduling.frmPatientRecall(gloPMGlobal.DatabaseConnectionString);

            ofrm.MdiParent = this;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowInTaskbar = false;
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuTransactionHistoryAnalysis_Click(object sender, EventArgs e)
        {
            //gloBilling.frmRpt_TransactionHistoryAnalysis ofrm = new frmRpt_TransactionHistoryAnalysis(gloPMGlobal.DatabaseConnectionString);

            //ofrm.MdiParent = this;
            //ofrm.WindowState = FormWindowState.Maximized;
            //ofrm.ShowInTaskbar = false;
            //ofrm.StartPosition = FormStartPosition.CenterParent;
            //ofrm.Show();
            //ShowHideMainMenu(false, false);
        }

        private void mnuTransactionNotes_Click(object sender, EventArgs e)
        {
            //Commented By Pramod Nair For Implementing Crystal Reports
            //gloBilling.frmRpt_TransactionNotes ofrm = new frmRpt_TransactionNotes(gloPMGlobal.DatabaseConnectionString);
            //ShowHideMainMenu(false, false);
            //ofrm.MdiParent = this;
            //ofrm.WindowState = FormWindowState.Maximized;
            //ofrm.ShowInTaskbar = false;
            //ofrm.StartPosition = FormStartPosition.CenterParent;
            //ofrm.Show();

            //Added By Pramod Nair For Implementing Crystal Reports
            //gloReports.C1Reports.frmRpt_TransactionNotes oTransactionNotes = new gloReports.C1Reports.frmRpt_TransactionNotes(gloPMGlobal.DatabaseConnectionString);

            //oTransactionNotes.MdiParent = this;
            //oTransactionNotes.WindowState = FormWindowState.Maximized;
            //oTransactionNotes.ShowInTaskbar = false;
            //oTransactionNotes.StartPosition = FormStartPosition.CenterParent;
            //oTransactionNotes.Show();
            //ShowHideMainMenu(false, false);


        }

        private void mnu_patientReport_Click(object sender, EventArgs e)
        {
            //Commented By Pramod Nair For Implementing Crystal Reports
            //gloBilling.frmRpt_PatientReport ofrm = new frmRpt_PatientReport(gloPMGlobal.DatabaseConnectionString);
            //ShowHideMainMenu(false, false);
            //ofrm.MdiParent = this;
            //ofrm.WindowState = FormWindowState.Maximized;
            //ofrm.ShowInTaskbar = false;
            //ofrm.StartPosition = FormStartPosition.CenterParent;
            //ofrm.Show();

            //Added By Pramod Nair For Implementing Crystal Reports
            gloReports.C1Reports.frmRpt_PatientReport oPatientReport = new gloReports.C1Reports.frmRpt_PatientReport(gloPMGlobal.DatabaseConnectionString);

            oPatientReport.MdiParent = this;
            oPatientReport.WindowState = FormWindowState.Maximized;
            oPatientReport.ShowInTaskbar = false;
            oPatientReport.StartPosition = FormStartPosition.CenterParent;
            oPatientReport.Show();
            ShowHideMainMenu(false, false);

        }

        private void mnu_rpt_Appointments_Click(object sender, EventArgs e)
        {
            //Patient On Report
            //gloPatient.frmRptPatientOn ofrmRptPatientOn = new gloPatient.frmRptPatientOn(gloPMGlobal.DatabaseConnectionString);
            try
            {
                //Commented By Pramod Nair For Implementing Crystal Reports

                //ofrmRptPatientOn.MdiParent = this;
                //ofrmRptPatientOn.Show();
                //ofrmRptPatientOn.WindowState = FormWindowState.Maximized;
                //ofrmRptPatientOn.ShowInTaskbar = false;
                //ofrmRptPatientOn.StartPosition = FormStartPosition.CenterParent;
                //ShowHideMainMenu(false, false);


                //Added By Pramod Nair For Implementing Crystal Reports
                // gloReports.C1Reports.frmRpt_Appointments oAppointments = new gloReports.C1Reports.frmRpt_Appointments(gloPMGlobal.DatabaseConnectionString);
                //  Program.gSQLServerName, Program.gDatabase, !Program.gWindowsAuthentication, Program.gLoginUser, Program.gLoginPassword,
                //Added by Chetan for SSRS reports
                gloReports.frmRPT_AppointmentView oAppointments = new gloReports.frmRPT_AppointmentView(gloPMGlobal.DatabaseConnectionString, "rptAppointmentList", Program.gSQLServerName, Program.gDatabase, !Program.gWindowsAuthentication, Program.gLoginUser, Program.gLoginPassword, "gloPM");

                oAppointments.MdiParent = this;
                oAppointments.WindowState = FormWindowState.Maximized;
                oAppointments.ShowInTaskbar = false;
                oAppointments.StartPosition = FormStartPosition.CenterParent;
                oAppointments.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnu_rpt_AppointmentWaiting_Click(object sender, EventArgs e)
        {
            //gloReports.C1Reports.frmRpt_AppointmentWaiting oWaitingAppointments = new gloReports.C1Reports.frmRpt_AppointmentWaiting(gloPMGlobal.DatabaseConnectionString);
            //ShowHideMainMenu(false, false);
            //oWaitingAppointments.MdiParent = this;
            //oWaitingAppointments.WindowState = FormWindowState.Maximized;
            //oWaitingAppointments.ShowInTaskbar = false;
            //oWaitingAppointments.StartPosition = FormStartPosition.CenterParent;
            //oWaitingAppointments.Show();

            gloReports.C1Reports.frmCrystalRpt_AppointmentWaiting oWaitingAppointments = gloReports.C1Reports.frmCrystalRpt_AppointmentWaiting.GetInstance(gloPMGlobal.DatabaseConnectionString);

            oWaitingAppointments.MdiParent = this;
            oWaitingAppointments.WindowState = FormWindowState.Maximized;
            //oWaitingAppointments.ShowInTaskbar = false;
            //oWaitingAppointments.StartPosition = FormStartPosition.CenterParent;
            oWaitingAppointments.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnu_rpt_NoShowAppointments_Click(object sender, EventArgs e)
        {
            //No Show Appointments
            //gloAppointmentScheduling.frmRpt_NoShowAppointments ofrmRpt_NoShowAppointments = new gloAppointmentScheduling.frmRpt_NoShowAppointments(gloPMGlobal.DatabaseConnectionString);   
            try
            {
                //Commented By Pramod Nair For Implementing Crystal Reports
                //ofrmRpt_NoShowAppointments.MdiParent = this;
                //ofrmRpt_NoShowAppointments.Show();
                //ofrmRpt_NoShowAppointments.WindowState = FormWindowState.Maximized;
                //ofrmRpt_NoShowAppointments.ShowInTaskbar = false;
                //ofrmRpt_NoShowAppointments.StartPosition = FormStartPosition.CenterParent;
                //ShowHideMainMenu(false, false);

                //Added By Pramod Nair For Implementing Crystal Reports
                //     gloReports.C1Reports.frmRpt_CancelledAppointments oCancelledAppointments = gloReports.C1Reports.frmRpt_CancelledAppointments.GetInstance(gloPMGlobal.DatabaseConnectionString);

                gloReports.frmRPT_AppointmentView oCancelledAppointments = new gloReports.frmRPT_AppointmentView(gloPMGlobal.DatabaseConnectionString, "rptCancelAppointments", Program.gSQLServerName, Program.gDatabase, !Program.gWindowsAuthentication, Program.gLoginUser, Program.gLoginPassword, "gloPM");


                oCancelledAppointments.MdiParent = this;
                oCancelledAppointments.WindowState = FormWindowState.Maximized;
                //oCancelledAppointments.ShowInTaskbar = false;
                //oCancelledAppointments.StartPosition = FormStartPosition.CenterParent;
                oCancelledAppointments.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnu_rpt_PaymentTrayReport_Click(object sender, EventArgs e)
        {
            try
            {

                //Added By Pramod Nair For Implementing Crystal Reports
                gloReports.C1Reports.frmRpt_PaymentTray oCancelledAppointments = new gloReports.C1Reports.frmRpt_PaymentTray(gloPMGlobal.DatabaseConnectionString);
                oCancelledAppointments.MdiParent = this;
                oCancelledAppointments.WindowState = FormWindowState.Maximized;
                oCancelledAppointments.ShowInTaskbar = false;
                oCancelledAppointments.StartPosition = FormStartPosition.CenterParent;
                oCancelledAppointments.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnuEdit_Schedule_Click(object sender, EventArgs e)
        {
            try
            {

                gloAppointmentScheduling.gloSchedule oSchedule = new gloAppointmentScheduling.gloSchedule(gloPMGlobal.DatabaseConnectionString);
                oSchedule.ShowScheduleView(this);
                ShowHideMainMenu(false, false);
                oSchedule.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_DailyCollectionReport_Click(object sender, EventArgs e)
        {
            try
            {
                //frmRpt_DailyCollection ofrmRpt_DailyCollection = new frmRpt_DailyCollection(gloPMGlobal.DatabaseConnectionString);

                //ofrmRpt_DailyCollection.MdiParent = this;
                //ofrmRpt_DailyCollection.Show();
                //ShowHideMainMenu(false, false);
                //ofrmRpt_DailyCollection.WindowState = FormWindowState.Maximized;
                //ofrmRpt_DailyCollection.StartPosition = FormStartPosition.CenterParent;
                //ofrmRpt_DailyCollection.ShowInTaskbar = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuReports_ZeroBalancePatient_Click(object sender, EventArgs e)
        {

            //frmRpt_ZeroBalance ofrmRpt_ZeroBalance = new frmRpt_ZeroBalance(gloPMGlobal.DatabaseConnectionString);
            try
            {
                //Commented By Pramod Nair For Implementing Crystal Reports
                //ShowHideMainMenu(false, false);
                //ofrmRpt_ZeroBalance.MdiParent = this;
                //ofrmRpt_ZeroBalance.ShowInTaskbar = false;
                //ofrmRpt_ZeroBalance.StartPosition = FormStartPosition.CenterParent;
                //ofrmRpt_ZeroBalance.WindowState = FormWindowState.Maximized;
                //ofrmRpt_ZeroBalance.Show();

                //Added By Pramod Nair For Implementing Crystal Reports
                //gloReports.C1Reports.frmRpt_PatientBalance oPatientBalance = new gloReports.C1Reports.frmRpt_PatientBalance(gloPMGlobal.DatabaseConnectionString);

                //oPatientBalance.MdiParent = this;
                //oPatientBalance.WindowState = FormWindowState.Maximized;
                //oPatientBalance.ShowInTaskbar = false;
                //oPatientBalance.StartPosition = FormStartPosition.CenterParent;
                //oPatientBalance.Show();
                //ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //if (ofrmRpt_ZeroBalance != null)
                //{
                //    ofrmRpt_ZeroBalance.Dispose();
                //}
            }
        }

        private void mnuView_PatientTemplates_Click(object sender, EventArgs e)
        {
            try
            {
                //gloOffice.frmWd_ViewPatientTemplates frm = new gloOffice.frmWd_ViewPatientTemplates(_CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
                gloOffice.frmWd_ViewPatientTemplates frm = gloOffice.frmWd_ViewPatientTemplates.GetInstance(SyncPatient("Patient Forms"), gloPMGlobal.DatabaseConnectionString);
                //gloOffice.frmWd_ViewPatientTemplates frm = gloOffice.frmWd_ViewPatientTemplates.GetInstance(_CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
                //ShowHideMainMenu(false, false);
                frm.MdiParent = this;
                frm.ShowInTaskbar = false;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuView_PatientStatement_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    gloBilling.frmWd_ViewPatientStatement ofrmViewPatientStatement = gloBilling.frmWd_ViewPatientStatement.GetInstance(_CurrentPatientId, gloPMGlobal.DatabaseConnectionString);
            //   // ShowHideMainMenu(false, false);
            //    ofrmViewPatientStatement.MdiParent = this;
            //    ofrmViewPatientStatement.ShowInTaskbar = false;
            //    ofrmViewPatientStatement.StartPosition = FormStartPosition.CenterParent;
            //    ofrmViewPatientStatement.WindowState = FormWindowState.Maximized;
            //    ofrmViewPatientStatement.Show();
            //    ShowHideMainMenu(false, false); //Mantis bugs no: 1258
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void mnuRpt_BatchPrint_Click(object sender, EventArgs e)
        {
            try
            {
                gloOffice.frmBatchPrinting ofrm = new gloOffice.frmBatchPrinting(gloPMGlobal.DatabaseConnectionString);

                ofrm.MdiParent = this;
                ofrm.ShowInTaskbar = false;
                ofrm.StartPosition = FormStartPosition.CenterParent;
                ofrm.WindowState = FormWindowState.Maximized;
                ofrm.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmnu_PatientDetails_Opening(object sender, CancelEventArgs e)
        {

        }

        private void mnu_MissingChargesReport_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_MissingCharges ofrmRpt_MissingCharges = new frmRpt_MissingCharges(gloPMGlobal.DatabaseConnectionString);

                ofrmRpt_MissingCharges.MdiParent = this;
                ofrmRpt_MissingCharges.WindowState = FormWindowState.Maximized;
                ofrmRpt_MissingCharges.ShowInTaskbar = false;
                ofrmRpt_MissingCharges.StartPosition = FormStartPosition.CenterParent;
                ofrmRpt_MissingCharges.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void mnu_MonthEndReport_Click(object sender, EventArgs e)
        {
            try
            {

                frmRpt_MonthEnd ofrmRpt_MonthEnd = new frmRpt_MonthEnd(gloPMGlobal.DatabaseConnectionString);

                ofrmRpt_MonthEnd.MdiParent = this;
                ofrmRpt_MonthEnd.WindowState = FormWindowState.Maximized;
                ofrmRpt_MonthEnd.ShowInTaskbar = false;
                ofrmRpt_MonthEnd.StartPosition = FormStartPosition.CenterParent;
                ofrmRpt_MonthEnd.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void patientAlertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // IF CONDITION BY SUDHIR 20101029 //
                if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
                {
                    //Bug #81090: 00000879: deceased patient status
                    MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                gloPatientStripControl.frmPatientAlerts ofrmPatientAlerts = new gloPatientStripControl.frmPatientAlerts(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
                ofrmPatientAlerts.ShowDialog(this);
                ofrmPatientAlerts.Dispose();
                FillAlerts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuView_Tasks_Click(object sender, EventArgs e)
        {
            btnTask_Click(null, null);
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            try
            {
                //gloTaskMail.gloTask oViewTask = new gloTaskMail.gloTask(gloPMGlobal.DatabaseConnectionString);

                //oViewTask.ShowTask(this, false, _CurrentPatientId);
                //oViewTask.Dispose();
                //////FillTasks();

                frmViewTask ofrmViewTask = frmViewTask.GetInstance();
                ofrmViewTask.IsEMREnable = false;
                ofrmViewTask.PatientID = _CurrentPatientId;

                //Developer: Sanjog Dhamke
                //Date:10 Dec 2011
                //Bug ID/PRD Name/Sales force Case: Issue is - Handler is getting added on every button click on same form n Task Button event is fire multiple time 
                //Reason: So now we check that if instance is already created means handler is also added in it so don't add another extra handler for this form
                //ofrmViewTask.OnViewTask_Change -= new frmViewTask.OnViewTaskChange(ofrmViewTask_OnViewTask_Change);
                if (ofrmViewTask._HandlerPresent == false)
                {
                    ofrmViewTask.OnViewTaskModifiedClicked += new frmViewTask.ViewTaskModifiedClicked(OnPatientPaymentClicked);
                }
                ofrmViewTask._HandlerPresent = true;
                ofrmViewTask.WindowState = FormWindowState.Maximized;
                ofrmViewTask.MdiParent = this;
                ofrmViewTask.Show();

                ShowHideMainMenu(false, false);
            }
            catch (Exception)
            {
            }


        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                mnuView_Appointment_Click(sender, e);
            }
            catch (Exception)
            {
            }
        }

        private void btnCalender_Click(object sender, EventArgs e)
        {
            try
            {
                tsb_Appointment_Click(sender, e);
            }
            catch (Exception)
            {
            }
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            try
            {
                gloTaskMail.frmViewUserMail oViewUserMail = gloTaskMail.frmViewUserMail.GetInstance();
                oViewUserMail.MdiParent = this;

                oViewUserMail.WindowState = FormWindowState.Maximized;
                oViewUserMail.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception)
            {
            }
        }

        private void c1PatientAlerts_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                if (c1PatientAlerts.CursorCell.Data.ToString().Trim() != "Global Period in Effect")
                {
                    gloPatientStripControl.frmPatientAlerts ofrmPatientAlerts = new gloPatientStripControl.frmPatientAlerts(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
                    ofrmPatientAlerts.ShowDialog(this);
                    ofrmPatientAlerts.Dispose();
                    FillAlerts();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guarantorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewPatientList ofrm = new frmViewPatientList(gloPMGlobal.DatabaseConnectionString, "Guarantor");

            ofrm.MdiParent = this;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowInTaskbar = false;
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.Show();
            ShowHideMainMenu(false, false);
        }

        private void patientToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmViewPatientList ofrm = new frmViewPatientList(gloPMGlobal.DatabaseConnectionString, "Patient");

            ofrm.MdiParent = this;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowInTaskbar = false;
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.Show();
            ShowHideMainMenu(false, false);
        }

        private void insurancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewPatientList ofrm = new frmViewPatientList(gloPMGlobal.DatabaseConnectionString, "Insurances");

            ofrm.MdiParent = this;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowInTaskbar = false;
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.Show();
            ShowHideMainMenu(false, false);
        }

        private void billingCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewPatientList ofrm = new frmViewPatientList(gloPMGlobal.DatabaseConnectionString, "Billing Code");

            ofrm.MdiParent = this;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowInTaskbar = false;
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.Show();
            ShowHideMainMenu(false, false);
        }

        private void diagnosisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewPatientList ofrm = new frmViewPatientList(gloPMGlobal.DatabaseConnectionString, "Diagnosis");

            ofrm.MdiParent = this;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowInTaskbar = false;
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.Show();
            ShowHideMainMenu(false, false);
        }


        private void btn_Appointment_Click(object sender, EventArgs e)
        {
            btn_Appointment.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
            btn_Appointment.Tag = "Selected";

            btn_Calendar.BackgroundImage = global::gloPM.Properties.Resources.Img_LongBlueBtn;
            btn_Calendar.Tag = "UnSelected";

            btn_Tasks.BackgroundImage = global::gloPM.Properties.Resources.Img_LongBlueBtn;
            btn_Tasks.Tag = "UnSelected";

            btn_Appointment.BackgroundImageLayout = ImageLayout.Stretch;

            pnlSideButton.Refresh();

            try
            {
                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
                {
                    return;
                }
                tsb_Appointment_Click(sender, e);
            }
            catch (Exception)
            {
            }


        }

        private void btn_Calendar_Click(object sender, EventArgs e)
        {

            btn_Calendar.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
            btn_Calendar.Tag = "Selected";

            btn_Tasks.BackgroundImage = global::gloPM.Properties.Resources.Img_LongBlueBtn;
            btn_Tasks.Tag = "UnSelected";

            btn_Appointment.BackgroundImage = global::gloPM.Properties.Resources.Img_LongBlueBtn;
            btn_Appointment.Tag = "UnSelected";
            btn_Calendar.BackgroundImageLayout = ImageLayout.Stretch;

            pnlSideButton.Refresh();

            try
            {
                mnuView_Appointment_Click(sender, e);
            }
            catch (Exception)
            {
            }

        }

        private void btn_Tasks_Click(object sender, EventArgs e)
        {
            btn_Tasks.BackgroundImage = global::gloPM.Properties.Resources.Img_LongOrange;
            btn_Tasks.Tag = "Selected";

            btn_Calendar.BackgroundImage = global::gloPM.Properties.Resources.Img_LongBlueBtn;
            btn_Calendar.Tag = "UnSelected";

            btn_Appointment.BackgroundImage = global::gloPM.Properties.Resources.Img_LongBlueBtn;
            btn_Appointment.Tag = "UnSelected";
            btn_Tasks.BackgroundImageLayout = ImageLayout.Stretch;

            pnlSideButton.Refresh();

            try
            {
                //gloTaskMail.gloTask oViewTask = new gloTaskMail.gloTask(gloPMGlobal.DatabaseConnectionString);
                //oViewTask.ShowTask(this, false, _CurrentPatientId);
                //oViewTask.Dispose();
                //FillTasks();

                frmViewTask ofrmViewTask = frmViewTask.GetInstance();
                ofrmViewTask.IsEMREnable = false;
                ofrmViewTask.PatientID = _CurrentPatientId;

                //Developer: Sanjog Dhamke
                //Date:10 Dec 2011
                //Bug ID/PRD Name/Sales force Case: Issue is - Handler is getting added on every button click on same form n Task Button event is fire multiple time 
                //Reason: So now we check that if instance is already created means handler is also added in it so don't add another extra handler for this form
                //ofrmViewTask.OnViewTask_Change -= new frmViewTask.OnViewTaskChange(ofrmViewTask_OnViewTask_Change);
                if (ofrmViewTask._HandlerPresent == false)
                {
                    ofrmViewTask.OnViewTaskModifiedClicked += new frmViewTask.ViewTaskModifiedClicked(OnPatientPaymentClicked);
                }
                ofrmViewTask._HandlerPresent = true;
                ofrmViewTask.WindowState = FormWindowState.Maximized;
                ofrmViewTask.MdiParent = this;
                ofrmViewTask.Show();

                ShowHideMainMenu(false, false);

            }
            catch (Exception)
            {
            }

        }

        private void btn_MouseHover(object sender, EventArgs e)
        {

            if (((Button)sender).Tag.ToString() == "UnSelected")
            {
                ((Button)sender).BackgroundImage = global::gloPM.Properties.Resources.Img_LongYellow;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {

            if (((Button)sender).Tag.ToString() == "UnSelected")
            {

                ((Button)sender).BackgroundImage = global::gloPM.Properties.Resources.Img_LongBlueBtn;
                ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            }

        }

        private void mnuReports_AuditTrail_Click(object sender, EventArgs e)
        {
            gloAuditTrail.frmRpt_AuditTrail oAuditTrail = new gloAuditTrail.frmRpt_AuditTrail(gloPMGlobal.DatabaseConnectionString);

            oAuditTrail.MdiParent = this;
            oAuditTrail.WindowState = FormWindowState.Maximized;
            oAuditTrail.ShowInTaskbar = false;
            oAuditTrail.StartPosition = FormStartPosition.CenterParent;
            oAuditTrail.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuReportsRefund_Click(object sender, EventArgs e)
        {

            //Added By Pramod Nair For Implementing Crystal Reports
            //gloReports.C1Reports.frmRpt_Refund oRefund = new gloReports.C1Reports.frmRpt_Refund(gloPMGlobal.DatabaseConnectionString);

            //oRefund.MdiParent = this;
            //oRefund.WindowState = FormWindowState.Maximized;
            //oRefund.ShowInTaskbar = false;
            //oRefund.StartPosition = FormStartPosition.CenterParent;
            //oRefund.Show();
            //ShowHideMainMenu(false, false);
        }

        //Added By Pramod Nair For Pending Copay Report
        private void mnuReportsPendingCopayReport_Click(object sender, EventArgs e)
        {
            //gloBilling.frmRpt_PendingCopayReport oPendingCopayReport = new frmRpt_PendingCopayReport(gloPMGlobal.DatabaseConnectionString);

            //oPendingCopayReport.MdiParent = this;
            //oPendingCopayReport.WindowState = FormWindowState.Maximized;
            //oPendingCopayReport.ShowInTaskbar = false;
            //oPendingCopayReport.StartPosition = FormStartPosition.CenterParent;
            //oPendingCopayReport.Show();
            ShowHideMainMenu(false, false);
        }

        #region Display Setting

        //System.Windows.Forms.SystemInformation.ComputerName();

        private void SaveDisplaySettings(bool IsDefaultSettings)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                string sMachineName = "";
                Int64 _nUserID = 0;

                if (IsDefaultSettings == false)
                {
                    _nUserID = gloPMGlobal.UserID;

                    //Get Machine Name
                    sMachineName = System.Windows.Forms.SystemInformation.ComputerName;

                }

                //Save Display Settings To MemoryStream
                MemoryStream memStream = new MemoryStream();
                uiPanelManager1.SaveLayoutFile(memStream);

                oDB.Connect(false);

                oParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sMachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@iDisplayStyle", memStream.ToArray(), ParameterDirection.Input, SqlDbType.Image); //SLR: instead of getbuffer, it should be toArray?
                oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("gsp_InUpDisplaySettings", oParameters);

                oDB.Disconnect();

                memStream.Dispose();


            }
            catch (gloDatabaseLayer.DBException odbex)
            {
                odbex.ERROR_Log(odbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //SLR: FRee memstream, oParamenrtes
                if (oParameters != null)
                {
                    oParameters.Dispose();
                }
            }
        }

        private void LoadDisplaySettings(bool IsDefaultSettings)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {

                Form[] oForms = this.MdiChildren;

                if (oForms.Length == 0)
                {

                    string sMachineName = "";
                    Int64 _nUserID = 0;

                    if (IsDefaultSettings == false)
                    {

                        _nUserID = gloPMGlobal.UserID;
                        //Get Machine Name
                        sMachineName = System.Windows.Forms.SystemInformation.ComputerName;
                    }

                    oDB.Connect(false);

                    DataTable dtSettings = null;
                    string _sqlQuery = "";
                    _sqlQuery = " SELECT iDisplayStyle FROM DisplaySettings WHERE nUserID = " + _nUserID + " AND nClinicID = " + gloPMGlobal.ClinicID + " AND sMachineName = '" + sMachineName + "'";
                    oDB.Retrive_Query(_sqlQuery, out dtSettings);

                    if (dtSettings != null)
                    {
                        if (dtSettings.Rows.Count > 0)
                        {
                            if (DBNull.Value != dtSettings.Rows[0]["iDisplayStyle"])
                            {
                                Byte[] oBytesArry = (Byte[])dtSettings.Rows[0]["iDisplayStyle"];
                                MemoryStream memStream = new MemoryStream(oBytesArry);

                                uiPanelManager1.LoadLayoutFile(memStream);
                                uiPanelManager1.ResumePanelLayout();

                                memStream.Close();
                                memStream.Dispose();
                                memStream = null;
                            }
                        }
                        dtSettings.Dispose();
                    }

                    // Get the General Patient Search Setting from the Setting Table
                    gloCntrlPatient.SetGeneralSearch(false);
                    _sqlQuery = "";
                    object _sqlresult = null;
                    _sqlQuery = " SELECT sSettingsValue FROM Settings WHERE sSettingsName = 'DashboardPatientGeneralSearchEnabled' AND nUserID = " + _nUserID + " AND nClinicID = " + gloPMGlobal.ClinicID + " AND nUserClinicFlag = 2";
                    _sqlresult = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_sqlresult != null && _sqlresult.ToString() != "")
                    {
                        if (Convert.ToInt16(_sqlresult.ToString()) > 0)
                        {
                            gloCntrlPatient.SetGeneralSearch(true);
                        }
                    }
                    if (_sqlresult != null)
                    {
                        _sqlresult = null;
                    }
                    //
                    oDB.Disconnect();
                }
                else
                {
                    MessageBox.Show("Please close all open screens to restore dashbord settings.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (gloDatabaseLayer.DBException odbex)
            {
                odbex.ERROR_Log(odbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }

            }
        }



        #endregion

        private void pnlPatient_Demographics_Click(object sender, EventArgs e)
        {

        }

        private void mnuEdit_TemplateAssociation_Click(object sender, EventArgs e)
        {
            gloOffice.frmSetupTemplateGalleryAssociation oSTGA = new gloOffice.frmSetupTemplateGalleryAssociation(gloPMGlobal.DatabaseConnectionString);
            try
            {
                oSTGA.ShowDialog(this);
                oSTGA.Dispose();
                oSTGA = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnu_ChargesPayments_Click(object sender, EventArgs e)
        {
            try
            {
                //Commented By Pramod Nair For Implementing Crystal Reports
                //gloBilling.frmRpt_ChargesPayments oChargesPayments = new frmRpt_ChargesPayments(gloPMGlobal.DatabaseConnectionString);
                //ShowHideMainMenu(false, false);
                //oChargesPayments.MdiParent = this;
                //oChargesPayments.WindowState = FormWindowState.Maximized;
                //oChargesPayments.ShowInTaskbar = false;
                //oChargesPayments.StartPosition = FormStartPosition.CenterParent;
                //oChargesPayments.Show();

                //Added By Pramod Nair For Implementing Crystal Reports
                gloReports.C1Reports.frmRpt_CashFlow oCashFlow = new gloReports.C1Reports.frmRpt_CashFlow(gloPMGlobal.DatabaseConnectionString);

                oCashFlow.MdiParent = this;
                oCashFlow.WindowState = FormWindowState.Maximized;
                oCashFlow.ShowInTaskbar = false;
                oCashFlow.StartPosition = FormStartPosition.CenterParent;
                oCashFlow.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void mnuTools_CardImage_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr sc_activeWindow = gloWord.WordDialogBoxBackgroundCloser.GetForegroundWindow();
                gloCardScanning.frmCardImage ofrmCardImage = new gloCardScanning.frmCardImage(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId, sc_activeWindow);
                ofrmCardImage.StartPosition = FormStartPosition.CenterScreen;
                ofrmCardImage.ShowDialog(this);
                ofrmCardImage.Dispose();
                FillCardInformation();  //added on 14/05/2010 refresh card info
                FillDemographicInformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuTools_UnLockRecords_Click(object sender, EventArgs e)
        {
            try
            {
                gloPMGeneral.frmUnlockRecords ofrmUnlockRecords = new gloPMGeneral.frmUnlockRecords(gloPMGlobal.DatabaseConnectionString);
                //ShowHideMainMenu(false, false);
                ofrmUnlockRecords.MdiParent = this;
                ofrmUnlockRecords.WindowState = FormWindowState.Maximized;
                ofrmUnlockRecords.ShowInTaskbar = false;
                ofrmUnlockRecords.StartPosition = FormStartPosition.CenterParent;
                ofrmUnlockRecords.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuSetting_Customization_Click(object sender, EventArgs e)
        {
            gloSettings.frmToolButtonSelection ofrmToolButtonSelection = new gloSettings.frmToolButtonSelection(gloPMGlobal.DatabaseConnectionString, ts_Main, gloSettings.enumModuleName.Dashboard, arrDashBoardToolStrip);
            ofrmToolButtonSelection.StartPosition = FormStartPosition.CenterScreen;
            if (arrDashBoardToolStrip == null)
            { arrDashBoardToolStrip = ofrmToolButtonSelection.GetDefaultToolStripSetting(); }
            ofrmToolButtonSelection.ShowDialog(this);
            ofrmToolButtonSelection.Dispose();


        }

        private void SetToolBar()
        {
            gloSettings.frmToolButtonSelection ofrmToolButtonSelection = new gloSettings.frmToolButtonSelection(gloPMGlobal.DatabaseConnectionString, ts_Main, gloSettings.enumModuleName.Dashboard, null);
            try
            {
                if (arrDashBoardToolStrip == null)
                {
                    arrDashBoardToolStrip = ofrmToolButtonSelection.GetDefaultToolStripSetting();
                }
                if (arrDashBoardToolStrip != null)
                {
                    ofrmToolButtonSelection.ShowButtonSelection();
                }
                //SLR: Free previoisuly allocated memory before alloacting again
                if (ofrmToolButtonSelection != null)
                {
                    ofrmToolButtonSelection.Dispose();
                }
                ofrmToolButtonSelection = new gloSettings.frmToolButtonSelection(gloPMGlobal.DatabaseConnectionString, ts_PatientDetail, gloSettings.enumModuleName.PatientDetails, null);
                if (arrPatientdetailsToolStrip == null)
                {
                    arrPatientdetailsToolStrip = ofrmToolButtonSelection.GetDefaultToolStripSetting();
                }
                if (arrPatientdetailsToolStrip != null)
                {
                    ofrmToolButtonSelection.ShowButtonSelection();
                }

                #region "Check For Follow Up Feature Enabled or Not "

                Boolean SettingsValue = false;
                object oValue = null;
                gloSettings.GeneralSettings ogloSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                ogloSettings.GetSetting("FOLLOWUP_FEATURE", 0, gloGlobal.gloPMGlobal.ClinicID, out oValue);
                if (Convert.ToString(oValue).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValue).ToLower().Trim() == "False".ToLower())
                {
                    SettingsValue = Convert.ToBoolean(oValue);
                }
                else if (Convert.ToString(oValue).Trim() == "1" || Convert.ToString(oValue).Trim() == "0")
                {
                    SettingsValue = Convert.ToBoolean(Convert.ToString(oValue).Trim() == "1" ? "TRUE" : "FALSE");
                }
                if (ogloSettings != null) { ogloSettings.Dispose(); }
                if (oValue != null)
                {
                    oValue = null;
                }
                if (!SettingsValue)
                {
                    tsb_RevenueCycle.Visible = false;
                    mnuGo_RevenueCycle.Visible = false;
                }

                if (gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_Feature"))
                    mnuBusCenterMismatch.Visible = true;
                else
                    mnuBusCenterMismatch.Visible = false;


                #endregion

                //MaheshB Payment button visible false.
                tsb_Payment.Visible = false;
                if (gloGlobal.gloPMGlobal.IsCleargageEnable == true)
                {
                    tsb_ClearGagePayment.Visible = true;
                }
                else
                {
                    tsb_ClearGagePayment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                ofrmToolButtonSelection.Dispose();
            }
        }

        #region " Patient Based Reports "

        private void mnu_MISReports_PatientPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_PatientPaymentHistory oPatientPayHistory = new frmRpt_PatientPaymentHistory(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
                oPatientPayHistory.MdiParent = this;
                oPatientPayHistory.WindowState = FormWindowState.Maximized;
                oPatientPayHistory.ShowInTaskbar = false;
                oPatientPayHistory.StartPosition = FormStartPosition.CenterParent;
                oPatientPayHistory.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_PatientTransactionHistory_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_PatientTransactionHistory oPatientTransHistory = new frmRpt_PatientTransactionHistory(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
                oPatientTransHistory.MdiParent = this;
                oPatientTransHistory.WindowState = FormWindowState.Maximized;
                oPatientTransHistory.ShowInTaskbar = false;
                oPatientTransHistory.StartPosition = FormStartPosition.CenterParent;
                oPatientTransHistory.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_Patientstatement_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    gloBilling.Statement.frmRpt_PatientStatement oPatientStatementForGateWayEDI = new gloBilling.Statement.frmRpt_PatientStatement(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
            //    oPatientStatementForGateWayEDI.MdiParent = this;
            //    oPatientStatementForGateWayEDI.WindowState = FormWindowState.Maximized;
            //    oPatientStatementForGateWayEDI.ShowInTaskbar = false;
            //    oPatientStatementForGateWayEDI.StartPosition = FormStartPosition.CenterParent;
            //    oPatientStatementForGateWayEDI.Show();
            //    ShowHideMainMenu(false, false);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //finally
            //{

            //}

        }

        #endregion

        #region " Production Based Reports "


        private void mnu_MISReports_ProductionByDoctor_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByDoctor oProductionByDoctor = new frmRpt_ProductionByDoctor(gloPMGlobal.DatabaseConnectionString);
                oProductionByDoctor.MdiParent = this;
                oProductionByDoctor.WindowState = FormWindowState.Maximized;
                oProductionByDoctor.ShowInTaskbar = false;
                oProductionByDoctor.StartPosition = FormStartPosition.CenterParent;
                oProductionByDoctor.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ProductionByFacility_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByFacility oProductionByFacility = new frmRpt_ProductionByFacility(gloPMGlobal.DatabaseConnectionString);
                oProductionByFacility.MdiParent = this;
                oProductionByFacility.WindowState = FormWindowState.Maximized;
                oProductionByFacility.ShowInTaskbar = false;
                oProductionByFacility.StartPosition = FormStartPosition.CenterParent;
                oProductionByFacility.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ProductionByDate_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByDate oProductionByDate = new frmRpt_ProductionByDate(gloPMGlobal.DatabaseConnectionString);
                oProductionByDate.MdiParent = this;
                oProductionByDate.WindowState = FormWindowState.Maximized;
                oProductionByDate.ShowInTaskbar = false;
                oProductionByDate.StartPosition = FormStartPosition.CenterParent;
                oProductionByDate.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ProductionByMonth_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByMonth oProductionByMonth = new frmRpt_ProductionByMonth(gloPMGlobal.DatabaseConnectionString);
                oProductionByMonth.MdiParent = this;
                oProductionByMonth.WindowState = FormWindowState.Maximized;
                oProductionByMonth.ShowInTaskbar = false;
                oProductionByMonth.StartPosition = FormStartPosition.CenterParent;
                oProductionByMonth.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ProductionByProcedureCode_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByProcedureCode oProductionByCPT = new frmRpt_ProductionByProcedureCode(gloPMGlobal.DatabaseConnectionString);
                oProductionByCPT.MdiParent = this;
                oProductionByCPT.WindowState = FormWindowState.Maximized;
                oProductionByCPT.ShowInTaskbar = false;
                oProductionByCPT.StartPosition = FormStartPosition.CenterParent;
                oProductionByCPT.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_ProductionByProcedureGroup_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByProcedureGroup oProductionByProcedureGroup = new frmRpt_ProductionByProcedureGroup(gloPMGlobal.DatabaseConnectionString);
                oProductionByProcedureGroup.MdiParent = this;
                oProductionByProcedureGroup.WindowState = FormWindowState.Maximized;
                oProductionByProcedureGroup.ShowInTaskbar = false;
                oProductionByProcedureGroup.StartPosition = FormStartPosition.CenterParent;
                oProductionByProcedureGroup.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ProductionByInsuranceCarrier_Click(object sender, EventArgs e)
        {
            frmRpt_ProductionByInsuranceCarrier oProductionByInsCarrier = new frmRpt_ProductionByInsuranceCarrier(gloPMGlobal.DatabaseConnectionString);
            oProductionByInsCarrier.MdiParent = this;
            oProductionByInsCarrier.WindowState = FormWindowState.Maximized;
            oProductionByInsCarrier.ShowInTaskbar = false;
            oProductionByInsCarrier.StartPosition = FormStartPosition.CenterParent;
            oProductionByInsCarrier.Show();
            ShowHideMainMenu(false, false);


        }

        private void mnu_MISReports_ProductionByFacilityByPatient_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByFacilityByPatient oProductionByFacilityByPatient = new frmRpt_ProductionByFacilityByPatient(gloPMGlobal.DatabaseConnectionString);
                oProductionByFacilityByPatient.MdiParent = this;
                oProductionByFacilityByPatient.WindowState = FormWindowState.Maximized;
                oProductionByFacilityByPatient.ShowInTaskbar = false;
                oProductionByFacilityByPatient.StartPosition = FormStartPosition.CenterParent;
                oProductionByFacilityByPatient.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_ProductionByFacilityByPatientDetail_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByFacilityByPatient_Details oProductionByFacilityByPatientDetails = new frmRpt_ProductionByFacilityByPatient_Details(gloPMGlobal.DatabaseConnectionString);
                oProductionByFacilityByPatientDetails.MdiParent = this;
                oProductionByFacilityByPatientDetails.WindowState = FormWindowState.Maximized;
                oProductionByFacilityByPatientDetails.ShowInTaskbar = false;
                oProductionByFacilityByPatientDetails.StartPosition = FormStartPosition.CenterParent;
                oProductionByFacilityByPatientDetails.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }


        #endregion

        #region " Re-imbursemennt Based Reports "


        private void mnu_MISReports_ReimbursementByMonth_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReimbursementByMonth oReimbursementByMonth = new frmRpt_ReimbursementByMonth(gloPMGlobal.DatabaseConnectionString);
                oReimbursementByMonth.MdiParent = this;
                oReimbursementByMonth.WindowState = FormWindowState.Maximized;
                oReimbursementByMonth.ShowInTaskbar = false;
                oReimbursementByMonth.StartPosition = FormStartPosition.CenterParent;
                oReimbursementByMonth.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_ReimbursementByInsuranceCarrier_Click(object sender, EventArgs e)
        {
            try
            {
                frmRptReimbursementByInsuranceCarrier oReimbursementByInsCarrier = new frmRptReimbursementByInsuranceCarrier(gloPMGlobal.DatabaseConnectionString);
                oReimbursementByInsCarrier.MdiParent = this;
                oReimbursementByInsCarrier.WindowState = FormWindowState.Maximized;
                oReimbursementByInsCarrier.ShowInTaskbar = false;
                oReimbursementByInsCarrier.StartPosition = FormStartPosition.CenterParent;
                oReimbursementByInsCarrier.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ReimbursementByInsuranceByCPT_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReimbursementByCarrierByCPT oReimbursementByInsuranceByCPT = new frmRpt_ReimbursementByCarrierByCPT(gloPMGlobal.DatabaseConnectionString);
                oReimbursementByInsuranceByCPT.MdiParent = this;
                oReimbursementByInsuranceByCPT.WindowState = FormWindowState.Maximized;
                oReimbursementByInsuranceByCPT.ShowInTaskbar = false;
                oReimbursementByInsuranceByCPT.StartPosition = FormStartPosition.CenterParent;
                oReimbursementByInsuranceByCPT.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_ReimbursementByCPTByInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReimbursementByCPTByCarrier oReimbursementByCPTByCarrier = new frmRpt_ReimbursementByCPTByCarrier(gloPMGlobal.DatabaseConnectionString);
                oReimbursementByCPTByCarrier.MdiParent = this;
                oReimbursementByCPTByCarrier.WindowState = FormWindowState.Maximized;
                oReimbursementByCPTByCarrier.ShowInTaskbar = false;
                oReimbursementByCPTByCarrier.StartPosition = FormStartPosition.CenterParent;
                oReimbursementByCPTByCarrier.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ReimbursementByDoctorByInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReimbursementByDoctorByInsurance oReimbursementByDoctorByInsurance = new frmRpt_ReimbursementByDoctorByInsurance(gloPMGlobal.DatabaseConnectionString);
                oReimbursementByDoctorByInsurance.MdiParent = this;
                oReimbursementByDoctorByInsurance.WindowState = FormWindowState.Maximized;
                oReimbursementByDoctorByInsurance.ShowInTaskbar = false;
                oReimbursementByDoctorByInsurance.StartPosition = FormStartPosition.CenterParent;
                oReimbursementByDoctorByInsurance.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_ReimbursementByMonthDetail_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReimbursementByMonthByAccount oReimbursementByMonthByAccount = new frmRpt_ReimbursementByMonthByAccount(gloPMGlobal.DatabaseConnectionString);
                oReimbursementByMonthByAccount.MdiParent = this;
                oReimbursementByMonthByAccount.WindowState = FormWindowState.Maximized;
                oReimbursementByMonthByAccount.ShowInTaskbar = false;
                oReimbursementByMonthByAccount.StartPosition = FormStartPosition.CenterParent;
                oReimbursementByMonthByAccount.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ReimbursementByInsuranceForCPT_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReimbursementByInsuranceForParticularCPT oReimbursementByInsuranceForParticularCPT = new frmRpt_ReimbursementByInsuranceForParticularCPT(gloPMGlobal.DatabaseConnectionString);
                oReimbursementByInsuranceForParticularCPT.MdiParent = this;
                oReimbursementByInsuranceForParticularCPT.WindowState = FormWindowState.Maximized;
                oReimbursementByInsuranceForParticularCPT.ShowInTaskbar = false;
                oReimbursementByInsuranceForParticularCPT.StartPosition = FormStartPosition.CenterParent;
                oReimbursementByInsuranceForParticularCPT.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_ReimbursementDetailsByAccount_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ReimbursementDetailByAccount oReimbursementDetailByAccount = new frmRpt_ReimbursementDetailByAccount(gloPMGlobal.DatabaseConnectionString);
                oReimbursementDetailByAccount.MdiParent = this;
                oReimbursementDetailByAccount.WindowState = FormWindowState.Maximized;
                oReimbursementDetailByAccount.ShowInTaskbar = false;
                oReimbursementDetailByAccount.StartPosition = FormStartPosition.CenterParent;
                oReimbursementDetailByAccount.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }


        #endregion

        #region " Aging Based Reports "

        private void mnu_MISReports_AgingSummaryByPatient_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_AgingSummaryByPatient oAgingSummaryByPatient = new frmRpt_AgingSummaryByPatient(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
                oAgingSummaryByPatient.MdiParent = this;
                oAgingSummaryByPatient.WindowState = FormWindowState.Maximized;
                oAgingSummaryByPatient.ShowInTaskbar = false;
                oAgingSummaryByPatient.StartPosition = FormStartPosition.CenterParent;
                oAgingSummaryByPatient.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_AgingSummaryByInsuranceCarrier_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_AgingSummaryByInsuranceCarrier oAgingSummaryByInsCarrier = new frmRpt_AgingSummaryByInsuranceCarrier(gloPMGlobal.DatabaseConnectionString);
                oAgingSummaryByInsCarrier.MdiParent = this;
                oAgingSummaryByInsCarrier.WindowState = FormWindowState.Maximized;
                oAgingSummaryByInsCarrier.ShowInTaskbar = false;
                oAgingSummaryByInsCarrier.StartPosition = FormStartPosition.CenterParent;
                oAgingSummaryByInsCarrier.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_AgingAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_AgingAnalysis oAgingAnalysis = new frmRpt_AgingAnalysis(gloPMGlobal.DatabaseConnectionString);
                oAgingAnalysis.MdiParent = this;
                oAgingAnalysis.WindowState = FormWindowState.Maximized;
                oAgingAnalysis.ShowInTaskbar = false;
                oAgingAnalysis.StartPosition = FormStartPosition.CenterParent;
                oAgingAnalysis.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }


        #endregion

        #region "Analysis Reports"

        private void mnu_MISReports_ProductionByPhysicianGroup_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionByPhysicianGroup oProductionByPhysicianGroup = new frmRpt_ProductionByPhysicianGroup(gloPMGlobal.DatabaseConnectionString);
                oProductionByPhysicianGroup.MdiParent = this;
                oProductionByPhysicianGroup.WindowState = FormWindowState.Maximized;
                oProductionByPhysicianGroup.ShowInTaskbar = false;
                oProductionByPhysicianGroup.StartPosition = FormStartPosition.CenterParent;
                oProductionByPhysicianGroup.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }


        private void mnu_MISReports_ProductionAnalysisByFacility_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionAnalysisByFacility oProductionAnalysisByFacility = new frmRpt_ProductionAnalysisByFacility(gloPMGlobal.DatabaseConnectionString);
                oProductionAnalysisByFacility.MdiParent = this;
                oProductionAnalysisByFacility.WindowState = FormWindowState.Maximized;
                oProductionAnalysisByFacility.ShowInTaskbar = false;
                oProductionAnalysisByFacility.StartPosition = FormStartPosition.CenterParent;
                oProductionAnalysisByFacility.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }


        private void mnu_MISReports_ProductionAnalysisByprocedureGroup_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionAnalysisByProcedureGroup oProductionAnalysisByProcedureGroup = new frmRpt_ProductionAnalysisByProcedureGroup(gloPMGlobal.DatabaseConnectionString);
                oProductionAnalysisByProcedureGroup.MdiParent = this;
                oProductionAnalysisByProcedureGroup.WindowState = FormWindowState.Maximized;
                oProductionAnalysisByProcedureGroup.ShowInTaskbar = false;
                oProductionAnalysisByProcedureGroup.StartPosition = FormStartPosition.CenterParent;
                oProductionAnalysisByProcedureGroup.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_ProductionAnalysisandTrendsByMonth_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionAnalysisAndTrendsByMonth oProductionAnalysisAndTrendsByMonth = new frmRpt_ProductionAnalysisAndTrendsByMonth(gloPMGlobal.DatabaseConnectionString);
                oProductionAnalysisAndTrendsByMonth.MdiParent = this;
                oProductionAnalysisAndTrendsByMonth.WindowState = FormWindowState.Maximized;
                oProductionAnalysisAndTrendsByMonth.ShowInTaskbar = false;
                oProductionAnalysisAndTrendsByMonth.StartPosition = FormStartPosition.CenterParent;
                oProductionAnalysisAndTrendsByMonth.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }

        }

        private void mnu_MISReports_ProductionTrendsByProcedureGrop_Click(object sender, EventArgs e)
        {
            try
            {
                frmRpt_ProductionTrendsByProcedureGroup oProductionTrendsByProcedureGroup = new frmRpt_ProductionTrendsByProcedureGroup(gloPMGlobal.DatabaseConnectionString);
                oProductionTrendsByProcedureGroup.MdiParent = this;
                oProductionTrendsByProcedureGroup.WindowState = FormWindowState.Maximized;
                oProductionTrendsByProcedureGroup.ShowInTaskbar = false;
                oProductionTrendsByProcedureGroup.StartPosition = FormStartPosition.CenterParent;
                oProductionTrendsByProcedureGroup.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }



        #endregion

        private void mnuWindow_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ShowHideMainMenu(false, false);
        }

        #region "Generate HL7 for Patient"

        private void InsertInMessageQueue(string strMessageName, Int64 PatientID, Int64 OtherID, string _ConnectionString)
        {
            //SqlConnection conn = default(SqlConnection);
            //SqlCommand cmd = default(SqlCommand);
            gloDatabaseLayer.DBLayer oDBLayer;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParamters = new gloDatabaseLayer.DBParameters();
                oDBLayer.Connect(false);

                oDBParamters.Add("@dtDatetimeStamp", DateAndTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParamters.Add("@MessageName", strMessageName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachineID", "1", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@nID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int, 1);
                // string strTestName = "";

                oDBParamters.Add("@sField1", "", ParameterDirection.Input, SqlDbType.VarChar, 5000);
                oDBParamters.Add("@MachineID", oDBLayer.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);

                oDBLayer.Execute("HL7_InsertMessageQueue", oDBParamters);
                oDBLayer.Disconnect();
                oDBLayer.Dispose();
                //SLR: FRee odbParamenters
                oDBParamters.Dispose();

                //conn = new SqlConnection(gloPMGlobal.DatabaseConnectionString);
                //cmd = new SqlCommand("HL7_InsertMessageQueue", conn);
                //cmd.CommandType = CommandType.StoredProcedure;

                //conn.Open();
                //SqlParameter objParam = default(SqlParameter);
                //cmd.Parameters.Clear();
                //objParam = cmd.Parameters.Add("@dtDatetimeStamp", SqlDbType.DateTime);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = Now;


                //objParam = null;

                //objParam = cmd.Parameters.Add("@MessageName", SqlDbType.VarChar, 50);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = strMessageName;



                //objParam = cmd.Parameters.Add("@sMachineID", SqlDbType.VarChar, 50);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = "1";

                //objParam = cmd.Parameters.Add("@sMachinename", SqlDbType.VarChar, 50);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = System.Environment.MachineName;


                //objParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = PatientID;


                //objParam = cmd.Parameters.Add("@nID", SqlDbType.BigInt);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = OtherID;


                //objParam = cmd.Parameters.Add("@Status", SqlDbType.Int);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = 1;


                //objParam = cmd.Parameters.Add("@sField1", SqlDbType.VarChar, 5000);
                //objParam.Direction = ParameterDirection.Input;

                //objParam.Value = strTestName;

                //objParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt);
                //objParam.Direction = ParameterDirection.Input;
                //objParam.Value = GetPrefixTransactionID();


                //cmd.Connection = conn;
                //cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                // gloAuditTrail.gloAuditTrail.UpdateLog (gloAuditTrail.ActivityModule.Dashboard,gloAuditTrail.ActivityCategory.PatientDetail,gloAuditTrail.ActivityType.Add,ex.ToString,gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                //if (oDBLayer != null)
                //{
                //    oDBLayer.Dispose(); 
                //}
                //if ((conn != null))
                //{
                //    if (conn.State == ConnectionState.Open)
                //    {
                //        conn.Close();
                //    }
                //    conn.Dispose();
                //    conn = null;
                //}

            }
        }

        #endregion

        #region "ToolStrip Customize"

        private void ts_Main_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ts_Main.ContextMenuStrip = cmnuToolStripCustomize;
                    cmnuToolStripCustomize.Tag = ts_Main.Name;
                }
                else
                {
                    ts_Main.ContextMenuStrip = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void ts_PatientDetail_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ts_PatientDetail.ContextMenuStrip = cmnuToolStripCustomize;
                    cmnuToolStripCustomize.Tag = ts_PatientDetail.Name;
                }
                else
                {
                    ts_PatientDetail.ContextMenuStrip = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }

        private void cmnuToolStripCustomize_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(cmnuToolStripCustomize.Tag) == ts_PatientDetail.Name)
                {
                    //Patient details custumization
                    gloSettings.frmToolButtonSelection ofrmToolButtonSelection = new gloSettings.frmToolButtonSelection(gloPMGlobal.DatabaseConnectionString, ts_PatientDetail, gloSettings.enumModuleName.PatientDetails, arrPatientdetailsToolStrip);
                    ofrmToolButtonSelection.StartPosition = FormStartPosition.CenterScreen;
                    if (arrDashBoardToolStrip == null)
                    { arrDashBoardToolStrip = ofrmToolButtonSelection.GetDefaultToolStripSetting(); }
                    ofrmToolButtonSelection.ShowDialog(this);
                    ofrmToolButtonSelection.Dispose();
                }
                else if (Convert.ToString(cmnuToolStripCustomize.Tag) == ts_Main.Name)
                {
                    //Patient details custumization
                    gloSettings.frmToolButtonSelection ofrmToolButtonSelection = new gloSettings.frmToolButtonSelection(gloPMGlobal.DatabaseConnectionString, ts_Main, gloSettings.enumModuleName.Dashboard, arrDashBoardToolStrip);
                    ofrmToolButtonSelection.StartPosition = FormStartPosition.CenterScreen;
                    if (arrDashBoardToolStrip == null)
                    { arrDashBoardToolStrip = ofrmToolButtonSelection.GetDefaultToolStripSetting(); }
                    ofrmToolButtonSelection.ShowDialog(this);
                    ofrmToolButtonSelection.Dispose();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }

        #endregion

        private void mnuGo_PatientLedger_Click(object sender, EventArgs e)
        {

            //ShowHideMainMenu(false, false);
            //frmPatientFinancialView oPatientFinancialView = frmPatientFinancialView.GetInstance(gloCntrlPatient.PatientID);
            gloAccountsV2.frmPatientFinancialViewV2 oPatientFinancialView = gloAccountsV2.frmPatientFinancialViewV2.GetInstance(SyncPatient("Patient Account"));
            // gloAccountsV2.frmPatientFinancialViewV2 oPatientFinancialView = gloAccountsV2.frmPatientFinancialViewV2.GetInstance(gloCntrlPatient.PatientID);
            oPatientFinancialView.WindowState = FormWindowState.Maximized;
            oPatientFinancialView.MdiParent = this;
            oPatientFinancialView.Show();
            ShowHideMainMenu(false, false); //Mantis bugID:1256
        }

        private void mnuGo_Remittance_Click(object sender, EventArgs e)
        {
            //gloBilling.frmRemittance ofrmRemittance = new gloBilling.frmRemittance(gloPMGlobal.DatabaseConnectionString);

            //ofrmRemittance.MdiParent = this;
            //ofrmRemittance.WindowState = FormWindowState.Maximized;
            //ofrmRemittance.ShowInTaskbar = false;
            //ofrmRemittance.StartPosition = FormStartPosition.CenterParent;
            //ofrmRemittance.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuBilling_InsPayment_Click(object sender, EventArgs e)
        {
            try
            {


                //gloBilling.frmPaymentInsuraceEOB oPaymentInsuranceEOB = new frmPaymentInsuraceEOB(gloCntrlPatient.PatientID, false, 0, 0, 0, 0);
                //oPaymentInsuranceEOB.WindowState = FormWindowState.Maximized;
                //oPaymentInsuranceEOB.StartPosition = FormStartPosition.CenterParent;
                //oPaymentInsuranceEOB.ShowInTaskbar = false;
                //oPaymentInsuranceEOB.ShowDialog();
                //ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuBilling_PaymentTray_Click(object sender, EventArgs e)
        {
            try
            {

                //frmBillingClosedJournals ofrmBillingClosedJournals = new frmBillingClosedJournals(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID, gloPMGlobal.UserName);
                //frmEOBPaymentTray ofrmEOBPaymentTray = new frmEOBPaymentTray(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID, gloPMGlobal.UserName);
                //ofrmEOBPaymentTray.WindowState = FormWindowState.Maximized;
                //ofrmEOBPaymentTray.MdiParent = this;
                //ofrmEOBPaymentTray.Show();
                //ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuBilling_DailyClose_Click(object sender, EventArgs e)
        {

            gloReports.C1Reports.frmRpt_DailyChargesPaySummary_SSRS oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyChargesPaySummary_SSRS(gloPMGlobal.DatabaseConnectionString);

            oDailyChargesPaySummary.MdiParent = this;
            oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
            oDailyChargesPaySummary.ShowInTaskbar = false;
            oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
            oDailyChargesPaySummary.Show();
            ShowHideMainMenu(false, false);

        }

        private void mnuBilling_Ledger_Click(object sender, EventArgs e)
        {

            frmEOBLedger ofrmEOBLedger = new frmEOBLedger(gloCntrlPatient.PatientID, gloPMGlobal.DatabaseConnectionString);
            ofrmEOBLedger.WindowState = FormWindowState.Maximized;
            ofrmEOBLedger.MdiParent = this;
            ofrmEOBLedger.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuBilling_Remittance_Click(object sender, EventArgs e)
        {

            //frmEOBRemittance ofrmEOBRemittance = new frmEOBRemittance(gloPMGlobal.DatabaseConnectionString, gloCntrlPatient.PatientID);
            ////frmEOBRemittance ofrmEOBRemittance = new frmEOBRemittance(gloPMGlobal.DatabaseConnectionString);
            //ofrmEOBRemittance.WindowState = FormWindowState.Maximized;
            //ofrmEOBRemittance.MdiParent = this;
            //ofrmEOBRemittance.Show();
            //ShowHideMainMenu(false, false);
        }

        private void mnuBilling_Charges_Click(object sender, EventArgs e)
        {
            if (gloCntrlPatient.PatientID != 0)
            {
                string _EMRdatabaseconnectionstring = "";

                if (appSettings["EMRConnectionString"] != null)
                { _EMRdatabaseconnectionstring = appSettings["EMRConnectionString"].ToString(); }
                else
                { _EMRdatabaseconnectionstring = ""; }
                pnlPatient_UpComingAppointments.DockState = Janus.Windows.UI.Dock.PanelDockState.Docked;
                gloBilling.gloBilling oBilling = new gloBilling.gloBilling(gloPMGlobal.DatabaseConnectionString, _EMRdatabaseconnectionstring);
                oBilling.ShowBillingTransaction(gloCntrlPatient.PatientID, this);
                ShowHideMainMenu(false, false);
                oBilling.Dispose();
            }
        }

        private void mnuBilling_PatPayment_Click(object sender, EventArgs e)
        {
            try
            {
                //gloBilling.frmPaymentPatient oPaymentInsurace = new frmPaymentPatient(gloCntrlPatient.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(gloCntrlPatient.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                frmPatientPaymentV2.StartPosition = FormStartPosition.CenterParent;
                frmPatientPaymentV2.ShowInTaskbar = false;
                frmPatientPaymentV2.ShowDialog(this);
                frmPatientPaymentV2.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void mnuRpt_VoidClaims_Click(object sender, EventArgs e)
        {
            //Added By Pramod Nair For Implementing Crystal Reports
            gloReports.C1Reports.frmRpt_VoidChargesAndPay oAppointments = new gloReports.C1Reports.frmRpt_VoidChargesAndPay(gloPMGlobal.DatabaseConnectionString);

            oAppointments.MdiParent = this;
            oAppointments.WindowState = FormWindowState.Maximized;
            oAppointments.ShowInTaskbar = false;
            oAppointments.StartPosition = FormStartPosition.CenterParent;
            oAppointments.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuGo_PatientStatment_Click(object sender, EventArgs e)
        {
            try
            {

                gloBilling.Statement.frmRpt_Revised_PatientStatement oPatientStatementForGateWayEDI = gloBilling.Statement.frmRpt_Revised_PatientStatement.GetInstance(gloPMGlobal.DatabaseConnectionString, gloCntrlPatient.PatientID);
                oPatientStatementForGateWayEDI.WindowState = FormWindowState.Maximized;
                oPatientStatementForGateWayEDI.MdiParent = this;
                oPatientStatementForGateWayEDI.ShowInTaskbar = false;
                oPatientStatementForGateWayEDI.StartPosition = FormStartPosition.CenterParent;
                oPatientStatementForGateWayEDI.Show();
                ShowHideMainMenu(false, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void tsb_PatientStatment_Click(object sender, EventArgs e)
        {
            try
            {
                gloBilling.Statement.frmRpt_Revised_PatientStatement oPatientStatementForGateWayEDI = gloBilling.Statement.frmRpt_Revised_PatientStatement.GetInstance(gloPMGlobal.DatabaseConnectionString, gloCntrlPatient.PatientID);
                oPatientStatementForGateWayEDI.WindowState = FormWindowState.Maximized;
                oPatientStatementForGateWayEDI.MdiParent = this;
                oPatientStatementForGateWayEDI.ShowInTaskbar = false;
                oPatientStatementForGateWayEDI.StartPosition = FormStartPosition.CenterParent;
                oPatientStatementForGateWayEDI.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        //Added By Shweta 20100211
        private void mnu_MISReports_Aging_Click(object sender, EventArgs e)
        {
            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                frmRpt_AgingReport oAgingSummary = new frmRpt_AgingReport(gloPMGlobal.DatabaseConnectionString);

                oAgingSummary.MdiParent = this;

                oAgingSummary.WindowState = FormWindowState.Maximized;
                oAgingSummary.ShowInTaskbar = false;
                oAgingSummary.StartPosition = FormStartPosition.CenterParent;
                oAgingSummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
                ShowSSRSReport("rptAgingReport", "Aging Report", true, mnu_MISReports_Aging.Image);
        }

        private void mnu_MISReports_FinancialSummary_Click(object sender, EventArgs e)
        {
            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                frmRpt_FinancialSummary oFinancialSummary = new frmRpt_FinancialSummary(gloPMGlobal.DatabaseConnectionString);
                oFinancialSummary.MdiParent = this;
                oFinancialSummary.WindowState = FormWindowState.Maximized;
                oFinancialSummary.ShowInTaskbar = false;
                oFinancialSummary.StartPosition = FormStartPosition.CenterParent;
                oFinancialSummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
                ShowSSRSReport("rptFinanceDetails", "Financial Summary Report", true, mnu_MISReports_FinancialSummary.Image);
        }

        private void mnu_InsuranceCompanySetup_Company_Category_Click(object sender, EventArgs e)
        {
            frmRpt_InsurancePlanSetup oInsurancePlanSetup = new frmRpt_InsurancePlanSetup(gloPMGlobal.DatabaseConnectionString);

            oInsurancePlanSetup.MdiParent = this;
            oInsurancePlanSetup.WindowState = FormWindowState.Maximized;
            oInsurancePlanSetup.ShowInTaskbar = false;
            oInsurancePlanSetup.StartPosition = FormStartPosition.CenterParent;
            oInsurancePlanSetup.Show();
            ShowHideMainMenu(false, false);
        }

        void oPatientFinancialView_FormClosed(object sender, FormClosedEventArgs e)
        {
            //refresh here
            FillAlerts();
        }

        private void mnu_MISReports_AvailableReserve_Click(object sender, EventArgs e)
        {
            try
            {
                //Roopali 20102007
                if (_IsEnableSSRSReports == false)
                {
                    pnlPatient_UpComingAppointments.DockState = Janus.Windows.UI.Dock.PanelDockState.Docked;
                    frmRpt_ReservAvailable oReservAvailabl = new frmRpt_ReservAvailable(gloPMGlobal.DatabaseConnectionString);
                    oReservAvailabl.MdiParent = this;
                    oReservAvailabl.WindowState = FormWindowState.Maximized;
                    oReservAvailabl.ShowInTaskbar = false;
                    oReservAvailabl.StartPosition = FormStartPosition.CenterParent;
                    oReservAvailabl.Show();
                    ShowHideMainMenu(false, false);
                }
                else
                    ShowSSRSReport("rptAvailableReserves", "Available Reserves Report", true, mnu_MISReports_AvailableReserve.Image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void mnu_MISReports_DailyCharges_Click(object sender, EventArgs e)
        {
            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary(gloPMGlobal.DatabaseConnectionString, "DailyCharges", "Daily");
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
                ShowSSRSReport("rptDailyCharge", "Daily Charge Report", true, mnu_MISReports_DailyCharges.Image);
        }

        private void mnu_MISReports_DailyPayments_Click(object sender, EventArgs e)
        {
            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary(gloPMGlobal.DatabaseConnectionString, "DailyPayment", "Daily");
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
                ShowSSRSReport("rptDailyPaymentList", "Daily Payment Report", true, mnu_MISReports_DailyPayments.Image);
        }

        private void mnu_MISReports_DailySummary_Click(object sender, EventArgs e)
        {
            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary(gloPMGlobal.DatabaseConnectionString, "DailyClose", "Daily");
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
                ShowSSRSReport("rptDailyCloseSummary", "Daily Close", true, mnu_MISReports_DailySummary.Image);
        }

        private void mnu_MISReports_MonthlyCharges_Click(object sender, EventArgs e)
        {

            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary(gloPMGlobal.DatabaseConnectionString, "DailyCharges", "Monthly");
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
                ShowSSRSReport("rptMonthlyCharge", "Monthly Charge Report", true, mnu_MISReports_MonthlyCharges.Image);
        }

        private void mnu_MISReports_MonthlyPayments_Click(object sender, EventArgs e)
        {
            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyMonthlyChargesPaySummary(gloPMGlobal.DatabaseConnectionString, "DailyPayment", "Monthly");
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
                ShowSSRSReport("rptMonthlyPaymentList", "Monthly Payment Report", true, mnu_MISReports_MonthlyPayments.Image);
        }

        private void tsbDailyClose_Click(object sender, EventArgs e)
        {
            //Roopali 20102007
            if (_IsEnableSSRSReports == false)
            {
                gloReports.C1Reports.frmRpt_DailyChargesPaySummary oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyChargesPaySummary(gloPMGlobal.DatabaseConnectionString);
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
            }
            else
            {
                gloReports.C1Reports.frmRpt_DailyChargesPaySummary_SSRS oDailyChargesPaySummary = new gloReports.C1Reports.frmRpt_DailyChargesPaySummary_SSRS(gloPMGlobal.DatabaseConnectionString);
                oDailyChargesPaySummary.MdiParent = this;
                oDailyChargesPaySummary.WindowState = FormWindowState.Maximized;
                oDailyChargesPaySummary.ShowInTaskbar = false;
                oDailyChargesPaySummary.StartPosition = FormStartPosition.CenterParent;
                oDailyChargesPaySummary.Show();
                ShowHideMainMenu(false, false);
                //   ShowSSRSReport("rptDailyCloseSummary", "Monthly Close Summary", true);
            }
        }

        private void mnuGo_EOBLedger_Click(object sender, EventArgs e)
        {
            frmEOBLedger ofrmEOBLedger = new frmEOBLedger(_CurrentPatientId, gloPMGlobal.DatabaseConnectionString);

            ofrmEOBLedger.MdiParent = this;
            ofrmEOBLedger.WindowState = FormWindowState.Maximized;
            ofrmEOBLedger.ShowInTaskbar = false;
            ofrmEOBLedger.StartPosition = FormStartPosition.CenterParent;
            ofrmEOBLedger.Show();
            ShowHideMainMenu(false, false);
        }

        private void ShowSSRSReport(string ReportName, string ReportTitle, bool blnIsgloStreamReport, Image img)
        {
            Cursor.Current = Cursors.WaitCursor;
            SSRSApplication.frmSSRSViewer frmSSRS = new SSRSApplication.frmSSRSViewer();
            frmSSRS.Conn = gloPMGlobal.DatabaseConnectionString;
            frmSSRS.reportName = ReportName;
            frmSSRS.reportTitle = ReportTitle;
            frmSSRS.formIcon = img;
            frmSSRS.IsgloStreamReport = blnIsgloStreamReport;
            frmSSRS.MdiParent = this;
            Cursor.Current = Cursors.Default;
            frmSSRS.Show();
            ShowHideMainMenu(false, false);
        }

        private void tsb_PD_PriorAuthorization_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearchFilter.Visible = false;
                _SelcetedPatient = PatientDetails.PriorAuthorization;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void mnu_MISReports_CPTAnalysis_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptCPTAnalysisReport", "CPT Analysis", true, mnu_MISReports_CPTAnalysis.Image);
        }

        private void mnu_MISReports_MonthlyClose_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptMonthlyCloseSummary", "Monthly Close", true, mnu_MISReports_MonthlyClose.Image);
        }

        private void mnuPriorAuthReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptPriorReport", "Prior Authorization Report", true, mnuPriorAuthReport.Image);
        }

        private void mnu_Reports_PatientExcludeSt_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptExcludePatientDue", "Patients Excluded from Statement", true, mnu_Reports_PatientExcludeSt.Image);
        }

        private void mnu_MISReports_Productivity_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptProductivityReport", "Productivity", true, mnu_MISReports_Productivity.Image);
        }
        private void mnu_MISReports_ProductivityProviderPaymentMthd_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptPaymentByPaymentMethod", "Productivity by Provider by Payment Method", true, mnu_MISReports_ProductivityProviderPaymentMthd.Image);
        }
        private void mnu_MISReports_ProductivityRVU_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptProductivityRVUReport", "Productivity By RVU", true, mnu_MISReports_ProductivityRVU.Image);//rptProductivityDetails_RVU.rdl this RDL was associated with 4 sub RDL. from 8030 we created single RDL.
        }
        private void mnu_MISReports_FinancialPayments_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptFinancialReport", "Financial Summary", true, mnu_MISReports_FinancialPayments.Image);//rptFinancialPayments this RDL was associated with 4 sub RDL. from 8030 we created single RDL.
        }
        private void mnuReimbursementWarning_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptFeeScheduleUnderReimbursement", "Reimbursement Warning", true, mnuReimbursementWarning.Image);
        }
        private void mnu_MISReports_ProductivityDOS_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptProductivityDOSReport", "Productivity By DOS", true, mnu_MISReports_ProductivityDOS.Image);
        }

        private void mnu_MISReports_FinancialProSummary_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptFinProReport", "Financial Productivity Summary", true, mnu_MISReports_FinancialProSummary.Image);
        }

        private void mnu_MISReports_ExpectedCollection_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("%ExpCollectionReport", "% Expected Collection", true, null);
        }

        private void mnu_rpt_MissedOpportunitiesReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptMissOppReport", "Missed Opportunities Report", true, null);
        }

        private void mnu_MISReports_DenialManagement_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptAdjustmentTrackingSummary", "Denial Management Report", true, null);
        }

        private void mnu_rpt_AppointmentCensusReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptAppCensusReport", "Appointment Census Report", true, null);
        }

        private void mnu_MISReports_Fin_ProdSummaryDX_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptFinProReport - ICD", "Financial Productivity with Dx", true, null);
        }

        private void mnu_MISReports_CachedFinancialProductivitySum_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptFinProReportCache", "Cached-Financial Productivity Summary", true, null);
        }

        private void mnu_MISReports_AgedPayment_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptAgedpayment", "Aged Payment Report", true, null);
        }


        private void mnu_rpt_PatientBenefitsInfo_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("RptBatchEligiility", "Patient Insurance Benefit Information", true, null);
        }

        private void mnu_MISReports_MTDYTDReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("RptMonthlyYearlyProcAnalysis_WithPayment", "RptMonthlyYearlyProcAnalysis_WithPayment", true, null);
        }

        //Added by Mayuri:20101006-Added Save As Copy functionality
        private void cmnuPatientItem_SaveAsCopy_Click(object sender, EventArgs e)
        {
            // IF CONDITION BY SUDHIR 20101029 //
            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
            {
                //Bug #81090: 00000879: deceased patient status
                MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            gloPatient.gloPatientDemographicsControl oPatientDemographicsControl;
            oPatientDemographicsControl = new gloPatient.gloPatientDemographicsControl(0, gloPMGlobal.DatabaseConnectionString, true);


            bool _IsSaveAsCopy = true;
            gloPatient.gloPatient oPatient = new gloPatient.gloPatient(gloPMGlobal.DatabaseConnectionString);
            string PatientFirstName = "";
            string PatientLastName = "";
            PatientFirstName = gloCntrlPatient.FirstName;
            PatientLastName = gloCntrlPatient.LastName;
            oPatient.PatientRegistration(_CurrentPatientId, out ReturnPatientId, out _IsSaveAsCopy, PatientFirstName, PatientLastName, this);
            if (ReturnPatientId > 0)
            {
                gloCntrlPatient.SelectedPatientID = ReturnPatientId;
                gloCntrlPatient.PatientID = ReturnPatientId;

            }
            _CurrentPatientId = ReturnPatientId;

            gloCntrlPatient.FillPatients();
            FillSelectedPatient();
            gloCntrlPatient.Refresh();
            gloCntrlPatient.SelectedPatientID = ReturnPatientId;
            oPatient.Dispose();
            //SLR: free oPatientDEmographicscontrol
            if (oPatientDemographicsControl != null)
            {
                oPatientDemographicsControl.Dispose();
            }
        }

        private void cmnuPatientItem_Cases_Click(object sender, EventArgs e)
        {
            if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
            {
                //Bug #81090: 00000879: deceased patient status
                MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmSetupCases ofrmSetupCases = new frmSetupCases(_CurrentPatientId);
            ofrmSetupCases.ShowDialog(this);
            //SLR: FRee ofrmSetupCases
            ofrmSetupCases.Dispose();
            if (_SelcetedPatient == PatientDetails.PatientCases)
                FillCases(_CurrentPatientId);
        }

        private void cmnuPatientItem_EmergencyAccess_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmEmergencyAccess ofrm = new frmEmergencyAccess())
                {
                    //gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloPMGlobal.DatabaseConnectionString);
                    if (ofrm.ShowDialog(this) == DialogResult.OK)
                    {
                        _CurrentPatientId = gloCntrlPatient.PatientID;
                        EnableDisableLockedPatientButtons(true);
                        FillSelectedPatient();
                        //if (oSecurity.isBadDebtPatient(gloCntrlPatient.PatientID, true))
                        //{
                        //    _CurrentPatientId = 0;
                        //    EnableDisableLockedPatientButtons(true);
                        //    EnableDisableBadDebtPatientButtons(false);
                        //    appSettings["PatientID"] = Convert.ToString(_CurrentPatientId);
                        //    _CurrentPatientId = gloCntrlPatient.PatientID;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tmrCopayAlertBlink_Tick(object sender, EventArgs e)
        {
            int COL_COPAYALERT_ALERTTEXT = 3;
            if (Convert.ToString(c1CopayAlert.GetData(0, COL_COPAYALERT_ALERTTEXT)) == "Copay")
            {
                if (_IsColored)
                {
                    for (int iCount = 0; iCount <= nBlinkingCells.Count - 1; iCount++)
                    {
                        c1CopayAlert.SetCellStyle(Convert.ToInt16(nBlinkingCells[iCount]), COL_COPAYALERT_ALERTTEXT, "ChildItemRegularRevised");
                    }
                    _IsColored = false;
                }
                else
                {
                    for (int iCount = 0; iCount <= nBlinkingCells.Count - 1; iCount++)
                    {
                        c1CopayAlert.SetCellStyle(Convert.ToInt16(nBlinkingCells[iCount]), COL_COPAYALERT_ALERTTEXT, "Default");
                    }
                    _IsColored = true;
                }
            }
        }

        private void mnu_Reports_BatchReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptBatchReport", "Batch Report", true, mnu_Reports_BatchReport.Image);
        }

        //added by mahesh s on 2011-06-27(yyyy-mm-dd) for merge account.
        private void mnuTools_MergePatientAccount_Click(object sender, EventArgs e)
        {
            try
            {
                gloPatient.frmMergeAccounts ofrm = new gloPatient.frmMergeAccounts(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId);
                ofrm.ShowDialog(this);
                ofrm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuItem_eligibilityCheckTest_Click(object sender, EventArgs e)
        {
            gloPatient.gloPatientEiligibility ogloEiligibility = null;
            try
            {
                ogloEiligibility = new gloPatient.gloPatientEiligibility(gloPMGlobal.DatabaseConnectionString);
                Int64 _insuranceContactid = ogloEiligibility.GetInsuranceContactID(tempInsId, gloCntrlPatient.PatientID);
                gloPatient.frmSubmit271Response frm = new gloPatient.frmSubmit271Response(gloPMGlobal.DatabaseConnectionString, gloCntrlPatient.PatientID, _insuranceContactid);
                frm.ShowDialog(this);
                //SLR: Free frm
                frm.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ogloEiligibility != null)
                {
                    ogloEiligibility.Dispose();
                }
            }

        }

        //Code added by Rohit on 20110930
        private void messageQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (System.Windows.Forms.Form oform in System.Windows.Forms.Application.OpenForms)
                {
                    if (oform.Name == "frmInterfacesMessageQueueViewReport")
                    {
                        oform.MdiParent = this;
                        oform.WindowState = FormWindowState.Maximized;
                        oform.Show();
                        oform.BringToFront();
                        oform.Focus();
                        ShowHideMainMenu(false, false);
                        return;
                    }
                }

                frmInterfacesMessageQueueViewReport ofrm = new frmInterfacesMessageQueueViewReport(1000);
                ofrm.MdiParent = this;
                ofrm.ShowInTaskbar = false;
                ofrm.StartPosition = FormStartPosition.CenterParent;
                ofrm.WindowState = FormWindowState.Maximized;
                ofrm.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuInterfaceReports_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (System.Windows.Forms.Form oform in System.Windows.Forms.Application.OpenForms)
                {
                    if (oform.Name == "frmInterfacesMessageErrorReport")
                    {
                        oform.MdiParent = this;
                        oform.WindowState = FormWindowState.Maximized;
                        oform.Show();
                        oform.BringToFront();
                        oform.Focus();
                        ShowHideMainMenu(false, false);
                        return;
                    }
                }
                frmInterfacesMessageErrorReport ofrm = new frmInterfacesMessageErrorReport();
                ofrm.MdiParent = this;
                ofrm.ShowInTaskbar = false;
                ofrm.StartPosition = FormStartPosition.CenterParent;
                ofrm.WindowState = FormWindowState.Maximized;
                ofrm.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuPatientActivationtReport_Click(object sender, EventArgs e)
        {
            try
            {
                //Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal"
                //if portal enable and user not have right then report is not show.
                if (oClsgloUserRights.PatientPortal == false)
                {
                    MessageBox.Show("This user does not have the rights to view Patient Activation Report. Please contact your system administrator for the same.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (System.Windows.Forms.Form oform in System.Windows.Forms.Application.OpenForms)
                {
                    if (oform.Name == "frmPatientActivationReport")
                    {
                        oform.MdiParent = this;
                        oform.WindowState = FormWindowState.Maximized;
                        oform.Show();
                        oform.BringToFront();
                        oform.Focus();
                        ShowHideMainMenu(false, false);
                        return;
                    }
                }
                frmPatientActivationReport ofrm = new frmPatientActivationReport(gloPMGlobal.DatabaseConnectionString, gloPMGlobal.UserID);
                ofrm.MdiParent = this;
                ofrm.ShowInTaskbar = false;
                ofrm.StartPosition = FormStartPosition.CenterParent;
                ofrm.WindowState = FormWindowState.Maximized;
                ofrm.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //end of Code added by Rohit on 20110930
        private void c1PatientDetails_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
        }

        private void mnuBatch_Eligibility_Click(object sender, EventArgs e)
        {
            try
            {
                gloPatient.gloPatientEiligibility objEligibility = new gloPatient.gloPatientEiligibility(gloPMGlobal.DatabaseConnectionString);
                objEligibility.ShowEligibility();

                //SLR: Free objEliginbiloity
                if (objEligibility != null)
                {
                    objEligibility.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void batchEligibilityActivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBatchEligibilityActivity ofrm = new frmBatchEligibilityActivity();
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.ShowDialog(this);
            //SLR: FRee oFrm
            ofrm.Dispose();
        }

        private void mnuGo_RevenueCycle_Click(object sender, EventArgs e)
        {
            gloBilling.Collections.frmRevenueCycle oPatientFinancialView = gloBilling.Collections.frmRevenueCycle.GetInstance(gloCntrlPatient.PatientID);
            oPatientFinancialView.WindowState = FormWindowState.Maximized;
            oPatientFinancialView.MdiParent = this;
            oPatientFinancialView.Show();
            ShowHideMainMenu(false, false);
        }

        private void tsb_RevenueCycle_Click(object sender, EventArgs e)
        {
            gloBilling.Collections.frmRevenueCycle oPatientFinancialView = gloBilling.Collections.frmRevenueCycle.GetInstance(gloCntrlPatient.PatientID);
            oPatientFinancialView.WindowState = FormWindowState.Maximized;
            oPatientFinancialView.MdiParent = this;
            oPatientFinancialView.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuItem_ViewBenefits_Click(object sender, EventArgs e)
        {
            Int64 nCurrentInsuranceID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, 0));
            frmViewBenefit ofrm = new frmViewBenefit(_CurrentPatientId, nCurrentInsuranceID, gloPMGlobal.DatabaseConnectionString);
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.ShowDialog(this);
            //SLR: Free oFrm
            ofrm.Dispose();
            FillPatientInsurance(_CurrentPatientId);
        }

        private void mnuBusCenterMismatch_Click(object sender, EventArgs e)
        {
            try
            {
                ShowSSRSReport("rptClaimAccountMisMatchBusinessCenter", "Business Center Mismatch", true, mnuBusCenterMismatch.Image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuChargeLagReport_Click(object sender, EventArgs e)
        {
            try
            {
                ShowSSRSReport("rptChargeLag", "Charge Lag Report", true, mnuChargeLagReport.Image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuBatchLagReport_Click(object sender, EventArgs e)
        {
            try
            {
                ShowSSRSReport("rptBatchLagReport", "Batch Lag Report", true, mnuBatchLagReport.Image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuMaster_ICD9Gallery_Click(object sender, EventArgs e)
        {
            try
            {
                frmICD9CPTGallery ofrmICD9CPTGallery = new frmICD9CPTGallery(gloPMGlobal.DatabaseConnectionString, clsGallery.GalleryType.ICD9);
                ofrmICD9CPTGallery.ShowInTaskbar = false;
                ofrmICD9CPTGallery.ShowDialog(this);
                ofrmICD9CPTGallery.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void mnuMaster_ICD10Gallery_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 ClinicID = 0;
                ClinicID = AppSettings.ClinicID;

                gloUIControlLibrary.WPFForms.frmICDCodeGallery frm = gloUIControlLibrary.WPFForms.frmICDCodeGallery.CheckFormOpen();

                if (frm == null)
                {
                    frm = gloUIControlLibrary.WPFForms.frmICDCodeGallery.GetInstance(gloPMGlobal.DatabaseConnectionString);
                    System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(frm);
                    frm.ClinicID = ClinicID;
                    frm.LoadIndexXMLData();
                    frm.Show();
                }
                else
                {
                    frm.Visibility = System.Windows.Visibility.Visible;
                    frm.WindowState = System.Windows.WindowState.Maximized;
                    frm.Activate();
                    frm.BringIntoView();
                    frm = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void mnuMaster_CPTGallery_Click(object sender, EventArgs e)
        {
            try
            {
                frmICD9CPTGallery ofrmICD9CPTGallery = new frmICD9CPTGallery(gloPMGlobal.DatabaseConnectionString, clsGallery.GalleryType.CPT);
                ofrmICD9CPTGallery.ShowInTaskbar = false;
                ofrmICD9CPTGallery.ShowDialog(this);
                ofrmICD9CPTGallery.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void mnuICDAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                gloICDAnalysis.ClassLib.DBSetting objDBSetting = new gloICDAnalysis.ClassLib.DBSetting(gloICDAnalysis.ClassLib.DBSetting.ApplicationType.gloEMR);
                objDBSetting.SqlServerName = Program.gSQLServerName;
                objDBSetting.DatabaseName = Program.gDatabase;
                objDBSetting.SqlUserName = Program.gLoginUser;
                objDBSetting.SqlPassword = Program.gLoginPassword;
                objDBSetting.IsWindowsAuthentication = Program.gWindowsAuthentication;

                //ICD9 Usage and ICD10 Mapping Report
                gloICDAnalysis.frmDashboard ofrmICDUtility = new gloICDAnalysis.frmDashboard(gloICDAnalysis.ClassLib.DBSetting.ApplicationType.gloPM, objDBSetting);
                ofrmICDUtility.WindowState = FormWindowState.Maximized;
                ofrmICDUtility.MdiParent = this;
                ofrmICDUtility.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        private void chkApptDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApptDate.Checked == true)
            {
                dtpFromDate.Enabled = true;
                dtpToDate.Enabled = true;
            }
            else
            {
                dtpFromDate.Enabled = false;
                dtpToDate.Enabled = false;
            }
            FromToDateChange();
        }


        #region "RCM"

        private void tsb_RCMDocs_Click(object sender, EventArgs e)
        {
            ShowRCM();
        }

        private void ShowRCM(Boolean CanImport = true, Boolean bIsFocus = true, Boolean _ISChildView = false, Boolean _ISOpenDoc = false)
        {
            gloEDocumentV3.gloEDocV3Management oEDocument = new gloEDocumentV3.gloEDocV3Management();
            try
            {
                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);


                oEDocument.IsSplitScreenShown = false;
                gloEDocumentV3.gloEDocV3Admin.Connect(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloGlobal.gloPMGlobal.DMSConnectionString, gloGlobal.gloPMGlobal.DMSV3TempPath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, System.Windows.Forms.Application.StartupPath);

                oEDocument.ShowEDocument(-1, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, this, gloEDocumentV3.Enumeration.enum_OpenExternalSource.RCM, 0);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oEDocument != null) { oEDocument.Dispose(); oEDocument = null; }
            }

        }

        #endregion

        #region "DMS"
        private void ShowDMS(Boolean CanImport = true, Boolean bIsFocus = true, Boolean _ISChildView = false, Boolean _ISOpenDoc = false)
        {
            gloEDocumentV3.gloEDocV3Management oEDocument = new gloEDocumentV3.gloEDocV3Management();
            try
            {
                gloSecurity.gloSecurity oSecurity = new gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                if (oSecurity.isPatientLock(gloCntrlPatient.PatientID, false))
                {
                    _CurrentPatientId = 0;
                    //Bug #81090: 00000879: deceased patient status
                    MessageBox.Show("The status of the patient is '" + appSettings["CurrentPatientStatus"] + "'. " + Environment.NewLine + "You can not perform any activity on this patient", gloPMGlobal.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                if (oSecurity != null) { oSecurity.Dispose(); oSecurity = null; }

                if (_CurrentPatientId == 0 || appSettings["CurrentPatientStatus"] == "Deceased")
                {
                    MessageBox.Show("Select patient", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //     If gblnProviderDisable = True Then
                //    If ShowAssociateProvider(gnPatientID, Me) = False Then
                //        Exit Sub
                //    Else
                //        oPatientListControl.FillPatients()
                //    End If
                //End If

                oEDocument.IsSplitScreenShown = false;
                gloEDocumentV3.gloEDocV3Admin.Connect(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloGlobal.gloPMGlobal.DMSConnectionString, gloGlobal.gloPMGlobal.DMSV3TempPath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, System.Windows.Forms.Application.StartupPath);
                if (_ISOpenDoc)
                {
                    Int64 _DocID = Convert.ToInt64(c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_D_CAT_FILENAME).ToString());
                    String _Year = c1PatientDetails.GetData(c1PatientDetails.RowSel, COL_D_CAT_YEAR).ToString();
                    if (_DocID > 0 && _Year != "")
                        oEDocument.ShowEDocument(_CurrentPatientId, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, this, gloEDocumentV3.Enumeration.enum_OpenExternalSource.DashBoard, _DocID, _ISChildView);

                }
                else
                {
                    if (CanImport == true)
                        oEDocument.ShowEDocument(SyncPatient(), gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, this, gloEDocumentV3.Enumeration.enum_OpenExternalSource.None, 0);
                    //oEDocument.ShowEDocument(_CurrentPatientId, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, this, gloEDocumentV3.Enumeration.enum_OpenExternalSource.None, 0);
                    else
                        oEDocument.ShowEDocument(SyncPatient(), gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewAllDocuments, this, gloEDocumentV3.Enumeration.enum_OpenExternalSource.None, 0, _ISChildView);
                    //oEDocument.ShowEDocument(_CurrentPatientId, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewAllDocuments, this, gloEDocumentV3.Enumeration.enum_OpenExternalSource.None, 0, _ISChildView);

                }
                if (_ISChildView)
                    ShowHideMainMenu(false, false);
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oEDocument != null) { oEDocument.Dispose(); oEDocument = null; }
            }

        }

        private Boolean LoadDMSSettings()
        {


            Boolean _result = true;
            String strSQLServerName = "", strDB = "";
            object obj;
            gloSettings.GeneralSettings objSetting = new gloSettings.GeneralSettings(gloPMGlobal.DatabaseConnectionString);
            try
            {
                objSetting.GetSetting("EMRInstalled", out obj);
                if (Convert.ToString(obj).ToLower().Trim() == "False".ToLower())
                {
                    tsb_ScanDocs.Visible = false;
                    mnuGo_ScanDocument.Visible = false;
                    mnuView_Documents.Visible = false;
                    tsb_PDViewDocument.Visible = false;
                    return false;
                }
                objSetting.GetSetting("GLODMSSERVERNAME", out obj);
                if (obj != null) { strSQLServerName = obj.ToString(); }

                objSetting.GetSetting("GLODMSDBNAME", out obj);
                if (obj != null) { strDB = obj.ToString(); }

                if (strSQLServerName != "" && strDB != "")
                {
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName = strSQLServerName;
                    gloEDocumentV3.gloEDocV3Admin.gstrDMSSqlServerName = strSQLServerName;
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName = strDB;
                    gloEDocumentV3.gloEDocV3Admin.gstrDMSDatabaseName = strDB;

                    gloGlobal.gloPMGlobal.DMSConnectionString = "SERVER=" + strSQLServerName + ";DATABASE=" + strDB + ";Integrated Security=SSPI";
                    gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer.ConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
                    gloGlobal.gloPMGlobal.DMSV3TempPath = gloSettings.FolderSettings.AppTempFolderPath + "DMSV3Temp";

                    objSetting.GetSetting("InternetFax", out obj);
                    if (obj != null)
                    {
                        gloGlobal.gloPMGlobal.IsInternetFaxEnabled = Convert.ToBoolean(Convert.ToInt16(obj));
                        appSettings["Internet Fax"] = gloGlobal.gloPMGlobal.IsInternetFaxEnabled.ToString();
                    }
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) != false)
                    {
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true);

                        if (gloRegistrySetting.GetRegistryValue("FAXOutputDirectory") != null)
                        {
                            gloGlobal.gloPMGlobal.FaxOutputDirectory = Convert.ToString(gloRegistrySetting.GetRegistryValue("FAXOutputDirectory"));
                        }
                        if (gloRegistrySetting.GetRegistryValue("FAXCoverPage") != null)
                        {
                            gloGlobal.gloPMGlobal.AddFaxCoverpage = Convert.ToBoolean(Convert.ToInt16(gloRegistrySetting.GetRegistryValue("FAXCoverPage")));
                        }

                        gloRegistrySetting.CloseRegistryKey();

                    }

                    gloEDocumentV3.gloEDocV3Admin.gstrFaxOutputDirectory = gloGlobal.gloPMGlobal.FaxOutputDirectory;
                    gloEDocumentV3.gloEDocV3Admin.blnIsInternetFaxEnabled = gloGlobal.gloPMGlobal.IsInternetFaxEnabled;
                    gloEDocumentV3.gloEDocV3Admin.gblnAddFaxCoverpage = gloGlobal.gloPMGlobal.AddFaxCoverpage;
                    gloEDocumentV3.gloEDocV3Admin.gblnAssociatedProvider = gloGlobal.gloPMGlobal.AssociatedProvider;
                    gloEDocumentV3.gloEDocV3Admin.SetUserName(gloGlobal.gloPMGlobal.UserName);
                    gloEDocumentV3.gloEDocV3Admin.GetDefaultPrinterDialog(false);
                    gloEDocumentV3.gloEDocV3Admin.Connect(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloGlobal.gloPMGlobal.DMSConnectionString, gloGlobal.gloPMGlobal.DMSV3TempPath, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.ClinicID, System.Windows.Forms.Application.StartupPath);
                }


            }
            catch (Exception)// ex)
            {
                _result = false;
                MessageBox.Show("Failed to load DMS Settings", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ex = null;
            }
            finally
            {
                obj = null;
                if (objSetting != null) { objSetting.Dispose(); objSetting = null; }
            }
            return _result;

        }

        private void mnuGo_ScanDocument_Click(object sender, EventArgs e)
        {
            ShowDMS();
        }

        private void mnuView_Documents_Click(object sender, EventArgs e)
        {
            ShowDMS(false, false, true);
        }

        private void tsb_PDViewDocument_Click(object sender, EventArgs e)
        {
            try
            {
                _SelcetedPatient = PatientDetails.PatientDocuments;
                _AppClick = true;
                dtpFromDate.Value = DateTime.Now;
                dtpToDate.Value = DateTime.Now;
                _SearchFlag = false;
                _AppClick = false;
                pnlSearchFilter.Visible = true;
                showPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {

            }
        }
        private void FillPatientDocuments(Int64 PatientID)
        {
            c1PatientDetails.Visible = false;
            gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            gloEDocumentV3.Document.BaseDocuments oDocuments = null;
            //Boolean IsArchiveDocument = false;
            //Boolean IsNotArchiveDocuemnt = false;
            try
            {
                //IsArchiveDocument = oList.GetArchiveInformation(PatientID);
                //IsNotArchiveDocuemnt = oList.GetNonArchiveInformation(PatientID);

                // if ( IsArchiveDocument == false && IsNotArchiveDocuemnt == false )
                //    pnlArchiveInfo.Visible = false;
                //else if( IsArchiveDocument == false && IsNotArchiveDocuemnt == true)
                //    pnlArchiveInfo.Visible = false;
                //else if( IsArchiveDocument == true && IsNotArchiveDocuemnt == true )
                // {
                //    pnlArchiveInfo.Visible = true;
                //    lblInfo.Text = "Documents are partially archived for a patient " & gstrPatientFirstName. & " " & gstrPatientLastName
                // }
                //else if( IsArchiveDocument == true && IsNotArchiveDocuemnt == false )
                // {
                //    pnlArchiveInfo.Visible = true;
                //    lblInfo.Text = "Documents are archived for a patient " & gstrPatientFirstName & " " & gstrPatientLastName
                // }

                #region " Grid Design "

                c1PatientDetails.DataSource = null;
                c1PatientDetails.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                this.pnlSearchFilter.Visible = true;

                c1PatientDetails.AllowSorting = AllowSortingEnum.None;
                c1PatientDetails.Visible = true;
                c1PatientDetails.BringToFront();
                c1PatientDetails.Cols.Count = Col_view_Count;
                c1PatientDetails.Rows.Count = 1;
                c1PatientDetails.Rows.Fixed = 1;
                c1PatientDetails.Cols.Fixed = 0;


                c1PatientDetails.Cols[COL_D_CAT_CATEGORY].Width = 0;
                // ID
                c1PatientDetails.Cols[COL_D_CAT_NAME].Width = 0;
                //225             ' Name
                c1PatientDetails.Cols[COL_D_CAT_NOTEFLAG].Width = 0;
                //15          ' Note Flag
                c1PatientDetails.Cols[COL_D_CAT_EXTRAFLAG].Width = 0;
                // Extra Col
                c1PatientDetails.Cols[COL_D_CAT_SOURCEMACHINE].Width = 0;
                // Source Machine
                c1PatientDetails.Cols[COL_D_CAT_SYSTEMFOLDER].Width = 0;
                // System Folder
                c1PatientDetails.Cols[COL_D_CAT_CONTAINER].Width = 0;
                // Container
                c1PatientDetails.Cols[COL_D_CAT_CATEGORY].Width = 0;
                // Category
                c1PatientDetails.Cols[COL_D_CAT_PATIENTID].Width = 0;
                // Patient ID
                c1PatientDetails.Cols[COL_D_CAT_YEAR].Width = 0;
                // Year
                c1PatientDetails.Cols[COL_D_CAT_MONTH].Width = 0;
                // Month
                c1PatientDetails.Cols[COL_D_CAT_SOURCEBIN].Width = 0;
                // Source Bin
                c1PatientDetails.Cols[COL_D_CAT_INUSED].Width = 0;
                // In Used
                c1PatientDetails.Cols[COL_D_CAT_USEDMACHINE].Width = 0;
                // Used Machine
                c1PatientDetails.Cols[COL_D_CAT_USEDTYPE].Width = 0;
                // Used Type
                c1PatientDetails.Cols[COL_D_CAT_PATH].Width = 0;
                // Path
                c1PatientDetails.Cols[COL_D_CAT_FILENAME].Width = 0;
                // File Name
                c1PatientDetails.Cols[COL_D_CAT_MACHINEID].Width = 0;
                // Machine ID
                c1PatientDetails.Cols[COL_D_CAT_COLTYPE].Width = 0;
                // Col Type
                c1PatientDetails.Cols[COL_D_CAT_VERSIONNO].Width = 0;
                // Version No
                c1PatientDetails.Cols[COL_D_CAT_MACHINEID].Width = 0;
                // Machine ID
                c1PatientDetails.Cols[COL_D_CAT_COLTYPE].Width = 0;
                // Col Type
                c1PatientDetails.Cols[COL_D_CAT_REVIWEDFLAG].Width = 0;
                //15           ' Col Type

                int _width = c1PatientDetails.Width - 5;

                c1PatientDetails.Cols[COL_View_CategoryHidden].Width = _width * 0;
                c1PatientDetails.Cols[COL_View_Category].Width = (_width / 2) - 18;
                c1PatientDetails.Cols[COL_View_Month].Width = _width * 0;
                c1PatientDetails.Cols[COL_View_DocumentName].Width = (_width / 2) - 18;
                c1PatientDetails.Cols[COL_View_NOTEFLAG].Width = 18;
                c1PatientDetails.Cols[COL_View_REVIWEDFLAG].Width = 18;
                c1PatientDetails.Cols[COL_D_CAT_ID].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_NAME].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_NOTEFLAG].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_EXTRAFLAG].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_SOURCEMACHINE].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_SYSTEMFOLDER].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_CONTAINER].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_CATEGORY].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_PATIENTID].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_YEAR].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_MONTH].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_SOURCEBIN].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_INUSED].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_USEDMACHINE].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_USEDTYPE].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_PATH].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_FILENAME].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_MACHINEID].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_COLTYPE].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_VERSIONNO].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_VERSIONNO].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_ISREVIWED].Visible = false;
                c1PatientDetails.Cols[COL_D_CAT_REVIWEDFLAG].Visible = false;
                c1PatientDetails.Cols[COL_View_CategoryHidden].Visible = false;
                c1PatientDetails.Cols[COL_View_Category].Visible = true;
                c1PatientDetails.Cols[COL_View_Category].AllowEditing = false;
                c1PatientDetails.Cols[COL_View_Month].Visible = false;
                c1PatientDetails.Cols[COL_View_Month].AllowEditing = false;
                c1PatientDetails.Cols[COL_View_DocumentName].Visible = true;
                c1PatientDetails.Cols[COL_View_DocumentName].AllowEditing = false;
                c1PatientDetails.Cols[COL_View_NOTEFLAG].Visible = true;
                c1PatientDetails.Cols[COL_View_NOTEFLAG].AllowEditing = false;
                c1PatientDetails.Cols[COL_View_REVIWEDFLAG].Visible = true;
                c1PatientDetails.Cols[COL_View_REVIWEDFLAG].AllowEditing = false;
                c1PatientDetails.SetData(0, COL_View_CategoryHidden, "Category");
                c1PatientDetails.Cols[COL_View_CategoryHidden].TextAlign = TextAlignEnum.LeftCenter;
                c1PatientDetails.SetData(0, COL_View_Category, "Category");
                c1PatientDetails.Cols[COL_View_Category].TextAlign = TextAlignEnum.LeftCenter;
                c1PatientDetails.Cols[COL_View_Category].DataType = typeof(System.String);
                c1PatientDetails.Cols[COL_D_CAT_YEAR].DataType = typeof(System.String);
                c1PatientDetails.Cols[COL_D_CAT_FILENAME].DataType = typeof(System.String);
                c1PatientDetails.SetData(0, COL_View_DocumentName, "Document Name");
                c1PatientDetails.Cols[COL_View_DocumentName].TextAlign = TextAlignEnum.GeneralCenter;

                c1PatientDetails.ExtendLastCol = false;
                c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1PatientDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;

                #endregion " Grid Design "

                oDocuments = oList.GetPatientBaseDocuments(PatientID, gloPMGlobal.ClinicID);
                ArrayList arrYears = new ArrayList();
                if ((oDocuments != null))
                {

                    for (Int16 k = 0; k <= oDocuments.Count - 1; k++)
                    {
                        for (int j = 0; j <= oDocuments[k].EContainers.Count - 1; j++)
                        {
                            //filter for date
                            if (dtpFromDate.Enabled == true)
                            {
                                if (!(oDocuments[k].CreatedDateTime.Date >= dtpFromDate.Value.Date & oDocuments[k].CreatedDateTime.Date <= dtpToDate.Value.Date))
                                {
                                    continue;
                                }
                            }

                            //filter for year
                            if (cmbStatus.Text != "All")
                            {
                                if (oDocuments[k].Year != cmbStatus.Text)
                                {
                                    continue;
                                }
                            }

                            //filter for month
                            if (cmbProvider.Text != "All")
                            {
                                //Check value for month is numeric as there may be values 10 or October by Rohit on 2011-10-13.
                                if ((oDocuments[k].Month != cmbProvider.Text))
                                {
                                    if ((Convert.ToString(cmbProvider.SelectedValue) != oDocuments[k].Month))
                                    {
                                        continue;
                                    }
                                }
                            }

                            if (arrYears.Contains(oDocuments[k].Year) == false)
                            {
                                arrYears.Add(oDocuments[k].Year);
                            }

                            c1PatientDetails.Rows.Add();
                            c1PatientDetails.Cols[COL_View_DocumentName].TextAlign = TextAlignEnum.LeftCenter;

                            C1.Win.C1FlexGrid.CellRange rgStyle = c1PatientDetails.GetCellRange(c1PatientDetails.Rows.Count - 1, COL_View_DocumentName, c1PatientDetails.Rows.Count - 1, COL_View_DocumentName);
                            rgStyle.StyleNew.DataType = typeof(string);
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_View_DocumentName, oDocuments[k].DocumentName);
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_SOURCEMACHINE, "");
                            // Source Machine
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_SYSTEMFOLDER, "");
                            // System Folder
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_CONTAINER, oDocuments[k].EContainers[j].EContainerID);
                            // Container
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_View_Category, oDocuments[k].Category);
                            // Category
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_PATIENTID, PatientID);
                            // Patient ID
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_YEAR, oDocuments[k].Year);
                            // Year
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_MONTH, oDocuments[k].Month);
                            // Month
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_SOURCEBIN, "");
                            // Source Bin
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_INUSED, "");
                            // In Used
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_USEDMACHINE, "");
                            // Used Machine
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_USEDTYPE, "");
                            // Used Type
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_PATH, "");
                            // Path
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_COLTYPE, Convert.ToInt32(enumColType.Document));
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_FILENAME, oDocuments[k].EContainers[j].EDocumentID);
                            //' DocumentID
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_MACHINEID, "");
                            c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_VERSIONNO, "");

                            if (oDocuments[k].HasNote == true)
                            {
                                c1PatientDetails.SetCellImage(c1PatientDetails.Rows.Count - 1, COL_View_NOTEFLAG, gloPM.Properties.Resources.High_Priority02);
                            }
                            if (oDocuments[k].IsAcknowledge == true)
                            {
                                c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_ISREVIWED, 1);
                                c1PatientDetails.SetCellImage(c1PatientDetails.Rows.Count - 1, COL_View_REVIWEDFLAG, gloPM.Properties.Resources.High_Priority03);
                                c1PatientDetails.Cols[COL_View_REVIWEDFLAG].ImageAlign = ImageAlignEnum.CenterCenter;
                            }
                            else
                            {
                                c1PatientDetails.SetData(c1PatientDetails.Rows.Count - 1, COL_D_CAT_ISREVIWED, 0);
                                //c1PatientDetails.SetCellImage(c1PatientDetails.Rows.Count - 1, COL_View_REVIWEDFLAG,new Image());
                                c1PatientDetails.Cols[COL_View_REVIWEDFLAG].ImageAlign = ImageAlignEnum.CenterCenter;
                            }
                        }
                    }
                }

                if (arrYears.Count > 0)
                {
                    arrYears.Sort();
                    for (int iYear = 0; iYear <= arrYears.Count - 1; iYear++)
                    {
                        if (cmbStatus.Items.Contains(arrYears[iYear]) == false)
                        {
                            cmbStatus.Items.Add(arrYears[iYear]);
                        }
                    }
                }

                arrYears = null;

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "Patient Document List viewed from DashBoard", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }

            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                c1PatientDetails.Visible = true;
                if ((oDocuments != null))
                {
                    oDocuments.Dispose();
                    oDocuments = null;
                }

                if ((oList != null))
                {
                    oList.Dispose();
                    oList = null;
                }
            }
        }

        private void FillMonthListInProviderCombo()
        {
            //Local variable.
            System.DateTime tempDate = Convert.ToDateTime("01/01/2011");

            //Clear binding from parent control
            cmbProvider.DataSource = null;
            cmbProvider.Items.Clear();

            //Temporary data table for the filter
            DataTable dt = new DataTable();
            dt.Columns.Add("sMonthname");
            dt.Columns.Add("nMonth");
            dt.Rows.Clear();

            //for All Months filter
            dt.Rows.Add();
            dt.Rows[0]["sMonthname"] = "All";
            dt.Rows[0]["nMonth"] = 0;

            //Creation of filter criteria
            for (Int16 iMonth = 1; iMonth <= 12; iMonth++)
            {
                dt.Rows.Add();
                dt.Rows[iMonth]["sMonthname"] = (enumMonth)iMonth;
                dt.Rows[iMonth]["nMonth"] = iMonth;
                tempDate = tempDate.AddMonths(1);
            }

            // Set data source for dropdown list 
            cmbProvider.DataSource = dt;

            // Set Display and View member for dropdown from data source
            cmbProvider.DisplayMember = dt.Columns["sMonthname"].ToString();
            cmbProvider.ValueMember = dt.Columns["nMonth"].ToString();

        }
        #endregion "DMS"

        private void mnuItem_NewWCForm_Click(object sender, EventArgs e)
        {



            //gloBilling.WC_Forms.frmWCPatientClaims ofrmWCPatientClaims = new gloBilling.WC_Forms.frmWCPatientClaims(_CurrentPatientId);

            //try
            //{
            //    ofrmWCPatientClaims.ShowDialog(this);
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
            //finally
            //{
            //    //SLR: Finaly dispose ofrmPriorAuthorization
            //    if (ofrmWCPatientClaims != null)
            //    {
            //        ofrmWCPatientClaims.Dispose();
            //    }
            //}

        }

        private void mnuItem_ModifyWCForm_Click(object sender, EventArgs e)
        {
            mnuItem_Modify_Click(null, null);
        }

        private void c4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (_CurrentPatientId == 0)
            //    return;
            frmWorkerCompFormViewer frmWorkerComp = null;
            try
            {

                Int64 _TransactionID = 0; //Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 2));
                Int64 _TransactionMasterID = 0;// Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 1));

                using (frmWorkerComp = new frmWorkerCompFormViewer(_CurrentPatientId, _TransactionID, _TransactionMasterID, gloPMGlobal.DatabaseConnectionString, 0))
                {
                    frmWorkerComp.ClaimNo = "";// Convert.ToString(c1Claims.GetData(c1Claims.RowSel, 5));
                    frmWorkerComp.ShowDialog(this);
                }

                FillPatientWorkerCompsForms(_CurrentPatientId);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                frmWorkerComp = null;
            }
        }

        private void c42ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWorkerCompFormViewer_C42 frmWorkerComp_C42 = null;
            try
            {

                Int64 _TransactionID = 0; //Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 2));
                Int64 _TransactionMasterID = 0;// Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 1));

                //using (frmWorkerComp_C42 = new frmWorkerCompFormViewer_C42(_CurrentPatientId, _TransactionID, _TransactionMasterID, gloPMGlobal.DatabaseConnectionString, 0))
                //{
                //    frmWorkerComp_C42.ClaimNo = "";// Convert.ToString(c1Claims.GetData(c1Claims.RowSel, 5));
                //    frmWorkerComp_C42.ShowDialog(this);
                //}

                frmWorkerComp_C42 = new frmWorkerCompFormViewer_C42(_CurrentPatientId, _TransactionID, _TransactionMasterID, gloPMGlobal.DatabaseConnectionString, 0);
                frmWorkerComp_C42.ClaimNo = "";
                frmWorkerComp_C42.ShowDialog(this);

                FillPatientWorkerCompsForms(_CurrentPatientId);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                frmWorkerComp_C42.Dispose();
                frmWorkerComp_C42 = null;
            }
        }

        private void c4AUTHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWorkerCompFormViewer_C4Auth frmWorkerComp_C4Auth = null;
            try
            {

                Int64 _TransactionID = 0; //Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 2));
                Int64 _TransactionMasterID = 0;// Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 1));

                frmWorkerComp_C4Auth = new frmWorkerCompFormViewer_C4Auth(_CurrentPatientId, _TransactionID, _TransactionMasterID, gloPMGlobal.DatabaseConnectionString, 0);
                frmWorkerComp_C4Auth.ClaimNo = "";
                frmWorkerComp_C4Auth.ShowDialog(this);

                FillPatientWorkerCompsForms(_CurrentPatientId);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                frmWorkerComp_C4Auth.Dispose();
                frmWorkerComp_C4Auth = null;
            }
        }

        private void mG2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWorkerCompFormViewer_MG2 frmWorkerComp_MG2 = null;
            try
            {

                Int64 _TransactionID = 0; //Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 2));
                Int64 _TransactionMasterID = 0;// Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 1));

                frmWorkerComp_MG2 = new frmWorkerCompFormViewer_MG2(_CurrentPatientId, _TransactionID, _TransactionMasterID, gloPMGlobal.DatabaseConnectionString, 0);
                frmWorkerComp_MG2.ClaimNo = "";
                frmWorkerComp_MG2.ShowDialog(this);

                FillPatientWorkerCompsForms(_CurrentPatientId);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                frmWorkerComp_MG2.Dispose();
                frmWorkerComp_MG2 = null;
            }
        }

        private void mG21ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWorkerCompFormViewer_MG21 frmWorkerComp_MG21 = null;
            try
            {

                Int64 _TransactionID = 0; //Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 2));
                Int64 _TransactionMasterID = 0;// Convert.ToInt64(c1Claims.GetData(c1Claims.RowSel, 1));

                frmWorkerComp_MG21 = new frmWorkerCompFormViewer_MG21(_CurrentPatientId, _TransactionID, _TransactionMasterID, gloPMGlobal.DatabaseConnectionString, 0);
                frmWorkerComp_MG21.ClaimNo = "";
                frmWorkerComp_MG21.ShowDialog(this);

                FillPatientWorkerCompsForms(_CurrentPatientId);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                frmWorkerComp_MG21.Dispose();
                frmWorkerComp_MG21 = null;
            }
        }

        private void mnuTools_CodeGuide_Click(object sender, EventArgs e)
        {
            gloUIControlLibrary.WPFForms.frmCodeGuide ofrmCodeGuide = null;

            try
            {
                ofrmCodeGuide = gloUIControlLibrary.WPFForms.frmCodeGuide.CheckInstance();

                if (ofrmCodeGuide == null)
                {
                    ofrmCodeGuide = gloUIControlLibrary.WPFForms.frmCodeGuide.GetCodeGuideInstance(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    WindowInteropHelper interopHelper = new WindowInteropHelper(ofrmCodeGuide);
                    interopHelper.Owner = this.Handle;
                    System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(ofrmCodeGuide);
                }


                ofrmCodeGuide.WindowState = System.Windows.WindowState.Maximized;
                ofrmCodeGuide.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                ofrmCodeGuide.Show();
                ofrmCodeGuide.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading code guide screen.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {

            }
        }

        private void mnuReports_ChargeEditReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptTriggeredClaimRuleInformation", "Charge Edit Report", true, mnuReports_ChargeEditReport.Image);
        }

        private void mnuReports_InactiveCPTSReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptCPTActiveDates", "Inactive CPT Usage Report", true, mnuReports_InactiveCPTSReport.Image);
        }

        private void mnuEdit_RCMCagetory_Click(object sender, EventArgs e)
        {
            bool _RCMDMSRunning = false;
            try
            {
                foreach (Form formItem in this.MdiChildren)
                {
                    if (formItem.Name == "frmEDocumentViewer")
                    {
                        _RCMDMSRunning = true;
                        break;
                    }
                }
                if (_RCMDMSRunning)
                {
                    MessageBox.Show("You can not add,modify and delete category while scan/view RCM document is open.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    gloEDocumentV3.gloEDocV3Management oRCMDOCCategory = new gloEDocumentV3.gloEDocV3Management();
                    try
                    {
                        oRCMDOCCategory.ShowEDocumentCategory(gloEDocumentV3.Enumeration.enum_OpenExternalSource.RCM);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading code guide screen." + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ex = null;
                    }
                    finally
                    {
                        oRCMDOCCategory.Dispose(); oRCMDOCCategory = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading code guide screen." + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ex = null;
            }

        }

        public void OpenSetupCases(long nPatientID, long nCaseID, Form frmParent = null)
        {
            clsCaseSetup.OpenSetupCasesDialog(nPatientID, nCaseID, frmParent);

        }
        public void OpenPMAlert(string strConnection, long nPatientID, string strAppName)
        {
            Form frmPatAlerts = new gloPatientStripControl.frmPatientAlerts(strConnection, nPatientID, strAppName);
            frmPatAlerts.ShowDialog(this);
            frmPatAlerts.Dispose();
            frmPatAlerts = null;
        }
        public ArrayList GetUserRightsArrayForPatientStrip()
        {
            return clsLoginUserRights.GetUserRightsArrayForPatientStrip();
        }
        public void OpenEMRAlert(long nPatientID)
        {
            Form frmPatAlerts = new frmPatientAlerts(nPatientID);
            frmPatAlerts.ShowDialog(this);
            frmPatAlerts.Dispose();
            frmPatAlerts = null;
        }

        public void OpenIntuitSendNewMessage(string age, long nPatEducationID = 0)
        {

            // SLR -> Following has to be corrected;
            return;
            //gloPatientPortalCommon.ClsIntuitSecureMessage clsIntuit = new gloPatientPortalCommon.ClsIntuitSecureMessage(gloPMGlobal.DatabaseConnectionString, _CurrentPatientId, 1, "PMMACHINE", gloPMGlobal.UserName);
            //Int16 PatientStatus = default(Int16);

            //clsgloPatientPortalEmail clsPatientPortal = new clsgloPatientPortalEmail(gloPMGlobal.DatabaseConnectionString);
            //try
            //{
            //    //'Added for MU2 Patient Portal - If Intuit setting is turn on then send secure message to Intuit Portal else gloStream Patient Portal on 20130823
            //    if (gblnUSEINTUITINTERFACE == true && gblnIntuitCommunication == true)
            //    {
            //        PatientStatus = clsIntuit.CheckValidUser();
            //        if (PatientStatus > 0)
            //        {
            //            ShowSecureMessage(PatientStatus, txtSearchIntuit.Text, nPatEducationID: nPatEducationID);
            //            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, "Patient portal Secure Messages Send New Screen Opened from DashBoard", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            //        }
            //    }
            //    else if (gblnPatientPortalEnabled == true && gblnIntuitCommunication == true)
            //    {
            //        DataTable dtPortalValidUser = clsPatientPortal.ToCheckPatientRegisterOrNotOnPortal(gnPatientID);
            //        if (dtPortalValidUser != null && dtPortalValidUser.Rows.Count > 0)
            //        {
            //            ShowSecureMessage(1, txtSearchIntuit.Text, "gloPatientPortal", nPatEducationID);
            //            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, "Patient portal Secure Messages Send New Screen Opened from DashBoard", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Patient does not have an active portal account. No portal messages may be sent to this patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        //'slr free dtPortalValidUser
            //        if (dtPortalValidUser != null)
            //        {
            //            dtPortalValidUser.Dispose();
            //            dtPortalValidUser = null;
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            //}
            //finally
            //{
            //    //'slr free clsIntuit
            //    if (clsIntuit != null)
            //    {
            //        clsIntuit.Dispose();
            //    }
            //    clsIntuit = null;
            //    clsPatientPortal = null;
            //}
            //return;
        }

        private void c1PatientDetails_AfterSort(object sender, SortColEventArgs e)
        {
            try
            {
                if (
                    _SelcetedPatient == PatientDetails.PatientNYWorkersCompForms
                    &&
                    sender != null
                    &&
                    e.Col == 4
                    )
                {
                    C1.Win.C1FlexGrid.C1FlexGrid flexGrid = sender as C1.Win.C1FlexGrid.C1FlexGrid;

                    if (flexGrid.Cols != null && flexGrid.Cols.Count > 15)
                    {
                        if (flexGrid.Cols[e.Col].Caption == "Claim #")
                        {
                            if (
                                flexGrid.Cols[flexGrid.Cols.Count - 2].Caption == "SortClaim"
                                &&
                                flexGrid.Cols[flexGrid.Cols.Count - 1].Caption == "SortSubClaim"
                                )
                            {
                                flexGrid.Cols[flexGrid.Cols.Count - 2].Sort = e.Order;
                                flexGrid.Cols[flexGrid.Cols.Count - 1].Sort = SortFlags.Ascending;
                                flexGrid.Sort(SortFlags.UseColSort, flexGrid.Cols.Count - 2, flexGrid.Cols.Count - 1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private void mnu_MISReports_QualityMeasures_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptPQRS", "Quality Measures", true, null);
        }

        private void mnu_MISReports_ChargesVSAllowedReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rpt_ChargeAllowed", "Charges v/s Allowed Report", true, null);
        }

        private void mnu_MISReports_PayerLagReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rpt_PayerLag", "Payer Lag Time Report", true, null);
        }

        private void mnuReports_CollectionExport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rpt_US_Collection_Export", "Collection Export", true, null);
        }

        private Int64 SyncPatient(string calledFrom = "")
        {
            long SelectedPatient = 0;
            Tuple<Int64, string, string> CurrentPatient = null;
            Tuple<Int64, string, string> DashBoardPatient = new Tuple<Int64, string, string>(gloCntrlPatient.PatientID, gloCntrlPatient.PatientCode, gloCntrlPatient.LastName + ", " + gloCntrlPatient.FirstName);
            GetSyncPatientSetting();
            SelectedPatient = DashBoardPatient.Item1;
            try
            {
                Form activeChild = this.ActiveMdiChild;
                if (activeChild != null)
                {
                    if (activeChild.Text != calledFrom)
                    {
                        if (activeChild.Text == "Patient Account")
                        {
                            CurrentPatient = ((gloAccountsV2.frmPatientFinancialViewV2)activeChild).SyncPatientId;
                        }
                        else if (activeChild.Text == "Charges")
                        {
                            CurrentPatient = ((gloBilling.frmBillingTransaction)activeChild).SyncPatientId;
                        }
                        else if (activeChild.Text == "Patient Forms")
                        {
                            CurrentPatient = ((gloOffice.frmWd_ViewPatientTemplates)activeChild).SyncPatientId;
                        }
                        else if (activeChild.Text == "Revenue Cycle")
                        {
                            CurrentPatient = ((gloBilling.Collections.frmRevenueCycle)activeChild).SyncPatientId;
                        }
                        else if (activeChild.Text.Contains("View Documents"))
                        {
                            if (((gloEDocumentV3.Forms.frmEDocumentViewer)activeChild).OpenEDocumentAs == gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewAllDocuments)
                            {
                                CurrentPatient = ((gloEDocumentV3.Forms.frmEDocumentViewer)activeChild).SyncPatientId;
                            }
                        }

                        if (SyncPatientSetting == null)//If User specific setting is not present
                        {
                            if (CurrentPatient != null)
                            {
                                if (DashBoardPatient.Item1 != CurrentPatient.Item1)
                                {
                                    bool isFormOpen = false;

                                    foreach (Form OpenForm in Application.OpenForms)
                                    {
                                        if (OpenForm.Text == calledFrom)
                                        {
                                            isFormOpen = true;
                                            if (OpenForm.Text == "Patient Account")
                                            {
                                                CurrentPatient = ((gloAccountsV2.frmPatientFinancialViewV2)OpenForm).SyncPatientId;
                                            }
                                            //else if (OpenForm.Text == "Charges")
                                            //{
                                            //    CurrentPatient = ((gloBilling.frmBillingTransaction)OpenForm).SyncPatientId;
                                            //}
                                            //else if (OpenForm.Text == "Patient Forms")
                                            //{
                                            //    CurrentPatient = ((gloOffice.frmWd_ViewPatientTemplates)OpenForm).SyncPatientId;
                                            //}
                                            else if (OpenForm.Text == "Revenue Cycle")
                                            {
                                                CurrentPatient = ((gloBilling.Collections.frmRevenueCycle)OpenForm).SyncPatientId;
                                            }
                                            //else if (OpenForm.Text.Contains("View Documents"))
                                            //{
                                            //    if (((gloEDocumentV3.Forms.frmEDocumentViewer)activeChild).OpenEDocumentAs == gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewAllDocuments)
                                            //    {
                                            //        CurrentPatient = ((gloEDocumentV3.Forms.frmEDocumentViewer)OpenForm).SyncPatientId;
                                            //    }
                                            //}
                                            SelectedPatient = CurrentPatient.Item1;
                                            break;
                                        }
                                    }
                                    if (isFormOpen == false)
                                    {
                                        frmSyncPatient oSync = new frmSyncPatient(DashBoardPatient, CurrentPatient);
                                        oSync.BringToFront();
                                        oSync.ShowDialog();
                                        SelectedPatient = oSync.SelectedPatientId;

                                    }
                                }
                            }
                        }
                        else if (SyncPatientSetting == "Current")//Matched with Values from frmSyncPatient
                        {
                            SelectedPatient = CurrentPatient.Item1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }

            return SelectedPatient;
        }

        private void GetSyncPatientSetting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dtSyncPatient = null;
            try
            {
                oDB.Connect(false);
                string query = "Select sSettingsValue as SettingsValue from Settings where sSettingsName ='SyncPatient' and nUserid=" + gloGlobal.gloPMGlobal.UserID + "";
                oDB.Retrive_Query(query, out dtSyncPatient);
                if (dtSyncPatient.Rows.Count > 0 && dtSyncPatient.Rows[0]["SettingsValue"].ToString() != "")
                {
                    oDB.Disconnect();
                    SyncPatientSetting = Convert.ToString(dtSyncPatient.Rows[0]["SettingsValue"]);
                }
                else
                {
                    SyncPatientSetting = null;
                    oDB.Disconnect();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void mnu_MISReports_PaymentPlanReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rpt_Patient_Paymentplan", "Patient Payment Plan Report", true, null);
        }

        private void tmrSingleSignOn_Tick(object sender, EventArgs e)
        {
            if (sslbl_SingleSignOn.Visible)
            {
                sslbl_SingleSignOn.Visible = false;
                tmrSingleSignOn.Enabled = false;
            }
        }

        private void mnu_rpt_ConfirmAppointments_Click(object sender, EventArgs e)
        {
            gloAppointmentScheduling.frmRpt_AppointmentStatus ofrm = new gloAppointmentScheduling.frmRpt_AppointmentStatus(gloPMGlobal.DatabaseConnectionString);
            ofrm.MdiParent = this;
            ofrm.WindowState = FormWindowState.Maximized;
            ofrm.ShowInTaskbar = false;
            ofrm.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuGo_CollectionAgencyRefund_Click(object sender, EventArgs e)
        {
            DataTable dtAcct = null;
            DataRow[] drAccountToSelect = null;
            gloAccountsV2.frmCollectionAgencyPaymentRefund ofrmPatientRefundvoid = new gloAccountsV2.frmCollectionAgencyPaymentRefund(gloPMGlobal.DatabaseConnectionString);
            try
            {
                ofrmPatientRefundvoid.SelectedPatientId = _CurrentPatientId;
                ofrmPatientRefundvoid.ShowDialog(this);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (ofrmPatientRefundvoid != null) { ofrmPatientRefundvoid.Dispose(); ofrmPatientRefundvoid = null; }
                if (dtAcct != null) { dtAcct.Dispose(); dtAcct = null; }
                if (drAccountToSelect != null) { drAccountToSelect = null; }
            }

        }

        public static DataTable GetPatientAccounts(long PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtAccounts = null;

            try
            {
                if (PatientID > 0)
                {
                    oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);
                    oDB.Retrive("PA_Select_PatientsAccounts", oParameters, out dtAccounts);
                    oDB.Disconnect();

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oParameters != null)
                    oParameters.Dispose();
            }
            return dtAccounts;
        }

        private void mnu_MISReports_BadDebtCollectionReport_Click(object sender, EventArgs e)
        {
            ShowSSRSReport("rptBadDebt", "Bad Debt Report", true, null);

        }

        bool bIsShowSplashScreen = false;
        private void LogOut()
        {
            bIsShowSplashScreen = true;
            this.Close();
        }

        private void mnuTools_ChangePassword_Click(object sender, EventArgs e)
        {
            using (gloGlobal.ChangePassword.frmChangePassword ofrmChangePassword = new gloGlobal.ChangePassword.frmChangePassword())
            {
                if (ofrmChangePassword.ShowDialog(((ofrmChangePassword.Parent==null)?this:ofrmChangePassword.Parent))==DialogResult.OK)
                {
                    LogOut();
                }
            }
        }

        bool bInSideRefreshDevices = false;
        private void mnuSetting_RefreshDevicesPrinters_Click(object sender, EventArgs e)
        {
            if (bInSideRefreshDevices)
            {
                return;
            }
            bInSideRefreshDevices = true;
            bool bisMsgShown = false;
            gloEDocumentV3.Common.RemoteScanCommon oRemoteScanCommon = new gloEDocumentV3.Common.RemoteScanCommon();
            gloPrintDialog.frmTSPrintDialog oTSDialog = new gloPrintDialog.frmTSPrintDialog();
            try
            {
                if ((gloGlobal.gloTSPrint.isCopyPrint || gloGlobal.gloRemoteScanSettings.EnableRemoteScan))
                {
                    if (!gloGlobal.gloTSPrint.isMapped())
                    {
                        MessageBox.Show("Unable to find mapped drive. Please check whether gloLDSSniffer Service is running. Looks like you have not enabled mapping while connecting to RDP.", gloGlobal.gloTSPrint.getMessageCaption(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (!oTSDialog.RefreshPrinters())
                        {
                            MessageBox.Show("Unable to refresh priter list, Please try after some time.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bisMsgShown = true;
                        }
                    }

                    if (gloGlobal.gloRemoteScanSettings.EnableRemoteScan)
                    {
                        gloGlobal.gloRemoteScanSettings.AssignReEvaluate();
                        gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist();
                        if (!gloGlobal.gloRemoteScanSettings.isScanServiceWorking(true))
                        {
                            return;
                            // TODO: Labeled Arguments not supported. Argument: 1 := 'showMsg'
                        }

                        if ((gloRemoteScanGeneral.RemoteScanSettings.RefreshScanners() == true))
                        {
                            string sRetVal = null;
                            sRetVal = oRemoteScanCommon.SetRemoteScannerCurrentSettings(null, null, null);
                            if (!string.IsNullOrEmpty(sRetVal))
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure);
                            }
                            gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject();
                        }
                        else
                        {
                            MessageBox.Show("Unable to update scanner list, Please try after some time.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bisMsgShown = true;
                        }
                    }
                    if (!bisMsgShown)
                    {
                        MessageBox.Show("Local devices and printer list refreshed.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Devices not refreshed because Local Print/Scan not enabled.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Update clients machine name
                gloAuditTrail.MachineDetails.MachineInfo myRemoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails(true);
                gloGlobal.gloTSPrint.sClientLocalMachineName = myRemoteMachine.MachineName;

                gloWord.LoadAndCloseWord.CurrentSessionID = System.Diagnostics.Process.GetCurrentProcess().SessionId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oRemoteScanCommon = null;
                oTSDialog = null;
                bInSideRefreshDevices = false;
            }
        }

        #region "Claim Manager"

        #region "Variables For CM Printing"

        gloPM.Forms.frmgloPrintClaimsProgress objfrmgloPrintClaimsProgress = null;
        List<gloPM.Forms.frmgloPrintClaimsProgress> oListobjfrmgloPrintClaimsProgress = new List<gloPM.Forms.frmgloPrintClaimsProgress>();

        bool bIsPause = false;
        int printedDoc = 0;
        object otype;
        DataTable odtClaimid;

        #endregion

        #region "BackGround Printing"

        enum PrintType
        {
            PrintOnForm,
            PrintData
        }

        #region "Printing Single Batch"

        public void EnableThreadQueue(object type, DataTable dtClaimID)
        {            
            try
            {
                //if (!((Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 5 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 7 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 3 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 1 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1)))
                if (!((Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) && ((Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 1) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 3) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 5)  || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 7 ))))
                {
                    if (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 4 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) 
                    {
                        MessageBox.Show("UB04 claims of billing type Professional cannot be printed.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (thrdQueue == null)
                    {
                        bIsPause = false;
                        objfrmgloPrintClaimsProgress = new gloPM.Forms.frmgloPrintClaimsProgress(dtClaimID.Rows.Count, Convert.ToString(dtClaimID.Rows[0]["BatchName"]));
                        objfrmgloPrintClaimsProgress.PausePrinting -= new frmgloPrintClaimsProgress.PauseProgressControlsDelegate(PausePrinting);
                        objfrmgloPrintClaimsProgress.PausePrinting += new frmgloPrintClaimsProgress.PauseProgressControlsDelegate(PausePrinting);
                        objfrmgloPrintClaimsProgress.PlayPrinting -= new frmgloPrintClaimsProgress.PlayProgressControlsDelegate(PlayPrinting);
                        objfrmgloPrintClaimsProgress.PlayPrinting += new frmgloPrintClaimsProgress.PlayProgressControlsDelegate(PlayPrinting);
                        objfrmgloPrintClaimsProgress.ClosePrinting -= new frmgloPrintClaimsProgress.CloseProgressControlsDelegate(ClosePrinting);
                        objfrmgloPrintClaimsProgress.ClosePrinting += new frmgloPrintClaimsProgress.CloseProgressControlsDelegate(ClosePrinting);

                        objfrmgloPrintClaimsProgress.Name = Convert.ToString(dtClaimID.Rows[0]["BatchName"]);
                        UpdateProgressBarControl(false, dtClaimID.Rows.Count, 0, objfrmgloPrintClaimsProgress);
                        otype = type; odtClaimid = dtClaimID; printedDoc = 0;
                        thrdQueue = new Thread(obj => { frmBillingBatch_Print(type, dtClaimID); });
                        thrdQueue.IsBackground = true;
                        thrdQueue.Start();
                    }
                    else
                    {
                        MessageBox.Show("'Claim Manager' is printing for Batch Name - '" + objfrmgloPrintClaimsProgress.Name + "' is in progress. You cannot print the Batch until this process is completed. Please try after some time.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Electronic claims of billing type Professional cannot be printed.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        void frmBillingBatch_Print(object type, DataTable dtClaimID)
        {
            int _BillingTypeId = 0;
            int _nBillingMethodID = 0;
            gloDatabaseLayer.DBLayer oDB = null;
            
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                if (dtClaimID.Rows.Count != 0)
                {
                    #region "Print All Claims"
                    
                    for (int i = 0; i < dtClaimID.Rows.Count; i++)
                    {
                        if (bIsPause == false && i >= printedDoc)
                        {
                            _BillingTypeId = Convert.ToInt32(dtClaimID.Rows[i]["BillingTypeID"]);
                            _nBillingMethodID = Convert.ToInt32(dtClaimID.Rows[i]["BillingMethodID"]);

                            if ((_BillingTypeId == 1 || _BillingTypeId == 0) && (_nBillingMethodID == 2))
                            {
                                #region "Print Form"

                                gloCMSEDI.HCFA1500 ofrm = new gloCMSEDI.HCFA1500(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                ofrm.FillTransactionOnForm(Convert.ToInt64(dtClaimID.Rows[i]["TransactionID"]), Convert.ToInt64(dtClaimID.Rows[i]["MasterTransactionID"]));

                                if ((PrintType)type == PrintType.PrintData)
                                {
                                    ofrm.IsPrintForm = false;
                                    if (_BillingTypeId == 1 || _BillingTypeId == 0)
                                    {
                                        ofrm.PrintData("Claimservice");
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    }
                                }
                                else
                                {
                                    ofrm.IsPrintForm = true;
                                    if (_BillingTypeId == 1 || _BillingTypeId == 0)
                                    {
                                        ofrm.PrintForm("Claimservice");
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    }
                                }

                                ofrm.Dispose();
                                ofrm = null;

                                #endregion
                            }
                            else if ((_BillingTypeId == 1 || _BillingTypeId == 0) && (_nBillingMethodID == 8))
                            {
                                #region "Print Form"

                                gloCMSEDI.HCFA1500New ofrm = new gloCMSEDI.HCFA1500New(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                ofrm.FillTransactionOnForm(Convert.ToInt64(dtClaimID.Rows[i]["TransactionID"]), Convert.ToInt64(dtClaimID.Rows[i]["MasterTransactionID"]));
                                if ((PrintType)type == PrintType.PrintData)
                                {
                                    ofrm.IsPrintForm = false;
                                    if (_BillingTypeId == 1 || _BillingTypeId == 0)
                                    {
                                        ofrm.PrintData("Claimservice");
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    }
                                }
                                else
                                {
                                    ofrm.IsPrintForm = true;
                                    if (_BillingTypeId == 1 || _BillingTypeId == 0)
                                    {
                                        ofrm.PrintForm("Claimservice");
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    }
                                }

                                ofrm.Dispose();
                                ofrm = null;

                                #endregion
                            }
                            else if (_BillingTypeId == 2)
                            {
                                #region "Print Form"
                                gloCMSEDI.UB04 oUB04 = new gloCMSEDI.UB04(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                oUB04.FillUB04Form(Convert.ToInt64(dtClaimID.Rows[i]["TransactionID"]), Convert.ToInt64(dtClaimID.Rows[i]["MasterTransactionID"]));
                                if ((PrintType)type == PrintType.PrintData)
                                {
                                    if (_BillingTypeId == 2)
                                    {
                                        oUB04.PrintData("Claimservice");
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    }
                                }
                                else
                                {
                                    if (_BillingTypeId == 2)
                                    {
                                        oUB04.PrintForm("Claimservice");
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                    }
                                }

                                oUB04.Dispose();
                                oUB04 = null;

                                #endregion
                            }
                            UpdateProgressBarControl(true, 0, i + 1, objfrmgloPrintClaimsProgress);
                        }
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show("Please select Paper Claim to print.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (bIsPause == false)
                {
                    DisposefrmgloPrintClaimsProgress();

                    if (oDB != null)
                    { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

                    if (dtClaimID != null)
                    { dtClaimID = null; }

                    if (thrdQueue != null)
                    {
                        thrdQueue = null;
                        foreach (Form OpenForm in Application.OpenForms)
                        {
                            if (OpenForm.Name == "frmBillingBatch_New")
                            {
                                frmBillingBatch_New oViewBilling = frmBillingBatch_New.GetInstance();
                                oViewBilling.Invoke(new Action(() => oViewBilling.FillC1AllBatch()));
                            }
                        }
                    }
                }
            }
        }

        #region "ProgressBar for Printing Single Batch"

        public delegate void DisposefrmgloPrintClaimsProgressV2();
        public void DisposefrmgloPrintClaimsProgress()
        {
            try
            {
                if (InvokeRequired)
                {
                    objfrmgloPrintClaimsProgress.Invoke(new DisposefrmgloPrintClaimsProgressV2(DisposefrmgloPrintClaimsProgress_Final));
                }
                else
                {
                    DisposefrmgloPrintClaimsProgress_Final();
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
            }
        }

        private void DisposefrmgloPrintClaimsProgress_Final()
        {
            try
            {
                if (objfrmgloPrintClaimsProgress != null)
                {
                    objfrmgloPrintClaimsProgress.Dispose();
                    objfrmgloPrintClaimsProgress = null;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
            }
        }

        public void UpdateProgressBarControl(bool InvokeRequired, Int32 nMaxClaimCount = 0, int increamentvalue = 0, gloPM.Forms.frmgloPrintClaimsProgress objfrmgloPrintClaimsProgress = null)
        {
            try
            {
                if (!InvokeRequired)
                {
                    objfrmgloPrintClaimsProgress.Show(this);                    
                    //objfrmgloPrintClaimsProgress.BringToFront();
                }
                else
                {
                    if (InvokeRequired)
                    {
                        objfrmgloPrintClaimsProgress.InvokeProgressUpdateControls(increamentvalue);
                    }
                }
                printedDoc = increamentvalue;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        #endregion
        
        #endregion
        
        #region "Printing Multiple Batches"

        //int ThreadCount = 0;
        //IList<string> threadlist = new List<string>();
        //IList<string> Removethreadlist = new List<string>();

        //public void EnableThreadQueue(object type, DataTable dtClaimID)
        //{
        //    EventArgs a = new EventArgs();
        //    try
        //    {
        //        if (!((Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 5 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 7 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 3 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1) || (Convert.ToInt32(dtClaimID.Rows[0]["BillingMethodID"]) == 1 && Convert.ToInt32(dtClaimID.Rows[0]["BillingTypeID"]) == 1)))
        //        {
        //            objfrmgloPrintClaimsProgress = new gloPM.Forms.frmgloPrintClaimsProgress(dtClaimID.Rows.Count, Convert.ToString(dtClaimID.Rows[0]["BatchName"]));
        //            objfrmgloPrintClaimsProgress.Name = Convert.ToString(dtClaimID.Rows[0]["BatchName"]);
        //            UpdateProgressBarControl(false, Convert.ToString(dtClaimID.Rows[0]["BatchName"]), dtClaimID.Rows.Count, 0, objfrmgloPrintClaimsProgress);

        //            ThreadCount++;
        //            thrdQueue = new Thread(obj => { frmBillingBatch_Print(type, dtClaimID); });
        //            thrdQueue.IsBackground = true;
        //            thrdQueue.Name = Convert.ToString(dtClaimID.Rows[0]["BatchName"]);

        //            if (threadlist.Contains(thrdQueue.Name))
        //            {
        //                if (thrdQueue.IsAlive == false) { ThreadCount--; }
        //                MessageBox.Show("'Claim Manager' is Printing for " + Convert.ToString(dtClaimID.Rows[0]["BatchName"]) + " is in progress. You cannot print the same Batch until this process is completed. Please try after some time", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            }
        //            else
        //            {
        //                thrdQueue.Start();
        //                threadlist.Add(thrdQueue.Name);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Electronic Claims of Billing Type Professional Cannot be Printed", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
        //    }
        //}

        //void frmBillingBatch_Print(object type, DataTable dtClaimID)
        //{
        //    int _BillingTypeId = 0;
        //    int _nBillingMethodID = 0;
        //    gloDatabaseLayer.DBLayer oDB = null;

        //    try
        //    {
        //        oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //        if (dtClaimID.Rows.Count != 0)
        //        {
        //            #region "Print All Claims"

        //            for (int i = 0; i < dtClaimID.Rows.Count; i++)
        //            {
        //                _BillingTypeId = Convert.ToInt32(dtClaimID.Rows[i]["BillingTypeID"]);
        //                _nBillingMethodID = Convert.ToInt32(dtClaimID.Rows[i]["BillingMethodID"]);

        //                if ((_BillingTypeId == 1 || _BillingTypeId == 0) && (_nBillingMethodID == 2))
        //                {
        //                    #region "Print Form"

        //                    gloCMSEDI.HCFA1500 ofrm = new gloCMSEDI.HCFA1500(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //                    ofrm.FillTransactionOnForm(Convert.ToInt64(dtClaimID.Rows[i]["TransactionID"]), Convert.ToInt64(dtClaimID.Rows[i]["MasterTransactionID"]));

        //                    if ((PrintType)type == PrintType.PrintData)
        //                    {
        //                        ofrm.IsPrintForm = false;
        //                        if (_BillingTypeId == 1 || _BillingTypeId == 0)
        //                        {
        //                            ofrm.PrintData("Claimservice");
        //                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ofrm.IsPrintForm = true;
        //                        if (_BillingTypeId == 1 || _BillingTypeId == 0)
        //                        {
        //                            ofrm.PrintForm("Claimservice");
        //                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
        //                        }
        //                    }

        //                    ofrm.Dispose();
        //                    ofrm = null;

        //                    #endregion
        //                }
        //                else if ((_BillingTypeId == 1 || _BillingTypeId == 0) && (_nBillingMethodID == 8))
        //                {
        //                    #region "Print Form"

        //                    gloCMSEDI.HCFA1500New ofrm = new gloCMSEDI.HCFA1500New(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //                    ofrm.FillTransactionOnForm(Convert.ToInt64(dtClaimID.Rows[i]["TransactionID"]), Convert.ToInt64(dtClaimID.Rows[i]["MasterTransactionID"]));
        //                    if ((PrintType)type == PrintType.PrintData)
        //                    {
        //                        ofrm.IsPrintForm = false;
        //                        if (_BillingTypeId == 1 || _BillingTypeId == 0)
        //                        {
        //                            ofrm.PrintData("Claimservice");
        //                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ofrm.IsPrintForm = true;
        //                        if (_BillingTypeId == 1 || _BillingTypeId == 0)
        //                        {
        //                            ofrm.PrintForm("Claimservice");
        //                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
        //                        }
        //                    }

        //                    ofrm.Dispose();
        //                    ofrm = null;

        //                    #endregion
        //                }
        //                else if (_BillingTypeId == 2)
        //                {
        //                    #region "Print Form"
        //                    gloCMSEDI.UB04 oUB04 = new gloCMSEDI.UB04(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //                    oUB04.FillUB04Form(Convert.ToInt64(dtClaimID.Rows[i]["TransactionID"]), Convert.ToInt64(dtClaimID.Rows[i]["MasterTransactionID"]));
        //                    if ((PrintType)type == PrintType.PrintData)
        //                    {
        //                        if (_BillingTypeId == 2)
        //                        {
        //                            oUB04.PrintData("Claimservice");
        //                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (_BillingTypeId == 2)
        //                        {
        //                            oUB04.PrintForm("Claimservice");
        //                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.Print, "Batch named-" + (dtClaimID.Rows[i]["BatchName"]) + ", Claim No-" + (dtClaimID.Rows[i]["Claim"]) + " Printed, [Billing Method: " + ((BatchBillingMethod)_nBillingMethodID).GetDescription() + "]", 0, Convert.ToInt64(dtClaimID.Rows[i]["BatchID"]), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
        //                        }
        //                    }

        //                    oUB04.Dispose();
        //                    oUB04 = null;

        //                    #endregion
        //                }
        //                UpdateProgressBarControl(true, Convert.ToString(dtClaimID.Rows[0]["BatchName"]), 0, i + 1);
        //                if (!Removethreadlist.Contains(Convert.ToString(dtClaimID.Rows[0]["BatchName"])))
        //                {
        //                    Removethreadlist.Add(Convert.ToString(dtClaimID.Rows[0]["BatchName"]));
        //                }
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please select Paper Claim to print.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
        //    }
        //    finally
        //    {
        //        for (int i = 0; i < Removethreadlist.Count; i++)
        //        {
        //            string Currentthread = Removethreadlist[i];
        //            if (Removethreadlist.Contains(Currentthread))
        //            {
        //                ThreadCount--;
        //                threadlist.Remove(Currentthread);
        //                Removethreadlist.Remove(Currentthread);
        //                DisposefrmgloPrintClaimsProgress(Currentthread);
        //            }
        //        }

        //        if (oDB != null)
        //        { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

        //        if (dtClaimID != null)
        //        { dtClaimID = null; }

        //        if (ThreadCount == 0)
        //        {
        //            thrdQueue = null;
        //            foreach (Form OpenForm in Application.OpenForms)
        //            {
        //                if (OpenForm.Name == "frmBillingBatch_New")
        //                {
        //                    frmBillingBatch_New oViewBilling = frmBillingBatch_New.GetInstance();
        //                    oViewBilling.Invoke(new Action(() => oViewBilling.FillC1AllBatch()));
        //                }
        //            }
        //        }
        //    }
        //}

        //#region "ProgressBar For Printing Multiple Batches"

        //public void DisposefrmgloPrintClaimsProgress(string sObjectName)
        //{
        //    try
        //    {
        //        var result = (gloPM.Forms.frmgloPrintClaimsProgress)oListobjfrmgloPrintClaimsProgress.Where(obj => obj.Name == sObjectName).FirstOrDefault();
        //        if (result != null)
        //        {
        //            if (result.InvokeRequired)
        //            {
        //                result.Invoke(new DisposefrmgloPrintClaimsProgressV2(DisposefrmgloPrintClaimsProgress_Final), sObjectName);
        //            }
        //            else
        //            {
        //                DisposefrmgloPrintClaimsProgress_Final(sObjectName);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
        //    }
        //}

        //public delegate void DisposefrmgloPrintClaimsProgressV2(string sObjectName);
        //private void DisposefrmgloPrintClaimsProgress_Final(string sObjectName)
        //{
        //    try
        //    {
        //        gloPM.Forms.frmgloPrintClaimsProgress objStored = (gloPM.Forms.frmgloPrintClaimsProgress)oListobjfrmgloPrintClaimsProgress.Where(obj => obj.Name == sObjectName).FirstOrDefault();
        //        oListobjfrmgloPrintClaimsProgress.Remove(objStored);
        //        if (objStored != null)
        //        {
        //            objStored.Dispose();
        //            objStored = null;
        //        }
        //        if (oListobjfrmgloPrintClaimsProgress != null && oListobjfrmgloPrintClaimsProgress.Count <= 0)
        //        {
        //            if (objfrmgloPrintClaimsProgress != null)
        //            {
        //                objfrmgloPrintClaimsProgress.Dispose();
        //                objfrmgloPrintClaimsProgress = null;
        //            }
        //        }
        //        else
        //        {
        //            if (objfrmgloPrintClaimsProgress != null)
        //            {
        //                objfrmgloPrintClaimsProgress.Dispose();
        //                objfrmgloPrintClaimsProgress = null;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
        //    }

        //    //if (oListobjfrmgloPrintClaimsProgress != null)
        //    //{
        //    //    if (oListobjfrmgloPrintClaimsProgress.Count == 0)
        //    //    {
        //    //        oListobjfrmgloPrintClaimsProgress = null;
        //    //    }
        //    //}
        //}

        //public void UpdateProgressBarControl(bool InvokeRequired, string sBatchName = "", Int32 nMaxClaimCount = 0, int increamentvalue = 0, gloPM.Forms.frmgloPrintClaimsProgress objfrmgloPrintClaimsProgress = null)
        //{
        //    try
        //    {
        //        if (oListobjfrmgloPrintClaimsProgress != null)
        //        {
        //            if (!InvokeRequired)
        //            {
        //                var result = oListobjfrmgloPrintClaimsProgress.Any(o => o.Name == objfrmgloPrintClaimsProgress.Name);
        //                if (!result)
        //                {
        //                    objfrmgloPrintClaimsProgress.Show(this);
        //                    objfrmgloPrintClaimsProgress.BringToFront();
        //                    oListobjfrmgloPrintClaimsProgress.Add(objfrmgloPrintClaimsProgress);
        //                }
        //            }
        //            else
        //            {
        //                if (InvokeRequired)
        //                {
        //                    var result = (gloPM.Forms.frmgloPrintClaimsProgress)oListobjfrmgloPrintClaimsProgress.Where(obj => obj.Name == sBatchName).FirstOrDefault();
        //                    if (result != null)
        //                    {
        //                        result.InvokeProgressUpdateControls(increamentvalue);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
        //    }
        //}

        //#endregion

        #endregion

        public bool PendingMessageQueue()
        {
            if (thrdQueue == null)
                return true;
            else
            {
                MessageBox.Show("Background file generation from 'Claim Manager' is in progress. You cannot logout until this process is completed. Please try after some time.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;                
            }
        }

        #endregion        

        #region "Play/Pause and Stop of ProgressBar for ClaimManager" 

        public void PausePrinting()
        {
            if (thrdQueue != null)
            {
                bIsPause = true;
            }
        }

        private void PlayPrinting()
        {
            if (thrdQueue != null)
            {
                bIsPause = false;
                if (bIsPause == false) 
                {
                    this.frmBillingBatch_Print(otype, odtClaimid);
                }
            }
        }

        private void ClosePrinting() 
        {
            if (thrdQueue != null)
            {
                bIsPause = true;
                DisposefrmgloPrintClaimsProgress();
                thrdQueue = null;
                foreach (Form OpenForm in Application.OpenForms)
                {
                    if (OpenForm.Name == "frmBillingBatch_New")
                    {
                        this.Cursor = Cursors.WaitCursor;
                        Thread.Sleep(5000);
                        frmBillingBatch_New oViewBilling = frmBillingBatch_New.GetInstance();
                        oViewBilling.Invoke(new Action(() => oViewBilling.FillC1AllBatch()));
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
                
        #endregion
        #endregion

        private void mnuCopayDist_ByAccount_Click(object sender, EventArgs e)
        {
            try
            {
                gloBilling.frmCopayDistributionList ofrmCopayDistributionList = gloBilling.frmCopayDistributionList.GetInstance();
               // frmCopayDistributionList ofrmCopayDistributionList = new frmCopayDistributionList();
                if (ofrmCopayDistributionList != null)
                {
                    ofrmCopayDistributionList.WindowState = FormWindowState.Maximized;
                    ofrmCopayDistributionList.OnCloseCopayDistribution -= new frmCopayDistributionList.CloseCopayDistribution(CopayDistribution_OnCloseCopayDistribution);
                    ofrmCopayDistributionList.OnCloseCopayDistribution += new frmCopayDistributionList.CloseCopayDistribution(CopayDistribution_OnCloseCopayDistribution);
                    ofrmCopayDistributionList.MdiParent = this;
                    ofrmCopayDistributionList.IsCallingFromByAcc = true;
                    ofrmCopayDistributionList.Show();
                    ShowHideMainMenu(false, false);
                   mnuCopayDist_ByCharge.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuCopayDist_ByCharge_Click(object sender, EventArgs e)
        {
            try
            {
                gloBilling.frmCopayDistributionList ofrmCopayDistributionList = gloBilling.frmCopayDistributionList.GetInstance();
                 //ofrmCopayDistributionList = new frmCopayDistributionList();
                if (ofrmCopayDistributionList != null)
                {
                    ofrmCopayDistributionList.WindowState = FormWindowState.Maximized;
                    ofrmCopayDistributionList.OnCloseCopayDistribution -= new frmCopayDistributionList.CloseCopayDistribution(CopayDistribution_OnCloseCopayDistribution);
                    ofrmCopayDistributionList.OnCloseCopayDistribution += new frmCopayDistributionList.CloseCopayDistribution(CopayDistribution_OnCloseCopayDistribution);
                    ofrmCopayDistributionList.MdiParent = this;
                    ofrmCopayDistributionList.IsCallingFromByAcc = false;
                    ofrmCopayDistributionList.Show();
                    ShowHideMainMenu(false, false);
                    mnuCopayDist_ByAccount.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CopayDistribution_OnCloseCopayDistribution()
        {
            mnuCopayDist_ByAccount.Enabled = true;
            mnuCopayDist_ByCharge.Enabled = true;
        }

        private void tsb_ClearGagePayment_Click(object sender, EventArgs e)
        {
            frmClearGagePaymentPosting ofrmClearGagePaymentPosting = frmClearGagePaymentPosting.GetInstance();
            ofrmClearGagePaymentPosting.WindowState = FormWindowState.Maximized;
            ofrmClearGagePaymentPosting.MdiParent = this;
            ofrmClearGagePaymentPosting.Show();
            ShowHideMainMenu(false, false);
        }

        private void mnuView_CleargageFileHistory_Click(object sender, EventArgs e)
        {
            try
            {
                frmCleargageFileHistory ofrmCleargageFileHistory = frmCleargageFileHistory.GetInstance();
                ofrmCleargageFileHistory.WindowState = FormWindowState.Maximized;
                ofrmCleargageFileHistory.MdiParent = this;
                ofrmCleargageFileHistory.Show();
                ShowHideMainMenu(false, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void ShowHideCleargageFileHistory()
        {
            gloSettings.GeneralSettings _oSettings = null;
            object _obj = null;

            try
            {
                _oSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                _oSettings.GetSetting("EnableCleargageFeature", 0, 1, out _obj);
                {
                    if (_obj != null && Convert.ToString(_obj).Trim().Length > 0)
                    {
                        mnuView_CleargageFileHistory.Visible = Convert.ToBoolean(_obj);
                    }
                }
                _oSettings = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Error while showing menu for CleargageFileHistory :" + ex.ToString(), true);
                ex = null;
            }
            finally
            {
                _oSettings = null;
                _obj = null;
            }
        }
        
    }

}
