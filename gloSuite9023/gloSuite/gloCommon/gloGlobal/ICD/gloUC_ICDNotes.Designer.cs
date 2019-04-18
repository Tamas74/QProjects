namespace gloGlobal.ICD
{
    partial class gloICDNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloICDNotes));
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblSelectedICD = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.TrvCategoryNotes = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblCategoryNotes = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TrvNotes = new System.Windows.Forms.TreeView();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel7.Size = new System.Drawing.Size(807, 28);
            this.panel7.TabIndex = 5;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel8.BackgroundImage")));
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.lblSelectedICD);
            this.panel8.Controls.Add(this.label11);
            this.panel8.Controls.Add(this.label12);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.label14);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(807, 25);
            this.panel8.TabIndex = 2;
            // 
            // lblSelectedICD
            // 
            this.lblSelectedICD.AutoSize = true;
            this.lblSelectedICD.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedICD.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedICD.ForeColor = System.Drawing.Color.White;
            this.lblSelectedICD.Location = new System.Drawing.Point(10, 5);
            this.lblSelectedICD.Name = "lblSelectedICD";
            this.lblSelectedICD.Size = new System.Drawing.Size(196, 14);
            this.lblSelectedICD.TabIndex = 51;
            this.lblSelectedICD.Text = "Notes for ICD-10 Code (G72.2)";
            this.lblSelectedICD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(0, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 23);
            this.label11.TabIndex = 43;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(806, 1);
            this.label12.TabIndex = 42;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label13.Location = new System.Drawing.Point(0, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(806, 1);
            this.label13.TabIndex = 41;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.label14.Location = new System.Drawing.Point(806, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 25);
            this.label14.TabIndex = 40;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel9);
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 187);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel6.Size = new System.Drawing.Size(813, 199);
            this.panel6.TabIndex = 6;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel9.Controls.Add(this.TrvCategoryNotes);
            this.panel9.Controls.Add(this.label16);
            this.panel9.Controls.Add(this.label17);
            this.panel9.Controls.Add(this.label18);
            this.panel9.Controls.Add(this.label20);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 27);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(807, 169);
            this.panel9.TabIndex = 3;
            // 
            // TrvCategoryNotes
            // 
            this.TrvCategoryNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrvCategoryNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvCategoryNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrvCategoryNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.TrvCategoryNotes.FullRowSelect = true;
            this.TrvCategoryNotes.HideSelection = false;
            this.TrvCategoryNotes.ImageIndex = 0;
            this.TrvCategoryNotes.ImageList = this.imgList;
            this.TrvCategoryNotes.Indent = 20;
            this.TrvCategoryNotes.ItemHeight = 18;
            this.TrvCategoryNotes.Location = new System.Drawing.Point(1, 1);
            this.TrvCategoryNotes.Name = "TrvCategoryNotes";
            this.TrvCategoryNotes.SelectedImageIndex = 0;
            this.TrvCategoryNotes.ShowLines = false;
            this.TrvCategoryNotes.ShowPlusMinus = false;
            this.TrvCategoryNotes.ShowRootLines = false;
            this.TrvCategoryNotes.Size = new System.Drawing.Size(805, 167);
            this.TrvCategoryNotes.TabIndex = 44;
            this.TrvCategoryNotes.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvCategoryNotes_BeforeCollapse);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "Bullet04.ico");
            this.imgList.Images.SetKeyName(1, "Bullet05.ico");
            this.imgList.Images.SetKeyName(2, "Bullet06.ico");
            this.imgList.Images.SetKeyName(3, "ICD 10.ico");
            this.imgList.Images.SetKeyName(4, "Notes.ico");
            this.imgList.Images.SetKeyName(5, "Forward-Code.png");
            this.imgList.Images.SetKeyName(6, "Back-Code.png");
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Left;
            this.label16.Location = new System.Drawing.Point(0, 1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 167);
            this.label16.TabIndex = 43;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(806, 1);
            this.label17.TabIndex = 42;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(0, 168);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(806, 1);
            this.label18.TabIndex = 41;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label20.Dock = System.Windows.Forms.DockStyle.Right;
            this.label20.Location = new System.Drawing.Point(806, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(1, 169);
            this.label20.TabIndex = 40;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(3, 0);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel10.Size = new System.Drawing.Size(807, 27);
            this.panel10.TabIndex = 5;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.Transparent;
            this.panel11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel11.BackgroundImage")));
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Controls.Add(this.lblCategoryNotes);
            this.panel11.Controls.Add(this.label22);
            this.panel11.Controls.Add(this.label23);
            this.panel11.Controls.Add(this.label24);
            this.panel11.Controls.Add(this.label25);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(807, 24);
            this.panel11.TabIndex = 2;
            // 
            // lblCategoryNotes
            // 
            this.lblCategoryNotes.AutoSize = true;
            this.lblCategoryNotes.BackColor = System.Drawing.Color.Transparent;
            this.lblCategoryNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryNotes.ForeColor = System.Drawing.Color.White;
            this.lblCategoryNotes.Location = new System.Drawing.Point(10, 5);
            this.lblCategoryNotes.Name = "lblCategoryNotes";
            this.lblCategoryNotes.Size = new System.Drawing.Size(0, 14);
            this.lblCategoryNotes.TabIndex = 51;
            this.lblCategoryNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Location = new System.Drawing.Point(0, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 22);
            this.label22.TabIndex = 43;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(806, 1);
            this.label23.TabIndex = 42;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label24.Location = new System.Drawing.Point(0, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(806, 1);
            this.label24.TabIndex = 41;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Right;
            this.label25.Location = new System.Drawing.Point(806, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 24);
            this.label25.TabIndex = 40;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel5.Controls.Add(this.TrvNotes);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 28);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(807, 156);
            this.panel5.TabIndex = 2;
            // 
            // TrvNotes
            // 
            this.TrvNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TrvNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrvNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrvNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.TrvNotes.FullRowSelect = true;
            this.TrvNotes.HideSelection = false;
            this.TrvNotes.ImageIndex = 0;
            this.TrvNotes.ImageList = this.imgList;
            this.TrvNotes.Indent = 20;
            this.TrvNotes.ItemHeight = 18;
            this.TrvNotes.Location = new System.Drawing.Point(1, 1);
            this.TrvNotes.Name = "TrvNotes";
            this.TrvNotes.SelectedImageIndex = 0;
            this.TrvNotes.ShowLines = false;
            this.TrvNotes.ShowPlusMinus = false;
            this.TrvNotes.ShowRootLines = false;
            this.TrvNotes.Size = new System.Drawing.Size(805, 154);
            this.TrvNotes.TabIndex = 44;
            this.TrvNotes.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvNotes_BeforeCollapse);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 154);
            this.label10.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(806, 1);
            this.label9.TabIndex = 42;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(806, 1);
            this.label7.TabIndex = 41;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Location = new System.Drawing.Point(806, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 156);
            this.label8.TabIndex = 40;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel1.Size = new System.Drawing.Size(813, 184);
            this.panel1.TabIndex = 7;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 184);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(813, 3);
            this.splitter1.TabIndex = 116;
            this.splitter1.TabStop = false;
            // 
            // gloICDNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "gloICDNotes";
            this.Size = new System.Drawing.Size(813, 386);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblSelectedICD;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TreeView TrvNotes;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TreeView TrvCategoryNotes;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label lblCategoryNotes;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;

    }
}
