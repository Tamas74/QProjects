<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInfoButtonBrowser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInfoButtonBrowser))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InfoButtonWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tls_gloCommunityDashboard = New gloGlobal.gloToolStripIgnoreFocus()
        Me.homeButton = New System.Windows.Forms.ToolStripButton()
        Me.backButton = New System.Windows.Forms.ToolStripButton()
        Me.forwardButton = New System.Windows.Forms.ToolStripButton()
        Me.refreshButton = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ts_SendToPortal = New System.Windows.Forms.ToolStripButton()
        Me.ts_Saveandclose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlOpenInfobuttonLinks = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.trvLinks = New System.Windows.Forms.TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnllblInfobuttonLink = New System.Windows.Forms.Panel()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.lblInfobuttonLink = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.panel23 = New System.Windows.Forms.Panel()
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.lblTags = New System.Windows.Forms.Label()
        Me.btnTag_Up = New System.Windows.Forms.Button()
        Me.btnTag_Down = New System.Windows.Forms.Button()
        Me.label34 = New System.Windows.Forms.Label()
        Me.label35 = New System.Windows.Forms.Label()
        Me.label36 = New System.Windows.Forms.Label()
        Me.label38 = New System.Windows.Forms.Label()
        Me.pnlLinks = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.tls_gloCommunityDashboard.SuspendLayout()
        Me.pnlOpenInfobuttonLinks.SuspendLayout()
        Me.pnllblInfobuttonLink.SuspendLayout()
        Me.panel23.SuspendLayout()
        Me.panel6.SuspendLayout()
        Me.pnlLinks.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.InfoButtonWebBrowser)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(272, 58)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1085, 519)
        Me.Panel1.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(1081, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 512)
        Me.Label4.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(0, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 512)
        Me.Label3.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(0, 515)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1082, 1)
        Me.Label2.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(1, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1080, 1)
        Me.Label1.TabIndex = 3
        '
        'InfoButtonWebBrowser
        '
        Me.InfoButtonWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InfoButtonWebBrowser.Location = New System.Drawing.Point(0, 3)
        Me.InfoButtonWebBrowser.Margin = New System.Windows.Forms.Padding(2)
        Me.InfoButtonWebBrowser.MinimumSize = New System.Drawing.Size(17, 17)
        Me.InfoButtonWebBrowser.Name = "InfoButtonWebBrowser"
        Me.InfoButtonWebBrowser.ScriptErrorsSuppressed = True
        Me.InfoButtonWebBrowser.Size = New System.Drawing.Size(1082, 513)
        Me.InfoButtonWebBrowser.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tls_gloCommunityDashboard)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1357, 58)
        Me.Panel2.TabIndex = 2
        '
        'tls_gloCommunityDashboard
        '
        Me.tls_gloCommunityDashboard.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tls_gloCommunityDashboard.BackgroundImage = CType(resources.GetObject("tls_gloCommunityDashboard.BackgroundImage"), System.Drawing.Image)
        Me.tls_gloCommunityDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_gloCommunityDashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tls_gloCommunityDashboard.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_gloCommunityDashboard.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_gloCommunityDashboard.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_gloCommunityDashboard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.homeButton, Me.backButton, Me.forwardButton, Me.refreshButton, Me.ts_btnPrint, Me.ts_SendToPortal, Me.ts_Saveandclose, Me.ts_btnClose})
        Me.tls_gloCommunityDashboard.Location = New System.Drawing.Point(0, 0)
        Me.tls_gloCommunityDashboard.Name = "tls_gloCommunityDashboard"
        Me.tls_gloCommunityDashboard.Size = New System.Drawing.Size(1357, 58)
        Me.tls_gloCommunityDashboard.TabIndex = 1
        Me.tls_gloCommunityDashboard.Text = "ToolStrip1"
        '
        'homeButton
        '
        Me.homeButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.homeButton.Image = CType(resources.GetObject("homeButton.Image"), System.Drawing.Image)
        Me.homeButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.homeButton.Name = "homeButton"
        Me.homeButton.Size = New System.Drawing.Size(46, 55)
        Me.homeButton.Tag = "Home"
        Me.homeButton.Text = "&Home"
        Me.homeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'backButton
        '
        Me.backButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.backButton.Image = CType(resources.GetObject("backButton.Image"), System.Drawing.Image)
        Me.backButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.backButton.Name = "backButton"
        Me.backButton.Size = New System.Drawing.Size(63, 55)
        Me.backButton.Tag = "Previous"
        Me.backButton.Text = "&Previous"
        Me.backButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'forwardButton
        '
        Me.forwardButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.forwardButton.Image = CType(resources.GetObject("forwardButton.Image"), System.Drawing.Image)
        Me.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.forwardButton.Name = "forwardButton"
        Me.forwardButton.Size = New System.Drawing.Size(39, 55)
        Me.forwardButton.Tag = "Next"
        Me.forwardButton.Text = "&Next"
        Me.forwardButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'refreshButton
        '
        Me.refreshButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.refreshButton.Image = CType(resources.GetObject("refreshButton.Image"), System.Drawing.Image)
        Me.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.refreshButton.Name = "refreshButton"
        Me.refreshButton.Size = New System.Drawing.Size(58, 55)
        Me.refreshButton.Tag = "Refresh"
        Me.refreshButton.Text = "&Refresh"
        Me.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 55)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_SendToPortal
        '
        Me.ts_SendToPortal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_SendToPortal.Image = CType(resources.GetObject("ts_SendToPortal.Image"), System.Drawing.Image)
        Me.ts_SendToPortal.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_SendToPortal.Name = "ts_SendToPortal"
        Me.ts_SendToPortal.Size = New System.Drawing.Size(101, 55)
        Me.ts_SendToPortal.Tag = "SendToPortal"
        Me.ts_SendToPortal.Text = "Sen&d to Portal"
        Me.ts_SendToPortal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_Saveandclose
        '
        Me.ts_Saveandclose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_Saveandclose.Image = CType(resources.GetObject("ts_Saveandclose.Image"), System.Drawing.Image)
        Me.ts_Saveandclose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_Saveandclose.Name = "ts_Saveandclose"
        Me.ts_Saveandclose.Size = New System.Drawing.Size(66, 55)
        Me.ts_Saveandclose.Tag = "Save&Close"
        Me.ts_Saveandclose.Text = "&Save&&Cls"
        Me.ts_Saveandclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_Saveandclose.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 55)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlOpenInfobuttonLinks
        '
        Me.pnlOpenInfobuttonLinks.Controls.Add(Me.trvLinks)
        Me.pnlOpenInfobuttonLinks.Controls.Add(Me.Label6)
        Me.pnlOpenInfobuttonLinks.Controls.Add(Me.Label5)
        Me.pnlOpenInfobuttonLinks.Controls.Add(Me.Label9)
        Me.pnlOpenInfobuttonLinks.Controls.Add(Me.Label11)
        Me.pnlOpenInfobuttonLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlOpenInfobuttonLinks.Location = New System.Drawing.Point(3, 29)
        Me.pnlOpenInfobuttonLinks.Name = "pnlOpenInfobuttonLinks"
        Me.pnlOpenInfobuttonLinks.Size = New System.Drawing.Size(266, 602)
        Me.pnlOpenInfobuttonLinks.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(266, 1)
        Me.Label11.TabIndex = 144
        Me.Label11.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(265, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 601)
        Me.Label9.TabIndex = 145
        Me.Label9.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 601)
        Me.Label5.TabIndex = 146
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 601)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(264, 1)
        Me.Label6.TabIndex = 147
        Me.Label6.Text = "label2"
        '
        'trvLinks
        '
        Me.trvLinks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvLinks.Indent = 20
        Me.trvLinks.ItemHeight = 20
        Me.trvLinks.Location = New System.Drawing.Point(1, 1)
        Me.trvLinks.Name = "trvLinks"
        Me.trvLinks.ShowLines = False
        Me.trvLinks.ShowPlusMinus = False
        Me.trvLinks.ShowRootLines = False
        Me.trvLinks.Size = New System.Drawing.Size(264, 600)
        Me.trvLinks.TabIndex = 148
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(1, "ICD 9_01.ico")
        Me.imgTreeView.Images.SetKeyName(2, "CPT_01.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Drugs.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Tag.ico")
        Me.imgTreeView.Images.SetKeyName(5, "Pat Education.ico")
        Me.imgTreeView.Images.SetKeyName(6, "ICD9 Association.ico")
        Me.imgTreeView.Images.SetKeyName(7, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(8, "FLow sheet.ico")
        Me.imgTreeView.Images.SetKeyName(9, "Lab orders.ico")
        Me.imgTreeView.Images.SetKeyName(10, "Radiology Orders.ico")
        Me.imgTreeView.Images.SetKeyName(11, "Refferal.ico")
        Me.imgTreeView.Images.SetKeyName(12, "Template.ico")
        Me.imgTreeView.Images.SetKeyName(13, "Doctor Speaker.ico")
        Me.imgTreeView.Images.SetKeyName(14, "Surescripts.ico")
        Me.imgTreeView.Images.SetKeyName(15, "ICD10GalleryGreen.png")
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(269, 58)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 634)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'pnllblInfobuttonLink
        '
        Me.pnllblInfobuttonLink.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnllblInfobuttonLink.Controls.Add(Me.btnCopy)
        Me.pnllblInfobuttonLink.Controls.Add(Me.lblInfobuttonLink)
        Me.pnllblInfobuttonLink.Controls.Add(Me.Label23)
        Me.pnllblInfobuttonLink.Controls.Add(Me.Label24)
        Me.pnllblInfobuttonLink.Controls.Add(Me.Label25)
        Me.pnllblInfobuttonLink.Controls.Add(Me.Label26)
        Me.pnllblInfobuttonLink.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnllblInfobuttonLink.Location = New System.Drawing.Point(272, 603)
        Me.pnllblInfobuttonLink.Name = "pnllblInfobuttonLink"
        Me.pnllblInfobuttonLink.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnllblInfobuttonLink.Size = New System.Drawing.Size(1085, 89)
        Me.pnllblInfobuttonLink.TabIndex = 28
        '
        'btnCopy
        '
        Me.btnCopy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopy.BackgroundImage = CType(resources.GetObject("btnCopy.BackgroundImage"), System.Drawing.Image)
        Me.btnCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCopy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCopy.Location = New System.Drawing.Point(940, 10)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(133, 27)
        Me.btnCopy.TabIndex = 15
        Me.btnCopy.Text = "Copy to Clipboard"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'lblInfobuttonLink
        '
        Me.lblInfobuttonLink.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfobuttonLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblInfobuttonLink.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfobuttonLink.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblInfobuttonLink.Location = New System.Drawing.Point(11, 11)
        Me.lblInfobuttonLink.Name = "lblInfobuttonLink"
        Me.lblInfobuttonLink.Padding = New System.Windows.Forms.Padding(5)
        Me.lblInfobuttonLink.Size = New System.Drawing.Size(923, 65)
        Me.lblInfobuttonLink.TabIndex = 14
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Location = New System.Drawing.Point(1081, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 84)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = "Label23"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 84)
        Me.Label24.TabIndex = 4
        Me.Label24.Text = "Label24"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Location = New System.Drawing.Point(0, 85)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1082, 1)
        Me.Label25.TabIndex = 3
        Me.Label25.Text = "Label25"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1082, 1)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "Label26"
        '
        'panel23
        '
        Me.panel23.Controls.Add(Me.panel6)
        Me.panel23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel23.Location = New System.Drawing.Point(272, 577)
        Me.panel23.Name = "panel23"
        Me.panel23.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.panel23.Size = New System.Drawing.Size(1085, 26)
        Me.panel23.TabIndex = 29
        '
        'panel6
        '
        Me.panel6.BackColor = System.Drawing.Color.Transparent
        Me.panel6.BackgroundImage = CType(resources.GetObject("panel6.BackgroundImage"), System.Drawing.Image)
        Me.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel6.Controls.Add(Me.lblTags)
        Me.panel6.Controls.Add(Me.btnTag_Up)
        Me.panel6.Controls.Add(Me.btnTag_Down)
        Me.panel6.Controls.Add(Me.label34)
        Me.panel6.Controls.Add(Me.label35)
        Me.panel6.Controls.Add(Me.label36)
        Me.panel6.Controls.Add(Me.label38)
        Me.panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel6.Location = New System.Drawing.Point(0, 0)
        Me.panel6.Name = "panel6"
        Me.panel6.Size = New System.Drawing.Size(1082, 23)
        Me.panel6.TabIndex = 0
        '
        'lblTags
        '
        Me.lblTags.BackColor = System.Drawing.Color.Transparent
        Me.lblTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTags.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTags.Location = New System.Drawing.Point(1, 1)
        Me.lblTags.Name = "lblTags"
        Me.lblTags.Size = New System.Drawing.Size(1032, 21)
        Me.lblTags.TabIndex = 2
        Me.lblTags.Text = "  Home URL"
        Me.lblTags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnTag_Up
        '
        Me.btnTag_Up.BackColor = System.Drawing.Color.Transparent
        Me.btnTag_Up.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnTag_Up.FlatAppearance.BorderSize = 0
        Me.btnTag_Up.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTag_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTag_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTag_Up.Image = CType(resources.GetObject("btnTag_Up.Image"), System.Drawing.Image)
        Me.btnTag_Up.Location = New System.Drawing.Point(1033, 1)
        Me.btnTag_Up.Name = "btnTag_Up"
        Me.btnTag_Up.Size = New System.Drawing.Size(24, 21)
        Me.btnTag_Up.TabIndex = 1
        Me.btnTag_Up.UseVisualStyleBackColor = False
        '
        'btnTag_Down
        '
        Me.btnTag_Down.BackColor = System.Drawing.Color.Transparent
        Me.btnTag_Down.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnTag_Down.FlatAppearance.BorderSize = 0
        Me.btnTag_Down.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTag_Down.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTag_Down.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTag_Down.Image = CType(resources.GetObject("btnTag_Down.Image"), System.Drawing.Image)
        Me.btnTag_Down.Location = New System.Drawing.Point(1057, 1)
        Me.btnTag_Down.Name = "btnTag_Down"
        Me.btnTag_Down.Size = New System.Drawing.Size(24, 21)
        Me.btnTag_Down.TabIndex = 0
        Me.btnTag_Down.UseVisualStyleBackColor = False
        '
        'label34
        '
        Me.label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label34.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label34.Location = New System.Drawing.Point(1, 22)
        Me.label34.Name = "label34"
        Me.label34.Size = New System.Drawing.Size(1080, 1)
        Me.label34.TabIndex = 12
        Me.label34.Text = "label2"
        '
        'label35
        '
        Me.label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label35.Dock = System.Windows.Forms.DockStyle.Left
        Me.label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label35.Location = New System.Drawing.Point(0, 1)
        Me.label35.Name = "label35"
        Me.label35.Size = New System.Drawing.Size(1, 22)
        Me.label35.TabIndex = 11
        Me.label35.Text = "label4"
        '
        'label36
        '
        Me.label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label36.Location = New System.Drawing.Point(1081, 1)
        Me.label36.Name = "label36"
        Me.label36.Size = New System.Drawing.Size(1, 22)
        Me.label36.TabIndex = 10
        Me.label36.Text = "label3"
        '
        'label38
        '
        Me.label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label38.Dock = System.Windows.Forms.DockStyle.Top
        Me.label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label38.Location = New System.Drawing.Point(0, 0)
        Me.label38.Name = "label38"
        Me.label38.Size = New System.Drawing.Size(1082, 1)
        Me.label38.TabIndex = 9
        Me.label38.Text = "label1"
        '
        'pnlLinks
        '
        Me.pnlLinks.Controls.Add(Me.pnlOpenInfobuttonLinks)
        Me.pnlLinks.Controls.Add(Me.Panel3)
        Me.pnlLinks.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLinks.Location = New System.Drawing.Point(0, 58)
        Me.pnlLinks.Name = "pnlLinks"
        Me.pnlLinks.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlLinks.Size = New System.Drawing.Size(269, 634)
        Me.pnlLinks.TabIndex = 149
        Me.pnlLinks.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(266, 26)
        Me.Panel3.TabIndex = 31
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(266, 23)
        Me.Panel4.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(264, 21)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "  Resources"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(1, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(264, 1)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 22)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(265, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 22)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(266, 1)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "label1"
        '
        'frmInfoButtonBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1357, 692)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.panel23)
        Me.Controls.Add(Me.pnllblInfobuttonLink)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLinks)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmInfoButtonBrowser"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "InfoButton Document"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tls_gloCommunityDashboard.ResumeLayout(False)
        Me.tls_gloCommunityDashboard.PerformLayout()
        Me.pnlOpenInfobuttonLinks.ResumeLayout(False)
        Me.pnllblInfobuttonLink.ResumeLayout(False)
        Me.panel23.ResumeLayout(False)
        Me.panel6.ResumeLayout(False)
        Me.pnlLinks.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents InfoButtonWebBrowser As System.Windows.Forms.WebBrowser
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tls_gloCommunityDashboard As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents homeButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents backButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents forwardButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents refreshButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_Saveandclose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_SendToPortal As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlOpenInfobuttonLinks As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents trvLinks As System.Windows.Forms.TreeView
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnllblInfobuttonLink As System.Windows.Forms.Panel
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents lblInfobuttonLink As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents panel23 As System.Windows.Forms.Panel
    Private WithEvents panel6 As System.Windows.Forms.Panel
    Private WithEvents lblTags As System.Windows.Forms.Label
    Private WithEvents btnTag_Up As System.Windows.Forms.Button
    Private WithEvents btnTag_Down As System.Windows.Forms.Button
    Private WithEvents label34 As System.Windows.Forms.Label
    Private WithEvents label35 As System.Windows.Forms.Label
    Private WithEvents label36 As System.Windows.Forms.Label
    Private WithEvents label38 As System.Windows.Forms.Label
    Friend WithEvents pnlLinks As System.Windows.Forms.Panel
    Private WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
End Class
