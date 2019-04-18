using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Rebex.Legacy; //Rebex DLL updated 2014R1

namespace gloBilling
{
    public partial class frmUploadClaim : Form
    {
        #region " Variable Declarations "

        private Int64 _clearinghouseid = 0;
        private string _clearinghousename = "";
        private string _databaseconnectionstring = "";
        private ArrayList _uploadfiles = new ArrayList();
        private EDIFileType _uploadfiletype = EDIFileType.None;
        private string _MessageBoxCaption = "Claim Management";
        private Rebex.Net.Ftp _Current_FTP = null;
        private FTPParamete _CurrentFTPParameter = new FTPParamete();
        private bool _CurrentFTPConnected = false;
        private long _fileSize = -1;
        private bool _CurrentProcessStarted = false;
        private bool _CurrentProcessStopped = false;
        private Rebex.Net.Sftp _secure_FTP = null;
        private bool _DialogResult = false;

        #endregion " Variable Declarations "

        #region " Property Procedures "

        public bool DialogResult
        {
            get { return _DialogResult; }
            set { _DialogResult = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmUploadClaim()
        {
            InitializeComponent();
        }

        public frmUploadClaim(Int64 clearinghouseid, string clearinghousename, string databaseconnectionstring, ArrayList uploadfiles, EDIFileType uploadfiletype)
        {
            InitializeComponent();

            _clearinghouseid = clearinghouseid;
            _clearinghousename = clearinghousename;
            _databaseconnectionstring = databaseconnectionstring;
            _uploadfiles = uploadfiles;
            _uploadfiletype = uploadfiletype;
        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmUploadClaim_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.ControlBox = false;
        }

        #endregion " Form Load "

        #region " Timer Events "

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                //UploadFiles();
                SecureUploadFiles();
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                if (_CurrentProcessStarted == true && _CurrentProcessStopped == true)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion " Timer Events "

        #region " Public & Private Methods "

        private void UploadFiles()
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
                _Current_FTP.TransferProgress += new Rebex.Net.FtpTransferProgressEventHandler(_Current_FTP_TransferProgress);
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

                #region "Connected then upload files"
                if (_Current_FTP.State == Rebex.Net.FtpState.Ready)
                {
                    _CurrentFTPConnected = true;
                    tsb_Close.Enabled = false;
                    rtflog.AppendText(Environment.NewLine + "Connected and ready to transfer data..." + Environment.NewLine);

                    #region "Upload Files"
                    string _remotePath = "";
                    string _remotefilePath = "";
                    string _localPath = "";
                    string _encryptedLocalFilePath = "";

                    #region "Find Remote Path"
                    switch (_uploadfiletype)
                    {
                        case EDIFileType.None:
                            _remotePath = "";
                            break;
                        case EDIFileType.InBox_271EligibilityResponse:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_271EligibilityResponse;
                            break;
                        case EDIFileType.InBox_277ClaimStatusResponse:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_277ClaimStatusResponse;
                            break;
                        case EDIFileType.InBox_835RemittanceAdvice:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_835RemittanceAdvice;
                            break;
                        case EDIFileType.InBox_997Acknowledgement:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_997Acknowledgement;
                            break;
                        case EDIFileType.OutBox_276EligibilityEnquiry:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_OutBox_276EligibilityEnquiry;
                            break;
                        case EDIFileType.OutBox_837PClaimSubmission:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_OutBox_837PClaimSubmission;
                            break;
                        case EDIFileType.OutBox_997Acknowledgement:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_OutBox_997Acknowledgement;
                            break;
                        case EDIFileType.General_CSRReports:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_General_CSRReports;
                            break;
                        case EDIFileType.General_Letters:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_General_Letters;
                            break;
                        case EDIFileType.General_Reports:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_General_Reports;
                            break;
                        case EDIFileType.General_Statements:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_General_Statements;
                            break;
                        case EDIFileType.General_WorkedTransaction:
                            _remotePath = _CurrentFTPParameter.ClaimManagement_General_WorkedTransaction;
                            break;
                        default:
                            break;
                    }
                    #endregion

                    _remotePath = "/" + _remotePath;

                    _CurrentProcessStarted = true;
                    //_Current_FTP.ChangeDirectory("/claims");
                    for (int i = 0; i <= _uploadfiles.Count - 1; i++)
                    {
                        _encryptedLocalFilePath = "";
                        _localPath = _uploadfiles[i].ToString();
                        System.IO.FileInfo oFile = new System.IO.FileInfo(_localPath);

                        _remotefilePath = _remotePath + "/" + oFile.Name;
                        _fileSize = oFile.Length;

                        #region " Perform PGP Encryption on file "

                        gloPMPGPEncryption ogloPMPGPEncryption = new gloPMPGPEncryption();
                        bool _isFileEncrypted = ogloPMPGPEncryption.EncryptFile(_localPath, out _encryptedLocalFilePath);



                        #endregion

                        //For Upload Folder
                        //_Current_FTP.PutFiles(_localPath, _remotePath,Rebex.Net.FtpBatchTransferOptions.Recursive);    
                        if (_isFileEncrypted == true)
                        {
                            oFile = new System.IO.FileInfo(_encryptedLocalFilePath);
                            _remotefilePath = _remotePath + "/" + oFile.Name;
                            _fileSize = oFile.Length;
                            _Current_FTP.BeginPutFile(_localPath, _remotefilePath, new AsyncCallback(UploadCallback), null);
                        }
                        else
                        {
                            MessageBox.Show("Error encrypting file", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    #endregion
                }
                #endregion

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
                MessageBox.Show("Clearinghouse ftp parameters missing, please setup through gloPM.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion " Public & Private Methods "

        #region "FTP Events "

        void _Current_FTP_StateChanged(object sender, Rebex.Net.FtpStateChangedEventArgs e)
        {
            if (!IsDisposed)
                Invoke(new Rebex.Net.FtpStateChangedEventHandler(StateChanged), new object[] { sender, e });
        }

        void _Current_FTP_TransferProgress(object sender, Rebex.Net.FtpTransferProgressEventArgs e)
        {
            if (!IsDisposed)
                Invoke(new Rebex.Net.FtpTransferProgressEventHandler(TransferProgress), new object[] { sender, e });
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

        public void TransferProgress(object sender, Rebex.Net.FtpTransferProgressEventArgs e)
        {
            if (e.State != Rebex.Net.FtpTransferState.None && _fileSize > 0)
            {
                decimal index = (decimal)e.BytesTransferred / (decimal)_fileSize;
                pbTransfer.Value = (int)(index * pbTransfer.Maximum);
                //lblProgress.Text = e.BytesTransferred + " bytes";
            }
        }

        //private delegate void TransferFinishedDelegate(Exception error, bool refreshFtp);

        //public void DownloadCallback(IAsyncResult asyncResult)
        //{
        //    try
        //    {
        //        _Current_FTP.EndGetFile(asyncResult);
        //        if (!IsDisposed)
        //            Invoke(new TransferFinishedDelegate(TransferFinished), new object[] { null, false });

        //    }
        //    catch (Exception ex)
        //    {
        //        if (!IsDisposed)
        //            Invoke(new TransferFinishedDelegate(TransferFinished), new object[] { ex, false });
        //    }
        //}

        //private void TransferFinished(Exception error, bool refreshFtp)
        //{
        //    if (error != null)
        //        rtfLog.AppendText(Environment.NewLine + error);

        //    //ShowTransferStatus();
        //    _fileSize = -1;
        //    //pbTransfer.Value = 0;
        //    //btnStop.Visible = false;

        //    //try
        //    //{
        //    //    if (refreshFtp)
        //    //        MakeFtpList();
        //    //    else
        //    //        MakeLocalList();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    WriteToLog(ex);
        //    //}
        //}

        //Upload Events

        private delegate void TransferFinishedDelegate(Exception error, bool refreshFtp);

        public void UploadCallback(IAsyncResult asyncResult)
        {
            try
            {
                _Current_FTP.EndPutFile(asyncResult);
                if (!IsDisposed)
                    Invoke(new TransferFinishedDelegate(TransferFinished), new object[] { null, true });
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    Invoke(new TransferFinishedDelegate(TransferFinished), new object[] { ex, true });
            }
        }

        private void TransferFinished(Exception error, bool refreshFtp)
        {
            if (error != null)
                rtflog.AppendText(Environment.NewLine + error);

            ShowTransferStatus();
            _fileSize = -1;
            pbTransfer.Value = 0;
            _CurrentProcessStopped = true;
            tsb_Close.Enabled = true;
            if (error == null) { _DialogResult = true; }
            this.Close();
            //btnStop.Visible = false;

            //try
            //{
            //    if (refreshFtp)
            //        MakeFtpList();
            //    else
            //        MakeLocalList();
            //}
            //catch (Exception ex)
            //{
            //    WriteToLog(ex);
            //}
        }

        private void ShowTransferStatus()
        {
            //// aborted transfer
            //if (_fileSize == -1)
            //    return;

            //// unknown size or null size
            //if (_fileSize == 0)
            //    return;

            //// bytes transferred
            //string outstring = _fileSize + " byte" + (_fileSize > 1 ? "s" : null) + " transferred";

            //// time spent
            //TimeSpan ts = DateTime.Now - _transferTime;

            //// speed
            //if (ts.TotalSeconds > 1)
            //{
            //    outstring += " in" + (ts.Days > 0 ? " " + ts.Days + " day" + (ts.Days > 1 ? "s" : null) : null);
            //    outstring += (ts.Hours > 0 ? " " + ts.Hours + " hour" + (ts.Hours > 1 ? "s" : null) : null);
            //    outstring += (ts.Minutes > 0 ? " " + ts.Minutes + " min" + (ts.Minutes > 1 ? "s" : null) : null);
            //    outstring += (ts.Seconds > 0 ? " " + ts.Seconds + " sec" + (ts.Seconds > 1 ? "s" : null) : null);

            //    outstring += " at " + ((long)_fileSize / (long)ts.TotalSeconds) / 1024 + " KB/s";
            //}
            //else
            //{
            //    outstring += " in " + ts.TotalSeconds + " sec";
            //}

            //WriteToLog("> " + outstring, RichTextBoxLogWriter.COLORCOMMAND);
        }

        #endregion

        #region " Tool Strip Button Events "

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                tsb_Close.Enabled = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion " Tool Strip Button Events "

        #region " Form Closing "

        private void frmUploadClaim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Current_FTP != null) //Condition Added by Debasish Das on 24th March 2010 (Mantis Bug ID:0001384)
            {
                if (_Current_FTP.State != Rebex.Net.FtpState.Disconnected)
                {
                    _Current_FTP.Disconnect();
                }
                _Current_FTP.Dispose();
            }

            DisconnectSecureFTP();
        }

        #endregion " Form Closing "

        #region " Secure FTP Upload "

        private void SecureUploadFiles()
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
                _secure_FTP.TransferProgress += new Rebex.Net.SftpTransferProgressEventHandler(_secure_FTP_TransferProgress);
                _secure_FTP.StateChanged += new Rebex.Net.SftpStateChangedEventHandler(_secure_FTP_StateChanged);
                _secure_FTP.Timeout = _CurrentFTPParameter.Timeout;

                #endregion

                #region "Proxy Code"
                if (_CurrentFTPParameter.ProxyEnabled == true)
                {
                    //Code Remaining
                }
                #endregion

                Rebex.Net.SshParameters sshParameters = null;

                #region "Security Code"

                if (_CurrentFTPParameter.SecurityType != Rebex.Net.FtpSecurity.Secure)
                {
                    //Code Remaining
                }
                #endregion

                Application.DoEvents();

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
                    if (_secure_FTP != null) //Condition Added by Debasish Das on 24th March 2010 (Mantis Bug ID:0001384)
                    {
                        _secure_FTP.EndConnect(ar);
                    }
                }
                catch (Rebex.Net.SftpException ex)
                {
                    MessageBox.Show("Problem while connecting clearinghouse.Please try again. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _DialogResult = false;
                    this.Close();
                    return;
                }
                finally
                {

                }
                #endregion

                Application.DoEvents();

                #region "Login to FTP"
                if (_secure_FTP != null) //Condition Added by Debasish Das on 24th March 2010 (Mantis Bug ID:0001384)
                {
                    _secure_FTP.Login(_CurrentFTPParameter.Login, _CurrentFTPParameter.Password);
                }
                #endregion

                Application.DoEvents();


                #region "Connected then upload files"
                if (_secure_FTP != null)
                {
                    if (_secure_FTP.State == Rebex.Net.SftpState.Ready)
                    {
                        _CurrentFTPConnected = true;
                        tsb_Close.Enabled = false;
                        rtflog.AppendText(Environment.NewLine + "Connected and ready to transfer data..." + Environment.NewLine);

                        #region "Upload Files"
                        string _remotePath = "";
                        string _remotefilePath = "";
                        string _localPath = "";
                        string _encryptedLocalFilePath = "";

                        #region "Find Remote Path"
                        switch (_uploadfiletype)
                        {
                            case EDIFileType.None:
                                _remotePath = "";
                                break;
                            case EDIFileType.InBox_271EligibilityResponse:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_271EligibilityResponse;
                                break;
                            case EDIFileType.InBox_277ClaimStatusResponse:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_277ClaimStatusResponse;
                                break;
                            case EDIFileType.InBox_835RemittanceAdvice:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_835RemittanceAdvice;
                                break;
                            case EDIFileType.InBox_997Acknowledgement:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_InBox_997Acknowledgement;
                                break;
                            case EDIFileType.OutBox_276EligibilityEnquiry:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_OutBox_276EligibilityEnquiry;
                                break;
                            case EDIFileType.OutBox_837PClaimSubmission:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_OutBox_837PClaimSubmission;
                                break;
                            case EDIFileType.OutBox_997Acknowledgement:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_OutBox_997Acknowledgement;
                                break;
                            case EDIFileType.General_CSRReports:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_General_CSRReports;
                                break;
                            case EDIFileType.General_Letters:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_General_Letters;
                                break;
                            case EDIFileType.General_Reports:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_General_Reports;
                                break;
                            case EDIFileType.General_Statements:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_General_Statements;
                                break;
                            case EDIFileType.General_WorkedTransaction:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_General_WorkedTransaction;
                                break;
                            case EDIFileType.OutBox_Statements:
                                _remotePath = _CurrentFTPParameter.ClaimManagement_OutBox_StatementsSubmission;
                                break;
                            default:
                                break;
                        }
                        #endregion

                        _remotePath = "/" + _remotePath;

                        _CurrentProcessStarted = true;
                        for (int i = 0; i <= _uploadfiles.Count - 1; i++)
                        {
                            _encryptedLocalFilePath = "";
                            _localPath = _uploadfiles[i].ToString();
                            System.IO.FileInfo oFile = new System.IO.FileInfo(_localPath);

                            _remotefilePath = _remotePath + "/" + oFile.Name;
                            _fileSize = oFile.Length;

                            oFile = new System.IO.FileInfo(_localPath);
                            _remotefilePath = _remotePath + "/" + oFile.Name;
                            _fileSize = oFile.Length;
                            _secure_FTP.BeginPutFile(_localPath, _remotefilePath, new AsyncCallback(SecureUploadCallback), null);

                        }
                        #endregion
                    }
                }
                #endregion

                if (_CurrentProcessStarted == true && _CurrentProcessStopped == true)
                {
                    if (_secure_FTP.State != Rebex.Net.SftpState.Disconnected)
                    {
                        _secure_FTP.Disconnect();
                    }
                    _secure_FTP.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Clearinghouse ftp parameters missing, please setup through gloPM.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool DisconnectSecureFTP()
        {
            bool _isDisconnected = false;
            try
            {
                if (_secure_FTP != null) //Condition Added by Debasish Das on 24th March 2010 (Mantis Bug ID:0001384)
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
            catch (Exception ex)
            {
                _isDisconnected = false;
            }
            return _isDisconnected;
        }

        void _secure_FTP_StateChanged(object sender, Rebex.Net.SftpStateChangedEventArgs e)
        {
            if (!IsDisposed)
                Invoke(new Rebex.Net.SftpStateChangedEventHandler(SecureStateChanged), new object[] { sender, e });
        }

        void _secure_FTP_TransferProgress(object sender, Rebex.Net.SftpTransferProgressEventArgs e)
        {
            if (!IsDisposed)
                Invoke(new Rebex.Net.SftpTransferProgressEventHandler(SecureTransferProgress), new object[] { sender, e });
        }

        public void SecureStateChanged(object sender, Rebex.Net.SftpStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case Rebex.Net.SftpState.Disconnected:
                case Rebex.Net.SftpState.Disposed:
                    break;
                case Rebex.Net.SftpState.Ready:
                    break;
            }
        }

        public void SecureTransferProgress(object sender, Rebex.Net.SftpTransferProgressEventArgs e)
        {
            //if (!e.Finished && _fileSize > 0)
            if (e.State != Rebex.Net.SftpTransferState.None && _fileSize > 0)
            {
                decimal index = (decimal)e.BytesTransferred / (decimal)_fileSize;
                pbTransfer.Value = (int)(index * pbTransfer.Maximum);
                //lblProgress.Text = e.BytesTransferred + " bytes";
            }
        }

        public void SecureUploadCallback(IAsyncResult asyncResult)
        {
            try
            {
                _secure_FTP.EndPutFile(asyncResult);
                if (!IsDisposed)
                    Invoke(new TransferFinishedDelegate(SecureTransferFinished), new object[] { null, true });
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    Invoke(new TransferFinishedDelegate(SecureTransferFinished), new object[] { ex, true });
            }
        }

        private void SecureTransferFinished(Exception error, bool refreshFtp)
        {
            if (error != null)
                rtflog.AppendText(Environment.NewLine + error);

            ShowTransferStatus();
            _fileSize = -1;
            pbTransfer.Value = 0;
            _CurrentProcessStopped = true;
            tsb_Close.Enabled = true;
            if (error == null) { _DialogResult = true; }
            this.Close();

        }

        #endregion " Secure FTP Upload "
    }
}
