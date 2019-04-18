using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupGroupCode : Form
    {
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ReasonGroupCodeID = 0;
      //  private Int64 _ClinicID = 0;
        private bool isClosed = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public frmSetupGroupCode()
        {
            InitializeComponent();
            
         
        }
        public frmSetupGroupCode(Int64 ID, String databaseconnectionstring)
        {
            _databaseconnectionstring = databaseconnectionstring;
            _ReasonGroupCodeID = ID;
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
            if (_ReasonGroupCodeID!=0)
            {
                FillGroupCode();
            }
        }
        private void FillGroupCode()
        {
           // bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = "SELECT nID,sCode,sDescription,nClinicID,isnull(IsBlock,0) as IsBlock FROM BL_Reason_GroupCode_MST where nID= "+_ReasonGroupCodeID + "";
                    

                DataTable dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtCode.Text = Convert.ToString(dt.Rows[0]["sCode"]);
                    txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    if (Convert.ToBoolean(dt.Rows[0]["IsBlock"]) == true)
                    {
                        chkIsBlock.Checked = true;
                    }
                    else
                    {
                        chkIsBlock.Checked = false;
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
                ReasonCodes ObjReasonCodes = new ReasonCodes(_databaseconnectionstring);
                if (ObjReasonCodes.IsExistsGroupCode(_ReasonGroupCodeID, txtCode.Text.Trim() ,txtDescription.Text.Trim()   ) && _ReasonGroupCodeID==0)
                {
                    MessageBox.Show("Code is already in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
              Int64 _tempResult = ObjReasonCodes.AddModifyGroupCodes(_ReasonGroupCodeID, txtCode.Text.Trim(), txtDescription.Text.Trim(), chkIsBlock.Checked);
                        if (_tempResult > 0)
                        {
                          
                        }
                        else
                        {
                            //Added By Rahul Patel on 14-09-2010
                            //Make the Field empty
                            txtCode.Text = "";
                            txtDescription.Text = "";
                            _ReasonGroupCodeID = 0;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.POS, gloAuditTrail.ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, gloAuditTrail.ActivityOutCome.Failure);
                        }



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
                MessageBox.Show("Please Enter Value for GroupCode.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();  
                return false ;
            }
            else
            {
                return true;
            }
             
 
        }
        public bool DeleteGroupCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_Reason_GroupCode_MST where nID =" + ID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}