using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail; 

namespace gloAppointmentScheduling
{
    public partial class frmAddNotes : Form
    {
        private Int64 _ClinicID = 0;
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private Boolean _bAppointmentNotes = true;
        private Int64  _nMSTAppointmentID = 0;
        private Int64 _nDTLAppointmentID = 0;
        private string _sNotesPrefix = "";
        private Int32 _nAppointmentStatusID = 0;
        

        // removed unused variable '_GenerateHL7Message' by Abhijeet on 20110920
        // Added variable by Abhijeet on 20110609 for savinf registry setting value for HL7 Outbound generation
        //private Boolean _GenerateHL7Message = false;
        // End of changes by Abhijeet on 20110609 for savinf registry setting value for HL7 Outbound generation

        public Boolean NotesType
        {
            get { return _bAppointmentNotes; }
            set { _bAppointmentNotes = value; }
        }

        public Int64 MSTAppointmentID
        {
            get { return _nMSTAppointmentID; }
            set { _nMSTAppointmentID = value; }
        }

        public Int64 DTLAppointmentID
        {
            get { return _nDTLAppointmentID; }
            set { _nDTLAppointmentID = value; }
        }

        public string  NotesPrefix
        {
            get { return _sNotesPrefix; }
            set { _sNotesPrefix = value; }
        }

        public Int32 nAppointmentStatusID
        {
            get { return _nAppointmentStatusID; }
            set { _nAppointmentStatusID = value; }
        }

        


        public frmAddNotes(String DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;

            _bAppointmentNotes = true;

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

            #region "GenerateHL7Message"
            // below code commented by Abhijeet on 20110920 and call gloHL7 class function to read registry

            //Added by Abhijeet on 20110609 
            //if (appSettings["GenerateHL7Message"] != null)
            //{
            //    if (appSettings["GenerateHL7Message"] != "")
            //    {
            //        if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
            //        {
            //            _GenerateHL7Message = true;
            //        }
            //    }
            //}
            //End of changes by Abhijeet on 20110609 
            //End of changes to above code commented by Abhijeet on 20110920 

            gloHL7.HL7OutboundSettings(_databaseconnectionstring);
            #endregion
         
        }

        public frmAddNotes(String DatabaseConnectionString, Boolean bNotesType)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _bAppointmentNotes = bNotesType;            

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

            #region "GenerateHL7Message"
            // below code commented by Abhijeet on 20110920 and call gloHL7 class function to read registry
            //Added by Abhijeet on 20110609 
            //if (appSettings["GenerateHL7Message"] != null)
            //{
            //    if (appSettings["GenerateHL7Message"] != "")
            //    {
            //        if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
            //        {
            //            _GenerateHL7Message = true;
            //        }
            //    }
            //}
            //End of changes by Abhijeet on 20110609 
            //End of changes to above code commented by Abhijeet on 20110920 

            gloHL7.HL7OutboundSettings(_databaseconnectionstring);
            #endregion

        }

        private void frmAddNotes_Load(object sender, EventArgs e)
        {
            FillNotes();
        }

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            
                if (_bAppointmentNotes == true)
                {
                    gloAppointmentScheduling.gloAppointment ogloAppointments = new gloAppointment(_databaseconnectionstring);
                    try
                    {
                        string _sNotes = string.Empty;

                        if (string.Compare(txtNotes.Text.Trim(), "", true) != 0)
                        {
                            if (txtNotes.Text.Trim().Length >= 985)
                            {
                                _sNotes = "\n" + txtNotes.Text.Trim();
                            }
                            else
                            {
                                _sNotes = "\n" + _sNotesPrefix + " " + txtNotes.Text.Trim();
                            }
                        }
                       
                       // ogloAppointments.AddNotes(_nMSTAppointmentID, _nDTLAppointmentID, "\n" + _sNotesPrefix + " " + txtNotes.Text.Trim());
                        ogloAppointments.AddNotes(_nMSTAppointmentID, _nDTLAppointmentID, _sNotes ,_nAppointmentStatusID );
                        
                        DataTable dtpatientDetail = ogloAppointments.GetPatient(_nMSTAppointmentID, _nDTLAppointmentID);
                        if (dtpatientDetail != null)
                        {
                            long nPatientId = Convert.ToInt64(dtpatientDetail.Rows[0]["nPatientID"]);
                            long nProviderId = Convert.ToInt64(dtpatientDetail.Rows[0]["nASBaseID"]);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Notes added to appointment", nPatientId, _nMSTAppointmentID, nProviderId, ActivityOutCome.Success);
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Appointment, ActivityCategory.ViewAppointment, ActivityType.Modify, "Notes added to appointment", 0, _nMSTAppointmentID, 0, ActivityOutCome.Success);
                        }
                        #region "Generate HL7 Message Queue for New Appointment"
                        //Code Added by Abhijeet for generating appointment cancle entry in HL7 message queue                    
                        if ((gloHL7.boolSendAppointmentDetails) && (string.Compare (_sNotesPrefix, " Cancel Notes :", true) != 0))
                        {
                            if (_nMSTAppointmentID > 0)
                            {
                                gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
                                DataTable dtpatient = ogloAppointment.GetPatient(_nMSTAppointmentID, _nDTLAppointmentID);
                                if (ogloAppointment != null)
                                {
                                    ogloAppointment.Dispose();
                                    ogloAppointment = null;
                                }
                                if (dtpatient != null)
                                {
                                    long nPatientId = Convert.ToInt64(dtpatient.Rows[0]["nPatientID"]);
                                    if (nPatientId > 0)
                                    {
                                        gloHL7.InsertInMessageQueue("S13", nPatientId, _nMSTAppointmentID, _nDTLAppointmentID.ToString(), _databaseconnectionstring);
                                    }
                                    if (dtpatient != null)
                                    {
                                        dtpatient.Dispose();
                                        dtpatient = null;
                                    }
                                }

                            }
                        }
                        //End of Code Added by Abhijeet for generating appointment cancle entry in HL7 message queue
                        #endregion


                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (ogloAppointments != null) { ogloAppointments.Dispose(); }
                    }

                }
                else
                {
                    //try
                    //{
                    //    //code added byte dipak to fix mantis bug id  	 0000235: Schedule>Add Notes> Database error is displayed on trying to save text of more than 250 characters in 'Add notes' dialog box.
                    //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    //    string _strSQL = "SELECT sNotes FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMSTAppointmentID + " AND nDTLScheduleID = " + _nDTLAppointmentID + " AND nClinicID = " + _ClinicID + "";
                    //    object _strNotes;
                    //    oDB.Connect(false);
                    //    _strNotes = oDB.ExecuteScalar_Query(_strSQL).ToString();
                    //    _strNotes = _strNotes + "\n" + _sNotesPrefix + " " + txtNotes.Text.Trim();
                    //    if (_strNotes.ToString().Length > 1000)
                    //    {
                    //        MessageBox.Show("The maximum capacity of note is 1000 characters, you are exceeding the limit " + _strNotes.ToString().Length, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        txtNotes.Focus();
                    //        return;
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.Notes, ActivityType.Add, "ERROR in Save Click of Notes: While Accessing sNotes of table AS_Schedule_DTL " + ex.ToString(), 0, _nMSTAppointmentID, 0, ActivityOutCome.Failure);
                    //}
                    //end code added by dipak 20091113

                    gloAppointmentScheduling.gloSchedule ogloSchedule = new gloSchedule(_databaseconnectionstring);
                    try
                    {

                        string _sNotes = string.Empty;

                        if (string.Compare(txtNotes.Text.Trim(), "", true) != 0)
                        {
                            if (txtNotes.Text.Trim().Length >= 985)
                            {
                                _sNotes = "\n" + txtNotes.Text.Trim();
                            }
                            else
                            {
                                _sNotes = "\n" + _sNotesPrefix + " " + txtNotes.Text.Trim();
                            }
                        }
                        //ogloSchedule.AddNotes(_nMSTAppointmentID, _nDTLAppointmentID, "\n" + _sNotesPrefix + " " + txtNotes.Text.Trim());
                        ogloSchedule.AddNotes(_nMSTAppointmentID, _nDTLAppointmentID, _sNotes);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Scheduling, ActivityCategory.ViewSchedule, ActivityType.Modify, "Notes added to schedule", 0, _nMSTAppointmentID, 0, ActivityOutCome.Success);
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (ogloSchedule != null) { ogloSchedule.Dispose(); }
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel ;  
            this.Close();  
        }

        private void FillNotes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object strNotes = new object();
            string strSQL = "";
           // DateTime _result = DateTime.Now;
           //  string _UserName = "";

            //if (appSettings["UserName"] != null)
            //{
            //    if (appSettings["UserName"] != "")
            //    { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
            //    else
            //    { _UserName = ""; }
            //}
            //else
            //{ _UserName = ""; }

            try
            {

                if (_bAppointmentNotes == true)
                {
                    strSQL = "SELECT sNotes FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + _nMSTAppointmentID + " AND nDTLAppointmentID = " + _nDTLAppointmentID + " AND nClinicID = " + _ClinicID + "";
                }
                else if(_bAppointmentNotes == false)
                {
                    strSQL = "SELECT sNotes FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMSTAppointmentID + " AND nDTLScheduleID = " + _nDTLAppointmentID + " AND nClinicID = " + _ClinicID + "";
                }
                
                oDB.Connect(false);
                strNotes = oDB.ExecuteScalar_Query(strSQL).ToString();

                if (strNotes.ToString() != "")
                {
                    txtNotes.Text = strNotes.ToString() + "\n"; //+ _result + " " + _UserName + " : ";
                    Int32 txtLength= txtNotes.Text.ToString().Length;
                    txtNotes.SelectionStart = txtLength;
                }
                //else
                //{
                //    txtNotes.Text = _result + " " + _UserName + " : ";
                //    Int32 txtLength = txtNotes.Text.ToString().Length;
                //    txtNotes.SelectionStart = txtLength;
                //}
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally 
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
          
            }

          
            
        }
    }
}