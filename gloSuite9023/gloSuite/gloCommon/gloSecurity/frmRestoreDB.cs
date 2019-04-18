using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;


namespace gloSecurity
{
    public partial class frmRestoreDB : Form
    {
        
        #region Declarations
        
        //private Server srvSql;
        //private string _messageBoxCaption = "gloPMS";

        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _databaseConnectionString = "";
      //  private string _sqlServerName = "";
       // private string _databaseName = "";
       // private string _sqlLogin = "";
       // private string _sqlPassword = "";
       // private bool _winAuthentication;
        
#endregion

        #region Constructor 
        
        public frmRestoreDB(string databseConnectionString)
        {
            //Added By Pramod Nair For Messagebox Caption 
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

            _databaseConnectionString = databseConnectionString;
            InitializeComponent();
        }
        
#endregion

        private void frmRestoreDB_Load(object sender, EventArgs e)
        {
            //getDatabaseSettings();
            //fillDatabases();
        }

        /// <summary>
        /// Select backup file from file browser dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    dlgBackupFile.Title = " Select Backup ";
            //    dlgBackupFile.Filter = "Backup Files(*.bak)|*.bak";
            //    dlgBackupFile.CheckFileExists = true;
            //    dlgBackupFile.Multiselect = false;
            //    dlgBackupFile.ShowHelp = false;
            //    dlgBackupFile.ShowReadOnly = false;

            //    if (dlgBackupFile.ShowDialog() == DialogResult.OK)
            //    {
            //        txtBackupFile.Text = dlgBackupFile.FileName;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //switch (e.ClickedItem.Tag.ToString())
            //{
            //    case "OK":
            //        try
            //        {
            //            //Make sure that Database name is entered
            //            if (txtDatabase.Text.Trim() == "")
            //            {
            //                MessageBox.Show(" Database name can not be blank  ", _gloMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                txtDatabase.Focus();
            //                break;
            //            }

            //            //Check backup file is present
            //            if (!File.Exists(txtBackupFile.Text.Trim()))
            //            {
            //                MessageBox.Show(" Backup file does not exists ", _gloMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                btnBrowse.Focus();
            //                break;
            //            }

            //            //restore database
            //            restoreDatabase();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.ToString(), _gloMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        break;


            //    case "Cancel":
            //        try
            //        {
            //            this.Close();
            //        }
            //        catch (Exception ex)
            //        {
            //        }//catch

            //        break;
            //}
        }

        /// <summary>
        /// This method restore the selected backup file to specified database 
        /// </summary>
        private void restoreDatabase()
        {

            //srvSql = connectToDatabse(_sqlServerName);

            //// Create a new database restore operation
            //Restore rstDatabase = new Restore();
            //// Set the restore type to a database restore
            //rstDatabase.Action = RestoreActionType.Database;
            //// Set the database that we want to perform the restore on
            //rstDatabase.Database = txtDatabase.Text.Trim();
            //// Set the backup device from which we want to restore, to a file
            //BackupDeviceItem bkpDevice = new BackupDeviceItem(txtBackupFile.Text.Trim(), DeviceType.File);
            //// Add the backup device to the restore type                        
            //rstDatabase.Devices.Add(bkpDevice);
            //// If the database already exists, replace it
            //rstDatabase.ReplaceDatabase = true;


            //string oldMdf = "", newMdf = "";
            //string oldLdf = "", newLdf = "";
            //string filePath = "";

            //// Gives list of file to Relocate
            //DataTable dtFiles = rstDatabase.ReadFileList(srvSql);

            ////get the Data & Log file names
            //for (int i = 0; i < dtFiles.Rows.Count; i++)
            //{
            //    string tempPysicalName = Convert.ToString(dtFiles.Rows[i]["PhysicalName"]);
            //    string fileType = tempPysicalName.Substring(tempPysicalName.Length - 3, 3);
            //    filePath = tempPysicalName.Substring(0, tempPysicalName.LastIndexOf("\\"));
            //    if (fileType.ToUpper() == "MDF") //Data(mdf) file
            //    {
            //        oldMdf = Convert.ToString(dtFiles.Rows[i]["LogicalName"]);
            //    }
            //    if (fileType.ToUpper() == "LDF") //Log(ldf) file
            //    {
            //        oldLdf = Convert.ToString(dtFiles.Rows[i]["LogicalName"]);
            //    }
            //}

            ////Restore the Data & Log files if already exists
            //if (File.Exists(filePath + "\\" + oldMdf + ".MDF"))
            //{
            //    newMdf = filePath + "\\" + txtDatabase.Text.Trim() + "_Data.mdf";
            //    newLdf = filePath + "\\" + txtDatabase.Text.Trim() + "_Log.ldf";
            //    rstDatabase.RelocateFiles.Add(new RelocateFile(oldMdf, newMdf));
            //    rstDatabase.RelocateFiles.Add(new RelocateFile(oldLdf, newLdf));
            //}

            //// Perform the restore 
            //rstDatabase.SqlRestore(srvSql);
        }

        /// <summary>
        /// this method set connection to given database
        /// and returns the server object
        /// </summary>
        /// <param name="sqlServerName"></param>
        /// <returns></returns>
        
        //private Server connectToDatabse(string sqlServerName)
        //{
            ////Connect to Database
            //ServerConnection srvConn = new ServerConnection(sqlServerName);
            //srvConn.LoginSecure = _winAuthentication;
            //if (!srvConn.LoginSecure)  //you can set login & password only if LoginSecure = true
            //{
            //    srvConn.Login = _sqlLogin;
            //    srvConn.Password = _sqlPassword;
            //}
            //return new Server(srvConn);
        //}

        /// <summary>
        /// This method gets database settings from Application Config file
        /// </summary>
        private void getDatabaseSettings()
        {
            //try
            //{
            //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            //    _sqlServerName = Convert.ToString(appSettings["SQLServerName"]);
            //    _databaseName = Convert.ToString(appSettings["DatabaseName"]);
            //    _sqlLogin = Convert.ToString(appSettings["SQLLoginName"]);
            //    _sqlPassword = Convert.ToString(appSettings["SQLPassword"]);
            //    _winAuthentication = Convert.ToBoolean(appSettings["WindowAuthentication"]);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), _gloMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        /// <summary>
        /// Display list of all existing databases in combobox
        /// </summary>
        private void fillDatabases()
        {
            //cmbDatabases.Items.Clear();
            //Server tempServer = connectToDatabse(_sqlServerName);
            //foreach (Database db in tempServer.Databases)
            //{
            //    cmbDatabases.Items.Add(db.Name);
            //}
        }

        /// <summary>
        /// select the database from existing databases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbDatabases.SelectedIndex != -1)
            //{
            //    txtDatabase.Text = cmbDatabases.SelectedItem.ToString();
            //}
        }

        private void rbtn_Overwrite_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbtn_Overwrite.Checked == true)
            //{
            //    cmbDatabases.Enabled = true;
            //}
            //else
            //{
            //    cmbDatabases.Enabled = false;
            //    cmbDatabases.SelectedIndex = -1;
            //}
        }

        private void txtDatabase_TextChanged(object sender, EventArgs e)
        {
            //int index = cmbDatabases.FindStringExact(txtDatabase.Text.Trim());

            //if (index == -1)
            //{
            //    rbtn_NewDatabse.Checked = true;
            //}
            //else
            //{
            //    cmbDatabases.SelectedIndex = index;
            //    rbtn_Overwrite.Checked = true;
            //}
        }


    }
}