using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using gloSettings;
using System.Text.RegularExpressions;
namespace gloPictureBox
{
    public class gloPictureEventArgs : EventArgs
    {
        public int value;
    }
    public class gloPictureBox : UserControl, IDisposable
    {

        private SolidBrush myBrush = null;
        private Pen outlinePictureBox = null;
        protected Rectangle myDrawingRect;
        protected Rectangle myPictRect;
        protected double zoomfactor = 1.0;
        protected static double stepfactor = 0.1;
        private static int defaultZoom = (int)((double)1.0 / stepfactor);
        public delegate void ZoomChanged(object sender, gloPictureEventArgs e);
        public event ZoomChanged OnZoomChanged;
        public delegate void RotationChanged(object sender, gloPictureEventArgs e);
        public event RotationChanged OnRotationChanged;
        //
        static readonly double minZoomFactor = 0.1;
        static readonly double maxZoomFactor = 8.9;
        //

        //Start/Rotation
        protected double rotationfactor = 0;
        private static double rotationstepfactor = 1.0;
        private static int rotationdefault = 0;
        private bool rotateMe = false;
        private Point[] rotationPoints = null;
        protected int rotationWidth = 0;
        protected int rotationHeight = 0;
        //End/

        //start/Newcode
        private Pen selectionPen = null;
        private Rectangle selectionRectangle;
        private bool selectMe = false;
        //End/newCode

        //private  int MyZoomFactor = 0;
        private bool zoomMe = false;
        protected Point myOrgLocation;
        //Added for Bug #77539: 00000115 : Patient Setup 
        private bool _ispicPAPhotomodified = false;
        protected  Int16 pictBoxWidth = 123;
        protected Int16 pictBoxHeight = 137;
        private bool _isMovable = false;

        private byte[] _myByte;
        bool draggingPicture = false; //Tells us if our image has been clicked on.

      protected  Point startLocation = new Point(0, 0); //Keep initial click for accurate panning.
        Point startMouseLocation = new Point(0, 0);

  
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
        bool amIFromDefaultImage = false; //This variable is set for proper image formation on the dashbord
        public byte[] byteImage
        {
            get
            {
                if (_Image == null)
                {
                    return null;
                }
                // Image MyImage = Image;
                bool isSaved = false;
                _myByte = null;//
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {

                    try
                    {
                        _Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        isSaved = true;
                    }
                    catch
                    {
                        isSaved = false;

                        try
                        {
                            Bitmap newBmp = new Bitmap(_Image);
                            if (newBmp != null)
                            {
                                newBmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                                newBmp.Dispose();
                                isSaved = true;
                            }
                        }
                        catch
                        {
                            isSaved = false;
                        }
                    }


                    if (isSaved == true)
                    {

                        // ms.Close(); ''Used 2 times 

                        Byte[] arrByteImage = ms.ToArray();//ms.GetBuffer();
                        int ZoomFactor = Zoom;
                        int RotationFactor = Rotation;
                        Point myLocation = PLocation;

                        if ((ZoomFactor != defaultZoom) || (myLocation.X != 0) || (myLocation.Y != 0)
                           || (RotationFactor != rotationdefault))
                        {
                            int MaxZoom = (int)((double)(maxZoomFactor / stepfactor) + 0.49) + 1;
                            UInt16 ZoomAndRotation = (UInt16)(RotationFactor * MaxZoom + ZoomFactor);
                            UInt16 checkMe = _isMagicNumber(ZoomAndRotation);
                            if (checkMe <= myMagicSize)
                            {
                                ZoomAndRotation = (UInt16)(ZoomAndRotation + checkMe);
                            }
                            else
                            {
                                ZoomAndRotation = (UInt16)(ZoomAndRotation + (checkMe - myMagicSize));
                            }

                            Byte[] myBytes;

                            myBytes = BitConverter.GetBytes((short)ZoomAndRotation);
                            System.Array.Copy(myBytes, 0, arrByteImage, 0, 2);

                            myBytes = BitConverter.GetBytes((short)(myLocation.X));
                            System.Array.Copy(myBytes, 0, arrByteImage, 6, 2);

                            myBytes = BitConverter.GetBytes((short)myLocation.Y);
                            System.Array.Copy(myBytes, 0, arrByteImage, 8, 2);


                        }
                        ms.Close();


                        _myByte = arrByteImage;
                    }
                    else
                    {
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        _myByte = enc.GetBytes(_Image.ToString());
                    }
                }//using
                return _myByte;
            }
            set
            {
                if (value != null)
                {
                    _myByte = (byte[])value.Clone();

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
                        }
                        else
                        {
                            ZoomFactor = defaultZoom;
                            RotationFactor = rotationdefault;
                            startLocation = myDrawingRect.Location;  //set for the setting the zoom value to '0' th position.
                        }
                    }
                    else
                    {
                        ZoomFactor = defaultZoom;
                        RotationFactor = rotationdefault;
                        startLocation = myDrawingRect.Location;//set for the setting the zoom value to '0' th position.
                    }
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(_myByte);

                   
                    try
                    {
                        Image = Image.FromStream(ms);
                         amIFromDefaultImage = ((ZoomFactor == defaultZoom) && (RotationFactor == rotationdefault) && (myLocation.X == 0) && (myLocation.Y == 0));
                        //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures Begin
                        if (amIFromDefaultImage)
                        {
                            if (adjustgloPictureZoom(Image, ref ZoomFactor))
                            {

                                if (!_isValidZoomFactor(ZoomFactor))
                                {
                                    Image myImage = (Image)Image.Clone();
                                    //Problem No: 00000361
                                    //Pass false as not copy from clipboard to implement webcam functionality in TS
                                    AspectRatio(myImage,false);
                                    myImage.Dispose();
                                    ZoomFactor = Zoom;
                                }
                            }
                        }
                        //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures End
                        Zoom = ZoomFactor;
                        Rotation = RotationFactor;
                        PLocation = myLocation;
                            
                    }
                    catch
                    {
                        _myByte = (byte[])value.Clone();
                        System.IO.MemoryStream ms1 = new System.IO.MemoryStream(_myByte);


                        try
                        {
                            Image = Image.FromStream(ms1);
                            //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures Begin
                            if ((ZoomFactor == defaultZoom) && (RotationFactor == rotationdefault) && (myLocation.X == 0) && (myLocation.Y == 0))
                            {
                                if (adjustgloPictureZoom(Image, ref ZoomFactor))
                                {

                                    if (!_isValidZoomFactor(ZoomFactor))
                                    {
                                        Image myImage = (Image)Image.Clone();
                                        //Problem No: 00000361
                                        //Pass false as not copy from clipboard to implement webcam functionality in TS
                                        AspectRatio(myImage, false);
                                        myImage.Dispose();
                                        ZoomFactor = Zoom;
                                    }
                                }
                            }
                            //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures End
                            Zoom = ZoomFactor;
                            Rotation = RotationFactor;
                            PLocation = myLocation;
                        }
                        catch
                        {
                            Image = null;
                        }


                    }
                    gloBox_DeflateRect();
                    startLocation = myDrawingRect.Location;
                }
                else
                {
                    Image = null;
                }

            }
        }

        

        //Start :Setted from gloPatientDemoGraphicControl: Set the PictureBox height and width
        public Int16 PictBoxHeight
        {
            get { return pictBoxHeight; }
            set { pictBoxHeight = value;
            myPictRect = new Rectangle(0, 0, pictBoxWidth, PictBoxHeight);
            myDrawingRect = myPictRect;
            }
        }
        public Int16 PictBoxWidth
        {
            get { return pictBoxWidth; }
            set { pictBoxWidth = value;
            myPictRect = new Rectangle(0, 0, pictBoxWidth, PictBoxHeight);
            myDrawingRect = myPictRect;
            }
        }
        public bool IsMovable
        {
            get { return _isMovable; }
            set { _isMovable = value; }
        }
        //Added for Bug #77539: 00000115 : Patient Setup
        public bool IsPAPhotomodified
        {
            get { return _ispicPAPhotomodified; }
            set { _ispicPAPhotomodified = value; }
        }

        //End :Setted from gloPatientDemoGraphicControl: Set the PictureBox height and width
        private static bool disposedValue = false;
        public gloPictureBox()
        {
            //_isMovable = true;
            //Set up double buffering and a little extra.
            _Image = null;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor,
            true);
            //DrawRect = ClientRectangle;
            //
            myPictRect = new Rectangle(0, 0, pictBoxWidth, PictBoxHeight);
            myDrawingRect = myPictRect;
            //Brush + Pen
            myBrush = new SolidBrush(Color.FromArgb(207, 224, 248));
            outlinePictureBox = new Pen(Color.Black);

            //start/Newcode
            selectionPen = new Pen(Color.FromArgb(207, 224, 248));
            //End/Newcode

            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1, 1);

            //Subscribe to our event handlers.
            this.MouseDown += new MouseEventHandler(gloBox_MouseDown);
            this.MouseMove += new MouseEventHandler(gloBox_MouseMove);
            this.MouseUp += new MouseEventHandler(gloBox_MouseUp);
            this.MouseHover += new EventHandler(gloBox_MouseHover);
            this.MouseWheel += new MouseEventHandler(gloBox_MouseWheel);
            //Start/Rotation
            //this.MouseDoubleClick += new MouseEventHandler(gloBox_MouseDoubleClick);
            //End/Rotation
            disposedValue = false;
        }
        //static bool isCameraPicture = false;
        //static string sCameraVersion = "6X";
        public void gloWebCameraClipingsGet(gloPictureBox myPictureBox)
        {
            _Image = (Image) myPictureBox._Image.Clone();
            zoomfactor = 1.0;
            zoomMe = false;
            rotationfactor = 0;
            //isCameraPicture = false;
            myDrawingRect = myPictureBox.myPictRect;

            rotationWidth = ImageWidthHere;
            rotationHeight = ImageHeightHere;
            myDrawingRect = myPictureBox.myPictRect;
            gloBox_SetCenter();
            gloBox_Rotate(rotationfactor, myDrawingRect.Location);
            gloBox_DeflateRect();


            myOrgLocation = myDrawingRect.Location;
            //Registry settings
            Zoom = myPictureBox.Zoom;
            Rotation = myPictureBox.Rotation;
           
            PLocation = myPictureBox.PLocation;
           // startLocation = myPictureBox.startLocation;
          
       
            gloBox_DeflateRect();
            startLocation = myDrawingRect.Location;
            startMouseLocation = myPictureBox.startMouseLocation;
           Invalidate();
           
        }

         
        public void gloBox_SetCenter()
        {

            
                int iX = (ImageWidthHere - myPictRect.Width) / 2;
                int iY = (ImageHeightHere - myPictRect.Height) / 2;
                myDrawingRect.X = (iX < 0 ? 0 : iX);
                myDrawingRect.Y = (iY < 0 ? 0 : iY);
            
        }


        void gloBox_MouseDown(object sender, MouseEventArgs e)
        {

            if ((_Image != null) && (_isMovable == true))
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        rotateMe = !rotateMe;
                        selectMe = false;
                    }
                    else
                    {
                        if (Control.ModifierKeys == Keys.Control)
                        {
                            startLocation = new Point((int)((double)e.Location.X * zoomfactor + 0.49) + myDrawingRect.Location.X, (int)((double)e.Location.Y * zoomfactor + 0.49) + myDrawingRect.Location.Y);
                            startMouseLocation = new Point(e.Location.X, e.Location.Y);
                            startLocation = gloBox_getOrgLocation(startLocation);
                            rotationfactor += 90.0;
                            if (rotationfactor >= 360) rotationfactor -= 360;
                            rotationfactor = (double)((int)((int)(rotationfactor) / 90) * 90);
                            gloBox_Rotate(rotationfactor, startLocation);
                            if (OnRotationChanged != null)
                            {
                                gloPictureEventArgs ge = new gloPictureEventArgs();
                                ge.value = Rotation;
                                OnRotationChanged(this, ge);
                                ge = null;
                               
                            }
                        }
                        else
                        {



                            draggingPicture = true;
                            //offset new point by original one so we know where in the image we are.

                            if (rotateMe)
                            {
                                startLocation = new Point((int)((double)e.Location.X * zoomfactor + 0.49) + myDrawingRect.Location.X, (int)((double)e.Location.Y * zoomfactor + 0.49) + myDrawingRect.Location.Y);
                                startMouseLocation = new Point(e.Location.X, e.Location.Y);//Newly added
                                startLocation = gloBox_getOrgLocation(startLocation);
                                Cursor = Cursors.SizeAll;

                            }
                            else
                            {
                                //Start/newcode
                                if (selectMe)
                                {
                                    Cursor = Cursors.Cross;
                                    startMouseLocation = new Point(e.Location.X, e.Location.Y);//Newly added
                                }
                                else
                                {

                                    startLocation = new Point((int)((double)e.Location.X * zoomfactor + 0.49) + myDrawingRect.Location.X, (int)((double)e.Location.Y * zoomfactor + 0.49) + myDrawingRect.Location.Y);//new Point(e.Location.X + myDrawingRect.Location.X, e.Location.Y + myDrawingRect.Location.Y);
                                    Cursor = Cursors.Hand; //just for looks.
                                }
                                //End/newocde
                            }
                        }
                    }
                }
                else
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (Control.ModifierKeys == Keys.Shift)
                        {
                            selectMe = !selectMe;
                            rotateMe = false;
                            if (selectMe)
                            {
                                selectionRectangle = new Rectangle(e.Location.X, e.Location.Y, 0, 0);
                            }
                        }
                        else
                        {
                            if (selectMe == false)
                            {
                                //newly added 
                                startLocation = new Point((int)((double)e.Location.X * zoomfactor + 0.49) + myDrawingRect.Location.X, (int)((double)e.Location.Y * zoomfactor + 0.49) + myDrawingRect.Location.Y);
                                startMouseLocation = new Point(e.Location.X, e.Location.Y);
                                //newly added 
                                zoomMe = !zoomMe;
                                if (zoomMe)
                                {
                                    gloBox_Zoom(-0.5);
                                    inMouseWheel = true;
                                    if (OnZoomChanged != null)
                                    {
                                        gloPictureEventArgs ge=new gloPictureEventArgs();
                                        ge.value = Zoom;                                        
                                        OnZoomChanged(this,ge);
                                        ge = null;
                                    }
                                   
                                    inMouseWheel = false;
                                }
                                else
                                {
                                    gloBox_Zoom(1.0 - zoomfactor);
                                    inMouseWheel = true;
                                    if (OnZoomChanged != null)
                                    {
                                        gloPictureEventArgs ge = new gloPictureEventArgs();
                                        ge.value = Zoom;
                                        OnZoomChanged(this, ge);
                                        ge = null;
                                    }
                                  
                                    inMouseWheel = false;

                                }

                            }
                            else
                            {
                                selectMe = false;
                                double newzoomx = (((double)selectionRectangle.Width * zoomfactor) / (double)myPictRect.Width);
                                double newzoomy = (((double)selectionRectangle.Height * zoomfactor) / (double)myPictRect.Height);
                                double newzoomz = newzoomx;
                                if (newzoomy > newzoomz)
                                {
                                    newzoomz = newzoomy;
                                }
                                startLocation = new Point(myDrawingRect.Location.X + (int)((double)selectionRectangle.X * zoomfactor + 0.49), myDrawingRect.Location.Y + (int)((double)selectionRectangle.Y * zoomfactor + 0.49));
                                startMouseLocation = new Point(0, 0);
                                gloBox_Zoom(newzoomz - zoomfactor);
                                inMouseWheel = true;
                                if (OnZoomChanged != null)
                                {
                                    gloPictureEventArgs ge = new gloPictureEventArgs();
                                    ge.value = Zoom;
                                    OnZoomChanged(this, ge);
                                    ge = null;
                                }
                                   
                                inMouseWheel = false;
                            }
                        }
                    }

                }
            }
            //Added for Bug #77539: 00000115 : Patient Setup
            _ispicPAPhotomodified = true;

        }
        public int Rotation
        {
            get
            {

                return (int)((double)(rotationfactor / rotationstepfactor) + 0.49);
            }
            set
            {
                rotationfactor = (double)value * rotationstepfactor;
                rotateMe = false;

                gloBox_Rotate(rotationfactor, myDrawingRect.Location);
            }
        }

        public int Zoom
        {
            get
            {

                return (int)((double)(zoomfactor / stepfactor) + 0.49);
            }
            set
            {
                double thiszoomfactor = (double)value * stepfactor;
                zoomMe = thiszoomfactor != 1;

                gloBox_Zoom(thiszoomfactor - zoomfactor);
            }
        }

        public Point PLocation
        {
            get
            {
                return (new Point(myDrawingRect.Location.X - myOrgLocation.X, myDrawingRect.Location.Y - myOrgLocation.Y));
            }
            set
            {
                myDrawingRect.Location = new Point(value.X + myOrgLocation.X, value.Y + myOrgLocation.Y);
                Invalidate();
            }
        }

        private Point gloBox_getOrgLocation(Point OrgLocation)
        {
            //if (_Image != null)
            {
                const double piBy2 = Math.PI / 2.0;

                double oldWidth = (double)ImageWidthHere;
                double oldHeight = (double)ImageHeightHere;
                double angleInRadians = ((double)rotationfactor) * Math.PI / 180.0;

                //Please change adjacentTop = lowerLeftX, oppositeTop = upplerLeftY, adjacentBottom=lowerRightY, oppositBottom=upperRightX;
                //double adjacentTop, oppositeTop;
                //double adjacentBottom, oppositeBottom;
                int lowerLeftX, upperRightX, lowerRightY, upperLeftY;
                double cosAngle = Math.Abs(Math.Cos(angleInRadians));
                double sinAngle = Math.Abs(Math.Sin(angleInRadians));

                if ((angleInRadians >= 0.0 && angleInRadians < piBy2) ||
                    (angleInRadians >= Math.PI && angleInRadians < (Math.PI + piBy2)))
                {
                    lowerLeftX = (int)((cosAngle) * oldWidth + 0.49);
                    upperLeftY = (int)((sinAngle) * oldWidth + 0.49);

                    lowerRightY = (int)((cosAngle) * oldHeight + 0.49);
                    upperRightX = (int)((sinAngle) * oldHeight + 0.49);
                }
                else
                {
                    lowerLeftX = (int)((sinAngle) * oldHeight + 0.49);
                    upperLeftY = (int)((cosAngle) * oldHeight + 0.49);

                    lowerRightY = (int)((sinAngle) * oldWidth + 0.49);
                    upperRightX = (int)((cosAngle) * oldWidth + 0.49);
                }

                //int previousRotationWidth = rotationWidth;
                //int previousRotationHeight = rotationHeight;


                rotationWidth = lowerLeftX + upperRightX;
                rotationHeight = lowerRightY + upperLeftY;

                int newX, newY;






                if (angleInRadians >= 0.0 && angleInRadians < piBy2)
                {
                    // rotationPoints = new Point[] { new Point(upperRightX, 0), new Point(rotationWidth, upperLeftY), new Point(0, lowerRightY) };
                    //newX = upperRightX + (int)((double)OrgLocation.X * cosAngle - (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor);
                    //newY = (int)((double)OrgLocation.Y * cosAngle + (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor);

                    newX = (int)(((double)OrgLocation.X * cosAngle + (double)OrgLocation.Y * sinAngle - (double)upperRightX * cosAngle + 0.49));
                    newY = (int)(((double)-OrgLocation.X * sinAngle + (double)OrgLocation.Y * cosAngle + (double)upperRightX * sinAngle + 0.49));

                }
                else if (angleInRadians >= piBy2 && angleInRadians < Math.PI)
                {
                    //   rotationPoints = new Point[] { new Point(rotationWidth, upperLeftY), new Point(lowerLeftX, rotationHeight), new Point(upperRightX, 0) };
                    //newX = rotationWidth + (int)((double)-OrgLocation.X * cosAngle - (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor);
                    //newY = upperLeftY + (int)((double)-OrgLocation.Y * cosAngle + (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor);

                    newX = (int)(((double)OrgLocation.Y * sinAngle - (double)OrgLocation.X * cosAngle - (double)upperLeftY * sinAngle + (double)rotationWidth * cosAngle + 0.49));
                    newY = (int)((-((double)OrgLocation.Y * cosAngle + (double)OrgLocation.X * sinAngle) + (double)upperLeftY * cosAngle + (double)rotationWidth * sinAngle + 0.49));
                }
                else if (angleInRadians >= Math.PI && angleInRadians < (Math.PI + piBy2))
                {
                    // rotationPoints = new Point[] { new Point(lowerLeftX, rotationHeight), new Point(0, lowerRightY), new Point(rotationWidth, upperLeftY) };
                    //newX = lowerLeftX + (int)((double)-OrgLocation.X * cosAngle + (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor);
                    //newY = rotationHeight + (int)((double)-OrgLocation.Y * cosAngle - (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor);

                    newX = (int)(-((double)OrgLocation.Y * sinAngle + (double)OrgLocation.X * cosAngle) + (double)rotationHeight * sinAngle + (double)lowerLeftX * cosAngle + 0.49);
                    newY = (int)((double)OrgLocation.X * sinAngle - (double)OrgLocation.Y * cosAngle - (double)lowerLeftX * sinAngle + (double)rotationHeight * cosAngle + 0.49);

                }
                else
                {
                    // rotationPoints = new Point[] { new Point(0, lowerRightY), new Point(upperRightX, 0), new Point(lowerLeftX, rotationHeight) };

                    //newX = (int)((double)OrgLocation.X * cosAngle + (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor);
                    //newY = lowerRightY + (int)((double)OrgLocation.Y * cosAngle - (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor);


                    newX = (int)((double)OrgLocation.X * cosAngle - (double)OrgLocation.Y * sinAngle + (double)lowerRightY * sinAngle + 0.49);
                    newY = (int)((double)-lowerRightY * cosAngle + (double)OrgLocation.X * sinAngle + (double)OrgLocation.Y * cosAngle + 0.49);
                }

               
                return new Point(newX, newY);
            }
            //else
            //{
            //    return OrgLocation;
            //}
        }
        void gloBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingPicture)
            {
                if (rotateMe)
                {
                    //StartLocation                           //StartLocation
                    rotationfactor = (180.0 * Math.Atan2(startMouseLocation.Y - e.Location.Y, startMouseLocation.X - e.Location.X)) / Math.PI;
                    if (rotationfactor < 0)
                    {
                        rotationfactor += 360;
                    }
                    int myRotationInt = (int)((double)(rotationfactor / rotationstepfactor) + 0.49);
                    rotationfactor = ((double)myRotationInt) * rotationstepfactor;
                    gloBox_Rotate(rotationfactor, startLocation);
                    if (OnRotationChanged != null)
                    {
                        gloPictureEventArgs ge = new gloPictureEventArgs();
                        ge.value = Rotation;
                        OnRotationChanged(this, ge);
                        ge = null;

                    }
                }
                else if (selectMe)
                {
                    int x = e.Location.X;
                    int y = e.Location.Y;
                    int x0 = startMouseLocation.X;
                    int y0 = startMouseLocation.Y;
                    int rctleft = 0, rcttop = 0, rctwidth = 0, rctheight = 0;
                    if ((x >= x0) && (y >= y0))
                    {

                        rctleft = x0;
                        rcttop = y0;
                        rctwidth = x - x0;
                        rctheight = y - y0;
                    }
                    else if ((x < x0) && (y > y0))
                    {

                        rctleft = x;
                        rcttop = y0;
                        rctwidth = x0 - x;
                        rctheight = y - y0;
                    }
                    else if ((x > x0) && (y < y0))
                    {

                        rctleft = x0;
                        rcttop = y;
                        rctwidth = x - x0;
                        rctheight = y0 - y;
                    }
                    else if ((x < x0) && (y < y0))
                    {

                        rctleft = x;
                        rcttop = y;
                        rctwidth = x0 - x;
                        rctheight = y0 - y;
                    }
                    selectionRectangle = new Rectangle(rctleft, rcttop, rctwidth, rctheight);
                   Invalidate();

                }
                else
                {

                    myDrawingRect.Location = new Point(startLocation.X - (int)((double)e.Location.X * zoomfactor + 0.49), startLocation.Y - (int)((double)e.Location.Y * zoomfactor + 0.49));//new Point(startLocation.X - e.Location.X, startLocation.Y - e.Location.Y);
                    gloBox_DeflateRect();
                }
            }
        }
        private int ImageWidthHere
        {
            get
            {
                if (_Image != null)
                {
                    return _Image.Width;
                }
                else
                    return 640;
            }
            
        }
        private int ImageHeightHere
        {
            get
            {
                if (_Image != null)
                {
                    return _Image.Height;
                }
                else
                    return 480;
            }

        }
        protected void gloBox_Rotate(double angle, Point OrgLocation)
        {   double imageWidth=ImageWidthHere;
            double imageHeight=ImageHeightHere;
             
                const double piBy2 = Math.PI / 2.0;

                double oldWidth = imageWidth;
                double oldHeight = imageHeight;
                double angleInRadians = ((double)angle) * Math.PI / 180.0;

                //Please change adjacentTop = lowerLeftX, oppositeTop = upplerLeftY, adjacentBottom=lowerRightY, oppositBottom=upperRightX;
                //double adjacentTop, oppositeTop;
                //double adjacentBottom, oppositeBottom;
                int lowerLeftX, upperRightX, lowerRightY, upperLeftY;
                double cosAngle = Math.Abs(Math.Cos(angleInRadians));
                double sinAngle = Math.Abs(Math.Sin(angleInRadians));

                if ((angleInRadians >= 0.0 && angleInRadians < piBy2) ||
                    (angleInRadians >= Math.PI && angleInRadians < (Math.PI + piBy2)))
                {
                    lowerLeftX = (int)((cosAngle) * oldWidth + 0.49);
                    upperLeftY = (int)((sinAngle) * oldWidth + 0.49);

                    lowerRightY = (int)((cosAngle) * oldHeight + 0.49);
                    upperRightX = (int)((sinAngle) * oldHeight + 0.49);
                }
                else
                {
                    lowerLeftX = (int)((sinAngle) * oldHeight + 0.49);
                    upperLeftY = (int)((cosAngle) * oldHeight + 0.49);

                    lowerRightY = (int)((sinAngle) * oldWidth + 0.49);
                    upperRightX = (int)((cosAngle) * oldWidth + 0.49);
                }

                //int previousRotationWidth = rotationWidth;
                //int previousRotationHeight = rotationHeight;
                rotationWidth = lowerLeftX + upperRightX;
                rotationHeight = lowerRightY + upperLeftY;

                int newX, newY;






                if (angleInRadians >= 0.0 && angleInRadians < piBy2)
                {
                    rotationPoints = new Point[] { new Point(upperRightX, 0), new Point(rotationWidth, upperLeftY), new Point(0, lowerRightY) };
                    newX = upperRightX + (int)((double)OrgLocation.X * cosAngle - (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor + 0.49);
                    newY = (int)((double)OrgLocation.Y * cosAngle + (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor + 0.49);

                }
                else if (angleInRadians >= piBy2 && angleInRadians < Math.PI)
                {
                    rotationPoints = new Point[] { new Point(rotationWidth, upperLeftY), new Point(lowerLeftX, rotationHeight), new Point(upperRightX, 0) };

                    newX = rotationWidth + (int)((double)-OrgLocation.X * cosAngle - (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor + 0.49);
                    newY = upperLeftY + (int)((double)-OrgLocation.Y * cosAngle + (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor + 0.49);

                }
                else if (angleInRadians >= Math.PI && angleInRadians < (Math.PI + piBy2))
                {
                    rotationPoints = new Point[] { new Point(lowerLeftX, rotationHeight), new Point(0, lowerRightY), new Point(rotationWidth, upperLeftY) };
                    newX = lowerLeftX + (int)((double)-OrgLocation.X * cosAngle + (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor + 0.49);
                    newY = rotationHeight + (int)((double)-OrgLocation.Y * cosAngle - (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor + 0.49);

                }
                else
                {
                    rotationPoints = new Point[] { new Point(0, lowerRightY), new Point(upperRightX, 0), new Point(lowerLeftX, rotationHeight) };

                    newX = (int)((double)OrgLocation.X * cosAngle + (double)OrgLocation.Y * sinAngle - (double)startMouseLocation.X * zoomfactor + 0.49);
                    newY = lowerRightY + (int)((double)OrgLocation.Y * cosAngle - (double)OrgLocation.X * sinAngle - (double)startMouseLocation.Y * zoomfactor + 0.49);

                }

               
                myDrawingRect.Location = new Point(newX, newY);
                gloBox_DeflateRect();

            
        }
        void gloBox_MouseUp(object sender, MouseEventArgs e)
        {
            draggingPicture = false;
            Cursor = Cursors.Default;
            if (zoomMe)
            {
                startLocation = new Point((int)((double)e.Location.X * zoomfactor + 0.49) + myDrawingRect.Location.X, (int)((double)e.Location.Y * zoomfactor + 0.49) + myDrawingRect.Location.Y);
                startMouseLocation = new Point(e.Location.X, e.Location.Y);
            }
        }
        void gloBox_MouseHover(object sender, EventArgs e)
        {

            if ((_Image != null) && (_isMovable == true))
            {
                if (rotateMe)
                {
                    Cursor = Cursors.SizeAll;
                }
                else
                {
                    if (selectMe)
                    {
                        Cursor = Cursors.Cross;
                    }
                    else
                    {
                        Cursor = Cursors.Hand;
                    }

                }
            }
        }

        void gloBox_Zoom(double stepFactor)
        {
            zoomfactor += stepFactor;
            int myZoomInt = (int)((double)(zoomfactor / stepfactor) + 0.49);
            zoomfactor = ((double)myZoomInt) * stepfactor;
            if (zoomfactor > maxZoomFactor)
            {
                stepFactor = maxZoomFactor - zoomfactor;
                zoomfactor = maxZoomFactor;

            }
            else if (zoomfactor < minZoomFactor)
            {
                stepFactor = minZoomFactor - zoomfactor;
                zoomfactor = minZoomFactor;
            }
            //if (((stepFactor > 0) && (zoomfactor < 5.0)) || ((stepFactor < 0) && (zoomfactor > 0.2)))
            if (stepFactor != 0)
            {
                //zoomfactor += stepFactor;
                // int myZoomInt = (int) ((double)(zoomfactor / stepfactor)+0.49);
                // zoomfactor = ((double) myZoomInt) * stepfactor;
                int newX = startLocation.X - (int)((double)((double)startMouseLocation.X * zoomfactor + 0.49));
                int newY = startLocation.Y - (int)((double)((double)startMouseLocation.Y * zoomfactor + 0.49));


                int newW = (int)((double)myPictRect.Width * (double)zoomfactor);
                int newH = (int)((double)myPictRect.Height * (double)zoomfactor);
                // due to truncate, if width or height  = 0, then do reverse zoomfactor to get minimum;
                if (newW == 0)
                {
                    zoomfactor = 1.0 / (double)myPictRect.Width;
                    myZoomInt = (int)(double)((zoomfactor / stepfactor) + 0.49);
                    zoomfactor = ((double)myZoomInt) * stepfactor;
                    if (zoomfactor < minZoomFactor) zoomfactor = minZoomFactor;
                    newX = startLocation.X - (int)((double)((double)startMouseLocation.X * zoomfactor + 0.49));
                    newY = startLocation.Y - (int)((double)((double)startMouseLocation.Y * zoomfactor + 0.49));

                    newW = (int)((double)myPictRect.Width * (double)zoomfactor);
                    newH = (int)((double)myPictRect.Height * (double)zoomfactor);
                }
                if (newH == 0)
                {
                    zoomfactor = 1.0 / (double)myPictRect.Height;
                    myZoomInt = (int)(double)((zoomfactor / stepfactor) + 0.49);
                    zoomfactor = ((double)myZoomInt) * stepfactor;
                    if (zoomfactor < minZoomFactor) zoomfactor = minZoomFactor;
                    newX = startLocation.X - (int)((double)((double)startMouseLocation.X * zoomfactor + 0.49));
                    newY = startLocation.Y - (int)((double)((double)startMouseLocation.Y * zoomfactor + 0.49));
                    newW = (int)((double)myPictRect.Width * (double)zoomfactor);
                    newH = (int)((double)myPictRect.Height * (double)zoomfactor);
                }
                if (newX < -Padding.Left) newX = -Padding.Left;
                if (newY < -Padding.Top) newY = -Padding.Top;
                if (newX > rotationWidth - newW + Padding.Right) newX = rotationWidth - newW + Padding.Right;
                if (newY > rotationHeight - newH + Padding.Bottom) newY = rotationHeight - newH + Padding.Bottom;


                myDrawingRect.Location = new Point(newX, newY);
                myDrawingRect.Size = new Size(newW, newH);
               Invalidate();
            }
        }
  
        
        static bool inMouseWheel = false;
        void gloBox_MouseWheel(object sender, MouseEventArgs e)
        {

            //if (_Image != null)
            if ((_Image != null))
            {
                if (zoomMe)
                {
                    if (e.Delta < 0)
                    {
                        gloBox_Zoom(stepfactor);
                    }
                    else if (e.Delta > 0)
                    {

                        gloBox_Zoom(-stepfactor);

                    }
                    if (inMouseWheel == false)
                    {
                        inMouseWheel = true;

                        if (OnZoomChanged != null)
                        {
                            gloPictureEventArgs ge = new gloPictureEventArgs();
                            ge.value = Zoom;
                            OnZoomChanged(this, ge);
                            ge = null;
                        }
                                   

                        inMouseWheel = false;
                    }


                }

            }


        }
        //Bottom zoom out
        protected void gloBox_DeflateRect()
        {
            int newX = myDrawingRect.Location.X;
            int newY = myDrawingRect.Location.Y;
            if (newX < -Padding.Left) newX = -Padding.Left;
            if (newY < -Padding.Top) newY = -Padding.Top;
            if (newX > rotationWidth - myDrawingRect.Width + Padding.Right) newX = rotationWidth - myDrawingRect.Width + Padding.Right;
            if (newY > rotationHeight - myDrawingRect.Height + Padding.Bottom) newY = rotationHeight - myDrawingRect.Height + Padding.Bottom;
            myDrawingRect.Location = new Point(newX, newY);
            Invalidate();
        }
        
        protected Image _Image = null;
        //_BackImage is set with backgroundimage as it is by default feature in .net 4.0

        //private Image _BackImage = null;
        //public Image BackgroundImage
        //{
        //    get
        //    {
        //        return _BackImage;
        //    }
        //    set
        //    {
        //        _BackImage = value;
        //    }
        //}
        public Image Image
        {
            get
            {
                return _Image;
            }
            set
            {
                if (_Image != null)
                {
                    //Code Review Changes: add try catch
                    try
                    {
                        _Image.Dispose();
                        _Image = null;
                    }
                    catch 
                    { }
                   
                }
                _Image = value;
                zoomfactor = 1.0;
                zoomMe = false;
                rotationfactor = 0;
                //isCameraPicture = false;
                myDrawingRect = myPictRect;
                
                    rotationWidth = ImageWidthHere;
                    rotationHeight = ImageHeightHere;
                    myDrawingRect = myPictRect;
                    gloBox_SetCenter();
                    gloBox_Rotate(rotationfactor, myDrawingRect.Location);
                    gloBox_DeflateRect();

                 
                myOrgLocation = myDrawingRect.Location;
                Invalidate();
            }
        }




        protected override void OnPaint(PaintEventArgs e)
        {

            if ((_Image != null))
            {

                //Rectangle myrotationRect = new Rectangle();
                //myrotationRect.Width = (int)((double)rotationWidth / (double)_Image.Width * (double)DrawRect.Width);
                //myrotationRect.Height = (int)((double)rotationHeight / (double)_Image.Height * (double)DrawRect.Height);

                //myrotationRect.X = (int)((double)DrawRect.X / (double)_Image.Width * (double)DrawRect.Width);
                //myrotationRect.Y = (int)((double)DrawRect.Y / (double)_Image.Height * (double)DrawRect.Height);

                using (Bitmap rotatedBmp = new Bitmap(rotationWidth, rotationHeight))
                {

                    using (Graphics g = Graphics.FromImage(rotatedBmp))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        try
                        {

                            g.DrawImage(_Image, rotationPoints);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        //
                        g.Save();

                        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        if (myBrush != null)
                        {
                            e.Graphics.FillRectangle(myBrush, ClientRectangle);
                        }
                        //
                        e.Graphics.DrawImage(rotatedBmp, ClientRectangle, myDrawingRect, GraphicsUnit.Pixel);
                    }


                }

            }
            else
            {
                //from the Dashbord
                if (BackgroundImage  != null)
                {
                    if (myBrush != null)
                    {
                        e.Graphics.FillRectangle(myBrush, ClientRectangle);
                    }
                    //
                    e.Graphics.DrawImage(BackgroundImage, ClientRectangle, new Rectangle(0, 0, BackgroundImage.Width, BackgroundImage.Height), GraphicsUnit.Pixel);
                    if (outlinePictureBox != null)
                    {
                        e.Graphics.DrawRectangle(outlinePictureBox, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                    }

                }
                else
                {

                    if (myBrush != null)
                    {
                        e.Graphics.FillRectangle(myBrush, ClientRectangle);
                    }
                    if (outlinePictureBox != null)
                    {
                        e.Graphics.DrawRectangle(outlinePictureBox, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                    }

                }

            }
            if (selectMe)
            {
                if (selectionPen != null)
                {
                    e.Graphics.DrawRectangle(selectionPen, selectionRectangle);
                }
            }
            base.OnPaint(e);
        }



        public Image copyFrame()
        {
            return copyFrame(ClientRectangle, true);
        }

        public Image copyFrame(Rectangle mySize, bool CropMe)
        {
            return copyFrame(mySize, CropMe, false);
        }

        //Start :: Taking the Size from another location
        public Image copyFrame(Rectangle mySize, bool CropMe, bool AddBlank)
        {


            //if (_Image != null)
            if ((_Image != null))
            {
                //Rotation
                //Rectangle myrotationRect = new Rectangle();
                //myrotationRect.Width = (int)((double)rotationWidth / (double)_Image.Width * (double)DrawRect.Width);
                //myrotationRect.Height = (int)((double)rotationHeight / (double)_Image.Height * (double)DrawRect.Height);

                //myrotationRect.X = (int)((double)DrawRect.X / (double)_Image.Width * (double)DrawRect.Width);
                //myrotationRect.Y = (int)((double)DrawRect.Y / (double)_Image.Height * (double)DrawRect.Height);

                Bitmap rotatedBmp = new Bitmap(rotationWidth, rotationHeight);

                using (Graphics g = Graphics.FromImage(rotatedBmp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                    g.DrawImage(_Image, rotationPoints);
                    //
                    g.Save();
                    //
                }
                //
                if (CropMe)
                {
                    Rectangle actRect = myDrawingRect;
                    if (actRect.X < 0)
                    {
                        actRect.Width += actRect.X;
                        actRect.X = 0;
                        if (amIFromDefaultImage)
                        {
                            actRect.Height += actRect.Y;
                            actRect.Y = 0;
                        }
                    }
                    if (actRect.Y < 0)
                    {
                        actRect.Height += actRect.Y;
                        actRect.Y = 0;
                        if (amIFromDefaultImage)
                        {
                            actRect.Width += actRect.X;
                            actRect.X = 0;
                        }
                    }
                    //Add(Center)--
                    if ((actRect.X + actRect.Width) > rotationWidth)
                    {
                        actRect.Width = rotationWidth - actRect.X;

                    }
                    if ((actRect.Y + actRect.Height) > rotationHeight)
                    {
                        actRect.Height = rotationHeight - actRect.Y;

                    }
                    //
                    //


                    //Rectangle mySrcRect = actRect;
                    Rectangle mySrcRect = new Rectangle();
                    mySrcRect.X = 0;
                    mySrcRect.Y = 0;



                    //
                    if (mySize == null)
                    {
                        mySize = actRect;
                    }
                    double xFactor = (double)mySize.Width / (double)actRect.Width;
                    double yFactor = (double)mySize.Height / (double)actRect.Height;


                    if (xFactor >= yFactor)
                    {

                        mySrcRect.Width = (int)((double)actRect.Width * yFactor);
                        mySrcRect.Height = mySize.Height;
                        if (AddBlank == true) //While we set the value it was going out of the picturebox control box.
                        {
                            mySrcRect.X = (mySize.Width - mySrcRect.Width) / 2;
                        }
 
                    }
                    else
                    {
                        mySrcRect.Height = (int)((double)actRect.Height * xFactor);
                        mySrcRect.Width = mySize.Width;
                        if (AddBlank == true)//While we set the value it was going out of the picturebox control box.
                        {
                            mySrcRect.Y = (mySize.Height - mySrcRect.Height) / 2;
                        }

                    }


                    Image srcBmp = null;
                    if (AddBlank == true)
                    {
                        srcBmp = new Bitmap(mySize.Width, mySize.Height);
                    }
                    else
                    {
                        srcBmp = new Bitmap(mySrcRect.Width, mySrcRect.Height);
                    }

                    Graphics srcMem = Graphics.FromImage(srcBmp);

                    if (AddBlank == true)
                    {
                        srcMem.FillRectangle(Brushes.White, mySize);

                    }
                    else
                    {
                        if (myBrush != null)
                        {
                            srcMem.FillRectangle(myBrush, mySrcRect);
                        }

                    }

                    //





                    srcMem.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    srcMem.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    srcMem.DrawImage(rotatedBmp, mySrcRect, actRect, GraphicsUnit.Pixel);

                    //
                    if (AddBlank == true)
                    {
                        Pen myPen = new Pen(Brushes.Black, 5);
                        srcMem.DrawRectangle(myPen, mySrcRect.X, mySrcRect.Y, mySrcRect.Width - 5, mySrcRect.Height - 5);
                        myPen.Dispose();
                        myPen = null;
                    }
                    //

                    srcMem.Save();
                    srcMem.Dispose();
                    rotatedBmp.Dispose();
                    Image myClone = (Image)srcBmp.Clone();
                    srcBmp.Dispose();

                    return myClone;
                }
                else
                {

                    Image myClone = (Image)rotatedBmp.Clone();
                    rotatedBmp.Dispose();
                    return myClone;
                }
               
            }
            else
            {
                if (AddBlank == false || mySize == null)
                {
                    return _Image;
                }

                Image srcBmp = new Bitmap(mySize.Width, mySize.Height);
                Graphics srcMem = Graphics.FromImage(srcBmp);
                srcMem.FillRectangle(Brushes.White, mySize);
                Pen myPen = new Pen(Brushes.Black, 5);
                srcMem.DrawRectangle(myPen, 0, 0, mySize.Width - 5, mySize.Height - 5);
                myPen.Dispose();
                myPen = null;
                srcMem.Save();
                srcMem.Dispose();
                Image myClone = (Image)srcBmp.Clone();
                srcBmp.Dispose();

                return myClone;
            }

            //End :: Taking the Size from another location




        }
        
        public Image copyFrame(bool CropMe)
        {



            if ((_Image != null))
            {
                Rectangle mySize = new Rectangle(0, 0, pictBoxWidth, PictBoxHeight);
                return copyFrame(mySize, CropMe);
            }
            else
            {
                return _Image;
            }

            //End :: Taking the Size from another location
        }

        static readonly int myTrackMax = 44;

       
        public int ZoomValueForTrackBar
        {
            get
            {
                 
                double defaultZoomFactor = ((double)defaultZoom * stepfactor);
                double numeratior = (defaultZoomFactor - zoomfactor) * myTrackMax * (maxZoomFactor - minZoomFactor);
                double denominator = ((minZoomFactor + maxZoomFactor - 2 * defaultZoomFactor) * zoomfactor) - 2 * (minZoomFactor * maxZoomFactor) + defaultZoomFactor * (maxZoomFactor + minZoomFactor);
                return (int)((numeratior / denominator) + 0.49);
               
            }
            set
            {
                
                double defaultZoomFactor = ((double)defaultZoom * stepfactor);
                double numeratior = defaultZoomFactor * myTrackMax * (maxZoomFactor - minZoomFactor) + (2 * (minZoomFactor * maxZoomFactor) - defaultZoomFactor * (maxZoomFactor + minZoomFactor)) * (double)value;
                double denominator = myTrackMax * (maxZoomFactor - minZoomFactor) + (minZoomFactor + maxZoomFactor - 2 * defaultZoomFactor) * (double)value;
                Zoom = (int)((numeratior / denominator) / stepfactor + 0.49);
                zoomMe = true;
            }
        }
         
        //ethomas was having some problem (as was seen in the zoom form if we import)
        private Image resizeImage(Image imgToResize, int destWidth, int destHeight)
        {
            Image bImage = null;
            try
            {
                bImage = new Bitmap(imgToResize, new Size(destWidth, destHeight));
            }
            catch
            {
                try
                {
                    bImage = new Bitmap(destWidth, destHeight);
                    if (imgToResize != null)
                    {
                        Graphics g = Graphics.FromImage(bImage);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        try
                        {
                            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                        }
                        catch
                        {
                            g.Dispose();
                            bImage.Dispose();
                            return ((Image)imgToResize.Clone());

                        }
                        g.Dispose();
                    }

                }
                catch
                {
                    return ((Image)imgToResize.Clone());
                }
            }

            return bImage;
        }
        //Problem No: 00000361
        //Get true value if image copy from clipboard to implement webcam functionality in TS,else False
        public void AspectRatio(Image img,bool IsFromClipboard)
        {

            double picOutputwidth = ((double)this.Width * maxZoomFactor);
            double picOutputheight = ((double)this.Height * maxZoomFactor);
            double OutputWidth = (double)img.Width;
            double OutputHeight = (double)img.Height;
            double myScale = 1;
            //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures Begin
            double picInputwidth = ((double)this.Width * minZoomFactor);
            double picInputheight = ((double)this.Height * minZoomFactor);

            if (OutputWidth < picInputwidth)
            {
                myScale = OutputWidth / picInputwidth;
                OutputWidth = picInputwidth;
                OutputHeight /= myScale;

            }
            if (OutputHeight < picInputheight)
            {
                myScale = OutputHeight / picInputheight;
                OutputHeight = picInputheight;
                OutputWidth /= myScale;

            }
            //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures End

            if (OutputWidth > picOutputwidth)
            {
                myScale = OutputWidth / picOutputwidth;
                OutputWidth = picOutputwidth;
                OutputHeight /= myScale;

            }
            if (OutputHeight > picOutputheight)
            {
                myScale = OutputHeight / picOutputheight;
                OutputHeight = picOutputheight;
                OutputWidth /= myScale;

            }

            myScale = (double)img.Height / (double)OutputHeight;
            //Problem No: 00000361
            //Check if copy from clipboard or not to implement webcam functionality in TS
            if( (myScale != 1.0) || (IsFromClipboard) )
            {

                Image myimg = resizeImage(img, (int)OutputWidth, (int)OutputHeight);
                if (myimg != null)
                {
                    this.Image = (Image)myimg.Clone();
                    myimg.Dispose();
                }
                else
                {
                    this.Image = (Image) img.Clone();
                }
            }
            else
            {
                this.Image = (Image)img.Clone();
            }



            double myPicWidth = (double)this.Width;
            double myPicHeight = (double)this.Height;

            double myScaleX = (double)this.Image.Width / myPicWidth;
            double myScaleY = (double)this.Image.Height / myPicHeight;

            double myScaleZ = myScaleX;
            if (myScaleZ > myScaleY)
            {
                myScaleZ = myScaleY;
            }
            this.Zoom = (int)((myScaleZ / stepfactor) + 0.49);
            //myDrawingRect.X = 0;
            //myDrawingRect.Y = 0;
           Invalidate();

        }


        //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures Begin
        public bool reDoAspectRatio(Image img)
        {

            double picOutputwidth = ((double)this.Width * maxZoomFactor);
            double picOutputheight = ((double)this.Height * maxZoomFactor);
            double OutputWidth = (double)img.Width;
            double OutputHeight = (double)img.Height;
            double picInputwidth = ((double)this.Width * minZoomFactor);
            double picInputheight = ((double)this.Height * minZoomFactor);
            return ((OutputWidth > picOutputwidth) || (OutputHeight > picOutputheight) || (OutputWidth < picInputwidth) || (OutputHeight < picInputheight));
        }


        public double getAutoFitZoomFactor(Image img, bool smallerOrLarger, bool evenIfsmaller)
        {
            double myScaleY = 1;
            double myScaleX = 1;
            if ((img.Width > this.Width) || evenIfsmaller)
            {
                myScaleX = ((double)img.Width) / ((double)this.Width);
            }
            if ((img.Height > this.Height) || evenIfsmaller)
            {
                myScaleY = ((double)img.Height) / ((double)this.Height);
            }
            if (myScaleX < myScaleY)
            {
                if (smallerOrLarger)
                {
                    return myScaleY;
                }
                return myScaleX;
            }
            if (smallerOrLarger)
            {
                return myScaleX;
            }
            return myScaleY;
        }
        public void fitImage(bool smallerOrLarger)
        {
            double myScaleY = 1;
            double myScaleX = 1;
            if ( Image.Width > pictBoxWidth )
            {
                myScaleX = ((double)Image.Width) / ((double)pictBoxWidth);
            }
            if (Image.Height > pictBoxHeight)
            {
                myScaleY = ((double)Image.Height) / ((double)pictBoxHeight);
            }
            
            if (myScaleX < myScaleY)
            {
                if (smallerOrLarger)
                {
                    Zoom = (int)((double)(myScaleY / stepfactor + 0.49));
                    
                }
                Zoom = (int)((double)(myScaleX / stepfactor + 0.49));
            }
            if (smallerOrLarger)
            {
                Zoom = (int)((double)(myScaleX / stepfactor + 0.49));
            }
            Zoom = (int)((double)(myScaleY / stepfactor + 0.49));
        }
        static String sPubZoomVersion = "";
        public String sZoomVersion
        {
            get 
            {   

                 if (sPubZoomVersion == "")
                {
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                    {
                        return "5X";
                    }
                    object oZoomVersion = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gZoomVersion);
                    gloRegistrySetting.CloseRegistryKey();

                    if (oZoomVersion != null)
                    {
                        sPubZoomVersion = oZoomVersion.ToString().ToUpper();
                    }
                    else
                    {
                        sPubZoomVersion = "5X";
                    }
                }
                return sPubZoomVersion;
            }
            set 
            {   

                    if( value == "Insurance" )
                    {

                            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, true, "") == false)
                            {
                                sPubZoomVersion = "5X";
                            }
                            else 
                            {
                            object oZoomVersion = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gZoomVersion);
                            gloRegistrySetting.CloseRegistryKey();

                            if (oZoomVersion != null)
                            {
                                sPubZoomVersion = oZoomVersion.ToString().ToUpper();
                            }
                            else
                            {
                                sPubZoomVersion = "5X";
                            }
                            }
                                  
                    }
                    else
                    {
                        if (value == "7X")
                        {
                            sPubZoomVersion = "7X";
                        }
                        else
                        {
                            if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == false)
                            {
                                sPubZoomVersion = "5X";
                            }
                            else
                            {
                                object oZoomVersion = gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gZoomVersion);
                                gloRegistrySetting.CloseRegistryKey();

                                if (oZoomVersion != null)
                                {
                                    sPubZoomVersion = oZoomVersion.ToString().ToUpper();
                                }
                                else
                                {
                                    sPubZoomVersion = "5X";
                                }
                            }
                        }
                    }
                   // sZoomVersion = sPubZoomVersion;

            }
        }



        public bool adjustgloPictureZoom(Image img, ref int myZoom)
        {
            if (sZoomVersion == "7X")
            {
                return false;
            }

            if (sZoomVersion == "6X")
            {
                if (reDoAspectRatio(img))
                {
                    //SLR: 12/12/11 Image is too big, to be reduced without any cropping   
                    myZoom = (int)((double)(getAutoFitZoomFactor(img, false, false) / stepfactor + 0.49));
                    return true;
                }
                else
                {
                    //SLR: 12/12/11 retain the zoom factor as it is 
                    return false;
                }
            }
            if (sZoomVersion == "5X")
            {
                //SLR: 12/12/11 Auto fit if image is large with cropped mode and don't enlarge if image is small
                myZoom = (int)((double)(getAutoFitZoomFactor(img, true, false) / stepfactor + 0.49));
            }
            if (sZoomVersion == "4X")
            {
                //SLR: 12/12/11 Auto fit if image is large without any cropping and don't enlarge if image is small 
                myZoom = (int)((double)(getAutoFitZoomFactor(img, false, false) / stepfactor + 0.49));
            }
            if (sZoomVersion == "3X")
            {
                //SLR: 12/12/11 Auto fit if image is large with cropped mode and enlarge even if image is small
                myZoom = (int)((double)(getAutoFitZoomFactor(img, true, true) / stepfactor + 0.49));
            }
            if (sZoomVersion == "2X")
            {
                //SLR: 12/12/11 Auto fit if image is large without any cropping and enlarge even if image is small
                myZoom = (int)((double)(getAutoFitZoomFactor(img, false, true) / stepfactor + 0.49));
            }
            return true;
        }
        //SLR: Added on 12/12/11 to adjust the zoom factor for ethomas pictures End


        private void DisposingPrivateObject()
        {
            this.MouseDown -= new MouseEventHandler(gloBox_MouseDown);
            this.MouseMove -= new MouseEventHandler(gloBox_MouseMove);
            this.MouseUp -= new MouseEventHandler(gloBox_MouseUp);
            this.MouseHover -= new EventHandler(gloBox_MouseHover);
            this.MouseWheel -= new MouseEventHandler(gloBox_MouseWheel);
            //Start/Rotation
            //this.MouseDoubleClick -= new MouseEventHandler(gloBox_MouseDoubleClick);
            //End/Rotation

            if (myBrush != null)
            {
                myBrush.Dispose();
                myBrush = null;

            }
            if (outlinePictureBox != null)
            {
                outlinePictureBox.Dispose();
                outlinePictureBox = null;
            }
            if (selectionPen != null)
            {
                selectionPen.Dispose();
                selectionPen = null;
            }
            if (_Image != null)
            {
                _Image.Dispose();
                _Image = null;
            }
            if (BackgroundImage != null)
            {
                BackgroundImage.Dispose();
                BackgroundImage = null;
            }
        }

        public void Disposer()
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
                    DisposingPrivateObject();
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

       

    }
    public static class gloImage
    {
        public static byte[] GetImageByteArray(byte[] byteArray, int patientPhotoWidth=123, int patientPhotoHeight=137)
        {

            byte[] arrByteImage = null;
            using (gloPictureBox myPixBx = new gloPictureBox() )
            {
                myPixBx.byteImage = byteArray;

                using (System.Drawing.Image PatientPhoto = myPixBx.copyFrame(new Rectangle(0, 0, patientPhotoWidth, patientPhotoHeight), true))
                {

                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {

                        try
                        {
                            PatientPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }
                        ms.Close();
                        arrByteImage = ms.ToArray();
                    }
                }

            }
            return arrByteImage;
        }
        public static byte[] GetImageByteArray(byte[] byteArray, bool bCrop)
        {

            byte[] arrByteImage = null;
            using (gloPictureBox myPixBx = new gloPictureBox())
            {
                myPixBx.byteImage = byteArray;

                using (System.Drawing.Image PatientPhoto = myPixBx.copyFrame(bCrop))
                {

                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {

                        try
                        {
                            PatientPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }
                        ms.Close();
                        arrByteImage = ms.ToArray();
                    }
                }

            }
            return arrByteImage;
        }
        public static Image GetImage(byte[] byteArray, int patientPhotoWidth = 123, int patientPhotoHeight = 137)
        {

           
            using (gloPictureBox myPixBx = new gloPictureBox())
            {
                myPixBx.byteImage = byteArray;

                return myPixBx.copyFrame(new Rectangle(0, 0, patientPhotoWidth, patientPhotoHeight), true);
               

            }
           
        }
        public static Image GetImage(byte[] byteArray, bool bCrop)
        {

             
            using (gloPictureBox myPixBx = new gloPictureBox())
            {
                myPixBx.byteImage = byteArray;

                return myPixBx.copyFrame(bCrop);
               

            }
             
        }
    }
}
    

