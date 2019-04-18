/*
 * This is a sample implementation of the ImageViewer control 
 * 
 * In this example the image viewer control is hosted in a user control
 * a toolbad containing commands to manipulate an image displayed in the viewer control is 
 * placed so that they could be easily accessed
 * 
 * By Samqty
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloScanImaging
{
    public partial class ImageControl : UserControl
    {
        public ImageControl()
        {
            InitializeComponent();
        }
        public Image CurrImage { get; set; }
        public int _CurrZoomIndex = 9;
        public string ImgPath { get; set; }
        public bool CalledFromCardScan = false;
        private ImageFile MyLoadImg = null;
        /// <summary>
        /// this member method is used to set the source of an image and is exposed to any 
        /// consuming code, like windows implementing them
        /// </summary>
        /// <param name="im">image source object that implement IZImage interface</param>
        public void SetImageSource(IZImage im)
        {
            img.SetImageSource(im);
            SetCurrentImage();
            //SetScrollBars();
            if (!CalledFromCardScan)
            {
                SetScrollBarsWithZoom(img.GetZoom());
            }
        }
        public Control GetPictureControl()
        {
            return img.GetPictureControl();
        }
        public void CloseCurrentImage()
        {
            if (img.originalImage != null)
            {
                img.originalImage.Dispose();
                img.originalImage = null;
            }

            try
            {
                if (MyLoadImg != null)
                {
                    MyLoadImg.DisposeMyLoadImg();
                    MyLoadImg = null;
                }
            }
            catch { }

        }

        public void UnloadDisplayImage()
        {
            img.UnloadDisplayImage();
        }


        public Point PictureScrollPos
        {
            get
            {
                return img.PictureScrollPos;
            }
            set
            {
                img.PictureScrollPos = value;
            }
        }


        const int WM_SETREDRAW = 0xb;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private int bSuspended = 0;

        public bool UpdateScreenOfControl(bool bResumeOrSuspend)
        {
            try
            {
                if (!bResumeOrSuspend)
                {
                    bSuspended++;
                }

                if (bResumeOrSuspend)
                {
                    bSuspended--;
                }

                if (bSuspended > 0 && bResumeOrSuspend)
                { return true; }

                Control tempCntr = this.Parent; //img.GetPictureControl();
                //SLR: We can use LockWindowUpdate for single thread applicaiton
                int iResumeOrSuspend = Convert.ToInt16(bResumeOrSuspend);
                SendMessage(tempCntr.Handle, WM_SETREDRAW, (IntPtr)iResumeOrSuspend, (IntPtr)0);
                if ((bResumeOrSuspend))
                {
                    //InvalidateRect(thisControl.Handle, Nothing, True)
                    //UpdateWindow(thisControl.Handle)
                    //SLR: ideally both should be refreshing, But not refreshing, hence used, builtin refresh which is a costly call.
                    img.AssignIMG();

                    tempCntr.Refresh();
                    // tempCntr.Parent.Refresh();
                    //this.Refresh();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
            }
        }

        public bool UpdateScreenOfControlWithout(bool bResumeOrSuspend)
        {
            try
            {
                if (!bResumeOrSuspend)
                {
                    bSuspended++;
                }

                if (bResumeOrSuspend)
                {
                    bSuspended--;
                }

                if (bSuspended > 0 && bResumeOrSuspend)
                { return true; }

                Control tempCntr = this.Parent; //img.GetPictureControl();
                //SLR: We can use LockWindowUpdate for single thread applicaiton
                int iResumeOrSuspend = Convert.ToInt16(bResumeOrSuspend);
                SendMessage(tempCntr.Handle, WM_SETREDRAW, (IntPtr)iResumeOrSuspend, (IntPtr)0);
                if ((bResumeOrSuspend))
                {
                    //InvalidateRect(thisControl.Handle, Nothing, True)
                    //UpdateWindow(thisControl.Handle)
                    //SLR: ideally both should be refreshing, But not refreshing, hence used, builtin refresh which is a costly call.
                    //img.AssignIMG();

                    tempCntr.Refresh();
                    // tempCntr.Parent.Refresh();
                    //this.Refresh();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
            }
        }


        public void SetImageWithPath(string sImgPath, bool bCalledFromCardScan = false, int _CurrIndex = 9)
        {
            UpdateScreenOfControl(false);
            if (_CurrIndex == -1)
            { _CurrIndex = 9; }

            _CurrZoomIndex = _CurrIndex;
            ImgPath = sImgPath;
            CalledFromCardScan = bCalledFromCardScan;
            MyLoadImg = new ImageFile(sImgPath);
            SetImageSource(MyLoadImg);
            SetCurrentImage();
            //SetScrollBars();
            if (!CalledFromCardScan)
            {
                SetScrollBarsWithZoom(img.GetZoom());
            }

            img.SetCenterToScroll();

            UpdateScreenOfControl(true);
        }

        public void RotateImage(bool Clockwise, int _CurrIndex = 9)
        {
            img.RotateImage(Clockwise);
            Image CloneImg = null;
            try
            {
                if (img.originalImage != null)
                {
                    try
                    {
                        CloneImg = (Image)img.originalImage.Clone();
                        img.originalImage.Dispose();
                        img.originalImage = null;
                    }
                    catch
                    { }
                    try
                    {
                        if (CloneImg != null)
                        {
                            try
                            {
                                using (FileStream fs = File.Open(ImgPath, FileMode.Open, FileAccess.ReadWrite))
                                {
                                    try
                                    {
                                        fs.Close();
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            catch
                            {
                                //Changing the file name if original file is locked.
                                ImgPath = Path.GetDirectoryName(ImgPath) + Path.GetFileNameWithoutExtension(ImgPath) + "f" + Path.GetExtension(ImgPath);
                            }
                            CloneImg.Save(ImgPath);
                            CloneImg.Dispose();
                            CloneImg = null;
                        }
                    }
                    catch
                    { }

                    try
                    {
                        SetImageWithPath(ImgPath, _CurrIndex: _CurrIndex);
                    }
                    catch
                    { }
                }
            }
            catch //(Exception ex)
            {


            }

            //dZoomVal = img.GetZoomValue(gloScanImaging.ZoomMode.FITPAGE);
            //DisplayImageWithZoomVal(dZoomVal);
        }



        public void ZoomImage(gloScanImaging.ZoomMode mode)
        {
            double dZoomVal;
            dZoomVal = img.GetZoomValue(mode);
            DisplayImageWithZoomVal(dZoomVal);
            img.CurrSelectedMode = mode;
        }

        //private void btn_Click(object sender, EventArgs e)
        //{
        //    PanButtonFunctionality(sender);
        //}

        //private void PanButtonFunctionality(object sender)
        //{
        //    img.ImagePreviewMode = (PreviewMode)Enum.Parse(typeof(PreviewMode), ((ToolStripButton)sender).Tag.ToString());
        //    btnPan.Checked = img.ImagePreviewMode == PreviewMode.PAN;
        //    btnRegionZoom.Checked = img.ImagePreviewMode == PreviewMode.REGIONSELECTION;
        //    btnZoomIn.Checked = img.ImagePreviewMode == PreviewMode.ZOOMIN;
        //    btnZoomOut.Checked = img.ImagePreviewMode == PreviewMode.ZOOMOUT;
        //    switch (img.ImagePreviewMode)
        //    {
        //        case PreviewMode.PAN:
        //            this.Cursor = Cursors.Hand;
        //            break;
        //        case PreviewMode.REGIONSELECTION:
        //            this.Cursor = Cursors.Cross;
        //            break;
        //        default:
        //            this.Cursor = Cursors.Default;
        //            break;

        //    }
        //}

        //public void PanButtonClicked(object sender)
        //{
        //    PanButtonFunctionality(sender);
        //    //SetScrollBars();
        //}

        private void SetScrollBars()
        {
            try
            {
                double mZoom = img.GetZoom() * 2;
                Size DisplaySize = new Size(1, 1);
                if (img.originalImage != null)
                {
                    DisplaySize = new Size(((int)((double)img.originalImageWidth * mZoom)), (int)((double)img.originalImageHeight * mZoom));
                }
                else
                {
                    if (this != null)
                    {
                        DisplaySize = new Size(((int)((double)this.Width * mZoom)), (int)((double)this.Height * mZoom));
                    }
                }

                mZoom = getAdjustedZoomForImage(DisplaySize);
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
                if (img != null)
                {
                    pictureBox1.Size = new Size(width, height);
                    //                    img.Size = new Size(width, height); ;
                }
                //if (img.originalImage != null)
                //{
                //    pictureBox1.Size = new Size(((int)((double)img.originalImage.Width * mZoom)), (int)((double)img.originalImage.Height * mZoom));
                //}
                //else
                //{
                //    pictureBox1.Size = new Size(((int)((double)this.Width * mZoom)), (int)((double)this.Height * mZoom));
                //}

            }
            catch
            {

            }
        }

        private void SetScrollBarsWithZoom(double mZoom)
        {
            try
            {
                Size DisplaySize = new Size(1, 1);
                if (img.originalImage != null)
                {
                    DisplaySize = new Size(((int)((double)img.originalImageWidth * mZoom)), (int)((double)img.originalImageHeight * mZoom));
                }
                else
                {
                    if (this != null)
                    {
                        DisplaySize = new Size(((int)((double)this.Width * mZoom)), (int)((double)this.Height * mZoom));
                    }

                }

                mZoom = getAdjustedZoomForImage(DisplaySize);
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
                if (img != null)
                {
                    pictureBox1.Size = new Size(width, height);
                    //                    img.Size = new Size(width, height);
                }

                //mZoom = getAdjustedZoomForImage(img.originalImage.Size) * mZoom;

                //if (img.originalImage != null)
                //{
                //    pictureBox1.Size = new Size(((int)((double)img.originalImage.Width * mZoom)), (int)((double)img.originalImage.Height * mZoom));
                //}
                //else
                //{
                //    pictureBox1.Size = new Size(((int)((double)this.Width * mZoom)), (int)((double)this.Height * mZoom));
                //}
            }
            catch
            {

            }
        }

        private void cmbZoom_TextUpdate(object sender, EventArgs e)
        {
            if (cmbZoom.Text.IndexOf('%') != -1)
            {
                img.ZoomImage(double.Parse(cmbZoom.Text.Trim('%')) / 100.0);

            }
            else
            {
                img.ZoomImage((gloScanImaging.ZoomMode)(Enum.Parse(typeof(gloScanImaging.ZoomMode), cmbZoom.Text, true)));
            }
        }

        public void ZoomTextUpdate(ToolStripComboBox ocmbZoom)
        {
            if (ocmbZoom.Text.IndexOf('%') != -1)
            {
                img.ZoomImage(double.Parse(ocmbZoom.Text.Trim('%')) / 100.0);

            }
            else
            {
                img.ZoomImage((gloScanImaging.ZoomMode)(Enum.Parse(typeof(gloScanImaging.ZoomMode), ocmbZoom.Text, true)));
            }
            SetScrollBars();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            img.MoveToPreviousImage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            img.MoveToNextImage();
        }

        private void cmbZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //those values with % value on them they are percent values
            //    //send this zoom value to the component
            //    if (cmbZoom.Text.IndexOf('%') != -1)
            //    {
            //        img.ZoomImage(double.Parse(cmbZoom.Text.Trim('%')) / 100.0);
            //    }
            //    else
            //    {
            //        //if its an enumeration then parse the text to get the enumeration
            //        img.ZoomImage((gloScanImaging.ZoomMode)(Enum.Parse(typeof(gloScanImaging.ZoomMode), cmbZoom.Text, true)));
            //    }
            //}
            //catch
            //{
            //    cmbZoom.Text = "";
            //}
            ToolStripComboBox ocmbZoom = cmbZoom;
            double dZoomVal = 1.0;
            try
            {
                //those values with % value on them they are percent values
                //send this zoom value to the component

                if (ocmbZoom.Text.IndexOf('%') != -1)
                {
                    try
                    {
                        dZoomVal = double.Parse(ocmbZoom.Text.Trim('%')) / 100.0;
                    }
                    catch
                    {
                        dZoomVal = 1.0;
                        ocmbZoom.Text = "100%";
                    }
                    img.ZoomImage(dZoomVal);
                }
                else
                {
                    //if its an enumeration then parse the text to get the enumeration
                    gloScanImaging.ZoomMode zmode = gloScanImaging.ZoomMode.FITPAGE;
                    try
                    {
                        zmode = (gloScanImaging.ZoomMode)(Enum.Parse(typeof(gloScanImaging.ZoomMode), ocmbZoom.Text, true));
                    }
                    catch
                    {
                        zmode = gloScanImaging.ZoomMode.FITPAGE;
                        try
                        {
                            ocmbZoom.Text = gloScanImaging.ZoomMode.FITPAGE.ToString();
                        }
                        catch
                        {
                        }
                    }
                    img.ZoomImage(zmode);
                }
            }
            catch
            {
                ocmbZoom.Text = "";
            }



        }

        double dZoomVal;

        public void ZoomValueChanged(ComboBox ocmbZoom)
        {
            try
            {
                //those values with % value on them they are percent values
                //send this zoom value to the component

                if (ocmbZoom.Text.IndexOf('%') != -1)
                {
                    try
                    {
                        dZoomVal = double.Parse(ocmbZoom.Text.Trim('%')) / 100.0;
                    }
                    catch
                    {
                        dZoomVal = 1.0;
                        ocmbZoom.Text = "100%";
                    }
                    img.CurrSelectedMode = ZoomMode.PERCENTAGE;
                    img.SetZoom(dZoomVal);
                }
                else
                {
                    //if its an enumeration then parse the text to get the enumeration
                    gloScanImaging.ZoomMode zmode = gloScanImaging.ZoomMode.FITPAGE;
                    try
                    {
                        zmode = (gloScanImaging.ZoomMode)(Enum.Parse(typeof(gloScanImaging.ZoomMode), ocmbZoom.Text, true));
                    }
                    catch
                    {
                        zmode = gloScanImaging.ZoomMode.FITPAGE;
                        try
                        {
                            ocmbZoom.Text = gloScanImaging.ZoomMode.FITPAGE.ToString();
                        }
                        catch
                        {
                        }
                    }
                    dZoomVal = img.GetZoomValue(zmode);
                    img.CurrSelectedMode = zmode;
                }
                DisplayImageWithZoomVal(dZoomVal);
            }
            catch
            {
                ocmbZoom.Text = "";
            }

        }

        public void ZoomValueChanged(ToolStripComboBox ocmbZoom)
        {
            try
            {
                //those values with % value on them they are percent values
                //send this zoom value to the component

                if (ocmbZoom.Text.IndexOf('%') != -1)
                {
                    try
                    {
                        dZoomVal = double.Parse(ocmbZoom.Text.Trim('%')) / 100.0;
                    }
                    catch
                    {
                        dZoomVal = 1.0;
                        ocmbZoom.Text = "100%";
                    }
                    img.CurrSelectedMode = ZoomMode.PERCENTAGE;
                }
                else
                {
                    //if its an enumeration then parse the text to get the enumeration
                    gloScanImaging.ZoomMode zmode = gloScanImaging.ZoomMode.FITPAGE;
                    try
                    {
                        zmode = (gloScanImaging.ZoomMode)(Enum.Parse(typeof(gloScanImaging.ZoomMode), ocmbZoom.Text, true));
                    }
                    catch
                    {
                        zmode = gloScanImaging.ZoomMode.FITPAGE;
                        try
                        {
                            ocmbZoom.Text = gloScanImaging.ZoomMode.FITPAGE.ToString();
                        }
                        catch
                        {
                        }
                    }
                    dZoomVal = img.GetZoomValue(zmode);
                    img.CurrSelectedMode = zmode;
                }

                DisplayImageWithZoomVal(dZoomVal);
            }
            catch
            {
                ocmbZoom.Text = "";
            }
        }

        public static int iMaxBitmapSize = 0x2000000;

        public static double getAdjustedZoomForImage(Size DisplaySize)
        {
            try
            {
                int Area = DisplaySize.Width * DisplaySize.Height;
                if (Area > iMaxBitmapSize)
                {
                    return (Math.Sqrt((double)iMaxBitmapSize / ((double)Area)));
                }
            }
            catch
            {
            }
            return 1.0;
        }
        private void DisplayImageWithZoomVal(double dZoomVal)
        {
            try
            {
                UpdateScreenOfControl(false);
                Size DisplaySize = img.GetPictureSize(dZoomVal); //GetDisplaySize();

                double dReduceZoom = ImageControl.getAdjustedZoomForImage(DisplaySize);

                //if ((DisplaySize.Width * DisplaySize.Height) > iMaxBitmapSize)
                //{
                //    dReduceZoom = Math.Sqrt((double) iMaxBitmapSize / ((double)DisplaySize.Width *(double) DisplaySize.Height));
                //}
                dZoomVal *= dReduceZoom;

                SetScrollBarsWithZoom(dZoomVal);
                img.SetPictureSize(dZoomVal);
                double LocZoom = dZoomVal;
                img.SetDisplayImage(ref LocZoom);
                img.ZoomImageWithZoomFactor(dZoomVal);
                UpdateScreenOfControl(true);
            }
            catch
            {
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    img.CurrentImage.Save(dlgSave.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Occured : \n" + ex.Message);
                }
            }
        }

        public void SetCurrentImage()
        {
            CurrImage = img.CurrentDisplayedImage;
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            img.RotateImage(true);
            SetScrollBars();
        }
    }
}
