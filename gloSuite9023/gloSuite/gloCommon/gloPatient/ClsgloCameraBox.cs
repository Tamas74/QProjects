using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using gloSettings;
using gloStreamWebCamera.Camera;

namespace gloPictureBox
{

    public class gloCameraBox : gloPictureBox, IDisposable
    {

        private static bool disposedValue = false;
        public gloCameraBox() : base() 
        {
            
            
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
            disposedValue = false;
        }
        

        public void Disposers()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            
            if (!disposedValue)
            {
                if (disposing)
                {
                    closeCam();
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                }
            }
            disposedValue = true;
            base.Dispose(disposing);
        }
       

        private CameraFrameSource _frameSource=null;
        private string sCameraVersion="";
        public string sCameraName = "";
        public int iFPS = 15;
        public bool iRunning=false;
        
        private string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
       
        public bool initCam()
        {
            try
            {
                
                Camera c = null;
               
                foreach (Camera cam in CameraService.AvailableCameras)
                {
                   
                    c = cam;
                    if (c.Name == sCameraName)
                    {
                        break;
                    }
                }
                if (c != null)
                {
                    closeCam();
                    sCameraName = c.Name;
                    _frameSource = new CameraFrameSource(c);
                  
                    _frameSource.Camera.CaptureWidth = 640;
                    _frameSource.Camera.CaptureHeight = 480;
                    _frameSource.Camera.Fps = iFPS;
                    _frameSource.NewFrame += OnImageCaptured;
                    underCapture = true;
                    //   pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);
                    _frameSource.StartFrameCapture();
                    iRunning = true;
                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                // comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message+"\r\n Webcam is not running.  Please check cables and confirm that power is on.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show(ex.Message);
            }
            iRunning = false;
            return false;
        }
        private bool underPainting = false;
        private bool underCapture = false;
        public bool OtherOperations
        {
            get
            {
                return underPainting;
            }
            set
            {
                underPainting = value;
                if (underPainting)
                {
                    if (_frameSource != null)
                    {
                        try
                        {
                            if (underCapture)
                            {
                                _frameSource.NewFrame -= OnImageCaptured;
                                underCapture = false;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    if (_frameSource != null)
                    {
                        try
                        {
                            if (!underCapture)
                            {
                                _frameSource.NewFrame += OnImageCaptured;
                                underCapture = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        public void OnImageCaptured(gloStreamWebCamera.Contracts.IFrameSource frameSource, gloStreamWebCamera.Contracts.Frame frame, double fps)
        {
            if (!underPainting)
            {
                _Image = frame.Image;
                underPainting = true;
                Invalidate();
                underPainting = false;
                
            }
        }

        public void gloWebCameraClipingsGet()
        {
           
            double ImageWidth = 640;
            double ImageHeight = 480;

            zoomfactor = 1.0;
            
            rotationfactor = 0;
            
            rotationWidth = (int) ImageWidth;
            rotationHeight = (int) ImageHeight;
            myDrawingRect = myPictRect;
            gloBox_SetCenter();
            gloBox_Rotate(rotationfactor, myDrawingRect.Location);
            gloBox_DeflateRect();
            myOrgLocation = myDrawingRect.Location;


          
            



            string sCameraLocation = "";
            
            double sCameraRotation = 0;
            sCameraName = "";
            iFPS = 15;
            sCameraVersion = "";
            object oCameraLocation = null;
            object oCameraZoom = null;
            object oCameraVersion = null;
            object oCameraName = null;
            object oCameraFPS = null;
            object oCameraRotation = null;
            
            double myScaleX = ImageWidth / (double)myPictRect.Width;
            double myScaleY = ImageHeight / (double)myPictRect.Height;
            double myScaleZ = myScaleX;
            if (myScaleZ > myScaleY)
            {
                myScaleZ = myScaleY;
            }
            double sCameraZoom = (int)((myScaleZ / stepfactor) + 0.49);
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {
               

               
            }
            else
            {
                oCameraLocation = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraLocation);
                oCameraZoom = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraZoom);
                oCameraRotation = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraRotation);
                oCameraName = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraName);
                oCameraFPS = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraFPS);
                oCameraVersion = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraVersion);
                gloRegistrySetting.CloseRegistryKey();
            }
            if (oCameraVersion != null)
            {
                sCameraVersion = oCameraVersion.ToString().ToUpper();
                if (sCameraVersion == "")
                {
                    sCameraVersion = "6X";
                }
            }
            else
            {
                sCameraVersion = "6X";
            }
            if (oCameraRotation != null)
            {
                try
                {
                    sCameraRotation = Convert.ToDouble(oCameraRotation);
                }
                catch
                {
                    sCameraRotation = 0;
                }
            }
            if (oCameraLocation != null)
            {
                sCameraLocation = oCameraLocation.ToString();
                if (sCameraLocation == "")
                {
                    sCameraLocation = "CENTER,CENTER";
                }
            }
            else
            {
                sCameraLocation = "CENTER,CENTER";
            }
            if (oCameraName != null)
            {
                sCameraName = oCameraName.ToString();
            }
            else
            {
                sCameraName = "";
            }

            if (oCameraFPS != null)
            {
                try
                {
                    iFPS = Convert.ToInt32(oCameraFPS);
                    if (iFPS <= 0)
                    {
                        iFPS = 15;
                    }
                }
                catch
                {
                   iFPS = 15;
                }
                 
            }
            else
            {
                iFPS= 15;
            }
            
            if (sCameraVersion == "4X")
            {

                myDrawingRect.Location = new Point(0, 0);
                sCameraZoom = 10;//2;

            }
            else if (sCameraVersion == "5X")
            {
                sCameraZoom = 52;//13;//35;//34;

                myDrawingRect.Location = new Point(0, 0);
            }
            else
            {

                if (oCameraZoom != null)
                {
                    try
                    {
                        sCameraZoom = Convert.ToDouble(oCameraZoom);

                    }
                    catch { sCameraZoom = 35; }
                }
                else
                {
                  
                }
            }


            //..
            if (sCameraZoom <= 0)
            {
                sCameraZoom = 35;
            }
           
            double zoomWidth = (double) pictBoxWidth *  sCameraZoom * stepfactor;
            double zoomHeight = (double) pictBoxHeight * sCameraZoom * stepfactor;
            myScaleX = zoomWidth /  ImageWidth;
            myScaleY = zoomHeight / ImageHeight;
            if (myScaleX < myScaleY)
            {
                zoomWidth =  ImageWidth * myScaleY;
            }
            else
            {
                zoomHeight = ImageHeight * myScaleX;
            }
            Zoom = (int) sCameraZoom;
            Rotation = (int) sCameraRotation;
            
            if (sCameraVersion == "6X")
            {
                int LocationX = 0;
                int LocationY = 0;
                string[] mySplitCameraLocation = Regex.Split(sCameraLocation, ",");
                if (mySplitCameraLocation.Length >= 2)
                {
                    string sCameraLocationX = mySplitCameraLocation[0];
                    string sCameraLocationY = mySplitCameraLocation[1];

                    switch (sCameraLocationX.ToUpper())
                    {
                        case "LEFT":
                            {
                                LocationX = 0;
                                break;
                            }
                        case "RIGHT":
                            {
                                LocationX = (int)zoomWidth - pictBoxWidth;

                                break;
                            }
                        case "CENTER":
                            {
                                LocationX = ((int)zoomWidth - pictBoxWidth) / 2;

                                break;
                            }
                        default:
                            {
                                LocationX = Convert.ToInt32(sCameraLocationX);
                                break;
                            }

                    }
                    switch (sCameraLocationY.ToUpper())
                    {
                        case "LEFT":
                            {
                                LocationY = 0;
                                break;
                            }
                        case "RIGHT":
                            {
                                LocationY = (int)zoomHeight - pictBoxHeight;

                                break;
                            }
                        case "CENTER":
                            {

                                LocationY = ((int)zoomHeight - pictBoxHeight) / 2;

                                break;
                            }
                        default:
                            {
                                LocationY = Convert.ToInt32(sCameraLocationY);
                                break;
                            }

                    }
                }
                myDrawingRect.Location = new Point(LocationX, LocationY);
            }//6x
            gloBox_DeflateRect();
            startLocation = myDrawingRect.Location;
           
        }
        public void closeCam()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                try
                {
                    if (underCapture)
                    {
                        _frameSource.NewFrame -= OnImageCaptured;
                        underCapture = false;
                    }
                }
                catch
                {
                }
                _frameSource.Camera.Dispose();
                _frameSource = null;
                iRunning = false;
                //  pictureBoxDisplay.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }
        
        public void gloWebCameraClipingsSet()
        {

            //saving

            if (gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoftEMR) == false)
            {
                gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftEMR);
            }
            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
            {
                return;
            }

            string myLocationString = myDrawingRect.Location.X.ToString() + "," + myDrawingRect.Location.Y.ToString();
            string myZoomString = Zoom.ToString();
            string myVersionString = sCameraVersion;
            string myRotationString = Rotation.ToString();
            string myName = sCameraName;
            string myFPS = iFPS.ToString();

            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraLocation, myLocationString);
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraZoom, myZoomString);
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraRotation, myRotationString);
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraVersion, myVersionString);
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraName, myName);
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraFPS, myFPS);
            gloRegistrySetting.CloseRegistryKey();

            //
        }
        public Point ELocation
        {

            get
            {
                return new Point(-startLocation.X + myDrawingRect.Location.X, -startLocation.Y + myDrawingRect.Location.Y);
            }
            set
            {
                startLocation = new Point(-value.X + myDrawingRect.Location.X, -value.Y + myDrawingRect.Location.Y);
            }
        }
    }
}
