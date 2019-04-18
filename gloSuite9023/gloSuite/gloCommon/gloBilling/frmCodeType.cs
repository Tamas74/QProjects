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
    public partial class frmCodeType : Form
    {
        #region'Constructors'
        public frmCodeType(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
        }

        public frmCodeType(Int64 ID, string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _CodeTypeID = ID;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }


        }
        
        #endregion

        #region 'Declaration & Properties'

        private Int64 _CodeTypeID = 0;
        private string _databaseconnectionstring;
        public string _MessageBoxCaption = String.Empty;


       
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 CodeTypeID
        {
            get { return _CodeTypeID; }
            set { _CodeTypeID = value; }
        }
        #endregion 'Declaration'

        #region 'Form Load Event'
        private void frmCodeType_Load(object sender, EventArgs e)
        {
            try
            {
                if (_CodeTypeID != 0)
                {
                    CodeType oCtype = new CodeType(_databaseconnectionstring);
                    DataTable dtCodetype;
                    dtCodetype = oCtype.GetcodeType(_CodeTypeID);

                    if (dtCodetype != null)
                    {
                        if (dtCodetype.Rows.Count != 0)
                        {
                            txtCodeTypeCode.Text = dtCodetype.Rows[0][0].ToString();
                            txtCodeTypeDesc.Text = dtCodetype.Rows[0][1].ToString();

                        }
                    }
                    oCtype.Dispose();
                }
            }
            catch (Exception)// ex)
            {
                MessageBox.Show("Error retrieving data.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ex.ToString();
                //ex = null;
            }

        }
        #endregion

        #region 'Tool Strip Events'
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e) 
       
        {
             switch (e.ClickedItem.Tag.ToString())
            {
                case "SaveClose":
                    if (Save())
                    {
                        this.Close();
                    }

                    break;

                case "Save":
                    if (Save())
                    {
                        txtCodeTypeCode.Text = "";
                        txtCodeTypeDesc.Text = "";
                        _CodeTypeID = 0;
                    }

                    break;

                case "Cancel":
                    this.Close();
                    break;
            }

        }
        #endregion

        #region 'Save Method'
        private bool Save()
        {
            if (txtCodeTypeCode.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodeTypeCode.Focus();
                return false;
 
            }
            if (txtCodeTypeDesc.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodeTypeDesc.Focus();
                return false;
            }
            try
            {
                CodeType oCtype = new CodeType(_databaseconnectionstring);
                oCtype.ClinicID = this.ClinicID;
                oCtype.CodetypeID = _CodeTypeID;
                oCtype.CodeTypeCode = Convert.ToString(txtCodeTypeCode.Text);
                oCtype.CodeTypeDesc = Convert.ToString(txtCodeTypeDesc.Text);

                if (oCtype.IsExistsCodeType( oCtype.CodetypeID, oCtype.CodeTypeCode, oCtype.CodeTypeDesc))
                {
                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false ;

                }
                _CodeTypeID=oCtype.Add() ;
                if (_CodeTypeID > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.CodeType, ActivityType.Add, "Add Code Type", 0, _CodeTypeID, 0, ActivityOutCome.Success);

                    return true;
                }
                else 
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.CodeType, ActivityType.Add, "Add Code Type", 0, _CodeTypeID, 0, ActivityOutCome.Failure);
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
               
            }
            return false;

        }
        #endregion







        



    }
}