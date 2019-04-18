using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.IO;

namespace gloBilling
{
    public partial class frmCleargageFileHistory : Form
    {
        #region "Table Constants"

        #region "C1AllUploadedFiles"

        private const int Col_CellImage = 0;
        private const int Col_nFileID = 1;
        private const int Col_nProcessID = 2;
        private const int Col_sFileName = 3;
        private const int Col_iBinaryFile = 4;
        private const int Col_dtUploadedDate = 5;
        private const int Col_c1AllUploadedFilesCount = 6;

        #endregion

        #region "C1UploadedFile"

        private const int Col_DetailsProcessID = 0;
        private const int Col_DetailsPatientID = 1;
        private const int Col_DetailsPatientName = 2;
        private const int Col_DetailsDOS = 3;
        private const int Col_DetailsBalDue = 4;
        private const int Col_DetailsEncounterID = 5;
        private const int Col_Detailsc1UploadedFileCount = 6;

        #endregion

        #endregion
        
        #region "Load"

        public frmCleargageFileHistory()
        {
            InitializeComponent();
        }

        private void frmCleargageFileHistory_Load(object sender, EventArgs e)
        {
            try 
            {
                LoadAllUploadedFiles();
            }
            catch (Exception ex)             
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void LoadAllUploadedFiles()
        {
            DataTable dtAllUploadedFiles;
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                dtAllUploadedFiles = oClsCleargagePaymentPosting.GetCleargageUploadedFiles();
                c1AllUploadedFiles.DataSource = null;
                c1AllUploadedFiles.DataSource = dtAllUploadedFiles.DefaultView;
                if (c1AllUploadedFiles.DataSource != null && dtAllUploadedFiles != null && dtAllUploadedFiles.Rows.Count > 0)
                {
                    DesignC1AllUploadedFiles();

                    #region "Label Values"

                    lblUploadedFilesCountValue.Text = Convert.ToString(c1AllUploadedFiles.Rows.Count);
                    lblFileUploadedDateValue.Text = Convert.ToDateTime(c1AllUploadedFiles.GetData(c1AllUploadedFiles.RowSel, Col_dtUploadedDate)).ToString("MM/dd/yyyy");
                    ShowHideFooterLabels(true);

                    #endregion
                }
                else
                {
                    ShowHideFooterLabels(false);
                }
                c1AllUploadedFiles.Select(0, 0);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                oClsCleargagePaymentPosting.Dispose();
                oClsCleargagePaymentPosting = null;
            }
        }

        #endregion

        #region "C1AllUploadedFiles Mouse Events"

        private void c1AllUploadedFiles_MouseLeave(object sender, EventArgs e)
        {
            C1SuperTooltipDx.Hide();
        }

        private void c1AllUploadedFiles_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltipDx, ((C1FlexGrid)sender), e.Location);
        }
        
        #endregion

        #region "Form Events"

        private void frmCleargageFileHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        #region "C1AllUploadedFiles Selection Event"

        private void c1AllUploadedFiles_AfterSelChange(object sender, RangeEventArgs e)
        {
            DataTable dtUploadedFileDetails;
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                ClearSearchC1UploadedFileDetails();
                if (c1AllUploadedFiles.Rows.Count > 0 && c1AllUploadedFiles.RowSel >= 0)
                {
                    dtUploadedFileDetails = oClsCleargagePaymentPosting.GetCleargageUploadedFileDetails(Convert.ToInt64(c1AllUploadedFiles.GetData(c1AllUploadedFiles.RowSel, Col_nProcessID)));                    
                    c1UploadedFileDetails.DataSource = dtUploadedFileDetails.DefaultView;

                    if (lblFileUploadedDate.Visible == true && lblFileUploadedDateValue.Visible == true) 
                    {
                        lblFileUploadedDateValue.Text = Convert.ToDateTime(c1AllUploadedFiles.GetData(c1AllUploadedFiles.RowSel, Col_dtUploadedDate)).ToString("MM/dd/yyyy");
                    }
                }
                else 
                {
                    c1UploadedFileDetails.DataSource = null;
                    c1UploadedFileDetails.Rows.Count = 1;
                }
                Designc1UploadedFileDetails(); 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally 
            {
                oClsCleargagePaymentPosting.Dispose();
                oClsCleargagePaymentPosting = null;
            }
        }

        #endregion

        #region "C1UploadedFileDetails Mouse Events"

        private void c1UploadedFileDetails_MouseLeave(object sender, EventArgs e)
        {
            C1SuperTooltipDx.Hide();
        }

        private void c1UploadedFileDetails_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltipDx, ((C1FlexGrid)sender), e.Location);
        }

        #endregion

        #region "Search"

        private void txtSearchAllUploadedFiles_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)c1AllUploadedFiles.DataSource;
                SearchAllUploadedFiles(txtSearchAllUploadedFiles.Text, dv);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void txtSearchUploadedFileDetials_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView();
            try
            {
                dv = (DataView)c1UploadedFileDetails.DataSource;
                SearchUploadedFileDetails(txtSearchUploadedFileDetials.Text, dv);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private bool SearchUploadedFileDetails(string strsearch, DataView dtView)
        {
            bool bResult = false;
            string[] strSearchArray = null;
            string _strTag = String.Empty;
            string sFilter = String.Empty;
            DataTable dt = new DataTable();
            DataView dv = null;

            try
            {
                dv = dtView;
                if (dv != null)
                {
                    strsearch = strsearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                    if (strsearch.Length > 1)
                    {
                        string str = strsearch.Substring(1).Replace("%", "");
                        strsearch = strsearch.Substring(0, 1) + str;
                    }
                    if (strsearch.Trim() != "")
                    {
                        strSearchArray = strsearch.Split(',');
                    }
                    if (strsearch.Trim() != "")
                    {
                        if (strSearchArray.Length == 1)
                        {
                            #region "Search For Single Value"

                            strsearch = strSearchArray[0];

                            sFilter = dv.Table.Columns["PatientCode"].ColumnName + " Like '" + strsearch + "%' OR " +
                                    dv.Table.Columns["PatientName"].ColumnName + " Like '%" + strsearch + "%' OR " +
                                    dv.Table.Columns["DateOfService"].ColumnName + " Like '" + strsearch + "%' OR " +
                                    dv.Table.Columns["EncounterID"].ColumnName + " Like '%" + strsearch + "%'";

                            #endregion
                        }
                        else
                        {
                            #region "Search For Comma Seperated Value"

                            for (int j = 0; j < strSearchArray.Length; j++)
                            {
                                strsearch = strSearchArray[j];
                                if (strsearch.Trim() != "")
                                {
                                    if (sFilter == "")
                                    {
                                        sFilter = "(" + dv.Table.Columns["PatientCode"].ColumnName + " Like '" + strsearch + "%' OR " +
                                                                dv.Table.Columns["PatientName"].ColumnName + " Like '%" + strsearch + "%' OR " +
                                                                dv.Table.Columns["DateOfService"].ColumnName + " Like '" + strsearch + "%' OR " +
                                                                dv.Table.Columns["EncounterID"].ColumnName + " Like '%" + strsearch + "%')";
                                    }
                                    else
                                    {
                                        sFilter = sFilter + "AND (" + dv.Table.Columns["PatientCode"].ColumnName + " Like '" + strsearch + "%' OR " +
                                                               dv.Table.Columns["PatientName"].ColumnName + " Like '%" + strsearch + "%' OR " +
                                                               dv.Table.Columns["DateOfService"].ColumnName + " Like '" + strsearch + "%' OR " +
                                                               dv.Table.Columns["EncounterID"].ColumnName + " Like '%" + strsearch + "%')";
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                    dv.RowFilter = sFilter;
                    if (dv.ToTable().Rows.Count > 0)
                    {
                        bResult = true;
                    }
                    c1UploadedFileDetails.DataSource = dv;
                }                                              
                Designc1UploadedFileDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            return bResult;
        }

        private bool SearchAllUploadedFiles(string strSearchKey, DataView dtView)
        {
            bool bResult = false;
            DataTable dt = new DataTable();
            DataView dv = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                RemoveSearchUploadedFileDetails();
                string sFilter = String.Empty;

                dv = dtView;
                if (dv != null)
                {
                    strSearchKey = strSearchKey.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                    if (strSearchKey.Length > 1)
                    {
                        string str = strSearchKey.Substring(1).Replace("%", "");
                        strSearchKey = strSearchKey.Substring(0, 1) + str;
                    }
                    if (strSearchKey.Trim() != "")
                    {
                        sFilter = dv.Table.Columns["sFileName"].ColumnName + " Like '%" + strSearchKey + "%'";
                    }

                    dv.RowFilter = sFilter;
                                       

                    bResult = true;
                    c1AllUploadedFiles.DataSource = dv;
                    DesignC1AllUploadedFiles();
                    c1AllUploadedFiles.RowSel = -1;
                    c1AllUploadedFiles.Select(-1, Col_sFileName, true);

                    if (dv != null && dv.Count > 0)
                    {
                        c1AllUploadedFiles.Select(0, Col_sFileName);
                        #region "Labels"

                        lblUploadedFilesCountValue.Text = Convert.ToString(c1AllUploadedFiles.Rows.Count);
                        lblFileUploadedDateValue.Text = Convert.ToDateTime(c1AllUploadedFiles.GetData(c1AllUploadedFiles.RowSel, Col_dtUploadedDate)).ToString("MM/dd/yyyy");
                        ShowHideFooterLabels(true);

                        #endregion
                    }
                    else { ShowHideFooterLabels(false); }
                }
                //else
                //{
                //    dv.RowFilter = "";
                //}

                //dv.RowFilter = sFilter;

                //c1AllUploadedFiles.RowSel = -1;
                //c1AllUploadedFiles.Select(-1, -1, false);

                //if (dv.ToTable().Rows.Count > 0)
                //{
                //    bResult = true;
                //    c1AllUploadedFiles.DataSource = dv;
                //    DesignC1AllUploadedFiles();
                //    c1AllUploadedFiles.RowSel = 0;
                //    c1AllUploadedFiles.Select(0, Col_sFileName, true);
                //}
                //else
                if (dv == null)
                {
                    c1AllUploadedFiles.RowSel = -1;
                    c1AllUploadedFiles.Select(-1, -1, false);
                    c1UploadedFileDetails.DataSource = null;
                    DesignC1AllUploadedFiles();
                    if (c1UploadedFileDetails.Rows.Count > 1)
                    {
                        c1UploadedFileDetails.Rows.RemoveRange(1, c1UploadedFileDetails.Rows.Count - 1);
                    }
                    ShowHideFooterLabels(false);
                }
            }
            catch (Exception)
            { }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return bResult;
        }

        private void RemoveSearchUploadedFileDetails()
        {
            try
            {
                txtSearchUploadedFileDetials.TextChanged -= new EventHandler(txtSearchUploadedFileDetials_TextChanged);
                txtSearchUploadedFileDetials.Text = "";
            }
            catch (Exception)
            { }
            finally
            {
                txtSearchUploadedFileDetials.TextChanged += new EventHandler(txtSearchUploadedFileDetials_TextChanged);
            }
        }

        #endregion

        #region "Button Click"

        private void btn_ClearC1UploadedFileDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ClearSearchC1UploadedFileDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btn_ClearC1AllUploadedFiles_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtSearchAllUploadedFiles.Text != "")
                //{ 
                //    txtSearchAllUploadedFiles.Text = "";
                //    if (txtSearchUploadedFileDetials.Text != "")
                //    { txtSearchUploadedFileDetials.Text = ""; }
                //}
                ClearSearchC1AllUploadedFiles();
                ClearSearchC1UploadedFileDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_View_Click(object sender, EventArgs e)
        {
            tsb_View.Click -= new EventHandler(tsb_View_Click);
            try
            {
                if (c1AllUploadedFiles.DataSource != null && c1AllUploadedFiles.Rows.Count > 0)
                {
                    string sFilename = "UploadedFile_" + Convert.ToString(c1AllUploadedFiles.GetData(c1AllUploadedFiles.RowSel, Col_sFileName));
                    string sFile = ConvertBinaryToFile((byte[])(c1AllUploadedFiles.GetData(c1AllUploadedFiles.RowSel, Col_iBinaryFile)), sFilename);
                    if (File.Exists(sFile))
                    {
                        System.Diagnostics.Process.Start(sFile);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally 
            {
                tsb_View.Click += new EventHandler(tsb_View_Click);
            }
        }

        private void tsb_ViewAcknowledgement_Click(object sender, EventArgs e)
        {
            tsb_ViewAcknowledgement.Click -= new EventHandler(tsb_ViewAcknowledgement_Click);
            DataTable dtAckBinaryFile;
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                if (c1AllUploadedFiles.DataSource != null && c1AllUploadedFiles.Rows.Count > 0)
                {
                    dtAckBinaryFile = oClsCleargagePaymentPosting.GetAcknowledgementBinaryFile(Convert.ToInt64(c1AllUploadedFiles.GetData(c1AllUploadedFiles.RowSel, Col_nFileID)));
                    if (dtAckBinaryFile != null && dtAckBinaryFile.Rows.Count > 0)
                    {
                        string sFilename = string.Empty;
                        if (Path.GetExtension(Convert.ToString(dtAckBinaryFile.Rows[0]["sOriginalFileName"])).ToUpper() == ".RESP")
                        {
                            sFilename = Path.GetFileNameWithoutExtension("AcknowledgementFile_" + Convert.ToString(dtAckBinaryFile.Rows[0]["sOriginalFileName"]));
                        }
                        else
                        {
                            sFilename = "AcknowledgementFile_" + Convert.ToString(dtAckBinaryFile.Rows[0]["sOriginalFileName"]);
                        }
                        string sFile = ConvertBinaryToFile((byte[])(dtAckBinaryFile.Rows[0]["iBinaryFile"]), sFilename);
                        if (File.Exists(sFile))
                        {
                            System.Diagnostics.Process.Start(sFile);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Acknowledgement file not available.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally 
            {
                tsb_ViewAcknowledgement.Click += new EventHandler(tsb_ViewAcknowledgement_Click);
            }
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAllUploadedFiles();
                ClearSearchC1AllUploadedFiles();
                ClearSearchC1UploadedFileDetails();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteTempFile();
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void ClearSearchC1UploadedFileDetails()
        {
            try
            {
                if (txtSearchUploadedFileDetials.Text != "")
                    txtSearchUploadedFileDetials.Text = "";
            }
            catch (Exception ex) 
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void ClearSearchC1AllUploadedFiles()
        {
            try
            {
                if (txtSearchAllUploadedFiles.Text != "")                
                    txtSearchAllUploadedFiles.Text = "";
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        #endregion

        #region "Design"

        private void DesignC1AllUploadedFiles()
        {
            int nWidth = c1AllUploadedFiles.Width - 5;
            try
            {
                c1AllUploadedFiles.Cols.Count = Col_c1AllUploadedFilesCount;
                

                c1AllUploadedFiles.Cols[Col_CellImage].Caption = "Image";
                c1AllUploadedFiles.Cols[Col_nFileID].Caption = "FileID";
                c1AllUploadedFiles.Cols[Col_nProcessID].Caption = "ProcessID";
                c1AllUploadedFiles.Cols[Col_sFileName].Caption = "Filename";
                c1AllUploadedFiles.Cols[Col_iBinaryFile].Caption = "BinaryFile";
                c1AllUploadedFiles.Cols[Col_dtUploadedDate].Caption = "UploadedDate";
                
                c1AllUploadedFiles.Cols[Col_CellImage].Visible = true;
                c1AllUploadedFiles.Cols[Col_nFileID].Visible = false;
                c1AllUploadedFiles.Cols[Col_nProcessID].Visible = false;
                c1AllUploadedFiles.Cols[Col_sFileName].Visible = true;
                c1AllUploadedFiles.Cols[Col_iBinaryFile].Visible = false;
                c1AllUploadedFiles.Cols[Col_dtUploadedDate].Visible = false;

                c1AllUploadedFiles.Cols[Col_CellImage].Width = (int)(nWidth * 0.12);
                c1AllUploadedFiles.Cols[Col_sFileName].Width = (int)(nWidth * 0.20);

                c1AllUploadedFiles.Cols[Col_CellImage].DataType = typeof(Image);
                for (int i = 0; i < c1AllUploadedFiles.Rows.Count; i++)
                {
                    c1AllUploadedFiles.SetCellImage(i, Col_CellImage, global::gloBilling.Properties.Resources.Bullet06);
                }
                c1AllUploadedFiles.AllowEditing = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void Designc1UploadedFileDetails()
        {            
            int nWidth = c1UploadedFileDetails.Width - 5;
            try
            {
                c1UploadedFileDetails.Cols.Count = Col_Detailsc1UploadedFileCount;
                c1UploadedFileDetails.Cols[Col_DetailsProcessID].Caption = "ProcessID";
                c1UploadedFileDetails.Cols[Col_DetailsPatientID].Caption = "Patient Code";
                c1UploadedFileDetails.Cols[Col_DetailsPatientName].Caption = "Patient";
                c1UploadedFileDetails.Cols[Col_DetailsDOS].Caption = "Service Date";
                c1UploadedFileDetails.Cols[Col_DetailsBalDue].Caption = "Patient Due";
                c1UploadedFileDetails.Cols[Col_DetailsEncounterID].Caption = "Encounter ID";

                c1UploadedFileDetails.Cols[Col_DetailsProcessID].Visible = false;
                c1UploadedFileDetails.Cols[Col_DetailsPatientID].Visible = true;
                c1UploadedFileDetails.Cols[Col_DetailsPatientName].Visible = true;
                c1UploadedFileDetails.Cols[Col_DetailsDOS].Visible = true;
                c1UploadedFileDetails.Cols[Col_DetailsBalDue].Visible = true;
                c1UploadedFileDetails.Cols[Col_DetailsEncounterID].Visible = true;

                c1UploadedFileDetails.Cols[Col_DetailsPatientID].Width = (int)(155);
                c1UploadedFileDetails.Cols[Col_DetailsPatientName].Width = (int)(257);
                c1UploadedFileDetails.Cols[Col_DetailsDOS].Width = (int)(102);
                c1UploadedFileDetails.Cols[Col_DetailsBalDue].Width = (int)(102);
                c1UploadedFileDetails.Cols[Col_DetailsEncounterID].Width = (int)(185);

                if (c1UploadedFileDetails.DataSource != null && c1UploadedFileDetails.Rows.Count > 1) 
                {
                    c1UploadedFileDetails.Cols[Col_DetailsProcessID].TextAlign = TextAlignEnum.LeftCenter;
                    c1UploadedFileDetails.Cols[Col_DetailsPatientID].TextAlign = TextAlignEnum.LeftCenter;
                    c1UploadedFileDetails.Cols[Col_DetailsPatientName].TextAlign = TextAlignEnum.LeftCenter;
                    c1UploadedFileDetails.Cols[Col_DetailsDOS].TextAlign = TextAlignEnum.LeftCenter;
                    c1UploadedFileDetails.Cols[Col_DetailsBalDue].TextAlign = TextAlignEnum.RightCenter;
                    c1UploadedFileDetails.Cols[Col_DetailsEncounterID].TextAlign = TextAlignEnum.LeftCenter;
                    
                }

                c1UploadedFileDetails.AllowEditing = false;
                c1UploadedFileDetails.ExtendLastCol = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void ShowHideFooterLabels(bool bIsVisible)
        {
            try
            {
                lblFileUploadedDate.Visible = bIsVisible;
                lblFileUploadedDateValue.Visible = bIsVisible;
                lblUploadedFiles.Visible = bIsVisible;
                lblUploadedFilesCountValue.Visible = bIsVisible;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        #endregion
        
        #region "Binary to Text"

        public static string ConvertBinaryToFile(object cntFromDB, string sFileName)
        {            
            try
            {
                if ((cntFromDB != null))
                {
                    if (cntFromDB == System.DBNull.Value == false)
                    {
                        byte[] content = (byte[])cntFromDB;

                        string _FilePath = GenerateTempFileName(sFileName);
                        try
                        {
                            System.IO.FileStream oFile = new System.IO.FileStream(_FilePath, System.IO.FileMode.Create);

                            string a = System.Text.ASCIIEncoding.ASCII.GetString(content);
                            string[] str = a.Split('~');

                            using (StreamWriter sw = new StreamWriter(oFile, System.Text.Encoding.UTF8))
                            {
                                for (int i = 0; i < str.Length; i++)
                                {
                                    sw.WriteLine(str[i]);
                                }
                            }
                            
                            //...** Disposing and free memory resources 
                            try
                            {
                                //oFile.Flush(); oFile.Close(); 
                                oFile.Dispose(); oFile = null;
                                str = null;
                                content = null;
                            }
                            catch //(Exception ex)
                            {/*Blank catch*/ }

                            return _FilePath;
                        }
                        catch (IOException)
                        { }
                        catch(Exception) 
                        { }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            return null;
        }

        public static string GenerateTempFileName(string sFileName)
        {
            string _FullPath = "";
            try
            {
                string _FileName = "";
                if (sFileName.Trim() == "")
                    _FileName = "BinaryToFile " + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".txt";//DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid().ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + ".txt";
                else
                    _FileName = sFileName;

                string sApptemp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Windows.Forms.Application.ProductName);
                
                if (Directory.Exists(sApptemp) == false)
                {
                    Directory.CreateDirectory(sApptemp);
                }

                string _AppTempFolder = Path.Combine(sApptemp, "TEMP");
                if (Directory.Exists(_AppTempFolder) == false)
                {
                    Directory.CreateDirectory(_AppTempFolder);
                }

                _FullPath = Path.Combine(_AppTempFolder,_FileName);                

                while (File.Exists(_FullPath))
                {
                    if (sFileName == "")
                        _FullPath = _AppTempFolder + "BinaryToFile" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".txt";//DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid().ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + ".txt";
                    else
                    {
                        _FullPath = Path.Combine(_AppTempFolder, sFileName);
                        break;
                    }
                }                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            return _FullPath;
        }

        private void DeleteTempFile()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Windows.Forms.Application.ProductName, "TEMP");
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception)
            {   }
        }

        #endregion
                
    }
}
