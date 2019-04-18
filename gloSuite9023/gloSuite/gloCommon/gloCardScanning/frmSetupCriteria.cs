using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloCardScanning
{
    public partial class frmSetupCriteria : Form
    {
        #region Variables
        public bool Isupdate = false;
        public bool IsNew = false;
        public bool IsAddress = false;
        public bool IsPhoto = false;
        public bool IsDOB = false;
        
        public bool IsFirstName = false;
        public bool IsMiddleName = false;
        public bool IsLastName = false;

        public bool IsSSN = false;

        public Int64 _PatientID;
        private Int64 _ClinicID = 0;
        string _MessageBoxCaption = String.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        #endregion

        #region Constructor
        public frmSetupCriteria(Int64 PatientID)
        {
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
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
            _PatientID=PatientID;
            InitializeComponent();
        }
        #endregion

        #region form event
        private void frmSetupCriteria_Load(object sender, EventArgs e)
        {
            if(_PatientID!=0)
            {
                rbNew.Visible=false;
            }
            else
            {
                rbNew.Visible=true;
            }
        }
        #endregion

        private void rbUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUpdate.Checked == true)
            {
                rbUpdate.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                groupBox2.Visible = true;
                chkadress.Enabled = true;
                chkPhoto.Enabled = true;
                chkDOB.Enabled = true;
            }
            else
            {
                rbUpdate.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNew.Checked == true)
            {
                rbNew.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                chkadress.Enabled = false;
                chkPhoto.Enabled = false;
                chkDOB.Enabled = false;
                chkadress.Checked = false;
                chkPhoto.Checked = false;
                chkDOB.Checked = false;

            }
            else
            {
                rbNew.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rbNew.Checked == true)
            {
                IsNew = true;
            }
            else if (rbUpdate.Checked == true)
            {
                if (chkadress.Checked == false && chkPhoto.Checked == false && chkDOB.Checked == false && chkFirstName.Checked == false && chkMiddleName.Checked == false && chkLastName.Checked == false && chkSSN.Checked == false)
                {
                    MessageBox.Show("Please select atleast one field to update. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Isupdate = true;
                    if (chkadress.Checked == true)
                    {
                        IsAddress = true;
                    }
                    if (chkPhoto.Checked == true)
                    {
                        IsPhoto = true;
                    }
                    if (chkDOB.Checked == true)
                    {
                        IsDOB = true;
                    }
                    if (chkFirstName.Checked == true)
                    {
                        IsFirstName = true;
                    }
                    if (chkLastName.Checked == true)
                    {
                        IsLastName = true;
                    }
                    if (chkMiddleName.Checked == true)
                    {
                        IsMiddleName = true;
                    }
                    if (chkSSN.Checked == true)
                    {
                        IsSSN = true;
                    }
                }
               
            }
             this.Close(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        //private void chkadress_CheckedChanged(object sender, EventArgs e)
        //{
        //    chkPhoto.Checked = false;
        //    chkboth.Checked = false;
        //    chkadress.Checked = true;

        //}

        //private void chkPhoto_CheckedChanged(object sender, EventArgs e)
        //{
        //    chkadress.Checked = false;
        //    chkboth.Checked = false;
        //    chkPhoto.Checked = true;
        //}

        //private void chkboth_CheckedChanged(object sender, EventArgs e)
        //{
        //    chkadress.Checked = false;
        //    chkPhoto.Checked = false;
        //    chkboth.Checked = true;
        //}
    }
}