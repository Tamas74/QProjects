using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using pdftron.PDF;
using pdftron.Common;
using pdftron.SDF;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace gloEDocumentV3
{

    //public class clsOpacity : UserControl,IDisposable
    //{

    //    private int _nOpacity;

    //    public clsOpacity(int myOpacity)
    //    {

    //        _nOpacity = myOpacity;
    //        this.MouseUp += new MouseEventHandler(clsgloAnnotation_MouseUp);
    //        this.MouseDown += new MouseEventHandler(clsgloAnnotation_MouseDown);
    //        this.MouseMove += new MouseEventHandler(clsgloAnnotation_MouseMove);
    //        this.MouseHover += new EventHandler(clsgloAnnotation_MouseHover);
    //    }
    //    public int Opacity
    //    {
    //        get
    //        {
    //            return _nOpacity;
    //        }
    //        set
    //        {
    //            _nOpacity = value;
    //            this.InvalidateEx();
    //        }
    //    }
    //    protected void InvalidateEx()
    //    {
    //        if (Parent == null)
    //        {
    //            return;
    //        }
    //        Rectangle rc = new Rectangle(this.Location, this.Size);
    //        Parent.Invalidate(rc, true);
    //    }
    //    void clsgloAnnotation_MouseUp(object sender, MouseEventArgs e)
    //    {
    //    }
    //    void clsgloAnnotation_MouseDown(object sender, MouseEventArgs e)
    //    {
    //    }
    //    void clsgloAnnotation_MouseMove(object sender, MouseEventArgs e)
    //    {
    //    }
    //    void clsgloAnnotation_MouseHover(object sender, EventArgs e)
    //    {

    //    }

    //}


    public class

        MyPDFView : PDFViewCtrl
    {

        private int _nOpacity;
        ArrayList openWith = new ArrayList();
        ArrayList openFields = new ArrayList();
        int mypageNo;
      //  Annot myannot;
        class myAnnots
        {
            public int mypageNo;
            public Annot myannot;

            public myAnnots()
            {

            }
            public myAnnots(int pageNo, ref Annot annot)
            {
                mypageNo = pageNo;
                myannot = annot;


            }
        }
        class MyFieldNames
        {
            #region "Private Variable"
            private String _sFiedName = null;
            #endregion "Private Variable"

            #region "Public Variable"
            public String SFiedName
            {
                get { return _sFiedName; }
                set { _sFiedName = value; }
            }
            #endregion "Public Variable"

            #region "Constructor"
            public MyFieldNames(String FiledName)
            {
                _sFiedName = FiledName;

            }
            #endregion "Constructor"
        }


        public MyPDFView(int myOpacity)
        {

            _nOpacity = myOpacity;
            PDFDoc doc = GetDoc();
            mypageNo = doc.GetPageCount();
            for (int j = 0; j < mypageNo; j++)
            {
                Page pg = doc.GetPage(j);
                Obj annots = pg.GetAnnots();
                for (int i = 0; i < annots.Size(); i++)
                {
                    Annot a = pg.GetAnnot(i);
                    myAnnots objMyAnnot = new myAnnots(mypageNo, ref a);
                    openWith.Add(objMyAnnot);
                }
            }

            //
            //this.MouseUp += new MouseEventHandler(clsgloAnnotation_MouseUp);
            //this.MouseDown += new MouseEventHandler(clsgloAnnotation_MouseDown);
            //this.MouseMove += new MouseEventHandler(clsgloAnnotation_MouseMove);
            //this.MouseHover += new EventHandler(clsgloAnnotation_MouseHover);
        }
        #region "Public Variable"
        public int Opacity
        {
            get
            {
                return _nOpacity;
            }
            set
            {
                _nOpacity = value;
                this.InvalidateEx();
            }
        }
        #endregion "Public Variable"
        #region "Creating an Transparent Windows"
        protected void InvalidateEx()
        {
            if (Parent == null)
            {
                return;
            }
            Rectangle rc = new Rectangle(this.Location, this.Size);
            Parent.Invalidate(rc, true);
        }
        #endregion "Creating an Transparent Windows"

        #region "Commented"
        //void clsgloAnnotation_MouseUp(object sender, MouseEventArgs e)
        //{
        //}
        //void clsgloAnnotation_MouseDown(object sender, MouseEventArgs e)
        //{
        //}
        //void clsgloAnnotation_MouseMove(object sender, MouseEventArgs e)
        //{
        //}
        //void clsgloAnnotation_MouseHover(object sender, EventArgs e)
        //{

        //}
        #endregion "Commented"

        public enum CustomToolMode
        {
            e_none,

            // Custom Markup tool
            e_custom_draw,

            // Custom Link-create tool
            e_custom_link_create,

            // Custom Rectangular zoom-in tool
            e_custom_zoomin,

            // Custom Rectangular zoom-out tool
            e_custom_zoomout,

            e_custom_link_action,

            e_custom_mouse_hover,

            // Extends the built-in text selection tool to implement a new tool
            // used to create hyper-links.
            e_custom_text_select_tool,

            // <-- Add new custom tools 

            // Custom TextBox  tool
            e_custom_textBox, //Add Text

            e_custom_checkmark,  //Checkmark

            e_custom_modify_textBox, //Modify Text 

            e_custom_signature //Insert Signature
        }

        public static ToolMode _base_tool_mode = ToolMode.e_pan;
        public static CustomToolMode _tool_mode = CustomToolMode.e_none;

        public MyPDFView(gloEDocumentV3.Forms.frmEDocumentViewer parent, int myOpacity) : base() 
        {
            _parent = parent;
            if (_popup == null)
            {
             _popup = new ContextMenu();
            }
            if (_popup != null)
            {
                _popup.Popup += new EventHandler(PopupEventHandler);
            }
            this.ContextMenu = _popup;
           
            // Customize PDFViewCtrl ...

            // this.ForeColor = Color.White;
            // this.BackColor = Color.White;
            // this.SetPageBorderVisibility(false);

            // Use built-in navigation panel?
            // In case you would like to implement your own document navigation, 
            // disable the  built-in navigation panel and add a custom panel along
            // the lines 'CUSTOM_NAV' code region in 'PDFViewForm.cs'.
            ShowNavToolbar(false);

            // By default PDFNet will use document preferences PDFDocViewPrefs
            // to display navigation panel. To programatically override the initial 
            // panel visibility, call _pdfview.ShowNavPanel(visible) immediately after
            // associating document with the view in 'PDFViewForm.cs'.

            // SetEnabledPanels((int)PDFViewCtrl.PanelType.e_bookmarks+(int)PDFViewCtrl.PanelType.e_thumbview); // Sets which navigation buttons will appear in the nvigation toolbar.
            // EnableScrollbar(false);

            // SetProgressiveRendering(false);
            // SetDrawAnnotations(false);
            // ...


            _nOpacity = myOpacity;
            //this.MouseUp += new MouseEventHandler(clsgloAnnotation_MouseUp);
            //this.MouseDown += new MouseEventHandler(clsgloAnnotation_MouseDown);
            //this.MouseMove += new MouseEventHandler(clsgloAnnotation_MouseMove);
            //this.MouseHover += new EventHandler(clsgloAnnotation_MouseHover);


            //PDFDoc doc = GetDoc();
            //mypageNo = doc.GetPageCount();
            //for modifiacation of the text annotation

            //FieldIterator  oFields = doc.GetFieldIterator();
            //string sFieldsName = "";
            //for (; oFields.HasNext(); oFields.Next())
            //{
            //    Field myField = oFields.Current();
            //    sFieldsName = "";
            //    sFieldsName = myField.GetName();
            //    if (sFieldsName.Substring(0, 4) == "#glo")
            //    {
            //        MyFieldNames oFieldName = new MyFieldNames(sFieldsName);
            //        openFields.Add(oFieldName);
            //    }

            //}


        }
       //Resolved bug : 38872

        //~MyPDFView()
        //{
        //    Dispose(false);
        //}

        //#region "Disposing class"

        //private bool disposed = false;
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _popup.Popup -= new EventHandler(PopupEventHandler);
        //            // base.MouseDown -= new MouseEventHandler(OnMouseDown);
        //            //base.MouseUp -= new MouseEventHandler(OnMouseUp);
        //            //base.MouseMove -= new MouseEventHandler(OnMouseMove);
        //        }
        //    }
        //    disposed = true;
        //}

        ////protected override void Dispose(bool disposing)
        ////{
        ////    if (disposing)
        ////    {
        ////        _popup.Popup -= new EventHandler(PopupEventHandler);
        ////    }
        ////    // You must invoke the Dispose method of the base class.
        ////    base.Dispose(disposing);
        ////    // Free your own state.

        ////}


        //#endregion "end Dispose"


        protected override void OnPaint(PaintEventArgs e)
        {
            if ((_tool_mode == CustomToolMode.e_custom_link_create ||
                 _tool_mode == CustomToolMode.e_custom_zoomin ||
                 _tool_mode == CustomToolMode.e_custom_zoomout || _tool_mode == CustomToolMode.e_custom_textBox ||
                 _tool_mode == CustomToolMode.e_custom_checkmark || _tool_mode == CustomToolMode.e_custom_signature)
                && Capture && _cur_page > 0)
            {
                // Draw Link rectangle annotations on top of the PDF page.
                float top, bottom, left, right;
                if (_start_pt.Y < _end_pt.Y)
                {
                    top = _start_pt.Y;
                    bottom = _end_pt.Y;
                }
                else
                {
                    bottom = _start_pt.Y;
                    top = _end_pt.Y;
                }

                if (_start_pt.X < _end_pt.X)
                {
                    left = _start_pt.X;
                    right = _end_pt.X;
                }
                else
                {
                    right = _start_pt.X;
                    left = _end_pt.X;
                }

                e.Graphics.DrawRectangle(Pens.Red, left, top, right - left, bottom - top);
            }

            if (_freehand_markup != null) // Draw FreeHand annotations on top.
            {
                PagePresentationMode pmode = GetPagePresentationMode();
                int curpage = GetCurrentPage();

                Graphics g = e.Graphics;
                foreach (DictionaryEntry itr in _freehand_markup)
                {
                    //System.Threading.Thread.Sleep(50);

                    int annot_page_num = (int)itr.Key;
                    if ((pmode == PDFViewCtrl.PagePresentationMode.e_single_page && annot_page_num != curpage) ||
                        (pmode == PDFViewCtrl.PagePresentationMode.e_facing && annot_page_num != curpage && annot_page_num != curpage + 1))
                    {
                        continue;
                    }

                    GraphicsState gs = g.Save();
                    Matrix page2scr_mtx = GetDeviceTransform(annot_page_num);
                    g.MultiplyTransform(page2scr_mtx);
                    FreeHandAnnot annot = (FreeHandAnnot)itr.Value;
                    annot.DrawAnnots(g, Pens.Red);
                    g.Restore(gs);
                }
            }
        }

        // By default, PDFViewCtrl will launch URL links in a web browser and 
        // will jump to a destination for 'GoTo' actions. The default behavior can 
        // be overloaded using OnAction method.
        public override Boolean OnAction(pdftron.PDF.Action action)
        {
            if (action.IsValid() == false) return true;

            pdftron.PDF.Action.Type type = action.GetType();
            if (type == pdftron.PDF.Action.Type.e_GoTo)
            {
                Destination dest = action.GetDest();
                if (!dest.IsValid())
                {
                    MessageBox.Show("Destination is not valid", "Custom PDF Action Handler");
                }
                else
                {
                    //MessageBox.Show("  Links to: page number " + dest.GetPage().GetIndex().ToString() + " in this document", "Custom PDF Action Handler"); Changed
                }
            }
            else if (type == pdftron.PDF.Action.Type.e_URI)
            {
                //MessageBox.Show("  Links to: " + action.GetSDFObj().Get("URI").Value().GetAsPDFText(), "Custom PDF Action Handler"); //Changed
            }
            else
            {
                // MessageBox.Show("Other Action Type ", "Custom PDF Action Handler"); // changed
            }

            // If this function returns true, PDFViewCtrl will not execute the default 
            // action internally (in case of URL the default action is to open the URL 
            // link in a web browser).
            return false; // -> execute the default action.
        }

        protected override void OnPaintBackground(PaintEventArgs pevent) { }


        protected PointF _start_pt, _end_pt;
        protected int _cur_page = 0;

        // A dictionary mapping page numbers to FreeHandAnnot-s (used to implement freehand markup tool)
        public Hashtable _freehand_markup = null;
        protected GraphicsPath _curstroke = null;  // Used to track the current stroke.

        protected override void OnMouseDown( MouseEventArgs e)
        {
            try
            {
                //int splp = base.GetSplitPosition();
                //Rect r = base.GetChildWindowPosition();
             
               
                base.OnMouseDown(e);  // First process the event in the base class
                //Resolved Bug # : 38872
                if (_tool_mode != CustomToolMode.e_custom_textBox)
                {
                    return;
                }


                if (_tool_mode == CustomToolMode.e_custom_link_create ||
                    _tool_mode == CustomToolMode.e_custom_zoomin ||
                    _tool_mode == CustomToolMode.e_custom_zoomout || _tool_mode == CustomToolMode.e_custom_textBox ||
                    _tool_mode == CustomToolMode.e_custom_checkmark || _tool_mode == CustomToolMode.e_custom_signature)
                {
                    _cur_page = GetPageNumberFromScreenPt(e.X, e.Y);
                    if (_cur_page < 1)
                    {
                        _cur_page = GetCurrentPage();
                    }

                    // Record the position for the rectangle origin.
                    _start_pt.X = e.X; _start_pt.Y = e.Y;
                    Capture = true;
                }

                else if (_tool_mode == CustomToolMode.e_custom_draw)
                {
                    // Sample code illustrating how to draw new content on top 
                    // of PDF page...
                    _cur_page = GetPageNumberFromScreenPt(e.X, e.Y);
                    if (_cur_page < 1)
                    {
                        _cur_page = GetCurrentPage();
                    }

                    if (_freehand_markup == null) _freehand_markup = new Hashtable();

                    FreeHandAnnot annot;
                    if (!_freehand_markup.ContainsKey(_cur_page))
                    {
                        annot = new FreeHandAnnot();
                        _freehand_markup.Add(_cur_page, annot);
                    }
                    else
                    {
                        annot = (FreeHandAnnot)_freehand_markup[_cur_page];
                    }

                    _curstroke = new GraphicsPath();  // Start a new stroke.
                    annot.AddPath(_curstroke);

                    // Record the current position.
                    double x = e.X, y = e.Y;
                    this.ConvScreenPtToPagePt(ref x, ref y, _cur_page);
                    _start_pt.X = (float)x; _start_pt.Y = (float)y;
                    Capture = true;



                    if (Control.ModifierKeys == Keys.Control)
                    {
                        SetToolMode(ToolMode.e_annot_edit);
                    }
                }
                else if (_tool_mode == CustomToolMode.e_custom_link_action || _tool_mode == CustomToolMode.e_custom_textBox ||
                        _tool_mode == CustomToolMode.e_custom_checkmark || _tool_mode == CustomToolMode.e_custom_signature)
                {
                    // Sample code illustrating how to implement link navigation.
                    if (IsFinishedRendering())
                    {
                        int page_num = GetPageNumberFromScreenPt(e.X, e.Y);
                        if (page_num < 1) return;

                        // Find the click point in page coordinate system...
                        double x = e.X, y = e.Y;
                        ConvScreenPtToPagePt(ref x, ref y, page_num);

                        Page page = GetDoc().GetPage(page_num);
                        int annot_num = page.GetNumAnnots();
                        for (int i = 0; i < annot_num; ++i)
                        {
                            Annot annot = page.GetAnnot(i);
                            // Process only link annotations...
                            if (annot.IsValid() == false ||
                                annot.GetType() != Annot.Type.e_Link) continue;

                            Rect box = annot.GetRect();
                            if (box.Contains(x, y))
                            {
                                // Execute the action - jump to the destination page 
                                pdftron.PDF.Action action = new pdftron.PDF.Annots.Link(annot).GetAction();
                                if (action.IsValid() == false) continue;
                                if (action.GetType() == pdftron.PDF.Action.Type.e_GoTo)
                                {
                                    Destination dest = action.GetDest();
                                    if (dest.IsValid())
                                    {
                                        page_num = dest.GetPage().GetIndex();
                                        SetCurrentPage(page_num);
                                        return;
                                    }
                                }
                                else if (action.GetType() == pdftron.PDF.Action.Type.e_URI)
                                {
                                    // open the browser and jump to the given hyperlink...
                                    string target;

                                    target = action.GetSDFObj().Get("URI").Value().GetAsPDFText();
                                    try
                                    {
                                        System.Diagnostics.Process.Start(target);
                                    }
                                    catch (System.ComponentModel.Win32Exception noBrowser)
                                    {
                                        if (noBrowser.ErrorCode == -2147467259)
                                            MessageBox.Show(noBrowser.Message);
                                    }
                                    catch (System.Exception other)
                                    {
                                        MessageBox.Show(other.Message);
                                    }
                                    break;
                                }
                                else if (action.GetType() == pdftron.PDF.Action.Type.e_GoToR)
                                {
                                    // if the remote document is PDF, possibly open the
                                    // document as a new PDFViewForm...
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
            }
        }



        protected override void OnMouseMove( MouseEventArgs e)
        {
            try
            {
                base.OnMouseMove(e);  // First process the event in the base class
                //Resolved Bug # : 38872
                if (_tool_mode == CustomToolMode.e_custom_modify_textBox)
                {
                    return;
                }

                if ((_tool_mode == CustomToolMode.e_custom_link_create ||
                     _tool_mode == CustomToolMode.e_custom_zoomin ||
                     _tool_mode == CustomToolMode.e_custom_zoomout || _tool_mode == CustomToolMode.e_custom_textBox ||
                      _tool_mode == CustomToolMode.e_custom_checkmark || _tool_mode == CustomToolMode.e_custom_signature)
                    && Capture && _cur_page > 0)
                {
                    _end_pt.X = e.X; _end_pt.Y = e.Y;
                    Invalidate();
                }
                else if (_tool_mode == CustomToolMode.e_custom_mouse_hover)
                {
                    // Sample code showing how to change mouse cursor when hovering
                    // over link annotations (in pan mode)...
                    if (IsFinishedRendering())
                    {
                        int page_num = GetPageNumberFromScreenPt(e.X, e.Y);
                        if (page_num < 1) return;

                        // Find the point in PDF page coordinate system...
                        double x = e.X, y = e.Y;
                        ConvScreenPtToPagePt(ref x, ref y, page_num);

                        Page page = GetDoc().GetPage(page_num);
                        int annot_num = page.GetNumAnnots();
                        bool over_link = false;
                        for (int i = 0; i < annot_num; ++i)
                        {
                            Annot annot = page.GetAnnot(i);
                            if (annot.IsValid() == false ||
                                annot.GetType() != Annot.Type.e_Link) continue;

                            Rect box = annot.GetRect();
                            if (box.Contains(x, y))
                            {
                                over_link = true;
                                break;
                            }
                        }

                        if (over_link) Cursor = Cursors.Cross;
                        else Cursor = Cursors.Hand;
                    }
                }
                else if (_tool_mode == CustomToolMode.e_custom_draw && Capture && _curstroke != null)
                {
                    double x = e.X, y = e.Y;
                    this.ConvScreenPtToPagePt(ref x, ref y, _cur_page);
                    _end_pt.X = (float)x; _end_pt.Y = (float)y;
                    _curstroke.AddLine(_start_pt.Y, _start_pt.X, _end_pt.Y, _end_pt.X);
                    _start_pt.X = _end_pt.X; _start_pt.Y = _end_pt.Y;
                    Invalidate();
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
            }
        }
        StringBuilder modifymyStringBuilder = null;
        protected override void OnMouseUp( MouseEventArgs e)
        {
            //// One way to override built-in content menu. Replace it with your own menu?
            //if (e.Button == MouseButtons.Right) 
            //{
            //	MenuItem menuItem = new MenuItem("Print");
            //	menuItem.Click += new EventHandler(OnPrint);
            //	ContextMenu popup = new ContextMenu();
            //	popup.MenuItems.Add(menuItem);
            //	Rect wnd_pos = GetChildWindowPosition();
            //	popup.Show(this, new System.Drawing.Point(e.X+(int)wnd_pos.x1, e.Y+(int)wnd_pos.y1));
            //	return;
            //}
            try
            {

                base.OnMouseUp(e);  // First process the event in the base class
                //Resolved Bug # : 38872
                if (_tool_mode == CustomToolMode.e_custom_modify_textBox)
                {
                    return;
                }


                if (_tool_mode == CustomToolMode.e_custom_link_create && Capture && _cur_page > 0)
                {
                    // The following sample code illustrates an implementation of link annotation 
                    // tool that can be used to create new links on top of existing PDF documents.
                    PDFDoc doc = GetDoc();
                    Page pg = doc.GetPage(_cur_page);
                    if (pg != null)
                    {
                        _end_pt.X = e.X; _end_pt.Y = e.Y;
                        pdftron.PDF.Action action = CreateLinkHelper();
                        if (action != null)
                        {
                            // Create link annotation position rectangle in PDF page coordinate system.
                            PointF[] pt_arr = { _start_pt, _end_pt };
                            Matrix screen2page = GetDeviceTransform(_cur_page);
                            screen2page.Invert();
                            screen2page.TransformPoints(pt_arr);

                            Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                            pos.Normalize();

                            pdftron.PDF.Annots.Link link = pdftron.PDF.Annots.Link.Create(GetDoc(), pos, action);
                            link.SetBorderStyle(new Annot.BorderStyle(Annot.BorderStyle.Style.e_solid, 1));
                            link.SetColor(new ColorPt(1, 0, 0));
                            pg.AnnotPushBack(link);
                        }
                    }

                    Capture = false;
                    _cur_page = 0;
                    Invalidate();
                }
                else if ((_tool_mode == CustomToolMode.e_custom_zoomin ||
                          _tool_mode == CustomToolMode.e_custom_zoomout)
                    && Capture && _cur_page > 0)
                {
                    // The following sample code illustrates an implementation of rectangular 
                    // zoom-in and zoom-out tool (similar to the one in Acrobat Reader).

                    _end_pt.X = e.X; _end_pt.Y = e.Y;
                    double zoom_width = _end_pt.X - _start_pt.X;
                    double zoom_height = _end_pt.Y - _start_pt.Y;

                    if (Math.Abs(zoom_width) > 20 && Math.Abs(zoom_height) > 20)
                    {
                        double z1, z2;
                        if (_tool_mode == CustomToolMode.e_custom_zoomin)
                        {
                            z1 = Width / Math.Abs(zoom_width);
                            z2 = Height / Math.Abs(zoom_height);
                        }
                        else
                        {
                            z1 = Math.Abs(zoom_width) / Width;
                            z2 = Math.Abs(zoom_height) / Height;
                        }

                        double z = Math.Min(z1, z2) * GetZoom();
                        SetZoom((int)(_start_pt.X + zoom_width / 2.0),
                            (int)(_start_pt.Y + zoom_height / 2.0), z);
                    }

                    Capture = false;
                    _cur_page = 0;
                    Invalidate();
                }
                else if (_tool_mode == CustomToolMode.e_custom_draw && Capture)
                {   // Freehand tool
                    double x = e.X, y = e.Y;
                    this.ConvScreenPtToPagePt(ref x, ref y, _cur_page);
                    _curstroke.AddLine(_start_pt.Y, _start_pt.X, (float)y, (float)x);

                    Invalidate();
                    _curstroke = null;
                    Capture = false;
                    Invalidate();
                }
                else if (_tool_mode == CustomToolMode.e_custom_text_select_tool && HasSelection())
                {
                    // Extends the built-in text selection tool to implement a new tool
                    // used to create hyper-links.
                    pdftron.PDF.Action action = CreateLinkHelper();
                    if (action != null)
                    {
                        Selection s = GetSelection();
                        Double[] pt_arr = s.GetQuads();
                        int num_quads = pt_arr.Length / 8;
                        Page pg = GetDoc().GetPage(s.GetPageNum());
                        for (int i = 0; i < num_quads; ++i)
                        {
                            //Convert a quad (a rectangle with arbitrary rotation) to its axis-aligned bounding box.
                            //Note that if the quad is not axis-aligned, the resulting bounding box could be larger
                            //than the original quad.
                            Double[] box = new Double[4];
                            ConvertQuad2Rect(box, pt_arr, i);

                            pdftron.PDF.Annots.Link link = pdftron.PDF.Annots.Link.Create(GetDoc(), new Rect(box[0], box[1], box[2], box[3]), action);
                            // Set the link appearance (optional)
                            // link.SetBorderStyle(new Annot.BorderStyle(Annot.BorderStyle.Style.e_solid, 1));
                            // link.SetColor(new ColorPt(1, 0, 0));
                            pg.AnnotPushBack(link);
                        }
                    }

                    Update();  // Refresh the view only if links are supposed to be visible.
                }
                #region"Commented"
                //else if (_tool_mode == CustomToolMode.e_custom_textBox && Capture)
                //{
                //   // pdftron.PDF.Annot action = CreateTextBox();
                //    //if (action != null)
                //    //{
                //        Selection s = GetSelection();
                //        Double[] pt_arr = s.GetQuads();
                //        int num_quads = pt_arr.Length / 8;
                //        Page pg = GetDoc().GetPage(s.GetPageNum());
                //        pdftron.SDF.SDFDoc sdfdoc = GetDoc().GetSDFDoc();  
                //        for (int i = 0; i < num_quads; ++i)
                //        {
                //            //Convert a quad (a rectangle with arbitrary rotation) to its axis-aligned bounding box.
                //            //Note that if the quad is not axis-aligned, the resulting bounding box could be larger
                //            //than the original quad.
                //            Double[] box = new Double[4];
                //            ConvertQuad2Rect(box, pt_arr, i);
                //            Field txt = GetDoc().FieldCreate("txt", Field.Type.e_text, GetDoc().CreateIndirectString("gloStream"));
                //            txt.SetFlag(Field.Flag.e_multiline, true);
                //            Annot textBox = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(box[0], box[1], box[2], box[3]), txt);
                //            //pdftron.PDF.Annots.Widget textBox = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(box[0], box[1], box[2], box[3]), txt);
                //            // Set the link appearance (optional)
                //            // link.SetBorderStyle(new Annot.BorderStyle(Annot.BorderStyle.Style.e_solid, 1));
                //            // link.SetColor(new ColorPt(1, 0, 0));
                //            pg.AnnotPushBack(textBox);
                //            txt.RefreshAppearance();
                //            txt.Flatten(pg); 

                //            //pdftron.PDF.Annots.Widget widget = new pdftron.PDF.Annots.Widget(annot); 


                //        }
                //    //}

                //    Update();  // Refresh the view only if links are supposed to be visible.
                //}
                //else if (_tool_mode == CustomToolMode.e_custom_modify_textBox && HasSelection())
                //{
                //    // Extends the built-in text selection tool to implement a new tool
                //    // used to create hyper-links.

                //    //SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                //        StringBuilder myString = null;
                //        Page pg =null;
                //        //Selection s = GetSelection();
                //        //Double[] pt_arr1 = s.GetQuads();
                //        //int num_quads = pt_arr1.Length / 8;
                //        //pg = GetDoc().GetPage(s.GetPageNum());

                //        string strSelectedstr = GetSelection().GetAsUnicode();
                //        if (strSelectedstr != string.Empty)
                //        {
                //            modifymyStringBuilder = new StringBuilder();
                //            modifymyStringBuilder.Append(strSelectedstr);
                //            myString = CreateTextBoxText("modify");
                //        }
                //        if (myString != null)
                //        {
                //            string str = myString.ToString();
                //            _end_pt.X = e.X; _end_pt.Y = e.Y;
                //            PDFDoc doc = GetDoc();
                //            doc.Lock();
                //            pg = doc.GetPage(_cur_page);
                //            //Calculate annotation position rectangle in PDF page coordinate system. 
                //            Matrix screen2page = GetDeviceTransform(_cur_page);
                //            screen2page.Invert();
                //            PointF[] pt_arr = { _start_pt, _end_pt };
                //            screen2page.TransformPoints(pt_arr);
                //            Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                //            pos.Normalize();
                //            // Create line annotation... 
                //            Annot kannot =null;
                //            Field txt ;
                //            //
                //            //try
                //            //{
                //            //    fld = doc.GetField("txt");
                //            //    String old_value = "none";
                //            //    if (fld != null)
                //            //    {
                //            //        if (fld.GetValue() != null)
                //            //            old_value = fld.GetValue().GetAsPDFText();
                //            //        fld.SetValue("This is a new value. The old one was: " + str);
                //            //        doc.RefreshFieldAppearances();
                //            //    }
                //            //}
                //            //catch (Exception ex)
                //            //{
                //            //    MessageBox.Show(ex.ToString());
                //            //}
                //            //
                //           txt = GetDoc().FieldCreate("txt", Field.Type.e_text, str);
                //           txt.SetFlag(Field.Flag.e_multiline, true);
                //           kannot = pdftron.PDF.Annots.Widget.Create(doc, pos, txt);

                //            //Annot kannot = pdftron.PDF.Annots.FreeText.Create(doc, pos);
                //            //kannot.SetContents(str);
                //            //Annot kannot = Annot.Create(doc, Annot.Type.e_Text, pos);
                //            // Set basic line annotation information .
                //            pdftron.SDF.Obj k = kannot.GetSDFObj();
                //            k.PutNumber("F", 4);  // NoZoom 
                //            // k.PutBool("Cap", true); 
                //            // k.PutText("Contents", "myScale"); 
                //            // k.PutName("S", "D"); 
                //            // k.PutText("DS", "font: arial 9pt; text-align:center; line-height: 10.35pt; color:#FF0000"); 
                //            // k.PutText("Subj", "Length Measurement"); 
                //            // k.PutString("IT", "LineDimension"); 
                //            // Set the line coordinates 
                //            k.PutRect("L", pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                //            // Setting line end type 
                //            // pdftron.SDF.Obj le = k.PutArray("LE"); 
                //            // le.PushBackName("Butt"); 
                //            // le.PushBackName("Butt"); 
                //            // pdftron.SDF.Obj lle = k.PutNumber("LLE", 2); 
                //            // Setting color (red) 
                //            ////ColorPt red = new ColorPt(1, 0, 0);
                //            ////kannot.SetColor(red);
                //            // Setting interior color (red) 
                //            // pdftron.SDF.Obj ic = k.PutArray("IC"); 
                //            // ic.PushBackNumber(1); 
                //            // ic.PushBackNumber(0); 
                //            // ic.PushBackNumber(0); 
                //            // Generate the appearance stream for the annotation (recommended). 
                //            ElementWriter w = new ElementWriter();
                //            ElementBuilder b = new ElementBuilder();
                //            b.PathBegin();
                //            b.MoveTo(pt_arr[0].Y, pt_arr[0].X);
                //            b.LineTo(pt_arr[1].Y, pt_arr[1].X);
                //            Element element = b.PathEnd();
                //            element = b.CreateTextRun("3", pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_helvetica), 0);

                //            element.SetPathFill(false);
                //            element.SetPathStroke(true);
                //            GState gs = element.GetGState();
                //            gs.SetStrokeColorSpace(ColorSpace.CreateDeviceRGB());
                //            //gs.SetStrokeColor(red);
                //            w.Begin(doc);
                //            //w.WriteElement(element);
                //            pdftron.SDF.Obj normal_ap = w.End();
                //            w.Dispose();
                //            b.Dispose();
                //            pdftron.SDF.Obj ap = k.PutDict("AP");
                //            ap.Put("N", normal_ap);
                //            normal_ap.PutRect("BBox", pos.x1, pos.y1, pos.x2, pos.y2);
                //            //txt.SetValue(str);
                //            pg.AnnotPushBack(kannot);
                //            //pg.AnnotPushBack(kannot);
                //            kannot.RefreshAppearance();
                //            //SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                //            //kannot.Flatten(pg); 
                //            txt.SetValue("Hii");
                //            doc.Unlock();
                //            Update();
                //            Capture = false;
                //            _cur_page = 0;
                //            Invalidate();
                //            //Update();  // Refresh the view only if links are supposed to be visible.
                //        }
                //}.
                #endregion "Commented"
                else if (_tool_mode == CustomToolMode.e_custom_modify_textBox && HasSelection())
                {
                    SetToolMode(PDFViewCtrl.ToolMode.e_pan);
                }
                //else if (_tool_mode == CustomToolMode.e_custom_modify_textBox && HasSelection())
                //{
                //    StringBuilder myString = null;
                //    pdftron.PDF.Action action = null;
                //    string strSelectedstr = GetSelection().GetAsUnicode();




                //    if (strSelectedstr != string.Empty)
                //    {

                //        modifymyStringBuilder = new StringBuilder();
                //        modifymyStringBuilder.Append(strSelectedstr);
                //        strSelectedstr= actionTextBox();

                //        if (IsFinishedRendering())
                //        {
                //            System.Diagnostics.Debug.Print("First place");
                //            int page_num = GetPageNumberFromScreenPt(e.X, e.Y);
                //            if (page_num < 1) return;

                //            // Find the point in PDF page coordinate system...
                //            double x = e.X, y = e.Y;
                //            ConvScreenPtToPagePt(ref x, ref y, page_num);

                //            Page page = GetDoc().GetPage(page_num);
                //            int annot_num = openWith.Count;
                //            Obj my_num_annots = page.GetAnnots();
                //            bool over_text = false;
                //            Annot annot = null;
                //            System.Diagnostics.Debug.Print(annot_num + "Second place" + page_num + "=" + my_num_annots.Size().ToString() );
                //            for (int i = 0; i < annot_num; ++i)
                //            {

                //                myAnnots objAnnot = (myAnnots)openWith[i];
                //                annot = objAnnot.myannot;
                //                System.Diagnostics.Debug.Print(objAnnot.mypageNo + "Secondsssssssss" + page_num + "=" + annot.GetType().ToString());
                //                if (objAnnot.mypageNo == page_num)
                //                {



                //                    System.Diagnostics.Debug.Print(annot.GetType().ToString());
                //                    if (annot.IsValid() == false ||
                //                        annot.GetType() != Annot.Type.e_Widget) continue;
                //                    System.Diagnostics.Debug.Print("Third place");
                //                    Rect box = annot.GetRect();
                //                    if (box.Contains(x, y))
                //                    {
                //                        System.Diagnostics.Debug.Print("Main place");
                //                        over_text = true;
                //                        //page.AnnotRemove(annot);
                //                        //CreateTextAnnot("Modify", e, strSelectedstr,ref objAnnot);
                //                        break;
                //                    }
                //                }
                //            }
                //            System.Diagnostics.Debug.Print("End place");
                //            if (over_text)
                //            {
                //                //page.AnnotRemove(annot);
                //                System.Diagnostics.Debug.Print(strSelectedstr);
                //                pdftron.PDF.Annots.Widget w = new pdftron.PDF.Annots.Widget(annot);
                //                Field f = w.GetField();
                //                    System.Diagnostics.Debug.Print( "1:"+ f.GetValueAsString());
                //                    System.Diagnostics.Debug.Print("11:" +annot.GetContents());

                //                f.SetValue(strSelectedstr);

                //                f.EraseAppearance();
                //                f.RefreshAppearance(); 
                //                System.Diagnostics.Debug.Print("2:" + f.GetValueAsString());
                //                System.Diagnostics.Debug.Print("22:" + annot.GetContents());
                //                annot.RefreshAppearance();
                //                annot.SetColor(new pdftron.PDF.ColorPt(5, 7, 15));
                //                annot.Flatten(page);
                //                SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                //                f.EraseAppearance();
                //                f.RefreshAppearance();
                //                System.Diagnostics.Debug.Print("3:" + f.GetValueAsString());
                //                System.Diagnostics.Debug.Print("33:" + annot.GetContents());
                //                annot.RefreshAppearance();

                //            }
                //        }


                //            Update();

                //    }

                //    //if (action != null)
                //    //{
                //    //    Selection s = GetSelection();
                //    //    Double[] pt_arr = s.GetQuads();
                //    //    int num_quads = pt_arr.Length / 8;
                //    //    Page pg = GetDoc().GetPage(s.GetPageNum());
                //    //    for (int i = 0; i < num_quads; ++i)
                //    //    {

                //    //        Double[] box = new Double[4];
                //    //        ConvertQuad2Rect(box, pt_arr, i);
                //    //        Field txt = GetDoc().FieldCreate("txt", Field.Type.e_text);
                //    //        txt.SetFlag(Field.Flag.e_multiline, true);
                //    //        Annot kannot = pdftron.PDF.Annots.Widget.Create(GetDoc(), new Rect(box[0], box[1], box[2], box[3]), txt);
                //    //        pg.AnnotPushBack(kannot);
                //    //    }
                //    //}

                //    Update();  // Refresh the view only if links 
                //}
                else if (_tool_mode == CustomToolMode.e_custom_textBox && _cur_page > 0)
                {
                    string str = "";
                    myAnnots objmyAnnot = null;
                    if (IsTextAdded == true)
                    {
                        //  CreateTextAnnot("Add", e, str, ref objmyAnnot);
                        CreateTextAnnot_New("Add", e, str, ref objmyAnnot);
                    }
                    return;
                    /*
                    //Set the code in the function
                    if (IsTextAdded == true)
                    {
                        StringBuilder myString = CreateTextBoxText("add");
                        str = myString.ToString();
                        _end_pt.X = e.X; _end_pt.Y = e.Y;
                        PDFDoc doc = GetDoc();
                        doc.Lock();
                        Page pg = doc.GetPage(_cur_page);
                        //Calculate annotation position rectangle in PDF page coordinate system. 
                        Matrix screen2page = GetDeviceTransform(_cur_page);
                        screen2page.Invert();
                        PointF[] pt_arr = { _start_pt, _end_pt };
                        screen2page.TransformPoints(pt_arr);
                        Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                        pos.Normalize();
                        // Create line annotation... 

                        Field txt = GetDoc().FieldCreate("txt", Field.Type.e_text);
                        txt.SetFlag(Field.Flag.e_multiline, true);

                        Annot kannot = pdftron.PDF.Annots.Widget.Create(doc, pos, txt);
                        txt.SetValue(str);

                        //Annot kannot = pdftron.PDF.Annots.FreeText.Create(doc, pos);
                        //kannot.SetContents(str);
                        //Annot kannot = Annot.Create(doc, Annot.Type.e_Text, pos);
                        // Set basic line annotation information .
                        pdftron.SDF.Obj k = kannot.GetSDFObj();
                        //k.PutText("Contnt", str);
                        //k.PutNumber("F", 4);  // NoZoom 
                        //// k.PutBool("Cap", true); 
                        //// k.PutText("Contents", "myScale"); 
                        //// k.PutName("S", "D"); 
                        //// k.PutText("DS", "font: arial 9pt; text-align:center; line-height: 10.35pt; color:#FF0000"); 
                        //// k.PutText("Subj", "Length Measurement"); 
                        //// k.PutString("IT", "LineDimension"); 
                        //// Set the line coordinates 
                        //k.PutRect("L", pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                        //// Setting line end type 
                        // pdftron.SDF.Obj le = k.PutArray("LE"); 
                        // le.PushBackName("Butt"); 
                        // le.PushBackName("Butt"); 
                        // pdftron.SDF.Obj lle = k.PutNumber("LLE", 2); 
                        // Setting color (red) 
                        ////ColorPt red = new ColorPt(1, 0, 0);
                        ////kannot.SetColor(red);
                        // Setting interior color (red) 
                        // pdftron.SDF.Obj ic = k.PutArray("IC"); 
                        // ic.PushBackNumber(1); 
                        // ic.PushBackNumber(0); 
                        // ic.PushBackNumber(0); 
                        // Generate the appearance stream for the annotation (recommended). 
                        ElementWriter w = new ElementWriter();
                        ElementBuilder b = new ElementBuilder();
                        b.PathBegin();
                        b.MoveTo(pt_arr[0].Y, pt_arr[0].X);
                        b.LineTo(pt_arr[1].Y, pt_arr[1].X);
                        Element element = b.PathEnd();

                        element = b.CreateTextRun(str, pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_helvetica), 0);
                        element.SetPathFill(true);
                        element.SetPathStroke(true);
                        GState gs = element.GetGState();
                        gs.SetStrokeColorSpace(ColorSpace.CreateDeviceRGB());

                        gs.SetStrokeColor(new ColorPt(0, 255, 0));
                        w.Begin(doc);
                        w.WriteElement(element);
                        pdftron.SDF.Obj normal_ap = w.End();
                        w.Dispose();
                        b.Dispose();
                        pdftron.SDF.Obj ap = k.PutDict("AP");
                        ap.Put("AP", normal_ap);
                        normal_ap.PutRect("BBox", pos.x1, pos.y1, pos.x2, pos.y2);
                        normal_ap.PutText("Contnt", str);
                        //

                        //

                        //pg.AnnotPushBack(kannot);
                        myAnnots objMyAnnots = new myAnnots(_cur_page, ref kannot);
                        openWith.Add(objMyAnnots);
                        pg.AnnotPushBack(kannot);
                        kannot.RefreshAppearance();
                        //SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                        //kannot.Flatten(pg);
                        doc.Unlock();
                        Update();
                        Capture = false;
                        _cur_page = 0;
                        Invalidate();
                        IsTextAdded = false;
                    }
                    */

                }
                else if (_tool_mode == CustomToolMode.e_custom_checkmark && _cur_page > 0)
                {
                    _end_pt.X = e.X; _end_pt.Y = e.Y;
                    PDFDoc doc = GetDoc();
                    doc.Lock();
                    Page pg = doc.GetPage(_cur_page);
                    //Calculate annotation position rectangle in PDF page coordinate system. 
                    Matrix screen2page = GetDeviceTransform(_cur_page);
                    screen2page.Invert();
                    PointF[] pt_arr = { _start_pt, _end_pt };
                    screen2page.TransformPoints(pt_arr);
                    Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                    pos.Normalize();
                    // Create line annotation... 
                    //Obj  stm = GetDoc().CreateIndirectDict();
                    Field chk = GetDoc().FieldCreate("CHK", Field.Type.e_check);
                    Annot kannot = pdftron.PDF.Annots.Widget.Create(doc, pos, chk);
                    //Annot kannot = Annot.Create(doc, Annot.Type.e_Text, pos);
                    // Set basic line annotation information .
                    pdftron.SDF.Obj k = kannot.GetSDFObj();
                    k.PutNumber("F", 4);  // NoZoom 
                    // k.PutBool("Cap", true); 
                    // k.PutText("Contents", "myScale"); 
                    // k.PutName("S", "D"); 
                    // k.PutText("DS", "font: arial 9pt; text-align:center; line-height: 10.35pt; color:#FF0000"); 
                    // k.PutText("Subj", "Length Measurement"); 
                    // k.PutString("IT", "LineDimension"); 
                    // Set the line coordinates 
                    k.PutRect("L", pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                    // Setting line end type 
                    // pdftron.SDF.Obj le = k.PutArray("LE"); 
                    // le.PushBackName("Butt"); 
                    // le.PushBackName("Butt"); 
                    // pdftron.SDF.Obj lle = k.PutNumber("LLE", 2); 
                    // Setting color (red) 
                    ////ColorPt red = new ColorPt(1, 0, 0);
                    ////kannot.SetColor(red);
                    // Setting interior color (red) 
                    // pdftron.SDF.Obj ic = k.PutArray("IC"); 
                    // ic.PushBackNumber(1); 
                    // ic.PushBackNumber(0); 
                    // ic.PushBackNumber(0); 
                    // Generate the appearance stream for the annotation (recommended). 
                    ElementWriter w = new ElementWriter();
                    ElementBuilder b = new ElementBuilder();
                    b.PathBegin();
                    b.MoveTo(pt_arr[0].Y, pt_arr[0].X);
                    b.LineTo(pt_arr[1].Y, pt_arr[1].X);
                    Element element = b.PathEnd();
                    element = b.CreateTextRun("3", pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_zapf_dingbats), 0.60);
                    element.SetPathFill(false);
                    element.SetPathStroke(true);
                    GState gs = element.GetGState();
                    gs.SetStrokeColorSpace(ColorSpace.CreateDeviceRGB());

                    //gs.SetStrokeColor(red);
                    w.Begin(doc);
                    w.WriteElement(element);
                    pdftron.SDF.Obj normal_ap = w.End();
             
                    pdftron.SDF.Obj ap = k.PutDict("AP");
                    ap.Put("N", normal_ap);
                    normal_ap.PutRect("BBox", pos.x1, pos.y1, pos.x2, pos.y2);

                    w.Dispose();
                    b.Dispose();

                    ////pg.AnnotPushBack(kannot);
                    pg.AnnotPushBack(kannot);
                    kannot.RefreshAppearance();
                    SetToolMode(PDFViewCtrl.ToolMode.e_pan);
                    //kannot.Flatten(pg); 
                    doc.Unlock();
                    Update();
                    Capture = false;
                    _cur_page = 0;
                    Invalidate();

                }

                else if (_tool_mode == CustomToolMode.e_custom_signature && _cur_page > 0)
                {
                    Obj stm = null;
                    
                    string _strSignFileName = "";
                    string _ProviderName = "";
                    string _SignetureText = "";
                    bool _signaturefound = false;
                    
                    
                    PDFDoc doc = GetDoc();
                    doc.Lock();
                    Page pg = doc.GetPage(_cur_page);
                    //Calculate annotation position rectangle in PDF page coordinate system. 
                    Matrix screen2page = GetDeviceTransform(_cur_page);
                    screen2page.Invert();
                    PointF[] pt_arr = { _start_pt, _end_pt };
                    screen2page.TransformPoints(pt_arr);
                    Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                    pos.Normalize();
                    #region "Generate PDF Doc Object"

                    // string _FilePath = "";
                    string _FolderPath = gloEDocV3Admin.gTemporaryProcessPath + "\\" + _ContainerId.ToString();

                    if (_FolderPath != null)
                    {
                        if (System.IO.Directory.Exists(_FolderPath) == true)
                        {
                            System.IO.Directory.Delete(_FolderPath, true);
                        }
                        System.IO.Directory.CreateDirectory(_FolderPath);
                    }


                    #endregion

                    #region "Creating the Temperory Process pathe"
                    string _FFolderPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath;
                    if (_FFolderPath != null)
                    {
                        if (System.IO.Directory.Exists(_FFolderPath) == false)
                        {
                            System.IO.Directory.CreateDirectory(_FFolderPath);
                        }
                    }
                    #endregion "Creating the Temperory Process pathe"

                    #region "Retrive Binary Image"
                    Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                    if (oDB != null)
                    {
                        DataTable dt = new DataTable();
                        Database.DBParameters oParameters = new gloEDocumentV3.Database.DBParameters();
                        oDB.Connect(false);
                        //Sanjog - Added On 20101011 for login user signature
                        oParameters.Add("@nPatientID", SignatureId, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nFlag", flag, ParameterDirection.Input, SqlDbType.Int);
                        //Sanjog - Added On 20101011 for login user signaturel

                        // oParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("gsp_eDoc_GetProviderSignature", oParameters, out dt);

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0][0].GetType() != typeof(System.DBNull))
                                {
                                    byte[] content = null;
                                    content = (byte[])dt.Rows[0][0];
                                    if (content != null)
                                    {
                                        _strSignFileName = _FFolderPath + "\\" + System.Convert.ToString(DateTime.Now.ToFileTime()) + ".bmp";
                                        //SLR: Stream is not needed 12/22
                                        //     MemoryStream oDataStream = new MemoryStream(content);
                                        FileStream fileStream = new FileStream(_strSignFileName, FileMode.Create);
                                        //oDataStream.WriteTo(fileStream);
                                        fileStream.Write(content, 0, content.Length);
                                        fileStream.Flush();
                                        fileStream.Close();
                                        fileStream.Dispose();
                                    }
                                    _ProviderName = dt.Rows[0]["ProviderName"].ToString();
                                    //_SignetureText =   _ProviderName + " " + DateTime.Now;
                                    _SignetureText = " Document reviewed by " + _ProviderName + " on " + DateTime.Now;
                                    _signaturefound = true;

                                }
                            }
                            dt.Dispose();
                            dt = null;
                        }

                       
                        if (_signaturefound == true)
                        {
                            ElementBuilder b = new ElementBuilder();
                            ElementWriter w = new ElementWriter();
                            pdftron.PDF.Image img = pdftron.PDF.Image.Create(doc, _strSignFileName);
                            int width = img.GetImageWidth();
                            int height = img.GetImageHeight();
                            Element img_element = b.CreateImage(img, 0, 0, width, height);

                            w.Begin(doc);
                            //w.WritePlacedElement(img_element);

                            //writing the Element to the end of the page.
                            //w.WriteElement(img_element);
                            stm = w.End();
                            // Set the bounding box 
                            stm.PutRect("BBox", 0, 0, width, height);
                            stm.PutName("Subtype", "Form");
                            stm.FindObj("Subtype");
                    

                            Field sig = GetDoc().FieldCreate("sig", Field.Type.e_signature, stm);   //creating the field of type signature
                            pdftron.PDF.Annots.Widget a = pdftron.PDF.Annots.Widget.Create(GetDoc(), pos, sig); // right corner setting 
                            w.Dispose();
                            b.Dispose();
                            if (stm != null)
                            {
                                a.GetAppearance();
                                a.SetAppearance(stm, Annot.AnnotationState.e_normal);
                                pg.AnnotPushBack(a);
                                doc.Unlock();
                                Update();
                                Capture = false;
                                _cur_page = 0;
                                Invalidate();
                            }
                           
                        }



                        //}
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
                        }
                        if (oDB != null)
                        {
                            oDB.Dispose();
                            oDB = null;
                        }
                        if (oParameters != null)
                        {
                            oParameters.Dispose();
                            oParameters = null;
                        }
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
            }
        }
        //private string getUniqueID()
        //{
        //    string _returnUnqueID = "";
        //    bool firstTime = true;
        //    Stopwatch myWatch = new Stopwatch();
        //    DateTime myTime = DateTime.Now;
        //    if (firstTime == true)
        //    {
        //        firstTime = false;
        //        myTime = DateTime.Now;
        //        myWatch.Start();
        //    }
        //    TimeSpan TmSp = new TimeSpan(myTime.Ticks + myWatch.ElapsedTicks);
        //    _returnUnqueID = TmSp.Ticks.ToString();
        //    return _returnUnqueID;
        //}
        //private static Stopwatch myWatch = null;
        //private static bool firstTime = true;
        //private static DateTime myTime = DateTime.Now;
        //private static string getUniqueID()
        //{

        //    string _returnUnqueID = "";
        //    if (myWatch == null)
        //    {
        //        myWatch = new Stopwatch();
        //        firstTime = true;
        //    }

        //    if (firstTime == true)
        //    {
        //        firstTime = false;
        //        myTime = DateTime.Now;
        //        myWatch.Start();
        //    }
        //    TimeSpan TmSp = new TimeSpan(myTime.Ticks + myWatch.ElapsedTicks);
        //    _returnUnqueID = TmSp.Ticks.ToString();
        //    return _returnUnqueID;
        //}
        //private void CreateTextAnnot_old(String Type, MouseEventArgs e, string strSelectedstr, ref myAnnots MyAnnots)
        //{
        //    StringBuilder myString = null;
        //    string str = null;

        //    if (Type == "Add")
        //    {
        //        myString = CreateTextBoxText("add");
        //        str = myString.ToString();
        //    }
        //    else
        //    {


        //        str = strSelectedstr;
        //        System.Diagnostics.Debug.Print("str: " + str);
        //    }

        //    if (str == null || str == string.Empty)
        //    {
        //        return;
        //    }

        //    _end_pt.X = e.X; _end_pt.Y = e.Y;
        //    PDFDoc doc = GetDoc();
        //    doc.Lock();
        //    Page pg = doc.GetPage(_cur_page);
        //    //Calculate annotation position rectangle in PDF page coordinate system. 
        //    Matrix screen2page = GetDeviceTransform(_cur_page);
        //    screen2page.Invert();
        //    PointF[] pt_arr = { _start_pt, _end_pt };
        //    screen2page.TransformPoints(pt_arr);
        //    Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
        //    pos.Normalize();
        //    // Create line annotation... 
        //    string fname = "#glo" + getUniqueID();
        //    System.Diagnostics.Debug.Print("fname: " + fname);
        //    //if (Type == "Add")
        //    //{
        //    Field txt = GetDoc().FieldCreate(fname, Field.Type.e_text, str);
        //    txt.SetValue(str);
        //    txt.SetFlag(Field.Flag.e_multiline, true);
        //    Annot kannot = pdftron.PDF.Annots.Widget.Create(doc, pos, txt);
        //    //}

        //    System.Diagnostics.Debug.Print("txt: " + txt.ToString());
        //    //Annot kannot = pdftron.PDF.Annots.FreeText.Create(doc, pos);
        //    //kannot.SetContents(str);
        //    //Annot kannot = Annot.Create(doc, Annot.Type.e_Text, pos);
        //    // Set basic line annotation information .
        //    pdftron.SDF.Obj k = kannot.GetSDFObj();
        //    k.PutNumber("F", 4);  // NoZoom 
        //    // k.PutBool("Cap", true); 
        //    // k.PutText("Contents", "myScale"); 
        //    // k.PutName("S", "D"); 
        //    // k.PutText("DS", "font: arial 9pt; text-align:center; line-height: 10.35pt; color:#FF0000"); 
        //    // k.PutText("Subj", "Length Measurement"); 
        //    // k.PutString("IT", "LineDimension"); 
        //    // Set the line coordinates 
        //    k.PutRect("L", pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
        //    // Setting line end type 
        //    // pdftron.SDF.Obj le = k.PutArray("LE"); 
        //    // le.PushBackName("Butt"); 
        //    // le.PushBackName("Butt"); 
        //    // pdftron.SDF.Obj lle = k.PutNumber("LLE", 2); 
        //    // Setting color (red) 
        //    //ColorPt red = new ColorPt(1, 0, 0);
        //    //kannot.SetColor(red);
        //    // Setting interior color (red)     
        //    // pdftron.SDF.Obj ic = k.PutArray("IC"); 
        //    // ic.PushBackNumber(1); 
        //    // ic.PushBackNumber(0); 
        //    // ic.PushBackNumber(0); 
        //    // Generate the appearance stream for the annotation (recommended). 

        //    ElementWriter w = new ElementWriter();
        //    ElementBuilder b = new ElementBuilder();
        //    b.PathBegin();
        //    b.MoveTo(pt_arr[0].Y, pt_arr[0].X);
        //    b.LineTo(pt_arr[1].Y, pt_arr[1].X);
        //    Element element = b.PathEnd();
        //    pdftron.PDF.Font _myFont = pdftron.PDF.Font.CreateTrueTypeFont(doc, _fFont, true, true);
        //    //element = b.CreateTextBegin(_myFont, 0);
        //    element = b.CreateTextRun("3", _myFont, 200);
        //    //element = b.CreateTextRun("3", pdftron.PDF.Font.CreateCIDTrueTypeFont(doc, _fFont,true,true), 0);
        //    // element = b.CreateTextBegin(_myFont, 12);  


        //    element.SetPathFill(false);
        //    element.SetPathStroke(true);
        //    GState gs = element.GetGState();
        //    gs.SetStrokeColorSpace(ColorSpace.CreateDeviceRGB());

        //    //gs.SetStrokeColor(red);
        //    w.Begin(doc);
        //    w.WriteElement(element);
        //    pdftron.SDF.Obj normal_ap = w.End();
        //    w.Dispose();
        //    b.Dispose();
        //    pdftron.SDF.Obj ap = k.PutDict("AP");
        //    ap.Put("N", normal_ap);
        //    normal_ap.PutRect("BBox", pos.x1, pos.y1, pos.x2, pos.y2);
        //    //pg.AnnotPushBack(kannot);
        //    myAnnots objMyAnnots = null;
        //    if (Type == "Add")
        //    {
        //        objMyAnnots = new myAnnots(_cur_page, ref kannot);
        //        openWith.Add(objMyAnnots);
        //    }
        //    else
        //    {

        //        MyAnnots.myannot = kannot;
        //        System.Diagnostics.Debug.Print("kannot: " + kannot.ToString());

        //    }
        //    pg.AnnotPushBack(kannot);
        //    System.Diagnostics.Debug.Print("kannot :: Completed");
        //    kannot.RefreshAppearance();
        //    SetToolMode(PDFViewCtrl.ToolMode.e_pan);
        //    //kannot.Flatten(pg);
        //    doc.Unlock();
        //    Update();
        //    Capture = false;
        //    _cur_page = 0;
        //    Invalidate();
        //    IsTextAdded = false;
        //}
     

        //private void CreateTextAnnot(String Type, MouseEventArgs e, string strSelectedstr, ref myAnnots MyAnnots)
        //{
        //    StringBuilder myString = null;
        //    string str = null;

        //    if (Type == "Add")
        //    {
        //        myString = CreateTextBoxText("add");
        //        str = myString.ToString();
        //    }
        //    else
        //    {


        //        str = strSelectedstr;
        //        System.Diagnostics.Debug.Print("str: " + str);
        //    }

        //    if (str == null || str == string.Empty)
        //    {
        //        return;
        //    }

        //    _end_pt.X = e.X; _end_pt.Y = e.Y;
        //    PDFDoc doc = GetDoc();
        //    doc.Lock();
        //    Page pg = doc.GetPage(_cur_page);
        //    //Calculate annotation position rectangle in PDF page coordinate system. 
        //    Matrix screen2page = GetDeviceTransform(_cur_page);
        //    screen2page.Invert();
        //    PointF[] pt_arr = { _start_pt, _end_pt };
        //    screen2page.TransformPoints(pt_arr);
        //    Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
        //    pos.Normalize();
        //    // Create line annotation... 
        //    string fname = "#glo" + getUniqueID();
        //    System.Diagnostics.Debug.Print("fname: " + fname);

        //    //pdftron.PDF.Font my_font = pdftron.PDF.Font.CreateTrueTypeFont(doc, _fFont, true, true);
        //    ElementWriter ew = new ElementWriter();
        //    ElementBuilder eb = new ElementBuilder();
        //    Element element;
        //    Obj stm = null;






        //    //GState gs = element.GetGState();
        //    //gs.SetStrokeColorSpace(ColorSpace.CreateDeviceRGB());

        //    //eb.PathBegin();
        //    //eb.MoveTo(pt_arr[0].Y, pt_arr[0].X);
        //    //eb.LineTo(pt_arr[1].Y, pt_arr[1].X);
        //    //eb.PathEnd();
        //    //element = eb.PathEnd();
        //    ew.Begin(pg);

        //    System.Drawing.Font _ffonts = new System.Drawing.Font("Times New Roman", 3);

        //    if (_fFont == null)
        //    {
        //        _fFont = _ffonts;
        //    }

        //    pdftron.PDF.Font _myFont = pdftron.PDF.Font.CreateTrueTypeFont(doc, _fFont, true, true);
        //    element = eb.CreateTextRun(str, _myFont, 200);

        //    //writing the Element to the end of the page.
        //    ew.WriteElement(element);
        //    stm = ew.End();
        //    // Set the bounding box 
        //    stm.PutRect("BBox", pos.x1, pos.y1, pos.x2, pos.y2);
        //    stm.PutText("Content", str);
        //    stm.PutName("Subtype", "Form");
        //    stm.FindObj("Subtype");



        //    ew.Dispose();
        //    eb.Dispose();



        //    //gs.SetStrokeColor(red);
        //    //ew.Begin(doc);
        //    //ew.WriteElement(element);
        //    //stm = ew.End();
        //    //ew.WriteElement(eb.CreateTextEnd());
        //    //stm = ew.End();
        //    //stm.PutRect("BBox", pos.x1, pos.y1, pos.x2, pos.y2);
        //    //stm.PutName("Subtype", "Form");
        //    //stm.FindObj("Subtype");
        //    //ew.Dispose();
        //    //eb.Dispose();
        //    if (stm != null)
        //    {
        //        Field txt = GetDoc().FieldCreate(fname, Field.Type.e_text, stm);
        //        txt.SetFlag(Field.Flag.e_multiline, true);
        //        txt.SetValue(str);//setting the string value to the text box 
        //        Annot kannot = pdftron.PDF.Annots.Widget.Create(doc, pos, txt);
        //        kannot.SetAppearance(stm, Annot.AnnotationState.e_normal);

        //        pg.AnnotPushBack(kannot);

        //    }
        //    //

        //    //myAnnots objMyAnnots = null;
        //    //if (Type == "Add")
        //    //{
        //    //    objMyAnnots = new myAnnots(_cur_page, ref kannot);
        //    //    openWith.Add(objMyAnnots);
        //    //}
        //    //else
        //    //{

        //    //    MyAnnots.myannot = kannot;
        //    //    System.Diagnostics.Debug.Print("kannot: " + kannot.ToString());

        //    //}
        //    //pg.AnnotPushBack(kannot);
        //    //System.Diagnostics.Debug.Print("kannot :: Completed");
        //    //kannot.RefreshAppearance();
        //    SetToolMode(PDFViewCtrl.ToolMode.e_pan);
        //    //kannot.Flatten(pg);
        //    doc.Unlock();
        //    try     //To handle Exception (Bug Id: 35710)
        //    {
        //        doc.RefreshFieldAppearances();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //    IsTextAdded = false;
        //    //Refreshing logic
        //    SetProgressiveRendering(false);
        //    Update();
        //    Capture = false;//required to update
        //    _cur_page = 0;
        //    Invalidate();
        //    UpdatePageLayout();
        //    Refresh();
        //    SetProgressiveRendering(true);
        //}

        private void CreateTextAnnot_New(String Type, MouseEventArgs e, string strSelectedstr, ref myAnnots MyAnnots)
        {
            try
            {
                StringBuilder myString = null;
                string str = null;

                if (Type == "Add")
                {
                    myString = CreateTextBoxText("add");
                    str = myString.ToString();
                   
                }
                else
                {


                    str = strSelectedstr;
                    System.Diagnostics.Debug.Print("str: " + str);
                }

                if (str == null || str == string.Empty)
                {

                    _base_tool_mode = PDFViewCtrl.ToolMode.e_text_struct_select;
                    _tool_mode = MyPDFView.CustomToolMode.e_none;
                    SetToolMode(PDFViewCtrl.ToolMode.e_pan); 
                    Refresh();
                    return;
                }

                _end_pt.X = e.X; _end_pt.Y = e.Y;
                PDFDoc doc = GetDoc();
                //  doc.Lock();
                Page pg = doc.GetPage(_cur_page);
                //Calculate annotation position rectangle in PDF page coordinate system. 
                Matrix screen2page = GetDeviceTransform(_cur_page);
                screen2page.Invert();
                PointF[] pt_arr = { _start_pt, _end_pt };
                screen2page.TransformPoints(pt_arr);
                Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
                pos.Normalize();
                // Create line annotation... 
                //string fname = "#glo" + getUniqueID();
                //System.Diagnostics.Debug.Print("fname: " + fname);


                //Field txt = doc.FieldCreate(fname, Field.Type.e_text, doc.CreateIndirectString(str));
                //txt.SetFlag(Field.Flag.e_multiline, true);
                //txt.SetValue(str);//setting the string value to the text box  

               // Annot kannot = pdftron.PDF.Annots.Widget.Create(doc, pos, txt);
               // Annot kannot = pdftron.PDF.Annots.FreeText.Create(doc, pos);
             

                pdftron.PDF.Annots.FreeText myFreeText = pdftron.PDF.Annots.FreeText.Create(doc, pos);
               // myFreeText.SetTextColor(new ColorPt(_cColor.R, _cColor.G, _cColor.B), 3);
                //myFreeText.SetTextColor(new ColorPt(10, 0, 0),3); 
                
              //  myFreeText.SetDefaultAppearance("/Courier 40 Tf 0 0 1 rg");
                //myFreeText.SetDefaultAppearance("/" + _fFont.Name.ToString() + " 40 Tf 0 0 1 rg");
                ////Obj dr = doc.GetAcroForm().FindObj("DR");
                ////if (dr == null) dr =doc.GetAcroForm().PutDict("DR");

                ////Obj font_dict = dr.FindObj("Font");
                ////if (font_dict == null) font_dict = dr.PutDict("Font");

                ////Obj myfont = font_dict.FindObj("MyFont"); if (myfont == null)
                ////{
                ////    pdftron.PDF.Font fnt = pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_times_bold_italic);
                   
                ////    myfont = fnt.GetSDFObj();
                ////    font_dict = font_dict.Put("MyFont", myfont);
                ////}
             Annot.BorderStyle Bs = new Annot.BorderStyle(Annot.BorderStyle.Style.e_solid, 0);
              myFreeText.SetBorderStyle(Bs);
              

              if (_fFont == null)
              {
                  
                  _fFont = new System.Drawing.Font("Times New Roman", 3); 
              }
             myFreeText.SetDefaultAppearance("/" + ToPdfFontName(_fFont) + " " + _fFont.Size.ToString() + " Tf");// + ((float)(_cColor.R) / 255.0).ToString() + " " + (((float)_cColor.G) / 255.0).ToString() + " " + (((float)_cColor.B) / 255.0).ToString() + " rg");
                //myFreeText.SetColor(new ColorPt(0,0,0,0),0);
                //myFreeText.SetLineColor(new ColorPt(0, 0, 0, 0), 0); 
              myFreeText.SetTextColor(new ColorPt( ( (double)(_cColor.R) ) / 255.0, ( (double)(_cColor.G) ) / 255.0, ( (double)(_cColor.B) ) / 255.0), 3);
            //  myFreeText.SetBorderEffect(pdftron.PDF.Annots.Markup.BorderEffect.e_None);

              myFreeText.SetIntentName(pdftron.PDF.Annots.FreeText.IntentName.e_FreeTextTypeWriter);
              //myFreeText.SetBorderEffectIntensity(1);

            myFreeText.SetAppearance(CreateTextboxAppearance(doc, str, pos.x1, pos.y1, pos.x2, pos.y2), Annot.AnnotationState.e_normal);
          //    doc.GetAcroForm().PutBool("NeedAppearances", true);  
                Annot kannot = new pdftron.PDF.Annot(myFreeText);
                try
                {

                    _fFont.Dispose();
                    _fFont = null;
                }
                catch (Exception)
                {
                }
           //     kannot.SetColor(new ColorPt(0, 0, 0, 0), 0);
               
             //   kannot.SetBorderStyle(Bs);

                //kannot.SetLineColor(new ColorPt(0, 0, 0, 0), 0);
               // kannot.SetTextColor(new ColorPt(_cColor.R, _cColor.G, _cColor.B), 3);
              //  kannot.SetBorderEffect(pdftron.PDF.Annots.Markup.BorderEffect.e_None);
                kannot.SetContents(str); 
               // kannot.SetAppearance(CreateTextboxAppearance(doc, str, pos.x1, pos.y1, pos.x2, pos.y2), Annot.AnnotationState.e_normal);
               // kannot.SetColor(new pdftron.PDF.ColorPt(0,0,125));
                
                kannot.RefreshAppearance();
                pg.AnnotPushBack(kannot);

               // doc.GetAcroForm().PutBool("NeedAppearances", true);                
               // SetToolMode(PDFViewCtrl.ToolMode.e_pan);
                 

                //doc.Unlock();

                try
                {

                    doc.RefreshFieldAppearances();
                }
                catch (Exception)
                {
                }



                IsTextAdded = false;
                //Refreshing logic
                SetProgressiveRendering(false);
                Update();
                Capture = false;//required to update
                _cur_page = 0;
                Invalidate();
                UpdatePageLayout();
                Refresh();
                if (pos != null)
                {
                    pos.Dispose();
                    pos = null;
                }
                _base_tool_mode = PDFViewCtrl.ToolMode.e_annot_edit;
                _tool_mode = MyPDFView.CustomToolMode.e_none;
                SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                SetProgressiveRendering(true);

              

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static Obj CreateTextboxAppearance(PDFDoc doc, string sText, Double x1, Double y1, Double x2, Double y2)
        {
            // Create a checkmark appearance stream ------------------------------------
            ElementBuilder builder = new ElementBuilder();
            ElementWriter writer = new ElementWriter();
            writer.Begin(doc);


           

            writer.WriteElement(builder.CreateTextBegin());

            Element TextElement = builder.CreateTextRun(sText, pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_times_bold_italic), 18);
            TextElement.GetGState().SetFont(pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_helvetica), 2);

            //TextElement.GetGState().SetFillColor(new ColorPt(0, 0, 255));

            //TextElement.GetGState().SetStrokeColor(new ColorPt(0, 255, 0));

            //TextElement.GetGState().SetHorizontalScale(10);

            //TextElement.GetGState().SetTextRenderMode(GState.TextRenderingMode.e_stroke_text);


            writer.WriteElement(TextElement);

            writer.WriteElement(builder.CreateTextEnd());

            Obj stm = writer.End();

          

            // Set the bounding box
            stm.PutRect("BBox", x1, y1, x2, y2);
            stm.PutName("Subtype", "Form");
            // Calling Dispose() on ElementReader/Writer/Builder can result in increased performance and lower memory consumption.
            writer.Dispose();
            builder.Dispose();
            return stm;
        }

        private static string ToPdfFontName(System.Drawing.Font f)
        {
            StringBuilder sb = new StringBuilder(f.Name);
            //StripSpaces(sb, f.Name);
            sb.Replace(" ", ""); 
            if ((f.Style & FontStyle.Bold) != 0 && (f.Style & FontStyle.Italic) != 0)
            {
                sb.Append(",BoldItalic");
            }
            else if ((f.Style & FontStyle.Bold) != 0)
            {
                sb.Append(",Bold");
            }
            else if ((f.Style & FontStyle.Italic) != 0)
            {
                sb.Append(",Italic");
            }

            //if ((f.Style & FontStyle.Strikeout) != 0 && (f.Style & FontStyle.Underline) != 0)
            //{
            //    sb.Append(",Strikeout,Underline");
            //}
            //else if ((f.Style & FontStyle.Strikeout) != 0)
            //{
            //    sb.Append(",Strikeout");
            //}
            //else if ((f.Style & FontStyle.Underline) != 0)
            //{
            //    sb.Append(",Underline");
            //}


            return sb.ToString();
        }


        //private void Create_StamperAnnot(String Type)
        //{
        //    StringBuilder myString = null;
        //    string str = null;

        //    if (Type == "Add")
        //    {
        //        myString = CreateTextBoxText("add");
        //        str = myString.ToString();
        //    }
        //    else
        //    {

        //    }

        //    if (str == null || str == string.Empty)
        //    {
        //        return;
        //    }

        //    _end_pt.X = e.X; _end_pt.Y = e.Y;
        //    PDFDoc doc = GetDoc();
        //    doc.Lock();
        //    Page pg = doc.GetPage(_cur_page);
        //    //Calculate annotation position rectangle in PDF page coordinate system. 
        //    Matrix screen2page = GetDeviceTransform(_cur_page);
        //    screen2page.Invert();
        //    PointF[] pt_arr = { _start_pt, _end_pt };
        //    screen2page.TransformPoints(pt_arr);
        //    Rect pos = new Rect(pt_arr[0].Y, pt_arr[0].X, pt_arr[1].Y, pt_arr[1].X);
        //    pos.Normalize();
        //    // Create line annotation... 
        //    string fname = "#glo" + getUniqueID();
        //    System.Diagnostics.Debug.Print("fname: " + fname);


        //    Stamper s = new Stamper(Stamper.SizeType.e_relative_scale, 0.5, 0.5);
        //    pdftron.PDF.Font myFont = pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_times_roman, true);

        //    s.SetFont(myFont);
        //    s.SetAsAnnotation(true);
        //    s.SetAlignment(Stamper.HorizontalAlignment.e_horizontal_center, Stamper.VerticalAlignment.e_vertical_center);
        //    s.SetFontColor(new ColorPt(0, 0, 3));
        //    //s.SetRotation(45);

        //    s.StampText(doc, str, new PageSet(_cur_page, _cur_page));

        //    SetToolMode(PDFViewCtrl.ToolMode.e_pan);
        //    //kannot.Flatten(pg);
        //    doc.Unlock();
        //    doc.RefreshFieldAppearances();

        //    IsTextAdded = false;
        //    //Refreshing logic
        //    SetProgressiveRendering(false);
        //    Update();
        //    Capture = false;//required to update
        //    _cur_page = 0;
        //    Invalidate();
        //    UpdatePageLayout();
        //    Refresh();
        //    SetProgressiveRendering(true);

        //}

        bool _isTextAdded = false;

        public bool IsTextAdded
        {
            get { return _isTextAdded; }
            set { _isTextAdded = value; }
        }

        long signatureId = 0;

        public long SignatureId
        {
            get { return signatureId; }
            set { signatureId = value; }
        }
        int flag = 0;

        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        long _ContainerId = 0;

        public long ContainerId
        {
            get { return _ContainerId; }
            set { _ContainerId = value; }
        }

        protected void ConvertQuad2Rect(Double[] rect, Double[] quads, int i)
        {
            Double min_x, max_x, min_y, max_y;
            min_x = max_x = quads[i * 8];
            min_y = max_y = quads[i * 8 + 1];

            min_x = Math.Min(min_x, quads[i * 8 + 2]);
            max_x = Math.Max(max_x, quads[i * 8 + 2]);
            min_y = Math.Min(min_y, quads[i * 8 + 3]);
            max_y = Math.Max(max_y, quads[i * 8 + 3]);

            min_x = Math.Min(min_x, quads[i * 8 + 4]);
            max_x = Math.Max(max_x, quads[i * 8 + 4]);
            min_y = Math.Min(min_y, quads[i * 8 + 5]);
            max_y = Math.Max(max_y, quads[i * 8 + 5]);

            min_x = Math.Min(min_x, quads[i * 8 + 6]);
            max_x = Math.Max(max_x, quads[i * 8 + 6]);
            min_y = Math.Min(min_y, quads[i * 8 + 7]);
            max_y = Math.Max(max_y, quads[i * 8 + 7]);

            rect[0] = min_x;
            rect[1] = min_y;
            rect[2] = max_x;
            rect[3] = max_y;
        }

        /*
        // Override built-in double click event?
        protected override void OnDoubleClick(EventArgs e) 
        {
            MessageBox.Show("Custom OnDoubleClick handler", "Test", MessageBoxButtons.OK);
        }	 
				 
        // Override built-in mouse wheel event handling?
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
                GotoPreviousPage();
            else if (e.Delta < 0)
                GotoNextPage();
        }*/

        private ContextMenu _popup = null;

        private void PopupEventHandler(System.Object sender, System.EventArgs e)
        {
            if (_popup != null)
            {
                gloGlobal.cEventHelper.RemoveAllEventHandlers(_popup);
                _popup.MenuItems.Clear();	// Clear all previously added MenuItems.
            }

            //if (HasSelection()) // Add MenuItems to display if there is a text selection.
            //{
            //    MenuItem menuItem1 = new MenuItem("Cop&y");
            //    menuItem1.Click += new EventHandler(OnTextCopy);
            //    _popup.MenuItems.Add(menuItem1);
            //}

            //if (_tool_mode == CustomToolMode.e_custom_mouse_hover)
            //{
            //    MenuItem menuItem2 = new MenuItem("Se&lect All");
            //    menuItem2.Click += new EventHandler(OnSelectAll);
            //    _popup.MenuItems.Add(menuItem2);

            //    MenuItem menuItem3 = new MenuItem("D&eselect All");
            //    menuItem3.Click += new EventHandler(OnDeselectAll);
            //    _popup.MenuItems.Add(menuItem3);

            //}

            {   // Context menu for all tool modes.
                //MenuItem menuItem1 = new MenuItem("Hand Tool");
                //menuItem1.Click += new EventHandler(OnSelectHandTool);
                //_popup.MenuItems.Add(menuItem1);

                //MenuItem menuItem2 = new MenuItem("Text Select Tool");
                //menuItem2.Click += new EventHandler(OnSelectTextTool);
                //_popup.MenuItems.Add(menuItem2);

                ////commented to   resolved bug: 35749
                //MenuItem menuItem3 = new MenuItem("Print");
                //menuItem3.Click += new EventHandler(OnPrint);
                //_popup.MenuItems.Add(menuItem3);
            }
        }

        public void OnTextCopy(object sender, EventArgs e)
        {
            Selection sel = GetSelection();
            // NET2 method: Clipboard.SetText(sel.GetText(), TextDataFormat.Html);
            // doesn't work in some applications, so we provide our own implementation:
            MyPDFView.CopyHtmlToClipBoard(sel.GetAsHtml(), sel.GetAsUnicode());
        }

        private void OnSelectAll(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void OnDeselectAll(object sender, EventArgs e)
        {
            ClearSelection();
        }

        // private void OnSelectTextTool(object sender, EventArgs e) { _parent.SetToolMode(CustomToolMode.e_text_rect_select); }
        // private void OnSelectHandTool(object sender, EventArgs e) {	_parent.SetToolMode(CustomToolMode.e_pan); }

        private void OnPrint(object sender, EventArgs e)
        {
            // _parent.Print();
        }

        private gloEDocumentV3.Forms.frmEDocumentViewer _parent;

        // A utility method to copy HTML selection to clipboard.
        static public void CopyHtmlToClipBoard(string html, string txt)
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;

            string begin = "Version:0.9\r\nStartHTML:{0:000000}\r\nEndHTML:{1:000000}"
                + "\r\nStartFragment:{2:000000}\r\nEndFragment:{3:000000}\r\n";

            string html_begin = "<html>\r\n<head>\r\n"
                + "<meta http-equiv=\"Content-Type\""
                + " content=\"text/html; charset=" + enc.WebName + "\">\r\n"
                + "<title>HTML clipboard</title>\r\n</head>\r\n<body>\r\n"
                + "<!--StartFragment-->";

            string html_end = "<!--EndFragment-->\r\n</body>\r\n</html>\r\n";

            string begin_sample = String.Format(begin, 0, 0, 0, 0);

            int count_begin = enc.GetByteCount(begin_sample);
            int count_html_begin = enc.GetByteCount(html_begin);
            int count_html = enc.GetByteCount(html);
            int count_html_end = enc.GetByteCount(html_end);

            string html_total = String.Format(
                begin
                , count_begin
                , count_begin + count_html_begin + count_html + count_html_end
                , count_begin + count_html_begin
                , count_begin + count_html_begin + count_html
                ) + html_begin + html + html_end;

            DataObject obj = new DataObject();
            MemoryStream myMemory = new System.IO.MemoryStream( enc.GetBytes(html_total) );
            obj.SetData(DataFormats.Html, myMemory) ;

            obj.SetData(DataFormats.UnicodeText, txt);
            try
            {
                Clipboard.SetDataObject(obj, true);
            }
            catch //(Exception Ex2)
            {

            }
            finally
            {
                if (myMemory != null)
                {
                    try
                    {
                        myMemory.Close();
                    }
                    catch
                    {
                    }
                    try
                    {
                        myMemory.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private pdftron.PDF.Action CreateLinkHelper()
        {
            LinkCreate dlg = new LinkCreate();
            if (dlg.ShowDialog(dlg.Parent) == DialogResult.OK)
            {
                if (dlg.link.Text == "")
                {
                    MessageBox.Show("The link text was not entered.", "PDFViewCtrl", MessageBoxButtons.OK);
                }
                else
                {
                    pdftron.PDF.Action action = null;
                    if (dlg.goto_webpage.Checked)
                    {
                        if (dlg.link.Text.StartsWith("http://"))
                        {
                            // Create a URI action
                            pdftron.SDF.Obj a = GetDoc().CreateIndirectDict();
                            a.PutName("S", "URI");
                            a.PutString("URI", dlg.link.Text);
                            action = new pdftron.PDF.Action(a);
                        }
                        else
                        {
                            MessageBox.Show("The hyperlink must start with 'http://'", "PDFViewCtrl", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        try
                        {
                            int page_num = System.Convert.ToInt32(dlg.link.Text);
                            if (page_num < 1 || page_num > GetDoc().GetPageCount())
                            {
                                MessageBox.Show("Invalid page number.", "PDFViewCtrl", MessageBoxButtons.OK);
                            }
                            else
                            {
                                Page dest_page = GetDoc().GetPage(page_num);
                                action = pdftron.PDF.Action.CreateGoto(Destination.CreateFitH(dest_page, dest_page.GetPageHeight()));
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Invalid page number.", "PDFViewCtrl", MessageBoxButtons.OK);
                        }
                    }
                    dlg.Dispose();
                    dlg = null;
                    return action;
                }
              
            }
            dlg.Dispose();
            dlg = null;
            return null;
        }
        System.Text.StringBuilder myStringBuilder = new StringBuilder();
        //pdftron.PDF.Action action = null;

        private String actionTextBox()
        {


            TextBox objAddText1 = new TextBox(modifymyStringBuilder);
            objAddText1.ShowDialog(objAddText1.Parent);
            myStringBuilder = objAddText1.MyStringBld;
            String str = myStringBuilder.ToString();

            //pdftron.SDF.Obj a = GetDoc().CreateIndirectDict();
            //a.PutName("S", "URI");
            //a.PutString("URI", str);
            //action = new pdftron.PDF.Action(a);

            objAddText1.Dispose();
            objAddText1 = null;


            return str;
        }
        System.Drawing.Font _fFont = null;
        System.Drawing.Color _cColor = default(System.Drawing.Color);
        private StringBuilder CreateTextBoxText(string Modifier)
        {

            if (Modifier == "add")
            {
                TextBox objAddText = new TextBox();
                objAddText.ShowDialog(objAddText.Parent);
               // System.Drawing.Font _ffonts = new System.Drawing.Font("Times New Roman", 20);
                if (objAddText.FFont == null)
                {
                    objAddText.FFont =objAddText.get_Txt_AnnotText_Font();
                }
               _fFont = (System.Drawing.Font) objAddText.FFont.Clone();
               _cColor = objAddText.CColor;
                //_fFont = _ffonts;
                //objAddText.FFont = _ffonts;

                myStringBuilder = objAddText.MyStringBld;
                objAddText.Dispose();
                objAddText = null;
            }
            else if (Modifier == "modify")
            {
                TextBox objAddText1 = new TextBox(modifymyStringBuilder);
                objAddText1.ShowDialog(objAddText1.Parent);
                myStringBuilder = objAddText1.MyStringBld;

                objAddText1.Dispose();
                objAddText1 = null;
            }

            return myStringBuilder;
        }

       

    }
    public class FreeHandAnnot
    {
        private ArrayList _paths = new ArrayList();

        public void AddPath(GraphicsPath path)
        {
            _paths.Add(path);
        }

        public void DrawAnnots(Graphics g, Pen pen)
        {
            for (int i = 0; i < _paths.Count; ++i) // Draw all FreeHand strokes
            {
                g.DrawPath(pen, (GraphicsPath)_paths[i]);
            }
        }

        //private ElementBuilder _builder = new ElementBuilder();
        //private ElementWriter _writer = new ElementWriter();

        //public void DrawAnnots(Page page, Pen pen)
        //{
        //    _writer.Begin(page);  // Begin writing to PDF page
        //    _builder.Reset(); 	  // Reset GState to default graphics state

        //    Matrix2D inv_mtx = page.GetDefaultMatrix();
        //    double x, y;

        //    for (int i = 0; i < _paths.Count; ++i) // Draw all FreeHand strokes
        //    {
        //        GraphicsPath p = (GraphicsPath)_paths[i];

        //        GraphicsPathIterator pitr = new GraphicsPathIterator(p);
        //        if (pitr.Count > 0)
        //        {
        //            bool IsClosed;
        //            int numSubpaths = pitr.NextSubpath(p, out IsClosed);

        //            _builder.PathBegin();

        //            byte subPathPointType;
        //            int pointTypeStartIndex, pointTypeEndIndex, numPointsFound;

        //            while ((numPointsFound = pitr.NextPathType(out subPathPointType, out pointTypeStartIndex, out pointTypeEndIndex)) != 0)
        //            {
        //                if (subPathPointType == (byte)PathPointType.Line)
        //                {
        //                    x = p.PathPoints[pointTypeStartIndex].Y; y = p.PathPoints[pointTypeStartIndex].X;
        //                    inv_mtx.Mult(ref x, ref y);
        //                    _builder.MoveTo(x, y);
        //                    for (int j = pointTypeStartIndex + 1; j <= pointTypeEndIndex; ++j)
        //                    {
        //                        x = p.PathPoints[j].Y; y = p.PathPoints[j].X;
        //                        inv_mtx.Mult(ref x, ref y);
        //                        _builder.LineTo(x, y);
        //                    }
        //                }
        //            }

        //            Element element = _builder.PathEnd();	// path geometry is completed
        //            element.SetPathFill(false);		// this path is should not filled
        //            element.SetPathStroke(true);

        //            // Set the path color space and color
        //            GState gstate = element.GetGState();
        //            gstate.SetStrokeColorSpace(ColorSpace.CreateDeviceRGB());
        //            Color pc = pen.Color;
        //            gstate.SetStrokeColor(new ColorPt(pc.R / 255.0, pc.G / 255.0, pc.B / 255.0));
        //            gstate.SetLineWidth(1);
        //            _writer.WriteElement(element);
        //        }
        //    }

        //    _writer.End();  // save changes to the page
        //}
    }
    public class LinkCreate : System.Windows.Forms.Form
    {
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.TextBox link;
        public System.Windows.Forms.RadioButton goto_page;
        public System.Windows.Forms.RadioButton goto_webpage;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public LinkCreate()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.goto_webpage = new System.Windows.Forms.RadioButton();
            this.goto_page = new System.Windows.Forms.RadioButton();
            this.link = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(200, 272);
            this.OK.Name = "OK";
            this.OK.TabIndex = 0;
            this.OK.Text = "Ok";
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(280, 272);
            this.Cancel.Name = "Cancel";
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.goto_webpage);
            this.groupBox1.Controls.Add(this.goto_page);
            this.groupBox1.Controls.Add(this.link);
            this.groupBox1.Location = new System.Drawing.Point(16, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 136);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Link Action";
            // 
            // goto_webpage
            // 
            this.goto_webpage.Checked = true;
            this.goto_webpage.Location = new System.Drawing.Point(16, 32);
            this.goto_webpage.Name = "goto_webpage";
            this.goto_webpage.Size = new System.Drawing.Size(256, 24);
            this.goto_webpage.TabIndex = 2;
            this.goto_webpage.TabStop = true;
            this.goto_webpage.Text = "Open a webpage";
            // 
            // goto_page
            // 
            this.goto_page.Location = new System.Drawing.Point(16, 64);
            this.goto_page.Name = "goto_page";
            this.goto_page.Size = new System.Drawing.Size(256, 24);
            this.goto_page.TabIndex = 1;
            this.goto_page.Text = "Goto a page view";
            // 
            // link
            // 
            this.link.Location = new System.Drawing.Point(16, 104);
            this.link.Name = "link";
            this.link.Size = new System.Drawing.Size(288, 22);
            this.link.TabIndex = 0;
            this.link.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(16, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Link Appearance";
            // 
            // LinkCreate
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(368, 304);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Name = "LinkCreate";
            this.Text = "Create Link";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

    }




}
