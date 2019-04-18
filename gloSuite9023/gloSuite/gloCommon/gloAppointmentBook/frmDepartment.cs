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
    internal partial class frmDepartment : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private Int64 _DeptID = 0;
        private Int64 _ReturnDeptID = 0;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region Constructor

        public frmDepartment(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
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

        public frmDepartment(Int64 DeptID, string databaseconnectionstring)
        {
            InitializeComponent();
            _DeptID = DeptID;
            _databaseconnectionstring = databaseconnectionstring;
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

        #endregion

        #region  " Property Procedures "

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }
        public Int64 ReturnDeptID
        {
            get { return _ReturnDeptID; }
            set { _ReturnDeptID = value; }
        }

        #endregion  " Property Procedures "

        #region " Form Load "

          private void frmDepartment_Load(object sender, EventArgs e)
        {
            try
            {
                //fill the location combo with the Locations
                FillLocationCombo();

                if (_DeptID != 0)
                {
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    DataTable dt;
                    String strQuery = "";
                    //strQuery = "SELECT ISNULL(sDepartment,'') as sDepartment,isnull(nLocationID,0) as nLocationID FROM AB_Department WHERE nDepartmentID = " + _DeptID.ToString() + " and bIsBlocked = 0 and nClinicID = 1";
                    strQuery = "SELECT ISNULL(sDepartment,'') as sDepartment,isnull(nLocationID,0) as nLocationID FROM AB_Department WHERE nDepartmentID = " + _DeptID.ToString();
                    oDB.Retrive_Query(strQuery.ToString(), out  dt);
                    oDB.Disconnect();
                    oDB.Dispose();
                    strQuery = null;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtDepartment.Text = dt.Rows[0]["sDepartment"].ToString(); // Resourse Description

                            //set the Location combo's selected index to the respective Location
                            cmbLocation.SelectedValue = dt.Rows[0]["nLocationID"];

                        }
                    }
                    dt.Dispose();
                }
                txtDepartment.Select();
            }//try
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

        #region " Fill Methods "

        private void FillLocationCombo()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                //String strQuery = "select nLocationID , sLocation from AB_Location where bIsBlocked = 0 and nClinicID = 1";
                String strQuery = "SELECT nLocationID , isnull(sLocation,'') as sLocation FROM AB_Location WHERE  bIsBlocked = 0 ";

                //connect to the database
                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out dt);

                cmbLocation.DataSource = dt;
                cmbLocation.DisplayMember = dt.Columns["sLocation"].ColumnName ;
                cmbLocation.ValueMember = "nLocationID";

                strQuery = null;
                //disconnect the database connection
                oDB.Disconnect();
            
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
            finally
            {
               if (oDB != null)
                {
                    oDB.Dispose();
                }//if
            }
        }//FillLocationCombo

        #endregion " Fill Methods "

        #region " Tool Strip Events "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (txtDepartment.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Department.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDepartment.Select();
                            break;
                        }

                        Books.Department oDept = new Books.Department(_databaseconnectionstring);


                        oDept.DepartmentID = _DeptID;
                        oDept.DepartmentName = txtDepartment.Text.Trim();
                        oDept.LocationID = Convert.ToInt64(cmbLocation.SelectedValue);
                        oDept.Location = cmbLocation.SelectedText.ToString();
                        oDept.IsBlocked = false;
                        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                        //oDept.ClinicID = 1;
                        oDept.ClinicID = ClinicID;
                        //

                        if (oDept.IsExists(_DeptID, txtDepartment.Text.Trim()) == true)
                        {
                            MessageBox.Show("Department with same name already exists, Enter different Department name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDepartment.Select();
                            break;
                        }

                        if (_DeptID == 0)
                        {
                            _ReturnDeptID = oDept.Add();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Department", 0, _ReturnDeptID, 0, ActivityOutCome.Success);

                            if (_ReturnDeptID <= 0)
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Department not added, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDepartment.Select();
                                break;
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Department", 0, _DeptID, 0, ActivityOutCome.Success);
                            //record not modified successfully
                            if (oDept.Modify() == false)
                            {
                                MessageBox.Show("Department not modified, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDepartment.Select();
                                break;
                            }
                        }

                        // Record is Added Successfully
                        //frmViewResourceType oRefResTypes;
                        //oRefResTypes = (frmViewResourceType)Owner.ActiveMdiChild;
                        //oRefResTypes.FillResourceTypes();
                        txtDepartment.Text = "";
                        txtDepartment.Select();
                        //'oRefResTypes = null;

                        if (_DeptID != 0)
                        {
                            this.Close();
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    default:
                        break;
                    case "Save":
                        if (txtDepartment.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter the Department.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDepartment.Select();
                            break;
                        }

                        Books.Department oDept1 = new Books.Department(_databaseconnectionstring);


                        oDept1.DepartmentID = _DeptID;
                        oDept1.DepartmentName = txtDepartment.Text.Trim();
                        oDept1.LocationID = Convert.ToInt64(cmbLocation.SelectedValue);
                        oDept1.Location = cmbLocation.SelectedText.ToString();
                        oDept1.IsBlocked = false;
                        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                        //oDept1.ClinicID = 1;
                        oDept1.ClinicID = ClinicID;
                        //

                        if (oDept1.IsExists(_DeptID, txtDepartment.Text.Trim()) == true)
                        {
                            MessageBox.Show("Department with same name already exists, Enter different Department name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDepartment.Select();
                            break;
                        }

                        if (_DeptID == 0)
                        {
                            _ReturnDeptID = oDept1.Add();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Department", 0, _ReturnDeptID, 0, ActivityOutCome.Success);

                            if (_ReturnDeptID <= 0)
                            {
                                // Record is Not Added Successfully
                                MessageBox.Show("Department not added, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDepartment.Select();
                                break;
                            }
                            else
                            {
                              
                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Add, "Add Department", 0, _DeptID, 0, ActivityOutCome.Success);
                            //record not modified successfully
                            if (oDept1.Modify() == false)
                            {
                                MessageBox.Show("Department not modified, Try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDepartment.Select();
                                break;
                            }
                        }

                        // Record is Added Successfully
                        //frmViewResourceType oRefResTypes;
                        //oRefResTypes = (frmViewResourceType)Owner.ActiveMdiChild;
                        //oRefResTypes.FillResourceTypes();
                        txtDepartment.Text = "";
                        txtDepartment.Select();
                        //'oRefResTypes = null;

                        if (_DeptID != 0)
                        {
                            _DeptID=0;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Tool Strip Events "

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }

    }
}