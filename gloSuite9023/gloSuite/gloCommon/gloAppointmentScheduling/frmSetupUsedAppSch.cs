using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace gloAppointmentScheduling
{
    public partial class frmSetupUsedAppSch : Form
    {
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public bool oUsedAppTrue_SchFalse = true;
        public gloAppointmentScheduling.AppointmentSchedules oUsedList = new AppointmentSchedules();
        public ASUpdateCriteria oDialogResult = ASUpdateCriteria.None;
        public ArrayList oDialogDontDeleteIDsList = new ArrayList();

        private const int COL_DetailID = 0;
        private const int COL_ASBaseID = 0;
        private const int COL_ASBaseCode = 1;
        private const int COL_ASBaseDescription = 2;
        private const int COL_ASBaseFlag = 3;
        private const int COL_StartDate = 4;
        private const int COL_StartTime = 5;
        private const int COL_EndDate = 6;
        private const int COL_EndTime = 7;
        private const int COL_ColorCode = 8;
        private const int COL_LocationID = 9;
        private const int COL_LocationName = 10;
        private const int COL_DepartmentID = 11;
        private const int COL_DepartmentName = 12;
        private const int COL_Notes = 13;
        private const int COL_Count = 14;

        public frmSetupUsedAppSch()
        {
            InitializeComponent();

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

        private void frmSetupUsedAppSch_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1UsedList, false);
            
            if (oUsedAppTrue_SchFalse == true)
            {
                FillUsedAppointments();
                this.Text = "Used Appointments";
                lblCriteria_ProviderProblemType_Header.Text = "Used Appointments";
            }
            else
            {
                FillUsedSchedules();
                this.Text = "Used Schedules";
                lblCriteria_ProviderProblemType_Header.Text = "Used Schedules";
            }
        }

        private void FillUsedAppointments()
        {
            try
            {
                #region "Design Grid"
                    c1UsedList.Rows.Count=1;
                    c1UsedList.Rows.Fixed = 1;
                    c1UsedList.Cols.Count = COL_Count;
                    c1UsedList.Cols.Fixed = 0;


                    c1UsedList.SetData(0, COL_DetailID, "DetailID");
                    c1UsedList.SetData(0,COL_ASBaseID,"ID");
                    c1UsedList.SetData(0,COL_ASBaseCode,"Code");
                    c1UsedList.SetData(0,COL_ASBaseDescription,"Provider");
                    c1UsedList.SetData(0,COL_ASBaseFlag,"Flag");
                    c1UsedList.SetData(0,COL_StartDate,"Start Date");
                    c1UsedList.SetData(0,COL_StartTime,"Time");
                    c1UsedList.SetData(0,COL_EndDate,"End Date");
                    c1UsedList.SetData(0,COL_EndTime,"Time");
                    c1UsedList.SetData(0,COL_ColorCode,"Color");
                    c1UsedList.SetData(0,COL_LocationID,"LocationID");
                    c1UsedList.SetData(0,COL_LocationName,"Location");
                    c1UsedList.SetData(0,COL_DepartmentID,"DepartmentID");
                    c1UsedList.SetData(0,COL_DepartmentName,"Department");
                    c1UsedList.SetData(0,COL_Notes,"Notes");

                    c1UsedList.Cols[COL_DetailID].Visible = false;
                    c1UsedList.Cols[COL_ASBaseID].Visible = false;
                    c1UsedList.Cols[COL_ASBaseCode].Visible = false;
                    c1UsedList.Cols[COL_ASBaseDescription].Visible = false;
                    c1UsedList.Cols[COL_ASBaseFlag].Visible = false;
                    c1UsedList.Cols[COL_StartDate].Visible = true;
                    c1UsedList.Cols[COL_StartTime].Visible = true;
                    c1UsedList.Cols[COL_EndDate].Visible = true;
                    c1UsedList.Cols[COL_EndTime].Visible = true;
                    c1UsedList.Cols[COL_ColorCode].Visible = false;
                    c1UsedList.Cols[COL_LocationID].Visible = false;
                    c1UsedList.Cols[COL_LocationName].Visible = true;
                    c1UsedList.Cols[COL_DepartmentID].Visible = false;
                    c1UsedList.Cols[COL_DepartmentName].Visible = true;
                    c1UsedList.Cols[COL_Notes].Visible = true;

                    c1UsedList.Cols[COL_DetailID].Width = 0;
                    c1UsedList.Cols[COL_ASBaseID].Width = 0;
                    c1UsedList.Cols[COL_ASBaseCode].Width = 0;
                    c1UsedList.Cols[COL_ASBaseDescription].Width = 0;
                    c1UsedList.Cols[COL_ASBaseFlag].Width = 0;
                    c1UsedList.Cols[COL_StartDate].Width = 75;
                    c1UsedList.Cols[COL_StartTime].Width = 70;
                    c1UsedList.Cols[COL_EndDate].Width = 75;
                    c1UsedList.Cols[COL_EndTime].Width = 70;
                    c1UsedList.Cols[COL_ColorCode].Width = 0;
                    c1UsedList.Cols[COL_LocationID].Width = 0;
                    c1UsedList.Cols[COL_LocationName].Width = 100;
                    c1UsedList.Cols[COL_DepartmentID].Width = 0;
                    c1UsedList.Cols[COL_DepartmentName].Width = 100;
                    c1UsedList.Cols[COL_Notes].Width = 150;

                #endregion

                if (oUsedList != null)
                {
                    for (int i = 0; i <= oUsedList.Count - 1; i++)
                    {
                        int _RowIndex = 0;
                        c1UsedList.Rows.Add();
                        _RowIndex = c1UsedList.Rows.Count-1;
                        c1UsedList.SetData(_RowIndex, COL_DetailID, oUsedList[i].DetailID);
                        c1UsedList.SetData(_RowIndex, COL_ASBaseID, oUsedList[i].ASBaseID);
                        c1UsedList.SetData(_RowIndex, COL_ASBaseCode, oUsedList[i].ASBaseCode);
                        c1UsedList.SetData(_RowIndex, COL_ASBaseDescription, oUsedList[i].ASBaseDescription);
                        c1UsedList.SetData(_RowIndex, COL_ASBaseFlag, oUsedList[i].ASBaseFlag.GetHashCode());
                        c1UsedList.SetData(_RowIndex, COL_StartDate, oUsedList[i].StartDate.ToShortDateString());
                        c1UsedList.SetData(_RowIndex, COL_StartTime, oUsedList[i].StartTime.ToShortTimeString());
                        c1UsedList.SetData(_RowIndex, COL_EndDate, oUsedList[i].EndDate.ToShortDateString());
                        c1UsedList.SetData(_RowIndex, COL_EndTime, oUsedList[i].EndTime.ToShortTimeString());
                        c1UsedList.SetData(_RowIndex, COL_ColorCode, oUsedList[i].ColorCode);
                        c1UsedList.SetData(_RowIndex, COL_LocationID, oUsedList[i].LocationID);
                        c1UsedList.SetData(_RowIndex, COL_LocationName, oUsedList[i].LocationName);
                        c1UsedList.SetData(_RowIndex, COL_DepartmentID, oUsedList[i].DepartmentID);
                        c1UsedList.SetData(_RowIndex, COL_DepartmentName, oUsedList[i].DepartmentName);
                        c1UsedList.SetData(_RowIndex, COL_Notes, oUsedList[i].Notes);
                    }
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

        private void FillUsedSchedules()
        {

        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            if (rbDelete.Checked == true)
            {
                oDialogResult = ASUpdateCriteria.DeleteOccurenceAndCreateNewRecurrence;
            }
            else if (rbDontDelete.Checked == true)
                //06-May-14 Aniket: Corrected. Replaced = with ==
            {//SLR: Is it correct? == vs = 4/22/2014

                oDialogResult = ASUpdateCriteria.DontDeleteOccurenceAndCreateNewRecurrence;
                for (int i = 1; i <= c1UsedList.Rows.Count - 1; i++)
                {
                    oDialogDontDeleteIDsList.Add(c1UsedList.GetData(i,COL_DetailID).ToString());
                }
            }
            this.Close();
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            oDialogResult = ASUpdateCriteria.CancelSave;
            this.Close();
        }

        private void rbDontDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDontDelete.Checked == true)
            {
                rbDontDelete.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbDontDelete.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDelete.Checked == true)
            {
                rbDelete.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbDelete.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void c1UsedList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
    }
}