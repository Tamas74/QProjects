using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupCardType : Form
    {

        #region " Varible Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _DatabaseConnectionString = "";
      //  private string _emrdatabaseConnectionString = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";

        private Int64 _creditcardid = 0;
        private string _creditcardname = "";

        #endregion " Varible Declarations "

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public Int64 CreditCardID
        {
            get { return _creditcardid; }
            set { _creditcardid = value; }
        }
        public string CreditCardName
        {
            get { return _creditcardname; }
            set { _creditcardname = value; }
        }

        #endregion " Property Procedures "

        #region " Construtor "

        public frmSetupCardType(string databaseconnectionstring)
        {
            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            _DatabaseConnectionString = databaseconnectionstring;

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

        public frmSetupCardType(string databaseconnectionstring,Int64 cardId)
        {
            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            #region " Retrive Database Connection String for appSettings "

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            _DatabaseConnectionString = databaseconnectionstring;
            _creditcardid = cardId;

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

        #endregion " Construtor "

        #region " Form Load "

        private void frmSetupCardType_Load(object sender, EventArgs e)
        {

            txtCardName.Select();
            PerformLoad();
        }

        #endregion " Form Load "

        #region " ToolStrip Button Click Event "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            bool _result=SaveCardType();
            if (_result == true)
            { 
                 this.Close();
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            bool _result = SaveCardType();
            if (_result == true)
            {
                _creditcardid = 0;
                txtCardName.Text = "";
            }
        }

        #endregion " ToolStrip Button Click Event "

        #region " Private & Public Methods "

        private bool SaveCardType()
        {
            CreditCards oCreditCards = new CreditCards(_DatabaseConnectionString);

            try
            {
                if (txtCardName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the credit card name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCardName.Focus();
                    return false;
                }
                
                oCreditCards.CreditCardDesc = txtCardName.Text;

                if (_creditcardid == 0)
                {
                    if (oCreditCards.IsExists(0,oCreditCards.CreditCardDesc.ToString()) == true)
                    {
                        MessageBox.Show(this, "Credit card name is already in use by another entry.  Please select a unique name.  ", _messageBoxCaption);
                        return false; 
                    }
                    _creditcardid = oCreditCards.Add();
                    if (_creditcardid == 0)
                    {
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add CreditCard", 0, _creditcardid, 0, ActivityOutCome.Failure);
                        MessageBox.Show("Card not added.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false; 
                    }
                    else
                    {
                        return true;
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _creditcardid, 0, ActivityOutCome.Success);
                    }
                    
                }
                else
                {
                    oCreditCards.CreditCardID = _creditcardid;
                    if (oCreditCards.IsExists(_creditcardid, oCreditCards.CreditCardDesc.Trim()) == true)
                    {
                        MessageBox.Show(this, "Card Description is already in use by another entry.  Please select a unique code.  ", _messageBoxCaption);
                        return false;
                    }

                    if (oCreditCards.Add() == 0)
                    {
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Failure);
                        //Hit message on failing to add category.
                        MessageBox.Show("Card not modified.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        oCreditCards.Dispose();
                        return false;
                    }
                    else
                    {
                        oCreditCards.Dispose();
                        return true;
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.Category, ActivityType.Add, "Add Category", 0, _catID, 0, ActivityOutCome.Success);
                    }
                }
               
                
            }
            catch (Exception ex)
            {
                oCreditCards.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;
            }
        }

        private void PerformLoad()
        {
            try
            {
                if (_creditcardid > 0)
                {
                    CreditCards oCreditCards = new CreditCards(_DatabaseConnectionString);
                    DataTable _dtCards = null;
                    _dtCards = oCreditCards.GetCreditCard(_creditcardid);
                    if (_dtCards != null && _dtCards.Rows.Count > 0)
                    {
                        txtCardName.Text = Convert.ToString(_dtCards.Rows[0]["sCreditCardDesc"]);
                        txtCardName.Tag = Convert.ToInt64(_dtCards.Rows[0]["nCreditCardID"]);
                    }
                    if (oCreditCards != null) { oCreditCards.Dispose(); }
                    if (_dtCards != null) { _dtCards.Dispose(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { }
        }

        #endregion " Private & Public Methods "

        //Resolved bug #test
     

       

    }
}