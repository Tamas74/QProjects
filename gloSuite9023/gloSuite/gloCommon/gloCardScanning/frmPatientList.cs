using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloCardScanning;

namespace gloCardScanningPatientList
{
    public partial class frmPatientList : Form
    {
        public frmPatientList(DataTable _dtPatient)
        {
            InitializeComponent();
           // _databaseconnectionstring = databaseconnectionstring;
           // _pmdatabaseconnectionstring = PMDatabaseConnectionString;
           // _clinicid = ClinicID;
           // //_enumemrpm = enumemrpm;
           // _firstName = FirstName;
           // _lastName = LastName;
           // _dateOfBirth = DOB;
           //// _gender = Gender; 
            dtPatients = _dtPatient;

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        private string _messageBoxCaption = "";
        private string _databaseconnectionstring = "";
        //private string _pmdatabaseconnectionstring = "";
        //private Int64 _clinicid = 0;
        //private EnumEMRPM _enumemrpm = EnumEMRPM.None;
        string _firstName = "";
        string _lastName = "";
        DateTime _dateOfBirth = default(DateTime);
        //string _gender = "";
        DataTable dtPatients = null;//new DataTable();
        public bool DialogPatientResult = false;
        public Int64 DialogPatientID = 0;

        private void frmPatientList_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1PatientList, false);
            FillPatientList();
        }

        private void FillPatientList()
        {
            try
            {
                

                //DataTable dtPatients = GetPatients();

                if (dtPatients == null)
                {
                    dtPatients = new DataTable();
                }
           //     c1PatientList.Clear();
                c1PatientList.DataSource = null;
                c1PatientList.DataSource = dtPatients;
                c1PatientList.Refresh();

                if (c1PatientList.DataSource != null)
                {
                    c1PatientList.Cols["nPatientID"].Caption = "nPatientID";
                    c1PatientList.Cols["sPatientCode"].Caption = "Patient Code";
                    c1PatientList.Cols["sFirstName"].Caption = "First Name";
                    //c1PatientList.Cols["sMiddleName"].Caption = "Mid Name";
                    c1PatientList.Cols["sLastName"].Caption = "Last Name";
                    c1PatientList.Cols["dtDOB"].Caption = "DOB";
                    //c1PatientList.Cols["sGender"].Caption = "Gender"; 
                    c1PatientList.Cols["nPatientID"].Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);      
            }
        }

        private DataTable GetPatients()
        {
            //SqlConnection _Connection = null;  
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oDataTable = new DataTable();
            DataTable returnDataTable=null;
            try
            {
              //SLR: Always dateofbirth is null? What is the purpose of this query? 4/22/2014
                string _sqlQuery = "SELECT ISNULL(nPatientID,0) AS  nPatientID,ISNULL(sPatientCode,'') AS sPatientCode,ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,dtDOB,ISNULL(sGender,'') AS sGender FROM Patient WHERE " +
                                   " UPPER(sFirstName) = '" + _firstName.ToUpper().Trim() + "' AND " +
                                   " UPPER(sLastName)  = '" + _lastName.ToUpper().Trim() + "' AND " +
                                   " UPPER(dtDOB) = " + _dateOfBirth + "";
                oDB.Retrive_Query(_sqlQuery, out oDataTable);

                returnDataTable = oDataTable;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(sqlEx.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                  if (oDataTable != null)
                {
                    
                    oDataTable.Dispose();
                    oDataTable = null;
                }
                
            }
          
            return returnDataTable;//oDataTable; 
        }

        private void tlsp_btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1PatientList.Rows.Count > 1)
                {
                    if (c1PatientList.Row >= 1)
                    {
                        DialogPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.Row, 0));
                        DialogPatientResult = true;
                        this.Close();
                    }
                    else
                    {
                        DialogPatientID = 0;
                        DialogPatientResult = false; 
                    }
                }
                else
                {
                    DialogPatientID = 0;
                    DialogPatientResult = false;
                }
            }
            catch (Exception ex)
            {
                DialogPatientID = 0;
                DialogPatientResult = false;
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlsp_btnCancel_Click(object sender, EventArgs e)
        {
            DialogPatientID = 0;
            DialogPatientResult = false;
            this.Close();
        }

        private void c1PatientList_DoubleClick(object sender, EventArgs e)
        {
           

            if (c1PatientList.RowSel>0)
            {
                 try
                {
                    if (c1PatientList.Rows.Count > 1)
                    {
                        if (c1PatientList.Row >= 1)
                        {
                            //tempTaskId = Convert.ToInt64(c1ViewTask.GetData(c1PatientList.RowSel, COL_TASKID));

                            DialogPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.Row, 0));
                            DialogPatientResult = true;
                            this.Close();
                        }
                        else
                        {
                            DialogPatientID = 0;
                            DialogPatientResult = false; 
                        }
                    }
                    else
                    {
                        DialogPatientID = 0;
                        DialogPatientResult = false;
                    }
                }
                catch (Exception ex)
                {
                    DialogPatientID = 0;
                    DialogPatientResult = false;
                    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void c1PatientList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
        private void DisposeAllEvent()
        {
            this.tlsp_btnOK.Click -= new System.EventHandler(this.tlsp_btnOK_Click);
            this.tlsp_btnCancel.Click -= new System.EventHandler(this.tlsp_btnCancel_Click);
            this.c1PatientList.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.c1PatientList_MouseMove);
            this.c1PatientList.DoubleClick -= new System.EventHandler(this.c1PatientList_DoubleClick);
            this.Load -= new System.EventHandler(this.frmPatientList_Load);
        }
        private void frmPatientList_Closed(object sender, EventArgs e)
        {
            DisposeAllEvent();
        }


    }
}