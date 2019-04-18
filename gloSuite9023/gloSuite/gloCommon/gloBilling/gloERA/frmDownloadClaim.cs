using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using gloBilling.gloERA;
using Rebex.Legacy; //Rebex update Rebex2014R1

namespace gloPMClaimService
{
    public partial class frmDownloadClaim : Form
    {
        #region " Variable Declarations "

        private string _messageBoxCaption = "gloPM";
        private string _databaseconnectionstring = "";
        private Int64 _clinicId = 0;
        private string[] _arrDownloadFiles;

        private Int64 _clearinghouseid = 0;
        private string _clearinghousename = "";
        private string _downloadpath = "";

        //***
        private Rebex.Net.Ftp _Current_FTP = new Rebex.Net.Ftp();// null;
        private Rebex.Net.FtpList _Current_ftpFileList = null;
        private FTPParamete _CurrentFTPParameter = new FTPParamete();
     //   private bool _CurrentFTPConnected = false;
        private long _fileSize = -1;
        private bool _CurrentProcessStarted = false;
        private bool _CurrentProcessStopped = false;

        private Rebex.Net.Sftp _secure_FTP = new Rebex.Net.Sftp();//  null;
        private Rebex.Net.SftpItemCollection _secure_Current_ftpFileList = new Rebex.Net.SftpItemCollection();//  null;

        Int32 _FileDowloadedCounter = 0;
        Int32 _SelectedFileCount = 0;

        #endregion

        #region " Property Procedures "

        public string Databaseconnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        public Int64 ClinicID
        {
            get { return _clinicId; }
            set { _clinicId = value; }
        }

        public string[] DownloadedFiles
        {
            get { return _arrDownloadFiles; }
            set { _arrDownloadFiles = value; }
        }

        #endregion

        #region " Constructor "

        public frmDownloadClaim()
        {
            InitializeComponent();
        }

        public frmDownloadClaim(Int64 clearinghouseid, string clearinghousename, string databaseconnectionstring, string downloadpath)
        {
            InitializeComponent();

            _clearinghouseid = clearinghouseid;
            _clearinghousename = clearinghousename;
            _databaseconnectionstring = databaseconnectionstring;
            _downloadpath = downloadpath;
        }

        #endregion

        #region " Form Load "

        private void frmDownloadClaim_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        #endregion

        #region " Tool Strip Button Click Event "

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion " Tool Strip Button Click Event "

        #region " Timer Events "

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            SecureDownloadFiles();
            System.Threading.Thread.Sleep(1000);
            if (_CurrentProcessStarted == true && _CurrentProcessStopped == true)
            {
                this.Close();
            }

        }

        #endregion

        #region " Public & Private Methods "

        private bool Connect()
        {
            bool _retValue = false;
            try
            {
                rtflog.Text = "Reading parameters...";
                _CurrentFTPParameter.ReadFolderStructure(_clearinghouseid);
                if (_CurrentFTPParameter.Host.ToString().Trim() != "" && _CurrentFTPParameter.Login.ToString().Trim() != "" && _CurrentFTPParameter.Password.ToString().Trim() != "")
                {
                    #region "FTP Intilization"
                    if (_Current_FTP != null)
                    {
                        if (_Current_FTP.State != Rebex.Net.FtpState.Disconnected)
                        {
                            _Current_FTP.Disconnect();
                        }
                        _Current_FTP.Dispose();
                    }
                    _Current_FTP = new Rebex.Net.Ftp();
                    _Current_FTP.LogWriter = new RichTextBoxLogWriter(rtflog, 10000000, Rebex.LogLevel.Error);
                    _Current_FTP.StateChanged += new Rebex.Net.FtpStateChangedEventHandler(_Current_FTP_StateChanged);
                    _Current_FTP.Passive = _CurrentFTPParameter.Passive;
                    _Current_FTP.Timeout = _CurrentFTPParameter.Timeout;
                    #endregion

                    #region "Proxy Code"
                    if (_CurrentFTPParameter.ProxyEnabled == true)
                    {
                        //Code Remaining
                    }
                    #endregion

                    Rebex.Net.TlsParameters p = null;

                    #region "Security Code"
                    if (_CurrentFTPParameter.SecurityType != Rebex.Net.FtpSecurity.Unsecure)
                    {
                        //Code Remaining
                    }
                    #endregion

                    Application.DoEvents();

                    #region "FTP BeginConnect"
                    try
                    {
                        rtflog.AppendText(Environment.NewLine + "Connecting...");
                        IAsyncResult ar = _Current_FTP.BeginConnect(_CurrentFTPParameter.Host, _CurrentFTPParameter.Port, p, _CurrentFTPParameter.SecurityType, null, null);
                        while (!ar.IsCompleted)
                        {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(1);
                        }
                        _Current_FTP.EndConnect(ar);
                    }
                    catch (Exception EX)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(EX.Message, false);
                    }
                    finally
                    {

                    }
                    #endregion

                    Application.DoEvents();

                    #region "Login to FTP"
                    _Current_FTP.Login(_CurrentFTPParameter.Login, _CurrentFTPParameter.Password);
                    #endregion

                    Application.DoEvents();

                    #region "FTP Security Setting"
                    if (_CurrentFTPParameter.ClearCommandChannel == true && _CurrentFTPParameter.SecurityType != Rebex.Net.FtpSecurity.Unsecure)
                    {
                        _Current_FTP.SecureTransfers = true;
                        _Current_FTP.ClearCommandChannel();
                    }
                    #endregion



                    _retValue = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                // MessageBox.Show( "Connection Error","PM",MessageBoxButtons.OK);  
            }
            finally
            {

            }

            return _retValue;
        }

        private Rebex.Net.FtpList GetFileList()
        {
            Rebex.Net.FtpList _ftpList = null;

            try
            {
                if (_Current_FTP != null)
                {
                    IAsyncResult ar = _Current_FTP.BeginGetList(null, null);
                    while (!ar.IsCompleted)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(1);
                    }
                    _ftpList = _Current_FTP.EndGetList(ar);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                _ftpList = null;
            }

            return _ftpList;
        }

        private Rebex.Net.SftpItemCollection GetSecureFileList()
        {
            Rebex.Net.SftpItemCollection _sftpList = null;

            try
            {
                if (_secure_FTP != null)
                {
                    IAsyncResult ar = _secure_FTP.BeginGetList(null, null, null);
                    while (!ar.IsCompleted)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(1);
                    }
                    _sftpList = _secure_FTP.EndGetList(ar);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                _sftpList = null;
            }

            return _sftpList;
        }

        private void DownloadFiles()
        {
            try
            {
                if (Connect() == false) { return; }

                if (_Current_FTP.State == Rebex.Net.FtpState.Ready)
                {
                    string _remotePath = "";
                    _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_835RemittanceAdvice;
                    _remotePath = "/" + _remotePath;
                    _Current_FTP.ChangeDirectory(_remotePath);

                    rtflog.AppendText(Environment.NewLine + "Connected and reading file list...");
                    Application.DoEvents();

                    Rebex.Net.FtpList _ftpList = GetFileList();
                    _Current_ftpFileList = _ftpList;

                    #region " Download files "

                    try
                    {
                        string _fileName = "";
                        Int64 _filesize = 0;
                        string _fileDateTime = "";
                        string _downLoadFilePath = "";
                        DialogResult _dlgDownlaodFileRslt = DialogResult.None;
                        bool IsDownloadAll = false;
                        #region " Show File List "

                        frmDownloadFileList ofrmDownloadFileList = new frmDownloadFileList(_Current_ftpFileList, null, _downloadpath, false);
                        ofrmDownloadFileList.ShowDialog(this);
                        if (ofrmDownloadFileList.IsDownloadImportClicked == true)
                        {
                            for (int i = 0; i < ofrmDownloadFileList.SecureFtpSelectedFileList.Count; i++)
                            {
                                _arrDownloadFiles[i] = ofrmDownloadFileList.SecureFtpSelectedFileList[i].Name.ToString();

                            }
                        }
                        _dlgDownlaodFileRslt = ofrmDownloadFileList.DlgResult;
                        if (_dlgDownlaodFileRslt == DialogResult.OK)
                        {
                          //  _ftpList = new Rebex.Net.FtpList();
                            _ftpList = ofrmDownloadFileList.FtpSelectedFileList;
                            IsDownloadAll = ofrmDownloadFileList._IsDownloadAll;
                        }
                        else if (_dlgDownlaodFileRslt == DialogResult.None || _dlgDownlaodFileRslt == DialogResult.Cancel)
                        {
                            this.Close();
                        }

                        #endregion
                        //Added By MaheshB
                        if (IsDownloadAll == false)
                        {
                            if (_ftpList != null && _ftpList.Count > 0)
                            {
                                for (int fileCounter = 0; fileCounter < _ftpList.Count; fileCounter++)
                                {
                                    if (_ftpList[fileCounter].IsFile)
                                    {
                                        _fileName = _ftpList[fileCounter].Name;
                                        _filesize = _ftpList[fileCounter].Size;
                                        _fileSize = _filesize;
                                        _fileDateTime = FormatFileTime(_ftpList[fileCounter].Modified);
                                        _downLoadFilePath = Path.Combine(_downloadpath, _fileName);

                                        if (File.Exists(_downLoadFilePath))
                                        {
                                            _downLoadFilePath = "";
                                            _fileName = "";
                                            _filesize = 0;
                                            _fileDateTime = "";
                                            continue;
                                        }

                                        _CurrentProcessStarted = true;
                                        rtflog.AppendText(Environment.NewLine + "Starting file download...");
                                        Application.DoEvents();
                                        rtflog.AppendText(Environment.NewLine + "File Name : " + _fileName + " ");
                                        Application.DoEvents();
                                        _Current_FTP.BeginGetFile(_fileName, _downLoadFilePath, new AsyncCallback(DownloadCallback), null);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                int _startIndex = 0;
                                int _endIndex = 0;
                                string _message = "";
                                Application.DoEvents();

                                _startIndex = rtflog.Text.Length;
                                _message = "No new " + _remotePath + " files found.....";
                                _endIndex = _message.Length;
                                rtflog.AppendText(Environment.NewLine + _message);
                                rtflog.Select(_startIndex, _endIndex);
                                rtflog.SelectionFont = gloGlobal.clsgloFont.gFontArial_Bold;//new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                rtflog.SelectionColor = Color.Red;
                                rtflog.Select(0, 0);
                                Application.DoEvents();

                                _message = "Disconnecting ....";
                                rtflog.AppendText(Environment.NewLine + _message);
                                Application.DoEvents();

                                System.Threading.Thread.Sleep(2000);
                                this.Close();
                            }

                            if (_CurrentProcessStarted == true && _CurrentProcessStopped == true)
                            {
                                if (_Current_FTP.State != Rebex.Net.FtpState.Disconnected)
                                {
                                    _Current_FTP.Disconnect();
                                }
                                _Current_FTP.Dispose();
                            }
                        }
                        else
                        {
                            _Current_FTP.GetFiles(_remotePath, _downloadpath, Rebex.Net.FtpBatchTransferOptions.Recursive);

                            System.Threading.Thread.Sleep(2000);
                            this.Close();
                        }
                        ofrmDownloadFileList.Dispose();
                        ofrmDownloadFileList = null;
                    }
                    catch (Exception ex)
                    {
                        //SftpException sx = ex as SftpException;
                        Rebex.Net.FtpException ftpEx = ex as Rebex.Net.FtpException;
                        if (ftpEx != null && ftpEx.Status == Rebex.Net.FtpExceptionStatus.OperationAborted)
                            MessageBox.Show("Operation aborted.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(ftpEx.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                    }

                    #endregion " Download files "

                }

                if (_CurrentProcessStarted == true && _CurrentProcessStopped == true)
                {
                    if (_Current_FTP.State != Rebex.Net.FtpState.Disconnected)
                    {
                        _Current_FTP.Disconnect();
                    }
                    _Current_FTP.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private delegate void TransferFinishedDelegate(Exception error, bool refreshFtp);

        public void DownloadCallback(IAsyncResult asyncResult)
        {
            try
            {
                _Current_FTP.EndGetFile(asyncResult);
                if (!IsDisposed)
                    Invoke(new TransferFinishedDelegate(TransferFinished), new object[] { null, false });
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    Invoke(new TransferFinishedDelegate(TransferFinished), new object[] { ex, false });
            }
        }

        private void TransferFinished(Exception error, bool refreshFtp)
        {

            if (error != null)
                rtflog.AppendText(Environment.NewLine + error);

            _fileSize = -1;


            rtflog.AppendText(Environment.NewLine + "Successfully finished file downloading ..." + Environment.NewLine);
            System.Threading.Thread.Sleep(1000);

            _CurrentProcessStopped = true;
            tsb_Close.Enabled = true;
            this.Close();
        }

        private static string FormatFileTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm");
        }

        #endregion

        #region " FTP Events "

        void _Current_FTP_StateChanged(object sender, Rebex.Net.FtpStateChangedEventArgs e)
        {
            if (!IsDisposed)
                Invoke(new Rebex.Net.FtpStateChangedEventHandler(StateChanged), new object[] { sender, e });
        }

        public void StateChanged(object sender, Rebex.Net.FtpStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case Rebex.Net.FtpState.Disconnected:
                case Rebex.Net.FtpState.Disposed:
                    break;
                case Rebex.Net.FtpState.Ready:
                    break;

            }
        }

        //void _Current_FTP_TransferProgress(object sender, Rebex.Net.FtpTransferProgressEventArgs e)
        //{

        //    if (!IsDisposed)
        //        Invoke(new Rebex.Net.FtpTransferProgressEventHandler(TransferProgress), new object[] { sender, e });
        //}

        //public void TransferProgress(object sender, Rebex.Net.FtpTransferProgressEventArgs e)
        //{
        //    if (e.State != Rebex.Net.FtpTransferState.None && _fileSize > 0)
        //    {
        //        decimal index = (decimal)e.BytesTransferred / (decimal)_fileSize;
        //        pbTransfer.Value = (int)(index * pbTransfer.Maximum);
        //    }
        //}

        #endregion " FTP Events "

        private void frmDownloadClaim_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (_Current_FTP != null)
                {
                    if (_Current_FTP.State != Rebex.Net.FtpState.Disconnected)
                    {
                        _Current_FTP.Disconnect();
                    }
                    _Current_FTP.Dispose();
                }

                DisconnectSecureFTP();
            }
            catch //(Exception ex)
            {
            }

        }

        #region " Secure FTP Download "
        private void SecureDownloadFiles()
        {
            try
            {

                rtflog.Text = "Reading parameters...";
                _CurrentFTPParameter.ReadFolderStructure(_clearinghouseid);
                if (_CurrentFTPParameter.Host.ToString().Trim() != "" && _CurrentFTPParameter.SecureHost.ToString().Trim() != "" &&
                    _CurrentFTPParameter.Login.ToString().Trim() != "" && _CurrentFTPParameter.Password.ToString().Trim() != "")
                {
                    #region "FTP Intilization"

                    if (_secure_FTP != null)
                    {
                        if (_secure_FTP.State != Rebex.Net.SftpState.Disconnected)
                        {
                            _secure_FTP.Disconnect();
                        }
                        _secure_FTP.Dispose();
                    }
                    _secure_FTP = new Rebex.Net.Sftp();
                    _secure_FTP.LogWriter = new RichTextBoxLogWriter(rtflog, 10000000, Rebex.LogLevel.Error);
                    //_secure_FTP.StateChanged += new Rebex.Net.SftpStateChangedEventHandler(_secure_FTP_StateChanged);
                    _secure_FTP.Timeout = _CurrentFTPParameter.Timeout;

                    //Added By MaheshB for Download All/Multiple
                    _secure_FTP.BatchTransferProgress += new Rebex.Net.SftpBatchTransferProgressEventHandler(_ftp_BatchTransferProgress);
                    _secure_FTP.BatchTransferProblemDetected += new Rebex.Net.SftpBatchTransferProblemDetectedEventHandler(_ftp_BatchTransferProblemDetected);


                    #endregion

                    #region "Proxy Code"
                    if (_CurrentFTPParameter.ProxyEnabled == true)
                    {
                        //Code Remaining
                    }
                    #endregion

                //    Rebex.Net.SshParameters sshParameters = null;

                    #region "Security Code"

                    if (_CurrentFTPParameter.SecurityType != Rebex.Net.FtpSecurity.Secure)
                    {
                        //Code Remaining
                    }
                    #endregion

                    //Application.DoEvents();

                    #region "FTP BeginConnect"
                    try
                    {
                        rtflog.AppendText(Environment.NewLine + "Connecting...");
                        IAsyncResult ar = _secure_FTP.BeginConnect(_CurrentFTPParameter.SecureHost, _CurrentFTPParameter.SecurePort, null, null, null);


                        while (!ar.IsCompleted)
                        {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(1);
                        }
                        if (_secure_FTP == null)//Added By MaheshB
                        {
                            _CurrentProcessStarted = true;
                            _CurrentProcessStopped = true;
                            return;
                        }
                        _secure_FTP.EndConnect(ar);

                    }

                    finally
                    {

                    }
                    #endregion

                    //Application.DoEvents();

                    #region "Login to FTP"
                    _secure_FTP.Login(_CurrentFTPParameter.Login, _CurrentFTPParameter.Password);

                    #endregion

                    //Application.DoEvents();


                    #region "Connected then Download files"
                    if (_secure_FTP.State == Rebex.Net.SftpState.Ready)
                    {
                        try
                        {

                         //   _CurrentFTPConnected = true;
                            tsb_Close.Enabled = false;
                            //rtflog.AppendText(Environment.NewLine + "Connected and ready to transfer data..." + Environment.NewLine);
                            if (_CurrentFTPParameter.ClaimManagement_InBox_835RemittanceAdvice == "")
                            {
                                MessageBox.Show("Unable to connect" + Environment.NewLine + "Clearinghouse setup is not complete" + Environment.NewLine + "Verify Clearinghouse setup in gloPMAdmin.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.Close();
                                return;
                            }
                            string _remotePath = "";
                            _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_835RemittanceAdvice;
                            _remotePath = "/" + _remotePath;
                            _secure_FTP.ChangeDirectory(_remotePath);

                            rtflog.AppendText(Environment.NewLine + "Connected and reading file list...");
                            Application.DoEvents();
                            //Rebex.Net.FtpList _ftpList = GetFileList();
                            Rebex.Net.SftpItemCollection _sftpList = GetSecureFileList();
                            _secure_Current_ftpFileList = _sftpList;

                            #region " Download files "
                            string _downLoadFilePath = "";
                            frmDownloadFileList ofrmDownloadFileList = null;

                            try
                            {
                                string _fileName = "";
                                Int64 _filesize = 0;
                                string _fileDateTime = "";
                                DialogResult _dlgDownlaodFileRslt = DialogResult.None;
                                bool IsDownloadAll = false;
                                bool IsMoveToRCM = false;
                                Int64 nSelectedCategoryID = 0;
                                string sSelectedCategoryName = "";

                                #region " Show File List "

                                //02242010 Remove Text in Log TextBox.
                                if (_secure_Current_ftpFileList != null && _secure_Current_ftpFileList.Count == 0)
                                {
                                    rtflog.Text = "";
                                }

                                ofrmDownloadFileList = new frmDownloadFileList(null, _secure_Current_ftpFileList, _downloadpath, true);
                                ofrmDownloadFileList.ShowDialog(this);
                                _dlgDownlaodFileRslt = ofrmDownloadFileList.DlgResult;


                                if (_dlgDownlaodFileRslt == DialogResult.OK)
                                {
                                   // _sftpList = new Rebex.Net.SftpItemCollection();
                                    _sftpList = ofrmDownloadFileList.SecureFtpSelectedFileList;
                                    IsDownloadAll = ofrmDownloadFileList._IsDownloadAll;
                                    IsMoveToRCM = ofrmDownloadFileList.IsMoveToRCMClicked;
                                    if (IsMoveToRCM==true)
                                    {
                                        nSelectedCategoryID = ofrmDownloadFileList.nCategoryID;
                                        sSelectedCategoryName = ofrmDownloadFileList.sCategoryName;
                                    }

                                }
                                else if (_dlgDownlaodFileRslt == DialogResult.None || _dlgDownlaodFileRslt == DialogResult.Cancel)
                                {
                                    this.Close();
                                    return;
                                }

                                #endregion

                                if (IsDownloadAll == false)
                                {
                                    #region " Download Selected Files "
                                    if (_sftpList != null && _sftpList.Count > 0)
                                    {

                                        _CurrentProcessStarted = true;
                                        rtflog.AppendText(Environment.NewLine + "Starting file download...");
                                        // Application.DoEvents();
                                        this.Refresh();

                                        if (ofrmDownloadFileList.IsDownloadImportClicked)
                                        {
                                            _arrDownloadFiles = new string[_sftpList.Count];
                                        }

                                        _SelectedFileCount = _sftpList.Count;
                                        for (int fileCounter = 0; fileCounter < _sftpList.Count; fileCounter++)
                                        {
                                            if (_sftpList[fileCounter].IsFile)
                                            {
                                                _fileName = _sftpList[fileCounter].Name;
                                                _filesize = _sftpList[fileCounter].Size;
                                                _fileSize = _filesize;
                                                _fileDateTime = FormatFileTime(_sftpList[fileCounter].Modified);
                                                _downLoadFilePath = Path.Combine(_downloadpath, _fileName);
                                                string _NewFileNamewithExt = _fileName;                                                
                                                //GLO2011-0015809 : Stuck ERA
                                                // Rename File name if Allready exists on Download Path.    
                                                if (File.Exists(_downLoadFilePath))
                                                {
                                                    int cnt = 1;
                                                    string _newfilename = Path.GetFileNameWithoutExtension(_fileName);
                                                    string _ext = Path.GetExtension(_fileName);
                                                    while (File.Exists(_downLoadFilePath))
                                                    {
                                                        _NewFileNamewithExt = _newfilename + "_" + cnt + _ext;
                                                        _downLoadFilePath = Path.Combine(_downloadpath, _NewFileNamewithExt);
                                                        cnt = cnt + 1;
                                                    }
                                                }
                                                if (ofrmDownloadFileList.IsDownloadImportClicked) // THIS ARRAY OF FILE NAMES WILL FORWARD FILENAMES TO ERA PAYMENT WINDOW TO IMPORT AUTOMATICALLY //
                                                { _arrDownloadFiles[fileCounter] = _downLoadFilePath; }

                                                //if (File.Exists(_downLoadFilePath))
                                                //{
                                                //    _downLoadFilePath = "";
                                                //    _fileName = "";
                                                //    _filesize = 0;
                                                //    _fileDateTime = "";
                                                //    //Added By MaheshB
                                                //    _CurrentProcessStopped = true;
                                                //    _CurrentProcessStarted = true;
                                                //    //MessageBox.Show("This file is already Exists. ", "Claim Management",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    //MessageBox decide-show or not.
                                                //    continue;
                                                //}
                                                
                                                rtflog.AppendText(Environment.NewLine + "File Name : " + _NewFileNamewithExt + " ");
                                                this.Refresh();
                                                // Application.DoEvents();

                                                //_secure_FTP.TransferProgress -= new Rebex.Net.SftpTransferProgressEventHandler(_secure_FTP_TransferProgress);
                                                // _secure_FTP.GetFile(_fileName, _downLoadFilePath);
                                                //_secure_FTP.BeginGetFile(_fileName, _downLoadFilePath, new AsyncCallback(SecureDownloadCallback), null);


                                                IAsyncResult ar = null;
                                                bool _Completedresult = false;
                                                try
                                                {
                                                    ar = _secure_FTP.BeginGetFile(_fileName, _downLoadFilePath, new AsyncCallback(SecureDownloadCallback), null);
                                                }
                                                catch (Rebex.Net.SftpException ex)
                                                {
                                                    _Completedresult = true;
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                                }
                                                catch (System.IO.IOException ex)
                                                {
                                                    _Completedresult = true;
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                                }
                                                catch (Exception ex)
                                                {
                                                    _Completedresult = true;
                                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                                }
                                                if (ar != null)
                                                {
                                                    while (!_Completedresult)
                                                    {
                                                        
                                                       if ( _secure_FTP.GetConnectionState().Connected)
                                                        {
                                                            _Completedresult = ar.IsCompleted;
                                                            System.Threading.Thread.Sleep(1);
                                                        }
                                                       else
                                                          break;

                                                    }
                                                    //_secure_FTP.EndPutFile(ar);
                                                }
                                                if (IsMoveToRCM == true)
                                                {
                                                    MoveToRCMDocs(_sftpList, _fileName, _downLoadFilePath, nSelectedCategoryID, sSelectedCategoryName);
                                                }
                                               _secure_FTP.DeleteFile(_fileName);
                                                #region " Commented Code "
                                                //Added By MaheshB
                                                //string Mod_Date = _sftpList[fileCounter].Modified.ToString();
                                                //if (File.Exists(_downLoadFilePath))
                                                //{
                                                //    Application.DoEvents();
                                                //string originalfile = _downLoadFilePath;
                                                //    _downLoadFilePath = _downLoadFilePath.Replace(".RMT", "");
                                                //    _downLoadFilePath = _downLoadFilePath + "-" + "1234";//Mod_Date;
                                                //    _downLoadFilePath = _downLoadFilePath + ".RMT";
                                                //    _downLoadFilePath=_downLoadFilePath.Replace(" ","");
                                                //    //System.IO.File.Move(originalfile, _downLoadFilePath);
                                                //    _downLoadFilePath=_downLoadFilePath.Replace("\\",@"\");
                                                //    Application.DoEvents();
                                                //    System.IO.File.Copy(originalfile, _downLoadFilePath);
                                                //    //_downLoadFilePath.
                                                //}
                                                //break;
                                                #endregion
                                            }
                                        }
                                        _CurrentProcessStopped = true;
                                    }
                                    else
                                    {
                                        int _startIndex = 0;
                                        int _endIndex = 0;
                                        string _message = "";
                                        Application.DoEvents();

                                        _startIndex = rtflog.Text.Length;
                                        _message = "No new " + _remotePath + " files found.....";
                                        _endIndex = _message.Length;
                                        rtflog.AppendText(Environment.NewLine + _message);
                                        rtflog.Select(_startIndex, _endIndex);
                                        rtflog.SelectionFont = gloGlobal.clsgloFont.gFontArial_Bold;//new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                        rtflog.SelectionColor = Color.Red;
                                        rtflog.Select(0, 0);
                                        Application.DoEvents();

                                        _message = "Disconnecting ....";
                                        rtflog.AppendText(Environment.NewLine + _message);
                                        Application.DoEvents();

                                        System.Threading.Thread.Sleep(2000);
                                        this.Close();
                                    }

                                    if (_CurrentProcessStarted == true && _CurrentProcessStopped == true)
                                    {
                                        if (_secure_FTP.State != Rebex.Net.SftpState.Disconnected)
                                        {
                                            _secure_FTP.Disconnect();
                                        }
                                        _secure_FTP.Dispose();
                                    }
                                    #region commented code
                                    //if (File.Exists(_downLoadFilePath))
                                    //{
                                    //Application.DoEvents();
                                    //string originalfile = _downLoadFilePath;
                                    //_downLoadFilePath = _downLoadFilePath.Replace(".RMT", "");
                                    //_downLoadFilePath = _downLoadFilePath + "-" + "1234";//Mod_Date;
                                    //_downLoadFilePath = _downLoadFilePath + ".RMT";
                                    //_downLoadFilePath = _downLoadFilePath.Replace(" ", "");
                                    ////System.IO.File.Move(originalfile, _downLoadFilePath);
                                    //_downLoadFilePath = _downLoadFilePath.Replace("\\", @"\");
                                    //Application.DoEvents();
                                    //System.IO.File.Move(originalfile, _downLoadFilePath);
                                    ////_downLoadFilePath.
                                    //}
                                    //return _downLoadFilePath;
                                    #endregion

                                    #endregion
                                }
                                else
                                {


                                    #region " Download All Files "


                                    //Rebex.Net.Sftp client = new Rebex.Net.Sftp();
                                    //client.Connect("ftp.gatewayedi.com");
                                    //client.Login("1C26", "4bntwyif");
                                    //client.GetFiles("/wwwroot/remits", _downloadpath, Rebex.Net.SftpBatchTransferOptions.Recursive);
                                    //if (!IsDisposed)
                                    //{
                                    _remotePath = _remotePath + "/*";
                                    //_remotePath = "/remits/*";//+ _remotePath;
                                    this.Cursor = Cursors.WaitCursor;
                                    rtflog.Text = "";
                                    rtflog.Text = "Downloading files....Please wait...";
                                    _secure_FTP.GetFiles(_remotePath, _downloadpath, Rebex.Net.SftpBatchTransferOptions.Recursive);

                                    System.Threading.Thread.Sleep(2000);
                                    this.Cursor = Cursors.Default;
                                    this.Close();
                                    //}
                                    //else
                                    //{
                                    //    this.Close();
                                    //    return; 
                                    //}
                                    #endregion
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                //SftpException sx = ex as SftpException;
                                Rebex.Net.SftpException ftpEx = ex as Rebex.Net.SftpException;
                                if (ftpEx != null && ftpEx.Status == Rebex.Net.SftpExceptionStatus.OperationAborted)
                                    MessageBox.Show("Operation aborted.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show(ftpEx.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                //return null;
                            }
                            finally
                            {
                                if (ofrmDownloadFileList != null) { ofrmDownloadFileList.Dispose(); ofrmDownloadFileList = null; }
                                //return null;
                            }

                            #endregion " Download files "

                        }
                        catch (Rebex.Net.SftpException ExRebex)
                        {
                            //gloAuditTrail.gloAuditTrail.ExceptionLog(ExRebex.Message, true);
                            MessageBox.Show(ExRebex.Message + Environment.NewLine + "Verify Clearinghouse setup in gloPMAdmin.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ExRebex.ToString(), false);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            this.Close();
                        }
                    }
                    #endregion
                }
                else
                {
                    if (MessageBox.Show("Clearinghouse ftp parameters missing, setup through gloPM Admin.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK) 
                    {
                        this.Close();
                    }

                }
                //return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
               // gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);    
                this.Close();
            }
        }

        private void MoveToRCMDocs(Rebex.Net.SftpItemCollection _sftpList, string _fileName, string _downLoadFilePath, long nSelectedCategoryID, string sSelectedCategoryName)
        {
            string _TempImportDirectory = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + -1 + "-" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");// +System.Guid.NewGuid().ToString();DateTime.Now.ToString("MMddyyyyHHmmssffff") + System.Guid.NewGuid().ToString();
            gloEDocumentV3.eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            try
            {
                Application.DoEvents();
                if (System.IO.Directory.Exists(_TempImportDirectory) == true)
                {
                    System.IO.Directory.Delete(_TempImportDirectory, true);
                }
                System.IO.Directory.CreateDirectory(_TempImportDirectory);
                if (System.IO.Directory.Exists(_TempImportDirectory) == false)
                {
                    MessageBox.Show("Unable to create directory");
                }
                Application.DoEvents();
                string _ImportInSubDirectory = "";
                if (string.IsNullOrEmpty(_ImportInSubDirectory))
                {
                    _ImportInSubDirectory = DateTime.Now.ToString("MM dd yyyy");
                }
                ArrayList oSourceDocuments = new ArrayList();
                oSourceDocuments.Add(_downLoadFilePath);
                //for (int i = 0; i <= _sftpList.Count-1; i++)
                //{
                    
                //}
                if (oSourceDocuments.Count <= 0)
                {
                    MessageBox.Show("Select Document to import");
                }
                Int64 oPatientID = -1;
                string _ImportYear = DateTime.Now.Year.ToString();
                string _ImportMonth = gloEDocumentV3.eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                ProgressBar pbDocument = new ProgressBar();
                Application.DoEvents();
                for (int i = 0; i < oSourceDocuments.Count; i++)
                {
                    Int64 oDialogDocumentID = 0;
                    Int64 oDialogContainerID = 0;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - START" + " " + DateTime.Now.TimeOfDay);
                    bool oDialogResultIsOK = oDocManager.ImportSplit(oPatientID, oSourceDocuments, _fileName, nSelectedCategoryID, sSelectedCategoryName, _ImportInSubDirectory, _ImportYear, _ImportMonth, gloGlobal.gloPMGlobal.ClinicID, out oDialogContainerID, out oDialogDocumentID, false, pbDocument, gloEDocumentV3.Enumeration.enum_OpenExternalSource.RCM);
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - FINISHED" + " " + DateTime.Now.TimeOfDay);
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {

               MessageBox.Show( ex.ToString());
                
            }
            finally
            {
                if (_TempImportDirectory != null && System.IO.Directory.Exists(_TempImportDirectory))
                {
                    System.IO.Directory.Delete(_TempImportDirectory, true);
                }
            }
        }

       

        private bool DisconnectSecureFTP()
        {
            bool _isDisconnected = false;
            try
            {
                if (_secure_FTP != null)
                {
                    if (_secure_FTP.State != Rebex.Net.SftpState.Disconnected)
                    {
                        _secure_FTP.Disconnect();
                    }
                    _secure_FTP.Dispose();
                    _secure_FTP = null;
                    _isDisconnected = true;
                }
            }
            catch //(Exception ex)
            {
                _isDisconnected = false;
            }
            return _isDisconnected;
        }

        //void _secure_FTP_StateChanged(object sender, Rebex.Net.SftpStateChangedEventArgs e)
        //{
        //    if (!IsDisposed)
        //        Invoke(new Rebex.Net.SftpStateChangedEventHandler(SecureStateChanged), new object[] { sender, e });
        //}

        //void _secure_FTP_TransferProgress(object sender, Rebex.Net.SftpTransferProgressEventArgs e)
        //{


        //   //if (!IsDisposed)
        //   //Invoke(new Rebex.Net.SftpTransferProgressEventHandler(SecureTransferProgress), new object[] { sender, e });
        //               // MessageBox.Show(e.ToString());  



        //}

        //public void SecureStateChanged(object sender, Rebex.Net.SftpStateChangedEventArgs e)
        //{

        //    switch (e.NewState)
        //    {
        //        case Rebex.Net.SftpState.Disconnected:
        //        case Rebex.Net.SftpState.Disposed:
        //            break;
        //        case Rebex.Net.SftpState.Ready:
        //            break;
        //    }
        //}

        //public void SecureTransferProgress(object sender, Rebex.Net.SftpTransferProgressEventArgs e)
        //{
        //    //if (!e.Finished && _fileSize > 0)
        //    if (e.State != Rebex.Net.SftpTransferState.None && _fileSize > 0)
        //    {
        //        decimal index = (decimal)e.BytesTransferred / (decimal)_fileSize;
        //        pbTransfer.Value = (int)(index * pbTransfer.Maximum);
        //        //lblProgress.Text = e.BytesTransferred + " bytes";
        //    }
        //}

        public void SecureDownloadCallback(IAsyncResult asyncResult)
        {
            try
            {
                //_secure_FTP.EndPutFile(asyncResult);
                _secure_FTP.EndGetFile(asyncResult);
                //if (!IsDisposed)
                //Invoke(new TransferFinishedDelegate(SecureTransferFinished), new object[] { null, true });
            }
            catch //(Exception ex)
            {
                //if (!IsDisposed)
                //Invoke(new TransferFinishedDelegate(SecureTransferFinished), new object[] { ex, true });
            }
        }

        private void SecureTransferFinished(Exception error, bool refreshFtp)
        {
            if (error != null)
            {
                //rtflog.AppendText(Environment.NewLine + error);
            }
            else
            {
                rtflog.Text = "";
            }

            //ShowTransferStatus();
            _FileDowloadedCounter++;
            _fileSize = -1;
            if (_FileDowloadedCounter >= _SelectedFileCount)
            {
                _CurrentProcessStopped = true;
                tsb_Close.Enabled = true;
                this.Close();
            }

        }


        //private void Rebex.Net.FtpBatchTransferProblemDetected(object sender, Rebex.Net.FtpBatchTransferProblemDetectedEventArgs e)
        //{
        //    _problemForm.ShowModal(this, e);

        //    // process any application events to prevent the windown from freezing
        //    Application.DoEvents();
        //}
        #endregion " Secure FTP Download "

        //Added By MaheshB for Select All/Multiple Select 

        private void _ftp_BatchTransferProblemDetected(object sender, Rebex.Net.SftpBatchTransferProblemDetectedEventArgs e)
        {
            if (e.ProblemType == Rebex.Net.SftpBatchTransferProblemType.FileExists)
            {
                e.Action = Rebex.Net.SftpBatchTransferAction.OverwriteIfDifferentSize;
                //return;
            }
            else
            {
                e.Action = Rebex.Net.SftpBatchTransferAction.Skip;
                //return;
            }

            // process any application events to prevent the windown from freezing
            Application.DoEvents();
        }

        /// <summary>
        /// Handles the single file transfer progress event.
        /// </summary>
        private void _ftp_BatchTransferProgress(object sender, Rebex.Net.SftpBatchTransferProgressEventArgs e)
        {
            rtflog.Text = "";
            rtflog.Text = "Downloading files....Please wait...";


            rtflog.Text = rtflog.Text + Environment.NewLine + string.Format("{0} / {1}", e.FilesProcessed, e.FilesTotal);

            Application.DoEvents();
        }
    }
}