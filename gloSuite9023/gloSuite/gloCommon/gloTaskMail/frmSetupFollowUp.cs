using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloTaskMail
{
    public partial class frmSetupFollowUp : Form
    {
        private string _databaseconnectionstring = "";
        private Int64 _followUpID = 0;
        private string _follwupName = "";
        //private string _messageBoxCaption = "gloPMS";
        private string _messageBoxCaption = String.Empty;

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 FollowUpID
        {
            get { return _followUpID; }
            set { _followUpID = value; }
        }

        public string FollowUpName
        {
            get { return _follwupName; }
            set { _follwupName = value; }
        }

        public frmSetupFollowUp(Int64 FollowUpID,string dataBaseConnectionString)
        {
             System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _databaseconnectionstring = dataBaseConnectionString;
            _followUpID = FollowUpID;
            InitializeComponent();

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

        }

        private void frmSetupFollowUp_Load(object sender, EventArgs e)
        {
            
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();
            try
            {
                //if Category for modify.
                if (FollowUpID > 0)
                {
                    oFollowUp = oTaskMail.GetFollowUp(FollowUpID);

                    if (oFollowUp != null)
                    {
                        txtfollowup.Text = oFollowUp.Description;

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
                oFollowUp.Dispose();
            }

        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    if (txtfollowup.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the follow up type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtfollowup.Select();
                        break;
                    }

                    gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                    gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();

                    if (FollowUpID == 0)
                    {
                        if (oTaskMail.IsExistsFollowUp(txtfollowup.Text.Trim()))
                        {
                            //Messagebox changed by Mayuri:20100102-To fix issue-5596-showing message without title
                            MessageBox.Show("Follow up already exists.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        
                        //Create FollowUp object
                        oFollowUp.ID = 0;
                        oFollowUp.Description = txtfollowup.Text.Trim();

                        if(oTaskMail.Add(oFollowUp) <=0)
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Follow up type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtfollowup.Select();
                            break;
                            
                        }

                        
                    }
                    else
                    {
                        if (txtfollowup.Text.Trim() != FollowUpName)
                        {
                            if (oTaskMail.IsExistsFollowUp(txtfollowup.Text.Trim()))
                            {
                                MessageBox.Show("Follow up already exists.  ");
                                return;
                            }
                        }
                        //Create FollowUp object
                        oFollowUp.ID = FollowUpID;
                        oFollowUp.Description = txtfollowup.Text.Trim();

                        if (! oTaskMail.Modify(oFollowUp))
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Follow up type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtfollowup.Select();
                            break;

                        }
                        
                    }

                    oTaskMail.Dispose();
                    oFollowUp.Dispose();
                    this.Close();
                    break;

                case "Cancel":
                    this.Close();
                    break;
                    
                case "Save":
                    if (txtfollowup.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the follow up type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtfollowup.Select();
                        break;
                    }

                    gloTasksMails.gloTaskMail oTaskMail1 = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                    gloTasksMails.Common.Followup oFollowUp1 = new gloTasksMails.Common.Followup();

                    if (FollowUpID == 0)
                    {
                        if (oTaskMail1.IsExistsFollowUp(txtfollowup.Text.Trim()))
                        {
                            //Msg change by Mayuri:20091203
                            //To fix issue:#443-Title should be required for messagebox
                            MessageBox.Show("Follow up already exists.  ",_messageBoxCaption ,MessageBoxButtons .OK ,MessageBoxIcon.Information);
                            return;
                        }

                        //Create FollowUp object
                        oFollowUp1.ID = 0;
                        oFollowUp1.Description = txtfollowup.Text.Trim();

                        if (oTaskMail1.Add(oFollowUp1) <= 0)
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Follow up type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtfollowup.Select();
                            break;

                        }


                    }
                    else
                    {
                        if (txtfollowup.Text.Trim() != FollowUpName)
                        {
                            if (oTaskMail1.IsExistsFollowUp(txtfollowup.Text.Trim()))
                            {
                                MessageBox.Show("Follow up already exists.  ");
                                return;
                            }
                        }
                        //Create FollowUp object
                        oFollowUp1.ID = FollowUpID;
                        oFollowUp1.Description = txtfollowup.Text.Trim();

                        if (!oTaskMail1.Modify(oFollowUp1))
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Follow up type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtfollowup.Select();
                            break;

                        }

                    }
                    _followUpID = 0;
                    txtfollowup.Text = "";
                    oTaskMail1.Dispose();
                    oFollowUp1.Dispose();
                   
                    break;
                default:
                    break;
            }
        }
    }
}