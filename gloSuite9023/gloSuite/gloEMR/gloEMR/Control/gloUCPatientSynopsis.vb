
Imports C1.Win.C1FlexGrid
Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloUserControlLibrary
Public Class gloUCPatientSynopsis

    Enum EnmImmediacy
        Acute = 1
        Chronic = 2
        unknown = 3
    End Enum

    Private Const intWidth As Int32 = 120
    Private blnProbExpand As Boolean
    Private pnlProblemsWidth As Int32
    Private pnlProblemsWidthMax As Int32

    Private blnMedExpand As Boolean
    Private pnlMedicationsWidth As Int32
    Private pnlMedicationsWidthMax As Int32

    Private blnHistoryExpand As Boolean
    Private pnlHistoryWidth As Int32
    Private pnlHistoryWidthMax As Int32

    Private blnProcExpand As Boolean
    Private pnlProceduresWidth As Int32
    Private pnlProceduresWidthMax As Int32

    Private blnLabsExpand As Boolean
    Private pnlLabsWidth As Int32
    Private pnlLabsWidthMax As Int32

    Private blnImagingSTExpand As Boolean
    Private pnlImagingSTWidth As Int32
    Private pnlImagingSTWidthMax As Int32

    Private blnImplantExpand As Boolean
    Private pnlImplantWidth As Int32
    Private pnlImplantWidthMax As Int32

    Dim pID As Int64
    Dim c2 As New ClsPatientSynopsis
    ' Dim dt As New DataTable
    Public dtProblemList As DataTable = Nothing ''ProblemList
    Public dtHistory As DataTable = Nothing ''History
    Public dtPatientDetails As DataTable = Nothing ''Medications
    Public dtProcDetails As DataTable = Nothing ''Procedures
    Public dtOrder As DataTable = Nothing ''Oroders
    Public dt_EF As DataTable = Nothing ''EjectionFraction
    Public dtDateofImplantDtls As DataTable = Nothing ''Implant
    Public dtStudyDateDtls As DataTable = Nothing ''Imaging
    Public Event ViewDMS()

    'Incident #55315: 00016572 : Carry forward issue
    Private tempsender As Object
    Private tempE As EventArgs

    'Event for Medication
    Public Event ViewMedications(ByVal PatientId As Int64, ByVal VisitID As Int64)

    'Event for ProblemList
    Public Event ViewProblemList(ByVal PatientID As Int64, ByVal ProblemId As Int64, ByVal VisitID As Int64)

    'Evnet for History
    Public Event ViewPatientHistory(ByVal VisitId As Int64, ByVal Visitdate As Date)


    'Event for labs
    Public Event OnLabsFlexDoubleClick(ByVal PatientID As Long, ByVal OrderID As Long, ByVal VisitID As Long, ByVal TransactionDate As Date)


    'Event for Radiology
    Public Event ViewRadiology(ByVal VisitId As Int64, ByVal Visitdate As Date)

    'Event for Procedures

    'CHANGE THE PARAMETER FOR THE FUCNTION FOR CHECKING THE gblnICD9Driven SETTING OF ADMIN
    Public Event ViewProcedures(ByVal ExamId As Int64, ByVal VisitId As Int64, ByVal dtDOS As DateTime, ByVal ExamName As String, ByVal PatientId As Int64)

    'Event for Stress Test
    Public Event ViewStressTest(ByVal PatientId As Int64, ByVal VisitID As Int64, ByVal DateOfStudy As Date, ByVal ExamID As Int64, ByVal ClinicID As Int64)

    'Event for CardioLogyDevice
    Public Event ViewCardiologyDevice(ByVal PatientId As Int64, ByVal ExamID As Int64, ByVal VisitID As Int64)

    Dim blnProcDate As Boolean = False

    Private WithEvents oSearchProblemListCtl As gloUCGeneralSearch
    Private WithEvents oSearchMedicationsCtl As gloUCGeneralSearch

    '05-May-15 Aniket: Resolving Bug #82983: EMR: Synopsis(PT history)- Search functionlity doe not work properly
    Public WithEvents oSearchAllergiesCtl As gloUCGeneralSearch
    Private WithEvents oSearchProceduresCtl As gloUCGeneralSearch
    Private WithEvents oSearchImagingCtl As gloUCGeneralSearch
    Private WithEvents oSearchEjectionFraction As gloUCGeneralSearch
    Private WithEvents oSearchImplantDevice As gloUCGeneralSearch

    Private _Defaultload As Boolean = True

    Private Const COL_D_CAT_ID = 0 ' ID
    Private Const COL_D_CAT_NAME = 1 ' Name
    Private Const COL_D_CAT_NOTEFLAG = 2 ' Note Flag
    Private Const COL_D_CAT_EXTRAFLAG = 3 ' Extra Col
    Private Const COL_D_CAT_SOURCEMACHINE = 4 ' Source Machine
    Private Const COL_D_CAT_SYSTEMFOLDER = 5 ' System Folder
    Private Const COL_D_CAT_CONTAINER = 6 ' Container
    Private Const COL_D_CAT_CATEGORY = 7 ' Category
    Private Const COL_D_CAT_PATIENTID = 8 ' Patient ID
    Private Const COL_D_CAT_YEAR = 9 ' Year
    Private Const COL_D_CAT_MONTH = 10 ' Month
    Private Const COL_D_CAT_SOURCEBIN = 11 ' Source Bin
    Private Const COL_D_CAT_INUSED = 12 ' In Used
    Private Const COL_D_CAT_USEDMACHINE = 13 ' Used Machine
    Private Const COL_D_CAT_USEDTYPE = 14 ' Used Type
    Private Const COL_D_CAT_PATH = 15 ' Path
    Private Const COL_D_CAT_COLTYPE = 16
    Private Const COL_D_CAT_FILENAME = 17 ' File Name
    Private Const COL_D_CAT_MACHINEID = 18 ' File Name
    Private Const COL_D_CAT_VERSIONNO = 19 ' Version No
    Private Const COL_D_CAT_ISREVIWED = 20 ' Reviwed
    Private Const COL_D_CAT_REVIWEDFLAG = 21 ' Reviwed
    'Private Const COL_D_CAT_COUNT = 22
    Private Const COL_View_CategoryHidden = 22
    Private Const COL_View_Category = 23
    Private Const COL_View_Month = 24
    Private Const COL_View_DocumentName = 25
    Private Const COL_View_NOTEFLAG = 26
    Private Const COL_View_REVIWEDFLAG = 27
    '   Private WithEvents dgPatientDetails As gloEMR.clsDataGrid
    Private Const Col_view_Count = 28


    '' Procedure Diagnosis Grid Constants
    Private Col_sICD9Code As Integer = 0
    Private Col_ICD9Code_Description As Integer = 1
    Private Col_DateOfService As Integer = 2
    Private Col_ICD9Code As Integer = 3
    Private Col_ICD9Desc As Integer = 4
    Private COl_CPTCode As Integer = 5
    Private Col_CPTDesc As Integer = 6
    Private Col_ModCode As Integer = 7
    Private Col_ModDesc As Integer = 8
    Private Col_Units As Integer = 9
    Private Col_DIAEXAMID As Integer = 10
    Private Col_DIAVISITID As Integer = 11

    Private Col_HiddenExamName As Integer = 12
    Private Col_HiddenDOS As Integer = 13
    Private Col_HiddenExFinish As Integer = 14
    'Private Col_HiddenVisitID As Integer = 14

    Private Col_Count = 15
    ''
    'Lab detail control
    Public WithEvents GloUC_TransactionHistory1 As gloUserControlLibrary.gloUC_TransactionHistory
    Dim strDia As String = " "

    Public Event GetPrescription() 'Incident #55315: 00016572 : Carry forward issue
    Public Event ShowMedicationForm(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event ShowHistoryForm(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event ShowProblemListForm(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event ShowRadiologyForm(ByVal visitId As Int64, ByVal orderdate As String)

    '' For Exam Search
    Private m_ExamFilter As Boolean = False
    Private m_ResetFlag As Boolean = False
    ''

    Private Const COLUM_NAME = 0
    Private Const COLUM_IDENTITY = 1
    Private Const COLUM_NUMVALUE = 2
    Private Const COLUM_UNIT = 3
    Private Const COLUM_ID = 4
    Private Const COLUM_TESTGROUPFLAG = 5
    Private Const COLUM_LEVELNO = 6
    Private Const COLUM_GROUPNO = 7
    Private Const COLUM_TEMPLATEID = 8
    ''''
    Private Const COLUM_DIAGNOSIS = 9
    Private Const COLUM_DIAGNOSISBUTTON = 10
    ''''
    Private Const COLUM_BUTTON = 11
    Private Const COLUM_ISFINISHED = 12
    Private Const COLUM_DMSID = 13

    Private Const COLUM_VISITID = 14
    Private Const COLUM_ORDERDATE = 15
    '' No of Columns
    Private Const COLUM_COUNT = 16
    Private blnIsExam As Boolean = True
    Public oMDI As System.Windows.Forms.Form
    Dim cMnuPatient As New ContextMenuStrip
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Private dtStatusFillExamTypeCombo As DataTable

    'Create Property procedure
    Public Property PatientID() As Int64
        Get
            Return pID
        End Get
        Set(ByVal value As Int64)

            pID = value
        End Set
    End Property
    'constructor commented as we are not going to use not parameterised constructor
    'which is required for case UC5070.003
    'Public Sub New()
    '    ' This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    ' Add any initialization after the InitializeComponent() call.
    'End Sub
    Public Sub New(ByVal mPatientID As Int64)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        pID = mPatientID

    End Sub
    Private Sub Get_PatientDetails(ByVal dtPatient As DataTable)

        Try
            'dtPatient = New DataTable

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
    End Sub

   


    Private Sub gloUCPatientSynopsis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        

        Try

            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center

            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center

            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center

            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center


            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center


            btnProbExpand.Visible = False
            btnProcedExpand.Visible = False
            btnMedExpand.Visible = False
            btnLabExpand.Visible = False
            btnImplantExpand.Visible = False
            btnImagSTExpand.Visible = False
            btnHistExpand.Visible = False
            Dim dsdata As DataSet
            dsdata = c2.PopulateSynopsisData(pID, "All")
            Call Get_PatientDetails(dsdata.Tables("PatientInfo"))
            PopulateProblemList1(dsdata.Tables("Problems"))
            ' PopulateLatestMedications1()

            PopulateLatestMedications(dsdata.Tables("Medications"))
            PopulateProcedures1(dsdata.Tables("Procedures"))
            PopulateLatestHistory(dsdata.Tables("History"))
            PopulateLabs(dsdata.Tables("Labs"))
            PopulateImaging(dsdata.Tables("Imaging")) 'from CV_StressTest table
            PopulateImplant(dsdata.Tables("Implant"))
            C1MedicationDetails.Visible = True
            blnIsExam = True





            FillExamDMS()

            'FillExamProviderCombo()
            ResetExamFilterControls()
            PopulateSearchCombo()
            cmbSearch.Text = "User Tag"
            lblsearch.Text = "User Tag : "
            'dgPatientDetails = New clsDataGrid
            'dgPatientDetails.Dock = DockStyle.Fill
            'dgPatientDetails.Visible = True
            'pnlMedications.Controls.Add(dgPatientDetails)
            oSearchProblemListCtl = New gloUCGeneralSearch
            pnlsearchProblems.Controls.Add(oSearchProblemListCtl)
            oSearchProblemListCtl.Dock = DockStyle.Right
            oSearchProblemListCtl.BringToFront()


            oSearchMedicationsCtl = New gloUCGeneralSearch
            pnlSearchMed.Controls.Add(oSearchMedicationsCtl)
            oSearchMedicationsCtl.Dock = DockStyle.Right
            oSearchMedicationsCtl.BringToFront()

            oSearchAllergiesCtl = New gloUCGeneralSearch
            pnlSearchAllergies.Controls.Add(oSearchAllergiesCtl)
            oSearchAllergiesCtl.Dock = DockStyle.Right
            oSearchAllergiesCtl.BringToFront()



            oSearchImplantDevice = New gloUCGeneralSearch
            pnlSearchImplant.Controls.Add(oSearchImplantDevice)
            oSearchImplantDevice.Dock = DockStyle.Right
            oSearchImplantDevice.BringToFront()



            oSearchEjectionFraction = New gloUCGeneralSearch
            pnlSearchEjection.Controls.Add(oSearchEjectionFraction)
            oSearchEjectionFraction.Dock = DockStyle.Right
            oSearchEjectionFraction.BringToFront()


            'oSearchProceduresCtl = New gloUCGeneralSearch
            'oSearchProceduresCtl.Dock = DockStyle.Right
            'oSearchProceduresCtl.BringToFront()
            'pnlSearchProc.Controls.Add(oSearchProceduresCtl)

            'oSearchImagingCtl = New gloUCGeneralSearch
            'oSearchImagingCtl.Dock = DockStyle.Right
            'oSearchImagingCtl.BringToFront()
            'pnlSearchImaging.Controls.Add(oSearchImagingCtl)

            GloUC_TransactionHistory1 = New gloUserControlLibrary.gloUC_TransactionHistory()
            GloUC_TransactionHistory1.Dock = DockStyle.Fill
            GloUC_TransactionHistory1.Visible = True
            '''''''''''''
            GloUC_TransactionHistory1.HideCloseButton = False
            '''''''''''''
            AddHandler GloUC_TransactionHistory1.gUC_ViewDocument, AddressOf gloLabUC_Transaction1_gUC_ViewDocument
            pnlLabDetails.Controls.Add(GloUC_TransactionHistory1)

            'Remove orders tab

            ''
            'Dim nwidth As Int16
            'nwidth = pnlSummary.Width
            'Dim splitterwidth As Int32 = 0
            'splitterwidth = spltHistory.Width * 7
            'Dim TotalWidth As Int16

            'TotalWidth = pnlProblems.Width + pnlMedications.Width + pnlHistory.Width + pnlProcedures.Width + pnlLabs.Width + pnlImagingST.Width
            'nwidth = nwidth / 7
            'pnlImplant.Width = nwidth - splitterwidth - TotalWidth

            'pnlProblems.Width = nwidth
            'pnlMedications.Width = nwidth
            'pnlHistory.Width = nwidth
            'pnlProcedures.Width = nwidth
            'pnlLabs.Width = nwidth
            'pnlImagingST.Width = nwidth
            'pnlImplant.Width = nwidth
            'pnlImaging.SendToBack()
            pnlImaging.Width = 0

            pnlImplant.Dock = DockStyle.Fill
            pnlProblemsWidth = pnlProblems.Width
            pnlMedicationsWidth = pnlMedications.Width
            pnlLabsWidth = pnlLabs.Width
            pnlProceduresWidth = pnlProcedures.Width
            pnlImplantWidth = pnlImplant.Width
            pnlImagingSTWidth = pnlImagingST.Width
            pnlHistoryWidth = pnlHistory.Width

           
            If pnlImplantWidth = 0 Then
                pnlImplantWidthMax = pnlImplant.Width
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function getWidthofListItems(ByVal _text As String, ByVal combo As ComboBox) As Integer 'code review changes 
        Dim width As Integer = 0
        Dim g As Graphics = Me.CreateGraphics()
        If Not g Is Nothing Then
            Dim s As SizeF = g.MeasureString(_text, combo.Font)
            width = Convert.ToInt32(s.Width)
            'Dispose graphics object
            g.Dispose()
        End If
        Return width
    End Function

    Private Sub gloLabUC_Transaction1_gUC_ViewDocument(TestID As Long, DocumentID As Long) Handles GloUC_TransactionHistory1.gUC_ViewDocument
        Try
            Dim oViewDocument As gloEDocumentV3.gloEDocV3Management = Nothing
            If DocumentID > 0 Then
                If oViewDocument Is Nothing Then
                    oViewDocument = New gloEDocumentV3.gloEDocV3Management()
                End If


                oViewDocument.oPatientExam = New clsPatientExams
                oViewDocument.oPatientMessages = New clsMessage
                oViewDocument.oPatientLetters = New clsPatientLetters
                oViewDocument.oNurseNotes = New clsNurseNotes
                oViewDocument.oHistory = New clsPatientHistory
                oViewDocument.oLabs = New clsLabs
                oViewDocument.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                oViewDocument.oRxmed = New clsPatientDetails
                oViewDocument.oOrders = New clsPatientDetails
                oViewDocument.oProblemList = New clsPatientProblemList

                oViewDocument.oCriteria = New DocumentCriteria
                oViewDocument.oWord = New gloEMRWord.clsWordDocument
                Dim isItDialog As Boolean = oViewDocument.ShowEDocument(pID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, Nothing, gloEDocumentV3.Enumeration.enum_OpenExternalSource.DashBoard, DocumentID)
                If (isItDialog = True) Then


                    If (IsNothing(oViewDocument.oPatientExam) = False) Then
                        DirectCast(oViewDocument.oPatientExam, clsPatientExams).Dispose()
                        oViewDocument.oPatientExam = Nothing
                    End If
                    If (IsNothing(oViewDocument.oPatientMessages) = False) Then
                        DirectCast(oViewDocument.oPatientMessages, clsMessage).Dispose()
                        oViewDocument.oPatientMessages = Nothing
                    End If
                    If (IsNothing(oViewDocument.oPatientLetters) = False) Then
                        DirectCast(oViewDocument.oPatientLetters, clsPatientLetters).Dispose()
                        oViewDocument.oPatientLetters = Nothing
                    End If
                    If (IsNothing(oViewDocument.oNurseNotes) = False) Then
                        DirectCast(oViewDocument.oNurseNotes, clsNurseNotes).Dispose()
                        oViewDocument.oNurseNotes = Nothing
                    End If
                    If (IsNothing(oViewDocument.oHistory) = False) Then
                        DirectCast(oViewDocument.oHistory, clsPatientHistory).Dispose()
                        oViewDocument.oHistory = Nothing
                    End If
                    If (IsNothing(oViewDocument.oLabs) = False) Then
                        DirectCast(oViewDocument.oLabs, clsLabs).Dispose()
                        oViewDocument.oLabs = Nothing
                    End If
                    If (IsNothing(oViewDocument.oDMS) = False) Then
                        DirectCast(oViewDocument.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                        oViewDocument.oDMS = Nothing
                    End If
                    If (IsNothing(oViewDocument.oRxmed) = False) Then
                        DirectCast(oViewDocument.oRxmed, clsPatientDetails).Dispose()
                        oViewDocument.oRxmed = Nothing
                    End If
                    If (IsNothing(oViewDocument.oOrders) = False) Then
                        DirectCast(oViewDocument.oOrders, clsPatientDetails).Dispose()
                        oViewDocument.oOrders = Nothing
                    End If
                    If (IsNothing(oViewDocument.oProblemList) = False) Then
                        DirectCast(oViewDocument.oProblemList, clsPatientProblemList).Dispose()
                        oViewDocument.oProblemList = Nothing
                    End If

                    If (IsNothing(oViewDocument.oCriteria) = False) Then

                        oViewDocument.oCriteria = Nothing
                    End If

                    oViewDocument.Dispose()
                End If
                oViewDocument = Nothing
            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub
    Public Sub PopulateLatestMedications(ByVal _table As DataTable)

        Try

            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    trMedications.Nodes.Clear()
                    Dim mynode As New TreeNode("Medications")
                    mynode.ImageIndex = 2
                    mynode.SelectedImageIndex = 2
                    trMedications.Nodes.Add(mynode)
                    Dim icnt As Int32 = 0
                    Dim strMaxlength As String = ""
                    For icnt = 0 To _table.Rows.Count - 1
                        Dim mychildNode As New TreeNode
                        mychildNode.Text = _table.Rows(icnt)("sMedication") & " " & _table.Rows(icnt)("sDosage") & " " & _table.Rows(icnt)("dtMedicationDate")
                        If Len(strMaxlength) < mychildNode.Text.Length Then
                            strMaxlength = mychildNode.Text
                        End If
                        mychildNode.Tag = _table.Rows(icnt)("VisitID")
                        mychildNode.ImageIndex = 7
                        mychildNode.SelectedImageIndex = 7
                        mynode.Nodes.Add(mychildNode)
                    Next
                    SetPanelWidth(trMedications, pnlMedicationsWidthMax, strMaxlength, pnlMedicationsWidth)
                End If
            End If

            trMedications.Sort()
            trMedications.ExpandAll()
            If Not IsNothing(trMedications.Nodes) Then
                If trMedications.Nodes.Count > 0 Then
                    trMedications.SelectedNode = trMedications.Nodes(0)
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
        Finally
            If Not IsNothing(_table) Then
                _table.Dispose()
                _table = Nothing
            End If
            
        End Try


    End Sub

    Private Sub PopulateSearchCombo()

        Try
            cmbSearch.Items.Add("User Tag")
            cmbSearch.Items.Add("Notes")
            cmbSearch.Items.Add("Acknowledge")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub FillExamDMS()
        ''dhruv 20091130
        ''to do faster operation
        C1PatientDMS.BeginInit()
        Try
            If blnIsExam Then
                '' Comment on 20090212- Control Change to C1FlexGrid
                'Dim dtPatientDetails As DataTable
                'Dim objPatientDetail As New clsPatientDetails
                'If m_ExamFilter Then
                '    dtPatientDetails = objPatientDetail.Fill_PastExams(pID, cmbExamProvider.SelectedValue, dtpFrom.Value, dtpTo.Value, cmbExamtype.SelectedValue)
                'Else
                '    dtPatientDetails = objPatientDetail.Fill_PastExams(pID)
                'End If

                ''dgExams = Nothing
                'dgExams.Enabled = False
                'dgExams.DataSource = dtPatientDetails.DefaultView
                'dgExams.Enabled = True
                'dgExams.TableStyles.Clear()
                'Dim grdColStyleExamID As New DataGridTextBoxColumn

                'With grdColStyleExamID
                '    .HeaderText = "Exam ID"
                '    .Alignment = HorizontalAlignment.Left
                '    .MappingName = dtPatientDetails.Columns(0).ColumnName
                '    .NullText = ""
                '    .Width = 0
                'End With
                'Dim grdColStyleVisitID As New DataGridTextBoxColumn
                'With grdColStyleVisitID
                '    .HeaderText = "Visit ID"
                '    .Alignment = HorizontalAlignment.Left
                '    .MappingName = dtPatientDetails.Columns(1).ColumnName
                '    .NullText = ""
                '    .Width = 0
                'End With

                'Dim grdColStyleDate As New DataGridTextBoxColumn

                'With grdColStyleDate
                '    .HeaderText = "DOS"
                '    .Alignment = HorizontalAlignment.Left
                '    .MappingName = dtPatientDetails.Columns(2).ColumnName
                '    .NullText = ""
                '    .Width = 0.1 * C1dgPatientDetails.Width
                'End With

                'Dim grdColStyleExamName As New DataGridTextBoxColumn
                'With grdColStyleExamName
                '    .HeaderText = "Exam Name"
                '    .Alignment = HorizontalAlignment.Left
                '    .MappingName = dtPatientDetails.Columns(3).ColumnName
                '    .NullText = ""
                '    .Width = 0.32 * C1dgPatientDetails.Width
                'End With

                'Dim grdColStyleFinished As New DataGridTextBoxColumn
                'With grdColStyleFinished
                '    .HeaderText = "Finished"
                '    .Alignment = HorizontalAlignment.Left
                '    .MappingName = dtPatientDetails.Columns(4).ColumnName
                '    .NullText = ""
                '    .Width = 0.1 * C1dgPatientDetails.Width - 5
                'End With

                'Dim grdColStyleProvider As New DataGridTextBoxColumn
                'With grdColStyleProvider
                '    .HeaderText = "Provider"
                '    .Alignment = HorizontalAlignment.Left
                '    .MappingName = dtPatientDetails.Columns("ProviderName").ColumnName
                '    .NullText = ""
                '    .Width = 0.18 * C1dgPatientDetails.Width - 5
                'End With

                'Dim grdColStyleReview As New DataGridTextBoxColumn
                'With grdColStyleReview
                '    .HeaderText = "Reviewed By"
                '    .Alignment = HorizontalAlignment.Left
                '    .MappingName = dtPatientDetails.Columns("ReviewedBy").ColumnName
                '    .NullText = ""
                '    .Width = 0.3 * C1dgPatientDetails.Width - 5
                'End With
                'dgExams.TableStyles.Clear()
                'Dim grdTableStyle As New clsDataGridTableStyle(dtPatientDetails.TableName)
                'grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleExamID, grdColStyleVisitID, grdColStyleDate, grdColStyleExamName, grdColStyleFinished, grdColStyleProvider, grdColStyleReview})
                'dgExams.TableStyles.Add(grdTableStyle)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Past Exam viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)
                '' Comment on 20090212- Control Change to C1FlexGrid

                Fill_PastExams()
            Else

                Fill_PatientSacnedDocuments(pID)

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        C1PatientDMS.EndInit()

    End Sub

    Public Sub PopulateImplant(ByVal dtDateofImplant As DataTable)

        ' Dim dtDateofImplant As DataTable = Nothing
        Dim dtCPT As DataTable = Nothing
        Dim dtProcedures As DataTable = Nothing
        Dim dtProductInfo As DataTable = Nothing
        Dim dtDeviceType As DataTable = Nothing
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try
            SetImplantGridStyle()


            Dim iDOS As Int16
            Dim iCPT As Int16
            Dim iProcedure As Int16
            'Dim iProductInfo As Int16
            Dim iDeviceType As Int16




            Dim strCptQry As String
            Dim strProcedureQry As String
            Dim strProductInfoQry As String
            Dim strDeviceTypeQry As String

            Dim strconcatCPT1 As String = ""
            'Dim nextRow As Integer
            Dim strCombine As String = ""


            Dim oDateNode As TreeNode
            Dim oCPTNode As TreeNode
            Dim oProcedureNode As TreeNode
            Dim oTypeOfDeviceNode As TreeNode
            Dim oDeviceTypeNode As TreeNode
            Dim oTempNode As TreeNode



            'strdtQry = "SELECT Distinct isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0) as nClinicID,dtDateofImplant as DateOfImplant FROM CV_CardiologyDevice WHERE nPatientID='" & pID & "'  order by DateOfImplant"
            'oDB.Connect(GetConnectionString)
            'dtDateofImplant = oDB.ReadQueryDataTable(strdtQry)


            With dtDateofImplant
                If Not IsNothing(dtDateofImplant) Then
                    If dtDateofImplant.Rows.Count > 0 Then
                        trImplant.Nodes.Clear()
                        trImplant.Nodes.Add("Implant")
                        trImplant.Nodes.Item(0).ImageIndex = 11
                        trImplant.Nodes.Item(0).SelectedImageIndex = 11

                        For iDOS = 0 To dtDateofImplant.Rows.Count - 1
                            Dim strDateofImplant As String = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID")
                            oDateNode = New TreeNode()
                            oDateNode.Text = dtDateofImplant.Rows(iDOS)("DateofImplant")
                            oDateNode.Tag = strDateofImplant ''dtDateofImplant.Rows(iDOS)("DateofImplant")
                            oDateNode.ImageIndex = 13
                            oDateNode.SelectedImageIndex = 13

                            trImplant.Nodes(0).Nodes.Add(oDateNode)


                            Dim dtImplantDate As Date = Convert.ToDateTime(dtDateofImplant.Rows(iDOS)("DateofImplant")).ToShortDateString()


                            '' CPT Code ''
                            oCPTNode = New TreeNode()
                            oCPTNode.Text = "CPT Code"
                            oCPTNode.Tag = strDateofImplant ''"CPT Code"
                            oCPTNode.ImageIndex = 14
                            oCPTNode.SelectedImageIndex = 14
                            oDateNode.Nodes.Add(oCPTNode)

                            strCptQry = "SELECT DISTINCT isnull(sCPTCode,'') as CPTCode from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sCPTCode<>''"
                            oDB.Connect(GetConnectionString)
                            dtCPT = oDB.ReadQueryDataTable(strCptQry)

                            If (IsNothing(dtCPT) = False) Then


                                For iCPT = 0 To dtCPT.Rows.Count - 1
                                    Dim strCPT As String = dtCPT.Rows(iCPT)("CPTCode")
                                    If strCPT <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = dtCPT.Rows(iCPT)("CPTCode")
                                        oTempNode.Tag = strDateofImplant ''dtCPT.Rows(iCPT)("CPTCode")
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7
                                        oCPTNode.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtCPT) = False) Then
                                    dtCPT.Dispose()
                                    dtCPT = Nothing
                                End If
                            End If


                            '' Procedures ''
                            oProcedureNode = New TreeNode()
                            oProcedureNode.Text = "Procedure"
                            oProcedureNode.Tag = strDateofImplant ''"Procedure"
                            oProcedureNode.ImageIndex = 15
                            oProcedureNode.SelectedImageIndex = 15
                            oDateNode.Nodes.Add(oProcedureNode)

                            strProcedureQry = "SELECT DISTINCT isnull(sProcedures,'') as Procedures from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sProcedures<>'' "
                            oDB.Connect(GetConnectionString)
                            dtProcedures = oDB.ReadQueryDataTable(strProcedureQry)

                            If (IsNothing(dtProcedures) = False) Then


                                For iProcedure = 0 To dtProcedures.Rows.Count - 1
                                    Dim strProcedure As String = dtProcedures.Rows(iProcedure)("Procedures")
                                    If strProcedure <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = dtProcedures.Rows(iProcedure)("Procedures")
                                        oTempNode.Tag = strDateofImplant ''dtProcedures.Rows(iProcedure)("Procedures")
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7
                                        oProcedureNode.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtProcedures) = False) Then
                                    dtProcedures.Dispose()
                                    dtProcedures = Nothing
                                End If
                            End If

                            '' Product Information ''
                            strProductInfoQry = "SELECT DISTINCT isnull(sProductName,'') as sProductName,isnull(sDeviceManufacturer,'') as sDeviceManufacturer,isnull(sProductSpecification,'') as sProductSpecification,isnull(sProductSerialNo,'') as sProductSerialNo," _
                            & " isnull(sManufacturerModelNo,'') as sManufacturerModelNo,isnull(sLeadType,'') as sLeadType,dtDateRemoved , isnull(sPhysicalLocation,'') as sPhysicalLocation , isnull(sLeadLocation,'') as sLeadLocation , " _
                            & " isnull(sThresholdAtrial,'') as sThresholdAtrial,isnull(sThresholdVentricular,'') as sThresholdVentricular, isnull(sSensingAtrial,'') as sSensingAtrial,isnull(sSensingVentricular,'') as sSensingVentricular,isnull(sImpedenceAtrial,'') as sImpedenceAtrial," _
                            & " isnull(sImpedenceVentricular,'') as sImpedenceVentricular from CV_CardiologyDevice " _
                            & " where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' "

                            oDB.Connect(GetConnectionString)
                            dtProductInfo = oDB.ReadQueryDataTable(strProductInfoQry)
                            If (IsNothing(dtProductInfo) = False) Then



                                If Not IsNothing(dtProductInfo.Rows(0)("sPhysicalLocation").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sPhysicalLocation").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Physical Location : " & dtProductInfo.Rows(0)("sPhysicalLocation")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sPhysicalLocation")
                                        oTempNode.ImageIndex = 29
                                        oTempNode.SelectedImageIndex = 29
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If



                                If Not IsNothing(dtProductInfo.Rows(0)("sLeadLocation").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sLeadLocation").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Lead Location : " & dtProductInfo.Rows(0)("sLeadLocation")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sLeadLocation")
                                        oTempNode.ImageIndex = 28
                                        oTempNode.SelectedImageIndex = 28
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If


                                If Not IsNothing(dtProductInfo.Rows(0)("sThresholdAtrial").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sThresholdAtrial").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Threshold Atrial : " & dtProductInfo.Rows(0)("sThresholdAtrial")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sThresholdAtrial")
                                        oTempNode.ImageIndex = 30
                                        oTempNode.SelectedImageIndex = 30
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If


                                If Not IsNothing(dtProductInfo.Rows(0)("sThresholdVentricular").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sThresholdVentricular").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Threshold Ventricular : " & dtProductInfo.Rows(0)("sThresholdVentricular")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sThresholdVentricular")
                                        oTempNode.ImageIndex = 31
                                        oTempNode.SelectedImageIndex = 31
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If


                                If Not IsNothing(dtProductInfo.Rows(0)("sSensingAtrial").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sSensingAtrial").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Sensing Atrial : " & dtProductInfo.Rows(0)("sSensingAtrial")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sSensingAtrial")
                                        oTempNode.ImageIndex = 32
                                        oTempNode.SelectedImageIndex = 32
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If


                                If Not IsNothing(dtProductInfo.Rows(0)("sSensingVentricular").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sSensingVentricular").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Sensing Ventricular : " & dtProductInfo.Rows(0)("sSensingVentricular")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sSensingVentricular")
                                        oTempNode.ImageIndex = 33
                                        oTempNode.SelectedImageIndex = 33
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If


                                If Not IsNothing(dtProductInfo.Rows(0)("sImpedenceAtrial").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sImpedenceAtrial").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Impedence Atrial : " & dtProductInfo.Rows(0)("sImpedenceAtrial")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sImpedenceAtrial")
                                        oTempNode.ImageIndex = 34
                                        oTempNode.SelectedImageIndex = 34
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If


                                If Not IsNothing(dtProductInfo.Rows(0)("sImpedenceVentricular").ToString()) Then
                                    If (dtProductInfo.Rows(0)("sImpedenceVentricular").ToString() <> "") Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = "Impedence Ventricular : " & dtProductInfo.Rows(0)("sImpedenceVentricular")
                                        oTempNode.Tag = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID") ''dtProductInfo.Rows(iProdInfo)("sImpedenceVentricular")
                                        oTempNode.ImageIndex = 35
                                        oTempNode.SelectedImageIndex = 35
                                        ''oDeviceTypeNode.Nodes.Add(oTempNode)
                                        oDateNode.Nodes.Add(oTempNode)
                                    End If
                                End If

                            End If
                            If (IsNothing(dtProductInfo) = False) Then
                                dtProductInfo.Dispose()
                                dtProductInfo = Nothing
                            End If
                            '' Type Of Device ''
                            oTypeOfDeviceNode = New TreeNode()
                            oTypeOfDeviceNode.Text = " Type Of Device "
                            oTypeOfDeviceNode.Tag = strDateofImplant ''"Type Of Device"
                            oTypeOfDeviceNode.ImageIndex = 26
                            oTypeOfDeviceNode.SelectedImageIndex = 26
                            ''oDateNode.Nodes.Add(oTypeOfDeviceNode)


                            '' Query for selecting Device Type ''
                            strDeviceTypeQry = "SELECT DISTINCT isnull(sDeviceType,'') as DeviceType from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sDeviceType<>'' "
                            oDB.Connect(GetConnectionString)
                            dtDeviceType = oDB.ReadQueryDataTable(strDeviceTypeQry)

                            If dtDeviceType IsNot Nothing Then

                                For iDeviceType = 0 To dtDeviceType.Rows.Count - 1
                                    Dim strDeviceType As String = dtDeviceType.Rows(iDeviceType)("DeviceType")
                                    Dim strDeviceT As String = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID")
                                    If strDeviceType <> "" Then
                                        oDeviceTypeNode = New TreeNode
                                        oDeviceTypeNode.Text = dtDeviceType.Rows(iDeviceType)("DeviceType")
                                        oDeviceTypeNode.Tag = strDeviceT ''dtDeviceType.Rows(iDeviceType)("DeviceType")
                                        oDeviceTypeNode.ImageIndex = 27
                                        oDeviceTypeNode.SelectedImageIndex = 27

                                        oTypeOfDeviceNode.Nodes.Add(oDeviceTypeNode)
                                        ''oDateNode.Nodes(0).Nodes.Add(oDeviceTypeNode)

                                        '' Product Information ''
                                        strProductInfoQry = "SELECT DISTINCT isnull(sProductName,'') as sProductName,isnull(sDeviceManufacturer,'') as sDeviceManufacturer,isnull(sProductSpecification,'') as sProductSpecification,isnull(sProductSerialNo,'') as sProductSerialNo," _
                                        & " isnull(sManufacturerModelNo,'') as sManufacturerModelNo,isnull(sLeadType,'') as sLeadType,dtDateRemoved , isnull(sPhysicalLocation,'') as sPhysicalLocation , isnull(sLeadLocation,'') as sLeadLocation , " _
                                        & " isnull(sThresholdAtrial,'') as sThresholdAtrial,isnull(sThresholdVentricular,'') as sThresholdVentricular, isnull(sSensingAtrial,'') as sSensingAtrial,isnull(sSensingVentricular,'') as sSensingVentricular,isnull(sImpedenceAtrial,'') as sImpedenceAtrial," _
                                        & " isnull(sImpedenceVentricular,'') as sImpedenceVentricular from CV_CardiologyDevice " _
                                        & " where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' " _
                                        & " and sDeviceType='" & oDeviceTypeNode.Text.Replace("'", "''") & "'"

                                        oDB.Connect(GetConnectionString)
                                        dtProductInfo = oDB.ReadQueryDataTable(strProductInfoQry)

                                        If dtProductInfo IsNot Nothing Then
                                            For iProdInfo As Integer = 0 To dtProductInfo.Rows.Count - 1
                                                Dim strProductI As String = dtDateofImplant.Rows(iDOS)("DateofImplant") & "|" & dtDateofImplant.Rows(iDOS)("nPatientID") & "|" & dtDateofImplant.Rows(iDOS)("nExamID") & "|" & dtDateofImplant.Rows(iDOS)("nVisitID") & "|" & dtDateofImplant.Rows(iDOS)("nClinicID")

                                                If Not IsNothing(dtProductInfo.Rows(iProdInfo)("sProductName").ToString()) Then
                                                    If (dtProductInfo.Rows(iProdInfo)("sProductName").ToString() <> "") Then
                                                        oTempNode = New TreeNode
                                                        oTempNode.Text = "Product Name : " & dtProductInfo.Rows(iProdInfo)("sProductName")
                                                        oTempNode.Tag = strProductI ''dtProductInfo.Rows(iProdInfo)("sProductName")
                                                        oTempNode.ImageIndex = 7
                                                        oTempNode.SelectedImageIndex = 7
                                                        oDeviceTypeNode.Nodes.Add(oTempNode)
                                                    End If
                                                End If



                                                If Not IsNothing(dtProductInfo.Rows(iProdInfo)("sDeviceManufacturer").ToString()) Then
                                                    If (dtProductInfo.Rows(iProdInfo)("sDeviceManufacturer").ToString() <> "") Then
                                                        oTempNode = New TreeNode
                                                        oTempNode.Text = "Device Manufacturer : " & dtProductInfo.Rows(iProdInfo)("sDeviceManufacturer")
                                                        oTempNode.Tag = strProductI ''dtProductInfo.Rows(iProdInfo)("sDeviceManufacturer")
                                                        oTempNode.ImageIndex = 7
                                                        oTempNode.SelectedImageIndex = 7
                                                        oDeviceTypeNode.Nodes.Add(oTempNode)
                                                    End If
                                                End If



                                                If Not IsNothing(dtProductInfo.Rows(iProdInfo)("sProductSpecification").ToString()) Then
                                                    If (dtProductInfo.Rows(iProdInfo)("sProductSpecification").ToString() <> "") Then
                                                        oTempNode = New TreeNode
                                                        oTempNode.Text = "Product Specification : " & dtProductInfo.Rows(iProdInfo)("sProductSpecification")
                                                        oTempNode.Tag = strProductI ''dtProductInfo.Rows(iProdInfo)("sProductSpecification")
                                                        oTempNode.ImageIndex = 7
                                                        oTempNode.SelectedImageIndex = 7
                                                        oDeviceTypeNode.Nodes.Add(oTempNode)
                                                    End If
                                                End If



                                                If Not IsNothing(dtProductInfo.Rows(iProdInfo)("sProductSerialNo").ToString()) Then
                                                    If (dtProductInfo.Rows(iProdInfo)("sProductSerialNo").ToString() <> "") Then
                                                        oTempNode = New TreeNode
                                                        oTempNode.Text = "Product Serial No : " & dtProductInfo.Rows(iProdInfo)("sProductSerialNo")
                                                        oTempNode.Tag = strProductI ''dtProductInfo.Rows(iProdInfo)("sProductSerialNo")
                                                        oTempNode.ImageIndex = 7
                                                        oTempNode.SelectedImageIndex = 7
                                                        oDeviceTypeNode.Nodes.Add(oTempNode)
                                                    End If
                                                End If


                                                If Not IsNothing(dtProductInfo.Rows(iProdInfo)("sManufacturerModelNo").ToString()) Then
                                                    If (dtProductInfo.Rows(iProdInfo)("sManufacturerModelNo").ToString() <> "") Then
                                                        oTempNode = New TreeNode
                                                        oTempNode.Text = "Manufacturer Model No : " & dtProductInfo.Rows(iProdInfo)("sManufacturerModelNo")
                                                        oTempNode.Tag = strProductI ''dtProductInfo.Rows(iProdInfo)("sManufacturerModelNo")
                                                        oTempNode.ImageIndex = 7
                                                        oTempNode.SelectedImageIndex = 7
                                                        oDeviceTypeNode.Nodes.Add(oTempNode)
                                                    End If
                                                End If


                                                If Not IsNothing(dtProductInfo.Rows(iProdInfo)("sLeadType").ToString()) Then
                                                    If (dtProductInfo.Rows(iProdInfo)("sLeadType").ToString() <> "") Then
                                                        oTempNode = New TreeNode
                                                        oTempNode.Text = "Lead Type : " & dtProductInfo.Rows(iProdInfo)("sLeadType")
                                                        oTempNode.Tag = strProductI ''dtProductInfo.Rows(iProdInfo)("sLeadType")
                                                        oTempNode.ImageIndex = 7
                                                        oTempNode.SelectedImageIndex = 7
                                                        oDeviceTypeNode.Nodes.Add(oTempNode)
                                                    End If
                                                End If



                                                If Not IsNothing(dtProductInfo.Rows(iProdInfo)("dtDateRemoved").ToString()) Then
                                                    If (dtProductInfo.Rows(iProdInfo)("dtDateRemoved").ToString() <> "") Then
                                                        oTempNode = New TreeNode
                                                        oTempNode.Text = "Date Removed : " & dtProductInfo.Rows(iProdInfo)("dtDateRemoved")
                                                        oTempNode.Tag = strProductI ''dtProductInfo.Rows(iProdInfo)("dtDateRemoved")
                                                        oTempNode.ImageIndex = 7
                                                        oTempNode.SelectedImageIndex = 7
                                                        oDeviceTypeNode.Nodes.Add(oTempNode)
                                                    End If
                                                End If


                                                oDateNode.Nodes.Add(oTypeOfDeviceNode)
                                            Next  ''For iProdInfo As Integer = 0 To dtProductInfo.Rows.Count - 1
                                            If (IsNothing(dtProductInfo) = False) Then
                                                dtProductInfo.Dispose()
                                                dtProductInfo = Nothing
                                            End If
                                        End If
                                    End If
                                Next  ''For iDeviceType = 0 To dtDeviceType.Rows.Count - 1
                                If (IsNothing(dtDeviceType) = False) Then
                                    dtDeviceType.Dispose()
                                    dtDeviceType = Nothing
                                End If
                            End If
                        Next   ''For iDOS = 0 To dtDateofImplant.Rows.Count - 1
                    End If
                    If (IsNothing(dtDateofImplant) = False) Then
                        dtDateofImplant.Dispose()
                        dtDateofImplant = Nothing
                    End If
                End If   ''If Not IsNothing(dtDateofImplant) Then

                'trImplant.Sort()
                trImplant.ExpandAll()

            End With




            If Not IsNothing(trImplant.Nodes) Then
                If trImplant.Nodes.Count > 0 Then
                    trImplant.SelectedNode = trImplant.Nodes(0)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If (IsNothing(dtDateofImplant) = False) Then
                dtDateofImplant.Dispose()
                dtDateofImplant = Nothing
            End If

            If (IsNothing(dtCPT) = False) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If

            If (IsNothing(dtProcedures) = False) Then
                dtProcedures.Dispose()
                dtProcedures = Nothing
            End If

            If (IsNothing(dtProductInfo) = False) Then
                dtProductInfo.Dispose()
                dtProductInfo = Nothing
            End If

            If (IsNothing(dtDeviceType) = False) Then
                dtDeviceType.Dispose()
                dtDeviceType = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub


    Public Sub PopulateImaging(ByVal dtStudyDate As DataTable)


        '  Dim dtStudyDate As DataTable = Nothing
        Dim dtCPT As DataTable = Nothing
        Dim dtTestType As DataTable = Nothing
        Dim dtUsers As DataTable = Nothing
        Dim dtRestingHeartRate As DataTable = Nothing
        Dim dtPeakHeartRate As DataTable = Nothing
        Dim dtNarrativeSummary As DataTable = Nothing
        'Dim dtResult As DataTable
        Dim dtTotalExerciseTime As DataTable = Nothing
        Dim dtEjectionFraction As DataTable = Nothing
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        Try
            'Dim dtDOS As DataTable


            Dim mDOS As Int16
            Dim mCPT As Int16
            Dim mTestType As Int16
            Dim mPhysician As Int16
            Dim mRestingHeartRate As Int16
            Dim mPeakHeartRate As Int16
            Dim mNarrativeSummary As Int16
            'Dim mResult As Int16
            Dim mTotalExerciseTime As Int16
            Dim mEjectionFraction As Int16

            ' Dim strdtQry As String
            Dim strCptQry As String
            Dim strCPTTestTypeQry As String
            Dim strPhysicianQry As String
            Dim strRestingHeartRateQry As String
            Dim strPeakHeartRateQry As String
            Dim strNarrativeSummaryQry As String
            'Dim strResultQry As String
            Dim strTotalExerciseTimeQry As String
            Dim strEjectionFractionQry As String
            Dim strCombine As String = ""


            Dim nDate As TreeNode = Nothing
            Dim nCPTCode As TreeNode = Nothing
            Dim nCPTCodedType As TreeNode = Nothing
            Dim nTestType As TreeNode = Nothing
            Dim nCPTTestType As TreeNode = Nothing
            Dim nPhysician As TreeNode = Nothing
            Dim nRestingHeartRate As TreeNode = Nothing
            Dim nPeakHeartRate As TreeNode = Nothing
            Dim nNarrativeSummary As TreeNode = Nothing
            'Dim nResult As TreeNode
            Dim nTotalExerciseTime As TreeNode = Nothing
            Dim nEjectionFraction As TreeNode = Nothing
            Dim oTempNode As TreeNode = Nothing


            'strdtQry = "SELECT Distinct isnull(nStressID,0) as nStressID,isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0) as nClinicID,dtDateOfStudy as DateofStudy FROM CV_StressTest WHERE nGroupID=0 AND nPatientID='" & pID & "' order by DateofStudy"
            'oDB.Connect(GetConnectionString)
            'dtStudyDate = oDB.ReadQueryDataTable(strdtQry)


            With dtStudyDate
                If Not IsNothing(dtStudyDate) Then
                    If dtStudyDate.Rows.Count > 0 Then
                        trImagingST.Nodes.Clear()
                        trImagingST.Nodes.Add("Stress Test")
                        trImagingST.Nodes.Item(0).ImageIndex = 12
                        trImagingST.Nodes.Item(0).SelectedImageIndex = 12


                        For mDOS = 0 To dtStudyDate.Rows.Count - 1
                            Dim strMaxLength As String = ""
                            Dim strStudy As String = dtStudyDate.Rows(mDOS)("DateofStudy") & "|" & dtStudyDate.Rows(mDOS)("nPatientID") & "|" & dtStudyDate.Rows(mDOS)("nExamID") & "|" & dtStudyDate.Rows(mDOS)("nVisitID") & "|" & dtStudyDate.Rows(mDOS)("nClinicID")
                            nDate = New TreeNode()
                            nDate.Text = dtStudyDate.Rows(mDOS)("DateofStudy")
                            nDate.Tag = strStudy ''dtStudyDate.Rows(mDOS)("DateOfStudy")
                            nDate.ImageIndex = 13
                            nDate.SelectedImageIndex = 13

                            trImagingST.Nodes(0).Nodes.Add(nDate)

                            nCPTCode = New TreeNode()
                            nCPTCode.Text = "CPT Code"
                            nCPTCode.Tag = "CPT Code"
                            nCPTCode.ImageIndex = 14
                            nCPTCode.SelectedImageIndex = 14
                            nDate.Nodes.Add(nCPTCode)


                            Dim StressID As String
                            Dim dtDateofStudy As Date

                            StressID = dtStudyDate.Rows(mDOS)("nStressID")
                            dtDateofStudy = Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString


                            '' CPT Code ''
                            strCptQry = "SELECT DISTINCT isnull(sCPT,'') as sCPT from CV_StressTest where nPatientID=" & pID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nGroupID=" & StressID & " AND sCPT<>''"
                            oDB.Connect(GetConnectionString)
                            dtCPT = oDB.ReadQueryDataTable(strCptQry)


                            With dtCPT
                                If dtCPT IsNot Nothing Then
                                    For mCPT = 0 To dtCPT.Rows.Count - 1
                                        Dim strCurrentCPT As String = dtCPT.Rows(mCPT)("sCPT")
                                        Dim strCPT As String = dtStudyDate.Rows(mDOS)("DateofStudy") & "|" & dtStudyDate.Rows(mDOS)("nPatientID") & "|" & dtStudyDate.Rows(mDOS)("nExamID") & "|" & dtStudyDate.Rows(mDOS)("nVisitID") & "|" & dtStudyDate.Rows(mDOS)("nClinicID")
                                        If strCurrentCPT <> "" Then
                                            nCPTCodedType = New TreeNode()
                                            nCPTCodedType.Text = dtCPT.Rows(mCPT)("sCPT")
                                            nCPTCodedType.Tag = strCPT ''dtCPT.Rows(mCPT)("sCPT")
                                            nCPTCodedType.ImageIndex = 27
                                            nCPTCodedType.SelectedImageIndex = 27

                                            nDate.Nodes(0).Nodes.Add(nCPTCodedType)
                                            ''nTestType = New TreeNode()
                                            ''nTestType.Text = "Test Type"
                                            ''nTestType.Tag = "Test Type"
                                            ''nTestType.ImageIndex = 19
                                            ''nTestType.SelectedImageIndex = 19
                                            ''nCPTCodedType.Nodes.Add(nTestType)
                                        End If




                                        '' Test Type '' & Result
                                        strCPTTestTypeQry = "SELECT DISTINCT isnull(sTestType,'') as sTestType,isnull(sResult,'') as Result from CV_StressTest where nPatientID=" & pID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nGroupID='" & StressID & "' AND sCPT='" & strCurrentCPT & "'  and sTestType<>''"
                                        oDB.Connect(GetConnectionString)
                                        dtTestType = oDB.ReadQueryDataTable(strCPTTestTypeQry)


                                        If dtTestType IsNot Nothing Then
                                            If dtTestType.Rows.Count > 0 Then
                                                nTestType = New TreeNode()
                                                nTestType.Text = "Test Type"
                                                nTestType.Tag = strCPT ''"Test Type"
                                                nTestType.ImageIndex = 19
                                                nTestType.SelectedImageIndex = 19
                                                nCPTCodedType.Nodes.Add(nTestType)
                                            End If
                                            For mTestType = 0 To dtTestType.Rows.Count - 1
                                                Dim strTestType As String = dtTestType.Rows(mTestType)("sTestType")
                                                If strTestType <> "" Then
                                                    nCPTTestType = New TreeNode()
                                                    If dtTestType.Rows(mTestType)("Result").ToString.Trim() <> "" Then
                                                        nCPTTestType.Text = dtTestType.Rows(mTestType)("sTestType") & " - " & dtTestType.Rows(mTestType)("Result")
                                                        nCPTTestType.Tag = strCPT ''dtTestType.Rows(mTestType)("sTestType") & " - " & dtTestType.Rows(mTestType)("Result")
                                                    Else
                                                        nCPTTestType.Text = dtTestType.Rows(mTestType)("sTestType")
                                                        nCPTTestType.Tag = strCPT ''dtTestType.Rows(mTestType)("sTestType")
                                                    End If
                                                    If Len(strMaxLength) < nCPTTestType.Text.Length Then
                                                        strMaxLength = nCPTTestType.Text
                                                    End If
                                                    nCPTTestType.ImageIndex = 7
                                                    nCPTTestType.SelectedImageIndex = 7

                                                    nCPTCodedType.Nodes(0).Nodes.Add(nCPTTestType)
                                                End If
                                            Next
                                            If (IsNothing(dtTestType) = False) Then
                                                dtTestType.Dispose()
                                                dtTestType = Nothing
                                            End If
                                        End If
                                    Next  ''For mCPT = 0 To dtCPT.Rows.Count - 1
                                    If (IsNothing(dtCPT) = False) Then
                                        dtCPT.Dispose()
                                        dtCPT = Nothing
                                    End If
                                End If  ''If dtCPT IsNot Nothing Then
                            End With



                            '' PHYSICIAN NODE ''
                            nPhysician = New TreeNode
                            nPhysician.Text = "Physician"
                            nPhysician.ImageIndex = 20
                            nPhysician.SelectedImageIndex = 20
                            nDate.Nodes.Add(nPhysician)

                            strPhysicianQry = "SELECT Distinct isnull(sUserName,'') as UserName from CV_StressTest WHERE  nPatientID = " & pID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtUsers = oDB.ReadQueryDataTable(strPhysicianQry)

                            If dtUsers IsNot Nothing Then
                                For mPhysician = 0 To dtUsers.Rows.Count - 1
                                    Dim strPhysician As String = dtUsers.Rows(mPhysician)("UserName")
                                    Dim strUsers As String = dtStudyDate.Rows(mDOS)("DateofStudy") & "|" & dtStudyDate.Rows(mDOS)("nPatientID") & "|" & dtStudyDate.Rows(mDOS)("nExamID") & "|" & dtStudyDate.Rows(mDOS)("nVisitID") & "|" & dtStudyDate.Rows(mDOS)("nClinicID")
                                    If strPhysician <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = dtUsers.Rows(mPhysician)("UserName")
                                        oTempNode.Tag = strUsers ''dtUsers.Rows(mPhysician)("UserName")
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7

                                        nPhysician.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtUsers) = False) Then
                                    dtUsers.Dispose()
                                    dtUsers = Nothing
                                End If
                            End If




                            '' RESTING HEART RATE ''
                            nRestingHeartRate = New TreeNode
                            nRestingHeartRate.Text = "Resting Heart Rate"
                            nRestingHeartRate.ImageIndex = 23
                            nRestingHeartRate.SelectedImageIndex = 23
                            nDate.Nodes.Add(nRestingHeartRate)

                            strRestingHeartRateQry = "SELECT Distinct isnull(nRestingHeartRate,0) as nRestingHeartRate,isnull(nRestingBPMin,0) as nRestingBPMin,isnull(nRestingBPMax,0) as nRestingBPMax from CV_StressTest WHERE  nPatientID = " & pID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtRestingHeartRate = oDB.ReadQueryDataTable(strRestingHeartRateQry)

                            If dtRestingHeartRate IsNot Nothing Then
                                For mRestingHeartRate = 0 To dtRestingHeartRate.Rows.Count - 1
                                    strCombine = ""
                                    If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString()) Then
                                        If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString() <> "") Then
                                            If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString() <> "0" Then
                                                If (strCombine = "") Then
                                                    strCombine = "Heart Rate" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate"))
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString()) Then
                                        If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString() <> "") Then
                                            If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString() <> "0" Then
                                                If (strCombine = "") Then
                                                    strCombine = "BP Min" + " " + ":" + " " + dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString()
                                                Else
                                                    strCombine = strCombine + "    " + "BP Min" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin"))
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString()) Then
                                        If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString() <> "") Then
                                            If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString() <> "0" Then
                                                If (strCombine = "") Then
                                                    strCombine = "BP Max" + " " + ":" + " " + dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString()
                                                Else
                                                    strCombine = strCombine + "    " + "BP Max" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax"))
                                                End If
                                            End If
                                        End If
                                    End If
                                    If strCombine <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = strCombine
                                        oTempNode.Tag = strCombine
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7

                                        nRestingHeartRate.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtRestingHeartRate) = False) Then
                                    dtRestingHeartRate.Dispose()
                                    dtRestingHeartRate = Nothing
                                End If
                            End If




                            '' PEAK HEART RATE ''
                            nPeakHeartRate = New TreeNode
                            nPeakHeartRate.Text = "Peak Heart Rate"
                            nPeakHeartRate.ImageIndex = 24
                            nPeakHeartRate.SelectedImageIndex = 24
                            nDate.Nodes.Add(nPeakHeartRate)

                            strPeakHeartRateQry = "SELECT Distinct isnull(nPeakHeartRate,0) as nPeakHeartRate,isnull(nPeakBPMin,0) as nPeakBPMin,isnull(nPeakBPMax,0) as nPeakBPMax from CV_StressTest WHERE  nPatientID = " & pID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtPeakHeartRate = oDB.ReadQueryDataTable(strPeakHeartRateQry)

                            If dtPeakHeartRate IsNot Nothing Then
                                For mPeakHeartRate = 0 To dtPeakHeartRate.Rows.Count - 1
                                    strCombine = ""
                                    If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString()) Then
                                        If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString() <> "") Then
                                            If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString() <> "0" Then
                                                If (strCombine = "") Then
                                                    strCombine = "Heart Rate" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString()
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()) Then
                                        If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString() <> "") Then
                                            If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString() <> "0" Then
                                                If (strCombine = "") Then
                                                    strCombine = "BP Min" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()
                                                Else
                                                    strCombine = strCombine + "    " + "BP Min" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()) Then
                                        If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString() <> "") Then
                                            If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString() <> "0" Then
                                                If (strCombine = "") Then
                                                    strCombine = "BP Max" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()
                                                Else
                                                    strCombine = strCombine + "    " + "BP Max" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()
                                                End If
                                            End If
                                        End If
                                    End If
                                    If strCombine <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = strCombine
                                        oTempNode.Tag = strCombine
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7

                                        nPeakHeartRate.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtPeakHeartRate) = False) Then
                                    dtPeakHeartRate.Dispose()
                                    dtPeakHeartRate = Nothing
                                End If
                            End If



                            '' Narrative Summary ''
                            nNarrativeSummary = New TreeNode
                            nNarrativeSummary.Text = "Narrative Summary"
                            nNarrativeSummary.ImageIndex = 22
                            nNarrativeSummary.SelectedImageIndex = 22
                            nDate.Nodes.Add(nNarrativeSummary)

                            strNarrativeSummaryQry = "SELECT DISTINCT isnull(sNarrativeSummary,'') as NarrativeSummary from CV_StressTest where nPatientID=" & pID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtNarrativeSummary = oDB.ReadQueryDataTable(strNarrativeSummaryQry)

                            If dtNarrativeSummary IsNot Nothing Then
                                For mNarrativeSummary = 0 To dtNarrativeSummary.Rows.Count - 1
                                    Dim strNarrativeSummary As String = dtNarrativeSummary.Rows(mNarrativeSummary)("NarrativeSummary")
                                    If strNarrativeSummary <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = dtNarrativeSummary.Rows(mNarrativeSummary)("NarrativeSummary")
                                        oTempNode.Tag = dtNarrativeSummary.Rows(mNarrativeSummary)("NarrativeSummary")
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7

                                        nNarrativeSummary.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtNarrativeSummary) = False) Then
                                    dtNarrativeSummary.Dispose()
                                    dtNarrativeSummary = Nothing
                                End If
                            End If


                            '' Total Excerise Time ''
                            nTotalExerciseTime = New TreeNode
                            nTotalExerciseTime.Text = "Total Excerise Time"
                            nTotalExerciseTime.ImageIndex = 18
                            nTotalExerciseTime.SelectedImageIndex = 18
                            nDate.Nodes.Add(nTotalExerciseTime)

                            strTotalExerciseTimeQry = "SELECT DISTINCT isnull(sTotExerciseTime,'') as TotalExerciseTime from CV_StressTest where nPatientID=" & pID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtTotalExerciseTime = oDB.ReadQueryDataTable(strTotalExerciseTimeQry)

                            If dtTotalExerciseTime IsNot Nothing Then
                                For mTotalExerciseTime = 0 To dtTotalExerciseTime.Rows.Count - 1
                                    Dim strTotalExerciseTime As String = dtTotalExerciseTime.Rows(mTotalExerciseTime)("TotalExerciseTime")
                                    Dim strTotalExercise As String = dtStudyDate.Rows(mDOS)("DateofStudy") & "|" & dtStudyDate.Rows(mDOS)("nPatientID") & "|" & dtStudyDate.Rows(mDOS)("nExamID") & "|" & dtStudyDate.Rows(mDOS)("nVisitID") & "|" & dtStudyDate.Rows(mDOS)("nClinicID")
                                    If strTotalExerciseTime <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = dtTotalExerciseTime.Rows(mTotalExerciseTime)("TotalExerciseTime")
                                        oTempNode.Tag = strTotalExercise ''dtTotalExerciseTime.Rows(mTotalExerciseTime)("TotalExerciseTime")
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7

                                        nTotalExerciseTime.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtTotalExerciseTime) = False) Then
                                    dtTotalExerciseTime.Dispose()
                                    dtTotalExerciseTime = Nothing
                                End If
                            End If


                            '' Ejection Fraction ''
                            nEjectionFraction = New TreeNode
                            nEjectionFraction.Text = "Ejection Fraction"
                            nEjectionFraction.ImageIndex = 17
                            nEjectionFraction.SelectedImageIndex = 17
                            nDate.Nodes.Add(nEjectionFraction)

                            strEjectionFractionQry = "SELECT DISTINCT isnull(sEjectionFraction,'') as EjectionFraction from CV_StressTest where nPatientID=" & pID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                            oDB.Connect(GetConnectionString)
                            dtEjectionFraction = oDB.ReadQueryDataTable(strEjectionFractionQry)

                            If dtEjectionFraction IsNot Nothing Then
                                For mEjectionFraction = 0 To dtEjectionFraction.Rows.Count - 1
                                    Dim strEjectionFraction As String = dtEjectionFraction.Rows(mEjectionFraction)("EjectionFraction")
                                    Dim strEjectionF As String = dtStudyDate.Rows(mDOS)("DateofStudy") & "|" & dtStudyDate.Rows(mDOS)("nPatientID") & "|" & dtStudyDate.Rows(mDOS)("nExamID") & "|" & dtStudyDate.Rows(mDOS)("nVisitID") & "|" & dtStudyDate.Rows(mDOS)("nClinicID")
                                    If strEjectionFraction <> "" Then
                                        oTempNode = New TreeNode
                                        oTempNode.Text = dtEjectionFraction.Rows(mEjectionFraction)("EjectionFraction")
                                        oTempNode.Tag = strEjectionF ''dtEjectionFraction.Rows(mEjectionFraction)("EjectionFraction")
                                        oTempNode.ImageIndex = 7
                                        oTempNode.SelectedImageIndex = 7

                                        nEjectionFraction.Nodes.Add(oTempNode)
                                    End If
                                Next
                                If (IsNothing(dtEjectionFraction) = False) Then
                                    dtEjectionFraction.Dispose()
                                    dtEjectionFraction = Nothing
                                End If
                            End If
                        Next  ''For mDOS = 0 To dtStudyDate.Rows.Count - 1

                    End If  ''If dtStudyDate.Rows.Count > 0 Then
                End If   ''If Not IsNothing(dtStudyDate) Then

                ''trImagingST.Sort()
                trImagingST.ExpandAll()
                If (IsNothing(dtStudyDate) = False) Then
                    dtStudyDate.Dispose()
                    dtStudyDate = Nothing
                End If
            End With


            If Not IsNothing(trImagingST.Nodes) Then
                If trImagingST.Nodes.Count > 0 Then
                    trImagingST.SelectedNode = trImagingST.Nodes(0)
                End If
            End If
            FillElectroPhysioTest()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally



            If (IsNothing(dtStudyDate) = False) Then
                dtStudyDate.Dispose()
                dtStudyDate = Nothing
            End If

            If (IsNothing(dtCPT) = False) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If

            If (IsNothing(dtTestType) = False) Then
                dtTestType.Dispose()
                dtTestType = Nothing
            End If

            If (IsNothing(dtUsers) = False) Then
                dtUsers.Dispose()
                dtUsers = Nothing
            End If

            If (IsNothing(dtRestingHeartRate) = False) Then
                dtRestingHeartRate.Dispose()
                dtRestingHeartRate = Nothing
            End If
            If (IsNothing(dtPeakHeartRate) = False) Then
                dtPeakHeartRate.Dispose()
                dtPeakHeartRate = Nothing
            End If
            If (IsNothing(dtNarrativeSummary) = False) Then
                dtNarrativeSummary.Dispose()
                dtNarrativeSummary = Nothing
            End If
            If (IsNothing(dtTotalExerciseTime) = False) Then
                dtTotalExerciseTime.Dispose()
                dtTotalExerciseTime = Nothing
            End If
            If (IsNothing(dtEjectionFraction) = False) Then
                dtEjectionFraction.Dispose()
                dtEjectionFraction = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        '''''''''''''''''''''Added by Ujwala - for New Stress Test moudle Changes as on 20101020
    End Sub


    Private Sub FillElectroPhysioTest()
        Dim dtProcDate As DataTable = Nothing
        Dim DtEjecfr As DataTable = Nothing
        ' Dim dtCPT As DataTable = Nothing
        'Dim dtProc As DataTable
        'Dim dtUsers As DataTable = Nothing
        Dim _tempdtprocdata As DataTable = Nothing



        Dim dvdata As DataView = Nothing

        Dim _tempdata As DataTable = Nothing
        Dim oDB As New DataBaseLayer
        Try

            Dim _Row As Integer



            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nDOS As Int16
            Dim nCPT As Int16
            'Dim nProc As Int16
            'Dim nUser As Int16


            'Dim strselecrICD9Qry As String
            'Dim strselectCPTQry As String
            'Dim strselectProcQry As String
            'Dim strselectMODQry As String
            'Dim strconcatCPT1 As String = ""
            'Dim nextICD As Integer


            '    Dim _tempdtprocdata As DataTable = Nothing


            Dim _strSQL As String = ""
            'Dim dvdata As DataView = Nothing

            'Dim _tempdata As DataTable = Nothing

            trimagingecho.Nodes.Clear()
            Try
                '  _strSQL = "SELECT nEchocardiogramID,sAortic,sLAArea,sLVDiastvol,sLvedd,sLVesd,sLVMass,sLVsystvol,sLVpostwallthick,sSeptalthick,sMitral,smvarea,sAVArea,sIDofintrepertingphys,sCPTCode,sNarrativeSummary,sProcedures,nPatientId,nvisitId,nExamId,convert(varchar,dtproceduredate,101) as dtproceduredate   FROM CV_Echocardiogram WHERE  nPatientID = " & gnPatientID & " and nVisitID= " & gnVisitID & " order by dtproceduredate " 'and  convert(varchar,dtproceduredate,101)=convert(varchar,dbo.gloGetDate(),101)"

                _strSQL = "select isnull(nEchocardiogramID,0)as nEchocardiogramID,isnull(sAortic,'')as sAortic,isnull(sLAArea,'')as 'sLAArea',isnull(sLVDiastvol,'')as sLVDiastvol,isnull(sLvedd,'')as sLvedd,isnull(sLVesd,'')as sLVesd,isnull(sLVMass,'')as sLVMass,isnull(sLVsystvol,'')as sLVsystvol,isnull(sLVpostwallthick,'')as sLVpostwallthick, isnull(sSeptalthick,'')as sSeptalthick,isnull(sMitral,'')as sMitral,isnull(smvarea,'')as smvarea,isnull(sAVArea,'')as sAVArea,isnull(sIDofintrepertingphys,'')as sIDofintrepertingphys,isnull(sCPTCode,'')as sCPTCode,isnull(sNarrativeSummary,'')as sNarrativeSummary,isnull(sProcedures,'')as sProcedures,isnull(nPatientId,0)as nPatientId,isnull(nvisitId,0)as nvisitId,isnull(nExamId,0)as nExamId,isnull(convert(varchar,dtproceduredate,101),'') as dtproceduredate   FROM CV_Echocardiogram WHERE  nPatientID = " & pID & " order by dtproceduredate "
                dtProcDate = oDB.GetDataTable_Query(_strSQL)

                _tempdtprocdata = dtProcDate
                dvdata = dtProcDate.DefaultView

                _tempdata = dvdata.ToTable(True, "dtproceduredate")

                '' Ejection Fraction 
                _strSQL = "select isNULL(sModalityTest,'') as sModalityTest ,isNULL(sQuantityPercent,'') as sQuantityPercent,isNULL(sQuantityDesc,'') as sQuantityDesc,isNULL(Convert(varchar,dtDateofTest,101),'') as dtDateofTest from cv_Ejectionfraction  where npatientid=" & pID & " and sModalityTest='Echocardiogram' order by dtDateofTest,sModalityTest "

                DtEjecfr = oDB.GetDataTable_Query(_strSQL)
                If (IsNothing(DtEjecfr)) Then
                    DtEjecfr = New DataTable()
                End If
                Dim dc As New DataColumn
                dc.ColumnName = "Flag"

                DtEjecfr.Columns.Add(dc)



            Catch ex As Exception

            Finally
                ' oDB = Nothing
            End Try
            Dim pnode As New TreeNode
            If Not IsNothing(_tempdata) Then
                If _tempdata.Rows.Count > 0 Then

                    pnode.ImageIndex = 22 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                    pnode.SelectedImageIndex = 22
                    pnode.Text = "Echocardiogram"
                    trimagingecho.Nodes.Add(pnode)
                End If
            End If
            For Each drc As DataRow In _tempdata.Rows

                Dim drrecord As DataRow() = _tempdtprocdata.Select("dtproceduredate='" & drc(0).ToString() & "'")
                Dim drejfrac As DataRow() = DtEjecfr.Select("dtDateofTest='" & drc(0).ToString() & "'")
                Dim onode As New TreeNode

                onode.Text = Convert.ToDateTime(drrecord(nDOS)("dtproceduredate")).ToShortDateString  '' 02/20/2009

                onode.ImageIndex = 1 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                onode.SelectedImageIndex = 1
                pnode.Nodes.Add(onode)



                Dim ocptnode As New TreeNode
                'Dim strCPT As String = dtProcDate.Rows(nDOS)("dtproceduredate") & "|" & dtProcDate.Rows(nDOS)("nPatientID") & "|" & dtProcDate.Rows(nDOS)("nExamID") & "|" & dtProcDate.Rows(nDOS)("nVisitID")
                ocptnode.Text = "CPT Code"  '' 02/20/2009

                ocptnode.ImageIndex = 17 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                ocptnode.SelectedImageIndex = 17
                onode.Nodes.Add(ocptnode)

                ' trimagingecho.Nodes.Add(ocptnode)



                Dim nextCPT As Integer = _Row

                For nCPT = 0 To drrecord.Length - 1
                    If drrecord(nCPT)("scptcode").ToString() <> "" Then
                        Dim ochildcptnode As New TreeNode
                        Dim strProcDate As String = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")

                        ochildcptnode.Text = drrecord(nCPT)("scptcode").ToString()
                        ochildcptnode.Tag = strProcDate
                        ochildcptnode.ImageIndex = 3
                        ochildcptnode.SelectedImageIndex = 3
                        ocptnode.Nodes.Add(ochildcptnode)
                        ' End With

                    End If
                Next



                Dim nextPRoc As Integer = nextCPT

                Dim oprocnode As New TreeNode

                oprocnode.Text = "Procedure Name"
                oprocnode.ImageIndex = 0
                oprocnode.SelectedImageIndex = 0
                onode.Nodes.Add(oprocnode)





                For nCPT = 0 To drrecord.Length - 1
                    If drrecord(nCPT)("sprocedures").ToString() <> "" Then
                        Dim ochildprocnode As New TreeNode
                        Dim strProcDate1 As String = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")

                        ochildprocnode.Text = drrecord(nCPT)("sprocedures").ToString()
                        ochildprocnode.Tag = strProcDate1
                        ochildprocnode.ImageIndex = 3
                        ochildprocnode.SelectedImageIndex = 3
                        oprocnode.Nodes.Add(ochildprocnode)





                    End If
                Next

                Dim onodephy As New TreeNode
                onodephy.Text = "Physicians"
                onodephy.ImageIndex = 2
                onodephy.SelectedImageIndex = 2
                onode.Nodes.Add(onodephy)
                'End With




                For nCPT = 0 To drrecord.Length - 1
                    If drrecord(nCPT)("sIDofintrepertingphys").ToString() <> "" Then
                        Dim ochildnodephy As New TreeNode()
                        Dim strProcDate2 As String = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                        ochildnodephy.Text = drrecord(nCPT)("sIDofintrepertingphys").ToString()
                        ochildnodephy.Tag = strProcDate2
                        ochildnodephy.ImageIndex = 3
                        ochildnodephy.SelectedImageIndex = 3
                        onodephy.Nodes.Add(ochildnodephy)
                    End If
                Next




                Dim _strlvd As String = ""
                For nCPT = 0 To 0
                    '  If dtProcDate.Rows(nCPT)("sIDofintrepertingphys").ToString() <> "" Then



                    If drrecord(nCPT)("sLvedd").ToString().Trim() <> "" Then
                        _strlvd = " LVEDD : " & drrecord(nCPT)("sLvedd").ToString() + " " + "mm" + "," '& ","
                    End If

                    If drrecord(nCPT)("sLvesd").ToString().Trim() <> "" Then
                        _strlvd = _strlvd & " LVESD : " & drrecord(nCPT)("sLvesd").ToString() + " " + "mm" + "," '& ","
                    End If

                    If drrecord(nCPT)("sLVpostwallthick").ToString().Trim() <> "" Then
                        _strlvd = _strlvd & " LV Posterior Wall Thickness : " & drrecord(nCPT)("sLVpostwallthick").ToString() + " " + "mm" + "," '& ","
                    End If

                    If drrecord(nCPT)("sSeptalthick").ToString().Trim() <> "" Then
                        _strlvd = _strlvd & "  Septal Thickness : " & drrecord(nCPT)("sSeptalthick").ToString() + " " + "mm" + "," '& ","
                    End If
                    If _strlvd.Length > 0 Then
                        _strlvd = _strlvd.Substring(0, _strlvd.Length - 1)
                    End If
                    If _strlvd <> "" Then

                        Dim ommode As New TreeNode()
                        Dim strMMode As String = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                        ommode.Text = "M-Mode"
                        ommode.Tag = strMMode
                        ommode.ImageIndex = 14
                        ommode.SelectedImageIndex = 14
                        onode.Nodes.Add(ommode)
                        ' End With

                        Dim ochildmode As New TreeNode()


                        ochildmode.Text = _strlvd
                        ochildmode.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                        ochildmode.ImageIndex = 3
                        ochildmode.SelectedImageIndex = 3
                        ommode.Nodes.Add(ochildmode)
                        'End With
                    End If




                Next
                nCPT = 0


                If drrecord(nCPT)("sAortic").ToString().Trim() = "" AndAlso drrecord(nCPT)("sMitral").ToString().Trim() = "" Then
                    Dim dpp As Integer = 0
                Else
                    Dim onodeDopp As New TreeNode()
                    Dim strDopp As String = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodeDopp.Text = "Doppler Gradients"
                    onodeDopp.Tag = strDopp
                    onodeDopp.ImageIndex = 13
                    onodeDopp.SelectedImageIndex = 13
                    onode.Nodes.Add(onodeDopp)






                    Dim _straero As String = ""
                    ' With C1CV_Echocardio.Rows(restnodes)


                    If drrecord(nCPT)("sAortic").ToString().Trim() <> "" Then
                        _straero = _straero & "  Aortic : " & drrecord(nCPT)("sAortic").ToString() + " " + "mmHg" + "," '& ","
                    End If

                    If drrecord(nCPT)("sMitral").ToString().Trim() <> "" Then
                        _straero = _straero & "  Mitral : " & drrecord(nCPT)("sMitral").ToString() & ","
                    End If

                    If _straero.Length > 0 Then
                        _straero = _straero.Substring(0, _straero.Length - 1)
                    End If

                    If _straero <> "" Then
                        Dim ochildnodeDopp As New TreeNode()
                        ochildnodeDopp.Text = _straero
                        ochildnodeDopp.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                        ochildnodeDopp.ImageIndex = 3
                        ochildnodeDopp.SelectedImageIndex = 3
                        onodeDopp.Nodes.Add(ochildnodeDopp)
                    End If



                End If



                If drrecord(nCPT)("sLAArea").ToString().Trim() <> "" Then

                    Dim onodelaarea As New TreeNode()
                    onodelaarea.Text = "LA Area"
                    onodelaarea.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodelaarea.ImageIndex = 6
                    onodelaarea.SelectedImageIndex = 6
                    onode.Nodes.Add(onodelaarea)

                    Dim ochildnodelaarea As New TreeNode()
                    ochildnodelaarea.Text = drrecord(nCPT)("sLAArea").ToString() + " " + "cm2"
                    ochildnodelaarea.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    ochildnodelaarea.ImageIndex = 3
                    ochildnodelaarea.SelectedImageIndex = 3
                    onodelaarea.Nodes.Add(ochildnodelaarea)
                End If


                If drrecord(nCPT)("sAVArea").ToString().Trim() <> "" Then

                    Dim onodeavarea As New TreeNode()

                    onodeavarea.Text = "AV Area"
                    onodeavarea.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodeavarea.ImageIndex = 5
                    onodeavarea.SelectedImageIndex = 5
                    onode.Nodes.Add(onodeavarea)
                    Dim ochildnodeavarea As New TreeNode()

                    ochildnodeavarea.Text = drrecord(nCPT)("sAVArea").ToString() + " " + "cm2"
                    ochildnodeavarea.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    ochildnodeavarea.ImageIndex = 3
                    ochildnodeavarea.SelectedImageIndex = 3
                    onodeavarea.Nodes.Add(ochildnodeavarea)

                End If


                If drrecord(nCPT)("sMVArea").ToString().Trim() <> "" Then
                    Dim onodemvarea As New TreeNode
                    onodemvarea.Text = "MV Area"
                    onodemvarea.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodemvarea.ImageIndex = 4
                    onodemvarea.SelectedImageIndex = 4
                    onode.Nodes.Add(onodemvarea)

                    Dim ochildnodemvarea As New TreeNode
                    ochildnodemvarea.Text = drrecord(nCPT)("sMVArea").ToString() + " " + "cm2"
                    ochildnodemvarea.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    ochildnodemvarea.ImageIndex = 3
                    ochildnodemvarea.SelectedImageIndex = 3
                    onodemvarea.Nodes.Add(ochildnodemvarea)
                End If


                If drrecord(nCPT)("sLVDiastVol").ToString().Trim() <> "" Then

                    ' .Node.Level = 1
                    Dim onodelvdiastvol As New TreeNode
                    onodelvdiastvol.Text = "LV Diastolic volume"
                    onodelvdiastvol.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodelvdiastvol.ImageIndex = 9
                    onodelvdiastvol.SelectedImageIndex = 9
                    onode.Nodes.Add(onodelvdiastvol)

                    '  restnodes = setdata(restnodes, drrecord(nCPT)("sLVDiastVol").ToString(), drrecord)
                    Dim ochildnodelvdiastvol As New TreeNode
                    ochildnodelvdiastvol.Text = drrecord(nCPT)("sLVDiastVol").ToString()
                    ochildnodelvdiastvol.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    ochildnodelvdiastvol.SelectedImageIndex = 3
                    ochildnodelvdiastvol.ImageIndex = 3
                    onodelvdiastvol.Nodes.Add(ochildnodelvdiastvol)

                End If



                If drrecord(nCPT)("sLVsystVol").ToString().Trim() <> "" Then


                    ' .Node.Level = 1
                    Dim onodelvsysttvol As New TreeNode
                    onodelvsysttvol.Text = "LV Systolic volume"
                    onodelvsysttvol.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodelvsysttvol.ImageIndex = 10
                    onodelvsysttvol.SelectedImageIndex = 10
                    onode.Nodes.Add(onodelvsysttvol)

                    'restnodes = setdata(restnodes, drrecord(nCPT)("sLVsystVol").ToString(), drrecord)
                    Dim ochildnodelvsysttvol As New TreeNode
                    ochildnodelvsysttvol.Text = drrecord(nCPT)("sLVsystVol").ToString()
                    ochildnodelvsysttvol.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    ochildnodelvsysttvol.ImageIndex = 3
                    ochildnodelvsysttvol.SelectedImageIndex = 3
                    onodelvsysttvol.Nodes.Add(ochildnodelvsysttvol)


                End If


                If drrecord(nCPT)("sLVMass").ToString().Trim() <> "" Then

                    Dim onodelvmass As New TreeNode
                    onodelvmass.Text = "LV Mass"
                    onodelvmass.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodelvmass.ImageIndex = 11
                    onodelvmass.SelectedImageIndex = 11
                    onode.Nodes.Add(onodelvmass)
                    ' End With
                    '    restnodes = setdata(restnodes, drrecord(nCPT)("sLVMass").ToString() + " " + "g", drrecord)
                    Dim ochildnodelvmass As New TreeNode
                    ochildnodelvmass.Text = drrecord(nCPT)("sLVMass").ToString() + " " + "g"
                    ochildnodelvmass.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    ochildnodelvmass.ImageIndex = 3
                    ochildnodelvmass.SelectedImageIndex = 3
                    onodelvmass.Nodes.Add(ochildnodelvmass)

                End If



                If drrecord(nCPT)("sNarrativeSummary").ToString().Trim() <> "" Then


                    Dim onodenarsum As New TreeNode
                    onodenarsum.Text = "Narrative Summary"
                    onodenarsum.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    onodenarsum.ImageIndex = 8
                    onodenarsum.SelectedImageIndex = 8
                    onode.Nodes.Add(onodenarsum)

                    '  restnodes = setdata(restnodes, drrecord(nCPT)("sNarrativeSummary").ToString(), drrecord)
                    Dim ochildnodenarsum As New TreeNode
                    ochildnodenarsum.Text = drrecord(nCPT)("sNarrativeSummary").ToString()
                    ochildnodenarsum.Tag = drc(0).ToString() & "|" & drrecord(nCPT)("nPatientID") & "|" & drrecord(nCPT)("nExamID") & "|" & drrecord(nCPT)("nVisitID")
                    ochildnodenarsum.ImageIndex = 3
                    ochildnodenarsum.SelectedImageIndex = 3
                    onodenarsum.Nodes.Add(ochildnodenarsum)


                End If
                '' Ejection Fraction added

                Dim opnodeejec As TreeNode = Nothing
                If drejfrac.Length >= 1 Then
                    'If DtEjecfr.Rows.Count >= 1 Then
                    opnodeejec = New TreeNode
                    opnodeejec.Text = "Ejection Fraction"
                    opnodeejec.ImageIndex = 8
                    opnodeejec.SelectedImageIndex = 8
                    onode.Nodes.Add(opnodeejec)
                End If

                For rowcnt As Integer = 0 To drejfrac.Length - 1 '  DtEjecfr.Length - 1
                    Dim ind As Integer = DtEjecfr.Rows.IndexOf(drejfrac(rowcnt))
                    DtEjecfr.Rows(ind)("Flag") = "1"
                    Dim onodeparenttest As New TreeNode
                    onodeparenttest.Text = "Modality Test"
                    onodeparenttest.ImageIndex = 3
                    onodeparenttest.SelectedImageIndex = 3

                    Dim onodetest As New TreeNode
                    onodetest.Text = drejfrac(rowcnt)("sModalityTest").ToString()
                    onodetest.ImageIndex = 3
                    onodetest.SelectedImageIndex = 3
                    onodeparenttest.Nodes.Add(onodetest)
                    ' onode.Nodes.Add(onodetest)
                    Dim onodechildecho As New TreeNode
                    onodechildecho.Text = "Quantity Percent "
                    Dim onodechildechoperc As New TreeNode
                    onodechildechoperc.Text = drejfrac(rowcnt)("sQuantityPercent").ToString()
                    onodechildecho.Nodes.Add(onodechildechoperc)

                    Dim onodechildecho2 As New TreeNode
                    onodechildecho2.Text = "Quantity Desc "

                    Dim onodechildecho2desc As New TreeNode
                    onodechildecho2desc.Text = drejfrac(rowcnt)("sQuantityDesc").ToString()

                    onodechildecho2.Nodes.Add(onodechildecho2desc)

                    opnodeejec.Nodes.Add(onodeparenttest)

                    onodechildecho.ImageIndex = 3
                    onodechildecho.SelectedImageIndex = 3
                    onodechildecho2.ImageIndex = 3
                    onodechildecho2.SelectedImageIndex = 3
                    onodechildecho2desc.ImageIndex = 3
                    onodechildecho2desc.SelectedImageIndex = 3
                    onodechildechoperc.ImageIndex = 3
                    onodechildechoperc.SelectedImageIndex = 3

                    opnodeejec.Nodes.Add(onodechildecho)
                    ' opnodeejec.Nodes.Add(onodechildechoperc)

                    opnodeejec.Nodes.Add(onodechildecho2)
                    ' opnodeejec.Nodes.Add(onodechildecho2desc)
                Next
            Next


            For rowcnt As Integer = 0 To DtEjecfr.Rows.Count - 1   '  DtEjecfr.Length - 1
                'Dim ind As Integer = DtEjecfr.Rows.IndexOf(drejfrac(rowcnt))
                If (DtEjecfr.Rows(rowcnt)("Flag").ToString().Trim() = "") Then


                    Dim onode As New TreeNode

                    onode.Text = Convert.ToDateTime(DtEjecfr.Rows(rowcnt)("dtDateofTest")).ToShortDateString  '' 02/20/2009

                    onode.ImageIndex = 1 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                    onode.SelectedImageIndex = 1
                    pnode.Nodes.Add(onode)

                    Dim opnodeejec As TreeNode
                    opnodeejec = New TreeNode
                    opnodeejec.Text = "Ejection Fraction"
                    opnodeejec.ImageIndex = 8
                    opnodeejec.SelectedImageIndex = 8
                    onode.Nodes.Add(opnodeejec)

                    Dim onodeparenttest As New TreeNode
                    onodeparenttest.Text = "Modality Test"
                    onodeparenttest.ImageIndex = 3
                    onodeparenttest.SelectedImageIndex = 3

                    Dim onodetest As New TreeNode
                    onodetest.Text = DtEjecfr.Rows(rowcnt)("sModalityTest").ToString()
                    onodetest.ImageIndex = 3
                    onodetest.SelectedImageIndex = 3
                    onodeparenttest.Nodes.Add(onodetest)
                    ' onode.Nodes.Add(onodetest)
                    Dim onodechildecho As New TreeNode
                    onodechildecho.Text = "Quantity Percent "
                    Dim onodechildechoperc As New TreeNode
                    onodechildechoperc.Text = DtEjecfr.Rows(rowcnt)("sQuantityPercent").ToString()
                    onodechildecho.Nodes.Add(onodechildechoperc)

                    Dim onodechildecho2 As New TreeNode
                    onodechildecho2.Text = "Quantity Desc "

                    Dim onodechildecho2desc As New TreeNode
                    onodechildecho2desc.Text = DtEjecfr.Rows(rowcnt)("sQuantityDesc").ToString()

                    onodechildecho2.Nodes.Add(onodechildecho2desc)

                    opnodeejec.Nodes.Add(onodeparenttest)

                    onodechildecho.ImageIndex = 3
                    onodechildecho.SelectedImageIndex = 3
                    onodechildecho2.ImageIndex = 3
                    onodechildecho2.SelectedImageIndex = 3
                    onodechildecho2desc.ImageIndex = 3
                    onodechildecho2desc.SelectedImageIndex = 3
                    onodechildechoperc.ImageIndex = 3
                    onodechildechoperc.SelectedImageIndex = 3

                    opnodeejec.Nodes.Add(onodechildecho)
                    ' opnodeejec.Nodes.Add(onodechildechoperc)

                    opnodeejec.Nodes.Add(onodechildecho2)
                    ' opnodeejec.Nodes.Add(onodechildecho2desc)
                End If
            Next

            trimagingecho.ExpandAll()
            If Not IsNothing(trImagingST.Nodes) Then
                If trimagingecho.Nodes.Count > 0 Then
                    trimagingecho.SelectedNode = trimagingecho.Nodes(0)
                End If
            End If

            ' Next
            ' drrecord = Nothing
            'dtProcDate = Nothing
            'dtCPT = Nothing
            'dtUsers = Nothing

            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing
            trimagingecho.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '   MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            If (IsNothing(dtProcDate) = False) Then
                dtProcDate.Dispose()
                dtProcDate = Nothing
            End If
            If (IsNothing(DtEjecfr) = False) Then
                DtEjecfr.Dispose()
                DtEjecfr = Nothing
            End If
            'If (IsNothing(dtCPT) = False) Then
            '    dtCPT.Dispose()
            '    dtCPT = Nothing
            'End If
            'If (IsNothing(dtUsers) = False) Then
            '    dtUsers.Dispose()
            '    dtUsers = Nothing
            'End If
            If (IsNothing(_tempdtprocdata) = False) Then
                _tempdtprocdata.Dispose()
                _tempdtprocdata = Nothing
            End If

            If (IsNothing(dvdata) = False) Then
                dvdata.Dispose()
                dvdata = Nothing
            End If

            If (IsNothing(_tempdata) = False) Then
                _tempdata.Dispose()
                _tempdata = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

  

    Public Sub FillPatientOrders()

        gloC1FlexStyle.Style(C1OrderDetails)

        ''dhruv 20091130
        ''Avoid the flickering
        C1OrderDetails.Redraw = False
        'Dim dtFiltered As DataTable

        ' '' ''Dim dtOrder As DataTable

        If IsNothing(dtOrder) Then
            ''Fixed Bug Id 5508 pID Insted of _PatientID
            Dim objPatientDetail As New clsPatientDetails
            dtOrder = objPatientDetail.Fill_PatientOrders(pID)
            objPatientDetail.Dispose()
            objPatientDetail = Nothing
            ''End
        End If
       
        If IsNothing(dtOrder) = False Then
            ''lm_Visit_ID, lm_OrderDate, lm_category_Description, lm_test_Name,lm_NumericResult()
           
            C1OrderDetails.DataSource = dtOrder
            Dim _width As Integer = C1OrderDetails.Width

            C1OrderDetails.Rows.Fixed() = 1
            C1OrderDetails.AllowEditing = False

            C1OrderDetails.Cols("lm_Visit_ID").Visible = False
            C1OrderDetails.Cols("ICD9").Visible = False

            C1OrderDetails.Cols("lm_OrderDate").Caption = "Order Date"
            C1OrderDetails.Cols("lm_sCategoryName").Caption = "Category"
            C1OrderDetails.Cols("lm_sTestName").Caption = "Test Name"
            C1OrderDetails.Cols("lm_NumericResult").Caption = "Numeric Result"
            ''Added Rahul on 20101030
            C1OrderDetails.Cols("Status").Caption = "Status"
            C1OrderDetails.Cols("LoincID").Caption = "LonicID"
            C1OrderDetails.Cols("TextComment").Caption = "TextComment"
            ''End

            C1OrderDetails.Cols("lm_OrderDate").Width = _width * 0.12
            C1OrderDetails.Cols("lm_sCategoryName").Width = _width * 0.16
            C1OrderDetails.Cols("lm_sTestName").Width = _width * 0.16
            C1OrderDetails.Cols("lm_NumericResult").Width = _width * 0.15
            C1OrderDetails.Cols("lm_NumericResult").TextAlign = TextAlignEnum.LeftBottom

            ''Added Rahul on 20101030
            C1OrderDetails.Cols("Status").Width = _width * 0.15
            C1OrderDetails.Cols("LoincID").Width = _width * 0.1
            C1OrderDetails.Cols("TextComment").Width = _width * 0.15
            ''End
        End If

        C1OrderDetails.Redraw = True
    End Sub


    Public Sub PopulateLatestHistory(ByVal oTable As DataTable)


        '   Dim _table As New DataTable
        '  Dim oTable As DataTable


        Try


            ' oTable = dsdata.Tables("History")
            If Not IsNothing(oTable) Then
                If oTable.Rows.Count > 0 Then
                    Dim strMaxLength As String = ""
                    Dim myNode As New TreeNode("History")
                    myNode.ImageIndex = 3
                    myNode.SelectedImageIndex = 3
                    Dim mychildnode As TreeNode = Nothing
                    Dim mychildnode1 As TreeNode = Nothing
                    trHistory.Nodes.Clear()
                    trHistory.Nodes.Add(myNode)
                    For i As Integer = 0 To oTable.Rows.Count - 1


                       


                        For Each childnode As TreeNode In myNode.Nodes
                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                            If childnode.Text.ToUpper.StartsWith(Convert.ToString(oTable.Rows(i)("sHistoryCategory")).ToUpper) Then
                                mychildnode = childnode

                                Exit For
                            End If
                        Next
                        If IsNothing(mychildnode) Then
                            mychildnode = New TreeNode(Convert.ToString(oTable.Rows(i)("sHistoryCategory")))
                            mychildnode.ImageIndex = 7
                            mychildnode.SelectedImageIndex = 7
                            mychildnode.Tag = oTable.Rows(i)("VisitID")
                            Dim VisitDate As Date = GetVisitdate(mychildnode.Tag)
                            mychildnode.Text = mychildnode.Text & " " & CType(VisitDate, String)
                            If Len(strMaxLength) < mychildnode.Text.Length Then
                                strMaxLength = mychildnode.Text
                            End If
                            myNode.Nodes.Add(mychildnode)
                        End If

                        Dim _Reaction As String = ""
                        If Convert.ToString(oTable.Rows(i)("sReaction")) <> "" Then
                            Dim sTemp() As String = Convert.ToString(oTable.Rows(i)("sReaction")).Split("|")
                            Dim sTemp1() As String = sTemp(0).Split(":")

                            If sTemp.Length > 1 Then
                                If sTemp1(0).Length > 0 Then
                                    _Reaction = sTemp1(0) & " - " & sTemp(1)
                                End If
                            End If
                        End If

                        mychildnode1 = New TreeNode(Convert.ToString(oTable.Rows(i)("HistoryItem")) & " - " & Convert.ToString(oTable.Rows(i)("sComments")) & " - " & _Reaction)
                        mychildnode1.ImageIndex = 9
                        mychildnode1.SelectedImageIndex = 9
                        mychildnode1.Tag = oTable.Rows(i)("VisitID")
                        mychildnode.Nodes.Add(mychildnode1)
                        If Len(strMaxLength) < mychildnode1.Text.Length Then
                            strMaxLength = mychildnode1.Text
                        End If
                        mychildnode1 = Nothing
                        mychildnode = Nothing


                    Next
                    SetPanelWidth(trHistory, pnlHistoryWidthMax, strMaxLength, pnlHistoryWidth)
                End If
            End If
            '    trHistory.Sort()
            trHistory.ExpandAll()

            If Not IsNothing(trHistory.Nodes) Then
                If trHistory.Nodes.Count > 0 Then
                    trHistory.SelectedNode = trHistory.Nodes(0)
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
        Finally

        End Try
    End Sub


#Region " Documents "
    Public Sub Fill_PatientSacnedDocuments(ByVal PatientID As Long, Optional ByVal DesignGrid As Boolean = True)
        ''dhruv 20091130
        ''To avoid the flickering effect
      
        Try
            'blnFormLoad = True
            ''Check DMS System Path is Correct
            'If oDMSPath.IsDMSSystem(DMSRootPath) = False Then
            '    MessageBox.Show("Document Management System Path not set.", gstrMessageBoxCaption, MessageBoxButtons.OK)
            '    Exit Sub
            'End If


            ' ''Design Uncategory Grid
            'DesignUncategorisedDocument(c1CategorisedDocuments)


            ''Categorised Year
            'spl_CategorisedDocument_YearTop.Visible = False
            'spl_CategorisedDocument_YearBottom.Visible = False
            'pnlDocument_CategorisedDocument_MonthYear.Visible = False
            'pnlDocument_CategorisedDocument_MenuOption.Visible = False

            ''Fill Categories

            ' ''Commented By Pramod
            ''Fill_Categories(c1CategorisedDocuments, PatientID)
            ' ''END

            'txtCategorisedYear.Text = Date.Now.Year
            'lblDocument_CategorisedDocument_Hdr.Text = "Document" ' : " & Date.Now.Year

            ''Load DMS Settings
            'chkCategorisedMenu_Merge.Checked = False
            'chkCategorisedMenu_12Month.Checked = False


            'pnlDocument_CategorisedDocument_Cmd_Year.Visible = False


            'Dim oSupporting As New gloStream.gloDMS.Supporting.Supporting
            'Dim oSettings As New gloStream.gloDMS.Supporting.DMSSettings
            'oSettings = oSupporting.GetDMSSettings
            'With oSettings
            '    DMS_ScanDoc_MonthView = .ScanDocumentMonthView
            '    DMS_ViewDoc_MonthView = .ViewDocumentMonthView

            '    chkCategorisedMenu_Merge.Checked = .CategoryDocMergeMenu
            '    'chkUncategorisedMenu_Merge.Checked = .GeneralBinMergeMenu

            '    If .ScanDocumentMonthView = True Then
            '        pnlDocument_CategorisedDocument_Cmd_Year.Visible = True
            '        'pnlDocument_UncategorisedDocument_Cmd_Year.Visible = True

            '        chkCategorisedMenu_12Month.Checked = .CategoryDoc12MonthsMenu
            '        'chkUncategorisedMenu_12Month.Checked = .GeneralBin12MonthsMenu
            '    End If
            'End With
            'oSettings = Nothing
            'oSupporting = Nothing

            'txtPatientID.Text = PatientID
            ''lblPatientName.Text = gstrPatientLastName & " " & gstrPatientFirstName

            ''Fill Categorised Documents
            ''ScanDocumentList()

            If C1PatientDMS.Visible = False Then '' SUDHIR 20090729 '' IF DMS TAB IS NOT OPEN THEN DON'T FILL DOCs '' BUG 2675 ''
                Exit Sub
            End If
            C1PatientDMS.Redraw = False
            '' 'Design Category Grid
            If DesignGrid Then
                DesignCategorisedDocument(C1PatientDMS)
            Else
                C1PatientDMS.Rows.Count = 1
            End If

            ' ''Fill data 
            'Fill_GridData(PatientID, c1CategorisedDocuments)
            'blnFormLoad = False

            'If c1CategorisedDocuments.Rows.Count > 1 Then
            '    pnlScannedDocs.Visible = True
            '    'c1CategorisedDocuments.Visible = True
            'Else
            '    'c1CategorisedDocuments.Visible = False
            '    pnlScannedDocs.Visible = False
            '    lbl_ViewDocuments.Visible = True
            'End If
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Doucment List viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)

            

            With C1PatientDMS
                gloC1FlexStyle.Style(C1PatientDMS, True)
                'UpdateLog_Temp("oList.GetBaseDocuments")
                Dim oList As New gloEDocumentV3.eDocManager.eDocGetList()
                Dim oDocuments As gloEDocumentV3.Document.BaseDocuments = oList.GetPatientBaseDocuments(pID, gClinicID)
                oList.Dispose()
                oList = Nothing
                ''UpdateLog("END- oList.GetBaseDocuments")
                If Not oDocuments Is Nothing Then
                    For k As Int16 = 0 To oDocuments.Count - 1

                        For j As Integer = 0 To oDocuments(k).EContainers.Count - 1

                            .Rows.Add()
                            .Cols(COL_View_DocumentName).TextAlign = TextAlignEnum.LeftCenter
                            Dim rgStyle As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, COL_View_DocumentName, .Rows.Count - 1, COL_View_DocumentName)
                            rgStyle.StyleNew.DataType = GetType(String)
                            .SetData(.Rows.Count - 1, COL_View_DocumentName, oDocuments(k).DocumentName)
                            .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEMACHINE, "")  ' Source Machine
                            .SetData(.Rows.Count - 1, COL_D_CAT_SYSTEMFOLDER, "")    ' System Folder
                            .SetData(.Rows.Count - 1, COL_D_CAT_CONTAINER, oDocuments(k).EContainers(j).EContainerID)          ' Container
                            .SetData(.Rows.Count - 1, COL_View_Category, oDocuments(k).Category)            ' Category
                            .SetData(.Rows.Count - 1, COL_D_CAT_PATIENTID, pID)          ' Patient ID
                            .SetData(.Rows.Count - 1, COL_D_CAT_YEAR, oDocuments(k).Year)                    ' Year
                            .SetData(.Rows.Count - 1, COL_D_CAT_MONTH, oDocuments(k).Month)                  ' Month
                            .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEBIN, "")          ' Source Bin
                            .SetData(.Rows.Count - 1, COL_D_CAT_INUSED, "")                ' In Used
                            .SetData(.Rows.Count - 1, COL_D_CAT_USEDMACHINE, "")      ' Used Machine
                            .SetData(.Rows.Count - 1, COL_D_CAT_USEDTYPE, "")            ' Used Type
                            .SetData(.Rows.Count - 1, COL_D_CAT_PATH, "")                    ' Path
                            .SetData(.Rows.Count - 1, COL_D_CAT_COLTYPE, CType(enumColType.Document, Integer))
                            .SetData(.Rows.Count - 1, COL_D_CAT_FILENAME, oDocuments(k).EContainers(j).EDocumentID) '' DocumentID
                            .SetData(.Rows.Count - 1, COL_D_CAT_MACHINEID, "")
                            .SetData(.Rows.Count - 1, COL_D_CAT_VERSIONNO, "")

                            If oDocuments(k).HasNote = True Then
                                .SetCellImage(.Rows.Count - 1, COL_View_NOTEFLAG, Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                            End If
                            If oDocuments(k).IsAcknowledge = True Then
                                .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 1)
                                '  .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File")
                                .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, Img_Reviwed.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                            Else
                                .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 0)
                                ' .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File_UnReviwed")
                                .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, Img_Blanck.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                            End If

                        Next


                        'nmonths = nmonths + 1
                    Next
                    oDocuments.Dispose()

                End If

            End With

            ' blnFormLoad = False
            C1PatientDMS.Visible = True
            'If C1PatientExamDMS.Rows.Count > 1 Then
            '    pnlScannedDocs.Visible = True
            '    'c1CategorisedDocuments.Visible = True
            'Else
            '    'c1CategorisedDocuments.Visible = False
            '    pnlScannedDocs.Visible = False
            '    lbl_ViewDocuments.Visible = True
            'End If
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Doucment List viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  blnFormLoad = False
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally

        End Try
        C1PatientDMS.Redraw = True

    End Sub

    Private Sub DesignCategorisedDocument(ByVal DesignControl As C1FlexGrid)

        With DesignControl
            .AllowSorting = AllowSortingEnum.None
            .Visible = True
            .BringToFront()
            .Cols.Count = Col_view_Count
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 0


            .Cols(COL_D_CAT_CATEGORY).Width = 0                 ' ID
            .Cols(COL_D_CAT_NAME).Width = 0    '225             ' Name
            .Cols(COL_D_CAT_NOTEFLAG).Width = 0 '15          ' Note Flag
            .Cols(COL_D_CAT_EXTRAFLAG).Width = 0          ' Extra Col
            .Cols(COL_D_CAT_SOURCEMACHINE).Width = 0      ' Source Machine
            .Cols(COL_D_CAT_SYSTEMFOLDER).Width = 0       ' System Folder
            .Cols(COL_D_CAT_CONTAINER).Width = 0          ' Container
            .Cols(COL_D_CAT_CATEGORY).Width = 0           ' Category
            .Cols(COL_D_CAT_PATIENTID).Width = 0          ' Patient ID
            .Cols(COL_D_CAT_YEAR).Width = 0               ' Year
            .Cols(COL_D_CAT_MONTH).Width = 0              ' Month
            .Cols(COL_D_CAT_SOURCEBIN).Width = 0          ' Source Bin
            .Cols(COL_D_CAT_INUSED).Width = 0             ' In Used
            .Cols(COL_D_CAT_USEDMACHINE).Width = 0        ' Used Machine
            .Cols(COL_D_CAT_USEDTYPE).Width = 0           ' Used Type
            .Cols(COL_D_CAT_PATH).Width = 0               ' Path
            .Cols(COL_D_CAT_FILENAME).Width = 0           ' File Name
            .Cols(COL_D_CAT_MACHINEID).Width = 0               ' Machine ID
            .Cols(COL_D_CAT_COLTYPE).Width = 0           ' Col Type
            .Cols(COL_D_CAT_VERSIONNO).Width = 0           ' Version No
            .Cols(COL_D_CAT_MACHINEID).Width = 0               ' Machine ID
            .Cols(COL_D_CAT_COLTYPE).Width = 0           ' Col Type
            .Cols(COL_D_CAT_REVIWEDFLAG).Width = 0 '15           ' Col Type

            ''Pramod 06162007 View Documents START
            .Cols(COL_View_CategoryHidden).Width = .Width * 0
            .Cols(COL_View_Category).Width = .Width * 0.47  ' Category Name
            .Cols(COL_View_Month).Width = .Width * 0          'Month
            .Cols(COL_View_DocumentName).Width = .Width * 0.47   'Document Name 
            .Cols(COL_View_NOTEFLAG).Width = 15  'Acnowlegements 
            .Cols(COL_View_REVIWEDFLAG).Width = 15           'review
            ''Pramod 06162007 View Documents END

            .Cols(COL_D_CAT_ID).Visible = False           ' ID
            .Cols(COL_D_CAT_NAME).Visible = False 'True          ' Name
            .Cols(COL_D_CAT_NOTEFLAG).Visible = False 'True      ' Note Flag
            .Cols(COL_D_CAT_EXTRAFLAG).Visible = False    ' Extra Col
            .Cols(COL_D_CAT_SOURCEMACHINE).Visible = False ' Source Machine
            .Cols(COL_D_CAT_SYSTEMFOLDER).Visible = False ' System Folder
            .Cols(COL_D_CAT_CONTAINER).Visible = False    ' Container
            .Cols(COL_D_CAT_CATEGORY).Visible = False     ' Category
            .Cols(COL_D_CAT_PATIENTID).Visible = False    ' Patient ID
            .Cols(COL_D_CAT_YEAR).Visible = False         ' Year
            .Cols(COL_D_CAT_MONTH).Visible = False        ' Month
            .Cols(COL_D_CAT_SOURCEBIN).Visible = False    ' Source Bin
            .Cols(COL_D_CAT_INUSED).Visible = False       ' In Used
            .Cols(COL_D_CAT_USEDMACHINE).Visible = False  ' Used Machine
            .Cols(COL_D_CAT_USEDTYPE).Visible = False     ' Used Type
            .Cols(COL_D_CAT_PATH).Visible = False         ' Path
            .Cols(COL_D_CAT_FILENAME).Visible = False           ' File Name
            .Cols(COL_D_CAT_MACHINEID).Visible = False               ' Machine ID
            .Cols(COL_D_CAT_COLTYPE).Visible = False      ' Col Type
            .Cols(COL_D_CAT_VERSIONNO).Visible = False      ' Version No
            .Cols(COL_D_CAT_VERSIONNO).Visible = False      ' Col Type
            .Cols(COL_D_CAT_ISREVIWED).Visible = False      ' Col Type
            .Cols(COL_D_CAT_REVIWEDFLAG).Visible = False ' True      ' Col Type

            ''Pramod 06162007 View Documents START
            .Cols(COL_View_CategoryHidden).Visible = False

            .Cols(COL_View_Category).Visible = True       ' Category Name
            .Cols(COL_View_Category).AllowEditing = False


            .Cols(COL_View_Month).Visible = False          'Month
            .Cols(COL_View_Month).AllowEditing = False

            .Cols(COL_View_DocumentName).Visible = True   'Document Name  
            .Cols(COL_View_DocumentName).AllowEditing = False


            .Cols(COL_View_NOTEFLAG).Visible = True  'Acnowlegements 
            .Cols(COL_View_NOTEFLAG).AllowEditing = False


            .Cols(COL_View_REVIWEDFLAG).Visible = True           'review
            .Cols(COL_View_REVIWEDFLAG).AllowEditing = False

            ''Pramod 06162007 View Documents END

            '.Cols(COL_CAT_NOTEFLAG).ComboList = "..."

            ''Pramod 06162007 View Documents START
            .SetData(0, COL_View_CategoryHidden, "Category")
            .Cols(COL_View_CategoryHidden).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, COL_View_Category, "Category")
            .Cols(COL_View_Category).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, COL_View_DocumentName, "Document Name")
            .Cols(COL_View_DocumentName).TextAlign = TextAlignEnum.GeneralCenter
            '.SetData(0, COL_View_NOTEFLAG, "Note")
            '.Cols(COL_View_NOTEFLAG).TextAlign = TextAlignEnum.GeneralCenter
            '.SetData(0, COL_View_REVIWEDFLAG, "Review")
            '.Cols(COL_View_REVIWEDFLAG).TextAlign = TextAlignEnum.GeneralCenter
            ''Pramod 06162007 View Documents END

        End With

    End Sub
#End Region

#Region "Search Past Exam"

    Public Sub FillExamTypeCombo()


        If IsNothing(dtStatusFillExamTypeCombo) = False Then
            dtStatusFillExamTypeCombo.Dispose()
            dtStatusFillExamTypeCombo = Nothing
        End If

        dtStatusFillExamTypeCombo = New DataTable

        Dim MyRow As DataRow

        dtStatusFillExamTypeCombo.Columns.Add(New DataColumn("StatusID", GetType(Short)))
        dtStatusFillExamTypeCombo.Columns.Add(New DataColumn("ExamStatus", GetType(String)))

        MyRow = dtStatusFillExamTypeCombo.NewRow()
        MyRow(0) = 2
        MyRow(1) = "All"
        dtStatusFillExamTypeCombo.Rows.Add(MyRow)

        MyRow = dtStatusFillExamTypeCombo.NewRow()
        MyRow(0) = 1
        MyRow(1) = "Finished"
        dtStatusFillExamTypeCombo.Rows.Add(MyRow)

        MyRow = dtStatusFillExamTypeCombo.NewRow()
        MyRow(0) = 0
        MyRow(1) = "UnFinished"
        dtStatusFillExamTypeCombo.Rows.Add(MyRow)

        cmbExamStatus.DataSource = dtStatusFillExamTypeCombo
        cmbExamStatus.DisplayMember = dtStatusFillExamTypeCombo.Columns("ExamStatus").ToString()
        cmbExamStatus.ValueMember = dtStatusFillExamTypeCombo.Columns("StatusID").ToString()


    End Sub

    Public Sub FillExamProviderCombo()
        Dim oDB As New DataBaseLayer
        Dim strSelect As String = "select nProviderID,isnull(sFirstName,'') + ' '+ CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' " _
                                & " When sMiddleName then sMiddleName +  ' ' END + + isnull(sLastName,'') as Name from Provider_MST"
        Dim dt As DataTable = Nothing
        dt = oDB.GetDataTable_Query(strSelect)
        oDB.Dispose()
        oDB = Nothing
        Dim r As DataRow
        r = dt.NewRow
        r.Item("Name") = "All"
        r.Item("nProviderID") = 0
        dt.Rows.InsertAt(r, 0)

        Dim strProviderName As String = ""

        cmbExamProvider.DataSource = dt
        cmbExamProvider.DisplayMember = dt.Columns("Name").ToString()
        cmbExamProvider.ValueMember = dt.Columns("nProviderID").ToString()

    End Sub

    Private Sub cmbExamProvider_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbExamProvider.SelectedIndexChanged
        Try
            ''added for bugid 70924

            If Not cmbExamProvider.SelectedItem Is Nothing Then

                If getWidthofListItems(Convert.ToString((CType(cmbExamProvider.Items(cmbExamProvider.SelectedIndex), DataRowView))("name")), cmbExamProvider) >= cmbExamProvider.DropDownWidth - 20 Then
                    ToolTip1.SetToolTip(cmbExamProvider, Convert.ToString((CType(cmbExamProvider.Items(cmbExamProvider.SelectedIndex), DataRowView))("name")))
                Else
                    ToolTip1.SetToolTip(cmbExamProvider, "")
                    Me.ToolTip1.Hide(cmbExamProvider)
                End If
            Else
                Me.ToolTip1.Hide(cmbExamProvider)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbExamProvider_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbExamProvider.SelectionChangeCommitted
       

        _Defaultload = False
        m_ExamFilter = True
    
        Fill_PastExams()
        m_ExamFilter = False
    End Sub

    Private Sub cmbExamStatus_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbExamStatus.SelectionChangeCommitted
      


        _Defaultload = False
        m_ExamFilter = True
        Fill_PastExams()
        m_ExamFilter = False
    End Sub

    Private Sub DTPExamFrom_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpExamTo.ValueChanged

        If Not m_ResetFlag Then
            _Defaultload = False
            m_ExamFilter = True

            Fill_PastExams()
            m_ExamFilter = False
        End If
    End Sub

    Private Sub DTPExamTo_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpExamFrom.ValueChanged

        If Not m_ResetFlag Then
            _Defaultload = False
            m_ExamFilter = True

            Fill_PastExams()
            m_ExamFilter = False
        End If

    End Sub

    Private Sub ResetExamFilterControls()
        m_ResetFlag = True
        FillExamTypeCombo()
        FillExamProviderCombo()
        dtpExamFrom.ResetText()
        dtpExamTo.ResetText()
        Fill_TemplateSpecility()
        'cmbExamProvider.SelectedIndex = 0
        'cmbExamtype.SelectedIndex = 0
        cmbExamProvider.Text = "All"
        cmbExamStatus.Text = "All"
        m_ResetFlag = False
        txtExamName.Text = ""

        chkDateFilter.Checked = False
        dtpExamFrom.Enabled = False
        dtpExamTo.Enabled = False
    End Sub

    '''''
    '20120824-Tempalte Speciality Combobox Functionality
    Private Sub Fill_TemplateSpecility()
        Dim pExam As New clsPatientExams()
        Dim dt As DataTable = pExam.GetAllTemplateSpecility()
        Dim str As String = dt.DefaultView.Sort()
        Dim r As DataRow
        r = dt.NewRow
        r.Item("sDescription") = "All"
        r.Item("nCategoryId") = 0
        dt.Rows.InsertAt(r, 0)

        Try
            cmbTemplateSpeciality.DataSource = dt.DefaultView
            cmbTemplateSpeciality.ValueMember = dt.Columns(0).ColumnName
            cmbTemplateSpeciality.DisplayMember = dt.Columns(1).ColumnName
            cmbTemplateSpeciality.SelectedIndex = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If IsNothing(dt) = False Then
                dt = Nothing
            End If

            If IsNothing(pExam) = False Then
                pExam.Dispose()
                pExam = Nothing
            End If
        End Try
    End Sub

    Private Sub cmbTemplateSpeciality_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplateSpeciality.SelectionChangeCommitted
        _Defaultload = False
        m_ExamFilter = True
        Fill_PastExams()
        m_ExamFilter = False
    End Sub
    '''''

#End Region

    Public Sub Fill_PastExams()
        ''dhruv 20091130
        ''To avoid the flickering effect
        C1PatientExam.Redraw = False
        Dim dtPatientDetails As DataTable
        Dim objPatientDetail As New clsPatientDetails
        ''''''''''''''
        If cmbExamStatus.Text.Trim = "All" AndAlso cmbExamProvider.Text.Trim = "All" AndAlso cmbTemplateSpeciality.Text.Trim = "All" AndAlso txtExamName.Text.Trim <> "" Then
            m_ExamFilter = False
        Else
            m_ExamFilter = True
        End If
        ''''''''''''''
        If m_ExamFilter Then
            If _Defaultload = True Then '' use to show by default all past exams when loading synopsis form 
                dtPatientDetails = objPatientDetail.Fill_PastExams(pID)
            Else
                If chkDateFilter.Checked Then
                    dtPatientDetails = objPatientDetail.Fill_PastExams(pID, cmbExamProvider.SelectedValue, dtpExamFrom.Value, dtpExamTo.Value, Replace(cmbExamStatus.SelectedValue, "'", "''")) 'Aniket: Resolved Issue 46695
                    _Defaultload = False
                Else
                    dtPatientDetails = objPatientDetail.Fill_PastExams(pID, cmbExamProvider.SelectedValue, Replace(cmbExamStatus.SelectedValue, "'", "''")) 'Aniket: Resolved Issue 46695
                End If

                End If
        Else
                dtPatientDetails = objPatientDetail.Fill_PastExams(pID)
        End If
        objPatientDetail.Dispose()
        objPatientDetail = Nothing
        If (IsNothing(dtPatientDetails)) Then
            Exit Sub
            ' SLR: Added to avoid blank tables..
            '     dtPatientDetails = New DataTable()
        End If
        dtPatientDetails.Columns.Add("RoleOfProvider", GetType(String))
        dtPatientDetails.AcceptChanges()

        Dim dv As New DataView(dtPatientDetails)
        'dv.RowFilter = " Category = '" & strCategory & "'"
        If (cmbTemplateSpeciality.Text <> "All" AndAlso cmbTemplateSpeciality.Text <> "") Then
            dv.RowFilter = "Specialty = '" & Replace(cmbTemplateSpeciality.Text, "'", "''") & "'" 'Aniket: Resolved Issue 46695
        End If
        dtPatientDetails = dv.ToTable()
        dv.Dispose()
        dv = Nothing

        Dim strRoles As String = ""
        Dim nExamID As Long = 0
     
        C1PatientExam.DataSource = dtPatientDetails
        C1PatientExam.Cols("nExamID").Visible = False
        C1PatientExam.Cols("nVisitID").Visible = False
        C1PatientExam.ShowCellLabels = True
        gloC1FlexStyle.Style(C1PatientExam, True)

        Dim _width As Integer = C1PatientExam.Width

        C1PatientExam.Cols("nExamID").Width = 0
        C1PatientExam.Cols("nVisitID").Width = 0
        C1PatientExam.Cols("DOS").Width = _width * 0.1
        C1PatientExam.Cols("Exam Name").Width = _width * 0.3
        C1PatientExam.Cols("ReviewedBy").Width = _width * 0.1
        C1PatientExam.Cols("ProviderName").Width = _width * 0.2
        C1PatientExam.Cols("Finished").Width = _width * 0.1
        C1PatientExam.Cols("RoleOfProvider").Width = _width * 0.18


        C1PatientExam.AllowEditing = True
        C1PatientExam.Cols("nExamID").AllowEditing = False
        C1PatientExam.Cols("nVisitID").AllowEditing = False
        C1PatientExam.Cols("DOS").AllowEditing = False
        C1PatientExam.Cols("Exam Name").AllowEditing = False
        C1PatientExam.Cols("Template Name").AllowEditing = False
        C1PatientExam.Cols("ReviewedBy").AllowEditing = False
        C1PatientExam.Cols("ProviderName").AllowEditing = False
        C1PatientExam.Cols("Finished").AllowEditing = False
        C1PatientExam.Cols("Specialty").AllowEditing = False
        C1PatientExam.Cols("RoleOfProvider").AllowEditing = True

        C1PatientExam.Cols("ReviewedBy").Caption = "Reviewed by"
        C1PatientExam.Cols("ProviderName").Caption = "Provider Name"
        C1PatientExam.Cols("RoleOfProvider").Caption = "Role of Provider"

        ''Set combolist for each row in column 'Role of Provider' 
        For k As Integer = 0 To dtPatientDetails.Rows.Count - 1
            Dim objectID As Object = dtPatientDetails.Rows(k)("nExamID")

            If (IsNothing(objectID) = False) Then

                nExamID = Convert.ToInt64(objectID)

                strRoles = GetRoles(nExamID)
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                Try
                    If (C1PatientExam.Styles.Contains("CS_Roles" & k)) Then
                        cStyle = C1PatientExam.Styles("CS_Roles" & k)
                    Else
                        cStyle = C1PatientExam.Styles.Add("CS_Roles" & k) ''style new for every row
                    End If
                Catch ex As Exception
                    cStyle = C1PatientExam.Styles.Add("CS_Roles" & k) ''style new for every row
                End Try




                Dim rgRole As C1.Win.C1FlexGrid.CellRange = C1PatientExam.GetCellRange(k + 1, 9)

                C1PatientExam.ShowButtons = ShowButtonsEnum.Always
                cStyle.ComboList = strRoles
                rgRole.Style = cStyle

                C1PatientExam.SetCellStyle(k + 1, 8, cStyle)
            End If

        Next
        C1PatientExam.Redraw = True

        C1PatientExam.Cols.Move(8, 5)
        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Past Exam viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)
    End Sub

    ''Code added to display role of Provider to grid by Sandip Darade
    ''Get roles of provider for the Exam 
    Private Function GetRoles(ByVal nExamID As Long) As String
        Dim strSelect As String = ""
        Dim dtroles As DataTable = Nothing
        Dim oDB As New DataBaseLayer
        Dim strRoles As String = ""

        Try
            strSelect = "SELECT  (sProviderName + '-' +  sCategory ) AS roleOFprovider   FROM PatientExam_DTL WHERE nExamID = " & nExamID & " ORDER BY  sProviderName "

            'dtroles = New DataTable
            dtroles = oDB.GetDataTable_Query(strSelect)
            If dtroles IsNot Nothing AndAlso dtroles.Rows.Count > 0 Then
                For k As Integer = 0 To dtroles.Rows.Count - 1
                    'If k = 0 Then
                    '    strRoles = Convert.ToString(dtroles.Rows(k)("roleOFprovider")) & "|"
                    'Else
                    '    strRoles += Convert.ToString(dtroles.Rows(k)("roleOFprovider")) & "|"
                    'End If

                    If k = 0 Then
                        strRoles = Convert.ToString(dtroles.Rows(k)("roleOFprovider"))
                    Else
                        strRoles += "|" & Convert.ToString(dtroles.Rows(k)("roleOFprovider"))
                    End If
                Next

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(dtroles) = False) Then
                dtroles.Dispose()
                dtroles = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

        Return strRoles
    End Function


    'Fuction To Populate Promblem list in tree view control
    Public Function PopulateProblemList1(ByVal dt As DataTable)
        Try
            Dim icnt As Int32 = 0

            'call PopulateProblemList Function in class file ClsPatientSynopsis



            'Add Tree viewcontrol Acc. to Problem in Problemlist
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    trProblemList.Nodes.Clear()
                    trProblemList.Nodes.Add("Problem List")
                    Dim myNode As TreeNode
                    myNode = trProblemList.Nodes.Item(0)
                    myNode.ImageIndex = 1
                    myNode.SelectedImageIndex = 1
                    Dim strMaxlength As String = ""
                    For icnt = 0 To dt.Rows.Count - 1
                        Dim mychildnode As TreeNode
                        mychildnode = New TreeNode()
                        mychildnode.Text = CType(dt.Rows.Item(icnt)(0), String) & "-" & CType(dt.Rows.Item(icnt)("DOS"), String)
                        If Len(strMaxlength) < mychildnode.Text.Length Then
                            strMaxlength = mychildnode.Text
                        End If
                        mychildnode.Tag = CType(dt.Rows.Item(icnt)(1), String) & "-" & CType(dt.Rows.Item(icnt)(2), String)
                        mychildnode.ImageIndex = 7
                        mychildnode.SelectedImageIndex = 7

                        myNode.Nodes.Add(mychildnode)
                    Next
                    SetPanelWidth(trProblemList, pnlProblemsWidthMax, strMaxlength, pnlProblemsWidth)
                End If
            End If


            trProblemList.Sort()
            trProblemList.ExpandAll()
            If Not IsNothing(trProblemList.Nodes) Then
                If trProblemList.Nodes.Count > 0 Then
                    trProblemList.SelectedNode = trProblemList.Nodes(0)
                End If

            End If

            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
        Return True

    End Function

    
    'Fuction To Populate LatestMedications list in tree view control
    Public Function PopulateLatestMedications1()
        Try
            Dim icnt As Int32 = 0

            'call PopulateLatestMedications Function in class file ClsPatientSynopsis
            Dim dt As DataTable = c2.PopulateLatestMedications(pID)

            'Add Tree viewcontrol Acc. to Medication List
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    trMedications.Nodes.Clear()
                    trMedications.Nodes.Add("Medication List")
                    Dim myNode As TreeNode
                    myNode = trMedications.Nodes.Item(0)
                    myNode.ImageIndex = 2
                    myNode.SelectedImageIndex = 2
                    For icnt = 0 To dt.Rows.Count - 1
                        Dim mychildnode As TreeNode
                        mychildnode = New TreeNode(CType(dt.Rows.Item(icnt)(0), String))
                        mychildnode.ImageIndex = 7
                        mychildnode.SelectedImageIndex = 7
                        myNode.Nodes.Add(mychildnode)
                    Next
                End If
            End If
            trMedications.ExpandAll()
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
        Return True

    End Function


    'Fuction To Populate Procedures list in tree view control
    Public Function PopulateProcedures1(ByVal dt As DataTable)
        Try
            Dim icnt As Int32 = 0

            'call PopulateProcedures Function in class file ClsPatientSynopsis
            ' Dim dt As DataTable = dsdata.Tables("Procedures")

            'Add Tree viewcontrol Acc. to Procedures List
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    trProcedures.Nodes.Clear()
                    trProcedures.Nodes.Add("Procedure List")
                    Dim myNode As TreeNode
                    myNode = trProcedures.Nodes.Item(0)
                    myNode.ImageIndex = 4
                    myNode.SelectedImageIndex = 4
                    Dim strMaxlength As String = ""
                    For icnt = 0 To dt.Rows.Count - 1
                        If Not IsDBNull(dt.Rows.Item(icnt)(0)) Then
                            If CType(dt.Rows.Item(icnt)(0), String).Trim <> "" Then
                                Dim mychildnode As TreeNode
                                mychildnode = New TreeNode(CType(dt.Rows.Item(icnt)(0), String))
                                mychildnode.Text = mychildnode.Text & "-" & CType(dt.Rows.Item(icnt)("DOS"), String)
                                If Len(strMaxlength) < mychildnode.Text.Length Then
                                    strMaxlength = mychildnode.Text
                                End If
                                'CHANGE THE TAG  FOR THE FUCNTION FOR CHECKING THE gblnICD9Driven SETTING OF ADMIN
                                '20-Jul-15 Aniket: Resolving Bug #86609: gloEMR>Synopsis>Procedure>If exam is finished then it should not allow to Open the DxCPT window and to modify the data.
                                mychildnode.Tag = CType(dt.Rows.Item(icnt)("ExamID"), String) & "-" & CType(dt.Rows.Item(icnt)("visitid"), String) & " - " & CType(dt.Rows.Item(icnt)("DOS"), String) & " - " & CType(dt.Rows.Item(icnt)("ExamName"), String) & " - " & CType(pID, String) & " - " & CType(dt.Rows.Item(icnt)("ExamStatus"), Boolean)
                                mychildnode.ImageIndex = 7
                                mychildnode.SelectedImageIndex = 7
                                myNode.Nodes.Add(mychildnode)
                            End If
                        End If
                    Next
                    SetPanelWidth(trProcedures, pnlProceduresWidthMax, strMaxlength, pnlProceduresWidth)
                End If
            End If

            trProcedures.Sort()
            trProcedures.ExpandAll()

            If Not IsNothing(trProcedures.Nodes) Then
                If trProcedures.Nodes.Count > 0 Then
                    trProcedures.SelectedNode = trProcedures.Nodes(0)
                End If
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
        Return True

    End Function
    Private Sub Splitdata(ByVal strdata As String, ByRef orderid As Int64, ByRef testid As Int64)
        Try
            Dim arr() As String = strdata.Split("-")
            If arr.Length > 0 Then
                orderid = arr(0)
            End If
            If arr.Length > 1 Then
                testid = arr(1)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub PopulateLabs_old()

        Dim _table As New DataTable
        ' Dim osqladpt As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter

        Try
            cnn.ConnectionString = GetConnectionString()
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Labs_GetLabsforPatient"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@npatientid"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = pID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            _table.Load(cmd.ExecuteReader)
            If Not IsNothing(_table) Then
                Dim icnt As Int32
                If _table.Rows.Count > 0 Then

                    Dim mynode As New TreeNode("Orders & Results")
                    trLabs.Nodes.Clear()
                    trLabs.Nodes.Add(mynode)
                    Dim mychildnode1 As TreeNode

                    mynode.ImageIndex = 5
                    mynode.SelectedImageIndex = 5
                    Dim strMaxLength As String = ""
                    For icnt = 0 To _table.Rows.Count - 1
                        mychildnode1 = New TreeNode()
                        mychildnode1.Tag = _table.Rows(icnt)(0).ToString & "-" & _table.Rows(icnt)(1).ToString & "-" & _table.Rows(icnt)("labotd_DateTime").ToString
                        mychildnode1.Text = _table.Rows(icnt)("labtm_Name").ToString
                        If Not IsDBNull(_table.Rows(icnt)(2)) Then
                            mychildnode1.Text = mychildnode1.Text & " (" & _table.Rows(icnt)("labotd_DateTime").ToString & ")"
                        End If
                        If Len(strMaxLength) < mychildnode1.Text.Length Then
                            strMaxLength = mychildnode1.Text
                        End If
                        mychildnode1.ImageIndex = 7
                        mychildnode1.SelectedImageIndex = 7
                        mynode.Nodes.Add(mychildnode1)
                        mychildnode1 = Nothing
                    Next
                    SetPanelWidth(trLabs, pnlLabsWidthMax, strMaxLength, pnlLabsWidth)
                End If
            End If


            _table.Rows.Clear()
            _table.Dispose()
            _table = Nothing
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Dim orderid As Int64 = 0
            Dim testid As Int64 = 0
            If trLabs.Nodes.Count > 0 Then

                If trLabs.Nodes.Item(0).Nodes.Count > 0 Then
                    Dim mynode As TreeNode
                    mynode = trLabs.Nodes.Item(0)

                    Dim strSQL As String = ""
                    For Each childnode As TreeNode In mynode.Nodes
                        Splitdata(childnode.Tag, orderid, testid)
                        cmd = New SqlCommand
                        cmd.Connection = cnn
                        cmd.CommandType = CommandType.Text
                        strSQL = "select isnull(labotrd_ResultName,'')+ Space(1)+ isnull(labotrd_ResultValue,'')+ " _
                                        & " space(1) + isnull(labotrd_ResultUnit,'') from dbo.Lab_Order_Test_ResultDtl " _
                                        & " where labotrd_OrderID=" & orderid & " and labotrd_TestID=" & testid & ""
                        cmd.CommandText = strSQL
                        _table = Nothing

                        _table = New DataTable

                        _table.Load(cmd.ExecuteReader)

                        Dim icnt As Int32

                        For icnt = 0 To _table.Rows.Count - 1
                            Dim mychildnode1 As New TreeNode
                            mychildnode1.Text = _table.Rows(icnt)(0).ToString
                            mychildnode1.ImageIndex = 7
                            mychildnode1.SelectedImageIndex = 7
                            childnode.Nodes.Add(mychildnode1)
                        Next
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                        If Not IsNothing(_table) Then
                            _table.Clear()
                            _table.Dispose()
                            _table = Nothing
                        End If
                        orderid = 0
                        testid = 0
                    Next

                End If
            End If
            'trLabs.Sort()
            trLabs.ExpandAll()
            If Not IsNothing(trLabs.Nodes) Then
                If trLabs.Nodes.Count > 0 Then
                    trLabs.SelectedNode = trLabs.Nodes(0)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If Not IsNothing(osqladpt) Then
            '    osqladpt.Dispose()
            '    osqladpt = Nothing
            'End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
    End Sub
    Public Sub PopulateLabs(ByVal _table As DataTable)

        '    Dim _table As DataTable


        Try


            '  _table = dsdata.Tables("Labs")
            Dim dv As DataView = Nothing
            dv = _table.DefaultView
            If Not IsNothing(_table) Then
                Dim icnt As Int32
                If _table.Rows.Count > 0 Then

                    Dim mynode As New TreeNode("Orders & Results")
                    trLabs.Nodes.Clear()
                    trLabs.Nodes.Add(mynode)
                    Dim mychildnode1 As TreeNode

                    mynode.ImageIndex = 5
                    mynode.SelectedImageIndex = 5
                    Dim strMaxLength As String = ""
                    Dim dtOrder As DataTable

                    dtOrder = dv.ToTable(True, "labotd_OrderID", "labotd_TestID", "labtm_Name", "labotd_DateTime")
                    If Not IsNothing(dtOrder) Then
                        If dtOrder.Rows.Count > 0 Then
                    For icnt = 0 To dtOrder.Rows.Count - 1
                        Dim strFilter As String = ""

                        mychildnode1 = New TreeNode()
                        mychildnode1.Tag = dtOrder.Rows(icnt)(0).ToString & "-" & dtOrder.Rows(icnt)(1).ToString & "-" & dtOrder.Rows(icnt)("labotd_DateTime").ToString
                        mychildnode1.Text = dtOrder.Rows(icnt)("labtm_Name").ToString
                        If Not IsDBNull(dtOrder.Rows(icnt)(2)) Then
                            mychildnode1.Text = mychildnode1.Text & " (" & dtOrder.Rows(icnt)("labotd_DateTime").ToString & ")"
                        End If
                        If Len(strMaxLength) < mychildnode1.Text.Length Then
                            strMaxLength = mychildnode1.Text
                        End If
                        mychildnode1.ImageIndex = 7
                        mychildnode1.SelectedImageIndex = 7
                        mynode.Nodes.Add(mychildnode1)
                        mychildnode1 = Nothing

                        Next
                    End If
                    End If

                    If Not IsNothing(dtOrder) Then
                        dtOrder.Dispose()
                        dtOrder = Nothing
                    End If
                    SetPanelWidth(trLabs, pnlLabsWidthMax, strMaxLength, pnlLabsWidth)
                End If
            End If


            Dim orderid As Int64 = 0
            Dim testid As Int64 = 0
            If trLabs.Nodes.Count > 0 Then

                If trLabs.Nodes.Item(0).Nodes.Count > 0 Then
                    Dim dvResult As DataView = Nothing
                    dvResult = _table.DefaultView
                    Dim mynode As TreeNode
                    mynode = trLabs.Nodes.Item(0)

                    Dim strSQL As String = ""
                    If Not IsNothing(dvResult) Then


                        If dvResult.Table.Rows.Count > 0 Then

                            For Each childnode As TreeNode In mynode.Nodes
                                Splitdata(childnode.Tag, orderid, testid)


                                ' For icnt = 0 To dvResult.Table.Rows.Count - 1
                                '  dvResult.RowFilter = "labtm_Name = '" & testid & "'"


                                dvResult.RowFilter = "labotd_OrderID = '" & orderid & "' and labotd_TestID = '" & testid & "' and ResultDetails <> ''"
                                If Not IsNothing(dvResult.ToTable) Then

                                    If dvResult.ToTable.Rows.Count > 0 Then

                                        For icnt1 As Integer = 0 To dvResult.ToTable.Rows.Count - 1

                                            Dim mychildnode1 As New TreeNode
                                            mychildnode1.Text = dv.ToTable.Rows(icnt1)(4).ToString
                                            mychildnode1.ImageIndex = 7
                                            mychildnode1.SelectedImageIndex = 7
                                            childnode.Nodes.Add(mychildnode1)
                                        Next
                                    End If
                                End If

                                orderid = 0
                                testid = 0
                            Next
                        End If
                    End If
                End If
            End If
            'trLabs.Sort()
            trLabs.ExpandAll()
            If Not IsNothing(trLabs.Nodes) Then
                If trLabs.Nodes.Count > 0 Then
                    trLabs.SelectedNode = trLabs.Nodes(0)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try
    End Sub

    'Fuction To  Populate Labs list in tree view control
    Public Sub PopulateLabs1()
        Try
            Dim icnt As Int32 = 0

            'call PopulateLabs Function in class file ClsPatientSynopsis
            Dim dt As DataTable = c2.PopulateLabs(pID)

            'Add Tree viewcontrol Acc. to Lab list
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    trLabs.Nodes.Clear()
                    trLabs.Nodes.Add("Labs List")
                    Dim myNode As TreeNode
                    myNode = trLabs.Nodes.Item(0)
                    myNode.ImageIndex = 5
                    myNode.SelectedImageIndex = 5
                    For icnt = 1 To dt.Rows.Count - 1
                        Dim mychildnode As TreeNode
                        mychildnode = New TreeNode(CType(dt.Rows.Item(icnt)(0), String))
                        mychildnode.ImageIndex = 7
                        mychildnode.SelectedImageIndex = 7
                        myNode.Nodes.Add(mychildnode)
                    Next
                End If
            End If
            trLabs.ExpandAll()
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
        'Return True

    End Sub

    'Private Sub Fill_ProblemList1()
    '    Dim dtProblemList As DataTable
    '    dtProblemList = c2.Fill_ProblemLists(pID)
    '    'objProblemList = Nothing
    '    With cfgProblemList
    '        .Visible = True
    '        .BringToFront()
    '        .Cols.Count = 8
    '        .Rows.Count = 1
    '        '' Set Fixed Rows
    '        .SetData(0, 0, "ProblemID")
    '        .SetData(0, 1, "DOS")
    '        .SetData(0, 2, "Chief Complaint")
    '        .SetData(0, 3, "Diagnosis")
    '        .SetData(0, 4, "Prescription")
    '        .SetData(0, 5, "VisitID")
    '        .SetData(0, 6, "Status")
    '        .SetData(0, 7, "User")
    '        '''' 
    '        .Cols(0).Width = 0
    '        .Cols(1).Width = .Width * 0.1
    '        .Cols(2).Width = .Width * 0.22
    '        .Cols(3).Width = .Width * 0.4
    '        .Cols(4).Width = .Width * 0.5
    '        .Cols(5).Width = 0
    '        .Cols(6).Width = .Width * 0.09
    '        .Cols(7).Width = .Width * 0.09
    '        '''' 
    '        .Cols(0).TextAlign = TextAlignEnum.LeftCenter
    '        .Cols(1).TextAlign = TextAlignEnum.LeftCenter
    '        .Cols(2).TextAlign = TextAlignEnum.LeftCenter
    '        .Cols(3).TextAlign = TextAlignEnum.LeftCenter
    '        .Cols(4).TextAlign = TextAlignEnum.LeftCenter
    '        .Cols(5).TextAlign = TextAlignEnum.LeftCenter
    '        .Cols(6).TextAlign = TextAlignEnum.LeftCenter
    '        .Cols(7).TextAlign = TextAlignEnum.LeftCenter
    '        ' ''
    '        If IsNothing(dtProblemList) = False Then
    '            For i As Int16 = 0 To dtProblemList.Rows.Count - 1
    '                Dim forecolor As Color
    '                Dim backcolor As Color
    '                Dim status As String = ""
    '                If dtProblemList.Rows(i)("Status") = frmProblemList.Status.Active Then
    '                    forecolor = Color.Red
    '                    ' backcolor = Color.White
    '                    status = "Active"
    '                ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Resolved Then
    '                    forecolor = Color.Green
    '                    ' backcolor = Color.White
    '                    status = "Resolved"
    '                ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Inactive Then
    '                    forecolor = Color.Blue
    '                    ' backcolor = Color.White
    '                    status = "Inactive"
    '                ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Chronic Then
    '                    forecolor = Color.Black
    '                    ' backcolor = Color.White
    '                    status = "Chronic"
    '                End If
    '                Dim r As C1.Win.C1FlexGrid.Row
    '                r = .Rows.Add()
    '                r.StyleNew.ForeColor = forecolor
    '                '  r.StyleNew.BackColor = backcolor
    '                r.Height = 20

    '                .SetData(r.Index, 0, dtProblemList.Rows(i)("nProblemID"))
    '                .SetData(r.Index, 1, dtProblemList.Rows(i)("dtDOS"))
    '                .SetData(r.Index, 2, dtProblemList.Rows(i)("Complaint"))
    '                .SetData(r.Index, 3, dtProblemList.Rows(i)("Diagnosis"))
    '                .SetData(r.Index, 4, dtProblemList.Rows(i)("Prescription"))
    '                .SetData(r.Index, 5, dtProblemList.Rows(i)("VisitID"))
    '                .SetData(r.Index, 6, status)
    '                .SetData(r.Index, 7, dtProblemList.Rows(i)("UserName"))

    '            Next
    '        End If
    '    End With
    'End Sub

    'Private Sub Fill_PatientHistory(ByVal PatientID As Long)
    '    Dim objDashBoard As New clsDoctorsDashBoard
    '    Dim dt As New DataTable
    '    Dim strdetails As String

    '    'If gnVisitID = 0 Then
    '    gnVisitID = GetVisitID(Now)
    '    'End If
    '    If gnVisitID > 0 Then
    '        '''' To Check if Current History Exists
    '        dt = objDashBoard.Fill_History(PatientID, gnVisitID, 0)
    '    End If


    '    If dt.Rows.Count > 0 Then
    '        '''' History Exists for Current Date

    '    Else
    '        '''' If History is Not Exist For Current Date then Check for the Previous Date
    '        dt = objDashBoard.Fill_History(PatientID, gnVisitID, 1)
    '        'dt(0) = VisitID
    '        'dt(1) = VisitDate
    '        If dt.Rows.Count > 0 Then
    '            '' If there Exist a Visit of History for Previous Date Then 
    '            '''' Get the History for that Date
    '            dt = objDashBoard.Fill_History(PatientID, dt.Rows(0)(0), 2)
    '        Else
    '        End If
    '    End If
    '     End With
    '    gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Problem List viewed from DashBoard", gstrLoginName, gstrClientMachineName, pid)
    'End Sub

    Friend Sub Fill_PatientHistory()
        Dim objDashBoard As New clsDoctorsDashBoard
        ' '' ''Dim dt As New DataTable
        'Dim strdetails As String
        Dim nVisitID As Long
        'If gnVisitID = 0 Then
        nVisitID = GetVisitID(Now, pID)
        'End If
        If IsNothing(dtHistory) Then
            If nVisitID > 0 Then
                '''' To Check if Current History Exists
                dtHistory = objDashBoard.Fill_History(pID, nVisitID, 0)
                
            Else

            End If
         
            If (IsNothing(dtHistory)) Then
                dtHistory = objDashBoard.Fill_History(pID, nVisitID, 1)
                If (IsNothing(dtHistory)) Then
                Else
                    If dtHistory.Rows.Count > 0 Then
                        '' If there Exist a Visit of History for Previous Date Then 
                        '''' Get the History for that Date
                        dtHistory = objDashBoard.Fill_History(pID, dtHistory.Rows(0)(0), 2)
                    Else
                    End If
                End If
            Else
                If dtHistory.Rows.Count > 0 Then
                    '''' History Exists for Current Date

                Else
                    '''' If History is Not Exist For Current Date then Check for the Previous Date
                    dtHistory = objDashBoard.Fill_History(pID, nVisitID, 1)
                    'dtHistory(0) = VisitID
                    'dtHistory(1) = VisitDate
                    If dtHistory.Rows.Count > 0 Then
                        '' If there Exist a Visit of History for Previous Date Then 
                        '''' Get the History for that Date
                        dtHistory = objDashBoard.Fill_History(pID, dtHistory.Rows(0)(0), 2)
                    Else
                    End If
                End If
            End If
           
            oSearchAllergiesCtl.IntialiseDatatable(dtHistory)
            Fill_History(dtHistory)
        End If

        objDashBoard = Nothing

        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient History viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)
    End Sub
    Private Sub Fill_History(ByVal dt As DataTable)

        gloC1FlexStyle.Style(C1dgPatientDetails) ''Ojeswini_01302010

        ''sHistoryCategory, sHistoryItem, sComments, nVisitID, dtVisitDate, nDrugID
        Dim ds As DataSet = Nothing
        Try
            Dim objclsDB As New clsDoctorsDashBoard

            Dim _categorytype As String = ""
            Dim stronsetActiveStatus As String = ""
            Dim _arrOnsetActive() As String
            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            ds = objclsDB.Fill_StandardHistoryTypes()
            With C1dgPatientDetails

                .AllowSorting = AllowSortingEnum.None
                .Visible = True
                .BringToFront()
                '.Cols.Count = 10
                ''Sanjog
                .Cols.Count = 13
                ''Sanjog
                .Rows.Count = 1
                C1dgPatientDetails.ShowCellLabels = True
                '' Set Fixed Rows
                .SetData(0, 0, "VisitID")
                .Cols(0).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 1, "Category_Hidden")
                .Cols(1).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 2, "Visit Date_Hidden")
                .Cols(2).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 3, "Visit Date")
                .Cols(3).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 4, "Category")
                .Cols(4).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 5, "History")
                .Cols(5).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 6, "Comments")
                .Cols(6).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 7, "") 'Reaction
                .Cols(7).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 8, "Status")
                .Cols(8).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 9, "DrugID")
                .Cols(9).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 10, "Smoking Status")
                .Cols(10).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 11, "Occur Date") '25-Jun-23 Aniket: Change caption to Occur Date
                .Cols(11).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 12, "Date")
                .Cols(12).TextAlign = TextAlignEnum.GeneralCenter

                '''' 
                '(0, "VisitID")(1, "Category_Hidden")(2, "Visit Date_Hidden")(3, "Visit Date")(4, "Category")
                '(5, "History")(6, "Comments")(7,"Reaction")(8,"Status")(9, "DrugID")
                .Cols(0).Width = .Width * 0
                .Cols(1).Width = .Width * 0
                .Cols(2).Width = .Width * 0
                .Cols(3).Width = .Width * 0.1
                .Cols(4).Width = .Width * 0.12
                .Cols(5).Width = .Width * 0.2
                .Cols(6).Width = .Width * 0.2
                .Cols(7).Width = .Width * 0.1
                .Cols(8).Width = .Width * 0.06
                .Cols(9).Width = 0
                If gblnShowSmokingColumn Then
                    .Cols(10).Width = .Width * 0.09
                Else
                    .Cols(10).Width = 0
                End If
                .Cols(11).Width = .Width * 0.06
                .Cols(12).Width = .Width * 0.1

                '''' 
                .BeginInit()
                If IsNothing(dt) = False Then
                    For i As Int16 = 0 To dt.Rows.Count - 1


                        Dim _Row As Integer = 0
                        'Dim _tempID As Long
                        For j As Int16 = 1 To .Rows.Count - 1
                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                            If .GetData(j, 1).ToString.ToUpper = dt.Rows(i)("sHistoryCategory").ToString.ToUpper Then

                                '' TO Insert the New Item At the END of the CAtegory
                                Try
                                    'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                                    If .GetData(j, 1).ToString.ToUpper <> .GetData(j + 1, 1).ToString.ToUpper Then
                                        '''' If The Current Category ID Is Not Matchs with the thw Category Name  at Next ROW 
                                        '' Then Add new Row at Just After the Current Row i.e At the END of the Category
                                        .Rows.Insert(j + 1)
                                        _Row = j + 1
                                        .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                                        Exit For
                                    End If
                                Catch ex As Exception
                                    '''' If The System Does Not Get the ROW At (i+1) Position then it Throws the Exception
                                    '' i.e we ahve to add the Row at the End 
                                    .Rows.Insert(j + 1)
                                    _Row = j + 1
                                    .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                                    Exit For
                                End Try
                            End If
                        Next

                        Dim objclsPatientHistory As New clsPatientHistory
                        _categorytype = Convert.ToString(dt.Rows(i)(0)).Trim
                        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                        Else
                            If Convert.ToString(dt.Rows(i)("sHistoryType")).Trim = "" Then
                                _categorytype = Convert.ToString(dt.Rows(i)(0)).Trim
                                _categorytype = objclsPatientHistory.getHistoryTypefromcategorymaster_Other(_categorytype, ds)
                            Else
                                _categorytype = Convert.ToString(dt.Rows(i)("sHistoryType")).Trim
                            End If

                        End If
                        
                        '(0, "VisitID")(1, "Category_Hidden")(2, "Visit Date_Hidden")(3, "Visit Date")(4, "Category")
                        '(5, "History")(6, "Comments")(7,"Reaction")(8,"Status")(9, "DrugID")
                        If _Row = 0 Then ''  Category Is Not exists
                            .Rows.Add()
                            _Row = .Rows.Count - 1
                            .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                            .SetData(_Row, 1, dt.Rows(i)("sHistoryCategory"))
                            .SetData(_Row, 2, dt.Rows(i)("dtVisitDate"))
                            If _Row = 1 Then
                                .SetData(_Row, 3, dt.Rows(i)("dtVisitDate"))
                            End If
                            .SetData(_Row, 4, dt.Rows(i)("sHistoryCategory"))


                            Dim asgTask As C1.Win.C1FlexGrid.CellStyle
                            Try
                                If (C1dgPatientDetails.Styles.Contains("asgTask")) Then
                                    asgTask = C1dgPatientDetails.Styles("asgTask")
                                Else
                                    asgTask = C1dgPatientDetails.Styles.Add("asgTask")
                                    asgTask.Font = gloGlobal.clsgloFont.getFontFromExistingSource(.Font, FontStyle.Bold) 'New System.Drawing.Font(.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                                End If
                            Catch ex As Exception
                                asgTask = C1dgPatientDetails.Styles.Add("asgTask")
                                asgTask.Font = gloGlobal.clsgloFont.getFontFromExistingSource(.Font, FontStyle.Bold) 'New System.Drawing.Font(.Font.FontFamily.Name, 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))

                            End Try
                            C1dgPatientDetails.SetCellStyle(_Row, 7, asgTask)

                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                            If Convert.ToString(dt.Rows(i)("sHistoryCategory")).ToUpper = "ALLERGIES" Then
                                .SetData(_Row, 7, "Reaction")
                                'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                            ElseIf Convert.ToString(dt.Rows(i)("sHistoryCategory")).ToUpper = "FAMILY HISTORY" Then
                                .SetData(_Row, 7, "Family member")
                            ElseIf Convert.ToString(dt.Rows(i)("sHistoryCategory")).ToUpper = "OB INITIAL PHYSICAL EXAMINATION" Then
                                .SetData(_Row, 7, "Initial Physical Exam")
                            Else
                                .SetData(_Row, 7, "")
                            End If

                            .Rows.Insert(_Row + 1)
                            _Row = _Row + 1
                        End If
                        '(0, "VisitID")(1, "Category_Hidden")(2, "Visit Date_Hidden")(3, "Visit Date")(4, "Category")
                        '(5, "History")(6, "Comments")(7,"Reaction")(8,"Status")(9, "DrugID")
                        .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                        .SetData(_Row, 1, dt.Rows(i)("sHistoryCategory"))
                        .SetData(_Row, 2, dt.Rows(i)("dtVisitDate"))
                        .SetData(_Row, 5, dt.Rows(i)("sHistoryItem"))
                        .SetData(_Row, 6, dt.Rows(i)("sComments"))
                        .SetData(_Row, 7, "")
                        .SetData(_Row, 11, dt.Rows(i)("OnsetDate"))
                        'Sanjog
                        .SetData(_Row, 12, dt.Rows(i)("DOE_Allergy"))
                        'Sanjog
                        Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                        Dim rgReaction As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, 7, _Row, 7)
                        Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, 8, _Row, 8)
                        ''If the category is allergy then insert combox and checkbox in flexgrid 
                        IsActive = False
                        IsOnsetDate = False
                        If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then
                            IsActive = True
                            IsOnsetDate = False
                        Else
                            If _categorytype <> "" Then
                                If _categorytype.Length > 2 Then


                                    stronsetActiveStatus = objclsPatientHistory.CheckHistoryTypeinStandardTable_other(_categorytype, ds)
                                    _arrOnsetActive = stronsetActiveStatus.Split(",")
                                    If IsNothing(_arrOnsetActive) = False Then
                                        If _arrOnsetActive.Length >= 1 Then
                                            IsOnsetDate = _arrOnsetActive.GetValue(0)
                                            If _arrOnsetActive.Length > 1 Then
                                                IsActive = _arrOnsetActive.GetValue(1)
                                            End If

                                        End If
                                    End If
                                End If
                            End If

                            End If

                        If IsActive AndAlso _categorytype = "All" Then
                            Dim strReaction As String = ""
                            Dim strActive As String = ""
                            If dt.Rows(i)("sReaction") <> "" Then
                                Dim arr() As String 'Srting Array
                                arr = Split(dt.Rows(i)("sReaction"), "|")
                                strReaction = arr.GetValue(0)
                                If (arr.Length > 1) Then
                                    strActive = arr.GetValue(1)
                                End If

                            End If

                            Dim strReactions As String = " "


                            Dim dtReaction As DataTable
                            dtReaction = objclsPatientHistory.GetAllCategory("Reaction")

                            If IsNothing(dtReaction) = False Then
                                For k As Int16 = 0 To dtReaction.Rows.Count - 1
                                    strReactions = strReactions & "|" & dtReaction.Rows(k)(1)
                                Next
                                dtReaction.Dispose()
                                dtReaction = Nothing
                            End If


                            rgReaction.Style = cStyle
                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)

                            .Rows(_Row).Height = .Rows.DefaultSize * arrReaction.Length - 1
                            .SetData(_Row, 7, strReaction)
                            '.SetData(_Row, 7, strReaction)

                            If strActive = "Active" Then
                                .SetCellCheck(_Row, 8, CheckEnum.Checked)
                            End If
                            ''20130215::Added for Showing Family Member in family history grid
                        ElseIf _categorytype = "Fam" Then

                            Dim strFamily As String = ""
                            Dim strFamilyActive As String = ""
                            If dt.Rows(i)("sReaction") <> "" Then
                                Dim arr() As String 'Srting Array
                                Dim arr1() As String 'Srting Array
                                arr = Split(dt.Rows(i)("sReaction"), "|")
                                arr1 = arr(0).Split(":")

                                If arr1.Length > 0 Then
                                    strFamily = arr1(0)
                                End If

                                If (arr.Length > 1) Then
                                    strFamilyActive = arr.GetValue(1)
                                End If
                            End If
                            Try
                                If (.Styles.Contains("Family History")) Then
                                    cStyle = .Styles("Family History")
                                Else
                                    cStyle = .Styles.Add("Family History")
                                End If
                            Catch ex As Exception
                                cStyle = .Styles.Add("Family History")
                            End Try

                            cStyle.ComboList = strFamily
                            rgReaction.Style = cStyle

                            If IsActive Then
                                rgActive.StyleNew.DataType = GetType(Boolean)
                                rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                                rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                                If strFamilyActive = "Active" Then
                                    .SetCellCheck(_Row, 9, CheckEnum.Checked)
                                End If
                            End If

                            Dim arrReaction As String()
                            arrReaction = strFamily.Split(vbNewLine)
                            C1dgPatientDetails.Rows(_Row).Height = C1dgPatientDetails.Rows.DefaultSize * arrReaction.Length - 1
                            C1dgPatientDetails.SetData(_Row, 7, strFamily)
                        ElseIf _categorytype = "OB Initial Physical Examination" Then

                            Dim strExam As String = ""
                            Dim strExamActive As String = ""
                            If dt.Rows(i)("sReaction") <> "" Then
                                Dim arr() As String 'Srting Array

                                arr = Split(dt.Rows(i)("sReaction"), "|")

                                If arr.Length > 0 Then
                                    strExam = arr.GetValue(0)
                                End If

                                If (arr.Length > 1) Then
                                    strExamActive = arr.GetValue(1)
                                End If
                            End If

                            Dim arrReaction As String()
                            arrReaction = strExam.Split(vbNewLine)
                            C1dgPatientDetails.Rows(_Row).Height = C1dgPatientDetails.Rows.DefaultSize * arrReaction.Length - 1
                            C1dgPatientDetails.SetData(_Row, 7, strExam)

                        ElseIf IsActive Then

                            Dim strReaction As String = ""
                            Dim strActive As String = ""
                            If dt.Rows(i)("sReaction") <> "" Then
                                Dim arr() As String 'Srting Array
                                arr = Split(dt.Rows(i)("sReaction"), "|")
                                strReaction = arr.GetValue(0)
                                If (arr.Length > 1) Then
                                    strActive = arr.GetValue(1)
                                End If

                            End If


                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter



                            If strActive = "Active" Then
                                .SetCellCheck(_Row, 8, CheckEnum.Checked)
                            End If

                        Else

                            rgReaction.Style = Nothing
                            rgActive.Style = Nothing

                        End If

                            ''Sanjog
                            'Bug #61329: 00000599 : patient has multiple history items in different casing the system will throw an error.
                            If InStr(dt.Rows(i)("sHistoryCategory").ToString.ToUpper, "SMOKING", CompareMethod.Text) = 1 Then

                                Dim strSmoking As String = ""
                                Dim strSmokeActive As String = ""
                                If dt.Rows(i)("sReaction") <> "" Then
                                    Dim arr() As String 'Srting Array
                                    arr = Split(dt.Rows(i)("sReaction"), "|")
                                    strSmoking = arr.GetValue(0)
                                    If (arr.Length > 1) Then
                                        strSmokeActive = arr.GetValue(1)
                                    End If
                                End If

                                Dim strSmokings As String = " "


                                Dim dtSmoking As DataTable
                                Dim objclsPHistory As New clsPatientHistory
                                dtSmoking = objclsPHistory.GetAllCategory("Smoking Status Type")
                                If Not IsNothing(objclsPHistory) Then
                                    objclsPHistory.Dispose()
                                    objclsPHistory = Nothing
                                End If

                                If IsNothing(dtSmoking) = False Then
                                    For k As Int16 = 0 To dtSmoking.Rows.Count - 1
                                        strSmokings = strSmokings & "|" & dtSmoking.Rows(k)(1)
                                    Next
                                    dtSmoking.Dispose()
                                    dtSmoking = Nothing
                                End If
                                Try
                                    If (.Styles.Contains("Smoking Status")) Then
                                        cStyle = .Styles("Smoking Status")
                                    Else
                                        cStyle = .Styles.Add("Smoking Status")
                                    End If
                                Catch ex As Exception
                                    cStyle = .Styles.Add("Smoking Status")
                                End Try

                                cStyle.ComboList = strSmokings


                                rgReaction.Style = cStyle

                                Dim arrSmoking As String()
                                arrSmoking = strSmoking.Split(vbNewLine)

                                C1dgPatientDetails.Rows(_Row).Height = C1dgPatientDetails.Rows.DefaultSize * arrSmoking.Length - 1
                                C1dgPatientDetails.SetData(_Row, 10, strSmoking)

                                '.SetData(_Row, 8, strSmoking)
                                'If strSmokeActive = "Active" Then
                                '    .SetCellCheck(_Row, 9, CheckEnum.Checked)
                                'End If
                                'objclsPHistory.Dispose()
                                'objclsPHistory = Nothing
                            Else
                                'rgReaction.Style = Nothing
                                'rgActive.Style = Nothing

                            End If
                            ''Sanjog

                            .SetData(_Row, 9, dt.Rows(i)("nDrugID"))
                            '''''''''''''''''''' End Pramod
                            .Row = _Row
                            If Not IsNothing(objclsPatientHistory) Then
                                objclsPatientHistory.Dispose()
                                objclsPatientHistory = Nothing
                            End If

                    Next
                End If
                .EndInit()
            End With

            objclsDB = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(ds) = False) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Sub

    'Private Sub FillCategoryTestGroups()
    '    Dim oDB As gloStream.gloDataBase.gloDataBase
    '    'Dim oDataReader As SqlClient.SqlDataReader
    '    Dim ds As New DataSet
    '    Dim _strSQL As String
    '    Dim _Categories As New Collection
    '    Dim _Groups As New Collection
    '    Dim _Tests As New Collection

    '    Dim _SubTests As New Collection

    '    Dim oFindNode As C1.Win.C1FlexGrid.Node
    '    Dim oTempNode As C1.Win.C1FlexGrid.Node
    '    Dim _tmpRow As Integer
    '    Dim cStyle As C1.Win.C1FlexGrid.CellStyle
    '    Dim dtcategories As New DataTable
    '    Dim objPatientSynopsis As New ClsPatientSynopsis
    '    With C1OrderDetails
    '        .Rows.Count = 1
    '        .Rows.Fixed = 1
    '        .Cols.Count = COLUM_COUNT
    '        .Cols.Fixed = 0
    '        .Rows(.Rows.Count - 1).Height = 21

    '        .Tree.Column = COLUM_NAME
    '        .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '        .Tree.LineStyle = Drawing2D.DashStyle.Solid
    '        .Tree.Indent = 15

    '        '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
    '        '.Redraw = False

    '        'Fill Categories
    '        'oDB = New gloStream.gloDataBase.gloDataBase
    '        'oDB.Connect(GetConnectionString)
    '        ''oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '1' AND lm_category_Description IS NOT NULL ")
    '        '_strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
    '        '        & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
    '        '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
    '        '        & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & pid & ") " _
    '        '        & " ORDER BY lm_category_Description "
    '        'ds = oDB.ReadCatRecords(_strSQL)
    '        'oDB.Disconnect()
    '        'oDB = Nothing
    '        dtcategories = objPatientSynopsis.CategoryName_Summary(pID)
    '        If IsNothing(dtcategories) = False Then
    '            For i As Integer = 0 To dtcategories.Rows.Count - 1
    '                If IsDBNull(dtcategories.Rows(i)("lm_category_Description")) = False Then
    '                    .Rows.Add()
    '                    With .Rows(.Rows.Count - 1)
    '                        .AllowEditing = False
    '                        .ImageAndText = True
    '                        .Height = 24
    '                        .IsNode = True
    '                        ''.Style = FillControl.Styles("CS_Category")
    '                        .Node.Level = 0
    '                        .Node.Data = dtcategories.Rows(i)("lm_category_Description")
    '                        .Node.Key = dtcategories.Rows(i)("lm_test_CategoryID")
    '                    End With

    '                    .SetData(.Rows.Count - 1, COLUM_IDENTITY, "C" & dtcategories.Rows(i)("lm_test_CategoryID"))
    '                    .SetData(.Rows.Count - 1, COLUM_NUMVALUE, Nothing)
    '                    .SetData(.Rows.Count - 1, COLUM_ID, dtcategories.Rows(i)("lm_test_CategoryID"))
    '                    .SetData(.Rows.Count - 1, COLUM_TESTGROUPFLAG, "C")
    '                    .SetData(.Rows.Count - 1, COLUM_LEVELNO, 0)
    '                    .SetData(.Rows.Count - 1, COLUM_GROUPNO, 0)
    '                    _Categories.Add(dtcategories.Rows(i)("lm_category_Description"))
    '                End If
    '            Next
    '        End If


    '        'Fill Groups
    '        For i As Int16 = 1 To _Categories.Count
    '            '_strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
    '            '        & " FROM LM_Test LEFT OUTER JOIN " _
    '            '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
    '            '        & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
    '            '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
    '            '        & " WHERE (LM_Orders.lm_Patient_ID =" & pid & ") AND  " _
    '            '        & " (LM_Category.lm_category_Description = '" & Replace(_Categories(i), "'", "''") & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
    '            '        & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "

    '            'oDB = New gloStream.gloDataBase.gloDataBase
    '            'oDB.Connect(GetConnectionString)
    '            'dtcategories = oDB.ReadQueryRecordAsDataSet(_strSQL)
    '            'oDB.Disconnect()
    '            'oDB = Nothing
    '            dtcategories = objPatientSynopsis.TestDetails_Summary(pID, _Categories(i))

    '            If IsNothing(dtcategories) = False Then
    '                For j As Integer = 0 To dtcategories.Rows.Count - 1
    '                    'If dtcategories.Rows(j)("lm_test_GroupNo").ToString = 0 Then
    '                    oFindNode = GetC1UniqueNode("C" & dtcategories.Rows(j)("lm_test_CategoryID").ToString, C1OrderDetails)
    '                    If Not oFindNode Is Nothing Then

    '                        '' Check For Duplicate Nodes Under the same Group
    '                        oTempNode = GetC1UniqueNode(dtcategories.Rows(j)("lm_test_TestGroupFlag").ToString & dtcategories.Rows(j)("lm_test_GroupNo").ToString, C1OrderDetails)
    '                        If IsNothing(oTempNode) = True Then
    '                            '' If Group Node is Not Exist then Add the Group Node
    '                            oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dtcategories.Rows(j)("GroupName"))
    '                            '//.Style = FillControl.Styles("CS_Category")
    '                            _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '                            If Not _tmpRow = -1 Then
    '                                .Rows(_tmpRow).AllowEditing = False
    '                                .Rows(_tmpRow).ImageAndText = True
    '                                .Rows(_tmpRow).Height = 24
    '                                .SetData(_tmpRow, COLUM_IDENTITY, dtcategories.Rows(j)("lm_test_TestGroupFlag").ToString & dtcategories.Rows(j)("lm_test_GroupNo").ToString)
    '                                .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
    '                                .SetData(_tmpRow, COLUM_ID, dtcategories.Rows(j)("lm_test_GroupNo"))
    '                                .SetData(_tmpRow, COLUM_TESTGROUPFLAG, dtcategories.Rows(j)("lm_test_TestGroupFlag"))
    '                                '.SetData(_tmpRow, COLUM_LEVELNO, dtcategories.Rows(j)("lm_test_LevelNo"))
    '                                '.SetData(_tmpRow, COLUM_GROUPNO, dtcategories.Rows(j)("lm_test_GroupNo"))

    '                                _Groups.Add(dtcategories.Rows(j)("lm_test_GroupNo"))

    '                                If dtcategories.Rows(j)("lm_test_TestGroupFlag") = "T" Then
    '                                    ''  IF is Test then 
    '                                    .Rows(_tmpRow).AllowEditing = True
    '                                    .Cols(COLUM_NAME).AllowEditing = False
    '                                    ''  SET TemplateID
    '                                    .SetData(_tmpRow, COLUM_TEMPLATEID, dtcategories.Rows(j)("lm_test_Template_ID"))
    '                                    '' For the Numeric Value
    '                                    C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
    '                                    ''  Insert CheckBox
    '                                    '.SetCellCheck(_tmpRow, COLUM_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
    '                                    .SetCellImage(_tmpRow, COLUM_BUTTON, ImgPatientTab.Images(4))
    '                                    ''  SET ShowComment Button
    '                                    .SetData(_tmpRow, COLUM_BUTTON, "")
    '                                    cStyle = .Styles.Add("BubbleValues")
    '                                    cStyle.ComboList = "..."
    '                                    '.CellButtonImage = imgPatienttab.Images(0)
    '                                    Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
    '                                    rgBubbleValues.Style = cStyle

    '                                    ''''' 20070129 For Fill Diagnosis '
    '                                    Dim csDia As CellStyle = .Styles.Add("Dia")
    '                                    '' Fill Values In ComboBox
    '                                    csDia.ComboList = strDia
    '                                    '''''
    '                                    .Cols(COLUM_DIAGNOSIS).Style = csDia

    '                                    Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
    '                                    rgDig.Style = cStyle

    '                                End If

    '                                _tmpRow = -1
    '                            End If
    '                        End If
    '                        '''''



    '                        '''''' Add Test 
    '                        Dim dttest As New DataTable
    '                        'Dim dsTest As New DataSet
    '                        '_strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
    '                        '            & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, LM_Orders.lm_Status, " _
    '                        '            & " LM_Test.lm_test_Template_ID , LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description " _
    '                        '        & " FROM  LM_Test INNER JOIN " _
    '                        '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
    '                        '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
    '                        '        & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & pid & ") AND  " _
    '                        '        & " (LM_Category.lm_category_Description = '" & Replace(_Categories(i), "'", "''") & "') AND (LM_Test.lm_test_GroupNo =" & dtcategories.Rows(j)("lm_test_GroupNo") & ") " _
    '                        '        & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

    '                        'oDB = New gloStream.gloDataBase.gloDataBase
    '                        'oDB.Connect(GetConnectionString)
    '                        'dsTest = oDB.ReadQueryRecordAsDataSet(_strSQL)
    '                        'oDB.Disconnect()
    '                        'oDB = Nothing
    '                        dttest = objPatientSynopsis.DetailDescription_Summary(pID, _Categories(i), dtcategories.Rows(j)("lm_test_GroupNo"))
    '                        If IsNothing(dttest) = False Then
    '                            For l As Integer = 0 To dttest.Rows.Count - 1
    '                                'If dtcategories.Rows(j)("lm_test_GroupNo").ToString = 0 Then
    '                                oFindNode = GetC1UniqueNode("G" & dtcategories.Rows(j)("lm_test_GroupNo"), C1OrderDetails)
    '                                If Not oFindNode Is Nothing Then
    '                                    '' Check For Duplicate Nodes Under the same Group
    '                                    oTempNode = GetC1UniqueNode(dttest.Rows(l)("lm_test_TestGroupFlag").ToString & dttest.Rows(l)("lm_test_ID").ToString, C1OrderDetails)
    '                                    If IsNothing(oTempNode) = False Then
    '                                        '' If Node is Alredy Exixst then Exit For
    '                                        Exit For
    '                                    End If
    '                                    '''''

    '                                    oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dttest.Rows(l)("lm_test_Name"))
    '                                    '//.Style = FillControl.Styles("CS_Category")
    '                                    _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
    '                                    If Not _tmpRow = -1 Then
    '                                        .Rows(_tmpRow).AllowEditing = False
    '                                        .Rows(_tmpRow).ImageAndText = True
    '                                        .Rows(_tmpRow).Height = 24
    '                                        .SetData(_tmpRow, COLUM_IDENTITY, dttest.Rows(l)("lm_test_TestGroupFlag").ToString & dttest.Rows(l)("lm_test_ID").ToString)
    '                                        .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
    '                                        .SetData(_tmpRow, COLUM_ID, dttest.Rows(l)("lm_test_ID"))
    '                                        .SetData(_tmpRow, COLUM_TESTGROUPFLAG, dttest.Rows(l)("lm_test_TestGroupFlag"))
    '                                        '.SetData(_tmpRow, COLUM_LEVELNO, dtcategories.Rows(j)("lm_test_LevelNo"))
    '                                        ' .SetData(_tmpRow, COLUM_GROUPNO, dtcategories.Rows(j)("lm_test_GroupNo"))
    '                                        .SetData(_tmpRow, COLUM_UNIT, dttest.Rows(l)("lm_test_Dimension"))

    '                                        _Tests.Add(dttest.Rows(l)("lm_test_Name"))

    '                                        If dttest.Rows(l)("lm_test_TestGroupFlag") = "T" Then
    '                                            ''  IF is Test then 
    '                                            .Rows(_tmpRow).AllowEditing = True
    '                                            .Cols(COLUM_NAME).AllowEditing = False
    '                                            '' For the Numeric Value
    '                                            C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
    '                                            ''  SET TemplateID
    '                                            .SetData(_tmpRow, COLUM_TEMPLATEID, dttest.Rows(l)("lm_test_Template_ID"))

    '                                            ''  Insert CheckBox
    '                                            '.SetCellCheck(_tmpRow, COLUM_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

    '                                            .SetCellImage(_tmpRow, COLUM_BUTTON, ImgPatientTab.Images(4))

    '                                            ''  SET ShowComment Button
    '                                            .SetData(_tmpRow, COLUM_BUTTON, "")
    '                                            cStyle = .Styles.Add("BubbleValues")
    '                                            cStyle.ComboList = "..."

    '                                            Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
    '                                            rgBubbleValues.Style = cStyle

    '                                            ''''' 20070129 For Fill Diagnosis
    '                                            Dim csDia As CellStyle = .Styles.Add("Dia")
    '                                            '' Fill Values In ComboBox
    '                                            csDia.ComboList = strDia
    '                                            '''''
    '                                            .Cols(COLUM_DIAGNOSIS).Style = csDia

    '                                            Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
    '                                            rgDig.Style = cStyle
    '                                            '' Set Associated Diagnosis with this Order
    '                                            .SetData(_tmpRow, COLUM_DIAGNOSIS, dttest.Rows(l)("lm_sICD9Code") & "-" & dttest.Rows(l)("lm_sICD9Description"))
    '                                            ''''''''''''''

    '                                            If IsDBNull(dttest.Rows(l)("lm_Result")) = False Then
    '                                                If IsNothing(dttest.Rows(l)("lm_Result")) = False Then
    '                                                    ''''' If Order comments are entered then Indicate it by ForeColor as RED
    '                                                    .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Red
    '                                                Else
    '                                                    ''''' If Order comments are NOT entered then Indicate it by ForeColor as GREEN
    '                                                    .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
    '                                                End If
    '                                            Else
    '                                                .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
    '                                            End If

    '                                        End If

    '                                        _tmpRow = -1
    '                                    End If
    '                                End If
    '                                ' End If
    '                            Next
    '                        End If
    '                        '' Add Test Close
    '                    End If
    '                    ' End If
    '                Next
    '            End If
    '        Next

    '        .Cols(COLUM_NAME).AllowEditing = False
    '        .Cols(COLUM_IDENTITY).AllowEditing = False
    '        .Cols(COLUM_NUMVALUE).AllowEditing = False
    '        .Cols(COLUM_ID).AllowEditing = False
    '        .Cols(COLUM_TESTGROUPFLAG).AllowEditing = False
    '        .Cols(COLUM_LEVELNO).AllowEditing = False
    '        .Cols(COLUM_GROUPNO).AllowEditing = False
    '        .Cols(COLUM_ISFINISHED).AllowEditing = False
    '        .Cols(COLUM_UNIT).AllowEditing = False
    '        .Cols(COLUM_DIAGNOSIS).AllowEditing = False
    '        .Cols(COLUM_DMSID).AllowEditing = False

    '        .SetData(0, COLUM_NAME, "Tests")
    '        .SetData(0, COLUM_IDENTITY, "Identity")
    '        .SetData(0, COLUM_NUMVALUE, "Value")
    '        .SetData(0, COLUM_BUTTON, "Comments")
    '        .SetData(0, COLUM_ID, "ID")
    '        .SetData(0, COLUM_TESTGROUPFLAG, "Flag")
    '        .SetData(0, COLUM_LEVELNO, "Level No")
    '        .SetData(0, COLUM_GROUPNO, "Group No")
    '        .SetData(0, COLUM_UNIT, "Unit")
    '        .SetData(0, COLUM_DIAGNOSIS, "Diagnosis")
    '        .SetData(0, COLUM_DMSID, "DMS ID")

    '        .Cols(COLUM_NAME).Width = .Width * 0.98 '((.Width / 5) * 2.5) - 20
    '        .Cols(COLUM_NUMVALUE).Width = .Width * 0.0  '((.Width / 5) * 0.5)
    '        .Cols(COLUM_BUTTON).Width = .Width * 0 '((.Width / 5) * 0.2)
    '        .Cols(COLUM_UNIT).Width = .Width * 0  '((.Width / 5) * 0.2)
    '        .Cols(COLUM_DIAGNOSIS).Width = .Width * 0
    '        .Cols(COLUM_DIAGNOSISBUTTON).Width = 0 ''.Width * 0.03


    '        .Cols(COLUM_NAME).Visible = True
    '        .Cols(COLUM_IDENTITY).Visible = False
    '        .Cols(COLUM_NUMVALUE).Visible = False
    '        .Cols(COLUM_ID).Visible = False
    '        .Cols(COLUM_TESTGROUPFLAG).Visible = False
    '        .Cols(COLUM_LEVELNO).Visible = False
    '        .Cols(COLUM_GROUPNO).Visible = False
    '        .Cols(COLUM_TEMPLATEID).Visible = False
    '        .Cols(COLUM_ISFINISHED).Visible = False
    '        .Cols(COLUM_DIAGNOSIS).Visible = True
    '        .Cols(COLUM_DMSID).Visible = False
    '        'c1OrderDetails.getData(0, 2)

    '        '' For the Numeric Value
    '        .Cols(COLUM_NUMVALUE).Format = Format("##0.000")
    '        .Cols(COLUM_NUMVALUE).DataType = System.Type.GetType("System.Decimal")
    '    End With

    'End Sub
#Region "Implant"

    Dim m_patientID As Int64 = 0
    Dim m_ExamID As Int64 = 0
    Dim m_VisitID As Int64 = 0



    Private Const COL_DateofImplant = 0
    Private Const COL_TypeofDevice = 1
    Private Const COL_ProductName = 2
    Private Const COL_DeviceManufacturer = 3
    Private Const COL_ProductSpecifications = 4
    Private Const COL_ProductSerialNumber = 5
    Private Const COL_ManufacturerModelNumber = 6
    Private Const COL_LeadsType = 7
    Private Const COL_DateRemoved = 8
    Private Const COL_PhysicalLocationofDeviceImplant = 9
    Private Const COL_PatientID = 10
    Private Const COL_ExamID = 11
    Private Const COL_VisitID = 12
    Private Const COL_CardiologyDeviceID = 13
    Private Const COL_ClinicID = 14

    Private Const COL_COUNT_Cardio = 15

    ''------------------------------------
    Dim COL_TestTypes = 16
    Dim COL_Procedures As Integer = 17
    Dim COL_LeadLocation As Integer = 18
    Dim COL_ThresholdAtrial As Integer = 19
    Dim COL_ThresholdVentricular As Integer = 20
    Dim COL_SensingAtrial As Integer = 21
    Dim COL_SensingVentricular As Integer = 22
    Dim COL_ImpedanceAtrial As Integer = 23
    Dim COL_ImpedanceVentricular As Integer = 24
    Dim COL_DateofStudyInvisibles As Integer = 25
    Dim COLUMN_COUNT1 As Integer = 26
    Dim COL_CPTCodes As Integer = 27
    ''---------------------------------------

    Dim cStyleDeviceManf As C1.Win.C1FlexGrid.CellStyle
    '' add ctstyle for combo of c1
    Dim cStyleDeviceType As C1.Win.C1FlexGrid.CellStyle

    Dim cStyleProductName As C1.Win.C1FlexGrid.CellStyle
    Dim cStyleLeadtype As C1.Win.C1FlexGrid.CellStyle


    Dim dt_cardio As DataTable

    Public Sub FillCardiologyDevice(ByVal PatientID As Long)
        ''dhruv 20091130
        ''Avoid the flickering
        '' ''C1Cardiology.Redraw = False
        m_patientID = PatientID
        FillImplantDevice()

        '' ''DesignC1Grid()
        ' '' '' FillC1Grid()
        '' ''Dim dtcardio As DataTable = PopulateCardioDeviceData()
        '' ''If Not IsNothing(dtcardio) Then
        '' ''    If dtcardio.Rows.Count > 0 Then
        '' ''        FillC1Grid(dtcardio)
        '' ''        oSearchImplantDevice.IntialiseDatatable(dtcardio)
        '' ''    End If

        '' ''End If
        '' ''C1Cardiology.Redraw = True

    End Sub

    Private Sub DesignC1Grid()

        gloC1FlexStyle.Style(C1Cardiology) ''Ojeswini_01302010

        ' ''Try

        ' ''    With C1Cardiology
        ' ''        C1Cardiology.DataSource = Nothing
        ' ''        .Clear()

        ' ''        Setfont()
        ' ''        .Font = New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
        ' ''        .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        ' ''        .BackColor = System.Drawing.Color.White
        ' ''        .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

        ' ''        .Cols.Count = COL_COUNT_Cardio
        ' ''        .Cols.Fixed = 1

        ' ''        .Rows.Count = 1
        ' ''        .Rows.Fixed = 1
        ' ''        C1Cardiology.ShowCellLabels = True
        ' ''        set col visible 
        ' ''        to visible falsse
        ' ''        .Cols(COL_DateofImplant).Visible = True
        ' ''        .Cols(COL_TypeofDevice).Visible = True
        ' ''        .Cols(COL_ProductName).Visible = True
        ' ''        .Cols(COL_DeviceManufacturer).Visible = True
        ' ''        .Cols(COL_ProductSpecifications).Visible = True
        ' ''        .Cols(COL_ProductSerialNumber).Visible = True
        ' ''        .Cols(COL_ManufacturerModelNumber).Visible = True
        ' ''        .Cols(COL_LeadsType).Visible = True
        ' ''        .Cols(COL_DateRemoved).Visible = True
        ' ''        .Cols(COL_PhysicalLocationofDeviceImplant).Visible = True
        ' ''        .Cols(COL_PatientID).Visible = False
        ' ''        .Cols(COL_ExamID).Visible = False
        ' ''        .Cols(COL_VisitID).Visible = False
        ' ''        .Cols(COL_CardiologyDeviceID).Visible = False
        ' ''        .Cols(COL_ClinicID).Visible = False


        ' ''        set col allow property
        ' ''        .Cols(COL_DateofImplant).AllowEditing = False 'True
        ' ''        .Cols(COL_TypeofDevice).AllowEditing = False 'True
        ' ''        .Cols(COL_ProductName).AllowEditing = False 'True
        ' ''        .Cols(COL_DeviceManufacturer).AllowEditing = False 'True
        ' ''        .Cols(COL_ProductSpecifications).AllowEditing = False 'True
        ' ''        .Cols(COL_ProductSerialNumber).AllowEditing = False 'True
        ' ''        .Cols(COL_ManufacturerModelNumber).AllowEditing = False 'True
        ' ''        .Cols(COL_LeadsType).AllowEditing = False 'True
        ' ''        .Cols(COL_DateRemoved).AllowEditing = False 'True
        ' ''        .Cols(COL_PhysicalLocationofDeviceImplant).AllowEditing = False 'True
        ' ''        .Cols(COL_PatientID).AllowEditing = False
        ' ''        .Cols(COL_ExamID).AllowEditing = False
        ' ''        .Cols(COL_VisitID).AllowEditing = False
        ' ''        .Cols(COL_CardiologyDeviceID).AllowEditing = False
        ' ''        .Cols(COL_ClinicID).AllowEditing = False

        ' ''        set col width            
        ' ''        Dim cWidth As Int32 = Panel7.Width ' pnlPatientDetails.Width

        ' ''        .Cols(COL_HIDDENAPPLICATIONNAME).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_DateofImplant).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_TypeofDevice).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_ProductName).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_DeviceManufacturer).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_ProductSpecifications).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_ProductSerialNumber).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_DateofImplant).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_ManufacturerModelNumber).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_LeadsType).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_DateRemoved).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_PhysicalLocationofDeviceImplant).Width = Convert.ToInt32(cWidth * 0.2)
        ' ''        .Cols(COL_PatientID).Width = 0
        ' ''        .Cols(COL_ExamID).Width = 0
        ' ''        .Cols(COL_VisitID).Width = 0
        ' ''        .Cols(COL_CardiologyDeviceID).Width = 0
        ' ''        .Cols(COL_ClinicID).Width = 0

        ' ''        Dim SendingApplicationName As String = ""
        ' ''        SendingApplicationName = "Sending Application ID|Sending Application Universal ID|Sending Application Type"
        ' ''        Dim devicetype As String = ""
        ' ''        set col datatype
        ' ''        .Cols(COL_DateofImplant).DataType = GetType(DateTime)
        ' ''        .Cols(COL_TypeofDevice).DataType = GetType(String)
        ' ''        .Cols(COL_TypeofDevice).ComboList = devicetype
        ' ''        .Cols(COL_ProductName).DataType = GetType(String)
        ' ''        .Cols(COL_DeviceManufacturer).DataType = GetType(String)
        ' ''        .Cols(COL_ProductSpecifications).DataType = GetType(String)
        ' ''        .Cols(COL_ProductSerialNumber).DataType = GetType(String)
        ' ''        .Cols(COL_ManufacturerModelNumber).DataType = GetType(String)
        ' ''        .Cols(COL_LeadsType).DataType = GetType(String)
        ' ''        .Cols(COL_DateRemoved).DataType = GetType(DateTime)
        ' ''        .Cols(COL_PhysicalLocationofDeviceImplant).DataType = GetType(String)
        ' ''        .Cols(COL_PatientID).DataType = GetType(Int64)
        ' ''        .Cols(COL_ExamID).DataType = GetType(Int64)
        ' ''        .Cols(COL_VisitID).DataType = GetType(Int64)
        ' ''        .Cols(COL_CardiologyDeviceID).DataType = GetType(Int64)
        ' ''        .Cols(COL_ClinicID).DataType = GetType(Int32)

        ' ''        set Heading
        ' ''        .SetData(0, COL_DateofImplant, "Date of Implant")
        ' ''        .SetData(0, COL_TypeofDevice, "Type of Device")
        ' ''        .SetData(0, COL_ProductName, "Product Name ")
        ' ''        .SetData(0, COL_DeviceManufacturer, "Device Manufacturer")
        ' ''        .SetData(0, COL_ProductSpecifications, "Product Specifications")
        ' ''        .SetData(0, COL_ProductSerialNumber, "Product Serial Number")
        ' ''        .SetData(0, COL_ManufacturerModelNumber, "Manufacturer Model Number")
        ' ''        .SetData(0, COL_LeadsType, "Leads Type")
        ' ''        .SetData(0, COL_DateRemoved, "Date Removed")
        ' ''        .SetData(0, COL_PhysicalLocationofDeviceImplant, "Physical Location of Device Implant")
        ' ''        .SetData(0, COL_PatientID, "PatientID")
        ' ''        .SetData(0, COL_ExamID, "ExamID")
        ' ''        .SetData(0, COL_VisitID, "VisitID")
        ' ''        .SetData(0, COL_ExamID, "CardiologyDeviceID")
        ' ''        .SetData(0, COL_VisitID, "ClinicID")

        ' ''        .Cols(Col_SendingApplication).DataType = GetType(String)
        ' ''        .Cols(Col_SendingApplication).ComboList = SendingApplicationName
        ' ''        .Cols(Col_SendingApplication).AllowEditing = True
        ' ''        set Heading
        ' ''        .SetData(0, COL_HIDDENAPPLICATIONNAME, "System ApplicationName")
        ' ''        .SetData(0, COL_ApplName, "Sending Application Name")
        ' ''        .SetData(0, COL_FACILITYNAME, "Sending Facility Name")
        ' ''    End With
        ' ''    C1Client.EndInit()

        ' ''Catch ex As Exception
        ' ''    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SysnopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        ' ''    C1Client.EndInit()
        ' ''    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ' ''End Try
        Try

            With C1Cardiology
                '.Clear()
                C1Cardiology.DataSource = Nothing
                .Clear()

                'Setfont
                .Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
                .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
                .BackColor = System.Drawing.Color.White
                .AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None

                .Cols.Count = COL_COUNT_Cardio
                '.Cols.Fixed = 1

                .Rows.Count = 1
                .Rows.Fixed = 1
                C1Cardiology.ShowCellLabels = True
                'set col visible 
                'to visible falsse
                .Cols(COL_DateofImplant).Visible = True
                .Cols(COL_TypeofDevice).Visible = True
                .Cols(COL_ProductName).Visible = True
                .Cols(COL_DeviceManufacturer).Visible = True
                .Cols(COL_ProductSpecifications).Visible = True
                .Cols(COL_ProductSerialNumber).Visible = True
                .Cols(COL_ManufacturerModelNumber).Visible = True
                .Cols(COL_LeadsType).Visible = True
                .Cols(COL_DateRemoved).Visible = True
                .Cols(COL_PhysicalLocationofDeviceImplant).Visible = True
                .Cols(COL_PatientID).Visible = False
                .Cols(COL_ExamID).Visible = False
                .Cols(COL_VisitID).Visible = False
                .Cols(COL_CardiologyDeviceID).Visible = False
                .Cols(COL_ClinicID).Visible = False


                'set col allow property
                .Cols(COL_DateofImplant).AllowEditing = False 'True
                .Cols(COL_TypeofDevice).AllowEditing = False 'True
                .Cols(COL_ProductName).AllowEditing = False 'True
                .Cols(COL_DeviceManufacturer).AllowEditing = False 'True
                .Cols(COL_ProductSpecifications).AllowEditing = False 'True
                .Cols(COL_ProductSerialNumber).AllowEditing = False 'True
                .Cols(COL_ManufacturerModelNumber).AllowEditing = False 'True
                .Cols(COL_LeadsType).AllowEditing = False 'True
                .Cols(COL_DateRemoved).AllowEditing = False 'True
                .Cols(COL_PhysicalLocationofDeviceImplant).AllowEditing = False 'True
                .Cols(COL_PatientID).AllowEditing = False
                .Cols(COL_ExamID).AllowEditing = False
                .Cols(COL_VisitID).AllowEditing = False
                .Cols(COL_CardiologyDeviceID).AllowEditing = False
                .Cols(COL_ClinicID).AllowEditing = False

                'set col width            
                Dim cWidth As Int32 = Panel7.Width ' pnlPatientDetails.Width

                '.Cols(COL_HIDDENAPPLICATIONNAME).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DateofImplant).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_TypeofDevice).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ProductName).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DeviceManufacturer).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ProductSpecifications).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ProductSerialNumber).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DateofImplant).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_ManufacturerModelNumber).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_LeadsType).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_DateRemoved).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_PhysicalLocationofDeviceImplant).Width = Convert.ToInt32(cWidth * 0.2)
                .Cols(COL_PatientID).Width = 0
                .Cols(COL_ExamID).Width = 0
                .Cols(COL_VisitID).Width = 0
                .Cols(COL_CardiologyDeviceID).Width = 0
                .Cols(COL_ClinicID).Width = 0

                'Dim SendingApplicationName As String = ""
                'SendingApplicationName = "Sending Application ID|Sending Application Universal ID|Sending Application Type"
                'Dim devicetype As String = ""
                'set col datatype
                .Cols(COL_DateofImplant).DataType = GetType(DateTime)
                .Cols(COL_TypeofDevice).DataType = GetType(String)
                '.Cols(COL_TypeofDevice).ComboList = devicetype
                .Cols(COL_ProductName).DataType = GetType(String)
                .Cols(COL_DeviceManufacturer).DataType = GetType(String)
                .Cols(COL_ProductSpecifications).DataType = GetType(String)
                .Cols(COL_ProductSerialNumber).DataType = GetType(String)
                .Cols(COL_ManufacturerModelNumber).DataType = GetType(String)
                .Cols(COL_LeadsType).DataType = GetType(String)
                .Cols(COL_DateRemoved).DataType = GetType(DateTime)
                .Cols(COL_PhysicalLocationofDeviceImplant).DataType = GetType(String)
                .Cols(COL_PatientID).DataType = GetType(Int64)
                .Cols(COL_ExamID).DataType = GetType(Int64)
                .Cols(COL_VisitID).DataType = GetType(Int64)
                .Cols(COL_CardiologyDeviceID).DataType = GetType(Int64)
                .Cols(COL_ClinicID).DataType = GetType(Int32)

                'set Heading
                .SetData(0, COL_DateofImplant, "Date of Implant")
                .SetData(0, COL_TypeofDevice, "Type of Device")
                .SetData(0, COL_ProductName, "Product Name ")
                .SetData(0, COL_DeviceManufacturer, "Device Manufacturer")
                .SetData(0, COL_ProductSpecifications, "Product Specifications")
                .SetData(0, COL_ProductSerialNumber, "Product Serial Number")
                .SetData(0, COL_ManufacturerModelNumber, "Manufacturer Model Number")
                .SetData(0, COL_LeadsType, "Leads Type")
                .SetData(0, COL_DateRemoved, "Date Removed")
                .SetData(0, COL_PhysicalLocationofDeviceImplant, "Physical Location of Device Implant")
                .SetData(0, COL_PatientID, "PatientID")
                .SetData(0, COL_ExamID, "ExamID")
                .SetData(0, COL_VisitID, "VisitID")
                .SetData(0, COL_ExamID, "CardiologyDeviceID")
                .SetData(0, COL_VisitID, "ClinicID")

                '.Cols(Col_SendingApplication).DataType = GetType(String)
                '.Cols(Col_SendingApplication).ComboList = SendingApplicationName
                '.Cols(Col_SendingApplication).AllowEditing = True
                'set Heading
                '.SetData(0, COL_HIDDENAPPLICATIONNAME, "System ApplicationName")
                '.SetData(0, COL_ApplName, "Sending Application Name")
                '.SetData(0, COL_FACILITYNAME, "Sending Facility Name")
            End With
            'C1Client.EndInit()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'C1Client.EndInit()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillC1Grid(ByVal dtcardio As DataTable)
        Dim dtDevicetype As DataTable = Nothing
        Dim dtProductname As DataTable = Nothing
        Dim dtDeviceManf As DataTable = Nothing
        Dim dtLeadtype As DataTable = Nothing
        Try

            'Dim r As C1.Win.C1FlexGrid.Row
            'r = C1Cardiology.Rows.Add()
            'C1Cardiology.SetData(r.Index, COL_PatientID, m_patientID)
            'C1Cardiology.SetData(r.Index, COL_ExamID, m_ExamID)
            'C1Cardiology.SetData(r.Index, COL_VisitID, m_VisitID)
            ' C1Cardiology.SetData(r.Index, COL_CardiologyDeviceID, 0)

            Dim i As Int16
            C1Cardiology.Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (C1Cardiology.Width - 20) / 11
            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

            C1Cardiology.Cols.Count = Col_Count
            C1Cardiology.Rows.Count = 1
            C1Cardiology.AllowEditing = True
            'C1Cardiology.AllowAddNew = True

            C1Cardiology.Styles.ClearUnused()


            '        dtDevicetype = New DataTable()
            dtDevicetype = GetDeviceTypeData()

            Dim strComboString As String = " "
            If (IsNothing(dtDevicetype) = False) Then
                For icnt As Int32 = 0 To dtDevicetype.Rows.Count - 1
                    strComboString = strComboString & "|" & dtDevicetype.Rows(icnt)(0).ToString
                Next

            End If

            'Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)

            Try
                If (C1Cardiology.Styles.Contains("Devicetype")) Then
                    cStyleDeviceType = C1Cardiology.Styles("Devicetype")
                Else
                    cStyleDeviceType = C1Cardiology.Styles.Add("Devicetype")
                End If
            Catch ex As Exception
                cStyleDeviceType = C1Cardiology.Styles.Add("Devicetype")
            End Try

            cStyleDeviceType.ComboList = strComboString
            strComboString = ""
            'rgOperator.Style = cStyleDeviceType


            '       dtProductname = New DataTable()
            dtProductname = GetProductData()
            strComboString = ""
            If (IsNothing(dtProductname) = False) Then


                For i = 0 To dtProductname.Rows.Count - 1
                    strComboString = strComboString & "|" & dtProductname.Rows(i)(0).ToString
                Next
            End If
            'Dim rgOperator1 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_ProductName, r.Index, COL_ProductName)

            Try
                If (C1Cardiology.Styles.Contains("ProductionName")) Then
                    cStyleProductName = C1Cardiology.Styles("ProductionName")
                Else
                    cStyleProductName = C1Cardiology.Styles.Add("ProductionName")
                End If
            Catch ex As Exception
                cStyleProductName = C1Cardiology.Styles.Add("ProductionName")
            End Try

            cStyleProductName.ComboList = strComboString
            strComboString = ""
            'rgOperator1.Style = cStyleProductName


            '      dtDeviceManf = New DataTable()
            dtDeviceManf = GetDevicemanfData()
            strComboString = ""
            If (IsNothing(dtDeviceManf) = False) Then


                For i = 0 To dtDeviceManf.Rows.Count - 1
                    strComboString = strComboString & "|" & dtDeviceManf.Rows(i)(0).ToString
                Next
            End If
            'Dim rgOperator2 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_DeviceManufacturer, r.Index, COL_DeviceManufacturer)
            Try
                If (C1Cardiology.Styles.Contains("DeviceManf")) Then
                    cStyleDeviceManf = C1Cardiology.Styles("DeviceManf")
                Else
                    cStyleDeviceManf = C1Cardiology.Styles.Add("DeviceManf")
                End If
            Catch ex As Exception
                cStyleDeviceManf = C1Cardiology.Styles.Add("DeviceManf")
            End Try

            cStyleDeviceManf.ComboList = strComboString
            strComboString = ""
            'rgOperator2.Style = cStyleDeviceManf


            '     dtLeadtype = New DataTable()
            dtLeadtype = GetLeadData()
            strComboString = ""
            If (IsNothing(dtLeadtype) = False) Then


                For i = 0 To dtLeadtype.Rows.Count - 1
                    strComboString = strComboString & "|" & dtLeadtype.Rows(i)(0).ToString
                Next
            End If
            'Dim rgOperator3 As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_LeadsType, r.Index, COL_LeadsType)
            Try
                If (C1Cardiology.Styles.Contains("Leadtype")) Then
                    cStyleLeadtype = C1Cardiology.Styles("Leadtype")
                Else
                    cStyleLeadtype = C1Cardiology.Styles.Add("Leadtype")
                End If
            Catch ex As Exception
                cStyleLeadtype = C1Cardiology.Styles.Add("Leadtype")
            End Try

            cStyleLeadtype.ComboList = strComboString
            strComboString = ""
            ' rgOperator3.Style = cStyleLeadtype
            C1Cardiology.SetCellStyle(0, COL_TypeofDevice, cStyleDeviceType)

            C1Cardiology.SetCellStyle(0, COL_ProductName, cStyleProductName)

            C1Cardiology.SetCellStyle(0, COL_DeviceManufacturer, cStyleDeviceManf)

            C1Cardiology.SetCellStyle(0, COL_LeadsType, cStyleLeadtype)
            If IsNothing(dtcardio) Then
                C1Cardiology.SetData(0, COL_PatientID, dtcardio.Rows(i)("PatientID"))
                C1Cardiology.SetData(0, COL_ExamID, dtcardio.Rows(i)("ExamID"))
                C1Cardiology.SetData(0, COL_VisitID, dtcardio.Rows(i)("VisitID"))
                C1Cardiology.SetData(0, COL_ClinicID, dtcardio.Rows(i)("ClinicID"))
            End If
            ''dt fill
            If Not IsNothing(dtcardio) Then
                If Not IsDBNull(dtcardio) Then
                    If dtcardio.Rows.Count > 0 Then

                        With C1Cardiology
                            For i = 0 To dtcardio.Rows.Count - 1
                                .Rows.Add()

                                ''''Set Column Style 
                                '''' Assinge the Cell for ComboBox
                                'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
                                'rgDia.Style = csDia  '' .Styles.Add("Dia")
                                '''' Assinge the Cell for ComboBox
                                'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
                                'rgStatus.Style = csStatus ''''  .Styles.Add("Status")
                                '' Fill the Retrived information to relative controls
                                'dtpDOS.Value = Format(dt.Rows(i)("dtDOS"), "MM/dd/yyyy")

                                .SetCellStyle(i + 1, COL_TypeofDevice, cStyleDeviceType)

                                .SetCellStyle(i + 1, COL_ProductName, cStyleProductName)

                                .SetCellStyle(i + 1, COL_DeviceManufacturer, cStyleDeviceManf)

                                .SetCellStyle(i + 1, COL_LeadsType, cStyleLeadtype)

                                .SetData(i + 1, COL_DateofImplant, dtcardio.Rows(i)("DateofImplant"))
                                .SetData(i + 1, COL_ProductSpecifications, dtcardio.Rows(i)("ProductSpecification"))
                                .SetData(i + 1, COL_ProductSerialNumber, dtcardio.Rows(i)("ProductSerialNo"))
                                .SetData(i + 1, COL_ManufacturerModelNumber, dtcardio.Rows(i)("sManufacturerModelNo"))
                                '.SetData(i + 1, COL_DateRemoved, Format(dtcardio.Rows(i)("DateRemoved"), "MM/dd/yyyy"))

                                'sarika Bug 1364 20090515
                                'If Not (dtcardio.Rows(i)("DateRemoved").ToString.Trim() = "") Then
                                '    .SetData(i + 1, COL_DateRemoved, dtcardio.Rows(i)("DateRemoved"))
                                'End If

                                If Not IsDBNull(dtcardio.Rows(i)("DateRemoved")) Then
                                    .SetData(i + 1, COL_DateRemoved, dtcardio.Rows(i)("DateRemoved"))
                                End If
                                '----



                                .SetData(i + 1, COL_PhysicalLocationofDeviceImplant, dtcardio.Rows(i)("PhysicalLocation"))
                                .SetData(i + 1, COL_PatientID, dtcardio.Rows(i)("PatientID"))
                                .SetData(i + 1, COL_ExamID, dtcardio.Rows(i)("ExamID"))
                                .SetData(i + 1, COL_VisitID, dtcardio.Rows(i)("VisitID"))
                                .SetData(i + 1, COL_ClinicID, dtcardio.Rows(i)("ClinicID"))
                                .SetData(i + 1, COL_CardiologyDeviceID, dtcardio.Rows(i)("CardiologyDeviceID"))
                                .SetData(i + 1, COL_TypeofDevice, dtcardio.Rows(i)("DeviceType"))

                                .SetData(i + 1, COL_ProductName, dtcardio.Rows(i)("ProductName"))

                                .SetData(i + 1, COL_DeviceManufacturer, dtcardio.Rows(i)("DeviceManufacturer"))

                                .SetData(i + 1, COL_LeadsType, dtcardio.Rows(i)("LeadType"))



                            Next
                        End With

                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(dtDevicetype) = False) Then
                dtDevicetype.Dispose()
                dtDevicetype = Nothing

            End If
            If (IsNothing(dtProductname) = False) Then
                dtProductname.Dispose()
                dtProductname = Nothing

            End If
            If (IsNothing(dtDeviceManf) = False) Then
                dtDeviceManf.Dispose()
                dtDeviceManf = Nothing

            End If
            If (IsNothing(dtLeadtype) = False) Then
                dtLeadtype.Dispose()
                dtLeadtype = Nothing

            End If



        End Try
    End Sub

    Private Function PopulateCardioDeviceData() As DataTable

        'Declaration of variables for making connection
        Dim dt As New DataTable
        Dim cmd As SqlCommand
        Dim sqladpt As SqlDataAdapter

        'Connection string
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)



        m_VisitID = GenerateVisitID(Now.Date, m_patientID)
        Dim strquery As String = "select isnull(nCardiologyDeviceID,0)as CardiologyDeviceID,isnull(nPatientID,0) as PatientID, isnull(nExamID,0) as ExamID, isnull(nVisitID,0)as VisitID , isnull(nClinicID,0) as ClinicID, isnull(dtDateofImplant,0) as DateofImplant, isnull(sDeviceType,'') as DeviceType, isnull(sProductName,'')as ProductName, isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification, isnull(sProductSerialNo,'') as ProductSerialNo ,isnull(sManufacturerModelNo,'') as sManufacturerModelNo, isnull(sLeadType,'') as LeadType, dtDateRemoved as DateRemoved, isnull(sPhysicalLocation,'') as PhysicalLocation,isnull(sCPTCode,'') as CPTCode,isnull(sProcedures,'') as Procedures,isnull(sLeadLocation,'') as LeadLocation,isnull(sThresholdAtrial,'') as ThresholdAtrial,isnull(sThresholdVentricular,'') as ThresholdVentricular,isnull(sSensingAtrial,'') as SensingAtrial,isnull(sSensingVentricular,'') as SensingVentricular,isnull(sImpedenceAtrial,'') as ImpedenceAtrial,isnull(sImpedenceVentricular,'') as ImpedenceVentricular from dbo.CV_CardiologyDevice where nPatientID = " & m_patientID & " And nVisitID = " & m_VisitID & ""

        cmd = New SqlCommand(strquery, conn)
        sqladpt = New SqlDataAdapter(cmd)

        'Fill data adapter
        sqladpt.Fill(dt)
        sqladpt.Dispose()
        sqladpt = Nothing
        cmd.Parameters.Clear()
        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
        'Return Data table
        Return dt
    End Function

    Private Function GetDeviceTypeData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Device Type'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds_cardio As New DataSet
            ds_cardio.Clear()
            adp.Fill(ds_cardio)
            dt_cardio = ds_cardio.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            ds_cardio.Dispose()
            ds_cardio = Nothing
            Return dt_cardio
            'setDataGridStyle(dt)
            'DataGrid.DataSource = ds.Tables(0)
            'DataGrid.Show()
            
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing

        End Try
    End Function

    Private Function GetProductData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Product Name'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds_cardio As New DataSet
            ds_cardio.Clear()
            adp.Fill(ds_cardio)
            dt_cardio = ds_cardio.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            ds_cardio.Dispose()
            ds_cardio = Nothing
            Return dt_cardio


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Connection.Close()
            Connection.Dispose()
            Connection = Nothing
        End Try
    End Function

    Private Function GetDevicemanfData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Device Manf.'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds_cardio As New DataSet
            ds_cardio.Clear()
            adp.Fill(ds_cardio)
            dt_cardio = ds_cardio.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            ds_cardio.Dispose()
            ds_cardio = Nothing
            Return dt_cardio
            'setDataGridStyle(dt)
            'DataGrid.DataSource = ds.Tables(0)
            'DataGrid.Show()
            'Connection.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If
            Connection.Dispose()
            Connection = Nothing
        End Try
    End Function

    Private Function GetLeadData() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Try
            Connection.Open()
            Dim CommandString As String = "select sDescription from category_mst where sCategoryType='Cardio Lead Type'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            Dim ds_cardio As New DataSet
            ds_cardio.Clear()
            adp.Fill(ds_cardio)
            dt_cardio = ds_cardio.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            ds_cardio.Dispose()
            ds_cardio = Nothing
            Return dt_cardio
            'setDataGridStyle(dt)
            'DataGrid.DataSource = ds.Tables(0)
            'DataGrid.Show()
            ' Connection.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If
            Connection.Dispose()
            Connection = Nothing
        End Try
    End Function

    Private Sub C1Cardiology_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1Cardiology.DoubleClick
        Try
            Dim rowCnt = C1Cardiology.RowSel
            If C1Cardiology.Rows.Count > 1 Then

                mPatientID = CType(C1Cardiology.GetData(rowCnt, COL_PatientID), Long)
                If mPatientID <> 0 Then
                    'mStressID = C1Cardiology.GetData(rowCnt, COL_StressID)
                    mExamID = CType(C1Cardiology.GetData(rowCnt, COL_ExamID), Long)
                    mVisitID = CType(C1Cardiology.GetData(rowCnt, COL_VisitID), Long)
                    mDateofStudy = C1Cardiology.GetData(rowCnt, COL_DateofStudyInvisible)
                    '' COL_DateofStudyInvisible
                    'mClinicID = C1Cardiology.GetData(rowCnt, COL_ClinicID_ST)
                Else
                    mPatientID = pID
                    mExamID = CType(C1Cardiology.GetData(rowCnt, COL_ExamID), Long)
                    mVisitID = CType(C1Cardiology.GetData(rowCnt, COL_VisitID), Long)
                End If

                Dim ofrmImplant As New frmCV_ImplantDevice(mPatientID, mVisitID, mDateofStudy)
                ofrmImplant.ShowDialog(ofrmImplant.Parent)
                SetGridStyle()
                FillImplantDevice()
                ofrmImplant.Dispose()
                ofrmImplant = Nothing
                ''PopulateImplant()
                '' '' '' Dim ofrm As New frmCardiologyDevice(mPatientID, mExamID, mVisitID)
                '' ''Dim ofrm As New frmCV_VWImplantDevice(mPatientID)
                '' ''ofrm.ShowDialog()
                '' ''If Not IsNothing(ofrm) Then
                '' ''    ofrm.Dispose()
                '' ''    ofrm = Nothing
                '' ''End If
                '' ''FillCardiologyDevice(pID)
                '' ''PopulateImplant()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
#End Region


#Region "Ejection Fraction"

    Dim _EjectionFractionID As Long = 0
    Dim _PatientID As Long = 0
    Dim _ExamID As Long = 0
    Dim _VisitID As Long = 0
    'Dim _ClinicID As Long = 0

    Dim cStyleModality As C1.Win.C1FlexGrid.CellStyle

    Dim cl As clsEjectionFraction


    '''' Column No
    Dim COL_EJECTION_FRACTIONID As Integer = 0
    Dim COL_PATIENTID_EF As Integer = 1
    Dim COL_EXAMID_EF As Integer = 2
    Dim COL_VISITID_EF As Integer = 3
    Dim COL_CLINICID_EF As Integer = 4
    Dim COL_TEST_DATE As Integer = 5
    Dim COL_MODALITY_TEST As Integer = 6
    Dim COL_QUANTITY_PERCENT As Integer = 7
    Dim COL_QUANTITY As Integer = 8
    Dim COLUMN_COUNT_EF As Int16 = 9

    Public Sub FillEjectionFraction(ByVal PatientID As Long)

        gloC1FlexStyle.Style(cfgEjectionFraction) ''Ojeswini_01302010

        ''dhruv 20091130
        ''Avoid the flickering
        cfgEjectionFraction.Redraw = False
        _PatientID = PatientID

        If IsNothing(dt_EF) Then
            dt_EF = PopulateEjectionFractionList()
        End If
        If Not IsNothing(dt_EF) Then
            If dt_EF.Rows.Count > 0 Then
                SetGridStyle(dt_EF)
                oSearchEjectionFraction.IntialiseDatatable(dt_EF)
            Else 'if row count is 0 then just design the grid with header row
                DesignEjectionFractionGrid()
            End If
        Else 'if there is nothing in datatable then just design the grid with header row
            DesignEjectionFractionGrid()
        End If
        cfgEjectionFraction.Redraw = True

    End Sub


    Private Sub DesignEjectionFractionGrid()

        gloC1FlexStyle.Style(cfgEjectionFraction) ''Ojeswini_01302010

        Try

            With cfgEjectionFraction
                'Dim i As Int16
                .Dock = DockStyle.Fill
                Dim _TotalWidth As Single = 0
                _TotalWidth = (.Width - 20) / 11
                'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

                .Cols.Count = COLUMN_COUNT_EF
                .Rows.Count = 1
                .Rows.Fixed = 1
                .AllowEditing = False 'True
                '.AllowAddNew = True

                .Styles.ClearUnused()
                cfgEjectionFraction.ShowCellLabels = True

                .Cols(COL_EJECTION_FRACTIONID).Width = .Width * 0
                .Cols(COL_EJECTION_FRACTIONID).AllowEditing = False
                .SetData(0, COL_EJECTION_FRACTIONID, "EjectionFractionID")
                .Cols(COL_EJECTION_FRACTIONID).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_PATIENTID_EF).Width = .Width * 0
                .Cols(COL_PATIENTID_EF).AllowEditing = False
                .SetData(0, COL_PATIENTID_EF, "PatientID")
                .Cols(COL_PATIENTID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_EXAMID_EF).Width = .Width * 0
                .Cols(COL_EXAMID_EF).AllowEditing = False
                .SetData(0, COL_EXAMID_EF, "ExamID")
                .Cols(COL_EXAMID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_VISITID_EF).Width = .Width * 0
                .Cols(COL_VISITID_EF).AllowEditing = False
                .SetData(0, COL_VISITID_EF, "VisitID")
                .Cols(COL_VISITID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_CLINICID_EF).Width = .Width * 0
                .Cols(COL_CLINICID_EF).AllowEditing = False
                .SetData(0, COL_CLINICID_EF, "ClinicID")
                .Cols(COL_CLINICID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_TEST_DATE).Width = _TotalWidth * 1.2
                .SetData(0, COL_TEST_DATE, "Date")
                .Cols(COL_TEST_DATE).DataType = GetType(Date)
                .Cols(COL_TEST_DATE).AllowEditing = False 'True

                .Cols(COL_MODALITY_TEST).Width = _TotalWidth * 2.7
                .SetData(0, COL_MODALITY_TEST, "Modality Test")
                .Cols(COL_MODALITY_TEST).AllowEditing = False 'True

                'Dim dtResults As DataTable
                'dtResults = New DataTable()
                'dtResults = GetTests()

                'Dim strComboString As String = " "
                'For icnt As Int32 = 0 To dtResults.Rows.Count - 1
                '    strComboString = strComboString & "|" & dtResults.Rows(icnt)(0).ToString
                'Next

                'Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)
                Try
                    If (.Styles.Contains("ModalityTest")) Then
                        cStyleModality = .Styles("ModalityTest")
                    Else
                        cStyleModality = .Styles.Add("ModalityTest")
                    End If
                Catch ex As Exception
                    cStyleModality = .Styles.Add("ModalityTest")
                End Try

                'cStyleModality.ComboList = strComboString
                'strComboString = ""

                .Cols(COL_QUANTITY_PERCENT).Width = _TotalWidth * 2.7
                .SetData(0, COL_QUANTITY_PERCENT, "Quantitative value as percent")
                .Cols(COL_QUANTITY_PERCENT).AllowEditing = False 'True
                .Cols(COL_QUANTITY_PERCENT).TextAlignFixed = TextAlignEnum.CenterCenter

                .Cols(COL_QUANTITY).Width = _TotalWidth * 2.7
                .SetData(0, COL_QUANTITY, "Qualitative value (narrative)")
                .Cols(COL_QUANTITY).AllowEditing = False 'True
                .Cols(COL_QUANTITY).TextAlignFixed = TextAlignEnum.CenterCenter

                '' Table dtEF Contains following Columns
                '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status
                'If Not IsNothing(dtEF) Then
                '    If dtEF.Rows.Count > 0 Then
                '        For i = 0 To dtEF.Rows.Count - 1
                '            .Rows.Add()

                '            ''''Set Column Style 
                '            '''' Assinge the Cell for ComboBox
                '            'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
                '            'rgDia.Style = csDia  '' .Styles.Add("Dia")

                '            '''' Assinge the Cell for ComboBox
                '            'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
                '            'rgStatus.Style = csStatus ''''  .Styles.Add("Status")

                '            '' Fill the Retrived information to relative controls
                '            'dtpDOS.Value = Format(dtEF.Rows(i)("dtDOS"), "MM/dd/yyyy")

                '            .SetData(i + 1, COL_EJECTION_FRACTIONID, dtEF.Rows(i)("EjectionFractionID"))
                '            .SetData(i + 1, COL_PATIENTID_EF, dtEF.Rows(i)("PatientID"))
                '            .SetData(i + 1, COL_EXAMID_EF, dtEF.Rows(i)("ExamID"))
                '            .SetData(i + 1, COL_VISITID_EF, dtEF.Rows(i)("VisitID"))
                '            .SetData(i + 1, COL_CLINICID_EF, dtEF.Rows(i)("ClinicID"))

                '            .SetData(i + 1, COL_TEST_DATE, Format(dtEF.Rows(i)("TestDate"), "MM/dd/yyyy"))
                '            .SetCellStyle(i + 1, COL_MODALITY_TEST, cStyleModality)
                '            .SetData(i + 1, COL_MODALITY_TEST, dtEF.Rows(i)("ModalityTest"))
                '            .SetData(i + 1, COL_QUANTITY_PERCENT, dtEF.Rows(i)("QuantityPercent"))
                '            .SetData(i + 1, COL_QUANTITY, dtEF.Rows(i)("QuantityDescription"))

                '        Next
                '    End If
                'End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub SetGridStyle(ByVal dtEF As DataTable)



        With cfgEjectionFraction

            Dim i As Int16
            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (.Width - 20) / 11
            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

            .Cols.Count = COLUMN_COUNT_EF
            .Rows.Count = 1
            .Rows.Fixed = 1
            .AllowEditing = False 'True
            '.AllowAddNew = True

            .Styles.ClearUnused()
            cfgEjectionFraction.ShowCellLabels = True

            .Cols(COL_EJECTION_FRACTIONID).Width = .Width * 0
            .Cols(COL_EJECTION_FRACTIONID).AllowEditing = False
            .SetData(0, COL_EJECTION_FRACTIONID, "EjectionFractionID")
            .Cols(COL_EJECTION_FRACTIONID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_PATIENTID_EF).Width = .Width * 0
            .Cols(COL_PATIENTID_EF).AllowEditing = False
            .SetData(0, COL_PATIENTID_EF, "PatientID")
            .Cols(COL_PATIENTID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_EXAMID_EF).Width = .Width * 0
            .Cols(COL_EXAMID_EF).AllowEditing = False
            .SetData(0, COL_EXAMID_EF, "ExamID")
            .Cols(COL_EXAMID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_VISITID_EF).Width = .Width * 0
            .Cols(COL_VISITID_EF).AllowEditing = False
            .SetData(0, COL_VISITID_EF, "VisitID")
            .Cols(COL_VISITID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_CLINICID_EF).Width = .Width * 0
            .Cols(COL_CLINICID_EF).AllowEditing = False
            .SetData(0, COL_CLINICID_EF, "ClinicID")
            .Cols(COL_CLINICID_EF).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_TEST_DATE).Width = _TotalWidth * 1.2
            .SetData(0, COL_TEST_DATE, "Date")
            .Cols(COL_TEST_DATE).DataType = GetType(Date)
            .Cols(COL_TEST_DATE).AllowEditing = False 'True

            .Cols(COL_MODALITY_TEST).Width = _TotalWidth * 2.7
            .SetData(0, COL_MODALITY_TEST, "Modality Test")
            .Cols(COL_MODALITY_TEST).AllowEditing = False 'True

            Dim dtResults As DataTable = Nothing
            '   dtResults = New DataTable()
            dtResults = GetTests()

            Dim strComboString As String = " "
            If (IsNothing(dtResults) = False) Then


                For icnt As Int32 = 0 To dtResults.Rows.Count - 1
                    strComboString = strComboString & "|" & dtResults.Rows(icnt)(0).ToString
                Next
            End If

            'Dim rgOperator As C1.Win.C1FlexGrid.CellRange = C1Cardiology.GetCellRange(r.Index, COL_TypeofDevice, r.Index, COL_TypeofDevice)

            Try
                If (.Styles.Contains("ModalityTest")) Then
                    cStyleModality = .Styles("ModalityTest")
                Else
                    cStyleModality = .Styles.Add("ModalityTest")
                End If
            Catch ex As Exception
                cStyleModality = .Styles.Add("ModalityTest")
            End Try

            cStyleModality.ComboList = strComboString
            strComboString = ""

            .Cols(COL_QUANTITY_PERCENT).Width = _TotalWidth * 2.7
            .SetData(0, COL_QUANTITY_PERCENT, "Quantitative value as percent")
            .Cols(COL_QUANTITY_PERCENT).AllowEditing = False 'True
            .Cols(COL_QUANTITY_PERCENT).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_QUANTITY).Width = _TotalWidth * 2.7
            .SetData(0, COL_QUANTITY, "Qualitative value (narrative)")
            .Cols(COL_QUANTITY).AllowEditing = False 'True
            .Cols(COL_QUANTITY).TextAlignFixed = TextAlignEnum.CenterCenter

            '' Table dtEF Contains following Columns
            '' ProblemID, VisitID , dtDOS, Diagnosis, Complaint ,Status
            If Not IsNothing(dtEF) Then
                If dtEF.Rows.Count > 0 Then
                    For i = 0 To dtEF.Rows.Count - 1
                        .Rows.Add()

                        ''''Set Column Style 
                        '''' Assinge the Cell for ComboBox
                        'Dim rgDia As CellRange = .GetCellRange(.Rows.Count - 1, COL_DIAGNOSIS)
                        'rgDia.Style = csDia  '' .Styles.Add("Dia")

                        '''' Assinge the Cell for ComboBox
                        'Dim rgStatus As CellRange = .GetCellRange(.Rows.Count - 1, COL_STATUS)
                        'rgStatus.Style = csStatus ''''  .Styles.Add("Status")

                        '' Fill the Retrived information to relative controls
                        'dtpDOS.Value = Format(dtEF.Rows(i)("dtDOS"), "MM/dd/yyyy")

                        .SetData(i + 1, COL_EJECTION_FRACTIONID, dtEF.Rows(i)("EjectionFractionID"))
                        .SetData(i + 1, COL_PATIENTID_EF, dtEF.Rows(i)("PatientID"))
                        .SetData(i + 1, COL_EXAMID_EF, dtEF.Rows(i)("ExamID"))
                        .SetData(i + 1, COL_VISITID_EF, dtEF.Rows(i)("VisitID"))
                        .SetData(i + 1, COL_CLINICID_EF, dtEF.Rows(i)("ClinicID"))

                        .SetData(i + 1, COL_TEST_DATE, Format(dtEF.Rows(i)("TestDate"), "MM/dd/yyyy"))
                        .SetCellStyle(i + 1, COL_MODALITY_TEST, cStyleModality)
                        .SetData(i + 1, COL_MODALITY_TEST, dtEF.Rows(i)("ModalityTest"))
                        .SetData(i + 1, COL_QUANTITY_PERCENT, dtEF.Rows(i)("QuantityPercent"))
                        .SetData(i + 1, COL_QUANTITY, dtEF.Rows(i)("QuantityDescription"))

                    Next
                End If
                If (IsNothing(dtResults) = False) Then
                    dtResults.Dispose()
                    dtResults = Nothing
                End If

            End If

        End With
    End Sub
    Private Function PopulateEjectionFractionList() As DataTable

        'Declaration of variables for making connection
        Dim dt As New DataTable
        Dim cmd As SqlCommand
        Dim sqladpt As SqlDataAdapter

        'Connection string
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        '        cmd = New SqlCommand("Select * from CV_EjectionFraction where nPatientID=" & _PatientID & " and nExamID=" & _ExamID & " and nVisitID=" & _VisitID, conn)
        Dim strquery As String = "Select isnull(nEjectionFractionID,0)as EjectionFractionID,isnull(nPatientID,0)as PatientID,isnull(nExamID,0)as ExamID,isnull(nVisitID,0)as VisitID,isnull(nClinicID,0)as ClinicID,isnull(dtDateofTest,0)as TestDate,isnull(sModalityTest,'')as ModalityTest,isnull(sQuantityPercent,0)as QuantityPercent,isnull(sQuantityDesc,0)as QuantityDescription from CV_EjectionFraction  where nPatientID=" & _PatientID '& " and nVisitID=" & _VisitID
        cmd = New SqlCommand(strquery, conn)
        sqladpt = New SqlDataAdapter(cmd)

        'Fill data adapter
        sqladpt.Fill(dt)
        sqladpt.Dispose()
        sqladpt = Nothing
        cmd.Parameters.Clear()
        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
        'Return Data table
        Return dt
    End Function

    Private Function GetTests() As DataTable
        Dim Connection As New SqlConnection(GetConnectionString)
        Dim ds As New DataSet
        Dim dt As DataTable
        Try
            Connection.Open()
            Dim CommandString As String = "select Distinct sDescription from Category_MST where sCategoryType = 'Cardio Modality Test'"

            Dim adp As New SqlDataAdapter(CommandString, Connection)
            ds.Clear()
            adp.Fill(ds)
            dt = ds.Tables(0).Copy()
            adp.Dispose()
            adp = Nothing
            ds.Dispose()
            ds = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Connection.State = ConnectionState.Open Then
                Connection.Close()
            End If
            Connection.Dispose()
            Connection = Nothing
        End Try
    End Function

    Private Sub cfgEjectionFraction_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cfgEjectionFraction.DoubleClick
        Try
            If cfgEjectionFraction.Rows.Count > 1 Then
                _PatientID = cfgEjectionFraction.GetData(cfgEjectionFraction.Row, COL_PATIENTID_EF)

                If _PatientID <> 0 Then
                    _ExamID = cfgEjectionFraction.GetData(cfgEjectionFraction.Row, COL_EXAMID_EF)
                    _VisitID = cfgEjectionFraction.GetData(cfgEjectionFraction.Row, COL_VISITID_EF)

                Else
                    _PatientID = pID
                    _ExamID = cfgEjectionFraction.GetData(cfgEjectionFraction.Row, COL_EXAMID_EF)
                    _VisitID = cfgEjectionFraction.GetData(cfgEjectionFraction.Row, COL_VISITID_EF)
                End If



                Dim ofrm As New EjectionFraction(_PatientID, _ExamID, _VisitID)
                ofrm.ShowInTaskbar = False
                ofrm.ShowDialog(ofrm.Parent)
                If Not IsNothing(ofrm) Then
                    ofrm.Dispose()
                    ofrm = Nothing
                End If
                dt_EF = Nothing ''For fetching latest data from database.
                FillEjectionFraction(pID)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    'Private Sub txtEjectionSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strSearch As String
    '        With txtSearch
    '            If Trim(.Text) <> "" Then
    '                strSearch = Replace(.Text, "'", "''")
    '            Else
    '                strSearch = ""
    '            End If
    '        End With
    '        Dim strNode As String = ""
    '        With cfgEjectionFraction
    '            For i As Int16 = 1 To .Cols.Count - 1
    '                If cfgEjectionFraction.Cols.Item(i).Width > 0 Then
    '                    strNode = ""
    '                    strNode = UCase(.GetData(i, .Cols.Item(i).Name).ToString.Trim)
    '                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
    '                        .Row = i
    '                        Exit Sub
    '                    End If
    '                End If

    '            Next
    '        End With
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Sub

#End Region

#Region "Imaging ST"

    Dim mPatientID As Long = 0
    Dim mStressID As Long = 0
    Dim mVisitID As Long = 0
    Dim mExamID As Long = 0
    Dim mClinicID As Long = 0
    Dim mDateofStudy As Date

    Dim COL_StressID As Integer = 0
    Dim COL_PatientID_ST As Integer = 1
    Dim COL_ExamID_ST As Integer = 2
    Dim COL_VisitID_ST As Integer = 3
    Dim COL_ClinicID_ST As Integer = 4
    Dim COL_DateofStudy As Integer = 5
    Dim COL_TestType = 6
    Dim COL_Result As Integer = 7
    Dim COL_DateofStudyInvisible As Integer = 8

    Dim COL_PHYSICIAN As Integer = 9
    Dim COL_RestingHeartRate As Integer = 10
    Dim COL_PeakHeartRate As Integer = 11
    Dim COL_NarrativeSummary As Integer = 12
    Dim COL_TotalExerciseTime As Integer = 13
    Dim COL_EjectionFraction As Integer = 14
    Dim COL_PARENT As Integer = 15
    Dim COL_IDENTITY As Integer = 16


    Dim COLUMN_COUNT As Integer = 17

    Private Sub FillImplantTab(ByVal PatientID As Long)
        '''''''' By Ujwala - for Implant Device - as on 20101020
        C1Cardiology.Redraw = False
        mPatientID = PatientID
        If IsNothing(dtDateofImplantDtls) Then
            ''SetImplantGridStyle()
            FillImplantDevice()

        End If
        C1Cardiology.Redraw = True
        '''''''' By Ujwala - for Implant Device - as on 20101020
    End Sub

    Private Sub SetImplantGridStyle()
        '''''''' By Ujwala - for Implant Device - as on 20101020
        'Dim struser As String
        With C1Cardiology
            ''Dim i As Int16
            ''.Dock = DockStyle.Fill

            .Cols.Count = COLUMN_COUNT1
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_CardiologyDeviceID).Width = .Width * 0
            .Cols(COL_CardiologyDeviceID).AllowEditing = False
            .SetData(0, COL_CardiologyDeviceID, "Cardiology Device ID")
            .Cols(COL_CardiologyDeviceID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .SetData(0, COL_PatientID, "Patient ID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ExamID).Width = .Width * 0
            .Cols(COL_ExamID).AllowEditing = False
            .SetData(0, COL_VisitID, "Exam ID")
            .Cols(COL_ExamID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .SetData(0, COL_VisitID, "Visit ID")
            .Cols(COL_VisitID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ClinicID).Width = .Width * 0
            .Cols(COL_ClinicID).AllowEditing = False
            .SetData(0, COL_VisitID, "Clinic ID")
            .Cols(COL_ClinicID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofImplant).Width = .Width * 1.3
            .Cols(COL_DateofImplant).AllowEditing = False
            .SetData(0, COL_DateofImplant, "Date of Implant")
            .Cols(COL_DateofImplant).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COl_CPTCode).Width = .Width * 0
            .Cols(COl_CPTCode).AllowEditing = False
            .SetData(0, COl_CPTCode, "CPT Code")
            .Cols(COl_CPTCode).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TestTypes).Width = .Width * 0
            .Cols(COL_TestTypes).AllowEditing = False
            .SetData(0, COL_TestTypes, "Test Type")
            .Cols(COL_TestTypes).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TypeofDevice).Width = .Width * 0
            .Cols(COL_TypeofDevice).AllowEditing = False
            .SetData(0, COL_TypeofDevice, "Device Type ")
            .Cols(COL_TypeofDevice).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ProductName).Width = .Width * 0
            .Cols(COL_ProductName).AllowEditing = False
            .SetData(0, COL_ProductName, "Product Name")
            .Cols(COL_ProductName).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DeviceManufacturer).Width = .Width * 0
            .Cols(COL_DeviceManufacturer).AllowEditing = False
            .SetData(0, COL_DeviceManufacturer, "Device Manufacturer")
            .Cols(COL_DeviceManufacturer).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ProductSpecifications).Width = .Width * 0
            .Cols(COL_ProductSpecifications).AllowEditing = False
            .SetData(0, COL_ProductSpecifications, "Product Specification")
            .Cols(COL_ProductSpecifications).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ProductSerialNumber).Width = .Width * 0
            .Cols(COL_ProductSerialNumber).AllowEditing = False
            .SetData(0, COL_ProductSerialNumber, "Product Serial No")
            .Cols(COL_ProductSerialNumber).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ManufacturerModelNumber).Width = .Width * 0
            .Cols(COL_ManufacturerModelNumber).AllowEditing = False
            .SetData(0, COL_ManufacturerModelNumber, "Manufacturer Model No")
            .Cols(COL_ManufacturerModelNumber).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_LeadsType).Width = .Width * 0
            .Cols(COL_LeadsType).AllowEditing = False
            .SetData(0, COL_LeadsType, "Lead Type")
            .Cols(COL_LeadsType).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateRemoved).Width = 0
            .SetData(0, COL_DateRemoved, "Date Removed")
            .Cols(COL_DateRemoved).DataType = GetType(String)
            .Cols(COL_DateRemoved).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PhysicalLocationofDeviceImplant).Width = .Width * 0
            .Cols(COL_PhysicalLocationofDeviceImplant).AllowEditing = False
            .SetData(0, COL_PhysicalLocationofDeviceImplant, "Physical Location")
            .Cols(COL_PhysicalLocationofDeviceImplant).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_Procedures).Width = .Width * 0
            .Cols(COL_Procedures).AllowEditing = False
            .SetData(0, COL_Procedures, "Procedures")
            .Cols(COL_Procedures).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_LeadLocation).Width = .Width * 0
            .Cols(COL_LeadLocation).AllowEditing = False
            .SetData(0, COL_LeadLocation, "Lead Location")
            .Cols(COL_LeadLocation).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ThresholdAtrial).Width = .Width * 0
            .Cols(COL_ThresholdAtrial).AllowEditing = False
            .SetData(0, COL_ThresholdAtrial, "Threshold Atrial")
            .Cols(COL_ThresholdAtrial).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ThresholdVentricular).Width = .Width * 0
            .Cols(COL_ThresholdVentricular).AllowEditing = False
            .SetData(0, COL_ThresholdVentricular, "Threshold Ventricular")
            .Cols(COL_ThresholdVentricular).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_SensingAtrial).Width = .Width * 0
            .Cols(COL_SensingAtrial).AllowEditing = False
            .SetData(0, COL_SensingAtrial, "Sensing Atrial")
            .Cols(COL_SensingAtrial).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_SensingVentricular).Width = .Width * 0
            .Cols(COL_SensingVentricular).AllowEditing = False
            .SetData(0, COL_SensingVentricular, "Sensing Ventricular")
            .Cols(COL_SensingVentricular).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ImpedanceAtrial).Width = .Width * 0
            .Cols(COL_ImpedanceAtrial).AllowEditing = False
            .SetData(0, COL_ImpedanceAtrial, "Impedance Atrial")
            .Cols(COL_ImpedanceAtrial).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ImpedanceVentricular).Width = .Width * 0
            .Cols(COL_ImpedanceVentricular).AllowEditing = False
            .SetData(0, COL_ImpedanceVentricular, "Impedance Ventricular")
            .Cols(COL_ImpedanceVentricular).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofStudyInvisibles).Width = 0
            .SetData(0, COL_DateofStudyInvisibles, "Date of Study Invisible")
            .Cols(COL_DateofStudyInvisibles).DataType = GetType(String)
            .Cols(COL_DateofStudyInvisibles).TextAlignFixed = TextAlignEnum.LeftCenter

            .ExtendLastCol = True
        End With
        '''''''' By Ujwala - for Implant Device - as on 20101020
    End Sub

    '' ''Public Sub FillImplantDevice()
    '' ''    '''''''' By Ujwala - for Implant Device - as on 20101020
    '' ''    Try

    '' ''        Dim _Row As Integer
    '' ''        SetImplantGridStyle()
    '' ''        ''set properties of treeview in flexgrid
    '' ''        With C1Cardiology
    '' ''            .Tree.Column = COL_DateofImplant
    '' ''            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '' ''            .Tree.LineStyle = Drawing2D.DashStyle.Solid
    '' ''            .Tree.Indent = 15
    '' ''            .Cols(COL_DateofImplant).TextAlign = TextAlignEnum.LeftCenter
    '' ''        End With

    '' ''        Dim dtDateofImplant As DataTable
    '' ''        Dim dtCPT As DataTable
    '' ''        Dim dtProcedures As DataTable
    '' ''        Dim dtProductInfo As DataTable
    '' ''        Dim dtDeviceType As DataTable
    '' ''        Dim dtLeadType As DataTable
    '' ''        Dim dtDateRemoved As DataTable
    '' ''        Dim dtPhysicalLocation As DataTable
    '' ''        Dim dtLeadLocation As DataTable
    '' ''        Dim dtPacingThreshold As DataTable
    '' ''        Dim dtSensing As DataTable
    '' ''        Dim dtImpedance As DataTable


    '' ''        Dim nDOS As Int16
    '' ''        Dim nCPT As Int16
    '' ''        Dim nProcedure As Int16
    '' ''        Dim nProductInfo As Int16
    '' ''        Dim nDeviceType As Int16
    '' ''        Dim nLeadType As Int16
    '' ''        Dim nDateRemoved As Int16
    '' ''        Dim nPhysicalLocation As Int16
    '' ''        Dim nLeadLocation As Int16
    '' ''        Dim nPacingThreshold As Int16
    '' ''        Dim nSensing As Int16
    '' ''        Dim nImpedance As Int16


    '' ''        Dim strdtQry As String
    '' ''        Dim strCptQry As String
    '' ''        Dim strProcedureQry As String
    '' ''        Dim strProductInfoQry As String
    '' ''        Dim strDeviceTypeQry As String
    '' ''        Dim strLeadTypeQry As String
    '' ''        Dim strDateRemovedQry As String
    '' ''        Dim strPhysicalLocationQry As String
    '' ''        Dim strLeadLocationQry As String
    '' ''        Dim strPacingThresholdQry As String
    '' ''        Dim strSensingQry As String
    '' ''        Dim strImpedanceQry
    '' ''        Dim strconcatCPT1 As String = ""
    '' ''        Dim nextRow As Integer
    '' ''        Dim strCombine As String = ""



    '' ''        strdtQry = "SELECT Distinct isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0)as nClinicID,dtDateofImplant FROM CV_CardiologyDevice WHERE nPatientID='" & pID & "'  order by dtDateofImplant"
    '' ''        Dim oDB As New gloStream.gloDataBase.gloDataBase
    '' ''        oDB.Connect(GetConnectionString)
    '' ''        dtDateofImplant = oDB.ReadQueryDataTable(strdtQry)
    '' ''        oDB.Disconnect()

    '' ''        With dtDateofImplant
    '' ''            If IsNothing(dtDateofImplant) = False Then
    '' ''                For nDOS = 0 To dtDateofImplant.Rows.Count - 1
    '' ''                    Dim CardiologyDeviceID As Int64 = 0
    '' ''                    Dim PatientID As Int64 = 0
    '' ''                    Dim VisitID As Int64 = 0
    '' ''                    Dim ExamID As Int64 = 0
    '' ''                    Dim ClinicID As Int64 = 0
    '' ''                    Dim DateofImplant As Date


    '' ''                    Dim count As Integer = nDOS + 1
    '' ''                    If CStr(dtDateofImplant.Rows(nDOS)("dtDateofImplant")).Trim <> "" Then
    '' ''                        C1Cardiology.Rows.Add()
    '' ''                        _Row = C1Cardiology.Rows.Count - 1
    '' ''                        ''set the properties for newly added row
    '' ''                        With C1Cardiology.Rows(_Row)
    '' ''                            .AllowEditing = False
    '' ''                            .ImageAndText = True
    '' ''                            .Height = 24
    '' ''                            .IsNode = True
    '' ''                            .Node.Level = 0
    '' ''                            .Node.Data = Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateofImplant")).ToShortDateString
    '' ''                            .Node.Image = ImgPatientTab.Images(21)
    '' ''                        End With
    '' ''                        nextRow = _Row
    '' ''                        With C1Cardiology
    '' ''                            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                            .SetData(_Row, COL_ExamID, dtDateofImplant.Rows(nDOS)("nExamID"))
    '' ''                            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                            .SetData(_Row, COL_ClinicID, dtDateofImplant.Rows(nDOS)("nClinicID"))
    '' ''                            ''.SetData(_Row, COL_CardiologyDeviceID, dtDateofImplant.Rows(nDOS)("nCardiologyDeviceID"))
    '' ''                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateofImplant")).ToShortDateString)

    '' ''                            PatientID = dtDateofImplant.Rows(nDOS)("nPatientID")
    '' ''                            VisitID = dtDateofImplant.Rows(nDOS)("nVisitID")
    '' ''                            ExamID = dtDateofImplant.Rows(nDOS)("nExamID")
    '' ''                            ClinicID = dtDateofImplant.Rows(nDOS)("nClinicID")
    '' ''                            ''CardiologyDeviceID = dtDateofImplant.Rows(nDOS)("nCardiologyDeviceID")
    '' ''                        End With


    '' ''                        Dim dtImplantDate As Date = Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateofImplant")).ToShortDateString()


    '' ''                        '' Query for selecting CPTCode ''
    '' ''                        strCptQry = "SELECT DISTINCT isnull(sCPTCode,'') as CPTCode from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sCPTCode<>''"
    '' ''                        oDB.Connect(GetConnectionString)
    '' ''                        dtCPT = oDB.ReadQueryDataTable(strCptQry)
    '' ''                        oDB.Disconnect()


    '' ''                        With dtCPT
    '' ''                            If IsNothing(dtCPT) = False Then
    '' ''                                If dtCPT.Rows.Count > 0 Then
    '' ''                                    C1Cardiology.Rows.Add()
    '' ''                                    _Row = C1Cardiology.Rows.Count - 1
    '' ''                                    With C1Cardiology.Rows(_Row)
    '' ''                                        .AllowEditing = False
    '' ''                                        .ImageAndText = True
    '' ''                                        .Height = 24
    '' ''                                        .IsNode = True
    '' ''                                        .Node.Level = 1
    '' ''                                        .Node.Data = "CPT Code"
    '' ''                                        .Node.Image = ImgPatientTab.Images(14)
    '' ''                                    End With
    '' ''                                    With C1Cardiology
    '' ''                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                    End With
    '' ''                                End If
    '' ''                                For nCPT = 0 To dtCPT.Rows.Count - 1
    '' ''                                    Dim strCPT As String = dtCPT.Rows(nCPT)("CPTCode").ToString()
    '' ''                                    If strCPT.Trim <> "" Then
    '' ''                                        C1Cardiology.Rows.Add()
    '' ''                                        _Row = C1Cardiology.Rows.Count - 1
    '' ''                                        With C1Cardiology.Rows(_Row)
    '' ''                                            .AllowEditing = True
    '' ''                                            .ImageAndText = True
    '' ''                                            .Height = 24
    '' ''                                            .IsNode = True
    '' ''                                            .Node.Level = 2
    '' ''                                            .TextAlign = TextAlignEnum.LeftCenter
    '' ''                                            .Node.Data = strCPT
    '' ''                                            .Node.Image = ImgPatientTab.Images(7)
    '' ''                                        End With
    '' ''                                        With C1Cardiology
    '' ''                                            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                        End With
    '' ''                                    End If
    '' ''                                Next
    '' ''                            End If
    '' ''                        End With


    '' ''                        '' Query for selecting Procedures ''
    '' ''                        strProcedureQry = "SELECT DISTINCT isnull(sProcedures,'') as Procedures from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sProcedures<>'' "
    '' ''                        oDB.Connect(GetConnectionString)
    '' ''                        dtProcedures = oDB.ReadQueryDataTable(strProcedureQry)
    '' ''                        oDB.Disconnect()


    '' ''                        With dtProcedures
    '' ''                            If IsNothing(dtProcedures) = False Then
    '' ''                                If dtProcedures.Rows.Count > 0 Then
    '' ''                                    C1Cardiology.Rows.Add()
    '' ''                                    _Row = C1Cardiology.Rows.Count - 1
    '' ''                                    With C1Cardiology.Rows(_Row)
    '' ''                                        .AllowEditing = False
    '' ''                                        .ImageAndText = True
    '' ''                                        .Height = 24
    '' ''                                        .IsNode = True
    '' ''                                        .Node.Level = 1
    '' ''                                        .Node.Data = "Procedures"
    '' ''                                        .Node.Image = ImgPatientTab.Images(15)
    '' ''                                    End With
    '' ''                                    With C1Cardiology
    '' ''                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                    End With
    '' ''                                End If
    '' ''                                For nProcedure = 0 To dtProcedures.Rows.Count - 1
    '' ''                                    Dim strProcedures As String = dtProcedures.Rows(nProcedure)("Procedures").ToString()
    '' ''                                    If strProcedures.Trim <> "" Then
    '' ''                                        C1Cardiology.Rows.Add()
    '' ''                                        _Row = C1Cardiology.Rows.Count - 1
    '' ''                                        With C1Cardiology.Rows(_Row)
    '' ''                                            .AllowEditing = True
    '' ''                                            .ImageAndText = True
    '' ''                                            .Height = 24
    '' ''                                            .IsNode = True
    '' ''                                            .Node.Level = 2
    '' ''                                            .TextAlign = TextAlignEnum.LeftCenter
    '' ''                                            .Node.Data = strProcedures
    '' ''                                            .Node.Image = ImgPatientTab.Images(7)
    '' ''                                        End With
    '' ''                                        With C1Cardiology
    '' ''                                            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                        End With
    '' ''                                    End If
    '' ''                                Next
    '' ''                            End If
    '' ''                        End With




    '' ''                        '' Query for selecting Type of Device ''
    '' ''                        strCptQry = "SELECT DISTINCT isnull(sDeviceType,'') as DeviceType from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sDeviceType<>'' "
    '' ''                        oDB.Connect(GetConnectionString)
    '' ''                        dtDeviceType = oDB.ReadQueryDataTable(strCptQry)
    '' ''                        oDB.Disconnect()


    '' ''                        With dtDeviceType
    '' ''                            If IsNothing(dtDeviceType) = False Then
    '' ''                                If dtDeviceType.Rows.Count >= 0 Then
    '' ''                                    C1Cardiology.Rows.Add()
    '' ''                                    _Row = C1Cardiology.Rows.Count - 1
    '' ''                                    With C1Cardiology.Rows(_Row)
    '' ''                                        .AllowEditing = False
    '' ''                                        .ImageAndText = True
    '' ''                                        .Height = 24
    '' ''                                        .IsNode = True
    '' ''                                        .Node.Level = 1
    '' ''                                        .Node.Data = "Type Of Device"
    '' ''                                        .Node.Image = ImgPatientTab.Images(26)
    '' ''                                    End With
    '' ''                                    With C1Cardiology
    '' ''                                        .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                        .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                    End With
    '' ''                                End If
    '' ''                                For nDeviceType = 0 To dtDeviceType.Rows.Count - 1
    '' ''                                    Dim strDeviceType As String = dtDeviceType.Rows(nDeviceType)("DeviceType")
    '' ''                                    If strDeviceType.Trim <> "" Then
    '' ''                                        C1Cardiology.Rows.Add()
    '' ''                                        _Row = C1Cardiology.Rows.Count - 1

    '' ''                                        With C1Cardiology.Rows(_Row)
    '' ''                                            .AllowEditing = True
    '' ''                                            .ImageAndText = True
    '' ''                                            .Height = 24
    '' ''                                            .IsNode = True
    '' ''                                            .Node.Level = 2
    '' ''                                            .Node.Data = strDeviceType
    '' ''                                            .Node.Image = ImgPatientTab.Images(27)
    '' ''                                        End With
    '' ''                                        With C1Cardiology
    '' ''                                            .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                            .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                        End With


    '' ''                                        '' Query for selecting Product Information ''
    '' ''                                        strProcedureQry = "SELECT DISTINCT isnull(sProductName,'') as ProductName,isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification,isnull(sProductSerialNo,'') as ProductSerialNo,isnull(sManufacturerModelNo,'') as ManufacturerModelNo,isnull(sLeadType,'') as LeadType,dtDateRemoved ,isnull(sPhysicalLocation,'') as PhysicalLocation , isnull(sLeadLocation,'') as LeadLocation , isnull(sThresholdAtrial,'') as ThresholdAtrial,isnull(SThresholdVentricular,'') as ThresholdVentricular, isnull(sSensingAtrial,'') as SensingAtrial,isnull(sSensingVentricular,'') as SensingVentricular,isnull(sImpedenceAtrial,'') as ImpedenceAtrial,isnull(sImpedenceVentricular,'') as ImpedenceVentricular from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' and sDeviceType='" & strDeviceType.Replace("'", "''") & "'"
    '' ''                                        oDB.Connect(GetConnectionString)
    '' ''                                        dtProductInfo = oDB.ReadQueryDataTable(strProcedureQry)
    '' ''                                        oDB.Disconnect()

    '' ''                                        With dtProductInfo
    '' ''                                            If IsNothing(dtProductInfo) = False Then
    '' ''                                                For nProductInfo = 0 To dtProductInfo.Rows.Count - 1
    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ProductName").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ProductName").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Product Name" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ProductName").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If
    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("DeviceManufacturer").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("DeviceManufacturer").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Device Manufacturer" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("DeviceManufacturer").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If
    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ProductSpecification").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ProductSpecification").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Product Specification" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ProductSpecification").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If
    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ProductSerialNo").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ProductSerialNo").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Product Serial No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ProductSerialNo").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If
    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ManufacturerModelNo").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ManufacturerModelNo").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Manufacturer Model No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ManufacturerModelNo").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("LeadType").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("LeadType").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Lead Type" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("LeadType").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Date Removed" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("PhysicalLocation").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("PhysicalLocation").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Physical Location" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("PhysicalLocation").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("LeadLocation").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("LeadLocation").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Lead Location" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("LeadLocation").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ThresholdAtrial").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ThresholdAtrial").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Threshold Atrial" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ThresholdAtrial").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ThresholdVentricular").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ThresholdVentricular").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Threshold Ventricular" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ThresholdVentricular").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("SensingAtrial").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("SensingAtrial").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Sensing Atrial" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("SensingAtrial").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("SensingVentricular").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("SensingVentricular").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Sensing Ventricular" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("SensingVentricular").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ImpedenceAtrial").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ImpedenceAtrial").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Impedence Atrial" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ImpedenceAtrial").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                    If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ImpedenceVentricular").ToString()) Then
    '' ''                                                        If (dtProductInfo.Rows(nProductInfo)("ImpedenceVentricular").ToString() <> "") Then
    '' ''                                                            C1Cardiology.Rows.Add()
    '' ''                                                            _Row = C1Cardiology.Rows.Count - 1
    '' ''                                                            With C1Cardiology.Rows(_Row)
    '' ''                                                                .AllowEditing = True
    '' ''                                                                .ImageAndText = True
    '' ''                                                                .Height = 24
    '' ''                                                                .IsNode = True
    '' ''                                                                .Node.Level = 3
    '' ''                                                                .Node.Data = "Impedence Ventricular" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ImpedenceVentricular").ToString()
    '' ''                                                                .Node.Image = ImgPatientTab.Images(7)
    '' ''                                                            End With
    '' ''                                                            With C1Cardiology
    '' ''                                                                .SetData(_Row, COL_PatientID, dtDateofImplant.Rows(nDOS)("nPatientID"))
    '' ''                                                                .SetData(_Row, COL_VisitID, dtDateofImplant.Rows(nDOS)("nVisitID"))
    '' ''                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplant.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
    '' ''                                                            End With
    '' ''                                                        End If
    '' ''                                                    End If

    '' ''                                                Next   ''For nProductInfo = 0 To dtProductInfo.Rows.Count - 1
    '' ''                                            End If
    '' ''                                        End With
    '' ''                                    End If
    '' ''                                Next    ''For nDeviceType = 0 To dtDeviceType.Rows.Count - 1
    '' ''                            End If   ''If IsNothing(dtDeviceType) = False Then
    '' ''                        End With


    '' ''                    End If     '' CStr(dtDateofImplant.Rows(nDOS)("DateofStudy")).Trim <> "" Then
    '' ''                Next   ''For nDOS = 0 To dtDateofImplant.Rows.Count - 1
    '' ''            End If
    '' ''        End With   '' With dtDateofImplant



    '' ''        dtDateofImplant = Nothing

    '' ''    Catch ex As Exception
    '' ''        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '' ''        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '' ''    End Try
    '' ''    '''''''' By Ujwala - for Implant Device - as on 20101020
    '' ''End Sub
    Public Sub FillImplantDevice()
        '''''''' By Ujwala - for Implant Device - as on 20101020
        Dim dtCPT As DataTable = Nothing
        Dim dtProcedures As DataTable = Nothing
        Dim dtProductInfo As DataTable = Nothing
        Dim dtDeviceType As DataTable = Nothing
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try
            Dim _Row As Integer
            SetImplantGridStyle()
            ''set properties of treeview in flexgrid
            With C1Cardiology
                .Tree.Column = COL_DateofImplant
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
                .Cols(COL_DateofImplant).TextAlign = TextAlignEnum.LeftCenter
            End With

            ''Dim dtDateofImplant As DataTable

            'Dim dtLeadType As DataTable
            'Dim dtDateRemoved As DataTable
            'Dim dtPhysicalLocation As DataTable
            'Dim dtLeadLocation As DataTable
            'Dim dtPacingThreshold As DataTable
            'Dim dtSensing As DataTable
            'Dim dtImpedance As DataTable


            Dim nDOS As Int16
            Dim nCPT As Int16
            Dim nProcedure As Int16
            Dim nProductInfo As Int16
            Dim nDeviceType As Int16
            'Dim nLeadType As Int16
            'Dim nDateRemoved As Int16
            'Dim nPhysicalLocation As Int16
            'Dim nLeadLocation As Int16
            'Dim nPacingThreshold As Int16
            'Dim nSensing As Int16
            'Dim nImpedance As Int16


            Dim strdtQry As String
            Dim strCptQry As String
            Dim strProcedureQry As String
            'Dim strProductInfoQry As String
            'Dim strDeviceTypeQry As String
            'Dim strLeadTypeQry As String
            'Dim strDateRemovedQry As String
            'Dim strPhysicalLocationQry As String
            'Dim strLeadLocationQry As String
            'Dim strPacingThresholdQry As String
            'Dim strSensingQry As String
            'Dim strImpedanceQry
            Dim strconcatCPT1 As String = ""
            Dim nextRow As Integer
            Dim strCombine As String = ""



            strdtQry = "SELECT Distinct isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0)as nClinicID,dtDateofImplant FROM CV_CardiologyDevice WHERE nPatientID='" & pID & "'  order by dtDateofImplant"

            oDB.Connect(GetConnectionString)
            dtDateofImplantDtls = oDB.ReadQueryDataTable(strdtQry)
            oDB.Disconnect()

            With dtDateofImplantDtls
                If IsNothing(dtDateofImplantDtls) = False Then
                    For nDOS = 0 To dtDateofImplantDtls.Rows.Count - 1
                        Dim CardiologyDeviceID As Int64 = 0
                        Dim PatientID As Int64 = 0
                        Dim VisitID As Int64 = 0
                        Dim ExamID As Int64 = 0
                        Dim ClinicID As Int64 = 0
                        'Dim DateofImplant As Date


                        Dim count As Integer = nDOS + 1
                        If CStr(dtDateofImplantDtls.Rows(nDOS)("dtDateofImplant")).Trim <> "" Then
                            C1Cardiology.Rows.Add()
                            _Row = C1Cardiology.Rows.Count - 1
                            ''set the properties for newly added row
                            With C1Cardiology.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                .Node.Data = Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateofImplant")).ToShortDateString
                                .Node.Image = ImgPatientTab.Images(21)
                            End With
                            nextRow = _Row
                            With C1Cardiology
                                .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                .SetData(_Row, COL_ExamID, dtDateofImplantDtls.Rows(nDOS)("nExamID"))
                                .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                .SetData(_Row, COL_ClinicID, dtDateofImplantDtls.Rows(nDOS)("nClinicID"))
                                ''.SetData(_Row, COL_CardiologyDeviceID, dtDateofImplant.Rows(nDOS)("nCardiologyDeviceID"))
                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateofImplant")).ToShortDateString)

                                PatientID = dtDateofImplantDtls.Rows(nDOS)("nPatientID")
                                VisitID = dtDateofImplantDtls.Rows(nDOS)("nVisitID")
                                ExamID = dtDateofImplantDtls.Rows(nDOS)("nExamID")
                                ClinicID = dtDateofImplantDtls.Rows(nDOS)("nClinicID")
                                ''CardiologyDeviceID = dtDateofImplant.Rows(nDOS)("nCardiologyDeviceID")
                            End With


                            Dim dtImplantDate As Date = Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateofImplant")).ToShortDateString()


                            '' Query for selecting CPTCode ''
                            strCptQry = "SELECT DISTINCT isnull(sCPTCode,'') as CPTCode from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sCPTCode<>''"
                            oDB.Connect(GetConnectionString)
                            dtCPT = oDB.ReadQueryDataTable(strCptQry)
                            oDB.Disconnect()


                            With dtCPT
                                If IsNothing(dtCPT) = False Then
                                    If dtCPT.Rows.Count > 0 Then
                                        C1Cardiology.Rows.Add()
                                        _Row = C1Cardiology.Rows.Count - 1
                                        With C1Cardiology.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "CPT Code"
                                            .Node.Image = ImgPatientTab.Images(14)
                                        End With
                                        With C1Cardiology
                                            .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                        End With
                                    End If
                                    For nCPT = 0 To dtCPT.Rows.Count - 1
                                        Dim strCPT As String = dtCPT.Rows(nCPT)("CPTCode").ToString()
                                        If strCPT.Trim <> "" Then
                                            C1Cardiology.Rows.Add()
                                            _Row = C1Cardiology.Rows.Count - 1
                                            With C1Cardiology.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .TextAlign = TextAlignEnum.LeftCenter
                                                .Node.Data = strCPT
                                                .Node.Image = ImgPatientTab.Images(7)
                                            End With
                                            With C1Cardiology
                                                .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                            End With
                                        End If
                                    Next
                                    If (IsNothing(dtCPT) = False) Then
                                        dtCPT.Dispose()
                                        dtCPT = Nothing
                                    End If
                                End If
                            End With


                            '' Query for selecting Procedures ''
                            strProcedureQry = "SELECT DISTINCT isnull(sProcedures,'') as Procedures from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sProcedures<>'' "
                            oDB.Connect(GetConnectionString)
                            dtProcedures = oDB.ReadQueryDataTable(strProcedureQry)
                            oDB.Disconnect()


                            With dtProcedures
                                If IsNothing(dtProcedures) = False Then
                                    If dtProcedures.Rows.Count > 0 Then
                                        C1Cardiology.Rows.Add()
                                        _Row = C1Cardiology.Rows.Count - 1
                                        With C1Cardiology.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Procedures"
                                            .Node.Image = ImgPatientTab.Images(15)
                                        End With
                                        With C1Cardiology
                                            .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                        End With
                                    End If
                                    For nProcedure = 0 To dtProcedures.Rows.Count - 1
                                        Dim strProcedures As String = dtProcedures.Rows(nProcedure)("Procedures").ToString()
                                        If strProcedures.Trim <> "" Then
                                            C1Cardiology.Rows.Add()
                                            _Row = C1Cardiology.Rows.Count - 1
                                            With C1Cardiology.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .TextAlign = TextAlignEnum.LeftCenter
                                                .Node.Data = strProcedures
                                                .Node.Image = ImgPatientTab.Images(7)
                                            End With
                                            With C1Cardiology
                                                .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                            End With
                                        End If
                                    Next
                                    If (IsNothing(dtProcedures) = False) Then
                                        dtProcedures.Dispose()
                                        dtProcedures = Nothing
                                    End If
                                End If
                            End With

                            ''''''''''''''''''''''By Ujwala as on 11192010
                            strProcedureQry = "SELECT DISTINCT isnull(sPhysicalLocation,'') as PhysicalLocation , isnull(sLeadLocation,'') as LeadLocation , isnull(sThresholdAtrial,'') as ThresholdAtrial,isnull(SThresholdVentricular,'') as ThresholdVentricular, isnull(sSensingAtrial,'') as SensingAtrial,isnull(sSensingVentricular,'') as SensingVentricular,isnull(sImpedenceAtrial,'') as ImpedenceAtrial,isnull(sImpedenceVentricular,'') as ImpedenceVentricular from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' "
                            oDB.Connect(GetConnectionString)
                            dtProductInfo = oDB.ReadQueryDataTable(strProcedureQry)
                            oDB.Disconnect()

                            If Not IsNothing(dtProductInfo.Rows(0)("PhysicalLocation").ToString()) Then
                                If (dtProductInfo.Rows(0)("PhysicalLocation").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Physical Location" + " " + ":" + " " + dtProductInfo.Rows(0)("PhysicalLocation").ToString()
                                        .Node.Image = ImgPatientTab.Images(29)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("LeadLocation").ToString()) Then
                                If (dtProductInfo.Rows(0)("LeadLocation").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Lead Location" + " " + ":" + " " + dtProductInfo.Rows(0)("LeadLocation").ToString()
                                        .Node.Image = ImgPatientTab.Images(28)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("ThresholdAtrial").ToString()) Then
                                If (dtProductInfo.Rows(0)("ThresholdAtrial").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Threshold Atrial" + " " + ":" + " " + dtProductInfo.Rows(0)("ThresholdAtrial").ToString()
                                        .Node.Image = ImgPatientTab.Images(30)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("ThresholdVentricular").ToString()) Then
                                If (dtProductInfo.Rows(0)("ThresholdVentricular").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Threshold Ventricular" + " " + ":" + " " + dtProductInfo.Rows(0)("ThresholdVentricular").ToString()
                                        .Node.Image = ImgPatientTab.Images(31)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("SensingAtrial").ToString()) Then
                                If (dtProductInfo.Rows(0)("SensingAtrial").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Sensing Atrial" + " " + ":" + " " + dtProductInfo.Rows(0)("SensingAtrial").ToString()
                                        .Node.Image = ImgPatientTab.Images(32)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("SensingVentricular").ToString()) Then
                                If (dtProductInfo.Rows(0)("SensingVentricular").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Sensing Ventricular" + " " + ":" + " " + dtProductInfo.Rows(0)("SensingVentricular").ToString()
                                        .Node.Image = ImgPatientTab.Images(33)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("ImpedenceAtrial").ToString()) Then
                                If (dtProductInfo.Rows(0)("ImpedenceAtrial").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Impedence Atrial" + " " + ":" + " " + dtProductInfo.Rows(0)("ImpedenceAtrial").ToString()
                                        .Node.Image = ImgPatientTab.Images(34)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If

                            If Not IsNothing(dtProductInfo.Rows(0)("ImpedenceVentricular").ToString()) Then
                                If (dtProductInfo.Rows(0)("ImpedenceVentricular").ToString() <> "") Then
                                    C1Cardiology.Rows.Add()
                                    _Row = C1Cardiology.Rows.Count - 1
                                    With C1Cardiology.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 1
                                        .Node.Data = "Impedence Ventricular" + " " + ":" + " " + dtProductInfo.Rows(0)("ImpedenceVentricular").ToString()
                                        .Node.Image = ImgPatientTab.Images(35)
                                    End With
                                    With C1Cardiology
                                        .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                        .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                    End With
                                End If
                            End If
                            If (IsNothing(dtProductInfo) = False) Then
                                dtProductInfo.Dispose()
                                dtProductInfo = Nothing
                            End If
                            ''''''''''''''''''''''By Ujwala as on 11192010

                            '' Query for selecting Type of Device ''
                            strCptQry = "SELECT DISTINCT isnull(sDeviceType,'') as DeviceType from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' AND sDeviceType<>'' "
                            oDB.Connect(GetConnectionString)
                            dtDeviceType = oDB.ReadQueryDataTable(strCptQry)
                            oDB.Disconnect()


                            With dtDeviceType
                                If IsNothing(dtDeviceType) = False Then
                                    If dtDeviceType.Rows.Count >= 0 Then
                                        C1Cardiology.Rows.Add()
                                        _Row = C1Cardiology.Rows.Count - 1
                                        With C1Cardiology.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Type Of Device"
                                            .Node.Image = ImgPatientTab.Images(26)
                                        End With
                                        With C1Cardiology
                                            .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                            .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                        End With
                                    End If
                                    For nDeviceType = 0 To dtDeviceType.Rows.Count - 1
                                        Dim strDeviceType As String = dtDeviceType.Rows(nDeviceType)("DeviceType")
                                        If strDeviceType.Trim <> "" Then
                                            C1Cardiology.Rows.Add()
                                            _Row = C1Cardiology.Rows.Count - 1

                                            With C1Cardiology.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = strDeviceType
                                                .Node.Image = ImgPatientTab.Images(27)
                                            End With
                                            With C1Cardiology
                                                .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                            End With


                                            '' Query for selecting Product Information ''
                                            strProcedureQry = "SELECT DISTINCT isnull(sProductName,'') as ProductName,isnull(sDeviceManufacturer,'') as DeviceManufacturer,isnull(sProductSpecification,'') as ProductSpecification,isnull(sProductSerialNo,'') as ProductSerialNo,isnull(sManufacturerModelNo,'') as ManufacturerModelNo,isnull(sLeadType,'') as LeadType,dtDateRemoved ,isnull(sPhysicalLocation,'') as PhysicalLocation , isnull(sLeadLocation,'') as LeadLocation , isnull(sThresholdAtrial,'') as ThresholdAtrial,isnull(SThresholdVentricular,'') as ThresholdVentricular, isnull(sSensingAtrial,'') as SensingAtrial,isnull(sSensingVentricular,'') as SensingVentricular,isnull(sImpedenceAtrial,'') as ImpedenceAtrial,isnull(sImpedenceVentricular,'') as ImpedenceVentricular from CV_CardiologyDevice where nPatientID=" & pID & " AND dtDateofImplant='" & dtImplantDate & "' and sDeviceType='" & strDeviceType.Replace("'", "''") & "'"
                                            oDB.Connect(GetConnectionString)
                                            dtProductInfo = oDB.ReadQueryDataTable(strProcedureQry)
                                            oDB.Disconnect()

                                            With dtProductInfo
                                                If IsNothing(dtProductInfo) = False Then
                                                    For nProductInfo = 0 To dtProductInfo.Rows.Count - 1
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ProductName").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("ProductName").ToString() <> "") Then
                                                                C1Cardiology.Rows.Add()
                                                                _Row = C1Cardiology.Rows.Count - 1
                                                                With C1Cardiology.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 3
                                                                    .Node.Data = "Product Name" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ProductName").ToString()
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1Cardiology
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("DeviceManufacturer").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("DeviceManufacturer").ToString() <> "") Then
                                                                C1Cardiology.Rows.Add()
                                                                _Row = C1Cardiology.Rows.Count - 1
                                                                With C1Cardiology.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 3
                                                                    .Node.Data = "Device Manufacturer" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("DeviceManufacturer").ToString()
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1Cardiology
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ProductSpecification").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("ProductSpecification").ToString() <> "") Then
                                                                C1Cardiology.Rows.Add()
                                                                _Row = C1Cardiology.Rows.Count - 1
                                                                With C1Cardiology.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 3
                                                                    .Node.Data = "Product Specification" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ProductSpecification").ToString()
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1Cardiology
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ProductSerialNo").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("ProductSerialNo").ToString() <> "") Then
                                                                C1Cardiology.Rows.Add()
                                                                _Row = C1Cardiology.Rows.Count - 1
                                                                With C1Cardiology.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 3
                                                                    .Node.Data = "Product Serial No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ProductSerialNo").ToString()
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1Cardiology
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If
                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("ManufacturerModelNo").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("ManufacturerModelNo").ToString() <> "") Then
                                                                C1Cardiology.Rows.Add()
                                                                _Row = C1Cardiology.Rows.Count - 1
                                                                With C1Cardiology.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 3
                                                                    .Node.Data = "Manufacturer Model No" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("ManufacturerModelNo").ToString()
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1Cardiology
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If

                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("LeadType").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("LeadType").ToString() <> "") Then
                                                                C1Cardiology.Rows.Add()
                                                                _Row = C1Cardiology.Rows.Count - 1
                                                                With C1Cardiology.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 3
                                                                    .Node.Data = "Lead Type" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("LeadType").ToString()
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1Cardiology
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If

                                                        If Not IsNothing(dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString()) Then
                                                            If (dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString() <> "") Then
                                                                C1Cardiology.Rows.Add()
                                                                _Row = C1Cardiology.Rows.Count - 1
                                                                With C1Cardiology.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 3
                                                                    .Node.Data = "Date Removed" + " " + ":" + " " + dtProductInfo.Rows(nProductInfo)("dtDateRemoved").ToString()
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1Cardiology
                                                                    .SetData(_Row, COL_PatientID, dtDateofImplantDtls.Rows(nDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtDateofImplantDtls.Rows(nDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtDateofImplantDtls.Rows(nDOS)("dtDateOfImplant")).ToShortDateString)
                                                                End With
                                                            End If
                                                        End If



                                                    Next   ''For nProductInfo = 0 To dtProductInfo.Rows.Count - 1
                                                    If (IsNothing(dtProductInfo) = False) Then
                                                        dtProductInfo.Dispose()
                                                        dtProductInfo = Nothing
                                                    End If
                                                End If
                                            End With
                                        End If
                                    Next    ''For nDeviceType = 0 To dtDeviceType.Rows.Count - 1
                                    If (IsNothing(dtDeviceType) = False) Then
                                        dtDeviceType.Dispose()
                                        dtDeviceType = Nothing
                                    End If
                                End If   ''If IsNothing(dtDeviceType) = False Then
                            End With


                        End If     '' CStr(dtDateofImplantDtls.Rows(nDOS)("DateofStudy")).Trim <> "" Then
                    Next   ''For nDOS = 0 To dtDateofImplantDtls.Rows.Count - 1
                End If
            End With   '' With dtDateofImplantDtls
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(dtCPT) = False) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            If (IsNothing(dtProcedures) = False) Then
                dtProcedures.Dispose()
                dtProcedures = Nothing
            End If
            If (IsNothing(dtProductInfo) = False) Then
                dtProductInfo.Dispose()
                dtProductInfo = Nothing
            End If
            If (IsNothing(dtDeviceType) = False) Then
                dtDeviceType.Dispose()
                dtDeviceType = Nothing
            End If
          
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        '''''''' By Ujwala - for Implant Device - as on 20101020
    End Sub


    Private Sub FillImagingST(ByVal PatientID As Long)
        ''dhruv 20091130
        ''Avoid the flickering
        C1CV_StressTest.Redraw = False
        mPatientID = PatientID
        'If IsNothing(dtStudyDateDtls) Then
        ''SetGridStyle()
        FillStressTest()
        'End If
        C1CV_StressTest.Redraw = True

    End Sub

    Private Sub SetGridStyle()
        'Declare a variable
        'Dim cStyle As C1.Win.C1FlexGrid.CellStyle


        'Dim struser As String
        With C1CV_StressTest
            .Clear(ClearFlags.All)
            'Dim i As Int16
            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (.Width - 20) / 3

            .Cols.Count = COLUMN_COUNT
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False
            C1CV_StressTest.ShowCellLabels = True
            .Styles.ClearUnused()

            .Cols(COL_StressID).Width = .Width * 0
            .Cols(COL_StressID).AllowEditing = False
            .SetData(0, COL_StressID, "Stress ID")
            .Cols(COL_StressID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PatientID_ST).Width = .Width * 0
            .Cols(COL_PatientID_ST).AllowEditing = False
            .SetData(0, COL_PatientID_ST, "Patient ID")
            .Cols(COL_PatientID_ST).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ExamID_ST).Width = .Width * 0
            .Cols(COL_ExamID_ST).AllowEditing = False
            .SetData(0, COL_ExamID_ST, "Exam ID")
            .Cols(COL_ExamID_ST).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_VisitID_ST).Width = .Width * 0
            .Cols(COL_VisitID_ST).AllowEditing = False
            .SetData(0, COL_VisitID_ST, "Visit ID")
            .Cols(COL_VisitID_ST).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_ClinicID_ST).Width = .Width * 0
            .Cols(COL_ClinicID_ST).AllowEditing = False
            .SetData(0, COL_ClinicID_ST, "Clinic ID")
            .Cols(COL_ClinicID_ST).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofStudy).Width = _TotalWidth * 1
            .SetData(0, COL_DateofStudy, "Stress Test")
            .Cols(COL_DateofStudy).DataType = GetType(String)
            .Cols(COL_DateofStudy).AllowEditing = False
            .Cols(COL_DateofStudy).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TestType).Width = .Width * 0
            .SetData(0, COL_TestType, "Test Type")
            .Cols(COL_TestType).AllowEditing = False
            .Cols(COL_TestType).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_Result).Width = .Width * 0
            .SetData(0, COL_Result, "Result")
            .Cols(COL_Result).AllowEditing = False
            .Cols(COL_Result).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofStudyInvisible).Width = 0
            .SetData(0, COL_DateofStudyInvisible, "Date of Study Invisible")
            .Cols(COL_DateofStudyInvisible).DataType = GetType(String)
            .Cols(COL_DateofStudyInvisible).AllowEditing = False
            .Cols(COL_DateofStudyInvisible).TextAlignFixed = TextAlignEnum.LeftCenter

            ' swaraj - 11-05-2010 - extra columns added ''

            .Cols(COL_PHYSICIAN).Width = .Width * 0
            .SetData(0, COL_PHYSICIAN, "PHYSICIAN")
            .Cols(COL_PHYSICIAN).AllowEditing = False
            .Cols(COL_PHYSICIAN).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_RestingHeartRate).Width = .Width * 0
            .SetData(0, COL_RestingHeartRate, "Resting Heart Rate")
            .Cols(COL_RestingHeartRate).AllowEditing = False
            .Cols(COL_RestingHeartRate).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PeakHeartRate).Width = .Width * 0
            .SetData(0, COL_PeakHeartRate, "Peak Heart Rate")
            .Cols(COL_PeakHeartRate).AllowEditing = False
            .Cols(COL_PeakHeartRate).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_NarrativeSummary).Width = .Width * 0
            .SetData(0, COL_NarrativeSummary, "Narrative Summary")
            .Cols(COL_NarrativeSummary).AllowEditing = False
            .Cols(COL_NarrativeSummary).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TotalExerciseTime).Width = .Width * 0
            .SetData(0, COL_TotalExerciseTime, "Total Exercise Time")
            .Cols(COL_TotalExerciseTime).AllowEditing = False
            .Cols(COL_TotalExerciseTime).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_EjectionFraction).Width = .Width * 0
            .SetData(0, COL_EjectionFraction, "Ejection Fraction")
            .Cols(COL_EjectionFraction).AllowEditing = False
            .Cols(COL_EjectionFraction).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PARENT).Width = .Width * 0
            .SetData(0, COL_PARENT, "PARENT")
            .Cols(COL_PARENT).AllowEditing = False
            .Cols(COL_PARENT).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_IDENTITY).Width = .Width * 0
            .SetData(0, COL_IDENTITY, "IDENTITY")
            .Cols(COL_IDENTITY).AllowEditing = False
            .Cols(COL_IDENTITY).TextAlignFixed = TextAlignEnum.LeftCenter

            .ExtendLastCol = True
            'swaraj - 11-05-2010 - extra columns added ''


            ''Dim dt1 As DataTable
            ''dt1 = fillusercombo()
            ''Dim strUserName As New System.Text.StringBuilder
            ''For j As Int32 = 0 To dt1.Rows.Count - 1
            ''    If j > 0 Then
            ''        strUserName.Append("|")
            ''    End If
            ''    strUserName.Append(dt1.Rows(j)("sLoginName"))
            ''Next

        End With
    End Sub


    ' ''Private Sub FillStressTest()
    ''''''''''''''''''''' by Ujwala - for New Stress Test moudle Changes as on 20101020
    ' ''gloC1FlexStyle.Style(C1CV_StressTest) ''Ojeswini_01302010

    ' ''Try

    ' ''    Dim _Row As Integer
    ' ''    'Dim i As Integer
    ' ''    'set properties of treeview in flexgrid
    ' ''    With C1CV_StressTest
    ' ''        .Tree.Column = COL_DateofStudy
    ' ''        .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    ' ''        .Tree.LineStyle = Drawing2D.DashStyle.Solid

    ' ''        .Tree.Indent = 15
    ' ''    End With
    ' ''    Dim dtStudyDate As DataTable
    ' ''    Dim dtCPT As DataTable
    ' ''    Dim dtUsers As DataTable
    ' ''    Dim dtTests As DataTable

    ' ''    Dim nDOS As Int16
    ' ''    Dim nCPT As Int16
    ' ''    Dim nUser As Int16


    ' ''    Dim strselecrICD9Qry As String
    ' ''    Dim strselectCPTQry As String
    ' ''    Dim strselectMODQry As String
    ' ''    Dim strconcatCPT1 As String = ""
    ' ''    Dim nextICD As Integer


    ' ''    strselecrICD9Qry = "SELECT Distinct nPatientID,nExamID,nVisitID,nClinicID,dtDateOfStudy as DateofStudy FROM CV_StressTest WHERE  nPatientID = " & mPatientID
    ' ''    Dim oDB As New gloStream.gloDataBase.gloDataBase
    ' ''    oDB.Connect(GetConnectionString)
    ' ''    dtStudyDate = oDB.ReadQueryDataTable(strselecrICD9Qry)
    ' ''    oDB.Disconnect()

    ' ''    With dtStudyDate
    ' ''        If IsNothing(dtStudyDate) = False Then
    ' ''            For nDOS = 0 To .Rows.Count - 1
    ' ''                Dim StressID As Int64 = 0
    ' ''                Dim PatientID As Int64 = 0
    ' ''                Dim VisitID As Int64 = 0
    ' ''                Dim ExamID As Int64 = 0
    ' ''                Dim ClinicID As Int64 = 0
    ' ''                Dim DateofStudy As Date
    ' ''                Dim TestType As String = ""

    ' ''                Dim Result As String = ""

    ' ''                Dim count As Integer = nDOS + 1
    ' ''                If CStr(dtStudyDate.Rows(nDOS)("DateofStudy")).Trim <> "" Then

    ' ''                    C1CV_StressTest.Rows.Add()
    ' ''                    _Row = C1CV_StressTest.Rows.Count - 1
    ' ''                    'set the properties for newly added row
    ' ''                    With C1CV_StressTest.Rows(_Row)
    ' ''                        .AllowEditing = False
    ' ''                        .ImageAndText = True
    ' ''                        .Height = 24
    ' ''                        .IsNode = True
    ' ''                        .Node.Level = 0
    ' ''                        .Node.Data = dtStudyDate.Rows(nDOS)("DateofStudy") '' 02/20/2009
    ' ''                        .Node.Image = ImgPatientTab.Images(13) 'Global.gloEMR.My.Resources.Resources.ICD_09
    ' ''                    End With
    ' ''                    nextICD = _Row
    ' ''                    With C1CV_StressTest
    ' ''                        '.SetData(_Row, COL_DateofStudy, _Row)
    ' ''                        '.SetData(_Row, COL_StressID, dtStudyDate.Rows(nDOS)("nStressID"))
    ' ''                        .SetData(_Row, COL_PatientID_ST, dtStudyDate.Rows(nDOS)("nPatientID"))
    ' ''                        .SetData(_Row, COL_ExamID_ST, dtStudyDate.Rows(nDOS)("nExamID"))
    ' ''                        .SetData(_Row, COL_VisitID_ST, dtStudyDate.Rows(nDOS)("nVisitID"))
    ' ''                        .SetData(_Row, COL_ClinicID_ST, dtStudyDate.Rows(nDOS)("nClinicID"))
    ' ''                        .SetData(_Row, COL_DateofStudyInvisible, dtStudyDate.Rows(nDOS)("DateofStudy"))
    ' ''                        .SetData(_Row, COL_TestType, TestType)
    ' ''                        .SetData(_Row, COL_Result, Result)
    ' ''                        'StressID = dtStudyDate.Rows(nDOS)("nStressID")
    ' ''                        PatientID = dtStudyDate.Rows(nDOS)("nPatientID")
    ' ''                        VisitID = dtStudyDate.Rows(nDOS)("nVisitID")
    ' ''                        ExamID = dtStudyDate.Rows(nDOS)("nExamID")
    ' ''                        ClinicID = dtStudyDate.Rows(nDOS)("nClinicID")
    ' ''                        DateofStudy = dtStudyDate.Rows(nDOS)("DateofStudy")
    ' ''                    End With


    ' ''                    Dim dtDateofStudy As Date = dtStudyDate.Rows(nDOS)("DateofStudy")


    ' ''                    'Query for selecting CPT for current exam
    ' ''                    strselectCPTQry = "SELECT DISTINCT isnull(sCPT,'') as sCPT FROM CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "'"
    ' ''                    oDB.Connect(GetConnectionString)
    ' ''                    dtCPT = oDB.ReadQueryDataTable(strselectCPTQry)
    ' ''                    oDB.Disconnect()

    ' ''                    'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nDOS)("sCPT"))

    ' ''                    With dtCPT
    ' ''                        If IsNothing(dtCPT) = False Then
    ' ''                            If dtCPT.Rows.Count > 0 Then
    ' ''                                C1CV_StressTest.Rows.Add()
    ' ''                                _Row = C1CV_StressTest.Rows.Count - 1
    ' ''                                With C1CV_StressTest.Rows(_Row)
    ' ''                                    .AllowEditing = False
    ' ''                                    .ImageAndText = True
    ' ''                                    .Height = 24
    ' ''                                    .IsNode = True
    ' ''                                    .Node.Level = 1
    ' ''                                    .Node.Data = "CPT" '' "CPT"
    ' ''                                    .Node.Image = ImgPatientTab.Images(14) 'Global.gloEMR.My.Resources.Resources.cpt
    ' ''                                End With
    ' ''                            End If
    ' ''                            For nCPT = 0 To .Rows.Count - 1
    ' ''                                Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPT")
    ' ''                                If strCurrentCPT.Trim <> "" Then
    ' ''                                    C1CV_StressTest.Rows.Add()
    ' ''                                    _Row = C1CV_StressTest.Rows.Count - 1
    ' ''                                    'set the properties for newly added row
    ' ''                                    With C1CV_StressTest.Rows(_Row)
    ' ''                                        .AllowEditing = True
    ' ''                                        .ImageAndText = True
    ' ''                                        .Height = 24
    ' ''                                        .IsNode = True
    ' ''                                        .Node.Level = 2
    ' ''                                        .Node.Data = dtCPT.Rows(nCPT)("sCPT")
    ' ''                                        .Node.Image = ImgPatientTab.Images(7) 'Global.gloEMR.My.Resources.Resources.CPT1
    ' ''                                    End With

    ' ''                                    With C1CV_StressTest
    ' ''                                        ' .SetData(_Row, COL_StressID, StressID)
    ' ''                                        .SetData(_Row, COL_PatientID_ST, PatientID)
    ' ''                                        .SetData(_Row, COL_ExamID_ST, ExamID)
    ' ''                                        .SetData(_Row, COL_VisitID_ST, VisitID)
    ' ''                                        .SetData(_Row, COL_ClinicID_ST, ClinicID)
    ' ''                                        .SetData(_Row, COL_DateofStudyInvisible, DateofStudy)
    ' ''                                    End With
    ' ''                                    strselectMODQry = "SELECT Distinct isnull(sTesttype,'') as TestType,isnull(sResult,'') as Result from CV_StressTest WHERE  nPatientID = " & mPatientID & " and convert(datetime,convert (varchar(50),datepart(mm,dtdateofstudy)) + '/'+ convert(varchar(50),datepart(dd,dtdateofstudy))+'/'+ convert(varchar(50),datepart(yy,dtdateofstudy))) = '" & dtDateofStudy & "' and sCPT='" & strCurrentCPT & "' and sTesttype is not null and sTesttype  <> ''"

    ' ''                                    oDB.Connect(GetConnectionString)
    ' ''                                    dtTests = oDB.ReadQueryDataTable(strselectMODQry)
    ' ''                                    oDB.Disconnect()

    ' ''                                    C1CV_StressTest.Rows.Add()
    ' ''                                    _Row = C1CV_StressTest.Rows.Count - 1
    ' ''                                    With C1CV_StressTest.Rows(_Row)
    ' ''                                        .AllowEditing = False
    ' ''                                        .ImageAndText = True
    ' ''                                        .Height = 24
    ' ''                                        .IsNode = True
    ' ''                                        .Node.Level = 3
    ' ''                                        .Node.Data = "TestType" '' 
    ' ''                                        .Node.Image = ImgPatientTab.Images(5) 'Global.gloEMR.My.Resources.Resources.p
    ' ''                                    End With
    ' ''                                    '' To Identify for which CPT we are addding the Test Type

    ' ''                                    With dtTests
    ' ''                                        If IsNothing(dtTests) = False Then
    ' ''                                            For nUser = 0 To dtTests.Rows.Count - 1
    ' ''                                                Dim strTest As String = dtTests.Rows(nUser)("TestType")
    ' ''                                                C1CV_StressTest.Rows.Add()
    ' ''                                                _Row = C1CV_StressTest.Rows.Count - 1
    ' ''                                                'set the properties for newly added row
    ' ''                                                With C1CV_StressTest.Rows(_Row)
    ' ''                                                    .AllowEditing = True
    ' ''                                                    .ImageAndText = True
    ' ''                                                    .Height = 24
    ' ''                                                    .IsNode = True
    ' ''                                                    .Node.Level = 4
    ' ''                                                    .Node.Data = strTest
    ' ''                                                    .Node.Image = ImgPatientTab.Images(7) 'Global.gloEMR.My.Resources.Resources.Modify1
    ' ''                                                End With

    ' ''                                                With C1CV_StressTest
    ' ''                                                    .SetData(_Row, COL_PatientID_ST, PatientID)
    ' ''                                                    .SetData(_Row, COL_ExamID_ST, ExamID)
    ' ''                                                    .SetData(_Row, COL_VisitID_ST, VisitID)
    ' ''                                                    .SetData(_Row, COL_ClinicID_ST, ClinicID)
    ' ''                                                    .SetData(_Row, COL_DateofStudyInvisible, DateofStudy)
    ' ''                                                    .SetData(_Row, COL_TestType, dtTests.Rows(nUser)("TestType"))
    ' ''                                                    .SetData(_Row, COL_Result, dtTests.Rows(nUser)("Result"))
    ' ''                                                End With
    ' ''                                            Next
    ' ''                                        End If
    ' ''                                    End With
    ' ''                                End If
    ' ''                            Next '' For nCPT = 0 To .Rows.Count - 1
    ' ''                        End If
    ' ''                    End With '' With dtCPT
    ' ''                    'Query for selecting Modifier for current exam 
    ' ''                    strselectMODQry = "SELECT Distinct isnull(sUserName,'') as UserName from CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "'"

    ' ''                    oDB.Connect(GetConnectionString)
    ' ''                    dtUsers = oDB.ReadQueryDataTable(strselectMODQry)
    ' ''                    oDB.Disconnect()

    ' ''                    With dtUsers

    ' ''                        If IsNothing(dtUsers) = False Then
    ' ''                            If dtUsers.Rows.Count > 0 Then

    ' ''                                Dim strUsers As String = dtUsers.Rows(0)("UserName")

    ' ''                                Dim arrUsers() As String = Split(strUsers, "|")
    ' ''                                If arrUsers.Length > 0 Then


    ' ''                                    C1CV_StressTest.Rows.Add()
    ' ''                                    _Row = C1CV_StressTest.Rows.Count - 1
    ' ''                                    With C1CV_StressTest.Rows(_Row)
    ' ''                                        .AllowEditing = False
    ' ''                                        .ImageAndText = True
    ' ''                                        .Height = 24
    ' ''                                        .IsNode = True
    ' ''                                        .Node.Level = 1
    ' ''                                        .Node.Data = "Users" '' "CPT"
    ' ''                                        .Node.Image = ImgPatientTab.Images(16) 'Global.gloEMR.My.Resources.Resources.p
    ' ''                                    End With



    ' ''                                    For nUser = 0 To arrUsers.Length - 1



    ' ''                                        C1CV_StressTest.Rows.Add()
    ' ''                                        _Row = C1CV_StressTest.Rows.Count - 1
    ' ''                                        'set the properties for newly added row
    ' ''                                        With C1CV_StressTest.Rows(_Row)
    ' ''                                            .AllowEditing = False
    ' ''                                            .ImageAndText = True
    ' ''                                            .Height = 24
    ' ''                                            .IsNode = True
    ' ''                                            .Node.Level = 2
    ' ''                                            .Node.Data = arrUsers(nUser)
    ' ''                                            .Node.Image = ImgPatientTab.Images(7) 'Global.gloEMR.My.Resources.Resources.Modify1
    ' ''                                        End With
    ' ''                                        'Dim concatMOD As String = strconcatCPT1 + "|" + "MOD"

    ' ''                                        With C1CV_StressTest
    ' ''                                            '.SetData(_Row, COL_StressID, StressID)
    ' ''                                            .SetData(_Row, COL_PatientID_ST, PatientID)
    ' ''                                            .SetData(_Row, COL_ExamID_ST, ExamID)
    ' ''                                            .SetData(_Row, COL_VisitID_ST, VisitID)
    ' ''                                            .SetData(_Row, COL_ClinicID_ST, ClinicID)
    ' ''                                            .SetData(_Row, COL_DateofStudyInvisible, DateofStudy)
    ' ''                                        End With



    ' ''                                    Next

    ' ''                                End If
    ' ''                            End If
    ' ''                        End If
    ' ''                    End With '' With dtUsers
    ' ''                End If  '' If CStr(dtStudyDate.Rows(nDOS)("sICD9Code")).Trim <> "" Then
    ' ''            Next ''For nDOS = 0 To .Rows.Count - 1
    ' ''        End If  '' If IsNothing(dtStudyDate) = False Then
    ' ''    End With '' With dtStudyDate


    ' ''    dtStudyDate = Nothing
    ' ''    dtCPT = Nothing
    ' ''    dtUsers = Nothing

    ' ''Catch ex As Exception
    ' ''    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SysnopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    ' ''    MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' ''End Try


    ''''''''''''''''''''' by Ujwala - for New Stress Test moudle Changes as on 20101020
    ' ''End Sub

    Public Sub FillStressTest()
        ''''''''''''''''''''' by Ujwala - for New Stress Test moudle Changes as on 20101020
        Dim dtCPT As DataTable = Nothing
        Dim dtTestType As DataTable = Nothing
        Dim dtUsers As DataTable = Nothing
        Dim dtRestingHeartRate As DataTable = Nothing
        Dim dtPeakHeartRate As DataTable = Nothing
        Dim dtNarrativeSummary As DataTable = Nothing
        'Dim dtResult As DataTable
        Dim dtTotalExerciseTime As DataTable = Nothing
        Dim dtEjectionFraction As DataTable = Nothing
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try
            If IsNothing(dtStudyDateDtls) Then
                Dim _Row As Integer
                SetGridStyle()
                ''set properties of treeview in flexgrid
                With C1CV_StressTest
                    .Tree.Column = COL_DateofStudy
                    .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                    .Tree.LineStyle = Drawing2D.DashStyle.Solid
                    .Tree.Indent = 15
                    .Cols(COL_DateofStudy).TextAlign = TextAlignEnum.LeftCenter
                End With

                ''Dim dtStudyDate As DataTable


                Dim mDOS As Int16
                Dim mCPT As Int16
                Dim mTestType As Int16
                Dim mPhysician As Int16
                Dim mRestingHeartRate As Int16
                Dim mPeakHeartRate As Int16
                Dim mNarrativeSummary As Int16
                'Dim mResult As Int16
                Dim mTotalExerciseTime As Int16
                Dim mEjectionFraction As Int16

                Dim strdtQry As String
                Dim strCptQry As String
                Dim strCPTTestTypeQry As String
                Dim strPhysicianQry As String
                Dim strRestingHeartRateQry As String
                Dim strPeakHeartRateQry As String
                Dim strNarrativeSummaryQry As String
                'Dim strResult As String
                Dim strTotalExerciseTimeQry As String
                Dim strEjectionFractionQry As String
                Dim strconcatCPT1 As String = ""
                Dim nextRow As Integer
                Dim strCombine As String = ""


                'Dim nCPTCode As myTreeNode
                'Dim nCPTTestType As myTreeNode
                'Dim nPhysician As myTreeNode
                'Dim nRestingHeartRate As myTreeNode
                'Dim nPeakHeartRate As myTreeNode
                'Dim nNarrativeSummary As myTreeNode
                'Dim nResult As myTreeNode
                'Dim nTotalExerciseTime As myTreeNode
                'Dim nEjectionFraction As myTreeNode


                strdtQry = "SELECT Distinct isnull(nStressID,0) as nStressID,isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0) as nClinicID,dtDateOfStudy as DateofStudy FROM CV_StressTest WHERE nGroupID=0 AND nPatientID='" & mPatientID & "' order by DateofStudy"

                oDB.Connect(GetConnectionString)
                dtStudyDateDtls = oDB.ReadQueryDataTable(strdtQry)
                oDB.Disconnect()

                With dtStudyDateDtls
                    If IsNothing(dtStudyDateDtls) = False Then
                        For mDOS = 0 To dtStudyDateDtls.Rows.Count - 1
                            Dim StressID As Int64 = 0
                            Dim PatientID As Int64 = 0
                            Dim VisitID As Int64 = 0
                            Dim ExamID As Int64 = 0
                            Dim ClinicID As Int64 = 0
                            Dim DateofStudy As Date
                            Dim TestType As String = ""

                            Dim Result As String = ""

                            Dim count As Integer = mDOS + 1
                            If CStr(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).Trim <> "" Then

                                C1CV_StressTest.Rows.Add()
                                _Row = C1CV_StressTest.Rows.Count - 1
                                ''set the properties for newly added row
                                With C1CV_StressTest.Rows(_Row)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    .Node.Data = Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString
                                    .Node.Image = ImgPatientTab.Images(13)
                                End With
                                nextRow = _Row
                                With C1CV_StressTest
                                    ''.SetData(_Row, COL_DateofStudy, _Row)
                                    .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                    .SetData(_Row, COL_ExamID, dtStudyDateDtls.Rows(mDOS)("nExamID"))
                                    .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                    .SetData(_Row, COL_ClinicID, dtStudyDateDtls.Rows(mDOS)("nClinicID"))
                                    .SetData(_Row, COL_StressID, dtStudyDateDtls.Rows(mDOS)("nStressID"))
                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                    .SetData(_Row, COL_TestType, TestType)
                                    .SetData(_Row, COL_Result, Result)

                                    PatientID = dtStudyDateDtls.Rows(mDOS)("nPatientID")
                                    VisitID = dtStudyDateDtls.Rows(mDOS)("nVisitID")
                                    ExamID = dtStudyDateDtls.Rows(mDOS)("nExamID")
                                    ClinicID = dtStudyDateDtls.Rows(mDOS)("nClinicID")
                                    StressID = dtStudyDateDtls.Rows(mDOS)("nStressID")
                                    DateofStudy = Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString
                                End With


                                Dim dtDateofStudy As Date = Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString()


                                '' Query for selecting CPT for current exam ''
                                strCptQry = "SELECT DISTINCT isnull(sCPT,'') as sCPT from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nGroupID='" & StressID & "' AND sCPT<>''"
                                oDB.Connect(GetConnectionString)
                                dtCPT = oDB.ReadQueryDataTable(strCptQry)
                                oDB.Disconnect()


                                With dtCPT
                                    If IsNothing(dtCPT) = False Then
                                        If dtCPT.Rows.Count >= 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "CPT Code"
                                                .Node.Image = ImgPatientTab.Images(14)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                        End If
                                        For mCPT = 0 To dtCPT.Rows.Count - 1
                                            Dim strCurrentCPT As String = dtCPT.Rows(mCPT)("sCPT")
                                            If strCurrentCPT.Trim <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                ''set the properties for newly added row
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = dtCPT.Rows(mCPT)("sCPT")
                                                    .Node.Image = ImgPatientTab.Images(7)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With


                                                '' Query for selecting CPTCoded Test Type ''
                                                strCPTTestTypeQry = "SELECT DISTINCT isnull(sTestType,'') as sTestType,isnull(sResult,'') as Result from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nGroupID='" & StressID & "' AND sCPT='" & strCurrentCPT & "'  and sTestType<>''"
                                                oDB.Connect(GetConnectionString)
                                                dtTestType = oDB.ReadQueryDataTable(strCPTTestTypeQry)
                                                oDB.Disconnect()

                                                If dtTestType.Rows.Count > 0 Then
                                                    C1CV_StressTest.Rows.Add()
                                                    _Row = C1CV_StressTest.Rows.Count - 1
                                                    With C1CV_StressTest.Rows(_Row)
                                                        .AllowEditing = False
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 3
                                                        .Node.Data = "Test Type"
                                                        .Node.Image = ImgPatientTab.Images(19)
                                                    End With
                                                    With C1CV_StressTest
                                                        .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                        .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                    End With
                                                    '' To Identify for which CPT we are addding the Test Type
                                                    With dtTestType
                                                        If IsNothing(dtTestType) = False Then
                                                            Dim strTest As String = ""
                                                            For mTestType = 0 To dtTestType.Rows.Count - 1
                                                                If dtTestType.Rows(mTestType)("Result").ToString.Trim() <> "" Then
                                                                    strTest = dtTestType.Rows(mTestType)("sTestType") & " - " & dtTestType.Rows(mTestType)("Result")
                                                                Else
                                                                    strTest = dtTestType.Rows(mTestType)("sTestType")
                                                                End If

                                                                C1CV_StressTest.Rows.Add()
                                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                                ''set the properties for newly added row
                                                                With C1CV_StressTest.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = strTest
                                                                    .Node.Image = ImgPatientTab.Images(7)
                                                                End With
                                                                With C1CV_StressTest
                                                                    .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                                End With
                                                            Next
                                                        End If   ''IsNothing(dtTestType) = False Then
                                                    End With
                                                End If   ''dtTestType.Rows.Count > 0 Then
                                                If (IsNothing(dtTestType) = False) Then
                                                    dtTestType.Dispose()
                                                    dtTestType = Nothing
                                                End If
                                            End If
                                        Next   ''mCPT = 0 To dtCPT.Rows.Count - 1
                                        If (IsNothing(dtCPT) = False) Then
                                            dtCPT.Dispose()
                                            dtCPT = Nothing
                                        End If
                                    End If
                                End With  '' With dtCPT




                                '' Query for selecting Physician for current exam  ''
                                strPhysicianQry = "SELECT Distinct isnull(sUserName,'') as UserName from CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                                oDB.Connect(GetConnectionString)
                                dtUsers = oDB.ReadQueryDataTable(strPhysicianQry)
                                oDB.Disconnect()

                                With dtUsers
                                    If IsNothing(dtUsers) = False Then
                                        If dtUsers.Rows.Count > 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Physician Name"
                                                .Node.Image = ImgPatientTab.Images(20)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                            For mPhysician = 0 To dtUsers.Rows.Count - 1
                                                Dim strUsers As String = dtUsers.Rows(mPhysician)("UserName")
                                                If strUsers <> "" Then
                                                    C1CV_StressTest.Rows.Add()
                                                    _Row = C1CV_StressTest.Rows.Count - 1
                                                    ''set the properties for newly added row
                                                    With C1CV_StressTest.Rows(_Row)
                                                        .AllowEditing = False
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 2
                                                        .Node.Data = dtUsers.Rows(mPhysician)("UserName")
                                                        .Node.Image = ImgPatientTab.Images(7)
                                                    End With
                                                    With C1CV_StressTest
                                                        .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                        .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                    End With
                                                End If
                                            Next  ''mPhysician = 0 To dtUsers.Rows.Count - 1
                                            If (IsNothing(dtUsers) = False) Then
                                                dtUsers.Dispose()
                                                dtUsers = Nothing
                                            End If
                                        End If
                                    End If
                                End With '' With dtUsers




                                '' Query for selecting RestingHeartRate for current exam  ''
                                strRestingHeartRateQry = "SELECT Distinct isnull(nRestingHeartRate,0) as nRestingHeartRate,isnull(nRestingBPMin,0) as nRestingBPMin,isnull(nRestingBPMax,0) as nRestingBPMax from CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                                oDB.Connect(GetConnectionString)
                                dtRestingHeartRate = oDB.ReadQueryDataTable(strRestingHeartRateQry)
                                oDB.Disconnect()

                                With dtRestingHeartRate
                                    If IsNothing(dtRestingHeartRate) = False Then
                                        If dtRestingHeartRate.Rows.Count > 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Resting Heart Rate"
                                                .Node.Image = ImgPatientTab.Images(23)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                            For mRestingHeartRate = 0 To dtRestingHeartRate.Rows.Count - 1
                                                strCombine = ""
                                                If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString()) Then
                                                    If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString() <> "") Then
                                                        If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate").ToString() <> "0" Then
                                                            If (strCombine = "") Then
                                                                strCombine = "Heart Rate" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingHeartRate"))
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                                If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString()) Then
                                                    If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString() <> "") Then
                                                        If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString() <> "0" Then
                                                            If (strCombine = "") Then
                                                                strCombine = "BP Min" + " " + ":" + " " + dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin").ToString()
                                                            Else
                                                                strCombine = strCombine + "    " + "BP Min" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMin"))
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                                If Not IsNothing(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString()) Then
                                                    If (dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString() <> "") Then
                                                        If dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString() <> "0" Then
                                                            If (strCombine = "") Then
                                                                strCombine = "BP Max" + " " + ":" + " " + dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax").ToString()
                                                            Else
                                                                strCombine = strCombine + "    " + "BP Max" + " " + ":" + " " + Convert.ToString(dtRestingHeartRate.Rows(mRestingHeartRate)("nRestingBPMax"))
                                                            End If
                                                        End If
                                                    End If
                                                End If

                                                If strCombine.Trim <> "" Then
                                                    C1CV_StressTest.Rows.Add()
                                                    _Row = C1CV_StressTest.Rows.Count - 1
                                                    ''set the properties for newly added row
                                                    With C1CV_StressTest.Rows(_Row)
                                                        .AllowEditing = False
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 2
                                                        .Node.Data = strCombine
                                                        .Node.Image = ImgPatientTab.Images(7)
                                                    End With
                                                    With C1CV_StressTest
                                                        .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                        .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                    End With
                                                End If
                                            Next
                                            If (IsNothing(dtRestingHeartRate) = False) Then
                                                dtRestingHeartRate.Dispose()
                                                dtRestingHeartRate = Nothing
                                            End If
                                        End If
                                    End If
                                End With



                                '' Query for selecting PeakHeartRate for current exam '' 
                                strPeakHeartRateQry = "SELECT Distinct isnull(nPeakHeartRate,0) as nPeakHeartRate,isnull(nPeakBPMin,0) as nPeakBPMin,isnull(nPeakBPMax,0) as nPeakBPMax from CV_StressTest WHERE  nPatientID = " & mPatientID & " and dtDateOfStudy = '" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                                oDB.Connect(GetConnectionString)
                                dtPeakHeartRate = oDB.ReadQueryDataTable(strPeakHeartRateQry)
                                oDB.Disconnect()


                                With dtPeakHeartRate
                                    If IsNothing(dtPeakHeartRate) = False Then
                                        If dtPeakHeartRate.Rows.Count > 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Peak Heart Rate"
                                                .Node.Image = ImgPatientTab.Images(24)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                            For mPeakHeartRate = 0 To dtPeakHeartRate.Rows.Count - 1
                                                strCombine = ""
                                                If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString()) Then
                                                    If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString() <> "") Then
                                                        If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString() <> "0" Then
                                                            If (strCombine = "") Then
                                                                strCombine = "Heart Rate" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakHeartRate").ToString()
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                                If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()) Then
                                                    If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString() <> "") Then
                                                        If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString() <> "0" Then
                                                            If (strCombine = "") Then
                                                                strCombine = "BP Min" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()
                                                            Else
                                                                strCombine = strCombine + "    " + "BP Min" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMin").ToString()
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                                If Not IsNothing(dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()) Then
                                                    If (dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString() <> "") Then
                                                        If dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString() <> "0" Then
                                                            If (strCombine = "") Then
                                                                strCombine = "BP Max" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()
                                                            Else
                                                                strCombine = strCombine + "    " + "BP Max" + " " + ":" + " " + dtPeakHeartRate.Rows(mPeakHeartRate)("nPeakBPMax").ToString()
                                                            End If
                                                        End If
                                                    End If
                                                End If

                                                If strCombine.Trim <> "" Then
                                                    C1CV_StressTest.Rows.Add()
                                                    _Row = C1CV_StressTest.Rows.Count - 1
                                                    ''set the properties for newly added row
                                                    With C1CV_StressTest.Rows(_Row)
                                                        .AllowEditing = False
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 2
                                                        .Node.Data = strCombine
                                                        .Node.Image = ImgPatientTab.Images(7)
                                                    End With
                                                    With C1CV_StressTest
                                                        .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                        .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                    End With
                                                End If

                                            Next
                                            If (IsNothing(dtPeakHeartRate) = False) Then
                                                dtPeakHeartRate.Dispose()
                                                dtPeakHeartRate = Nothing
                                            End If
                                        End If
                                    End If
                                End With


                                '' Query for selecting Narrative Summary ''
                                strNarrativeSummaryQry = "SELECT DISTINCT isnull(sNarrativeSummary,'') as NarrativeSummary from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                                oDB.Connect(GetConnectionString)
                                dtNarrativeSummary = oDB.ReadQueryDataTable(strNarrativeSummaryQry)
                                oDB.Disconnect()


                                With dtNarrativeSummary
                                    If IsNothing(dtNarrativeSummary) = False Then
                                        If dtNarrativeSummary.Rows.Count > 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Narrative Summary"
                                                .Node.Image = ImgPatientTab.Images(22)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                        End If
                                        For mNarrativeSummary = 0 To dtNarrativeSummary.Rows.Count - 1
                                            Dim strNarrativesummary As String = dtNarrativeSummary.Rows(mNarrativeSummary)("NarrativeSummary").ToString()
                                            If strNarrativesummary.Trim <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    ''.DataType = Type.GetType("System.String")
                                                    .TextAlign = TextAlignEnum.LeftCenter
                                                    .Node.Data = strNarrativesummary
                                                    .Node.Image = ImgPatientTab.Images(7)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With
                                            End If
                                        Next  'For mNarrativesummary  = 0 To .Rows.Count - 1
                                        If (IsNothing(dtNarrativeSummary) = False) Then
                                            dtNarrativeSummary.Dispose()
                                            dtNarrativeSummary = Nothing
                                        End If
                                    End If  'dtNarrativeSummary
                                End With



                                ' '' '' '' Query for selecting Result ''
                                ' '' ''strResult = "SELECT DISTINCT isnull(sResult,'') as Result from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                                ' '' ''oDB.Connect(GetConnectionString)
                                ' '' ''dtResult = oDB.ReadQueryDataTable(strResult)
                                ' '' ''oDB.Disconnect()


                                ' '' ''With dtResult
                                ' '' ''    If IsNothing(dtResult) = False Then
                                ' '' ''        If dtResult.Rows.Count > 0 Then
                                ' '' ''            C1CV_StressTest.Rows.Add()
                                ' '' ''            _Row = C1CV_StressTest.Rows.Count - 1
                                ' '' ''            With C1CV_StressTest.Rows(_Row)
                                ' '' ''                .AllowEditing = False
                                ' '' ''                .ImageAndText = True
                                ' '' ''                .Height = 24
                                ' '' ''                .IsNode = True
                                ' '' ''                .Node.Level = 1
                                ' '' ''                .Node.Data = "Result"
                                ' '' ''                .Node.Image = ImgPatientTab.Images(25)
                                ' '' ''            End With
                                ' '' ''            With C1CV_StressTest
                                ' '' ''                .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                ' '' ''                .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                ' '' ''                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                ' '' ''            End With
                                ' '' ''        End If
                                ' '' ''        For mResult = 0 To dtResult.Rows.Count - 1
                                ' '' ''            Dim strResults As String = dtResult.Rows(mResult)("Result")
                                ' '' ''            If strResults.Trim <> "" Then
                                ' '' ''                C1CV_StressTest.Rows.Add()
                                ' '' ''                _Row = C1CV_StressTest.Rows.Count - 1
                                ' '' ''                With C1CV_StressTest.Rows(_Row)
                                ' '' ''                    .AllowEditing = True
                                ' '' ''                    .ImageAndText = True
                                ' '' ''                    .Height = 24
                                ' '' ''                    .IsNode = True
                                ' '' ''                    .Node.Level = 2
                                ' '' ''                    .TextAlign = TextAlignEnum.LeftCenter
                                ' '' ''                    .Node.Data = dtResult.Rows(mResult)("Result")
                                ' '' ''                    .Node.Image = ImgPatientTab.Images(7)
                                ' '' ''                End With
                                ' '' ''                With C1CV_StressTest
                                ' '' ''                    .SetData(_Row, COL_PatientID, dtStudyDate.Rows(mDOS)("nPatientID"))
                                ' '' ''                    .SetData(_Row, COL_VisitID, dtStudyDate.Rows(mDOS)("nVisitID"))
                                ' '' ''                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDate.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                ' '' ''                End With
                                ' '' ''            End If
                                ' '' ''        Next
                                ' '' ''    End If
                                ' '' ''End With



                                '' Query for selecting Total Exercise Time ''
                                strTotalExerciseTimeQry = "SELECT DISTINCT isnull(sTotExerciseTime,'') as TotalExerciseTime from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                                oDB.Connect(GetConnectionString)
                                dtTotalExerciseTime = oDB.ReadQueryDataTable(strTotalExerciseTimeQry)
                                oDB.Disconnect()


                                With dtTotalExerciseTime
                                    If IsNothing(dtTotalExerciseTime) = False Then
                                        If dtTotalExerciseTime.Rows.Count > 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Total Exercise Time"
                                                .Node.Image = ImgPatientTab.Images(18)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                        End If
                                        For mTotalExerciseTime = 0 To dtTotalExerciseTime.Rows.Count - 1
                                            Dim strTotExerciseTime As String = dtTotalExerciseTime.Rows(mTotalExerciseTime)("TotalExerciseTime")
                                            If strTotExerciseTime.Trim <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .TextAlign = TextAlignEnum.LeftCenter
                                                    ''.DataType = Type.GetType("System.String")
                                                    .Node.Data = dtTotalExerciseTime.Rows(mTotalExerciseTime)("TotalExerciseTime")
                                                    .Node.Image = ImgPatientTab.Images(7)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With
                                            End If
                                        Next
                                        If (IsNothing(dtTotalExerciseTime) = False) Then
                                            dtTotalExerciseTime.Dispose()
                                            dtTotalExerciseTime = Nothing
                                        End If
                                    End If
                                End With



                                '' Query for selecting Ejection Fraction ''
                                strEjectionFractionQry = "SELECT DISTINCT isnull(sEjectionFraction,'') as EjectionFraction from CV_StressTest where nPatientID=" & mPatientID & " AND dtDateofStudy='" & dtDateofStudy & "' AND nStressID='" & StressID & "'"
                                oDB.Connect(GetConnectionString)
                                dtEjectionFraction = oDB.ReadQueryDataTable(strEjectionFractionQry)
                                oDB.Disconnect()


                                With dtEjectionFraction
                                    If IsNothing(dtEjectionFraction) = False Then
                                        If dtEjectionFraction.Rows.Count > 0 Then
                                            C1CV_StressTest.Rows.Add()
                                            _Row = C1CV_StressTest.Rows.Count - 1
                                            With C1CV_StressTest.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Ejection Fraction"
                                                .Node.Image = ImgPatientTab.Images(17)
                                            End With
                                            With C1CV_StressTest
                                                .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                            End With
                                        End If
                                        For mEjectionFraction = 0 To dtEjectionFraction.Rows.Count - 1
                                            Dim strTotExerciseTime As String = dtEjectionFraction.Rows(mEjectionFraction)("EjectionFraction")
                                            If strTotExerciseTime.Trim <> "" Then
                                                C1CV_StressTest.Rows.Add()
                                                _Row = C1CV_StressTest.Rows.Count - 1
                                                With C1CV_StressTest.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .TextAlign = TextAlignEnum.LeftCenter
                                                    .Node.Data = dtEjectionFraction.Rows(mEjectionFraction)("EjectionFraction")
                                                    .Node.Image = ImgPatientTab.Images(7)
                                                End With
                                                With C1CV_StressTest
                                                    .SetData(_Row, COL_PatientID, dtStudyDateDtls.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VisitID, dtStudyDateDtls.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtStudyDateDtls.Rows(mDOS)("DateofStudy")).ToShortDateString)
                                                End With
                                            End If
                                        Next
                                        If (IsNothing(dtEjectionFraction) = False) Then
                                            dtEjectionFraction.Dispose()
                                            dtEjectionFraction = Nothing
                                        End If
                                    End If
                                End With


                            End If     '' CStr(dtStudyDate.Rows(mDOS)("DateofStudy")).Trim <> "" Then
                        Next   ''For mDOS = 0 To dtStudyDate.Rows.Count - 1
                    End If   ''If IsNothing(dtStudyDate) = False Then
                End With   '' With dtStudyDate



                'dtStudyDateDtls = Nothing
                '     dtCPT = Nothing
                '    dtUsers = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
           

            If (IsNothing(dtCPT) = False) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If

            If (IsNothing(dtTestType) = False) Then
                dtTestType.Dispose()
                dtTestType = Nothing
            End If

            If (IsNothing(dtUsers) = False) Then
                dtUsers.Dispose()
                dtUsers = Nothing
            End If

            If (IsNothing(dtRestingHeartRate) = False) Then
                dtRestingHeartRate.Dispose()
                dtRestingHeartRate = Nothing
            End If
            If (IsNothing(dtPeakHeartRate) = False) Then
                dtPeakHeartRate.Dispose()
                dtPeakHeartRate = Nothing
            End If
            If (IsNothing(dtNarrativeSummary) = False) Then
                dtNarrativeSummary.Dispose()
                dtNarrativeSummary = Nothing
            End If
            If (IsNothing(dtTotalExerciseTime) = False) Then
                dtTotalExerciseTime.Dispose()
                dtTotalExerciseTime = Nothing
            End If
            If (IsNothing(dtEjectionFraction) = False) Then
                dtEjectionFraction.Dispose()
                dtEjectionFraction = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        ''''''''''''''''''''' by Ujwala - for New Stress Test moudle Changes as on 20101020
    End Sub

    Private Sub C1CV_StressTest_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1CV_StressTest.DoubleClick
        Try
            If C1CV_StressTest.Rows.Count > 1 Then


                mPatientID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID) '' COL_PatientID_ST
                mStressID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_StressID)
                mExamID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ExamID) '' COL_ExamID_ST
                mVisitID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID) '' COL_VisitID_ST
                mClinicID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ClinicID) '' COL_ClinicID_ST

                mDateofStudy = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_DateofStudyInvisible)
                'lOAD stress details

                Dim ofrm As New frmCV_StressTests(mPatientID, mVisitID, mDateofStudy)
                ofrm.ShowDialog(ofrm.Parent)
                If Not IsNothing(ofrm) Then
                    ofrm.Dispose()
                    ofrm = Nothing
                End If
                dtStudyDateDtls = Nothing ''For Fetching latest data from database.
                FillImagingST(pID)
                Dim dsdata As DataSet
                dsdata = c2.PopulateSynopsisData(mPatientID, "Imaging")
                PopulateImaging(dsdata.Tables("Imaging"))
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtImagingSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImagingSearch.TextChanged
        Try
            Dim strSearch As String
            With txtImagingSearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            With C1CV_StressTest
                .Row = .FindRow(strSearch, 1, COL_DateofStudy, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                '' 20070921 - Mahesh - InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, COL_DateofStudy).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
                '' ---
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Imaging having substring" & txtImagingSearch.Text.Trim, gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Imaging having substring" & txtImagingSearch.Text.Trim, gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Friend Sub FillCategoryTestGroups()
        Dim oDB As gloStream.gloDataBase.gloDataBase
        'Dim oDataReader As SqlClient.SqlDataReader
        Dim ds As DataSet
        Dim _strSQL As String
        Dim _Categories As New Collection
        Dim _Groups As New Collection
        Dim _Tests As New Collection

        Dim _SubTests As New Collection

        Dim oFindNode As C1.Win.C1FlexGrid.Node
        Dim oTempNode As C1.Win.C1FlexGrid.Node
        Dim _tmpRow As Integer
        Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        With C1OrderDetails
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COLUM_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21
            C1OrderDetails.ShowCellLabels = True


            .Tree.Column = COLUM_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.LineStyle = Drawing2D.DashStyle.Solid
            .Tree.Indent = 15

            '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
            '.Redraw = False

            'Fill Categories
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            'oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '1' AND lm_category_Description IS NOT NULL ")
            _strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
                    & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                    & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & pID & ") " _
                    & " ORDER BY lm_category_Description "
            ds = oDB.ReadCatRecords(_strSQL)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            If IsNothing(ds) = False Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If IsDBNull(ds.Tables(0).Rows(i)("lm_category_Description")) = False Then
                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            ''.Style = FillControl.Styles("CS_Category")
                            .Node.Level = 0
                            .Node.Data = ds.Tables(0).Rows(i)("lm_category_Description")
                            .Node.Key = ds.Tables(0).Rows(i)("lm_test_CategoryID")
                            .Node.Image = ImgPatientTab.Images(6) 'Global.gloEMR.My.Resources.Resources.ICD_09
                        End With

                        .SetData(.Rows.Count - 1, COLUM_IDENTITY, "C" & ds.Tables(0).Rows(i)("lm_test_CategoryID"))
                        .SetData(.Rows.Count - 1, COLUM_NUMVALUE, Nothing)
                        .SetData(.Rows.Count - 1, COLUM_ID, ds.Tables(0).Rows(i)("lm_test_CategoryID"))
                        .SetData(.Rows.Count - 1, COLUM_TESTGROUPFLAG, "C")
                        .SetData(.Rows.Count - 1, COLUM_LEVELNO, 0)
                        .SetData(.Rows.Count - 1, COLUM_GROUPNO, 0)
                        _Categories.Add(ds.Tables(0).Rows(i)("lm_category_Description"))
                    End If
                Next
            End If


            'Fill Groups
            For i As Int16 = 1 To _Categories.Count
                _strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
                        & " FROM LM_Test LEFT OUTER JOIN " _
                        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                        & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
                        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                        & " WHERE (LM_Orders.lm_Patient_ID =" & pID & ") AND  " _
                        & " (LM_Category.lm_category_Description = '" & Replace(_Categories(i), "'", "''") & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
                        & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "

                oDB = New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString)
                ds = oDB.ReadQueryRecordAsDataSet(_strSQL)
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing

                If IsNothing(ds) = False Then
                    For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        'If ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString = 0 Then
                        oFindNode = GetC1UniqueNode("C" & ds.Tables(0).Rows(j)("lm_test_CategoryID").ToString, C1OrderDetails)
                        If Not oFindNode Is Nothing Then

                            '' Check For Duplicate Nodes Under the same Group
                            oTempNode = GetC1UniqueNode(ds.Tables(0).Rows(j)("lm_test_TestGroupFlag").ToString & ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString, C1OrderDetails)
                            If IsNothing(oTempNode) = True Then
                                '' If Group Node is Not Exist then Add the Group Node
                                oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, ds.Tables(0).Rows(j)("GroupName"), Nothing, ImgPatientTab.Images(7))
                                '//.Style = FillControl.Styles("CS_Category")
                                _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If Not _tmpRow = -1 Then
                                    .Rows(_tmpRow).AllowEditing = False
                                    .Rows(_tmpRow).ImageAndText = True
                                    .Rows(_tmpRow).Height = 24
                                    .SetData(_tmpRow, COLUM_IDENTITY, ds.Tables(0).Rows(j)("lm_test_TestGroupFlag").ToString & ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString)
                                    .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                    .SetData(_tmpRow, COLUM_ID, ds.Tables(0).Rows(j)("lm_test_GroupNo"))
                                    .SetData(_tmpRow, COLUM_TESTGROUPFLAG, ds.Tables(0).Rows(j)("lm_test_TestGroupFlag"))
                                    '.SetData(_tmpRow, COLUM_LEVELNO, ds.Tables(0).Rows(j)("lm_test_LevelNo"))
                                    '.SetData(_tmpRow, COLUM_GROUPNO, ds.Tables(0).Rows(j)("lm_test_GroupNo"))

                                    _Groups.Add(ds.Tables(0).Rows(j)("lm_test_GroupNo"))

                                    If ds.Tables(0).Rows(j)("lm_test_TestGroupFlag") = "T" Then
                                        ''  IF is Test then 
                                        .Rows(_tmpRow).AllowEditing = True
                                        .Cols(COLUM_NAME).AllowEditing = False
                                        ''  SET TemplateID
                                        .SetData(_tmpRow, COLUM_TEMPLATEID, ds.Tables(0).Rows(j)("lm_test_Template_ID"))
                                        '' For the Numeric Value
                                        C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                                        ''  Insert CheckBox
                                        '.SetCellCheck(_tmpRow, COLUM_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                        .SetCellImage(_tmpRow, COLUM_BUTTON, ImgPatientTab.Images(4))
                                        ''  SET ShowComment Button
                                        .SetData(_tmpRow, COLUM_BUTTON, "")
                                        Try
                                            If (.Styles.Contains("BubbleValues")) Then
                                                cStyle = .Styles("BubbleValues")
                                            Else
                                                cStyle = .Styles.Add("BubbleValues")
                                            End If
                                        Catch ex As Exception
                                            cStyle = .Styles.Add("BubbleValues")
                                        End Try


                                        cStyle.ComboList = "..."
                                        '.CellButtonImage = imgPatienttab.Images(0)
                                        Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
                                        rgBubbleValues.Style = cStyle


                                        ''''' 20070129 For Fill Diagnosis '
                                        Dim csDia As CellStyle
                                        '' Fill Values In ComboBox

                                        Try
                                            If (.Styles.Contains("Dia")) Then
                                                csDia = .Styles("Dia")
                                            Else
                                                csDia = .Styles.Add("Dia")
                                            End If
                                        Catch ex As Exception
                                            csDia = .Styles.Add("Dia")
                                        End Try

                                        csDia.ComboList = strDia
                                        '''''
                                        .Cols(COLUM_DIAGNOSIS).Style = csDia

                                        Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
                                        rgDig.Style = cStyle

                                    End If

                                    _tmpRow = -1
                                End If
                            End If
                            '''''



                            '''''' Add Test 
                            Dim dsTest As DataSet
                            _strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
                                        & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, LM_Orders.lm_Status, " _
                                        & " LM_Test.lm_test_Template_ID , LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description " _
                                    & " FROM  LM_Test INNER JOIN " _
                                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
                                    & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                                    & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & pID & ") AND  " _
                                    & " (LM_Category.lm_category_Description = '" & Replace(_Categories(i), "'", "''") & "') AND (LM_Test.lm_test_GroupNo =" & ds.Tables(0).Rows(j)("lm_test_GroupNo") & ") " _
                                    & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

                            oDB = New gloStream.gloDataBase.gloDataBase
                            oDB.Connect(GetConnectionString)
                            dsTest = oDB.ReadQueryRecordAsDataSet(_strSQL)
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing

                            If IsNothing(dsTest) = False Then
                                For l As Integer = 0 To dsTest.Tables(0).Rows.Count - 1
                                    'If ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString = 0 Then
                                    oFindNode = GetC1UniqueNode("G" & ds.Tables(0).Rows(j)("lm_test_GroupNo"), C1OrderDetails)
                                    If Not oFindNode Is Nothing Then
                                        '' Check For Duplicate Nodes Under the same Group
                                        oTempNode = GetC1UniqueNode(dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag").ToString & dsTest.Tables(0).Rows(l)("lm_test_ID").ToString, C1OrderDetails)
                                        If IsNothing(oTempNode) = False Then
                                            '' If Node is Alredy Exixst then Exit For
                                            Exit For
                                        End If
                                        '''''

                                        oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dsTest.Tables(0).Rows(l)("lm_test_Name"), Nothing, ImgPatientTab.Images(9))
                                        '//.Style = FillControl.Styles("CS_Category")
                                        _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        If Not _tmpRow = -1 Then
                                            .Rows(_tmpRow).AllowEditing = False
                                            .Rows(_tmpRow).ImageAndText = True
                                            .Rows(_tmpRow).Height = 24
                                            .SetData(_tmpRow, COLUM_IDENTITY, dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag").ToString & dsTest.Tables(0).Rows(l)("lm_test_ID").ToString)
                                            .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                            .SetData(_tmpRow, COLUM_ID, dsTest.Tables(0).Rows(l)("lm_test_ID"))
                                            .SetData(_tmpRow, COLUM_TESTGROUPFLAG, dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag"))
                                            '.SetData(_tmpRow, COLUM_LEVELNO, ds.Tables(0).Rows(j)("lm_test_LevelNo"))
                                            ' .SetData(_tmpRow, COLUM_GROUPNO, ds.Tables(0).Rows(j)("lm_test_GroupNo"))
                                            .SetData(_tmpRow, COLUM_UNIT, dsTest.Tables(0).Rows(l)("lm_test_Dimension"))

                                            .SetData(_tmpRow, COLUM_VISITID, dsTest.Tables(0).Rows(l)("lm_Visit_ID"))
                                            .SetData(_tmpRow, COLUM_ORDERDATE, dsTest.Tables(0).Rows(l)("lm_OrderDate"))

                                            _Tests.Add(dsTest.Tables(0).Rows(l)("lm_test_Name"))

                                            If dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag") = "T" Then
                                                ''  IF is Test then 
                                                .Rows(_tmpRow).AllowEditing = True
                                                .Cols(COLUM_NAME).AllowEditing = False
                                                '' For the Numeric Value
                                                C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                                                ''  SET TemplateID
                                                .SetData(_tmpRow, COLUM_TEMPLATEID, dsTest.Tables(0).Rows(l)("lm_test_Template_ID"))

                                                ''  Insert CheckBox
                                                '.SetCellCheck(_tmpRow, COLUM_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

                                                .SetCellImage(_tmpRow, COLUM_BUTTON, ImgPatientTab.Images(4))

                                                ''  SET ShowComment Button
                                                .SetData(_tmpRow, COLUM_BUTTON, "")

                                                Try
                                                    If (.Styles.Contains("BubbleValues")) Then
                                                        cStyle = .Styles("BubbleValues")
                                                    Else
                                                        cStyle = .Styles.Add("BubbleValues")
                                                    End If
                                                Catch ex As Exception
                                                    cStyle = .Styles.Add("BubbleValues")
                                                End Try

                                                cStyle.ComboList = "..."

                                                Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
                                                rgBubbleValues.Style = cStyle

                                                ''''' 20070129 For Fill Diagnosis
                                                Dim csDia As CellStyle
                                                '' Fill Values In ComboBox

                                                Try
                                                    If (.Styles.Contains("Dia")) Then
                                                        csDia = .Styles("Dia")
                                                    Else
                                                        csDia = .Styles.Add("Dia")
                                                    End If
                                                Catch ex As Exception
                                                    csDia = .Styles.Add("Dia")
                                                End Try
                                                '' Fill Values In ComboBox
                                                csDia.ComboList = strDia
                                                '''''
                                                .Cols(COLUM_DIAGNOSIS).Style = csDia

                                                Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
                                                rgDig.Style = cStyle
                                                '' Set Associated Diagnosis with this Order
                                                .SetData(_tmpRow, COLUM_DIAGNOSIS, dsTest.Tables(0).Rows(l)("lm_sICD9Code") & "-" & dsTest.Tables(0).Rows(l)("lm_sICD9Description"))
                                                ''''''''''''''

                                                If IsDBNull(dsTest.Tables(0).Rows(l)("lm_Result")) = False Then
                                                    If IsNothing(dsTest.Tables(0).Rows(l)("lm_Result")) = False Then
                                                        ''''' If Order comments are entered then Indicate it by ForeColor as RED
                                                        .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Red
                                                    Else
                                                        ''''' If Order comments are NOT entered then Indicate it by ForeColor as GREEN
                                                        .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
                                                    End If
                                                Else
                                                    .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
                                                End If

                                            End If

                                            _tmpRow = -1
                                        End If
                                    End If
                                    ' End If
                                Next
                            End If
                            '' Add Test Close
                        End If
                        ' End If
                    Next
                End If
            Next

            .Cols(COLUM_NAME).AllowEditing = False
            .Cols(COLUM_IDENTITY).AllowEditing = False
            .Cols(COLUM_NUMVALUE).AllowEditing = False
            .Cols(COLUM_ID).AllowEditing = False
            .Cols(COLUM_TESTGROUPFLAG).AllowEditing = False
            .Cols(COLUM_LEVELNO).AllowEditing = False
            .Cols(COLUM_GROUPNO).AllowEditing = False
            .Cols(COLUM_ISFINISHED).AllowEditing = False
            .Cols(COLUM_UNIT).AllowEditing = False
            .Cols(COLUM_DIAGNOSIS).AllowEditing = False
            .Cols(COLUM_DMSID).AllowEditing = False
            .Cols(COLUM_VISITID).AllowEditing = False
            .Cols(COLUM_ORDERDATE).AllowEditing = False

            .SetData(0, COLUM_NAME, "Tests")
            .SetData(0, COLUM_IDENTITY, "Identity")
            .SetData(0, COLUM_NUMVALUE, "Value")
            .SetData(0, COLUM_BUTTON, "Comments")
            .SetData(0, COLUM_ID, "ID")
            .SetData(0, COLUM_TESTGROUPFLAG, "Flag")
            .SetData(0, COLUM_LEVELNO, "Level No")
            .SetData(0, COLUM_GROUPNO, "Group No")
            .SetData(0, COLUM_UNIT, "Unit")
            .SetData(0, COLUM_DIAGNOSIS, "Diagnosis")

            .SetData(0, COLUM_DMSID, "DMS ID")
            .SetData(0, COLUM_VISITID, "Visit ID")
            .SetData(0, COLUM_ORDERDATE, "Order Date")


            .Cols(COLUM_NAME).Width = .Width * 0.98 '((.Width / 5) * 2.5) - 20
            .Cols(COLUM_NUMVALUE).Width = .Width * 0.0  '((.Width / 5) * 0.5)
            .Cols(COLUM_BUTTON).Width = .Width * 0 '((.Width / 5) * 0.2)
            .Cols(COLUM_UNIT).Width = .Width * 0  '((.Width / 5) * 0.2)
            .Cols(COLUM_DIAGNOSIS).Width = .Width * 0
            .Cols(COLUM_DIAGNOSISBUTTON).Width = 0 ''.Width * 0.03
            .Cols(COLUM_VISITID).Width = 0 '.Width * 0.03
            .Cols(COLUM_ORDERDATE).Width = 0 '.Width * 0.03


            .Cols(COLUM_NAME).Visible = True
            .Cols(COLUM_IDENTITY).Visible = False
            .Cols(COLUM_NUMVALUE).Visible = False
            .Cols(COLUM_ID).Visible = False
            .Cols(COLUM_TESTGROUPFLAG).Visible = False
            .Cols(COLUM_LEVELNO).Visible = False
            .Cols(COLUM_GROUPNO).Visible = False
            .Cols(COLUM_TEMPLATEID).Visible = False
            .Cols(COLUM_ISFINISHED).Visible = False
            .Cols(COLUM_DIAGNOSIS).Visible = True
            .Cols(COLUM_DMSID).Visible = False
            .Cols(COLUM_VISITID).Visible = False
            .Cols(COLUM_ORDERDATE).Visible = False
            'c1OrderDetails.getData(0, 2)

            '' For the Numeric Value
            .Cols(COLUM_NUMVALUE).Format = Format("##0.000")
            .Cols(COLUM_NUMVALUE).DataType = System.Type.GetType("System.Decimal")
        End With

    End Sub

    Private Function GetC1UniqueNode(ByVal FindData As String, ByVal _C1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        Dim _FindRow As Integer = _C1FlexGrid.FindRow(FindData, 0, COLUM_IDENTITY, False, True, True)
        If _FindRow > 0 Then
            _Node = _C1FlexGrid.Rows(_FindRow).Node
        End If
        Return _Node
    End Function

    Friend Sub Fill_ProblemList(Optional ByVal dtSearchProblemlist As DataTable = Nothing)
        ''dhruv 20091130
        ''Avoid the flickering
        gloC1FlexStyle.Style(c1ProblemList) ''Ojeswini_01302010

        c1ProblemList.Redraw = False
        ' '' ''Dim dtProblemList As DataTable
        Dim objProblemList As New clsPatientProblemList
        If IsNothing(dtSearchProblemlist) Then
            If IsNothing(dtProblemList) Then
                dtProblemList = objProblemList.Fill_ProblemLists(pID)
                oSearchProblemListCtl.IntialiseDatatable(dtProblemList)
             
            End If
        Else
            dtProblemList = dtSearchProblemlist
        End If
        objProblemList.Dispose()
        objProblemList = Nothing

        'dgPatientDetails.Visible = False



        With c1ProblemList
            .Visible = True
            .BringToFront()
            .Cols.Count = 15 '8
            .Rows.Count = 1
            c1ProblemList.ShowCellLabels = True
            gloC1FlexStyle.Style(c1ProblemList)
            '' Set Fixed Rows
            .SetData(0, 0, "ProblemID")
            .SetData(0, 1, "DOS")
            .SetData(0, 2, "Description")
            .SetData(0, 3, "SnoMed CT ID")
            .SetData(0, 4, "Diagnosis")
            .SetData(0, 5, "Prescription")
            .SetData(0, 6, "VisitID")
            .SetData(0, 7, "Status")
            .SetData(0, 8, "User")

            ''Start :ProblemList: New Columns Added 
            .SetData(0, 9, "Resolved Date")
            .SetData(0, 10, "Immediacy")
            .SetData(0, 11, "Provider")
            .SetData(0, 12, "Location")
            .SetData(0, 13, "Last Update")
            .SetData(0, 14, "Problem Type")

            ''End :ProblemList: New Columns Added

            '''' 
            .Cols(0).Width = 0
            .Cols(1).Width = .Width * 0.1
            .Cols(2).Width = .Width * 0.22
            .Cols(3).Width = Width * 0.1
            .Cols(4).Width = .Width * 0.4
            .Cols(5).Width = .Width * 0.5
            .Cols(6).Width = 0
            .Cols(7).Width = .Width * 0.09
            .Cols(8).Width = .Width * 0.09


            ''Start :ProblemList: New Columns Added 
            .Cols(9).Width = Width * 0.1
            .Cols(10).Width = Width * 0.1
            .Cols(11).Width = Width * 0.1
            .Cols(12).Width = Width * 0.1
            .Cols(13).Width = Width * 0.1
            .Cols(14).Width = Width * 0.1

            ''End :ProblemList: New Columns Added
            '''' 
            .Cols(0).TextAlign = TextAlignEnum.LeftCenter
            .Cols(1).TextAlign = TextAlignEnum.LeftCenter
            .Cols(2).TextAlign = TextAlignEnum.LeftCenter
            .Cols(3).TextAlign = TextAlignEnum.LeftCenter
            .Cols(4).TextAlign = TextAlignEnum.LeftCenter
            .Cols(5).TextAlign = TextAlignEnum.LeftCenter
            .Cols(6).TextAlign = TextAlignEnum.LeftCenter
            .Cols(7).TextAlign = TextAlignEnum.LeftCenter

            ''Start :ProblemList: New Columns Added 
            .Cols(8).TextAlign = TextAlignEnum.LeftCenter
            .Cols(9).TextAlign = TextAlignEnum.LeftCenter
            .Cols(10).TextAlign = TextAlignEnum.LeftCenter
            .Cols(11).TextAlign = TextAlignEnum.LeftCenter
            .Cols(12).TextAlign = TextAlignEnum.LeftCenter
            .Cols(13).TextAlign = TextAlignEnum.LeftCenter
            .Cols(14).TextAlign = TextAlignEnum.LeftCenter
            ''End :ProblemList: New Columns Added
            ' ''

            If IsNothing(dtProblemList) = False Then
                For i As Int16 = 0 To dtProblemList.Rows.Count - 1
                    Dim forecolor As Color
                    'Dim backcolor As Color
                    Dim status As String = ""
                    If dtProblemList.Rows(i)("Status") = frmProblemList.Status.Active Then
                        forecolor = Color.Red
                        ' backcolor = Color.White
                        status = "Active"
                    ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Resolved Then
                        forecolor = Color.Green
                        ' backcolor = Color.White
                        status = "Resolved"
                    ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Inactive Then
                        forecolor = Color.Blue
                        ' backcolor = Color.White
                        status = "Inactive"
                    ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Chronic Then
                        forecolor = Color.Black
                        ' backcolor = Color.White
                        status = "Chronic"
                    End If
                    Dim r As C1.Win.C1FlexGrid.Row
                    r = .Rows.Add()
                    r.StyleNew.ForeColor = forecolor
                    '  r.StyleNew.BackColor = backcolor
                    r.Height = 20

                    .SetData(r.Index, 0, dtProblemList.Rows(i)("nProblemID"))
                    .SetData(r.Index, 1, dtProblemList.Rows(i)("dtDOS"))
                    .SetData(r.Index, 2, dtProblemList.Rows(i)("Complaint"))
                    .SetData(r.Index, 3, dtProblemList.Rows(i)("sConceptID"))
                    .SetData(r.Index, 4, dtProblemList.Rows(i)("Diagnosis"))
                    .SetData(r.Index, 5, dtProblemList.Rows(i)("Prescription"))
                    .SetData(r.Index, 6, dtProblemList.Rows(i)("VisitID"))
                    .SetData(r.Index, 7, status)
                    .SetData(r.Index, 8, dtProblemList.Rows(i)("UserName"))


                    ''Start :ProblemList: New Columns Added 
                    .SetData(r.Index, 9, dtProblemList.Rows(i)("ResolvedDt"))
                    .SetData(r.Index, 10, dtProblemList.Rows(i)("Immediacy"))
                    .SetData(r.Index, 11, dtProblemList.Rows(i)("Provider"))
                    .SetData(r.Index, 12, dtProblemList.Rows(i)("Location"))
                    .SetData(r.Index, 13, dtProblemList.Rows(i)("ModifiedDate"))
                    .SetData(r.Index, 14, dtProblemList.Rows(i)("sTransactionID1"))

                    ''End :ProblemList: New Columns Added

                Next
            End If
            'oSearchProblemListCtl.IntialiseDatatable(dtProblemList)
        End With
        c1ProblemList.Redraw = True
        ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Problem List viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)
    End Sub

    'Private Function FillCPTTreeView() As Boolean
    '    Dim oCPTs As gloCPTs
    '    Dim oCPT As gloCPT
    '    Dim oICD9s As gloICD9s
    '    Dim oICD9 As gloICD9
    '    Dim oMods As gloModifiers
    '    Dim oMod As gloModifier

    '    Try

    '        oCPTs = New gloEMR.gloCPTs
    '        oCPTs = FillICDCPTMOD(pID)


    '        Dim myNode As TreeNode
    '        trProcedureDetails.Nodes.Clear()
    '        For i As Integer = 0 To oCPTs.Count - 1
    '            oCPT = oCPTs.Item(i)

    '            myNode = New TreeNode
    '            myNode.Tag = oCPT.Code
    '            myNode.Text = oCPT.Code & " - " & oCPT.Description & "-" & oCPT.Unit
    '            myNode.ImageIndex = 4
    '            myNode.SelectedImageIndex = 4
    '            trProcedureDetails.Nodes.Add(myNode)

    '            oICD9s = oCPT.ICD9Col

    '            Dim childNode As New TreeNode
    '            If oICD9s.Count > 0 Then
    '                childNode.Text = "Diagnosis"
    '                myNode.ImageIndex = 8
    '                myNode.SelectedImageIndex = 8
    '                myNode.Nodes.Add(childNode)
    '            End If

    '            Dim tempNode As TreeNode

    '            For j As Integer = 0 To oICD9s.Count - 1
    '                oICD9 = New gloEMR.gloICD9

    '                oICD9 = oICD9s.Item(j)

    '                tempNode = New TreeNode
    '                tempNode.Tag = oICD9.Code
    '                tempNode.Text = oICD9.Code & " - " & oICD9.Description
    '                tempNode.ImageIndex = 8
    '                tempNode.SelectedImageIndex = 8
    '                childNode.Nodes.Add(tempNode)
    '            Next

    '            oMods = oCPT.ModfierCol

    '            Dim childNode1 As New TreeNode
    '            If oMods.Count > 0 Then
    '                childNode1.Text = "Modifiers"
    '                childNode1.ImageIndex = 10
    '                childNode1.SelectedImageIndex = 10
    '                myNode.Nodes.Add(childNode1)
    '            End If

    '            For j As Integer = 0 To oMods.Count - 1
    '                oMod = New gloEMR.gloModifier

    '                oMod = oMods.Item(j)

    '                tempNode = New TreeNode
    '                tempNode.Tag = oMod.Code
    '                tempNode.Text = oMod.Code & " - " & oMod.Description
    '                tempNode.ImageIndex = 7
    '                tempNode.SelectedImageIndex = 7
    '                childNode1.Nodes.Add(tempNode)

    '            Next
    '        Next
    '        trProcedureDetails.ExpandAll()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function


    'function for show exam 

    Public Sub FillGridStyle()
        Try
            gloC1FlexStyle.Style(C1Dignosis)  ''Ojeswini_01302010
            C1Dignosis.Clear(ClearFlags.Content)
            With C1Dignosis
                Dim _TotalWidth As Single = .Width - 5
                .Cols.Fixed = 0
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Count = Col_Count
                C1Dignosis.ShowCellLabels = True
                'for Diagnosis
                .Cols(Col_sICD9Code).Width = _TotalWidth * 0
                .SetData(0, Col_sICD9Code, "sICD9Code")
                .Cols(Col_sICD9Code).DataType = GetType(String)
                .Cols(Col_sICD9Code).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_DateOfService).Width = _TotalWidth * 0
                .SetData(0, Col_DateOfService, "Date")
                .Cols(Col_DateOfService).AllowEditing = False
                .Cols(Col_DateOfService).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


                .Cols(Col_ICD9Code_Description).Width = _TotalWidth * 0.8
                .SetData(0, Col_ICD9Code_Description, "Diagnosis")  ''ICD9 replaceby Diagnosis bugid 72796
                .Cols(Col_ICD9Code_Description).AllowEditing = False
                .Cols(Col_ICD9Code_Description).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Code).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Code, "ICDCODE")
                .Cols(Col_ICD9Code).AllowEditing = True
                .Cols(Col_ICD9Code).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ICD9Desc).Width = _TotalWidth * 0
                .SetData(0, Col_ICD9Desc, "ICDDescription")
                .Cols(Col_ICD9Desc).AllowEditing = True
                .Cols(Col_ICD9Desc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(COl_CPTCode).Width = _TotalWidth * 0
                .SetData(0, COl_CPTCode, "CPTCODE")
                .Cols(COl_CPTCode).AllowEditing = True
                .Cols(COl_CPTCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_CPTDesc).Width = _TotalWidth * 0
                .SetData(0, Col_CPTDesc, "CPTDescription")
                .Cols(Col_CPTDesc).AllowEditing = True
                .Cols(Col_CPTDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModCode).Width = _TotalWidth * 0
                .SetData(0, Col_ModCode, "MODCODE")
                .Cols(Col_ModCode).AllowEditing = True
                .Cols(Col_ModCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_ModDesc).Width = _TotalWidth * 0
                .SetData(0, Col_ModDesc, "MODDescription")
                .Cols(Col_ModDesc).AllowEditing = True
                .Cols(Col_ModDesc).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_Units).Width = _TotalWidth * 0.17
                .SetData(0, Col_Units, "Units")
                '.Cols(Col_Units).DataType = GetType(System.Decimal)
                .Cols(Col_Units).Format = "###.####"
                .Cols(Col_Units).DataType = GetType(System.Double)

                .Cols(Col_Units).AllowEditing = True
                .Cols(Col_Units).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_DIAEXAMID).Width = _TotalWidth * 0
                .Cols(Col_DIAVISITID).Visible = True
                .SetData(0, Col_DIAEXAMID, "ExamID")
                .Cols(Col_DIAEXAMID).DataType = GetType(System.Int64)
                .Cols(Col_DIAEXAMID).AllowEditing = True
                .Cols(Col_DIAEXAMID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_DIAVISITID).Width = _TotalWidth * 0
                .Cols(Col_DIAVISITID).Visible = True
                .SetData(0, Col_DIAVISITID, "VisitID")
                .Cols(Col_DIAVISITID).DataType = GetType(System.Int64)
                .Cols(Col_DIAVISITID).AllowEditing = True
                .Cols(Col_DIAVISITID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_HiddenExamName).Width = _TotalWidth * 0
                .Cols(Col_HiddenExamName).Visible = True
                .SetData(0, Col_HiddenExamName, "ExamName")
                .Cols(Col_HiddenExamName).DataType = GetType(String)
                .Cols(Col_HiddenExamName).AllowEditing = True
                .Cols(Col_HiddenExamName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter


                .Cols(Col_HiddenDOS).Width = _TotalWidth * 0
                .SetData(0, Col_HiddenDOS, "HiddenDate")
                .Cols(Col_HiddenDOS).AllowEditing = False
                .Cols(Col_HiddenDOS).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .Cols(Col_HiddenExFinish).Width = _TotalWidth * 0
                .SetData(0, Col_HiddenExFinish, "ExFinish")
                .Cols(Col_HiddenExFinish).DataType = GetType(String)
                .Cols(Col_HiddenExFinish).AllowEditing = False
                .Cols(Col_HiddenExFinish).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Friend Sub FillICDCPTMOD(ByVal patientid As Int64) 'As gloCPTs
        ''dhruv 20091130
        ''Avoid the flickering
        C1Dignosis.Redraw = False
        Dim dtICD9 As DataTable = Nothing
        Dim dtCPT As DataTable = Nothing
        Dim dtMOD As DataTable = Nothing
        Try

            FillGridStyle()

            Dim _Row As Integer
            'Dim i As Integer
            'set properties of treeview in flexgrid
            With C1Dignosis
                .Tree.Column = Col_ICD9Code_Description
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
            End With

            ''Dim dtDOS As DataTable


            Dim DiagNode As myTreeNode
            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nDOS As Int16
            'Dim nDiag As Int16
            Dim nICD9 As Int16
            Dim nCPT As Int16
            Dim nMOD As Int16


            Dim strselectDOSQry As String
            'Dim strselectDiagQry As String

            Dim strselecrICD9Qry As String
            Dim strselectCPTQry As String
            Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextICD As Integer

            'Dim nextDOS As Integer

            If IsNothing(dtProcDetails) Then
                'Query for selecting Diagnosis for current exam 
                strselectDOSQry = "select distinct PatientExams.dtDOS as dtDOS, ExamICD9CPT.nExamID as nExamID, ExamICD9CPT.nVisitID as nVisitID, isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished" _
                                  & " from PatientExams inner join ExamICD9CPT on  PatientExams.nExamID = ExamICD9CPT.nExamID" _
                                  & " where ExamICD9CPT.nPatientID = " & patientid

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString)
                dtProcDetails = oDB.ReadQueryDataTable(strselectDOSQry)
                oDB.Disconnect()

                With dtProcDetails 'pull all the Diagnosis for the respective dates
                    If IsNothing(dtProcDetails) = False Then
                        For nDOS = 0 To .Rows.Count - 1
                            'Dim ElectroPhysiologyID As Int64 = 0
                            Dim VisitID As Int64 = 0
                            Dim ExamID As Int64 = 0
                            Dim DateofServ As Date
                            Dim ICD9Code As String = ""
                            Dim CPTCode As String = ""

                            Dim count1 As Integer = nDOS + 1

                            If CStr(dtProcDetails.Rows(nDOS)("dtDOS")).Trim <> "" Then
                                C1Dignosis.Rows.Add()
                                _Row = C1Dignosis.Rows.Count - 1
                                'set the properties for newly added row
                                With C1Dignosis.Rows(_Row)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 0
                                    .Node.Data = dtProcDetails.Rows(nDOS)("dtDOS") 'dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")
                                    .Node.Image = ImgPatientTab.Images(13) ' Global.gloEMR.My.Resources.Resources.ICD_09
                                End With
                                nextICD = _Row
                                With C1Dignosis
                                    .SetData(_Row, Col_DateOfService, dtProcDetails.Rows(nDOS)("dtDOS"))
                                    .SetData(_Row, Col_DIAEXAMID, dtProcDetails.Rows(nDOS)("nExamID"))
                                    .SetData(_Row, Col_DIAVISITID, dtProcDetails.Rows(nDOS)("nVisitID"))
                                    .SetData(_Row, Col_HiddenExamName, dtProcDetails.Rows(nDOS)("sExamName"))
                                    .SetData(_Row, Col_HiddenDOS, dtProcDetails.Rows(nDOS)("dtDOS"))
                                    .SetData(_Row, Col_HiddenExFinish, dtProcDetails.Rows(nDOS)("bIsFinished"))

                                    VisitID = dtProcDetails.Rows(nDOS)("nVisitID")
                                    ExamID = dtProcDetails.Rows(nDOS)("nExamID")
                                    DateofServ = dtProcDetails.Rows(nDOS)("dtDOS")
                                End With

                                Dim dtDateOfService As Date = dtProcDetails.Rows(nDOS)("dtDOS")


                                'Query for selecting Diagnosis for current exam 
                                'strselecrICD9Qry = "SELECT Distinct isnull(ExamICD9CPT.sICD9Code,'') as sICD9Code,  isnull(ExamICD9CPT.sICD9Description,'') as sICD9Description, ExamICD9CPT.nExamID, ExamICD9CPT.nVisitID  FROM ExamICD9CPT WHERE  ExamICD9CPT.nPatientID = " & patientid & ""
                                strselecrICD9Qry = "SELECT Distinct isnull(ExamICD9CPT.sICD9Code,'') as sICD9Code,  isnull(ExamICD9CPT.sICD9Description,'') as sICD9Description , isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished ,  ISNULL(ExamICD9CPT.nLineNo,0) as nLineNo" _
                                                   & " FROM ExamICD9CPT inner join PatientExams on " _
                                                   & " ExamICD9CPT.nExamID = PatientExams.nExamID " _
                                                   & " WHERE  ExamICD9CPT.nPatientID = " & patientid & " and PatientExams.dtDOS= '" & dtDateOfService & "' Order by ISNULL(ExamICD9CPT.nLineNo,0) "

                                'Dim oDB As New gloStream.gloDataBase.gloDataBase
                                oDB.Connect(GetConnectionString)
                                dtICD9 = oDB.ReadQueryDataTable(strselecrICD9Qry)
                                oDB.Disconnect()

                                'add the whole dtICD9 block
                                With dtICD9
                                    If IsNothing(dtICD9) = False Then
                                        For nICD9 = 0 To .Rows.Count - 1
                                            Dim count As Integer = nICD9 + 1
                                            If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                                                C1Dignosis.Rows.Add()
                                                _Row = C1Dignosis.Rows.Count - 1
                                                'set the properties for newly added row
                                                With C1Dignosis.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 1
                                                    .Node.Data = dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")
                                                    .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                                                End With
                                                nextICD = _Row
                                                With C1Dignosis
                                                    .SetData(_Row, Col_sICD9Code, _Row)
                                                    .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                    .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                    .SetData(_Row, Col_DIAEXAMID, ExamID)
                                                    .SetData(_Row, Col_DIAVISITID, VisitID)
                                                    .SetData(_Row, Col_HiddenExamName, dtICD9.Rows(nICD9)("sExamName"))
                                                    .SetData(_Row, Col_HiddenDOS, dtDateOfService)
                                                    .SetData(_Row, Col_HiddenExFinish, dtICD9.Rows(nICD9)("bIsFinished"))

                                                    ICD9Code = dtICD9.Rows(nICD9)("sICD9Code")
                                                End With


                                                Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")


                                                'Query for selecting CPT for current exam
                                                'strselectCPTQry = "SELECT DISTINCT isnull(ExamICD9CPT.sCPTcode,'') as sCPTcode , isnull(ExamICD9CPT.sCPTDescription,'') as sCPTDescription , ExamICD9CPT.nUnit, ExamICD9CPT.nExamID, ExamICD9CPT.nVisitID   FROM ExamICD9CPT " _
                                                '& " WHERE  ExamICD9CPT.nPatientID = " & patientid & " AND isnull(ExamICD9CPT.sICD9Code,'') = '" & dtICD9.Rows(nICD9)("sICD9Code") & "' AND isnull(ExamICD9CPT.sICD9Description,'') = '" & dtICD9.Rows(nICD9)("sICD9Description") & "'"
                                                strselectCPTQry = "SELECT Distinct isnull(ExamICD9CPT.sCPTcode,'') as sCPTcode, isnull(ExamICD9CPT.sCPTDescription,'') as sCPTDescription, isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished,nUnit " _
                                                                & " FROM ExamICD9CPT inner join PatientExams on" _
                                                                & " ExamICD9CPT.nExamID = PatientExams.nExamID" _
                                                                & " WHERE  ExamICD9CPT.nPatientID = " & patientid & " and PatientExams.dtDOS=' " & dtDateOfService & " '" _
                                                                & " and ExamICD9CPT.sICD9Code='" & (ICD9Code).Replace("'", "''") & "'"

                                                oDB.Connect(GetConnectionString)
                                                dtCPT = oDB.ReadQueryDataTable(strselectCPTQry)
                                                oDB.Disconnect()

                                                'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nICD9)("sCPTcode"))

                                                With dtCPT
                                                    If IsNothing(dtCPT) = False Then

                                                        For nCPT = 0 To .Rows.Count - 1

                                                            Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
                                                            If strCurrentCPT.Trim <> "" Then
                                                                C1Dignosis.Rows.Add()
                                                                _Row = C1Dignosis.Rows.Count - 1
                                                                'set the properties for newly added row
                                                                With C1Dignosis.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 2
                                                                    .Node.Data = dtCPT.Rows(nCPT)("sCPTcode") & "-" & dtCPT.Rows(nCPT)("sCPTDescription")
                                                                    .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1

                                                                End With

                                                                If _Row = 2 Then
                                                                    strconcatCPT1 = Convert.ToString(count) + "|" + Convert.ToString(count) + "CPT"
                                                                Else
                                                                    strconcatCPT1 = Convert.ToString(nextICD) + "|" + Convert.ToString(nextICD) + "CPT"
                                                                End If

                                                                With C1Dignosis
                                                                    .SetData(_Row, Col_sICD9Code, strconcatCPT1)
                                                                    '.SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                                    '.SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                                    .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                    .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                    .SetData(_Row, Col_Units, dtCPT.Rows(nCPT)("nUnit"))
                                                                    .SetData(_Row, Col_DIAEXAMID, ExamID)
                                                                    .SetData(_Row, Col_DIAVISITID, VisitID)
                                                                    .SetData(_Row, Col_HiddenExamName, dtCPT.Rows(nCPT)("sExamName"))
                                                                    .SetData(_Row, Col_HiddenDOS, dtDateOfService)
                                                                    .SetData(_Row, Col_HiddenExFinish, dtCPT.Rows(nCPT)("bIsFinished"))

                                                                    CPTCode = dtCPT.Rows(nCPT)("sCPTcode")
                                                                End With
                                                            End If

                                                            'Query for selecting Modifier for current exam 
                                                            'strselectMODQry = "SELECT Distinct isnull(ExamICD9CPT.sModCode,'') as sModCode, isnull(ExamICD9CPT.sModDescription,'') as sModDescription, ExamICD9CPT.nExamID, ExamICD9CPT.nVisitID  FROM ExamICD9CPT " _
                                                            '& " WHERE  ExamICD9CPT.nPatientID = " & patientid & " AND isnull(ExamICD9CPT.sCPTcode,'')= '" & dtCPT.Rows(nCPT)("sCPTcode") & "' AND isnull(ExamICD9CPT.sCPTcode,'')= '" & dtCPT.Rows(nCPT)("sCPTcode") & "'"

                                                            strselectMODQry = "SELECT Distinct isnull(ExamICD9CPT.sModCode,'') as sModCode,  isnull(ExamICD9CPT.sModDescription,'') as sModDescription, isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished " _
                                                                              & " FROM ExamICD9CPT inner join PatientExams on ExamICD9CPT.nExamID = PatientExams.nExamID" _
                                                                              & " WHERE  ExamICD9CPT.nPatientID = " & patientid & " and PatientExams.dtDOS='" & dtDateOfService & "' and ExamICD9CPT.sICD9Code='" & (ICD9Code).Replace("'", "''") & "' and ExamICD9CPT.sCPTcode = '" & CPTCode.Replace("'", "''") & "' "

                                                            oDB.Connect(GetConnectionString)
                                                            dtMOD = oDB.ReadQueryDataTable(strselectMODQry)
                                                            oDB.Disconnect()

                                                            With dtMOD
                                                                If IsNothing(dtMOD) = False Then
                                                                    For nMOD = 0 To .Rows.Count - 1

                                                                        Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")

                                                                        If strCurrentMod.Trim <> "" Then
                                                                            C1Dignosis.Rows.Add()
                                                                            _Row = C1Dignosis.Rows.Count - 1
                                                                            'set the properties for newly added row
                                                                            With C1Dignosis.Rows(_Row)
                                                                                .AllowEditing = False
                                                                                .ImageAndText = True
                                                                                .Height = 24
                                                                                .IsNode = True
                                                                                .Node.Level = 3
                                                                                .Node.Data = dtMOD.Rows(nMOD)("sModCode") & "-" & dtMOD.Rows(nMOD)("sModDescription")
                                                                                .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                                                            End With
                                                                            Dim concatMOD As String = strconcatCPT1 + "|" + "MOD"

                                                                            With C1Dignosis
                                                                                .SetData(_Row, Col_sICD9Code, concatMOD)
                                                                                '.SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                                                '.SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                                                '.SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                                '.SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                                .SetData(_Row, Col_ModCode, dtMOD.Rows(nMOD)("sModCode"))
                                                                                .SetData(_Row, Col_ModDesc, dtMOD.Rows(nMOD)("sModDescription"))
                                                                                .SetData(_Row, Col_DIAEXAMID, ExamID)
                                                                                .SetData(_Row, Col_DIAVISITID, VisitID)
                                                                                .SetData(_Row, Col_HiddenExamName, dtMOD.Rows(nMOD)("sExamName"))
                                                                                .SetData(_Row, Col_HiddenDOS, dtDateOfService)
                                                                                .SetData(_Row, Col_HiddenExFinish, dtMOD.Rows(nMOD)("bIsFinished"))

                                                                                'Dim sunitint As Integer =  GetType(Integer) dtMOD.Rows(nMOD)("nUnit")
                                                                            End With


                                                                        End If
                                                                    Next
                                                                    If (IsNothing(dtMOD) = False) Then
                                                                        dtMOD.Dispose()
                                                                        dtMOD = Nothing
                                                                    End If
                                                                End If
                                                            End With '' With dtMOD
                                                        Next '' For nCPT = 0 To .Rows.Count - 1
                                                        If (IsNothing(dtCPT) = False) Then
                                                            dtCPT.Dispose()
                                                            dtCPT = Nothing
                                                        End If
                                                    End If
                                                End With '' With dtCPT
                                            End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                                        Next ''For nICD9 = 0 To .Rows.Count - 1
                                        If (IsNothing(dtICD9) = False) Then
                                            dtICD9.Dispose()
                                            dtICD9 = Nothing
                                        End If
                                    End If  '' If IsNothing(dtICD9) = False Then
                                End With '' With dtICD9



                            End If
                        Next
                    End If
                End With






             

                DiagNode = Nothing
                ICD9Node = Nothing
                CPTNode = Nothing
                MODNode = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(dtProcDetails) = False) Then
                dtProcDetails.Dispose()
                dtProcDetails = Nothing
            End If
            If (IsNothing(dtICD9) = False) Then
                dtICD9.Dispose()
                dtICD9 = Nothing
            End If
            If (IsNothing(dtCPT) = False) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            If (IsNothing(dtMOD) = False) Then
                dtMOD.Dispose()
                dtMOD = Nothing
            End If

        End Try
        C1Dignosis.Redraw = True
    End Sub

    Private Sub FillICDCPTMODForSearch(ByVal patientid As Int64, ByVal strSearch As String, ByVal blnProcDt As Boolean, Optional ByVal fromDate As Date = Nothing, Optional ByVal toDate As Date = Nothing) 'As gloCPTs

        Dim dtDOS As DataTable = Nothing
        Dim dtICD9 As DataTable = Nothing
        Dim dtCPT As DataTable = Nothing
        Dim dtMOD As DataTable = Nothing
        Dim oDB As New gloStream.gloDataBase.gloDataBase

        Try
            FillGridStyle()

            Dim _Row As Integer
        
            With C1Dignosis
                .Tree.Column = Col_ICD9Code_Description
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
            End With



            Dim DiagNode As myTreeNode
            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode
            Dim nDOS As Int16

            Dim nICD9 As Int16
            Dim nCPT As Int16
            Dim nMOD As Int16


            Dim strselectDOSQry As String


            Dim strselecrICD9Qry As String
            Dim strselectCPTQry As String
            Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextICD As Integer




            'Query for selecting ICD9 for current exam 
            If blnProcDt = False Then 'means  call the text search queries
                strselectDOSQry = "select distinct PatientExams.dtDOS as dtDOS, ExamICD9CPT.nExamID as nExamID, ExamICD9CPT.nVisitID as nVisitID, isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished" _
                                              & " from PatientExams inner join ExamICD9CPT on  PatientExams.nExamID = ExamICD9CPT.nExamID" _
                                              & " where ExamICD9CPT.nPatientID = " & patientid & " and (isnull(sICD9Code,'') like '%" & strSearch & "%' or  isnull(sICD9Description,'') like '%" & strSearch & "%' or isnull(sCPTcode,'') like '%" & strSearch & "%' or  isnull(sCPTDescription,'') like '%" & strSearch & "%' or isnull(sModCode,'') like '%" & strSearch & "%' or  isnull(sModDescription,'') like '% " & strSearch & "%')"
            Else ' call the date search queries
                strselectDOSQry = "SELECT Distinct PatientExams.dtDOS as dtDOS,  ExamICD9CPT.nExamID as nExamID,  " _
                                          & " ExamICD9CPT.nVisitID as nVisitID, isnull(PatientExams.sExamName,'') as sExamName , PatientExams.bIsFinished as bIsFinished " _
                                          & " FROM ExamICD9CPT inner join PatientExams on  ExamICD9CPT.nExamID = PatientExams.nExamID  " _
                                          & " WHERE  (ExamICD9CPT.nPatientID = " & patientid & " and PatientExams.dtDOS >= '" & fromDate & "' and PatientExams.dtDOS <= '" & toDate & "') "

            End If


            oDB.Connect(GetConnectionString)
            dtDOS = oDB.ReadQueryDataTable(strselectDOSQry)
            oDB.Disconnect()

            With dtDOS 'pull all the Diagnosis for the respective dates
                If IsNothing(dtDOS) = False Then
                    For nDOS = 0 To .Rows.Count - 1
                        'Dim ElectroPhysiologyID As Int64 = 0
                        Dim VisitID As Int64 = 0
                        Dim ExamID As Int64 = 0
                        Dim DateofServ As Date
                        Dim ICD9Code As String = ""
                        Dim CPTCode As String = ""

                        Dim count1 As Integer = nDOS + 1

                        If CStr(dtDOS.Rows(nDOS)("dtDOS")).Trim <> "" Then
                            C1Dignosis.Rows.Add()
                            _Row = C1Dignosis.Rows.Count - 1
                            'set the properties for newly added row
                            With C1Dignosis.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                .Node.Data = dtDOS.Rows(nDOS)("dtDOS") 'dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")
                                .Node.Image = ImgPatientTab.Images(13) ' Global.gloEMR.My.Resources.Resources.ICD_09
                            End With
                            nextICD = _Row
                            With C1Dignosis
                                .SetData(_Row, Col_DateOfService, dtDOS.Rows(nDOS)("dtDOS"))
                                .SetData(_Row, Col_DIAEXAMID, dtDOS.Rows(nDOS)("nExamID"))
                                .SetData(_Row, Col_DIAVISITID, dtDOS.Rows(nDOS)("nVisitID"))
                                .SetData(_Row, Col_HiddenExamName, dtDOS.Rows(nDOS)("sExamName"))
                                .SetData(_Row, Col_HiddenDOS, dtDOS.Rows(nDOS)("dtDOS"))
                                .SetData(_Row, Col_HiddenExFinish, dtDOS.Rows(nDOS)("bIsFinished"))

                                VisitID = dtDOS.Rows(nDOS)("nVisitID")
                                ExamID = dtDOS.Rows(nDOS)("nExamID")
                                DateofServ = dtDOS.Rows(nDOS)("dtDOS")
                            End With

                            Dim dtDateOfService As Date = dtDOS.Rows(nDOS)("dtDOS")



                            strselecrICD9Qry = "SELECT Distinct isnull(ExamICD9CPT.sICD9Code,'') as sICD9Code,  " _
                           & " isnull(ExamICD9CPT.sICD9Description,'') as sICD9Description , " _
                           & " isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished  " _
                           & " FROM ExamICD9CPT inner join PatientExams on  ExamICD9CPT.nExamID = PatientExams.nExamID  " _
                           & " WHERE  (ExamICD9CPT.nPatientID = " & patientid & " and PatientExams.dtDOS= '" & dtDateOfService & "') "


                            oDB.Connect(GetConnectionString)
                            dtICD9 = oDB.ReadQueryDataTable(strselecrICD9Qry)
                            oDB.Disconnect()

                            'add the whole dtICD9 block
                            With dtICD9
                                If IsNothing(dtICD9) = False Then
                                    For nICD9 = 0 To .Rows.Count - 1
                                        Dim count As Integer = nICD9 + 1
                                        If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                                            C1Dignosis.Rows.Add()
                                            _Row = C1Dignosis.Rows.Count - 1
                                            'set the properties for newly added row
                                            With C1Dignosis.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")
                                                .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
                                            End With
                                            nextICD = _Row
                                            With C1Dignosis
                                                .SetData(_Row, Col_sICD9Code, _Row)
                                                .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                .SetData(_Row, Col_DIAEXAMID, ExamID)
                                                .SetData(_Row, Col_DIAVISITID, VisitID)
                                                .SetData(_Row, Col_HiddenExamName, dtICD9.Rows(nICD9)("sExamName"))
                                                .SetData(_Row, Col_HiddenDOS, dtDateOfService)
                                                .SetData(_Row, Col_HiddenExFinish, dtICD9.Rows(nICD9)("bIsFinished"))

                                                ICD9Code = dtICD9.Rows(nICD9)("sICD9Code")
                                            End With


                                            Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")


                                            'Query for selecting CPT for current exam
                                            strselectCPTQry = "SELECT Distinct isnull(ExamICD9CPT.sCPTcode,'') as sCPTcode, isnull(ExamICD9CPT.sCPTDescription,'') as sCPTDescription, isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished, ExamICD9CPT.nUnit " _
                                                            & " FROM ExamICD9CPT inner join PatientExams on" _
                                                            & " ExamICD9CPT.nExamID = PatientExams.nExamID" _
                                                            & " WHERE (ExamICD9CPT.nPatientID = " & patientid & " and PatientExams.dtDOS=' " & dtDateOfService & " '" _
                                                            & " and ExamICD9CPT.sICD9Code='" & (ICD9Code).Replace("'", "''") & "') "

                                            oDB.Connect(GetConnectionString)
                                            dtCPT = oDB.ReadQueryDataTable(strselectCPTQry)
                                            oDB.Disconnect()

                                            With dtCPT
                                                If IsNothing(dtCPT) = False Then

                                                    For nCPT = 0 To .Rows.Count - 1

                                                        Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
                                                        If strCurrentCPT.Trim <> "" Then
                                                            C1Dignosis.Rows.Add()
                                                            _Row = C1Dignosis.Rows.Count - 1
                                                            'set the properties for newly added row
                                                            With C1Dignosis.Rows(_Row)
                                                                .AllowEditing = True
                                                                .ImageAndText = True
                                                                .Height = 24
                                                                .IsNode = True
                                                                .Node.Level = 2
                                                                .Node.Data = dtCPT.Rows(nCPT)("sCPTcode") & "-" & dtCPT.Rows(nCPT)("sCPTDescription")
                                                                .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1

                                                            End With



                                                            With C1Dignosis
                                                                .SetData(_Row, Col_sICD9Code, strconcatCPT1)
                                                                '.SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                                '.SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                                .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                .SetData(_Row, Col_Units, dtCPT.Rows(nCPT)("nUnit"))
                                                                .SetData(_Row, Col_DIAEXAMID, ExamID)
                                                                .SetData(_Row, Col_DIAVISITID, VisitID)
                                                                .SetData(_Row, Col_HiddenExamName, dtCPT.Rows(nCPT)("sExamName"))
                                                                .SetData(_Row, Col_HiddenDOS, dtDateOfService)
                                                                .SetData(_Row, Col_HiddenExFinish, dtCPT.Rows(nCPT)("bIsFinished"))

                                                                CPTCode = dtCPT.Rows(nCPT)("sCPTcode")
                                                            End With
                                                        End If

                                                        'Query for selecting Modifier for current exam 

                                                        strselectMODQry = "SELECT Distinct isnull(ExamICD9CPT.sModCode,'') as sModCode,  isnull(ExamICD9CPT.sModDescription,'') as sModDescription, isnull(PatientExams.sExamName,'') as sExamName, PatientExams.bIsFinished as bIsFinished " _
                                                                          & " FROM ExamICD9CPT inner join PatientExams on ExamICD9CPT.nExamID = PatientExams.nExamID" _
                                                                          & " WHERE  (ExamICD9CPT.nPatientID = " & patientid & " and PatientExams.dtDOS='" & dtDateOfService & "' and ExamICD9CPT.sICD9Code='" & ICD9Code.Replace("'", "''") & "' and ExamICD9CPT.sCPTcode = '" & CPTCode.Replace("'", "''") & "') "

                                                        oDB.Connect(GetConnectionString)
                                                        dtMOD = oDB.ReadQueryDataTable(strselectMODQry)
                                                        oDB.Disconnect()

                                                        With dtMOD
                                                            If IsNothing(dtMOD) = False Then
                                                                For nMOD = 0 To .Rows.Count - 1

                                                                    Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")

                                                                    If strCurrentMod.Trim <> "" Then
                                                                        C1Dignosis.Rows.Add()
                                                                        _Row = C1Dignosis.Rows.Count - 1
                                                                        'set the properties for newly added row
                                                                        With C1Dignosis.Rows(_Row)
                                                                            .AllowEditing = False
                                                                            .ImageAndText = True
                                                                            .Height = 24
                                                                            .IsNode = True
                                                                            .Node.Level = 3
                                                                            .Node.Data = dtMOD.Rows(nMOD)("sModCode") & "-" & dtMOD.Rows(nMOD)("sModDescription")
                                                                            .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
                                                                        End With
                                                                        Dim concatMOD As String = strconcatCPT1 + "|" + "MOD"

                                                                        With C1Dignosis
                                                                            .SetData(_Row, Col_sICD9Code, concatMOD)
                                                                            '.SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
                                                                            '.SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
                                                                            '.SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
                                                                            '.SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
                                                                            .SetData(_Row, Col_ModCode, dtMOD.Rows(nMOD)("sModCode"))
                                                                            .SetData(_Row, Col_ModDesc, dtMOD.Rows(nMOD)("sModDescription"))
                                                                            .SetData(_Row, Col_DIAEXAMID, ExamID)
                                                                            .SetData(_Row, Col_DIAVISITID, VisitID)
                                                                            .SetData(_Row, Col_HiddenExamName, dtMOD.Rows(nMOD)("sExamName"))
                                                                            .SetData(_Row, Col_HiddenDOS, dtDateOfService)
                                                                            .SetData(_Row, Col_HiddenExFinish, dtMOD.Rows(nMOD)("bIsFinished"))

                                                                            'Dim sunitint As Integer =  GetType(Integer) dtMOD.Rows(nMOD)("nUnit")
                                                                        End With


                                                                    End If
                                                                Next
                                                                If (IsNothing(dtMOD) = False) Then
                                                                    dtMOD.Dispose()
                                                                    dtMOD = Nothing
                                                                End If
                                                            End If
                                                        End With '' With dtMOD
                                                    Next '' For nCPT = 0 To .Rows.Count - 1
                                                    If (IsNothing(dtCPT) = False) Then
                                                        dtCPT.Dispose()
                                                        dtCPT = Nothing
                                                    End If
                                                End If
                                            End With '' With dtCPT
                                        End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                                    Next ''For nICD9 = 0 To .Rows.Count - 1
                                    If (IsNothing(dtICD9) = False) Then
                                        dtICD9.Dispose()
                                        dtICD9 = Nothing
                                    End If
                                End If  '' If IsNothing(dtICD9) = False Then
                            End With '' With dtICD9



                        End If
                    Next
                End If
            End With








            DiagNode = Nothing
            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(dtDOS) = False) Then
                dtDOS.Dispose()
                dtDOS = Nothing
            End If
            If (IsNothing(dtICD9) = False) Then
                dtICD9.Dispose()
                dtICD9 = Nothing
            End If
            If (IsNothing(dtCPT) = False) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            If (IsNothing(dtMOD) = False) Then
                dtMOD.Dispose()
                dtMOD = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub


    'Private Function FillICDCPTMOD_Old(ByVal patientid As Int64) As gloCPTs

    '    Try
    '        FillGridStyle()

    '        Dim _Row As Integer
    '        'Dim i As Integer
    '        'set properties of treeview in flexgrid
    '        With C1Dignosis
    '            .Tree.Column = Col_ICD9Code_Description
    '            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
    '            .Tree.LineStyle = Drawing2D.DashStyle.Solid
    '            .Tree.Indent = 15
    '        End With
    '        Dim dtICD9 As DataTable
    '        Dim dtCPT As DataTable
    '        Dim dtMOD As DataTable

    '        Dim ICD9Node As myTreeNode
    '        Dim CPTNode As myTreeNode
    '        Dim MODNode As myTreeNode

    '        Dim nICD9 As Int16
    '        Dim nCPT As Int16
    '        Dim nMOD As Int16

    '        Dim strselecrICD9Qry As String
    '        Dim strselectCPTQry As String
    '        Dim strselectMODQry As String
    '        Dim strconcatCPT1 As String = ""
    '        Dim nextICD As Integer

    '        'Query for selecting ICD9 for current exam 
    '        strselecrICD9Qry = "SELECT Distinct isnull(ExamICD9CPT.sICD9Code,'') as sICD9Code,  isnull(ExamICD9CPT.sICD9Description,'') as sICD9Description, ExamICD9CPT.nExamID, ExamICD9CPT.nVisitID  FROM ExamICD9CPT WHERE  ExamICD9CPT.nPatientID = " & patientid & ""
    '        Dim oDB As New gloStream.gloDataBase.gloDataBase
    '        oDB.Connect(GetConnectionString)
    '        dtICD9 = oDB.ReadQueryDataTable(strselecrICD9Qry)
    '        oDB.Disconnect()

    '        With dtICD9
    '            If IsNothing(dtICD9) = False Then
    '                For nICD9 = 0 To .Rows.Count - 1
    '                    Dim count As Integer = nICD9 + 1
    '                    If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
    '                        C1Dignosis.Rows.Add()
    '                        _Row = C1Dignosis.Rows.Count - 1
    '                        'set the properties for newly added row
    '                        With C1Dignosis.Rows(_Row)
    '                            .AllowEditing = False
    '                            .ImageAndText = True
    '                            .Height = 24
    '                            .IsNode = True
    '                            .Node.Level = 0
    '                            .Node.Data = dtICD9.Rows(nICD9)("sICD9Code") & "-" & dtICD9.Rows(nICD9)("sICD9Description")
    '                            .Node.Image = Global.gloEMR.My.Resources.Resources.ICD_09
    '                        End With
    '                        nextICD = _Row
    '                        With C1Dignosis
    '                            .SetData(_Row, Col_sICD9Code, _Row)
    '                            .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
    '                            .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
    '                            .SetData(_Row, Col_DIAEXAMID, dtICD9.Rows(nICD9)("nExamID"))
    '                            .SetData(_Row, Col_DIAVISITID, dtICD9.Rows(nICD9)("nVisitID"))
    '                        End With


    '                        Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")


    '                        'Query for selecting CPT for current exam
    '                        strselectCPTQry = "SELECT DISTINCT isnull(ExamICD9CPT.sCPTcode,'') as sCPTcode , isnull(ExamICD9CPT.sCPTDescription,'') as sCPTDescription , ExamICD9CPT.nUnit, ExamICD9CPT.nExamID, ExamICD9CPT.nVisitID   FROM ExamICD9CPT " _
    '                        & " WHERE  ExamICD9CPT.nPatientID = " & patientid & " AND isnull(ExamICD9CPT.sICD9Code,'') = '" & dtICD9.Rows(nICD9)("sICD9Code") & "' AND isnull(ExamICD9CPT.sICD9Description,'') = '" & dtICD9.Rows(nICD9)("sICD9Description") & "'"
    '                        oDB.Connect(GetConnectionString)
    '                        dtCPT = oDB.ReadQueryDataTable(strselectCPTQry)
    '                        oDB.Disconnect()

    '                        'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nICD9)("sCPTcode"))

    '                        With dtCPT
    '                            If IsNothing(dtCPT) = False Then

    '                                For nCPT = 0 To .Rows.Count - 1

    '                                    Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
    '                                    If strCurrentCPT.Trim <> "" Then
    '                                        C1Dignosis.Rows.Add()
    '                                        _Row = C1Dignosis.Rows.Count - 1
    '                                        'set the properties for newly added row
    '                                        With C1Dignosis.Rows(_Row)
    '                                            .AllowEditing = True
    '                                            .ImageAndText = True
    '                                            .Height = 24
    '                                            .IsNode = True
    '                                            .Node.Level = 1
    '                                            .Node.Data = dtCPT.Rows(nCPT)("sCPTcode") & "-" & dtCPT.Rows(nCPT)("sCPTDescription")
    '                                            .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1

    '                                        End With

    '                                        If _Row = 2 Then
    '                                            strconcatCPT1 = Convert.ToString(count) + "|" + Convert.ToString(count) + "CPT"
    '                                        Else
    '                                            strconcatCPT1 = Convert.ToString(nextICD) + "|" + Convert.ToString(nextICD) + "CPT"
    '                                        End If

    '                                        With C1Dignosis
    '                                            .SetData(_Row, Col_sICD9Code, strconcatCPT1)
    '                                            .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
    '                                            .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
    '                                            .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
    '                                            .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
    '                                            .SetData(_Row, Col_Units, dtCPT.Rows(nCPT)("nUnit"))
    '                                            .SetData(_Row, Col_DIAEXAMID, dtCPT.Rows(nCPT)("nExamID"))
    '                                            .SetData(_Row, Col_DIAVISITID, dtCPT.Rows(nCPT)("nVisitID"))
    '                                        End With
    '                                    End If

    '                                    'Query for selecting Modifier for current exam 
    '                                    strselectMODQry = "SELECT Distinct isnull(ExamICD9CPT.sModCode,'') as sModCode, isnull(ExamICD9CPT.sModDescription,'') as sModDescription, ExamICD9CPT.nExamID, ExamICD9CPT.nVisitID  FROM ExamICD9CPT " _
    '                                    & " WHERE  ExamICD9CPT.nPatientID = " & patientid & " AND isnull(ExamICD9CPT.sCPTcode,'')= '" & dtCPT.Rows(nCPT)("sCPTcode") & "' AND isnull(ExamICD9CPT.sCPTcode,'')= '" & dtCPT.Rows(nCPT)("sCPTcode") & "'"

    '                                    oDB.Connect(GetConnectionString)
    '                                    dtMOD = oDB.ReadQueryDataTable(strselectMODQry)
    '                                    oDB.Disconnect()

    '                                    With dtMOD
    '                                        If IsNothing(dtMOD) = False Then
    '                                            For nMOD = 0 To .Rows.Count - 1

    '                                                Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")

    '                                                If strCurrentMod.Trim <> "" Then
    '                                                    C1Dignosis.Rows.Add()
    '                                                    _Row = C1Dignosis.Rows.Count - 1
    '                                                    'set the properties for newly added row
    '                                                    With C1Dignosis.Rows(_Row)
    '                                                        .AllowEditing = False
    '                                                        .ImageAndText = True
    '                                                        .Height = 24
    '                                                        .IsNode = True
    '                                                        .Node.Level = 2
    '                                                        .Node.Data = dtMOD.Rows(nMOD)("sModCode") & "-" & dtMOD.Rows(nMOD)("sModDescription")
    '                                                        .Node.Image = Global.gloEMR.My.Resources.Resources.Modify1
    '                                                    End With
    '                                                    Dim concatMOD As String = strconcatCPT1 + "|" + "MOD"

    '                                                    With C1Dignosis
    '                                                        .SetData(_Row, Col_sICD9Code, concatMOD)
    '                                                        .SetData(_Row, Col_ICD9Code, dtICD9.Rows(nICD9)("sICD9Code"))
    '                                                        .SetData(_Row, Col_ICD9Desc, dtICD9.Rows(nICD9)("sICD9Description"))
    '                                                        .SetData(_Row, COl_CPTCode, dtCPT.Rows(nCPT)("sCPTcode"))
    '                                                        .SetData(_Row, Col_CPTDesc, dtCPT.Rows(nCPT)("sCPTDescription"))
    '                                                        .SetData(_Row, Col_ModCode, dtMOD.Rows(nMOD)("sModCode"))
    '                                                        .SetData(_Row, Col_ModDesc, dtMOD.Rows(nMOD)("sModDescription"))
    '                                                        .SetData(_Row, Col_DIAEXAMID, dtMOD.Rows(nMOD)("nExamID"))
    '                                                        .SetData(_Row, Col_DIAVISITID, dtMOD.Rows(nMOD)("nVisitID"))
    '                                                        'Dim sunitint As Integer =  GetType(Integer) dtMOD.Rows(nMOD)("nUnit")
    '                                                    End With


    '                                                End If
    '                                            Next
    '                                        End If
    '                                    End With '' With dtMOD
    '                                Next '' For nCPT = 0 To .Rows.Count - 1
    '                            End If
    '                        End With '' With dtCPT
    '                    End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
    '                Next ''For nICD9 = 0 To .Rows.Count - 1
    '            End If  '' If IsNothing(dtICD9) = False Then
    '        End With '' With dtICD9


    '        dtICD9 = Nothing
    '        dtCPT = Nothing
    '        dtMOD = Nothing

    '        ICD9Node = Nothing
    '        CPTNode = Nothing
    '        MODNode = Nothing
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, "Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function

    'Private Function FillICDCPTMOD(ByVal patientid As Int64) As gloCPTs
    '    Dim conn As New SqlConnection(GetConnectionString)
    '    Dim sqladpt As SqlDataAdapter
    '    Dim sql As SqlCommand

    '    Dim dtICD9 As New DataTable
    '    Dim dtCPT As New DataTable
    '    Dim dtMOD As New DataTable
    '    Dim CPTCol As New gloCPTs


    '    Dim _ICD9 As gloICD9
    '    Dim _CPT As gloCPT
    '    Dim _Modifier As gloModifier
    '    conn.Open()

    '    Try

    '        Dim nICD9 As Int16
    '        Dim nCPT As Int16
    '        Dim nMOD As Int16

    '        Dim strselecrICD9Qry As String
    '        Dim strselectCPTQry As String
    '        Dim strselectMODQry As String

    '        'Query for selecting ICD9 for current exam 
    '        'If ExamID = 0 Then
    '        'strselectCPTQry = "SELECT distinct isnull(i.sCPTCode,'') as sCPTCode,isnull(i.sCPTDescription,'') as sCPTDescription, " _
    '        '                   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
    '        '                   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),'ExamID'=p.nexamid ,'Unit' =isnull(nUnit,0) FROM ExamICD9CPT i inner join " _
    '        '                   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
    '        '                   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
    '        '                   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
    '        '                   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
    '        '                   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "' and v.npatientid = " & patientid
    '        strselectCPTQry = "SELECT distinct isnull(i.sCPTCode,'') as sCPTCode,isnull(i.sCPTDescription,'') as sCPTDescription, " _
    '                          & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
    '                          & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),'ExamID'=p.nexamid ,'Unit' =isnull(nUnit,0) FROM ExamICD9CPT i inner join " _
    '                          & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
    '                          & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
    '                          & " v.npatientid = " & patientid
    '        'Else
    '        'strselectCPTQry = "SELECT distinct isnull(i.sCPTCode,'') as sCPTCode,isnull(i.sCPTDescription,'') as sCPTDescription, " _
    '        '   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
    '        '   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),'ExamID'=p.nexamid ,'Unit' =isnull(nUnit,0) FROM ExamICD9CPT i inner join " _
    '        '   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
    '        '   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
    '        '   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
    '        '   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
    '        '   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "' and v.npatientid = " & patientid & " and p.nexamid = " & ExamID & ""

    '        'End If
    '        sql = New SqlCommand
    '        sql.Connection = conn
    '        sql.CommandType = CommandType.Text
    '        sql.CommandText = strselectCPTQry
    '        sqladpt = New SqlDataAdapter(sql)

    '        sqladpt.Fill(dtCPT)
    '        sql.Dispose()
    '        sql = Nothing

    '        With dtCPT
    '            If IsNothing(dtCPT) = False Then
    '                For nCPT = 0 To .Rows.Count - 1
    '                    If nCPT = 0 Then
    '                        '_Visit.ProviderDoctor.Code = dtCPT.Rows(0)("ExternalCode")
    '                        '_Visit.ProviderDoctor.FirstName = dtCPT.Rows(0)("Firstname")
    '                        '_Visit.ProviderDoctor.MiddleName = dtCPT.Rows(0)("Middlename")
    '                        '_Visit.ProviderDoctor.LastName = dtCPT.Rows(0)("Lastname")
    '                    End If
    '                    '   Dim count As Integer = nCPT + 1
    '                    If CStr(dtCPT.Rows(nCPT)("sCPTCode")).Trim <> "" Then

    '                        _CPT = New gloCPT

    '                        _CPT.Code = dtCPT.Rows(nCPT)("sCPTCode")
    '                        _CPT.Description = dtCPT.Rows(nCPT)("sCPTDescription")
    '                        _CPT.ExamID = dtCPT.Rows(nCPT)("ExamId")
    '                        _CPT.Unit = dtCPT.Rows(nCPT)("Unit")
    '                        Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTCode")


    '                        'Query for selecting CPT for current exam
    '                        'strselecrICD9Qry = "SELECT Distinct isnull(i.sICD9Code,'') as sICD9Code,isnull(i.sICD9Description,'') as sICD9Description, " _
    '                        '   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
    '                        '   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,'') FROM ExamICD9CPT i inner join " _
    '                        '   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
    '                        '   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
    '                        '   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
    '                        '   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
    '                        '   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "'" _
    '                        '   & " and v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID
    '                        strselecrICD9Qry = "SELECT Distinct isnull(i.sICD9Code,'') as sICD9Code,isnull(i.sICD9Description,'') as sICD9Description, " _
    '                           & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
    '                           & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,'') FROM ExamICD9CPT i inner join " _
    '                           & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
    '                           & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
    '                           & " v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID

    '                        dtICD9.Clear()
    '                        sql = New SqlCommand
    '                        sql.Connection = conn
    '                        sql.CommandType = CommandType.Text
    '                        sql.CommandText = strselecrICD9Qry
    '                        sqladpt = New SqlDataAdapter(sql)

    '                        sqladpt.Fill(dtICD9)
    '                        sql.Dispose()
    '                        sql = Nothing
    '                        'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nICD9)("sCPTcode"))

    '                        With dtICD9
    '                            If IsNothing(dtICD9) = False Then

    '                                For nICD9 = 0 To .Rows.Count - 1

    '                                    Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")
    '                                    If strCurrentCPT.Trim <> "" Then
    '                                        _ICD9 = New gloICD9

    '                                        _ICD9.Code = dtICD9.Rows(nICD9)("sICD9Code")
    '                                        _ICD9.Description = dtICD9.Rows(nICD9)("sICD9Description")
    '                                        _CPT.ICD9Col.Add(_ICD9)
    '                                        _ICD9 = Nothing '' With dtMOD
    '                                    End If
    '                                Next '' For nCPT = 0 To .Rows.Count - 1
    '                            End If
    '                        End With '' With dtCPT
    '                        'Query for selecting Modifier for current exam 
    '                        'strselectMODQry = "SELECT Distinct isnull(i.sModCode,'') as sModCode,isnull(i.sModDescription,'') as sModDescription, " _
    '                        '   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
    '                        '   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),isnull(nUnit,0) as nUnit FROM ExamICD9CPT i inner join " _
    '                        '   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
    '                        '   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
    '                        '   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
    '                        '   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
    '                        '   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "'" _
    '                        '   & " and v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID
    '                        strselectMODQry = "SELECT Distinct isnull(i.sModCode,'') as sModCode,isnull(i.sModDescription,'') as sModDescription, " _
    '                        & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
    '                        & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),isnull(nUnit,0) as nUnit FROM ExamICD9CPT i inner join " _
    '                        & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
    '                        & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
    '                        & " v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID

    '                        dtMOD.Clear()
    '                        sql = New SqlCommand
    '                        sql.Connection = conn
    '                        sql.CommandType = CommandType.Text
    '                        sql.CommandText = strselectMODQry
    '                        sqladpt = New SqlDataAdapter(sql)

    '                        sqladpt.Fill(dtMOD)
    '                        sql.Dispose()
    '                        sql = Nothing
    '                        With dtMOD
    '                            If IsNothing(dtMOD) = False Then
    '                                For nMOD = 0 To .Rows.Count - 1
    '                                    Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")
    '                                    If strCurrentMod.Trim <> "" Then
    '                                        'set the properties for newly added row
    '                                        _Modifier = New gloEMR.gloModifier
    '                                        If Not IsDBNull(dtMOD.Rows(nMOD)("sModCode")) Then
    '                                            _Modifier.Code = dtMOD.Rows(nMOD)("sModCode")
    '                                        Else
    '                                            _Modifier.Code = ""
    '                                        End If
    '                                        If Not IsDBNull(dtMOD.Rows(nMOD)("sModDescription")) Then
    '                                            _Modifier.Description = dtMOD.Rows(nMOD)("sModDescription")
    '                                        Else
    '                                            _Modifier.Description = ""
    '                                        End If
    '                                        'If Not IsDBNull(dtMOD.Rows(nMOD)("nUnit")) Then
    '                                        '    _Modifier.Unit = dtMOD.Rows(nMOD)("nUnit")
    '                                        'Else
    '                                        '    _Modifier.Unit = 0
    '                                        'End If

    '                                        _CPT.ModfierCol.Add(_Modifier)
    '                                        _Modifier = Nothing
    '                                    End If
    '                                Next
    '                            End If
    '                        End With
    '                        CPTCol.Add(_CPT)
    '                        _CPT = Nothing
    '                    End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
    '                Next ''For nICD9 = 0 To .Rows.Count - 1
    '            End If  '' If IsNothing(dtICD9) = False Then
    '        End With '' With dtICD9
    '        dtICD9 = Nothing
    '        dtCPT = Nothing
    '        dtMOD = Nothing

    '        Return CPTCol
    '    Catch ex As Exception
    '        Throw
    '        Return Nothing
    '    Finally
    '        If conn.State = ConnectionState.Open Then
    '            conn.Close()
    '        End If
    '        conn = Nothing
    '    End Try
    'End Function
    ''

    Friend Sub Fill_Medications(Optional ByVal dtSearchMedications As DataTable = Nothing)

        gloC1FlexStyle.Style(C1MedicationDetails) ''Ojeswini_01302010

        ''dhruv 20091130
        ''Avoid the flickering
        C1MedicationDetails.Redraw = False

        C1MedicationDetails.Visible = True
        'trvPatientDetails.Visible = False

        ' '' ''Dim dtPatientDetails As DataTable
        Dim objPatientDetail As New clsPatientDetails

        If IsNothing(dtSearchMedications) Then
            If IsNothing(dtPatientDetails) Then
                dtPatientDetails = objPatientDetail.Fill_Medication(pID)
                'dtMedicationDate, sMedication ,sDosage ,sRoute, sFrequency, sDuration, sAmount, sStatus, dtStartDate, dtEndDate, UserID , UserName 
                'objPatientDetail = Nothing
                oSearchMedicationsCtl.IntialiseDatatable(dtPatientDetails)
            End If
        Else
            dtPatientDetails = dtSearchMedications
        End If
        objPatientDetail.Dispose()
        objPatientDetail = Nothing

        C1MedicationDetails.Enabled = False
        C1MedicationDetails.DataSource = dtPatientDetails.DefaultView
        C1MedicationDetails.Enabled = True

        'PRD - PRD EMR 6030_XXXX Rx Workflow Data integrity 
        With C1MedicationDetails
            .AllowSorting = AllowSortingEnum.None
            .Visible = True
            .BringToFront()
            .Cols.Count = 14 ''added prescriber col as asked from OM team in 7002 version
            '.Rows.Count = 1
            C1dgPatientDetails.ShowCellLabels = True
            gloC1FlexStyle.Style(C1MedicationDetails)
            .ExtendLastCol = True
            .Cols(0).AllowEditing = True
            .Cols(0).AllowResizing = True
            '' Set Fixed Rows
            .SetData(0, 0, "Updated")
            .Cols(0).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 1, "Reviewed By")
            .Cols(1).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 2, "Updated By")
            .Cols(2).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 3, "Drug")
            .Cols(3).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 4, "Prescriber") ''added prescriber col as asked from OM team in 7002 version
            .Cols(4).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 5, "Start Date")
            .Cols(5).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 6, "End Date")
            .Cols(6).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 7, "Status")
            .Cols(7).TextAlign = TextAlignEnum.LeftCenter
           
            '.SetData(0, 2, "Dosage")
            '.Cols(2).TextAlign = TextAlignEnum.LeftCenter
            '.SetData(0, 3, "Route")
            '.Cols(3).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 8, "Patient Directions")
            .Cols(8).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 9, "Duration")
            .Cols(9).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 10, "Quantity")
            .Cols(10).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 11, "Refills")
            .Cols(11).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 12, "Issue Method")
            .Cols(12).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, 13, "Pharmacy")
            .Cols(13).TextAlign = TextAlignEnum.LeftCenter
            'PRD EMR 6030_XXXX Rx Workflow Updated PRD Rx Field Names
            '.SetData(0, 11, "Allow Substitution")
            '.Cols(11).TextAlign = TextAlignEnum.LeftCenter





            .Cols(0).Width = .Width * 0.08  'Date
            .Cols(1).Width = .Width * 0.08   'User Name 
            .Cols(2).Width = .Width * 0.08   'Updated by
            .Cols(3).Width = .Width * 0.2   'Medication
            .Cols(4).Width = .Width * 0.09  ''added prescriber col as asked from OM team in 7002 version
            .Cols(5).Width = .Width * 0.08  'Dosage
            .Cols(6).Width = .Width * 0.08   'Route
            .Cols(7).Width = .Width * 0.07  'Frequency
            .Cols(8).Width = .Width * 0.09  'Duration 
            .Cols(9).Width = .Width * 0.12  'Dispense
            .Cols(10).Width = .Width * 0.07   'Status
            .Cols(11).Width = .Width * 0.05  'Start Date
            .Cols(12).Width = .Width * 0.095  'End Date    

            'PRD EMR 6030_XXXX Rx Workflow Updated PRD Rx Field Names
            .Cols(13).Width = .Width * 0.09

        End With

        ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Medication viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)
        C1MedicationDetails.Redraw = True
    End Sub
    Private Sub tbSummary_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbSummary.SelectedIndexChanged
        Select Case tbSummary.SelectedIndex
            Case 1
                'SelectedPatientDetail = PatientDetails.ProblemList
                'ShowPatientDetails()
                Fill_ProblemList()
            Case 2
                'SelectedPatientDetail = PatientDetails.Medication
                'ShowPatientDetails()
                Fill_Medications()

            Case 3
                'SelectedPatientDetail = PatientDetails.History
                'ShowPatientDetails()
                Fill_PatientHistory()
            Case 4
                'SelectedPatientDetail = PatientDetails.Procedures
                'ShowPatientDetails()
                ''FillCPTTreeView()
                'FillGridStyle()

                ''first fill the criteria combo with the DATE, TEST, RESULT criteria
                cmbCriteria.Items.Clear()
                cmbCriteria.Items.Add("Description")
                cmbCriteria.Items.Add("Date")
                '           If cmbCriteria.Items.Count > 0 Then
                cmbCriteria.SelectedIndex = 0
                'End If
                'initially first time the datesearch panel will be false, when the user select SearchBy : Date then only the pnlDateSearch will be visible
                pnlDateSearch.Visible = False
                FillICDCPTMOD(pID)

            Case 6
                'SelectedPatientDetail = PatientDetails.Orders
                'ShowPatientDetails()
                'FillCategoryTestGroups()  '' COMMENT BY SUDHIR 20090601 '' SHOWN AS PATINET DETAILS OF DASHBOARD. ''

                FillPatientOrders()

            Case 5
                GloUC_TransactionHistory1.LoadPreviousLabs(pID, DateTime.Now) '.ToString("MM/dd/yyyy hh:mm:ss"))
                GloUC_TransactionHistory1.DesignTestGrid()
                GloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                GloUC_TransactionHistory1.cmbCriteria.Text = "Date"
                GloUC_TransactionHistory1.HideCloseButton = False
            Case 7 'Tab ImagingST

                FillImagingST(pID)

            Case 8 'Tab Implant load with CardiologyDevice data

                ' '' ''FillCardiologyDevice(pID)

                FillImplantTab(pID)

            Case 9 'Tab Ejection Fraction load with Ejection Fraction data

                FillEjectionFraction(pID)



        End Select
    End Sub


    Private Sub cmbCriteria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCriteria.SelectedIndexChanged
        Try
            If cmbCriteria.Text = "Date" Then
                Label164.Visible = False  ''search box hide for bugid Bug #70929 
                Panel19.Visible = False
                pnlDateSearch.Visible = True
                txtsearchProcedures.Text = ""
                txtsearchProcedures.ReadOnly = True

                blnProcDate = True

                C1Dignosis.Rows.Count = 1
                FillICDCPTMODForSearch(pID, txtsearchProcedures.Text, blnProcDate, dtpFromDate.Text, dtpToDate.Text)

            Else
                Label164.Visible = True
                Panel19.Visible = True
                pnlDateSearch.Visible = False
                txtsearchProcedures.ReadOnly = False
                blnProcDate = False

                C1Dignosis.Rows.Count = 1
                FillICDCPTMODForSearch(pID, txtsearchProcedures.Text, blnProcDate)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub ShowPastExam(ByVal ExamID As Long, ByVal PatientId As Int64, ByVal VisitID As Long, ByVal DOS As String, ByVal ExamName As String, ByVal blnFinished As Boolean, Optional ByVal PatientCode As String = "")
        Try

            'If ShowDefaultPatientDetails() = False Then
            '    ShowDefaultPatientDetails(pID)
            'End If
            ' 

            ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010
            If MainMenu.IsAccess(False, PatientId) = False Then
                Exit Sub
            End If
            ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010

            '''''<><><><><> Check Patient Status <><><><><><>''''
            ''If CheckPatientStatus(PatientId) = False Then
            ''    Exit Sub
            ''End If
            '''''<><><><><> Check Patient Status <><><><><><>''''

            If Not blnFinished Then
                Dim objExam As New clsPatientExams
                objExam.SetProviderExam(ExamID)
                objExam.Dispose()
                objExam = Nothing
            End If

            Me.Cursor = Cursors.WaitCursor

            Dim frm As New frmPatientExam(pID, VisitID)
            '''''''''''''''' 
            AddHandler frm.FormClosed, AddressOf OnPatientExam_Close
            '''''''''''''''' 
            frm.Hide()
            frm.blnModify = True
            frm.Text = "Past Exams"
            Dim sender As Object = Nothing
            Dim e As System.EventArgs = Nothing
            frm.cmdPastExam_Click(sender, e)
            frm.PatientID = pID
            ''Added on 20100814 by sanjog for Template Name 
            frm.TemplateName = _TemplateName
            'frm.TemplateID = _TemplateID
            ''Added on 20100814 by sanjog for Template Name

            frm.pnlPastExam.Visible = True
            ''Added On 20100806 by sanjog for Exam form show
            If frm.OpenPastExam(ExamID, VisitID, Convert.ToDateTime(DOS), ExamName.Trim, blnFinished) = True Then

                frm.MdiParent = Me.ParentForm.MdiParent
                frm.IsPastExam = True
                CType(Me.ParentForm.MdiParent, MainMenu).pnlMainToolBar.Visible = False
                frm.Show()
                If frm.ExamViewMode Then
                    frm.ViewExam(ExamID)
                Else
                    frm.OpenPastExamContents(ExamID, blnFinished)
                End If
            Else
                'frm.Close()
                RemoveHandler frm.FormClosed, AddressOf OnPatientExam_Close
                frm.Dispose()
                frm = Nothing
            End If
            ''Added On 20100806 by sanjog for Exam form show

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Other, "Exam Opened.", gstrLoginName, gstrClientMachineName, pID, True)
            'objAudit = Nothing

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SysnopsisScreen, gloAuditTrail.ActivityType.View, "Exam Opened.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, "Exam Opened.", PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub OnPatientExam_Close(ByVal sender As Object, ByVal e As FormClosedEventArgs)

        Dim frm As frmPatientExam = Nothing

        Try
            frm = DirectCast(sender, frmPatientExam)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf OnPatientExam_Close
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try


        Try


            Fill_PastExams()
            '''''''''''''''''''' by Ujwala Atre as on 11232010
            Dim dsdata As DataSet
            dsdata = c2.PopulateSynopsisData(pID, "All")
            PopulateProblemList1(dsdata.Tables("Problems"))
            PopulateLatestMedications(dsdata.Tables("Medications"))
            PopulateProcedures1(dsdata.Tables("Procedures"))
            '  PopulateRadiology(dsdata.Tables("OrderTemplates"))
            PopulateLatestHistory(dsdata.Tables("History"))
            PopulateLabs(dsdata.Tables("Labs"))
            PopulateImaging(dsdata.Tables("Imaging"))
            PopulateImplant(dsdata.Tables("Implant"))
            '''''''''''''''''''' by Ujwala Atre as on 11232010
        Catch ex As Exception

        End Try
    End Sub

    Private Sub tbExamDMS_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbExamDMS.SelectedIndexChanged
        Try
            Select Case tbExamDMS.SelectedIndex
                Case 0
                    blnIsExam = True
                    ResetExamFilterControls()
                    _Defaultload = True
                    Fill_PastExams()
                Case 1
                    blnIsExam = False
                    Fill_PatientSacnedDocuments(pID)
            End Select


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtSearchCriteria_SearchFired() Handles txtSearchCriteria.SearchFired
        Try
            If txtSearchCriteria.Text.Trim.Length = 0 Then
                Fill_PatientSacnedDocuments(pID)
            Else
                SearchDMS()
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched DMS having substring " & txtSearchCriteria.Text.Trim, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search DMS having substring " & txtSearchCriteria.Text.Trim, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub txtSearchCriteria_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchCriteria.TextChanged

    End Sub

    Private Sub SearchExams() ' As DataTable
        Dim conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim strSQL As String = ""
        Dim dtPatientdetails As DataTable = Nothing
        Try
            conn = New SqlConnection
            conn.ConnectionString = GetConnectionString()
            conn.Open()
            cmd = New SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            strSQL = " SELECT     PatientExams.nExamID, PatientExams.nVisitID, PatientExams.dtDOS AS DOS, PatientExams.sExamName AS [Exam Name],PatientExams.sTemplateName AS [Template Name],      " _
          & " CASE PatientExams.bIsFinished WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END AS Finished, ISNULL(Provider_MST.sFirstName, '')     " _
          & " + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then  Provider_MST.sMiddleName + ' ' END + ISNULL(Provider_MST.sLastName, '') AS ProviderName, ISNULL(Provider_MST_1.sFirstName, '') " _
          & " + ' ' + ISNULL(Provider_MST_1.sMiddleName, '') + ' ' + ISNULL(Provider_MST_1.sLastName, '') + ' ' + ISNULL(' On ' + convert(varchar,PatientExams.dtReviewDate,100),'') AS ReviewedBy,sTemplateSpecility as Specialty   FROM   PatientExams INNER JOIN " _
          & " Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID left OUTER JOIN " _
          & " Provider_MST AS Provider_MST_1 ON PatientExams.nReviewerID = Provider_MST_1.nProviderID " _
          & " WHERE     (PatientExams.nPatientID = '" & pID & "') and PatientExams.sExamName like '%" & txtExamName.Text & "%' ORDER BY DOS DESC, PatientExams.nExamID DESC  "
            cmd.CommandText = strSQL
            dtPatientdetails = New DataTable
            dtPatientdetails.Load(cmd.ExecuteReader)


            dtPatientdetails.Columns.Add("RoleOfProvider", GetType(String))
            dtPatientdetails.AcceptChanges()

            Dim strRoles As String = ""
            Dim nExamID As Long = 0


            C1PatientExam.DataSource = dtPatientdetails
            C1PatientExam.Cols("nExamID").Visible = False
            C1PatientExam.Cols("nVisitID").Visible = False
            C1PatientExam.ShowCellLabels = True

            Dim _width As Integer = C1PatientExam.Width

            C1PatientExam.Cols("nExamID").Width = 0
            C1PatientExam.Cols("nVisitID").Width = 0
            C1PatientExam.Cols("DOS").Width = _width * 0.1
            C1PatientExam.Cols("Exam Name").Width = _width * 0.3
            C1PatientExam.Cols("ReviewedBy").Width = _width * 0.1
            C1PatientExam.Cols("ProviderName").Width = _width * 0.2
            C1PatientExam.Cols("Finished").Width = _width * 0.1
            C1PatientExam.Cols("RoleOfProvider").Width = _width * 0.18


            C1PatientExam.AllowEditing = True
            C1PatientExam.Cols(0).AllowEditing = False
            C1PatientExam.Cols(1).AllowEditing = False
            C1PatientExam.Cols(2).AllowEditing = False
            C1PatientExam.Cols(3).AllowEditing = False
            C1PatientExam.Cols(4).AllowEditing = False
            C1PatientExam.Cols(5).AllowEditing = False
            C1PatientExam.Cols(6).AllowEditing = False
            C1PatientExam.Cols(7).AllowEditing = True
            C1PatientExam.Cols(8).AllowEditing = False
            C1PatientExam.Cols("ReviewedBy").Caption = "Reviewed by"
            C1PatientExam.Cols("ProviderName").Caption = "Provider Name"
            C1PatientExam.Cols("RoleOfProvider").Caption = "Role of Provider"

            ''Set combolist for each row in column 'Role of Provider' 
            For k As Integer = 0 To dtPatientdetails.Rows.Count - 1
                nExamID = dtPatientdetails.Rows(k)("nExamID")
                strRoles = GetRoles(nExamID)
                Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                Try
                    If (C1PatientExam.Styles.Contains("CS_Roles" & k)) Then
                        cStyle = C1PatientExam.Styles("CS_Roles" & k)
                    Else
                        cStyle = C1PatientExam.Styles.Add("CS_Roles" & k) ''style new for every row
                    End If
                Catch ex As Exception
                    cStyle = C1PatientExam.Styles.Add("CS_Roles" & k) ''style new for every row
                End Try

                Dim rgRole As C1.Win.C1FlexGrid.CellRange = C1PatientExam.GetCellRange(k + 1, 7)

                C1PatientExam.ShowButtons = ShowButtonsEnum.Always
                cStyle.ComboList = strRoles
                rgRole.Style = cStyle

                C1PatientExam.SetCellStyle(k + 1, 7, cStyle)
            Next
            C1PatientExam.Cols.Move(8, 5)

            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Past Exam viewed from DashBoard", gstrLoginName, gstrClientMachineName, pID)
            'Return dtPatientdetails
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If (IsNothing(conn) = False) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
            If (IsNothing(cmd) = False) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If (IsNothing(dtPatientdetails) = False) Then

            '    dtPatientdetails.Dispose()
            '    dtPatientdetails = Nothing
            'End If

        End Try
    End Sub

    Private Sub SearchDMS()
        Dim oCategories As gloEDocumentV3.Common.Categories



        Dim otable As DataTable = Nothing
        Try

            '' 'Design Category Grid

            DesignCategorisedDocument(C1PatientDMS)
            With C1PatientDMS
                Dim searchcriteria As String = txtSearchCriteria.Text.Trim
                Dim oList As New gloEDocumentV3.eDocManager.eDocGetList()
                oCategories = oList.GetCategories(gClinicID)
                oList.Dispose()
                oList = Nothing
                If Not oCategories Is Nothing Then
                    For i As Int16 = 0 To oCategories.Count - 1

                        Dim categoryname As String = oCategories(i).CategoryName
                        Dim clinicid As Int64 = gClinicID
                        Dim conn As SqlConnection = Nothing
                        Dim cmd As SqlCommand = Nothing
                        Dim strSQL As String = ""
                        Dim NTAOType As Int16 = gloEDocumentV3.Enumeration.enum_NTAOType.None
                        If txtSearchCriteria.Text.Trim <> "" Then
                            Try
                                conn = New SqlConnection
                                conn.ConnectionString = GetDMSConnectionString() 'SET CONNECTION STRING FOR DMS DATABASE TO RESOLVED ISSUE 13648
                                conn.Open()
                                cmd = New SqlCommand
                                cmd.CommandType = CommandType.Text
                                Select Case cmbSearch.Text
                                    Case "Notes"
                                        NTAOType = gloEDocumentV3.Enumeration.enum_NTAOType.Notes
                                        '    strSQL = "SELECT ed.eContainerID,ed.eDocumentID,ed.DocumentName,ed.Category," _
                                        '            & " ed.PatientID,Year,Month,ed.PageCounts,ed.CreatedDateTime,ed.IsAcknowledge,ed.HasNote," _
                                        '            & " ed.ClinicID FROM eDocument_Details ed inner join eDocument_Notes edn " _
                                        '            & " on ed.eDocumentID=edn.eDocumentID " _
                                        '            & " WHERE ed.PatientID = " & pID & " and ed.category='" & categoryname & "' and ed.clinicid= " & clinicid & " AND ed.eContainerID <> 0 AND ed.eDocumentID <> 0 AND " _
                                        '            & " ed.DocumentName IS NOT NULL AND ed.Category IS NOT NULL " _
                                        '            & " AND ed.PatientID IS NOT NULL AND Year IS NOT NULL AND Month IS NOT NULL AND ed.PageCounts IS NOT NULL " _
                                        '            & " and edn.notedescription like '%" & searchcriteria & "%'" _
                                        '            & " ORDER BY Year,Month,ed.DocumentName,ed.CreatedDateTime desc"
                                    Case "User Tag"
                                        NTAOType = gloEDocumentV3.Enumeration.enum_NTAOType.Tag
                                        'strSQL = "SELECT ed.eContainerID,ed.eDocumentID,ed.DocumentName,ed.Category," _
                                        '        & " ed.PatientID,Year,Month,ed.PageCounts,ed.CreatedDateTime,ed.IsAcknowledge,ed.HasNote," _
                                        '        & " ed.ClinicID FROM eDocument_Details ed inner join eDocument_UserTags edn " _
                                        '        & " on ed.eDocumentID=edn.eDocumentID " _
                                        '        & " WHERE ed.PatientID =  " & pID & " and ed.category='" & categoryname & "' and ed.clinicid= " & clinicid & " AND ed.eContainerID <> 0 AND ed.eDocumentID <> 0 AND " _
                                        '        & " ed.DocumentName IS NOT NULL AND ed.Category IS NOT NULL " _
                                        '        & " AND ed.PatientID IS NOT NULL AND Year IS NOT NULL AND Month IS NOT NULL AND ed.PageCounts IS NOT NULL " _
                                        '        & " and edn.UserTagDescription like '%" & searchcriteria & "%'" _
                                        '        & " ORDER BY Year,Month,ed.DocumentName,ed.CreatedDateTime desc"
                                    Case "Acknowledge"
                                        NTAOType = gloEDocumentV3.Enumeration.enum_NTAOType.Acknowledge
                                        'strSQL = "SELECT ed.eContainerID,ed.eDocumentID,ed.DocumentName,ed.Category," _
                                        '       & " ed.PatientID,Year,Month,ed.PageCounts,ed.CreatedDateTime,ed.IsAcknowledge,ed.HasNote," _
                                        '       & " ed.ClinicID FROM eDocument_Details ed inner join eDocument_Acknowledge edn " _
                                        '       & " on ed.eDocumentID=edn.eDocumentID " _
                                        '       & " WHERE ed.PatientID =  " & pID & " and ed.category='" & categoryname & "' and ed.clinicid= " & clinicid & " AND ed.eContainerID <> 0 AND ed.eDocumentID <> 0 AND " _
                                        '       & " ed.DocumentName IS NOT NULL AND ed.Category IS NOT NULL " _
                                        '       & " AND ed.PatientID IS NOT NULL AND Year IS NOT NULL AND Month IS NOT NULL AND ed.PageCounts IS NOT NULL " _
                                        '       & " and edn.AcknowledgeDescription like '%" & searchcriteria & "%'" _
                                        '       & " ORDER BY Year,Month,ed.DocumentName,ed.CreatedDateTime desc"
                                End Select

                                strSQL = " SELECT distinct ISNULL(eDocument_NTAO_V3.eContainerID,0) As eContainerID , ISNULL(eDocument_NTAO_V3.eDocumentID,0) AS eDocumentID, ISNULL(eDocument_Details_V3.DocumentName,'') As DocumentName, ISNULL(eDocument_Details_V3.CategoryID,0) AS CategoryID, " _
                                        & " ISNULL(eDocument_Details_V3.Category,'') AS CategoryName , ISNULL(eDocument_Details_V3.PatientID,0) AS PatientID, ISNULL(eDocument_Details_V3.Year,'') AS Year, ISNULL(eDocument_Details_V3.Month,'') AS Month, " _
                                        & " ISNULL(eDocument_Details_V3.PageCounts,0) AS PageCounts,eDocument_Details_V3.CreatedDateTime AS CreatedDateTime, ISNULL(eDocument_Details_V3.IsAcknowledge,0) AS  IsAcknowledge, " _
                                        & " ISNULL(eDocument_Details_V3.HasNote,0) AS HasNote , ISNULL(eDocument_Details_V3.ClinicID,1) AS ClinicID  " _
                                        & " FROM eDocument_NTAO_V3 INNER JOIN " _
                                        & " eDocument_Details_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Details_V3.eDocumentID " _
                                        & " Where ISNULL(eDocument_NTAO_V3.NTAOType,0) =" & NTAOType & " AND ISNULL(eDocument_Details_V3.PatientID,0) = " & pID & " AND  ISNULL(eDocument_Details_V3.Category,'') = '" & categoryname & "' " _
                                        & " AND NTAODescription Like '%" & searchcriteria & "%' " _
                                        & " ORDER BY Year,Month,DocumentName,CreatedDateTime desc "

                                cmd.Connection = conn
                                cmd.CommandText = strSQL
                                otable = New DataTable
                                otable.Load(cmd.ExecuteReader)
                            Catch ex As Exception
                            Finally
                                If (IsNothing(conn) = False) Then
                                    conn.Close()
                                    conn.Dispose()
                                    conn = Nothing
                                End If
                                If (IsNothing(cmd) = False) Then
                                    cmd.Parameters.Clear()
                                    cmd.Dispose()
                                    cmd = Nothing
                                End If
                            End Try

                            If Not otable Is Nothing Then
                                For k As Int16 = 0 To otable.Rows.Count - 1

                                    .Rows.Add()
                                    .Cols(COL_View_DocumentName).TextAlign = TextAlignEnum.LeftCenter
                                    Dim rgStyle As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, COL_View_DocumentName, .Rows.Count - 1, COL_View_DocumentName)
                                    rgStyle.StyleNew.DataType = GetType(String)
                                    .SetData(.Rows.Count - 1, COL_View_DocumentName, otable.Rows(k)("DocumentName").ToString)
                                    .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEMACHINE, "")  ' Source Machine
                                    .SetData(.Rows.Count - 1, COL_D_CAT_SYSTEMFOLDER, "")    ' System Folder
                                    .SetData(.Rows.Count - 1, COL_D_CAT_CONTAINER, otable.Rows(k)("eContainerID"))          ' Container
                                    .SetData(.Rows.Count - 1, COL_View_Category, otable.Rows(k)("CategoryName"))            ' Category
                                    .SetData(.Rows.Count - 1, COL_D_CAT_PATIENTID, otable.Rows(k)("PatientID"))          ' Patient ID
                                    .SetData(.Rows.Count - 1, COL_D_CAT_YEAR, otable.Rows(k)("Year"))                    ' Year
                                    .SetData(.Rows.Count - 1, COL_D_CAT_MONTH, otable.Rows(k)("Month"))                  ' Month
                                    .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEBIN, "")          ' Source Bin
                                    .SetData(.Rows.Count - 1, COL_D_CAT_INUSED, "")                ' In Used
                                    .SetData(.Rows.Count - 1, COL_D_CAT_USEDMACHINE, "")      ' Used Machine
                                    .SetData(.Rows.Count - 1, COL_D_CAT_USEDTYPE, "")            ' Used Type
                                    .SetData(.Rows.Count - 1, COL_D_CAT_PATH, "")                    ' Path
                                    .SetData(.Rows.Count - 1, COL_D_CAT_COLTYPE, CType(enumColType.Document, Integer))
                                    .SetData(.Rows.Count - 1, COL_D_CAT_FILENAME, CType(otable.Rows(k)("eDocumentID"), Int64)) '' DocumentID
                                    .SetData(.Rows.Count - 1, COL_D_CAT_MACHINEID, "")
                                    .SetData(.Rows.Count - 1, COL_D_CAT_VERSIONNO, "")

                                    If otable.Rows(k)("HasNote") = True Then
                                        .SetCellImage(.Rows.Count - 1, COL_View_NOTEFLAG, Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                    End If
                                    If otable.Rows(k)("IsAcknowledge") = True Then
                                        .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 1)
                                        '  .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File")
                                        .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, Img_Reviwed.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                        .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                                    Else
                                        .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 0)
                                        ' .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File_UnReviwed")
                                        .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, Img_Blanck.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                        .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                                    End If
                                    'nmonths = nmonths + 1
                                Next
                                otable.Dispose()
                                otable = Nothing
                            End If
                        Else

                            Fill_PatientSacnedDocuments(pID)
                        End If


                    Next
                End If
                If (IsNothing(oCategories) = False) Then
                    oCategories.Dispose()
                    oCategories = Nothing
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub oSearchAllergiesCtl_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles oSearchAllergiesCtl.AfterTextSearch
        Try
            If (IsNothing(dv) = False) Then
                Fill_History(dv.ToTable())
            End If

            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched History having substring" & oSearchAllergiesCtl.SearchString, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search History having substring" & oSearchAllergiesCtl.SearchString, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")

        End Try
    End Sub

    Private Sub oSearchImagingCtl_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles oSearchImagingCtl.AfterTextSearch
        Try

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub oSearchMedicationsCtl_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles oSearchMedicationsCtl.AfterTextSearch
        Try
            If (IsNothing(dv) = False) Then
                Fill_Medications(dv.ToTable())
            End If

            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Medications having substring" & oSearchMedicationsCtl.SearchString, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Medications having substring" & oSearchMedicationsCtl.SearchString, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub oSearchProblemListCtl_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles oSearchProblemListCtl.AfterTextSearch
        Try
            If (IsNothing(dv) = False) Then
                Fill_ProblemList(dv.ToTable())
            End If

            '  gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Problem list having substring" & oSearchProblemListCtl.SearchString, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Problem list having substring" & oSearchProblemListCtl.SearchString, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub oSearchProceduresCtl_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles oSearchProceduresCtl.AfterTextSearch
        Try

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    '' COMMENT BY SUDHIR 20090601 '' 
    'Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    '    Try

    '        Dim strSearch As String
    '        With txtSearch
    '            If Trim(.Text) <> "" Then
    '                strSearch = Replace(.Text, "'", "''")
    '            Else
    '                strSearch = ""
    '            End If
    '        End With

    '        With C1OrderDetails
    '            .Row = .FindRow(strSearch, 1, COLUM_NAME, False, False, True)
    '            If .Row > 0 Then
    '                Exit Sub
    '            End If

    '            '' 20070921 - Mahesh - InString Search 
    '            Dim strNode As String = ""
    '            For i As Int16 = 1 To .Rows.Count - 1
    '                strNode = ""
    '                strNode = UCase(.GetData(i, COLUM_NAME).ToString.Trim)
    '                If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
    '                    .Row = i
    '                    Exit Sub
    '                End If
    '            Next
    '            '' ---
    '            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Orders having substring" & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
    '        End With


    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SysnopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Orders having substring" & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try

            Dim strSearch As String
            With txtSearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            C1OrderDetails.Row = C1OrderDetails.FindRow(strSearch, 1, 3, False, False, True)
            If C1OrderDetails.Row > 0 Then
                Exit Sub
            End If

            '' 20070921 - Mahesh - InString Search 
            Dim strNode As String = ""
            For i As Int16 = 1 To C1OrderDetails.Rows.Count - 1
                strNode = ""
                strNode = UCase(C1OrderDetails.GetData(i, 3).ToString.Trim)
                If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                    C1OrderDetails.Row = i
                    Exit Sub
                End If
            Next
            '' ---
            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Orders having substring" & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Orders having substring" & txtSearch.Text.Trim, gstrLoginName, gstrClientMachineName, gnPatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub C1PatientDMS_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1PatientDMS.DoubleClick
        RaiseEvent ViewDMS()
    End Sub

    Private Sub C1PatientExam_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1PatientExam.DoubleClick
        Dim nPastExamID As Long
        Dim nVisitID As Long
        Dim dtDOS As DateTime
        Dim strExamName As String
        'Dim bIsReviewed As Boolean




        If C1PatientExam.Rows.Count > 1 Then
            ''Dim em As System.Windows.Forms.MouseEventArgs
            nPastExamID = CType(C1PatientExam.GetData(C1PatientExam.Row, "nExamID"), Int64)
            nVisitID = CType(C1PatientExam.GetData(C1PatientExam.Row, "nVisitID"), Int64)
            dtDOS = CType(C1PatientExam.GetData(C1PatientExam.Row, "DOS"), Date)
            strExamName = CType(C1PatientExam.GetData(C1PatientExam.Row, "Exam Name"), String)
            ''Added on 20100813 by sanjog
            _TemplateName = CType(C1PatientExam.GetData(C1PatientExam.Row, "Template Name"), String)
            ''Added on 20100813 by sanjog
            Dim blnFinished As Boolean
            If CType(C1PatientExam.GetData(C1PatientExam.Row, "Finished"), String) = "Yes" Then
                blnFinished = True
            Else
                blnFinished = False
            End If

            ShowPastExam(nPastExamID, pID, nVisitID, dtDOS, strExamName, blnFinished, strPatientCode)
        End If

    End Sub

    Private Sub C1PatientExam_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1PatientExam.MouseDown
        'Try
        '    If e.Button = Windows.Forms.MouseButtons.Right Then
        '        Dim oMenuStrip As New ContextMenuStrip
        '        Dim oMenuItem As ToolStripItem

        '        Dim nRow As Integer
        '        nRow = C1PatientExam.HitTest(e.X, e.Y).Row
        '        If nRow >= 0 Then
        '            oMenuItem = New ToolStripMenuItem


        '            oMenuItem.Text = "New Exam"
        '            oMenuItem.Tag = C1PatientExam.Item(nRow, 0)
        '            oMenuStrip.Items.Add(oMenuItem)
        '            C1PatientExam.ContextMenuStrip = oMenuStrip
        '            C1PatientExam.Select(nRow, 0)
        '            AddHandler oMenuItem.Click, AddressOf OpenNewExam
        '        End If
        '    End If




        'Catch ex As Exception

        'End Try

        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                'Dim nRow As Integer
                'Dim nCol As Integer
                'nRow = C1PatientExam.HitTest(e.X, e.Y).Row
                'nCol = C1PatientExam.HitTest(e.X, e.Y).Column
                '  If nRow >= 0 And nCol >= 0 Then

                'Developer:Sanjog Dhamke
                'Date: 4 April 2012
                'Bug PRD Name:Copy Exam Functionality
                'Reason: To implement new functionality
                cMnuPatient.Items.Clear()
                If e.Button = MouseButtons.Right Then
                    Dim _nRow As Integer = C1PatientExam.HitTest(e.X, e.Y).Row
                    If _nRow >= 0 Then
                        C1PatientExam.Row = _nRow
                    End If
                End If
                'End - Sanjog

                Call FillMenus()
                'Try
                '    If (IsNothing(C1PatientExam.ContextMenuStrip) = False) Then
                '        C1PatientExam.ContextMenuStrip.Dispose()
                '        C1PatientExam.ContextMenuStrip = Nothing

                '    End If
                'Catch ex As Exception

                'End Try

                C1PatientExam.ContextMenuStrip = cMnuPatient
                ' cmnuPatient_NewExam.me()
                ''Method added by Sandip Darade 
                ''To fill check in check out menu
                ' FillCheckInCheckOutMenu()
                'Else
                'C1PatientExam.ContextMenuStrip = Nothing
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub FillMenus()
        Try

            Dim dt As DataTable = Nothing
            Dim i As Integer
            '  Dim oclsPatientDetails As New clsPatientDetails
            Dim oMenuItem As ToolStripMenuItem
            Dim oChildItem As ToolStripMenuItem

            'sarika
            Dim oNextChildItem As ToolStripMenuItem
            '--


            cMnuPatient.Items.Clear()




            'sarika added code on 20081201 for Provider speccific exam templates
            Dim clsPatDet As New clsPatientDetails


            'fill Providers
            Dim dtProviders As DataTable
            'oMenuItem = New ToolStripMenuItem
            dtProviders = clsPatDet.Fill_Providers()

            If Not IsNothing(dtProviders) Then


                'Exam Main Menu
                oMenuItem = New ToolStripMenuItem
                With oMenuItem
                    .Text = "New Exam"
                    .Text = "New Exam"
                    ' .Image = imgList_Common.Images(9)
                    '.Shortcut = Shortcut.CtrlE
                    '.ShowShortcut = False
                End With
                'cMnuPatient.Items.Add(oMenuItem)
                '   oMenuItem.DropDownItems.Add()

                'add providers

                For p As Integer = 0 To dtProviders.Rows.Count - 1
                  
                    oChildItem = New ToolStripMenuItem
                    With oChildItem
                        .Text = Trim(dtProviders.Rows(p).Item(1))
                        .Tag = Trim(dtProviders.Rows(p).Item(0)) ' enmContextMenu.ProviderExam

                        ' .Name = Trim(dtProviders.Rows(p).Item(0))
                        '.Shortcut = Shortcut.CtrlShiftP
                        '.ShowShortcut = False
                    End With
                    dt = clsPatDet.Fill_ProviderTemplates(Trim(dtProviders.Rows(p).Item(0)))
                    If (IsNothing(dt) = False) Then


                        If dt.Rows.Count > 0 Then
                          
                            For i = 0 To dt.Rows.Count - 1
                                oNextChildItem = New ToolStripMenuItem
                                With oNextChildItem
                                    .Text = Trim(dt.Rows(i).Item(1))
                                    .Tag = "New Exam" & "-" & dt.Rows(i).Item("nTemplateID")
                                    ' .Name = Trim(dtProviders.Rows(p).Item(0))
                                    '     .Shortcut = Shortcut.CtrlShiftE
                                    '   .ShowShortcut = False
                                End With
                                ' AddHandler oChildItem.Click, AddressOf SetMenus
                                ' oMenuItem.MenuItems.Add(oChildItem)
                                AddHandler oNextChildItem.Click, AddressOf SetMenus
                                oChildItem.DropDownItems.Add(oNextChildItem)
                                oNextChildItem = Nothing
                            Next
                        End If 'dt.Rows.Count > 0 

                        dt.Dispose()
                        dt = Nothing
                    End If

                    ' AddHandler oChildItem.Click, AddressOf SetMenus
                    oMenuItem.DropDownItems.Add(oChildItem)
                    oChildItem = Nothing
                Next

              

                oChildItem = New ToolStripMenuItem
                With oChildItem
                    .Text = "All"
                    .Tag = 0 ' enmContextMenu.ProviderExam
                    ' .Name = Trim(dtProviders.Rows(p).Item(0))
                    '   .Shortcut = Shortcut.CtrlShiftP
                    '   .ShowShortcut = False
                End With

                dt = clsPatDet.Fill_Templates()
                If (IsNothing(dt) = False) Then


                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            oNextChildItem = New ToolStripMenuItem
                            With oNextChildItem
                                .Text = Trim(dt.Rows(i).Item(1))
                                '.Tag = "New Exam"
                                .Tag = "New Exam" & "-" & dt.Rows(i).Item("nTemplateID")
                                ' .Name = Trim(dtProviders.Rows(p).Item(0))
                                '  .Shortcut = Shortcut.CtrlShiftE
                                '  .ShowShortcut = False
                            End With
                            ' AddHandler oChildItem.Click, AddressOf SetMenus
                            ' oMenuItem.MenuItems.Add(oChildItem)
                            AddHandler oNextChildItem.Click, AddressOf SetMenus
                            oChildItem.DropDownItems.Add(oNextChildItem)
                            oNextChildItem = Nothing
                        Next
                    End If 'dt.Rows.Count > 0 
                    dt.Dispose()
                    dt = Nothing
                End If

                oMenuItem.DropDownItems.Add(oChildItem)
                oChildItem = Nothing


                ''Exam Details
                'For i = 0 To dt.Rows.Count - 1
                '    oChildItem = New MenuItem
                '    With oChildItem
                '        .Text = Trim(dt.Rows(i).Item(1))
                '        .Tag = enmContextMenu.NewExam
                '        .Shortcut = Shortcut.CtrlShiftE
                '        .ShowShortcut = False
                '    End With
                '    AddHandler oChildItem.Click, AddressOf SetMenus
                '    oMenuItem.MenuItems.Add(oChildItem)
                '    oChildItem = Nothing
                'Next
                cMnuPatient.Items.Add(oMenuItem)
                '  oMenuItem = Nothing

                'Developer:Sanjog Dhamke
                'Date: 4 April 2012
                'Bug PRD Name:Copy Exam Functionality
                'Reason: To implement new functionality

                'Added by Amit - 7020 Enable Copy Exam if setting is 1 then showing "Copy Exam" menu
                Dim value As New Object()
                Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
                ogloSettings.GetSetting("ENABLECOPYEXAM", 0, gnClinicID, value)
                ogloSettings.Dispose()
                ogloSettings = Nothing

                If value = "1" Then
                    If C1PatientExam.Rows.Count > 1 Then
                        oMenuItem = New ToolStripMenuItem
                        oMenuItem.Text = "Copy Exam"
                        AddHandler oMenuItem.Click, AddressOf CopyPastExam
                        cMnuPatient.Items.Add(oMenuItem)
                    End If
                End If
                value = Nothing
                dtProviders.Dispose()
                dtProviders = Nothing
            End If ' If Not IsNothing(dtProviders) Then
            clsPatDet.Dispose()
            clsPatDet = Nothing


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private _TemplateName As String = ""
    Dim _TemplateID As Int64 = 0
    Public Sub SetMenus(ByVal sender As Object, ByVal e As EventArgs)
        Dim oCurrentMenu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

        Try
            Dim a As Array = Nothing
            If (oCurrentMenu.Tag.ToString().Contains("New Exam")) Then
                a = oCurrentMenu.Tag.ToString.Split("-")
                oCurrentMenu.Tag = "New Exam"
            End If
            Select Case oCurrentMenu.Tag.ToString()
                Case "New Exam"
                    _TemplateName = oCurrentMenu.Text
                    If (a.Length >= 2) Then
                        _TemplateID = a(1)
                    End If
                    Call NewExam()
                    _TemplateName = ""
                    _TemplateID = 0
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString)

        End Try

    End Sub
#Region " Exam "
    Private Sub NewExam()
        Try
            ' UpdateLog("picnewExam click start")
            If Trim(strPatientFirstName) <> "" Then


                ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010
                If MainMenu.IsAccess(False, pID) = False Then
                    Exit Sub
                End If
                ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                ' ''If CheckPatientStatus(pID) = False Then ''gnPatientID replaced by pID
                ' ''    Exit Sub
                ' ''End If
                Dim strMessage As String = ""
                Dim Result As DialogResult
                Dim objExam As New clsPatientExams

                '' 

                If gnLoginProviderID <> 0 Then
                    Dim objProvider As New clsProvider
                    Dim nPatientProvider As Int64
                    Dim strPatientProviderName As String
                    nPatientProvider = objProvider.GetPatientProvider(pID) ''gnPatientID replaced by pID
                    If nPatientProvider <> 0 Then
                        strPatientProviderName = objExam.GetProvidernameforExam(nPatientProvider)
                    Else
                        strPatientProviderName = ""

                    End If

                    strMessage = GetPatientExamProviderMismatchMessage(strPatientProviderName) '"This patient belongs to '" & strPatientProviderName & "'. Do you want to change the Provider?"

                    If gnLoginProviderID <> nPatientProvider Then
                        Result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If Result = Windows.Forms.DialogResult.Yes Then
                            If objProvider.ChangePatientProvider(pID, gnLoginProviderID) = True Then 'gnPatientID replaced by pID
                                'Call Load_PatientControl()
                                ''DB Call ShowDefaultPatientDetails()
                            End If
                        ElseIf Result = Windows.Forms.DialogResult.No Then
                        ElseIf Result = Windows.Forms.DialogResult.Cancel Then
                            If IsNothing(objProvider) = False Then
                                objProvider.Dispose()
                                objProvider = Nothing
                            End If
                            If IsNothing(objExam) = False Then
                                objExam.Dispose()
                                objExam = Nothing
                            End If
                            Exit Sub
                        End If
                    End If
                    If IsNothing(objProvider) = False Then
                        objProvider.Dispose()
                        objProvider = Nothing
                    End If

                End If

                '''''<><><><><> Check Patient Status <><><><><><>''''
                Dim objResultExam As DataTable
                objResultExam = objExam.GetUnfinshedExams(pID) ''gnPatientID replaced by pID
                If Not objResultExam Is Nothing Then

                    strMessage = "Unfinished Exam(s) exists for the selected patient. Do you still want to open New Exam?" & vbNewLine & vbNewLine & vbTab & "YES - To Open New Exam " & vbNewLine & vbNewLine & vbTab & "NO  - To Open the latest Unfinished Exam"

                    Result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    If Result = Windows.Forms.DialogResult.Yes Then
                        OpenNewExam(True)
                    ElseIf Result = Windows.Forms.DialogResult.No Then
                        Dim nPastExamID As Long
                        Dim nVisitID As Long
                        Dim dtDOS As DateTime
                        Dim strExamName As String = String.Empty

                        ''Dim em As System.Windows.Forms.MouseEventArgs
                        If Not IsDBNull(objResultExam.Rows(0)("nExamID")) Then
                            nPastExamID = CType(objResultExam.Rows(0)("nExamID"), Int64)
                        End If
                        If Not IsDBNull(objResultExam.Rows(0)("nVisitID")) Then
                            nVisitID = CType(objResultExam.Rows(0)("nVisitID"), Int64)
                        End If
                        If Not IsDBNull(objResultExam.Rows(0)("dtDOS")) Then
                            dtDOS = CType(objResultExam.Rows(0)("dtDOS"), Date)
                        End If
                        If Not IsDBNull(objResultExam.Rows(0)("sExamName")) Then
                            strExamName = objResultExam.Rows(0)("sExamName").ToString
                        End If
                        ''Sanjog - Added on 2011 Feb 28 to show the previous Template name 
                        If Not IsDBNull(objResultExam.Rows(0)("sTemplateName")) Then
                            _TemplateName = objResultExam.Rows(0)("sTemplateName").ToString
                        End If
                        ''Sanjog - Added on 2011 Feb 28 to show the previous Template name 
                        ''Remove Comment on 20100812 by Sanjog to show Past Exam
                        ShowPastExam(nPastExamID, pID, nVisitID, dtDOS, strExamName, False) ''gnPatientID replaced by pID
                        ''Remove Comment on 20100812 by Sanjog to show Past Exam
                    End If
                    objResultExam.Dispose()
                    objResultExam = Nothing
                Else
                    OpenNewExam(True)
                End If

                If IsNothing(objExam) = False Then
                    objExam.Dispose()
                    objExam = Nothing
                End If
            Else

            End If


            Me.Cursor = Cursors.Default
        Catch ex As Exception
            '''' Show Tool Bar Mahesh 20070424
            'Me.pnlMainMenu.Visible = True
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub OpenNewExam(Optional ByVal SwitchOffSavingFlags As Boolean = False)
        ''Commented by Ravi on 01/18/2008 to meet CCHIT 2007 requirements as DM logic hase been refined
        'Dim clsDM_Template As New clsDM_Template
        'Dim oGuidelineResults As New GuidelineResults

        'oGuidelineResults = clsDM_Template.CheckForDueGuideline(gnPatientID, Now.Date, 0)

        'With oGuidelineResults
        '    If IsNothing(oGuidelineResults) = False Then
        '        If oGuidelineResults.GuidelineIs <> EnumGuidelineResult.None Then
        '            Dim strMessage As String = ""
        '            Dim Result As DialogResult
        '            If oGuidelineResults.GuidelineIs = EnumGuidelineResult.Due Then
        '                strMessage = "The Guidelines '" & .TemplateName & "' are due for this Patient since '" & .OnSetDate & "'. Do you want to give the guidelines now?"

        '            ElseIf oGuidelineResults.GuidelineIs = EnumGuidelineResult.OverDue Then
        '                strMessage = "The Guidelines '" & .TemplateName & "' are overdue for this Patient since '" & .OnSetDate & "'. Do you want to give the guidelines now?"
        '            End If

        '            Result = MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        '            If Result = DialogResult.Yes Then
        '                Dim oPatient As New Collection
        '                Dim oTemplate As New Collection

        '                oPatient.Add(gnPatientID)

        '                Dim lst As New myList
        '                lst.ID = .TemplateID
        '                lst.Description = .TemplateName
        '                oTemplate.Add(lst)

        '                Dim frmDue As New frmDM_DueTemplate(oPatient, oTemplate)

        '                With frmDue
        '                    .ShowInTaskbar = False
        '                    .MdiParent = Me
        '                    .WindowState = FormWindowState.Maximized
        '                    .Show()
        '                    Exit Sub
        '                End With

        '            ElseIf Result = DialogResult.No Then
        '                '''' Open Exams 
        '            ElseIf Result = DialogResult.Cancel Then
        '                Exit Sub
        '            End If

        '        End If

        '    End If

        'End With


        ''''' Temp Added By Mahesh For Screen Shots on Descise Management                
        '''' By Mahesh 2007024
        'If MessageBox.Show("The Guidelines are due for this Patient. Do you want to give the guidelines now?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
        '    Exit Sub
        'End If
        ''''''


        Windows.Forms.Cursor.Current = Cursors.WaitCursor


        Dim frm As New frmPatientExam(pID, _VisitID) ''gnPatientID replaced by pID

        AddHandler frm.FormClosed, AddressOf OnPatientExam_Close

        frm.Hide()
        '      UpdateLog("Filling Patient details")
        frm.IsNewExam = True
        frm.SwitchOffSavingFlags = SwitchOffSavingFlags
        frm.TemplateName = _TemplateName
        frm.TemplateID = _TemplateID
        frm.blnModify = False
        frm.Text = "New Exam"
        frm.PatientID = pID ''gnPatientID replaced by pID
        frm.MdiParent = Me.ParentForm.MdiParent 'oMDI
        frm.OpenExam()
        frm.Splitter2.Visible = True
        ' .MdiParent = Me.ParentForm.ParentForm
        ''''' History Alerts
        ' '' Check if History Of the Date is Exists or Not
        'If frm.getHistoryAfterDate(Now) = True Then
        '    '' If History of Patient is Not Entered then 
        '    '' Create History Alert
        '    'ShowPatientAlerts("History for this patient is not entered. Do you want to enter the History, Now?")
        '    'If MessageBox.Show("History for this patient is not entered. Do you want to enter the History, Now?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
        '    Dim Result As DialogResult

        '    Result = MessageBox.Show("History for this patient has not been entered. Do you want to enter the history now?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        '    'If MessageBox.Show("History for this patient has not been entered. Do you want to enter the history now?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
        '    If Result = DialogResult.Yes Then
        '        '''' Open History Form
        '        'Dim sender As Object
        '        'Dim e As System.EventArgs

        '        'mnuPatientHistory_Click(sender, e)
        '        'Exit Sub
        '        frm._blnOpenHistory = True
        '    End If
        'End If
        frm.Show()
        If SwitchOffSavingFlags Then
            frm.SaveExam(0, True)
        End If

        If frm._blnOpenHistory = True Then
            frm.SendToBack()
        End If


        ' ShowHideMainMenu(False, False)
        '''' Hide Tool Bar Mahesh 20070424
        ' Me.pnlMainToolBar.Visible = False

        '' pnlLeft.Visible = False
        'pnlRights.Visible = False
        ''Splitter1.Visible = False
        Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub PastExam()
        Try
            If Trim(strPatientFirstName) <> "" Then


                ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010
                If MainMenu.IsAccess(False, pID) = False Then
                    Exit Sub
                End If
                ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                ''If CheckPatientStatus(pID) = False Then ''gnPAtientID replaced by PiD
                ''    Exit Sub
                ''End If
                '''''<><><><><> Check Patient Status <><><><><><>''''

                Me.Cursor = Cursors.WaitCursor
                '''' Hide Tool Bar Mahesh 20070424
                ' Me.pnlMainToolBar.Visible = False

                Dim frm As New frmPatientExam(pID, _VisitID) ''gnPAtientID replaced by PiD
                '''''''''''''''' 
                AddHandler frm.FormClosed, AddressOf OnPatientExam_Close
                '''''''''''''''' 
                '''' Hide Tool Bar Mahesh 20070424
                ' Me.pnlMainToolBar.Visible = False

                frm.Hide()
                frm.blnModify = True
                frm.Text = "Past Exams"
                Dim sender As Object = Nothing
                Dim e As System.EventArgs = Nothing
                frm.IsPastExam = True
                frm.cmdPastExam_Click(sender, e)

                frm.PatientID = pID 'gnPAtientID replaced by PiD
                ''Added 0n 20100813 by sanjog 
                frm.TemplateName = _TemplateName
                ''Added 0n 20100813 by sanjog 
                frm.pnlPastExam.Visible = True
                ' frm.chkShowPreview.Visible = True
                '.wdPastExam.Visible = False
                '.pnlPastExamView.Visible = True
                frm.MdiParent = Me.ParentForm.MdiParent 'oMDI
                frm.Show()

                '  ShowHideMainMenu(False, False)
                If gblnVoiceEnabled = True AndAlso gblnSpeakerExists = True Then
                    Try
                        '' 20070613
                        'tlbbtn_Microphone.Visible = True
                        'tlbbtn_Microphone.Checked = False
                    Catch ex As Exception

                    End Try
                End If
                Me.Cursor = Cursors.Default
            Else
                '''''This IF statement is added by Anil on 31/10/2007,This message will appear if patients are there in the grid i.e., if grid is not blank
                ' If oPatientListControl.PatientCount > 0 Then
                Me.Cursor = Cursors.Default
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'End If
            End If
        Catch ex As Exception
            '  Me.pnlMainToolBar.Visible = True
            MsgBox(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

#End Region


    'Private Sub OpenNewExam(ByVal sender As Object, ByVal e As EventArgs)
    '    Try

    '        Dim nPastExamID As Long
    '        Dim nVisitID As Long
    '        Dim dtDOS As DateTime
    '        Dim strExamName As String
    '        Dim bIsReviewed As Boolean

    '        ''Dim em As System.Windows.Forms.MouseEventArgs
    '        nPastExamID = CType(C1PatientExam.GetData(C1PatientExam.RowSel, 0), Int64)
    '        nVisitID = CType(C1PatientExam.GetData(C1PatientExam.RowSel, 1), Int64)
    '        strExamName = CType(C1PatientExam.GetData(C1PatientExam.RowSel, 3), String)
    '        dtDOS = CType(C1PatientExam.GetData(C1PatientExam.RowSel, 2), Date)

    '        Dim blnFinished As Boolean
    '        If CType(C1PatientExam.GetData(C1PatientExam.RowSel, 6), String) = "True" Then
    '            blnFinished = True
    '        Else
    '            blnFinished = False
    '        End If

    '        'ShowPastExam(nPastExamID, pID, nVisitID, dtDOS, strExamName, blnFinished, gstrPatientCode)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub






    Private Sub c1ProblemList_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1ProblemList.MouseDoubleClick
        Try
            RaiseEvent ShowProblemListForm(sender, e)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub



    Private Sub C1OrderDetails_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1OrderDetails.MouseDoubleClick
        Try
            If Not IsNothing(C1OrderDetails.GetData(C1OrderDetails.RowSel, 0)) AndAlso Not IsNothing(C1OrderDetails.GetData(C1OrderDetails.RowSel, 1)) Then
                'RaiseEvent ShowRadiologyForm(C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_VISITID), C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_ORDERDATE))
                RaiseEvent ShowRadiologyForm(CType(C1OrderDetails.GetData(C1OrderDetails.Row, 0), Int64), CType(C1OrderDetails.GetData(C1OrderDetails.Row, 1), String))

            End If
            'C1OrderDetails.GetData(C1OrderDetails.RowSel, COLUM_ORDERDATE)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub GloUC_TransactionHistory1_btnShowGraphClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GloUC_TransactionHistory1.btnShowGraphClick
        Try

            Dim dt_selectedResult As DataTable = Nothing
            '''' Get selected Result From Grid
            dt_selectedResult = GloUC_TransactionHistory1.SelectResult()

            '''' If DataTable is empty then exit from Procedure.
            'If dt_selectedResult Is Nothing Then
            If (IsNothing(dt_selectedResult)) Then
                Exit Sub
            End If
            If dt_selectedResult.Rows.Count = 0 Then
                dt_selectedResult.Dispose()
                Exit Sub
            End If
            ' End If

            For rowcnt As Int32 = 0 To dt_selectedResult.Rows.Count - 1 ''for condition added for Bug #68058:
                If String.IsNullOrEmpty(dt_selectedResult.Rows(rowcnt)(0)) = True Then
                    MessageBox.Show("Graph cannot be displayed because collected date is blank.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dt_selectedResult.Dispose()
                    Exit Sub
                End If
            Next rowcnt
            ''''Get Min and Max Value from DataTable

            Dim dv As DataView
            dv = New DataView(dt_selectedResult)
            dv.Sort = "Value"

            Dim max As Integer = dv.Item(dv.Count - 1)("Value").ToString()
            Dim min As Integer = dv.Item(0)("Value").ToString() '' dv.Table.Rows.Count - 1)("Value")



            Dim dtSelectedResultToDate As DateTime = CType(dt_selectedResult.Rows(dt_selectedResult.Rows.Count - 1)(0), DateTime)


            ' lines for get the first results data and show it into the label as From-date
            Dim dtStartdate As DateTime
            dtStartdate = dt_selectedResult.Rows.Item(0)(0) ' Take from date for Display 
            dtStartdate = Format(dtStartdate, "MM/dd/yyyy")

            ' view the graphs for the provided values as a parameters provided
            Dim oGraphResult As New frmLab_GraphsResult(dtStartdate, dtSelectedResultToDate, 0, 0, PatientID, dt_selectedResult.Rows(0)(1), dt_selectedResult.Rows(0)(2), dt_selectedResult, , False, , min, max)

            '.MdiParent = Me.MdiParent
            oGraphResult.WindowState = FormWindowState.Maximized
            oGraphResult.ShowInTaskbar = False
            oGraphResult.Show()

            'Me.Close()
            Exit Sub
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub GloUC_TransactionHistory1_On_Flex_DoubleClick(ByVal PatientID As Long, ByVal OrderID As Long, ByVal VisitID As Long, ByVal TransactionDate As Date) Handles GloUC_TransactionHistory1.On_Flex_DoubleClick
        Try
            If TransactionDate <> "12:00:00 AM" Then
                RaiseEvent OnLabsFlexDoubleClick(PatientID, OrderID, VisitID, TransactionDate)
            Else
                RaiseEvent OnLabsFlexDoubleClick(PatientID, OrderID, VisitID, DateTime.Now)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub txtsearchProcedures_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchProcedures.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            With C1Dignosis
                If C1Dignosis.Row > 0 Then
                    C1Dignosis.Select(C1Dignosis.Row, 1, True)
                End If
            End With
           
        End If
    End Sub

    Private Sub txtsearchProcedures_SearchFired() Handles txtsearchProcedures.SearchFired

        Try


            Dim strSearch As String

            With txtsearchProcedures
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            If txtsearchProcedures.Text.Length = 0 Then
                FillICDCPTMOD(pID)
            Else

                If cmbCriteria.Text = "Date" Then

                    txtsearchProcedures.Text = ""
                    FillICDCPTMODForSearch(pID, "", True, dtpFromDate.Text, dtpToDate.Text)

                Else
                    FillICDCPTMODForSearch(pID, strSearch, False)

                End If
                With C1Dignosis

                    Dim strNode As String = ""
                    For i As Int16 = 1 To .Rows.Count - 1
                        strNode = ""

                        strNode = UCase(C1Dignosis.GetData(i, Col_ICD9Code_Description).ToString.Trim)
                        If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                            .Row = i
                            Exit Sub
                        End If
                    Next

                End With

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub trMedications_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trMedications.DoubleClick
        Try
            If Not IsNothing(trMedications.SelectedNode) Then
                If Not trMedications.SelectedNode Is trMedications.Nodes.Item(0) Then
                    'PatientId,VisitID
                    RaiseEvent ViewMedications(pID, CType(trMedications.SelectedNode.Tag, Int64))
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trProblemList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trProblemList.DoubleClick
        Try
            If Not IsNothing(trProblemList.SelectedNode) Then
                If Not trProblemList.SelectedNode Is trProblemList.Nodes.Item(0) Then
                    'PatientId,VisitID
                    If Not IsNothing(trProblemList.SelectedNode.Tag) Then

                        Dim arr() As String = Split(CType(trProblemList.SelectedNode.Tag, String), "-")
                        If Not IsNothing(arr) Then
                            If arr.Length = 2 Then
                                Dim OrderID As Int64 = CType(arr(0), Int64)
                                'Dim VisitID As Int64 = CType(arr(1), Int64)
                                Dim VisitID As Int64 = CType(arr(1), Int64)

                                RaiseEvent ViewProblemList(pID, OrderID, VisitID)
                            End If
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trHistory.DoubleClick
        Try
            If Not IsNothing(trHistory.SelectedNode) Then
                If Not trHistory.SelectedNode Is trHistory.Nodes.Item(0) Then
                    'PatientId,VisitID
                    If Not IsNothing(trHistory.SelectedNode.Tag) Then
                        Dim objPatientSynopsis As New ClsPatientSynopsis

                        Dim visitdate As DateTime = objPatientSynopsis.GetVisitdate(trHistory.SelectedNode.Tag)

                        RaiseEvent ViewPatientHistory(CType(trHistory.SelectedNode.Tag, Int64), visitdate)
                    End If

                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trProcedures_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trProcedures.DoubleClick
        Try
            'trProcedures.SuspendLayout()
            'Me.Cursor = Cursors.WaitCursor
            'trProcedures.Enabled = False
            If Not IsNothing(trProcedures.SelectedNode) Then
                If Not trProcedures.SelectedNode Is trProcedures.Nodes.Item(0) Then
                    'PatientId,VisitID
                    'CHANGE THE PARAMETER FOR THE FUCNTION FOR CHECKING THE gblnICD9Driven SETTING OF ADMIN
                    Dim arr() As String = Split(CType(trProcedures.SelectedNode.Tag, String), "-")
                    If Not IsNothing(arr) Then
                        If arr.Length = 6 Then

                            '20-Jul-15 Aniket: Resolving Bug #86609: gloEMR>Synopsis>Procedure>If exam is finished then it should not allow to Open the DxCPT window and to modify the data.
                            If Trim(CType(arr(5), String)) = "False" Then

                                Dim ExamID As Int64 = CType(arr(0), Int64)
                                Dim VisitID As Int64 = CType(arr(1), Int64)
                                Dim DOS As DateTime = CType(arr(2), DateTime)
                                Dim ExamName As String = CType(arr(3), String)
                                Dim PatientId As Int64 = CType(arr(4), Int64)


                                RaiseEvent ViewProcedures(ExamID, VisitID, DOS, ExamName, PatientId)
                            Else
                                MessageBox.Show("Diagnosis/Procedures for this exam cannot be changed as its status is 'Finished'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If

                        End If

                    End If

                    End If
                End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'Me.Cursor = Cursors.Default
            'trProcedures.Enabled = True
            'trProcedures.ResumeLayout()
        End Try
    End Sub

    Private Sub trImaging_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trImaging.DoubleClick
        Try
            If Not IsNothing(trImaging.SelectedNode) Then
                If Not trImaging.SelectedNode Is trImaging.Nodes.Item(0) Then
                    If trImaging.SelectedNode.Parent.Text <> "Orders" Then
                        'PatientId,VisitID
                        If Not IsNothing(trImaging.SelectedNode.Tag) Then
                            '    Dim objPatientSynopsis As New ClsPatientSynopsis
                            '    Dim visitdate As DateTime = objPatientSynopsis.GetVisitdate(CType(trImaging.SelectedNode.Tag, Int64))

                            '    RaiseEvent ViewRadiology(CType(trImaging.SelectedNode.Tag, Int64), visitdate)
                            Dim arr() As String = Split(CType(trImaging.SelectedNode.Tag, String), "|")
                            If Not IsNothing(arr) Then
                                If arr.Length = 2 Then
                                    Dim VisitID As Int64 = CType(arr(0), Int64)
                                    Dim OrderDate As DateTime = CType(arr(1), DateTime)


                                    RaiseEvent ViewRadiology(VisitID, OrderDate)
                                End If
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub trImagingST_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trImagingST.DoubleClick
        Try
            If Not IsNothing(trImagingST.SelectedNode) Then
                If Not trImagingST.SelectedNode Is trImagingST.Nodes.Item(0) Then
                    'If trImagingST.SelectedNode.Parent.Text <> "Date of Study" Then
                    'PatientId,VisitID
                    If Not IsNothing(trImagingST.SelectedNode.Tag) Then
                        '    Dim objPatientSynopsis As New ClsPatientSynopsis
                        '    Dim visitdate As DateTime = objPatientSynopsis.GetVisitdate(CType(trImaging.SelectedNode.Tag, Int64))

                        '    RaiseEvent ViewRadiology(CType(trImaging.SelectedNode.Tag, Int64), visitdate)
                        Dim arr() As String = Split(CType(trImagingST.SelectedNode.Tag, String), "|")
                        If Not IsNothing(arr) Then
                            If arr.Length > 0 Then

                                Dim DateOfStudy As DateTime = CType(arr(0), DateTime)
                                Dim PatientID As Int64 = CType(arr(1), Int64)
                                Dim ExamID As Int64 = CType(arr(2), Int64)
                                Dim VisitID As Int64 = CType(arr(3), Int64)
                                Dim ClinicID As Int64 = CType(arr(4), Int64)



                                RaiseEvent ViewStressTest(PatientID, VisitID, DateOfStudy, ExamID, ClinicID)
                            End If
                        End If
                        'End If
                    End If
                End If
            End If

            ' '' ''If C1CV_StressTest.Rows.Count > 1 Then


            ' '' ''    mPatientID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_PatientID) '' COL_PatientID_ST
            ' '' ''    mStressID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_StressID)
            ' '' ''    mExamID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ExamID) '' COL_ExamID_ST
            ' '' ''    mVisitID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_VisitID) '' COL_VisitID_ST
            ' '' ''    mClinicID = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_ClinicID) '' COL_ClinicID_ST
            ' '' ''    mDateofStudy = C1CV_StressTest.GetData(C1CV_StressTest.Row, COL_DateofStudyInvisible)

            ' '' ''    ''RaiseEvent ViewStressTest(mPatientID, mVisitID, mDateofStudy, mExamID, mClinicID)

            ' '' ''    Dim ofrm As New frmCV_StressTests(mPatientID, mVisitID, mDateofStudy)
            ' '' ''    ofrm.ShowDialog()
            ' '' ''    SetGridStyle()
            ' '' ''    FillStressTest()

            ' '' ''End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trImplant_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trImplant.DoubleClick
        Try
            If Not IsNothing(trImplant.SelectedNode) Then
                If Not trImplant.SelectedNode Is trImplant.Nodes.Item(0) Then
                    'If trImagingST.SelectedNode.Parent.Text <> "Date of Study" Then
                    'PatientId,VisitID
                    If Not IsNothing(trImplant.SelectedNode.Tag) Then
                        '    Dim objPatientSynopsis As New ClsPatientSynopsis
                        '    Dim visitdate As DateTime = objPatientSynopsis.GetVisitdate(CType(trImaging.SelectedNode.Tag, Int64))

                        '    RaiseEvent ViewRadiology(CType(trImaging.SelectedNode.Tag, Int64), visitdate)
                        Dim arr() As String = Split(CType(trImplant.SelectedNode.Tag, String), "|")
                        If Not IsNothing(arr) Then
                            If arr.Length > 0 Then

                                Dim DateOfStudy As DateTime = CType(arr(0), DateTime)
                                Dim PatientID As Int64 = CType(arr(1), Int64)
                                Dim ExamID As Int64 = CType(arr(2), Int64)
                                Dim VisitID As Int64 = CType(arr(3), Int64)
                                Dim ClinicID As Int64 = CType(arr(4), Int64)

                                Dim ofrmImplant As New frmCV_ImplantDevice(PatientID, VisitID, DateOfStudy)
                                ofrmImplant.ShowDialog(ofrmImplant.Parent)
                                SetGridStyle()
                                FillImplantDevice()
                                Dim dsdata As DataSet
                                dsdata = c2.PopulateSynopsisData(PatientID, "Implant")
                                PopulateImplant(dsdata.Tables("Implant"))
                                If Not IsNothing(dsdata) Then
                                    dsdata.Dispose()
                                    dsdata = Nothing
                                End If
                                '   AddHandler ofrmImplant.FormClosed, AddressOf onImplantDeviceClosed


                                RaiseEvent ViewCardiologyDevice(PatientID, ExamID, VisitID)
                                ofrmImplant.Dispose()
                                ofrmImplant = Nothing
                            End If
                        End If
                        'End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub trLabs_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trLabs.DoubleClick
        Try
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "Opening labs form....", gloAuditTrail.ActivityOutCome.Success)
            If Not IsNothing(trLabs.SelectedNode) Then
                If Not trLabs.SelectedNode Is trLabs.Nodes.Item(0) Then

                    'PatientId,VisitID
                    If Not IsNothing(trLabs.SelectedNode.Tag) Then
                        Dim arr() As String = Split(CType(trLabs.SelectedNode.Tag, String), "-")
                        If Not IsNothing(arr) Then
                            If arr.Length = 3 Then
                                Dim OrderID As Int64 = CType(arr(0), Int64)
                                'Dim VisitID As Int64 = CType(arr(1), Int64)
                                Dim OrderDate As Date = CType(arr(2), Date)

                                Dim objPatientSynopsis As New ClsPatientSynopsis
                                Dim VisitID As Int64 = objPatientSynopsis.GetVisitIDforLabs(OrderID)
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "Raised event to Open labs form....", gloAuditTrail.ActivityOutCome.Success)
                                RaiseEvent OnLabsFlexDoubleClick(pID, OrderID, VisitID, OrderDate)
                                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Load, "Raise event to open labs completed........", gloAuditTrail.ActivityOutCome.Success)
                            End If
                        End If
                    End If
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        End Try
    End Sub

    Private Sub C1Dignosis_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1Dignosis.DoubleClick
        Try
            C1Dignosis.Cursor = Cursors.WaitCursor
            C1Dignosis.Enabled = False
            With C1Dignosis
                If .Row > 0 Then


                    '20-Jul-15 Aniket: Resolving Bug #86609: gloEMR>Synopsis>Procedure>If exam is finished then it should not allow to Open the DxCPT window and to modify the data.
                    If (CType(.GetData(.Row, Col_HiddenExFinish), String)) = "False" Then

                        Dim ExamID As Int64 = CType(.GetData(.Row, Col_DIAEXAMID), Int64)
                        Dim VisitID As Int64 = CType(.GetData(.Row, Col_DIAVISITID), Int64)
                        Dim ExamName As String = CType(.GetData(.Row, Col_HiddenExamName), String)
                        Dim DOS As DateTime = (.GetData(.Row, Col_HiddenDOS).Date)

                        RaiseEvent ViewProcedures(ExamID, VisitID, DOS, ExamName, pID)

                    Else
                        C1Dignosis.Cursor = Cursors.Default
                        C1Dignosis.Enabled = True
                        MessageBox.Show("Diagnosis/Procedures for this exam cannot be changed as its status is 'Finished'.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                End If
            End With

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            C1Dignosis.Cursor = Cursors.Default
            C1Dignosis.Enabled = True
        End Try
    End Sub

    Private Sub C1Dignosis_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Dignosis.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then




                Dim nRow As Integer
                nRow = C1Dignosis.HitTest(e.X, e.Y).Row
                If nRow > 0 Then
                    Dim oMenuStrip As New ContextMenuStrip
                    Dim oMenuItem As ToolStripItem
                    oMenuItem = New ToolStripMenuItem


                    oMenuItem.Text = "Open Exam"
                    oMenuItem.Tag = C1Dignosis.Item(nRow, 0)
                    oMenuStrip.Items.Add(oMenuItem)
                    'Try
                    '    If (IsNothing(C1Dignosis.ContextMenuStrip) = False) Then
                    '        C1Dignosis.ContextMenuStrip.Dispose()
                    '        C1Dignosis.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    If (IsNothing(C1Dignosis.ContextMenuStrip) = False) Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(C1Dignosis.ContextMenuStrip)
                        If (IsNothing(C1Dignosis.ContextMenuStrip.Items) = False) Then
                            C1Dignosis.ContextMenuStrip.Items.Clear()
                        End If

                        C1Dignosis.ContextMenuStrip.Dispose()
                    End If

                    C1Dignosis.ContextMenuStrip = oMenuStrip
                    C1Dignosis.Select(nRow, 0)
                    AddHandler oMenuItem.Click, AddressOf OpenProcExam
                End If
            End If




        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub OpenProcExam(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim nPastExamID As Long
            Dim nVisitID As Long
            Dim dtDOS As DateTime
            Dim strExamName As String
            'Dim bIsReviewed As Boolean

            ''Dim em As System.Windows.Forms.MouseEventArgs
            nPastExamID = CType(C1Dignosis.GetData(C1Dignosis.RowSel, Col_DIAEXAMID), Int64)
            nVisitID = CType(C1Dignosis.GetData(C1Dignosis.RowSel, Col_DIAVISITID), Int64)
            strExamName = CType(C1Dignosis.GetData(C1Dignosis.RowSel, Col_HiddenExamName), String)
            dtDOS = CType(C1Dignosis.GetData(C1Dignosis.RowSel, Col_HiddenDOS), Date)

            Dim blnFinished As Boolean
            'If CType(C1Dignosis.GetData(C1Dignosis.RowSel, Col_HiddenExFinish), String) = "True" Then
            '    blnFinished = True
            'Else
            '    blnFinished = False
            'End If
            ''Code added by Dipak
            ''IF exam is displayed as unfinished exam on the loacal mc  but is finished from 
            ''other machine then read the finished status of the exam from database   
            Try
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim strSQL As String
                Dim Status As String

                strSQL = "SELECT ISNULL(bIsFinished ,'False' ) AS bIsFinished FROM PatientExams WHERE nExamID = " & nPastExamID & " " _
                        & " AND nVisitID = " & nVisitID & " AND  nPatientID = " & pID & "  AND dtDOS = '" & dtDOS & "' "
                oDB.Connect(GetConnectionString)
                Status = oDB.ExecuteQueryScaler(strSQL)
                blnFinished = Convert.ToBoolean(Status)
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            Catch ex As Exception

            End Try

            ShowPastExam(nPastExamID, pID, nVisitID, dtDOS, strExamName, blnFinished, strPatientCode)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub oSearchEjectionFraction_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles oSearchEjectionFraction.AfterTextSearch
        Try




            If IsNothing(dv) Then
                If oSearchEjectionFraction.SearchString.Trim.Length = 0 Then
                    Dim dtEF As DataTable = PopulateEjectionFractionList()
                    SetGridStyle(dtEF)
                    oSearchEjectionFraction.IntialiseDatatable(dtEF)
                End If

            Else
                If IsNothing(dv.Table) = False Then
                    'FillC1Grid(dv.ToTable)
                    SetGridStyle(dv.ToTable)
                End If
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Ejection Fraction having substring" & oSearchEjectionFraction.SearchString, gstrLoginName, gstrClientMachineName, _PatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Ejection Fraction having substring" & oSearchEjectionFraction.SearchString, gstrLoginName, gstrClientMachineName, _PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
        End Try
    End Sub
    Private Sub oSearchImplantDevice_AfterTextSearch(ByVal dv As System.Data.DataView, ByVal sScarchString As String) Handles oSearchImplantDevice.AfterTextSearch
        Try
            'Check if result entered for Implant device
            ''If IsNothing(dv) Then
            ''    If oSearchImplantDevice.SearchString.Trim.Length = 0 Then
            ''        Dim dtCardio As DataTable = PopulateCardioDeviceData()
            ''        FillC1Grid(dtCardio)
            ''        oSearchImplantDevice.IntialiseDatatable(dtCardio)
            ''    End If

            ''Else
            ''    'if result available filter data and set grid style
            ''    If IsNothing(dv.Table) = False Then
            ''        FillC1Grid(dv.ToTable)
            ''    End If
            ''    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched implant device having substring" & oSearchImplantDevice.SearchString, gstrLoginName, gstrClientMachineName, m_patientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            ''End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Implant device having substring" & oSearchImplantDevice.SearchString, gstrLoginName, gstrClientMachineName, m_patientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
        End Try
    End Sub



    Private Sub dtpFromDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFromDate.TextChanged
        Try

            C1Dignosis.Rows.Count = 1
            FillICDCPTMODForSearch(pID, "", True, dtpFromDate.Text, dtpToDate.Text)
            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched procedures having date " & dtpFromDate.Text & " and " & dtpToDate.Text, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            'With C1Dignosis
            '    Dim _FindRow As Integer = 0
            '    _FindRow = .FindRow(strSearch, 1, Col_ICD9Code_Description, False, False, True)
            '    If .Row > 0 Then
            '        .Select(_FindRow, 1, True)
            '    End If

            '    '' 20070921 - Mahesh - InString Search 
            '    Dim strNode As String = ""
            '    For i As Int16 = 1 To .Rows.Count - 1
            '        strNode = ""
            '        strNode = UCase(.GetData(i, Col_ICD9Code_Description).ToString.Trim)
            '        If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
            '            .Row = i
            '            Exit Sub
            '        End If
            '    Next
            '    '' ---
            'End With
            'code to search in C1 commented on 26/02/2009
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search procedures having date " & dtpFromDate.Text & " and " & dtpToDate.Text, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtpToDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpToDate.TextChanged
        Try

            C1Dignosis.Rows.Count = 1
            FillICDCPTMODForSearch(pID, "", True, dtpFromDate.Text, dtpToDate.Text)
            '   gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched procedures having date " & dtpFromDate.Text & " and " & dtpToDate.Text, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '  gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search procedures having date " & dtpFromDate.Text & " and " & dtpToDate.Text, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub


    Private Sub C1dgPatientDetails_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1dgPatientDetails.MouseDoubleClick
        Try
            RaiseEvent ShowHistoryForm(sender, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub btnProbExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblProblems.Click


        If blnProbExpand = False Then
            'pnlProblems.Width = pnlProblemsWidth * 5
            pnlProblems.Width = pnlProblemsWidthMax + 100

            pnlMedications.Width = pnlMedicationsWidth
            pnlHistory.Width = pnlHistoryWidth
            pnlImplant.Width = pnlImplantWidth
            pnlProcedures.Width = pnlProceduresWidth
            pnlLabs.Width = pnlLabsWidth
            pnlImagingST.Width = pnlImagingSTWidth

            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center

            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center

            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center


            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center

            blnProbExpand = True
            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center
            Dim TotalWidth As Int32 = splImagingST.Width + pnlImagingST.Width + spltLabs.Width + pnlLabs.Width + spltProcedures.Width + spltHistory.Width + pnlHistory.Width + spltMedications.Width + spltProblems.Width + pnlProblems.Width

            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlSummary.Width - TotalWidth
        Else
            pnlProblems.Width = pnlProblemsWidth
            blnProbExpand = False
            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center
            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlImplantWidth
        End If
    End Sub


    Private Sub btnMedExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMedications.Click
        If blnMedExpand = False Then
            pnlMedications.Width = pnlMedicationsWidthMax + intWidth

            pnlProblems.Width = pnlProblemsWidth
            pnlHistory.Width = pnlHistoryWidth
            pnlImplant.Width = pnlImplantWidth
            pnlProcedures.Width = pnlProceduresWidth
            pnlLabs.Width = pnlLabsWidth
            pnlImagingST.Width = pnlImagingSTWidth

            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center


            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center

            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center

            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center


            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center

            blnMedExpand = True
            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            Dim TotalWidth As Int32 = splImagingST.Width + pnlImagingST.Width + spltLabs.Width + pnlLabs.Width + spltProcedures.Width + spltHistory.Width + pnlHistory.Width + spltMedications.Width + spltProblems.Width + pnlProblems.Width

            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlSummary.Width - TotalWidth
        Else
            pnlMedications.Width = pnlMedicationsWidth
            blnMedExpand = False
            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center
            pnlImplant.Dock = DockStyle.Fill
        End If
    End Sub



    Private Sub btnLabExpand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLabs.Click
        If blnLabsExpand = False Then
            pnlLabs.Width = pnlLabsWidthMax + intWidth

            pnlProblems.Width = pnlProblemsWidth
            pnlMedications.Width = pnlMedicationsWidth
            pnlHistory.Width = pnlHistoryWidth
            pnlImplant.Width = pnlImplantWidth
            pnlProcedures.Width = pnlProceduresWidth
            pnlImagingST.Width = pnlImagingSTWidth

            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center

            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center

            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center


            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center

            blnLabsExpand = True
            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center
            Dim TotalWidth As Int32 = splImagingST.Width + pnlImagingST.Width + spltLabs.Width + pnlLabs.Width + spltProcedures.Width + spltHistory.Width + pnlHistory.Width + spltMedications.Width + spltProblems.Width + pnlProblems.Width

            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlSummary.Width - TotalWidth
        Else
            pnlLabs.Width = pnlHistoryWidth
            blnLabsExpand = False
            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center
            pnlImplant.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub btnProcedExpand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProcedures.Click
        If blnProcExpand = False Then
            pnlProcedures.Width = pnlProceduresWidthMax + intWidth

            pnlProblems.Width = pnlProblemsWidth
            pnlMedications.Width = pnlMedicationsWidth
            pnlHistory.Width = pnlHistoryWidth
            pnlImplant.Width = pnlImplantWidth
            pnlLabs.Width = pnlLabsWidth
            pnlImagingST.Width = pnlImagingSTWidth

            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center


            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center

            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center


            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center

            blnProcExpand = True
            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center
            Dim TotalWidth As Int32 = splImagingST.Width + pnlImagingST.Width + spltLabs.Width + pnlLabs.Width + spltProcedures.Width + spltHistory.Width + pnlHistory.Width + spltMedications.Width + spltProblems.Width + pnlProblems.Width

            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlSummary.Width - TotalWidth
        Else
            pnlProcedures.Width = pnlProceduresWidth
            blnProcExpand = False
            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center
            pnlImplant.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub btnImplantExpand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblImplant.Click
        If blnImplantExpand = False Then
            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlImplantWidthMax + intWidth
            pnlProblems.Width = pnlProblemsWidth
            pnlMedications.Width = pnlMedicationsWidth
            pnlHistory.Width = pnlHistoryWidth
            pnlImagingST.Width = pnlImagingSTWidth
            pnlLabs.Width = pnlLabsWidth
            pnlProcedures.Width = pnlProceduresWidth

            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center

            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center

            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center

            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center

            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center

            blnImplantExpand = True
            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

        Else
            pnlImplant.Width = pnlImplantWidth
            blnImplantExpand = False
            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center
            pnlImplant.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub btnImagSTExpand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblImaging.Click
        If blnImagingSTExpand = False Then
            pnlImagingST.Width = pnlImagingSTWidthMax + intWidth
            pnlProblems.Width = pnlProblemsWidth
            pnlMedications.Width = pnlMedicationsWidth
            pnlHistory.Width = pnlHistoryWidth
            pnlImplant.Width = pnlImplantWidth
            pnlLabs.Width = pnlLabsWidth
            pnlProcedures.Width = pnlProceduresWidth

            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center

            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center

            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center

            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center

            blnImagingSTExpand = True
            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center
            Dim TotalWidth As Int32 = splImagingST.Width + pnlImagingST.Width + spltLabs.Width + pnlLabs.Width + spltProcedures.Width + spltHistory.Width + pnlHistory.Width + spltMedications.Width + spltProblems.Width + pnlProblems.Width

            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlSummary.Width - TotalWidth
        Else
            pnlImagingST.Width = pnlImagingSTWidth
            blnImagingSTExpand = False
            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center
            pnlImplant.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub btnHistExpand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblHistory.Click
        If blnHistoryExpand = False Then
            pnlHistory.Width = pnlHistoryWidthMax + intWidth

            pnlProblems.Width = pnlProblemsWidth
            pnlMedications.Width = pnlMedicationsWidth
            pnlImplant.Width = pnlImplantWidth
            pnlProcedures.Width = pnlProceduresWidth
            pnlLabs.Width = pnlLabsWidth
            pnlImagingST.Width = pnlImagingSTWidth

            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center

            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center

            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center

            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center

            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center

            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center

            blnHistoryExpand = True
            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center
            Dim TotalWidth As Int32 = splImagingST.Width + pnlImagingST.Width + spltLabs.Width + pnlLabs.Width + spltProcedures.Width + spltHistory.Width + pnlHistory.Width + spltMedications.Width + spltProblems.Width + pnlProblems.Width

            pnlImplant.Dock = DockStyle.Fill
            pnlImplant.Width = pnlSummary.Width - TotalWidth
        Else
            pnlHistory.Width = pnlHistoryWidth
            blnHistoryExpand = False
            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center
            pnlImplant.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub btnProbExpand_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProbExpand.MouseHover
        If blnProbExpand Then
            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowHover
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowHover
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnProbExpand_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProbExpand.MouseLeave
        If blnProbExpand Then
            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnProbExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProbExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnProcedExpand_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcedExpand.MouseHover
        If blnProcExpand Then
            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowHover
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowHover
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnProcedExpand_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcedExpand.MouseLeave
        If blnProcExpand Then
            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnProcedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnProcedExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnMedExpand_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMedExpand.MouseHover
        If blnMedExpand Then
            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowHover
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowHover
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnMedExpand_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMedExpand.MouseLeave
        If blnMedExpand Then
            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnMedExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnMedExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnLabExpand_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabExpand.MouseHover
        If blnLabsExpand Then
            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowHover
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowHover
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnLabExpand_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabExpand.MouseLeave
        If blnLabsExpand Then
            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnLabExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnLabExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnImplantExpand_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImplantExpand.MouseHover
        If blnImplantExpand Then
            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowHover
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowHover
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnImplantExpand_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImplantExpand.MouseLeave
        If blnImplantExpand Then
            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnImplantExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImplantExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnImagSTExpand_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImagSTExpand.MouseHover
        If blnImagingSTExpand Then
            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowHover
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowHover
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnImagSTExpand_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImagSTExpand.MouseLeave
        If blnImagingSTExpand Then
            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnImagSTExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnImagSTExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnHistExpand_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistExpand.MouseHover
        If blnHistoryExpand Then
            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowHover
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowHover
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btnHistExpand_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistExpand.MouseLeave
        If blnHistoryExpand Then
            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_LeftDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center
        Else
            btnHistExpand.BackgroundImage = Global.gloEMR.My.Resources.Img_RightDoubleArrowWhite
            btnHistExpand.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    '' SUDHIR 20090715 '' TO REFRESH DMS LIST ''
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtSearchCriteria.Text = ""
        Fill_PatientSacnedDocuments(pID)
        txtSearchCriteria.Focus() ''added to solve bugid 70927

    End Sub
    '' END SUDHIR ''

    Private Sub Button1_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.Button1, " Refresh ")
    End Sub
    Private Sub SetPanelWidth(ByVal oTreeView As TreeView, ByRef intPnlWidth As Int32, ByVal stext As String, ByVal ActualPanelWidth As Int32)
        Dim myFont As Font
        Dim stringsize As SizeF
        Try
            myFont = oTreeView.Font
            Dim oGrp As Graphics = oTreeView.CreateGraphics()
            stringsize = oGrp.MeasureString(stext, myFont)
            ''Code Review Changes: Dispose Graphics object
            oGrp.Dispose()
            If stringsize.Width > ActualPanelWidth Then
                intPnlWidth = stringsize.Width
            Else
                intPnlWidth = ActualPanelWidth
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub cmbSearch_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedValueChanged
        Try
            'Developer: Mitesh Patel
            'Date:20-Dec-2011
            'Bug ID: 17089

            txtSearchCriteria.Text = String.Empty
            lblsearch.Text = cmbSearch.Text & " : "
        Catch ex As Exception

        End Try
    End Sub

    Private Sub C1MedicationDetails_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1MedicationDetails.MouseDoubleClick
        Try
            'Incident #55315: 00016572 : Carry forward issue
            RaiseEvent GetPrescription() ''ShowMedicationForm(sender, e)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub lblProblems_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblProblems.MouseHover
        lblProblems.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblProblems.BackgroundImageLayout = ImageLayout.Stretch
        lblProblems.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblProblems_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProblems.MouseLeave
        lblProblems.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblProblems.BackgroundImageLayout = ImageLayout.Stretch
        lblProblems.ForeColor = Color.White
    End Sub

    Private Sub lblMedications_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblMedications.MouseHover
        lblMedications.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblMedications.BackgroundImageLayout = ImageLayout.Stretch
        lblMedications.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblMedications_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblMedications.MouseLeave
        lblMedications.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblMedications.BackgroundImageLayout = ImageLayout.Stretch
        lblMedications.ForeColor = Color.White
    End Sub

    Private Sub lblLabs_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLabs.MouseHover
        lblLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblLabs.BackgroundImageLayout = ImageLayout.Stretch
        lblLabs.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblLabs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLabs.MouseLeave
        lblLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblLabs.BackgroundImageLayout = ImageLayout.Stretch
        lblLabs.ForeColor = Color.White
    End Sub

    Private Sub lblOrders_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrders.MouseHover
        lblOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblOrders.BackgroundImageLayout = ImageLayout.Stretch
        lblOrders.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblOrders_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrders.MouseLeave
        lblOrders.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblOrders.BackgroundImageLayout = ImageLayout.Stretch
        lblOrders.ForeColor = Color.White
    End Sub

    Private Sub lblProcedures_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProcedures.MouseHover
        lblProcedures.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblProcedures.BackgroundImageLayout = ImageLayout.Stretch
        lblProcedures.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblProcedures_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProcedures.MouseLeave
        lblProcedures.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblProcedures.BackgroundImageLayout = ImageLayout.Stretch
        lblProcedures.ForeColor = Color.White
    End Sub

    Private Sub lblImplant_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblImplant.MouseHover
        lblImplant.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblImplant.BackgroundImageLayout = ImageLayout.Stretch
        lblImplant.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblImplant_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblImplant.MouseLeave
        lblImplant.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblImplant.BackgroundImageLayout = ImageLayout.Stretch
        lblImplant.ForeColor = Color.White
    End Sub

    Private Sub lblHistory_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblHistory.MouseHover
        lblHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblHistory.BackgroundImageLayout = ImageLayout.Stretch
        lblHistory.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblHistory_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblHistory.MouseLeave
        lblHistory.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblHistory.BackgroundImageLayout = ImageLayout.Stretch
        lblHistory.ForeColor = Color.White
    End Sub

    Private Sub lblImaging_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblImaging.MouseHover
        lblImaging.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        lblImaging.BackgroundImageLayout = ImageLayout.Stretch
        lblImaging.ForeColor = Color.FromArgb(31, 73, 125)
    End Sub

    Private Sub lblImaging_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblImaging.MouseLeave
        lblImaging.BackgroundImage = Global.gloEMR.My.Resources.Img_Blue2007
        lblImaging.BackgroundImageLayout = ImageLayout.Stretch
        lblImaging.ForeColor = Color.White
    End Sub

    Private Sub btnClearProcedures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearProcedures.Click
        'Shubhangi 20091009
        'Use to clear search text box
        txtsearchProcedures.ResetText()
        txtsearchProcedures.Focus()

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'Shubhangi 20091009
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Private Sub btnClearImaging_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearImaging.Click
        'Shubhangi 20091009
        'Use to clear search text box
        txtImagingSearch.ResetText()
        txtImagingSearch.Focus()

    End Sub

    Private Sub trimagingecho_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trimagingecho.DoubleClick
        Try
            If Not IsNothing(trimagingecho.SelectedNode) Then
                If Not trimagingecho.SelectedNode Is trimagingecho.Nodes.Item(0) Then
                    'If trimagingecho.SelectedNode.Parent.Text <> "Date of Study" Then
                    'PatientId,VisitID
                    If Not IsNothing(trimagingecho.SelectedNode.Tag) Then
                        '    Dim objPatientSynopsis As New ClsPatientSynopsis
                        '    Dim visitdate As DateTime = objPatientSynopsis.GetVisitdate(CType(trImaging.SelectedNode.Tag, Int64))

                        '    RaiseEvent ViewRadiology(CType(trImaging.SelectedNode.Tag, Int64), visitdate)
                        Dim arr() As String = Split(CType(trimagingecho.SelectedNode.Tag, String), "|")
                        If Not IsNothing(arr) Then
                            If arr.Length > 0 Then

                                Dim DateOfStudy As DateTime = CType(arr(0), DateTime)
                                Dim PatientID As Int64 = CType(arr(1), Int64)
                                Dim ExamID As Int64 = CType(arr(2), Int64)
                                Dim VisitID As Int64 = CType(arr(3), Int64)
                                'Dim ClinicID As Int64 = CType(arr(4), Int64)

                                ''frmCV_Echocardiogram
                                Dim ofrm As New frmCV_Echocardiogram(PatientID, VisitID, DateOfStudy)
                                ofrm.ShowDialog(ofrm.Parent)
                                If Not IsNothing(ofrm) Then
                                    ofrm.Dispose()
                                    ofrm = Nothing
                                End If
                                FillElectroPhysioTest()
                            End If
                        End If
                        'End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub



    'Developer:Sanjog Dhamke
    'Date: 4 April 2012
    'Bug PRD Name:Copy Exam Functionality
    'Reason: To implement new functionality
    Private Sub CopyPastExam()


        Dim nPastExamID As Long
        Dim nVisitID As Long
        Dim dtDOS As DateTime
        Dim strExamName As String
        Dim strTemplateName As String

        Try


            ''20121220::AuditTrail Entry Added Whenever the ‘Copy Exam’ menu is clicked:: Email Dated 12/20/2012:: Subject-copy forward
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Open, "Past Exam copied forward using Copy Exam.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

            '10-Dec-15 Aniket: Resolving Bug #91850: gloEMR: Copy exam: Application gives exception 
            If IsNumeric(C1PatientExam.GetData(C1PatientExam.Row, "nExamID")) = True Then



                nPastExamID = CType(C1PatientExam.GetData(C1PatientExam.Row, "nExamID"), Int64)
                nVisitID = CType(C1PatientExam.GetData(C1PatientExam.Row, "nVisitID"), Int64)
                dtDOS = CType(C1PatientExam.GetData(C1PatientExam.Row, "DOS"), Date)
                strExamName = CType(C1PatientExam.GetData(C1PatientExam.Row, "Exam Name"), String)
                strTemplateName = CType(C1PatientExam.GetData(C1PatientExam.Row, "Template Name"), String)
                Dim blnFinished As Boolean

                If CType(C1PatientExam.GetData(C1PatientExam.Row, "Finished"), String) = "Yes" Then
                    blnFinished = True
                Else
                    blnFinished = False
                End If



                If Trim(pID) <> 0 Then

                    Me.Cursor = Cursors.WaitCursor

                    Dim frm As New frmPatientExam(pID, _VisitID)


                    frm.IsCopyExam = True
                    frm.OpenExam(nPastExamID)

                    Dim NewExamId As Int64 = frm.examid
                    Dim NewVisitID As Int64 = frm.mgnVisitID


                    Dim objclsExam As New clsPatientExams
                    objclsExam.SetProviderExam(NewExamId)

                    With frm
                        .Hide()
                        .IsNewExam = True
                        .blnModify = False
                        .Text = "Copy Exams"
                        .PatientID = pID


                        If .OpenPastExam(nPastExamID, nVisitID, dtDOS, strExamName.Trim(), False, strTemplateName) = True Then

                            CType(Me.ParentForm.MdiParent, MainMenu).pnlMainToolBar.Visible = False

                            .MdiParent = Me.ParentForm.MdiParent
                            .Show()
                            If .ExamViewMode Then
                                .ViewExam(nPastExamID)
                            Else
                                .OpenPastExamContents(nPastExamID, False)
                                .GetdataFromOtherForms(gloEMRWord.enumDocType.None)
                                objclsExam.UnLock_Exam(nPastExamID, gstrClientMachineName)
                            End If
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Open, "Exam Opened for Copy.", pID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                            frm.examid = NewExamId
                            frm.mgnVisitID = NewVisitID

                            frm.lblExamName.Text = strExamName.Trim()
                            'Ashish added on 1st November 2014
                            'to autosave copied Exam
                            frm.SwitchOffSavingFlags = True
                            frm.SaveExam(0, True)
                            frm.SwitchOffSavingFlags = False
                            AddHandler frm.FormClosed, AddressOf OnPatientExam_Close

                            If Not IsNothing(objclsExam) Then
                                objclsExam.Dispose()
                                objclsExam = Nothing
                            End If
                        Else
                            objclsExam.DeleteExam(NewExamId, strExamName, True)
                            frm.Dispose()
                            frm = Nothing
                        End If

                    End With
                    Me.Cursor = Cursors.Default

                    If Not IsNothing(objclsExam) Then
                        objclsExam.Dispose()
                        objclsExam = Nothing
                    End If
                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Select the patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Me.Cursor = Cursors.Default

    End Sub


    Private Sub chkDateFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDateFilter.CheckedChanged
        If chkDateFilter.Checked Then
            dtpExamFrom.Enabled = True
            dtpExamTo.Enabled = True
        Else
            dtpExamFrom.Enabled = False
            dtpExamTo.Enabled = False
        End If
        DTPExamTo_ValueChanged(sender, e)
    End Sub

    'Incident #55315: 00016572 : Carry forward issue   
    'C1MedicationDetails_MouseDown and EditMedication newly added
    Private Sub C1MedicationDetails_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles C1MedicationDetails.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then


                Dim nRow As Integer
                nRow = C1MedicationDetails.HitTest(e.X, e.Y).Row
                If nRow > 0 Then
                    Dim oMenuStrip As New ContextMenuStrip
                    Dim oMenuItem As ToolStripItem
                    oMenuItem = New ToolStripMenuItem

                    oMenuItem.Text = "Edit Medication"
                    oMenuItem.Tag = C1MedicationDetails.Item(nRow, 0)
                    oMenuStrip.Items.Add(oMenuItem)
                    
                    If (IsNothing(C1MedicationDetails) = False) Then
                        Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {C1MedicationDetails.ContextMenuStrip}

                        If (IsNothing(CmpControls) = False) Then
                            If CmpControls.Length > 0 Then
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                            End If
                        End If

                        If (IsNothing(CmpControls) = False) Then
                            If CmpControls.Length > 0 Then
                                gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                            End If
                        End If

                        
                    End If
                    C1MedicationDetails.ContextMenuStrip = oMenuStrip
                    C1MedicationDetails.Select(nRow, 0)
                    tempsender = sender
                    tempE = e
                    AddHandler oMenuItem.Click, AddressOf EditMedication
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub EditMedication(sender As Object, e As EventArgs)

        '02-Mar-2016 Aniket: Resolving Bug #93870: gloMedication>Synopsis>Medication>Throwing an exception
        If IsNothing(tempsender) = False AndAlso IsNothing(tempE) = False Then
            RaiseEvent ShowMedicationForm(tempsender, tempE)
            tempsender = Nothing
            tempE = Nothing
        End If
        

    End Sub


    Private Sub btnExamNameClear_Click(sender As System.Object, e As System.EventArgs) Handles btnExamNameClear.Click
        txtExamName.ResetText()
        txtExamName.Focus()
    End Sub


End Class

