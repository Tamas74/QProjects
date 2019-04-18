using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloSettings;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Runtime.InteropServices;
using gloBilling; 



namespace gloBilling.Collections
{
    
    public partial class frmSSRSViewer : Form
    {
        string strReportServer = string.Empty;
        private ComboBox combo;
        string strReportFolder = string.Empty;
        string strReportProtocol = string.Empty;
        string strVirtualDir = string.Empty;
        string _reportName = string.Empty;
        string _UserName = string.Empty;
        string _reportTitle = string.Empty;
        string _conn = string.Empty;
        string _messageboxcaption = string.Empty;
        string _parameterName = string.Empty;
        string _ParameterValue = string.Empty;
        string strParameter = string.Empty;
        string reportParam = string.Empty;
      //  private bool _IsAllDatesValid = true;
        gloListControl.gloListControl oListControl = null;
      //  gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        bool _IsgloStreamReport;
        private Image _Img = null;

        System.Uri SSRSReportURL;
        List<Microsoft.Reporting.WinForms.ReportParameter> paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public frmSSRSViewer()
        {
            InitializeComponent();
        }

        public string reportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }  

       

        public string Conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

        public string reportTitle
        {
            get { return _reportTitle; }
            set { _reportTitle = value; }
        }

        public string parameterName
        {
            get { return _parameterName; }
            set { _parameterName = value; }
        }

        public string ParameterValue
        {
            get { return _ParameterValue; }
            set { _ParameterValue = value; }
        }

        public Image formIcon
        {
            get { return _Img; }
            set { _Img = value; }
        }

        public bool IsgloStreamReport
        {
            get { return _IsgloStreamReport; }
            set { _IsgloStreamReport = value; }
        }
        public bool  Close_Btn
        {
            get { return tls_btnExit.Enabled; }
            set { tls_btnExit.Enabled= value; }
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyIcon(IntPtr hIcon);
       private void frmSSRSViewer_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            object oValue = new object();
            cmbGroupBy.SelectedIndex = 0;
            mskEnddate.Text = dtpEndDate.Value.ToString("MMddyyyy") ;  
            try
            {
                if (_Img != null)
                {
                    IntPtr myIcon = ((Bitmap)(_Img)).GetHicon();
                    this.Icon = Icon.FromHandle(myIcon);
                    DestroyIcon(myIcon);
                }

                //CURRENT LOGGED IN USER NAME
                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    {_UserName = Convert.ToString(appSettings["UserName"]); }
                }

                
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageboxcaption = "gloPM"; ;
                    }
                }
                else
                { _messageboxcaption = "gloPM"; ; }



                //REPORT SERVER NAME
                GeneralSettings oSetting = new GeneralSettings(_conn);

                oSetting.GetSetting("ReportServer", out oValue);
                if (oValue != null)
                {
                    strReportServer = oValue.ToString();
                    oValue = null;
                }

                //REPORT FOLDER NAME
                oSetting.GetSetting("ReportFolder", out oValue);
                if (oValue != null)
                {
                    strReportFolder = oValue.ToString();
                    oValue = null;
                }

                //VIRTUAL DIRECTORY NAME
                oSetting.GetSetting("ReportVirtualDirectory", out oValue);
                if (oValue != null)
                {
                    strVirtualDir = oValue.ToString();
                    oValue = null;
                }

                oSetting.GetSetting("ReportProtocol", out oValue);
                if (oValue != null)
                {
                    strReportProtocol = oValue.ToString();
                    oValue = null;
                }

                if (strReportProtocol == "" ||  strReportServer == "" || strReportFolder == "" || strVirtualDir == "")
                {
                    MessageBox.Show("SSRS Settings not set. Set the Report Server settings and then deploy the reports.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    this.Text = _reportTitle;
                    SSRSReportURL = new Uri(strReportProtocol + "://" + strReportServer + "/" + strVirtualDir);
                    SSRSViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                    SSRSViewer.ServerReport.ReportServerUrl = SSRSReportURL;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SSRS Reporting Service is not available.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return;
                }


                //if (_IsgloStreamReport == true)
                //{
                //    SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
                //    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtStartDate", "02/04/2012", false));
                //    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtEndDate", "02/04/2012", false));
                //    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sActionCode", "1", false));
                //    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProviderID", "1", false));
                //    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sGroupBy", "1", false));
                //    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
                //    paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
                //    this.SSRSViewer.ServerReport.SetParameters(paramList);
                //    this.SSRSViewer.RefreshReport();
                //}
                
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("rsItemNotFound"))
                {
                    if (_reportTitle.Contains("report") || _reportTitle.Contains("Report"))
                    {
                        MessageBox.Show(_reportTitle + " is not available on the report server " + strReportServer + ".", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(_reportTitle + " Report is not available on the report server " + strReportServer + ".", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (ex.Message.Contains("The remote name could not be resolved"))
                {
                    MessageBox.Show("Report server is not available. Please check report server settings.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ex.Message.Contains("The Report Server Windows service 'ReportServer' is not running"))
                {
                    MessageBox.Show("SQL Server Reporting Service is not installed or Report Server is not running.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //_UserName = null;
                oValue = null;
                Cursor.Current = Cursors.Default;
            }
        }

        //CLINIC NAME
        private string getClinicName()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_conn);
            try
            {
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST");
                if (_Result.ToString() != "")
                { return _Result.ToString(); }
                else
                { return ""; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }
            finally
            {
                oDB.Dispose();                
            }
        }

        private void tls_btnExit_Click(object sender, EventArgs e)
        {
            SSRSViewer.Clear();
            SSRSViewer.Dispose(); 
            this.Close();
        }

        private void frmSSRSViewer_FormClosing(object sender, FormClosingEventArgs e)
        {

        }      
                

        private void dtpStartDate_CloseUp(object sender, EventArgs e)
        {
            // textBox1.Text = dateTimePicker1.Value.ToShortDateString();   
            mskStartDate.Text = dtpStartDate.Value.ToString("MMddyyyy");   
        }

        private void dtpEndDate_CloseUp(object sender, EventArgs e)
        {
            mskEnddate.Text = dtpEndDate.Value.ToString("MMddyyyy");  
        }

        private void btnBrowseMultiProvider_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch
                        {
                        }


                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(Conn  , gloListControl.gloListControlType.ReportProviders, true, 0);
                oListControl.ControlHeader = "Providers";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                //oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);

                for (int i = 0; i < cmbProvider.Items.Count; i++)
                {
                    cmbProvider.SelectedIndex = i;
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbProvider.SelectedValue), cmbProvider.Text);
                }
                if (cmbProvider.Items.Count > 0)
                    cmbProvider.SelectedIndex = 0;
               
                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.None;                   
                    Point Y = new Point(cmbProvider.Location.X , cmbProvider.Location.Y + 80) ;
                    oListControl.Location = Y;
                    oListControl.BringToFront();
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void oListControl_SelectedClick(object sender, EventArgs e)
        {
            try
            {
                switch (oListControl.ControlHeader.ToString().ToLower())
                {
                    case "providers":
                        {
                            //this.cmbProvider.SelectedIndexChanged -= new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
                           // cmbProvider.Items.Clear();
                            cmbProvider.DataSource = null;
                            cmbProvider.Items.Clear();
                            
                            DataTable dtReff = new DataTable();
                            DataColumn dcId = new DataColumn("ID");
                            DataColumn dcDescription = new DataColumn("Description");
                            dtReff.Columns.Add(dcId);
                            dtReff.Columns.Add(dcDescription);
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                                {
                                    DataRow drTemp = dtReff.NewRow();
                                    drTemp["ID"] = oListControl.SelectedItems[i].ID;
                                    drTemp["Description"] = oListControl.SelectedItems[i].Description;
                                    dtReff.Rows.Add(drTemp);
                                }
                            }
                            cmbProvider.DataSource = dtReff;
                            cmbProvider.ValueMember = dtReff.Columns["ID"].ColumnName;
                            cmbProvider.DisplayMember = dtReff.Columns["Description"].ColumnName;
                            if (dtReff != null && dtReff.Rows.Count > 0)
                                cmbProvider.DropDownHeight = (cmbProvider.Height - 2) * 5;
                            else
                                cmbProvider.DropDownHeight = 1;
                           
                           
                        }
                        break;
                    case "action code":
                        {
                            //this.cmbProvider.SelectedIndexChanged -= new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
                           // cmbActionCode.Items.Clear();
                            cmbActionCode.DataSource = null;
                            cmbActionCode.Items.Clear();
                            DataTable dtReff = new DataTable();
                            DataColumn dcId = new DataColumn("ID");
                            DataColumn dcDescription = new DataColumn("Description");
                            dtReff.Columns.Add(dcId);
                            dtReff.Columns.Add(dcDescription);
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                                {
                                    DataRow drTemp = dtReff.NewRow();
                                    drTemp["ID"] = oListControl.SelectedItems[i].Code;
                                    drTemp["Description"] =oListControl.SelectedItems[i].Code +"-"+ oListControl.SelectedItems[i].Description;
                                    dtReff.Rows.Add(drTemp);
                                }
                            }
                            cmbActionCode.DataSource = dtReff;
                            cmbActionCode.ValueMember = dtReff.Columns["ID"].ColumnName;
                            cmbActionCode.DisplayMember = dtReff.Columns["Description"].ColumnName;
                            if (dtReff != null && dtReff.Rows.Count > 0)
                                cmbActionCode.DropDownHeight = (cmbProvider.Height - 2) * 5;
                            else
                                cmbActionCode.DropDownHeight = 1;
                        }
                        break;

                }


            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            }

        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }


                }
                catch
                {
                }
              
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
          //  cmbProvider.Items.Clear();
            cmbProvider.DataSource = null;
            cmbProvider.Items.Clear();
            cmbProvider.Refresh();
            cmbProvider.DropDownHeight = 1;
        }

        private void btnBrowseMultiActionCode_Click(object sender, EventArgs e)
        {

            try
            {
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch
                        {
                        }


                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(Conn, gloListControl.gloListControlType.ReportAccountActionCode, true, 0);
                oListControl.ControlHeader = "Action Code";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
              
                this.Controls.Add(oListControl);

                for (int i = 0; i < cmbActionCode.Items.Count; i++)
                {
                    cmbActionCode.SelectedIndex = i;
                    oListControl.SelectedItems.Add(cmbActionCode.SelectedValue.ToString(), cmbActionCode.Text);
                }
                if (cmbActionCode.Items.Count > 0)
                    cmbActionCode.SelectedIndex = 0;

                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.None;
                    oListControl.Height = 300;
                    Point Y = new Point(cmbActionCode.Location.X, cmbActionCode.Location.Y + 80);
                    oListControl.Location = Y;
                    oListControl.BringToFront();
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        private void btnClearActionCode_Click(object sender, EventArgs e)
        {
           // cmbActionCode.Items.Clear();
            cmbActionCode.DataSource = null;
            cmbActionCode.Items.Clear();
            cmbActionCode.Refresh();
            cmbActionCode.DropDownHeight = 1;
           
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (mskStartDate.Text.Trim() != "/  /" && IsValidDate(mskStartDate.Text.Trim(), "mskStartDate") == false)
                {
                    MessageBox.Show("Please enter valid start date.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskStartDate.Focus();
                    mskStartDate.Select();
                }
                else
                    if (mskEnddate.MaskCompleted == false)
                    {
                        MessageBox.Show("Please enter the end date.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskEnddate.Focus();
                        mskEnddate.Select();
                    }
                    else if (IsValidDate(mskEnddate.Text.Trim(), "mskEndDate") == true)
                    {
                        if (mskStartDate.Text.Trim() != "/  /" && Convert.ToDateTime(mskStartDate.Text.Trim()) > Convert.ToDateTime(mskEnddate.Text.Trim()))
                            MessageBox.Show("Start date must be less than end date.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        generateReport();

                    }
            }
            catch (Exception ex)
            {//Added by kanchan on 20130606 to solve bug
                if (ex.Message.Contains("rsItemNotFound"))
                {
                    if (_reportTitle.Contains("report") || _reportTitle.Contains("Report"))
                    {
                        MessageBox.Show(_reportTitle + " is not available on the report server " + strReportServer + ".", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(_reportTitle + " Report is not available on the report server " + strReportServer + ".", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (ex.Message.Contains("The remote name could not be resolved"))
                {
                    MessageBox.Show("Report server is not available. Please check report server settings.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ex.Message.Contains("The Report Server Windows service 'ReportServer' is not running"))
                {
                    MessageBox.Show("SQL Server Reporting Service is not installed or Report Server is not running.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }



        private bool IsValidDate(object strDate, string name)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;                       
                    }
                    else
                    {
                        Success = false;
                        if (name == "mskStartDate")
                            MessageBox.Show("Please enter valid start date.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (name == "mskEndDate")
                            MessageBox.Show("Please enter valid end date.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (FormatException e)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.ToString(), false);
                e = null;
                Success = false; // If this line is reached, an exception was thrown
            }
            return Success;
        }


        private string GetActions()
        {
            string ActionCodes=string.Empty ; 
              if(cmbActionCode.Items.Count > 0)  
              {
                  for (int i = 0; i < cmbActionCode.Items.Count; i++)
                  {
                      cmbActionCode.SelectedIndex = i;
                      ActionCodes = ActionCodes +","+ cmbActionCode.SelectedValue.ToString();
                  }
                  cmbActionCode.SelectedIndex = 0;

              }
              return ActionCodes;
        }

        private string GetActionsName()
        {
            string ActionCodes = string.Empty;
            if (cmbActionCode.Items.Count > 0)
            {
                for (int i = 0; i < cmbActionCode.Items.Count; i++)
                {
                    cmbActionCode.SelectedIndex = i;
                    if (i == 0)
                        ActionCodes = cmbActionCode.Text.ToString();
                    else
                        ActionCodes = ActionCodes + " , " + cmbActionCode.Text.ToString();
                }
                cmbActionCode.SelectedIndex = 0;

            }
            return ActionCodes;
        }


        private string GetProviders()
        {
            string ProviderIds = string.Empty;
            if (cmbProvider.Items.Count > 0)
            {
                for (int i = 0; i < cmbProvider.Items.Count; i++)
                {
                    cmbProvider.SelectedIndex = i;
                    if(i==0)
                        ProviderIds = cmbProvider.SelectedValue.ToString();
                    else
                    ProviderIds = ProviderIds + "," + cmbProvider.SelectedValue.ToString();
                }
                cmbProvider.SelectedIndex = 0;

            }
            return ProviderIds;
        }

        private string GetProvidersName()
        {
            string ProviderIds = string.Empty;
            if (cmbProvider.Items.Count > 0)
            {
                for (int i = 0; i < cmbProvider.Items.Count; i++)
                {
                    cmbProvider.SelectedIndex = i;
                    if (i == 0)
                        ProviderIds = cmbProvider.Text.ToString();
                    else
                        ProviderIds = ProviderIds + " , " + cmbProvider.Text.ToString();
                }
                cmbProvider.SelectedIndex = 0;

            }
            return ProviderIds;
        }



        private void generateReport()
        {
            SSRSViewer.ServerReport.ReportPath = "/" + strReportFolder + "/" + _reportName;
            paramList = new List<Microsoft.Reporting.WinForms.ReportParameter>();

            if (mskStartDate.Text.Trim() == "/  /")
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtStartDate", new string[] { null }, false));
            else if (mskStartDate.Text.Trim() != "/  /" && IsValidDate(mskStartDate.Text.Trim(), "mskStartDate") == false)
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtStartDate",Convert.ToDateTime(mskEnddate.Text).AddDays(1).ToShortDateString(), false));
            else
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtStartDate", mskStartDate.Text, false));
                


            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("dtEndDate", mskEnddate.Text, false));

            if(cmbActionCode.Items.Count == 0)  
             paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sActionCode", new string[] { null }, false));
            else
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sActionCode", GetActions(), false));

            if (cmbProvider.Items.Count == 0)
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProviderID", new string[] { null }, false));
            else
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProviderID", GetProviders(), false));


            if (cmbProvider.Items.Count == 0)
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sParameterLabel", new string[] { null }, false));
            else
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sParameterLabel", GetProvidersName(), false));


            if(cmbActionCode.Items.Count == 0)
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sSheduleParameter", new string[] { null }, false));
            else
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sSheduleParameter", GetActionsName(), false));


           


            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("sGroupBy", cmbGroupBy.Text, false));           

            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("suser", _UserName, false));
            paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("Practice", getClinicName(), false));
            if (chkExcludeReserves.Checked)
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ExcludeAvailableReserves", Convert.ToString(true), false));
            else
                paramList.Add(new Microsoft.Reporting.WinForms.ReportParameter("ExcludeAvailableReserves", Convert.ToString(false), false));
            this.SSRSViewer.ServerReport.SetParameters(paramList);
            this.SSRSViewer.RefreshReport();
        }

        private void cmbProvider_MouseMove(object sender, MouseEventArgs e)
        {
           
            try
            {

                combo = (ComboBox)sender;

                if (cmbProvider.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbProvider.Items[cmbProvider.SelectedIndex])["Description"]), cmbProvider) >= cmbProvider.DropDownWidth - 20)
                    {
                        tooltip_Billing.SetToolTip(cmbProvider, Convert.ToString(((DataRowView)cmbProvider.Items[cmbProvider.SelectedIndex])["Description"]));
                    }
                    else
                    {
                        this.tooltip_Billing.Hide(cmbProvider);
                    }
                }
                else
                {
                    this.tooltip_Billing.Hide(cmbProvider);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        private void cmbActionCode_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {

                combo = (ComboBox)sender;

                if (cmbProvider.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbProvider.Items[cmbProvider.SelectedIndex])["Description"]), cmbProvider) >= cmbProvider.DropDownWidth - 20)
                    {
                        tooltip_Billing.SetToolTip(cmbProvider, Convert.ToString(((DataRowView)cmbProvider.Items[cmbProvider.SelectedIndex])["Description"]));
                    }
                    else
                    {
                        this.tooltip_Billing.Hide(cmbProvider);
                    }
                }
                else
                {
                    this.tooltip_Billing.Hide(cmbProvider);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }
    }

   
}
