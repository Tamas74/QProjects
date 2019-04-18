<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDM_PatientCriteria
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
            If (IsNothing(oDM) = False) Then
                oDM.Dispose()
                oDM = Nothing
            End If
            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch

            End Try

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_PatientCriteria))
        Me.pnl_tlstrip = New System.Windows.Forms.Panel
        Me.tlsDM = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsDM_Save = New System.Windows.Forms.ToolStripButton
        Me.tlsDM_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlRight = New System.Windows.Forms.Panel
        Me.GloUC_trvAssociates = New gloUserControlLibrary.gloUC_TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnltrvTriggers = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.trvTriggers = New System.Windows.Forms.TreeView
        Me.Label74 = New System.Windows.Forms.Label
        Me.Label75 = New System.Windows.Forms.Label
        Me.Label76 = New System.Windows.Forms.Label
        Me.Label77 = New System.Windows.Forms.Label
        Me.pnltxtSearchOrder = New System.Windows.Forms.Panel
        Me.txtSearchOrder = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label82 = New System.Windows.Forms.Label
        Me.Label83 = New System.Windows.Forms.Label
        Me.Label84 = New System.Windows.Forms.Label
        Me.pnlbtnLab = New System.Windows.Forms.Panel
        Me.btnLab = New System.Windows.Forms.Button
        Me.Label78 = New System.Windows.Forms.Label
        Me.Label79 = New System.Windows.Forms.Label
        Me.Label80 = New System.Windows.Forms.Label
        Me.Label81 = New System.Windows.Forms.Label
        Me.pnlbtnReferrals = New System.Windows.Forms.Panel
        Me.btnReferrals = New System.Windows.Forms.Button
        Me.Label70 = New System.Windows.Forms.Label
        Me.Label71 = New System.Windows.Forms.Label
        Me.Label72 = New System.Windows.Forms.Label
        Me.Label73 = New System.Windows.Forms.Label
        Me.pnlbtnRx = New System.Windows.Forms.Panel
        Me.btnRx = New System.Windows.Forms.Button
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label67 = New System.Windows.Forms.Label
        Me.Label68 = New System.Windows.Forms.Label
        Me.Label69 = New System.Windows.Forms.Label
        Me.pnlbtnRadiologyTest = New System.Windows.Forms.Panel
        Me.btnRadiologyTest = New System.Windows.Forms.Button
        Me.Label55 = New System.Windows.Forms.Label
        Me.Label56 = New System.Windows.Forms.Label
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label62 = New System.Windows.Forms.Label
        Me.pnlbtnGuideline = New System.Windows.Forms.Panel
        Me.btnGuideline = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlSummaryOthers = New System.Windows.Forms.Panel
        Me.pnlGuideline = New System.Windows.Forms.Panel
        Me.trvHealthPlan = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label99 = New System.Windows.Forms.Label
        Me.Label100 = New System.Windows.Forms.Label
        Me.Label101 = New System.Windows.Forms.Label
        Me.Label102 = New System.Windows.Forms.Label
        Me.pnlGuidelineHeader = New System.Windows.Forms.Panel
        Me.pnl3 = New System.Windows.Forms.Panel
        Me.Label95 = New System.Windows.Forms.Label
        Me.Label96 = New System.Windows.Forms.Label
        Me.Label97 = New System.Windows.Forms.Label
        Me.Label98 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.pnlMsgTOP = New System.Windows.Forms.Panel
        Me.pnlMsg = New System.Windows.Forms.Panel
        Me.txtMessage = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.CntConditions = New System.Windows.Forms.ContextMenu
        Me.mnuDelete = New System.Windows.Forms.MenuItem
        Me.mnuReferral = New System.Windows.Forms.MenuItem
        Me.EditReferral = New System.Windows.Forms.MenuItem
        Me.mnuEditTemplate = New System.Windows.Forms.MenuItem
        Me.pnl_tlstrip.SuspendLayout()
        Me.tlsDM.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.pnltrvTriggers.SuspendLayout()
        Me.pnltxtSearchOrder.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbtnLab.SuspendLayout()
        Me.pnlbtnReferrals.SuspendLayout()
        Me.pnlbtnRx.SuspendLayout()
        Me.pnlbtnRadiologyTest.SuspendLayout()
        Me.pnlbtnGuideline.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlSummaryOthers.SuspendLayout()
        Me.pnlGuideline.SuspendLayout()
        Me.pnlGuidelineHeader.SuspendLayout()
        Me.pnl3.SuspendLayout()
        Me.pnlMsgTOP.SuspendLayout()
        Me.pnlMsg.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_tlstrip
        '
        Me.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlstrip.Controls.Add(Me.tlsDM)
        Me.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlstrip.Name = "pnl_tlstrip"
        Me.pnl_tlstrip.Size = New System.Drawing.Size(794, 56)
        Me.pnl_tlstrip.TabIndex = 4
        '
        'tlsDM
        '
        Me.tlsDM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsDM_Save, Me.tlsDM_Close})
        Me.tlsDM.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsDM.Name = "tlsDM"
        Me.tlsDM.Size = New System.Drawing.Size(794, 53)
        Me.tlsDM.TabIndex = 0
        Me.tlsDM.Text = "ToolStrip1"
        '
        'tlsDM_Save
        '
        Me.tlsDM_Save.Image = CType(resources.GetObject("tlsDM_Save.Image"), System.Drawing.Image)
        Me.tlsDM_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Save.Name = "tlsDM_Save"
        Me.tlsDM_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlsDM_Save.Tag = "Save"
        Me.tlsDM_Save.Text = "&Save&&Cls"
        Me.tlsDM_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsDM_Save.ToolTipText = "Save and Close"
        '
        'tlsDM_Close
        '
        Me.tlsDM_Close.Image = CType(resources.GetObject("tlsDM_Close.Image"), System.Drawing.Image)
        Me.tlsDM_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Close.Name = "tlsDM_Close"
        Me.tlsDM_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlsDM_Close.Tag = "Close"
        Me.tlsDM_Close.Text = "&Close"
        Me.tlsDM_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.GloUC_trvAssociates)
        Me.pnlRight.Controls.Add(Me.pnltrvTriggers)
        Me.pnlRight.Controls.Add(Me.pnltxtSearchOrder)
        Me.pnlRight.Controls.Add(Me.pnlbtnLab)
        Me.pnlRight.Controls.Add(Me.pnlbtnReferrals)
        Me.pnlRight.Controls.Add(Me.pnlbtnRx)
        Me.pnlRight.Controls.Add(Me.pnlbtnRadiologyTest)
        Me.pnlRight.Controls.Add(Me.pnlbtnGuideline)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(588, 30)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(206, 585)
        Me.pnlRight.TabIndex = 5
        '
        'GloUC_trvAssociates
        '
        Me.GloUC_trvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvAssociates.CheckBoxes = False
        Me.GloUC_trvAssociates.CodeMember = Nothing
        Me.GloUC_trvAssociates.DescriptionMember = Nothing
        Me.GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
        Me.GloUC_trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvAssociates.DrugFlag = CType(16, Short)
        Me.GloUC_trvAssociates.DrugFormMember = Nothing
        Me.GloUC_trvAssociates.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvAssociates.DurationMember = Nothing
        Me.GloUC_trvAssociates.FrequencyMember = Nothing
        Me.GloUC_trvAssociates.ImageIndex = 0
        Me.GloUC_trvAssociates.ImageList = Me.ImageList1
        Me.GloUC_trvAssociates.ImageObject = Nothing
        Me.GloUC_trvAssociates.IsDrug = False
        Me.GloUC_trvAssociates.IsNarcoticsMember = Nothing
        Me.GloUC_trvAssociates.Location = New System.Drawing.Point(0, 30)
        Me.GloUC_trvAssociates.MaximumNodes = 1000
        Me.GloUC_trvAssociates.Name = "GloUC_trvAssociates"
        Me.GloUC_trvAssociates.NDCCodeMember = Nothing
        Me.GloUC_trvAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.GloUC_trvAssociates.ParentImageIndex = 0
        Me.GloUC_trvAssociates.ParentMember = Nothing
        Me.GloUC_trvAssociates.RouteMember = Nothing
        Me.GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvAssociates.SearchBox = True
        Me.GloUC_trvAssociates.SearchText = Nothing
        Me.GloUC_trvAssociates.SelectedImageIndex = 0
        Me.GloUC_trvAssociates.SelectedNode = Nothing
        Me.GloUC_trvAssociates.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvAssociates.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvAssociates.SelectedParentImageIndex = 0
        Me.GloUC_trvAssociates.Size = New System.Drawing.Size(206, 435)
        Me.GloUC_trvAssociates.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvAssociates.TabIndex = 39
        Me.GloUC_trvAssociates.Tag = Nothing
        Me.GloUC_trvAssociates.UnitMember = Nothing
        Me.GloUC_trvAssociates.ValueMember = Nothing
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(1, "Pat Demographic.ico")
        Me.ImageList1.Images.SetKeyName(2, "History.ico")
        Me.ImageList1.Images.SetKeyName(3, "ICD 09.ico")
        Me.ImageList1.Images.SetKeyName(4, "Drugs.ico")
        Me.ImageList1.Images.SetKeyName(5, "CPT.ico")
        Me.ImageList1.Images.SetKeyName(6, "Lab.ico")
        Me.ImageList1.Images.SetKeyName(7, "Radiology.ico")
        Me.ImageList1.Images.SetKeyName(8, "Guideline Template.ico")
        Me.ImageList1.Images.SetKeyName(9, "RX.ico")
        Me.ImageList1.Images.SetKeyName(10, "PatientDetails.ico")
        Me.ImageList1.Images.SetKeyName(11, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(12, "Health plan (inbetween).ico")
        '
        'pnltrvTriggers
        '
        Me.pnltrvTriggers.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvTriggers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvTriggers.Controls.Add(Me.Label16)
        Me.pnltrvTriggers.Controls.Add(Me.trvTriggers)
        Me.pnltrvTriggers.Controls.Add(Me.Label74)
        Me.pnltrvTriggers.Controls.Add(Me.Label75)
        Me.pnltrvTriggers.Controls.Add(Me.Label76)
        Me.pnltrvTriggers.Controls.Add(Me.Label77)
        Me.pnltrvTriggers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvTriggers.Location = New System.Drawing.Point(0, 56)
        Me.pnltrvTriggers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrvTriggers.Name = "pnltrvTriggers"
        Me.pnltrvTriggers.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrvTriggers.Size = New System.Drawing.Size(206, 411)
        Me.pnltrvTriggers.TabIndex = 23
        Me.pnltrvTriggers.Visible = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(1, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(201, 4)
        Me.Label16.TabIndex = 38
        '
        'trvTriggers
        '
        Me.trvTriggers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTriggers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTriggers.ForeColor = System.Drawing.Color.Black
        Me.trvTriggers.HideSelection = False
        Me.trvTriggers.Location = New System.Drawing.Point(1, 1)
        Me.trvTriggers.Name = "trvTriggers"
        Me.trvTriggers.Size = New System.Drawing.Size(201, 406)
        Me.trvTriggers.TabIndex = 3
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label74.Location = New System.Drawing.Point(1, 407)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(201, 1)
        Me.Label74.TabIndex = 8
        Me.Label74.Text = "label2"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(0, 1)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(1, 407)
        Me.Label75.TabIndex = 7
        Me.Label75.Text = "label4"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label76.Location = New System.Drawing.Point(202, 1)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 407)
        Me.Label76.TabIndex = 6
        Me.Label76.Text = "label3"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(0, 0)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(203, 1)
        Me.Label77.TabIndex = 5
        Me.Label77.Text = "label1"
        '
        'pnltxtSearchOrder
        '
        Me.pnltxtSearchOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltxtSearchOrder.Controls.Add(Me.txtSearchOrder)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label20)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label21)
        Me.pnltxtSearchOrder.Controls.Add(Me.PictureBox1)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label15)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label82)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label83)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label84)
        Me.pnltxtSearchOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltxtSearchOrder.ForeColor = System.Drawing.Color.Black
        Me.pnltxtSearchOrder.Location = New System.Drawing.Point(0, 30)
        Me.pnltxtSearchOrder.Name = "pnltxtSearchOrder"
        Me.pnltxtSearchOrder.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltxtSearchOrder.Size = New System.Drawing.Size(206, 26)
        Me.pnltxtSearchOrder.TabIndex = 16
        Me.pnltxtSearchOrder.Visible = False
        '
        'txtSearchOrder
        '
        Me.txtSearchOrder.BackColor = System.Drawing.Color.White
        Me.txtSearchOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchOrder.ForeColor = System.Drawing.Color.Black
        Me.txtSearchOrder.Location = New System.Drawing.Point(29, 5)
        Me.txtSearchOrder.Name = "txtSearchOrder"
        Me.txtSearchOrder.Size = New System.Drawing.Size(173, 15)
        Me.txtSearchOrder.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(173, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(173, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 22)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(201, 1)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "label2"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(0, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1, 22)
        Me.Label82.TabIndex = 41
        Me.Label82.Text = "label4"
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label83.Location = New System.Drawing.Point(202, 1)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(1, 22)
        Me.Label83.TabIndex = 40
        Me.Label83.Text = "label3"
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(0, 0)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(203, 1)
        Me.Label84.TabIndex = 39
        Me.Label84.Text = "label1"
        '
        'pnlbtnLab
        '
        Me.pnlbtnLab.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnLab.Controls.Add(Me.btnLab)
        Me.pnlbtnLab.Controls.Add(Me.Label78)
        Me.pnlbtnLab.Controls.Add(Me.Label79)
        Me.pnlbtnLab.Controls.Add(Me.Label80)
        Me.pnlbtnLab.Controls.Add(Me.Label81)
        Me.pnlbtnLab.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnLab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlbtnLab.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnLab.Name = "pnlbtnLab"
        Me.pnlbtnLab.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnLab.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnLab.TabIndex = 24
        '
        'btnLab
        '
        Me.btnLab.BackColor = System.Drawing.Color.Transparent
        Me.btnLab.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLab.FlatAppearance.BorderSize = 0
        Me.btnLab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLab.Location = New System.Drawing.Point(1, 1)
        Me.btnLab.Name = "btnLab"
        Me.btnLab.Size = New System.Drawing.Size(201, 25)
        Me.btnLab.TabIndex = 0
        Me.btnLab.Tag = "Selected"
        Me.btnLab.Text = "&Lab"
        Me.btnLab.UseVisualStyleBackColor = False
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label78.Location = New System.Drawing.Point(1, 26)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(201, 1)
        Me.Label78.TabIndex = 8
        Me.Label78.Text = "label2"
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(0, 1)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1, 26)
        Me.Label79.TabIndex = 7
        Me.Label79.Text = "label4"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label80.Location = New System.Drawing.Point(202, 1)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 26)
        Me.Label80.TabIndex = 6
        Me.Label80.Text = "label3"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(0, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(203, 1)
        Me.Label81.TabIndex = 5
        Me.Label81.Text = "label1"
        '
        'pnlbtnReferrals
        '
        Me.pnlbtnReferrals.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnReferrals.Controls.Add(Me.btnReferrals)
        Me.pnlbtnReferrals.Controls.Add(Me.Label70)
        Me.pnlbtnReferrals.Controls.Add(Me.Label71)
        Me.pnlbtnReferrals.Controls.Add(Me.Label72)
        Me.pnlbtnReferrals.Controls.Add(Me.Label73)
        Me.pnlbtnReferrals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnReferrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnReferrals.Location = New System.Drawing.Point(0, 465)
        Me.pnlbtnReferrals.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnReferrals.Name = "pnlbtnReferrals"
        Me.pnlbtnReferrals.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnReferrals.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnReferrals.TabIndex = 22
        '
        'btnReferrals
        '
        Me.btnReferrals.BackgroundImage = CType(resources.GetObject("btnReferrals.BackgroundImage"), System.Drawing.Image)
        Me.btnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReferrals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReferrals.FlatAppearance.BorderSize = 0
        Me.btnReferrals.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnReferrals.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnReferrals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReferrals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReferrals.Location = New System.Drawing.Point(1, 1)
        Me.btnReferrals.Name = "btnReferrals"
        Me.btnReferrals.Size = New System.Drawing.Size(201, 25)
        Me.btnReferrals.TabIndex = 5
        Me.btnReferrals.Tag = "UnSelected"
        Me.btnReferrals.Text = "&Referrals"
        Me.btnReferrals.UseVisualStyleBackColor = True
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label70.Location = New System.Drawing.Point(1, 26)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(201, 1)
        Me.Label70.TabIndex = 8
        Me.Label70.Text = "label2"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(0, 1)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(1, 26)
        Me.Label71.TabIndex = 7
        Me.Label71.Text = "label4"
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label72.Location = New System.Drawing.Point(202, 1)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(1, 26)
        Me.Label72.TabIndex = 6
        Me.Label72.Text = "label3"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(0, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(203, 1)
        Me.Label73.TabIndex = 5
        Me.Label73.Text = "label1"
        '
        'pnlbtnRx
        '
        Me.pnlbtnRx.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnRx.Controls.Add(Me.btnRx)
        Me.pnlbtnRx.Controls.Add(Me.Label66)
        Me.pnlbtnRx.Controls.Add(Me.Label67)
        Me.pnlbtnRx.Controls.Add(Me.Label68)
        Me.pnlbtnRx.Controls.Add(Me.Label69)
        Me.pnlbtnRx.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnRx.Location = New System.Drawing.Point(0, 495)
        Me.pnlbtnRx.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnRx.Name = "pnlbtnRx"
        Me.pnlbtnRx.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRx.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnRx.TabIndex = 21
        '
        'btnRx
        '
        Me.btnRx.BackgroundImage = CType(resources.GetObject("btnRx.BackgroundImage"), System.Drawing.Image)
        Me.btnRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRx.FlatAppearance.BorderSize = 0
        Me.btnRx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRx.Location = New System.Drawing.Point(1, 1)
        Me.btnRx.Name = "btnRx"
        Me.btnRx.Size = New System.Drawing.Size(201, 25)
        Me.btnRx.TabIndex = 4
        Me.btnRx.Tag = "UnSelected"
        Me.btnRx.Text = "&Drugs"
        Me.btnRx.UseVisualStyleBackColor = True
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label66.Location = New System.Drawing.Point(1, 26)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(201, 1)
        Me.Label66.TabIndex = 8
        Me.Label66.Text = "label2"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(0, 1)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(1, 26)
        Me.Label67.TabIndex = 7
        Me.Label67.Text = "label4"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label68.Location = New System.Drawing.Point(202, 1)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(1, 26)
        Me.Label68.TabIndex = 6
        Me.Label68.Text = "label3"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(0, 0)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(203, 1)
        Me.Label69.TabIndex = 5
        Me.Label69.Text = "label1"
        '
        'pnlbtnRadiologyTest
        '
        Me.pnlbtnRadiologyTest.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnRadiologyTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnRadiologyTest.Controls.Add(Me.btnRadiologyTest)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label55)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label56)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label57)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label62)
        Me.pnlbtnRadiologyTest.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRadiologyTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnRadiologyTest.Location = New System.Drawing.Point(0, 525)
        Me.pnlbtnRadiologyTest.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnRadiologyTest.Name = "pnlbtnRadiologyTest"
        Me.pnlbtnRadiologyTest.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRadiologyTest.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnRadiologyTest.TabIndex = 20
        '
        'btnRadiologyTest
        '
        Me.btnRadiologyTest.BackgroundImage = CType(resources.GetObject("btnRadiologyTest.BackgroundImage"), System.Drawing.Image)
        Me.btnRadiologyTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiologyTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiologyTest.FlatAppearance.BorderSize = 0
        Me.btnRadiologyTest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyTest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiologyTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRadiologyTest.Location = New System.Drawing.Point(1, 1)
        Me.btnRadiologyTest.Name = "btnRadiologyTest"
        Me.btnRadiologyTest.Size = New System.Drawing.Size(201, 25)
        Me.btnRadiologyTest.TabIndex = 2
        Me.btnRadiologyTest.Tag = "UnSelected"
        Me.btnRadiologyTest.Text = "&Orders"
        Me.btnRadiologyTest.UseVisualStyleBackColor = True
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label55.Location = New System.Drawing.Point(1, 26)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(201, 1)
        Me.Label55.TabIndex = 8
        Me.Label55.Text = "label2"
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(0, 1)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 26)
        Me.Label56.TabIndex = 7
        Me.Label56.Text = "label4"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label57.Location = New System.Drawing.Point(202, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 26)
        Me.Label57.TabIndex = 6
        Me.Label57.Text = "label3"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(0, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(203, 1)
        Me.Label62.TabIndex = 5
        Me.Label62.Text = "label1"
        '
        'pnlbtnGuideline
        '
        Me.pnlbtnGuideline.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnGuideline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnGuideline.Controls.Add(Me.btnGuideline)
        Me.pnlbtnGuideline.Controls.Add(Me.Label17)
        Me.pnlbtnGuideline.Controls.Add(Me.Label18)
        Me.pnlbtnGuideline.Controls.Add(Me.Label19)
        Me.pnlbtnGuideline.Controls.Add(Me.Label22)
        Me.pnlbtnGuideline.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnGuideline.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnGuideline.Location = New System.Drawing.Point(0, 555)
        Me.pnlbtnGuideline.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnGuideline.Name = "pnlbtnGuideline"
        Me.pnlbtnGuideline.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnGuideline.Size = New System.Drawing.Size(206, 30)
        Me.pnlbtnGuideline.TabIndex = 20
        '
        'btnGuideline
        '
        Me.btnGuideline.BackgroundImage = CType(resources.GetObject("btnGuideline.BackgroundImage"), System.Drawing.Image)
        Me.btnGuideline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGuideline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGuideline.FlatAppearance.BorderSize = 0
        Me.btnGuideline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnGuideline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnGuideline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuideline.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuideline.Location = New System.Drawing.Point(1, 1)
        Me.btnGuideline.Name = "btnGuideline"
        Me.btnGuideline.Size = New System.Drawing.Size(201, 25)
        Me.btnGuideline.TabIndex = 1
        Me.btnGuideline.Tag = "UnSelected"
        Me.btnGuideline.Text = "&Guidelines"
        Me.btnGuideline.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(1, 26)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(201, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 26)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(202, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 26)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(203, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlSummaryOthers)
        Me.pnlMain.Controls.Add(Me.pnlRight)
        Me.pnlMain.Controls.Add(Me.pnlMsgTOP)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(794, 615)
        Me.pnlMain.TabIndex = 6
        '
        'pnlSummaryOthers
        '
        Me.pnlSummaryOthers.Controls.Add(Me.pnlGuideline)
        Me.pnlSummaryOthers.Controls.Add(Me.pnlGuidelineHeader)
        Me.pnlSummaryOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSummaryOthers.Location = New System.Drawing.Point(0, 30)
        Me.pnlSummaryOthers.Name = "pnlSummaryOthers"
        Me.pnlSummaryOthers.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlSummaryOthers.Size = New System.Drawing.Size(588, 585)
        Me.pnlSummaryOthers.TabIndex = 8
        '
        'pnlGuideline
        '
        Me.pnlGuideline.Controls.Add(Me.trvHealthPlan)
        Me.pnlGuideline.Controls.Add(Me.Label1)
        Me.pnlGuideline.Controls.Add(Me.Label99)
        Me.pnlGuideline.Controls.Add(Me.Label100)
        Me.pnlGuideline.Controls.Add(Me.Label101)
        Me.pnlGuideline.Controls.Add(Me.Label102)
        Me.pnlGuideline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGuideline.Location = New System.Drawing.Point(3, 27)
        Me.pnlGuideline.Name = "pnlGuideline"
        Me.pnlGuideline.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGuideline.Size = New System.Drawing.Size(582, 558)
        Me.pnlGuideline.TabIndex = 2
        '
        'trvHealthPlan
        '
        Me.trvHealthPlan.BackColor = System.Drawing.Color.White
        Me.trvHealthPlan.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHealthPlan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvHealthPlan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvHealthPlan.ForeColor = System.Drawing.Color.Black
        Me.trvHealthPlan.HideSelection = False
        Me.trvHealthPlan.ImageIndex = 0
        Me.trvHealthPlan.ImageList = Me.ImageList1
        Me.trvHealthPlan.ItemHeight = 21
        Me.trvHealthPlan.Location = New System.Drawing.Point(1, 4)
        Me.trvHealthPlan.Name = "trvHealthPlan"
        Me.trvHealthPlan.SelectedImageIndex = 0
        Me.trvHealthPlan.ShowLines = False
        Me.trvHealthPlan.Size = New System.Drawing.Size(580, 550)
        Me.trvHealthPlan.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(580, 3)
        Me.Label1.TabIndex = 13
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label99.Location = New System.Drawing.Point(1, 554)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(580, 1)
        Me.Label99.TabIndex = 12
        Me.Label99.Text = "label2"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label100.Location = New System.Drawing.Point(0, 1)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(1, 554)
        Me.Label100.TabIndex = 11
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label101.Location = New System.Drawing.Point(581, 1)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 554)
        Me.Label101.TabIndex = 10
        Me.Label101.Text = "label3"
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.Location = New System.Drawing.Point(0, 0)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(582, 1)
        Me.Label102.TabIndex = 9
        Me.Label102.Text = "label1"
        '
        'pnlGuidelineHeader
        '
        Me.pnlGuidelineHeader.Controls.Add(Me.pnl3)
        Me.pnlGuidelineHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGuidelineHeader.Location = New System.Drawing.Point(3, 0)
        Me.pnlGuidelineHeader.Name = "pnlGuidelineHeader"
        Me.pnlGuidelineHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGuidelineHeader.Size = New System.Drawing.Size(582, 27)
        Me.pnlGuidelineHeader.TabIndex = 5
        '
        'pnl3
        '
        Me.pnl3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl3.Controls.Add(Me.Label95)
        Me.pnl3.Controls.Add(Me.Label96)
        Me.pnl3.Controls.Add(Me.Label97)
        Me.pnl3.Controls.Add(Me.Label98)
        Me.pnl3.Controls.Add(Me.Label6)
        Me.pnl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl3.Location = New System.Drawing.Point(0, 0)
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(582, 24)
        Me.pnl3.TabIndex = 1
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label95.Location = New System.Drawing.Point(1, 23)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(580, 1)
        Me.Label95.TabIndex = 12
        Me.Label95.Text = "label2"
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.Location = New System.Drawing.Point(0, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 23)
        Me.Label96.TabIndex = 11
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label97.Location = New System.Drawing.Point(581, 1)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1, 23)
        Me.Label97.TabIndex = 10
        Me.Label97.Text = "label3"
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(0, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(582, 1)
        Me.Label98.TabIndex = 9
        Me.Label98.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Image = CType(resources.GetObject("Label6.Image"), System.Drawing.Image)
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(582, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "       Orders to be given"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMsgTOP
        '
        Me.pnlMsgTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsgTOP.Controls.Add(Me.pnlMsg)
        Me.pnlMsgTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMsgTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnlMsgTOP.Name = "pnlMsgTOP"
        Me.pnlMsgTOP.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMsgTOP.Size = New System.Drawing.Size(794, 30)
        Me.pnlMsgTOP.TabIndex = 7
        '
        'pnlMsg
        '
        Me.pnlMsg.BackColor = System.Drawing.Color.Transparent
        Me.pnlMsg.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsg.Controls.Add(Me.txtMessage)
        Me.pnlMsg.Controls.Add(Me.Label4)
        Me.pnlMsg.Controls.Add(Me.txtName)
        Me.pnlMsg.Controls.Add(Me.Label3)
        Me.pnlMsg.Controls.Add(Me.Label23)
        Me.pnlMsg.Controls.Add(Me.Label24)
        Me.pnlMsg.Controls.Add(Me.Label63)
        Me.pnlMsg.Controls.Add(Me.Label25)
        Me.pnlMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMsg.Location = New System.Drawing.Point(3, 3)
        Me.pnlMsg.Name = "pnlMsg"
        Me.pnlMsg.Size = New System.Drawing.Size(788, 24)
        Me.pnlMsg.TabIndex = 3
        '
        'txtMessage
        '
        Me.txtMessage.BackColor = System.Drawing.Color.White
        Me.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessage.ForeColor = System.Drawing.Color.Black
        Me.txtMessage.Location = New System.Drawing.Point(367, 1)
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.Size = New System.Drawing.Size(420, 22)
        Me.txtMessage.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(282, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 22)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "   Message :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(61, 1)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(221, 22)
        Me.txtName.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 22)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "  Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(786, 1)
        Me.Label23.TabIndex = 17
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
        Me.Label24.TabIndex = 16
        Me.Label24.Text = "label4"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(787, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 23)
        Me.Label63.TabIndex = 15
        Me.Label63.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(788, 1)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "label1"
        '
        'CntConditions
        '
        Me.CntConditions.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDelete, Me.mnuReferral, Me.EditReferral, Me.mnuEditTemplate})
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 0
        Me.mnuDelete.Text = "Delete "
        '
        'mnuReferral
        '
        Me.mnuReferral.Index = 1
        Me.mnuReferral.Text = "Add Referral"
        '
        'EditReferral
        '
        Me.EditReferral.Index = 2
        Me.EditReferral.Text = "Edit Referrals"
        '
        'mnuEditTemplate
        '
        Me.mnuEditTemplate.Index = 3
        Me.mnuEditTemplate.Text = "Edit Template"
        '
        'frmDM_PatientCriteria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(794, 671)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDM_PatientCriteria"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Patient Criteria"
        Me.pnl_tlstrip.ResumeLayout(False)
        Me.pnl_tlstrip.PerformLayout()
        Me.tlsDM.ResumeLayout(False)
        Me.tlsDM.PerformLayout()
        Me.pnlRight.ResumeLayout(False)
        Me.pnltrvTriggers.ResumeLayout(False)
        Me.pnltxtSearchOrder.ResumeLayout(False)
        Me.pnltxtSearchOrder.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbtnLab.ResumeLayout(False)
        Me.pnlbtnReferrals.ResumeLayout(False)
        Me.pnlbtnRx.ResumeLayout(False)
        Me.pnlbtnRadiologyTest.ResumeLayout(False)
        Me.pnlbtnGuideline.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlSummaryOthers.ResumeLayout(False)
        Me.pnlGuideline.ResumeLayout(False)
        Me.pnlGuidelineHeader.ResumeLayout(False)
        Me.pnl3.ResumeLayout(False)
        Me.pnlMsgTOP.ResumeLayout(False)
        Me.pnlMsg.ResumeLayout(False)
        Me.pnlMsg.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl_tlstrip As System.Windows.Forms.Panel
    Friend WithEvents tlsDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsDM_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsDM_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnltrvTriggers As System.Windows.Forms.Panel
    Friend WithEvents trvTriggers As System.Windows.Forms.TreeView
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents pnltxtSearchOrder As System.Windows.Forms.Panel
    Friend WithEvents txtSearchOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnLab As System.Windows.Forms.Panel
    Friend WithEvents btnLab As System.Windows.Forms.Button
    Private WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnReferrals As System.Windows.Forms.Panel
    Friend WithEvents btnReferrals As System.Windows.Forms.Button
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnRx As System.Windows.Forms.Panel
    Friend WithEvents btnRx As System.Windows.Forms.Button
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnRadiologyTest As System.Windows.Forms.Panel
    Friend WithEvents btnRadiologyTest As System.Windows.Forms.Button
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnGuideline As System.Windows.Forms.Panel
    Friend WithEvents btnGuideline As System.Windows.Forms.Button
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlMsgTOP As System.Windows.Forms.Panel
    Friend WithEvents pnlMsg As System.Windows.Forms.Panel
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlSummaryOthers As System.Windows.Forms.Panel
    Friend WithEvents pnlGuideline As System.Windows.Forms.Panel
    Private WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents trvHealthPlan As System.Windows.Forms.TreeView
    Friend WithEvents pnlGuidelineHeader As System.Windows.Forms.Panel
    Friend WithEvents pnl3 As System.Windows.Forms.Panel
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Private WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents CntConditions As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReferral As System.Windows.Forms.MenuItem
    Friend WithEvents EditReferral As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditTemplate As System.Windows.Forms.MenuItem
    Friend WithEvents GloUC_trvAssociates As gloUserControlLibrary.gloUC_TreeView
    Private WithEvents Label1 As System.Windows.Forms.Label
End Class
