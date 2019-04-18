<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDisclosureSet
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    If (IsNothing(dtpToDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate)
                        Catch ex As Exception

                        End Try


                        dtpToDate.Dispose()
                        dtpToDate = Nothing
                    End If
                Catch
                End Try

                Try
                    If (IsNothing(dtpFromDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFromDate)
                        Catch ex As Exception

                        End Try


                        dtpFromDate.Dispose()
                        dtpFromDate = Nothing
                    End If
                Catch
                End Try

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDisclosureSet))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tlsDisclosureSet = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsPrint = New System.Windows.Forms.ToolStripButton
        Me.tlsSave = New System.Windows.Forms.ToolStripButton
        Me.tlsClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.trvAssociation = New System.Windows.Forms.TreeView
        Me.ImgList = New System.Windows.Forms.ImageList(Me.components)
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker
        Me.cmbDisclosureSet = New System.Windows.Forms.ComboBox
        Me.lblDisclosureset = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pnlTop.SuspendLayout()
        Me.tlsDisclosureSet.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.tlsDisclosureSet)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(729, 53)
        Me.pnlTop.TabIndex = 0
        '
        'tlsDisclosureSet
        '
        Me.tlsDisclosureSet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDisclosureSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDisclosureSet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDisclosureSet.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDisclosureSet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsPrint, Me.tlsSave, Me.tlsClose})
        Me.tlsDisclosureSet.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDisclosureSet.Location = New System.Drawing.Point(0, 0)
        Me.tlsDisclosureSet.Name = "tlsDisclosureSet"
        Me.tlsDisclosureSet.Size = New System.Drawing.Size(729, 53)
        Me.tlsDisclosureSet.TabIndex = 3
        Me.tlsDisclosureSet.Text = "ToolStrip1"
        '
        'tlsPrint
        '
        Me.tlsPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPrint.Image = CType(resources.GetObject("tlsPrint.Image"), System.Drawing.Image)
        Me.tlsPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsPrint.Name = "tlsPrint"
        Me.tlsPrint.Size = New System.Drawing.Size(41, 50)
        Me.tlsPrint.Tag = "Print"
        Me.tlsPrint.Text = "&Print"
        Me.tlsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsSave
        '
        Me.tlsSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsSave.Image = CType(resources.GetObject("tlsSave.Image"), System.Drawing.Image)
        Me.tlsSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsSave.Name = "tlsSave"
        Me.tlsSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsSave.Tag = "Save"
        Me.tlsSave.Text = "&Save&&Cls"
        Me.tlsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsSave.ToolTipText = "Save and Close"
        '
        'tlsClose
        '
        Me.tlsClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsClose.Image = CType(resources.GetObject("tlsClose.Image"), System.Drawing.Image)
        Me.tlsClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsClose.Name = "tlsClose"
        Me.tlsClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsClose.Tag = "Close"
        Me.tlsClose.Text = "&Close"
        Me.tlsClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 53)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(729, 508)
        Me.pnlMain.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.trvAssociation)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 30)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(729, 478)
        Me.Panel2.TabIndex = 2
        '
        'trvAssociation
        '
        Me.trvAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAssociation.ForeColor = System.Drawing.Color.Black
        Me.trvAssociation.ImageIndex = 0
        Me.trvAssociation.ImageList = Me.ImgList
        Me.trvAssociation.Indent = 20
        Me.trvAssociation.ItemHeight = 20
        Me.trvAssociation.Location = New System.Drawing.Point(4, 4)
        Me.trvAssociation.Name = "trvAssociation"
        Me.trvAssociation.SelectedImageIndex = 0
        Me.trvAssociation.ShowRootLines = False
        Me.trvAssociation.Size = New System.Drawing.Size(721, 470)
        Me.trvAssociation.TabIndex = 0
        '
        'ImgList
        '
        Me.ImgList.ImageStream = CType(resources.GetObject("ImgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList.Images.SetKeyName(0, "Pat Demographic.ico")
        Me.ImgList.Images.SetKeyName(1, "History.ico")
        Me.ImgList.Images.SetKeyName(2, "Drugs.ico")
        Me.ImgList.Images.SetKeyName(3, "Past Exam01.ico")
        Me.ImgList.Images.SetKeyName(4, "Lab orders.ico")
        Me.ImgList.Images.SetKeyName(5, "Radiology.ico")
        Me.ImgList.Images.SetKeyName(6, "DMS Settings.ico")
        Me.ImgList.Images.SetKeyName(7, "Bullet06.ico")
        Me.ImgList.Images.SetKeyName(8, "Orders.ico")
        Me.ImgList.Images.SetKeyName(9, "Patient Consent.ico")
        Me.ImgList.Images.SetKeyName(10, "FLow sheet.ico")
        Me.ImgList.Images.SetKeyName(11, "Immunization.ico")
        Me.ImgList.Images.SetKeyName(12, "Message.ico")
        Me.ImgList.Images.SetKeyName(13, "Vitals.ico")
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(4, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(721, 3)
        Me.Label7.TabIndex = 41
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(4, 474)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(721, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 474)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(725, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 474)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(723, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.dtpFromDate)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.dtpToDate)
        Me.Panel1.Controls.Add(Me.cmbDisclosureSet)
        Me.Panel1.Controls.Add(Me.lblDisclosureset)
        Me.Panel1.Controls.Add(Me.lbl_pnlTop)
        Me.Panel1.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel1.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel1.Controls.Add(Me.lbl_pnlRight)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(723, 24)
        Me.Panel1.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(395, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label9.Size = New System.Drawing.Size(76, 20)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "From Date :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFromDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtpFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(471, 1)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(95, 22)
        Me.dtpFromDate.TabIndex = 43
        Me.dtpFromDate.Value = New Date(2005, 8, 3, 16, 21, 41, 890)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(566, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label8.Size = New System.Drawing.Size(64, 20)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "To Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtpToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(630, 1)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(92, 22)
        Me.dtpToDate.TabIndex = 41
        Me.dtpToDate.Value = New Date(2005, 8, 3, 16, 21, 41, 875)
        '
        'cmbDisclosureSet
        '
        Me.cmbDisclosureSet.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbDisclosureSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDisclosureSet.ForeColor = System.Drawing.Color.Black
        Me.cmbDisclosureSet.FormattingEnabled = True
        Me.cmbDisclosureSet.Location = New System.Drawing.Point(91, 1)
        Me.cmbDisclosureSet.Name = "cmbDisclosureSet"
        Me.cmbDisclosureSet.Size = New System.Drawing.Size(277, 22)
        Me.cmbDisclosureSet.TabIndex = 0
        '
        'lblDisclosureset
        '
        Me.lblDisclosureset.BackColor = System.Drawing.Color.Transparent
        Me.lblDisclosureset.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblDisclosureset.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisclosureset.Location = New System.Drawing.Point(1, 1)
        Me.lblDisclosureset.Name = "lblDisclosureset"
        Me.lblDisclosureset.Size = New System.Drawing.Size(90, 22)
        Me.lblDisclosureset.TabIndex = 1
        Me.lblDisclosureset.Text = "  Select Set :"
        Me.lblDisclosureset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(721, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 23)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(721, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(722, 0)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel3.Size = New System.Drawing.Size(729, 30)
        Me.Panel3.TabIndex = 3
        '
        'frmDisclosureSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDisclosureSet"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Disclosure Set"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tlsDisclosureSet.ResumeLayout(False)
        Me.tlsDisclosureSet.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents trvAssociation As System.Windows.Forms.TreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbDisclosureSet As System.Windows.Forms.ComboBox
    Friend WithEvents tlsDisclosureSet As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImgList As System.Windows.Forms.ImageList
    Friend WithEvents lblDisclosureset As System.Windows.Forms.Label
    Friend WithEvents tlsClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
End Class
