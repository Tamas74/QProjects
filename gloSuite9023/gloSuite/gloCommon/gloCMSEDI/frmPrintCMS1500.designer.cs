namespace gloCMSEDI
{
    partial class frmPrintCMS1500
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintCMS1500));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.rbFormPrintProper = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.rbYetToCheck = new System.Windows.Forms.RadioButton();
            this.rbProblemforPrint = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Controls.Add(this.btnContinue);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.rbFormPrintProper);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.rbYetToCheck);
            this.panel1.Controls.Add(this.rbProblemforPrint);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(610, 236);
            this.panel1.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(27, 25);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(38, 14);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "label1";
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(268, 201);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 4;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Right;
            this.label13.Location = new System.Drawing.Point(606, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(1, 228);
            this.label13.TabIndex = 3;
            // 
            // rbFormPrintProper
            // 
            this.rbFormPrintProper.AutoSize = true;
            this.rbFormPrintProper.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFormPrintProper.Location = new System.Drawing.Point(30, 115);
            this.rbFormPrintProper.Name = "rbFormPrintProper";
            this.rbFormPrintProper.Size = new System.Drawing.Size(209, 18);
            this.rbFormPrintProper.TabIndex = 1;
            this.rbFormPrintProper.Text = "Yes, the form is printing properly.";
            this.rbFormPrintProper.UseVisualStyleBackColor = true;
            this.rbFormPrintProper.CheckedChanged += new System.EventHandler(this.rbFormPrintProper_CheckedChanged);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Location = new System.Drawing.Point(3, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(1, 228);
            this.label14.TabIndex = 2;
            // 
            // rbYetToCheck
            // 
            this.rbYetToCheck.AutoSize = true;
            this.rbYetToCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbYetToCheck.Location = new System.Drawing.Point(30, 146);
            this.rbYetToCheck.Name = "rbYetToCheck";
            this.rbYetToCheck.Size = new System.Drawing.Size(339, 18);
            this.rbYetToCheck.TabIndex = 2;
            this.rbYetToCheck.Text = "Not yet, I have not reviewed the printing.  I will do that.";
            this.rbYetToCheck.UseVisualStyleBackColor = true;
            this.rbYetToCheck.CheckedChanged += new System.EventHandler(this.rbYetToCheck_CheckedChanged);
            // 
            // rbProblemforPrint
            // 
            this.rbProblemforPrint.AutoSize = true;
            this.rbProblemforPrint.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProblemforPrint.Location = new System.Drawing.Point(30, 177);
            this.rbProblemforPrint.Name = "rbProblemforPrint";
            this.rbProblemforPrint.Size = new System.Drawing.Size(406, 18);
            this.rbProblemforPrint.TabIndex = 3;
            this.rbProblemforPrint.Text = "Problem!  I have reviewed the printing and it does not look correct.  ";
            this.rbProblemforPrint.UseVisualStyleBackColor = true;
            this.rbProblemforPrint.CheckedChanged += new System.EventHandler(this.rbProblemforPrint_CheckedChanged);
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Location = new System.Drawing.Point(3, 232);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(604, 1);
            this.label15.TabIndex = 1;
            
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(604, 1);
            this.label16.TabIndex = 0;
            // 
            // frmPrintCMS1500
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(610, 236);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintCMS1500";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CMS1500 Print  Layout Confirmation";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton rbFormPrintProper;
        private System.Windows.Forms.RadioButton rbYetToCheck;
        private System.Windows.Forms.RadioButton rbProblemforPrint;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Label lblMessage;
    }
}