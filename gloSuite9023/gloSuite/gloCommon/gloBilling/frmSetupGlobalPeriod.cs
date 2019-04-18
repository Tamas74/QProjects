using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupGlobalPeriod : Form
    {
        #region "Variable declaration"

     //   bool _bInternalControlFocused = false;
        bool _bsaveForcefully = false;
        public bool oDialogResult = false;

        gloGridListControl ogloGridListControl = null;
      
        string sPreviousCPT = string.Empty;
        string sPreviousCPTDesc = string.Empty;
        string sSaveMessage = string.Empty;

        clsGlobalPeriods _objclsGlobalPeriods;
        #endregion

        #region "Properties"
        public Int64 PatientID { get; set; } 
        #endregion

        #region "Constructors"

        public frmSetupGlobalPeriod()
        {
            InitializeComponent();
        }

        public frmSetupGlobalPeriod(Int64 nPatientID)
        {
            InitializeComponent();
            PatientID = nPatientID;
        }

        public frmSetupGlobalPeriod(clsGlobalPeriods objclsGlobalPeriods)
        {
            InitializeComponent();
            _objclsGlobalPeriods = objclsGlobalPeriods;
        }

        #endregion

        #region " Form Events "

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillProviderData()
        {
            DataTable dtProviders = null;
            dtProviders = gloCharges.GetCachedProviders();

            if (dtProviders != null)
            {
                if (dtProviders.Rows.Count > 0)
                {
                    DataRow _dr = dtProviders.NewRow();
                    _dr["sProviderName"] = "";
                    _dr["nProviderID"] = 0;
                    dtProviders.Rows.InsertAt(_dr, 0);

                    cmbProvider.DataSource = dtProviders;
                    cmbProvider.DisplayMember = "sProviderName";
                    cmbProvider.ValueMember = "nProviderID";
                }
            }
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            DataSet _dsgloBaldates = null;
            Int32 _nglobalDays = 0;
            clsGlobalPeriods oGP = new clsGlobalPeriods();
            try
            {
                dtpStartDate.ValueChanged -= dtpStartDate_ValueChanged;
                if (ogloGridListControl.SelectedItems != null)
                {
                    if (ogloGridListControl.SelectedItems.Count > 0)
                    {
                        switch (ogloGridListControl.ControlType)
                        {
                            case gloGridListControlType.CPT:
                                {
                                    txtCpt.Text = ogloGridListControl.SelectedItems[0].Code;
                                    lblCPTDesc.Text = ogloGridListControl.SelectedItems[0].Description;

                                    if(cmbInsurance.SelectedItem != null &&   cmbInsurance.SelectedItem.ToString().Trim()  != "")                                     
                                        _dsgloBaldates = oGP.GetPatientDetails(0, ogloGridListControl.SelectedItems[0].Code, Convert.ToInt64(((System.Data.DataRowView)(cmbInsurance.SelectedItem)).Row.ItemArray[1]));
                                    else
                                        _dsgloBaldates = oGP.GetPatientDetails(0, ogloGridListControl.SelectedItems[0].Code, 0);

                                    if (_dsgloBaldates != null)
                                    {
                                        if (_dsgloBaldates.Tables[0] != null && _dsgloBaldates.Tables[0].Rows.Count > 0)
                                        {
                                            dtpStartDate.Value = DateTime.Now;
                                            cmbPeriodDays.Text = Convert.ToString(_dsgloBaldates.Tables[0].Rows[0]["nPeriodDays"]);
                                            if (Int32.TryParse(Convert.ToString(_dsgloBaldates.Tables[0].Rows[0]["nPeriodDays"]), out _nglobalDays))
                                            {
                                                //if (_result >= 1)
                                                //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result - 1).ToString("MM/dd/yyyy");
                                                //else
                                                //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result).ToString("MM/dd/yyyy");

                                                lblEndDate.Text = dtpStartDate.Value.AddDays(_nglobalDays).ToString("MM/dd/yyyy");
                                            }
                                            txtBillingReminder.Text = Convert.ToString(_dsgloBaldates.Tables[0].Rows[0]["sBillingReminder"]);
                                        }
                                        else
                                        {
                                            dtpStartDate.Value = DateTime.Now;
                                            cmbPeriodDays.Text = "";
                                            lblEndDate.Text = "";
                                            txtBillingReminder.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        dtpStartDate.Value = DateTime.Now;
                                        cmbPeriodDays.Text = "";
                                        lblEndDate.Text = "";
                                        txtBillingReminder.Text = "";
                                    }

                                    sPreviousCPT = txtCpt.Text.Trim();
                                    sPreviousCPTDesc = lblCPTDesc.Text.Trim();
                                    dtpStartDate.Focus();
                                    break;
                                }
                            default:
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oGP != null) { oGP.Dispose(); }
                dtpStartDate.ValueChanged += dtpStartDate_ValueChanged;
                CloseControl();
            }
        }

        private void frmSetupGlobalPeriod_Load(object sender, EventArgs e)
        {
            ClearFormData();
            FillPeriodDaysCombo();
            if (_objclsGlobalPeriods != null)
            {
                FillPatientGlobalPeriodDetails(_objclsGlobalPeriods);
            }
            else
            {
                FillPatientGlobalPeriodDetails();
            }
          
            txtCpt.Select();  
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);


        }

        private void txtCpt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {

                                    bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                                    if (_IsItemSelected)
                                    {
                                    }
                                }
                            }
                            e.SuppressKeyPress = false;
                        }
                        break;
                    case Keys.Delete:
                        {

                        }
                        break;
                    case Keys.Down:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    ogloGridListControl.Focus();
                                }
                            }
                        }
                        break;
                    case Keys.Escape:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    txtCpt.Text = sPreviousCPT;
                                    lblCPTDesc.Text = sPreviousCPTDesc;
                                    CloseControl();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void txtCpt_Enter(object sender, EventArgs e)
        {
               
            //try
            //{
            //    sPreviousCPT = txtCpt.Text.Trim();
            //    sPreviousCPTDesc = lblCPTDesc.Text.Trim();

            //    OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
            //    if (ogloGridListControl != null)
            //    {
            //        string _strSearchString = txtCpt.Text.Trim();
            //        ogloGridListControl.FillControl(_strSearchString);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //    ex = null;
            //    throw;
            //}
        }

        private void txtCpt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!pnlInternalControl.Visible)
                {
                    OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
                }
                if (ogloGridListControl != null)
                {
                    string _strSearchString = txtCpt.Text.Trim();
                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            Int32 _nGlobalDays = 0;

            try
            {
                if (Int32.TryParse(cmbPeriodDays.Text, out _nGlobalDays))
                {
                    //if (_result >= 1)
                    //   lblEndDate.Text = dtpStartDate.Value.AddDays(_result - 1).ToString("MM/dd/yyyy");
                    //else
                    //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result).ToString("MM/dd/yyyy");

                    lblEndDate.Text = dtpStartDate.Value.AddDays(_nGlobalDays).ToString("MM/dd/yyyy");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool ValidateCPT(string CPTCode)
        {
          clsGlobalPeriods oGP = new clsGlobalPeriods();
          return   oGP.ValidateCPT(CPTCode);  
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            DialogResult result;
            clsGlobalPeriods oGP = new clsGlobalPeriods();
            
            try
            {
                //result = MessageBox.Show("Do you want to save the changes?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                //if (result == DialogResult.Yes)
                //{
                    if (txtCpt.Text.Trim() == null || txtCpt.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter a CPT code.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCpt.Text = "";
                        txtCpt.Focus();
                    }
                    else if (cmbPeriodDays.Text.Trim() == null || cmbPeriodDays.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Days.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbPeriodDays.Text = "";
                        cmbPeriodDays.Focus();
                    }
                    else if (!ValidateCPT(txtCpt.Text.Trim()))
                    {
                        MessageBox.Show("Enter valid CPT code.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCpt.Focus();
                        //txtCpt.Text = "";
                       
                    }
                    else
                    {
                        oDialogResult = oGP.SaveGlobalPeriod(0, Convert.ToInt64(lblPatient.Tag), Convert.ToInt64(cmbInsurance.SelectedValue), txtCpt.Text.Trim(),
                            dtpStartDate.Value, Convert.ToDateTime(lblEndDate.Text), Convert.ToInt32(cmbPeriodDays.Text), Convert.ToInt64(cmbProvider.SelectedValue),
                            txtBillingReminder.Text.Trim(), txtNote.Text.Trim(), DateTime.Now, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName,
                            ref sSaveMessage, _bsaveForcefully);

                        if (sSaveMessage != string.Empty)
                        {
                            result = MessageBox.Show("Duplicate Global Period Warning."
                                + Environment.NewLine
                                + Environment.NewLine
                                + sSaveMessage
                                + Environment.NewLine
                                + Environment.NewLine
                                + "Create a new Global Period?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                sSaveMessage = string.Empty;
                                _bsaveForcefully = true;

                                oDialogResult = oGP.SaveGlobalPeriod(0, Convert.ToInt64(lblPatient.Tag), Convert.ToInt64(cmbInsurance.SelectedValue), txtCpt.Text.Trim(),
                                   dtpStartDate.Value, Convert.ToDateTime(lblEndDate.Text), Convert.ToInt32(cmbPeriodDays.Text), Convert.ToInt64(cmbProvider.SelectedValue),
                                   txtBillingReminder.Text.Trim(), txtNote.Text.Trim(), DateTime.Now, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName,
                                   ref sSaveMessage, _bsaveForcefully);

                                this.Close();
                            }

                        }
                        else if (sSaveMessage == string.Empty && oDialogResult)
                        {
                            this.Close();
                        }

                    }
                //}
                //else if (result == DialogResult.No)
                //{
                //    oDialogResult = false;
                //    this.Close();
                //}
            }
            catch (Exception ex)
            {
                oDialogResult = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oGP != null) { oGP.Dispose(); }
                sSaveMessage = string.Empty;
                _bsaveForcefully = false;
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult  result = MessageBox.Show("Do you want to save the changes?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    this.Close();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    tsb_OK_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //private void txtDays_TextChanged(object sender, EventArgs e)
        //{
        //    Int64 _result = 0;

        //    try
        //    {
        //        if (Int64.TryParse(cmbPeriodDays.Text, out _result))
        //        {
        //            lblEndDate.Text = dtpStartDate.Value.AddDays(_result).ToString("MM/dd/yyyy");
        //        }
        //        else
        //            lblEndDate.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        //private void txtDays_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //    else
        //    {
        //        e.Handled = false;
        //    }
        //} 

        #endregion

        #region "Public & Private Method"

        private void ClearFormData()
        {
            lblPatient.Text = string.Empty;
            lblCPTDesc.Text = string.Empty;
            cmbPeriodDays.Text = string.Empty;
            lblEndDate.Text = string.Empty;
            txtBillingReminder.Text = string.Empty;
            txtCpt.Text = string.Empty;
            txtNote.Text = string.Empty;
            dtpStartDate.ValueChanged -= dtpStartDate_ValueChanged;
            dtpStartDate.Value = DateTime.Now;
            dtpStartDate.ValueChanged += dtpStartDate_ValueChanged;
        }

        private void FillPatientGlobalPeriodDetails()
        {
            DataSet dsPatient = null;
            clsGlobalPeriods oGP = new clsGlobalPeriods();
            try
            {
                dsPatient = oGP.GetPatientDetails(PatientID, "",0);
                if (dsPatient != null && dsPatient.Tables.Count != 0)
                {
                    if (dsPatient.Tables[0] != null)
                    {
                        if (dsPatient.Tables[0].Rows.Count > 0)
                        {
                            //Filling Patient Code and Patient Name Field
                            if (dsPatient.Tables[0] != null && dsPatient.Tables[0].Rows.Count > 0)
                            {
                                lblPatient.Text = Convert.ToString(dsPatient.Tables[0].Rows[0]["sPatientCode"]) + " - " + Convert.ToString(dsPatient.Tables[0].Rows[0]["sPatientName"]);
                                lblPatient.Tag = dsPatient.Tables[0].Rows[0]["nPatientID"];
                            }

                            //Filling Insurance Plan Combo
                            if (dsPatient.Tables[1] != null && dsPatient.Tables[1].Rows.Count > 0)
                            {

                                cmbInsurance.DataSource = dsPatient.Tables[1];
                                cmbInsurance.DisplayMember = "sInsuranceName";
                                cmbInsurance.ValueMember = "nInsuranceID";

                                cmbInsurance.SelectedIndex = 0;
                                cmbInsurance.Refresh();

                            }


                          
                            FillProviderData(); 
                            cmbProvider.SelectedValue = dsPatient.Tables[0].Rows[0]["nProviderID"];
                            cmbProvider.Refresh();

                         
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oGP != null) { oGP.Dispose(); }
            }
        }

        private void FillPatientGlobalPeriodDetails(clsGlobalPeriods _objclsGlobalPeriods)
        {
            DataSet dsPatient = null;
            ///clsGlobalPeriods oGP = new clsGlobalPeriods();
            try
            {
                if (_objclsGlobalPeriods != null)
                {
                    dsPatient = _objclsGlobalPeriods.GetPatientDetails(_objclsGlobalPeriods.nPatientID, "", 0);

                    if (dsPatient != null)
                    {
                        //Filling Patient Code and Patient Name Field
                        if (dsPatient.Tables[0] != null && dsPatient.Tables[0].Rows.Count > 0)
                        {
                            lblPatient.Text = Convert.ToString(dsPatient.Tables[0].Rows[0]["sPatientCode"]) + " - " + Convert.ToString(dsPatient.Tables[0].Rows[0]["sPatientName"]);
                            lblPatient.Tag = dsPatient.Tables[0].Rows[0]["nPatientID"];
                        }

                        //Filling Insurance Plan Combo
                        if (dsPatient.Tables[1] != null && dsPatient.Tables[1].Rows.Count > 0)
                        {

                            cmbInsurance.DataSource = dsPatient.Tables[1];
                            cmbInsurance.DisplayMember = "sInsuranceName";
                            cmbInsurance.ValueMember = "nInsuranceID";

                        }

                        FillProviderData(); 
                    }

                    cmbInsurance.SelectedValue = _objclsGlobalPeriods.nInsuranceID ;
                    cmbProvider.SelectedValue = _objclsGlobalPeriods.nProviderID;
                    txtCpt.Text = _objclsGlobalPeriods.sCPT;
                    lblCPTDesc.Text = _objclsGlobalPeriods.sCPTDescription;
                    dtpStartDate.Value = _objclsGlobalPeriods.dtStartDate;
                    cmbPeriodDays.Text = Convert.ToString(_objclsGlobalPeriods.nDays);

                    if(Convert.ToDouble(cmbPeriodDays.Text) >=1 )
                       //lblEndDate.Text = _objclsGlobalPeriods.dtStartDate.AddDays(Convert.ToDouble(cmbPeriodDays.Text)-1).ToString("MM/dd/yyyy");
                       lblEndDate.Text = _objclsGlobalPeriods.dtStartDate.AddDays(Convert.ToDouble(cmbPeriodDays.Text)).ToString("MM/dd/yyyy");
                    else
                       lblEndDate.Text = _objclsGlobalPeriods.dtStartDate.ToString("MM/dd/yyyy");

                    txtBillingReminder.Text = _objclsGlobalPeriods.sReminder;
                    CloseControl();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
               // if (oGP != null) { oGP.Dispose(); }
            }
        }

        private bool CloseControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0;  i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                    }
                    catch { }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            { }
            return _result;
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {
                if (ogloGridListControl != null)
                {
                    CloseControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }

            return _result;
        }

        #endregion

        private void frmSetupGlobalPeriod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                if (pnlInternalControl.Visible)
                {
                    if (ogloGridListControl != null)
                    {
                        txtCpt.Text = sPreviousCPT;
                        lblCPTDesc.Text = sPreviousCPTDesc;
                        CloseControl();
                    }
                }
            }
        }

        private void cmbPeriodDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 _nGlobalDays = 0;
            try
            {
                if (Int32.TryParse(cmbPeriodDays.Text, out _nGlobalDays))
                {
                    lblEndDate.Text = dtpStartDate.Value.AddDays(_nGlobalDays).ToString("MM/dd/yyyy");
                }
                else
                    lblEndDate.Text = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbPeriodDays_TextChanged(object sender, EventArgs e)
        {
            Int32 _nGlobalDays = 0;
            try
            {
                if (Int32.TryParse(cmbPeriodDays.Text, out _nGlobalDays))
                {
                    //if(_result >= 1)
                    //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result - 1).ToString("MM/dd/yyyy");
                    //else
                    //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result).ToString("MM/dd/yyyy");

                    lblEndDate.Text = dtpStartDate.Value.AddDays(_nGlobalDays).ToString("MM/dd/yyyy");
                }
                else
                    lblEndDate.Text = "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbPeriodDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || e.KeyChar == '')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void FillPeriodDaysCombo()
        {
            DataTable _dt = new DataTable();

            try
            {
                DataColumn _dc = new DataColumn();
                _dc.ColumnName = "PeriodDays";
                _dc.DataType = typeof(System.String);
                _dt.Columns.Add(_dc);

                DataRow _dr = _dt.NewRow();
                _dr["PeriodDays"] = "";
                _dt.Rows.Add(_dr);

                _dr = _dt.NewRow();
                _dr["PeriodDays"] = "0";
                _dt.Rows.Add(_dr);

                _dr = _dt.NewRow();
                _dr["PeriodDays"] = "10";
                _dt.Rows.Add(_dr);

                _dr = _dt.NewRow();
                _dr["PeriodDays"] = "90";
                _dt.Rows.Add(_dr);

                _dt.AcceptChanges();

                cmbPeriodDays.DataSource = _dt;
                cmbPeriodDays.DisplayMember = "PeriodDays";
                cmbPeriodDays.ValueMember = "PeriodDays";
                cmbPeriodDays.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbPeriodDays_MouseDown(object sender, MouseEventArgs e)
        {
            
            
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    ContextMenu cntMenu = null;// new System.Windows.Forms.ContextMenu();
                    cmbPeriodDays.ContextMenu = cntMenu;
                }
            
        }

        private void frmSetupGlobalPeriod_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_objclsGlobalPeriods != null)
                _objclsGlobalPeriods.Dispose();
        }


    }
}
