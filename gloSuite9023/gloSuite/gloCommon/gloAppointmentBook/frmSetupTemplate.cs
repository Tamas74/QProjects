using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAppointmentBook.Books;
using gloAppointmentBook;
using gloAuditTrail;

namespace gloAppointmentBook
{
    public partial class frmSetupTemplate : Form
    {

        #region " Declarations "

        private string _MessageBoxCaption = string.Empty;
        private string _databaseConnectionString = "";
        private Int64 _templateID = 0;
        private Int32 _rowIndex = 0;
        private bool IsEditing = false;
        //private bool _flag = false;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public enum FormAction
        { 
            New,
            Edit,
            Delete
        }

        public FormAction ActionToPerform { get; set; }
 
        #endregion  " Property Procedures "

        #region " Constructor "

        public frmSetupTemplate(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
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

        public frmSetupTemplate(Int64 TemplateID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _templateID = TemplateID;
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

        private void frmSetupTemplate_Load(object sender, EventArgs e)
        {


            gloC1FlexStyle.Style(c1Template, false);

            if (_templateID != 0)
            {
                pnlTemplateInfo.Enabled = false;
                pnlSearch.Enabled = true;
                tsb_Add.Visible = true;
                tsb_Close.Visible = true;
                tsb_Save.Visible = true;
            }
            else
            {
                pnlTemplateInfo.Enabled = false;
                //pnlSearch.Enabled = false;
            }
            DesignGrid();
            FillAppointmentTypes();
            FillLocations();
            FillDepartments();

            if (_templateID != 0)
            {
                fillTemplate();
            }

            SetupControls(ActionToPerform);
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

         private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save_Close":

                        if (SaveTemplate() == true)
                        {
                            this.Close();
                        }
                        break;
                    case "Save":

                        if (SaveTemplate() == true)
                        {
                            //this.Close();
                            txtTemplateName.Text = "";
                            cmbAppointmentType.SelectedIndex = -1;
                            cmbDepartment.SelectedIndex = -1;
                            cmbLocation.SelectedIndex = -1;
                            //cmb.SelectedIndex = -1;
                            c1Template.Rows.RemoveRange(1, c1Template.Rows.Count-1);
                        }
                        break;
                    case "Close":
                        if (c1Template.Rows.Count > 1)
                        {
                            //DialogResult res = MessageBox.Show(" Do you want to close ? ", _messageBoxCaption, MessageBoxButtons.YesNo);

                            //if (res == DialogResult.Yes)
                            //{
                            this.Close();
                            //}

                        }
                        else
                        {

                            this.Close();
                        }

                        break;
                    case "Add":
                        SetupControls(FormAction.New);
                        break;
                    case "Remove":
                        //Condition Added by Mayuri:20091229-to fix issue:#4915-
                        //Edit> Appointment configuration>System throws an exception on clicking ‘remove’ button after saving appointment template.

                        if (c1Template.Rows.Count > 1)
                        {
                            btnRemove_Click(null, null);
                            tsb_Remove.Visible = true;
                        }
                        else
                        {
                            tsb_Remove.Visible = false;

                        }
                        break;
                    case "Update":
                        btnSave_Click(null, null);
                        pnlTemplateInfo.Enabled = false;
                        tsb_Update.Visible = false;
                        tsb_Cancel.Visible = false;
                        tsb_Save.Visible = true;
                        tsb_SaveAs.Visible = true;
                        tsb_Close.Visible = true;
                        tsb_Add.Visible = true;
                        tsb_UpdateAdd.Visible = false;
                        tlsp_btnSave.Visible = true;
                        if (c1Template.Rows.Count > 1)
                        {
                            tsb_Remove.Visible = true;
                        }
                        else
                        {
                            tsb_Remove.Visible = false;

                        }
                        break;
                    case "Update & Add":
                        btnSave_Click(null, null);
                        //pnlTemplateInfo.Enabled = false;
                        tsb_Save.Visible = false;
                        tsb_SaveAs.Visible = false;
                        tsb_Close.Visible = false;
                        tsb_Add.Visible = false;
                        if (c1Template.Rows.Count > 1)
                        {
                            tsb_Remove.Visible = true;
                        }
                        else
                        {
                            tsb_Remove.Visible = false;

                        }
                        pnlTemplateInfo.Enabled = true;
                        pnlSearch.Enabled = true;
                        tsb_Update.Visible = true;
                        tsb_Cancel.Visible = true;
                        tsb_UpdateAdd.Visible = true;
                        ClearControls();

                        if (c1Template.Rows.Count > 1)
                        {
                            dtpStartTime.Value = Convert.ToDateTime(c1Template.GetData(c1Template.Rows.Count - 1, 4));

                        }

                        break;
                    case "Cancel":
                        pnlTemplateInfo.Enabled = false;
                        //pnlSearch.Enabled = false;
                        tsb_Update.Visible = false;
                        tsb_Cancel.Visible = false;
                        tsb_Remove.Visible = false;
                        tsb_Save.Visible = true;
                        tsb_SaveAs.Visible = true;
                        tsb_Close.Visible = true;
                        tsb_Add.Visible = true;
                        tsb_UpdateAdd.Visible = false;
                        tlsp_btnSave.Visible = true ;
                        ClearControls();

                        break;
                    case "Save As":

                        _templateID = 0;
                        txtTemplateName.Focus();
                        if (SaveTemplate() == true)
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

         private void SetupControls(FormAction action)
         {
             if (action == FormAction.New)
             {
                 pnlTemplateInfo.Enabled = true;
                 pnlSearch.Enabled = true;
                 tsb_Update.Visible = true;
                 tsb_Cancel.Visible = true;
                 tsb_Remove.Visible = false;
                 tsb_Add.Visible = false;
                 tsb_Close.Visible = false;
                 tsb_Save.Visible = false;
                 tsb_SaveAs.Visible = false;
                 tsb_UpdateAdd.Visible = true;
                 tlsp_btnSave.Visible = false;
                 ClearControls();

                 if (c1Template.Rows.Count > 1)
                 {
                     dtpStartTime.Value = Convert.ToDateTime(c1Template.GetData(c1Template.Rows.Count - 1, 4));
                     tsb_Remove.Visible = true;
                 }
             }
         }

        #endregion " Tool Strip Event "

        #region " Private & Public Methods "

        private void FillDepartments()
        {
            Books.Department oDepartments = new Books.Department(_databaseConnectionString);
            try
            {
                DataTable dt = null;
                dt = oDepartments.GetList();
                DataRow r;
                r = dt.NewRow();
                r["nDepartmentID"] = 0;
                r["sDepartment"] = "";
                dt.Rows.InsertAt(r, 0);
                if (dt != null)
                {
                    cmbDepartment.DataSource = dt.Copy();
                    cmbDepartment.DisplayMember = "sDepartment";
                    cmbDepartment.ValueMember = "nDepartmentID";
                    cmbDepartment.Refresh();

                    if (dt.Rows.Count > 0)
                        //cmbDepartment.SelectedIndex = -1;
                        cmbDepartment.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDepartments.Dispose();
            }
        }

        private void FillLocations()
        {
            Books.Location oLocation = new Books.Location();
            try
            {
                DataTable dt = null;
                dt = oLocation.GetList();
                DataRow r;
                r = dt.NewRow();
                r["nLocationID"] = 0;
                r["sLocation"] = "<All Locations>";
                dt.Rows.InsertAt(r, 0);
                if (dt != null)
                {
                    cmbLocation.DataSource = dt.Copy();
                    cmbLocation.DisplayMember = "sLocation";
                    cmbLocation.ValueMember = "nLocationID";
                    cmbLocation.Refresh();

                    if (dt.Rows.Count > 0)
                        cmbLocation.SelectedIndex = -1;
                }
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

        private void FillAppointmentTypes()
        {
            AppointmentType oAppointmentType = new AppointmentType(_databaseConnectionString);
            try
            {
                DataTable dtAppointmentType = oAppointmentType.GetList(AppointmentProcedureType.AppointmentType);
                if (dtAppointmentType != null)
                {
                    cmbAppointmentType.DataSource = dtAppointmentType;
                    cmbAppointmentType.DisplayMember = dtAppointmentType.Columns["sAppointmentType"].ColumnName;
                    cmbAppointmentType.ValueMember = dtAppointmentType.Columns["nAppointmentTypeID"].ColumnName;
                }
                cmbAppointmentType.Refresh();
                cmbAppointmentType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAppointmentType != null) { oAppointmentType.Dispose(); oAppointmentType = null; }
            }
        }

        private void fillTemplate()
        {

            //if (_templateID != 0)
            //{
            //    tsb_Save.Visible = false;
            //    tsb_Close.Visible = false;
            //    tsb_Add.Visible = false;
            //    if (c1Template.Rows.Count > 1)
            //    {
            //        tsb_Remove.Visible = true;
            //    }
            //    else
            //    {
            //        tsb_Remove.Visible = false;

            //    }
            //    tsb_Update.Visible = true;
            //    tsb_Cancel.Visible = true;
            //    tsb_UpdateAdd.Visible = true;
            //}
            AppointmentType oAppointmentType = new AppointmentType(_databaseConnectionString);
            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate(_databaseConnectionString);
            AppointmentTemplate oTemplate = null;
            try
            {
                DataTable dtAppointmentType = oAppointmentType.GetList(AppointmentProcedureType.AppointmentType);

                oTemplate = ogloAppointmentTemplate.GetTemplate(_templateID);
                if (oTemplate != null)
                {
                    txtTemplateName.Text = oTemplate.TemplateName;

                    for (int i = 0; i < oTemplate.TemplateDetails.Count; i++)
                    {
                        c1Template.Rows.Add();
                        Int32 Index = c1Template.Rows.Count - 1;

                        c1Template.SetData(Index, 0, Convert.ToString(oTemplate.TemplateDetails[i].AppointmentTypeID));
                        c1Template.SetData(Index, 1, Convert.ToString(oTemplate.TemplateDetails[i].TemplateLineNo));
                        c1Template.SetData(Index, 2, oTemplate.TemplateDetails[i].AppointmentTypeDesc);
                        c1Template.SetData(Index, 3, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, oTemplate.TemplateDetails[i].StartTime).ToShortTimeString());
                        c1Template.SetData(Index, 4, gloDateMaster.gloTime.TimeAsDateTime(DateTime.Now, oTemplate.TemplateDetails[i].EndTime).ToShortTimeString());
                        c1Template.SetData(Index, 6, oTemplate.TemplateDetails[i].LocationID);
                        c1Template.SetData(Index, 7, oTemplate.TemplateDetails[i].LocationName);
                        c1Template.SetData(Index, 8, oTemplate.TemplateDetails[i].DepartmentID);
                        c1Template.SetData(Index, 9, oTemplate.TemplateDetails[i].DepartmentName);
                        CellRange crng = c1Template.GetCellRange(Index, 5);
                        c1Template.SetData(Index, 10, oTemplate.TemplateDetails[i].AppointmentTypeCode);
                        CellStyle csAppointmentColor;
                        // csAppointmentColor = c1Template.Styles.Add("ColorOF" + Index + 5);
                        string myString = "ColorOF" + Index + 5;
                        try
                        {
                            if (c1Template.Styles.Contains(myString))
                            {
                                csAppointmentColor = c1Template.Styles[myString];
                            }
                            else
                            {
                                csAppointmentColor = c1Template.Styles.Add(myString);

                            }

                        }
                        catch
                        {
                            csAppointmentColor = c1Template.Styles.Add(myString);

                        }
                        csAppointmentColor.BackColor = Color.FromArgb(oTemplate.TemplateDetails[i].ColorCode);
                        crng.Style = csAppointmentColor;
                    }


                    #region Commented Code 20080320
                    //for (int i = 1; i < c1Template.Rows.Count; i++)
                    //{
                    //    for (int j = 0; j < oTemplate.TemplateDetails.Count; j++)
                    //    {
                    //        Int64 TempAppointmentTypeID = Convert.ToInt64(c1Template.GetData(i,0));
                    //        if (TempAppointmentTypeID == oTemplate.TemplateDetails[j].AppointmentTypeID)
                    //        {

                    //            c1Template.SetData(i, 1,true);    
                    //            c1Template.SetData(i, 3, oTemplate.TemplateDetails[j].StartTime.ToString("00:00"));
                    //            c1Template.SetData(i, 4, oTemplate.TemplateDetails[j].EndTime.ToString("00:00"));

                    //            CellRange crng = c1Template.GetCellRange(i, 5);
                    //            CellStyle csAppointmentColor;
                    //            csAppointmentColor = c1Template.Styles.Add("ColorOF" + i + 5);
                    //            csAppointmentColor.BackColor = Color.FromArgb(oTemplate.TemplateDetails[j].ColorCode);
                    //            crng.Style = csAppointmentColor;    
                    //        }
                    //    }
                    //} 
                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oAppointmentType != null) { oAppointmentType.Dispose(); oAppointmentType = null; }
                if (ogloAppointmentTemplate != null) { ogloAppointmentTemplate.Dispose(); ogloAppointmentTemplate = null; }
                if (oTemplate != null) { oTemplate.Dispose(); oTemplate = null; }
            }
        }

        private void ClearControls()
        {
            cmbAppointmentType.SelectedIndex = -1;
            txtColor.BackColor = Color.White;
            numDuration.Value = 0;
            numericdurationHrs.Value = 0;
            //cmbDepartment.SelectedIndex = -1;
            //cmbLocation.SelectedIndex = -1;
            numOccurrences.Value = 1;

        }

        private bool SaveTemplate()
        {
            bool Result = false;
            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
            AppointmentTemplate oTemplate = new AppointmentTemplate();
            AppointmentTemplateLine oAppType = null;
            try
            {
                if (ValidateData() == true)
                {
                    oTemplate.TemplateID = _templateID;
                    oTemplate.TemplateName = txtTemplateName.Text.Trim();
                    for (int i = 1; i < c1Template.Rows.Count; i++)
                    {
                        //Appointment type selected (Checked)
                        //if (Convert.ToBoolean(c1Template.GetData(i, 1)) == true)
                        //{
                        oAppType = new AppointmentTemplateLine();
                        oAppType.AppointmentTypeID = Convert.ToInt64(c1Template.GetData(i, 0));
                        oAppType.AppointmentTypeCode = Convert.ToString(c1Template.GetData(i, 10));
                        oAppType.AppointmentTypeDesc = Convert.ToString(c1Template.GetData(i, 2));
                        if (Convert.ToString((c1Template.GetData(i, 1))).Trim() != "")
                        {
                            oAppType.TemplateLineNo = Convert.ToInt64(c1Template.GetData(i, 1));
                        }
                        else
                        {
                            oAppType.TemplateLineNo = 0;
                        }


                        // oAppType.EndTime = Convert.ToInt32(Convert.ToString(c1Template.GetData(i, 4)).Replace(":", ""));
                        oAppType.StartTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToString((c1Template.GetData(i, 3))));
                        oAppType.EndTime = gloDateMaster.gloTime.TimeAsNumber(Convert.ToString((c1Template.GetData(i, 4))));
                        oAppType.ColorCode = c1Template.GetCellRange(i, 5).Style.BackColor.ToArgb();
                        oAppType.LocationID = Convert.ToInt64(c1Template.GetData(i, 6));
                        oAppType.LocationName = Convert.ToString(c1Template.GetData(i, 7));
                        oAppType.DepartmentID = Convert.ToInt64(c1Template.GetData(i, 8));
                        oAppType.DepartmentName = Convert.ToString(c1Template.GetData(i, 9));
                        oTemplate.TemplateDetails.Add(oAppType);
                        // }
                        oAppType = null;
                    }
                    Int64 ID = ogloTemplate.Add(oTemplate);
                    if (ID > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Template, ActivityType.Add, "Add Template", 0, ID, 0, ActivityOutCome.Success);

                        Result = true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Template, ActivityType.Add, "Add Template", 0, ID, 0, ActivityOutCome.Failure);

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
                if (oTemplate != null) { oTemplate.Dispose(); oTemplate = null; }
                if (oAppType != null) { oAppType.Dispose(); oAppType = null; }
            }
            return Result;
        }

        private bool ValidateData()
        {
            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate();
            bool result = ogloTemplate.IsTemplateNamePresent(_templateID, txtTemplateName.Text.ToString());


            if (result == true)
            {
                MessageBox.Show("Template name already exists.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTemplateName.Focus();
                return false;
            }

            if (txtTemplateName.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Template name.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTemplateName.Focus();
                return false;
            }
            if (c1Template.Rows.Count <= 1)
            {
                MessageBox.Show("Select an Appointment slot to save as a Template.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
            return true;
        }

        private void DesignGrid()
        {

            try
            {
                // c1Template.DataSource = dt;
                c1Template.Clear(ClearFlags.All);
                c1Template.Cols.Count = 11;
                c1Template.Rows.Count = 1;

                c1Template.SetData(0, 0, "nAppointmentTypeID");
                c1Template.SetData(0, 1, "nTemplateLineNo");
                c1Template.SetData(0, 2, "Appointment Type");
                c1Template.SetData(0, 3, "Start Time");
                c1Template.SetData(0, 4, "End Time");
                c1Template.SetData(0, 5, "Color");
                c1Template.SetData(0, 6, "Location ID");
                c1Template.SetData(0, 7, "Location");
                c1Template.SetData(0, 8, "Department ID");
                c1Template.SetData(0, 9, "Department");
                c1Template.SetData(0, 10, "Appointment Type Code");


                c1Template.Cols[0].Visible = false;
                c1Template.Cols[1].Visible = false;
                c1Template.Cols[2].Visible = true;
                c1Template.Cols[3].Visible = true;
                c1Template.Cols[4].Visible = true;
                c1Template.Cols[5].Visible = true;
                c1Template.Cols[6].Visible = false;
                c1Template.Cols[7].Visible = true;
                c1Template.Cols[8].Visible = false;
                c1Template.Cols[9].Visible = true;
                c1Template.Cols[10].Visible = false;

                int nWidth = pnlMain.Width;
                c1Template.Cols[0].Width = 0;
                c1Template.Cols[1].Width = (int)(0.05 * (nWidth));
                c1Template.Cols[2].Width = (int)(0.25 * (nWidth));
                c1Template.Cols[3].Width = (int)(0.10 * (nWidth));
                c1Template.Cols[4].Width = (int)(0.10 * (nWidth));
                c1Template.Cols[5].Width = (int)(0.10 * (nWidth));
                c1Template.Cols[6].Width = 0;
                c1Template.Cols[7].Width = (int)(0.18 * (nWidth));
                c1Template.Cols[8].Width = 0;
                c1Template.Cols[9].Width = (int)(0.18 * (nWidth));
                c1Template.Cols[10].Width = 0;

                c1Template.Cols[3].EditMask = "00:00";
                c1Template.Cols[4].EditMask = "00:00";

                //c1Template.Cols[5].ComboList = "...";

                //c1Template.Cols[5].AllowEditing = false;
                c1Template.Cols[0].AllowEditing = false;
                c1Template.Cols[1].AllowEditing = false;
                c1Template.Cols[2].AllowEditing = false;
                c1Template.Cols[3].AllowEditing = false;
                c1Template.Cols[4].AllowEditing = false;
                c1Template.Cols[5].AllowEditing = false;
                c1Template.Cols[6].AllowEditing = false;
                c1Template.Cols[7].AllowEditing = false;
                c1Template.Cols[8].AllowEditing = false;
                c1Template.Cols[9].AllowEditing = false;
                c1Template.Cols[10].AllowEditing = false;
                c1Template.DrawMode = DrawModeEnum.Normal;

                //AppointmentType oAppointmentType = new AppointmentType(_databaseConnectionString);
                //DataTable dtAppointmentType = oAppointmentType.GetList(AppointmentProcedureType.AppointmentType);
                //if (oAppointmentType != null)
                //{
                //    for (int i = 0; i < dtAppointmentType.Rows.Count; i++)
                //    {
                //        if (c1Template.Rows.Count - 1 <= i)
                //        {
                //            c1Template.Rows.Add();
                //        }

                //        c1Template.SetData(i + 1, 0, Convert.ToInt64(dtAppointmentType.Rows[i]["nAppointmentTypeID"]));
                //        c1Template.SetData(i + 1, 2, Convert.ToString(dtAppointmentType.Rows[i]["sAppointmentType"]));
                //    }
                //}

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion " Private & Public Methods "

        #region " Events "

        //Save new Appointment Type To Grid
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _noOfOccurrences = Convert.ToInt32(numOccurrences.Value);
                for (int i = 0; i < _noOfOccurrences; i++)
                {
                    if (cmbAppointmentType.SelectedIndex != -1)
                    {
                        Int32 Index = 0;
                        if (_rowIndex == 0)
                        {
                            c1Template.Rows.Add();
                            Index = c1Template.Rows.Count - 1;
                        }
                        else
                        {
                            Index = _rowIndex;
                        }

                        c1Template.SetData(Index, 0, Convert.ToString(cmbAppointmentType.SelectedValue));
                        c1Template.SetData(Index, 2, Convert.ToString(cmbAppointmentType.Text));
                        c1Template.SetData(Index, 3, Convert.ToString(dtpStartTime.Value.ToShortTimeString()));
                        c1Template.SetData(Index, 4, Convert.ToString(dtpEndTime.Value.ToShortTimeString()));

                        if (cmbLocation.SelectedIndex != -1)
                        {
                            c1Template.SetData(Index, 6, Convert.ToString(cmbLocation.SelectedValue));
                            c1Template.SetData(Index, 7, Convert.ToString(cmbLocation.Text));
                        }
                        else
                        {
                            c1Template.SetData(Index, 6, 0);
                            c1Template.SetData(Index, 7, "");

                        }

                        if (cmbDepartment.SelectedIndex != -1)
                        {
                            c1Template.SetData(Index, 8, Convert.ToString(cmbDepartment.SelectedValue));
                            c1Template.SetData(Index, 9, Convert.ToString(cmbDepartment.Text));
                        }
                        else
                        {
                            c1Template.SetData(Index, 8, 0);
                            c1Template.SetData(Index, 9, "");

                        }

                        CellRange crng = c1Template.GetCellRange(Index, 5);

                        CellStyle csAppointmentColor;
                       // csAppointmentColor = c1Template.Styles.Add("ColorOF" + cmbAppointmentType.Text + Index + "5");
                        string myString = "ColorOF" + cmbAppointmentType.Text + Index + "5";
                        try
                        {
                            if (c1Template.Styles.Contains(myString))
                            {
                                csAppointmentColor = c1Template.Styles[myString];
                            }
                            else
                            {
                                csAppointmentColor = c1Template.Styles.Add(myString);

                            }

                        }
                        catch
                        {
                            csAppointmentColor = c1Template.Styles.Add(myString);

                        }
                        csAppointmentColor.BackColor = txtColor.BackColor;
                        crng.Style = csAppointmentColor;

                        c1Template.Col = 1;
                        _rowIndex = 0;


                    }
                }
                ClearControls();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Select Row for Modification
        private void c1Template_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (c1Template.Row >= 1)
                {
                    IsEditing = true;
                    _rowIndex = c1Template.Row;
                    cmbAppointmentType.SelectedIndex = cmbAppointmentType.FindStringExact(Convert.ToString(c1Template.GetData(_rowIndex, 2)));
                    dtpStartTime.Value = Convert.ToDateTime(c1Template.GetData(_rowIndex, 3));
                    dtpEndTime.Value = Convert.ToDateTime(c1Template.GetData(_rowIndex, 4));
                    txtColor.BackColor = c1Template.GetCellRange(_rowIndex, 5).Style.BackColor;
                    
                    //Location and Department of the template to modify..
                    cmbLocation.SelectedValue = (object)Convert.ToInt64(c1Template.GetData(_rowIndex, 6));
                    cmbDepartment.SelectedValue = (object)Convert.ToInt64(c1Template.GetData(_rowIndex, 8));
                    //..

                    TimeSpan ts = dtpEndTime.Value.Subtract(dtpStartTime.Value);
                    int _Hrs = 0;
                    int _Minutes = 0;
                    if (ts.Hours < 0)
                    {
                         _Hrs = -(ts.Hours);
                    }
                    else
                    {
                        _Hrs = ts.Hours;
                    }
                    if (ts.Minutes < 0)
                    {
                        _Minutes = -(ts.Minutes);
                    }
                    else
                    {
                        _Minutes = ts.Minutes;
                    }
                    numericdurationHrs.Value = _Hrs;
                    numDuration.Value = _Minutes;
                    //TimeSpan ts = new TimeSpan
                    if (_templateID != 0)
                    {
                        tsb_Save.Visible = false;
                        tsb_Close.Visible = false;
                        tsb_Add.Visible = false;
                        if (c1Template.Rows.Count > 1)
                        {
                            tsb_Remove.Visible = true;
                        }
                        else
                        {
                            tsb_Remove.Visible = false;

                        }
                        pnlTemplateInfo.Enabled = true;
                        pnlSearch.Enabled = true;
                        tsb_Update.Visible = true;
                        tsb_Cancel.Visible = true;
                        tsb_UpdateAdd.Visible = true;



                    }
                }

                IsEditing = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Set Color to Appointment Type
        private void c1Template_CellButtonClick(object sender, RowColEventArgs e)
        {
            try
            {
                if (c1Template.Col == 5 && c1Template.Row != 0)
                {
                    try
                    {
                        dlgColor.CustomColors = gloGlobal.gloCustomColor.customColor;
                    }
                    catch
                    {
                    }
                    if (dlgColor.ShowDialog(this) == DialogResult.OK)
                    {
                        Color SelectedColor = dlgColor.Color;

                        CellRange crng = c1Template.GetCellRange(c1Template.Row, c1Template.Col);

                        CellStyle csAppointmentColor;
                       // csAppointmentColor = c1Template.Styles.Add("ColorOF" + c1Template.Row + c1Template.Col);
                        string mystring = "ColorOF" + c1Template.Row + c1Template.Col;
                        try
                        {
                            if (c1Template.Styles.Contains(mystring))
                            {
                                csAppointmentColor = c1Template.Styles[mystring];
                            }
                            else
                            {
                                csAppointmentColor = c1Template.Styles.Add(mystring);

                            }

                        }
                        catch
                        {
                            csAppointmentColor = c1Template.Styles.Add(mystring);

                        }
                        csAppointmentColor.BackColor = SelectedColor;
                        crng.Style = csAppointmentColor;

                        if (c1Template.Row < c1Template.Rows.Count - 1)
                            c1Template.Row = c1Template.Row + 1;

                        c1Template.Col = 1;
                        try
                        {
                            gloGlobal.gloCustomColor.customColor = dlgColor.CustomColors;
                        }
                        catch
                        {
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbAppointmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtDuration = null;
            AppointmentType oAppType = new AppointmentType(_databaseConnectionString);
            try
            {
                if (cmbAppointmentType.SelectedIndex >= 0 && cmbAppointmentType.ValueMember != "")
                {
                    dtDuration = oAppType.GetAppointmentType(Convert.ToInt64(cmbAppointmentType.SelectedValue));
                    //dtDuration = oAppType.GetAppointmentTypeProcedure(Convert.ToInt64(((System.Data.DataRowView)(cmbAppointmentType.SelectedValue)).Row.ItemArray[cmbAppointmentType.SelectedIndex]));
                    if (dtDuration != null && IsEditing == false)
                    {
                        if (dtDuration.Rows.Count > 0)
                        {

                            TimeSpan ts = new TimeSpan(0, Convert.ToInt32(dtDuration.Rows[0]["nDuration"]), 0);
                            numericdurationHrs.Value = ts.Hours;
                            numDuration.Value = ts.Minutes;

                            //numDuration.Value = Convert.ToInt64(dtDuration.Rows[0]["nDuration"]);
                            txtColor.BackColor = Color.FromArgb(Convert.ToInt32(dtDuration.Rows[0]["sColorCode"].ToString()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtDuration != null) { dtDuration.Dispose(); dtDuration = null; }
                if (oAppType != null) { oAppType.Dispose(); oAppType = null; }
            }
        }

        private void btnBrowseColor_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dlgColor.CustomColors = gloGlobal.gloCustomColor.customColor;
                }
                catch
                {
                }
                if (dlgColor.ShowDialog(this) == DialogResult.OK)
                {
                    txtColor.BackColor = dlgColor.Color;
                    try
                    {
                        gloGlobal.gloCustomColor.customColor = dlgColor.CustomColors;
                    }
                    catch
                    {
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnBrowseColor_MouseHover(object sender, EventArgs e)
        {
            btnBrowseColor.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnBrowseColor.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnBrowseColor_MouseLeave(object sender, EventArgs e)
        {
            btnBrowseColor.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnBrowseColor.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            btnSave.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            btnSave.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseHover(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Yellow;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseLeave(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloAppointmentBook.Properties.Resources.Img_Button;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
        }

        #endregion " Events "

        #region Time Duration Validation

        private void numDuration_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan ts = new TimeSpan((Int32)numericdurationHrs.Value, (Int32)numDuration.Value, 0);
                if (IsEditing == false)
                    dtpEndTime.Value = dtpStartTime.Value.AddMinutes(ts.TotalMinutes);
                //dtpEndTime.Value = dtpStartTime.Value.AddMinutes(Convert.ToInt32(numDuration.Value));
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan ts = new TimeSpan((Int32)numericdurationHrs.Value, (Int32)numDuration.Value, 0);
                // dtpEndTime.Value = dtpStartTime.Value.AddMinutes(Convert.ToInt32(numDuration.Value));
                if (IsEditing == false)
                    dtpEndTime.Value = dtpStartTime.Value.AddMinutes(ts.TotalMinutes);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void numericdurationHrs_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TimeSpan ts = new TimeSpan((Int32)numericdurationHrs.Value, (Int32)numDuration.Value, 0);
                if (IsEditing == false)
                    dtpEndTime.Value = dtpStartTime.Value.AddMinutes(ts.TotalMinutes);
                //dtpEndTime.Value = dtpStartTime.Value.AddMinutes(Convert.ToInt32(numDuration.Value));
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region Delete Row

        private void c1Template_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (c1Template.HitTest(e.X, e.Y).Row > 0)
                {
                    c1Template.Row = c1Template.HitTest(e.X, e.Y).Row;
                    if (e.Button == MouseButtons.Right)
                    {
                        c1Template.ContextMenuStrip = cmnuDelete;
                    }
                    else
                    {
                        c1Template.ContextMenuStrip = null;

                    }
                }
                else
                {
                    c1Template.ContextMenuStrip = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmnu_deleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                c1Template.Rows.Remove(c1Template.Row);
                _rowIndex = 0;
                ClearControls();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (_rowIndex > 0)
                {
                    c1Template.Rows.Remove(_rowIndex);
                    ClearControls();
                }
                else
                {
                    c1Template.Rows.Remove(c1Template.Row);
                }
                _rowIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        private void c1Template_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void txtTemplateName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }

        
    }
}