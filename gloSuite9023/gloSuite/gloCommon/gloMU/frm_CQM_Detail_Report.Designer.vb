<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CQM_Detail_Report
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    If (IsNothing(dtpicEndDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpicEndDate)
                        Catch ex As Exception

                        End Try


                        dtpicEndDate.Dispose()
                        dtpicEndDate = Nothing
                    End If
                Catch
                End Try


                Try
                    If (IsNothing(dtpicStartDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpicStartDate)
                        Catch ex As Exception

                        End Try


                        dtpicStartDate.Dispose()
                        dtpicStartDate = Nothing
                    End If
                Catch
                End Try


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CQM_Detail_Report))
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_MU_Detail = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btndetail = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.C1Denominator = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlLabs = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftMain = New System.Windows.Forms.Panel()
        Me.C1Numerator = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.C1CQMDetail = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1CQMDetail1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.pnlMeasurementPeriod = New System.Windows.Forms.Panel()
        Me.dtpicEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpicStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblProviderName = New System.Windows.Forms.Label()
        Me.lblReportName = New System.Windows.Forms.Label()
        Me.lblNPI = New System.Windows.Forms.Label()
        Me.lblReportingYear = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lnkhelp = New System.Windows.Forms.LinkLabel()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lbl_TaxIDValue = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlExcl_Excp = New System.Windows.Forms.Panel()
        Me.pnlExcp = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.C1DenominatorExceptions = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.plnDenominatorExceptionsSearch = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlExcl = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.C1DenominatorExclusions = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.plnDenominatorExclusionSearch = New System.Windows.Forms.Panel()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.ImgFlag = New System.Windows.Forms.ImageList(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_MU_Detail.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.C1Denominator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlLabs.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftMain.SuspendLayout()
        CType(Me.C1Numerator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnl.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.C1CQMDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1CQMDetail1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.pnlMeasurementPeriod.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlExcl_Excp.SuspendLayout()
        Me.pnlExcp.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.C1DenominatorExceptions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.pnlExcl.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.C1DenominatorExclusions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel16.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(1, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(240, 22)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "Patients meeting objective criteria :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 342)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(574, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(576, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 23)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(574, 1)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 23)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(578, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 339)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 339)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(575, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(576, 1)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_MU_Detail)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1264, 54)
        Me.pnlToolStrip.TabIndex = 15
        '
        'ts_MU_Detail
        '
        Me.ts_MU_Detail.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_MU_Detail.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Toolstrip
        Me.ts_MU_Detail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_MU_Detail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_MU_Detail.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_MU_Detail.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_MU_Detail.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btndetail, Me.ts_btnClose})
        Me.ts_MU_Detail.Location = New System.Drawing.Point(0, 0)
        Me.ts_MU_Detail.Name = "ts_MU_Detail"
        Me.ts_MU_Detail.Size = New System.Drawing.Size(1264, 53)
        Me.ts_MU_Detail.TabIndex = 1
        Me.ts_MU_Detail.Text = "ToolStrip1"
        '
        'ts_btndetail
        '
        Me.ts_btndetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btndetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btndetail.Image = CType(resources.GetObject("ts_btndetail.Image"), System.Drawing.Image)
        Me.ts_btndetail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btndetail.Name = "ts_btndetail"
        Me.ts_btndetail.Size = New System.Drawing.Size(80, 50)
        Me.ts_btndetail.Tag = "CQMSetup"
        Me.ts_btndetail.Text = "&CQM Setup"
        Me.ts_btndetail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'imgTreeView
        '
        Me.imgTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imgTreeView.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Button
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Panel1)
        Me.Panel6.Controls.Add(Me.Label20)
        Me.Panel6.Controls.Add(Me.Label22)
        Me.Panel6.Controls.Add(Me.Label23)
        Me.Panel6.Controls.Add(Me.Label24)
        Me.Panel6.Controls.Add(Me.Label25)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(576, 24)
        Me.Panel6.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(289, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(286, 22)
        Me.Panel1.TabIndex = 13
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.C1Denominator)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 27)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel4.Size = New System.Drawing.Size(682, 340)
        Me.Panel4.TabIndex = 10
        '
        'C1Denominator
        '
        Me.C1Denominator.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Denominator.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1Denominator.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1Denominator.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Denominator.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Denominator.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Denominator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Denominator.ExtendLastCol = True
        Me.C1Denominator.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Denominator.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Denominator.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Denominator.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Denominator.Location = New System.Drawing.Point(1, 1)
        Me.C1Denominator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Denominator.Name = "C1Denominator"
        Me.C1Denominator.Rows.Count = 1
        Me.C1Denominator.Rows.DefaultSize = 19
        Me.C1Denominator.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Denominator.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Denominator.ShowCellLabels = True
        Me.C1Denominator.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Denominator.Size = New System.Drawing.Size(677, 338)
        Me.C1Denominator.StyleInfo = resources.GetString("C1Denominator.StyleInfo")
        Me.C1Denominator.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(1, 339)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(677, 1)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 339)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(678, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 339)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(679, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label1"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlRight)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlLeft)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMain.Location = New System.Drawing.Point(0, 275)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1264, 367)
        Me.pnlMain.TabIndex = 13
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.Panel4)
        Me.pnlRight.Controls.Add(Me.Panel3)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(582, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(682, 367)
        Me.pnlRight.TabIndex = 113
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlLabs)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(682, 27)
        Me.Panel3.TabIndex = 9
        '
        'pnlLabs
        '
        Me.pnlLabs.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Button
        Me.pnlLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLabs.Controls.Add(Me.Panel8)
        Me.pnlLabs.Controls.Add(Me.Label11)
        Me.pnlLabs.Controls.Add(Me.Label14)
        Me.pnlLabs.Controls.Add(Me.Label15)
        Me.pnlLabs.Controls.Add(Me.Label16)
        Me.pnlLabs.Controls.Add(Me.Label17)
        Me.pnlLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabs.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabs.Name = "pnlLabs"
        Me.pnlLabs.Size = New System.Drawing.Size(679, 24)
        Me.pnlLabs.TabIndex = 4
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel8.Location = New System.Drawing.Point(406, 1)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(272, 22)
        Me.Panel8.TabIndex = 50
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(1, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label11.Size = New System.Drawing.Size(375, 22)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Unique patients seen who do NOT meet objective criteria :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(677, 1)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 23)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(678, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 23)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(679, 1)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(579, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 367)
        Me.Splitter1.TabIndex = 114
        Me.Splitter1.TabStop = False
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.pnlLeftMain)
        Me.pnlLeft.Controls.Add(Me.Panel5)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(579, 367)
        Me.pnlLeft.TabIndex = 112
        '
        'pnlLeftMain
        '
        Me.pnlLeftMain.Controls.Add(Me.C1Numerator)
        Me.pnlLeftMain.Controls.Add(Me.Label5)
        Me.pnlLeftMain.Controls.Add(Me.Label6)
        Me.pnlLeftMain.Controls.Add(Me.Label7)
        Me.pnlLeftMain.Controls.Add(Me.Label8)
        Me.pnlLeftMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftMain.Location = New System.Drawing.Point(0, 24)
        Me.pnlLeftMain.Name = "pnlLeftMain"
        Me.pnlLeftMain.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnlLeftMain.Size = New System.Drawing.Size(579, 343)
        Me.pnlLeftMain.TabIndex = 3
        '
        'C1Numerator
        '
        Me.C1Numerator.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Numerator.AllowEditing = False
        Me.C1Numerator.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1Numerator.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1Numerator.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Numerator.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Numerator.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Numerator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Numerator.ExtendLastCol = True
        Me.C1Numerator.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Numerator.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Numerator.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Numerator.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Numerator.Location = New System.Drawing.Point(4, 4)
        Me.C1Numerator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Numerator.Name = "C1Numerator"
        Me.C1Numerator.Rows.Count = 1
        Me.C1Numerator.Rows.DefaultSize = 19
        Me.C1Numerator.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Numerator.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Numerator.ShowCellLabels = True
        Me.C1Numerator.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Numerator.Size = New System.Drawing.Size(574, 338)
        Me.C1Numerator.StyleInfo = resources.GetString("C1Numerator.StyleInfo")
        Me.C1Numerator.TabIndex = 10
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel5.Size = New System.Drawing.Size(579, 24)
        Me.Panel5.TabIndex = 8
        '
        'pnl
        '
        Me.pnl.Controls.Add(Me.Panel7)
        Me.pnl.Controls.Add(Me.Panel9)
        Me.pnl.Controls.Add(Me.Panel2)
        Me.pnl.Controls.Add(Me.lbl_TaxIDValue)
        Me.pnl.Controls.Add(Me.Label2)
        Me.pnl.Controls.Add(Me.Label3)
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.Label9)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl.Location = New System.Drawing.Point(0, 54)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl.Size = New System.Drawing.Size(1264, 221)
        Me.pnl.TabIndex = 111
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.C1CQMDetail)
        Me.Panel7.Controls.Add(Me.C1CQMDetail1)
        Me.Panel7.Controls.Add(Me.Label47)
        Me.Panel7.Controls.Add(Me.C1FlexGrid1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(4, 28)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1256, 152)
        Me.Panel7.TabIndex = 128
        '
        'C1CQMDetail
        '
        Me.C1CQMDetail.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1CQMDetail.AllowEditing = False
        Me.C1CQMDetail.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1CQMDetail.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1CQMDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1CQMDetail.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1CQMDetail.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1CQMDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1CQMDetail.ExtendLastCol = True
        Me.C1CQMDetail.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1CQMDetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1CQMDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1CQMDetail.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never
        Me.C1CQMDetail.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1CQMDetail.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1CQMDetail.Location = New System.Drawing.Point(0, 1)
        Me.C1CQMDetail.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1CQMDetail.Name = "C1CQMDetail"
        Me.C1CQMDetail.Rows.Count = 1
        Me.C1CQMDetail.Rows.DefaultSize = 19
        Me.C1CQMDetail.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1CQMDetail.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1CQMDetail.ShowCellLabels = True
        Me.C1CQMDetail.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1CQMDetail.Size = New System.Drawing.Size(1256, 151)
        Me.C1CQMDetail.StyleInfo = resources.GetString("C1CQMDetail.StyleInfo")
        Me.C1CQMDetail.TabIndex = 107
        '
        'C1CQMDetail1
        '
        Me.C1CQMDetail1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1CQMDetail1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1CQMDetail1.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.C1CQMDetail1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1CQMDetail1.ExtendLastCol = True
        Me.C1CQMDetail1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1CQMDetail1.Location = New System.Drawing.Point(0, 1)
        Me.C1CQMDetail1.Name = "C1CQMDetail1"
        Me.C1CQMDetail1.Rows.Count = 1
        Me.C1CQMDetail1.Rows.DefaultSize = 19
        Me.C1CQMDetail1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1CQMDetail1.Size = New System.Drawing.Size(1256, 151)
        Me.C1CQMDetail1.StyleInfo = resources.GetString("C1CQMDetail1.StyleInfo")
        Me.C1CQMDetail1.TabIndex = 106
        Me.C1CQMDetail1.Visible = False
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(0, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1256, 1)
        Me.Label47.TabIndex = 12
        Me.Label47.Text = "label1"
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexGrid1.AllowEditing = False
        Me.C1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1FlexGrid1.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid1.ExtendLastCol = True
        Me.C1FlexGrid1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1FlexGrid1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1FlexGrid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1FlexGrid1.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexGrid1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.Count = 1
        Me.C1FlexGrid1.Rows.DefaultSize = 19
        Me.C1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1FlexGrid1.ShowCellLabels = True
        Me.C1FlexGrid1.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1FlexGrid1.Size = New System.Drawing.Size(1256, 152)
        Me.C1FlexGrid1.StyleInfo = resources.GetString("C1FlexGrid1.StyleInfo")
        Me.C1FlexGrid1.TabIndex = 11
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Label30)
        Me.Panel9.Controls.Add(Me.pnlMeasurementPeriod)
        Me.Panel9.Controls.Add(Me.Label29)
        Me.Panel9.Controls.Add(Me.lblProviderName)
        Me.Panel9.Controls.Add(Me.lblReportName)
        Me.Panel9.Controls.Add(Me.lblNPI)
        Me.Panel9.Controls.Add(Me.lblReportingYear)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel9.Location = New System.Drawing.Point(4, 180)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1256, 37)
        Me.Panel9.TabIndex = 128
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1256, 1)
        Me.Label30.TabIndex = 127
        '
        'pnlMeasurementPeriod
        '
        Me.pnlMeasurementPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMeasurementPeriod.Controls.Add(Me.dtpicEndDate)
        Me.pnlMeasurementPeriod.Controls.Add(Me.dtpicStartDate)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label21)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label26)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label27)
        Me.pnlMeasurementPeriod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMeasurementPeriod.Location = New System.Drawing.Point(322, 3)
        Me.pnlMeasurementPeriod.Name = "pnlMeasurementPeriod"
        Me.pnlMeasurementPeriod.Size = New System.Drawing.Size(427, 31)
        Me.pnlMeasurementPeriod.TabIndex = 110
        '
        'dtpicEndDate
        '
        Me.dtpicEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicEndDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpicEndDate.Enabled = False
        Me.dtpicEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicEndDate.Location = New System.Drawing.Point(309, 4)
        Me.dtpicEndDate.Name = "dtpicEndDate"
        Me.dtpicEndDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpicEndDate.TabIndex = 10
        '
        'dtpicStartDate
        '
        Me.dtpicStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicStartDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpicStartDate.Enabled = False
        Me.dtpicStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicStartDate.Location = New System.Drawing.Point(178, 4)
        Me.dtpicStartDate.Name = "dtpicStartDate"
        Me.dtpicStartDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpicStartDate.TabIndex = 9
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(4, 8)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(127, 14)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "Measurement Period :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(136, 8)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(38, 14)
        Me.Label26.TabIndex = 6
        Me.Label26.Text = "From "
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(282, 8)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(26, 14)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "To "
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(19, 12)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(59, 14)
        Me.Label29.TabIndex = 10
        Me.Label29.Text = "Provider :"
        '
        'lblProviderName
        '
        Me.lblProviderName.AutoSize = True
        Me.lblProviderName.BackColor = System.Drawing.Color.Transparent
        Me.lblProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblProviderName.Location = New System.Drawing.Point(79, 12)
        Me.lblProviderName.Name = "lblProviderName"
        Me.lblProviderName.Size = New System.Drawing.Size(0, 14)
        Me.lblProviderName.TabIndex = 122
        '
        'lblReportName
        '
        Me.lblReportName.AutoSize = True
        Me.lblReportName.BackColor = System.Drawing.Color.Transparent
        Me.lblReportName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReportName.Location = New System.Drawing.Point(370, 11)
        Me.lblReportName.Name = "lblReportName"
        Me.lblReportName.Size = New System.Drawing.Size(0, 14)
        Me.lblReportName.TabIndex = 126
        '
        'lblNPI
        '
        Me.lblNPI.AutoSize = True
        Me.lblNPI.BackColor = System.Drawing.Color.Transparent
        Me.lblNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNPI.Location = New System.Drawing.Point(643, 11)
        Me.lblNPI.Name = "lblNPI"
        Me.lblNPI.Size = New System.Drawing.Size(0, 14)
        Me.lblNPI.TabIndex = 123
        '
        'lblReportingYear
        '
        Me.lblReportingYear.AutoSize = True
        Me.lblReportingYear.BackColor = System.Drawing.Color.Transparent
        Me.lblReportingYear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportingYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReportingYear.Location = New System.Drawing.Point(124, 12)
        Me.lblReportingYear.Name = "lblReportingYear"
        Me.lblReportingYear.Size = New System.Drawing.Size(0, 14)
        Me.lblReportingYear.TabIndex = 124
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lnkhelp)
        Me.Panel2.Controls.Add(Me.Label33)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1256, 24)
        Me.Panel2.TabIndex = 127
        '
        'lnkhelp
        '
        Me.lnkhelp.AutoSize = True
        Me.lnkhelp.Dock = System.Windows.Forms.DockStyle.Left
        Me.lnkhelp.Location = New System.Drawing.Point(195, 0)
        Me.lnkhelp.Name = "lnkhelp"
        Me.lnkhelp.Padding = New System.Windows.Forms.Padding(15, 4, 0, 0)
        Me.lnkhelp.Size = New System.Drawing.Size(109, 17)
        Me.lnkhelp.TabIndex = 126
        Me.lnkhelp.TabStop = True
        Me.lnkhelp.Text = "Click here for Help"
        '
        'Label33
        '
        Me.Label33.AutoEllipsis = True
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(0, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Padding = New System.Windows.Forms.Padding(10, 4, 0, 0)
        Me.Label33.Size = New System.Drawing.Size(195, 18)
        Me.Label33.TabIndex = 125
        Me.Label33.Text = "List of Patients in Numerator"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_TaxIDValue
        '
        Me.lbl_TaxIDValue.AutoSize = True
        Me.lbl_TaxIDValue.BackColor = System.Drawing.Color.Transparent
        Me.lbl_TaxIDValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TaxIDValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TaxIDValue.Location = New System.Drawing.Point(891, 42)
        Me.lbl_TaxIDValue.Name = "lbl_TaxIDValue"
        Me.lbl_TaxIDValue.Size = New System.Drawing.Size(0, 14)
        Me.lbl_TaxIDValue.TabIndex = 120
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(1260, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 213)
        Me.Label2.TabIndex = 117
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 213)
        Me.Label3.TabIndex = 116
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(3, 217)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1258, 1)
        Me.Label4.TabIndex = 113
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1258, 1)
        Me.Label9.TabIndex = 112
        '
        'pnlExcl_Excp
        '
        Me.pnlExcl_Excp.Controls.Add(Me.pnlExcp)
        Me.pnlExcl_Excp.Controls.Add(Me.Splitter2)
        Me.pnlExcl_Excp.Controls.Add(Me.pnlExcl)
        Me.pnlExcl_Excp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExcl_Excp.Location = New System.Drawing.Point(0, 645)
        Me.pnlExcl_Excp.Name = "pnlExcl_Excp"
        Me.pnlExcl_Excp.Size = New System.Drawing.Size(1264, 284)
        Me.pnlExcl_Excp.TabIndex = 112
        '
        'pnlExcp
        '
        Me.pnlExcp.Controls.Add(Me.Panel10)
        Me.pnlExcp.Controls.Add(Me.Panel11)
        Me.pnlExcp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExcp.Location = New System.Drawing.Point(582, 0)
        Me.pnlExcp.Name = "pnlExcp"
        Me.pnlExcp.Size = New System.Drawing.Size(682, 284)
        Me.pnlExcp.TabIndex = 113
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.C1DenominatorExceptions)
        Me.Panel10.Controls.Add(Me.Label1)
        Me.Panel10.Controls.Add(Me.Label10)
        Me.Panel10.Controls.Add(Me.Label28)
        Me.Panel10.Controls.Add(Me.Label31)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 27)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel10.Size = New System.Drawing.Size(682, 257)
        Me.Panel10.TabIndex = 10
        '
        'C1DenominatorExceptions
        '
        Me.C1DenominatorExceptions.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1DenominatorExceptions.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1DenominatorExceptions.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1DenominatorExceptions.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1DenominatorExceptions.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1DenominatorExceptions.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1DenominatorExceptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1DenominatorExceptions.ExtendLastCol = True
        Me.C1DenominatorExceptions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1DenominatorExceptions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1DenominatorExceptions.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1DenominatorExceptions.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1DenominatorExceptions.Location = New System.Drawing.Point(1, 1)
        Me.C1DenominatorExceptions.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1DenominatorExceptions.Name = "C1DenominatorExceptions"
        Me.C1DenominatorExceptions.Rows.Count = 1
        Me.C1DenominatorExceptions.Rows.DefaultSize = 19
        Me.C1DenominatorExceptions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1DenominatorExceptions.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1DenominatorExceptions.ShowCellLabels = True
        Me.C1DenominatorExceptions.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1DenominatorExceptions.Size = New System.Drawing.Size(677, 252)
        Me.C1DenominatorExceptions.StyleInfo = resources.GetString("C1DenominatorExceptions.StyleInfo")
        Me.C1DenominatorExceptions.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 253)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(677, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 253)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(678, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 253)
        Me.Label28.TabIndex = 6
        Me.Label28.Text = "label3"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(679, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Panel12)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel11.Size = New System.Drawing.Size(682, 27)
        Me.Panel11.TabIndex = 9
        '
        'Panel12
        '
        Me.Panel12.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Button
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.plnDenominatorExceptionsSearch)
        Me.Panel12.Controls.Add(Me.Label32)
        Me.Panel12.Controls.Add(Me.Label34)
        Me.Panel12.Controls.Add(Me.Label35)
        Me.Panel12.Controls.Add(Me.Label36)
        Me.Panel12.Controls.Add(Me.Label37)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(679, 24)
        Me.Panel12.TabIndex = 4
        '
        'plnDenominatorExceptionsSearch
        '
        Me.plnDenominatorExceptionsSearch.BackColor = System.Drawing.Color.Transparent
        Me.plnDenominatorExceptionsSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.plnDenominatorExceptionsSearch.Location = New System.Drawing.Point(406, 1)
        Me.plnDenominatorExceptionsSearch.Name = "plnDenominatorExceptionsSearch"
        Me.plnDenominatorExceptionsSearch.Size = New System.Drawing.Size(272, 22)
        Me.plnDenominatorExceptionsSearch.TabIndex = 50
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(1, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label32.Size = New System.Drawing.Size(375, 22)
        Me.Label32.TabIndex = 12
        Me.Label32.Text = "Denominator Exceptions :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(1, 23)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(677, 1)
        Me.Label34.TabIndex = 9
        Me.Label34.Text = "label2"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(0, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 23)
        Me.Label35.TabIndex = 8
        Me.Label35.Text = "label4"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(678, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 23)
        Me.Label36.TabIndex = 7
        Me.Label36.Text = "label3"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(0, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(679, 1)
        Me.Label37.TabIndex = 6
        Me.Label37.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.Location = New System.Drawing.Point(579, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 284)
        Me.Splitter2.TabIndex = 114
        Me.Splitter2.TabStop = False
        '
        'pnlExcl
        '
        Me.pnlExcl.Controls.Add(Me.Panel15)
        Me.pnlExcl.Controls.Add(Me.Panel16)
        Me.pnlExcl.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlExcl.Location = New System.Drawing.Point(0, 0)
        Me.pnlExcl.Name = "pnlExcl"
        Me.pnlExcl.Size = New System.Drawing.Size(579, 284)
        Me.pnlExcl.TabIndex = 112
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.C1DenominatorExclusions)
        Me.Panel15.Controls.Add(Me.Label38)
        Me.Panel15.Controls.Add(Me.Label39)
        Me.Panel15.Controls.Add(Me.Label40)
        Me.Panel15.Controls.Add(Me.Label41)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Location = New System.Drawing.Point(0, 24)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel15.Size = New System.Drawing.Size(579, 260)
        Me.Panel15.TabIndex = 3
        '
        'C1DenominatorExclusions
        '
        Me.C1DenominatorExclusions.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1DenominatorExclusions.AllowEditing = False
        Me.C1DenominatorExclusions.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1DenominatorExclusions.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1DenominatorExclusions.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1DenominatorExclusions.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1DenominatorExclusions.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1DenominatorExclusions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1DenominatorExclusions.ExtendLastCol = True
        Me.C1DenominatorExclusions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1DenominatorExclusions.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1DenominatorExclusions.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1DenominatorExclusions.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1DenominatorExclusions.Location = New System.Drawing.Point(4, 4)
        Me.C1DenominatorExclusions.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1DenominatorExclusions.Name = "C1DenominatorExclusions"
        Me.C1DenominatorExclusions.Rows.Count = 1
        Me.C1DenominatorExclusions.Rows.DefaultSize = 19
        Me.C1DenominatorExclusions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1DenominatorExclusions.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1DenominatorExclusions.ShowCellLabels = True
        Me.C1DenominatorExclusions.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1DenominatorExclusions.Size = New System.Drawing.Size(574, 252)
        Me.C1DenominatorExclusions.StyleInfo = resources.GetString("C1DenominatorExclusions.StyleInfo")
        Me.C1DenominatorExclusions.TabIndex = 10
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(4, 256)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(574, 1)
        Me.Label38.TabIndex = 8
        Me.Label38.Text = "label2"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(3, 4)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 253)
        Me.Label39.TabIndex = 7
        Me.Label39.Text = "label4"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label40.Location = New System.Drawing.Point(578, 4)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 253)
        Me.Label40.TabIndex = 6
        Me.Label40.Text = "label3"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(3, 3)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(576, 1)
        Me.Label41.TabIndex = 5
        Me.Label41.Text = "label1"
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.Panel17)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel16.Size = New System.Drawing.Size(579, 24)
        Me.Panel16.TabIndex = 8
        '
        'Panel17
        '
        Me.Panel17.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Button
        Me.Panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel17.Controls.Add(Me.plnDenominatorExclusionSearch)
        Me.Panel17.Controls.Add(Me.Label42)
        Me.Panel17.Controls.Add(Me.Label43)
        Me.Panel17.Controls.Add(Me.Label44)
        Me.Panel17.Controls.Add(Me.Label45)
        Me.Panel17.Controls.Add(Me.Label46)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel17.Location = New System.Drawing.Point(3, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(576, 24)
        Me.Panel17.TabIndex = 4
        '
        'plnDenominatorExclusionSearch
        '
        Me.plnDenominatorExclusionSearch.BackColor = System.Drawing.Color.Transparent
        Me.plnDenominatorExclusionSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.plnDenominatorExclusionSearch.Location = New System.Drawing.Point(289, 1)
        Me.plnDenominatorExclusionSearch.Name = "plnDenominatorExclusionSearch"
        Me.plnDenominatorExclusionSearch.Size = New System.Drawing.Size(286, 22)
        Me.plnDenominatorExclusionSearch.TabIndex = 13
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Location = New System.Drawing.Point(1, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(240, 22)
        Me.Label42.TabIndex = 12
        Me.Label42.Text = "Denominator Exclusions :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(1, 23)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(574, 1)
        Me.Label43.TabIndex = 9
        Me.Label43.Text = "label2"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 23)
        Me.Label44.TabIndex = 8
        Me.Label44.Text = "label4"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(575, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 23)
        Me.Label45.TabIndex = 7
        Me.Label45.Text = "label3"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(576, 1)
        Me.Label46.TabIndex = 6
        Me.Label46.Text = "label1"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(0, 642)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(1264, 3)
        Me.Splitter3.TabIndex = 115
        Me.Splitter3.TabStop = False
        '
        'ImgFlag
        '
        Me.ImgFlag.ImageStream = CType(resources.GetObject("ImgFlag.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgFlag.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgFlag.Images.SetKeyName(0, "Information.png")
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frm_CQM_Detail_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1264, 929)
        Me.Controls.Add(Me.pnlExcl_Excp)
        Me.Controls.Add(Me.Splitter3)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_CQM_Detail_Report"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_MU_Detail.ResumeLayout(False)
        Me.ts_MU_Detail.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.C1Denominator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlRight.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlLabs.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftMain.ResumeLayout(False)
        CType(Me.C1Numerator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        CType(Me.C1CQMDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1CQMDetail1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.pnlMeasurementPeriod.ResumeLayout(False)
        Me.pnlMeasurementPeriod.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlExcl_Excp.ResumeLayout(False)
        Me.pnlExcp.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        CType(Me.C1DenominatorExceptions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.pnlExcl.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        CType(Me.C1DenominatorExclusions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel16.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pnlLabs As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pnlLeftMain As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents ts_MU_Detail As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lblReportingYear As System.Windows.Forms.Label
    Friend WithEvents lblNPI As System.Windows.Forms.Label
    Friend WithEvents lblProviderName As System.Windows.Forms.Label
    Friend WithEvents lbl_TaxIDValue As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlMeasurementPeriod As System.Windows.Forms.Panel
    Friend WithEvents dtpicEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpicStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblReportName As System.Windows.Forms.Label
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents C1Numerator As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Denominator As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlExcl_Excp As System.Windows.Forms.Panel
    Friend WithEvents pnlExcp As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents C1DenominatorExceptions As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents plnDenominatorExceptionsSearch As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlExcl As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents C1DenominatorExclusions As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents plnDenominatorExclusionSearch As System.Windows.Forms.Panel
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents ts_btndetail As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents C1CQMDetail1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1CQMDetail As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lnkhelp As System.Windows.Forms.LinkLabel
    Friend WithEvents ImgFlag As System.Windows.Forms.ImageList
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
