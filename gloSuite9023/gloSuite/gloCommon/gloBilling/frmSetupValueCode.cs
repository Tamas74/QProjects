using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupValueCode : Form
    {
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ValueCodeID = 0;
       // private Int64 _ClinicID = 0;
        private bool isClosed = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public Int64 ValueID
        {
            set {
                _ValueCodeID = value;
            }

               get {
                return _ValueCodeID;
            }
        }

        public bool IsSystemRecord { get; set; }

        public frmSetupValueCode()
        {
            InitializeComponent();
            
         
        }
        public frmSetupValueCode(Int64 ID, String databaseconnectionstring)
        {
            _databaseconnectionstring = databaseconnectionstring;
            _ValueCodeID = ID;
            InitializeComponent();
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
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (_ValueCodeID!=0)
            {
                tsb_Save.Visible = false;
                FillValueCode();

                if (IsSystemRecord)
                {
                    txtCode.Enabled = false;
                    txtDescription.Enabled = false;
                }
            }
        }
        private void FillValueCode()
        {
           // bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = "SELECT nValueID,sValueCode,sDescription,IsActive FROM UB_ValueCodes where nValueID= " + _ValueCodeID + "";
                    

                DataTable dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtCode.Text = Convert.ToString(dt.Rows[0]["sValueCode"]);
                    txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    if (Convert.ToBoolean(dt.Rows[0]["IsActive"]) == true)
                    {
                        rbActive.Checked = true;
                        rbInactive.Checked = false;
                    }
                    else
                    {
                        rbActive.Checked = false;
                        rbInactive.Checked = true;
                    }
                }


            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {

            }

            //return dt;

        }
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                ValueCodes ObjValueCodes = new ValueCodes(_databaseconnectionstring);
                if (ObjValueCodes.IsExistsValueCode(_ValueCodeID, txtCode.Text.Trim()))
                {
                    MessageBox.Show("Code is already in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return;
                }
                Int64 _tempResult = ObjValueCodes.AddValueCode(_ValueCodeID, txtCode.Text.Trim(), txtDescription.Text.Trim(), (rbActive.Checked ? true : false));
                if (_tempResult > 0)
                {
                    _ValueCodeID = 0;// _tempResult;
                }
                else
                {           
                    txtCode.Text = "";
                    txtDescription.Text = "";
                    _ValueCodeID = 0;
                }

                txtCode.Text = "";
                txtDescription.Text = "";

                if (isClosed == true)
                {
                this.Close();
                }
            }
            
        }

        private void tsb_Saveclose_Click(object sender, EventArgs e)
        {
            isClosed = true; 
            tsb_Save_Click(null, null);
            //this.Close(); 
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private bool  Validate()
        {
            if (txtCode.Text.Trim()=="")
            {
                MessageBox.Show("Enter a Value code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();  
                return false ;
            }
            if (txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Enter a description.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return false;
            }
            else
            {
                return true;
            }
             
 
        }
        

        # region " Events "
        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInactive.Checked == true)
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbInactive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActive.Checked == true)
            {
                rbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbActive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }
        #endregion
    }
}