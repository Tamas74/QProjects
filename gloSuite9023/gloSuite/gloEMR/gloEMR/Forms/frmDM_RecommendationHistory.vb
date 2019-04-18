Imports gloUserControlLibrary
Imports gloEMR.gloStream.DiseaseManagement.Supporting
Imports gloemr.gloStream.DiseaseManagement
Public Class frmDM_RecommendationHistory
    Inherits System.Windows.Forms.Form
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

    'Public oMainForm As MainMenu

    Dim _RecommendationId As Int64
    Dim _CriteriaID As Int64
    Dim _nPatientId As Int64
    Friend WithEvents lblAnnouncedDate As System.Windows.Forms.Label
    Friend WithEvents lblRecommendationName As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal RecommendationId As Int64, ByVal CreteriaID As Int64, ByVal PatientID As Int64)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _RecommendationId = RecommendationId
        _CriteriaID = CreteriaID
        _nPatientId = PatientID

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
    Friend WithEvents c1RecommendationHistory As C1.Win.C1FlexGrid.C1FlexGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_RecommendationHistory))
        Me.c1RecommendationHistory = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.lblAnnouncedDate = New System.Windows.Forms.Label()
        Me.lblRecommendationName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.c1RecommendationHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'c1RecommendationHistory
        '
        Me.c1RecommendationHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1RecommendationHistory.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1RecommendationHistory.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.c1RecommendationHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1RecommendationHistory.ExtendLastCol = True
        Me.c1RecommendationHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1RecommendationHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1RecommendationHistory.Location = New System.Drawing.Point(3, 2)
        Me.c1RecommendationHistory.Name = "c1RecommendationHistory"
        Me.c1RecommendationHistory.Rows.DefaultSize = 19
        Me.c1RecommendationHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1RecommendationHistory.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1RecommendationHistory.ShowCellLabels = True
        Me.c1RecommendationHistory.Size = New System.Drawing.Size(928, 529)
        Me.c1RecommendationHistory.StyleInfo = resources.GetString("c1RecommendationHistory.StyleInfo")
        Me.c1RecommendationHistory.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(935, 53)
        Me.pnlToolStrip.TabIndex = 0
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(935, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.c1RecommendationHistory)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 83)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(935, 534)
        Me.pnl_Base.TabIndex = 2
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 530)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(927, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 2)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 529)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(931, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 529)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(929, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnlSearch
        '
        Me.pnlSearch.Controls.Add(Me.pnlTopRight)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 53)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(935, 30)
        Me.pnlSearch.TabIndex = 3
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.lblAnnouncedDate)
        Me.pnlTopRight.Controls.Add(Me.lblRecommendationName)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label5)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.Label7)
        Me.pnlTopRight.Controls.Add(Me.Label8)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(929, 24)
        Me.pnlTopRight.TabIndex = 1
        '
        'lblAnnouncedDate
        '
        Me.lblAnnouncedDate.AutoSize = True
        Me.lblAnnouncedDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblAnnouncedDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnnouncedDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAnnouncedDate.Location = New System.Drawing.Point(721, 5)
        Me.lblAnnouncedDate.Name = "lblAnnouncedDate"
        Me.lblAnnouncedDate.Size = New System.Drawing.Size(64, 14)
        Me.lblAnnouncedDate.TabIndex = 13
        Me.lblAnnouncedDate.Text = "DateTime"
        '
        'lblRecommendationName
        '
        Me.lblRecommendationName.AutoEllipsis = True
        Me.lblRecommendationName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblRecommendationName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecommendationName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblRecommendationName.Location = New System.Drawing.Point(174, 5)
        Me.lblRecommendationName.Name = "lblRecommendationName"
        Me.lblRecommendationName.Size = New System.Drawing.Size(450, 14)
        Me.lblRecommendationName.TabIndex = 12
        Me.lblRecommendationName.Text = "Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(9, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 14)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Recommendation Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(630, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Announced :"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(927, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 23)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(928, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(929, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'frmDM_RecommendationHistory
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(935, 617)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDM_RecommendationHistory"
        Me.Text = "Recommendation History"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.c1RecommendationHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDM_DiseaseView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1RecommendationHistory)

        Try
            Fill_RecommendationHistory()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub DesignGrid()

        With c1RecommendationHistory
          
            For i As Int16 = 0 To .Cols.Count - 1
                .Cols(i).AllowEditing = False
            Next

            .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

        End With
        c1RecommendationHistory.Cols(0).Visible = False
        c1RecommendationHistory.Cols(1).Visible = False
        c1RecommendationHistory.Cols(2).Visible = False

        c1RecommendationHistory.Cols(3).Format = "MM/dd/yyyy hh:mm tt"
        c1RecommendationHistory.Cols(3).Caption = "Date"
        c1RecommendationHistory.Cols(3).Width = 148


        c1RecommendationHistory.Cols(4).Caption = "Satisfied Date"
        c1RecommendationHistory.Cols(4).Format = "MM/dd/yyyy"


        c1RecommendationHistory.Cols(5).Width = 130
        c1RecommendationHistory.Cols(5).Caption = "Status"
        c1RecommendationHistory.Cols(6).Caption = "Note"
        c1RecommendationHistory.Cols(6).Width = 320
        c1RecommendationHistory.Cols(6).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftTop

        c1RecommendationHistory.Cols(7).Caption = "User"
        c1RecommendationHistory.Cols(8).Caption = "Note Date"
        c1RecommendationHistory.Cols(8).Format = "MM/dd/yyyy hh:mm tt"
        c1RecommendationHistory.Cols(8).Width = 148

        c1RecommendationHistory.Cols(9).Visible = False
        c1RecommendationHistory.Cols(10).Visible = False
        c1RecommendationHistory.Cols(11).Visible = False
        '' c1RecommendationHistory.Cols(3).Visible = False
        With c1RecommendationHistory
            For _rowIndex As Int16 = 1 To c1RecommendationHistory.Rows.Count - 1
                .Rows(_rowIndex).Height = 40
            Next

            If c1RecommendationHistory.Rows.Count > 1 Then

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                '  cStyle = .Styles.Add("WordWrap")
                Try
                    If (.Styles.Contains("WordWrap")) Then
                        cStyle = .Styles("WordWrap")
                    Else
                        cStyle = .Styles.Add("WordWrap")

                    End If
                Catch ex As Exception
                    cStyle = .Styles.Add("WordWrap")

                End Try
                cStyle.WordWrap = True
                cStyle.Trimming = StringTrimming.EllipsisCharacter

                Dim rgOperator As C1.Win.C1FlexGrid.CellRange = Nothing
                rgOperator = .GetCellRange(1, .Cols(6).Index, .Rows.Count - 1, .Cols(6).Index)

                rgOperator.Style = cStyle

            End If

        End With

    End Sub

    Private Sub Fill_RecommendationHistory()
        Dim oDM As New DiseaseManagement
        Dim _dtRecommendationHistory As DataTable
        Try

            _dtRecommendationHistory = oDM.GetRecommendationsHistory(_RecommendationId, _CriteriaID, _nPatientId)
            'c1RecommendationHistory.Clear()
            c1RecommendationHistory.DataSource = Nothing
            '_dtRecommendationHistory = <GetMethod for history returning table and accepting _RecommendationId>
            c1RecommendationHistory.BeginUpdate()
            c1RecommendationHistory.DataSource = _dtRecommendationHistory.DefaultView
            c1RecommendationHistory.EndUpdate()
            If _dtRecommendationHistory IsNot Nothing AndAlso _dtRecommendationHistory.Rows.Count > 0 Then
                lblRecommendationName.Text = _dtRecommendationHistory.Rows(0)("RecommendationName").ToString()
                lblAnnouncedDate.Text = _dtRecommendationHistory.Rows(0)("Announced").ToString()
            Else
                lblRecommendationName.Text = ""
                lblAnnouncedDate.Text = ""

            End If
            DesignGrid()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.View, "'" & lblRecommendationName.Text & "' Viewed Recommendation history", _nPatientId, _CriteriaID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ViewRecommendationRule, gloAuditTrail.ActivityCategory.ViewRecommendationRule, gloAuditTrail.ActivityType.View, "'" & lblRecommendationName.Text & "' unsuccessfully Viewed Recommendation history", _nPatientId, _CriteriaID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
        Finally
            If oDM IsNot Nothing Then
                oDM.Dispose()
                oDM = Nothing
            End If
        End Try


    End Sub

    

    Private Sub RefreshGrid()
        Try


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub FormClose()
        Me.Close()
    End Sub



    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        FormClose()
    End Sub

    Private Sub c1RecommendationHistory_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1RecommendationHistory.MouseMove
        Try

            If c1RecommendationHistory.HitTest(e.X, e.Y).Column = 6 Then
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
            End If

        Catch ex As Exception
            'Blank
        End Try
    End Sub
End Class
