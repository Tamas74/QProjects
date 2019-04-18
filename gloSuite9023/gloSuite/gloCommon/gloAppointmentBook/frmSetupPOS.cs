using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloAppointmentBook
{
    public partial class frmSetupPOS : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
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

            #region " Retrieve MessageBoxCaption from AppSettings "

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

             #endregion

             InitializeComponent();

           
         
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
                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
        }

         #endregion " Form Load "

        #region " Tool Strip Event "

        private void tls_SetupResource_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Int64 _tempResult = 0;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {

                            if (ValidateData())
                            {
                                Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                //Check if the POS already exists if yes dont add.
                                if ((oResource.IsExistsPOS(Convert.ToInt64(txtCode.Tag), txtCode.Text, txtName.Text)))
                                {
                                    MessageBox.Show("Code is alredy in use by another entry.  Please select a unique code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (POSID > 0)
                                {
                                    //Modify
                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    _tempResult = oResource.AddModifyPOS(Convert.ToInt64(txtCode.Tag), txtCode.Text, txtName.Text, txtDescription.Text);

                                    if (_tempResult > 0)
                                    {
                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.POS, ActivityType.Add, "Add POS", 0, _tempResult, 0, ActivityOutCome.Success);                


                                        oResource.Dispose();
                                    }

                                }
                                else
                                {
                                    //Add New
                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    _tempResult = oResource.AddModifyPOS(0, txtCode.Text, txtName.Text, txtDescription.Text);
                                    if (_tempResult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.POS, ActivityType.Add, "Add POS", 0, _tempResult, 0, ActivityOutCome.Success);                

                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        oResource.Dispose();
                                    }

                                }

                                this.Close(); 
                            }
                        }//Case "OK"
                        break;
                    case "Cancel" :
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);

            }
        }

        #endregion " Tool Strip Event "

        #region " Private Methods "

        private void FillPOS(Int64 posId)
        {
            Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            DataTable dtPOS = null;

            try
            {
                if (posId > 0)
                {
                    dtPOS = oResource.GetPOS(POSID);
                    if (dtPOS != null && dtPOS.Rows.Count > 0)
                    {
                        txtCode.Text = dtPOS.Rows[0]["sPOSCode"].ToString();
                        txtCode.Tag = dtPOS.Rows[0]["nPOSID"].ToString();
                        txtName.Text = dtPOS.Rows[0]["sPOSName"].ToString();
                        txtDescription.Text = dtPOS.Rows[0]["sPOSDescription"].ToString();

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oResource != null) { oResource.Dispose(); oResource = null; }
                if (dtPOS != null) { dtPOS.Dispose(); dtPOS = null; }
            }
        }

        private bool ValidateData()
        {
            try
            {
                if (txtCode.Text == "")
                {
                    MessageBox.Show("Please enter code for place of service.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return false;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter name for place of service.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
                return false;                
            }
            finally
            { 
                
            }
        }

        #endregion " Private Methods "

    }
}