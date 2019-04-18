namespace gloOffice
{
    partial class frmAddDropDown
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddDropDown));
            this.pnl_tls_ = new System.Windows.Forms.Panel();
            this.tlsDropdown = new gloGlobal.gloToolStripIgnoreFocus();
            this.btn_tls_Ok = new System.Windows.Forms.ToolStripButton();
            this.btn_tls_Cancel = new System.Windows.Forms.ToolStripButton();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label12 = new System.Windows.Forms.Label();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Label13 = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnl_tls_.SuspendLayout();
            this.tlsDropdown.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel4.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_tls_
            // 
            this.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tls_.Controls.Add(this.tlsDropdown);
            this.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tls_.Location = new System.Drawing.Point(0, 0);
            this.pnl_tls_.Name = "pnl_tls_";
            this.pnl_tls_.Size = new System.Drawing.Size(469, 54);
            this.pnl_tls_.TabIndex = 13;
            // 
            // tlsDropdown
            // 
            this.tlsDropdown.BackColor = System.Drawing.Color.Transparent;
            this.tlsDropdown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tlsDropdown.BackgroundImage")));
            this.tlsDropdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlsDropdown.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tlsDropdown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_tls_Ok,
            this.btn_tls_Cancel});
            this.tlsDropdown.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tlsDropdown.Location = new System.Drawing.Point(0, 0);
            this.tlsDropdown.Name = "tlsDropdown";
            this.tlsDropdown.Size = new System.Drawing.Size(469, 53);
            this.tlsDropdown.TabIndex = 0;
            this.tlsDropdown.Text = "toolStrip1";
            this.tlsDropdown.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tlsDropdown_ItemClicked);
            // 
            // btn_tls_Ok
            // 
            this.btn_tls_Ok.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_tls_Ok.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Ok.Image")));
            this.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Ok.Name = "btn_tls_Ok";
            this.btn_tls_Ok.Size = new System.Drawing.Size(66, 50);
            this.btn_tls_Ok.Tag = "Ok";
            this.btn_tls_Ok.Text = "&Save&&Cls";
            this.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Ok.ToolTipText = "Save and Close";
            // 
            // btn_tls_Cancel
            // 
            this.btn_tls_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tls_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_tls_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_tls_Cancel.Image")));
            this.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_tls_Cancel.Name = "btn_tls_Cancel";
            this.btn_tls_Cancel.Size = new System.Drawing.Size(43, 50);
            this.btn_tls_Cancel.Tag = "Cancel";
            this.btn_tls_Cancel.Text = "&Close";
            this.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_tls_Cancel.ToolTipText = "Close";
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.Transparent;
            this.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel3.Controls.Add(this.Label5);
            this.Panel3.Controls.Add(this.Label7);
            this.Panel3.Controls.Add(this.Label14);
            this.Panel3.Controls.Add(this.Panel4);
            this.Panel3.Controls.Add(this.Label15);
            this.Panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel3.Location = new System.Drawing.Point(0, 54);
            this.Panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Padding = new System.Windows.Forms.Padding(3);
            this.Panel3.Size = new System.Drawing.Size(469, 30);
            this.Panel3.TabIndex = 21;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label5.Location = new System.Drawing.Point(4, 26);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(461, 1);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "label2";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(3, 4);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(1, 23);
            this.Label7.TabIndex = 7;
            this.Label7.Text = "label4";
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label14.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label14.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Label14.Location = new System.Drawing.Point(465, 4);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(1, 23);
            this.Label14.TabIndex = 6;
            this.Label14.Text = "label3";
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Transparent;
            this.Panel4.BackgroundImage = global::gloOffice.Properties.Resources.Img_Button;
            this.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel4.Controls.Add(this.txtTitle);
            this.Panel4.Controls.Add(this.Label3);
            this.Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Panel4.Location = new System.Drawing.Point(3, 4);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(463, 23);
            this.Panel4.TabIndex = 6;
            // 
            // txtTitle
            // 
            this.txtTitle.ForeColor = System.Drawing.Color.Black;
            this.txtTitle.Location = new System.Drawing.Point(53, 0);
            this.txtTitle.MaxLength = 64;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(201, 22);
            this.txtTitle.TabIndex = 1;
            // 
            // Label3
            // 
            this.Label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label3.Location = new System.Drawing.Point(0, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(463, 23);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "  Title :";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.Location = new System.Drawing.Point(3, 3);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(463, 1);
            this.Label15.TabIndex = 5;
            this.Label15.Text = "label1";
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.Controls.Add(this.Label12);
            this.Panel2.Controls.Add(this.lstItems);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.btnUp);
            this.Panel2.Controls.Add(this.btnDown);
            this.Panel2.Controls.Add(this.Label9);
            this.Panel2.Controls.Add(this.Label8);
            this.Panel2.Controls.Add(this.Label10);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel2.Location = new System.Drawing.Point(232, 84);
            this.Panel2.Name = "Panel2";
            this.Panel2.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.Panel2.Size = new System.Drawing.Size(237, 199);
            this.Panel2.TabIndex = 22;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label12.Location = new System.Drawing.Point(4, 195);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(229, 1);
            this.Label12.TabIndex = 22;
            this.Label12.Text = "label1";
            // 
            // lstItems
            // 
            this.lstItems.ForeColor = System.Drawing.Color.Black;
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 14;
            this.lstItems.Location = new System.Drawing.Point(12, 27);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(185, 158);
            this.lstItems.TabIndex = 5;
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
            this.lstItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDown);
            this.lstItems.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseMove);
            // 
            // Label2
            // 
            this.Label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label2.Location = new System.Drawing.Point(4, 2);
            this.Label2.Name = "Label2";
            this.Label2.Padding = new System.Windows.Forms.Padding(6, 3, 0, 0);
            this.Label2.Size = new System.Drawing.Size(229, 19);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Drop-down item :";
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUp.BackgroundImage")));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Enabled = false;
            this.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(203, 71);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(22, 22);
            this.btnUp.TabIndex = 7;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            this.btnUp.MouseLeave += new System.EventHandler(this.btnUp_MouseLeave);
            this.btnUp.MouseHover += new System.EventHandler(this.btnUp_MouseHover);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDown.BackgroundImage")));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Enabled = false;
            this.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(203, 107);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(22, 22);
            this.btnDown.TabIndex = 8;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnDown.MouseLeave += new System.EventHandler(this.btnDown_MouseLeave);
            this.btnDown.MouseHover += new System.EventHandler(this.btnDown_MouseHover);
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label9.Location = new System.Drawing.Point(4, 1);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(229, 1);
            this.Label9.TabIndex = 20;
            this.Label9.Text = "label1";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label8.Location = new System.Drawing.Point(3, 1);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(1, 195);
            this.Label8.TabIndex = 19;
            this.Label8.Text = "label4";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label10.Location = new System.Drawing.Point(233, 1);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(1, 195);
            this.Label10.TabIndex = 21;
            this.Label10.Text = "label4";
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Controls.Add(this.txtItem);
            this.Panel1.Controls.Add(this.btnAdd);
            this.Panel1.Controls.Add(this.btnRemove);
            this.Panel1.Controls.Add(this.btnModify);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Controls.Add(this.Label6);
            this.Panel1.Controls.Add(this.Label4);
            this.Panel1.Controls.Add(this.Label11);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 84);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.Panel1.Size = new System.Drawing.Size(232, 199);
            this.Panel1.TabIndex = 23;
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label13.Location = new System.Drawing.Point(4, 195);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(224, 1);
            this.Label13.TabIndex = 22;
            this.Label13.Text = "label1";
            // 
            // txtItem
            // 
            this.txtItem.ForeColor = System.Drawing.Color.Black;
            this.txtItem.Location = new System.Drawing.Point(15, 34);
            this.txtItem.MaxLength = 256;
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(203, 22);
            this.txtItem.TabIndex = 2;
            this.txtItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItem_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAdd.Location = new System.Drawing.Point(75, 69);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(84, 25);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add >>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            this.btnAdd.MouseHover += new System.EventHandler(this.btnAdd_MouseHover);
            // 
            // btnRemove
            // 
            this.btnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove.BackgroundImage")));
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.Enabled = false;
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnRemove.Location = new System.Drawing.Point(75, 142);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(84, 25);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemove.MouseLeave += new System.EventHandler(this.btnRemove_MouseLeave);
            this.btnRemove.MouseHover += new System.EventHandler(this.btnRemove_MouseHover);
            // 
            // btnModify
            // 
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModify.Enabled = false;
            this.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(108)))));
            this.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(160)))));
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnModify.Location = new System.Drawing.Point(75, 106);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(84, 25);
            this.btnModify.TabIndex = 6;
            this.btnModify.Text = "Modify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            this.btnModify.MouseLeave += new System.EventHandler(this.btnModify_MouseLeave);
            this.btnModify.MouseHover += new System.EventHandler(this.btnModify_MouseHover);
            // 
            // Label1
            // 
            this.Label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label1.Location = new System.Drawing.Point(4, 2);
            this.Label1.Name = "Label1";
            this.Label1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 0);
            this.Label1.Size = new System.Drawing.Size(224, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Drop-down item :";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label6.Location = new System.Drawing.Point(4, 1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(224, 1);
            this.Label6.TabIndex = 20;
            this.Label6.Text = "label1";
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label4.Location = new System.Drawing.Point(3, 1);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(1, 195);
            this.Label4.TabIndex = 19;
            this.Label4.Text = "label4";
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.Label11.Location = new System.Drawing.Point(228, 1);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(1, 195);
            this.Label11.TabIndex = 21;
            this.Label11.Text = "label4";
            // 
            // frmAddDropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(469, 283);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.pnl_tls_);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddDropDown";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Drop-Down List";
            this.Load += new System.EventHandler(this.frmAddDropDown_Load);
            this.pnl_tls_.ResumeLayout(false);
            this.pnl_tls_.PerformLayout();
            this.tlsDropdown.ResumeLayout(false);
            this.tlsDropdown.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel4.ResumeLayout(false);
            this.Panel4.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_tls_;
        private gloGlobal.gloToolStripIgnoreFocus tlsDropdown;
        private System.Windows.Forms.ToolStripButton btn_tls_Ok;
        private System.Windows.Forms.ToolStripButton btn_tls_Cancel;
        internal System.Windows.Forms.Panel Panel3;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.TextBox txtTitle;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Panel Panel2;
        private System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.ListBox lstItems;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnUp;
        internal System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox txtItem;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.Button btnModify;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.Label Label11;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}