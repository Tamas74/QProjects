Imports gloEMRGeneralLibrary.gloEMRLab
Public Class frmLab_Acknoledgement
    Inherits System.Windows.Forms.Form
#Region " Windows Controls "
    Friend WithEvents pnlFileName As System.Windows.Forms.Panel
    Friend WithEvents txtOrderName As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_LabAcknoledgment As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
#End Region

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal _OrderID As Long, ByVal _OrderNumberPrefix As String, ByVal _OrderNumberID As Long)
        MyBase.New()
        OrderId = _OrderID
        OrderNumberPrefix = _OrderNumberPrefix
        OrderNumberID = _OrderNumberID
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpReviwed}
            Dim cntControls() As System.Windows.Forms.Control = {dtpReviwed}
            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try




            If (IsNothing(dtpControls) = False) Then
                If dtpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                End If
            End If


            If (IsNothing(cntControls) = False) Then
                If cntControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                End If
            End If

        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlDocument As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ImgList_Pages As System.Windows.Forms.ImageList
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents dtpReviwed As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbUsers As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLab_Acknoledgement))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlDocument = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.dtpReviwed = New System.Windows.Forms.DateTimePicker
        Me.cmbUsers = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.pnlFileName = New System.Windows.Forms.Panel
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.txtOrderName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ImgList_Pages = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_LabAcknoledgment = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        Me.pnlDocument.SuspendLayout()
        Me.pnlFileName.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_LabAcknoledgment.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlDocument)
        Me.pnlMain.Controls.Add(Me.pnlFileName)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(446, 222)
        Me.pnlMain.TabIndex = 48
        '
        'pnlDocument
        '
        Me.pnlDocument.Controls.Add(Me.Label5)
        Me.pnlDocument.Controls.Add(Me.Label6)
        Me.pnlDocument.Controls.Add(Me.Label7)
        Me.pnlDocument.Controls.Add(Me.Label8)
        Me.pnlDocument.Controls.Add(Me.txtUser)
        Me.pnlDocument.Controls.Add(Me.txtComments)
        Me.pnlDocument.Controls.Add(Me.dtpReviwed)
        Me.pnlDocument.Controls.Add(Me.cmbUsers)
        Me.pnlDocument.Controls.Add(Me.Label4)
        Me.pnlDocument.Controls.Add(Me.Label3)
        Me.pnlDocument.Controls.Add(Me.Label2)
        Me.pnlDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDocument.Location = New System.Drawing.Point(0, 39)
        Me.pnlDocument.Name = "pnlDocument"
        Me.pnlDocument.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDocument.Size = New System.Drawing.Size(446, 183)
        Me.pnlDocument.TabIndex = 44
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(4, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(438, 1)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 176)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(442, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 176)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(440, 1)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "label1"
        '
        'txtUser
        '
        Me.txtUser.BackColor = System.Drawing.Color.White
        Me.txtUser.ForeColor = System.Drawing.Color.Black
        Me.txtUser.Location = New System.Drawing.Point(112, 12)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.ReadOnly = True
        Me.txtUser.Size = New System.Drawing.Size(316, 22)
        Me.txtUser.TabIndex = 6
        '
        'txtComments
        '
        Me.txtComments.ForeColor = System.Drawing.Color.Black
        Me.txtComments.Location = New System.Drawing.Point(112, 78)
        Me.txtComments.MaxLength = 1000
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(316, 91)
        Me.txtComments.TabIndex = 5
        '
        'dtpReviwed
        '
        Me.dtpReviwed.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpReviwed.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpReviwed.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpReviwed.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpReviwed.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpReviwed.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtpReviwed.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReviwed.Location = New System.Drawing.Point(112, 42)
        Me.dtpReviwed.Name = "dtpReviwed"
        Me.dtpReviwed.Size = New System.Drawing.Size(174, 22)
        Me.dtpReviwed.TabIndex = 4
        '
        'cmbUsers
        '
        Me.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsers.ForeColor = System.Drawing.Color.Black
        Me.cmbUsers.Location = New System.Drawing.Point(112, 12)
        Me.cmbUsers.Name = "cmbUsers"
        Me.cmbUsers.Size = New System.Drawing.Size(316, 22)
        Me.cmbUsers.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Location = New System.Drawing.Point(35, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Comments :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(67, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Reviewed By :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlFileName
        '
        Me.pnlFileName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlFileName.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlFileName.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlFileName.Controls.Add(Me.lbl_pnlRight)
        Me.pnlFileName.Controls.Add(Me.lbl_pnlTop)
        Me.pnlFileName.Controls.Add(Me.txtOrderName)
        Me.pnlFileName.Controls.Add(Me.Label1)
        Me.pnlFileName.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFileName.Location = New System.Drawing.Point(0, 0)
        Me.pnlFileName.Name = "pnlFileName"
        Me.pnlFileName.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlFileName.Size = New System.Drawing.Size(446, 39)
        Me.pnlFileName.TabIndex = 42
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 35)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(438, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 32)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(442, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 32)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(440, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'txtOrderName
        '
        Me.txtOrderName.BackColor = System.Drawing.Color.White
        Me.txtOrderName.ForeColor = System.Drawing.Color.Black
        Me.txtOrderName.Location = New System.Drawing.Point(111, 8)
        Me.txtOrderName.Name = "txtOrderName"
        Me.txtOrderName.ReadOnly = True
        Me.txtOrderName.Size = New System.Drawing.Size(316, 22)
        Me.txtOrderName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Order Name :   "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ImgList_Pages
        '
        Me.ImgList_Pages.ImageStream = CType(resources.GetObject("ImgList_Pages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList_Pages.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList_Pages.Images.SetKeyName(0, "")
        Me.ImgList_Pages.Images.SetKeyName(1, "")
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_LabAcknoledgment)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(446, 54)
        Me.pnl_tlsp.TabIndex = 50
        '
        'tlsp_LabAcknoledgment
        '
        Me.tlsp_LabAcknoledgment.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_LabAcknoledgment.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_LabAcknoledgment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_LabAcknoledgment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_LabAcknoledgment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_LabAcknoledgment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_LabAcknoledgment.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_LabAcknoledgment.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_LabAcknoledgment.Name = "tlsp_LabAcknoledgment"
        Me.tlsp_LabAcknoledgment.Size = New System.Drawing.Size(446, 53)
        Me.tlsp_LabAcknoledgment.TabIndex = 0
        Me.tlsp_LabAcknoledgment.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmLab_Acknoledgement
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(446, 276)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLab_Acknoledgement"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lab Acknowledgment"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlDocument.ResumeLayout(False)
        Me.pnlDocument.PerformLayout()
        Me.pnlFileName.ResumeLayout(False)
        Me.pnlFileName.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_LabAcknoledgment.ResumeLayout(False)
        Me.tlsp_LabAcknoledgment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private OrderId As Long
    Private OrderNumberPrefix As String
    Private OrderNumberID As Long
    Public Shared ISsavedAckw As Boolean = False
    Public _ViewedDocumentPath As String = ""
    Public _ViewedDocumentDispName As String = ""


    Private Sub frmLab_Acknoledgement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  Dim i As Integer
        'Dim _DocumentPath As String

        txtOrderName.Text = OrderNumberPrefix & "-" & OrderNumberID
        txtComments.Text = ""
        dtpReviwed.Value = Format(Date.Now, "MM/dd/yyyy hh:mm:ss tt")
        cmbUsers.Visible = False

        'Dim oDB As New gloStream.gloDataBase.gloDataBase
        'Dim oDataReader As SqlClient.SqlDataReader
        'oDB.Connect(GetConnectionString)
        'oDataReader = oDB.ReadQueryRecords("SELECT nUserID,sLoginName FROM User_MST WHERE sLoginName IS NOT NULL ORDER BY sLoginName")
        'If oDataReader.HasRows = True Then
        '    While oDataReader.Read
        '        cmbUsers.Items.Add(oDataReader.Item("sLoginName"))
        '    End While
        'End If
        'oDB.Disconnect()

        'If cmbUsers.Items.Count > 0 Then
        '    For i = 0 To cmbUsers.Items.Count - 1
        '        If UCase(cmbUsers.Items.Item(i)) = gstrLoginName.ToUpper Then '// Item Change for VS 2005 on 27/06/2007 //
        '            cmbUsers.SelectedIndex = i
        '            Exit For
        '        End If
        '    Next
        'End If

        ''--Added by Anil to resolve bug no-583 on 20071214
        Try

       
            txtUser.Text = gstrLoginName
            txtUser.ReadOnly = True
            txtUser.BackColor = Color.White
            txtOrderName.BackColor = Color.White
            ''---------

            '4. Progress Bar 
            'ProgressBar1.Minimum = 0
            'ProgressBar1.Maximum = 100
            'ProgressBar1.Value = 0
            'ProgressBar1.Enabled = False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try
    End Sub

    Private Sub SaveAcknoledgement()

        Dim blnSuccess As Boolean = False
        Dim _ViwedUserID As Long = 0

        'If cmbUsers.SelectedItem Is Nothing Then
        '    MessageBox.Show("Please select reviwed by user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If



        If txtUser.Text = "" Then
            MessageBox.Show("Please enter User Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If txtComments.Text = "" Then
            MessageBox.Show("Please enter comments", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            Me.DialogResult = Windows.Forms.DialogResult.Cancel 'DialogResult.Cancel
            ' ProgressBar1.Enabled = True
            ts_btnSave.Enabled = False
            ts_btnClose.Enabled = False

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            _ViwedUserID = Val(oDB.ExecuteQueryScaler("SELECT nUserID FROM User_MST WHERE UPPER(sLoginName) = '" & UCase(txtUser.Text) & "' AND sLoginName IS NOT NULL"))
            oDB.Disconnect()

            'Dim oReviwed As New gloStream.gloDMS.Document.Document
            'If oReviwed.UpdateReviwed(_ViewedDocumentPath, _ViwedUserID, dtpReviwed.Value, txtComments.Text.Trim) = True Then
            '    Me.DialogResult = Windows.Forms.DialogResult.OK 'DialogResult.OK
            'End If
            'oReviwed = Nothing


            Dim oReviewed As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder

            'Developer:Sanjog Dhamke
            'Date: 20 Dec 2011 (6060)
            'Bug ID/PRD Name/Sales force Case: PRD Lab Usability - To show Add,Modify & Deletion functionality for ACkw.
            'Reason:this form is not in used but still it calls the Acknw. form Function

            oReviewed.Add_Acknowledgment(OrderId, OrderNumberPrefix, OrderNumberID, _ViwedUserID, dtpReviwed.Value, txtComments.Text.Trim, "", 0)

            oReviewed = Nothing

            'frmLab_RequestOrder.tlbbtn_Acknowledgment.Visible = False
            'frmLab_RequestOrder.tlbbtn_VWAcknowledgment.Visible = True
            ISsavedAckw = True
            DialogResult = Windows.Forms.DialogResult.OK
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Lab Order Acknowledgment Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Lab Order Acknowledgment Added", gloAuditTrail.ActivityOutCome.Success)
        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.Add, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(oError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        Finally
            'ProgressBar1.Enabled = False
            ts_btnSave.Enabled = True
            ts_btnClose.Enabled = True
        End Try
    End Sub

    Private Sub tlsp_LabAcknoledgment_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_LabAcknoledgment.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "save"
                    SaveAcknoledgement()

                Case "close"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub
End Class
