'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.IO
Imports System.Data.SqlClient
Imports gloSettings


Public Class frmSplash
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        sethelpBuildermode()
        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents pnlLogin As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Private WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents pnlDataBase As System.Windows.Forms.Panel
    Friend WithEvents cmbDatabaseName As System.Windows.Forms.ComboBox
    Friend WithEvents lblDataBase As System.Windows.Forms.Label
    Private WithEvents pnlLoginButton As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents lblCopyrghTag As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents picAnimation As System.Windows.Forms.PictureBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.pnlLogin = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.picAnimation = New System.Windows.Forms.PictureBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlUser = New System.Windows.Forms.Panel()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlDataBase = New System.Windows.Forms.Panel()
        Me.cmbDatabaseName = New System.Windows.Forms.ComboBox()
        Me.lblDataBase = New System.Windows.Forms.Label()
        Me.pnlLoginButton = New System.Windows.Forms.Panel()
        Me.label5 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.label2 = New System.Windows.Forms.Label()
        Me.lblCopyrghTag = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.picAnimation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        Me.pnlDataBase.SuspendLayout()
        Me.pnlLoginButton.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLogin
        '
        Me.pnlLogin.BackColor = System.Drawing.Color.Transparent
        Me.pnlLogin.Location = New System.Drawing.Point(851, 86)
        Me.pnlLogin.Name = "pnlLogin"
        Me.pnlLogin.Size = New System.Drawing.Size(164, 10)
        Me.pnlLogin.TabIndex = 1
        Me.pnlLogin.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'picAnimation
        '
        Me.picAnimation.BackColor = System.Drawing.Color.Transparent
        Me.picAnimation.Image = CType(resources.GetObject("picAnimation.Image"), System.Drawing.Image)
        Me.picAnimation.Location = New System.Drawing.Point(456, 426)
        Me.picAnimation.Name = "picAnimation"
        Me.picAnimation.Size = New System.Drawing.Size(61, 11)
        Me.picAnimation.TabIndex = 2
        Me.picAnimation.TabStop = False
        Me.picAnimation.Visible = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Arial", 21.05!, System.Drawing.FontStyle.Bold)
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(450, 403)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(0, 34)
        Me.lblVersion.TabIndex = 14
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVersion.Visible = False
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Font = New System.Drawing.Font("Droid Sans", 8.25!)
        Me.lblDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.lblDate.Location = New System.Drawing.Point(23, 502)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(72, 13)
        Me.lblDate.TabIndex = 15
        Me.lblDate.Text = "Mar 05, 2016"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.pnlUser)
        Me.Panel1.Controls.Add(Me.pnlDataBase)
        Me.Panel1.Controls.Add(Me.pnlLoginButton)
        Me.Panel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(567, 22)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(221, 133)
        Me.Panel1.TabIndex = 22
        '
        'pnlUser
        '
        Me.pnlUser.BackColor = System.Drawing.Color.Transparent
        Me.pnlUser.Controls.Add(Me.txtPassword)
        Me.pnlUser.Controls.Add(Me.Label3)
        Me.pnlUser.Controls.Add(Me.txtUserName)
        Me.pnlUser.Controls.Add(Me.Label4)
        Me.pnlUser.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlUser.Location = New System.Drawing.Point(0, 0)
        Me.pnlUser.Name = "pnlUser"
        Me.pnlUser.Size = New System.Drawing.Size(221, 68)
        Me.pnlUser.TabIndex = 20
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtPassword.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(75, 39)
        Me.txtPassword.MaxLength = 100
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(141, 20)
        Me.txtPassword.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial Rounded MT Bold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(-3, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 12)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "PASSWORD:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserName
        '
        Me.txtUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserName.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(75, 8)
        Me.txtUserName.MaxLength = 50
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(141, 20)
        Me.txtUserName.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial Rounded MT Bold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(0, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 12)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "USERNAME:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlDataBase
        '
        Me.pnlDataBase.BackColor = System.Drawing.Color.Transparent
        Me.pnlDataBase.Controls.Add(Me.cmbDatabaseName)
        Me.pnlDataBase.Controls.Add(Me.lblDataBase)
        Me.pnlDataBase.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDataBase.Location = New System.Drawing.Point(0, 68)
        Me.pnlDataBase.Name = "pnlDataBase"
        Me.pnlDataBase.Size = New System.Drawing.Size(221, 30)
        Me.pnlDataBase.TabIndex = 21
        '
        'cmbDatabaseName
        '
        Me.cmbDatabaseName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDatabaseName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDatabaseName.FormattingEnabled = True
        Me.cmbDatabaseName.IntegralHeight = False
        Me.cmbDatabaseName.ItemHeight = 14
        Me.cmbDatabaseName.Location = New System.Drawing.Point(75, 2)
        Me.cmbDatabaseName.Name = "cmbDatabaseName"
        Me.cmbDatabaseName.Size = New System.Drawing.Size(141, 22)
        Me.cmbDatabaseName.TabIndex = 2
        '
        'lblDataBase
        '
        Me.lblDataBase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDataBase.AutoSize = True
        Me.lblDataBase.BackColor = System.Drawing.Color.Transparent
        Me.lblDataBase.Font = New System.Drawing.Font("Arial Rounded MT Bold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDataBase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.lblDataBase.Location = New System.Drawing.Point(2, 6)
        Me.lblDataBase.Name = "lblDataBase"
        Me.lblDataBase.Size = New System.Drawing.Size(69, 12)
        Me.lblDataBase.TabIndex = 14
        Me.lblDataBase.Text = "DATABASE:"
        Me.lblDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlLoginButton
        '
        Me.pnlLoginButton.BackColor = System.Drawing.Color.Transparent
        Me.pnlLoginButton.Controls.Add(Me.btnCancel)
        Me.pnlLoginButton.Controls.Add(Me.btnLogin)
        Me.pnlLoginButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLoginButton.Location = New System.Drawing.Point(0, 98)
        Me.pnlLoginButton.Name = "pnlLoginButton"
        Me.pnlLoginButton.Size = New System.Drawing.Size(221, 35)
        Me.pnlLoginButton.TabIndex = 22
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Font = New System.Drawing.Font("Arial", 7.95!, System.Drawing.FontStyle.Bold)
        Me.label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.label5.Image = CType(resources.GetObject("label5.Image"), System.Drawing.Image)
        Me.label5.Location = New System.Drawing.Point(582, 494)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(58, 22)
        Me.label5.TabIndex = 155
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.label5.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_BlueBtnSplashScreen
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(116, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.btnLogin.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_OrangeBtnSplashScreen
        Me.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(2, 5)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(100, 23)
        Me.btnLogin.TabIndex = 3
        Me.btnLogin.Text = "&Login"
        Me.btnLogin.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.Controls.Add(Me.label2)
        Me.Panel3.Location = New System.Drawing.Point(431, 398)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(18, 11)
        Me.Panel3.TabIndex = 23
        Me.Panel3.Visible = False
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label2.Font = New System.Drawing.Font("Arial", 5.0!)
        Me.label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.label2.Location = New System.Drawing.Point(0, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(14, 7)
        Me.label2.TabIndex = 18
        Me.label2.Text = "TM"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblCopyrghTag
        '
        Me.lblCopyrghTag.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyrghTag.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCopyrghTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCopyrghTag.Font = New System.Drawing.Font("Droid Sans", 8.25!)
        Me.lblCopyrghTag.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.lblCopyrghTag.Location = New System.Drawing.Point(0, 0)
        Me.lblCopyrghTag.Name = "lblCopyrghTag"
        Me.lblCopyrghTag.Size = New System.Drawing.Size(800, 15)
        Me.lblCopyrghTag.TabIndex = 8
        Me.lblCopyrghTag.Text = "CPT® copyright 2015 American Medical Association. All rights reserved."
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.label1)
        Me.Panel2.Controls.Add(Me.lblCopyrghTag)
        Me.Panel2.Location = New System.Drawing.Point(24, 520)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(800, 76)
        Me.Panel2.TabIndex = 16
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.label1.Font = New System.Drawing.Font("Droid Sans", 8.25!)
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.label1.Location = New System.Drawing.Point(0, 15)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(800, 60)
        Me.label1.TabIndex = 9
        Me.label1.Text = resources.GetString("label1.Text")
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 7.95!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label9.Image = CType(resources.GetObject("Label9.Image"), System.Drawing.Image)
        Me.Label9.Location = New System.Drawing.Point(630, 482)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 38)
        Me.Label9.TabIndex = 156
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label9.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 7.95!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label6.Image = CType(resources.GetObject("Label6.Image"), System.Drawing.Image)
        Me.Label6.Location = New System.Drawing.Point(625, 160)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 40)
        Me.Label6.TabIndex = 158
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(651, 196)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(62, 18)
        Me.Label7.TabIndex = 159
        Me.Label7.Text = "ADMIN"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmSplash
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(837, 605)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.picAnimation)
        Me.Controls.Add(Me.pnlLogin)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gloPM Admin"
        CType(Me.picAnimation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnlUser.ResumeLayout(False)
        Me.pnlUser.PerformLayout()
        Me.pnlDataBase.ResumeLayout(False)
        Me.pnlDataBase.PerformLayout()
        Me.pnlLoginButton.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim strPrevLoginName As String
    Dim nNoOfLoginAttempt As Byte
    Dim AccessFlag As Boolean = False
    Dim dtDatabase As New DataTable

    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding
    ''Const sServiceDatabaseName = "gloServices"
    '' Dim sServiceDatabaseName As String = gstrServicesDBName
    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding

    Public Shared gstrHelpProvider As String = "CLIENT"

    ''----------------- Load our bitmaps
    'Private bmpFrmBack As New Bitmap(GetType(frmSplash), "SplashTransperentFront.bmp")
    'Private bmpFrmWhite As New Bitmap(GetType(frmSplash), "whitebitmap.bmp")
    ''------------------------------------------------------

    Private Sub sethelpBuildermode()
        Try

            'Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER 
            'Changes to read write registry based on OS
            If GetOSInfo() Then
                gloRegistrySetting.IsServerOS = True
            End If
            '-----

            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("HelpProvider")) = True Then
                gloRegistrySetting.SetRegistryValue("HelpProvider", "Client")
                gloEMR.Help.HelpComponent.blnbuildmode = False
                gstrHelpProvider = "CLIENT"
            Else
                gstrHelpProvider = gloRegistrySetting.GetRegistryValue("HelpProvider").ToString()
                If gstrHelpProvider.ToUpper() = "CLIENT" Then
                    gloEMR.Help.HelpComponent.blnbuildmode = False

                Else
                    gloEMR.Help.HelpComponent.blnbuildmode = True
                End If
            End If
            gloRegistrySetting.CloseRegistryKey()
        Catch ex As Exception

        End Try
    End Sub




    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        'Retrieve the Computer Name and store it in global variable
        gstrClientMachineName = System.Windows.Forms.SystemInformation.ComputerName()
        'Display gloEMR Version No
        'lblVersion.Text = "Version: " & RetrieveVersion()
        Call ShowDateStamp()

        'code start by nilesh on 20110228 for case GLO2010-0008612
        CheckAnotherInstance()

        'code end by nilesh on 20110228 for case GLO2010-0008612

        'code comment start by nilesh on 20110228 for case GLO2010-0008612
        'Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName

        'Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)

        'If Process.GetProcessesByName(aProcName).Length > 1 Then
        '    MessageBox.Show("Another instance of this application is already running.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Application.Exit()
        '    End
        '    Exit Sub
        'End If
        'code comment end by nilesh on 20110228 for case GLO2010-0008612

        If IsSettings() = False Then
            Dim frmSettings As New frmStartupSettings
            Me.Hide()
            If frmSettings.ShowDialog() = DialogResult.OK Then
                Me.Visible = True
                pnlLogin.Visible = True
                picAnimation.Visible = False
            End If
        End If

        ''Added sServiceDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table   
        ''To Fill the Database from gloservice

        FillServiceDatabases()
        ''Added sServiceDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table   


        Dim objSettings As New clsStartUpSettings
        If objSettings.IsConnect(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR) = False Then
            If MessageBox.Show("Unable to connect to SQL Server " & gstrSQLServerName & " and Database " & gstrDatabaseName & vbCrLf & "Do you want to change SQL Server or Database Settings?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                End
            End If
            Dim frmSettings As New frmStartupSettings
            Me.Hide()
            If frmSettings.ShowDialog() = DialogResult.OK Then
                Me.Visible = True
                pnlLogin.Visible = True
                picAnimation.Visible = False
                FillServiceDatabases()
            End If
            Exit Sub
        End If
        Me.Visible = True
        pnlLogin.Visible = True
        picAnimation.Visible = False
        ''Madhu Sandeep 20110520
        '' For Admin updates throguh AUS Service.

        Dim strConnectionstring As String = String.Empty
        strConnectionstring = gloPMAdmin.mdlGeneral.GetConnectionString()

        Dim objAUS As New gloAUSLibrary.clsAus()

        objAUS.CheckForAdminUpdate(strConnectionstring, "pmadmin")
        txtUserName.Focus()
    End Sub

    'code start by nilesh on 20110228 for case GLO2010-0008612
    'check process for another instance as per session
    Private Sub CheckAnotherInstance()
        Try
            Dim _currentSessionID As Int32 = System.Diagnostics.Process.GetCurrentProcess().SessionId
            Dim _currentProcName As String = System.Diagnostics.Process.GetCurrentProcess().ProcessName
            Dim _currentProcessID As Int32 = System.Diagnostics.Process.GetCurrentProcess().Id

            Dim _Process() As System.Diagnostics.Process = Process.GetProcessesByName(_currentProcName)

            If _Process.Length > 1 Then
                For Each _proc As System.Diagnostics.Process In _Process
                    If _proc.SessionId = _currentSessionID And _proc.Id <> _currentProcessID And _proc.ProcessName = _currentProcName Then
                        MessageBox.Show("Another instance of this application is already running.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Application.Exit()
                        End
                        Exit Sub
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'code end by nilesh on 20110228 for case GLO2010-0008612


    'Private Function IsSettings() As Boolean
    '    Dim regKey As RegistryKey
    '    If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
    '        Return False
    '    End If
    '    regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR")
    '    If IsNothing(regKey.GetValue("SQLServer")) = True Then
    '        regKey.Close()
    '        regKey = Nothing
    '        Return False
    '    End If
    '    If IsNothing(regKey.GetValue("Database")) = True Then
    '        regKey.Close()
    '        regKey = Nothing
    '        Return False
    '    End If
    '    If IsNothing(regKey.GetValue("ArchiveDatabase")) = True Then
    '        regKey.Close()
    '        regKey = Nothing
    '        Return False
    '    End If
    '    gstrSQLServerName = regKey.GetValue("SQLServer")
    '    gstrDatabaseName = regKey.GetValue("Database")
    '    gstrArchiveDatabaseName = regKey.GetValue("ArchiveDatabase")
    '    gstrDomainName = regKey.GetValue("Domain")
    '    gstrWindowsServerName = regKey.GetValue("WindowsServer")
    '    regKey.Close()
    '    regKey = Nothing

    '    If Trim(gstrSQLServerName) = "" Or Trim(gstrDatabaseName) = "" Or Trim(gstrDomainName) = "" Or Trim(gstrWindowsServerName) = "" Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    Private Function IsSettings() As Boolean
        gstrAdminFor = "gloPM"

        If (gstrAdminFor = "gloEMR") Then
            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                Return False
            End If
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            gstrMessageBoxCaption = "gloEMR Admin"
        Else
            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM) = False Then
                Return False
            End If
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, True)
            gstrMessageBoxCaption = "gloPM Admin"
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("SQLServer")) = True Then
            gloRegistrySetting.CloseRegistryKey()
            Return False
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("Database")) = True Then
            gloRegistrySetting.CloseRegistryKey()
            Return False
        End If
        If (gstrAdminFor = "gloEMR") Then
            If IsNothing(gloRegistrySetting.GetRegistryValue("ArchiveDatabase")) = True Then
                gloRegistrySetting.CloseRegistryKey()
                Return False
            End If
        End If
        gstrSQLServerName = gloRegistrySetting.GetRegistryValue("SQLServer")
        gstrDatabaseName = gloRegistrySetting.GetRegistryValue("Database")

        '' SUDHIR 20090727 ''
        If gloRegistrySetting.GetRegistryValue("IsSQLAuthentication") IsNot Nothing Then
            gblnSQLAuthentication = gloRegistrySetting.GetRegistryValue("IsSQLAuthentication")
        End If
        If gloRegistrySetting.GetRegistryValue("SQLUserEMR") IsNot Nothing Then
            gstrSQLUserEMR = gloRegistrySetting.GetRegistryValue("SQLUserEMR")
        End If
        If gloRegistrySetting.GetRegistryValue("SQLPasswordEMR") IsNot Nothing Then
            If gloRegistrySetting.GetRegistryValue("SQLPasswordEMR") <> "" Then
                Dim oEncryption As New clsEncryption
                gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(gloRegistrySetting.GetRegistryValue("SQLPasswordEMR"), constEncryptDecryptKey_Services)
                oEncryption = Nothing
            End If
        End If
        '' END SUDHIR ''

        ''Sandip Darade 20090725
        If (gstrAdminFor = "gloEMR") Then
            gstrArchiveDatabaseName = gloRegistrySetting.GetRegistryValue("ArchiveDatabase")
            gstrDomainName = gloRegistrySetting.GetRegistryValue("Domain")
            gstrWindowsServerName = gloRegistrySetting.GetRegistryValue("WindowsServer")
        Else
            gstrArchiveDatabaseName = ""
            gstrDomainName = ""
            gstrWindowsServerName = ""

        End If
        ''Sandip Darade  20090808
        ''Read values below for PM admin for import fee schedule setting

        If (gstrAdminFor = "gloEMR") Then
            If (gblnSQLAuthentication = True) Then
                gblnWindowsAuthentication = False
            Else
                gblnWindowsAuthentication = True
            End If
        Else

            If gloRegistrySetting.GetRegistryValue("ISWINAUTHENTICATION") IsNot Nothing And gloRegistrySetting.GetRegistryValue("ISWINAUTHENTICATION") <> "" Then
                gblnWindowsAuthentication = gloRegistrySetting.GetRegistryValue("ISWINAUTHENTICATION")
            End If
        End If
        If (gblnWindowsAuthentication = False) Then
            'If regKey.GetValue("SQLUser") IsNot Nothing Then
            '    gstrSQLUser = regKey.GetValue("SQLUser")
            'End If
            If gloRegistrySetting.GetRegistryValue("SQLUSERNAME") IsNot Nothing Then
                gstrSQLUser = gloRegistrySetting.GetRegistryValue("SQLUSERNAME")
                gstrSQLUserEMR = gstrSQLUser ''to get connection string 
            End If

            If gloRegistrySetting.GetRegistryValue("SQLPassword") IsNot Nothing Then
                Dim oEncryption As New clsEncryption
                gstrSQLPassword = oEncryption.DecryptFromBase64String(Convert.ToString(gloRegistrySetting.GetRegistryValue("SQLPassword")), constEncryptDecryptKey_Services)
                gstrSQLPasswordEMR = gstrSQLPassword ''to get connection string 
            End If
            gblnSQLAuthentication = True
        End If
        ''Sandip Darade 20090717
        ''Test connection 

        If (gstrSQLServerName.Trim() = "" Or gstrDatabaseName.Trim() = "") Then
            MessageBox.Show("Please select valid SQL Server Name/Database", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim frmSettings As New frmStartupSettings
            Me.Hide()
            If frmSettings.ShowDialog() = DialogResult.OK Then
                Me.Visible = True
                pnlLogin.Visible = True
                picAnimation.Visible = False
            End If
        End If

        gloRegistrySetting.CloseRegistryKey()

        ReadVersion()



        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        '   Dim oDataReader As SqlDataReader 
        'gintNoOfAttempts
        Try
            _strSQL = "select sSettingsValue from Settings where sSettingsName = 'No. Of. Attempts'"
            conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            gintNoOfAttempts = cmd.ExecuteScalar

            conn.Close()
            ''Sandip Darade 20091117
            cmd.Dispose()
            conn.Dispose()
        Catch ex As Exception
            ' MsgBox(ex.Message)
            MessageBox.Show("Error while retrieving Settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try


        ''Added sServiceDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table   
        Try
            Dim dtService As DataTable
            dtService = gloAUSLibrary.clsGeneral.GetServicesDBCredentials(gloPMAdmin.mdlGeneral.GetConnectionString)
            If Not IsNothing(dtService) Then
                For Each dr As DataRow In dtService.Rows
                    Select Case dr("sSettingsName").ToString.ToUpper
                        Case "SERVICESAUTHEN"
                            gbServicesIsSQLAUTHEN = Convert.ToBoolean(dr("sSettingsValue"))
                        Case "SERVICESDATABASENAME"
                            gstrServicesDBName = Convert.ToString(dr("sSettingsValue"))
                        Case "SERVICESPASSWORD"
                            Dim objgloServicesDecryptions As New clsEncryption
                            If Not IsNothing(objgloServicesDecryptions) Then
                                gstrServicesPassWord = objgloServicesDecryptions.DecryptFromBase64String(Convert.ToString(dr("sSettingsValue")), mdlGeneral.constEncryptDecryptKey)
                                objgloServicesDecryptions = Nothing
                            End If
                            objgloServicesDecryptions = Nothing
                        Case "SERVICESSERVERNAME"
                            gstrServicesServerName = Convert.ToString(dr("sSettingsValue"))
                        Case "SERVICESUSERID"
                            gstrServicesUserID = Convert.ToString(dr("sSettingsValue"))
                    End Select
                Next
            End If
        Catch ex As Exception
            gstrServicesDBName = "gloServices"
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        ''Added sServiceDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table   

        If (gstrAdminFor = "gloEMR") Then
            If Trim(gstrSQLServerName) = "" Or Trim(gstrDatabaseName) = "" Or Trim(gstrDomainName) = "" Or Trim(gstrWindowsServerName) = "" Then
                Return False
            Else
                Return True
            End If
        Else
            If Trim(gstrSQLServerName) = "" Then
                Return False
            Else
                Return True
            End If
        End If

    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            End
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Retrieve gloEMR Version No
    Private Function RetrieveVersion() As String
        RetrieveVersion = "1.0.0"
    End Function



    'Procedure to validate the Login
    'Check User Name & Password is entered or not
    'Check User Name and Passwords are valid or not
    'Return True if User Name & Passwords are valid
    Private Function CheckLogin() As Boolean



        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Try
            Dim _strSQL As String = ""
            Dim oDataReader As SqlDataReader

            Dim numdigits, numletters, numspchars, numcapletters, numminlength, numdays As Integer
            Dim blnResetPwdFlag As Boolean
            Dim blnIsAdministrator As Boolean

            AccessFlag = False

            'Check User Name is entered or not
            If Trim(txtUserName.Text) = "" Then
                'User Name is not entered
                MessageBox.Show("User Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtUserName.Focus()
                AccessFlag = True
                Return False
            End If

            'Check Password is entered or not
            If Trim(txtPassword.Text) = "" Then
                'Password is not entered
                MessageBox.Show("Password must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPassword.Focus()
                AccessFlag = True
                Return False
            End If
            'User Name and password are enetered

            ''''''''''''''''''''''''''
            'Error log settings For PM            
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, True)
            If IsNothing(gloRegistrySetting.GetRegistryValue("EnableApplicationLogs")) = True Then
                gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", False)
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue("EnableErrorLogs")) = True Then
                gloRegistrySetting.SetRegistryValue("EnableErrorLogs", True)
            End If
            '----xxx----
            'Set gloTSprint variable value                            
            If gloRegistrySetting.GetRegistryValue("EnableLocalPrinter") IsNot Nothing Then
                If gloRegistrySetting.GetRegistryValue("EnableLocalPrinter").ToString() = "1" Then
                    gloGlobal.gloTSPrint.isCopyPrint = True
                Else
                    gloGlobal.gloTSPrint.isCopyPrint = False
                End If
            Else
                gloGlobal.gloTSPrint.isCopyPrint = False
            End If
            'get file type to be used for Claims printing - EMF / PDF
            If gloRegistrySetting.GetRegistryValue("UseEMFForClaims") IsNot Nothing Then
                If gloRegistrySetting.GetRegistryValue("UseEMFForClaims").ToString() = "1" Then
                    gloGlobal.gloTSPrint.UseEMFForClaims = True
                Else
                    gloGlobal.gloTSPrint.UseEMFForClaims = False
                End If
            Else
                gloGlobal.gloTSPrint.UseEMFForClaims = True
            End If

            gloGlobal.gloTSPrint.TempPath = gloSettings.FolderSettings.AppTempFolderPath
            '----xxx----
            conn.Open()

            _strSQL = "select nAdministrator from User_MST where sLoginName = '" & txtUserName.Text.Trim.Replace("'", "''") & "'"
            cmd = New SqlCommand(_strSQL, conn)
            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so chk for null value
                        If Not IsDBNull(oDataReader.Item("nAdministrator")) Then
                            'if not nulll then set the value of flag 
                            blnIsAdministrator = oDataReader.Item("nAdministrator")
                        Else
                            'if the value is null then set the flag to false
                            blnIsAdministrator = False
                        End If
                    End While
                End If
                oDataReader.Close()
            End If


            Dim objLogin As New clsLogin

            If blnIsAdministrator = False Then

                _strSQL = "select * from PwdSettings"
                cmd = New SqlCommand(_strSQL, conn)
                oDataReader = cmd.ExecuteReader

                If Not oDataReader Is Nothing Then
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            If Not IsDBNull(oDataReader.Item("ExpCapitalLetters")) Then
                                numcapletters = oDataReader.Item("ExpCapitalLetters")
                            Else
                                numcapletters = 0
                            End If
                            If Not IsDBNull(oDataReader.Item("ExpNoOfLetters")) Then
                                numletters = oDataReader.Item("ExpNoOfLetters")
                            Else
                                numletters = 0
                            End If
                            If Not IsDBNull(oDataReader.Item("ExpNoOfDigits")) Then
                                numdigits = oDataReader.Item("ExpNoOfDigits")
                            Else
                                numdigits = 0
                            End If
                            If Not IsDBNull(oDataReader.Item("ExpNoOfSpecChars")) Then
                                numspchars = oDataReader.Item("ExpNoOfSpecChars")
                            Else
                                numspchars = 0
                            End If
                            If Not IsDBNull(oDataReader.Item("ExpPwdLength")) Then
                                numminlength = oDataReader.Item("ExpPwdLength")
                            Else
                                numminlength = 0
                            End If
                            If Not IsDBNull(oDataReader.Item("ExpTimeFrameinDays")) Then
                                numdays = oDataReader.Item("ExpTimeFrameinDays")
                            Else
                                numdays = 0
                            End If
                        End While
                    End If
                    oDataReader.Close()
                End If

                '_strSQL = "select  IsPasswordReset from User_MST where sLoginName = '" & Trim(txtUserName.Text) & "'"
                'cmd = New SqlClient.SqlCommand(_strSQL, conn)

                ''  blnResetPwdFlag = cmd.ExecuteScalar

                'oDataReader = cmd.ExecuteReader

                'If Not oDataReader Is Nothing Then
                '    If oDataReader.HasRows = True Then
                '        While oDataReader.Read
                '            'the value can be NULL besides 0 and 1 , so chk for null value
                '            If Not IsDBNull(oDataReader.Item("IsPasswordReset")) Then
                '                'if not nulll then set the value of flag 
                '                blnResetPwdFlag = oDataReader.Item("IsPasswordReset")
                '            Else
                '                'if the value is null then set the flag to false
                '                blnResetPwdFlag = False
                '            End If
                '        End While
                '    End If
                '    oDataReader.Close()
                'End If

                If blnResetPwdFlag = False Then
                    If Not ValidatePassword(txtPassword.Text.Trim, numminlength, numcapletters, 0, numdigits, numspchars, Nothing, numletters, numdays) = True Then
                        MessageBox.Show("Invalid User Name/Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        txtUserName.Focus()
                        Return False
                    End If
                End If

                ' Check this Client machine has rights to access the gloEMR or not

                'sarika 14th sept 07
                If objLogin.IsClientAccess(gstrClientMachineName) = False Then
                    'Client machine does not have rights to access the gloEMR
                    MessageBox.Show("This machine does not have rights to access gloPM system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    objLogin = Nothing
                    End
                End If
                ' ---------------------------------------------------

            End If


            ''''''''''''''''''''''''''''

            'Encrypt the Password
            Dim objEncryption As New clsEncryption
            Dim strPassword As String
            strPassword = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
            objEncryption = Nothing

            'Check User Name and Passwords are valid or not
            If objLogin.IsValidLogin(txtUserName.Text, strPassword) = False Then
                'User Name or Password is not valid
                MessageBox.Show("Invalid User Name/Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Text = ""
                txtUserName.Focus()
                objLogin = Nothing

                Return False
            End If

            'Check the User is blocked or not
            If objLogin.IsAccessPermission(txtUserName.Text) = False Then
                'User is blocked
                MessageBox.Show("Access Denied." & vbCrLf & txtUserName.Text & " Please contact the administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUserName.Focus()
                objLogin = Nothing
                AccessFlag = True
                Return False
            End If
            'Check User has rights to access the gloEMR Admin or not
            If objLogin.IsAccessPermission(txtUserName.Text, True) = False Then

                MessageBox.Show("Access Denied." & vbCrLf & txtUserName.Text & " user has not rights to access gloPM Admin application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUserName.Focus()
                objLogin = Nothing
                AccessFlag = True
                Return False
            End If
            objLogin = Nothing
            'User is valid and has rights to access the gloEMR Admin
            Return True
        Catch ex As Exception

        Finally
            ''Sandip Darade 20091117
            conn.Close()
            cmd.Dispose()
            conn.Dispose()
        End Try
    End Function

    Private Sub ShowDateStamp()
        Try
            Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
            ' lblDate.Text = "Last Modified Date " & File.GetLastWriteTime(Application.StartupPath & "\" & aModuleName)
            ''Sandip Darade 20090819
            ''show last modified datae of the application
            'Dim strDate As String = Format(File.GetLastWriteTime(Application.StartupPath & "\" & aModuleName), "dd MMM, yyyy") & vbCrLf & Format(File.GetLastWriteTime(Application.StartupPath & "\" & aModuleName), "hh:mm:ss tt")
            'lblDate.Text = "Last Modified Date " & strDate

            Dim strDate As String = Format(File.GetLastWriteTime(Application.StartupPath & "\" & aModuleName), "MMM dd, yyyy")
            lblDate.Text = strDate

        Catch ex As Exception

        End Try

    End Sub


    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        If cmbDatabaseName.Visible Then
            If cmbDatabaseName.Text <> "" And dtDatabase.Rows.Count > 0 Then
                For iRow As Integer = 0 To dtDatabase.Rows.Count - 1
                    If dtDatabase.Rows(iRow)("nDBConnectionId") = cmbDatabaseName.SelectedValue Then
                        gstrDatabaseName = dtDatabase.Rows(iRow)("sDatabaseName")
                        gstrSQLServerName = dtDatabase.Rows(iRow)("sServerName")

                        gblnSQLAuthentication = True
                        gstrSQLUserEMR = dtDatabase.Rows(iRow)("sSqlUserName")
                        Dim oEncryption As New clsEncryption
                        gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(dtDatabase.Rows(iRow)("sSqlPassword"), constEncryptDecryptKey)
                        oEncryption = Nothing
                        Dim oDBLayer As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
                        ''Dhruv 20091210
                        ''to check the connection has been done or not.
                        If oDBLayer.CheckConnection() = False Then
                            'MessageBox.Show("Problem in connecting in the connection Credential,Connect with another Database", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            MessageBox.Show("Selected database/SQL server is not valid", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            oDBLayer.Dispose()
                            Exit Sub
                        End If



                        ''to check the connection has been done or not.
                        'Dim gloPMVersion As System.Diagnostics.FileVersionInfo = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() & "\gloPMAdmin.exe")

                        Dim dtversion As DataTable = Nothing
                        Dim oSetting As New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString())
                        dtversion = oSetting.GetSetting("DB Version", 0)
                        Dim _DBVersion As [String] = ""
                        If dtversion IsNot Nothing AndAlso dtversion.Rows.Count > 0 Then
                            _DBVersion = dtversion.Rows(0)("sSettingsValue").ToString()
                        End If
                        If dtversion IsNot Nothing Then
                            dtversion.Dispose()
                        End If

                        'If gloPMVersion.ProductVersion <> _DBVersion Then
                        If Application.ProductVersion <> _DBVersion Then
                            MessageBox.Show("Application version mismatch, Please contact your administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                            oDBLayer.Dispose()
                            oDBLayer = Nothing
                            Return
                        End If



                        ''--------------------------------------------------------------
                        ''Code below commented by Sandip Darade 20100120
                        ''no need to to check rights to access  

                        'Dim objLogin As New clsLogin
                        'If objLogin.IsClientAccess(gstrClientMachineName) = False Then
                        '    MessageBox.Show("This machine does not have rights to access gloEMR system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    objLogin = Nothing
                        '    Exit Sub
                        'End If

                        Exit For
                    End If
                Next
            End If
        End If


        If gstrDatabaseName = "" Or gstrSQLServerName = "" Then
            Exit Sub
        End If






        '***************
        ''Sandip Darade 20090724
        ''Add valuses to appsetting
        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
        gstrConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString
        appSettings("SQLServerName") = gstrSQLServerName
        appSettings("DatabaseName") = gstrDatabaseName
        ''appSettings("SQLLoginName") = gstrLoginName
        appSettings("SQLLoginName") = gstrSQLUser
        appSettings("SQLPassword") = gstrSQLPassword
        '      appSettings["WindowAuthentication"] =gb
        appSettings("DataBaseConnectionString") = gstrConnectionString
        appSettings("MessageBOXCaption") = gstrMessageBoxCaption
        appSettings("ClinicID") = 1
        appSettings("UserName") = txtUserName.Text

        Dim oSettings As New gloSettings.GeneralSettings(gloPMAdmin.mdlGeneral.GetConnectionString())
        Dim value As Object = Nothing
        oSettings = New gloSettings.GeneralSettings(gstrConnectionString)
        oSettings.GetSetting("Country", value)

        If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
            appSettings("Country") = value.ToString()
        Else
            appSettings("Country") = "US"
        End If
        value = Nothing



        gloGlobal.gloPMGlobal.ClinicID = gnClinicID
        gloGlobal.gloPMGlobal.DatabaseConnectionString = gstrConnectionString
        gloGlobal.gloPMGlobal.UserID = gnLoginID
        gloGlobal.gloPMGlobal.UserName = gstrLoginName
        gloGlobal.gloPMGlobal.LoginProviderID = 0
        gloGlobal.gloPMGlobal.MessageBoxCaption = gstrMessageBoxCaption

        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim blnResetPwdFlag As Boolean = False
        Dim _strSQL As String = ""
        '*********************
        _strSQL = "select nUserID,nAdministrator from User_Mst where sLoginName ='" & txtUserName.Text.Trim.Replace("'", "''") & "'"
        conn.Open()
        cmd = New SqlCommand(_strSQL, conn)
        oDataReader = cmd.ExecuteReader

        If Not oDataReader Is Nothing Then
            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    If Not IsDBNull(oDataReader.Item("nAdministrator")) Then
                        'if not nulll then set the value of flag 
                        gblnAdmin = oDataReader.Item("nAdministrator")
                    Else
                        'if the value is null then set the flag to false
                        gblnAdmin = False
                    End If
                    If Not IsDBNull(oDataReader.Item("nUserID")) Then
                        'if not nulll then set the value of flag 
                        gnLoginID = oDataReader.Item("nUserID")
                    Else
                        'if the value is null then set the flag to false
                        gnLoginID = 0
                    End If
                End While
                'the value can be NULL besides 0 and 1 , so chk for null value
            End If
            oDataReader.Close()
        End If
        conn.Close()
        appSettings("UserID") = gnLoginID
        ''Sandip Darade 20091117
        cmd.Dispose()
        conn.Dispose()
        If (txtPassword.Text.Trim() = "" And txtUserName.Text.Trim() = "") Then
            MessageBox.Show("Please enter User Name and Password.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword.Focus()
            Exit Sub
        ElseIf (txtPassword.Text.Trim() = "") Then
            MessageBox.Show("Please enter a Password.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtPassword.Focus()
            Exit Sub
        ElseIf (txtUserName.Text.Trim() = "") Then
            MessageBox.Show("Please enter an User Name .  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUserName.Focus()
            Exit Sub

        Else
            If gblnAdmin = False Then

                'sarika 19th july 08
                'Bug 859

                '//the user is not an administrator so, he should not be allowed to login to the Admin system 
                '//though he enters valid credentials as only administrator users are allowed to login to the admin module

                MessageBox.Show("Access denied. User " & txtUserName.Text & " have no rights to access gloPM admin application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Clear()
                txtUserName.Focus()
                Exit Sub

                '----------------------
            End If
        End If


        Try
            'sarika 14th feb --------
            If gstrLoginName <> Trim(txtUserName.Text) Then
                gstrLoginName = Trim(txtUserName.Text)
                nNoOfLoginAttempt = 0
            End If
            '-------------------------

            gstrLoginPassword = txtPassword.Text

            Dim objAudit As New clsAudit

            If CheckLogin() = True Then
                'Vinayak - 12 Sep 2007 - For Installation
                Me.Hide()
                'Dim oInstallServer As New frmServerInstallation
                'oInstallServer.ShowDialog()

                'sarika 21st feb
                gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(gstrLoginName, False, "", gloAuditTrail.SoftwareComponent.gloPM.ToString(), 1)
                objAudit.CreateLog(clsAudit.enmActivityType.Login, txtUserName.Text & " has successfully logged in.", gstrLoginName, gstrClientMachineName)

                '----------
                gstrLoginTime = CType(Format(Date.Now, "Medium Time"), String)
                gloGlobal.gloPMGlobal.UserID = gnLoginID
                gloGlobal.gloPMGlobal.UserName = gstrLoginName
                Dim oClinic As New clsClinic
                gstrClinicExternalCode = oClinic.GetAUSUserName()
                oClinic = Nothing
                Dim frmgloEMRMain As New frmgloEMRAdmin
                ' Me.Hide()
                frmgloEMRMain.ShowDialog()

            Else
                objAudit.CreateLog(clsAudit.enmActivityType.NodeAuthenticationFailure, "Login Attempt failed due to incorrect credentials.", gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)

                If AccessFlag = False Then
                    If gblnAdmin = False Then
                        nNoOfLoginAttempt = nNoOfLoginAttempt + 1

                        If nNoOfLoginAttempt >= gintNoOfAttempts Then
                            'sarika Remove Restrict Access form
                            'Me.Hide()
                            'Dim frmNoAccess As New frmRestrictAccess
                            'sarika Remove Restrict Access form
                            If SetAccessDeniedFlag() = True Then
                                objAudit.CreateLog(clsAudit.enmActivityType.SecurityAdmin, "User's access to gloPM admin has been denied since, he could not enter correct credentials and exceeded the number of LockOut attempts. ", gstrLoginName, gstrClientMachineName)
                                MessageBox.Show("Your access to gloPM system is denied . Please Contact the administrator.", "gloPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            'sarika Remove Restrict Access form
                            'frmNoAccess.ShowDialog()

                            End
                            'sarika Remove Restrict Access form
                        End If
                    End If
                End If
            End If
            objAudit = Nothing

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function SetAccessDeniedFlag() As Boolean

        'sets the AccessDenied flag to true for the user
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        '  Dim oDataReader As SqlDataReader
        Dim _strSQL As String = ""

        Try
            conn.Open()

            _strSQL = "update User_MST set nAccessDenied = 1 where sLoginName ='" & txtUserName.Text.Trim.Replace("'", "''") & "'"
            cmd = New SqlCommand(_strSQL, conn)

            cmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return False
        Finally
            conn.Close()
        End Try
    End Function

    Function ValidatePassword(ByVal pwd As String, _
               Optional ByVal minLength As Integer = 8, _
               Optional ByVal numUpper As Integer = 0, _
               Optional ByVal numLower As Integer = 0, _
               Optional ByVal numNumbers As Integer = 1, _
               Optional ByVal numSpecial As Integer = 0, _
               Optional ByVal resStrs() As String = Nothing, _
               Optional ByVal numLetters As Integer = 1, _
               Optional ByVal numofdays As Integer = 0) As Boolean

        Try

            ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
            Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
            Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
            Dim letters As New System.Text.RegularExpressions.Regex("[a-zA-Z]")
            Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
            ' Special is "none of the above".
            Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")


            ' Check the length.
            If Len(pwd) < minLength Then
                '  MsgBox("The  length of the password  should be atleast  " & minLength)
                ' txtPassword.Text = ""
                Return False
            End If

            ' Check for minimum number of occurrences.
            If upper.Matches(pwd).Count < numUpper Then
                '  MsgBox("The password should contain atleast " & numUpper & " upper case letter")
                '  txtPassword.Text = ""
                Return False
            End If


            If lower.Matches(pwd).Count < numLower Then
                '  MsgBox("The password should contain atleast " & numLower & " lower case letter")
                ' txtPassword.Text = ""
                Return False
            End If

            If number.Matches(pwd).Count < numNumbers Then
                '  MsgBox("The password should contain atleast " & numNumbers & " digits")
                '  txtPassword.Text = ""
                Return False
            End If

            If special.Matches(pwd).Count < numSpecial Then
                '  MsgBox("The password should contain atleast " & numSpecial & " special characters")
                Return False
            End If

            'If InStr(UCase(pwd), UCase(txtUserName.Text.Trim)) Then
            '    MsgBox("The password should not contain your login name")
            '    Return False
            'End If

            If UCase(pwd) = UCase(gstrLoginName) Then
                ' MsgBox("The password should not same as  your login name")
                Return False
            End If

            If letters.Matches(pwd).Count < numLetters Then
                ' MsgBox("The password should contain atleast " & numSpecial & " alphabet")
                Return False
            End If

            ''Check whether the pwd is one of the recent pwds
            'If GetRecentPwds(pwd) Then
            '    MsgBox("You have already used this password recently , so select another password")
            '    Return False
            'End If

            ' Passed all checks.
            Return True

        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally

        End Try
    End Function
    ''Sandip Darade  20090420
    ''Read  Application version
    Private Sub ReadVersion()
        Try


            Dim conn As New SqlConnection
            Dim cmd As New SqlCommand
            ''Dim _strSQL As String = "SELECT ISNULL(sSettingsValue,'') AS  sSettingsValue FROM Settings WHERE sSettingsName = 'Application Version'"
            'Dim _strSQL As String = "SELECT ISNULL(sSettingsValue,'') AS  sSettingsValue FROM Settings WHERE sSettingsName = 'gloPMAdminVersion'"
            'conn.ConnectionString = (gloPMAdmin.mdlGeneral.GetConnectionString)
            'conn.Open()

            'cmd = New SqlCommand(_strSQL, conn)
            '_strSQL = cmd.ExecuteScalar
            'If _strSQL <> "" Then
            '    gstrVersion = _strSQL          
            'End If

            'If gstrVersion <> "" Then
            '    lblVersion.Text = gstrVersion
            'End If
            lblVersion.Text = gloGlobal.clsMISC.GetMajorVersion(Application.ProductVersion)


            '     conn.Close()

            '  cmd.Dispose()
            '   conn.Dispose()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
        ''End Read version

    End Sub

    Private Sub lblVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblVersion.Click

    End Sub

    Private Sub FillServiceDatabases()
        ''Local variable declaration
        Dim con As SqlConnection = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim sqlCommand As SqlCommand = Nothing

        Try
            con = New SqlConnection()
            con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)

            If Not IsNothing(con) Then

                Dim _sqlParameter As SqlParameter = Nothing

                sqlCommand = New SqlCommand()
                sqlCommand.Connection = con
                sqlCommand.CommandTimeout = 0
                sqlCommand.CommandType = CommandType.StoredProcedure
                sqlCommand.CommandText = "gsp_GetMultipleDatabases"

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@ServiceName"
                _sqlParameter.Value = "gloEMR"
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.VarChar
                _sqlParameter.Size = 150

                sqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@MachineName"
                _sqlParameter.Value = gloAuditTrail.MachineDetails.LocalMachineDetails().MachineName
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.VarChar
                _sqlParameter.Size = 150

                sqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                da = New SqlDataAdapter(sqlCommand)

                da.Fill(dtDatabase)

                If Not IsNothing(dtDatabase) Then
                    If dtDatabase.Rows.Count > 0 Then
                        cmbDatabaseName.DataSource = dtDatabase
                        cmbDatabaseName.DisplayMember = "sDatabaseName"
                        cmbDatabaseName.ValueMember = "nDBConnectionId"
                        cmbDatabaseName.SelectedItem = Nothing

                        cmbDatabaseName.Visible = True
                        pnlDataBase.Visible = True
                        pnlUser.Visible = True
                        pnlLoginButton.Visible = True
                        lblDataBase.Visible = True

                        '' CHECK FOR DEFAULT DATABASE IN COMBO LIST ''
                        For iRow As Integer = 0 To dtDatabase.Rows.Count - 1
                            If Convert.ToBoolean(dtDatabase.Rows(iRow)("bEnabled")) = True Then
                                cmbDatabaseName.Text = dtDatabase.Rows(iRow)("sDatabaseName").ToString()
                                Exit For
                            End If
                        Next
                    Else
                        cmbDatabaseName.Visible = False
                        pnlDataBase.Visible = False
                        pnlUser.Visible = True
                        pnlLoginButton.Visible = True
                        lblDataBase.Visible = False
                    End If
                Else
                    cmbDatabaseName.Visible = False
                    pnlDataBase.Visible = False
                    pnlUser.Visible = True
                    pnlLoginButton.Visible = True
                    lblDataBase.Visible = False
                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
            cmbDatabaseName.Visible = False
            pnlDataBase.Visible = False
            lblDataBase.Visible = False
        Finally

            If Not IsNothing(sqlCommand) Then
                If Not IsNothing(sqlCommand.Parameters) Then
                    sqlCommand.Parameters.Clear()
                End If
                sqlCommand.Dispose()
                sqlCommand = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If

        End Try
    End Sub

    Private Sub label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label1.Click

    End Sub

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '' ===========Make our bitmap region for the form ========================
        ' BitmapRegion.CreateControlRegion(Me, bmpFrmBack)

        ''Commented by Ujwala on 23022015 to resolve issue with Hardcoded gloServices DB Name
        ' ''cmbDatabaseName.Visible = False
        ' ''pnlDataBase.Visible = False
        ' ''pnlUser.Visible = False
        ' ''pnlLoginButton.Visible = False
        ' ''lblDataBase.Visible = False
        ''Commented by Ujwala on 23022015 to resolve issue with Hardcoded gloServices DB Name
        'Me.Height = 605
        'Me.Width = 837
    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub btnLogin_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnLogin.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloPMAdmin.My.Resources.Img_OrangeBtnHoverSplashScreen()
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Private Sub btnLogin_MouseLeave(sender As Object, e As System.EventArgs) Handles btnLogin.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloPMAdmin.My.Resources.Img_OrangeBtnSplashScreen
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Private Sub btnCancel_MouseHover(sender As Object, e As System.EventArgs) Handles btnCancel.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloPMAdmin.My.Resources.Img_BlueBtnHoverSplashScreen
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Private Sub btnCancel_MouseLeave(sender As Object, e As System.EventArgs) Handles btnCancel.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloPMAdmin.My.Resources.Img_BlueBtnSplashScreen
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub
    '=================Add for TaskBar Minimized ======================
    'Private Sub frmSplash_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize

    '    If Me.WindowState = FormWindowState.Minimized Then
    '        BitmapRegion.CreateControlRegion(Me, bmpFrmWhite)
    '    Else
    '        If Me.WindowState = FormWindowState.Maximized Then
    '            BitmapRegion.CreateControlRegion(Me, bmpFrmBack)
    '        End If
    '    End If

    'End Sub

    'Private Sub frmSplash_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
    '    If Me.WindowState = FormWindowState.Minimized Then
    '        BitmapRegion.CreateControlRegion(Me, bmpFrmWhite)
    '    Else
    '        If Me.WindowState = FormWindowState.Maximized Then
    '            BitmapRegion.CreateControlRegion(Me, bmpFrmBack)
    '        End If
    '    End If

    '    '============End for TaskBar Minimized =========================

    'End Sub
End Class
