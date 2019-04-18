using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace gloPrintDialog
{
    public class gloExtendedPrinterSettings
    {
        public enum PageSize
        {
            ActualPageSize = 0,
            FitToPage,
            TwoByOne,
            TwoByTwo,
            ThreeByTwo,
            ThreeByThree,
            None

        }
        public gloExtendedPrinterSettings()
        {
            PrinterMarginsTop = 0;
            PrinterMarginsLeft = 0;
            PrinterMarginsBottom = 0;
            PrinterMarginsRight = 0;
            HorizontalOverlap = 0;
            VerticalOverlap = 0;
            HorizontalGutter = 0;
            VerticalGutter = 0;
            AdjustActualPageHorizontalPageWidthMargin = 50.9f;
            AdjustActualPageVerticalPageHeightMargin = 50.9f;
            AdjustFitToPageHorizontalPageWidthMargin = 0f;
            AdjustFitToPageVerticalPageHeightMargin = 0f;
            CurrentPageSize = PageSize.FitToPage;
            IsHorizontalFlow = true;
            IsActualLandscape = false;
            IsActualMultiPage = false;
            IsCustomDPI = false;
            CustomDPI = 200;
            IsBackGroundPrint = true;
            IsShowProgress = true;
            IsBackGroundPrint = true;
            IsShowProgress = true;

            FooterLeft = 0;
            FooterRight = 0;
            FooterBottom = 0;
            FooterTop = 0;
            FooterFont = null;
            FooterColor = System.Drawing.Color.Black;
            NormalSettings = new GraphicsBound();
            FlatSettings = new GraphicsBound();
        }
        public bool IsCustomDPI { get; set; }
        public Int32 CustomDPI { get; set; }

        //   public bool IsActualPageSize { get; set; }
        public PageSize CurrentPageSize { get; set; }
        public bool IsHorizontalFlow { get; set; }
        public bool IsActualLandscape { get; set; }
        public bool IsActualMultiPage { get; set; }
        public float PrinterMarginsTop { get; set; }
        public float PrinterMarginsLeft { get; set; }
        public float PrinterMarginsBottom { get; set; }
        public float PrinterMarginsRight { get; set; }
        public float HorizontalOverlap { get; set; }
        public float VerticalOverlap { get; set; }
        public float HorizontalGutter { get; set; }
        public float VerticalGutter { get; set; }
        public float AdjustActualPageHorizontalPageWidthMargin { get; set; }
        public float AdjustActualPageVerticalPageHeightMargin { get; set; }
        public float AdjustFitToPageHorizontalPageWidthMargin { get; set; }
        public float AdjustFitToPageVerticalPageHeightMargin { get; set; }
        public bool IsBackGroundPrint { get; set; }
        public bool IsShowProgress { get; set; }
        private System.Drawing.Font _FooterFont = null;
        public System.Drawing.Font FooterFont
        {
            get
            {
                return _FooterFont;
            }

            set
            {
                if (_FooterFont != null)
                {
                    _FooterFont.Dispose();
                    _FooterFont = null;
                }
                if (value != null)
                {
                    _FooterFont = new System.Drawing.Font(value, value.Style);
                }
                else
                {
                    _FooterFont = null;
                }
            }
        }
        private System.Drawing.Color _FooterColor = System.Drawing.Color.Black;
        public System.Drawing.Color FooterColor
        {
            get
            {
                return _FooterColor;
            }

            set
            {
                if (value != null)
                {
                    _FooterColor = value;
                }
                else
                {
                    _FooterColor = System.Drawing.Color.Black;
                }
            }
        }

        public float FooterTop { get; set; }
        public float FooterLeft { get; set; }
        public float FooterBottom { get; set; }
        public float FooterRight { get; set; }
        public GraphicsBound NormalSettings { get; set; }
        public GraphicsBound FlatSettings { get; set; }
        public void Dispose()
        {
            if (_FooterFont != null)
            {
                _FooterFont.Dispose();
                _FooterFont = null;
            }
        }
        public void Copy(gloExtendedPrinterSettings gExPr)
        {
            if (gExPr == null)
            {
                return;
            }
            PrinterMarginsTop = gExPr.PrinterMarginsTop;
            PrinterMarginsLeft = gExPr.PrinterMarginsLeft;
            PrinterMarginsBottom = gExPr.PrinterMarginsBottom;
            PrinterMarginsRight = gExPr.PrinterMarginsRight;

            HorizontalOverlap = gExPr.HorizontalOverlap;
            VerticalOverlap = gExPr.VerticalOverlap;
            HorizontalGutter = gExPr.HorizontalGutter;
            VerticalGutter = gExPr.VerticalGutter;

            AdjustActualPageHorizontalPageWidthMargin = gExPr.AdjustActualPageHorizontalPageWidthMargin;
            AdjustActualPageVerticalPageHeightMargin = gExPr.AdjustActualPageVerticalPageHeightMargin;
            AdjustFitToPageHorizontalPageWidthMargin = gExPr.AdjustFitToPageHorizontalPageWidthMargin;
            AdjustFitToPageVerticalPageHeightMargin = gExPr.AdjustFitToPageVerticalPageHeightMargin;

            CurrentPageSize = gExPr.CurrentPageSize;

            IsHorizontalFlow = gExPr.IsHorizontalFlow;
            IsActualLandscape = gExPr.IsActualLandscape;
            IsActualMultiPage = gExPr.IsActualMultiPage;

            
            //  IsActualPageSize = gExPr.IsActualPageSize;

            IsCustomDPI = gExPr.IsCustomDPI;
            CustomDPI = gExPr.CustomDPI;
            IsBackGroundPrint = gExPr.IsBackGroundPrint;
            IsShowProgress = gExPr.IsShowProgress;
            FooterLeft = gExPr.FooterLeft;
            FooterRight = gExPr.FooterRight;
            FooterBottom = gExPr.FooterBottom;
            FooterTop = gExPr.FooterTop;
            FooterFont = gExPr.FooterFont;
            FooterColor = gExPr.FooterColor;

            NormalSettings = gExPr.NormalSettings;
            FlatSettings = gExPr.FlatSettings;
        }
        public int GetSubPagesCount()
        {
            switch (CurrentPageSize)
            {
                case PageSize.ActualPageSize:
                    return 1;
                case PageSize.FitToPage:
                    return 1;
                case PageSize.TwoByOne:
                    return 2;
                case PageSize.TwoByTwo:
                    return 4;
                case PageSize.ThreeByTwo:
                    return 6;
                case PageSize.ThreeByThree:
                    return 9;
                default:
                    return 0;
            }
        }
        public int GetHorizontalPagesCount()
        {
            switch (CurrentPageSize)
            {
                case PageSize.ActualPageSize:
                    return 1;
                case PageSize.FitToPage:
                    return 1;
                case PageSize.TwoByOne:
                    return 1;
                case PageSize.TwoByTwo:
                    return 2;
                case PageSize.ThreeByTwo:
                    return 2;
                case PageSize.ThreeByThree:
                    return 3;
                default:
                    return 0;
            }
        }
        public int GetVerticalPagesCount()
        {
            switch (CurrentPageSize)
            {
                case PageSize.ActualPageSize:
                    return 1;
                case PageSize.FitToPage:
                    return 1;
                case PageSize.TwoByOne:
                    return 2;
                case PageSize.TwoByTwo:
                    return 2;
                case PageSize.ThreeByTwo:
                    return 3;
                case PageSize.ThreeByThree:
                    return 3;
                default:
                    return 0;
            }
        }
        public static int GetSubPagesCount(PageSize CurrentPageSize)
        {
            switch (CurrentPageSize)
            {
                case PageSize.ActualPageSize:
                    return 1;
                case PageSize.FitToPage:
                    return 1;
                case PageSize.TwoByOne:
                    return 2;
                case PageSize.TwoByTwo:
                    return 4;
                case PageSize.ThreeByTwo:
                    return 6;
                case PageSize.ThreeByThree:
                    return 9;
                default:
                    return 0;
            }
        }
        public static int GetHorizontalPagesCount(PageSize CurrentPageSize)
        {
            switch (CurrentPageSize)
            {
                case PageSize.ActualPageSize:
                    return 1;
                case PageSize.FitToPage:
                    return 1;
                case PageSize.TwoByOne:
                    return 1;
                case PageSize.TwoByTwo:
                    return 2;
                case PageSize.ThreeByTwo:
                    return 2;
                case PageSize.ThreeByThree:
                    return 3;
                default:
                    return 0;
            }
        }
        public static int GetVerticalPagesCount(PageSize CurrentPageSize)
        {
            switch (CurrentPageSize)
            {
                case PageSize.ActualPageSize:
                    return 1;
                case PageSize.FitToPage:
                    return 1;
                case PageSize.TwoByOne:
                    return 2;
                case PageSize.TwoByTwo:
                    return 2;
                case PageSize.ThreeByTwo:
                    return 3;
                case PageSize.ThreeByThree:
                    return 3;
                default:
                    return 0;
            }
        }
        //public static float GetFooterFontSize(PageSize CurrentPageSize, System.Drawing.Graphics gr, System.Drawing.Font[] CurrentFont)
        //{
        //    try
        //    {
        //        switch (CurrentPageSize)
        //        {
        //            case PageSize.ActualPageSize:
        //                return CurrentFont[0].GetHeight(gr);
        //            case PageSize.FitToPage:
        //                return CurrentFont[0].GetHeight(gr);
        //            case PageSize.TwoByOne:
        //                return CurrentFont[1].GetHeight(gr);
        //            case PageSize.TwoByTwo:
        //                return CurrentFont[1].GetHeight(gr);
        //            case PageSize.ThreeByTwo:
        //                return CurrentFont[2].GetHeight(gr);
        //            case PageSize.ThreeByThree:
        //                return CurrentFont[2].GetHeight(gr);
        //            default:
        //                return CurrentFont[0].GetHeight(gr);
        //        }
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        public static System.Drawing.Font GetFooterFont(PageSize CurrentPageSize, System.Drawing.Font[] CurrentFont)
        {
            try
            {
                switch (CurrentPageSize)
                {
                    case PageSize.ActualPageSize:
                        return CurrentFont[0];
                    case PageSize.FitToPage:
                        return CurrentFont[0];
                    case PageSize.TwoByOne:
                        return CurrentFont[1];
                    case PageSize.TwoByTwo:
                        return CurrentFont[1];
                    case PageSize.ThreeByTwo:
                        return CurrentFont[2];
                    case PageSize.ThreeByThree:
                        return CurrentFont[2];
                    default:
                        return CurrentFont[0];
                }
            }
            catch
            {
                return System.Drawing.SystemFonts.CaptionFont;
            }
        }
        public class GraphicsBound
        {
            public float DpiX = 0;
            public float DpiY = 0;
            public float Top = 0;
            public float Left = 0;
            public float Bottom = 0;
            public float Right = 0;
            public float[] FontHeight = new float[3] { 0, 0, 0 };
            public bool bValuesAssigned = false;
        }
        // Get real page bounds based on printable area of the page
        public GraphicsBound GetGraphicsBound(PrinterSettings prSettings, Font[] _ExtendedPrinterSettingsFooterFontBy=null)
        {
            try
            {
                using (Graphics thisGraphics = prSettings.CreateMeasurementGraphics(prSettings.DefaultPageSettings))
                {
                    GraphicsBound thisGraphicsBound = new GraphicsBound();
                    GraphicsUnit myUnit = thisGraphics.PageUnit;
                    thisGraphics.PageUnit = GraphicsUnit.Point;
                    // Translate to units of 1/100 inch
                    RectangleF vpb = thisGraphics.VisibleClipBounds;
                    PointF[] bottomRight = { new PointF(vpb.Size.Width, vpb.Size.Height) };
                    thisGraphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, bottomRight);

                    thisGraphicsBound.Right = bottomRight[0].X;
                    thisGraphicsBound.Bottom = bottomRight[0].Y;
                    thisGraphicsBound.Left = prSettings.DefaultPageSettings.HardMarginX;
                    thisGraphicsBound.Top = prSettings.DefaultPageSettings.HardMarginY;
                    thisGraphicsBound.DpiX = thisGraphics.DpiX;
                    thisGraphicsBound.DpiY = thisGraphics.DpiY;
                    bool bCreatedFonts = false;
                    if (_ExtendedPrinterSettingsFooterFontBy == null)
                    {
                        _ExtendedPrinterSettingsFooterFontBy = new System.Drawing.Font[3];
                        bCreatedFonts = true;
                        for (int scaling = 1; scaling <= 3; scaling++)
                        {
                            if (_ExtendedPrinterSettingsFooterFontBy[scaling - 1] != null)
                            {
                                _ExtendedPrinterSettingsFooterFontBy[scaling - 1].Dispose();
                                _ExtendedPrinterSettingsFooterFontBy[scaling - 1] = null;
                            }
                            Font eFooterFont = FooterFont;
                            if (eFooterFont == null)
                            {
                                eFooterFont = System.Drawing.SystemFonts.CaptionFont;
                            }
                            _ExtendedPrinterSettingsFooterFontBy[scaling - 1] = new System.Drawing.Font(eFooterFont.FontFamily, eFooterFont.Size / (float)scaling, eFooterFont.Style);
                        }
                    }
                    if (_ExtendedPrinterSettingsFooterFontBy != null)
                    {
                        for (int scaling = 0; scaling < 3; scaling++)
                        {
                            thisGraphicsBound.FontHeight[scaling] = _ExtendedPrinterSettingsFooterFontBy[scaling].GetHeight(thisGraphics);
                        }
                    }
                    if (bCreatedFonts)
                    {
                        for (int scaling = 1; scaling <= 3; scaling++)
                        {
                            if (_ExtendedPrinterSettingsFooterFontBy[scaling - 1] != null)
                            {
                                _ExtendedPrinterSettingsFooterFontBy[scaling - 1].Dispose();
                                _ExtendedPrinterSettingsFooterFontBy[scaling - 1] = null;
                            }
                        }
                    }
                    thisGraphicsBound.bValuesAssigned = true;
                    thisGraphics.PageUnit = myUnit;
                    return thisGraphicsBound;
                }
            }
            catch
            {
                return null;
            }
        }
        public static float GetFooterFontSize(PageSize CurrentPageSize, GraphicsBound gr)
        {
            try
            {
                switch (CurrentPageSize)
                {
                    case PageSize.ActualPageSize:
                        return gr.FontHeight[0];
                    case PageSize.FitToPage:
                        return gr.FontHeight[0];
                    case PageSize.TwoByOne:
                        return gr.FontHeight[1];
                    case PageSize.TwoByTwo:
                        return gr.FontHeight[1];
                    case PageSize.ThreeByTwo:
                        return gr.FontHeight[2];
                    case PageSize.ThreeByThree:
                        return gr.FontHeight[2];
                    default:
                        return gr.FontHeight[0];
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
