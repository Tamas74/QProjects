using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using gloAuditTrail;
using gloCommon;
using System.Data.SqlClient;
namespace gloPMClaimService
{
    public partial class frmDownloadFileList : Form
    {

        #region " Variable Declarations "

        


        private const int COL_NAME = 0;
        private const int COL_SIZE = 1;
        private const int COL_DATE = 2;
        private const int COL_SEARCHSIZE = 3;
        private string[] _keepRow = null;
        private Rebex.Net.FtpList _ftpFileList = null;
        private DialogResult _dlgRslt = DialogResult.None;
        private Rebex.Net.FtpList _ftpselectedFileList = new Rebex.Net.FtpList();//  null;
        private string _MessageBoxCaption = "gloPM";
        private string _DataBaseConnectionString = "";
        private Int64 _ClinicID = 1;

        private Rebex.Net.SftpItemCollection _secureftpFileList = null;
        private bool _isSecureFTP = false;
        private Rebex.Net.SftpItemCollection _sftpselectedFileList = new Rebex.Net.SftpItemCollection();// null;
        public bool _IsDownloadAll = false;
        //private String[] _arrLocalFiles = null;
        private String _downloadpath = "";
       // private int _idealtime=0 ;
        public Int64 nCategoryID = 0;
        public string sCategoryName = "";

        
        #endregion " Variable Declarations "

        #region " Property Procedures "
        public DialogResult DlgResult
        {
            get { return _dlgRslt; }
            set { _dlgRslt = value; }
        }

        public Rebex.Net.FtpList FtpSelectedFileList
        {
            get { return _ftpselectedFileList; }
            set { _ftpselectedFileList = value; }
        }

        public Rebex.Net.SftpItemCollection SecureFtpSelectedFileList
        {
            get { return _sftpselectedFileList; }
            set { _sftpselectedFileList = value; }
        }

        public bool IsDownloadImportClicked
        { get; set; }

        public bool IsMoveToRCMClicked
        { get; set; }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmDownloadFileList(Rebex.Net.FtpList FTPFileList, Rebex.Net.SftpItemCollection SFTPFileList, String downloadpath, bool IsSFTP)
        {
            InitializeComponent();

            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption; 

            _downloadpath = downloadpath;

            _ftpFileList = FTPFileList;
            _secureftpFileList = SFTPFileList;
            _isSecureFTP = IsSFTP;


        }

        #endregion " Constructor "

        #region  " Form Load Event "

        private void frmDownloadFileList_Load(object sender, EventArgs e)
        {
            try
            {
                tsb_MoveToRCM.Enabled = false;
                GetExtensions();
                FillCategoryCombo();
                pnlRCMDOCCategory.Visible = false;
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);
                
                //ODB.Retrive_Query("select sOriginalFileName from ERA_Files", out _Dresult);
                LoadList();
              //  _idealtime= System.DateTime.Now.TimeOfDay.Minutes;      
                
                


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion

        #region " ToolStrip Button Click Event "

        private void tsb_Select_All_Click(object sender, EventArgs e)
        {
            //listServer.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(listServer_ItemSelectionChanged);
            listServer.SelChange -= new EventHandler(listServer_SelChange);
            try
            {
                for (int i = 1; i <listServer.Rows.Count; i++)
                {
                    listServer.Rows[i].Selected = true;
                }
                listServer.Select();
                tsb_Select_All.Visible = false;
                tsb_Clear_All.Visible = true;
            }
            catch (Exception ex) { ex.ToString(); }
            finally
            {
                // listServer.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(listServer_ItemSelectionChanged);
                listServer.SelChange += new EventHandler(listServer_SelChange);
                listServer.Styles.Focus.BackColor = System.Drawing.Color.LightSkyBlue;
            }


        }

        private void tsb_Clear_All_Click(object sender, EventArgs e)
        {
            //listServer.ItemSelectionChanged -= new ListViewItemSelectionChangedEventHandler(listServer_ItemSelectionChanged);
            listServer.SelChange -= new EventHandler(listServer_SelChange);
            try
            {
                for (int i = 1; i <listServer.Rows.Count; i++)
                {
                    listServer.Rows[i].Selected = false;
                }
                listServer.Select();
                tsb_Select_All.Visible = true;
                tsb_Clear_All.Visible = false;
            }
            catch (Exception ex) { ex.ToString(); }
            finally
            {
                // listServer.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(listServer_ItemSelectionChanged);
                listServer.SelChange += new EventHandler(listServer_SelChange);
                listServer.Styles.Focus.BackColor = System.Drawing.Color.Transparent; 
            }

        }

        private void tsb_Download_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (listServer.Rows.Selected.Count > 0)
                {
                    // _idealtime = (System.DateTime.Now.TimeOfDay.Minutes-_idealtime);
                    //if (_idealtime < -2 || _idealtime > 2)
                    //{
                    //    MessageBox.Show("Required time for Download files Expired..");
                    //    this.Close();
                    //    return; 
                    //}

                  
                      bool bIsPDFfound = false;
                        for (int i = 0; i <= listServer.Rows.Selected.Count-1; i++)
                        {
                            string sExtension = Path.GetExtension(Convert.ToString(listServer.Rows.Selected[i][0]));
                            if (sExtension == ".pdf")
                            {
                                bIsPDFfound = true;
                                break;
                            }

                        }
                     List<string> list = new List<string>();
                     if (listServer.Rows.Selected.Count >= 1)
                     {
                         for (int i = 0; i <= listServer.Rows.Selected.Count - 1; i++)
                         {
                             if (list.Contains(Path.GetExtension(Convert.ToString(listServer.Rows.Selected[i][0]))) == false)
                             {
                                 list.Add(Path.GetExtension(Convert.ToString(listServer.Rows.Selected[i][0]))); //list contains all selected files extension
                             }
                         }
                         if (list.Contains(".pdf") && list.Count > 1) //list contains .pdf files and other extension files 
                         {
                             if (MessageBox.Show("Download selected files can be found under the '" + _downloadpath + "' " +
                                Environment.NewLine + Environment.NewLine + "Download only selected files?",
                                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                 return;
                         }
                     }

                        if (!IsDownloadImportClicked && !IsMoveToRCMClicked && bIsPDFfound == false)
                            if (MessageBox.Show("The selected ERA files will need to be imported after download is complete. " +
                                Environment.NewLine + "Download ERA files can be found under the '" + _downloadpath + "' " +
                                Environment.NewLine + Environment.NewLine + "Download only selected ERA files?",
                                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;
                        if (!IsMoveToRCMClicked && !IsDownloadImportClicked && bIsPDFfound == true)
                            if (MessageBox.Show("Download pdf files can be found under the '" + _downloadpath + "' " +
                                Environment.NewLine + Environment.NewLine + "Download only selected pdf files?",
                                _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;
                    
                    if (_isSecureFTP == false)
                    {
                        if (listServer.Rows.Selected.Count > 0)
                        {
                            _ftpselectedFileList = new Rebex.Net.FtpList();
                            //  int _selectedIndex = listServer.SelectedIndices[0];
                            int _selectedIndex = listServer.Rows.Selected[0].Index;
                            // string _selectedItem = listServer.SelectedItems[0].SubItems[0].Text;
                            string _selectedItem = listServer.GetData(_selectedIndex, COL_NAME).ToString();
                            bool _isFileSelected = false;

                            for (int i = 0; i < _ftpFileList.Count; i++)
                            {
                                if (i == _selectedIndex && _selectedItem.ToUpper() == _ftpFileList[i].Name.ToUpper())
                                {
                                    _ftpselectedFileList.Add(_ftpFileList[i]);
                                    _dlgRslt = DialogResult.OK;
                                    _isFileSelected = true;
                                    break;
                                }
                            }

                            if (_isFileSelected == false) { _dlgRslt = DialogResult.None; _ftpselectedFileList = null; }
                            this.Close();
                        }
                      
                    }
                    else
                    {
                        if (listServer.Rows.Selected.Count > 0)
                        {
                            bool _isFileSelected = false;
                            int _selectedIndex = 0;
                            string _selectedItem = "";

                            _sftpselectedFileList = new Rebex.Net.SftpItemCollection();

                            for (int iList = 0; iList < listServer.Rows.Selected.Count; iList++)
                            {
                                //_selectedIndex = listServer.SelectedIndices[iList];
                                _selectedIndex = listServer.Rows.Selected[iList].Index;
                                // _selectedItem = listServer.SelectedItems[iList].SubItems[0].Text;
                                _selectedItem = listServer.GetData(_selectedIndex, COL_NAME).ToString();

                                //Original Code
                                for (int i = 0; i < _secureftpFileList.Count; i++)
                                {

                                    // if (i == _selectedIndex && _selectedItem.ToUpper() == _secureftpFileList[i].Name.ToUpper())
                                    if (_selectedItem.ToUpper() == _secureftpFileList[i].Name.ToUpper())
                                    {
                                        _sftpselectedFileList.Add(_secureftpFileList[i]);
                                        break;
                                    }
                                }

                                _dlgRslt = DialogResult.OK;
                                _isFileSelected = true;
                            }

                            if (_isFileSelected == false) { _dlgRslt = DialogResult.None; _sftpselectedFileList = null; }
                            this.Close();
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Select File", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            _dlgRslt = DialogResult.None;
            _ftpselectedFileList = null;
            _secureftpFileList = null;
            this.Close();
        }

        private void tsb_DownloadAll_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < listServer.Items.Count; i++)
            //{
            //    listServer.Items[i].Selected = true;
            //}
            try
            {
                //for (int i = 0; i < _ftpFileList.Count; i++)
                //{
                //    if (i == _selectedIndex && _selectedItem.ToUpper() == _ftpFileList[i].Name.ToUpper())
                //    {
                //        _ftpselectedFileList.Add(_ftpFileList[i]);
                //        _dlgRslt = DialogResult.OK;
                //        _isFileSelected = true;
                //        break;
                //    }
                //}
                //_ftpselectedFileList.GetFiles(remotePath, localPath, FtpBatchTransferOptions.Recursive);
                bool _isFileSelected = false;
                _sftpselectedFileList = new Rebex.Net.SftpItemCollection();
                for (int i = 0; i < _secureftpFileList.Count; i++)
                {

                    _sftpselectedFileList.Add(_secureftpFileList[i]);

                    _dlgRslt = DialogResult.OK;
                    _isFileSelected = true;

                }

                if (_isFileSelected == false) { _dlgRslt = DialogResult.None; _sftpselectedFileList = null; }
                _IsDownloadAll = true;
                this.Close();



            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_DownloadImport_Click(object sender, EventArgs e)
        {
            IsDownloadImportClicked = true;
            tsb_Download_Click(sender, e);


        }

        #endregion

        #region " Private Methods "
        private static string FormatFileTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm");

        }

        private void LoadList()
        {

            try
            {

                ArrayList _AvailableFileList = new ArrayList();
                //string lookfor = "*.RMT;*.txt";
                //string[] extensions = lookfor.Split(new char[] { ';' });
                String[] _arrLocalFilesRMT = System.IO.Directory.GetFiles(_downloadpath, "*.RMT");
                String[] _arrLocalFilestxt = System.IO.Directory.GetFiles(_downloadpath, "*.txt");
                DataTable _dtDBFiles = new DataTable();
                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                ODB.Connect(false);
                ODB.Retrive_Query("SELECT ISNULL(sOriginalFileName,'') AS sOriginalFileName FROM ERA_Files", out _dtDBFiles);

                // GET txt FILE LIST FROM SERVER PATH //
                for (int i = 0; i < _arrLocalFilestxt.Length; i++)
                {
                    if (!_AvailableFileList.Contains(Path.GetFileName(_arrLocalFilestxt[i])))
                        _AvailableFileList.Add(Path.GetFileName(_arrLocalFilestxt[i]));

                }

                // GET RMT FILE LIST FROM SERVER PATH //
                for (int i = 0; i < _arrLocalFilesRMT.Length; i++)
                {
                    if (!_AvailableFileList.Contains(Path.GetFileName(_arrLocalFilesRMT[i])))
                        _AvailableFileList.Add(Path.GetFileName(_arrLocalFilesRMT[i]));

                }
                if (_dtDBFiles != null)
                {
                    // GET FILE LIST FROM ERA_FILES //
                    for (int j = 0; j < _dtDBFiles.Rows.Count; j++)
                    {
                        if (!_AvailableFileList.Contains(_dtDBFiles.Rows[j]["sOriginalFileName"].ToString()))
                            _AvailableFileList.Add(_dtDBFiles.Rows[j]["sOriginalFileName"].ToString());
                    }
                }

                if (_isSecureFTP == false)
                {

                    if (_ftpFileList != null && _ftpFileList.Count > 0)
                    {
                        // files
                        Rebex.Net.FtpList _FileListftp = new Rebex.Net.FtpList();
                        for (int c = 0; c < _ftpFileList.Count; c++)
                        {
                            String Extension = Path.GetExtension(_ftpFileList[c].Name.ToLower());
                            if (Convert.ToString(cmbFileType.SelectedItem) == Path.GetExtension(_ftpFileList[c].Name))
                            {
                                _FileListftp.Add(_ftpFileList[c]);
                            }
                            else if (Convert.ToString(cmbFileType.SelectedItem) == "")
                            {
                                _FileListftp.Add(_ftpFileList[c]);
                            }
                        }

                        DesignListServerGrid(_FileListftp.Count);
                        for (int c = 0; c < _FileListftp.Count; c++)
                        {
                            // normal file
                            if (_FileListftp[c].IsFile)
                                {
                                    int i;
                                    string[] row = new string[3];
                                    row[0] = _FileListftp[c].Name;
                                    row[1] = _FileListftp[c].Size.ToString();
                                    row[2] = FormatFileTime(_FileListftp[c].Modified);

                                    for (i = 0; i < _arrLocalFilesRMT.Length; i++)
                                    {
                                        if (_FileListftp[c].Name == _arrLocalFilesRMT[i].ToString())
                                            break;
                                    }
                                    if (i == _arrLocalFilesRMT.Length)
                                    {
                                    }

                                    else
                                    {
                                        // listServer.Items.Add(new ListViewItem(row));
                                        listServer.SetData(c + 1, COL_NAME, row[0]);
                                        listServer.SetData(c + 1, COL_SIZE, row[1]);
                                        listServer.SetData(c + 1, COL_DATE, row[2]);


                                    }

                                }

                        
                    
                    
                            else
                            {
                                if (_ftpFileList[c].IsFile)
                                {
                                    int i;
                                    string[] row = new string[3];
                                    row[0] = _ftpFileList[c].Name;
                                    row[1] = _ftpFileList[c].Size.ToString();
                                    row[2] = FormatFileTime(_ftpFileList[c].Modified);

                                    for (i = 0; i < _arrLocalFilesRMT.Length; i++)
                                    {
                                        if (_ftpFileList[c].Name == _arrLocalFilesRMT[i].ToString())
                                            break;
                                    }
                                    if (i == _arrLocalFilesRMT.Length)
                                    {
                                    }

                                    else
                                    {
                                        // listServer.Items.Add(new ListViewItem(row));
                                        listServer.SetData(c + 1, COL_NAME, row[0]);
                                        listServer.SetData(c + 1, COL_SIZE, row[1]);
                                        listServer.SetData(c + 1, COL_DATE, row[2]);


                                    }
                                }
                            }
                        }
                       
                    }

                    else
                    {
                        MessageBox.Show("No files found", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _ftpselectedFileList = null;
                        _dlgRslt = DialogResult.None;
                        this.Close();
                    }
                }
                else
                {
                    if (_secureftpFileList != null && _secureftpFileList.Count > 0)
                    {
                        // files
                        
                        Rebex.Net.SftpItemCollection _FileList = new Rebex.Net.SftpItemCollection();
                        for (int i = 0; i < _secureftpFileList.Count; i++)
                        {
                            string Extension = Path.GetExtension(_secureftpFileList[i].Name.ToLower());
                            if (Convert.ToString(cmbFileType.SelectedItem) == Extension)
                            {
                                _FileList.Add(_secureftpFileList[i]);
                            }
                            else if (Convert.ToString(cmbFileType.SelectedItem) == "")
                            {
                                _FileList.Add(_secureftpFileList[i]);
                            }
                        }
                        Int64 _size = 0;
                        DesignListServerGrid(_FileList.Count);
                        string[] row = new string[4]; 
                        for (int c = 0; c < _FileList.Count; c++)
                        {
                            // normal file


                            if (_FileList[c].IsFile)
                            {

                                row[0] = _FileList[c].Name;

                                _size = Convert.ToInt64(_FileList[c].Size) / 1024;
                                if (_size == 0)
                                    row[1] = _FileList[c].Size.ToString() + " B";
                                else
                                    row[1] = _size.ToString() + " KB";
                                row[2] = FormatFileTime(_FileList[c].Modified);
                                // row[3] = _secureftpFileList[c].Size.ToString();
                                // COMMENTED BY SUDHIR 20100818 // TO SHOW ALL FILES FROM FTP LIST //
                                // IF UNCOMMENT THEN IT WILL HIDE THE FILES WHICH ARE PRESENT UNDER gloPM SERVER PATH & DATABASE //
                                // if (!_AvailableFileList.Contains(_secureftpFileList[c].Name))


                                listServer.SetData(c + 1, COL_NAME, row[0]);
                                listServer.SetData(c + 1, COL_SIZE, row[1]);
                                listServer.SetData(c + 1, COL_DATE, row[2]);
                                listServer.SetData(c + 1, COL_SEARCHSIZE, _FileList[c].Size);

                                //listServer.Items.Add(new ListViewItem(row));
                            }




                        }
                        //for (int c = 0; c < _secureftpFileList.Count; c++)
                        //{
                        //    // normal file
                            

                        //        if (_secureftpFileList[c].IsFile)
                        //        {

                        //            row[0] = _secureftpFileList[c].Name;

                        //            _size = Convert.ToInt64(_secureftpFileList[c].Size) / 1024;
                        //            if (_size == 0)
                        //                row[1] = _secureftpFileList[c].Size.ToString() + " B";
                        //            else
                        //                row[1] = _size.ToString() + " KB";
                        //            row[2] = FormatFileTime(_secureftpFileList[c].Modified);
                        //            // row[3] = _secureftpFileList[c].Size.ToString();
                        //            // COMMENTED BY SUDHIR 20100818 // TO SHOW ALL FILES FROM FTP LIST //
                        //            // IF UNCOMMENT THEN IT WILL HIDE THE FILES WHICH ARE PRESENT UNDER gloPM SERVER PATH & DATABASE //
                        //            // if (!_AvailableFileList.Contains(_secureftpFileList[c].Name))


                        //            listServer.SetData(c + 1, COL_NAME, row[0]);
                        //            listServer.SetData(c + 1, COL_SIZE, row[1]);
                        //            listServer.SetData(c + 1, COL_DATE, row[2]);
                        //            listServer.SetData(c + 1, COL_SEARCHSIZE, _secureftpFileList[c].Size);

                        //            //listServer.Items.Add(new ListViewItem(row));
                        //        }
                            
                            
                              

                        //}
                       
                    }
                   
                    else
                    {
                        MessageBox.Show("No files found", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _secureftpFileList = null;
                        _dlgRslt = DialogResult.None;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        private void GetExtensions()
        {
            List<string> list = new List<string>();
            if (_isSecureFTP == false)
            {

                if (_ftpFileList != null && _ftpFileList.Count > 0)
                {
                    // files
                  
                    for (int c = 0; c < _ftpFileList.Count; c++)
                    {
                        // normal file
                        if (_ftpFileList[c].IsFile)
                        {

                            if (list.Contains(Path.GetExtension(_ftpFileList[c].Name.ToLower())) == false)
                            {
                                list.Add(Path.GetExtension(_ftpFileList[c].Name.ToLower()));
                            }
                        }
                    }
                }
            }
            else
            {
                if (_secureftpFileList != null && _secureftpFileList.Count > 0)
                {
                    for (int c = 0; c < _secureftpFileList.Count; c++)
                    {
                        // normal file

                        if (_secureftpFileList[c].IsFile)
                        {

                            if (list.Contains(Path.GetExtension(_secureftpFileList[c].Name.ToLower())) == false)
                            {
                                list.Add(Path.GetExtension(_secureftpFileList[c].Name.ToLower()));
                            }
                        }
                    }
                }
            }
            foreach(var item in list)
            {
                if (item.ToLower() == ".txt" || item.ToLower() == ".RTM")
                {
                    panel2.Visible=false;
                    tsb_Select_All.Enabled = true;
                }
                else if (item.ToLower() == ".pdf")
                {
                    panel2.Visible=true;
                    tsb_Select_All.Enabled = false;
                    break;
                }
            }
            list.Insert(0, "");
            cmbFileType.SelectedIndexChanged -= new EventHandler(cmbFileType_SelectedIndexChanged);
            cmbFileType.DataSource = list;
            cmbFileType.DisplayMember = Convert.ToString(list);
            cmbFileType.SelectedIndexChanged += new EventHandler(cmbFileType_SelectedIndexChanged);
        }


        private void listServer_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //try
            //{
            //    bool _Disselected = false;
            //    for (int i = 0; i < listServer.Items.Count; i++)
            //    {
            //        if (!listServer.Items[i].Selected)
            //        {
            //            _Disselected = true;
            //            break;
            //        }
            //    }

            //    tsb_Clear_All.Visible = !_Disselected;
            //    tsb_Select_All.Visible = _Disselected;

            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //    ex = null;
            //}
        }

        private void DesignListServerGrid(int RowCount)
        {
            listServer.SelChange -= new EventHandler(listServer_SelChange); 
            listServer.ExtendLastCol = true;
            listServer.Clear();
            listServer.Rows.Count = RowCount + 1;
            listServer.Rows.Fixed = 1;
            listServer.Cols.Count = 4;
            listServer.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            #region "SET HEADER"
            listServer.SetData(0, COL_NAME, "Name");
            listServer.SetData(0, COL_SIZE, "Size");
            listServer.SetData(0, COL_DATE, "Date");
            listServer.SetData(0, COL_SEARCHSIZE, "Size");
            # endregion
            listServer.Cols[COL_NAME].Width = (Int32 )(listServer.Width * 0.60);
            listServer.Cols[COL_SIZE].Width = (Int32)(listServer.Width * 0.15);
            listServer.Cols[COL_DATE].Width = (Int32)(listServer.Width * 0.2);
            listServer.Cols[COL_SEARCHSIZE].Width = 0;
            listServer.Cols[COL_NAME].AllowEditing = false;
            listServer.Cols[COL_SIZE].AllowEditing = false;
            listServer.Cols[COL_DATE].AllowEditing = false;
            listServer.Cols[COL_SEARCHSIZE].Visible = false;
            listServer.Cols[COL_SEARCHSIZE].DataType = typeof(long);
            listServer.Select(-1, -1);
            listServer.SelChange += new EventHandler(listServer_SelChange);

        }
        private void listServer_SelChange(object sender, EventArgs e)
        {
            try
            {
                List<string> list1 = new List<string>();
                bool _Disselected = false;
                for (int i = 1; i <listServer.Rows.Count; i++)
                {
                    // if (!listServer.Items[i].Selected)
                    if (!listServer.Rows[i].Selected)
                    {
                        _Disselected = true;
                        break;
                    }
                    
                }
                if (listServer.Rows[listServer.RowSel.GetHashCode()].Selected)
                {
                    listServer.Styles.Focus.BackColor = System.Drawing.Color.LightSkyBlue;
                   

                }
                else
                {

                    listServer.Styles.Focus.BackColor = System.Drawing.Color.Transparent;
                }
                    tsb_Clear_All.Visible = !_Disselected;
                    tsb_Select_All.Visible = _Disselected;

                //if (listServer.Rows.Selected.Count == 1)
                //{
                //   // string extension = Path.GetExtension(listServer.Selection.Clip);
                //    if (Path.GetExtension((listServer.Selection.Clip)) == ".pdf" || cmbFileType.Text == ".pdf")
                //    {
                //        tsb_MoveToRCM.Enabled = true;
                //        pnlRCMDOCCategory.Visible = true;
                //        tsb_DonwloadImport.Enabled = false;


                //    }
                //    else
                //    {
                //        tsb_MoveToRCM.Enabled = false;
                //        pnlRCMDOCCategory.Visible = false;
                //        tsb_DonwloadImport.Enabled = true;
                //    }
                //}
                    List<string> list = new List<string>();
                    

                if (listServer.Rows.Selected.Count >= 1)
                {
                    for (int i = 1; i < listServer.Rows.Count; i++)
                    {
                        if (listServer.Rows[i].Selected)
                        {
                            if (list.Contains(Path.GetExtension(Convert.ToString(listServer.Rows[i][0]).ToLower())) == false)
                            {
                                list.Add(Path.GetExtension(Convert.ToString(listServer.Rows[i][0]).ToLower())); //list contains all selected files extension
                            }
                        }
                    }
                    if (list.Contains(".pdf") && list.Count>1) //list contains .pdf files and other extension files 
                        {
                            tsb_MoveToRCM.Enabled = false;
                            pnlRCMDOCCategory.Visible = false;
                            tsb_DonwloadImport.Enabled = false;
                        }
                        else if (list.Contains(".pdf"))          //list contains only .pdf files
                        {
                            tsb_MoveToRCM.Enabled = true;
                            pnlRCMDOCCategory.Visible = true;
                            tsb_DonwloadImport.Enabled = false;
                             
                        }
                        else if (!list.Contains(".pdf"))           //list contains other then .pdf files
                        {
                            tsb_MoveToRCM.Enabled = false;
                            pnlRCMDOCCategory.Visible = false;
                            tsb_DonwloadImport.Enabled = true;
                        }
                         



                    


                }
                
                

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

        

        }

        private void listServer_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            // listServer.SelChange -= new EventHandler(listServer_SelChange);
            int nSortColumn = 0;            
            C1.Win.C1FlexGrid.C1FlexGrid _C1 = (C1.Win.C1FlexGrid.C1FlexGrid)sender;
            if (e.Col != COL_SIZE)
                nSortColumn = e.Col;
            else
                nSortColumn = COL_SEARCHSIZE;
            
            _C1.Sort(e.Order, nSortColumn);

            for (int i = 0; i < _keepRow.Length; i++)
            {
                for (int iList = 0; iList < listServer.Rows.Count; iList++)
                {
                    if (_keepRow[i]==listServer.GetData(iList, COL_NAME).ToString())
                    {
                        listServer.Rows[iList].Selected = true;
                        break;
                    }

                }

            }
            listServer.SelChange += new EventHandler(listServer_SelChange);
        }

        private void listServer_BeforeSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            //CurrencyManager cm =(CurrencyManager)BindingContext[this.listServer.DataSource,this.listServer.DataMember.ToString()]       ;
            //for (int index = 0; index < cm.List.Count; index++)
            //{
            //    DataRowView dr = cm.List[index] as DataRowView;
            //    if (dr.Row == _keepRow)
            //    {
            //        cm.Position = index;
            //        break;
            //    }
            //}
            listServer.SelChange -= new EventHandler(listServer_SelChange);
         //   int[] index;
            _keepRow = new string[listServer.Rows.Selected.Count];
            int _selectedIndex = 0;
            string _selectedItem = "";
            for (int iList = 0; iList < _keepRow.Length; iList++)
            {
                // _selectedIndex = ;
                _selectedItem = listServer.GetData(listServer.Rows.Selected[iList].Index, COL_NAME).ToString();
                _keepRow[iList] = _selectedItem;
                listServer.Rows[_selectedIndex].Selected = false;
            }

            //listServer.SelChange += new EventHandler(listServer_SelChange);
        }

        #endregion " Private Methods "

        private void cmbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(cmbFileType.SelectedItem) == ".pdf")
            {
                tsb_MoveToRCM.Enabled = true;
                tsb_Download.Enabled = true;
                tsb_Select_All.Enabled = true;
                pnlRCMDOCCategory.Visible = true;

                tsb_DonwloadImport.Enabled = false;
                
            }
            else
            {
                if (Convert.ToString(cmbFileType.SelectedItem) == "")
                {
                    
                    tsb_Select_All.Enabled = false;
                    tsb_DonwloadImport.Enabled = true;
                }
                else
                {
                     tsb_Select_All.Enabled = true;
                    
                }
                tsb_MoveToRCM.Enabled = false;
                pnlRCMDOCCategory.Visible = false;
                tsb_DonwloadImport.Enabled = true;
              
               
            }
            tsb_Clear_All.Visible = false;
            tsb_Select_All.Visible = true;
            LoadList();
        }

        private void FillCategoryCombo()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DMSConnectionString);
            DataTable _dtDBFiles = new DataTable();
            try
            {
                ODB.Connect(false);
              
                
                ODB.Retrive_Query("SELECT CategoryId,CategoryName,isnull(GroupId,0) as GroupId,isnull(LevelId,0) as LevelId  FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE ClinicID = " + 1 + " " +
                                    " AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY LevelId,GroupId,CategoryName", out _dtDBFiles);
             
                DataRow dr = _dtDBFiles.NewRow();
                dr["CategoryId"] = 0;
                dr["CategoryName"] = "";
                _dtDBFiles.Rows.InsertAt(dr, 0);
                cmbRCMDOCCategory.DataSource = _dtDBFiles;
            
                cmbRCMDOCCategory.DisplayMember = "CategoryName";
                cmbRCMDOCCategory.ValueMember = "CategoryId";
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Dispose();
                    ODB = null;
                }
                if (_dtDBFiles != null)
                {
                    _dtDBFiles = null;
                }
            }

        }

        private void tsb_MoveToRCM_Click(object sender, EventArgs e)
        {
            IsMoveToRCMClicked = true;
            if (Convert.ToInt64(cmbRCMDOCCategory.SelectedValue) != 0)
            {
                nCategoryID = Convert.ToInt64(cmbRCMDOCCategory.SelectedValue);
                sCategoryName = Convert.ToString(cmbRCMDOCCategory.Text);
                tsb_Download_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Select Category", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            //string _TempImportDirectory = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + -1 + "-" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");// +System.Guid.NewGuid().ToString();DateTime.Now.ToString("MMddyyyyHHmmssffff") + System.Guid.NewGuid().ToString();
            //gloEDocumentV3.eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            //try
            //{
            //    Application.DoEvents();
            //    if (System.IO.Directory.Exists(_TempImportDirectory) == true)
            //    {
            //        System.IO.Directory.Delete(_TempImportDirectory, true);
            //    }
            //    System.IO.Directory.CreateDirectory(_TempImportDirectory);
            //    if (System.IO.Directory.Exists(_TempImportDirectory) == false)
            //    {
            //        MessageBox.Show("Unable to create directory");
            //    }
            //    Application.DoEvents();
            //    string _ImportInSubDirectory = "";
            //    if (string.IsNullOrEmpty(_ImportInSubDirectory))
            //    {
            //        _ImportInSubDirectory = DateTime.Now.ToString("MM DD YYYY");
            //    }
            //    ArrayList oSourceDocuments = new ArrayList();
            //    for (int i = 1; i <= listServer.Rows.Selected.Count; i++)
            //    {
            //        oSourceDocuments.Add(listServer.Rows[i][0]);
            //    }
            //    if (oSourceDocuments.Count <= 0)
            //    {
            //        MessageBox.Show("Select Document to import");
            //    }
            //    Int64 oPatientID = -1;
            //    ProgressBar pbDocument = new ProgressBar();
            //    Application.DoEvents();
            //    for (int i = 0; i < oSourceDocuments.Count; i++)
            //    {
            //         Int64 oDialogDocumentID = 0;
            //         Int64 oDialogContainerID = 0;
            //         gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - START" + " " + DateTime.Now.TimeOfDay);
            //         bool oDialogResultIsOK = oDocManager.ImportSplit(oPatientID, oSourceDocuments, oSourceDocuments[i].ToString(), Convert.ToInt64(cmbRCMDOCCategory.SelectedValue), cmbRCMDOCCategory.Text.ToString(), _ImportInSubDirectory, "2018", "January", _ClinicID, out oDialogContainerID, out oDialogDocumentID, false, pbDocument, gloEDocumentV3.Enumeration.enum_OpenExternalSource.RCM);
            //    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - FINISHED" + " " + DateTime.Now.TimeOfDay);
            //    }
               
            //    Application.DoEvents();
            //}
            //catch (Exception)
            //{
            //}
            //finally
            //{
            //}

        
        }

      
    }
    
}


      

       
 