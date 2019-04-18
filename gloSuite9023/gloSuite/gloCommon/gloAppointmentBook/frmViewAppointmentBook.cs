using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAppointmentBook.Books;
using Janus.Windows.Schedule;
using Janus.Windows.Common;
using gloAuditTrail;

namespace gloAppointmentBook
{
    internal partial class frmViewAppointmentBook : Form
    {

        #region "Variable Declaration"
        //Code Start added by kanchan on 20120102 for gloCommunity integration
        public delegate void gloCommunityHandler();   //added delegate for calling gloCommunityViewDataform for AppConfig Download form.
        public event gloCommunityHandler EvntgloCommunityHandler; //added event for calling gloCommunityViewDataform for AppConfig Download form.

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        private DataView _dv;
        //public Int64 SelectedView;
        public string SelectedView;
        private Int64 _UserID = 0;
        public string _searchcolumn = "";
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        //Variables added to Extend tekmplates
        DateTime _ExtendTempStartTime;
        DateTime _ExtendTempEndTime;
        Int64 _ExtendTempProviderID;

        //Added By Pramod for Implementing Broad Search 2009-05-13
        //DataTable dtTemp;
        //DataView dvNext;
        private string _UserName = "";
        gloUserRights.ClsgloUserRights oClsgloUserRights = null;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #endregion

        #region "Constructor"

        private frmViewAppointmentBook()
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
            //Get User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            //

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }
            //

            InitializeComponent();
        }

        private static frmViewAppointmentBook _frm = null;

        public static frmViewAppointmentBook GetInstance()
        {

            if (_frm != null)
            {
                return _frm;
            }
            else
            {
                _frm = new frmViewAppointmentBook();
                return _frm;
            }

        }

        bool blnDisposed;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (blnDisposed == false)
            {
                if (disposing && (components != null))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (oClsgloUserRights != null)
                        {
                            oClsgloUserRights.Dispose();
                            oClsgloUserRights = null;
                        }
                    }
                    catch
                    {
                    }
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            _frm = null;
            blnDisposed = true;
        }

        private void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        #endregion

        #region "Form Load Event"

        private void frmViewAppointmentBook_Load(object sender, EventArgs e)
        {

           // gloC1FlexStyle.Style(c1AppointmentType, false);
            
            try
            {
                 
                Fill_AppointmentBook();
                pnltrvSerchAppBook.Visible = false;
                //lblSearch.Text = "Resource ";
                lblSearch.Text = "Search :";

                if (oClsgloUserRights.Resource == true)
                {
                    Fill_Resource(0);
                }
                //Fill_ResourceTypes();

                // Added by Anil on 20090720 For User Rights implementation
                //AssignUserRights();

                #region "Code Start added by kanchan on 20120102 for gloCommunity integration"
                ts_gloCommunityDownload.Visible = false;
                if (_MessageBoxCaption.ToLower() != "gloPM".ToLower())
                {
                    //gloUserRights.ClsgloUserRights t = new gloUserRights.ClsgloUserRights(

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                    oDB.Connect(false);
                    string strQueryRight = "SELECT  Rights_MST.sRightsName AS RightsName FROM UserRights_DTL INNER JOIN Rights_MST ON UserRights_DTL.nRightsID = Rights_MST.nRightsID INNER JOIN User_MST ON UserRights_DTL.nUserID = User_MST.nUserID WHERE User_MST.sLoginName = '" + _UserName + "' AND Rights_MST.sRightsName = 'Share' AND ISNULL(ApplicationType, 0) = 0";
                    
                    if (Convert.ToString(oDB.ExecuteScalar_Query(strQueryRight)) != string.Empty)//Added condition to fixed Bug # : 37984 on 20120928
                    {
                        string strQuery = "Select sSettingsValue as SettingsValue from Settings where sSettingsName='gloCommunity Feature' ";
                        Boolean Result = false;
                        Boolean.TryParse(Convert.ToString(oDB.ExecuteScalar_Query(strQuery)), out Result);
                        strQuery = null;

                        ts_gloCommunityDownload.Visible = Result;
                    }
                    strQueryRight = null;
                    oDB.Disconnect();
                }

                #endregion "Code end added by kanchan on 20120102 for gloCommunity integration"
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }

        #endregion

        #region "Tool Strip Buttons "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (trvAppointmentBook.Nodes.Count > 0 && e.ClickedItem.Tag.ToString() != "Close")
                {
                    frmSetupResourceType oResourceType_MST;
                    Int64 ID = 0;
                    if (e.ClickedItem.Tag.ToString() == "Delete" || e.ClickedItem.Tag.ToString() == "Modify")
                    {
                        // Check only for Modify & Delete 
                        //Appointment Type
                        if (Convert.ToString(trvAppointmentBook.SelectedNode.Tag) == "Appointment Type")
                        {
                            if (c1AppointmentType.RowSel == -1)
                            {
                                return;
                            }
                            else
                            {
                                ID = Convert.ToInt64(c1AppointmentType.GetData(c1AppointmentType.RowSel, 0));
                            }
                        }
                        else
                        {
                            if (SelectedView != "Template" && SelectedView != "Template Allocation")
                            {
                                if (dgResource.SelectedRows.Count <= 0)
                                {
                                    // If Data Is Not Selected form GridView to Modify or Delete
                                    return;
                                }
                                if (dgResource.SelectedRows[0].Cells[0].Value.ToString() == "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() == "0")
                                    // IF ID is Not Present
                                    return;
                                else
                                    ID = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value.ToString());
                            }
                        }
                    }

                    switch (SelectedView)
                    {
                        case "Resource Type": // Resource Type 
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    txtSearch.Visible = true;
                                    oResourceType_MST = new frmSetupResourceType();
                                    oResourceType_MST.DatabaseConnectionString = _databaseconnectionstring;
                                    oResourceType_MST.ShowDialog(this);
                                    Fill_ResourceTypes(0);
                                    oResourceType_MST.Dispose();
                                    break;
                                case "Modify":
                                    {
                                        Int64 ResourceTypeID = 0;
                                        if (dgResource.SelectedRows.Count > 0)
                                        {
                                            if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                                            {
                                                if (dgResource.SelectedRows[0].Cells[2].Value.ToString() != "")
                                                {
                                                    if (Convert.ToInt16(dgResource.SelectedRows[0].Cells[2].Value.ToString()) > 2)
                                                    {
                                                        ResourceTypeID = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value.ToString());
                                                        oResourceType_MST = new frmSetupResourceType(ResourceTypeID);
                                                        oResourceType_MST.DatabaseConnectionString = _databaseconnectionstring;
                                                        oResourceType_MST.ShowDialog(this);
                                                        oResourceType_MST.Dispose();
                                                        oResourceType_MST = null;
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Cannot modify system resource type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Cannot modify this resource type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                        }
                                        Fill_ResourceTypes(ResourceTypeID);
                                    }
                                    break;
                                case "Delete":
                                    {
                                        Int64 ResourceTypeID = 0;
                                        if (dgResource.SelectedRows[0].Cells[2].Value.ToString() != "")
                                        {
                                            if (Convert.ToInt16(dgResource.SelectedRows[0].Cells[2].Value.ToString()) > 2)
                                            {
                                                //Remark - Delete Validation after transaction table create
                                                //Delete/Block Code Here
                                                if (MessageBox.Show("Are you sure you want to delete this resource type?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                                {
                                                    gloResourceType ogloResourceType = new gloResourceType();
                                                    ResourceTypeID = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value.ToString());
                                                    if (ogloResourceType.CanDelete(ResourceTypeID))
                                                    {
                                                        //bool Result = ogloResourceType.Block(ResourceTypeID);
                                                        bool Result = ogloResourceType.Delete(ResourceTypeID);
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.ResourceType, ActivityType.Delete, "Delete Resource Type", 0, ResourceTypeID, 0, ActivityOutCome.Success);

                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Cannot delete resource type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                    if (ogloResourceType != null) { ogloResourceType.Dispose(); ogloResourceType = null; }
                                                }
                                                Fill_ResourceTypes(ResourceTypeID);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Cannot delete system resource type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                    break;
                                case "Refresh":
                                    Fill_ResourceTypes(0);
                                    break;
                                case "Close":
                                    this.Close();
                                    break;
                                case "Help":
                                default:
                                    break;
                            }
                            break;


                        case "Appointment Block Type": // Appointment Block Type
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSetupAppointmentBlockType ofrm = new frmSetupAppointmentBlockType(_databaseconnectionstring);
                                        ofrm.ShowDialog(this);
                                        Fill_AppointmentBlockTypes(ofrm.ReturnappointmentBlockTypeID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        frmSetupAppointmentBlockType ofrm = new frmSetupAppointmentBlockType(ID, _databaseconnectionstring);
                                       //Code Added by Mayuri:20091103
                                        //To make invisible button save if form gets open for modify
                                        ofrm.tsb_Save.Visible = false;
                                        //End Code Added by Mayuri:20091103
                                        ofrm.ShowDialog(this);
                                        ofrm.Dispose();
                                        ofrm = null;
                                        Fill_AppointmentBlockTypes(ID);
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        if (MessageBox.Show("Are you sure to delete this appointment block type?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            Books.AppointmentBlockType oAppointmentBlockType = new Books.AppointmentBlockType(_databaseconnectionstring);
                                            oAppointmentBlockType.AppointmentBlockTypeID = ID;
                                            oAppointmentBlockType.Delete(ID);
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentBlockType, ActivityType.Delete, "Delete Appointment Block Type", 0, ID, 0, ActivityOutCome.Success);

                                            Fill_AppointmentBlockTypes(0);

                                            if (oAppointmentBlockType != null) { oAppointmentBlockType.Dispose(); oAppointmentBlockType = null; }
                                            
                                            //if (oAppointmentBlockType.CanDelete(ID))
                                            //{
                                            //    if (oAppointmentBlockType.Block() == false)
                                            //    {
                                            //        MessageBox.Show("Appointment Block Type not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //        break;
                                            //    }
                                            //    Fill_AppointmentBlockTypes();
                                            //}
                                            //else
                                            //{
                                            //    MessageBox.Show(" Can not delete Block Type ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //}
                                        }
                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_AppointmentBlockTypes(0);
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Appointment Block Type
                            break;





                        //case "ClinicalInstruction": // Appointment Block Type
                        //    dgResource.Visible = true;
                        //    c1AppointmentType.Visible = false;
                        //    switch (e.ClickedItem.Tag.ToString())
                        //    {
                        //        case "Add":
                        //            {
                        //                frmMstClinicalInstruction ofrm = new frmMstClinicalInstruction(0);
                        //                ofrm.ShowDialog();
                        //                Fill_ClinicalInstruction(0);
                        //            }
                        //            break;
                        //        case "Modify":
                        //            {
                        //                Int64 nID = 0;
                        //                if (dgResource.SelectedRows.Count > 0)
                        //                {
                        //                    if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                        //                    {
                                                                                        
                        //                       nID = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value.ToString());

                        //                       frmMstClinicalInstruction ofrm = new frmMstClinicalInstruction(nID);
                        //                       ofrm.tsb_Save.Visible = false;
                        //                       ofrm.ShowDialog();
                                                
                        //                    }
                        //                }                                     

                        //                Fill_ClinicalInstruction(0);
                        //            } 
                        //            break;
                        //        case "Delete":
                        //            {
                        //                if (MessageBox.Show("Are you sure to delete this clinical instruction?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        //                {
                        //                    Int64 nID = 0;
                        //                    if (dgResource.SelectedRows.Count > 0)
                        //                    {
                        //                        if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                        //                        {

                        //                            nID = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value.ToString());
                        //                            ClsClinicalInstruction objClinicalInstruction = new ClsClinicalInstruction();
                        //                            objClinicalInstruction.DeleteClinicalInstruction(nID);
                                                    

                        //                        }
                        //                    }
                        //                    Fill_ClinicalInstruction(0);
                        //                }
                        //            } 
                        //            break;
                        //        case "Refresh":
                        //            {
                        //                Fill_ClinicalInstruction(0);
                        //            } 
                        //            break;
                        //        case "Close":
                        //            {
                        //                this.Close();
                        //            }
                        //            break;
                        //    } 
                        //    break;















                        case "Appointment Type": // Appointment Type
                            dgResource.Visible = false;
                            c1AppointmentType.Visible = true;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSetupAppointmentType ofrm = new frmSetupAppointmentType(_databaseconnectionstring, AppointmentProcedureType.AppointmentType);
                                        ofrm.ShowDialog(this);
                                        Fill_AppointmentTypes(ofrm.AppTypeId, "");
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        frmSetupAppointmentType ofrm = new frmSetupAppointmentType(ID, _databaseconnectionstring, AppointmentProcedureType.AppointmentType);
                                        //Code Added by Mayuri:20091103
                                        //To make invisible  save button if form gets open for modify
                                        ofrm.tsb_Save.Visible = false;
                                        //End Code Added by Mayuri:20091103
                                        ofrm.ShowDialog(this);
                                        ofrm.Dispose();
                                        ofrm = null;
                                        Fill_AppointmentTypes(ID, "");
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        Object nAppointmentTypeID;
                                        nAppointmentTypeID = AB_ValidateAppointmentType_Delete(ID);

                                        if (Convert.ToInt64 (nAppointmentTypeID) > 0)
                                        {
                                            MessageBox.Show("You cannot delete this appointment type because it is used in transaction entry.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            if (MessageBox.Show("Are you sure to delete this appointment type?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                Books.AppointmentType oAppointmentType = new Books.AppointmentType(_databaseconnectionstring);
                                                oAppointmentType.AppointmentTypeID = ID;
                                                oAppointmentType.DeleteAppointmentType(ID);
                                                oAppointmentType.Dispose();
                                                oAppointmentType = null;
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentType, ActivityType.Delete, "Delete Appointment Type", 0, ID, 0, ActivityOutCome.Success);

                                                Fill_AppointmentTypes(0, "");

                                                //if (oAppointmentType.CanDeleteAppointmentType(ID))
                                                //{
                                                //    if (oAppointmentType.Block() == false)
                                                //    {
                                                //        MessageBox.Show("Appointment Type not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //        break;
                                                //    }
                                                //    Fill_AppointmentTypes();
                                                //}
                                                //else
                                                //{
                                                //    MessageBox.Show(" Can not delete Appointment Type ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //}
                                            }
                                        }
                                        nAppointmentTypeID = null;
                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_AppointmentTypes(0, "");
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Appointment Type
                            break;

                        case "Appointment Status": // Appointment Status 
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSetupAppointmentStatus ofrm = new frmSetupAppointmentStatus(_databaseconnectionstring);
                                        ofrm.ShowDialog(this);
                                        Fill_AppointmentStatus(ofrm.ReturnAppStatusID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        Books.AppointmentStatus oAppointmentStatus = new Books.AppointmentStatus(_databaseconnectionstring);
                                        oAppointmentStatus.AppointmentStatusID = ID;
                                        if (oAppointmentStatus.CanDelete(ID))
                                        {
                                            frmSetupAppointmentStatus ofrm = new frmSetupAppointmentStatus(ID, _databaseconnectionstring);
                                            //Code Added by Mayuri:20091103
                                            //To make invisible button save if form gets open for modify
                                            ofrm.tsb_Save.Visible = false;
                                            //End Code Added by Mayuri:20091103
                                            ofrm.ShowDialog(this);
                                            ofrm.Dispose();
                                            ofrm = null;
                                            Fill_AppointmentStatus(ID);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Cannot modify system appointment status.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        if (oAppointmentStatus != null) { oAppointmentStatus.Dispose(); oAppointmentStatus = null; }
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {

                                        Books.AppointmentStatus oAppointmentStatus = new Books.AppointmentStatus(_databaseconnectionstring);
                                        oAppointmentStatus.AppointmentStatusID = ID;

                                        if (oAppointmentStatus.CanDelete(ID))
                                        {
                                            if (MessageBox.Show("Are you sure to delete this appointment status?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                oAppointmentStatus.Delete(ID);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Delete, "Delete Appointment Status", 0, ID, 0, ActivityOutCome.Success);
                                                Fill_AppointmentStatus(0);
                                            }
                                        }
                                        else
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.Delete, "Delete Appointment Status", 0, ID, 0, ActivityOutCome.Failure);

                                            MessageBox.Show("Cannot delete system appointment status.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        if (oAppointmentStatus != null) { oAppointmentStatus.Dispose(); oAppointmentStatus = null; }

                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_AppointmentStatus(0);
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Appointment Status
                            break;

                        case "Department": // Department
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmDepartment ofrm = new frmDepartment(_databaseconnectionstring);
                                        ofrm.ShowDialog(this);
                                        Fill_Department(ofrm.ReturnDeptID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        frmDepartment ofrm = new frmDepartment(ID, _databaseconnectionstring);
                                        //Code Added by Mayuri:20091103
                                        //To make invisible button save if form gets open for modify
                                        ofrm.tsb_Save.Visible = false;
                                        //End Code Added by Mayuri:20091103
                                        ofrm.ShowDialog(this);
                                        Fill_Department(ID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        if (MessageBox.Show("Are you sure you want to delete this department?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            Books.Department oDepartment = new Books.Department(_databaseconnectionstring);
                                            oDepartment.DepartmentID = ID;
                                            oDepartment.Delete(ID);
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Department, ActivityType.Delete, "Delete Department", 0, ID, 0, ActivityOutCome.Success);

                                            //if (oDepartment.Block() == false)
                                            //{
                                            //    MessageBox.Show("Department not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //    break;
                                            //}
                                            Fill_Department(0);
                                            if (oDepartment != null) { oDepartment.Dispose(); oDepartment = null; }
                                        }
                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_Department(0);
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Department
                            break;
                        case "Location": // Location 
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSetupLocation ofrm = new frmSetupLocation(_databaseconnectionstring);
                                        ofrm.ShowDialog(this);
                                        Fill_Location(ofrm.ReturnLocationID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        frmSetupLocation ofrm = new frmSetupLocation(ID, _databaseconnectionstring);
                                        //Code Added by Mayuri:20091103
                                        //To make invisible button save if form gets open for modify
                                        ofrm.tlsp_btnSave.Visible = false;
                                        //End Code Added by Mayuri:20091103
                                        ofrm.ShowDialog(this);
                                        Fill_Location(ID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        Books.Location oLocation = new Books.Location();
                                        oLocation.LocationID = ID;
                                        if (oLocation.GetLocationTransactionCount(ID) == "0")
                                        {
                                            if (MessageBox.Show("Are you sure you want to delete this location?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                oLocation.Delete(ID);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Location, ActivityType.Delete, "Delete Location", 0, ID, 0, ActivityOutCome.Success);

                                                //if (oLocation.Block() == false)
                                                //{
                                                //    MessageBox.Show("Location not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    break;
                                                //}

                                                Fill_Location(0);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("You cannot delete this location because it is used in transaction entry.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        if (oLocation != null) { oLocation.Dispose(); oLocation = null; }
                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_Location(0);
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Location
                            break;
                        case "Patient Relationship": // Patient Relationship

                            break;

                        case "Resource": // Resource 
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSetupResource ofrm = new frmSetupResource(0, _databaseconnectionstring);
                                        ofrm.ShowDialog(this);
                                        Fill_Resource(ofrm.nResourceID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                        //Fill_Resource(0);
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        frmSetupResource ofrm = new frmSetupResource(ID, _databaseconnectionstring);
                                        //Code Added by Mayuri:20091103
                                        //To make Save button invisible if form gets open for modify
                                        ofrm.tlsp_btnSave.Visible = false;                                       
                                        //End Code Added by Mayuri:20091103
                                        ofrm.ShowDialog(this);
                                        Fill_Resource(ID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        if (MessageBox.Show("Are you sure you want to delete this resource?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            Books.Resource oResource = new Books.Resource(_databaseconnectionstring);
                                            oResource.ResourceID = ID;
                                            oResource.Delete(ID);
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Resource, ActivityType.Delete, "Delete Resource", 0, ID, 0, ActivityOutCome.Success);

                                            Fill_Resource(0);

                                            if (oResource != null) { oResource.Dispose(); oResource = null; }
                                            //if (oResource.CanDelete(ID))
                                            //{
                                            //    if (oResource.Block(ID) == false)
                                            //    {
                                            //        MessageBox.Show("Unable to delete Resource", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //        break;
                                            //    }
                                            //    Fill_Resource();
                                            //}
                                            //else
                                            //{
                                            //    MessageBox.Show(" Can not delete Resource ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //}
                                        }
                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_Resource(0);
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Location
                            break;

                        case "Problem Type": // Procedures 
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSetupAppointmentType ofrm = new frmSetupAppointmentType(_databaseconnectionstring, AppointmentProcedureType.Procedure);
                                        ofrm.ShowDialog(this);
                                        Fill_Procedures(ofrm.AppTypeId);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        frmSetupAppointmentType ofrm = new frmSetupAppointmentType(ID, _databaseconnectionstring, AppointmentProcedureType.Procedure);
                                        //Code Added by Mayuri:20091103
                                        //To make invisible button save if form gets open for modify
                                        ofrm.tsb_Save.Visible = false;
                                        //End Code Added by Mayuri:20091103
                                        ofrm.ShowDialog(this);
                                        Fill_Procedures(ID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        //Message box changed by Mayuri:20091130
                                        //To fix issue:-5228
                                        //if (MessageBox.Show("Are you sure you want to delete this procedure?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        if (MessageBox.Show("Are you sure you want to delete this problem type?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            Books.AppointmentType oAppointmentType = new Books.AppointmentType(_databaseconnectionstring);
                                            oAppointmentType.AppointmentTypeID = ID;
                                            oAppointmentType.DeleteProcedureType(ID);
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.ProblemType, ActivityType.Delete, "Delete Problem Type", 0, ID, 0, ActivityOutCome.Success);

                                            Fill_Procedures(0);

                                            if (oAppointmentType != null) { oAppointmentType.Dispose(); oAppointmentType = null; }
                                            //if (oAppointmentType.CanDeleteProcedureType(ID))
                                            //{
                                            //    if (oAppointmentType.Block() == false)
                                            //    {
                                            //        MessageBox.Show("Procedure not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //        break;
                                            //    }
                                            //    Fill_Procedures();
                                            //}
                                            //else
                                            //{
                                            //    MessageBox.Show(" Can not delete Procedure type", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //}
                                        }
                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_Procedures(0);
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Appointment Type
                            break;
                        case "Template": // Template
                            dgResource.Visible = false;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    frmSetupTemplate ofrmTemplate = new frmSetupTemplate(_databaseconnectionstring);
                                    ofrmTemplate.ActionToPerform = frmSetupTemplate.FormAction.New;
                                    ofrmTemplate.ShowDialog(this);
                                    for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                    {
                                        if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template")
                                        {
                                            trvAppointmentBook.Nodes[i].Nodes.Clear();
                                        }
                                    }
                                    for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                    {
                                        if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template")
                                        {
                                            trvAppointmentBook.Nodes[i].Nodes.Clear();
                                        }
                                    }
                                    Fill_Template(0);
                                    ofrmTemplate.Dispose();
                                    ofrmTemplate = null;
                                    break;
                                case "Modify":
                                    Int64 TemplateId = 0;
                                    if (trvAppointmentBook.SelectedNode != null)
                                    {
                                        if (trvAppointmentBook.SelectedNode.Level == 1)
                                        {
                                            TemplateId = Convert.ToInt64(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2));
                                            frmSetupTemplate ofrmTemplateMod = new frmSetupTemplate(TemplateId, _databaseconnectionstring);
                                            ofrmTemplateMod.ShowDialog(this);
                                            //Fill_Templates(TemplateId);
                                            Fill_Template(TemplateId);
                                            ofrmTemplateMod.Dispose();
                                            ofrmTemplateMod = null;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please select a template.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }

                                    }
                                    for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                    {
                                        if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template")
                                        {
                                            trvAppointmentBook.Nodes[i].Nodes.Clear();
                                        }
                                    }
                                    Fill_Template(0);
                                    break;
                                case "Delete":
                                    Int64 TemplateIdDel = 0;
                                    if (trvAppointmentBook.SelectedNode != null)
                                    {
                                        if (trvAppointmentBook.SelectedNode.Level == 1)
                                        {
                                            TemplateIdDel = Convert.ToInt64(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2));
                                            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate(_databaseconnectionstring);

                                            DialogResult res = MessageBox.Show("Are you sure you want to delete this template?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                            if (res == DialogResult.Yes)
                                            {
                                                ogloAppointmentTemplate.DeleteTemplate(TemplateIdDel);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Template, ActivityType.Delete, "Delete Template", 0, TemplateIdDel, 0, ActivityOutCome.Success);

                                            }

                                            for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                            {
                                                if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template")
                                                {
                                                    trvAppointmentBook.Nodes[i].Nodes.Clear();
                                                }
                                            }
                                            Fill_Template(0);
                                            if (ogloAppointmentTemplate != null) { ogloAppointmentTemplate.Dispose(); ogloAppointmentTemplate = null; }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please select a template.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }
                                    }
                                    for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                    {
                                        if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template")
                                        {
                                            trvAppointmentBook.Nodes[i].Nodes.Clear();
                                        }
                                    }
                                    Fill_Template(0);

                                    //Fill_Template1(0);
                                    break;
                                case "Refresh":
                                    for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                    {
                                        if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template")
                                        {
                                            trvAppointmentBook.Nodes[i].Nodes.Clear();
                                        }
                                    }
                                    Fill_Template(0);
                                    // Fill_trvTemplates();
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;

                            } // END Switch of Template

                            break;
                        case "Template Allocation": // Template Allocation

                            dgResource.Visible = false;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    Int64 ProviderIDAdd = 0;
                                    if (trvAppointmentBook.SelectedNode != null)
                                    {
                                        if (trvAppointmentBook.SelectedNode.Level == 0)
                                        {
                                            MessageBox.Show("Please select a provider.  ", _MessageBoxCaption, MessageBoxButtons.OK);

                                        }
                                        if (trvAppointmentBook.SelectedNode.Level == 1)
                                        {
                                            ProviderIDAdd = Convert.ToInt64(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2));

                                            frmSetupTemplateAllocation ofrmTemplateAllocation = new frmSetupTemplateAllocation(ProviderIDAdd, _databaseconnectionstring);
                                            ofrmTemplateAllocation.SelectedStartDate = this.SelectedStartDate;
                                            ofrmTemplateAllocation.SelectedEndDate = this.SelectedEndDate;

                                            ofrmTemplateAllocation.ShowDialog(this);
                                            for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                            {
                                                if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template Allocation")
                                                {
                                                    trvAppointmentBook.Nodes[i].Nodes.Clear();
                                                }
                                            }
                                            Fill_TemplateAllocation(0);
                                            ofrmTemplateAllocation.Dispose();
                                            ofrmTemplateAllocation = null;

                                        }


                                    }
                                    break;
                                case "Modify":
                                    Int64 ProviderIDMod = 0;
                                    if (trvAppointmentBook.SelectedNode != null)
                                    {
                                        if (trvAppointmentBook.SelectedNode.Level == 1)
                                        {
                                            ProviderIDMod = Convert.ToInt64(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2));
                                            if (CalendarTemplate.SelectedAppointments.Count > 0)
                                            {

                                                frmSetupTemplateAllocation frmAllocation = new frmSetupTemplateAllocation(ProviderIDMod, _databaseconnectionstring);
                                                frmAllocation.ShowDialog(this);
                                                frmAllocation.Dispose();
                                                frmAllocation = null;
                                                //for (int i = 0; i <= trvAppointmentBook.Nodes.Count; i++)
                                                //{
                                                //    if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == " Template Allocation")
                                                //    {
                                                //        trvAppointmentBook.Nodes[i].Nodes.Clear();
                                                //    }
                                                //}
                                                // Fill_TemplateAllocation1(ProviderIDMod);
                                            }
                                        }

                                    }

                                    break;
                                case "Delete":

                                    Int64 ProviderIDDel = 0;
                                    if (trvAppointmentBook.SelectedNode != null)
                                    {
                                        if (trvAppointmentBook.SelectedNode.Level == 0)
                                        {
                                            MessageBox.Show("Please select a provider.  ", _MessageBoxCaption, MessageBoxButtons.OK);

                                        }
                                        if (trvAppointmentBook.SelectedNode.Level == 1)
                                        {
                                            ProviderIDDel = Convert.ToInt64(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2));
                                            DeleteTemplateAllocation frmDelete = new DeleteTemplateAllocation(ProviderIDDel, _databaseconnectionstring);
                                            //gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseconnectionstring);
                                            frmDelete.ShowDialog(this);
                                            frmDelete.Close();
                                            for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                            {
                                                if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template Allocation")
                                                {
                                                    trvAppointmentBook.Nodes[i].Nodes.Clear();
                                                }
                                            }
                                            Fill_TemplateAllocation(ProviderIDDel);
                                            frmDelete.Dispose();
                                            frmDelete = null;

                                        }
                                    }

                                    break;
                                case "Extend":
                                    {
                                        if (trvAppointmentBook.SelectedNode.Level != 0 && CalendarTemplate.Appointments.Count > 0)
                                        {
                                            if (CalendarTemplate.View == Janus.Windows.Schedule.ScheduleView.DayView)
                                            {
                                                frmExtendToTemplate ofrmExtendToTemplate = new frmExtendToTemplate(_ExtendTempProviderID, _ExtendTempStartTime, _ExtendTempEndTime, _databaseconnectionstring);
                                                ofrmExtendToTemplate.ShowDialog(this);
                                                ofrmExtendToTemplate.Dispose();
                                                ofrmExtendToTemplate = null;
                                            }
                                            else
                                            {
                                                MessageBox.Show("To extend the template,please select Day view for the calendar.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        if (CalendarTemplate.Appointments.Count.Equals(0))
                                        {
                                            MessageBox.Show("No template associated for the selected date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    break;
                                case "Refresh":
                                    for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                                    {
                                        if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template Allocation")
                                        {
                                            trvAppointmentBook.Nodes[i].Nodes.Clear();
                                        }
                                    }
                                    Fill_TemplateAllocation(0);
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                                case "DayView":
                                    this.CalendarTemplate.Date = DateTime.Today.Date;
                                    this.CalendarTemplate.View = Janus.Windows.Schedule.ScheduleView.DayView;
                                    break;
                                case "WeekView":
                                    this.CalendarTemplate.Date = DateTime.Today.Date;
                                    this.CalendarTemplate.View = Janus.Windows.Schedule.ScheduleView.WeekView;
                                    break;
                                case "MonthView":
                                    this.CalendarTemplate.Date = DateTime.Today.Date;
                                    this.CalendarTemplate.View = Janus.Windows.Schedule.ScheduleView.MonthView;
                                    break;
                            } // END Switch of Template
                            break;

                        case "Provider Appointment Type Association": //

                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    frmProvider_AppointmentType_Association frmsetAssociation = new frmProvider_AppointmentType_Association(_databaseconnectionstring);
                                    frmsetAssociation.ShowDialog(this);
                                    Fill_AptmntType_Provider(frmsetAssociation.ReturnId);
                                    frmsetAssociation.Dispose();
                                    frmsetAssociation = null;
                                    break;
                                case "Modify":
                                    Int64 ProviderId = 0;
                                    if (dgResource.SelectedRows.Count > 0)
                                    {
                                        if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                                        {
                                            ProviderId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                                            frmsetAssociation = new frmProvider_AppointmentType_Association(ProviderId, _databaseconnectionstring);
                                            //Code Added by Mayuri:20091103
                                            //To make invisible button save if form gets open for modify
                                            frmsetAssociation.tsb_Save.Visible = false;
                                            //End Code Added by Mayuri:20091103
                                             frmsetAssociation.ShowDialog(this);
                                            Fill_AptmntType_Provider(ProviderId);
                                            frmsetAssociation.Dispose();
                                            frmsetAssociation = null;
                                        }
                                        else
                                        {
                                            MessageBox.Show(" ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }

                                    break;
                                case "Delete":
                                    if (dgResource.SelectedRows.Count > 0)
                                    {
                                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this association? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                        {
                                            if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                                            {
                                                ProviderId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                                                Books.AppointmentType oApptype = new Books.AppointmentType(_databaseconnectionstring);
                                                oApptype.DeleteApptmnt_Provider(ProviderId);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Provider_AppointmentType_Association, ActivityType.Delete, "Delete Provider Appointment Type Association", 0, ProviderId, ProviderId, ActivityOutCome.Success);

                                                Fill_AptmntType_Provider(0);
                                                if (oApptype != null) { oApptype.Dispose(); oApptype = null; }
                                            }
                                            else
                                            {
                                                MessageBox.Show(" ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                    Fill_AptmntType_Provider(0);

                                    break;
                                case "Refresh":

                                    Fill_AptmntType_Provider(0);

                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;

                            }
                            break;
                        //****************************** Anil 20081120
                        case "Follow Up": //

                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    frmSetupFollowup ofrmSetupFollowup = new frmSetupFollowup(_databaseconnectionstring);
                                    ofrmSetupFollowup.ShowDialog(this);
                                    Fill_FollowUps(0);
                                    ofrmSetupFollowup.Dispose();
                                    ofrmSetupFollowup = null;
                                    break;
                                case "Modify":
                                    Int64 FollowUpId = 0;
                                    if (dgResource.SelectedRows.Count > 0)
                                    {
                                        if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                                        {
                                            FollowUpId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                                            ofrmSetupFollowup = new frmSetupFollowup(_databaseconnectionstring, FollowUpId);
                                            //Code Added by Mayuri:20091103
                                            //To make invisible button save if form gets open for modify
                                            ofrmSetupFollowup.tsb_Save.Visible = false;
                                            //End Code Added by Mayuri:20091103
                                            ofrmSetupFollowup.ShowDialog(this);
                                            Fill_FollowUps(0);
                                            ofrmSetupFollowup.Dispose();
                                            ofrmSetupFollowup = null;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Cannot modify this follow up.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }

                                    break;
                                case "Delete":
                                    if (dgResource.SelectedRows.Count > 0)
                                    {
                                        if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                                        {
                                            FollowUpId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                                            gloAppointmentBook ogloAppointmentBook = new gloAppointmentBook(_databaseconnectionstring);
                                            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this follow up? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                            {
                                                ogloAppointmentBook.DeleteFollowUp(FollowUpId);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.FollowUp, ActivityType.Delete, "Delete FollowUp", 0, FollowUpId, 0, ActivityOutCome.Success);

                                                //Fill_FollowUps(0);
                                            }
                                            if (ogloAppointmentBook != null) { ogloAppointmentBook.Dispose(); ogloAppointmentBook = null; }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Cannot delete this follow up.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    Fill_FollowUps(0);

                                    break;
                                case "Refresh":

                                    Fill_FollowUps(0);

                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;

                            }
                            break;


                        case "Occupation": // Resource 
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSetupOccupation ofrm = new frmSetupOccupation(0, _databaseconnectionstring);
                                        ofrm.showemployertextbox = true;
                                        ofrm.ShowDialog(this);
                                        Fill_Occupation(ofrm.ReturnOccupationID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    {
                                        frmSetupOccupation ofrm = new frmSetupOccupation(ID, _databaseconnectionstring);
                                        ofrm.showemployertextbox = true;
                                        ofrm.ShowDialog(this);
                                        Fill_Occupation(ID);
                                        ofrm.Dispose();
                                        ofrm = null;
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        if (MessageBox.Show("Are you sure you want to delete this occupation?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            Books.Occupation oOccupation = new Books.Occupation();
                                            oOccupation.OccupationID = ID;
                                            oOccupation.Delete(ID);
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.Delete, "Delete Occupation", 0, ID, 0, ActivityOutCome.Success);
                                            Fill_Occupation(0);
                                            oOccupation.Dispose();
                                            oOccupation = null;
                                        }
                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_Occupation(0);
                                    } // END Refresh
                                    break;
                                case "Close":
                                    {
                                        this.Close();
                                    }
                                    break;
                            } // END Switch of Location
                            break;







                        //******************************

                        #region  ' Commented code of TOS & POS '

                        //case 12: //Type Of Service
                        //    {
                        //        dgResource.Visible = true;
                        //        c1AppointmentType.Visible = false;
                        //        switch (e.ClickedItem.Tag.ToString())
                        //        {
                        //            case "Add":
                        //                {
                        //                    frmSetupTOS ofrmSetupTOS = new frmSetupTOS(_databaseconnectionstring, 0);
                        //                    ofrmSetupTOS.ShowDialog(this);
                        //                    Fill_TOS();

                        //                }
                        //                break;
                        //            case "Modify":
                        //                {
                        //                    frmSetupTOS ofrmSetupTOS = new frmSetupTOS(_databaseconnectionstring,ID);
                        //                    ofrmSetupTOS.ShowDialog(this);
                        //                    Fill_TOS();
                        //                }
                        //                break;
                        //            case "Delete":
                        //                {
                        //                }
                        //                break;
                        //            case "Refresh":
                        //                Fill_TOS();
                        //                break;
                        //            case "Close":
                        //                {
                        //                    this.Close();
                        //                }
                        //                break;

                        //        }

                        //    }
                        //    break;
                        //case 13: //Place Of Service 
                        //    {
                        //        dgResource.Visible = true;
                        //        c1AppointmentType.Visible = false;
                        //        switch (e.ClickedItem.Tag.ToString())
                        //        {
                        //            case "Add":
                        //                {
                        //                    frmSetupPOS ofrmSetupPOS = new frmSetupPOS(_databaseconnectionstring, 0);
                        //                    ofrmSetupPOS.ShowDialog(this);
                        //                    Fill_POS();
                        //                }
                        //                break;
                        //            case "Modify":
                        //                {
                        //                    frmSetupPOS ofrmSetupPOS = new frmSetupPOS(_databaseconnectionstring, ID);
                        //                    ofrmSetupPOS.ShowDialog(this);
                        //                    Fill_POS();
                        //                }
                        //                break;
                        //            case "Delete":
                        //                {
                        //                }
                        //                break;
                        //            case "Refresh":
                        //                Fill_POS();
                        //                break;
                        //            case "Close":
                        //                {
                        //                    this.Close();
                        //                }
                        //                break;
                        //        }

                        //    }
                        //    break;

                        #endregion  ' Commented code of TOS & POS '

                        default:
                            if (e.ClickedItem.Tag.ToString() == "Close")
                            {
                                this.Close();
                            }
                            break;

                    }
                }
                else
                {
                    if (e.ClickedItem.Tag.ToString() == "Close")
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } //  

        #endregion

        public object AB_ValidateAppointmentType_Delete(Int64 nAppointmentTypeID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object ObjnAppointmentTypeID=null;
            oDB.Connect(false);

            try
            {
                oDBParameters.Add("@nAppointmentTypeID", nAppointmentTypeID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Flag", "AppointmentType", System.Data.ParameterDirection.Input, System.Data.SqlDbType.Text);
                ObjnAppointmentTypeID = oDB.ExecuteScalar("AB_ValidateFor_Delete", oDBParameters);
                if (ObjnAppointmentTypeID == null)
                {
                    return 0;
                }
                else
                {
                    if (Convert.ToString(ObjnAppointmentTypeID) == "")
                    {
                        return 0;
                    }
                }
                
                //if (ObjnAppointmentTypeID == "")
                //{
                //    ObjnAppointmentTypeID = 0;
                //}

                return ObjnAppointmentTypeID;
                
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                ObjnAppointmentTypeID = null;
            }
        }

        #region " Fill Methods "

        private void Fill_AppointmentBook()
        {
            TreeNode oNode;

            try
            {
                trvAppointmentBook.Nodes.Clear();

                //oNode = new TreeNode();
                //oNode.Text = "Resource Type";
                //oNode.Tag = "Resource Type";
                //oNode.ImageIndex = 0;
                //oNode.SelectedImageIndex = 0;
                //trvAppointmentBook.Nodes.Add(oNode);
                //// Default Select the "Resource Type"
                //trvAppointmentBook.SelectedNode = oNode;


                if (_UserName.Trim() != "")
                {
                    try
                    {
                        if (oClsgloUserRights != null)
                        {
                            oClsgloUserRights.Dispose();
                            oClsgloUserRights = null;
                        }
                    }
                    catch
                    {
                    }
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oClsgloUserRights.CheckForUserRights(_UserName);
                }
                if (oClsgloUserRights.Resource)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Resource";
                    oNode.Tag = "Resource";
                    oNode.ImageIndex = 1;
                    oNode.SelectedImageIndex = 1;
                    trvAppointmentBook.Nodes.Add(oNode);
                    trvAppointmentBook.SelectedNode = oNode;
                }

                if (oClsgloUserRights.AppointmentBlockType)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Appointment Block Type";
                    oNode.Tag = "Appointment Block Type";
                    oNode.ImageIndex = 2;
                    oNode.SelectedImageIndex = 2;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                if (oClsgloUserRights.ProblemType)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Problem Type";
                    oNode.Tag = "Problem Type";
                    oNode.ImageIndex = 4;
                    oNode.SelectedImageIndex = 4;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                if (oClsgloUserRights.AppointmentType)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Appointment Type";
                    oNode.Tag = "Appointment Type";
                    oNode.ImageIndex = 3;
                    oNode.SelectedImageIndex = 3;
                    trvAppointmentBook.Nodes.Add(oNode);
                }
                if (oClsgloUserRights.AppointmentStatus)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Appointment Status";
                    oNode.Tag = "Appointment Status";
                    oNode.ImageIndex = 5;
                    oNode.SelectedImageIndex = 5;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                if (oClsgloUserRights.Location)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Location";
                    oNode.Tag = "Location";
                    oNode.ImageIndex = 6;
                    oNode.SelectedImageIndex = 6;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                if (oClsgloUserRights.Department)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Department";
                    oNode.Tag = "Department";
                    oNode.ImageIndex = 7;
                    oNode.SelectedImageIndex = 7;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                if (oClsgloUserRights.Template)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Template";
                    oNode.Tag = "Template";
                    oNode.ImageIndex = 9;
                    oNode.SelectedImageIndex = 9;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                if (oClsgloUserRights.TemplateAllocation)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Template Allocation";
                    oNode.Tag = "Template Allocation";
                    oNode.ImageIndex = 11;
                    oNode.SelectedImageIndex = 11;
                    trvAppointmentBook.Nodes.Add(oNode);
                }


                //oNode = new TreeNode();
                //oNode.Text = "Users";
                //oNode.Tag = "Users";
                //oNode.ImageIndex = 1;
                //oNode.SelectedImageIndex = 1;
                //trvAppointmentBook.Nodes.Add(oNode);

                if (oClsgloUserRights.ProviderATS)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Provider Appointment Type Association";
                    oNode.Tag = "Provider Appointment Type Association";
                    oNode.ImageIndex = 12;
                    oNode.SelectedImageIndex = 12;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                if (oClsgloUserRights.Followup)
                {
                    //Anil 20081120
                    oNode = new TreeNode();
                    oNode.Text = "Follow Up";
                    oNode.Tag = "Follow Up";
                    oNode.ImageIndex = 13;
                    oNode.SelectedImageIndex = 13;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                //-------------- TOS & POS code moved to Billing Book so can be deleted from here -------------------------------
                //oNode = new TreeNode();
                //oNode.Text = "TOS";
                //oNode.Tag = 12;
                //oNode.ImageIndex = 12;
                //oNode.SelectedImageIndex = 12;
                //trvAppointmentBook.Nodes.Add(oNode);

                //oNode = new TreeNode();
                //oNode.Text = "POS";
                //oNode.Tag = 13;
                //oNode.ImageIndex = 13;
                //oNode.SelectedImageIndex = 13;
                //trvAppointmentBook.Nodes.Add(oNode);
                //-------------- TOS & POS code moved to Billing Book so can be deleted from here -------------------------------



                if (oClsgloUserRights.TemplateAllocation)
                {
                    oNode = new TreeNode();
                    oNode.Text = "Occupation";
                    oNode.Tag = "Occupation";
                    oNode.ImageIndex = 15;
                    oNode.SelectedImageIndex = 15;
                    trvAppointmentBook.Nodes.Add(oNode);
                }

                //oNode = new TreeNode();
                //oNode.Text = "Clinical Instruction";
                //oNode.Tag = "ClinicalInstruction";
                //oNode.ImageIndex = 16;
                //oNode.SelectedImageIndex = 16;
                //trvAppointmentBook.Nodes.Add(oNode);



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            { oNode = null; }
        
        }

        public void Fill_Resource(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string strQuery = " SELECT nResourceID,ISNULL(sCode,'') AS sCode, " +
                                  " ISNULL(sDescription,'') AS sDescription, " +
                                  " ISNULL(nResourceTypeID,0) AS nResourceTypeID, " +
                                  " ISNULL(bitIsBlocked,0) AS bitIsBlocked, " +
                                  " ISNULL(sUserName,'') AS sUserName " +
                                  " FROM  " +
                                  " AB_Resource_MST " +
                                  " WHERE  " +
                                  " bitIsBlocked = '" + false + "' AND nClinicID = " + this.ClinicID + " ";

                ////
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);
                _dv = dt.DefaultView;
                oDB.Disconnect();
                strQuery = null;
                dgResource.DataSource = _dv;
                //dgResource.Columns[0].HeaderText = "ResourceID";
                //dgResource.Columns[1].HeaderText = "Resource Name";
                //dgResource.Columns[3].HeaderText = "nResourceType";
                //dgResource.Columns[2].HeaderText = "Resource Type";
                //dgResource.Columns[4].HeaderText = "ResourceTypeID";
                //dgResource.Columns[5].HeaderText = "User Name";

                dgResource.Columns[0].HeaderText = "ResourceID";
                dgResource.Columns[1].HeaderText = "Code";
                dgResource.Columns[2].HeaderText = "Resource Name";
                dgResource.Columns[3].HeaderText = "ResourceTypeID";
                dgResource.Columns[4].HeaderText = "bitIsBlocked";
                dgResource.Columns[5].HeaderText = "User Name";

                dgResource.Columns[0].Visible = false;
                dgResource.Columns[1].Visible = true;
                dgResource.Columns[2].Visible = true;
                dgResource.Columns[3].Visible = false;
                dgResource.Columns[4].Visible = false;
                dgResource.Columns[5].Visible = true;

                int nWidth = dgResource.Width;
                dgResource.Columns[0].Width = 0;
                dgResource.Columns[1].Width = (int)(nWidth * 0.25 - 10);
                dgResource.Columns[2].Width = (int)(nWidth * 0.50 - 10);
                dgResource.Columns[3].Width = 0;
                dgResource.Columns[4].Width = 0;
                dgResource.Columns[5].Width = (int)(nWidth * 0.25 - 10);


                dgResource.Columns[0].DisplayIndex = 0;
                dgResource.Columns[1].DisplayIndex = 1;
                dgResource.Columns[2].DisplayIndex = 2;
                dgResource.Columns[3].DisplayIndex = 3;
                dgResource.Columns[5].DisplayIndex = 4;

                dgResource.ReadOnly = true;

                if (dgResource.DataSource != null && ID != 0)
                {
                    for (int i = 0; i < dgResource.Rows.Count; i++)
                    {
                        if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                        {
                            dgResource.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
                if (dgResource.RowCount > 0)
                {
                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                }
                if (dgResource.RowCount == 0)
                {
                    tsb_Modify.Enabled = false;
                    tsb_Delete.Enabled = false;
                }               
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Resource, ActivityType.View, "View Resource", ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
        }

        public void Fill_ResourceTypes(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //String strQuery = "SELECT nResourceTypeID, sResourceTypeDescription, nResourceType FROM AB_ResourceType_MST WHERE bitIsBlocked <> 1";
                String strQuery = "SELECT nResourceTypeID, sResourceTypeDescription, nResourceType FROM AB_ResourceType_MST WHERE bitIsBlocked <> 1 AND nClinicID = " + this.ClinicID + " ";
                //
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);
                _dv = dt.DefaultView;
                oDB.Disconnect();
                strQuery = null;

                dgResource.DataSource = _dv;
                dgResource.Columns[0].HeaderText = "ResourceTypeID";
                dgResource.Columns[1].HeaderText = "Resource Type";
                dgResource.Columns[2].HeaderText = "nResourceType";

                dgResource.Columns[0].Visible = false;
                dgResource.Columns[1].Visible = true;
                dgResource.Columns[2].Visible = false;

                int nWidth = dgResource.Width;
                dgResource.Columns[0].Width = 0;
                dgResource.Columns[1].Width = nWidth - 10;
                dgResource.Columns[2].Width = 0;

                dgResource.ReadOnly = true;

                if (dgResource.DataSource != null && ID != 0)
                {
                    for (int i = 0; i < dgResource.Rows.Count; i++)
                    {
                        if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                        {
                            dgResource.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.ResourceType, ActivityType.View, "View Resource Type", 0, 0, 0, ActivityOutCome.Success);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
            }
        }

        public void Fill_AppointmentBlockTypes(Int64 ID)
        {
            Books.AppointmentBlockType oAppointmentBlockType = new Books.AppointmentBlockType(_databaseconnectionstring);

            try
            {
                DataTable dt = null;
                dt = oAppointmentBlockType.GetList();
                _dv = dt.DefaultView;

                dgResource.DataSource = _dv;
                dgResource.Columns[0].HeaderText = "nAppointmentBlockTypeID";
                dgResource.Columns[1].HeaderText = "Appointment Block Type";
                dgResource.Columns[2].HeaderText = "nClinicID";

                dgResource.Columns[0].Visible = false;
                dgResource.Columns[1].Visible = true;
                dgResource.Columns[2].Visible = false;

                int nWidth = dgResource.Width;
                dgResource.Columns[0].Width = 0;
                dgResource.Columns[1].Width = nWidth - 10;
                dgResource.Columns[2].Width = 0;

                dgResource.ReadOnly = true;
                if (dgResource.DataSource != null && ID != 0)
                {
                    for (int i = 0; i < dgResource.Rows.Count; i++)
                    {
                        if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                        {
                            dgResource.Rows[i].Selected = true;
                            break;
                        }
                    }
                }
                if (dgResource.RowCount > 0)
                {
                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                }
                if (dgResource.RowCount == 0)
                {
                    tsb_Modify.Enabled = false;
                    tsb_Delete.Enabled = false;
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentBlockType, ActivityType.View, "View AppointmentBlock Type", 0, 0, 0, ActivityOutCome.Success);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oAppointmentBlockType.Dispose();
            }
        }

        public void Fill_AppointmentTypes(Int64 ID,String sFilter)
        {
            Books.AppointmentType oAppointmentType = new Books.AppointmentType(_databaseconnectionstring);

            try
            {
                DataTable dt = null;
                dt = oAppointmentType.GetList(AppointmentProcedureType.AppointmentType, sFilter);                
                if (dt != null)
                {
                    _dv = dt.DefaultView;

                    c1AppointmentType.Clear(ClearFlags.All);

                    c1AppointmentType.Rows.Count = 1;
                    c1AppointmentType.Cols.Count = 7;
                    c1AppointmentType.Cols[0].Caption = "nAppointmentTypeID";
                    c1AppointmentType.Cols[1].Caption = "Appointment Type";                    
                    c1AppointmentType.Cols[2].Caption = "Duration (Hrs:Mins)";   //Mayuri:20091229-Changed caption to "Duration (Minutes)" -To fix issue ID:#5053                    
                    c1AppointmentType.Cols[3].Caption = "Color Code";
                    c1AppointmentType.Cols[4].Caption = "Type";
                    c1AppointmentType.Cols[5].Caption = "nClinicID";
                    c1AppointmentType.Cols[6].Caption = "Prior Auth. Required";

                    c1AppointmentType.Cols[0].Visible = false;
                    c1AppointmentType.Cols[1].Visible = true;
                    c1AppointmentType.Cols[2].Visible = true;
                    c1AppointmentType.Cols[3].Visible = true;
                    c1AppointmentType.Cols[4].Visible = false;
                    c1AppointmentType.Cols[5].Visible = false;
                    c1AppointmentType.Cols[6].Visible = true;

                    c1AppointmentType.Cols[0].TextAlign = TextAlignEnum.LeftCenter;  
                    c1AppointmentType.Cols[1].TextAlign = TextAlignEnum.LeftCenter;
                    c1AppointmentType.Cols[2].TextAlign = TextAlignEnum.LeftCenter;
                    c1AppointmentType.Cols[3].TextAlign = TextAlignEnum.LeftCenter;
                    c1AppointmentType.Cols[4].TextAlign = TextAlignEnum.LeftCenter;
                    c1AppointmentType.Cols[5].TextAlign = TextAlignEnum.LeftCenter;
                    c1AppointmentType.Cols[6].TextAlign = TextAlignEnum.LeftCenter;


                    int nWidth = c1AppointmentType.Width;
                    c1AppointmentType.Cols[0].Width = 0;
                    c1AppointmentType.Cols[1].Width = (int)(0.5 * (nWidth) - 10);
                    c1AppointmentType.Cols[2].Width = (int)(0.15 * (nWidth) - 10);
                    c1AppointmentType.Cols[3].Width = (int)(0.25 * (nWidth) - 10);
                    c1AppointmentType.Cols[4].Width = 0;
                    c1AppointmentType.Cols[5].Width = 0;
                    c1AppointmentType.Cols[6].Width = (int)(0.15 * (nWidth) - 10);

                    c1AppointmentType.DrawMode = DrawModeEnum.Normal;
                    c1AppointmentType.AllowEditing = false;

                    c1AppointmentType.BringToFront();



                    int i;
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (c1AppointmentType.Rows.Count - 1 <= i)
                        {
                            c1AppointmentType.Rows.Add();
                        }
                        // added by sandip dhakane convert decimal to timespan 20100716
                        TimeSpan tspan = TimeSpan.FromMinutes(Convert.ToInt64(dt.Rows[i]["nDuration"]));
                        // end code

                        c1AppointmentType.SetData(i + 1, 0, Convert.ToInt64(dt.Rows[i]["nAppointmentTypeID"]));
                        c1AppointmentType.SetData(i + 1, 1, Convert.ToString(dt.Rows[i]["sAppointmentType"]));

                        // added by sandip dhakane convert decimal to timespan 20100716
                        //c1AppointmentType.SetData(i + 1, 2, Convert.ToInt64(dt.Rows[i]["nDuration"]));
                        c1AppointmentType.SetData(i + 1, 2, tspan);
                        // end code

                        c1AppointmentType.SetData(i + 1, 4, dt.Rows[i]["nAppProcType"]);
                        c1AppointmentType.SetData(i + 1, 5, dt.Rows[i]["nClinicID"]);
                        c1AppointmentType.SetData(i + 1, 6, dt.Rows[i]["bIsPriorAuthRequired"]);

                        C1.Win.C1FlexGrid.CellStyle cStyle;
                        c1AppointmentType.Cols[3].UserData = dt.Rows[i]["sColorCode"];
                        c1AppointmentType.SetData(i + 1, 3, "     ");
                        //c1AppointmentType.SetData(i+1, 3, "     " + dt.Rows[i]["sColorCode"]);
                        C1.Win.C1FlexGrid.CellRange rgBubbleValues = c1AppointmentType.GetCellRange(i + 1, 3);
                        //cStyle = c1AppointmentType.Styles.Add("BubbleValues" + i);
                        string mystring = "BubbleValues" + i;
                        try
                        {
                            if (c1AppointmentType.Styles.Contains(mystring))
                            {
                                cStyle = c1AppointmentType.Styles[mystring];
                            }
                            else
                            {
                                cStyle = c1AppointmentType.Styles.Add(mystring);
  
                            }

                        }
                        catch
                        {
                            cStyle = c1AppointmentType.Styles.Add(mystring);
  
                        }
                        cStyle.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[i]["sColorCode"]));  // Color.Blue;

                        rgBubbleValues.Style = cStyle;
                    }
                    string ID1 = Convert.ToString(ID);
                    if (ID == 0 && c1AppointmentType.Rows.Count > 1)
                    {
                        c1AppointmentType.Row = 1;
                    }
                    else
                    {
                        int RowIndex = c1AppointmentType.FindRow(ID1, 1, 0, false, false, false);
                        c1AppointmentType.Row = RowIndex;
                    }

                }
                c1AppointmentType.Cols[6].Move(3);
                //Check rows in the grid 
                if (c1AppointmentType.Rows.Count > 1)
                {

                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                }
                if (c1AppointmentType.Rows.Count <= 1)
                {
                    tsb_Modify.Enabled = false;
                    tsb_Delete.Enabled = false;
                }

                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentType, ActivityType.View, "View Appointment Type", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oAppointmentType.Dispose();
            }
        }

        public void Fill_Procedures(Int64 ID)
        {
            Books.AppointmentType oAppointmentType = new Books.AppointmentType(_databaseconnectionstring);

            try
            {
                DataTable dt = null;
                dt = oAppointmentType.GetList(AppointmentProcedureType.Procedure);
                if (dt != null)
                {
                    _dv = dt.DefaultView;
                    //nAppointmentTypeID, sAppointmentType, nDuration, sColorCode,nAppProcType, nClinicID
                    dgResource.DataSource = _dv;
                    dgResource.Columns[0].HeaderText = "nAppointmentTypeID";
                    dgResource.Columns[1].HeaderText = "Problem Type";
                    dgResource.Columns[2].HeaderText = "Duration";
                    dgResource.Columns[3].HeaderText = "Color Code";
                    dgResource.Columns[4].HeaderText = "Type";
                    dgResource.Columns[5].HeaderText = "nClinicID";

                    dgResource.Columns[0].Visible = false;
                    dgResource.Columns[1].Visible = true;
                    dgResource.Columns[2].Visible = false;
                    dgResource.Columns[3].Visible = false;
                    dgResource.Columns[4].Visible = false;
                    dgResource.Columns[5].Visible = false;

                    int nWidth = dgResource.Width;
                    dgResource.Columns[0].Width = 0;
                    dgResource.Columns[1].Width = (int)(1 * (nWidth) - 10);
                    dgResource.Columns[2].Width = 0;
                    dgResource.Columns[3].Width = 0;//(int)(0.25 * (nWidth) - 10);
                    dgResource.Columns[4].Width = 0;
                    dgResource.Columns[5].Width = 0;

                    dgResource.ReadOnly = true;
                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }

                }
                if (dgResource.RowCount > 0)
                {
                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                }
                if (dgResource.RowCount == 0)
                {
                    tsb_Modify.Enabled = false;
                    tsb_Delete.Enabled = false;
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.ProblemType, ActivityType.View, "View Problem Type", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oAppointmentType.Dispose();
            }
        }

        public void Fill_AppointmentStatus(Int64 ID)
        {
            Books.AppointmentStatus oAppointmentStatus = new Books.AppointmentStatus(_databaseconnectionstring);

            try
            {
                DataTable dt = null;
                dt = oAppointmentStatus.GetList();
                if (dt != null)
                {
                    _dv = dt.DefaultView;
                    //nAppointmentStatusID, sAppointmentStatus, nClinicID
                    dgResource.DataSource = _dv; 
                    dgResource.Columns[0].HeaderText = "nAppointmentStatusID";
                    dgResource.Columns[1].HeaderText = "Appointment Status";
                    dgResource.Columns[2].HeaderText = "nClinicID";
                    dgResource.Columns[3].HeaderText = "Record Type";

                   

                    dgResource.Columns[0].Visible = false;
                    dgResource.Columns[1].Visible = true;
                    dgResource.Columns[2].Visible = false;
                    dgResource.Columns[3].Visible = true;

                    int nWidth = dgResource.Width;
                    dgResource.Columns[0].Width = 0;
                    dgResource.Columns[1].Width = (int)(0.5 * (nWidth) - 10); 
                    dgResource.Columns[2].Width = 0;
                    dgResource.Columns[2].Width = (int)nWidth -50;


                    dgResource.ReadOnly = true;
                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                   
                    if (dgResource.RowCount > 0)
                    {
                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                    }
                    if (dgResource.RowCount == 0)
                    {
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.AppointmentStatus, ActivityType.View, "View Appointment Status", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oAppointmentStatus.Dispose();
            }
        }

        public void Fill_Department(Int64 ID)
        {
            Books.Department oDepartment = new Books.Department(_databaseconnectionstring);

            try
            {
                DataTable dt = null;
                dt = oDepartment.GetList();

                if (dt != null)
                {
                    _dv = dt.DefaultView;
                    //nDepartmentID,  sDepartment , nLocationID , AB_Location.sLocation AS sLocation , AB_Department.nClinicID AS nClinicID 
                    dgResource.DataSource = _dv;
                    dgResource.Columns["nDepartmentID"].HeaderText = "DepartmentID";
                    dgResource.Columns["sDepartment"].HeaderText = "Department";
                    dgResource.Columns["nLocationID"].HeaderText = "LocationID";
                    dgResource.Columns["sLocation"].HeaderText = "Location";
                    dgResource.Columns["nClinicID"].HeaderText = "ClinicID";

                    dgResource.Columns["nDepartmentID"].Visible = false;
                    dgResource.Columns["sDepartment"].Visible = true;
                    dgResource.Columns["nLocationID"].Visible = false;
                    dgResource.Columns["sLocation"].Visible = true;
                    dgResource.Columns["nClinicID"].Visible = false;

                    int nWidth = dgResource.Width;
                    dgResource.Columns["nDepartmentID"].Width = 0;
                    dgResource.Columns["sDepartment"].Width = (int)(0.5 * (nWidth) - 10);
                    dgResource.Columns["nLocationID"].Width = 0;
                    dgResource.Columns["sLocation"].Width = (int)(0.5 * (nWidth) - 10);
                    dgResource.Columns["nClinicID"].Width = 0;


                    dgResource.Columns["nDepartmentID"].DisplayIndex = 0;
                    dgResource.Columns["sDepartment"].DisplayIndex = 1;
                    dgResource.Columns["nLocationID"].DisplayIndex = 2;
                    dgResource.Columns["sLocation"].DisplayIndex = 3;
                    dgResource.Columns["nClinicID"].DisplayIndex = 4;

                    dgResource.ReadOnly = true;
                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                    if (dgResource.RowCount > 0)
                    {
                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                    }
                    if (dgResource.RowCount <= 0)
                    {
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Department, ActivityType.View, "View Department", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDepartment.Dispose();
            }
        }

        public void Fill_Location(Int64 ID)
        {
            Books.Location oLocation = new Books.Location();

            try
            {
                DataTable dt = null;
                dt = oLocation.GetList();

                if (dt != null)
                {
                    _dv = dt.DefaultView;
                    //nLocationID, sLocation, nClinicID
                    dgResource.DataSource = _dv;
                    dgResource.Columns[0].HeaderText = "nLocationID";
                    dgResource.Columns[1].HeaderText = "Location";
                    dgResource.Columns[2].HeaderText = "Address Line1";
                    dgResource.Columns[3].HeaderText = "Address Line2";
                    dgResource.Columns[4].HeaderText = "City";
                    dgResource.Columns[5].HeaderText = "State";
                    dgResource.Columns[6].HeaderText = "Zip";
                    dgResource.Columns[7].HeaderText = "County";
                    dgResource.Columns[8].HeaderText = "nClinicID";
                    dgResource.Columns[9].HeaderText = "Default Location";
                   

                    dgResource.Columns[0].Visible = false;
                    dgResource.Columns[1].Visible = true;
                    dgResource.Columns[2].Visible = true;
                    dgResource.Columns[3].Visible = true;
                    dgResource.Columns[4].Visible = true;
                    dgResource.Columns[5].Visible = true;
                    dgResource.Columns[6].Visible = true;
                    dgResource.Columns[7].Visible = true;
                    dgResource.Columns[8].Visible = false;
                    dgResource.Columns[9].Visible = true;
                  

                    int nWidth = dgResource.Width;
                    dgResource.Columns[0].Width = 0;
                    dgResource.Columns[1].Width = (int)(nWidth * 0.14);
                    dgResource.Columns[2].Width = (int)(nWidth * 0.15);
                    dgResource.Columns[3].Width = (int)(nWidth * 0.15) - 1;
                    dgResource.Columns[4].Width = (int)(nWidth * 0.14);
                    dgResource.Columns[5].Width = (int)(nWidth * 0.14);
                    dgResource.Columns[6].Width = (int)(nWidth * 0.14);
                    dgResource.Columns[7].Width = (int)(nWidth * 0.14);
                    dgResource.Columns[8].Width = 0;
                    dgResource.Columns[9].Width = (int)(nWidth * 0.12); 
                    dgResource.ReadOnly = false;

                    dgResource.Columns[0].ReadOnly = true;
                    dgResource.Columns[1].ReadOnly = true;
                    dgResource.Columns[2].ReadOnly = true;
                    dgResource.Columns[3].ReadOnly = true;
                    dgResource.Columns[4].ReadOnly = true;
                    dgResource.Columns[5].ReadOnly = true;
                    dgResource.Columns[6].ReadOnly = true;
                    dgResource.Columns[7].ReadOnly = true;
                    dgResource.Columns[8].ReadOnly = true;
                    dgResource.Columns[9].ReadOnly = true;
                  
                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                    if (dgResource.RowCount > 0)
                    {
                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                    }
                    if (dgResource.RowCount <= 0)
                    {
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Location, ActivityType.View, "View Location", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oLocation.Dispose();
            }
        }

        public void Fill_Relationship(Int64 ID)
        {
            Books.Location oLocation = new Books.Location();

            try
            {
                DataTable dt = null;
                dt = oLocation.GetList();
                if (dt != null)
                {
                    _dv = dt.DefaultView;
                    //nLocationID, sLocation, nClinicID
                    dgResource.DataSource = _dv;
                    dgResource.Columns[0].HeaderText = "nLocationID";
                    dgResource.Columns[1].HeaderText = "Relationship";
                    dgResource.Columns[2].HeaderText = "nClinicID";
                   
                    dgResource.Columns[0].Visible = false;
                    dgResource.Columns[1].Visible = true;
                    dgResource.Columns[2].Visible = false;

                    int nWidth = dgResource.Width;
                    dgResource.Columns[0].Width = 0;
                    dgResource.Columns[1].Width = (nWidth - 10);
                    dgResource.Columns[2].Width = 0;

                    dgResource.ReadOnly = true;
                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Relationship, ActivityType.View, "View Relationship", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oLocation.Dispose();
            }
        }

        public void Fill_TemplateAllocation(Int64 ID)
        {
            lblTemplateName.Visible = false;
            lblTemplateName.Text = string.Empty;

            tsb_DayView.Visible = true;
            tsb_WeekView.Visible = true;
            tsb_MonthView.Visible = true;

            Resource oProvider = new Resource(_databaseconnectionstring);
            DataTable dtProviders;
            dtProviders = oProvider.GetProviders();

            if (oProvider != null) { oProvider.Dispose(); oProvider = null; }

            TreeNode oNode = new TreeNode();
            CalendarTemplate.Appointments.Clear();
            bool isnodePresent = false;
            pnlCalendarTemplate.Visible = true;
            if (dtProviders != null)
            {


                for (int i = 0; i < dtProviders.Rows.Count; i++)
                {
                    oNode = new TreeNode();
                    oNode.Text = Convert.ToString(dtProviders.Rows[i]["ProviderName"]);
                    oNode.Tag = "Template Allocation" + "~" + dtProviders.Rows[i]["nProviderID"];
                    oNode.ImageIndex = 10;
                    oNode.SelectedImageIndex = 10;

                    foreach (TreeNode childNode in trvAppointmentBook.SelectedNode.Nodes)
                    {
                        if (Convert.ToString(childNode.Tag) == Convert.ToString(oNode.Tag))
                        {
                            isnodePresent = true;
                            break;
                        }
                    }
                    if (isnodePresent == false)
                    {
                        trvAppointmentBook.SelectedNode.Nodes.Add(oNode);
                    }
                    oNode = null;
                }

            }
            if (trvAppointmentBook.SelectedNode.Level == 0)
            {
                if (trvAppointmentBook.SelectedNode.Nodes.Count > 0)
                {

                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                }
                if (trvAppointmentBook.SelectedNode.Nodes.Count <= 0)
                {
                    tsb_Modify.Enabled = false;
                    tsb_Delete.Enabled = false;
                }
            }


            trvAppointmentBook.ExpandAll();
            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.TemplateAllocation, ActivityType.View, "View Template Allocation", 0, 0, 0, ActivityOutCome.Success);

        }

        public void Fill_Occupation(Int64 ID)
        {
            Books.Occupation oOccupation = new Books.Occupation();

            try
            {
                DataTable dt = null;
                dt = oOccupation.GetList();
                if (dt != null)
                {
                    _dv = dt.DefaultView;
                    //nAppointmentStatusID, sAppointmentStatus, nClinicID
                    dgResource.DataSource = _dv;
                    dgResource.Columns[0].HeaderText = "nOccupationID";
                    dgResource.Columns[1].HeaderText = "Employer";
                    dgResource.Columns[2].HeaderText = "Occupation";
                    dgResource.Columns[3].HeaderText = "Place of Employment";
                    dgResource.Columns[4].HeaderText = "Address1";
                    dgResource.Columns[5].HeaderText = "Address2";
                    dgResource.Columns[6].HeaderText = "City";
                    dgResource.Columns[7].HeaderText = "State";
                    dgResource.Columns[8].HeaderText = "Zip";
                    dgResource.Columns[9].HeaderText = "Country";
                    dgResource.Columns[10].HeaderText = "Phone";
                    dgResource.Columns[11].HeaderText = "Mobile";
                    dgResource.Columns[12].HeaderText = "Fax";
                    dgResource.Columns[13].HeaderText = "Email";                    


                    dgResource.Columns[0].Visible = false;
                    dgResource.Columns[1].Visible = true;
                    dgResource.Columns[2].Visible = true;
                    dgResource.Columns[3].Visible = true;
                    dgResource.Columns[4].Visible = false;
                    dgResource.Columns[5].Visible = false;
                    dgResource.Columns[6].Visible = true;
                    dgResource.Columns[7].Visible = false;
                    dgResource.Columns[8].Visible = false;
                    dgResource.Columns[9].Visible = false;
                    dgResource.Columns[10].Visible = true;
                    dgResource.Columns[11].Visible = true;
                    dgResource.Columns[12].Visible = false;
                    dgResource.Columns[13].Visible = false;
                    


                    int nWidth = dgResource.Width;
                    dgResource.Columns[0].Width = 0;
                    dgResource.Columns[1].Width = (int)(0.2 * nWidth);
                    dgResource.Columns[2].Width = (int)(0.2 * nWidth);
                    dgResource.Columns[3].Width = (int)(0.2 * nWidth);
                    dgResource.Columns[4].Width = 0;
                    dgResource.Columns[5].Width = 0;
                    dgResource.Columns[6].Width = (int)(0.15 * nWidth); 
                    dgResource.Columns[7].Width = 0;
                    dgResource.Columns[8].Width = 0;
                    dgResource.Columns[9].Width = 0;
                    dgResource.Columns[10].Width = (int)(0.1 * nWidth); 
                    dgResource.Columns[11].Width = (int)(0.1 * nWidth); 
                    dgResource.Columns[12].Width = 0;
                    dgResource.Columns[13].Width = 0;


                    dgResource.ReadOnly = true;
                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }

                    if (dgResource.RowCount > 0)
                    {
                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                    }
                    if (dgResource.RowCount == 0)
                    {
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.None, ActivityType.View, "View Occupation", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oOccupation.Dispose();
            }
        }


        //Fill Template Allocation Sub Nodes
        private void Fill_trvTemplateAllocation()
        {

            tsb_DayView.Visible = true;
            tsb_WeekView.Visible = true;
            tsb_MonthView.Visible = true;

            //DateTime _FillStartDateTime = CalendarTemplate.Dates[0];
            //DateTime _FillEndDateTime = CalendarTemplate.Dates[CalendarTemplate.Dates.Count - 1];

            DateTime _FillStartDateTime = SelectedStartDate;
            DateTime _FillEndDateTime = SelectedEndDate;

            //setting dates to extend templates
            _ExtendTempStartTime = _FillStartDateTime;
            _ExtendTempEndTime = _FillEndDateTime;

            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate();
            AppointmentTemplate oAppointmentTemplate = null;
            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            pnlCalendarTemplate.Visible = true;
            DataTable dtAllocations = null;
            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseconnectionstring);

            lblTemplateName.Visible = true;
            lblTemplateName.Text = "  Provider Name : " + trvAppointmentBook.SelectedNode.Text;

            Int64 _nProviderID = Convert.ToInt64(GetTagElement(Convert.ToString(trvAppointmentBook.SelectedNode.Tag), '~', 2));
            //setting providerID to extend templates
            _ExtendTempProviderID = _nProviderID;
            dtAllocations = ogloTemplate.GetTemplateAllocations(_nProviderID, _FillStartDateTime, _FillEndDateTime);
            CalendarTemplate.Appointments.Clear();
            try
            {
                if (dtAllocations != null)
                {
                    for (int i = 0; i < dtAllocations.Rows.Count; i++)
                    {
                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                        oJUC_Appointment.Text = Convert.ToString(dtAllocations.Rows[i]["sAppointmentTypeDesc"]) + " - " + Convert.ToString(dtAllocations.Rows[i]["sTemplateName"]);
                        oJUC_Appointment.Description = "";
                        oJUC_Appointment.Prefix = "";
                        oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(Convert.ToInt32(dtAllocations.Rows[i]["nColorCode"].ToString()));
                        // 23-Jan-15 Aniket: Resolving issue to make fore colour visible as per mail by phill with the subject 'Calendar Screen Shots'
                        oJUC_Appointment.FormatStyle.ForeColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(oJUC_Appointment.FormatStyle.BackColor);

                        bool _ErrorFound = false;
                        try
                        {
                            oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtStartDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtStartTime"].ToString()));
                            oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtEndDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtEndTime"].ToString()));
                        }
                        catch { _ErrorFound = true; }

                        if (_ErrorFound == true)
                        {
                            try
                            {
                                oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtEndDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtEndTime"].ToString()));
                                oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dtAllocations.Rows[i]["dtStartDate"])), System.Convert.ToInt32(dtAllocations.Rows[i]["dtStartTime"].ToString()));
                            }
                            catch
                            {
                            }
                        }

                        CalendarTemplate.Appointments.Add(oJUC_Appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                ogloAppointmentTemplate.Dispose();
                if (oAppointmentTemplate != null)
                {
                    oAppointmentTemplate.Dispose();
                }
                oJUC_Appointment = null;
                if (dtAllocations != null) { dtAllocations.Dispose(); dtAllocations = null; }
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
            }



        }

        public void Fill_Template(Int64 TemplateID)
        {
            gloAppointmentTemplate ogloTemplate = null;
            DataTable dtTemplates = null;
            TreeNode oNode = null;
            try
            {
                lblTemplateName.Visible = false;
                lblTemplateName.Text = string.Empty;

                CalendarTemplate.Appointments.Clear();
                CalendarTemplate.Date = DateTime.Today.Date;
                CalendarTemplate.View = Janus.Windows.Schedule.ScheduleView.DayView;
                ogloTemplate = new gloAppointmentTemplate(_databaseconnectionstring);
                dtTemplates = ogloTemplate.GetTemplates();
                oNode = new TreeNode();
                bool isnodePresent = false;
                pnlCalendarTemplate.Visible = true;


                //CalendarTemplate.Appointments.Clear();

                if (dtTemplates != null)
                {
                    for (int i = 0; i < dtTemplates.Rows.Count; i++)
                    {
                        isnodePresent = false;
                        oNode = new TreeNode();
                        oNode.Text = Convert.ToString(dtTemplates.Rows[i]["sAppointmentTemplates"]);
                        oNode.Tag = "Template~" + dtTemplates.Rows[i]["nAppointmentTemplateID"];
                        oNode.ImageIndex = 10;
                        oNode.SelectedImageIndex = 10;

                        foreach (TreeNode childNode in trvAppointmentBook.SelectedNode.Nodes)
                        {
                            if (Convert.ToString(childNode.Tag) == Convert.ToString(oNode.Tag))
                            {
                                isnodePresent = true;
                                break;
                            }
                        }
                        if (isnodePresent == false)
                        {
                            trvAppointmentBook.SelectedNode.Nodes.Add(oNode);
                        }

                    }

                }
                if (trvAppointmentBook.SelectedNode.Level == 0)
                {
                    if (trvAppointmentBook.SelectedNode.Nodes.Count > 0)
                    {

                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                    }
                    if (trvAppointmentBook.SelectedNode.Nodes.Count <= 0)
                    {
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                    }
                }

                trvAppointmentBook.ExpandAll();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Template, ActivityType.View, "View Template ", 0, 0, 0, ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oNode = null;
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
                if (dtTemplates != null) { dtTemplates.Dispose(); dtTemplates = null; }
            }
        }

        //Fill Template Sub Nodes
        private void Fill_trvTemplates()
        {

            pnlTemplate.BringToFront();
            tsb_DayView.Visible = false;
            tsb_WeekView.Visible = false;
            tsb_MonthView.Visible = false;

            CalendarTemplate.Date = DateTime.Today.Date;
            CalendarTemplate.View = Janus.Windows.Schedule.ScheduleView.DayView;
            CalendarTemplate.Date = DateTime.Now;

            DateTime _FillStartDateTime = CalendarTemplate.Dates[0];
            DateTime _FillEndDateTime = CalendarTemplate.Dates[CalendarTemplate.Dates.Count - 1];

            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate();
            AppointmentTemplate oAppointmentTemplate = null;
            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            pnlCalendarDetail.Visible = false;
            pnlCalendarTemplate.Visible = true;
            //Janus.Windows.Schedule.ScheduleHourRange oRange;
            CalendarTemplate.WorkingHourSchema.WorkingHoursRange.Clear();
            CalendarTemplate.Appointments.Clear();

            lblTemplateName.Visible = true;
            lblTemplateName.Text = "  Template Name : " + trvAppointmentBook.SelectedNode.Text;

            Int64 _nTemplateID = Convert.ToInt64(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2));
            oAppointmentTemplate = ogloAppointmentTemplate.GetTemplate(_nTemplateID);
            try
            {

                for (int i = 0; i < oAppointmentTemplate.TemplateDetails.Count; i++)
                {
                    oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                    oJUC_Appointment.Text = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeDesc;
                    oJUC_Appointment.Description = "";
                    oJUC_Appointment.Prefix = "";
                    oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(oAppointmentTemplate.TemplateDetails[i].ColorCode);

                    // 23-Jan-15 Aniket: Resolving issue to make fore colour visible as per mail by phill with the subject 'Calendar Screen Shots'
                    oJUC_Appointment.FormatStyle.ForeColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(oJUC_Appointment.FormatStyle.BackColor);

                    bool _ErrorFound = false;
                    try
                    {
                        oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].EndTime);
                        oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].StartTime);
                    }
                    catch { _ErrorFound = true; }

                    if (_ErrorFound == true)
                    {
                        try
                        {
                            oJUC_Appointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].StartTime);
                            oJUC_Appointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].EndTime);
                        }
                        catch { }
                    }

                    CalendarTemplate.Appointments.Add(oJUC_Appointment);
                }

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloAppointmentTemplate.Dispose();
                if (oAppointmentTemplate != null)
                {
                    oAppointmentTemplate.Dispose();
                }
                oJUC_Appointment = null;
            }
        }

        private void Fill_Users(string UserType, Int64 ID)
        {
            gloUser ogloUsers;
            DataView _dv;
            ogloUsers = new gloUser(_databaseconnectionstring);
            DataTable dtUsers = null;
            try
            {
                switch (UserType)
                {
                    case "Active Users": // Fill Active Users  
                        dtUsers = ogloUsers.getActiveUsers();
                        break;
                    case "Blocked Users": // Fill Blocked Users
                        dtUsers = ogloUsers.getBlockedUsers();
                        break;
                    case "All Users": // Fill All Users
                        dtUsers = ogloUsers.getAllUsers();
                        break;
                    default:
                        
                        dgResource.DataSource = null;
                        dgResource.Refresh();
                        break;
                }

                if (dtUsers != null)
                {
                    _dv = dtUsers.DefaultView;
                    dgResource.DataSource = _dv;

                    dgResource.Columns[0].HeaderText = "UserId";
                    dgResource.Columns[1].HeaderText = "Login Name";
                    dgResource.Columns[2].HeaderText = "Full Name";
                    dgResource.Columns[3].HeaderText = "Phone No";
                    dgResource.Columns[4].HeaderText = "Mobile No";
                    dgResource.Columns[5].HeaderText = "Is Admin";


                    dgResource.Columns[0].Visible = false;
                    dgResource.Columns[1].Visible = true;
                    dgResource.Columns[2].Visible = true;
                    dgResource.Columns[3].Visible = true;
                    dgResource.Columns[4].Visible = true;
                    dgResource.Columns[5].Visible = true;

                    int nWidth = dgResource.Width;
                    dgResource.Columns[0].Width = 0;
                    dgResource.Columns[1].Width = nWidth / 5;
                    dgResource.Columns[2].Width = nWidth / 5;
                    dgResource.Columns[3].Width = nWidth / 5;
                    dgResource.Columns[4].Width = nWidth / 5;
                    dgResource.Columns[5].Width = nWidth / 5;

                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloUsers != null) { ogloUsers.Dispose(); }
                if (dtUsers != null) { dtUsers.Dispose(); }
            }
        }

        //Fill User Sub Nodes
        private void Fill_trvUsers()
        {
            TreeNode oNode;
            bool isnodePresent = false;

            //Active Users Node
            oNode = new TreeNode();
            oNode.Text = "Active Users";
            oNode.Tag = "Users~" + "Active Users";
            oNode.ImageIndex = 10;
            oNode.SelectedImageIndex = 10;

            foreach (TreeNode childNode in trvAppointmentBook.SelectedNode.Nodes)
            {
                if (Convert.ToString(childNode.Tag) == Convert.ToString(oNode.Tag))
                {
                    isnodePresent = true;
                    break;
                }
            }
            if (isnodePresent == false)
            {
                trvAppointmentBook.SelectedNode.Nodes.Add(oNode);
            }

            //Blocked Users Node
            oNode = new TreeNode();
            oNode.Text = "Blocked Users";
            oNode.Tag = "Users~" + "Blocked Users";
            oNode.ImageIndex = 10;
            oNode.SelectedImageIndex = 10;
            foreach (TreeNode childNode in trvAppointmentBook.SelectedNode.Nodes)
            {
                if (Convert.ToString(childNode.Tag) == Convert.ToString(oNode.Tag))
                {
                    isnodePresent = true;
                    break;
                }
            }
            if (isnodePresent == false)
            {
                trvAppointmentBook.SelectedNode.Nodes.Add(oNode);
            }

            //All Users Node
            oNode = new TreeNode();
            oNode.Text = "All Users";
            oNode.Tag = "Users~" + "All Users";
            oNode.ImageIndex = 10;
            oNode.SelectedImageIndex = 10;
            //trvAppointmentBook.Nodes[0].Nodes.Add(oNode);

            foreach (TreeNode childNode in trvAppointmentBook.SelectedNode.Nodes)
            {
                if (Convert.ToString(childNode.Tag) == Convert.ToString(oNode.Tag))
                {
                    isnodePresent = true;
                    break;
                }
            }
            if (isnodePresent == false)
            {
                trvAppointmentBook.SelectedNode.Nodes.Add(oNode);
            }

            trvAppointmentBook.SelectedNode.ExpandAll();

            oNode = null;
        }

        private void Fill_AptmntType_Provider(Int64 ID)
        {
            Books.AppointmentType oApptype = new Books.AppointmentType(_databaseconnectionstring);

            try
            {
                DataTable dt = null;

                dt = oApptype.GetApptmnt_Providers();
                if (dt != null)
                {
                    _dv = dt.DefaultView;

                    dgResource.DataSource = _dv;
                    dgResource.Columns["nProviderID"].HeaderText = "ProviderID";
                    dgResource.Columns["sname"].HeaderText = "Provider Name";
                    dgResource.Columns["sAppointmentTypeDesc"].HeaderText = "Appointment Type ";
                    dgResource.Columns["sAppointmentTypeCode"].HeaderText = "Appointment Type Code";
                    dgResource.Columns["nAppointmentTypeFlag"].HeaderText = "Appointment Type Flag";
                    //dgResource.Columns["nClinicID"].HeaderText = "ClinicID";

                    dgResource.Columns["nProviderID"].Visible = false;
                    dgResource.Columns["sname"].Visible = true;
                    dgResource.Columns["sAppointmentTypeDesc"].Visible = true;
                    dgResource.Columns["sAppointmentTypeCode"].Visible = false;
                    dgResource.Columns["nAppointmentTypeFlag"].Visible = false;
                    //dgResource.Columns["nClinicID"].Visible = false;

                    int nWidth = dgResource.Width;
                    dgResource.Columns["nProviderID"].Width = 0;
                    dgResource.Columns["sname"].Width = (int)(0.5 * (nWidth) - 10);
                    dgResource.Columns["sAppointmentTypeDesc"].Width = (int)(0.5 * (nWidth) - 10);
                    dgResource.Columns["sAppointmentTypeCode"].Width = 0;
                    dgResource.Columns["nAppointmentTypeFlag"].Width = 0;
                    //dgResource.Columns["nClinicID"].Width = 0;




                    dgResource.ReadOnly = true;
                    if (dgResource.DataSource != null && ID != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (ID == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                    if (dgResource.RowCount > 0)
                    {
                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                    }
                    if (dgResource.RowCount <= 0)
                    {
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Provider_AppointmentType_Association, ActivityType.View, "View Provider Appointment Type Association", 0, 0, 0, ActivityOutCome.Success);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oApptype.Dispose();
            }

        }

        public void Fill_FollowUps(Int64 FollowUpId)
        {
            gloAppointmentBook ogloAppointmentBook = new gloAppointmentBook(_databaseconnectionstring);

            try
            {
                DataTable dt = null;
                dt = ogloAppointmentBook.GetFollowUps(FollowUpId);
                if (dt != null)
                {
                    _dv = dt.DefaultView;

                    dgResource.DataSource = _dv;
                    dgResource.Columns[0].HeaderText = "nFollowUpID";
                    dgResource.Columns[1].HeaderText = "Follow Up Name";
                    dgResource.Columns[2].HeaderText = "Duration";
                    dgResource.Columns[3].HeaderText = "nCriteria";
                    dgResource.Columns[4].HeaderText = "Criteria";


                    dgResource.Columns[0].Visible = false;
                    dgResource.Columns[1].Visible = true;
                    dgResource.Columns[2].Visible = true;
                    dgResource.Columns[3].Visible = false;
                    dgResource.Columns[4].Visible = true;
                    dgResource.Columns[5].Visible = false;


                    int nWidth = dgResource.Width;
                    dgResource.Columns[0].Width = 0;
                    dgResource.Columns[1].Width = (int)(nWidth * 0.8);
                    dgResource.Columns[2].Width = (int)(nWidth * 0.2);
                    dgResource.Columns[3].Width = 0;
                    dgResource.Columns[4].Width = (int)(nWidth * 0.2);

                    dgResource.ReadOnly = true;
                    if (dgResource.DataSource != null && FollowUpId != 0)
                    {
                        for (int i = 0; i < dgResource.Rows.Count; i++)
                        {
                            if (FollowUpId == Convert.ToInt64(dgResource.Rows[i].Cells[0].Value))
                            {
                                dgResource.Rows[i].Selected = true;
                                break;
                            }
                        }
                    }
                    if (dgResource.RowCount > 0)
                    {
                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                    }
                    if (dgResource.RowCount <= 0)
                    {
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.FollowUp, ActivityType.View, "View FollowUp", 0, 0, 0, ActivityOutCome.Success);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloAppointmentBook.Dispose();
            }
        }

        private void AssignUserRights()
        {
            gloUserRights.ClsgloUserRights oLocalClsgloUserRights = null;
            try
            {

               
                if (_UserName.Trim() != "")
                {

                    oLocalClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oLocalClsgloUserRights.CheckForUserRights(_UserName);

                    if (trvAppointmentBook.Nodes.Count > 0)
                    {
                        for (int i = trvAppointmentBook.Nodes.Count - 1; i >= 0; i--)
                        {
                            if (trvAppointmentBook.Nodes[i].Name == "Resource")
                            {
                                if (!oLocalClsgloUserRights.Resource)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Appointment Block Type")
                            {
                                if (!oLocalClsgloUserRights.AppointmentBlockType)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Appointment Type")
                            {
                                if (!oLocalClsgloUserRights.AppointmentType)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Problem Type")
                            {
                                if (!oLocalClsgloUserRights.ProblemType)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Appointment Status")
                            {
                                if (!oLocalClsgloUserRights.AppointmentStatus)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Location")
                            {
                                if (!oLocalClsgloUserRights.Location)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Department")
                            {
                                if (!oLocalClsgloUserRights.Department)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Template")
                            {
                                if (!oLocalClsgloUserRights.Template)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Template Allocation")
                            {
                                if (!oLocalClsgloUserRights.TemplateAllocation)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Provider Appointment Type Association")
                            {
                                if (!oLocalClsgloUserRights.ProviderATS)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Follow Up")
                            {
                                if (!oLocalClsgloUserRights.Followup)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                            else if (trvAppointmentBook.Nodes[i].Name == "Occupation")
                            {
                                if (!oLocalClsgloUserRights.Followup)
                                    trvAppointmentBook.Nodes[i].Remove();
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                try
                {
                    if (oLocalClsgloUserRights != null)
                    {
                        oLocalClsgloUserRights.Dispose();
                        oLocalClsgloUserRights = null;
                    }
                }
                catch
                {
                }
            }
        }

        #region " Commented Code "

        //Code for TOS & POS shifted to Billing book so this can be deleted.


        //#region " TOS & POS Fill Methods "

        //public void Fill_TOS() 
        //{
        //    Books.Resource oResource = new Resource(_databaseconnectionstring);
        //    DataTable dtTOS = new DataTable();
        //    try
        //    {
        //        //Pass 0 to get all records
        //        dtTOS = oResource.GetTOS(0);
        //        if (dtTOS != null && dtTOS.Rows.Count > 0)
        //        {
        //            _dv = dtTOS.DefaultView;
        //            //nTOSID,sDescription
        //            dgResource.DataSource = _dv;
        //            dgResource.Columns[0].HeaderText = "TOSID";
        //            dgResource.Columns[1].HeaderText = "Description";

        //            dgResource.Columns[0].Visible = false;
        //            dgResource.Columns[1].Visible = true;

        //            int nWidth = dgResource.Width;
        //            dgResource.Columns[0].Width = 0;
        //            dgResource.Columns[1].Width = (nWidth - 10) / 1;

        //            dgResource.ReadOnly = true;

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(" ERROR : Cannot get Record TOS ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); 
        //    }
        //    finally
        //    {
        //        oResource.Dispose();
        //    }
        //}

        //public void Fill_POS() 
        //{
        //    Books.Resource oResource = new Resource(_databaseconnectionstring);
        //    DataTable dtPOS = new DataTable();
        //    try
        //    {
        //        dtPOS = oResource.GetPOS(0);
        //        if (dtPOS != null && dtPOS.Rows.Count > 0)
        //        {
        //            _dv = dtPOS.DefaultView;
        //            //nPOSID,sPOSCode,sPOSName,sPOSDescription
        //            dgResource.DataSource = _dv;
        //            dgResource.Columns[0].HeaderText = "POSID";
        //            dgResource.Columns[1].HeaderText = "Code";
        //            dgResource.Columns[2].HeaderText = "Name";
        //            dgResource.Columns[3].HeaderText = "Description";

        //            dgResource.Columns[0].Visible = false;
        //            dgResource.Columns[1].Visible = true;
        //            dgResource.Columns[2].Visible = true;
        //            dgResource.Columns[3].Visible = true;

        //            int nWidth = dgResource.Width;
        //            dgResource.Columns[0].Width = 0;
        //            dgResource.Columns[1].Width = (nWidth - 10) / 3;
        //            dgResource.Columns[2].Width = (nWidth - 10) / 3;
        //            dgResource.Columns[3].Width = (nWidth - 10) / 3;

        //            dgResource.ReadOnly = true;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //       MessageBox.Show(" ERROR : Cannot get Record TOS ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); 
        //    }
        //    finally
        //    {
        //        oResource.Dispose();

        //    }
        //}

        //#endregion " TOS & POS Fill Methods "

        #endregion " Fill Methods "
       

#endregion " Fill Methods "

        #region "Search Appointment Book"

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string[] strSearchArray = null;
            try
            {

                _dv = (DataView)dgResource.DataSource;
                dgResource.DataSource = _dv;
                if (_dv == null) return;

                string sFilter = "";

                string strSearch = txtSearch.Text.Trim();

                //strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");

                //Added By Mukesh Patel for instring search  2009-08-29
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                //if (strSearch.Length > 1)
                //{
                //    string str = strSearch.Substring(1).Replace("%", "");
                //    strSearch = strSearch.Substring(0, 1) + str;
                //}
                //Added By Pramod for Splitting the string with commas  2009-05-13
                if (strSearch.Trim() != "")
                {
                    strSearchArray = strSearch.Split(',');
                }

                switch (SelectedView)
                {
                    case "Resource Type": // Resource Type
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sResourceTypeDescription"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sResourceTypeDescription"].ColumnName + " Like '" + strSearch + "%'";


                        }
                        break;
                    case "Appointment Block Type": // Appointment Block Type
                        {

                            //Commented By Pramod For Implementing Broad Search  2009-05-13
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sAppointmentBlockType"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sAppointmentBlockType"].ColumnName + " Like '" + strSearch + "%'";

                            //Added By Pramod Implementing Broad search 2009-05-13
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sAppointmentBlockType"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sAppointmentBlockType"].ColumnName + " Like '" + strSearch + "%' )";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sAppointmentBlockType"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }

                        }
                        break;
                    case "Appointment Type": // Appointment Type
                        //{
                        //int RowIndex = c1AppointmentType.FindRow(strSearch, 1, 1, false, false, false);                        
                        //c1AppointmentType.Row = RowIndex;
                        //}
                        //break;
                        {
                            //Added By Mukesh For Implementing search 2009-08-18                            
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    sFilter += " ( sAppointmentType Like '" + strSearch + "%' OR convert(nchar,nDuration) Like '" + strSearch + "%' )";
                                    Fill_AppointmentTypes(0, sFilter);
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {


                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( sAppointmentType Like '" + strSearch + "%' OR convert(nchar,nDuration) Like '" + strSearch + "%') ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (sAppointmentType Like '" + strSearch + "%' OR convert(nchar,nDuration) Like '" + strSearch + "%' ) ";
                                            }

                                        }
                                    }
                                    Fill_AppointmentTypes(0, sFilter);

                                }
                            }
                            else
                            {
                                Fill_AppointmentTypes(0, "");
                            }
                        }
                        break;
                    case "Appointment Status": // Appointment Status
                        {
                            ////Commented By Pramod For Implementing Broad Search  2009-05-13
                            ////if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            ////_dv.RowFilter = _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%'";
                            ////else
                            ////_dv.RowFilter = _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%'";

                            ////Added By Pramod For Implementing Broad search 2009-05-13
                            //if (strSearch.Trim() != "")
                            //{
                            //    if (strSearchArray.Length == 1)
                            //    {
                            //        //For Single value search 
                            //        strSearch = strSearchArray[0];
                            //        strSearch = strSearch.Trim();
                            //        _dv.RowFilter = _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%'";
                            //    }
                            //    else
                            //    {
                            //        //For Comma separated  value search
                            //        for (int i = 0; i < strSearchArray.Length; i++)
                            //        {
                            //            strSearch = strSearchArray[i];
                            //            strSearch = strSearch.Trim();
                            //            if (strSearch.Trim() != "")
                            //            {
                            //                if (sFilter == "")//if (i == 0)
                            //                {
                            //                    sFilter = " ( " + _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%')";
                            //                }
                            //                else
                            //                {
                            //                    sFilter = sFilter + " AND (" + _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%')";
                            //                }

                            //            }
                            //        }
                            //        _dv.RowFilter = sFilter;

                            //    }
                            //}
                            //else
                            //{
                            //    _dv.RowFilter = "";
                            //}
                            //Added By MaheshB
                            if (strSearch.Trim() != "")
                            {

                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%' OR " +

                                               _dv.Table.Columns["bIsSystem"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {

                                    //For Comma separated  value search
                                    sFilter = "";
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i].Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {
                                            if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                   _dv.Table.Columns["bIsSystem"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                if (sFilter != "")
                                                    sFilter = sFilter + " AND ";


                                                sFilter = sFilter + " ( " + _dv.Table.Columns["sAppointmentStatus"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                   _dv.Table.Columns["bIsSystem"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }


                        }
                        break;
                    case "Department": // Department
                        {
                            //Commented By Pramod For Implementing Broad Search 2009-05-13
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sDepartment"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sDepartment"].ColumnName + " Like '" + strSearch + "%'";


                            //Added By Pramod For Implementing Broad search 2009-05-13
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sDepartment"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {


                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sDepartment"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sDepartment"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                                             _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;

                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;
                    case "Location": // Location
                        {
                            //Commented By Pramod For Implementing Broad Search 2009-05-13
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%'";



                            //Added By Pramod For Implementing Broad search 2009-05-13
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sZIP"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCounty"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sZIP"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sCounty"].ColumnName + " Like '" + strSearch + "%') ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sLocation"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sAddressLine1"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sAddressLine2"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sState"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sZIP"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sCounty"].ColumnName + " Like '" + strSearch + "%') ";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;


                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }

                        }
                        break;
                    case "Patient Relationship": // Patient Relationship
                        {

                        }
                        break;
                    case "Resource": // Resource
                        {
                            //Commented By Pramod For Implementing Broad Search 2009-05-13
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns[_searchcolumn].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns[_searchcolumn].ColumnName + " Like '" + strSearch + "%'";


                            //Added By Pramod For Implementing Broad search 2009-05-13
                            sFilter = "";
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                     _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;

                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;
                    case "Problem Type": // Problem Type
                        {
                            //Commented By Pramod For Implementing Broad Search 2009-05-13
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sAppointmentType"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sAppointmentType"].ColumnName + " Like '" + strSearch + "%'";


                            //Added By Pramod For Implementing Broad search 2009-05-13
                            if (strSearch.Trim() != "")
                            {
                                //For Single value search 
                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sAppointmentType"].ColumnName + " Like '" + strSearch + "%' ";
                                    //+ " OR " + _dv.Table.Columns["nDuration"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {


                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sAppointmentType"].ColumnName + " Like '" + strSearch + "%' ) ";
                                                //+ " OR " +  _dv.Table.Columns["nDuration"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sAppointmentType"].ColumnName + " Like '" + strSearch + "%' ) ";
                                                //+ " OR " + _dv.Table.Columns["nDuration"].ColumnName + " Like '" + strSearch + "%' ) ";                                            
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;
                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }

                        }
                        break;
                    case "Type Of Service": //Type Of Service
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like'" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sDescription"].ColumnName + " Like '" + strSearch + "%'";
                        }
                        break;
                    case "Place Of Service": //Place Of Service
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sPOSName"].ColumnName + " Like'" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sPOSName"].ColumnName + " Like '" + strSearch + "%'";
                        }
                        break;
                    case "Users": // Resource Type
                        {
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["Name"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["Name"].ColumnName + " Like '" + strSearch + "%'";
                        }
                        break;
                    case "Provider Appointment Type Association":
                        {
                            //Commented By Pramod For Implementing Broad Search 2009-05-13
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sname"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sname"].ColumnName + " Like '" + strSearch + "%'";


                            //Added By Pramod For Implementing Broad search  2009-05-13
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sname"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sAppointmentTypeDesc"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {


                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sname"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sAppointmentTypeDesc"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sname"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sAppointmentTypeDesc"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;

                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }

                        }
                        break;
                    case "Follow Up":
                        {
                            //Commented By Pramod For Implementing Broad Search 2009-05-13
                            //if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            //    _dv.RowFilter = _dv.Table.Columns["sFollowUpName"].ColumnName + " Like '" + strSearch + "%'";
                            //else
                            //    _dv.RowFilter = _dv.Table.Columns["sFollowUpName"].ColumnName + " Like '" + strSearch + "%'";


                            //Added By Pramod For Implementing Broad search  2009-05-13
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sFollowUpName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["nDuration"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sCriteria"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sFollowUpName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["nDuration"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sCriteria"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sFollowUpName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["nDuration"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sCriteria"].ColumnName + " Like '" + strSearch + "%' ) ";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;

                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }
                        }
                        break;
                    //case "ClinicalInstruction": // Clinical Instruction
                    //    {

                    //        sFilter = "";
                    //        if (strSearch.Trim() != "")
                    //        {
                    //            if (strSearchArray.Length == 1)
                    //            {
                    //                //For Single value search 
                    //                strSearch = strSearchArray[0];
                    //                strSearch = strSearch.Trim();
                    //                if (strSearch.Length > 1)
                    //                {
                    //                    string str = strSearch.Substring(1).Replace("%", "");
                    //                    strSearch = strSearch.Substring(0, 1) + str;
                    //                }
                    //                _dv.RowFilter = _dv.Table.Columns["Instruction"].ColumnName + " Like '" + strSearch + "%' OR [" + _dv.Table.Columns["Instruction Description"].ColumnName + "] Like '" + strSearch + "%'";
                    //            }
                    //            else
                    //            {
                    //                //For Comma separated  value search
                    //                for (int i = 0; i < strSearchArray.Length; i++)
                    //                {
                    //                    strSearch = strSearchArray[i];
                    //                    strSearch = strSearch.Trim();
                    //                    if (strSearch.Length > 1)
                    //                    {
                    //                        string str = strSearch.Substring(1).Replace("%", "");
                    //                        strSearch = strSearch.Substring(0, 1) + str;
                    //                    }
                    //                    if (strSearch.Trim() != "")
                    //                    {
                    //                        if (sFilter == "")//if (i == 0)
                    //                        {
                    //                            sFilter = " ( " + _dv.Table.Columns["Instruction"].ColumnName + " Like '" + strSearch + "%' OR [" +                                                          
                    //                                      _dv.Table.Columns["Instruction Description"].ColumnName + "] Like '" + strSearch + "%' ) ";
                    //                        }
                    //                        else
                    //                        {
                    //                            sFilter = sFilter + " AND (" + _dv.Table.Columns["Instruction"].ColumnName + " Like '" + strSearch + "%' OR [" +
                    //                                     _dv.Table.Columns["Instruction Description"].ColumnName + "] Like '" + strSearch + "%' ) ";
                    //                        }

                    //                    }
                    //                }
                    //                _dv.RowFilter = sFilter;

                    //            }
                    //        }
                    //        else
                    //        {
                    //            _dv.RowFilter = "";
                    //        }
                    //    }
                    //  break;
                    case "Occupation": // Location
                        {
                            //Added By Mukesh For Implementing Broad search 2009-08-28
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    //For Single value search 
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                    }
                                    _dv.RowFilter = _dv.Table.Columns["sOccupation"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sEmployerName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sPlaceofEmployment"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sWorkCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sWorkPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["sWorkMobile"].ColumnName + " Like '" + strSearch + "%'";
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Length > 1)
                                        {
                                            string str = strSearch.Substring(1).Replace("%", "");
                                            strSearch = strSearch.Substring(0, 1) + str;
                                        }
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( " + _dv.Table.Columns["sOccupation"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sEmployerName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sPlaceofEmployment"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sWorkCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sWorkPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                           _dv.Table.Columns["sWorkMobile"].ColumnName + " Like '" + strSearch + "%')";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + _dv.Table.Columns["sOccupation"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sEmployerName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sPlaceofEmployment"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sWorkCity"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sWorkPhone"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                          _dv.Table.Columns["sWorkMobile"].ColumnName + " Like '" + strSearch + "%')";
                                            }

                                        }
                                    }
                                    _dv.RowFilter = sFilter;


                                }
                            }
                            else
                            {
                                _dv.RowFilter = "";
                            }

                        }
                        break;
                }
                dgResource.DataSource = _dv;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { strSearchArray = null; }
        }

        private void txtSearchAppBook_TextChanged(object sender, EventArgs e)
        {
            string strSearch = txtSearchAppBook.Text.Trim();
            //strSearch = strSearch.Replace("'", "''");
            strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "");
            //strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
            if (strSearch.Length > 1)
            {
                string str = strSearch.Substring(1).Replace("%", "");
                strSearch = strSearch.Substring(0, 1) + str;
            }
            try
            {
                if (trvAppointmentBook.SelectedNode.Level == 0 && trvAppointmentBook.SelectedNode.Nodes.Count != 0)
                {
                    foreach (TreeNode n in trvAppointmentBook.SelectedNode.Nodes)
                    {
                        if (n.Text.ToString().ToUpper().StartsWith(strSearch.ToUpper()))
                        {
                            trvAppointmentBook.SelectedNode = n;
                            break;
                        }
                    }
                }
                if (trvAppointmentBook.SelectedNode.Level == 1 && trvAppointmentBook.SelectedNode.Parent.Nodes.Count != 0)
                {
                    foreach (TreeNode n in trvAppointmentBook.SelectedNode.Parent.Nodes)
                    {
                        if (n.Text.ToString().ToUpper().StartsWith(strSearch.ToUpper()))
                        {
                            trvAppointmentBook.SelectedNode = n;
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            { strSearch = null; }
        }

        #endregion

        #region "Form Control Events"

        private void trvAppointmentBook_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                
                if (trvAppointmentBook.SelectedNode != null)
                {
                    pnlMain.BringToFront();


                    SelectedView = Convert.ToString(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 1));

                    //Clear Template if other node is selected
                    if (SelectedView != "Template" && trvAppointmentBook.SelectedNode.Level != 1)
                    {
                        for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                        {
                            if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template")
                            {
                                trvAppointmentBook.Nodes[i].Nodes.Clear();
                            }
                        }
                    }
                    if (SelectedView != "Template Allocation " && trvAppointmentBook.SelectedNode.Level != 1)
                    {
                        for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                        {
                            if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Template Allocation")
                            {
                                trvAppointmentBook.Nodes[i].Nodes.Clear();
                            }
                        }
                    }

                    if (SelectedView != "Users" && trvAppointmentBook.SelectedNode.Level != 1)
                    {
                        for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                        {
                            if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Users")
                            {
                                trvAppointmentBook.Nodes[i].Nodes.Clear();
                            }
                        }
                    }
                    if (SelectedView != "Follow Up" && trvAppointmentBook.SelectedNode.Level != 1)
                    {
                        for (int i = 0; i < trvAppointmentBook.Nodes.Count; i++)
                        {
                            if (Convert.ToString(trvAppointmentBook.Nodes[i].Tag) == "Follow Up")
                            {
                                trvAppointmentBook.Nodes[i].Nodes.Clear();
                            }
                        }
                    }

                    if ((SelectedView == "Template Allocation") || (SelectedView == "Template"))
                    {
                        pnlMain.Visible = false;
                    }
                    else
                    {
                        pnlMain.Visible = true;
                    }


                    if (SelectedView == "Template Allocation")
                    {
                        tsb_Modify.Visible = false;

                        tsb_ExtendtoTemplates.Visible = true;
                    }
                    else
                    {
                        tsb_Modify.Visible = true;
                        tsb_ExtendtoTemplates.Visible = false;
                    }
                    if (SelectedView == "Users")
                    {
                        tsb_Delete.Visible = false;

                    }
                    else
                    {
                        tsb_Delete.Visible = true;

                    }
                    if (SelectedView == "Template")
                    {
                        pnltrvSerchAppBook.Visible = true;
                        txtSearchAppBook.Visible = true;
                        PicBx_Search.Visible = true;

                    }
                    else
                    {
                        pnltrvSerchAppBook.Visible = false;
                        txtSearchAppBook.Visible = false;
                        PicBx_Search.Visible = false;
                        txtSearchAppBook.Clear();
                    }
                    //if (SelectedView == "Appointment Type")
                    //{
                    //    c1AppointmentType.Visible = true;
                    //}
                    //else
                    //{
                    //}

                    switch (SelectedView)
                    {
                        case "Resource Type": // Resource Type
                            //lblSearch.Text = "Resource Type";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_ResourceTypes(0);
                            break;
                        case "Appointment Block Type": // Appointment Block Type
                            //lblSearch.Text = "Appointment Block Type";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_AppointmentBlockTypes(0);
                            break;
                        //case "ClinicalInstruction": // Appointment Block Type
                        //    //lblSearch.Text = "Appointment Block Type";
                        //    dgResource.Visible = true;
                        //    c1AppointmentType.Visible = false;
                        //    tsb_DayView.Visible = false;
                        //    tsb_WeekView.Visible = false;
                        //    tsb_MonthView.Visible = false;
                        //    Fill_ClinicalInstruction(0);
                        //    break;
                        case "Appointment Type": // Appointment Type
                            //lblSearch.Text = "Appointment Type";
                            dgResource.Visible = false;
                            c1AppointmentType.Visible = true;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_AppointmentTypes(0,"");
                            break;
                        case "Appointment Status": // Appointment Status
                            //lblSearch.Text = "Appointment Status";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_AppointmentStatus(0);
                            break;
                        case "Department": // Department
                            //lblSearch.Text = "Department";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_Department(0);
                            break;
                        case "Location": // Location
                            //lblSearch.Text = "Location";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_Location(0);
                            break;
                        case "Patient Relationship": // Patient Relationship
                            //lblSearch.Text = "Patient Relationship";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_Relationship(0);
                            break;
                        case "Resource": // Resource
                            //lblSearch.Text = "Code";
                            _searchcolumn = "sCode";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_Resource(0);
                            break;
                        case "Problem Type": // procedure
                            //lblSearch.Text = "Problem Type";
                            dgResource.Visible = true;
                            c1AppointmentType.Visible = false;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            Fill_Procedures(0);
                            break;
                        case "Template": // Template
                            pnlCalendarDetail.Visible = false;

                            if (trvAppointmentBook.SelectedNode.Level == 1)
                            {
                                pnlTemplate.BringToFront();
                                Fill_trvTemplates();
                                tsb_DayView.Visible = false;
                                tsb_WeekView.Visible = false;
                                tsb_MonthView.Visible = false;
                            }
                            else
                            {
                                //lblSearch.Text = "Template";
                                pnlTemplate.BringToFront();
                                //Fill_Templates(0);
                                Fill_Template(0);
                                tsb_DayView.Visible = false;

                                tsb_WeekView.Visible = false;
                                tsb_MonthView.Visible = false;
                            }
                            break;
                        case "Template Allocation": // Template Allocation
                            //pnlTemplateAllocation.BringToFront();
                            pnlCalendarDetail.Visible = true;
                            if (trvAppointmentBook.SelectedNode.Level == 1)
                            {
                                pnlTemplate.BringToFront();
                                Fill_trvTemplateAllocation();
                            }
                            else
                            {
                                pnlTemplate.BringToFront();
                                //Fill_TemplatesAllocation(0);
                                Fill_TemplateAllocation(0);
                            }
                            break;
                        case "Users":

                            //lblSearch.Text = "Name";
                            dgResource.Visible = true;
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            c1AppointmentType.Visible = false;

                            if (trvAppointmentBook.SelectedNode.Level == 1)
                            {
                                Fill_Users(Convert.ToString(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2)), _UserID);
                            }
                            else
                            {
                                Fill_trvUsers();
                                trvAppointmentBook.SelectedNode = trvAppointmentBook.SelectedNode.Nodes[2]; //Select "All Users" Node by Default
                                Fill_Users(Convert.ToString(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2)), _UserID);
                            }
                            break;
                        //
                        //case 12:
                        //    lblSearch.Text = "Type Of Service";
                        //    dgResource.Visible = true;
                        //    c1AppointmentType.Visible = false;
                        //    Fill_TOS();
                        //    break;
                        //case 13:
                        //    lblSearch.Text = "Place Of Service";
                        //    dgResource.Visible = true;
                        //    c1AppointmentType.Visible = false;
                        //    Fill_POS();
                        //    break;
                        //
                        case "Provider Appointment Type Association":

                            //lblSearch.Text = "Provider Name";
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            c1AppointmentType.Visible = false;
                            dgResource.Visible = true;
                            Fill_AptmntType_Provider(0);
                            break;

                        case "Follow Up":
                            //lblSearch.Text = "Follow Up ";
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            c1AppointmentType.Visible = false;
                            dgResource.Visible = true;
                            Fill_FollowUps(0);
                            break;

                        case "Occupation":
                            tsb_DayView.Visible = false;
                            tsb_WeekView.Visible = false;
                            tsb_MonthView.Visible = false;
                            c1AppointmentType.Visible = false;
                            dgResource.Visible = true;
                            Fill_Occupation(0);
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                //throw;
            }
        }

        //private void Fill_ClinicalInstruction(Int64 nid)
        //{
            
        //    try
        //    {
        //        ClsClinicalInstruction objClinicalInstruction = new ClsClinicalInstruction();
        //        DataTable _dtMstTable= objClinicalInstruction.GetClinicalInstruction(nid);
               
        //        if (_dtMstTable != null)
        //        {
        //            _dv = _dtMstTable.DefaultView;

        //            dgResource.DataSource = _dv;
        //            dgResource.Columns["Id"].Visible = false;                    
        //            dgResource.ReadOnly = true;
        //            dgResource.Columns["Instruction"].Width = 150;
        //        }


                 
        //            if (dgResource.RowCount > 0)
        //            {
        //                tsb_Modify.Enabled = true;
        //                tsb_Delete.Enabled = true;
        //            }
        //            if (dgResource.RowCount <= 0)
        //            {
        //                tsb_Modify.Enabled = false;
        //                tsb_Delete.Enabled = false;
        //            }


        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {

        //    }
            
        //    //throw new NotImplementedException();
        //}

        private void c1AppointmentType_OwnerDrawCell(object sender, C1.Win.C1FlexGrid.OwnerDrawCellEventArgs e)
        {
            //Books.AppointmentType oAppointmentType = new Books.AppointmentType(_databaseconnectionstring);

            try
            {
                // DataTable dt = new DataTable();
                //dt  =oAppointmentType.GetList(AppointmentProcedureType.AppointmentType);
                //if (dt != null)
                //{
                //    int j = 0;
                //    int k = e.Col;
                //    k++;
                //    for (int i = 0; i <= dt.Rows.Count-1 ; i++)
                //    {
                //        if ((c1AppointmentType.Cols[3].UserData != null) && i == j && k == 2)
                //        {
                //           // double value;

                //            //calculate bar extent 
                //            Rectangle rc = e.Bounds;

                //            rc.Width =  25;

                //            //draw background 
                //            e.DrawCell(DrawCellFlags.Background | DrawCellFlags.Border);

                //            //draw bar 
                //            //object colBackColor;
                //            //System.Drawing.Brush brBackColor;

                //            SolidBrush _bdrBrush;
                //            _bdrBrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(c1AppointmentType.Cols[3].UserData)));


                //            e.Graphics.FillRectangle(_bdrBrush, rc);

                //            //draw cell content 
                //            e.DrawCell(DrawCellFlags.Content);
                //            j++;

                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void dgResource_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Int64 ID = 0;
            //if (SelectedView == "Resource")
            //{
            //    if (e.ColumnIndex == 1)
            //    {
            //        lblSearch.Text = "Code";
            //        _searchcolumn = "sCode";
            //    }
            //    if (e.ColumnIndex == 2)
            //    {
            //        lblSearch.Text = "Resource Name";
            //        _searchcolumn = "sDescription";
            //    }
            //}

            if (e.RowIndex == -1)
            {
                return;
            }
            try
            {
                ID = Convert.ToInt64(dgResource.Rows[e.RowIndex].Cells[0].Value);
                switch (SelectedView)
                {
                    case "Resource Type": // Resource Type
                        {
                            Int64 ResourceTypeID = 0;
                            if (dgResource.Rows[e.RowIndex].Cells[0].Value.ToString() != "" || dgResource.Rows[e.RowIndex].Cells[0].Value.ToString() != "0")
                            {
                                if (dgResource.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                                {
                                    ResourceTypeID = Convert.ToInt64(dgResource.Rows[e.RowIndex].Cells[0].Value.ToString());
                                    if (Convert.ToInt64(dgResource.Rows[e.RowIndex].Cells[0].Value.ToString()) > 2)
                                    {
                                        frmSetupResourceType oResourceType_MST = new frmSetupResourceType(Convert.ToInt64(dgResource.Rows[e.RowIndex].Cells[0].Value.ToString()));
                                        oResourceType_MST.DatabaseConnectionString = _databaseconnectionstring;
                                        oResourceType_MST.ShowDialog(this);
                                        oResourceType_MST.Dispose();
                                        oResourceType_MST = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cannot modify system resource type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Cannot modify this resource type.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            Fill_ResourceTypes(ResourceTypeID);
                        }
                        break;

                    case "Appointment Block Type": // Appointment Block Type
                        frmSetupAppointmentBlockType ofrmBlockType = new frmSetupAppointmentBlockType(ID, _databaseconnectionstring);
                        //Code Added by Mayuri:20091103
                        //To make invisible save button if form gets open for modify
                        ofrmBlockType.tsb_Save.Visible = false;
                        //End Code Added by Mayuri:20091103
                        ofrmBlockType.ShowDialog(this);
                        Fill_AppointmentBlockTypes(ID);
                        ofrmBlockType.Dispose();
                        break;
                    case "Appointment Type": // Appointment Type

                        break;
                    case "Appointment Status": // Appointment Status 
                        //Added By MaheshB
                        if (dgResource.Rows[e.RowIndex].Cells[3].Value.ToString() == "System")
                        {
                            MessageBox.Show("Cannot modify system appointment status.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            frmSetupAppointmentStatus ofrmAppStatus = new frmSetupAppointmentStatus(ID, _databaseconnectionstring);
                            //Code Added by Mayuri:20091103
                            //To make invisible button save if form gets open for modify
                            ofrmAppStatus.tsb_Save.Visible = false;
                            //End Code Added by Mayuri:20091103
                            ofrmAppStatus.ShowDialog(this);
                            Fill_AppointmentStatus(ID);
                            ofrmAppStatus.Dispose();
                        }
                        break;
                    case "Department": // Department
                        frmDepartment ofrmDepartment = new frmDepartment(ID, _databaseconnectionstring);
                        //Code Added by Mayuri:20091103
                        //To make invisible button save if form gets open for modify
                        ofrmDepartment.tsb_Save.Visible = false;
                        //End Code Added by Mayuri:20091103
                        ofrmDepartment.ShowDialog(this);
                        Fill_Department(ID);
                        ofrmDepartment.Dispose();
                        ofrmDepartment = null;
                        break;
                    case "Location": // Location 
                        frmSetupLocation ofrmLocation = new frmSetupLocation(ID, _databaseconnectionstring);
                        //Code Added by Mayuri:20091103
                        //To make invisible button save if form gets open for modify
                        ofrmLocation.tlsp_btnSave.Visible = false;
                        //End Code Added by Mayuri:20091103
                        ofrmLocation.ShowDialog(this);
                        Fill_Location(ID);
                        ofrmLocation.Dispose();
                        ofrmLocation = null;
                        break;
                    case "Patient Relationship": // Patient Relationship

                        break;
                    case "Resource": // Resource 
                        frmSetupResource ofrmResource = new frmSetupResource(ID, _databaseconnectionstring);
                        //Code Added by Mayuri:20091103
                        //To make Save button invisible if form gets open for modify
                        ofrmResource.tlsp_btnSave.Visible = false;
                        //End Code Added by Mayuri:20091103
                        ofrmResource.ShowDialog(this);
                        Fill_Resource(ID);
                        ofrmResource.Dispose();
                        ofrmResource = null;
                        break;
                    case "Procedures": // Procedures 
                        frmSetupAppointmentType ofrmProcedure = new frmSetupAppointmentType(ID, _databaseconnectionstring, AppointmentProcedureType.Procedure);
                        ofrmProcedure.ShowDialog(this);
                        Fill_Procedures(ID);
                        ofrmProcedure.Dispose();
                        ofrmProcedure = null;
                        break;
                    //case "ClinicalInstruction": // Procedures 

                    //    frmMstClinicalInstruction oMstClinicalInstruction = new frmMstClinicalInstruction(ID);
                    //    oMstClinicalInstruction.tsb_Save.Visible = false;
                    //    oMstClinicalInstruction.ShowDialog();
                    //    Fill_ClinicalInstruction(0);
                    //    break;
                    case "Problem Type":
                        frmSetupAppointmentType ofrm = new frmSetupAppointmentType(ID, _databaseconnectionstring, AppointmentProcedureType.Procedure);
                        //Code Added by Mayuri:20091103
                        //To make invisible button save if form gets open for modify
                        ofrm.tsb_Save.Visible = false;
                        //End Code Added by Mayuri:20091103
                        ofrm.ShowDialog(this);
                        Fill_Procedures(ID);
                        ofrm.Dispose();
                        ofrm = null;
                        break;
                    case "Provider Appointment Type Association":
                        Int64 ProviderId = 0;
                        if (dgResource.SelectedRows.Count > 0)
                        {
                            if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                            {
                                ProviderId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                                frmProvider_AppointmentType_Association frmsetAssociation = new frmProvider_AppointmentType_Association(_databaseconnectionstring);
                                frmsetAssociation = new frmProvider_AppointmentType_Association(ProviderId, _databaseconnectionstring);
                                //Code Added by Mayuri:20091103
                                //To make invisible button save if form gets open for modify
                                frmsetAssociation.tsb_Save.Visible = false;
                                //End Code Added by Mayuri:20091103
                                frmsetAssociation.ShowDialog(this);
                                Fill_AptmntType_Provider(ProviderId);
                                frmsetAssociation.Dispose();
                                frmsetAssociation = null;
                            }
                            else
                            {
                                MessageBox.Show(" ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;


                    case "Follow Up": // follow up 
                        Int64 FollowUpId = 0;
                        if (dgResource.SelectedRows.Count > 0)
                        {
                            if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                            {
                                FollowUpId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                                frmSetupFollowup ofrmSetupFollowup = new frmSetupFollowup(_databaseconnectionstring, FollowUpId);
                                //Code Added by Mayuri:20091103
                                //To make invisible button save if form gets open for modify
                                ofrmSetupFollowup.tsb_Save.Visible = false;
                                //End Code Added by Mayuri:20091103
                                ofrmSetupFollowup.ShowDialog(this);
                                Fill_FollowUps(0);
                                ofrmSetupFollowup.Dispose();
                                ofrmSetupFollowup = null;
                            }
                            else
                            {
                                MessageBox.Show("Cannot modify this follow up.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;

                    case "Occupation": // Location 
                        Int64 OccupationId = 0;
                        if (dgResource.SelectedRows.Count > 0)
                        {
                            if (dgResource.SelectedRows[0].Cells[0].Value.ToString() != "" || dgResource.SelectedRows[0].Cells[0].Value.ToString() != "0")
                            {
                                OccupationId = Convert.ToInt64(dgResource.SelectedRows[0].Cells[0].Value);
                                frmSetupOccupation ofrmSetupOccupation = new frmSetupOccupation(OccupationId, _databaseconnectionstring);
                                ofrmSetupOccupation.showemployertextbox = true;
                                ofrmSetupOccupation.ShowDialog(this);
                                Fill_Occupation(0);
                                ofrmSetupOccupation.Dispose();
                                ofrmSetupOccupation = null;
                            }
                            else
                            {
                                MessageBox.Show("Cannot modify this follow up.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;

                    //case 12 : //Type Of Service
                    //    frmSetupTOS ofrmSetupTOS = new frmSetupTOS(_databaseconnectionstring, ID);
                    //    ofrmSetupTOS.ShowDialog(this);
                    //    Fill_TOS();
                    //    break;
                    //case 13 : //Place Of Service
                    //    frmSetupPOS ofrmSetupPOS = new frmSetupPOS(_databaseconnectionstring, ID);
                    //    ofrmSetupPOS.ShowDialog(this);
                    //    Fill_POS();
                    //    break;
                    //default:
                    //    break;
                }
                // Code added to clear the reset search text 
                txtSearch.TextChanged -= new EventHandler(txtSearch_TextChanged);
                txtSearch.Text = string.Empty;
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1AppointmentType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (c1AppointmentType.Row == -1)
                {
                    return;
                }
                Int64 ID = Convert.ToInt64(c1AppointmentType.GetData(c1AppointmentType.RowSel, 0));
                frmSetupAppointmentType ofrm = new frmSetupAppointmentType(ID, _databaseconnectionstring, AppointmentProcedureType.AppointmentType);
                //Code Added by Mayuri:20091103
                //To make invisible button save if form gets open for modify
                ofrm.tsb_Save.Visible = false; 
                //End Code Added by Mayuri:20091103
                ofrm.ShowDialog(this);
                Fill_AppointmentTypes(ID,"");
                ofrm.Dispose();
                ofrm = null;
                // Code added to clear the reset search text 
                txtSearch.TextChanged -= new EventHandler(txtSearch_TextChanged);
                txtSearch.Text = string.Empty;
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Set the default location
        private void dgResource_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SelectedView == "Location") //Location
            {
                //if (e.ColumnIndex == 3)
                if (e.ColumnIndex == 10)
                {
                    dgResource.EndEdit();
                    Books.Location oLocation = new Books.Location();

                    oLocation.LocationID = Convert.ToInt64(dgResource.Rows[e.RowIndex].Cells[0].Value);
                    oLocation.LocationName = Convert.ToString(dgResource.Rows[e.RowIndex].Cells[1].Value);
                    oLocation.IsBlocked = false;
                    oLocation.IsDefault = Convert.ToBoolean(dgResource.Rows[e.RowIndex].Cells[3].Value);

                    //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                    //oLocation.ClinicID = 1;
                    oLocation.ClinicID = ClinicID;
                    //

                    if (oLocation.Modify() == false)
                    {
                        MessageBox.Show("Location not modified, please try again.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Fill_Location(Convert.ToInt64(dgResource.Rows[e.RowIndex].Cells[0].Value));
                    if (oLocation != null) { oLocation.Dispose(); oLocation = null; }
                }
            }
        }
        #endregion

        #region "Template Supporting Event & Methods"

        DateTime SelectedStartDate 
        { 
            get { return CalendarTemplate.Dates[0]; }
        }

        DateTime SelectedEndDate 
        {
            get { return CalendarTemplate.Dates[CalendarTemplate.Dates.Count - 1]; }
        }

        //if selected date for template allocation changes
        private void juc_Calendar_SelectionChanged(object sender, EventArgs e)
        {
            string TempTag = Convert.ToString(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 1));
            if (TempTag == "Template Allocation" && trvAppointmentBook.SelectedNode.Level == 1)
            {
                Fill_trvTemplateAllocation();
            }
            TempTag = null;
        }

        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            string[] temp;
            if (TagContent.Contains(Delimeter.ToString()))
            {
                temp = TagContent.Split(Delimeter);
                return temp[Position - 1];
            }
            else
            {
                return TagContent;
            }
        }

        #endregion

        public void CalendarTemplate_MouseClick(object sender, MouseEventArgs e)
        {

            //try
            //{
            //    if (e.Button == MouseButtons.Right)
            //    {
            //        ContextMenuStrip cmnu_strip = new ContextMenuStrip();
            //        //ContextMenu cmnu_template = new ContextMenu();
            //        //MenuItem mnu_Copy = new MenuItem();
            //        //MenuItem mnu_Paste = new MenuItem();
            //        //mnu_Copy.Text = "Copy";
            //        //mnu_Paste.Text = "Paste";
            //        //cmnu_template.MenuItems.Add(mnu_Copy);
            //        //cmnu_template.MenuItems.Add(mnu_Paste);
            //        //mnu_Copy.Click+=new EventHandler(mnu_Copy_Click);
            //        //mnu_Paste.Click+=new EventHandler(mnu_Paste_Click);
            //        //CalendarTemplate.ContextMenu = cmnu_template;
            //        //Point oPoint = new Point(e.X, e.Y);
            //        //CalendarTemplate.SelectedAppointments.Add(CalendarTemplate.GetAppointmentAt(oPoint));
            //        //CalendarTemplate.CurrentAppointment = CalendarTemplate.GetAppointmentAt(oPoint);

            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{

            //}


        }

        private void c1AppointmentType_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            c1AppointmentType.Row = e.Row;
        }

        private void c1AppointmentType_AfterSort(object sender, SortColEventArgs e)
        {
            c1AppointmentType.Row = -1;
        }

        private void c1AppointmentType_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1AppointmentType_DoubleClick_1(object sender, EventArgs e)
        {
            try
            {
                if (c1AppointmentType.Row == -1)
                {
                    return;
                }
                Int64 ID = Convert.ToInt64(c1AppointmentType.GetData(c1AppointmentType.RowSel, 0));
                frmSetupAppointmentType ofrm = new frmSetupAppointmentType(ID, _databaseconnectionstring, AppointmentProcedureType.AppointmentType);
                //Code Added by Mayuri:20091103
                //To make invisible button save if form gets open for modify
                ofrm.tsb_Save.Visible = false;
                //End Code Added by Mayuri:20091103
                ofrm.ShowDialog(this);
                Fill_AppointmentTypes(ID, "");
                ofrm.Dispose();
                ofrm = null;
                // Code added to clear the reset search text 
                txtSearch.TextChanged -= new EventHandler(txtSearch_TextChanged);
                txtSearch.Text = string.Empty;
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ts_gloCommunityDownload_Click(object sender, EventArgs e)
        {
            //Code Start added by kanchan on 20120102 for gloCommunity integration
            if (EvntgloCommunityHandler != null)
                EvntgloCommunityHandler();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();
        }

       

    }
}