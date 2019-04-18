using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    internal partial class frmSetupQuickNotes : Form
    {
        #region "Variables"
         //MessageBox Caption
           private string _messageBoxCaption = String.Empty;
           QuickNotes oQuickNotes = null;
           System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion "Variables"
        
        #region "Property Procedures"

        private Int64 _ID = 0;
        private string _Note = "";
        private string _databaseconnectionstring = "";

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }

        #endregion "Property Procedures"

        #region "Constuctor"
         
          //Constructor with conn String
            public frmSetupQuickNotes(string DatabaseConnectionString)
            {
                InitializeComponent();
                _databaseconnectionstring = DatabaseConnectionString;

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

            public frmSetupQuickNotes(Int64 ID, string DatabaseConnectionString)
            {
                InitializeComponent();
                
                //Variable Initialization
                _ID = ID;
                _databaseconnectionstring = DatabaseConnectionString;
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

            public frmSetupQuickNotes(string Note, string DatabaseConnectionString)
            {
                InitializeComponent();

                //Variable Initialization
                _Note = Note;
                _databaseconnectionstring = DatabaseConnectionString;
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

        #endregion "Constuctor"

        #region "Form Load Event"
        private void frmSetupNotes_Load(object sender, EventArgs e)
        {
            QuickNotes oNote=null;
            DataTable dt = null;
            
            try
            {
                cmbNoteType.Select();
                _FillControls();

                //For modify show existing information on form.
                if (_ID != 0)
                {
                    oNote = new QuickNotes(_databaseconnectionstring);
                    dt = oNote.GetNote(_ID);

                    if (dt != null)
                    {
                        if (dt.Rows.Count != 0)
                        {
                            txtNotes.Text = dt.Rows[0]["sNoteDescription"].ToString();
                            cmbNoteType.Text = Enumerations.GetEnumDescription((QuickNoteType)Convert.ToInt32(dt.Rows[0]["nNoteType"])).ToString();
                            chkStatus.Checked = Convert.ToBoolean(dt.Rows[0]["bIsActive"]);
                        }
                       
                    }
                }
                else
                {
                    txtNotes.Text = _Note;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose(); dt = null;
                }
                if (oNote != null)
                {
                    oNote.Dispose(); oNote = null;
                }
            }
        }
        #endregion "Form Load Event"

        #region "Toolstrip Button Events"
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (SaveNote())
                        {
                            this.Close();
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        if (SaveNote())
                        {
                            _ID = 0;
                            txtNotes.Text = string.Empty;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _ID, 0, ActivityOutCome.Failure);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
              
            }

        }
        #endregion "Toolstrip Button Events"

        #region "Fill Controls"

        private void _FillControls()
       {
            cmbNoteType.Items.Clear();
            cmbNoteType.Items.Add("");
            cmbNoteType.Items.Add(Enumerations.GetEnumDescription(QuickNoteType.ClaimInternal).ToString());
            cmbNoteType.Items.Add(Enumerations.GetEnumDescription(QuickNoteType.AccountInternal).ToString());
            cmbNoteType.Items.Add(Enumerations.GetEnumDescription(QuickNoteType.StatementPatient).ToString());
            cmbNoteType.Items.Add(Enumerations.GetEnumDescription(QuickNoteType.StatementCharge).ToString());
            cmbNoteType.SelectedIndex = 0;
        }

        #endregion "Fill Procedures"

        private void cmbNoteType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNoteType.SelectedItem != null)
            {
                string _Type = cmbNoteType.SelectedItem.ToString();
                if (_Type != "")
                {
                    if (_Type == Enumerations.GetEnumDescription(QuickNoteType.StatementPatient))
                    {
                        lblAleart.Text = "Maximum 200 characters are allowed";
                        txtNotes.MaxLength = 200;
                    }
                    else
                    {
                        lblAleart.Text = "Maximum 255 characters are allowed";
                        txtNotes.MaxLength = 255;
                    }
                    lblAleart.Visible = true;
                }
            }
        }

        private bool SaveNote()
        {
            bool _result = false;
            try
            {
                oQuickNotes = new QuickNotes(_databaseconnectionstring);
                if (cmbNoteType.Text == "")
                {
                    MessageBox.Show("Select the Note Type", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbNoteType.Focus();
                    return _result;
                }
                if (txtNotes.Text.Trim() == "")
                {
                    MessageBox.Show("Enter the Note", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNotes.Focus();
                    return _result;
                }
                //Set values to Object
                string _Type = cmbNoteType.SelectedItem.ToString();
                if (_Type == Enumerations.GetEnumDescription(QuickNoteType.StatementPatient))
                {
                    if (txtNotes.Text.Trim().Length > 200)
                    {
                        MessageBox.Show("Note text entered exceeds the maximum length of allowed character(s). \n (Character(s) entered: " + txtNotes.Text.Trim().Length + ").", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNotes.Focus();
                        return _result;
                    }
                }

                oQuickNotes.Notes = txtNotes.Text.Trim();
                oQuickNotes.NoteType = (QuickNoteType)cmbNoteType.SelectedIndex;
                oQuickNotes.Status = chkStatus.Checked;

                if (_ID == 0)
                {
                    _ID = oQuickNotes.Add();
                    if (_ID == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Notes, ActivityType.Add, "Add Quick Notes", 0, _ID, 0, ActivityOutCome.Failure);
                        MessageBox.Show("Note not added.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return _result;
                    }
                    else
                    {
                        _result = true;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Notes, ActivityType.Add, "Add Quick Notes", 0, _ID, 0, ActivityOutCome.Success);
                    }

                }
                else
                {
                    oQuickNotes.NoteID = _ID;

                    if (oQuickNotes.Add() == 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Notes, ActivityType.Modify, "Modify Quick Notes", 0, _ID, 0, ActivityOutCome.Failure);
                        MessageBox.Show("Note not modified.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return _result;
                    }
                    else
                    {
                        _result = true;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Notes, ActivityType.Modify, "Modify Quick Notes", 0, _ID, 0, ActivityOutCome.Success);

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
                _result = false;
            }
            finally
            {
                if (oQuickNotes != null)
                {
                    oQuickNotes.Dispose();
                    oQuickNotes = null;
                }
            }
            return _result;
        }
    }
}