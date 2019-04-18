namespace gloBilling.C1GridFilter
{
    partial class AccountLogTypeFilterEditor
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
            this.chklistTypes = new ColorCodedCheckedListBox();
            this.chkNotes = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chklistTypes
            // 
            this.chklistTypes.BackColor = System.Drawing.SystemColors.Window;
            this.chklistTypes.CheckedColor = System.Drawing.Color.Green;
            this.chklistTypes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chklistTypes.FormattingEnabled = true;
            this.chklistTypes.IndeterminateColor = System.Drawing.Color.Orange;
            this.chklistTypes.Location = new System.Drawing.Point(5, 29);
            this.chklistTypes.Name = "chklistTypes";
            this.chklistTypes.Size = new System.Drawing.Size(272, 184);
            this.chklistTypes.TabIndex = 0;
            this.chklistTypes.UncheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));           
            this.chklistTypes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.chklistTypes_KeyUp);
            this.chklistTypes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chklistTypes_MouseUp);
            // 
            // chkNotes
            // 
            this.chkNotes.AutoSize = true;
            this.chkNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkNotes.Location = new System.Drawing.Point(5, 220);
            this.chkNotes.Name = "chkNotes";
            this.chkNotes.Size = new System.Drawing.Size(54, 17);
            this.chkNotes.TabIndex = 3;
            this.chkNotes.Text = "Notes";
            this.chkNotes.UseVisualStyleBackColor = true;
            this.chkNotes.CheckedChanged += new System.EventHandler(this.chkNotes_CheckedChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.chkSelectAll.Location = new System.Drawing.Point(5, 6);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(76, 17);
            this.chkSelectAll.TabIndex = 4;
            this.chkSelectAll.Text = "(Select All)";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // AccountLogTypeFilterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.chkNotes);
            this.Controls.Add(this.chklistTypes);
            this.Name = "AccountLogTypeFilterEditor";
            this.Size = new System.Drawing.Size(280, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkNotes;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private ColorCodedCheckedListBox chklistTypes;
    }
}
