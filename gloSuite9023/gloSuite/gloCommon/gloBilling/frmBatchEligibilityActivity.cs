using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloSettings;
using System.IO;
namespace gloBilling
{
    public partial class frmBatchEligibilityActivity : Form
    {

        #region "Private Variable"
        private String DataBaseConnectionString = "";
        private Int64 nClearingHouseID;
     //   private DateTime dtSendNextAptTime;
    //    private DateTime dtcheckTimeDurationTime;
     //   private DateTime dtTerminateDayOfafterAptTime;
        private DataTable dtBatchEligibiltySetting;
     //   private TimeSpan currentSendNextAptTime;
     //   private TimeSpan currentcheckTimeDurationTime;
     //   private TimeSpan currenterminateDayOfafterAptTime;
     //   private bool IsKeyPress = false;
        Boolean _IsCloseClick = false;
        public String _MessageBoxCaption = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        clsBatchEligibiltySetting oclsBatchEligibiltySetting = new clsBatchEligibiltySetting(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        private const int COL_BatchName = 0;
        private const int COL_Activity = 1;
        private const int COL_Status = 2;
        private const int COL_CreateDateTime=3;
        private const int COL_BatchID=4;
        Font oFontRegular = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        Font oFontBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        Int32 _Width;
        #endregion
        
        #region " Constructor "
        public frmBatchEligibilityActivity()
        {

            DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            InitializeComponent();
            _MessageBoxCaption = AppSettings.MessageBoxCaption;
           
        }

        #endregion

        #region FormLoadEvent
        private void frmBatchEligibilityActivity_Load(object sender, EventArgs e)
        {

            Refresh();
         }
        #endregion

        #region "Private Method"
        private void Refresh()
        {
            // this.dtCheckApt_DurationTime.ValueChanged -= new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
            dtBatchEligibiltySetting = oclsBatchEligibiltySetting.GetBatchEligibiltySetting();
            if (dtBatchEligibiltySetting.Rows.Count > 0 && dtBatchEligibiltySetting != null)
                FilEligibiltySettingData(dtBatchEligibiltySetting);
            //else
            //    cmbChkResponseDurTime.Text = "02:00";
            //currentSendNextAptTime = dtpApp_DateTime_StartTime.Value.TimeOfDay;
            //currentcheckTimeDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay;
            //currenterminateDayOfafterAptTime = dtTerminatAptAfterTime.Value.TimeOfDay;            
            FillBatchesGrid();
            DesignGrid();
            //  this.dtCheckApt_DurationTime.ValueChanged += new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        }
        private void FilEligibiltySettingData(DataTable dtElegibiltySettingData)
        {
               
                lblClearingHouse.Text =  dtBatchEligibiltySetting.Rows[0]["sClearingHouse"].ToString();
              //  dtpApp_DateTime_StartTime.Value = (oclsBatchEligibiltySetting.DateAsDateTime((TimeSpan)(dtBatchEligibiltySetting.Rows[0]["tSendNextDaysAptTime"])));
              //  cmbChkResponseDurTime.BindingContext = new BindingContext();   
              //  cmbChkResponseDurTime.BeginUpdate();
              //  cmbChkResponseDurTime.ValueMember = dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"].ToString();//(dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"]).ToString().Substring(0, (dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"]).ToString().Length - 3);
              //  cmbChkResponseDurTime.DisplayMember = (dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"]).ToString().Substring(0, (dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"]).ToString().Length - 3);
              //  cmbChkResponseDurTime.Text = (dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"]).ToString().Substring(0, (dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"]).ToString().Length - 3);
              //  cmbChkResponseDurTime.EndUpdate();
              ////  dtCheckApt_DurationTime.Value = (oclsBatchEligibiltySetting.DateAsDateTime((TimeSpan)dtBatchEligibiltySetting.Rows[0]["tCheckForResponseTime"]));
              //  dtTerminatAptAfterTime.Value = (oclsBatchEligibiltySetting.DateAsDateTime((TimeSpan)dtBatchEligibiltySetting.Rows[0]["tTerminateDayOfAfterTime"]));
                if (dtBatchEligibiltySetting.Rows[0]["nClearingHouseID"] != null || Convert.ToString(dtBatchEligibiltySetting.Rows[0]["nClearingHouseID"]) != "")
                 nClearingHouseID = Convert.ToInt64(dtBatchEligibiltySetting.Rows[0]["nClearingHouseID"]);
        }
        private void FillBatchesGrid()
        {
            DataTable _dtBatchesData = new DataTable();
            _dtBatchesData=oclsBatchEligibiltySetting.GetBatches();
           // C1BatchEligibiltyActivity.Cols[1].Style=c1
            if (_dtBatchesData != null && _dtBatchesData.Rows.Count > 0)
            {
                // int row = 0;
                //if (_dtBatchesData != null)
                // {
                //     for (int i = 1; i <=_dtBatchesData.Rows.Count; i++)
                //     {
                //         C1BatchEligibiltyActivity.Rows.Add();
                //         int a = C1BatchEligibiltyActivity.Rows.Count;
                //         C1BatchEligibiltyActivity.SetData(i, 0, _dtBatchesData.Rows[i-1][0].ToString());
                //         if (Convert.ToInt16(_dtBatchesData.Rows[i - 1][1]) == BatchEligibilityActivity.BatchResponse.GetHashCode())
                //          C1BatchEligibiltyActivity.SetData(i, 1, "Batch Response (270)");
                //         C1BatchEligibiltyActivity.SetData(i, 2, _dtBatchesData.Rows[i-1][2].ToString());
                //         C1BatchEligibiltyActivity.SetData(i, 3, _dtBatchesData.Rows[i-1][3].ToString());
                //         C1BatchEligibiltyActivity.SetData(i, 4, _dtBatchesData.Rows[i-1][4].ToString());
                //     }
                // }

                C1BatchEligibiltyActivity.BeginUpdate();
                C1BatchEligibiltyActivity.DataSource = _dtBatchesData.DefaultView;
                C1BatchEligibiltyActivity.EndUpdate();
                //cmbState.SelectedIndex = -1;
            }
            else
            {
                C1BatchEligibiltyActivity.BeginUpdate();
                C1BatchEligibiltyActivity.DataSource = _dtBatchesData.DefaultView;
                C1BatchEligibiltyActivity.EndUpdate();
            }
            if (_dtBatchesData != null)
            {
                _dtBatchesData.Dispose();
                _dtBatchesData = null;
            }
        }
        #endregion

        #region "Form Events"
        private void tsb_SaveClose_Click(object sender, EventArgs e)
        {
            bool IsSaved = false;
           DateTime dtSendNextAptTime = default(DateTime);
           DateTime dtcheckTimeDurationTime = default(DateTime);
           DateTime dtTerminateDayOfafterAptTime = default(DateTime);
           // dtcheckTimeDurationTime=(oclsBatchEligibiltySetting.DateAsDateTime(TimeSpan.Parse((cmbChkResponseDurTime.Text))));
          //  dtTerminateDayOfafterAptTime = dtTerminatAptAfterTime.Value;
            IsSaved= oclsBatchEligibiltySetting.SaveBatchEligibiltySetting(nClearingHouseID, dtSendNextAptTime, dtcheckTimeDurationTime, dtTerminateDayOfafterAptTime);
            if (IsSaved)
            {
                _IsCloseClick = true;
                this.Close();
            }
        }

        #region "Commented Code For Duration Time Control"
         //  private void dtCheckApt_DurationTime_ValueChanged(object sender, EventArgs e)
        //{
        //    TimeSpan ChngdChkAptDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay; 
        //    bool IshrsChangd = false;
        //    int minutechangd = ChngdChkAptDurationTime.Minutes;
        //    int hoursChnged = ChngdChkAptDurationTime.Hours;
        //    TimeSpan TotalTimeChnegd=ChngdChkAptDurationTime-currentcheckTimeDurationTime;
        //    if (currentcheckTimeDurationTime.Hours!=ChngdChkAptDurationTime.Hours)
        //    {
        //        IshrsChangd = true;
        //        if (dtCheckApt_DurationTime.Value.TimeOfDay.Hours <2)
        //        {
        //            hoursChnged = 6;
        //            minutechangd = 00;
        //        }
        //        else 
        //        if( dtCheckApt_DurationTime.Value.TimeOfDay.Hours > 6)
        //        {
        //            hoursChnged =2;
        //            minutechangd = 00;
        //        }
        //        this.dtCheckApt_DurationTime.ValueChanged -= new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
              
        //        dtCheckApt_DurationTime.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hoursChnged, minutechangd, DateTime.Today.Second);
        //        //dtcheckTimeDurationTime.value =new DateTime(
        //        //dtCheckApt_DurationTime.Format
        //        currentcheckTimeDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay;
                
        //        this.dtCheckApt_DurationTime.ValueChanged += new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //        currentcheckTimeDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay;
        //    }
        //    if(IsKeyPress&&!IshrsChangd&&dtCheckApt_DurationTime.Value.TimeOfDay.Minutes>1)
        //    {
        //        IsKeyPress = false;
        //        if (dtCheckApt_DurationTime.Value.TimeOfDay.Minutes > 1 && dtCheckApt_DurationTime.Value.TimeOfDay.Minutes < 15)
        //            minutechangd = 15;
        //        else if (dtCheckApt_DurationTime.Value.TimeOfDay.Minutes > 15 && dtCheckApt_DurationTime.Value.TimeOfDay.Minutes < 30)
        //            minutechangd = 30;
        //        else if (dtCheckApt_DurationTime.Value.TimeOfDay.Minutes > 30 && dtCheckApt_DurationTime.Value.TimeOfDay.Minutes < 45)
        //            minutechangd = 45;
        //        else if (dtCheckApt_DurationTime.Value.TimeOfDay.Minutes > 45 && dtCheckApt_DurationTime.Value.TimeOfDay.Minutes < 60)
        //            minutechangd = 59;
        //        else if (dtCheckApt_DurationTime.Value.TimeOfDay.Minutes == 60)
        //            minutechangd = 59;
        //        else
        //            minutechangd = dtCheckApt_DurationTime.Value.TimeOfDay.Minutes;
        //        this.dtCheckApt_DurationTime.ValueChanged -= new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //        dtCheckApt_DurationTime.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hoursChnged, minutechangd, DateTime.Today.Second);
        //        currentcheckTimeDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay;
        //        this.dtCheckApt_DurationTime.ValueChanged += new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //    }
        //    else if (!IshrsChangd && currentcheckTimeDurationTime.Minutes < ChngdChkAptDurationTime.Minutes)
        //    {

        //        minutechangd = minutechangd + 14;
        //        if (minutechangd >= 60)
        //            minutechangd = 59;
        //        this.dtCheckApt_DurationTime.ValueChanged -= new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //        dtCheckApt_DurationTime.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hoursChnged, minutechangd, DateTime.Today.Second);
        //        currentcheckTimeDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay;
        //        this.dtCheckApt_DurationTime.ValueChanged += new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //    }
        //   else if (!IshrsChangd && ChngdChkAptDurationTime.Minutes <= 0)
        //    {
        //        minutechangd = minutechangd + 15;
        //        if (minutechangd >= 61)
        //            minutechangd = 0;
        //        else if (minutechangd == 60)
        //            minutechangd = 59;
        //        this.dtCheckApt_DurationTime.ValueChanged -= new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //        dtCheckApt_DurationTime.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hoursChnged, minutechangd, DateTime.Today.Second);
        //        currentcheckTimeDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay;
        //        this.dtCheckApt_DurationTime.ValueChanged += new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //    }
        //    else if(!IshrsChangd)
        //    {
        //        if (currentcheckTimeDurationTime.Minutes==59)
        //            minutechangd = minutechangd - 13;
        //        else
        //            minutechangd = minutechangd - 14;
        //        if (minutechangd <= -1)
        //            minutechangd = 0;
        //        this.dtCheckApt_DurationTime.ValueChanged -= new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);
        //        dtCheckApt_DurationTime.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hoursChnged, minutechangd, DateTime.Today.Second);
        //        currentcheckTimeDurationTime = dtCheckApt_DurationTime.Value.TimeOfDay;
        //        this.dtCheckApt_DurationTime.ValueChanged += new System.EventHandler(this.dtCheckApt_DurationTime_ValueChanged);

        //    }
           
        //}
        #endregion
        private void dtpApp_DateTime_StartTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtTerminatAptAfterTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtCheckApt_DurationTime_KeyPress(object sender, KeyPressEventArgs e)
        {
           // IsKeyPress = true; 
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            
                _IsCloseClick = true;
                this.Close();
            
        }

        private void frmBatchEligibilityActivity_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_IsCloseClick)
            {
                    this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBatchEligibilityActivity_FormClosing);
                    _IsCloseClick = true;
                    this.Close();
            }
        }

        private void C1BatchEligibiltyActivity_DoubleClick(object sender, EventArgs e)
        {

        }

        #endregion

        private void txtBatchSearch_TextChanged(object sender, EventArgs e)
        {
        //     private const int COL_BatchName = 0;
        //private const int COL_Activity = 1;
        //private const int COL_Status = 2;
            DataView _dv = null;
            C1.Win.C1FlexGrid.C1FlexGrid _C1 = null;
            try
            {
               
                string _SearchText = txtBatchSearch.Text.Trim();
                string _Filter = "";

                _SearchText = _SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]").Replace("*", "%");

                _dv = (DataView)C1BatchEligibiltyActivity.DataSource;
                _C1 = C1BatchEligibiltyActivity;
                if (_dv == null) return;
                #region " SEARCH "
                if (_SearchText == "")
                    if (chkNoResponseFoundStatus.Checked==true)
                        _dv.RowFilter = "Status NOT LIKE 'No Response Found'";
                    else
                        _dv.RowFilter = "";

                else
                {
                    if (_SearchText.Contains(",") == false)
                    {
                        #region " Simple Search "

                        _Filter = _C1.Cols[COL_BatchName].Name + " LIKE '" + _SearchText + "%' OR " +
                        _C1.Cols[COL_Activity].Name + " LIKE '" + _SearchText + "%' OR " +
                      _C1.Cols[COL_Status].Name + " LIKE '" + _SearchText + "%' ";

                        #endregion
                    }
                    else
                    {
                        #region " Comma Separated Search "

                        string[] _SplitSearch = _SearchText.Split(',');
                        string _SplitString;

                        for (int i = 0; i < _SplitSearch.Length; i++)
                        {

                            _SplitString = _SplitSearch[i].Trim();

                            if (_SplitString != "")
                            {
                                if (_Filter != "")
                                    _Filter = _Filter + " AND ";

                                _Filter = _Filter + " ( " +
                                    _C1.Cols[COL_BatchName].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_Activity].Name + " LIKE '" + _SplitString + "%' OR " +
                                  _C1.Cols[COL_Status].Name + " LIKE '" + _SplitString + "%' " +
                                    " ) ";
                            }
                        }

                        #endregion
                    }

                    if (chkNoResponseFoundStatus.Checked == true)
                        _Filter = "( " + _Filter + " ) AND Status NOT LIKE 'No Response Found'";

                    _dv.RowFilter = _Filter;
                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _dv = null;
                _C1 = null;
            }
        }

        private void DesignGrid()
        {
            C1BatchEligibiltyActivity.Redraw = false;
            try
            {
               // C1BatchEligibiltyActivity.Cols.RemoveRange(0, 4);
                gloC1FlexStyle.Style(C1BatchEligibiltyActivity, true);

                C1BatchEligibiltyActivity.Rows.Fixed = 1;
                C1BatchEligibiltyActivity.AllowEditing = false;

                C1BatchEligibiltyActivity.Cols[COL_BatchName].Caption = "Batch Name";
                C1BatchEligibiltyActivity.Cols[COL_Activity].Caption = "Last Activity";
                C1BatchEligibiltyActivity.Cols[COL_Status].Caption = "Status";
                C1BatchEligibiltyActivity.Cols[COL_CreateDateTime].Caption = "Date/Time";
                C1BatchEligibiltyActivity.Cols[COL_BatchID].Caption = "BatchID";
              

                _Width = C1BatchEligibiltyActivity.Width;

                C1BatchEligibiltyActivity.Cols[COL_BatchName].Width = (Int32)(_Width * 0.3);
                C1BatchEligibiltyActivity.Cols[COL_Activity].Width = (Int32)(_Width * 0.25);
                C1BatchEligibiltyActivity.Cols[COL_Status].Width = (Int32)(_Width * 0.25);
                C1BatchEligibiltyActivity.Cols[COL_CreateDateTime].Width = (Int32)(_Width *0.2);
                C1BatchEligibiltyActivity.Cols[COL_BatchID].Width = (Int32)(_Width * 0.1);
               

                C1BatchEligibiltyActivity.Cols[COL_BatchID].Visible = false;

                C1BatchEligibiltyActivity.Cols[COL_CreateDateTime].DataType = typeof(DateTime);
                C1BatchEligibiltyActivity.Cols[COL_CreateDateTime].Format = "MM/dd/yyyy hh:mm tt";
               // C1BatchEligibiltyActivity.Cols[COL_BatchName].DataType = typeof(string);
               // C1BatchEligibiltyActivity.Cols[COL_Activity].DataType = typeof(string);
               // C1BatchEligibiltyActivity.Cols[COL_Status].DataType = typeof(string);
               //// C1BatchEligibiltyActivity.Cols[COL_CreateDateTime].Format = "MM/dd/yyyy";
                // Date Format and Sorting Mahesh Nawal
                //C1BatchEligibiltyActivity.Cols[COL_ImportDate].DataType = typeof(System.DateTime);
                //C1BatchEligibiltyActivity.Cols[COL_ImportDate].Format = "MM/dd/yyyy";
                //C1BatchEligibiltyActivity.Cols[COL_FileDate].DataType = typeof(System.DateTime);
                //C1BatchEligibiltyActivity.Cols[COL_FileDate].Format = "MM/dd/yyyy";
                //C1BatchEligibiltyActivity.Cols["SearchImportDate"].Visible = false;
                //C1BatchEligibiltyActivity.Cols["SearchFileDate"].Visible = false;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                C1BatchEligibiltyActivity.Redraw = true;
            }
        }
        private void chkNoResponseFoundStatus_CheckedChanged(object sender, EventArgs e)
        {

            if (txtBatchSearch.Text.Trim() != "")
            {
                txtBatchSearch.TextChanged -= new EventHandler(txtBatchSearch_TextChanged);
                txtBatchSearch.Clear();
                txtBatchSearch.TextChanged += new EventHandler(txtBatchSearch_TextChanged);
            }

            if (chkNoResponseFoundStatus.Checked == true)
            {
               
                //chkNoResponseFoundStatus.Font = oFontBold;                
                if (C1BatchEligibiltyActivity.DataSource != null)
                    ((DataView)C1BatchEligibiltyActivity.DataSource).RowFilter = "Status NOT LIKE 'No Response Found'";                
            }
            else
            {
                chkNoResponseFoundStatus.Font = oFontRegular;
                if (C1BatchEligibiltyActivity.DataSource != null)
                    ((DataView)C1BatchEligibiltyActivity.DataSource).RowFilter = "";
            }
            //c1ERAFiles_AfterRowColChange(null, null);
        }

        private void tsbView270File_Click(object sender, EventArgs e)
        {
            Int64 nBatchID = 0;
            string sFileName = "270RequestFile.TXT";
            DataTable _dtBathfile = new DataTable();
            if (C1BatchEligibiltyActivity.RowSel > 0)
            {
                nBatchID = Convert.ToInt64(C1BatchEligibiltyActivity.Cols[4][C1BatchEligibiltyActivity.RowSel]);
                sFileName = Convert.ToString((C1BatchEligibiltyActivity.Cols[0][C1BatchEligibiltyActivity.RowSel]))+ ".txt";
                _dtBathfile = oclsBatchEligibiltySetting.GetBatchFile(nBatchID);
                if (_dtBathfile != null && _dtBathfile.Rows.Count > 0)
                {
                    if (_dtBathfile.Rows[0]["i270BinaryFile"] != DBNull.Value)
                    {
                        sFileName = oclsBatchEligibiltySetting.ConvertBinaryToFile(_dtBathfile.Rows[0]["i270BinaryFile"], sFileName);
                        if (File.Exists(sFileName))
                            System.Diagnostics.Process.Start(sFileName);
                        else
                            MessageBox.Show("No 270 File Found.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                        MessageBox.Show("No 270  File Found.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                     
                }
                else
                    MessageBox.Show("No 270 File Found.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show(C1BatchEligibiltyActivity.RowSel.ToString());

            }
            if (_dtBathfile != null)
            {
                _dtBathfile.Dispose();
                _dtBathfile = null;
            }
        }

        private void tsbView271File_Click(object sender, EventArgs e)
        {
            Int64 nBatchID = 0;
            string sFileName = "271ResponsFile.TXT";
            DataTable _dtBathfile = new DataTable();
            if (C1BatchEligibiltyActivity.RowSel > 0)
            {
                nBatchID = Convert.ToInt64(C1BatchEligibiltyActivity.Cols[4][C1BatchEligibiltyActivity.RowSel]);
                sFileName = Convert.ToString((C1BatchEligibiltyActivity.Cols[0][C1BatchEligibiltyActivity.RowSel])) + ".txt"; ;
                _dtBathfile = oclsBatchEligibiltySetting.GetBatchFile(nBatchID);
                if (_dtBathfile != null && _dtBathfile.Rows.Count > 0)
                {
                    if (_dtBathfile.Rows[0]["i271BinaryFile"] != DBNull.Value)
                    {
                        sFileName = oclsBatchEligibiltySetting.ConvertBinaryToFile(_dtBathfile.Rows[0]["i271BinaryFile"], sFileName);
                        if (File.Exists(sFileName))
                            System.Diagnostics.Process.Start(sFileName);
                        else
                            MessageBox.Show("Batch Response(271) Not Found.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                        MessageBox.Show("Batch Response(271) Not Found.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Batch Response(271) Not Found.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show(C1BatchEligibiltyActivity.RowSel.ToString());

            }
            if (_dtBathfile != null)
            {
                _dtBathfile.Dispose();
                _dtBathfile = null;
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }



    }
}
