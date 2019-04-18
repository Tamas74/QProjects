namespace DrawingControl
{
    partial class DrawingPad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
                try
                {
                    if (dlgOpenFile != null)
                    {
                         
                        dlgOpenFile.Dispose();
                        dlgOpenFile = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (colorDialog1 != null)
                    {

                        colorDialog1.Dispose();
                        colorDialog1 = null;
                    }
                }
                catch
                {
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingPad));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.tlbMain = new gloGlobal.gloToolStripIgnoreFocus();
            this.btnOpenfile = new System.Windows.Forms.ToolStripButton();
            this.btnInsert = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnLine = new System.Windows.Forms.ToolStripButton();
            this.btnRectangle = new System.Windows.Forms.ToolStripButton();
            this.btnFillRectangle = new System.Windows.Forms.ToolStripButton();
            this.btnEllipsis = new System.Windows.Forms.ToolStripButton();
            this.btnFilledEllipse = new System.Windows.Forms.ToolStripButton();
            this.btnstrline = new System.Windows.Forms.ToolStripButton();
            this.btnColor = new System.Windows.Forms.ToolStripButton();
            this.tlbdrp_eraser = new System.Windows.Forms.ToolStripDropDownButton();
            this.tlpEraser1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpEraser2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpEraser3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpcmb_Width = new System.Windows.Forms.ToolStripComboBox();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlfill = new System.Windows.Forms.Panel();
            this.picDrawing = new System.Windows.Forms.PictureBox();
            this.pnlCanvasBottom = new System.Windows.Forms.Panel();
            this.pnlCanvasRight = new System.Windows.Forms.Panel();
            this.pnlCanvasLeft = new System.Windows.Forms.Panel();
            this.pnlCanvasTop = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pnlTop.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.pnlfill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawing)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTop.BackgroundImage")));
            this.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTop.Controls.Add(this.tlbMain);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1143, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // tlbMain
            // 
            this.tlbMain.BackColor = System.Drawing.Color.Transparent;
            this.tlbMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenfile,
            this.btnInsert,
            this.btnClear,
            this.btnLine,
            this.btnRectangle,
            this.btnFillRectangle,
            this.btnEllipsis,
            this.btnFilledEllipse,
            this.btnstrline,
            this.btnColor,
            this.tlbdrp_eraser,
            this.tlpcmb_Width,
            this.btnClose});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(1143, 50);
            this.tlbMain.TabIndex = 0;
            this.tlbMain.Text = "toolStrip1";
            // 
            // btnOpenfile
            // 
            this.btnOpenfile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenfile.Image")));
            this.btnOpenfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenfile.Name = "btnOpenfile";
            this.btnOpenfile.Size = new System.Drawing.Size(72, 47);
            this.btnOpenfile.Text = "Open File";
            this.btnOpenfile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenfile.Click += new System.EventHandler(this.btnOpenfile_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Image = ((System.Drawing.Image)(resources.GetObject("btnInsert.Image")));
            this.btnInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 47);
            this.btnInsert.Text = "   Insert   ";
            this.btnInsert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInsert.ToolTipText = "Insert Image";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 47);
            this.btnClear.Text = "   Clear   ";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClear.ToolTipText = "Clear Picture";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLine
            // 
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(62, 47);
            this.btnLine.Text = "   Line   ";
            this.btnLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLine.ToolTipText = "Draw Line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangle.Image")));
            this.btnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(75, 47);
            this.btnRectangle.Text = "Rectangle";
            this.btnRectangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRectangle.ToolTipText = "Draw Rectangle";
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnFillRectangle
            // 
            this.btnFillRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnFillRectangle.Image")));
            this.btnFillRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFillRectangle.Name = "btnFillRectangle";
            this.btnFillRectangle.Size = new System.Drawing.Size(115, 47);
            this.btnFillRectangle.Text = "Filled Rectangle";
            this.btnFillRectangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFillRectangle.ToolTipText = "Draw Filled Rectangle";
            this.btnFillRectangle.Click += new System.EventHandler(this.btnFillRectangle_Click);
            // 
            // btnEllipsis
            // 
            this.btnEllipsis.Image = ((System.Drawing.Image)(resources.GetObject("btnEllipsis.Image")));
            this.btnEllipsis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEllipsis.Name = "btnEllipsis";
            this.btnEllipsis.Size = new System.Drawing.Size(58, 47);
            this.btnEllipsis.Text = " Ellipse";
            this.btnEllipsis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEllipsis.ToolTipText = "Draw Ellipse";
            this.btnEllipsis.Click += new System.EventHandler(this.btnEllipsis_Click);
            // 
            // btnFilledEllipse
            // 
            this.btnFilledEllipse.Image = ((System.Drawing.Image)(resources.GetObject("btnFilledEllipse.Image")));
            this.btnFilledEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilledEllipse.Name = "btnFilledEllipse";
            this.btnFilledEllipse.Size = new System.Drawing.Size(94, 47);
            this.btnFilledEllipse.Text = "Filled Ellipse";
            this.btnFilledEllipse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilledEllipse.ToolTipText = "Draw Filled Ellipse";
            this.btnFilledEllipse.Click += new System.EventHandler(this.btnFilledEllipse_Click);
            // 
            // btnstrline
            // 
            this.btnstrline.Image = ((System.Drawing.Image)(resources.GetObject("btnstrline.Image")));
            this.btnstrline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnstrline.Name = "btnstrline";
            this.btnstrline.Size = new System.Drawing.Size(94, 47);
            this.btnstrline.Text = "Straight Line";
            this.btnstrline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnstrline.ToolTipText = "Draw Straight Line";
            this.btnstrline.Click += new System.EventHandler(this.btnstrline_Click);
            // 
            // btnColor
            // 
            this.btnColor.Image = ((System.Drawing.Image)(resources.GetObject("btnColor.Image")));
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(69, 47);
            this.btnColor.Text = "   Color   ";
            this.btnColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnColor.ToolTipText = "Change Color";
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // tlbdrp_eraser
            // 
            this.tlbdrp_eraser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlpEraser1,
            this.tlpEraser2,
            this.tlpEraser3});
            this.tlbdrp_eraser.Image = ((System.Drawing.Image)(resources.GetObject("tlbdrp_eraser.Image")));
            this.tlbdrp_eraser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbdrp_eraser.Name = "tlbdrp_eraser";
            this.tlbdrp_eraser.Size = new System.Drawing.Size(79, 47);
            this.tlbdrp_eraser.Text = "  Eraser  ";
            this.tlbdrp_eraser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbdrp_eraser.ToolTipText = "Erase Drawing";
            this.tlbdrp_eraser.Click += new System.EventHandler(this.tlbdrp_eraser_Click);
            // 
            // tlpEraser1
            // 
            this.tlpEraser1.Image = ((System.Drawing.Image)(resources.GetObject("tlpEraser1.Image")));
            this.tlpEraser1.Name = "tlpEraser1";
            this.tlpEraser1.Size = new System.Drawing.Size(151, 38);
            this.tlpEraser1.Text = "Small";
            this.tlpEraser1.ToolTipText = "Select Small Eraser";
            this.tlpEraser1.Click += new System.EventHandler(this.tlpEraser1_Click);
            // 
            // tlpEraser2
            // 
            this.tlpEraser2.Image = ((System.Drawing.Image)(resources.GetObject("tlpEraser2.Image")));
            this.tlpEraser2.Name = "tlpEraser2";
            this.tlpEraser2.Size = new System.Drawing.Size(151, 38);
            this.tlpEraser2.Text = "Medium";
            this.tlpEraser2.ToolTipText = "Select Medium Eraser";
            this.tlpEraser2.Click += new System.EventHandler(this.tlpEraser2_Click);
            // 
            // tlpEraser3
            // 
            this.tlpEraser3.Image = ((System.Drawing.Image)(resources.GetObject("tlpEraser3.Image")));
            this.tlpEraser3.Name = "tlpEraser3";
            this.tlpEraser3.Size = new System.Drawing.Size(151, 38);
            this.tlpEraser3.Text = "Large";
            this.tlpEraser3.ToolTipText = "Select Large Eraser";
            this.tlpEraser3.Click += new System.EventHandler(this.tlpEraser3_Click);
            // 
            // tlpcmb_Width
            // 
            this.tlpcmb_Width.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tlpcmb_Width.Name = "tlpcmb_Width";
            this.tlpcmb_Width.Size = new System.Drawing.Size(75, 50);
            this.tlpcmb_Width.ToolTipText = "Change Pen Width";
            this.tlpcmb_Width.SelectedIndexChanged += new System.EventHandler(this.tlpcmb_Width_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 47);
            this.btnClose.Text = "    Close    ";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.ToolTipText = "Close Drawing Pad";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlfill
            // 
            this.pnlfill.AutoScroll = true;
            this.pnlfill.BackColor = System.Drawing.Color.Transparent;
            this.pnlfill.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlfill.BackgroundImage")));
            this.pnlfill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlfill.Controls.Add(this.picDrawing);
            this.pnlfill.Controls.Add(this.pnlCanvasBottom);
            this.pnlfill.Controls.Add(this.pnlCanvasRight);
            this.pnlfill.Controls.Add(this.pnlCanvasLeft);
            this.pnlfill.Controls.Add(this.pnlCanvasTop);
            this.pnlfill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlfill.Location = new System.Drawing.Point(0, 50);
            this.pnlfill.Name = "pnlfill";
            this.pnlfill.Size = new System.Drawing.Size(1143, 837);
            this.pnlfill.TabIndex = 1;
            // 
            // picDrawing
            // 
            this.picDrawing.BackColor = System.Drawing.Color.White;
            this.picDrawing.Location = new System.Drawing.Point(18, 18);
            this.picDrawing.Name = "picDrawing";
            this.picDrawing.Size = new System.Drawing.Size(1106, 801);
            this.picDrawing.TabIndex = 4;
            this.picDrawing.TabStop = false;
            this.picDrawing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDrawing_MouseDown);
            this.picDrawing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDrawing_MouseMove);
            this.picDrawing.Paint += new System.Windows.Forms.PaintEventHandler(this.picDrawing_Paint);
            this.picDrawing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picDrawing_MouseUp);
            // 
            // pnlCanvasBottom
            // 
            this.pnlCanvasBottom.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlCanvasBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCanvasBottom.Location = new System.Drawing.Point(18, 819);
            this.pnlCanvasBottom.Name = "pnlCanvasBottom";
            this.pnlCanvasBottom.Size = new System.Drawing.Size(1106, 18);
            this.pnlCanvasBottom.TabIndex = 3;
            // 
            // pnlCanvasRight
            // 
            this.pnlCanvasRight.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlCanvasRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCanvasRight.Location = new System.Drawing.Point(1124, 18);
            this.pnlCanvasRight.Name = "pnlCanvasRight";
            this.pnlCanvasRight.Size = new System.Drawing.Size(19, 819);
            this.pnlCanvasRight.TabIndex = 2;
            // 
            // pnlCanvasLeft
            // 
            this.pnlCanvasLeft.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlCanvasLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCanvasLeft.Location = new System.Drawing.Point(0, 18);
            this.pnlCanvasLeft.Name = "pnlCanvasLeft";
            this.pnlCanvasLeft.Size = new System.Drawing.Size(18, 819);
            this.pnlCanvasLeft.TabIndex = 1;
            // 
            // pnlCanvasTop
            // 
            this.pnlCanvasTop.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlCanvasTop.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlCanvasTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCanvasTop.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvasTop.Name = "pnlCanvasTop";
            this.pnlCanvasTop.Size = new System.Drawing.Size(1143, 18);
            this.pnlCanvasTop.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Line.ico");
            this.imageList1.Images.SetKeyName(1, "empty rectangle_03.ico");
            this.imageList1.Images.SetKeyName(2, "rectangle.ico");
            this.imageList1.Images.SetKeyName(3, "empty ellipse_02.ico");
            this.imageList1.Images.SetKeyName(4, "ellipse.ico");
            this.imageList1.Images.SetKeyName(5, "Color.ico");
            this.imageList1.Images.SetKeyName(6, "Clear All Cirle.ico");
            this.imageList1.Images.SetKeyName(7, "Open File.ico");
            this.imageList1.Images.SetKeyName(8, "Insert Image.ico");
            this.imageList1.Images.SetKeyName(9, "erase.ico");
            this.imageList1.Images.SetKeyName(10, "close.ico");
            this.imageList1.Images.SetKeyName(11, "mini.ico");
            this.imageList1.Images.SetKeyName(12, "squr_mini.ico");
            this.imageList1.Images.SetKeyName(13, "squr.ico");
            this.imageList1.Images.SetKeyName(14, "Curve_01.ico");
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "dlgOpenFile";
            // 
            // DrawingPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.pnlfill);
            this.Controls.Add(this.pnlTop);
            this.Name = "DrawingPad";
            this.Size = new System.Drawing.Size(1143, 887);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.pnlfill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDrawing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlfill;
        private gloGlobal.gloToolStripIgnoreFocus tlbMain;
        private System.Windows.Forms.ToolStripButton btnLine;
        private System.Windows.Forms.ToolStripButton btnRectangle;
        private System.Windows.Forms.ToolStripButton btnEllipsis;
        private System.Windows.Forms.ToolStripButton btnColor;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnOpenfile;
        private System.Windows.Forms.ToolStripButton btnInsert;
        private System.Windows.Forms.ToolStripButton btnFillRectangle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Panel pnlCanvasTop;
        private System.Windows.Forms.Panel pnlCanvasRight;
        private System.Windows.Forms.Panel pnlCanvasLeft;
        private System.Windows.Forms.Panel pnlCanvasBottom;
        private System.Windows.Forms.PictureBox picDrawing;
        private System.Windows.Forms.ToolStripButton btnFilledEllipse;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripDropDownButton tlbdrp_eraser;
        private System.Windows.Forms.ToolStripMenuItem tlpEraser1;
        private System.Windows.Forms.ToolStripMenuItem tlpEraser2;
        private System.Windows.Forms.ToolStripMenuItem tlpEraser3;
        private System.Windows.Forms.ToolStripComboBox tlpcmb_Width;
        private System.Windows.Forms.ToolStripButton btnstrline;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
