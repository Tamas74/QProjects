using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling.Collections
{
    public partial class frmSelectPaymentcollectionAgency : Form
    {
        public long ContactId_Collection { get; set; }
        private string strConString="";       
        private DataTable dtCollectionAgency = null;
        gloContacts.clsCollectionAgency oCollection = null;
        private string _Messageboxcaption = "gloPM";

        public frmSelectPaymentcollectionAgency()
        {
            InitializeComponent();
        }

        public frmSelectPaymentcollectionAgency(string databaseConnectionstring,DataTable dtCollectionAgency)
        {
            InitializeComponent();
            strConString = databaseConnectionstring;
            oCollection = new gloContacts.clsCollectionAgency(strConString);
            this.dtCollectionAgency = dtCollectionAgency;
            _Messageboxcaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        }
        
        private void frmSelectPaymentcollectionAgency_Load(object sender, EventArgs e)
        {
            if (dtCollectionAgency != null)
            {
                DataRow dr = dtCollectionAgency.NewRow();
                try
                {

                    dtCollectionAgency.Rows.InsertAt(dr, 0);
                    dtCollectionAgency.Rows[0][0] = 0;
                    dtCollectionAgency.Rows[0][1] = "";
                    dtCollectionAgency.AcceptChanges();
                    cmbCollectionAgency.DataSource = null;
                    cmbCollectionAgency.DataSource = dtCollectionAgency.DefaultView;
                    cmbCollectionAgency.DisplayMember = dtCollectionAgency.Columns["sName"].ColumnName;
                    cmbCollectionAgency.ValueMember = dtCollectionAgency.Columns["nContactID"].ColumnName;

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (dr != null)
                    {
                        dr = null;
                    }
                }
            }
        }
            
        

        private void chkIsCollectionAgencyPost_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsCollectionAgencyPost.Checked)
            {
                cmbCollectionAgency.Enabled = true; 
            }
            else
            {
                cmbCollectionAgency.SelectedIndex = 0;
                cmbCollectionAgency.Enabled = false ; 
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkIsCollectionAgencyPost.Checked)
            {
                if (Convert.ToInt64(cmbCollectionAgency.SelectedValue) > 0)
                {

                    ContactId_Collection = Convert.ToInt64(cmbCollectionAgency.SelectedValue);                  
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Payment, ActivityType.Select, "Collection agency selected to mark against patient payment .", 0, ContactId_Collection, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("select collection agency.", _Messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("select collection agency.", _Messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

      
       

       
    }
}
