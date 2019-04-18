using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmSetupClaimCategoryType : Form
    {

        #region "Variables"
        
        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
   
        #endregion "Variables"

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public string DatabaseConnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        public Int64 ClaimReportingCategoryID
        {
            get;
            set;
        }
        public string ClaimReportingCategoryCode
        {
            get;
            set;
        }
        public string ClaimReportingCategoryDesc
        {
            get;
            set;
        } 

        #endregion

        #region " Constructor "

       
        public frmSetupClaimCategoryType(Int64 nCliamCategoryTypeID, string DatabaseConnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionstring;
            ClaimReportingCategoryID = nCliamCategoryTypeID;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;

        }

        #endregion

        #region " Form Load Event "
        
        private void frmSetClaimCategoryType_Load(object sender, EventArgs e)
        {
            try
            {
                if (ClaimReportingCategoryID != 0)
                {
                    ClaimReportingCategory oClaimCategoryType = new ClaimReportingCategory(_databaseconnectionstring);
                    DataTable dtAdjustmentType;
                    dtAdjustmentType = oClaimCategoryType.GetClaimReportingCategory(ClaimReportingCategoryID);
                    if (oClaimCategoryType != null) { oClaimCategoryType.Dispose(); }

                    if (dtAdjustmentType != null)
                    {
                        if (dtAdjustmentType.Rows.Count != 0)
                        {
                            txtClaimCategoryDesc.Text = Convert.ToString(dtAdjustmentType.Rows[0]["sClaimReportingCategoryDescription"]);
                            txtClaimCategoryCode.Text = Convert.ToString(dtAdjustmentType.Rows[0]["sClaimReportingCategoryCode"]);
                            txtClaimCategoryDesc.Tag = Convert.ToString(dtAdjustmentType.Rows[0]["nClaimReportingCategoryID"]);
                            chkDeactivate.Checked = Convert.ToBoolean(dtAdjustmentType.Rows[0]["sStatus"]);   
                        }
                    }
                    tsb_Save.Visible = false; 
                }
                else
                {
                    txtClaimCategoryCode.Focus();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }
 
        #endregion

        #region " Tool Strip Button Click Event "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            if (SaveClaimCategoryType())
            {
                this.Close();
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveClaimCategoryType())
                {
                    txtClaimCategoryCode.Text = "";
                    txtClaimCategoryDesc.Text = "";
                    chkDeactivate.Checked = true;
                    ClaimReportingCategoryID = 0;
                    txtClaimCategoryCode.Focus();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Claim Reporting Category", 0, ClaimReportingCategoryID, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }

        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 
        #endregion

        #region "Private & Public Methods"

        private bool SaveClaimCategoryType()
        {
            ClaimReportingCategory oClaimCategoryType = new ClaimReportingCategory(_databaseconnectionstring);
            Boolean bisSaved = false;
            try
            {
                #region " Validate Data "

                if (txtClaimCategoryCode.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter claim reporting category code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClaimCategoryCode.Focus();
                    return false;
                }
                if (txtClaimCategoryDesc.Text.Trim() == "")
                {
                    MessageBox.Show(" Enter claim reporting category description.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClaimCategoryDesc.Focus();
                    return false;
                }

                #endregion

                oClaimCategoryType.ClaimReportingCategoryID = Convert.ToInt64(txtClaimCategoryDesc.Tag);
                oClaimCategoryType.ClaimReportingCategoryCode = txtClaimCategoryCode.Text.Trim();
                oClaimCategoryType.ClaimReportingCategoryDescription = txtClaimCategoryDesc.Text.Trim();
                oClaimCategoryType.bIsActive = chkDeactivate.Checked;

                if (ClaimReportingCategoryID == 0)
                {
                    if (oClaimCategoryType.IsClaimReportingCategoryExists() == true)
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    ClaimReportingCategoryID = oClaimCategoryType.AddClaimReportingCategory();
                    if (ClaimReportingCategoryID == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Claim Reporting Category", 0, ClaimReportingCategoryID, 0, ActivityOutCome.Failure);
                        MessageBox.Show("claim reporting category not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bisSaved = true;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Claim Reporting Category", 0, ClaimReportingCategoryID, 0, ActivityOutCome.Success);
                    }
                }
                else
                {
                    oClaimCategoryType.ClaimReportingCategoryID = ClaimReportingCategoryID;
                    if (oClaimCategoryType.IsClaimReportingCategoryExists())
                    {
                        MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    if (oClaimCategoryType.AddClaimReportingCategory() == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Claim Reporting Category", 0, ClaimReportingCategoryID, 0, ActivityOutCome.Failure);

                        MessageBox.Show("claim reporting category not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bisSaved = true;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Claim Reporting Category", 0, ClaimReportingCategoryID, 0, ActivityOutCome.Success);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.AdjustmentType, ActivityType.Add, "Add Claim Reporting Category", 0, ClaimReportingCategoryID, 0, ActivityOutCome.Failure);

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oClaimCategoryType != null) { oClaimCategoryType.Dispose(); }
            }
            return bisSaved;
        }

        #endregion
    }
}