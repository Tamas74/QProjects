using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;
using System.Collections;
using gloSettings;


namespace gloBilling
{
    public partial class frmInsuranceReserveRemaining : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private bool _isOpenForModify = false;   
        public bool _isOpenForView = false;

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

       // EOBPayment.Common.PaymentInsurance oPaymentInsurace = null;
     //   EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentReserveDetail = null;     
        private EOBPayment.Common.InsuranceReserveRemainingDetails _ReserveDetails = null;
        private SplitClaimDetails _ClaimDetails = new SplitClaimDetails();
        bool _viewPatientDefault = false;
        Int64 _EOBPaymentDetailID = 0;
        Int64 _EOBPaymentID = 0;

        #endregion " Declarations "

        #region " Property Procedures "
        
        //public decimal ReserveAllowed
        //{
        //    get { return _ReserveAllowed; }
        //    set { _ReserveAllowed = value; }
        //}
        
        public Int64 EOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }
    
        public Int64 EOBPaymentDetailID 
        {
            get { return _EOBPaymentDetailID; }
            set { _EOBPaymentDetailID = value; }
        }

        public bool IsOpenForModify
        {
            get { return _isOpenForModify; }
            set { _isOpenForModify = value;}
        }     

        private SplitClaimDetails ClaimDetails
        {
            get { return _ClaimDetails; }
            set
            {
                _ClaimDetails = value;

                cmbClaimNo.Text = string.Empty;
                if (_ClaimDetails.IsClaimExist)
                { cmbClaimNo.Text = _ClaimDetails.ClaimDisplayNo; }
            }
        }

        public EOBPayment.Common.InsuranceReserveRemainingDetails ReserveDetails
        {

            get { return _ReserveDetails; }
            set { _ReserveDetails = value; }

        }     
 
        public bool viewPatientDefault
        {
            get { return _viewPatientDefault; }
            set { _viewPatientDefault = value; }
        }

        #endregion

        #region " Constructor "
      
        public frmInsuranceReserveRemaining(string DatabaseConnectionString, Int64 nEOBPaymentID, Int64 nEOBPaymentDetailID)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrieve ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #endregion

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
            
            _EOBPaymentID = nEOBPaymentID;
            _EOBPaymentDetailID = nEOBPaymentDetailID;      
            _isOpenForModify = true;
        }

        public frmInsuranceReserveRemaining(string DatabaseConnectionString,bool OpenModify)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            #region " Retrieve ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #endregion

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

            // _ReserveAllowed = AllowedReserveAmt;
            //ReserveDetails.AmountToReserve = ReserveAmount;
            //ReserveDetails.ReserveNote = Note;
            _isOpenForModify = OpenModify;

        }
      
        #endregion

        #region " Form Load Event "

        private void frmInsuranceReserveRemaining_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
            try
            {
                if (_isOpenForView)
                {
                    ShowInsuranceReserved();
                }
                getData();  
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        #endregion " Form Load Event "

        #region " Control Events "

        private void tlsbtnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveValidation() == true && IsInsuranceCompanySelected())
                {
                    bool _IsValidClaim = false;
                    _IsValidClaim = getValidClaimDetails();
                    if (_IsValidClaim)
                    {
                        setData();
                        if (_isOpenForView)
                        {
                            UpdateReserveNote();
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void tlsbtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
        }

        private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            try
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
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
                               
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            txtPatient.Text = "";
            txtPatient.Tag = 0;
           // cmbClaimNo.Items.Clear();
            cmbClaimNo.DataSource = null;
            cmbClaimNo.Items.Clear();
            cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
            cmbClaimNo.Text = "";
            cmbClaimNo.Tag = 0;
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            try
            {
               
                int _Counter = 0;
                switch (_CurrentControlType)
                {
                    case gloListControl.gloListControlType.InsuranceCompany:
                        {
                            txtInsCompany.Text = "";
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                DataTable oBindTable = new DataTable();

                                oBindTable.Columns.Add("ID");
                                oBindTable.Columns.Add("DispName");

                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    DataRow oRow;
                                    oRow = oBindTable.NewRow();
                                    oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                    oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                    oBindTable.Rows.Add(oRow);
                                }
                                txtInsCompany.Tag= Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                                txtInsCompany.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                            }
                        }
                        break;
                    case gloListControl.gloListControlType.Patient:
                        {
                            txtPatient.Text  = "";
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                DataTable oBindTable = new DataTable();

                                oBindTable.Columns.Add("ID");
                                oBindTable.Columns.Add("DispName");

                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    DataRow oRow;
                                    oRow = oBindTable.NewRow();
                                    oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                    oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                    oBindTable.Rows.Add(oRow);
                                }

                                txtPatient.Text =Convert.ToString(oBindTable.Rows[0]["DispName"]);
                                txtPatient.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                            }
                            DataTable dtClaim = new DataTable();
                            
                            cmbClaimNo.DataSource = null;
                            cmbClaimNo.Items.Clear();
                            cmbClaimNo.Text = "";
                            getPatientClaimNos(Convert.ToInt64(txtPatient.Tag));
                        }
                        break;
                }
                this.Width = 572;
                this.Height = 304;
            }
            catch (Exception)
            {


            }
            finally
            {
               
            
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch { }
               
            }
                this.Width = 572;
                this.Height = 304;
        }

        private void cmbClaimNo_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                if (e.KeyChar == Convert.ToChar(45) && cmbClaimNo.Text.Contains("-") == true)
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == Convert.ToChar(45) && cmbClaimNo.Text.Contains("-") == false)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == 13)
            {
              getValidClaimDetails();               
            }
        }

        private void btnSearchInsuranceCompany_Click(object sender, EventArgs e)
        {

            try
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
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.InsuranceCompany, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Insurance Company";
                _CurrentControlType = gloListControl.gloListControlType.InsuranceCompany;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            ex = null;
            }       

        }

        private void btnClearInsCompany_Click(object sender, EventArgs e)
        {
            txtInsCompany.Text = "";
            txtInsCompany.Tag =0;
        }
        
        #endregion

        #region " Private and Public Methods "

        private bool SaveValidation()
        {
            bool _retValue = true;
            decimal _ReserveAmount = 0;
            try
            {
                _ReserveAmount = Convert.ToDecimal(txtAmount.Text);

                // Validation Commented by pankaj 30042010
                // ------------------------------------
                //if (_ReserveAmount > _ReserveAllowed) // Reserve amount should not be greater than reserve allowed.
                //{
                //    MessageBox.Show("Please enter Reserve Amount less than or equal to " + _ReserveAllowed.ToString("#.00"), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    _retValue = false;
                //}
               
            }
            catch 
            {
                MessageBox.Show("Please enter a valid amount.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog("Please enter a valid amount", false);
                _retValue = false;
            }
            return _retValue;
        }

        /// <summary>
        /// To Update the Reserve Notes if Open for Modify
        /// </summary>
        public void UpdateReserveNote()
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                #region "Add Reserve Association Entry"

                oParameters.Clear();
                oParameters.Add("@nEOBPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nEOBPaymentDetailID", EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionID", ReserveDetails.MSTTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTrackTrnID", ReserveDetails.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", ReserveDetails.PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.ExecuteScalar("BL_INUP_Reserve_Association", oParameters);
                oDB.Disconnect();

                #endregion

                #region "Update Note"


                string _strSQL = "";

                _strSQL = " UPDATE BL_EOB_Notes SET sNoteDescription = '" + ReserveDetails.ReserveNote + "' WHERE nEOBPaymentID= " + EOBPaymentID +
                    " AND nPaymentNoteType = 1 AND nPaymentNoteSubType = 9";

                oDB.Connect(false);
                oDB.ExecuteScalar_Query(_strSQL);
                oDB.Disconnect();

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }

        }
 
        void ShowInsuranceReserved()
        {
            txtAmount.Enabled = false;

            //Added By Pramod Nair To Edit the Notes 
            txtNotes.Enabled = true;
            tlsbtnSaveClose.Visible = true;
            btnClearInsCompany.Enabled = false;
            btnSearchInsuranceCompany.Enabled = false;
        }

        DataTable GetInsuranceReserve(Int64 InsuranceCompanyID, Int64 nEOBPaymentID, Int64 nEOBPaymentDetailID)
        {
            return new DataTable();
        }

        private void getPatientClaimNos(Int64 nPatientID)
        {
            DataTable _dtClaimNo = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                string _strSql = "";
                _strSql = "SELECT CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionMasterID )+ '-' + CONVERT(VARCHAR,BL_Transaction_Claim_MST.nTransactionID) AS ID, "
                         + " dbo.GetSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo,BL_Transaction_Claim_MST.nSubClaimNo ,BL_Transaction_Claim_MST.sMainClaimNo,5) as Claim  "
                         + " FROM BL_Transaction_Claim_MST  WITH (NOLOCK) WHERE (LEFT(nSubClaimNo,1)<> '-')  AND nPatientID = " + nPatientID + "" 
                         + " ORDER BY dtCreateDate DESC";

                oDB.Connect(false);
                oDB.Retrive_Query(_strSql, out _dtClaimNo);
                oDB.Disconnect();

                if (_dtClaimNo != null && _dtClaimNo.Rows.Count > 0)
                {
                    //if (_dtClaimNo.Rows.Count > 1)
                    //{
                        DataRow dr = _dtClaimNo.NewRow();
                        dr["Claim"] = "";
                        dr["ID"] = 0;
                        _dtClaimNo.Rows.InsertAt(dr, 0);


                        cmbClaimNo.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmbClaimNo.DataSource = _dtClaimNo;
                        cmbClaimNo.DisplayMember = "Claim";
                        cmbClaimNo.ValueMember = "ID";

                    //}
                    //else
                    //{
                    //    cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                    //    cmbClaimNo.DataSource = _dtClaimNo;
                    //    cmbClaimNo.DisplayMember = "Claim";
                    //    cmbClaimNo.ValueMember = "ID";
                    //}
                }
                else
                {
                    cmbClaimNo.DropDownStyle = ComboBoxStyle.Simple;
                }                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }
        
        private DataTable getInsCompany(Int64 nInsuranceCompanyID)
        {
            DataTable dtInsCompany = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
          
            try
            {
                if (nInsuranceCompanyID != 0)
                {
                    string _strSql = "";
                    _strSql = "SELECT ISNULL(sDescription,'') AS sInsName FROM Contacts_InsuranceCompany_MST  WITH (NOLOCK) " +
                              " WHERE nID = " + nInsuranceCompanyID + "";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSql, out dtInsCompany);
                    oDB.Disconnect();
                }
                return dtInsCompany;
            }
            catch (Exception ex)
               
            {  
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
                return null;
            }

        }

        private bool getValidClaimDetails()
        {
           gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
           gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        
           try
           {
               if (cmbClaimNo.Text.StartsWith("-"))
               {
                   MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   txtPatient.Text = "";
                   cmbClaimNo.Text = "";
                   return false;
               }
               else
               {                   
                   ogloBilling.ClaimNumber = cmbClaimNo.Text;
                   ogloBilling.SetClaimNumbers();

                   if (ogloBilling.MainClaimNumber != 0 || ogloBilling.SubClaimNumber != "")
                   {
                       ClaimDetails = new SplitClaimDetails(ogloBilling.MainClaimNumber, ogloBilling.SubClaimNumber);

                       if (!ClaimDetails.IsClaimExist)
                       {
                           MessageBox.Show("Claim selected is invalid or does not exist.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                           txtPatient.Text = "";
                           return false;
                       }
                       else
                       {

                           DataTable dtTransactionID = new DataTable();
                           string _strSql = "";

                           if (ogloBilling.SubClaimNumber == "")
                           {
                               _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                   + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                   + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                   + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                   + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + "";
                           }
                           else
                           {
                               _strSql = " SELECT BL_Transaction_Claim_MST.nPatientID,ISNULL(Patient.sFirstName,'') + SPACE(1) +   "
                                       + " CASE ISNULL(Patient.sMiddleName,'') WHEN  '' THEN ''  WHEN Patient.sMiddleName THEN  Patient.sMiddleName + SPACE(1)  "
                                       + " END + ISNULL(Patient.sLastName,'') AS Patient,"
                                       + " BL_Transaction_Claim_MST.nTransactionMasterID,BL_Transaction_Claim_MST.nTransactionID from BL_Transaction_Claim_MST  WITH (NOLOCK) INNER JOIN Patient  WITH (NOLOCK) ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID "
                                       + " WHERE nClaimNo = " + ogloBilling.MainClaimNumber + " AND nSubClaimNo = " + ogloBilling.SubClaimNumber;
                           }
                           oDB.Connect(false);
                           oDB.Retrive_Query(_strSql, out dtTransactionID);
                           oDB.Disconnect();
                           if (oDB != null) { oDB.Dispose(); }

                           cmbClaimNo.Tag = Convert.ToString(Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionMasterID"])) + '-' + Convert.ToString(Convert.ToInt64(dtTransactionID.Rows[0]["nTransactionID"]));
                           txtPatient.Text = "";
                           txtPatient.Text = Convert.ToString(dtTransactionID.Rows[0]["Patient"]);
                           txtPatient.Tag = Convert.ToInt64(dtTransactionID.Rows[0]["nPatientID"]);

                       }
                   }
                 

               } return true;

           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
               ex = null;
               return false;
           }
           finally
           {
               if (oDB != null) { oDB.Dispose(); }
               if (ogloBilling != null) { ogloBilling.Dispose(); }
           }
       
        }

        private bool IsInsuranceCompanySelected()
        {
            if (txtInsCompany.Tag == null || Convert.ToInt64(txtInsCompany.Tag).Equals(0))              
            {
                MessageBox.Show("Please select Insurance Company.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        public void setData()
        {
            try
            {
                ReserveDetails = new global::gloBilling.EOBPayment.Common.InsuranceReserveRemainingDetails();

                ReserveDetails.InsuranceCompany = txtInsCompany.Text;
                ReserveDetails.InsuranceCompanyID = Convert.ToInt64(txtInsCompany.Tag);
                ReserveDetails.PatientID = Convert.ToInt64(txtPatient.Tag);
                ReserveDetails.PatientName = txtPatient.Text;
                ReserveDetails.ClaimNo = cmbClaimNo.Text;
                ReserveDetails.AmountToReserve = Convert.ToDecimal(txtAmount.Text);
                ReserveDetails.ReserveNote = txtNotes.Text;

                if (cmbClaimNo.SelectedValue != null && cmbClaimNo.SelectedValue.ToString() != "0"  )
                {
                    string[] IDs = cmbClaimNo.SelectedValue.ToString().Split('-');
                    if (IDs.Length.Equals(2))
                    {
                        ReserveDetails.MSTTransactionID = Convert.ToInt64(IDs[0]);
                        ReserveDetails.TransactionID = Convert.ToInt64(IDs[1]);
                    }
                }
                else if (cmbClaimNo.Text != null && Convert.ToString(cmbClaimNo.Text) != "")
                {
                    string[] IDs = cmbClaimNo.Tag.ToString().Split('-');
                    if (IDs.Length.Equals(2))
                    {
                        ReserveDetails.MSTTransactionID = Convert.ToInt64(IDs[0]);
                        ReserveDetails.TransactionID = Convert.ToInt64(IDs[1]);
                    }                
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        public void getData()
        {
            try
            {
                if (ReserveDetails != null)
                {
                    txtInsCompany.Text = ReserveDetails.InsuranceCompany;
                    txtInsCompany.Tag = Convert.ToString(ReserveDetails.InsuranceCompanyID);
                    txtPatient.Text = ReserveDetails.PatientName;
                    txtPatient.Tag = Convert.ToString(ReserveDetails.PatientID);
                    getPatientClaimNos(ReserveDetails.PatientID);
                    cmbClaimNo.Text = ReserveDetails.ClaimNo;
                    cmbClaimNo.Tag = Convert.ToString(ReserveDetails.MSTTransactionID) + '-' + Convert.ToString(ReserveDetails.TransactionID);
                    txtAmount.Text = ReserveDetails.AmountToReserve.ToString("#0.00");
                    txtNotes.Text = ReserveDetails.ReserveNote.Trim();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        #endregion                 

        private void cmbClaimNo_Leave(object sender, EventArgs e)
        {
            getValidClaimDetails();    
        }
         
    }
}