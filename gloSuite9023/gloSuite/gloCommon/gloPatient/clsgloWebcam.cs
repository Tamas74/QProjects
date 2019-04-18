using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using gloSettings;
using System.Text.RegularExpressions;

namespace gloWebcam
{
    //class gloWebcam
    //{
    //    #region "Api/constants"

    //    private const int WS_CHILD = 0x40000000;
    //    private const int WS_VISIBLE = 0x10000000;
    //    private const int SWP_NOMOVE = 0x2;
    //    private const int SWP_NOZORDER = 0x4;
    //    private const int SWP_DRAWFRAME = 0x0020;
    //    private const int WM_USER = 0x400;       

    //    private const int WM_CAP_DRIVER_CONNECT = WM_USER + 10;
    //    private const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;
    //    private const int WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;
    //    private const int WM_CAP_SET_PREVIEW = WM_USER + 50;
    //    private const int WM_CAP_SET_PREVIEWRATE = WM_USER + 52;
    //    private const long WM_CAP_GET_FRAME = 1024;//1084
    //    private const long WM_CAP_COPY = 1054;
    //    private const long WM_CAP_START = WM_USER;
    //    private const long WM_CAP_STOP = (WM_CAP_START + 68);
    //    private const long WM_CAP_SEQUENCE = (WM_CAP_START + 62);
    //    private const long WM_CAP_SET_SEQUENCE_SETUP = (WM_CAP_START + 64);
    //    private const long WM_CAP_FILE_SET_CAPTURE_FILEA = (WM_CAP_START + 20);
    //    //dhruv setted the scale
    //    private const int WM_CAP_SET_SCALE = WM_USER + 53;
    //    private const short HWND_BOTTOM = 1;

       
  
    //    [DllImport("User32.dll", EntryPoint = "SendMessage")]
    //    private static extern int SendMessage(int hwnd , int wMsg, short  wParam , string  lParam );
  
    //    [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA")]
    //    private static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
    
    //    [DllImport("avicap32.dll", EntryPoint = "capGetDriverDescriptionA")]
    //    private static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);

    //    [DllImport("GDI32.DLL", EntryPoint = "StretchBlt")]
    //    private static extern bool StretchBlt( IntPtr hdcDest, int nXOriginDest,  int nYOriginDest,    int nWidthDest,   int nHeightDest, IntPtr hdcSrc,   int nXOriginSrc,   int nYOriginSrc,    int nWidthSrc,    int nHeightSrc,    int dwRop);
     
    //    [DllImport("GDI32.DLL", EntryPoint = "SetStretchBltMode")]
    //    private static extern int SetStretchBltMode(  IntPtr hdc,  int iStretchMode );

    //    //dhruv setting the windows pos
    //    [DllImport("user32.DLL", EntryPoint = "SetWindowPos")]
    //    private static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

    //    [DllImport("GDI32.DLL", EntryPoint = "BitBlt")]
    //    private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);


    //    #endregion

    //    //private string _MessageBoxCaption = "gloPM";
    //    private string _MessageBoxCaption = String.Empty;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

    //    private string iDevice;
    //    private int hHwnd;
    //    //private int lwndC;
    //    public bool iRunning;
    //    private int CamFrameRate = 15;
    //    //private int OutputHeight = (120*165/160);//140;
    //    //private int OutputWidth = (160 * 123 / 120);//120;
        
    //    //Set to original size of camera
    //    private int OutputHeight = 120;
    //    private int OutputWidth = 160;



    //    public gloWebcam()
    //    {

    //        //Added By Pramod Nair For Messagebox Caption 
    //        #region " Retrieve MessageBoxCaption from AppSettings "

    //        if (appSettings["MessageBOXCaption"] != null)
    //        {
    //            if (appSettings["MessageBOXCaption"] != "")
    //            {
    //                _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
    //            }
    //            else
    //            {
    //                _MessageBoxCaption = "gloPM";
    //            }
    //        }
    //        else
    //        { _MessageBoxCaption = "gloPM"; }

    //        #endregion
    //    }

    //    public void resetCam()
    //    {
    //        //resets the camera after setting change
    //        if (iRunning)
    //        {
    //            closeCam();
    //            if (setCam() == false)
    //            {
    //                MessageBox.Show("Errror setting/re-setting camera.  ");
    //            }
    //        }
    //    }




    //    //dhruv setted the height and width of the picture box over setwindowspos
    //    //public bool initCam(int parentH)
    //    public bool initCam(int parentH, Int32 picOutputheight, Int32 picOutputwidth)
    //    {

                   

    //        //Gets the handle and initiates camera setup
    //        if (this.iRunning == true )
    //        {
    //            MessageBox.Show("Camera is already running.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    //            return false;            
    //        }
    //        else
    //        {

    //            // hHwnd = capCreateCaptureWindowA(iDevice, WS_VISIBLE Or WS_CHILD, 0, 0, OutputWidth, CShort(OutputHeight), parentH, 0)
    //            //hHwnd = capCreateCaptureWindowA(iDevice, WS_VISIBLE | WS_CHILD,0, 0, OutputWidth, Convert.ToInt16(OutputHeight), parentH ,0);
    //            hHwnd = capCreateCaptureWindowA(iDevice, WS_VISIBLE | WS_CHILD, 0, 0, OutputWidth, Convert.ToInt16(OutputHeight), parentH, parentH);

    //            //dhruv
    //            if (Convert.ToBoolean(SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT,Convert.ToInt16(iDevice),Convert.ToString(0))))
    //            {

    //                //Set the preview scale 

    //                SendMessage(hHwnd, WM_CAP_SET_SCALE, Convert.ToInt16(true), Convert.ToString(0));
        

    //                //Set the preview rate in milliseconds 

    //                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, Convert.ToString(0));
        

    //                //Start previewing the image from the camera 

    //                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, Convert.ToInt16(true), Convert.ToString(0));
                    


    //                //Coded  for finding the aspect ratio
    //                // Resize window to fit in picturebox 
    //                double myPicWidth = picOutputwidth;
    //                double myPicHeight = picOutputheight;
    //                double myScaleX = myPicWidth / (double)OutputWidth;
    //                double myScaleY = myPicHeight / (double)OutputHeight;
    //                double myStartX = 0;
    //                double myStartY = 0;

    //                if (myScaleX < myScaleY)
    //                {
    //                    myPicWidth = (double)OutputWidth * myScaleY;
    //                    myStartX = ((double)picOutputwidth - myPicWidth) / 2;
    //                }
    //                else
    //                {
    //                    myPicHeight = (double)OutputHeight * myScaleX;
    //                    myStartY = ((double)picOutputheight - myPicHeight) / 2;
    //                }
    //                SetWindowPos(hHwnd, HWND_BOTTOM, (int)myStartX, (int)myStartY, (int)myPicWidth, (int)myPicHeight, SWP_NOZORDER);
    //                //----------------------------------------------------------------------------------

                  

    //               // //Set size to Capture window on Pat Reg form
    //               // Double _dYScaleRatio = (Double)picOutputheight / (Double)OutputHeight;
    //               // Double _dXScaleRatio = (Double)picOutputwidth / (Double)OutputWidth;
                    
    //               // //Set Height & Width according to Y scaling
    //               // OutputHeight = (int)(120 * _dYScaleRatio);
    //               // OutputWidth = (int)(160 * _dYScaleRatio);

    //               // Double _dXStartPosition = 0;
    //               // Double _dYStartPosition = 0;

    //               //_dXStartPosition = (Double)(((Double)OutputWidth - (Double)picOutputwidth)/2) * - 1;
    //               //_dYStartPosition = 0;
    //               // //

    //               //SetWindowPos(hHwnd, HWND_BOTTOM, (int)_dXStartPosition, (int)_dYStartPosition, (int)OutputWidth, (int)OutputHeight,SWP_NOZORDER);
    //               // //SetWindowPos(hHwnd, HWND_BOTTOM, (int)_dXStartPosition, 0, (int)OutputWidth, (int)OutputHeight, SWP_NOMOVE | SWP_NOZORDER);

    //                if (setCam() == false)
    //                {
    //                 MessageBox.Show("Webcam is not running.  Please check cables and confirm that power is on.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
    //                 return false;
    //                }
    //            }

    //        }
    //        return true;
    //    }
    

    //    public void setFrameRate(long iRate)
    //    {
    //        //sets the frame rate of the camera
    //        CamFrameRate = Convert.ToInt32(1000 / iRate);
    //        resetCam();
    //    }

    //    private bool setCam()
    //    {
    //        //Sets all the camera up
    //        if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT,Convert.ToInt16(iDevice), Convert.ToString(0)) == 1)
    //        {
    //            SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, Convert.ToInt16(CamFrameRate), Convert.ToString(0));
    //            SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, Convert.ToString(0));
                
    //            this.iRunning = true;
    //            return true;
    //        }
    //        else
    //        {
    //            this.iRunning = false;
    //            return false;
    //        }
    //    }
        
    //    public bool closeCam()
    //    {
    //        bool _result = false;
    //        //Closes the camera
    //        if (this.iRunning)
    //        {
    //            _result = Convert.ToBoolean(SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, 0, Convert.ToString(0)));
    //            this.iRunning = false;
    //        }
    //        return _result; 
    //    }

    //    public Bitmap copyFrame(PictureBox src, RectangleF rect)
    //    {
    //        Bitmap _result = null;
    //        if (iRunning)
    //        {
    //            Graphics srcPic = src.CreateGraphics();
   
    //            //srcBmp.Save("C:\\WEBTest.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
     

    //            double myPicWidth = rect.Width;
    //            double myPicHeight = rect.Height;
    //            double myScaleX = myPicWidth / (double)src.Width;
    //            double myScaleY = myPicHeight / (double)src.Height;


    //            if (myScaleX < myScaleY)
    //            {
    //                myPicWidth = (double)OutputWidth * myScaleY;
  
    //            }
    //            else
    //            {
    //                myPicHeight = (double)OutputHeight * myScaleX;
    //            }

    //            Bitmap srcBmp = new Bitmap(Convert.ToInt32(myPicWidth), Convert.ToInt32(myPicHeight), srcPic);
    //            Graphics srcMem = Graphics.FromImage(srcBmp);
    //            IntPtr HDC1 = srcPic.GetHdc();
    //            IntPtr HDC2 = srcMem.GetHdc();
    //            SetStretchBltMode(HDC2,4);
    //            StretchBlt(HDC2, 0, 0, Convert.ToInt32(myPicWidth), Convert.ToInt32(myPicHeight), HDC1, 0, 0, src.Width, src.Height, 13369376);

    //            //BitBlt(HDC2, 0, 0, src.Width, src.Height, HDC1, Convert.ToInt32(rect.X), Convert.ToInt32(rect.Y), 13369376);


    //            //_result = new Bitmap(srcBmp, (int)myPicWidth, (int)myPicHeight);
    //            _result = (Bitmap)srcBmp.Clone();

    //            //Clean Up 
    //            srcPic.ReleaseHdc(HDC1);
    //            srcMem.ReleaseHdc(HDC2);
    //            srcPic.Dispose();
    //            srcMem.Dispose();
    //        }
    //        else
    //        {
    //            MessageBox.Show("Camera is not running.  ");
    //        }
    //        return _result; 
    //    }

    //    public int FPS()
    //    {
    //        return Convert.ToInt32(1000 / (CamFrameRate));
    //    }
    
    //}
    public class gloWebcam : UserControl, IDisposable
    {
        public gloWebcam()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
           ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor,
           true);
            #region " Retrieve MessageBoxCaption from AppSettings "

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

            #endregion

            //InitializeComponent();
            //  myCam = new gloWebcam();
            Opacity = 0;

            this.MouseUp += new MouseEventHandler(gloTransparent_MouseUp);
            this.MouseHover += new EventHandler(gloTransparent_MouseHover);
            this.MouseMove += new MouseEventHandler(gloTransparent_MouseMove);
            this.MouseDown += new MouseEventHandler(gloTransparent_MouseDown);


        }
        //public gloWebcam(int MyOpacity)
        //{

        //    #region " Retrieve MessageBoxCaption from AppSettings "

        //    if (appSettings["MessageBOXCaption"] != null)
        //    {
        //        if (appSettings["MessageBOXCaption"] != "")
        //        {
        //            _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
        //        }
        //        else
        //        {
        //            _MessageBoxCaption = "gloPM";
        //        }
        //    }
        //    else
        //    { _MessageBoxCaption = "gloPM"; }

        //    #endregion

        //    //InitializeComponent();
        //    //  myCam = new gloWebcam();
        //    Opacity = MyOpacity;

        //    this.MouseUp += new MouseEventHandler(gloTransparent_MouseUp);
        //    this.MouseHover += new EventHandler(gloTransparent_MouseHover);
        //    this.MouseMove += new MouseEventHandler(gloTransparent_MouseMove);
        //    this.MouseDown += new MouseEventHandler(gloTransparent_MouseDown);


        //}
        //public class gloWebcam

        #region "Api/constants"

        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_DRAWFRAME = 0x0020;
        private const uint WM_USER = 0x400;
        private const uint WM_DESTROY = 0x2;
        private const uint WM_CAP_DRIVER_CONNECT = WM_USER + 10;
        private const uint WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;
        private const uint WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;
        private const uint WM_CAP_SET_PREVIEW = WM_USER + 50;
        private const uint WM_CAP_SET_PREVIEWRATE = WM_USER + 52;
        private const uint WM_CAP_GET_FRAME = WM_USER + 60;
        private const uint WM_CAP_COPY = WM_USER + 30;
        private const long WM_CAP_START = WM_USER;
        private const long WM_CAP_STOP = (WM_CAP_START + 68);
        private const long WM_CAP_SEQUENCE = (WM_CAP_START + 62);
        private const long WM_CAP_SET_SEQUENCE_SETUP = (WM_CAP_START + 64);
        private const long WM_CAP_FILE_SET_CAPTURE_FILEA = (WM_CAP_START + 20);
        //dhruv setted the scale
        private const uint WM_CAP_SET_SCALE = WM_USER + 53;
        // private const short HWND_BOTTOM = 1;



        //[DllImport("User32.dll", EntryPoint = "SendMessage")]
        //private static extern int SendMessage(int hwnd, int wMsg, short wParam, string lParam);
        //Changed done for 64 and 32 bit compatibality
        //[DllImport("user32.dll", EntryPoint = "SendMessage")]
        //static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);
       
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, uint wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);
     
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        protected static extern int PostMessage(int hwnd, uint wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);
 

        //[DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA")]
        //private static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        //[DllImport("avicap32.dll", EntryPoint = "capGetDriverDescriptionA")]
        //private static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);
        [DllImport("avicap32.dll", EntryPoint = "capGetDriverDescriptionA")]
        protected static extern bool capGetDriverDescriptionA(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
           int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);
        //This function enables create a  window child with so that you can display it in a picturebox for example
        [DllImport("avicap32.dll" ,EntryPoint = "capCreateCaptureWindowA")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);
        //This function enables set changes to the size, position, and Z order of a child window

        [DllImport("GDI32.DLL", EntryPoint = "StretchBlt")]
        private static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, int dwRop);

        [DllImport("GDI32.DLL", EntryPoint = "SetStretchBltMode")]
        private static extern int SetStretchBltMode(IntPtr hdc, int iStretchMode);


        //Start/ SetWindow Position
        //dhruv setting the windows pos
        //[DllImport("user32.DLL", EntryPoint = "SetWindowPos")]
        //private static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        //before setting the window position set the flags required.
        [Flags()]
        private enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            SynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
            /// contents of the client area are saved and copied back into the client area after the window is sized or 
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
            /// window uncovered as a result of the window being moved. When this flag is set, the application must 
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }
        //[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);// SetWindowPosFlags uFlags);
        //This function enables set changes to the size, position, and Z order of a child window
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [DllImport("user32.dll",  EntryPoint = "DestroyWindow")]
        protected static extern bool DestroyWindow(int hwnd);

        //static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        //static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        //static readonly IntPtr HWND_TOP = new IntPtr(0);
       // static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        //End/ SetWindow Position
        private short HWND_BOTTOM = 1;

        [DllImport("GDI32.DLL", EntryPoint = "BitBlt")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);


        #endregion


        private string _MessageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string iDevice=null;
        private int hHwnd;
        private static int ParentHeight = 0;
        private static int ParentWidth = 0;
        public bool iRunning;
        private int CamFrameRate = 66;
        //Set to original size of camera
        private int OutputHeight = 120;
        private int OutputWidth = 160;
        string sCameraVersion = "";
      //  private static bool disposedValue = false;
        int myDevice = 0;


        public void resetCam()
        {
            //resets the camera after setting change
            if (iRunning)
            {
                closeCam();
                if (setCam() == false)
                {
                    MessageBox.Show("Errror setting/re-setting camera.  ");
                }
            }
        }

        public void ZoomCam(double ZoomMe)
        {
            if (this.iRunning == true)
            {
                if (ZoomMe == 0.0)
                {
                    MessageBox.Show("Camera is already running.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    Point mouseLocation = ELocation;


                    double myStartX = ((myDrawingRect.Location.X - startLocation.X)) + (double)startLocation.X * zoomfactor / ZoomMe;
                    double myStartY = ((myDrawingRect.Location.Y - startLocation.Y)) + (double)startLocation.Y * zoomfactor / ZoomMe;
                    zoomfactor = ZoomMe;
                    double myPicWidth = (zoomWidth / zoomfactor);
                    double myPicHeight = (zoomHeight / zoomfactor);
                    if (myStartX > 0) myStartX = 0;
                    if (myStartY > 0) myStartY = 0;
                    if (myStartX < (-myPicWidth + ParentWidth)) myStartX = (-myPicWidth + ParentWidth);
                    if (myStartY < (-myPicHeight + ParentHeight)) myStartY = (-myPicHeight + ParentHeight);
                    myDrawingRect = new Rectangle((int)myStartX, (int)myStartY, (int)myPicWidth, (int)myPicHeight);
                    SetWindowPos(hHwnd, HWND_BOTTOM, (int)myStartX, (int)myStartY, (int)myPicWidth, (int)myPicHeight, (int)SetWindowPosFlags.IgnoreZOrder);
                    ELocation = mouseLocation;
                }
            }


        }

        public bool initCam(int parentH, Int32 picOutputheight, Int32 picOutputwidth, double ZoomMe)
        {



            //Gets the handle and initiates camera setup
            if (this.iRunning == true)
            {
                if (ZoomMe == 0)
                {
                    MessageBox.Show("Camera is already running.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    ZoomCam(ZoomMe);
                }
            }
            else
            {

                if (ZoomMe == 0)
                {

                    string sCameraLocation = "";
                    double sCameraZoom = 35f;

                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                    {
                        return false;
                    }
                    object oCameraLocation = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraLocation);
                    object oCameraZoom = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraZoom);
                    object oCameraVersion = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gCameraVersion);
                    gloRegistrySetting.CloseRegistryKey();
                    if (oCameraVersion != null)
                    {
                        sCameraVersion = oCameraVersion.ToString().ToUpper();
                    }
                    else
                    {
                        sCameraVersion = "6X";
                    }
                    if (oCameraLocation != null)
                    {
                        sCameraLocation = oCameraLocation.ToString();
                    }
                    else
                    {
                        sCameraLocation = "CENTER,CENTER";
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
                            sCameraZoom = 35;
                        }
                    }

                    //..

                    zoomfactor = (double)sCameraZoom * stepfactor;
                    ParentHeight = picOutputheight;
                    ParentWidth = picOutputwidth;
                    zoomWidth = ParentWidth * 3.5;
                    zoomHeight = ParentHeight * 3.5;
                    double myScaleX = zoomWidth / (double)OutputWidth;
                    double myScaleY = zoomHeight / (double)OutputHeight;
                    if (myScaleX < myScaleY)
                    {
                        zoomWidth = (double)OutputWidth * myScaleY;
                    }
                    else
                    {
                        zoomHeight = (double)OutputHeight * myScaleX;
                    }
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
                                        LocationX = (int)zoomWidth - ParentWidth;

                                        break;
                                    }
                                case "CENTER":
                                    {
                                        LocationX = ((int)zoomWidth - ParentWidth) / 2;

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
                                        LocationY = (int)zoomHeight - ParentHeight;

                                        break;
                                    }
                                case "CENTER":
                                    {

                                        LocationY = ((int)zoomHeight - ParentHeight) / 2;

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

                }
                else
                {
                    zoomfactor = ZoomMe;
                }

                string dDevice = "".PadLeft(1000);
                string dVersion = dDevice;
                myDevice = -1; 
                for (short deviceIndex = 0; deviceIndex <= 9; deviceIndex++)
                {
                    if (capGetDriverDescriptionA(deviceIndex, ref dDevice, 999, ref dVersion, 999))
                    {
                        iDevice = dDevice;
                        myDevice = deviceIndex;
                        break;
                    }
                }
                hHwnd = capCreateCaptureWindowA(ref iDevice, WS_VISIBLE | WS_CHILD, 0, 0, OutputWidth, OutputHeight, parentH, 0);

                if (myDevice == -1)
                {
                    try
                    {
                        myDevice = Convert.ToInt32(iDevice);
                    }
                    catch
                    {
                        try
                        {
                            myDevice = Convert.ToInt32(null);
                        }
                        catch
                        {
                            myDevice = 0;
                        }
                    }
                }
                //if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, myDevice, 0) != 0)
                //{

                //    //Set the preview scale 
                //    SendMessage(hHwnd, WM_CAP_SET_SCALE, -1, 0);
                //    //Set the preview rate in milliseconds 
                //    SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);
                //    //Start previewing the image from the camera 
                //    SendMessage(hHwnd, WM_CAP_SET_PREVIEW, -1, 0);
                    if (setCam())
                    {
                        ZoomCam(zoomfactor);
                    }
                    else
                    {
                        MessageBox.Show("Webcam is not running.  Please check cables and confirm that power is on.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                //}
                //else
                //{
                //    DestroyWindow(hHwnd);
                //    MessageBox.Show("Webcam is not running.  Please check cables and confirm that power is on.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    this.iRunning = false;
                //    return false;

                //}

            }
            return true;
        }


        //public void setFrameRate(long iRate)
        //{
        //    //sets the frame rate of the camera
        //    CamFrameRate = Convert.ToInt32(1000 / iRate);
        //    resetCam();
        //}

        private bool setCam()
        {
            //Sets all the camera up
            
            if ((SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, myDevice, 0)) != 0)
            {
                //Set the preview scale 
                SendMessage(hHwnd, WM_CAP_SET_SCALE, -1, 0);
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE,  CamFrameRate ,  0 );
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, -1, 0);

                this.iRunning = true;
                return true;
            }
            else
            {
                //DestroyWindow(hHwnd);
                PostMessage(hHwnd, WM_DESTROY, 0, 0); 
                this.iRunning = false;
                return false;
            }
        }


        public bool closeCam()
        {
            bool _result = false;
            //Closes the camera
            if (this.iRunning)
            {
                //int myDevice = 0;
                //try
                //{
                //    myDevice = Convert.ToInt32(iDevice);
                //}
                //catch
                //{
                //    try
                //    {
                //        myDevice = Convert.ToInt32(null);
                //    }
                //    catch
                //    {
                //        myDevice = 0;
                //    }
                //}
                //SLR:SendMessage Hangs!!
                _result = PostMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, myDevice, 0) != 0;
                
                 //   DestroyWindow(hHwnd);
                PostMessage(hHwnd, WM_DESTROY, 0, 0);

                    this.iRunning = false;

               
            }
            return _result;
        }

        public Bitmap copyFrame(PictureBox src, RectangleF rect)
        {
            Bitmap _result = null;
            if (iRunning)
            {
                //
                SendMessage(hHwnd, WM_CAP_GET_FRAME, 0, 0);
                // copy the frame to the clipboard
                SendMessage(hHwnd, WM_CAP_COPY,  0,  0 );
                // paste the frame into the event args image
                // get from the clipboard
                System.Drawing.Bitmap webCamImg = null;
                try
                {
                    IDataObject tempObj = Clipboard.GetDataObject();
                    webCamImg = (System.Drawing.Bitmap)tempObj.GetData(System.Windows.Forms.DataFormats.Bitmap);
                }
                catch //(Exception Ex)
                {

                }
                //
                if (webCamImg == null)
                {

                    Graphics srcPic = src.CreateGraphics();
                    double myPicWidth = rect.Width;
                    double myPicHeight = rect.Height;
                    double myScaleX = myPicWidth / (double)src.Width;
                    double myScaleY = myPicHeight / (double)src.Height;


                    if (myScaleX < myScaleY)
                    {
                        myPicHeight = (double)OutputHeight * myScaleX;

                    }
                    else
                    {

                        myPicWidth = (double)OutputWidth * myScaleY;
                    }

                    Bitmap srcBmp = new Bitmap(Convert.ToInt32(myPicWidth), Convert.ToInt32(myPicHeight), srcPic);
                    Graphics srcMem = Graphics.FromImage(srcBmp);
                    IntPtr HDC1 = srcPic.GetHdc();
                    IntPtr HDC2 = srcMem.GetHdc();
                    SetStretchBltMode(HDC2, 4);
                    StretchBlt(HDC2, 0, 0, Convert.ToInt32(myPicWidth), Convert.ToInt32(myPicHeight), HDC1, 0, 0, src.Width, src.Height, 13369376);

                    _result = (Bitmap)srcBmp.Clone();

                    //Clean Up 
                    srcPic.ReleaseHdc(HDC1);
                    srcMem.ReleaseHdc(HDC2);
                    srcPic.Dispose();
                    srcMem.Dispose();
                    srcBmp.Dispose();
                }
                else
                {
                    _result = (Bitmap)webCamImg.Clone();
                    webCamImg.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Camera is not running.  ");
            }
            return _result;
        }

        //public int FPS()
        //{
        //    return Convert.ToInt32(1000 / (CamFrameRate));
        //}




        ///
        static readonly int myTrackMax = 44;
        private static double stepfactor = 0.1;
        private static int defaultZoom = (int)((double)1.0 / stepfactor);
        static readonly double minZoomFactor = 0.1;//0.76;//0.35;//0.1;
        static readonly double maxZoomFactor = 8.9;//1.3;//8.9;
        private double zoomfactor = 1.0;
        private double zoomWidth = 0;
        private double zoomHeight = 0;

        public int Zoom
        {
            get
            {

                return (int)((double)(zoomfactor / stepfactor) + 0.49);
            }
            set
            {
                ZoomCam((double)value * stepfactor);
            }
        }

        public void SetZoomFromTrackbar(System.Windows.Forms.TrackBar myInternalTrackBar)
        {
            if (myInternalTrackBar == null)
            {
                return;
            }
            double defaultZoomFactor = ((double)defaultZoom * stepfactor);
            double numeratior = defaultZoomFactor * myTrackMax * (maxZoomFactor - minZoomFactor) + (2 * (minZoomFactor * maxZoomFactor) - defaultZoomFactor * (maxZoomFactor + minZoomFactor)) * (double)myInternalTrackBar.Value;
            double denominator = myTrackMax * (maxZoomFactor - minZoomFactor) + (minZoomFactor + maxZoomFactor - 2 * defaultZoomFactor) * (double)myInternalTrackBar.Value;
            Zoom = (int)((numeratior / denominator) / stepfactor + 0.49);





        }
        public void GetZoomFromTrackbar(ref System.Windows.Forms.TrackBar myInternalTrackBar)
        {
            if (myInternalTrackBar == null)
            {
                return;
            }


            double defaultZoomFactor = ((double)defaultZoom * stepfactor);
            double numeratior = (defaultZoomFactor - zoomfactor) * myTrackMax * (maxZoomFactor - minZoomFactor);
            double denominator = ((minZoomFactor + maxZoomFactor - 2 * defaultZoomFactor) * zoomfactor) - 2 * (minZoomFactor * maxZoomFactor) + defaultZoomFactor * (maxZoomFactor + minZoomFactor);
            myInternalTrackBar.Value = (int)((numeratior / denominator) + 0.49);





        }

        bool draggingPicture = false; //Tells us if our image has been clicked on.
        Point startLocation = new Point(0, 0); //Keep initial click for accurate panning.
        private Rectangle myDrawingRect;



        void gloBox_DeflateRect()
        {
            int newX = myDrawingRect.Location.X;
            int newY = myDrawingRect.Location.Y;
            if (newX > 0) newX = 0;
            if (newY > 0) newY = 0;
            if (newX < (-myDrawingRect.Width + ParentWidth)) newX = (int)(-myDrawingRect.Width + ParentWidth);
            if (newY < (-myDrawingRect.Height + ParentHeight)) newY = (int)(-myDrawingRect.Height + ParentHeight);
            myDrawingRect.Location = new Point(newX, newY);

            SetWindowPos(hHwnd, HWND_BOTTOM, (int)newX, (int)newY, (int)myDrawingRect.Width, (int)myDrawingRect.Height, (int)SetWindowPosFlags.IgnoreZOrder);
        }
        //


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;//WS_EX_TRANSPARENT
                return cp;
            }
        }


        private int opacity;

        public int Opacity
        {
            get { return opacity; }
            set
            {
                opacity = value;
                this.InvalidateEx();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Color bk = Color.FromArgb(Opacity, this.BackColor);
            using (SolidBrush sb = new SolidBrush(bk))
            {
                e.Graphics.FillRectangle(sb, e.ClipRectangle);
            }

        }

        protected void InvalidateEx()
        {
            if (Parent == null)
                return;
            Rectangle rc = new Rectangle(this.Location, this.Size);
            Parent.Invalidate(rc, true);
        }

        void gloTransparent_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
            draggingPicture = false;


            startLocation = new Point(-e.Location.X + myDrawingRect.Location.X, -e.Location.Y + myDrawingRect.Location.Y);


        }
        void gloTransparent_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;



        }
        void gloTransparent_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingPicture)
            {

                myDrawingRect.Location = new Point(startLocation.X + e.Location.X, startLocation.Y + e.Location.Y);//new Point(startLocation.X - e.Location.X, startLocation.Y - e.Location.Y);
                gloBox_DeflateRect();

            }
        }
        void gloTransparent_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            if (e.Button == MouseButtons.Left)
            {
                draggingPicture = true;
                //offset new point by original one so we know where in the image we are.

                startLocation = new Point(-e.Location.X + myDrawingRect.Location.X, -e.Location.Y + myDrawingRect.Location.Y);


            }
        }
        public Point PLocation
        {
            get
            {
                return (new Point((int)((double)-myDrawingRect.Location.X * zoomfactor + 0.49), (int)((double)-myDrawingRect.Location.Y * zoomfactor + 0.49)));
            }
            set
            {
                myDrawingRect.Location = new Point((int)((double)-value.X / zoomfactor + 0.49), (int)((double)-value.Y / zoomfactor + 0.49));
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
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraLocation, myLocationString);
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraZoom, myZoomString);
            gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gCameraVersion, myVersionString);
            gloRegistrySetting.CloseRegistryKey();

            //
        }
        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void DisposingPrivateObject()
        {
            this.MouseDown -= new MouseEventHandler(gloTransparent_MouseDown);
            this.MouseMove -= new MouseEventHandler(gloTransparent_MouseMove);
            this.MouseUp -= new MouseEventHandler(gloTransparent_MouseUp);
            this.MouseHover -= new EventHandler(gloTransparent_MouseHover);
            closeCam();
        }
        private static bool disposedValue = false;
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposingPrivateObject();
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                }
                base.Dispose(disposing);
            }
            disposedValue = true;

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
