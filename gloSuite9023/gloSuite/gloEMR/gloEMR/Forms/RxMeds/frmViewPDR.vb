Imports System.Data
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloGeneral
Imports System.ComponentModel

Public Class frmViewPDR
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        ConnectToPDFTron()
        dbLayer = New PatientCommunicationBusinessLayer()
    End Sub


    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                'If (IsNothing(dgAttemptDetails) = False) Then
                '    dgAttemptDetails.TableStyles.Clear()
                '    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgAttemptDetails)
                '    dgAttemptDetails.Dispose()
                '    dgAttemptDetails = Nothing
                'End If
            Catch ex As Exception

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
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    'Removed PegausImageXpress7 -> Dhruv
    'Friend WithEvents picPreview As PegasusImaging.WinForms.ImagXpress7.PICImagXpress
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuFirst As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPrevious As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNext As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLast As System.Windows.Forms.MenuItem
    Friend WithEvents pnlAttemptsDetailsBody As System.Windows.Forms.Panel
    Friend WithEvents pnlAttemptsDetailsHeader As System.Windows.Forms.Panel
    Friend WithEvents tblBtn As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tblbtnClose As System.Windows.Forms.ToolStripButton
    'Friend WithEvents AxPdfVw As AxXpdfViewer.AxXpdfViewer
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgPatientCommunication As System.Windows.Forms.DataGridView
    Friend WithEvents pnlflexgrid As System.Windows.Forms.Panel
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents label17 As System.Windows.Forms.Label
    Private WithEvents lblGridBottom As System.Windows.Forms.Label
    Private WithEvents lblGridLeft As System.Windows.Forms.Label
    Private WithEvents lblGridRight As System.Windows.Forms.Label
    Private WithEvents lblGridTop As System.Windows.Forms.Label
    Friend WithEvents ProgramDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Medication As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProgramName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Img As System.Windows.Forms.ImageList
    'Friend WithEvents ImagXpress1 As PegasusImaging.WinForms.ImagXpress9.ImagXpress
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewPDR))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tblBtn = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tblbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlAttemptsDetailsBody = New System.Windows.Forms.Panel()
        Me.dgPatientCommunication = New System.Windows.Forms.DataGridView()
        Me.ProgramDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Medication = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProgramName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.pnlflexgrid = New System.Windows.Forms.Panel()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.label17 = New System.Windows.Forms.Label()
        Me.lblGridBottom = New System.Windows.Forms.Label()
        Me.lblGridLeft = New System.Windows.Forms.Label()
        Me.lblGridRight = New System.Windows.Forms.Label()
        Me.lblGridTop = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnlAttemptsDetailsHeader = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlPreview = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.mnuFirst = New System.Windows.Forms.MenuItem()
        Me.mnuPrevious = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuNext = New System.Windows.Forms.MenuItem()
        Me.mnuLast = New System.Windows.Forms.MenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Img = New System.Windows.Forms.ImageList(Me.components)
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlToolStrip.SuspendLayout()
        Me.tblBtn.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlAttemptsDetailsBody.SuspendLayout()
        CType(Me.dgPatientCommunication, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlflexgrid.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlAttemptsDetailsHeader.SuspendLayout()
        Me.pnlPreview.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.tblBtn)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1028, 56)
        Me.pnlToolStrip.TabIndex = 0
        '
        'tblBtn
        '
        Me.tblBtn.BackColor = System.Drawing.Color.Transparent
        Me.tblBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblBtn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblBtn.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblBtn.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPrint, Me.ToolStripSeparator1, Me.tblbtnClose})
        Me.tblBtn.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblBtn.Location = New System.Drawing.Point(0, 0)
        Me.tblBtn.Name = "tblBtn"
        Me.tblBtn.Size = New System.Drawing.Size(1028, 53)
        Me.tblBtn.TabIndex = 0
        Me.tblBtn.Text = "ToolStrip1"
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.btnPrint.Tag = "Back"
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.AutoSize = False
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 53)
        Me.ToolStripSeparator1.Visible = False
        '
        'tblbtnClose
        '
        Me.tblbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnClose.Image = CType(resources.GetObject("tblbtnClose.Image"), System.Drawing.Image)
        Me.tblbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnClose.Name = "tblbtnClose"
        Me.tblbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tblbtnClose.Tag = "Close"
        Me.tblbtnClose.Text = "&Close"
        Me.tblbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlAttemptsDetailsBody)
        Me.pnlMain.Controls.Add(Me.pnlflexgrid)
        Me.pnlMain.Controls.Add(Me.Panel4)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlMain.Size = New System.Drawing.Size(297, 690)
        Me.pnlMain.TabIndex = 2
        '
        'pnlAttemptsDetailsBody
        '
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.dgPatientCommunication)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label18)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label19)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label20)
        Me.pnlAttemptsDetailsBody.Controls.Add(Me.Label21)
        Me.pnlAttemptsDetailsBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAttemptsDetailsBody.Location = New System.Drawing.Point(0, 97)
        Me.pnlAttemptsDetailsBody.Name = "pnlAttemptsDetailsBody"
        Me.pnlAttemptsDetailsBody.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlAttemptsDetailsBody.Size = New System.Drawing.Size(297, 593)
        Me.pnlAttemptsDetailsBody.TabIndex = 19
        '
        'dgPatientCommunication
        '
        Me.dgPatientCommunication.AllowUserToAddRows = False
        Me.dgPatientCommunication.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.dgPatientCommunication.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgPatientCommunication.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgPatientCommunication.BackgroundColor = System.Drawing.Color.White
        Me.dgPatientCommunication.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(217, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgPatientCommunication.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgPatientCommunication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgPatientCommunication.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ProgramDate, Me.Medication, Me.ProgramName})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgPatientCommunication.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgPatientCommunication.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPatientCommunication.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgPatientCommunication.EnableHeadersVisualStyles = False
        Me.dgPatientCommunication.GridColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgPatientCommunication.Location = New System.Drawing.Point(4, 1)
        Me.dgPatientCommunication.MultiSelect = False
        Me.dgPatientCommunication.Name = "dgPatientCommunication"
        Me.dgPatientCommunication.RowHeadersVisible = False
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.dgPatientCommunication.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgPatientCommunication.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgPatientCommunication.Size = New System.Drawing.Size(292, 588)
        Me.dgPatientCommunication.TabIndex = 9
        '
        'ProgramDate
        '
        Me.ProgramDate.DataPropertyName = "CreatedDate"
        Me.ProgramDate.HeaderText = "Date"
        Me.ProgramDate.Name = "ProgramDate"
        Me.ProgramDate.ReadOnly = True
        '
        'Medication
        '
        Me.Medication.DataPropertyName = "Medication"
        Me.Medication.HeaderText = "Medication"
        Me.Medication.Name = "Medication"
        Me.Medication.ReadOnly = True
        '
        'ProgramName
        '
        Me.ProgramName.DataPropertyName = "Name"
        Me.ProgramName.HeaderText = "Program"
        Me.ProgramName.Name = "ProgramName"
        Me.ProgramName.ReadOnly = True
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(4, 589)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(292, 1)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 589)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(296, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 589)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label3"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(3, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(294, 1)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "label1"
        '
        'pnlflexgrid
        '
        Me.pnlflexgrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlflexgrid.Controls.Add(Me.dtFrom)
        Me.pnlflexgrid.Controls.Add(Me.dtTo)
        Me.pnlflexgrid.Controls.Add(Me.Label2)
        Me.pnlflexgrid.Controls.Add(Me.label17)
        Me.pnlflexgrid.Controls.Add(Me.lblGridBottom)
        Me.pnlflexgrid.Controls.Add(Me.lblGridLeft)
        Me.pnlflexgrid.Controls.Add(Me.lblGridRight)
        Me.pnlflexgrid.Controls.Add(Me.lblGridTop)
        Me.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlflexgrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlflexgrid.Location = New System.Drawing.Point(0, 33)
        Me.pnlflexgrid.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlflexgrid.Name = "pnlflexgrid"
        Me.pnlflexgrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlflexgrid.Size = New System.Drawing.Size(297, 64)
        Me.pnlflexgrid.TabIndex = 24
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(93, 33)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(93, 21)
        Me.dtTo.TabIndex = 232
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 14)
        Me.Label2.TabIndex = 231
        Me.Label2.Text = "End Date :"
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.Location = New System.Drawing.Point(18, 9)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(72, 14)
        Me.label17.TabIndex = 229
        Me.label17.Text = "Start Date :"
        '
        'lblGridBottom
        '
        Me.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblGridBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridBottom.Location = New System.Drawing.Point(4, 60)
        Me.lblGridBottom.Name = "lblGridBottom"
        Me.lblGridBottom.Size = New System.Drawing.Size(289, 1)
        Me.lblGridBottom.TabIndex = 10
        Me.lblGridBottom.Text = "label2"
        '
        'lblGridLeft
        '
        Me.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblGridLeft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridLeft.Location = New System.Drawing.Point(3, 1)
        Me.lblGridLeft.Name = "lblGridLeft"
        Me.lblGridLeft.Size = New System.Drawing.Size(1, 60)
        Me.lblGridLeft.TabIndex = 9
        Me.lblGridLeft.Text = "label4"
        '
        'lblGridRight
        '
        Me.lblGridRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblGridRight.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridRight.Location = New System.Drawing.Point(293, 1)
        Me.lblGridRight.Name = "lblGridRight"
        Me.lblGridRight.Size = New System.Drawing.Size(1, 60)
        Me.lblGridRight.TabIndex = 8
        Me.lblGridRight.Text = "label3"
        '
        'lblGridTop
        '
        Me.lblGridTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblGridTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridTop.Location = New System.Drawing.Point(3, 0)
        Me.lblGridTop.Name = "lblGridTop"
        Me.lblGridTop.Size = New System.Drawing.Size(291, 1)
        Me.lblGridTop.TabIndex = 7
        Me.lblGridTop.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.pnlAttemptsDetailsHeader)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(297, 30)
        Me.Panel4.TabIndex = 23
        '
        'pnlAttemptsDetailsHeader
        '
        Me.pnlAttemptsDetailsHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlAttemptsDetailsHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlAttemptsDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_RightBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.lbl_TopBrd)
        Me.pnlAttemptsDetailsHeader.Controls.Add(Me.Label1)
        Me.pnlAttemptsDetailsHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAttemptsDetailsHeader.Location = New System.Drawing.Point(3, 0)
        Me.pnlAttemptsDetailsHeader.Name = "pnlAttemptsDetailsHeader"
        Me.pnlAttemptsDetailsHeader.Size = New System.Drawing.Size(294, 27)
        Me.pnlAttemptsDetailsHeader.TabIndex = 21
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 26)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(292, 1)
        Me.lbl_BottomBrd.TabIndex = 13
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 26)
        Me.lbl_LeftBrd.TabIndex = 12
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(293, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 26)
        Me.lbl_RightBrd.TabIndex = 11
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(294, 1)
        Me.lbl_TopBrd.TabIndex = 10
        Me.lbl_TopBrd.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(294, 27)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "   Medication with patient communication"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPreview
        '
        Me.pnlPreview.BackColor = System.Drawing.Color.Transparent
        Me.pnlPreview.Controls.Add(Me.Panel1)
        Me.pnlPreview.Controls.Add(Me.Label26)
        Me.pnlPreview.Controls.Add(Me.Label28)
        Me.pnlPreview.Controls.Add(Me.Label27)
        Me.pnlPreview.Controls.Add(Me.Label29)
        Me.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPreview.ForeColor = System.Drawing.Color.Transparent
        Me.pnlPreview.Location = New System.Drawing.Point(301, 56)
        Me.pnlPreview.Name = "pnlPreview"
        Me.pnlPreview.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlPreview.Size = New System.Drawing.Size(727, 690)
        Me.pnlPreview.TabIndex = 18
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(722, 682)
        Me.Panel1.TabIndex = 27
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(1, 686)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(722, 1)
        Me.Label26.TabIndex = 26
        Me.Label26.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(723, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 683)
        Me.Label28.TabIndex = 24
        Me.Label28.Text = "label3"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 683)
        Me.Label27.TabIndex = 25
        Me.Label27.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 3)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(724, 1)
        Me.Label29.TabIndex = 23
        Me.Label29.Text = "label1"
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFirst, Me.mnuPrevious, Me.MenuItem3, Me.mnuNext, Me.mnuLast})
        '
        'mnuFirst
        '
        Me.mnuFirst.Index = 0
        Me.mnuFirst.Text = "First"
        '
        'mnuPrevious
        '
        Me.mnuPrevious.Index = 1
        Me.mnuPrevious.Text = "Previous"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "-"
        '
        'mnuNext
        '
        Me.mnuNext.Index = 3
        Me.mnuNext.Text = "Next"
        '
        'mnuLast
        '
        Me.mnuLast.Index = 4
        Me.mnuLast.Text = "Last"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(297, 56)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 690)
        Me.Splitter1.TabIndex = 24
        Me.Splitter1.TabStop = False
        '
        'Img
        '
        Me.Img.ImageStream = CType(resources.GetObject("Img.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.Img.TransparentColor = System.Drawing.Color.Transparent
        Me.Img.Images.SetKeyName(0, "Drug.ico")
        Me.Img.Images.SetKeyName(1, "Pdf.ico")
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(93, 6)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(93, 21)
        Me.dtFrom.TabIndex = 233
        '
        'frmViewPDR
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.pnlPreview)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewPDR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Physician's Desk Reference"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tblBtn.ResumeLayout(False)
        Me.tblBtn.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlAttemptsDetailsBody.ResumeLayout(False)
        CType(Me.dgPatientCommunication, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlflexgrid.ResumeLayout(False)
        Me.pnlflexgrid.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.pnlAttemptsDetailsHeader.ResumeLayout(False)
        Me.pnlPreview.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Properties and Variables"

    Dim dbLayer As PatientCommunicationBusinessLayer = Nothing

    Private nPatientID As Int64
    Public Property PatientID() As Int64
        Get
            Return nPatientID
        End Get
        Set(ByVal value As Int64)
            nPatientID = value
        End Set
    End Property

    Private Shared myPrinterSetting As System.Drawing.Printing.PrinterSettings = Nothing
    Private CurrentProgram As PCProgram = Nothing

    Private WithEvents PatientStripControl As gloUserControlLibrary.gloUC_PatientStrip

    Dim _fileExtension As String = "pdf"
    Dim oPDFView As pdftron.PDF.PDFViewCtrl
    Dim oPDFDoc As pdftron.PDF.PDFDoc

    Dim TransactionID As Int32 = 0
    Dim lstPrograms As BindingList(Of PCProgram) = Nothing

#End Region

#Region "Constructor"

    Public Sub New(ByVal PatientID As Int64)
        Me.New()
        Me.PatientID = PatientID
    End Sub

    Public Sub New(ByVal PatientID As Int64, ByVal TransactionID As Int32)
        Me.New(PatientID)
        Me.TransactionID = TransactionID
    End Sub
#End Region

#Region "Form Events"
    
    Private Sub frmPendingFAXPreview_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If IsNothing(oPDFView) = False Then
                DisconnectToPDFTron()
            End If

            If Me.dbLayer IsNot Nothing Then
                Me.dbLayer.Dispose()
                Me.dbLayer = Nothing
            End If

            If Me.CurrentProgram IsNot Nothing Then
                Me.CurrentProgram.Dispose()
                Me.CurrentProgram = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmPendingFAXPreview_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.LoadPatientStripControl()
            Me.dtFrom.Value = Date.Now.AddDays(-7)
            Me.FillPatientCommunication()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "PDFTron functionality"

    Public Shared Sub ConnectToPDFTron()
        If gloEDocumentV3.gloEDocV3Admin.gIsPDFTronConnected = True Then
            DisconnectToPDFTron()
        End If

        Try
            '' New Licenece Added on 20120926
            ' pdftron.PDFNet.Initialize("gloStream, Inc.(glostream.com):OEM:gloEMR::W:AMC(20110602):7DE6A118A47A49B951EDEC194A2640E528387DE495B2C9112BD15C49D07AF0FA")
            pdftron.PDFNet.Initialize("gloStream, Inc.(glostream.com):OEM:gloEMR::W:AMC(20130603):4DE63118A4FA49B931EDEC194A2640E528387DE495B2C9112BD15C49D07AF0FA")
            ''

            '' Commented on 20100907 Licence Expired
            ' pdftron.PDFNet.Initialize("gloStream Inc.(glostream.com):CPU:1:E:W:AMC(20091008):69F4EAB1559D7A21E3C841109C92CA1D944C4B5A192BD15C49D07AF0FA")
            ''
            'pdftron.PDFNet.Initialize("gloStream Inc. (glostream.com):CPU:1:E:W:AMC(20081108):3CF4F371559DFA2163C846109C92BA7DE4C54B5A192BD15C49D07AF0FA");
            Dim strResourcePath As String = Application.StartupPath & "\\pdfnet.res"
            pdftron.PDFNet.SetResourcesPath(strResourcePath)
        Catch ex As pdftron.Common.PDFNetException

        End Try
        'End Code Added on 20091015
    End Sub

    Public Shared Sub DisconnectToPDFTron()
        Try
            pdftron.PDFNet.Terminate()
            gloEDocumentV3.gloEDocV3Admin.gIsPDFTronConnected = False
        Catch ex As pdftron.Common.PDFNetException

        End Try
    End Sub

#End Region

#Region "Printing and Viewing Functionality"

    Private Sub LoadProgram(ByVal PCProgram As PCProgram)
        Dim sWrittenFile As String = String.Empty
        Dim sBase64 As String = String.Empty

        Try
            If PCProgram.FileWritten Then
                sWrittenFile = PCProgram.FilePath
            Else
                sBase64 = dbLayer.GetPDRProgram(PCProgram.ID)
                sWrittenFile = Me.WriteFile(sBase64)
                PCProgram.FilePath = sWrittenFile
            End If
            Me.CurrentProgram = PCProgram
            Me.ViewProgram(sWrittenFile)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            sWrittenFile = String.Empty
            sBase64 = String.Empty
        End Try
    End Sub

    Private Function WriteFile(ByVal Base64String As String) As String
        Dim sTempFile As String = String.Empty

        Try
            sTempFile = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, "." & _fileExtension, "yyyyMMddHHmmssffff")

            If (File.Exists(sTempFile)) Then
                File.Delete(sTempFile)
            End If
            File.WriteAllBytes(sTempFile, System.Convert.FromBase64String(Base64String))
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return sTempFile
    End Function

    Private Sub ViewProgram(ByVal FilePath As String)
        Try
            If oPDFView Is Nothing Then
                oPDFView = New pdftron.PDF.PDFViewCtrl()
            End If

            oPDFDoc = New pdftron.PDF.PDFDoc(FilePath)  'myStrFileName
            If oPDFView Is Nothing Then
                oPDFView = New pdftron.PDF.PDFViewCtrl()
            End If

            oPDFView.Show()
            oPDFView.SetDoc(oPDFDoc)
            pnlPreview.Controls.Add(oPDFView)
            oPDFView.Dock = DockStyle.Fill
            oPDFView.BringToFront()
            oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page)

            oPDFView.SetCaching(True)
            oPDFView.SetProgressiveRendering(True)
            oPDFView.Visible = True
            oPDFView.Refresh()
            oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page)
            oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width)

            If (oPDFView.GotoFirstPage() = True) Then
                oPDFView.GetSelectionBeginPage()
            End If
            oPDFView.EnableInteractiveForms(False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub PrintProgrammes(ByVal FilePath As String)
        Try
            Dim ogloPrintProgressController As gloPrintDialog.gloPrintProgressController = Nothing
            myPrinterSetting = New System.Drawing.Printing.PrinterSettings()

            Dim doc As New pdftron.PDF.PDFDoc(FilePath)

            Using oDialog As New gloPrintDialog.gloPrintDialog()
                oDialog.ConnectionString = GetConnectionString()
                oDialog.TopMost = True
                oDialog.ShowPrinterProfileDialog = True

                If oDialog IsNot Nothing Then

                    doc.Lock()
                    Dim maxPage As Integer = doc.GetPageCount()

                    If Not gloGlobal.gloTSPrint.isCopyPrint Then
                        oDialog.PrinterSettings = myPrinterSetting

                        oDialog.AllowSomePages = True

                        oDialog.PrinterSettings.ToPage = maxPage
                        oDialog.PrinterSettings.FromPage = 1
                        oDialog.PrinterSettings.MaximumPage = maxPage
                        oDialog.PrinterSettings.MinimumPage = 1
                    End If

                    If oDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                        If Not gloGlobal.gloTSPrint.isCopyPrint Then
                            myPrinterSetting = oDialog.PrinterSettings
                        End If
                        ogloPrintProgressController = New gloPrintDialog.gloPrintProgressController(doc, doc.GetFileName(), oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, blnautoLandScape:=False)
                        ogloPrintProgressController.ShowProgress(Me)
                    End If
                    doc.Unlock()
                Else
                    Dim _ErrorMessage As String = "Error in Showing Print Dialog"

                    If _ErrorMessage.Trim() <> "" Then
                        Dim _MessageString As String = System.Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") & _ErrorMessage
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString)
                        _MessageString = ""
                    End If
                End If

            End Using

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, _
                                                   gloAuditTrail.ActivityCategory.PatientCommunication, _
                                                   gloAuditTrail.ActivityType.Initialize, _
                                                   "Error in PrintProgrammes function" + ex.ToString(), _
                                                   gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

#End Region

#Region "Load data"

    Private Sub LoadPatientStripControl()
        Try
            If IsNothing(PatientStripControl) Then
                PatientStripControl = New gloUserControlLibrary.gloUC_PatientStrip

                With PatientStripControl
                    .Dock = DockStyle.Top
                    .Padding = New Padding(3, 0, 3, 0)
                    .ShowDetail(PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.RxFillNotifications)
                    .BringToFront()
                End With
                Me.Controls.Add(PatientStripControl)
                pnlToolStrip.SendToBack()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillPatientCommunication()
        Dim dsPatComm As DataSet = Nothing
        Dim dtPrograms As DataTable = Nothing        
        Try
            If dbLayer Is Nothing Then
                dbLayer = New PatientCommunicationBusinessLayer()
            End If

            dsPatComm = dbLayer.GetPDRPrograms(PatientID, dtFrom.Value, dtTo.Value)
            dtPrograms = dsPatComm.Tables(0)

            If Me.lstPrograms Is Nothing Then
                Me.lstPrograms = New BindingList(Of PCProgram)
            End If

            lstPrograms.Clear()           
            For Each element As PCProgram In dtPrograms.AsEnumerable() _
                                 .Select(Function(a) New PCProgram() With
                                                     {.Name = a("sProgramName"),
                                                            .ProgramID = a("nProgramID"),
                                                               .ID = a("nID"),
                                                                .Medication = a("sMedication"),
                                                                    .CreatedDate = a("dtCreatedDate"),
                                                                        .TransactionID = a("nTransactionID")})

                lstPrograms.Add(element)

            Next

            Me.dgPatientCommunication.AutoGenerateColumns = False
            Me.dgPatientCommunication.DataSource = Nothing
            If lstPrograms.Any() Then
                Me.dgPatientCommunication.DataSource = lstPrograms
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If dsPatComm IsNot Nothing Then
                dsPatComm.Dispose()
                dsPatComm = Nothing
            End If


            If dtPrograms IsNot Nothing Then
                dtPrograms.Dispose()
                dtPrograms = Nothing
            End If
        End Try
    End Sub

#End Region

#Region "Toolbar and Gridview button clicks"

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.CurrentProgram IsNot Nothing Then
                Me.PrintProgrammes(CurrentProgram.FilePath)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tblbtnClose_Click(sender As System.Object, e As System.EventArgs) Handles tblbtnClose.Click
        Me.Close()
    End Sub

    Private Sub dtTo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtTo.ValueChanged, dtFrom.ValueChanged
        Me.FillPatientCommunication()
    End Sub

    Private Sub dgPatientCommunication_SelectionChanged(sender As System.Object, e As System.EventArgs) Handles dgPatientCommunication.SelectionChanged
        Dim PCProg As PCProgram = Nothing
        Try
            If dgPatientCommunication.CurrentRow IsNot Nothing AndAlso TypeOf (dgPatientCommunication.CurrentRow.DataBoundItem) Is PCProgram Then
                PCProg = DirectCast(dgPatientCommunication.CurrentRow.DataBoundItem, PCProgram)

                If Not Me.CurrentProgram Is PCProg Then
                    Me.LoadProgram(PCProg)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            PCProg = Nothing
        End Try
    End Sub

#End Region

#Region "Private classes"

    Private Class PCProgram
        Implements IDisposable

        Private bFileWritten As Boolean
        Public ReadOnly Property FileWritten() As Boolean
            Get
                Return bFileWritten
            End Get
        End Property


        Private sFilePath As String
        Public Property FilePath() As String
            Get
                Return sFilePath
            End Get
            Set(ByVal value As String)
                If Not String.IsNullOrWhiteSpace(value) Then
                    Me.bFileWritten = True
                End If
                sFilePath = value
            End Set
        End Property

        Private sMedication As String
        Public Property Medication() As String
            Get
                Return sMedication
            End Get
            Set(ByVal value As String)
                sMedication = value
            End Set
        End Property


        Private dtCreatedDate As DateTime
        Public Property CreatedDate() As DateTime
            Get
                Return dtCreatedDate
            End Get
            Set(ByVal value As DateTime)
                dtCreatedDate = value
            End Set
        End Property


        Private nID As Int64
        Public Property ID() As Int64
            Get
                Return nID
            End Get
            Set(ByVal value As Int64)
                nID = value
            End Set
        End Property


        'Private nMPID As Int32
        'Public Property MPID() As Int32
        '    Get
        '        Return nMPID
        '    End Get
        '    Set(ByVal value As Int32)
        '        nMPID = value
        '    End Set
        'End Property

        Private sName As String
        Public Property Name() As String
            Get
                Return sName
            End Get
            Set(ByVal value As String)
                sName = value
            End Set
        End Property

        Private nProgamID As Int64
        Public Property ProgramID() As Int64
            Get
                Return nProgamID
            End Get
            Set(ByVal value As Int64)
                nProgamID = value
            End Set
        End Property

        Private nTransactionID As Int64
        Public Property TransactionID() As Int64
            Get
                Return nTransactionID
            End Get
            Set(ByVal value As Int64)
                nTransactionID = value
            End Set
        End Property


#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then

                    Me.FilePath = String.Empty
                    Me.ID = 0
                    Me.Name = String.Empty
                    Me.ProgramID = 0
                    Me.TransactionID = 0
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

#End Region



End Class




