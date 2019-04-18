using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using gloCommon;
using gloAuditTrail;

namespace gloContactsMerge
{
    public partial class frmContactsMerge : Form
    {

        #region "Variable Declaration"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private gloListControl.gloListControl oListControl;
        private Int64 _ClinicID = 0;
        private Int64 _userID = 0;
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";

        #endregion

        #region " Constractor and Destractor "

        public frmContactsMerge(string sDatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = sDatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _userID = Convert.ToInt64(appSettings["UserID"]); }
                else { _userID = 0; }
            }
            else
            { _userID = 0; }

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

        }

        #endregion

        #region " Form Load "

        private void frmContactsMerge_Load(object sender, EventArgs e)
        {
            string sNote = "Note: The bottom box is for the Plan record you wish to remain in the system." + Environment.NewLine + "All information currently associated with Plan Record 1 will be merged with Plan Record 2.";
            lblNote.Text = sNote;
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);
            ClearFields();
            tls.Focus();
        } 

        #endregion

        #region " Form Control Events"
        
        private void btnRemovePlanBrowse_Click(object sender, EventArgs e)
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
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Insurance Plans";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();
                oListControl.BringToFront();
                oListControl.Visible = true;
                oListControl.Dock = DockStyle.Fill;
                pnlMain.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void btnRemainPlanBrowse_Click(object sender, EventArgs e)
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
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, false, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Insurance Plans";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_Remain_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_Remain_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();
                oListControl.BringToFront();
                oListControl.Visible = true;
                oListControl.Dock = DockStyle.Fill;
                pnlMain.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnRemovePlanClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearRemovingPlanDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnRemainPlanClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearRemainPlanDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 

        #endregion

        #region "Tool Strip Button Events"

        private void tlsMerge_Click(object sender, EventArgs e)
        {
            MergeContacts oMergeContacts = new MergeContacts(_databaseconnectionstring);
            StringBuilder sMessage = new StringBuilder();

            try
            {

                if (txtRemovePlanName.Text == string.Empty)
                {
                    btnRemovePlanBrowse.Focus();
                    MessageBox.Show("Please select Plan records", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtRemainPlanName.Text == string.Empty)
                {
                    btnRemainPlanBrowse.Focus();
                    MessageBox.Show("Please select Plan records", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Convert.ToInt64(txtRemainPlanName.Tag) == Convert.ToInt64(txtRemovePlanName.Tag))
                {
                    MessageBox.Show("\"Plan Record 1\" and \"Plan Record 2\" should not be same", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    
                    sMessage.Append("Merging Insurance Plans is permanent.");
                    sMessage.Append(Environment.NewLine + "Insurance plan \"" + txtRemovePlanName.Text + "\" will be merged into \"" + txtRemainPlanName.Text + "\".");
                    sMessage.Append(Environment.NewLine + "All patients with the old insurance will be found under the remaining insurance.");
                    sMessage.Append(Environment.NewLine + "Continue?");

                    if (MessageBox.Show(sMessage.ToString(), _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                      
                        //Code for Merging.........
                        string sErrMsg = string.Empty;
                        if (oMergeContacts.MergeInsurancePlan(Convert.ToInt64(txtRemovePlanName.Tag), Convert.ToInt64(txtRemainPlanName.Tag), txtRemainPlanName.Text, txtRemovePlanName.Text,ref sErrMsg) == true)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Admin, ActivityCategory.Insurance, ActivityType.Delete, "Insurance Plan " + txtRemovePlanName.Text + "(" + txtRemovePlanName.Tag + ")" + " is Merged with " + txtRemainPlanName.Text + "(" + txtRemainPlanName.Tag + ") using merge tool", ActivityOutCome.Success);
                            ClearRemovingPlanDetails();
                            ReloadRemainInsurancePlan(Convert.ToInt64(txtRemainPlanName.Tag));
                            MessageBox.Show("Merge complete.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Admin, ActivityCategory.Insurance, ActivityType.Delete, "Insurance Plan " + txtRemovePlanName.Text + "(" + txtRemovePlanName.Tag + ")" + " is Merged with " + txtRemainPlanName.Text + "(" + txtRemainPlanName.Tag + ") using merge tool", ActivityOutCome.Failure);
                            MessageBox.Show("Error while Merging." + Environment.NewLine + sErrMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        this.Cursor = Cursors.Default;

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (oMergeContacts != null) { oMergeContacts.Dispose(); }
            }
        }

        private void tlsRemove_Click(object sender, EventArgs e)
        {
            StringBuilder sMessage = new StringBuilder();
            MergeContacts oMergeContacts = new MergeContacts(_databaseconnectionstring);
            DataTable _dtInsPlan = oMergeContacts.GetDuplicatePlans();
            StringBuilder sPlanName = new StringBuilder();
            DataTable _dtFiltered;
                       
            try
            {
                if (_dtInsPlan != null)
                {
                    if (_dtInsPlan.Rows.Count > 0)
                    {                             
                        _dtFiltered = SelectDistinct("", _dtInsPlan, "sName");
                                            
                        sMessage.Append("Removing Insurance Plans is permanent.");
                        sMessage.Append(Environment.NewLine + "All duplicate insurance plans (by Name) that have not been assigned to any patient will be removed from the system, leaving behind a single insurance plan.");
                        sMessage.Append(Environment.NewLine + _dtFiltered.Rows.Count + " plan names found with duplicate insurance plans:");
                        sMessage.Append(Environment.NewLine + Environment.NewLine);

                        for (int iCount = 0; iCount <= _dtFiltered.Rows.Count - 1; iCount++)
                        {
                            if (iCount <= 30)
                            {
                                if (sPlanName.Length==0)
                                {
                                    sPlanName.Append(Convert.ToString(_dtFiltered.Rows[iCount]["sName"]));
                                }
                                else
                                {
                                    sPlanName.Append(", " + Convert.ToString(_dtFiltered.Rows[iCount]["sName"]));
                                }
                            }
                            else
                            {
                                sPlanName.Append("....");
                                break;
                            }

                        }
                        
                        sMessage.Append(sPlanName);
                        sMessage.Append(Environment.NewLine + Environment.NewLine + "Continue?");

                        if (MessageBox.Show(sMessage.ToString(), _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            //Code for Removing Duplicate Plan.........
                            sPlanName = new StringBuilder();
                            for (int iCount = 0; iCount <= _dtInsPlan.Rows.Count - 1; iCount++)
                            {
                                if (sPlanName.Length == 0)
                                {
                                    sPlanName.Append(Convert.ToString(_dtInsPlan.Rows[iCount]["sName"]) + "(" + Convert.ToString(_dtInsPlan.Rows[iCount]["nContactID"]) + ")");
                                }
                                else
                                {
                                    sPlanName.Append(", " + Convert.ToString(_dtInsPlan.Rows[iCount]["sName"]) + "(" + Convert.ToString(_dtInsPlan.Rows[iCount]["nContactID"]) + ")");
                                }
                                
                            }

                            Int64 nDeletedCount = oMergeContacts.RemoveDuplicateInsurancePlan();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Admin, ActivityCategory.Insurance, ActivityType.Delete, nDeletedCount + " unused insurance plans removed using merge tool named " + sPlanName.ToString() + ".", ActivityOutCome.Success);
                            MessageBox.Show("Removal complete." + Environment.NewLine + nDeletedCount + " insurance plans removed.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Cursor = Cursors.Default;
                        }

                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Admin, ActivityCategory.Insurance, ActivityType.Delete, "Zero duplicate insurance plans found.", ActivityOutCome.Failure);
                        MessageBox.Show("Zero duplicate insurance plans found.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Admin, ActivityCategory.Insurance, ActivityType.Delete, "Zero duplicate insurance plans found.", ActivityOutCome.Failure);
                    MessageBox.Show("Zero duplicate insurance plans found.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (oMergeContacts != null) { oMergeContacts.Dispose(); }
            }

        }
           
        private void tlsCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion
        
        #region " List Control Events "

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                            pnlMain.Visible = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            MergeContacts oMergeContacts = new MergeContacts(_databaseconnectionstring);

            try
            {
                ClearRemovingPlanDetails();
                pnlMain.Visible = true;
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        txtRemovePlanName.Tag = oListControl.SelectedItems[i].ID;
                        txtRemovePlanName.Text = oListControl.SelectedItems[i].Description;

                        DataTable dtContacts = oMergeContacts.GetContactDetails(oListControl.SelectedItems[i].ID);

                        if (dtContacts != null)
                        {
                            if (dtContacts != null)
                            {
                                lblRemovePlanPayerID.Text = Convert.ToString(dtContacts.Rows[0]["sPayerId"]);
                                lblRemovePlanNoofPatient.Text = Convert.ToString(dtContacts.Rows[0]["nPatientCount"]);
                               
                                string sAddress = String.Empty;

                                if (Convert.ToString(dtContacts.Rows[0]["sAddressLine1"]) != String.Empty)
                                {
                                    sAddress = Convert.ToString(dtContacts.Rows[0]["sAddressLine1"]);
                                }

                                if (Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]) != String.Empty)
                                {
                                    if (sAddress != String.Empty)
                                    {
                                        sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]);
                                    }
                                    else
                                    {
                                        sAddress += Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]);
                                    }
                                }

                                if (Convert.ToString(dtContacts.Rows[0]["sCity"]) != String.Empty)
                                {
                                    if (sAddress != String.Empty)
                                    {
                                        sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sCity"]);
                                    }
                                    else
                                    {
                                        sAddress += Convert.ToString(dtContacts.Rows[0]["sCity"]);
                                    }
                                }

                                if (Convert.ToString(dtContacts.Rows[0]["sState"]) != String.Empty)
                                {
                                    if (sAddress != String.Empty)
                                    {
                                        sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sState"]);
                                    }
                                    else
                                    {
                                        sAddress += Convert.ToString(dtContacts.Rows[0]["sState"]);
                                    }
                                }

                                if (Convert.ToString(dtContacts.Rows[0]["sZIP"]) != String.Empty)
                                {
                                    if (sAddress != String.Empty)
                                    {
                                        sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sZIP"]);
                                    }
                                    else
                                    {
                                        sAddress += Convert.ToString(dtContacts.Rows[0]["sZIP"]);
                                    }
                                }

                                lblRemovePlanAddress.Text = sAddress;
                                lblRemovePlanPhoneNo.Text = Convert.ToString(dtContacts.Rows[0]["sPhone"]);
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
                if (oMergeContacts != null) { oMergeContacts.Dispose(); }
            }
        }

        private void oListControl_Remain_ItemClosedClick(object sender, EventArgs e)
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
                            pnlMain.Visible = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void oListControl_Remain_ItemSelectedClick(object sender, EventArgs e)
        {
            MergeContacts oMergeContacts = new MergeContacts(_databaseconnectionstring);

            try
            {
                ClearRemainPlanDetails();
                pnlMain.Visible = true;
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        txtRemainPlanName.Tag = oListControl.SelectedItems[i].ID;
                        txtRemainPlanName.Text = oListControl.SelectedItems[i].Description;

                        DataTable dtContacts = oMergeContacts.GetContactDetails(oListControl.SelectedItems[i].ID);

                        if (dtContacts.Rows.Count > 0 && dtContacts != null)
                        {
                            lblRemainPlanPayerID.Text = Convert.ToString(dtContacts.Rows[0]["sPayerId"]);
                            lblRemainPlanNoofPatient.Text = Convert.ToString(dtContacts.Rows[0]["nPatientCount"]);

                            string sAddress = String.Empty;

                            if (Convert.ToString(dtContacts.Rows[0]["sAddressLine1"]) != String.Empty)
                            {
                                sAddress = Convert.ToString(dtContacts.Rows[0]["sAddressLine1"]);
                            }

                            if (Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]) != String.Empty)
                            {
                                if (sAddress != String.Empty)
                                {
                                    sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]);
                                }
                                else
                                {
                                    sAddress += Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]);
                                }
                            }

                            if (Convert.ToString(dtContacts.Rows[0]["sCity"]) != String.Empty)
                            {
                                if (sAddress != String.Empty)
                                {
                                    sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sCity"]);
                                }
                                else
                                {
                                    sAddress += Convert.ToString(dtContacts.Rows[0]["sCity"]);
                                }
                            }

                            if (Convert.ToString(dtContacts.Rows[0]["sState"]) != String.Empty)
                            {
                                if (sAddress != String.Empty)
                                {
                                    sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sState"]);
                                }
                                else
                                {
                                    sAddress += Convert.ToString(dtContacts.Rows[0]["sState"]);
                                }
                            }

                            if (Convert.ToString(dtContacts.Rows[0]["sZIP"]) != String.Empty)
                            {
                                if (sAddress != String.Empty)
                                {
                                    sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sZIP"]);
                                }
                                else
                                {
                                    sAddress += Convert.ToString(dtContacts.Rows[0]["sZIP"]);
                                }
                            }

                            lblRemainPlanAddress.Text = sAddress;
                            lblRemainPlanPhoneNo.Text = Convert.ToString(dtContacts.Rows[0]["sPhone"]);
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
                if (oMergeContacts != null) { oMergeContacts.Dispose(); }
            }
        } 

        #endregion
              
        #region " Private Method "

        public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        {
            DataTable dt = new DataTable(TableName);
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
           
            return dt;
        }

        private bool ColumnEqual(object A, object B)
        {          
            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is DBNull.Value
                return false;
            return (A.Equals(B));  // value type standard comparison
        }

        private int getWidthofListItems(string _text, Label label)
        {

            Graphics g = this.CreateGraphics();
            SizeF s = g.MeasureString(_text, label.Font);
            int width = Convert.ToInt32(s.Width);
            return width;
        }

        private int getWidthofListItems(string _text, TextBox txt)
        {

            Graphics g = this.CreateGraphics();
            SizeF s = g.MeasureString(_text, txt.Font);
            int width = Convert.ToInt32(s.Width);
            return width;
        }

        private void ClearFields()
        {
            try
            {
                ClearRemainPlanDetails();
                ClearRemovingPlanDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void ClearRemovingPlanDetails()
        {
            try
            {
                txtRemovePlanName.Text = String.Empty;

                lblRemovePlanAddress.Text = String.Empty;
                lblRemovePlanAddress.BorderStyle = BorderStyle.None;

                lblRemovePlanNoofPatient.Text = String.Empty;
                lblRemovePlanNoofPatient.BorderStyle = BorderStyle.None;

                lblRemovePlanPayerID.Text = String.Empty;
                lblRemovePlanPayerID.BorderStyle = BorderStyle.None;

                lblRemovePlanPhoneNo.Text = String.Empty;
                lblRemovePlanPhoneNo.BorderStyle = BorderStyle.None;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ClearRemainPlanDetails()
        {
            try
            {

                txtRemainPlanName.Text = String.Empty;

                lblRemainPlanAddress.Text = String.Empty;
                lblRemainPlanAddress.BorderStyle = BorderStyle.None;

                lblRemainPlanNoofPatient.Text = String.Empty;
                lblRemainPlanNoofPatient.BorderStyle = BorderStyle.None;

                lblRemainPlanPayerID.Text = String.Empty;
                lblRemainPlanPayerID.BorderStyle = BorderStyle.None;

                lblRemainPlanPhoneNo.Text = String.Empty;
                lblRemainPlanPhoneNo.BorderStyle = BorderStyle.None;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ReloadRemainInsurancePlan(Int64 nContactID)
        {
            MergeContacts oMergeContacts = new MergeContacts(_databaseconnectionstring);

            try
            {
                DataTable dtContacts = oMergeContacts.GetContactDetails(nContactID);

                if (dtContacts.Rows.Count > 0 && dtContacts != null)
                {
                    lblRemainPlanPayerID.Text = Convert.ToString(dtContacts.Rows[0]["sPayerId"]);
                    lblRemainPlanNoofPatient.Text = Convert.ToString(dtContacts.Rows[0]["nPatientCount"]);

                    string sAddress = String.Empty;

                    if (Convert.ToString(dtContacts.Rows[0]["sAddressLine1"]) != String.Empty)
                    {
                        sAddress = Convert.ToString(dtContacts.Rows[0]["sAddressLine1"]);
                    }

                    if (Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]) != String.Empty)
                    {
                        if (sAddress != String.Empty)
                        {
                            sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]);
                        }
                        else
                        {
                            sAddress += Convert.ToString(dtContacts.Rows[0]["sAddressLine2"]);
                        }
                    }

                    if (Convert.ToString(dtContacts.Rows[0]["sCity"]) != String.Empty)
                    {
                        if (sAddress != String.Empty)
                        {
                            sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sCity"]);
                        }
                        else
                        {
                            sAddress += Convert.ToString(dtContacts.Rows[0]["sCity"]);
                        }
                    }

                    if (Convert.ToString(dtContacts.Rows[0]["sState"]) != String.Empty)
                    {
                        if (sAddress != String.Empty)
                        {
                            sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sState"]);
                        }
                        else
                        {
                            sAddress += Convert.ToString(dtContacts.Rows[0]["sState"]);
                        }
                    }

                    if (Convert.ToString(dtContacts.Rows[0]["sZIP"]) != String.Empty)
                    {
                        if (sAddress != String.Empty)
                        {
                            sAddress += Environment.NewLine + Convert.ToString(dtContacts.Rows[0]["sZIP"]);
                        }
                        else
                        {
                            sAddress += Convert.ToString(dtContacts.Rows[0]["sZIP"]);
                        }
                    }

                    lblRemainPlanAddress.Text = sAddress;
                    lblRemainPlanPhoneNo.Text = Convert.ToString(dtContacts.Rows[0]["sPhone"]);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oMergeContacts != null) { oMergeContacts.Dispose(); }
            }
        } 

        #endregion

        #region " Textbox & Lable Tooltip Code"

        private void lblRemovePlanPayerID_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                if (lblRemovePlanPayerID.Text != null && lblRemovePlanPayerID.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemovePlanPayerID.Text), lblRemovePlanPayerID) >= lblRemovePlanPayerID.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemovePlanPayerID, lblRemovePlanPayerID.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemovePlanPayerID);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemovePlanPayerID);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblRemovePlanPayerID_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (lblRemovePlanPayerID.Text != null && lblRemovePlanPayerID.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemovePlanPayerID.Text), lblRemovePlanPayerID) >= lblRemovePlanPayerID.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemovePlanPayerID, lblRemovePlanPayerID.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemovePlanPayerID);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemovePlanPayerID);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }
                
        private void lblRemovePlanPayerID_MouseLeave(object sender, EventArgs e)
        {
            this.tooltip_MergePlan.Hide(lblRemovePlanPayerID);
        }

        private void lblRemovePlanAddress_MouseLeave(object sender, EventArgs e)
        {
            this.tooltip_MergePlan.Hide(lblRemovePlanAddress);
        }

        private void lblRemovePlanAddress_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (lblRemovePlanAddress.Text != null && lblRemovePlanAddress.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemovePlanAddress.Text), lblRemovePlanAddress) >= lblRemovePlanAddress.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemovePlanAddress, lblRemovePlanAddress.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemovePlanAddress);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemovePlanAddress);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblRemovePlanAddress_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                if (lblRemovePlanAddress.Text != null && lblRemovePlanAddress.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemovePlanAddress.Text), lblRemovePlanAddress) >= lblRemovePlanAddress.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemovePlanAddress, lblRemovePlanAddress.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemovePlanAddress);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemovePlanAddress);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblRemainPlanPayerID_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                if (lblRemainPlanPayerID.Text != null && lblRemainPlanPayerID.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemainPlanPayerID.Text), lblRemainPlanPayerID) >= lblRemainPlanPayerID.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemainPlanPayerID, lblRemainPlanPayerID.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemainPlanPayerID);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemainPlanPayerID);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblRemainPlanPayerID_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (lblRemainPlanPayerID.Text != null && lblRemainPlanPayerID.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemainPlanPayerID.Text), lblRemainPlanPayerID) >= lblRemainPlanPayerID.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemainPlanPayerID, lblRemainPlanPayerID.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemainPlanPayerID);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemainPlanPayerID);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblRemainPlanPayerID_MouseLeave(object sender, EventArgs e)
        {
            this.tooltip_MergePlan.Hide(lblRemainPlanPayerID);
        }

        private void lblRemainPlanAddress_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                if (lblRemainPlanAddress.Text != null && lblRemainPlanAddress.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemainPlanAddress.Text), lblRemainPlanAddress) >= lblRemainPlanAddress.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemainPlanAddress, lblRemainPlanAddress.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemainPlanAddress);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemainPlanAddress);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblRemainPlanAddress_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (lblRemainPlanAddress.Text != null && lblRemainPlanAddress.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblRemainPlanAddress.Text), lblRemainPlanAddress) >= lblRemainPlanAddress.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(lblRemainPlanAddress, lblRemainPlanAddress.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(lblRemainPlanAddress);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(lblRemainPlanAddress);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void lblRemainPlanAddress_MouseLeave(object sender, EventArgs e)
        {
            this.tooltip_MergePlan.Hide(lblRemainPlanAddress);
        }

        private void txtRemovePlanName_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                if (txtRemovePlanName.Text != null && txtRemovePlanName.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(txtRemovePlanName.Text), txtRemovePlanName) >= txtRemovePlanName.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(txtRemovePlanName, txtRemovePlanName.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(txtRemovePlanName);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(txtRemovePlanName);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void txtRemovePlanName_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (txtRemovePlanName.Text != null && txtRemovePlanName.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(txtRemovePlanName.Text), txtRemovePlanName) >= txtRemovePlanName.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(txtRemovePlanName, txtRemovePlanName.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(txtRemovePlanName);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(txtRemovePlanName);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void txtRemovePlanName_MouseLeave(object sender, EventArgs e)
        {
            this.tooltip_MergePlan.Hide(txtRemovePlanName);
        }

        private void txtRemainPlanName_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                if (txtRemainPlanName.Text != null && txtRemainPlanName.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(txtRemainPlanName.Text), txtRemainPlanName) >= txtRemainPlanName.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(txtRemainPlanName, txtRemainPlanName.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(txtRemainPlanName);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(txtRemainPlanName);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void txtRemainPlanName_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                if (txtRemainPlanName.Text != null && txtRemainPlanName.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(txtRemainPlanName.Text), txtRemainPlanName) >= txtRemainPlanName.Width - 20)
                    {
                        tooltip_MergePlan.SetToolTip(txtRemainPlanName, txtRemainPlanName.Text);
                    }
                    else
                    {
                        this.tooltip_MergePlan.Hide(txtRemainPlanName);
                    }
                }
                else
                {
                    this.tooltip_MergePlan.Hide(txtRemainPlanName);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void txtRemainPlanName_MouseLeave(object sender, EventArgs e)
        {
            this.tooltip_MergePlan.Hide(txtRemainPlanName);
        }

        #endregion
               
    }
}
