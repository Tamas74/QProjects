using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloTaskMail
{
    public partial class frmSetupStatus : Form
    {

        private string _databaseconnectionstring = "";
        private Int64 _StatusID = 0;
        private string _tempStatusName="";
        //private string _messageBoxCaption = "gloPM";
        
        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 StatusID 
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        public string StatusName
        {
            get { return _tempStatusName; }
            set { _tempStatusName = value; }
        }


        public frmSetupStatus(Int64 StatusId,string DataBaseConnectionString)
        {
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

            _databaseconnectionstring = DataBaseConnectionString;
            _StatusID = StatusId; 
            InitializeComponent();
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":

                    if (txtStatusType.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the status type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtStatusType.Select();
                        break;
                    }

                    
                    gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                    gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();


                    if (StatusID == 0)
                    {
                        if (oTaskMail.IsExistsStatus(txtStatusType.Text.Trim()))
                        {
                            MessageBox.Show("Status already exists.  ");
                            return;
                        }
                        //Create Status object
                        oStatus.ID = 0;
                        oStatus.Description = txtStatusType.Text.Trim();

                        if (oTaskMail.Add(oStatus) <= 0)
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Status type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatusType.Select();
                            break;
                        
                        }
                        
                        
                    }
                    else
                    {
                        if (txtStatusType.Text.Trim() != StatusName)
                        {
                            if (oTaskMail.IsExistsStatus(txtStatusType.Text.Trim()))
                            {
                                MessageBox.Show("Status already exists.  ");
                                return;
                            }
                        }
                        //Create Status object
                        oStatus.ID = StatusID;
                        oStatus.Description = txtStatusType.Text.Trim();
                        if (! oTaskMail.Modify(oStatus))
                        {
                            MessageBox.Show("Status type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatusType.Select();
                            break;
                        }

                    }

                    oTaskMail.Dispose();
                    oStatus.Dispose();
                    this.Close();
                    break;

                case "Cancel":
                    this.Close();
                    break;
                case "Save":
                    if (txtStatusType.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the status type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtStatusType.Select();
                        break;
                    }


                    gloTasksMails.gloTaskMail oTaskMail1 = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                    gloTasksMails.Common.Status oStatus1 = new gloTasksMails.Common.Status();


                    if (StatusID == 0)
                    {
                        if (oTaskMail1.IsExistsStatus(txtStatusType.Text.Trim()))
                        {
                            MessageBox.Show("Status already exists.  ");
                            return;
                        }
                        //Create Status object
                        oStatus1.ID = 0;
                        oStatus1.Description = txtStatusType.Text.Trim();

                        if (oTaskMail1.Add(oStatus1) <= 0)
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Status type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatusType.Select();
                            break;

                        }


                    }
                    else
                    {
                        if (txtStatusType.Text.Trim() != StatusName)
                        {
                            if (oTaskMail1.IsExistsStatus(txtStatusType.Text.Trim()))
                            {
                                MessageBox.Show("Status already exists.  ");
                                return;
                            }
                        }
                        //Create Status object
                        oStatus1.ID = StatusID;
                        oStatus1.Description = txtStatusType.Text.Trim();
                        if (!oTaskMail1.Modify(oStatus1))
                        {
                            MessageBox.Show("Status type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtStatusType.Select();
                            break;
                        }

                    }

                    oTaskMail1.Dispose();
                    oStatus1.Dispose();
                    _StatusID = 0;
                    txtStatusType.Text = "";
                    break;

                default:
                    break;
            }
        }

        private void frmSetupStatus_Load(object sender, EventArgs e)
        {
            
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Status oStatus = new gloTasksMails.Common.Status();
            try
            {
                //if Category for modify.
                if (StatusID > 0)
                {
                    oStatus = oTaskMail.GetStatus(StatusID);

                    if (oStatus != null)
                    {
                        txtStatusType.Text = oStatus.Description;

                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oTaskMail.Dispose();
                oStatus.Dispose();
            }
        }
    }
}