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
    internal partial class frmSetupAppointmentBlockType : Form
    {
        
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _appointmentBlockTypeID = 0;
        private Int64 _ReturnappointmentBlockTypeID = 0;


       
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region " Constructor "

        public frmSetupAppointmentBlockType(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
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

        public frmSetupAppointmentBlockType(Int64 appointmentBlockTypeID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _appointmentBlockTypeID = appointmentBlockTypeID;
            _databaseconnectionstring = DatabaseConnectionString;
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

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 ReturnappointmentBlockTypeID
        {
            get { return _ReturnappointmentBlockTypeID; }
            set { _ReturnappointmentBlockTypeID = value; }
        }
        #endregion  " Property Procedures "

        #region " Form Load "

        private void frmSetupAppointmentBlockType_Load(object sender, EventArgs e)
        {
            try
            {
                if (_appointmentBlockTypeID == 0)
                { // New Appointment Block Type 
                }
                else
                // Fill Appointment Block Type Data for Update
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    DataTable dt;
                    String strQuery = "";
                    strQuery = "SELECT ISNULL(sAppointmentBlockType,'') as sAppointmentBlockType FROM AB_AppointmentBlockType WHERE nAppointmentBlockTypeID= " + _appointmentBlockTypeID.ToString();
                    oDB.Retrive_Query(strQuery.ToString(), out  dt);
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtAppBlockType.Text = dt.Rows[0]["sAppointmentBlockType"].ToString(); // Resourse Description
                        }
                    }
                    dt.Dispose();
                }
                txtAppBlockType.Select();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion " Form Load "

        #region " Methods "

        private Boolean AddAppointmentBlockType()
        {
            Books.AppointmentBlockType oAppointmentBlockType = new Books.AppointmentBlockType(_databaseconnectionstring);

            oAppointmentBlockType.AppointmentBlockTypeID = _appointmentBlockTypeID;
            oAppointmentBlockType.AppointmentBlockTypeName = txtAppBlockType.Text.Trim();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                //AB_INUP_AppointmentBlockType
                //@AppointmentBlockTypeID, @AppointmentBlockType, @IsBlocked, @ClinicID

                //  Add/Update Resource
                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@AppointmentBlockTypeID";
                // if _ResourseID=0 then AddNew 
                // if _ResourseID>0 then Update
                oDBParameter.Value = _appointmentBlockTypeID;
                oDBParameter.DataType = SqlDbType.BigInt;
                oDBParameter.ParameterDirection = ParameterDirection.InputOutput;
                oDBParameters.Add(oDBParameter);

                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@AppointmentBlockType";
                oDBParameter.Value = txtAppBlockType.Text.Trim();
                oDBParameter.DataType = SqlDbType.VarChar;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@IsBlocked";
                oDBParameter.Value = 0;
                oDBParameter.DataType = SqlDbType.Bit ;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@ClinicID";
                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                //oDBParameter.Value = 1;
                oDBParameter.Value = ClinicID;
                //
                oDBParameter.DataType = SqlDbType.BigInt ;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                oDB.Connect(false);
               
                // START - Check Duplicate
               
                // END Check Duplicate

                oDBParameter = new gloDatabaseLayer.DBParameter();
                oDBParameter.ParameterName = "@ResourceType";
                oDBParameter.Value = 0;
                oDBParameter.DataType = SqlDbType.Int;
                oDBParameter.ParameterDirection = ParameterDirection.Input;
                oDBParameters.Add(oDBParameter);

                object resourseID;
                oDB.Execute("AB_INUP_ResourceType", oDBParameters, out resourseID);

                if (resourseID != null)
                {
                    _appointmentBlockTypeID = (Int64)resourseID;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentBlockType,  ActivityType.Add, "Add Appointment Block Type", 0, _appointmentBlockTypeID, 0, ActivityOutCome.Failure);                
  
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
            }
        }

        #endregion " Methods "

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            Books.AppointmentBlockType oAppointmentBlockType = new Books.AppointmentBlockType(_databaseconnectionstring);
            Books.AppointmentBlockType oAppointmentBlockType1 = new Books.AppointmentBlockType(_databaseconnectionstring);
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (txtAppBlockType.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Appointment Block Type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppBlockType.Select();
                            break;
                        }


                        oAppointmentBlockType.AppointmentBlockTypeID = _appointmentBlockTypeID;
                        oAppointmentBlockType.AppointmentBlockTypeName = txtAppBlockType.Text.Trim();

                        if (oAppointmentBlockType.IsExists(_appointmentBlockTypeID, txtAppBlockType.Text.Trim()) == true)
                        {
                            MessageBox.Show("Appointment Block Type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppBlockType.Select();
                            break;
                        }  // IS EXists

                        if (_appointmentBlockTypeID == 0)
                        {  // FOR New 
                            _ReturnappointmentBlockTypeID = oAppointmentBlockType.Add();
                            if (_ReturnappointmentBlockTypeID > 0)
                            {
                                // Record is Added Successfully
                                _appointmentBlockTypeID = 0;
                                txtAppBlockType.Text = "";
                                txtAppBlockType.Select();
                                this.Close();
                                break;
                            }
                            else
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Appointment Block Type is not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppBlockType.Select();
                                break;
                            }
                        }  //if (_appointmentBlockTypeID == 0)
                        else
                        {
                            if (oAppointmentBlockType.Modify() == true)
                            {
                                this.Close();
                            }
                            else
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Appointment Block Type is not modified.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppBlockType.Select();
                                break;
                            }
                        }

                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        if (txtAppBlockType.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Appointment Block Type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppBlockType.Select();
                            break;
                        }


                        oAppointmentBlockType1.AppointmentBlockTypeID = _appointmentBlockTypeID;
                        oAppointmentBlockType1.AppointmentBlockTypeName = txtAppBlockType.Text.Trim();

                        if (oAppointmentBlockType1.IsExists(_appointmentBlockTypeID, txtAppBlockType.Text.Trim()) == true)
                        {
                            MessageBox.Show("Appointment Block Type already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAppBlockType.Select();
                            break;
                        }  // IS EXists

                        if (_appointmentBlockTypeID == 0)
                        {  // FOR New 
                            _ReturnappointmentBlockTypeID = oAppointmentBlockType1.Add();
                            if (_ReturnappointmentBlockTypeID > 0)
                            {
                                // Record is Added Successfully
                                _appointmentBlockTypeID = 0;
                                txtAppBlockType.Text = "";
                                txtAppBlockType.Select();
                                _appointmentBlockTypeID = 0;
                                break;
                            }
                            else
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Appointment Block Type is not added.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppBlockType.Select();
                                break;
                            }
                        }  //if (_appointmentBlockTypeID == 0)
                        else
                        {
                            if (oAppointmentBlockType1.Modify() == true)
                            {
                                _appointmentBlockTypeID = 0;
                                txtAppBlockType.Text = "";
                            }
                            else
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Appointment Block Type is not modified.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAppBlockType.Select();
                                break;
                            }
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAppointmentBlockType != null) { oAppointmentBlockType.Dispose(); oAppointmentBlockType = null; }
                if (oAppointmentBlockType1 != null) { oAppointmentBlockType1.Dispose(); oAppointmentBlockType1 = null; }
            }

        }

        #endregion " Tool Strip Event "

        private void txtAppBlockType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }

    }
}