using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DrawingControl
{
    public partial class gloDrawingPad : UserControl
    {
        public gloDrawingPad()
        {
            InitializeComponent();
            InitializeControl();
        }
        internal enum DrawMode
        {
            modeEmptyRectangle = 1,
            modeEmptyEllipse = 2,
            modeLine = 3,
            modeRubber = 4,
            modeFilledRectangle = 5,
            modeFilledEllipse = 6,
            modeStraightLine = 7
        }
        internal enum DirectionMode
        {
            modeupperlefttolowerright = 1,
            modeupperrighttolowerleft = 2,
            modelowerlefttoupperright = 3,
            modelowerrighttoupperleft = 4
        }
        int x0;
        int y0;
        int x;
        int y;
        int oldX;
        int oldY;
        private string sApplicationPath;
        private Graphics gB = null;
        private Graphics gBWhite = null;


        private bool mouseDown = false;
        private Color current_color = Color.Black;
        private int current_width;
        private int rubber_width = 5;
        // Rectangle rc;

        Image canvas = null;
        Image canvasWhite = null;

        Image myOriginalImage = null;

        private DrawMode dmode = DrawMode.modeLine;
        private Cursor cr = null;

        public delegate void DelClick(object sender, EventArgs e, bool blninsert);
        public event DelClick btnClick;

        public delegate void DPCloseForm(object sender, EventArgs e);
        public event DPCloseForm btnCloseForm;

        public delegate void CloseClick(object sender, EventArgs e);

        //private Color border;
        private Boolean blnflag = false;
        private Boolean blnDrawImage = false;
        private string imagepath = "";

        //private Graphics g;


      //  private DirectionMode dirmode;

        public Image GetImage
        {
            get
            {
                return canvas;
            }

        }
        public gloDrawingPad(string spath, string simagepath)
        {
            sApplicationPath = spath;
            imagepath = simagepath;
            InitializeComponent();
            InitializeControl();
        }


        public int PenWidth
        {
            get
            {
                return current_width;
            }
            set
            {
                current_width = value;
            }
        }

        public string gloApplicationPath
        {
            get { return sApplicationPath; }
            set { sApplicationPath = value; }
        }
        public string DrawingImagePath
        {
            get { return imagepath; }
            set { imagepath = value; }
        }
        public void mydispose()
        {
            //canvas = null;
            //picDrawing.Image = null;
            //picDrawing.Refresh();
            //gB.Dispose();
            //gB = null;
            //imagepath = "";

            if (gB != null)
            {
                gB.Dispose();
                gB = null;
            }
            if (gBWhite != null)
            {
                gBWhite.Dispose();
                gBWhite = null;
            }

            //if (g != null)
            //{
            //    g.Dispose();
            //    g = null;
            //}
            if (canvas != null)
            {
                canvas.Dispose();
                canvas = null;
            }
            if (canvasWhite != null)
            {
                canvasWhite.Dispose();
                canvasWhite = null;
            }
            if (myOriginalImage != null)
            {
                myOriginalImage.Dispose();
                myOriginalImage = null;
            }
            if (picDrawing != null)
            {
                if (picDrawing.Image != null)
                {
                    picDrawing.Image.Dispose();
                    picDrawing.Image = null;
                }

                //picDrawing.Refresh();
            }

            imagepath = "";
        }
     
        private Size orgCanvasSize = new Size(100,100);
        private void InitializeControl()
        {
            
            //check if cursor is available of type pen  
            if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
            {
                cr = new Cursor(sApplicationPath + "\\pencil.cur");
                picDrawing.Cursor = cr;

            }



            Size spicture = new Size(picDrawing.Width <= 0 ? 100 : picDrawing.Width, picDrawing.Height <= 0 ? 100 : picDrawing.Height);
            orgCanvasSize = spicture;
            // rc = picDrawing.DisplayRectangle;
            //check if image is available on the Clipboard
            if (imagepath != "")
            {
                //if (System.IO.File.Exists(sApplicationPath + "\\Temp\\" + imagepath))
                if (System.IO.File.Exists(imagepath))
                {
                    picDrawing.Dock = DockStyle.None;
                    picDrawing.SizeMode = PictureBoxSizeMode.AutoSize;

                    //picDrawing.Image = Image.FromFile(sApplicationPath + "\\Temp\\" + imagepath);
                    Image thisImage = Image.FromFile(imagepath);
                    picDrawing.Image = thisImage;
                    spicture = new Size(picDrawing.Width <= 0 ? 100 : picDrawing.Width, picDrawing.Height <= 0 ? 100 : picDrawing.Height);
                    canvas = new Bitmap(picDrawing.Image, spicture);


                    //canvas = picDrawing.Image;
                    blnflag = true;


                }
                //if image is not available on the Clipboard,then create a bitmap without image
                else
                {
                    //  spicture = new Size(picDrawing.Width == 0 ? picDrawing.Width : 100, picDrawing.Height == 0 ? picDrawing.Height : 100);
                    canvas = new Bitmap(spicture.Width, spicture.Height);
                    picDrawing.Image = new Bitmap(canvas, spicture);
                    blnflag = false;
                    picDrawing.Dock = DockStyle.Fill;

                }

            }
            else
            //if image is not available on the Clipboard,then create a bitmap without image
            {

                canvas = new Bitmap(spicture.Width, spicture.Height);
                picDrawing.Image = new Bitmap(canvas, spicture);
                picDrawing.Dock = DockStyle.Fill;
                blnflag = false;
            }
            canvasWhite = new Bitmap(spicture.Width, spicture.Height);
            gBWhite = Graphics.FromImage(canvasWhite);
            gBWhite.DrawRectangle(Pens.White, 0, 0, spicture.Width, spicture.Height);
            gBWhite.FillRectangle(Brushes.White, 0, 0, spicture.Width, spicture.Height);


            //create a graphics object for the Bitmap object name of this object is canvas
            gB = Graphics.FromImage(canvas);
            //g = picDrawing.CreateGraphics();


            //if image is not set for picturebox then clear the graphics object for the 
            //canvas object

            if (blnflag == false)
            {
                picDrawing.BackColor = Color.White;
                Color bckColor = picDrawing.BackColor;
                gB.Clear(bckColor);
            }
            //--
            myOriginalImage = (Image)canvas.Clone();
        }

        private void picDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Point p = new Point(e.X, e.Y);
                x0 = p.X;
                y0 = p.Y;

                oldX = e.X;
                oldY = e.Y;

                mouseDown = true;
                Graphics g = picDrawing.CreateGraphics();
                Pen myPen = null;
                switch (dmode)
                {
                    case DrawMode.modeRubber:
                        myPen = new Pen(Color.White, 10);
                        g.DrawLine(myPen, new Point(oldX, oldY), new Point(e.X, e.Y));
                        if (myPen != null)
                        {
                            myPen.Dispose();
                        }
                        break;
                    case DrawMode.modeLine:
                        myPen = new Pen(current_color, current_width);
                        g.DrawLine(myPen, new Point(oldX, oldY), new Point(e.X, e.Y));
                        if (myPen != null)
                        {
                            myPen.Dispose();
                        }
                        break;

                }

                g.Dispose();

            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {

            }
        }

        private void picDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!mouseDown) return;
                Point p = new Point(e.X, e.Y);
                x = p.X;
                y = p.Y;

                int cx = x - x0;
                int cy = y - y0;
                Int32 rctleft = 0, rcttop = 0, rctwidth = 0, rctheight = 0;
                switch (dmode)
                {
                    case DrawMode.modeEmptyRectangle:
                    case DrawMode.modeFilledRectangle:
                    case DrawMode.modeEmptyEllipse:
                    case DrawMode.modeFilledEllipse:
                    case DrawMode.modeStraightLine:
                        Graphics gx = picDrawing.CreateGraphics();
                        gx.DrawImage(canvas, 0, 0);
                        gx.Dispose();
                        break;
                }
                switch (dmode)
                {
                    case DrawMode.modeEmptyRectangle:
                    case DrawMode.modeFilledRectangle:
                        if ((x >= x0) && (y >= y0))
                        {
                            //dirmode = DirectionMode.modeupperlefttolowerright;
                            rctleft = x0;
                            rcttop = y0;
                            rctwidth = x - x0;
                            rctheight = y - y0;
                        }
                        else if ((x < x0) && (y > y0))
                        {
                           // dirmode = DirectionMode.modeupperrighttolowerleft;
                            rctleft = x;
                            rcttop = y0;
                            rctwidth = x0 - x;
                            rctheight = y - y0;
                        }
                        else if ((x > x0) && (y < y0))
                        {
                            //dirmode = DirectionMode.modelowerlefttoupperright;
                            rctleft = x0;
                            rcttop = y;
                            rctwidth = x - x0;
                            rctheight = y0 - y;
                        }
                        else if ((x < x0) && (y < y0))
                        {
                           // dirmode = DirectionMode.modelowerrighttoupperleft;
                            rctleft = x;
                            rcttop = y;
                            rctwidth = x0 - x;
                            rctheight = y0 - y;
                        }
                        break;
                }


                Graphics g = picDrawing.CreateGraphics();
                Pen pen = new Pen(current_color, current_width);
                SolidBrush obrush = new SolidBrush(current_color);

                switch (dmode)
                {
                    //empty rectangle
                    case DrawMode.modeEmptyRectangle:
                        {
                            g.DrawRectangle(pen, rctleft, rcttop, rctwidth, rctheight);

                            blnDrawImage = true;
                            break;
                        }

                    case DrawMode.modeEmptyEllipse:
                        {

                            g.DrawEllipse(pen, x0, y0, cx, cy);
                            blnDrawImage = true;

                            break;
                        }

                    //filled rectangle
                    case DrawMode.modeFilledRectangle:
                        {

                            g.DrawRectangle(pen, rctleft, rcttop, rctwidth, rctheight);
                            g.Dispose();
                            g = picDrawing.CreateGraphics();
                            g.FillRectangle(obrush, rctleft, rcttop, rctwidth, rctheight);
                            blnDrawImage = true;
                            break;
                        }
                    case DrawMode.modeFilledEllipse:
                        {

                            g.DrawEllipse(pen, x0, y0, cx, cy);
                            g.Dispose();
                            g = picDrawing.CreateGraphics();
                            g.FillEllipse(obrush, x0, y0, cx, cy);

                            blnDrawImage = true;

                            break;
                        }
                    case DrawMode.modeRubber:
                        {
                            Pen myPen = new Pen(Color.White, rubber_width);
                            g.DrawLine(myPen, new Point(oldX, oldY), new Point(e.X, e.Y));
                            gBWhite.DrawLine(myPen, new Point(oldX, oldY), new Point(e.X, e.Y));
                            myPen.Dispose();
                            makeTransparent();
                            //line is added by dipak 20090925 to solve Bug 4167 :Exam ->Drawing Pad :If you put in a picture, double click on it, 
                            //use only the eraser, hit insert.  The picture does not change. 
                            //The only way a change is kept is if something is drawn on the picture.
                            blnDrawImage = true;
                            //end line is added by dipak 20090925
                            break;
                        }
                    case DrawMode.modeLine:
                        {
                            g.DrawLine(pen, new Point(oldX, oldY), new Point(e.X, e.Y));
                            gBWhite.DrawLine(pen, new Point(oldX, oldY), new Point(e.X, e.Y));
                            makeTransparent();
                            blnDrawImage = true;
                            break;
                        }
                    case DrawMode.modeStraightLine:
                        {
                            g.DrawLine(pen, x0, y0, x, y);
                            blnDrawImage = true;
                            break;

                        }
                    default:
                        break;
                }

                oldX = e.X;
                oldY = e.Y;
                pen.Dispose();
                obrush.Dispose();
                g.Dispose();
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {

            }
        }

        private void picDrawing_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //the shape selected is draw to the bitmap object
                mouseDown = false;
                Int32 rctleft = 0, rcttop = 0, rctwidth = 0, rctheight = 0;
                int cx = x - x0;
                int cy = y - y0;
                Pen pen = new Pen(current_color, current_width);
                SolidBrush obrush = new SolidBrush(current_color);
                switch (dmode)
                {
                    //empty rectangle
                    case DrawMode.modeEmptyRectangle:
                        {
                            if ((x >= x0) && (y >= y0))
                            {
                                //dirmode = DirectionMode.modeupperlefttolowerright;
                                rctleft = x0;
                                rcttop = y0;
                                rctwidth = x - x0;
                                rctheight = y - y0;
                            }
                            else if ((x < x0) && (y > y0))
                            {
                               // dirmode = DirectionMode.modeupperrighttolowerleft;
                                rctleft = x;
                                rcttop = y0;
                                rctwidth = x0 - x;
                                rctheight = y - y0;
                            }
                            else if ((x > x0) && (y < y0))
                            {
                                //dirmode = DirectionMode.modelowerlefttoupperright;
                                rctleft = x0;
                                rcttop = y;
                                rctwidth = x - x0;
                                rctheight = y0 - y;
                            }
                            else if ((x < x0) && (y < y0))
                            {
                                //dirmode = DirectionMode.modelowerrighttoupperleft;
                                rctleft = x;
                                rcttop = y;
                                rctwidth = x0 - x;
                                rctheight = y0 - y;
                            }
                            //gB.DrawRectangle(pen, x0, y0, cx, cy);
                            gBWhite.DrawRectangle(pen, rctleft, rcttop, rctwidth, rctheight);

                            blnDrawImage = true;
                            break;
                        }

                    case DrawMode.modeEmptyEllipse:
                        {

                            gBWhite.DrawEllipse(pen, x0, y0, cx, cy);
                            blnDrawImage = true;

                            break;
                        }

                    //filled rectangle
                    case DrawMode.modeFilledRectangle:
                        {
                            if ((x >= x0) && (y >= y0))
                            {
                              //  dirmode = DirectionMode.modeupperlefttolowerright;
                                rctleft = x0;
                                rcttop = y0;
                                rctwidth = x - x0;
                                rctheight = y - y0;
                            }
                            else if ((x < x0) && (y > y0))
                            {
                                //dirmode = DirectionMode.modeupperrighttolowerleft;
                                rctleft = x;
                                rcttop = y0;
                                rctwidth = x0 - x;
                                rctheight = y - y0;
                            }
                            else if ((x > x0) && (y < y0))
                            {
                               // dirmode = DirectionMode.modelowerlefttoupperright;
                                rctleft = x0;
                                rcttop = y;
                                rctwidth = x - x0;
                                rctheight = y0 - y;
                            }
                            else if ((x < x0) && (y < y0))
                            {
                               // dirmode = DirectionMode.modelowerrighttoupperleft;
                                rctleft = x;
                                rcttop = y;
                                rctwidth = x0 - x;
                                rctheight = y0 - y;
                            }

                            gBWhite.DrawRectangle(pen, rctleft, rcttop, rctwidth, rctheight);
                            gBWhite.FillRectangle(obrush, rctleft, rcttop, rctwidth, rctheight);

                            //gB.DrawRectangle(pen, x0, y0, cx, cy);
                            //gB.FillRectangle(obrush, x0, y0, cx, cy);
                            blnDrawImage = true;
                            break;
                        }
                    case DrawMode.modeFilledEllipse:
                        {


                            gBWhite.DrawEllipse(pen, x0, y0, cx, cy);
                            gBWhite.FillEllipse(obrush, x0, y0, cx, cy);

                            blnDrawImage = true;

                            break;
                        }

                    case DrawMode.modeStraightLine:
                        {
                            gBWhite.DrawLine(pen, x0, y0, x, y);
                            blnDrawImage = true;
                            break;
                        }
                    case DrawMode.modeRubber:
                        {
                            Graphics gx = picDrawing.CreateGraphics();
                            gx.DrawImage(canvas, 0, 0);
                            gx.Dispose();
                            break;
                        }
                    case DrawMode.modeLine:
                        {
                            Graphics gx = picDrawing.CreateGraphics();
                            gx.DrawImage(canvas, 0, 0);
                            gx.Dispose();
                            break;
                        }
                    default:
                        break;
                }
                if (pen != null)
                {
                    pen.Dispose();
                    pen = null;
                }
                if (obrush != null)
                {
                    obrush.Dispose();
                    obrush = null;
                }

            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                makeTransparent();

            }
        }
        private void makeTransparent()
        {
            Image canvasTransparent = (Image)canvasWhite.Clone();
            System.Drawing.Bitmap myBmp = new Bitmap(canvasTransparent);
            myBmp.MakeTransparent(Color.White);
            if (gB != null)
            {
                gB.Dispose();
                gB = null;
            }
            if (canvas != null)
            {
                canvas.Dispose();
                canvas = null;
            }
            Point myPoint = new Point(0, 0);
            canvas = new Bitmap(myOriginalImage.Width, myOriginalImage.Height);
            gB = Graphics.FromImage(canvas);
            gB.DrawImage(myOriginalImage, myPoint);
            gB.DrawImage(myBmp, myPoint);
            if (myBmp != null)
            {
                myBmp.Dispose();
                myBmp = null;
            }
            if (canvasTransparent != null)
            {
                canvasTransparent.Dispose();
                canvasTransparent = null;
            }
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            try
            {
                // set draw mode to line
                dmode = DrawMode.modeLine;

            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }

            }
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            try
            {
                dmode = DrawMode.modeEmptyRectangle;

            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }

            }
        }

        private void btnEllipsis_Click(object sender, EventArgs e)
        {
            try
            {
                dmode = DrawMode.modeEmptyEllipse;
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }


            }
        }

        private void btnOpenfile_Click(object sender, EventArgs e)
        {
            try
            {
                dlgOpenFile.Title = "Select Image";
                dlgOpenFile.Filter = "Images Files(*.bmp,*.jpg,*.jpeg,*.gif)|*.bmp;*.jpg;*.jpeg;*.gif";
                dlgOpenFile.CheckFileExists = true;
                dlgOpenFile.Multiselect = false;
                dlgOpenFile.ShowHelp = false;
                dlgOpenFile.ShowReadOnly = false;
                DialogResult bresult = dlgOpenFile.ShowDialog(this);

                //load image from a file in to the picturebox
                //if (bresult == DialogResult.OK)
                //{

                //    picDrawing.Refresh();
                //    canvas = null;

                //    picDrawing.Dock = DockStyle.None;
                //    picDrawing.SizeMode = PictureBoxSizeMode.AutoSize;
                //    picDrawing.Image = Image.FromFile(dlgOpenFile.FileName);
                //    Size spicture = new Size(picDrawing.Width, picDrawing.Height);
                //    canvas = picDrawing.Image;

                //    //if the graphics object for the canvas is not cleared,clear it
                //    if (gB != null)
                //    {
                //        gB.Dispose();
                //        gB = null;
                //    }
                //    if (g != null)
                //    {
                //        g.Dispose();
                //        g = null;
                //    }
                //    g = picDrawing.CreateGraphics();  
                //    gB = Graphics.FromImage(canvas);

                //    blnflag = true;
                //    blnDrawImage = true;
                //}

                if (bresult == DialogResult.OK)
                {
                    mydispose();
                    /*
                                        if (gB != null)
                                        {
                                            gB.Dispose();
                                            gB = null;
                                        }
                                        //if (g != null)
                                        //{
                                        //    g.Dispose();
                                        //    g = null;
                                        //}
                                        if (canvas != null)
                                        {
                                            canvas.Dispose();
                                            canvas = null;
                                        }
                                        if (picDrawing != null)
                                        {
                                            if (picDrawing.Image != null)
                                            {
                                                picDrawing.Image.Dispose();
                                                picDrawing.Image = null;
                                            }

                                            //picDrawing.Refresh();
                                        }
                    */
                    picDrawing.Dock = DockStyle.None;
                    picDrawing.SizeMode = PictureBoxSizeMode.AutoSize;
                    imagepath = dlgOpenFile.FileName;

                    picDrawing.Image = Image.FromFile(imagepath);
                    Size spicture = new Size(picDrawing.Width <= 0 ? 100 : picDrawing.Width, picDrawing.Height <= 0 ? 100 : picDrawing.Height);

                    //UNCOMMENT COZ WE WANT TO CONVERT THAT INTO BITMAP OTHERWISE IT WILL GIVE AN EXCEPTION THAT A GRAPHICS OBJECT CAN NOT BE CREATED FROM AN IMAGE THAT HAS 
                    canvas = new Bitmap(picDrawing.Image, spicture);
                    //COMMENTED THIS
                    //canvas = picDrawing.Image;                  
                    gB = Graphics.FromImage(canvas);
                    //g = picDrawing.CreateGraphics();

                    blnflag = true;
                    blnDrawImage = true;
                    canvasWhite = new Bitmap(spicture.Width, spicture.Height);
                    gBWhite = Graphics.FromImage(canvasWhite);
                    gBWhite.DrawRectangle(Pens.White, 0, 0, spicture.Width, spicture.Height);
                    gBWhite.FillRectangle(Brushes.White, 0, 0, spicture.Width, spicture.Height);
                    //--
                    myOriginalImage = (Image)canvas.Clone();

                }
               //dlgOpenFile.Dispose();

            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //String strDrawingImageName = "";  
            try
            {
                //if (blnDrawImage == true)
                //{
                //insert  code has been commented
                ////if (imagepath != "")
                ////{
                ////    //canvas.Save(sApplicationPath + "\\Temp\\" + imagepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                ////    //picDrawing.SizeMode = PictureBoxSizeMode.AutoSize;   
                ////    //picDrawing.Image = Image.FromFile(sApplicationPath + "\\Temp\\" + imagepath);
                ////    //picDrawing.Image.Save(sApplicationPath + "\\Temp\\DrawingImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg); 

                ////    //save the image in the canvas to temporary file path
                ////    canvas.Save(sApplicationPath + "\\Temp\\DrawingImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                ////}
                ////else
                ////{
                ////    //save the image in the canvas to temporary file path
                ////    canvas.Save(sApplicationPath + "\\Temp\\DrawingImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                ////}
                //canvas.Save(sApplicationPath + "\\Temp\\DrawingImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //insert code has been commented

                //strDrawingImageName = sApplicationPath + "\\Temp\\DrawingImage" + DateTime.Now.ToString("MM dd yy hh mm ss") + ".jpg";

                if (blnDrawImage == true)
                {

                    // canvas.Save(strDrawingImageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    try
                    {

                        //Clipboard.Clear();
                        Clipboard.SetImage(canvas);
                    }
                    catch //(Exception Ex)
                    {
                    //   MessageBox.Show("Unable to get image from Clipboard", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                    }

                }
                //}
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                //clean up code has been commented
                //gB.Dispose();
                //if (btnClick != null)
                //{
                //    btnClick(sender, e);
                //}
                //clean up code has been commented
                mydispose();
                /*
                                if (gB != null)
                                {
                                    gB.Dispose();
                                    gB = null;
                                }
                                if (g != null)
                                {
                                    g.Dispose();
                                    g = null;
                                }
                                if (canvas != null)
                                {
                                    canvas.Dispose();
                                    canvas = null;
                                }
                                if (picDrawing != null)
                                {
                                    if (picDrawing.Image != null)
                                    {
                                        picDrawing.Image.Dispose();
                                        picDrawing.Image = null;
                                    }

                                    //picDrawing.Refresh();
                                }
                 */
                if (btnClick != null)
                {
                    btnClick(sender, e, blnDrawImage);
                }

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //clean up code has been commented
                ////Refresh the picturebox
                //picDrawing.Image = null;
                //picDrawing.Refresh();
                //gB.Dispose();

                ////clear the canvas object
                //canvas.Dispose();
                //canvas = null;
                //clean up code has been commented
                mydispose();
                /*
                                if (gB != null)
                                {
                                    gB.Dispose();
                                    gB = null;
                                }
                                if (canvas != null)
                                {
                                    canvas.Dispose();
                                    canvas = null;
                                }
                                if (picDrawing != null)
                                {
                                    if (picDrawing.Image != null)
                                    {
                                        picDrawing.Image.Dispose();
                                        picDrawing.Image = null;
                                    }

                                   // picDrawing.Refresh();
                                }
                */
                Size spicture = new Size(orgCanvasSize.Width <= 0 ? 100 : orgCanvasSize.Width, orgCanvasSize.Height <= 0 ? 100 : orgCanvasSize.Height);
                picDrawing.Image =  new Bitmap(spicture.Width, spicture.Height);
                canvas = new Bitmap(picDrawing.Image, spicture);

                picDrawing.Dock = DockStyle.Fill;
                blnflag = false;
                canvasWhite = new Bitmap(spicture.Width, spicture.Height);
                gBWhite = Graphics.FromImage(canvasWhite);
                gBWhite.DrawRectangle(Pens.White, 0, 0, spicture.Width, spicture.Height);
                gBWhite.FillRectangle(Brushes.White, 0, 0, spicture.Width, spicture.Height);
                blnDrawImage = false;


                //create a graphics object for the Bitmap object name of this object is canvas
                gB = Graphics.FromImage(canvas);
                //g = picDrawing.CreateGraphics();


                //if image is not set for picturebox then clear the graphics object for the 
                //canvas object

                if (blnflag == false)
                {
                    picDrawing.BackColor = Color.White;
                    Color bckColor = picDrawing.BackColor;
                    gB.Clear(bckColor);
                }
                //--
                myOriginalImage = (Image)canvas.Clone();

                //picDrawing.Dock = DockStyle.Fill;
                //picDrawing.SizeMode = PictureBoxSizeMode.Normal;
                ////set the canvas object 
                //canvas = new Bitmap(picDrawing.Width, picDrawing.Height);
                ////generate graphics object for the canvas
                //picDrawing.Image = canvas;
                //gB = Graphics.FromImage(canvas);
                ////g = picDrawing.CreateGraphics();

                //blnflag = false;
                //blnDrawImage = false;
                //picDrawing.BackColor = Color.White;
                //Color bckColor = picDrawing.BackColor;
                //gB.Clear(bckColor);
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {

                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }

            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            try
            {
                //change the color of the pen
               // ColorDialog colDlg = new ColorDialog();
                try
                {
                    colorDialog1.CustomColors = gloGlobal.gloCustomColor.customColor;
                }
                catch
                {
                }
                if (colorDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    current_color = colorDialog1.Color;
                    try
                    {
                        gloGlobal.gloCustomColor.customColor = colorDialog1.CustomColors;
                    }
                    catch
                    {
                    }
                    //border = colDlg.Color;
                }
                //colDlg.Dispose();
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {

            }
        }

        private void btnFillRectangle_Click(object sender, EventArgs e)
        {
            try
            {
                //set draw mode to filled rectangle
                dmode = DrawMode.modeFilledRectangle;
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }


            }
        }

        private void btnFilledEllipse_Click(object sender, EventArgs e)
        {
            try
            {
                //set draw mode to filled ellipse
                dmode = DrawMode.modeFilledEllipse;
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }
            }
        }

        private void tlbdrp_PenWidth_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                //change the width of pen based on value selected in the pen width drop down


                foreach (ToolStripMenuItem otlp in tlbdrb_PenWidth.DropDownItems)
                {

                    otlp.Checked = false;

                }
                ToolStripMenuItem t = (ToolStripMenuItem)e.ClickedItem;
                t.Checked = true;
                current_width = Convert.ToInt32(e.ClickedItem.Text);
                if(dmode != DrawMode.modeLine )
                {dmode = DrawMode.modeLine;}
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }

            }
        }

        private void tlbdrp_eraser_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                foreach (ToolStripMenuItem otlp in tlbdrp_eraser.DropDownItems)
                {

                    otlp.Checked = false;

                }
                ToolStripMenuItem t = (ToolStripMenuItem)e.ClickedItem;
                t.Checked = true;

            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult messageResult;

            //solving salesforce case-GLO2010-0005441
            if (blnDrawImage == true)
            {

                messageResult = MessageBox.Show("Do you want to insert the image?", "gloEMR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (messageResult == DialogResult.Yes)
                {
                    btnInsert_Click(sender, e);
                    mydispose();
                    /*
                                    if (gB != null)
                                {
                                    gB.Dispose();
                                    gB = null;
                                }
                                if (g != null)
                                {
                                    g.Dispose();
                                    g = null;
                                }
                                if (canvas != null)
                                {
                                    canvas.Dispose();
                                    canvas = null;
                                }
                                if (picDrawing != null)
                                {
                                    if (picDrawing.Image != null)
                                    {
                                        picDrawing.Image.Dispose();
                                        picDrawing.Image = null;
                                    }

                                }
                     */
                    btnClick(sender, e, blnDrawImage);

                }


                if (messageResult == DialogResult.No)
                {

                    mydispose();
                    /*
                                    if (gB != null)
                                    {
                                        gB.Dispose();
                                        gB = null;
                                    }
                                    if (g != null)
                                    {
                                        g.Dispose();
                                        g = null;
                                    }
                                    if (canvas != null)
                                    {
                                        canvas.Dispose();
                                        canvas = null;
                                    }
                                    if (picDrawing != null)
                                    {
                                        if (picDrawing.Image != null)
                                        {
                                            picDrawing.Image.Dispose();
                                            picDrawing.Image = null;
                                        }

                                    }
                    */
                    btnCloseForm(sender, e);

                }



            }
            else
            {
                mydispose();
                btnCloseForm(sender, e);
            }

        }




        private void btnstrline_Click(object sender, EventArgs e)
        {

            try
            {
                dmode = DrawMode.modeStraightLine;

            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\pencil.cur");
                    picDrawing.Cursor = cr;

                }

            }
        }

        private void tlpEraser1_Click(object sender, EventArgs e)
        {
            try
            {
                //change rubber size
                dmode = DrawMode.modeRubber;
                rubber_width = 5;
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\rubbermini.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\rubbermini.cur");
                    picDrawing.Cursor = cr;

                }
            }
        }

        private void tlpEraser2_Click(object sender, EventArgs e)
        {
            try
            {
                //change rubber size
                dmode = DrawMode.modeRubber;
                rubber_width = 20;
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\rubbermed.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\rubbermed.cur");
                    picDrawing.Cursor = cr;

                }
            }
        }

        private void tlpEraser3_Click(object sender, EventArgs e)
        {
            try
            {
                //change rubber size
                dmode = DrawMode.modeRubber;
                rubber_width = 80;
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
            finally
            {
                if (System.IO.File.Exists(sApplicationPath + "\\rubber.cur") == true)
                {
                    if (cr != null)
                    {
                        cr.Dispose();
                        cr = null;
                    }
                    cr = new Cursor(sApplicationPath + "\\rubber.cur");
                    picDrawing.Cursor = cr;

                }
            }
        }


    }
}
