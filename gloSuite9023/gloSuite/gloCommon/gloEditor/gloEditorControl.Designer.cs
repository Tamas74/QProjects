namespace gloEditor
{
    partial class gloEditorControl
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
                for (int i = ts_cmbFonts.Items.Count - 1; i >= 0; i--)
                {
                    System.Drawing.Font thisFont = null;
                    try
                    {
                        thisFont= (System.Drawing.Font)ts_cmbFonts.Items[i];
                    }
                    catch
                    {
                    }
                    try
                    {
                        ts_cmbFonts.Items.RemoveAt(i);
                    }
                    catch
                    {
                    }
                    if (thisFont != null)
                    {
                        try
                        {
                            thisFont.Dispose();
                        }
                        catch
                        {
                        }
                    }
                }
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
                    if (saveFileDialog1 != null)
                    {

                        saveFileDialog1.Dispose();
                        saveFileDialog1 = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (fontDialog1 != null)
                    {

                        fontDialog1.Dispose();
                        fontDialog1 = null;
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
                try
                {
                    if (fileDialog != null)
                    {

                        fileDialog.Dispose();
                        fileDialog = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloEditorControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsb_Editor = new gloGlobal.gloToolStripIgnoreFocus();
            this.ts_btnBold = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnItalic = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnLeftAlign = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnCenterAlign = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnRightAlign = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnIndentIN = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_IndentOUT = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnSaveSignature = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_btnOpenSignature = new System.Windows.Forms.ToolStripButton();
            this.ts_btnGetSignature = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.ts_cmbFontSize = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ts_cmbFonts = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtxtBox = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tsb_Editor.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tsb_Editor);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 36);
            this.panel1.TabIndex = 0;
            // 
            // tsb_Editor
            // 
            this.tsb_Editor.AllowDrop = true;
            this.tsb_Editor.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Editor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Editor.BackgroundImage")));
            this.tsb_Editor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsb_Editor.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsb_Editor.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsb_Editor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_btnBold,
            this.toolStripSeparator1,
            this.ts_btnItalic,
            this.toolStripSeparator2,
            this.ts_btnUnderline,
            this.toolStripSeparator5,
            this.ts_btnLeftAlign,
            this.toolStripSeparator6,
            this.ts_btnCenterAlign,
            this.toolStripSeparator7,
            this.ts_btnRightAlign,
            this.toolStripSeparator8,
            this.ts_btnImage,
            this.toolStripSeparator11,
            this.ts_btnColor,
            this.toolStripSeparator12,
            this.ts_btnIndentIN,
            this.toolStripSeparator14,
            this.ts_IndentOUT,
            this.toolStripSeparator15,
            this.ts_btnSaveSignature,
            this.toolStripSeparator16,
            this.ts_btnOpenSignature,
            this.ts_btnGetSignature,
            this.toolStripSeparator17});
            this.tsb_Editor.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsb_Editor.Location = new System.Drawing.Point(210, 0);
            this.tsb_Editor.Name = "tsb_Editor";
            this.tsb_Editor.Size = new System.Drawing.Size(678, 33);
            this.tsb_Editor.TabIndex = 1;
            this.tsb_Editor.Text = "toolStrip1";
            this.tsb_Editor.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsb_Editor_ItemClicked);
            // 
            // ts_btnBold
            // 
            this.ts_btnBold.CheckOnClick = true;
            this.ts_btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnBold.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnBold.Image")));
            this.ts_btnBold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnBold.Name = "ts_btnBold";
            this.ts_btnBold.Size = new System.Drawing.Size(23, 30);
            this.ts_btnBold.Tag = "BOLD";
            this.ts_btnBold.ToolTipText = "Bold";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnItalic
            // 
            this.ts_btnItalic.CheckOnClick = true;
            this.ts_btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnItalic.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnItalic.Image")));
            this.ts_btnItalic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnItalic.Name = "ts_btnItalic";
            this.ts_btnItalic.Size = new System.Drawing.Size(23, 30);
            this.ts_btnItalic.Tag = "ITALIC";
            this.ts_btnItalic.Text = "I";
            this.ts_btnItalic.ToolTipText = "Italic";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnUnderline
            // 
            this.ts_btnUnderline.CheckOnClick = true;
            this.ts_btnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnUnderline.Image")));
            this.ts_btnUnderline.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnUnderline.Name = "ts_btnUnderline";
            this.ts_btnUnderline.Size = new System.Drawing.Size(23, 30);
            this.ts_btnUnderline.Tag = "UNDERLINED";
            this.ts_btnUnderline.Text = "U";
            this.ts_btnUnderline.ToolTipText = "UnderLine";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnLeftAlign
            // 
            this.ts_btnLeftAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnLeftAlign.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnLeftAlign.Image")));
            this.ts_btnLeftAlign.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnLeftAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnLeftAlign.Name = "ts_btnLeftAlign";
            this.ts_btnLeftAlign.Size = new System.Drawing.Size(23, 30);
            this.ts_btnLeftAlign.Tag = "L_ALIGN";
            this.ts_btnLeftAlign.Text = "Left";
            this.ts_btnLeftAlign.ToolTipText = "Left Align";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnCenterAlign
            // 
            this.ts_btnCenterAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnCenterAlign.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnCenterAlign.Image")));
            this.ts_btnCenterAlign.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnCenterAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnCenterAlign.Name = "ts_btnCenterAlign";
            this.ts_btnCenterAlign.Size = new System.Drawing.Size(23, 30);
            this.ts_btnCenterAlign.Tag = "C_ALING";
            this.ts_btnCenterAlign.Text = "Center";
            this.ts_btnCenterAlign.ToolTipText = "Center Align";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnRightAlign
            // 
            this.ts_btnRightAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnRightAlign.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnRightAlign.Image")));
            this.ts_btnRightAlign.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnRightAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnRightAlign.Name = "ts_btnRightAlign";
            this.ts_btnRightAlign.Size = new System.Drawing.Size(23, 30);
            this.ts_btnRightAlign.Tag = "R_ALING";
            this.ts_btnRightAlign.Text = "Right";
            this.ts_btnRightAlign.ToolTipText = "Right Align";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnImage
            // 
            this.ts_btnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnImage.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnImage.Image")));
            this.ts_btnImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnImage.Name = "ts_btnImage";
            this.ts_btnImage.Size = new System.Drawing.Size(23, 30);
            this.ts_btnImage.Tag = "IMAGE";
            this.ts_btnImage.Text = "Image";
            this.ts_btnImage.ToolTipText = "Insert Image";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnColor
            // 
            this.ts_btnColor.BackColor = System.Drawing.Color.Transparent;
            this.ts_btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnColor.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnColor.Image")));
            this.ts_btnColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnColor.Name = "ts_btnColor";
            this.ts_btnColor.Size = new System.Drawing.Size(23, 30);
            this.ts_btnColor.Tag = "COLOR";
            this.ts_btnColor.ToolTipText = "Select Color";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnIndentIN
            // 
            this.ts_btnIndentIN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnIndentIN.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnIndentIN.Image")));
            this.ts_btnIndentIN.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_btnIndentIN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnIndentIN.Name = "ts_btnIndentIN";
            this.ts_btnIndentIN.Size = new System.Drawing.Size(23, 30);
            this.ts_btnIndentIN.Tag = "INDENT_IN";
            this.ts_btnIndentIN.Text = "toolStripButton1";
            this.ts_btnIndentIN.ToolTipText = "Indent";
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_IndentOUT
            // 
            this.ts_IndentOUT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_IndentOUT.Image = ((System.Drawing.Image)(resources.GetObject("ts_IndentOUT.Image")));
            this.ts_IndentOUT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ts_IndentOUT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_IndentOUT.Name = "ts_IndentOUT";
            this.ts_IndentOUT.Size = new System.Drawing.Size(23, 30);
            this.ts_IndentOUT.Tag = "INDENT_OUT";
            this.ts_IndentOUT.Text = "toolStripButton2";
            this.ts_IndentOUT.ToolTipText = "Indent";
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 33);
            // 
            // ts_btnSaveSignature
            // 
            this.ts_btnSaveSignature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnSaveSignature.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnSaveSignature.Image")));
            this.ts_btnSaveSignature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnSaveSignature.Name = "ts_btnSaveSignature";
            this.ts_btnSaveSignature.Size = new System.Drawing.Size(23, 30);
            this.ts_btnSaveSignature.Tag = "SAVE";
            this.ts_btnSaveSignature.ToolTipText = "Save Signature";
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 33);
            this.toolStripSeparator16.Visible = false;
            // 
            // ts_btnOpenSignature
            // 
            this.ts_btnOpenSignature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnOpenSignature.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnOpenSignature.Image")));
            this.ts_btnOpenSignature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnOpenSignature.Name = "ts_btnOpenSignature";
            this.ts_btnOpenSignature.Size = new System.Drawing.Size(23, 30);
            this.ts_btnOpenSignature.Tag = "OPEN";
            this.ts_btnOpenSignature.ToolTipText = "Open Signature";
            // 
            // ts_btnGetSignature
            // 
            this.ts_btnGetSignature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ts_btnGetSignature.Image = ((System.Drawing.Image)(resources.GetObject("ts_btnGetSignature.Image")));
            this.ts_btnGetSignature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_btnGetSignature.Name = "ts_btnGetSignature";
            this.ts_btnGetSignature.Size = new System.Drawing.Size(23, 30);
            this.ts_btnGetSignature.Tag = "getdata";
            this.ts_btnGetSignature.ToolTipText = "Get Signature";
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(135)))), ((int)(((byte)(214)))));
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 33);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.ts_cmbFontSize);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.ts_cmbFonts);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(210, 33);
            this.panel4.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Location = new System.Drawing.Point(203, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(3, 25);
            this.label12.TabIndex = 22;
            // 
            // ts_cmbFontSize
            // 
            this.ts_cmbFontSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.ts_cmbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ts_cmbFontSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_cmbFontSize.ForeColor = System.Drawing.Color.Black;
            this.ts_cmbFontSize.FormattingEnabled = true;
            this.ts_cmbFontSize.Items.AddRange(new object[] {
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72",
            "8",
            "9"});
            this.ts_cmbFontSize.Location = new System.Drawing.Point(156, 4);
            this.ts_cmbFontSize.Name = "ts_cmbFontSize";
            this.ts_cmbFontSize.Size = new System.Drawing.Size(47, 22);
            this.ts_cmbFontSize.TabIndex = 1;
            this.ts_cmbFontSize.TabStop = false;
            this.ts_cmbFontSize.SelectedIndexChanged += new System.EventHandler(this.ts_cmbFontSize_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(153, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(3, 25);
            this.label10.TabIndex = 20;
            // 
            // ts_cmbFonts
            // 
            this.ts_cmbFonts.Dock = System.Windows.Forms.DockStyle.Left;
            this.ts_cmbFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ts_cmbFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ts_cmbFonts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_cmbFonts.ForeColor = System.Drawing.Color.Black;
            this.ts_cmbFonts.FormattingEnabled = true;
            this.ts_cmbFonts.Location = new System.Drawing.Point(3, 4);
            this.ts_cmbFonts.Name = "ts_cmbFonts";
            this.ts_cmbFonts.Size = new System.Drawing.Size(150, 23);
            this.ts_cmbFonts.Sorted = true;
            this.ts_cmbFonts.TabIndex = 0;
            this.ts_cmbFonts.TabStop = false;
            this.ts_cmbFonts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ts_cmbFonts_DrawItem);
            this.ts_cmbFonts.SelectedIndexChanged += new System.EventHandler(this.ts_cmbFonts_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(3, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(207, 3);
            this.label9.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(207, 3);
            this.label7.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(3, 31);
            this.label8.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(178)))), ((int)(((byte)(228)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 1);
            this.label6.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 1);
            this.label1.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(0, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(888, 3);
            this.label11.TabIndex = 7;
            // 
            // fontDialog1
            // 
            this.fontDialog1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // fileDialog
            // 
            this.fileDialog.Filter = " *.bmp|*.png|*.jpeg|*.jpg";
            this.fileDialog.Title = "Select Image";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtxtBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 384);
            this.panel2.TabIndex = 1;
            // 
            // rtxtBox
            // 
            this.rtxtBox.AcceptsTab = true;
            this.rtxtBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtBox.ForeColor = System.Drawing.Color.Black;
            this.rtxtBox.Location = new System.Drawing.Point(1, 1);
            this.rtxtBox.Name = "rtxtBox";
            this.rtxtBox.Size = new System.Drawing.Size(886, 382);
            this.rtxtBox.TabIndex = 0;
            this.rtxtBox.Text = "";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 382);
            this.label5.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(887, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 382);
            this.label4.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(888, 1);
            this.label3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(888, 1);
            this.label2.TabIndex = 3;
            // 
            // gloEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloEditorControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(894, 426);
            this.Load += new System.EventHandler(this.RTE_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tsb_Editor.ResumeLayout(false);
            this.tsb_Editor.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label11;
        private gloGlobal.gloToolStripIgnoreFocus tsb_Editor;
        private System.Windows.Forms.ToolStripButton ts_btnBold;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ts_btnItalic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ts_btnUnderline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton ts_btnLeftAlign;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton ts_btnCenterAlign;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton ts_btnRightAlign;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton ts_btnImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton ts_btnColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton ts_btnIndentIN;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton ts_IndentOUT;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton ts_btnSaveSignature;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton ts_btnOpenSignature;
        private System.Windows.Forms.ToolStripButton ts_btnGetSignature;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtxtBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ts_cmbFonts;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox ts_cmbFontSize;
    }
}
