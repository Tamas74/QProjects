Imports System.Data.SqlClient
Public Class frmMergePatientRecords
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbPatientToMerge As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPatientToMergeIn As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblSSN_Source As System.Windows.Forms.Label
    Friend WithEvents lblProvider_Source As System.Windows.Forms.Label
    Friend WithEvents lblDOB_Source As System.Windows.Forms.Label
    Friend WithEvents lblNoofExams_Source As System.Windows.Forms.Label
    Friend WithEvents lblNoofExam_Destination As System.Windows.Forms.Label
    Friend WithEvents lblDOB_Destination As System.Windows.Forms.Label
    Friend WithEvents lblProvider_Destination As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp As System.Windows.Forms.Panel
    Private WithEvents tlsp_MergePatientRecords As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnMerge As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pnlNote As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblPatientToMerge As System.Windows.Forms.Label
    Friend WithEvents lblPatientToMergeIn As System.Windows.Forms.Label
    Friend WithEvents ChkDemoGraphics As System.Windows.Forms.CheckBox
    Friend WithEvents btnBrowsepatientSource As System.Windows.Forms.Button
    Friend WithEvents btnBrowsepatientDest As System.Windows.Forms.Button
    Friend WithEvents pnlPatientList As System.Windows.Forms.Panel
    Friend WithEvents pnlStart As System.Windows.Forms.Panel
    Friend WithEvents lblSSN_Destination As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMergePatientRecords))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.ChkDemoGraphics = New System.Windows.Forms.CheckBox
        Me.btnBrowsepatientSource = New System.Windows.Forms.Button
        Me.lblPatientToMerge = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblNoofExams_Source = New System.Windows.Forms.Label
        Me.lblDOB_Source = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblProvider_Source = New System.Windows.Forms.Label
        Me.lblSSN_Source = New System.Windows.Forms.Label
        Me.cmbPatientToMerge = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblNoofExam_Destination = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblDOB_Destination = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblProvider_Destination = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblSSN_Destination = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.cmbPatientToMergeIn = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnl_tlsp = New System.Windows.Forms.Panel
        Me.tlsp_MergePatientRecords = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnMerge = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnBrowsepatientDest = New System.Windows.Forms.Button
        Me.lblPatientToMergeIn = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.pnlNote = New System.Windows.Forms.Panel
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.pnlPatientList = New System.Windows.Forms.Panel
        Me.pnlStart = New System.Windows.Forms.Panel
        Me.pnlMain.SuspendLayout()
        Me.pnl_tlsp.SuspendLayout()
        Me.tlsp_MergePatientRecords.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlNote.SuspendLayout()
        Me.pnlStart.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.ChkDemoGraphics)
        Me.pnlMain.Controls.Add(Me.btnBrowsepatientSource)
        Me.pnlMain.Controls.Add(Me.lblPatientToMerge)
        Me.pnlMain.Controls.Add(Me.Label20)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.lblNoofExams_Source)
        Me.pnlMain.Controls.Add(Me.lblDOB_Source)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.lblProvider_Source)
        Me.pnlMain.Controls.Add(Me.lblSSN_Source)
        Me.pnlMain.Controls.Add(Me.cmbPatientToMerge)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMain.Location = New System.Drawing.Point(0, 44)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(713, 149)
        Me.pnlMain.TabIndex = 0
        '
        'ChkDemoGraphics
        '
        Me.ChkDemoGraphics.AutoSize = True
        Me.ChkDemoGraphics.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDemoGraphics.Location = New System.Drawing.Point(444, 5)
        Me.ChkDemoGraphics.Name = "ChkDemoGraphics"
        Me.ChkDemoGraphics.Size = New System.Drawing.Size(161, 18)
        Me.ChkDemoGraphics.TabIndex = 36
        Me.ChkDemoGraphics.Text = "Include Demographics"
        Me.ChkDemoGraphics.UseVisualStyleBackColor = True
        Me.ChkDemoGraphics.Visible = False
        '
        'btnBrowsepatientSource
        '
        Me.btnBrowsepatientSource.BackgroundImage = CType(resources.GetObject("btnBrowsepatientSource.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsepatientSource.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsepatientSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsepatientSource.Image = CType(resources.GetObject("btnBrowsepatientSource.Image"), System.Drawing.Image)
        Me.btnBrowsepatientSource.Location = New System.Drawing.Point(682, 26)
        Me.btnBrowsepatientSource.Name = "btnBrowsepatientSource"
        Me.btnBrowsepatientSource.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowsepatientSource.TabIndex = 28
        Me.btnBrowsepatientSource.UseVisualStyleBackColor = True
        '
        'lblPatientToMerge
        '
        Me.lblPatientToMerge.BackColor = System.Drawing.Color.White
        Me.lblPatientToMerge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPatientToMerge.ForeColor = System.Drawing.Color.Black
        Me.lblPatientToMerge.Location = New System.Drawing.Point(348, 26)
        Me.lblPatientToMerge.Name = "lblPatientToMerge"
        Me.lblPatientToMerge.Size = New System.Drawing.Size(328, 20)
        Me.lblPatientToMerge.TabIndex = 27
        Me.lblPatientToMerge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(4, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(119, 14)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "  Patient Record 1"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 145)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(705, 1)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 142)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(709, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 142)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(707, 1)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "label1"
        '
        'lblNoofExams_Source
        '
        Me.lblNoofExams_Source.BackColor = System.Drawing.Color.White
        Me.lblNoofExams_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNoofExams_Source.ForeColor = System.Drawing.Color.Black
        Me.lblNoofExams_Source.Location = New System.Drawing.Point(348, 116)
        Me.lblNoofExams_Source.Name = "lblNoofExams_Source"
        Me.lblNoofExams_Source.Size = New System.Drawing.Size(328, 20)
        Me.lblNoofExams_Source.TabIndex = 20
        Me.lblNoofExams_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDOB_Source
        '
        Me.lblDOB_Source.BackColor = System.Drawing.Color.White
        Me.lblDOB_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDOB_Source.Location = New System.Drawing.Point(556, 56)
        Me.lblDOB_Source.Name = "lblDOB_Source"
        Me.lblDOB_Source.Size = New System.Drawing.Size(120, 20)
        Me.lblDOB_Source.TabIndex = 18
        Me.lblDOB_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(512, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 14)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "DOB :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProvider_Source
        '
        Me.lblProvider_Source.BackColor = System.Drawing.Color.White
        Me.lblProvider_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProvider_Source.ForeColor = System.Drawing.Color.Black
        Me.lblProvider_Source.Location = New System.Drawing.Point(348, 86)
        Me.lblProvider_Source.Name = "lblProvider_Source"
        Me.lblProvider_Source.Size = New System.Drawing.Size(328, 20)
        Me.lblProvider_Source.TabIndex = 16
        Me.lblProvider_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSSN_Source
        '
        Me.lblSSN_Source.BackColor = System.Drawing.Color.White
        Me.lblSSN_Source.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSSN_Source.ForeColor = System.Drawing.Color.Black
        Me.lblSSN_Source.Location = New System.Drawing.Point(348, 56)
        Me.lblSSN_Source.Name = "lblSSN_Source"
        Me.lblSSN_Source.Size = New System.Drawing.Size(120, 20)
        Me.lblSSN_Source.TabIndex = 13
        Me.lblSSN_Source.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbPatientToMerge
        '
        Me.cmbPatientToMerge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPatientToMerge.ForeColor = System.Drawing.Color.Black
        Me.cmbPatientToMerge.Location = New System.Drawing.Point(348, 24)
        Me.cmbPatientToMerge.Name = "cmbPatientToMerge"
        Me.cmbPatientToMerge.Size = New System.Drawing.Size(328, 22)
        Me.cmbPatientToMerge.TabIndex = 10
        Me.cmbPatientToMerge.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(263, 119)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 14)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "No of Exams :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(287, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Provider :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(309, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "SSN :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(8, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(338, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Select patient record you wish to remove from the system :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNoofExam_Destination
        '
        Me.lblNoofExam_Destination.BackColor = System.Drawing.Color.White
        Me.lblNoofExam_Destination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNoofExam_Destination.Location = New System.Drawing.Point(348, 114)
        Me.lblNoofExam_Destination.Name = "lblNoofExam_Destination"
        Me.lblNoofExam_Destination.Size = New System.Drawing.Size(328, 20)
        Me.lblNoofExam_Destination.TabIndex = 28
        Me.lblNoofExam_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(263, 117)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(83, 14)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "No of Exams :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDOB_Destination
        '
        Me.lblDOB_Destination.BackColor = System.Drawing.Color.White
        Me.lblDOB_Destination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDOB_Destination.Location = New System.Drawing.Point(556, 56)
        Me.lblDOB_Destination.Name = "lblDOB_Destination"
        Me.lblDOB_Destination.Size = New System.Drawing.Size(120, 20)
        Me.lblDOB_Destination.TabIndex = 26
        Me.lblDOB_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(512, 59)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 14)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "DOB :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProvider_Destination
        '
        Me.lblProvider_Destination.BackColor = System.Drawing.Color.White
        Me.lblProvider_Destination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProvider_Destination.Location = New System.Drawing.Point(348, 84)
        Me.lblProvider_Destination.Name = "lblProvider_Destination"
        Me.lblProvider_Destination.Size = New System.Drawing.Size(328, 20)
        Me.lblProvider_Destination.TabIndex = 24
        Me.lblProvider_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(287, 87)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(59, 14)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "Provider :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSSN_Destination
        '
        Me.lblSSN_Destination.BackColor = System.Drawing.Color.White
        Me.lblSSN_Destination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSSN_Destination.Location = New System.Drawing.Point(348, 56)
        Me.lblSSN_Destination.Name = "lblSSN_Destination"
        Me.lblSSN_Destination.Size = New System.Drawing.Size(120, 20)
        Me.lblSSN_Destination.TabIndex = 22
        Me.lblSSN_Destination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Location = New System.Drawing.Point(309, 59)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(37, 14)
        Me.Label18.TabIndex = 21
        Me.Label18.Text = "SSN :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPatientToMergeIn
        '
        Me.cmbPatientToMergeIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPatientToMergeIn.ForeColor = System.Drawing.Color.Black
        Me.cmbPatientToMergeIn.Location = New System.Drawing.Point(348, 23)
        Me.cmbPatientToMergeIn.Name = "cmbPatientToMergeIn"
        Me.cmbPatientToMergeIn.Size = New System.Drawing.Size(328, 22)
        Me.cmbPatientToMergeIn.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(29, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(317, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Select patient record you wish to remain in the system :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnl_tlsp
        '
        Me.pnl_tlsp.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp.Controls.Add(Me.tlsp_MergePatientRecords)
        Me.pnl_tlsp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlsp.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp.Name = "pnl_tlsp"
        Me.pnl_tlsp.Size = New System.Drawing.Size(713, 54)
        Me.pnl_tlsp.TabIndex = 14
        '
        'tlsp_MergePatientRecords
        '
        Me.tlsp_MergePatientRecords.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_MergePatientRecords.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MergePatientRecords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MergePatientRecords.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_MergePatientRecords.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MergePatientRecords.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnMerge, Me.ts_btnClose})
        Me.tlsp_MergePatientRecords.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MergePatientRecords.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_MergePatientRecords.Name = "tlsp_MergePatientRecords"
        Me.tlsp_MergePatientRecords.Size = New System.Drawing.Size(713, 53)
        Me.tlsp_MergePatientRecords.TabIndex = 0
        Me.tlsp_MergePatientRecords.Text = "toolStrip1"
        '
        'ts_btnMerge
        '
        Me.ts_btnMerge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnMerge.Image = CType(resources.GetObject("ts_btnMerge.Image"), System.Drawing.Image)
        Me.ts_btnMerge.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnMerge.Name = "ts_btnMerge"
        Me.ts_btnMerge.Size = New System.Drawing.Size(49, 50)
        Me.ts_btnMerge.Tag = "Merge"
        Me.ts_btnMerge.Text = "&Merge"
        Me.ts_btnMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnBrowsepatientDest)
        Me.Panel1.Controls.Add(Me.lblPatientToMergeIn)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.lblNoofExam_Destination)
        Me.Panel1.Controls.Add(Me.lblSSN_Destination)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblDOB_Destination)
        Me.Panel1.Controls.Add(Me.cmbPatientToMergeIn)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.lblProvider_Destination)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 193)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(713, 150)
        Me.Panel1.TabIndex = 15
        '
        'btnBrowsepatientDest
        '
        Me.btnBrowsepatientDest.BackgroundImage = CType(resources.GetObject("btnBrowsepatientDest.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsepatientDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsepatientDest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsepatientDest.Image = CType(resources.GetObject("btnBrowsepatientDest.Image"), System.Drawing.Image)
        Me.btnBrowsepatientDest.Location = New System.Drawing.Point(682, 24)
        Me.btnBrowsepatientDest.Name = "btnBrowsepatientDest"
        Me.btnBrowsepatientDest.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowsepatientDest.TabIndex = 35
        Me.btnBrowsepatientDest.UseVisualStyleBackColor = True
        '
        'lblPatientToMergeIn
        '
        Me.lblPatientToMergeIn.BackColor = System.Drawing.Color.White
        Me.lblPatientToMergeIn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPatientToMergeIn.ForeColor = System.Drawing.Color.Black
        Me.lblPatientToMergeIn.Location = New System.Drawing.Point(348, 24)
        Me.lblPatientToMergeIn.Name = "lblPatientToMergeIn"
        Me.lblPatientToMergeIn.Size = New System.Drawing.Size(328, 20)
        Me.lblPatientToMergeIn.TabIndex = 34
        Me.lblPatientToMergeIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(4, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(119, 14)
        Me.Label21.TabIndex = 33
        Me.Label21.Text = "  Patient Record 2"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 146)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(705, 1)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 146)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(709, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 146)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(707, 1)
        Me.Label17.TabIndex = 29
        Me.Label17.Text = "label1"
        '
        'pnlNote
        '
        Me.pnlNote.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlNote.Controls.Add(Me.Label23)
        Me.pnlNote.Controls.Add(Me.Label24)
        Me.pnlNote.Controls.Add(Me.Label25)
        Me.pnlNote.Controls.Add(Me.Label26)
        Me.pnlNote.Controls.Add(Me.Label27)
        Me.pnlNote.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlNote.Location = New System.Drawing.Point(0, 0)
        Me.pnlNote.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlNote.Name = "pnlNote"
        Me.pnlNote.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlNote.Size = New System.Drawing.Size(713, 44)
        Me.pnlNote.TabIndex = 16
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(4, 8)
        Me.Label23.Name = "Label23"
        Me.Label23.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Label23.Size = New System.Drawing.Size(705, 35)
        Me.Label23.TabIndex = 25
        Me.Label23.Text = "Note: The bottom box is for the patient record you wish to remain in the system." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "All information currently associated with Patient Record 1 will be merged with " & _
            "Patient Record 2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(4, 43)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(705, 1)
        Me.Label24.TabIndex = 24
        Me.Label24.Text = "label2"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 4)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 40)
        Me.Label25.TabIndex = 23
        Me.Label25.Text = "label4"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(709, 4)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 40)
        Me.Label26.TabIndex = 22
        Me.Label26.Text = "label3"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(3, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(707, 1)
        Me.Label27.TabIndex = 21
        Me.Label27.Text = "label1"
        '
        'pnlPatientList
        '
        Me.pnlPatientList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPatientList.Location = New System.Drawing.Point(0, 54)
        Me.pnlPatientList.Name = "pnlPatientList"
        Me.pnlPatientList.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPatientList.Size = New System.Drawing.Size(713, 343)
        Me.pnlPatientList.TabIndex = 34
        '
        'pnlStart
        '
        Me.pnlStart.Controls.Add(Me.Panel1)
        Me.pnlStart.Controls.Add(Me.pnlMain)
        Me.pnlStart.Controls.Add(Me.pnlNote)
        Me.pnlStart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlStart.Location = New System.Drawing.Point(0, 54)
        Me.pnlStart.Name = "pnlStart"
        Me.pnlStart.Size = New System.Drawing.Size(713, 343)
        Me.pnlStart.TabIndex = 36
        '
        'frmMergePatientRecords
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(713, 397)
        Me.Controls.Add(Me.pnlStart)
        Me.Controls.Add(Me.pnlPatientList)
        Me.Controls.Add(Me.pnl_tlsp)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMergePatientRecords"
        Me.Text = "Merge Patient Records"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnl_tlsp.ResumeLayout(False)
        Me.pnl_tlsp.PerformLayout()
        Me.tlsp_MergePatientRecords.ResumeLayout(False)
        Me.tlsp_MergePatientRecords.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlNote.ResumeLayout(False)
        Me.pnlStart.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim _FName As String
    Dim _LName As String
    Dim _Code As String
    Dim _SSN As String
    Dim _Phone As String
    Dim _DOB As Date
    Dim _ISDOB As Boolean = False

    ''Start :: 20101810 : Patient Merge
    Public IsPatientSourceclick As Boolean = False
    Public IsPatientDestinationclick As Boolean = False
    Public WithEvents ogloPatientCntrl As gloUserControlLibrary.gloPatientDataGrid
    Public Shared _LPatientMergeIn As Long = 0 ' Set from Dashboard
    Dim _PatientID As Int64
    Dim _PatientCode As String
    Dim _PatientFName As String
    Dim _PatientMName As String
    Dim _PatientLName As String
    Dim _PatientSSN As String
    Dim _PatientDOB As String
    Dim _patientProvider As String
    Dim Phone_AS As String
    Dim DOB_AS As Date
    Dim ISDOB_AS As Boolean
    Dim dvPatient As DataView
    ''End :: 20101810 : Patient Merge

    Public Property FName() As String
        Get
            Return _FName
        End Get
        Set(ByVal Value As String)
            _FName = Value
        End Set
    End Property

    Public Property LName() As String
        Get
            Return _LName
        End Get
        Set(ByVal Value As String)
            _LName = Value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal Value As String)
            _Code = Value
        End Set
    End Property

    Public Property SSN() As String
        Get
            Return _SSN
        End Get
        Set(ByVal Value As String)
            _SSN = Value
        End Set
    End Property

    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal Value As String)
            _Phone = Value
        End Set
    End Property

    Public Property DOB() As Date
        Get
            Return _DOB
        End Get
        Set(ByVal Value As Date)
            _DOB = Value
        End Set
    End Property

    Public Property ISDOB() As Boolean
        Get
            Return _ISDOB
        End Get
        Set(ByVal Value As Boolean)
            _ISDOB = Value
        End Set
    End Property

   

    Private Sub frmMergePatientRecords_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ''Sandip Darade 20090624
            '' All patients will be shown instead of showing duplicate patients only
            '' Please refer PER case  GLO2009-0002681
            ''Sandip Darade 20091007
            ''only duplicate patients will be shown
            ''Bug ID 4258


            ''Start :: Commented for the new logic
            'RemoveHandler cmbPatientToMerge.SelectedIndexChanged, AddressOf cmbPatientToMerge_SelectedIndexChanged
            'Call Fill_DuplicatePatients()
            'AddHandler cmbPatientToMerge.SelectedIndexChanged, AddressOf cmbPatientToMerge_SelectedIndexChanged
            'If (cmbPatientToMerge.Items.Count > 0) Then
            '    cmbPatientToMerge.SelectedIndex = 0
            '    cmbPatientToMerge_SelectedIndexChanged(Nothing, Nothing)
            'End If
            ''end :: Commented for the new logic
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally



        End Try
    End Sub

    Private Sub Fill_DuplicatePatients()
        Dim oPatient As New clsPatient
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        dt1 = oPatient.Fill_DuplicatePatient()
        oPatient = Nothing

        'Dim sender As Object
        Dim e As System.EventArgs = Nothing

        If IsNothing(dt1) = False Then
            dt2 = dt1.Copy
            cmbPatientToMerge.DataSource = dt1
            cmbPatientToMerge.DisplayMember = dt1.Columns("PatientName").ColumnName
            cmbPatientToMerge.ValueMember = dt1.Columns("PatientID").ColumnName
            If dt1.Rows.Count > 0 Then
                cmbPatientToMerge.SelectedValue = dt1.Rows(0)("PatientID")
                cmbPatientToMerge_SelectionChangeCommitted(cmbPatientToMerge, e)
            End If

            'With cmbPatientToMergeIn
            '    .DataSource = dt2
            '    .DisplayMember = dt2.Columns("PatientName").ColumnName
            '    .ValueMember = dt2.Columns("PatientID").ColumnName
            '    If dt2.Rows.Count > 0 Then
            '        .SelectedValue = dt2.Rows(0)("PatientID")
            '        cmbPatientToMergeIn_SelectionChangeCommitted(cmbPatientToMergeIn, e)
            '    End If
            'End With
        End If
    End Sub
#Region "Commented Functionality changes according MU"

    'Private Sub SaveMergeBtn()

    '    Try

    '        '' By Mahesh 20071026
    '        If IsNothing(cmbPatientToMerge.SelectedValue) = False And IsNothing(cmbPatientToMergeIn.SelectedValue) = False Then
    '            If cmbPatientToMerge.SelectedValue = cmbPatientToMergeIn.SelectedValue Then
    '                MessageBox.Show("'Patient to Merge' and 'Patient to Merge in' both should not the same.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '            ''
    '            If MessageBox.Show("Are you sure you want to merge the patient records?  ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '                Dim oclsPatient As New clsPatient
    '                ''Added by Mayuri:20100421:case No:#GLO2010-0004863
    '                Dim _arrsplit As String()
    '                _arrsplit = cmbPatientToMergeIn.Text.Split("-")
    '                Dim _code As String = _arrsplit(0)
    '                Dim _name As String = _arrsplit(1)
    '                ''
    '                With oclsPatient
    '                    If .Merge_Patients(cmbPatientToMerge.SelectedValue, cmbPatientToMerge.Text, cmbPatientToMergeIn.SelectedValue, cmbPatientToMergeIn.Text, gloStream.gloAlert.Alert.Alert_Type.General, _code) = True Then
    '                        '' By Mahesh 20071009
    '                        '' To Delete The Merged Patient 
    '                        Dim oclsPatientReg As New ClsPatientRegistrationDBLayer
    '                        oclsPatientReg.DeleteData(cmbPatientToMerge.SelectedValue, cmbPatientToMerge.Text)
    '                        oclsPatientReg = Nothing
    '                        'commented by dipak 20100925 as only use for select patient on dashboard need to comment for remove reffrences of gnpatientid
    '                        'gnPatientID = cmbPatientToMergeIn.SelectedValue
    '                        MessageBox.Show("Patient merge was successful.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Call Fill_DuplicatePatients()
    '                        ''Fill_Patients()
    '                    Else
    '                        MessageBox.Show("Patient merge was not successful.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                    End If
    '                End With
    '            End If
    '        End If
    '        'Me.Close()
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    ''' <summary>
    ''' New code according to mu
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveMergeBtn()

        Try
            '' By Mahesh 20071026
            If lblPatientToMerge.Text = "" Or lblPatientToMergeIn.Text = "" Then
                MessageBox.Show("Select source and destination patients.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Convert.ToInt64(lblPatientToMerge.Tag) = Convert.ToInt64(lblPatientToMergeIn.Tag) Then
                MessageBox.Show("Source and destination patients should not be the same.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Exit Sub
            End If
            ''

            If MessageBox.Show("Are you sure you want to merge the patient records?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim oclsPatient As New clsPatient
                ''Added by Mayuri:20101118
                Dim _arrsplit As String()
                Dim _arrsplitMergeinpatient As String()
                _arrsplitMergeinpatient = lblPatientToMergeIn.Text.Split("-")
                Dim _MergeInPatientcode As String = ""
                Dim _MergeInPatientname As String = ""
                If _arrsplitMergeinpatient.Length > 1 Then
                    _MergeInPatientcode = _arrsplitMergeinpatient(0).Trim
                    _MergeInPatientname = _arrsplitMergeinpatient(1).Trim
                End If

                _arrsplit = lblPatientToMerge.Text.Split("-")
                Dim _Patientcode As String = ""
                Dim _Patientname As String = ""
                If _arrsplit.Length > 1 Then
                    _Patientcode = _arrsplit(0).Trim
                    _Patientname = _arrsplit(1).Trim
                End If

                ''
                With oclsPatient
                    '' If .Merge_Patients(Convert.ToInt64(lblPatientToMerge.Tag), lblPatientToMerge.Text, Convert.ToInt64(lblPatientToMergeIn.Tag), lblPatientToMergeIn.Text, gloStream.gloAlert.Alert.Alert_Type.General) = True Then    ''''''''Alert_Type parameter Added by Ujwala Atre for case# GLO2008-0004863 Integration as on 07022010
                    If .Merge_Patients(Convert.ToInt64(lblPatientToMerge.Tag), _Patientcode, Convert.ToInt64(lblPatientToMergeIn.Tag), _MergeInPatientcode, gloStream.gloAlert.Alert.Alert_Type.General) = True Then    ''''''''Alert_Type parameter Added by Ujwala Atre for case# GLO2008-0004863 Integration as on 07022010
                        '' By Mahesh 20071009
                        '' To Delete The Merged Patient 
                        ''swaraj - 26-04-2010  To include demographics information''
                        If gblnPatDeamoMerg = True Then
                            .Merge_Demographics(Convert.ToInt64(lblPatientToMerge.Tag), Convert.ToInt64(lblPatientToMergeIn.Tag))
                            'Else

                        End If

                        Dim oclsPatientReg As New ClsPatientRegistrationDBLayer
                        If Not IsNothing(oclsPatientReg) Then
                            'oclsPatientReg.DeleteData(Convert.ToInt64(lblPatientToMerge.Tag), lblPatientToMerge.Text)
                            'oclsPatientReg.DeletePatientData(Convert.ToInt64(lblPatientToMerge.Tag), Convert.ToInt64(lblPatientToMergeIn.Tag), lblPatientToMerge.Text)
                            oclsPatientReg.DeletePatientData(Convert.ToInt64(lblPatientToMerge.Tag), Convert.ToInt64(lblPatientToMergeIn.Tag), _Patientcode)
                            If Not IsNothing(oclsPatientReg) Then
                                oclsPatientReg.Dispose()
                                oclsPatientReg = Nothing
                            End If
                        End If


                        MessageBox.Show("Patient merge was successful.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        gloPatient.PatientMergeAccounts.MergePatientAccountsForEMR(Convert.ToInt64(lblPatientToMergeIn.Tag), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.DatabaseConnectionString, _MergeInPatientcode, gloGlobal.gloPMGlobal.UserID)
                        _LPatientMergeIn = Convert.ToInt64(lblPatientToMergeIn.Tag)
                        ResetData()
                        'Call Fill_DuplicatePatients()
                    Else
                        MessageBox.Show("Patient merge was not successful.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End With
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

#End Region




    Private Sub cmbPatientToMerge_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPatientToMerge.SelectionChangeCommitted

        Dim dt As DataTable = Nothing
        Try

            If cmbPatientToMerge.SelectedValue > 0 Then
                Dim oclsPatient As New clsPatient
                dt = oclsPatient.Fill_PatientDetails(cmbPatientToMerge.SelectedValue)
                oclsPatient = Nothing
            End If

            If IsNothing(dt) = False Then
                With dt
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(.Rows(0)("SSN")) = True Then
                            lblSSN_Source.Text = ""
                        Else
                            lblSSN_Source.Text = .Rows(0)("SSN")
                        End If
                        If IsDBNull(.Rows(0)("DOB")) = True Then
                            lblDOB_Source.Text = ""
                        Else
                            lblDOB_Source.Text = dt.Rows(0)("DOB")
                        End If
                        If IsDBNull(.Rows(0)("ProviderName")) = True Then
                            lblProvider_Source.Text = ""
                        Else
                            lblProvider_Source.Text = .Rows(0)("ProviderName")
                        End If
                        If IsDBNull(.Rows(0)("ExamCount")) = True Then
                            lblNoofExams_Source.Text = ""
                        Else
                            lblNoofExams_Source.Text = .Rows(0)("ExamCount")
                        End If
                    Else
                        lblSSN_Source.Text = ""
                        lblDOB_Source.Text = ""
                        lblProvider_Source.Text = ""
                        lblNoofExams_Source.Text = ""
                    End If
                End With
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub

    Private Sub cmbPatientToMergeIn_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPatientToMergeIn.SelectionChangeCommitted
        Dim dt As DataTable = Nothing
        Try


            '' Fetch the Details of patient
            If cmbPatientToMergeIn.SelectedValue > 0 Then
                Dim oclsPatient As New clsPatient
                dt = oclsPatient.Fill_PatientDetails(cmbPatientToMergeIn.SelectedValue)
                oclsPatient = Nothing
            End If

            If IsNothing(dt) = False Then
                With dt
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(.Rows(0)("SSN")) = True Then
                            lblSSN_Destination.Text = ""
                        Else
                            lblSSN_Destination.Text = .Rows(0)("SSN")
                        End If
                        If IsDBNull(.Rows(0)("DOB")) = True Then
                            lblDOB_Destination.Text = ""
                        Else
                            lblDOB_Destination.Text = dt.Rows(0)("DOB")
                        End If
                        If IsDBNull(.Rows(0)("ProviderName")) = True Then
                            lblProvider_Destination.Text = ""
                        Else
                            lblProvider_Destination.Text = .Rows(0)("ProviderName")
                        End If
                        If IsDBNull(.Rows(0)("ExamCount")) = True Then
                            lblNoofExam_Destination.Text = ""
                        Else
                            lblNoofExam_Destination.Text = .Rows(0)("ExamCount")
                        End If
                    Else
                        lblSSN_Destination.Text = ""
                        lblDOB_Destination.Text = ""
                        lblProvider_Destination.Text = ""
                        lblNoofExam_Destination.Text = ""
                    End If
                End With
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Sub


    Private Sub cmbPatientToMerge_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPatientToMerge.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim _arrsplit As String()
            _arrsplit = cmbPatientToMerge.Text.Split("-")
            Dim _code As String = _arrsplit(0)
            Dim _name As String = _arrsplit(1)
            Dim dv As DataView
            For i As Int32 = 0 To cmbPatientToMergeIn.Items.Count - 1
                cmbPatientToMergeIn.SelectedIndex = i

                Dim _arrsplit1 As String()
                _arrsplit1 = cmbPatientToMergeIn.Text.Split("-")
                Dim _code1 As String = _arrsplit1(0)
                Dim _name1 As String = _arrsplit1(1)
                If (_name = _name1 And _code <> _code1) Then
                    Exit For
                End If
            Next
            cmbPatientToMergeIn_SelectionChangeCommitted(Nothing, Nothing)
            ''Added by Mayuri:20100104-To Filter Patients in cmbPatientToMergeIn as per selection of Patients in cmbPatientToMerge
            Dim oPatient As New clsPatient
            Dim dt1 As DataTable = Nothing
            '      Dim dt2 As New DataTable
            dt1 = oPatient.Fill_DuplicatePatient()
            If dt1 IsNot Nothing Then
                If dt1.Rows.Count > 0 Then
                    Dim strFilter As String

                    dv = dt1.DefaultView
                    strFilter = "PatientName like  '%" + _name.Trim().Replace("'", "''") + "%' AND PatientID <> " + cmbPatientToMerge.SelectedValue().ToString().Trim()
                    dv.RowFilter = strFilter
                    dt1 = dv.ToTable
                    If IsNothing(dt1) = False Then
                        cmbPatientToMergeIn.DataSource = dt1
                        cmbPatientToMergeIn.DisplayMember = dt1.Columns("PatientName").ColumnName
                        cmbPatientToMergeIn.ValueMember = dt1.Columns("PatientID").ColumnName
                        If dt1.Rows.Count > 0 Then
                            cmbPatientToMergeIn.SelectedValue = dt1.Rows(0)("PatientID")
                            cmbPatientToMergeIn_SelectionChangeCommitted(cmbPatientToMergeIn, e)
                        End If
                    End If
                    ''End code Added by Mayuri:20100104
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub




    Private Sub tblbtn_Merge_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MessageBox.Show("Are you sure to merge the records of Patient?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim oclsPatient As New clsPatient
                With oclsPatient
                    If .Merge_Patients(cmbPatientToMerge.SelectedValue, cmbPatientToMerge.Text, cmbPatientToMergeIn.SelectedValue, cmbPatientToMergeIn.Text) = True Then
                        MessageBox.Show("Merging of Patients has been done Successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Merging of Patients has NOT been done Successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End With
                oclsPatient = Nothing
            End If
            'Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_MergePatientRecords_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_MergePatientRecords.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Merge"
                    SaveMergeBtn()
                Case "Close"
                    Me.Close()

            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)

        End Try
    End Sub



#Region "Source/Destination Button Click"
    Private Sub btnBrowsepatientSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsepatientSource.Click
        patientSourceClick()
    End Sub
    Private Sub btnBrowsepatientDest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsepatientDest.Click
        patientDestinationClick()
    End Sub
    Private Sub patientSourceClick()
        Try
            IsPatientSourceclick = True
            IsPatientDestinationclick = False
            ogloPatientCntrl = New gloUserControlLibrary.gloPatientDataGrid
            _isgloPatientControl = True
            If Not IsNothing(ogloPatientCntrl) Then
                pnlPatientList.Controls.Add(ogloPatientCntrl)
                ogloPatientCntrl.Dock = DockStyle.Fill
                ogloPatientCntrl.BringToFront()
                ogloPatientCntrl.Visible = True

                'oPatient.DatabaseConnection = GetConnectionString()
                'oPatient.ClinicID = gnClinicID
                ' ''oPatientListControl.SelectedPatientID = gnPatientID
                'oPatient.FillPatients()
                'pnlSourcePatient.Controls.Add(oPatient)
                'oPatient.Dock = DockStyle.Fill
                pnlStart.SendToBack()
                pnlPatientList.BringToFront()

                'pnl_tlsp.SendToBack()
                'pnlMain.SendToBack()
            End If
        Catch ex As Exception

        Finally
            'If Not IsNothing(ogloPatientCntrl) Then
            '    ogloPatientCntrl.Dispose()
            '    ogloPatientCntrl = Nothing
            'End If
        End Try
    End Sub
    Dim _isgloPatientControl As Boolean = False
    Private Sub patientDestinationClick()
        Try
            IsPatientSourceclick = False
            IsPatientDestinationclick = True

            ogloPatientCntrl = New gloUserControlLibrary.gloPatientDataGrid
            _isgloPatientControl = True
            If Not IsNothing(ogloPatientCntrl) Then
                pnlPatientList.Controls.Add(ogloPatientCntrl)
                ogloPatientCntrl.Dock = DockStyle.Fill
                ogloPatientCntrl.BringToFront()
                ogloPatientCntrl.Visible = True

                'oPatient.DatabaseConnection = GetConnectionString()
                'oPatient.ClinicID = gnClinicID
                ' ''oPatientListControl.SelectedPatientID = gnPatientID
                'oPatient.FillPatients()
                'pnlSourcePatient.Controls.Add(oPatient)
                'oPatient.Dock = DockStyle.Fill
                pnlStart.SendToBack()
                pnlPatientList.BringToFront()
                'pnl_tlsp.SendToBack()
                'pnlMain.SendToBack()
            End If
        Catch ex As Exception
        Finally
            'If Not IsNothing(ogloPatientCntrl) Then
            '    ogloPatientCntrl.Dispose()
            '    ogloPatientCntrl = Nothing
            'End If
        End Try
    End Sub
#End Region

#Region "gloPatientControl Events"
    Private Sub ogloPatientCntrl_Cancel_Click() Handles ogloPatientCntrl.Cancel_Click
        Try
            pnlPatientList.Controls.Remove(ogloPatientCntrl)
            pnlPatientList.SendToBack()
            pnlStart.BringToFront()
            ogloPatientCntrl.Dispose()
            ogloPatientCntrl = Nothing
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ogloPatientCntrl_OK_Click() Handles ogloPatientCntrl.OK_Click
        '_PatientID = Convert.ToInt64(ogloPatientCntrl.PatientID);
        '               _PatientCode = ogloPatientCntrl.PatientCode.ToString();
        '               _PatientName = ogloPatientCntrl.FirstName.ToString() + " " + ogloPatientCntrl.MiddleName.ToString() + " " + ogloPatientCntrl.LastName.ToString();
        '               lblPatients.Text = "Selected Patient -" + _PatientName;

        'If IsPatientToclick Then

        'ElseIf IsPatientInclick Then

        'End If
        _PatientID = Convert.ToInt64(ogloPatientCntrl.PatientID)
        _PatientCode = ogloPatientCntrl.PatientCode
        _PatientFName = ogloPatientCntrl.FirstName
        _PatientMName = ogloPatientCntrl.MiddleName
        _PatientLName = ogloPatientCntrl.LastName
        _PatientSSN = ogloPatientCntrl.PatientSSN
        _patientProvider = ogloPatientCntrl.PatientProvider
        If ogloPatientCntrl.PatientDOB = "1/1/1900" Then
            _PatientDOB = ""
        Else
            _PatientDOB = ogloPatientCntrl.PatientDOB
        End If


        If IsPatientSourceclick Then
            ''sPatientCode + ' - ' + isnull(sFirstName,'')+space(2)+isnull(sMiddleName,'')+space(2)+isnull(sLastName,'') as PatientName
            lblPatientToMerge.Text = _PatientCode & " - " & _PatientFName & "  " & _PatientMName & "  " & _PatientLName
            lblPatientToMerge.Tag = _PatientID
            lblSSN_Source.Text = _PatientSSN

            lblDOB_Source.Text = _PatientDOB
            ' Chetan Change to remove space from Provider Name  20 Aug 2010
            lblProvider_Source.Text = _patientProvider.Replace("    ", " ").Replace("  ", " ")

            Dim oPatient As New clsPatient
            lblNoofExams_Source.Text = Convert.ToString(oPatient.GetPatientExamCount(_PatientID))
            oPatient = Nothing


        ElseIf IsPatientDestinationclick Then
            lblPatientToMergeIn.Text = _PatientCode & " - " & _PatientFName & "  " & _PatientMName & "  " & _PatientLName
            lblPatientToMergeIn.Tag = _PatientID
            lblSSN_Destination.Text = _PatientSSN

            lblDOB_Destination.Text = _PatientDOB
            ' Chetan Change to remove space from Provider Name  20 Aug 2010
            lblProvider_Destination.Text = _patientProvider.Replace("    ", " ").Replace("  ", " ")

            Dim oPatient As New clsPatient
            lblNoofExam_Destination.Text = Convert.ToString(oPatient.GetPatientExamCount(_PatientID))
            oPatient = Nothing
        End If
        ogloPatientCntrl_Cancel_Click()
    End Sub
    Private Sub ogloPatientCntrl_PicAdv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogloPatientCntrl.PicAdv_Click
        Try
            Dim RowFilter As String = ""
            ' Dim DVMain As DataView

            Dim frm As New frmAdvancedSearch
            With frm
                .Phone = Phone_AS
                .ISDOB = ISDOB_AS
                .DOB = DOB_AS
                '''''-------Code modified by Anil on 20071212
                'Dim strSearchText As String = ""
                'strSearchText = txtSearchPatient.Text.Trim
                'strSearchText = Replace(strSearchText, "'", "")

                'Select Case Trim(lblSearchCriteria.Text)
                '    Case "Patient ID"
                '        .Code = strSearchText
                '    Case "First Name"
                '        .FName = strSearchText
                '    Case "Last Name"
                '        .LName = strSearchText
                '    Case "SSN No"
                '        .SSN = strSearchText
                'End Select
                ''''--------
                .ShowInTaskbar = False
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                '' Set Values For Update

                '' SUDHIR 20090604 '' 
                .Phone = Replace(.Phone, "'", "''")
                .Phone = Replace(.Phone, "[", "") & ""
                .Phone = mdlGeneral.ReplaceSpecialCharacters(.Phone)
                '' END SUDHIR ''


                Phone_AS = .Phone
                ISDOB_AS = .ISDOB
                DOB_AS = .DOB

                '''''''''
                'If .ISDOB = True Then
                '    ISDOB = True
                '    DOB = .DOB()
                'End If
                'Phone = .Phone

                dvPatient = CType(ogloPatientCntrl.dgPatient.DataSource, DataView)
                If (IsNothing(dvPatient) = False) Then


                    '' SUDHIR 20090610 '' 
                    .Code = Replace(.Code, "'", "''")
                    .Code = Replace(.Code, "[", "") & ""
                    .Code = mdlGeneral.ReplaceSpecialCharacters(.Code)
                    '' END SUDHIR ''



                    If .Code <> "" Then ''PatientCode
                        RowFilter = GetRowFilter(RowFilter)
                        If .Code.StartsWith("%") = True Or .Code.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientCode").ColumnName & " Like '%" & .Code & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientCode").ColumnName & " Like '" & .Code & "%'"
                        End If
                    End If
                    If .Mobile <> "" Then ''PatientCode
                        RowFilter = GetRowFilter(RowFilter)
                        If .Mobile.StartsWith("%") = True Or .Mobile.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMobile").ColumnName & " Like '%" & .Mobile & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMobile").ColumnName & " Like '" & .Mobile & "%'"
                        End If
                    End If

                    If .FName <> "" Then ''PatientFirstName
                        RowFilter = GetRowFilter(RowFilter)
                        If .FName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strFirstName = Mid(.FName, 1, .FName.IndexOf(","))
                            strLastName = Mid(.FName, .FName.IndexOf(",") + 2)

                            '' MAHESH 20090610 '' 
                            strFirstName = Replace(.Code, "'", "''")
                            strFirstName = Replace(.Code, "[", "") & ""
                            strFirstName = mdlGeneral.ReplaceSpecialCharacters(strFirstName)
                            '' END SUDHIR ''

                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .FName.StartsWith("%") = True Or .FName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '%" & .FName.Replace("'", "''").Replace("%", "''") & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & .FName.Replace("'", "''").Replace("%", "''") & "%'"
                            End If
                        End If
                    End If

                    If .LName <> "" Then ''PatientLastName
                        RowFilter = GetRowFilter(RowFilter)
                        If .LName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strLastName = Mid(.LName, 1, .LName.IndexOf(","))
                            strFirstName = Mid(.LName, .LName.IndexOf(",") + 2)
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .LName.StartsWith("%") = True Or .LName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '%" & .LName.Replace("'", "''").Replace("%", "''") & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & .LName.Replace("'", "''").Replace("%", "''") & "%'"
                            End If
                        End If
                    End If

                    If .SSN <> "" And IsNumeric(.SSN) = True Then ''SSNNo  ''PatientDOB '' Phone
                        RowFilter = GetRowFilter(RowFilter)
                        RowFilter = RowFilter & dvPatient.Table.Columns("SSNNo").ColumnName & "=" & .SSN
                        'Else
                        '    RowFilter = GetRowFilter(RowFilter)
                        '    RowFilter = RowFilter & dvPatient.Table.Columns("SSNNo").ColumnName & " Like '%'"
                    End If

                    If .Phone <> "" Then ''PatientDOB '' Phone
                        RowFilter = GetRowFilter(RowFilter)
                        If .Phone.StartsWith("%") = True Or .Phone.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("Phone").ColumnName & " Like '%" & .Phone & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("Phone").ColumnName & " Like '" & .Phone & "%'"
                        End If
                    End If


                    If .ISDOB = True And IsDate(.DOB) = True Then
                        RowFilter = GetRowFilter(RowFilter)
                        RowFilter = RowFilter & dvPatient.Table.Columns("PatientDOB").ColumnName & "= '" & .DOB & "'"
                    End If


                    '''' For Search on Gardian's Info
                    '''' 20070128
                    If .IsGuardianinfo = True Then
                        ''sMother_fName,  sMother_lName, sMother_Phone, sMother_Mobile, sFather_fName, sFather_lName, sFather_Phone, sFather_Mobile
                        ''''

                        '' SUDHIR 20090604 '' 
                        .MotherFirstName = Replace(.MotherFirstName, "'", "''")
                        .MotherFirstName = Replace(.MotherFirstName, "[", "") & ""
                        .MotherFirstName = mdlGeneral.ReplaceSpecialCharacters(.MotherFirstName)
                        '' END SUDHIR ''


                        If .MotherFirstName <> "" Then  ''MotherFirstName
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherFirstName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.MotherFirstName, 1, .MotherFirstName.IndexOf(","))
                                strFirstName = Mid(.MotherFirstName, .MotherFirstName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .MotherFirstName.StartsWith("%") = True Or .MotherFirstName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '%" & .MotherFirstName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & .MotherFirstName & "%'"
                                End If
                            End If
                        End If
                        ''''

                        '' SUDHIR 20090604 '' 
                        .MotherLastName = Replace(.MotherLastName, "'", "''")
                        .MotherLastName = Replace(.MotherLastName, "[", "") & ""
                        .MotherLastName = mdlGeneral.ReplaceSpecialCharacters(.MotherLastName)
                        '' END SUDHIR ''


                        If .MotherLastName <> "" Then   ''MotherLastName
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherLastName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.MotherLastName, 1, .MotherLastName.IndexOf(","))
                                strFirstName = Mid(.MotherLastName, .MotherLastName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .MotherLastName.StartsWith("%") = True Or .MotherLastName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '%" & .MotherLastName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & .MotherLastName & "%'"
                                End If
                            End If
                        End If

                        '' SUDHIR 20090604 '' 
                        .MotherCellNo = Replace(.MotherCellNo, "'", "''")
                        .MotherCellNo = Replace(.MotherCellNo, "[", "") & ""
                        .MotherCellNo = mdlGeneral.ReplaceSpecialCharacters(.MotherCellNo)
                        '' END SUDHIR ''


                        If .MotherCellNo <> "" Then  ''MotherCellNo '' sMother_Mobile
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherCellNo.StartsWith("%") = True Or .MotherCellNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Mobile").ColumnName & " Like '%" & .MotherCellNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Mobile").ColumnName & " Like '" & .MotherCellNo & "%'"
                            End If
                        End If

                        '' SUDHIR 20090604 '' 
                        .MotherPhoneNo = Replace(.MotherPhoneNo, "'", "''")
                        .MotherPhoneNo = Replace(.MotherPhoneNo, "[", "") & ""
                        .MotherPhoneNo = mdlGeneral.ReplaceSpecialCharacters(.MotherPhoneNo)
                        '' END SUDHIR ''


                        If .MotherPhoneNo <> "" Then  ''MotherPhoneNo '' sMother_Phone
                            RowFilter = GetRowFilter(RowFilter)
                            If .MotherPhoneNo.StartsWith("%") = True Or .MotherPhoneNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Phone").ColumnName & " Like '%" & .MotherPhoneNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Phone").ColumnName & " Like '" & .MotherPhoneNo & "%'"
                            End If
                        End If
                        '''''
                        '' SUDHIR 20090604 '' 
                        .FatherFirstName = Replace(.FatherFirstName, "'", "''")
                        .FatherFirstName = Replace(.FatherFirstName, "[", "") & ""
                        .FatherFirstName = mdlGeneral.ReplaceSpecialCharacters(.FatherFirstName)
                        '' END SUDHIR ''

                        '''''
                        If .FatherFirstName <> "" Then   '' FatherFirstName , sFather_fName
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherFirstName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.FatherFirstName, 1, .FatherFirstName.IndexOf(","))
                                strFirstName = Mid(.FatherFirstName, .FatherFirstName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .FatherFirstName.StartsWith("%") = True Or .FatherFirstName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '%" & .FatherFirstName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & .FatherFirstName & "%'"
                                End If
                            End If
                        End If
                        ''''
                        '' SUDHIR 20090604 '' 
                        .FatherLastName = Replace(.FatherLastName, "'", "''")
                        .FatherLastName = Replace(.FatherLastName, "[", "") & ""
                        .FatherLastName = mdlGeneral.ReplaceSpecialCharacters(.FatherLastName)
                        '' END SUDHIR ''


                        If .FatherLastName <> "" Then    ''FatherLastName, sFather_lName
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherLastName.IndexOf(",") >= 1 Then
                                Dim strFirstName As String
                                Dim strLastName As String
                                strLastName = Mid(.FatherLastName, 1, .FatherLastName.IndexOf(","))
                                strFirstName = Mid(.FatherLastName, .FatherLastName.IndexOf(",") + 2)
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & strLastName & "%'"
                            Else
                                If .FatherLastName.StartsWith("%") = True Or .FatherLastName.StartsWith("*") = True Then
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '%" & .FatherLastName & "%'"
                                Else
                                    RowFilter = RowFilter & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & .FatherLastName & "%'"
                                End If
                            End If
                        End If

                        '' SUDHIR 20090604 '' 
                        .FatherCellNo = Replace(.FatherCellNo, "'", "''")
                        .FatherCellNo = Replace(.FatherCellNo, "[", "") & ""
                        .FatherCellNo = mdlGeneral.ReplaceSpecialCharacters(.FatherCellNo)
                        '' END SUDHIR ''

                        If .FatherCellNo <> "" Then   ''FatherCellNo ''  sFather_Mobile
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherCellNo.StartsWith("%") = True Or .FatherCellNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Mobile").ColumnName & " Like '%" & .FatherCellNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Mobile").ColumnName & " Like '" & .FatherCellNo & "%'"
                            End If
                        End If

                        '' SUDHIR 20090604 '' 
                        .FatherPhoneNo = Replace(.FatherPhoneNo, "'", "''")
                        .FatherPhoneNo = Replace(.FatherPhoneNo, "[", "") & ""
                        .FatherPhoneNo = mdlGeneral.ReplaceSpecialCharacters(.FatherPhoneNo)
                        '' END SUDHIR ''


                        If .FatherPhoneNo <> "" Then   ''FatherPhoneNo '' sFather_Phone
                            RowFilter = GetRowFilter(RowFilter)
                            If .FatherPhoneNo.StartsWith("%") = True Or .FatherPhoneNo.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Phone").ColumnName & " Like '%" & .FatherPhoneNo & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Phone").ColumnName & " Like '" & .FatherPhoneNo & "%'"
                            End If
                        End If

                    End If
                End If
            End With
            frm.Dispose()
            frm = Nothing
            Me.Cursor = Cursors.WaitCursor
            'Dim dvPatient As DataView

            'dgPatient.DataSource = dvPatient

            'If RowFilter <> "" Then
            If (IsNothing(dvPatient) = False) Then
                dvPatient.RowFilter = RowFilter
            End If

            'End If



            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ResetData()
        ''''Source Data
        lblPatientToMerge.Text = ""
        lblPatientToMerge.Tag = ""
        lblSSN_Source.Text = ""
        lblDOB_Source.Text = ""
        lblProvider_Source.Text = ""
        lblNoofExams_Source.Text = ""


        ''''Destination Data
        lblPatientToMergeIn.Text = ""
        lblPatientToMergeIn.Tag = ""
        lblSSN_Destination.Text = ""
        lblDOB_Destination.Text = ""
        lblProvider_Destination.Text = ""
        lblNoofExam_Destination.Text = ""
        '''''''''''''''
        ChkDemoGraphics.Checked = False
        '''''''''''''''
    End Sub

#End Region

    Private Function GetRowFilter(ByVal RowFilter As String) As String
        If RowFilter = "" Then
            Return RowFilter
        Else
            Return RowFilter & " AND "
        End If
    End Function


End Class
