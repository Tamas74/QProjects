using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using wd = Microsoft.Office.Interop.Word;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace bitmaptweaker
{
    public partial class BitmapTweaker : Form
    {
        gloGlobal.gloClipboardWatcher myWatcher = null;
        public BitmapTweaker()
        {
            InitializeComponent();
            myWatcher = new gloGlobal.gloClipboardWatcher();
            myWatcher.OnClipboardContentChanged += new gloGlobal.gloClipboardControl.ClipboardContentChanged(myWatcher_OnClipboardContentChanged);
            myWatcher.Start();
        }
        private int thisCount = 0;
        private void btnOK_Click(object sender, EventArgs e)
        {
            TweakBitmap();
        }

        private void SaveBitmap()
        {
            if (resultBitmap != null)
            {
                string FileName = dlgBitmapTweaker.FileName;
                if (!FileName.Contains(".png"))
                {
                    FileName += ".png";
                }
                thisCount++;
                resultBitmap.Save(FileName.Replace(".png", "_new" + thisCount.ToString() + ".png"),ImageFormat.Png);
            }
        }
        private string thisFileName = null;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Image originalImage = null;
        Bitmap originalBitmap = null;
        Bitmap resultBitmap = null;

        Metafile myMetaData = null;
        int totCount = 0;


        private void txtFileSelection_Click(object sender, EventArgs e)
        {
           
            if (thisFileName != null)
            {
                dlgBitmapTweaker.FileName = thisFileName;
            }
            DialogResult dlgRes = dlgBitmapTweaker.ShowDialog();
            
            if (dlgRes == System.Windows.Forms.DialogResult.OK)
            {
                thisCount = 0;
                thisFileName = dlgBitmapTweaker.FileName;
                txtFileSelection.Text = thisFileName;
                
               // object missing = Type.Missing;
               // wd.Application myApp = new wd.Application();
               // myApp.Visible =false;
               // wd.Document myDoc = myApp.Documents.Open(txtFileSelection.Text,missing,missing,missing);
               // myDoc.Activate();
                

               // int iCount=1;
               //// foreach (wd.Window windows in myDoc.Windows)
               // wd.Window windows = myDoc.ActiveWindow;
               // {
               //     windows.Activate();
               //     foreach (wd.Pane panes in windows.Panes)
               //     {
               //         panes.Activate();
               //         for (int i=1;i<=panes.Pages.Count;i++)
               //         {
               //             panes.Activate();
               //             bool ableToAccess = false;
               //             byte[] bytes = null;
               //             while (!ableToAccess)
               //             {
               //                 try
               //                 {

               //                     bytes = panes.Pages[i].EnhMetaFileBits;
               //                     ableToAccess = true;
               //                 }
               //                 catch (COMException)
               //                 {
               //                     System.Threading.Thread.Sleep(1);

               //                 }
               //             }
                            
               //                 string nwFileName = txtFileSelection.Text.Replace(".docx", "_" + iCount.ToString() + ".emf");
               //                 File.WriteAllBytes(nwFileName, bytes);
                           
               //             iCount++;

               //         }
               //     }
               // }
               // object wdSave = wd.WdSaveFormat.wdFormatPDF;
               // myDoc.SaveAs(txtFileSelection.Text.Replace(".docx", "_" + iCount.ToString() + ".pdf"),wdSave);
               // myDoc.Close(missing,missing,missing);
               // myApp.Quit(missing,missing,missing);
               // totCount = iCount - 1;
               // //for (int jCount = 1; jCount < iCount; jCount++)
               // //{
               // //     myMetaData = new Metafile(txtFileSelection.Text.Replace(".docx", "_" + iCount.ToString() + ".emf"));

               // //}
               // curCount = 1;
               // myMetaFileDelegate = new Graphics.EnumerateMetafileProc(MetaFileCallBack);
               // PrintDocument Pd = new PrintDocument();
               // Pd.BeginPrint += new PrintEventHandler(Pd_BeginPrint);
               // Pd.QueryPageSettings += new QueryPageSettingsEventHandler(Pd_QueryPageSettings);
               // Pd.EndPrint += new PrintEventHandler(Pd_EndPrint);
               // Pd.PrintPage += new PrintPageEventHandler(Pd_PrintPage);
               // Pd.Print();
               // Pd.Dispose();
               // Pd = null;
               // if (myMetaData != null)
               // {
               //     myMetaData.Dispose();
               //     myMetaData = null;
               // }
               // if (iCount != 0)
               // {
               //     return;
               //    // dlgBitmapTweaker.FileName = txtFileSelection.Text.Replace(".docx", "_1.emf");
                   
               // }
                if (originalImage != null)
                {
                    originalImage.Dispose();
                    originalImage = null;
                }

                originalImage = Image.FromFile(dlgBitmapTweaker.FileName);
                if (originalBitmap != null)
                {
                    originalBitmap.Dispose();
                    originalBitmap = null;
                }
                originalBitmap = new Bitmap(originalImage);
                picOriginal.Image = new Bitmap(originalBitmap);
                TweakBitmap();
            }
        }

        void myWatcher_OnClipboardContentChanged(object sender, EventArgs e)
        {
          Image myImage =  Clipboard.GetImage();
          if (myImage != null)
          {
              myImage.Save(@"D:\SLR\Test" + curCount.ToString() + ".png", ImageFormat.Png);
          }
          var thistext = Clipboard.GetText();
          var thisdata = Clipboard.GetDataObject();
          curCount++;
          try
          {
              myImage.Dispose();
          }
          catch
          {
          }

        }
        int curCount = 1;
        void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Point point = new System.Drawing.Point(0, 0);
           
            float widthInPoints = myMetaData.Width / myMetaData.HorizontalResolution * 72;
            float HeightInPoints = myMetaData.Height / myMetaData.VerticalResolution * 72;
            //if (rdTweaker.Checked)
            //{
            //    float widthScale = widthInPoints / (e.MarginBounds.Width );
            //    float heightScale = HeightInPoints / (e.MarginBounds.Height );
            //    float bothScale = 1;
            //    if (widthScale < heightScale)
            //    {
            //        bothScale = widthScale;
            //    }
            //    else
            //    {
            //        bothScale = heightScale;
            //    }
            //    if (bothScale > 1)
            //    {
            //        bothScale = 1;
            //    }
            //    e.Graphics.ScaleTransform(bothScale, bothScale);
            //    System.Drawing.Rectangle newRect = new System.Drawing.Rectangle(0, 0, System.Convert.ToInt32(widthInPoints), System.Convert.ToInt32(HeightInPoints));


            //    e.Graphics.EnumerateMetafile(myMetaData, point, newRect, GraphicsUnit.Point, myMetaFileDelegate);
            //}
            //else
            {
                float widthScale = (e.MarginBounds.Width + e.MarginBounds.X) / widthInPoints;
                float heightScale = (e.MarginBounds.Height + e.MarginBounds.Y) / HeightInPoints;
                float bothScale = 1;
                if (widthScale < heightScale)
                {
                    bothScale = widthScale;
                }
                else
                {
                    bothScale = heightScale;
                }
                //if (bothScale < 1)
                //{
                //    bothScale = 1;
                //}
                if (myMetaData != null)
                {
                    myMetaData.Dispose();
                    myMetaData = null;
                }
                Image myImage = Image.FromFile(txtFileSelection.Text.Replace(".docx", "_" + curCount.ToString() + ".emf"));

                //e.Graphics.ScaleTransform(bothScale, bothScale);
                Rectangle newRect = new Rectangle(0, 0, Convert.ToInt32(widthInPoints * bothScale), Convert.ToInt32(HeightInPoints * bothScale));

                //Point[] points = { new Point(e.MarginBounds.Location.X, e.MarginBounds.Y), new Point(e.MarginBounds.Right, e.MarginBounds.Top), new Point(e.MarginBounds.Left, e.MarginBounds.Bottom) };
                //e.Graphics.EnumerateMetafile(myMetaData, point, newRect,GraphicsUnit.Point, myMetaFileDelegate);
                // e.Graphics.EnumerateMetafile(myMetaData, point,  myMetaFileDelegate);
                e.Graphics.DrawImage(myImage, newRect);
                myImage.Dispose();
                myImage = null;
            }
            if (curCount < totCount)
            {
                curCount++;
                myMetaData = new Metafile(txtFileSelection.Text.Replace(".docx", "_" + curCount.ToString() + ".emf"));
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        void Pd_EndPrint(object sender, PrintEventArgs e)
        {
            MessageBox.Show("Successfully Printed");
        }

        void Pd_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            e.PageSettings.Landscape = myMetaData.Height < myMetaData.Width;
        }

        void Pd_BeginPrint(object sender, PrintEventArgs e)
        {
            if (myMetaData != null)
            {
                myMetaData.Dispose();
                myMetaData = null;
            }
            myMetaData = new Metafile(txtFileSelection.Text.Replace(".docx", "_" + curCount.ToString() + ".emf"));
          
        }

      
        Graphics.EnumerateMetafileProc myMetaFileDelegate = null;

        private bool MetaFileCallBack(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
        {
            byte[] dataArray = null;
            if (data != IntPtr.Zero)
            {
                dataArray = new byte[dataSize];
                Marshal.Copy(data, dataArray, 0, dataSize);
            }
            if (myMetaData != null)
            {
                myMetaData.PlayRecord(recordType, flags, dataSize, dataArray);
            }
            return true;
        }
        public Bitmap BitmapCompressor(Bitmap orgImage)
        {
            int width = (orgImage.Width+2)/3*3;
            int height = (orgImage.Height+2)/3*3;
            Bitmap newBitmap = new Bitmap(width, height);
            newBitmap.SetResolution(orgImage.HorizontalResolution, orgImage.VerticalResolution);
            using (Graphics thisGraphics = Graphics.FromImage(newBitmap))
            {
                thisGraphics.PageUnit = GraphicsUnit.Pixel;
                thisGraphics.DrawImageUnscaled(orgImage, 0, 0);
                using( gloGlobal.LockBitmap gLockBitmap = new gloGlobal.LockBitmap(newBitmap))
                {
                    for (int i = 1; i < height; i += 3)
                    {
                        for (int j = 1; j < width; j += 3)
                        {

                            Color[,] colorKL = new Color[3, 3];
                            int sumRed = 0, sumGreen = 0, sumBlue = 0;
                            int rowRedLeft = 0, rowGreenLeft = 0, rowBlueLeft = 0;
                            int rowRedRight = 0, rowGreenRight = 0, rowBlueRight = 0;
                            int colRedTop = 0, colGreenTop = 0, colBlueTop = 0;
                            int colRedBottom = 0, colGreenBottom = 0, colBlueBottom = 0;
                            for (int k = -1; k <= 1; k++)
                            {

                                for (int l = -1; l <= 1; l++)
                                {
                                    Color thisColor = gLockBitmap.GetPixel(j + l, i + k);
                                    sumRed += thisColor.R;
                                    sumGreen += thisColor.G;
                                    sumBlue += thisColor.B;
                                    if (k == -1)
                                    {
                                        colRedTop += thisColor.R;
                                        colGreenTop += thisColor.G;
                                        colBlueTop += thisColor.B;
                                    }
                                    if (k == 1)
                                    {
                                        colRedBottom += thisColor.R;
                                        colGreenBottom += thisColor.G;
                                        colBlueBottom += thisColor.B;
                                    }

                                    if (l == -1)
                                    {
                                        rowRedLeft += thisColor.R;
                                        rowGreenLeft += thisColor.G;
                                        rowBlueLeft += thisColor.B;
                                    }
                                    if (l == 1)
                                    {
                                        rowRedRight += thisColor.R;
                                        rowGreenRight += thisColor.G;
                                        rowBlueRight += thisColor.B;
                                    }

                                }
                            }
                            int cSumRed = sumRed / 9;
                            int cSumGreen = sumGreen / 9;
                            int cSumBlue = sumBlue / 9;

                            int bColRed = (colRedBottom - colRedTop) / 6;
                            int bColGreen = (colGreenBottom - colGreenTop) / 6;
                            int bColBlue = (colBlueBottom - colBlueTop) / 6;

                            int aRowRed = (rowRedRight - rowRedLeft) / 6;
                            int aRowGreen = (rowGreenRight - rowGreenLeft) / 6;
                            int aRowBlue = (rowBlueRight - rowBlueLeft) / 6;
                            //if (cSumRed > 0)
                            //{
                            //    cSumRed = cSumRed;
                            //}

                            for (int k = -1; k <= 1; k++)
                            {

                                for (int l = -1; l <= 1; l++)
                                {
                                    int colorRed = (cSumRed + bColRed * k + aRowRed * l);
                                    int colorGreen = (cSumGreen + bColGreen * k + aRowGreen * l);
                                    int colorBlue = (cSumBlue + bColBlue * k + aRowBlue * l);
                                    if (colorRed > 255)
                                    {
                                        colorRed = 255;
                                    }
                                    if (colorRed < 0)
                                    {
                                        colorRed = 0;
                                    }
                                    if (colorGreen > 255)
                                    {
                                        colorGreen = 255;
                                    }
                                    if (colorGreen < 0)
                                    {
                                        colorGreen = 0;
                                    }
                                    if (colorBlue > 255)
                                    {
                                        colorBlue = 255;
                                    }
                                    if (colorBlue < 0)
                                    {
                                        colorBlue = 0;
                                    }

                                    Color newColor = Color.FromArgb(colorRed, colorGreen, colorBlue);
                                    gLockBitmap.SetPixel(j + l, i + k, newColor);
                                }
                            }
                        }
                    }
                }
            }
            return newBitmap;
        }
        private void TweakBitmap()
        {
            if (resultBitmap != null)
            {
                resultBitmap.Dispose();
                resultBitmap = null;
            }
            resultBitmap = BitmapCompressor(originalBitmap);
            //if (rdTweaker.Checked)
            //{
            //    resultBitmap = gloGlobal.BitmapConverter.BitmapTweaker(originalBitmap);
            //}
            //else
            //{
            //    resultBitmap = gloGlobal.BitmapConverter.BitmapBorderTweaker(originalBitmap);
            //}
            picResult.Image = new Bitmap(resultBitmap);
            SaveBitmap();
        }

        private void rdTweaker_CheckedChanged(object sender, EventArgs e)
        {
            if (thisFileName != null)
            {
                TweakBitmap();
            }

            if (rdTweaker.Checked == true)
            {
                rdTweaker.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rdTweaker.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (thisFileName != null)
            {
                TweakBitmap();
            }

            if (rdBorder.Checked == true)
            {
                rdBorder.Font = new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rdBorder.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }
    }
}
