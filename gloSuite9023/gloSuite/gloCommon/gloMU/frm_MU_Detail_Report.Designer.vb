<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MU_Detail_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MU_Detail_Report))
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
        Me.lblNotSatisfied = New System.Windows.Forms.Label()
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lblReportName = New System.Windows.Forms.Label()
        Me.lblReportingYear = New System.Windows.Forms.Label()
        Me.lblNPI = New System.Windows.Forms.Label()
        Me.lblProviderName = New System.Windows.Forms.Label()
        Me.lbl_TaxIDValue = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlMeasurementPeriod = New System.Windows.Forms.Panel()
        Me.dtpicEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpicStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.chk_FirstYear = New System.Windows.Forms.CheckBox()
        Me.lbl_TaxID = New System.Windows.Forms.Label()
        Me.lbl_PNPI = New System.Windows.Forms.Label()
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
        Me.Panel2.SuspendLayout()
        Me.pnlMeasurementPeriod.SuspendLayout()
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
        Me.Label5.Location = New System.Drawing.Point(4, 351)
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
        Me.Label7.Size = New System.Drawing.Size(1, 348)
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
        Me.Label6.Size = New System.Drawing.Size(1, 348)
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
        Me.pnlToolStrip.Size = New System.Drawing.Size(1101, 54)
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
        Me.ts_MU_Detail.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.ts_MU_Detail.Location = New System.Drawing.Point(0, 0)
        Me.ts_MU_Detail.Name = "ts_MU_Detail"
        Me.ts_MU_Detail.Size = New System.Drawing.Size(1101, 53)
        Me.ts_MU_Detail.TabIndex = 1
        Me.ts_MU_Detail.Text = "ToolStrip1"
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
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(2, "Current.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Yesterdays.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Last Week.ico")
        Me.imgTreeView.Images.SetKeyName(5, "LastMonth.ico")
        Me.imgTreeView.Images.SetKeyName(6, "Olders.ico")
        Me.imgTreeView.Images.SetKeyName(7, "Outstanding orders.ico")
        Me.imgTreeView.Images.SetKeyName(8, "Add Data.ico")
        Me.imgTreeView.Images.SetKeyName(9, "Unfinished Exam.ico")
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
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(519, 352)
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
        Me.C1Denominator.Size = New System.Drawing.Size(514, 347)
        Me.C1Denominator.StyleInfo = resources.GetString("C1Denominator.StyleInfo")
        Me.C1Denominator.TabIndex = 10
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(1, 348)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(514, 1)
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
        Me.Label13.Size = New System.Drawing.Size(1, 348)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(515, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 348)
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
        Me.Label19.Size = New System.Drawing.Size(516, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label1"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlRight)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlLeft)
        Me.pnlMain.Controls.Add(Me.pnl)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1101, 487)
        Me.pnlMain.TabIndex = 13
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.Panel4)
        Me.pnlRight.Controls.Add(Me.Panel3)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(582, 108)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(519, 379)
        Me.pnlRight.TabIndex = 113
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlLabs)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(519, 27)
        Me.Panel3.TabIndex = 9
        '
        'pnlLabs
        '
        Me.pnlLabs.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Button
        Me.pnlLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLabs.Controls.Add(Me.Panel8)
        Me.pnlLabs.Controls.Add(Me.lblNotSatisfied)
        Me.pnlLabs.Controls.Add(Me.Label14)
        Me.pnlLabs.Controls.Add(Me.Label15)
        Me.pnlLabs.Controls.Add(Me.Label16)
        Me.pnlLabs.Controls.Add(Me.Label17)
        Me.pnlLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabs.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabs.Name = "pnlLabs"
        Me.pnlLabs.Size = New System.Drawing.Size(516, 24)
        Me.pnlLabs.TabIndex = 4
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel8.Location = New System.Drawing.Point(243, 1)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(272, 22)
        Me.Panel8.TabIndex = 50
        '
        'lblNotSatisfied
        '
        Me.lblNotSatisfied.BackColor = System.Drawing.Color.Transparent
        Me.lblNotSatisfied.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblNotSatisfied.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotSatisfied.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNotSatisfied.Location = New System.Drawing.Point(1, 1)
        Me.lblNotSatisfied.Name = "lblNotSatisfied"
        Me.lblNotSatisfied.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.lblNotSatisfied.Size = New System.Drawing.Size(375, 22)
        Me.lblNotSatisfied.TabIndex = 12
        Me.lblNotSatisfied.Text = "Unique patients seen who do NOT meet objective criteria :"
        Me.lblNotSatisfied.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(514, 1)
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
        Me.Label16.Location = New System.Drawing.Point(515, 1)
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
        Me.Label17.Size = New System.Drawing.Size(516, 1)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(579, 108)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 379)
        Me.Splitter1.TabIndex = 114
        Me.Splitter1.TabStop = False
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.pnlLeftMain)
        Me.pnlLeft.Controls.Add(Me.Panel5)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 108)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(579, 379)
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
        Me.pnlLeftMain.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlLeftMain.Size = New System.Drawing.Size(579, 355)
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
        Me.C1Numerator.Size = New System.Drawing.Size(574, 347)
        Me.C1Numerator.StyleInfo = resources.GetString("C1Numerator.StyleInfo")
        Me.C1Numerator.TabIndex = 9
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
        Me.pnl.Controls.Add(Me.lbl_TaxID)
        Me.pnl.Controls.Add(Me.lbl_PNPI)
        Me.pnl.Controls.Add(Me.Panel2)
        Me.pnl.Controls.Add(Me.lblReportName)
        Me.pnl.Controls.Add(Me.lblReportingYear)
        Me.pnl.Controls.Add(Me.lblNPI)
        Me.pnl.Controls.Add(Me.lblProviderName)
        Me.pnl.Controls.Add(Me.lbl_TaxIDValue)
        Me.pnl.Controls.Add(Me.Label1)
        Me.pnl.Controls.Add(Me.Label2)
        Me.pnl.Controls.Add(Me.Label3)
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.Label9)
        Me.pnl.Controls.Add(Me.Label10)
        Me.pnl.Controls.Add(Me.pnlMeasurementPeriod)
        Me.pnl.Controls.Add(Me.Label28)
        Me.pnl.Controls.Add(Me.Label29)
        Me.pnl.Controls.Add(Me.chk_FirstYear)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl.Location = New System.Drawing.Point(0, 0)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl.Size = New System.Drawing.Size(1101, 108)
        Me.pnl.TabIndex = 111
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label30)
        Me.Panel2.Controls.Add(Me.Label33)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1093, 24)
        Me.Panel2.TabIndex = 127
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label30.Location = New System.Drawing.Point(0, 23)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1093, 1)
        Me.Label30.TabIndex = 126
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(7, 5)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(185, 14)
        Me.Label33.TabIndex = 125
        Me.Label33.Text = "List of Patients in Numerator"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReportName
        '
        Me.lblReportName.AutoSize = True
        Me.lblReportName.BackColor = System.Drawing.Color.Transparent
        Me.lblReportName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReportName.Location = New System.Drawing.Point(343, 35)
        Me.lblReportName.Name = "lblReportName"
        Me.lblReportName.Size = New System.Drawing.Size(0, 14)
        Me.lblReportName.TabIndex = 126
        '
        'lblReportingYear
        '
        Me.lblReportingYear.AutoSize = True
        Me.lblReportingYear.BackColor = System.Drawing.Color.Transparent
        Me.lblReportingYear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportingYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblReportingYear.Location = New System.Drawing.Point(615, 57)
        Me.lblReportingYear.Name = "lblReportingYear"
        Me.lblReportingYear.Size = New System.Drawing.Size(0, 14)
        Me.lblReportingYear.TabIndex = 124
        '
        'lblNPI
        '
        Me.lblNPI.AutoSize = True
        Me.lblNPI.BackColor = System.Drawing.Color.Transparent
        Me.lblNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNPI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblNPI.Location = New System.Drawing.Point(115, 57)
        Me.lblNPI.Name = "lblNPI"
        Me.lblNPI.Size = New System.Drawing.Size(0, 14)
        Me.lblNPI.TabIndex = 123
        '
        'lblProviderName
        '
        Me.lblProviderName.AutoSize = True
        Me.lblProviderName.BackColor = System.Drawing.Color.Transparent
        Me.lblProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblProviderName.Location = New System.Drawing.Point(115, 35)
        Me.lblProviderName.Name = "lblProviderName"
        Me.lblProviderName.Size = New System.Drawing.Size(0, 14)
        Me.lblProviderName.TabIndex = 122
        '
        'lbl_TaxIDValue
        '
        Me.lbl_TaxIDValue.AutoSize = True
        Me.lbl_TaxIDValue.BackColor = System.Drawing.Color.Transparent
        Me.lbl_TaxIDValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TaxIDValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TaxIDValue.Location = New System.Drawing.Point(115, 79)
        Me.lbl_TaxIDValue.Name = "lbl_TaxIDValue"
        Me.lbl_TaxIDValue.Size = New System.Drawing.Size(0, 14)
        Me.lbl_TaxIDValue.TabIndex = 120
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(255, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 14)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "Report Name :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(1097, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 100)
        Me.Label2.TabIndex = 117
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 100)
        Me.Label3.TabIndex = 116
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(3, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1095, 1)
        Me.Label4.TabIndex = 113
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1095, 1)
        Me.Label9.TabIndex = 112
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(895, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(186, 14)
        Me.Label10.TabIndex = 111
        Me.Label10.Text = "Reporting Period In Progress"
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
        Me.pnlMeasurementPeriod.Location = New System.Drawing.Point(484, 30)
        Me.pnlMeasurementPeriod.Name = "pnlMeasurementPeriod"
        Me.pnlMeasurementPeriod.Size = New System.Drawing.Size(409, 25)
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
        Me.dtpicEndDate.Location = New System.Drawing.Point(309, 0)
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
        Me.dtpicStartDate.Location = New System.Drawing.Point(178, 0)
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
        Me.Label21.Location = New System.Drawing.Point(4, 4)
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
        Me.Label26.Location = New System.Drawing.Point(135, 4)
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
        Me.Label27.Location = New System.Drawing.Point(281, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(26, 14)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "To "
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(517, 57)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(97, 14)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "Reporting Year :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(55, 35)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(59, 14)
        Me.Label29.TabIndex = 10
        Me.Label29.Text = "Provider :"
        '
        'chk_FirstYear
        '
        Me.chk_FirstYear.AutoSize = True
        Me.chk_FirstYear.BackColor = System.Drawing.Color.Transparent
        Me.chk_FirstYear.Enabled = False
        Me.chk_FirstYear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_FirstYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chk_FirstYear.Location = New System.Drawing.Point(814, 55)
        Me.chk_FirstYear.Name = "chk_FirstYear"
        Me.chk_FirstYear.Size = New System.Drawing.Size(77, 18)
        Me.chk_FirstYear.TabIndex = 3
        Me.chk_FirstYear.Text = "First Year"
        Me.chk_FirstYear.UseVisualStyleBackColor = False
        '
        'lbl_TaxID
        '
        Me.lbl_TaxID.AutoSize = True
        Me.lbl_TaxID.BackColor = System.Drawing.Color.Transparent
        Me.lbl_TaxID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TaxID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TaxID.Location = New System.Drawing.Point(15, 79)
        Me.lbl_TaxID.Name = "lbl_TaxID"
        Me.lbl_TaxID.Size = New System.Drawing.Size(99, 14)
        Me.lbl_TaxID.TabIndex = 129
        Me.lbl_TaxID.Text = "Provider Tax ID :"
        '
        'lbl_PNPI
        '
        Me.lbl_PNPI.AutoSize = True
        Me.lbl_PNPI.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PNPI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_PNPI.Location = New System.Drawing.Point(32, 57)
        Me.lbl_PNPI.Name = "lbl_PNPI"
        Me.lbl_PNPI.Size = New System.Drawing.Size(82, 14)
        Me.lbl_PNPI.TabIndex = 128
        Me.lbl_PNPI.Text = "Provider NPI :"
        '
        'frm_MU_Detail_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1101, 541)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_MU_Detail_Report"
        Me.Text = "MU Detail Report"
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
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlMeasurementPeriod.ResumeLayout(False)
        Me.pnlMeasurementPeriod.PerformLayout()
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
    Private WithEvents lblNotSatisfied As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pnlLeftMain As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents C1Denominator As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ts_MU_Detail As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents C1Numerator As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lblReportingYear As System.Windows.Forms.Label
    Friend WithEvents lblNPI As System.Windows.Forms.Label
    Friend WithEvents lblProviderName As System.Windows.Forms.Label
    Friend WithEvents lbl_TaxIDValue As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlMeasurementPeriod As System.Windows.Forms.Panel
    Friend WithEvents dtpicEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpicStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents chk_FirstYear As System.Windows.Forms.CheckBox
    Friend WithEvents lblReportName As System.Windows.Forms.Label
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lbl_TaxID As System.Windows.Forms.Label
    Friend WithEvents lbl_PNPI As System.Windows.Forms.Label
End Class
