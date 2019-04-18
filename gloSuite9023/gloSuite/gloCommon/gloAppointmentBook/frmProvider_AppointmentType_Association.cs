using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloGeneralItem;
using gloAuditTrail;

namespace gloAppointmentBook
{
    public partial class frmProvider_AppointmentType_Association : Form
    {

        #region "Declarations"
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        string _AppointmentTypes = " ";
        Int64 _ProviderID = 0;
        string _ApptmntCode = "";
        string _ApptmntDesc = "";
        Int64 _ApptmntFlag = 0;
        //variable to send  ID of newly  Added entry  to view form 
        private Int64 _ReturnID = 0;
        private bool _IsProviderPresent = true;
        public Int64 ReturnId
        {
            get { return _ReturnID; }
            set { _ReturnID = value; }
        }

        private const int Col_ProviderId = 0;
        private const int Col_ProviderName = 1;
        private const int Col_AppTypeDesc = 2;
        private const int Col_AppTypeCode = 3;
        private const int Col_AppTypeFlag = 4;

        #endregion

        #region " Constructor "

        public frmProvider_AppointmentType_Association(string databaseconnectionstring)
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

        public frmProvider_AppointmentType_Association(Int64 ProviderID, string databaseconnectionstring)
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
            _ProviderID = ProviderID;

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

        #region "Form Load Method"

        private void frmProvider_AppointmentType_Association_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1AppointmentType, false);

            DesignGrid();
            //FillProviders();
            Fill_AppointmentTypes();
            if (_IsProviderPresent == false)
            {
                this.Close();
            }

        }

        #endregion

        #region " Fill, Design,Save Method"
        private void DesignGrid()
        {
            c1AppointmentType.Visible = true;
            Books.AppointmentType oApptype = new Books.AppointmentType(_databaseconnectionstring);
            Books.Resource oProvider = new Books.Resource(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                new global::gloAppointmentBook.AppointmentTypeFlag();
                if (_ProviderID != 0)
                {
                    dt = oApptype.GetApptmnt_Provider(_ProviderID);
                }
                else
                {
                    dt = oProvider.GetProviders();
                }

                if (dt != null)
                {
                    c1AppointmentType.Clear();
                    c1AppointmentType.Rows.Count = 1;
                    c1AppointmentType.Cols.Count = 5;

                    c1AppointmentType.SetData(0, Col_ProviderId, "ProviderID");
                    c1AppointmentType.SetData(0, Col_ProviderName, "Provider Name");
                    c1AppointmentType.SetData(0, Col_AppTypeDesc, "Appointment Type");
                    c1AppointmentType.SetData(0, Col_AppTypeCode, "Appointment Type Code ");
                    c1AppointmentType.SetData(0, Col_AppTypeFlag, "Appointment Type Flag");

                    c1AppointmentType.Cols[Col_ProviderId].Visible = false;
                    c1AppointmentType.Cols[Col_ProviderName].Visible = true;
                    c1AppointmentType.Cols[Col_AppTypeDesc].Visible = true;
                    c1AppointmentType.Cols[Col_AppTypeCode].Visible = false;
                    c1AppointmentType.Cols[Col_AppTypeFlag].Visible = false;

                    int nWidth = c1AppointmentType.Width;
                    c1AppointmentType.Cols[Col_ProviderId].Width = 0;
                    c1AppointmentType.Cols[Col_ProviderName].Width = (int)(0.5 * (nWidth));
                    c1AppointmentType.Cols[Col_AppTypeDesc].Width = (int)(0.5 * (nWidth) - 1);
                    c1AppointmentType.Cols[Col_AppTypeCode].Width = 0;
                    c1AppointmentType.Cols[Col_AppTypeFlag].Width = 0;

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            Int32 RowIndex = c1AppointmentType.Rows.Count;
                            c1AppointmentType.Rows.Add();
                            c1AppointmentType.SetData(RowIndex, Col_ProviderId, dt.Rows[i]["nProviderID"]);
                            c1AppointmentType.SetData(RowIndex, Col_ProviderName, dt.Rows[i]["ProviderName"]);
                            if (_ProviderID != 0)
                            {
                                c1AppointmentType.SetData(RowIndex, Col_AppTypeDesc, dt.Rows[i]["sAppointmentTypeDesc"]);
                                c1AppointmentType.SetData(RowIndex, Col_AppTypeCode, dt.Rows[i]["sAppointmentTypeCode"]);
                                c1AppointmentType.SetData(RowIndex, Col_AppTypeFlag, (dt.Rows[i]["nAppointmentTypeFlag"]));
                            }
                            else
                            {
                                c1AppointmentType.SetData(RowIndex, Col_AppTypeFlag, AppointmentTypeFlag.Followup.GetHashCode());
                            }
                        }
                    }
                    c1AppointmentType.AllowEditing = true;
                    //c1AppointmentType.Cols[Col_AppTypeDesc].AllowEditing = true;
                    c1AppointmentType.Cols[Col_ProviderName].AllowEditing = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oApptype != null) { oApptype.Dispose(); oApptype = null; }
                if (oProvider != null) { oProvider.Dispose(); oProvider = null; }
                if (dt != null) { dt.Dispose(); dt = null; }
            }
        }

        private void Fill_AppointmentTypes()
        {
            Books.AppointmentType oAppointmentType = new Books.AppointmentType(_databaseconnectionstring);
            DataTable dtAppointmentType = oAppointmentType.GetList(AppointmentProcedureType.AppointmentType);
            Books.Resource oProvider = new Books.Resource(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                if (_ProviderID != 0)
                {
                    dt = oAppointmentType.GetApptmnt_Provider(_ProviderID);
                }
                else
                {
                    dt = oProvider.GetProviders();
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dtAppointmentType != null && dtAppointmentType.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtAppointmentType.Rows.Count; i++)
                        {
                            _AppointmentTypes += "|" + Convert.ToString(dtAppointmentType.Rows[i]["sAppointmentType"]);
                        }

                        c1AppointmentType.Cols[Col_AppTypeDesc].ComboList = _AppointmentTypes;
                    }
                }
                else
                {
                    _IsProviderPresent = false;
                    MessageBox.Show("Provider is not there in database.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAppointmentType != null) { oAppointmentType.Dispose(); oAppointmentType = null; }
                if (dtAppointmentType != null) { dtAppointmentType.Dispose(); dtAppointmentType = null; }
                if (oProvider != null) { oProvider.Dispose(); oProvider = null; }
                if (dt != null) { dt.Dispose(); dt = null; }
            }

        }

        private void FillProviders()
        {
            try
            {
                DataTable dt;
                // Fill Providers in the Combo Box
                Books.Resource oProvider = new Books.Resource(_databaseconnectionstring);
                dt = oProvider.GetProviders();


                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {

                        c1AppointmentType.SetData(i, 0, dt.Rows[i]["nProviderID"]);
                        c1AppointmentType.SetData(i, 1, dt.Rows[i]["ProviderName"]);

                    }
                }
                dt = null;
                oProvider.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool Savedata()
        {
            Books.AppointmentType oAptmnt = new global::gloAppointmentBook.Books.AppointmentType(_databaseconnectionstring);

            try
            {
                for (int i = 1; i < c1AppointmentType.Rows.Count; i++)
                {
                    _ProviderID = Convert.ToInt64(c1AppointmentType.GetData(i, Col_ProviderId));
                    _ApptmntCode = "";
                    _ApptmntDesc = Convert.ToString(c1AppointmentType.GetData(i, Col_AppTypeDesc));
                    _ApptmntFlag = Convert.ToInt64(c1AppointmentType.GetData(i, Col_AppTypeFlag));
                    if (_ApptmntDesc != "")
                    {
                        _ReturnID = _ProviderID;
                        if (oAptmnt.AddApptmnt_Provider(_ProviderID, _ApptmntCode, _ApptmntDesc, _ApptmntFlag) > 0)
                        {
                            //return true; 
                        }
                    }

                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Provider_AppointmentType_Association, ActivityType.Add, "Add Provider Appointment Type Association", 0, _ReturnID, _ProviderID, ActivityOutCome.Success);

                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAptmnt != null) { oAptmnt.Dispose(); oAptmnt = null; }
            }
            return false;
        }

        #endregion

        #region "ToolStrip Event "

        private void tlsp_ProApptypeAssociation_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {

                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            if (Savedata() == true)
                                this.Close();

                        }

                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        {
                            if (Savedata() == true)
                            {
                                _ProviderID = 0;
                                _AppointmentTypes = "";
                                //dhruv 20091229 
                                //When appointment type is selected it should stay as selected against that patient.
                                //DesignGrid();
                                Fill_AppointmentTypes();

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
        }

        #endregion

        private void c1AppointmentType_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }



    }
}