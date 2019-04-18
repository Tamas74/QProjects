Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient

Public Class frmLocationStatus
    Inherits System.Windows.Forms.Form

    Dim _Location As String = ""
    Dim _Status As String = ""
    Dim nAppointmentID As Int64 = 0
    Friend WithEvents cntAdd As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAddLocation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddStatus As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_LocationStatus As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Dim _Users As New Collection

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal Location As String, ByVal Status As String, ByVal User As Collection, ByVal PatientID As Long)
        MyBase.New()

        _Location = Location
        _Status = Status
        _Users = User
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientId = PatientID
        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal AppointmentID As Int64, ByVal PatientID As Long)
        MyBase.New()

        nAppointmentID = AppointmentID
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _PatientId = PatientID
        'Add any initialization after the InitializeComponent() call

    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {cntAdd}

            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try



            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                End If
            End If
            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
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
    Friend WithEvents pnlLocation As System.Windows.Forms.Panel
    Friend WithEvents pnltrvLocation As System.Windows.Forms.Panel
    Friend WithEvents chklstLocation As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents pnlTrvStatus As System.Windows.Forms.Panel
    Friend WithEvents chklstStatus As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents pnlCHKUsers As System.Windows.Forms.Panel
    Friend WithEvents chklstUsers As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLocationStatus))
        Me.pnlLocation = New System.Windows.Forms.Panel
        Me.pnltrvLocation = New System.Windows.Forms.Panel
        Me.chklstLocation = New System.Windows.Forms.CheckedListBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlStatus = New System.Windows.Forms.Panel
        Me.pnlTrvStatus = New System.Windows.Forms.Panel
        Me.chklstStatus = New System.Windows.Forms.CheckedListBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.pnlUser = New System.Windows.Forms.Panel
        Me.pnlCHKUsers = New System.Windows.Forms.Panel
        Me.chklstUsers = New System.Windows.Forms.CheckedListBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.cntAdd = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAddLocation = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAddStatus = New System.Windows.Forms.ToolStripMenuItem
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_LocationStatus = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlLocation.SuspendLayout()
        Me.pnltrvLocation.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.pnlTrvStatus.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        Me.pnlCHKUsers.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.cntAdd.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_LocationStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLocation
        '
        Me.pnlLocation.Controls.Add(Me.pnltrvLocation)
        Me.pnlLocation.Controls.Add(Me.Panel4)
        Me.pnlLocation.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLocation.Location = New System.Drawing.Point(0, 53)
        Me.pnlLocation.Name = "pnlLocation"
        Me.pnlLocation.Size = New System.Drawing.Size(200, 415)
        Me.pnlLocation.TabIndex = 5
        '
        'pnltrvLocation
        '
        Me.pnltrvLocation.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvLocation.Controls.Add(Me.chklstLocation)
        Me.pnltrvLocation.Controls.Add(Me.Label29)
        Me.pnltrvLocation.Controls.Add(Me.Label28)
        Me.pnltrvLocation.Controls.Add(Me.Label12)
        Me.pnltrvLocation.Controls.Add(Me.Label13)
        Me.pnltrvLocation.Controls.Add(Me.Label14)
        Me.pnltrvLocation.Controls.Add(Me.Label15)
        Me.pnltrvLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvLocation.Location = New System.Drawing.Point(0, 32)
        Me.pnltrvLocation.Name = "pnltrvLocation"
        Me.pnltrvLocation.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrvLocation.Size = New System.Drawing.Size(200, 383)
        Me.pnltrvLocation.TabIndex = 3
        '
        'chklstLocation
        '
        Me.chklstLocation.BackColor = System.Drawing.Color.White
        Me.chklstLocation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklstLocation.CheckOnClick = True
        Me.chklstLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklstLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstLocation.ForeColor = System.Drawing.Color.Black
        Me.chklstLocation.Location = New System.Drawing.Point(7, 4)
        Me.chklstLocation.Name = "chklstLocation"
        Me.chklstLocation.Size = New System.Drawing.Size(192, 374)
        Me.chklstLocation.TabIndex = 4
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.White
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(7, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(192, 3)
        Me.Label29.TabIndex = 14
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.White
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(4, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(3, 378)
        Me.Label28.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(4, 379)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(195, 1)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 379)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(199, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 379)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(197, 1)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(200, 32)
        Me.Panel4.TabIndex = 21
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(197, 26)
        Me.Panel2.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 24)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "New Location "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(1, 25)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(195, 1)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 25)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(196, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 25)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(197, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(200, 53)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 415)
        Me.Splitter1.TabIndex = 13
        Me.Splitter1.TabStop = False
        '
        'pnlStatus
        '
        Me.pnlStatus.Controls.Add(Me.pnlTrvStatus)
        Me.pnlStatus.Controls.Add(Me.Panel3)
        Me.pnlStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlStatus.Location = New System.Drawing.Point(203, 53)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(200, 415)
        Me.pnlStatus.TabIndex = 14
        '
        'pnlTrvStatus
        '
        Me.pnlTrvStatus.BackColor = System.Drawing.Color.Transparent
        Me.pnlTrvStatus.Controls.Add(Me.chklstStatus)
        Me.pnlTrvStatus.Controls.Add(Me.Label30)
        Me.pnlTrvStatus.Controls.Add(Me.Label31)
        Me.pnlTrvStatus.Controls.Add(Me.Label4)
        Me.pnlTrvStatus.Controls.Add(Me.Label9)
        Me.pnlTrvStatus.Controls.Add(Me.Label10)
        Me.pnlTrvStatus.Controls.Add(Me.Label11)
        Me.pnlTrvStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTrvStatus.Location = New System.Drawing.Point(0, 32)
        Me.pnlTrvStatus.Name = "pnlTrvStatus"
        Me.pnlTrvStatus.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlTrvStatus.Size = New System.Drawing.Size(200, 383)
        Me.pnlTrvStatus.TabIndex = 4
        '
        'chklstStatus
        '
        Me.chklstStatus.BackColor = System.Drawing.Color.White
        Me.chklstStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklstStatus.CheckOnClick = True
        Me.chklstStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklstStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstStatus.ForeColor = System.Drawing.Color.Black
        Me.chklstStatus.Location = New System.Drawing.Point(4, 4)
        Me.chklstStatus.Name = "chklstStatus"
        Me.chklstStatus.Size = New System.Drawing.Size(195, 374)
        Me.chklstStatus.TabIndex = 4
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.White
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(4, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(195, 3)
        Me.Label30.TabIndex = 16
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.White
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(1, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(3, 378)
        Me.Label31.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1, 379)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(198, 1)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 379)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(199, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 379)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(200, 1)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(200, 32)
        Me.Panel3.TabIndex = 21
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), System.Drawing.Image)
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Controls.Add(Me.Label20)
        Me.Panel7.Controls.Add(Me.Label21)
        Me.Panel7.Controls.Add(Me.Label22)
        Me.Panel7.Controls.Add(Me.Label23)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 3)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(200, 26)
        Me.Panel7.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(1, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(198, 24)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "New Status"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1, 25)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(198, 1)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "label2"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 25)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(199, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 25)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(200, 1)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Location = New System.Drawing.Point(403, 53)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 415)
        Me.Splitter2.TabIndex = 15
        Me.Splitter2.TabStop = False
        '
        'pnlUser
        '
        Me.pnlUser.BackColor = System.Drawing.Color.Transparent
        Me.pnlUser.Controls.Add(Me.pnlCHKUsers)
        Me.pnlUser.Controls.Add(Me.Panel8)
        Me.pnlUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUser.Location = New System.Drawing.Point(406, 53)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(217, 415)
        Me.pnlUser.TabIndex = 17
        '
        'pnlCHKUsers
        '
        Me.pnlCHKUsers.BackColor = System.Drawing.Color.Transparent
        Me.pnlCHKUsers.Controls.Add(Me.chklstUsers)
        Me.pnlCHKUsers.Controls.Add(Me.Label32)
        Me.pnlCHKUsers.Controls.Add(Me.Label33)
        Me.pnlCHKUsers.Controls.Add(Me.Label5)
        Me.pnlCHKUsers.Controls.Add(Me.Label6)
        Me.pnlCHKUsers.Controls.Add(Me.Label7)
        Me.pnlCHKUsers.Controls.Add(Me.Label8)
        Me.pnlCHKUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCHKUsers.Location = New System.Drawing.Point(0, 32)
        Me.pnlCHKUsers.Name = "pnlCHKUsers"
        Me.pnlCHKUsers.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlCHKUsers.Size = New System.Drawing.Size(217, 383)
        Me.pnlCHKUsers.TabIndex = 4
        '
        'chklstUsers
        '
        Me.chklstUsers.BackColor = System.Drawing.Color.White
        Me.chklstUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklstUsers.CheckOnClick = True
        Me.chklstUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklstUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklstUsers.ForeColor = System.Drawing.Color.Black
        Me.chklstUsers.Location = New System.Drawing.Point(4, 4)
        Me.chklstUsers.Name = "chklstUsers"
        Me.chklstUsers.Size = New System.Drawing.Size(209, 374)
        Me.chklstUsers.TabIndex = 5
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.White
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(4, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(209, 3)
        Me.Label32.TabIndex = 16
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(1, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(3, 378)
        Me.Label33.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 379)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(212, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 379)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(213, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 379)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(214, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Panel9)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel8.Size = New System.Drawing.Size(217, 32)
        Me.Panel8.TabIndex = 21
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImage = CType(resources.GetObject("Panel9.BackgroundImage"), System.Drawing.Image)
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label1)
        Me.Panel9.Controls.Add(Me.Label24)
        Me.Panel9.Controls.Add(Me.Label25)
        Me.Panel9.Controls.Add(Me.Label26)
        Me.Panel9.Controls.Add(Me.Label27)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(0, 3)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(214, 26)
        Me.Panel9.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Send Task To"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(1, 25)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(212, 1)
        Me.Label24.TabIndex = 8
        Me.Label24.Text = "label2"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 25)
        Me.Label25.TabIndex = 7
        Me.Label25.Text = "label4"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(213, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 25)
        Me.Label26.TabIndex = 6
        Me.Label26.Text = "label3"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(214, 1)
        Me.Label27.TabIndex = 5
        Me.Label27.Text = "label1"
        '
        'cntAdd
        '
        Me.cntAdd.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddLocation, Me.mnuAddStatus})
        Me.cntAdd.Name = "cntAdd"
        Me.cntAdd.Size = New System.Drawing.Size(146, 48)
        '
        'mnuAddLocation
        '
        Me.mnuAddLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuAddLocation.Image = CType(resources.GetObject("mnuAddLocation.Image"), System.Drawing.Image)
        Me.mnuAddLocation.Name = "mnuAddLocation"
        Me.mnuAddLocation.Size = New System.Drawing.Size(145, 22)
        Me.mnuAddLocation.Text = "Add Location"
        '
        'mnuAddStatus
        '
        Me.mnuAddStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuAddStatus.Image = CType(resources.GetObject("mnuAddStatus.Image"), System.Drawing.Image)
        Me.mnuAddStatus.Name = "mnuAddStatus"
        Me.mnuAddStatus.Size = New System.Drawing.Size(145, 22)
        Me.mnuAddStatus.Text = "Add Status"
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_LocationStatus)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(623, 53)
        Me.pnl_tlsp.TabIndex = 18
        '
        'tlsp_LocationStatus
        '
        Me.tlsp_LocationStatus.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_LocationStatus.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_LocationStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_LocationStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_LocationStatus.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_LocationStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_LocationStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_LocationStatus.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_LocationStatus.Name = "tlsp_LocationStatus"
        Me.tlsp_LocationStatus.Size = New System.Drawing.Size(623, 53)
        Me.tlsp_LocationStatus.TabIndex = 0
        Me.tlsp_LocationStatus.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
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
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmLocationStatus
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(623, 468)
        Me.Controls.Add(Me.pnlUser)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLocation)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(2, 44)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLocationStatus"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set Patient's Location Status"
        Me.pnlLocation.ResumeLayout(False)
        Me.pnltrvLocation.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlTrvStatus.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlUser.ResumeLayout(False)
        Me.pnlCHKUsers.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.cntAdd.ResumeLayout(False)
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_LocationStatus.ResumeLayout(False)
        Me.tlsp_LocationStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "variables"
    Dim COL_COUNT As Int16 = 5

    Dim COL_ID As Integer = 0
    Dim COL_CLIENTID As Integer = 1
    Dim COL_MACHINENAME As Integer = 2
    Dim COL_LOCATION As Integer = 3
    Dim COL_STATUS As Integer = 4

    Dim _ErrorMessage As String = ""

#End Region

    ' Public lst As New myList
    'Dim dt As DataTable
    'Dim objclsDashBoard As clsDoctorsDashBoard
    Dim _strSQL As String = ""
    Dim _InProgress As Boolean = False ''Sandip Dardae  for single selection
    'Sandip Dardae BUG 6508
    Dim _isFormloading As Boolean = False
    Private _AuthorizeCheck As Boolean
    'integrated from 5076 -suppress task for patient status change setting
    Private _isEnableTasksforPatientStatusChange As Boolean = True
    Dim _PatientId As Long
 
    Private Sub frmLocationStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Sarika 23rd Apr
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        Dim _EnableTasksforPatientStatusChange As New Object
        Try
            'dtLocationStatus = New DataTable
            '_strSQL = "SELECT ClinicWorkFlowSettings.nID, ClientSettings_MST.nClientID, ClientSettings_MST.sMachineName, ClinicWorkFlowSettings.sLocation, ClinicWorkFlowSettings.sStatus FROM ClinicWorkFlowSettings RIGHT OUTER JOIN ClientSettings_MST ON ClinicWorkFlowSettings.nClientID = ClientSettings_MST.nClientID"
            'conn = New SqlConnection(GetConnectionString())
            'cmd = New SqlCommand(_strSQL, conn)
            'da = New SqlDataAdapter(cmd)
            'da.Fill(dtLocationStatus)
            _isFormloading = True
            _AuthorizeCheck = True
            GetstatusHistory()
            'SetGridStyle(dtLocationStatus)
            Call FillLocation()
            Call FillStatus()
            'integrated from 5076 -suppress task for patient status change setting
            'Call FillUsers()
            'code to read the suppress task setting from database 
            'If setting ON- UserList should not be visible
            'OFF-Userlist should be visible
            ogloSettings.GetSetting("EnableTasksforPatientStatusChange", _EnableTasksforPatientStatusChange)
            If IsDBNull(_EnableTasksforPatientStatusChange) = False Then
                If IsNothing(_EnableTasksforPatientStatusChange) = False Then
                    If _EnableTasksforPatientStatusChange = "" Then
                        _isEnableTasksforPatientStatusChange = True
                    Else
                        _isEnableTasksforPatientStatusChange = Convert.ToBoolean(Val(_EnableTasksforPatientStatusChange))
                    End If
                Else
                    _isEnableTasksforPatientStatusChange = True
                End If
            Else
                _isEnableTasksforPatientStatusChange = True
            End If

            'check setting
            If _isEnableTasksforPatientStatusChange = False Then
                ''if true make user list visible false 
                '' increase the size other two panels
                pnlUser.Visible = False
                pnlCHKUsers.Visible = False
                Splitter2.Visible = False
                pnlLocation.Size = New System.Drawing.Size(309, 415)
                pnlStatus.Size = New System.Drawing.Size(309, 415)
            Else
                ''fill userlist
                Call FillUsers()
            End If

            _AuthorizeCheck = False
            _isFormloading = False

        Catch ex As Exception
            MessageBox.Show("Error while retrieving location Status." & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If IsNothing(_EnableTasksforPatientStatusChange) = False Then
                _EnableTasksforPatientStatusChange = Nothing
            End If
            If IsNothing(ogloSettings) = False Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If
        End Try

    End Sub
    Public Sub FillLocation()

        Dim objclsDashBoard As clsDoctorsDashBoard = New clsDoctorsDashBoard
        'dt = New DataTable
        'If (IsNothing(dt) = False) Then
        '    dt.Dispose()
        '    dt = Nothing
        'End If
        With chklstLocation
            Dim dt As DataTable = objclsDashBoard.GetLocation()
            If IsNothing(dt) = False Then
                .DataSource = dt
                .DisplayMember = dt.Columns("sDescription").ColumnName.ToString
                .ValueMember = dt.Columns("nCategoryID").ColumnName
                ' .SelectedIndex = 0
                If _Location <> "" Then
                    For i As Integer = 0 To .Items.Count - 1
                        If CType(.Items(i), DataRowView).Row.Item("sDescription") = _Location Then
                            .SetSelected(i, True)
                            .SetItemChecked(i, True)

                            Exit For
                        End If
                    Next

                End If
            End If

        End With
        objclsDashBoard = Nothing
    End Sub
    Public Sub FillStatus()
        Dim objclsDashBoard As clsDoctorsDashBoard = New clsDoctorsDashBoard
        'If (IsNothing(dt) = False) Then
        '    dt.Dispose()
        '    dt = Nothing
        'End If
        With chklstStatus
            Dim dt As DataTable = objclsDashBoard.GetStatus()
            If IsNothing(dt) = False Then
                .DataSource = dt
                .DisplayMember = dt.Columns("sDescription").ColumnName.ToString
                .ValueMember = dt.Columns("nCategoryID").ColumnName
                '.SelectedIndex = 0

                If _Status <> "" Then
                    For i As Integer = 0 To .Items.Count - 1
                        If CType(.Items(i), DataRowView).Row.Item("sDescription") = _Status Then
                            .SetSelected(i, True)
                            .SetItemChecked(i, True)
                            Exit For
                        End If
                    Next
                End If
            End If
        End With
    End Sub
    Public Sub FillUsers()
        Dim objclsDashBoard As clsDoctorsDashBoard = New clsDoctorsDashBoard
        'dt = New DataTable
        With chklstUsers
            Dim dt As DataTable = objclsDashBoard.GetUsers()
            If IsNothing(dt) = False Then
                .DataSource = dt
                .DisplayMember = dt.Columns("sLoginName").ColumnName.ToString
                .ValueMember = dt.Columns("nUserID").ColumnName
                '.SelectedIndex = 0

                If IsNothing(_Users) = False Then
                    For i As Integer = 0 To .Items.Count - 1
                        For j As Int64 = 1 To _Users.Count
                            If CType(.Items(i), DataRowView).Row.Item("nUserID") = _Users(j) Then
                                .SetSelected(i, True)
                                .SetItemChecked(i, True)
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If
        End With
    End Sub

    Private Sub GetstatusHistory()

        Try


            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim dtStatusHistory As DataTable = Nothing
            Dim _strSQL As String = ""

            _strSQL = " SELECT  PatientTracking_DTL.nUserID, PatientTracking.nID, PatientTracking_2.sLocation, PatientTracking_2.sStatus FROM    PatientTracking_DTL INNER JOIN " _
                      & " PatientTracking ON PatientTracking_DTL.nID = PatientTracking.nID INNER JOIN PatientTracking AS PatientTracking_2 ON PatientTracking.nID = PatientTracking_2.nID " _
            & " WHERE PatientTracking_DTL.nID =(select top 1 nID from ( SELECT nID, dtDate, sTimeIn FROM PatientTracking WHERE PatientTracking.nDTLAppointmentID = " & nAppointmentID & ") MyTable order by dtdate desc, sTimeIn desc)"
            oDB.Connect(False)
            oDB.Retrive_Query(_strSQL, dtStatusHistory)


           
            For i As Int16 = 0 To dtStatusHistory.Rows.Count - 1

                _Status = Convert.ToString(dtStatusHistory.Rows(0)("sStatus"))
              
                _Location = Convert.ToString(dtStatusHistory.Rows(0)("sLocation"))
               
                _Users.Add(Convert.ToString(dtStatusHistory.Rows(i)("nUserID")))




            Next
            oDB.Dispose()
            oDB = Nothing
            dtStatusHistory.Dispose()
            dtStatusHistory = Nothing
        Catch ex As Exception
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patinet status history viewed ", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patinet status history viewed ", _PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''


        End Try
    End Sub

   

    Private Sub SaveLocationStatus()

        Try
            Dim i As Integer
            Dim strLocation As String = ""
            Dim strStatus As String = ""
            Dim strTimeIn As String = ""

            With chklstLocation
                For i = 0 To .CheckedItems.Count - 1
                    .SelectedItem = .CheckedItems(i)
                    strLocation = CType(CType((.SelectedItem), System.Data.DataRowView).Row, System.Data.DataRow).ItemArray(1)
                    'strLocation = strLocation & " - at " & Now.ToLongTimeString
                Next
            End With
            If strLocation.Trim = "" Then
                MessageBox.Show("Location is not selected", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            With chklstStatus
                For i = 0 To .CheckedItems.Count - 1
                    .SelectedItem = .CheckedItems(i)
                    strStatus = CType(CType((.SelectedItem), System.Data.DataRowView).Row, System.Data.DataRow).ItemArray(1)
                    'strStatus = strStatus & " - at " & Now.ToLongTimeString
                Next
            End With
            If strStatus.Trim = "" Then
                MessageBox.Show("Status is not selected", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'strTimeIn = Now.ToLongTimeString
            strTimeIn = Now.ToShortTimeString

            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim col_Users As New Collection

            With chklstUsers
                For i = 0 To .CheckedItems.Count - 1   ''  .CheckedIndices.Count - 1
                    Dim lst As myList = New myList
                    .SelectedItem = .CheckedItems(i)
                    lst.ID = CType(CType((.SelectedItem), System.Data.DataRowView).Row, System.Data.DataRow).ItemArray(0)
                    With lst
                        .Code = strLocation '' Location
                        .Description = strStatus '' Description
                        .Value = strTimeIn '' Time In
                    End With
                    col_Users.Add(lst)
                Next
            End With
            'If col_Users.Count <= 0 Then
            '    MessageBox.Show("Users are not selected", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            

            '@dtDate AS DateTime,
            '@nPatientID  NUMERIC(18,0), 
            '@sTimeIn VARCHAR(50), 
            '@sLocation VARCHAR(250),
            '@sStatus VARCHAR(250), 
            '@sTimeOut VARCHAR(50)
            Dim nID As Long = 0

            With oDB
                .DBParameters.Add("@dtDate", Now.Date, ParameterDirection.Input, SqlDbType.DateTime)
                .DBParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@nDTLAppointmentID", nAppointmentID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@nTrackingStatus", 8, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@sTimeIn", strTimeIn, ParameterDirection.Input, SqlDbType.VarChar, 50)
                .DBParameters.Add("@sLocation", strLocation, ParameterDirection.Input, SqlDbType.VarChar, 250)
                .DBParameters.Add("@sStatus", strStatus, ParameterDirection.Input, SqlDbType.VarChar, 250)
                .DBParameters.Add("@sTimeOut", "", ParameterDirection.Input, SqlDbType.VarChar, 50)
                '' For Check In / Status Pass 0 
                .DBParameters.Add("@nClinicID", gnClinicID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@bIsCheckOut", 0, ParameterDirection.Input, SqlDbType.Bit)
                .DBParameters.Add("@nID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt)

                .Connect(GetConnectionString)
                nID = .ExecuteNonQueryForOutput("gsp_INSERT_PatientTracking")
                .DBParameters.Clear()
                If nID <> 0 Then
                    'added on 20110202(5076) for setting to suppress task on status change
                    'functionality modifed in 5076
                    'the task generated during status will work on setting 
                    'if no user is selected than no task should generate whatever setting may be 
                    'there we give @nUserID=0 to make dummy entry
                    If col_Users.Count <= 0 Then
                        .DBParameters.Add("@nID", nID, ParameterDirection.Input, SqlDbType.BigInt)
                        .DBParameters.Add("@nUserID", 0, ParameterDirection.Input, SqlDbType.BigInt)
                        .ExecuteNonQuery("gsp_INSERT_PatientTracking_DTL")
                        .DBParameters.Clear()
                    Else
                        'more than one user is selected    
                        For i = 1 To col_Users.Count
                            .DBParameters.Add("@nID", nID, ParameterDirection.Input, SqlDbType.BigInt)
                            .DBParameters.Add("@nUserID", CType(col_Users(i), myList).ID, ParameterDirection.Input, SqlDbType.BigInt)
                            .ExecuteNonQuery("gsp_INSERT_PatientTracking_DTL")
                            .DBParameters.Clear()
                        Next

                        Dim ogloTask As New gloTaskMail.gloTask(GetConnectionString())
                        Dim oTask As New gloTaskMail.Task()
                        Dim oTaskProgress As New gloTaskMail.TaskProgress()

                        For i = 1 To col_Users.Count

                            Dim oTaskAssign As New gloTaskMail.TaskAssign()

                            oTaskAssign.AssignFromID = Convert.ToInt64(gnLoginID)
                            ' gnLoginID;
                            oTaskAssign.AssignFromName = ""
                            oTaskAssign.AssignToID = CType(col_Users(i), myList).ID
                            oTaskAssign.ClinicID = gnClinicID
                            If oTaskAssign.AssignFromID = oTaskAssign.AssignToID Then
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept
                            Else
                                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned
                                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
                            End If



                            oTaskAssign.AssignToName = ""

                            oTask.Assignment.Add(oTaskAssign)
                        Next


                        oTaskProgress.ClinicID = gnClinicID
                        ' gnClinicID;
                        oTaskProgress.Complete = 0
                        oTaskProgress.DateTime = DateTime.Now
                        oTaskProgress.Description = strStatus
                        oTaskProgress.StatusID = 1
                        '' Not Started 
                        oTaskProgress.TaskID = 0
                        ' TaskId;
                        '' 
                        oTask.UserID = gnLoginID
                        oTask.TaskType = gloTaskMail.TaskType.None
                        oTask.PatientID = _PatientId
                        'PatientID
                        oTask.Subject = strStatus
                        oTask.ClinicID = 1
                        ' gnClinicID;
                        oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortTimeString())
                        'taskdate
                        oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortTimeString())
                        'taskdate
                        oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortTimeString())
                        'taskduedate
                        oTask.IsPrivate = False
                        oTask.MachineName = System.Windows.Forms.SystemInformation.ComputerName
                        ' gstrClientMachineName;
                        oTask.Progress = oTaskProgress


                        oTask.PriorityID = 3 ''high



                        ogloTask.Add(oTask)
                        ogloTask.Dispose()
                        ogloTask = Nothing

                        oTask.Dispose()
                        oTask = Nothing
                        oTaskProgress.Dispose()
                        oTaskProgress = Nothing

                    End If
                End If


            
            End With
            ''''''''
            col_Users.Clear()
            col_Users = Nothing


           
            ''Added by Mayuri:20100423-To fix case no:#0003868
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.SetupAppointment, gloAuditTrail.ActivityType.Modify, "Patient status changed", _PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Me.Close()
            ''''''''
            oDB.Dispose()
            oDB = Nothing


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        
        End Try
    End Sub

    Private Sub chklstLocation_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklstLocation.MouseDown
        Try
            If e.Button = MouseButtons.Right Then

                cntAdd.Items(0).Visible = True
                cntAdd.Items(1).Visible = False
                'Try
                '    If (IsNothing(chklstLocation.ContextMenuStrip) = False) Then
                '        chklstLocation.ContextMenuStrip.Dispose()
                '        chklstLocation.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                chklstLocation.ContextMenuStrip = cntAdd
            Else
                ''Sandip Darade 20100329
                ''Bug 6508 (Patient Flow) --> On "Set Patient's Location Status", I click on "Location" or "Status" Panel, the check box get checked
                If (_isFormloading = False) Then

                    Dim loc As Point = chklstLocation.PointToClient(Cursor.Position)
                    For i As Integer = 0 To chklstLocation.Items.Count - 1
                        Dim rec As Rectangle = chklstLocation.GetItemRectangle(i)
                        rec.Width = pnlLocation.Width
                        'checkbox itself has a default width of about 16 pixels 
                        If rec.Contains(loc) Then
                            _AuthorizeCheck = True
                            Dim newValue As Boolean = Not chklstLocation.GetItemChecked(i)
                            chklstLocation.SetItemChecked(i, newValue)
                            'check 
                            _AuthorizeCheck = False

                            Exit Sub
                        End If
                    Next

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' 'added event to fix bug 11443
    Private Sub chklstStatus_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklstStatus.MouseClick
        Try

            If e.Button = MouseButtons.Right Then
                cntAdd.Items(0).Visible = False
                cntAdd.Items(1).Visible = True
                'Try
                '    If (IsNothing(chklstLocation.ContextMenuStrip) = False) Then
                '        chklstLocation.ContextMenuStrip.Dispose()
                '        chklstLocation.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                chklstStatus.ContextMenuStrip = cntAdd
            Else
                ''Sandip Darade 20100329
                ''Bug 6508 (Patient Flow) --> On "Set Patient's Location Status", I click on "Location" or "Status" Panel, the check box get checked
                If (_isFormloading = False) Then

                    Dim loc As Point = chklstStatus.PointToClient(Cursor.Position)
                    'For i As Integer = 0 To chklstStatus.Items.Count - 1
                    '    Dim rec As Rectangle = chklstStatus.GetItemRectangle(i)
                    '    rec.Width = 16
                    '    'checkbox itself has a default width of about 16 pixels 
                    '    If rec.Contains(loc) Then
                    '        _AuthorizeCheck = True
                    '        Dim newValue As Boolean = Not chklstStatus.GetItemChecked(i)
                    '        chklstStatus.SetItemChecked(i, newValue)
                    '        'check 
                    '        _AuthorizeCheck = False

                    '        Exit Sub
                    '    End If
                    'Next
                    Dim rec As Rectangle = chklstStatus.GetItemRectangle(chklstStatus.SelectedIndex)
                    rec.Width = pnlStatus.Width
                    'checkbox itself has a default width of about 16 pixels 
                    If rec.Contains(loc) Then
                        _AuthorizeCheck = True
                        Dim newValue As Boolean = Not chklstStatus.GetItemChecked(chklstStatus.SelectedIndex)
                        chklstStatus.SetItemChecked(chklstStatus.SelectedIndex, newValue)
                        'check 
                        _AuthorizeCheck = False

                        Exit Sub

                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    

    Dim _isStatusClicked As Boolean = False
    Private Sub chklstStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chklstStatus.SelectedIndexChanged
        Try


            Dim nStatusID As Long

            With chklstStatus
                'For i As Integer = 0 To .CheckedIndices.Count
                '    ' .SelectedItem = .Items(i)
                '    'Dim ind As Integer
                '    'ind = .SelectedIndex()
                '    .SetItemChecked(i, False)
                'Next
                '.SetItemChecked(.SelectedIndex, True)
                nStatusID = chklstStatus.SelectedItem.Row.ItemArray(0) ''chklstStatus.SelectedValue.Row.ItemArray(0)

            End With


            Dim dtAssociate As DataTable
            Dim clsDashBoard As New clsDoctorsDashBoard

            dtAssociate = clsDashBoard.GetAssociates(nStatusID)
            ''User_MST.nUserID, User_MST.sLoginName, Name  

            'Dim instance As CheckedListBox.CheckedItemCollection
            'lst = chklstUsers.CheckedItems

            'CType(chklstUsers.CheckedIndexCollection, IList)

            chklstUsers.BeginUpdate()
            For i As Integer = 0 To chklstUsers.CheckedIndices.Count - 1
                chklstUsers.SetItemChecked(i, False)
            Next



            If IsNothing(dtAssociate) = False Then
                'Dim myNode As TreeNode
                _isStatusClicked = True
                For j As Integer = 0 To dtAssociate.Rows.Count - 1
                    'myNode = New TreeNode
                    '.Text = dtAssociate.Rows(j)("sLoginName")
                    '.Tag = dtAssociate.Rows(j)("nUserID")

                    For i As Integer = 0 To chklstUsers.Items.Count - 1
                        chklstUsers.SelectedItem = chklstUsers.Items(i)
                        If dtAssociate.Rows(j)("nUserID") = chklstUsers.SelectedValue Then
                            If chklstStatus.CheckedIndices.Count > 0 Then
                                chklstUsers.SetItemCheckState(i, CheckState.Checked)
                            End If


                        End If
                    Next
                Next
                '.SelectedIndex = 0
            End If
            dtAssociate.Dispose()
            dtAssociate = Nothing
            clsDashBoard = Nothing

            '06-Jan-15 Aniket: Resolving Bug #77869: Patient Status - Send Task selection moves to the bottom of the List Close Editor  
            If chklstUsers.Items.Count > 0 Then
                chklstUsers.SelectedIndex = 0
            End If


            chklstUsers.EndUpdate()
            _isStatusClicked = False
        Catch ex As Exception
            chklstUsers.EndUpdate()
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuAddLocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddLocation.Click
        Dim frm As New CategoryMaster

        Try
            frm.Text = "Add Location"
            frm.IsfromLocation = True
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            FillLocation()


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm.IsfromLocation = False
            frm.Dispose()
            frm = Nothing
        End Try
    End Sub


    Private Sub mnuAddStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddStatus.Click
        Dim frm As New CategoryMaster
        Try
            frm.Text = "Add Status"
            frm.Isfromstatus = True
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            FillStatus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm.Isfromstatus = False
            frm.Dispose()
            frm = Nothing
        End Try
    End Sub

    Private Sub tlsp_LocationStatus_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_LocationStatus.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    SaveLocationStatus()
                Case "Close"
                    Me.Close()

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub
#Region "Code for single Selection "
    ''Sandip Darade 20100318
    Private Sub chklstLocation_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chklstLocation.ItemCheck
        If (_InProgress = True) Then
            Exit Sub
        End If
        If Not _AuthorizeCheck Then
            e.NewValue = e.CurrentValue
            'check state change was not through authorized actions 
        End If
        If (CheckState.Checked = CheckState.Checked) Then


            Dim _Text As String = Convert.ToString(chklstLocation.GetItemText(chklstLocation.SelectedItem))
            For Each i As Integer In chklstLocation.CheckedIndices


                If (Convert.ToString(chklstLocation.GetItemText(chklstLocation.Items.Item(i))) = _Text) Then

                Else
                    _InProgress = True
                    chklstLocation.SetItemChecked(i, False)

                End If
            Next


        End If
        _InProgress = False


    End Sub

    Private Sub chklstStatus_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chklstStatus.ItemCheck

        If (_InProgress = True Or _isStatusClicked = True) Then
            Exit Sub
        End If

        '30-Jan-15 Aniket: Resolving Bug #77869: Patient Status - Send Task selection moves to the bottom of the List
        chklstUsers.BeginUpdate()

        If Not _AuthorizeCheck Then
            e.NewValue = e.CurrentValue
            'check state change was not through authorized actions 
        End If

        If (CheckState.Checked = CheckState.Checked) Then


            Dim _Text As String = Convert.ToString(chklstStatus.GetItemText(chklstStatus.SelectedItem))
            For Each i As Integer In chklstStatus.CheckedIndices


                If (Convert.ToString(chklstStatus.GetItemText(chklstStatus.Items.Item(i))) = _Text) Then

                Else
                    _InProgress = True
                    chklstStatus.SetItemChecked(i, False)

                End If
            Next
            '' clear associated users 
            If _AuthorizeCheck Then


                For j As Integer = 0 To chklstUsers.Items.Count - 1
                    chklstUsers.SelectedItem = chklstUsers.Items(j)
                    chklstUsers.SetItemCheckState(j, False)
                Next

            End If
        End If

        If chklstUsers.Items.Count > 0 Then
            chklstUsers.SelectedIndex = 0
        End If

        chklstUsers.EndUpdate()


        _InProgress = False

    End Sub
    

   
#End Region

    Private Sub chklstUsers_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chklstUsers.ItemCheck

        If (_InProgress = True Or _isStatusClicked = True) Then
            Exit Sub
        End If
        If Not _AuthorizeCheck Then
            e.NewValue = e.CurrentValue
            'check state change was not through authorized actions 
        End If
        If (CheckState.Checked = CheckState.Checked) Then

            _InProgress = True
            chklstUsers.SetItemChecked(e.Index, False)


        End If
        _InProgress = False

    End Sub

    Private Sub chklstUsers_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles chklstUsers.MouseDown

        'Sandip Darade 20100402
        'Bug 6508 (Patient Flow) --> On "Set Patient's Location Status", I click on "Location" or "Status" Panel, the check box get checked
        If (_isFormloading = False) Then

            Dim loc As Point = chklstUsers.PointToClient(Cursor.Position)
            For i As Integer = 0 To chklstUsers.Items.Count - 1
                Dim rec As Rectangle = chklstUsers.GetItemRectangle(i)
                rec.Width = pnlLocation.Width
                'checkbox itself has a default width of about 16 pixels 
                If rec.Contains(loc) Then
                    _AuthorizeCheck = True
                    Dim newValue As Boolean = Not chklstUsers.GetItemChecked(i)
                    chklstUsers.SetItemChecked(i, newValue)
                    'check 
                    _AuthorizeCheck = False

                    Exit Sub
                End If
            Next

        End If
    End Sub
End Class
