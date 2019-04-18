using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloPMGeneral.gloAppointmentsChargesLinking
{
    public partial class frmPatientAppointments : Form
    {

        public List<Int64> AppointmentIDs { get; set; }



        private string strConnectionString = "";
        private long PatientID = 0;
        private long FromDate = 0;
        private long ToDate = 0;

        private const int COL_Appointment_Select = 0;
        private const int COL_Appointment_AppointmentID = 1;
        private const int COL_Appointment_FromDate = 2;
        private const int COL_Appointment_Time = 3;
        //private const int COL_Appointment_ToDate = 3;
        private const int COL_Appointment_Provider = 4;
        private const int COL_Appointment_Location = 5;
        private const int COL_Appointment_Type = 6;
        private const int COL_Appointment_Status = 7;
        private const int COL_Appointment_Notes = 8;
        private const int COL_Appointment_ProviderID = 9;
        private const int COL_Appointment_LocationID = 10;
        private const int COL_Appointment_FacilityID = 11;
        private const int COL_Appointment_LocationAssociatedFacility = 12;
        private const int COL_Appointment_TypeID = 13;
        private const int COL_Appointment_DefaultDos=14;
        private const int COL_Appointment_DefaultFacility=15;
        private const int COL_Appointment_DefaultRenderingProvider=16;
        

        public bool _IsOnLoadform = false;
        public String _DefaultDos = "";
        public string _Location = "";
        public string _Provider = "";
        public Int64 _ProviderID = 0;
        public Int64 _LocationID = 0;
        public Int64 _FacilityID = 0;
        public Int64 _AppointmentID = 0;
        public Int64 _AppointmentTypeID = 0;
        string FacilityDesc = "";
        public bool bIsDefaultFacility = false;
        public bool bIsDefaultDOS = false;
        public bool bIsDefaultRenderingProvider = false;
        public Int64 nPatientID = 0;
        public Int64 LastAppointmentID = 0;

        public string DefaultDos
        {
            get { return _DefaultDos; }
            set { _DefaultDos = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public string Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }
        public Int64 ProviderID
        {
            get { return _ProviderID; }
            set { _ProviderID = value; }
        }
        public Int64 LocationID
        {
            get { return _LocationID; }
            set { _LocationID = value; }
        }
        public Int64 FacilityID
        {
            get { return _FacilityID; }
            set { _FacilityID = value; }
        }
        public Int64 AppointmentID
        {
            get { return _AppointmentID; }
            set { _AppointmentID = value; }

        }
        public Int64 AppointmentTypeID
        {
            get { return _AppointmentTypeID; }
            set { _AppointmentTypeID = value; }

        }
        

        [DefaultValue(false)]
        public bool AppointmentsSet { get; set; }

        [DefaultValue(0)]
        public Int64 ChargeID { get; set; }        

        public List<Int64> Appointments { get; set; }

        [DefaultValue(null)]
        public DataTable AppointmentsData { get; set; }
        public DataSet PatientAppointments { get; set; }
      
        public frmPatientAppointments()
        {
            InitializeComponent();

            this.Appointments = new List<long>();
        }

        public frmPatientAppointments(string DbConn,long PatID,long From ,long To)
        {
            InitializeComponent();
            PatientID = PatID;
            FromDate = From;
            ToDate = To;
            strConnectionString = DbConn;

            this.Appointments = new List<long>();
        }

        public frmPatientAppointments(string DbConn, long PatID, long From, long To, Int64 ChargeID)
        {
            InitializeComponent();
            PatientID = PatID;
            FromDate = From;
            ToDate = To;
            strConnectionString = DbConn;
            this.ChargeID = ChargeID;

            this.Appointments = new List<long>();
        }

        private void DesignGrid()
        {
            
            c1Appointments.SetData(0, COL_Appointment_Select, "");
            c1Appointments.SetData(0, COL_Appointment_AppointmentID, "ID");
            c1Appointments.SetData(0, COL_Appointment_FromDate, "Appt. Date");
            //c1Appointments.SetData(0, COL_Appointment_ToDate, "To Date");
            c1Appointments.SetData(0, COL_Appointment_Provider, "Provider/Resource");
            c1Appointments.SetData(0, COL_Appointment_Location, "Location");
            c1Appointments.SetData(0, COL_Appointment_Type, "Type");
            c1Appointments.SetData(0, COL_Appointment_Status, "Status");
            c1Appointments.SetData(0, COL_Appointment_Notes, "Notes");
            c1Appointments.SetData(0, COL_Appointment_ProviderID, "Provider ID");
            c1Appointments.SetData(0, COL_Appointment_LocationID, "Location ID");
            c1Appointments.SetData(0, COL_Appointment_FacilityID, "Facility ID");
            c1Appointments.SetData(0, COL_Appointment_LocationAssociatedFacility, "Facility");
            c1Appointments.SetData(0,COL_Appointment_TypeID,"Appointment TypeID");
            c1Appointments.SetData(0,COL_Appointment_DefaultDos, "Default DOS");
            c1Appointments.SetData(0, COL_Appointment_DefaultFacility, "Default Facility");
            c1Appointments.SetData(0, COL_Appointment_DefaultRenderingProvider, "Default Rendering Provider");
            c1Appointments.SetData(0, COL_Appointment_Time, "Appt. Time");
            
            c1Appointments.Cols[COL_Appointment_AppointmentID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_FromDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            //c1Appointments.Cols[COL_Appointment_ToDate].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1Appointments.Cols[COL_Appointment_Provider].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_Location].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_Type].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_Status].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_Notes].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_ProviderID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_LocationID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_FacilityID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_TypeID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_DefaultDos].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_DefaultFacility].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_DefaultRenderingProvider].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Appointments.Cols[COL_Appointment_Time].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

            c1Appointments.Cols[COL_Appointment_Select].AllowEditing = true;
            c1Appointments.Cols[COL_Appointment_AppointmentID].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_FromDate].AllowEditing = false;
            //c1Appointments.Cols[COL_Appointment_ToDate].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_Provider].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_Location].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_Type].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_Status].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_Notes].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_ProviderID].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_LocationID].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_FacilityID].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_TypeID].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_DefaultDos].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_DefaultFacility].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_DefaultRenderingProvider].AllowEditing = false;
            c1Appointments.Cols[COL_Appointment_Time].AllowEditing = false;

            c1Appointments.Cols[COL_Appointment_Select].Width = Width / 24;
            c1Appointments.Cols[COL_Appointment_AppointmentID].Width = 0;
            c1Appointments.Cols[COL_Appointment_FromDate].Width = Width / 8;
            //c1Appointments.Cols[COL_Appointment_ToDate].Width = Width / 10;
            c1Appointments.Cols[COL_Appointment_Provider].Width = Width / 6;
            c1Appointments.Cols[COL_Appointment_Location].Width = Width / 9;
            c1Appointments.Cols[COL_Appointment_Type].Width = Width / 7;
            c1Appointments.Cols[COL_Appointment_Status].Width = Width / 12;
            //c1Appointments.Cols[COL_Appointment_Notes].Width = Width / 6;
            c1Appointments.Cols[COL_Appointment_ProviderID].Width = 0;
            c1Appointments.Cols[COL_Appointment_LocationID].Width = 0;
            c1Appointments.Cols[COL_Appointment_FacilityID].Width = 0;
            c1Appointments.Cols[COL_Appointment_LocationAssociatedFacility].Width = Width / 12;
            c1Appointments.Cols[COL_Appointment_TypeID].Width = 0;
            c1Appointments.Cols[COL_Appointment_DefaultDos].Width = 0;
            c1Appointments.Cols[COL_Appointment_DefaultFacility].Width = 0;
            c1Appointments.Cols[COL_Appointment_DefaultRenderingProvider].Width = 0;

            c1Appointments.Cols[COL_Appointment_Select].DataType = typeof(Boolean);
            if (_IsOnLoadform == false)
            {
                c1Appointments.Cols[COL_Appointment_LocationAssociatedFacility].Width = 0;
                c1Appointments.Cols[COL_Appointment_Provider].Width = Width / 4;
                c1Appointments.Cols[COL_Appointment_Location].Width = Width / 8;
                c1Appointments.Cols[COL_Appointment_Type].Width = Width / 6;
                c1Appointments.Cols[COL_Appointment_Status].Width = Width / 12;
            }
            c1Appointments.Cols[COL_Appointment_FromDate].DataType = typeof(System.DateTime);
            c1Appointments.Cols[COL_Appointment_FromDate].Format = "MM/dd/yyyy";
            c1Appointments.Cols[COL_Appointment_Time].DataType = typeof(System.DateTime);
            c1Appointments.Cols[COL_Appointment_Time].Format = "h:mm tt";

        }

        //private DataTable GetLinkedAppointments()
        //{
        //    DataTable dtAppointmet = new DataTable();
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(strConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {
        //        oDB.Connect(false);
        //        oDBParameters.Add("@nPatientId", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@nChargeID", ChargeID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@dtStartDateOfService", FromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@dtEndDateOfService", ToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDB.Retrive("AptChrgs_GetAssociatedPatientAppointments", oDBParameters, out dtAppointmet);
        //        return dtAppointmet;
        //    }

        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        DBErr.ERROR_Log(DBErr.ToString());
        //        System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return null;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDBParameters.Dispose();
        //        oDB.Dispose();
        //        dtAppointmet.Dispose();
        //    }
        //}

        //private DataTable GetAppointments()
        //{
        //    DataTable dtAppointmet = new DataTable();
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(strConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {
        //        oDB.Connect(false);
        //        oDBParameters.Add("@nPatientId", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@dtStartDateOfService", FromDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDBParameters.Add("@dtEndDateOfService", ToDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //        oDB.Retrive("AptChrgs_GetPatientAppointments", oDBParameters, out dtAppointmet);
        //        return dtAppointmet;
        //    }

        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        DBErr.ERROR_Log(DBErr.ToString());
        //        System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return null;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDBParameters.Dispose();
        //        oDB.Dispose();
        //        dtAppointmet.Dispose();
        //    }
        //}

        private void frmPatientAppointments_Load(object sender, EventArgs e)
        {

            if (_IsOnLoadform == false)
            {
                lblAppointmentHeader.Text = "Select appointment to link current claim";
                tls_ShowCheckInApointment.Visible = false;
                tls_ShowCheckOutAppointment.Visible = false;
                tls_ShowAllAppointment.Visible = false;
                this.AppointmentIDs = new List<long>();
                //c1Appointments.Clear();          
                c1Appointments.DataSource = null;
                c1Appointments.Rows.Fixed = 1;
                if (AppointmentsData != null && AppointmentsData.Rows.Count > 0)
                {
                    for (int i = 1; i <= AppointmentsData.Rows.Count; i++)
                    {
                        Int64 nAppointmentID = 0;

                        nAppointmentID = Convert.ToInt64(AppointmentsData.Rows[i - 1]["ID"]);

                        c1Appointments.Rows.Add();
                        c1Appointments.SetData(i, COL_Appointment_AppointmentID, Convert.ToString(AppointmentsData.Rows[i - 1]["ID"]));
                        c1Appointments.SetData(i, COL_Appointment_FromDate,Convert.ToDateTime(AppointmentsData.Rows[i - 1]["Appointment Date"]));
                        c1Appointments.SetData(i, COL_Appointment_Time, Convert.ToDateTime(AppointmentsData.Rows[i - 1]["Appointment Time"]));
                        c1Appointments.SetData(i, COL_Appointment_Provider, Convert.ToString(AppointmentsData.Rows[i - 1]["Provider"]));
                        c1Appointments.SetData(i, COL_Appointment_Location, Convert.ToString(AppointmentsData.Rows[i - 1]["Location"]));
                        c1Appointments.SetData(i, COL_Appointment_Type, Convert.ToString(AppointmentsData.Rows[i - 1]["Type"]));
                        c1Appointments.SetData(i, COL_Appointment_Status, Convert.ToString(AppointmentsData.Rows[i - 1]["Status"]));
                        c1Appointments.SetData(i, COL_Appointment_Notes, Convert.ToString(AppointmentsData.Rows[i - 1]["Notes"]));

                        if (this.Appointments.Contains(nAppointmentID))
                        {
                            c1Appointments.SetData(i, COL_Appointment_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
                        }
                    }
                }
            }
            else
            {
                lblAppointmentHeader.Text = "Select appointment to inherit charge details";
                tls_ShowAllAppointment.Visible = false;
                c1Appointments.DataSource = null;
                c1Appointments.Rows.Fixed = 1;
                DataTable PatientAppointment = PatientAppointments.Tables[0];

                if (PatientAppointment != null && PatientAppointment.Rows.Count > 0)
                {
                    for (int i = 1; i <= PatientAppointment.Rows.Count; i++)
                    {
                        Int64 nAppointmentID = 0;

                        nAppointmentID = Convert.ToInt64(PatientAppointment.Rows[i - 1]["ID"]);

                        c1Appointments.Rows.Add();
                        c1Appointments.SetData(i, COL_Appointment_AppointmentID, Convert.ToString(PatientAppointment.Rows[i - 1]["ID"]));
                        c1Appointments.SetData(i, COL_Appointment_FromDate, Convert.ToDateTime(PatientAppointment.Rows[i - 1]["Appointment Date"]));
                        c1Appointments.SetData(i, COL_Appointment_Time, Convert.ToDateTime(PatientAppointment.Rows[i - 1]["Appointment Time"]));
                        c1Appointments.SetData(i, COL_Appointment_Provider, Convert.ToString(PatientAppointment.Rows[i - 1]["Provider"]));
                        c1Appointments.SetData(i, COL_Appointment_Location, Convert.ToString(PatientAppointment.Rows[i - 1]["Location"]));
                        c1Appointments.SetData(i, COL_Appointment_Type, Convert.ToString(PatientAppointment.Rows[i - 1]["Type"]));
                        c1Appointments.SetData(i, COL_Appointment_Status, Convert.ToString(PatientAppointment.Rows[i - 1]["Status"]));
                        c1Appointments.SetData(i, COL_Appointment_Notes, Convert.ToString(PatientAppointment.Rows[i - 1]["Notes"]));
                        c1Appointments.SetData(i, COL_Appointment_ProviderID, Convert.ToString(PatientAppointment.Rows[i - 1]["ProviderID"]));
                        c1Appointments.SetData(i, COL_Appointment_LocationID, Convert.ToString(PatientAppointment.Rows[i - 1]["LocationID"]));
                        c1Appointments.SetData(i, COL_Appointment_FacilityID, Convert.ToString(PatientAppointment.Rows[i - 1]["FacilityID"]));
                        c1Appointments.SetData(i,COL_Appointment_TypeID,Convert.ToString(PatientAppointment.Rows[i-1]["AppointmentTypeID"]));
                        
                       
                        string Facility = Convert.ToString(PatientAppointment.Rows[i - 1]["FacilityCode"]);
                        c1Appointments.Rows[i].ComboList = Facility;
                        string[] tokens = Facility.Split('|');

                        if (tokens.Length > 0)
                        {
                            c1Appointments.SetData(i, COL_Appointment_LocationAssociatedFacility, Convert.ToString(tokens[0]));
                        }
                        
                        c1Appointments.SetData(i, COL_Appointment_DefaultDos, Convert.ToString(PatientAppointment.Rows[i - 1]["bIsDefaultDOS"]));
                        c1Appointments.SetData(i, COL_Appointment_DefaultFacility, Convert.ToString(PatientAppointment.Rows[i - 1]["bIsDefaultFacility"]));
                        c1Appointments.SetData(i, COL_Appointment_DefaultRenderingProvider, Convert.ToString(PatientAppointment.Rows[i - 1]["bIsDefaultRenderingProvider"]));

                        
                        
                        if (this.Appointments.Contains(nAppointmentID))
                        {
                            c1Appointments.SetData(i, COL_Appointment_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
                        }
                    }
                }
                
            }
          

            DesignGrid();
            c1Appointments.Select(1, COL_Appointment_Select, true);
        }

        private void tls_Close_Click(object sender, EventArgs e)
        {
            this.AppointmentsSet = false;
            this.Close();
            if (_IsOnLoadform == true)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.AppointmentDefault, gloAuditTrail.ActivityType.Close, "User closed appointment screen", nPatientID, AppointmentID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
            }
        }

        private void tls_SaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsOnLoadform == false)
                {

                    if (c1Appointments.Rows.Count > 1)
                    {
                        for (int i = c1Appointments.Rows.Fixed; i < c1Appointments.Rows.Count; i++)
                        {
                            if (c1Appointments.GetCellCheck(i, COL_Appointment_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                AppointmentIDs.Add(Convert.ToInt64(c1Appointments.GetData(i, COL_Appointment_AppointmentID)));
                            }

                        }
                    }

                    this.AppointmentsSet = true;
                    this.Close();
                }
                else
                {

                    if (c1Appointments.Rows.Count > 1)
                    {
                        for (int i = c1Appointments.Rows.Fixed; i < c1Appointments.Rows.Count; i++)
                        {
                            if (c1Appointments.GetCellCheck(i, COL_Appointment_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                AppointmentID = Convert.ToInt64(c1Appointments.GetData(i, COL_Appointment_AppointmentID));
                                DefaultDos = Convert.ToDateTime(c1Appointments.GetData(i, COL_Appointment_FromDate)).ToString("MM/dd/yyyy");
                                Location = Convert.ToString(c1Appointments.GetData(i, COL_Appointment_Location));
                                Provider = Convert.ToString(c1Appointments.GetData(i, COL_Appointment_Provider));
                                ProviderID = Convert.ToInt64(c1Appointments.GetData(i, COL_Appointment_ProviderID));
                                LocationID = Convert.ToInt64(c1Appointments.GetData(i, COL_Appointment_LocationID));
                                //FacilityID = Convert.ToInt64(c1Appointments.GetData(i, COL_Appointment_FacilityID));
                                AppointmentTypeID = Convert.ToInt64(c1Appointments.GetData(i, COL_Appointment_TypeID));
                                FacilityDesc = Convert.ToString(c1Appointments.GetData(i, COL_Appointment_LocationAssociatedFacility));

                                bIsDefaultDOS = Convert.ToBoolean(c1Appointments.GetData(i, COL_Appointment_DefaultDos));
                                bIsDefaultRenderingProvider = Convert.ToBoolean(c1Appointments.GetData(i, COL_Appointment_DefaultRenderingProvider));
                                bIsDefaultFacility = Convert.ToBoolean(c1Appointments.GetData(i, COL_Appointment_DefaultFacility));
                                bool DefaultFacility = Convert.ToBoolean(c1Appointments.GetData(i, COL_Appointment_DefaultFacility));
                               
                                string strFacility = Convert.ToString(c1Appointments.GetData(i, COL_Appointment_FacilityID));
                                string[] strFacilityIDs = strFacility.Split('|');

                                if (DefaultFacility == true)
                                {
                                    DialogResult dr = DialogResult.None;
                                    if (strFacilityIDs.Length > 1 && LastAppointmentID!=AppointmentID)
                                    {
                                        string strMsg = string.Format("Selected appointment has multiple facilty associcated with appointment location. Do you want to continue?\n\nYes: Selected appointment details inherited on charge.\nNo: Select another facility.");
                                        dr = MessageBox.Show(strMsg, gloGlobal.gloPMGlobal.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                                    }
                                    if (dr == DialogResult.No)
                                    {
                                        LastAppointmentID = AppointmentID;
                                        c1Appointments.Focus();
                                        c1Appointments.Select(i, COL_Appointment_LocationAssociatedFacility, true);
                                        AppointmentID = 0;
                                        return;
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.AppointmentDefault, gloAuditTrail.ActivityType.Select, "Selected appointment details are inherited", nPatientID, AppointmentID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);
                                    }
                                }

                                FacilityID = GetLocationAssociatedFacilityID(FacilityDesc.Trim());
                            }
                        }
                        if (AppointmentID == 0)
                        {
                            MessageBox.Show("Select Appointment", gloGlobal.gloPMGlobal.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return;
                        }
                    }
                    this.Close();
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

        private void c1Appointments_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is C1.Win.C1FlexGrid.C1FlexGrid)
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, sender as C1.Win.C1FlexGrid.C1FlexGrid, e.Location);
            }
        }

        private void c1Appointments_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            
            try
            {
                if (_IsOnLoadform == true)
                {
                    if (c1Appointments.Rows.Count > 0)
                    {
                        if (e.Col == COL_Appointment_Select)
                        {
                            for (int i = 1; i <= c1Appointments.Rows.Count - 1; i++)
                            {
                                if (c1Appointments.GetCellCheck(i, COL_Appointment_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked & c1Appointments.RowSel == i)
                                {
                                    c1Appointments.SetCellCheck(i, COL_Appointment_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                    //c1Appointments.Rows[i].AllowEditing = false;
                                }
                                else
                                {
                                    //c1Appointments.Rows[i].AllowEditing = true;
                                    c1Appointments.SetCellCheck(i, COL_Appointment_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public static Int64 GetLocationAssociatedFacilityID(string FacilityCode)
        {
            DataTable dtFacility = new DataTable();
            Int64 FacilityId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@FacilityDesc", FacilityCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
              
                oDB.Retrive("gsp_GetLocationAssociatedFacility", oDBParameters, out dtFacility);
                if (dtFacility.Rows.Count > 0 && dtFacility.Rows.Count != 0)
                {
                    FacilityId = Convert.ToInt64(dtFacility.Rows[0]["FacilityID"]);
                }
               
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                    
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (dtFacility != null)
                {
                    dtFacility.Dispose();
                    dtFacility = null;
                }
            }

            return FacilityId;
        }

        private void mnuBilling_Save_Click(object sender, EventArgs e)
        {
            tls_SaveAndClose.PerformClick();
        }
        //bool allowMove = false;
        private void c1Appointments_BeforeSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            //if (allowMove == true)
            //{
            //    allowMove = false;
            //    return;
            //}

            //int row = e.NewRange.r1;
            //int col = e.NewRange.c1;

            //while (!allowMove)
            //{
            //    if (c1Appointments.Cols[col].AllowEditing != true)
            //    {
            //        if (e.NewRange.r1 > e.OldRange.r1)
            //        {
            //            col += 1;
            //        }
            //        else if (e.NewRange.r1 < e.OldRange.r1)
            //        {
            //            col -= 1;
            //        }
            //        else
            //        {
            //            if (e.NewRange.c1 >= e.OldRange.c1)
            //            {
            //                col += 1;
            //            }
            //            else
            //            {
            //                col -= 1;
            //            }
            //        }

            //        if (col >= c1Appointments.Cols.Count)
            //        {
            //            if (row == c1Appointments.Rows.Count - 1)
            //            {
            //                row = c1Appointments.Rows.Fixed;
            //            }
            //            else
            //            {
            //                row += 1;
            //            }

            //            col = c1Appointments.Cols.Fixed;
            //        }
            //        else if (col <= c1Appointments.Cols.Fixed - 1)
            //        {
            //            row -= 1;
            //            col = c1Appointments.Cols.Count - 1;
            //        }
            //    }
            //    else
            //    {
            //        allowMove = true;
            //    }
            //}

            //e.Cancel = true;
            //c1Appointments.Select(row, col);
        }

        private void c1Appointments_EnterCell(object sender, EventArgs e)
        {
            string Facility = Convert.ToString(c1Appointments.GetData(c1Appointments.Row, COL_Appointment_LocationAssociatedFacility));
            if (Facility != "")
            {
                c1Appointments.Cols[COL_Appointment_LocationAssociatedFacility].AllowEditing = true;
                c1Appointments.Focus();
            }
            else
            {
                c1Appointments.Cols[COL_Appointment_LocationAssociatedFacility].AllowEditing = false;
            }

        }

        
    }
}
