Public Class frmVo_NetLocation
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents browseNetDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtDispName As System.Windows.Forms.TextBox
    Friend WithEvents txtNetAddress As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsSettings As System.Windows.Forms.Panel
    Private WithEvents tlsDictionary As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents cmdEdit As System.Windows.Forms.ToolStripButton
    Private WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Private WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDispName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNetAddress = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.browseNetDialog = New System.Windows.Forms.FolderBrowserDialog
        Me.pnl_tlsSettings = New System.Windows.Forms.Panel
        Me.tlsDictionary = New gloGlobal.gloToolStripIgnoreFocus
        Me.cmdEdit = New System.Windows.Forms.ToolStripButton
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.pnl_tlsSettings.SuspendLayout()
        Me.tlsDictionary.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "&Display Name :"
        '
        'txtDispName
        '
        Me.txtDispName.Location = New System.Drawing.Point(121, 30)
        Me.txtDispName.Name = "txtDispName"
        Me.txtDispName.Size = New System.Drawing.Size(269, 22)
        Me.txtDispName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 14)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "&Address :"
        '
        'txtNetAddress
        '
        Me.txtNetAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAddress.Location = New System.Drawing.Point(88, 24)
        Me.txtNetAddress.Name = "txtNetAddress"
        Me.txtNetAddress.Size = New System.Drawing.Size(240, 22)
        Me.txtNetAddress.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.AutoSize = True
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmdBrowse)
        Me.GroupBox1.Controls.Add(Me.txtNetAddress)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(33, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(403, 179)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Network Location"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(231, 147)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 14)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "(HTTP with SSL)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(47, 147)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(179, 14)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "https://MyServer.com/webdaw"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(231, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 14)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "(UNC Path)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(231, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(141, 14)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "(mapped network drive)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(231, 124)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 14)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "(HTTP)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(47, 101)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 14)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "\\MyComputers\User"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(47, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "S:\ or S:\users"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(47, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(174, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "http://MyServer.com/webdaw"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Examples:"
        '
        'cmdBrowse
        '
        Me.cmdBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.cmdBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowse.Location = New System.Drawing.Point(334, 24)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(23, 23)
        Me.cmdBrowse.TabIndex = 2
        '
        'pnl_tlsSettings
        '
        Me.pnl_tlsSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsSettings.Controls.Add(Me.tlsDictionary)
        Me.pnl_tlsSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsSettings.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_tlsSettings.Name = "pnl_tlsSettings"
        Me.pnl_tlsSettings.Size = New System.Drawing.Size(459, 54)
        Me.pnl_tlsSettings.TabIndex = 7
        '
        'tlsDictionary
        '
        Me.tlsDictionary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsDictionary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDictionary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDictionary.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDictionary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdEdit, Me.cmdDelete, Me.ToolStripButton1, Me.ToolStripButton2})
        Me.tlsDictionary.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDictionary.Location = New System.Drawing.Point(0, 0)
        Me.tlsDictionary.Name = "tlsDictionary"
        Me.tlsDictionary.Size = New System.Drawing.Size(459, 53)
        Me.tlsDictionary.TabIndex = 0
        Me.tlsDictionary.Text = "toolStrip1"
        '
        'cmdEdit
        '
        Me.cmdEdit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.Image = Global.gloEMR.My.Resources.Resources.Modify
        Me.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(42, 50)
        Me.cmdEdit.Tag = "HTTP"
        Me.cmdEdit.Text = "&HTTP"
        Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdEdit.ToolTipText = "HTTP"
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = Global.gloEMR.My.Resources.Resources.Show
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(36, 50)
        Me.cmdDelete.Tag = "SSL"
        Me.cmdDelete.Text = "&SSL"
        Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdDelete.ToolTipText = "SSL"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = Global.gloEMR.My.Resources.Resources.Close
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(66, 50)
        Me.ToolStripButton1.Tag = "Ok"
        Me.ToolStripButton1.Text = "&Save&&Cls"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Save and Close"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.Image = Global.gloEMR.My.Resources.Resources.Close01
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton2.Tag = "Cancel"
        Me.ToolStripButton2.Text = "&Close"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.txtDispName)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(459, 268)
        Me.Panel1.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(455, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 260)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(3, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 260)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Location = New System.Drawing.Point(3, 264)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(453, 1)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(3, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(453, 1)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "label1"
        '
        'frmVo_NetLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(459, 322)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsSettings)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVo_NetLocation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Roaming User Network Location"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnl_tlsSettings.ResumeLayout(False)
        Me.pnl_tlsSettings.PerformLayout()
        Me.tlsDictionary.ResumeLayout(False)
        Me.tlsDictionary.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public networkDirectory As DNSTools.DgnNetworkDirectory
    Public Engine As AxDNSTools.AxDgnEngineControl
    Public bNewDirectory As Boolean

    Public Sub Initialize(ByVal netDirectory As DNSTools.DgnNetworkDirectory, ByVal EngineControl As AxDNSTools.AxDgnEngineControl, ByVal bNew As Boolean)
        networkDirectory = netDirectory
        Engine = EngineControl
        bNewDirectory = bNew

        txtDispName.Text = networkDirectory.DisplayName
        txtNetAddress.Text = networkDirectory.Location
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        browseNetDialog.Description = "Select Network directory:"
        browseNetDialog.ShowNewFolderButton = True

        If (browseNetDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK) Then
            txtNetAddress.Text = browseNetDialog.SelectedPath
        End If
    End Sub

    Private Sub cmdHTTP_Settings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        If (txtNetAddress.Text.IndexOf("http")) Then
            MsgBox("You must put http in the path of the network location.", MsgBoxStyle.Information, Me.Text)
        Else
            Dim HTTPSettingsForm As New frmVo_HttpSettings
            HTTPSettingsForm.Initialize(networkDirectory)
            HTTPSettingsForm.ShowDialog(IIf(IsNothing(HTTPSettingsForm.Parent), Me, HTTPSettingsForm.Parent))
            HTTPSettingsForm.Dispose()
            HTTPSettingsForm = Nothing
        End If
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        On Error GoTo Cannot_Change_Settings

        networkDirectory.DisplayName = txtDispName.Text
        networkDirectory.Location = txtNetAddress.Text

        If (bNewDirectory) Then
            Engine.RoamingUserNetworkDirectories.Add(networkDirectory)
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
        Return

Cannot_Change_Settings:
        MsgBox("Cannot change the settings" & vbCrLf & _
            "Reason : " & Err.Description, MsgBoxStyle.Critical, Me.Text)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdSSLSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If (txtNetAddress.Text.IndexOf("https:")) Then
            MsgBox("You must put https: in the path of the network location.", MsgBoxStyle.Information, Me.Text)
        Else
            Dim SSLSettingsForm As New frmVo_SSLSettings
            SSLSettingsForm.Initialize(networkDirectory)
            SSLSettingsForm.ShowDialog(IIf(IsNothing(SSLSettingsForm.Parent), Me, SSLSettingsForm.Parent))
            SSLSettingsForm.Dispose()
            SSLSettingsForm = Nothing
        End If
    End Sub
End Class
