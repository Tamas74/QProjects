using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace gloPMGeneral
{
    public partial class frmPriorAuthorization : Form
    {

        #region Declarations
        //Declarations
        private String _databaseConnectionString = "";
        //private String _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


//nAuthorizationID, nPatientID, nInsuranceID, sInsuranceName, dtAuthorization, dtAuthorizationThrough,
//sAuthorizationNumber, nTotalVisits, nVisitsMade, nAppointmentType, sAuthorizationStatus, dtAuthorizationStatus


        private const int  COL_nAuthorizationID = 0 ;
        private const int COL_nInsuranceID = 1;
        private const int COL_sInsuranceName = 2;
        private const int COL_dtAuthorization = 3;
        private const int COL_dtAuthorizationThrough = 4;
        private const int COL_sAuthorizationNumber = 5;
        private const int COL_nTotalVisits = 6;
        private const int COL_nVisitsMade = 7;
        private const int COL_nAppointmentType = 8;
        private const int COL_sAuthorizationStatus = 9;
        private const int COL_dtAuthorizationStatus = 10;



        #endregion Declarations

        #region "Property Procedures"
        //Property Procedures
            private Int64 _PatientID = 0;
            private String _InsuranceName = "";
            private Int64 _InsuranceId = 0;
           private Int64 _AuthorizationId = 0;
            private DateTime _AuthorizationDate;
            private DateTime _AuthorizationThroughDate;
            private String _AuthorizationNo = "";
            private Int64 _NumberOfVisits = 0;
            private Int64 _VisitsMade = 0;
            private Int64 _AppointmentType = 0;
             private Int64 _tempID = 0;
        private bool _CloseAfterSave = false;

            public Int64 PatientID
            {
                set { _PatientID = value; }
                get { return _PatientID; }
            }

            public String InsuranceName
            {
                set { _InsuranceName = value; }
                get { return _InsuranceName; }
            }

            public Int64 InsuranceID
            {
                set { _InsuranceId = value; }
                get { return _InsuranceId; }
            }

            public DateTime AuthorizationDate
            {
                get { return _AuthorizationDate; }
                set { _AuthorizationDate = value; }
            }

            public DateTime AuthorizationThroughDate
            {
                get { return _AuthorizationThroughDate; }
                set { _AuthorizationThroughDate = value; }
            }

            public String AuthorizationNumber
            {
                get { return _AuthorizationNo; }
                set { _AuthorizationNo = value; }
            }

            public Int64 NumberOfVisits
            {
                get { return _NumberOfVisits; }
                set { _NumberOfVisits = value; }
            }

            public Int64 VisitsMade
            {
                get { return _VisitsMade; }
                set { _VisitsMade = value; }
            }
            public Int64 AppointmentType
            {
                get { return _AppointmentType; }
                set { _AppointmentType = value; }
            }

            public bool CloseAfterSave
            {
                get { return _CloseAfterSave; }
                set { _CloseAfterSave = value; }
            }

        #endregion "Property Procedures"

        #region "Constructor"
           
         public frmPriorAuthorization()
        {
            InitializeComponent();
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

         public frmPriorAuthorization(Int64 PatientID, String InsuranceName, Int64 InsuranceId, String ConnectionString)
        {
            InitializeComponent();

            _PatientID = PatientID;
            _InsuranceName = InsuranceName;
            _InsuranceId = InsuranceId;

            _databaseConnectionString = ConnectionString;
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmPriorAuthorization(Int64 PatientID, String InsuranceName, Int64 InsuranceId, String ConnectionString, bool closeaftersave)
        {
            InitializeComponent();

            _PatientID = PatientID;
            _InsuranceName = InsuranceName;
            _InsuranceId = InsuranceId;

            _databaseConnectionString = ConnectionString;
            _CloseAfterSave = closeaftersave;
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

        }
        
        #endregion "Constructor"

        #region "Form Load and Fill Methods"

            private void frmPriorAuthorization_Load(object sender, EventArgs e)
        {


            gloC1FlexStyle.Style(c1PriorAuthorization, false);
            try
            {
                //Fill the appointment types in DropDown
                FillAppointmentTypes();
                
                DesignGrid();
                Show_PriorAuthorizations();

                //Fill the Authorization Status  in DropDown
                FillAuthorizationStatus();


                //Fill patient insurances
                FillPatientInsurance(_PatientID);
                
                //Get values for the Insurances from Authorization Table.
                //dtTemp = oPriorAutorization.GetPriorAuthorization(PatientID, InsuranceID, InsuranceName);

                //Fill retrieved values
                #region "GetLast Authorization"
                //if (dtTemp != null && dtTemp.Rows.Count > 0)
                //{
                //    txtTotalVisits.Enabled = false;    //Disable the Total Visits to be Made.

                //    //Show values
                //    // dtTemp.Rows[0][];
                //    //nAuthorizationID  
                //    // _InsuranceName ="";
                //    // _InsuranceId = 0;
                //    if (dtTemp.Rows[0]["dtAuthorization"].ToString().Trim() != "")
                //        dtAuthorizationDate.Value = Convert.ToDateTime(dtTemp.Rows[0]["dtAuthorization"]);
                //    if (dtTemp.Rows[0]["dtAuthorizationThrough"].ToString().Trim() != "")
                //        dtAuthorizationThroughDate.Value = Convert.ToDateTime(dtTemp.Rows[0]["dtAuthorizationThrough"]);
                //    if (dtTemp.Rows[0]["dtAuthorizationStatus"].ToString().Trim() != "")
                //        dtAuthorizationStatusDate.Value = Convert.ToDateTime(dtTemp.Rows[0]["dtAuthorizationStatus"]);
                //    cbAuthorizationStatus.SelectedValue = Convert.ToString(dtTemp.Rows[0]["sAuthorizationStatus"]);
                //    txtAuthorizationNumber.Text = Convert.ToString(dtTemp.Rows[0]["sAuthorizationNumber"]);
                //    txtTotalVisits.Text = Convert.ToString(dtTemp.Rows[0]["nTotalVisits"]);
                //    txtVisitsMade.Text = Convert.ToString(dtTemp.Rows[0]["nVisitsMade"]);
                //    if (dtTemp.Rows[0]["nAppointmentType"].ToString().Trim() != "")
                //        cmbAppointmentType.SelectedValue = Convert.ToInt64(dtTemp.Rows[0]["nAppointmentType"]);

                //}
                //else //No entry for the Insurance.
                //{
                //    txtTotalVisits.Enabled = true;

                //}


                ////BL for the Visits.
                //if (txtTotalVisits.Text.Trim() != "" && txtVisitsMade.Text.Trim() != "")
                //{
                //    if ((Convert.ToInt64(txtTotalVisits.Text.Trim()) - Convert.ToInt64(txtVisitsMade.Text.Trim())) > 0)
                //    {
                //        lblVisitsLeft.Text = Convert.ToString(Convert.ToInt64(txtTotalVisits.Text.Trim()) - Convert.ToInt64(txtVisitsMade.Text.Trim()));
                //    }
                //    else
                //    {
                //        lblVisitsLeft.Text = "0";
                //    }
                //} 
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ////dtTemp.Dispose();
                //oPriorAutorization.Dispose();
            }
        }

            private void FillAuthorizationStatus()
        {
            DataTable dtTempStatus= new DataTable();

            try
            {
                dtTempStatus.Columns.Add("sAuthorizationStatus");
                dtTempStatus.Columns.Add("sAuthorizationValue");

                dtTempStatus.Rows.Add("Approved", "Approved");
                dtTempStatus.Rows.Add("Denied", "Denied");
                dtTempStatus.Rows.Add("Suspended", "Suspended");
                dtTempStatus.Rows.Add("Pending Approved", "Pending Approved");
                dtTempStatus.Rows.Add("Expired", "Expired");

                cbAuthorizationStatus.DataSource = dtTempStatus;
                cbAuthorizationStatus.DisplayMember = "sAuthorizationStatus";
                cbAuthorizationStatus.ValueMember = "sAuthorizationValue";


            }
            catch (Exception ex)
            {
                if (dtTempStatus != null)
                {
                    dtTempStatus.Dispose();
                }
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //throw;
            }
          //  throw new Exception("The method or operation is not implemented.");
        }

            private void FillAppointmentTypes()
        {
            //fill appointments on formload.
           
            cmbAppointmentType.DataSource = null;
            cmbAppointmentType.Items.Clear();
            DataTable dtApptType = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);

            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("Select nAppointmentTypeID,isnull(sAppointmentType,'') as sAppointmentType from AB_AppointmentType where nAppProcType =1 order by sAppointmentType ", out dtApptType);

                if (dtApptType != null && dtApptType.Rows.Count > 0)
                {
                    cmbAppointmentType.DataSource    = dtApptType;
                    cmbAppointmentType.DisplayMember = dtApptType.Columns[1].ColumnName;
                    cmbAppointmentType.ValueMember   = dtApptType.Columns[0].ColumnName;
                    cmbAppointmentType.SelectedIndex = 0;
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
               
                cmbAppointmentType.DataSource = null;
                cmbAppointmentType.Items.Clear();
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (dtApptType != null)
                {
                    dtApptType.Dispose();
                }
            }
            finally 
            {
                if (oDB!= null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }              
            }
        }

        #endregion "Form Load and Fill Methods"

        #region "Tool Strip Events"

           private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        //Validation function
                        if (IsFormValid())
                        {
                           //Save Prior Authorization..
                            if (c1PriorAuthorization.Rows.Count == 1 )
                            {
                                _AuthorizationId = 0;
                            }
                            PriorAutorization oPriorAuthorization = new PriorAutorization(_databaseConnectionString);
                            AddAuthorization(oPriorAuthorization);
                            oPriorAuthorization.Dispose();
                            oPriorAuthorization = null;
                            _AuthorizationId = 0;
                            ClearControls();
                            frmPriorAuthorization_Load(null, null);
                            Show_PriorAuthorizations();
                            if (_CloseAfterSave == true)
                            {
                                this.Close();
                            }
                        }
                        break;
                    case "Close":
                        this.Close();
                        break;
                        //for the Visits and Insurance functionality,
                    case "CheckIn":
                        PatientCheckin(_PatientID);
                        break;
                        //**for the Visits and Insurance functionality,
                   
                    case "Clear":
                        _AuthorizationId = 0;
                        ClearControls();
                        frmPriorAuthorization_Load(null, null);
                        
                        break;
                    default:
                        break;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
                
        #endregion "Tool Strip Events"

        #region "Validations"
        private bool IsFormValid()
        {
            //if (txtVisitsMade.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please enter the Visits Made", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtVisitsMade.Select();
            //    return false;
            //}

            if (txtAuthorizationNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please enter an authorization number.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAuthorizationNumber.Select();
                return false;
            }

            if (txtTotalVisits.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the total number visits.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTotalVisits.Select();
                return false;
            }
            //|| Convert.ToDateTime(dtAuthorizationDate.Value) == Convert.ToDateTime(dtAuthorizationThroughDate.Value)
            if (Convert.ToDateTime(dtAuthorizationDate.Value) > Convert.ToDateTime(dtAuthorizationThroughDate.Value) )
            {
                MessageBox.Show("Authorization through date should be after date of authorization .  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtAuthorizationDate.Select();
                return false;
            }

            if (txtTotalVisits.Text.Trim() != "" && txtVisitsMade.Text.Trim()!= "")
            if (Convert.ToInt64(txtTotalVisits.Text.Trim()) < Convert.ToInt64(txtVisitsMade.Text.Trim()))
            {
                MessageBox.Show("Visits made cannot exceed total number of authorized visits.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVisitsMade.Select();
                return false;
            }

            if (cmbAppointmentType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the appointment type for prior authorization.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbAppointmentType.Select();
                return false;
            }

            //****** For restricting to save authorization without Insurance provider details ** code added by vishal on 24 june,09
            if (cmb_Insurances.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the insurance.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_Insurances.Select();               
                return false;
            }
             
            return true;
        }
        #endregion "Validations"

        #region "Supporting Methods"

            private void AddAuthorization(PriorAutorization oPriorAuthorization)
        {
            try
            {
                oPriorAuthorization.PatientID = _PatientID;
                oPriorAuthorization.InsuranceName = _InsuranceName;
                oPriorAuthorization.InsuranceID = _InsuranceId;
                if (dtAuthorizationDate.Text.Trim() != "")
                    oPriorAuthorization.AuthorizationDate = Convert.ToDateTime(dtAuthorizationDate.Text.Trim());
                if (dtAuthorizationThroughDate.Text.Trim() != "")
                    oPriorAuthorization.AuthorizationThroughDate = Convert.ToDateTime(dtAuthorizationThroughDate.Text.Trim());
                if (dtAuthorizationStatusDate.Text.Trim() != "")
                    oPriorAuthorization.AuthorizationStatusDate = Convert.ToDateTime(dtAuthorizationStatusDate.Text.Trim());
                oPriorAuthorization.AuthorizationStatus = cbAuthorizationStatus.SelectedValue.ToString().Trim();
                oPriorAuthorization.AuthorizationNumber = txtAuthorizationNumber.Text.Trim();
                if (txtTotalVisits.Text.Trim() != "")
                    oPriorAuthorization.TotalVisits = Convert.ToInt64(txtTotalVisits.Text.Trim());
                if (txtVisitsMade.Text.Trim() != "")
                    oPriorAuthorization.VisitsMade = Convert.ToInt64(txtVisitsMade.Text.Trim());
                //if (txtVisitsMade.Text.Trim() != "" && txtTotalVisits.Text.Trim() != "") 
                oPriorAuthorization.AppointMentType = Convert.ToInt64(cmbAppointmentType.SelectedValue.ToString());
                oPriorAuthorization.AuthorizationID = _AuthorizationId;
                //oPriorAuthorization.AuthorizationID = 0;
                oPriorAuthorization.AddModifyPriorAuthorization(oPriorAuthorization);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion "Supporting Methods"

        #region "Business Logic"

            private void txtVisitsMade_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    if (txtTotalVisits.Text.Trim() != "" && txtVisitsMade.Text.Trim() != "")
                    {
                        if ((Convert.ToInt64(txtTotalVisits.Text.Trim()) - Convert.ToInt64(txtVisitsMade.Text.Trim())) > 0)
                        {
                            lblVisitsLeft.Text = Convert.ToString(Convert.ToInt64(txtTotalVisits.Text.Trim()) - Convert.ToInt64(txtVisitsMade.Text.Trim()));
                        }
                        else
                        {
                            lblVisitsLeft.Text = "0";
                        }
                    }
                    else if(txtVisitsMade.Text.Trim() == "")
                    {
                        lblVisitsLeft.Text = txtTotalVisits.Text.Trim();
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            private void txtVisitsMade_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (Char.IsDigit(e.KeyChar) == true || Char.IsControl(e.KeyChar) == true)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }

            private void txtTotalVisits_TextChanged(object sender, EventArgs e)
            {
                if(txtVisitsMade.Text.Trim() == "")
                {
                txtVisitsMade.Text = "1";
                }
                if (txtTotalVisits.Text.Trim() == "")
                {
                    txtVisitsMade.Text = "";
                }
                txtVisitsMade_TextChanged(sender, e);
            }
            private void txtTotalVisits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) == true || Char.IsControl(e.KeyChar) == true)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion "Business Logic"

        #region "Methods to Display ,Add,Modify,Delete"

        private void PatientCheckin(Int64 patientID)
        {
            //Function Call for Checkin 
            ///declarations
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            String _sql = String.Empty;
            DataTable dttempPatientTracking = new DataTable();
            DataTable dttempPriorInsurances = new DataTable();
            // Check Checkin Flag for Patient.

            try
            {
                _sql = " Select nID from PatientTracking where nPatientID = '" + patientID + "' and dtDate ='" + DateTime.Now.Date + "'";

                oDB.Connect(false);
                oDB.Retrive_Query(_sql, out dttempPatientTracking);

                if (dttempPatientTracking != null && dttempPatientTracking.Rows.Count > 0)
                {
                    // Read Insurances if any from Prior Authorization.
                    _sql = "";
                    _sql = " SELECT ISNULL(PatientPriorAuthorization.nAuthorizationID, 0) AS nAuthorizationID, ISNULL(PatientPriorAuthorization.nPatientID, 0) AS nPatientID, ";
                    _sql += " ISNULL(PatientPriorAuthorization.nInsuranceID, 0) AS nInsuranceID, ISNULL(PatientPriorAuthorization.sInsuranceName, '') AS sInsuranceName, ";
                    _sql += " PatientPriorAuthorization.dtAuthorization, PatientPriorAuthorization.dtAuthorizationThrough, ISNULL(PatientPriorAuthorization.sAuthorizationNumber, '') ";
                    _sql += " AS sAuthorizationNumber, ISNULL(PatientPriorAuthorization.nTotalVisits, 0) AS nTotalVisits, ISNULL(PatientPriorAuthorization.nVisitsMade, 0) ";
                    _sql += " AS nVisitsMade, ISNULL(PatientPriorAuthorization.nAppointmentType, 0) AS nAppointmentType, AS_Appointment_MST.sAppointmentTypeDesc, ";
                    _sql += " AS_Appointment_DTL.dtStartDate , AS_Appointment_DTL.nDTLAppointmentID as nDTLAppointmentID, AS_Appointment_DTL.nMSTAppointmentID AS nMSTAppointmentID, AS_Appointment_DTL.nUsedStatus as  nUsedStatus ";
                    _sql += " FROM AS_Appointment_DTL INNER JOIN ";
                    _sql += " AS_Appointment_MST ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID INNER JOIN ";
                    _sql += " PatientPriorAuthorization ON AS_Appointment_MST.nPatientID = PatientPriorAuthorization.nPatientID AND ";
                    _sql += " AS_Appointment_MST.nAppointmentTypeID = PatientPriorAuthorization.nAppointmentType ";
                    _sql += " where PatientPriorAuthorization.npatientid = '" + _PatientID + "' AND AS_Appointment_DTL.dtStartDate = '" + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString()) + "' AND AS_Appointment_DTL.dtEndDate = '" + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString()) + "' ";
                    oDB.Retrive_Query(_sql, out dttempPriorInsurances);

                    if (dttempPriorInsurances != null && dttempPriorInsurances.Rows.Count > 0)
                    {
                        for (int i = 0; i < dttempPriorInsurances.Rows.Count; i++)
                        {
                            // Validate against Date.    
                            if (Convert.ToDateTime(dttempPriorInsurances.Rows[i]["dtAuthorization"]) <= DateTime.Now && Convert.ToDateTime(dttempPriorInsurances.Rows[i]["dtAuthorizationThrough"]) >= DateTime.Now)
                            {
                                //Increment Visits made for a PATIENT against all insurances.
                                _sql = " UPDATE PatientPriorAuthorization SET nVisitsMade =nVisitsMade + 1 where nPatientId = '" + _PatientID + "' and nInsuranceID = '" + dttempPriorInsurances.Rows[i]["nInsuranceID"] + "'and dtAuthorization <= '" + DateTime.Now.Date + "' and  dtAuthorizationThrough >= '" + DateTime.Now.Date + "'";
                                oDB.Execute_Query(_sql);
                            }
                            //Change the AppointmentDTL Status.
                            if (dttempPriorInsurances.Rows[i]["nDTLAppointmentID"].ToString() != "" && dttempPriorInsurances.Rows[i]["nMSTAppointmentID"].ToString() != "")
                            {
                                //Increment Visits made for a PATIENT against all insurances.
                                _sql = " UPDATE AS_Appointment_DTL SET nUsedStatus = 2 where nDTLAppointmentID = '" + dttempPriorInsurances.Rows[i]["nDTLAppointmentID"].ToString() + "' and nMSTAppointmentID = '" + dttempPriorInsurances.Rows[i]["nMSTAppointmentID"].ToString() + "'";
                                oDB.Execute_Query(_sql);
                            }
                            //**Change the AppointmentDTL Status.
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                if (dttempPatientTracking != null)
                {
                    dttempPatientTracking.Dispose();
                    dttempPatientTracking = null;
                }

                if (dttempPriorInsurances != null)
                {
                    dttempPriorInsurances.Dispose();
                    dttempPriorInsurances = null;
                }

                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dttempPatientTracking != null)
                {
                    dttempPatientTracking.Dispose();
                    dttempPatientTracking = null;
                }

                if (dttempPriorInsurances != null)
                {
                    dttempPriorInsurances.Dispose();
                    dttempPriorInsurances = null;
                }
            }
            
        }
            
        private void Show_PriorAuthorizations()
        {
            PriorAutorization oPriorAutorization = new PriorAutorization(_databaseConnectionString);
            DataTable dtTemp = null;
            dtTemp = oPriorAutorization.ViewPriorAuthorization(_PatientID);
            //dtTemp = ViewPriorAuthorization(_PatientID);
            c1PriorAuthorization.Rows.Count = 1;
            try
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {

                    c1PriorAuthorization.Rows.Add();
                    Int32 Index = c1PriorAuthorization.Rows.Count - 1;
                    c1PriorAuthorization.SetData(Index, COL_nVisitsMade, Convert.ToString(dtTemp.Rows[i]["nVisitsMade"]));
                    if (dtTemp.Rows[i]["dtAuthorization"] != null && dtTemp.Rows[i]["dtAuthorization"].ToString() != "")
                    {
                        c1PriorAuthorization.SetData(Index, COL_dtAuthorization, Convert.ToDateTime(dtTemp.Rows[i]["dtAuthorization"]));
                    }
                    if (dtTemp.Rows[i]["dtAuthorizationThrough"] != null && dtTemp.Rows[i]["dtAuthorizationThrough"].ToString() != "")
                    {
                        c1PriorAuthorization.SetData(Index, COL_dtAuthorizationThrough, Convert.ToDateTime(dtTemp.Rows[i]["dtAuthorizationThrough"]));
                    }
                    if (dtTemp.Rows[i]["dtAuthorizationStatus"] != null && dtTemp.Rows[i]["dtAuthorizationStatus"].ToString() != "")
                    {
                        c1PriorAuthorization.SetData(Index, COL_dtAuthorizationStatus, Convert.ToDateTime(dtTemp.Rows[i]["dtAuthorizationStatus"]));
                    }
                    c1PriorAuthorization.SetData(Index, COL_sAuthorizationStatus, Convert.ToString(dtTemp.Rows[i]["sAuthorizationStatus"]));
                    c1PriorAuthorization.SetData(Index, COL_nTotalVisits, Convert.ToString(dtTemp.Rows[i]["nTotalVisits"]));
                    c1PriorAuthorization.SetData(Index, COL_sAuthorizationNumber, Convert.ToString(dtTemp.Rows[i]["sAuthorizationNumber"]));

                    c1PriorAuthorization.SetData(Index, COL_nAppointmentType, Convert.ToString(dtTemp.Rows[i]["nAppointmentType"]));
                    c1PriorAuthorization.SetData(Index, COL_nAuthorizationID, Convert.ToString(dtTemp.Rows[i]["nAuthorizationID"]));
                    c1PriorAuthorization.SetData(Index, COL_sInsuranceName, Convert.ToString(dtTemp.Rows[i]["sInsuranceName"]));
                    c1PriorAuthorization.SetData(Index, COL_nInsuranceID, Convert.ToString(dtTemp.Rows[i]["nInsuranceID"]));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtTemp != null)
                {
                    dtTemp.Dispose();
                    dtTemp = null;
                }
                if (oPriorAutorization != null)
                {
                    oPriorAutorization.Dispose();
                    oPriorAutorization = null;
                }
            }
        }

        private void DesignGrid()
        {
            try
            {
                c1PriorAuthorization.Cols.Count = 11;
                c1PriorAuthorization.Rows.Count = 1;
                //nAuthorizationID,  nInsuranceID, sInsuranceName, dtAuthorization, dtAuthorizationThrough,
                //sAuthorizationNumber, nTotalVisits, nVisitsMade, nAppointmentType, sAuthorizationStatus, dtAuthorizationStatus

                c1PriorAuthorization.SetData(0, COL_nAuthorizationID, "nAuthorizationID ");
                c1PriorAuthorization.SetData(0, COL_nInsuranceID, "nInsuranceID");
                c1PriorAuthorization.SetData(0, COL_sInsuranceName, "InsuranceName");
                c1PriorAuthorization.SetData(0, COL_dtAuthorization, "Authorization Date");
                c1PriorAuthorization.SetData(0, COL_dtAuthorizationStatus, "Authorization Status Date");
                c1PriorAuthorization.SetData(0, COL_dtAuthorizationThrough, "Valid Till");
                c1PriorAuthorization.SetData(0, COL_sAuthorizationNumber, "Auth Number");
                c1PriorAuthorization.SetData(0, COL_nTotalVisits, "Total Visits");
                c1PriorAuthorization.SetData(0, COL_nVisitsMade, "Visits Made");
                c1PriorAuthorization.SetData(0, COL_nAppointmentType, " Appointment Type");
                c1PriorAuthorization.SetData(0, COL_sAuthorizationStatus, " Auth Status");


                c1PriorAuthorization.Cols[COL_nAuthorizationID].Visible = false;
                c1PriorAuthorization.Cols[COL_nInsuranceID].Visible = false;
                c1PriorAuthorization.Cols[COL_sInsuranceName].Visible = true;
                c1PriorAuthorization.Cols[COL_dtAuthorization].Visible = false;
                c1PriorAuthorization.Cols[COL_dtAuthorizationStatus].Visible = false;
                c1PriorAuthorization.Cols[COL_dtAuthorizationThrough].Visible = true;
                c1PriorAuthorization.Cols[COL_sAuthorizationNumber].Visible = true;
                c1PriorAuthorization.Cols[COL_nTotalVisits].Visible = true;
                c1PriorAuthorization.Cols[COL_nVisitsMade].Visible = true;
                c1PriorAuthorization.Cols[COL_nAppointmentType].Visible = false;
                c1PriorAuthorization.Cols[COL_sAuthorizationStatus].Visible = true;


                int nWidth = panel3.Width;
                c1PriorAuthorization.Cols[COL_nAuthorizationID].Width = 0;
                c1PriorAuthorization.Cols[COL_nInsuranceID].Width = 0;
                c1PriorAuthorization.Cols[COL_sInsuranceName].Width = (int)(0.25 * (nWidth));
                c1PriorAuthorization.Cols[COL_dtAuthorization].Width = 0;
                c1PriorAuthorization.Cols[COL_dtAuthorizationStatus].Width = 0;
                c1PriorAuthorization.Cols[COL_dtAuthorizationThrough].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_sAuthorizationNumber].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_nTotalVisits].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_nVisitsMade].Width = (int)(0.15 * (nWidth));
                c1PriorAuthorization.Cols[COL_nAppointmentType].Width = 0;
                c1PriorAuthorization.Cols[COL_sAuthorizationStatus].Width = (int)(0.15 * (nWidth));

                c1PriorAuthorization.Cols[COL_dtAuthorization].AllowEditing = true; ;
                c1PriorAuthorization.Cols[COL_dtAuthorizationStatus].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_dtAuthorizationThrough].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_sAuthorizationNumber].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_nTotalVisits].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_nVisitsMade].AllowEditing = true;
                c1PriorAuthorization.Cols[COL_sAuthorizationStatus].AllowEditing = true;

                c1PriorAuthorization.AllowEditing = false;
                c1PriorAuthorization.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //private DataTable ViewPriorAuthorization(Int64 PatientID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_connectionString);
        //    oDB.Connect(false);
        //    DataTable dt_PriorAuthorization = null;
        //    try
        //    {
        //        string Sqlquery = "SELECT   ISNULL(nAuthorizationID,0)AS nAuthorizationID, ISNULL(nPatientID,0) AS  nPatientID, " +
        //        "ISNULL(nInsuranceID,0) AS nInsuranceID,ISNULL(sInsuranceName,'') AS sInsuranceName, " +
        //        "dtAuthorization, dtAuthorizationThrough, ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber,  " +
        //        "ISNULL(nTotalVisits,0) AS nTotalVisits,  ISNULL(nVisitsMade,0) AS nVisitsMade, ISNULL(nAppointmentType,0) AS nAppointmentType,  " +
        //        "ISNULL(sAuthorizationStatus,'') AS sAuthorizationStatus,  dtAuthorizationStatus " +
        //        "FROM PatientPriorAuthorization WHERE nPatientID= " + PatientID + "  ORDER BY nAuthorizationID ";

        //        oDB.Retrive_Query(Sqlquery, out dt_PriorAuthorization);
        //        if (dt_PriorAuthorization != null)
        //        {
        //            return dt_PriorAuthorization;

        //        }
        //        return null;


        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        MessageBox.Show("Error in Connecting Database -" + dbEx.ToString(), "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null; ;

        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }

        //}

        //private void Delete_Authorization(Int64 AuthorizationID)
        //{
        //    gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_connectionString);
        //    ODB.Connect(false);
        //    string sqlquery = "DELETE  FROM PatientPriorAuthorization WHERE  nAuthorizationID= " + AuthorizationID + "  ";

        //    try
        //    {
        //        ODB.Execute_Query(sqlquery);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error in Connecting Database -" + ex.ToString(), "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}

        private void Modify()
        {
           // PriorAutorization oPriorAutorization = new PriorAutorization(_databaseConnectionString);
            try
            {
                if (c1PriorAuthorization.Rows.Count > 1)
                {
                    txtTotalVisits.Enabled = false;   

                    dtAuthorizationDate.Value = Convert.ToDateTime(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_dtAuthorization));
                    dtAuthorizationThroughDate.Value = Convert.ToDateTime(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_dtAuthorizationThrough));
                    dtAuthorizationStatusDate.Value = Convert.ToDateTime(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_dtAuthorizationStatus));
                    cbAuthorizationStatus.Text = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_sAuthorizationStatus));
                    txtAuthorizationNumber.Text = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_sAuthorizationNumber));
                    txtTotalVisits.Text = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nTotalVisits));
                    txtVisitsMade.Text = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nVisitsMade));
                    cmbAppointmentType.Text = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nAppointmentType));
                    cmbAppointmentType.SelectedValue = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nAppointmentType));
                    cmb_Insurances.SelectedValue = Convert.ToString(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nInsuranceID));
                    _AuthorizationId = Convert.ToInt64(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nAuthorizationID));
                    _tempID = _AuthorizationId;
                    
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error in connecting database -" + Ex.ToString(), _messageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void FillPatientInsurance(Int64 PatientID)
        {
            try
            {
                DataTable dt_ins = null;
                dt_ins = getPatientInsurances(PatientID);
               
                cmb_Insurances.DataSource = null;
                cmb_Insurances.Items.Clear();
                if (dt_ins != null && dt_ins.Rows.Count > 0)
                {
                    cmb_Insurances.DataSource = dt_ins;
                    cmb_Insurances.DisplayMember = dt_ins.Columns["InsuranceName"].ToString();
                    cmb_Insurances.ValueMember = dt_ins.Columns["nInsuranceID"].ToString();
                    cmb_Insurances.SelectedValue = _InsuranceId;

                }

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
            }

        }

        public DataTable getPatientInsurances(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);

            DataTable dtInsurance = null;
            try
            {

                //string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                //                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                //                    " PatientInsurance_DTL.sPhone, " +
                //                    " PatientInsurance_DTL.dtDOB,  " +
                //                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                //                    " PatientInsurance_DTL.dtExpiryDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                //                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                //                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                //                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                //                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                //                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                //                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                //                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                //                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                //                    " PatientInsurance_DTL.sSubscriberGender,  " +
                //                    " PatientInsurance_DTL.sPayerID,  " +
                //                    " ISNULL(Patient.sCity, '') AS sCity, " +
                //                    " ISNULL(Patient.sState, '') AS sState,  " +
                //                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                //                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                //                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                //                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                //                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                //                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                //                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                //                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                //                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                //                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                //                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                //                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                //                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                //                    " ELSE '' END  AS sInsuranceFlag  " +
                //                    " FROM PatientInsurance_DTL  " +
                //                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                //                    " INNER JOIN PatientRelationship ON  " +
                //                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                //                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY nInsuranceFlag ";

                //Query changed 20081222 ,join modified in the query 

                string _strQuery = " SELECT PatientInsurance_DTL.nInsuranceID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceName, '') AS InsuranceName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberID, '')  AS sSubscriberID, " +
                                    " ISNULL(PatientInsurance_DTL.sSubFName, '') + SPACE(1) +  " +
                                    " ISNULL(PatientInsurance_DTL.sSubMName, '') + SPACE(1)   +  " +
                                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS sSubscriberName,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberPolicy#, '') AS sSubscriberPolicy#, " +
                                    " ISNULL(PatientInsurance_DTL.sGroup, '') AS sGroup,  " +
                                    " PatientInsurance_DTL.sPhone, " +
                                    " PatientInsurance_DTL.dtDOB,  " +
                                    " PatientInsurance_DTL.dtEffectiveDate,  " +
                                    " PatientInsurance_DTL.dtExpiryDate,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubFName, '') AS SubFName,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubMName, '') AS SubMName, " +
                                    " ISNULL(PatientInsurance_DTL.sSubLName, '') AS SubLName,   " +
                                    " ISNULL(PatientInsurance_DTL.nRelationShipID, 0) AS RelationShipID,  " +
                                    " ISNULL(PatientInsurance_DTL.sRelationShip, '') AS RelationShip,  " +
                                    " ISNULL(PatientInsurance_DTL.nDeductableamount, 0) AS Deductableamount, " +
                                    " ISNULL(PatientInsurance_DTL.nCoveragePercent, 0) AS CoveragePercent,  " +
                                    " ISNULL(PatientInsurance_DTL.nCoPay, 0) AS CoPay,  " +
                                    " ISNULL(PatientInsurance_DTL.bAssignmentofBenifit, 0) AS AssignmentofBenifit,  " +
                                    " PatientInsurance_DTL.dtStartDate, PatientInsurance_DTL.dtEndDate,  " +
                                    " ISNULL(PatientInsurance_DTL.nInsuranceFlag, 0) AS nInsuranceFlag, " +
                                    " PatientInsurance_DTL.sSubscriberGender,  " +
                                    " PatientInsurance_DTL.sPayerID,  " +
                                    " ISNULL(Patient.sCity, '') AS sCity, " +
                                    " ISNULL(Patient.sState, '') AS sState,  " +
                                    " ISNULL(Patient.sZIP, '') AS sZIP,   " +
                                    " ISNULL(Patient.sAddressLine1, '') AS sAddress1, " +
                                    " ISNULL(Patient.sAddressLine2, '') AS sAddress2, " +
                                    " ISNULL(PatientRelationship.sRelationshipCode, '')   AS RelationshipCode, " +
                                    " ISNULL(PatientInsurance_DTL.nContactID,0) AS nContactID, " +
                                    " ISNULL(PatientInsurance_DTL.sInsuranceTypeCode, '') AS InsuranceTypeCode, " +
                                    " ISNULL(PatientInsurance_DTL.sPayerId, '') AS PayerID, " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr1, '') AS SubscriberAddr1,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberAddr2, '') AS SubscriberAddr2,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberCity, '') AS SubscriberCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberState, '') AS SubscriberState,  " +
                                    " ISNULL(PatientInsurance_DTL.sSubscriberZip, '') AS SubscriberZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sZip, '') AS PayerZip,  " +
                                    " ISNULL(PatientInsurance_DTL.sCity, '') AS PayerCity,  " +
                                    " ISNULL(PatientInsurance_DTL.sState, '') AS PayerState,  " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine1, '') AS PayerAddress1, " +
                                    " ISNULL(PatientInsurance_DTL.sAddressLine2, '') AS PayerAddress2, " +
                                    " CASE ISNULL(PatientInsurance_DTL.nInsuranceFlag,0) " +
                                    " WHEN 0 THEN 'InActive' WHEN 1 THEN 'Primary'  " +
                                    " WHEN 2 THEN 'Secondary' WHEN 3 THEN 'Tertiary'   " +
                                    " ELSE '' END  AS sInsuranceFlag  " +
                                    " FROM PatientInsurance_DTL  " +
                                    " INNER JOIN Patient ON PatientInsurance_DTL.nPatientID = Patient.nPatientID  " +
                                    " LEFT JOIN PatientRelationship ON  " +
                                    " PatientInsurance_DTL.nRelationShipID = PatientRelationship.nPatientRelID  " +
                                    " WHERE PatientInsurance_DTL.nPatientID='" + PatientID + "'   ORDER BY nInsuranceFlag ";



                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dtInsurance);
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                dbEX.ERROR_Log(dbEX.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return dtInsurance;
        }
        
        private void cmb_Insurances_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmb_Insurances.SelectedValue != null)
            {
                _InsuranceId = Convert.ToInt64(cmb_Insurances.SelectedValue.ToString());
                _InsuranceName = cmb_Insurances.Text;
            }
        }

        private void c1PriorAuthorization_DoubleClick(object sender, EventArgs e)
        {
            tsb_Clear.Visible = true;
            if (c1PriorAuthorization.Row == -1)
            {
                return;
            }
            Modify();
        }

        private void GetLastauthorization()
        {
            DataTable dtTemp = new DataTable();
            PriorAutorization oPriorAutorization = new PriorAutorization(_databaseConnectionString);
            try
            {
                dtTemp = oPriorAutorization.GetPriorAuthorization(PatientID, InsuranceID, InsuranceName);
                //Fill retrieved values
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    txtTotalVisits.Enabled = false;    //Disable the Total Visits to be Made.

                    if (dtTemp.Rows[0]["dtAuthorization"].ToString().Trim() != "")
                        dtAuthorizationDate.Value = Convert.ToDateTime(dtTemp.Rows[0]["dtAuthorization"]);
                    if (dtTemp.Rows[0]["dtAuthorizationThrough"].ToString().Trim() != "")
                        dtAuthorizationThroughDate.Value = Convert.ToDateTime(dtTemp.Rows[0]["dtAuthorizationThrough"]);
                    if (dtTemp.Rows[0]["dtAuthorizationStatus"].ToString().Trim() != "")
                        dtAuthorizationStatusDate.Value = Convert.ToDateTime(dtTemp.Rows[0]["dtAuthorizationStatus"]);
                    cbAuthorizationStatus.SelectedValue = Convert.ToString(dtTemp.Rows[0]["sAuthorizationStatus"]);
                    txtAuthorizationNumber.Text = Convert.ToString(dtTemp.Rows[0]["sAuthorizationNumber"]);
                    txtTotalVisits.Text = Convert.ToString(dtTemp.Rows[0]["nTotalVisits"]);
                    txtVisitsMade.Text = Convert.ToString(dtTemp.Rows[0]["nVisitsMade"]);
                    if (dtTemp.Rows[0]["nAppointmentType"].ToString().Trim() != "")
                        cmbAppointmentType.SelectedValue = Convert.ToInt64(dtTemp.Rows[0]["nAppointmentType"]);

                }
                else //No entry for the Insurance.
                {
                    txtTotalVisits.Enabled = true;

                }


                //BL for the Visits.
                if (txtTotalVisits.Text.Trim() != "" && txtVisitsMade.Text.Trim() != "")
                {
                    if ((Convert.ToInt64(txtTotalVisits.Text.Trim()) - Convert.ToInt64(txtVisitsMade.Text.Trim())) > 0)
                    {
                        lblVisitsLeft.Text = Convert.ToString(Convert.ToInt64(txtTotalVisits.Text.Trim()) - Convert.ToInt64(txtVisitsMade.Text.Trim()));
                    }
                    else
                    {
                        lblVisitsLeft.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtTemp != null)
                {
                    dtTemp.Dispose();
                    dtTemp = null;
                }
                if (oPriorAutorization != null)
                {
                    oPriorAutorization.Dispose();
                    oPriorAutorization = null;
                }
            }
        } 

        private void ClearControls()
        {
            txtTotalVisits.Enabled = true;  
            txtAuthorizationNumber.Clear();
            txtTotalVisits.Clear();
            txtVisitsMade.Clear();
            dtAuthorizationDate.Value = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            dtAuthorizationStatusDate.Value = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            dtAuthorizationThroughDate.Value = Convert.ToDateTime(DateTime.Today.ToShortDateString());
        }

        #endregion

        private void c1PriorAuthorization_Click(object sender, EventArgs e)
        {
            //ContextMenu cmnu = new ContextMenu();
            //c1PriorAuthorization.ContextMenu = cmnu;

            //MenuItem cmnu_Modify = new MenuItem();
            //cmnu_Modify.Text = "Modify";
            //cmnu_Modify.
            //cmnu.MenuItems.Add(cmnu_Modify); 

            //MenuItem cmnu_Delete = new MenuItem();
            //cmnu_Delete.Text = "Delete";
            //cmnu.MenuItems.Add(cmnu_Delete);
        }
        //private void cmnu_Delete_Click(object sender, EventArgs e)
        //{
        //    PriorAutorization oPriorAuthorization = new PriorAutorization(_connectionString);
        //    if (c1PriorAuthorization.Rows.Count > 1)
        //        _AuthorizationId = Convert.ToInt64(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nAuthorizationID));
        //    if (oPriorAuthorization.Delete_PriorAuthorization(_AuthorizationId) == true)
        //        Show_PriorAuthorizations();
        //}

        //private void cmnu_Modify_Click(object sender, EventArgs e)
        //{
        //    Modify();
        //}
        private void c1PriorAuthorization_MouseDown(object sender, MouseEventArgs e)
        {
            
          
            c1PriorAuthorization.ContextMenuStrip = null;
            if (c1PriorAuthorization.Rows.Count > 1)
            c1PriorAuthorization.ContextMenuStrip = cmnu_PriorAuthorization;

        }
        

        private void modiFyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modify();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PriorAutorization oPriorAuthorization = new PriorAutorization(_databaseConnectionString);
            if (c1PriorAuthorization.Rows.Count > 1)
            {
                _AuthorizationId = Convert.ToInt64(c1PriorAuthorization.GetData(c1PriorAuthorization.RowSel, COL_nAuthorizationID));
                if (oPriorAuthorization.Delete_PriorAuthorization(_AuthorizationId) == true)
                {
                    Show_PriorAuthorizations();
                    if (_AuthorizationId == _tempID)
                    {
                       _AuthorizationId = 0;
                        ClearControls();
                        frmPriorAuthorization_Load(null, null);
                        
                    }
                    if (c1PriorAuthorization.Rows.Count > 1)
                        for (int i = 1; i < c1PriorAuthorization.Rows.Count; i++)
                        {
                            if (_tempID == Convert.ToInt64(c1PriorAuthorization.GetData(i, COL_nAuthorizationID)))
                            {
                                c1PriorAuthorization.Row = i;
                                break;
                                
                            }

                        }
                }
            }
            if (oPriorAuthorization != null)
            {
                oPriorAuthorization.Dispose();
                oPriorAuthorization = null;
            }
        }

        private void c1PriorAuthorization_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

       

    }
}