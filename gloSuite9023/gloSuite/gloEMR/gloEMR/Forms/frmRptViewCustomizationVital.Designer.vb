<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptViewVitalCustomization
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptViewVitalCustomization))
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtnShowReport = New System.Windows.Forms.ToolStripButton()
        Me.tblbtnViewReport = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Export_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Print_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlVitalsReport = New System.Windows.Forms.Panel()
        Me.chkDateRange = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpVitalsTo = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpVitalsFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlReport = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblStrip_32.SuspendLayout()
        Me.pnlVitalsReport.SuspendLayout()
        Me.pnlReport.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(887, 54)
        Me.pnlBottom.TabIndex = 1
        '
        'ReportViewer1
        '
        Me.ReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(3, 1)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(900, 522)
        Me.ReportViewer1.TabIndex = 0
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtnShowReport, Me.tblbtnViewReport, Me.tblbtn_Export_32, Me.tblbtn_Print_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(907, 53)
        Me.tblStrip_32.TabIndex = 12
        Me.tblStrip_32.TabStop = True
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtnShowReport
        '
        Me.tblbtnShowReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnShowReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtnShowReport.Image = CType(resources.GetObject("tblbtnShowReport.Image"), System.Drawing.Image)
        Me.tblbtnShowReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnShowReport.Name = "tblbtnShowReport"
        Me.tblbtnShowReport.Size = New System.Drawing.Size(117, 50)
        Me.tblbtnShowReport.Text = "Show &Basic Vitals"
        Me.tblbtnShowReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtnViewReport
        '
        Me.tblbtnViewReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tblbtnViewReport.Image = CType(resources.GetObject("tblbtnViewReport.Image"), System.Drawing.Image)
        Me.tblbtnViewReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnViewReport.Name = "tblbtnViewReport"
        Me.tblbtnViewReport.Size = New System.Drawing.Size(102, 50)
        Me.tblbtnViewReport.Text = "Show &All Vitals"
        Me.tblbtnViewReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Export_32
        '
        Me.tblbtn_Export_32.Enabled = False
        Me.tblbtn_Export_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Export_32.Image = CType(resources.GetObject("tblbtn_Export_32.Image"), System.Drawing.Image)
        Me.tblbtn_Export_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Export_32.Name = "tblbtn_Export_32"
        Me.tblbtn_Export_32.Size = New System.Drawing.Size(52, 50)
        Me.tblbtn_Export_32.Text = "&Export"
        Me.tblbtn_Export_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Print_32
        '
        Me.tblbtn_Print_32.Enabled = False
        Me.tblbtn_Print_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Print_32.Image = CType(resources.GetObject("tblbtn_Print_32.Image"), System.Drawing.Image)
        Me.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Print_32.Name = "tblbtn_Print_32"
        Me.tblbtn_Print_32.Size = New System.Drawing.Size(41, 50)
        Me.tblbtn_Print_32.Text = "&Print"
        Me.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlVitalsReport
        '
        Me.pnlVitalsReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlVitalsReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlVitalsReport.Controls.Add(Me.chkDateRange)
        Me.pnlVitalsReport.Controls.Add(Me.Label11)
        Me.pnlVitalsReport.Controls.Add(Me.dtpVitalsTo)
        Me.pnlVitalsReport.Controls.Add(Me.Label6)
        Me.pnlVitalsReport.Controls.Add(Me.Label7)
        Me.pnlVitalsReport.Controls.Add(Me.Label8)
        Me.pnlVitalsReport.Controls.Add(Me.Label9)
        Me.pnlVitalsReport.Controls.Add(Me.Label12)
        Me.pnlVitalsReport.Controls.Add(Me.dtpVitalsFrom)
        Me.pnlVitalsReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVitalsReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlVitalsReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlVitalsReport.Location = New System.Drawing.Point(0, 53)
        Me.pnlVitalsReport.Name = "pnlVitalsReport"
        Me.pnlVitalsReport.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlVitalsReport.Size = New System.Drawing.Size(907, 44)
        Me.pnlVitalsReport.TabIndex = 1
        '
        'chkDateRange
        '
        Me.chkDateRange.Checked = True
        Me.chkDateRange.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDateRange.Location = New System.Drawing.Point(75, 15)
        Me.chkDateRange.Name = "chkDateRange"
        Me.chkDateRange.Size = New System.Drawing.Size(15, 14)
        Me.chkDateRange.TabIndex = 1
        Me.chkDateRange.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(225, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 14)
        Me.Label11.TabIndex = 54
        Me.Label11.Text = "To :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpVitalsTo
        '
        Me.dtpVitalsTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpVitalsTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpVitalsTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpVitalsTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpVitalsTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpVitalsTo.CustomFormat = "MM/dd/yyyy"
        Me.dtpVitalsTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpVitalsTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVitalsTo.Location = New System.Drawing.Point(259, 11)
        Me.dtpVitalsTo.Name = "dtpVitalsTo"
        Me.dtpVitalsTo.Size = New System.Drawing.Size(98, 22)
        Me.dtpVitalsTo.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(4, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(899, 1)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 37)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(903, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 37)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(901, 1)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(27, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 14)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "From :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpVitalsFrom
        '
        Me.dtpVitalsFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpVitalsFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpVitalsFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpVitalsFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpVitalsFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpVitalsFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtpVitalsFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpVitalsFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVitalsFrom.Location = New System.Drawing.Point(95, 11)
        Me.dtpVitalsFrom.Name = "dtpVitalsFrom"
        Me.dtpVitalsFrom.Size = New System.Drawing.Size(98, 22)
        Me.dtpVitalsFrom.TabIndex = 2
        '
        'pnlReport
        '
        Me.pnlReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlReport.Controls.Add(Me.Label2)
        Me.pnlReport.Controls.Add(Me.Label3)
        Me.pnlReport.Controls.Add(Me.ReportViewer1)
        Me.pnlReport.Controls.Add(Me.Label4)
        Me.pnlReport.Controls.Add(Me.Label5)
        Me.pnlReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlReport.Location = New System.Drawing.Point(0, 97)
        Me.pnlReport.Name = "pnlReport"
        Me.pnlReport.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlReport.Size = New System.Drawing.Size(907, 526)
        Me.pnlReport.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(4, 522)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(899, 1)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 522)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(903, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 522)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(901, 1)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.AutoSize = True
        Me.pnlToolStrip.Controls.Add(Me.tblStrip_32)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(907, 53)
        Me.pnlToolStrip.TabIndex = 3
        '
        'frmRptViewVitalCustomization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(907, 623)
        Me.Controls.Add(Me.pnlReport)
        Me.Controls.Add(Me.pnlVitalsReport)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "frmRptViewVitalCustomization"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vitals Customization Report"
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlVitalsReport.ResumeLayout(False)
        Me.pnlVitalsReport.PerformLayout()
        Me.pnlReport.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtnShowReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Print_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlVitalsReport As System.Windows.Forms.Panel
    Friend WithEvents chkDateRange As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpVitalsTo As System.Windows.Forms.DateTimePicker
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpVitalsFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Public Sub New(ByVal PatientID As Long)
        InitializeComponent()
        _PatientID = PatientID
    End Sub
    Friend WithEvents tblbtnViewReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Export_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlReport As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel


    Public Sub New(ByVal PatientID As Long, ByVal ReportName As String)
        InitializeComponent()
        _PatientID = PatientID
        _RptName = ReportName
    End Sub
End Class
