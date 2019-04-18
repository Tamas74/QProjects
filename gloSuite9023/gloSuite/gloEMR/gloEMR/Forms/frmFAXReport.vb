'To Display the all Patient's Pending & Sent FAXes
Imports System.IO
Imports System.Windows.Documents

Public Class frmFAXReport
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
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtTo, dtFrom}
            Dim cntControls() As System.Windows.Forms.Control = {dtTo, dtFrom}
            components.Dispose()
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

          

            If (IsNothing(dtpControls) = False) Then
                If dtpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                End If
            End If


            If (IsNothing(cntControls) = False) Then
                If cntControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                End If
            End If


            If Not (components Is Nothing) Then
                Try
                    If (IsNothing(SaveFileDialog1) = False) Then
                        SaveFileDialog1.Dispose()
                        SaveFileDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents optPendingFAXes As System.Windows.Forms.RadioButton
    Friend WithEvents optSentFaxes As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents trvCriteria As System.Windows.Forms.TreeView
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftTopTop As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents flxFAXes As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ts_btnShowFaxes As System.Windows.Forms.ToolStripButton
    Private WithEvents cmbFaxStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents numTopRecords As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents tmrprint As System.Windows.Forms.Timer
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFAXReport))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.trvCriteria = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.optPendingFAXes = New System.Windows.Forms.RadioButton()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.optSentFaxes = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlLeftMain = New System.Windows.Forms.Panel()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.flxFAXes = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.pnlLeftTopTop = New System.Windows.Forms.Panel()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.numTopRecords = New System.Windows.Forms.NumericUpDown()
        Me.cmbFaxStatus = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnExport = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnShowFaxes = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.tmrprint = New System.Windows.Forms.Timer(Me.components)
        Me.pnlLeft.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlLeftMain.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        CType(Me.flxFAXes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.pnlLeftTopTop.SuspendLayout()
        CType(Me.numTopRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.Panel3)
        Me.pnlLeft.Controls.Add(Me.Panel5)
        Me.pnlLeft.Controls.Add(Me.Panel4)
        Me.pnlLeft.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(190, 464)
        Me.pnlLeft.TabIndex = 0
        Me.pnlLeft.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.trvCriteria)
        Me.Panel3.Controls.Add(Me.Label27)
        Me.Panel3.Controls.Add(Me.lbl_pnlRight)
        Me.Panel3.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel3.Controls.Add(Me.lbl_pnlTop)
        Me.Panel3.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 55)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(190, 409)
        Me.Panel3.TabIndex = 9
        '
        'trvCriteria
        '
        Me.trvCriteria.BackColor = System.Drawing.Color.White
        Me.trvCriteria.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCriteria.ForeColor = System.Drawing.Color.Black
        Me.trvCriteria.HideSelection = False
        Me.trvCriteria.ImageIndex = 0
        Me.trvCriteria.ImageList = Me.ImageList1
        Me.trvCriteria.Indent = 20
        Me.trvCriteria.ItemHeight = 20
        Me.trvCriteria.Location = New System.Drawing.Point(4, 3)
        Me.trvCriteria.Name = "trvCriteria"
        Me.trvCriteria.SelectedImageIndex = 0
        Me.trvCriteria.Size = New System.Drawing.Size(185, 402)
        Me.trvCriteria.TabIndex = 1
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Olders.ico")
        Me.ImageList1.Images.SetKeyName(1, "Yesterdays.ico")
        Me.ImageList1.Images.SetKeyName(2, "Last Week.ico")
        Me.ImageList1.Images.SetKeyName(3, "LastMonth.ico")
        Me.ImageList1.Images.SetKeyName(4, "Current.ico")
        Me.ImageList1.Images.SetKeyName(5, "Fax.ico")
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Location = New System.Drawing.Point(4, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(185, 2)
        Me.Label27.TabIndex = 12
        Me.Label27.Text = "label1"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(189, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 404)
        Me.lbl_pnlRight.TabIndex = 11
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 405)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(186, 1)
        Me.lbl_pnlBottom.TabIndex = 10
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(186, 1)
        Me.lbl_pnlTop.TabIndex = 9
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 406)
        Me.lbl_pnlLeft.TabIndex = 8
        Me.lbl_pnlLeft.Text = "label4"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.pnlLeftTop)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 28)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(190, 27)
        Me.Panel5.TabIndex = 11
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLeftTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTop.Controls.Add(Me.optPendingFAXes)
        Me.pnlLeftTop.Controls.Add(Me.Label25)
        Me.pnlLeftTop.Controls.Add(Me.optSentFaxes)
        Me.pnlLeftTop.Controls.Add(Me.Label9)
        Me.pnlLeftTop.Controls.Add(Me.Label10)
        Me.pnlLeftTop.Controls.Add(Me.Label11)
        Me.pnlLeftTop.Controls.Add(Me.Label12)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(3, 0)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Size = New System.Drawing.Size(187, 24)
        Me.pnlLeftTop.TabIndex = 0
        '
        'optPendingFAXes
        '
        Me.optPendingFAXes.BackColor = System.Drawing.Color.Transparent
        Me.optPendingFAXes.Dock = System.Windows.Forms.DockStyle.Left
        Me.optPendingFAXes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPendingFAXes.Location = New System.Drawing.Point(16, 1)
        Me.optPendingFAXes.Name = "optPendingFAXes"
        Me.optPendingFAXes.Size = New System.Drawing.Size(91, 22)
        Me.optPendingFAXes.TabIndex = 0
        Me.optPendingFAXes.Text = "Pending"
        Me.optPendingFAXes.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(1, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(15, 22)
        Me.Label25.TabIndex = 9
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'optSentFaxes
        '
        Me.optSentFaxes.BackColor = System.Drawing.Color.Transparent
        Me.optSentFaxes.Dock = System.Windows.Forms.DockStyle.Right
        Me.optSentFaxes.Location = New System.Drawing.Point(113, 1)
        Me.optSentFaxes.Name = "optSentFaxes"
        Me.optSentFaxes.Size = New System.Drawing.Size(73, 22)
        Me.optSentFaxes.TabIndex = 1
        Me.optSentFaxes.Text = "Sent"
        Me.optSentFaxes.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(1, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(185, 1)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 23)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Location = New System.Drawing.Point(186, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 23)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(187, 1)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(190, 28)
        Me.Panel4.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(187, 22)
        Me.Panel1.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Location = New System.Drawing.Point(1, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(185, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 21)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(186, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 21)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(187, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(187, 22)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "  Fax Status"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlLeftMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.pnlMain.Size = New System.Drawing.Size(882, 464)
        Me.pnlMain.TabIndex = 2
        '
        'pnlLeftMain
        '
        Me.pnlLeftMain.Controls.Add(Me.pnl_Base)
        Me.pnlLeftMain.Controls.Add(Me.Panel7)
        Me.pnlLeftMain.Controls.Add(Me.Panel6)
        Me.pnlLeftMain.Controls.Add(Me.Splitter1)
        Me.pnlLeftMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftMain.Location = New System.Drawing.Point(3, 0)
        Me.pnlLeftMain.Name = "pnlLeftMain"
        Me.pnlLeftMain.Size = New System.Drawing.Size(879, 464)
        Me.pnlLeftMain.TabIndex = 3
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.Label21)
        Me.pnl_Base.Controls.Add(Me.Label22)
        Me.pnl_Base.Controls.Add(Me.Label23)
        Me.pnl_Base.Controls.Add(Me.Label24)
        Me.pnl_Base.Controls.Add(Me.flxFAXes)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(3, 55)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_Base.Size = New System.Drawing.Size(876, 409)
        Me.pnl_Base.TabIndex = 8
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(1, 405)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(871, 1)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Location = New System.Drawing.Point(0, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 405)
        Me.Label22.TabIndex = 3
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Location = New System.Drawing.Point(872, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 405)
        Me.Label23.TabIndex = 2
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(873, 1)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "label1"
        '
        'flxFAXes
        '
        Me.flxFAXes.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.flxFAXes.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxFAXes.ColumnInfo = "12,0,0,0,0,105,Columns:0{Style:""ImageAlign:CenterCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{StyleFixed:""TextAli" & _
    "gn:CenterCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.flxFAXes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxFAXes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.flxFAXes.Location = New System.Drawing.Point(0, 0)
        Me.flxFAXes.Name = "flxFAXes"
        Me.flxFAXes.Rows.DefaultSize = 21
        Me.flxFAXes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.flxFAXes.Size = New System.Drawing.Size(873, 406)
        Me.flxFAXes.StyleInfo = resources.GetString("flxFAXes.StyleInfo")
        Me.flxFAXes.TabIndex = 1
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.pnlLeftTopTop)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(3, 28)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel7.Size = New System.Drawing.Size(876, 27)
        Me.Panel7.TabIndex = 10
        '
        'pnlLeftTopTop
        '
        Me.pnlLeftTopTop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftTopTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTopTop.Controls.Add(Me.Label29)
        Me.pnlLeftTopTop.Controls.Add(Me.numTopRecords)
        Me.pnlLeftTopTop.Controls.Add(Me.cmbFaxStatus)
        Me.pnlLeftTopTop.Controls.Add(Me.Label28)
        Me.pnlLeftTopTop.Controls.Add(Me.Label26)
        Me.pnlLeftTopTop.Controls.Add(Me.dtTo)
        Me.pnlLeftTopTop.Controls.Add(Me.Label3)
        Me.pnlLeftTopTop.Controls.Add(Me.dtFrom)
        Me.pnlLeftTopTop.Controls.Add(Me.Label2)
        Me.pnlLeftTopTop.Controls.Add(Me.Label17)
        Me.pnlLeftTopTop.Controls.Add(Me.Label18)
        Me.pnlLeftTopTop.Controls.Add(Me.Label19)
        Me.pnlLeftTopTop.Controls.Add(Me.Label20)
        Me.pnlLeftTopTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTopTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTopTop.Name = "pnlLeftTopTop"
        Me.pnlLeftTopTop.Size = New System.Drawing.Size(873, 24)
        Me.pnlLeftTopTop.TabIndex = 0
        '
        'Label29
        '
        Me.Label29.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(674, 5)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(122, 14)
        Me.Label29.TabIndex = 58
        Me.Label29.Text = "Showing Records :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'numTopRecords
        '
        Me.numTopRecords.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numTopRecords.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numTopRecords.Location = New System.Drawing.Point(798, 1)
        Me.numTopRecords.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.numTopRecords.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numTopRecords.Name = "numTopRecords"
        Me.numTopRecords.Size = New System.Drawing.Size(66, 22)
        Me.numTopRecords.TabIndex = 57
        Me.numTopRecords.Tag = "Queue"
        Me.numTopRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numTopRecords.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'cmbFaxStatus
        '
        Me.cmbFaxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFaxStatus.FormattingEnabled = True
        Me.cmbFaxStatus.Location = New System.Drawing.Point(485, 1)
        Me.cmbFaxStatus.Name = "cmbFaxStatus"
        Me.cmbFaxStatus.Size = New System.Drawing.Size(154, 22)
        Me.cmbFaxStatus.TabIndex = 55
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(429, 5)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(56, 14)
        Me.Label28.TabIndex = 54
        Me.Label28.Text = "Status :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(358, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(19, 22)
        Me.Label26.TabIndex = 9
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.CustomFormat = "MM/dd/yyyy"
        Me.dtTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(257, 1)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(101, 22)
        Me.dtTo.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(177, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 22)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "          To "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(75, 1)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(102, 22)
        Me.dtFrom.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 22)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "From "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Location = New System.Drawing.Point(1, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(871, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 23)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Location = New System.Drawing.Point(872, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 23)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(873, 1)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(3, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel6.Size = New System.Drawing.Size(876, 28)
        Me.Panel6.TabIndex = 9
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(873, 22)
        Me.Panel2.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(1, 21)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(871, 1)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(1, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(871, 21)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "   Fax Report"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 21)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(872, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 21)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(873, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 464)
        Me.Splitter1.TabIndex = 7
        Me.Splitter1.TabStop = False
        Me.Splitter1.Visible = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(882, 54)
        Me.pnlToolStrip.TabIndex = 11
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnPrint, Me.ts_btnExport, Me.ts_btnDelete, Me.ts_btnShowFaxes, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(882, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnExport
        '
        Me.ts_btnExport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnExport.Image = CType(resources.GetObject("ts_btnExport.Image"), System.Drawing.Image)
        Me.ts_btnExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnExport.Name = "ts_btnExport"
        Me.ts_btnExport.Size = New System.Drawing.Size(52, 50)
        Me.ts_btnExport.Tag = "Export"
        Me.ts_btnExport.Text = "&Export"
        Me.ts_btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnShowFaxes
        '
        Me.ts_btnShowFaxes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnShowFaxes.Image = CType(resources.GetObject("ts_btnShowFaxes.Image"), System.Drawing.Image)
        Me.ts_btnShowFaxes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnShowFaxes.Name = "ts_btnShowFaxes"
        Me.ts_btnShowFaxes.Size = New System.Drawing.Size(79, 50)
        Me.ts_btnShowFaxes.Tag = "ShowFaxes"
        Me.ts_btnShowFaxes.Text = "&ShowFaxes"
        Me.ts_btnShowFaxes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'tmrprint
        '
        '
        'frmFAXReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(882, 518)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFAXReport"
        Me.Text = "Fax Status Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlLeftMain.ResumeLayout(False)
        Me.pnl_Base.ResumeLayout(False)
        CType(Me.flxFAXes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.pnlLeftTopTop.ResumeLayout(False)
        Me.pnlLeftTopTop.PerformLayout()
        CType(Me.numTopRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'Private COL_FAXDATE As Byte = 0
    'Private COL_PATNAME As Byte = 1
    'Private COL_FAXTO As Byte = 2
    'Private COL_FAXNO As Byte = 3
    'Private COL_FAXTYPE As Byte = 4



    ''sarika 12th nov 07
    'Private COL_FAXID As Byte = 5

    'Private COL_FAXSELECT As Byte = 6 '' Dhruv 20101002
    '----

    Private COL_FAXSELECT As Byte = 0 '' Dhruv 20101002
    '----
    Private COL_FAXDATE As Byte = 1
    Private COL_PATNAME As Byte = 2
    Private COL_FAXTO As Byte = 3
    Private COL_FAXNO As Byte = 4
    Private COL_FAXTYPE As Byte = 5



    ''sarika 12th nov 07
    Private COL_FAXID As Byte = 6
    Private COL_CurrentStatus As Byte = 7
    Private COL_Attempts As Byte = 8

    Private Enum enm_Faxstatus
        All = 0
        Cancel = 1
        [Error] = 2
        Failed = 3
        Pending = 4
        Sent = 5
    End Enum



    'To Fill Criteria
    Private Sub Fill_Criterias()
        Try
            Dim rootNode As TreeNode
            Dim ChildNode As TreeNode

            With trvCriteria
                .Nodes.Clear()
                rootNode = New TreeNode
                rootNode.Text = "Faxes"
                rootNode.Tag = -1
                rootNode.ImageIndex = 5
                rootNode.SelectedImageIndex = 5
                .Nodes.Add(rootNode)

                ChildNode = New TreeNode
                ChildNode.Text = "Today"
                ChildNode.ImageIndex = 4
                ChildNode.SelectedImageIndex = 4
                rootNode.Nodes.Add(ChildNode)
                ChildNode = Nothing

                ChildNode = New TreeNode
                ChildNode.Text = "Yesterday"
                ChildNode.ImageIndex = 1
                ChildNode.SelectedImageIndex = 1
                rootNode.Nodes.Add(ChildNode)
                ChildNode = Nothing

                ChildNode = New TreeNode
                ChildNode.Text = "Last Week"
                ChildNode.ImageIndex = 2
                ChildNode.SelectedImageIndex = 2
                rootNode.Nodes.Add(ChildNode)
                ChildNode = Nothing

                ChildNode = New TreeNode
                ChildNode.Text = "Last Month"
                ChildNode.ImageIndex = 3
                ChildNode.SelectedImageIndex = 3
                rootNode.Nodes.Add(ChildNode)
                ChildNode = Nothing

                ChildNode = New TreeNode
                ChildNode.Text = "Customize"
                ChildNode.ImageIndex = 0
                ChildNode.SelectedImageIndex = 0
                rootNode.Nodes.Add(ChildNode)
                ChildNode = Nothing
                .SelectedNode = .Nodes(0).Nodes(0)
                .ExpandAll()
            End With
            'For i As Integer = 0 To trvCriteria.Nodes.Count - 1
            '    trvCriteria.Nodes(0).Nodes(i).SelectedImageIndex = i
            'Next
        Catch ex As Exception
            Throw ex
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Fill_FaxStatus()
        RemoveHandler cmbFaxStatus.SelectedIndexChanged, AddressOf cmbFaxStatus_SelectedIndexChanged
        cmbFaxStatus.DataSource = System.Enum.GetValues(GetType(enm_Faxstatus))
        AddHandler cmbFaxStatus.SelectedIndexChanged, AddressOf cmbFaxStatus_SelectedIndexChanged
    End Sub

    'When the user will click on any criteria
    Private Sub trvCriteria_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCriteria.MouseDown
        Try
            Dim trvNode As TreeNode
            'Get the treenode where user has clicked
            trvNode = trvCriteria.GetNodeAt(e.X, e.Y)
            'Check treenode is exists or not
            If IsNothing(trvNode) = False Then
                ''  pnlLeftTopTop.Enabled = False
                'Select the Tree Node
                trvCriteria.SelectedNode = trvNode
                'To Display the Patient's Pending or Sent FAXes as per selected criterias
                ''Call Fill_Faxes()
            End If
        Catch objErr As Exception
            MessageBox.Show("Unable to show exams due to " & objErr.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    'To close the FAX Report
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'While loading form
    Private Sub frmFAXReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        optPendingFAXes.Checked = True

        gloC1FlexStyle.Style(flxFAXes)
        Try
            Me.Cursor = Cursors.WaitCursor
            RemoveHandler dtFrom.ValueChanged, AddressOf dtFrom_ValueChanged
            RemoveHandler dtTo.ValueChanged, AddressOf dtTo_ValueChanged
            'Set Customize DateTime Picker's value as todays date
            dtFrom.Value = System.DateTime.Now.AddDays(-30)
            dtTo.Value = System.DateTime.Now

            Fill_FaxStatus()

            'Fill all criterias
            'Call Fill_Criterias()
            'If Not IsNothing(trvCriteria.Nodes(0).Nodes(0)) Then
            '    trvCriteria.SelectedNode = trvCriteria.Nodes(0).Nodes(0)
            'End If

            'To Display the Patient's Pending or Sent FAXes as per selected criterias
            ''  Call Fill_Faxes()
            Call Fill_FaxStatusReport(cmbFaxStatus.Text.ToString())

            AddHandler dtFrom.ValueChanged, AddressOf dtFrom_ValueChanged
            AddHandler dtTo.ValueChanged, AddressOf dtTo_ValueChanged
            AddHandler numTopRecords.ValueChanged, AddressOf numTopRecords_ValueChanged
            ''Added by Anil on 20071211

            ts_btnShowFaxes.Visible = False
            'Label3.Visible = False
            'dtTo.Visible = False
            ''
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'To Display the Patient's Pending or Sent FAXes as per selected criterias
    Private Sub Fill_Faxes()
        Try
            'Check any Node is selected or not
            '' If IsNothing(trvCriteria.SelectedNode) = True Then Exit Sub

            'Check Is user has selected first node
            '' If trvCriteria.SelectedNode Is trvCriteria.Nodes(0) Then Exit Sub

            'Check Is user has selected Customize node or not
            'If Trim(trvCriteria.SelectedNode.Text) = "Customize" Then
            '    'User has selected Customize Node. So enable the customize panel
            '    pnlLeftTopTop.Enabled = True

            ''If (dtFrom.Value() < dtTo.Value()) = True Then
            ''    MessageBox.Show("From date can not be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ''    Exit Sub
            ''End If

            'Else
            ''User has not selected Customize Node. So disable the customize panel
            ' '' pnlLeftTopTop.Enabled = False
            'End If




            'If (dtFrom.Value.ToShortDateString() > dtTo.Value.ToShortDateString()) = True Then
            '    MessageBox.Show("From date can not be greater than to date. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If


            'Check User has selected Pending or Sent FAX option
            If optPendingFAXes.Checked = True Then
                ''Commented by Mayuri:20100104-To fix issue-#2792:Reports > Fax > Fax Status >Doesn't Show Pending Faxes
                'If Directory.Exists(gstrFAXOutputDirectory) = True Then
                'Retrieve all pending faxes
                Call Fill_PendingFAX()
                'End If
            Else
                'Retrieve all sent faxes
                Call Fill_SentFaxes()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Sub

    'Retrieve all sent faxes
    Private Sub Fill_SentFaxes()
        'gloAuditTrail.gloAuditTrail.UpdatePILog("Fax start Load")
        Dim objFAXDetails As New clsPatientDetails
        Dim dtPatientDetails As DataTable = Nothing
        'Retrieve Patient's Sent Faxes as per selected criteria
        '''''''''''''''''''Code modifications are done by Anil on 20071113
        Dim _DateRange() As DateTime
        Try
            Select Case Trim(trvCriteria.SelectedNode.Text)
                Case "Today"
                    dtPatientDetails = objFAXDetails.Fill_FAXStatus(clsPatientDetails.enmCriteria.Today, System.DateTime.Now.Date, System.DateTime.Now.Date)
                    _DateRange = GetDateRange(DateCategory.Today)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "Date"
                        Label3.Visible = False
                        dtFrom.Value = _DateRange(0)
                        dtTo.Visible = False
                        dtTo.Visible = False
                    End If
                Case "Yesterday"
                    dtPatientDetails = objFAXDetails.Fill_FAXStatus(clsPatientDetails.enmCriteria.Yesterday, System.DateTime.Now.Date, System.DateTime.Now.Date)
                    _DateRange = GetDateRange(DateCategory.Yesterday)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "Date"
                        Label3.Visible = False
                        dtFrom.Value = _DateRange(0)
                        dtTo.Visible = False
                    End If
                Case "Last Week"
                    dtPatientDetails = objFAXDetails.Fill_FAXStatus(clsPatientDetails.enmCriteria.LastWeek, System.DateTime.Now.Date, System.DateTime.Now.Date)
                    _DateRange = GetDateRange(DateCategory.LastWeek)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "From"
                        Label3.Text = "To"
                        Label3.Visible = True
                        dtTo.Visible = True
                        dtFrom.Value = _DateRange(0).AddDays(1)
                        dtTo.Value = _DateRange(1)
                    End If
                Case "Last Month"
                    dtPatientDetails = objFAXDetails.Fill_FAXStatus(clsPatientDetails.enmCriteria.LastMonth, System.DateTime.Now.Date, System.DateTime.Now.Date)
                    _DateRange = GetDateRange(DateCategory.LastMonth)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "From"
                        Label3.Text = "To"
                        Label3.Visible = True
                        dtTo.Visible = True
                        dtFrom.Value = _DateRange(0)
                        dtTo.Value = _DateRange(1)
                    End If
                Case "Customize"
                    dtPatientDetails = objFAXDetails.Fill_FAXStatus(clsPatientDetails.enmCriteria.Customize, dtFrom.Value, dtTo.Value)
                    Label2.Text = "From"
                    Label3.Text = "To"
                    Label3.Visible = True
                    dtTo.Visible = True

            End Select
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Call DesignGrid()
            Dim nCount As Int16

            flxFAXes.Redraw = False

            With flxFAXes
                For nCount = 0 To dtPatientDetails.Rows.Count - 1
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_FAXDATE, dtPatientDetails.Rows(nCount).Item(1))
                    .SetData(.Rows.Count - 1, COL_PATNAME, dtPatientDetails.Rows(nCount).Item(7))
                    .SetData(.Rows.Count - 1, COL_FAXTO, dtPatientDetails.Rows(nCount).Item(2))
                    .SetData(.Rows.Count - 1, COL_FAXNO, dtPatientDetails.Rows(nCount).Item(3))
                    .SetData(.Rows.Count - 1, COL_FAXTYPE, dtPatientDetails.Rows(nCount).Item(4))
                    .SetData(.Rows.Count - 1, COL_FAXID, dtPatientDetails.Rows(nCount).Item("FAXID"))
                    If optPendingFAXes.Checked = True Then
                        .SetData(.Rows.Count - 1, COL_CurrentStatus, dtPatientDetails.Rows(nCount).Item("CurrentStatus"))
                    End If
                Next
            End With
            flxFAXes.Redraw = True
            'gloAuditTrail.gloAuditTrail.UpdatePILog("Fax end Load")

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If (IsNothing(dtPatientDetails) = False) Then
                dtPatientDetails.Dispose()
                dtPatientDetails = Nothing
            End If
            If (IsNothing(objFAXDetails) = False) Then
                objFAXDetails.Dispose()
                objFAXDetails = Nothing
            End If

        End Try
    End Sub

    ''Retrieve all pending faxes
    Private Sub Fill_PendingFAX()
        Dim objPendingFAXes As New clsFAX
        Dim dtPendingFAX As DataTable = Nothing
        Dim _DateRange() As DateTime
        Try
            Select Case Trim(trvCriteria.SelectedNode.Text)
                Case "Today"
                    dtPendingFAX = objPendingFAXes.Fill_PendingFAXes(clsFAX.enmDateCriteria.Today, System.DateTime.Now, System.DateTime.Now)
                    _DateRange = GetDateRange(DateCategory.Today)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "Date"
                        Label3.Visible = False
                        dtFrom.Value = _DateRange(0)
                        dtTo.Visible = False
                        dtTo.Visible = False
                    End If
                Case "Yesterday"
                    dtPendingFAX = objPendingFAXes.Fill_PendingFAXes(clsFAX.enmDateCriteria.Yesterday, System.DateTime.Now, System.DateTime.Now)
                    _DateRange = GetDateRange(DateCategory.Yesterday)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "Date"
                        Label3.Visible = False
                        dtFrom.Value = _DateRange(0)
                        dtTo.Visible = False
                    End If
                Case "Last Week"
                    dtPendingFAX = objPendingFAXes.Fill_PendingFAXes(clsFAX.enmDateCriteria.LastWeek, System.DateTime.Now, System.DateTime.Now)
                    _DateRange = GetDateRange(DateCategory.LastWeek)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "From"
                        Label3.Text = "To"
                        Label3.Visible = True
                        dtTo.Visible = True
                        dtFrom.Value = _DateRange(0).AddDays(1)
                        dtTo.Value = _DateRange(1)
                    End If
                Case "Last Month"
                    dtPendingFAX = objPendingFAXes.Fill_PendingFAXes(clsFAX.enmDateCriteria.LastMonth, System.DateTime.Now, System.DateTime.Now)
                    _DateRange = GetDateRange(DateCategory.LastMonth)
                    If _DateRange.Length > 0 Then
                        Label2.Text = "From"
                        Label3.Text = "To"
                        Label3.Visible = True
                        dtTo.Visible = True
                        dtFrom.Value = _DateRange(0)
                        dtTo.Value = _DateRange(1)
                    End If
                Case "Customize"
                    dtPendingFAX = objPendingFAXes.Fill_PendingFAXes(clsFAX.enmDateCriteria.Customize, dtFrom.Value, dtTo.Value)
                    Label2.Text = "From"
                    Label3.Text = "To"
                    Label3.Visible = True
                    dtTo.Visible = True
                    'dtFrom.Value = Date.Now
                    'dtTo.Value = Date.Now
            End Select

            'objPendingFAXes = Nothing
            Call DesignGrid()
            Dim nCount As Int16
            flxFAXes.Redraw = False
            With flxFAXes
                For nCount = 0 To dtPendingFAX.Rows.Count - 1
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_FAXDATE, dtPendingFAX.Rows(nCount).Item("FAXDate"))
                    .SetData(.Rows.Count - 1, COL_PATNAME, dtPendingFAX.Rows(nCount).Item("PatientName"))
                    .SetData(.Rows.Count - 1, COL_FAXTO, dtPendingFAX.Rows(nCount).Item("FAXTo"))
                    .SetData(.Rows.Count - 1, COL_FAXNO, dtPendingFAX.Rows(nCount).Item("FAXNo"))
                    .SetData(.Rows.Count - 1, COL_FAXTYPE, dtPendingFAX.Rows(nCount).Item("FAXType"))
                    .SetData(.Rows.Count - 1, COL_FAXID, dtPendingFAX.Rows(nCount).Item("FAXID"))
                    .SetData(.Rows.Count - 1, COL_CurrentStatus, dtPendingFAX.Rows(nCount).Item("CurrentStatus"))
                    '.SetData(.Rows.Count - 1, COL_FAXSELECT, dtPendingFAX.Rows(nCount).Item("Select")) ''Dhruv 20101002


                Next
            End With
            flxFAXes.Redraw = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If (IsNothing(objPendingFAXes) = False) Then
                objPendingFAXes.Dispose()
                objPendingFAXes = Nothing
            End If
            If (IsNothing(dtPendingFAX) = False) Then
                dtPendingFAX.Dispose()
                dtPendingFAX = Nothing
            End If
        End Try
    End Sub

    Private Sub Fill_FaxStatusReport(ByVal _sStatus As String)

        '11-Nov-14 Aniket: Resolving Bug #75873: gloEMR > Reports > Fax > Failed Faxes > Showing wrong message while try to enter 'To' date
        If (dtFrom.Value.Date > dtTo.Value.Date) = True Then
            MessageBox.Show("From date cannot be greater than to date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim objPendingFAXes As New clsFAX
        Dim dtPendingFAX As DataTable = Nothing

        Try

            dtPendingFAX = objPendingFAXes.Fill_AllFAXes_Withstatus(_sStatus, dtFrom.Value, dtTo.Value, numTopRecords.Value)
            '    objPendingFAXes = Nothing
            Call DesignGrid()
            Dim nCount As Int16
            flxFAXes.Redraw = False
            With flxFAXes
                For nCount = 0 To dtPendingFAX.Rows.Count - 1
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_FAXDATE, dtPendingFAX.Rows(nCount).Item("FAXDate"))
                    .SetData(.Rows.Count - 1, COL_PATNAME, dtPendingFAX.Rows(nCount).Item("PatientName"))
                    .SetData(.Rows.Count - 1, COL_FAXTO, dtPendingFAX.Rows(nCount).Item("FAXTo"))
                    .SetData(.Rows.Count - 1, COL_FAXNO, dtPendingFAX.Rows(nCount).Item("FAXNo"))
                    .SetData(.Rows.Count - 1, COL_FAXTYPE, dtPendingFAX.Rows(nCount).Item("FAXType"))
                    .SetData(.Rows.Count - 1, COL_FAXID, dtPendingFAX.Rows(nCount).Item("FAXID"))
                    .SetData(.Rows.Count - 1, COL_CurrentStatus, dtPendingFAX.Rows(nCount).Item("CurrentStatus"))
                    .SetData(.Rows.Count - 1, COL_Attempts, dtPendingFAX.Rows(nCount).Item("NoOfAttempts"))
                    '.SetData(.Rows.Count - 1, COL_FAXSELECT, dtPendingFAX.Rows(nCount).Item("Select")) ''Dhruv 20101002


                Next
            End With
            flxFAXes.Redraw = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        Finally
            If (IsNothing(objPendingFAXes) = False) Then
                objPendingFAXes.Dispose()
                objPendingFAXes = Nothing
            End If
            If (IsNothing(dtPendingFAX) = False) Then
                dtPendingFAX.Dispose()
                dtPendingFAX = Nothing
            End If
        End Try
    End Sub
    Private Sub optPendingFAXes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPendingFAXes.CheckedChanged
        Try
            'Display Patient's Pending or Sent FAXes
            If optPendingFAXes.Checked = True Then
                optPendingFAXes.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
                'btnDelete.Visible = True
            Else
                optPendingFAXes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
                'btnDelete.Visible = False
            End If
            ''  Call Fill_Faxes()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignGrid()
        Try
            With flxFAXes
                .Rows.Count = 1
                .Rows.Fixed = 1
                'code commented by sarika 12th Nov 07
                '.Cols.Count = 5
                'code added by sarika 12th Nov 07
                '.Cols.Count = 6
                '---
                'code added by sarika 12th Nov 07
                .Cols.Count = 9
                '---

                ''dhruv 20101002
                .SetData(0, COL_FAXSELECT, "Select")    ''Setting the columns name as "Select"
                ''--


                .SetData(0, COL_FAXDATE, "Fax Date")
                .SetData(0, COL_PATNAME, "Patient Name")
                .SetData(0, COL_FAXTO, "Fax To")
                .SetData(0, COL_FAXNO, "Fax No")
                .SetData(0, COL_FAXTYPE, "Fax Type")

                'code added by sarika 12th Nov 07
                .SetData(0, COL_FAXID, "Fax ID")

                .SetData(0, COL_CurrentStatus, "Current Status")
                .SetData(0, COL_Attempts, "Attempts")

                .Cols(COL_FAXDATE).Width = 130
                .Cols(COL_PATNAME).Width = 175
                .Cols(COL_FAXTO).Width = 150
                .Cols(COL_FAXNO).Width = 100
                .Cols(COL_FAXTYPE).Width = 270
                'code added by sarika 12th nov 07
                .Cols(COL_FAXID).Width = 0


                'If optPendingFAXes.Checked = True Then
                .Cols(COL_CurrentStatus).Width = 130
                'Else
                '.Cols(COL_CurrentStatus).Width = 0
                'End If
                .Cols(COL_Attempts).Width = 100

                '.Cols(COL_FAXID).Visible = False
                '   .Cols(COL_FAXDATE).DataType = System.Type.GetType("System.DateTime")
                '---

                .Cols(COL_FAXSELECT).Width = 70 ''dhruv 20101002

                .Cols(COL_FAXDATE).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_PATNAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXTO).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXNO).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_FAXTYPE).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(COL_CurrentStatus).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_Attempts).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                'code added by sarika 12th nov 07
                '.Cols(COL_FAXID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                '---



                .Cols(COL_FAXSELECT).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter '' Dhruv 20101002 '' Setting the alignment to the center of the text
                .Cols(COL_FAXSELECT).DataType = GetType(System.Boolean)  ''End----------Dhruv ''for the C1flex we can set the checkbox as type of the boolean in the system.


                .Cols(COL_FAXSELECT).AllowEditing = True  '' Dhruv 20101002 ''Setting the 0th columns as editing to true

                For ncount As Integer = 1 To .Cols.Count - 1    ''Dhruv 20101102
                    .Cols(ncount).AllowEditing = False          ''checking the columns if other then 0th column set its editing as false.
                    .Cols(ncount).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                Next                                            ''End---------------------Dhruv
                .Cols(8).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter
                .Cols(COL_FAXID).Visible = False
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw ex
        End Try
    End Sub
#Region "Commented By Dhruv 20101102 As it was only converting single data to excel format"
    Private Sub ExportReport()
        Try
            If flxFAXes.Row >= 1 Then
                If MessageBox.Show("Are you sure you want to export the report?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    With SaveFileDialog1
                        .Filter = "Excel File(*.xls)|*.xls|Text File (*.txt)|*.txt"
                        .OverwritePrompt = True
                        .ShowHelp = False
                        .Title = "Select export location"
                    End With
                    If SaveFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                        If SaveFileDialog1.FileName.ToLower.EndsWith(".xls") = True Then
                            flxFAXes.Cols(0).Visible = False
                            flxFAXes.Cols(COL_FAXDATE).DataType = System.Type.GetType("System.DateTime")
                            flxFAXes.SaveGrid(SaveFileDialog1.FileName, C1.Win.C1FlexGrid.FileFormatEnum.Excel, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                            flxFAXes.Cols(0).Visible = True
                        ElseIf SaveFileDialog1.FileName.ToLower.EndsWith(".txt") = True Then
                            Dim myC1 As C1.Win.C1FlexGrid.C1FlexGrid = Nothing
                            myC1 = CopyDataToTempC1(flxFAXes)
                            myC1.SaveGrid(SaveFileDialog1.FileName, C1.Win.C1FlexGrid.FileFormatEnum.TextCustom, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
                            myC1.Dispose()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.ToString().Contains("Failed to create storage file") Then
                MessageBox.Show("Unable to Export the report as file is already open. Close the file to continue.", "FAX", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Unable to export the data due to " & ex.Message, "FAX", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.ExportToExcel, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  MessageBox.Show("Unable to export the data due to " & ex.Message, "FAX", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region


    Private Function CopyDataToTempC1(ByVal myC1 As C1.Win.C1FlexGrid.C1FlexGrid)
        Dim myC2 As C1.Win.C1FlexGrid.C1FlexGrid = New C1.Win.C1FlexGrid.C1FlexGrid()
        For nCount As Int32 = 0 To myC1.Rows.Count - 1
            myC2.Rows.Add()
            myC2.SetData(nCount, COL_FAXDATE, flxFAXes.GetData(nCount, COL_FAXDATE))
            myC2.SetData(nCount, COL_PATNAME, flxFAXes.GetData(nCount, COL_PATNAME))
            myC2.SetData(nCount, COL_FAXTO, flxFAXes.GetData(nCount, COL_FAXTO))
            myC2.SetData(nCount, COL_FAXNO, flxFAXes.GetData(nCount, COL_FAXNO))
            myC2.SetData(nCount, COL_FAXTYPE, flxFAXes.GetData(nCount, COL_FAXTYPE))
        Next
        Return myC2
    End Function
    Private Sub PrintReport()
        Try
            If flxFAXes.Row >= 1 Then
                flxFAXes.PrintParameters.PrintDocument.DefaultPageSettings.Margins.Left = 5
                flxFAXes.PrintParameters.PrintDocument.DefaultPageSettings.Landscape = True
                flxFAXes.PrintParameters.PrintPreviewDialog.Text = "FAX Report"
                flxFAXes.PrintParameters.PrintPreviewDialog.WindowState = FormWindowState.Maximized
                Dim strHeader As String
                'If optPendingFAXes.Checked Then
                strHeader = "FAX Status Report"
                'Else
                '    strHeader = "Sent Faxes"
                'End If
                'Select Case Trim(trvCriteria.SelectedNode.Text)
                '    Case "Today"
                '        strHeader = strHeader & " on " & System.DateTime.Now.Date
                '    Case "Yesterday"
                '        strHeader = strHeader & " on " & System.DateTime.Now.Date.AddDays(-1)
                '    Case "Last Week"
                '        strHeader = strHeader & " from " & System.DateTime.Now.Date.AddDays(-7).Date & " To " & System.DateTime.Now.Date
                '    Case "Last Month"
                '        strHeader = strHeader & " from " & System.DateTime.Now.Date.AddMonths(-1).Date & " To " & System.DateTime.Now.Date
                '    Case "Customize"
                '        strHeader = strHeader & " from " & dtFrom.Value.Date & " To " & dtTo.Value.Date
                'End Select
                With flxFAXes
                    .Cols(COL_FAXSELECT).Width = 42
                    .Cols(COL_FAXDATE).Width = 125
                    .Cols(COL_PATNAME).Width = 130
                    .Cols(COL_FAXTO).Width = 150
                    .Cols(COL_FAXNO).Width = 80
                    .Cols(COL_FAXTYPE).Width = 200
                End With
                flxFAXes.Cols(0).Visible = False

                'Sandip :: 11 Nov 2014 : changes done for checking if use Default Printer is ON/OFF in gloEMR Application
                If (gblnUseDefaultPrinter) Then
                    flxFAXes.PrintGrid("Fax Report", C1.Win.C1FlexGrid.PrintGridFlags.ShowPreviewDialog, vbTab & strHeader, vbTab & "{0} of {1}")
                Else
                    'flxFAXes.PrintGrid("Fax Report", C1.Win.C1FlexGrid.PrintGridFlags.ShowPageSetupDialog, vbTab & strHeader, vbTab & "{0} of {1}")
                    flxFAXes.PrintGrid("Fax Report", C1.Win.C1FlexGrid.PrintGridFlags.ShowPrintDialog, vbTab & strHeader, vbTab & "{0} of {1}")
                End If
                '**Sandip :: 11 Nov 2014 : changes done for checking if use Default Printer is ON/OFF in gloEMR Application

                flxFAXes.Cols(0).Visible = True
                With flxFAXes
                    .Cols(COL_FAXSELECT).Width = 42
                    .Cols(COL_FAXDATE).Width = 125
                    .Cols(COL_PATNAME).Width = 130
                    .Cols(COL_FAXTO).Width = 150
                    .Cols(COL_FAXNO).Width = 80
                    .Cols(COL_FAXTYPE).Width = 400
                End With
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show("Unable to export the data due to " & ex.Message, "FAX", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub


    Private Sub trvCriteria_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCriteria.AfterSelect
        Try
            ''''Added by Anil on 20071211
            If Trim(trvCriteria.SelectedNode.Text) = "Customize" Then
                ts_btnShowFaxes.Visible = True
                ''btnShowReport.Visible = True
                dtFrom.Value = Date.Now
                dtTo.Value = Date.Now
            Else
                ts_btnShowFaxes.Visible = False

            End If
            ''''''''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try
    End Sub


    'sarika 12th nov 07
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



#Region "Commented by dhruv 20101102 '' Mainly used for the single selected files"
    '' Mainly used for the single selected files
    'Private Sub DeleteReport()
    '    Try
    '        If flxFAXes.Row >= 1 Then
    '            Dim Id As Long
    '            Id = CType((flxFAXes.Item(flxFAXes.Row, 5)), Long)
    '            'check whether the fax is in queue
    '            'if no then delete pending fax
    '            Dim sFaxStatus As String = ""
    '            Dim objPendingFAXes As New clsFAX
    '          

    '                    If optPendingFAXes.Checked = True Then
    '                        sFaxStatus = objPendingFAXes.GetFaxStatus(Id)

    '                        If sFaxStatus = "Queue" Then
    '                            MessageBox.Show("This Pending Fax cannot be deleted as the fax application is attempting to send it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Else
    '                            If MessageBox.Show("Are you sure you want to delete this Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
    '                                'delete the selected pending fax
    '                                objPendingFAXes.DeletePendingFAX(Id)
    '                            End If
    '                        End If
    '                    Else
    '                        If MessageBox.Show("Are you sure you want to delete this Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
    '                            'delete the selected pending fax
    '                            objPendingFAXes.DeleteSentFAX(Id)
    '                        End If
    '                    End If

    '              --
    '            '--------------------------------------------------------------
    '            ''''''This code is added by Anil on
    '            'SortOrder = CType(dgContactsList.DataSource, DataView).Sort
    '            'strSearchstring = txtSearch.Text.Trim
    '            'arrcolumnsort = Split(SortOrder, "]")
    '            'If arrcolumnsort.Length > 1 Then
    '            '    strcolumnName = arrcolumnsort.GetValue(0)
    '            '    strsortorder = arrcolumnsort.GetValue(1)
    '            'End If
    '            'UpdateContacts(strcolumnName, strsortorder, strSearchstring)
    '            '''''''''''''''''''''

    '            'refresh the pending fax list
    '            Fill_Faxes()
    '        End If





    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
#End Region
    ''' <summary>
    ''' Dhruv 20101102
    ''' Used for the multiple selected files
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteReport()
        ''---Variable Declaration---
        Dim Id As Long
        Dim _isMessage As Boolean
        Dim sFaxStatus As String = ""
        Dim objPendingFAXes As New clsFAX
        ''----End-----------Variable Declaration


        Try

            If flxFAXes.Rows.Count <= 1 Then        ''Checking the Rows Count in c1flex having the rows greater then 1
                Exit Sub
            End If                                  ''End----Checking

            For ncount As Integer = 1 To flxFAXes.Rows.Count - 1                            ''Checking each rows of the c1Flex wheather it is selected or not
                If Convert.ToBoolean(flxFAXes.GetData(ncount, COL_FAXSELECT)) = True Then   ''Checking for the selected Row
                    _isMessage = True
                End If                                                                      ''End----------------------------Checking
            Next

            If _isMessage = False Then                                                      ''If No Rows is selected it will come out
                Exit Sub
            End If                                                                           ''end----------------selected

            'If String.Compare(Convert.ToString(flxFAXes.GetData(flxFAXes.RowSel, COL_CurrentStatus)), "Sent", True) <> 0 Then                                           ''"Pending" Checking for the option button   
            '    For ncount As Integer = 1 To flxFAXes.Rows.Count - 1
            '        sFaxStatus = objPendingFAXes.GetFaxStatus(Id)                           ''Checking for the Faxstatus ,if it is in Queue it wiill not be able to delete
            '        If sFaxStatus = "Queue" Then
            '            MessageBox.Show("This Pending Fax cannot be deleted as the fax application is attempting to send it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End If
            '    Next
            '    ''If the faxes are actully in the pending format then it will show faxes to delete
            '    If MessageBox.Show("Are you sure you want to delete the selected Faxes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then '' 
            '        For ncount As Integer = 1 To flxFAXes.Rows.Count - 1
            '            If Convert.ToBoolean(flxFAXes.GetData(ncount, COL_FAXSELECT)) = True Then           ''If the the fax is selected 
            '                Id = flxFAXes.GetData(ncount, COL_FAXID)                                        ''Take the Id of that Fax
            '                objPendingFAXes.DeletePendingFAX_ID(Id)                                         ''Delete the fax against that id
            '            End If
            '        Next
            '    End If
            'Else                                                                              ''"Sent" Option Button 
            '    If MessageBox.Show("Are you sure you want to delete the selected Faxes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            '        For ncount As Integer = 1 To flxFAXes.Rows.Count - 1
            '            If Convert.ToBoolean(flxFAXes.GetData(ncount, COL_FAXSELECT)) = True Then            ''If the the fax is selected
            '                Id = flxFAXes.GetData(ncount, COL_FAXID)                                         ''Take the Id of that Fax
            '                objPendingFAXes.DeleteSentFAX(Id)                                                ''Delete the fax against that id
            '            End If
            '        Next
            '    End If
            'End If

            For ncount As Integer = 1 To flxFAXes.Rows.Count - 1
                ''                                                                       ''Checking for the Faxstatus ,if it is in Queue it wiill not be able to delete
                If Convert.ToBoolean(flxFAXes.GetData(ncount, COL_FAXSELECT)) = True Then
                    Id = flxFAXes.GetData(ncount, COL_FAXID)
                    sFaxStatus = objPendingFAXes.GetFaxStatus(Id)
                    If String.Compare(sFaxStatus, "Queue", True) = 0 Then
                        MessageBox.Show("This Pending Fax cannot be deleted as the fax application is attempting to send it.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            Next

            If MessageBox.Show("Are you sure you want to delete the selected Faxes?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then '' 

                For ncount As Integer = 1 To flxFAXes.Rows.Count - 1

                    If String.Compare(Convert.ToString(flxFAXes.GetData(ncount, COL_CurrentStatus)).Trim(), "Sent", True) = 0 Then                                           ''"Pending" Checking for the option button   

                        If Convert.ToBoolean(flxFAXes.GetData(ncount, COL_FAXSELECT)) = True Then            ''If the the fax is selected
                            Id = flxFAXes.GetData(ncount, COL_FAXID)                                         ''Take the Id of that Fax
                            objPendingFAXes.DeleteSentFAX(Id)                                                ''Delete the fax against that id
                        End If

                    Else

                        If Convert.ToBoolean(flxFAXes.GetData(ncount, COL_FAXSELECT)) = True Then           ''If the the fax is selected 
                            Id = flxFAXes.GetData(ncount, COL_FAXID)                                        ''Take the Id of that Fax
                            objPendingFAXes.DeletePendingFAX_ID(Id)                                         ''Delete the fax against that id
                        End If

                    End If

                Next
            End If
            '' Fill_Faxes()
            Call Fill_FaxStatusReport(cmbFaxStatus.Text.ToString())

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Dhruv 20101102 
    ''' It is used to show the Faxes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowFaxes()
        Try
            'To Display the Patient's Pending or Sent FAXes
            Call Fill_Faxes()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Export"
                Call ExportReport()
            Case "Print"
                tmrprint.Start()
            Case "Delete"
                Call DeleteReport()
            Case "Close"
                Me.Close()

            Case "ShowFaxes"  ''Dhruv20101102 To show the faxes

                Call Fill_FaxStatusReport(cmbFaxStatus.Text.ToString())

        End Select
    End Sub

    Private Sub optSentFaxes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSentFaxes.CheckedChanged
        If optSentFaxes.Checked = True Then
            optSentFaxes.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optSentFaxes.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub


    Private Sub flxFAXes_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles flxFAXes.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub pnlLeftTopTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlLeftTopTop.Paint

    End Sub

    Private Sub cmbFaxStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbFaxStatus.SelectedIndexChanged
        Try
            Call Fill_FaxStatusReport(cmbFaxStatus.Text.ToString())

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtFrom_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtFrom.ValueChanged
        Call Fill_FaxStatusReport(cmbFaxStatus.Text.ToString())
    End Sub

    Private Sub dtTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtTo.ValueChanged
        Call Fill_FaxStatusReport(cmbFaxStatus.Text.ToString())
    End Sub

    Private Sub numTopRecords_ValueChanged(sender As System.Object, e As System.EventArgs) ''Handles numTopRecords.ValueChanged
        Call Fill_FaxStatusReport(cmbFaxStatus.Text.ToString())
    End Sub

    Private Sub tmrprint_Tick(sender As System.Object, e As System.EventArgs) Handles tmrprint.Tick
        tmrprint.Stop() ''added for bugid 104531 
        PrintReport()
    End Sub
End Class
