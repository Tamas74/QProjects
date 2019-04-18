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
    internal partial class frmSetupResourceType : Form
    {

        #region  " Enumerations "

        public enum ResourceType
        {
            Provider = 1,
            Equipment = 2,
            Other = 3
        }

        #endregion  " Enumerations "

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _resourseID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region  " Property Procedures "
        
        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        
        #endregion  " Property Procedures "

        #region " Constructor "
        
        public frmSetupResourceType()
        {
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

        public frmSetupResourceType(Int64 resourceID)
        {
            InitializeComponent();
            _resourseID = resourceID;
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

        private void frmSetupResourceType_Load(object sender, EventArgs e)
        {
            try
            {
                if (_resourseID == 0)
                { // New Resource
                    txtResourceType.Tag = ResourceType.Other;
                }
                else
                // Fill Resourse Data for Update
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    DataTable dt;
                    String strQuery = "";
                    strQuery = "SELECT ISNULL(sResourceTypeDescription,'') AS sResourceTypeDescription, nResourceType FROM AB_ResourceType_MST WHERE nResourceTypeID = " + _resourseID.ToString();
                    oDB.Retrive_Query(strQuery.ToString(), out  dt);
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtResourceType.Text = dt.Rows[0]["sResourceTypeDescription"].ToString(); // Resourse Description
                            txtResourceType.Tag = dt.Rows[0]["nResourceType"]; // ResourceType
                        }
                    }
                    dt.Dispose();
                }
                txtResourceType.Select();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (txtResourceType.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter the resource type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtResourceType.Select();
                            break;
                        }
                        if (txtResourceType.Tag.ToString() != "")
                        {
                            if ((ResourceType)txtResourceType.Tag != ResourceType.Other)
                            {
                                MessageBox.Show("This is system defined resource you can not modify it.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }

                        if (AddResource() == false)
                        {
                            txtResourceType.Select();
                            break;
                        }

                        if (_resourseID == 0)
                        {
                            // frmViewResourceType oRefResTypes;
                            //oRefResTypes = (frmViewResourceType)Owner.ActiveMdiChild;
                            //oRefResTypes.Fill_ResourceTypes();
                            txtResourceType.Text = "";
                            txtResourceType.Select();
                            //'oRefResTypes = null;
                        }
                        else
                        {
                            this.Close();
                        }
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
            }

        }

        #endregion " Tool Strip Event "

        #region " Methods "

        private Boolean AddResource()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameter oDBParameter = null;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {


                //  Add/Update Resource
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@ResourceID";
                // if _ResourseID=0 then AddNew 
                // if _ResourseID>0 then Update
                oDBParameter.Value = _resourseID;
                oDBParameter.DataType = SqlDbType.BigInt;
                oDBParameter.ParameterDirection = ParameterDirection.InputOutput;
                oDBParameters.Add(oDBParameter);

                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@ResourceTypeDescription";
                oDBParameter.Value = txtResourceType.Text.Trim();
                oDBParameter.DataType = SqlDbType.VarChar;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@nClinicID";
                oDBParameter.Value = this.ClinicID ;
                oDBParameter.DataType = SqlDbType.BigInt;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);


                oDB.Connect(false);
                // START - Check Duplicate
                int recCount = 0;
                recCount = (int)oDB.ExecuteScalar("AB_CheckDuplicate_ResourceType", oDBParameters);
                if (recCount > 0)
                {
                    MessageBox.Show("Resource type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtResourceType.Select();
                    return false;
                }
                // END Check Duplicate

                oDB.Disconnect();



                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@ResourceType";
                if ((ResourceType)txtResourceType.Tag == ResourceType.Other)
                {
                    oDBParameter.Value =(int)ResourceType.Other; // Other 
                }
                else
                {

                }
                oDBParameter.DataType = SqlDbType.Int;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                object resourseID;

                oDB.Connect(false);
                oDB.Execute("AB_INUP_ResourceType", oDBParameters, out resourseID);

                if (resourseID != null)
                {
                    _resourseID = (Int64)resourseID;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.ResourceType, ActivityType.Add, "Add Resource Type", 0, _resourseID, 0, ActivityOutCome.Failure);                

                }

                resourseID = null;
                oDBParameters.Clear();
                oDB.Disconnect();
                return true;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameter != null) { oDBParameter.Dispose(); oDBParameter = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }

        #endregion " Methods "

        private void txtResourceType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }


    }
}