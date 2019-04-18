using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace gloBilling
{
    public partial class frmImportRVUSchedule : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private DialogResult _frmDlgRst = DialogResult.None;

        private string _strFile = "";
        private string _strFileType = "";
        private Int32 _nFilePEType = 0;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public string ImportFileName
        {
            get { return _strFile; }
            set { _strFile = value; }
        }
        public string ImportFileType
        {
            get { return _strFileType; }
            set { _strFileType = value; }
        }
        public Int32 ImportPEId
        {
            get { return _nFilePEType; }
            set { _nFilePEType = value; }
        }

        #endregion " Declarations "

        public DialogResult FrmDlgRst
        {
            get { return _frmDlgRst; }
            set { _frmDlgRst = value; }
        }

        #region "Contructor"

        public frmImportRVUSchedule(string DatabaseConnectionString)
        {
            
          _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            InitializeComponent();
        } 

        #endregion

        private void frmImportRVUSchedule_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);
          try
          {
              FillSpecialities();
          }
          catch (Exception ex)
          {
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
          }
        }
        public void FillSpecialities()
        {
             DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("nID");
                dt.Columns.Add("sDesc");
                dt.Rows.Add();
                dt.Rows[0]["nID"] = 0;
                dt.Rows[0]["sDesc"] = "";
                dt.Rows.Add();
                dt.Rows[1]["nID"] = 1;
                dt.Rows[1]["sDesc"] = "Transitioned, Non-Facility";
                dt.Rows.Add();
                dt.Rows[2]["nID"] = 2;
                dt.Rows[2]["sDesc"] = "Transitioned, Facility";
                dt.Rows.Add();
                dt.Rows[3]["nID"] = 3;
                dt.Rows[3]["sDesc"] = "Fully Implemented, Non-Facility";
                dt.Rows.Add();
                dt.Rows[4]["nID"] = 4;
                dt.Rows[4]["sDesc"] = "Fully Implemented, Facility";

                dt.AcceptChanges();
                cmbPEType.DataSource = dt;
                cmbPEType.ValueMember = "nID";
                cmbPEType.DisplayMember = "sDesc";
                cmbPEType.Refresh();
            }
            catch (Exception ex)
            { 
                if(dt != null) { dt.Dispose(); }
                throw ex;
            }
            finally
            {
                //if (dt != null) { dt.Dispose(); }
            }

        }

        #region "Button Click Events"

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            try
            {
                dlgBrowseFile.Title = " Browse File ";
                //dlgBrowseFile.Filter = "Excel Files(*.csv)|*.csv";
                //if (rbStandard.Checked == true)
                //{
                //    dlgBrowseFile.Filter = "Office Documents(*.xls, *.xlsx, *.csv)|*.xls;*.xlsx;*.csv";  
                //}
                //else
                //{
                    dlgBrowseFile.Filter = "Office Documents(*.xls, *.xlsx)|*.xls;*.xlsx";
                //}
                //dlgBrowseFile.Filter = "Office Documents(*.xls, *.xlsx)|*.xls;*.xlsx";
                dlgBrowseFile.CheckFileExists = true;
                dlgBrowseFile.Multiselect = false;
                dlgBrowseFile.ShowHelp = false;
                dlgBrowseFile.ShowReadOnly = false;

                if (dlgBrowseFile.ShowDialog(this) == DialogResult.OK)
                {
                    txtImportFile.Text = dlgBrowseFile.FileName;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Import_Click(object sender, EventArgs e)
        {
            frm_SetupRVUSchedule objfrm_SetupRVUSchedule = new frm_SetupRVUSchedule(0,_databaseconnectionstring);
            try
            {
                if (ValidateData() == true)
                {
                    ImportFileName = Convert.ToString(dlgBrowseFile.FileName);
                    ImportPEId = Convert.ToInt32(cmbPEType.SelectedValue);
                    if (rbStandard.Checked == true)
                    {
                        _strFileType = "Standard";
                    }
                    else if (rbCustom.Checked == true)
                    {
                        _strFileType = "Custom";
                    }
                    _frmDlgRst = DialogResult.OK;
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (objfrm_SetupRVUSchedule != null) { objfrm_SetupRVUSchedule.Dispose(); }
            }
            finally
            {
                if (objfrm_SetupRVUSchedule != null) { objfrm_SetupRVUSchedule.Dispose(); }
            }

        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private bool ValidateData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (rbCustom.Checked == true)
                {
                    if (cmbPEType.Text.Trim() == "")
                    {
                        MessageBox.Show("Select Practice Expense Type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbPEType.Focus();
                        return false;
                    }
                }
                if (txtImportFile.Text.Trim() == "")
                {
                    MessageBox.Show("Select a file to import.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_Browse.Focus();  
                    return false;
                }

                if (File.Exists(txtImportFile.Text.Trim()) == false)
                {
                    MessageBox.Show("Source file does not exists.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_Browse.Focus();
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
            return true; 

        }

        private void rbStandard_CheckedChanged(object sender, EventArgs e)
        {
            cmbPEType.Visible = false;
            cmbPEType.SelectedIndex = 0;
            lblPETypeStar.Visible = false;
            lblPEType.Visible = false;
            this.Height = 154;
            if (rbStandard.Checked == true)
                rbStandard.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbStandard.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }

        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            cmbPEType.Visible = true;
            lblPETypeStar.Visible = true;
            lblPEType.Visible = true;
            this.Height = 185;
            if (rbCustom.Checked == true)
                rbCustom.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbCustom.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }


       
    }
}