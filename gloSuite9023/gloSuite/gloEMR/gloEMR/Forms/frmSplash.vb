Imports Microsoft.Win32
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Data.SqlClient
Imports System.Configuration
Imports gloSettings
Imports gloEMR.Help
Imports gloRemoteScanGeneral
Imports System.Threading

Public Class frmSplash
    '' Inherits System.Windows.Forms.Form
    Inherits gloAUSLibrary.MasterForm

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'The 2 event handlers 

    End Sub


#Region " TO Check the Multiple instances Of Form "

    'TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lbl_mktngversion As System.Windows.Forms.Label
    Friend WithEvents lblLoginBaner As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblCopyrghTag As System.Windows.Forms.Label
    Private WithEvents pnlLogin As System.Windows.Forms.Panel
    Private WithEvents pnlDataBase As System.Windows.Forms.Panel
    Friend WithEvents cmbDatabaseName As System.Windows.Forms.ComboBox
    Friend WithEvents lblDataBase As System.Windows.Forms.Label
    Private WithEvents pnlLoginButton As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents pnlUser As System.Windows.Forms.Panel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblPleaseWait As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents picAnimation As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private Shared frm As frmSplash

    'Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                ' Dispose managed resources.
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        frm = Nothing
        Me.blnDisposed = True

    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance() As frmSplash
        Try
            If frm Is Nothing Then
                frm = New frmSplash
            End If

        Finally
        End Try
        Return frm
    End Function

#End Region

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblLastModifiedDate As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblCopyrghTag = New System.Windows.Forms.Label()
        Me.pnlLogin = New System.Windows.Forms.Panel()
        Me.pnlUser = New System.Windows.Forms.Panel()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlDataBase = New System.Windows.Forms.Panel()
        Me.cmbDatabaseName = New System.Windows.Forms.ComboBox()
        Me.lblDataBase = New System.Windows.Forms.Label()
        Me.pnlLoginButton = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblLoginBaner = New System.Windows.Forms.Label()
        Me.lblLastModifiedDate = New System.Windows.Forms.Label()
        Me.lbl_mktngversion = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lblPleaseWait = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.picAnimation = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel2.SuspendLayout()
        Me.pnlLogin.SuspendLayout()
        Me.pnlUser.SuspendLayout()
        Me.pnlDataBase.SuspendLayout()
        Me.pnlLoginButton.SuspendLayout()
        CType(Me.picAnimation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Arial", 5.0!)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 7)
        Me.Label3.TabIndex = 18
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.Label3.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.lblCopyrghTag)
        Me.Panel2.Location = New System.Drawing.Point(24, 520)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(800, 74)
        Me.Panel2.TabIndex = 25
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.05!)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(0, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(800, 72)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'lblCopyrghTag
        '
        Me.lblCopyrghTag.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyrghTag.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCopyrghTag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCopyrghTag.Font = New System.Drawing.Font("Arial", 8.05!)
        Me.lblCopyrghTag.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.lblCopyrghTag.Location = New System.Drawing.Point(0, 0)
        Me.lblCopyrghTag.Name = "lblCopyrghTag"
        Me.lblCopyrghTag.Size = New System.Drawing.Size(800, 15)
        Me.lblCopyrghTag.TabIndex = 8
        Me.lblCopyrghTag.Text = "CPT® copyright 2015 American Medical Association. All rights reserved."
        '
        'pnlLogin
        '
        Me.pnlLogin.BackColor = System.Drawing.Color.Transparent
        Me.pnlLogin.Controls.Add(Me.pnlUser)
        Me.pnlLogin.Controls.Add(Me.pnlDataBase)
        Me.pnlLogin.Controls.Add(Me.pnlLoginButton)
        Me.pnlLogin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLogin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(170, Byte), Integer))
        Me.pnlLogin.Location = New System.Drawing.Point(566, 20)
        Me.pnlLogin.Name = "pnlLogin"
        Me.pnlLogin.Size = New System.Drawing.Size(221, 133)
        Me.pnlLogin.TabIndex = 24
        '
        'pnlUser
        '
        Me.pnlUser.BackColor = System.Drawing.Color.Transparent
        Me.pnlUser.Controls.Add(Me.txtPassword)
        Me.pnlUser.Controls.Add(Me.Label1)
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
        Me.txtPassword.Location = New System.Drawing.Point(74, 39)
        Me.txtPassword.MaxLength = 100
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(144, 20)
        Me.txtPassword.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(-1, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 12)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "PASSWORD:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUserName
        '
        Me.txtUserName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserName.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(74, 8)
        Me.txtUserName.MaxLength = 50
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(144, 20)
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
        Me.Label4.Location = New System.Drawing.Point(1, 11)
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
        Me.cmbDatabaseName.Location = New System.Drawing.Point(74, 2)
        Me.cmbDatabaseName.Name = "cmbDatabaseName"
        Me.cmbDatabaseName.Size = New System.Drawing.Size(144, 22)
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
        Me.lblDataBase.Location = New System.Drawing.Point(3, 7)
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
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_BlueBtnSplashScreen
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(118, 2)
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
        Me.btnLogin.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_OrangeBtnSplashScreen
        Me.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(203, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(6, 2)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(100, 23)
        Me.btnLogin.TabIndex = 3
        Me.btnLogin.Text = "&Login"
        Me.btnLogin.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 7.95!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label6.Image = CType(resources.GetObject("Label6.Image"), System.Drawing.Image)
        Me.Label6.Location = New System.Drawing.Point(609, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(132, 41)
        Me.Label6.TabIndex = 163
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLoginBaner
        '
        Me.lblLoginBaner.AutoSize = True
        Me.lblLoginBaner.BackColor = System.Drawing.Color.Transparent
        Me.lblLoginBaner.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoginBaner.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblLoginBaner.Location = New System.Drawing.Point(0, 0)
        Me.lblLoginBaner.Name = "lblLoginBaner"
        Me.lblLoginBaner.Size = New System.Drawing.Size(0, 16)
        Me.lblLoginBaner.TabIndex = 14
        '
        'lblLastModifiedDate
        '
        Me.lblLastModifiedDate.AutoSize = True
        Me.lblLastModifiedDate.BackColor = System.Drawing.Color.Transparent
        Me.lblLastModifiedDate.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.lblLastModifiedDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(174, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.lblLastModifiedDate.Location = New System.Drawing.Point(23, 502)
        Me.lblLastModifiedDate.Name = "lblLastModifiedDate"
        Me.lblLastModifiedDate.Size = New System.Drawing.Size(70, 14)
        Me.lblLastModifiedDate.TabIndex = 5
        Me.lblLastModifiedDate.Text = "Mar 04, 2016"
        '
        'lbl_mktngversion
        '
        Me.lbl_mktngversion.AutoSize = True
        Me.lbl_mktngversion.BackColor = System.Drawing.Color.Transparent
        Me.lbl_mktngversion.Font = New System.Drawing.Font("Arial", 21.05!, System.Drawing.FontStyle.Bold)
        Me.lbl_mktngversion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lbl_mktngversion.Location = New System.Drawing.Point(0, 0)
        Me.lbl_mktngversion.Name = "lbl_mktngversion"
        Me.lbl_mktngversion.Size = New System.Drawing.Size(0, 34)
        Me.lbl_mktngversion.TabIndex = 13
        Me.lbl_mktngversion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_mktngversion.Visible = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(0, -26)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(53, 16)
        Me.lblVersion.TabIndex = 7
        Me.lblVersion.Text = "Version "
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblVersion.Visible = False
        '
        'Timer1
        '
        '
        'lblPleaseWait
        '
        Me.lblPleaseWait.AutoSize = True
        Me.lblPleaseWait.BackColor = System.Drawing.Color.Transparent
        Me.lblPleaseWait.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPleaseWait.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.lblPleaseWait.Location = New System.Drawing.Point(608, 145)
        Me.lblPleaseWait.Name = "lblPleaseWait"
        Me.lblPleaseWait.Size = New System.Drawing.Size(135, 14)
        Me.lblPleaseWait.TabIndex = 28
        Me.lblPleaseWait.Text = "Loading. . .  Please Wait"
        Me.lblPleaseWait.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 7.95!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label9.Image = CType(resources.GetObject("Label9.Image"), System.Drawing.Image)
        Me.Label9.Location = New System.Drawing.Point(691, 427)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 33)
        Me.Label9.TabIndex = 158
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label9.Visible = False
        '
        'picAnimation
        '
        Me.picAnimation.Location = New System.Drawing.Point(286, 464)
        Me.picAnimation.Name = "picAnimation"
        Me.picAnimation.Size = New System.Drawing.Size(100, 50)
        Me.picAnimation.TabIndex = 159
        Me.picAnimation.TabStop = False
        Me.picAnimation.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(670, 439)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(15, 21)
        Me.PictureBox1.TabIndex = 160
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
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
        Me.Controls.Add(Me.lblPleaseWait)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.picAnimation)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lbl_mktngversion)
        Me.Controls.Add(Me.lblLoginBaner)
        Me.Controls.Add(Me.lblLastModifiedDate)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlLogin)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gloEMR"
        Me.Panel2.ResumeLayout(False)
        Me.pnlLogin.ResumeLayout(False)
        Me.pnlUser.ResumeLayout(False)
        Me.pnlUser.PerformLayout()
        Me.pnlDataBase.ResumeLayout(False)
        Me.pnlDataBase.PerformLayout()
        Me.pnlLoginButton.ResumeLayout(False)
        CType(Me.picAnimation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim nNoOfLoginAttempt As Integer
    Dim blnResetPwdFlag As Boolean

    Dim AccessFlag As Boolean

    Dim strVersion As String
    Dim strDate As String
    ''dhruv 20091202 Hardcorded the Service DatabaseName

    ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table instead of Hardcoding
    '' Const sServiceDatabaseName = "gloServices"
    ' Dim sServiceDatabaseName As String = gstrServicesDBName
    ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table instead of Hardcoding

    ''======================= Load our bitmaps
    'Private bmpFrmBack As New Bitmap(gloGlobal.Properties.Resources.SplashTransperentFront)
    'Private bmpFrmWhite As New Bitmap(gloGlobal.Properties.Resources.whitebitmap)

    'Private bAfterLoad As Boolean = False
    ''===========================================

    Const strMessageBoxCaption = "gloEMR"
    Private dtDatabase As New DataTable
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings '' Kanchan 20100102 '' to add applictation settings for gloPM ''
    Dim dvSettings As DataView
    'Public Property dv_Settings As DataView
    Public Property LogOffFired As Boolean
    Private Function IsSettings() As Boolean

        Dim _IsValidDatabase As Boolean = False

        '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
        'Dim objGlobalMisc As New gloGlobal.clsMISC

        UpdateLog("IsSettings() : Check for gloEMR Reg START")

        If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
            Return False
        End If
        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        UpdateLog("IsSettings() : Check for gloEMR Reg END")





        If IsNothing(gloRegistrySetting.GetRegistryValue("HelpProvider")) = True Then
            gloRegistrySetting.SetRegistryValue("HelpProvider", "Client")
            gloEMR.Help.HelpComponent.blnbuildmode = False
            gstrHelpProvider = "Client"

        Else
            gstrHelpProvider = gloRegistrySetting.GetRegistryValue("HelpProvider")
            If gstrHelpProvider = "Client" Then
                gloEMR.Help.HelpComponent.blnbuildmode = False

            Else
                gloEMR.Help.HelpComponent.blnbuildmode = True


            End If


        End If







        If IsNothing(gloRegistrySetting.GetRegistryValue("SQLServer")) = True Then
            gloRegistrySetting.CloseRegistryKey()
            Return False
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("Database")) = True Then
            _IsValidDatabase = True
            gloRegistrySetting.CloseRegistryKey()
            Return False
        Else
            _IsValidDatabase = False
        End If

        gstrSQLServerName = gloRegistrySetting.GetRegistryValue("SQLServer")
        gloEmdeonCommon.mdlGeneral.gstrSQLServerName = gloRegistrySetting.GetRegistryValue("SQLServer")

        gstrDatabaseName = gloRegistrySetting.GetRegistryValue("Database")

        If gloRegistrySetting.GetRegistryValue("IsSQLAuthentication") IsNot Nothing Then
            gblnSQLAuthentication = gloRegistrySetting.GetRegistryValue("IsSQLAuthentication")
            gloEmdeonCommon.mdlGeneral.gblnSQLAuthentication = gloRegistrySetting.GetRegistryValue("IsSQLAuthentication")

            If gblnSQLAuthentication = True Then '''' this is used in gloSurescriptGeneral.GetconnectionString()
                gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
            End If

        End If

        If gloRegistrySetting.GetRegistryValue("SQLUserEMR") IsNot Nothing Then
            gstrSQLUserEMR = gloRegistrySetting.GetRegistryValue("SQLUserEMR")
            gloEmdeonCommon.mdlGeneral.gstrSQLUserEMR = gloRegistrySetting.GetRegistryValue("SQLUserEMR")
        End If

        If gloRegistrySetting.GetRegistryValue("SQLPasswordEMR") IsNot Nothing Then
            If gloRegistrySetting.GetRegistryValue("SQLPasswordEMR") <> "" Then
                Dim oEncryption As New clsencryption
                gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(gloRegistrySetting.GetRegistryValue("SQLPasswordEMR"), constEncryptDecryptKey)
                gloEmdeonCommon.mdlGeneral.gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(gloRegistrySetting.GetRegistryValue("SQLPasswordEMR"), constEncryptDecryptKey)
                oEncryption = Nothing
            End If
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("ServerPath")) = False Then
            gstrServerPath = gloRegistrySetting.GetRegistryValue("ServerPath")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("MessageRefreshTime")) = False Then
            gMessageUpdateTime = gloRegistrySetting.GetRegistryValue("MessageRefreshTime")
        Else
            gMessageUpdateTime = 5
        End If

        gloRegistrySetting.CloseRegistryKey()



        UpdateLog("IsSettings() : Checking for No of attems for Login ")

        If _IsValidDatabase = True Then

            'Dim conn As New SqlConnection(GetConnectionString)
            'Dim cmd As SqlCommand
            'Dim _strSQL As String = ""

            Try
                '_strSQL = "select sSettingsValue from Settings where sSettingsName = 'No. Of. Attempts'"
                'conn.Open()
                'cmd = New SqlCommand(_strSQL, conn)
                '_strSQL = cmd.ExecuteScalar & ""

                'If IsNothing(_strSQL) = True Then
                '    _strSQL = ""
                'End If
                ''Code opti.
                dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'No. Of. Attempts'"
                If dvSettings.Count > 0 Then
                    Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()

                    If sValue = "" Then
                        gintNoOfAttempts = 3 '' Default No of Attempts are 3
                    Else
                        gintNoOfAttempts = sValue
                    End If
                Else
                    gintNoOfAttempts = 3 '' Default No of Attempts are 3
                End If

                ' conn.Close()
                UpdateLog("IsSettings() : Checking for No of attems for Login END")

                UpdateLog("IsSettings() : Fill Clinic START ")
                Call Fill_Clinic()

                UpdateLog("IsSettings() : Fill Clinic END")

            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dvSettings.RowFilter = Nothing
            End Try
            ReadVersion()
            ReadMarketingVersion()
        End If

        ReadVersion()
        GetBannerName()

        ''Added sServiceDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   
        GetgloServicesDBName()
        ''Added sServiceDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   

        '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
        lbl_mktngversion.Text = gloGlobal.clsMISC.GetMajorVersion(Application.ProductVersion)

        ' objGlobalMisc = Nothing

        If gstrSQLServerName = "" Or gstrDatabaseName = "" Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub GetBannerName()
        Try
            'Dim conn As New SqlConnection
            'Dim cmd As New SqlCommand
            'Dim _strSQL As String = ""
            '_strSQL = "select ISNULL(sSettingsValue,'') from Settings where UPPER(sSettingsName) = 'UNAUTHENTICATEDLOGINBANNER'"
            'conn.ConnectionString = GetConnectionString()
            'conn.Open()
            'cmd = New SqlCommand(_strSQL, conn)
            '_strSQL = cmd.ExecuteScalar & ""
            'If _strSQL <> "" Then
            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'UNAUTHENTICATEDLOGINBANNER'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue <> "" Then
                    gstrBannerName = sValue
                Else
                    gstrBannerName = ""
                End If
            Else
                gstrBannerName = ""
            End If
            lblLoginBaner.Text = gstrBannerName
            dvSettings.RowFilter = Nothing
            '  conn.Close()
        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub GetgloServicesDBName()
        ''Added sServiceDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   

        'If (IsNothing(dvSettings) = False) Then
        '    dvSettings.Dispose()
        '    dvSettings = Nothing
        'End If
        'dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView

        'dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'SERVICESDATABASENAME'"
        'If dvSettings.Count > 0 Then
        '    Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
        '    If sValue <> "" Then
        '        gstrServicesDBName = sValue
        '        gloEmdeonCommon.mdlGeneral.gstrServicesDBName = sValue
        '    Else
        '        gstrServicesDBName = "gloServices"
        '        gloEmdeonCommon.mdlGeneral.gstrServicesDBName = "gloServices"
        '    End If
        'Else
        '    gstrServicesDBName = "gloServices"
        '    gloEmdeonCommon.mdlGeneral.gstrServicesDBName = "gloServices"
        'End If
        'dvSettings.RowFilter = Nothing


        Try
            Dim dtService As DataTable
            dtService = gloAUSLibrary.clsGeneral.GetServicesDBCredentials(GetConnectionString())
            If (dtService IsNot Nothing) Then
                For Each dr As DataRow In dtService.Rows
                    Select Case Convert.ToString(dr("sSettingsName")).ToUpper()
                        Case "SERVICESAUTHEN"
                            gbServicesIsSQLAUTHEN = Convert.ToBoolean(dr("sSettingsValue"))
                            gloEmdeonCommon.mdlGeneral.gbServicesIsSQLAUTHEN = Convert.ToBoolean(dr("sSettingsValue"))
                            Exit Select
                        Case "SERVICESDATABASENAME"
                            gstrServicesDBName = Convert.ToString(dr("sSettingsValue"))
                            gloEmdeonCommon.mdlGeneral.gstrServicesDBName = Convert.ToString(dr("sSettingsValue"))
                            Exit Select
                        Case "SERVICESPASSWORD"
                            Dim objgloServicesDecryptions As New clsencryption()
                            If (objgloServicesDecryptions IsNot Nothing) Then
                                gstrServicesPassWord = objgloServicesDecryptions.DecryptFromBase64String(Convert.ToString(dr("sSettingsValue")), constEncryptDecryptKey)
                                gloEmdeonCommon.mdlGeneral.gstrServicesPassWord = gstrServicesPassWord
                                objgloServicesDecryptions = Nothing
                            End If
                            objgloServicesDecryptions = Nothing
                            Exit Select
                        Case "SERVICESSERVERNAME"
                            gstrServicesServerName = Convert.ToString(dr("sSettingsValue"))
                            gloEmdeonCommon.mdlGeneral.gstrServicesServerName = Convert.ToString(dr("sSettingsValue"))
                            Exit Select
                        Case "SERVICESUSERID"
                            gstrServicesUserID = Convert.ToString(dr("sSettingsValue"))
                            gloEmdeonCommon.mdlGeneral.gstrServicesUserID = Convert.ToString(dr("sSettingsValue"))
                            Exit Select
                    End Select
                Next
                dtService.Dispose()
                dtService = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, strMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try

        ''Added sServiceDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   
    End Sub

    Private Sub DoesNetworkDirExist()

        gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist()

    End Sub

    Private Sub GetEMRSettings()
        UpdateLog("GetEMRSettings() : Read Reg Values START")

        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        If IsNothing(gloRegistrySetting.GetRegistryValue("DefaultPatientCode")) = True Then
            gloRegistrySetting.SetRegistryValue("DefaultPatientCode", "")
        End If

        gloEmdeonCommon.mdlGeneral.gblHL7SENDOUTBOUNDGLOEMRonLabModule = False
        gblnHL7SENDOUTBOUNDGLOEMR = False
        gblnAddModPatient = False
        gblnSendHL7Appointment = False
        gblnSendImmunization = False
        gblnSaveandClose = False
        gblnSaveandFinish = False
        gbInGENIUSSENDOUTBOUNDGLOEMR = False
        gbInGeniusSaveClose = False
        gbInGeniusSaveFinish = False
        gblnVisitSumSaveandClose = False
        gblnVisitSumSaveandFinish = False

        If appSettings("HL7SENDOUTBOUNDGLOEMR") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("HL7SENDOUTBOUNDGLOEMR"))) = True Then
                gblnHL7SENDOUTBOUNDGLOEMR = True
                gloEmdeonCommon.mdlGeneral.gblHL7SENDOUTBOUNDGLOEMRonLabModule = gblnHL7SENDOUTBOUNDGLOEMR
            End If
        End If


        If appSettings("SendPatientDetails") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("SendPatientDetails"))) = True Then
                gblnAddModPatient = True
            End If
        End If

        If appSettings("SendAppointmentDetails") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("SendAppointmentDetails"))) = True Then
                gblnSendHL7Appointment = True
            End If
        End If
        If appSettings("SendImmunizationDetails") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("SendImmunizationDetails"))) = True Then
                gblnSendImmunization = True
            End If
        End If

        If appSettings("SendOnSaveandClose") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("SendOnSaveandClose"))) = True Then
                gblnSaveandClose = True
            End If
        End If

        If appSettings("SendOnSaveandFinish") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("SendOnSaveandFinish"))) = True Then
                gblnSaveandFinish = True
            End If
        End If

        If appSettings("GENIUSSENDOUTBOUNDGLOEMR") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("GENIUSSENDOUTBOUNDGLOEMR"))) = True Then
                gbInGENIUSSENDOUTBOUNDGLOEMR = True
            End If
        End If

        If appSettings("GeniusSaveClose") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("GeniusSaveClose"))) = True Then
                gbInGeniusSaveClose = True
            End If
        End If

        If appSettings("GeniusSaveFinish") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("GeniusSaveFinish"))) = True Then
                gbInGeniusSaveFinish = True
            End If
        End If

        'Start of code added by manoj jadhav on 20140220 for MDM_T02 Message Queue genration
        If appSettings("bHL7_sendVisitSumSaveClose") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("bHL7_sendVisitSumSaveClose"))) = True Then
                gblnVisitSumSaveandClose = True
            End If
        End If

        If appSettings("bHL7_sendVisitSumSaveFinish") <> Nothing Then
            If Convert.ToBoolean(Val(appSettings("bHL7_sendVisitSumSaveFinish"))) = True Then
                gblnVisitSumSaveandFinish = True
            End If
        End If
        'end of code added by manoj jadhav on 20140220 for MDM_T02 Message Queue generation

        If IsNothing(gloRegistrySetting.GetRegistryValue("SurescriptFaxSetting")) = False Then
            gblnIsFaxEnabled = gloRegistrySetting.GetRegistryValue("SurescriptFaxSetting")
        Else ''''since there is no setting for SurescriptFaxSetting set the variable to false
            gblnIsFaxEnabled = False
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("SendChargesToGenius")) = False Then
            gblnSendChargesToGenius = gloRegistrySetting.GetRegistryValue("SendChargesToGenius")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("PatientSynopsisTabCount")) = False Then
            gnPatientSynopsisTabCount = gloRegistrySetting.GetRegistryValue("PatientSynopsisTabCount")
        Else
            gnPatientSynopsisTabCount = 1
        End If

        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try

        'MedHx 10.6 Settings
        Try
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " ='MEDHISTORY10DOT6ENABLE'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue"))
                If sValue = "0" Or sValue = "" Then
                    gblnMedHX10Dot6Enabled = False
                Else
                    gblnMedHX10Dot6Enabled = True
                End If
            End If

            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'DuplicateMedHxReq'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue"))

                If Int32.TryParse(sValue, mdlGeneral.MedHxRestriction) = False Then
                    MedHxRestriction = 0
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        'End MedHx 10.6 Settings

        'GLO2010-0006444 Search Category Change
        If IsNothing(gloRegistrySetting.GetRegistryValue("ResetSearch")) = False Then
            gblnResetSearchTextBox = gloRegistrySetting.GetRegistryValue("ResetSearch")
            gloUserControlLibrary.gloUC_TreeView.blnResetSearch = gblnResetSearchTextBox
        End If

        'Formulary Alternative setting for All Drugs
        If IsNothing(gloRegistrySetting.GetRegistryValue("FormularyAlertnativesOffFormularyDrgs")) = False Then
            gblnFormularyAlertnativesOffFormularyDrgs = gloRegistrySetting.GetRegistryValue("FormularyAlertnativesOffFormularyDrgs")
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_OffFormularyDrugs = gblnFormularyAlertnativesOffFormularyDrgs
        End If
        'Formulary Alternative setting for All Drugs

        'Formulary Alternative setting for Not Reimbursable Drugs
        If IsNothing(gloRegistrySetting.GetRegistryValue("FormularyAlertnativesNRDrgs")) = False Then
            gblnFormularyAlertnativesNRDrgs = gloRegistrySetting.GetRegistryValue("FormularyAlertnativesNRDrgs")
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_NRDrugs = gblnFormularyAlertnativesNRDrgs
        End If
        'Formulary Alternative setting for Not Reimbursable Drugs

        'Formulary Alternative setting for All Drugs
        If IsNothing(gloRegistrySetting.GetRegistryValue("FormularyAlertnativesAllDrgs")) = False Then
            gblnFormularyAlertnativesAllDrgs = gloRegistrySetting.GetRegistryValue("FormularyAlertnativesAllDrgs")
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyAlt_AllDrugs = gblnFormularyAlertnativesAllDrgs
        End If
        'Formulary Alternative setting for All Drugs

        ' Show NDC in  alternatives
        If IsNothing(gloRegistrySetting.GetRegistryValue(ShowNDCInAlternatives)) = False Then
            gblnShowNDCInAlternatives = gloRegistrySetting.GetRegistryValue(ShowNDCInAlternatives)
        End If

        ' Show Off formulary alternatives
        If IsNothing(gloRegistrySetting.GetRegistryValue("ShowOffFormularyAlternatives")) = False Then
            gblnShowOffformularyalternatives = gloRegistrySetting.GetRegistryValue("ShowOffFormularyAlternatives")
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnFormlyShowOFFFormulary_Alt = gblnShowOffformularyalternatives
        End If
        '---

        ' Show NDC In Medication History
        If IsNothing(gloRegistrySetting.GetRegistryValue("ShowNDCInMedicationHistory")) = False Then
            gblnShowNDCInMedicationHistory = gloRegistrySetting.GetRegistryValue("ShowNDCInMedicationHistory")
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnShowNDCInMedication_History = gblnShowNDCInMedicationHistory
        End If
        '------ -

        If IsNothing(gloRegistrySetting.GetRegistryValue("FAXPrinterName")) = False Then
            gstrFAXPrinterName = gloRegistrySetting.GetRegistryValue("FAXPrinterName")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("FAXOutputDirectory")) = False Then
            gstrFAXOutputDirectory = gloRegistrySetting.GetRegistryValue("FAXOutputDirectory")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("FAXReceivedDirectory")) = False Then
            gstrFAXReceivedDirectory = gloRegistrySetting.GetRegistryValue("FAXReceivedDirectory")
            If System.IO.Directory.Exists(gstrFAXReceivedDirectory) = False Then
                gstrFAXReceivedDirectory = ""
            End If
        End If

        '' CR00000126 : FAX for Terminal Server
        '' New setting added for ReceivedFaxFolder useful only for Terminal Server 
        UpdateLog("gstrFAXReceivedDirectoryTS : Start ")
        If (gloSettings.gloRegistrySetting.IsServerOS) Then
            Try
                '' To get the default setting for Fax Download Directory (admin setting) and add to registry if not present.
                Dim receivedFaxDirectory As String = String.Empty
                dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'Fax Receive Path For DMS'"
                If dvSettings.Count > 0 Then
                    receivedFaxDirectory = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                End If

                If IsNothing(gloRegistrySetting.GetRegistryValue("FAXDownloadDirectory")) = False Then
                    gstrFAXReceivedDirectoryTS = gloRegistrySetting.GetRegistryValue("FAXDownloadDirectory")
                    If System.IO.Directory.Exists(gstrFAXReceivedDirectoryTS) = False Then
                        gstrFAXReceivedDirectoryTS = receivedFaxDirectory
                    End If
                Else
                    gstrFAXReceivedDirectoryTS = receivedFaxDirectory
                    gloRegistrySetting.SetRegistryValue("FAXDownloadDirectory", gstrFAXReceivedDirectoryTS)
                End If
                receivedFaxDirectory = String.Empty

            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                dvSettings.RowFilter = Nothing
            End Try
        End If
        UpdateLog("gstrFAXReceivedDirectoryTS : End ")


        UpdateLog(" isPrinterSettingsSet : START ")
        'Check FAX Printers necessary settings are set or not
        gblnFAXPrinterSettingsSet = isPrinterSettingsSet()
        UpdateLog(" isPrinterSettingsSet : END ")

        If IsNothing(gloRegistrySetting.GetRegistryValue("SameCoverPageForAllReferrals")) = False Then
            gblnSameCoverPageForAllReferrals = gloRegistrySetting.GetRegistryValue("SameCoverPageForAllReferrals")
        Else
            gblnSameCoverPageForAllReferrals = True
            gloRegistrySetting.SetRegistryValue("SameCoverPageForAllReferrals", gblnSameCoverPageForAllReferrals)
        End If

        'Get FAX Cover Page
        gblnFAXCoverPage = False

        If IsNothing(gloRegistrySetting.GetRegistryValue("FAXCoverPage")) = False Then
            If gloRegistrySetting.GetRegistryValue("FAXCoverPage") = "1" Then
                gblnFAXCoverPage = True
            End If
        End If

        '<Vinayak-Chnage for if Path is Drive then remove "\" - 27 Dec 2005>

        If IsNothing(gloRegistrySetting.GetRegistryValue("DMSPath")) = False Then
            DMSRootPath = gloRegistrySetting.GetRegistryValue("DMSPath")
            If DMSRootPath.Trim <> "" Then
                If Mid(DMSRootPath, Len(DMSRootPath)) = "\" Then
                    DMSRootPath = Mid(DMSRootPath, 1, Len(DMSRootPath) - 1)
                End If
            End If
        End If

        'If IsNothing(regKey.GetValue("VMSPath")) = False Then
        '    VMSRootPath = regKey.GetValue("VMSPath") & ""
        If IsNothing(gloRegistrySetting.GetRegistryValue("VMSPath")) = False Then
            VMSRootPath = gloRegistrySetting.GetRegistryValue("VMSPath") & ""
            If VMSRootPath.Trim <> "" Then
                If Mid(VMSRootPath, Len(VMSRootPath)) = "\" Then
                    VMSRootPath = Mid(VMSRootPath, 1, Len(VMSRootPath) - 1)
                End If
            End If
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("ServerPath")) = False Then
            gstrServerPath = gloRegistrySetting.GetRegistryValue("ServerPath")
        End If
        'Retrieve Appointment Level
        If IsNothing(gloRegistrySetting.GetRegistryValue("AppointmentModuleLevel")) = False Then
            gnAppointmentModuleLevel = Val(gloRegistrySetting.GetRegistryValue("AppointmentModuleLevel"))
        Else
            gnAppointmentModuleLevel = 0
        End If


        'Retrieve Clinic Working Time Color Settings
        If IsNothing(gloRegistrySetting.GetRegistryValue("WorkingTime")) = False Then
            nWorkingTimeColor = gloRegistrySetting.GetRegistryValue("WorkingTime")
        Else
            nWorkingTimeColor = Color.FromArgb(255, 255, 128).ToArgb
        End If

        'Retrieve Clinic None Working Time Color Settings
        If IsNothing(gloRegistrySetting.GetRegistryValue("NonWorkingTime")) = False Then
            nNonWorkingTimeColor = gloRegistrySetting.GetRegistryValue("NonWorkingTime")
        Else
            nNonWorkingTimeColor = Color.FromArgb(192, 192, 192).ToArgb
        End If

        'Retrieve Doctor Busy Time Color Settings
        If IsNothing(gloRegistrySetting.GetRegistryValue("BusyTimeColor")) = False Then
            nBusyTimeColor = gloRegistrySetting.GetRegistryValue("BusyTimeColor")
        Else
            nBusyTimeColor = Color.FromArgb(255, 0, 0).ToArgb
        End If

        'Retrieve Missing Appointments Color Settings
        If IsNothing(gloRegistrySetting.GetRegistryValue("MissingAppointmentsColor")) = False Then
            nMissingAppointmentsColor = gloRegistrySetting.GetRegistryValue("MissingAppointmentsColor")
        Else
            nMissingAppointmentsColor = Color.FromArgb(255, 255, 255).ToArgb
        End If

        'Retrieve Pull Charts Appointment Color Settings
        If IsNothing(gloRegistrySetting.GetRegistryValue("PullChartsAppointmentsColor")) = False Then
            nPullChartsAppointmentsColor = gloRegistrySetting.GetRegistryValue("PullChartsAppointmentsColor")
        Else
            nPullChartsAppointmentsColor = Color.FromArgb(255, 255, 255).ToArgb
        End If

        'Retrieve Lock Interval Color Settings
        If IsNothing(gloRegistrySetting.GetRegistryValue("LockTime")) = False Then
            gLockTime = gloRegistrySetting.GetRegistryValue("LockTime")
        Else
            gLockTime = 10
        End If

        'Retrieve Auto Application Lock Status
        If IsNothing(gloRegistrySetting.GetRegistryValue("AutoLockEnable")) = False Then
            gblnAutoLockEnable = gloRegistrySetting.GetRegistryValue("AutoLockEnable")
        Else
            gblnAutoLockEnable = True
        End If

        'CheckNewVersion
        gblnCheckNewVersion = False

        '< AlertForeColor/ AlertBackColor>
        'To Get the Alerts Fore Color / Back Color
        If IsNothing(gloRegistrySetting.GetRegistryValue("AlertForeColor")) = False Then
            gnAlertForeColor = gloRegistrySetting.GetRegistryValue("AlertForeColor")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("AlertBackColor")) = False Then
            gnAlertBackColor = gloRegistrySetting.GetRegistryValue("AlertBackColor")
        End If
        '''' < AlertForeColor/ AlertBackColor>

        If IsNothing(gloRegistrySetting.GetRegistryValue("HighLightWord")) = False Then
            gblnWordColorHighlight = gloRegistrySetting.GetRegistryValue("HighLightWord")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("HighlightedColor")) = False Then
            gblnWordBackColor = gloRegistrySetting.GetRegistryValue("HighlightedColor")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("ExamNotesSelection")) = False Then
            gblnExamSelection = gloRegistrySetting.GetRegistryValue("ExamNotesSelection")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("ShowBdayReminder")) = False Then
            If gloRegistrySetting.GetRegistryValue("ShowBdayReminder") = 1 Then
                gblnBdayReminder = True
                If IsNothing(gloRegistrySetting.GetRegistryValue("BdayReminderDays")) = False Then
                    gnBDayReminderDays = gloRegistrySetting.GetRegistryValue("BdayReminderDays")
                Else
                    gnBDayReminderDays = 0
                End If
            End If
        End If '------

        'Retrieve Pen Width  for Drawing Pad
        If IsNothing(gloRegistrySetting.GetRegistryValue("PenWidth")) = False Then
            gintPenWidth = Val(gloRegistrySetting.GetRegistryValue("PenWidth"))
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("RxSelectedDrugButton")) = False Then
            gnDrugListButton = Val(gloRegistrySetting.GetRegistryValue("RxSelectedDrugButton"))
        Else
            gloRegistrySetting.SetRegistryValue("RxSelectedDrugButton", "13")
            gnDrugListButton = 13
        End If


        ''''Retrieve sure script Alert will Show or not
        If IsNothing(gloRegistrySetting.GetRegistryValue(SureScriptAlert)) = False Then
            If gloRegistrySetting.GetRegistryValue(SureScriptAlert) = 1 Then
                gblnSurescriptAlert = True
            Else
                gblnSurescriptAlert = False
            End If
        End If

        '''' Retrieve Sure script Alert Interval Time

        If IsNothing(gloRegistrySetting.GetRegistryValue(AddReferralsNote)) = False Then
            If gloRegistrySetting.GetRegistryValue(AddReferralsNote) = 1 Then
                gblnIsReferalNoteadd = True
            Else
                gblnIsReferalNoteadd = False
            End If
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue(SureScriptAlertTime)) = False Then
            gStrSurescriptAlertmin = gloRegistrySetting.GetRegistryValue(SureScriptAlertTime)
        Else
            gStrSurescriptAlertmin = "15 Sec"
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("PageNoSetting")) = True Then
            gloRegistrySetting.SetRegistryValue("PageNoSetting", "True")
            gblnPageNo = True
        Else
            gblnPageNo = gloRegistrySetting.GetRegistryValue("PageNoSetting")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("PageNoSetting")) = True Then
            gloRegistrySetting.SetRegistryValue("PageNoSetting", "True")
            gblnPageNo = True
        Else
            gblnPageNo = gloRegistrySetting.GetRegistryValue("PageNoSetting")
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("DICOMPATH")) = True Then
            DICOMPath = ""
        Else
            DICOMPath = gloRegistrySetting.GetRegistryValue("DICOMPATH")
        End If

        ' added on date 20160525  for setting global varibale for local print setting
        If IsNothing(gloRegistrySetting.GetRegistryValue("EnableLocalPrinter")) = False Then
            If gloRegistrySetting.GetRegistryValue("EnableLocalPrinter") = 1 Then
                gblnEnableLocalPrinter = True
                gloGlobal.gloTSPrint.isCopyPrint = True

                'get setting for adding footer in application / gloClinical Queue service 
                If IsNothing(gloRegistrySetting.GetRegistryValue("AddFooterInService")) = False Then
                    If gloRegistrySetting.GetRegistryValue("AddFooterInService") = 1 Then
                        gblnAddFooterInService = True
                        gloGlobal.gloTSPrint.AddFooterInService = True
                    Else
                        gblnAddFooterInService = False
                        gloGlobal.gloTSPrint.AddFooterInService = False
                    End If
                Else
                    gblnAddFooterInService = False
                    gloGlobal.gloTSPrint.AddFooterInService = False
                End If

                'get no of pages to split
                Dim result As Integer = 0
                If IsNothing(gloRegistrySetting.GetRegistryValue("NoOfPagesToSplit")) = False Then
                    Integer.TryParse(gloRegistrySetting.GetRegistryValue("NoOfPagesToSplit").ToString(), result)
                End If
                gloGlobal.gloTSPrint.NoOfPages = result

                'get no of templates to print per jon for Batch Print template report
                Dim NoOfTemplates As Integer = 1
                If IsNothing(gloRegistrySetting.GetRegistryValue("NoOfTemplatesPerJob")) = False Then
                    Integer.TryParse(gloRegistrySetting.GetRegistryValue("NoOfTemplatesPerJob").ToString(), NoOfTemplates)
                End If
                gloGlobal.gloTSPrint.NoOfTemplatesPerJob = NoOfTemplates

                'get file type to be used for word printing - EMF / PDF
                Try
                    If IsNothing(gloRegistrySetting.GetRegistryValue("UseEMFFile")) = False Then
                        If gloRegistrySetting.GetRegistryValue("UseEMFFile").ToString().ToLower() = "1" Or gloRegistrySetting.GetRegistryValue("UseEMFFile").ToString().ToLower() = "true" Then
                            gloGlobal.gloTSPrint.UseEMFForWord = True
                        Else
                            gloGlobal.gloTSPrint.UseEMFForWord = False
                        End If
                    Else
                        gloGlobal.gloTSPrint.UseEMFForWord = True
                    End If
                Catch
                    gloGlobal.gloTSPrint.UseEMFForWord = True
                End Try


                'get file type to be used for SSRS printing - EMF / PDF
                Try
                    If IsNothing(gloRegistrySetting.GetRegistryValue("UseEMFFileSSRS")) = False Then
                        If gloRegistrySetting.GetRegistryValue("UseEMFFileSSRS") = 1 Then
                            gloGlobal.gloTSPrint.UseEMFForSSRS = True
                        Else
                            gloGlobal.gloTSPrint.UseEMFForSSRS = False
                        End If
                    Else
                        gloGlobal.gloTSPrint.UseEMFForSSRS = True
                    End If
                Catch
                    gloGlobal.gloTSPrint.UseEMFForSSRS = True
                End Try

                'get file type to be used for Claims printing - EMF / PDF
                Try
                    If IsNothing(gloRegistrySetting.GetRegistryValue("UseEMFForClaims")) = False Then
                        If gloRegistrySetting.GetRegistryValue("UseEMFForClaims") = 1 Then
                            gloGlobal.gloTSPrint.UseEMFForClaims = True
                        Else
                            gloGlobal.gloTSPrint.UseEMFForClaims = False
                        End If
                    Else
                        gloGlobal.gloTSPrint.UseEMFForClaims = True
                    End If
                Catch
                    gloGlobal.gloTSPrint.UseEMFForClaims = True
                End Try

                'get file type to be used for Claims printing - EMF / PDF
                Try
                    If IsNothing(gloRegistrySetting.GetRegistryValue("UseEMFForImages")) = False Then
                        If gloRegistrySetting.GetRegistryValue("UseEMFForImages") = 1 Then
                            gloGlobal.gloTSPrint.UseEMFForImages = True
                        Else
                            gloGlobal.gloTSPrint.UseEMFForImages = False
                        End If
                    Else
                        gloGlobal.gloTSPrint.UseEMFForImages = True
                    End If
                Catch
                    gloGlobal.gloTSPrint.UseEMFForImages = True
                End Try

                'get file type to be used for metadata file
                Try
                    If IsNothing(gloRegistrySetting.GetRegistryValue("UseZippedMetadata")) = False Then
                        If gloRegistrySetting.GetRegistryValue("UseZippedMetadata").ToString().ToLower() = "1" Or gloRegistrySetting.GetRegistryValue("UseZippedMetadata").ToString().ToLower() = "true" Then
                            gloGlobal.gloTSPrint.UseZippedMetadata = True
                        Else
                            gloGlobal.gloTSPrint.UseZippedMetadata = False
                        End If
                    Else
                        gloGlobal.gloTSPrint.UseZippedMetadata = False
                    End If
                Catch
                    gloGlobal.gloTSPrint.UseZippedMetadata = False
                End Try
            Else
                gblnEnableLocalPrinter = False
                gloGlobal.gloTSPrint.isCopyPrint = False

                gblnAddFooterInService = False
                gloGlobal.gloTSPrint.AddFooterInService = False

                gloGlobal.gloTSPrint.NoOfPages = 0
                'gloGlobal.gloTSPrint.UseEMFForWord = False
                'gloGlobal.gloTSPrint.UseEMFForSSRS = False
                'gloGlobal.gloTSPrint.UseEMFForClaims = False
                'gloGlobal.gloTSPrint.UseEMFForImages = False
            End If
        Else
            gblnEnableLocalPrinter = False
            gloGlobal.gloTSPrint.isCopyPrint = False

            gblnAddFooterInService = False
            gloGlobal.gloTSPrint.AddFooterInService = False

            gloGlobal.gloTSPrint.NoOfPages = 0
            'gloGlobal.gloTSPrint.UseEMFForWord = False
            'gloGlobal.gloTSPrint.UseEMFForSSRS = False
            'gloGlobal.gloTSPrint.UseEMFForClaims = False
            'gloGlobal.gloTSPrint.UseEMFForImages = False
        End If
        gloGlobal.gloTSPrint.TempPath = gloSettings.FolderSettings.AppTempFolderPath

        If IsNothing(gloRegistrySetting.GetRegistryValue("UseDefaultPrinter")) = False Then
            If gloRegistrySetting.GetRegistryValue("UseDefaultPrinter") = 1 Then
                gblnUseDefaultPrinter = True
                '' added by Abhijeet on date 20100419  for setting glolab varibale for default printer setting
                gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = True
                '' end of code  by Abhijeet on date 20100419  for setting glolab varibale for default printer setting

            Else
                gblnUseDefaultPrinter = False
                '' added by Abhijeet on date 20100419  for setting glolab varibale for default printer setting
                gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = False
                '' end of code  by Abhijeet on date 20100419  for setting glolab varibale for default printer setting

            End If
        Else
            gblnUseDefaultPrinter = True
            '' added by Abhijeet on date 20100419  for setting glolab varibale for default printer setting
            gloEmdeonCommon.mdlGeneral.gblnIsDefaultPrinter = True
            '' end of code  by Abhijeet on date 20100419  for setting glolab varibale for default printer setting

        End If

        'Check Network DIR 
        'gloGlobal.gloRemoteScanSettings.DoesNetworkDirExist()

        '28-Apr-17 Aniket: Resolving Login Speed Issue
        Dim oDoesNetworkDirExist As Thread
        oDoesNetworkDirExist = New Thread(New ThreadStart(AddressOf DoesNetworkDirExist))
        oDoesNetworkDirExist.Start()

        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " ='SkipScannerList'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue"))
                If sValue.Trim() <> "" Then
                    gloGlobal.gloRemoteScanSettings.skipScannerList = sValue.Split(",")
                End If
            End If
            'conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try


        Dim oRemoteScan As New gloEDocumentV3.Common.RemoteScanCommon()
        Dim sRetVal As String = Nothing


        'Remote Scanning
        'Dim oRemoteScan As New gloEDocumentV3.Common.RemoteScanCommon()
        sRetVal = Nothing
        'Dim key As RegistryKey = Nothing
        Try
            'Refresh Scanners


            'EnableRemoteScan Setting
            gloRegistrySetting.CloseRegistryKey()
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            'key = Registry.CurrentUser.OpenSubKey(gloRegistrySetting.gstrSoftEMR)
            Dim sRemoteScanRegKey As String = "false"
            'If key IsNot Nothing Then
            '    sRemoteScanRegKey = Convert.ToString(key.GetValue("EnableRemoteScan"))
            'End If
            sRemoteScanRegKey = Convert.ToString(gloRegistrySetting.GetRegistryValue("EnableRemoteScan"))

            'oRemoteScan.GetRegistryValue("EnableRemoteScan");
            If Not String.IsNullOrEmpty(sRemoteScanRegKey) Then
                gloGlobal.gloRemoteScanSettings.EnableRemoteScan = Convert.ToBoolean(sRemoteScanRegKey)
            End If
            ' Scanner Settings Masters
            If gloGlobal.gloRemoteScanSettings.EnableRemoteScan Then
                Dim sZipScanSettingsRegKey As String = "false"
                sZipScanSettingsRegKey = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrZipScannerSettings))
                If Not String.IsNullOrEmpty(sZipScanSettingsRegKey) Then
                    gloGlobal.gloRemoteScanSettings.bZipScanSettings = Convert.ToBoolean(sZipScanSettingsRegKey)
                End If

                If gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings Is Nothing Then
                    gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject()

                    'Current Settings
                    sRetVal = oRemoteScan.SetRemoteScannerCurrentSettings(Nothing, Nothing, Nothing)
                    If Not String.IsNullOrEmpty(sRetVal) Then
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure)
                        UpdateLog(sRetVal)
                    End If
                End If
            End If


        Catch ex As Exception
            ' MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Security, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            UpdateLog(ex.ToString())
        Finally
            oRemoteScan = Nothing
            'key = Nothing
        End Try

        'End Remote Scanning

        ' Eliminate EliminatePegasus
        If gloGlobal.gloRemoteScanSettings.EnableRemoteScan Then
            gloGlobal.gloEliminatePegasus.bEliminatePegasus = False
        Else
            Try
                gloRegistrySetting.CloseRegistryKey()
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                Dim sEliminatePegasus As String = "false"
                sEliminatePegasus = Convert.ToString(gloRegistrySetting.GetRegistryValue("EliminatePegasus"))

                If Not String.IsNullOrEmpty(sEliminatePegasus) Then
                    gloGlobal.gloEliminatePegasus.bEliminatePegasus = Convert.ToBoolean(sEliminatePegasus)
                End If

                If gloGlobal.gloEliminatePegasus.bEliminatePegasus Then
                    If gloRemoteScanGeneral.RemoteScanSettings.oScannerSettings Is Nothing Then
                        Dim logdata As New gloGlobal.clsDatalog()
                        If gloRemoteScanGeneral.TwainScanFunctionality.CreateTwainScanSettingsFile() Then
                            gloRemoteScanGeneral.RemoteScanSettings.SetScannerSettingsObject(True)

                            'Current Settings
                            If oRemoteScan Is Nothing Then
                                oRemoteScan = New gloEDocumentV3.Common.RemoteScanCommon()
                            End If

                            sRetVal = oRemoteScan.SetRemoteScannerCurrentSettings(Nothing, Nothing, Nothing)
                            If Not String.IsNullOrEmpty(sRetVal) Then
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, sRetVal, gloAuditTrail.ActivityOutCome.Failure)
                            End If
                            '
                        End If
                    End If

                    'Delete old scan config files (local) 
                    Try
                        gloRemoteScanGeneral.gloRemoteScanMetaDataWriter.DeleteScanConfigFiles()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Finally
                'gloRegistrySetting.CloseRegistryKey()
                'gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                If Not oRemoteScan Is Nothing Then
                    oRemoteScan = Nothing
                End If
            End Try
        End If

        'read pegasus brightness
        Try
            gloRegistrySetting.CloseRegistryKey()
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            Dim sPegasusBright As String = ""
            sPegasusBright = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrPegasusBright))

            If Not String.IsNullOrEmpty(sPegasusBright) Then
                gloGlobal.gloEliminatePegasus.sPegasusBright = sPegasusBright
            Else
                sPegasusBright = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSBright))
                If Not String.IsNullOrEmpty(sPegasusBright) Then
                    gloGlobal.gloEliminatePegasus.sPegasusBright = sPegasusBright
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusBright, sPegasusBright)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'gloRegistrySetting.CloseRegistryKey()
            'gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        End Try
        '
        'read pegasus contrast
        Try
            gloRegistrySetting.CloseRegistryKey()
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            Dim sPegasusContrast As String = ""
            sPegasusContrast = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrPegasusContrast))

            If Not String.IsNullOrEmpty(sPegasusContrast) Then
                gloGlobal.gloEliminatePegasus.sPegasusContrast = sPegasusContrast
            Else
                sPegasusContrast = Convert.ToString(gloRegistrySetting.GetRegistryValue(gloRegistrySetting.gstrDMSContrast))
                If Not String.IsNullOrEmpty(sPegasusContrast) Then
                    gloGlobal.gloEliminatePegasus.sPegasusContrast = sPegasusContrast
                    gloRegistrySetting.SetRegistryValue(gloRegistrySetting.gstrPegasusContrast, sPegasusContrast)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Login, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'gloRegistrySetting.CloseRegistryKey()
            'gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        End Try
        '

        If IsNothing(gloRegistrySetting.GetRegistryValue("DICOMPath")) = False Then
            DICOMPath = gloRegistrySetting.GetRegistryValue("DICOMPath")
        End If


        gloRegistrySetting.CloseRegistryKey()
        UpdateLog("GetEMRSettings() : Read Reg Values END")


        UpdateLog("GetEMRSettings() : USECODEDHISTORY START ")
        'CCHIT 08 to read the Coded History setting value
        'Dim conn As New SqlConnection(GetConnectionString)
        'Dim cmd As SqlCommand
        Dim _strSQL As String = ""
        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " ='USECODEDHISTORY'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue"))
                If sValue.Trim() = "0" Or sValue.Trim() = "" Then
                    gblnCodedHistory = False
                Else
                    gblnCodedHistory = True
                End If
            Else
                gblnCodedHistory = False
            End If
            'conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        UpdateLog("GetEMRSettings() : USECODEDHISTORY END ")

        ''Read Setting to Show Coded History code,description or both from Admin 
        If (gblnCodedHistory = True) Then
            ShowCodeOrDescription()
        End If

        ''Read value for ICD9 driven /CPT driven
        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " ='EXAM DIAGNOSIS'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue"))
                If sValue.Trim() = "0" Then
                    gblnICD9Driven = False
                Else
                    gblnICD9Driven = True
                End If
            Else
                gblnICD9Driven = True
            End If
            '  conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try


        ''read value for internet fax setting
        'Added on 20100616 for E&M OnOff Setting by sanjog
        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'E&M ENABLE'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue = "0" Then
                    gblnEMEnable = False
                Else
                    gblnEMEnable = True
                End If
            Else
                gblnEMEnable = True
            End If
            '  conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try

        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'InternetFax'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue = "0" Or sValue = "" Then
                    gblnInternetFax = False
                Else
                    gblnInternetFax = True
                End If
            Else
                gblnInternetFax = False
            End If
            ' conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        'end internet fax setting 

        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'CQM CYPRESS TESTING'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue = "0" Or sValue = "" Then
                    gblnEnableCQMCypressTesting = False
                Else
                    gblnEnableCQMCypressTesting = True
                End If
            Else
                gblnEnableCQMCypressTesting = False
            End If
            ' conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try

        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'EMVISITTYPE'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                gstrVisitTypes = sValue
            Else
                gstrVisitTypes = ""
            End If
            'conn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        Try


            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'EMEXAMTYPES'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                gstrExamTypes = sValue
            Else
                gstrExamTypes = ""
            End If
            '  conn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        ' Integrated by Mayuri:20100731- To retrieve E&M Code Enable value from gloAdmin"
        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'E&M ENABLE'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue = "0" Then
                    gblnEMEnable = False
                Else
                    gblnEMEnable = True
                End If
            Else
                gblnEMEnable = True
            End If
            ' conn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        ' Integrated by Mayuri:20100731 - To retrieve E&M Code Enable value from gloAdmin"

        Try

            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'Country'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue <> "" Then
                    gstrCountry = sValue
                Else
                    gstrCountry = "US"
                End If
            Else
                gstrCountry = "US"
            End If
            'conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        ''Sandip Darade 20100326
        'gintNoOfAttempts
        Try

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'No. Of. Attempts'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue = "" Then
                    gintNoOfAttempts = 3 '' Default No of Attempts are 3
                Else
                    gintNoOfAttempts = sValue
                End If
            Else
                gintNoOfAttempts = 3 '' Default No of Attempts are 3
            End If
            'conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try

        ''new setting added for task incident keeping task window open or not from dms screen
        Try

            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'AUTO COMPLETE DMS TASKS ON ACKNOWLEDGEMENT'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue.Contains("True") Then
                    gblnAutoCompDMSTask = True
                Else
                    gblnAutoCompDMSTask = False
                End If
            Else
                gblnAutoCompDMSTask = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try

        Try

            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'Close DMS Task Window'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue.Contains("True") Then
                    gblnAutoCloseDMSWindow = True
                Else
                    gblnAutoCloseDMSWindow = False
                End If
            Else
                gblnAutoCloseDMSWindow = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
        GetSigPlusSettings()

        GetGreyScreenIssueSettings()
        
        Dim objCleargage As Object = Nothing
        Dim clsSettings As New gloSettings.GeneralSettings(GetConnectionString())
        Try
            clsSettings.GetSetting("EnableCleargageFeature", objCleargage)
            If Not String.IsNullOrEmpty(Convert.ToString(objCleargage)) Then
                gloGlobal.gloPMGlobal.IsCleargageEnable = Convert.ToBoolean(Convert.ToString(objCleargage).Trim())
            Else
                gloGlobal.gloPMGlobal.IsCleargageEnable = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(clsSettings) Then
                clsSettings.Dispose()
                clsSettings = Nothing
            End If
            If Not IsNothing(objCleargage) Then
                objCleargage = Nothing
            End If
        End Try



    End Sub


    Public Function ScanClientInterface(ByVal strProductName As String, ByVal strMachineName As String)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim dsData As New DataSet
        Dim objCmd As New SqlCommand
        Try

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewClientInterface"
            objCmd.Connection = objCon
            Dim objMachineName As New SqlParameter
            With objMachineName
                .ParameterName = "@sMachineName"
                .Value = strMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objMachineName)

            Dim objParaProduct As New SqlParameter
            With objParaProduct
                .ParameterName = "@sProductName"
                .Value = strProductName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objParaProduct)
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dsData)
            objDA.Dispose()
            objDA = Nothing

            objCon.Close()

            Dim nCount As Integer
            For nCount = 0 To dsData.Tables(0).Rows.Count - 1

                appSettings("HL7SENDOUTBOUNDGLOEMR") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("HL7SENDOUTBOUNDGLOEMR")).ToString()
                appSettings("GENIUSSENDOUTBOUNDGLOEMR") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("GENIUSSENDOUTBOUNDGLOEMR")).ToString()
                appSettings("SendPatientDetails") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bHl7_SendPatientDetails")).ToString()
                appSettings("SendAppointmentDetails") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendAppointmentDetails")).ToString()
                appSettings("SendImmunizationDetails") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bHl7_SendImmunizationDetails")).ToString()

                appSettings("SendOnSaveandClose") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendChargesSaveClose")).ToString()
                appSettings("SendOnSaveandFinish") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendChargesSaveFinish")).ToString()

                appSettings("GeniusSaveClose") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bGenius_SendChargesSaveClose")).ToString()
                appSettings("GeniusSaveFinish") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bGenius_SendChargesSaveFinish")).ToString()

                'start of code added by manoj jadhav on 20140220 for MDM_T02 Message Queue genration
                appSettings("bHL7_sendVisitSumSaveClose") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bHL7_sendVisitSumSaveClose")).ToString()
                appSettings("bHL7_sendVisitSumSaveFinish") = Convert.ToInt16(dsData.Tables(0).Rows.Item(nCount).Item("bHL7_sendVisitSumSavefinish")).ToString()
                'start of code added by manoj jadhav on 20140220 for MDM_T02 Message Queue genration


            Next

            objParaProduct = Nothing
            objMachineName = Nothing

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dsData.Dispose()
            dsData = Nothing
            objCon.Dispose()
            objCon = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Private Sub setSigplusDefaultSettingsforTS()
        Try
            If gloRegistrySetting.IsServerOS Then

                Try
                    If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                        gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                        gloRegistrySetting.CloseRegistryKey()
                    End If
                Catch ex As Exception
                    UpdateLog("Error in Creating gloEMR Registry keys for Sigplus Setting : " + ex.ToString())
                    ex = Nothing
                End Try


                Try
                    If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = True Then
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                        gloRegistrySetting.SetRegistryValue("SigPlusSupportTS", "True")
                        gloRegistrySetting.SetRegistryValue("SigPlusTabletPortPath", gloSettings.FolderSettings.TempFolderName.Replace("Temp_", ""))
                        gloRegistrySetting.SetRegistryValue("SigPlusTabletType", "7")
                        gloRegistrySetting.CloseRegistryKey()
                    End If
                Catch ex As Exception
                    UpdateLog("Error in writing gloEMR Registry keys for Sigplus Setting : " + ex.ToString())
                    ex = Nothing
                End Try

            End If
        Catch ex As Exception
            UpdateLog("Error in Writing Registry Values for Sigplus Setting : " + ex.ToString())
            ex = Nothing
        End Try
    End Sub

    ' Problem #28303: 00000156 : Signature Pad not working on Terminal Server    
    ' Function added to Get SigPlus related Settings from Registry 
    Private Function GetSigPlusSettings() As Boolean
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        'Dim value As New Object()
        '  Dim strValue As String

        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            gblnIsSigPlusSettingsAvailable = True

            '' Tablet Port Path
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusSupportTS")) Then
                gblnIsSigPlusSettingsAvailable = False
            Else
                gblnSigPlusSupportTS = gloRegistrySetting.GetRegistryValue("SigPlusSupportTS")
            End If

            '' Tablet Port Path
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusTabletPortPath")) Then
                gblnIsSigPlusSettingsAvailable = False
            Else
                gstrSigPlusTabletPortPath = gloRegistrySetting.GetRegistryValue("SigPlusTabletPortPath")
            End If

            '' Tablet Type
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusTabletType")) Then
                If gblnSigPlusSupportTS And gloRegistrySetting.IsServerOS Then
                    gshortSigPlusTabletType = "7"    '' Default for TS
                Else
                    gshortSigPlusTabletType = "6"    '' Default for Normal OS
                End If
            Else
                gshortSigPlusTabletType = gloRegistrySetting.GetRegistryValue("SigPlusTabletType")
            End If

            'Local Signature pad
            If IsNothing(gloRegistrySetting.GetRegistryValue("SigPlusLocalSignaturePad")) Then
                gblnLocalSignaturePad = False
            Else
                gblnLocalSignaturePad = gloRegistrySetting.GetRegistryValue("SigPlusLocalSignaturePad")
            End If

            gloRegistrySetting.CloseRegistryKey()
            Return True

        Catch ex As Exception
            gblnIsSigPlusSettingsAvailable = False
            Return False

        Finally
            If Not ogloSettings Is Nothing Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If
            'If Not value Is Nothing Then
            '    value = Nothing
            'End If

        End Try
    End Function
    '------------------

    Private Function GetGreyScreenIssueSettings() As Boolean
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        Try
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)

            If IsNothing(gloRegistrySetting.GetRegistryValue("GreyScreenIssue")) Then
                gblnGreyScreenIssue = False
            Else
                gblnGreyScreenIssue = gloRegistrySetting.GetRegistryValue("GreyScreenIssue")
            End If

            gloRegistrySetting.CloseRegistryKey()
            Return True

        Catch ex As Exception
            gblnGreyScreenIssue = False
            Return False
        Finally
            If Not ogloSettings Is Nothing Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If
        End Try
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            UpdateLog("Timer1_Tick: START")
            gloAuditTrail.gloAuditTrail.MessageBoxCaption = strMessageBoxCaption ''Dhruv for setting the audit log when there is no gloServices database presenet
            Timer1.Enabled = False
            Application.DoEvents()

            'code start by nilesh on 20110226 for case GLO2010-0008612
            CheckAnotherInstance()
            'code end by nilesh on 20110226 for case GLO2010-0008612

            Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
            UpdateLog("START GetOSInfo()")
            If GetOSInfo() = False Then
                'OS is Not Server OS
                Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)
                'code comment start by nilesh on 20110226 for case GLO2010-0008612

                'Dont Create a Folder inside Temp Folder 
                'Just Assign the Temp Folder to global Variable
                ''gstrgloTempFolder = "\Temp\"

                'For Testing ONLY Remove it afetr Testing - Mahesh
                'gstrgloTempFolder = CreateNewTempDirectory() & "\"
                '
            Else
                '' OS is Server OS 
                gloRegistrySetting.IsServerOS = True
                '' Create a Temp Folder of each user 
                ''gstrgloTempFolder = CreateNewTempDirectory() & "\" '' "\Temp\" & Now.Year & Now.Month & Now.Date & Now.Hour & Now.Minute
                '' Temp\Date Folder
            End If
            UpdateLog("END GetOSInfo()")

            ''''Pramod Set DMSV2 global path
            gDMSV2TempPath = gloSettings.FolderSettings.AppTempFolderPath & "DMSV2Temp"
            gDMSV3TempPath = gloSettings.FolderSettings.AppTempFolderPath & "DMSV3Temp"

            UpdateLog("START IsSettings()")
            If IsSettings() = False Then
                Dim frmSettings As New frmStartupSettings
                Me.Hide()
                If frmSettings.ShowDialog(IIf(IsNothing(frmSettings.Parent), Me, frmSettings.Parent)) = DialogResult.OK Then
                    Me.Visible = True
                    pnlLogin.Visible = True
                    picAnimation.Visible = False
                End If
                'Code opti
                If Not IsNothing(frmSettings) Then
                    frmSettings.Dispose()
                    frmSettings = Nothing
                End If
            End If
            UpdateLog("END IsSettings()")



            UpdateLog("Checking Connection : START")
            ' Dim objSettings As New clsStartUpSettings
            If clsStartUpSettings.IsConnect(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR) = False Then
                If MessageBox.Show("Unable to connect to SQL Server " & gstrSQLServerName & " and Database " & gstrDatabaseName & vbCrLf & "Do you want to change SQL Server or Database Settings?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                    End
                End If
                Dim frmSettings As New frmStartupSettings
                Me.Hide()
                If frmSettings.ShowDialog(IIf(IsNothing(frmSettings.Parent), Me, frmSettings.Parent)) = DialogResult.OK Then
                    Me.Visible = True
                    pnlLogin.Visible = True
                    picAnimation.Visible = False
                End If
                'Code opti
                If Not IsNothing(frmSettings) Then
                    frmSettings.Dispose()
                    frmSettings = Nothing
                End If
            End If

            '  objSettings = Nothing
            UpdateLog("Checking Connection : END")

            UpdateLog("Checking Machine Permission : START")
            'Check Machine Permission
            gstrClientMachineName = System.Windows.Forms.SystemInformation.ComputerName()
            Dim objLogin As New clsLogin
            If objLogin.IsClientAccess(gstrClientMachineName) = False Then
                MessageBox.Show("This machine does not have rights to access gloEMR system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                objLogin.Dispose()
                objLogin = Nothing
                End
            Else
                objLogin.Dispose()
                objLogin = Nothing
            End If
            UpdateLog("Checking Machine Permission : END")

            ''Dhruv 20091202

            ''Added sServiceDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   
            ''To Fill the Database from gloservice
            '' sServiceDatabaseName = gstrServicesDBName
            FillServiceDatabases()
            ''Added sServiceDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table   


            'UpdateLog("Auto Upadte Code : START")
            '' Auto Upadte Code
            ''''Add by Pramod to check client Update
            UpdateLog("Check for Update log Start")
            Dim blsIsUpdatePresent As Boolean
            Dim objAus As New gloAUSLibrary.clsAus
            ' objAus.CheckforUpdate(GetConnectionString, "gloEMR", gstrServerPath, gstrDatabaseName)
            'Aniket: Return value for Single Sign ON
            blsIsUpdatePresent = objAus.CheckforUpdate(GetConnectionString, "gloEMR", gstrServerPath, gstrDatabaseName)

            UpdateLog("Check for Update log End")

            objAus = Nothing
            UpdateLog("Controls geting Visible: START")
            'Retrieve Last Modified Date
            'Dim strDate As String = Format(File.GetLastWriteTime(gstrgloEMRStartupPath & "\" & aModuleName), "dd MMM, yyyy") & vbCrLf & Format(File.GetLastWriteTime(gstrgloEMRStartupPath & "\" & aModuleName), "hh:mm:ss tt")
            'lblLastModifiedDate.Text = "Software Last Modified " & strDate ''"Last Modified Date " & File.GetLastWriteTime(gstrgloEMRStartupPath & "\" & aModuleName)
            'Change Date modified format for new splash screen change.
            lblLastModifiedDate.Text = Format(File.GetLastWriteTime(gstrgloEMRStartupPath & "\" & aModuleName), "MMM dd, yyyy")
            lblVersion.Text = gstrVersion

            ''Version CheckUp & Update if not Present in Database.
            strVersion = GetLastVersion(aModuleName)
            'objLogin.UpdateClientVersion(gstrClientMachineName, strVersion, strDate)

            pnlLogin.Visible = True
            picAnimation.Visible = False
            txtUserName.Focus()
            Me.Visible = True
            UpdateLog("Controls geting Visible: END")
            UpdateLog("Timer1_Tick: END")
            If LogOffFired = False AndAlso blsIsUpdatePresent = False Then
                Loginclick(True)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SingleSignONUserNamePassword()

        Dim objLogin As New clsLogin
        Dim dtUser As DataTable
        Dim oEncryption As New clsencryption
        Dim localMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.LocalMachineDetails()



        dtUser = objLogin.GetSingleSignONUser(localMachine.UserName)

        If IsNothing(dtUser) = False Then
            If dtUser.Rows.Count > 0 Then

                txtUserName.Text = dtUser.Rows(0)("sLoginName")

                If txtUserName.Text <> "" Then
                    txtPassword.Text = oEncryption.DecryptFromBase64String(dtUser.Rows(0)("sPassword"), constEncryptDecryptKey)
                End If

            End If
        End If

        If IsNothing(oEncryption) = False Then
            oEncryption.Dispose()
            oEncryption = Nothing
        End If

        If IsNothing(dtUser) = False Then
            dtUser.Dispose()
            dtUser = Nothing
        End If

        If IsNothing(objLogin) = False Then
            objLogin.Dispose()
            objLogin = Nothing
        End If

    End Sub

    'code start by nilesh on 20110226 for case GLO2010-0008612
    'check process for another instance as per session
    Private Sub CheckAnotherInstance()
        Try
            ' Dim _currentSessionID As Int32 = System.Diagnostics.Process.GetCurrentProcess().SessionId
            Dim _currentProcName As String = System.Diagnostics.Process.GetCurrentProcess().ProcessName
            Dim _currentProcessID As Int32 = System.Diagnostics.Process.GetCurrentProcess().Id

            Dim _Process() As System.Diagnostics.Process = Process.GetProcessesByName(_currentProcName)

            If _Process.Length > 1 Then
                For Each _proc As System.Diagnostics.Process In _Process
                    If _proc.SessionId = gloWord.LoadAndCloseWord.CurrentSessionID And _proc.Id <> _currentProcessID And _proc.ProcessName = _currentProcName Then
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




    Private Function CheckLogin() As Boolean

        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        Dim oDataReader As SqlDataReader = Nothing

        Dim numdigits, numletters, numspchars, numcapletters, numminlength, numdays As Integer

        AccessFlag = False

        Try

            If Trim(txtUserName.Text) = "" Then
                MessageBox.Show("User Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtUserName.Focus()
                lblPleaseWait.Visible = False
                txtPassword.Clear()
                AccessFlag = True
                Return False
            End If
            If Trim(txtPassword.Text) = "" Then
                txtPassword.Clear()
                MessageBox.Show("Password must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPassword.Focus()
                lblPleaseWait.Visible = False
                AccessFlag = True
                Return False
            End If
            conn = New SqlConnection(GetConnectionString)
            conn.Open()

            _strSQL = "select ExpCapitalLetters,ExpNoOfLetters,ExpNoOfDigits,ExpNoOfSpecChars,ExpPwdLength,ExpTimeFrameinDays from PwdSettings"
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
                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            _strSQL = "select  IsPasswordReset, sLoginName from User_MST where sLoginName = '" & Trim(txtUserName.Text).Replace("'", "''") & "'"
            cmd = New SqlClient.SqlCommand(_strSQL, conn)

            '  blnResetPwdFlag = cmd.ExecuteScalar

            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so check for null value
                        If Not IsDBNull(oDataReader.Item("IsPasswordReset")) Then
                            'if not null then set the value of flag 
                            blnResetPwdFlag = oDataReader.Item("IsPasswordReset")
                        Else
                            'if the value is null then set the flag to false
                            blnResetPwdFlag = False
                        End If
                        gstrLoginName = CStr(oDataReader.Item("sLoginName")).Trim
                    End While
                End If
                oDataReader.Close()
                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Dim objLogin As New clsLogin
            Dim objEncryption As New clsencryption
            Dim strPassword As String
            strPassword = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
            objEncryption = Nothing
            If objLogin.IsValidLogin(gstrLoginName, strPassword) = False Then

                blnResetPwdFlag = IsPasswordResetted()

                If blnResetPwdFlag = True Then
                    MessageBox.Show("Your Password has been reset by the administrator. Please contact the administrator to get the new password.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPassword.Text = ""
                    lblPleaseWait.Visible = False
                    txtPassword.Focus()
                    objLogin.Dispose()
                    objLogin = Nothing
                    Return False
                End If

                MessageBox.Show(gstrUnauthLoginBanner, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, gstrUnauthLoginBanner, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, True)
                txtPassword.Clear()
                lblPleaseWait.Visible = False
                ''to stop the waiting cursor bring back to it's original position
                Me.Cursor = Cursors.Default
                txtUserName.Focus()
                objLogin.Dispose()
                objLogin = Nothing
                Return False
            End If

            If objLogin.IsAccessPermission(gstrLoginName) = False Then
                MessageBox.Show("Access Denied." & vbCrLf & "Please contact the administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtUserName.Focus()
                lblPleaseWait.Visible = False
                objLogin.Dispose()
                objLogin = Nothing
                AccessFlag = True
                Return False
            End If

            If IsAdmin(gstrLoginName) = False Then
                If blnResetPwdFlag = False Then
                    If Not ValidatePassword(txtPassword.Text.Trim, numminlength, numcapletters, 0, numdigits, numspchars, Nothing, numletters, numdays) = True Then
                        MessageBox.Show("Your password do not meet the password complexity. So, you need to change your password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'Reset The count of Login Attempt
                        nNoOfLoginAttempt = 0
                        nNoOfLoginAttempt = nNoOfLoginAttempt - 1
                        txtUserName.Focus()
                        Dim frm As New frmChangePassword(gstrLoginName)
                        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        frm.Dispose()
                        frm = Nothing
                        objLogin.Dispose()
                        objLogin = Nothing
                        CheckLogin = Nothing
                        If Not IsNothing(cmd) Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If
                        If Not IsNothing(conn) Then
                            conn.Dispose()
                            conn = Nothing
                        End If
                        Exit Function
                    End If
                End If
            End If

            gblnIsAdmin = objLogin.IsLoginUserAdmin(gstrLoginName, True) = True

            'Added Code for Audit LOG Enhancement
            'Dim intLoginSessionID As Long
            Dim _isLogin As Boolean = True
            gintLoginSessionID = gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(gstrLoginName, True, gstrClientMachineName, gloAuditTrail.SoftwareComponent.gloEMR.ToString(), gnClinicID, _isLogin)
            objLogin.UpdateLoginStatus(gstrLoginName, True, gstrClientMachineName, gintLoginSessionID)
            _isLogin = Nothing

            'Retrieve Default Login Provider
            gstrLoginProviderName = objLogin.DefaultLoginProvider(gstrLoginName, gnLoginProviderID).Trim
            objLogin.Dispose()
            objLogin = Nothing
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(conn) Then ''added if condition for bugid 70911
                conn.Close()
            End If
            'Code opti
            If Not IsNothing(oDataReader) Then

                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Private Function IsPasswordResetted() As Boolean
        Dim blnResetPwdFlag As Boolean = False
        Dim _strSQL As String = ""
        Dim oDataReader As SqlDataReader = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection

        Try
            conn.ConnectionString = GetConnectionString()

            _strSQL = "select IsPasswordReset ,sLoginName, nUserID from User_MST where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"
            cmd = New SqlClient.SqlCommand(_strSQL, conn)
            conn.Open()

            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so check for null value
                        If Not IsDBNull(oDataReader.Item("IsPasswordReset")) Then
                            'if not null then set the value of flag 
                            blnResetPwdFlag = oDataReader.Item("IsPasswordReset")
                        Else
                            'if the value is null then set the flag to false
                            blnResetPwdFlag = False
                        End If
                        '''' 
                        gstrLoginName = oDataReader.Item("sLoginName")
                        gnLoginID = oDataReader.Item("nUserID")
                    End While
                End If
                oDataReader.Close()
            End If

            Return blnResetPwdFlag

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            'Code opti
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function
    '-----

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
                Return False
            End If

            ' Check for minimum number of occurrences.
            If upper.Matches(pwd).Count < numUpper Then
                Return False
            End If

            If lower.Matches(pwd).Count < numLower Then
                Return False
            End If

            If number.Matches(pwd).Count < numNumbers Then
                Return False
            End If

            If special.Matches(pwd).Count < numSpecial Then
                Return False
            End If

            If UCase(pwd) = UCase(gstrLoginName) Then
                Return False
            End If

            If letters.Matches(pwd).Count < numLetters Then
                Return False
            End If

            ' Passed all checks.
            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            End
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub LoginClick(Optional SingleSignON As Boolean = False) 'Optional AutoUserName As String = "", Optional AutoPassword As String = ""

        Dim blnSingleSignON As Boolean

        ''setting the variable 
        If cmbDatabaseName.Visible Then
            If (IsNothing(dtDatabase) = False) Then


                If cmbDatabaseName.Text <> "" And dtDatabase.Rows.Count > 0 Then
                    For iRow As Integer = 0 To dtDatabase.Rows.Count - 1
                        If dtDatabase.Rows(iRow)("nDBConnectionId") = cmbDatabaseName.SelectedValue Then
                            gstrDatabaseName = dtDatabase.Rows(iRow)("sDatabaseName")

                            gstrSQLServerName = dtDatabase.Rows(iRow)("sServerName")
                            gloEmdeonCommon.mdlGeneral.gstrSQLServerName = dtDatabase.Rows(iRow)("sServerName")

                            gblnSQLAuthentication = True
                            gloEmdeonCommon.mdlGeneral.gblnSQLAuthentication = True
                            gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True

                            gstrSQLUserEMR = dtDatabase.Rows(iRow)("sSqlUserName")
                            gloEmdeonCommon.mdlGeneral.gstrSQLUserEMR = dtDatabase.Rows(iRow)("sSqlUserName")

                            Dim oEncryption As New clsencryption
                            gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(dtDatabase.Rows(iRow)("sSqlPassword"), constEncryptDecryptKey)
                            gloEmdeonCommon.mdlGeneral.gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(dtDatabase.Rows(iRow)("sSqlPassword"), constEncryptDecryptKey)
                            oEncryption = Nothing
                            Dim oDBLayer As New gloDatabaseLayer.DBLayer(GetConnectionString())

                            ''to check the connection has been done or not.
                            If oDBLayer.CheckConnection() = False Then
                                MessageBox.Show("Selected database/SQL server is not valid", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                oDBLayer.Dispose()
                                oDBLayer = Nothing
                                Exit Sub
                            End If

                            oDBLayer.Dispose()
                            oDBLayer = Nothing


                            If IsNothing(dvSettings) Then
                                dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView
                            Else
                                dvSettings.Table.Rows.Clear()
                                dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView
                            End If

                            'Single Sign ON Start

                            If SingleSignON = True Then
                                dvSettings.RowFilter = "sName = 'EnableSingleSignON'"

                                If dvSettings.Count > 0 Then

                                    blnSingleSignON = dvSettings(0)("sValue")

                                    If blnSingleSignON = True Then
                                        SingleSignONUserNamePassword()
                                        If txtUserName.Text = "" OrElse txtPassword.Text = "" Then
                                            Exit Sub
                                        End If
                                    Else
                                        Exit Sub
                                    End If
                                Else
                                    Exit Sub
                                End If
                            End If


                            'Single Sign ON End
                            Dim objLogin As New clsLogin
                            If objLogin.IsClientAccess(gstrClientMachineName) = False Then
                                MessageBox.Show("This machine does not have rights to access gloEMR system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                objLogin.Dispose()
                                objLogin = Nothing
                                Exit Sub
                            End If

                            'Get EMR Settings  ''Fill EMR Settings
                            If IsNothing(dvSettings) Then
                                dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView
                                'dv_Settings = dvSettings
                            Else
                                dvSettings.Table.Rows.Clear()
                                dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView
                                'dv_Settings = dvSettings
                            End If
                            '-----
                            objLogin.Dispose()
                            objLogin = Nothing




                            Exit For
                        End If
                    Next
                End If
            End If
        End If

        If gstrDatabaseName = "" Or gstrSQLServerName = "" Then
            Exit Sub
        End If

        'Bug #82275: CR0000361: Patient color status based on color
        'Read the Enable Static Color Setting 
        If IsNothing(dvSettings) Then
            dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView
        End If

        If blnSingleSignON = False Then

            If SingleSignON = True Then
                dvSettings.RowFilter = "sName = 'EnableSingleSignON'"

                If dvSettings.Count > 0 Then

                    blnSingleSignON = dvSettings(0)("sValue")

                    If blnSingleSignON = True Then
                        SingleSignONUserNamePassword()
                        If txtUserName.Text = "" OrElse txtPassword.Text = "" Then
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If

            End If

        End If

        Me.Cursor = Cursors.WaitCursor
        Dim blnResetPwdFlag As Boolean = False
        Dim blnIsAccessDenied As Boolean = False
        Dim _strSQL As String = ""
        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

        gstrgloEMRStartupPath = System.Windows.Forms.Application.StartupPath
        ReadVersion()
        ReadMarketingVersion()

        '10-Dec-13 Aniket: Putting version check
        If Application.ProductVersion <> gstrVersion Then
            MessageBox.Show("Application version mismatch. Please contact your administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblPleaseWait.Visible = False
            Cursor = Cursors.Default
            Exit Sub
        End If

        'Change implemented to resolve AUS issue 0
        UpdateLog("GetEMRSettings() : START")
        ScanClientInterface("gloEMR", System.Environment.MachineName)
        GetEMRSettings()
        UpdateLog("GetEMRSettings() : END")

        gstrMessageBoxCaption = "gloEMR"
        appSettings("DataBaseConnectionString") = GetConnectionString()
        appSettings("SQLServerName") = gstrSQLServerName
        appSettings("DatabaseName") = gstrDatabaseName
        appSettings("SQLLoginName") = gstrSQLUserEMR
        appSettings("SQLPassword") = gstrSQLPasswordEMR
        appSettings("WindowAuthentication") = Not gblnSQLAuthentication
        appSettings("MessageBOXCaption") = gstrMessageBoxCaption
        'appSettings("PMConnectionString") = getPMconnectionstring() 'Not In Use
        appSettings("Add Patient To gloPM") = getAddPatienttoPMsetting()
        appSettings("UseDefaultPrinter") = Convert.ToString(gblnUseDefaultPrinter)

        'line added by dipak 20091008 to set value for Country in app setting
        appSettings("Country") = gstrCountry
        'set value for whether its internet fax or not to appsettings
        appSettings("Internet Fax") = Convert.ToString(gblnInternetFax)
        appSettings("ClinicID") = gnClinicID


        'Set gloPM global variables [Used in common module]
        gloGlobal.gloPMGlobal.DatabaseConnectionString = GetConnectionString()
        gloGlobal.gloPMGlobal.MessageBoxCaption = gstrMessageBoxCaption
        gloGlobal.gloPMGlobal.ClinicID = gnClinicID
        gloGlobal.gloPMGlobal.IsAccountsOn = GetPatientAccountFeatureSetting()

        'Bug #82275: CR0000361: Patient color status based on color
        'Read the Enable Static Color Setting 
        If IsNothing(dvSettings) Then
            dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView
        End If

        dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'Enable Static Color'"
        If dvSettings.Count > 0 Then
            Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue"))
            If sValue <> "" Then
                _isEnableStaticColor = sValue
            End If
        End If

        'Added to Read CCDAAutoDelete setting at time of login
        dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'CCDAAutoDelete'"
        If dvSettings.Count > 0 Then
            Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue"))
            If sValue <> "" Then
                _isAutoDeleteCCDAFiles = sValue
            End If
        End If



        Dim oResult As Object = Nothing
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        ogloSettings.GetSetting("UseSitePrefix", oResult)
        Dim _UseSitePrefix As Int32 = 0
        If oResult IsNot Nothing AndAlso oResult.ToString() <> "" Then
            _UseSitePrefix = Convert.ToInt32(oResult)
        End If
        If _UseSitePrefix <> 0 Then

            Dim dtPrefixDTL As DataTable = Nothing
            Dim oPatient As New gloPatient.gloPatient(GetConnectionString().ToString())
            dtPrefixDTL = oPatient.GetPrefix()
            If IsNothing(dtPrefixDTL) = False Then
                If dtPrefixDTL.Rows.Count > 0 Then
                    appSettings("PatientPrefix") = Convert.ToString(dtPrefixDTL.Rows(0)("sPreFix"))
                Else
                    MessageBox.Show("Site Prefix is not set up for this site. Please contact your administrator to set up the site prefix before using the application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Cursor = Cursors.Default
                    If IsNothing(dtPrefixDTL) = False Then
                        dtPrefixDTL.Dispose()
                    End If
                    If IsNothing(oPatient) = False Then
                        oPatient.Dispose()
                    End If

                    Exit Sub
                End If
            Else
                MessageBox.Show("Site Prefix is not set up for this site. Please contact your administrator to set up the site prefix before using the application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Cursor = Cursors.Default
                If IsNothing(dtPrefixDTL) = False Then
                    dtPrefixDTL.Dispose()
                End If
                If IsNothing(oPatient) = False Then
                    oPatient.Dispose()
                End If

                Exit Sub
            End If
            If IsNothing(dtPrefixDTL) = False Then
                dtPrefixDTL.Dispose()
            End If
            If IsNothing(oPatient) = False Then
                oPatient.Dispose()
            End If
        Else
            appSettings("PatientPrefix") = ""
        End If
        gloAuditTrail.gloAuditTrail.MessageBoxCaption = gstrMessageBoxCaption
        'flag for checking whether the audit trails are allowed for the logged in user or not 
        Dim blnIsAuditTrailEnabledFlag As Boolean = False

        Dim conn As New SqlConnection(GetConnectionString)
        Try

            Dim cmd As SqlCommand = Nothing
            Dim oDataReader As SqlDataReader = Nothing
            conn.Open()
            _strSQL = "select nAccessDenied from User_MST where sLoginName = '" & txtUserName.Text.Trim.Replace("'", "''") & "'"
            cmd = New SqlClient.SqlCommand(_strSQL, conn)
            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so check for null value

                        If Not IsDBNull(oDataReader.Item("nAccessDenied")) Then
                            'if not null then set the value of flag 
                            blnIsAccessDenied = oDataReader.Item("nAccessDenied")
                        Else
                            'if the value is null then set the flag to false
                            blnIsAccessDenied = False
                        End If
                    End While
                End If
                oDataReader.Close()
                oDataReader.Dispose()
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            'conn.Close()
            'conn.Dispose()
            'conn = Nothing

            ''Code opti.
            Dim sValue As String = String.Empty
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " ='UNAUTHENTICATEDLOGINBANNER'"
            If dvSettings.Count > 0 Then
                sValue = Convert.ToString(dvSettings(0).Row("sValue"))
            End If

            If Not IsDBNull(sValue) Then
                If sValue <> "" Then
                    'if not null then set the value of flag 
                    gstrUnauthLoginBanner = sValue
                Else
                    gstrUnauthLoginBanner = "Invalid Username/Password."
                End If
            Else
                'if the value is null then set Default Message
                gstrUnauthLoginBanner = "Invalid Username/Password."
            End If
            dvSettings.RowFilter = Nothing
            sValue = String.Empty
            '--x--


            Select Case String.Compare(gstrLoginName, Trim(txtUserName.Text), True)
                Case -1, 1
                    gstrLoginName = Trim(txtUserName.Text)
                    nNoOfLoginAttempt = 0
                Case Else

            End Select


            If blnIsAccessDenied = False Then

                If CheckLogin() = True Then
                    CheckVoiceEnabled()
                    gstrLoginPassword = Trim(txtPassword.Text)

                    blnResetPwdFlag = IsPasswordResetted()

                    '' Validate Provider License
                    Dim smessage As String = ""
                    smessage = MyBase.ValidateLogin(gnLoginProviderID, GetConnectionString())
                    If Trim(smessage) <> "" Then
                        If MessageBox.Show(smessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                            System.Environment.[Exit](0)
                        End If
                    End If
                    smessage = ""
                    '' Validate Provider License

                    Me.Cursor = Cursors.WaitCursor
                    If blnResetPwdFlag = True Then
                        Dim frm As New frmChangePassword(gstrLoginName)   ''txtUserName.Text.Trim
                        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        frm.Dispose()
                        frm = Nothing
                        txtUserName.Text = "" ''added to clear username and password at logout
                        txtPassword.Text = ""
                        If Not IsNothing(conn) Then
                            conn.Close()
                            conn.Dispose()
                            conn = Nothing
                        End If
                        Exit Sub
                    End If

                    gstrLoginTime = CType(Format(Date.Now, "Medium Time"), String)

                    '' To get the cosign right enabled for the user logged in
                    GetCoSign(gstrLoginName)
                    ''To get the Security user 
                    GetSecurityUser(gstrLoginName)

                    'Add Login ID to Application Configuration File
                    appSettings("UserID") = gnLoginID


                    'Add ClinicID & Clinic Name to Application Configuration File
                    appSettings("ClinicID") = gnClinicID
                    appSettings("ClinicName") = gstrClinicName
                    appSettings("StartupPath") = gloSettings.FolderSettings.AppTempFolderPath ''Application.StartupPath & gstrgloTempFolder

                    'Add Login Provider ID to Application configue File
                    Dim objLogin As New clsLogin


                    gstrLoginProviderName = objLogin.DefaultLoginProvider(gstrLoginName, gnLoginProviderID).Trim
                    gnSelectedProviderID = gnLoginProviderID
                    appSettings("ProviderID") = objLogin.GetLoginProviderID(gstrLoginName).ToString()
                    objLogin.Dispose()
                    objLogin = Nothing
                    appSettings("UserName") = Convert.ToString(gstrLoginName)

                    gloGlobal.gloPMGlobal.LoginProviderID = gnLoginProviderID
                    gloGlobal.gloPMGlobal.UserID = gnLoginID
                    gloGlobal.gloPMGlobal.UserName = Convert.ToString(gstrLoginName)

                    'Bug #76377: 00000806: LOGIN NAME LIQUID LINK IN ORDERS AND RESULTS
                    gloEMRGeneralLibrary.gloGeneral.globalSecurity.gstrLoginName = Convert.ToString(gstrLoginName)
                    gloEMRGeneralLibrary.gloGeneral.globalSecurity.gnUserID = gnLoginID

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.LoginScreen, gloAuditTrail.ActivityType.Login, gstrLoginName & " successfully logged in", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Me.Cursor = Cursors.WaitCursor

                    'Dim con As New SqlConnection(gloEMR.mdlGeneral.GetConnectionString())
                    Dim query As String = " SELECT ISNULL(sSpeciality,'') AS Speciality from User_MST where sLoginName = '" & txtUserName.Text.Replace("'", "''") & "'"
                    Dim cmd1 As New SqlCommand(query, conn)
                    Dim adp As New SqlDataAdapter(cmd1)
                    Dim dtProvider As New DataTable
                    adp.Fill(dtProvider)
                    If Not IsNothing(dtProvider) Then
                        If dtProvider.Rows.Count > 0 Then
                            gstrSpeciality = dtProvider.Rows(0)("Speciality").ToString()
                        Else
                            gstrSpeciality = ""
                        End If
                    Else
                        gstrSpeciality = ""
                    End If
                    cmd1.Parameters.Clear()
                    cmd1.Dispose()
                    cmd1 = Nothing
                    adp.Dispose()
                    adp = Nothing
                    Dim frmgloEMRMain As New MainMenu
                    frmgloEMRMain.SingleSignON = blnSingleSignON
                    frmgloEMRMain.AllSettings = dvSettings

                    Me.Hide()
                    frmgloEMRMain.Opacity = 0

                    'If Not IsNothing(dvSettings) Then
                    '    dvSettings.Dispose()
                    '    dvSettings = Nothing
                    'End If

                    'SLR: Don't free it is part of datasource?
                    'If Not IsNothing(dtDatabase) Then
                    '    dtDatabase.Dispose()
                    '    dtDatabase = Nothing
                    'End If
                    If Not IsNothing(dtProvider) Then
                        dtProvider.Dispose()
                        dtProvider = Nothing
                    End If


                    frmgloEMRMain.Hide()
                    frmgloEMRMain.ShowDialog(IIf(IsNothing(frmgloEMRMain.Parent), Me, frmgloEMRMain.Parent))
                    frmgloEMRMain.Dispose()
                    frmgloEMRMain = Nothing
                    txtPassword.Text = ""
                    txtUserName.Text = ""
                    txtUserName.Focus()
                Else

                    If AccessFlag = False Then
                        nNoOfLoginAttempt = nNoOfLoginAttempt + 1

                        If nNoOfLoginAttempt >= gintNoOfAttempts Then
                            Me.Cursor = Cursors.WaitCursor
                            If IsAdmin(gstrLoginName) = False Then
                                Me.Hide()
                                If SetAccessDeniedFlag() = True Then
                                    MsgBox("Access denied. Please contact your administrator.", MsgBoxStyle.Information)
                                    txtPassword.Clear()
                                    txtUserName.Focus()
                                    'End
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                MsgBox("Access denied. Please contact your administrator.", MsgBoxStyle.Information)
                txtPassword.Clear()
                txtUserName.Focus()
                'End
            End If
            'conn.Close()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '
            'If Not IsNothing(oDataReader) Then
            '    oDataReader.Dispose()
            '    oDataReader = Nothing
            'End If
            'If Not IsNothing(cmd) Then
            '    cmd.Dispose()
            '    cmd = Nothing
            'End If
            lblPleaseWait.Visible = False
            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            lblPleaseWait.Visible = True
            Application.DoEvents()
            Loginclick()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Function IsAdmin(ByVal LoginName) As Boolean
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim oDataReader As SqlDataReader = Nothing
        Dim blnIsAdministrator As Boolean = False
        Dim _strSQL As String = String.Empty
        Try


            _strSQL = "select nAdministrator from User_MST where sLoginName = '" & txtUserName.Text.Trim.Replace("'", "''") & "'"
            cmd = New SqlClient.SqlCommand(_strSQL, conn)
            conn.Open()
            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so check for null value
                        If Not IsDBNull(oDataReader.Item("nAdministrator")) Then
                            'if not null then set the value of flag 
                            blnIsAdministrator = oDataReader.Item("nAdministrator")
                        Else
                            'if the value is null then set the flag to false
                            blnIsAdministrator = False
                        End If
                    End While
                End If
                oDataReader.Close()
            End If
            conn.Close()
            Return blnIsAdministrator
        Catch ex As Exception
            Return blnIsAdministrator
        Finally
            If Not IsNothing(oDataReader) Then
                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Public Function SetAccessDeniedFlag() As Boolean
        'sets the AccessDenied flag to true for the user
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        ' Dim oDataReader As SqlDataReader
        Dim _strSQL As String = ""
        Try
            conn.Open()
            _strSQL = "update User_MST set nAccessDenied = 1 where sLoginName ='" & txtUserName.Text.Trim & "'"
            cmd = New SqlCommand(_strSQL, conn)
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conn.Close()
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Private Sub CheckVoiceEnabled()
        Dim dtClients As DataTable
        Dim objClientMachine As New clsLogin
        dtClients = objClientMachine.ScanClientMachine(gstrClientMachineName)
        objClientMachine.Dispose()
        objClientMachine = Nothing
        If Not IsNothing(dtClients) Then
            If dtClients.Rows(0).Item("VoiceEnabled") = 0 Then
                gblnVoiceEnabled = False
            Else
                gblnVoiceEnabled = True
            End If

            If dtClients.Rows(0).Item("ScanEnabled") = 0 Then
                gblnScanEnabled = False
            Else
                gblnScanEnabled = True
            End If
            dtClients.Dispose()
            dtClients = Nothing
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFileOpen As New OpenFileDialog
        If oFileOpen.ShowDialog() = Windows.Forms.DialogResult.OK Then
        End If

        oFileOpen = Nothing
    End Sub

    Private Sub frmSplash_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If Not IsNothing(dvSettings) Then
            dvSettings.Dispose()
            dvSettings = Nothing
        End If

    End Sub

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''======= Make our bitmap region for the form============
        'bAfterLoad = True
        'gloGlobal.BitmapRegion.CreateControlRegion(Me, bmpFrmBack)
        ''========================================================
        lblPleaseWait.Visible = False
        lblCopyrghTag.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain
        lblCopyrghTag.Refresh()
        Label2.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightSub
        Label2.Refresh()
        gstrgloEMRStartupPath = System.Windows.Forms.Application.StartupPath
        Application.DoEvents()
        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        If IsNothing(gloRegistrySetting.GetRegistryValue("EnableApplicationLogs")) = True Then
            gloRegistrySetting.SetRegistryValue("EnableApplicationLogs", False)
        End If

        If IsNothing(gloRegistrySetting.GetRegistryValue("EnableErrorLogs")) = True Then
            gloRegistrySetting.SetRegistryValue("EnableErrorLogs", True)
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("EnableApplicationLogs")) = False Then

            gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = gloRegistrySetting.GetRegistryValue("EnableApplicationLogs")
        Else
            ' gblnEnableApplicationLogs = False
            gloAuditTrail.gloAuditTrail.gblnEnableApplicationLogs = False
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("EnableErrorLogs")) = False Then

            gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = gloRegistrySetting.GetRegistryValue("EnableErrorLogs")
        Else
            ' gblnEnableExceptionLogs = False
            gloAuditTrail.gloAuditTrail.gblnEnableExceptionLogs = False
        End If
        Timer1.Enabled = True
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Private Sub GetCoSign(ByVal username As String)
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = String.Empty

        Try
            conn.Open()

            _strSQL = "select bCoSign from User_MST where sLoginName ='" & username.Replace("'", "''") & "'"
            cmd = New SqlCommand(_strSQL, conn)
            gblnCoSignFlag = Convert.ToBoolean(cmd.ExecuteScalar)

            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'code opti
            gblnCoSignFlag = False
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                'Dim erg As System.EventArgs
                lblPleaseWait.Visible = True
                Application.DoEvents()
                LoginClick()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtUserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                'Dim erg As System.EventArgs
                lblPleaseWait.Visible = True
                Application.DoEvents()
                LoginClick()
            End If
        Catch ex As Exception
        End Try
    End Sub

    'ClinicID Implementation Test Code. 
    Private Sub Fill_Clinic()
        Dim oCon As New SqlConnection(GetConnectionString())
        Dim oCmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strQuery As String = String.Empty
        Dim dtClinics As New DataTable()
        Try
            '' Get the Clinic Information
            strQuery = "select ISNULL(nClinicID,0) AS nClinicID ,ISNULL(sClinicName,'') AS sClinicName, ISNULL(sState, '') AS sState, ISNULL(sAddress1, '') AS sAddress, ISNULL(sCity, '') AS sCity, ISNULL(sZip, '') AS sZip, ISNULL(sPhoneNo, '') AS sPhoneNo, ISNULL(sFax, '') AS sFax from Clinic_MST"
            oCmd.Connection = oCon
            oCmd.CommandText = strQuery
            da.SelectCommand = oCmd
            da.Fill(dtClinics)
            If dtClinics IsNot Nothing AndAlso dtClinics.Rows.Count > 0 Then
                gnClinicID = dtClinics.Rows(0)("nClinicID")
                gstrClinicName = dtClinics.Rows(0)("sClinicName")
                gstrClinicAddress = dtClinics.Rows(0)("sAddress")
                gstrClinicState = dtClinics.Rows(0)("sState")
                gstrClinicCity = dtClinics.Rows(0)("sCity")
                gstrClinicZip = dtClinics.Rows(0)("sZip")
                gstrClinicPhone = dtClinics.Rows(0)("sPhoneNo")
                gstrClinicFax = dtClinics.Rows(0)("sFax")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally
            'Code opti
            If Not IsNothing(dtClinics) Then
                dtClinics.Dispose()
                dtClinics = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(oCmd) Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing
            End If
            If Not IsNothing(oCon) Then
                oCon.Dispose()
                oCon = Nothing
            End If
        End Try
    End Sub

    ''Read Setting to Show Coded History code,description or both from Admin 
    Private Sub ShowCodeOrDescription()
        Try
            'Dim conn As New SqlConnection
            'Dim cmd As New SqlCommand
            'Dim _strSQL As String = "SELECT ISNULL(sSettingsValue,'') AS  sSettingsValue FROM Settings WHERE sSettingsName = 'SHOWCODEDHISTORY'"
            'conn.ConnectionString = GetConnectionString()
            'conn.Open()
            'cmd = New SqlCommand(_strSQL, conn)

            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'SHOWCODEDHISTORY'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                gsrtrShowCodedHistory = sValue
            Else
                gsrtrShowCodedHistory = ""
            End If
            'conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dvSettings.RowFilter = Nothing
        End Try
    End Sub

    ''Read  Application version
    Private Sub ReadVersion()
        Try
            'Dim conn As New SqlConnection
            'Dim cmd As New SqlCommand
            'Dim _strSQL As String = "SELECT ISNULL(sSettingsValue,'') AS  sSettingsValue FROM Settings WHERE sSettingsName = 'Application Version'"
            'conn.ConnectionString = GetConnectionString()
            'conn.Open()
            'cmd = New SqlCommand(_strSQL, conn)
            '_strSQL = cmd.ExecuteScalar & ""

            ''Code opti.
            'Get EMR Settings  ''Fill EMR Settings
            If (IsNothing(dvSettings) = False) Then
                dvSettings.Dispose()
                dvSettings = Nothing
            End If
            dvSettings = Get_All_EMRSettings(GetConnectionString()).Tables("SettingsData").DefaultView
            'dv_Settings = dvSettings
            '-----

            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'Application Version'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue <> "" Then
                    gstrVersion = sValue
                Else
                    gstrVersion = "5.0.5.0"
                End If
            Else
                gstrVersion = "5.0.5.0"
            End If
            dvSettings.RowFilter = Nothing
            'conn.Close()
        Catch ex As Exception
        End Try
    End Sub





    Private Sub ReadMarketingVersion()

        '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
        ' Dim objGlobalMisc As New gloGlobal.clsMISC

        Try


            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'Marketing Version'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If sValue <> "" Then
                    gstrMktngVersion = sValue
                Else
                    gstrMktngVersion = "RTM1"
                End If
            Else
                gstrMktngVersion = "RTM1"
            End If

            lbl_mktngversion.Text = "" & gstrMktngVersion

            '09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
            lbl_mktngversion.Text = gloGlobal.clsMISC.GetMajorVersion(Application.ProductVersion)

            'objGlobalMisc = Nothing

        Catch ex As Exception

        Finally
            dvSettings.RowFilter = Nothing
        End Try

    End Sub

    'Get connection string for gloPM
    Private Function getPMconnectionstring() As String
        'Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        'oDB.Connect(False)
        'Dim _Connectionstring As String = ""
        'Dim _sqlQuery As String = "SELECT ISNULL(sSettingsValue,'0') FROM SETTINGS WHERE sSettingsName = 'PMConnectionString' "
        '_Connectionstring = Convert.ToString(oDB.ExecuteScalar_Query(_sqlQuery))
        'oDB.Disconnect()
        'Dim objCon As New SqlConnection
        'Try
        '    If _Connectionstring <> "" Then  ''Condition Added by Mayuri:20100129:to fix speed issue
        '        objCon.ConnectionString = _Connectionstring
        '        objCon.Open()
        '        objCon.Close()
        '        objCon = Nothing
        '    End If
        ''Code opti.

        Dim _Connectionstring As String = String.Empty
        Try
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'PMConnectionString'"
            If dvSettings.Count > 0 Then
                _Connectionstring = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If _Connectionstring <> "" Then
                    Dim objCon As New SqlConnection
                    objCon.ConnectionString = _Connectionstring
                    objCon.Open()
                    objCon.Close()
                    objCon.Dispose()
                    objCon = Nothing
                End If
            End If
        Catch ex As Exception
            'objCon = Nothing
            _Connectionstring = String.Empty
        Finally
            'If oDB IsNot Nothing Then
            '    oDB.Dispose()
            '    oDB = Nothing
            'End If
            dvSettings.RowFilter = Nothing
        End Try
        Return _Connectionstring
    End Function

    Private Function getAddPatienttoPMsetting() As String
        'Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        'oDB.Connect(False)
        Dim _AddPatientToPM As String = "0"

        'Dim _sqlQuery As String = "SELECT ISNULL(sSettingsValue,'0') as sSettingsValue FROM SETTINGS WHERE sSettingsName = 'AddPatientToPM' "

        '_AddPatientToPM = oDB.ExecuteScalar_Query(_sqlQuery)

        'oDB.Disconnect()
        'oDB.Dispose()
        'oDB = Nothing

        ''Code opti.
        dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'AddPatientToPM'"
        If dvSettings.Count > 0 Then
            _AddPatientToPM = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
            If Not IsNothing(_AddPatientToPM) AndAlso Convert.ToString(_AddPatientToPM) <> "" Then
                _AddPatientToPM = _AddPatientToPM
            Else
                _AddPatientToPM = "0"
            End If

            If _AddPatientToPM = "1" Then
                _AddPatientToPM = "True"
            Else
                _AddPatientToPM = "False"
            End If
        End If
        dvSettings.RowFilter = Nothing
        Return _AddPatientToPM
    End Function

    Public Function GetLastVersion(ByVal aModuleName As String) As String
        Dim strLen As Integer
        Dim count As Integer = 0
        Dim strfileVer As String = String.Empty
        Dim fileVer As FileVersionInfo = FileVersionInfo.GetVersionInfo(gstrgloEMRStartupPath & "\" & aModuleName)
        Dim strVer As String = fileVer.FileVersion().ToString()
        strLen = strVer.Length
        For i As Integer = 0 To strLen - 1

            'CHECK IF SEPARATOR IS '.'
            If strVer(i) = "." Then
                count = count + 1
            End If

            ' JUMP OUT CONDITION
            If count = 3 Or count > 3 Then
                Exit For
            End If

            'append the character read
            strfileVer = strfileVer + strVer(i)
        Next
        Return strfileVer
    End Function

    ''' <summary>
    ''' Dhruv 20091202 
    ''' To fill the databaseName from the gloServices Database
    ''' </summary>
 

#Region "To fill the database in the combobox"
    Private Sub FillServiceDatabases()
        ''Local variable declaration
        Dim con As SqlConnection = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim sqlCommand As SqlCommand = Nothing

        'Dim _Query As String

        Try
            ''Setting the connection string
            con = New SqlConnection(GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord))

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

                If (IsNothing(dtDatabase)) Then
                    dtDatabase = New DataTable
                End If
                da.Fill(dtDatabase)


                If Not IsNothing(dtDatabase) Then
                    If dtDatabase.Rows.Count > 0 Then
                        cmbDatabaseName.DataSource = dtDatabase
                        cmbDatabaseName.DisplayMember = "sDatabaseName"
                        cmbDatabaseName.ValueMember = "nDBConnectionId"
                        cmbDatabaseName.SelectedItem = Nothing

                        '' CHECK FOR DEFAULT DATABASE IN COMBO LIST ''
                        For iRow As Integer = 0 To dtDatabase.Rows.Count - 1
                            If Convert.ToBoolean(dtDatabase.Rows(iRow)("bEnabled")) = True Then
                                cmbDatabaseName.Text = dtDatabase.Rows(iRow)("sDatabaseName").ToString()
                                Exit For
                            End If
                        Next
                        cmbDatabaseName.Visible = True
                        lblDataBase.Visible = True
                    Else
                        cmbDatabaseName.Visible = False
                        ''Panel visible false
                        pnlDataBase.Visible = False
                        lblDataBase.Visible = False
                    End If
                Else
                    cmbDatabaseName.Visible = False
                    ''Panel visible false
                    pnlDataBase.Visible = False
                    lblDataBase.Visible = False
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            'MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question)
            cmbDatabaseName.Visible = False
            ''Panel visible false
            pnlDataBase.Visible = False
            lblDataBase.Visible = False
        Finally

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            If Not IsNothing(sqlCommand) Then
                sqlCommand.Parameters.Clear()
                sqlCommand.Dispose()
                sqlCommand = Nothing
            End If

            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Sub


#End Region

    Public Sub GetPediatricSetting()
        'Dim conn As New SqlConnection(GetConnectionString)
        'Dim cmd As SqlCommand
        'Dim _strSQL As String = ""
        Try
            '    _strSQL = "select ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='PEDIATRICS'"
            '    If Not IsNothing(conn) Then
            '        If conn.State = ConnectionState.Closed Then
            '            conn.Open()
            '        End If
            '        cmd = New SqlCommand(_strSQL, conn)
            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'PEDIATRICS'"
            If dvSettings.Count > 0 Then
                Dim sValue As String = Convert.ToString(dvSettings(0).Row("sValue")).Trim()
                If Not sValue = "" Then
                    glbIsPediatric = sValue
                End If
                'If conn.State = ConnectionState.Open Then
                '    conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'If Not IsNothing(cmd) Then
            '    cmd.Dispose()
            '    cmd = Nothing
            'End If
            'If Not IsNothing(conn) Then
            '    conn.Dispose()
            '    conn = Nothing
            'End If
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If

            'If Not IsNothing(cmd) Then
            '    cmd.Dispose()
            '    cmd = Nothing
            'End If
            'If Not IsNothing(conn) Then
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            dvSettings.RowFilter = Nothing
        End Try

    End Sub

    Public Sub GetSetting()
        'Dim conn As New SqlConnection(GetConnectionString)
        'Dim _strSQL As String = ""
        'Try
        '    _strSQL = "select sSettingsName, ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='SHOW AGE IN DAYS' or sSettingsName='AGE LIMIT'"
        '    If Not IsNothing(conn) Then
        '        If conn.State = ConnectionState.Closed Then
        '            conn.Open()
        '        End If
        '        Dim da As New SqlDataAdapter(_strSQL, conn)
        '        Dim _dt As DataTable
        '        Dim ds As New DataSet
        '        If Not IsNothing(da) Then
        '            da.Fill(ds, "Data")
        '            If Not IsNothing(ds) Then
        '                _dt = ds.Tables("Data")
        '                If IsNothing(_dt) = False Then
        '                    If _dt.Rows.Count > 0 Then
        '                        gblnShowAgeInDays = _dt.Rows(0)(1)
        '                        '' Age Limit in Years, Convert it to Days
        '                        If _dt.Rows(1)(1).ToString() <> "" Then
        '                            gintAgeLimit = CType((_dt.Rows(1)(1) * 365), Int32)
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        End If
        '        If Not IsNothing(da) Then
        '            da.Dispose()
        '            da = Nothing
        '        End If
        '        If Not IsNothing(ds) Then
        '            ds.Dispose()
        '            ds = Nothing
        '        End If
        '        If conn.State = ConnectionState.Open Then
        '            conn.Close()
        '        End If
        '   End If
        Dim _dt As DataTable = Nothing
        Try
            ''Code opti.
            dvSettings.RowFilter = dvSettings.Table.Columns("sName").ColumnName & " = 'SHOW AGE IN DAYS' OR " & dvSettings.Table.Columns("sName").ColumnName & " = 'AGE LIMIT'"
            If dvSettings.Count > 0 Then
                _dt = dvSettings.ToTable()
                gblnShowAgeInDays = _dt.Rows(0)(1)
                '' Age Limit in Years, Convert it to Days
                If _dt.Rows(1)(1).ToString() <> "" Then
                    gintAgeLimit = CType((_dt.Rows(1)(1) * 365), Int32)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If

            'If Not IsNothing(conn) Then
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            dvSettings.RowFilter = Nothing
        Finally
            'If conn.State = ConnectionState.Open Then
            '    conn.Close()
            'End If
            'If Not IsNothing(conn) Then
            '    conn.Dispose()
            '    conn = Nothing
            'End If
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If
            dvSettings.RowFilter = Nothing
        End Try
    End Sub

    Public Function Get_All_EMRSettings(ByVal Connectionstring As String) As DataSet
        Dim conn As New SqlConnection(Connectionstring)
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""
        Dim DS As New DataSet
        Try
            _strSQL = "select sSettingsName as sName, ISNULL(sSettingsValue,'') AS sValue  from settings "
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd = New SqlCommand(_strSQL, conn)

                If Not IsNothing(cmd) Then
                    Dim da As New SqlDataAdapter(_strSQL, conn)
                    da.Fill(DS, "SettingsData")
                    da.Dispose()
                    da = Nothing
                End If
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                Return DS
            End If
            Return Nothing
        Catch ex As Exception
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
        End Try

    End Function
    Private Function GetPatientAccountFeatureSetting() As Boolean

        Dim result As Object = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)
            'Dim _strSqlQuery As String = "select ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='Patient Account Feature'"
            Dim _strSqlQuery As String = "SELECT ISNULL(sSettingValue,'') AS sSettingsValue FROM Settings_Replication where sSettingName='Patient Account Feature'"

            result = oDB.ExecuteScalar_Query(_strSqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try
        If result.ToString().Trim().Length = 0 Then
            result = 0
        End If
        Return Convert.ToBoolean(result)
    End Function

    Private Sub btnLogin_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnLogin.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_OrangeBtnHoverSplashScreen
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try

    End Sub

    Private Sub btnLogin_MouseLeave(sender As Object, e As System.EventArgs) Handles btnLogin.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_OrangeBtnSplashScreen
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub

    Private Sub btnCancel_MouseHover(sender As Object, e As System.EventArgs) Handles btnCancel.MouseHover
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_BlueBtnHoverSplashScreen
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try

    End Sub

    Private Sub btnCancel_MouseLeave(sender As Object, e As System.EventArgs) Handles btnCancel.MouseLeave
        Try
            If Not sender Is Nothing Then
                CType(sender, Button).BackgroundImage = gloEMR.My.Resources.Img_BlueBtnSplashScreen
                CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
            End If

        Catch ex As Exception
            'Blank
        End Try

    End Sub

    Private Sub lblPleaseWait_Click(sender As System.Object, e As System.EventArgs) Handles lblPleaseWait.Click

    End Sub

    'Private Sub frmSplash_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
    '    If Me.WindowState = FormWindowState.Minimized Then
    '        Me.Region = Nothing
    '    Else
    '        If bAfterLoad Then
    '            gloGlobal.BitmapRegion.CreateControlRegion(Me, bmpFrmBack)

    '        End If
    '    End If
    'End Sub

    'Private Sub frmSplash_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
    '    If Me.WindowState = FormWindowState.Minimized Then
    '        'BitmapRegion.CreateControlRegion(this, bmpFrmWhite);
    '        Me.Region = Nothing
    '    Else
    '        If bAfterLoad Then
    '            gloGlobal.BitmapRegion.CreateControlRegion(Me, bmpFrmBack)
    '        End If
    '    End If
    '    '============End for TaskBar Minimized =========================
    'End Sub
End Class
