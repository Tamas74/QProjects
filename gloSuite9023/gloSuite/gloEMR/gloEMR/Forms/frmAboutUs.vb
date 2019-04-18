Imports System.Runtime.InteropServices

Public Class frmAboutUs
    Inherits System.Windows.Forms.Form

    '================= Load our bitmaps=====================
    Private bmpFrmBack As New Bitmap(gloGlobal.Properties.Resources.gloEMRAboutUs)
    '=======================================================

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'This is required for Windows Rounded Borders_ Form Ojeswini(11Jan2016)
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        'Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20))



    End Sub

    'This is required for Windows Rounded Borders_ Form Ojeswini(11Jan2016)
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")> _
    Private Shared Function CreateRoundRectRgn(nLeftRect As Integer, nTopRect As Integer, nRightRect As Integer, nBottomRect As Integer, nWidthEllipse As Integer, nHeightEllipse As Integer) As IntPtr
        ' width of ellipse
    End Function

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblProductVersion As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblBuildVersion As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemoteRefresh As System.Windows.Forms.Button
    Friend WithEvents lblRemoteIPAddress As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblRemoteUser As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblRemoteDomain As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblRemoteMachine As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLocalRefresh As System.Windows.Forms.Button
    Friend WithEvents lblLocalIPAddress As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblLocalUser As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblLocalDomain As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblLocalMachine As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Private WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAboutUs))
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnLocalRefresh = New System.Windows.Forms.Button()
        Me.btnRemoteRefresh = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblProductVersion = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblBuildVersion = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblRemoteIPAddress = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblRemoteUser = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblRemoteDomain = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblRemoteMachine = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblLocalIPAddress = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblLocalUser = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblLocalDomain = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblLocalMachine = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(347, 18)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 8
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(469, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(34, 30)
        Me.Button1.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.Button1, "Close")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnLocalRefresh
        '
        Me.btnLocalRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLocalRefresh.FlatAppearance.BorderSize = 0
        Me.btnLocalRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLocalRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLocalRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLocalRefresh.Image = CType(resources.GetObject("btnLocalRefresh.Image"), System.Drawing.Image)
        Me.btnLocalRefresh.Location = New System.Drawing.Point(392, 15)
        Me.btnLocalRefresh.Name = "btnLocalRefresh"
        Me.btnLocalRefresh.Size = New System.Drawing.Size(26, 26)
        Me.btnLocalRefresh.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.btnLocalRefresh, "Refresh")
        Me.btnLocalRefresh.UseVisualStyleBackColor = True
        '
        'btnRemoteRefresh
        '
        Me.btnRemoteRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRemoteRefresh.FlatAppearance.BorderSize = 0
        Me.btnRemoteRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoteRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoteRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoteRefresh.Image = CType(resources.GetObject("btnRemoteRefresh.Image"), System.Drawing.Image)
        Me.btnRemoteRefresh.Location = New System.Drawing.Point(392, 16)
        Me.btnRemoteRefresh.Name = "btnRemoteRefresh"
        Me.btnRemoteRefresh.Size = New System.Drawing.Size(26, 26)
        Me.btnRemoteRefresh.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnRemoteRefresh, "Refresh")
        Me.btnRemoteRefresh.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.Panel2.Size = New System.Drawing.Size(515, 30)
        Me.Panel2.TabIndex = 13
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Controls.Add(Me.PictureBox4)
        Me.Panel4.Controls.Add(Me.PictureBox5)
        Me.Panel4.Controls.Add(Me.pictureBox1)
        Me.Panel4.Controls.Add(Me.GroupBox3)
        Me.Panel4.Controls.Add(Me.GroupBox2)
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(515, 549)
        Me.Panel4.TabIndex = 26
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = CType(resources.GetObject("PictureBox4.Image"), System.Drawing.Image)
        Me.PictureBox4.Location = New System.Drawing.Point(271, 495)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(70, 24)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox4.TabIndex = 152
        Me.PictureBox4.TabStop = False
        Me.PictureBox4.Visible = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"), System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(396, 49)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(94, 28)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 151
        Me.PictureBox5.TabStop = False
        '
        'pictureBox1
        '
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(27, 31)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(156, 63)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox1.TabIndex = 150
        Me.pictureBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.Panel5)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(23, 111)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(467, 60)
        Me.GroupBox3.TabIndex = 114
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Product Information"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.lblProductVersion)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Controls.Add(Me.lblBuildVersion)
        Me.Panel5.Location = New System.Drawing.Point(146, 25)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(200, 22)
        Me.Panel5.TabIndex = 25
        '
        'lblProductVersion
        '
        Me.lblProductVersion.AutoSize = True
        Me.lblProductVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblProductVersion.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblProductVersion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblProductVersion.Location = New System.Drawing.Point(53, 0)
        Me.lblProductVersion.Name = "lblProductVersion"
        Me.lblProductVersion.Size = New System.Drawing.Size(44, 13)
        Me.lblProductVersion.TabIndex = 24
        Me.lblProductVersion.Text = "5.0.1.1"
        Me.lblProductVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(41, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(12, 13)
        Me.Label26.TabIndex = 16
        Me.Label26.Text = "-"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBuildVersion
        '
        Me.lblBuildVersion.AutoSize = True
        Me.lblBuildVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblBuildVersion.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblBuildVersion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBuildVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblBuildVersion.Location = New System.Drawing.Point(0, 0)
        Me.lblBuildVersion.Name = "lblBuildVersion"
        Me.lblBuildVersion.Size = New System.Drawing.Size(41, 13)
        Me.lblBuildVersion.TabIndex = 15
        Me.lblBuildVersion.Text = "BUILD"
        Me.lblBuildVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(67, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Build ID :"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.btnRemoteRefresh)
        Me.GroupBox2.Controls.Add(Me.lblRemoteIPAddress)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.lblRemoteUser)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.lblRemoteDomain)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.lblRemoteMachine)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(23, 282)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(467, 109)
        Me.GroupBox2.TabIndex = 114
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Remote"
        '
        'lblRemoteIPAddress
        '
        Me.lblRemoteIPAddress.AutoSize = True
        Me.lblRemoteIPAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRemoteIPAddress.Location = New System.Drawing.Point(143, 85)
        Me.lblRemoteIPAddress.Name = "lblRemoteIPAddress"
        Me.lblRemoteIPAddress.Size = New System.Drawing.Size(51, 13)
        Me.lblRemoteIPAddress.TabIndex = 7
        Me.lblRemoteIPAddress.Text = "Label10"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(51, 85)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "IP Address :"
        '
        'lblRemoteUser
        '
        Me.lblRemoteUser.AutoSize = True
        Me.lblRemoteUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRemoteUser.Location = New System.Drawing.Point(143, 64)
        Me.lblRemoteUser.Name = "lblRemoteUser"
        Me.lblRemoteUser.Size = New System.Drawing.Size(51, 13)
        Me.lblRemoteUser.TabIndex = 5
        Me.lblRemoteUser.Text = "Label10"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(51, 64)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(74, 13)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "User Name :"
        '
        'lblRemoteDomain
        '
        Me.lblRemoteDomain.AutoSize = True
        Me.lblRemoteDomain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRemoteDomain.Location = New System.Drawing.Point(143, 43)
        Me.lblRemoteDomain.Name = "lblRemoteDomain"
        Me.lblRemoteDomain.Size = New System.Drawing.Size(51, 13)
        Me.lblRemoteDomain.TabIndex = 3
        Me.lblRemoteDomain.Text = "Label10"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(34, 43)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(91, 13)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Domain Name :"
        '
        'lblRemoteMachine
        '
        Me.lblRemoteMachine.AutoSize = True
        Me.lblRemoteMachine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRemoteMachine.Location = New System.Drawing.Point(143, 22)
        Me.lblRemoteMachine.Name = "lblRemoteMachine"
        Me.lblRemoteMachine.Size = New System.Drawing.Size(51, 13)
        Me.lblRemoteMachine.TabIndex = 1
        Me.lblRemoteMachine.Text = "Label10"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(30, 22)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(95, 13)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Machine Name :"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.btnLocalRefresh)
        Me.GroupBox1.Controls.Add(Me.lblLocalIPAddress)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblLocalUser)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.lblLocalDomain)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.lblLocalMachine)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(23, 172)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(467, 109)
        Me.GroupBox1.TabIndex = 113
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Local"
        '
        'lblLocalIPAddress
        '
        Me.lblLocalIPAddress.AutoSize = True
        Me.lblLocalIPAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLocalIPAddress.Location = New System.Drawing.Point(144, 85)
        Me.lblLocalIPAddress.Name = "lblLocalIPAddress"
        Me.lblLocalIPAddress.Size = New System.Drawing.Size(51, 13)
        Me.lblLocalIPAddress.TabIndex = 7
        Me.lblLocalIPAddress.Text = "Label10"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(50, 85)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(74, 13)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "IP Address :"
        '
        'lblLocalUser
        '
        Me.lblLocalUser.AutoSize = True
        Me.lblLocalUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLocalUser.Location = New System.Drawing.Point(144, 64)
        Me.lblLocalUser.Name = "lblLocalUser"
        Me.lblLocalUser.Size = New System.Drawing.Size(51, 13)
        Me.lblLocalUser.TabIndex = 5
        Me.lblLocalUser.Text = "Label10"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(50, 64)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "User Name :"
        '
        'lblLocalDomain
        '
        Me.lblLocalDomain.AutoSize = True
        Me.lblLocalDomain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLocalDomain.Location = New System.Drawing.Point(144, 43)
        Me.lblLocalDomain.Name = "lblLocalDomain"
        Me.lblLocalDomain.Size = New System.Drawing.Size(51, 13)
        Me.lblLocalDomain.TabIndex = 3
        Me.lblLocalDomain.Text = "Label10"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(33, 43)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(91, 13)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Domain Name :"
        '
        'lblLocalMachine
        '
        Me.lblLocalMachine.AutoSize = True
        Me.lblLocalMachine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblLocalMachine.Location = New System.Drawing.Point(144, 22)
        Me.lblLocalMachine.Name = "lblLocalMachine"
        Me.lblLocalMachine.Size = New System.Drawing.Size(51, 13)
        Me.lblLocalMachine.TabIndex = 1
        Me.lblLocalMachine.Text = "Label10"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(29, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Machine Name :"
        '
        'frmAboutUs
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(515, 549)
        Me.Controls.Add(Me.Panel4)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(515, 549)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(515, 549)
        Me.Name = "frmAboutUs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About Us"
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub


#End Region




    Private Sub frmAboutUs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '=============Make our bitmap region for the form
        gloGlobal.BitmapRegion.CreateControlRegion(Me, bmpFrmBack)
        Label6.Focus()
        '=======================================================

        '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
        'Dim objGlobalMisc As New gloGlobal.clsMISC

        lblBuildVersion.Visible = True

        lblBuildVersion.Text = gloEMR.My.Application.Info.Version.ToString
        lblProductVersion.Text = gstrVersion

        '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
        Dim localMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.LocalMachineDetails()
        Dim remoteMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.RemoteMachineDetails()
        lblLocalMachine.Text = localMachine.MachineName
        lblLocalDomain.Text = localMachine.DomainName
        lblLocalIPAddress.Text = localMachine.MachineIp
        lblLocalUser.Text = localMachine.UserName
        lblRemoteMachine.Text = remoteMachine.MachineName
        lblRemoteDomain.Text = remoteMachine.DomainName
        lblRemoteIPAddress.Text = remoteMachine.MachineIp
        lblRemoteUser.Text = remoteMachine.UserName

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click, btnRemoteRefresh.Click
        Dim remoteMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.RemoteMachineDetails(True)
        lblRemoteMachine.Text = remoteMachine.MachineName
        lblRemoteDomain.Text = remoteMachine.DomainName
        lblRemoteIPAddress.Text = remoteMachine.MachineIp
        lblRemoteUser.Text = remoteMachine.UserName
    End Sub

    Private Sub btnLocalRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnLocalRefresh.Click
        Dim localMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.LocalMachineDetails(True)
        lblLocalMachine.Text = localMachine.MachineName
        lblLocalDomain.Text = localMachine.DomainName
        lblLocalIPAddress.Text = localMachine.MachineIp
        lblLocalUser.Text = localMachine.UserName

    End Sub
End Class

