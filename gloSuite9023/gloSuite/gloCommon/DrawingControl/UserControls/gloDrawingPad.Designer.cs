namespace DrawingControl
{
    partial class gloDrawingPad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloDrawingPad));
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
            this.tlbdrb_PenWidth = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.pnlFill = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picDrawing = new System.Windows.Forms.PictureBox();
            this.pnlCanvasBottom = new System.Windows.Forms.Panel();
            this.pnlCanvasRight = new System.Windows.Forms.Panel();
            this.pnlCanvasLeft = new System.Windows.Forms.Panel();
            this.pnlCanvasTop = new System.Windows.Forms.Panel();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pnlTop.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawing)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
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
            this.tlbMain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlbMain.BackgroundImage")));
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
            this.btnstrline,
            this.btnRectangle,
            this.btnFillRectangle,
            this.btnEllipsis,
            this.btnFilledEllipse,
            this.btnColor,
            this.tlbdrp_eraser,
            this.tlbdrb_PenWidth,
            this.btnClose});
            this.tlbMain.Location = new System.Drawing.Point(0, 0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(1143, 50);
            this.tlbMain.TabIndex = 1;
            this.tlbMain.Text = "toolStrip1";
            // 
            // btnOpenfile
            // 
            this.btnOpenfile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnOpenfile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenfile.Image")));
            this.btnOpenfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenfile.Name = "btnOpenfile";
            this.btnOpenfile.Size = new System.Drawing.Size(66, 47);
            this.btnOpenfile.Text = "&Open File";
            this.btnOpenfile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenfile.Click += new System.EventHandler(this.btnOpenfile_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnInsert.Image = ((System.Drawing.Image)(resources.GetObject("btnInsert.Image")));
            this.btnInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(56, 47);
            this.btnInsert.Text = " &Insert ";
            this.btnInsert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInsert.ToolTipText = "Insert Image";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(49, 47);
            this.btnClear.Text = " Clea&r ";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClear.ToolTipText = "Clear Picture";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLine
            // 
            this.btnLine.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(44, 47);
            this.btnLine.Text = " &Line ";
            this.btnLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLine.ToolTipText = "Draw Line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRectangle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangle.Image")));
            this.btnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(72, 47);
            this.btnRectangle.Text = "&Rectangle";
            this.btnRectangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRectangle.ToolTipText = "Draw Rectangle";
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnFillRectangle
            // 
            this.btnFillRectangle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFillRectangle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnFillRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnFillRectangle.Image")));
            this.btnFillRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFillRectangle.Name = "btnFillRectangle";
            this.btnFillRectangle.Size = new System.Drawing.Size(106, 47);
            this.btnFillRectangle.Text = "&Filled Rectangle";
            this.btnFillRectangle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFillRectangle.ToolTipText = "Draw Filled Rectangle";
            this.btnFillRectangle.Click += new System.EventHandler(this.btnFillRectangle_Click);
            // 
            // btnEllipsis
            // 
            this.btnEllipsis.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEllipsis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnEllipsis.Image = ((System.Drawing.Image)(resources.GetObject("btnEllipsis.Image")));
            this.btnEllipsis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEllipsis.Name = "btnEllipsis";
            this.btnEllipsis.Size = new System.Drawing.Size(48, 47);
            this.btnEllipsis.Text = "&Ellipse";
            this.btnEllipsis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEllipsis.ToolTipText = "Draw Ellipse";
            this.btnEllipsis.Click += new System.EventHandler(this.btnEllipsis_Click);
            // 
            // btnFilledEllipse
            // 
            this.btnFilledEllipse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilledEllipse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnFilledEllipse.Image = ((System.Drawing.Image)(resources.GetObject("btnFilledEllipse.Image")));
            this.btnFilledEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilledEllipse.Name = "btnFilledEllipse";
            this.btnFilledEllipse.Size = new System.Drawing.Size(82, 47);
            this.btnFilledEllipse.Text = "F&illed Ellipse";
            this.btnFilledEllipse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilledEllipse.ToolTipText = "Draw Filled Ellipse";
            this.btnFilledEllipse.Click += new System.EventHandler(this.btnFilledEllipse_Click);
            // 
            // btnstrline
            // 
            this.btnstrline.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstrline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnstrline.Image = ((System.Drawing.Image)(resources.GetObject("btnstrline.Image")));
            this.btnstrline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnstrline.Name = "btnstrline";
            this.btnstrline.Size = new System.Drawing.Size(91, 47);
            this.btnstrline.Text = "&Straight Line";
            this.btnstrline.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnstrline.ToolTipText = "Draw Straight Line";
            this.btnstrline.Click += new System.EventHandler(this.btnstrline_Click);
            // 
            // btnColor
            // 
            this.btnColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnColor.Image = ((System.Drawing.Image)(resources.GetObject("btnColor.Image")));
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(51, 47);
            this.btnColor.Text = " C&olor ";
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
            this.tlbdrp_eraser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbdrp_eraser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbdrp_eraser.Image = ((System.Drawing.Image)(resources.GetObject("tlbdrp_eraser.Image")));
            this.tlbdrp_eraser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbdrp_eraser.Name = "tlbdrp_eraser";
            this.tlbdrp_eraser.Size = new System.Drawing.Size(57, 47);
            this.tlbdrp_eraser.Text = "Er&aser";
            this.tlbdrp_eraser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbdrp_eraser.ToolTipText = "Erase Drawing";
            this.tlbdrp_eraser.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlbdrp_eraser_DropDownItemClicked);
            // 
            // tlpEraser1
            // 
            this.tlpEraser1.Image = ((System.Drawing.Image)(resources.GetObject("tlpEraser1.Image")));
            this.tlpEraser1.Name = "tlpEraser1";
            this.tlpEraser1.Size = new System.Drawing.Size(168, 38);
            this.tlpEraser1.Text = "Small";
            this.tlpEraser1.ToolTipText = "Select Small Eraser";
            this.tlpEraser1.Click += new System.EventHandler(this.tlpEraser1_Click);
            // 
            // tlpEraser2
            // 
            this.tlpEraser2.Image = ((System.Drawing.Image)(resources.GetObject("tlpEraser2.Image")));
            this.tlpEraser2.Name = "tlpEraser2";
            this.tlpEraser2.Size = new System.Drawing.Size(168, 38);
            this.tlpEraser2.Text = "Medium";
            this.tlpEraser2.ToolTipText = "Select Medium Eraser";
            this.tlpEraser2.Click += new System.EventHandler(this.tlpEraser2_Click);
            // 
            // tlpEraser3
            // 
            this.tlpEraser3.Image = ((System.Drawing.Image)(resources.GetObject("tlpEraser3.Image")));
            this.tlpEraser3.Name = "tlpEraser3";
            this.tlpEraser3.Size = new System.Drawing.Size(168, 38);
            this.tlpEraser3.Text = "Large";
            this.tlpEraser3.ToolTipText = "Select Large Eraser";
            this.tlpEraser3.Click += new System.EventHandler(this.tlpEraser3_Click);
            // 
            // tlbdrb_PenWidth
            // 
            this.tlbdrb_PenWidth.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.tlbdrb_PenWidth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlbdrb_PenWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tlbdrb_PenWidth.Image = ((System.Drawing.Image)(resources.GetObject("tlbdrb_PenWidth.Image")));
            this.tlbdrb_PenWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbdrb_PenWidth.Name = "tlbdrb_PenWidth";
            this.tlbdrb_PenWidth.Size = new System.Drawing.Size(84, 47);
            this.tlbdrb_PenWidth.Text = "&Pen Width";
            this.tlbdrb_PenWidth.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlbdrb_PenWidth.ToolTipText = "Set Pen Width";
            this.tlbdrb_PenWidth.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlbdrp_PenWidth_DropDownItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem1.Text = "1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem2.Text = "2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem3.Text = "3";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem4.Text = "4";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem5.Image")));
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem5.Text = "5";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem6.Text = "6";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem7.Image")));
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem7.Text = "7";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem8.Image")));
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem8.Text = "8";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem9.Image")));
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem9.Text = "9";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem10.Image")));
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(168, 38);
            this.toolStripMenuItem10.Text = "10";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(43, 47);
            this.btnClose.Text = "&Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClose.ToolTipText = "Close Drawing Pad";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlFill
            // 
            this.pnlFill.BackColor = System.Drawing.Color.Transparent;
            this.pnlFill.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlFill.BackgroundImage")));
            this.pnlFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFill.Controls.Add(this.picDrawing);
            this.pnlFill.Controls.Add(this.label4);
            this.pnlFill.Controls.Add(this.label3);
            this.pnlFill.Controls.Add(this.label2);
            this.pnlFill.Controls.Add(this.label1);
            this.pnlFill.Controls.Add(this.pnlCanvasBottom);
            this.pnlFill.Controls.Add(this.pnlCanvasRight);
            this.pnlFill.Controls.Add(this.pnlCanvasLeft);
            this.pnlFill.Controls.Add(this.pnlCanvasTop);
            this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFill.Location = new System.Drawing.Point(0, 50);
            this.pnlFill.Name = "pnlFill";
            this.pnlFill.Size = new System.Drawing.Size(1143, 837);
            this.pnlFill.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(1127, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 805);
            this.label4.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(15, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 805);
            this.label3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(15, 821);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1113, 1);
            this.label2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1113, 1);
            this.label1.TabIndex = 6;
            // 
            // picDrawing
            // 
            this.picDrawing.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDrawing.Location = new System.Drawing.Point(16, 16);
            this.picDrawing.Name = "picDrawing";
            this.picDrawing.Size = new System.Drawing.Size(1111, 805);
            this.picDrawing.TabIndex = 5;
            this.picDrawing.TabStop = false;
            this.picDrawing.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picDrawing_MouseMove);
            this.picDrawing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picDrawing_MouseDown);
            this.picDrawing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picDrawing_MouseUp);
            // 
            // pnlCanvasBottom
            // 
            this.pnlCanvasBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCanvasBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCanvasBottom.Location = new System.Drawing.Point(15, 822);
            this.pnlCanvasBottom.Name = "pnlCanvasBottom";
            this.pnlCanvasBottom.Size = new System.Drawing.Size(1113, 15);
            this.pnlCanvasBottom.TabIndex = 4;
            // 
            // pnlCanvasRight
            // 
            this.pnlCanvasRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCanvasRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCanvasRight.Location = new System.Drawing.Point(1128, 15);
            this.pnlCanvasRight.Name = "pnlCanvasRight";
            this.pnlCanvasRight.Size = new System.Drawing.Size(15, 822);
            this.pnlCanvasRight.TabIndex = 3;
            // 
            // pnlCanvasLeft
            // 
            this.pnlCanvasLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCanvasLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlCanvasLeft.Location = new System.Drawing.Point(0, 15);
            this.pnlCanvasLeft.Name = "pnlCanvasLeft";
            this.pnlCanvasLeft.Size = new System.Drawing.Size(15, 822);
            this.pnlCanvasLeft.TabIndex = 2;
            // 
            // pnlCanvasTop
            // 
            this.pnlCanvasTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlCanvasTop.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlCanvasTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCanvasTop.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvasTop.Name = "pnlCanvasTop";
            this.pnlCanvasTop.Size = new System.Drawing.Size(1143, 15);
            this.pnlCanvasTop.TabIndex = 1;
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "openFileDialog1";
            // 
            // gloDrawingPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlTop);
            this.Name = "gloDrawingPad";
            this.Size = new System.Drawing.Size(1143, 887);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDrawing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlFill;
        private System.Windows.Forms.Panel pnlCanvasTop;
        private System.Windows.Forms.Panel pnlCanvasLeft;
        private System.Windows.Forms.Panel pnlCanvasRight;
        private System.Windows.Forms.Panel pnlCanvasBottom;
        private System.Windows.Forms.PictureBox picDrawing;
        private gloGlobal.gloToolStripIgnoreFocus tlbMain;
        private System.Windows.Forms.ToolStripButton btnOpenfile;
        private System.Windows.Forms.ToolStripButton btnInsert;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnLine;
        private System.Windows.Forms.ToolStripButton btnRectangle;
        private System.Windows.Forms.ToolStripButton btnFillRectangle;
        private System.Windows.Forms.ToolStripButton btnEllipsis;
        private System.Windows.Forms.ToolStripButton btnFilledEllipse;
        private System.Windows.Forms.ToolStripButton btnstrline;
        private System.Windows.Forms.ToolStripButton btnColor;
        private System.Windows.Forms.ToolStripDropDownButton tlbdrp_eraser;
        private System.Windows.Forms.ToolStripMenuItem tlpEraser1;
        private System.Windows.Forms.ToolStripMenuItem tlpEraser2;
        private System.Windows.Forms.ToolStripMenuItem tlpEraser3;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.ToolStripDropDownButton tlbdrb_PenWidth;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
