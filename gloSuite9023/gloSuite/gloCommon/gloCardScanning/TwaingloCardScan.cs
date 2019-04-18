using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PegasusImaging.WinForms.TwainPro5;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Drawing;

namespace gloCardScanning
{
    class TwaingloCardScan : IDisposable
    {
        #region "Private Variables"
        string _sScannerType = string.Empty;
        float _Resolution = 300;
        float _ColorScheme = 2;
        bool _DuplexScan = false;
        string _sSelectedScannerName = string.Empty;
        private PegasusImaging.WinForms.TwainPro5.TwainDevice twainDevice = null;
        private PegasusImaging.WinForms.TwainPro5.TwainPro twainPro1;
        //private PegasusImaging.WinForms.ImagXpress9.ImageXView imageXView1;
        //private PegasusImaging.WinForms.ImagXpress9.ImagXpress imagXpress1;
        private gloScanImaging.ImageControl imageControl1;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        bool _isCardFrontImage = false;
        bool _isCardBackImage = false;
        string _CardFrontImagePath = string.Empty;
        string _CardBackImagePath = string.Empty;
        string _CardFaceImagePath = string.Empty;
        Int64 _PatientID = 0;
        string _TempProcessDirPath = string.Empty;
        string _ScanImageDir = "ScanImages";
        float _fRmtScanerX = 0;
        float _fRmtScanerY = 0;
        float _fRmtScanerHeight = 0;
        float _fRmtScanerWidth = 0;
      //  string _sSelectedRemoteScanner = "";
        #endregion

        #region "Public Properties"
        public string CardFrontImagePath
        {
            get { return _CardFrontImagePath; }
            set { _CardFrontImagePath = value; }
        }
        public string CardBackImagePath
        {
            get { return _CardBackImagePath; }
            set { _CardBackImagePath = value; }
        }
        public string CardFaceImagePath
        {
            get { return _CardFaceImagePath; }
            set { _CardFaceImagePath = value; }
        }
        public string TempProcessDirPath
        {
            get { return _TempProcessDirPath; }
        }

        #endregion "Public Properties"

        #region "Constructor"
        public TwaingloCardScan(Int64 _PatientID)
        {
            this._PatientID = _PatientID;


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }
            //this.imagXpress1 = new PegasusImaging.WinForms.ImagXpress9.ImagXpress();
            this.twainPro1 = new PegasusImaging.WinForms.TwainPro5.TwainPro();

            string RootPath = gloSettings.FolderSettings.AppTempFolderPath; //appSettings["StartupPath"].ToString();
            if (_messageBoxCaption == "gloEMR")
            {
                _TempProcessDirPath = RootPath + _ScanImageDir;
            }
            else
            {
                _TempProcessDirPath = RootPath + _ScanImageDir;
            }
            if (Directory.Exists(_TempProcessDirPath) == false)
            { Directory.CreateDirectory(_TempProcessDirPath); }

            #endregion

        }


        public void Dispose()
        {
            // Perform any object clean up here.

            // If you are inheriting from another class that
            // also implements IDisposable, don't forget to
            // call base.Dispose() as well.
            DisposeTwainObjects();

            if (imageControl1 != null)
            {
                imageControl1.Dispose();
                imageControl1 = null;
            }
        }
        #endregion "Constructor"

        #region" Private Method and Scanning Method"
        public void setImagePath()
        {
            String _IntFileName = _PatientID + "_" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");//DateTime.Now.ToString("MM dd yyyy - hh mm ss tt") + " " + DateTime.Now.Millisecond.ToString() + System.Guid.NewGuid().ToString() + ".txt";DateTime.Now.ToString("MM dd yyyy - hh mm ss fff tt") + System.Guid.NewGuid().ToString();string.Format(DateTime.Now.ToString(), "mmddyyyyhhmmtt").ToString().Replace("/", "").Replace(":", "");

            _CardFrontImagePath = _TempProcessDirPath + "\\" + _IntFileName + "-CardFront.jpg";
            _CardBackImagePath = _TempProcessDirPath + "\\" + _IntFileName + "-CardFront-back.jpg";
            _CardFaceImagePath = _TempProcessDirPath + "\\" + _IntFileName + "-CardFace.jpg";
            _IntFileName = null;
        }
        private void LoadSettings()
        {
            using (gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting())
            {
                try
                {

                    #region " Get Resolution settings for Insurance "

                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ResolutionInsurance"))
                    {
                        case "300":
                            _Resolution = 300;
                            break;
                        case "600":
                            _Resolution = 600;
                            break;
                        default:
                            _Resolution = 600;
                            break;
                    }

                    #endregion

                    #region " Get ColorScheme Settings for Scan "

                    switch (oSettings.ReadSettings_XML("CardScannerSettings", "ColorScheme"))
                    {
                        case "4"://true color
                            _ColorScheme = 2;
                            break;
                        case "1"://gray 
                            _ColorScheme = 1;
                            break;
                        case "2"://black & white
                            _ColorScheme = 0;
                            break;
                        default:
                            _ColorScheme = 2;
                            break;
                    }


                    #endregion

                    #region " Get Scan Mode Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex") != "")
                    {
                        string _scanduplex = oSettings.ReadSettings_XML("CardScannerSettings", "ScanDuplex");
                        if (_scanduplex == "1")
                        {
                            _DuplexScan = true;
                        }
                        else
                        {
                            _DuplexScan = false;

                        }
                    }
                    else
                    {
                        _DuplexScan = false;

                    }

                    #endregion

                    //Added on 05052010
                    #region " Get Selected Scanner Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner") != "")
                    {
                        if (oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner") == "None")
                        {
                            _sSelectedScannerName = "None";
                        }
                        else
                        {
                            string _sScannerName = oSettings.ReadSettings_XML("CardScannerSettings", "SelectedScanner");
                            if (_sScannerName.ToString().Trim().Length > 0)
                            {
                                _sSelectedScannerName = _sScannerName;
                            }
                            else
                            {
                                _sSelectedScannerName = "None";
                            }
                        }

                    }
                    else
                    {
                        _sSelectedScannerName = "None";
                    }

                    #endregion


                    #region " Get ScannerType Settings "

                    if (oSettings.ReadSettings_XML("CardScannerSettings", "ScannerType") != "")
                    {
                        string _sSType = oSettings.ReadSettings_XML("CardScannerSettings", "ScannerType");
                        if (_sSType.ToString().Trim().Length > 0)
                        {
                            _sScannerType = _sSType;
                        }
                        else
                        {
                            //? _sScannerType = "Other";   ?????????????????????
                        }
                        _sSType = null;
                    }
                    else
                    {
                        // ?_sScannerType = "";                 ???????????????
                    }

                    #endregion


                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oSettings != null) { oSettings.Dispose(); }
                }
            }
        }
        public bool TwainScaningImage()
        {
            bool _result = false;
            _isCardBackImage = false;
            _isCardFrontImage = false;
            String _scanner = "";
            twainPro1.Licensing.UnlockRuntime(1808984205, 249325542, 1216513884, 14413);
            //imagXpress1.Licensing.UnlockRuntime(1908208815, 373700144, 1341181380, 19197);
            DisposeTwainObjects();
            LoadSettings();
            setImagePath();
            //Added Clear Cards                
            if (twainDevice == null)
            {
                twainDevice = new PegasusImaging.WinForms.TwainPro5.TwainDevice(twainPro1);
                twainDevice.Scanned += new PegasusImaging.WinForms.TwainPro5.ScannedEventHandler(this.twainDevice_Scanned);

            }
            DataSourceCollection oScanners = null;
            try
            {
                twainDevice.ShowUserInterface = false;
                if (_sSelectedScannerName == "None")
                {
                    twainDevice.SelectSource();
                    if (twainDevice.CloseOnCancel == false)
                    {
                        if (twainDevice != null)
                        {
                            twainDevice.CloseSession();

                            twainDevice.Dispose();
                            twainDevice = null;
                        }

                        return false;
                    }
                    else
                    {
                        try
                        {
                            twainDevice.OpenDataSourceManager();
                            oScanners = new DataSourceCollection(twainDevice);
                            if (oScanners != null)
                            {
                                try
                                {
                                    if (oScanners.Current >= 0)
                                    {
                                        _scanner = oScanners[oScanners.Current].ToString().ToUpper();
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                        finally
                        {
                            if (oScanners != null)
                            {
                                oScanners.Dispose();
                                oScanners = null;
                            }
                        }
                    }
                }
                else
                {

                    //Scanner Selection 
                    try
                    {
                        twainDevice.OpenDataSourceManager();
                        oScanners = new DataSourceCollection(twainDevice);
                        if (oScanners != null)
                        {
                            if (_sSelectedScannerName.ToString().Trim().Length > 0)
                            {
                                for (int i = 0; i <= oScanners.Count - 1; i++)
                                {

                                    if (_sSelectedScannerName == oScanners[i].ToString())
                                    {
                                        oScanners.Current = i;
                                        _scanner = oScanners[oScanners.Current].ToString().ToUpper();
                                        break;
                                    }
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
                        if (oScanners != null)
                        {
                            oScanners.Dispose();
                            oScanners = null;
                        }
                    }

                }
                if (twainDevice == null)
                    return false;

                twainDevice.OpenSession();
                PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueFloat myCap = null;//04-03-2011 : Commperss



                // Write the string to a file.
                //System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\Result.txt");


                //try
                //{
                //    string text = System.IO.File.ReadAllText("C:\\AdvancedCapability.txt");
                //    string[] splittext = text.Split(',');
                //    for (int i = 0; i < splittext.Length; i++)
                //    {
                //        string[] stext = splittext[i].Split('=');
                //        try
                //        {

                //            if (twainDevice.IsCapabilitySupported(Convert.ToInt32(stext[1].Trim())))
                //            {
                //                try
                //                {

                //                    CapabilityContainerOneValueFloat myCap1 = (CapabilityContainerOneValueFloat)twainDevice.GetCapability(Convert.ToInt32(stext[1].Trim()));
                //                    file.WriteLine(stext[0].ToString() + " " + myCap1.Capability + " " + myCap1.Value);
                //                }
                //                catch (Exception e1)
                //                {
                //                    try
                //                    {
                //                        PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueString oCapabilityContainerEnum = (PegasusImaging.WinForms.TwainPro5.CapabilityContainerOneValueString)twainDevice.GetCapability(Convert.ToInt32(stext[1].Trim()));
                //                        file.WriteLine(stext[0].ToString() + " " + oCapabilityContainerEnum.Capability + " " + (oCapabilityContainerEnum.Value));


                //                    }
                //                    catch (Exception e2)
                //                    {
                //                        MessageBox.Show("Error");
                //                    }
                //                }
                //            }
                //        }
                //        catch (Exception e)
                //        {
                //            twainDevice.GetCapability(Convert.ToInt32(stext[1].Trim()));
                //            MessageBox.Show("Error");
                //        }
                //    }
                //}
                //catch (Exception e)
                //{ 
                //}
                //file.Close();

                if (twainDevice.IsCapabilitySupported(Capability.IcapSupportedSizes) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapSupportedSizes);
                    if ((_scanner.Contains("SCANSHELL") || (_sSelectedScannerName.ToUpper().Contains("SCANSHELL"))))
                    {
                        myCap.Value = (float)53;

                        twainDevice.SetCapability(myCap);
                        myCap = null;
                        if (twainDevice.IsCapabilitySupported(Capability.IcapUnits) == true)
                        {
                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                            myCap.Value = (float)0; //Inches
                            twainDevice.SetCapability(myCap);
                            myCap = null;
                        }

                    }
                    else
                    {

                        myCap.Value = (float)13;
                        twainDevice.SetCapability(myCap);
                        myCap = null;
                        if (twainDevice.IsCapabilitySupported(Capability.IcapUnits) == true)
                        {
                            myCap = new CapabilityContainerOneValueFloat(Capability.IcapUnits);
                            myCap.Value = (float)0; //Inches
                            twainDevice.SetCapability(myCap);
                            myCap = null;
                        }
                    }

                    if (_sSelectedScannerName == "None")
                    {

                        if (_scanner.Contains("FUJITSU") || _scanner.ToUpper().Contains("FUJITSU")) //check scanner setting for selected scaner
                        {
                            // twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0.0f, 3.583f, 2.166f);
                            twainDevice.ImageLayout = new System.Drawing.RectangleF(0.25f, 0.0f, 3.583f, 2.166f);

                        }
                        else if (_scanner.Contains("SCANSHELL") || _scanner.ToUpper().Contains("SCANSHELL"))////check scanner setting for selected scaner
                        {
                            twainDevice.ImageLayout = new System.Drawing.RectangleF(2.5f, 0.0f, 3.583f, 2.166f);
                        }
                        else
                        {
                            twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0.0f, 3.583f, 2.166f);
                        }

                    }
                    else if (_sSelectedScannerName == "RemoteScan(TM)")
                    {
                       
                        LoadRemoteScannerSetting();
                        twainDevice.ImageLayout = new System.Drawing.RectangleF(_fRmtScanerX, _fRmtScanerY, _fRmtScanerWidth, _fRmtScanerHeight);                        
                    }
                    else
                    {

                        if (_scanner.Contains(_sSelectedScannerName) || _scanner.ToUpper().Contains(_sSelectedScannerName.ToUpper()))
                        {

                            if (_sSelectedScannerName.Contains("FUJITSU") || _sSelectedScannerName.ToUpper().Contains("FUJITSU")) //check scanner setting for selected scaner
                            {
                                //twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0.0f, 3.583f, 2.166f); - original commented before 7006 starting
                                twainDevice.ImageLayout = new System.Drawing.RectangleF(0.25f, 0.0f, 3.583f, 2.166f);
                            }

                            else if (_sSelectedScannerName.ToUpper().Contains("SCANSHELL") || _sSelectedScannerName.Contains("SCANSHELL"))////check scanner setting for selected scaner
                            {
                                //3000D
                                if (_sSelectedScannerName.Contains("3100"))
                                {
                                    twainDevice.ImageLayout = new System.Drawing.RectangleF(0.0f, 0.0f, 3.583f, 2.166f);
                                }
                                else
                                {
                                    twainDevice.ImageLayout = new System.Drawing.RectangleF(2.5f, 0.0f, 3.583f, 2.166f);
                                }

                            }
                            else if (_sSelectedScannerName.ToString().Trim() == string.Empty)
                            {
                                twainDevice.ImageLayout = new System.Drawing.RectangleF(0f, 0.0f, 3.583f, 2.166f);
                            }
                            //additional else for remote scanner testing
                            else
                            {
                                //twainDevice.ImageLayout = new System.Drawing.RectangleF(2.5f, 0.0f, 3.583f, 2.166f); - commented for remote scanner testing

                                //string readtext = System.IO.File.ReadAllText("C:\\ScanHeight.txt").ToString();
                                //string[] arrval = readtext.Split(',');
                                //float _scanX = float.Parse(arrval[0].ToString());
                                //float _scanY = float.Parse(arrval[1].ToString());
                                //float _scanW = float.Parse(arrval[2].ToString());
                                //float _scanH = float.Parse(arrval[3].ToString());



                                //twainDevice.ImageLayout = new System.Drawing.RectangleF(_scanX, _scanY, _scanW, _scanH);

                                twainDevice.ImageLayout = new System.Drawing.RectangleF(2.5f, -0.003f, 3.583f, 2.166f);
                            }
                        }

                    }

                }

                myCap = null;//04-03-2011 : Commperss

                if (twainDevice.IsCapabilitySupported(Capability.CapDuplex) == true)
                {
                    //  myCap = new CapabilityContainerOneValueFloat(Capability.CapDuplex);
                    myCap = null;//04-03-2011 : Commperss

                    if (twainDevice.IsCapabilitySupported(Capability.CapDuplexEnabled) == true)
                    {
                        myCap = new CapabilityContainerOneValueFloat(Capability.CapDuplexEnabled);
                        if (_DuplexScan == true)
                        {
                            myCap.Value = (float)(1);
                        }
                        else
                        {
                            myCap.Value = (float)(0);
                        }
                        twainDevice.SetCapability(myCap);
                        myCap = null;
                    }
                }

                myCap = null;//04-03-2011 : Commperss

                if (twainDevice.IsCapabilitySupported(Capability.IcapXResolution) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapXResolution);
                    myCap.Value = (float)_Resolution;
                    twainDevice.SetCapability(myCap);
                    myCap = null;//04-03-2011 : Commperss
                }
                myCap = null;//04-03-2011 : Commperss

                if (twainDevice.IsCapabilitySupported(Capability.IcapYResolution) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapYResolution);
                    myCap.Value = (float)_Resolution;
                    twainDevice.SetCapability(myCap);
                    myCap = null;//04-03-2011 : Commperss
                }
                myCap = null;//04-03-2011 : Commperss

                if (twainDevice.IsCapabilitySupported(Capability.IcapPixelType) == true)
                {
                    myCap = new CapabilityContainerOneValueFloat(Capability.IcapPixelType);
                    myCap.Value = Single.Parse(_ColorScheme.ToString());
                    twainDevice.SetCapability(myCap);
                    myCap = null;//04
                }
                myCap = null;//04-03-2011 : Commperss  
                twainDevice.ShowUserInterface = false;
                if (twainDevice != null)
                {
                    try
                    {
                        twainDevice.StartSession();
                    }
                    catch //(Exception ex)
                    {

                        twainDevice.CloseSession();
                    }
                    //twainDevice.CloseSession();
                }


            }

            catch (TwainDllLoadException ex)
            {
                ex.ToString();
                MessageBox.Show("Scanner is not connected.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;

                if (twainDevice != null)
                {
                    twainDevice.CloseSession();

                    twainDevice.Dispose();
                    twainDevice = null;
                }
            }
            catch (TwainException ex)
            {

                ex.ToString();
                MessageBox.Show("Scanner is not connected.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _result = false;
                //}

                if (twainDevice != null)
                {
                    twainDevice.CloseSession();
                    twainDevice.Dispose();
                    twainDevice = null;
                }
            }
            catch (TwainProException ex)
            {

                if (ex.ErrorNumber == 7004)
                {
                    MessageBox.Show("Scan canceled by the user", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //clear images


                    _result = false;
                }
                else
                {
                    MessageBox.Show("Scanner is not connected.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _result = false;
                }

                //if (twainDevice != null)
                //{
                //    twainDevice.CloseSession();

                //    twainDevice.Dispose();
                //    twainDevice = null;
                //}
            }
            finally
            {
                _scanner = null;
                DisposeTwainObjects();

            }

            return _result;


        }
        private DataTable  getRemoteScannerNames()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer( appSettings["DataBaseConnectionString"].ToString());
            DataTable dt=null; 
            try
            {
               
                    if (oDB != null)
                    {
                        oDB.Connect(false);
                        string SqlQuery = "select sScannerName FROM  CS_ScannerCoordinates";
                        oDB.Retrive_Query(SqlQuery, out  dt);
                    }
               
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }
        private void LoadRemoteScannerSetting()
        {
            DataTable _dtRemoteScanner = null;
            try
            {
                bool _issetProperties = false;
                _dtRemoteScanner = getRemoteScannerNames();
                if (_dtRemoteScanner != null && _dtRemoteScanner.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtRemoteScanner.Rows.Count; i++)
                    {
                        using (gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting())
                        {
                            if (oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "ISDEFAULT") == "True")
                            {
                                _fRmtScanerX = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "X").ToString()));
                                _fRmtScanerY = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "Y").ToString()));
                                _fRmtScanerHeight = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "HEIGHT").ToString()));
                                _fRmtScanerWidth = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtRemoteScanner.Rows[i][0].ToString(), "WIDTH").ToString()));
                                _issetProperties = true;
                                break;
                            }
                        }
                    }
                    if (!_issetProperties && _dtRemoteScanner != null)
                    {
                        DataRow[] _dtr = _dtRemoteScanner.Select(" sScannerName  = 'FUJITSU fi-6130' ");
                        if (_dtr != null && _dtr.Length > 0)
                        {
                            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                            _fRmtScanerX = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr[0].ToString(), "X").ToString()));
                            _fRmtScanerY = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr[0].ToString(), "Y").ToString()));
                            _fRmtScanerHeight = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr.ToString(), "HEIGHT").ToString()));
                            _fRmtScanerWidth = float.Parse((oSettings.ReadSettings_XML("ScannerProperties_" + _dtr[0].ToString(), "WIDTH").ToString()));
                            oSettings.Dispose();
                            oSettings = null;
                        }
                        _dtr = null;
                    }
                }
            }
            catch (Exception Ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                if (_dtRemoteScanner != null) { _dtRemoteScanner.Dispose(); _dtRemoteScanner = null; }
            }
        }
        #endregion

        #region "After Scanned TwainDevice"
        private void twainDevice_Scanned(object sender, PegasusImaging.WinForms.TwainPro5.ScannedEventArgs e)
        {
            Bitmap TempImg = null;
            try
            {
                if (imageControl1 == null)
                {
                    imageControl1 = new gloScanImaging.ImageControl();
                }
                try
                {
                    TempImg = e.ScannedImage.ToBitmap();
                }
                catch (Exception)
                {
                    TempImg = null;
                }

                if (TempImg != null)
                {
                    if (!_isCardFrontImage)
                    {
                        try
                        {
                            // e.ScannedImage.SaveFile(_CardFrontImagePath);
                            TempImg.Save(_CardFrontImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageControl1.SetImageWithPath(_CardFrontImagePath, true);
                            _isCardFrontImage = true;
                        }
                        catch { }
                    }
                    else if (!_isCardBackImage)
                    {
                        try
                        {
                            // e.ScannedImage.SaveFile(_CardBackImagePath);
                            TempImg.Save(_CardBackImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageControl1.SetImageWithPath(_CardBackImagePath, true);
                            _isCardBackImage = true;

                        }
                        catch { }
                    }

                    if (TempImg != null)
                    {
                        TempImg.Dispose();
                        TempImg = null;
                    }
                }
            }
            catch (PegasusImaging.WinForms.TwainPro5.TwainProException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (imageXView1.Image != null)
                //{
                //    imageXView1.Image.Dispose();
                //    imageXView1.Image = null;
                //}
            }


        }
        #endregion

        #region "Disposing twain object"
        public void DisposeTwainObjects()
        {
            try
            {
                if (twainDevice != null)
                {
                    twainDevice.Scanned -= new PegasusImaging.WinForms.TwainPro5.ScannedEventHandler(this.twainDevice_Scanned);
                    twainDevice.CloseSession();
                    twainDevice.Dispose();
                    twainDevice = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public void DisposeReferenceObject()
        {

            Marshal.IsComObject(twainPro1);
            Marshal.ReleaseComObject(twainPro1);


        }
        #endregion

    }
}
