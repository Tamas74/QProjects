Imports System.Data.SqlClient
Imports gloOffice

Public Class frmRecoverExam
    Inherits System.Windows.Forms.Form
    Private intCatID As Long
    Private strCatDesc As String
    Private strcatype As String
    Private strCode As String
    '' To Allow Sreach
    Private _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String

    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnViewExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDeleteExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlRecoverExam As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Dim strsortorder As String


    Friend WithEvents C1dgPatientDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Private nPatientId As Long = 1
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblto As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkenbdate As System.Windows.Forms.CheckBox
    Private WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents lblFrom As System.Windows.Forms.Label



    Public ReadOnly Property DsRecoverExam() As DataTable
        Get
            Return DRecoverExamTable
        End Get
    End Property



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
                If (IsNothing(dtpToDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpToDate)
                    Catch ex As Exception

                    End Try


                    dtpToDate.Dispose()
                    dtpToDate = Nothing
                End If
            Catch
            End Try

            Try
                If (IsNothing(dtpFromDate) = False) Then
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpFromDate)
                    Catch ex As Exception

                    End Try


                    dtpFromDate.Dispose()
                    dtpFromDate = Nothing
                End If
            Catch
            End Try

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
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecoverExam))
        Me.pnlTopRight = New System.Windows.Forms.Panel
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker
        Me.lblto = New System.Windows.Forms.Label
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker
        Me.chkenbdate = New System.Windows.Forms.CheckBox
        Me.lblFrom = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnViewExam = New System.Windows.Forms.ToolStripButton
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDeleteExam = New System.Windows.Forms.ToolStripButton
        Me.ts_btnSelectAll = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlRecoverExam = New System.Windows.Forms.Panel
        Me.C1dgPatientDetails = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlTopRight.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlRecoverExam.SuspendLayout()
        CType(Me.C1dgPatientDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.chkSelectAll)
        Me.pnlTopRight.Controls.Add(Me.dtpToDate)
        Me.pnlTopRight.Controls.Add(Me.lblto)
        Me.pnlTopRight.Controls.Add(Me.dtpFromDate)
        Me.pnlTopRight.Controls.Add(Me.chkenbdate)
        Me.pnlTopRight.Controls.Add(Me.lblFrom)
        Me.pnlTopRight.Controls.Add(Me.btnClear)
        Me.pnlTopRight.Controls.Add(Me.Label6)
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(1242, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelectAll.Location = New System.Drawing.Point(690, 2)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(79, 19)
        Me.chkSelectAll.TabIndex = 49
        Me.chkSelectAll.Text = "Select All "
        Me.chkSelectAll.UseVisualStyleBackColor = True
        Me.chkSelectAll.Visible = False
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.Enabled = False
        Me.dtpToDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(572, 2)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(86, 21)
        Me.dtpToDate.TabIndex = 47
        '
        'lblto
        '
        Me.lblto.BackColor = System.Drawing.Color.Transparent
        Me.lblto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblto.Location = New System.Drawing.Point(531, 1)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(37, 20)
        Me.lblto.TabIndex = 44
        Me.lblto.Text = "To :"
        Me.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFromDate.Enabled = False
        Me.dtpFromDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(443, 2)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(86, 21)
        Me.dtpFromDate.TabIndex = 45
        '
        'chkenbdate
        '
        Me.chkenbdate.AutoSize = True
        Me.chkenbdate.Location = New System.Drawing.Point(422, 6)
        Me.chkenbdate.Name = "chkenbdate"
        Me.chkenbdate.Size = New System.Drawing.Size(15, 14)
        Me.chkenbdate.TabIndex = 48
        Me.chkenbdate.UseVisualStyleBackColor = True
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblFrom.Location = New System.Drawing.Point(366, 2)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(52, 19)
        Me.lblFrom.TabIndex = 46
        Me.lblFrom.Text = "From :"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(296, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(292, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label6.Size = New System.Drawing.Size(4, 20)
        Me.Label6.TabIndex = 43
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(69, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(223, 22)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblSearch.Size = New System.Drawing.Size(68, 20)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1240, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1241, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Image = CType(resources.GetObject("Label4.Image"), System.Drawing.Image)
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1242, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1248, 53)
        Me.pnlToolStrip.TabIndex = 2
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = CType(resources.GetObject("ts_ViewButtons.BackgroundImage"), System.Drawing.Image)
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnViewExam, Me.ts_btnModify, Me.ts_btnDeleteExam, Me.ts_btnSelectAll, Me.ts_btnClose})
        Me.ts_ViewButtons.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1248, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.TabStop = True
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnViewExam
        '
        Me.ts_btnViewExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnViewExam.Image = CType(resources.GetObject("ts_btnViewExam.Image"), System.Drawing.Image)
        Me.ts_btnViewExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnViewExam.Name = "ts_btnViewExam"
        Me.ts_btnViewExam.Size = New System.Drawing.Size(76, 50)
        Me.ts_btnViewExam.Tag = "Add"
        Me.ts_btnViewExam.Text = "View &Exam"
        Me.ts_btnViewExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnModify.Visible = False
        '
        'ts_btnDeleteExam
        '
        Me.ts_btnDeleteExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnDeleteExam.Image = CType(resources.GetObject("ts_btnDeleteExam.Image"), System.Drawing.Image)
        Me.ts_btnDeleteExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDeleteExam.Name = "ts_btnDeleteExam"
        Me.ts_btnDeleteExam.Size = New System.Drawing.Size(92, 50)
        Me.ts_btnDeleteExam.Tag = "Delete"
        Me.ts_btnDeleteExam.Text = "&Delete Exams"
        Me.ts_btnDeleteExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnSelectAll
        '
        Me.ts_btnSelectAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSelectAll.Image = CType(resources.GetObject("ts_btnSelectAll.Image"), System.Drawing.Image)
        Me.ts_btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSelectAll.Name = "ts_btnSelectAll"
        Me.ts_btnSelectAll.Size = New System.Drawing.Size(67, 50)
        Me.ts_btnSelectAll.Tag = "Select All"
        Me.ts_btnSelectAll.Text = "&Select All"
        Me.ts_btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlRecoverExam
        '
        Me.pnlRecoverExam.Controls.Add(Me.C1dgPatientDetails)
        Me.pnlRecoverExam.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlRecoverExam.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlRecoverExam.Controls.Add(Me.lbl_RightBrd)
        Me.pnlRecoverExam.Controls.Add(Me.lbl_TopBrd)
        Me.pnlRecoverExam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRecoverExam.Location = New System.Drawing.Point(0, 83)
        Me.pnlRecoverExam.Name = "pnlRecoverExam"
        Me.pnlRecoverExam.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlRecoverExam.Size = New System.Drawing.Size(1248, 548)
        Me.pnlRecoverExam.TabIndex = 1
        '
        'C1dgPatientDetails
        '
        Me.C1dgPatientDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1dgPatientDetails.AutoResize = False
        Me.C1dgPatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1dgPatientDetails.CausesValidation = False
        Me.C1dgPatientDetails.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.C1dgPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1dgPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1dgPatientDetails.Location = New System.Drawing.Point(4, 2)
        Me.C1dgPatientDetails.Name = "C1dgPatientDetails"
        Me.C1dgPatientDetails.Rows.DefaultSize = 19
        Me.C1dgPatientDetails.Size = New System.Drawing.Size(1240, 542)
        Me.C1dgPatientDetails.StyleInfo = resources.GetString("C1dgPatientDetails.StyleInfo")
        Me.C1dgPatientDetails.TabIndex = 9
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 544)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(1240, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 2)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 543)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(1244, 2)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 543)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(1242, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1248, 30)
        Me.Panel2.TabIndex = 0
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmRecoverExam
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1248, 631)
        Me.Controls.Add(Me.pnlRecoverExam)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRecoverExam"
        Me.ShowInTaskbar = False
        Me.Text = "Recover Exam"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlRecoverExam.ResumeLayout(False)
        CType(Me.C1dgPatientDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub


    Dim COL_DOS As Int32 = 3
    Dim COL_ExamName As Int32 = 4
    Dim COL_TemplateName As Int32 = 5
    Dim COL_PatientName As Int32 = 2
    Dim COL_PatientCode As Int32 = 1
    Dim COL_ProviderName As Int32 = 6
    Dim COL_Finished As Int32 = 7
    Dim COL_UpdatedDate As Int32 = 8
    Dim COL_PatientExamsAuditID As Int32 = 9
    Dim COL_nExamID As Int32 = 10
    Dim COL_nVisitID As Int32 = 11
    Dim COL_nPatientID As Int32 = 12
    Dim COL_MachineName As Int32 = 13
    Dim COL_nReviewerId As Int32 = 14
    Dim COL_dtReviewDate As Int32 = 15
    Dim COL_nProviderID As Int32 = 16
    Dim COL_SELECT As Int32 = 0
    Dim COL_COUNT As Int32 = 17

    Public Delegate Sub SearchFilterChangedHandler(ByVal isChanged As Boolean)
    Public Event SearchFilterChanged As SearchFilterChangedHandler
    Dim dvFilter As DataView = Nothing
    Private DRecoverExamTable As DataTable = Nothing


    Private Sub frmRecoverExam_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(dvFilter) Then
            dvFilter.Dispose()
            dvFilter = Nothing
        End If
        If Not IsNothing(DRecoverExamTable) Then
            DRecoverExamTable.Dispose()
            DRecoverExamTable = Nothing
        End If
    End Sub

    Private Sub frmVWCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1dgPatientDetails)

        Try

            '14-Nov-14 Aniket: Bug #76017: gloEMR > Tools > Recover Exam >Taking too much time to open 'Recover Exam'
            'Load only current days exams

            chkenbdate.Checked = True
            Fill_RecoverExam()

            AddHandler SearchFilterChanged, AddressOf SearchFilter_Changed

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Fill_RecoverExam()

        Dim dtFiltered As DataTable = Nothing
        Dim _width As Integer = 0
        Dim dvFilter As DataView = Nothing
        Dim strFilter As String = ""
        Dim dtPatientDetails As DataTable = Nothing
        Try

            dtPatientDetails = FillPatientExam()
            If Not IsNothing(DRecoverExamTable) Then
                DRecoverExamTable = Nothing
            End If
            DRecoverExamTable = dtPatientDetails
            'dvFilter = New DataView()
            dvFilter = dtPatientDetails.DefaultView

            If txtSearch.Text.Trim() <> "" Then
                strFilter = " [Exam Name] Like '%" & txtSearch.Text.Trim() & "%' or  [Template Name] Like '%" & txtSearch.Text.Trim() & "%' or [Provider Name] Like '%" & txtSearch.Text.Trim() & "%' or [Patient Name] Like '%" & txtSearch.Text.Trim() & "%' or [Patient Code] Like '%" & txtSearch.Text.Trim() & "%'"
            End If

            If (chkenbdate.Visible = True) And (chkenbdate.Checked = True) Then
                If strFilter <> "" Then

                    If strFilter.Trim() <> "" Then
                        dvFilter.RowFilter = strFilter
                        dtPatientDetails = dvFilter.ToTable
                    End If

                    strFilter = "[Last Updated Date] >=  '" & dtpFromDate.Value.Date & " 12:00:00 AM" & "' AND [Last Updated Date] <= '" & dtpToDate.Value.Date & " 11:59:59 PM" & "' "
                Else
                    strFilter = "[Last Updated Date] >=  '" & dtpFromDate.Value.Date & " 12:00:00 AM" & "' AND [Last Updated Date] <= '" & dtpToDate.Value.Date & " 11:59:59 PM" & "' "
                End If
                dvFilter = dtPatientDetails.DefaultView
            End If

            If strFilter.Trim() <> "" Then
                dvFilter.RowFilter = strFilter
                dtFiltered = dvFilter.ToTable
            Else
                dtFiltered = dtPatientDetails
            End If

            C1dgPatientDetails.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            'C1dgPatientDetails.Clear()
            C1dgPatientDetails.DataSource = Nothing
            C1dgPatientDetails.Rows.Add(1)
            C1dgPatientDetails.Rows.Fixed = 1
            C1dgPatientDetails.Cols.Fixed = 0
            C1dgPatientDetails.Cols.Count = COL_COUNT
            C1dgPatientDetails.Rows.Count = 1

            _width = pnlRecoverExam.Width / 3

            'If _width < Convert.ToInt32(c1dgPatientDetailSize) And c1dgPatientDetailSize <> Nothing Then
            '    _width = Convert.ToInt32(c1dgPatientDetailSize)
            'End If

            C1dgPatientDetails.Cols(COL_ExamName).Caption = "Exam Name"
            C1dgPatientDetails.Cols(COL_TemplateName).Caption = "Template Name"
            C1dgPatientDetails.Cols(COL_UpdatedDate).Caption = "Last Updated Date"
            'C1dgPatientDetails.Cols("nExamID").Caption = "Exam Id"
            C1dgPatientDetails.Cols(COL_DOS).Caption = "DOS"
            C1dgPatientDetails.Cols(COL_PatientName).Caption = "Patient Name"
            C1dgPatientDetails.Cols(COL_PatientCode).Caption = "Patient Code"
            C1dgPatientDetails.Cols(COL_ProviderName).Caption = "Provider Name"
            C1dgPatientDetails.Cols(COL_Finished).Caption = "Finished"
            C1dgPatientDetails.Cols(COL_SELECT).Caption = "Select"

            If Not IsNothing(C1dgPatientDetails) Then
                DesignC1PatientDetailGrid()

                C1dgPatientDetails.SetData(0, COL_DOS, "DOS")
                C1dgPatientDetails.SetData(0, COL_ExamName, "Exam Name")
                C1dgPatientDetails.SetData(0, COL_TemplateName, "Template Name")
                C1dgPatientDetails.SetData(0, COL_PatientCode, "Patient Code")
                C1dgPatientDetails.SetData(0, COL_PatientName, "Patient Name")
                C1dgPatientDetails.SetData(0, COL_ProviderName, "Provider Name")
                C1dgPatientDetails.SetData(0, COL_Finished, "Finished")
                C1dgPatientDetails.SetData(0, COL_UpdatedDate, "Last Updated Date")
                C1dgPatientDetails.SetData(0, COL_PatientExamsAuditID, "PatientExamsAuditID")
                C1dgPatientDetails.SetData(0, COL_nExamID, "nExamID")
                C1dgPatientDetails.SetData(0, COL_nVisitID, "nVisitID")
                C1dgPatientDetails.SetData(0, COL_nPatientID, "nPatientID")
                C1dgPatientDetails.SetData(0, COL_MachineName, "Machine Name")
                C1dgPatientDetails.SetData(0, COL_nReviewerId, "nReviewerId")
                C1dgPatientDetails.SetData(0, COL_dtReviewDate, "dtReviewDate")
                C1dgPatientDetails.SetData(0, COL_nProviderID, "nProviderID")
                C1dgPatientDetails.SetData(0, COL_SELECT, "Select")

                C1dgPatientDetails.Cols(COL_SELECT).DataType = System.Type.GetType("System.Boolean")

                If Not IsNothing(dtFiltered) Then
                    If dtFiltered.Rows.Count > 0 Then
                        C1dgPatientDetails.Rows.Count = dtFiltered.Rows.Count + 1
                        'Dim rowCnt As Integer = dtPatientExamDetail.Rows.Count - 1
                        For i As Integer = 1 To dtFiltered.Rows.Count
                            C1dgPatientDetails.SetData(i, COL_DOS, Format(dtFiltered.Rows(i - 1)("DOS"), "MM/dd/yyyy"))
                            C1dgPatientDetails.SetData(i, COL_ExamName, dtFiltered.Rows(i - 1)("Exam Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_TemplateName, dtFiltered.Rows(i - 1)("Template Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_PatientCode, dtFiltered.Rows(i - 1)("Patient Code").ToString())
                            C1dgPatientDetails.SetData(i, COL_PatientName, dtFiltered.Rows(i - 1)("Patient Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_ProviderName, dtFiltered.Rows(i - 1)("Provider Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_Finished, dtFiltered.Rows(i - 1)("Finished").ToString())
                            C1dgPatientDetails.SetData(i, COL_UpdatedDate, dtFiltered.Rows(i - 1)("Last Updated Date").ToString())
                            C1dgPatientDetails.SetData(i, COL_PatientExamsAuditID, dtFiltered.Rows(i - 1)("PatientExamsAuditID").ToString())
                            C1dgPatientDetails.SetData(i, COL_nExamID, dtFiltered.Rows(i - 1)("nExamID").ToString())
                            C1dgPatientDetails.SetData(i, COL_nVisitID, dtFiltered.Rows(i - 1)("nVisitID").ToString())
                            C1dgPatientDetails.SetData(i, COL_nPatientID, dtFiltered.Rows(i - 1)("nPatientID").ToString())
                            C1dgPatientDetails.SetData(i, COL_MachineName, dtFiltered.Rows(i - 1)("Machine Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_nReviewerId, dtFiltered.Rows(i - 1)("nReviewerId").ToString())
                            C1dgPatientDetails.SetData(i, COL_dtReviewDate, dtFiltered.Rows(i - 1)("dtReviewDate").ToString())
                            C1dgPatientDetails.SetData(i, COL_nProviderID, dtFiltered.Rows(i - 1)("nProviderID").ToString())
                            C1dgPatientDetails.SetData(i, COL_nProviderID, dtFiltered.Rows(i - 1)("nProviderID").ToString())

                        Next
                    End If
                End If

                C1dgPatientDetails.Cols(COL_PatientExamsAuditID).Visible = False
                C1dgPatientDetails.Cols(COL_nExamID).Visible = False
                C1dgPatientDetails.Cols(COL_nVisitID).Visible = False
                C1dgPatientDetails.Cols(COL_nPatientID).Visible = False
                C1dgPatientDetails.Cols(COL_MachineName).Visible = False
                C1dgPatientDetails.Cols(COL_nReviewerId).Visible = False
                C1dgPatientDetails.Cols(COL_dtReviewDate).Visible = False
                C1dgPatientDetails.Cols(COL_nProviderID).Visible = False
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "Patient Recovered Exam viewed from DashBoard", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception

        Finally
            'If Not IsNothing(dvFilter) Then
            '    dvFilter.Dispose()
            '    dvFilter = Nothing
            'End If
            If Not IsNothing(dtPatientDetails) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            If Not IsNothing(dtFiltered) Then
                dtFiltered.Dispose()
                dtFiltered = Nothing
            End If

        End Try

    End Sub
    Public Function FillPatientExam() As DataTable
        Dim dtPatientExamDetail As DataTable = Nothing
        Dim oDB As gloStream.gloDataBase.gloDataBase = Nothing
        nPatientId = 0
        Try
            oDB = New gloStream.gloDataBase.gloDataBase()
            oDB.Connect(GetConnectionString)
            oDB.DBParameters.Clear()
            'oDB.DBParameters.Add("@StartDate", datFrom, ParameterDirection.Input, SqlDbType.DateTime)
            'oDB.DBParameters.Add("@EndDate", datTo, ParameterDirection.Input, SqlDbType.DateTime)
            oDB.DBParameters.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.VarChar)
            dtPatientExamDetail = oDB.ReadData("GetPatientExamFromAudit")

            If Not IsNothing(dtPatientExamDetail) Then
                Return dtPatientExamDetail
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        Finally
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        Return Nothing
    End Function



    Private Sub RefillRecoverExam()
        'Fill_RecoverExam()
        Dim dtFiltered As DataTable = Nothing
        Dim _width As Integer = 0

        Dim strFilter As String = ""
        Dim dtPatientDetails As DataTable = Nothing
        Try

            'dtPatientDetails = C1dgPatientDetails.DataSource

            'dvFilter = New DataView()
            'dtPatientDetails = New DataTable
            dtPatientDetails = DRecoverExamTable
            dvFilter = dtPatientDetails.DefaultView()


            If txtSearch.Text.Trim() <> "" Then
                strFilter = " [Exam Name] Like '%" & txtSearch.Text.Trim() & "%' or  [Template Name] Like '%" & txtSearch.Text.Trim() & "%' or [Provider Name] Like '%" & txtSearch.Text.Trim() & "%' or [Patient Name] Like '%" & txtSearch.Text.Trim() & "%' or [Patient Code] Like '%" & txtSearch.Text.Trim() & "%'"
            End If

            If (chkenbdate.Visible = True) And (chkenbdate.Checked = True) Then
                If strFilter <> "" Then

                    If strFilter.Trim() <> "" Then
                        dvFilter.RowFilter = strFilter
                        dtPatientDetails = dvFilter.ToTable
                    End If

                    strFilter = "[Last Updated Date] >=  '" & dtpFromDate.Value.Date & " 12:00:00 AM" & "' AND [Last Updated Date] <= '" & dtpToDate.Value.Date & " 11:59:59 PM" & "' "
                Else
                    strFilter = "[Last Updated Date] >=  '" & dtpFromDate.Value.Date & " 12:00:00 AM" & "' AND [Last Updated Date] <= '" & dtpToDate.Value.Date & " 11:59:59 PM" & "' "
                End If
                dvFilter = dtPatientDetails.DefaultView
                'strFilter = "Updated Date  Between '" & dtpFromDate.Value.Date & " 12:00:00 AM" & "' AND  '" & dtpToDate.Value.Date & " 11:59:59 PM" & "' "
            End If

            If strFilter.Trim() <> "" Then
                dvFilter.RowFilter = strFilter
                dtFiltered = dvFilter.ToTable
            Else
                dtFiltered = dtPatientDetails
            End If
            'C1dgPatientDetails.Clear()
            C1dgPatientDetails.DataSource = Nothing
            C1dgPatientDetails.Rows.Add(1)
            C1dgPatientDetails.Rows.Fixed = 1
            C1dgPatientDetails.Cols.Fixed = 0
            C1dgPatientDetails.Cols.Count = COL_COUNT
            C1dgPatientDetails.Rows.Count = 1
            '_width = pnlRecoverExam.Width / 3


            C1dgPatientDetails.Cols(COL_ExamName).Caption = "Exam Name"
            C1dgPatientDetails.Cols(COL_TemplateName).Caption = "Template Name"
            C1dgPatientDetails.Cols(COL_UpdatedDate).Caption = "Last Updated Date"
            'C1dgPatientDetails.Cols("nExamID").Caption = "Exam Id"
            C1dgPatientDetails.Cols(COL_DOS).Caption = "DOS"
            C1dgPatientDetails.Cols(COL_PatientName).Caption = "Patient Name"
            C1dgPatientDetails.Cols(COL_PatientCode).Caption = "Patient Code"
            C1dgPatientDetails.Cols(COL_ProviderName).Caption = "Provider Name"
            C1dgPatientDetails.Cols(COL_Finished).Caption = "Finished"
            C1dgPatientDetails.Cols(COL_SELECT).Caption = "Select"

            If Not IsNothing(C1dgPatientDetails) Then

                DesignC1PatientDetailGrid()

                C1dgPatientDetails.SetData(0, COL_DOS, "DOS")
                C1dgPatientDetails.SetData(0, COL_ExamName, "Exam Name")
                C1dgPatientDetails.SetData(0, COL_TemplateName, "Template Name")
                C1dgPatientDetails.SetData(0, COL_PatientCode, "Patient Code")
                C1dgPatientDetails.SetData(0, COL_PatientName, "Patient Name")
                C1dgPatientDetails.SetData(0, COL_ProviderName, "Provider Name")
                C1dgPatientDetails.SetData(0, COL_Finished, "Finished")
                C1dgPatientDetails.SetData(0, COL_UpdatedDate, "Last Updated Date")
                C1dgPatientDetails.SetData(0, COL_PatientExamsAuditID, "PatientExamsAuditID")
                C1dgPatientDetails.SetData(0, COL_nExamID, "nExamID")
                C1dgPatientDetails.SetData(0, COL_nVisitID, "nVisitID")
                C1dgPatientDetails.SetData(0, COL_nPatientID, "nPatientID")
                C1dgPatientDetails.SetData(0, COL_MachineName, "Machine Name")
                C1dgPatientDetails.SetData(0, COL_nReviewerId, "nReviewerId")
                C1dgPatientDetails.SetData(0, COL_dtReviewDate, "dtReviewDate")
                C1dgPatientDetails.SetData(0, COL_nProviderID, "nProviderID")
                C1dgPatientDetails.SetData(0, COL_SELECT, "Select")

                C1dgPatientDetails.Cols(COL_SELECT).DataType = System.Type.GetType("System.Boolean")

                If Not IsNothing(dtFiltered) Then
                    If dtFiltered.Rows.Count > 0 Then
                        C1dgPatientDetails.Rows.Count = dtFiltered.Rows.Count + 1
                        'Dim rowCnt As Integer = dtPatientExamDetail.Rows.Count - 1
                        For i As Integer = 1 To dtFiltered.Rows.Count
                            C1dgPatientDetails.SetData(i, COL_DOS, Format(dtFiltered.Rows(i - 1)("DOS"), "MM/dd/yyyy"))
                            C1dgPatientDetails.SetData(i, COL_ExamName, dtFiltered.Rows(i - 1)("Exam Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_TemplateName, dtFiltered.Rows(i - 1)("Template Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_PatientCode, dtFiltered.Rows(i - 1)("Patient Code").ToString())
                            C1dgPatientDetails.SetData(i, COL_PatientName, dtFiltered.Rows(i - 1)("Patient Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_ProviderName, dtFiltered.Rows(i - 1)("Provider Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_Finished, dtFiltered.Rows(i - 1)("Finished").ToString())
                            C1dgPatientDetails.SetData(i, COL_UpdatedDate, dtFiltered.Rows(i - 1)("Last Updated Date").ToString())
                            C1dgPatientDetails.SetData(i, COL_PatientExamsAuditID, dtFiltered.Rows(i - 1)("PatientExamsAuditID").ToString())
                            C1dgPatientDetails.SetData(i, COL_nExamID, dtFiltered.Rows(i - 1)("nExamID").ToString())
                            C1dgPatientDetails.SetData(i, COL_nVisitID, dtFiltered.Rows(i - 1)("nVisitID").ToString())
                            C1dgPatientDetails.SetData(i, COL_nPatientID, dtFiltered.Rows(i - 1)("nPatientID").ToString())
                            C1dgPatientDetails.SetData(i, COL_MachineName, dtFiltered.Rows(i - 1)("Machine Name").ToString())
                            C1dgPatientDetails.SetData(i, COL_nReviewerId, dtFiltered.Rows(i - 1)("nReviewerId").ToString())
                            C1dgPatientDetails.SetData(i, COL_dtReviewDate, dtFiltered.Rows(i - 1)("dtReviewDate").ToString())
                            C1dgPatientDetails.SetData(i, COL_nProviderID, dtFiltered.Rows(i - 1)("nProviderID").ToString())
                        Next
                    End If
                End If



                C1dgPatientDetails.Cols(COL_PatientExamsAuditID).Visible = False
                C1dgPatientDetails.Cols(COL_nExamID).Visible = False
                C1dgPatientDetails.Cols(COL_nVisitID).Visible = False
                C1dgPatientDetails.Cols(COL_nPatientID).Visible = False
                C1dgPatientDetails.Cols(COL_MachineName).Visible = False
                C1dgPatientDetails.Cols(COL_nReviewerId).Visible = False
                C1dgPatientDetails.Cols(COL_dtReviewDate).Visible = False
                C1dgPatientDetails.Cols(COL_nProviderID).Visible = False
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Load, "Patient Recovered Exam viewed from DashBoard", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            MessageBox.Show("Error in Exam.", "gloEMR", MessageBoxButtons.OK)
        Finally
            If Not IsNothing(dtPatientDetails) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            If Not IsNothing(dtFiltered) Then
                dtFiltered.Dispose()
                dtFiltered = Nothing
            End If
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Use Clear Button to Clear Search text box.
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        'Fill_RecoverExam()
        RaiseEvent SearchFilterChanged(True)
    End Sub

    Private Sub C1dgPatientDetails_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1dgPatientDetails.MouseDoubleClick
        ' MessageBox.Show("hi")
        'MessageBox.Show(C1dgPatientDetails.Rows.Item(C1dgPatientDetails.RowSel).ToString())

        'Dim nExamId As Int64 = 0
        'Dim nPatietId As Int64 = 0
        'Dim frmExamView As gloOffice.frmWd_PatientExam = Nothing

        'Try
        '    If C1dgPatientDetails.RowSel > 0 Then
        '        nExamId = CType(C1dgPatientDetails.GetData(C1dgPatientDetails.RowSel, COL_PatientExamsAuditID), Int64)
        '        nPatietId = CType(C1dgPatientDetails.GetData(C1dgPatientDetails.RowSel, COL_nPatientID), Int64)

        '        frmExamView = New frmWd_PatientExam(GetConnectionString, nExamId, nPatietId, True)
        '        frmExamView.Width = 900
        '        frmExamView.Height = 950
        '        frmExamView.ShowDialog(Me)
        '    End If

        'Catch ex As Exception
        '    MessageBox.Show("Error in Exam.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'Finally
        '    If Not IsNothing(frmExamView) Then
        '        frmExamView.Dispose()
        '        frmExamView = Nothing
        '    End If
        'End Try

    End Sub

    Private Sub chkenbdate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkenbdate.CheckedChanged
        If chkenbdate.Checked = True Then
            dtpFromDate.Enabled = True
            dtpToDate.Enabled = True
        Else
            dtpFromDate.Enabled = False
            dtpToDate.Enabled = False
        End If

        RaiseEvent SearchFilterChanged(True)

    End Sub

    Private Sub SearchFilter_Changed(ByVal isChanged As Boolean)
        RefillRecoverExam()
    End Sub
    Private Sub dtpFromDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFromDate.TextChanged
        RaiseEvent SearchFilterChanged(True)
    End Sub

    Private Sub dtpToDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.TextChanged
        RaiseEvent SearchFilterChanged(True)
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            'For i As Integer = 1 To C1dgPatientDetails.Rows.Count - 1
            '    If chkSelectAll.Checked Then
            '        C1dgPatientDetails.SetCellCheck(i, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
            '    Else
            '        C1dgPatientDetails.SetCellCheck(i, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            '    End If
            'Next
        Catch ex As Exception
            'MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("chkSelectAll_CheckedChanged : " & ex.ToString())
        End Try
    End Sub

    Private Sub ts_btnDeleteExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnDeleteExam.Click
        Dim _tempExamId As String = Nothing

        Dim sqlStatement As String = Nothing
        Try

            Dim nIsAuditingEnabled As Integer = 0
            Dim dbLayer As New gloDatabaseLayer.DBLayer(GetConnectionString)
            dbLayer.Connect(False)
            nIsAuditingEnabled = Convert.ToInt16(dbLayer.ExecuteScalar_Query("SELECT sSettingsValue FROM Settings WHERE sSettingsName = 'AUDIT_LOG_IN_HISTORY'"))
            dbLayer.Disconnect()
            dbLayer.Dispose()
            dbLayer = Nothing
            If nIsAuditingEnabled = 1 Then
                MsgBox("Previous exams cannot be deleted because Auditing setting is enabled.", MsgBoxStyle.Information)
               
                Exit Sub
            End If

            For Each row As C1.Win.C1FlexGrid.Row In C1dgPatientDetails.Rows
                If C1dgPatientDetails.GetCellCheck(Convert.ToInt32(row.Index), COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    If _tempExamId = "" Then
                        _tempExamId = C1dgPatientDetails.GetData(row.Index, COL_PatientExamsAuditID).ToString()
                    Else
                        _tempExamId = _tempExamId & "," & C1dgPatientDetails.GetData(row.Index, COL_PatientExamsAuditID).ToString()
                    End If
                End If
            Next
            If Not IsNothing(_tempExamId) AndAlso _tempExamId <> "" Then
                sqlStatement = "Delete from PatientExams_Audit where PatientExamsAuditId  in (" & _tempExamId & ")"
                Dim oDB As gloStream.gloDataBase.gloDataBase = Nothing
                oDB = New gloStream.gloDataBase.gloDataBase()
                If Not IsNothing(oDB) Then
                    oDB.Connect(GetConnectionString)
                    oDB.ExecuteNonSQLQuery(sqlStatement)
                    oDB.Disconnect()
                    oDB.Dispose()
                    oDB = Nothing
                End If
                Fill_RecoverExam()
            Else
                MessageBox.Show("Please select Exams.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.None)
            End If
        Catch ex As Exception
            UpdateLog("Error While Deleting the Patient Exam : " & ex.ToString())
        End Try
    End Sub

    'Private Sub ts_btnViewExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnViewExam.Click
    '    Dim nExamId As Int64 = 0
    '    Dim nPatietId As Int64 = 0
    '    Dim frmExamView As gloOffice.frmWd_PatientExam = Nothing

    '    Try
    '        nExamId = CType(C1dgPatientDetails.GetData(C1dgPatientDetails.RowSel, COL_PatientExamsAuditID), Int64)
    '        nPatietId = CType(C1dgPatientDetails.GetData(C1dgPatientDetails.RowSel, COL_nPatientID), Int64)

    '        frmExamView = New frmWd_PatientExam(GetConnectionString, nExamId, True)
    '        frmExamView.Width = 900
    '        frmExamView.Height = 950
    '        frmExamView.ShowDialog(Me)

    '    Catch ex As Exception
    '        UpdateLog("Error While View the Patient Exam : " & ex.ToString())
    '    Finally
    '        If Not IsNothing(frmExamView) Then
    '            frmExamView.Dispose()
    '            frmExamView = Nothing
    '        End If
    '    End Try

    'End Sub


    Private Sub ts_btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnViewExam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnViewExam.Click
        Dim nExamId As Int64 = 0
        Dim nPatietId As Int64 = 0
        Dim frmExamView As gloOffice.frmWd_PatientExam = Nothing

        Try
            If C1dgPatientDetails.RowSel > 0 Then
                nExamId = CType(C1dgPatientDetails.GetData(C1dgPatientDetails.RowSel, COL_PatientExamsAuditID), Int64)
                nPatietId = CType(C1dgPatientDetails.GetData(C1dgPatientDetails.RowSel, COL_nPatientID), Int64)

                frmExamView = New frmWd_PatientExam(GetConnectionString, nExamId, nPatietId, True)
                frmExamView.Width = 900
                frmExamView.Height = 900
                frmExamView.Top = 0
                frmExamView.Left = 20
                frmExamView.ShowDialog(IIf(IsNothing(frmExamView.Parent), Me, frmExamView.Parent))
            End If

        Catch ex As Exception
            MessageBox.Show("Error in Exam.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If Not IsNothing(frmExamView) Then
                frmExamView.Dispose()
                frmExamView = Nothing
            End If
        End Try
    End Sub

    Private Sub ts_btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnSelectAll.Click
        Try
            For i As Integer = 1 To C1dgPatientDetails.Rows.Count - 1
                If ts_btnSelectAll.Tag.ToUpper.Trim() = "SELECT ALL" Then
                    C1dgPatientDetails.SetCellCheck(i, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
                Else
                    C1dgPatientDetails.SetCellCheck(i, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                End If
            Next

            If ts_btnSelectAll.Tag.ToUpper.Trim() = "SELECT ALL" Then
                ts_btnSelectAll.Text = "De-Select All"
                ts_btnSelectAll.ToolTipText = "De-Select All"
                ts_btnSelectAll.Tag = "De-Select All"
            Else
                ts_btnSelectAll.Text = "Select All"
                ts_btnSelectAll.ToolTipText = "Select All"
                ts_btnSelectAll.Tag = "Select All"
            End If
        Catch ex As Exception
            MessageBox.Show("Error While Selecting Exam.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub DesignC1PatientDetailGrid()
        Dim _width As Long
        Try
            _width = pnlRecoverExam.Width / 3
            If Not IsNothing(C1dgPatientDetails) Then
                C1dgPatientDetails.Cols(COL_nVisitID).Width = 0
                C1dgPatientDetails.Cols(COL_nPatientID).Width = 0
                C1dgPatientDetails.Cols(COL_DOS).Width = _width * 0.25
                C1dgPatientDetails.Cols(COL_ExamName).Width = _width * 0.7
                C1dgPatientDetails.Cols(COL_TemplateName).Width = _width * 0.6
                C1dgPatientDetails.Cols(COL_PatientName).Width = _width * 0.4
                C1dgPatientDetails.Cols(COL_PatientCode).Width = _width * 0.25
                C1dgPatientDetails.Cols(COL_ProviderName).Width = _width * 0.4
                C1dgPatientDetails.Cols(COL_Finished).Width = _width * 0.2
                C1dgPatientDetails.Cols(COL_UpdatedDate).Width = _width * 0.4
                C1dgPatientDetails.Cols(COL_SELECT).Width = _width * 0.15

                C1dgPatientDetails.Cols(COL_PatientCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                C1dgPatientDetails.Cols(COL_ExamName).AllowDragging = False
                C1dgPatientDetails.Cols(COL_TemplateName).AllowDragging = False
                C1dgPatientDetails.Cols(COL_ProviderName).AllowDragging = False
                'C1dgPatientDetails.Cols("nExamID").Caption = "Exam Id"
                C1dgPatientDetails.Cols(COL_DOS).AllowDragging = False
                C1dgPatientDetails.Cols(COL_PatientName).AllowDragging = False
                C1dgPatientDetails.Cols(COL_PatientCode).AllowDragging = False
                C1dgPatientDetails.Cols(COL_ProviderName).AllowDragging = False
                C1dgPatientDetails.Cols(COL_Finished).AllowDragging = False

                C1dgPatientDetails.Cols(COL_ExamName).AllowResizing = False
                C1dgPatientDetails.Cols(COL_TemplateName).AllowResizing = False
                C1dgPatientDetails.Cols(COL_ProviderName).AllowResizing = False
                C1dgPatientDetails.Cols(COL_DOS).AllowResizing = False
                C1dgPatientDetails.Cols(COL_PatientName).AllowResizing = False
                C1dgPatientDetails.Cols(COL_PatientCode).AllowResizing = False
                C1dgPatientDetails.Cols(COL_Finished).AllowResizing = False
                C1dgPatientDetails.Cols(COL_UpdatedDate).AllowResizing = False
                C1dgPatientDetails.Cols(COL_PatientExamsAuditID).AllowResizing = False
                C1dgPatientDetails.Cols(COL_nExamID).AllowResizing = False
                C1dgPatientDetails.Cols(COL_nVisitID).AllowResizing = False
                C1dgPatientDetails.Cols(COL_nPatientID).AllowResizing = False
                C1dgPatientDetails.Cols(COL_MachineName).AllowResizing = False
                C1dgPatientDetails.Cols(COL_nReviewerId).AllowResizing = False
                C1dgPatientDetails.Cols(COL_dtReviewDate).AllowResizing = False
                C1dgPatientDetails.Cols(COL_nProviderID).AllowResizing = False

                C1dgPatientDetails.Cols(COL_ExamName).AllowEditing = False
                C1dgPatientDetails.Cols(COL_TemplateName).AllowEditing = False
                C1dgPatientDetails.Cols(COL_ProviderName).AllowEditing = False
                C1dgPatientDetails.Cols(COL_DOS).AllowEditing = False
                C1dgPatientDetails.Cols(COL_PatientName).AllowEditing = False
                C1dgPatientDetails.Cols(COL_PatientCode).AllowEditing = False
                C1dgPatientDetails.Cols(COL_Finished).AllowEditing = False
                C1dgPatientDetails.Cols(COL_UpdatedDate).AllowEditing = False
                C1dgPatientDetails.Cols(COL_PatientExamsAuditID).AllowEditing = False
                C1dgPatientDetails.Cols(COL_nExamID).AllowEditing = False
                C1dgPatientDetails.Cols(COL_nVisitID).AllowEditing = False
                C1dgPatientDetails.Cols(COL_nPatientID).AllowEditing = False
                C1dgPatientDetails.Cols(COL_MachineName).AllowEditing = False
                C1dgPatientDetails.Cols(COL_nReviewerId).AllowEditing = False
                C1dgPatientDetails.Cols(COL_dtReviewDate).AllowEditing = False
                C1dgPatientDetails.Cols(COL_nProviderID).AllowEditing = False

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub C1dgPatientDetails_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1dgPatientDetails.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
