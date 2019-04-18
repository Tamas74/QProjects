using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentScheduling
{
    public partial class frmModifyScheduleCriteria : Form
    {
        #region Declaration Variables & Properties

        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private ShortApointmentSchedules _ProblemTypes = null;
        private ShortApointmentSchedules _Resources = null;
        private DateTime _dtNewStartTime;
        private DateTime _dtNewEndTime;
        private string _MessageText = "";  

        public ShortApointmentSchedules ProblemTypes
        {
            get { return _ProblemTypes; }
            set { _ProblemTypes = value; }
        }
        public ShortApointmentSchedules Resources
        {
            get { return _Resources; }
            set { _Resources = value; }
        }
        public DateTime NewStartTime
        {
            get { return _dtNewStartTime; }
            set { _dtNewStartTime = value; }
        }
        public DateTime NewEndTime
        {
            get { return _dtNewEndTime; }
            set { _dtNewEndTime = value; }
        }
        public string MessageText
        {
            get { return _MessageText; }
            set { _MessageText = value; }
        }

        #endregion

        #region "C1Grid column Constants"

        private const int COL_ID = 0;
        private const int COL_CODE = 1;
        private const int COL_DESC = 2;
        private const int COL_STARTTIME = 3;
        private const int COL_ENDTIME = 4;
        private const int COL_NEW_STARTTIME = 5;
        private const int COL_NEW_ENDTIME = 6;
        private const int COL_OBJECT = 7;
        private const int COL_COLUMNCOUNT = 8;

        #endregion

        #region "Contructor"

        public frmModifyScheduleCriteria()
        {

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
        #endregion

        #region "Form Load Event"

        private void frmModifyScheduleCriteria_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1ProblemType, false);
            gloC1FlexStyle.Style(c1Resource, false);
            
            this.DialogResult = DialogResult.None;

            lblMessage.Text = _MessageText;
            lblStartTime.Text = _dtNewStartTime.ToShortTimeString();
            lblEndTime.Text = _dtNewEndTime.ToShortTimeString();   

            DesignGrid();
            FillData();
        }

        #endregion

        #region "Tool Strip Buttons Click"

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (SetData() == true)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        break;
                    case "Cancel":
                        this.DialogResult = DialogResult.Cancel;
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

        #endregion

        #region "Methods"

        private void FillData()
        {
            try
            {
                if (_ProblemTypes != null)
                {
                    for (int i = 0; i < _ProblemTypes.Count; i++)
                    {
                        Int32 RowIndex;
                        c1ProblemType.Rows.Add();
                        RowIndex = c1ProblemType.Rows.Count - 1;
                        c1ProblemType.SetData(RowIndex, COL_ID, _ProblemTypes[i].ASCommonID);
                        c1ProblemType.SetData(RowIndex, COL_CODE, _ProblemTypes[i].ASCommonCode);
                        c1ProblemType.SetData(RowIndex, COL_DESC, _ProblemTypes[i].ASCommonDescription);
                        c1ProblemType.SetData(RowIndex, COL_STARTTIME, _ProblemTypes[i].StartTime.ToShortTimeString());
                        c1ProblemType.SetData(RowIndex, COL_ENDTIME, _ProblemTypes[i].EndTime.ToShortTimeString());
                        c1ProblemType.SetData(RowIndex, COL_NEW_STARTTIME, _dtNewStartTime.ToShortTimeString());
                        c1ProblemType.SetData(RowIndex, COL_NEW_ENDTIME, _dtNewEndTime.ToShortTimeString());
                        c1ProblemType.SetData(RowIndex, COL_OBJECT, _ProblemTypes[i]);
                    }
                }

                if (_Resources != null)
                {
                    for (int i = 0; i < _Resources.Count; i++)
                    {
                        Int32 RowIndex;
                        c1Resource.Rows.Add();
                        RowIndex = c1Resource.Rows.Count - 1;
                        c1Resource.SetData(RowIndex, COL_ID, _Resources[i].ASCommonID);
                        c1Resource.SetData(RowIndex, COL_CODE, _Resources[i].ASCommonCode);
                        c1Resource.SetData(RowIndex, COL_DESC, _Resources[i].ASCommonDescription);
                        c1Resource.SetData(RowIndex, COL_STARTTIME, _Resources[i].StartTime.ToShortTimeString());
                        c1Resource.SetData(RowIndex, COL_ENDTIME, _Resources[i].EndTime.ToShortTimeString());
                        c1Resource.SetData(RowIndex, COL_NEW_STARTTIME, _dtNewStartTime.ToShortTimeString());
                        c1Resource.SetData(RowIndex, COL_NEW_ENDTIME, _dtNewEndTime.ToShortTimeString());
                        c1Resource.SetData(RowIndex, COL_OBJECT, _Resources[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool SetData()
        {
            bool _result = false;
            ShortApointmentSchedules oProblemTypes = new ShortApointmentSchedules();
            ShortApointmentSchedules oResources = new ShortApointmentSchedules();
            try
            {
                if (ValidateData())
                {
                    ShortApointmentSchedule oProblemType = null;
                    for (int i = 1; i < c1ProblemType.Rows.Count; i++)
                    {
                        oProblemType = (ShortApointmentSchedule)c1ProblemType.GetData(i, COL_OBJECT);
                        oProblemType.StartTime = Convert.ToDateTime(c1ProblemType.GetData(i, COL_NEW_STARTTIME));
                        oProblemType.EndTime = Convert.ToDateTime(c1ProblemType.GetData(i, COL_NEW_ENDTIME));

                        oProblemTypes.Add(oProblemType);
                        oProblemType = null;
                    }

                    ShortApointmentSchedule oResource = null;
                    for (int i = 1; i < c1Resource.Rows.Count; i++)
                    {
                        oResource = (ShortApointmentSchedule)c1Resource.GetData(i, COL_OBJECT);
                        oResource.StartTime = Convert.ToDateTime(c1Resource.GetData(i, COL_NEW_STARTTIME));
                        oResource.EndTime = Convert.ToDateTime(c1Resource.GetData(i, COL_NEW_ENDTIME));

                        oResources.Add(oResource);
                        oResource = null;
                    }

                    _ProblemTypes = oProblemTypes;
                    _Resources = oResources;
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            return _result;
        }

        private bool ValidateData()
        {
            #region " Problem Type/ Resource Time Validations "

            //   DateTime dtScheduleStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + _dtNewStartTime.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            //DateTime dtScheduleEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + _dtNewEndTime.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

            DateTime dtScheduleStartTime = Convert.ToDateTime(String.Format(_dtNewStartTime.ToShortDateString() + " " + _dtNewStartTime.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
            DateTime dtScheduleEndTime = Convert.ToDateTime(String.Format(_dtNewEndTime.ToShortDateString() + " " + _dtNewEndTime.ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));


            //Check Problem Type Start & End Time is between Schedule Time 
            for (int i = 1; i < c1ProblemType.Rows.Count; i++)
            {
                DateTime dtPTStartTime;
                DateTime dtPTEndTime;

                //dtPTStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProblemType.GetData(i, COL_NEW_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //dtPTEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1ProblemType.GetData(i, COL_NEW_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                dtPTStartTime = Convert.ToDateTime(String.Format(_dtNewStartTime.ToShortDateString() + " " + Convert.ToDateTime(c1ProblemType.GetData(i, COL_NEW_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                dtPTEndTime = Convert.ToDateTime(String.Format(_dtNewEndTime.ToShortDateString() + " " + Convert.ToDateTime(c1ProblemType.GetData(i, COL_NEW_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));


                if (dtPTStartTime > dtPTEndTime)
                {
                    MessageBox.Show(" Problem type 'Start time' must be less than 'End time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1ProblemType.Focus();
                    c1ProblemType.Row = i;
                    c1ProblemType.Col = COL_NEW_STARTTIME;
                    return false;
                }

                if (dtPTStartTime < dtScheduleStartTime || dtPTStartTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Problem type 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1ProblemType.Focus();
                    c1ProblemType.Row = i;
                    c1ProblemType.Col = COL_NEW_STARTTIME;
                    return false;
                }
                if (dtPTEndTime < dtScheduleStartTime || dtPTEndTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Problem type 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1ProblemType.Focus();
                    c1ProblemType.Row = i;
                    c1ProblemType.Col = COL_NEW_ENDTIME;
                    return false;
                }
            }

            //Check Resource Start & End Time is between Schedule Time 
            for (int i = 1; i < c1Resource.Rows.Count; i++)
            {
                DateTime dtRSStartTime;
                DateTime dtRSEndTime;

                //dtRSStartTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resource.GetData(i, COL_NEW_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                //dtRSEndTime = Convert.ToDateTime(String.Format(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(c1Resource.GetData(i, COL_NEW_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));

                dtRSStartTime = Convert.ToDateTime(String.Format(_dtNewStartTime.ToShortDateString() + " " + Convert.ToDateTime(c1Resource.GetData(i, COL_NEW_STARTTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));
                dtRSEndTime = Convert.ToDateTime(String.Format(_dtNewEndTime.ToShortDateString() + " " + Convert.ToDateTime(c1Resource.GetData(i, COL_NEW_ENDTIME)).ToShortTimeString(), "MM/dd/yyyy hh:mm:ss tt"));


                if (dtRSStartTime > dtRSEndTime)
                {
                    MessageBox.Show(" Resource 'Start time' must be less than 'End time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1Resource.Focus();
                    c1Resource.Row = i;
                    c1Resource.Col = COL_NEW_STARTTIME;
                    return false;
                }

                if (dtRSStartTime < dtScheduleStartTime || dtRSStartTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Resource 'Start time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1Resource.Focus();
                    c1Resource.Row = i;
                    c1Resource.Col = COL_NEW_STARTTIME;
                    return false;
                }
                if (dtRSEndTime < dtScheduleStartTime || dtRSEndTime > dtScheduleEndTime)
                {
                    MessageBox.Show(" Resource 'End time' must be in 'Schedule time'.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1Resource.Focus();
                    c1Resource.Row = i;
                    c1Resource.Col = COL_NEW_ENDTIME;
                    return false;
                }
            }




            #endregion

            return true;
        }


        private void DesignGrid()
        {
            try
            {
                int _width;

                #region ProblemType

                c1ProblemType.Rows.Count = 1;
                c1ProblemType.Rows.Fixed = 1;
                c1ProblemType.Cols.Count = COL_COLUMNCOUNT;
                c1ProblemType.Cols.Fixed = 0;

                c1ProblemType.SetData(0, COL_ID, "ID");
                c1ProblemType.SetData(0, COL_CODE, "Code");
                c1ProblemType.SetData(0, COL_DESC, "Description");
                c1ProblemType.SetData(0, COL_STARTTIME, "Start Time");
                c1ProblemType.SetData(0, COL_ENDTIME, "End Time");
                c1ProblemType.SetData(0, COL_NEW_STARTTIME, "New Start Time");
                c1ProblemType.SetData(0, COL_NEW_ENDTIME, "New End Time");
                c1ProblemType.SetData(0, COL_OBJECT, "Object");


                c1ProblemType.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
                c1ProblemType.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
                c1ProblemType.Cols[COL_NEW_STARTTIME].DataType = typeof(System.DateTime);
                c1ProblemType.Cols[COL_NEW_ENDTIME].DataType = typeof(System.DateTime);
                c1ProblemType.Cols[COL_STARTTIME].Format = "hh:mm tt";
                c1ProblemType.Cols[COL_ENDTIME].Format = "hh:mm tt";
                c1ProblemType.Cols[COL_NEW_STARTTIME].Format = "hh:mm tt";
                c1ProblemType.Cols[COL_NEW_ENDTIME].Format = "hh:mm tt";


                c1ProblemType.Cols[COL_ID].Visible = false;
                c1ProblemType.Cols[COL_CODE].Visible = true;
                c1ProblemType.Cols[COL_DESC].Visible = true;
                c1ProblemType.Cols[COL_STARTTIME].Visible = true;
                c1ProblemType.Cols[COL_ENDTIME].Visible = true;
                c1ProblemType.Cols[COL_NEW_STARTTIME].Visible = true;
                c1ProblemType.Cols[COL_NEW_ENDTIME].Visible = true;
                c1ProblemType.Cols[COL_OBJECT].Visible = false;

                _width = (pnlCriteria_ProblemType.Width - 2);

                c1ProblemType.Cols[COL_ID].Width = 0;
                c1ProblemType.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.15);
                c1ProblemType.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.20);
                c1ProblemType.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.16);
                c1ProblemType.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.16);
                c1ProblemType.Cols[COL_NEW_STARTTIME].Width = Convert.ToInt32(_width * 0.16);
                c1ProblemType.Cols[COL_NEW_ENDTIME].Width = Convert.ToInt32(_width * 0.16);
                c1ProblemType.Cols[COL_OBJECT].Width = 0;

                c1ProblemType.AllowEditing = true;
                c1ProblemType.Cols[COL_ID].AllowEditing = false;
                c1ProblemType.Cols[COL_CODE].AllowEditing = false;
                c1ProblemType.Cols[COL_DESC].AllowEditing = false;
                c1ProblemType.Cols[COL_STARTTIME].AllowEditing = false;
                c1ProblemType.Cols[COL_ENDTIME].AllowEditing = false;
                c1ProblemType.Cols[COL_NEW_STARTTIME].AllowEditing = true;
                c1ProblemType.Cols[COL_NEW_ENDTIME].AllowEditing = true;
                c1ProblemType.Cols[COL_OBJECT].AllowEditing = false;

                #endregion

                #region Resources

                c1Resource.Rows.Count = 1;
                c1Resource.Rows.Fixed = 1;
                c1Resource.Cols.Count = COL_COLUMNCOUNT;
                c1Resource.Cols.Fixed = 0;

                c1Resource.SetData(0, COL_ID, "ID");
                c1Resource.SetData(0, COL_CODE, "Code");
                c1Resource.SetData(0, COL_DESC, "Description");
                c1Resource.SetData(0, COL_STARTTIME, "Start Time");
                c1Resource.SetData(0, COL_ENDTIME, "End Time");
                c1Resource.SetData(0, COL_NEW_STARTTIME, "New Start Time");
                c1Resource.SetData(0, COL_NEW_ENDTIME, "New End Time");
                c1Resource.SetData(0, COL_OBJECT, "Object");


                c1Resource.Cols[COL_STARTTIME].DataType = typeof(System.DateTime);
                c1Resource.Cols[COL_ENDTIME].DataType = typeof(System.DateTime);
                c1Resource.Cols[COL_NEW_STARTTIME].DataType = typeof(System.DateTime);
                c1Resource.Cols[COL_NEW_ENDTIME].DataType = typeof(System.DateTime);
                c1Resource.Cols[COL_STARTTIME].Format = "hh:mm tt";
                c1Resource.Cols[COL_ENDTIME].Format = "hh:mm tt";
                c1Resource.Cols[COL_NEW_STARTTIME].Format = "hh:mm tt";
                c1Resource.Cols[COL_NEW_ENDTIME].Format = "hh:mm tt";


                c1Resource.Cols[COL_ID].Visible = false;
                c1Resource.Cols[COL_CODE].Visible = true;
                c1Resource.Cols[COL_DESC].Visible = true;
                c1Resource.Cols[COL_STARTTIME].Visible = true;
                c1Resource.Cols[COL_ENDTIME].Visible = true;
                c1Resource.Cols[COL_NEW_STARTTIME].Visible = true;
                c1Resource.Cols[COL_NEW_ENDTIME].Visible = true;
                c1Resource.Cols[COL_OBJECT].Visible = false;

                _width = (pnlCriteria_Resource.Width - 2);

                c1Resource.Cols[COL_ID].Width = 0;
                c1Resource.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.15);
                c1Resource.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.20);
                c1Resource.Cols[COL_STARTTIME].Width = Convert.ToInt32(_width * 0.16);
                c1Resource.Cols[COL_ENDTIME].Width = Convert.ToInt32(_width * 0.16);
                c1Resource.Cols[COL_NEW_STARTTIME].Width = Convert.ToInt32(_width * 0.16);
                c1Resource.Cols[COL_NEW_ENDTIME].Width = Convert.ToInt32(_width * 0.16);
                c1Resource.Cols[COL_OBJECT].Width = 0;

                c1Resource.AllowEditing = true;
                c1Resource.Cols[COL_ID].AllowEditing = false;
                c1Resource.Cols[COL_CODE].AllowEditing = false;
                c1Resource.Cols[COL_DESC].AllowEditing = false;
                c1Resource.Cols[COL_STARTTIME].AllowEditing = false;
                c1Resource.Cols[COL_ENDTIME].AllowEditing = false;
                c1Resource.Cols[COL_NEW_STARTTIME].AllowEditing = true;
                c1Resource.Cols[COL_NEW_ENDTIME].AllowEditing = true;
                c1Resource.Cols[COL_OBJECT].AllowEditing = false;

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        } 

        #endregion

        private void c1ProblemType_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void c1Resource_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }
      
    }
}