<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientConsentTracking
    Inherits gloEMR.frmBaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
               Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpControls As DateTimePicker() = {dtConsentDate}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientConsentTracking))
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmbConsenttype = New System.Windows.Forms.ComboBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tblStrip = New gloToolStrip.gloToolStrip()
        Me.tblbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbconsentstatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCommentes = New System.Windows.Forms.TextBox()
        Me.cmbObtainedby = New System.Windows.Forms.ComboBox()
        Me.txtConsenter = New System.Windows.Forms.TextBox()
        Me.dtConsentDate = New System.Windows.Forms.DateTimePicker()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblBottom = New System.Windows.Forms.Label()
        Me.lblTop = New System.Windows.Forms.Label()
        Me.lblLeft = New System.Windows.Forms.Label()
        Me.lblRight = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.tblStrip.SuspendLayout()
        Me.pnl.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(28, 49)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(92, 14)
        Me.Label30.TabIndex = 8
        Me.Label30.Text = "Consent Type :"
        '
        'cmbConsenttype
        '
        Me.cmbConsenttype.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbConsenttype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbConsenttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbConsenttype.FormattingEnabled = True
        Me.cmbConsenttype.Location = New System.Drawing.Point(124, 45)
        Me.cmbConsenttype.Name = "cmbConsenttype"
        Me.cmbConsenttype.Size = New System.Drawing.Size(185, 22)
        Me.cmbConsenttype.TabIndex = 2
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(15, 20)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(105, 14)
        Me.Label43.TabIndex = 2
        Me.Label43.Text = "Date of Consent :"
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.tblStrip)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(674, 53)
        Me.Panel1.TabIndex = 18
        Me.Panel1.TabStop = True
        Me.Panel1.Tag = "1"
        '
        'tblStrip
        '
        Me.tblStrip.AddSeparatorsBetweenEachButton = False
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.ButtonsToHide = CType(resources.GetObject("tblStrip.ButtonsToHide"), System.Collections.ArrayList)
        Me.tblStrip.ConnectionString = Nothing
        Me.tblStrip.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Save, Me.tblbtn_Close})
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.ModuleName = Nothing
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(674, 53)
        Me.tblStrip.TabIndex = 1
        Me.tblStrip.TabStop = True
        Me.tblStrip.Text = "ToolStrip1"
        Me.tblStrip.UserID = CType(0, Long)
        '
        'tblbtn_Save
        '
        Me.tblbtn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save.Image = CType(resources.GetObject("tblbtn_Save.Image"), System.Drawing.Image)
        Me.tblbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save.Name = "tblbtn_Save"
        Me.tblbtn_Save.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save.Tag = "Save and Close"
        Me.tblbtn_Save.Text = "&Save&&Cls"
        Me.tblbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save.ToolTipText = "Save and Close"
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "&Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Close.ToolTipText = "Close"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(346, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 14)
        Me.Label3.TabIndex = 364
        Me.Label3.Text = "Consent Status :"
        '
        'cmbconsentstatus
        '
        Me.cmbconsentstatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbconsentstatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbconsentstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbconsentstatus.FormattingEnabled = True
        Me.cmbconsentstatus.Location = New System.Drawing.Point(454, 16)
        Me.cmbconsentstatus.Name = "cmbconsentstatus"
        Me.cmbconsentstatus.Size = New System.Drawing.Size(195, 22)
        Me.cmbconsentstatus.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(47, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 14)
        Me.Label1.TabIndex = 368
        Me.Label1.Text = "Comments :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(49, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 14)
        Me.Label2.TabIndex = 366
        Me.Label2.Text = "Consenter :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(369, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 14)
        Me.Label4.TabIndex = 367
        Me.Label4.Text = "Obtained By :"
        '
        'txtCommentes
        '
        Me.txtCommentes.Location = New System.Drawing.Point(124, 103)
        Me.txtCommentes.MaxLength = 500
        Me.txtCommentes.Multiline = True
        Me.txtCommentes.Name = "txtCommentes"
        Me.txtCommentes.Size = New System.Drawing.Size(525, 76)
        Me.txtCommentes.TabIndex = 5
        '
        'cmbObtainedby
        '
        Me.cmbObtainedby.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbObtainedby.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbObtainedby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbObtainedby.FormattingEnabled = True
        Me.cmbObtainedby.Location = New System.Drawing.Point(454, 45)
        Me.cmbObtainedby.Name = "cmbObtainedby"
        Me.cmbObtainedby.Size = New System.Drawing.Size(195, 22)
        Me.cmbObtainedby.TabIndex = 3
        '
        'txtConsenter
        '
        Me.txtConsenter.Location = New System.Drawing.Point(124, 74)
        Me.txtConsenter.MaxLength = 100
        Me.txtConsenter.Name = "txtConsenter"
        Me.txtConsenter.Size = New System.Drawing.Size(525, 22)
        Me.txtConsenter.TabIndex = 4
        '
        'dtConsentDate
        '
        Me.dtConsentDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtConsentDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtConsentDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtConsentDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtConsentDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtConsentDate.CustomFormat = "MM/dd/yyyy  hh:mm:ss tt"
        Me.dtConsentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtConsentDate.Location = New System.Drawing.Point(124, 16)
        Me.dtConsentDate.Name = "dtConsentDate"
        Me.dtConsentDate.Size = New System.Drawing.Size(185, 22)
        Me.dtConsentDate.TabIndex = 0
        '
        'pnl
        '
        Me.pnl.AutoSize = True
        Me.pnl.BackColor = System.Drawing.Color.Transparent
        Me.pnl.Controls.Add(Me.Label6)
        Me.pnl.Controls.Add(Me.Label5)
        Me.pnl.Controls.Add(Me.lblBottom)
        Me.pnl.Controls.Add(Me.dtConsentDate)
        Me.pnl.Controls.Add(Me.txtConsenter)
        Me.pnl.Controls.Add(Me.lblTop)
        Me.pnl.Controls.Add(Me.cmbObtainedby)
        Me.pnl.Controls.Add(Me.lblLeft)
        Me.pnl.Controls.Add(Me.txtCommentes)
        Me.pnl.Controls.Add(Me.lblRight)
        Me.pnl.Controls.Add(Me.Label1)
        Me.pnl.Controls.Add(Me.Label30)
        Me.pnl.Controls.Add(Me.Label2)
        Me.pnl.Controls.Add(Me.cmbConsenttype)
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.Label43)
        Me.pnl.Controls.Add(Me.Label3)
        Me.pnl.Controls.Add(Me.cmbconsentstatus)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(0, 53)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl.Size = New System.Drawing.Size(674, 196)
        Me.pnl.TabIndex = 369
        Me.pnl.Tag = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(12, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(14, 14)
        Me.Label6.TabIndex = 370
        Me.Label6.Text = "*"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(25, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 14)
        Me.Label5.TabIndex = 369
        Me.Label5.Text = "*"
        '
        'lblBottom
        '
        Me.lblBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBottom.Location = New System.Drawing.Point(4, 192)
        Me.lblBottom.Name = "lblBottom"
        Me.lblBottom.Size = New System.Drawing.Size(666, 1)
        Me.lblBottom.TabIndex = 12
        Me.lblBottom.Text = "label1"
        '
        'lblTop
        '
        Me.lblTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTop.Location = New System.Drawing.Point(4, 3)
        Me.lblTop.Name = "lblTop"
        Me.lblTop.Size = New System.Drawing.Size(666, 1)
        Me.lblTop.TabIndex = 11
        Me.lblTop.Text = "label1"
        '
        'lblLeft
        '
        Me.lblLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblLeft.Location = New System.Drawing.Point(3, 3)
        Me.lblLeft.Name = "lblLeft"
        Me.lblLeft.Size = New System.Drawing.Size(1, 190)
        Me.lblLeft.TabIndex = 10
        Me.lblLeft.Text = "label1"
        '
        'lblRight
        '
        Me.lblRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblRight.Location = New System.Drawing.Point(670, 3)
        Me.lblRight.Name = "lblRight"
        Me.lblRight.Size = New System.Drawing.Size(1, 190)
        Me.lblRight.TabIndex = 9
        Me.lblRight.Text = "label1"
        '
        'frmPatientConsentTracking
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(674, 249)
        Me.Controls.Add(Me.pnl)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPatientConsentTracking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient Consent Tracking"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbConsenttype As System.Windows.Forms.ComboBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents tblStrip As gloToolStrip.gloToolStrip
    Friend WithEvents tblbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbconsentstatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCommentes As System.Windows.Forms.TextBox
    Friend WithEvents cmbObtainedby As System.Windows.Forms.ComboBox
    Friend WithEvents txtConsenter As System.Windows.Forms.TextBox
    Friend WithEvents dtConsentDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Private WithEvents lblBottom As System.Windows.Forms.Label
    Private WithEvents lblTop As System.Windows.Forms.Label
    Private WithEvents lblLeft As System.Windows.Forms.Label
    Private WithEvents lblRight As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
