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
    public partial class frmSetupTOS : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _TOSID = 0;
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

        public Int64 TOSID
        {
            get { return _TOSID; }
            set { _TOSID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupTOS(string DatabaseConnectionString, Int64 TOSId)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _TOSID = TOSId;
            InitializeComponent();
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
         

        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupTOS_Load(object sender, EventArgs e)
        {
            try
            {
                FillTOS(TOSID);
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
            Int64 _tempresult = 0;
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                            if (ValidateData())
                            {

                                //Check if TOS already exists if Yes do not add
                                if ((oResource.IsExistsTOS(Convert.ToInt64(txtName.Tag), txtName.Text)))
                                {
                                    MessageBox.Show("Type of service name is alredy in use by another entry.  Please select a unique name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                //Add
                                if (TOSID > 0)
                                {

                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    _tempresult = oResource.AddModifyTOS(Convert.ToInt64(txtName.Tag), txtName.Text);
                                    if (_tempresult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.TOS, ActivityType.Add, "Add TOS", 0, _tempresult, 0, ActivityOutCome.Success);                

                                        //MessageBox.Show("Record Modified Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        oResource.Dispose();
                                    }
                                }
                                else
                                {
                                    //Modify
                                    //Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                                    _tempresult = oResource.AddModifyTOS(0, txtName.Text);
                                    if (_tempresult > 0)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.TOS, ActivityType.Add, "Add TOS", 0, _tempresult,0, ActivityOutCome.Success);                

                                        //MessageBox.Show("Record Added Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        oResource.Dispose();
                                    }
                                }

                                this.Close(); 
                            }
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {

            }
        }

        #endregion " Tool Strip Event "

        #region " Private & Public Methods "

        private void FillTOS(Int64 tosId)
        {
            Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
            DataTable dtTOS = new DataTable();

            try
            {
                if (tosId > 0)
                {
                    dtTOS = oResource.GetTOS(TOSID);
                    if (dtTOS != null && dtTOS.Rows.Count > 0)
                    {
                        //nTOSID,sDescription
                        txtName.Text = dtTOS.Rows[0]["sDescription"].ToString();
                        txtName.Tag = dtTOS.Rows[0]["nTOSID"].ToString();

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oResource.Dispose();
                dtTOS.Dispose();
            }
        }

        private bool ValidateData()
        {

            try
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please enter the type of service.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

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

        #endregion " Private & Public Methods "

        
    }
}