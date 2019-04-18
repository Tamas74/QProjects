Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Data.Common
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Linq
'Imports System.Data.Linq
Imports System.Collections.Generic

Public Class frmIm_transaction
    'Inherits System.Windows.Forms.Form
    Inherits frmBaseForm

    Implements IPatientContext
    Dim WithEvents objImmunizationReport As ClsImmunizationReport
    Public _EditID As Long
    Public _EditName As String
    Public _SaveFlag As Boolean
    Private combo As ComboBox
    Private COL_ITEMNAME As Integer = 0
    Private COL_ITEMCOUNTERID As Integer = 1
    Dim miniwindow As Integer = 0
    Dim miniwindowy As Integer = 0
    ' use for storing itemname with its value
    Dim hashtblItemName As New Hashtable
    Dim SelRowNo As Integer = -1
    Private blnClrAfterImmun As Boolean = False
    Private _isIMDM As Boolean = False
    'Private COL_TRNID As Integer = 2 '3 '2
    'Private COL_TRNDATE As Integer = 3 ' 4 '3
    'Private COL_PATIENTID As Integer = 4 '5 '4
    'Private COL_VISITID As Integer = 5 '6 '5
    'Private COL_ITEMID As Integer = 6 '7 '6

    'Private COL_DOSE As Integer = 7 '8 '7
    'Private COL_DATEGIVEN As Integer = 8 '9 '8
    'Private COL_ROUTE As Integer = 9 '10 '9
    'Private COL_LOTNUMBER As Integer = 10 '11 '10
    'Private COL_EXPDATE As Integer = 11 '12 '11
    'Private COL_MANUFACT As Integer = 12 '13 '12
    'Private COL_ISLOCK As Integer = 13 '14 '13
    'Private COL_USERID As Integer = 14 '15 '14
    'Private COL_NOTES As Integer = 15 '16 '15
    'Private COL_IDENTIFIER As Integer = 16 '17 '16
    'Private COL_DUEDATE As Integer = 17 '2 '17
    '' modification on 20070425 for CCHIT 2007
    'Private COL_SITE As Integer = 18
    'Private Col_USERNAME As Integer = 19

    'Private COL_COUNT As Integer = 20

    '' modification on 20070425 for CCHIT 2007

    Private COL_DATEGIVEN As Integer = 2
    Private COL_TIMEGIVEN As Integer = 3
    Private COL_DOSE As Integer = 4
    Private COL_DOSEUNIT As Integer = 5
    Private COL_ROUTE As Integer = 6
    Private COL_SITE As Integer = 7
    Private COL_LOTNUMBER As Integer = 8
    Private COL_EXPDATE As Integer = 9
    Private COL_MANUFACT As Integer = 10
    Private COL_DUEDATE As Integer = 11
    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110    
    Private COL_REACTION As Integer = 12
    Private COL_REACTIONDT As Integer = 13

    Private COL_REACTIONBTN = 14
    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
    Private Arry_CPTCODES As New ArrayList

    Private COL_NOTES As Integer = 15
    Private Col_USERNAME As Integer = 16
    Private COL_IDENTIFIER As Integer = 17
    Private COL_USERID As Integer = 18
    Private COL_ISLOCK As Integer = 19
    Private COL_ITEMID As Integer = 20
    Private COL_VISITID As Integer = 21
    Private COL_PATIENTID As Integer = 22
    Private COL_TRNDATE As Integer = 23
    Private COL_TRNID As Integer = 24



    Private COL_ISREMINDER As Integer = 25
    Private COL_VACCINEELIGIBILITYCODE As Integer = 26

    Private COL_REASONFORNONADMIN As Integer = 27
    Private COL_CPTCODES As Integer = 28
    Private Col_CPTCodesHidden As Integer = 29
    Private Col_VaccineCode As Int32 = 30
    Private Col_ItemName2 As Integer = 31

    ''Code Start-Added by kanchan on 20100904 for snomed implementation 
    Private COL_ConceptID As Integer = 32
    Private COL_DescriptionID As Integer = 33
    Private COL_SnomedID As Integer = 34
    ''Code End-Added by kanchan on 20100904 for snomed implementation 
    'Sanjog for Admin Status
    Private COL_AdminStatus As Integer = 35
    'Sanjog for Admin Status

    Private COL_COUNT As Integer = 36
    Private COL_COUNT2 As Integer = 36



    ''''''''''Added by Ujwala - Immunization  - as on 20101006
    Dim _PatientID As Long
    ''''''''''Added by Ujwala - Immunization  - as on 20101006

    'Private COL_NOTES As Integer = 11
    'Private Col_USERNAME As Integer = 12
    'Private COL_IDENTIFIER As Integer = 13
    'Private COL_USERID As Integer = 14
    'Private COL_ISLOCK As Integer = 15
    'Private COL_ITEMID As Integer = 16
    'Private COL_VISITID As Integer = 17
    'Private COL_PATIENTID As Integer = 18
    'Private COL_TRNDATE As Integer = 19
    'Private COL_TRNID As Integer = 20


    'Private COL_ISREMINDER As Integer = 21
    'Private COL_VACCINEELIGIBILITYCODE As Integer = 22

    'Private COL_REASONFORNONADMIN As Integer = 23
    'Private COL_CPTCODES As Integer = 24
    'Private Col_CPTCodesHidden As Integer = 25
    'Private Col_VaccineCode As Int32 = 26

    'Private COL_COUNT As Integer = 27



    '' '' solving sales force case- GLO2010-0005466
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationSettings.AppSettings
    Private PrintDialog1 As System.Windows.Forms.PrintDialog



    'Private COL_COUNT As Integer = 21
    '---
    'Friend WithEvents tblStrip_32 As System.Windows.Forms.ToolStrip
    Friend WithEvents tblStrip_32 As gloToolStrip.gloToolStrip
    Friend WithEvents tblbtn_PrintSummary_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_PrintDue_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Save_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton



    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
    Private WithEvents dgCustomGrid As CustomTask

    Dim _Temprow As Int32
    Dim _TempRx As String
    Dim strDia, strRx As String
    Private Col_Check As Integer = 2
    Private Col_HReaction As Integer = 0
    Private Col_Comment As Integer = 1
    Private Col_HRCount As Integer = 3
    '''''''''''''''''''''''''''''''''''''
    Dim cStyle1 As C1.Win.C1FlexGrid.CellStyle
    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

    Public arrDM As ArrayList

    Private _blnChangesMade As Boolean = False
    Friend WithEvents pnlRtxtBox As System.Windows.Forms.Panel
    Friend WithEvents rchtxtbox As System.Windows.Forms.RichTextBox
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents C1Flex_Transaction As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlImmn As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbcptcode As System.Windows.Forms.ComboBox
    Friend WithEvents cmbradmin As System.Windows.Forms.ComboBox
    Friend WithEvents cmbelicode As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents txtnotes As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cmbmanufac As System.Windows.Forms.ComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents dtexpdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtlotno As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtdose As System.Windows.Forms.TextBox
    Friend WithEvents txttime As System.Windows.Forms.TextBox
    Friend WithEvents cmbdtgiven As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbsite As System.Windows.Forms.ComboBox
    Friend WithEvents cmbroute As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbimmu As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents C1FlexTransaction As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btn_Save As System.Windows.Forms.Button
    Friend WithEvents chkrem As System.Windows.Forms.CheckBox
    Friend WithEvents cmbreac As System.Windows.Forms.ComboBox
    Friend WithEvents chkreaction As System.Windows.Forms.CheckedListBox
    Friend WithEvents pnlcustomTask As System.Windows.Forms.Panel
    Friend WithEvents btnBrowseReaction As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents btndelete As System.Windows.Forms.Button
    Friend WithEvents btnedit As System.Windows.Forms.Button
    Friend WithEvents cmbreacdt As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbduedt As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnimmst As System.Windows.Forms.Button
    Friend WithEvents tblCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblDescriptionID As System.Windows.Forms.Label
    Friend WithEvents lblSnoMedID As System.Windows.Forms.Label
    Friend WithEvents lblConceptID As System.Windows.Forms.Label
    Friend WithEvents lstReaction As System.Windows.Forms.ListBox
    ''  Record Level Locking
    Private _blnRecordLock As Boolean = False
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtDoseUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label

    Dim objfrmHistory As frmHistory
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents rbtn_Reported As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_Admin As System.Windows.Forms.RadioButton
    Friend WithEvents lbl_AdminStatus As System.Windows.Forms.Label
    Dim _arrImmu As ArrayList = Nothing
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Private _isChangesdone As Boolean = False

    'Dim oTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction
    'Dim oIM As New gloStream.Immunization.Transaction
    'Dim arrlst As New List(Of ClsImmunization)
    Public Delegate Sub DrawItemEventHandler(ByVal sender As Object, ByVal e As DrawItemEventArgs)


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        ''Added by Mayuri:20101117-to show tooltip on combo box
        cmbimmu.DrawMode = DrawMode.OwnerDrawFixed
        AddHandler cmbimmu.DrawItem, AddressOf ShowTooltipOnComboBox


        'Add any initialization after the InitializeComponent() call
        ''''''''''Added by Ujwala - Immunization  - as on 20101006
        _PatientID = PatientID          '' Replace _PatientID
        ''''''''''Added by Ujwala - Immunization  - as on 20101006
    End Sub


    Public Sub New(ByVal PatientID As Long, ByVal arrImmu As ArrayList)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        ''''''''''Added by Ujwala - Immunization  - as on 20101006
        _PatientID = PatientID          '' Replace _PatientID
        ''''''''''Added by Ujwala - Immunization  - as on 20101006
        _arrImmu = arrImmu
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
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
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cntxNothing As System.Windows.Forms.ContextMenu
    Friend WithEvents cntxMenuNothing As System.Windows.Forms.MenuItem
    Friend WithEvents lblPatientAddress As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTransaction As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblDOB As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblPatientPhone As System.Windows.Forms.Label
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIm_transaction))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.dtpTransaction = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblDOB = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblPatientPhone = New System.Windows.Forms.Label()
        Me.lblAge = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblPatientAddress = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.tblStrip_32 = New gloToolStrip.gloToolStrip()
        Me.tblbtn_PrintSummary_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_PrintDue_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblCCD = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Save_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlcustomTask = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.C1FlexTransaction = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.C1Flex_Transaction = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlImmn = New System.Windows.Forms.Panel()
        Me.rbtn_Admin = New System.Windows.Forms.RadioButton()
        Me.rbtn_Reported = New System.Windows.Forms.RadioButton()
        Me.lbl_AdminStatus = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtDoseUnit = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lstReaction = New System.Windows.Forms.ListBox()
        Me.lblDescriptionID = New System.Windows.Forms.Label()
        Me.lblSnoMedID = New System.Windows.Forms.Label()
        Me.lblConceptID = New System.Windows.Forms.Label()
        Me.btnimmst = New System.Windows.Forms.Button()
        Me.cmbreacdt = New System.Windows.Forms.DateTimePicker()
        Me.cmbduedt = New System.Windows.Forms.DateTimePicker()
        Me.btndelete = New System.Windows.Forms.Button()
        Me.btnedit = New System.Windows.Forms.Button()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.btnBrowseReaction = New System.Windows.Forms.Button()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtnotes = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.chkreaction = New System.Windows.Forms.CheckedListBox()
        Me.cmbcptcode = New System.Windows.Forms.ComboBox()
        Me.cmbreac = New System.Windows.Forms.ComboBox()
        Me.chkrem = New System.Windows.Forms.CheckBox()
        Me.cmbradmin = New System.Windows.Forms.ComboBox()
        Me.cmbelicode = New System.Windows.Forms.ComboBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmbmanufac = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.dtexpdate = New System.Windows.Forms.DateTimePicker()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtlotno = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtdose = New System.Windows.Forms.TextBox()
        Me.txttime = New System.Windows.Forms.TextBox()
        Me.cmbdtgiven = New System.Windows.Forms.DateTimePicker()
        Me.cmbsite = New System.Windows.Forms.ComboBox()
        Me.cmbroute = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbimmu = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cntxNothing = New System.Windows.Forms.ContextMenu()
        Me.cntxMenuNothing = New System.Windows.Forms.MenuItem()
        Me.pnlRtxtBox = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.rchtxtbox = New System.Windows.Forms.RichTextBox()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTop.SuspendLayout()
        Me.tblStrip_32.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1FlexTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Flex_Transaction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlImmn.SuspendLayout()
        Me.pnlRtxtBox.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.dtpTransaction)
        Me.pnlTop.Controls.Add(Me.Label10)
        Me.pnlTop.Controls.Add(Me.lblDOB)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.lblPatientPhone)
        Me.pnlTop.Controls.Add(Me.lblAge)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label9)
        Me.pnlTop.Controls.Add(Me.lblPatientAddress)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.lblName)
        Me.pnlTop.Controls.Add(Me.lblCode)
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTop.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTop.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTop.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTop.Location = New System.Drawing.Point(206, 115)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(1105, 129)
        Me.pnlTop.TabIndex = 1
        Me.pnlTop.TabStop = True
        '
        'dtpTransaction
        '
        Me.dtpTransaction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpTransaction.CalendarFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTransaction.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpTransaction.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpTransaction.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpTransaction.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpTransaction.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpTransaction.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTransaction.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTransaction.Location = New System.Drawing.Point(740, 97)
        Me.dtpTransaction.Name = "dtpTransaction"
        Me.dtpTransaction.Size = New System.Drawing.Size(120, 22)
        Me.dtpTransaction.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(664, 74)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 14)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Phone No :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDOB
        '
        Me.lblDOB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDOB.AutoSize = True
        Me.lblDOB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDOB.Location = New System.Drawing.Point(747, 46)
        Me.lblDOB.Name = "lblDOB"
        Me.lblDOB.Size = New System.Drawing.Size(0, 14)
        Me.lblDOB.TabIndex = 16
        Me.lblDOB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(694, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 14)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "DOB :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientPhone
        '
        Me.lblPatientPhone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPatientPhone.AutoSize = True
        Me.lblPatientPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientPhone.Location = New System.Drawing.Point(747, 74)
        Me.lblPatientPhone.Name = "lblPatientPhone"
        Me.lblPatientPhone.Size = New System.Drawing.Size(0, 14)
        Me.lblPatientPhone.TabIndex = 17
        Me.lblPatientPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAge
        '
        Me.lblAge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAge.AutoSize = True
        Me.lblAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAge.Location = New System.Drawing.Point(747, 16)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(0, 14)
        Me.lblAge.TabIndex = 12
        Me.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(625, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 14)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Transaction Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(696, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 14)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Age :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientAddress
        '
        Me.lblPatientAddress.AutoSize = True
        Me.lblPatientAddress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientAddress.Location = New System.Drawing.Point(124, 74)
        Me.lblPatientAddress.Name = "lblPatientAddress"
        Me.lblPatientAddress.Size = New System.Drawing.Size(0, 14)
        Me.lblPatientAddress.TabIndex = 10
        Me.lblPatientAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 14)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Patient Address :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Patient Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.Location = New System.Drawing.Point(124, 16)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(0, 14)
        Me.lblName.TabIndex = 6
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.Location = New System.Drawing.Point(124, 46)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(0, 14)
        Me.lblCode.TabIndex = 8
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(26, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Patient Code :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 125)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(1097, 1)
        Me.lbl_BottomBrd.TabIndex = 22
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 122)
        Me.lbl_LeftBrd.TabIndex = 21
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(1101, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 122)
        Me.lbl_RightBrd.TabIndex = 20
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(1099, 1)
        Me.lbl_TopBrd.TabIndex = 19
        Me.lbl_TopBrd.Text = "label1"
        '
        'tblStrip_32
        '
        Me.tblStrip_32.AddSeparatorsBetweenEachButton = False
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.ButtonsToHide = CType(resources.GetObject("tblStrip_32.ButtonsToHide"), System.Collections.ArrayList)
        Me.tblStrip_32.ConnectionString = Nothing
        Me.tblStrip_32.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tblStrip_32.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_PrintSummary_32, Me.tblbtn_PrintDue_32, Me.tblCCD, Me.tblbtn_Save_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.ModuleName = Nothing
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(1228, 53)
        Me.tblStrip_32.TabIndex = 4
        Me.tblStrip_32.TabStop = True
        Me.tblStrip_32.Text = "ToolStrip1"
        Me.tblStrip_32.UserID = CType(0, Long)
        '
        'tblbtn_PrintSummary_32
        '
        Me.tblbtn_PrintSummary_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_PrintSummary_32.Image = CType(resources.GetObject("tblbtn_PrintSummary_32.Image"), System.Drawing.Image)
        Me.tblbtn_PrintSummary_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PrintSummary_32.Name = "tblbtn_PrintSummary_32"
        Me.tblbtn_PrintSummary_32.Size = New System.Drawing.Size(102, 50)
        Me.tblbtn_PrintSummary_32.Tag = "Print Summary"
        Me.tblbtn_PrintSummary_32.Text = "Print &Summary"
        Me.tblbtn_PrintSummary_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_PrintSummary_32.ToolTipText = "Print Summary"
        '
        'tblbtn_PrintDue_32
        '
        Me.tblbtn_PrintDue_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_PrintDue_32.Image = CType(resources.GetObject("tblbtn_PrintDue_32.Image"), System.Drawing.Image)
        Me.tblbtn_PrintDue_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PrintDue_32.Name = "tblbtn_PrintDue_32"
        Me.tblbtn_PrintDue_32.Size = New System.Drawing.Size(69, 50)
        Me.tblbtn_PrintDue_32.Tag = "Print Due"
        Me.tblbtn_PrintDue_32.Text = "Print &Due"
        Me.tblbtn_PrintDue_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_PrintDue_32.ToolTipText = "Print Due"
        '
        'tblCCD
        '
        Me.tblCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblCCD.Image = CType(resources.GetObject("tblCCD.Image"), System.Drawing.Image)
        Me.tblCCD.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.tblCCD.Name = "tblCCD"
        Me.tblCCD.Size = New System.Drawing.Size(63, 50)
        Me.tblCCD.Tag = "Generate CCD"
        Me.tblCCD.Text = "&Gen CCD"
        Me.tblCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblCCD.ToolTipText = "Generate CCD"
        '
        'tblbtn_Save_32
        '
        Me.tblbtn_Save_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save_32.Image = CType(resources.GetObject("tblbtn_Save_32.Image"), System.Drawing.Image)
        Me.tblbtn_Save_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save_32.Name = "tblbtn_Save_32"
        Me.tblbtn_Save_32.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save_32.Tag = "Save and Close"
        Me.tblbtn_Save_32.Text = "&Save&&Cls"
        Me.tblbtn_Save_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save_32.ToolTipText = "Save and Close"
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Tag = "Close"
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Close_32.ToolTipText = "Close"
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.Label24)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 84)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(151, 79)
        Me.pnlBottom.TabIndex = 0
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(151, 1)
        Me.Label24.TabIndex = 47
        Me.Label24.Text = "label2"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlcustomTask)
        Me.pnlMain.Controls.Add(Me.Panel1)
        Me.pnlMain.Controls.Add(Me.C1Flex_Transaction)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 344)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(1228, 658)
        Me.pnlMain.TabIndex = 1
        '
        'pnlcustomTask
        '
        Me.pnlcustomTask.Location = New System.Drawing.Point(1039, 64)
        Me.pnlcustomTask.Name = "pnlcustomTask"
        Me.pnlcustomTask.Size = New System.Drawing.Size(45, 33)
        Me.pnlcustomTask.TabIndex = 43
        Me.pnlcustomTask.TabStop = True
        Me.pnlcustomTask.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.C1FlexTransaction)
        Me.Panel1.Controls.Add(Me.pnlBottom)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Location = New System.Drawing.Point(376, 414)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(152, 163)
        Me.Panel1.TabIndex = 24
        Me.Panel1.TabStop = True
        Me.Panel1.Visible = False
        '
        'C1FlexTransaction
        '
        Me.C1FlexTransaction.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexTransaction.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1FlexTransaction.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1FlexTransaction.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1FlexTransaction.Cursor = System.Windows.Forms.Cursors.Default
        Me.C1FlexTransaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexTransaction.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexTransaction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexTransaction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1FlexTransaction.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1FlexTransaction.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1FlexTransaction.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexTransaction.Name = "C1FlexTransaction"
        Me.C1FlexTransaction.Rows.DefaultSize = 19
        Me.C1FlexTransaction.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexTransaction.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1FlexTransaction.ShowCellLabels = True
        Me.C1FlexTransaction.Size = New System.Drawing.Size(151, 84)
        Me.C1FlexTransaction.StyleInfo = resources.GetString("C1FlexTransaction.StyleInfo")
        Me.C1FlexTransaction.TabIndex = 27
        Me.C1FlexTransaction.Tree.NodeImageCollapsed = CType(resources.GetObject("C1FlexTransaction.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1FlexTransaction.Tree.NodeImageExpanded = CType(resources.GetObject("C1FlexTransaction.Tree.NodeImageExpanded"), System.Drawing.Image)
        Me.C1FlexTransaction.Visible = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(151, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 163)
        Me.Label25.TabIndex = 8
        Me.Label25.Text = "label3"
        '
        'C1Flex_Transaction
        '
        Me.C1Flex_Transaction.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Flex_Transaction.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Flex_Transaction.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Flex_Transaction.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1Flex_Transaction.Cursor = System.Windows.Forms.Cursors.Default
        Me.C1Flex_Transaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Flex_Transaction.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1Flex_Transaction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Flex_Transaction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Flex_Transaction.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1Flex_Transaction.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Flex_Transaction.Location = New System.Drawing.Point(4, 4)
        Me.C1Flex_Transaction.Name = "C1Flex_Transaction"
        Me.C1Flex_Transaction.Rows.DefaultSize = 19
        Me.C1Flex_Transaction.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Flex_Transaction.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Flex_Transaction.ShowCellLabels = True
        Me.C1Flex_Transaction.Size = New System.Drawing.Size(1220, 650)
        Me.C1Flex_Transaction.StyleInfo = resources.GetString("C1Flex_Transaction.StyleInfo")
        Me.C1Flex_Transaction.TabIndex = 2
        Me.C1Flex_Transaction.Tree.NodeImageCollapsed = CType(resources.GetObject("C1Flex_Transaction.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1Flex_Transaction.Tree.NodeImageExpanded = CType(resources.GetObject("C1Flex_Transaction.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 654)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1220, 1)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 651)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(1224, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 651)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1222, 1)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "label1"
        '
        'pnlImmn
        '
        Me.pnlImmn.Controls.Add(Me.rbtn_Admin)
        Me.pnlImmn.Controls.Add(Me.rbtn_Reported)
        Me.pnlImmn.Controls.Add(Me.lbl_AdminStatus)
        Me.pnlImmn.Controls.Add(Me.Label41)
        Me.pnlImmn.Controls.Add(Me.txtDoseUnit)
        Me.pnlImmn.Controls.Add(Me.Label40)
        Me.pnlImmn.Controls.Add(Me.Label34)
        Me.pnlImmn.Controls.Add(Me.Label26)
        Me.pnlImmn.Controls.Add(Me.lstReaction)
        Me.pnlImmn.Controls.Add(Me.lblDescriptionID)
        Me.pnlImmn.Controls.Add(Me.lblSnoMedID)
        Me.pnlImmn.Controls.Add(Me.lblConceptID)
        Me.pnlImmn.Controls.Add(Me.btnimmst)
        Me.pnlImmn.Controls.Add(Me.cmbreacdt)
        Me.pnlImmn.Controls.Add(Me.cmbduedt)
        Me.pnlImmn.Controls.Add(Me.btndelete)
        Me.pnlImmn.Controls.Add(Me.btnedit)
        Me.pnlImmn.Controls.Add(Me.btn_Save)
        Me.pnlImmn.Controls.Add(Me.btnBrowseReaction)
        Me.pnlImmn.Controls.Add(Me.Label33)
        Me.pnlImmn.Controls.Add(Me.Label39)
        Me.pnlImmn.Controls.Add(Me.txtnotes)
        Me.pnlImmn.Controls.Add(Me.Label2)
        Me.pnlImmn.Controls.Add(Me.Label35)
        Me.pnlImmn.Controls.Add(Me.Label1)
        Me.pnlImmn.Controls.Add(Me.Label38)
        Me.pnlImmn.Controls.Add(Me.chkreaction)
        Me.pnlImmn.Controls.Add(Me.cmbcptcode)
        Me.pnlImmn.Controls.Add(Me.cmbreac)
        Me.pnlImmn.Controls.Add(Me.chkrem)
        Me.pnlImmn.Controls.Add(Me.cmbradmin)
        Me.pnlImmn.Controls.Add(Me.cmbelicode)
        Me.pnlImmn.Controls.Add(Me.Label37)
        Me.pnlImmn.Controls.Add(Me.Label36)
        Me.pnlImmn.Controls.Add(Me.Label32)
        Me.pnlImmn.Controls.Add(Me.Label31)
        Me.pnlImmn.Controls.Add(Me.Label30)
        Me.pnlImmn.Controls.Add(Me.cmbmanufac)
        Me.pnlImmn.Controls.Add(Me.Label29)
        Me.pnlImmn.Controls.Add(Me.dtexpdate)
        Me.pnlImmn.Controls.Add(Me.Label28)
        Me.pnlImmn.Controls.Add(Me.txtlotno)
        Me.pnlImmn.Controls.Add(Me.Label27)
        Me.pnlImmn.Controls.Add(Me.txtdose)
        Me.pnlImmn.Controls.Add(Me.txttime)
        Me.pnlImmn.Controls.Add(Me.cmbdtgiven)
        Me.pnlImmn.Controls.Add(Me.cmbsite)
        Me.pnlImmn.Controls.Add(Me.cmbroute)
        Me.pnlImmn.Controls.Add(Me.Label23)
        Me.pnlImmn.Controls.Add(Me.Label22)
        Me.pnlImmn.Controls.Add(Me.Label21)
        Me.pnlImmn.Controls.Add(Me.Label20)
        Me.pnlImmn.Controls.Add(Me.cmbimmu)
        Me.pnlImmn.Controls.Add(Me.Label18)
        Me.pnlImmn.Controls.Add(Me.Label19)
        Me.pnlImmn.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImmn.Location = New System.Drawing.Point(0, 54)
        Me.pnlImmn.Name = "pnlImmn"
        Me.pnlImmn.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlImmn.Size = New System.Drawing.Size(1228, 290)
        Me.pnlImmn.TabIndex = 0
        '
        'rbtn_Admin
        '
        Me.rbtn_Admin.AutoSize = True
        Me.rbtn_Admin.Checked = True
        Me.rbtn_Admin.Location = New System.Drawing.Point(137, 43)
        Me.rbtn_Admin.Name = "rbtn_Admin"
        Me.rbtn_Admin.Size = New System.Drawing.Size(96, 18)
        Me.rbtn_Admin.TabIndex = 0
        Me.rbtn_Admin.TabStop = True
        Me.rbtn_Admin.Text = "Administered"
        Me.rbtn_Admin.UseVisualStyleBackColor = True
        '
        'rbtn_Reported
        '
        Me.rbtn_Reported.AutoSize = True
        Me.rbtn_Reported.Location = New System.Drawing.Point(235, 43)
        Me.rbtn_Reported.Name = "rbtn_Reported"
        Me.rbtn_Reported.Size = New System.Drawing.Size(76, 18)
        Me.rbtn_Reported.TabIndex = 1
        Me.rbtn_Reported.Text = "Reported"
        Me.rbtn_Reported.UseVisualStyleBackColor = True
        '
        'lbl_AdminStatus
        '
        Me.lbl_AdminStatus.AutoSize = True
        Me.lbl_AdminStatus.Location = New System.Drawing.Point(46, 45)
        Me.lbl_AdminStatus.Name = "lbl_AdminStatus"
        Me.lbl_AdminStatus.Size = New System.Drawing.Size(88, 14)
        Me.lbl_AdminStatus.TabIndex = 561
        Me.lbl_AdminStatus.Text = "Admin Status :"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(215, 135)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(37, 14)
        Me.Label41.TabIndex = 560
        Me.Label41.Text = "Unit :"
        '
        'txtDoseUnit
        '
        Me.txtDoseUnit.Location = New System.Drawing.Point(255, 131)
        Me.txtDoseUnit.Name = "txtDoseUnit"
        Me.txtDoseUnit.Size = New System.Drawing.Size(63, 22)
        Me.txtDoseUnit.TabIndex = 5
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(855, 186)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(87, 14)
        Me.Label40.TabIndex = 210
        Me.Label40.Text = "DescriptionID :"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(871, 217)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(71, 14)
        Me.Label34.TabIndex = 209
        Me.Label34.Text = "SnoMedID :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(869, 155)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(73, 14)
        Me.Label26.TabIndex = 208
        Me.Label26.Text = "ConceptID :"
        '
        'lstReaction
        '
        Me.lstReaction.FormattingEnabled = True
        Me.lstReaction.ItemHeight = 14
        Me.lstReaction.Location = New System.Drawing.Point(514, 131)
        Me.lstReaction.Name = "lstReaction"
        Me.lstReaction.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lstReaction.Size = New System.Drawing.Size(214, 74)
        Me.lstReaction.TabIndex = 12
        '
        'lblDescriptionID
        '
        Me.lblDescriptionID.BackColor = System.Drawing.Color.White
        Me.lblDescriptionID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDescriptionID.Enabled = False
        Me.lblDescriptionID.Location = New System.Drawing.Point(945, 182)
        Me.lblDescriptionID.Name = "lblDescriptionID"
        Me.lblDescriptionID.Size = New System.Drawing.Size(214, 22)
        Me.lblDescriptionID.TabIndex = 556
        Me.lblDescriptionID.Text = "DescriptionID"
        Me.lblDescriptionID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSnoMedID
        '
        Me.lblSnoMedID.BackColor = System.Drawing.Color.White
        Me.lblSnoMedID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSnoMedID.Enabled = False
        Me.lblSnoMedID.Location = New System.Drawing.Point(945, 213)
        Me.lblSnoMedID.Name = "lblSnoMedID"
        Me.lblSnoMedID.Size = New System.Drawing.Size(214, 22)
        Me.lblSnoMedID.TabIndex = 558
        Me.lblSnoMedID.Text = "SnoMedID"
        Me.lblSnoMedID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblConceptID
        '
        Me.lblConceptID.BackColor = System.Drawing.Color.White
        Me.lblConceptID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblConceptID.Enabled = False
        Me.lblConceptID.Location = New System.Drawing.Point(945, 151)
        Me.lblConceptID.Name = "lblConceptID"
        Me.lblConceptID.Size = New System.Drawing.Size(214, 22)
        Me.lblConceptID.TabIndex = 555
        Me.lblConceptID.Text = "ConceptID"
        Me.lblConceptID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnimmst
        '
        Me.btnimmst.BackgroundImage = CType(resources.GetObject("btnimmst.BackgroundImage"), System.Drawing.Image)
        Me.btnimmst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnimmst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnimmst.Image = CType(resources.GetObject("btnimmst.Image"), System.Drawing.Image)
        Me.btnimmst.Location = New System.Drawing.Point(323, 13)
        Me.btnimmst.Name = "btnimmst"
        Me.btnimmst.Size = New System.Drawing.Size(21, 21)
        Me.btnimmst.TabIndex = 1
        Me.btnimmst.UseVisualStyleBackColor = True
        '
        'cmbreacdt
        '
        Me.cmbreacdt.Checked = False
        Me.cmbreacdt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cmbreacdt.Location = New System.Drawing.Point(514, 204)
        Me.cmbreacdt.Name = "cmbreacdt"
        Me.cmbreacdt.ShowCheckBox = True
        Me.cmbreacdt.Size = New System.Drawing.Size(214, 22)
        Me.cmbreacdt.TabIndex = 14
        '
        'cmbduedt
        '
        Me.cmbduedt.Checked = False
        Me.cmbduedt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cmbduedt.Location = New System.Drawing.Point(514, 102)
        Me.cmbduedt.Name = "cmbduedt"
        Me.cmbduedt.ShowCheckBox = True
        Me.cmbduedt.Size = New System.Drawing.Size(214, 22)
        Me.cmbduedt.TabIndex = 11
        '
        'btndelete
        '
        Me.btndelete.BackgroundImage = CType(resources.GetObject("btndelete.BackgroundImage"), System.Drawing.Image)
        Me.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btndelete.Location = New System.Drawing.Point(1099, 255)
        Me.btndelete.Name = "btndelete"
        Me.btndelete.Size = New System.Drawing.Size(60, 23)
        Me.btndelete.TabIndex = 22
        Me.btndelete.Text = "Delete"
        Me.btndelete.UseVisualStyleBackColor = True
        '
        'btnedit
        '
        Me.btnedit.BackgroundImage = CType(resources.GetObject("btnedit.BackgroundImage"), System.Drawing.Image)
        Me.btnedit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnedit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnedit.Location = New System.Drawing.Point(1026, 255)
        Me.btnedit.Name = "btnedit"
        Me.btnedit.Size = New System.Drawing.Size(60, 23)
        Me.btnedit.TabIndex = 21
        Me.btnedit.Text = "Edit"
        Me.btnedit.UseVisualStyleBackColor = True
        '
        'btn_Save
        '
        Me.btn_Save.BackgroundImage = CType(resources.GetObject("btn_Save.BackgroundImage"), System.Drawing.Image)
        Me.btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Save.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Save.Location = New System.Drawing.Point(951, 255)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(60, 23)
        Me.btn_Save.TabIndex = 20
        Me.btn_Save.Text = "Add "
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'btnBrowseReaction
        '
        Me.btnBrowseReaction.BackgroundImage = CType(resources.GetObject("btnBrowseReaction.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseReaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseReaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseReaction.Image = CType(resources.GetObject("btnBrowseReaction.Image"), System.Drawing.Image)
        Me.btnBrowseReaction.Location = New System.Drawing.Point(734, 134)
        Me.btnBrowseReaction.Name = "btnBrowseReaction"
        Me.btnBrowseReaction.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseReaction.TabIndex = 13
        Me.btnBrowseReaction.UseVisualStyleBackColor = True
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(895, 93)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(47, 14)
        Me.Label33.TabIndex = 27
        Me.Label33.Text = "Notes :"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(4, 3)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1220, 1)
        Me.Label39.TabIndex = 47
        Me.Label39.Text = "label2"
        '
        'txtnotes
        '
        Me.txtnotes.Location = New System.Drawing.Point(945, 85)
        Me.txtnotes.Multiline = True
        Me.txtnotes.Name = "txtnotes"
        Me.txtnotes.Size = New System.Drawing.Size(214, 60)
        Me.txtnotes.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 286)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1220, 1)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "label2"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(1224, 3)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 284)
        Me.Label35.TabIndex = 45
        Me.Label35.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 284)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "label4"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(868, 60)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(74, 14)
        Me.Label38.TabIndex = 34
        Me.Label38.Text = "CPT Codes :"
        '
        'chkreaction
        '
        Me.chkreaction.FormattingEnabled = True
        Me.chkreaction.Location = New System.Drawing.Point(646, 295)
        Me.chkreaction.Name = "chkreaction"
        Me.chkreaction.Size = New System.Drawing.Size(120, 4)
        Me.chkreaction.TabIndex = 42
        Me.chkreaction.Visible = False
        '
        'cmbcptcode
        '
        Me.cmbcptcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbcptcode.FormattingEnabled = True
        Me.cmbcptcode.Location = New System.Drawing.Point(945, 56)
        Me.cmbcptcode.Name = "cmbcptcode"
        Me.cmbcptcode.Size = New System.Drawing.Size(214, 22)
        Me.cmbcptcode.TabIndex = 18
        '
        'cmbreac
        '
        Me.cmbreac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbreac.FormattingEnabled = True
        Me.cmbreac.Location = New System.Drawing.Point(1166, 99)
        Me.cmbreac.Name = "cmbreac"
        Me.cmbreac.Size = New System.Drawing.Size(27, 22)
        Me.cmbreac.TabIndex = 15
        Me.cmbreac.Visible = False
        '
        'chkrem
        '
        Me.chkrem.AutoSize = True
        Me.chkrem.Location = New System.Drawing.Point(945, 6)
        Me.chkrem.Name = "chkrem"
        Me.chkrem.Size = New System.Drawing.Size(77, 18)
        Me.chkrem.TabIndex = 17
        Me.chkrem.Text = "Reminder"
        Me.chkrem.UseVisualStyleBackColor = True
        '
        'cmbradmin
        '
        Me.cmbradmin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbradmin.FormattingEnabled = True
        Me.cmbradmin.Location = New System.Drawing.Point(514, 234)
        Me.cmbradmin.Name = "cmbradmin"
        Me.cmbradmin.Size = New System.Drawing.Size(214, 22)
        Me.cmbradmin.TabIndex = 15
        '
        'cmbelicode
        '
        Me.cmbelicode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbelicode.FormattingEnabled = True
        Me.cmbelicode.Location = New System.Drawing.Point(945, 28)
        Me.cmbelicode.Name = "cmbelicode"
        Me.cmbelicode.Size = New System.Drawing.Size(214, 22)
        Me.cmbelicode.TabIndex = 16
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(375, 239)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(136, 14)
        Me.Label37.TabIndex = 33
        Me.Label37.Text = "Reason for non Admin :"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(851, 32)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(91, 14)
        Me.Label36.TabIndex = 32
        Me.Label36.Text = "Eligibility Code :"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(449, 134)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(62, 14)
        Me.Label32.TabIndex = 25
        Me.Label32.Text = "Reaction :"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(419, 208)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(92, 14)
        Me.Label31.TabIndex = 23
        Me.Label31.Text = "Reaction Date :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(444, 106)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(67, 14)
        Me.Label30.TabIndex = 21
        Me.Label30.Text = "Due Date :"
        '
        'cmbmanufac
        '
        Me.cmbmanufac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbmanufac.FormattingEnabled = True
        Me.cmbmanufac.Location = New System.Drawing.Point(514, 73)
        Me.cmbmanufac.Name = "cmbmanufac"
        Me.cmbmanufac.Size = New System.Drawing.Size(214, 22)
        Me.cmbmanufac.TabIndex = 10
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(424, 77)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(87, 14)
        Me.Label29.TabIndex = 19
        Me.Label29.Text = "Manufacturer :"
        '
        'dtexpdate
        '
        Me.dtexpdate.Checked = False
        Me.dtexpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtexpdate.Location = New System.Drawing.Point(514, 10)
        Me.dtexpdate.Name = "dtexpdate"
        Me.dtexpdate.ShowCheckBox = True
        Me.dtexpdate.Size = New System.Drawing.Size(181, 22)
        Me.dtexpdate.TabIndex = 9
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(442, 14)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(69, 14)
        Me.Label28.TabIndex = 17
        Me.Label28.Text = "Exp. Date :"
        '
        'txtlotno
        '
        Me.txtlotno.Location = New System.Drawing.Point(514, 41)
        Me.txtlotno.Name = "txtlotno"
        Me.txtlotno.Size = New System.Drawing.Size(181, 22)
        Me.txtlotno.TabIndex = 8
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(455, 45)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(56, 14)
        Me.Label27.TabIndex = 15
        Me.Label27.Text = "Lot No. :"
        '
        'txtdose
        '
        Me.txtdose.Location = New System.Drawing.Point(137, 131)
        Me.txtdose.Name = "txtdose"
        Me.txtdose.Size = New System.Drawing.Size(63, 22)
        Me.txtdose.TabIndex = 4
        '
        'txttime
        '
        Me.txttime.Location = New System.Drawing.Point(137, 100)
        Me.txttime.Name = "txttime"
        Me.txttime.Size = New System.Drawing.Size(181, 22)
        Me.txttime.TabIndex = 3
        '
        'cmbdtgiven
        '
        Me.cmbdtgiven.Checked = False
        Me.cmbdtgiven.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.cmbdtgiven.Location = New System.Drawing.Point(137, 69)
        Me.cmbdtgiven.Name = "cmbdtgiven"
        Me.cmbdtgiven.ShowCheckBox = True
        Me.cmbdtgiven.Size = New System.Drawing.Size(181, 22)
        Me.cmbdtgiven.TabIndex = 2
        '
        'cmbsite
        '
        Me.cmbsite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbsite.FormattingEnabled = True
        Me.cmbsite.Location = New System.Drawing.Point(137, 193)
        Me.cmbsite.Name = "cmbsite"
        Me.cmbsite.Size = New System.Drawing.Size(181, 22)
        Me.cmbsite.TabIndex = 7
        '
        'cmbroute
        '
        Me.cmbroute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbroute.FormattingEnabled = True
        Me.cmbroute.Location = New System.Drawing.Point(137, 162)
        Me.cmbroute.Name = "cmbroute"
        Me.cmbroute.Size = New System.Drawing.Size(181, 22)
        Me.cmbroute.TabIndex = 6
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(98, 197)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(36, 14)
        Me.Label23.TabIndex = 9
        Me.Label23.Text = "Site :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(86, 166)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(48, 14)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "Route :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(92, 135)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(42, 14)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "Dose :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(58, 104)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(76, 14)
        Me.Label20.TabIndex = 5
        Me.Label20.Text = "Time Given :"
        '
        'cmbimmu
        '
        Me.cmbimmu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbimmu.FormattingEnabled = True
        Me.cmbimmu.Location = New System.Drawing.Point(137, 12)
        Me.cmbimmu.Name = "cmbimmu"
        Me.cmbimmu.Size = New System.Drawing.Size(181, 22)
        Me.cmbimmu.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(59, 73)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(75, 14)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Date Given :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(47, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(87, 14)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "Immunization :"
        '
        'cntxNothing
        '
        Me.cntxNothing.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cntxMenuNothing})
        '
        'cntxMenuNothing
        '
        Me.cntxMenuNothing.Index = 0
        Me.cntxMenuNothing.Text = "Delete"
        '
        'pnlRtxtBox
        '
        Me.pnlRtxtBox.Controls.Add(Me.Label14)
        Me.pnlRtxtBox.Controls.Add(Me.Label15)
        Me.pnlRtxtBox.Controls.Add(Me.Label16)
        Me.pnlRtxtBox.Controls.Add(Me.Label17)
        Me.pnlRtxtBox.Controls.Add(Me.rchtxtbox)
        Me.pnlRtxtBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRtxtBox.Location = New System.Drawing.Point(0, 54)
        Me.pnlRtxtBox.Name = "pnlRtxtBox"
        Me.pnlRtxtBox.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlRtxtBox.Size = New System.Drawing.Size(1228, 948)
        Me.pnlRtxtBox.TabIndex = 2
        Me.pnlRtxtBox.TabStop = True
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(4, 944)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1220, 1)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 941)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(1224, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 941)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1222, 1)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "label1"
        '
        'rchtxtbox
        '
        Me.rchtxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rchtxtbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rchtxtbox.ForeColor = System.Drawing.Color.Black
        Me.rchtxtbox.Location = New System.Drawing.Point(3, 3)
        Me.rchtxtbox.Name = "rchtxtbox"
        Me.rchtxtbox.Size = New System.Drawing.Size(1222, 942)
        Me.rchtxtbox.TabIndex = 0
        Me.rchtxtbox.Text = ""
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tblStrip_32)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1228, 54)
        Me.Panel2.TabIndex = 3
        '
        'frmIm_transaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1228, 1002)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlImmn)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlRtxtBox)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmIm_transaction"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient's  Immunization"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.C1FlexTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Flex_Transaction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlImmn.ResumeLayout(False)
        Me.pnlImmn.PerformLayout()
        Me.pnlRtxtBox.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub InitializeToolStrip()
        tblStrip_32.ConnectionString = GetConnectionString()
        tblStrip_32.ModuleName = Me.Name
        tblStrip_32.UserID = gnLoginID
    End Sub

#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip

    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip
        Me.Controls.Add(gloUC_PatientStrip1)
        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            .ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.Immunization)
            .BringToFront()
            pnlImmn.BringToFront()
            Panel2.SendToBack()
        End With

        '''''
        pnlMain.BringToFront()
        C1FlexTransaction.BringToFront()
        '' Hide Previous Patient Details
        pnlTop.Visible = False
        ' ''
    End Sub

#End Region

    Private Sub frmIm_transaction_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmIm_transaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            InitializeToolStrip()
            cmbreacdt.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
            dtexpdate.CustomFormat = "MM/dd/yyyy"
            cmbduedt.CustomFormat = "MM/dd/yyyy"
            cmbdtgiven.CustomFormat = "MM/dd/yyyy"
            ' fill c1grid with data.
            dtpTransaction.TabIndex = 5
            ''cmbdtgiven.Text = DateTime.Now
            Clear()
            FillEmptyStringCombo()
            FillItems()
            Call Get_PatientDetails()
            ' Call Fill_Update()
            'fillclFlexTran1()
            Call Fill_UpdateCopy()

            Dim strItemName As String = ""
            Dim strVacName As String = ""
            Dim IsIMPresent As Boolean = False
            Dim strDescriptionId As String = ""
            Dim strConceptId As String = ""
            Dim strSnomedId As String = ""
            Dim nIMId As Int64 = 0
            If Not IsNothing(arrDM) Then
                If arrDM.Count > 0 Then
                    For i As Integer = 0 To arrDM.Count - 1
                        strItemName = CType(arrDM.Item(i), myList).Route
                        nIMId = CType(arrDM.Item(i), myList).ID
                        strVacName = CType(arrDM.Item(i), myList).DMTemplateName
                        strDescriptionId = CType(arrDM.Item(i), myList).Duration
                        strConceptId = CType(arrDM.Item(i), myList).DrugForm
                        strSnomedId = CType(arrDM.Item(i), myList).Frequency
                        IsIMPresent = False
                        For j As Integer = 0 To C1Flex_Transaction.Rows.Count - 1
                            If C1Flex_Transaction.GetData(j, COL_ITEMNAME) = strVacName Then
                                IsIMPresent = True
                                Exit For
                            End If
                        Next
                        If IsIMPresent = False Then
                            FillIMForDM(strItemName, nIMId, strVacName, strDescriptionId, strConceptId, strSnomedId)
                        End If
                    Next
                End If
            End If

            If Not IsNothing(_arrImmu) Then
                If _arrImmu.Count > 0 Then
                    For i As Integer = 0 To _arrImmu.Count - 1
                        ''Sanjog
                        If CType(_arrImmu.Item(i), myList).Route = "" Then
                            Dim oDB1 As New gloStream.gloDataBase.gloDataBase
                            Dim oDS As DataSet
                            Dim strSQL1 As String
                            strSQL1 = "select IM_ITEM_NAME from Im_mst where im_item_id=" & CType(_arrImmu.Item(i), myList).ID & ""
                            oDB1.Connect(GetConnectionString)
                            oDS = oDB1.ReadQueryRecordAsDataSet(strSQL1)
                            If oDS.Tables(0).Rows.Count > 0 Then
                                strItemName = oDS.Tables(0).Rows(0)(0).ToString()
                            Else
                                strItemName = ""
                            End If
                            If Not IsNothing(oDS) Then    'obj Disposed by mitesh
                                oDS.Dispose()
                                oDS = Nothing
                            End If
                            If Not IsNothing(oDB1) Then    'obj Disposed by mitesh
                                oDB1.Dispose()
                                oDB1 = Nothing
                            End If
                        Else
                            strItemName = CType(_arrImmu.Item(i), myList).Route
                        End If


                        ''Sanjog
                        'strItemName = CType(_arrImmu.Item(i), myList).Route
                        nIMId = CType(_arrImmu.Item(i), myList).ID
                        strVacName = CType(_arrImmu.Item(i), myList).DMTemplateName
                        strDescriptionId = CType(_arrImmu.Item(i), myList).Duration
                        strConceptId = CType(_arrImmu.Item(i), myList).DrugForm
                        strSnomedId = CType(_arrImmu.Item(i), myList).Frequency
                        IsIMPresent = False
                        For j As Integer = 0 To C1Flex_Transaction.Rows.Count - 1
                            If C1Flex_Transaction.GetData(j, COL_ITEMNAME) = strVacName Then
                                IsIMPresent = True
                                Exit For
                            End If
                        Next
                        If IsIMPresent = False Then
                            Try

                                FillIMForDM(strItemName, nIMId, strVacName, strDescriptionId, strConceptId, strSnomedId)
                            Catch ex As Exception

                            End Try

                        End If
                    Next
                End If
            End If

            SetComboIndex()
            lblName.Text = strPatientFirstName & " " & strPatientLastName
            lblCode.Text = strPatientCode
            lblDOB.Text = strPatientDOB
            lblAge.Text = strPatientAge

            '' get the parient's details. modified on 20070328 by Bipin
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlClient.SqlDataReader
            Dim strSQL As String
            'Dim strAddress As String

            strSQL = "SELECT ISNULL(sAddressLine1,'') AS sAddressLine1, ISNULL(sAddressLine2,'') AS sAddressLine2, ISNULL(sCity,'') AS sCity , ISNULL(sState,'') AS sState , ISNULL(sZIP,'') AS sZIP , ISNULL(sCounty,'') AS sCounty , ISNULL(sPhone,'') AS sPhone FROM Patient WHERE nPatientID = " & _PatientID & " "
            oDB.Connect(GetConnectionString)
            oDataReader = oDB.ReadQueryRecords(strSQL)
            Dim _sAddress As String = ""
            Dim _First As Boolean = False

            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    If oDataReader("sAddressLine1") <> "" Then
                        _sAddress = oDataReader("sAddressLine1")
                    End If
                    If oDataReader("sAddressLine2") <> "" Then
                        If _sAddress.Trim <> "" Then
                            _sAddress = _sAddress & ", " & oDataReader("sAddressLine2")
                        Else
                            _sAddress = oDataReader("sAddressLine2")
                        End If
                    End If
                    If oDataReader("sCity") <> "" Then
                        If _sAddress.Trim <> "" Then
                            _sAddress = _sAddress & ", " & oDataReader("sCity")
                        Else
                            _sAddress = oDataReader("sCity")
                        End If
                    End If
                    If oDataReader("sState") <> "" Then
                        If _sAddress.Trim <> "" Then
                            _sAddress = _sAddress & ", " & oDataReader("sState")
                        Else
                            _sAddress = oDataReader("sState")
                        End If
                    End If
                    If oDataReader("sZIP") <> "" Then
                        If _sAddress.Trim <> "" Then
                            _sAddress = _sAddress & ", " & oDataReader("sZIP")
                        Else
                            _sAddress = oDataReader("sZIP")
                        End If
                    End If

                    'If oDataReader("sCounty") <> "" Then
                    '    If _sAddress.Trim <> "" Then
                    '        _sAddress = _sAddress & ", " & oDataReader("sCounty")
                    '    Else
                    '        _sAddress = oDataReader("sCounty")
                    '    End If
                    'End If

                    lblPatientPhone.Text = oDataReader.Item("sPhone")
                End While
            End If
            oDB.Disconnect()
            If Not IsNothing(oDataReader) Then   'obj Disposed by mitesh
                oDataReader.Dispose()
                oDataReader = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            '' Filling Reaction 
            FillReaction()
            lblPatientAddress.Text = _sAddress
            ''
            ' '' Show Patient Details
            Set_PatientDetailStrip()
            ' ''
            cmbimmu.Focus()

            _isLoaded = True

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "Immunization Transaction Opened", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, "Immunization Transaction Opened", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _blnRecordLock = True Then
                '' Record is Opened by other user 
                tblbtn_Save_32.Enabled = False
            Else
                tblbtn_Save_32.Enabled = True
            End If
        End Try
    End Sub
    Public Sub SetReaction()
        ''''''''''Added by Ujwala - for Snomed Implementation From Immunization in History - as on 20101008
        lstReaction.Items.Clear()
        Dim _sReaction As String = objfrmHistory.Reaction
        If _sReaction <> "" Then
            Dim Im_Reaction As String() = _sReaction.Split(vbNewLine)
            If Not IsNothing(Im_Reaction) Then
                If Im_Reaction.Count > 0 Then
                    For Each i As String In Im_Reaction
                        If i.Trim() <> "" Then
                            lstReaction.Items.Add(i.Trim())
                        End If
                    Next
                    If lstReaction.Items.Count > 0 Then
                        cmbreacdt.Checked = True
                    Else
                        cmbreacdt.Checked = False
                    End If

                End If
            End If
        End If
        ''''''''''Added by Ujwala - for Snomed Implementation From Immunization in History - as on 20101008
    End Sub
    Private Sub FillReaction()


        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim oDataReader As SqlClient.SqlDataReader
        Dim strSQL As String
        'Dim strAddress As String

        strSQL = "SELECT ISNULL(sDescription,'') AS Reaction  FROM  Category_MST WHERE sCategoryType ='Reaction'"
        oDB.Connect(GetConnectionString)
        oDataReader = oDB.ReadQueryRecords(strSQL)

        If oDataReader.HasRows = True Then
            While oDataReader.Read
                If oDataReader("Reaction") <> "" Then
                    cmbreac.Items.Add(oDataReader("Reaction"))
                End If



            End While
        End If
        oDB.Disconnect()
        If Not IsNothing(oDataReader) Then  'obj Disposed by mitesh
            oDataReader.Dispose()
            oDataReader = Nothing
        End If
        If Not IsNothing(oDB) Then
            oDB.Dispose()
            oDB = Nothing
        End If

    End Sub

    Private Sub Loadform()
        Try


            ' fill c1grid with data.
            dtpTransaction.TabIndex = 5
            ' cmbdtgiven.Text = DateTime.Now
            FillEmptyStringCombo()
            FillItems()

            ' Call Fill_Update()
            'fillclFlexTran1()
            Call Fill_UpdateCopy()
            SetComboIndex()
            
          
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If _blnRecordLock = True Then
                '' Record is Opened by other user 
                tblbtn_Save_32.Enabled = False
            Else
                tblbtn_Save_32.Enabled = True
            End If
        End Try
    End Sub



    Private Sub FillEmptyStringCombo()
        cmbimmu.Items.Add("")
        cmbroute.Items.Add("")
        cmbsite.Items.Add("")
        cmbreac.Items.Add("")
        cmbelicode.Items.Add("")
        cmbradmin.Items.Add("")
        cmbcptcode.Items.Add("")
        cmbmanufac.Items.Add("")
    End Sub

    Private Sub SetComboIndex()
        cmbimmu.SelectedIndex = 0
        cmbroute.SelectedIndex = 0
        cmbsite.SelectedIndex = 0
        '' cmbreac.SelectedIndex = 0
        cmbelicode.SelectedIndex = 0
        cmbradmin.SelectedIndex = 0
        cmbcptcode.SelectedIndex = 0
        cmbmanufac.SelectedIndex = 0
        ' cmbduedt.ShowCheckBox = False
        ' cmbreacdt.ShowCheckBox = False
    End Sub

    Private Sub FillItems()

        Dim oImmunization As New gloStream.Immunization.ItemSetup
        Dim oItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems

        Dim oTransaction As New gloStream.Immunization.Transaction
        Dim oIMTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction

        ' counter for fill root node and subnode of the c1.
        Dim _N As Integer
        Dim _Nc As Integer

        Dim _Manufacturers As String = " "
        Dim oMans As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIM As New gloStream.Immunization.Common

        oMans = oIM.Manufacturers()
        ' get item details
        If Not oMans Is Nothing Then
            For i As Integer = 1 To oMans.Count

                _Manufacturers = _Manufacturers & "|" & oMans(i).Description
                If cmbmanufac.Items.IndexOf(oMans(i).Description.Trim()) = -1 Then
                    cmbmanufac.Items.Add(oMans(i).Description.Trim())
                End If
            Next
        End If
        oMans = Nothing
        oIM = Nothing

        '''''''''''' Modified on 20070425 for CCHIT2007
        Dim _Sites As String = " "
        Dim oSites As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIMSite As New gloStream.Immunization.Common

        oSites = oIMSite.Sites
        ' get item details
        If Not oSites Is Nothing Then
            For i As Integer = 1 To oSites.Count
                'If i = 1 Then
                '    _Manufacturers = oMans(i).Description
                'Else
                _Sites = _Sites & "|" & oSites(i).Description
                'End If
                If cmbsite.Items.IndexOf(oSites(i).Description.Trim()) = -1 Then
                    cmbsite.Items.Add(oSites(i).Description.Trim())
                End If
            Next
        End If
        oSites = Nothing
        oIMSite = Nothing
        ''''''''''''
        Dim _Routes As String = " "
        Dim oRoutes As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIMRoutes As New gloStream.Immunization.Common

        oRoutes = oIMRoutes.Route

        ' get item details
        If Not oRoutes Is Nothing Then
            For i As Integer = 1 To oRoutes.Count

                _Routes = _Routes & "|" & oRoutes(i).Description
                If cmbroute.Items.IndexOf(oRoutes(i).Description.Trim()) = -1 Then
                    cmbroute.Items.Add(oRoutes(i).Description.Trim())
                End If

            Next
        End If
        oRoutes = Nothing
        oIMRoutes = Nothing

        ''''''''''''


        ''sarika
        Dim _EligibilityCodes As String = " "
        Dim oEligibilityCodes As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIMEligibilityCodes As New gloStream.Immunization.Common

        oEligibilityCodes = oIMEligibilityCodes.EligibilityCodes

        ' get item details
        If Not oEligibilityCodes Is Nothing Then
            For i As Integer = 1 To oEligibilityCodes.Count

                _EligibilityCodes = _EligibilityCodes & "|" & oEligibilityCodes(i).Description

                If cmbelicode.Items.IndexOf(oEligibilityCodes(i).Description.Trim()) = -1 Then
                    cmbelicode.Items.Add(oEligibilityCodes(i).Description.Trim())
                End If

            Next
        End If
        oEligibilityCodes = Nothing
        oIMEligibilityCodes = Nothing

        '''''''''''''


        Dim _CPTCodes As String = " "

       
        Dim _strAdmin As String = "|Person Condition|Family Member Condition|Waiver|Documented immunity/Titer"
        Dim _strspladmin As String() = _strAdmin.Split("|")
        For Len As Integer = 0 To _strspladmin.Length - 1
            If _strspladmin(Len).Trim() <> "" Then
                cmbradmin.Items.Add(_strspladmin(Len))
            End If

        Next



        Dim dt As DataTable = Nothing
        oItemDetails = oImmunization.ItemDetails()
        dt = oImmunization.PatientImmunization(_PatientID)
        Dim j As Int64
        Dim ImmPresentflag As Boolean = False
        ' loop for root node fill
        Dim _strImmu As String = ""
        If Not oItemDetails Is Nothing Then
            If oItemDetails.Count > 0 Then

                Arry_CPTCODES.Clear() ' Arry_CPTCODES is used for storing CPT code values with Immunization name  
                ' fill name
                For _N = 1 To oItemDetails.Count
                    Dim oItemNode As C1.Win.C1FlexGrid.Node
                    ' .Rows.Add()
                    'With .Rows(.Rows.Count - 1)
                    '    .ImageAndText = True
                    '    .Height = 24
                    '    .IsNode = True

                    '    .Node.Level = 0
                    '    .Node.Data = oItemDetails(_N).Name
                    '    .Node.Key = oItemDetails(_N).ID
                    '    oItemNode = .Node
                    '    .AllowEditing = False
                    'End With
                    'set the value of the root
                    _strImmu = oItemDetails(_N).Name
                    '.SetData(.Rows.Count - 1, COL_ITEMNAME, oItemDetails(_N).Name)
                    '.SetData(.Rows.Count - 1, COL_ITEMCOUNTERID, 0)
                    '.SetData(.Rows.Count - 1, COL_TRNID, 0)
                    '.SetData(.Rows.Count - 1, COL_ITEMID, oItemDetails(_N).ID)
                    '.SetData(.Rows.Count - 1, COL_IDENTIFIER, "I" & oItemDetails(_N).ID)

                    '.SetData(.Rows.Count - 1, Col_VaccineCode, oItemDetails(_N).VaccineCode)

                    '-------code added after we removed the cptCodes from the IM_GetImmunizationDetails stored procedure
                    'here using the oItemDetails(_N).ID we will query the IM_MST table for retrieving the CPT_Codes against that item and fill them in the CPT Codes combo box.
                    Try
                        If oItemDetails(_N).VaccineCode.Trim() = "" Then
                            oItemDetails(_N).VaccineCode = "-1"

                        End If
                        Dim strCptCodes As String = oImmunization.getCPTCodes(oItemDetails(_N).ID, Convert.ToInt16(oItemDetails(_N).VaccineCode))
                        _CPTCodes = " "

                        If strCptCodes <> "" Then
                            Dim strCPTarry As String() = Split(strCptCodes, ",")
                            If strCPTarry.Length > 1 Then
                                For i As Int16 = 0 To strCPTarry.Length - 1

                                    ' _CPTCodes = _CPTCodes & "|" & strCPTarry(i)

                                    Arry_CPTCODES.Add(_strImmu & "*#\" & strCPTarry(i))
                                Next
                            Else
                                ' _CPTCodes = _CPTCodes & "|" & strCPTarry(0)

                                Arry_CPTCODES.Add(_strImmu & "*#\" & strCPTarry(0))

                            End If

                        Else
                            '  _CPTCodes = _CPTCodes
                            Arry_CPTCODES.Add(_strImmu & "*#\" & _CPTCodes)

                        End If

                    Catch ex As Exception

                    End Try


                    ' loop for the sub node
                    ''Sanjog -Added on 20101117 for immunization
                    For _Nc = 1 To oItemDetails.Item(_N).HowMany
                        ImmPresentflag = False
                        If Not IsNothing(dt) Then
                            For j = 0 To dt.Rows.Count - 1
                                If (_strImmu & _Nc.ToString() = Convert.ToString(dt.Rows(j)(0)).Trim() & Convert.ToString(dt.Rows(j)(1)).Trim()) Then
                                    ImmPresentflag = True
                                    Exit For
                                End If
                            Next
                        End If
                        If ImmPresentflag = False Then
                            cmbimmu.Items.Add(_strImmu & _Nc.ToString())
                            'hashtblItemName.Add(_strImmu & _Nc.ToString(), _strImmu)
                        End If
                        '' To Add all entries in hashtable
                        hashtblItemName.Add(_strImmu & _Nc.ToString(), _strImmu)
                    Next
                    ''Sanjog -Added on 20101117 for immunization
                    ' .Rows(.Rows.Count - 1).AllowEditing = True
                Next
            End If
        End If


        'End With
        fillclFlexTran1()


        If Not IsNothing(oIM) Then    'obj Disposed by mitesh
            'oIM.Dispose()
            oIM = Nothing
        End If

        If Not IsNothing(oMans) Then
            ' oMans.Dispose()
            oMans = Nothing
        End If
        If Not IsNothing(oIMTransaction) Then
            oIMTransaction = Nothing
        End If
        If Not IsNothing(oTransaction) Then
            oTransaction = Nothing
        End If
        If Not IsNothing(oItemDetails) Then
            oItemDetails = Nothing
        End If
        If Not IsNothing(oImmunization) Then
            oImmunization = Nothing
        End If

    End Sub


    Private Sub FillCMbImmu()
        Dim oImmunization As New gloStream.Immunization.ItemSetup
        Dim oItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems

        Dim oTransaction As New gloStream.Immunization.Transaction
        Dim oIMTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction

        ' counter for fill root node and subnode of the c1.
        Dim _N As Integer
        Dim _Nc As Integer
        cmbimmu.Items.Clear()
        cmbimmu.Items.Add("")
        Dim _Manufacturers As String = " "
        ' Dim oMans As New gloStream.Immunization.Supporting.ItemDetails
        ' Dim oIM As New gloStream.Immunization.Common
        hashtblItemName.Clear()
        ' oMans = oIM.Manufacturers()
        ' get item details

        'If Not oMans Is Nothing Then
        '    For i As Integer = 1 To oMans.Count

        '        _Manufacturers = _Manufacturers & "|" & oMans(i).Description
        '        cmbmanufac.Items.Add(oMans(i).Description)

        '    Next
        'End If
        'oMans = Nothing
        'oIM = Nothing

        '''''''''''' Modified on 20070425 for CCHIT2007
        'Dim _Sites As String = " "
        'Dim oSites As New gloStream.Immunization.Supporting.ItemDetails
        'Dim oIMSite As New gloStream.Immunization.Common

        'oSites = oIMSite.Sites
        '' get item details
        'If Not oSites Is Nothing Then
        '    For i As Integer = 1 To oSites.Count
        '        'If i = 1 Then
        '        '    _Manufacturers = oMans(i).Description
        '        'Else
        '        _Sites = _Sites & "|" & oSites(i).Description
        '        'End If
        '        cmbsite.Items.Add(oSites(i).Description)
        '    Next
        'End If
        'oSites = Nothing
        'oIMSite = Nothing
        ''''''''''''
        'Dim _Routes As String = " "
        'Dim oRoutes As New gloStream.Immunization.Supporting.ItemDetails
        'Dim oIMRoutes As New gloStream.Immunization.Common

        'oRoutes = oIMRoutes.Route

        '' get item details
        'If Not oRoutes Is Nothing Then
        '    For i As Integer = 1 To oRoutes.Count

        '        _Routes = _Routes & "|" & oRoutes(i).Description
        '        cmbroute.Items.Add(oRoutes(i).Description)

        '    Next
        'End If
        'oRoutes = Nothing
        'oIMRoutes = Nothing

        ''''''''''''


        ''sarika
        'Dim _EligibilityCodes As String = " "
        'Dim oEligibilityCodes As New gloStream.Immunization.Supporting.ItemDetails
        'Dim oIMEligibilityCodes As New gloStream.Immunization.Common

        'oEligibilityCodes = oIMEligibilityCodes.EligibilityCodes

        '' get item details
        'If Not oEligibilityCodes Is Nothing Then
        '    For i As Integer = 1 To oEligibilityCodes.Count

        '        _EligibilityCodes = _EligibilityCodes & "|" & oEligibilityCodes(i).Description

        '        cmbelicode.Items.Add(oEligibilityCodes(i).Description)
        '    Next
        'End If
        'oEligibilityCodes = Nothing
        'oIMEligibilityCodes = Nothing

        '''''''''''''


        Dim _CPTCodes As String = " "







        oItemDetails = oImmunization.ItemDetails()


        Dim _strImmu As String = ""
        If Not oItemDetails Is Nothing Then
            If oItemDetails.Count > 0 Then

                Arry_CPTCODES.Clear() ' Arry_CPTCODES is used for storing CPT code values with Immunization name  
                ' fill name
                For _N = 1 To oItemDetails.Count
                    Dim oItemNode As C1.Win.C1FlexGrid.Node

                    _strImmu = oItemDetails(_N).Name
                    '-------code added after we removed the cptCodes from the IM_GetImmunizationDetails stored procedure
                    'here using the oItemDetails(_N).ID we will query the IM_MST table for retrieving the CPT_Codes against that item and fill them in the CPT Codes combo box.
                    Try
                        If oItemDetails(_N).VaccineCode.Trim() = "" Then
                            oItemDetails(_N).VaccineCode = "-1"

                        End If
                        Dim strCptCodes As String = oImmunization.getCPTCodes(oItemDetails(_N).ID, Convert.ToInt16(oItemDetails(_N).VaccineCode))
                        _CPTCodes = " "

                        If strCptCodes <> "" Then
                            Dim strCPTarry As String() = Split(strCptCodes, ",")
                            If strCPTarry.Length > 1 Then
                                For i As Int16 = 0 To strCPTarry.Length - 1

                                    ' _CPTCodes = _CPTCodes & "|" & strCPTarry(i)

                                    Arry_CPTCODES.Add(_strImmu & "*#\" & strCPTarry(i))
                                Next
                            Else
                                ' _CPTCodes = _CPTCodes & "|" & strCPTarry(0)

                                Arry_CPTCODES.Add(_strImmu & "*#\" & strCPTarry(0))

                            End If

                        Else
                            '  _CPTCodes = _CPTCodes
                            Arry_CPTCODES.Add(_strImmu & "*#\" & _CPTCodes)

                        End If

                    Catch ex As Exception

                    End Try





                    ' loop for the sub node

                    For _Nc = 1 To oItemDetails.Item(_N).HowMany
                        cmbimmu.Items.Add(_strImmu & _Nc.ToString())
                        hashtblItemName.Add(_strImmu & _Nc.ToString(), _strImmu)
                    Next

                    ' .Rows(.Rows.Count - 1).AllowEditing = True
                Next
            End If
        End If






    End Sub


    Private Sub fillclFlexTran1()


        Dim oImmunization As New gloStream.Immunization.ItemSetup
        Dim oItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems

        Dim oTransaction As New gloStream.Immunization.Transaction
        Dim oIMTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction

        ' counter for fill root node and subnode of the c1.
        Dim _N As Integer
        Dim _Nc As Integer

        Dim _Manufacturers As String = " "
        Dim oMans As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIM As New gloStream.Immunization.Common

        oMans = oIM.Manufacturers()
        ' get item details
        If Not oMans Is Nothing Then
            For i As Integer = 1 To oMans.Count

                _Manufacturers = _Manufacturers & "|" & oMans(i).Description
                If cmbmanufac.Items.IndexOf(oMans(i).Description.Trim()) = -1 Then

                    cmbmanufac.Items.Add(oMans(i).Description)
                End If
            Next
        End If
        oMans = Nothing
        oIM = Nothing

        '''''''''''' Modified on 20070425 for CCHIT2007
        Dim _Sites As String = " "
        Dim oSites As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIMSite As New gloStream.Immunization.Common

        oSites = oIMSite.Sites
        ' get item details
        If Not oSites Is Nothing Then
            For i As Integer = 1 To oSites.Count

                _Sites = _Sites & "|" & oSites(i).Description
                If cmbsite.Items.IndexOf(oSites(i).Description.Trim()) = -1 Then

                    cmbsite.Items.Add(oSites(i).Description.Trim())
                End If
            Next
        End If
        oSites = Nothing
        oIMSite = Nothing
        ''''''''''''
        Dim _Routes As String = " "
        Dim oRoutes As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIMRoutes As New gloStream.Immunization.Common

        oRoutes = oIMRoutes.Route

        ' get item details
        If Not oRoutes Is Nothing Then
            For i As Integer = 1 To oRoutes.Count

                _Routes = _Routes & "|" & oRoutes(i).Description
                If cmbroute.Items.IndexOf(oRoutes(i).Description.Trim()) = -1 Then
                    cmbroute.Items.Add(oRoutes(i).Description.Trim())
                End If

            Next
        End If
        oRoutes = Nothing
        oIMRoutes = Nothing


        ''''''''''''


        ''sarika
        Dim _EligibilityCodes As String = " "
        Dim oEligibilityCodes As New gloStream.Immunization.Supporting.ItemDetails
        Dim oIMEligibilityCodes As New gloStream.Immunization.Common

        oEligibilityCodes = oIMEligibilityCodes.EligibilityCodes

        ' get item details
        If Not oEligibilityCodes Is Nothing Then
            For i As Integer = 1 To oEligibilityCodes.Count

                _EligibilityCodes = _EligibilityCodes & "|" & oEligibilityCodes(i).Description

                If cmbelicode.Items.IndexOf(oEligibilityCodes(i).Description.Trim()) = -1 Then

                    cmbelicode.Items.Add(oEligibilityCodes(i).Description)
                End If

            Next
        End If
        oEligibilityCodes = Nothing
        oIMEligibilityCodes = Nothing

        '''''''''''''


        Dim _CPTCodes As String = " "



        With C1Flex_Transaction
            'set properties of c1 grid
            .Rows.Count = 1
            .Rows.Fixed = 1
            'Commented by Sanjog
            .Cols.Count = COL_COUNT  ''COL_COUNT2
            .Cols.Count = 36
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 22

            Dim _Width As Single = (.Width - 20) / 11
            'set column value
            .Cols(COL_TRNID).Width = 0
            .Cols(COL_TRNDATE).Width = 0
            .Cols(COL_PATIENTID).Width = 0
            .Cols(COL_VISITID).Width = 0
            .Cols(COL_ITEMID).Width = 49

            .Cols(COL_ITEMNAME).Width = _Width * 1.7



            .Cols(COL_ITEMCOUNTERID).Width = 50


            .Cols(COL_DOSE).Width = _Width * 1.2
            ''Added By Shweta 20100915 for MU
            .Cols(COL_DOSEUNIT).Width = _Width * 1.2
            ''END-Added By Shweta 20100915 for MU 
            .Cols(COL_DATEGIVEN).Width = _Width * 1.3
            .Cols(COL_TIMEGIVEN).Width = _Width * 1.3



            .Cols(COL_ROUTE).Width = _Width * 1.3



            .Cols(COL_LOTNUMBER).Width = _Width * 1.2



            .Cols(COL_EXPDATE).Width = _Width * 1.3



            .Cols(COL_MANUFACT).Width = _Width * 1.3



            .Cols(COL_ISLOCK).Width = 0 '_Width * 1



            .Cols(COL_USERID).Width = 0 '_Width * 1
            .Cols(COL_NOTES).Width = _Width * 1.3
            .Cols(COL_IDENTIFIER).Width = 0 '_Width * 1
            .Cols(COL_DUEDATE).Width = _Width * 1.3


            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
            .Cols(COL_REACTION).Width = _Width * 2.7


            .Cols(COL_REACTIONBTN).Width = _Width * 0.3


            .Cols(COL_REACTIONDT).Width = _Width * 1.35
            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
            .Cols(COL_SITE).Width = _Width * 1.3
            .Cols(Col_USERNAME).Width = _Width * 1.2

            'sarika
            .Cols(COL_ISREMINDER).Width = _Width * 1.3
            .Cols(COL_VACCINEELIGIBILITYCODE).Width = _Width * 2.0
            '---
            .Cols(COL_REASONFORNONADMIN).Width = _Width * 2.0
            '---------
            .Cols(COL_CPTCODES).Width = _Width * 1.3
            .Cols(Col_CPTCodesHidden).Width = 0
            .Cols(Col_ItemName2).Width = 0

            ''Code Start-Added by kanchan on 20100904 for snomed implementation 
            .Cols(COL_ConceptID).Width = _Width * 1.3
            .Cols(COL_DescriptionID).Width = _Width * 1.3
            .Cols(COL_SnomedID).Width = _Width * 1.3
            ''Code Start-Added by kanchan on 20100904 for snomed implementation 

            'Sanjog
            .Cols(COL_AdminStatus).Width = _Width * 1.3


            'set column header
            .SetData(0, COL_TRNID, "Transaction ID")
            .SetData(0, COL_TRNDATE, "Trn. Date")
            .SetData(0, COL_PATIENTID, "Pat. ID")
            .SetData(0, COL_VISITID, "Visit. ID")
            .SetData(0, COL_ITEMID, "Item ID")
            .SetData(0, COL_ITEMNAME, "Immunization")
            .SetData(0, COL_ITEMCOUNTERID, "Counter")
            .SetData(0, COL_DOSE, "Dose")
            ''Added By Shweta 20100915 for MU
            .SetData(0, COL_DOSEUNIT, "Unit")
            ''END-Added By Shweta 20100915 for MU
            .SetData(0, COL_DATEGIVEN, "Date Given")
            .SetData(0, COL_TIMEGIVEN, "Time Given")
            .SetData(0, COL_ROUTE, "Route")
            .SetData(0, COL_LOTNUMBER, "Lot No.")
            .SetData(0, COL_EXPDATE, "Exp. Date")
            .SetData(0, COL_MANUFACT, "Manufacturer")
            .SetData(0, COL_ISLOCK, "Lock")
            .SetData(0, COL_USERID, "User ID")
            .SetData(0, COL_NOTES, "Notes")
            .SetData(0, COL_DUEDATE, "Due Date")

            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
            .SetData(0, COL_REACTION, "Reaction")
            .SetData(0, COL_REACTIONBTN, " ")
            .SetData(0, COL_REACTIONDT, "Reaction Date")
            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110



            .SetData(0, COL_SITE, "Site")
            '--------
            .SetData(0, COL_CPTCODES, "CPT Codes")
            '--------
            .SetData(0, Col_USERNAME, "User Name")

            'sarika
            .SetData(0, COL_ISREMINDER, "Reminder")
            .SetData(0, COL_VACCINEELIGIBILITYCODE, "Eligibility Code")
            '---------
            .SetData(0, COL_REASONFORNONADMIN, "Reason For Non Admin")
            .SetData(0, Col_CPTCodesHidden, "CPT Codes Hidden")
            .SetData(0, Col_VaccineCode, "CPT Codes Hidden")
            .SetData(0, Col_ItemName2, "Item Name Hidden")

            'Code Start-Added by kanchan on 20100904 for snomed implementation 
            .SetData(0, COL_ConceptID, "ConceptID")
            .SetData(0, COL_DescriptionID, "DescriptionID")
            .SetData(0, COL_SnomedID, "SnomedID")
            'Code End-Added by kanchan on 20100904 for snomed implementation 

            .SetData(0, COL_AdminStatus, "Admin Status")

            'set visibility of the column
            .Cols(COL_TRNID).Visible = False
            .Cols(COL_TRNDATE).Visible = False 'True
            .Cols(COL_PATIENTID).Visible = False
            .Cols(COL_VISITID).Visible = False
            .Cols(COL_ITEMID).Visible = False
            .Cols(COL_ITEMNAME).Visible = True
            .Cols(COL_ITEMCOUNTERID).Visible = False 'true
            .Cols(COL_DOSE).Visible = True
            ''Added By Shweta 20100915 for MU
            .Cols(COL_DOSEUNIT).Visible = True
            ''END-Added By Shweta 20100915 for MU
            .Cols(COL_DATEGIVEN).Visible = True
            .Cols(COL_TIMEGIVEN).Visible = True
            .Cols(COL_ROUTE).Visible = True
            .Cols(COL_LOTNUMBER).Visible = True
            .Cols(COL_EXPDATE).Visible = True
            .Cols(COL_MANUFACT).Visible = True
            .Cols(COL_ISLOCK).Visible = False
            .Cols(COL_USERID).Visible = False
            .Cols(COL_NOTES).Visible = True
            .Cols(COL_IDENTIFIER).Visible = False
            .Cols(COL_DUEDATE).Visible = True

            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
            .Cols(COL_REACTION).Visible = True
            .Cols(COL_REACTIONBTN).Visible = False
            .Cols(COL_REACTIONDT).Visible = True
            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110


            .Cols(COL_SITE).Visible = True
            '-------
            .Cols(COL_CPTCODES).Visible = True
            '-------
            .Cols(Col_USERNAME).Visible = True

            'sarika
            .Cols(COL_ISREMINDER).Visible = True
            .Cols(COL_VACCINEELIGIBILITYCODE).Visible = True
            '---
            .Cols(COL_REASONFORNONADMIN).Visible = True
            .Cols(Col_CPTCodesHidden).Visible = False
            .Cols(Col_VaccineCode).Visible = False
            .Cols(Col_ItemName2).Visible = False
            'set data type of the column
            .Cols(COL_TRNID).DataType = GetType(Long)
            .Cols(COL_TRNDATE).DataType = GetType(String)
            .Cols(COL_PATIENTID).DataType = GetType(Long)
            .Cols(COL_VISITID).DataType = GetType(Long)
            .Cols(COL_ITEMID).DataType = GetType(Long)
            .Cols(COL_ITEMNAME).DataType = GetType(String)
            .Cols(COL_ITEMCOUNTERID).DataType = GetType(Integer)
            .Cols(COL_DOSE).DataType = GetType(String)
            .Cols(COL_DATEGIVEN).DataType = GetType(String)
            .Cols(COL_TIMEGIVEN).DataType = GetType(String)
            .Cols(COL_TIMEGIVEN).Format = "hh:mm:ss tt"
            .Cols(COL_TIMEGIVEN).EditMask = "00:00:00 LL"
            .Cols(COL_ROUTE).DataType = GetType(String)
            'sarika
            .Cols(Col_CPTCodesHidden).DataType = GetType(String)
            .Cols(Col_VaccineCode).DataType = GetType(String)



            .Cols(COL_ISREMINDER).DataType = GetType(System.Boolean)
            .Cols(COL_VACCINEELIGIBILITYCODE).DataType = GetType(String)

            .Cols(COL_REASONFORNONADMIN).DataType = GetType(String)


            .Cols(COL_LOTNUMBER).DataType = GetType(String)
            .Cols(COL_EXPDATE).DataType = GetType(String)

            .Cols(COL_MANUFACT).DataType = GetType(String)

            .Cols(COL_ISLOCK).DataType = GetType(Boolean)
            .Cols(COL_USERID).DataType = GetType(Long)
            .Cols(COL_NOTES).DataType = GetType(String)
            .Cols(COL_DUEDATE).DataType = GetType(String)

            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110                  
            .Cols(COL_REACTION).DataType = GetType(String)
            .Cols(COL_REACTIONDT).DataType = GetType(String)

            .Cols(COL_SITE).DataType = GetType(String)


            .Cols(Col_USERNAME).DataType = GetType(String)
            .Cols(Col_ItemName2).DataType = GetType(String)

            'Code Start-Added by kanchan on 20100904 for snomed implementation 
            .Cols(COL_ConceptID).Visible = True
            .Cols(COL_DescriptionID).Visible = True
            .Cols(COL_SnomedID).Visible = True
            'Code End-Added by kanchan on 20100904 for snomed implementation 

            ''Added BY Shweta 20100915
            .Cols(COL_DOSEUNIT).DataType = GetType(String)
            ''End Shweta 20100915

            ' set column editing properties.
            .Cols(COL_TRNID).AllowEditing = False
            .Cols(COL_TRNDATE).AllowEditing = False
            .Cols(COL_PATIENTID).AllowEditing = False
            .Cols(COL_VISITID).AllowEditing = False
            .Cols(COL_ITEMID).AllowEditing = False
            .Cols(COL_ITEMNAME).AllowEditing = False
            .Cols(COL_ITEMCOUNTERID).AllowEditing = False
            .Cols(COL_DOSE).AllowEditing = False
            .Cols(COL_DATEGIVEN).AllowEditing = False
            .Cols(COL_TIMEGIVEN).AllowEditing = False
            .Cols(COL_ROUTE).AllowEditing = False
            .Cols(COL_LOTNUMBER).AllowEditing = False
            .Cols(COL_EXPDATE).AllowEditing = False
            .Cols(COL_MANUFACT).AllowEditing = False
            .Cols(COL_ISLOCK).AllowEditing = False
            .Cols(COL_USERID).AllowEditing = False
            .Cols(COL_NOTES).AllowEditing = False
            .Cols(COL_DUEDATE).AllowEditing = False

            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
            .Cols(COL_REACTION).AllowEditing = False
            .Cols(COL_REACTIONBTN).AllowEditing = False
            .Cols(COL_REACTIONDT).AllowEditing = False
            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110


            .Cols(COL_SITE).AllowEditing = False
            '-------
            .Cols(COL_CPTCODES).AllowEditing = False
            '-------
            .Cols(Col_USERNAME).AllowEditing = False

            'sarika
            .Cols(COL_ISREMINDER).AllowEditing = False
            .Cols(COL_VACCINEELIGIBILITYCODE).AllowEditing = False
            '---
            .Cols(COL_REASONFORNONADMIN).AllowEditing = False

            .Cols(COL_DOSE).TextAlign = TextAlignEnum.LeftCenter
            ''Added By Shweta 20100915 for MU
            .Cols(COL_DOSEUNIT).TextAlign = TextAlignEnum.LeftCenter
            ''END-Added By Shweta 20100915 for MU

            .Cols(Col_CPTCodesHidden).AllowEditing = False
            .Cols(Col_ItemName2).AllowEditing = False

            'Code Start-Added by kanchan on 20100904 for snomed implementation 
            .Cols(COL_ConceptID).AllowEditing = False
            .Cols(COL_DescriptionID).AllowEditing = False
            .Cols(COL_SnomedID).AllowEditing = False
            'Code End-Added by kanchan on 20100904 for snomed implementation 

            ''Added BY Shweta 20100915
            .Cols(COL_DOSEUNIT).AllowEditing = False
            ''End Shweta 20100915

            'set tree properties.
            .Tree.Column = COL_ITEMNAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.Indent = 15

            oItemDetails = oImmunization.ItemDetails()
            'Dim strCurrentTime As String = Format(Now, "hh:mm:ss")
            ' loop for root node fill
            Dim _strImmu As String = ""
            'If Not oItemDetails Is Nothing Then
            '    If oItemDetails.Count > 0 Then


            '    End If
            'End If

            'set flag for add/edit
            Dim oIMFlag As New gloStream.Immunization.Transaction
            _EditID = oIMFlag.IsImmunizationExists(_PatientID)
            If _EditID > 0 Then
                _SaveFlag = False
            Else
                _SaveFlag = True
            End If
            oIMFlag = Nothing


            ' '' <><><> Record Level Locking <><><><> 
            ' '' Mahesh - 20070718 
            If gblnRecordLocking = True Then
                Dim mydt As New mytable
                mydt = Scan_n_Lock_Transaction(TrnType.Immunization, _EditID, 0, Now)
                If mydt.Description <> gstrClientMachineName Then
                    MessageBox.Show("This Patient Immunization is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    _blnRecordLock = True
                End If
            End If

            '''' <><><> Record Level Locking <><><><> 

            ' ''set values to columns

        End With
    End Sub

    Private Sub Fill_UpdateCopy()
        Dim oTransaction As New gloStream.Immunization.Transaction
        Dim oIMTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction
        Dim oIMTransactionLine As New gloStream.Immunization.Supporting.ImmunizationTransactionLine
        Dim r As Integer
        '''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
        Dim StrReaction As String = ""
        Dim csReaction As CellStyle


        oIMTransaction = oTransaction.TransactionDetail(_PatientID)

        If Not oIMTransaction Is Nothing Then
            If oIMTransaction.TransactionLines.Count > 0 Then
                C1Flex_Transaction.Focus()
                C1Flex_Transaction.Select()
                With C1Flex_Transaction
                    For i As Integer = 1 To oIMTransaction.TransactionLines.Count
                        oIMTransactionLine = oIMTransaction.TransactionLines(i)

                        C1Flex_Transaction.Rows.Add(1)
                        r = C1Flex_Transaction.Rows.Count - 1
                        If IsDate(oIMTransactionLine.DateGiven) And oIMTransactionLine.DateGiven <> "#12:00:00 AM#" Then
                            ''''' If Exp Date is Valid

                            Dim _dtgiven As String() = oIMTransactionLine.DateGiven.ToString().Split(" ")

                            .SetData(r, COL_DATEGIVEN, Convert.ToDateTime(_dtgiven(0).Trim()).ToString("MM/dd/yyyy"))


                            ' .SetData(r, COL_DATEGIVEN, oIMTransactionLine.DateGiven)

                            .SetData(r, COL_TIMEGIVEN, oIMTransactionLine.TimeGiven)

                        Else
                            ''''' if exp date is Invalid
                            .SetData(r, COL_EXPDATE, Nothing)

                        End If

                        .SetData(r, COL_DOSE, oIMTransactionLine.Dose)
                        ''Added By Shweta 20100915 for MU
                        .SetData(r, COL_DOSEUNIT, oIMTransactionLine.DoseUnit)
                        ''END-Added By Shweta 20100915 for MU
                        .SetData(r, COL_ITEMID, oIMTransactionLine.ItemID)
                        .SetData(r, COL_ITEMNAME, oIMTransactionLine.ItemName & oIMTransactionLine.ItemCounterID)
                        .SetData(r, Col_ItemName2, oIMTransactionLine.ItemName)
                        .SetData(r, COL_ITEMCOUNTERID, oIMTransactionLine.ItemCounterID)
                        If IsDate(oIMTransactionLine.ExpiryDate) And oIMTransactionLine.ExpiryDate <> "#12:00:00 AM#" Then
                            ''''' If Exp Date is Valid 
                            Dim _expdate As String() = oIMTransactionLine.ExpiryDate.ToString().Split(" ")

                            .SetData(r, COL_EXPDATE, Convert.ToDateTime(_expdate(0).Trim()).ToString("MM/dd/yyyy"))


                            '.SetData(r, COL_EXPDATE, oIMTransactionLine.ExpiryDate)
                        Else
                            ''''' if exp date is Invalid
                            .SetData(r, COL_EXPDATE, Nothing)
                        End If
                        ''''''''''''

                        .SetData(r, COL_LOTNUMBER, oIMTransactionLine.LotNumber)
                        .SetData(r, COL_MANUFACT, oIMTransactionLine.Manufacturer)
                        .SetData(r, COL_NOTES, oIMTransactionLine.Notes)
                        .SetData(r, COL_ROUTE, oIMTransactionLine.Route)

                        'code added at 20070205
                        If IsDate(oIMTransactionLine.DueDate) And oIMTransactionLine.DueDate <> "#12:00:00 AM#" Then
                            Dim _duedate As String() = oIMTransactionLine.DueDate.ToString().Split(" ")

                            .SetData(r, COL_DUEDATE, Convert.ToDateTime(_duedate(0).Trim()).ToString("MM/dd/yyyy"))

                        Else
                            .SetData(r, COL_DUEDATE, Nothing)
                        End If
                        ''


                        ''
                        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                        '''''''''To set the values in csReaction Combolist
                        Try
                            ''   MessageBox.Show(_N)

                            csReaction = .Styles.Add("csReaction" & r)
                            StrReaction = ""
                            '' Fill Values In ComboBox
                            If Not IsDBNull(oIMTransactionLine.Reaction) Then
                                .SetData(r, COL_REACTION, oIMTransactionLine.Reaction)
                                ''''''''''''''' Added by Ujwala Atre 
                                Dim arrReaction As String()
                                arrReaction = oIMTransactionLine.Reaction.Split(vbNewLine)
                                .Rows(r).Height = .Rows.DefaultSize * arrReaction.Length - 1
                                ''''''''''''''' Added by Ujwala Atre 
                            End If

                        Catch ex As Exception

                        End Try


                        If IsDate(oIMTransactionLine.ReactionDT) And oIMTransactionLine.ReactionDT <> "#12:00:00 AM#" Then
                            .SetData(r, COL_REACTIONDT, oIMTransactionLine.ReactionDT)
                        Else
                            .SetData(r, COL_REACTIONDT, "")
                        End If

                        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110








                        If IsDate(oIMTransactionLine.TransactionDate) And oIMTransactionLine.TransactionDate <> "#12:00:00 AM#" Then
                            .SetData(r, COL_TRNDATE, Convert.ToDateTime(oIMTransactionLine.TransactionDate).ToString("MM/dd/yyyy"))

                        Else
                            .SetData(r, COL_TRNDATE, "")
                        End If

                        .SetData(r, COL_USERID, oIMTransactionLine.UserID)
                        .SetData(r, COL_VISITID, oIMTransactionLine.VisitID)
                        .SetData(r, COL_TRNID, oIMTransaction.TransactionID)

                        '' modification on 20070425 for CCHIT 2007
                        If Not IsDBNull(COL_SITE) Then
                            .SetData(r, COL_SITE, oIMTransactionLine.Site)
                        Else
                            .SetData(r, COL_SITE, Nothing)
                        End If

                        'Sanjog - Added for admin status
                        If oIMTransactionLine.AdminStatus = "" Then
                            .SetData(r, COL_AdminStatus, "Administered")
                        ElseIf oIMTransactionLine.AdminStatus = "0" Then
                            .SetData(r, COL_AdminStatus, "Administered")
                        Else
                            .SetData(r, COL_AdminStatus, "Reported")
                        End If


                        .SetData(r, Col_USERNAME, oIMTransactionLine.UserName)
                        ''

                        '' Sarika 20080604
                        If Not IsDBNull(oIMTransactionLine.IsReminder) Then
                            If oIMTransactionLine.IsReminder = True Then
                                .SetCellCheck(r, COL_ISREMINDER, CheckEnum.Checked)
                            Else
                                .SetCellCheck(r, COL_ISREMINDER, CheckEnum.Unchecked)
                            End If
                        Else
                            .SetCellCheck(r, COL_ISREMINDER, CheckEnum.Unchecked)
                        End If

                        If Not IsDBNull(oIMTransactionLine.EligibilityCode) Then
                            .SetData(r, COL_VACCINEELIGIBILITYCODE, oIMTransactionLine.EligibilityCode)
                        Else
                            .SetData(r, COL_VACCINEELIGIBILITYCODE, Nothing)
                        End If


                        If Not IsDBNull(oIMTransactionLine.ReasonForNonAdmin) Then
                            .SetData(r, COL_REASONFORNONADMIN, oIMTransactionLine.ReasonForNonAdmin)
                        Else
                            .SetData(r, COL_REASONFORNONADMIN, Nothing)
                        End If


                        If Not IsDBNull(oIMTransactionLine.CPTCode) Then
                            .SetData(r, COL_CPTCODES, oIMTransactionLine.CPTCode)
                        Else
                            .SetData(r, COL_CPTCODES, Nothing)
                        End If


                        If Not IsDBNull(oIMTransactionLine.VaccineCode) Then
                            .SetData(r, Col_VaccineCode, oIMTransactionLine.VaccineCode)
                        Else
                            .SetData(r, Col_VaccineCode, Nothing)
                        End If

                        'Code Start-Added by kanchan on 20100904 for snomed implementation 
                        .SetData(r, COL_ConceptID, oIMTransactionLine.ConceptID)
                        .SetData(r, COL_DescriptionID, oIMTransactionLine.DescriptionID)
                        .SetData(r, COL_SnomedID, oIMTransactionLine.SnoMedID)
                        'Code End-Added by kanchan on 20100904 for snomed implementation
                        ''Added By Shweta 20100915
                        .SetData(r, COL_DOSEUNIT, oIMTransactionLine.DoseUnit)
NEXTLOOP:

                    Next


                End With
            End If
        Else

            dtpTransaction.TabIndex = 0
            dtpTransaction.Focus()
            dtpTransaction.Select()
        End If
    End Sub



    ' ''function for set the values to column.
    Private Sub Fill_Update()
        Dim oTransaction As New gloStream.Immunization.Transaction
        Dim oIMTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction
        Dim oIMTransactionLine As New gloStream.Immunization.Supporting.ImmunizationTransactionLine
        Dim r As Integer
        '''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
        Dim StrReaction As String = ""
        Dim csReaction As CellStyle
        ''With C1FlexTransaction
        ''    Dim csR1 As CellStyle = .Styles.Add("csR1")
        ''    csR1.ComboList = " |"
        ''    .Cols(COL_REACTION).Style = csR1
        ''End With
        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

        '''''''''''''''''''''''''''''''''''''''''''''


        oIMTransaction = oTransaction.TransactionDetail(_PatientID)

        If Not oIMTransaction Is Nothing Then
            If oIMTransaction.TransactionLines.Count > 0 Then
                C1FlexTransaction.Focus()
                C1FlexTransaction.Select()
                With C1FlexTransaction
                    For i As Integer = 1 To oIMTransaction.TransactionLines.Count
                        oIMTransactionLine = oIMTransaction.TransactionLines(i)

                        'Dim r As Integer = .FindRow(oIMTransactionLine.ItemName, 1, COL_ITEMNAME, False, True, False)
                        r = .FindRow("I" & oIMTransactionLine.ItemID & "C" & oIMTransactionLine.ItemCounterID, 1, COL_IDENTIFIER, False, True, False)
                        If r < 1 Then
                            GoTo NEXTLOOP
                        End If
                        '.SetData(r, COL_DATEGIVEN, oIMTransactionLine.DateGiven)
                        If IsDate(oIMTransactionLine.DateGiven) And oIMTransactionLine.DateGiven <> "#12:00:00 AM#" Then
                            ''''' If Exp Date is Valid

                            .SetData(r, COL_DATEGIVEN, oIMTransactionLine.DateGiven)
                            'If InStr(oIMTransactionLine.TimeGiven, ":") Then
                            '    C1FlexTransaction.Cols(COL_TIMEGIVEN).EditMask = GetMask(oIMTransactionLine.TimeGiven)
                            'Else
                            '    C1FlexTransaction.Cols(COL_TIMEGIVEN).Format = GetMask(oIMTransactionLine.TimeGiven)
                            'End If
                            'If oIMTransactionLine.TimeGiven <> "" Then
                            '    .SetData(r, COL_TIMEGIVEN, oIMTransactionLine.TimeGiven)
                            'Else
                            '    .Cols(COL_TIMEGIVEN).EditMask = GetMask(oIMTransactionLine.TimeGiven)
                            'End If
                            .SetData(r, COL_TIMEGIVEN, oIMTransactionLine.TimeGiven)
                            .SetCellCheck(r, COL_ITEMNAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            .Rows(r).Style = .Styles("CS_Given")
                        Else
                            ''''' if exp date is Invalid
                            .SetData(r, COL_EXPDATE, Nothing)
                            '.Rows(r).Style = .Styles("CS_Due")
                        End If

                        .SetData(r, COL_DOSE, oIMTransactionLine.Dose)
                        ''Added By Shweta 20100915 for MU
                        .SetData(r, COL_DOSEUNIT, oIMTransactionLine.DoseUnit)
                        ''END-Added By Shweta 20100915 for MU
                        '.SetData(r, COL_EXPDATE, oIMTransactionLine.ExpiryDate)
                        ''''''''''''''
                        If IsDate(oIMTransactionLine.ExpiryDate) And oIMTransactionLine.ExpiryDate <> "#12:00:00 AM#" Then
                            ''''' If Exp Date is Valid 
                            .SetData(r, COL_EXPDATE, oIMTransactionLine.ExpiryDate)
                        Else
                            ''''' if exp date is Invalid
                            .SetData(r, COL_EXPDATE, Nothing)
                        End If
                        ''''''''''''

                        '.SetData(r, COL_ITEMCOUNTERID, oIMTransactionLine.ItemCounterID)
                        '.SetData(r, COL_ITEMID, oIMTransactionLine.ItemID)
                        .SetData(r, COL_LOTNUMBER, oIMTransactionLine.LotNumber)
                        .SetData(r, COL_MANUFACT, oIMTransactionLine.Manufacturer)
                        .SetData(r, COL_NOTES, oIMTransactionLine.Notes)
                        .SetData(r, COL_ROUTE, oIMTransactionLine.Route)

                        'code added at 20070205
                        If IsDate(oIMTransactionLine.DueDate) And oIMTransactionLine.DueDate <> "#12:00:00 AM#" Then
                            .SetData(r, COL_DUEDATE, oIMTransactionLine.DueDate)
                            If .GetCellCheck(r, COL_ITEMNAME) <> CheckEnum.Checked Then
                                .Rows(r).Style = .Styles("CS_Due")
                            End If
                        Else
                            .SetData(r, COL_DUEDATE, Nothing)
                        End If
                        ''


                        ''
                        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                        '''''''''To set the values in csReaction Combolist
                        Try
                            ''   MessageBox.Show(_N)

                            csReaction = .Styles.Add("csReaction" & r)
                            StrReaction = ""
                            '' Fill Values In ComboBox
                            If Not IsDBNull(oIMTransactionLine.Reaction) Then
                                StrReaction = oIMTransactionLine.Reaction.ToString
                                StrReaction = StrReaction.Replace(",", "|")

                                'Dim splstr As String() = StrReaction.Split("|")
                                'For splstrlen As Integer = 0 To splstr.Length - 1
                                '    cmbreac.Items.Add(splstr(splstrlen))
                                '    chkreaction.Items.Add(splstr(splstrlen))
                                'Next
                                If (StrReaction = "") Then
                                    StrReaction = " |"
                                End If

                                csReaction.ComboList = StrReaction
                                .Cols(COL_REACTION).Style = csReaction
                                .Cols(COL_REACTION).TextAlign = TextAlignEnum.LeftCenter

                                If (StrReaction.IndexOf("|") >= 0) Then
                                    .SetData(r, COL_REACTION, StrReaction.Substring(0, StrReaction.IndexOf("|")))
                                ElseIf (StrReaction.Contains("|") = False) Then
                                    .SetData(r, COL_REACTION, StrReaction)
                                End If
                            End If
                            Dim cR As C1.Win.C1FlexGrid.CellRange = .GetCellRange(r, COL_REACTION)
                            cR.Style = csReaction
                        Catch ex As Exception
                            ''''''''' MessageBox.Show(ex.ToString)
                        End Try

                        ' .SetData(i + 1, COL_PRESCRIPTION, dt.Rows(i)("Prescription"))
                        ' ''Dim cR As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i + 1, COL_PRESCRIPTION)
                        ' ''cR.Style = csPresc
                        ' ''.SetData(r, COL_REACTION, oIMTransactionLine.Reaction)

                        ''''''''''''''''                       

                        ''''''''''''''2701 .SetData(r, COL_REACTIONBTN, "")

                        'Dim rgRx As C1.Win.C1FlexGrid.CellRange = .GetCellRange(r, COL_REACTIONBTN, r, COL_REACTIONBTN)
                        'rgRx.Style = cStyle1
                        ''''''''''''''''
                        If IsDate(oIMTransactionLine.ReactionDT) And oIMTransactionLine.ReactionDT <> "#12:00:00 AM#" Then
                            .SetData(r, COL_REACTIONDT, oIMTransactionLine.ReactionDT)
                        Else
                            .SetData(r, COL_REACTIONDT, Nothing)
                        End If

                        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110








                        '.SetData(r, COL_TRNDATE, oIMTransactionLine.TransactionDate)
                        If IsDate(oIMTransactionLine.TransactionDate) And oIMTransactionLine.TransactionDate <> "#12:00:00 AM#" Then
                            .SetData(r, COL_TRNDATE, oIMTransactionLine.TransactionDate)
                            .SetCellCheck(r, COL_ITEMNAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
                        Else
                            .SetData(r, COL_TRNDATE, Nothing)
                        End If

                        .SetData(r, COL_USERID, oIMTransactionLine.UserID)
                        .SetData(r, COL_VISITID, oIMTransactionLine.VisitID)
                        .SetData(r, COL_TRNID, oIMTransaction.TransactionID)

                        '' modification on 20070425 for CCHIT 2007
                        If Not IsDBNull(COL_SITE) Then
                            .SetData(r, COL_SITE, oIMTransactionLine.Site)
                        Else
                            .SetData(r, COL_SITE, Nothing)
                        End If



                        .SetData(r, Col_USERNAME, oIMTransactionLine.UserName)
                        ''

                        '' Sarika 20080604
                        If Not IsDBNull(oIMTransactionLine.IsReminder) Then
                            If oIMTransactionLine.IsReminder = True Then
                                .SetCellCheck(r, COL_ISREMINDER, CheckEnum.Checked)
                            Else
                                .SetCellCheck(r, COL_ISREMINDER, CheckEnum.Unchecked)
                            End If
                        Else
                            .SetCellCheck(r, COL_ISREMINDER, CheckEnum.Unchecked)
                        End If

                        If Not IsDBNull(oIMTransactionLine.EligibilityCode) Then
                            .SetData(r, COL_VACCINEELIGIBILITYCODE, oIMTransactionLine.EligibilityCode)
                        Else
                            .SetData(r, COL_VACCINEELIGIBILITYCODE, Nothing)
                        End If


                        If Not IsDBNull(oIMTransactionLine.ReasonForNonAdmin) Then
                            .SetData(r, COL_REASONFORNONADMIN, oIMTransactionLine.ReasonForNonAdmin)
                        Else
                            .SetData(r, COL_REASONFORNONADMIN, Nothing)
                        End If
                        '.Rows(r).AllowEditing = True

                        If Not IsDBNull(oIMTransactionLine.CPTCode) Then
                            .SetData(r, COL_CPTCODES, oIMTransactionLine.CPTCode)
                        Else
                            .SetData(r, COL_CPTCODES, Nothing)
                        End If


                        If Not IsDBNull(oIMTransactionLine.VaccineCode) Then
                            .SetData(r, Col_VaccineCode, oIMTransactionLine.VaccineCode)
                        Else
                            .SetData(r, Col_VaccineCode, Nothing)
                        End If

NEXTLOOP:

                    Next


                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                    '''''''''''''''''''''''''''''''''''''''''

                    For r = 1 To C1FlexTransaction.Rows.Count - 1
                        Try
                            ''  MessageBox.Show(.GetData(r, COL_REACTION).ToString.Trim())
                            If .GetData(r, COL_REACTION).ToString.Trim() = "" Then
                                csReaction = .Styles.Add("csReaction" & r)
                                StrReaction = " |"
                                csReaction.ComboList = StrReaction
                                .Cols(COL_REACTION).Style = csReaction
                            End If
                            '''''''''''''''                            
                        Catch

                            csReaction = .Styles.Add("csReaction" & r)
                            StrReaction = " |"
                            csReaction.ComboList = StrReaction
                            .Cols(COL_REACTION).Style = csReaction
                            '''''''''''''''

                        End Try
                    Next
                    '''''''''''''''''''''''''''''''''''''''''
                    'cStyle1 = .Styles.Add("BubbleValues")
                    'cStyle1.ComboList = "..."
                    '.Cols(COL_REACTIONBTN).Style = cStyle1

                    ''Dim rgRx As C1.Win.C1FlexGrid.CellRange = .GetCellRange(1, COL_REACTIONBTN, (.Rows.Count - 1), COL_REACTIONBTN)
                    ''rgRx.Style = cStyle1
                    ''''''''''''
                    '''''''''''''''''''''''''''''''''''''''''
                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110




                End With
            End If
        Else

            dtpTransaction.TabIndex = 0
            dtpTransaction.Focus()
            dtpTransaction.Select()
        End If
    End Sub
    Public Function GetMask(ByVal str As String) As String
        Select Case str
            Case "12:12:12"
                Return "::"
            Case str
                Return "hh:mm:ss tt"
        End Select

    End Function


    Private Function GetC1Node(ByVal FindItem As String) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        ' Dim _FindRow As Integer = C1FlexTransaction.FindRow(FindItem, 0, COL_IDENTITY, True, True, False)
        Dim _FindRow As Integer = C1FlexTransaction.FindRow(FindItem, 0, COL_ITEMNAME, True, True, False)
        If _FindRow > 0 Then
            _Node = C1FlexTransaction.Rows(_FindRow).Node
        End If
        Return _Node
    End Function


#Region "Button code Not in Use. Instead of that Tool bar code is used."

    'Private Sub btnIMTransactionClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim Result As DialogResult
    '    If _blnChangesMade = True Then
    '        Result = MessageBox.Show("Do you want to save the changes ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

    '        If Result = Windows.Forms.DialogResult.Yes Then
    '            btnIMTransactionSave_Click(sender, e)
    '            _blnChangesMade = False
    '            Me.Close()
    '        ElseIf Result = Windows.Forms.DialogResult.No Then
    '            _blnChangesMade = False
    '            Me.Close()
    '        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
    '            Exit Sub
    '        End If

    '    Else
    '        Me.Close()
    '    End If
    'End Sub

    '' function for save the edit data
    'Private Sub btnIMTransactionSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim _result As Boolean = False
    '    Dim _SaveTrnID As Long = 0

    '    Try
    '        Dim oImmunizationTransactionLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine
    '        Dim oTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction
    '        Dim oIM As New gloStream.Immunization.Transaction

    '        ' chekc for mandatory fiels of selectd rows
    '        With C1FlexTransaction
    '            For i As Integer = 1 To C1FlexTransaction.Rows.Count - 1
    '                If .GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    '                    If .GetData(i, COL_DATEGIVEN) = Nothing Then
    '                        MessageBox.Show("Please enter the given date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                        .RowSel = i
    '                        Exit Sub
    '                    End If
    '                End If
    '            Next
    '        End With

    '        With oTransaction
    '            'Master
    '            .TransactionID = 0
    '            .PatientID = _PatientID

    '            ' assign values to the properties
    '            For i As Integer = 1 To C1FlexTransaction.Rows.Count - 1
    '                If C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Or C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
    '                    oImmunizationTransactionLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
    '                    With oImmunizationTransactionLine
    '                        If C1FlexTransaction.GetData(i, COL_DATEGIVEN) Is Nothing Then
    '                            .DateGiven = "12:00:00 AM"
    '                        Else
    '                            .DateGiven = C1FlexTransaction.GetData(i, COL_DATEGIVEN)
    '                        End If

    '                        If C1FlexTransaction.GetData(i, COL_DOSE) Is Nothing Then
    '                            .Dose = ""
    '                        Else
    '                            .Dose = C1FlexTransaction.GetData(i, COL_DOSE)
    '                        End If

    '                        If C1FlexTransaction.GetData(i, COL_EXPDATE) Is Nothing Then
    '                            .ExpiryDate = "12:00:00 AM"
    '                        Else
    '                            .ExpiryDate = C1FlexTransaction.GetData(i, COL_EXPDATE)
    '                        End If

    '                        If C1FlexTransaction.GetData(i, COL_DUEDATE) Is Nothing Then
    '                            .DueDate = "12:00:00 AM"
    '                        Else
    '                            .DueDate = C1FlexTransaction.GetData(i, COL_DUEDATE)
    '                        End If

    '                        .ItemCounterID = C1FlexTransaction.GetData(i, COL_ITEMCOUNTERID)
    '                        .ItemID = C1FlexTransaction.GetData(i, COL_ITEMID)
    '                        .ItemName = C1FlexTransaction.GetData(i, COL_ITEMNAME) & ""
    '                        .LotNumber = C1FlexTransaction.GetData(i, COL_LOTNUMBER) & ""
    '                        .Manufacturer = C1FlexTransaction.GetData(i, COL_MANUFACT) & ""
    '                        .Notes = C1FlexTransaction.GetData(i, COL_NOTES) & ""
    '                        .Route = C1FlexTransaction.GetData(i, COL_ROUTE) & ""

    '                        '' modified on 20070327
    '                        'If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    '                        '    .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    '                        'Else
    '                        '    .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    '                        'End If

    '                        ' if no data in field then todays default value should be todays date.
    '                        If C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    '                            If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    '                                '.TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    '                                .TransactionDate = DateTime.Today()
    '                            Else
    '                                .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    '                            End If
    '                        Else
    '                            If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    '                                .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    '                            Else
    '                                .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    '                            End If
    '                        End If
    '                        '' modified on 20070327

    '                        .UserID = C1FlexTransaction(i, COL_USERID)
    '                        .VisitID = C1FlexTransaction(i, COL_VISITID)

    '                        '' modification on 20070425 for CCHIT 2007
    '                        .Site = C1FlexTransaction(i, COL_SITE) & ""
    '                        .UserName = C1FlexTransaction(i, Col_USERNAME) & ""
    '                        ''
    '                    End With
    '                    .TransactionLines.Add(oImmunizationTransactionLine)
    '                    oImmunizationTransactionLine = Nothing
    '                End If
    '            Next
    '        End With


    '        _SaveTrnID = oIM.IsImmunizationExists(_PatientID)

    '        'set add/edit flag
    '        'If _SaveFlag = True Then
    '        If _SaveTrnID <= 0 Then
    '            _result = oIM.Add(oTransaction)
    '            gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordAdded, "Immunization Record Added.", gstrLoginName, gstrClientMachineName, _PatientID)

    '        Else
    '            _result = oIM.Modify(_SaveTrnID, oTransaction)
    '            gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordModified, "Immunization Record Modified.", gstrLoginName, gstrClientMachineName, _PatientID)

    '        End If

    '        'If _result = True Then
    '        '    ProgressBar1.Visible = True
    '        '    ProgressBar1.Minimum = 1
    '        '    ProgressBar1.Maximum = 2000
    '        '    For i As Integer = 1 To 2000
    '        '        ProgressBar1.Value = i
    '        '    Next
    '        '    ProgressBar1.Visible = False
    '        'End If

    '        _blnChangesMade = False

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Dim objAudit As New clsAudit
    '        objAudit.CreateLog(clsAudit.enmActivityType.PatientRecordAdded, "Immunization Record Error.", gstrLoginName, gstrClientMachineName, _PatientID, , clsAudit.enmOutCome.Failure)
    '        objAudit = Nothing
    '    End Try

    'End Sub


#End Region

    Private Sub frmIm_Transaction_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        With C1FlexTransaction
            If .Cols.Count = COL_COUNT Then
                Dim _Width As Single = (.Width - 20) / 12
                .Cols(COL_TRNID).Width = 0  '_Width * 2
                .Cols(COL_TRNDATE).Width = _Width * 1
                .Cols(COL_PATIENTID).Width = 0 '_Width * 1
                .Cols(COL_VISITID).Width = 0 '_Width * 1
                .Cols(COL_ITEMID).Width = 0 '_Width * 1
                .Cols(COL_ITEMNAME).Width = _Width * 2
                .Cols(COL_ITEMCOUNTERID).Width = 0 '_Width * 1
                .Cols(COL_DOSE).Width = _Width * 1
                ''Added By Shweta 20100915 for MU
                .Cols(COL_DOSEUNIT).Width = _Width * 1
                ''END-Added By Shweta 20100915 for MU

                .Cols(COL_DATEGIVEN).Width = _Width * 1
                .Cols(COL_ROUTE).Width = _Width * 1
                .Cols(COL_LOTNUMBER).Width = _Width * 1
                .Cols(COL_EXPDATE).Width = _Width * 1
                .Cols(COL_MANUFACT).Width = _Width * 1
                .Cols(COL_ISLOCK).Width = 0 '_Width * 1
                .Cols(COL_USERID).Width = 0 '_Width * 1
                .Cols(COL_NOTES).Width = _Width * 2
                .Cols(COL_DUEDATE).Width = _Width * 1

                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                .Cols(COL_REACTION).Width = _Width * 2.7
                .Cols(COL_REACTIONBTN).Width = _Width * 0.2
                .Cols(COL_REACTIONDT).Width = _Width * 1.3
                '' MessageBox.Show(Convert.ToString(.Cols(COL_REACTIONDT).Width))
                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110


                '' modification on 20070425 for CCHIT 2007
                .Cols(COL_SITE).Width = _Width * 1
                ''
            End If
        End With
    End Sub
    ' function for update/properties the c1flexgrid.
    Private Sub C1FlexTransaction_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        Try
            With C1FlexTransaction
                ''''Last one condition in IF is added by Anil on 01/10/2007  at 11:50 a.m.
                '' That Codition is C1FlexTransaction.GetData(C1FlexTransaction.Row, COL_DUEDATE) = Nothing 
                If C1FlexTransaction.GetCellCheck(C1FlexTransaction.Row, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked And C1FlexTransaction.GetData(C1FlexTransaction.Row, COL_TRNID) > 0 And .Col = COL_ITEMNAME And C1FlexTransaction.GetData(C1FlexTransaction.Row, COL_DUEDATE) = Nothing Then
                    ''''''to clear the user name when the DateGiven and DueDate are not there in the row/record on unchecking the check box. 
                    ' If IsDate(C1FlexTransaction.GetCellCheck(C1FlexTransaction.Row, COL_DATEGIVEN)) Then '= False And .Col = COL_ITEMNAME Then
                    If MessageBox.Show("Are you sure to delete this record details.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        .SetData(.Row, COL_DATEGIVEN, Nothing)
                        .SetData(.Row, COL_TIMEGIVEN, Nothing)
                        .SetData(.Row, COL_DOSE, Nothing)
                        ''Added By Shweta 20100915 for MU
                        .SetData(.Row, COL_DOSEUNIT, Nothing)
                        ''END-Added By Shweta 20100915 for MU
                        .SetData(.Row, COL_ROUTE, Nothing)
                        .SetData(.Row, COL_LOTNUMBER, Nothing)
                        .SetData(.Row, COL_EXPDATE, Nothing)
                        .SetData(.Row, COL_MANUFACT, Nothing)
                        .SetData(.Row, COL_NOTES, Nothing)
                        .SetData(.Row, COL_TRNDATE, Nothing)
                        .SetData(.Row, COL_SITE, Nothing)
                        .SetData(.Row, COL_DUEDATE, Nothing)   '''''Added by Anil on 01/10/2007 at 11:50 a.m.   
                        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                        .SetData(.Row, COL_REACTION, Nothing)
                        ''.SetData(.Row, COL_REACTIONBTN, Nothing)
                        .SetData(.Row, COL_REACTIONDT, Nothing)
                        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110


                        .SetData(.Row, Col_USERNAME, Nothing)  '''''Added by Anil
                        .SetData(.Row, COL_USERID, Nothing)    '''''Added by Anil on 16/10/2007 for solution of bug no.117

                        '.SetData(.Row, COL_ISREMINDER, Nothing)
                        .SetCellCheck(.Row, COL_ISREMINDER, CheckEnum.Unchecked)
                        .SetData(.Row, COL_VACCINEELIGIBILITYCODE, Nothing)
                        .SetData(.Row, COL_REASONFORNONADMIN, Nothing)
                        .SetData(.Row, COL_CPTCODES, Nothing)
                        .SetData(.Row, Col_VaccineCode, Nothing)
                    Else
                        .SetCellCheck(.Row, COL_ITEMNAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

                    End If
                    '   End If
                ElseIf .GetCellCheck(C1FlexTransaction.Row, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                    .SetData(.Row, COL_DATEGIVEN, Nothing)
                    .SetData(.Row, COL_TIMEGIVEN, Nothing)
                    .SetData(.Row, COL_DOSE, Nothing)
                    ''Added By Shweta 20100915 for MU
                    .SetData(.Row, COL_DOSEUNIT, Nothing)
                    ''END-Added By Shweta 20100915 for MU
                    .SetData(.Row, COL_ROUTE, Nothing)
                    .SetData(.Row, COL_LOTNUMBER, Nothing)
                    .SetData(.Row, COL_EXPDATE, Nothing)
                    .SetData(.Row, COL_MANUFACT, Nothing)
                    .SetData(.Row, COL_NOTES, Nothing)
                    .SetData(.Row, COL_TRNDATE, Nothing)
                    .SetData(.Row, COL_SITE, Nothing)
                    '.SetData(.Row, COL_DUEDATE, Nothing)


                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                    .SetData(.Row, COL_REACTION, Nothing)
                    ''.SetData(.Row, COL_REACTIONBTN, Nothing)
                    .SetData(.Row, COL_REACTIONDT, Nothing)
                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

                    .SetData(.Row, Col_USERNAME, Nothing)      '''''Added by Anil
                    .SetData(.Row, COL_USERID, Nothing)        '''''Added by Anil on 16/10/2007 for solution of bug no.117

                    .SetCellCheck(.Row, COL_ISREMINDER, CheckEnum.Unchecked)
                    ' .SetData(.Row, COL_ISREMINDER, CheckEnum.Unchecked)
                    .SetData(.Row, COL_VACCINEELIGIBILITYCODE, Nothing)
                    .SetData(.Row, COL_REASONFORNONADMIN, Nothing)
                    .SetData(.Row, COL_CPTCODES, Nothing)
                    .SetData(.Row, Col_VaccineCode, Nothing)
                    '''''''''''''
                    'C1FlexTransaction.Rows(C1FlexTransaction.Row).AllowEditing = False
                Else
                    ' if check box is checked then TrnDate and Date Given should be fill with current date.
                    ' default date should be todays after selection of checkbox and user can chenge those dates also.
                    If C1FlexTransaction.GetData(.Row, COL_TRNDATE) = Nothing Then
                        .SetData(.Row, COL_TRNDATE, DateTime.Today())
                        .SetData(.Row, COL_DATEGIVEN, DateTime.Today())

                        ''''' modification on 20070425 for CCHIT 2007
                        .SetData(.Row, COL_TIMEGIVEN, Format(Now, "hh:mm:ss tt"))
                        Dim _UserID As Long
                        _UserID = getUserId(gstrLoginName)
                        .SetData(.Row, COL_USERID, _UserID)
                        .SetData(.Row, Col_USERNAME, gstrLoginName)
                    ElseIf (.GetCellCheck(C1FlexTransaction.Row, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked) And C1FlexTransaction.GetData(.Row, COL_TRNDATE) = Nothing Then
                        .SetData(.Row, COL_TRNDATE, DateTime.Today())
                        .SetData(.Row, COL_DATEGIVEN, DateTime.Today())

                        ''''' modification on 20070425 for CCHIT 2007
                        .SetData(.Row, COL_TIMEGIVEN, Format(Now, "hh:mm:ss tt"))
                        Dim _UserID As Long
                        _UserID = getUserId(gstrLoginName)
                        .SetData(.Row, COL_USERID, _UserID)
                        .SetData(.Row, Col_USERNAME, gstrLoginName)
                    End If

                End If

                'If .GetCellCheck(C1FlexTransaction.Row, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                '    .Rows(.Row).AllowEditing = True
                '    '.Cols(COL_TRNID).AllowEditing = False
                '    .Cols(COL_TRNDATE).AllowEditing = True
                '    .Cols(COL_DOSE).AllowEditing = True
                '    .Cols(COL_DATEGIVEN).AllowEditing = True
                '    .Cols(COL_ROUTE).AllowEditing = True
                '    .Cols(COL_LOTNUMBER).AllowEditing = True
                '    .Cols(COL_EXPDATE).AllowEditing = True
                '    .Cols(COL_MANUFACT).AllowEditing = True
                '    .Cols(COL_NOTES).AllowEditing = True
                '    '.Cols(COL_PATIENTID).AllowEditing = False
                '    '.Cols(COL_VISITID).AllowEditing = False
                '    '.Cols(COL_ITEMID).AllowEditing = False
                '    .Cols(COL_ITEMNAME).AllowEditing = True
                '    '.Cols(COL_ITEMCOUNTERID).AllowEditing = False
                '    '.Cols(COL_ISLOCK).AllowEditing = False
                '    '.Cols(COL_USERID).AllowEditing = False
                'End If
            End With

            _blnChangesMade = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    Dim oRpt As ReportDocument
    '    Try

    '        oRpt = New ReportDocument
    '        oRpt.Load(Application.StartupPath & "\Reports\PatientImmunizationSummary.rpt")
    '        Dim crtableLogoninfos As New TableLogOnInfos
    '        Dim crtableLogoninfo As New TableLogOnInfo
    '        Dim crConnectionInfo As New ConnectionInfo
    '        Dim CrTables As Tables
    '        Dim CrTable As Table
    '        Dim TableCounter

    '        With crConnectionInfo
    '            .ServerName = gstrSQLServerName

    '            'If you are connecting to Oracle there is no 
    '            'DatabaseName. Use an empty string. 
    '            'For example, .DatabaseName = "" 

    '            .DatabaseName = gstrDatabaseName

    '            '.UserID = "Your User ID"
    '            '.Password = "Your Password"
    '        End With

    '        'This code works for both user tables and stored 
    '        'procedures. Set the CrTables to the Tables collection 
    '        'of the report 

    '        CrTables = oRpt.Database.Tables

    '        'Loop through each table in the report and apply the 
    '        'LogonInfo information 

    '        For Each CrTable In CrTables
    '            crtableLogoninfo = CrTable.LogOnInfo
    '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '            CrTable.ApplyLogOnInfo(crtableLogoninfo)

    '            'If your DatabaseName is changing at runtime, specify 
    '            'the table location. 
    '            'For example, when you are reporting off of a 
    '            'Northwind database on SQL server you 
    '            'should have the following line of code: 

    '            CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
    '        Next

    '        oRpt.SetParameterValue("PatientID", _PatientID.ToString)

    '        'If blnPrint = False Then
    '        '    Call SetFAXPrinterDefaultSettings()
    '        '    'Retrieve the FAX Cover Page details
    '        '    'Find FAX Parameters
    '        '    'Get Pharmacy FAX No
    '        '    Dim strFAXTo As String
    '        '    Dim strFAXNo As String
    '        '    Dim objmytable As mytable
    '        '    Dim objFAX As New clsFAX
    '        '    objmytable = objFAX.GetPharmacyFAXNo(_PatientID)

    '        '    If Not IsNothing(objmytable) Then
    '        '        gstrFAXContactPersonFAXNo = objmytable.Description
    '        '        gstrFAXContactPerson = objmytable.Code
    '        '    End If
    '        '    If Trim(gstrFAXContactPerson) = "" Then
    '        '        gstrFAXContactPerson = InputBox("Please enter the Pharmacy Name", gstrMessageBoxCaption)
    '        '    End If
    '        '    If gblnFAXCoverPage Then
    '        '        If RetrieveFAXDetails(mdlFAX.enmFAXType.Medication, _PatientID, gstrFAXContactPerson, gstrFAXContactPersonFAXNo, "Medication", 0, visitid, 0) = False Then
    '        '            Exit Sub
    '        '        End If
    '        '    Else
    '        '        If Trim(gstrFAXContactPersonFAXNo) = "" Then
    '        '            gstrFAXContactPersonFAXNo = InputBox("Please enter the Pharmacy FAX No", gstrMessageBoxCaption)
    '        '        End If
    '        '    End If
    '        '    If Trim(gstrFAXContactPersonFAXNo) = "" Then
    '        '        MessageBox.Show("You have not entered the FAX No. So FAX will not be send", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        '    End If

    '        '    'Retrieve FAX Document Name
    '        '    Dim strFAXDocumentName As String
    '        '    strFAXDocumentName = RetrieveFAXDocumentName()
    '        '    If SetFAXPrinterDocumentSettings(strFAXDocumentName) = False Then Exit Sub
    '        '    objFAX.AddPendingFAX(_PatientID, gstrFAXContactPerson, "Medication", gstrFAXContactPersonFAXNo, gstrLoginName, strFAXDocumentName, System.DateTime.Now, CurrentSendingFAXPriority)
    '        '    objFAX = Nothing
    '        '    oRpt.PrintToPrinter(1, False, 0, 0)
    '        'Else

    '            oRpt.PrintToPrinter(1, False, 0, 0)

    '        ' End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRpt As ReportDocument
        Try


            oRpt = New ReportDocument

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            'Dim TableCounter

            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"

                .IntegratedSecurity = True
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            'If rbtSummary.Checked = True Then
            '    oRpt.Load(Application.StartupPath & "\Reports\PatientImmunizationSummary.rpt")
            '    CrTables = oRpt.Database.Tables

            '    'Loop through each table in the report and apply the 
            '    'LogonInfo information 

            '    For Each CrTable In CrTables
            '        crtableLogoninfo = CrTable.LogOnInfo
            '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '        CrTable.ApplyLogOnInfo(crtableLogoninfo)

            '        'If your DatabaseName is changing at runtime, specify 
            '        'the table location. 
            '        'For example, when you are reporting off of a 
            '        'Northwind database on SQL server you 
            '        'should have the following line of code: 

            '        CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            '    Next
            '    oRpt.SetParameterValue("PatientID", _PatientID.ToString)

            'Else
            ' For Displaying report of patients for which Imm is DUE

            'oRpt = New ReportDocument
            oRpt.Load(Application.StartupPath & "\Reports\PatientImmunizationDues.rpt")

            CrTables = oRpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
            oRpt.SetParameterValue("PatientID", _PatientID.ToString)

            ' End If

            oRpt.PrintToPrinter(1, False, 0, 0)

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Immunization report printed.", gstrLoginName, gstrClientMachineName, _PatientID)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Immunization report printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Immunization report printed.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'objAudit = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Dim objAudit As New clsAudit
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'objAudit = Nothing
        End Try
    End Sub
    'Wrong code

    Private Sub C1FlexTransaction_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        With C1FlexTransaction
            If e.KeyCode = Keys.Delete Then
                For i As Integer = 1 To C1FlexTransaction.Rows.Count - 1

                Next
            End If
        End With
        'If Not e.KeyChar = ChrW(8) Then
        '    e.Handled = True
        'End If
    End Sub

    Private Sub C1FlexTransaction_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub

    Private Sub C1FlexTransaction_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            Dim _x As Integer = e.X
            '  Dim _y As Integer = e.Y
            If (miniwindow = 1) Then
                pnlcustomTask.Location = New Point(e.X, miniwindowy)
                miniwindow = 0
            End If

            With C1FlexTransaction
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    ''--------------Code modified by Anil on 20071206
                    Dim ht As HitTestInfo
                    ht = .HitTest(e.X, e.Y)
                    If ht.Row <= 0 Then
                        C1FlexTransaction.ContextMenu = Nothing
                        Exit Sub
                    End If
                    C1FlexTransaction.Row = ht.Row
                    .Col = ht.Column
                    ''---------------------------
                    ''''The IF statement is added by Anil on 20071201
                    If .Row >= 0 Then
                        .Select(.Row, .Col)
                        ' detect for specific columns and null value into respective row and columns.
                        If (.Col = COL_DUEDATE And .GetData(.Row, .Col) <> Nothing) Or (.Col = COL_EXPDATE And .GetData(.Row, .Col) <> Nothing) Or (.Col = COL_TRNDATE And .GetData(.Row, .Col) <> Nothing) Or (.Col = COL_DATEGIVEN And .GetData(.Row, .Col) <> Nothing) And .Row >= 0 Then                     'Or C1FlexTransaction.Cols("COL_EXPDATE") Then
                            C1FlexTransaction.ContextMenu = cntxNothing
                            cntxMenuNothing.Text = "Delete"
                            _blnChangesMade = True
                            ''''''******This code is Added by Anil on 01/10/2007 at 10:15 a.m.
                            ''''''this is to fill items in context menu on clicking the column of a grid
                        ElseIf .Col = COL_ROUTE Then
                            C1FlexTransaction.ContextMenu = cntxNothing
                            cntxMenuNothing.Text = "Add Route"
                            _blnChangesMade = True

                        ElseIf (.Col = COL_SITE) Then
                            C1FlexTransaction.ContextMenu = cntxNothing
                            cntxMenuNothing.Text = "Add Site"
                            _blnChangesMade = True

                        ElseIf (.Col = COL_MANUFACT) Then
                            C1FlexTransaction.ContextMenu = cntxNothing
                            cntxMenuNothing.Text = "Add Manufacturer"
                            _blnChangesMade = True
                        Else
                            C1FlexTransaction.ContextMenu = Nothing
                        End If
                    Else
                        C1FlexTransaction.ContextMenu = Nothing
                    End If
                Else
                    C1FlexTransaction.ContextMenu = Nothing
                End If
                ''''''''''********up to here the code is added.
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cntxMenuNothing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntxMenuNothing.Click
        Try
            If C1FlexTransaction.Row >= 0 Then
                C1FlexTransaction.SetData(C1FlexTransaction.Row, C1FlexTransaction.Col, Nothing)
                _blnChangesMade = True
                '''''''This code line is added by Anil 0n 01/10/2007 at 10:13 a.m. 
                ''''''to call the function on context menu click 
                AddContextMenu()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub C1FlexTransaction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Try
            With C1FlexTransaction
                If .Col = COL_DUEDATE Or .Col = COL_EXPDATE Or .Col = COL_TRNDATE Or .Col = COL_DATEGIVEN Then
                    If e.KeyCode = Keys.Delete Then
                        C1FlexTransaction.SetData(C1FlexTransaction.Row, C1FlexTransaction.Col, Nothing)
                        _blnChangesMade = True
                    End If
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub frmIm_Transaction_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070718
            UnLock_Transaction(TrnType.Immunization, _EditID, 0, Now)
            '' <><><> Unlock the Record <><><>


            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAud
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Close, "Immunization Transaction Closed", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916itTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Close, "Immunization Transaction Closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Function getUserId(ByVal LoginName As String) As Long
        Dim conn As New SqlClient.SqlConnection(GetConnectionString)
        'Dim oDataReader As SqlClient.SqlDataReader
        Dim cmdItemTrans As New SqlClient.SqlCommand
        Dim userid As Long = 0

        Try
            conn.Open()

            With cmdItemTrans
                .CommandType = CommandType.Text
                .Connection = conn
                .CommandText = "Select nUserID from user_mst where sLoginName = '" & LoginName.Replace("'", "''") & "'"
            End With

            userid = cmdItemTrans.ExecuteScalar

            Return userid

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()

        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRpt As ReportDocument
        Try
            oRpt = New ReportDocument

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            'Dim TableCounter

            With crConnectionInfo
                .ServerName = gstrSQLServerName

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                .DatabaseName = gstrDatabaseName

                '.UserID = "Your User ID"
                '.Password = "Your Password"
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            oRpt.Load(Application.StartupPath & "\Reports\PatientImmunizationSummary.rpt")
            CrTables = oRpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
            oRpt.SetParameterValue("PatientID", _PatientID.ToString)



            oRpt.PrintToPrinter(1, False, 0, 0)

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Immunization report printed.", gstrLoginName, gstrClientMachineName, _PatientID)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Immunization report printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Immunization report printed.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Error while printing immunization record.", gstrLoginName, gstrClientMachineName, _PatientID, , clsAudit.enmOutCome.Failure)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        End Try
    End Sub


    'Private Sub HistoryClick()
    '   
    'End Sub




    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_PrintSummary_32.Click
        Dim oRpt As ReportDocument
        Try
            'Code Added on 20091110
            'To fix issue:#5080:The gloEMR system should not print anything if there is no data
            Dim blnAllowPrint As Boolean
            If SaveImmunization(blnAllowPrint) = False Then
                Exit Sub
            End If

            If blnAllowPrint = False Then
                Exit Sub
            End If
            'End code Added on 20091107
            oRpt = New ReportDocument

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            'Dim TableCounter

            With crConnectionInfo
                '.ServerName = gstrSQLServerName
                .ServerName = Convert.ToString(appSettings("SQLServerName"))

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                '.DatabaseName = gstrDatabaseName
                .DatabaseName = Convert.ToString(appSettings("DatabaseName"))

                '.UserID = "Your User ID"
                '.Password = "Your Password"

                '.IntegratedSecurity = True

                If Convert.ToBoolean(appSettings("WindowAuthentication")) = False Then
                    .IntegratedSecurity = False
                    'Convert.ToBoolean(appSettings["WindowAuthentication"]);
                    .UserID = Convert.ToString(appSettings("SQLLoginName"))
                    .Password = Convert.ToString(appSettings("SQLPassword"))
                Else
                    .IntegratedSecurity = True
                End If
            End With

            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            oRpt.Load(Application.StartupPath & "\Reports\PatientImmunizationSummary.rpt")
            CrTables = oRpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'oRpt.SetParameterValue("PatientID", gnPatientID.ToString)
            oRpt.SetParameterValue("PatientID", _PatientID.ToString)
            'end modification


            '' solving sales force case- GLO2010-0005466
            ''oRpt.PrintToPrinter(1, False, 0, 0)

            Dim _DefaultPrinter As Boolean = False
            If appSettings("DefaultPrinter") IsNot Nothing Then
                If appSettings("DefaultPrinter") <> "" Then
                    _DefaultPrinter = Convert.ToBoolean(appSettings("DefaultPrinter"))
                Else
                    _DefaultPrinter = False
                End If
            Else
                _DefaultPrinter = False
            End If
            If oRpt IsNot Nothing Then
                'To print the report 
                If _DefaultPrinter = False Then
                    PrintDialog1 = New PrintDialog()
                    If PrintDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                        oRpt.PrintToPrinter(1, False, 0, 0)
                    End If
                Else
                    'PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = true;
                    oRpt.PrintToPrinter(1, False, 0, 0)
                End If
            End If
            '' End.

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Immunization report printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Immunization report printed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Error while printing immunization record.", gstrLoginName, gstrClientMachineName, gnPatientID, , clsAudit.enmOutCome.Failure)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        End Try
    End Sub

    Private Sub tblbtn_PrintDue_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_PrintDue_32.Click
        Dim oRpt As ReportDocument
        'Shubhangi
        Dim conn As New SqlConnection(GetConnectionString())
        Dim sqlcmd As SqlCommand
        Dim strsql As String
        Dim dtcount As New DataTable

        Try
            Dim blnAllowPrint As Boolean
            'Code Added by Mayuri:20091110
            'To fix issue:#5080:The gloEMR system should not print anything if there is no data
            If SaveImmunization(blnAllowPrint) = False Then
                Exit Sub
            End If

            'Shubhangi 20091203
            'Bug No: 4176. If patient having no immunization dues then no need to print report so take count of due immunization first

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'strsql = " Select  IM_Trn_Dtl.im_trn_Dategiven,  IM_Trn_Dtl.im_trn_Date, IM_Trn_Dtl.im_trn_duedate " _
            '   & " FROM  IM_Trn_Dtl INNER JOIN " _
            '   & "  IM_Trn_Mst ON  IM_Trn_Dtl.im_trn_mst_Id =  IM_Trn_Mst.im_trn_mst_Id " _
            '   & "Where  IM_Trn_Mst.im_trn_mst_nPatientID= " & gnPatientID & " And  IM_Trn_Dtl.im_trn_duedate IS Not NULL AND IM_Trn_Dtl.im_trn_Dategiven IS NULL"
            strsql = " Select  IM_Trn_Dtl.im_trn_Dategiven,  IM_Trn_Dtl.im_trn_Date, IM_Trn_Dtl.im_trn_duedate " _
               & " FROM  IM_Trn_Dtl INNER JOIN " _
               & "  IM_Trn_Mst ON  IM_Trn_Dtl.im_trn_mst_Id =  IM_Trn_Mst.im_trn_mst_Id " _
               & "Where  IM_Trn_Mst.im_trn_mst_nPatientID= " & _PatientID & " And  IM_Trn_Dtl.im_trn_duedate IS Not NULL AND IM_Trn_Dtl.im_trn_Dategiven IS NULL"
            'end modification
            sqlcmd = New SqlCommand(strsql, conn)

            Dim ad As SqlDataAdapter = New SqlDataAdapter(sqlcmd)
            ad.Fill(dtcount)

            If IsNothing(dtcount) = False Then
                If dtcount.Rows.Count = 0 Then
                    MessageBox.Show("Patient does not have any due immunization.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Else
                MessageBox.Show("Patient does not have any due Immunization.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            'End Shubhangi 

            If blnAllowPrint = False Then
                Exit Sub
            End If
            'End Code Added by Mayuri:20091110
            oRpt = New ReportDocument

            Dim crtableLogoninfos As New TableLogOnInfos
            Dim crtableLogoninfo As New TableLogOnInfo
            Dim crConnectionInfo As New ConnectionInfo
            Dim CrTables As Tables
            Dim CrTable As Table
            'Dim TableCounter

            With crConnectionInfo
                '.ServerName = gstrSQLServerName
                .ServerName = Convert.ToString(appSettings("SQLServerName"))

                'If you are connecting to Oracle there is no 
                'DatabaseName. Use an empty string. 
                'For example, .DatabaseName = "" 

                ' .DatabaseName = gstrDatabaseName
                .DatabaseName = Convert.ToString(appSettings("DatabaseName"))

                '.IntegratedSecurity = True
                If Convert.ToBoolean(appSettings("WindowAuthentication")) = False Then
                    .IntegratedSecurity = False
                    'Convert.ToBoolean(appSettings["WindowAuthentication"]);
                    .UserID = Convert.ToString(appSettings("SQLLoginName"))
                    .Password = Convert.ToString(appSettings("SQLPassword"))
                Else
                    .IntegratedSecurity = True
                End If

                '.UserID = "Your User ID"
                '.Password = "Your Password"
            End With


            'This code works for both user tables and stored 
            'procedures. Set the CrTables to the Tables collection 
            'of the report 

            'If rbtSummary.Checked = True Then
            '    oRpt.Load(Application.StartupPath & "\Reports\PatientImmunizationSummary.rpt")
            '    CrTables = oRpt.Database.Tables

            '    'Loop through each table in the report and apply the 
            '    'LogonInfo information 

            '    For Each CrTable In CrTables
            '        crtableLogoninfo = CrTable.LogOnInfo
            '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '        CrTable.ApplyLogOnInfo(crtableLogoninfo)

            '        'If your DatabaseName is changing at runtime, specify 
            '        'the table location. 
            '        'For example, when you are reporting off of a 
            '        'Northwind database on SQL server you 
            '        'should have the following line of code: 

            '        CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            '    Next
            '    oRpt.SetParameterValue("PatientID", gnPatientID.ToString)

            'Else
            ' For Displaying report of patients for which Imm is DUE

            'oRpt = New ReportDocument
            oRpt.Load(Application.StartupPath & "\Reports\PatientImmunizationDues.rpt")

            CrTables = oRpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'oRpt.SetParameterValue("PatientID", gnPatientID.ToString)
            oRpt.SetParameterValue("PatientID", _PatientID.ToString)
            'end modification

            ' End If

            ''*******************************
            ' oRpt.PrintToPrinter(1, False, 0, 0)

            Dim _DefaultPrinter As Boolean = False
            If appSettings("DefaultPrinter") IsNot Nothing Then
                If appSettings("DefaultPrinter") <> "" Then
                    _DefaultPrinter = Convert.ToBoolean(appSettings("DefaultPrinter"))
                Else
                    _DefaultPrinter = False
                End If
            Else
                _DefaultPrinter = False
            End If
            If oRpt IsNot Nothing Then
                'To print the report 
                If _DefaultPrinter = False Then
                    PrintDialog1 = New PrintDialog()
                    If PrintDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                        oRpt.PrintToPrinter(1, False, 0, 0)
                    End If
                Else
                    'PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = true;
                    oRpt.PrintToPrinter(1, False, 0, 0)
                End If
            End If
            ''*****************************

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Immunization report printed.", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Immunization report printed.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Immunization report printed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PHIExport, "Error while printing immunization record.", gstrLoginName, gstrClientMachineName, gnPatientID, , clsAudit.enmOutCome.Failure)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.Print, "Error while printing immunization record.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        End Try
    End Sub

    Private Sub tblbtn_Save_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Save_32.Click
        _isSaveClicked = True
        SaveIM_transaction()
    End Sub

    Private Function SaveImmunization(Optional ByRef AllowPrint As Boolean = False) As Boolean
        Dim _result As Boolean = False
        Dim _SaveTrnID As Long = 0
        Dim objImmunization As ClsImmunization

        Dim arrlst As New List(Of ClsImmunization)
        Dim result As Int32 = 0
        Dim oImmunizationTransactionLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine
        Dim oTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction
        Dim oIM As New gloStream.Immunization.Transaction

        ' check for mandatory fields of selected rows
        'With C1Flex_Transaction
        '    For i As Integer = 1 To C1Flex_Transaction.Rows.Count - 1
        '        ' If .GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
        '        If .GetData(i, COL_DATEGIVEN) = Nothing Then
        '            MessageBox.Show("Please enter the given date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '            .RowSel = i
        '            Exit Function
        '        End If
        '        ' End If
        '    Next
        'End With

        With oTransaction
            'Master
            .TransactionID = 0
            .PatientID = _PatientID


            ' assign values to the properties
            For i As Integer = 1 To C1Flex_Transaction.Rows.Count - 1
                ' If C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then 'Or C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                oImmunizationTransactionLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
                objImmunization = New ClsImmunization
                objImmunization.RecordType = "A" '"U" or "D"
                objImmunization.PatientID = strPatientCode
                'objImmunization.PersonCounty =

                'Dim Im_ParentNode As C1.Win.C1FlexGrid.Node
                'Dim ItemNode As C1.Win.C1FlexGrid.Node
                'ItemNode = C1FlexTransaction.Rows(i).Node
                ' Im_ParentNode = ItemNode.GetNode(NodeTypeEnum.Parent)
                objImmunization.VaccineName = C1Flex_Transaction.GetData(i, Col_ItemName2)

                objImmunization.ToBePOCForReminders = "Y"
                With oImmunizationTransactionLine
                    If C1Flex_Transaction.GetData(i, COL_DATEGIVEN) Is Nothing Or C1Flex_Transaction.GetData(i, COL_DATEGIVEN) = "" Then
                        .DateGiven = "12:00:00 AM"
                    Else
                        .DateGiven = C1Flex_Transaction.GetData(i, COL_DATEGIVEN)
                    End If

                    ''Sanjog-Added to pass the immunization time given
                    If C1Flex_Transaction.GetData(i, COL_TIMEGIVEN) Is Nothing Or C1Flex_Transaction.GetData(i, COL_TIMEGIVEN) = "" Then
                        .TimeGiven = "12:00:00 AM"
                    Else
                        .TimeGiven = C1Flex_Transaction.GetData(i, COL_TIMEGIVEN)
                    End If
                    '.TimeGiven = C1Flex_Transaction.GetData(i, COL_TIMEGIVEN)
                    ''Sanjog-Added to pass the immunization time given

                    If C1Flex_Transaction.GetData(i, COL_DOSE) Is Nothing Then
                        .Dose = ""
                    Else
                        .Dose = C1Flex_Transaction.GetData(i, COL_DOSE)
                    End If

                    ''Added By Shweta 20100915
                    If C1Flex_Transaction.GetData(i, COL_DOSEUNIT) Is Nothing Then
                        .DoseUnit = ""
                    Else
                        .DoseUnit = C1Flex_Transaction.GetData(i, COL_DOSEUNIT)
                    End If
                    ''''End -Added By Shweta 20100915

                    If C1Flex_Transaction.GetData(i, COL_EXPDATE) Is Nothing Or C1Flex_Transaction.GetData(i, COL_EXPDATE) = "" Then
                        .ExpiryDate = "12:00:00 AM"
                    Else
                        .ExpiryDate = C1Flex_Transaction.GetData(i, COL_EXPDATE)
                    End If

                    If C1Flex_Transaction.GetData(i, COL_DUEDATE) Is Nothing Or C1Flex_Transaction.GetData(i, COL_DUEDATE) = "" Then
                        .DueDate = "12:00:00 AM"
                    Else
                        .DueDate = C1Flex_Transaction.GetData(i, COL_DUEDATE)
                    End If



                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                    If C1Flex_Transaction.GetData(i, COL_REACTION) Is Nothing Then
                        .ReactionDT = "12:00:00 AM"
                    Else
                        If C1Flex_Transaction.GetData(i, COL_REACTIONDT) Is Nothing Or C1Flex_Transaction.GetData(i, COL_REACTIONDT) = "" Then
                            .ReactionDT = "12:00:00 AM"
                        Else

                            .ReactionDT = C1Flex_Transaction.GetData(i, COL_REACTIONDT)
                        End If
                    End If
                    If C1Flex_Transaction.GetData(i, COL_REACTION) Is Nothing Then
                        .Reaction = ""
                    Else
                        If C1Flex_Transaction.GetData(i, COL_REACTION).ToString().Trim() = "" Then
                            .Reaction = ""
                        Else
                            .Reaction = C1Flex_Transaction.GetData(i, COL_REACTION).ToString()
                        End If

                    End If
                    '''''''''' to get Reactions from Combolist
                    'Dim rg As C1.Win.C1FlexGrid.CellRange = C1Flex_Transaction.GetCellRange(i, COL_REACTION, i, COL_REACTION)
                    'Dim rgReaction As CellStyle = rg.Style()

                    'Dim tempRx As String = ""
                    'If Not rgReaction Is Nothing Then
                    '    If rgReaction.ComboList <> "" Then
                    '        tempRx = rgReaction.ComboList
                    '        tempRx = tempRx.Replace("|", ",")
                    '    End If
                    'End If
                    'If tempRx.Trim() = "," Then
                    '    tempRx = ""
                    'End If
                    '.Reaction = tempRx

                    ' C1Flex_Transaction.GetData(i, COL_REACTION)

                    ''If C1FlexTransaction.GetData(i, COL_REACTION) Is Nothing Then
                    ''    ''If tempRx.Trim <> "" Then
                    ''    ''    tempRx = tempRx + "," + C1FlexTransaction.GetData(i, COL_REACTION)
                    ''    ''Else
                    ''    ''    tempRx = C1FlexTransaction.GetData(i, COL_REACTION)
                    ''    ''End If
                    ''    .Reaction = tempRx
                    ''Else
                    ''    If Trim(C1FlexTransaction.GetData(i, COL_REACTION).ToString()) = "" Then
                    ''        .Reaction = tempRx
                    ''    Else
                    ''        .Reaction = C1FlexTransaction.GetData(i, COL_REACTION)
                    ''    End If
                    ''End If
                    ''tempRx = tempRx.Trim()
                    ''If tempRx.EndsWith(",") Then
                    ''    tempRx = tempRx.Substring(0, tempRx.Length - 1)
                    ''End If


                    ''If C1FlexTransaction.GetData(i, COL_REACTION) Is Nothing Then
                    ''    .Reaction = tempRx
                    ''Else
                    ''    If tempRx.Trim() <> "" Then
                    ''        If C1FlexTransaction.GetData(i, COL_REACTION).ToString.Trim() <> "" Then
                    ''            tempRx = tempRx + "," + C1FlexTransaction.GetData(i, COL_REACTION)
                    ''        Else
                    ''            tempRx = tempRx
                    ''        End If
                    ''    End If
                    ''        .Reaction = C1FlexTransaction.GetData(i, COL_REACTION)
                    ''End If


                    ' ''.SetData(.Row, COL_REACTIONBTN, Nothing)
                    '.SetData(.Row, COL_REACTIONDT, Nothing)
                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110





                    .ItemCounterID = C1Flex_Transaction.GetData(i, COL_ITEMCOUNTERID)

                    'this will save the item counter id from the oImmunizationTransactionLine object to the objImmunization object becaz we need to show the item counter id when we show the Im_Validation message.
                    objImmunization.ItemCounterId = .ItemCounterID

                    .ItemID = C1Flex_Transaction.GetData(i, COL_ITEMID)
                    '.ItemName = C1Flex_Transaction.GetData(Im_ParentNode.Row.Index, Col_ItemName2) & ""
                    .ItemName = C1Flex_Transaction.GetData(i, Col_ItemName2) & ""

                    .LotNumber = C1Flex_Transaction.GetData(i, COL_LOTNUMBER) & ""


                    .Manufacturer = C1Flex_Transaction.GetData(i, COL_MANUFACT) & ""

                    .Notes = C1Flex_Transaction.GetData(i, COL_NOTES) & ""
                    'objImmunization.ReasonForNonAdmin = .Notes

                    .Route = C1Flex_Transaction.GetData(i, COL_ROUTE) & ""


                    '' modified on 20070327
                    'If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
                    '    .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
                    'Else
                    '    .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
                    'End If

                    ' if no data in field then todays default value should be todays date.
                    'If C1Flex_Transaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    If C1Flex_Transaction.GetData(i, COL_TRNDATE) Is Nothing Or C1Flex_Transaction.GetData(i, COL_TRNDATE) = "" Then
                        '.TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
                        .TransactionDate = "12:00:00 AM"
                    Else
                        .TransactionDate = C1Flex_Transaction(i, COL_TRNDATE)
                    End If
                    'Else
                    'If C1Flex_Transaction.GetData(i, COL_TRNDATE) Is Nothing Then
                    '    .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
                    'Else
                    '    .TransactionDate = C1Flex_Transaction(i, COL_TRNDATE)
                    'End If
                    ' End If
                    '' modified on 20070327

                    .UserID = C1Flex_Transaction(i, COL_USERID)
                    .VisitID = C1Flex_Transaction(i, COL_VISITID)

                    '' modification on 20070425 for CCHIT 2007
                    .Site = C1Flex_Transaction(i, COL_SITE) & ""

                    'Sanjog - Added on 2011 March 19 to add the Admin Status
                    '"0" for Administered and "1" for Reported
                    If C1Flex_Transaction(i, COL_AdminStatus) = "Administered" Then
                        .AdminStatus = "0" ''''If checked then means administered
                    Else
                        .AdminStatus = "1" ''''It means reported checked then means Reported
                    End If
                    'Sanjog - Added on 2011 March 19 to add the Admin Status

                    .UserName = C1Flex_Transaction(i, Col_USERNAME) & ""
                    ''
                    objImmunization.VaccByOtherProvider = gnLoginProviderID
                    '' Sarika 20080604
                    If (C1Flex_Transaction.GetCellCheck(i, COL_ISREMINDER) = CheckEnum.Checked) Then
                        .IsReminder = True
                        objImmunization.ReminderRecall = "Y"
                    Else
                        .IsReminder = False
                        objImmunization.ReminderRecall = "N"
                    End If

                    .EligibilityCode = C1Flex_Transaction.GetData(i, COL_VACCINEELIGIBILITYCODE) & ""

                    objImmunization.VaccineEligibilityDesc = .EligibilityCode
                    objImmunization.VaccSiteCode = .Site
                    objImmunization.VaccRouteCode = .Route
                    objImmunization.Manufacturer = .Manufacturer
                    objImmunization.DateofEncounter = .DateGiven
                    objImmunization.DoseAmount = .Dose
                    objImmunization.LotNumber = .LotNumber
                    ''Added By Shweta 20100915 for MU
                    objImmunization.DoseUnit = .DoseUnit
                    ''END-Added By Shweta 20100915 for MU

                    If Not IsNothing(C1Flex_Transaction.GetData(i, COL_REASONFORNONADMIN)) Then
                        .ReasonForNonAdmin = C1Flex_Transaction.GetData(i, COL_REASONFORNONADMIN)
                    Else
                        .ReasonForNonAdmin = ""
                    End If


                    'SAve the CPT Codes
                    .CPTCode = C1Flex_Transaction.GetData(i, COL_CPTCODES) & ""
                    '.VaccineCode = C1Flex_Transaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode) & ""
                    .VaccineCode = C1Flex_Transaction.GetData(i, Col_VaccineCode) & ""

                    objImmunization.CPTCode = .CPTCode
                    ' objImmunization.VaccineCode = C1Flex_Transaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode)
                    objImmunization.VaccineCode = C1Flex_Transaction.GetData(i, Col_VaccineCode)
                    'Code commented & added by kanchan on 20100914 for immunization MU
                    'objImmunization.VaccineCode = "20"
                    If objImmunization.VaccineCode = "" Then
                        objImmunization.VaccineCode = "20"
                    End If

                    'Code Start-Added by kanchan on 20100904 for snomed implementation in immunization
                    .ConceptID = C1Flex_Transaction.GetData(i, COL_ConceptID)
                    .DescriptionID = C1Flex_Transaction.GetData(i, COL_DescriptionID)
                    .SnoMedID = C1Flex_Transaction.GetData(i, COL_SnomedID)
                    'Code End-Added by kanchan on 20100904 for snomed implementation in immunization

                    ''Enter the guardian information
                    'if all guardian info is present then enter the Guardian information,
                    'else check the mother information, if all information present enter mother information,
                    'else check fathers information, if present enter fathers information in the objImmunization object.
                    'if any of the guardians/ mothers/ fathers info is absent then in any case the file is going to be rejected.
                    FillGuardianInformation(objImmunization)
                End With
                .TransactionLines.Add(oImmunizationTransactionLine)
                arrlst.Add(objImmunization)
                objImmunization = Nothing
                oImmunizationTransactionLine = Nothing



                'Else 'if only due date is given and the item is not checked then that item should be saved.


                '    If Not IsNothing(C1FlexTransaction.GetData(i, COL_DUEDATE)) Then
                '        If C1FlexTransaction.GetData(i, COL_DUEDATE) <> "12:00:00 AM" Then



                '            oImmunizationTransactionLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
                '            'objImmunization = New ClsImmunization
                '            'objImmunization.RecordType = "A" '"U" or "D"
                '            'objImmunization.PatientID = gstrPatientCode

                '            Dim Im_ParentNode As C1.Win.C1FlexGrid.Node
                '            Dim ItemNode As C1.Win.C1FlexGrid.Node
                '            ItemNode = C1FlexTransaction.Rows(i).Node
                '            Im_ParentNode = ItemNode.GetNode(NodeTypeEnum.Parent)
                '            'objImmunization.VaccineName = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, COL_ITEMNAME)

                '            'objImmunization.ToBePOCForReminders = "Y"
                '            With oImmunizationTransactionLine
                '                If C1FlexTransaction.GetData(i, COL_DATEGIVEN) Is Nothing Then
                '                    .DateGiven = "12:00:00 AM"
                '                Else
                '                    .DateGiven = C1FlexTransaction.GetData(i, COL_DATEGIVEN)
                '                End If


                '                If Not IsNothing(C1FlexTransaction.GetData(i, COL_TIMEGIVEN)) Then
                '                    .TimeGiven = C1FlexTransaction.GetData(i, COL_TIMEGIVEN)
                '                End If

                '                If C1FlexTransaction.GetData(i, COL_DOSE) Is Nothing Then
                '                    .Dose = ""
                '                Else
                '                    .Dose = C1FlexTransaction.GetData(i, COL_DOSE)
                '                End If


                '                If C1FlexTransaction.GetData(i, COL_EXPDATE) Is Nothing Then
                '                    .ExpiryDate = "12:00:00 AM"
                '                Else
                '                    .ExpiryDate = C1FlexTransaction.GetData(i, COL_EXPDATE)
                '                End If


                '                .DueDate = C1FlexTransaction.GetData(i, COL_DUEDATE)



                '                .ItemCounterID = C1FlexTransaction.GetData(i, COL_ITEMCOUNTERID)

                '                'this will save the item counter id from the oImmunizationTransactionLine object to the objImmunization object becaz we need to show the item counter id when we show the Im_Validation message.
                '                'objImmunization.ItemCounterId = .ItemCounterID

                '                .ItemID = C1FlexTransaction.GetData(i, COL_ITEMID)
                '                .ItemName = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, COL_ITEMNAME) & ""
                '                .LotNumber = C1FlexTransaction.GetData(i, COL_LOTNUMBER) & ""


                '                .Manufacturer = C1FlexTransaction.GetData(i, COL_MANUFACT) & ""

                '                .Notes = C1FlexTransaction.GetData(i, COL_NOTES) & ""


                '                .Route = C1FlexTransaction.GetData(i, COL_ROUTE) & ""


                '                ' if no data in field then todays default value should be todays date.
                '                If C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                '                    If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
                '                        '.TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
                '                        .TransactionDate = DateTime.Today()
                '                    Else
                '                        .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
                '                    End If
                '                Else
                '                    If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
                '                        .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
                '                    Else
                '                        .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
                '                    End If
                '                End If
                '                '' modified on 20070327

                '                .UserID = C1FlexTransaction(i, COL_USERID)
                '                .VisitID = C1FlexTransaction(i, COL_VISITID)

                '                ' modification on 20070425 for CCHIT 2007
                '                .Site = C1FlexTransaction(i, COL_SITE) & ""



                '                .UserName = C1FlexTransaction(i, Col_USERNAME) & ""
                '                ''
                '                'objImmunization.VaccByOtherProvider = gnDoctorID
                '                '' Sarika 20080604
                '                If (C1FlexTransaction.GetCellCheck(i, COL_ISREMINDER) = CheckEnum.Checked) Then
                '                    .IsReminder = True
                '                    'objImmunization.ReminderRecall = "Y"
                '                Else
                '                    .IsReminder = False
                '                    'objImmunization.ReminderRecall = "N"
                '                End If

                '                .EligibilityCode = C1FlexTransaction.GetData(i, COL_VACCINEELIGIBILITYCODE) & ""

                '                'objImmunization.VaccineEligibilityDesc = .EligibilityCode
                '                'objImmunization.VaccSiteCode = .Site
                '                'objImmunization.VaccRouteCode = .Route
                '                'objImmunization.Manufacturer = .Manufacturer
                '                'objImmunization.DateofEncounter = .DateGiven
                '                'objImmunization.DoseAmount = .Dose
                '                'objImmunization.LotNumber = .LotNumber

                '                If Not IsNothing(C1FlexTransaction.GetData(i, COL_REASONFORNONADMIN)) Then
                '                    .ReasonForNonAdmin = C1FlexTransaction.GetData(i, COL_REASONFORNONADMIN)
                '                Else
                '                    .ReasonForNonAdmin = ""
                '                End If


                '                'SAve the CPT Codes
                '                .CPTCode = C1FlexTransaction.GetData(i, COL_CPTCODES) & ""
                '                .VaccineCode = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode) & ""

                '                'objImmunization.CPTCode = .CPTCode
                '                'objImmunization.VaccineCode = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode)
                '                'objImmunization.VaccineCode = "20"
                '            End With
                '            .TransactionLines.Add(oImmunizationTransactionLine)
                '            'arrlst.Add(objImmunization)
                '            'objImmunization = Nothing
                '            oImmunizationTransactionLine = Nothing
                '        End If
                '    End If

                'End If
            Next

        End With
        'if the generateImmunization setting is true then generate the report.
        'If gblnGenerateMicReport = True Then
        '    If arrlst.Count > 0 Then
        '        If System.IO.Directory.Exists(gstrMCIRReportPath) Then
        '            objImmunizationReport = New ClsImmunizationReport()
        '            result = objImmunizationReport.GenerateImuunizationReport(arrlst)
        '            'User selected not to continue,so the Immunization will not be saved and 
        '            'form will not be unloaded
        '            If result = 2 Then
        '                Exit Function

        '            End If
        '        Else
        '            MessageBox.Show("Kindly Set the MCIR REPORT PATH from gloEMRAdmin to Generate Immunization Report", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        End If


        '    End If
        'End If
        _SaveTrnID = oIM.IsImmunizationExists(_PatientID)

        'set add/edit flag
        'If _SaveFlag = True Then
        If _SaveTrnID <= 0 Then
            'Code Start-Added by kanchan on 20100904 for snomed implementation in immunization
            '_result = oIM.Add(oTransaction)
            _result = oIM.Add_SnoMed(oTransaction)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordAdded, "Immunization Record Added.", gstrLoginName, gstrClientMachineName, _PatientID)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Added.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Added.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Else
            'Code Start-Added by kanchan on 20100904 for snomed implementation in immunization
            '_result = oIM.Modify(_SaveTrnID, oTransaction)
            _result = oIM.Modify_SnoMed(_SaveTrnID, oTransaction)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordModified, "Immunization Record Modified.", gstrLoginName, gstrClientMachineName, _PatientID)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, "Immunization Record Modified.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, "Immunization Record Modified.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        End If

        'If _result = True Then
        '    ProgressBar1.Visible = True
        '    ProgressBar1.Minimum = 1
        '    ProgressBar1.Maximum = 2000
        '    For i As Integer = 1 To 2000
        '        ProgressBar1.Value = i
        '    Next
        '    ProgressBar1.Visible = False
        'End If
        _blnChangesMade = False

        'Code Added on 20091110
        'To fix issue:#5080:The gloEMR system should not print anything if there is no data
        If IsNothing(oTransaction) = False Then
            If oTransaction.TransactionLines.Count > 0 Then
                AllowPrint = True
            End If
        End If
        'End code Added on 20091110

        Return True
    End Function



    '''''''''

    ''Private Function SaveImmunizationOld(Optional ByRef AllowPrint As Boolean = False) As Boolean
    ''    Dim _result As Boolean = False
    ''    Dim _SaveTrnID As Long = 0
    ''    Dim objImmunization As ClsImmunization

    ''    Dim arrlst As New List(Of ClsImmunization)
    ''    Dim result As Int32 = 0
    ''    Dim oImmunizationTransactionLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine
    ''    Dim oTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction
    ''    Dim oIM As New gloStream.Immunization.Transaction

    ''    ' check for mandatory fields of selected rows
    ''    With C1FlexTransaction
    ''        For i As Integer = 1 To C1FlexTransaction.Rows.Count - 1
    ''            If .GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    ''                If .GetData(i, COL_DATEGIVEN) = Nothing Then
    ''                    MessageBox.Show("Please enter the given date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    ''                    .RowSel = i
    ''                    Exit Function
    ''                End If
    ''            End If
    ''        Next
    ''    End With

    ''    With oTransaction
    ''        'Master
    ''        .TransactionID = 0
    ''        .PatientID = _PatientID


    ''        ' assign values to the properties
    ''        For i As Integer = 1 To C1FlexTransaction.Rows.Count - 1
    ''            If C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then 'Or C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
    ''                oImmunizationTransactionLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
    ''                objImmunization = New ClsImmunization
    ''                objImmunization.RecordType = "A" '"U" or "D"
    ''                objImmunization.PatientID = gstrPatientCode
    ''                'objImmunization.PersonCounty =

    ''                Dim Im_ParentNode As C1.Win.C1FlexGrid.Node
    ''                Dim ItemNode As C1.Win.C1FlexGrid.Node
    ''                ItemNode = C1FlexTransaction.Rows(i).Node
    ''                Im_ParentNode = ItemNode.GetNode(NodeTypeEnum.Parent)
    ''                objImmunization.VaccineName = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, COL_ITEMNAME)

    ''                objImmunization.ToBePOCForReminders = "Y"
    ''                With oImmunizationTransactionLine
    ''                    If C1FlexTransaction.GetData(i, COL_DATEGIVEN) Is Nothing Then
    ''                        .DateGiven = "12:00:00 AM"
    ''                    Else
    ''                        .DateGiven = C1FlexTransaction.GetData(i, COL_DATEGIVEN)
    ''                    End If


    ''                    .TimeGiven = C1FlexTransaction.GetData(i, COL_TIMEGIVEN)
    ''                    If C1FlexTransaction.GetData(i, COL_DOSE) Is Nothing Then
    ''                        .Dose = ""
    ''                    Else
    ''                        .Dose = C1FlexTransaction.GetData(i, COL_DOSE)
    ''                    End If
    ''                    ''Added By Shweta 20100915 for MU
    ''                    If C1FlexTransaction.GetData(i, COL_DOSEUNIT) Is Nothing Then
    ''                        .DoseUnit = ""
    ''                    Else
    ''                        .DoseUnit = C1FlexTransaction.GetData(i, COL_DOSEUNIT)
    ''                    End If
    ''                    ''END-Added By Shweta 20100915 for MU

    ''                    If C1FlexTransaction.GetData(i, COL_EXPDATE) Is Nothing Then
    ''                        .ExpiryDate = "12:00:00 AM"
    ''                    Else
    ''                        .ExpiryDate = C1FlexTransaction.GetData(i, COL_EXPDATE)
    ''                    End If

    ''                    If C1FlexTransaction.GetData(i, COL_DUEDATE) Is Nothing Then
    ''                        .DueDate = "12:00:00 AM"
    ''                    Else
    ''                        .DueDate = C1FlexTransaction.GetData(i, COL_DUEDATE)
    ''                    End If



    ''                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
    ''                    If C1FlexTransaction.GetData(i, COL_REACTION) Is Nothing Then
    ''                        .ReactionDT = "12:00:00 AM"
    ''                    Else
    ''                        .ReactionDT = C1FlexTransaction.GetData(i, COL_REACTIONDT)
    ''                    End If
    ''                    '''''''''' to get Reactions from Combolist
    ''                    Dim rg As C1.Win.C1FlexGrid.CellRange = C1FlexTransaction.GetCellRange(i, COL_REACTION, i, COL_REACTION)
    ''                    Dim rgReaction As CellStyle = rg.Style()

    ''                    Dim tempRx As String = ""
    ''                    If Not rgReaction Is Nothing Then
    ''                        If rgReaction.ComboList <> "" Then
    ''                            tempRx = rgReaction.ComboList
    ''                            tempRx = tempRx.Replace("|", ",")
    ''                        End If
    ''                    End If
    ''                    If tempRx.Trim() = "," Then
    ''                        tempRx = ""
    ''                    End If
    ''                    .Reaction = tempRx
    ''                    ''If C1FlexTransaction.GetData(i, COL_REACTION) Is Nothing Then
    ''                    ''    ''If tempRx.Trim <> "" Then
    ''                    ''    ''    tempRx = tempRx + "," + C1FlexTransaction.GetData(i, COL_REACTION)
    ''                    ''    ''Else
    ''                    ''    ''    tempRx = C1FlexTransaction.GetData(i, COL_REACTION)
    ''                    ''    ''End If
    ''                    ''    .Reaction = tempRx
    ''                    ''Else
    ''                    ''    If Trim(C1FlexTransaction.GetData(i, COL_REACTION).ToString()) = "" Then
    ''                    ''        .Reaction = tempRx
    ''                    ''    Else
    ''                    ''        .Reaction = C1FlexTransaction.GetData(i, COL_REACTION)
    ''                    ''    End If
    ''                    ''End If
    ''                    ''tempRx = tempRx.Trim()
    ''                    ''If tempRx.EndsWith(",") Then
    ''                    ''    tempRx = tempRx.Substring(0, tempRx.Length - 1)
    ''                    ''End If


    ''                    ''If C1FlexTransaction.GetData(i, COL_REACTION) Is Nothing Then
    ''                    ''    .Reaction = tempRx
    ''                    ''Else
    ''                    ''    If tempRx.Trim() <> "" Then
    ''                    ''        If C1FlexTransaction.GetData(i, COL_REACTION).ToString.Trim() <> "" Then
    ''                    ''            tempRx = tempRx + "," + C1FlexTransaction.GetData(i, COL_REACTION)
    ''                    ''        Else
    ''                    ''            tempRx = tempRx
    ''                    ''        End If
    ''                    ''    End If
    ''                    ''        .Reaction = C1FlexTransaction.GetData(i, COL_REACTION)
    ''                    ''End If


    ''                    ' ''.SetData(.Row, COL_REACTIONBTN, Nothing)
    ''                    '.SetData(.Row, COL_REACTIONDT, Nothing)
    ''                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110





    ''                    .ItemCounterID = C1FlexTransaction.GetData(i, COL_ITEMCOUNTERID)

    ''                    'this will save the item counter id from the oImmunizationTransactionLine object to the objImmunization object becaz we need to show the item counter id when we show the Im_Validation message.
    ''                    objImmunization.ItemCounterId = .ItemCounterID

    ''                    .ItemID = C1FlexTransaction.GetData(i, COL_ITEMID)
    ''                    .ItemName = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, COL_ITEMNAME) & ""
    ''                    .LotNumber = C1FlexTransaction.GetData(i, COL_LOTNUMBER) & ""


    ''                    .Manufacturer = C1FlexTransaction.GetData(i, COL_MANUFACT) & ""

    ''                    .Notes = C1FlexTransaction.GetData(i, COL_NOTES) & ""
    ''                    'objImmunization.ReasonForNonAdmin = .Notes

    ''                    .Route = C1FlexTransaction.GetData(i, COL_ROUTE) & ""


    ''                    '' modified on 20070327
    ''                    'If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    ''                    '    .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    ''                    'Else
    ''                    '    .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    ''                    'End If

    ''                    ' if no data in field then todays default value should be todays date.
    ''                    If C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    ''                        If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    ''                            '.TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    ''                            .TransactionDate = DateTime.Today()
    ''                        Else
    ''                            .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    ''                        End If
    ''                    Else
    ''                        If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    ''                            .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    ''                        Else
    ''                            .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    ''                        End If
    ''                    End If
    ''                    '' modified on 20070327

    ''                    .UserID = C1FlexTransaction(i, COL_USERID)
    ''                    .VisitID = C1FlexTransaction(i, COL_VISITID)

    ''                    '' modification on 20070425 for CCHIT 2007
    ''                    .Site = C1FlexTransaction(i, COL_SITE) & ""



    ''                    .UserName = C1FlexTransaction(i, Col_USERNAME) & ""
    ''                    ''
    ''                    objImmunization.VaccByOtherProvider = gnLoginProviderID
    ''                    '' Sarika 20080604
    ''                    If (C1FlexTransaction.GetCellCheck(i, COL_ISREMINDER) = CheckEnum.Checked) Then
    ''                        .IsReminder = True
    ''                        objImmunization.ReminderRecall = "Y"
    ''                    Else
    ''                        .IsReminder = False
    ''                        objImmunization.ReminderRecall = "N"
    ''                    End If

    ''                    .EligibilityCode = C1FlexTransaction.GetData(i, COL_VACCINEELIGIBILITYCODE) & ""

    ''                    objImmunization.VaccineEligibilityDesc = .EligibilityCode
    ''                    objImmunization.VaccSiteCode = .Site
    ''                    objImmunization.VaccRouteCode = .Route
    ''                    objImmunization.Manufacturer = .Manufacturer
    ''                    objImmunization.DateofEncounter = .DateGiven
    ''                    objImmunization.DoseAmount = .Dose
    ''                    objImmunization.LotNumber = .LotNumber

    ''                    If Not IsNothing(C1FlexTransaction.GetData(i, COL_REASONFORNONADMIN)) Then
    ''                        .ReasonForNonAdmin = C1FlexTransaction.GetData(i, COL_REASONFORNONADMIN)
    ''                    Else
    ''                        .ReasonForNonAdmin = ""
    ''                    End If


    ''                    'SAve the CPT Codes
    ''                    .CPTCode = C1FlexTransaction.GetData(i, COL_CPTCODES) & ""
    ''                    .VaccineCode = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode) & ""

    ''                    objImmunization.CPTCode = .CPTCode
    ''                    objImmunization.VaccineCode = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode)
    ''                    objImmunization.VaccineCode = "20"


    ''                    ''Enter the guardian information
    ''                    'if all guardian info is present then enter the Guardian information,
    ''                    'else check the mother information, if all information present enter mother information,
    ''                    'else check fathers information, if present enter fathers information in the objImmunization object.
    ''                    'if any of the guardians/ mothers/ fathers info is absent then in any case the file is going to be rejected.
    ''                    FillGuardianInformation(objImmunization)
    ''                End With
    ''                .TransactionLines.Add(oImmunizationTransactionLine)
    ''                arrlst.Add(objImmunization)
    ''                objImmunization = Nothing
    ''                oImmunizationTransactionLine = Nothing



    ''            Else 'if only due date is given and the item is not checked then that item should be saved.


    ''                If Not IsNothing(C1FlexTransaction.GetData(i, COL_DUEDATE)) Then
    ''                    If C1FlexTransaction.GetData(i, COL_DUEDATE) <> "12:00:00 AM" Then



    ''                        oImmunizationTransactionLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
    ''                        'objImmunization = New ClsImmunization
    ''                        'objImmunization.RecordType = "A" '"U" or "D"
    ''                        'objImmunization.PatientID = gstrPatientCode

    ''                        Dim Im_ParentNode As C1.Win.C1FlexGrid.Node
    ''                        Dim ItemNode As C1.Win.C1FlexGrid.Node
    ''                        ItemNode = C1FlexTransaction.Rows(i).Node
    ''                        Im_ParentNode = ItemNode.GetNode(NodeTypeEnum.Parent)
    ''                        'objImmunization.VaccineName = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, COL_ITEMNAME)

    ''                        'objImmunization.ToBePOCForReminders = "Y"
    ''                        With oImmunizationTransactionLine
    ''                            If C1FlexTransaction.GetData(i, COL_DATEGIVEN) Is Nothing Then
    ''                                .DateGiven = "12:00:00 AM"
    ''                            Else
    ''                                .DateGiven = C1FlexTransaction.GetData(i, COL_DATEGIVEN)
    ''                            End If


    ''                            If Not IsNothing(C1FlexTransaction.GetData(i, COL_TIMEGIVEN)) Then
    ''                                .TimeGiven = C1FlexTransaction.GetData(i, COL_TIMEGIVEN)
    ''                            End If


    ''                            If C1FlexTransaction.GetData(i, COL_DOSE) Is Nothing Then
    ''                                .Dose = ""
    ''                            Else
    ''                                .Dose = C1FlexTransaction.GetData(i, COL_DOSE)
    ''                            End If

    ''                            ''Added By Shweta 20100915 for MU
    ''                            If C1FlexTransaction.GetData(i, COL_DOSEUNIT) Is Nothing Then
    ''                                .DoseUnit = ""
    ''                            Else
    ''                                .DoseUnit = C1FlexTransaction.GetData(i, COL_DOSEUNIT)
    ''                            End If
    ''                            ''END-Added By Shweta 20100915 for MU

    ''                            If C1FlexTransaction.GetData(i, COL_EXPDATE) Is Nothing Then
    ''                                .ExpiryDate = "12:00:00 AM"
    ''                            Else
    ''                                .ExpiryDate = C1FlexTransaction.GetData(i, COL_EXPDATE)
    ''                            End If


    ''                            .DueDate = C1FlexTransaction.GetData(i, COL_DUEDATE)



    ''                            .ItemCounterID = C1FlexTransaction.GetData(i, COL_ITEMCOUNTERID)

    ''                            'this will save the item counter id from the oImmunizationTransactionLine object to the objImmunization object becaz we need to show the item counter id when we show the Im_Validation message.
    ''                            'objImmunization.ItemCounterId = .ItemCounterID

    ''                            .ItemID = C1FlexTransaction.GetData(i, COL_ITEMID)
    ''                            .ItemName = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, COL_ITEMNAME) & ""
    ''                            .LotNumber = C1FlexTransaction.GetData(i, COL_LOTNUMBER) & ""


    ''                            .Manufacturer = C1FlexTransaction.GetData(i, COL_MANUFACT) & ""

    ''                            .Notes = C1FlexTransaction.GetData(i, COL_NOTES) & ""


    ''                            .Route = C1FlexTransaction.GetData(i, COL_ROUTE) & ""


    ''                            ' if no data in field then todays default value should be todays date.
    ''                            If C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
    ''                                If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    ''                                    '.TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    ''                                    .TransactionDate = DateTime.Today()
    ''                                Else
    ''                                    .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    ''                                End If
    ''                            Else
    ''                                If C1FlexTransaction.GetData(i, COL_TRNDATE) Is Nothing Then
    ''                                    .TransactionDate = "12:00:00 AM" 'dtpTransaction.Value
    ''                                Else
    ''                                    .TransactionDate = C1FlexTransaction(i, COL_TRNDATE)
    ''                                End If
    ''                            End If
    ''                            '' modified on 20070327

    ''                            .UserID = C1FlexTransaction(i, COL_USERID)
    ''                            .VisitID = C1FlexTransaction(i, COL_VISITID)

    ''                            ' modification on 20070425 for CCHIT 2007
    ''                            .Site = C1FlexTransaction(i, COL_SITE) & ""



    ''                            .UserName = C1FlexTransaction(i, Col_USERNAME) & ""
    ''                            ''
    ''                            'objImmunization.VaccByOtherProvider = gnDoctorID
    ''                            '' Sarika 20080604
    ''                            If (C1FlexTransaction.GetCellCheck(i, COL_ISREMINDER) = CheckEnum.Checked) Then
    ''                                .IsReminder = True
    ''                                'objImmunization.ReminderRecall = "Y"
    ''                            Else
    ''                                .IsReminder = False
    ''                                'objImmunization.ReminderRecall = "N"
    ''                            End If

    ''                            .EligibilityCode = C1FlexTransaction.GetData(i, COL_VACCINEELIGIBILITYCODE) & ""

    ''                            'objImmunization.VaccineEligibilityDesc = .EligibilityCode
    ''                            'objImmunization.VaccSiteCode = .Site
    ''                            'objImmunization.VaccRouteCode = .Route
    ''                            'objImmunization.Manufacturer = .Manufacturer
    ''                            'objImmunization.DateofEncounter = .DateGiven
    ''                            'objImmunization.DoseAmount = .Dose
    ''                            'objImmunization.LotNumber = .LotNumber

    ''                            If Not IsNothing(C1FlexTransaction.GetData(i, COL_REASONFORNONADMIN)) Then
    ''                                .ReasonForNonAdmin = C1FlexTransaction.GetData(i, COL_REASONFORNONADMIN)
    ''                            Else
    ''                                .ReasonForNonAdmin = ""
    ''                            End If


    ''                            'SAve the CPT Codes
    ''                            .CPTCode = C1FlexTransaction.GetData(i, COL_CPTCODES) & ""
    ''                            .VaccineCode = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode) & ""

    ''                            'objImmunization.CPTCode = .CPTCode
    ''                            'objImmunization.VaccineCode = C1FlexTransaction.GetData(Im_ParentNode.Row.Index, Col_VaccineCode)
    ''                            'objImmunization.VaccineCode = "20"
    ''                        End With
    ''                        .TransactionLines.Add(oImmunizationTransactionLine)
    ''                        'arrlst.Add(objImmunization)
    ''                        'objImmunization = Nothing
    ''                        oImmunizationTransactionLine = Nothing
    ''                    End If
    ''                End If

    ''            End If
    ''        Next

    ''    End With
    ''    'if the generateImmunization setting is true then generate the report.
    ''    If gblnGenerateMicReport = True Then
    ''        If arrlst.Count > 0 Then
    ''            If System.IO.Directory.Exists(gstrMCIRReportPath) Then
    ''                objImmunizationReport = New ClsImmunizationReport()
    ''                result = objImmunizationReport.GenerateImuunizationReport(arrlst)
    ''                'User selected not to continue,so the Immunization will not be saved and 
    ''                'form will not be unloaded
    ''                If result = 2 Then
    ''                    Exit Function

    ''                End If
    ''            Else
    ''                MessageBox.Show("Kindly Set the MCIR REPORT PATH from gloEMRAdmin to Generate Immunization Report", "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Information)
    ''            End If


    ''        End If
    ''    End If
    ''    _SaveTrnID = oIM.IsImmunizationExists(_PatientID)

    ''    'set add/edit flag
    ''    'If _SaveFlag = True Then
    ''    If _SaveTrnID <= 0 Then
    ''        _result = oIM.Add(oTransaction)

    ''        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordAdded, "Immunization Record Added.", gstrLoginName, gstrClientMachineName, _PatientID)
    ''        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Added.", gloAuditTrail.ActivityOutCome.Success)
    ''        ''Added Rahul P on 20100916
    ''        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Added.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
    ''        ''
    ''    Else
    ''        '_result = oIM.Modify(_SaveTrnID, oTransaction)
    ''        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordModified, "Immunization Record Modified.", gstrLoginName, gstrClientMachineName, _PatientID)
    ''        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, "Immunization Record Modified.", gloAuditTrail.ActivityOutCome.Success)
    ''        ''Added Rahul P on 20100916
    ''        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Modify, "Immunization Record Modified.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
    ''        ''

    ''    End If

    ''    'If _result = True Then
    ''    '    ProgressBar1.Visible = True
    ''    '    ProgressBar1.Minimum = 1
    ''    '    ProgressBar1.Maximum = 2000
    ''    '    For i As Integer = 1 To 2000
    ''    '        ProgressBar1.Value = i
    ''    '    Next
    ''    '    ProgressBar1.Visible = False
    ''    'End If
    ''    _blnChangesMade = False

    ''    'Code Added on 20091110
    ''    'To fix issue:#5080:The gloEMR system should not print anything if there is no data
    ''    If IsNothing(oTransaction) = False Then
    ''        If oTransaction.TransactionLines.Count > 0 Then
    ''            AllowPrint = True
    ''        End If
    ''    End If
    ''    'End code Added on 20091110

    ''    Return True
    ''End Function


    Private Sub GetPatientGuardianDetails(ByVal objImmunization As ClsImmunization)

        Dim sqlconn As SqlConnection
        Dim sqlcmd As SqlCommand
        Dim strsqlconn As String = ""
        strsqlconn = GetConnectionString()
        sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
        sqlconn.Open()
        Dim strsql As String = ""
        Dim dr As SqlDataReader
        Try

            strsql = "SELECT isnull(sFirstName,'') as PatFname, isnull(sMiddleName,'') as PatMName, isnull(sLastName,'') as PatLName , isnull(dtDOB,'') as PatDOB , isnull(sGender,'') as PatGender , isnull(sMaritalStatus,'') as PatMaritalStat , isnull(sAddressLine1,'') + Space(1) + isnull(sAddressLine2,'') as PatAddress, isnull(sCity,'') as PatCity , isnull(sState,'') as PatState , isnull(sZIP,'') as PatZip, isnull(sCounty,'') as PatCounty,nProviderID as PatProviderId, " & _
                     "isnull(sMother_fName,'') as PatMotherFname, isnull(sMother_mName,'') as PatMotherMName, isnull(sMother_lName,'') as PatMotherLName, isnull(sMother_Address1,'') + space(1) + isnull(sMother_Address2,'') as PatMotherAddress, isnull(sMother_City,'') as PatMotherCity, isnull(sMother_State,'') as PatMotherState, isnull(sMother_ZIP,'') as PatMotherZip, " & _
                     "isnull(sMother_County,'') as PatMotherCounty, isnull(sFather_fName,'') as PatFatherFname, isnull(sFather_mName,'') as PatFatherMName, isnull(sFather_lName,'') as PatFatherLName, isnull(sFather_Address1,'') + space(1) + isnull(sFather_Address2,'') as PatFatherAddress, isnull(sFather_City,'') as PatFatherCity, isnull(sFather_State,'') as PatFatherState, isnull(sFather_ZIP,'') as PatFatherZip, " & _
                     "isnull(sFather_County,'') as PatFatherCounty, isnull(sGuardian_fName,'') as PatGuardianFName, isnull(sGuardian_mName,'') as PatGuardianMName, isnull(sGuardian_lName,'') as PatGuardianLName, isnull(sGuardian_Address1,'') + Space(1) + isnull(sGuardian_Address2,'') as PatGuardianAddress, isnull(sGuardian_City,'') as PatGuardianCity, " & _
                     "isnull(sGuardian_State,'') as PatGuardianState, isnull(sGuardian_ZIP,'') as patGuardianZip, isnull(sGuardian_County,'') as PatGuardianCounty" & _
                     " FROM Patient where sPatientCode= " & "'" & "" & objImmunization.PatientID.Replace("'", "" & "'" & "'") & "" & "'" & ""

            'strsql = "select * from patient where sPatientCode = " & "'" & "" & objImmunization.PatientID & "" & "'" & ""

            sqlcmd = New SqlCommand(strsql, sqlconn)
            dr = sqlcmd.ExecuteReader
            If Not IsNothing(dr) Then
                While dr.Read

                    '--------Patient Info
                    objImmunization.PersonFName = CType(dr.Item("PatFname"), String)
                    objImmunization.PersonMName = CType(dr.Item("PatMName"), String)
                    objImmunization.PersonLName = CType(dr.Item("PatLName"), String)
                    Select Case CType(dr.Item("PatGender"), String)
                        Case "Male"
                            objImmunization.PersonGender = "M"

                        Case "Female"
                            objImmunization.PersonGender = "F"

                    End Select
                    objImmunization.PersonCounty = CType(dr.Item("PatCounty"), String)
                    objImmunization.PersonDOB = CType(dr.Item("PatDOB"), String)
                    objImmunization.PersonState = CType(dr.Item("PatState"), String)
                    objImmunization.PersonCity = CType(dr.Item("PatCity"), String)
                    objImmunization.PersonZip = CType(dr.Item("PatZip"), String)
                    objImmunization.PersonAddress = CType(dr.Item("PatAddress"), String)
                    '--------Patient Info

                    '--------Patient Mother Info
                    objImmunization.PersonMotherFName = CType(dr.Item("PatMotherFname"), String)
                    objImmunization.PersonMotherMName = CType(dr.Item("PatMotherMName"), String)
                    objImmunization.PersonMotherLName = CType(dr.Item("PatMotherLName"), String)
                    objImmunization.PersonMotherAddress = CType(dr.Item("PatMotherAddress"), String)
                    objImmunization.PersonMotherCity = CType(dr.Item("PatMotherCity"), String)
                    objImmunization.PersonMotherState = CType(dr.Item("PatMotherState"), String)
                    objImmunization.PersonMotherZip = CType(dr.Item("PatMotherZip"), String)
                    objImmunization.PersonMotherCounty = CType(dr.Item("PatMotherCounty"), String)
                    '--------Patient Mother Info

                    '--------Patient Father Info
                    objImmunization.PersonFatherFName = CType(dr.Item("PatFatherFname"), String)
                    objImmunization.PersonFatherMName = CType(dr.Item("PatFatherMName"), String)
                    objImmunization.PersonFatherLName = CType(dr.Item("PatFatherLName"), String)
                    objImmunization.PersonFatherAddress = CType(dr.Item("PatFatherAddress"), String)
                    objImmunization.PersonFatherCity = CType(dr.Item("PatFatherCity"), String)
                    objImmunization.PersonFatherState = CType(dr.Item("PatFatherState"), String)
                    objImmunization.PersonFatherZip = CType(dr.Item("PatFatherZip"), String)
                    objImmunization.PersonFatherCounty = CType(dr.Item("PatFatherCounty"), String)
                    '--------Patient Father Info


                    '--------Patient Guardian Info
                    objImmunization.PersonGuardianFName = CType(dr.Item("PatGuardianFName"), String)
                    objImmunization.PersonGuardianMName = CType(dr.Item("PatGuardianMName"), String)
                    objImmunization.PersonGuardianLName = CType(dr.Item("PatGuardianLName"), String)
                    objImmunization.PersonGuardianAddress = CType(dr.Item("PatGuardianAddress"), String)
                    objImmunization.PersonGuardianCity = CType(dr.Item("PatGuardianCity"), String)
                    objImmunization.PersonGuardianState = CType(dr.Item("PatGuardianState"), String)
                    objImmunization.PersonGuardianZip = CType(dr.Item("patGuardianZip"), String)
                    objImmunization.PersonGuardianCounty = CType(dr.Item("PatGuardianCounty"), String)
                    '--------Patient Guardian Info

                    'objImmunization.ResponsiblePersonFName = CType(dr.Item(4), String)
                    'objImmunization.ResponsiblePersonLName = CType(dr.Item(5), String)
                    'objImmunization.ResponsiblePersonStreet = CType(dr.Item(6), String)
                    'objImmunization.ResponsiblePersonCity = CType(dr.Item(7), String)
                    'objImmunization.ResponsiblePersonState = CType(dr.Item(8), String)
                    'objImmunization.ResponsiblePersonCounty = CType(dr.Item(9), String)
                    'objImmunization.ResponsiblePersonZip = CType(dr.Item(10), String)
                    'objImmunization.ResponsiblePersonPhone = CType(dr.Item(11), String)


                End While
                dr.Close()

            End If

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr = Nothing
            End If
            If Not IsNothing(sqlcmd) Then
                sqlcmd.Dispose()
                sqlcmd = Nothing
            End If
            If Not IsNothing(sqlconn) Then
                If sqlconn.State = ConnectionState.Open Then
                    sqlconn.Close()
                End If
                sqlconn.Dispose()
                sqlconn = Nothing
            End If
        End Try
    End Sub


    Private Sub FillGuardianInformation(ByVal objImmunization As ClsImmunization)
        Try


            GetPatientGuardianDetails(objImmunization)
            '''''''''''validation wrt to Responsible Person i.e. Guardian
            'first check the Guardians information, if any one text is missing then go for Mothers information, if any on text is missing check for Fathers information.
            ''if all the guardian info is present dont check for Mothers and fathers Info and exit out.
            'if any of guardians, mothers and fathers info is missing then show the missing guardian info only.

            'Dim StrbldGuardianInfo As New System.Text.StringBuilder
            'Dim strbldMotherInfo As New System.Text.StringBuilder
            'Dim strbldFatherInfo As New System.Text.StringBuilder

            Dim BoolGuardInfoMissing As Boolean = False
            If objImmunization.PersonGuardianFName.Trim.Length = 0 Then
                BoolGuardInfoMissing = True
                'StrbldGuardianInfo.Append("First Name, ")
            End If
            If objImmunization.PersonGuardianLName.Trim.Length = 0 Then
                BoolGuardInfoMissing = True
                'StrbldGuardianInfo.Append("Last Name, ")
            End If
            If objImmunization.PersonGuardianAddress.Trim.Length = 0 Then
                BoolGuardInfoMissing = True
                'StrbldGuardianInfo.Append("Address, ")
            End If
            If objImmunization.PersonGuardianCity.Trim.Length = 0 Then
                BoolGuardInfoMissing = True
                'StrbldGuardianInfo.Append("City, ")
            End If
            If objImmunization.PersonGuardianState.Trim.Length = 0 Then
                BoolGuardInfoMissing = True
                'StrbldGuardianInfo.Append("State, ")
            End If
            If objImmunization.PersonGuardianZip.Trim.Length = 0 Then
                BoolGuardInfoMissing = True
                'StrbldGuardianInfo.Append("Zip, ")
            End If
            If objImmunization.PersonGuardianCounty.Trim.Length = 0 Then
                BoolGuardInfoMissing = True
                'StrbldGuardianInfo.Append("County, ")
            End If

            If BoolGuardInfoMissing Then 'i.e guardian info is missing
                'since the BoolGuardInfoMissing variable is true that means one/all the guardian info is missing so check for mothers info

                Dim BoolMotherInfoMissing As Boolean = False

                If objImmunization.PersonMotherFName.Trim.Length = 0 Then
                    BoolMotherInfoMissing = True
                    'strbldMotherInfo.Append("Mother First Name, ")
                End If
                If objImmunization.PersonMotherLName.Trim.Length = 0 Then
                    BoolMotherInfoMissing = True
                    'strbldMotherInfo.Append("Mother Last Name, ")
                End If
                If objImmunization.PersonMotherAddress.Trim.Length = 0 Then
                    BoolMotherInfoMissing = True
                    'strbldMotherInfo.Append("Mother Address, ")
                End If
                If objImmunization.PersonMotherCity.Trim.Length = 0 Then
                    BoolMotherInfoMissing = True
                    'strbldMotherInfo.Append("Mother City, ")
                End If
                If objImmunization.PersonMotherState.Trim.Length = 0 Then
                    BoolMotherInfoMissing = True
                    'strbldMotherInfo.Append("Mother State, ")
                End If
                If objImmunization.PersonMotherZip.Trim.Length = 0 Then
                    BoolMotherInfoMissing = True
                    'strbldMotherInfo.Append("Mother Zip, ")
                End If
                If objImmunization.PersonMotherCounty.Trim.Length = 0 Then
                    BoolMotherInfoMissing = True
                    'strbldMotherInfo.Append("Mother County, ")
                End If

                If BoolMotherInfoMissing Then
                    'since the BoolMotherInfoMissing variable is true that means one/all the Mother info is missing so check for Fathers info
                    Dim BoolFatherInfoMissing As Boolean = False

                    If objImmunization.PersonFatherFName.Trim.Length = 0 Then
                        BoolFatherInfoMissing = True
                        'strbldFatherInfo.Append("Father First Name, ")
                    End If
                    If objImmunization.PersonFatherLName.Trim.Length = 0 Then
                        BoolFatherInfoMissing = True
                        'strbldFatherInfo.Append("Father Last Name, ")
                    End If
                    If objImmunization.PersonFatherAddress.Trim.Length = 0 Then
                        BoolFatherInfoMissing = True
                        'strbldFatherInfo.Append("Father Address, ")
                    End If
                    If objImmunization.PersonFatherCity.Trim.Length = 0 Then
                        BoolFatherInfoMissing = True
                        'strbldFatherInfo.Append("Father City, ")
                    End If
                    If objImmunization.PersonFatherState.Trim.Length = 0 Then
                        BoolFatherInfoMissing = True
                        'strbldFatherInfo.Append("Father State, ")
                    End If
                    If objImmunization.PersonFatherZip.Trim.Length = 0 Then
                        BoolFatherInfoMissing = True
                        'strbldFatherInfo.Append("Father Zip, ")
                    End If
                    If objImmunization.PersonFatherCounty.Trim.Length = 0 Then
                        BoolFatherInfoMissing = True
                        'strbldFatherInfo.Append("Father County, ")
                    End If

                    If BoolFatherInfoMissing Then
                        'since the BoolFatherInfoMissing variable is true that means one/all the Fathers info is missing, therefore show the missing guardian info atlast
                        'StrbldGuardianInfo.Append(" missing for Guardian" & vbCrLf)
                        'StrMessageBuilder.Append(StrbldGuardianInfo)

                    Else 'since all fathersinfo is present dont check any other info becaz fathers info is present as guardian info.
                        'dont check any other info becaz fathers info is present as guardian info.

                        ''''''''''''''''''''enter fathers information as guardian info
                        objImmunization.ResponsiblePersonFName = objImmunization.PersonFatherFName.Trim
                        objImmunization.ResponsiblePersonLName = objImmunization.PersonFatherLName.Trim
                        objImmunization.ResponsiblePersonMName = objImmunization.PersonFatherMName.Trim
                        objImmunization.ResponsiblePersonStreet = objImmunization.PersonFatherAddress.Trim
                        objImmunization.ResponsiblePersonState = objImmunization.PersonFatherState.Trim
                        objImmunization.ResponsiblePersonCity = objImmunization.PersonFatherCity.Trim
                        objImmunization.ResponsiblePersonCounty = objImmunization.PersonFatherCounty.Trim
                        objImmunization.ResponsiblePersonZip = objImmunization.PersonFatherZip.Trim
                    End If

                Else 'mothers all info is present, so dont check for fathersinfo becaz the mothers info is present as guardian info.
                    'dont check for fathersinfo becaz the mothers info is present as guardian info.
                    ''''''''''''''''''''enter mothers information as guardian info
                    objImmunization.ResponsiblePersonFName = objImmunization.PersonMotherFName.Trim
                    objImmunization.ResponsiblePersonLName = objImmunization.PersonMotherLName.Trim
                    objImmunization.ResponsiblePersonMName = objImmunization.PersonMotherMName.Trim
                    objImmunization.ResponsiblePersonStreet = objImmunization.PersonMotherAddress.Trim
                    objImmunization.ResponsiblePersonState = objImmunization.PersonMotherState.Trim
                    objImmunization.ResponsiblePersonCity = objImmunization.PersonMotherCity.Trim
                    objImmunization.ResponsiblePersonCounty = objImmunization.PersonMotherCounty.Trim
                    objImmunization.ResponsiblePersonZip = objImmunization.PersonMotherZip.Trim
                End If

            Else 'all guardian info is present.
                ''if all the guardian info is present dont check for Mothers and fathers Info and exit out.
                ''''''''''''''''''''enter gurdian information as guardian info
                objImmunization.ResponsiblePersonFName = objImmunization.PersonGuardianFName.Trim
                objImmunization.ResponsiblePersonLName = objImmunization.PersonGuardianLName.Trim
                objImmunization.ResponsiblePersonMName = objImmunization.PersonGuardianMName.Trim
                objImmunization.ResponsiblePersonStreet = objImmunization.PersonGuardianAddress.Trim
                objImmunization.ResponsiblePersonState = objImmunization.PersonGuardianState.Trim
                objImmunization.ResponsiblePersonCity = objImmunization.PersonGuardianCity.Trim
                objImmunization.ResponsiblePersonCounty = objImmunization.PersonGuardianCounty.Trim
                objImmunization.ResponsiblePersonZip = objImmunization.PersonGuardianZip.Trim
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub tblbtn_Close_32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblbtn_Close_32.Click
        If _blnRecordLock = False Then
            Dim Result As DialogResult
            If _blnChangesMade = True Then
                Result = MessageBox.Show("Do you want to save the changes ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                If Result = Windows.Forms.DialogResult.Yes Then
                    'btnIMTransactionSave_Click(sender, e)
                    tblbtn_Save_32_Click(sender, e)
                    _blnChangesMade = True
                    Me.Close()
                ElseIf Result = Windows.Forms.DialogResult.No Then
                    _blnChangesMade = False
                    Me.Close()
                ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If

            Else
                'If _isChangesdone = False Then
                '20110517
                'If lstReaction.Items.Count > 0 And _isChangesdone = True Then
                '    Result = MessageBox.Show("Do you want to save the changes ? ", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                '    If Result = Windows.Forms.DialogResult.Yes Then
                '        'btnIMTransactionSave_Click(sender, e)
                '        tblbtn_Save_32_Click(sender, e)
                '        _blnChangesMade = True
                '        Me.Close()
                '    ElseIf Result = Windows.Forms.DialogResult.No Then
                '        _blnChangesMade = False
                '        Me.Close()
                '    ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                '        Exit Sub
                '    End If

                'Else
                Me.Close()

                'End If
                '    Else
                '    Me.Close()
                'End If

            End If
        Else
        Me.Close()
        End If
        If _isClose = True Then
            hashtblItemName = Nothing
            Arry_CPTCODES = Nothing
        End If

        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "Immunization Transaction closed", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    End Sub

    Private Sub tblStrip_32_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip_32.ItemClicked

    End Sub

    ''''''''******This function is added by Anil on 01/10/07 at 10:10 a.m.
    '''''''*******This is added to add new Route, Site and Manufacturer using ContextMenu in the application in immunization module.
    Public Sub AddContextMenu()
        Try
            ''''''''This code gets the column which is clicked by the user 
            Dim _CategoryName As String
            _CategoryName = ""
            If C1FlexTransaction.Col = COL_ROUTE Then
                _CategoryName = "Route"
            ElseIf C1FlexTransaction.Col = COL_MANUFACT Then
                _CategoryName = "Manf."
            ElseIf C1FlexTransaction.Col = COL_SITE Then
                _CategoryName = "Site"
            End If
            ''''''''end

            Dim frm As New CategoryMaster(_CategoryName)        '''''''Object of Form CategoryMaster is Declared
            ''''''Category name is assigned and name is given to the form at runtime and CategoryMaster is opened.

            If _CategoryName = "Route" Then
                frm.Text = "Add Route"
                frm.ShowDialog()
                ''''''''''''
                Dim _Routes As String = " "
                Dim oRoutes As New gloStream.Immunization.Supporting.ItemDetails
                Dim oIMRoutes As New gloStream.Immunization.Common

                oRoutes = oIMRoutes.Route

                ' get item details
                If Not oRoutes Is Nothing Then
                    For i As Integer = 1 To oRoutes.Count
                        'If i = 1 Then
                        '    _Manufacturers = oMans(i).Description
                        'Else
                        _Routes = _Routes & "|" & oRoutes(i).Description
                        'End If
                    Next
                End If
                oRoutes = Nothing
                oIMRoutes = Nothing

                ''''''''''''
                C1FlexTransaction.Cols(COL_ROUTE).ComboList = _Routes
            End If
            If _CategoryName = "Site" Then
                frm.Text = "Add Site"
                frm.ShowDialog()

                Dim _Sites As String = " "
                Dim oSites As New gloStream.Immunization.Supporting.ItemDetails
                Dim oIMSite As New gloStream.Immunization.Common

                oSites = oIMSite.Sites
                ' get item details
                If Not oSites Is Nothing Then
                    For i As Integer = 1 To oSites.Count
                        'If i = 1 Then
                        '    _Manufacturers = oMans(i).Description
                        'Else
                        _Sites = _Sites & "|" & oSites(i).Description
                        'End If
                    Next
                End If
                oSites = Nothing
                oIMSite = Nothing

                C1FlexTransaction.Cols(COL_SITE).ComboList = _Sites
            End If
            If _CategoryName = "Manf." Then
                frm.Text = "Add Manufacturer"
                frm.ShowDialog()

                Dim _Manufacturers As String = " "
                Dim oMans As New gloStream.Immunization.Supporting.ItemDetails
                Dim oIM As New gloStream.Immunization.Common

                oMans = oIM.Manufacturers
                ' get item details
                If Not oMans Is Nothing Then
                    For i As Integer = 1 To oMans.Count
                        'If i = 1 Then
                        '    _Manufacturers = oMans(i).Description
                        'Else
                        _Manufacturers = _Manufacturers & "|" & oMans(i).Description
                        'End If
                    Next
                End If
                oMans = Nothing
                oIM = Nothing

                C1FlexTransaction.Cols(COL_MANUFACT).ComboList = _Manufacturers
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub C1FlexTransaction_ValidateEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.ValidateEditEventArgs)

        'sarika Expiry Date Validation 31st May 08

        If C1FlexTransaction.Cols(e.Col).Name = "Time Given" Then

            'code commented by sarika Expiry Date Validation 31st May 08


            'If C1FlexTransaction.Cols(e.Col).Caption = "Time Given" Then

            'Try
            '    Dim value As Integer = C1FlexTransaction.Editor.Text
            '    If value > 0 And value < 24 Then
            '        Return
            '    End If
            'Catch ex As Exception

            'End Try
            'e.Cancel = True
            '----
        End If


        'code added by sarika Expiry Date Validation
        '*******validation removed as per the mail from pravin sir dated 05 November 2008. "GLO2008-0002165 - Immunization for history fix"
        'If C1FlexTransaction.Cols(e.Col).Caption = "Exp. Date" Then
        '    Try
        '        Dim value As Date = C1FlexTransaction.Editor.Text
        '        If value < Now.Date Then
        '            '---------e.Cancel = True
        '            MessageBox.Show("Expiry Date cannot be less than Today's date.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            C1FlexTransaction.Editor.Text = Now.Date
        '            Return
        '        End If
        '    Catch ex As Exception
        '        UpdateLog("Error : " & ex.ToString & " validating Expiry date in Immunization transaction.")
        '    End Try

        'End If
        '-----------
    End Sub



    Private Sub C1FlexTransaction_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            'If C1FlexTransaction.ColSel = 24 Then
            '    Dim _StyleName As String = "CS_CPTCodes" & DateTime.Now.ToString("MMddyyhhmmss")

            '    'C1FlexTransaction.Cols(COL_CPTCODES).ComboList = C1FlexTransaction.GetData(C1FlexTransaction.RowSel, 25)

            '    Dim CSCPTCodes As C1.Win.C1FlexGrid.CellStyle = C1FlexTransaction.Styles.Add(_StyleName)
            '    With CSCPTCodes
            '        .Font = New Font(Font, FontStyle.Regular)
            '        '.ForeColor = Color.Navy
            '        '.BackColor = Color.LightYellow
            '        .Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
            '        If Not C1FlexTransaction.GetData(C1FlexTransaction.Rows(C1FlexTransaction.RowSel).Node.GetNode(NodeTypeEnum.Parent).Row.Index, 25) Is Nothing Then
            '            .ComboList = C1FlexTransaction.GetData(C1FlexTransaction.Rows(C1FlexTransaction.RowSel).Node.GetNode(NodeTypeEnum.Parent).Row.Index, 25).ToString()
            '        Else
            '            .ComboList = "..."
            '        End If

            '    End With


            '    Dim oRange As CellRange = C1FlexTransaction.GetCellRange(C1FlexTransaction.RowSel, 24, C1FlexTransaction.Row, 24)
            '    oRange.Style = C1FlexTransaction.Styles(_StyleName)
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub objImmunizationReport_ShowMessage(ByVal strMessage As String, ByRef ID As Int16) Handles objImmunizationReport.ShowMessage
        'Show richtextbox with data
        'load a modal form pass tthe message data to this form,when the user closes the form show below message
        Dim ofrmImValidation As New frmIm_Validation(strMessage)

        ofrmImValidation.ShowDialog()
        If ofrmImValidation.btnClick = 1 Then
            ID = 1
        Else 'means the ofrmImValidation.btnClick = 2 i.e. close was clicked
            ID = 2
        End If


        'If MessageBox.Show("The Data to Generate Immunization Report Is Incomplete and following data is missing : " & vbCrLf & StrMessageBuilder.ToString & vbCrLf & "Do you want to continue?", "Immunization", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
        '    ID = 1
        'Else
        '    ID = 2
        'End If
        Exit Sub
    End Sub

    'Private Sub C1FlexTransaction_MouseHoverCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1FlexTransaction.MouseHoverCell
    '    Try
    '        If C1FlexTransaction.mo Then
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub C1FlexTransaction_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub C1FlexTransaction_CellButtonClick(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
        '''''''''''''''Reaction button click
        Try
            If C1FlexTransaction.GetCellCheck(C1FlexTransaction.Row, COL_ITEMNAME) = CheckEnum.Checked Then ''''''''''''' Added by Ujwala Atre - to show Message if Immunization is not selected  - as on 20100518
                If e.Col = COL_REACTIONBTN Then
                    Dim RowNumber As Int16
                    Dim ColNumber As Int16
                    RowNumber = e.Row
                    ColNumber = e.Col
                    LoadUserGrid()
                    dgCustomGrid.Label1.Visible = False
                    dgCustomGrid.txtsearch.Visible = False
                    dgCustomGrid.Panel2.Visible = False
                    pnlcustomTask.Visible = True

                    pnlcustomTask.BringToFront()
                    _Temprow = e.Row

                    Dim rg As C1.Win.C1FlexGrid.CellRange = C1FlexTransaction.GetCellRange(_Temprow, COL_REACTION, _Temprow, COL_REACTION)
                    Dim RxStyle As CellStyle = rg.Style()

                    '.HistoryCategory = Trim(c1ProblemList.GetData(i, COL_PRESCRIPTION))
                    If Not IsNothing(RxStyle) Then
                        If RxStyle.ComboList <> "" Then
                            _TempRx = RxStyle.ComboList

                        Else
                            _TempRx = String.Empty
                        End If
                    Else
                        _TempRx = String.Empty
                    End If

                    SetReactionVals()
                End If
            End If ''''''''''''' Added by Ujwala Atre - to show Message if Immunization is not selected  - as on 20100518
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.CloseClick
        dgCustomGrid.Visible = False
        pnlcustomTask.Visible = False
    End Sub



    Private Sub dgCustomGrid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgCustomGrid.MouseDown
        Dim x As Integer = e.X
        Dim y As Integer = e.Y
    End Sub

    'Private Sub dgCustomGrid_HistoryClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
    '    Try

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        dgCustomGrid.Visible = False
    '    End Try
    'End Sub


    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCustomGrid.OKClick
        Try

            strRx = String.Empty
            cmbreac.Items.Clear()
            cmbreac.Items.Add("")
            cmbreac.Text = ""
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                If dgCustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    ' If strRx = "" Then
                    'strRx = dgCustomGrid.GetItem(i, 1).ToString
                    If dgCustomGrid.GetItem(i, 4).ToString.Trim() <> "" Then
                        cmbreac.Items.Add(dgCustomGrid.GetItem(i, 1).ToString & "-" & dgCustomGrid.GetItem(i, 4).ToString)
                    Else
                        cmbreac.Items.Add(dgCustomGrid.GetItem(i, 1).ToString)

                    End If

                    'dgCustomGrid.C1Task.SetCellCheck(i, 0, CheckEnum.Checked)
                    'If Not IsDBNull(dgCustomGrid.GetItem(i, 2)) And dgCustomGrid.GetItem(i, 2).ToString.Trim <> "" Then
                    'Replaced character " - " by  " ~ " 
                    '  strRx &= " ~ " & dgCustomGrid.GetItem(i, 2).ToString

                    ' End If
                    'Else
                    ' strRx &= "|" & dgCustomGrid.GetItem(i, 1).ToString '& "-" & dtDia.Rows(i)("Field2")
                    'If Not IsDBNull(dgCustomGrid.GetItem(i, 2)) And dgCustomGrid.GetItem(i, 2).ToString.Trim <> "" Then
                    'strRx &= " ~ " & dgCustomGrid.GetItem(i, 2).ToString
                    'cmbreac.Items.Add(dgCustomGrid.GetItem(i, 2).ToString)
                    'End If
                    'End If

                End If
            Next
            If cmbreac.Items.Count > 1 Then
                cmbreac.SelectedIndex = 1
            End If

            'With C1FlexTransaction

            '    Dim csRx As CellStyle = .Styles.Add("ReactionStyle" & _Temprow)
            '    If (strRx = "") Then
            '        strRx = " |"
            '    End If
            '    csRx.ComboList = strRx
            '    ''''
            '    Dim rg As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Temprow, COL_REACTION, _Temprow, COL_REACTION)
            '    rg.Style = csRx
            '    _Temprow = 0

            'End With
            pnlcustomTask.Visible = False


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            dgCustomGrid.Visible = False
        End Try
    End Sub



    Private Sub LoadUserGrid()  ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
        Try
            AddControl()
            If Not IsNothing(dgCustomGrid) Then
                dgCustomGrid.Visible = True
                'dgCustomGrid.Width = pnlWordObj.Width
                dgCustomGrid.Width = pnlcustomTask.Width
                'pnlcustomTask.Width = dgCustomGrid.Width
                '''''''''''
                ''  pnlcustomTask.Height = pnlcustomTask.Height + 100
                '''''''''''
                dgCustomGrid.Height = pnlcustomTask.Height
                'If dgCustomGrid.Height < 150 Then
                '    dgCustomGrid.Height = 150
                'End If
                dgCustomGrid.BringToFront()
                dgCustomGrid.SetVisible = False
                BindUserGrid()
                dgCustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
                pnlcustomTask.BringToFront()
                'pnlMain.SendToBack()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub   ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110



    ''' 
    Public Sub CustomReactionGridStyle()  ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

        Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5

        ' '' Show Drugs Info
        With dgCustomGrid.C1Task
            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .Cols.Count = Col_HRCount
            .AllowEditing = True

            .SetData(0, Col_Check, "Select")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_Check).Width = _TotalWidth * 1
            .Cols(Col_Check).AllowEditing = True
            .Cols(Col_Check).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_HReaction, "Reaction")
            '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(Col_HReaction).Width = _TotalWidth * 2
            .Cols(Col_HReaction).AllowEditing = False
            ''.Cols(COL_REACTION).DataType = System.Type.GetType("System.Boolean")

            .SetData(0, Col_Comment, "Comment")
            .Cols(Col_Comment).Width = _TotalWidth * 2
            .Cols(Col_Comment).AllowEditing = False
            ''move the last column to select column
            '.Cols.Move(.Cols.Count - 1, 0)
            'dgCustomGrid.C1Task.SetCellCheck(3, 0, CheckEnum.Checked)

        End With

    End Sub   ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
    Private Sub BindUserGrid()  ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

        Try
            Dim dt As DataTable
            Dim strSQL As String
            Dim oDB As New DataBaseLayer

            ' strSQL = "select distinct sHistoryItem Reaction,sComments Comment from history where sHistoryCategory ='Allergies' and nPatientID=" & _PatientID & " order by sHistoryItem"
            dt = Nothing
            '  strSQL = "SELECT DISTINCT sHistoryItem Item,sComments Comment, CASE WHEN CharIndex('InActive', ISNULL(sReaction,''))>0   THEN 0 ELSE 1 END ReactionFlag,CASE WHEN  Substring(IsNull(sReaction,'|'),1, CharIndex('|', isnull(sReaction,'|')))='' then '' else Substring(IsNull(sReaction,'|'),1, CharIndex('|', isnull(sReaction,'|'))-1) end Reaction  from history where sHistoryCategory ='Allergies' and nPatientID=" & _PatientID & "  order by sHistoryItem"

            strSQL = "SELECT DISTINCT sHistoryItem Item,sComments Comment, CASE WHEN CharIndex('InActive', ISNULL(sReaction,''))>0   THEN 0 ELSE 1 END ReactionFlag,CASE WHEN  Substring(IsNull(sReaction,'|'),1, CharIndex('|', isnull(sReaction,'|')))='' then '' else Substring(IsNull(sReaction,'|'),1, CharIndex('|', isnull(sReaction,'|'))-1) end Reaction  from history  inner join visits v on history.nvisitid= v.nvisitid  where sHistoryCategory ='Allergies' and History.nPatientID=" & _PatientID & " and convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+  convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+ convert(varchar(50),datepart(yy,v.dtvisitdate)))='" + DateTime.Now.ToString("MM/dd/yyyy") + "' order by sHistoryItem"

            dt = oDB.GetDataTable_Query(strSQL)

            CustomReactionGridStyle()

            Dim colActive As New DataColumn
            colActive.ColumnName = "Active"
            colActive.DataType = System.Type.GetType("System.Boolean")

            colActive.DefaultValue = CBool("False")
            colActive.Caption = "Active"
            dt.Columns.Add(colActive)

            Dim col As New DataColumn
            col.ColumnName = "Select"
            col.DataType = System.Type.GetType("System.Boolean")

            col.DefaultValue = CBool("False")
            dt.Columns.Add(col)





            If Not IsNothing(dt) Then
                '' For DataBinding Users
                ' If dt.Rows.Count > 0 Then ''commented by Sandip Darade 20090527 to fix the issue regarding prescriptions  
                dt.Columns("Select").Caption = "Select"

                For rowcnt As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(rowcnt)("ReactionFlag") = 1 Then
                        dt.Rows(rowcnt)("Active") = CBool("True")
                    Else
                        dt.Rows(rowcnt)("Active") = CBool("False")
                    End If
                Next


                dgCustomGrid.datasource(dt.DefaultView)
                ''End If
            End If

            ''Reset the grid
            Dim _TotalWidth As Single = dgCustomGrid.C1Task.Width - 5
            dgCustomGrid.C1Task.Cols.Move(dgCustomGrid.C1Task.Cols.Count - 1, 0)
            dgCustomGrid.C1Task.AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).AllowEditing = True
            dgCustomGrid.C1Task.Cols(0).Width = _TotalWidth * 0.25
            dgCustomGrid.C1Task.Cols(1).AllowEditing = False
            dgCustomGrid.C1Task.Cols(1).Width = _TotalWidth * 0.45
            dgCustomGrid.C1Task.Cols(3).Visible = False
            'dgCustomGrid.C1Task.Cols(2).AllowEditing = True
            '''''''''''''' MessageBox.Show(dgCustomGrid.C1Task.Height.ToString)
            '  dgCustomGrid.C1Task.Cols(2).AllowEditing = False
            ' dgCustomGrid.C1Task.Cols(2).Width = _TotalWidth * 0.45
            dgCustomGrid.C1Task.Cols(3).AllowEditing = False
            dgCustomGrid.C1Task.Cols(3).Width = _TotalWidth * 0.45

            ''''''''''''''''''''''''''''''            
            dgCustomGrid.C1Task.Cols(0).TextAlign = TextAlignEnum.LeftCenter
            dgCustomGrid.C1Task.Cols(1).TextAlign = TextAlignEnum.LeftCenter
            dgCustomGrid.C1Task.Cols(2).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''''''''
            'If dgCustomGrid.Height < 150 Then
            '    dgCustomGrid.Height = 150
            'End If
            'If dgCustomGrid.Width < 250 Then
            '    dgCustomGrid.Width = 250
            'End If
            'pnlcustomTask.Height = dgCustomGrid.Height
            'pnlcustomTask.Width = dgCustomGrid.Width
            'If pnlcustomTask.Height < 150 Then
            '    pnlcustomTask.Height = 150
            'End If
            'If pnlcustomTask.Width < 250 Then
            '    pnlcustomTask.Width = 250
            'End If
            '''''''''''''''''
            ''dgCustomGrid.C1Task.Height = dgCustomGrid.C1Task.Height + 100
            '  UserCount = dt.Rows.Count
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Immunization", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub  ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
    Private Sub SetReactionVals() ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

        Dim _TempRx As String = String.Empty
        For cmbreclan As Integer = 0 To cmbreac.Items.Count - 1
            _TempRx = _TempRx & "|" & cmbreac.Items(cmbreclan).ToString()
        Next

        If _TempRx <> "" Then
            Dim _Reacts As String() = Split(_TempRx, "|")
            For i As Int32 = 0 To dgCustomGrid.C1Task.Rows.Count - 1
                For _index As Int32 = 0 To _Reacts.Length - 1
                    ' Dim _Reactionm As String() = Reacts(_index)
                    If dgCustomGrid.GetItem(i, 1).ToString.Trim = _Reacts(_index).Trim Then
                        dgCustomGrid.C1Task.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    End If
                Next

            Next

        End If

    End Sub ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

    Private Sub AddControl() ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
        Try
            If Not IsNothing(dgCustomGrid) Then
                RemoveControl()
            End If
            'Dim xscrollpos As Integer = C1FlexTransaction.ScrollPosition.X
            'Dim yscrollpos As Integer = C1FlexTransaction.ScrollPosition.Y
            dgCustomGrid = New CustomTask
            '' C1FlexTransaction.Controls.Add(pnlcustomTask)

            'dgCustomGrid.Width = 320
            'pnlcustomTask.Width = 320
            pnlcustomTask.Controls.Add(dgCustomGrid)

            pnlcustomTask.BringToFront()

            'Dim y As Int64
            'Dim x As Int64

            'C1FlexTransaction.ShowCell(RowNumber, 0)
            'y = C1FlexTransaction.Cols(ColNumber - 2).Left + 52
            ''change by chetan to adjust window location
            'If RowNumber > 25 Then
            '    x = C1FlexTransaction.Rows(RowNumber).Top - pnlcustomTask.Height
            '    ' pnlcustomTask.Location = New Point(((customcolwidth + xscrollpos) * 2) - 200, x)
            'Else

            '    x = C1FlexTransaction.Rows(RowNumber).Bottom
            '    'pnlcustomTask.Location = New Point(y, x)
            '    ' pnlcustomTask.Location = New Point(((customcolwidth + xscrollpos) * 2) - 200, x)

            'End If
            'miniwindowy = x
            '    C1FlexTransaction.ScrollPosition = New Point(C1FlexTransaction.ScrollPosition.X, C1FlexTransaction.ScrollPosition.Y)
            'dgCustomGrid.Focus()
            'C1FlexTransaction.ScrollPosition = New Point(xscrollpos, yscrollpos)
            'miniwindow = 1
        Catch ex As Exception

        End Try
    End Sub ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

    Private Sub RemoveControl() ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
        Try
            If Not IsNothing(dgCustomGrid) Then
                If pnlcustomTask.Controls.Contains(dgCustomGrid) Then
                    pnlcustomTask.Controls.Remove(dgCustomGrid)
                End If
                dgCustomGrid.Visible = False
                dgCustomGrid = Nothing
            End If
        Catch ex As Exception
            '' MessageBox.Show(ex.Message)
        End Try
    End Sub ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

    Private Sub C1FlexTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '''''''''''''' Added by Ujwala Atre - to show Message if Immunization is not selected  - as on 20100518
        If C1FlexTransaction.GetCellCheck(C1FlexTransaction.Row, COL_ITEMNAME) <> CheckEnum.Checked And C1FlexTransaction.Col <> COL_ITEMNAME Then
            MessageBox.Show("Please Select the Immunization", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        '''''''''''''' Added by Ujwala Atre - to show Message if Immunization is not selected  - as on 20100518
    End Sub



    Private Sub C1FlexTransaction_BeforeMouseDown(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.BeforeMouseDownEventArgs)
        'If C1FlexTransaction.GetCellCheck(C1FlexTransaction.Row, COL_ITEMNAME) <> CheckEnum.Checked And C1FlexTransaction.Col <> COL_ITEMNAME Then
        '    MessageBox.Show(" Please Select the Immunization ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    e.Cancel = True
        '    Exit Sub
        'End If
    End Sub


    'Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
    '    '  Dim row_no As Integer = C1Flex_Transaction.Rows.Add()

    'End Sub
    Private Sub SaveIM_transaction()
        Try
            'Code Added on 20091110
            'To fix issue:5080:The gloEMR system should not print anything if there is no data
            'If SaveImmunization() = True Then
            '    Me.Close()
            'End If
            'End code Added on 20091110



            If SaveImmunization() = True Then
                ''Added Rahul on 201009010
                If Not arrDM Is Nothing Then
                    If arrDM.Count > 0 Then
                        For i As Integer = 0 To arrDM.Count - 1
                            Dim blnIsPresent As Boolean = False
                            Dim lst As New myList
                            lst = CType(arrDM(i), myList)
                            For j As Integer = 0 To C1Flex_Transaction.Rows.Count - 1

                                If Convert.ToString(lst.DMTemplateName) = C1Flex_Transaction.Item(j, COL_ITEMNAME) Then
                                    lst.IsFinished = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If

                If Not _arrImmu Is Nothing Then
                    If _arrImmu.Count > 0 Then
                        For i As Integer = 0 To _arrImmu.Count - 1
                            Dim blnIsPresent As Boolean = False
                            Dim lst As New myList
                            lst = CType(_arrImmu(i), myList)
                            For j As Integer = 0 To C1Flex_Transaction.Rows.Count - 1

                                If Convert.ToString(lst.DMTemplateName) = C1Flex_Transaction.Item(j, COL_ITEMNAME) Then
                                    lst.IsFinished = True
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If


                ''End
                hashtblItemName = Nothing
                Arry_CPTCODES = Nothing
                If _isClose = False Then
                    _isClose = True
                    Me.Close()

                End If

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.PatientRecordAdded, "Immunization Record Error.", gstrLoginName, gstrClientMachineName, _PatientID, , clsAudit.enmOutCome.Failure)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Error.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, "Immunization Record Error.", _PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        End Try
    End Sub
    Private Sub btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        Try


            Dim row_no As Integer = 0
            Dim stritemname As String = ""
            Dim oImmunization As New gloStream.Immunization.ItemSetup
            Dim oItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems

            Dim oTransaction As New gloStream.Immunization.Transaction
            Dim oIMTransaction As New gloStream.Immunization.Supporting.ImmunizationTransaction
            ' Dim csReaction As CellStyle
            ' counter for fill root node and subnode of the c1.
            Dim _N As Integer
            Dim _Nc As Integer


            For Each di As DictionaryEntry In hashtblItemName
                If di.Key.ToString = cmbimmu.Text Then
                    stritemname = di.Value.ToString()
                    Exit For
                End If
            Next

            oItemDetails = oImmunization.ItemDetails()
            'Dim strCurrentTime As String = Format(Now, "hh:mm:ss")
            ' loop for root node fill
            Dim _strImmu As String = ""


            If Not oItemDetails Is Nothing Then
                If oItemDetails.Count > 0 Then


                    ' fill name
                    For _N = 1 To oItemDetails.Count

                        'set the value of the root
                        If stritemname = oItemDetails(_N).Name Then
                            '.SetData(.Rows.Count - 1, COL_ITEMNAME, oItemDetails(_N).Name)
                            '.SetData(.Rows.Count - 1, COL_ITEMCOUNTERID, 0)
                            '.SetData(.Rows.Count - 1, COL_TRNID, 0)
                            '.SetData(.Rows.Count - 1, COL_ITEMID, oItemDetails(_N).ID)
                            '.SetData(.Rows.Count - 1, COL_IDENTIFIER, "I" & oItemDetails(_N).ID)

                            '.SetData(.Rows.Count - 1, Col_VaccineCode, oItemDetails(_N).VaccineCode)

                            If stritemname <> "" Then
                                If SelRowNo = -1 Then

                                    Dim flgaddrow As Integer = 1


                                    For row_cnt As Integer = 1 To C1Flex_Transaction.Rows.Count - 1
                                        If C1Flex_Transaction.GetData(row_cnt, COL_ITEMNAME) = cmbimmu.Text.Trim() Then
                                            row_no = row_cnt
                                            flgaddrow = 0
                                            Exit For
                                        End If
                                        'Or C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then

                                    Next
                                    If flgaddrow = 1 Then
                                        C1Flex_Transaction.Rows.Add(1)
                                        row_no = C1Flex_Transaction.Rows.Count - 1
                                    End If



                                Else




                                    row_no = SelRowNo
                                End If

                                ''Added By Shweta 20100915 for MU
                                If Trim(txtdose.Text) = "" And Trim(txtDoseUnit.Text) <> "" Then
                                    txtDoseUnit.Text = ""
                                End If
                                ''End-Added By Shweta 20100915 for MU


                                With C1Flex_Transaction

                                    .SetData(row_no, COL_TRNID, 0)
                                    ' .SetData(row_no, COL_TRNDATE, "Trn. Date")
                                    .SetData(row_no, COL_PATIENTID, "Pat. ID")
                                    .SetData(row_no, COL_VISITID, "Visit. ID")
                                    .SetData(row_no, COL_ITEMID, oItemDetails(_N).ID)
                                    .SetData(row_no, COL_ITEMNAME, cmbimmu.Text)
                                    Dim ItemCounterID As Integer = Convert.ToInt32(cmbimmu.Text.Substring(stritemname.Length, (cmbimmu.Text.Length - stritemname.Length)))


                                    .SetData(row_no, COL_ITEMCOUNTERID, ItemCounterID)
                                    .SetData(row_no, COL_DOSE, txtdose.Text)
                                    ''Added By Shweta 20100915 for MU
                                    .SetData(row_no, COL_DOSEUNIT, txtDoseUnit.Text)
                                    ''End-Added By Shweta 20100915 for MU

                                    If cmbdtgiven.Checked = True Then
                                        .SetData(row_no, COL_DATEGIVEN, cmbdtgiven.Text)
                                        .SetData(row_no, COL_TIMEGIVEN, txttime.Text)
                                        .SetData(row_no, COL_TRNDATE, cmbdtgiven.Text)
                                    Else
                                        .SetData(row_no, COL_DATEGIVEN, "")
                                        .SetData(row_no, COL_TIMEGIVEN, "")
                                        .SetData(row_no, COL_TRNDATE, "")
                                    End If
                                    '.SetData(row_no, COL_DATEGIVEN, cmbdtgiven.Text)
                                    '.SetData(row_no, COL_TIMEGIVEN, txttime.Text)
                                    .SetData(row_no, COL_ROUTE, cmbroute.Text)
                                    .SetData(row_no, COL_LOTNUMBER, txtlotno.Text)
                                    If dtexpdate.Checked = True Then
                                        .SetData(row_no, COL_EXPDATE, dtexpdate.Text)
                                    Else
                                        .SetData(row_no, COL_EXPDATE, "")
                                    End If
                                    .SetData(row_no, COL_MANUFACT, cmbmanufac.Text)
                                    .SetData(row_no, COL_ISLOCK, "Lock")
                                    .SetData(row_no, COL_USERID, "User ID")
                                    .SetData(row_no, COL_NOTES, txtnotes.Text)
                                    If cmbduedt.Checked = True Then
                                        .SetData(row_no, COL_DUEDATE, cmbduedt.Text)
                                    Else
                                        .SetData(row_no, COL_DUEDATE, "")
                                    End If
                                    'Sanjog -
                                    If rbtn_Admin.Checked Then
                                        .SetData(row_no, COL_AdminStatus, "Administered")
                                    Else
                                        .SetData(row_no, COL_AdminStatus, "Reported")
                                    End If
                                    'Sanjog -
                                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                                    '  csReaction = .Styles.Add("csReaction" & row_no)
                                    'StrReaction = ""
                                    ' '' Fill Values In ComboBox
                                    'If Not IsDBNull(oIMTransactionLine.Reaction) Then
                                    '    StrReaction = oIMTransactionLine.Reaction.ToString

                                    ''''''''''Added by Ujwala - for Snomed Immunization - Dont add Reaction date for blank reaction - as on 20101008
                                    Dim StrReaction As String = ""

                                    If lstReaction.Items.Count > 0 Then

                                        'For reacitems As Integer = 1 To cmbreac.Items.Count - 1
                                        '    If reacitems = 1 Then
                                        '        StrReaction = cmbreac.Items(reacitems).ToString()
                                        '    Else
                                        '        StrReaction = StrReaction & "," & cmbreac.Items(reacitems).ToString()
                                        '    End If

                                        'Next
                                        For i As Integer = 0 To lstReaction.Items.Count - 1
                                            If i = 0 Then
                                                StrReaction = lstReaction.Items(i).ToString()
                                            Else
                                                StrReaction = StrReaction & vbNewLine & lstReaction.Items(i).ToString()
                                            End If
                                        Next
                                    End If

                                    ''''''''''Added by Ujwala - for Snomed Immunization - Dont add Reaction date for blank reaction - as on 20101008
                                    If StrReaction = "" Then
                                        cmbreacdt.Checked = False
                                    End If
                                    ''''''''''Added by Ujwala - for Snomed Immunization - Dont add Reaction date for blank reaction - as on 20101008

                                    If cmbreacdt.Checked = True Then
                                        .SetData(row_no, COL_REACTIONDT, cmbreacdt.Text)
                                    Else
                                        .SetData(row_no, COL_REACTIONDT, "")
                                    End If

                                    'StrReaction = cmbreac.Text.Trim()

                                    .Cols(COL_REACTION).TextAlign = TextAlignEnum.LeftCenter
                                    ''''''''''''''' Added by Ujwala Atre 
                                    Dim arrReaction As String()
                                    arrReaction = StrReaction.Split(vbNewLine)
                                    .Rows(row_no).Height = .Rows.DefaultSize * arrReaction.Length - 1
                                    ''''''''''''''' Added by Ujwala Atre 
                                    .SetData(row_no, COL_REACTION, StrReaction)



                                    ' .SetData(row_no, COL_REACTION, cmbreac.Text)
                                    .SetData(row_no, COL_REACTIONBTN, " ")




                                    '.SetData(row_no, COL_REACTIONDT, cmbreacdt.Text)
                                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110



                                    .SetData(row_no, COL_SITE, cmbsite.Text)
                                    '--------
                                    .SetData(row_no, COL_CPTCODES, cmbcptcode.Text)
                                    '--------
                                    .SetData(row_no, Col_USERNAME, gstrLoginName)

                                    'sarika
                                    .SetData(row_no, COL_ISREMINDER, "Reminder")
                                    .SetData(row_no, COL_VACCINEELIGIBILITYCODE, cmbelicode.Text)
                                    '---------
                                    .SetData(row_no, COL_REASONFORNONADMIN, cmbradmin.Text)
                                    .SetData(row_no, Col_CPTCodesHidden, "CPT Codes Hidden")
                                    'Code Start-Added by kanchan on 20100914 for immunization MU
                                    '.SetData(row_no, Col_VaccineCode, "")
                                    .SetData(row_no, Col_VaccineCode, oItemDetails(_N).VaccineCode)
                                    .SetData(row_no, Col_ItemName2, stritemname)

                                    'Code Start-Added by kanchan on 20100904 for snomed implementation in immunization
                                    Dim dt As New DataTable
                                    dt = oImmunization.GetSnoMedids(oItemDetails(_N).ID)
                                    If Not IsNothing(dt) Then
                                        If dt.Rows.Count > 0 Then
                                            .SetData(row_no, COL_ConceptID, dt.Rows(0)("im_sConceptID"))
                                            .SetData(row_no, COL_DescriptionID, dt.Rows(0)("im_sDescriptionID"))
                                            .SetData(row_no, COL_SnomedID, dt.Rows(0)("im_sSnoMedID"))
                                        End If
                                    End If
                                    'Code End-Added by kanchan on 20100904 for snomed implementation in immunization

                                    If chkrem.Checked Then
                                        .SetCellCheck(row_no, COL_ISREMINDER, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                    Else

                                        .SetCellCheck(row_no, COL_ISREMINDER, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                    End If


                                End With

                            End If

                            Exit For
                        End If
                    Next ' End If

                End If
            End If
            '_isChangesdone = True
        Catch ex As Exception
        Finally
            SelRowNo = -1
            Clear()
            SetComboIndex()
            RemovecheckBox()
        End Try
    End Sub
    Private Sub FillIMForDM(ByVal _streItemname As String, ByVal ID As Int64, ByVal _strVacName As String, ByVal _strDescriptionId As String, ByVal _strConceptId As String, ByVal _strSnomedId As String)
        With C1Flex_Transaction
            Dim row_no As Integer
            row_no = .Rows.Add().Index
            .SetData(row_no, COL_TRNID, 0)
            ' .SetData(row_no, COL_TRNDATE, "Trn. Date")
            .SetData(row_no, COL_PATIENTID, "Pat. ID")
            .SetData(row_no, COL_VISITID, "Visit. ID")
            .SetData(row_no, COL_ITEMID, ID)
            .SetData(row_no, COL_ITEMNAME, _strVacName)
            Dim ItemCounterID As Integer = Convert.ToInt32(_strVacName.Substring(_streItemname.Length, (_strVacName.Length - _streItemname.Length)))


            .SetData(row_no, COL_ITEMCOUNTERID, ItemCounterID)
            .SetData(row_no, COL_DOSE, "")


            If cmbdtgiven.Checked = True Then
                .SetData(row_no, COL_DATEGIVEN, cmbdtgiven.Text)
                .SetData(row_no, COL_TIMEGIVEN, "")
                .SetData(row_no, COL_TRNDATE, "")
            Else
                .SetData(row_no, COL_DATEGIVEN, cmbdtgiven.Text)
                ''Sanjog-Added on 20101129 to show the default time for the Immunization
                .SetData(row_no, COL_TIMEGIVEN, "12:00:00 AM")
                .SetData(row_no, COL_TRNDATE, "")
            End If
            '.SetData(row_no, COL_DATEGIVEN, cmbdtgiven.Text)
            '.SetData(row_no, COL_TIMEGIVEN, txttime.Text)
            .SetData(row_no, COL_ROUTE, "")
            .SetData(row_no, COL_LOTNUMBER, "")
            If dtexpdate.Checked = True Then
                .SetData(row_no, COL_EXPDATE, "")
            Else
                .SetData(row_no, COL_EXPDATE, "")
            End If
            .SetData(row_no, COL_MANUFACT, "")
            .SetData(row_no, COL_ISLOCK, "Lock")
            .SetData(row_no, COL_USERID, "User ID")
            .SetData(row_no, COL_NOTES, "")

            .SetData(row_no, COL_DUEDATE, "")




            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
            '  csReaction = .Styles.Add("csReaction" & row_no)
            'StrReaction = ""
            ' '' Fill Values In ComboBox
            'If Not IsDBNull(oIMTransactionLine.Reaction) Then
            '    StrReaction = oIMTransactionLine.Reaction.ToString




            .SetData(row_no, COL_REACTIONDT, "")


            .Cols(COL_REACTION).TextAlign = TextAlignEnum.LeftCenter
            .SetData(row_no, COL_REACTION, "")



            ' .SetData(row_no, COL_REACTION, cmbreac.Text)
            .SetData(row_no, COL_REACTIONBTN, " ")




            '.SetData(row_no, COL_REACTIONDT, cmbreacdt.Text)
            ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110



            .SetData(row_no, COL_SITE, "")
            '--------
            .SetData(row_no, COL_CPTCODES, "")
            '--------
            .SetData(row_no, Col_USERNAME, gstrLoginName)

            'sarika
            .SetData(row_no, COL_ISREMINDER, "Reminder")
            .SetData(row_no, COL_VACCINEELIGIBILITYCODE, "")
            '---------
            .SetData(row_no, COL_REASONFORNONADMIN, "")
            .SetData(row_no, Col_CPTCodesHidden, "CPT Codes Hidden")
            .SetData(row_no, Col_VaccineCode, "")
            .SetData(row_no, Col_ItemName2, _streItemname)

            'Code Start-Added by kanchan on 20100904 for snomed implementation in immunization
            'Dim dt As New DataTable
            'dt = oImmunization.GetSnoMedids(oItemDetails(_N).ID)
            'If Not IsNothing(dt) Then
            '    If dt.Rows.Count > 0 Then
            .SetData(row_no, COL_ConceptID, _strConceptId)
            .SetData(row_no, COL_DescriptionID, _strDescriptionId)
            .SetData(row_no, COL_SnomedID, _strSnomedId)
            '    End If
            'End If
            'Code End-Added by kanchan on 20100904 for snomed implementation in immunization

            If chkrem.Checked Then
                .SetCellCheck(row_no, COL_ISREMINDER, C1.Win.C1FlexGrid.CheckEnum.Checked)
            Else

                .SetCellCheck(row_no, COL_ISREMINDER, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
            End If


        End With
    End Sub

    Private Sub RemovecheckBox()
        dtexpdate.Checked = False
        cmbduedt.Checked = False
        cmbreacdt.Checked = False
        cmbdtgiven.Checked = False
    End Sub

    Private Sub cmbdtgiven_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdtgiven.ValueChanged
        If cmbdtgiven.Checked = True Then
            txttime.Text = DateTime.Now.ToString("hh:mm:ss tt")
            cmbduedt.Checked = False
        Else
            cmbduedt.Checked = True
            txttime.Text = ""
        End If

    End Sub

    ''Added by Mayuri:20101117-to show tooltip on combo box
    Private Sub cmbimmu_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbimmu.MouseMove
        Try
            combo = DirectCast(sender, ComboBox)
            If cmbimmu.SelectedItem <> "" Then
                If getWidthofListItems(cmbimmu.Items(cmbimmu.SelectedIndex), cmbimmu) >= cmbimmu.DropDownWidth - 20 Then

                    ToolTip1.SetToolTip(cmbimmu, cmbimmu.Items(cmbimmu.SelectedIndex))
                Else
                    Me.ToolTip1.Hide(cmbimmu)

                End If
            End If
        Catch Ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), False)
            Ex = Nothing
        End Try
    End Sub

    'Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer

    '    Dim g As Graphics = Me.CreateGraphics()
    '    Dim s As SizeF = g.MeasureString(_text, combo.Font)
    '    Dim width As Integer = Convert.ToInt32(s.Width)
    '    Return width
    'End Function

    ''End Sub





    Private Sub cmbimmu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbimmu.SelectedIndexChanged
        Try

       
            If blnClrAfterImmun = False Then
                ClearAfterImmun()
                ''''''''''Added by Ujwala - for Snomed Immunization - Auto set Dategiven Checkbox - as on 20101007
                If cmbimmu.Text.Trim() <> "" Then
                    cmbdtgiven.Checked = True
                    txttime.Text = DateTime.Now.ToString("hh:mm:ss tt")
                Else
                    cmbdtgiven.Checked = False
                    txttime.Text = ""
                End If
                ''''''''''Added by Ujwala - for Snomed Immunization - Auto set Dategiven Checkbox - as on 20101007            
            Else
                blnClrAfterImmun = False
            End If

            If SelRowNo = -1 Then
                'For row_cnt As Integer = 1 To C1Flex_Transaction.Rows.Count - 1
                '    If C1Flex_Transaction.GetData(row_cnt, COL_ITEMNAME).ToString().Trim() = cmbimmu.Text.Trim() Then
                '        MessageBox.Show("Immunization Already Exist.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        cmbimmu.Text = ""
                '        Exit Sub
                '    End If
                '    'Or C1FlexTransaction.GetCellCheck(i, COL_ITEMNAME) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then

                'Next

            End If




            Dim flag As Integer = 0
            If cmbimmu.Text.Length > 0 Then
                'Dim cmblen As Integer = 0
                'While flag = 0
                '    cmblen = cmblen + 1
                '    Dim strno As String = cmbimmu.Text.Substring((cmbimmu.Text.Length - cmblen), 1)
                '    Select Case strno
                '        Case "1"
                '            flag = 0

                '        Case "2"
                '            flag = 0
                '        Case "3"
                '            flag = 0
                '        Case "4"
                '            flag = 0
                '        Case "5"
                '            flag = 0
                '        Case "6"
                '            flag = 0
                '        Case "7"
                '            flag = 0
                '        Case "8"
                '            flag = 0
                '        Case "9"
                '            flag = 0
                '        Case "0"
                '            flag = 0
                '        Case Else
                '            flag = 1



                '    End Select


                'End While
                Dim stringtosearch As String = ""
                For Each di As DictionaryEntry In hashtblItemName
                    If di.Key.ToString = cmbimmu.Text.Trim() Then
                        stringtosearch = di.Value.ToString()
                        Exit For
                    End If
                Next

                ' Dim stringtosearch = cmbimmu.Text.Substring(0, (cmbimmu.Text.Length - cmblen) + 1)


                Dim query = From x In Arry_CPTCODES Where (x.StartsWith(stringtosearch)) Select x

                cmbcptcode.Items.Clear()
                cmbcptcode.Items.Add("")
                cmbcptcode.Text = ""
                For Each result As String In query
                    ' Dim strind As Integer = result.IndexOf("*#\")
                    Dim strind As String = result.Replace(stringtosearch & "*#\", "")
                    If strind.IndexOf("*#\") > -1 Then
                        strind = strind.Substring(strind.IndexOf("*#\") + 3, (strind.Length - (strind.IndexOf("*#\") + 3)))
                    End If
                    'cmbcptcode.Items.Add(result.Substring(strind + 3, (result.Length - (strind + 3))))

                    '  If cmbcptcode.Items.IndexOf(strind.Trim()) = -1 Then
                    cmbcptcode.Items.Add(strind.Trim())
                    ' End If
                Next


            End If
            SelRowNo = -1
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    Private Sub btnsave2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub


    Private Sub C1Flex_Transaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Flex_Transaction.Click


    End Sub

    Private Sub C1Flex_Transaction_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Flex_Transaction.DoubleClick
        Dim rowsel As Integer = C1Flex_Transaction.RowSel
        'cmbreac.Items.Clear()
        'cmbreac.Items.Add("")
        If rowsel > 0 Then
            Clear()
            blnClrAfterImmun = True
            With C1Flex_Transaction

                If Not IsNothing(.GetData(rowsel, COL_ITEMNAME)) Then
                    SelRowNo = rowsel
                    ' .GetData(rowsel, COL_TRNID)
                    ' .GetData(rowsel, COL_TRNDATE)
                    '.GetData(rowsel, COL_PATIENTID)
                    '.GetData(rowsel, COL_VISITID)
                    '.GetData(rowsel, COL_ITEMID)
                    '.GetData(rowsel, COL_ITEMNAME)
                    ' .GetData(rowsel, COL_ITEMCOUNTERID)

                    '' Added item Which is not present in combo box
                    If cmbimmu.Items.Contains(.GetData(rowsel, COL_ITEMNAME)) Then
                        cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                    Else
                        cmbimmu.Sorted = True
                        cmbimmu.Items.Add(.GetData(rowsel, COL_ITEMNAME))
                        cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                    End If
                    ''

                    txtdose.Text = .GetData(rowsel, COL_DOSE)

                    If .GetData(rowsel, COL_DATEGIVEN) <> "" Then
                        cmbdtgiven.Checked = True
                        ''Code for Fixed BugID 6828.
                        cmbdtgiven.Value = .GetData(rowsel, COL_DATEGIVEN)
                        ''End
                    End If

                    txttime.Text = .GetData(rowsel, COL_TIMEGIVEN)
                    cmbroute.Text = .GetData(rowsel, COL_ROUTE)
                    ''Added  By Shweta 20100915
                    txtDoseUnit.Text = .GetData(rowsel, COL_DOSEUNIT)

                    txtlotno.Text = .GetData(rowsel, COL_LOTNUMBER)
                    dtexpdate.Text = .GetData(rowsel, COL_EXPDATE)
                    cmbmanufac.Text = .GetData(rowsel, COL_MANUFACT)
                    ' .GetData(rowsel, COL_ISLOCK)
                    '.GetData(rowsel, COL_USERID)
                    txtnotes.Text = .GetData(rowsel, COL_NOTES)
                    cmbduedt.Text = .GetData(rowsel, COL_DUEDATE)
                    '''''''''''''''
                    If .GetData(rowsel, COL_DUEDATE) <> "" Then
                        cmbduedt.Checked = True
                        cmbdtgiven.Checked = False
                    Else
                        cmbdtgiven.Checked = True
                    End If
                    '''''''''''''''
                    Dim _SplReaction() As String

                    If Not IsNothing(.GetData(rowsel, COL_REACTION)) Then
                        ' _SplReaction = .GetData(rowsel, COL_REACTION).ToString().Split(",")
                        _SplReaction = Split(.GetData(rowsel, COL_REACTION).ToString().Trim(), vbNewLine)
                        'For lensplreac As Integer = 0 To _SplReaction.Length - 1
                        '    If _SplReaction(lensplreac).Trim() <> "" Then
                        '        cmbreac.Items.Add(_SplReaction(lensplreac))
                        '    End If
                        'Next
                        'If cmbreac.Items.Count > 1 Then
                        '    Dim reacInd As Integer = cmbreac.Items.IndexOf(_SplReaction)
                        '    If reacInd >= 1 Then
                        '        cmbreac.SelectedIndex = reacInd
                        '    End If
                        'End If
                        For i As Integer = 0 To _SplReaction.Count - 1
                            lstReaction.Items.Add(_SplReaction.GetValue(i).ToString().Trim())
                        Next
                        '  cmbreac.Text = .GetData(rowsel, COL_REACTION)
                    End If
                    .GetData(rowsel, COL_REACTIONBTN)

                    cmbreacdt.Text = .GetData(rowsel, COL_REACTIONDT)
                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 22rowsel11rowsel




                    cmbsite.Text = .GetData(rowsel, COL_SITE)
                    '--------
                    cmbcptcode.Text = .GetData(rowsel, COL_CPTCODES)
                    '--------
                    'txtusrname.Text = .GetData(rowsel, Col_USERNAME)

                    'sarika
                    '  .GetData(rowsel, COL_ISREMINDER)
                    cmbelicode.Text = .GetData(rowsel, COL_VACCINEELIGIBILITYCODE)

                    ' '' Added item Which is not present in combo box
                    'If cmbimmu.Items.Contains(.GetData(rowsel, COL_ITEMNAME)) Then
                    '    cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                    'Else
                    '    cmbimmu.Sorted = True
                    '    cmbimmu.Items.Add(.GetData(rowsel, COL_ITEMNAME))
                    '    cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                    'End If
                    ' ''

                    'Sanjog - Added on 2011 March 19 to select the reason for non admin
                    If .GetData(rowsel, COL_REASONFORNONADMIN) <> "" Then
                        cmbradmin.Text = .GetData(rowsel, COL_REASONFORNONADMIN)
                    Else
                        cmbradmin.Text = ""
                    End If
                    'Sanjog - Added on 2011 March 19 to select the reason for non admin


                    If .GetData(rowsel, COL_AdminStatus) = "Administered" Then
                        rbtn_Admin.Checked = True
                    Else
                        rbtn_Reported.Checked = True
                    End If


                    Dim bool As C1.Win.C1FlexGrid.CheckEnum = .GetCellCheck(rowsel, COL_ISREMINDER)
                    If bool = CheckEnum.Checked Then
                        chkrem.Checked = True
                    Else
                        chkrem.Checked = False
                    End If

                    '---------
                    '.GetData(rowsel, COL_REASONFORNONADMIN)
                    '.GetData(rowsel, Col_CPTCodesHidden)
                    '.GetData(rowsel, Col_VaccineCode)
                    '.GetData(rowsel, Col_ItemName2)

                    lblConceptID.Text = .GetData(rowsel, COL_ConceptID)
                    lblDescriptionID.Text = .GetData(rowsel, COL_DescriptionID)
                    lblSnoMedID.Text = .GetData(rowsel, COL_SnomedID)
                End If


            End With
        End If
    End Sub
    Public Sub Clear()
        txtdose.Text = ""
        ''Added By Shweta 20100915 for MU
        txtDoseUnit.Text = ""
        ''END-Added By Shweta 20100915 for MU
        txttime.Text = ""
        cmbroute.Text = ""
        txtlotno.Text = ""
        cmbmanufac.Text = ""
        txtnotes.Text = ""
        cmbreac.Text = ""
        cmbsite.Text = ""
        cmbcptcode.Text = ""
        cmbradmin.Text = ""
        cmbelicode.Text = ""
        cmbimmu.Text = ""
        chkrem.Checked = False
        cmbreacdt.Checked = False
        lstReaction.Items.Clear()
        ''cmbdtgiven.Value = DateTime.Now
        dtexpdate.Value = DateTime.Now
        cmbduedt.Value = DateTime.Now
        cmbreacdt.Value = DateTime.Now
        lblConceptID.Text = "ConceptID"
        lblDescriptionID.Text = "DescriptionID"
        lblSnoMedID.Text = "SnoMedID"
        ' cmbreac.Items.Clear()
    End Sub


    Public Sub ClearAfterImmun()
        txtdose.Text = ""
        ''Added By Shweta 20100915 for MU
        txtDoseUnit.Text = ""
        ''END-Added By Shweta 20100915 for MU
        txttime.Text = ""
        cmbroute.Text = ""
        txtlotno.Text = ""
        cmbmanufac.Text = ""
        txtnotes.Text = ""
        cmbreac.Text = ""
        cmbsite.Text = ""
        cmbcptcode.Text = ""
        cmbradmin.Text = ""
        cmbelicode.Text = ""
        chkrem.Checked = False
        lstReaction.Items.Clear()
        dtexpdate.Value = DateTime.Now
        cmbduedt.Value = DateTime.Now
        cmbreacdt.Value = DateTime.Now
        'cmbreac.Items.Clear()
        RemovecheckBox()
    End Sub

    Private Sub ClearComboBox()
        cmbreac.Items.Clear()
        cmbelicode.Items.Clear()
        cmbimmu.Items.Clear()
        cmbradmin.Items.Clear()
        cmbroute.Items.Clear()
        cmbsite.Items.Clear()
        cmbmanufac.Items.Clear()


    End Sub


    Private Sub btnBrowseReaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseReaction.Click
        'If cmbimmu.Text.Trim() <> "" Then
        '    Dim _sVaccineName As String = ""
        '    Dim _ConceptID As String = ""
        '    Dim _DescriptionID As String = ""
        '    Dim _SnomedID As String = ""
        '    Dim _ICD9 As String = ""
        '    Dim _SnomedDescription As String = ""
        '    Dim _SnomedDefination As String = ""
        '    Dim _RxNormID As String = ""
        '    Dim _NDCCode As String = ""
        '    Dim oImmunization As New gloStream.Immunization.ItemSetup
        '    For Each di As DictionaryEntry In hashtblItemName
        '        If di.Key.ToString = cmbimmu.Text Then
        '            _sVaccineName = di.Value.ToString()
        '            Exit For
        '        End If
        '    Next

        '    Dim dt As New DataTable
        '    dt = oImmunization.GetSnoMedids(_sVaccineName)
        '    If Not IsNothing(dt) Then
        '        If dt.Rows.Count > 0 Then
        '            _ConceptID = dt.Rows(0)("im_sConceptID")
        '            _DescriptionID = dt.Rows(0)("im_sDescriptionID")
        '            _SnomedID = dt.Rows(0)("im_sSnoMedID")
        '            _ICD9 = dt.Rows(0)("sICD9")
        '            _RxNormID = dt.Rows(0)("sTranID1")
        '            _SnomedDefination = dt.Rows(0)("im_sSnomedDefination")
        '            _NDCCode = dt.Rows(0)("sTranID2")
        '        End If
        '    End If
        '    ''Added this condition for pevious records:Becoz from imsetup if we select blank conceptid then default labconceptid.text was conceptid 
        '    ''so instead of getting blank conceptid it was coming ConceptID:#7490
        '    If _ConceptID.ToString.Trim() = "ConceptID" Then
        '        _ConceptID = ""
        '    End If
        '    If _DescriptionID.ToString.Trim() = "DescriptionID" Then
        '        _DescriptionID = ""
        '    End If
        '    If _SnomedID.ToString.Trim() = "SnoMedID" Then
        '        _SnomedID = ""
        '    End If
        '    objfrmHistory = New frmHistory(GenerateVisitID(Now.Date, _PatientID), DateTime.Now, _sVaccineName, _ConceptID, _SnomedID, _DescriptionID, _ICD9, _SnomedDefination, _RxNormID, _NDCCode, True, _PatientID)
        '    objfrmHistory.MdiParent = Me.ParentForm
        '    objfrmHistory.mycallerImm = Me
        '    objfrmHistory.PopulatePatientHistory_Final()
        '    If objfrmHistory.blncancel = False Then

        '    Else
        '        objfrmHistory.Show()
        '    End If
        'End If
    End Sub


    Private Sub pnlTop_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlTop.Paint

    End Sub

    Private Sub C1FlexTransaction_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1FlexTransaction.Click

    End Sub

    Private Sub pnlImmn_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlImmn.Paint

    End Sub

    Private Sub btn_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnedit.Click
        Dim rowsel As Integer = C1Flex_Transaction.RowSel

        If rowsel > 0 Then
            Clear()
            blnClrAfterImmun = True
            With C1Flex_Transaction
                ''Sanjog-Added on 20101129 to show all immunization name and the the cpt code on edit click 
                If Not IsNothing(.GetData(rowsel, COL_ITEMNAME)) Then
                    SelRowNo = rowsel

                    '' Added item Which is not present in combo box
                    If cmbimmu.Items.Contains(.GetData(rowsel, COL_ITEMNAME)) Then
                        cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                    Else
                        cmbimmu.Sorted = True
                        cmbimmu.Items.Add(.GetData(rowsel, COL_ITEMNAME))
                        cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                    End If
                    ''

                    txtdose.Text = .GetData(rowsel, COL_DOSE)

                    If .GetData(rowsel, COL_DATEGIVEN) <> "" Then
                        cmbdtgiven.Checked = True
                        ''Added on 20110103 by Sanjog 
                        cmbdtgiven.Value = .GetData(rowsel, COL_DATEGIVEN)
                        ''Added on 20110103 by Sanjog 
                    End If

                    txttime.Text = .GetData(rowsel, COL_TIMEGIVEN)
                    cmbroute.Text = .GetData(rowsel, COL_ROUTE)
                    ''Added  By Shweta 20100915
                    txtDoseUnit.Text = .GetData(rowsel, COL_DOSEUNIT)

                    txtlotno.Text = .GetData(rowsel, COL_LOTNUMBER)
                    dtexpdate.Text = .GetData(rowsel, COL_EXPDATE)
                    cmbmanufac.Text = .GetData(rowsel, COL_MANUFACT)
                    txtnotes.Text = .GetData(rowsel, COL_NOTES)
                    cmbduedt.Text = .GetData(rowsel, COL_DUEDATE)
                    '''''''''''''''
                    If .GetData(rowsel, COL_DUEDATE) <> "" Then
                        cmbduedt.Checked = True
                        cmbdtgiven.Checked = False
                    Else
                        cmbdtgiven.Checked = True
                    End If
                    '''''''''''''''
                    Dim _SplReaction() As String

                    If Not IsNothing(.GetData(rowsel, COL_REACTION)) Then
                        ' _SplReaction = .GetData(rowsel, COL_REACTION).ToString().Split(",")
                        _SplReaction = Split(.GetData(rowsel, COL_REACTION).ToString().Trim(), vbNewLine)
                        For i As Integer = 0 To _SplReaction.Count - 1
                            lstReaction.Items.Add(_SplReaction.GetValue(i).ToString().Trim())
                        Next
                        '  cmbreac.Text = .GetData(rowsel, COL_REACTION)
                    End If
                    .GetData(rowsel, COL_REACTIONBTN)
                    cmbreacdt.Text = .GetData(rowsel, COL_REACTIONDT)
                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 22rowsel11rowsel

                    cmbsite.Text = .GetData(rowsel, COL_SITE)
                    '--------
                    cmbcptcode.Text = .GetData(rowsel, COL_CPTCODES)
                    '--------
                    'sarika
                    '  .GetData(rowsel, COL_ISREMINDER)
                    cmbelicode.Text = .GetData(rowsel, COL_VACCINEELIGIBILITYCODE)

                    Dim bool As C1.Win.C1FlexGrid.CheckEnum = .GetCellCheck(rowsel, COL_ISREMINDER)
                    If bool = CheckEnum.Checked Then
                        chkrem.Checked = True
                    Else
                        chkrem.Checked = False
                    End If

                    lblConceptID.Text = .GetData(rowsel, COL_ConceptID)
                    lblDescriptionID.Text = .GetData(rowsel, COL_DescriptionID)
                    lblSnoMedID.Text = .GetData(rowsel, COL_SnomedID)
                End If
                ''Sanjog-Added on 20101129 to show immunization name and the the cpt code in combo on edit click 

                ''Commented by sanjog 
                'If Not IsNothing(.GetData(rowsel, COL_ITEMNAME)) Then
                '    SelRowNo = rowsel
                '    ' .GetData(rowsel, COL_TRNID)
                '    ' .GetData(rowsel, COL_TRNDATE)
                '    '.GetData(rowsel, COL_PATIENTID)
                '    '.GetData(rowsel, COL_VISITID)
                '    '.GetData(rowsel, COL_ITEMID)
                '    '.GetData(rowsel, COL_ITEMNAME)
                '    ' .GetData(rowsel, COL_ITEMCOUNTERID)
                '    txtdose.Text = .GetData(rowsel, COL_DOSE)
                '    cmbdtgiven.Text = .GetData(rowsel, COL_DATEGIVEN)

                '    txttime.Text = .GetData(rowsel, COL_TIMEGIVEN)
                '    cmbroute.Text = .GetData(rowsel, COL_ROUTE)
                '    ''Added By Shweta 20100915 for MU
                '    txtDoseUnit.Text = .GetData(rowsel, COL_DOSEUNIT)
                '    ''END-Added By Shweta 20100915 for MU

                '    txtlotno.Text = .GetData(rowsel, COL_LOTNUMBER)
                '    dtexpdate.Text = .GetData(rowsel, COL_EXPDATE)
                '    cmbmanufac.Text = .GetData(rowsel, COL_MANUFACT)
                '    ' .GetData(rowsel, COL_ISLOCK)
                '    '.GetData(rowsel, COL_USERID)
                '    txtnotes.Text = .GetData(rowsel, COL_NOTES)
                '    cmbduedt.Text = .GetData(rowsel, COL_DUEDATE)

                '    '''''''''''''''
                '    If .GetData(rowsel, COL_DUEDATE) <> "" Then
                '        cmbduedt.Checked = True
                '        cmbdtgiven.Checked = False
                '    End If
                '    '''''''''''''''

                '    Dim _SplReaction() As String

                '    If Not IsNothing(.GetData(rowsel, COL_REACTION)) Then
                '        _SplReaction = .GetData(rowsel, COL_REACTION).ToString().Split(vbNewLine)
                '        lstReaction.Items.Clear()
                '        For lensplreac As Integer = 0 To _SplReaction.Length - 1
                '            If _SplReaction(lensplreac).Trim() <> "" Then
                '                'cmbreac.Items.Add(_SplReaction(lensplreac))
                '                lstReaction.Items.Add(_SplReaction(lensplreac).Trim())
                '            End If
                '        Next
                '        '  cmbreac.Text = .GetData(rowsel, COL_REACTION)
                '    End If
                '    .GetData(rowsel, COL_REACTIONBTN)

                '    cmbreacdt.Text = .GetData(rowsel, COL_REACTIONDT)
                '    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 22rowsel11rowsel




                '    cmbsite.Text = .GetData(rowsel, COL_SITE)
                '    '--------
                '    cmbcptcode.Text = .GetData(rowsel, COL_CPTCODES)
                '    '--------
                '    ' txtusrname.Text = .GetData(rowsel, Col_USERNAME)

                '    'sarika
                '    '  .GetData(rowsel, COL_ISREMINDER)
                '    cmbelicode.Text = .GetData(rowsel, COL_VACCINEELIGIBILITYCODE)
                '    'cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                '    If cmbimmu.Items.Contains(.GetData(rowsel, COL_ITEMNAME)) Then
                '        cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                '    Else
                '        cmbimmu.Sorted = True
                '        cmbimmu.Items.Add(.GetData(rowsel, COL_ITEMNAME))
                '        cmbimmu.Text = .GetData(rowsel, COL_ITEMNAME)
                '    End If

                '    Dim bool As C1.Win.C1FlexGrid.CheckEnum = .GetCellCheck(rowsel, COL_ISREMINDER)
                '    If bool = CheckEnum.Checked Then
                '        chkrem.Checked = True
                '    Else
                '        chkrem.Checked = False
                '    End If
                '    If .GetData(rowsel, COL_ConceptID) <> "" Then
                '        lblConceptID.Text = .GetData(rowsel, COL_ConceptID)
                '    End If

                '    If .GetData(rowsel, COL_DescriptionID) <> "" Then
                '        lblDescriptionID.Text = .GetData(rowsel, COL_DescriptionID)
                '    End If

                '    If .GetData(rowsel, COL_SnomedID) <> "" Then
                '        lblDescriptionID.Text = .GetData(rowsel, COL_SnomedID)
                '    End If

                '    '---------
                '    '.GetData(rowsel, COL_REASONFORNONADMIN)
                '    '.GetData(rowsel, Col_CPTCodesHidden)
                '    '.GetData(rowsel, Col_VaccineCode)
                '    '.GetData(rowsel, Col_ItemName2)
                'End If


            End With
        End If
    End Sub

    Private Sub cmbimmu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbimmu.Click

    End Sub

    Private Sub btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim ronosel As Integer = C1Flex_Transaction.RowSel
        'ArrLst = arrDM
        Dim i As Integer
        With C1Flex_Transaction
            'Dim _HistoryCategory As String = .Rows(.Row).Node.GetNode(NodeTypeEnum.Root).Data
            'Dim _Group As String = .Rows(.Row).Node.GetNode(NodeTypeEnum.Parent).Data
            'Dim _HistoryItem As String = .GetData(ronosel, COL_ITEMNAME).ToString()


            'For i = 0 To ArrLst.Count - 1
            '    If CType(ArrLst(i), myList).DMTemplateName = _HistoryItem Then
            '        ArrLst.RemoveAt(i)
            '        Exit For
            '    End If
            'Next

            If ronosel > 0 Then
                Dim dialogResult As DialogResult
                dialogResult = MessageBox.Show("Do you want to delete Immunization?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If dialogResult = Windows.Forms.DialogResult.Yes Then
                    C1Flex_Transaction.Rows.Remove(ronosel)
                    _isChangesdone = True
                    Clear()
                    SetComboIndex()
                    RemovecheckBox()
                End If
            End If
        End With
    End Sub


    Private Sub btnimmst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimmst.Click

        Dim objfrmsetup As New frmIM_Setup
        objfrmsetup._SaveFlag = True
        objfrmsetup.ShowDialog()
        SelRowNo = -1

        'blnClrAfterImmun = False
        'ClearComboBox()
        'hashtblItemName.Clear()
        'Arry_CPTCODES.Clear()
        'ClearAfterImmun()
        'RemovecheckBox()
        'Loadform()
        FillCMbImmu()
    End Sub

    Private Sub tblCCD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblCCD.Click
        'Code Start - Added by kanchan on 20100601 for CCD
        Try
            Dim objfrm As New frmCCDGenerateList(_PatientID)
            objfrm.ChkImmunization.Checked = True

            With objfrm
                .WindowState = FormWindowState.Normal
                .BringToFront()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
        Catch ex As Exception

        End Try

        'Code End - Added by kanchan on 20100601 for CCD
    End Sub

    Private Sub cmbreac_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbreac.SelectionChangeCommitted
        If cmbreac.Items.Count > 1 Then
            If cmbreac.Text.Trim <> "" Then
                cmbreacdt.Checked = True
            Else
                cmbreacdt.Checked = False
            End If
        Else
            cmbreacdt.Checked = False
        End If

    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub cmbduedt_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbduedt.ValueChanged
        '''''''''''Added by Ujwala
        If cmbduedt.Checked = True Then
            cmbdtgiven.Checked = False
            txttime.Text = ""
        Else
            cmbdtgiven.Checked = True
            txttime.Text = DateTime.Now.ToString("hh:mm:ss tt")
        End If
        '''''''''''Added by Ujwala
    End Sub
    ''Added by Mayuri:20101117-to show tooltip on combo box
    Private Sub ShowTooltipOnComboBox(ByVal sender As Object, ByVal e As DrawItemEventArgs)

        combo = DirectCast(sender, ComboBox)
        If combo.Items.Count > 0 AndAlso e.Index >= 0 Then

            e.DrawBackground()
            Using br As New SolidBrush(e.ForeColor)
                e.Graphics.DrawString(combo.GetItemText(combo.Items(e.Index)).ToString(), e.Font, br, e.Bounds)
            End Using

            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                If combo.DroppedDown Then

                    If getWidthofListItems(combo.GetItemText(combo.Items(e.Index)).ToString(), combo) >= combo.DropDownWidth - 20 Then
                        Me.ToolTip1.Show(combo.GetItemText(combo.Items(e.Index)), combo, e.Bounds.Right - 100, e.Bounds.Bottom + 25)
                    Else

                        Me.ToolTip1.Hide(combo)
                    End If
                Else
                    Me.ToolTip1.Hide(combo)
                End If
            Else
                ToolTip1.Hide(combo)
            End If
            e.DrawFocusRectangle()
        End If
    End Sub

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer

        Dim g As Graphics = Me.CreateGraphics()
        Dim s As SizeF = g.MeasureString(_text, combo.Font)
        Dim width As Integer = Convert.ToInt32(s.Width)
        Return width
    End Function

    Private Sub chkrem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable

        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))

                End If
            End If
        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If


        End Try
    End Function

    Private Sub frmIm_transaction_SaveFunction() Handles Me.SaveFunction
        SaveIM_transaction()
    End Sub
End Class
