Imports C1.Win.C1FlexGrid
Imports System.IO

Public Class frmVMS_ImportDocumentEvent
    Inherits System.Windows.Forms.Form
    Dim _PatientID As Long

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()
        _ProcessParameter = New gloStream.gloVMS.Supporting.ImportProcessParameters
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
                Try
                    If (IsNothing(openfiledilogVideo) = False) Then
                        openfiledilogVideo.Dispose()
                        openfiledilogVideo = Nothing
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlDocument As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lvwPages As System.Windows.Forms.ListView
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents openfiledilogVideo As System.Windows.Forms.OpenFileDialog
    Friend WithEvents rbPDF As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents c1Document As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblfilePathLable As System.Windows.Forms.Label
    Friend WithEvents lblFileLengthlable As System.Windows.Forms.Label
    Friend WithEvents lblFileNameLable As System.Windows.Forms.Label
    Friend WithEvents lblfilePath As System.Windows.Forms.Label
    Friend WithEvents lblFileLength As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tls_VMSImportDocument As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents rbImages As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVMS_ImportDocumentEvent))
        Dim ListViewItem15 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("1", 1)
        Dim ListViewItem16 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("2", 1)
        Dim ListViewItem17 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("3", 1)
        Dim ListViewItem18 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("4", 1)
        Dim ListViewItem19 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("5", 1)
        Dim ListViewItem20 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("6", 1)
        Dim ListViewItem21 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("7", 1)
        Dim ListViewItem22 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("8", 1)
        Dim ListViewItem23 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("9", 1)
        Dim ListViewItem24 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("10", 1)
        Dim ListViewItem25 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("11", 1)
        Dim ListViewItem26 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("12", 1)
        Dim ListViewItem27 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("13", 1)
        Dim ListViewItem28 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("14", 1)
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlDocument = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblfilePathLable = New System.Windows.Forms.Label
        Me.lblFileLengthlable = New System.Windows.Forms.Label
        Me.lblFileNameLable = New System.Windows.Forms.Label
        Me.lblfilePath = New System.Windows.Forms.Label
        Me.lblFileLength = New System.Windows.Forms.Label
        Me.lblFileName = New System.Windows.Forms.Label
        Me.rbImages = New System.Windows.Forms.RadioButton
        Me.rbPDF = New System.Windows.Forms.RadioButton
        Me.c1Document = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnAdd = New System.Windows.Forms.Button
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lvwPages = New System.Windows.Forms.ListView
        Me.openfiledilogVideo = New System.Windows.Forms.OpenFileDialog
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tls_VMSImportDocument = New gloGlobal.gloToolStripIgnoreFocus
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlDocument.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.c1Document, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tls_VMSImportDocument.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.pnlDocument)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 84)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(406, 120)
        Me.pnlMain.TabIndex = 52
        '
        'pnlDocument
        '
        Me.pnlDocument.Controls.Add(Me.Panel3)
        Me.pnlDocument.Controls.Add(Me.rbImages)
        Me.pnlDocument.Controls.Add(Me.rbPDF)
        Me.pnlDocument.Controls.Add(Me.c1Document)
        Me.pnlDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDocument.Location = New System.Drawing.Point(3, 0)
        Me.pnlDocument.Name = "pnlDocument"
        Me.pnlDocument.Size = New System.Drawing.Size(400, 117)
        Me.pnlDocument.TabIndex = 44
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ProgressBar1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.lblfilePathLable)
        Me.Panel3.Controls.Add(Me.lblFileLengthlable)
        Me.Panel3.Controls.Add(Me.lblFileNameLable)
        Me.Panel3.Controls.Add(Me.lblfilePath)
        Me.Panel3.Controls.Add(Me.lblFileLength)
        Me.Panel3.Controls.Add(Me.lblFileName)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(400, 117)
        Me.Panel3.TabIndex = 59
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.ForeColor = System.Drawing.Color.LawnGreen
        Me.ProgressBar1.Location = New System.Drawing.Point(1, 101)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(398, 15)
        Me.ProgressBar1.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(398, 1)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 116)
        Me.Label4.TabIndex = 66
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(399, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 116)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(400, 1)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "label1"
        '
        'lblfilePathLable
        '
        Me.lblfilePathLable.AutoSize = True
        Me.lblfilePathLable.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfilePathLable.Location = New System.Drawing.Point(14, 61)
        Me.lblfilePathLable.Name = "lblfilePathLable"
        Me.lblfilePathLable.Size = New System.Drawing.Size(86, 14)
        Me.lblfilePathLable.TabIndex = 63
        Me.lblfilePathLable.Text = "    File Path :"
        Me.lblfilePathLable.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblfilePathLable.Visible = False
        '
        'lblFileLengthlable
        '
        Me.lblFileLengthlable.AutoSize = True
        Me.lblFileLengthlable.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileLengthlable.Location = New System.Drawing.Point(33, 34)
        Me.lblFileLengthlable.Name = "lblFileLengthlable"
        Me.lblFileLengthlable.Size = New System.Drawing.Size(67, 14)
        Me.lblFileLengthlable.TabIndex = 62
        Me.lblFileLengthlable.Text = "File Size :"
        Me.lblFileLengthlable.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFileLengthlable.Visible = False
        '
        'lblFileNameLable
        '
        Me.lblFileNameLable.AutoSize = True
        Me.lblFileNameLable.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileNameLable.Location = New System.Drawing.Point(15, 7)
        Me.lblFileNameLable.Name = "lblFileNameLable"
        Me.lblFileNameLable.Size = New System.Drawing.Size(85, 14)
        Me.lblFileNameLable.TabIndex = 61
        Me.lblFileNameLable.Text = "  File Name :"
        Me.lblFileNameLable.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFileNameLable.Visible = False
        '
        'lblfilePath
        '
        Me.lblfilePath.BackColor = System.Drawing.Color.Transparent
        Me.lblfilePath.Location = New System.Drawing.Point(107, 61)
        Me.lblfilePath.Name = "lblfilePath"
        Me.lblfilePath.Size = New System.Drawing.Size(265, 33)
        Me.lblfilePath.TabIndex = 60
        Me.lblfilePath.Text = "File Path"
        Me.lblfilePath.Visible = False
        '
        'lblFileLength
        '
        Me.lblFileLength.AutoSize = True
        Me.lblFileLength.BackColor = System.Drawing.Color.Transparent
        Me.lblFileLength.Location = New System.Drawing.Point(107, 34)
        Me.lblFileLength.Name = "lblFileLength"
        Me.lblFileLength.Size = New System.Drawing.Size(67, 14)
        Me.lblFileLength.TabIndex = 59
        Me.lblFileLength.Text = "File Lenght"
        Me.lblFileLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFileLength.Visible = False
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.BackColor = System.Drawing.Color.Transparent
        Me.lblFileName.Location = New System.Drawing.Point(107, 7)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(59, 14)
        Me.lblFileName.TabIndex = 58
        Me.lblFileName.Text = "File Name"
        Me.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblFileName.Visible = False
        '
        'rbImages
        '
        Me.rbImages.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbImages.Location = New System.Drawing.Point(40, 80)
        Me.rbImages.Name = "rbImages"
        Me.rbImages.Size = New System.Drawing.Size(30, 24)
        Me.rbImages.TabIndex = 56
        Me.rbImages.Text = "I"
        Me.rbImages.Visible = False
        '
        'rbPDF
        '
        Me.rbPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rbPDF.Location = New System.Drawing.Point(8, 80)
        Me.rbPDF.Name = "rbPDF"
        Me.rbPDF.Size = New System.Drawing.Size(30, 24)
        Me.rbPDF.TabIndex = 55
        Me.rbPDF.Text = "P"
        Me.rbPDF.Visible = False
        '
        'c1Document
        '
        Me.c1Document.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1Document.BackColor = System.Drawing.Color.GhostWhite
        Me.c1Document.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.c1Document.ColumnInfo = "3,0,0,0,0,95,Columns:0{Width:150;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Width:100;}" & Global.Microsoft.VisualBasic.ChrW(9) & "2{Width:200;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1Document.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Document.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Document.ForeColor = System.Drawing.Color.Black
        Me.c1Document.Location = New System.Drawing.Point(0, 0)
        Me.c1Document.Name = "c1Document"
        Me.c1Document.Rows.DefaultSize = 19
        Me.c1Document.Rows.Fixed = 0
        Me.c1Document.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Document.Size = New System.Drawing.Size(400, 117)
        Me.c1Document.StyleInfo = resources.GetString("c1Document.StyleInfo")
        Me.c1Document.TabIndex = 52
        Me.c1Document.Tree.Column = 2
        Me.c1Document.Tree.Indent = 72
        Me.c1Document.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.c1Document.Tree.NodeImageCollapsed = CType(resources.GetObject("c1Document.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1Document.Tree.NodeImageExpanded = CType(resources.GetObject("c1Document.Tree.NodeImageExpanded"), System.Drawing.Image)
        Me.c1Document.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None
        Me.c1Document.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.txtFileName)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.btnAdd)
        Me.Panel2.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Controls.Add(Me.lbl_RightBrd)
        Me.Panel2.Controls.Add(Me.lbl_TopBrd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(400, 24)
        Me.Panel2.TabIndex = 42
        '
        'txtFileName
        '
        Me.txtFileName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtFileName.ForeColor = System.Drawing.Color.Black
        Me.txtFileName.Location = New System.Drawing.Point(100, 1)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(165, 22)
        Me.txtFileName.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "File Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAdd
        '
        Me.btnAdd.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(366, 1)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(33, 22)
        Me.btnAdd.TabIndex = 41
        Me.btnAdd.Text = "Add"
        Me.btnAdd.Visible = False
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(398, 1)
        Me.lbl_BottomBrd.TabIndex = 45
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 44
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(399, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 43
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(400, 1)
        Me.lbl_TopBrd.TabIndex = 42
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.lvwPages)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(408, 24)
        Me.Panel1.TabIndex = 53
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(406, 1)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 23)
        Me.Label8.TabIndex = 43
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(407, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 23)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(408, 1)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "label1"
        '
        'lvwPages
        '
        Me.lvwPages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvwPages.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwPages.ForeColor = System.Drawing.Color.Black
        Me.lvwPages.GridLines = True
        Me.lvwPages.HideSelection = False
        ListViewItem15.Checked = True
        ListViewItem15.StateImageIndex = 1
        ListViewItem16.Checked = True
        ListViewItem16.StateImageIndex = 1
        ListViewItem17.Checked = True
        ListViewItem17.StateImageIndex = 1
        ListViewItem18.Checked = True
        ListViewItem18.StateImageIndex = 1
        ListViewItem19.Checked = True
        ListViewItem19.StateImageIndex = 1
        ListViewItem20.Checked = True
        ListViewItem20.StateImageIndex = 1
        ListViewItem21.Checked = True
        ListViewItem21.StateImageIndex = 1
        ListViewItem22.Checked = True
        ListViewItem22.StateImageIndex = 1
        ListViewItem23.Checked = True
        ListViewItem23.StateImageIndex = 1
        ListViewItem24.Checked = True
        ListViewItem24.StateImageIndex = 1
        ListViewItem25.Checked = True
        ListViewItem25.StateImageIndex = 1
        ListViewItem26.Checked = True
        ListViewItem26.StateImageIndex = 1
        ListViewItem27.Checked = True
        ListViewItem27.StateImageIndex = 1
        ListViewItem28.Checked = True
        ListViewItem28.StateImageIndex = 1
        Me.lvwPages.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem15, ListViewItem16, ListViewItem17, ListViewItem18, ListViewItem19, ListViewItem20, ListViewItem21, ListViewItem22, ListViewItem23, ListViewItem24, ListViewItem25, ListViewItem26, ListViewItem27, ListViewItem28})
        Me.lvwPages.Location = New System.Drawing.Point(99, 4)
        Me.lvwPages.Name = "lvwPages"
        Me.lvwPages.Size = New System.Drawing.Size(22, 15)
        Me.lvwPages.TabIndex = 40
        Me.lvwPages.UseCompatibleStateImageBehavior = False
        Me.lvwPages.View = System.Windows.Forms.View.SmallIcon
        Me.lvwPages.Visible = False
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tls_VMSImportDocument)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(406, 54)
        Me.pnl_tlspTOP.TabIndex = 55
        '
        'tls_VMSImportDocument
        '
        Me.tls_VMSImportDocument.BackColor = System.Drawing.Color.Transparent
        Me.tls_VMSImportDocument.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_VMSImportDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_VMSImportDocument.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_VMSImportDocument.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_VMSImportDocument.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ts_btnOk, Me.ts_btnCancel})
        Me.tls_VMSImportDocument.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_VMSImportDocument.Location = New System.Drawing.Point(0, 0)
        Me.tls_VMSImportDocument.Name = "tls_VMSImportDocument"
        Me.tls_VMSImportDocument.Size = New System.Drawing.Size(406, 53)
        Me.tls_VMSImportDocument.TabIndex = 0
        Me.tls_VMSImportDocument.Text = "toolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(44, 50)
        Me.ToolStripButton1.Tag = "Add"
        Me.ToolStripButton1.Text = " &Add "
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 54)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel4.Size = New System.Drawing.Size(406, 30)
        Me.Panel4.TabIndex = 56
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Panel1)
        Me.Panel5.Location = New System.Drawing.Point(0, 181)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel5.Size = New System.Drawing.Size(414, 30)
        Me.Panel5.TabIndex = 57
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmVMS_ImportDocumentEvent
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(406, 204)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVMS_ImportDocumentEvent"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Upload Video"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlDocument.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.c1Document, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tls_VMSImportDocument.ResumeLayout(False)
        Me.tls_VMSImportDocument.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim WithEvents oVMS As gloStream.gloVMS.gloVMS
    Private _ProcessParameter As gloStream.gloVMS.Supporting.ImportProcessParameters
    Private Const COL_DOC_NAME = 0
    Private Const COL_DOC_SIZE = 1
    Private Const COL_DOC_PATH = 2
    Dim oProcessor As BEPPROCLib.PDFProcessor
    Public sImportDocumentPath As String = ""


    Public Property ProcessParameter() As gloStream.gloVMS.Supporting.ImportProcessParameters
        Get
            Return _ProcessParameter
        End Get
        Set(ByVal Value As gloStream.gloVMS.Supporting.ImportProcessParameters)
            _ProcessParameter = Value
        End Set
    End Property

    Private Sub frmVMS_CategoryDocumentEvent_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(c1Document)

        Try

            '2. Show Hide Control
            'txtSearchDocument.Text = ""

            Dim oSupporting As New gloStream.gloVMS.Supporting.Supporting

            txtFileName.Text = oSupporting.NewDocumentName(_ProcessParameter.PatientID, _ProcessParameter.Category, gloStream.gloDMS.Supporting.enumDocumentType.CategorisedDocument)

            oSupporting = Nothing

            '3. Progress Bar 
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = 100
            ProgressBar1.Value = 0
            ProgressBar1.Enabled = False
            rbPDF.Checked = True
        Catch oError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.DMS), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub OK_VMSImportDocument()
        oVMS = New gloStream.gloVMS.gloVMS(_PatientID)
        '  Dim oProcessParameter As New gloStream.gloVMS.Supporting.ImportProcessParameters
        Dim blnSuccess As Boolean = False
        Dim i As Integer
        Dim _Documents As New Collection

        Try
            'Me.DialogResult = DialogResult.Cancel
            ProgressBar1.Enabled = True
            ts_btnOk.Enabled = False
            ts_btnCancel.Enabled = False

            If Trim(txtFileName.Text) = "" Then
                MessageBox.Show("Please enter Video name to import into VMS", gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            End If

            If lblFileName.Text = "" And lblFileLength.Text = "" And lblfilePath.Text = "" Then
                MessageBox.Show("Please Select file to import into VMS", gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            End If

            Dim oDocument As New gloStream.gloVMS.Document.document
            If oDocument.FindDocument(txtFileName.Text.Replace("'", "''"), _ProcessParameter.PatientID, _ProcessParameter.Category, _ProcessParameter.DocumentType) = True Then
                MessageBox.Show("File with the same name already exists, please enter another name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            oDocument = Nothing

            _Documents.Add(lblfilePath.Text)


            Dim FileName As String = lblFileName.Text

            Dim fSourceFile As System.IO.FileInfo = New System.IO.FileInfo(lblfilePath.Text)

            'With oProcessParameter
            '    .PatientID = _ProcessParameter.PatientID
            '    .Category = _ProcessParameter.Category
            '    .Container = _ProcessParameter.Container
            '    .Month = _ProcessParameter.Month
            '    .Year = _ProcessParameter.Year
            _ProcessParameter.MediaFileDisplayName = Trim(txtFileName.Text)
            _ProcessParameter.Extension = fSourceFile.Extension
            _ProcessParameter.Documents = _Documents
            'End With


            blnSuccess = oVMS.SendToGeneralBin(_ProcessParameter)

            If blnSuccess Then
                oVMS.UploadVideo_DB(fSourceFile, _ProcessParameter, _PatientID)
                Me.Close()
            End If


        Catch oError As Exception
            If Trim(oVMS.ErrorMessage) <> "" Then
                MessageBox.Show(oVMS.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            Else
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, oError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(WriteExceptionLog(oError, mdlGeneral.gloEMRExceptionActorType.Video), gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            End If
        Finally

            ProgressBar1.Enabled = False
            ts_btnOk.Enabled = True
            ts_btnCancel.Enabled = True

            oVMS = Nothing
            ' oProcessParameter = Nothing
            blnSuccess = Nothing
            _Documents = Nothing
            i = Nothing
        End Try
    End Sub

    Private Function CleanGeneratePDFFromImages() As Boolean
        Dim _GeneratePath As String = Application.StartupPath & "\GeneratePDF"
        If Directory.Exists(_GeneratePath) = True Then
            Directory.Delete(_GeneratePath, True)
        End If
        Return Nothing
    End Function

    Private Sub c1Document_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1Document.EnterCell
        If c1Document.Rows.Count < 0 Then
            Exit Sub
        End If
        If c1Document.RowSel < 0 Then
            Exit Sub
        End If
        Dim nRow As Integer = c1Document.RowSel

        If nRow >= 0 Then
            With c1Document
                'txtSearchDocument.Text = Trim(.GetData(nRow, COL_DOC_NAME))
            End With
        End If
    End Sub

    Private Sub txtFileName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFileName.KeyUp
        On Error Resume Next
        Dim sFileName As String = Trim(txtFileName.Text)
        Dim sVldFileName As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(sFileName, "\", ""), "/", ""), ":", ""), "?", ""), "<", ""), ">", ""), "*", ""), ".", ""), """", "")
        If sFileName <> sVldFileName Then
            txtFileName.Text = sVldFileName
            txtFileName.SelectionStart = Len(txtFileName.Text)
        End If
    End Sub

    Private Sub oDMS_ReportPercentage(ByVal nPercentage As Integer) Handles oVMS.ReportPercentage
        ProgressBar1.Value = nPercentage
    End Sub

    Private Sub oDMS_ReportImportDocument(ByVal DocumentPath As String) Handles oVMS.ReportImportDocument
        sImportDocumentPath = DocumentPath
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click, ToolStripButton1.Click
        'Dim oFile As String
        Try

            'If lblFileName.Text <> "" And lblFileLength.Text <> "" And lblfilePath.Text <> "" Then
            '    Exit Sub
            'End If

            If openfiledilogVideo.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                If openfiledilogVideo.FileName.Length > 0 Then
                    Dim fSourceFile As System.IO.FileInfo = New System.IO.FileInfo(openfiledilogVideo.FileName)
                    lblFileName.Visible = True
                    lblFileLength.Visible = True
                    lblfilePath.Visible = True

                    lblFileNameLable.Visible = True
                    lblFileLengthlable.Visible = True
                    lblfilePathLable.Visible = True

                    lblFileName.Text = fSourceFile.Name
                    ' TextBox1.Text = fSourceFile.Name
                    If fSourceFile.Length > 1024 Then
                        lblFileLength.Text = (fSourceFile.Length / 1024) & " KB"
                    Else
                        lblFileLength.Text = (fSourceFile.Length) & " KB"
                    End If

                    lblfilePath.Text = fSourceFile.FullName
                    'Dim objgloVMS As New gloStream.gloVMS.gloVMS
                    'If objgloVMS.UploadVideo_Library(fSourceFile) Then
                    '    objgloVMS.UploadVideo_DB(fSourceFile)
                    'End If
                End If
            End If



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lblFileLength.Text = ""
        lblFileName.Text = ""
        lblfilePath.Text = ""
    End Sub

    Protected Overrides Sub Finalize()
        _ProcessParameter = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub tls_VMSImportDocument_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_VMSImportDocument.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    OK_VMSImportDocument()

                Case "Cancel"
                    Me.Close()

            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.VMS, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub c1Document_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1Document.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

  
End Class
