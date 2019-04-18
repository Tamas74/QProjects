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
    public partial class frmSetupPOS : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64  _POSID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "
        
        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 POSID
        {
            get { return _POSID; }
            set { _POSID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupPOS(string databaseConnectionString,Int64 POSId)
        {
            _databaseconnectionstring = databaseConnectionString;
            _POSID = POSId;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        #endregion " Constructor "

        #region " Form Load "

          private void frmSetupPOS_Load(object sender, EventArgs e)
        {
            try
            {
                FillPOS(POSID);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

         #endregion " Form Load "

        #region " Tool Strip Event "

        private void tls_SetupResource_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Int64 _tempResult = 0;
            Int32 _FacilityType=0;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {

                            if (Validate())
                            {
                                //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
                                //Check if the POS already exists if yes dont add.
                                if ((oTOSPOS.IsExistsPOS(Convert.ToInt64(txtCode.Tag), txtCode.Text.Trim(), txtName.Text.Trim())))
                                {
                                    MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }                              
                                if (POSID > 0)
                                {
                                    //Modify
                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    //if(cmbFacilityType.SelectedValue!=null)
                                    //{
                                    //_FacilityType = Convert.ToInt32(((FacilityType)cmbFacilityType.Text));
                                   

                                    //} 
                                        //cmbFacilityType.Text = _FacilityType.ToString();   
                                    _tempResult = oTOSPOS.AddModifyPOS(Convert.ToInt64(txtCode.Tag), txtCode.Text.Trim(), txtName.Text.Trim(), txtDescription.Text.Trim(),_FacilityType);
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add  Place Of Service", 0, _tempResult, 0, ActivityOutCome.Success);

                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        oTOSPOS.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                                    }

                                }
                                else
                                {
                                    //Add New
                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    _tempResult = oTOSPOS.AddModifyPOS(0, txtCode.Text.Trim(), txtName.Text.Trim(), txtDescription.Text.Trim(),_FacilityType);
                                    _POSID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add  Place Of Service", 0, _tempResult, 0, ActivityOutCome.Success);

                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        oTOSPOS.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                                    }

                                }
                                this.Close(); 
                                
                            }
                        }//Case "OK"
                        break;
                    case "Cancel" :
                        this.Close();
                        break;

                    case "Save":
                        {
                            if (Validate())
                            {
                                //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
                                //Check if the POS already exists if yes dont add.
                                if ((oTOSPOS.IsExistsPOS(Convert.ToInt64(txtCode.Tag), txtCode.Text.Trim(), txtName.Text.Trim())))
                                {
                                    MessageBox.Show("Code is already in use by another entry.  Select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }                                
                                if (POSID > 0)
                                {
                                    //Modify
                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    //if(cmbFacilityType.SelectedValue!=null)
                                    //{
                                    //_FacilityType = Convert.ToInt32(((FacilityType)cmbFacilityType.Text));
                                  

                                    //} 
                                    //cmbFacilityType.Text = _FacilityType.ToString();   
                                    _tempResult = oTOSPOS.AddModifyPOS(Convert.ToInt64(txtCode.Tag), txtCode.Text.Trim(), txtName.Text.Trim(), txtDescription.Text.Trim(), _FacilityType);
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add  Place Of Service", 0, _tempResult, 0, ActivityOutCome.Success);

                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _POSID = 0;
                                        txtCode.Text = "";
                                        txtName.Text = "";                                      
                                        txtDescription.Text = "";
                                        oTOSPOS.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                                    }

                                }
                                else
                                {
                                    //Add New
                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    _tempResult = oTOSPOS.AddModifyPOS(0, txtCode.Text.Trim(), txtName.Text.Trim(), txtDescription.Text.Trim(), _FacilityType);
                                    _POSID = _tempResult;
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add  Place Of Service", 0, _tempResult, 0, ActivityOutCome.Success);

                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _POSID = 0;
                                        txtCode.Text="";
                                        txtName.Text="";                                       
                                        txtDescription.Text = "";
                                        oTOSPOS.Dispose();
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.POS, ActivityType.Add, "Add Place Of Service", 0, _tempResult, 0, ActivityOutCome.Failure);
                                    }

                                }
                              

                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion " Tool Strip Event "

        #region " Private Methods "

        private void FillPOS(Int64 posId)
        {
            //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            CLsBL_TOSPOS oTOSPOS = new CLsBL_TOSPOS(_databaseconnectionstring);

            DataTable dtPOS = new DataTable();

            if (posId > 0)
            {
                dtPOS = oTOSPOS.GetPOS(POSID);
                if (dtPOS != null && dtPOS.Rows.Count > 0)
                {
                    txtCode.Text = dtPOS.Rows[0]["sPOSCode"].ToString();
                    txtCode.Tag = dtPOS.Rows[0]["nPOSID"].ToString();
                    txtName.Text = dtPOS.Rows[0]["sPOSName"].ToString();
                    txtDescription.Text = dtPOS.Rows[0]["sPOSDescription"].ToString();
                    //cmbFacilityType.DataSource = dtPOS;
                    //cmbFacilityType.ValueMember = dtPOS.Columns[4].ColumnName;
                    //cmbFacilityType.DisplayMember = dtPOS.Columns[4].ColumnName;                  
                    
                }

            }
        }

        private bool Validate()
        {
            try
            {
                if (txtCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter code for Place of Service.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return false;
                }
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Enter name for Place of Service.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.Focus();
                    return false;
                }
                //if (txtDescription.Text == "")
                //{
                //    MessageBox.Show("Please enter Description for Place Of Service", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txtName.Focus();
                //    return false;
                //}
                return true;
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;                
            }
            finally
            { 
                
            }
        }

        #endregion " Private Methods "

    }
}