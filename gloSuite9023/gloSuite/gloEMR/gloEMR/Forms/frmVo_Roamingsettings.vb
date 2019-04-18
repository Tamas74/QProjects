Imports System.IO

Public Class frmVo_Roamingsettings
    Inherits System.Windows.Forms.Form

    Private Engine As AxDNSTools.AxDgnEngineControl
    Private WithEvents pnl_tlsSettings As System.Windows.Forms.Panel
    Private WithEvents tlsDictionary As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents cmdOK As System.Windows.Forms.ToolStripButton
    Private WithEvents cmdCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents cmdAdd As System.Windows.Forms.ToolStripButton
    Private WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Private WithEvents cmdEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Public ModalResult As Boolean

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
    Friend WithEvents chkEnable As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkCopyLogToMaster As System.Windows.Forms.CheckBox
    Friend WithEvents chkLimitNetworkTraffic As System.Windows.Forms.CheckBox
    Friend WithEvents chkRestrictLocalUserAccess As System.Windows.Forms.CheckBox
    Friend WithEvents chkDoNotCopyDraFiles As System.Windows.Forms.CheckBox
    Friend WithEvents chkAudioSettingsOverride As System.Windows.Forms.CheckBox
    Friend WithEvents chkAlwaysBreakLock As System.Windows.Forms.CheckBox
    Friend WithEvents chkCopyAcousticAlways As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncorporateVocDelta As System.Windows.Forms.CheckBox
    Friend WithEvents txtLocalCacheDirectory As System.Windows.Forms.TextBox
    Friend WithEvents lbSizePrefix As System.Windows.Forms.Label
    Friend WithEvents txtMaxContainerSize As System.Windows.Forms.TextBox
    Friend WithEvents lbSizeSuffix As System.Windows.Forms.Label
    Friend WithEvents cmdBrowseLocal As System.Windows.Forms.Button
    Friend WithEvents browseDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents clDisplayName As System.Windows.Forms.ColumnHeader
    Friend WithEvents clNetLocation As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvNetworkDirectories As System.Windows.Forms.ListView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lbSizeSuffix = New System.Windows.Forms.Label
        Me.txtMaxContainerSize = New System.Windows.Forms.TextBox
        Me.lbSizePrefix = New System.Windows.Forms.Label
        Me.chkCopyLogToMaster = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtLocalCacheDirectory = New System.Windows.Forms.TextBox
        Me.chkEnable = New System.Windows.Forms.CheckBox
        Me.chkLimitNetworkTraffic = New System.Windows.Forms.CheckBox
        Me.chkRestrictLocalUserAccess = New System.Windows.Forms.CheckBox
        Me.chkDoNotCopyDraFiles = New System.Windows.Forms.CheckBox
        Me.chkAudioSettingsOverride = New System.Windows.Forms.CheckBox
        Me.chkAlwaysBreakLock = New System.Windows.Forms.CheckBox
        Me.chkCopyAcousticAlways = New System.Windows.Forms.CheckBox
        Me.chkIncorporateVocDelta = New System.Windows.Forms.CheckBox
        Me.cmdBrowseLocal = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lvNetworkDirectories = New System.Windows.Forms.ListView
        Me.clDisplayName = New System.Windows.Forms.ColumnHeader
        Me.clNetLocation = New System.Windows.Forms.ColumnHeader
        Me.browseDialog = New System.Windows.Forms.FolderBrowserDialog
        Me.pnl_tlsSettings = New System.Windows.Forms.Panel
        Me.tlsDictionary = New gloGlobal.gloToolStripIgnoreFocus
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton
        Me.cmdEdit = New System.Windows.Forms.ToolStripButton
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton
        Me.cmdOK = New System.Windows.Forms.ToolStripButton
        Me.cmdCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.pnl_tlsSettings.SuspendLayout()
        Me.tlsDictionary.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbSizeSuffix
        '
        Me.lbSizeSuffix.Location = New System.Drawing.Point(354, 397)
        Me.lbSizeSuffix.Name = "lbSizeSuffix"
        Me.lbSizeSuffix.Size = New System.Drawing.Size(24, 18)
        Me.lbSizeSuffix.TabIndex = 14
        Me.lbSizeSuffix.Text = "MB"
        '
        'txtMaxContainerSize
        '
        Me.txtMaxContainerSize.Location = New System.Drawing.Point(420, 451)
        Me.txtMaxContainerSize.MaxLength = 4
        Me.txtMaxContainerSize.Name = "txtMaxContainerSize"
        Me.txtMaxContainerSize.Size = New System.Drawing.Size(44, 22)
        Me.txtMaxContainerSize.TabIndex = 13
        Me.txtMaxContainerSize.Text = "500"
        '
        'lbSizePrefix
        '
        Me.lbSizePrefix.AutoSize = True
        Me.lbSizePrefix.Location = New System.Drawing.Point(186, 456)
        Me.lbSizePrefix.Name = "lbSizePrefix"
        Me.lbSizePrefix.Size = New System.Drawing.Size(233, 14)
        Me.lbSizePrefix.TabIndex = 12
        Me.lbSizePrefix.Text = "Disk space reser&ved for network archive :"
        '
        'chkCopyLogToMaster
        '
        Me.chkCopyLogToMaster.AutoSize = True
        Me.chkCopyLogToMaster.Location = New System.Drawing.Point(186, 224)
        Me.chkCopyLogToMaster.Name = "chkCopyLogToMaster"
        Me.chkCopyLogToMaster.Size = New System.Drawing.Size(182, 18)
        Me.chkCopyLogToMaster.TabIndex = 5
        Me.chkCopyLogToMaster.Text = "Cop&y Dragon log to network"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 192)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&Local directory (for cache) :"
        '
        'txtLocalCacheDirectory
        '
        Me.txtLocalCacheDirectory.Location = New System.Drawing.Point(186, 189)
        Me.txtLocalCacheDirectory.Name = "txtLocalCacheDirectory"
        Me.txtLocalCacheDirectory.Size = New System.Drawing.Size(417, 22)
        Me.txtLocalCacheDirectory.TabIndex = 3
        '
        'chkEnable
        '
        Me.chkEnable.AutoSize = True
        Me.chkEnable.Location = New System.Drawing.Point(20, 16)
        Me.chkEnable.Name = "chkEnable"
        Me.chkEnable.Size = New System.Drawing.Size(62, 18)
        Me.chkEnable.TabIndex = 0
        Me.chkEnable.Text = "&Enable"
        '
        'chkLimitNetworkTraffic
        '
        Me.chkLimitNetworkTraffic.AutoSize = True
        Me.chkLimitNetworkTraffic.Location = New System.Drawing.Point(186, 253)
        Me.chkLimitNetworkTraffic.Name = "chkLimitNetworkTraffic"
        Me.chkLimitNetworkTraffic.Size = New System.Drawing.Size(245, 18)
        Me.chkLimitNetworkTraffic.TabIndex = 6
        Me.chkLimitNetworkTraffic.Text = "Access network at &user open/close only"
        '
        'chkRestrictLocalUserAccess
        '
        Me.chkRestrictLocalUserAccess.AutoSize = True
        Me.chkRestrictLocalUserAccess.Location = New System.Drawing.Point(186, 282)
        Me.chkRestrictLocalUserAccess.Name = "chkRestrictLocalUserAccess"
        Me.chkRestrictLocalUserAccess.Size = New System.Drawing.Size(239, 18)
        Me.chkRestrictLocalUserAccess.TabIndex = 7
        Me.chkRestrictLocalUserAccess.Text = "Allow non-roaming users to be o&pened"
        '
        'chkDoNotCopyDraFiles
        '
        Me.chkDoNotCopyDraFiles.AutoSize = True
        Me.chkDoNotCopyDraFiles.Location = New System.Drawing.Point(186, 311)
        Me.chkDoNotCopyDraFiles.Name = "chkDoNotCopyDraFiles"
        Me.chkDoNotCopyDraFiles.Size = New System.Drawing.Size(209, 18)
        Me.chkDoNotCopyDraFiles.TabIndex = 8
        Me.chkDoNotCopyDraFiles.Text = "&Conserve archive size on network"
        '
        'chkAudioSettingsOverride
        '
        Me.chkAudioSettingsOverride.AutoSize = True
        Me.chkAudioSettingsOverride.Location = New System.Drawing.Point(186, 340)
        Me.chkAudioSettingsOverride.Name = "chkAudioSettingsOverride"
        Me.chkAudioSettingsOverride.Size = New System.Drawing.Size(301, 18)
        Me.chkAudioSettingsOverride.TabIndex = 9
        Me.chkAudioSettingsOverride.Text = "&Set audio levels on each machine (recommended)"
        '
        'chkAlwaysBreakLock
        '
        Me.chkAlwaysBreakLock.AutoSize = True
        Me.chkAlwaysBreakLock.Location = New System.Drawing.Point(186, 369)
        Me.chkAlwaysBreakLock.Name = "chkAlwaysBreakLock"
        Me.chkAlwaysBreakLock.Size = New System.Drawing.Size(358, 18)
        Me.chkAlwaysBreakLock.TabIndex = 10
        Me.chkAlwaysBreakLock.Text = "As&k before breaking locks on network users (recommended)"
        '
        'chkCopyAcousticAlways
        '
        Me.chkCopyAcousticAlways.AutoSize = True
        Me.chkCopyAcousticAlways.Location = New System.Drawing.Point(186, 398)
        Me.chkCopyAcousticAlways.Name = "chkCopyAcousticAlways"
        Me.chkCopyAcousticAlways.Size = New System.Drawing.Size(272, 18)
        Me.chkCopyAcousticAlways.TabIndex = 11
        Me.chkCopyAcousticAlways.Text = "Al&ways copy acoustic information to network"
        '
        'chkIncorporateVocDelta
        '
        Me.chkIncorporateVocDelta.AutoSize = True
        Me.chkIncorporateVocDelta.Location = New System.Drawing.Point(186, 427)
        Me.chkIncorporateVocDelta.Name = "chkIncorporateVocDelta"
        Me.chkIncorporateVocDelta.Size = New System.Drawing.Size(386, 18)
        Me.chkIncorporateVocDelta.TabIndex = 12
        Me.chkIncorporateVocDelta.Text = "Mer&ge contents of vocdelta.dat into network user when file is full"
        '
        'cmdBrowseLocal
        '
        Me.cmdBrowseLocal.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.cmdBrowseLocal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdBrowseLocal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdBrowseLocal.Location = New System.Drawing.Point(609, 189)
        Me.cmdBrowseLocal.Name = "cmdBrowseLocal"
        Me.cmdBrowseLocal.Size = New System.Drawing.Size(23, 23)
        Me.cmdBrowseLocal.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lvNetworkDirectories)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 43)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(652, 136)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "&Network Directories"
        '
        'lvNetworkDirectories
        '
        Me.lvNetworkDirectories.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clDisplayName, Me.clNetLocation})
        Me.lvNetworkDirectories.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvNetworkDirectories.FullRowSelect = True
        Me.lvNetworkDirectories.Location = New System.Drawing.Point(8, 21)
        Me.lvNetworkDirectories.MultiSelect = False
        Me.lvNetworkDirectories.Name = "lvNetworkDirectories"
        Me.lvNetworkDirectories.Size = New System.Drawing.Size(638, 109)
        Me.lvNetworkDirectories.TabIndex = 0
        Me.lvNetworkDirectories.UseCompatibleStateImageBehavior = False
        Me.lvNetworkDirectories.View = System.Windows.Forms.View.Details
        '
        'clDisplayName
        '
        Me.clDisplayName.Text = "Display Name"
        Me.clDisplayName.Width = 154
        '
        'clNetLocation
        '
        Me.clNetLocation.Text = "Network Location"
        Me.clNetLocation.Width = 325
        '
        'pnl_tlsSettings
        '
        Me.pnl_tlsSettings.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsSettings.Controls.Add(Me.tlsDictionary)
        Me.pnl_tlsSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsSettings.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_tlsSettings.Name = "pnl_tlsSettings"
        Me.pnl_tlsSettings.Size = New System.Drawing.Size(676, 54)
        Me.pnl_tlsSettings.TabIndex = 6
        '
        'tlsDictionary
        '
        Me.tlsDictionary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsDictionary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDictionary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDictionary.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDictionary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.cmdEdit, Me.cmdDelete, Me.cmdOK, Me.cmdCancel})
        Me.tlsDictionary.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDictionary.Location = New System.Drawing.Point(0, 0)
        Me.tlsDictionary.Name = "tlsDictionary"
        Me.tlsDictionary.Size = New System.Drawing.Size(676, 53)
        Me.tlsDictionary.TabIndex = 0
        Me.tlsDictionary.Text = "toolStrip1"
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(36, 50)
        Me.cmdAdd.Tag = "Add"
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdAdd.ToolTipText = "Add"
        '
        'cmdEdit
        '
        Me.cmdEdit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.Image = Global.gloEMR.My.Resources.Resources.Modify
        Me.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(36, 50)
        Me.cmdEdit.Tag = "Edit"
        Me.cmdEdit.Text = "&Edit"
        Me.cmdEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdEdit.ToolTipText = "Edit"
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Image = Global.gloEMR.My.Resources.Resources.Delete
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(50, 50)
        Me.cmdDelete.Tag = "Delete"
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdDelete.ToolTipText = "Delete"
        '
        'cmdOK
        '
        Me.cmdOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.Image = Global.gloEMR.My.Resources.Resources.Close
        Me.cmdOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(66, 50)
        Me.cmdOK.Tag = "Ok"
        Me.cmdOK.Text = "&Save&&Cls"
        Me.cmdOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdOK.ToolTipText = "Save and Close"
        '
        'cmdCancel
        '
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Image = Global.gloEMR.My.Resources.Resources.Close01
        Me.cmdCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(43, 50)
        Me.cmdCancel.Tag = "Cancel"
        Me.cmdCancel.Text = "&Close"
        Me.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbSizeSuffix)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtMaxContainerSize)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lbSizePrefix)
        Me.Panel1.Controls.Add(Me.txtLocalCacheDirectory)
        Me.Panel1.Controls.Add(Me.chkCopyLogToMaster)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.chkLimitNetworkTraffic)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.chkIncorporateVocDelta)
        Me.Panel1.Controls.Add(Me.chkEnable)
        Me.Panel1.Controls.Add(Me.chkRestrictLocalUserAccess)
        Me.Panel1.Controls.Add(Me.cmdBrowseLocal)
        Me.Panel1.Controls.Add(Me.chkCopyAcousticAlways)
        Me.Panel1.Controls.Add(Me.chkAudioSettingsOverride)
        Me.Panel1.Controls.Add(Me.chkDoNotCopyDraFiles)
        Me.Panel1.Controls.Add(Me.chkAlwaysBreakLock)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(676, 487)
        Me.Panel1.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(672, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 479)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 479)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(3, 483)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(670, 1)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(3, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(670, 1)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "label1"
        '
        'frmVo_Roamingsettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(676, 541)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsSettings)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVo_Roamingsettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Administrative Settings"
        Me.GroupBox2.ResumeLayout(False)
        Me.pnl_tlsSettings.ResumeLayout(False)
        Me.pnl_tlsSettings.PerformLayout()
        Me.tlsDictionary.ResumeLayout(False)
        Me.tlsDictionary.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Initialize(ByRef EngineControl As AxDNSTools.AxDgnEngineControl)
        Engine = EngineControl
        ModalResult = False

        ' Update the UI.
        txtLocalCacheDirectory.Text = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionLocalCacheDirectory)
        txtMaxContainerSize.Text = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionMaxContainerSizeMb)

        chkEnable.Checked = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRoamingUserOn)
        chkCopyLogToMaster.Checked = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionCopyLogToMaster)
        chkLimitNetworkTraffic.Checked = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionLimitedNetworkTraffic)
        chkRestrictLocalUserAccess.Checked = Not Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRestrictLocalUserAccess)
        chkDoNotCopyDraFiles.Checked = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionDoNotCopyDraFiles)
        chkAudioSettingsOverride.Checked = Not Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionAudioSettingsOverride)
        chkAlwaysBreakLock.Checked = Not Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionAlwaysBreakLock)
        chkCopyAcousticAlways.Checked = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionCopyAcousticAlways)
        chkIncorporateVocDelta.Checked = Engine.get_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionIncorporateVocDelta)

        updateNetworkDirectoriesList()

        UpdateUI()
    End Sub

    'Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Cancel.Click, cmdCancel.Click
    '    Me.DialogResult = DialogResult.Cancel
    '    Me.Close()
    'End Sub

    Private Sub chkEnable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnable.CheckedChanged
        UpdateUI()
    End Sub

    Private Sub UpdateUI()
        txtLocalCacheDirectory.Enabled = chkEnable.Checked
        txtMaxContainerSize.Enabled = chkEnable.Checked

        chkCopyLogToMaster.Enabled = chkEnable.Checked
        chkLimitNetworkTraffic.Enabled = chkEnable.Checked
        chkRestrictLocalUserAccess.Enabled = chkEnable.Checked
        chkDoNotCopyDraFiles.Enabled = chkEnable.Checked
        chkAudioSettingsOverride.Enabled = chkEnable.Checked
        chkAlwaysBreakLock.Enabled = chkEnable.Checked
        chkCopyAcousticAlways.Enabled = chkEnable.Checked
        chkIncorporateVocDelta.Enabled = chkEnable.Checked

        lbSizePrefix.Enabled = chkEnable.Checked
        lbSizeSuffix.Enabled = chkEnable.Checked

        cmdBrowseLocal.Enabled = chkEnable.Checked
        GroupBox2.Enabled = chkEnable.Checked
        If (Not GroupBox2.Enabled) Then
            lvNetworkDirectories.BackColor = SystemColors.Control
        Else
            lvNetworkDirectories.BackColor = SystemColors.Window
        End If

        EnableEditListBoxCmd()
    End Sub

    'Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Ok.Click, cmdOK.Click
    '        ' Do not perform any changes if the Roaming User functionality is
    '        ' turned off.
    '        If chkEnable.Checked Then
    '            ' The RU functionality can't be enabled without specifying at least one
    '            ' network location where user files are stored
    '            If Engine.RoamingUserNetworkDirectories.Count = 0 Then
    '                MsgBox("Please add at least one network directory.", MsgBoxStyle.Information, Me.Text)
    '                cmdAdd1.Focus()
    '                Return
    '            End If

    '            If txtLocalCacheDirectory.Text = "" Then
    '                MsgBox("Please specify the Local Directory.", MsgBoxStyle.Information, Me.Text)
    '                Return
    '            End If

    '            If Not isAbsoluteLocalPath(txtLocalCacheDirectory.Text) Then
    '                MsgBox("Please specify absolute path for Local Directory.", MsgBoxStyle.Information, Me.Text)
    '                Return
    '            End If

    '            If Not isPositiveNumber(txtMaxContainerSize.Text) Then
    '                MsgBox("Incorrect disk space value entered. Please enter a non-negative integer.", _
    '                    vbInformation, Me.Text)
    '                txtMaxContainerSize.Focus()
    '                Exit Sub
    '            End If

    '            On Error GoTo IncorrectLocalDir
    '            If Not Directory.Exists(txtLocalCacheDirectory.Text) Then
    '                ' Trying to create Local Directory.
    '                Directory.CreateDirectory(txtLocalCacheDirectory.Text)
    '            End If

    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionLocalCacheDirectory, txtLocalCacheDirectory.Text)

    '            On Error GoTo Cannot_Change_Settings
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRoamingUserOn, chkEnable.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionCopyLogToMaster, chkCopyLogToMaster.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionLimitedNetworkTraffic, chkLimitNetworkTraffic.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRestrictLocalUserAccess, Not chkRestrictLocalUserAccess.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionDoNotCopyDraFiles, chkDoNotCopyDraFiles.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionAudioSettingsOverride, Not chkAudioSettingsOverride.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionAlwaysBreakLock, Not chkAlwaysBreakLock.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionCopyAcousticAlways, chkCopyAcousticAlways.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionIncorporateVocDelta, chkIncorporateVocDelta.Checked)
    '            Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionMaxContainerSizeMb, CInt(txtMaxContainerSize.Text))
    '        End If

    '        Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRoamingUserOn, chkEnable.Checked)

    '        Me.DialogResult = DialogResult.OK
    '        Me.Close()
    '        Return

    'IncorrectLocalDir:
    '        MsgBox("Unable to set Local Directory. The directory " + txtLocalCacheDirectory.Text + _
    '                " does not exist or the path is invalid." + vbCrLf + "Error details : " + _
    '                Err.Description, MsgBoxStyle.Critical, Me.Text)
    '        Return

    'Cannot_Change_Settings:
    '        MsgBox("Cannot change the settings" & vbCrLf & _
    '            "Reason : " & Err.Description, MsgBoxStyle.Critical, Me.Text)
    '        Return
    ' End Sub

    Private Sub cmdBrowseLocal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseLocal.Click
        browseDialog.Description = "Select Local directory for cache:"
        browseDialog.SelectedPath = txtLocalCacheDirectory.Text
        browseDialog.ShowNewFolderButton = True

        If (browseDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK) Then
            txtLocalCacheDirectory.Text = browseDialog.SelectedPath
        End If
    End Sub

    Private Function isPositiveNumber(ByVal text As String) As Boolean
        Dim res As Integer

        On Error GoTo IncorrectFormat
        ' Only positive numeric values are accepted.
        res = CInt(text)
        If (res >= 0) Then
            ' All is correct.
            Return True
        End If

IncorrectFormat:
        Return False
    End Function

    Private Function isAbsoluteLocalPath(ByVal path As String)
        Dim drive As Char
        drive = path.Chars(0)

        ' Absolute local path should have letter character in the first position
        ' and ":\" or ":/" string in the second position (after the drive name).
        Return ((drive >= "a" And drive <= "z") Or (drive >= "A" And drive <= "Z")) And _
            (path.IndexOf(":\") = 1 Or path.IndexOf(":/") = 1)
    End Function

    'Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
    '    editNetworkDirectory(True, New DNSTools.DgnNetworkDirectory)
    'End Sub

    '    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click, cmdDelete.Click
    '        On Error GoTo Cannot_Remove_Network_Directory

    '        If (lvNetworkDirectories.SelectedItems.Count = 0) Then
    '            MsgBox("Please select a network directory", MsgBoxStyle.Information, Me.Text)
    '            Return
    '        End If

    '        Engine.RoamingUserNetworkDirectories.Remove(lvNetworkDirectories.SelectedIndices(0))
    '        updateNetworkDirectoriesList()
    '        Return

    'Cannot_Remove_Network_Directory:
    '        MsgBox("Cannot remove the network directory" & vbCrLf & _
    '             "Reason : " & Err.Description, MsgBoxStyle.Critical, Me.Text)
    '    End Sub

    'Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click, cmdEdit.Click
    '    If (lvNetworkDirectories.SelectedItems.Count = 0) Then
    '        MsgBox("Please select a network directory", MsgBoxStyle.Information, Me.Text)
    '        Return
    '    End If

    '    editNetworkDirectory(False, Engine.RoamingUserNetworkDirectories.Item(lvNetworkDirectories.SelectedIndices(0)))
    'End Sub

    Private Sub editNetworkDirectory(ByVal bNewDirectory As Boolean, ByVal directory As DNSTools.IDgnNetworkDirectory)
        Dim NetLocationForm As New frmVo_NetLocation
        NetLocationForm.Initialize(directory, Engine, bNewDirectory)

        If (NetLocationForm.ShowDialog(IIf(IsNothing(NetLocationForm.Parent), Me, NetLocationForm.Parent)) = DialogResult.OK) Then
            updateNetworkDirectoriesList()
        End If
        NetLocationForm.Dispose()
        NetLocationForm = Nothing
    End Sub

    Private Sub updateNetworkDirectoriesList()
        lvNetworkDirectories.Items.Clear()

        Dim networkDirectories As DNSTools.IDgnNetworkDirectories
        networkDirectories = Engine.RoamingUserNetworkDirectories

        ' Fill the directories.
        Dim networkDirectory As DNSTools.IDgnNetworkDirectory
        For Each networkDirectory In networkDirectories
            Dim item As New ListViewItem(networkDirectory.DisplayName)
            item.SubItems.Add(networkDirectory.Location)
            lvNetworkDirectories.Items.Add(item)
        Next networkDirectory
    End Sub

    Private Sub EnableEditListBoxCmd()
        cmdDelete.Enabled = lvNetworkDirectories.SelectedItems.Count > 0
        cmdEdit.Enabled = lvNetworkDirectories.SelectedItems.Count > 0
    End Sub

    Private Sub lvNetworkDirectories_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvNetworkDirectories.SelectedIndexChanged
        EnableEditListBoxCmd()
    End Sub

    Private Sub tlsDictionary_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDictionary.ItemClicked
        Select Case e.ClickedItem.Tag

            Case "Add"
                editNetworkDirectory(True, New DNSTools.DgnNetworkDirectory)

            Case "Edit"
                If (lvNetworkDirectories.SelectedItems.Count = 0) Then
                    MsgBox("Please select a network directory", MsgBoxStyle.Information, Me.Text)
                    Return
                End If

                editNetworkDirectory(False, Engine.RoamingUserNetworkDirectories.Item(lvNetworkDirectories.SelectedIndices(0)))

            Case "Delete"
                On Error GoTo Cannot_Remove_Network_Directory

                If (lvNetworkDirectories.SelectedItems.Count = 0) Then
                    MsgBox("Please select a network directory", MsgBoxStyle.Information, Me.Text)
                    Return
                End If

                Engine.RoamingUserNetworkDirectories.Remove(lvNetworkDirectories.SelectedIndices(0))
                updateNetworkDirectoriesList()
                Return

Cannot_Remove_Network_Directory:
                MsgBox("Cannot remove the network directory" & vbCrLf & _
                     "Reason : " & Err.Description, MsgBoxStyle.Critical, Me.Text)
            Case "Ok"
                ' Do not perform any changes if the Roaming User functionality is
                ' turned off.
                If chkEnable.Checked Then
                    ' The RU functionality can't be enabled without specifying at least one
                    ' network location where user files are stored
                    If Engine.RoamingUserNetworkDirectories.Count = 0 Then
                        MsgBox("Please add at least one network directory.", MsgBoxStyle.Information, Me.Text)
                        'cmdAdd1.Focus()
                        Return
                    End If

                    If txtLocalCacheDirectory.Text = "" Then
                        MsgBox("Please specify the Local Directory.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If

                    If Not isAbsoluteLocalPath(txtLocalCacheDirectory.Text) Then
                        MsgBox("Please specify absolute path for Local Directory.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If

                    If Not isPositiveNumber(txtMaxContainerSize.Text) Then
                        MsgBox("Incorrect disk space value entered. Please enter a non-negative integer.", _
                            vbInformation, Me.Text)
                        txtMaxContainerSize.Focus()
                        Exit Sub
                    End If

                    On Error GoTo IncorrectLocalDir
                    If Not Directory.Exists(txtLocalCacheDirectory.Text) Then
                        ' Trying to create Local Directory.
                        Directory.CreateDirectory(txtLocalCacheDirectory.Text)
                    End If

                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionLocalCacheDirectory, txtLocalCacheDirectory.Text)

                    On Error GoTo Cannot_Change_Settings
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRoamingUserOn, chkEnable.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionCopyLogToMaster, chkCopyLogToMaster.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionLimitedNetworkTraffic, chkLimitNetworkTraffic.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRestrictLocalUserAccess, Not chkRestrictLocalUserAccess.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionDoNotCopyDraFiles, chkDoNotCopyDraFiles.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionAudioSettingsOverride, Not chkAudioSettingsOverride.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionAlwaysBreakLock, Not chkAlwaysBreakLock.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionCopyAcousticAlways, chkCopyAcousticAlways.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionIncorporateVocDelta, chkIncorporateVocDelta.Checked)
                    Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionMaxContainerSizeMb, CInt(txtMaxContainerSize.Text))
                End If

                Engine.set_RoamingUserOption(DNSTools.DgnRoamingUserOptionConstants.dgnruoptionRoamingUserOn, chkEnable.Checked)

                Me.DialogResult = DialogResult.OK
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                Return

IncorrectLocalDir:
                MsgBox("Unable to set Local Directory. The directory " + txtLocalCacheDirectory.Text + _
                        " does not exist or the path is invalid." + vbCrLf + "Error details : " + _
                        Err.Description, MsgBoxStyle.Critical, Me.Text)
                Return

Cannot_Change_Settings:
                MsgBox("Cannot change the settings" & vbCrLf & _
                    "Reason : " & Err.Description, MsgBoxStyle.Critical, Me.Text)
                Return
            Case "Cancel"
                Me.DialogResult = DialogResult.Cancel
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select
    End Sub
End Class
