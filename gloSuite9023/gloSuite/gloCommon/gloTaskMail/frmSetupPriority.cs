using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloTaskMail
{
    public partial class frmSetupPriority : Form
    {

        private string _databaseconnectionstring = "";
        private Int64 _PriorityID = 0;
        private string _PriorityName = "";
        //private string _messageBoxCaption = "gloPM";

        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;


        public string PriorityName
        {
            get { return _PriorityName; }
            set { _PriorityName = value; }
        }

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 PriorityID
        {
            get { return _PriorityID ; }
            set { _PriorityID  = value; }
        }

        public frmSetupPriority(Int64 PriorityId,string DatabaseConnectionString)
        {

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _PriorityID = PriorityId;
            _databaseconnectionstring = DatabaseConnectionString;
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

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    if (txtPriorityType.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the priority type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriorityType.Select();
                        break;
                    }

                    gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                    gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                    
                    if (PriorityID == 0)
                    {

                        if (oTaskMail.IsExistsPriority(txtPriorityType.Text.Trim()))
                        {
                            MessageBox.Show("Priority already exists.  ");
                            return;
                        }
                        //Create Priority object
                        oPriority.ID = 0;
                        oPriority.Description = txtPriorityType.Text.Trim();
                        oPriority.PriorityLevel = Convert.ToInt64(cmbPriorityLevel.Text);

                        if(oTaskMail.Add(oPriority) <= 0)
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Priority type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPriorityType.Select();
                            break;
                        }
                      
                    }
                    else
                    {
                        if (txtPriorityType.Text.Trim() != PriorityName)
                        {
                            if (oTaskMail.IsExistsPriority(txtPriorityType.Text.Trim()))
                            {
                                MessageBox.Show("Priority already exists.  ");
                                return;
                            }
                        }
                        
                        oPriority.ID = PriorityID;
                        oPriority.Description = txtPriorityType.Text.Trim();
                        oPriority.PriorityLevel = Convert.ToInt64(cmbPriorityLevel.Text);

                        if(! oTaskMail.Modify(oPriority))
                        {
                            MessageBox.Show("Priority type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPriorityType.Select();
                            break;
                        }

                        
                    }

                    oTaskMail.Dispose();
                    oPriority.Dispose();
                    this.Close();
                    break;

                case "Cancel":
                    this.Close();
                    break;
                case "Save":
                    if (txtPriorityType.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter the priority type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriorityType.Select();
                        break;
                    }

                    gloTasksMails.gloTaskMail oTaskMail1 = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
                    gloTasksMails.Common.Priority oPriority1 = new gloTasksMails.Common.Priority();

                    if (PriorityID == 0)
                    {

                        if (oTaskMail1.IsExistsPriority(txtPriorityType.Text.Trim()))
                        {
                            MessageBox.Show("Priority already exists.  ");
                            return;
                        }
                        //Create Priority object
                        oPriority1.ID = 0;
                        oPriority1.Description = txtPriorityType.Text.Trim();
                        oPriority1.PriorityLevel = Convert.ToInt64(cmbPriorityLevel.Text);

                        if (oTaskMail1.Add(oPriority1) <= 0)
                        {
                            // Record is Not Added Successfully
                            MessageBox.Show("Priority type not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPriorityType.Select();
                            break;
                        }

                    }
                    else
                    {
                        if (txtPriorityType.Text.Trim() != PriorityName)
                        {
                            if (oTaskMail1.IsExistsPriority(txtPriorityType.Text.Trim()))
                            {
                                MessageBox.Show("Priority already exists.  ");
                                return;
                            }
                        }

                        oPriority1.ID = PriorityID;
                        oPriority1.Description = txtPriorityType.Text.Trim();
                        oPriority1.PriorityLevel = Convert.ToInt64(cmbPriorityLevel.Text);

                        if (!oTaskMail1.Modify(oPriority1))
                        {
                            MessageBox.Show("Priority type not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPriorityType.Select();
                            break;
                        }


                    }

                    oTaskMail1.Dispose();
                    oPriority1.Dispose();
                    _PriorityID=0;
                    txtPriorityType.Text = "";
                    cmbPriorityLevel.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }

        private void frmSetupPriority_Load(object sender, EventArgs e)
        {
            
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();

            try
            {
                cmbPriorityLevel.SelectedIndex = 2;
                //if Category for modify.
                if (PriorityID > 0)
                {
                    oPriority = oTaskMail.GetPriority(PriorityID);
                    if (oPriority != null)
                    {
                        txtPriorityType.Text = oPriority.Description;
                        cmbPriorityLevel.Text = oPriority.PriorityLevel.ToString();
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
                oPriority.Dispose();
            }
        }
    }
}