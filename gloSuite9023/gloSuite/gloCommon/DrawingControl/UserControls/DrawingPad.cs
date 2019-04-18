using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using gloGDI;

namespace DrawingControl
{
    public partial class DrawingPad : UserControl
    {
        internal enum DrawMode
        {
            modeEmptyRectangle = 1,
            modeEmptyEllipse = 2,
            modeLine = 3,
            modeRubber = 4,
            modeFilledRectangle = 5,
            modeFilledEllipse = 6,
            modeStraightLine=7
        }
       // protected Graphics pg1 = null;
        protected TextureBrush staticTextureBrush = null;
        /// <summary>
        /// Protected variable to hold the 
        /// erasing pen for the object being drawn.
        /// </summary>
        protected Pen staticPenRubout = null;
        /// <summary>
        /// 
        private string sApplicationPath;
        private Graphics g = null;
        private Graphics gpic = null;
        private int oldX = 0;
        private int oldY = 0;
        private int originX = 0;
        private int originY = 0;
        private bool mouseDown = false;

        private Color current_color = Color.Blue;
        private int current_width = 5;
        private int rubber_width = 15;
        Rectangle rc;
        Image canvas=null;
        private DrawMode dmode = DrawMode.modeLine;
        private Cursor cr = null;
        //private Boolean blninsert=false;
        public delegate void DelClick(object sender, EventArgs e);
        public event DelClick btnClick;
        private Color fill;
        private Color border;
        private Point start;
        //private Rectangle rct;
        private System.Drawing.Drawing2D.GraphicsPath mypath = new System.Drawing.Drawing2D.GraphicsPath();
      //  private Boolean  blnflag=false;
        private Boolean blnDrawImage = false;
        private string imagepath="";
        protected Point p1 = new Point(0, 0);
        protected Point p2 = new Point(0, 0);

        public Image GetImage
        {
            get 
            {
                return canvas;
            }
           
        }      

        public DrawingPad(string spath,string simagepath)
        {
            sApplicationPath = spath;
            imagepath = simagepath;
            InitializeComponent();
            InitializeControl();
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
            if (canvas != null)
            {
                canvas.Dispose();
                canvas = null;
            }
            if (picDrawing.Image != null)
            {
                picDrawing.Image.Dispose();
                picDrawing.Image = null;
            }   
            //picDrawing.Refresh();
            if (g != null)
            {
                g.Dispose();
                g = null;
            }
            if (gpic != null)
            {
                gpic.Dispose();
                gpic = null;
            }
            imagepath = "";
            if (staticTextureBrush != null)
            {
                staticTextureBrush.Dispose();
                staticTextureBrush = null; 
            }
            if (staticPenRubout != null)
            {
                staticPenRubout.Dispose();
                staticPenRubout = null;
            } 
            
        }
        private void InitializeControl()
        {
            //check if cursor file available ,if so change the cursor of picturebox
            if (System.IO.File.Exists(sApplicationPath + "\\pencil.cur") == true)
            {
                if (cr != null)
                {
                    cr.Dispose();
                    cr = null;
                }
                cr = new Cursor(sApplicationPath + "\\pencil.cur");
                if (cr != null)
                {
                    picDrawing.Cursor = cr;
                }

            }
         
            rc = picDrawing.DisplayRectangle;

            //check if imagepath exists
            //load the picturebox with image from the imagepath
            if (imagepath != "")
            {
                if (System.IO.File.Exists(sApplicationPath + "\\Temp\\" + imagepath))
                {
                    picDrawing.Dock = DockStyle.None;
                    picDrawing.SizeMode = PictureBoxSizeMode.AutoSize;
                    if (picDrawing.Image != null)
                    {
                        picDrawing.Image.Dispose();
                        picDrawing.Image = null;
                    }
                    picDrawing.Image = Image.FromFile(sApplicationPath + "\\Temp\\" + imagepath);
                    //canvas = picDrawing.Image;
                   // blnflag = true;

                }
                    //if image path not available do not load the picturebox also set the dock
                    //style of the picturebox to none.
                else
                {
                    //canvas = new Bitmap(picDrawing.Width, picDrawing.Height);                    
                    
                   // blnflag = false;
                    picDrawing.Dock = DockStyle.Fill;
                    //picDrawing.Image=canvas;
                }

            }
            //if image path not available do not load the picturebox also set the dock
            //style of the picturebox to none.
            else
            {
                //canvas = new Bitmap(picDrawing.Width, picDrawing.Height);
                picDrawing.Dock = DockStyle.None;                
             //   blnflag = false;
                //picDrawing.Image = canvas;
            }

            
            //g = Graphics.FromImage(canvas);
            //gpic = picDrawing.CreateGraphics();
            
            tlbMain.ImageList = imageList1; 
            btnLine.ImageIndex = 14;
            btnRectangle.ImageIndex = 1;
            btnFillRectangle.ImageIndex = 2;
            btnEllipsis.ImageIndex = 3;
            btnFilledEllipse.ImageIndex = 4; 
            btnColor.ImageIndex = 5;
            btnClear.ImageIndex = 6;
            btnOpenfile.ImageIndex = 7;
            btnInsert.ImageIndex = 8;
            tlbdrp_eraser.ImageIndex = 9;
            btnstrline.ImageIndex =0;
            
            btnClose.ImageIndex = 10; 
            //btnWidth.ImageIndex = 8;
            border = Color.Blue;
            fill = Color.LightGray;
            for (int itemcount = 1; itemcount <= 100; itemcount++)
            {
                tlpcmb_Width.Items.Add(itemcount);   
            }
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            try
            {
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }


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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                }
                
              
            }
        }

        private void btnOpenfile_Click(object sender, EventArgs e)
        {
            try
            {
                dlgOpenFile.Title = "Select Clinic Logo";
                dlgOpenFile.Filter = "Images Files(*.bmp,*.jpg,*.jpeg,*.gif)|*.bmp;*.jpg;*.jpeg;*.gif";
                dlgOpenFile.CheckFileExists = true;
                dlgOpenFile.Multiselect = false;
                dlgOpenFile.ShowHelp = false;
                dlgOpenFile.ShowReadOnly = false;
                DialogResult bresult = dlgOpenFile.ShowDialog(this);
                if (bresult == DialogResult.OK)
                {
                    //blninsert = true;
                   
                    //btnRubber.Visible = false;
                    picDrawing.Refresh();
                   // canvas = null;
                    picDrawing.Dock = DockStyle.None;  
                    picDrawing.SizeMode = PictureBoxSizeMode.AutoSize;
                    if (picDrawing.Image != null)
                    {
                        picDrawing.Image.Dispose();
                        picDrawing.Image = null;
                    }
                    picDrawing.Image = Image.FromFile(dlgOpenFile.FileName);
                    if (canvas != null)
                    {
                        canvas.Dispose();
                        canvas = null;
                    }
                    canvas = (Image)picDrawing.Image.Clone();

                    //picDrawing.Size = canvas.Size;
                    if (g != null)
                    {
                        g.Dispose();
                        g = null;
                    }
                    g = Graphics.FromImage(canvas);
                    if (gpic != null)
                    {
                        gpic.Dispose();
                        gpic = null;
                    }
                    gpic = picDrawing.CreateGraphics();
               //     blnflag = true;
                    blnDrawImage = false;
                    //Code Review Changes: Dispose Graphics object
                    gpic.Dispose();
                    gpic = null;
                }
                dlgOpenFile.Dispose();
            }
            catch (Exception ex)
            {
                DrawingControlException objex = new DrawingControlException(ex.Message);
                throw objex;
            }
        }
        public void RefreshScribble(Color newColor)
        {
            current_color = newColor;
            this.picDrawing.Refresh();
        }

        public void RefreshScribble(int newWidth)
        {
            current_width = newWidth;
            Refresh();
        }

        
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //old code to insert image in word object
            //try
            //{
            //    g.Dispose();
            //    gpic.Dispose();
            //    if (blnflag == false)
            //    {
            //        if (blnDrawImage == true)
            //        {
            //            if (canvas != null)
            //            {
            //                //MessageBox.Show("Image present");
            //                if (System.IO.Directory.Exists(sApplicationPath + "\\Temp") == true)
            //                {
            //                    //canvas = null;
            //                    //canvas = picDrawing.Image;
            //                    canvas.Save(sApplicationPath + "\\Temp\\DrawingImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //                }

            //            }
                        
            //        }
                    
            //    }
            //    else
            //    {
            //        if (picDrawing.Image!= null)
            //        {
            //            //MessageBox.Show("Image present");
            //            if (System.IO.Directory.Exists(sApplicationPath + "\\Temp") == true)
            //            {
            //                picDrawing.Image.Save(sApplicationPath + "\\Temp\\DrawingImage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //                picDrawing.Refresh();
            //                picDrawing.Image = null;
            //            }

            //        }
            //    }
               
            //}
            //catch (Exception ex)
            //{
            //    DrawingControlException objex = new DrawingControlException(ex.Message);
            //    throw objex;
            //}
            //finally
            //{

            //    if (btnClick != null)
            //    {
            //        btnClick(sender, e);
            //    }

            //}
            //old code to insert image in word object

            g.Dispose();
            g = null;
            gpic.Dispose();
            gpic = null;
            if (blnDrawImage == true)
            {
                try
                {
                    Graphics gpicture = picDrawing.CreateGraphics();
                    GDI.SavePicture(gpicture, picDrawing.Width, picDrawing.Height, sApplicationPath + "\\Temp\\DrawingImage.jpg");
                    //Code Review Changes: Dispose Graphics object
                    gpicture.Dispose();
                }
                catch (Exception ex)
                {
                    DrawingControlException objex = new DrawingControlException(ex.Message);
                    throw objex;
                }
                finally
                {

                    if (btnClick != null)
                    {
                        btnClick(sender, e);
                    }
                    if (gpic != null)
                    {
                        gpic.Dispose();
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //blninsert  = false;
                //btnRubber.Visible = true;
                if (picDrawing.Image != null)
                {
                    picDrawing.Image.Dispose();
                }
                picDrawing.Image = null;
                picDrawing.Refresh();
               // canvas = null;
                picDrawing.Dock = DockStyle.Fill;
                if (canvas != null)
                {
                    canvas.Dispose();
                    canvas = null;
                }
                canvas = new Bitmap(picDrawing.Width, picDrawing.Height);
                if (g != null)
                {
                    g.Dispose();
                    g = null;
                }
                g = Graphics.FromImage(canvas);
                if (gpic != null)
                {
                    gpic.Dispose();
                    gpic = null;
                }
                gpic = picDrawing.CreateGraphics();
                picDrawing.SizeMode = PictureBoxSizeMode.AutoSize;
              //  blnflag = false;
                blnDrawImage = false;
                //Code Review Changes: Dispose Graphics object
                gpic.Dispose();
                gpic = null;
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                }
                //Code Review Changes: Dispose Graphics object
                if (gpic != null)
                {
                    gpic.Dispose();
                    gpic = null;
                }
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            try
            {
              //  ColorDialog colDlg = new ColorDialog();
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
                    border = colorDialog1.Color;
                    this.RefreshScribble(current_color);
                    try
                    {
                        gloGlobal.gloCustomColor.customColor = colorDialog1.CustomColors;
                    }
                    catch
                    {
                    }
                }
                //colDlg.Dispose();
                //colDlg = null;
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                }


            }
        }

        private void btnFilledEllipse_Click(object sender, EventArgs e)
        {
            try
            {
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                }


            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnClick(sender,e);
        }

        private void picDrawing_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                mouseDown = false;
                //oldX = -1;
                //oldY = -1;
               
                p2 = new Point(e.X, e.Y);            
            
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

                if (g != null && gpic != null)
                {
                    switch (dmode)
                    {
                        //empty rectangle
                        case DrawMode.modeEmptyRectangle:
                            {
                                Graphics grect = picDrawing.CreateGraphics();
                                gpic.CompositingMode = CompositingMode.SourceOver;
                                gpic.CompositingQuality = CompositingQuality.GammaCorrected;

                                Pen pen1 =new Pen(border, (int)current_width);
                                //Pen pen2 = new Pen(border, (int)current_width);
                                GDI.DrawXORRectangle(grect, pen1 , new Rectangle(originX, originY, oldX - originX, oldY - originY));
                                GDI.DrawXORRectangle(grect, pen1, new Rectangle(originX, originY, e.X - originX, e.Y - originY));
                                oldX = e.X;
                                oldY = e.Y;
                                //Code Review Changes: Dispose Graphics object
                                grect.Dispose();
                                pen1.Dispose();
                                break;
                               
                            }  
                     
                        case DrawMode.modeEmptyEllipse:
                            {
                                Graphics grect = picDrawing.CreateGraphics();
                                Pen pen1 = new Pen(border, (int)current_width);
                                GDI.DrawXOREllipse(grect, pen1 , new Rectangle(originX, originY, oldX - originX, oldY - originY));
                                GDI.DrawXOREllipse(grect, pen1 , new Rectangle(originX, originY, e.X - originX, e.Y - originY));

                                oldX = e.X;
                                oldY = e.Y;

                                //rct = new Rectangle(start.X, start.Y, e.X - start.X, e.Y - start.Y);
                                blnDrawImage = true;
                                //Code Review Changes: Dispose Graphics object
                                grect.Dispose();
                                pen1.Dispose();
                                break;
                            }

                        //filled rectangle
                        case DrawMode.modeFilledRectangle:
                            {
                                Graphics grect = picDrawing.CreateGraphics();
                                Pen pen1=new Pen(border, (int)current_width);
                                GDI.DrawXORFilledRectangle(grect,pen1 , new Rectangle(originX, originY, oldX - originX, oldY - originY));
                                GDI.DrawXORFilledRectangle(grect, pen1, new Rectangle(originX, originY, e.X - originX, e.Y - originY));
                                oldX = e.X;
                                oldY = e.Y;
                                //rct = new Rectangle(start.X, start.Y, e.X - start.X, e.Y - start.Y);
                                blnDrawImage = true;
                                //Code Review Changes: Dispose Graphics object
                                grect.Dispose();
                                pen1.Dispose();
                                break;
                            }
                        case DrawMode.modeFilledEllipse:
                            {
                                Graphics grect = picDrawing.CreateGraphics();
                                Pen pen1 = new Pen(border, (int)current_width);
                                GDI.DrawXORFilledEllipse(grect, pen1, new Rectangle(originX, originY, oldX - originX, oldY - originY));
                                GDI.DrawXORFilledEllipse(grect, pen1, new Rectangle(originX, originY, e.X - originX, e.Y - originY));
                                oldX = e.X;
                                oldY = e.Y;

                                //rct = new Rectangle(start.X, start.Y, e.X - start.X, e.Y - start.Y);
                                blnDrawImage = true;
                                //Code Review Changes: Dispose Graphics object
                                grect.Dispose();
                                pen1.Dispose();
                                break;
                            }
                        case DrawMode.modeRubber:
                            {
                                Pen pen1= new Pen(Color.White, rubber_width);
                                g.DrawLine(pen1, new Point(oldX, oldY), new Point(e.X, e.Y));
                                gpic.DrawLine(pen1, new Point(oldX, oldY), new Point(e.X, e.Y));
                                pen1.Dispose();
                                break;
                            }
                        case DrawMode.modeLine:
                            {
                                Pen pen1 = new Pen(current_color, current_width);
                                g.DrawLine(pen1, new Point(oldX, oldY), new Point(e.X, e.Y));
                                gpic.DrawLine(pen1, new Point(oldX, oldY), new Point(e.X, e.Y));
                                blnDrawImage = true;
                                pen1.Dispose();
                                break;
                            }
                        case DrawMode.modeStraightLine:
                            {
                                Graphics grect = picDrawing.CreateGraphics();
                                Pen pen1=new Pen(current_color, current_width);
                                GDI.DrawXORLine(grect,pen1 , originX, originY, oldX, oldY);
                                GDI.DrawXORLine(grect, pen1, originX, originY, e.X, e.Y); ;
                                oldX = e.X;
                                oldY = e.Y;
                                blnDrawImage = true;
                                //Code Review Changes: Dispose Graphics object
                                grect.Dispose();
                                pen1.Dispose();
                                break;
                                //g.DrawLine(new Pen(border, (int)current_width), start, new Point(e.X, e.Y));
                                //gpic.DrawLine(new Pen(border, (int)current_width), start, new Point(e.X, e.Y));
                                
                                
                            }
                        default:
                            break;
                    }

                }

                oldX = e.X;
                oldY = e.Y;
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

        private void picDrawing_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (mouseDown == true)
                {
                    if (g != null && gpic != null)
                    {
                        //switch (dmode)
                        //{

                        //    case DrawMode.modeRubber:
                        //        g.DrawLine(new Pen(Color.White, 10), new Point(rc.X, rc.Y), new Point(rc.Width, rc.Height));
                        //        gpic.DrawLine(new Pen(Color.White, 10), new Point(rc.X, rc.Y), new Point(rc.Width, rc.Height));
                        //        break;

                        //    case DrawMode.modeLine:
                        //        g.DrawLine(new Pen(current_color, current_width), new Point(rc.X, rc.Y), new Point(rc.Width, rc.Height));
                        //        gpic.DrawLine(new Pen(current_color, current_width), new Point(rc.X, rc.Y), new Point(rc.Width, rc.Height));
                        //        break;
                           
                            

                        //}
                    }
                }

                picDrawing.Focus();
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

        private void picDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                oldX = e.X;
                oldY = e.Y;
                originX = e.X;
                originY = e.Y;
                
                p1 = new Point(e.X, e.Y);
                
                mouseDown = true;
                start = new Point(e.X, e.Y);
                if (g != null && gpic != null)
                {

                    switch (dmode)
                    {
                        case DrawMode.modeRubber:
                            Pen pen1=new Pen(Color.White, 10);
                            g.DrawLine(pen1, new Point(oldX, oldY), new Point(e.X, e.Y));
                            gpic.DrawLine(pen1, new Point(oldX, oldY), new Point(e.X, e.Y));
                            pen1.Dispose();
                            break;
                        case DrawMode.modeLine:
                            Pen pen2=new Pen(current_color, current_width);
                            g.DrawLine(pen2, new Point(oldX, oldY), new Point(e.X, e.Y));
                            gpic.DrawLine(pen2, new Point(oldX, oldY), new Point(e.X, e.Y));
                            pen2.Dispose();
                            break;                    
                        
                    }

                }

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

        private void DrawXorLine(Graphics gtemp, Point p1, Point p2, Boolean bRubOut)
        {
            // Debug.WriteLine("DrawXorLine");

            if (!mouseDown)
            {
                return;
            }

            if (picDrawing == null)
            {
                throw new NullReferenceException("RubberBandLine.picDrawing is null.");
            }

            gtemp.CompositingMode = CompositingMode.SourceOver;

            if (bRubOut && staticTextureBrush == null && staticPenRubout == null)
            {
                

                if (picDrawing.Image != null)
                {
                    staticTextureBrush = new TextureBrush(picDrawing.Image);
                    staticPenRubout = new Pen(staticTextureBrush, (float)this.current_width + 2);
                    
                }
                else
                {
                  //  gtemp.Dispose();
                    
                }
            }

            if (bRubOut && staticPenRubout != null && !(p1 == p2))
            {
               
                switch (dmode)
                {
                    case DrawMode.modeEmptyEllipse:
                        gtemp.DrawEllipse(staticPenRubout, p1.X, p1.Y, p2.X, p2.Y);
                        break;
                    case DrawMode.modeEmptyRectangle:
                        gtemp.DrawRectangle(staticPenRubout, p1.X, p1.Y, p2.X, p2.Y);
                        break;
 
                }
                   
                
               
            }
            else
            {
                Pen p = new Pen(this.current_color, this.current_width);
                switch (dmode)
                {
                    case DrawMode.modeEmptyEllipse:
                        gtemp.DrawEllipse(p, p1.X, p1.Y, p2.X, p2.Y);

                        break;
                    case DrawMode.modeEmptyRectangle:
                        gtemp.DrawRectangle(p, p1.X, p1.Y, p2.X, p2.Y);
                        break;
                }
                p.Dispose();
                p = null;
            }
        }

        private void tlbdrp_eraser_Click(object sender, EventArgs e)
        {
            try
            {
                dmode = DrawMode.modeRubber;
                switch(rubber_width)
                {
                    case 5:
                        {
                             if (System.IO.File.Exists(sApplicationPath + "\\rubbermini.cur") == true)
                            {
                                if (cr != null)
                                {
                                    cr.Dispose();
                                    cr = null;
                                }

                                cr = new Cursor(sApplicationPath + "\\rubbermini.cur");
                                if (cr != null)
                                {
                                    picDrawing.Cursor = cr;
                                }

                   
                            }
                            break;
                        }
                    case 20:
                        {
                            if (System.IO.File.Exists(sApplicationPath + "\\rubbermed.cur") == true)
                            {
                                if (cr != null)
                                {
                                    cr.Dispose();
                                    cr = null;
                                }

                                cr = new Cursor(sApplicationPath + "\\rubbermed.cur");
                                if (cr != null)
                                {
                                    picDrawing.Cursor = cr;
                                }

                   
                            }
                            break;
                        }
                    case 80:
                        {
                            if (System.IO.File.Exists(sApplicationPath + "\\rubber.cur") == true)
                            {
                                if (cr != null)
                                {
                                    cr.Dispose();
                                    cr = null;
                                }

                                cr = new Cursor(sApplicationPath + "\\rubber.cur");
                                if (cr != null)
                                {
                                    picDrawing.Cursor = cr;
                                }

                   

                            }
                            break;
                        }
                }

            }
            finally
            {
               
            }
        }

        private void tlpEraser1_Click(object sender, EventArgs e)
        {
            try
            {
                dmode = DrawMode.modeRubber;
                rubber_width = 5;
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                   

                }
            }
        }

        private void tlpEraser2_Click(object sender, EventArgs e)
        {
            try
            {
                dmode = DrawMode.modeRubber;
                rubber_width = 20;
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                   

                }
            }
        }

        private void tlpEraser3_Click(object sender, EventArgs e)
        {
            try
            {
                dmode = DrawMode.modeRubber;
                rubber_width = 80;
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                   

                }
            }
        }

        private void tlpcmb_Width_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(tlpcmb_Width.SelectedItem) != "")
            {
                current_width = Convert.ToInt32(tlpcmb_Width.SelectedItem);
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
                    if (cr != null)
                    {
                        picDrawing.Cursor = cr;
                    }

                }

            }
        }

    }
}