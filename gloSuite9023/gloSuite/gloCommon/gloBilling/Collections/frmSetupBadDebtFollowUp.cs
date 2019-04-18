using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloGlobal;

namespace gloBilling.Collections
{
    public partial class frmSetupBadDebtFollowUp : Form
    {

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        Int64 _TemplateID = 0;

        #region " Property Procedures "

        private string DatabaseConnectionString{get;set;}

        public Int64 FollowUPCodeId { get; set; }

        private Int64 ClinicID { get; set; }

        public Int64 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }

        ComboBox combo;

        private string MessageBoxCaption { get; set; }

        public Boolean bIsUsed { get; set; }

        private string sPrevFollowupCode { get; set; }

        private string sPrevFollowupDescription { get; set; }
        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupBadDebtFollowUp(Int64 _FollowUPCodeId)
        {
            FollowUPCodeId = _FollowUPCodeId;
            InitializeComponent();
            ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            DatabaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            cmbPatAccDefFUAction.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPatAccDefFUAction.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            bIsUsed = false;
        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupClaimAccFollowUp_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = null;
            try
            {
                ContextMenu cm = null;//new ContextMenu();
                txtDefNextActiondays.ContextMenu = cm;
                FillFollowUpActions();
                FillFollowUpCode(FollowUPCodeId);
                tabSettings = new Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);
                //if (bIsUsed)
                //{
                //    txtCode.Enabled = false;
                //    txtDesc.Enabled = false;

                //}
                //else
                //{
                //    txtCode.Enabled = true;
                //    txtDesc.Enabled = true;

                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void tls_SetupResource_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            if (SaveFollowUpCode())
                            {
                                this.Close();
                            }
                        }
                        break;
                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    case "Save":
                        {
                            if (SaveFollowUpCode())
                            {
                                FollowUPCodeId = 0;
                                txtDesc.Text = "";
                                txtCode.Text = "";
                                cmbPatAccDefFUAction.SelectedIndex=0;
                                txtDefNextActiondays.Text= "";
                            }  
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Tool Strip Event "

        #region " Control Events "

        private void txtDefNextActiondays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void cmbPatAccDefFUAction_MouseEnter(object sender, EventArgs e)
        {
            if (cmbPatAccDefFUAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPatAccDefFUAction.Items[cmbPatAccDefFUAction.SelectedIndex])["sFollowUpAction"]), cmbPatAccDefFUAction) >= cmbPatAccDefFUAction.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbPatAccDefFUAction, Convert.ToString(((DataRowView)cmbPatAccDefFUAction.Items[cmbPatAccDefFUAction.SelectedIndex])["sFollowUpAction"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbPatAccDefFUAction, "");
                }
            }
        }

        private void cmbPatAccDefFUAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPatAccDefFUAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPatAccDefFUAction.Items[cmbPatAccDefFUAction.SelectedIndex])["sFollowUpAction"]), cmbPatAccDefFUAction) >= cmbPatAccDefFUAction.DropDownWidth - 20)
                {
                    this.toolTip1.SetToolTip(cmbPatAccDefFUAction, Convert.ToString(((DataRowView)cmbPatAccDefFUAction.Items[cmbPatAccDefFUAction.SelectedIndex])["sFollowUpAction"]));
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbPatAccDefFUAction, "");
                }
            }
        }

        #endregion " Control Events "

        #region " Private & Public Methods "

        private void FillFollowUpCode(Int64 Id)
        {

            CL_FollowUpCode oFollowUpCode = new CL_FollowUpCode();
            DataTable dtFollowUpCode = null;
            try
            {
                if (Id > 0)
                {
                    dtFollowUpCode = oFollowUpCode.GetFollowUpCode(Id,CollectionEnums.FollowUpType.BadDebt);
                    if (dtFollowUpCode != null && dtFollowUpCode.Rows.Count > 0)
                    {
                        txtDesc.Text = Convert.ToString(dtFollowUpCode.Rows[0]["sFollowUpActionDescription"]);
                        txtCode.Text = Convert.ToString(dtFollowUpCode.Rows[0]["sFollowUpActionCode"]);
                        sPrevFollowupCode = Convert.ToString(dtFollowUpCode.Rows[0]["sFollowUpActionCode"]);
                        sPrevFollowupDescription = Convert.ToString(dtFollowUpCode.Rows[0]["sFollowUpActionDescription"]);
                        cmbPatAccDefFUAction.SelectedValue = Convert.ToString(dtFollowUpCode.Rows[0]["sDefNextActionFollowUpCode"]);
                        txtDefNextActiondays.Text = Convert.ToString(dtFollowUpCode.Rows[0]["nFollowUpActionDays"]);
                        if (dtFollowUpCode.Rows[0]["nTemplateID"] != null && Convert.ToString(dtFollowUpCode.Rows[0]["nTemplateID"]) != "")
                        {
                            TemplateID = Convert.ToInt64(dtFollowUpCode.Rows[0]["nTemplateID"]);

                            DataTable _dtTemplate = new DataTable();
                            _dtTemplate = oFollowUpCode.GetTemplateName(TemplateID);
                            if (_dtTemplate != null && _dtTemplate.Rows.Count > 0)
                            {
                                txtDefaultTemplate.Text = Convert.ToString(_dtTemplate.Rows[0]["TemplateName"]);
                                txtDefaultTemplate.Tag = TemplateID;
                            }
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
                if (oFollowUpCode != null) { oFollowUpCode.Dispose(); }
                if (dtFollowUpCode != null) { dtFollowUpCode.Dispose(); }
            }
        }

        private void FillFollowUpActions()
        {
            try
            {
                CL_FollowUpCode clFollow = new CL_FollowUpCode();
                DataTable dtFollowUpActions = clFollow.getFollowUpAction(CollectionEnums.FollowUpType.BadDebt);
                if (dtFollowUpActions != null && dtFollowUpActions.Rows.Count > 0)
                {
                    DataRow dr = dtFollowUpActions.NewRow();
                    dr["sFollowUpActionCode"] = "";
                    dr["sFollowUpAction"] = "";
                    dtFollowUpActions.Rows.InsertAt(dr, 0);
                    cmbPatAccDefFUAction.BeginUpdate();
                    cmbPatAccDefFUAction.DataSource = dtFollowUpActions;
                    cmbPatAccDefFUAction.DisplayMember = dtFollowUpActions.Columns["sFollowUpAction"].ColumnName;
                    cmbPatAccDefFUAction.ValueMember = dtFollowUpActions.Columns["sFollowUpActionCode"].ColumnName;
                    cmbPatAccDefFUAction.EndUpdate();
                    //cmbPatAccDefFUAction.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private Boolean SaveFollowUpCode()
        {
            Int64 _tempresult = 0;
            Boolean _bResult = false;
            CL_FollowUpCode oFollowUpCode = new CL_FollowUpCode();
            string sNextActionCode = "";
            string sNextActionDesc = "";
            if (ValidateSave())
            {
                //Check if RevenueCode already exists if Yes do not add
                if (oFollowUpCode.IsExistsFollowUpCode(FollowUPCodeId, txtCode.Text.Trim(), CollectionEnums.FollowUpType.BadDebt))
                {
                    MessageBox.Show("Follow Up code is already in use by another entry.\n Enter unique Follow Up code.  ", MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return false;
                }
                if (cmbPatAccDefFUAction.Text.Trim() != "")
                {
                    sNextActionCode = Convert.ToString(cmbPatAccDefFUAction.SelectedValue).Trim();
                    sNextActionDesc = Convert.ToString(cmbPatAccDefFUAction.Text.Trim().Substring(cmbPatAccDefFUAction.Text.Trim().IndexOf("-")+1)).Trim();
                }
                //Modify
                if (FollowUPCodeId > 0)
                {

                    if (bIsUsed == true)
                    {
                        if (MessageBox.Show("Follow Up Code and Action is already used in Follow-up Queue do you want to modify it ?", MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {

                            _tempresult = oFollowUpCode.AddModifyFollowUpCode(FollowUPCodeId, txtCode.Text.Trim(), txtDesc.Text.Trim(), sNextActionCode, sNextActionDesc, txtDefNextActiondays.Text.Trim(), CollectionEnums.FollowUpType.BadDebt, TemplateID);
                            if (_tempresult > 0)
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Add, "Add Bad Debt Follow Up Code ", 0, _tempresult, 0, ActivityOutCome.Success);
                                oFollowUpCode.Dispose();
                                _bResult = true;

                                if (Convert.ToString(sPrevFollowupCode).Trim() != Convert.ToString(txtCode.Text).Trim() || Convert.ToString(sPrevFollowupDescription).Trim() != Convert.ToString(txtDesc.Text).Trim())
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Modify, "Modify Bad Debt Follow Up Code - Previous Code = " + sPrevFollowupCode + ", Current Code = " + txtCode.Text + ", Previous Description = " + sPrevFollowupDescription + ", Current Description = " + txtDesc.Text, 0, _tempresult, 0, ActivityOutCome.Success);
                                }

                            }
                            else
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Add, "Add Follow Up Code ", 0, _tempresult, 0, ActivityOutCome.Failure);
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }

                    else
                    {
                        _tempresult = oFollowUpCode.AddModifyFollowUpCode(FollowUPCodeId, txtCode.Text.Trim(), txtDesc.Text.Trim(), sNextActionCode, sNextActionDesc, txtDefNextActiondays.Text.Trim(), CollectionEnums.FollowUpType.BadDebt, TemplateID);
                        if (_tempresult > 0)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Add, "Add Bad Debt Follow Up Code ", 0, _tempresult, 0, ActivityOutCome.Success);
                            oFollowUpCode.Dispose();
                            _bResult = true;

                            if (Convert.ToString(sPrevFollowupCode).Trim() != Convert.ToString(txtCode.Text).Trim() || Convert.ToString(sPrevFollowupDescription).Trim() != Convert.ToString(txtDesc.Text).Trim())
                            {
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Modify, "Modify Bad Debt Follow Up Code - Previous Code = " + sPrevFollowupCode + ", Current Code = " + txtCode.Text + ", Previous Description = " + sPrevFollowupDescription + ", Current Description = " + txtDesc.Text, 0, _tempresult, 0, ActivityOutCome.Success);
                            }

                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Add, "Add Bad Debt Follow Up Code ", 0, _tempresult, 0, ActivityOutCome.Failure);
                        }
                    }
                  
                }
               
                else
                {
                    //Add
                    _tempresult = oFollowUpCode.AddModifyFollowUpCode(0, txtCode.Text.Trim(), txtDesc.Text.Trim(), sNextActionCode, sNextActionDesc, txtDefNextActiondays.Text.Trim(), CollectionEnums.FollowUpType.BadDebt, TemplateID);
                    FollowUPCodeId = _tempresult;
                    if (_tempresult > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Add, "Add Bad Debt Follow Up Code ", 0, _tempresult, 0, ActivityOutCome.Success);
                        oFollowUpCode.Dispose();
                        _bResult = true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.RCM, ActivityCategory.None, ActivityType.Add, "Add Bad Debt Follow Up Code ", 0, _tempresult, 0, ActivityOutCome.Failure);
                    }
                }

            }
            return _bResult;
        }

        private bool ValidateSave()
        {
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Follow Up code. ",MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return false; 
                }
                if (txtDesc.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Follow Up Description. ", MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDesc.Focus();
                    return false;
                }
                if (txtDefNextActiondays.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtDefNextActiondays.Text) > 200)
                    {
                        if (MessageBox.Show("Are you sure you want the days setting to be that large: " + txtDefNextActiondays.Text + "?", MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width=0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            combo = (ComboBox)sender;
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

        #endregion " Private & Public Methods "

        #region "Default Template Association"

        private void btnBrowseTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Height = 500;
                this.Width = 675;
                pnltlsStrip.Visible = false;
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch
                        {
                        }


                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }
                oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.Template, false, this.Width);
                oListControl.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                oListControl.ControlHeader = " Template Association";

                _CurrentControlType = gloListControl.gloListControlType.Template;
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

        private void btnClearTemplate_Click(object sender, EventArgs e)
        {
            txtDefaultTemplate.Text = "";
            txtDefaultTemplate.Tag = 0;
            TemplateID = 0;
        }

        #region "User control events"

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;
            try
            {
                switch (_CurrentControlType)
                {
                    case gloListControl.gloListControlType.Template:
                        {
                            txtDefaultTemplate.Text = "";
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

                                txtDefaultTemplate.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                                txtDefaultTemplate.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                                TemplateID = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            { this.Width = 473; this.Height = 231; pnltlsStrip.Visible = true; }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            { this.Width = 473; this.Height = 231; pnltlsStrip.Visible = true; }
        }

        #endregion

        #endregion "Default Template Association"

        
       
    }
}