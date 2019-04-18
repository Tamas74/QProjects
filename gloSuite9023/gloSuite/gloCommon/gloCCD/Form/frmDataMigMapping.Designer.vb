<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataMigMapping
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try


                Try
                    If (IsNothing(dlgOpenFile) = False) Then
                        dlgOpenFile.Dispose()
                        dlgOpenFile = Nothing
                    End If
                Catch ex As Exception

                End Try

                Dim dtpControls As System.Windows.Forms.ContextMenuStrip() = {ContextMenuStrip1}


                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(dtpControls)
                Catch

                End Try

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDataMigMapping))
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.txtVersion = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblUserName = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.BrowseToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SaveToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.CloseToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.pnlMappedFields = New System.Windows.Forms.Panel
        Me.pnlPrintMessage = New System.Windows.Forms.Panel
        Me.lblMessage = New System.Windows.Forms.Label
        Me.lblPleaseWait = New System.Windows.Forms.Label
        Me.pnltr = New System.Windows.Forms.Panel
        Me.Label24 = New System.Windows.Forms.Label
        Me.trMappedFields = New System.Windows.Forms.TreeView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDelHL7Field = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDelgloEMRField = New System.Windows.Forms.ToolStripMenuItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.splleft = New System.Windows.Forms.Splitter
        Me.spltRight = New System.Windows.Forms.Splitter
        Me.pnlgloEMRFields = New System.Windows.Forms.Panel
        Me.trgloEMRFields = New System.Windows.Forms.TreeView
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.tbxSearchgloEMR = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.pnlHL7Fields = New System.Windows.Forms.Panel
        Me.trHL7Fields = New System.Windows.Forms.TreeView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.tbxSearchHL7 = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.tltpHL7Fields = New System.Windows.Forms.ToolTip(Me.components)
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog
        Me.pnlTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlMappedFields.SuspendLayout()
        Me.pnlPrintMessage.SuspendLayout()
        Me.pnltr.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlgloEMRFields.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.pnlHL7Fields.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.TextBox1)
        Me.pnlTop.Controls.Add(Me.txtVersion)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.lblUserName)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label30)
        Me.pnlTop.Controls.Add(Me.Label31)
        Me.pnlTop.Controls.Add(Me.Label32)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 54)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlTop.Size = New System.Drawing.Size(1264, 40)
        Me.pnlTop.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(108, 39)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(163, 22)
        Me.TextBox1.TabIndex = 21
        Me.TextBox1.Visible = False
        '
        'txtVersion
        '
        Me.txtVersion.Enabled = False
        Me.txtVersion.Location = New System.Drawing.Point(108, 10)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(163, 22)
        Me.txtVersion.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(27, 14)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 14)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "CCR version :"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserName.Location = New System.Drawing.Point(31, 43)
        Me.lblUserName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(74, 14)
        Me.lblUserName.TabIndex = 18
        Me.lblUserName.Text = "User Name :"
        Me.lblUserName.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1256, 1)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "label2"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 4)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 36)
        Me.Label30.TabIndex = 14
        Me.Label30.Text = "label4"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(1260, 4)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 36)
        Me.Label31.TabIndex = 13
        Me.Label31.Text = "label3"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(3, 3)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1258, 1)
        Me.Label32.TabIndex = 12
        Me.Label32.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ToolStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1264, 54)
        Me.Panel1.TabIndex = 4
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = CType(resources.GetObject("ToolStrip1.BackgroundImage"), System.Drawing.Image)
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BrowseToolStripButton, Me.SaveToolStripButton1, Me.CloseToolStripButton2})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1264, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BrowseToolStripButton
        '
        Me.BrowseToolStripButton.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BrowseToolStripButton.Image = CType(resources.GetObject("BrowseToolStripButton.Image"), System.Drawing.Image)
        Me.BrowseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BrowseToolStripButton.Name = "BrowseToolStripButton"
        Me.BrowseToolStripButton.Size = New System.Drawing.Size(56, 50)
        Me.BrowseToolStripButton.Text = "&Browse"
        Me.BrowseToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BrowseToolStripButton.ToolTipText = "Browse"
        '
        'SaveToolStripButton1
        '
        Me.SaveToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveToolStripButton1.Image = CType(resources.GetObject("SaveToolStripButton1.Image"), System.Drawing.Image)
        Me.SaveToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton1.Name = "SaveToolStripButton1"
        Me.SaveToolStripButton1.Size = New System.Drawing.Size(66, 50)
        Me.SaveToolStripButton1.Text = "Sa&ve&&Cls"
        Me.SaveToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.SaveToolStripButton1.ToolTipText = "Save and Close"
        '
        'CloseToolStripButton2
        '
        Me.CloseToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseToolStripButton2.Image = CType(resources.GetObject("CloseToolStripButton2.Image"), System.Drawing.Image)
        Me.CloseToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CloseToolStripButton2.Name = "CloseToolStripButton2"
        Me.CloseToolStripButton2.Size = New System.Drawing.Size(43, 50)
        Me.CloseToolStripButton2.Text = "&Close"
        Me.CloseToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMappedFields
        '
        Me.pnlMappedFields.Controls.Add(Me.pnlPrintMessage)
        Me.pnlMappedFields.Controls.Add(Me.pnltr)
        Me.pnlMappedFields.Controls.Add(Me.splleft)
        Me.pnlMappedFields.Controls.Add(Me.spltRight)
        Me.pnlMappedFields.Controls.Add(Me.pnlgloEMRFields)
        Me.pnlMappedFields.Controls.Add(Me.pnlHL7Fields)
        Me.pnlMappedFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMappedFields.Location = New System.Drawing.Point(0, 94)
        Me.pnlMappedFields.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlMappedFields.Name = "pnlMappedFields"
        Me.pnlMappedFields.Size = New System.Drawing.Size(1264, 892)
        Me.pnlMappedFields.TabIndex = 1
        '
        'pnlPrintMessage
        '
        Me.pnlPrintMessage.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPrintMessage.BackColor = System.Drawing.Color.White
        Me.pnlPrintMessage.BackgroundImage = CType(resources.GetObject("pnlPrintMessage.BackgroundImage"), System.Drawing.Image)
        Me.pnlPrintMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPrintMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPrintMessage.Controls.Add(Me.lblMessage)
        Me.pnlPrintMessage.Controls.Add(Me.lblPleaseWait)
        Me.pnlPrintMessage.Location = New System.Drawing.Point(503, 411)
        Me.pnlPrintMessage.Name = "pnlPrintMessage"
        Me.pnlPrintMessage.Size = New System.Drawing.Size(242, 96)
        Me.pnlPrintMessage.TabIndex = 62
        Me.pnlPrintMessage.Visible = False
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblMessage.Location = New System.Drawing.Point(21, 43)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(213, 16)
        Me.lblMessage.TabIndex = 62
        Me.lblMessage.Text = "Loading Mapping Information… "
        '
        'lblPleaseWait
        '
        Me.lblPleaseWait.AutoSize = True
        Me.lblPleaseWait.BackColor = System.Drawing.Color.Transparent
        Me.lblPleaseWait.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPleaseWait.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPleaseWait.Location = New System.Drawing.Point(19, 10)
        Me.lblPleaseWait.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPleaseWait.MaximumSize = New System.Drawing.Size(119, 19)
        Me.lblPleaseWait.MinimumSize = New System.Drawing.Size(119, 19)
        Me.lblPleaseWait.Name = "lblPleaseWait"
        Me.lblPleaseWait.Size = New System.Drawing.Size(119, 19)
        Me.lblPleaseWait.TabIndex = 18
        Me.lblPleaseWait.Text = "Please wait..."
        '
        'pnltr
        '
        Me.pnltr.Controls.Add(Me.Label24)
        Me.pnltr.Controls.Add(Me.trMappedFields)
        Me.pnltr.Controls.Add(Me.Panel5)
        Me.pnltr.Controls.Add(Me.Label4)
        Me.pnltr.Controls.Add(Me.Label19)
        Me.pnltr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltr.Location = New System.Drawing.Point(354, 0)
        Me.pnltr.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnltr.Name = "pnltr"
        Me.pnltr.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnltr.Size = New System.Drawing.Size(556, 892)
        Me.pnltr.TabIndex = 4
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(1, 888)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(554, 1)
        Me.Label24.TabIndex = 29
        Me.Label24.Text = "label1"
        '
        'trMappedFields
        '
        Me.trMappedFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trMappedFields.ContextMenuStrip = Me.ContextMenuStrip1
        Me.trMappedFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trMappedFields.ForeColor = System.Drawing.Color.Black
        Me.trMappedFields.HideSelection = False
        Me.trMappedFields.ImageIndex = 4
        Me.trMappedFields.ImageList = Me.ImageList1
        Me.trMappedFields.Indent = 20
        Me.trMappedFields.ItemHeight = 20
        Me.trMappedFields.Location = New System.Drawing.Point(1, 28)
        Me.trMappedFields.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.trMappedFields.Name = "trMappedFields"
        Me.trMappedFields.SelectedImageKey = "CSV Files.ico"
        Me.trMappedFields.Size = New System.Drawing.Size(554, 861)
        Me.trMappedFields.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDelHL7Field, Me.mnuDelgloEMRField})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 48)
        '
        'mnuDelHL7Field
        '
        Me.mnuDelHL7Field.Name = "mnuDelHL7Field"
        Me.mnuDelHL7Field.Size = New System.Drawing.Size(176, 22)
        Me.mnuDelHL7Field.Text = "Delete XML Field"
        '
        'mnuDelgloEMRField
        '
        Me.mnuDelgloEMRField.Name = "mnuDelgloEMRField"
        Me.mnuDelgloEMRField.Size = New System.Drawing.Size(176, 22)
        Me.mnuDelgloEMRField.Text = "Delete gloEMRField"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "HL7Segment.ico")
        Me.ImageList1.Images.SetKeyName(1, "Map Field.ico")
        Me.ImageList1.Images.SetKeyName(2, "gloEMR Felid.ico")
        Me.ImageList1.Images.SetKeyName(3, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(4, "CCR.ico")
        Me.ImageList1.Images.SetKeyName(5, "gloEMR01.ico")
        Me.ImageList1.Images.SetKeyName(6, "HL7.ico")
        Me.ImageList1.Images.SetKeyName(7, "Mapping CCD.ico")
        Me.ImageList1.Images.SetKeyName(8, "Mapping CCR.ico")
        Me.ImageList1.Images.SetKeyName(9, "CCD.ico")
        Me.ImageList1.Images.SetKeyName(10, "CSV Files.ico")
        Me.ImageList1.Images.SetKeyName(11, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(12, "arrow_01.ico")
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(1, 3)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(554, 25)
        Me.Panel5.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(554, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Mapped Fields"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(0, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(554, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(554, 1)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(555, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 886)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(0, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 886)
        Me.Label19.TabIndex = 28
        Me.Label19.Text = "label3"
        '
        'splleft
        '
        Me.splleft.Location = New System.Drawing.Point(350, 0)
        Me.splleft.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.splleft.Name = "splleft"
        Me.splleft.Size = New System.Drawing.Size(4, 892)
        Me.splleft.TabIndex = 6
        Me.splleft.TabStop = False
        '
        'spltRight
        '
        Me.spltRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.spltRight.Location = New System.Drawing.Point(910, 0)
        Me.spltRight.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.spltRight.Name = "spltRight"
        Me.spltRight.Size = New System.Drawing.Size(4, 892)
        Me.spltRight.TabIndex = 5
        Me.spltRight.TabStop = False
        '
        'pnlgloEMRFields
        '
        Me.pnlgloEMRFields.Controls.Add(Me.trgloEMRFields)
        Me.pnlgloEMRFields.Controls.Add(Me.Panel4)
        Me.pnlgloEMRFields.Controls.Add(Me.Label25)
        Me.pnlgloEMRFields.Controls.Add(Me.Panel3)
        Me.pnlgloEMRFields.Controls.Add(Me.Label20)
        Me.pnlgloEMRFields.Controls.Add(Me.Label23)
        Me.pnlgloEMRFields.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlgloEMRFields.Location = New System.Drawing.Point(914, 0)
        Me.pnlgloEMRFields.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlgloEMRFields.Name = "pnlgloEMRFields"
        Me.pnlgloEMRFields.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlgloEMRFields.Size = New System.Drawing.Size(350, 892)
        Me.pnlgloEMRFields.TabIndex = 1
        '
        'trgloEMRFields
        '
        Me.trgloEMRFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trgloEMRFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trgloEMRFields.ForeColor = System.Drawing.SystemColors.ControlText
        Me.trgloEMRFields.HideSelection = False
        Me.trgloEMRFields.ImageIndex = 2
        Me.trgloEMRFields.ImageList = Me.ImageList1
        Me.trgloEMRFields.Indent = 20
        Me.trgloEMRFields.ItemHeight = 20
        Me.trgloEMRFields.Location = New System.Drawing.Point(1, 57)
        Me.trgloEMRFields.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.trgloEMRFields.Name = "trgloEMRFields"
        Me.trgloEMRFields.SelectedImageIndex = 2
        Me.trgloEMRFields.Size = New System.Drawing.Size(345, 831)
        Me.trgloEMRFields.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.tbxSearchgloEMR)
        Me.Panel4.Controls.Add(Me.Label26)
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.PictureBox1)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.Black
        Me.Panel4.Location = New System.Drawing.Point(1, 28)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(345, 29)
        Me.Panel4.TabIndex = 31
        '
        'tbxSearchgloEMR
        '
        Me.tbxSearchgloEMR.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxSearchgloEMR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbxSearchgloEMR.Location = New System.Drawing.Point(27, 8)
        Me.tbxSearchgloEMR.Name = "tbxSearchgloEMR"
        Me.tbxSearchgloEMR.Size = New System.Drawing.Size(318, 15)
        Me.tbxSearchgloEMR.TabIndex = 1
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.White
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Location = New System.Drawing.Point(27, 4)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(318, 4)
        Me.Label26.TabIndex = 37
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Location = New System.Drawing.Point(27, 23)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(318, 2)
        Me.Label27.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label28.Location = New System.Drawing.Point(0, 25)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(345, 1)
        Me.Label28.TabIndex = 35
        Me.Label28.Text = "label1"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Location = New System.Drawing.Point(0, 3)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(345, 1)
        Me.Label29.TabIndex = 36
        Me.Label29.Text = "label1"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(1, 888)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(345, 1)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(1, 3)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(345, 25)
        Me.Panel3.TabIndex = 23
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(0, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(345, 1)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(345, 24)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "gloEMR Fields"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(345, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(346, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 886)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(0, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 886)
        Me.Label23.TabIndex = 29
        Me.Label23.Text = "label3"
        '
        'pnlHL7Fields
        '
        Me.pnlHL7Fields.Controls.Add(Me.trHL7Fields)
        Me.pnlHL7Fields.Controls.Add(Me.Panel2)
        Me.pnlHL7Fields.Controls.Add(Me.Label12)
        Me.pnlHL7Fields.Controls.Add(Me.Panel9)
        Me.pnlHL7Fields.Controls.Add(Me.Label10)
        Me.pnlHL7Fields.Controls.Add(Me.Label9)
        Me.pnlHL7Fields.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlHL7Fields.Location = New System.Drawing.Point(0, 0)
        Me.pnlHL7Fields.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlHL7Fields.Name = "pnlHL7Fields"
        Me.pnlHL7Fields.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlHL7Fields.Size = New System.Drawing.Size(350, 892)
        Me.pnlHL7Fields.TabIndex = 0
        '
        'trHL7Fields
        '
        Me.trHL7Fields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trHL7Fields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trHL7Fields.ForeColor = System.Drawing.Color.Black
        Me.trHL7Fields.HideSelection = False
        Me.trHL7Fields.ImageIndex = 4
        Me.trHL7Fields.ImageList = Me.ImageList1
        Me.trHL7Fields.Indent = 20
        Me.trHL7Fields.ItemHeight = 20
        Me.trHL7Fields.Location = New System.Drawing.Point(4, 57)
        Me.trHL7Fields.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.trHL7Fields.Name = "trHL7Fields"
        Me.trHL7Fields.SelectedImageKey = "CCR.ico"
        Me.trHL7Fields.Size = New System.Drawing.Size(345, 831)
        Me.trHL7Fields.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.tbxSearchHL7)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.PictureBox3)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.Black
        Me.Panel2.Location = New System.Drawing.Point(4, 28)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(345, 29)
        Me.Panel2.TabIndex = 25
        '
        'tbxSearchHL7
        '
        Me.tbxSearchHL7.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tbxSearchHL7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbxSearchHL7.Location = New System.Drawing.Point(27, 8)
        Me.tbxSearchHL7.Name = "tbxSearchHL7"
        Me.tbxSearchHL7.Size = New System.Drawing.Size(318, 15)
        Me.tbxSearchHL7.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Location = New System.Drawing.Point(27, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(318, 4)
        Me.Label13.TabIndex = 37
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.White
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Location = New System.Drawing.Point(27, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(318, 2)
        Me.Label14.TabIndex = 38
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(0, 4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(27, 21)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Location = New System.Drawing.Point(0, 25)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(345, 1)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(0, 3)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(345, 1)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(4, 888)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(345, 1)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "label2"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImage = CType(resources.GetObject("Panel9.BackgroundImage"), System.Drawing.Image)
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label1)
        Me.Panel9.Controls.Add(Me.Label8)
        Me.Panel9.Controls.Add(Me.Label11)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(4, 3)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(345, 25)
        Me.Panel9.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(345, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "CCR Fields"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(0, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(345, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(345, 1)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "label1"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(349, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 886)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 886)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "label3"
        '
        'dlgOpenFile
        '
        Me.dlgOpenFile.FileName = "dlgOpenFile"
        '
        'frmDataMigMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1264, 986)
        Me.Controls.Add(Me.pnlMappedFields)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmDataMigMapping"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CCR Mapping"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlMappedFields.ResumeLayout(False)
        Me.pnlPrintMessage.ResumeLayout(False)
        Me.pnlPrintMessage.PerformLayout()
        Me.pnltr.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlgloEMRFields.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.pnlHL7Fields.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlMappedFields As System.Windows.Forms.Panel
    Friend WithEvents pnlHL7Fields As System.Windows.Forms.Panel
    Friend WithEvents trHL7Fields As System.Windows.Forms.TreeView
    Friend WithEvents pnltr As System.Windows.Forms.Panel
    Friend WithEvents trMappedFields As System.Windows.Forms.TreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents SaveToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents CloseToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDelHL7Field As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDelgloEMRField As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tltpHL7Fields As System.Windows.Forms.ToolTip
    Friend WithEvents tbxSearchHL7 As System.Windows.Forms.TextBox
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents splleft As System.Windows.Forms.Splitter
    Friend WithEvents spltRight As System.Windows.Forms.Splitter
    Friend WithEvents pnlgloEMRFields As System.Windows.Forms.Panel
    Friend WithEvents trgloEMRFields As System.Windows.Forms.TreeView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents tbxSearchgloEMR As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents BrowseToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Private WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlPrintMessage As System.Windows.Forms.Panel
    Public WithEvents lblPleaseWait As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
End Class
