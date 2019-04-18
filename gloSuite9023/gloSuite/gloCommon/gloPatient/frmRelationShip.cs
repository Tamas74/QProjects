using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloPatient
{
    internal partial class frmRelationShip : Form
    {

        private string _databaseconnectionstring = "";

        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private Int64 _relID = 0;

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 RelID
        {
            get { return _relID; }
            set { _relID = value; }
        }

        public frmRelationShip(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;


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

        public frmRelationShip(Int64 appID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _relID = appID;

            _databaseconnectionstring = DatabaseConnectionString;


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

        private void frmRelationShip_Load(object sender, EventArgs e)
        {
            if (_relID != 0)
            {
                RelationShip ofrm = new RelationShip(_databaseconnectionstring);
                 DataTable dtRelType ;
                dtRelType = ofrm.GetRelationShip(_relID);
                ofrm.Dispose();
                if (dtRelType != null)
                {
                    if (dtRelType.Rows.Count != 0)
                    {
                        txtRelationship.Text = dtRelType.Rows[0]["sRelationship"].ToString();    
                    }
                }
                

                                
            }
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                RelationShip oRelationShip = new RelationShip(_databaseconnectionstring );
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        
                        if (txtRelationship.Text.Trim() == "")
                        {
                            MessageBox.Show(this, "Please enter relationship.  ", _messageBoxCaption);
                            txtRelationship.Focus();
                            return;
                        
                        }

                        oRelationShip.Relationship = txtRelationship.Text.Trim();

                        oRelationShip.ClinicID = 1;
                        
                        if (_relID == 0)
                        {
                         if (oRelationShip.IsExists(0,oRelationShip.Relationship.ToString()) == true)
                            {
                                MessageBox.Show(this, "Patient relationship already exists.  ", _messageBoxCaption);
                                txtRelationship.Focus();
                                return;
                            }

                          if (oRelationShip.Add() == 0)
                            {

                                MessageBox.Show("Patient relationship not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        else
                        {
                            oRelationShip.PatientRelID = _relID;
                            if (oRelationShip.IsExists(_relID, oRelationShip.Relationship.Trim()) == true)
                            {
                                MessageBox.Show(this, "Patient relationship  already exist.  ", _messageBoxCaption);
                                txtRelationship.Focus();
                                return;
                            }
                           

                           if (oRelationShip.Modify() == false)
                            {

                                MessageBox.Show("Patient relationship not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        this.Close();
                        oRelationShip.Dispose();
                   
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                this.Close();
            }




        }
    }
}