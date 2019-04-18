﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
namespace gloTransparentScreen
{
    [Designer(typeof(gloLinkLabelDesigner))]
    public partial class gloLinkLabel : UserControl
    {
        public bool drag = false;
        private string labelText;
        public gloLinkLabel()
        {
            InitializeComponent();
            //Set style for double buffering
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint, true);

        }
        //SLR: Showing the Text Property in the designer  
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return this.labelText;
            }
            set
            {
                this.labelText = value;
                if (!this.DesignMode)
                {
                    if (this.Visible)
                    {
                        Form thisForm = this.FindForm();
                        if (thisForm != null)
                        {
                            if (thisForm.Visible)
                            {
                                if (thisForm is gloTransparentScreen.gloTransperentForm)
                                {
                                    gloTransparentScreen.gloTransperentForm myForm = thisForm as gloTransparentScreen.gloTransperentForm;
                                    myForm.UpdateLayeredBackground();
                                }
                            }
                        }
                    }
                }
            }
        }
        [Browsable(true)]
        public event LinkLabelLinkClickedEventHandler LinkClicked;
        
        
        
        public Color LinkColor
        {
            get;
            set;
        }
        private ContentAlignment TextAlignment = ContentAlignment.MiddleLeft;
        public ContentAlignment TextAlign
        {
            get
            {
                return this.TextAlignment;
            }
            set
            {
                this.TextAlignment = value;
                if (!this.DesignMode)
                {
                    this.Invalidate();
                }
            }
        }



        /// <summary>
        /// OnPaint override. This is where the text is rendered vertically.
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
          
            if (this.DesignMode || CheckForRepaint())
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                PaintLabel(this, ref g, new Point(0, 0));
            }
        }
        private bool CheckForRepaint()
        {
            Control parentControl = this.Parent;
            return ( (parentControl != null) && ( !(parentControl is gloTransparentScreen.gloTransperentForm) ) && ((parentControl.BackColor != Color.Transparent) )); //|| (parentControl is GroupBox)));
            //    return ((parentControl != null) && (  (parentControl is GroupBox)));
        }
        private static StringFormat GetStringFormatFromContentAllignment(ContentAlignment ca)
        {
            StringFormat format = new StringFormat();


            Int32 lNum = (Int32)Math.Log((Double)ca, 2);

            format.LineAlignment = (StringAlignment)(lNum / 4);

            format.Alignment = (StringAlignment)(lNum % 4);
            format.FormatFlags = StringFormatFlags.FitBlackBox;
            //switch (ca)
            //{
            //    case ContentAlignment.TopCenter:
            //        format.LineAlignment = StringAlignment.Near;
            //        format. Alignment = StringAlignment.Center;
            //        break;
            //    case ContentAlignment.TopLeft:
            //        format.LineAlignment = StringAlignment.Near;
            //        format.Alignment = StringAlignment.Near;
            //        break;
            //    case ContentAlignment.TopRight:
            //        format.LineAlignment = StringAlignment.Near;
            //        format.Alignment = StringAlignment.Far;
            //        break;
            //    case ContentAlignment.MiddleCenter:
            //        format.LineAlignment = StringAlignment.Center;
            //        format.Alignment = StringAlignment.Center;
            //        break;
            //    case ContentAlignment.MiddleLeft:
            //        format.LineAlignment = StringAlignment.Center;
            //        format.Alignment = StringAlignment.Near;
            //        break;
            //    case ContentAlignment.MiddleRight:
            //        format.LineAlignment = StringAlignment.Center;
            //        format.Alignment = StringAlignment.Far;
            //        break;
            //    case ContentAlignment.BottomCenter:
            //        format.LineAlignment = StringAlignment.Far;
            //        format.Alignment = StringAlignment.Center;
            //        break;
            //    case ContentAlignment.BottomLeft:
            //        format.LineAlignment = StringAlignment.Far;
            //        format.Alignment = StringAlignment.Near;
            //        break;
            //    case ContentAlignment.BottomRight:
            //        format.LineAlignment = StringAlignment.Far;
            //        format.Alignment = StringAlignment.Far;
            //        break;
            //}
            return format;
        }
        public static void PaintLabel(gloTransparentScreen.gloLinkLabel lblText, ref Graphics grLabel, Point pos)
        {

            
            Color controlBackColor = lblText.BackColor;
            int lineMarginX = 1;
            int lineMarginY = 2;
            int lineHeight = 1;
            Rectangle controlRect = new Rectangle(pos.X, pos.Y, lblText.Size.Width, lblText.Size.Height);
            using (Pen labelBorderPen = new Pen(controlBackColor, 0))
            {
                using (SolidBrush labelBackColorBrush = new SolidBrush(controlBackColor))
                {

                    using (SolidBrush labelForeColorBrush = new SolidBrush(lblText.ForeColor))
                    {


                        grLabel.DrawRectangle(labelBorderPen, controlRect);
                        grLabel.FillRectangle(labelBackColorBrush, controlRect);
                        grLabel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        using (StringFormat sf = GetStringFormatFromContentAllignment(lblText.TextAlign))
                        {
                            grLabel.DrawString(lblText.Text, lblText.Font, labelForeColorBrush, controlRect, sf);
                        }
                        int TextWidth = (int) grLabel.MeasureString(lblText.Text, lblText.Font).Width;
                        Rectangle LineRect = new Rectangle(pos.X + lineMarginX, pos.Y - lineMarginY + lblText.Size.Height, TextWidth-lineMarginX, lineHeight);
                        using (SolidBrush linkBrush = new SolidBrush(lblText.LinkColor))
                        {
                            grLabel.FillRectangle(linkBrush, LineRect);
                        }
                    }
                }
            }
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (LinkClicked!=null)
            {
                LinkClicked((object) this, null);
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (LinkClicked != null)
            {
                LinkClicked((object)this, null);
            }
        }
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Cursor = Cursors.Hand;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Hand;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Arrow;
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Cursor = Cursors.Hand;
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Cursor = Cursors.Arrow;
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Cursor = Cursors.Hand;
        }
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    base.OnPaintBackground(e);
        //    Graphics g = e.Graphics;

        //    if (Parent != null && !drag)
        //    {
        //        BackColor = Color.Transparent;
        //        int index = Parent.Controls.GetChildIndex(this);

        //        for (int i = Parent.Controls.Count - 1; i > index; i--)
        //        {
        //            Control c = Parent.Controls[i];
        //            if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
        //            {
        //                Bitmap bmp = new Bitmap(c.Width, c.Height, g);
        //                c.DrawToBitmap(bmp, c.ClientRectangle);

        //                g.TranslateTransform(c.Left - Left, c.Top - Top);
        //                g.DrawImageUnscaled(bmp, Point.Empty);
        //                g.TranslateTransform(Left - c.Left, Top - c.Top);
        //                bmp.Dispose();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        g.Clear(Parent.BackColor);
        //    }

        //}
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (int)Win32.WindowStyles.WS_EX_TRANSPARENT;
                return cp;
            }
        }
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            Rectangle pBounds = this.Bounds;
            pBounds.Inflate(pBounds.Width / 2, pBounds.Height / 2);
            this.Invalidate();
            if (this.Parent != null) this.Parent.Invalidate(pBounds, true);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Rectangle pBounds = this.Bounds;
            pBounds.Inflate(pBounds.Width / 2, pBounds.Height / 2);
            this.Invalidate();
            if (this.Parent != null) this.Parent.Invalidate(pBounds, true);
        }

    }

    internal class gloLinkLabelDesigner : ControlDesigner
    {
        private gloLinkLabel control;


        protected override void OnMouseDragBegin(int x, int y)
        {
            base.OnMouseDragBegin(x, y);
            control = (gloLinkLabel)(this.Control);
            control.drag = true;

        }

        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();
            control = (gloLinkLabel)(this.Control);
            control.drag = false;

        }

    }
}
