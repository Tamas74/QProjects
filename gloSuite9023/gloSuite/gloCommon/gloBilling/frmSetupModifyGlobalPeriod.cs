using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmSetupModifyGlobalPeriod : Form
    {

        #region "Variable declaration"

        const int COL_NOTE_IMAGE = 5;
        const int COL_CLAIM_NO = 7;
        const int COL_TRANSACTION_MST_ID = 34;
      //  private int iSummarySelRow = 1;
        private int iChargesSelRow = 1;
      //  private int iPaymentSelRow = 1;
      //  private int iReserveSelRow = 1;

        public bool oDialogResult = false;

        gloGridListControl ogloGridListControl = null;

        string sPreviousCPT = string.Empty;
        string sPreviousCPTDesc = string.Empty;
        string sSaveMessage = string.Empty;


        string sOriginalCPT = string.Empty;
        DateTime dtOriginalStartDate;
        string sOriginalNote = string.Empty;



        #endregion

        Int64 GlobalperiodID { get; set; }

        public frmSetupModifyGlobalPeriod()
        {
            InitializeComponent();
        }

        public frmSetupModifyGlobalPeriod(Int64 nGlobalperiodID)
        {
            GlobalperiodID = nGlobalperiodID;
            InitializeComponent();
        }

        private void frmSetupModifyGlobalPeriod_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1FlexGridChargesClaims, false);
            DataSet _dsGlobalperiodDetails = null;
            clsGlobalPeriods oGP = new clsGlobalPeriods();

            try
            {
                ClearFormData();
                _dsGlobalperiodDetails = oGP.GetGlobalperiodDetails(GlobalperiodID);
                if (_dsGlobalperiodDetails != null)
                {
                    //Filling Insurance Plan Combo
                    if (_dsGlobalperiodDetails.Tables[0] != null && _dsGlobalperiodDetails.Tables[0].Rows.Count > 0)
                    {

                        cmbInsurance.DataSource = _dsGlobalperiodDetails.Tables[0];
                        cmbInsurance.DisplayMember = "sInsuranceName";
                        cmbInsurance.ValueMember = "nInsuranceID";

                        cmbInsurance.SelectedIndex = 0;
                        cmbInsurance.Refresh();

                    }

                    //Filling Provider Combo
                    //if (_dsGlobalperiodDetails.Tables[1] != null && _dsGlobalperiodDetails.Tables[1].Rows.Count > 0)
                    //{
                    //    DataRow _dr = _dsGlobalperiodDetails.Tables[1].NewRow();
                    //    _dr["sProviderName"] = "";
                    //    _dr["nProviderID"] = 0;
                    //    _dsGlobalperiodDetails.Tables[1].Rows.InsertAt(_dr, 0);

                    //    cmbProvider.DataSource = _dsGlobalperiodDetails.Tables[1];
                    //    cmbProvider.DisplayMember = "sProviderName";
                    //    cmbProvider.ValueMember = "nProviderID";

                    //    cmbProvider.SelectedIndex = 0;
                    //    cmbProvider.Refresh();

                    //}

                    DataTable dtProviders = null;
                    dtProviders = gloCharges.GetCachedAllProviders();

                    if (dtProviders != null)
                    {
                        if (dtProviders.Rows.Count > 0)
                        {
                            DataRow _dr = dtProviders.NewRow();
                            _dr["sProviderName"] = "";
                            _dr["nProviderID"] = 0;
                            dtProviders.Rows.InsertAt(_dr, 0);

                            cmbProvider.BeginUpdate();
                            cmbProvider.DataSource = dtProviders.Copy();
                            cmbProvider.ValueMember = dtProviders.Columns["nProviderID"].ColumnName;
                            cmbProvider.DisplayMember = dtProviders.Columns["sProviderName"].ColumnName;
                            cmbProvider.EndUpdate();
                            cmbProvider.SelectedIndex = 0;
                        }
                    }

                    this.cmbPeriodDays.TextChanged -= new System.EventHandler(this.cmbPeriodDays_TextChanged);
                    this.cmbPeriodDays.SelectedIndexChanged -= new System.EventHandler(this.cmbPeriodDays_SelectedIndexChanged);
                    FillPeriodDaysCombo();
                    if (_dsGlobalperiodDetails.Tables[1] != null && _dsGlobalperiodDetails.Tables[1].Rows.Count > 0)
                    {
                        lblPatient.Text = Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["sPatientCode"]) + " - " + Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["PatientName"]);
                        lblPatient.Tag = _dsGlobalperiodDetails.Tables[1].Rows[0]["nPatientID"];

                        cmbInsurance.SelectedValue = Convert.ToInt64(_dsGlobalperiodDetails.Tables[1].Rows[0]["nInsuranceID"]);
                        cmbProvider.SelectedValue = Convert.ToInt64(_dsGlobalperiodDetails.Tables[1].Rows[0]["nProviderID"]);
                        txtCpt.Text = Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["sCPT"]);
                        lblCPTDesc.Text = Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["sDescription"]);
                        dtpStartDate.Value = Convert.ToDateTime(_dsGlobalperiodDetails.Tables[1].Rows[0]["dtStartDate"]);
                        lblEndDate.Text = Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["dtEndDate"]);
                        cmbPeriodDays.Text = Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["nDays"]);
                        txtBillingReminder.Text = Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["sReminder"]);
                        txtNote.Text = Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["sNotes"]);

                        lblCreated.Text = Convert.ToDateTime(_dsGlobalperiodDetails.Tables[1].Rows[0]["dtCreatedDateTime"]).ToString("MM/dd/yyyy")
                            + "   " + Convert.ToDateTime(_dsGlobalperiodDetails.Tables[1].Rows[0]["dtCreatedDateTime"]).ToString("hh:mm tt")
                            + "   " + Convert.ToString(_dsGlobalperiodDetails.Tables[1].Rows[0]["CreatedUserName"]);



                        sOriginalCPT = txtCpt.Text;
                        dtOriginalStartDate = dtpStartDate.Value;
                        sOriginalNote = txtNote.Text;



                    }

                    if (_dsGlobalperiodDetails.Tables[2] != null && _dsGlobalperiodDetails.Tables[2].Rows.Count > 0)
                    {
                        FillClaim_Charges(_dsGlobalperiodDetails.Tables[2]);
                    }

                }
               
                CloseControl();

                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                // This method actually sets the order all the way down the control hierarchy.
                tom.SetTabOrder(scheme);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                this.cmbPeriodDays.TextChanged += new System.EventHandler(this.cmbPeriodDays_TextChanged);
                this.cmbPeriodDays.SelectedIndexChanged += new System.EventHandler(this.cmbPeriodDays_SelectedIndexChanged);
                if (oGP != null) { oGP.Dispose(); }
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
               // cmbPeriodDays.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        private void FillClaim_Charges(DataTable _dt)
        {
            DataView _dv = null;   
            int cntr;

            try
            {
                if (_dt.Rows.Count > 0)
                {
                    
                    gloGlobal.gloPMGlobal.SplitClaimColumn(_dt, _dt.Columns.IndexOf("SplitClaimNumber"));

                    _dv = _dt.DefaultView;
                    
                    this.c1FlexGridChargesClaims.DataSource = _dv;        
                    for (cntr = 0; cntr < (c1FlexGridChargesClaims.Rows.Count - 1); cntr++)
                    {
                        if (Convert.ToBoolean(Convert.ToString(c1FlexGridChargesClaims.GetData(cntr + 1, c1FlexGridChargesClaims.Cols["blnNoteFlag"].Index)) == "" ? 0 : c1FlexGridChargesClaims.GetData(cntr + 1, c1FlexGridChargesClaims.Cols["blnNoteFlag"].Index)))
                        {
                            System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                            this.c1FlexGridChargesClaims.SetCellImage(cntr + 1, COL_NOTE_IMAGE, imgFlag);
                        }
                        if (c1FlexGridChargesClaims.Rows.Count >= iChargesSelRow)
                            c1FlexGridChargesClaims.Row = iChargesSelRow;
                        else if (_dt.Rows.Count > 1)
                            c1FlexGridChargesClaims.Row = 1;
                        
                    }
                    FillClaimOnHold();
                }
                else
                {
                    _dv = _dt.DefaultView;                
                    this.c1FlexGridChargesClaims.DataSource = _dv;                
                    this.c1FlexGridChargesClaims.Rows[0].Visible = false;

                }             
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                this.c1FlexGridChargesClaims.Rows[0].Visible = false;             
            }           

        }

        private void FillClaim_Charges()
        {
            DataTable _dt = null;
            DataView _dv = null;          
            int cntr;
            clsGlobalPeriods oGP = new clsGlobalPeriods();
            try
            {
                _dt = oGP.GetGlobalperiodChargeDetails(GlobalperiodID);
                if (_dt.Rows.Count > 0)
                {
                    _dv = _dt.DefaultView;
                    this.c1FlexGridChargesClaims.DataSource = _dv;
                    for (cntr = 0; cntr < (c1FlexGridChargesClaims.Rows.Count - 1); cntr++)
                    {
                        if (Convert.ToBoolean(Convert.ToString(c1FlexGridChargesClaims.GetData(cntr + 1, c1FlexGridChargesClaims.Cols["blnNoteFlag"].Index)) == "" ? 0 : c1FlexGridChargesClaims.GetData(cntr + 1, c1FlexGridChargesClaims.Cols["blnNoteFlag"].Index)))
                        {
                            System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                            this.c1FlexGridChargesClaims.SetCellImage(cntr + 1, COL_NOTE_IMAGE, imgFlag);
                        }
                        if (c1FlexGridChargesClaims.Rows.Count >= iChargesSelRow)
                            c1FlexGridChargesClaims.Row = iChargesSelRow;
                        else if (_dt.Rows.Count > 1)
                            c1FlexGridChargesClaims.Row = 1;

                    }

                    FillClaimOnHold();
                }
                else
                {
                    _dv = _dt.DefaultView;
                    this.c1FlexGridChargesClaims.DataSource = _dv;
                    this.c1FlexGridChargesClaims.Rows[0].Visible = false;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                this.c1FlexGridChargesClaims.Rows[0].Visible = false;
            }
            finally
            {
                if (oGP != null) { oGP.Dispose(); }
            }

        }
        private void FillClaimOnHold()
        {

            try
            {
                DataTable dtClaimOnHold = new DataTable();
                clsGlobalPeriods oGP = new clsGlobalPeriods();
                dtClaimOnHold = oGP.GetClaimsOnHold(Convert.ToInt64(lblPatient.Tag));
                if (dtClaimOnHold.Rows.Count > 0)
                {
                    string[] strArrClaims = dtClaimOnHold.Rows[0]["ClaimNo"].ToString().Split(',');
                    for (int iClaimCount = 0; iClaimCount <= strArrClaims.Length - 1; iClaimCount++)
                    {
                        for (int iGridClaimCount = 1; iGridClaimCount <= c1FlexGridChargesClaims.Rows.Count - 1; iGridClaimCount++)
                        {
                            if (c1FlexGridChargesClaims.GetData(iGridClaimCount, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString() == strArrClaims[iClaimCount].ToString())
                            {

                                CellStyle csSubCol;

                             //   csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                                try
                                {
                                    if (c1FlexGridChargesClaims.Styles.Contains("SubCol"))
                                    {
                                        csSubCol = c1FlexGridChargesClaims.Styles["SubCol"];
                                    }
                                    else
                                    {
                                        csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                                        csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                                        csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                                        csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                        csSubCol.TextEffect = TextEffectEnum.Flat;
                                        csSubCol.ForeColor = Color.Red;

                                    }

                                }
                                catch
                                {
                                    csSubCol = c1FlexGridChargesClaims.Styles.Add("SubCol");
                                    csSubCol.TextAlign = TextAlignEnum.LeftCenter;
                                    csSubCol.BackColor = Color.FromArgb(255, 255, 255);
                                    csSubCol.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    csSubCol.TextEffect = TextEffectEnum.Flat;
                                    csSubCol.ForeColor = Color.Red;

                                }
                           
                                c1FlexGridChargesClaims.Rows[iGridClaimCount].Style = csSubCol;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {


            }
        }
        private void ClearFormData()
        {
            lblCreated.Text = string.Empty;
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
               

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            Int32 _nGlobalDays = 0;

            try
            {
                if (Int32.TryParse(cmbPeriodDays.Text, out _nGlobalDays))
                {
                    //if (_result >= 1)
                    //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result - 1).ToString("MM/dd/yyyy");
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
            return oGP.ValidateCPT(CPTCode);
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
                        MessageBox.Show("Please enter CPT", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    //else if (txtBillingReminder.Text.Trim() == null || txtBillingReminder.Text.Trim() == "")
                    //{
                    //    MessageBox.Show("Please enter Billing Reminder", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBillingReminder.Text = "";
                    //    txtBillingReminder.Focus();
                    //}
                    //else if (txtNote.Text.Trim() == null || txtNote.Text.Trim() == "")
                    //{
                    //    MessageBox.Show("Please enter Notes", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtNote.Text = "";
                    //    txtNote.Focus();
                    //}
                    else
                    {

                        if (sOriginalCPT != txtCpt.Text || dtOriginalStartDate.ToShortDateString()  != dtpStartDate.Value.ToShortDateString())
                        {
                            oDialogResult = oGP.SaveGlobalPeriod(GlobalperiodID, Convert.ToInt64(lblPatient.Tag), Convert.ToInt64(cmbInsurance.SelectedValue), txtCpt.Text.Trim(),
                                 dtpStartDate.Value, Convert.ToDateTime(lblEndDate.Text), Convert.ToInt32(cmbPeriodDays.Text), Convert.ToInt64(cmbProvider.SelectedValue),
                                 txtBillingReminder.Text.Trim(), txtNote.Text.Trim(), DateTime.Now, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName,
                                 ref sSaveMessage, false);

                            if (sSaveMessage != string.Empty)
                            {
                                result = MessageBox.Show("Duplicate Global Period Warning."
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + sSaveMessage
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "Modify Global Period?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                if (result == System.Windows.Forms.DialogResult.Yes)
                                {
                                    sSaveMessage = string.Empty;


                                    oDialogResult = oGP.SaveGlobalPeriod(GlobalperiodID, Convert.ToInt64(lblPatient.Tag), Convert.ToInt64(cmbInsurance.SelectedValue), txtCpt.Text.Trim(),
                                       dtpStartDate.Value, Convert.ToDateTime(lblEndDate.Text), Convert.ToInt32(cmbPeriodDays.Text), Convert.ToInt64(cmbProvider.SelectedValue),
                                       txtBillingReminder.Text.Trim(), txtNote.Text.Trim(), DateTime.Now, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName,
                                       ref sSaveMessage, true);

                                    this.Close();
                                }
                            }


                            else if (sSaveMessage == string.Empty && oDialogResult)
                            {
                                this.Close();
                            }
                        }
                        else if (sOriginalCPT == txtCpt.Text && dtOriginalStartDate == dtpStartDate.Value)
                        {
                            oDialogResult = oGP.SaveGlobalPeriod(GlobalperiodID, Convert.ToInt64(lblPatient.Tag), Convert.ToInt64(cmbInsurance.SelectedValue), txtCpt.Text.Trim(),
                            dtpStartDate.Value, Convert.ToDateTime(lblEndDate.Text), Convert.ToInt32(cmbPeriodDays.Text), Convert.ToInt64(cmbProvider.SelectedValue),
                            txtBillingReminder.Text.Trim(), txtNote.Text.Trim(), DateTime.Now, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName,
                            ref sSaveMessage, true);
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
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = MessageBox.Show("Do you want to save the changes?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

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

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            DataSet _dsgloBaldates = null;
            Int32 _nGlobalDays = 0;
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

                                    if (cmbInsurance.SelectedItem != null && cmbInsurance.SelectedItem.ToString().Trim() != "")
                                        _dsgloBaldates = oGP.GetPatientDetails(0, ogloGridListControl.SelectedItems[0].Code, Convert.ToInt64(((System.Data.DataRowView)(cmbInsurance.SelectedItem)).Row.ItemArray[0]));
                                    else
                                        _dsgloBaldates = oGP.GetPatientDetails(0, ogloGridListControl.SelectedItems[0].Code, 0);

                                    if (_dsgloBaldates != null)
                                    {
                                        if (_dsgloBaldates.Tables[0] != null && _dsgloBaldates.Tables[0].Rows.Count > 0)
                                        {
                                            dtpStartDate.Value = DateTime.Now;
                                            cmbPeriodDays.Text = Convert.ToString(_dsgloBaldates.Tables[0].Rows[0]["nPeriodDays"]);
                                            if (Int32.TryParse(Convert.ToString(_dsgloBaldates.Tables[0].Rows[0]["nPeriodDays"]), out _nGlobalDays))
                                            {
                                                //if (_result >= 1)
                                                //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result - 1).ToString("MM/dd/yyyy");
                                                //else
                                                //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result).ToString("MM/dd/yyyy");

                                                lblEndDate.Text = dtpStartDate.Value.AddDays(_nGlobalDays).ToString("MM/dd/yyyy");

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

        private void txtCpt_Enter(object sender, EventArgs e)
        {
            try
            {
                sPreviousCPT = txtCpt.Text.Trim();
                sPreviousCPTDesc = lblCPTDesc.Text.Trim();

                OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
                if (ogloGridListControl != null)
                {
                    string _strSearchString = txtCpt.Text.Trim();
                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
       

        private void c1GlobalPeriods_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                HitTestInfo hitInfo = this.c1FlexGridChargesClaims.HitTest(e.X, e.Y);
                if (c1FlexGridChargesClaims.Rows.Count > 1)
                {
                    if (hitInfo.Row != 0)
                    {
                        _IsModified = ModifyCharge();
                        if (_IsModified)
                        {
                            FillClaim_Charges();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Claim not available.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private Boolean ModifyCharge()
        {
            Boolean _IsModified = false;
            gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            gloAccountsV2.gloPatientFinancialViewV2 objPatFinacialView = new gloAccountsV2.gloPatientFinancialViewV2(Convert.ToInt64(this.lblPatient.Tag));
            try
            {
                Int64 ParamTransactionId = 0;
                Int64 PatientID = 0;              

                if (c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index) != null && Convert.ToString(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["nTransactionMSTID"].Index)) != "")
                {
                    DataSet dsPatFinView = new DataSet();

                    if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-")) >= 0)
                    {
                        if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["bIsVoid"].Index)) == 1)
                        {
                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), true);
                            PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));
                            _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                        }
                        else
                        {
                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(0, c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-"))), c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().Substring(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index).ToString().IndexOf("-") + 1), false);

                            PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));
                            if ( ParamTransactionId != 0)
                                _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                        }

                    }
                    else
                    {

                        if (Convert.ToInt32(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["bIsVoid"].Index)) == 1)
                        {
                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", true);

                            PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientID"].Index));
                            _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                        }
                        else
                        {
                            ParamTransactionId = objPatFinacialView.GetClaimTransactionID(Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)), "", false);

                            PatientID = Convert.ToInt64(c1FlexGridChargesClaims.GetData(c1FlexGridChargesClaims.RowSel, c1FlexGridChargesClaims.Cols["PatientId"].Index));
                            if ( ParamTransactionId != 0)
                                _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                        }
                    }
                }

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
            return _IsModified;
        }

        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                if (c1FlexGridChargesClaims.Rows.Count > 1)
                {
                    _IsModified = ModifyCharge();
                    if (_IsModified)
                    {
                        FillClaim_Charges();
                    }
                    frmSetupModifyGlobalPeriod_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Claim not available.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void txtDays_TextChanged(object sender, EventArgs e)
        {
            Int32 _nGlobalDays = 0;

            try
            {
                if (Int32.TryParse(cmbPeriodDays.Text, out _nGlobalDays))
                {
                    //if (_result >= 1)
                    //    lblEndDate.Text = dtpStartDate.Value.AddDays(_result - 1).ToString("MM/dd/yyyy");
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

        private void txtDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void cmbPeriodDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 _nGlobalDays = 0;
            try
            {
                if (Int32.TryParse(cmbPeriodDays.Text, out _nGlobalDays))
                {
                    //if (_result >= 1)
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

        private void cmbPeriodDays_TextChanged(object sender, EventArgs e)
        {
            Int32 _nGlobalDays = 0;
            try
            {
                if (Int32.TryParse(cmbPeriodDays.Text, out _nGlobalDays))
                {
                    //if (_result >= 1)
                    //   lblEndDate.Text = dtpStartDate.Value.AddDays(_result-1).ToString("MM/dd/yyyy");
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
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )|| e.KeyChar == '')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
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

        private void SetNoteFlag()
        {
            bool isNoteFlag = false;

            //c1FlexGridChargesClaims.FinishEditing();
            //c1FlexGridChargesClaims.Redraw = false;
            foreach (Row myRow in c1FlexGridChargesClaims.Rows)
            {
                isNoteFlag = false;

                if (myRow.Index == 0)
                { continue; }

                object noteFlag = c1FlexGridChargesClaims.GetData(myRow.Index, c1FlexGridChargesClaims.Cols.IndexOf("blnNoteFlag"));

                if (!noteFlag.Equals(System.DBNull.Value))
                {
                    if (Convert.ToBoolean(c1FlexGridChargesClaims.GetData(myRow.Index, c1FlexGridChargesClaims.Cols.IndexOf("blnNoteFlag"))))
                    { isNoteFlag = true; }
                }

                if (isNoteFlag)
                {
                    // Set the Note Image
                    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.Notes;
                    this.c1FlexGridChargesClaims.SetCellImage(myRow.Index, COL_NOTE_IMAGE, imgFlag);
                }
                else
                {
                    // Clear the Note Image
                    System.Drawing.Image imgFlag = global::gloBilling.Properties.Resources.None;
                    this.c1FlexGridChargesClaims.SetCellImage(myRow.Index, COL_NOTE_IMAGE, null);
                }

                noteFlag = null;
            }

            if (c1FlexGridChargesClaims.Rows.Count > 1)
            {
                if (c1FlexGridChargesClaims.Rows.Count >= iChargesSelRow)
                {
                    c1FlexGridChargesClaims.Row = iChargesSelRow;
                }
            }
            //c1FlexGridChargesClaims.Redraw = true;
            //c1FlexGridChargesClaims.StartEditing();
        }

        private void c1FlexGridChargesClaims_AfterSort(object sender, SortColEventArgs e)
        {

            if (c1FlexGridChargesClaims.Rows.Count > 1)
            {
                for (int i = 1; i <= c1FlexGridChargesClaims.Rows.Count - 1; i++)
                {
                    setNormalGridStyle(i);
                }
                SetNoteFlag();
                FillClaimOnHold();
            }

            if (e.Col == c1FlexGridChargesClaims.Cols["SplitClaimNumber"].Index)
            {
                c1FlexGridChargesClaims.Cols["SortClaim"].Sort = e.Order;
                c1FlexGridChargesClaims.Cols["SortSubClaim"].Sort = SortFlags.Ascending;
                c1FlexGridChargesClaims.Sort(
                    SortFlags.UseColSort, 
                    c1FlexGridChargesClaims.Cols["SortClaim"].Index, 
                    c1FlexGridChargesClaims.Cols["SortSubClaim"].Index
                    );
            }            
           
        }
        private void setNormalGridStyle(int rowNum)
        {
            CellStyle csSubCol1;
          //  csSubCol1 = c1FlexGridChargesClaims.Styles.Add("SubCol1");
            try
            {
                if (c1FlexGridChargesClaims.Styles.Contains("SubCol1"))
                {
                    csSubCol1 = c1FlexGridChargesClaims.Styles["SubCol1"];
                }
                else
                {
                    csSubCol1 = c1FlexGridChargesClaims.Styles.Add("SubCol1");
                    csSubCol1.TextAlign = TextAlignEnum.LeftCenter;
                    csSubCol1.BackColor = Color.FromArgb(255, 255, 255);
                    csSubCol1.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubCol1.TextEffect = TextEffectEnum.Flat;
                    csSubCol1.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

                }

            }
            catch
            {
                csSubCol1 = c1FlexGridChargesClaims.Styles.Add("SubCol1");
                csSubCol1.TextAlign = TextAlignEnum.LeftCenter;
                csSubCol1.BackColor = Color.FromArgb(255, 255, 255);
                csSubCol1.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubCol1.TextEffect = TextEffectEnum.Flat;
                csSubCol1.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            }
       
            c1FlexGridChargesClaims.Rows[rowNum].Style = csSubCol1;
        }

        private void c1FlexGridChargesClaims_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(c1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }




       
    }
}
