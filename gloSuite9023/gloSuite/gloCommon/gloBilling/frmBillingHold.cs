using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmBillingHold : Form
    {

        #region " Variable Declarations"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

       
        public bool oDialogResult = false;
        private string _databaseConnection = "";
        private Int64 _TransactionID = 0;
        private Int64 _TransactionMasterID = 0;
        //private Int64 _ClinicID = 0;
        private string _ClaimID = "";
        private Int64 _UserID = 0;
        private string _UserName = "";
        public ClaimHold _oClaimHold = null;
        private string _messageBoxCaption = "";
        private Boolean bReasonModified = false;
        private string sResPartyStatus = "";
       
        #endregion

        #region "Constructor"

        public frmBillingHold(string DatabaseConnectionString, string ClaimID, Int64 ClinicID, Int64 UserID, Int64 TransactionID, Int64 TransactionMasterID, ClaimHold oClaimHold,string sPartyStatus)
        {
            InitializeComponent();
            _databaseConnection = DatabaseConnectionString;
            _TransactionID = TransactionID;
            _TransactionMasterID = TransactionMasterID;
            //_oClaimHold = new ClaimHold();
            _oClaimHold = oClaimHold;
            _ClaimID = ClaimID;
            sResPartyStatus = sPartyStatus;
            
            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }
            #endregion
            
            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

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
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        } 

        #endregion

        #region "Form Load"
     
        private void frmBillingHold_Load(object sender, EventArgs e)
        {
            try
            {
                txtHoldNote.MaxLength = 255;
                txtHoldNoteMod.MaxLength = 255;
                
              //  lblHoldReason.MaximumSize= 34;
                FillComboBoxData(_oClaimHold.IsHold);

                //Reason For Condition****************************************
                //if already that claim is on Hold
                //*************************************************************
                
                if (_oClaimHold.IsHold)
                {
                    pnlBillingHold.Visible = false;
                    pnlOnBillingHold.Visible = true;

                    lblHoldDate.Text = _oClaimHold.HoldDateTime.ToString("MM/dd/yyyy"); //Convert.ToString(_oClaimHold.HoldDateTime.Date);
                    lblHoldTime.Text = _oClaimHold.HoldDateTime.ToString("hh:mm tt");
                    lblHoldUser.Text = GetUserName(Convert.ToInt64(_oClaimHold.HoldUserID));

                    lblHoldModDate.Text = _oClaimHold.HoldModDateTime.ToString("MM/dd/yyyy");
                    lblHoldModTime.Text = _oClaimHold.HoldModDateTime.ToString("hh:mm tt");
                    lblHoldModUser.Text = GetUserName(Convert.ToInt64(_oClaimHold.HoldModUserID));

                    txtHoldNoteMod.Text = _oClaimHold.HoldReason;
                    txtHoldNoteMod.Select();
                    this.cmbHoldReason.SelectedIndexChanged -= new System.EventHandler(this.cmbHoldReason_SelectedIndexChanged);
                    cmbHoldReason.SelectedValue = _oClaimHold.HoldID;
                    this.cmbHoldReason.SelectedIndexChanged += new System.EventHandler(this.cmbHoldReason_SelectedIndexChanged);
        
                }
                else
                {
                    txtHoldNote.Select();
                    pnlBillingHold.Visible = true;
                    pnlOnBillingHold.Visible = false;
                }

                #region "Assigning Hold Message on Form"

                lblMsgHold.Text = "Claim " + _ClaimID + " will be put on Billing Hold. Insurance Billing will not proceed until the hold is released.";
               
                #endregion

                bReasonModified = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        } 

        #endregion
        private void FillComboBoxData(bool IsHold)
        {
            HoldBilling oHoldBilling = new HoldBilling(_databaseConnection);
            DataTable dtBillingHold = null;

            try
            {

                this.cmbReason.SelectedIndexChanged -= new System.EventHandler(this.cmbReason_SelectedIndexChanged);
                this.cmbHoldReason.SelectedIndexChanged -= new System.EventHandler(this.cmbHoldReason_SelectedIndexChanged);

                dtBillingHold = oHoldBilling.GetHoldBilling(0);
                if (dtBillingHold != null && dtBillingHold.Rows.Count > 0)
                {
                    DataRow row = dtBillingHold.NewRow();
                    row["sHoldBillingReason"] = "";
                    row["sHoldBillingDescription"] = "";
                    row["nHoldBillingID"] = 0;
                    dtBillingHold.Rows.InsertAt(row, 0);

                    if (!IsHold)
                    {
                        cmbReason.DataSource = dtBillingHold.Copy();
                        cmbReason.ValueMember = "nHoldBillingID";
                        cmbReason.DisplayMember = "sDescription";
                    }
                    else
                    {
                        cmbHoldReason.DataSource = dtBillingHold.Copy();
                        cmbHoldReason.ValueMember = "nHoldBillingID";
                        cmbHoldReason.DisplayMember = "sDescription";
                    }

                    //cmbLogAction.SelectedIndex = -1;
                }





                cmbReason.DrawMode = DrawMode.OwnerDrawFixed;
                cmbReason.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
                cmbHoldReason.DrawMode = DrawMode.OwnerDrawFixed;
                cmbHoldReason.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oHoldBilling != null) { oHoldBilling.Dispose(); }
                this.cmbReason.SelectedIndexChanged += new System.EventHandler(this.cmbReason_SelectedIndexChanged);
                this.cmbHoldReason.SelectedIndexChanged += new System.EventHandler(this.cmbHoldReason_SelectedIndexChanged);
            }
        }
        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            ComboBox combo = (ComboBox)sender;
            if (combo.Items.Count > 0 && e.Index >= 0)
            {

                e.DrawBackground();
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    if (combo.DroppedDown)
                    {
                        string txt = combo.GetItemText(combo.Items[e.Index]).ToString();


                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                        {
                            if (toolTip1.GetToolTip(combo) != txt)
                            {
                                this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                            }
                        }
                        else
                        {
                            this.toolTip1.SetToolTip(combo, "");
                        }
                    }
                    else
                    {
                        this.toolTip1.Hide(combo);
                    }
                }
                else
                {
                    //this.tooltip_Billing.SetToolTip(combo,"");
                }
                e.DrawFocusRectangle();
            }
        }
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }

            return width;
        }

        #region " Tool strip Events " 
        
        #region "Event fired while first time user is saving Claim On Hold"

        private void tls_btnSaveNClose_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtHoldNote.Text.Trim() == null || txtHoldNote.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Hold Reason", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoldNote.Text = "";
                    txtHoldNote.Focus();
                }
                else
                {

                    //DialogResult res = MessageBox.Show("Continue with Billing Hold?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (MessageBox.Show("Continue with Billing Hold?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        AssignDataToObject(txtHoldNote.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 

        #endregion

        #region "Event fired after clicking close button while putting Hold Information first time"

        private void tls_btnClose_Click(object sender, EventArgs e)
        {            
            DialogResult result;
            if (bReasonModified)
            {
                result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (txtHoldNote.Text.Trim() == null || txtHoldNote.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter Hold Reason", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtHoldNote.Text = "";
                        txtHoldNote.Focus();

                    }
                    else
                    {
                        AssignDataToObject(txtHoldNote.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    oDialogResult = false;
                    this.Close();
                }
            }
            else
            {
                oDialogResult = false;
                this.Close();
            }
            
        } 

        #endregion

        #region "Event fired while user is updating plan On Hold"

        private void tls_SaveAndCloseMod_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHoldNoteMod.Text.Trim() == null || txtHoldNoteMod.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Hold Note", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoldNoteMod.Text = "";
                    txtHoldNoteMod.Focus();

                }
                else
                {
                    //if (MessageBox.Show("Continue with Billing Hold?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{
                    AssignDataToObject(txtHoldNoteMod.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 

        #endregion

        #region "Event fired after clicking close button while modifying Hold Information "

        private void tls_CloseMod_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (bReasonModified)
            {
                result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (txtHoldNoteMod.Text.Trim() == null || txtHoldNoteMod.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter Hold Note", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtHoldNoteMod.Text = "";
                        txtHoldNoteMod.Focus();

                    }
                    else
                    {
                        AssignDataToObject(txtHoldNoteMod.Text.Trim());
                        oDialogResult = true;
                        this.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    oDialogResult = false;
                    this.Close();
                }
            }
            else
            {
                oDialogResult = false;
                this.Close();
            }
        } 

        #endregion

        #region " Commented Code"

        private void tls_saveMod_Click(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    if (txtHoldNoteMod.Text.Trim() == null || txtHoldNoteMod.Text.Trim() == "")
            ////    {
            ////        MessageBox.Show("Please enter Hold Note", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////        txtHoldNoteMod.Text = "";
            ////        txtHoldNoteMod.Focus();

            ////    }
            ////    else
            ////    {

            ////        if (_oClaimHold == null)
            ////        {
            ////            _oClaimHold = new ClaimHold();
            ////            _oClaimHold.HoldDateTime = DateTime.Now;
            ////        }

            ////        //_oClaimHold.ClinicID = _ClinicID;
            ////        _oClaimHold.HoldModDateTime = DateTime.Now;
            ////        _oClaimHold.HoldReason = txtHoldNoteMod.Text;
            ////        _oClaimHold.HoldUserID = _UserID;
            ////        _oClaimHold.TransactionID = _TransactionID;
            ////        _oClaimHold.TransactionMasterID = _TransactionMasterID;

            ////        lblHoldModDate.Text = _oClaimHold.HoldModDateTime.ToString("MM/dd/yyyy");
            ////        lblHoldModTime.Text = _oClaimHold.HoldModDateTime.ToString("hh:mm tt");
            ////        lblHoldModUser.Text = GetUserName(Convert.ToInt64(_oClaimHold.HoldUserID));
            ////        oDialogResult = true;
            ////        //this.Close();
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            ////}
        } 

        #endregion

        #region "Event fired while Release button is clicked "

        private void tls_Release_Click(object sender, EventArgs e)
        {
            try
            {
                if (sResPartyStatus.ToLower() == "Pending".ToLower())
                {
                    DialogResult res = MessageBox.Show("Claim is in Pending Status and will be released from Billing Hold. " + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        _oClaimHold.IsHold = false;
                        _oClaimHold.HoldModified = true;
                        _oClaimHold.HoldModDateTime = DateTime.Now;
                        _oClaimHold.HoldReason = txtHoldNoteMod.Text;
                        oDialogResult = true;
                        this.Close();
                    }
                }
                else
                {
                    DialogResult res = MessageBox.Show("Claim will be released from Billing Hold. " + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        _oClaimHold.IsHold = false;
                        _oClaimHold.HoldModified = true;
                        _oClaimHold.HoldModDateTime = DateTime.Now;
                        _oClaimHold.HoldReason = txtHoldNoteMod.Text;
                        oDialogResult = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        } 

        #endregion

       

        #endregion

        #region "Public & Private Methods"
        
        private string GetUserName(Int64 nUID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
            String _UserName = "";
            try
            {

                oDB.Connect(false);
                string strQuery = "";
                strQuery = " select ISNULL(sLoginName,'') As sUserName from User_MST WITH (NOLOCK) where nUserID= " + nUID + "";
                object _Result = oDB.ExecuteScalar_Query(strQuery);
                if (_Result != null && _Result.ToString() != "")
                {
                    _UserName = _Result.ToString();
                }
                return _UserName;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        #region "Assign/Update Data To Object"
        
        //Method used to assign/Update the _oClaimHold object
        private void AssignDataToObject(string sReason)
        {
           
            try
            {
                //checks whether the claim is on Hold if not create a new object 
                if (_oClaimHold.IsHold == false)
                {
                    _oClaimHold = new ClaimHold();
                    _oClaimHold.HoldDateTime = DateTime.Now;
                    _oClaimHold.HoldUserID = _UserID;
                    _oClaimHold.HoldID = Convert.ToInt64(cmbReason.SelectedValue);
                }
                else
                {
                    _oClaimHold.HoldID = Convert.ToInt64(cmbHoldReason.SelectedValue);
                }
                
               
                //_oClaimHold.ClinicID = _ClinicID;
                _oClaimHold.HoldModDateTime = DateTime.Now;
                _oClaimHold.HoldReason = sReason;
                _oClaimHold.TransactionID = _TransactionID;
                _oClaimHold.HoldModUserID = _UserID;
                _oClaimHold.TransactionMasterID = _TransactionMasterID;
                _oClaimHold.IsHold = true;
                _oClaimHold.HoldModified = true;
                
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        
        #endregion

        private void txtHoldNoteMod_TextChanged(object sender, EventArgs e)
        {
            bReasonModified = true;
        }

        #endregion

        #region " Form Button Click Events "

        private void txtHoldNote_TextChanged(object sender, EventArgs e)
        {
            bReasonModified = true;
        }

        #endregion

        private void cmbReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            HoldBilling oHoldBilling = new HoldBilling(_databaseConnection);
            DataTable dt = null;
            if (Convert.ToInt64(cmbReason.SelectedValue) != 0)
            {
                dt = oHoldBilling.GetHoldBilling(Convert.ToInt64(cmbReason.SelectedValue));
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (txtHoldNote.Text.Equals(""))
                    {
                        txtHoldNote.Text += dt.Rows[0]["sHoldBillingDescription"].ToString();
                    }
                    else
                    {
                        txtHoldNote.Text += " ; " + dt.Rows[0]["sHoldBillingDescription"].ToString();
                    }


                }
            }
            else
            {
                txtHoldNote.Text = "";

            }

            

              
        }

        private void cmbHoldReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            HoldBilling oHoldBilling = new HoldBilling(_databaseConnection);
            DataTable dt = null;
            if (Convert.ToInt64(cmbHoldReason.SelectedValue) != 0)
            {
                dt = oHoldBilling.GetHoldBilling(Convert.ToInt64(cmbHoldReason.SelectedValue));
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (txtHoldNoteMod.Text.Equals(""))
                    {
                        txtHoldNoteMod.Text += dt.Rows[0]["sHoldBillingDescription"].ToString();
                    }
                    else
                    {
                        txtHoldNoteMod.Text += " ; " + dt.Rows[0]["sHoldBillingDescription"].ToString();
                    }


                }
            }

            else
            {
                txtHoldNoteMod.Text = "";

            }

            }


        
            
             
            
    }

}
