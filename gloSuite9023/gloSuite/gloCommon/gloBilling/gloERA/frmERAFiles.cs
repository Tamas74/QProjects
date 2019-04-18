using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace gloBilling.gloERA
{
    public partial class frmERAFiles : Form
    {

        #region " Variable Declarations "
        ToolTip oToolTip;
        Font oFontRegular = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        Font oFontBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public string _DataBaseConnectionString;
        string _MessageBoxCaption;
           private gloERA oERA;
        private ClsERALogs oERALogs=new ClsERALogs(); 
        
        string _TempStr = "";
        string _ServerPath = "";
        string _835FolderPath = "";
        Int32 _Width;
        Int64 _TempID;

        #region " C1 Constants "

        private const int COL_FileID = 0;
        private const int COL_FileName = 1;
        private const int COL_OrigFileName = 2;
        private const int COL_ImportDate = 3;
        private const int COL_PayerID = 4;
        private const int COL_FileDate = 5;
        private const int COL_FileTime = 6;
        private const int COL_ControlNo = 7;
        private const int COL_CheckCount = 8;
        private const int COL_Status = 9;
        private const int COL_nStatus = 10;

        #endregion

        #region " Server Path Constants "
        private const string _BASE_FOLDER = "Claim Management";
        private const string _INBOX_FOLDER = "InBox";
        private const string _CLAIM_FOLDER = "835 Remittance Advice";
        #endregion

        #endregion

        #region " Public Properties "
        public string MessageLog
        { get; set; }
        #endregion

        #region " Constructor "
        public frmERAFiles()
        {
            #region " Get MessageBoxCaption from AppSettings "
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;    
            #endregion
            InitializeComponent();
        }
        #endregion

        #region " Form Events "

        private void frmERAFiles_Load(object sender, EventArgs e)
        {
            try
            {
                #region " Set ToolTip "
                oToolTip = new ToolTip();
                oToolTip.SetToolTip(btnClearSearch, "Clear Search");
                #endregion

                lblProgress.Visible = false;
                prgProgress.Visible = false;

                RefreshView();

                chkShowUnProcessed.Checked = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void frmERAFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                #region " Disposing ToolTip "
                if (oToolTip != null)
                {
                    oToolTip.RemoveAll();
                    oToolTip.Dispose();
                    oToolTip = null;
                }
                #endregion

                if (oERA != null) { oERA.Dispose(); oERA = null; }
                //if (oFontRegular != null) { oFontRegular.Dispose(); oFontRegular = null; }
                //if (oFontBold != null) { oFontBold.Dispose(); oFontBold = null; }
                
            }
            catch { }

        }

        #endregion

        #region " Tool Strip Events "

        private void tsb_Import_Click(object sender, EventArgs e)
        {
            try
            {
                #region " Import ERA Files "
                _ServerPath =  oERALogs.GetServerPath();
                OpenFileDialog oDialog = new OpenFileDialog();
                oDialog.Title = "Import";
                oDialog.Filter = "All Remittance Files (*.RMT;*.txt)|*.RMT;*.txt|Remittance Files (*.RMT)|*.RMT|Text Documents (*.txt)|*.txt";

                _835FolderPath = _ServerPath + "\\" + _BASE_FOLDER + "\\" + _INBOX_FOLDER + "\\" + _CLAIM_FOLDER;
                if (_ServerPath != "")
                    oDialog.InitialDirectory = _835FolderPath;
                oDialog.ValidateNames = true;
                oDialog.CheckFileExists = true;
                oDialog.CheckPathExists = true;
                oDialog.Multiselect = true;
                if (oDialog.ShowDialog(this) == DialogResult.OK)
                {
                    ImportERAFiles(oDialog.FileNames);
                }
                oDialog.Dispose();
                oDialog = null;

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Process_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1ERAFiles.RowSel >= 0)
                {
                    if (c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_Status).ToString() == "Un-Processed")
                    {
                        this.Cursor = Cursors.WaitCursor;
                        lblProgress.Visible = true;
                        prgProgress.Visible = true;
                        chkShowUnProcessed.Visible = false;

                        _TempStr = c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_FileName).ToString(); // AUTO GENERATED ERA FILE NAME FOR REFERENCE //
                        _TempID = Convert.ToInt64(c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_FileID));
                        oERA = new gloERA();
                        DataTable dtFile;
                        dtFile = oERA.GetERAFile(_TempID);
                        if (dtFile != null && dtFile.Rows.Count > 0)
                        {
                            gloGeneralItem.gloItems oItems = new gloGeneralItem.gloItems();
                            oItems.Add(0, _TempStr, Supporting.ConvertBinaryToFile(dtFile.Rows[0]["iBinaryFile"], ""));

                            if (oItems.Count > 0)
                                if (File.Exists(oItems[0].Description))
                                {
                                    oERA.ImportERAFiles(oItems, _TempID, _TempStr, ref prgProgress, ref lblProgress);
                                    RefreshView();
                                }
                            oItems.Clear();
                            oItems.Dispose();
                            oItems = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Processing ERA file failed", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                lblProgress.Visible = false;
                prgProgress.Visible = false;
                chkShowUnProcessed.Visible = true;
                this.Cursor = Cursors.Default;

                if (oERA != null)
                {
                    oERA.Dispose();
                    oERA = null;
                }
            }

        }

        private void tsb_UnProcess_Click(object sender, EventArgs e)
        {
            if (c1ERAFiles.RowSel >= 0)
            {
                oERA = new gloERA();
                _TempID = Convert.ToInt64(c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_FileID));
                if (oERALogs.IsPostedCheckPresent(_TempID))
                    MessageBox.Show("The selected ERA File cannot be un-processed because it contains posted ERA Payments.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (MessageBox.Show("Are you sure you want to un-process file?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        oERA.DeleteParsedData(_TempID);
                        RefreshView();
                    }
                }

                if (oERA != null) { oERA.Dispose(); oERA = null; }
            }
        }

        private void tsb_View_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1ERAFiles.RowSel >= 0)
                {
                    _TempID = Convert.ToInt64(c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_FileID));
                    _TempStr = Convert.ToString(c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_OrigFileName));
                    oERA = new gloERA();
                    DataTable dtFile;
                    dtFile = oERA.GetERAFile(_TempID);
                    if (dtFile != null && dtFile.Rows.Count > 0)
                    {
                        _TempStr = Supporting.ConvertBinaryToFile(dtFile.Rows[0]["iBinaryFile"], _TempStr);
                        if (File.Exists(_TempStr))
                            System.Diagnostics.Process.Start(_TempStr);
                    }
                    if (dtFile != null)
                    {
                        dtFile.Dispose();
                        dtFile = null;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oERA != null)
                {
                    oERA.Dispose();
                    oERA = null;
                }
            }
        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            if (c1ERAFiles.RowSel >= 0)
            {
                oERA = new gloERA();
                _TempID = Convert.ToInt64(c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_FileID));

                if (oERALogs.IsPostedCheckPresent(_TempID))
                    MessageBox.Show("The selected ERA File cannot be deleted because it contains posted ERA Payments.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    _TempStr = c1ERAFiles.GetData(c1ERAFiles.RowSel, COL_FileName).ToString();
                    if (MessageBox.Show("ERA file " + _TempStr + " will be deleted.\n\nAre you sure you want to delete file?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        oERALogs.DeleteERAFile(_TempID);
                        RefreshView();
                    }
                }
                if (oERA != null) { oERA.Dispose(); oERA = null; }
            }
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region " Private Methods "
        private void DesignGrid()
        {
            c1ERAFiles.Redraw = false;
            try
            {
                gloC1FlexStyle.Style(c1ERAFiles, true);

                c1ERAFiles.Rows.Fixed = 1;
                c1ERAFiles.AllowEditing = false;

                c1ERAFiles.Cols[COL_FileName].Caption = "File Name";
                c1ERAFiles.Cols[COL_OrigFileName].Caption = "Orig. File Name";
                c1ERAFiles.Cols[COL_ImportDate].Caption = "Import Date";
                c1ERAFiles.Cols[COL_PayerID].Caption = "Payer ID";
                c1ERAFiles.Cols[COL_FileDate].Caption = "File Date";
                c1ERAFiles.Cols[COL_FileTime].Caption = "File Time";
                c1ERAFiles.Cols[COL_ControlNo].Caption = "Control Number";
                c1ERAFiles.Cols[COL_CheckCount].Caption = "# Checks";
                c1ERAFiles.Cols[COL_Status].Caption = "Status";

                _Width = c1ERAFiles.Width;

                c1ERAFiles.Cols[COL_FileName].Width = (Int32)(_Width * 0.1);
                c1ERAFiles.Cols[COL_OrigFileName].Width = (Int32)(_Width * 0.15);
                c1ERAFiles.Cols[COL_ImportDate].Width = (Int32)(_Width * 0.1);
                c1ERAFiles.Cols[COL_PayerID].Width = (Int32)(_Width * 0.1);
                c1ERAFiles.Cols[COL_FileDate].Width = (Int32)(_Width * 0.1);
                c1ERAFiles.Cols[COL_FileTime].Width = (Int32)(_Width * 0.1);
                c1ERAFiles.Cols[COL_ControlNo].Width = (Int32)(_Width * 0.15);
                c1ERAFiles.Cols[COL_CheckCount].Width = (Int32)(_Width * 0.1);
                c1ERAFiles.Cols[COL_Status].Width = (Int32)(_Width * 0.1);

                c1ERAFiles.Cols[COL_FileID].Visible = false;
                c1ERAFiles.Cols[COL_nStatus].Visible = false;

                // Date Format and Sorting Mahesh Nawal
                c1ERAFiles.Cols[COL_ImportDate].DataType = typeof(System.DateTime);
                c1ERAFiles.Cols[COL_ImportDate].Format = "MM/dd/yyyy";
                c1ERAFiles.Cols[COL_FileDate].DataType = typeof(System.DateTime);
                c1ERAFiles.Cols[COL_FileDate].Format = "MM/dd/yyyy";
                c1ERAFiles.Cols["SearchImportDate"].Visible = false;
                c1ERAFiles.Cols["SearchFileDate"].Visible = false; 
  

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                c1ERAFiles.Redraw = true;
            }
        }

        private void RefreshView()
        {
            DataTable dtFiles;
            try
            {
                dtFiles = oERALogs.GetERAFiles();
                if (dtFiles != null)
                {
                    c1ERAFiles.DataSource = dtFiles.DefaultView;
                    if (chkShowUnProcessed.Checked)
                        ((DataView)c1ERAFiles.DataSource).RowFilter = "Status = 'Un-Processed'";
                    else
                        ((DataView)c1ERAFiles.DataSource).RowFilter = "";

                    DesignGrid();

                    c1ERAFiles_AfterRowColChange(null, null);
                }


                if (txtSearch.Text.Trim() != "")
                {
                    txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
                    txtSearch.Clear();
                    txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ImportERAFiles(string[] _ImportedFiles)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Refresh();
                lblProgress.Visible = true;
                prgProgress.Visible = true;
                chkShowUnProcessed.Visible = false;

                oERA = new gloERA();
                string[] _FileNames = _ImportedFiles;
                gloGeneralItem.gloItems _SplitedFiles;
                string _InvalidFiles = "";
                string _MessageLog = "";

                #region " CHECK FOR FILE WITH .TXT & .RMT ONLY "
                for (int i = 0; i < _FileNames.Length; i++)
                {
                    if (Path.GetExtension(_FileNames[i]).ToUpper() != ".TXT" && Path.GetExtension(_FileNames[i]).ToUpper() != ".RMT")
                    {
                        _InvalidFiles = _InvalidFiles + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "   Invalid ERA File selected " + Path.GetFileName(_FileNames[i]) + "." + Environment.NewLine;
                        _FileNames[i] = "";
                    }
                }
                #endregion

                _SplitedFiles = GetSplitedFiles(_FileNames);

                if (_SplitedFiles != null)
                    _MessageLog = oERA.ImportERAFiles(_SplitedFiles, 0, "", ref prgProgress, ref lblProgress);

                MessageLog = _InvalidFiles + _MessageLog;

                if (oERA != null) { oERA.Dispose(); oERA = null; }
                if (_SplitedFiles != null) { _SplitedFiles.Dispose(); _SplitedFiles = null; }

                RefreshView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor = Cursors.Default;
                prgProgress.Visible = false;
                lblProgress.Visible = false;
                chkShowUnProcessed.Visible = true;
            }
        }

        public gloGeneralItem.gloItems GetSplitedFiles(string[] sFileNames)
        {
            gloGeneralItem.gloItems oFiles = new gloGeneralItem.gloItems();
            ArrayList _SplittedFiles;
            try
            {
                for (int i = 0; i < sFileNames.Length; i++)
                {
                    if (sFileNames[i] != "")
                    {
                        _SplittedFiles = SplitFile(sFileNames[i]);
                        if (_SplittedFiles != null && _SplittedFiles.Count > 0)
                        {
                            for (int j = 0; j < _SplittedFiles.Count; j++)
                            {
                                if (_SplittedFiles.Count > 1)
                                    oFiles.Add(j + 1, sFileNames[i].ToString(), _SplittedFiles[j].ToString());
                                else
                                    oFiles.Add(0, sFileNames[i].ToString(), _SplittedFiles[j].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            return oFiles;
        }

        private ArrayList SplitFile(string sFileName)
        {
            ArrayList _ArrSplitedFiles = new ArrayList();
            FileStream oFileRead = null;
            FileStream oFileSplit = null;
            StreamReader oReader = null;
            StreamWriter oWriter = null;
            try
            {
                File.SetAttributes(sFileName, FileAttributes.Normal);
                oFileRead = new FileStream(sFileName, FileMode.Open);
                oReader = new StreamReader(oFileRead);
                string _FileData = oReader.ReadToEnd();
                string[] _SplitString = { "ISA*0" };
                string[] _SplitData = _FileData.Split(_SplitString, StringSplitOptions.RemoveEmptyEntries);

                if (_SplitData.Length == 1)
                    _ArrSplitedFiles.Add(sFileName);
                else
                {
                    #region " CREATE NEW FILES PER ISA "

                    for (int i = 0; i < _SplitData.Length; i++)
                    {
                        _TempStr = Supporting.GenerateTempFileName("");
                        oFileSplit = new FileStream(_TempStr, FileMode.CreateNew);
                        oWriter = new StreamWriter(oFileSplit);
                        oWriter.Write("ISA*0" + _SplitData[i]);
                        _ArrSplitedFiles.Add(_TempStr);
                        oWriter.Close();
                        oFileSplit.Close();
                    }

                    #endregion
                }


                oReader.Close();
                oFileRead.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                throw ex;
            }
            finally
            {
                if (oFileRead != null) { oFileRead.Dispose(); oFileRead = null; }
                if (oFileSplit != null) { oFileSplit.Dispose(); oFileSplit = null; }
                if (oReader != null) { oReader.Dispose(); oReader = null; }
                if (oWriter != null) { oWriter.Dispose(); oWriter = null; }
            }
            return _ArrSplitedFiles;
        }
            
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();
        }

        private void chkShowUnProcessed_CheckedChanged(object sender, EventArgs e)
        {

            if (txtSearch.Text.Trim() != "")
            {
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
                txtSearch.Clear();
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            }

            if (chkShowUnProcessed.Checked == true)
            {
                chkShowUnProcessed.Font = oFontBold;
                if (c1ERAFiles.DataSource != null)
                    ((DataView)c1ERAFiles.DataSource).RowFilter = "Status = 'Un-Processed'";
            }
            else
            {
                chkShowUnProcessed.Font = oFontRegular;
                if (c1ERAFiles.DataSource != null)
                    ((DataView)c1ERAFiles.DataSource).RowFilter = "";
            }
            c1ERAFiles_AfterRowColChange(null, null);
        }

        private void c1ERAFiles_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                Int32 _RowSel = c1ERAFiles.RowSel;
                if (_RowSel >= 0)
                {
                    if (c1ERAFiles.GetData(_RowSel, COL_Status) != null)
                    {
                        string _Status = c1ERAFiles.GetData(_RowSel, COL_Status).ToString();
                        if (_Status == "Processed")
                        {
                            tsb_Process.Visible = false;
                            tsb_UnProcess.Visible = true;
                            tsb_Delete.Visible = true;
                        }
                        else if (_Status == "Un-Processed")
                        {
                            tsb_Process.Visible = true;
                            tsb_UnProcess.Visible = false;
                            tsb_Delete.Visible = true;
                        }
                        else if (_Status == "Finished")
                        {
                            tsb_Process.Visible = false;
                            tsb_UnProcess.Visible = false;
                            tsb_Delete.Visible = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            DataView _dv = null;
            C1.Win.C1FlexGrid.C1FlexGrid _C1 = null;
            try
            {

                string _SearchText = txtSearch.Text.Trim();
                string _Filter = "";

                _SearchText = _SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]").Replace("*", "%");

                _dv = (DataView)c1ERAFiles.DataSource;
                _C1 = c1ERAFiles;
                if (_dv != null)
                {
                    #region " SEARCH "
                    if (_SearchText == "")
                        if (chkShowUnProcessed.Checked == true)
                            _dv.RowFilter = "Status = 'Un-Processed'";
                        else
                            _dv.RowFilter = "";

                    else
                    {
                        if (_SearchText.Contains(",") == false)
                        {
                            #region " Simple Search "

                            _Filter = _C1.Cols[COL_FileName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_OrigFileName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["SearchImportDate"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_PayerID].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["SearchFileDate"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_FileTime].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_ControlNo].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_CheckCount].Name + " LIKE '" + _SearchText + "%' OR " +
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
                                        _C1.Cols[COL_FileName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_OrigFileName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["SearchImportDate"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_PayerID].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["SearchFileDate"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_FileTime].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_ControlNo].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_CheckCount].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_Status].Name + " LIKE '" + _SplitString + "%' " +
                                        " ) ";
                                }
                            }

                            #endregion
                        }

                        if (chkShowUnProcessed.Checked == true)
                            _Filter = "( " + _Filter + " ) AND Status = 'Un-Processed'";

                        _dv.RowFilter = _Filter;
                    }
                    #endregion
                }
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
        #endregion

    }
}