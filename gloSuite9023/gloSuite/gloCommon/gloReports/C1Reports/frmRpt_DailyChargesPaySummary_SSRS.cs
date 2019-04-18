using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloGlobal;


namespace gloReports.C1Reports
{
    public partial class frmRpt_DailyChargesPaySummary_SSRS : Form
    {

        #region " Variable Declarations "

        string strReportServer = string.Empty;
        string strReportFolder = string.Empty;
        string strVirtualDir = string.Empty;
        string strReportProtocol = string.Empty;
        System.Uri SSRSReportURL;

    
        private string _databaseconnectionstring = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
      
      //  private Boolean _isFormLoading=false;

        private Boolean _DailyCloseSetting = false;

        #endregion

        #region " Constructors "
        
        public frmRpt_DailyChargesPaySummary_SSRS(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
        }
        
        #endregion

       
        #region " Form Events "

        private void frmRpt_DailyChargesPaySummary_SSRS_Load(object sender, EventArgs e)
        {
           
            try
            {
                string _sclinicName = "";
                _sclinicName = gloReports.getClinicName();
                //_isFormLoading = true;
                FillDailyCloseDates();
                FillDayClose();
                lblUserNm.Text = gloGlobal.gloPMGlobal.UserName.ToString();
                //tsb_btnDailyClose.Visible = false;
                GetReportServerSettings();
                FillChargeReport(_sclinicName);
                //SSRSViewerPayment.Print += new CancelEventHandler(SSRSViewerPayment_Print);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
        }

        //void SSRSViewerPayment_Print(object sender, CancelEventArgs e)
        //{
        //   // throw new NotImplementedException();
        //}

        private void FillChargeReport(string _clinicName)
        {
            try
            {
                string reportParam = "&suser=" + gloGlobal.gloPMGlobal.UserName + "&Practice=" + _clinicName;
                this.Text = "Daily Charge Report";
                List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();


                if (strReportProtocol == "" || strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                    SSRSViewerCharge.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    SSRSViewerCharge.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }
                //SSRSReportURL = new Uri("http://" + strReportServer + "/" + strVirtualDir);
                //SSRSViewerCharge.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                //SSRSViewerCharge.ServerReport.ReportServerUrl = SSRSReportURL;


                //axWebBrowser1.Navigate("http://" + strReportServer + "/" + strVirtualDir + "?/" + strReportFolder + "/" + _reportName + reportParam + "&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=true");
                SSRSViewerCharge.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptDailyCharge";// +reportParam;
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", gloGlobal.gloPMGlobal.UserName, false));
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", _clinicName, false));
                this.SSRSViewerCharge.ServerReport.SetParameters(paramList);
                this.SSRSViewerCharge.RefreshReport();
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillCloseReport(string _clinicName)
        {
            try
            {
                string reportParam = "&suser=" + gloGlobal.gloPMGlobal.UserName + "&Practice=" + _clinicName;
                this.Text = "Daily Close Report";
                List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();

                if (strReportProtocol == "" || strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                    SSRSViewerClose.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    SSRSViewerClose.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }

                SSRSViewerClose.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptDailyCloseSummary";// +reportParam;
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", gloGlobal.gloPMGlobal.UserName, false));
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", _clinicName, false));
                this.SSRSViewerClose.ServerReport.SetParameters(paramList);
                this.SSRSViewerClose.RefreshReport();
            }            
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillPaymentReport(string _clinicName)
        {
            try
            {
            
            string reportParam = "&suser=" + gloGlobal.gloPMGlobal.UserName + "&Practice=" + _clinicName;
            this.Text = "Daily Payment Report";
            List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();

            if (strReportProtocol == "" || strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
            {
                MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                SSRSViewerPayment.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                SSRSViewerPayment.ServerReport.ReportServerUrl = SSRSReportURL;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return;
            }

            SSRSViewerPayment.ServerReport.ReportPath = "/" + strReportFolder + "/" + "rptDailyPaymentList";// +reportParam;
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", gloGlobal.gloPMGlobal.UserName, false));
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", _clinicName, false));
            this.SSRSViewerPayment.ServerReport.SetParameters(paramList);
            this.SSRSViewerPayment.RefreshReport();
            this.SSRSViewerPayment.PromptAreaCollapsed = true;
            this.SSRSViewerPayment.PromptAreaCollapsed = false;
            }
        
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void GetReportServerSettings()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            try
            {
                //RETRIVING REPORT SERVER NAME
              
                object oValue = new object();
                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    strReportProtocol = oValue.ToString();
                    oValue = null;
                }



                //RETRIVING REPORT FOLDER NAME
                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString();
                    oValue = null;
                }

                //RETRIVING VIRTUAL DIRECTORY NAME
                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString();
                    oValue = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSetting != null) { oSetting.Dispose(); }
            }

        }

        #endregion

        
        #region " Tool Strip Events "
        
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_btnDailyClose_Click(object sender, EventArgs e)
        {

            try
            {

                Cursor.Current = Cursors.WaitCursor;
                DialogResult _dltRst = DialogResult.None;
                Int64 _CloseDate = 0;
                string _sCloseDate = "";
                Int64 _nToDate = 0;
                string _sToDate = "";
                string _strDayClose = "";
                 int _StIndx = -1; 
                int _EdIndx = -1;
                if (gloReports.get_DailyCloseSetting() == true)
                {
                    _DailyCloseSetting = true;
                }
                else
                {
                    _DailyCloseSetting = false;
                }

                if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
                {

                    if (trvMonths.Nodes != null)
                    {

                        bool _ItemFound = false;

                        #region "Validation"
                        //Check the day is selected
                        for (int i = 0; i < trvMonths.Nodes.Count; i++)
                        {
                            if (trvMonths.Nodes[i].Checked == true)
                            {
                                _ItemFound = true;
                                break;
                            }
                        }

                        if (_ItemFound == false)
                        {
                            MessageBox.Show("Please select a Date to Close.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        for (int i = 0; i < trvMonths.Nodes.Count; i++)
                        {
                            if (trvMonths.Nodes[i].Checked == true)
                            {
                                _StIndx = i; break;
                            }
                        }

                        for (int i = trvMonths.Nodes.Count - 1; i >= 0; i--)
                        {
                            if (trvMonths.Nodes[i].Checked == true)
                            {
                                _EdIndx = i; break;
                            }
                        }

                        //Check Previous day is closed or not
                        if (trvMonths.Nodes.Count > 1)
                        {
                            for (int i = 1; i < trvMonths.Nodes.Count; i++)
                            {
                                if (trvMonths.Nodes[i].Checked == true)
                                {
                                    if (trvMonths.Nodes[0].Checked == false)
                                    {
                                        MessageBox.Show("The day cannot be closed until the previous days are closed. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                        for (int m = 1; m < trvMonths.Nodes.Count; m++)
                                        {
                                            trvMonths.Nodes[m].Checked = false;
                                        }
                                        trvMonths.Nodes[0].Checked = true;
                                        return;
                                    }
                                }
                            }
                        }

                        //Check Previous day is closed or not
                        _ItemFound = false;
                        if (trvMonths.Nodes.Count > 0)
                        {
                            for (int i = _StIndx; i <= _EdIndx; i++)
                            {
                                if (trvMonths.Nodes[i].Checked == false)
                                {
                                    _ItemFound = true; break;
                                }
                                if (_CloseDate <= 0)
                                {
                                    _CloseDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(trvMonths.Nodes[i].Text));
                                    _sCloseDate = Convert.ToString(trvMonths.Nodes[i].Text);

                                    if (_StIndx == _EdIndx)
                                    {
                                        _nToDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(trvMonths.Nodes[i].Text));
                                        _sToDate = Convert.ToString(trvMonths.Nodes[i].Text);
                                    }
                                }
                                else
                                {
                                    _nToDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(trvMonths.Nodes[i].Text));
                                    _sToDate = Convert.ToString(trvMonths.Nodes[i].Text);
                                }
                            }
                        }
                        if (_ItemFound == true)
                        {
                            MessageBox.Show("The day cannot be closed until the previous days are closed. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            for (int m = 1; m < trvMonths.Nodes.Count; m++)
                            {
                                trvMonths.Nodes[m].Checked = false;
                            }
                            trvMonths.Nodes[0].Checked = true;
                            return;
                        }

                     

                        #endregion "Validation"
                    }
                }

                string text = string.Empty;
                if (trvMonths.Nodes.Count > 0)
                {
                    for (int i = 0; i < trvMonths.Nodes.Count; i++)
                    {
                        if (trvMonths.Nodes[i].Checked == true && Convert.ToDateTime(trvMonths.Nodes[i].Text) > DateTime.Now.Date)
                        {
                            if (text != string.Empty)
                            {
                                if (Convert.ToDateTime(text).Date < Convert.ToDateTime(trvMonths.Nodes[i].Text).Date)
                                { text = trvMonths.Nodes[i].Text; }
                            }
                            else
                            {
                                text = trvMonths.Nodes[i].Text;
                            }

                        }
                    }
                }
                if (text != string.Empty)
                {
                    if (MessageBox.Show("Warning – you have selected a future date to be closed. Continue to close " + text + "?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (_CloseDate > 0)
                {
                    if (_nToDate > 0 && _CloseDate != _nToDate)
                    {
                        // _dltRst = MessageBox.Show("Close " + _sCloseDate + "  To  " + _sToDate + "  ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (_DailyCloseSetting == false)
                        {
                            //Warning Message
                            string msg = Get_RemainingAmount_Alert(get_DateRangeString());
                            if (msg == "OK")
                            {
                                _dltRst = MessageBox.Show("Close " + _sCloseDate + "  To  " + _sToDate + "?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                switch (_dltRst)
                                {

                                    case DialogResult.No:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.No, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    case DialogResult.Yes:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.Yes, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                _dltRst = MessageBox.Show(msg, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                switch (_dltRst)
                                {

                                    case DialogResult.No:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.No, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    case DialogResult.Yes:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.Yes, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                        else
                        {
                            //Restriction Message
                            string msg = Get_RemainingAmount_Alert(get_DateRangeString());
                            if (msg == "OK")
                            {
                                _dltRst = MessageBox.Show("Close " + _sCloseDate + "  To  " + _sToDate + "?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                switch (_dltRst)
                                {

                                    case DialogResult.No:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.No, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    case DialogResult.Yes:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.Yes, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                // msg = msg + Environment.NewLine + "A day cannot be closed until its payments have been completed.";
                               _dltRst=  MessageBox.Show(msg, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                switch (_dltRst)
                                {

                                    case DialogResult.None:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.OK, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    case DialogResult.OK:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.OK, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }
                                return;
                            }


                        }
                    }
                    else
                    {
                        // _dltRst = MessageBox.Show("Close " + _sCloseDate + "  ?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (_DailyCloseSetting == false)
                        {
                            //Warning Message
                            string msg = Get_RemainingAmount_Alert(get_DateRangeString());
                            if (msg == "OK")
                            {
                                _dltRst = MessageBox.Show("Close " + _sCloseDate + "?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                
                                switch (_dltRst)
                                {

                                    case DialogResult.No:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.No, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    case DialogResult.Yes:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.Yes, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                _dltRst = MessageBox.Show(msg, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                switch (_dltRst)
                                {
                                   
                                    case DialogResult.No:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.No, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;                                   
                                    case DialogResult.Yes:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.Yes, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }
                               
                            }

                        }
                        else
                        {
                            //Restriction Message
                            string msg = Get_RemainingAmount_Alert(get_DateRangeString());

                            if (msg == "OK")
                            {
                                _dltRst = MessageBox.Show("Close " + _sCloseDate + "?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                               
                                switch (_dltRst)
                                {

                                    case DialogResult.No:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.No, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    case DialogResult.Yes:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.Yes, "Close " + _sCloseDate + "?", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                //msg = msg + Environment.NewLine + "A day cannot be closed until its payments have been completed.";
                               _dltRst= MessageBox.Show(msg, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                               
                                switch (_dltRst)
                                {
                                   
                                    case DialogResult.None:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.OK, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    case DialogResult.OK:
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DailyClose, gloAuditTrail.ActivityCategory.DailyCloseAlert, gloAuditTrail.ActivityType.OK, msg, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        break;
                                    default:
                                        break;
                                }

                                return;
                            }

                        }
                    }
                   

                    if (_dltRst.ToString() == "Yes")
                    {

                        if (tabSummary.SelectedTab.Tag.ToString() == "DailyCharges")
                        {
                            //#region " Daily Charges Close "

                            //DataTable dtDailyCharges = new DataTable();

                            //oParameters.Clear();
                            //oDB.Connect(false);
                            //oDB.Retrive("Rpt_ChargesTray", oParameters, out dtDailyCharges);
                            //oDB.Disconnect();
                            //oParameters.Clear();

                            //if (dtDailyCharges != null && dtDailyCharges.Rows.Count > 0)
                            //{
                            //    for (int i = 0; i <= dtDailyCharges.Rows.Count - 1; i++)
                            //    {
                            //        _TransID = Convert.ToInt64(dtDailyCharges.Rows[i]["nTransactionID"]);
                            //        if (_TransID != _LastTransID)
                            //        {
                            //            oDB.Connect(false);
                            //            oDB.Execute_Query("Update BL_Transaction_MST SET bIsDayUpdated=1 WHERE nTransactionID= " + _TransID + " ");
                            //        }
                            //        _LastTransID = Convert.ToInt64(dtDailyCharges.Rows[i]["nTransactionID"]);

                            //    }

                            //    MessageBox.Show("Updated", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}

                            //#endregion
                        }
                        else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
                        {
                            //#region " Daily Payment Close "

                            //DataTable dtDailyPayment = new DataTable();

                            //oParameters.Clear();
                            //oDB.Connect(false);
                            //oDB.Retrive("BL_SELECT_EOBDailyPayment", oParameters, out dtDailyPayment);
                            //oDB.Disconnect();
                            //oParameters.Clear();

                            //#endregion
                        }
                        
                        else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
                        {

                            #region " Daily Close "
                            //gloDatabaseLayer.DBLayer ODB2 = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);


                            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                            

                            for (int m = 0; m < trvMonths.Nodes.Count; m++)
                            {
                                if (trvMonths.Nodes[m].Checked == true)
                                {
                                    if (m == 0)
                                        _strDayClose = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(trvMonths.Nodes[m].Text));
                                    else
                                        _strDayClose = _strDayClose + "," + Convert.ToString(gloDateMaster.gloDate.DateAsNumber(trvMonths.Nodes[m].Text));
                                }
                            }
                            
                            #region "For Refresh Issue"

                            string _sStartDate = Convert.ToString(trvMonths.Nodes[_StIndx].Text);
                            string _sEndDate = Convert.ToString(trvMonths.Nodes[_EdIndx].Text);
                            Boolean _isDatesInBetween ;
                            _isDatesInBetween = gloReports.CheckForCloseDatesInBetween(Convert.ToDateTime(_sStartDate), Convert.ToDateTime(_sEndDate), _strDayClose);

                            if (_isDatesInBetween == true)
                            {
                                MessageBox.Show("The day cannot be closed until the previous days are closed. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                for (int m = 1; m < trvMonths.Nodes.Count; m++)
                                {
                                    trvMonths.Nodes[m].Checked = false;
                                }
                                trvMonths.Nodes[0].Checked = true;
                                return;
                            }

                            #endregion

                            Boolean _bResult = gloReports.SaveDailyCloseDates(_strDayClose);


                            FillDailyCloseDates();
                            //_isFormLoading = true;
                            FillDayClose();
                            #endregion
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
                Cursor.Current = Cursors.Default;
            }
        }
       
        #endregion


        #region "Tab Events "

        private void tabSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //_isFormLoading = false;
                string sClinicName = "";
                sClinicName = gloReports.getClinicName();
                this.Text = tabSummary.SelectedTab.Text;
                //crvRptViewCloseDay.DisplayToolbar = true;

                if (tabSummary.SelectedTab.Tag.ToString() == "DailyCharges")
                {
                    // btnCloseCharges.Text = "Update Charges";
                    tsb_btnDailyClose.Visible = false;
                    FillChargeReport(sClinicName);
                    //SelectCloseDates();
                }
                else if (tabSummary.SelectedTab.Tag.ToString() == "DailyPayment")
                {



                    // btnCloseCharges.Text = "Close Daily Payment";
                    tsb_btnDailyClose.Visible = false;
                    FillPaymentReport(sClinicName);
                    //SelectCloseDates();
                    //FillPaymentSortByCombo();

                }
                else if (tabSummary.SelectedTab.Tag.ToString() == "DailyClose")
                {
                    // btnCloseCharges.Text = "Close Charges";
                    tsb_btnDailyClose.Visible = true;
                    FillCloseReport(sClinicName);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }
 
        #endregion


        #region "--------------------------- Day Close -------------------------------------- "

        #region " FillMethods "

        private void FillDayClose()
        {
                DataTable dtLastCloseDt = null;
                Int64 _closeDate = 0;

                try
                {
                    dtLastCloseDt = gloReports.FillDayClose();
                    if (dtLastCloseDt != null && dtLastCloseDt.Rows.Count > 0)
                    {
                        lblLstClosedDt.Text = dtLastCloseDt.Rows[0]["nCloseDayDate"].ToString();
                    }

                    if (trvMonths.Nodes != null)
                    {
                        for (int i = 0; i < trvMonths.Nodes.Count; i++)
                        {
                            if (trvMonths.Nodes[i].Checked == true)
                            {
                                _closeDate = gloDateMaster.gloDate.DateAsNumber(trvMonths.Nodes[i].Text.ToString());
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
                {
                   
                    dtLastCloseDt.Dispose();
                }
        }

        #endregion


        #endregion



        private void FillDailyCloseDates()
        {   

            DataTable dtCloseDates = null;
            try
            {
                dtCloseDates = gloReports.FillDailyCloseDates();
                trvMonths.Nodes.Clear();
                if (dtCloseDates != null && dtCloseDates.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtCloseDates.Rows.Count - 1; i++)
                    {
                        DataRow dr = dtCloseDates.Rows[i];
                        TreeNode oNode = new TreeNode();
                        oNode.Tag = String.Format("{0:MM/dd/yyyy}", dr[0]);
                        oNode.Text = String.Format("{0:MM/dd/yyyy}", dr[0]);
                        trvMonths.Nodes.Add(oNode);
                        oNode = null;
                    }

                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                dtCloseDates.Dispose();
            }

        }


        private string get_DateRangeString()
        {
            string _strCloseDate = string.Empty;

            for (int m = 0; m < trvMonths.Nodes.Count; m++)
            {
                if (trvMonths.Nodes[m].Checked == true)
                {
                    if (m == 0)
                        _strCloseDate = trvMonths.Nodes[m].Text;
                    else
                        _strCloseDate = _strCloseDate + "," + trvMonths.Nodes[m].Text;
                }
            }
            return _strCloseDate;
        }

        private string Get_RemainingAmount_Alert(string _sCloseDate)
        {
            DataTable _dtRemaining = new DataTable();
            string _sMessage = string.Empty; 
            try
            {

                _dtRemaining = gloReports.Get_RemainingAmount_Payments(_sCloseDate);
                if (_dtRemaining != null && _dtRemaining.Rows.Count > 0)
                {
                    if (_dtRemaining.Rows.Count == 1)
                    {
                          int i = 0;
                          _sMessage = "There are payments for Close Date " + Convert.ToString(_dtRemaining.Rows[i]["CloseDate"]) + " that have not been completed. " + Environment.NewLine;                      
                        _sMessage = _sMessage + Environment.NewLine + "# Payments: " + Convert.ToString(_dtRemaining.Rows[i]["CheckCount"]) + Environment.NewLine;
                        _sMessage = _sMessage + "Total Remaining: $" + Convert.ToString(_dtRemaining.Rows[i]["Remaining"]) + Environment.NewLine;
                        if (_DailyCloseSetting == false)
                        {
                            _sMessage = _sMessage + "Continue with close of  " + Convert.ToString(_dtRemaining.Rows[i]["CloseDate"]) + "?";
                        }
                        else
                        {
                            _sMessage = _sMessage + Convert.ToString(_dtRemaining.Rows[i]["CloseDate"]) + " cannot be closed until all payments have been completed.";
                        }

                    }
                    else
                    {
                        if (_dtRemaining.Rows.Count > 15)
                        {
                            //If Payments Remaining is greater than 15
                            _sMessage = "There are payments that have not been completed" + Environment.NewLine;
                            for (int i = 0; i < 15; i++)
                            {
                                _sMessage = _sMessage + Convert.ToString(_dtRemaining.Rows[i]["CloseDate"]);
                                _sMessage = _sMessage + Environment.NewLine + "     # Payments: " + Convert.ToString(_dtRemaining.Rows[i]["CheckCount"]) + Environment.NewLine;
                                _sMessage = _sMessage + "     Total Remaining: $" + Convert.ToString(_dtRemaining.Rows[i]["Remaining"]) + Environment.NewLine;

                            }

                            _sMessage = _sMessage + "                      .               " + Environment.NewLine;
                            _sMessage = _sMessage + "                      .               " + Environment.NewLine;
                            _sMessage = _sMessage + "                      .               " + Environment.NewLine;

                            if (_DailyCloseSetting == false)
                            {
                                _sMessage = _sMessage + "Do you want to continue?";
                            }
                            else
                            {
                                _sMessage = _sMessage + "A day cannot be closed until its payments have been completed.";
                            }
                            //---
                        }
                        else
                        {

                            _sMessage = "There are payments that have not been completed" + Environment.NewLine;
                            for (int i = 0; i < _dtRemaining.Rows.Count; i++)
                            {
                                _sMessage = _sMessage + Convert.ToString(_dtRemaining.Rows[i]["CloseDate"]);
                                _sMessage = _sMessage + Environment.NewLine + "     # Payments: " + Convert.ToString(_dtRemaining.Rows[i]["CheckCount"]) + Environment.NewLine;
                                _sMessage = _sMessage + "     Total Remaining: $" + Convert.ToString(_dtRemaining.Rows[i]["Remaining"]) + Environment.NewLine;

                            }
                            if (_DailyCloseSetting == false)
                            {
                                _sMessage = _sMessage + "Do you want to continue?";
                            }
                            else
                            {
                                _sMessage = _sMessage + "A day cannot be closed until its payments have been completed.";
                            }

                        }
                      
                    }
                }
                else
                {
                    _sMessage = "OK";
                }
                
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);  }
            finally
            {
                if (_dtRemaining != null) { _dtRemaining.Dispose(); }
            }
            return _sMessage;
        }


    }
}