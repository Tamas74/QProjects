Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports System.IO
Imports gloEMR.gloEMRWord
Imports gloUserControlLibrary
Imports System.Runtime.InteropServices
Public Class frmDM_Template
    Inherits System.Windows.Forms.Form

    Implements ISignature
    Implements gloVoice, IExamChildEvents


    Implements IHotKey


    Private m_VisitID As Long
    ' Private m_patientID As Long

    'Instantiate voice class
    Private ogloVoice As ClsVoice
    'implement interface for Voice --supriya 03/06/2009

    Private m_PatientID As Long
    Private m_Guidelines As ArrayList

    Dim oclsDM_Tempalte As New clsDM_Template

    Private Arrlist As ArrayList

    '' To Insert Signature
    Public Shared Imagepath As String

    Dim blnTemplateExist As Boolean = False

    Dim oWordApp As Wd.Application
    Private oCurDoc As Wd.Document
    Private WithEvents _PatientStrip As gloUC_PatientStrip = Nothing
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    ' Dim oTempDoc As Wd.Document
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents wdDMTemplate As AxDSOFramer.AxFramerControl
    Dim ObjWord As clsWordDocument
    Dim objCriteria As DocCriteria
    ' Private WithEvents wdTemp As AxDSOFramer.AxFramerControl
    Friend WithEvents tlsDM As New WordToolStrip.gloWordToolStrip
    Public IsopenfromDM As Boolean = False
    Friend WithEvents trvTemplate As System.Windows.Forms.TreeView
    Dim myidx As Int32
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Dim blnTemplateId As Int64 = 0

    Public Enum Type
        Save = 1
        Print = 2
        Fax = 3
    End Enum

#Region " Windows Form Designer generated code "

    'Public Sub New(ByVal COL_CriteriaID As Collection, ByVal _SelectedGuideLines As Collection, ByVal blnIsSinglePatient As Boolean)
    '    MyBase.New()
    '    _blnIsSinglePatient = blnIsSinglePatient
    '    ' _PatientID = nPatientID
    '    '_COL_CriteriaID.Add(_CriteriaID)
    '    _COL_CriteriaID = COL_CriteriaID
    '    _COL_Guidlines = _SelectedGuideLines
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()



    '    'Add any initialization after the InitializeComponent() call

    'End Sub
    Public Sub New(ByVal _SelectedGuideLines As ArrayList, ByVal _PatientId As Int64)
        MyBase.New()
        m_PatientID = _PatientId
        m_Guidelines = _SelectedGuideLines
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
            If Not IsNothing(ogloVoice) Then
                ogloVoice.Dispose()
                ogloVoice = Nothing
            End If
            If (IsNothing(oclsDM_Tempalte) = False) Then
                oclsDM_Tempalte = Nothing
            End If
            Try
                If (IsNothing(_PatientStrip) = False) Then
                    _PatientStrip.Dispose()
                    _PatientStrip = Nothing
                End If
            Catch

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ImgPatientEducation As System.Windows.Forms.ImageList
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents trvGuidlineHistory As System.Windows.Forms.TreeView
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_Template))
        Me.ImgPatientEducation = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.trvTemplate = New System.Windows.Forms.TreeView
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlRight = New System.Windows.Forms.Panel
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.trvGuidlineHistory = New System.Windows.Forms.TreeView
        Me.Label30 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblHeader = New System.Windows.Forms.Label
        Me.pnlMiddle = New System.Windows.Forms.Panel
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.wdDMTemplate = New AxDSOFramer.AxFramerControl
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.pnlLeft.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlMiddle.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.wdDMTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImgPatientEducation
        '
        Me.ImgPatientEducation.ImageStream = CType(resources.GetObject("ImgPatientEducation.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgPatientEducation.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgPatientEducation.Images.SetKeyName(0, "arrow_01.ico")
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.Panel1)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(232, 734)
        Me.pnlLeft.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(232, 734)
        Me.Panel1.TabIndex = 9
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.trvTemplate)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(0, 28)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(232, 706)
        Me.Panel4.TabIndex = 20
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(4, 702)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(227, 1)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 702)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "label4"
        '
        'trvTemplate
        '
        Me.trvTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTemplate.ForeColor = System.Drawing.Color.Black
        Me.trvTemplate.ImageIndex = 0
        Me.trvTemplate.ImageList = Me.ImgPatientEducation
        Me.trvTemplate.ItemHeight = 21
        Me.trvTemplate.Location = New System.Drawing.Point(3, 1)
        Me.trvTemplate.Name = "trvTemplate"
        Me.trvTemplate.SelectedImageIndex = 0
        Me.trvTemplate.Size = New System.Drawing.Size(228, 702)
        Me.trvTemplate.TabIndex = 8
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(231, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 702)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(229, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(232, 28)
        Me.Panel2.TabIndex = 9
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label5)
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Controls.Add(Me.Label8)
        Me.Panel6.Controls.Add(Me.Label7)
        Me.Panel6.Controls.Add(Me.Label1)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(3, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(229, 22)
        Me.Panel6.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Location = New System.Drawing.Point(0, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 20)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Location = New System.Drawing.Point(228, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 20)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(0, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(229, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(229, 1)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(229, 22)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "   Guidelines"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.Panel10)
        Me.pnlRight.Controls.Add(Me.Panel3)
        Me.pnlRight.Controls.Add(Me.Panel9)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(784, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(232, 734)
        Me.pnlRight.TabIndex = 11
        Me.pnlRight.Visible = False
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label27)
        Me.Panel10.Controls.Add(Me.Label28)
        Me.Panel10.Controls.Add(Me.Label29)
        Me.Panel10.Controls.Add(Me.trvGuidlineHistory)
        Me.Panel10.Controls.Add(Me.Label30)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel10.Location = New System.Drawing.Point(0, 54)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel10.Size = New System.Drawing.Size(232, 680)
        Me.Panel10.TabIndex = 20
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(1, 676)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(227, 1)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 676)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(228, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 676)
        Me.Label29.TabIndex = 6
        Me.Label29.Text = "label3"
        '
        'trvGuidlineHistory
        '
        Me.trvGuidlineHistory.BackColor = System.Drawing.Color.White
        Me.trvGuidlineHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvGuidlineHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvGuidlineHistory.ForeColor = System.Drawing.Color.Black
        Me.trvGuidlineHistory.HideSelection = False
        Me.trvGuidlineHistory.ImageIndex = 0
        Me.trvGuidlineHistory.ImageList = Me.ImgPatientEducation
        Me.trvGuidlineHistory.Location = New System.Drawing.Point(0, 1)
        Me.trvGuidlineHistory.Name = "trvGuidlineHistory"
        Me.trvGuidlineHistory.SelectedImageIndex = 0
        Me.trvGuidlineHistory.ShowLines = False
        Me.trvGuidlineHistory.Size = New System.Drawing.Size(229, 676)
        Me.trvGuidlineHistory.TabIndex = 5
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(229, 1)
        Me.Label30.TabIndex = 5
        Me.Label30.Text = "label1"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.Controls.Add(Me.TextBox1)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Controls.Add(Me.label9)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.ForeColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(0, 28)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(232, 26)
        Me.Panel3.TabIndex = 16
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(29, 5)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(199, 15)
        Me.TextBox1.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(199, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(199, 2)
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
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(1, 22)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(227, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(227, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(228, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "label4"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 23)
        Me.Label26.TabIndex = 41
        Me.Label26.Text = "label4"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel7)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel9.Size = New System.Drawing.Size(232, 28)
        Me.Panel9.TabIndex = 17
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label25)
        Me.Panel7.Controls.Add(Me.Label24)
        Me.Panel7.Controls.Add(Me.Label3)
        Me.Panel7.Controls.Add(Me.lbl_pnlTop)
        Me.Panel7.Controls.Add(Me.Label2)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(229, 22)
        Me.Panel7.TabIndex = 8
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Location = New System.Drawing.Point(0, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 20)
        Me.Label25.TabIndex = 42
        Me.Label25.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Location = New System.Drawing.Point(228, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 20)
        Me.Label24.TabIndex = 41
        Me.Label24.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(0, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(229, 1)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label1"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(229, 1)
        Me.lbl_pnlTop.TabIndex = 7
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(229, 22)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "   History  Guidelines"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHeader
        '
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(546, 22)
        Me.lblHeader.TabIndex = 16
        Me.lblHeader.Text = "   Guideline Template"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMiddle.Controls.Add(Me.Panel8)
        Me.pnlMiddle.Controls.Add(Me.Panel11)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(235, 0)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Size = New System.Drawing.Size(546, 734)
        Me.pnlMiddle.TabIndex = 17
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.wdDMTemplate)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.Label15)
        Me.Panel8.Controls.Add(Me.Label22)
        Me.Panel8.Controls.Add(Me.Label23)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 28)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel8.Size = New System.Drawing.Size(546, 706)
        Me.Panel8.TabIndex = 20
        '
        'wdDMTemplate
        '
        Me.wdDMTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdDMTemplate.Enabled = True
        Me.wdDMTemplate.Location = New System.Drawing.Point(1, 1)
        Me.wdDMTemplate.Name = "wdDMTemplate"
        Me.wdDMTemplate.OcxState = CType(resources.GetObject("wdDMTemplate.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdDMTemplate.Size = New System.Drawing.Size(544, 701)
        Me.wdDMTemplate.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 702)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(544, 1)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 702)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(545, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 702)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(546, 1)
        Me.Label23.TabIndex = 5
        Me.Label23.Text = "label1"
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Panel5)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel11.Size = New System.Drawing.Size(546, 28)
        Me.Panel11.TabIndex = 21
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.lbl_pnlRight)
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.lblHeader)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(546, 22)
        Me.Panel5.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 20)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(545, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 20)
        Me.lbl_pnlRight.TabIndex = 20
        Me.lbl_pnlRight.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(0, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(546, 1)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "label1"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(546, 1)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(232, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 734)
        Me.Splitter1.TabIndex = 18
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(781, 0)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 734)
        Me.Splitter2.TabIndex = 19
        Me.Splitter2.TabStop = False
        '
        'frmDM_Template
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 734)
        Me.Controls.Add(Me.pnlMiddle)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnlLeft)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDM_Template"
        Me.ShowInTaskbar = False
        Me.Text = "Guideline Template"
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnlRight.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlMiddle.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        CType(Me.wdDMTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmDM_Template_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If
        End If
        Me.WindowState = FormWindowState.Maximized
        If Me.ParentForm IsNot Nothing Then
            CType(Me.ParentForm, MainMenu).RegisterMyHotKey()
            CType(Me.ParentForm, MainMenu).ActiveDSO = wdDMTemplate
        End If
    End Sub

    Private Sub frmDM_Template_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If
        End If
        'Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub frmDM_Template_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (IsNothing(Me.ParentForm) = False) Then
            CType(Me.ParentForm, MainMenu).ActiveDSO = Nothing
        End If
        Try
            If (IsNothing(_PatientStrip) = False) Then
                _PatientStrip.Dispose()
                _PatientStrip = Nothing
            End If
        Catch ex As Exception

        End Try

        If (IsNothing(mdlFAX.Owner) = False) Then
            mdlFAX.Owner = Nothing
        End If

    End Sub

    Private Sub frmDM_Template_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                TurnoffMicrophone()
                ogloVoice.UnInitializeVoiceComponents()
            End If
        End If
    End Sub

    Private Sub frmDM_Template_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''''Pramod Change Make to open form from frmDM_PatientSpecific start
            'If IsopenfromDM = False Then
            '    With trvPatients
            '        .Nodes.Clear()
            '        For i As Int16 = 1 To _COL_PatientID.Count
            '            Dim oNode As New myTreeNode
            '            With oNode
            '                .Text = oclsDM_Tempalte.GetPatientName(_COL_PatientID(i))
            '                .Tag = _COL_PatientID(i)
            '                If i = 1 Then
            '                    _PatientID = .Tag
            '                End If
            '            End With

            '            .Nodes.Add(oNode)
            '            oNode = Nothing
            '        Next
            '    End With
            'End If
            ''''Pramod Change Make to open form from frmDM_PatientSpecific End
            ''Modified by Anil on 20071218
            ''loadPatientStrip()
            ''
            Dim oDM As New gloStream.DiseaseManagement.DiseaseManagement
            blnTemplateId = 0

            trvTemplate.Nodes.Clear()
            Dim myRootNode As myTreeNode = Nothing
            For _index As Integer = 0 To m_Guidelines.Count - 1

                'Dim dt As DataTable
                'Dim i As Integer

                ''''' to Get all Templates(ID & Name) of Patient Education
                Dim oGuideline As myList
                oGuideline = CType(m_Guidelines(_index), myList)
                If _index = 0 Then
                    myRootNode = New myTreeNode
                    myRootNode.Text = oDM.GetCriteriaName(oGuideline.Index)
                    ''Sandip Darade 20090819
                    ''set id to tag 
                    myRootNode.Tag = oGuideline.Index
                    myRootNode.Key = -1
                    myRootNode.ImageIndex = 0
                    myRootNode.SelectedImageIndex = 0
                    'myRootNode.DMTemplate = oGuideline.DMTemplate
                    trvTemplate.Nodes.Add(myRootNode)

                End If

                Dim blnExists As Boolean = False
                ' dt = oclsDM_Tempalte.FillTemplates(oGuideline.Index)
                For Each mynode As myTreeNode In trvTemplate.Nodes
                    'sarika DM Denormalization 20090403
                    'If mynode.Tag = oGuideline.Tag Then
                    ' If mynode.Tag = oGuideline.Index Then
                    If mynode.Tag = oGuideline.Index Then
                        '--
                        myRootNode = mynode
                        blnExists = True
                    End If
                Next
                If Not blnExists Then
                    myRootNode = New myTreeNode
                    myRootNode.Text = oDM.GetCriteriaName(oGuideline.Index)
                    myRootNode.Tag = oGuideline.Index ''20100225 
                    ''myRootNode.Tag = oGuideline.ID
                    myRootNode.Key = -1
                    myRootNode.ImageIndex = 0
                    myRootNode.SelectedImageIndex = 0
                    'myRootNode.DMTemplate = oGuideline.DMTemplate
                    trvTemplate.Nodes.Add(myRootNode)
                End If

                
                Dim childNode As New myTreeNode
                childNode.Text = oGuideline.Value
                childNode.Tag = oGuideline.ID

                childNode.DMTemplate = oGuideline.DMTemplate

                myRootNode.Nodes.Add(childNode)
                If _index = 0 Then
                    trvTemplate.SelectedNode = childNode
                    ' blnTemplateId = oGuideline.ID
                End If



            Next
            '' If Guidelines are for Single Patient
            lblHeader.Text = "Guidelines for the Patient - " & oclsDM_Tempalte.GetPatientName(m_PatientID)


            'CType(Me.ParentForm, MainMenu).pnlMenu.Visible = False
            'CType(Me.ParentForm, MainMenu).pnlLeft.Visible = False
            'CType(Me.ParentForm, MainMenu).Splitter1.Visible = False

            'WindowState = FormWindowState.Maximized

            trvTemplate.ExpandAll()
            trvTemplate.Select()
            oDM.Dispose()
            oDM = Nothing
            ''Modified by Anil on 20071218
            loadPatientStrip()
            loadToolStrip()
            If Not trvTemplate.SelectedNode Is Nothing Then
                Dim objTrvArgs As New TreeNodeMouseClickEventArgs(trvTemplate.SelectedNode, Windows.Forms.MouseButtons.Left, 2, trvTemplate.SelectedNode.Bounds.X, trvTemplate.SelectedNode.Bounds.Y)
                Dim objSender As Object = Nothing
                Call trvTemplate_NodeMouseDoubleClick(objSender, objTrvArgs)
            End If
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                InitializeVoiceObject()
                ShowMicroPhone()
            End If
            'If blnTemplateId <> 0 Then

            '    Fill_TemplateGallery(blnTemplateId)
            '    blnTemplateExist = True
            'End If
            ''
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, m_PatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    'Private Sub Fill_TemplateGallery(ByVal TemplateID As Long)

    '    Dim strFileName As String
    '    ObjWord = New clsWordDocument
    '    objCriteria = New DocCriteria
    '    objCriteria.DocCategory = enumDocCategory.Template
    '    objCriteria.PrimaryID = TemplateID

    '    ObjWord.DocumentCriteria = objCriteria
    '    ''//Retrieving the Referrals from DB and Save it as Physical File
    '    strFileName = ObjWord.RetrieveDocumentFile()
    '    objCriteria = Nothing
    '    ObjWord = Nothing
    '    If strFileName <> "" Then
    '        LoadWordUserControl(strFileName, True)
    '        'Set the Start postion of the cursor in documents
    '        oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
    '    End If
    'End Sub
    Private Sub Fill_TemplateGallery(ByVal Template As Object)

        Dim strFileName As String
        ObjWord = New clsWordDocument
        objCriteria = New DocCriteria
        objCriteria.DocCategory = enumDocCategory.Template
        'Commented by shweta 20100107
        'Against the bug id:5611
        'objCriteria.PrimaryID = TemplateID
        'End commenting

        ObjWord.DocumentCriteria = objCriteria
        ''//Retrieving the Referrals from DB and Save it as Physical File
        'Changed by shweta 20100107
        'Against the bug id:5611
        strFileName = ExamNewDocumentName
        ObjWord.GenerateFile(Template, strFileName)
        'End changes
        objCriteria.Dispose()
        objCriteria = Nothing
        ObjWord = Nothing
        If strFileName <> "" Then
            LoadWordUserControl(strFileName, True)
            'Set the Start postion of the cursor in documents
            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        End If
    End Sub

    'sarika DM Denormalization
    Private Sub Fill_TemplateGallery(ByVal myNode As myTreeNode)

        Dim strFileName As String
        ObjWord = New clsWordDocument

        If IsDBNull(myNode.DMTemplate) = False Then
            strFileName = ExamNewDocumentName
            '' generate Physical file
            strFileName = ObjWord.GenerateFile(myNode.DMTemplate, strFileName)

        Else
            strFileName = ""
        End If

        ObjWord = Nothing
        If strFileName <> "" Then
            LoadWordUserControl(strFileName, True)
            'Set the Start postion of the cursor in documents
            oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        End If
    End Sub

    '---

    Private Sub Print()

        If Not oCurDoc Is Nothing Then
            GeneratePrintFaxDocument()

            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Patient Due Template Document Printed.", gstrLoginName, gstrClientMachineName, m_PatientID)
        End If

    End Sub

    Private Sub GeneratePrintFaxDocument(Optional ByVal IsPrintFlag As Boolean = True)
        If Not oCurDoc Is Nothing Then
            Dim PageNo As Integer = 0
            Dim totalPages As Integer = 0
            Dim PageCountStat As Microsoft.Office.Interop.Word.WdStatistic = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages
            Dim Missing As Object = System.Reflection.Missing.Value

            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            Dim _SaveFlag As Boolean = False
            If oCurDoc.Saved Then
                _SaveFlag = True
            End If
            '   Dim sFileName As String = ExamNewDocumentName
            If IsNothing(wdDMTemplate) = False AndAlso IsNothing(oWordApp) = False Then
                Try
                    gloWord.LoadAndCloseWord.SaveDSO(wdDMTemplate, oCurDoc, oWordApp)
                Catch ex As Exception

                End Try
                If (IsPrintFlag) Then
                    Try
                        PageNo = oCurDoc.Application.Selection.Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber)
                    Catch ex As Exception

                    End Try
                    Try
                        totalPages = oCurDoc.ComputeStatistics(PageCountStat, Missing)
                    Catch ex As Exception

                    End Try

                End If


                'oTempDoc = wdTemp.ActiveDocument
                Dim myLoadWord As gloWord.LoadAndCloseWord = New gloWord.LoadAndCloseWord()
                Try
                    PrintAndFaxWord.ClsPrintOrFax.PrintOrFaxWordDocument(myLoadWord, oCurDoc.FullName, IsPrintFlag, m_PatientID, AddressOf FaxDueTemplate, totalPages, PageNo:=PageNo, iOwner:=Me)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try


                '  Dim oTempDoc As Wd.Document = myLoadWord.LoadWordApplication(sFileName)

                'If IsPrintFlag Then
                '    'If oTempDoc.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
                '    '    oTempDoc.Unprotect()
                '    'End If
                '    Dim oPrint As New clsPrintFAX
                '    oPrint.PrintDoc(oTempDoc, m_PatientID)
                '    oPrint.Dispose()
                '    oPrint = Nothing
                'Else
                '    Call FaxDueTemplate(myLoadWord, oTempDoc)
                'End If

                'oTempDoc = Nothing
                'wdTemp.Close()
                'Me.Controls.Remove(wdTemp)
                'wdTemp.Dispose()
                'myLoadWord.CloseWordApplication(oTempDoc)
                myLoadWord.CloseApplicationOnly()
                myLoadWord = Nothing
                If Not IsNothing(oCurDoc) Then
                    oCurDoc.Saved = _SaveFlag
                End If
                'LoadWordUserControl(sFileName, False)
                'oCurDoc.ActiveWindow.View.ShowFieldCodes = False
                ''Set the Start postion of the cursor in documents
                'oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
                'oCurDoc.Saved = _SaveFlag
            End If
        End If


    End Sub
    'Private Sub tblTemplate_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&Print"
    '                Print()
    '                Call SaveTemplate(Type.Print)
    '            Case "Sign"
    '                Call InsertProviderSignature()

    '            Case "&Capture"
    '                Call InsertSignature()
    '            Case "&Fax"
    '                Call FaxDueTemplate()
    '            Case "&Save"
    '                Call SaveTemplate(Type.Save)
    '                ''Me.Close()
    '            Case "&Close"
    '                If IsNothing(oCurDoc) = False Then
    '                    If oCurDoc.Saved = False Then
    '                        Dim Result As Int16
    '                        Result = MsgBox("Do you want to save the Guideline?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel)
    '                        If Result = MsgBoxResult.Yes Then
    '                            Call SaveTemplate(Type.Save, True)
    '                            'Me.Close()
    '                        ElseIf Result = MsgBoxResult.Cancel Then
    '                            '' Nothing to here
    '                        ElseIf Result = MsgBoxResult.No Then
    '                            'Me.Close()
    '                        End If
    '                    Else
    '                        'Me.Close()
    '                    End If
    '                Else
    '                    'Me.Close()
    '                End If

    '                If Get_NON_GuidelinePatients() = True Then
    '                    Me.Close()
    '                End If

    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Function Get_NON_GuidelinePatients() As Boolean
    '    Dim nde As myTreeNode
    '    Dim COL_NON_GuidelinePatient As New Collection
    '    Dim strPatients As String = ""
    '    For i As Integer = 0 To trvPatients.GetNodeCount(False) - 1
    '        nde = New myTreeNode
    '        nde = CType(trvPatients.Nodes(i), myTreeNode)
    '        If nde.NodeName = "" Then
    '            '' Get the Patients against which Guidelines are not Saved Or Printed Or Fax   
    '            If strPatients = "" Then
    '                strPatients = nde.Text
    '            Else
    '                strPatients = strPatients & vbCrLf & nde.Text
    '            End If
    '            COL_NON_GuidelinePatient.Add(nde.Tag & "|" & nde.Text)
    '        End If
    '    Next

    '    If strPatients <> "" Then
    '        Dim Result As DialogResult
    '        '' If There Exist Some Patients to whom Guidelines are not given
    '        Result = MessageBox.Show("There are some Patients to whom Guidelines are not given, Do you want to document the reasons?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
    '        If Result = Windows.Forms.DialogResult.Yes Then
    '            Dim frm As New frmDM_Resons(_CriteriaID, _TemplateID, COL_NON_GuidelinePatient)
    '            With frm
    '                .ShowInTaskbar = False
    '                .ShowDialog(Me)
    '            End With
    '            Return True
    '        ElseIf Result = Windows.Forms.DialogResult.No Then
    '            For i As Integer = 1 To COL_NON_GuidelinePatient.Count
    '                Dim str() As String
    '                str = Split(COL_NON_GuidelinePatient(i), "|", 2)

    '                oclsDM_Tempalte.Save_Trigger(0, str(0), _CriteriaID, _TemplateID, Format(Now, "MM/dd/yyyy"), "", frmDM_Template.Type.Save, True, "Prompted to User, User canceled the Action")
    '            Next

    '            Return True
    '        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
    '            Return False
    '        End If

    '    Else
    '        Return True
    '    End If

    'End Function

    Private Sub FaxDueTemplate(ByRef myLoadWord As gloWord.LoadAndCloseWord, ByVal oTempDoc As String)

        CurrentSendingFAXPriority = mdlFAX.enmFAXPriority.NormalPriority
        'Send the document for Printing i.e. to generate the TIFF File
        UpdateLog("Clicked on FAX button")
        UpdateLog("Creating the object of Class")
        Dim objPrintFAX As New clsPrintFAX(gstrFAXPrinterName)
        UpdateLog("Object Created - Retrieving FAX Details")
        mdlFAX.Owner = Me
        If RetrieveFAXDetails(mdlFAX.enmFAXType.PatientExam, CStr(m_PatientID), "", "", "", 0, 0, 0, True, Me) = False Then
            Exit Sub
        End If
        UpdateLog("FAX Details - FAX No, FAX To, Cover Page retrieved")


        'Commented by Shweta 20100201
        '''''''Against the bug id:5260 '''''''
        'Check the FAX Cover Page is enabled or not.
        'If the FAX Cover Page is enabled then Delete the Page Header from Exam
        'If gblnFAXCoverPage Then
        '    'Unprotect the document
        '    ' If blnExamFinished Then
        '    UpdateLog("Unprotecting Document")
        '    If oTempDoc.Application.ActiveDocument.ProtectionType = Wd.WdProtectionType.wdAllowOnlyComments Then
        '        oTempDoc.Application.ActiveDocument.Unprotect()
        '    End If
        '    UpdateLog("Document UnProtected")
        '    'End If

        '    'To Delete Header
        '    UpdateLog("Deleting DueTemplate Page Header")
        '    Try

        '        If oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdNormalView Or oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdOutlineView Then
        '            oTempDoc.ActiveWindow.ActivePane.View.Type = Wd.WdViewType.wdPrintView
        '        End If
        '        oTempDoc.Activate()
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekPrimaryHeader

        '        If oTempDoc.Application.Selection.HeaderFooter.IsHeader Then
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Select()
        '            oTempDoc.Application.Selection.HeaderFooter.Range.Delete()
        '            UpdateLog("DueTemplate Page Header deleted")
        '        End If

        '    Catch ex As Exception
        '        UpdateVoiceLog("Error Deleting DueTemplate Page Header - " & ex.ToString)
        '    Finally
        '        oTempDoc.ActiveWindow.ActivePane.View.SeekView = Wd.WdSeekView.wdSeekMainDocument
        '    End Try
        'End If
        'End Commenting

        UpdateLog("Calling FAX Document method")
        If objPrintFAX.FAXDocument(myLoadWord, oTempDoc, CStr(m_PatientID), gstrFAXContactPerson, gstrFAXContactPersonFAXNo, gstrLoginName, System.DateTime.Now, "", clsPrintFAX.enmFAXType.PatientExam) = False Then
            If Trim(objPrintFAX.ErrorMessage) <> "" Then
                MessageBox.Show("Unable to send the FAX due to " & objPrintFAX.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        UpdateLog("DueTemplate Faxed")
        'oTempDoc = Nothing
        objPrintFAX.Dispose()
        objPrintFAX = Nothing
    End Sub

    Private Sub loadToolStrip()
        If Not tlsDM Is Nothing Then
            tlsDM.Dispose()
        End If
        tlsDM = New WordToolStrip.gloWordToolStrip
        tlsDM.Dock = DockStyle.Top

        tlsDM.ConnectionString = GetConnectionString()
        tlsDM.UserID = gnLoginID

        tlsDM.IsCoSignEnabled = gblnCoSignFlag
        tlsDM.FormType = WordToolStrip.enumControlType.DiseaseManagement
        Me.pnlMiddle.Controls.Add(tlsDM)
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.MyWordToolStrip = tlsDM
                ShowMicroPhone()
            End If
        End If
        '        If gnLoginProviderID = 0 And gblnAssociatedProviderSignature And m_IsReadOnly = False And m_IsRecordLock = False Then
        If gnLoginProviderID = 0 And gblnAssociatedProviderSignature Then
            tlsDM.MyToolStrip.Items("Insert Associated Provider Signature").Visible = True
            tlsDM.MyToolStrip.ButtonsToHide.Remove(tlsDM.MyToolStrip.Items("Insert Associated Provider Signature").Name)
        Else
            tlsDM.MyToolStrip.Items("Insert Associated Provider Signature").Visible = False
            If (tlsDM.MyToolStrip.ButtonsToHide.Contains(tlsDM.MyToolStrip.Items("Insert Associated Provider Signature").Name) = False) Then
                tlsDM.MyToolStrip.ButtonsToHide.Add(tlsDM.MyToolStrip.Items("Insert Associated Provider Signature").Name)
            End If
        End If
    End Sub

    Public Sub InsertCoSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            ObjWord = New clsWordDocument
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end modification

            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID '' For inserting coSignature
            ObjWord.DocumentCriteria = objCriteria

            Imagepath = ObjWord.getData_FromDB("User_MST.imgSignature", "Co-Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            ObjWord = Nothing
            Imagepath = Mid(Imagepath, 1, Len(Imagepath) - 2)

            If System.IO.File.Exists(Imagepath) Then
                oCurDoc.ActiveWindow.SetFocus()

                '' SUDHIR 20090619 '' 
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(Imagepath)
                oWord = Nothing
                'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=Imagepath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

                oCurDoc.Application.Selection.TypeParagraph()
                '' By Mahesh Signature With Date - 20070113
                '''' Add Date Time When Signature is Inserted
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                ''''
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' to insert user's signature
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InsertUserSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If

            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Exam
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end exam
            objCriteria.VisitID = 0
            objCriteria.PrimaryID = gnLoginID
            objWord.DocumentCriteria = objCriteria

            Imagepath = objWord.getData_FromDB("User_MST.imgSignature", "Provider Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing
            Imagepath = Mid(Imagepath, 1, Len(Imagepath) - 2)

            If File.Exists(Imagepath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(Imagepath)
                oWord = Nothing
                'code added by dipak 20100118 to fixe bug no 5741 :Patient EXAM Sigin
                Dim wdRng As Wd.Range = oCurDoc.Application.Selection.Range
                If wdRng.Tables.Count > 0 Then
                    'oCurDoc.Application.Selection.Move(1)
                    oCurDoc.Application.Selection.EndKey()
                End If
                'end code added by dipak 
                oCurDoc.Application.Selection.TypeParagraph()
                'Dim clsExam As New clsPatientExams
                'clsExam.Dispose()
                'clsExam = Nothing
                oCurDoc.Application.Selection.TypeText(Text:="Signed by User :" & " '" & gstrLoginName & "'. " & Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "User Signature Inserted from DM_Template", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        End Try

    End Sub
    Public Sub InsertProviderSignature()
        Try
            If oCurDoc Is Nothing Then
                Exit Sub
            End If
            Dim objWord As New clsWordDocument
            Dim objCriteria As DocCriteria
            objCriteria = New DocCriteria
            objCriteria.DocCategory = enumDocCategory.Others
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end modification

            objCriteria.VisitID = m_VisitID
            objCriteria.PrimaryID = 0
            objWord.DocumentCriteria = objCriteria
            ''// Get the path of the image of Provider Signature
            Imagepath = objWord.getData_FromDB("Provider_MST.imgSignature", "Provider Signature")
            objCriteria.Dispose()
            objCriteria = Nothing
            objWord = Nothing

            Imagepath = Mid(Imagepath, 1, Len(Imagepath) - 2)


            If File.Exists(Imagepath) Then
                'Set focus to Wd object
                oCurDoc.ActiveWindow.SetFocus()

                '' SUDHIR 20090619 '' 
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(Imagepath)
                oWord = Nothing
                'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=Imagepath, LinkToFile:=False, SaveWithDocument:=True)
                '' END SUDHIR ''

                oCurDoc.Application.Selection.TypeParagraph()
                '' By Mahesh Signature With Date - 20070113
                ''''' Add Date Time When Signature is Inserted
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.InsertSignature, gloAuditTrail.ActivityType.General, "Provider Signature Inserted", m_PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                '''''
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub SaveTemplate(ByVal Type As Type, Optional ByVal IsFinish As Boolean = False)

        If oCurDoc Is Nothing Then
            Exit Sub
        End If

        Dim Nde As New myTreeNode

        Dim str As String = ""

        If Type.Save Then
            If Nde.NodeName = "" Then
                str = "S"
            Else
                str = "|" & "S"
            End If
        ElseIf Type.Print Then
            If Nde.NodeName = "" Then
                str = "P"
            Else
                str = "|" & "P"
            End If
        ElseIf Type.Fax Then
            If Nde.NodeName = "" Then
                str = "F"
            Else
                str = "|" & "F"
            End If
        End If

        Nde.NodeName = str
        
        'Dim strFileName As String
        'strFileName = ExamNewDocumentName
        '' wdDMTemplate.Save(strFileName, True, "", "")
        'oCurDoc.SaveAs(strFileName, Wd.WdSaveFormat.wdFormatXMLDocument, False, "", False)
        'wdDMTemplate.Close()
        Dim myByte As Byte() = gloWord.LoadAndCloseWord.SaveWordFiletoBinary(wdDMTemplate, oCurDoc, oWordApp, gloSettings.FolderSettings.AppTempFolderPath, False, IsFinish)

        Dim myBinaray As Object = Nothing
        If (IsNothing(myByte) = False) Then
            myBinaray = CType(myByte, Object)
        End If
        'objclsPatientEducation.SaveExamEducation(m_VisitID, m_patientID, strTempFileName1, cmbTemplate.Text)
        ''Bug No 49555::20130422::CDS-Recommendation-Application is showing the error while saving guideline templates
        If Not IsNothing(trvTemplate.SelectedNode.Parent) Then
            oclsDM_Tempalte.Save_TemplateBytes(0, m_PatientID, trvTemplate.SelectedNode.Parent.Tag, trvTemplate.SelectedNode.Tag, myBinaray, Type)
        Else
            oclsDM_Tempalte.Save_TemplateBytes(0, m_PatientID, 0, trvTemplate.SelectedNode.Tag, myBinaray, Type)
        End If

        If Not m_Guidelines Is Nothing Then
            If m_Guidelines.Count > 0 Then
                For i As Integer = 0 To m_Guidelines.Count - 1
                    Dim blnIsPresent As Boolean = False
                    Dim lst As myList
                    lst = CType(m_Guidelines(i), myList)

                    If lst.ID = trvTemplate.SelectedNode.Tag Then
                        lst.IsFinished = True
                        Exit For
                    End If
                Next
            End If
        End If

        ''Sandip Darade  20100303 BUG ID 6212
        If IsFinish = True Then
            Dim obj As Object
            For Each obj In m_Guidelines
                Dim lst As myList
                lst = CType(obj, myList)
                lst.IsFinished = True
            Next

        End If
        
        If IsFinish Then
            If (IsNothing(oCurDoc) = False) Then
                Try
                    Marshal.ReleaseComObject(oCurDoc)
                Catch ex As Exception


                End Try
                oCurDoc = Nothing

            End If
        End If

        'If IsFinish = False Then

        '    LoadWordUserControl(strFileName, False)
        '    'Set the Start postion of the cursor in documents
        '    oCurDoc.Application.Selection.HomeKey(Wd.WdUnits.wdStory)
        '    oCurDoc.Saved = True
        'End If

    End Sub

    'Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Trim(txtSearch.Text) <> "" Then
    '            If trvPatients.GetNodeCount(False) > 0 Then
    '                Dim mychildnode As TreeNode
    '                'child node collection

    '                For Each mychildnode In trvPatients.Nodes
    '                    Dim str As String
    '                    str = UCase(Trim(mychildnode.Text))
    '                    If Mid(str, 1, Len(Trim(txtSearch.Text))) = UCase(Trim(txtSearch.Text)) Then
    '                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
    '                        trvPatients.SelectedNode = trvPatients.SelectedNode.LastNode
    '                        '*************
    '                        trvPatients.SelectedNode = mychildnode
    '                        txtSearch.Focus()
    '                        Exit Sub
    '                    End If
    '                Next
    '            End If
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If trvTemplate.GetNodeCount(False) > 0 Then
    '        If (e.KeyChar = ChrW(13)) Then
    '            trvTemplate.Select()
    '            'Else
    '            '    trvSource.SelectedNode = trvSource.Nodes.Item(0)
    '        End If
    '    End If
    'End Sub

    Public WriteOnly Property ImageFilePath() As String Implements mdlGeneral.ISignature.ImageFilePath
        Set(ByVal Value As String)
            Imagepath = Value
        End Set
    End Property
    Public Sub InsertSignature()
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        Try
            Imagepath = ""

            Dim frm As New FrmSignature
            frm.Owner = Me
            ' frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            frm.ShowDialog(frm.Parent)
            frm.Dispose()
            ''commented by Dhruv 20091214 
            ''To not to save on form closing
            'If File.Exists(Imagepath) Then
            '    'Set focus to Wd object
            '    oCurDoc.ActiveWindow.SetFocus()

            '    '' SUDHIR 20090619 '' 
            '    Dim oWord As New clsWordDocument
            '    oWord.CurDocument = oCurDoc
            '    oWord.InsertImage(Imagepath)
            '    oWord = Nothing
            '    'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=Imagepath, LinkToFile:=False, SaveWithDocument:=True)
            '    '' END SUDHIR ''

            '    oCurDoc.Application.Selection.TypeParagraph()
            '    '' By Mahesh Signature With Date - 20070113
            '    ''''' Add Date Time When Signature is Inserted
            '    oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
            '    '''''
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''Dhruv 20091214 To add the signature into the Word document
    Public Sub AddSignature(ByVal sImagePath As String) Implements ISignature.AddSignature

        If Not IsNothing(oCurDoc) Then
            If File.Exists(sImagePath) Then
                oCurDoc.ActiveWindow.SetFocus()
                Dim oWord As New clsWordDocument
                oWord.CurDocument = oCurDoc
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                oWord.InsertImage(sImagePath)
                oWord = Nothing
                oCurDoc.Application.Selection.TypeParagraph()
                oCurDoc.Application.Selection.TypeText(Text:=Format(Now, "MM/dd/yyyy") & " " & Format(Now, "Medium Time"))
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
            End If
        End If
    End Sub
    Private Sub trvTemplate_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    End Sub

    'Private Sub trvPatients_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
    '    Try
    '        '' Modified by Anil on 20071218
    '        If Not IsNothing(trvPatients.SelectedNode) Then
    '            m_PatientID = trvPatients.SelectedNode.Tag
    '        End If
    '        loadPatientStrip()
    '        ''
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub loadPatientStrip()
        ''Modified by Anil on 20071218
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(m_PatientID, gloUC_PatientStrip.enumFormName.HealthPlan)
        _PatientStrip.Dock = DockStyle.Top
        'If pnlMiddle.Controls.Contains(_PatientStrip) = False Then
        pnlMiddle.Controls.Add(_PatientStrip)
        'End If
        ''loadToolStrip()
    End Sub

    Private Sub UnDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Undo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ReDoChanges()
        Try
            If Not IsNothing(oCurDoc) Then
                oCurDoc.Redo()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    Private Sub ImportDocument(ByVal nInsertScan As Int16)
        'Insert File - 1
        'Scan Images - 2
        If oCurDoc Is Nothing Then
            Exit Sub
        End If
        'Set focus to Wd object
        oCurDoc.ActiveWindow.SetFocus()
        Try
            If nInsertScan = 1 Then
                Dim oFileDialogWindow As New System.Windows.Forms.OpenFileDialog
                oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf"
                ' oFileDialogWindow.Filter = "Text Files|*.txt|Wd Documents|*.doc|Rich Text Format|*.rtf"
                '//oFileDialogWindow.Filter = "Text Files (*.txt)|*.txt|Wd Documents (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf"
                oFileDialogWindow.FilterIndex = 2
                oFileDialogWindow.Title = "Insert External Documents"
                oFileDialogWindow.Multiselect = False
                If oFileDialogWindow.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                    Dim oFile As FileInfo = New FileInfo(oFileDialogWindow.FileName)
                    If oFile.Extension.ToUpper = UCase(".Doc") Or oFile.Extension.ToUpper = UCase(".Docx") Or oFile.Extension.ToUpper = UCase(".txt") Or oFile.Extension.ToUpper = UCase(".rtf") Then

                        ''''' Statement to Go end of Selelcted Wd Document
                        '//oCurDoc1.ActiveWindow.Selection.EndKey(Wd.WdUnits.wdStory)
                        'Insert file in Wd dobject
                        oCurDoc.Application.Selection.InsertFile(oFile.FullName)
                    End If
                End If
                oFileDialogWindow.Dispose()
                oFileDialogWindow = Nothing
            ElseIf nInsertScan = 2 Then
                'Dim frmObj As New frmDMS_ScannedDocumentEvent_TwainPro
                'Dim _Files As New Collection
                'frmObj.blnDMSScan = False
                'frmObj.ShowDialog(Me)
                '_Files = frmObj._NonDMSFileCollection

                Dim oFiles As New ArrayList()
                Dim oEDocument As New gloEDocumentV3.gloEDocV3Management()

                'Commented BY Rahul Patel on 26-10-2010
                'gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'Added by Rahul Patel on 26-10-2010
                'For changing the DMS Hybrid database change.
                gloEDocumentV3.gloEDocV3Admin.Connect(GetConnectionString(), GetDMSConnectionString(), gDMSV2TempPath, gnLoginID, gClinicID, Application.StartupPath)
                'End of code added by Rahul Patel on 26-10-2010

                ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                'oEDocument.ShowEScannerForImages(gnPatientID, oFiles)
                oEDocument.ShowEScannerForImages(m_PatientID, oFiles)
                'end modify
                oEDocument.Dispose()

                Dim firstFlag As Boolean = True
                Dim i As Integer
                Dim myType As Wd.WdViewType = Nothing
                Dim myLayout As Boolean = gloWord.LoadAndCloseWord.ChangeToEditView(oCurDoc, myType)
                For i = 0 To oFiles.Count - 1
                    If File.Exists(oFiles.Item(i)) Then

                        '' SUDHIR 20090619 '' 
                        Dim oWord As New clsWordDocument
                        oWord.GetandSetMyFirstFlag(True, firstFlag)
                        oWord.CurDocument = oCurDoc
                        oWord.InsertImage(oFiles.Item(i))
                        firstFlag = oWord.GetandSetMyFirstFlag(False, False)
                        oWord = Nothing
                        'oCurDoc.Application.Selection.InlineShapes.AddPicture(FileName:=oFiles.Item(i), LinkToFile:=False, SaveWithDocument:=True)
                        '' END SUDHIR ''

                        oCurDoc.Application.Selection.EndKey()
                        oCurDoc.Application.Selection.InsertBreak()

                    End If
                Next
                gloWord.LoadAndCloseWord.RestoreFromEditView(oCurDoc, myType, myLayout)
                For i = oFiles.Count - 1 To 0 Step -1
                    If File.Exists(oFiles.Item(i)) Then
                        Try
                            Kill(oFiles.Item(i))
                        Catch
                        End Try

                    End If
                Next

                ''frmObj = Nothing
                i = Nothing
                ''_Files = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally

        End Try
    End Sub

    Private Sub LoadWordUserControl(ByVal strFileName As String, Optional ByVal blnGetData As Boolean = False)
        ''//Open Template for processing in user Ctrl
        Try
            wdDMTemplate.ActivationPolicy = DSOFramer.dsoActivationPolicy.dsoDefaultBehavior
            wdDMTemplate.FrameHookPolicy = DSOFramer.dsoFrameHookPolicy.dsoSetOnFirstOpen
        Catch ex As Exception

        End Try

        'wdDMTemplate.Open(strFileName)
        ' Dim oWordApp As Wd.Application = Nothing
        gloWord.LoadAndCloseWord.OpenDSO(wdDMTemplate, strFileName, oCurDoc, oWordApp)

        ''//To retrieve the Form fields for the Word document
        If blnGetData Then
            objCriteria = New DocCriteria
            ObjWord = New clsWordDocument
            objCriteria.DocCategory = enumDocCategory.Others

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objCriteria.PatientID = gnPatientID
            objCriteria.PatientID = m_PatientID
            'end modification

            objCriteria.VisitID = m_VisitID
            objCriteria.PrimaryID = 0
            ObjWord.DocumentCriteria = objCriteria
            ObjWord.CurDocument = oCurDoc
            ''Replace Form fields with Concerned data
            ObjWord.GetFormFieldData(enumDocType.None)
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            objCriteria.Dispose()
            objCriteria = Nothing
        Else
            ObjWord = New clsWordDocument
            ObjWord.CurDocument = oCurDoc
            ObjWord.HighlightColor()
            oCurDoc = ObjWord.CurDocument
            oCurDoc.ActiveWindow.View.ShowFieldCodes = False
            ObjWord = Nothing

        End If
    End Sub

    Private Sub wdDMTemplate_BeforeDocumentClosed(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent) Handles wdDMTemplate.BeforeDocumentClosed
        Try
            If Not oWordApp Is Nothing Then
                Try
                    RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try

                frmPatientExam.blnIsHandlers = True
                For Each oFile As Wd.RecentFile In oWordApp.RecentFiles
                    If (IsNothing(oFile) = False) Then
                        Try
                            If oFile.Path = gloSettings.FolderSettings.AppTempFolderPath Then
                                Try
                                    oFile.Delete()
                                Catch ex As Exception
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                                    ex = Nothing
                                End Try
                            End If
                        Catch ex As Exception
                            
                        End Try
                    End If
                Next
            End If
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub


    Private Sub wdDMTempalate_OnDocumentClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles wdDMTemplate.OnDocumentClosed
        Try
            If Not oCurDoc Is Nothing Then
                Marshal.ReleaseComObject(oCurDoc)
                oCurDoc = Nothing
            End If
            'If Not oWordApp Is Nothing Then
            '    ' Marshal.FinalReleaseComObject(oWordApp)
            '    oWordApp = Nothing
            'End If
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
        Catch ex As Exception
            UpdateVoiceLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub wdDMTempalate_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdDMTemplate.OnDocumentOpened
        oCurDoc = wdDMTemplate.ActiveDocument
        oWordApp = oCurDoc.Application
        Try

            Try
                RemoveHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                ex = Nothing
            End Try

            AddHandler oWordApp.WindowSelectionChange, AddressOf DDLCBEvent
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        oCurDoc.ActiveWindow.SetFocus()
        oCurDoc.FormFields.Shaded = False
    End Sub

    '''' <summary>
    '''' To implemt the Dropdown and check Box selection change event
    '''' </summary>
    '''' <param name="Sel"></param>
    '''' <remarks></remarks>
    Private Sub DDLCBEvent(ByVal Sel As Wd.Selection)

        Try
            If IsNothing(Sel) Then
                Return
            End If
            If (Sel.Type <> Microsoft.Office.Interop.Word.WdSelectionType.wdNoSelection) Then
                If Sel.Start = Sel.End Then

                    Dim r As Wd.Range = Nothing
                    Try
                        r = Sel.Range
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    Try
                        r.SetRange(Sel.Start, Sel.End + 1)
                    Catch ex As Exception

                    End Try
                    If (IsNothing(r)) Then
                        Exit Sub
                    End If
                    ' r.SetRange(Sel.Start, Sel.End + 1)

                    If r.FormFields IsNot Nothing AndAlso r.FormFields.Count >= 1 Then

                        '  Dim om As Object = System.Reflection.Missing.Value

                        Dim f As Wd.FormField = Nothing



                        Try
                            Dim o As Object = 1
                            f = r.FormFields.Item(o)
                            o = Nothing
                        Catch

                        End Try

                        'If f.Type = Wd.WdFieldType.wdFieldFormDropDown Then

                        '    Dim dd As Wd.DropDown = f.DropDown

                        '    Dim iCurSel As Integer = dd.Value

                        '    Dim oPU As oOffice.CommandBar = oWordApp.CommandBars.Add("CustomFormFieldPopup", oOffice.MsoBarPosition.msoBarPopup, om, True)

                        '    If False Then

                        '        Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlDropdown, om, om, om, True), oOffice.CommandBarComboBox)

                        '        oDD.Style = oOffice.MsoComboStyle.msoComboLabel

                        '        oDD.DropDownLines = dd.ListEntries.Count

                        '        Dim le As Wd.ListEntry
                        '        For Each le In dd.ListEntries

                        '            oDD.AddItem(le.Name, om)

                        '        Next

                        '        oDD.ListIndex = iCurSel

                        '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

                        '        dd.Value = oDD.ListIndex

                        '    Else

                        '        myidx = dd.Value

                        '        Dim iter As Integer = 1

                        '        Dim le As Wd.ListEntry
                        '        For Each le In dd.ListEntries

                        '            Dim btn As oOffice.CommandBarButton
                        '            '     Dim oDD As oOffice.CommandBarComboBox = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                        '            btn = CType(oPU.Controls.Add(oOffice.MsoControlType.msoControlButton, om, om, om, True), oOffice.CommandBarButton)

                        '            btn.Style = oOffice.MsoButtonStyle.msoButtonAutomatic

                        '            btn.Caption = le.Name

                        '            btn.Enabled = True

                        '            If iter = myidx Then

                        '                btn.State = oOffice.MsoButtonState.msoButtonDown
                        '            End If

                        '            iter = iter + 1

                        '            ' btn.Click += New Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(btn_Click)
                        '            AddHandler btn.Click, AddressOf btn_Click
                        '        Next

                        '        CType(oPU, oOffice.CommandBar).ShowPopup(om, om)

                        '        dd.Value = myidx

                        '    End If

                        'End If
                        If (IsNothing(f) = False) Then

                            If f.Type = Wd.WdFieldType.wdFieldFormCheckBox Then

                                f.CheckBox.Value = Not f.CheckBox.Value

                                Dim oUnit As Object = Wd.WdUnits.wdCharacter

                                Dim oCnt As Object = 1

                                Dim oMove As Object = Wd.WdMovementType.wdMove

                                Sel.MoveRight(oUnit, oCnt, oMove)

                            End If

                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try

    End Sub

    '''' <summary>
    '''' To raise the click event for drop down list
    '''' </summary>
    '''' <param name="btn"></param>
    '''' <param name="Cancel"></param>
    '''' <remarks></remarks>
    Private Sub btn_Click(ByVal btn As oOffice.CommandBarButton, ByRef Cancel As Boolean)
        myidx = btn.Index
    End Sub

    Private Sub tlsDM_ToolStripClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDM.ToolStripClick
        Try
            Select Case e.ClickedItem.Name
                Case "Mic"
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic started from tblButtons_ButtonClick in PatientConsent when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                    'UpdateVoiceLog("--------------SwitchOff Mic started from tblButtons_ButtonClick in Patient Messages when " & e.ClickedItem.Name & " is invoked------------")
                    If MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_OFF
                        e.ClickedItem.ToolTipText = "Microphone Off"
                    ElseIf MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff Then
                        MyMDIParent.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn
                        e.ClickedItem.Image = Global.gloEMR.My.Resources.Mic_ON
                        e.ClickedItem.ToolTipText = "Microphone On"
                    End If
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.Load, "SwitchOff Mic Completed from tblButtons_ButtonClick in PatientConsent when " & e.ClickedItem.Name & " is invoked", gloAuditTrail.ActivityOutCome.Success)
                Case "Save"
                    TurnoffMicrophone()
                    Call SaveTemplate(Type.Save)
                Case "Save & Close"
                    Call SaveTemplate(Type.Save, True)
                    Me.Close()
                Case "Print"
                    TurnoffMicrophone()
                    Call Print()
                    Call SaveTemplate(Type.Print)
                Case "FAX"
                    TurnoffMicrophone()
                    Call GeneratePrintFaxDocument(False)
                    'Case "Insert Sign"
                    '    Call InsertProviderSignature()
                    'Case "Insert Associated Provider Signature"
                    '    Call InsertProviderSignature()
                Case "Insert Sign"
                    'Call InsertProviderSignature() 'if user is provider then insert provider sign lese user's sign
                    If IsNothing(oCurDoc) = False Then
                        'If else condition added by dipak as allow user to add sign
                        If gnLoginProviderID > 0 Then
                            InsertProviderSignature()
                        Else
                            InsertUserSignature()
                        End If
                        'end code added by dipak 20100105
                    End If
                    'case added by dipak 20100105 for ProviderSign 
                Case "Insert Associated Provider Signature"
                    If IsNothing(oCurDoc) = False Then
                        InsertProviderSignature()
                    End If

                Case "Insert CoSign"
                    Call InsertCoSignature()
                Case "Capture Sign"
                    Call InsertSignature()
                Case "Undo"
                    Call UnDoChanges()
                Case "Redo"
                    Call ReDoChanges()
                Case "Insert File"
                    TurnoffMicrophone()
                    ImportDocument(1)
                Case "Scan Documents"
                    TurnoffMicrophone()
                    ImportDocument(2)
                Case "Close"
                    If IsNothing(oCurDoc) = False Then
                        If oCurDoc.Saved = False Then
                            Dim Result As DialogResult
                            Result = MessageBox.Show("Do you want to save the Patient Guidelines?", "Disease Management", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                            If Result = Windows.Forms.DialogResult.Yes Then
                                Call SaveTemplate(Type.Save, True)
                                Me.Close()
                            ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                                '' Nothing to here
                            ElseIf Result = Windows.Forms.DialogResult.No Then
                                Me.Close()
                            End If
                        Else
                            Me.Close()
                        End If
                    Else
                        Me.Close()
                    End If
                Case "Export"
                    ' Export Function for Word Docs Integrated by Dipak  as on 26 oct 2010
                    Dim objword1 As clsWordDocument
                    objword1 = New clsWordDocument
                    Dim Result As Boolean = objword1.ExportData(oCurDoc, "", True, "Patient Exam", Me)
                    If Result = True Then
                        MessageBox.Show(" Patient Exam Document Exported Successfully ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    objword1 = Nothing
                    'Export Function for Word Docs Integrated by dipak  as on 26 oct 2010

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Private Sub trvTemplate_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvTemplate.NodeMouseDoubleClick
        Try
            trvTemplate.SelectedNode = e.Node

            If IsNothing(trvTemplate.SelectedNode) Then
                ''Validate the selected node for field node but should not be Parent or table node
                Exit Sub
                '    ''validate the node is parent node
            ElseIf CType(trvTemplate.SelectedNode, myTreeNode).Key = -1 Then
                Exit Sub

            Else
                If Not oCurDoc Is Nothing Then
                    If oCurDoc.Saved = False Then
                        Dim Result As DialogResult
                        Result = MessageBox.Show("Do you want to save the Patient Guidelines?", "Disease Management", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                        If Result = Windows.Forms.DialogResult.Yes Then
                            Call SaveTemplate(Type.Save, True)
                        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                            Exit Sub
                        ElseIf Result = Windows.Forms.DialogResult.No Then
                            wdDMTemplate.Close()
                        End If
                    End If
                End If
                'sarika DM Denormalization 20090403
                'Call Fill_TemplateGallery(trvTemplate.SelectedNode.Tag)
               ' Call Fill_TemplateGallery(CType(trvTemplate.SelectedNode, myTreeNode))
                '---
                ''Sandip Darade 20090819             
                Call Fill_TemplateGallery(CType(CType(trvTemplate.SelectedNode, myTreeNode).DMTemplate, Object))  ''CType(trvTemplate.SelectedNode.Tag, Long))

                blnTemplateExist = True
                'Else
                ''    Call InsertTemplate(trvTemplate.SelectedNode.Tag)
                ' End If
                If Not oCurDoc Is Nothing Then
                    oCurDoc.ActiveWindow.Application.ActiveDocument.SpellingChecked = True
                    oCurDoc.ActiveWindow.Application.ActiveDocument.ShowGrammaticalErrors = False
                    oCurDoc.ActiveWindow.View.WrapToWindow = True
                    ''''' Statement to Go start of Selelcted Wd Document

                    oCurDoc.ActiveWindow.Selection.HomeKey(Wd.WdUnits.wdStory)
                    oCurDoc.Application.Selection.MoveRight(1, 1)
                    oCurDoc.Application.Selection.MoveLeft(1, 1)
                    oCurDoc.Saved = False
                End If
                End If
                'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CType(Me.ParentForm, MainMenu).ShowHideMainMenu(True, True)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Trigger Voice commands
    ''' </summary>
    ''' <param name="VoiceCol"></param>
    ''' <remarks></remarks>
    Public Sub ActivateBasicVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateBasicVoiceCmds

    End Sub

    Public Sub ActivateVoiceCmds(ByVal VoiceCol As DNSTools.DgnStrings) Implements mdlgloVoice.gloVoice.ActivateVoiceCmds
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ActivateVoiceCmds(VoiceCol)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Add voice commands from custom collection to DgnStrings
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AddVoiceCommands() Implements mdlgloVoice.gloVoice.AddVoiceCommands
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.AddVoiceCommands()
            End If
        End If
    End Sub

    Public Sub CustomGetchanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_GetChangesEvent) Implements mdlgloVoice.gloVoice.CustomGetchanges

    End Sub

    Public Sub CustomMakechanges(ByVal e As AxDNSTools._DDgnDictCustomEvents_MakeChangesEvent) Implements mdlgloVoice.gloVoice.CustomMakechanges

    End Sub
    ''' <summary>
    ''' Show microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowMicroPhone() Implements mdlgloVoice.gloVoice.ShowMicroPhone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.ShowMicroPhone()
            End If

        End If
    End Sub
    ''' <summary>
    ''' Turnoff microphone
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TurnoffMicrophone() Implements mdlgloVoice.gloVoice.TurnoffMicrophone
        If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
            If Not IsNothing(ogloVoice) Then
                ogloVoice.TurnoffMicrophone()
            End If

        End If
    End Sub
    ''' <summary>
    ''' Add Basic Voice commands to hashtable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddBasicVoiceCommands() As Hashtable

        Dim oHashtable As New Hashtable
        oHashtable.Clear()
        oHashtable.Add("Save Disease Management", "Save")
        oHashtable.Add("Print Disease Management", "Print")
        oHashtable.Add("Fax Disease Management", "FAX")
        oHashtable.Add("Save and Close", "Save & Close")
        oHashtable.Add("Save and Close Disease Management", "Save & Close")
        oHashtable.Add("Insert Signature", "Insert Sign")
        oHashtable.Add("Close Disease Management", "Close")

        Return oHashtable
    End Function
    ''' <summary>
    ''' Initialise glovoice class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeVoiceObject()
        If Not IsNothing(ogloVoice) Then
            ogloVoice.Dispose()
            ogloVoice = Nothing
        End If
        Dim oHashtable As Hashtable = AddBasicVoiceCommands()
        ogloVoice = New ClsVoice(oHashtable)
        ogloVoice.eActivityModule = gloAuditTrail.ActivityModule.DiseaseManagement
        ogloVoice.MyWordToolStrip = Me.tlsDM
        ogloVoice.MDIParentVoice = MyMDIParent
        ogloVoice.MessageName = "Disease Management"

        ogloVoice.InitializeVoiceComponents()
        ogloVoice.DelWordToolStripClick = New HandleWordToolStripClick(AddressOf tlsDM_ToolStripClick)
    End Sub

    Public Sub Navigate(ByVal strstring As String) Implements mdlHotkey.IHotKey.Navigate
        If strstring = "ON" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsDM.MyToolStrip.Items("Mic").Visible = True
                tlsDM.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_ON
                tlsDM.MyToolStrip.ButtonsToHide.Remove(tlsDM.MyToolStrip.Items("Mic").Name)
            End If
        ElseIf strstring = "OFF" Then
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                tlsDM.MyToolStrip.Items("Mic").Visible = True
                tlsDM.MyToolStrip.ButtonsToHide.Remove(tlsDM.MyToolStrip.Items("Mic").Name)
            Else
                tlsDM.MyToolStrip.Items("Mic").Visible = False
                If tlsDM.MyToolStrip.ButtonsToHide.Contains(tlsDM.MyToolStrip.Items("Mic").Name) = False Then
                    tlsDM.MyToolStrip.ButtonsToHide.Add(tlsDM.MyToolStrip.Items("Mic").Name)
                End If
            End If
            tlsDM.MyToolStrip.Items("Mic").Image = Global.gloEMR.My.Resources.Mic_OFF
        Else
            'oCurDoc.Application.Selection.Find.ClearFormatting()
            'Try
            '    With oCurDoc.Application.Selection.Find
            '        .Text = strstring
            '        .Replacement.Text = " "
            '        .Forward = True
            '        .Wrap = Wd.WdFindWrap.wdFindContinue
            '        .Format = False
            '        .MatchCase = False
            '        .MatchWholeWord = False
            '        .MatchWildcards = False
            '        .MatchSoundsLike = False
            '        .MatchAllWordForms = False
            '    End With
            '    oCurDoc.Application.Selection.Find.Execute()
            'Catch ex As Exception
            If Not oCurDoc Is Nothing Then
                oCurDoc.ActiveWindow.SetFocus()
                Try
                    ''Bug #75280 : gloEMR- Exam - F5 functional key is not working
                    gloEMRWord.clsWordDocument.FindAndReplace(MyApp:=oCurDoc.Application, FindText:=strstring, ReplaceWith:=" ", Forward:=True, Wrap:=Wd.WdFindWrap.wdFindContinue, Replace:=Wd.WdReplace.wdReplaceNone, MatchWildCards:=False, MatchWholeWord:=False)
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    ex = Nothing
                End Try
                'End Try
            End If


            End If
    End Sub

    Public ReadOnly Property MyParent() As MainMenu Implements mdlgloVoice.gloVoice.MyParent
        Get
            Return MyMDIParent
        End Get
    End Property

    Private Event ActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.ActivateExamChild

    Private Event DeActivateExamChild(ByVal frmExamChild As mdlgloVoice.gloVoice) Implements mdlgloVoice.IExamChildEvents.DeActivateExamChild

    Public Property Handle1() As Integer Implements mdlgloVoice.IExamChildEvents.Handle
        Get
            Return Me.Handle.ToInt32()

        End Get
        Set(ByVal value As Integer)

        End Set
    End Property
End Class
