using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using C1.Win.C1FlexGrid;
namespace gloReports
{
   
    public partial class frmInterfacesMessageErrorReport : Form
    {
       
        #region "Properties"
        public DateTime dtSpecificDateReport
        {
            get;
            set;
        }       
        #endregion

        # region "Global Variables"
        Int32 iStartIndex = 0;
        Int32 iEndIndex = 0;
        Int32 iPageNumber = 1;
        Int32 iTotalPages = 1;
        Int32 iLogCount = 0;
        Int32 iPageSize = 0;
        String strStatus = "All";
        String strConnectionString = String.Empty;
        String gstrMessageBoxCaption = String.Empty;
        DataTable dtMessageLog = null;
        //global variables used to designate columns in C1Flex grid for HL7 service
        int mColIndex_MessageID = 0;
        int mColIndex_DateTimeStamp = 1;
        int mColIndex_PatientCode = 2;
        int mColIndex_Patient = 3;
        int mColIndex_EventName = 4;
        int mColIndex_SegmentName = 5;
        int mColIndex_Description = 6;
        int mColIndex_Status = 7;
        int mColIndex_TransactionType = 8;
        int mColIndex_ExternalCode = 9;
        int mColIndex_ExternalType = 10;

        //global variables used to designate columns for other services
        int ColIndex_DateTimeStamp = 0;
        int ColIndex_MessageName = 1;
        int ColIndex_MachineName = 2;
        int ColIndex_PatientCode = 3;
        int ColIndex_Patient = 4;
        int ColIndex_Status = 5;

        bool _isFormLoaded = false;
        bool _isFieldChange = false;
        public bool _isDeleteRecord = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion "Global Variables"

        #region "Constructor & Distructor"
        public frmInterfacesMessageErrorReport()
        {
            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    strConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
            }
            strConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            {
                gstrMessageBoxCaption = "gloEMR";
            }
            InitializeComponent();
            // iPageSize = _intPagesize;
            // iPageSize = 50;// getMessageQueuePageSize();
        }
       
        ~frmInterfacesMessageErrorReport()
        {
            this.Dispose();
        }
        #endregion "Constructor & Destructor"

        #region "Form Events"
        //Load Event
        private void frmHL7_MessageQueue_Load(object sender, EventArgs e)
        {
            _isFormLoaded = false;
            try
            {
              
                    if ((dtSpecificDateReport.ToString() != "#12:00:00 AM#") && (dtSpecificDateReport.ToString() != "1/1/0001 12:00:00 AM"))
                    {
                        dtpToDate.Text = dtSpecificDateReport.ToShortDateString();
                        dtpFromDate.Text = dtSpecificDateReport.ToShortDateString();
                        ChkUseDateRange.Checked = true;
                        chkTo.Checked = true;
                        dtpToDate.Enabled = true;
                    }
                    else
                    {
                        dtpToDate.Text = DateTime.Now.ToShortDateString();
                        dtpFromDate.Text = DateTime.Now.ToShortDateString();
                    }
               
                

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports,
                gloAuditTrail.ActivityCategory.ViewInterfaceReport,
                gloAuditTrail.ActivityType.View, "Viewed interface log report.", 0, 0, 0,
                gloAuditTrail.ActivityOutCome.Success, (gstrMessageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
               
                //LoadServiceName();
                AddPagingSize();

                //cmbStatus.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 4;
                strStatus = cmbStatus.Text;
                cmbServiceName.SelectedIndex = 0;

                PopulateHL7Clients();
                //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                _isFormLoaded = true;
               // pnlExport.Visible = false;

            }
            catch (Exception)
            {
                _isFormLoaded = true;

            }
        }
        #endregion "Form Events"

        #region "Form Control Events"

        //Previous Button Click
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                iPageNumber--;

                if (iPageNumber < 1)
                {
                    iPageNumber = 1;
                    return;
                }

                iStartIndex = iStartIndex - iPageSize;
                iEndIndex = iEndIndex - iPageSize;

                if (iStartIndex < 0)
                {
                    iStartIndex = 0;
                }

                if (iEndIndex < iPageSize)
                {
                    iEndIndex = iPageSize;
                }

                //PopulateDatagrid();
                BindGrid();

                if (iLogCount > 0)
                {
                    //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                }
                else
                {
                    //  lblPage.Text = "";
                }

            }
            catch //(Exception ex)
            {
                // Interaction.MsgBox(Err().Description);
            }
        }

        //Next Button Click
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                iPageNumber++;

                if (iPageNumber > iTotalPages)
                {
                    iPageNumber = iTotalPages;
                    return;
                }

                iStartIndex = iStartIndex + iPageSize;
                iEndIndex = iEndIndex + iPageSize;

                if (iStartIndex > (iLogCount - iPageSize))
                {
                    iStartIndex = iLogCount - iPageSize;
                }

                if (iEndIndex > iLogCount)
                {
                    iEndIndex = iLogCount;
                }

                // PopulateDatagrid();
                BindGrid();

                if (iLogCount > 0)
                {
                    //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                }
                else
                {
                    //lblPage.Text = "";
                }

            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                //Interaction.MsgBox(Err().Description);
            }

        }

        //Refresh Button Click
        private void tlpRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            ChkUseDateRange.Checked = false;
            chkTo.Checked = false;
            dtpToDate.Text = System.DateTime.Now.ToShortDateString();
            dtpFromDate.Text = System.DateTime.Now.ToShortDateString();
            cmbStatus.SelectedIndex = 4;
            _isFieldChange = false;
            ProcessMessageLog(iPageSize, strConnectionString);
            BindGrid();
            //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
            lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
        }

        // Close Button Click
        private void tlpClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbServiceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessMessageLog(iPageSize, strConnectionString);
            if (_isFormLoaded)
            {
                _isFieldChange = true; 
                BindGrid();
            }
            lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;

        }
       

        private void txtPatientCode_TextChanged(object sender, EventArgs e)
        {
            _isFieldChange = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if ((ChkUseDateRange.Checked) && (chkTo.Checked))
            {
                if (DateTime.Compare(Convert.ToDateTime(dtpFromDate.Value.ToShortDateString()), Convert.ToDateTime(dtpToDate.Value.ToShortDateString())) == 1)
                {
                    if (dtpToDate.Value < dtpFromDate.Value)
                    {
                        MessageBox.Show("'To' date should not be less than 'From' date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        iTotalPages = 1;
                        dtpToDate.Focus();
                        BindGrid();
                        SetData();
                        return;
                    }
                }
            }
            _isFieldChange = false;
            ProcessMessageLog(iPageSize, strConnectionString);
            BindGrid();
            SetData();
            if (iLogCount <= 0)
            {
                MessageBox.Show("No message found.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isFormLoaded)
            {
                strStatus = cmbStatus.Text;
                _isFieldChange = true;
            }
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                iPageNumber--;

                if (iPageNumber < 1)
                {
                    iPageNumber = 1;
                    return;
                }

                iStartIndex = iStartIndex - iPageSize;
                iEndIndex = iEndIndex - iPageSize;

                if (iStartIndex < 0)
                {
                    iStartIndex = 0;
                }

                if (iEndIndex < iPageSize)
                {
                    iEndIndex = iPageSize;
                }

                //PopulateDatagrid();
                BindGrid();

                if (iLogCount > 0)
                {
                    //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                }
                else
                {
                    //lblPage.Text = "";
                }

            }
            catch (Exception)
            {
                // Interaction.MsgBox(Err().Description);
            }
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            SetData();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            try
            {
                iPageNumber++;

                if (iPageNumber > iTotalPages)
                {
                    iPageNumber = iTotalPages;
                    return;
                }

                iStartIndex = iStartIndex + iPageSize;
                iEndIndex = iEndIndex + iPageSize;

                if (iStartIndex > (iLogCount - iPageSize))
                {
                    iStartIndex = iLogCount - iPageSize;
                }

                if (iEndIndex > iLogCount)
                {
                    iEndIndex = iLogCount;
                }

                // PopulateDatagrid();
                BindGrid();

                if (iLogCount > 0)
                {
                    //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                }
                else
                {
                    //lblPage.Text = "";
                    //lblSelected.Text = "";
                }

            }
            catch (Exception)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                //Interaction.MsgBox(Err().Description);
            }
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            try
            {
                if (iPageNumber != iTotalPages)
                {
                    iPageNumber = iTotalPages;
                    iStartIndex = iLogCount - iPageSize;
                    iEndIndex = iLogCount;
                    //PopulateDatagrid();
                    BindGrid();

                    if (iLogCount > 0)
                    {
                        //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                        lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                        iPageNumber = iTotalPages;
                    }
                    else
                    {
                        //lblPage.Text = "";
                        //lblSelected.Text = "";
                    }
                }
            }
            catch (Exception)
            {
                // Interaction.MsgBox(Err().Description);
            }
        }

        private void ChkUseDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkUseDateRange.Checked)
            {
                dtpFromDate.Enabled = true;
            }
            else
            {
                dtpFromDate.Enabled = false;
            }
            _isFieldChange = true;
        }

        private void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isFormLoaded)
            {   //Assign page size value from combo box
                _isFieldChange = true;
                iPageSize = Convert.ToInt32(cmbPageSize.Text);
                btnReport_Click(null, null);
            }

        }
    
        private void C1Flex_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {   //getting column name selected by click on header of grid
            //C1Flex.AutoSizeRows(1, mColIndex_Patient, C1Flex.Rows.Count - 1, C1Flex.Cols.Count - 1, 10, C1.Win.C1FlexGrid.AutoSizeFlags.None);
            //C1Flex.AutoSizeCols(0, mColIndex_MessageID, C1Flex.Rows.Count - 1, mColIndex_MessageID + 1, 4, C1.Win.C1FlexGrid.AutoSizeFlags.None);
     
            lblSearch.Text = Convert.ToString(C1Flex.GetData(0, e.Col));
            C1Flex.Col = e.Col;
            txtSearch.Text = "";
        }

        /// <summary>
        /// code to generate excel file by exporting data from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlpExport_Click(object sender, EventArgs e)
        {
            string _strPathName = string.Empty;
            try
            {
                if (_isFieldChange)
                {
                    MessageBox.Show("Click on ‘View Report’ to get latest report.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReport.Focus();
                    return;
                }
                //btnReport_Click(null, null);
                if ((C1Flex.Rows.Count == 0) || (C1Flex.Rows.Count == 1))
                {
                    return;
                }
                FrmExportData objExport = new FrmExportData(iTotalPages);
                objExport.ShowDialog(this);
                
                //rd_CurrentPage.Checked = true;
                //txtPageRange.Text = string.Empty;
                pnlTop.Enabled = false;
                tlpViewReport.Enabled = false;
                pnlTop.Enabled = false;
                C1Flex.Enabled = false;
               // pnlExport.Visible = true;

                if (objExport.Export)
                {
                    #region "select file"

                    //FolderBrowserDialog op = new FolderBrowserDialog();//select the path where user want to store.
                    //op.ShowDialog(this);
                    //if ((op.SelectedPath == string.Empty) || (op.SelectedPath == null))
                    //{
                    //    pnlTop.Enabled = true;
                    //    tlpViewReport.Enabled = true;
                    //    pnlTop.Enabled = true;
                    //    C1Flex.Enabled = true;
                    //    return;
                    //}
                    //else
                    //{
                    //    _strPathName = System.IO.Path.Combine(op.SelectedPath, "InterfaceReport-" + System.DateTime.Now.ToString("MMddyyyyhhmmss") + ".xls");
                    //}
                    //try
                    //{
                    //    if (System.IO.File.Exists(_strPathName))
                    //    {
                    //        System.IO.File.Delete(_strPathName);
                    //    }

                    //}
                    //catch (Exception)
                    //{
                    //    pnlTop.Enabled = true;
                    //    tlpViewReport.Enabled = true;
                    //    pnlTop.Enabled = true;
                    //    C1Flex.Enabled = true;
                    //    MessageBox.Show("This file is currently used by another person. Please close file and retry.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    #endregion "select file"
                   _strPathName= objExport.FilePath;
                    #region "export record to excel file"
                    try
                    {
                        Application.DoEvents();
                        pnlregistration.Visible = true;
                        pnlregistration.BringToFront();
                        this.Cursor = Cursors.WaitCursor;
                        if (iTotalPages >= 1)//check if there are more tab pages
                        {
                            Application.DoEvents();

                            if (objExport.CurrentPage)//check if Current page is selected
                            {
                                C1Flex.SaveExcel(_strPathName, "Page 1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
                            }
                            else if (objExport.SelectAll)//check if all page is selected
                            {
                                iStartIndex = 0;
                                iEndIndex = iPageSize;
                                for (int i = 1; i <= iTotalPages; i++)
                                {
                                    ExportData(i, _strPathName);
                                }
                                iStartIndex = iStartIndex - iPageSize;
                                iEndIndex = iEndIndex - iPageSize;
                                BindGrid();
                            }
                            else
                            {
                                Int32 _IEndIndex = 0;
                                Int32 _IStartIndex = 0;

                                String[] iRange = objExport.PageRange.Split(',');// txtPageRange.Text.Split(',');
                                String[] strRange;

                               for (Int32 j = 0; j < iRange.Length; j++)
                                {
                                    String sSplit = iRange[j].ToString();
                                    if ((sSplit.Contains("-")))
                                    {
                                        strRange = iRange[j].Split('-');

                                        _IStartIndex = Convert.ToInt32(strRange[0]);
                                        _IEndIndex = Convert.ToInt32(strRange[1]);

                                        if (_IEndIndex > iTotalPages)
                                        {
                                            _IEndIndex = iTotalPages;
                                        }
                                        if (_IStartIndex > iTotalPages)
                                        {
                                            this.Cursor = Cursors.Default;
                                            pnlregistration.Visible = false;
                                            return;
                                        }
                                        for (int i = _IStartIndex; i <= _IEndIndex; i++)
                                        {
                                            ExportData(_IStartIndex, _strPathName);
                                            _IStartIndex++;
                                        }

                                    }
                                    else
                                    {
                                        _IStartIndex = Convert.ToInt32(sSplit);
                                        if (_IStartIndex <= iTotalPages)
                                        {
                                            ExportData(_IStartIndex, _strPathName);
                                        }
                                        else
                                        {
                                            this.Cursor = Cursors.Default;
                                           // txtPageRange.Focus();
                                            pnlregistration.Visible = false;
                                            return;
                                        }
                                    }

                                }
                            }

                            pnlregistration.Visible = false;
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("The Interface report file exported successfully." + "\n" + "(Note: Data file path: " + _strPathName + ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion "export record to excel file"
             
                }
                objExport.Dispose();
                objExport = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                pnlTop.Enabled = true;
                tlpViewReport.Enabled = true;
                pnlTop.Enabled = true;
                C1Flex.Enabled = true;
            }
        }

        private void tlpDeleteLog_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (iLogCount > 0)
                {
                    frmDeleteOptions frmDel = new frmDeleteOptions(strConnectionString);
                    frmDel.InterfaceService = cmbServiceName.Text;
                    frmDel.ShowDialog(this);
                    if (frmDel.isDeleteRecord)
                    {
                        ProcessMessageLog(iPageSize, strConnectionString);
                        BindGrid();
                        SetData();
                    }
                    frmDel.Dispose();
                    frmDel = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion "Form Control Events"

        #region "User Defined Functions"
        /// <summary>
        /// Function to retrieve message log records.
        /// </summary>
        /// <returns>Data Table</returns>
        private DataTable GetMessageLogRecords()
        {
            gloDatabaseLayer.DBLayer oDbLayer = null;
            string strQuery = String.Empty;
            DataTable dtHl7MessageLog = null;
            String sServiceName = String.Empty;
            String sHL7client = String.Empty;
            String _strFilter = getFilterCriteria();
            if (_strFilter.Length > 0)
                _strFilter = " where " + _strFilter;
            //if (_strFilter.Length > 0)
            //    strQuery += " AND  (" + _strFilter + ")";
            try
            {
                if ((strConnectionString != null) && (strConnectionString != String.Empty))
                {
                    oDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                    oDbLayer.Connect(false);
                    sServiceName = cmbServiceName.SelectedItem.ToString();
                    sHL7client = Convert.ToString( cmbHL7Clients.SelectedValue);
                    if (cmbServiceName.SelectedIndex == 0)
                    {

                        strQuery = @"With tbl_HL7MsgLog
                                   As
                                  (
                                    SELECT MessageID as [Message ID],DateTimeProcessed as [Datetime Processed],ISNULL(PatientName,'') AS [Patient Name],ISNULL(PatientCode,'') AS [Patient Code]
                                    ,ISNULL(EventName, '') AS [Event Name],ISNULL(SegmentName,'') AS [Segment Name],ISNULL(Description,'') as [Description],
                                    Status as [Status],ISNULL(TransactionType,'') AS [Transaction Type],ISNULL(ExternalCode,'') AS [External Code],ISNULL(ExternalCodeType,'') AS [External Type]
                                    FROM dbo.HL7_MessageLog";
                        //if ((strStatus == "All") && (ChkUseDateRange.Checked))
                        //{
                        //    strQuery = strQuery + " WHERE CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value.ToShortDateString() + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        //}

                        //else if ((strStatus == "All") && (ChkUseDateRange.Checked == false))
                        //{
                        //    strQuery = strQuery + "";
                        //}
                        //else if ((strStatus != "All") && (ChkUseDateRange.Checked))
                        //{
                        //    strQuery = strQuery + " WHERE HL7_MessageLog.Status='" + strStatus + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        //}
                        //else
                        //{
                        //    strQuery = strQuery + " WHERE  HL7_MessageLog.Status='" + strStatus + "'";
                        //}
                        #region "When Status=All"
                        if ((strStatus == "All") && (ChkUseDateRange.Checked) && (chkTo.Checked))
                        {
                            strQuery = strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) >= '" + dtpFromDate.Value.ToShortDateString() + "' AND CONVERT(DATE, DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus == "All") && ((ChkUseDateRange.Checked) == false) && (chkTo.Checked))
                        {
                            strQuery = strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }

                        else if ((strStatus == "All") && (ChkUseDateRange.Checked == false) && ((chkTo.Checked) == false))
                        {
                            strQuery = strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) = '" + System.DateTime.Now.ToShortDateString() + "'";
                        }
                        else if ((strStatus == "All") && (ChkUseDateRange.Checked) && ((chkTo.Checked) == false))
                        {
                            strQuery = strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) >= '" + dtpFromDate.Value.ToShortDateString() + "'";
                        }
                        #endregion "When Status=All"

                        #region "When Status=Error/Failed"
                        else if ((strStatus == "Error/Failed") && (ChkUseDateRange.Checked) && (chkTo.Checked))
                        {
                            strQuery = strQuery + " WHERE Status <>'Success' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus == "Error/Failed") && ((ChkUseDateRange.Checked) == false) && (chkTo.Checked))
                        {
                            strQuery = strQuery + " WHERE Status <>'Success' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus == "Error/Failed") && ((ChkUseDateRange.Checked) == false) && ((chkTo.Checked) == false))
                        {
                            strQuery = strQuery + " WHERE  Status <>'Success' AND  CONVERT(DATE, DatetimeProcessed) = '" + System.DateTime.Now.ToShortDateString() + "'";
                        }
                        else if ((strStatus == "Error/Failed") && (ChkUseDateRange.Checked) && ((chkTo.Checked) == false))
                        {
                            strQuery = strQuery + " WHERE Status <>'Success'  AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "'";
                        }
                        #endregion "When Status=Error/Failed"

                        #region "When Status is not All and Error/Failed
                        else if ((strStatus != "All") && (ChkUseDateRange.Checked) && (chkTo.Checked))
                        {
                            strQuery = strQuery + " WHERE Status='" + strStatus + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus != "All") && ((ChkUseDateRange.Checked) == false) && (chkTo.Checked))
                        {
                            strQuery = strQuery + " WHERE Status='" + strStatus + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus != "All") && ((ChkUseDateRange.Checked) == false) && ((chkTo.Checked) == false))
                        {
                            strQuery = strQuery + " WHERE  Status='" + strStatus + "' AND  CONVERT(DATE, DatetimeProcessed) = '" + System.DateTime.Now.ToShortDateString() + "'";
                        }
                        else if ((strStatus != "All") && (ChkUseDateRange.Checked) && ((chkTo.Checked) == false))
                        {
                            strQuery = strQuery + " WHERE Status='" + strStatus + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "'";
                        }
                        #endregion "When Status is not All and Error/Failed

                        if (! String.IsNullOrEmpty(sHL7client) )
                        {
                            strQuery = strQuery + " AND HL7Client ='" + sHL7client  + "'";
                        }
                        strQuery = strQuery + ") SELECT [Message ID],[Datetime Processed],[Patient Code],[Patient Name],[Event Name],[Segment Name],[Description],[Status],[Transaction Type],[External Code],[External Type] FROM (Select ROW_NUMBER() over(order by [Datetime Processed] DESC) AS ROWID, [Message ID],[Datetime Processed],[Patient Code],[Patient Name],[Event Name],[Segment Name],[Description],[Status],[Transaction Type],[External Code],[External Type] from tbl_Hl7MsgLog  " + _strFilter + " ) MyTable where RowId  >" + iStartIndex.ToString() + @" 
                                   And RowId <=" + iEndIndex.ToString() + "";//order by DateTimeStamp desc
                    }
                    else
                    {
                        //                           strQuery = @" SELECT  dbo.Patient.nPatientID AS PatientId, dbo.Gl_Messagequeue.dtDateTimeStamp AS DateTimeStamp,    
                        //                                       CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS 'MessageName', dbo.Gl_Messagequeue.sMachineName AS MachineName,    
                        //                                     ISNULL(dbo.Patient.sPatientCode,'') AS PatientCode, ISNULL(dbo.Patient.sFirstName,'')+' '+ISNULL(dbo.Patient.sMiddleName,'')+' '+ ISNULL(dbo.Patient.sLastName,'') as Patient,    
                        //                                     CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as 'Status',CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS Date   
                        //                                     FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='"+ sServiceName + "'";

                        strQuery = @"With tbl_GlMsgLog
                                    AS
                                    (
                                    SELECT  
			                                    ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
			                                    ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS DateTimeStamp,    
			                                    CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS 'MessageName'
			                                    , dbo.Gl_Messagequeue.sMachineName AS MachineName,    
			                                    ISNULL(dbo.Patient.sPatientCode,'') AS PatientCode, 
			                                    dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as Patient,  

			                                    CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                    CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
			                                    FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + @"'
                                        			
                                    ) 
                                    SELECT DateTimeStamp,MessageName,MachineName,PatientCode,Patient,[Status] FROM(Select  ROW_NUMBER() over(order by [DateTimeStamp] DESC) AS ROWID, DateTimeStamp,MessageName,MachineName,PatientCode,Patient,[Status] from tbl_GlMsgLog " + _strFilter + " )MyTable where RowId  >" + iStartIndex.ToString() + @" 
                                        And RowId <=" + iEndIndex.ToString() + ""; // order by DateTimeStamp desc";
                    }
                    //String _strSearhString = txtSearch.Text.Trim();

                }
                oDbLayer.Retrive_Query(strQuery, out dtHl7MessageLog);
                return dtHl7MessageLog;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDbLayer != null)
                {
                    oDbLayer.Disconnect();
                    oDbLayer.Dispose();
                    oDbLayer = null;
                }
            }

        }
        private string getFilterCriteria()
        {
            List<String> lstHL7ColumnList = new List<string>(){
                "[Message ID]",
                "[Datetime Processed]",
                "[Event Name]",
                "[Transaction Type]",
                "[Patient Code]",
                "[Patient Name]",
                "[Status]",
                "[Segment Name]",
                "[Description]",
                "[External Code]",
                "[External Type]"
            };
            List<String> lstGeneralColumnList = new List<string>()
            {
                "DateTimeStamp",
                "MessageName",
                "MachineName",
                "PatientCode",
                "Patient",
                "Status"

            };
            String _strSearhString = txtSearch.Text.Trim();
            String _strFilter = String.Empty;
            try
            {
                if (_strSearhString.Length > 0)
                {
                    _strSearhString = _strSearhString.Replace("'", "");
                    _strSearhString = _strSearhString.Replace("[", "");
                    _strSearhString = _strSearhString.Replace("*", "");
                    _strSearhString = _strSearhString.Replace("%", "");
                    _strSearhString = _strSearhString.Replace("]", "");

                    _strFilter = String.Empty;
                    if (_strSearhString.Length > 0)
                    {
                        if (cmbServiceName.SelectedIndex == 0)
                        {
                            foreach (String strCol in lstHL7ColumnList)
                            {

                                if (strCol == Convert.ToString("[" + lblSearch.Text + "]"))
                                {
                                    if (strCol == "[Datetime Processed]")
                                    {
                                        if (_strFilter.Length == 0)
                                            _strFilter = "CONVERT(VARCHAR," + strCol + ",101) LIKE  '%" + _strSearhString + "%'";
                                        //else
                                        //    _strFilter += " Or " + strCol + " Like  '%" + _strSearhString + "%'";
                                    }
                                    else
                                    {
                                        if (_strFilter.Length == 0)
                                            _strFilter = strCol + " Like  '%" + _strSearhString + "%'";
                                        else
                                            _strFilter += " Or " + strCol + " Like  '%" + _strSearhString + "%'";
                                    }

                                }
                            }
                        }
                        else
                        {
                            foreach (String strCol in lstGeneralColumnList)
                            {
                                if (_strFilter.Length == 0)
                                    _strFilter = strCol + " Like  '%" + _strSearhString + "%'";
                                else
                                    _strFilter += " Or " + strCol + " Like  '%" + _strSearhString + "%'";

                            }
                        }
                    }
                }
                return _strFilter;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return String.Empty;
            }

        }

        /// <summary>
        /// Function retrieves Count of HL7 message Queue records.
        /// </summary>
        /// <returns>Int32</returns>
        private Int32 GetHL7MessaLogCount()
        {
            gloDatabaseLayer.DBLayer _oDBLayer = null;
            string _strQuery = String.Empty;
            string sServiceName = String.Empty;
            Int32 _CntRecords = 0;
            try
            {
                if ((strConnectionString != null) && (strConnectionString != String.Empty))
                {

                    _oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                    _oDBLayer.Connect(false);
                    if (cmbServiceName.SelectedIndex == 0)
                    {
                        //                        _strQuery = @"With tbl_HL7MsgLog
                        //                                    As
                        //                                    (
                        //                                    SELECT ROW_NUMBER() over(order by HL7_MessageLog.MessageID ASC) AS ROWID,   HL7_MessageLog.MessageID as MessageID, HL7_MessageLog.DatetimeProcessed as DateTimeStamp , 
                        //                                    Case HL7_MessageLog.EventName when 'A08' then 'Patient Modification' when 'A04' then 'Patient Registration'  
                        //                                    when 'O01' then 'Lab Order' when 'P03' then 'Charges' when 'S12' then 'Appointment Registration'  
                        //                                    when 'S13' then 'Appointment Modification' when 'S15' then 'Appointment Canceled/Deleted'  
                        //                                    when 'V04' then 'Immunization Update' when 'A28' then 'Patient Information' when 'V04IR' then 'Immunization Registry Update' when 'M02' then 'Referral inbound'
                        //                                    when 'T02' then 'Lab Document inbound' when 'R01' then 'lab Result inbound' end as 'EventName', HL7_MessageLog.TransactionType AS TransactionType, 
                        //                                    isnull( HL7_MessageLog.PatientCode,'') as  'PatientCode' , ISNULL(HL7_MessageLog.PatientName,'') AS 'Patient',  
                        //                                    HL7_MessageLog.Status as 'Status', 
                        //                                    HL7_MessageLog.SegmentName as 'SegmentName' FROM HL7_MessageLog";  
                        //  ON Patient.sPatientCode = HL7_MessageLog.PatientCode"; 
                        //)SELECT COUNT(*) FROM tbl_HL7MsgLog ";
                        _strQuery = @"With tbl_HL7MsgLog
                                   As
                                  (
                                    SELECT MessageID as [Message ID],DateTimeProcessed as [Datetime Processed],ISNULL(PatientName,'') AS [Patient Name],ISNULL(PatientCode,'') AS [Patient Code]
                                    ,ISNULL(EventName, '') AS [Event Name],ISNULL(SegmentName,'') AS [Segment Name],ISNULL(Description,'') as [Description],
                                    Status as [Status],ISNULL(TransactionType,'') AS [Transaction Type],ISNULL(ExternalCode,'') AS [External Code],ISNULL(ExternalCodeType,'') AS [External Type]
                                    FROM dbo.HL7_MessageLog";
                        #region "When status=All"
                        if ((strStatus == "All") && (ChkUseDateRange.Checked) && (chkTo.Checked))
                        {
                            _strQuery = _strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) >= '" + dtpFromDate.Value.ToShortDateString() + "' AND CONVERT(DATE, DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus == "All") && ((ChkUseDateRange.Checked) == false) && (chkTo.Checked))
                        {
                            _strQuery = _strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }

                        else if ((strStatus == "All") && (ChkUseDateRange.Checked == false) && ((chkTo.Checked) == false))
                        {
                            _strQuery = _strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) = '" + System.DateTime.Now.ToShortDateString() + "'";
                        }
                        else if ((strStatus == "All") && (ChkUseDateRange.Checked) && ((chkTo.Checked) == false))
                        {
                            _strQuery = _strQuery + " WHERE CONVERT(DATE, DatetimeProcessed) >= '" + dtpFromDate.Value.ToShortDateString() + "'";
                        }
                        #endregion "When status=All"

                        #region "When status=Error/Failed"
                        else if ((strStatus == "Error/Failed") && (ChkUseDateRange.Checked) && (chkTo.Checked))
                        {
                            _strQuery = _strQuery + " WHERE Status <>'Success' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus == "Error/Failed") && ((ChkUseDateRange.Checked) == false) && (chkTo.Checked))
                        {
                            _strQuery = _strQuery + " WHERE Status <>'Success' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus == "Error/Failed") && ((ChkUseDateRange.Checked) == false) && ((chkTo.Checked) == false))
                        {
                            _strQuery = _strQuery + " WHERE  Status <>'Success' and CONVERT(DATE, DatetimeProcessed) = '" + System.DateTime.Now.ToShortDateString() + "'";
                        }
                        else if ((strStatus == "Error/Failed") && (ChkUseDateRange.Checked) && ((chkTo.Checked) == false))
                        {
                            _strQuery = _strQuery + " WHERE Status <>'Success' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "'";
                        }
                        #endregion "When status=Error/Failed"

                        #region "When Status is not All and Error/Failed
                        else if ((strStatus != "All") && (ChkUseDateRange.Checked) && (chkTo.Checked))
                        {
                            _strQuery = _strQuery + " WHERE Status='" + strStatus + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus != "All") && ((ChkUseDateRange.Checked) == false) && (chkTo.Checked))
                        {
                            _strQuery = _strQuery + " WHERE Status='" + strStatus + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) <= '" + dtpToDate.Value + "'";
                        }
                        else if ((strStatus != "All") && ((ChkUseDateRange.Checked) == false) && ((chkTo.Checked) == false))
                        {
                            _strQuery = _strQuery + " WHERE  Status='" + strStatus + "' and CONVERT(DATE, DatetimeProcessed) = '" + System.DateTime.Now.ToShortDateString() + "'";
                        }
                        else if ((strStatus != "All") && (ChkUseDateRange.Checked) && ((chkTo.Checked) == false))
                        {
                            _strQuery = _strQuery + " WHERE Status='" + strStatus + "' AND CONVERT(DATE, HL7_MessageLog.DatetimeProcessed) >= '" + dtpFromDate.Value + "'";
                        }
                        #endregion "When Status is not All and Error/Failed
                        //When Client is selected
                        if (!String.IsNullOrEmpty(Convert.ToString(cmbHL7Clients.SelectedValue)))
                        {
                            _strQuery = _strQuery + " AND HL7Client ='" + Convert.ToString(cmbHL7Clients.SelectedValue) + "'";
                        }
                        //else
                        //{
                        //    _strQuery = _strQuery + " WHERE  Status='" + strStatus + "'";
                        //}
                        _strQuery = _strQuery + " )SELECT COUNT([Message ID]) FROM tbl_HL7MsgLog";
                        //_strQuery = "select COUNT(*) FROM Patient INNER JOIN HL7_MessageQueue ON Patient.nPatientID = HL7_MessageQueue.nPatientID";
                    }
                    else
                    {
                        sServiceName = cmbServiceName.SelectedItem.ToString();
                        _strQuery = @"With tbl_GlMsgLog
                                    AS
                                    (
                                    SELECT  
                                        			
			                                    ROW_NUMBER() over(order by Patient.nPatientID ASC) AS ROWID,
			                                    ISNULL(dbo.Patient.nPatientID ,0)AS PatientId, 
			                                    ISNULL(dbo.Gl_Messagequeue.dtDateTimeStamp,'') AS DateTimeStamp,    
			                                    CASE dbo.Gl_Messagequeue.sMessageName WHEN 'HEALTHVAULT-DATA' THEN 'Data Exchange' WHEN 'HEALTHVAULT-EMAIL' THEN 'Access Request'  else dbo.Gl_Messagequeue.sMessageName END AS 'MessageName'
			                                    , dbo.Gl_Messagequeue.sMachineName AS MachineName,    
			                                    ISNULL(dbo.Patient.sPatientCode,'') AS PatientCode, 
			                                    dbo.GET_NAME(ISNULL(dbo.Patient.sFirstName,''),ISNULL(dbo.Patient.sMiddleName,''), ISNULL(dbo.Patient.sLastName,'')) as Patient,  

			                                    CASE dbo.Gl_Messagequeue.nStatus WHEN 1 THEN 'Queue' WHEN 0 THEN 'Processed'WHEN 2 THEN 'Error' ELSE 'Process Error' END as [Status],
			                                    CONVERT(varchar,dbo.Gl_Messagequeue.dtDateTimeStamp,101) AS 'Date'   
			                                    FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + @"'
                                        			
                                    )SELECT COUNT(ROWID) from tbl_GlMsgLog ";
                        //_strQuery = "SELECT COUNT(*) FROM dbo.Gl_Messagequeue INNER JOIN dbo.Patient ON dbo.Gl_Messagequeue.nPatientID = dbo.Patient.nPatientID Where sServiceName='" + sServiceName + "'";
                    }
                    String _strFilter = getFilterCriteria();
                    if (_strFilter.Length > 0)
                        _strQuery += " Where " + _strFilter + "";

                    _CntRecords = Convert.ToInt32(_oDBLayer.ExecuteScalar_Query(_strQuery));
                    _oDBLayer.Disconnect();
                }
                return _CntRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (_oDBLayer != null)
                {
                    _oDBLayer.Dispose();
                    _oDBLayer = null;
                }
            }
        }

        /// <summary>
        /// Function to initialize page count / log size
        /// </summary>
        /// <param name="nShowReport"></param>
        /// <param name="ConnectionString"></param>
        private void ProcessMessageLog(Int32 nShowReport, string ConnectionString)
        {
            // ClsDbCredentials _objDbCredentials = new ClsDbCredentials();
            string _strStatus = string.Empty;
            try
            {
                iLogCount = 1;
                iTotalPages = 1;
                iLogCount = GetHL7MessaLogCount();
            
                if (iLogCount > 0)
                {
                    if (iLogCount > nShowReport)
                    {
                        iTotalPages = Convert.ToInt16(Math.Floor(Convert.ToDecimal(iLogCount / nShowReport)));
                        if (iLogCount > (nShowReport * iTotalPages))
                        {
                            Int32 iLogDifference = iLogCount - (nShowReport * iTotalPages);
                            iLogCount = iLogCount + (nShowReport - iLogDifference);
                            iTotalPages = iTotalPages + 1;
                        }
                    }

                }
                iPageNumber = 1;
                iStartIndex = 0;
                iEndIndex = iPageSize;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Function for binding data to grid
        /// </summary>
        /// 
       
        private void BindGrid()
        {
            //DataTable _dtMessageLog = new DataTable();
            string _strStatus = string.Empty;
            try
            {
                C1Flex.BeginUpdate();
                dtMessageLog = GetMessageLogRecords();

                // _dtMessageLog = GetMessageLogRecords();
                setC1FlexGridCol();
                if (dtMessageLog != null)
                {
                    C1Flex.Styles.Normal.WordWrap = true;
                  
                    C1Flex.DataSource = dtMessageLog;
                    if (cmbServiceName.SelectedIndex == 0)
                    {
                        //Set text  align
                        C1Flex.Cols[mColIndex_MessageID].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_MessageID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        
                        C1Flex.Cols[mColIndex_DateTimeStamp].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_DateTimeStamp].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_PatientCode].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_PatientCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_Patient].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_Patient].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_EventName].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_EventName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_SegmentName].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_SegmentName].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_Description].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_Description].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_Status].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_Status].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_TransactionType].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_TransactionType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_ExternalCode].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_ExternalCode].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;

                        C1Flex.Cols[mColIndex_ExternalType].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
                        C1Flex.Cols[mColIndex_ExternalType].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop;
         
                        int nWidth = pnlGrid.Width;
                        C1Flex.ExtendLastCol = true;
                        C1Flex.AutoResize = false;

                        C1Flex.Cols[mColIndex_MessageID].Width = Convert.ToInt32(nWidth * 0.15);
                        C1Flex.Cols[mColIndex_DateTimeStamp].Width = Convert.ToInt32(nWidth * 0.12);
                        C1Flex.Cols[mColIndex_PatientCode].Width = Convert.ToInt32(nWidth * 0.09);
                        C1Flex.Cols[mColIndex_Patient].Width = Convert.ToInt32(nWidth * 0.10);
                        C1Flex.Cols[mColIndex_EventName].Width = Convert.ToInt32(nWidth * 0.09);
                        C1Flex.Cols[mColIndex_SegmentName].Width = Convert.ToInt32(nWidth * 0.10);
                        C1Flex.Cols[mColIndex_Description].Width = Convert.ToInt32(nWidth * 0.15);
                        C1Flex.Cols[mColIndex_Status].Width = Convert.ToInt32(nWidth * 0.06);
                        C1Flex.Cols[mColIndex_TransactionType].Width = Convert.ToInt32(nWidth * 0.10);
                        C1Flex.Cols[mColIndex_ExternalCode].Width = Convert.ToInt32(nWidth * 0.09);
                        C1Flex.Cols[mColIndex_ExternalType].Width = Convert.ToInt32(nWidth * 0.09);
                        C1Flex.AllowEditing = false;
                        //Applying style to DateTimeStamp Column
                        if (dtMessageLog.Rows.Count > 0)
                        {
                            C1Flex.Cols[mColIndex_DateTimeStamp].Format = "MM/dd/yyyy hh:mm:ss tt";
                            //C1.Win.C1FlexGrid.CellStyle cs = C1Flex.Styles.Add("datetime");
                            //cs.DataType = typeof(DateTime);
                            //cs.Format = "MM/dd/yyyy hh:mm tt";
                            //C1.Win.C1FlexGrid.CellRange rg = C1Flex.GetCellRange(1, mColIndex_DateTimeStamp, C1Flex.Rows.Count - 1, mColIndex_DateTimeStamp);
                            //rg.Style = C1Flex.Styles["datetime"];

                            C1Flex.AutoSizeRows(1, mColIndex_Patient, C1Flex.Rows.Count - 1, C1Flex.Cols.Count - 1, 10, C1.Win.C1FlexGrid.AutoSizeFlags.None);
                            C1Flex.AutoSizeCols(0, mColIndex_MessageID, C1Flex.Rows.Count - 1, mColIndex_MessageID+ 1, 4, C1.Win.C1FlexGrid.AutoSizeFlags.None);
                        }


                    }
                    else
                    {
                        int nWidth = pnlGrid.Width;
                        C1Flex.ExtendLastCol = true;
                        C1Flex.Cols[0].Width = Convert.ToInt32(nWidth * 0.20);
                        C1Flex.Cols[1].Width = Convert.ToInt32(nWidth * 0.20);
                        C1Flex.Cols[2].Width = Convert.ToInt32(nWidth * 0.15);
                        C1Flex.Cols[3].Width = Convert.ToInt32(nWidth * 0.10);
                        C1Flex.Cols[4].Width = Convert.ToInt32(nWidth * 0.15);
                        C1Flex.Cols[5].Width = Convert.ToInt32(nWidth * 0.20);


                        if (dtMessageLog.Rows.Count > 0)
                        {
                            C1Flex.AllowEditing = false;
                            //Applying style to DateTimeStamp Column
                            C1.Win.C1FlexGrid.CellStyle cs;//= C1Flex.Styles.Add("datetime");
                            try
                            {
                                if (C1Flex.Styles.Contains("datetime"))
                                {
                                    cs = C1Flex.Styles["datetime"];
                                }
                                else
                                {
                                    cs = C1Flex.Styles.Add("datetime");
                                   
                                }

                            }
                            catch
                            {
                                cs = C1Flex.Styles.Add("datetime");
              

                            }
         
                            cs.DataType = typeof(DateTime);
                            cs.Format = "MM/dd/yyyy hh:mm tt";
                            // C1Flex.Cols[mColIndex_sDatetimeStamp].Style = cs;
                            C1.Win.C1FlexGrid.CellRange rg = C1Flex.GetCellRange(1, ColIndex_DateTimeStamp, C1Flex.Rows.Count - 1, ColIndex_DateTimeStamp);
                            rg.Style = C1Flex.Styles["datetime"];
                        }

                   }
                }
                C1Flex.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Function for applying styles to grid.
        /// </summary>
        private void setC1FlexGridCol()
        {
            try
            {
               // C1Flex.Clear();
                C1Flex.DataSource = null;
                C1Flex.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                //C1Flex.DataSource = null;
                C1Flex.AllowDrop = true;
                C1Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                C1Flex.Cols[0].Width = 30;
                C1Flex.ExtendLastCol = true;
            
                //set properties of c1 grid
                C1Flex.Rows.Count = 1;
                C1Flex.Rows.Fixed = 1;
                if (cmbServiceName.SelectedIndex == 0)
                {
                    C1Flex.Cols.Count = 11;
                    C1Flex.Cols.Fixed = 0;
                    C1Flex.AutoResize = false;

                    C1Flex.Rows[C1Flex.Rows.Count - 1].Height = 21;
                    C1Flex.Cols[mColIndex_MessageID].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_DateTimeStamp].DataType = Type.GetType("System.DateTime");
                    C1Flex.Cols[mColIndex_PatientCode].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_Patient].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_EventName].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_SegmentName].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_Description].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_Status].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_TransactionType].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_ExternalCode].DataType = Type.GetType("System.String");
                    C1Flex.Cols[mColIndex_ExternalType].DataType = Type.GetType("System.String");


                    //Set Column Headers
                    C1Flex.SetData(0, mColIndex_MessageID, "Message Id");
                    C1Flex.SetData(0, mColIndex_DateTimeStamp, "DateTime Stamp");
                    C1Flex.SetData(0, mColIndex_PatientCode, "Patient Code");
                    C1Flex.SetData(0, mColIndex_Patient, "Patient Name");
                    C1Flex.SetData(0, mColIndex_EventName, "Event Name");
                    C1Flex.SetData(0, mColIndex_SegmentName, "Segment Name");
                    C1Flex.SetData(0, mColIndex_Description, "Description");
                    C1Flex.SetData(0, mColIndex_Status, "Status");
                    C1Flex.SetData(0, mColIndex_TransactionType, "Transaction Type");
                    C1Flex.SetData(0, mColIndex_ExternalCode, "External Code");
                    C1Flex.SetData(0, mColIndex_ExternalType, "External Type");

                    //set column editing properties.
                    C1Flex.Cols[mColIndex_MessageID].AllowEditing = false;
                    C1Flex.Cols[mColIndex_DateTimeStamp].AllowEditing = false;
                    C1Flex.Cols[mColIndex_PatientCode].AllowEditing = false;
                    C1Flex.Cols[mColIndex_Patient].AllowEditing = false;
                    C1Flex.Cols[mColIndex_EventName].AllowEditing = false;
                    C1Flex.Cols[mColIndex_SegmentName].AllowEditing = false;
                    C1Flex.Cols[mColIndex_Description].AllowEditing = false;
                    C1Flex.Cols[mColIndex_Status].AllowEditing = false;
                    C1Flex.Cols[mColIndex_TransactionType].AllowEditing = false;
                    C1Flex.Cols[mColIndex_ExternalCode].AllowEditing = false;
                    C1Flex.Cols[mColIndex_ExternalType].AllowEditing = false;
                }
                else
                {
                    C1Flex.Cols.Count = 6;
                    C1Flex.Cols.Fixed = 0;
                    C1Flex.Rows[C1Flex.Rows.Count - 1].Height = 21;

                    C1Flex.Cols[ColIndex_DateTimeStamp].DataType = Type.GetType("System.DateTime");
                    C1Flex.Cols[ColIndex_MessageName].DataType = Type.GetType("System.String");
                    C1Flex.Cols[ColIndex_MachineName].DataType = Type.GetType("System.String");
                    C1Flex.Cols[ColIndex_PatientCode].DataType = Type.GetType("System.String");
                    C1Flex.Cols[ColIndex_Patient].DataType = Type.GetType("System.String");
                    C1Flex.Cols[ColIndex_Status].DataType = Type.GetType("System.String");

                    C1Flex.SetData(0, ColIndex_DateTimeStamp, "Date Time Stamp");
                    C1Flex.SetData(0, ColIndex_MessageName, "Message Name");
                    C1Flex.SetData(0, ColIndex_MachineName, "Machine Name");
                    C1Flex.SetData(0, ColIndex_PatientCode, "Patient Code");
                    C1Flex.SetData(0, ColIndex_Patient, "Patient Name");
                    C1Flex.SetData(0, ColIndex_Status, "Status");

                    //set column editing properties.
                    C1Flex.Cols[ColIndex_DateTimeStamp].AllowEditing = false;
                    C1Flex.Cols[ColIndex_MessageName].AllowEditing = false;
                    C1Flex.Cols[ColIndex_MachineName].AllowEditing = false;
                    C1Flex.Cols[ColIndex_PatientCode].AllowEditing = false;
                    C1Flex.Cols[ColIndex_Patient].AllowEditing = false;
                    C1Flex.Cols[ColIndex_Status].AllowEditing = false;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Function to retrieve service names from General Message Log
        /// </summary>
        private void LoadServiceName()
        {
            DataTable dtServiceNames = new DataTable();
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
            string _strSQL = string.Empty;

            try
            {
                _strSQL = "SELECT Distinct sServiceName FROM Gl_Messagequeue";

                objDbLayer.Connect(false);
                objDbLayer.Retrive_Query(_strSQL, out dtServiceNames);


                if ((dtServiceNames != null) & dtServiceNames.Rows.Count > 0)
                {
                    foreach (DataRow _dataRow in dtServiceNames.Rows)
                    {
                        cmbServiceName.Items.Add(Convert.ToString(_dataRow["sServiceName"]));
                    }
                }

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if ((objDbLayer != null))
                {
                    objDbLayer.Dispose();
                    objDbLayer = null;
                }
                _strSQL = string.Empty;
            }
        }

        /// <summary>
        /// Function to retrieve message queue page size from Settings
        /// </summary>
        /// <returns></returns>
        private string RetrivePagingSize()
        {
            object objPageSize;
            gloDatabaseLayer.DBLayer oDbLayer = null;
            try
            {
                oDbLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                oDbLayer.Connect(false);
                objPageSize = oDbLayer.ExecuteScalar_Query("select ISNULL(sSettingsValue,'') from Settings where UPPER(sSettingsName) ='INTERFACEMESSAGELOGPAGESIZE'");
                oDbLayer.Disconnect();
                if (Convert.ToString(objPageSize).Length == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return Convert.ToString(objPageSize);
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            finally
            {
                if (oDbLayer != null)
                {
                    oDbLayer.Dispose();
                    oDbLayer = null;
                }
            }
        }
        /// <summary>
        /// Adding page size for paging logic
        /// </summary>
        private void AddPagingSize()
        {
            cmbPageSize.Items.Clear();
            string PagingSize = RetrivePagingSize();
            // check if setting available
            if (PagingSize != string.Empty)
            {
                long CurrentSize = 0;
                string[] pagingArray = PagingSize.Split(',');
                for (int index = 0; index <= pagingArray.Length - 1; index++)
                {
                    if (long.TryParse(pagingArray[index], out CurrentSize))
                    {
                        cmbPageSize.Items.Add(CurrentSize);
                    }

                }
                cmbPageSize.SelectedIndex = 0;
            }
            // if setting not found
            else
            {
                cmbPageSize.Items.Add("100");
                cmbPageSize.Items.Add("200");
                cmbPageSize.Items.Add("300");
                cmbPageSize.SelectedIndex = 0;
            }
            iEndIndex = Convert.ToInt32(cmbPageSize.Text);
            iPageSize = Convert.ToInt32(cmbPageSize.Text);
        }
        /// <summary>
        /// Set Paging logic from start
        /// </summary>
        private void SetData()
        {
            try
            {
                iPageNumber--;

                if (iPageNumber < 1)
                {
                    iPageNumber = 1;
                    lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                    return;
                }
                iPageNumber = 1;
                iStartIndex = 0;
                iEndIndex = iPageSize;

                BindGrid();

                //lblPage.Text = "Page: " + iPageNumber + " of " + iTotalPages;
                lblSelected.Text = "Page: " + iPageNumber + " of " + iTotalPages;
            }
            catch (Exception)
            {
                // Interaction.MsgBox(Err().Description);
            }
        }

        #endregion "User Defined Functions"

        private void frmHL7_MessageQueue_Shown(object sender, EventArgs e)
        {
            BindGrid();
            if ((dtSpecificDateReport.ToString() != "#12:00:00 AM#") && (dtSpecificDateReport.ToString() != "1/1/0001 12:00:00 AM"))
            {
                if (dtMessageLog == null || dtMessageLog.Rows.Count<=0)
                {
                    MessageBox.Show("No Message Log found. Message Log may have been deleted by user.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_isFormLoaded)
            {
                _isFieldChange = true;
                if (chkTo.Checked)
                {
                    dtpToDate.Enabled = true;
                }
                else
                {
                    dtpToDate.Enabled = false;
                }
            }
        }
      
        private void ExportData(Int32 _IStartIndex, String _strPathName)
        {

            if (_IStartIndex == 1)
            {
                iStartIndex = 0;
            }
            else
            {
                iStartIndex = ((_IStartIndex - 1) * iPageSize);
            }
            //_IEndIndex = iStartIndex + iPageSize;
            iEndIndex = iStartIndex + iPageSize;

            Application.DoEvents();
            BindGrid();
            C1Flex.SaveExcel(_strPathName, "Page " + _IStartIndex.ToString(), C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
            lblSelected.Text = "Page: " + _IStartIndex + " of " + iTotalPages;
            iStartIndex = iStartIndex + iPageSize;
            iEndIndex = iStartIndex + iPageSize;
            iPageNumber = _IStartIndex;
        }

        private void txtPageRange_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) & e.KeyChar <= Convert.ToChar(57)) | e.KeyChar == Convert.ToChar(8) | e.KeyChar == ',' | e.KeyChar == '-'))
            {
                e.Handled = true;
            }
        }

        private void rd_CurrentPage_CheckedChanged_1(object sender, EventArgs e)
        {
            //txtPageRange.Visible = false;
            //lblpageRange.Visible = false;
        }

        private void rd_SelectAll_CheckedChanged_1(object sender, EventArgs e)
        {
            //txtPageRange.Visible = false;
            //lblpageRange.Visible = false;
        }

        private void rd_PageRange_CheckedChanged_1(object sender, EventArgs e)
        {
            //if (rd_PageRange.Checked)
            //{
            //    //RecordTo.Enabled = true;
            //    //RecordFrom.Enabled = true;
            //    txtPageRange.Visible = true;
            //    txtPageRange.Focus();
            //    lblpageRange.Visible = true;
            //}
            //else
            //{
            //    //RecordTo.Enabled = false;
            //    //RecordFrom.Enabled = false;
            //    txtPageRange.Visible = false;
            //    // lblpageRange.Visible = false;
            //}
        }

        //private void tsExportData_Click(object sender, EventArgs e)
        //{
        //    string _strPathName = string.Empty;
        //   // pnlExport.Visible = true;
        //    //pnlExport.BringToFront();
        //    int tempiStartindex = iStartIndex;
        //    int tempiEndIndex = iPageSize;
        //    #region "Check if Valid page no."
        //    if (rd_PageRange.Checked)
        //    {
        //        String[] iRange = txtPageRange.Text.Split(',');
        //        String[] strRange;
        //        for (Int32 j = 0; j < iRange.Length; j++)
        //        {
        //            String sSplit = iRange[j].ToString();
        //            if ((sSplit.Contains("-")))
        //            {
        //                strRange = iRange[j].Split('-');

        //                if ((strRange[0] == string.Empty) || (strRange[1] == string.Empty) || (Convert.ToInt32(strRange[0]) > Convert.ToInt32(strRange[1])))
        //                {
        //                    MessageBox.Show("Please enter the valid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    txtPageRange.Focus();
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                if ((sSplit.Trim() == string.Empty) || (Convert.ToInt32(sSplit) > iTotalPages))
        //                {
        //                    MessageBox.Show("Please enter the valid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    txtPageRange.Focus();
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //    #endregion "Check if Valid page no."

        //    #region "select file"

        //    FolderBrowserDialog op = new FolderBrowserDialog();//select the path where user want to store.
        //    op.ShowDialog(this);
        //    if ((op.SelectedPath == string.Empty) || (op.SelectedPath == null))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        _strPathName = System.IO.Path.Combine(op.SelectedPath, "InterfaceReport-" + System.DateTime.Now.ToString("MMddyyyyhhmmss") + ".xls");
        //    }
        //    try
        //    {
        //        if (System.IO.File.Exists(_strPathName))
        //        {
        //            System.IO.File.Delete(_strPathName);
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("This file is currently used by another person. Please close file and retry.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    #endregion "select file"

        //    #region "export record to excel file"
        //    try
        //    {
        //        Application.DoEvents();
        //        pnlregistration.Visible = true;
        //        pnlregistration.BringToFront();
        //        this.Cursor = Cursors.WaitCursor;
        //        if (iTotalPages >= 1)//check if there are more tab pages
        //        {
        //            Application.DoEvents();

        //            if (rd_CurrentPage.Checked)
        //            {
        //                C1Flex.SaveExcel(_strPathName, "Page 1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);
        //            }
        //            else if (rd_SelectAll.Checked)
        //            {
        //                iStartIndex = 0;
        //                iEndIndex = iPageSize;
        //                for (int i = 1; i <= iTotalPages; i++)
        //                {
        //                    ExportData(i, _strPathName);
        //                }
        //                iStartIndex = iStartIndex - iPageSize;
        //                iEndIndex = iEndIndex - iPageSize;
        //                BindGrid();
        //            }
        //            else
        //            {
        //                Int32 _IEndIndex = 0;
        //                Int32 _IStartIndex = 0;

        //                if (txtPageRange.Text.Trim().Length <= 0)
        //                {
        //                    MessageBox.Show("Please enter the valid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    txtPageRange.Focus();
        //                    this.Cursor = Cursors.Default;
        //                    pnlregistration.Visible = false;
        //                    return;
        //                }
        //                String[] iRange = txtPageRange.Text.Split(',');
        //                String[] strRange;

        //                //_IStartIndex = Convert.ToInt32(RecordFrom.Value);
        //                //_IEndIndex = _IStartIndex + Convert.ToInt32(RecordTo.Value);

        //                for (Int32 j = 0; j < iRange.Length; j++)
        //                {
        //                    String sSplit = iRange[j].ToString();
        //                    if ((sSplit.Contains("-")))
        //                    {
        //                        strRange = iRange[j].Split('-');

        //                        if ((strRange[0] == string.Empty) || (strRange[1] == string.Empty) || (Convert.ToInt32(strRange[0]) > Convert.ToInt32(strRange[1])))
        //                        {
        //                            MessageBox.Show("Please enter the valid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            txtPageRange.Focus();
        //                            this.Cursor = Cursors.Default;
        //                            pnlregistration.Visible = false;
        //                            return;

        //                        }
        //                        _IStartIndex = Convert.ToInt32(strRange[0]);
        //                        _IEndIndex = Convert.ToInt32(strRange[1]);

        //                        if (_IEndIndex > iTotalPages)
        //                        {
        //                            _IEndIndex = iTotalPages;
        //                        }
        //                        if (_IStartIndex > iTotalPages)
        //                        {
        //                            this.Cursor = Cursors.Default;
        //                            pnlregistration.Visible = false;
        //                            return;
        //                        }
        //                        for (int i = _IStartIndex; i <= _IEndIndex; i++)
        //                        {
        //                            ExportData(_IStartIndex, _strPathName);
        //                            _IStartIndex++;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        if (sSplit == string.Empty)
        //                        {
        //                            MessageBox.Show("Please enter the valid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            txtPageRange.Focus();
        //                            this.Cursor = Cursors.Default;
        //                            pnlregistration.Visible = false;
        //                            return;
        //                        }
        //                        _IStartIndex = Convert.ToInt32(sSplit);
        //                        if (_IStartIndex <= iTotalPages)
        //                        {
        //                            ExportData(_IStartIndex, _strPathName);
        //                        }
        //                        else
        //                        {
        //                            this.Cursor = Cursors.Default;
        //                            txtPageRange.Focus();
        //                            pnlregistration.Visible = false;
        //                            return;
        //                        }
        //                    }

        //                }
        //             }
        //            pnlregistration.Visible = false;
        //            this.Cursor = Cursors.Default;
        //            MessageBox.Show("The Interface report file exported successfully." + "\n" + "(Note: Data file path: " + _strPathName + ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            pnlExport.Visible = false;
        //            pnlTop.Enabled = true;
        //            tlpViewReport.Enabled = true;
        //            pnlTop.Enabled = true;
        //            C1Flex.Enabled = true;
        //        }
        //        else
        //        {
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    #endregion "export record to excel file"
        //}

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if(_isFormLoaded)
                _isFieldChange = true;
    
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (_isFormLoaded)
                _isFieldChange = true;
        }

        private void PopulateHL7Clients()
        {
            DataTable dtHL7Clients = new DataTable();
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(gloSettings.HL7Settings.GetHL7ConnectionString());
            gloSettings.GeneralSettings objSetting = new gloSettings.GeneralSettings(strConnectionString);
            string _sSendingApplication = string.Empty;
            string _strSQL = string.Empty;

            try
            {
                _strSQL = "SELECT sSendingApplicationName AS HL7ClientName, sSendingApplicationName AS HL7ClientID FROM dbo.HL7_Clients WHERE nDBConnectionId = " + gloSettings.HL7Settings.GetEmrDBConnectionIdInHL7DB();

                objDbLayer.Connect(false);
                objDbLayer.Retrive_Query(_strSQL, out dtHL7Clients);
                objDbLayer.Disconnect();

                //Blank Row
                dtHL7Clients.Rows.InsertAt(dtHL7Clients.NewRow(), 0);

                if (objSetting != null)
                {
                    DataTable dtSettings = objSetting.GetSetting("Sending Application");
                    
                    if (dtSettings != null && dtSettings.Rows.Count > 0)
                    {
                            _sSendingApplication = Convert.ToString(dtSettings.Rows[0][0]);
                    }
                }
                
                DataRow dtRow = dtHL7Clients.NewRow();

                if (!string.IsNullOrEmpty(_sSendingApplication))
                {
                    dtRow["HL7ClientName"] = _sSendingApplication + " [Outbound]";
                    dtRow["HL7ClientID"] = _sSendingApplication;
                }
                else
                {
                    dtRow["HL7ClientName"] = "gloStream [Outbound]";
                    dtRow["HL7ClientID"] = "gloStream";
                }
                //gloStream
                dtHL7Clients.Rows.InsertAt(dtRow, 1);
                dtRow = null;

                //Clients from db
                if ((dtHL7Clients != null) && dtHL7Clients.Rows.Count > 0)
                {
                    cmbHL7Clients.DataSource = dtHL7Clients;
                    cmbHL7Clients.DisplayMember = "HL7ClientName";
                    cmbHL7Clients.ValueMember = "HL7ClientID";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((objDbLayer != null))
                {
                    objDbLayer.Dispose();
                    objDbLayer = null;
                }
                if ((objSetting != null))
                {
                    objSetting.Dispose();
                    objSetting = null;
                }
                _strSQL = string.Empty;
                _sSendingApplication = string.Empty;
            }
        }

          private void ShowHL7Message(String sMessageId)
        {
            gloDatabaseLayer.DBLayer oDbLayer = null;
            String strQuery = String.Empty;
            String sMessageDetails = String.Empty;
            frmHL7MessageDetails objHL7MessageDetails = null;

            try
            {
                oDbLayer = new gloDatabaseLayer.DBLayer(gloSettings.HL7Settings.GetHL7ConnectionString());
                oDbLayer.Connect(false);

                strQuery = "SELECT ISNULL(HL7Message,'') AS HL7Message FROM HL7_HL7Data WHERE MessageID='" + sMessageId + "'";
                sMessageDetails = Convert.ToString(oDbLayer.ExecuteScalar_Query(strQuery));
                oDbLayer.Disconnect();

                if (!String.IsNullOrEmpty(sMessageDetails))
                {
                    objHL7MessageDetails = new frmHL7MessageDetails(sMessageId,sMessageDetails);
                    objHL7MessageDetails.ShowDialog(this);
                    objHL7MessageDetails.Dispose();
                    objHL7MessageDetails = null;
                }
                else
                {
                    MessageBox.Show("Message not found." + Environment.NewLine + "This message might be removed or archived  from source database.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((oDbLayer != null))
                {
                    oDbLayer.Dispose();
                    oDbLayer = null;
                }

                if ((objHL7MessageDetails != null))
                {
                    objHL7MessageDetails.Dispose();
                    objHL7MessageDetails = null;
                }

                strQuery = String.Empty;
                sMessageDetails = String.Empty;
            }
        }

        private void tspViewHL7Message_Click(object sender, EventArgs e)
        {
            String sMessageId = String.Empty;
            
            try
            {
                if (C1Flex.RowSel <= 0)
                {
                    MessageBox.Show("Please select record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    sMessageId = Convert.ToString(C1Flex.GetData(C1Flex.RowSel, 0));

                    if (!String.IsNullOrEmpty(sMessageId))
                    {
                        ShowHL7Message(sMessageId);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sMessageId = String.Empty;
            }

        }

  
    }
}
