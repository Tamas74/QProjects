<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddSummaryCareRecord
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddSummaryCareRecord))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSaveClose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Main = New System.Windows.Forms.Panel()
        Me.dtSummaryCareRecordDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.chkSummaryCareRecord = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnl_Main.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(460, 53)
        Me.pnlToolStrip.TabIndex = 13
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = CType(resources.GetObject("ts_ViewButtons.BackgroundImage"), System.Drawing.Image)
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSaveClose, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(460, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnSaveClose
        '
        Me.ts_btnSaveClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSaveClose.Image = CType(resources.GetObject("ts_btnSaveClose.Image"), System.Drawing.Image)
        Me.ts_btnSaveClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSaveClose.Name = "ts_btnSaveClose"
        Me.ts_btnSaveClose.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSaveClose.Text = "&Save&&Cls"
        Me.ts_btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSaveClose.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_Main
        '
        Me.pnl_Main.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Main.Controls.Add(Me.dtSummaryCareRecordDate)
        Me.pnl_Main.Controls.Add(Me.Label2)
        Me.pnl_Main.Controls.Add(Me.txtRemarks)
        Me.pnl_Main.Controls.Add(Me.chkSummaryCareRecord)
        Me.pnl_Main.Controls.Add(Me.Label1)
        Me.pnl_Main.Controls.Add(Me.Label19)
        Me.pnl_Main.Controls.Add(Me.Label20)
        Me.pnl_Main.Controls.Add(Me.Label25)
        Me.pnl_Main.Controls.Add(Me.Label26)
        Me.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Main.Location = New System.Drawing.Point(0, 53)
        Me.pnl_Main.Name = "pnl_Main"
        Me.pnl_Main.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Main.Size = New System.Drawing.Size(460, 211)
        Me.pnl_Main.TabIndex = 14
        '
        'dtSummaryCareRecordDate
        '
        Me.dtSummaryCareRecordDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtSummaryCareRecordDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtSummaryCareRecordDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtSummaryCareRecordDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtSummaryCareRecordDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtSummaryCareRecordDate.CustomFormat = "MM/dd/yyyy"
        Me.dtSummaryCareRecordDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtSummaryCareRecordDate.Location = New System.Drawing.Point(89, 17)
        Me.dtSummaryCareRecordDate.Name = "dtSummaryCareRecordDate"
        Me.dtSummaryCareRecordDate.Size = New System.Drawing.Size(108, 22)
        Me.dtSummaryCareRecordDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(45, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 14)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Date :"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(89, 71)
        Me.txtRemarks.MaxLength = 500
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(342, 117)
        Me.txtRemarks.TabIndex = 4
        '
        'chkSummaryCareRecord
        '
        Me.chkSummaryCareRecord.AutoSize = True
        Me.chkSummaryCareRecord.Location = New System.Drawing.Point(89, 46)
        Me.chkSummaryCareRecord.Name = "chkSummaryCareRecord"
        Me.chkSummaryCareRecord.Size = New System.Drawing.Size(311, 18)
        Me.chkSummaryCareRecord.TabIndex = 1
        Me.chkSummaryCareRecord.Text = "Summary of Care Record Requested and Unavaliable"
        Me.chkSummaryCareRecord.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Remarks :"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(4, 207)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(452, 1)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 204)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(456, 4)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 204)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 3)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(454, 1)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "label1"
        '
        'frmAddSummaryCareRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(460, 264)
        Me.Controls.Add(Me.pnl_Main)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddSummaryCareRecord"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Summary Care Record"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnl_Main.ResumeLayout(False)
        Me.pnl_Main.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnSaveClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl_Main As System.Windows.Forms.Panel
    Public WithEvents dtSummaryCareRecordDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents chkSummaryCareRecord As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
End Class
