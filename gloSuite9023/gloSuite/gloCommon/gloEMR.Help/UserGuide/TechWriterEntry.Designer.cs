namespace gloEMR.Help
{
    partial class TechWriterEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
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
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechWriterEntry));
            this.label1 = new System.Windows.Forms.Label();
            this.tvNodes = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.gbProperties = new System.Windows.Forms.GroupBox();
            this.AppendFormName = new System.Windows.Forms.CheckBox();
            this.cbNavigator = new System.Windows.Forms.ComboBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.cbShowHelp = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.helpFileComboBox = new System.Windows.Forms.ComboBox();
            this.appendExtensionCheckBox = new System.Windows.Forms.CheckBox();
            this.pnl_tlsp_Top = new System.Windows.Forms.Panel();
            this.tstripDiagnosis = new gloGlobal.gloToolStripIgnoreFocus();
            this.tlsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbProperties.SuspendLayout();
            this.pnl_tlsp_Top.SuspendLayout();
            this.tstripDiagnosis.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Help File :";
            // 
            // tvNodes
            // 
            this.tvNodes.HideSelection = false;
            this.tvNodes.Location = new System.Drawing.Point(18, 98);
            this.tvNodes.Name = "tvNodes";
            this.tvNodes.Size = new System.Drawing.Size(550, 139);
            this.tvNodes.TabIndex = 2;
            this.tvNodes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNodes_AfterSelect);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "User Interface Tree";
            // 
            // gbProperties
            // 
            this.gbProperties.Controls.Add(this.AppendFormName);
            this.gbProperties.Controls.Add(this.cbNavigator);
            this.gbProperties.Controls.Add(this.txtCategory);
            this.gbProperties.Controls.Add(this.cbShowHelp);
            this.gbProperties.Controls.Add(this.label4);
            this.gbProperties.Controls.Add(this.label3);
            this.gbProperties.Enabled = false;
            this.gbProperties.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbProperties.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gbProperties.Location = new System.Drawing.Point(94, 243);
            this.gbProperties.Name = "gbProperties";
            this.gbProperties.Size = new System.Drawing.Size(401, 118);
            this.gbProperties.TabIndex = 3;
            this.gbProperties.TabStop = false;
            this.gbProperties.Text = "Properties:";
            // 
            // AppendFormName
            // 
            this.AppendFormName.AutoSize = true;
            this.AppendFormName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppendFormName.Location = new System.Drawing.Point(187, 86);
            this.AppendFormName.Name = "AppendFormName";
            this.AppendFormName.Size = new System.Drawing.Size(135, 18);
            this.AppendFormName.TabIndex = 3;
            this.AppendFormName.Text = "Append Form Name";
            this.AppendFormName.UseVisualStyleBackColor = true;
            this.AppendFormName.CheckedChanged += new System.EventHandler(this.AppendFormName_CheckedChanged);
            // 
            // cbNavigator
            // 
            this.cbNavigator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNavigator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNavigator.FormattingEnabled = true;
            this.cbNavigator.Location = new System.Drawing.Point(83, 53);
            this.cbNavigator.Name = "cbNavigator";
            this.cbNavigator.Size = new System.Drawing.Size(298, 22);
            this.cbNavigator.TabIndex = 1;
            this.cbNavigator.Validated += new System.EventHandler(this.cbNavigator_Validated);
            // 
            // txtCategory
            // 
            this.txtCategory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategory.Location = new System.Drawing.Point(83, 20);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(298, 22);
            this.txtCategory.TabIndex = 0;
            this.txtCategory.Validated += new System.EventHandler(this.txtCategory_Validated);
            // 
            // cbShowHelp
            // 
            this.cbShowHelp.AutoSize = true;
            this.cbShowHelp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowHelp.Location = new System.Drawing.Point(83, 86);
            this.cbShowHelp.Name = "cbShowHelp";
            this.cbShowHelp.Size = new System.Drawing.Size(84, 18);
            this.cbShowHelp.TabIndex = 2;
            this.cbShowHelp.Text = "Show help";
            this.cbShowHelp.UseVisualStyleBackColor = true;
            this.cbShowHelp.Validated += new System.EventHandler(this.cbShowHelp_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 14);
            this.label4.TabIndex = 1;
            this.label4.Text = "Navigator :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Category :";
            // 
            // helpFileComboBox
            // 
            this.helpFileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.helpFileComboBox.FormattingEnabled = true;
            this.helpFileComboBox.Location = new System.Drawing.Point(177, 18);
            this.helpFileComboBox.Name = "helpFileComboBox";
            this.helpFileComboBox.Size = new System.Drawing.Size(298, 22);
            this.helpFileComboBox.TabIndex = 0;
            // 
            // appendExtensionCheckBox
            // 
            this.appendExtensionCheckBox.AutoSize = true;
            this.appendExtensionCheckBox.Location = new System.Drawing.Point(25, 53);
            this.appendExtensionCheckBox.Name = "appendExtensionCheckBox";
            this.appendExtensionCheckBox.Size = new System.Drawing.Size(462, 18);
            this.appendExtensionCheckBox.TabIndex = 1;
            this.appendExtensionCheckBox.Text = "Automatically append extension (.htm) when a topic should directly be opened";
            this.appendExtensionCheckBox.UseVisualStyleBackColor = true;
            this.appendExtensionCheckBox.Validated += new System.EventHandler(this.appendExtensionCheckBox_Validated);
            // 
            // pnl_tlsp_Top
            // 
            this.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnl_tlsp_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_tlsp_Top.Controls.Add(this.tstripDiagnosis);
            this.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_tlsp_Top.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.pnl_tlsp_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_tlsp_Top.Name = "pnl_tlsp_Top";
            this.pnl_tlsp_Top.Size = new System.Drawing.Size(589, 54);
            this.pnl_tlsp_Top.TabIndex = 17;
            // 
            // tstripDiagnosis
            // 
            this.tstripDiagnosis.BackColor = System.Drawing.Color.Transparent;
            this.tstripDiagnosis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tstripDiagnosis.BackgroundImage")));
            this.tstripDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tstripDiagnosis.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tstripDiagnosis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstripDiagnosis.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tstripDiagnosis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsbtnSave});
            this.tstripDiagnosis.Location = new System.Drawing.Point(0, 0);
            this.tstripDiagnosis.Name = "tstripDiagnosis";
            this.tstripDiagnosis.Size = new System.Drawing.Size(589, 53);
            this.tstripDiagnosis.TabIndex = 0;
            this.tstripDiagnosis.Text = "ToolStrip1";
            // 
            // tlsbtnSave
            // 
            this.tlsbtnSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tlsbtnSave.Image")));
            this.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsbtnSave.Name = "tlsbtnSave";
            this.tlsbtnSave.Size = new System.Drawing.Size(66, 50);
            this.tlsbtnSave.Text = "&Save&&Cls";
            this.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlsbtnSave.ToolTipText = "Save and Close";
            this.tlsbtnSave.Click += new System.EventHandler(this.tlsbtnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.appendExtensionCheckBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.helpFileComboBox);
            this.panel1.Controls.Add(this.tvNodes);
            this.panel1.Controls.Add(this.gbProperties);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(589, 375);
            this.panel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 367);
            this.label8.TabIndex = 5;
            this.label8.Text = "Help file:";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Location = new System.Drawing.Point(585, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 367);
            this.label7.TabIndex = 4;
            this.label7.Text = "Help file:";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(3, 371);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(583, 1);
            this.label6.TabIndex = 3;
            this.label6.Text = "Help file:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(583, 1);
            this.label5.TabIndex = 2;
            this.label5.Text = "Help file:";
            // 
            // TechWriterEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(589, 429);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_tlsp_Top);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TechWriterEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help Editor";
            this.gbProperties.ResumeLayout(false);
            this.gbProperties.PerformLayout();
            this.pnl_tlsp_Top.ResumeLayout(false);
            this.pnl_tlsp_Top.PerformLayout();
            this.tstripDiagnosis.ResumeLayout(false);
            this.tstripDiagnosis.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView tvNodes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbProperties;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbNavigator;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.CheckBox cbShowHelp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox helpFileComboBox;
        private System.Windows.Forms.CheckBox appendExtensionCheckBox;
        private System.Windows.Forms.CheckBox AppendFormName;
        private System.Windows.Forms.Panel pnl_tlsp_Top;
        internal gloGlobal.gloToolStripIgnoreFocus tstripDiagnosis;
        internal System.Windows.Forms.ToolStripButton tlsbtnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}