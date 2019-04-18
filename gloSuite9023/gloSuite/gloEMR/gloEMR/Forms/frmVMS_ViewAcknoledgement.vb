Public Class frmVMS_ViewAcknoledgement
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        


        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then

            Try
                If (IsNothing(dtpReviwed) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpReviwed)
                    Catch ex As Exception

                    End Try


                    dtpReviwed.Dispose()
                    dtpReviwed = Nothing
                End If
            Catch
            End Try


            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlDocument As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents ImgList_Pages As System.Windows.Forms.ImageList
    Friend WithEvents pnlFileName As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents dtpReviwed As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    Friend WithEvents lblUsers As System.Windows.Forms.Label
    Friend WithEvents lblReviwed As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls_VMSImportDocument As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblComments As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVMS_ViewAcknoledgement))
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.pnlDocument = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblComments = New System.Windows.Forms.Label
        Me.lblReviwed = New System.Windows.Forms.Label
        Me.lblUsers = New System.Windows.Forms.Label
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.dtpReviwed = New System.Windows.Forms.DateTimePicker
        Me.cmbUsers = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlFileName = New System.Windows.Forms.Panel
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.ImgList_Pages = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tls_VMSImportDocument = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlDocument.SuspendLayout()
        Me.pnlFileName.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls_VMSImportDocument.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.ForeColor = System.Drawing.Color.LimeGreen
        Me.ProgressBar1.Location = New System.Drawing.Point(4, 141)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(406, 15)
        Me.ProgressBar1.TabIndex = 18
        Me.ProgressBar1.Visible = False
        '
        'pnlDocument
        '
        Me.pnlDocument.Controls.Add(Me.ProgressBar1)
        Me.pnlDocument.Controls.Add(Me.Label5)
        Me.pnlDocument.Controls.Add(Me.Label6)
        Me.pnlDocument.Controls.Add(Me.Label7)
        Me.pnlDocument.Controls.Add(Me.Label8)
        Me.pnlDocument.Controls.Add(Me.lblComments)
        Me.pnlDocument.Controls.Add(Me.lblReviwed)
        Me.pnlDocument.Controls.Add(Me.lblUsers)
        Me.pnlDocument.Controls.Add(Me.txtComments)
        Me.pnlDocument.Controls.Add(Me.dtpReviwed)
        Me.pnlDocument.Controls.Add(Me.cmbUsers)
        Me.pnlDocument.Controls.Add(Me.Label4)
        Me.pnlDocument.Controls.Add(Me.Label3)
        Me.pnlDocument.Controls.Add(Me.Label2)
        Me.pnlDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDocument.Location = New System.Drawing.Point(0, 84)
        Me.pnlDocument.Name = "pnlDocument"
        Me.pnlDocument.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDocument.Size = New System.Drawing.Size(414, 160)
        Me.pnlDocument.TabIndex = 44
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 156)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(406, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 156)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(410, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 156)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(408, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'lblComments
        '
        Me.lblComments.BackColor = System.Drawing.Color.White
        Me.lblComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComments.ForeColor = System.Drawing.Color.Black
        Me.lblComments.Location = New System.Drawing.Point(104, 69)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(275, 69)
        Me.lblComments.TabIndex = 8
        '
        'lblReviwed
        '
        Me.lblReviwed.BackColor = System.Drawing.Color.White
        Me.lblReviwed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReviwed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReviwed.ForeColor = System.Drawing.Color.Black
        Me.lblReviwed.Location = New System.Drawing.Point(104, 39)
        Me.lblReviwed.Name = "lblReviwed"
        Me.lblReviwed.Size = New System.Drawing.Size(170, 22)
        Me.lblReviwed.TabIndex = 7
        Me.lblReviwed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUsers
        '
        Me.lblUsers.BackColor = System.Drawing.Color.White
        Me.lblUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsers.ForeColor = System.Drawing.Color.Black
        Me.lblUsers.Location = New System.Drawing.Point(104, 9)
        Me.lblUsers.Name = "lblUsers"
        Me.lblUsers.Size = New System.Drawing.Size(170, 22)
        Me.lblUsers.TabIndex = 6
        Me.lblUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComments
        '
        Me.txtComments.Enabled = False
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(104, 69)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(275, 69)
        Me.txtComments.TabIndex = 5
        Me.txtComments.Visible = False
        '
        'dtpReviwed
        '
        Me.dtpReviwed.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpReviwed.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpReviwed.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpReviwed.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpReviwed.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpReviwed.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpReviwed.Enabled = False
        Me.dtpReviwed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReviwed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReviwed.Location = New System.Drawing.Point(104, 39)
        Me.dtpReviwed.Name = "dtpReviwed"
        Me.dtpReviwed.Size = New System.Drawing.Size(170, 22)
        Me.dtpReviwed.TabIndex = 4
        Me.dtpReviwed.Visible = False
        '
        'cmbUsers
        '
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.Enabled = False
        Me.cmbUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUsers.Location = New System.Drawing.Point(104, 9)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(170, 22)
        Me.cmbUsers.TabIndex = 3
        Me.cmbUsers.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Comments :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(59, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Reviewed By :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlFileName
        '
        Me.pnlFileName.BackColor = System.Drawing.Color.Transparent
        Me.pnlFileName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlFileName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFileName.Controls.Add(Me.txtFileName)
        Me.pnlFileName.Controls.Add(Me.Label1)
        Me.pnlFileName.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlFileName.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlFileName.Controls.Add(Me.lbl_RightBrd)
        Me.pnlFileName.Controls.Add(Me.lbl_TopBrd)
        Me.pnlFileName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFileName.Location = New System.Drawing.Point(3, 3)
        Me.pnlFileName.Name = "pnlFileName"
        Me.pnlFileName.Size = New System.Drawing.Size(408, 24)
        Me.pnlFileName.TabIndex = 42
        '
        'txtFileName
        '
        Me.txtFileName.BackColor = System.Drawing.Color.White
        Me.txtFileName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtFileName.ForeColor = System.Drawing.Color.Black
        Me.txtFileName.Location = New System.Drawing.Point(99, 1)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(172, 22)
        Me.txtFileName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "File Name : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(406, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(407, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(408, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'ImgList_Pages
        '
        Me.ImgList_Pages.ImageStream = CType(resources.GetObject("ImgList_Pages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList_Pages.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList_Pages.Images.SetKeyName(0, "")
        Me.ImgList_Pages.Images.SetKeyName(1, "")
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls_VMSImportDocument)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(414, 54)
        Me.pnl_tlspTOP.TabIndex = 56
        '
        'tls_VMSImportDocument
        '
        Me.tls_VMSImportDocument.BackColor = System.Drawing.Color.Transparent
        Me.tls_VMSImportDocument.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_VMSImportDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_VMSImportDocument.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_VMSImportDocument.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_VMSImportDocument.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tls_VMSImportDocument.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_VMSImportDocument.Location = New System.Drawing.Point(0, 0)
        Me.tls_VMSImportDocument.Name = "tls_VMSImportDocument"
        Me.tls_VMSImportDocument.Size = New System.Drawing.Size(414, 53)
        Me.tls_VMSImportDocument.TabIndex = 0
        Me.tls_VMSImportDocument.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlFileName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(414, 30)
        Me.Panel2.TabIndex = 57
        '
        'frmVMS_ViewAcknoledgement
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(414, 244)
        Me.Controls.Add(Me.pnlDocument)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVMS_ViewAcknoledgement"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Acknowledgment"
        Me.pnlDocument.ResumeLayout(False)
        Me.pnlDocument.PerformLayout()
        Me.pnlFileName.ResumeLayout(False)
        Me.pnlFileName.PerformLayout()
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls_VMSImportDocument.ResumeLayout(False)
        Me.tls_VMSImportDocument.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public _ViewedDocumentPath As String = ""
    Public _ViewedDocumentDispName As String = ""

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmVMS_ViewAcknoledgement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim _DocumentPath As String = ""
        Try
            txtFileName.Text = _ViewedDocumentDispName
            txtComments.Text = ""
            dtpReviwed.Value = Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt")

            cmbUsers.Items.Clear()
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlClient.SqlDataReader
            oDB.Connect(GetConnectionString)
            oDataReader = oDB.ReadQueryRecords("SELECT nUserID,sLoginName FROM User_MST WHERE sLoginName IS NOT NULL ORDER BY sLoginName")
            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    cmbUsers.Items.Add(oDataReader.Item("sLoginName"))
                End While
            End If
            oDB.Disconnect()

            If cmbUsers.Items.Count > 0 Then
                For i = 0 To cmbUsers.Items.Count - 1
                    If UCase(cmbUsers.Items(i)) = gstrLoginName.ToUpper Then
                        cmbUsers.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If

            Dim oReviwed As New gloStream.gloVMS.Document.document
            Dim oReviwedDetail As New gloStream.gloVMS.Document.ReviwedDetail

            oReviwedDetail = oReviwed.ViewReviwed(_ViewedDocumentPath)
            If Not oReviwedDetail Is Nothing Then
                If cmbUsers.Items.Count > 0 Then
                    For i = 0 To cmbUsers.Items.Count - 1
                        If UCase(cmbUsers.Items(i)) = oReviwedDetail.ReviwedByUserName.ToUpper Then
                            cmbUsers.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
                lblUsers.Text = oReviwedDetail.ReviwedByUserName

                If IsDate(oReviwedDetail.ReviwedDateTime) Then
                    dtpReviwed.Value = oReviwedDetail.ReviwedDateTime
                End If
                lblReviwed.Text = oReviwedDetail.ReviwedDateTime

                txtComments.Text = oReviwedDetail.Comments
                lblComments.Text = oReviwedDetail.Comments

            End If
            oReviwed = Nothing
            oReviwedDetail = Nothing

            '4. Progress Bar 
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = 100
            ProgressBar1.Value = 0
            ProgressBar1.Enabled = False
        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.DMS), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnOk.Click
        Dim blnSuccess As Boolean = False
        Dim _ViwedUserID As Long = 0

        If cmbUsers.SelectedItem Is Nothing Then
            MessageBox.Show("Select reviwed by User", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            '' ProgressBar1.Enabled = True
            'btnOK.Enabled = False
            'btnCancel.Enabled = False

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            _ViwedUserID = Val(oDB.ExecuteQueryScaler("SELECT nUserID FROM User_MST WHERE UPPER(sLoginName) = '" & UCase(cmbUsers.SelectedItem) & "' AND sLoginName IS NOT NULL"))
            oDB.Disconnect()

            Dim oReviwed As New gloStream.gloVMS.Document.document
            If oReviwed.UpdateReviwed(_ViewedDocumentPath, _ViwedUserID, dtpReviwed.Value, txtComments.Text.Trim) = True Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
            oReviwed = Nothing

        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.DMS), gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        Finally
            ProgressBar1.Enabled = False
            'btnOK.Enabled = True
            'btnCancel.Enabled = True
        End Try
    End Sub

   
End Class
