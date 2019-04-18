Public Class frmVo_HttpSettings
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
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents gbAuthentication As System.Windows.Forms.GroupBox
    Friend WithEvents rbDigest As System.Windows.Forms.RadioButton
    Friend WithEvents rbBasic As System.Windows.Forms.RadioButton
    Friend WithEvents gbFollowRedirects As System.Windows.Forms.GroupBox
    Friend WithEvents rbSomeOnly As System.Windows.Forms.RadioButton
    Friend WithEvents rbAlways As System.Windows.Forms.RadioButton
    Friend WithEvents rbNever As System.Windows.Forms.RadioButton
    Friend WithEvents gbFirewall As System.Windows.Forms.GroupBox
    Friend WithEvents gbTimeouts As System.Windows.Forms.GroupBox
    Friend WithEvents chkPrompt As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseProxyServer As System.Windows.Forms.CheckBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtFirewallData As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents txtProxyUser As System.Windows.Forms.TextBox
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents txtConnection As System.Windows.Forms.TextBox
    Friend WithEvents txtLock As System.Windows.Forms.TextBox
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdestore As System.Windows.Forms.Button
    Friend WithEvents rbAbsolut As System.Windows.Forms.RadioButton
    Friend WithEvents rbInactivity As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmVo_HttpSettings))
        Me.gbAuthentication = New System.Windows.Forms.GroupBox
        Me.rbDigest = New System.Windows.Forms.RadioButton
        Me.rbBasic = New System.Windows.Forms.RadioButton
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkPrompt = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.gbFollowRedirects = New System.Windows.Forms.GroupBox
        Me.rbSomeOnly = New System.Windows.Forms.RadioButton
        Me.rbAlways = New System.Windows.Forms.RadioButton
        Me.rbNever = New System.Windows.Forms.RadioButton
        Me.gbFirewall = New System.Windows.Forms.GroupBox
        Me.txtFirewallData = New System.Windows.Forms.TextBox
        Me.cbType = New System.Windows.Forms.ComboBox
        Me.txtProxyPassword = New System.Windows.Forms.TextBox
        Me.txtPort = New System.Windows.Forms.TextBox
        Me.txtProxyUser = New System.Windows.Forms.TextBox
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.chkUseProxyServer = New System.Windows.Forms.CheckBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdestore = New System.Windows.Forms.Button
        Me.gbTimeouts = New System.Windows.Forms.GroupBox
        Me.rbAbsolut = New System.Windows.Forms.RadioButton
        Me.rbInactivity = New System.Windows.Forms.RadioButton
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtConnection = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtLock = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.gbAuthentication.SuspendLayout()
        Me.gbFollowRedirects.SuspendLayout()
        Me.gbFirewall.SuspendLayout()
        Me.gbTimeouts.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbAuthentication
        '
        Me.gbAuthentication.Controls.Add(Me.rbDigest)
        Me.gbAuthentication.Controls.Add(Me.rbBasic)
        Me.gbAuthentication.Controls.Add(Me.txtPassword)
        Me.gbAuthentication.Controls.Add(Me.txtUser)
        Me.gbAuthentication.Controls.Add(Me.Label4)
        Me.gbAuthentication.Controls.Add(Me.Label3)
        Me.gbAuthentication.Controls.Add(Me.Label2)
        Me.gbAuthentication.Controls.Add(Me.chkPrompt)
        Me.gbAuthentication.Controls.Add(Me.Label1)
        Me.gbAuthentication.Location = New System.Drawing.Point(8, 8)
        Me.gbAuthentication.Name = "gbAuthentication"
        Me.gbAuthentication.Size = New System.Drawing.Size(448, 144)
        Me.gbAuthentication.TabIndex = 0
        Me.gbAuthentication.TabStop = False
        Me.gbAuthentication.Text = "Authentication"
        '
        'rbDigest
        '
        Me.rbDigest.Location = New System.Drawing.Point(264, 101)
        Me.rbDigest.Name = "rbDigest"
        Me.rbDigest.Size = New System.Drawing.Size(56, 24)
        Me.rbDigest.TabIndex = 8
        Me.rbDigest.Text = "Digest"
        '
        'rbBasic
        '
        Me.rbBasic.Location = New System.Drawing.Point(264, 70)
        Me.rbBasic.Name = "rbBasic"
        Me.rbBasic.Size = New System.Drawing.Size(56, 24)
        Me.rbBasic.TabIndex = 7
        Me.rbBasic.Text = "Basic"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(72, 104)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.TabIndex = 5
        Me.txtPassword.Text = ""
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(72, 72)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.TabIndex = 3
        Me.txtUser.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(16, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Passwor&d:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "&User:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Default User and Password:"
        '
        'chkPrompt
        '
        Me.chkPrompt.Location = New System.Drawing.Point(24, 16)
        Me.chkPrompt.Name = "chkPrompt"
        Me.chkPrompt.Size = New System.Drawing.Size(192, 24)
        Me.chkPrompt.TabIndex = 0
        Me.chkPrompt.Text = "&Prompt for User and Password"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(248, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "&Authentication Type:"
        '
        'gbFollowRedirects
        '
        Me.gbFollowRedirects.Controls.Add(Me.rbSomeOnly)
        Me.gbFollowRedirects.Controls.Add(Me.rbAlways)
        Me.gbFollowRedirects.Controls.Add(Me.rbNever)
        Me.gbFollowRedirects.Location = New System.Drawing.Point(464, 8)
        Me.gbFollowRedirects.Name = "gbFollowRedirects"
        Me.gbFollowRedirects.Size = New System.Drawing.Size(163, 144)
        Me.gbFollowRedirects.TabIndex = 2
        Me.gbFollowRedirects.TabStop = False
        Me.gbFollowRedirects.Text = "&Follow Redirects"
        '
        'rbSomeOnly
        '
        Me.rbSomeOnly.Location = New System.Drawing.Point(16, 104)
        Me.rbSomeOnly.Name = "rbSomeOnly"
        Me.rbSomeOnly.Size = New System.Drawing.Size(120, 24)
        Me.rbSomeOnly.TabIndex = 2
        Me.rbSomeOnly.Text = "Some Scheme Only"
        '
        'rbAlways
        '
        Me.rbAlways.Location = New System.Drawing.Point(16, 65)
        Me.rbAlways.Name = "rbAlways"
        Me.rbAlways.Size = New System.Drawing.Size(64, 24)
        Me.rbAlways.TabIndex = 1
        Me.rbAlways.Text = "Always"
        '
        'rbNever
        '
        Me.rbNever.Location = New System.Drawing.Point(16, 25)
        Me.rbNever.Name = "rbNever"
        Me.rbNever.Size = New System.Drawing.Size(56, 24)
        Me.rbNever.TabIndex = 0
        Me.rbNever.Text = "Never"
        '
        'gbFirewall
        '
        Me.gbFirewall.Controls.Add(Me.txtFirewallData)
        Me.gbFirewall.Controls.Add(Me.cbType)
        Me.gbFirewall.Controls.Add(Me.txtProxyPassword)
        Me.gbFirewall.Controls.Add(Me.txtPort)
        Me.gbFirewall.Controls.Add(Me.txtProxyUser)
        Me.gbFirewall.Controls.Add(Me.txtServer)
        Me.gbFirewall.Controls.Add(Me.Label10)
        Me.gbFirewall.Controls.Add(Me.Label9)
        Me.gbFirewall.Controls.Add(Me.Label8)
        Me.gbFirewall.Controls.Add(Me.Label7)
        Me.gbFirewall.Controls.Add(Me.Label6)
        Me.gbFirewall.Controls.Add(Me.Label5)
        Me.gbFirewall.Controls.Add(Me.chkUseProxyServer)
        Me.gbFirewall.Location = New System.Drawing.Point(8, 152)
        Me.gbFirewall.Name = "gbFirewall"
        Me.gbFirewall.Size = New System.Drawing.Size(448, 224)
        Me.gbFirewall.TabIndex = 1
        Me.gbFirewall.TabStop = False
        Me.gbFirewall.Text = "Firewall and Proxy Servers"
        '
        'txtFirewallData
        '
        Me.txtFirewallData.Location = New System.Drawing.Point(16, 176)
        Me.txtFirewallData.Name = "txtFirewallData"
        Me.txtFirewallData.Size = New System.Drawing.Size(392, 20)
        Me.txtFirewallData.TabIndex = 12
        Me.txtFirewallData.Text = ""
        '
        'cbType
        '
        Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbType.Location = New System.Drawing.Point(64, 64)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(136, 21)
        Me.cbType.TabIndex = 2
        '
        'txtProxyPassword
        '
        Me.txtProxyPassword.Location = New System.Drawing.Point(293, 128)
        Me.txtProxyPassword.Name = "txtProxyPassword"
        Me.txtProxyPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtProxyPassword.Size = New System.Drawing.Size(113, 20)
        Me.txtProxyPassword.TabIndex = 10
        Me.txtProxyPassword.Text = ""
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(293, 96)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(49, 20)
        Me.txtPort.TabIndex = 6
        Me.txtPort.Text = ""
        '
        'txtProxyUser
        '
        Me.txtProxyUser.Location = New System.Drawing.Point(64, 128)
        Me.txtProxyUser.Name = "txtProxyUser"
        Me.txtProxyUser.Size = New System.Drawing.Size(136, 20)
        Me.txtProxyUser.TabIndex = 8
        Me.txtProxyUser.Text = ""
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(64, 96)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(136, 20)
        Me.txtServer.TabIndex = 4
        Me.txtServer.Text = ""
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(24, 152)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(192, 16)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Firewall Data or Proxy Authorization:"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(227, 132)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 15)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Pass&word:"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(227, 97)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 16)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "P&ort:"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 128)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 18)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "U&ser:"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(16, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 15)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Se&rver:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(16, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 15)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "T&ype:"
        '
        'chkUseProxyServer
        '
        Me.chkUseProxyServer.Location = New System.Drawing.Point(24, 24)
        Me.chkUseProxyServer.Name = "chkUseProxyServer"
        Me.chkUseProxyServer.Size = New System.Drawing.Size(129, 24)
        Me.chkUseProxyServer.TabIndex = 0
        Me.chkUseProxyServer.Text = "Use Pro&xy Server"
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(273, 384)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(113, 24)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(393, 384)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(112, 24)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        '
        'cmdestore
        '
        Me.cmdestore.Location = New System.Drawing.Point(513, 384)
        Me.cmdestore.Name = "cmdestore"
        Me.cmdestore.Size = New System.Drawing.Size(113, 24)
        Me.cmdestore.TabIndex = 6
        Me.cmdestore.Text = "&Restore Defaults"
        '
        'gbTimeouts
        '
        Me.gbTimeouts.Controls.Add(Me.rbAbsolut)
        Me.gbTimeouts.Controls.Add(Me.rbInactivity)
        Me.gbTimeouts.Controls.Add(Me.Label16)
        Me.gbTimeouts.Controls.Add(Me.txtConnection)
        Me.gbTimeouts.Controls.Add(Me.Label15)
        Me.gbTimeouts.Controls.Add(Me.Label14)
        Me.gbTimeouts.Controls.Add(Me.txtLock)
        Me.gbTimeouts.Controls.Add(Me.Label13)
        Me.gbTimeouts.Controls.Add(Me.Label12)
        Me.gbTimeouts.Controls.Add(Me.Label11)
        Me.gbTimeouts.Location = New System.Drawing.Point(464, 152)
        Me.gbTimeouts.Name = "gbTimeouts"
        Me.gbTimeouts.Size = New System.Drawing.Size(163, 224)
        Me.gbTimeouts.TabIndex = 3
        Me.gbTimeouts.TabStop = False
        Me.gbTimeouts.Text = "Timeouts"
        '
        'rbAbsolut
        '
        Me.rbAbsolut.Location = New System.Drawing.Point(32, 187)
        Me.rbAbsolut.Name = "rbAbsolut"
        Me.rbAbsolut.Size = New System.Drawing.Size(72, 21)
        Me.rbAbsolut.TabIndex = 9
        Me.rbAbsolut.Text = "Absolute"
        '
        'rbInactivity
        '
        Me.rbInactivity.Location = New System.Drawing.Point(32, 166)
        Me.rbInactivity.Name = "rbInactivity"
        Me.rbInactivity.Size = New System.Drawing.Size(72, 21)
        Me.rbInactivity.TabIndex = 8
        Me.rbInactivity.Text = "Inactivity"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(104, 118)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 15)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "seconds"
        '
        'txtConnection
        '
        Me.txtConnection.Location = New System.Drawing.Point(32, 118)
        Me.txtConnection.MaxLength = 4
        Me.txtConnection.Name = "txtConnection"
        Me.txtConnection.Size = New System.Drawing.Size(64, 20)
        Me.txtConnection.TabIndex = 5
        Me.txtConnection.Text = ""
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(32, 64)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 16)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "(0 - use server default)"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(104, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 16)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "seconds"
        '
        'txtLock
        '
        Me.txtLock.Location = New System.Drawing.Point(32, 40)
        Me.txtLock.MaxLength = 4
        Me.txtLock.Name = "txtLock"
        Me.txtLock.Size = New System.Drawing.Size(64, 20)
        Me.txtLock.TabIndex = 1
        Me.txtLock.Text = ""
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(16, 146)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(32, 15)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "&Type:"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(16, 97)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 17)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "&Connection:"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(16, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 16)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "&Lock:"
        '
        'HTTPSettings
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(634, 413)
        Me.Controls.Add(Me.gbTimeouts)
        Me.Controls.Add(Me.cmdestore)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.gbFirewall)
        Me.Controls.Add(Me.gbFollowRedirects)
        Me.Controls.Add(Me.gbAuthentication)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HTTPSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HTTP Settings"
        Me.gbAuthentication.ResumeLayout(False)
        Me.gbFollowRedirects.ResumeLayout(False)
        Me.gbFirewall.ResumeLayout(False)
        Me.gbTimeouts.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public networkDirectory As DNSTools.DgnNetworkDirectory

    Private Sub UpdateNetdirectoryFromObject()
        chkPrompt.Checked = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpPromptForPassword)
        txtUser.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpAuthenticationUser)
        txtPassword.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpAuthenticationPassword)
        If (networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpAuthenticationType) = DNSTools.DgnHttpAuthenticationTypeConstants.dgnhttpauthtypeBasic) Then
            rbBasic.Checked = True
        Else
            rbDigest.Checked = True
        End If
        chkUseProxyServer.Checked = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpUseProxyServer)
        cbType.SelectedIndex = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyType)
        txtServer.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerName)
        txtPort.Text = Format(networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerPort), "g")
        txtProxyPassword.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerPassword)
        txtFirewallData.Text = networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerAuthenticationString)
        If (networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpFollowRedirects) = DNSTools.DgnHttpFollowRedirectsConstants.dgnhttpfollowNever) Then
            rbNever.Checked = True
        Else
            If (networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpFollowRedirects) = DNSTools.DgnHttpFollowRedirectsConstants.dgnhttpfollowAlways) Then
                rbAlways.Checked = True
            Else
                rbSomeOnly.Checked = True
            End If
        End If
        txtLock.Text = Format(networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpLockTimeout), "g")
        txtConnection.Text = Format(networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpConnectionTimeout), "g")
        If (networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpTimeoutIfInactive)) Then
            rbInactivity.Checked = True
        Else
            rbAbsolut.Checked = True
        End If
    End Sub

    Private Sub UpdateNetdirectoryFromForm()
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpPromptForPassword) = chkPrompt.Checked
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpAuthenticationUser) = txtUser.Text
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpAuthenticationPassword) = txtPassword.Text
        If (rbBasic.Checked) Then
            networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpAuthenticationType) = DNSTools.DgnHttpAuthenticationTypeConstants.dgnhttpauthtypeBasic
        Else
            networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpAuthenticationType) = DNSTools.DgnHttpAuthenticationTypeConstants.dgnhttpauthtypeDigest
        End If
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpUseProxyServer) = chkUseProxyServer.Checked
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyType) = cbType.SelectedIndex
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerName) = txtServer.Text()
        If (txtPort.Text <> "") Then
            networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerPort) = CInt(txtPort.Text)
        End If
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerPassword) = txtProxyPassword.Text
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpProxyServerAuthenticationString) = txtFirewallData.Text
        If (rbNever.Checked) Then
            networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpFollowRedirects) = DNSTools.DgnHttpFollowRedirectsConstants.dgnhttpfollowNever
        Else
            If (rbAlways.Checked) Then
                networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpFollowRedirects) = DNSTools.DgnHttpFollowRedirectsConstants.dgnhttpfollowAlways
            Else
                networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpFollowRedirects) = DNSTools.DgnHttpFollowRedirectsConstants.dgnhttpfollowSameSchemeOnly
            End If
        End If
        If (txtLock.Text <> "") Then
            networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpLockTimeout) = CInt(txtLock.Text)
        End If
        If (txtConnection.Text <> "") Then
            networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpConnectionTimeout) = CInt(txtConnection.Text)
        End If
        networkDirectory.Option(DNSTools.DgnNetworkDirectoryOptionConstants.dgnnetdiroptionHttpTimeoutIfInactive) = rbInactivity.Checked
    End Sub

    Private Sub UpdateProxySettingsUI()
        txtServer.Enabled = chkUseProxyServer.Checked
        txtProxyUser.Enabled = chkUseProxyServer.Checked
        txtPort.Enabled = chkUseProxyServer.Checked
        txtProxyPassword.Enabled = chkUseProxyServer.Checked
        cbType.Enabled = chkUseProxyServer.Checked
        txtFirewallData.Enabled = chkUseProxyServer.Checked
    End Sub

    Public Sub Initialize(ByVal netDirectory As DNSTools.DgnNetworkDirectory)
        networkDirectory = netDirectory

        cbType.Items.Add("HTTP Proxy")
        cbType.Items.Add("Tunnel")
        cbType.Items.Add("SOCKS4")
        cbType.Items.Add("SOCKS5")

        UpdateNetdirectoryFromObject()
        UpdateProxySettingsUI()

        txtUser.Focus()

    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        UpdateNetdirectoryFromForm()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdestore.Click
        networkDirectory.RestoreDefaultOptions()
        UpdateNetdirectoryFromObject()
    End Sub

    Private Sub cmdUserProxyServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseProxyServer.CheckedChanged
        UpdateProxySettingsUI()
    End Sub
End Class
