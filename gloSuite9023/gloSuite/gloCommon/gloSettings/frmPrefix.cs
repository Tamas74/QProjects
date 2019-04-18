using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient ;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloSettings 
{
    public partial class frmPrefix : Form
    {   
        private long _ID = 0;
       // private string prev_prefix = null;
        private string _Server=null;
        private string _Database=null;
        private String _prefix=null;
        private string _databaseconnectionstring = "";

        enum RecordExists
        {
            ServerAndDatabase,
            Prefix
        }

        public frmPrefix(string databaseconnectionstring)
        {
            _databaseconnectionstring = databaseconnectionstring;
            InitializeComponent();
        }
        public frmPrefix(long nID, string sServer, string sDatabase, string sPrefix, string databaseconnectionstring)
        {

            _databaseconnectionstring = databaseconnectionstring;
            InitializeComponent();
            _ID = nID;
    //         prev_prefix= _prefix ;
            _Server = sServer;
            _Database = sDatabase;
            _prefix = sPrefix;

        }
        private void frmPrefix_Load(object sender, EventArgs e)
        {
            if (_ID > 0)
            {
                txtServer.Text   = _Server;
                txtDatabase.Text = _Database;
                txtPrefix.Text   = _prefix;
            }
            else
            {
                txtServer.Text = "";
                txtDatabase.Text = "";
                txtPrefix.Text = "";
            }

        }

        public bool Save_Prefix()
        {
            SqlCommand objCmd = null;
            try
            {

                SqlConnection objCon = new SqlConnection();
                objCon.ConnectionString = _databaseconnectionstring;
                objCmd = new SqlCommand();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "gsp_InUp_Prefix";

                SqlParameter objID = new SqlParameter();
                {
                    objID.ParameterName = "@nPrefixID";
                    objID.Value = _ID;
                    objID.Direction = ParameterDirection.Input;
                    objID.SqlDbType = SqlDbType.BigInt;
                }
                objCmd.Parameters.Add(objID);
                objID = null;

                SqlParameter objServer = new SqlParameter();
                {
                    objServer.ParameterName = "@sServer";
                    objServer.Value = (txtServer.Text.Trim());
                    objServer.Direction = ParameterDirection.Input;
                    objServer.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objServer);
                objServer = null;

                SqlParameter objDatabase = new SqlParameter();
                {

                    objDatabase.ParameterName = "@sDatabase";
                    objDatabase.Value = (txtDatabase.Text.Trim());
                    objDatabase.Direction = ParameterDirection.Input;
                    objDatabase.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objDatabase);
                objDatabase = null;


                SqlParameter objPrefix = new SqlParameter();
                {
                    objPrefix.ParameterName = "@sPrefix";
                    objPrefix.Value = txtPrefix.Text.Trim();
                    objPrefix.Direction = ParameterDirection.Input;
                    objPrefix.SqlDbType = SqlDbType.VarChar;
                }
                objCmd.Parameters.Add(objPrefix);
                objPrefix = null;


                objCmd.Connection = objCon;
                objCon.Open();
                objCmd.ExecuteNonQuery();
                objCon.Close();


                if (_ID > 0)
                {
                    string strDescription;
                    strDescription = _Server + " " + _Database + " " + _prefix + " " + "modified to" + " " + txtServer.Text + " " + txtDatabase.Text + " " + txtPrefix.Text;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Modify, strDescription, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Add, "New prefix Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                }
                //objCmd = null;
                objCon = null;
                //return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();  
                    objCmd.Dispose();
                    objCmd = null;

                }
            }
            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RecordExists checkPrefix = RecordExists.Prefix;
            RecordExists CheckServer = RecordExists.ServerAndDatabase;
            try
            {

                if (string.IsNullOrEmpty((txtServer.Text.Trim())))
                {

                    MessageBox.Show("Enter server name", gloAuditTrail.gloAuditTrail.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtServer.Focus();  
                    return;
                }
                else if (string.IsNullOrEmpty((txtDatabase.Text.Trim())))
                {
                    MessageBox.Show("Enter database name", gloAuditTrail.gloAuditTrail.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDatabase.Focus();  
                    return;
                }
                else if (txtPrefix.Text.Trim().Length < 3)
                {
                    MessageBox.Show("Enter 3 character site prefix", gloAuditTrail.gloAuditTrail.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }

                else if (txtPrefix.Text != _prefix)
                {
                    if (checkExists(checkPrefix.ToString()) == true)
                    {
                        MessageBox.Show("Prefix already in use.", gloAuditTrail.gloAuditTrail.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPrefix.Focus();  
                        return;
                    }

                }
                if ((txtDatabase.Text != _Database) || (txtServer.Text != _Server))
                {
                    if (checkExists(CheckServer.ToString()) == true)
                    {
                        MessageBox.Show("Database and Server name already exists.", gloAuditTrail.gloAuditTrail.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDatabase.Focus();
                        return;

                    }
                    Save_Prefix();
                }
                else
                {
                    Save_Prefix();
                }
               
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
               
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool checkExists(string FeildName)
        {
            string _Query = null;
            System.Data.SqlClient.SqlConnection objcon = new System.Data.SqlClient.SqlConnection();
            objcon.ConnectionString = _databaseconnectionstring;
            System.Data.SqlClient.SqlCommand cmd = default(System.Data.SqlClient.SqlCommand);
            bool _blnResult = false;
            int _strResult = 0;

            try
            {
                if (FeildName == RecordExists.ServerAndDatabase.ToString())
                {
                    _Query = "SELECT COUNT(*) FROM Prefix WHERE sServer = '" + (txtServer.Text.Trim()) + "' AND sDatabase= '" + txtDatabase.Text.Trim() + "' ";
                }
                else if (FeildName == RecordExists.Prefix.ToString())
                {
                    _Query = "SELECT COUNT(*) FROM Prefix WHERE sPrefix = '" + (txtPrefix.Text.Trim()) + "'";
                }

                cmd = new System.Data.SqlClient.SqlCommand(_Query, objcon);

                objcon.Open();
                _strResult = Convert.ToInt32(cmd.ExecuteScalar());
                objcon.Close();
                if (_strResult > 0)
                {
                    _blnResult = true;
                }

                //return _blnResult;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (objcon != null)
                {
                    objcon.Dispose();
                    objcon = null;
                }
                if (cmd != null)
                {
                    cmd.Parameters.Clear();  
                    cmd.Dispose();
                    cmd = null;
                }
                _Query = null;
            }
            return _blnResult;
        }
        public bool delete_Prefix(long nprefixId)
        {
            SqlParameter objServer =null;
            SqlCommand objCmd =null;
            try
            {

                SqlConnection objCon = new SqlConnection();
                objCon.ConnectionString = _databaseconnectionstring;
                objCmd = new SqlCommand();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "gsp_Delete_Prefix";
                
                objServer = new SqlParameter();
                {
                    objServer.ParameterName = "@nPrefixID";
                    objServer.Value = nprefixId;
                    objServer.Direction = ParameterDirection.Input;
                    objServer.SqlDbType = SqlDbType.BigInt;
                }
                objCmd.Parameters.Add(objServer);

                objCmd.Connection = objCon;
                objCon.Open();
                objCmd.ExecuteNonQuery();
                objCon.Close();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.Settings, gloAuditTrail.ActivityType.Delete, " Prefix deleted  ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                //objCmd = null;
                objServer = null;
                objCon = null;
                return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objCmd != null)
                {
                    objCmd.Parameters.Clear();
                    objCmd.Dispose();
                    objCmd = null;

                }
            }
            return true;
        }


            private void txtPrefix_KeyPress(object sender, KeyPressEventArgs e)
            {
                try
                {
                    if (char.IsNumber(e.KeyChar) == false && (e.KeyChar) != 8 && (char.IsLetter(e.KeyChar) == false))
                    {
                        e.Handled = true;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
            }
}
    }





