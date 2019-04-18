Public Class frmVo_SSLSettings
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
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
                Try
                    If (IsNothing(browseFileDialog) = False) Then
                        browseFileDialog.Dispose()
                        browseFileDialog = Nothing
                    End If
                Catch ex As Exception

                End Try
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdRestore As System.Windows.Forms.Button
    Friend WithEvents cbTls1 As System.Windows.Forms.CheckBox
    Friend WithEvents rbSpc As System.Windows.Forms.RadioButton
    Friend WithEvents rbCa As System.Windows.Forms.RadioButton
    Friend WithEvents rbRoot As System.Windows.Forms.RadioButton
    Friend WithEvents rbMy As System.Windows.Forms.RadioButton
    Friend WithEvents rbPemKey As System.Windows.Forms.RadioButton
    Friend WithEvents rbPfxBlob As System.Windows.Forms.RadioButton
    Friend WithEvents rbPfxFile As System.Windows.Forms.RadioButton
    Friend WithEvents rbMachine As System.Windows.Forms.RadioButton
    Friend WithEvents rbUserDefault As System.Windows.Forms.RadioButton
    Friend WithEvents txtCertificate As System.Windows.Forms.TextBox
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowseCADir As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseCertificate As System.Windows.Forms.Button
    Friend WithEvents txtCADirectory As System.Windows.Forms.TextBox
    Friend WithEvents txtCertificateAuthFile As System.Windows.Forms.TextBox
    Friend WithEvents txtCipherList As System.Windows.Forms.TextBox
    Friend WithEvents cbUsingOpenSSL As System.Windows.Forms.CheckBox
    Friend WithEvents lbCADirectory As System.Windows.Forms.Label
    Friend WithEvents lbCertificateAuthFile As System.Windows.Forms.Label
    Friend WithEvents lbCipherList As System.Windows.Forms.Label
    Friend WithEvents rbOther As System.Windows.Forms.RadioButton
    Friend WithEvents cbSsl3 As System.Windows.Forms.CheckBox
    Friend WithEvents cbPct1 As System.Windows.Forms.CheckBox
    Friend WithEvents cbSsl2 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents browseDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents browseFileDialog As System.Windows.Forms.OpenFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdRestore = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCertificate = New System.Windows.Forms.TextBox
        Me.txtOther = New System.Windows.Forms.TextBox
        Me.rbOther = New System.Windows.Forms.RadioButton
        Me.rbSpc = New System.Windows.Forms.RadioButton
        Me.rbCa = New System.Windows.Forms.RadioButton
        Me.rbRoot = New System.Windows.Forms.RadioButton
        Me.rbMy = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.rbPemKey = New System.Windows.Forms.RadioButton
        Me.rbPfxBlob = New System.Windows.Forms.RadioButton
        Me.rbPfxFile = New System.Windows.Forms.RadioButton
        Me.rbMachine = New System.Windows.Forms.RadioButton
        Me.rbUserDefault = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdBrowseCADir = New System.Windows.Forms.Button
        Me.cmdBrowseCertificate = New System.Windows.Forms.Button
        Me.txtCADirectory = New System.Windows.Forms.TextBox
        Me.txtCertificateAuthFile = New System.Windows.Forms.TextBox
        Me.txtCipherList = New System.Windows.Forms.TextBox
        Me.lbCADirectory = New System.Windows.Forms.Label
        Me.lbCertificateAuthFile = New System.Windows.Forms.Label
        Me.lbCipherList = New System.Windows.Forms.Label
        Me.cbUsingOpenSSL = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cbSsl3 = New System.Windows.Forms.CheckBox
        Me.cbPct1 = New System.Windows.Forms.CheckBox
        Me.cbSsl2 = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbTls1 = New System.Windows.Forms.CheckBox
        Me.browseDialog = New System.Windows.Forms.FolderBrowserDialog
        Me.browseFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmOK
        '
        Me.cmOK.Location = New System.Drawing.Point(344, 375)
        Me.cmOK.Name = "cmOK"
        Me.cmOK.Size = New System.Drawing.Size(96, 23)
        Me.cmOK.TabIndex = 3
        Me.cmOK.Text = "OK"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(472, 375)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(96, 23)
        Me.cmdCancel.TabIndex = 4
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdRestore
        '
        Me.cmdRestore.Location = New System.Drawing.Point(600, 375)
        Me.cmdRestore.Name = "cmdRestore"
        Me.cmdRestore.Size = New System.Drawing.Size(96, 23)
        Me.cmdRestore.TabIndex = 6
        Me.cmdRestore.Text = "&Restore Default"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtCertificate)
        Me.GroupBox1.Controls.Add(Me.txtOther)
        Me.GroupBox1.Controls.Add(Me.rbOther)
        Me.GroupBox1.Controls.Add(Me.rbSpc)
        Me.GroupBox1.Controls.Add(Me.rbCa)
        Me.GroupBox1.Controls.Add(Me.rbRoot)
        Me.GroupBox1.Controls.Add(Me.rbMy)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(296, 360)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Certificate Store"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 309)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(147, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Certificate Store Pass&word:"
        '
        'txtCertificate
        '
        Me.txtCertificate.Location = New System.Drawing.Point(24, 329)
        Me.txtCertificate.Name = "txtCertificate"
        Me.txtCertificate.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtCertificate.Size = New System.Drawing.Size(256, 20)
        Me.txtCertificate.TabIndex = 9
        Me.txtCertificate.Text = ""
        '
        'txtOther
        '
        Me.txtOther.Location = New System.Drawing.Point(24, 272)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(256, 20)
        Me.txtOther.TabIndex = 7
        Me.txtOther.Text = ""
        '
        'rbOther
        '
        Me.rbOther.Location = New System.Drawing.Point(22, 248)
        Me.rbOther.Name = "rbOther"
        Me.rbOther.Size = New System.Drawing.Size(130, 15)
        Me.rbOther.TabIndex = 6
        Me.rbOther.Text = "Other (details below)"
        '
        'rbSpc
        '
        Me.rbSpc.Location = New System.Drawing.Point(22, 230)
        Me.rbSpc.Name = "rbSpc"
        Me.rbSpc.Size = New System.Drawing.Size(50, 15)
        Me.rbSpc.TabIndex = 5
        Me.rbSpc.Text = "SPC"
        '
        'rbCa
        '
        Me.rbCa.Location = New System.Drawing.Point(22, 194)
        Me.rbCa.Name = "rbCa"
        Me.rbCa.Size = New System.Drawing.Size(42, 15)
        Me.rbCa.TabIndex = 3
        Me.rbCa.Text = "CA"
        '
        'rbRoot
        '
        Me.rbRoot.Location = New System.Drawing.Point(22, 212)
        Me.rbRoot.Name = "rbRoot"
        Me.rbRoot.Size = New System.Drawing.Size(58, 15)
        Me.rbRoot.TabIndex = 4
        Me.rbRoot.Text = "ROOT"
        '
        'rbMy
        '
        Me.rbMy.Location = New System.Drawing.Point(22, 176)
        Me.rbMy.Name = "rbMy"
        Me.rbMy.Size = New System.Drawing.Size(42, 15)
        Me.rbMy.TabIndex = 2
        Me.rbMy.Text = "MY"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "&Certificated Store:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.rbPemKey)
        Me.GroupBox4.Controls.Add(Me.rbPfxBlob)
        Me.GroupBox4.Controls.Add(Me.rbPfxFile)
        Me.GroupBox4.Controls.Add(Me.rbMachine)
        Me.GroupBox4.Controls.Add(Me.rbUserDefault)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GroupBox4.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(296, 360)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Certificate Store"
        '
        'rbPemKey
        '
        Me.rbPemKey.Location = New System.Drawing.Point(20, 124)
        Me.rbPemKey.Name = "rbPemKey"
        Me.rbPemKey.Size = New System.Drawing.Size(74, 16)
        Me.rbPemKey.TabIndex = 5
        Me.rbPemKey.Text = "PEM Key"
        '
        'rbPfxBlob
        '
        Me.rbPfxBlob.Location = New System.Drawing.Point(20, 105)
        Me.rbPfxBlob.Name = "rbPfxBlob"
        Me.rbPfxBlob.Size = New System.Drawing.Size(74, 16)
        Me.rbPfxBlob.TabIndex = 4
        Me.rbPfxBlob.Text = "PFX Blob"
        '
        'rbPfxFile
        '
        Me.rbPfxFile.Location = New System.Drawing.Point(20, 86)
        Me.rbPfxFile.Name = "rbPfxFile"
        Me.rbPfxFile.Size = New System.Drawing.Size(74, 16)
        Me.rbPfxFile.TabIndex = 3
        Me.rbPfxFile.Text = "PFX File"
        '
        'rbMachine
        '
        Me.rbMachine.Location = New System.Drawing.Point(20, 67)
        Me.rbMachine.Name = "rbMachine"
        Me.rbMachine.Size = New System.Drawing.Size(74, 16)
        Me.rbMachine.TabIndex = 2
        Me.rbMachine.Text = "Machine"
        '
        'rbUserDefault
        '
        Me.rbUserDefault.Location = New System.Drawing.Point(20, 48)
        Me.rbUserDefault.Name = "rbUserDefault"
        Me.rbUserDefault.Size = New System.Drawing.Size(98, 16)
        Me.rbUserDefault.TabIndex = 1
        Me.rbUserDefault.Text = "User (default)"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Certificated Store T&ype:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdBrowseCADir)
        Me.GroupBox2.Controls.Add(Me.cmdBrowseCertificate)
        Me.GroupBox2.Controls.Add(Me.txtCADirectory)
        Me.GroupBox2.Controls.Add(Me.txtCertificateAuthFile)
        Me.GroupBox2.Controls.Add(Me.txtCipherList)
        Me.GroupBox2.Controls.Add(Me.lbCADirectory)
        Me.GroupBox2.Controls.Add(Me.lbCertificateAuthFile)
        Me.GroupBox2.Controls.Add(Me.lbCipherList)
        Me.GroupBox2.Controls.Add(Me.cbUsingOpenSSL)
        Me.GroupBox2.Location = New System.Drawing.Point(312, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(384, 168)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "OpenSSL"
        '
        'cmdBrowseCADir
        '
        Me.cmdBrowseCADir.Location = New System.Drawing.Point(304, 135)
        Me.cmdBrowseCADir.Name = "cmdBrowseCADir"
        Me.cmdBrowseCADir.TabIndex = 8
        Me.cmdBrowseCADir.Text = "Brow&se..."
        '
        'cmdBrowseCertificate
        '
        Me.cmdBrowseCertificate.Location = New System.Drawing.Point(304, 98)
        Me.cmdBrowseCertificate.Name = "cmdBrowseCertificate"
        Me.cmdBrowseCertificate.TabIndex = 5
        Me.cmdBrowseCertificate.Text = "&Browse..."
        '
        'txtCADirectory
        '
        Me.txtCADirectory.Location = New System.Drawing.Point(136, 136)
        Me.txtCADirectory.Name = "txtCADirectory"
        Me.txtCADirectory.Size = New System.Drawing.Size(160, 20)
        Me.txtCADirectory.TabIndex = 7
        Me.txtCADirectory.Text = ""
        '
        'txtCertificateAuthFile
        '
        Me.txtCertificateAuthFile.Location = New System.Drawing.Point(136, 100)
        Me.txtCertificateAuthFile.Name = "txtCertificateAuthFile"
        Me.txtCertificateAuthFile.Size = New System.Drawing.Size(160, 20)
        Me.txtCertificateAuthFile.TabIndex = 4
        Me.txtCertificateAuthFile.Text = ""
        '
        'txtCipherList
        '
        Me.txtCipherList.Location = New System.Drawing.Point(136, 64)
        Me.txtCipherList.Name = "txtCipherList"
        Me.txtCipherList.Size = New System.Drawing.Size(160, 20)
        Me.txtCipherList.TabIndex = 2
        Me.txtCipherList.Text = ""
        '
        'lbCADirectory
        '
        Me.lbCADirectory.Location = New System.Drawing.Point(16, 140)
        Me.lbCADirectory.Name = "lbCADirectory"
        Me.lbCADirectory.Size = New System.Drawing.Size(72, 16)
        Me.lbCADirectory.TabIndex = 6
        Me.lbCADirectory.Text = "CA &Directory:"
        '
        'lbCertificateAuthFile
        '
        Me.lbCertificateAuthFile.Location = New System.Drawing.Point(16, 103)
        Me.lbCertificateAuthFile.Name = "lbCertificateAuthFile"
        Me.lbCertificateAuthFile.Size = New System.Drawing.Size(128, 16)
        Me.lbCertificateAuthFile.TabIndex = 3
        Me.lbCertificateAuthFile.Text = "Certificate &Authority File:"
        '
        'lbCipherList
        '
        Me.lbCipherList.Location = New System.Drawing.Point(16, 66)
        Me.lbCipherList.Name = "lbCipherList"
        Me.lbCipherList.Size = New System.Drawing.Size(72, 16)
        Me.lbCipherList.TabIndex = 1
        Me.lbCipherList.Text = "Cipher &List:"
        '
        'cbUsingOpenSSL
        '
        Me.cbUsingOpenSSL.Location = New System.Drawing.Point(19, 24)
        Me.cbUsingOpenSSL.Name = "cbUsingOpenSSL"
        Me.cbUsingOpenSSL.Size = New System.Drawing.Size(121, 16)
        Me.cbUsingOpenSSL.TabIndex = 0
        Me.cbUsingOpenSSL.Text = "Using &OpenSSL"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbSsl3)
        Me.GroupBox3.Controls.Add(Me.cbPct1)
        Me.GroupBox3.Controls.Add(Me.cbSsl2)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.cbTls1)
        Me.GroupBox3.Location = New System.Drawing.Point(312, 184)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(384, 184)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "General"
        '
        'cbSsl3
        '
        Me.cbSsl3.Location = New System.Drawing.Point(144, 54)
        Me.cbSsl3.Name = "cbSsl3"
        Me.cbSsl3.Size = New System.Drawing.Size(56, 16)
        Me.cbSsl3.TabIndex = 2
        Me.cbSsl3.Text = "SSL&3"
        '
        'cbPct1
        '
        Me.cbPct1.Location = New System.Drawing.Point(144, 102)
        Me.cbPct1.Name = "cbPct1"
        Me.cbPct1.Size = New System.Drawing.Size(56, 16)
        Me.cbPct1.TabIndex = 4
        Me.cbPct1.Text = "&PCT1"
        '
        'cbSsl2
        '
        Me.cbSsl2.Location = New System.Drawing.Point(144, 78)
        Me.cbSsl2.Name = "cbSsl2"
        Me.cbSsl2.Size = New System.Drawing.Size(56, 16)
        Me.cbSsl2.TabIndex = 3
        Me.cbSsl2.Text = "SSL&2"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(17, 29)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "SSL Enabled Protocols:"
        '
        'cbTls1
        '
        Me.cbTls1.Location = New System.Drawing.Point(144, 30)
        Me.cbTls1.Name = "cbTls1"
        Me.cbTls1.Size = New System.Drawing.Size(56, 16)
        Me.cbTls1.TabIndex = 1
        Me.cbTls1.Text = "TLS&1"
        '
        'SSLSettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(704, 406)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdRestore)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmOK)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "SSLSettings"
        Me.Text = "SSL Settings"
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public networkDirectory As DNSTools.DgnNetworkDirectory

    Private Sub UpdateUI()
        Dim bUsingOpenSSLState = cbUsingOpenSSL.Checked
        Dim bCertificateStoreState = (rbUserDefault.Checked Or rbMachine.Checked) And _
            Not bUsingOpenSSLState

        ' Certificate Store Type group
        rbMachine.Enabled = Not bUsingOpenSSLState
        If (bUsingOpenSSLState And rbMachine.Checked) Then
            rbUserDefault.Checked = True
        End If

        ' Certificate Store group
        rbMy.Enabled = bCertificateStoreState
        rbCa.Enabled = bCertificateStoreState
        rbRoot.Enabled = bCertificateStoreState
        rbSpc.Enabled = bCertificateStoreState
        If (Not bCertificateStoreState) Then
            rbOther.Checked = True
        End If
        txtOther.Enabled = rbOther.Checked

        ' Open SSL group
        txtCipherList.Enabled = bUsingOpenSSLState
        txtCertificateAuthFile.Enabled = bUsingOpenSSLState
        txtCADirectory.Enabled = bUsingOpenSSLState
        cmdBrowseCertificate.Enabled = bUsingOpenSSLState
        cmdBrowseCADir.Enabled = bUsingOpenSSLState

        ' Enabled Protocols group
        cbTls1.Enabled = Not bUsingOpenSSLState
        cbSsl3.Enabled = Not bUsingOpenSSLState
        cbSsl2.Enabled = Not bUsingOpenSSLState
        cbPct1.Enabled = Not bUsingOpenSSLState
    End Sub

    Public Sub Initialize(ByVal netDirectory As DNSTools.DgnNetworkDirectory)
        networkDirectory = netDirectory
        UpdateSSLSettingsFromObject()

        UpdateUI()
    End Sub

    Private Sub cmOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmOK.Click
        On Error GoTo Cannot_Set_SSL_Settings
        UpdateSSLSettingsFromForm()
        Me.DialogResult = DialogResult.OK
        Me.Close()
        Return
Cannot_Set_SSL_Settings:
        MsgBox("Cannot set SSL settings." & vbCrLf & _
               "Reason : " & Err.Description, MsgBoxStyle.Critical, Me.Text)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRestore.Click
        networkDirectory.RestoreDefaultOptions()
        UpdateSSLSettingsFromObject()

        UpdateUI()
    End Sub
    Private Sub rbUserDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUserDefault.Click
        UpdateUI()
    End Sub

    Private Sub rbMachine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMachine.Click
        UpdateUI()
    End Sub

    Private Sub cbUsingOpenSSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUsingOpenSSL.Click
        UpdateUI()
    End Sub

    Private Sub rbPfxFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPfxFile.Click
        UpdateUI()
    End Sub

    Private Sub rbPfxBlob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPfxBlob.Click
        UpdateUI()
    End Sub

    Private Sub rbPemKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPemKey.Click
        UpdateUI()
    End Sub

    Private Sub rbMy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMy.Click
        UpdateUI()
    End Sub

    Private Sub rbCa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCa.Click
        UpdateUI()
    End Sub

    Private Sub rbRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbRoot.Click
        UpdateUI()
    End Sub

    Private Sub rbSpc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSpc.Click
        UpdateUI()
    End Sub

    Private Sub rbOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbOther.Click
        UpdateUI()
    End Sub

    Private Sub cmdBrowseCertificate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseCertificate.Click
        If (browseFileDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK) Then
            txtCertificateAuthFile.Text = browseFileDialog.FileName
        End If
    End Sub

    Private Sub cmdBrowseCADir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseCADir.Click
        browseDialog.Description = "Browse For Folder"
        browseDialog.ShowNewFolderButton = True

        If (browseDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK) Then
            txtCADirectory.Text = browseDialog.SelectedPath
        End If
    End Sub

    Private Sub UpdateSSLSettingsFromObject()
        LoadSertificateStoreType()
        LoadSertificateStore()
        txtCertificate.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslCertificateStorePassword)
        cbUsingOpenSSL.Checked = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsUseOpenSsl)
        txtCipherList.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsOpenSslCipherList)
        txtCertificateAuthFile.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsOpenSslCaFile)
        txtCADirectory.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsOpenSslCaDirectory)
        LoadSSLEnabledProtocols()
    End Sub

    Private Sub UpdateSSLSettingsFromForm()
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslCertificateStoreType) = SaveSertificateStoreType()
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslCertificateStore) = SaveSertificateStore()
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslCertificateStorePassword) = txtCertificate.Text
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsUseOpenSsl) = cbUsingOpenSSL.Checked
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsOpenSslCipherList) = txtCipherList.Text
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsOpenSslCaFile) = txtCertificateAuthFile.Text
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsOpenSslCaDirectory) = txtCADirectory.Text
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslEnabledProtocols) = SaveSSLEnabledProtocols()
    End Sub

    Private Sub LoadSertificateStoreType()
        Select Case networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslCertificateStoreType)
            Case DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstoreMachine
                rbMachine.Checked = True
            Case DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstorePemKey
                rbPemKey.Checked = True
            Case DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstorePfxBlob
                rbPfxBlob.Checked = True
            Case DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstorePfxFile
                rbPfxFile.Checked = True
            Case DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstoreUser
                rbUserDefault.Checked = True
        End Select
    End Sub

    Private Function SaveSertificateStoreType()
        If rbMachine.Checked Then
            Return DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstoreMachine
        End If
        If rbPemKey.Checked Then
            Return DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstorePemKey
        End If
        If rbPfxBlob.Checked Then
            Return DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstorePfxBlob
        End If
        If rbPfxFile.Checked Then
            Return DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstorePfxFile
        End If
        If rbUserDefault.Checked Then
            Return DNSTools.DgnHttpsCertificateStoreTypeConstants.dgnhttpscertstoreUser
        End If
        Return Nothing
    End Function

    Private Sub LoadSertificateStore()
        txtOther.Text = ""
        Dim certificateStore As String
        certificateStore = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslCertificateStore)
        Select Case certificateStore
            Case "MY"
                rbMy.Checked = True
            Case "CA"
                rbCa.Checked = True
            Case "ROOT"
                rbRoot.Checked = True
            Case "SPC"
                rbSpc.Checked = True
            Case Else
                rbOther.Checked = True
                txtOther.Text = certificateStore
        End Select
    End Sub

    Private Function SaveSertificateStore()
        If rbMy.Checked Then
            Return "MY"
        End If
        If rbCa.Checked Then
            Return "CA"
        End If
        If rbRoot.Checked Then
            Return "ROOT"
        End If
        If rbSpc.Checked Then
            Return "SPC"
        End If
        If rbOther.Checked Then
            Return txtOther.Text
        End If
        Return Nothing
    End Function

    Private Sub LoadSSLEnabledProtocols()
        Dim decValue As Decimal
        decValue = Decimal.op_Implicit(networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpsSslEnabledProtocols))
        Dim nValue As Integer
        nValue = CInt(decValue)

        If (nValue And DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotTls1) Then
            cbTls1.Checked = True
        End If

        If (nValue And DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotSsl3) Then
            cbSsl3.Checked = True
        End If

        If (nValue And DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotSsl2) Then
            cbSsl2.Checked = True
        End If

        If (nValue And DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotPct1) Then
            cbPct1.Checked = True
        End If
    End Sub

    Private Function SaveSSLEnabledProtocols()
        Dim nResult As Integer = 0

        If cbTls1.Checked Then
            nResult = (nResult Or DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotTls1)
        End If
        If cbSsl3.Checked Then
            nResult = (nResult Or DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotSsl3)
        End If
        If cbSsl2.Checked Then
            nResult = (nResult Or DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotSsl2)
        End If
        If cbPct1.Checked Then
            nResult = (nResult Or DNSTools.DgnHttpsSslEnabledProtocolConstants.dgnhttpsprotPct1)
        End If

        Return nResult
    End Function

End Class
