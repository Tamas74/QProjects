using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace gloCardScanning
{
    public enum CardScanType
    {
        None = 1, DrivingLicense = 2, InsuranceCard = 3, CardImages = 4, Cheque = 5, NewDrivingLicense = 6
    }

    public class gloCardScanning
    {
        string _MessageBoxCaption = "gloPM";

        private string _databaseconnectionstring = "";

        #region "Constructor & Destructor"

        public gloCardScanning(string databaseconnectionstring)
        {
            _databaseconnectionstring = databaseconnectionstring;
            //Added By Shweta 20091107
            //Against Bug Id:4471
            //To set message box caption 
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }
            //End Code Adding 20091107s
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~gloCardScanning()
        {

            Dispose(false);
        }

        #endregion

        public static Image ImageFromFile(string FilePathName)
        {
            Image myImage = null;
            try
            {               
                myImage = Image.FromFile(FilePathName);
                //ConvertImage(FilePathName);
            }
            catch (Exception ex)
            {
                myImage = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            return myImage;
        }
        private static bool FreeBigImageResources(ref BitmapImage big)
        {

            if (big != null)
            {


                try
                {
                    big.StreamSource.Dispose();

                }
                catch
                {
                }

                big.UriSource = null;

                try
                {
                    big.StreamSource.Dispose();

                }
                catch
                {
                }


                try
                {
                    big.BeginInit();
                    big.UriSource = null;
                    big.EndInit();
                }
                catch
                {
                }
                try
                {
                    big.StreamSource.Dispose();

                }
                catch
                {
                }

                //08-May-13 Aniket: Resolving Memory Leaks
                //big = New BitmapImage()
                //big.UriSource = Nothing

                big = null;
                return true;

            }
            else
            {
                return false;
            }

        }

        public static void ConvertImage(string FilePathName)
        {
            
            BitmapImage big = new BitmapImage();
            bool errorInLoading = false;
            try
            {
                big.BeginInit();

                big.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                big.CacheOption = BitmapCacheOption.OnLoad;

                big.UriSource = new Uri(FilePathName, UriKind.RelativeOrAbsolute);
                // big.EndInit();

                double orgHScale = big.DpiX;
                double orgVScale = big.DpiY;
                int myWidth = big.PixelWidth;
                int myHeight = big.PixelHeight;

                //big.UriSource = null;

                //try
                //{                
                //    big.BeginInit();
                //    big.UriSource = null;
                //    big.EndInit();

                //}
                //catch
                //{
                //}

                //big = new BitmapImage();
                //big.UriSource = null;

                double myArea = 0;
                double myScale = 1.0;
                double curPixel = 0;
                myArea = Convert.ToDouble(myWidth) * Convert.ToDouble(myHeight);
                if ((myArea > (1280 * 1024)))
                {
                    myScale = System.Math.Sqrt((1280 * 1024) / myArea);
                    // big.DecodePixelWidth = Convert.ToInt32(Convert.ToDouble(myWidth) * myScale / curPixel);
                }
                else
                {
                    // big.DecodePixelWidth = Convert.ToInt32(Convert.ToDouble(myWidth) / curPixel);
                }
                Double horzScale = 1280.0 / Convert.ToDouble(myWidth);
                Double verScale = 1024.0 / Convert.ToDouble(myHeight);

                if (myScale > horzScale)
                {
                    myScale = horzScale;
                }
                if (myScale > verScale)
                {
                    myScale = verScale;
                }
                big.DecodePixelWidth = Convert.ToInt32(Convert.ToDouble(myWidth) * myScale / curPixel);
                big.DecodePixelHeight = Convert.ToInt32(Convert.ToDouble(myHeight) * myScale / curPixel);

                big.EndInit();
            }
            catch
            {
                errorInLoading = true;
            }
            if (!errorInLoading)
            {
                    string myImagePath = FilePathName;
                    FileInfo oDFileInfo = null;
                    string myDir = null;
                    string oFileName = null;
                    try
                    {
                        oDFileInfo = new FileInfo(FilePathName);
                        myDir = oDFileInfo.DirectoryName;
                        oFileName = gloGlobal.clsFileExtensions.NewDocumentName(myDir, ".bmp", "yyyyMMddHHmmssffff"); // myDir + "\\" + getUniqueID() + ".bmp";
                        oDFileInfo = null;

                        BmpBitmapEncoder Encoder = new BmpBitmapEncoder();

                        //Encoder.Frames.Add(BitmapFrame.Create(small))
                        //'
                        Encoder.Frames.Add(BitmapFrame.Create(big));
                        FileStream fs = new FileStream(oFileName, FileMode.Create);
                        Encoder.Save(fs);
                        Encoder = null;

                        try
                        {
                            fs.Close();
                            fs.Dispose();
                            fs = null;

                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        myImagePath = null;
                        myDir = null;
                        oFileName = null;
                    }
            }
            FreeBigImageResources(ref big);

        }

        public void ScanCard(string RootPath, Int64 PatientID)
        {
            try
            {
                IntPtr sc_activeWindow = gloGlobal.WordDialogBoxBackgroundCloser.GetForegroundWindow();
                frmCardImage oCardScanning = new frmCardImage(_databaseconnectionstring, PatientID, sc_activeWindow);
                if (oCardScanning != null)
                {
                    oCardScanning.StartPosition = FormStartPosition.CenterScreen;
                    oCardScanning.ShowDialog(oCardScanning.Parent);
                    if (oCardScanning != null)
                    {
                        oCardScanning.Dispose();
                        oCardScanning = null;
                    }
                }
                //frmScanCard_New oCardScanning = new frmScanCard_New(RootPath, PatientID, _databaseconnectionstring, "Insurance");
                //oCardScanning._ErrorMessage = "";
                //oCardScanning.ShowDialog();
                //oCardScanning = null;

            }
            catch (Exception ex)
            {
                //If Prerequsite for scanner not available.
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Component required for card scanning is not available.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        public void ScanCard(string RootPath, Int64 PatientID,Form oParent)
        {
            try
            {
                IntPtr sc_activeWindow = gloGlobal.WordDialogBoxBackgroundCloser.GetForegroundWindow();
                frmCardImage oCardScanning = new frmCardImage(_databaseconnectionstring, PatientID, sc_activeWindow);
                if (oCardScanning != null)
                {
                    oCardScanning.StartPosition = FormStartPosition.CenterScreen;
                    oCardScanning.ShowDialog(oParent);
                    if (oCardScanning != null)
                    {
                        oCardScanning.Dispose();
                        oCardScanning = null;
                    }
                }
            }
            catch (Exception ex)
            { 
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Component required for card scanning is not available.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ScanCard_New(string RootPath, Int64 PatientID)
        {
            try
            {

                frmScanCard_New oCardScanning = new frmScanCard_New(PatientID, _databaseconnectionstring, "Insurance");
                //SLRAgain: Check for Null
                if (oCardScanning != null)
                {
                    oCardScanning._ErrorMessage = "";
                    oCardScanning.ShowDialog(oCardScanning.Parent);
                    if (oCardScanning != null)
                    {
                        oCardScanning.Dispose();
                        oCardScanning = null;
                    }
                }
            }
            catch (Exception ex)
            {
                //If Prerequsite for scanner not available.
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show("Component required for card scanning is not available.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        
        //public static string  getUniqueID()
        //{
        //    bool firstTime = true;
        //    System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
        //    DateTime myTime = default(DateTime);
        //    if (firstTime == true)
        //    {
        //        firstTime = false;
        //        myTime = DateTime.Now;
        //        myWatch.Start();
        //    }
        //    TimeSpan TmSp = new TimeSpan(myTime.Ticks + myWatch.ElapsedTicks);
        //    return TmSp.Ticks.ToString();


        //}
        //private static System.Diagnostics.Stopwatch myWatch = null;
        //private static bool firstTime = true;
        //private static DateTime myTime = DateTime.Now;
        //public static string getUniqueID()
        //{

        //    string _returnUnqueID = "";
        //    if (myWatch == null)
        //    {
        //        myWatch = new System.Diagnostics.Stopwatch();
        //        firstTime = true;
        //    }

        //    if (firstTime == true)
        //    {
        //        firstTime = false;
        //        myTime = DateTime.Now;
        //        myWatch.Start();
        //    }
        //    TimeSpan TmSp = new TimeSpan(myTime.Ticks + myWatch.ElapsedTicks);
        //    _returnUnqueID = TmSp.Ticks.ToString();
        //    return _returnUnqueID;
        //}
        public bool DeleteCard(bool DeleteAll, Int64 PatientID, Int64 CardNumber)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            //SLRAgain: Check ODB=null?

            string _strSQL = "";
            bool _result = false;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                if (oDB != null)
                {
                    //SLRAgain: It should be in else portion:

                    if (DeleteAll == true)
                    {
                        _strSQL = "DELETE FROM Patient_Cards WHERE nPatientID = " + PatientID + "";
                    }
                    else
                    {
                        _strSQL = "DELETE FROM Patient_Cards WHERE nPatientID = " + PatientID + " AND nPatientCardNo = " + CardNumber + "";
                    }
                    //SLRAgain: Check for _result depending upon connection, status of exeecute_query etc:
                    if (oDB.Connect(false))
                    {
                        oDB.Execute_Query(_strSQL);
                    }

                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                _result = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                _strSQL = null;
            }
            return _result;
        }

        public bool SaveScanData(Int64 PatientID, System.Drawing.Image ScanImage, System.Drawing.Image BackImage, DateTime ScanDate, Int64 CardTypeId, string ScannedData, Int64 nUserId)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            int quality = 30;
            //@nPatientID ,@iCard,@dtScanDateTime ,@nCardTypeID ,@sScannedData
            //MessageBox.Show("Parameter Assigning", "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //SLRAgain: Check for ODB, oParameters :
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                if (oDB != null)
                {
                    if (oDB.Connect(false))
                    {
                        oParameters = new gloDatabaseLayer.DBParameters();
                        if (oParameters != null)
                        {
                            oParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                            if (ScanImage != null)
                            {
                                System.Drawing.Image ilogo = (Image)(ScanImage.Clone());

                                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                //ilogo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                //Byte[] arrImage = ms.GetBuffer();

                                Byte[] arrImage = SaveJpegIntoByte(ilogo, quality);

                                oParameters.Add("@iCard", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                                //ms.Close();
                                //if (ms != null)
                                //{
                                //    ms.Dispose();
                                //    ms = null;
                                //}

                                if (ilogo != null)
                                {
                                    ilogo.Dispose();
                                    ilogo = null;
                                }
                                arrImage = null;
                            }
                            if (BackImage == null)
                            {
                                oParameters.Add("@iCardBack", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                            }
                            else
                            {

                                if (BackImage != null)
                                {
                                    System.Drawing.Image iBack = (Image)(BackImage.Clone());
                                    //System.IO.MemoryStream msImageBack = new System.IO.MemoryStream();
                                    //iBack.Save(msImageBack, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    //Byte[] arrImageBack = msImageBack.GetBuffer();

                                    Byte[] arrImageBack = SaveJpegIntoByte(iBack, quality);

                                    oParameters.Add("@iCardBack", arrImageBack, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                                    //msImageBack.Close();
                                    //if (msImageBack != null)
                                    //{
                                    //    msImageBack.Dispose();
                                    //    msImageBack = null;
                                    //}

                                    if (iBack != null)
                                    {
                                        iBack.Dispose();
                                        iBack = null;
                                    }
                                    arrImageBack = null;
                                }
                            }


                            oParameters.Add("@dtScanDateTime", ScanDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                            oParameters.Add("@nCardTypeID", CardTypeId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oParameters.Add("@sScannedData", ScannedData, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            //Added by Mitesh
                            oParameters.Add("@nUserId", nUserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            //
                            int _res = oDB.Execute("gsp_IN_PatientCards", oParameters);
                           
                        }
                        //MessageBox.Show("Database Status" + _res.ToString(), "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                return true;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                //if (ScanImage != null)
                //{ 
                //    ScanImage.Dispose();
                //    ScanImage = null;
                //}

                //if (BackImage != null)
                //{   
                //    BackImage.Dispose(); 
                //    BackImage = null; 
                //}
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }


        }


        public Byte[] SaveJpegIntoByte(Image image, long compression_level)
        {
            // compression level.
            EncoderParameters encoder_params = new EncoderParameters(1);
            encoder_params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression_level);

            // Prepare the codec to encode.
            ImageCodecInfo image_codec_info = GetEncoderInfo("image/jpeg");

            // Save the file.
            MemoryStream memory_stream = new MemoryStream();
            image.Save(memory_stream, image_codec_info, encoder_params);
            try
            {
                memory_stream.Flush();
            }
            catch
            {
            }
            Byte[] arrImage = memory_stream.ToArray();
            try
            {
                memory_stream.Close();
                memory_stream.Dispose();
            }
            catch
            {
            }
            finally
            {
                if (encoder_params != null) { encoder_params.Dispose(); encoder_params = null; }
                image_codec_info = null; 
            }

            return arrImage;
            // return memory_stream;
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders = null;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i <= encoders.Length; i++)
            {
                if (encoders[i].MimeType == mimeType)
                {
                    return encoders[i];
                }
            }
            return null;
        }
        public Int64 IsInsuranceExists(string InsuranceName)
        {
            //Method to Check if the Scan Card Insurance Type is Registered or not
            //If exists return the Insurance ID else return 0.

            gloDatabaseLayer.DBLayer oDB = null;
            //SLRAgain: Check for new...

            string strQuery = "";
            Int64 _ContactId = 0;
            DataTable dt = null;
            try
            {
                //UpdateLog("Start - IsInsurance Exists");
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                if (oDB != null)
                {
                    //Check for the Insurance exists in Contact Master if not Register the Insurance first
                    if (oDB.Connect(false))
                    {
                        //strQuery = "select nContactID from Contacts_MST where sName = '" + oScannedInsurance.PlanProvider + "' ";
                        strQuery = "select nContactID from Contacts_MST where sName = '" + InsuranceName + "' ";
                        oDB.Retrive_Query(strQuery, out dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            _ContactId = Convert.ToInt64(dt.Rows[0][0]);
                        }
                        //UpdateLog("END - IsInsurance Exists");
                    }
                }
                return _ContactId;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                //SLRAgain: reverse dispose(dt first, then db)
                //compress
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                strQuery = null;
            }
        }

        public DataTable IsPatientExists(string _sFirstName, string _sLastName, DateTime _dob)
        {
            //Int64 _PatientId=0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);//compress
            //SLRAgain: Check for new
            DataTable dtPatient = null;
            DataTable returnDtPatient = null;
            try
            {
                if (oDB != null)//Check for null
                {
                    //string dateob=gloDateMaster.gloDate.DateAsNumber(_dob.ToShortDateString());
                    string strquery = "Select nPatientID,sPatientCode,sFirstName,sLastName,dtDOB from Patient where sFirstName='" + _sFirstName + "' and sLastName='" + _sLastName + "' and dtDOB='" + _dob.Date.ToShortDateString() + "'";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strquery, out dtPatient);
                    if (dtPatient != null)
                    {
                        if (dtPatient.Rows.Count > 0)
                        {
                            returnDtPatient = dtPatient;//Compress
                            return returnDtPatient;//dtPatient;
                        }
                    }
                }
                return null;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
                return null;
            }
            finally
            {
               //SLR30: I suppose, this table is to be disposed from the function calling since it is returned..
                //SLRAgain: Check  reverse .
                /*
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;

                }
                 */

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;

                }
           
            }
            //return _PatientId;

        }

        public DataTable GetScannerProperties()
        {

            DataTable _dtScannerProps = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);//compress

            try
            {
                if (oDB != null)//Check for null
                {
                    string strquery = "SELECT nID,sScannerName,nXcoOrdinate,nYcoOrdinate,nWidth,nHeight FROM CS_ScannerCoordinates";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strquery, out _dtScannerProps);
                    strquery = null;
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
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
            return _dtScannerProps;
        }

        //Not in use.
        #region "commented ModifyPatient,ModifyPatientByCriteria"

        //public void ModifyPatient(gloPatient.ScanedPatient oScannedPatient, Int64 _TempPatientID,System.Drawing.Image _pbFaceImage)
        //{
        //        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //        try
        //        {
        //            if (oScannedPatient != null)
        //            {
        //                oDB.Connect(false);
        //                //string strquery="Update Patient set "+        
        //                //        "nSSN = '"+oScannedPatient.SocialSecurity+"'"+
        //                //        ",sFirstName = '" + oScannedPatient.FirstName + "'" +
        //                //        ",sMiddleName = '" + oScannedPatient.MiddleName+"'"+
        //                //        ",sLastName ='" +  oScannedPatient.LastName+"'"+
        //                //        ",dtDOB = '" + Convert.ToDateTime(oScannedPatient.DOB)+"'"+
        //                //        ",nProviderID= '" + oScannedPatient.ProviderId+"'"+
        //                //        ",sGender = '" + oScannedPatient.Sex+"'"+
        //                //        ",sCity = '" + oScannedPatient.City+"'"+
        //                //        ",sCounty = '" + oScannedPatient.County+"'"+
        //                //        ",sZip = '" + oScannedPatient.Zip+"'"+
        //                //        ",sAddressLine1 = '" + oScannedPatient.Address1+"'"+
        //                //        ",sState = '" + oScannedPatient.State + "'" +
        //                //        "where nPatientID='"+_TempPatientID+"'";
        //                //oDB.Execute_Query(strquery);

        //                System.Drawing.Image ilogo = _pbFaceImage;
        //                System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //                ilogo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                Byte[] arrImage = ms.GetBuffer();

        //                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //                oParameters.Add("@sAddressline1", oScannedPatient.Address1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //                oParameters.Add("@sCity", oScannedPatient.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //                oParameters.Add("@sState", oScannedPatient.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //                oParameters.Add("@sZip", oScannedPatient.Zip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //                oParameters.Add("@iPhoto", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
        //                oParameters.Add("@nPatientID", _TempPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
        //                ms.Close();
        //                ms.Dispose();

        //                //string strquery = "Update  Patient set sAddressLine1='" + oScannedPatient.Address1 + "',sCity='" + oScannedPatient.City + "',sState = '" + oScannedPatient.State + "',sZip = '" + oScannedPatient.Zip + "',iPhoto='" + arrImage + "' where nPatientID='" + _TempPatientID + "'";

        //                oDB.Execute("gsp_IN_PatientModifyScan", oParameters);
        //            }
        //        }
        //        catch (Exception Ex)
        //        {
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
        //            MessageBox.Show("Error Occured: "+Ex, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        finally
        //        {
        //            oDB.Disconnect();
        //            oDB.Dispose();
        //        }
        //}

        //Added By MaheshB
        //public void ModifyPatientByCriteria(gloPatient.ScanedPatient oScannedPatient, Int64 _TempPatientID, System.Drawing.Image _pbFaceImage,bool _IsAddress, bool _IsPhoto,bool _IsDob,bool _IsFirstName,bool _IsMiddleName,bool _IsLastName,bool _IsSSN)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    try
        //    {
        //        if (oScannedPatient != null)
        //        {
        //            System.Drawing.Image ilogo = _pbFaceImage;
        //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //            ilogo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //            Byte[] arrImage = ms.GetBuffer();

        //            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //            oParameters.Add("@sAddressline1", oScannedPatient.Address1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //            oParameters.Add("@sCity", oScannedPatient.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //            oParameters.Add("@sState", oScannedPatient.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //            oParameters.Add("@sZip", oScannedPatient.Zip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //            oParameters.Add("@iPhoto", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
        //            oParameters.Add("@DOB", oScannedPatient.DOB, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
        //            oParameters.Add("@nPatientID", _TempPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

        //            oParameters.Add("@IsAddress", _IsAddress, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
        //            oParameters.Add("@IsPhoto", _IsPhoto, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
        //            oParameters.Add("@IsDob", _IsDob, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

        //            oParameters.Add("@sFirstName", oScannedPatient.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //            oParameters.Add("@sMiddleName", oScannedPatient.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //            oParameters.Add("@sLastName", oScannedPatient.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //            oParameters.Add("@sSSN", oScannedPatient.SocialSecurity, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

        //            oParameters.Add("@IsFirstName", _IsFirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
        //            oParameters.Add("@IsMiddleName", _IsMiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
        //            oParameters.Add("@IsLastName", _IsLastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
        //            oParameters.Add("@IsSSN", _IsSSN, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

        //            ms.Close();
        //            ms.Dispose();

        //            //string strquery = "Update  Patient set sAddressLine1='" + oScannedPatient.Address1 + "',sCity='" + oScannedPatient.City + "',sState = '" + oScannedPatient.State + "',sZip = '" + oScannedPatient.Zip + "',iPhoto='" + arrImage + "' where nPatientID='" + _TempPatientID + "'";
        //            oDB.Connect(false);
        //            oDB.Execute("gsp_IN_PatientModifyScan", oParameters);

        //            //string strquery=string.Empty;

        //            //if(_IsAddress==true || _IsPhoto==true||_IsDob==true)
        //            //{
        //            //    strquery = "Update Patient set ";
        //            //    if(_IsAddress==true)
        //            //    {
        //            //        //if (strquery.EndsWith(","))
        //            //        //{
        //            //            strquery = strquery + "sAddressline1='" + oScannedPatient.Address1.Replace("'", "''") + "',sCity='" + oScannedPatient.City.Replace("'", "''") + "',sState='" + oScannedPatient.State.Replace("'", "''") + "',sZip='" + oScannedPatient.Zip + "',";
        //            //        //}
        //            //    }
        //            //    if (_IsPhoto == true)
        //            //    {
        //            //        //if (strquery.EndsWith(","))
        //            //        //{
        //            //            strquery = strquery + "iPhoto='" + arrImage + "',";
        //            //        //}
        //            //    }
        //            //    if (_IsDob == true)
        //            //    {
        //            //        //if (strquery.EndsWith(","))
        //            //        //{
        //            //            strquery = strquery + "dtDOB='" + oScannedPatient.DOB + "'";
        //            //        //}
        //            //    }
        //            //    if(strquery.EndsWith(","))
        //            //    {
        //            //       strquery= strquery.Remove(strquery.Length - 1, 1);
        //            //    }
        //            //}
        //            //if (strquery != "")
        //            //{
        //            //    strquery = strquery + " where nPatientID='" + _TempPatientID + "'";
        //            //    oDB.Connect(false);
        //            //    oDB. Execute_Query(strquery);
        //            //}

        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
        //        MessageBox.Show("Error Occured: " + Ex, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //}

        //Added by Suraj- To update patient's Face photo


        public void ModifyPatientFaceImage(Int64 _TempPatientID, System.Drawing.Image _pbFaceImage)
        {
            //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    try
            //    {
            //        if (_TempPatientID > 0)
            //        {
            //            System.Drawing.Image ilogo = _pbFaceImage;
            //            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //            ilogo.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //            Byte[] arrImage = ms.GetBuffer();



            //            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            //            oParameters.Add("@iPhoto", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);

            //            oParameters.Add("@nPatientID", _TempPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

            //            ms.Close();
            //            ms.Dispose();

            //            oDB.Connect(false);
            //            oDB.Execute("gsp_IN_PatientModifyImage", oParameters);

            //            if (ilogo != null)
            //            {
            //                ilogo.Dispose();
            //                ilogo = null;
            //            }

            //        }
            //    }
            //    catch (Exception Ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            //        MessageBox.Show("Error Occured: " + Ex, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    finally
            //    {
            //        if (_pbFaceImage != null)
            //        {
            //            _pbFaceImage.Dispose();
            //            _pbFaceImage = null;
            //        }

            //        oDB.Disconnect();
            //        oDB.Dispose();
            //    }
        }

        #endregion


        //public System.Drawing.Image Getphoto(Int64 patientid)
        //{
        //    //Function to Get the patient information from Database

        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();

        //    odbparam.Add("@PatienID", patientid, ParameterDirection.Input, SqlDbType.BigInt);

        //    DataTable dt;
        //    System.Drawing.Image _faceimage;
        //    string sql= 

        //    try
        //    {

        //        oDB.Connect(false);
        //        oDB.Retrive("PA_Select_Patient_Cards", odbparam, out  dt);
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                //for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //                //{
        //                if ((dt.Rows[0]["iPhoto"] != DBNull.Value))
        //                    {
        //                        System.Drawing.Image ilogo;
        //                        Byte[] arrPicture = (Byte[])dt.Rows[0]["iPhoto"];
        //                        System.IO.MemoryStream ms = new System.IO.MemoryStream(arrPicture);
        //                        ilogo = System.Drawing.Image.FromStream(ms);
        //                        _faceimage = ilogo;
        //                        if (_faceimage != null)
        //                        {
        //                            return _faceimage;
        //                        }                               
        //                    } 

        //                //}   
        //            }
        //        }
        //        dt.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        ex = null;
        //        throw new Exception("Error while retrieving patient.");
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //   // return opatientcards;
        //}
        //private double zoomfactor = 1.0;

        private static double stepfactor = 0.1;

        private static int defaultZoom = (int)((double)1.0 / stepfactor);

        //
        static readonly double minZoomFactor = 0.1;
        static readonly double maxZoomFactor = 8.9;
        //

        //Start/Rotation
        //private double rotationfactor = 0;
        private static double rotationstepfactor = 1.0;

        private static int rotationdefault = 0;
        //private bool rotateMe = false;

        static readonly UInt16[] magicNumber = new UInt16[] { (UInt16)0x4142, (UInt16)0x424D, (UInt16)0x4349, (UInt16)0x4943, (UInt16)0x4D42, (UInt16)0x5043, (UInt16)0x5450 };

        static readonly UInt16 myMagicSize = (UInt16)magicNumber.Length;

        public UInt16 _isMagicNumber(UInt16 checkMe)
        {
            // BM – Windows 3.1x, 95, NT, ... etc. = 0x4D42
            // BA – OS/2 Bitmap Array = 0x4142
            // CI – OS/2 Color Icon = 0x4943
            // CP – OS/2 Color Pointer = 0x5043
            // IC – OS/2 Icon = 0x4349
            // PT – OS/2 Pointer = 0x5450
            // MB - Machintosh BMP = 0x424D
            //UInt16 mySize = myMagicSize;
            for (UInt16 iCheck = 0; iCheck < myMagicSize; iCheck++)
            {
                UInt16 thisMagicNumber = magicNumber[iCheck];
                if (checkMe == thisMagicNumber)
                {
                    return ((UInt16)(iCheck + myMagicSize + 1));
                }
                else
                {
                    if ((checkMe + iCheck) < thisMagicNumber)
                    {
                        return (iCheck);
                    }
                }
            }
            return myMagicSize;
        }

        public bool _isValidRotationFactor(int myRotation)
        {
            double currentRotation = ((double)myRotation * rotationstepfactor);
            return ((currentRotation <= 360.0) && (currentRotation >= 0.0));
        }

        public bool _isValidZoomFactor(int myZoom)
        {
            double currentZoom = ((double)myZoom * stepfactor);
            return ((currentZoom <= maxZoomFactor) && (currentZoom >= minZoomFactor));
        }

        public System.Drawing.Image GetOldPhoto(Int64 patientid)
        {

            DataTable dtPatient = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Byte[] _myByte = null;
            try
            {
                if (oDB.Connect(false))
                {
                    //String strQuery = "SELECT iPhoto FROM Patient where nPatientID= " + patientid + "";
                    String strQuery = "SELECT iPhoto FROM Patient_Photo where nPatientID= " + patientid + "";
                    oDB.Retrive_Query(strQuery, out dtPatient);
                    strQuery = null;
                    if (dtPatient != null)
                    {
                        if (dtPatient.Rows.Count > 0)
                        {
                            if ((dtPatient.Rows[0]["iPhoto"] != DBNull.Value))
                            {
                                System.Drawing.Image ilogo = null;
                                // Byte[] arrPicture = (Byte[])dtPatient.Rows[0]["iPhoto"];
                                _myByte = (Byte[])dtPatient.Rows[0]["iPhoto"];
                                //



                                Point myLocation = new Point(0, 0);
                                UInt16 ZoomAndRotationFactor = (UInt16)BitConverter.ToInt16(_myByte, 0);
                                UInt16 checkMe = _isMagicNumber(ZoomAndRotationFactor);
                                int ZoomFactor = 1;
                                int RotationFactor = 0;
                                if (checkMe <= myMagicSize)
                                {
                                    ZoomAndRotationFactor = (UInt16)(ZoomAndRotationFactor - (checkMe));

                                    int MaxZoom = (int)((double)(maxZoomFactor / stepfactor) + 0.49) + 1;

                                    ZoomFactor = ZoomAndRotationFactor % MaxZoom;
                                    RotationFactor = ((ZoomAndRotationFactor - ZoomFactor) / MaxZoom);

                                    if ((ZoomFactor != 0x4D42) && _isValidZoomFactor(ZoomFactor)
                                        && _isValidRotationFactor(RotationFactor))
                                    {
                                        myLocation.X = (int)BitConverter.ToInt16(_myByte, 6);
                                        myLocation.Y = (int)BitConverter.ToInt16(_myByte, 8);
                                        Byte[] myBytes;
                                        myBytes = BitConverter.GetBytes((short)0x4D42);
                                        System.Array.Copy(myBytes, 0, _myByte, 0, 2);
                                        myBytes = BitConverter.GetBytes((short)0x0);
                                        System.Array.Copy(myBytes, 0, _myByte, 6, 2);
                                        myBytes = BitConverter.GetBytes((short)0x0);
                                        System.Array.Copy(myBytes, 0, _myByte, 8, 2);
                                        myBytes = null;
                                    }
                                    else
                                    {
                                        ZoomFactor = defaultZoom;
                                        RotationFactor = rotationdefault;

                                    }
                                }
                                else
                                {
                                    ZoomFactor = defaultZoom;
                                    RotationFactor = rotationdefault;
                                }
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(_myByte);


                                try
                                {
                                    ilogo = Image.FromStream(ms);

                                }
                                catch
                                {

                                    System.IO.MemoryStream ms1 = new System.IO.MemoryStream(_myByte);


                                    try
                                    {
                                        ilogo = Image.FromStream(ms1);

                                    }
                                    catch
                                    {
                                        ilogo = null;
                                    }


                                }
                                //---End
                                //System.IO.MemoryStream ms = new System.IO.MemoryStream(arrPicture);
                                //ilogo = System.Drawing.Image.FromStream(ms);
                                //ms.Close();
                                //if (ms != null)
                                //{
                                //    ms.Dispose();
                                //    ms = null;
                                //}

                                return ilogo;
                            }
                        }
                    }
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
                return null;
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                MessageBox.Show("Error Occured: " + Ex, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {
                //SLRAgain: Check reverse
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                _myByte = null;
            }
        }

        //public static Bitmap ConvertToGrayScale(Bitmap original)
        public  Bitmap ConvertToGrayScale(string FrontImagePath)
        {
            Bitmap original = new Bitmap(Image.FromFile(FrontImagePath));
            ColorMatrix colorMatrix = null;
            try
            {
                Bitmap newBitmap = new Bitmap(original.Width, original.Height);

                Graphics g = Graphics.FromImage(newBitmap);

                //create the grayscale ColorMatrix
                colorMatrix = new ColorMatrix(
                               new float[][]
                  {
                     new float[] {.3f, .3f, .3f, 0, 0},
                     new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {0, 0, 0, 0, 1}
                  });


                ImageAttributes attributes = new ImageAttributes();

                attributes.SetColorMatrix(colorMatrix);

                //create original image using the grayscale color matrix
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                   0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

                g.Dispose();
                attributes.Dispose();
                
                return newBitmap;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
                return null;
            }
            finally
            {
                if (original != null) { original.Dispose(); original = null; }
                colorMatrix = null;
            }
        }

        //public Bitmap ConvertToBlacknWhite(string FrontImagePath)
        //{
        //    Bitmap original = new Bitmap(Image.FromFile(FrontImagePath));
        //    try
        //    {
        //        Bitmap newBitmap = new Bitmap(original.Width, original.Height);

        //        Graphics g = Graphics.FromImage(newBitmap);

        //        //create the Black & white ColorMatrix
        //        ColorMatrix colorMatrix = new ColorMatrix(
        //                      new float[][] { 
        //        new float[] { 0.299f, 0.299f, 0.299f, 0, 0 }, 
        //        new float[] { 0.587f, 0.587f, 0.587f, 0, 0 }, 
        //        new float[] { 0.114f, 0.114f, 0.114f, 0, 0 }, 
        //        new float[] { 0,      0,      0,      1, 0 }, 
        //        new float[] { 0,      0,      0,      0, 1 } 
        //    });


        //        ImageAttributes attributes = new ImageAttributes();

        //        attributes.SetColorMatrix(colorMatrix);

        //        //create original image using the grayscale color matrix
        //        g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
        //           0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

        //        g.Dispose();
        //        return newBitmap;
        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.ToString();
        //        return null;
        //    }
        //    finally
        //    {
        //        if (original != null) { original.Dispose(); original = null; }
        //    }
        //}

        //public Bitmap ConvertToBlacknWhite(string CardimagePath)
        //{
        //    Bitmap cardimage = new Bitmap(Image.FromFile(CardimagePath));

        //    Rectangle rectangle = new Rectangle(0, 0, cardimage.Width, cardimage.Height);

        //    Bitmap blackAndWhite = new System.Drawing.Bitmap(cardimage.Width, cardimage.Height);

        //    // make an exact copy of the bitmap provided
        //    using (Graphics graphics = System.Drawing.Graphics.FromImage(blackAndWhite))
        //        graphics.DrawImage(cardimage, new System.Drawing.Rectangle(0, 0, cardimage.Width, cardimage.Height),
        //            new Rectangle(0, 0, cardimage.Width, cardimage.Height), GraphicsUnit.Pixel);

        //    // for every pixel in the rectangle region
        //    for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < cardimage.Width; xx++)
        //    {
        //        for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < cardimage.Height; yy++)
        //        {
        //            // average the red, green and blue of the pixel to get a gray value
        //            System.Drawing.Color pixel = blackAndWhite.GetPixel(xx, yy);
        //            Int32 avg = (pixel.R + pixel.G + pixel.B) / 3;

        //            blackAndWhite.SetPixel(xx, yy, System.Drawing.Color.FromArgb(0, avg, avg, avg));
        //        }
        //    }

        //    return blackAndWhite;
        //}

        public Bitmap ConvertToBlacknWhite(string CardimagePath)
        {
            Bitmap SourceImage = new Bitmap(Image.FromFile(CardimagePath));
            using (Graphics gr = Graphics.FromImage(SourceImage)) 
            {
                var BW_matrix = new float[][] { 
                new float[] { 0.299f, 0.299f, 0.299f, 0, 0 }, 
                new float[] { 0.587f, 0.587f, 0.587f, 0, 0 }, 
                new float[] { 0.114f, 0.114f, 0.114f, 0, 0 }, 
                new float[] { 0,      0,      0,      1, 0 }, 
                new float[] { 0,      0,      0,      0, 1 } 
            };

                ImageAttributes imgAttr = new ImageAttributes();
                imgAttr.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix(BW_matrix));
                imgAttr.SetThreshold(0.5f);                 
                var rc = new Rectangle(0, 0, SourceImage.Width, SourceImage.Height);
                gr.DrawImage(SourceImage, rc, 0, 0, SourceImage.Width, SourceImage.Height, GraphicsUnit.Pixel, imgAttr);
                imgAttr.Dispose();
            }
            return SourceImage;
        }


    }
}
