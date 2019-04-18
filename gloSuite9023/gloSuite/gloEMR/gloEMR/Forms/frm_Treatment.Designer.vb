<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Treatment
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
                Try
                    If (IsNothing(_PatientStrip) = False) Then
                        _PatientStrip.Dispose()
                        _PatientStrip = Nothing
                    End If
                Catch ex As Exception

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Treatment))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.lblCopyRight = New System.Windows.Forms.Label()
        Me.tls_Top = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tls_btnAddLine = New System.Windows.Forms.ToolStripButton()
        Me.toolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnRemoveLine = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnCodingRule = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnSaveNClose = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlC1Diagnosis = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.C1Diagnosis = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnlElementHosts = New System.Windows.Forms.Panel()
        Me.elementHostSearch = New System.Windows.Forms.Integration.ElementHost()
        Me.pnlSmallStrip = New System.Windows.Forms.Panel()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.ts_SmallStrip = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btnMappingClose = New System.Windows.Forms.ToolStripButton()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.btn_Right = New System.Windows.Forms.Button()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.label52 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.pnltxtPrimaryDiagnosis = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtPrimaryDiagnosis = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.gloUCTreatment = New gloUserControlLibrary.gloUC_Treatment()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.btnPQRS = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.rbICD10 = New System.Windows.Forms.RadioButton()
        Me.rbICD9 = New System.Windows.Forms.RadioButton()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.btnLeft = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.label30 = New System.Windows.Forms.Label()
        Me.label31 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label19 = New System.Windows.Forms.Label()
        Me.label16 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.mnuShortCut = New System.Windows.Forms.MenuStrip()
        Me.ShortCuts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddLine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveLine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.ContextMenuDiagnosis = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRefusedselectedDiagnosis = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlToolStrip.SuspendLayout()
        Me.tls_Top.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlC1Diagnosis.SuspendLayout()
        CType(Me.C1Diagnosis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlElementHosts.SuspendLayout()
        Me.pnlSmallStrip.SuspendLayout()
        Me.ts_SmallStrip.SuspendLayout()
        Me.pnltxtPrimaryDiagnosis.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.mnuShortCut.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.ContextMenuDiagnosis.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.lblCopyRight)
        Me.pnlToolStrip.Controls.Add(Me.tls_Top)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(994, 56)
        Me.pnlToolStrip.TabIndex = 5
        '
        'lblCopyRight
        '
        Me.lblCopyRight.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyRight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblCopyRight.Location = New System.Drawing.Point(563, 33)
        Me.lblCopyRight.Name = "lblCopyRight"
        Me.lblCopyRight.Size = New System.Drawing.Size(397, 14)
        Me.lblCopyRight.TabIndex = 10
        Me.lblCopyRight.Text = "CPT™ copyright  2012 American Medical Association. All rights reserved"
        '
        'tls_Top
        '
        Me.tls_Top.BackColor = System.Drawing.Color.Transparent
        Me.tls_Top.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Top.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Top.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Top.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tls_btnAddLine, Me.toolStripButton2, Me.tls_btnRemoveLine, Me.tlsbtnCodingRule, Me.tls_btnRefresh, Me.tls_btnSaveNClose, Me.tls_btnCancel})
        Me.tls_Top.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_Top.Location = New System.Drawing.Point(0, 0)
        Me.tls_Top.Name = "tls_Top"
        Me.tls_Top.Size = New System.Drawing.Size(994, 53)
        Me.tls_Top.TabIndex = 9
        Me.tls_Top.Text = "toolStrip1"
        '
        'tls_btnAddLine
        '
        Me.tls_btnAddLine.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_btnAddLine.Image = CType(resources.GetObject("tls_btnAddLine.Image"), System.Drawing.Image)
        Me.tls_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnAddLine.Name = "tls_btnAddLine"
        Me.tls_btnAddLine.Size = New System.Drawing.Size(65, 50)
        Me.tls_btnAddLine.Tag = "AddLine"
        Me.tls_btnAddLine.Text = "&Add Line"
        Me.tls_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'toolStripButton2
        '
        Me.toolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toolStripButton2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.toolStripButton2.Image = CType(resources.GetObject("toolStripButton2.Image"), System.Drawing.Image)
        Me.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.toolStripButton2.Name = "toolStripButton2"
        Me.toolStripButton2.Size = New System.Drawing.Size(77, 50)
        Me.toolStripButton2.Tag = "InsertLine"
        Me.toolStripButton2.Text = "&Insert Line"
        Me.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.toolStripButton2.Visible = False
        '
        'tls_btnRemoveLine
        '
        Me.tls_btnRemoveLine.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_btnRemoveLine.Image = CType(resources.GetObject("tls_btnRemoveLine.Image"), System.Drawing.Image)
        Me.tls_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnRemoveLine.Name = "tls_btnRemoveLine"
        Me.tls_btnRemoveLine.Size = New System.Drawing.Size(89, 50)
        Me.tls_btnRemoveLine.Tag = "RemoveLine"
        Me.tls_btnRemoveLine.Text = "Re&move Line"
        Me.tls_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnCodingRule
        '
        Me.tlsbtnCodingRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlsbtnCodingRule.Image = CType(resources.GetObject("tlsbtnCodingRule.Image"), System.Drawing.Image)
        Me.tlsbtnCodingRule.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnCodingRule.Name = "tlsbtnCodingRule"
        Me.tlsbtnCodingRule.Size = New System.Drawing.Size(85, 50)
        Me.tlsbtnCodingRule.Text = "Coding &Rule"
        Me.tlsbtnCodingRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_btnRefresh
        '
        Me.tls_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_btnRefresh.Image = CType(resources.GetObject("tls_btnRefresh.Image"), System.Drawing.Image)
        Me.tls_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnRefresh.Name = "tls_btnRefresh"
        Me.tls_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tls_btnRefresh.Tag = "Refresh"
        Me.tls_btnRefresh.Text = "&Refresh"
        Me.tls_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_btnRefresh.ToolTipText = "Refresh"
        Me.tls_btnRefresh.Visible = False
        '
        'tls_btnSaveNClose
        '
        Me.tls_btnSaveNClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnSaveNClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_btnSaveNClose.Image = CType(resources.GetObject("tls_btnSaveNClose.Image"), System.Drawing.Image)
        Me.tls_btnSaveNClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnSaveNClose.Name = "tls_btnSaveNClose"
        Me.tls_btnSaveNClose.Size = New System.Drawing.Size(66, 50)
        Me.tls_btnSaveNClose.Tag = "SaveNClose"
        Me.tls_btnSaveNClose.Text = "&Save&&Cls"
        Me.tls_btnSaveNClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_btnSaveNClose.ToolTipText = "Save and Close"
        '
        'tls_btnCancel
        '
        Me.tls_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tls_btnCancel.Image = CType(resources.GetObject("tls_btnCancel.Image"), System.Drawing.Image)
        Me.tls_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnCancel.Name = "tls_btnCancel"
        Me.tls_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.tls_btnCancel.Tag = "Cancel"
        Me.tls_btnCancel.Text = "&Close"
        Me.tls_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Panel2)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.Panel3)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(994, 689)
        Me.pnlMain.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlC1Diagnosis)
        Me.Panel2.Controls.Add(Me.pnlElementHosts)
        Me.Panel2.Controls.Add(Me.pnlSmallStrip)
        Me.Panel2.Controls.Add(Me.pnltxtPrimaryDiagnosis)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 284)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(994, 405)
        Me.Panel2.TabIndex = 2
        '
        'pnlC1Diagnosis
        '
        Me.pnlC1Diagnosis.Controls.Add(Me.Label15)
        Me.pnlC1Diagnosis.Controls.Add(Me.Label17)
        Me.pnlC1Diagnosis.Controls.Add(Me.C1Diagnosis)
        Me.pnlC1Diagnosis.Controls.Add(Me.Label18)
        Me.pnlC1Diagnosis.Controls.Add(Me.Label22)
        Me.pnlC1Diagnosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1Diagnosis.Location = New System.Drawing.Point(0, 27)
        Me.pnlC1Diagnosis.Name = "pnlC1Diagnosis"
        Me.pnlC1Diagnosis.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlC1Diagnosis.Size = New System.Drawing.Size(565, 378)
        Me.pnlC1Diagnosis.TabIndex = 215
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(4, 374)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(557, 1)
        Me.Label15.TabIndex = 16
        Me.Label15.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 374)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "label4"
        '
        'C1Diagnosis
        '
        Me.C1Diagnosis.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Diagnosis.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Diagnosis.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.C1Diagnosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Diagnosis.ExtendLastCol = True
        Me.C1Diagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Diagnosis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Diagnosis.Location = New System.Drawing.Point(3, 1)
        Me.C1Diagnosis.Name = "C1Diagnosis"
        Me.C1Diagnosis.Rows.DefaultSize = 19
        Me.C1Diagnosis.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Diagnosis.Size = New System.Drawing.Size(558, 374)
        Me.C1Diagnosis.StyleInfo = resources.GetString("C1Diagnosis.StyleInfo")
        Me.C1Diagnosis.TabIndex = 213
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(561, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 374)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(559, 1)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "label1"
        '
        'pnlElementHosts
        '
        Me.pnlElementHosts.Controls.Add(Me.elementHostSearch)
        Me.pnlElementHosts.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlElementHosts.Location = New System.Drawing.Point(565, 27)
        Me.pnlElementHosts.Name = "pnlElementHosts"
        Me.pnlElementHosts.Size = New System.Drawing.Size(400, 378)
        Me.pnlElementHosts.TabIndex = 6
        Me.pnlElementHosts.Visible = False
        '
        'elementHostSearch
        '
        Me.elementHostSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.elementHostSearch.Location = New System.Drawing.Point(0, 0)
        Me.elementHostSearch.Name = "elementHostSearch"
        Me.elementHostSearch.Size = New System.Drawing.Size(400, 378)
        Me.elementHostSearch.TabIndex = 0
        Me.elementHostSearch.Text = "ElementHost1"
        Me.elementHostSearch.Child = Nothing
        '
        'pnlSmallStrip
        '
        Me.pnlSmallStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSmallStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSmallStrip.Controls.Add(Me.Label43)
        Me.pnlSmallStrip.Controls.Add(Me.ts_SmallStrip)
        Me.pnlSmallStrip.Controls.Add(Me.Label44)
        Me.pnlSmallStrip.Controls.Add(Me.btn_Right)
        Me.pnlSmallStrip.Controls.Add(Me.Label45)
        Me.pnlSmallStrip.Controls.Add(Me.label52)
        Me.pnlSmallStrip.Controls.Add(Me.Label46)
        Me.pnlSmallStrip.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSmallStrip.Location = New System.Drawing.Point(965, 27)
        Me.pnlSmallStrip.Name = "pnlSmallStrip"
        Me.pnlSmallStrip.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnlSmallStrip.Size = New System.Drawing.Size(29, 378)
        Me.pnlSmallStrip.TabIndex = 216
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Location = New System.Drawing.Point(2, 374)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(23, 1)
        Me.Label43.TabIndex = 25
        '
        'ts_SmallStrip
        '
        Me.ts_SmallStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_SmallStrip.BackgroundImage = CType(resources.GetObject("ts_SmallStrip.BackgroundImage"), System.Drawing.Image)
        Me.ts_SmallStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_SmallStrip.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_SmallStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_SmallStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_SmallStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnMappingClose})
        Me.ts_SmallStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ts_SmallStrip.Location = New System.Drawing.Point(2, 25)
        Me.ts_SmallStrip.Name = "ts_SmallStrip"
        Me.ts_SmallStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ts_SmallStrip.Size = New System.Drawing.Size(23, 350)
        Me.ts_SmallStrip.TabIndex = 21
        Me.ts_SmallStrip.Text = "toolStrip1"
        Me.ts_SmallStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270
        '
        'btnMappingClose
        '
        Me.btnMappingClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMappingClose.Image = CType(resources.GetObject("btnMappingClose.Image"), System.Drawing.Image)
        Me.btnMappingClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnMappingClose.Name = "btnMappingClose"
        Me.btnMappingClose.Size = New System.Drawing.Size(21, 164)
        Me.btnMappingClose.Text = "  ICD 9 to 10 Mapping"
        Me.btnMappingClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnMappingClose.ToolTipText = "Show ICD 9 to 10 Mapping"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Location = New System.Drawing.Point(2, 24)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(23, 1)
        Me.Label44.TabIndex = 23
        '
        'btn_Right
        '
        Me.btn_Right.BackColor = System.Drawing.Color.Transparent
        Me.btn_Right.BackgroundImage = CType(resources.GetObject("btn_Right.BackgroundImage"), System.Drawing.Image)
        Me.btn_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Right.Dock = System.Windows.Forms.DockStyle.Top
        Me.btn_Right.FlatAppearance.BorderSize = 0
        Me.btn_Right.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_Right.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btn_Right.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Right.Image = Global.gloEMR.My.Resources.Resources.Rewind
        Me.btn_Right.Location = New System.Drawing.Point(2, 2)
        Me.btn_Right.Name = "btn_Right"
        Me.btn_Right.Size = New System.Drawing.Size(23, 22)
        Me.btn_Right.TabIndex = 22
        Me.btn_Right.UseVisualStyleBackColor = False
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label45.Location = New System.Drawing.Point(2, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(23, 1)
        Me.Label45.TabIndex = 19
        '
        'label52
        '
        Me.label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label52.Dock = System.Windows.Forms.DockStyle.Left
        Me.label52.Location = New System.Drawing.Point(1, 1)
        Me.label52.Name = "label52"
        Me.label52.Size = New System.Drawing.Size(1, 374)
        Me.label52.TabIndex = 9
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Location = New System.Drawing.Point(25, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 374)
        Me.Label46.TabIndex = 24
        '
        'pnltxtPrimaryDiagnosis
        '
        Me.pnltxtPrimaryDiagnosis.Controls.Add(Me.Panel1)
        Me.pnltxtPrimaryDiagnosis.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltxtPrimaryDiagnosis.Location = New System.Drawing.Point(0, 0)
        Me.pnltxtPrimaryDiagnosis.Name = "pnltxtPrimaryDiagnosis"
        Me.pnltxtPrimaryDiagnosis.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnltxtPrimaryDiagnosis.Size = New System.Drawing.Size(994, 27)
        Me.pnltxtPrimaryDiagnosis.TabIndex = 214
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.txtPrimaryDiagnosis)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(988, 24)
        Me.Panel1.TabIndex = 212
        '
        'txtPrimaryDiagnosis
        '
        Me.txtPrimaryDiagnosis.BackColor = System.Drawing.Color.White
        Me.txtPrimaryDiagnosis.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtPrimaryDiagnosis.Location = New System.Drawing.Point(112, 1)
        Me.txtPrimaryDiagnosis.Name = "txtPrimaryDiagnosis"
        Me.txtPrimaryDiagnosis.ReadOnly = True
        Me.txtPrimaryDiagnosis.Size = New System.Drawing.Size(330, 22)
        Me.txtPrimaryDiagnosis.TabIndex = 46
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 22)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "  Exam Name : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(986, 1)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(987, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 23)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(988, 1)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 281)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(994, 3)
        Me.Splitter1.TabIndex = 5
        Me.Splitter1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(994, 281)
        Me.Panel3.TabIndex = 3
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label11)
        Me.Panel6.Controls.Add(Me.Label12)
        Me.Panel6.Controls.Add(Me.Label13)
        Me.Panel6.Controls.Add(Me.Label14)
        Me.Panel6.Controls.Add(Me.gloUCTreatment)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 28)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel6.Size = New System.Drawing.Size(994, 253)
        Me.Panel6.TabIndex = 215
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 252)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(986, 1)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 249)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(990, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 249)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(988, 1)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "label1"
        '
        'gloUCTreatment
        '
        Me.gloUCTreatment.BackColor = System.Drawing.Color.Transparent
        Me.gloUCTreatment.ColSel = 1
        Me.gloUCTreatment.DatabaseConnectionString = Nothing
        Me.gloUCTreatment.DisableGrid = False
        Me.gloUCTreatment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloUCTreatment.DOS = New Date(CType(0, Long))
        Me.gloUCTreatment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gloUCTreatment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gloUCTreatment.ICD9Count = gloUserControlLibrary.enumIC9Count.Show_8_ICD9
        Me.gloUCTreatment.ICDRevision = 0
        Me.gloUCTreatment.IsExamPTBillingEnabled = False
        Me.gloUCTreatment.Location = New System.Drawing.Point(3, 3)
        Me.gloUCTreatment.ModifierCount = gloUserControlLibrary.enumModifierCount.Show_4_Modifier
        Me.gloUCTreatment.Name = "gloUCTreatment"
        Me.gloUCTreatment.RowSel = 1
        Me.gloUCTreatment.Size = New System.Drawing.Size(988, 250)
        Me.gloUCTreatment.TabIndex = 0
        Me.gloUCTreatment.TreatmentModified = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel4)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel5.Size = New System.Drawing.Size(994, 28)
        Me.Panel5.TabIndex = 214
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Controls.Add(Me.rbICD10)
        Me.Panel4.Controls.Add(Me.rbICD9)
        Me.Panel4.Controls.Add(Me.btnRight)
        Me.Panel4.Controls.Add(Me.btnLeft)
        Me.Panel4.Controls.Add(Me.btnDown)
        Me.Panel4.Controls.Add(Me.btnUp)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(988, 25)
        Me.Panel4.TabIndex = 213
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label29)
        Me.Panel7.Controls.Add(Me.btnPQRS)
        Me.Panel7.Controls.Add(Me.Label28)
        Me.Panel7.Controls.Add(Me.Label27)
        Me.Panel7.Location = New System.Drawing.Point(872, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(94, 25)
        Me.Panel7.TabIndex = 57
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(1, 24)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(92, 1)
        Me.Label29.TabIndex = 13
        '
        'btnPQRS
        '
        Me.btnPQRS.BackColor = System.Drawing.Color.Transparent
        Me.btnPQRS.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_OliveHeader1
        Me.btnPQRS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPQRS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPQRS.FlatAppearance.BorderColor = System.Drawing.Color.Olive
        Me.btnPQRS.FlatAppearance.BorderSize = 0
        Me.btnPQRS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPQRS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPQRS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPQRS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPQRS.ForeColor = System.Drawing.Color.Black
        Me.btnPQRS.Location = New System.Drawing.Point(1, 0)
        Me.btnPQRS.Name = "btnPQRS"
        Me.btnPQRS.Size = New System.Drawing.Size(92, 25)
        Me.btnPQRS.TabIndex = 56
        Me.btnPQRS.Text = "QDCs"
        Me.btnPQRS.UseVisualStyleBackColor = False
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(0, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 25)
        Me.Label28.TabIndex = 12
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(93, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 25)
        Me.Label27.TabIndex = 11
        '
        'rbICD10
        '
        Me.rbICD10.AutoSize = True
        Me.rbICD10.BackColor = System.Drawing.Color.Transparent
        Me.rbICD10.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbICD10.Location = New System.Drawing.Point(163, 1)
        Me.rbICD10.Name = "rbICD10"
        Me.rbICD10.Size = New System.Drawing.Size(58, 23)
        Me.rbICD10.TabIndex = 55
        Me.rbICD10.TabStop = True
        Me.rbICD10.Text = "ICD10"
        Me.rbICD10.UseVisualStyleBackColor = False
        '
        'rbICD9
        '
        Me.rbICD9.AutoSize = True
        Me.rbICD9.BackColor = System.Drawing.Color.Transparent
        Me.rbICD9.Dock = System.Windows.Forms.DockStyle.Left
        Me.rbICD9.Location = New System.Drawing.Point(112, 1)
        Me.rbICD9.Name = "rbICD9"
        Me.rbICD9.Size = New System.Drawing.Size(51, 23)
        Me.rbICD9.TabIndex = 54
        Me.rbICD9.TabStop = True
        Me.rbICD9.Text = "ICD9"
        Me.rbICD9.UseVisualStyleBackColor = False
        '
        'btnRight
        '
        Me.btnRight.BackColor = System.Drawing.Color.Transparent
        Me.btnRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.SmallRight
        Me.btnRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnRight.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnRight.FlatAppearance.BorderSize = 0
        Me.btnRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRight.Location = New System.Drawing.Point(87, 1)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(25, 23)
        Me.btnRight.TabIndex = 46
        Me.btnRight.UseVisualStyleBackColor = False
        '
        'btnLeft
        '
        Me.btnLeft.BackColor = System.Drawing.Color.Transparent
        Me.btnLeft.BackgroundImage = Global.gloEMR.My.Resources.Resources.SmallLeft
        Me.btnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnLeft.FlatAppearance.BorderSize = 0
        Me.btnLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLeft.Location = New System.Drawing.Point(62, 1)
        Me.btnLeft.Name = "btnLeft"
        Me.btnLeft.Size = New System.Drawing.Size(25, 23)
        Me.btnLeft.TabIndex = 47
        Me.btnLeft.UseVisualStyleBackColor = False
        '
        'btnDown
        '
        Me.btnDown.BackColor = System.Drawing.Color.Transparent
        Me.btnDown.BackgroundImage = Global.gloEMR.My.Resources.Resources.SmallDown
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDown.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(37, 1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(25, 23)
        Me.btnDown.TabIndex = 49
        Me.btnDown.UseVisualStyleBackColor = False
        '
        'btnUp
        '
        Me.btnUp.BackColor = System.Drawing.Color.Transparent
        Me.btnUp.BackgroundImage = Global.gloEMR.My.Resources.Resources.SmallUp
        Me.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnUp.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnUp.FlatAppearance.BorderSize = 0
        Me.btnUp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(12, 1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(25, 23)
        Me.btnUp.TabIndex = 48
        Me.btnUp.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 23)
        Me.Label2.TabIndex = 45
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(986, 1)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 24)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(987, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 24)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(988, 1)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "label1"
        '
        'pnlBottom
        '
        Me.pnlBottom.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.label30)
        Me.pnlBottom.Controls.Add(Me.label31)
        Me.pnlBottom.Controls.Add(Me.label20)
        Me.pnlBottom.Controls.Add(Me.label21)
        Me.pnlBottom.Controls.Add(Me.label19)
        Me.pnlBottom.Controls.Add(Me.label16)
        Me.pnlBottom.Controls.Add(Me.Label23)
        Me.pnlBottom.Controls.Add(Me.Label24)
        Me.pnlBottom.Controls.Add(Me.Label25)
        Me.pnlBottom.Controls.Add(Me.Label26)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBottom.Location = New System.Drawing.Point(3, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(988, 24)
        Me.pnlBottom.TabIndex = 211
        '
        'label30
        '
        Me.label30.BackColor = System.Drawing.Color.Transparent
        Me.label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label30.Location = New System.Drawing.Point(348, 1)
        Me.label30.Name = "label30"
        Me.label30.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.label30.Size = New System.Drawing.Size(94, 22)
        Me.label30.TabIndex = 56
        Me.label30.Text = "- Save && Close"
        '
        'label31
        '
        Me.label31.BackColor = System.Drawing.Color.Transparent
        Me.label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label31.ForeColor = System.Drawing.Color.Maroon
        Me.label31.Location = New System.Drawing.Point(293, 1)
        Me.label31.Name = "label31"
        Me.label31.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.label31.Size = New System.Drawing.Size(55, 22)
        Me.label31.TabIndex = 55
        Me.label31.Text = "Ctrl + S"
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.Transparent
        Me.label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.Location = New System.Drawing.Point(199, 1)
        Me.label20.Name = "label20"
        Me.label20.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.label20.Size = New System.Drawing.Size(94, 22)
        Me.label20.TabIndex = 46
        Me.label20.Text = "- Remove Line"
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.Color.Maroon
        Me.label21.Location = New System.Drawing.Point(143, 1)
        Me.label21.Name = "label21"
        Me.label21.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.label21.Size = New System.Drawing.Size(56, 22)
        Me.label21.TabIndex = 45
        Me.label21.Text = "Ctrl + D"
        '
        'label19
        '
        Me.label19.BackColor = System.Drawing.Color.Transparent
        Me.label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.Location = New System.Drawing.Point(73, 1)
        Me.label19.Name = "label19"
        Me.label19.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.label19.Size = New System.Drawing.Size(70, 22)
        Me.label19.TabIndex = 44
        Me.label19.Text = "- Add Line"
        '
        'label16
        '
        Me.label16.BackColor = System.Drawing.Color.Transparent
        Me.label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.ForeColor = System.Drawing.Color.Maroon
        Me.label16.Location = New System.Drawing.Point(1, 1)
        Me.label16.Name = "label16"
        Me.label16.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.label16.Size = New System.Drawing.Size(72, 22)
        Me.label16.TabIndex = 44
        Me.label16.Text = "   Ctrl + A"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(986, 1)
        Me.Label23.TabIndex = 60
        Me.Label23.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 59
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(987, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 23)
        Me.Label25.TabIndex = 58
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(988, 1)
        Me.Label26.TabIndex = 57
        Me.Label26.Text = "label1"
        '
        'mnuShortCut
        '
        Me.mnuShortCut.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShortCuts})
        Me.mnuShortCut.Location = New System.Drawing.Point(0, 0)
        Me.mnuShortCut.Name = "mnuShortCut"
        Me.mnuShortCut.Size = New System.Drawing.Size(994, 24)
        Me.mnuShortCut.TabIndex = 212
        Me.mnuShortCut.Text = "MenuStrip1"
        Me.mnuShortCut.Visible = False
        '
        'ShortCuts
        '
        Me.ShortCuts.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddLine, Me.mnuRemoveLine, Me.mnuSave})
        Me.ShortCuts.Name = "ShortCuts"
        Me.ShortCuts.Size = New System.Drawing.Size(74, 20)
        Me.ShortCuts.Text = "Short Cuts"
        '
        'mnuAddLine
        '
        Me.mnuAddLine.Name = "mnuAddLine"
        Me.mnuAddLine.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.mnuAddLine.Size = New System.Drawing.Size(184, 22)
        Me.mnuAddLine.Text = "Add Line"
        '
        'mnuRemoveLine
        '
        Me.mnuRemoveLine.Name = "mnuRemoveLine"
        Me.mnuRemoveLine.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuRemoveLine.Size = New System.Drawing.Size(184, 22)
        Me.mnuRemoveLine.Text = "Remove Line"
        '
        'mnuSave
        '
        Me.mnuSave.Name = "mnuSave"
        Me.mnuSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuSave.Size = New System.Drawing.Size(184, 22)
        Me.mnuSave.Text = "Save"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.pnlBottom)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel9.Location = New System.Drawing.Point(0, 745)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel9.Size = New System.Drawing.Size(994, 27)
        Me.Panel9.TabIndex = 213
        '
        'ContextMenuDiagnosis
        '
        Me.ContextMenuDiagnosis.BackColor = System.Drawing.Color.White
        Me.ContextMenuDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRefusedselectedDiagnosis})
        Me.ContextMenuDiagnosis.Name = "ContextMenuDiagnosis"
        Me.ContextMenuDiagnosis.Size = New System.Drawing.Size(173, 48)
        '
        'mnuRefusedselectedDiagnosis
        '
        Me.mnuRefusedselectedDiagnosis.Name = "mnuRefusedselectedDiagnosis"
        Me.mnuRefusedselectedDiagnosis.Size = New System.Drawing.Size(172, 22)
        Me.mnuRefusedselectedDiagnosis.Text = "Set Refusal Reason"
        '
        'frm_Treatment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(994, 772)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Controls.Add(Me.mnuShortCut)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuShortCut
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_Treatment"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View Diagnosis"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tls_Top.ResumeLayout(False)
        Me.tls_Top.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlC1Diagnosis.ResumeLayout(False)
        CType(Me.C1Diagnosis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlElementHosts.ResumeLayout(False)
        Me.pnlSmallStrip.ResumeLayout(False)
        Me.pnlSmallStrip.PerformLayout()
        Me.ts_SmallStrip.ResumeLayout(False)
        Me.ts_SmallStrip.PerformLayout()
        Me.pnltxtPrimaryDiagnosis.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.mnuShortCut.ResumeLayout(False)
        Me.mnuShortCut.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.ContextMenuDiagnosis.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Private WithEvents tls_Top As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tls_btnAddLine As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripButton2 As System.Windows.Forms.ToolStripButton
    Private WithEvents tls_btnRemoveLine As System.Windows.Forms.ToolStripButton
    Private WithEvents tls_btnRefresh As System.Windows.Forms.ToolStripButton
    Private WithEvents tls_btnSaveNClose As System.Windows.Forms.ToolStripButton
    Private WithEvents tls_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents pnlBottom As System.Windows.Forms.Panel
    Private WithEvents label30 As System.Windows.Forms.Label
    Private WithEvents label31 As System.Windows.Forms.Label
    Private WithEvents label20 As System.Windows.Forms.Label
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents label16 As System.Windows.Forms.Label
    Friend WithEvents gloUCTreatment As gloUserControlLibrary.gloUC_Treatment
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnLeft As System.Windows.Forms.Button
    Friend WithEvents btnRight As System.Windows.Forms.Button
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents mnuShortCut As System.Windows.Forms.MenuStrip
    Friend WithEvents ShortCuts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddLine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveLine As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtPrimaryDiagnosis As System.Windows.Forms.TextBox
    Friend WithEvents lblCopyRight As System.Windows.Forms.Label
    Protected WithEvents C1Diagnosis As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlC1Diagnosis As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnltxtPrimaryDiagnosis As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents rbICD10 As System.Windows.Forms.RadioButton
    Friend WithEvents rbICD9 As System.Windows.Forms.RadioButton
    Friend WithEvents tlsbtnCodingRule As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlElementHosts As System.Windows.Forms.Panel
    Friend WithEvents elementHostSearch As System.Windows.Forms.Integration.ElementHost
    Private WithEvents pnlSmallStrip As System.Windows.Forms.Panel
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents ts_SmallStrip As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btnMappingClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents btn_Right As System.Windows.Forms.Button
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents label52 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents btnPQRS As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuDiagnosis As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRefusedselectedDiagnosis As System.Windows.Forms.ToolStripMenuItem
End Class
