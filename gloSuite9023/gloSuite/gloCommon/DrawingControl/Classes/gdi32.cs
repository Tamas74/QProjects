using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace gloGDI
{
	/// <summary>
	/// Wrapper class for the gdi32.dll.
	/// </summary>
	public class Gdi32
	{
        public enum PenStyles
        {
            PS_SOLID = 0,
            PS_DASH = 1,
            PS_DOT = 2,
            PS_DASHDOT = 3,
            PS_DASHDOTDOT = 4,
            PS_NULL = 5,
            PS_INSIDEFRAME = 6,
            PS_USERSTYLE = 7,
            PS_ALTERNATE = 8,
            PS_STYLE_MASK = 0x0000000F,
            PS_ENDCAP_ROUND = 0x00000000,
            PS_ENDCAP_SQUARE = 0x00000100,
            PS_ENDCAP_FLAT = 0x00000200,
            PS_ENDCAP_MASK = 0x00000F00,
            PS_JOIN_ROUND = 0x00000000,
            PS_JOIN_BEVEL = 0x00001000,
            PS_JOIN_MITER = 0x00002000,
            PS_JOIN_MASK = 0x0000F000,
            PS_COSMETIC = 0x00000000,
            PS_GEOMETRIC = 0x00010000,
            PS_TYPE_MASK = 0x000F0000
        }
		public enum DrawingMode
		{
           R2_BLACK=            1   /*  0       */
        ,R2_NOTMERGEPEN=      2   /* DPon     */
	    ,R2_MASKNOTPEN=       3   /* DPna     */
        ,R2_NOTCOPYPEN=       4   /* PN       */
        ,R2_MASKPENNOT=       5   /* PDna     */
        ,R2_NOT=              6   /* Dn       */
        ,R2_XORPEN=           7   /* DPx      */
        ,R2_NOTMASKPEN=       8   /* DPan     */
        ,R2_MASKPEN=          9   /* DPa      */
        ,R2_NOTXORPEN=        10  /* DPxn     */
        ,R2_NOP=              11  /* D        */
        ,R2_MERGENOTPEN=      12  /* DPno     */
        ,R2_COPYPEN=          13  /* P        */
        ,R2_MERGEPENNOT=      14  /* PDno     */
        ,R2_MERGEPEN=         15  /* DPo      */
        ,R2_WHITE=            16  /*  1       */
        ,R2_LAST=             16

            //R2_NOTXORPEN = 10,
            //RGN_AND=11,
            //RGN_OR=12,
            //RGN_XOR=13,
            //RGN_DIFF=14,
            //RGN_COPY=15

		}
        [DllImport("gdi32.dll")]
        public static extern bool Ellipse(IntPtr hDC, int left, int top, int right, int bottom);

		[DllImport("gdi32.dll")]
		public static extern bool Rectangle(IntPtr hDC, int left, int top, int right, int bottom);

		[DllImport("gdi32.dll")]
		public static extern int SetROP2(IntPtr hDC, int fnDrawMode);

		[DllImport("gdi32.dll")]
		public static extern bool MoveToEx(IntPtr hDC, int x, int y, ref Point p);

		[DllImport("gdi32.dll")]
		public static extern bool LineTo(IntPtr hdc, int x, int y);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);

		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObj);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int X1, int Y1, int X2, int Y2);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(Int32 crColor);

        [DllImport("gdi32.dll")]
        public static extern bool FillRgn(IntPtr hDC, IntPtr hRgn, IntPtr hBrush);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(

            IntPtr hdcDest, // handle to destination DC 

            int nXDest, // x-coord of destination upper-left corner 

            int nYDest, // y-coord of destination upper-left corner 

            int nWidth, // width of destination rectangle 

            int nHeight, // height of destination rectangle 

            IntPtr hdcSrc, // handle to source DC 

            int nXSrc, // x-coordinate of source upper-left corner 

            int nYSrc, // y-coordinate of source upper-left corner 

            System.Int32 dwRop // raster operation code 

       ); 
	}

	/// <summary>
	/// Provides utilities directly accessing the gdi32.dll 
	/// </summary>
	public class GDI
	{
		static private Point nullPoint = new Point(0,0);
        
        
        // Convert the Argb from .NET to a gdi32 RGB
		static private int ArgbToRGB(int rgb)
		{
            return ((rgb >> 16 & 0x0000FF)| (rgb & 0x00FF00) | (rgb << 16 & 0xFF0000));
		}
        static public void DrawXORRectangle(Graphics graphics, Pen pen, Rectangle rectangle)
        {
            IntPtr hDC = graphics.GetHdc();
            IntPtr hPen = Gdi32.CreatePen(0 , (int)pen.Width, ArgbToRGB(pen.Color.ToArgb()));
            Gdi32.SelectObject(hDC, hPen);
            Gdi32.SetROP2(hDC, (int)Gdi32.DrawingMode.R2_NOTXORPEN);
           
            Gdi32.Rectangle(hDC, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            Gdi32.DeleteObject(hPen);
            graphics.ReleaseHdc(hDC);
        }

        //static public void DrawXORFilledRectangle(Graphics graphics, Color ocolor, int x1, int y1, int x2, int y2)
        //{
             //IntPtr hRgn;
             //IntPtr hBrush;
             //IntPtr hDC = graphics.GetHdc();
 
                        
             //hRgn = Gdi32.CreateRectRgn(x1, y1, x2, y2);
             //hBrush = Gdi32.CreateSolidBrush(ocolor.ToArgb());
             //Gdi32.SetROP2(hDC, (int)Gdi32.DrawingMode.R2_NOTXORPEN);
             //Gdi32.FillRgn(hDC, hRgn, hBrush);
             //Gdi32.DeleteObject(hBrush);
             //Gdi32.DeleteObject(hRgn);
             //graphics.ReleaseHdc(hDC);           
               
        //}
        //static public void DrawXORFilledEllipse(Graphics graphics, Color ocolor, int x1, int y1, int x2, int y2)
        //{
            //IntPtr hRgn;
            //IntPtr hBrush;
            //IntPtr hDC = graphics.GetHdc();


            //hRgn = Gdi32.CreateRectRgn(x1, y1, x2, y2);
            //hBrush = Gdi32.CreateSolidBrush(ocolor.ToArgb());
            //Gdi32.SetROP2(hDC, (int)Gdi32.DrawingMode.R2_NOTXORPEN);
            //Gdi32.FillRgn(hDC, hRgn, hBrush);
            //Gdi32.DeleteObject(hBrush);
            //Gdi32.DeleteObject(hRgn);
            //graphics.ReleaseHdc(hDC);           
           
        //}
        static public void DrawXORFilledRectangle(Graphics graphics, Pen pen, Rectangle rectangle)
        {
            IntPtr hDC = graphics.GetHdc();
            pen.Color = Color.Black;
            IntPtr hPen = Gdi32.CreatePen(0, (int)pen.Width, pen.Color.ToArgb());
            Gdi32.SelectObject(hDC, hPen);
            Gdi32.SetROP2(hDC, (int)Gdi32.DrawingMode.R2_XORPEN);
            Gdi32.Rectangle(hDC, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            Gdi32.DeleteObject(hPen);
            graphics.ReleaseHdc(hDC);
        }
        static public void DrawXORFilledEllipse(Graphics graphics, Pen pen, Rectangle rectangle)
        {
            IntPtr hDC = graphics.GetHdc();
            pen.Color = Color.Pink;
            IntPtr hPen = Gdi32.CreatePen(0, (int)pen.Width, pen.Color.ToArgb());
            Gdi32.SelectObject(hDC, hPen);
            Gdi32.SetROP2(hDC, (int)Gdi32.DrawingMode.R2_XORPEN);
            Gdi32.Ellipse(hDC, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            Gdi32.DeleteObject(hPen);
            graphics.ReleaseHdc(hDC);
        }
        static public void DrawXORLine(Graphics graphics, Pen pen, int x1, int y1, int x2, int y2)
        {
            IntPtr hDC = graphics.GetHdc();
            IntPtr hPen = Gdi32.CreatePen(0, (int)pen.Width, ArgbToRGB(pen.Color.ToArgb()));
            Gdi32.SelectObject(hDC, hPen);
            Gdi32.SetROP2(hDC, (int)Gdi32.DrawingMode.R2_NOTXORPEN);
            
            Gdi32.MoveToEx(hDC, x1, y1, ref nullPoint);
            Gdi32.LineTo(hDC, x2, y2);
            Gdi32.DeleteObject(hPen);
            graphics.ReleaseHdc(hDC);
        }
        static public void DrawXOREllipse(Graphics graphics, Pen pen, Rectangle rectangle)
        {
            IntPtr hDC = graphics.GetHdc();
            IntPtr hPen = Gdi32.CreatePen(0, (int)pen.Width, ArgbToRGB(pen.Color.ToArgb()));
            //IntPtr hPen = Gdi32.CreatePen((int)Gdi32.PenStyles.PS_GEOMETRIC ,(int)pen.Width,255);
            Gdi32.SelectObject(hDC, hPen);
            Gdi32.SetROP2(hDC, (int)Gdi32.DrawingMode.R2_NOTXORPEN);
           
            Gdi32.Ellipse(hDC, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            Gdi32.DeleteObject(hPen);
            graphics.ReleaseHdc(hDC);
        }
        static public void SavePicture(Graphics g1,Int32 picwidth,Int32 picheight,string strfilename)
        {

            Image MyImage = new Bitmap(picwidth, picheight, g1);

            Graphics g2 = Graphics.FromImage(MyImage);

            IntPtr dc1 = g1.GetHdc();

            IntPtr dc2 = g2.GetHdc();

            Gdi32.BitBlt(dc2, 0, 0, picwidth, picheight, dc1, 0, 0, 13369376);

            g1.ReleaseHdc(dc1);

            g2.ReleaseHdc(dc2);

            MyImage.Save(strfilename, System.Drawing.Imaging.ImageFormat.Jpeg);
            MyImage.Dispose();
            g2.Dispose();

        }
	}
}
