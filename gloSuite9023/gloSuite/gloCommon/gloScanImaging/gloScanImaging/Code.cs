/*
 * Image Viewer component source
 * 
 * definations found in this file are
 * -ImageViewer class
 * -ZoomMode enumeration
 * -PreviewMode enumeration
 * -IZImage interface
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
//using System.IO;

namespace gloScanImaging
{
    /// <summary>
    /// this class extends from control
    /// provides the capability of displaying images with enhaced features of previewing them
    /// </summary>
    public class ImageViewer : Control
    {
        #region member fields

        //the interface that isolates the required properties and methods from any object that implement it
        IZImage mainImage;
        //a private member that temporarly holds the image that is currently being displayed
        public Image originalImage;
        public float originalImageWidth = 3;
        public float originalImageHeight = 3;

        //the amount by which the current image is zoomed
        double mZoom;
        //this is the coordinate of the center of the image displayed on the control in reference to 
        //the coordinate system of the original image
        Point displayCenter;
        //a bitmap that is used to draw the requested portion of the image on to the display
        Bitmap displayImage;
        //determines how the mouse behaves on the image
        PreviewMode previewMode;

        //the base control that is used to to display the finalized version of the requested image
        PictureBox pic;


        public gloScanImaging.ZoomMode CurrSelectedMode = gloScanImaging.ZoomMode.FITPAGE;
        //a private member that temporarly holds points in the process of manipulations
        //Point tempCenter;

        //the following members are used to hold a certain number of values when ever the display
        //changes so that the hosting control can request the previously previewed portions of the image
        //A collection of double variables that holds a certain amount of zoom values
        //List<double> zoomValues;
        //A Collection of Point structures that holds a certain amount of displayed center values
        //List<Point> centerValues;
        //a variable that limites the back tracking capacity of the history facility
        //const int HISTORYCAPACITY=5;

        #endregion

        #region member properties

        /// <summary>
        /// gets or sets how the mouse behaves on the image control display area
        /// </summary>
        public PreviewMode ImagePreviewMode
        {
            get { return previewMode; }
            set { previewMode = value; }
        }
        //public static void WriteLog(string LogString)
        //{
        //    using (StreamWriter oStreamWriter = new StreamWriter(@"C:\Program Files (x86)\gloEMR\CodeLogFile.txt", true))
        //    {
        //        if (oStreamWriter != null)
        //        {
        //            oStreamWriter.WriteLine(LogString);
        //            oStreamWriter.Close();
        //        }

        //    }
        //}
        /// <summary>
        /// get the original image that is currently being diplayed
        /// the entire image even though it is not fully displayed
        /// </summary>
        public Image CurrentImage
        {
            get { return originalImage; }
        }

        /// <summary>
        /// returns the image that is displayed on the screen
        /// </summary>
        public Image CurrentDisplayedImage
        {
            get { return displayImage; }
        }

        #endregion

        public void DisposeMyResources()
        {
            try
            {
                if (displayImage != null)
                {
                    displayImage.Dispose();
                    displayImage = null;
                }

            }
            catch
            {
            }
            try
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                    originalImage = null;
                }

            }
            catch
            {
            }
        }

        #region member methods

        /// <summary>
        /// this method draws the image with the image mode specified
        /// </summary>
        /// <param name="md">describes how the original image will appear in the control</param>
        /// 
        private Size GetParentSize()
        {
            if (this.Parent != null)
            {
                //if (originalImage != null)
                //{
                //    this.pic.SizeChanged -= new System.EventHandler(this.pic_SizeChanged);
                //    this.pic.Size = GetPictureSize();
                //    this.pic.SizeChanged += new System.EventHandler(this.pic_SizeChanged);
                //}
                return this.Parent.Size;

            }
            else
            {
                return this.Size;

            }
        }
        public Control GetPictureControl()
        {
            return pic;
        }
        public void ZoomImage(ZoomMode md)
        {
            /*
             * what this function basically does is retrieve the zoom mode requested
             * then compute the center and zoom values that are appropriate to correctly display
             * the image in the requested mode
             */
            Size ParentSize = GetParentSize();
            ParentSize.Height -= 2;
            ParentSize.Width -= 2;
            if (ParentSize.Height <= 2)
            {
                ParentSize.Height = 3;
            }
            if (ParentSize.Width <= 2)
            {
                ParentSize.Width = 3;
            }
            if (originalImage != null)
            {
                displayCenter = new Point((int)(originalImageWidth / 2), (int)(originalImageHeight / 2));
                switch (md)
                {
                    case ZoomMode.ACTUALSIZE:
                        mZoom = 1;
                        break;
                    case ZoomMode.FITHEIGHT:
                        mZoom = (double)ParentSize.Height / originalImageHeight;
                        break;
                    case ZoomMode.FITPAGE:
                        double mZoomY = (double)ParentSize.Height / originalImageHeight;
                        double mZoomX = (double)ParentSize.Width / originalImageWidth;
                        mZoom = Math.Min(mZoomX, mZoomY);
                        break;
                    case ZoomMode.FITWIDTH:
                        mZoom = (double)ParentSize.Width / originalImageWidth;
                        break;
                    default:
                        throw new Exception("Invalid zoom request!!");
                }
            }
            //once the values have been calculated call the zoomimage function to redraw the image
            //with the new parameters 
            ZoomImage();

        }
        //private float returnWidth()
        //{
        //    if ((originalImage.VerticalResolution != 0) && (originalImage.VerticalResolution != 0))
        //    {
        //        if (originalImage.HorizontalResolution > originalImage.VerticalResolution)
        //        {
        //            return ((float)originalImage.Width * (originalImage.VerticalResolution / originalImage.HorizontalResolution));
        //        }
        //    }
        //    return originalImage.Width;
        //}
        //private float returnHeight()
        //{
        //    if ((originalImage.VerticalResolution != 0) && (originalImage.VerticalResolution != 0))
        //    {
        //            if (originalImage.HorizontalResolution < originalImage.VerticalResolution)
        //            {
        //                return ((float)originalImage.Height * (originalImage.HorizontalResolution / originalImage.VerticalResolution));
        //            }
        //    }
        //    return originalImage.Height;
        //}
        //private Size returnSize()
        //{
        //    Size newSize = new Size(originalImage.Width, originalImage.Height);
        //    if ((originalImage.VerticalResolution != 0) && (originalImage.VerticalResolution != 0))
        //    {
        //        if (originalImage.HorizontalResolution > originalImage.VerticalResolution)
        //        {
        //            newSize.Width = (int)((float)originalImage.Width * (originalImage.VerticalResolution / originalImage.HorizontalResolution));
        //        }
        //        else
        //        {
        //            if (originalImage.HorizontalResolution < originalImage.VerticalResolution)
        //            {
        //                newSize.Height = (int)((float)originalImage.Height * (originalImage.HorizontalResolution / originalImage.VerticalResolution));
        //            }
        //        }
        //    }
        //    return newSize;
        //}
        public void AdjustResolution()
        {
            if (originalImage != null)
            {
                originalImageWidth = originalImage.Width;
                originalImageHeight = originalImage.Height;
                if ((originalImage.VerticalResolution != 0) && (originalImage.VerticalResolution != 0))
                {
                    if (originalImage.HorizontalResolution > originalImage.VerticalResolution)
                    {
                        originalImageWidth = ((float)originalImage.Width * (originalImage.VerticalResolution / originalImage.HorizontalResolution));
                    }
                    else
                    {
                        if (originalImage.HorizontalResolution < originalImage.VerticalResolution)
                        {
                            originalImageHeight = (int)((float)originalImage.Height * (originalImage.HorizontalResolution / originalImage.VerticalResolution));
                        }
                    }
                }
            }
            else
            {
                originalImageWidth = 3;
                originalImageHeight = 3;
            }
        }

        public double GetZoomValue(ZoomMode md)
        {
            /*
             * what this function basically does is retrieve the zoom mode requested
             * then compute the center and zoom values that are appropriate to correctly display
             * the image in the requested mode
             */
            Size ParentSize = GetParentSize();
            ParentSize.Height -= 20;
            ParentSize.Width -= 20;
            if (ParentSize.Height <= 20)
            {
                ParentSize.Height = 21;
            }
            if (ParentSize.Width <= 20)
            {
                ParentSize.Width = 21;
            }
            double mZoom = 1;
            if (originalImage == null)
            {
                return mZoom;
            }
            displayCenter = new Point((int)(originalImageWidth / 2), (int)(originalImageHeight / 2));
            switch (md)
            {
                case ZoomMode.ACTUALSIZE:
                    mZoom = 1;
                    break;
                case ZoomMode.FITHEIGHT:
                    //float newHeight = returnHeight();
                    //if (newHeight <= 0) newHeight = 1;
                    mZoom = (double)ParentSize.Height / originalImageHeight;
                    break;
                case ZoomMode.FITPAGE:
                    //Size newSize = returnSize();
                    //if (newSize.Height <= 0) newSize.Height = 1;
                    //if (newSize.Width <= 0) newSize.Width = 1;
                    double mZoomY = (double)ParentSize.Height / originalImageHeight;
                    double mZoomX = (double)ParentSize.Width / originalImageWidth;
                    mZoom = Math.Min(mZoomX, mZoomY);
                    break;
                case ZoomMode.FITWIDTH:
                    //float newWidth = returnWidth();
                    //if (newWidth <= 0) newWidth = 1;
                    mZoom = (double)ParentSize.Width / originalImageWidth;
                    break;
                default:
                    throw new Exception("Invalid zoom request!!");
            }
            //once the values have been calculated call the zoomimage function to redraw the image
            //with the new parameters 
            //  ZoomImage();

            return mZoom;
        }
        /// <summary>
        /// this method draws the image with the zoom amount as well as the center of the image specified
        /// </summary>
        /// <param name="z">the value by how much the image will be zoomed</param>
        /// <param name="pt">the center coordinate of the image in reference to the original coordinate system</param>
        public void ZoomImage(double z, Point pt)
        {
            //provided both the center and the zoom value 
            mZoom = z;

            Size ParentSize = GetParentSize();

            //build the source rectangle extracted from the original image
            Rectangle rect = new Rectangle(pt.X - (int)(ParentSize.Width / (2 * z)),
                pt.Y - (int)(ParentSize.Height / (2 * z)),
                (int)(ParentSize.Width / z),
                (int)(ParentSize.Height / z));

            //call the drawImage method to draw the selected image in the rectangle
            DrawImage(rect);
        }

        /// <summary>
        /// this is an overload that retains the center of the image and zooms the image with the value specified
        /// </summary>
        /// <param name="z">the value by how much the image will be zooomed</param>
        public void ZoomImage(double z)
        {
            //by chaning the zoom value and retaining the center value 
            //redraw the image
            mZoom = z;
            ZoomImage();
        }

        /// <summary>
        /// this is an over load that retains both the center of the image as well as the zoom value 
        /// mostly used to redraw the image, in cases like resizing the control
        /// </summary>
        public void ZoomImage()
        {
            //using the private members zoomvalue and display center 
            //compute the source rectangel from the original image
            Size ParentSize = GetParentSize();

            Rectangle rect = new Rectangle(displayCenter.X - (int)(ParentSize.Width / (2 * mZoom)),
                          displayCenter.Y - (int)(ParentSize.Height / (2 * mZoom)),
                          (int)((double)ParentSize.Width / mZoom),
                          (int)((double)ParentSize.Height / mZoom));

            //call the drawimage function to extract the selected rectangel
            DrawImage(rect);


        }

        public void ZoomImageWithZoomFactor(double mZoom)
        {
            //using the private members zoomvalue and display center 
            //compute the source rectangel from the original image
            Rectangle rect;
            if (originalImage != null)
            {
                double CurrentPictWidth = originalImageWidth * mZoom;
                double CurrentPictHeight = originalImageHeight * mZoom;
                Size ParentSize = GetPictureSize(mZoom); //GetParentSize();

                rect = new Rectangle((int)(((double)ParentSize.Width - CurrentPictWidth) / 2.0),
                                                (int)(((double)ParentSize.Height - CurrentPictHeight) / 2.0),
                                                (int)(((double)CurrentPictWidth)),
                                                (int)(((double)CurrentPictHeight))
                                                );

                //(int)(ParentSize.Width / (2 * mZoom)),
                //displayCenter.Y - (int)(ParentSize.Height / (2 * mZoom)),
                //(int)((double)ParentSize.Width / mZoom),
                //(int)((double)ParentSize.Height / mZoom));
            }
            else
            {
                Size ParentSize = GetParentSize();

                rect = new Rectangle(displayCenter.X - (int)(ParentSize.Width / (2 * mZoom)),
                              displayCenter.Y - (int)(ParentSize.Height / (2 * mZoom)),
                              (int)((double)ParentSize.Width / mZoom),
                              (int)((double)ParentSize.Height / mZoom));

            }
            DrawImage(rect);

            //call the drawimage function to extract the selected rectangel



        }
        public double GetZoom()
        {
            return mZoom;

        }

        public void SetZoom(double value)
        {
            mZoom = value;
        }

        public void AssignIMG()
        {
            pic.Image = displayImage;
        }
        private bool bRetriveImageControl = false;
        private static gloScanImaging.ImageControl _myImagePanel;
        private gloScanImaging.ImageControl myImagePanel
        {
            get
            {
                if (bRetriveImageControl)
                {
                    return _myImagePanel;
                }
                try
                {
                    _myImagePanel = pic.Parent.Parent as gloScanImaging.ImageControl;
                }
                catch
                {
                }
                if (_myImagePanel != null)
                {
                    bRetriveImageControl = true;
                }
                return _myImagePanel;
            }
        }
        public Point PictureScrollPos
        {
            get
            {
                gloScanImaging.ImageControl tempPanel = myImagePanel;//pic.Parent.Parent as gloScanImaging.ImageControl;

                return new Point(tempPanel.AutoScrollPosition.X, tempPanel.AutoScrollPosition.Y);
            }
            set
            {
                gloScanImaging.ImageControl tempPanel = myImagePanel; // pic.Parent.Parent as gloScanImaging.ImageControl;
                tempPanel.UpdateScreenOfControlWithout(false);
                tempPanel.AutoScrollPosition = new Point(-value.X, -value.Y);
                tempPanel.UpdateScreenOfControlWithout(true);
            }
        }


        /// <summary>
        /// a method used by the class to draw the final region of the the original image 
        /// </summary>
        /// <param name="rect">the requested region of the image</param>
        void DrawImage(Rectangle rect)
        {
            /*
             * this is the main function that actually does the drawing of the image
             * It takes the source rectangle which is to be displayed to the control
             * and draws this image on the displayedimage bitmap pbject
             */
            try
            {
                if (displayImage != null)
                {
                    using (Graphics gr = Graphics.FromImage(displayImage))
                    {
                        gr.FillRectangle(Brushes.White, new Rectangle(0, 0, displayImage.Width, displayImage.Height));
                        //draw the selected rectangel on to the displayedimage bitmap

                        //Rectangle DestRect = new Rectangle((int)((double)rect.X * mZoom),
                        //                                    (int)((double)rect.Y * mZoom),
                        //                                    (int)((double)rect.Width * mZoom),
                        //                                    (int)((double)rect.Height * mZoom));

                        if (originalImage != null)
                        {
                            //SLR: Changed by doing initialAdjustmentOnResolution
                            //Rectangle drawRect = rect;
                            //if ((originalImage.VerticalResolution != 0) && (originalImage.VerticalResolution != 0))
                            //{
                            //    if (originalImage.HorizontalResolution > originalImage.VerticalResolution)
                            //    {
                            //        int iNewWidth = (int)((float)rect.Width * (originalImage.VerticalResolution / originalImage.HorizontalResolution));
                            //        drawRect.X += (rect.Width - iNewWidth) / 2;
                            //        drawRect.Width = iNewWidth;
                            //    }
                            //    else
                            //    {
                            //        if (originalImage.HorizontalResolution < originalImage.VerticalResolution)
                            //        {
                            //            int iNewHeight = (int)((float)rect.Height * (originalImage.HorizontalResolution / originalImage.VerticalResolution));
                            //            drawRect.Y += (rect.Height - iNewHeight) / 2;
                            //            drawRect.Height = iNewHeight;
                            //        }
                            //    }
                            //}
                            gr.DrawImage(originalImage,
                                rect,//drawRect,//DestRect,//new Rectangle(0, 0, displayImage.Width, displayImage.Height),
                                new Rectangle(0, 0, originalImage.Width, originalImage.Height), //rect,
                                GraphicsUnit.Pixel);
                        }

                    }
                    //first draw a white back ground to clear all the drawings performed earlier


                }
                try
                {
                    if (pic != null)
                    {
                        pic.Image = displayImage;
                    }
                }
                catch
                {
                }
            }
            catch (Exception)
            {
                //WriteLog(" CODE LOG : " + ex.ToString());
            }

            // displayImage.Save(@"D:\RemoteScan\"+System.Guid.NewGuid().ToString() +".bmp",System.Drawing.Imaging.ImageFormat.Bmp);
            //save the values to history
            //AddDisplayLog();
        }

        public void SetCenterToScroll()
        {
            if ((displayImage != null) && (pic.Parent != null))
            {
                gloScanImaging.ImageControl tempPanel = myImagePanel; //pic.Parent.Parent as gloScanImaging.ImageControl;
                tempPanel.AutoScrollPosition = new Point((-tempPanel.ClientSize.Width / 2) + (displayImage.Width / 2), (-tempPanel.ClientSize.Height / 2) + (displayImage.Height / 2));
            }
        }


        /// <summary>
        /// a method that requests the next available image from the source image
        /// </summary>
        public void MoveToNextImage()
        {
            //check to see if the current index does not go out of bound
            if (mainImage.CurrentIndex < mainImage.ImageCount)
            {
                //if (originalImage != null)
                //{
                //    originalImage.Dispose();
                //}
                //retrieve the next image from the image source
                originalImage = mainImage.GetNextImage();
                AdjustResolution();
                //diplay the image to fit the display screen
                //ZoomImage(ZoomMode.FITPAGE);
            }
        }
        public void MoveToFirstImage()
        {

            //if (originalImage != null)
            //{
            //    originalImage.Dispose();
            //}
            //retrieve the next image from the image source
            originalImage = mainImage.GetPreviousImage();
            AdjustResolution();
            double LocZoom = mZoom;
            SetDisplayImage(ref LocZoom);
            //diplay the image to fit the display screen
            //ZoomImage(ZoomMode.FITPAGE);
        }
        /// <summary>
        /// a method that requests the previous image from the source image
        /// </summary>
        public void MoveToPreviousImage()
        {
            //check if the current index does not go out of bound
            if (mainImage.CurrentIndex > 0)
            {
                //if (originalImage != null)
                //{
                //    originalImage.Dispose();
                //}
                //retrieve the previous image from the image source object
                originalImage = mainImage.GetPreviousImage();
                AdjustResolution();
                //displaye the image to fit the diplay screen
                //ZoomImage(ZoomMode.FITPAGE);
            }
        }

        /// <summary>
        /// this method sets the source of the image collections that will be displayed:
        /// it could be any object that implements IZImage interface
        /// </summary>
        /// <param name="img">source object that implement IZImage</param>
        public void SetImageSource(IZImage img)
        {
            //sets the image source object
            try
            {
                if (displayImage != null)
                {
                    displayImage.Dispose();
                    displayImage = null;
                }

            }
            catch
            {
            }
            try
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                    originalImage = null;
                }

            }
            catch
            {
            }


            mainImage = img;

            //displays the first image in the source
            MoveToFirstImage();

        }

        /// <summary>
        /// this method rotated the image 
        /// </summary>
        /// <param name="ClockWise">specifies if its clockwise or the other</param>
        public void RotateImage(bool ClockWise)
        {
            if (originalImage != null)
            {
                if (ClockWise)
                { originalImage.RotateFlip(RotateFlipType.Rotate270FlipXY); }
                else
                { originalImage.RotateFlip(RotateFlipType.Rotate90FlipXY); }
                //originalImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                AdjustResolution();
                //ZoomImage(ZoomMode.FITPAGE);
            }
        }

        /// <summary>
        /// this is a private method that initializes the collections used to log preview values
        /// </summary>
        //private void InitializeHistory()
        //{
        //    //instantiate the collections used to store the zoom and center values
        //    zoomValues = new List<double>();
        //    centerValues = new List<Point>();
        //}

        /// <summary>
        /// a private method that logs the current display values into the collection
        /// its bahaves as:
        /// it inserts the current value and removes the final one if the capacity is full
        /// </summary>
        //private void AddDisplayLog()
        //{
        //    //insert the last action performed to the first index of the list
        //    centerValues.Insert(0, displayCenter);
        //    zoomValues.Insert(0, mZoom);

        //    //if the list has exceeded its capacity then remove the last item from each of the collections
        //    if (centerValues.Count > HISTORYCAPACITY)
        //    {
        //        centerValues.RemoveAt(HISTORYCAPACITY);
        //        zoomValues.RemoveAt(HISTORYCAPACITY);
        //    }
        //}

        private void InitializeComponent()
        {


            this.pic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();



            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pic.ForeColor = System.Drawing.SystemColors.Control;
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(50, 50);
            this.pic.TabIndex = 0;
            this.pic.TabStop = true;
            //this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            //this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            //this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            this.pic.SizeChanged += new System.EventHandler(this.pic_SizeChanged);
            this.pic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_MouseClick);
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            this.Controls.Add(this.pic);
            this.Size = new System.Drawing.Size(67, 67);

            //((System.ComponentModel.ISupportInitialize)(this.hScroll)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.vScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();

            this.ResumeLayout(false);
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor,
          true);

        }

        #endregion

        #region Constructors

        /// <summary>
        /// initialilzes the default values of the control
        /// </summary>
        public ImageViewer()
        {
            InitializeComponent();
            mZoom = 1;
            previewMode = PreviewMode.NONE;
            if (displayImage != null)
            {
                displayImage.Dispose();
                displayImage = null;
            }
            Size DisplaySize = GetPictureSize(mZoom);//GetDisplaySize();
            DisplaySize = GetAdjustedSize(DisplaySize);
            displayImage = new Bitmap(DisplaySize.Width, DisplaySize.Height);
            pic.Image = displayImage; //SLR: initialize picturebox image
            //InitializeHistory();
        }

        public Size GetPictureSize(double mzoom)
        {
            Size _ParentSize = GetParentSize();

            int iWidth = _ParentSize.Width;
            int iHeight = _ParentSize.Height;

            if (originalImage != null)
            {
                iWidth = (int)((double)originalImageWidth * mzoom);
                iHeight = (int)((double)originalImageHeight * mzoom);
            }

            iWidth = Math.Max(_ParentSize.Width, iWidth);
            iHeight = Math.Max(_ParentSize.Height, iHeight);
            return new Size(iWidth, iHeight);

        }

        private System.Drawing.Size GetAdjustedSize(Size DisplaySize)
        {
            double mZoom = ImageControl.getAdjustedZoomForImage(DisplaySize);
            int width = (int)((double)DisplaySize.Width * mZoom);
            int height = (int)((double)DisplaySize.Height * mZoom);
            if (width <= 0)
            {
                width = 1;
            }
            if (height <= 0)
            {
                height = 1;
            }
            return new Size(width, height);
        }

        private Size GetDisplaySize()
        {
            int iWidth = this.Width;
            int iHeight = this.Height;
            if (originalImage != null)
            {
                iWidth = (int)((double)originalImageWidth * mZoom);
                iHeight = (int)((double)originalImageHeight * mZoom);
            }
            iWidth = Math.Max(this.Width, iWidth);
            iHeight = Math.Max(this.Height, iHeight);

            Size ParentSize = GetParentSize();
            double zoomX = (double)iWidth / (double)ParentSize.Width;
            double zoomY = (double)iHeight / (double)ParentSize.Height;
            double zoom = Math.Max(zoomX, zoomY);
            return new Size((int)((double)ParentSize.Width * zoom), (int)((double)ParentSize.Height * zoom));

        }
        /// <summary>
        /// initilizes the image control object with the source provided
        /// </summary>
        /// <param name="img">source image</param>
        public ImageViewer(IZImage img)
            : this()
        {
            SetImageSource(img);
        }

        #endregion

        #region event handlers

        private void pic_SizeChanged(object sender, EventArgs e)
        {
            //when the size of the image is changed then the size of the bitmap 
            //used to draw the selected image will change 
            if (CurrSelectedMode != ZoomMode.PERCENTAGE)
            {
                mZoom = GetZoomValue(CurrSelectedMode);
            }

            double LocZoom = mZoom;

            SetDisplayImage(ref LocZoom);
            //redraw the image if there exists the image
            if (originalImage != null)
            {
                ZoomImage(mZoom, displayCenter);
            }
        }

        public void SetDisplayImage(ref double mZoom)
        {
            if (displayImage != null)
            {
                displayImage.Dispose();
                displayImage = null;
            }
            Size DisplaySize = GetPictureSize(mZoom); //GetDisplaySize();
            double dReduceZoom = ImageControl.getAdjustedZoomForImage(DisplaySize);

            //if ((DisplaySize.Width * DisplaySize.Height) > ImageControl.iMaxBitmapSize)
            //{
            //    dReduceZoom = Math.Sqrt((double)ImageControl.iMaxBitmapSize / ((double)DisplaySize.Width * (double)DisplaySize.Height));
            //}
            mZoom *= dReduceZoom;
            int width = (int)((double)DisplaySize.Width * dReduceZoom);
            int height = (int)((double)DisplaySize.Height * dReduceZoom);
            if (width <= 0)
            {
                width = 1;
            }
            if (height <= 0)
            {
                height = 1;
            }
            displayImage = new Bitmap(width, height);
            pic.Image = displayImage;
        }

        public void UnloadDisplayImage()
        {
            if (displayImage != null)
            {
                displayImage.Dispose();
                displayImage = null;
            }
            Size DisplaySize = GetPictureSize(1.0);
            DisplaySize = GetAdjustedSize(DisplaySize);
            displayImage = new Bitmap(DisplaySize.Width, DisplaySize.Height);

            pic.Image = displayImage;

        }


        //private void pic_MouseDown(object sender, MouseEventArgs e)
        //{
        //    //in cases where the display mode is pan or region select
        //    //there is a need to retain the first point of the mouse
        //    //this coordinate is then stored in the temporary variable 
        //    switch (previewMode)
        //    {
        //        case PreviewMode.REGIONSELECTION:
        //        case PreviewMode.PAN:
        //            tempCenter = e.Location;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //private void pic_MouseMove(object sender, MouseEventArgs e)
        //{
        //    //when a mouse is down and moving 
        //    //panning and region selection are posibilities
        //    switch (previewMode)
        //    {
        //        case PreviewMode.REGIONSELECTION:
        //            if (e.Button==MouseButtons.Left)
        //            {
        //                //display a rectangular region to the selected region 
        //                //to notify the user what he is selecting
        //                int w = Math.Abs(tempCenter.X - e.X), h = Math.Abs(tempCenter.Y - e.Y);
        //                if (w > 1 || h > 1)
        //                {
        //                    this.Refresh();
        //                    Graphics gr = pic.CreateGraphics();
        //                    gr.DrawString("(" + (tempCenter.X + e.X) / 2 + "," + (tempCenter.Y + e.Y) / 2 + ")", this.Font, Brushes.Khaki, new PointF((tempCenter.X + e.X) / 2, (tempCenter.Y + e.Y) / 2));
        //                    gr.DrawRectangle(Pens.Red, new Rectangle((tempCenter.X + e.X - w) / 2, (tempCenter.Y + e.Y - h) / 2, w, h));
        //                    gr.Dispose();
        //                }
        //            }
        //            break;
        //        case PreviewMode.PAN:
        //            if (e.Button==MouseButtons.Left&&(tempCenter.X != e.X || tempCenter.Y != e.Y))
        //            {
        //                displayCenter = new Point(displayCenter.X + (int)((tempCenter.X- e.X ) / mZoom),
        //                    displayCenter.Y + (int)((tempCenter.Y-e.Y) / mZoom));
        //                ZoomImage();
        //                tempCenter = e.Location;
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //private void pic_MouseUp(object sender, MouseEventArgs e)
        //{
        //    //when the mouse is up its when most of the previews are commited
        //    switch (previewMode)
        //    {
        //        case PreviewMode.REGIONSELECTION:
        //            displayCenter = new Point(displayCenter.X + (int)(((double)(tempCenter.X + e.X - this.Width) / 2) / mZoom),
        //                displayCenter.Y + (int)(((double)(tempCenter.Y + e.Y - this.Height) / 2) / mZoom));

        //            double z = mZoom * pic.Width / Math.Abs(tempCenter.X - e.X);
        //            if (mZoom * pic.Height / Math.Abs(tempCenter.Y - e.Y) < z)
        //                z = mZoom * pic.Height / Math.Abs(tempCenter.Y - e.Y);
        //            mZoom = z;
        //            ZoomImage();
        //            break;
        //        case PreviewMode.ZOOMIN:
        //            //when zoom in the zoom value is simply multiplied by two
        //            displayCenter=new Point(displayCenter.X+(int)((e.X-this.Width/2)/mZoom),
        //                displayCenter.Y+(int)((e.Y-this.Height/2)/mZoom));
        //            mZoom *= 2;
        //            //redraw the image with the new zoom value
        //            ZoomImage();
        //            break;
        //        case PreviewMode.ZOOMOUT:
        //            //when zoom out the zoom value is simply divided by two
        //            displayCenter = new Point(displayCenter.X + (int)((e.X - this.Width / 2) / mZoom),
        //                displayCenter.Y + (int)((e.Y - this.Height / 2) / mZoom));
        //            mZoom /= 2;
        //            //redraw the image with the new zoom value
        //            ZoomImage();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            gloScanImaging.ImageControl tempPanel = myImagePanel; //pic.Parent.Parent as gloScanImaging.ImageControl;
            Point prevAutoScrollPosition = tempPanel.AutoScrollPosition;
            tempPanel.Focus();
            tempPanel.AutoScrollPosition = new Point(-prevAutoScrollPosition.X, -prevAutoScrollPosition.Y);
        }
        #endregion

        public void SetPictureSize(double dZoomVal)
        {
            Size DisplaySize = GetPictureSize(dZoomVal);
            this.pic.Size = GetAdjustedSize(DisplaySize);
        }

        private Point startPos;
        private bool bInDragMode = false;
        private Cursor CurCursor;

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            CurCursor = Cursor.Current;
            Cursor.Current = Cursors.Hand;   
            bInDragMode= true;
            startPos = new Point(e.X, e.Y);
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (bInDragMode)
            {
                //Point newPos = e.Location;
                if (((e.X - startPos.X) * (e.X - startPos.X)) + ((e.Y - startPos.Y) * (e.Y - startPos.Y)) > 10)
                {
                    Point startScrollPos = PictureScrollPos;
                    PictureScrollPos = new Point(startScrollPos.X + e.X - startPos.X, startScrollPos.Y + e.Y - startPos.Y);
                }
            }
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (bInDragMode)
            {
                Cursor.Current = CurCursor;   
                bInDragMode = false;
            }        
        }
    }

    /// <summary>
    /// enumerations that specifies the zoom modes available when ever an image is on display
    /// </summary>
    public enum ZoomMode
    {
        FITPAGE,        //displays the entire image in the diplay region
        FITWIDTH,       //fits the image in the width of the control
        FITHEIGHT,      //fits the image in the height of the control
        ACTUALSIZE,      //displays the acutal size of the image
        PERCENTAGE  //Combo Box Selected in  %
    }

    /// <summary>
    /// enumerations that specifies how the mouse behaves over the control
    /// </summary>
    public enum PreviewMode
    {
        REGIONSELECTION,    //enables the user to select a region to zoom
        ZOOMIN,             //enables the user to zoom in to a point
        ZOOMOUT,            //anables the user to zoom out to a point
        PAN,                //anables the user to grab and pan the image
        NONE
    }

    /// <summary>
    /// a signiture interface that when implemented by any object
    /// enables it to utilize the image viewer component
    /// </summary>
    public interface IZImage
    {
        //gets the number of images that are available for display
        int ImageCount { get; }
        //gets the current index of the image that is being displayed
        int CurrentIndex { get; }
        //retreives the next available image
        Image GetNextImage();
        //retrieves the previously displayed image
        Image GetPreviousImage();
    }
}
