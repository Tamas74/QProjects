using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using gloUserControlLibrary;
using gloEmdeonInterface.Classes;
namespace gloEmdeonInterface.Forms
{

    public partial class frmSpirometryTestsNew : Form
    {

        #region "Declartion"

        public delegate void ViewVitalForm(Int64 nVitalID);
        public event ViewVitalForm EvntViewVitalFormHandler;
        private string _gstrMessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloListControl.gloListControl oListControl=null;
        private string gloConnectionString = string.Empty;
        private string SpiroConnectionString = string.Empty;
        private long _nPatinet_ID;
        private Int32 _nMappedSpiroRaceCode = 0;
        private long _nVitalID = 0;
        private long _nVisitID = 0;
        private long _nTechnicianID = 0;
        private bool _LunchTest = false;
        private Int64 _Contactid = 0;
        private string strOrderedtype = String.Empty;
       // private bool _Ismodify = false; 
        private bool _IsLoading = false;
        private Int32 _nPatientAge = 0;
        //private Int32 _nAge;
        #endregion

        #region "Property"
        
        public string OrderedByType
        {
            get
            {
                return strOrderedtype;
            }
            set
            {
                strOrderedtype = value;
            }
        }

        public Int32 MappedSpiroRaceCode
        {
            get
            {
                return _nMappedSpiroRaceCode;
            }
            set
            {
                _nMappedSpiroRaceCode = value;
            }
        }

        public Int64 OrderByID
        {
            get
            {
                Int64 _OrderByID = 0;
                Int64.TryParse(Convert.ToString(txtOrederd_by.Tag), out _OrderByID);
                return _OrderByID;
            }

        }

        public string OrderByName
        {
            get
            {
                return txtOrederd_by.Text;
            }
        }

        public Int64 TechnicianID
        {
            get
            {
                return _nTechnicianID;
            }

        }

        public Int32 NonSmoker
        {
            get
            {
                if (optNonSmoker.Checked)
                    return 1;
                else
                    return 0;
            }
        }

        public Int32 Smoker
        {
            get
            {
                if (optSmoker.Checked)
                    return 1;
                else
                    return 0;
            }
        }

        public Int32 QuitSmoking
        {
            get
            {
                if (ChkQuit.Checked)
                    return 1;
                else
                    return 0;
            }
        }

        public Int32 CigsDay
        {
            get
            {
                Int32 _CigsDay = 0;
                Int32.TryParse(txtCigsDay.Text, out _CigsDay);
                return _CigsDay;
            }

        }

        public Int32 SmokerForYears
        {
            get
            {
                Int32 _ForYears = 0;
                Int32.TryParse(txtForYears.Text, out _ForYears);
                return _ForYears;
            }

        }

        public Int32 QuitYearsAgo
        {
            get
            {
                Int32 _QuitYearsAgo = 0;
                Int32.TryParse(txtQuitYearAgo.Text, out _QuitYearsAgo);
                return _QuitYearsAgo;
            }

        }

        public Double PatientHeight
        {
            get
            {
                Double _PatientHeight = 00.00;
                Double.TryParse(Convert.ToString(txtPAHeight.Tag), out _PatientHeight);
                return _PatientHeight;
            }

        }

        public Double PatientWeight
        {
            get
            {
                Double _PatientWeight = 0.0;
                Double.TryParse(Convert.ToString(txtPAWeight.Tag), out _PatientWeight);
                return _PatientWeight;

            }

        }

        public long VisitID
        {
            get
            {
                return _nVisitID;
            }
          
        }


        public bool LunchTest
        {
            get
            {
                return _LunchTest;
            }

        }

        //public Int32  PatientAge
        //{
        //    get
        //    {
        //        return _nAge;
        //    }
        //    set
        //    {

        //        DateTime PatientDateOFBirth;
        //        if (DateTime.TryParse(Convert.ToString(value), out PatientDateOFBirth) == true)
        //        {
        //           DateTime Result=  DateTime.Now.Year  - PatientDateOFBirth.Year ;

        //        }
        //        else
        //        {
        //            _nAge = 0;
        //        }

        //    }
        //}

        #endregion

        #region "Constructor"

        public frmSpirometryTestsNew(long Pationt_ID,Int32 Age, long EMRRaceID, string EMRRaceName, string GloEMRConnectionString, string SpirometryConnectionstring)
        {

            InitializeComponent();
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"].Length > 0)
                {
                    _gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { _gstrMessageBoxCaption = "gloEMR"; }

            //if (appSettings["ClinicID"] != null)
            //{
            //    if (appSettings["ClinicID"].Length > 0)
            //    {
            //        _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //    }
            //    else
            //    {
            //        _ClinicID = 1;
            //    }
            //}
            //else
            //{ _ClinicID = 1; }
            // set form varriable from constructor varriable
            _nPatinet_ID = Pationt_ID;
            _nPatientAge = Age;
            cmbPARace.Tag = EMRRaceID;
            cmbPARace.Text = EMRRaceName;
            SpiroConnectionString = SpirometryConnectionstring;
            gloConnectionString = GloEMRConnectionString;

            //get login user id from app settigs
            _nTechnicianID = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString());
            // get login user name from app settings
            LblTechnician.Text = System.Configuration.ConfigurationManager.AppSettings["UserName"].ToString();
        }

        #endregion

        //public delegate void a();
        public delegate void showAddVitals(object o, EventArgs e);


        #region "Event"

        private void frmSpiroTest_New_Load(object sender, EventArgs e)
        {

            try
            {

                try
                {
                    gloPatient.gloPatient.GetWindowTitle(this, _nPatinet_ID, gloConnectionString, _gstrMessageBoxCaption);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                _IsLoading = true;

                // set race information
                SetRaceInfo();
                // set vital information
                SetVitalInfo();

                //Set Smoking history if available
                SetSmokingHistory();
                //If patient is non smoker clear all information
                if (optNonSmoker.Checked)
                {
                    optSmoker.Checked = false;
                    ChkQuit.Checked = false;
                    txtCigsDay.Text = String.Empty;
                    txtForYears.Text = String.Empty;
                    txtQuitYearAgo.Text = String.Empty;
                    txtCigsDay.Enabled = false;
                    txtForYears.Enabled = false;
                    ChkQuit.Enabled = false;
                    txtQuitYearAgo.Enabled = false;
                }
                //If race is not available for patient.. disable configure race button 
                if (cmbPARace.Text.Trim().Length == 0)
                { btnConfigureRace.Enabled = false; }
                SetDefaultProvider();
            }
            finally
            {
                _IsLoading = false;
            }
        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (Convert.ToString(e.ClickedItem.Tag).ToLower())
            {
                case "launchtest":
                    {
                        if (txtOrederd_by.Text.Trim().Length == 0)
                        {
                            MessageBox.Show("Enter ordering provider.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnOrederBy.Focus();
                            return;
                        }

                        else if (cmbPARace.Text.Trim().Length <= 0)
                        {
                            MessageBox.Show("Enter Race for patient.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnConfigureRace.Focus();
                            return;
                        }
                        // else if (cmbPARace.BackColor == Color.Red)
                        else if (LblRaceErrorMsg.Text.Trim().Length > 0)
                        {
                            // if (!(cmbPARace.Text.Trim().Length > 0))
                            //{
                            MessageBox.Show("Race is not configured with device race list, Click to configure race.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnConfigureRace.Focus();
                            return;
                            // }
                            //else
                            //    MessageBox.Show("Race is not prenet please enter race for patient", _gstrMessageBoxCaption, MessageBoxButtons.OK);
                            
                        }
                        else if (txtPAHeight.Text.Trim().Length <= 0)
                        {
                            MessageBox.Show("Enter Height for patient.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnGenrateVisit.Focus();
                            return;
                        }
                        else if (txtPAWeight.Text.Trim().Length <= 0)
                        {
                            MessageBox.Show("Enter Weight for patient.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnGenrateVisit.Focus();
                            return;
                        }
                        else if (optSmoker.Checked)
                        {
                            if (txtCigsDay.Text.Length == 0)
                            {
                                MessageBox.Show("Enter no of cigars per day.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCigsDay.Focus();
                                return;
                            }
                            else if (Convert.ToInt32(txtCigsDay.Text.Trim()) == 0)
                            {
                                MessageBox.Show("Number of cigars per day should be greater than zero.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtCigsDay.Focus();
                                return;
                            }
                            else if (txtForYears.Text.Length == 0)
                            {
                                MessageBox.Show("Enter no of smoking years.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtForYears.Focus();
                                return;
                            }
                            else if (Convert.ToInt32(txtForYears.Text.Trim()) == 0)
                            {
                                MessageBox.Show("Number of smoking years should be greater than zero.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtForYears.Focus();
                                return;
                            }
                            else if (_nPatientAge > 0)
                            {
                                if (Convert.ToInt32(txtForYears.Text.Trim()) >= _nPatientAge)
                                {
                                    MessageBox.Show("Number of smoking years should be less than age", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtForYears.Focus();
                                    return;

                                }
                            }
                         }
                         if (ChkQuit.Checked)
                         {
                                if (txtQuitYearAgo.Text.Length == 0)
                                {
                                    MessageBox.Show("Enter no of quit smoking years.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtQuitYearAgo.Focus();
                                    return;
                                }
                                else if (Convert.ToInt32(txtQuitYearAgo.Text.Trim()) == 0)
                                {
                                    MessageBox.Show("Number of years quit smoking should be greater than zero.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtQuitYearAgo.Focus();
                                    return;
                                }
                                else if (Convert.ToInt32(txtQuitYearAgo.Text.Trim()) > Convert.ToInt32(txtForYears.Text.Trim()))
                                {
                                    MessageBox.Show("Quit years should be less than smoking year.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtQuitYearAgo.Focus();
                                    return;
                                }

                            }
                        //}
                        //else if (ChkQuit.Checked)
                        //{
                        //    if (txtQuitYearAgo.Text.Length == 0)
                        //    {
                        //        MessageBox.Show("Number of years patient quit smoking can not be blank.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        txtQuitYearAgo.Focus();
                        //        return;
                        //    }
                        //    else if (Convert.ToInt32(txtQuitYearAgo.Text.Trim()) == 0)
                        //    {
                        //        MessageBox.Show("Number of years patient quit smoking should be greater than zero.", _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        txtQuitYearAgo.Focus();
                        //        return;
                        //    }
                        //}

                        if (optProvider.Checked)
                        {
                            this.OrderedByType = "Provider";
                        }
                        else if (OptRefferal.Checked)
                        {
                            this.OrderedByType = "Refferal";
                        }

                         _LunchTest = true;
                         this.Close();
                         break;
                    }
                case "close":
                    {
                        _LunchTest = false;
                        this.Close();
                        break;
                    }
            }
        }

        private void btnConfigureRace_Click(object sender, EventArgs e)
        {
            // show race Mapping form
            frmSpirometryRaceMappingsConfiguration frmobj = new frmSpirometryRaceMappingsConfiguration(SpiroConnectionString, gloConnectionString);
            frmobj.ShowDialog(this);
            // set race information
            SetRaceInfo();
            frmobj.Dispose();
            frmobj = null;
        }

        private void oListControl_SelectedClick(object sender, EventArgs e)
        {
            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        txtOrederd_by.Tag = Convert.ToString(oListControl.SelectedItems[i].ID);
                        txtOrederd_by.Text = oListControl.SelectedItems[i].Description;
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            panel5.Visible = true;
            this.Width = this.Width - 500;
            this.Height = this.Height - 200;
            int x = Screen.PrimaryScreen.Bounds.Width - this.Width;
            int y = Screen.PrimaryScreen.Bounds.Height - this.Height;
            this.Location = new Point(x / 2, y / 2);
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyClick);
                    oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddClick);

                }
                catch { }
            }
            panel5.Visible = true;
            this.Width = this.Width - 500;
            this.Height = this.Height - 200;
            int x = Screen.PrimaryScreen.Bounds.Width - this.Width;
            int y = Screen.PrimaryScreen.Bounds.Height - this.Height;
            this.Location = new Point(x / 2, y / 2);
        }

        private void oListControl_ModifyClick(object sender, EventArgs e)
        {
            if (oListControl.ControlHeader == "Physicians")
            {
                if (oListControl.dgListView.CurrentRow != null)
                {
                    _Contactid = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                }
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    gloContacts.frmSetupPhysician ofrmModifyContact = new gloContacts.frmSetupPhysician(_Contactid, gloConnectionString);
                    ofrmModifyContact.ShowDialog(this);

                    if (ofrmModifyContact.DialogResult == DialogResult.OK)
                    {
                        //_Ismodify = true;
                        oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);

                        // oListControl.FillListAsCriteria1(ofrmModifyContact.ContactID, true);

                    }
                    ofrmModifyContact.Dispose();
                    ofrmModifyContact = null;
                }

            }
        }

        private void oListControl_AddClick(object sender, EventArgs e)
        {
            gloContacts.frmSetupPhysician objfrmSetupPhysician = new gloContacts.frmSetupPhysician(gloConnectionString);
            objfrmSetupPhysician.ShowDialog(this);
            if (objfrmSetupPhysician.DialogResult == DialogResult.OK)
            {
                oListControl.FillListAsCriteria(objfrmSetupPhysician.ContactID);
            }
            objfrmSetupPhysician.Dispose();
            objfrmSetupPhysician = null;
        }

        private void btnOrederBy_Click(object sender, EventArgs e)
        {
            //If refferal option is selected open referrals window
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyClick);
                    oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddClick);

                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }
            if (OptRefferal.Checked)
            {
                oListControl = new gloListControl.gloListControl(gloConnectionString, gloListControl.gloListControlType.Physicians, false, this.Width);
                oListControl.ControlHeader = "Physicians";

            }
            //if Provider option is selected open provider window
            else if (optProvider.Checked)
            {
                oListControl = new gloListControl.gloListControl(gloConnectionString, gloListControl.gloListControlType.Providers, false, this.Width);
                oListControl.ControlHeader = "Providers";
            }
            //Add the events for the control
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyClick);
            oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddClick);

            this.Controls.Add(oListControl);
            this.Width = this.Width + 500;
            this.Height = this.Height + 200;
            // this.Location = new Point(this.Location.X - 300, this.Location.Y);
            //this.DesktopLocation=
            oListControl.Width = this.Width;
            oListControl.Height = this.Height;
            oListControl.Dock = DockStyle.Fill;
            panel5.Visible = false;
            oListControl.OpenControl();
            oListControl.BringToFront();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            int x = Screen.PrimaryScreen.Bounds.Width - this.Width;
            int y = Screen.PrimaryScreen.Bounds.Height - this.Height;
            this.Location = new Point(x / 2, y / 2);
            if (optProvider.Checked)
            {
                this.OrderedByType = "Provider";
            }
            else if (OptRefferal.Checked)
            {
                this.OrderedByType = "Refferal";
            }


        }

        private void BtnRemoveOrderBy_Click(object sender, EventArgs e)
        {
            txtOrederd_by.Tag = null;
            txtOrederd_by.Text = string.Empty;
        }

        private void optNonSmoker_CheckedChanged(object sender, EventArgs e)
        {
            //if (optNonSmoker.Focused)
            //{
                if (optNonSmoker.Checked)
                {
                    optSmoker.Checked = false;
                    ChkQuit.Checked = false;
                    txtCigsDay.Text = string.Empty;
                    txtForYears.Text = string.Empty;
                    txtQuitYearAgo.Text = string.Empty;
                    txtCigsDay.Enabled = false;
                    txtForYears.Enabled = false;
                    ChkQuit.Enabled = false;
                    txtQuitYearAgo.Enabled = false;
                }
            //}

        }

        private void optSmoker_CheckedChanged(object sender, EventArgs e)
        {
            //if (optSmoker.Focused)
            //{
                if (optSmoker.Checked)
                {
                    optNonSmoker.Checked = false;
                    txtCigsDay.Enabled = true;
                    txtForYears.Enabled = true;
                    ChkQuit.Enabled = true;

                }
                else
                {
                    txtCigsDay.Text = string.Empty;
                    txtCigsDay.Enabled = false;
                    txtForYears.Text = string.Empty;
                    txtForYears.Enabled = false;
                    ChkQuit.Checked = false;
                    ChkQuit.Enabled = false;
                    txtQuitYearAgo.Text = string.Empty;
                    txtQuitYearAgo.Enabled = false;
                }

           // }
        }

        private void ChkQuit_CheckedChanged(object sender, EventArgs e)
        {
            //if (ChkQuit.Focused)
            //{
                if (ChkQuit.Checked)
                {
                    txtQuitYearAgo.Enabled = true;
                }
                else
                {
                    txtQuitYearAgo.Text = "";
                    txtQuitYearAgo.Enabled = false;
                }
           // }
        }

        private void btnGenrateVisit_Click(object sender, EventArgs e)
        {
            if ((txtPAHeight.Text.Length > 0) || (txtPAWeight.Text.Length > 0))
            {
                //if (MessageBox.Show("Do you want add new vitals record? ", _gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    EvntViewVitalFormHandler(0);
                //}
                //else
                //{
                //    EvntViewVitalFormHandler(_nVitalID);
                //}
                DialogResult objDialogueres = MessageBox.Show("Do you want to add new vitals record?\n\n YES - To add new vitals to patient.\n\n NO - To update existing vitals of patient.", _gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (objDialogueres)
                {
                    case DialogResult.Yes:
                        EvntViewVitalFormHandler(0);
                        break;
                    case DialogResult.No:
                        EvntViewVitalFormHandler(_nVitalID);
                        break;
                    case DialogResult.Cancel:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                EvntViewVitalFormHandler(0);
            }

            // SaveVitalInformation();
            SetVitalInfo();
        }

        #endregion


        #region "Function And Methods"

        // funcation to set Race
        private void SetRaceInfo()
        {
             //LblRaceErrorMsg.Text = string.Empty;
            //// check GloEMRReace is available for this patient Or NOT
            //long EMRRaceID = 0;
            //Int64.TryParse(cmbPARace.Tag.ToString(), out EMRRaceID);
            //if (EMRRaceID == 0 && cmbPARace.Text == string.Empty)
            //{
            //    cmbPARace.BackColor = Color.Red;
            //    LblRaceErrorMsg.Text = "Race is not present";
            //}
            //else
            //{
            //    // if Race is available in Gloemr check it is Mapped or not
            //    clsCategoryMST objCatMst = new clsCategoryMST(SpiroConnectionString);
            //    if (Int32.TryParse(objCatMst.GetMappedSpiroRaceID(EMRRaceID), out  _nMappedSpiroRaceCode) == false)
            //    {
            //        cmbPARace.BackColor = Color.Red;
            //        LblRaceErrorMsg.Text = "Race is not configured with device race list, Click to map race.";
            //    }
            //    else
            //    {
            //        cmbPARace.BackColor = Color.White;
            //        LblRaceErrorMsg.Text = "";
            //    }

            //}


            LblRaceErrorMsg.Text = string.Empty;
            long EMRRaceID = 0;
            clsCategoryMST objCatMst = null;
            try
            {
                //check is race present for patient
                Int64.TryParse(cmbPARace.Tag.ToString(), out EMRRaceID);
                if (EMRRaceID != 0 && cmbPARace.Text.Trim().Length > 0)
                {
                    _nMappedSpiroRaceCode = 0;
                    objCatMst = new clsCategoryMST(SpiroConnectionString);
                    if (Int32.TryParse(objCatMst.GetMappedSpiroRaceID(EMRRaceID), out  _nMappedSpiroRaceCode) == false)
                    {
                        cmbPARace.BackColor = Color.Red;
                        LblRaceErrorMsg.Text = "Race is not configured with device race list, Click to configure race.";
                        btnConfigureRace.Enabled = true;
                    }
                    else
                    {
                        cmbPARace.BackColor = Color.White;
                        LblRaceErrorMsg.Text = "";
                        btnConfigureRace.Enabled = false;
                    }

                }
                else
                {
                    cmbPARace.Text = string.Empty;
                    cmbPARace.BackColor = Color.Red;
                    LblRaceErrorMsg.Text = "Race is not present";
                    btnConfigureRace.Enabled = false; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryTestsNew.SetRaceInfo() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
           finally
            {
                if (objCatMst != null)
                {
                    objCatMst.Dispose();
                    objCatMst = null;
                }
            }
        }

        // funcation to set vital information
        private void SetVitalInfo()
        {
            //lblVitalinformation.Text = string.Empty;
            //gloEmdeonInterface.Classes.clsSpiroVitalsManagement objSpirovitals = new gloEmdeonInterface.Classes.clsSpiroVitalsManagement();
            //// retrive height and weight 
            //DataTable dtgetdata = objSpirovitals.RetriveHeightWeight(gloConnectionString, _nPatinet_ID);

            //if ((dtgetdata != null) && (dtgetdata.Rows.Count > 0))
            //{
            //        long.TryParse(dtgetdata.Rows[0]["nVitalID"].ToString(), out _nVitalID);
            //        //   txtPAHeight.Text = dtgetdata.Rows[0]["sHeight"].ToString();
            //        txtPAHeight.Text = Convert.ToString(dtgetdata.Rows[0]["dHeightinCm"]);
            //        txtPAHeight.Tag = Convert.ToString(dtgetdata.Rows[0]["dHeightinCm"]);
            //        txtPAWeight.Text = Convert.ToString(dtgetdata.Rows[0]["dWeightinlbs"]);
            //        txtPAWeight.Tag = Convert.ToString(dtgetdata.Rows[0]["dWeightinKg"]);
            //        txtPAHeight.BackColor = Color.White;
            //        txtPAWeight.BackColor = Color.White;
            //        //btnGenrateVisit.Enabled = false;
                
                
            //}
            //else
            //{
            //    lblVitalinformation.Text = "Vital information not available, Click to add vitals.";
            //    txtPAHeight.BackColor = Color.Red;
            //    txtPAWeight.BackColor = Color.Red;
            //    _nVitalID = 0;
            //    txtPAHeight.Text = string.Empty;
            //    txtPAWeight.Text = string.Empty;
            //    btnGenrateVisit.Enabled = true;
            //}

            lblVitalinformation.Text = string.Empty;
            clsSpiroVitalsManagement objSpirovitals = null;
            DataTable dtgetdata = null;
            try
            {

                objSpirovitals = new gloEmdeonInterface.Classes.clsSpiroVitalsManagement();
                dtgetdata = objSpirovitals.RetriveHeightWeight(gloConnectionString, _nPatinet_ID);
                if ((dtgetdata != null) && (dtgetdata.Rows.Count > 0))
                {
                    _nVitalID = 0;
                    _nVisitID = 0;
                    long.TryParse(dtgetdata.Rows[0]["nVitalID"].ToString(), out _nVitalID);
                    long.TryParse(dtgetdata.Rows[0]["nVisitID"].ToString(), out _nVisitID);
                    if (_nVitalID != 0 && _nVisitID!=0)
                    {
                        if (Convert.ToString(dtgetdata.Rows[0]["dHeightinCm"]).Trim().Length > 0)
                        {
                            txtPAHeight.Text = Convert.ToString(dtgetdata.Rows[0]["dHeightinCm"]);
                            txtPAHeight.Tag = Convert.ToString(dtgetdata.Rows[0]["dHeightinCm"]);
                            txtPAHeight.BackColor = Color.White;
                        }
                        else
                        {
                            txtPAHeight.Text = string.Empty;
                            txtPAHeight.Tag = string.Empty;
                            lblVitalinformation.Text = "Vital information not available, Click to add vitals.";
                            txtPAHeight.BackColor = Color.Red;

                        }
                        if (Convert.ToString(dtgetdata.Rows[0]["dWeightinlbs"]).Trim().Length > 0)
                        {
                            txtPAWeight.Text = Convert.ToString(dtgetdata.Rows[0]["dWeightinlbs"]);
                            txtPAWeight.Tag = Convert.ToString(dtgetdata.Rows[0]["dWeightinKg"]);
                            txtPAWeight.BackColor = Color.White;
                        }
                        else
                        {
                            txtPAWeight.Text = string.Empty;
                            lblVitalinformation.Text = "Vital information not available, Click to add vitals.";
                            txtPAWeight.BackColor = Color.Red;
                        }

                    }// end _nVitalID != 0
                    else
                    {
                        _nVisitID = 0;
                        _nVitalID = 0;
                        lblVitalinformation.Text = "Vital information not available, Click to add vitals.";
                        txtPAHeight.Text = string.Empty;
                        txtPAHeight.Tag = string.Empty;
                        txtPAWeight.Text = string.Empty;
                        txtPAHeight.BackColor = Color.Red;
                        txtPAWeight.BackColor = Color.White;

                    }


                }// end dtgetdata.Rows.Count
                else
                {
                    lblVitalinformation.Text = "Vital information not available, Click to add vitals.";
                    txtPAHeight.BackColor = Color.Red;
                    txtPAWeight.BackColor = Color.Red;
                    _nVitalID = 0;
                    txtPAHeight.Text = string.Empty;
                    txtPAWeight.Text = string.Empty;
                    btnGenrateVisit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryTestsNew.SetVitalInfo() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (dtgetdata != null)
                {
                    dtgetdata.Dispose();
                    dtgetdata = null;
                }
                if (objSpirovitals != null)
                {
                    objSpirovitals.Dispose();
                    objSpirovitals = null;
                }

            }
        }

        private void SetSmokingHistory()
        {
            clsSpiroReportmanager objclsSpiromanager = null;
            bool isSmoker = false;
            int intnoOfCigars = 0;
            int intnoOfSmokingYears = 0;
            bool isQuitSmoking = false;
            int intnoOfQuitYears = 0;
            try
            {
                objclsSpiromanager = new clsSpiroReportmanager(SpiroConnectionString);
                objclsSpiromanager.GetPatientSmokingHistory(_nPatinet_ID, ref isSmoker, ref intnoOfCigars, ref intnoOfSmokingYears, ref isQuitSmoking, ref intnoOfQuitYears);
                optSmoker.Checked = isSmoker;
                txtCigsDay.Text = Convert.ToString(intnoOfCigars);
                txtForYears.Text = Convert.ToString(intnoOfSmokingYears);
                ChkQuit.Checked = isQuitSmoking;
                txtQuitYearAgo.Text = Convert.ToString(intnoOfQuitYears);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in frmSpirometryTestsNew.SetSmokingHistory() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (objclsSpiromanager != null)
                {
                    objclsSpiromanager.Dispose();
                    objclsSpiromanager = null;
                }
            }
        }

        public void SetDefaultProvider()
        {
            long nProviderId = 0;
            txtOrederd_by.Text = clsSpiroGeneralModule.GetProviderDetails(_nPatinet_ID, ref nProviderId, gloConnectionString);
            txtOrederd_by.Tag = Convert.ToString(nProviderId);
        }

        //funcation retrive heoght and wegt from viyal table


        //functiopn to get mapped race code


        // method to convert height and weght
        //private void SaveVitalInformation()
        //{

        //    //if (txtPAHeight.Text.Trim() != "" || txtPAHeight.Text != "")
        //    //{
        //    //    // get hight in cm,ft,inches
        //    //    txtPAHeight.Text = txtPAHeight.Text.Replace("-", "");
        //    //    txtPAHeight.Text = txtPAHeight.Text.Replace("&", "").Replace("$", "");
        //    //    double HtInCM = 0;
        //    //    double HtInInches = 0;
        //    //    double HtInch = 0;
        //    //    double HtInft = 0;
        //    //    double.TryParse(txtPAHeight.Text, out HtInCM);
        //    //    HtInInches = HtInCM * 0.3937008;
        //    //    HtInch = HtInInches % 12;
        //    //    HtInft = HtInInches / 12;
        //    //    txtPAWeight.Text = txtPAWeight.Text.Replace("-", "");
        //    //    txtPAWeight.Text = txtPAWeight.Text.Replace("&", "").Replace("$", "");
        //    //    double WtInkg = Convert.ToDouble(txtPAWeight.Text) / 2.2033;
        //    //    //long _visitID = getVisitID(DateTime.Now, _nPatinet_ID);
        //    //    long _visitID =gloEmdeonInterface.Classes.clsSpiroGeneralModule.getVisitID(DateTime.Now, _nPatinet_ID,gloConnectionString);
        //    //    //long _VItalID = GetUniqueueId();
        //    //    long _VItalID = 0;
        //    //    string[] hght = Convert.ToString(Math.Round(HtInft, 2)).Split('.');
        //    //    string _sHeight = "";
        //    //    if (hght.Length == 1)
        //    //    {
        //    //        _sHeight = hght[0].ToString() + "'";

        //    //    }
        //    //    else if (hght.Length == 2)
        //    //    {
        //    //        _sHeight = hght[0].ToString() + "'" + hght[1].ToString() + "''";
        //    //    }
        //    //    _sHeight = Convert.ToString(Math.Round(HtInft, 0)) + "'";
        //    //    string[] wght = txtPAWeight.Text.Split('.');
        //    //    string _weigtOz = "";
        //    //    if (wght.Length == 1)
        //    //    {
        //    //        _weigtOz = wght[0].ToString() + "lbs 0oz";
        //    //    }
        //    //    else if (wght.Length == 2)
        //    //    {
        //    //        _weigtOz = wght[0].ToString() + "lbs " + wght[1].ToString() + "oz";
        //    //    }
        //    //    clsSpiroVitalsManagement objVitalMgt = new clsSpiroVitalsManagement();

        //    //    if (objVitalMgt.AddVital(_visitID,ref _VItalID, _nPatinet_ID, DateTime.Now, _sHeight, Math.Round(Convert.ToDouble(txtPAWeight.Text), 2), Math.Round(WtInkg, 2), Math.Round(HtInInches), Math.Round(Convert.ToDouble(txtPAHeight.Text), 2), _weigtOz,gloConnectionString))
        //    //    {
        //           SetVitalInfo();
        //    //    }

        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Invalid Vital Information", "gloEMR", MessageBoxButtons.OK);
        //    //}
        //}

        // funnction to add height and weight in vital table


        // function to get unique id
        private long GetUniqueueId()
        {
            return gloEmdeonInterface.Classes.clsSpiroGeneralModule.GetSpUniqueID(gloConnectionString);
            //  throw(new Exception("Not Implemented Under construction");
        }

        #endregion

        private void txtCigsDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || (e.KeyChar == Convert.ToChar(8)))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtForYears_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || (e.KeyChar == Convert.ToChar(8)))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtQuitYearAgo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || (e.KeyChar == Convert.ToChar(8)))
            {
            }
            else
            {
                e.Handled = true;
            }
        }

        private void optProvider_Click(object sender, EventArgs e)
        {
            if (!_IsLoading)
            {
                if (optProvider.Checked)
                {
                    txtOrederd_by.Text = string.Empty;
                    txtOrederd_by.Tag = string.Empty;
                }
            }
        }

        private void OptRefferal_Click(object sender, EventArgs e)
        {
            if (OptRefferal.Checked)
            {
                txtOrederd_by.Text = string.Empty;
                txtOrederd_by.Tag = string.Empty;
            }
        }


        


    }
}
