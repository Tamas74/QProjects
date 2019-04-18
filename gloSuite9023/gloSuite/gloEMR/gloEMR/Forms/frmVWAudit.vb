Imports C1.Win.C1FlexGrid
Imports gloOffice

Public Class frmVWAudit

#Region "Attributes and Properties"
    Dim oclsAudit As clsAuditHistory
    Dim dt As DataTable
    Dim c1dgAuditDetailSize As Object = Nothing

    Private Col_ActivityDate As Integer = 0
    Private Col_UserAction As Integer = 1
    Private Col_HistoryCategory As Integer = 2

    Private Col_HistoryItem As Integer = 3
    Private Col_Active As Integer = 4
    Private Col_Reaction As Integer = 5

    Private Col_Comments As Integer = 6
    Private Col_Activity As Integer = 7
    Private Col_UserName As Integer = 8

    Private Col_FullName As Integer = 9

    Private Col_COUNT As Integer = 10

    Private Col_CQMCategory As Integer = 11
    Private Col_UDI As Integer = 12
    Private Col_ConcernStatus As Integer = 13
    Private Col_ResolvedEndDate As Integer = 14
    Private Col_AllergySeverity As Integer = 15
    Private Col_AllergyIntelorenceCode As Integer = 16

    Private bIsExam As Boolean = False
    Private _nTransactionID As Int64
    Private _sActivityType As String

    Private _sActivityCategory As String
    Private _IsUpdatedReconcile As Boolean = False
    Private _nAuditTrailID As Int64


    Private Enum HistoryEnum
        sHistoryCategory
        sHistoryItem
        sComments
        sReaction
        sActivity
        dtActivityDate
        dtVisitdate
        LoginName
        FullName
    End Enum
    Public Property IsUpdatedReconcile() As Boolean
        Get
            Return _IsUpdatedReconcile
        End Get
        Set(value As Boolean)
            _IsUpdatedReconcile = value
        End Set
    End Property
    Public Property IsExam() As Boolean
        Get
            Return Me.bIsExam
        End Get
        Set(value As Boolean)
            Me.bIsExam = value
        End Set
    End Property
#End Region

#Region "Form Initialization"

    Private Sub frmVWAudit_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            c1Audit.AllowEditing = False
            c1PatientAudit.AllowEditing = False
            Select Case _sActivityCategory
                Case "ProblemList"
                    FillProblemList(_nTransactionID, _sActivityType)
                Case "Vitals"
                    FillVitals(_nTransactionID, _sActivityType)
                Case "CarePlan"
                    FillCarePlan(_nTransactionID, _sActivityType)
                Case "Medication"
                    FillMedication(_nTransactionID, _sActivityType)
                Case "Prescription"
                    FillPrescription(_nTransactionID, _sActivityType)
                Case "History"
                    FillHistory(_nTransactionID, _sActivityType)
                Case "Immunization"
                    FillImmunization(_nTransactionID, _sActivityType)
                Case "Patient"
                    FillPatient(_nTransactionID, _sActivityType, _nAuditTrailID)
                Case "Exam"
                    FillExam(_nTransactionID, _sActivityType)
                    Me.bIsExam = True
                Case "MedicationReconcile"
                    FillMedication(_nTransactionID, _sActivityType)
                Case "AllergyReconcile"
                    FillHistory(_nTransactionID, _sActivityType)
                Case "ProblemReconcile"
                    FillProblemList(_nTransactionID, _sActivityType)
                Case "PatientEducation"
                    FillPatientEducationAudit(_nTransactionID, _sActivityType)
                Case "ImplantableDevice"
                    FillImplantableDevicesAudit(_nTransactionID, _sActivityType)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub New(ByVal nTransactionID As Int64, ByVal sActivityType As String, ByVal sActivityCategory As String)
        _nTransactionID = nTransactionID
        _sActivityType = sActivityType
        _sActivityCategory = sActivityCategory
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal nTransactionID As Int64, ByVal sActivityType As String, ByVal sActivityCategory As String, ByVal nAuditTrailID As Int64)
        _nTransactionID = nTransactionID
        _sActivityType = sActivityType
        _sActivityCategory = sActivityCategory
        _nAuditTrailID = nAuditTrailID

        InitializeComponent()
    End Sub
#End Region

#Region "Grid Clicks"

    Private Sub c1Audit_DoubleClick(sender As Object, e As System.EventArgs) Handles c1Audit.DoubleClick

        If Not Me.bIsExam Then
            Exit Sub
        End If

        Dim nExamID As Long = 0
        Dim nPatientExamsAuditID As Long = 0
        Dim sActionType As String = String.Empty
        Dim sUserAction As String = String.Empty
        Dim nValidatedID As Long = 0

        Dim objDatabaseLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim dbParameters As gloDatabaseLayer.DBParameters = Nothing

        Try
            nExamID = Convert.ToInt64(c1Audit.Rows(c1Audit.RowSel)("nExamID"))
            nPatientExamsAuditID = Convert.ToInt64(c1Audit.Rows(c1Audit.RowSel)("PatientExamsAuditID"))
            sUserAction = Convert.ToString(c1Audit.Rows(c1Audit.RowSel)("UserAction"))

            If sUserAction.ToLower = "inserted" Then
                sActionType = "Add"
            ElseIf sUserAction.ToLower = "before update" Then
                sActionType = "Modify"
            ElseIf sUserAction.ToLower = "after update" Then
                sActionType = "Updated"
            ElseIf sUserAction.ToLower = "deleted" Then
                sActionType = "Delete"
            End If

            If nPatientExamsAuditID > 0 And nPatientExamsAuditID > 0 Then
                objDatabaseLayer = New gloDatabaseLayer.DBLayer(GetConnectionString)
                dbParameters = New gloDatabaseLayer.DBParameters()

                With dbParameters
                    .Add(New gloDatabaseLayer.DBParameter("@nExamID", nExamID, ParameterDirection.Input, SqlDbType.BigInt))
                    .Add(New gloDatabaseLayer.DBParameter("@nPatientExamsAuditID", nPatientExamsAuditID, ParameterDirection.Input, SqlDbType.BigInt))
                    .Add(New gloDatabaseLayer.DBParameter("@sActionType", sActionType, ParameterDirection.Input, SqlDbType.VarChar))
                    .Add(New gloDatabaseLayer.DBParameter("@nReturnedPatientExamsAuditID", 0, ParameterDirection.Output, SqlDbType.BigInt))
                End With

                objDatabaseLayer.Connect(False)
                objDatabaseLayer.Execute("Get_AuditTrailDetails_PatientExams", dbParameters, nValidatedID)
                objDatabaseLayer.Disconnect()

                If nValidatedID > 0 Then
                    Dim frmDocumentViewer As New gloOffice.frmWd_PatientExam(GetConnectionString, nValidatedID, 0, True)
                    frmDocumentViewer.ShowDialog(IIf(IsNothing(frmDocumentViewer.Parent), Me, frmDocumentViewer.Parent))
                    frmDocumentViewer.Dispose()
                    frmDocumentViewer = Nothing
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        Finally
            If dbParameters IsNot Nothing Then
                dbParameters.Clear()
                dbParameters.Dispose()
                dbParameters = Nothing
            End If

            If objDatabaseLayer IsNot Nothing Then
                objDatabaseLayer.Dispose()
                objDatabaseLayer = Nothing
            End If

            nExamID = Nothing
            nPatientExamsAuditID = Nothing
            sActionType = Nothing
            sUserAction = Nothing
            nValidatedID = Nothing

        End Try
    End Sub

#End Region

#Region "Grid Filling Procedures"

    Private Sub FillExam(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetExamHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    With c1Audit
                        .Clear()
                        .DataSource = dt.DefaultView
                        .DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                        .AllowSorting = False

                        Dim cs As C1.Win.C1FlexGrid.CellStyle '= c1Audit.Styles.Add("Wrap")
                        Try
                            If (c1Audit.Styles.Contains("Wrap")) Then
                                cs = c1Audit.Styles("Wrap")
                            Else
                                cs = c1Audit.Styles.Add("Wrap")

                            End If
                        Catch ex As Exception
                            cs = c1Audit.Styles.Add("Wrap")

                        End Try
                        cs.WordWrap = True

                        .Cols("PatientExamsAuditID").Caption = "PatientExamsAuditID"
                        .Cols("PatientExamsAuditID").Visible = False

                        .Cols("bIsFinished").Caption = "Exam Finished"

                        .Cols("dtDOS").Caption = "DOS"
                        .Cols("dtDOS").DataType = GetType(String)
                        .Cols("dtDOS").Format = "MM/dd/yyyy h:mm tt"
                        .Cols("dtDOS").Width = 150

                        .Cols("nExamID").Caption = "nExamID"
                        .Cols("nExamID").Visible = False

                        .Cols("UpdatedDateTime").DataType = GetType(String)
                        .Cols("UpdatedDateTime").Format = "MM/dd/yyyy h:mm tt"

                        .Cols("UpdatedDateTime").Caption = "Activity Date Time"
                        .Cols("UpdatedDateTime").Width = 150

                        .Cols("sExamName").Caption = "Exam Name"
                        .Cols("sExamName").Width = 200

                        .Cols("sUserName").Caption = "User Name"

                        .Cols("sMachineName").Caption = "Machine Name"
                        .Cols("sMachineName").Width = 100

                        .Cols("sTemplateName").Caption = "Template Name"
                        .Cols("sTemplateName").Width = 200

                        .Cols("UserAction").Caption = "User Action"

                        .Cols("NotesUpdateAction").Caption = "NotesUpdateAction"
                        .Cols("NotesUpdateAction").Visible = False

                        .Cols("ProviderName").Width = 125
                        .Cols("ProviderName").Caption = "Provider"
                    End With
                    For i As Integer = 0 To c1Audit.Cols.Count - 1
                        c1Audit.Cols(i).TextAlign = TextAlignEnum.LeftCenter
                    Next
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub FillProblemList(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable

        Try
            dt = oclsAudit.GetProblemHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    c1Audit.Clear()
                    c1Audit.DataSource = dt.DefaultView
                    c1Audit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Audit.AllowSorting = False

                    '' Set Column Header
                    c1Audit.Cols("nAuditTrailID").Caption = "nAuditTrailID"
                    c1Audit.Cols("nProblemHSTID").Caption = "nProblemHSTID"
                    c1Audit.Cols("nProblemID").Caption = "ProblemID"
                    c1Audit.Cols("nVisitID").Caption = "Visit ID"
                    c1Audit.Cols("nPatientID").Caption = "Patient ID"
                    c1Audit.Cols("dtDOS").Caption = "Date"
                    c1Audit.Cols("ICD9").Caption = "Diagnosis(ICD9)"
                    c1Audit.Cols("sCheifComplaint").Caption = "Description"
                    c1Audit.Cols("nProblemStatus").Caption = "Status"
                    c1Audit.Cols("sPrescription").Caption = "Prescription"
                    c1Audit.Cols("nUserID").Caption = "UserID"
                    c1Audit.Cols("sUserName").Caption = "User"
                    c1Audit.Cols("sMachineName").Caption = "MachineName"
                    c1Audit.Cols("dtResolvedDate").Caption = "Resolved Date"
                    c1Audit.Cols("sResolvedComment").Caption = "Resolved Comment"
                    c1Audit.Cols("nImmediacy").Caption = "Immediacy"
                    c1Audit.Cols("sComments").Caption = "Comments"
                    c1Audit.Cols("sProvider").Caption = "Provider"
                    c1Audit.Cols("sLocation").Caption = "Location"
                    c1Audit.Cols("dtModifiedDate").Caption = "Last Update"
                    c1Audit.Cols("sExamId").Caption = "Exam Id"
                    c1Audit.Cols("sTranUser").Caption = "Transaction User"
                    c1Audit.Cols("sConceptID").Caption = "SnoMed CT ID"
                    c1Audit.Cols("sSnoMedID").Caption = "SnoMed ID"
                    c1Audit.Cols("sDescriptionID").Caption = "Description ID"
                    c1Audit.Cols("sSnoMedDescription").Caption = "SnoMed Description"
                    c1Audit.Cols("sTransactionID1").Caption = "Problem Type"
                    c1Audit.Cols("sTransactionID2").Caption = "Transaction ID2"
                    c1Audit.Cols("sUserAction").Caption = "User Action"
                    c1Audit.Cols("dtHistoryDateTime").Caption = "Activity DateTime"
                    c1Audit.Cols("dtActivityDateTime").Caption = "Activity DateTime"
                    c1Audit.Cols("sActivityCategory").Caption = "Activity Category"
                    c1Audit.Cols("sActivityDescription").Caption = "Activity Description"
                    c1Audit.Cols("nAuditUsrID").Caption = "Audit User ID"
                    c1Audit.Cols("sAuditMachineName").Caption = "Audit Machine Name"
                    c1Audit.Cols("sSoftwareComponent").Caption = "Software Component"
                    c1Audit.Cols("sOutcome").Caption = "Outcome"
                    c1Audit.Cols("nClinicID").Caption = "ClinicID"
                    c1Audit.Cols("sActivityModule").Caption = "Activity Module"
                    c1Audit.Cols("sActivityType").Caption = "Activity Type"
                    c1Audit.Cols("nTransactionID").Caption = "Transaction ID"
                    c1Audit.Cols("nProviderID").Caption = "Provider ID"
                    c1Audit.Cols("sAuditUserName").Caption = "Audit User Name"




                    '' Set Column Visibility
                    c1Audit.Cols("nAuditTrailID").Visible = False
                    c1Audit.Cols("nProblemHSTID").Visible = False
                    c1Audit.Cols("nProblemID").Visible = False
                    c1Audit.Cols("nVisitID").Visible = False
                    c1Audit.Cols("nPatientID").Visible = False
                    c1Audit.Cols("dtDOS").Visible = True
                    c1Audit.Cols("ICD9").Visible = True
                    c1Audit.Cols("sCheifComplaint").Visible = True
                    c1Audit.Cols("nProblemStatus").Visible = True
                    c1Audit.Cols("sPrescription").Visible = True
                    c1Audit.Cols("nUserID").Visible = False
                    c1Audit.Cols("sUserName").Visible = True
                    c1Audit.Cols("sMachineName").Visible = False
                    c1Audit.Cols("dtResolvedDate").Visible = True
                    c1Audit.Cols("sResolvedComment").Visible = False
                    c1Audit.Cols("nImmediacy").Visible = True
                    c1Audit.Cols("sComments").Visible = False
                    c1Audit.Cols("sProvider").Visible = True
                    c1Audit.Cols("sLocation").Visible = True
                    c1Audit.Cols("dtModifiedDate").Visible = True
                    c1Audit.Cols("sExamId").Visible = False
                    c1Audit.Cols("sTranUser").Visible = False
                    c1Audit.Cols("sConceptID").Visible = True
                    c1Audit.Cols("sSnoMedID").Visible = False
                    c1Audit.Cols("sDescriptionID").Visible = False
                    c1Audit.Cols("sSnoMedDescription").Visible = False
                    c1Audit.Cols("sTransactionID1").Visible = True
                    c1Audit.Cols("sTransactionID2").Visible = False
                    c1Audit.Cols("sUserAction").Visible = True
                    c1Audit.Cols("dtHistoryDateTime").Visible = True
                    c1Audit.Cols("dtActivityDateTime").Visible = False
                    c1Audit.Cols("sActivityCategory").Visible = False
                    c1Audit.Cols("sActivityDescription").Visible = False
                    c1Audit.Cols("nAuditUsrID").Visible = False
                    c1Audit.Cols("sAuditMachineName").Visible = False
                    c1Audit.Cols("sSoftwareComponent").Visible = False
                    c1Audit.Cols("sOutcome").Visible = False
                    c1Audit.Cols("nClinicID").Visible = False
                    c1Audit.Cols("sActivityModule").Visible = False
                    c1Audit.Cols("sActivityType").Visible = False
                    c1Audit.Cols("nTransactionID").Visible = False
                    c1Audit.Cols("nProviderID").Visible = False
                    c1Audit.Cols("sAuditUserName").Visible = True

                    For i As Integer = 0 To c1Audit.Cols.Count - 1
                        c1Audit.Cols(i).TextAlign = TextAlignEnum.LeftCenter
                    Next
                    Dim cs As C1.Win.C1FlexGrid.CellStyle '= c1Audit.Styles.Add("Wrap")
                    Try
                        If (c1Audit.Styles.Contains("Wrap")) Then
                            cs = c1Audit.Styles("Wrap")
                        Else
                            cs = c1Audit.Styles.Add("Wrap")

                        End If
                    Catch ex As Exception
                        cs = c1Audit.Styles.Add("Wrap")

                    End Try
                    cs.WordWrap = True
                    c1Audit.Cols("sCheifComplaint").Style = cs

                    ''Set Column width and Datatype
                    c1Audit.Cols("ICD9").Width = 300
                    c1Audit.Cols("sCheifComplaint").Width = 300
                    c1Audit.Cols("sUserAction").Width = 100
                    c1Audit.Cols("dtDOS").Width = 75

                    c1Audit.Cols("dtDOS").DataType = GetType(String)

                    c1Audit.Cols("dtHistoryDateTime").DataType = GetType(String)
                    c1Audit.Cols("dtHistoryDateTime").Format = "MM/dd/yyyy h:mm tt"
                    c1Audit.Cols("dtHistoryDateTime").Width = 125

                    c1Audit.Cols("dtActivityDateTime").DataType = GetType(String)
                    c1Audit.Cols("dtActivityDateTime").Format = "MM/dd/yyyy h:mm tt"
                    c1Audit.Cols("dtActivityDateTime").Width = 125

                    c1Audit.Cols("dtModifiedDate").DataType = GetType(String)
                    c1Audit.Cols("dtModifiedDate").Format = "MM/dd/yyyy"
                    c1Audit.Cols("dtModifiedDate").Width = 125

                    c1Audit.Cols("dtResolvedDate").DataType = GetType(String)
                    c1Audit.Cols("dtResolvedDate").Format = "MM/dd/yyyy"
                    c1Audit.Cols("dtResolvedDate").Width = 125

                    Dim _strComplaints As String
                    Dim _Comments() As String
                    Dim _nCommentCount As Integer
                    If c1Audit.Rows.Count > 1 Then
                        For i As Integer = 1 To c1Audit.Rows.Count - 1
                            _strComplaints = ""
                            _Comments = Nothing
                            _nCommentCount = 0
                            _strComplaints = c1Audit.GetData(i, "sComments")
                            If _strComplaints <> "" Then
                                _Comments = Split(_strComplaints, vbNewLine)
                                _nCommentCount = _Comments.Length + 1
                            End If
                            c1Audit.Rows(i).AllowResizing = AllowDraggingEnum.Both
                            c1Audit.Rows(i).AllowDragging = DrawModeEnum.OwnerDraw
                            If _nCommentCount <> 0 Then
                                c1Audit.Rows(i).Height = c1Audit.Rows.DefaultSize * _nCommentCount
                            End If
                        Next
                    Else
                        MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    End If
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub FillImmunization(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetImmunizationHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    c1Audit.Clear()
                    c1Audit.DataSource = dt.DefaultView
                    c1Audit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Audit.AllowSorting = False
                    c1Audit.Cols("Activity DateTime").Width = 135
                    c1Audit.Cols("nAuditTrailID").Visible = False
                    c1Audit.Cols("dtActivityDateTime").Visible = False
                    c1Audit.Cols("sActivityCategory").Visible = False
                    c1Audit.Cols("sDescription").Visible = False
                    c1Audit.Cols("nUserID").Visible = False
                    c1Audit.Cols("sAuditMachineName").Visible = False
                    c1Audit.Cols("sSoftwareComponent").Visible = False
                    c1Audit.Cols("sOutcome").Visible = False
                    c1Audit.Cols("nClinicID").Visible = False
                    c1Audit.Cols("sActivityModule").Visible = False
                    c1Audit.Cols("sActivityType").Visible = False
                    c1Audit.Cols("nTransactionID").Visible = False
                    c1Audit.Cols("nProviderID").Visible = False
                    c1Audit.Cols("sAuditUserName").Visible = False

                    c1Audit.Cols("Activity DateTime").DataType = GetType(String)
                    c1Audit.Cols("Date").DataType = GetType(String)
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try

    End Sub


    Public Sub fillPatientAudit(ByVal nPatientID As Int64, ByVal nAuditTrailId As Int64)
        Dim dtPatientAudit As New DataTable()
        dtPatientAudit = oclsAudit.GetPatientAuditHistory(nPatientID, nAuditTrailId)
        If Not IsNothing(dtPatientAudit) Then
            If dtPatientAudit.Rows.Count > 0 Then
                pnl_Details.Visible = True
                pnl_Base.Visible = False
                pnl_Details.Dock = System.Windows.Forms.DockStyle.Fill
                c1PatientAudit.Clear()
                c1PatientAudit.DataSource = dtPatientAudit
                c1PatientAudit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                c1PatientAudit.AllowSorting = False

                c1PatientAudit.Cols("dtActivityDateTime").Visible = True
                c1PatientAudit.Cols("UserAction").Visible = True
                c1PatientAudit.Cols("sFirstName").Visible = True
                c1PatientAudit.Cols("sMiddleName").Visible = True
                c1PatientAudit.Cols("sLastName").Visible = True
                c1PatientAudit.Cols("nSSN").Visible = True
                c1PatientAudit.Cols("dtDOB").Visible = True
                c1PatientAudit.Cols("sGender").Visible = True
                c1PatientAudit.Cols("sAddressLine1").Visible = True
                c1PatientAudit.Cols("sAddressLine2").Visible = True
                c1PatientAudit.Cols("sCity").Visible = True
                c1PatientAudit.Cols("sState").Visible = True
                c1PatientAudit.Cols("sZIP").Visible = True
                c1PatientAudit.Cols("sCounty").Visible = True
                c1PatientAudit.Cols("sPhone").Visible = True
                c1PatientAudit.Cols("sLoginName").Visible = True
                c1PatientAudit.Cols("Provider").Visible = True
                c1PatientAudit.Cols("sLang").Visible = True
                c1PatientAudit.Cols("sRace").Visible = True
                c1PatientAudit.Cols("sEthn").Visible = True
                c1PatientAudit.Cols("sSexualOrientationDesc").Visible = True
                c1PatientAudit.Cols("sGenderIdentityDesc").Visible = True
                c1PatientAudit.Cols("sMultipleBirthIndicator").Visible = True
                c1PatientAudit.Cols("nBirthOrder").Visible = True
                c1PatientAudit.Cols("sImmunizationRegistryStatus").Visible = True

                c1PatientAudit.Cols("dtActivityDateTime").Width = 135

                c1PatientAudit.Cols("PatientAuditLogID").Visible = False
                c1PatientAudit.Cols("AuditTrailID").Visible = False
                c1PatientAudit.Cols("LoginSessionID").Visible = False
                c1PatientAudit.Cols("PatientID").Visible = False
                c1PatientAudit.Cols("PatientCode").Visible = False
                c1PatientAudit.Cols("sSuffix").Visible = True
                c1PatientAudit.Cols("sBirthTime").Visible = True
                c1PatientAudit.Cols("sMaritalStatus").Visible = True
                c1PatientAudit.Cols("sHandDominance").Visible = True
                c1PatientAudit.Cols("sCommunicationPreference").Visible = True
                c1PatientAudit.Cols("sAreaCode").Visible = True
                c1PatientAudit.Cols("sCountry").Visible = True
                c1PatientAudit.Cols("sMobile").Visible = True
                c1PatientAudit.Cols("sEmail").Visible = True
                c1PatientAudit.Cols("sFAX").Visible = True
                c1PatientAudit.Cols("nProviderID").Visible = False
                c1PatientAudit.Cols("sEmergencyContact").Visible = True
                c1PatientAudit.Cols("sEmergencyPhone").Visible = True
                c1PatientAudit.Cols("sEmergencyMobile").Visible = True
                c1PatientAudit.Cols("sEmergencyRelationshipCode").Visible = False
                c1PatientAudit.Cols("sEmergencyRelationshipDesc").Visible = True
                c1PatientAudit.Cols("nSexualOrientationCategoryID").Visible = False
                c1PatientAudit.Cols("sSexualOrientationCode").Visible = False
                c1PatientAudit.Cols("sSexualOrientationOtherSpecification").Visible = True
                c1PatientAudit.Cols("nGenderIdentityCateroryID").Visible = False
                c1PatientAudit.Cols("sGenderIdentityCode").Visible = False
                c1PatientAudit.Cols("sGenderIdentityOtherSpecification").Visible = True
                c1PatientAudit.Cols("sPatientPrevFname").Visible = True
                c1PatientAudit.Cols("sPatientPrevMname").Visible = True
                c1PatientAudit.Cols("sPatientPrevLname").Visible = True
                c1PatientAudit.Cols("sBirthSex").Visible = True

                c1PatientAudit.Cols("bSignatureOnFile").Visible = False
                c1PatientAudit.Cols("nPatientDirective").Visible = False
                c1PatientAudit.Cols("nExemptFromReport").Visible = False

                c1PatientAudit.Cols("dtActivityDateTime").Caption = "Activity DateTime"
                c1PatientAudit.Cols("UserAction").Caption = "User Action"
                c1PatientAudit.Cols("sFirstName").Caption = "First Name"
                c1PatientAudit.Cols("sMiddleName").Caption = "Middle Name"
                c1PatientAudit.Cols("sLastName").Caption = "Last Name"
                c1PatientAudit.Cols("nSSN").Caption = "SSN"
                c1PatientAudit.Cols("dtDOB").Caption = "DOB"
                c1PatientAudit.Cols("sGender").Caption = "Gender"
                c1PatientAudit.Cols("sAddressLine1").Caption = "Address line 1"
                c1PatientAudit.Cols("sAddressLine2").Caption = "Address line 2"
                c1PatientAudit.Cols("sCity").Caption = "City"
                c1PatientAudit.Cols("sState").Caption = "State"
                c1PatientAudit.Cols("sZIP").Caption = "Zip"
                c1PatientAudit.Cols("sCounty").Caption = "County"
                c1PatientAudit.Cols("sPhone").Caption = "Phone"
                c1PatientAudit.Cols("sLoginName").Caption = "Login Name"
                c1PatientAudit.Cols("Provider").Caption = "Provider"
                c1PatientAudit.Cols("sLang").Caption = "Language"
                c1PatientAudit.Cols("sRace").Caption = "Race"
                c1PatientAudit.Cols("sEthn").Caption = "Ethnicity"
                c1PatientAudit.Cols("sSexualOrientationDesc").Caption = "Sexual Orientation"
                c1PatientAudit.Cols("sGenderIdentityDesc").Caption = "Gender Identity"
                c1PatientAudit.Cols("sMultipleBirthIndicator").Caption = "Multiple Birth Indicator"
                c1PatientAudit.Cols("nBirthOrder").Caption = "Birth Order"
                c1PatientAudit.Cols("dtActivityDateTime").DataType = GetType(String)
                c1PatientAudit.Cols("sBirthSex").Caption = "Birth Sex"
                c1PatientAudit.Cols("sImmunizationRegistryStatus").Caption = "Immunization Registry Status"
                c1PatientAudit.Cols("sSuffix").Caption = "Suffix"
                c1PatientAudit.Cols("sBirthTime").Caption = "Birth Time"
                c1PatientAudit.Cols("sMaritalStatus").Caption = "Marital Status"
                c1PatientAudit.Cols("sHandDominance").Caption = "Hand Dominance"
                c1PatientAudit.Cols("sCommunicationPreference").Caption = "Communication Preference"
                c1PatientAudit.Cols("sAreaCode").Caption = "Area Code"
                c1PatientAudit.Cols("sCountry").Caption = "Country"
                c1PatientAudit.Cols("sMobile").Caption = "Mobile"
                c1PatientAudit.Cols("sEmail").Caption = "Email"
                c1PatientAudit.Cols("sFAX").Caption = "Fax"
                c1PatientAudit.Cols("sEmergencyContact").Caption = "Emergency Contact"
                c1PatientAudit.Cols("sEmergencyPhone").Caption = "Emergency Phone"
                c1PatientAudit.Cols("sEmergencyMobile").Caption = "Emergency Mobile"
                c1PatientAudit.Cols("sEmergencyRelationshipDesc").Caption = "Emergency Relationship Desc"
                c1PatientAudit.Cols("sSexualOrientationOtherSpecification").Caption = "Sexual Orientation Other Specification"
                c1PatientAudit.Cols("sGenderIdentityOtherSpecification").Caption = "Gender Identity Other Specification"
                c1PatientAudit.Cols("sPatientPrevFname").Caption = " Prev. First Name"
                c1PatientAudit.Cols("sPatientPrevMname").Caption = " Prev. Middle Name"
                c1PatientAudit.Cols("sPatientPrevLname").Caption = " Prev. Last Name"

                c1PatientAudit.Cols("sMultipleBirthIndicator").Width = 155
                c1PatientAudit.Cols("sSexualOrientationDesc").Width = 150
                c1PatientAudit.Cols("sGenderIdentityDesc").Width = 135
                c1PatientAudit.Cols("sHandDominance").Width = 135
                c1PatientAudit.Cols("sCommunicationPreference").Width = 175
                c1PatientAudit.Cols("sEmergencyContact").Width = 135
                c1PatientAudit.Cols("sEmergencyPhone").Width = 135
                c1PatientAudit.Cols("sEmergencyMobile").Width = 135
                c1PatientAudit.Cols("sEmergencyRelationshipDesc").Width = 200
                c1PatientAudit.Cols("sSexualOrientationOtherSpecification").Width = 250
                c1PatientAudit.Cols("sGenderIdentityOtherSpecification").Width = 250
                c1PatientAudit.Cols("sPatientPrevFname").Width = 135
                c1PatientAudit.Cols("sPatientPrevMname").Width = 135
                c1PatientAudit.Cols("sPatientPrevLname").Width = 135
                c1PatientAudit.Cols("sImmunizationRegistryStatus").Width = 200

                c1PatientAudit.Cols("sInboundHospital").Visible = True
                c1PatientAudit.Cols("sInboundHospital").Width = 135
                c1PatientAudit.Cols("sInboundHospital").Caption = "Inbound Hospital"

                c1PatientAudit.Cols("sInboundtranCare").Visible = True
                c1PatientAudit.Cols("sInboundtranCare").Width = 160
                c1PatientAudit.Cols("sInboundtranCare").Caption = "Inbound Transition Care"
            Else
                pnl_Details.Visible = False
            End If
        Else
            pnl_Details.Visible = False

        End If
    End Sub
    Public Sub FillPatient(ByVal nTransactionID As Int64, ByVal sActivityType As String, ByVal nAuditTrailId As Int64)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable

        Try
            fillPatientAudit(nTransactionID, nAuditTrailId)
            If pnl_Details.Visible = False Then
                dt = oclsAudit.GetPatientHistory(nTransactionID, sActivityType)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        c1Audit.Clear()
                        c1Audit.DataSource = dt.DefaultView
                        c1Audit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                        c1Audit.AllowSorting = False
                        c1Audit.Cols("dtChangeDateTime").Width = 135
                        c1Audit.Cols("nPatientID").Caption = "nPatientID"
                        c1Audit.Cols("nPatientID").Visible = False
                        c1Audit.Cols("dtChangeDateTime").Caption = "Activity DateTime"
                        c1Audit.Cols("dtChangeDateTime").Width = 135
                        c1Audit.Cols("sUserAction").Caption = "User Action"
                        c1Audit.Cols("sUserAction").Width = 105
                        c1Audit.Cols("sFirstName").Caption = "First Name"
                        c1Audit.Cols("sMiddleName").Caption = "Middle Name"
                        c1Audit.Cols("sLastName").Caption = "Last Name"
                        c1Audit.Cols("dtDOB").Caption = "DOB"
                        c1Audit.Cols("sGender").Caption = "Gender"
                        c1Audit.Cols("sAddressLine1").Caption = "AddressLine1"
                        c1Audit.Cols("sAddressLine2").Caption = "AddressLine2"
                        c1Audit.Cols("sCity").Caption = "City"
                        c1Audit.Cols("sState").Caption = "State"
                        c1Audit.Cols("sZIP").Caption = "ZIP"
                        c1Audit.Cols("sCounty").Caption = "County"
                        c1Audit.Cols("sPhone").Caption = "Phone"
                        c1Audit.Cols("sLoginName").Caption = "Login Name"
                        c1Audit.Cols("sLanguage").Caption = "Language"
                        'c1Audit.Cols("sLoginName").Visible = False

                        c1Audit.Cols("dtChangeDateTime").DataType = GetType(String)
                        c1Audit.Cols("dtDOB").DataType = GetType(String)
                    Else
                        If pnl_Details.Visible = False Then
                            MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Close()
                        End If

                    End If
                Else
                    If pnl_Details.Visible = False Then
                        MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    End If
                End If
            End If
          
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub FillHistory(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetHistoryofHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    Dim strActivityDate As String
                    'Dim strVisitDate As String
                    Dim strCategory As String
                    Dim strhistory As String
                    Dim strReaction As String = ""
                    Dim strActive As String = ""
                    Dim strComment As String
                    Dim strRection_Status As String
                    Dim strActivity As String
                    Dim strUserName As String
                    Dim strFullName As String = ""
                    Dim strUserAction As String
                    Dim strhistoryType As String = ""
                    Dim strCQMCategory As String = ""
                    Dim strUDI As String = ""
                    Dim strConcernStatus As String = ""
                    Dim strResolvedEndDate As String = ""
                    Dim strAllergySeverity As String = ""
                    Dim strAllergyIntelorenceCode As String = ""
                    With c1Audit

                        .Rows.Count = 1

                        gloC1FlexStyle.Style(c1Audit, True)
                        .SetData(0, Col_ActivityDate, "Activity DateTime")
                        .SetData(0, Col_UserAction, "User Action")
                        .SetData(0, Col_HistoryCategory, "Category")
                        .SetData(0, Col_HistoryItem, "History")
                        .SetData(0, Col_Comments, "Comment")
                        .SetData(0, Col_Active, "Active")
                        '16-Nov-17 Aniket: Resolving Resolving Bug #110313: gloEMR>>History>>Allergy columns names showing for all history module audit log and family history relation showing in patient details panel, not showing in admin audit log.
                        .SetData(0, Col_Reaction, "History Details")
                        .SetData(0, Col_Activity, "Activity")
                        .SetData(0, Col_UserName, "User Name")
                        .SetData(0, Col_FullName, "FullName")
                        .SetData(0, 10, "History Type")
                        .SetData(0, Col_CQMCategory, "CQM Category")
                        .SetData(0, Col_UDI, "UDI")
                        .SetData(0, Col_ConcernStatus, "Concern Status")
                        .SetData(0, Col_ResolvedEndDate, "Resolved/End Date")
                        .SetData(0, Col_AllergySeverity, "Allergy Severity")
                        .SetData(0, Col_AllergyIntelorenceCode, "Allergy Intolerence Type")
                        .Cols(Col_Active).Width = 50
                        .Cols(Col_Reaction).Width = 180
                        .Cols(Col_ActivityDate).Width = 140
                        .Cols(Col_HistoryCategory).Width = 135
                        .Cols(Col_HistoryItem).Width = 160
                        .Cols(Col_Comments).Width = 140
                        .Cols(Col_UserName).Width = 80
                        .Cols(Col_CQMCategory).Width = 200
                        .Cols(Col_UDI).Width = 200
                        .Cols(Col_ConcernStatus).Width = 100
                        .Cols(Col_ResolvedEndDate).Width = 140
                        .Cols(Col_AllergySeverity).Width = 130
                        .Cols(Col_AllergyIntelorenceCode).Width = 200
                        .Cols(Col_Activity).Visible = False
                        .Cols(Col_UserName).Visible = True
                        .Cols(Col_FullName).Visible = False
                        .Cols(10).Width = 0
                        .Cols(10).Visible = False
                        

                    End With

                    For Each drHistory As DataRow In dt.Rows
                        strActivityDate = drHistory(2)
                        strUserAction = drHistory(3)
                        ' strVisitDate = drHistory(HistoryEnum.dtVisitdate)
                        strCategory = drHistory(4)
                        strhistory = drHistory(5)
                        strComment = drHistory(6)
                        strRection_Status = drHistory(7)
                        strActivity = drHistory(8)
                        strUserName = drHistory(24)
                        strhistoryType = drHistory(25)
                        strCQMCategory = drHistory(26)
                        strUDI = drHistory(27)
                        strConcernStatus = drHistory(28)
                        strResolvedEndDate = drHistory(29)
                        If strResolvedEndDate = "1/1/1900" Then
                            strResolvedEndDate = ""
                        End If
                        strAllergySeverity = drHistory(30)
                        strAllergyIntelorenceCode = drHistory(31)
                        FillGrid(strActivityDate, strUserAction, strCategory, strhistory, strComment, strActivity, strUserName, strFullName, strRection_Status, strActive, strReaction, strhistoryType, strCQMCategory, strUDI, strConcernStatus, strResolvedEndDate, strAllergySeverity, strAllergyIntelorenceCode)
                    Next
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Private Sub FillGrid(ByVal strActivityDate As String, ByVal strUserAction As String, ByVal strCategory As String, ByVal strhistory As String, ByVal strComment As String, ByVal strActivity As String, ByVal strUserName As String, ByVal strFullName As String, ByVal strRection_Status As String, ByVal strActive As String, ByVal strReaction As String, ByVal strHistoryType As String, ByVal strCQMCategory As String, ByVal strUDI As String, ByVal strConcernStatus As String, ByVal strResolvedEndDate As String, ByVal strAllergySeverity As String, ByVal strAllergyIntelorenceCode As String)
        Try
            Dim i As Integer


            Dim objclsDB As New clsDoctorsDashBoard
            Dim _categorytype As String = ""
            Dim stronsetActiveStatus As String = ""
            Dim _arrOnsetActive() As String
            Dim IsActive As Boolean = False
            Dim IsOnsetDate As Boolean = False
            Dim dtReaction As DataTable = Nothing  ''change made for patient switching optimization
            Dim dtSmoking As DataTable = Nothing
            Dim ds As DataSet
            ds = objclsDB.Fill_StandardHistoryTypes()

            With c1Audit
                .Rows.Add()
                i = .Rows.Count - 1

                .SetData(i, Col_ActivityDate, strActivityDate)
                .SetData(i, Col_UserAction, strUserAction)

                .SetData(i, Col_HistoryCategory, strCategory)
                .SetData(i, Col_HistoryItem, strhistory)
                .SetData(i, Col_Comments, strComment)
                .SetData(i, Col_Reaction, "")
                .SetData(i, Col_Activity, strActivity)
                .SetData(i, Col_UserName, strUserName)
                .SetData(i, Col_FullName, strFullName)
                .SetData(i, Col_CQMCategory, strCQMCategory)
                .SetData(i, Col_UDI, strUDI)
                .SetData(i, Col_ConcernStatus, strConcernStatus)
                .SetData(i, Col_ResolvedEndDate, strResolvedEndDate)
                .SetData(i, Col_AllergySeverity, strAllergySeverity)
                .SetData(i, Col_AllergyIntelorenceCode, strAllergyIntelorenceCode)

                Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim rgReaction As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i, Col_Reaction, i, Col_Reaction)
                Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(i, Col_Active, i, Col_Active)


                Dim objclsPatientHistory As New clsPatientHistory
                _categorytype = strCategory
                If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Then

                Else
                    If strHistoryType = "" Then
                        _categorytype = strCategory
                        _categorytype = objclsPatientHistory.getHistoryTypefromcategorymaster_Other(_categorytype, ds)
                    Else
                        _categorytype = strHistoryType
                    End If
                End If
                If Not IsNothing(ds) Then
                    ds.Dispose()
                    ds = Nothing
                End If

                IsActive = False
                IsOnsetDate = False
                'Active checkbox is shown for allergies section so this section also included in below condition
                If _categorytype = "OB Medical History" Or _categorytype = "OB Genetic History" Or _categorytype = "OB Infection History" Or _categorytype = "OB Initial Physical Examination" Or _categorytype = "All" Then
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
                                    IsActive = _arrOnsetActive.GetValue(1)
                                End If
                            End If
                        End If
                    End If
                End If
                If Not IsNothing(objclsPatientHistory) Then
                    objclsPatientHistory.Dispose()
                    objclsPatientHistory = Nothing
                End If
                ''If the category is allergy then insert combox and checkbox in flexgrid 
                If IsActive And _categorytype = "All" Then

                    Dim strReaction1 As String = ""
                    Dim strActive1 As String = ""
                    If strRection_Status <> "" Then
                        Dim arr() As String 'Srting Array
                        arr = Split(strRection_Status, "|")
                        strReaction1 = arr.GetValue(0)
                        If (arr.Length > 1) Then
                            strActive1 = arr.GetValue(1)
                        End If
                        arr = Nothing
                    End If

                    Dim strReactions As String = " "
                    ' Dim objclsPatientHistory As clsPatientHistory = Nothing
                    If IsNothing(dtReaction) Then  ''code change for patient switching performance
                        objclsPatientHistory = New clsPatientHistory
                        dtReaction = objclsPatientHistory.GetAllCategory("Reaction")
                    End If

                    If IsNothing(dtReaction) = False Then
                        If strReactions = " " Then  ''condition added  for patient switching performance
                            For m As Int16 = 0 To dtReaction.Rows.Count - 1
                                strReactions = strReactions & "|" & dtReaction.Rows(m)(1)
                            Next
                        End If
                    End If

                    Dim arrReaction As String()
                    arrReaction = strReaction1.Split(vbNewLine)

                    c1Audit.Rows(i).Height = c1Audit.Rows.DefaultSize * arrReaction.Length - 1
                    c1Audit.SetData(i, Col_Reaction, strReaction1)
                    rgActive.StyleNew.DataType = GetType(Boolean)
                    rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                    rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter
                    If strActive1 = "Active" Then
                        .SetCellCheck(i, Col_Active, CheckEnum.Checked)
                    End If

                    arrReaction = Nothing
                    strActive1 = Nothing
                    strReaction1 = Nothing

                    strReactions = Nothing
                    objclsPatientHistory.Dispose()
                    objclsPatientHistory = Nothing

                ElseIf _categorytype = "Fam" Then

                    Dim strFamily As String = ""
                    Dim strFamilyActive As String = ""
                    If strRection_Status <> "" Then
                        Dim arr() As String 'Srting Array
                        Dim arr1() As String 'Srting Array
                        arr = Split(strRection_Status, "|")
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
                            .SetCellCheck(i, Col_Active, CheckEnum.Checked)
                        End If
                    End If

                    Dim arrReaction As String()
                    arrReaction = strFamily.Split(vbNewLine)
                    c1Audit.Rows(i).Height = c1Audit.Rows.DefaultSize * arrReaction.Length - 1
                    c1Audit.SetData(i, Col_Reaction, strFamily)
                ElseIf _categorytype = "OB Initial Physical Examination" Then

                    Dim strExam As String = ""
                    Dim strExamActive As String = ""
                    If strRection_Status <> "" Then
                        Dim arr() As String 'Srting Array

                        arr = Split(strRection_Status, "|")

                        If arr.Length > 0 Then
                            strExam = arr.GetValue(0)
                        End If

                        If (arr.Length > 1) Then
                            strExamActive = arr.GetValue(1)
                        End If
                    End If

                    Dim arrReaction As String()
                    arrReaction = strExam.Split(vbNewLine)
                    c1Audit.Rows(i).Height = c1Audit.Rows.DefaultSize * arrReaction.Length - 1
                    c1Audit.SetData(i, Col_Reaction, strExam)
                ElseIf InStr(strCategory.ToString.ToUpper, "SMOKING", CompareMethod.Text) = 1 Then

                    Dim strSmoking As String = ""
                    Dim strSmokeActive As String = ""
                    If strRection_Status <> "" Then
                        Dim arr() As String 'Srting Array
                        arr = Split(strRection_Status, "|")
                        strSmoking = arr.GetValue(0)
                        If (arr.Length > 1) Then
                            strSmokeActive = arr.GetValue(1)
                        End If
                    End If
                    Dim strSmokings As String = " "
                    If IsNothing(dtSmoking) Then
                        Dim objclsPHistory As clsPatientHistory
                        objclsPHistory = New clsPatientHistory()
                        dtSmoking = objclsPHistory.GetAllCategory("Smoking Status Type")
                        objclsPHistory.Dispose()
                        objclsPHistory = Nothing
                    End If
                    If IsNothing(dtSmoking) = False Then
                        If strSmokings = " " Then
                            For m As Int16 = 0 To dtSmoking.Rows.Count - 1
                                strSmokings = strSmokings & "|" & dtSmoking.Rows(m)(1)
                            Next
                        End If
                    End If
                    Dim arrSmoking As String()
                    arrSmoking = strSmoking.Split(vbNewLine)
                    c1Audit.Rows(i).Height = c1Audit.Rows.DefaultSize * arrSmoking.Length - 1
                    c1Audit.SetData(i, Col_Reaction, strSmoking)
                    strSmokings = Nothing
                    arrSmoking = Nothing

                ElseIf IsActive Then

                    Dim strReaction1 As String = ""
                    Dim strActive1 As String = ""
                    If strRection_Status <> "" Then
                        Dim arr() As String 'Srting Array
                        arr = Split(strRection_Status, "|")
                        strReaction1 = arr.GetValue(0)
                        If (arr.Length > 1) Then
                            strActive1 = arr.GetValue(1)
                        End If
                        arr = Nothing
                    End If
                    rgActive.StyleNew.DataType = GetType(Boolean)
                    rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                    rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

                    If strActive1 = "Active" Then
                        .SetCellCheck(i, Col_Active, CheckEnum.Checked)
                    End If

                    strActive1 = Nothing
                    strReaction1 = Nothing
                End If

                cStyle = Nothing
                rgReaction = Nothing
                rgActive = Nothing
                .Row = i
                .Cols(Col_Reaction).AllowEditing = False
                .Cols(Col_Active).AllowEditing = False

            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub FillVitals(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetVitalHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    c1Audit.Clear()
                    c1Audit.DataSource = dt.DefaultView
                    c1Audit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Audit.AllowSorting = False
                    'CustomGridStyle()

                    ' '' Set Column Header
                    c1Audit.Cols("nVitalHSTID").Caption = "nVitalHSTID"
                    c1Audit.Cols("nVitalID").Caption = "nVitalID"
                    c1Audit.Cols("nVisitID").Caption = "nVisitID"
                    c1Audit.Cols("nPatientID").Caption = "nPatientID"
                    c1Audit.Cols("dtHistoryDateTime").Caption = "Activity DateTime"
                    c1Audit.Cols("dtHistoryDateTime").Width = 150
                    c1Audit.Cols("sUserAction").Caption = "User Action"
                    c1Audit.Cols("dtVitalDate").Caption = "Vital Date"
                    c1Audit.Cols("dtVitalDate").Width = 150
                    c1Audit.Cols("sHeight").Caption = "Height/Length(ft' in'')"
                    c1Audit.Cols("sHeight").Width = 180
                    c1Audit.Cols("dWeightinlbs").Caption = "Weight(lbs)"
                    c1Audit.Cols("dWeightinlbs").Width = 130
                    c1Audit.Cols("dWeightChange").Caption = "Weight Change"
                    c1Audit.Cols("dWeightChange").Width = 130
                    c1Audit.Cols("dBMI").Caption = "BMI"
                    c1Audit.Cols("dWeightinKg").Caption = "Weight(kg)"
                    c1Audit.Cols("dWeightinKg").Width = 130
                    c1Audit.Cols("dTemperature").Caption = "Temperature(F)"
                    c1Audit.Cols("dTemperature").Width = 130
                    c1Audit.Cols("dRespiratoryRate").Caption = "Respiratory Rate"
                    c1Audit.Cols("dRespiratoryRate").Width = 130
                    c1Audit.Cols("dPulsePerMinute").Caption = "Pulse Per Minute"
                    c1Audit.Cols("dPulsePerMinute").Width = 130
                    c1Audit.Cols("dPulseOx").Caption = "Pulse Ox"
                    c1Audit.Cols("dPulseOxSupplement").Caption = "Pulse Ox on O2"
                    c1Audit.Cols("dPulseOxSupplement").Width = 130
                    c1Audit.Cols("dPulseRate").Caption = "O2 Rate"
                    c1Audit.Cols("BPSitting").Caption = "BP Sitting"
                    c1Audit.Cols("BPStanding").Caption = "BP Standing"
                    c1Audit.Cols("sComments").Caption = "Comments"
                    c1Audit.Cols("dHeadCircumferance").Caption = "Head Circum(cm)"
                    c1Audit.Cols("dHeadCircumferance").Width = 130
                    c1Audit.Cols("dStature").Caption = "Stature(cm)"
                    c1Audit.Cols("dStature").Width = 130
                    c1Audit.Cols("dTHRperMin").Caption = "dTHRperMin"
                    c1Audit.Cols("dTHRperMax").Caption = "dTHRperMax"
                    c1Audit.Cols("dTHRMin").Caption = "THR Minimum"
                    c1Audit.Cols("dTHRMin").Width = 130
                    c1Audit.Cols("dTHRMax").Caption = "THR Maximum"
                    c1Audit.Cols("dTHRMax").Width = 130
                    c1Audit.Cols("dTHR").Caption = "THR"
                    c1Audit.Cols("dHeightinInch").Caption = "Height/Length(in)"
                    c1Audit.Cols("dHeightinInch").Width = 150
                    c1Audit.Cols("dHeightinCm").Caption = "Height/Length(cm)"
                    c1Audit.Cols("dHeightinCm").Width = 150
                    c1Audit.Cols("sWeightinLbsOz").Caption = "Weight(lbs oz)"
                    c1Audit.Cols("sWeightinLbsOz").Width = 130
                    c1Audit.Cols("dTemperatureinCelcius").Caption = "Temperature(C)"
                    c1Audit.Cols("dTemperatureinCelcius").Width = 130
                    c1Audit.Cols("nPainLevel").Caption = "Pain Level Current"
                    c1Audit.Cols("nPainLevel").Width = 150
                    c1Audit.Cols("dPEFR1").Caption = "PEFR 1"
                    c1Audit.Cols("dPEFR2").Caption = "PEFR 2"
                    c1Audit.Cols("dPEFR3").Caption = "PEFR 3"
                    c1Audit.Cols("dHeadCircuminInch").Caption = "Head Circum(in)"
                    c1Audit.Cols("dHeadCircuminInch").Width = 130
                    c1Audit.Cols("dStatureinInch").Caption = "Stature(in)"
                    c1Audit.Cols("Site For BP").Caption = "Site For BP"
                    c1Audit.Cols("Last Menstrual Period").Caption = "Last Menstrual Period"
                    c1Audit.Cols("Last Menstrual Period").Width = 180
                    c1Audit.Cols("Neck Circumference (cm)").Caption = "Neck Circumference(cm)"
                    c1Audit.Cols("Neck Circumference (cm)").Width = 180
                    c1Audit.Cols("Neck Circumference (in)").Caption = "Neck Circumference(in)"
                    c1Audit.Cols("Neck Circumference (in)").Width = 180
                    c1Audit.Cols("Left Eye Pressure").Caption = "Left Eye Pressure"
                    c1Audit.Cols("Left Eye Pressure").Width = 130
                    c1Audit.Cols("Right Eye Pressure").Caption = "Right Eye Pressure"
                    c1Audit.Cols("Right Eye Pressure").Width = 130
                    c1Audit.Cols("nPainLevelWithMedication").Caption = "Pain Level With Medication"
                    c1Audit.Cols("nPainLevelWithMedication").Width = 200
                    c1Audit.Cols("nPainLevelWithoutMedication").Caption = "Pain Level Without Medication"
                    c1Audit.Cols("nPainLevelWithoutMedication").Width = 220
                    c1Audit.Cols("nPainLevelWorst").Caption = "Pain Level Worst"
                    c1Audit.Cols("nPainLevelWorst").Width = 150
                    c1Audit.Cols("nODIPercent").Caption = "ODI (%)"
                    c1Audit.Cols("nDAS28").Caption = "DAS 28"
                    c1Audit.Cols("nAuditTrailID").Caption = "nAuditTrailID"
                    c1Audit.Cols("nAuditTrailID").Width = 150
                    c1Audit.Cols("dtActivityDateTime").Caption = "Activity DateTime"
                    c1Audit.Cols("dtActivityDateTime").Width = 150

                    c1Audit.Cols("sActivityCategory").Caption = "Activity Category"
                    c1Audit.Cols("sActivityCategory").Width = 150

                    c1Audit.Cols("sDescription").Caption = "Activity Description"
                    c1Audit.Cols("sDescription").Width = 150


                    c1Audit.Cols("nUserID").Caption = "Audit User ID"
                    c1Audit.Cols("sAuditMachineName").Caption = "Audit Machine Name"
                    c1Audit.Cols("sAuditMachineName").Width = 150

                    c1Audit.Cols("sSoftwareComponent").Caption = "Software Component"
                    c1Audit.Cols("sSoftwareComponent").Width = 150

                    c1Audit.Cols("sOutcome").Caption = "Outcome"
                    c1Audit.Cols("nClinicID").Caption = "ClinicID"
                    c1Audit.Cols("sActivityModule").Caption = "Activity Module"
                    c1Audit.Cols("sActivityModule").Width = 130

                    c1Audit.Cols("sActivityType").Caption = "Activity Type"
                    c1Audit.Cols("sActivityType").Width = 130

                    c1Audit.Cols("nTransactionID").Caption = "Transaction ID"
                    c1Audit.Cols("nTransactionID").Width = 130

                    c1Audit.Cols("nProviderID").Caption = "Provider ID"
                    c1Audit.Cols("nProviderID").Width = 130

                    c1Audit.Cols("sAuditUserName").Caption = "Audit UserName"
                    c1Audit.Cols("sAuditUserName").Width = 130



                    ' '' Set Column Visibility
                    c1Audit.Cols("nVitalHSTID").Visible = False
                    c1Audit.Cols("nVitalID").Visible = False
                    c1Audit.Cols("nVisitID").Visible = False
                    c1Audit.Cols("nPatientID").Visible = False
                    c1Audit.Cols("dtHistoryDateTime").Visible = True
                    c1Audit.Cols("sUserAction").Visible = True
                    c1Audit.Cols("dtVitalDate").Visible = True
                    c1Audit.Cols("sHeight").Visible = True
                    c1Audit.Cols("dWeightinlbs").Visible = True
                    c1Audit.Cols("dWeightChange").Visible = True
                    c1Audit.Cols("dBMI").Visible = True
                    c1Audit.Cols("dWeightinKg").Visible = True
                    c1Audit.Cols("dTemperature").Visible = True
                    c1Audit.Cols("dRespiratoryRate").Visible = True
                    c1Audit.Cols("dPulsePerMinute").Visible = True
                    c1Audit.Cols("dPulseOx").Visible = True
                    c1Audit.Cols("dPulseOxSupplement").Visible = True
                    c1Audit.Cols("dPulseRate").Visible = True
                    c1Audit.Cols("BPSitting").Visible = True
                    c1Audit.Cols("BPStanding").Visible = True
                    c1Audit.Cols("sComments").Visible = True
                    c1Audit.Cols("dHeadCircumferance").Visible = True
                    c1Audit.Cols("dStature").Visible = True
                    c1Audit.Cols("dTHRperMin").Visible = False
                    c1Audit.Cols("dTHRperMax").Visible = False
                    c1Audit.Cols("dTHRMin").Visible = True
                    c1Audit.Cols("dTHRMax").Visible = True
                    c1Audit.Cols("dTHR").Visible = True
                    c1Audit.Cols("dHeightinInch").Visible = True
                    c1Audit.Cols("dHeightinCm").Visible = True
                    c1Audit.Cols("sWeightinLbsOz").Visible = True
                    c1Audit.Cols("dTemperatureinCelcius").Visible = True
                    c1Audit.Cols("nPainLevel").Visible = True
                    c1Audit.Cols("dPEFR1").Visible = True
                    c1Audit.Cols("dPEFR2").Visible = False
                    c1Audit.Cols("dPEFR3").Visible = False
                    c1Audit.Cols("dHeadCircuminInch").Visible = True
                    c1Audit.Cols("dStatureinInch").Visible = True
                    c1Audit.Cols("Site For BP").Visible = True
                    c1Audit.Cols("Last Menstrual Period").Visible = True
                    c1Audit.Cols("Neck Circumference (cm)").Visible = True
                    c1Audit.Cols("Neck Circumference (in)").Visible = True
                    c1Audit.Cols("Left Eye Pressure").Visible = True
                    c1Audit.Cols("Right Eye Pressure").Visible = True
                    c1Audit.Cols("nPainLevelWithMedication").Visible = True
                    c1Audit.Cols("nPainLevelWithoutMedication").Visible = True
                    c1Audit.Cols("nPainLevelWorst").Visible = True
                    c1Audit.Cols("nODIPercent").Visible = True
                    c1Audit.Cols("nDAS28").Visible = True
                    c1Audit.Cols("nAuditTrailID").Visible = False
                    c1Audit.Cols("dtActivityDateTime").Visible = False
                    c1Audit.Cols("sActivityCategory").Visible = False
                    c1Audit.Cols("sDescription").Visible = False

                    c1Audit.Cols("nUserID").Visible = False
                    c1Audit.Cols("sAuditMachineName").Visible = False
                    c1Audit.Cols("sSoftwareComponent").Visible = False
                    c1Audit.Cols("sOutcome").Visible = False
                    c1Audit.Cols("nClinicID").Visible = False
                    c1Audit.Cols("sActivityModule").Visible = False
                    c1Audit.Cols("sActivityType").Visible = False
                    c1Audit.Cols("nTransactionID").Visible = False
                    c1Audit.Cols("nProviderID").Visible = False
                    c1Audit.Cols("sAuditUserName").Visible = False

                    ' ''''Set Column Header alignment

                    For i As Integer = 0 To c1Audit.Cols.Count - 1
                        c1Audit.Cols(i).TextAlign = TextAlignEnum.LeftCenter
                    Next
                    c1Audit.Cols("dtVitalDate").DataType = GetType(String)
                    c1Audit.Cols("Last Menstrual Period").DataType = GetType(String)
                    c1Audit.Cols("dtHistoryDateTime").DataType = GetType(String)
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try

    End Sub

    Public Sub FillCarePlan(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetCarePlanHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    c1Audit.Clear()
                    c1Audit.DataSource = dt.DefaultView
                    c1Audit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Audit.AllowSorting = False

                    c1Audit.Cols("nCarePlanHSTID").Caption = "nCarePlanHSTID"
                    c1Audit.Cols("nId").Caption = "nId"
                    c1Audit.Cols("CarePlan_Patient_ID").Caption = "CarePlan_Patient_ID"
                    c1Audit.Cols("Problem").Caption = "Problem"
                    c1Audit.Cols("Goal").Caption = "Goal"
                    c1Audit.Cols("Note").Caption = "Note"
                    c1Audit.Cols("Instruction").Caption = "Instruction"
                    c1Audit.Cols("CreatedDate").Caption = "Created Date"
                    c1Audit.Cols("IsActive").Caption = "Is Active"
                    c1Audit.Cols("sUserAction").Caption = "User Action"
                    c1Audit.Cols("dtHistoryDateTime").Caption = "Activity Date & Time"
                    c1Audit.Cols("nAuditTrailID").Caption = "nAuditTrailID"
                    c1Audit.Cols("dtActivityDateTime").Caption = "dtActivityDateTime"
                    c1Audit.Cols("sActivityCategory").Caption = "sActivityCategory"
                    c1Audit.Cols("sDescription").Caption = "sDescription"
                    c1Audit.Cols("nUserID").Caption = "nUserID"
                    c1Audit.Cols("sAuditMachineName").Caption = "sAuditMachineName"
                    c1Audit.Cols("sSoftwareComponent").Caption = "sSoftwareComponent"
                    c1Audit.Cols("sOutcome").Caption = "sOutcome"
                    c1Audit.Cols("nClinicID").Caption = "nClinicID"
                    c1Audit.Cols("sActivityModule").Caption = "sActivityModule"
                    c1Audit.Cols("sActivityType").Caption = "sActivityType"
                    c1Audit.Cols("nTransactionID").Caption = "nTransactionID"
                    c1Audit.Cols("nProviderID").Caption = "nProviderID"
                    c1Audit.Cols("sAuditUserName").Caption = "sAuditUserName"

                    c1Audit.Cols("dtHistoryDateTime").Width = 150
                    c1Audit.Cols("IsActive").Width = 50
                    c1Audit.Cols("sUserAction").Width = 175
                    c1Audit.Cols("CreatedDate").Width = 130
                    c1Audit.Cols("Instruction").Width = 125
                    c1Audit.Cols("Goal").Width = 150
                    c1Audit.Cols("Note").Width = 100
                    c1Audit.Cols("Problem").Width = 150
                    c1Audit.Cols("sUserAction").Width = 125

                    c1Audit.Cols("nCarePlanHSTID").Visible = False
                    c1Audit.Cols("nId").Visible = False
                    c1Audit.Cols("CarePlan_Patient_ID").Visible = False
                    c1Audit.Cols("Problem").Visible = True
                    c1Audit.Cols("Goal").Visible = True
                    c1Audit.Cols("Note").Visible = True
                    c1Audit.Cols("Instruction").Visible = True
                    c1Audit.Cols("CreatedDate").Visible = True
                    c1Audit.Cols("IsActive").Visible = True
                    c1Audit.Cols("sUserAction").Visible = True
                    c1Audit.Cols("dtHistoryDateTime").Visible = True
                    c1Audit.Cols("nAuditTrailID").Visible = False
                    c1Audit.Cols("dtActivityDateTime").Visible = False
                    c1Audit.Cols("sActivityCategory").Visible = False
                    c1Audit.Cols("sDescription").Visible = False
                    c1Audit.Cols("nUserID").Visible = False
                    c1Audit.Cols("sAuditMachineName").Visible = False
                    c1Audit.Cols("sSoftwareComponent").Visible = False
                    c1Audit.Cols("sOutcome").Visible = False
                    c1Audit.Cols("nClinicID").Visible = False
                    c1Audit.Cols("sActivityModule").Visible = False
                    c1Audit.Cols("sActivityType").Visible = False
                    c1Audit.Cols("nTransactionID").Visible = False
                    c1Audit.Cols("nProviderID").Visible = False
                    c1Audit.Cols("sAuditUserName").Visible = False
                    For i As Integer = 0 To c1Audit.Cols.Count - 1
                        c1Audit.Cols(i).TextAlign = TextAlignEnum.LeftCenter
                    Next

                    c1Audit.Cols("CreatedDate").DataType = GetType(String)
                    c1Audit.Cols("dtHistoryDateTime").DataType = GetType(String)
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub FillMedication(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetMedicationHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    c1Audit.DataSource = dt
                    c1Audit.Cols("Activity DateTime").Caption = "Activity DateTime"
                    c1Audit.Cols("User Action").Caption = "User Action"
                    c1Audit.Cols("dtmedicationdate").Caption = "Updated"
                    c1Audit.Cols("username").Caption = "Updated By"
                    c1Audit.Cols("smedication").Caption = "Drug"
                    c1Audit.Cols("Prescriber").Caption = "Prescriber"
                    c1Audit.Cols("dtstartdate").Caption = "Start Date"
                    c1Audit.Cols("dtenddate").Caption = "End Date"
                    c1Audit.Cols("sstatus").Caption = "Status"
                    c1Audit.Cols("sfrequency").Caption = "Patient Directions"
                    c1Audit.Cols("sduration").Caption = "Duration"
                    c1Audit.Cols("samount").Caption = "Quantity"
                    c1Audit.Cols("sRefills").Caption = "Refills"
                    c1Audit.Cols("sMethod").Caption = "Issue Method"
                    c1Audit.Cols("Pharmacy").Caption = "Pharmacy"

                    c1Audit.Cols("username").Visible = True

                    c1Audit.AllowEditing = False
                    c1Audit.AllowSorting = True

                    c1Audit.Cols("Activity DateTime").DataType = GetType(String)

                    Dim _width As Integer = 0

                    _width = Convert.ToInt32(c1Audit.Width)

                    c1Audit.Cols("Activity DateTime").Width = _width * 0.13
                    c1Audit.Cols("User Action").Width = _width * 0.09
                    c1Audit.Cols("dtmedicationdate").Width = _width * 0.08
                    c1Audit.Cols("smedication").Width = _width * 0.2
                    c1Audit.Cols("Prescriber").Width = _width * 0.12
                    c1Audit.Cols("dtstartdate").Width = _width * 0.08
                    c1Audit.Cols("dtenddate").Width = _width * 0.08
                    c1Audit.Cols("sstatus").Width = _width * 0.07
                    c1Audit.Cols("username").Width = _width * 0.09
                    c1Audit.Cols("sfrequency").Width = _width * 0.12
                    c1Audit.Cols("sduration").Width = _width * 0.07
                    c1Audit.Cols("samount").Width = _width * 0.07
                    c1Audit.Cols("sRefills").Width = _width * 0.05
                    c1Audit.Cols("sMethod").Width = _width * 0.095
                    c1Audit.Cols("Pharmacy").Width = _width * 0.09
                    _width = Nothing
                    c1Audit.Cols("samount").TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub FillPrescription(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetPrescriptionHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    With c1Audit

                        .AllowSorting = True
                        .Visible = True
                        .Cols.Count = 16
                        .Rows.Count = 1
                        .SetData(0, 0, "Activity DateTime")
                        .SetData(0, 1, "User Action")
                        .SetData(0, 2, "Date")
                        .SetData(0, 3, "Drug")
                        .SetData(0, 4, "Prescriber")
                        .SetData(0, 5, "Patient Directions")
                        .SetData(0, 6, "Duration")
                        .SetData(0, 7, "Quantity") '
                        .SetData(0, 8, "Refills")
                        .SetData(0, 9, "Issue Method")
                        .SetData(0, 10, "Status")
                        .SetData(0, 11, "eRx Message")
                        .SetData(0, 12, "User")
                        .SetData(0, 13, "Allow Substitution")
                        .SetData(0, 14, "Pharmacy Note")
                        .SetData(0, 15, "Prescription Id")

                        Dim _width As Integer = 0

                        _width = Convert.ToInt32(c1Audit.Width)

                        .Cols(0).Width = _width * 0.13
                        .Cols(1).Width = _width * 0.09
                        .Cols(2).Width = _width * 0.08
                        .Cols(3).Width = _width * 0.2
                        .Cols(4).Width = _width * 0.13
                        .Cols(5).Width = _width * 0.12
                        .Cols(6).Width = _width * 0.07
                        .Cols(7).Width = _width * 0.07
                        .Cols(8).Width = _width * 0.05
                        .Cols(9).Width = _width * 0.095
                        .Cols(10).Width = _width * 0.1
                        .Cols(11).Width = _width * 0.0
                        .Cols(12).Width = _width * 0.08
                        .Cols(13).Width = _width * 0.14
                        .Cols(14).Width = _width * 0.1
                        .Cols(15).Width = 0 '_width * 0.1

                        _width = Nothing

                        .Cols(0).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(1).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(2).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(3).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(4).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(5).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(6).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(7).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(8).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(9).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(10).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(11).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(12).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(13).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(14).TextAlign = TextAlignEnum.LeftCenter
                        .Cols(15).TextAlign = TextAlignEnum.LeftCenter

                        If IsNothing(dt) = False Then
                            For i As Int16 = 0 To dt.Rows.Count - 1
                                Dim r As C1.Win.C1FlexGrid.Row
                                r = .Rows.Add()
                                r.Height = 20
                                .SetData(r.Index, 0, dt.Rows(i)("Activity DateTime"))
                                .SetData(r.Index, 1, dt.Rows(i)("User Action"))
                                .SetData(r.Index, 2, CType(dt.Rows(i)("dtPrescriptionDate"), Date).ToShortDateString())
                                .SetData(r.Index, 3, dt.Rows(i)("sMedication"))
                                .SetData(r.Index, 4, dt.Rows(i)("Prescriber"))
                                .SetData(r.Index, 5, dt.Rows(i)("sFrequency"))
                                .SetData(r.Index, 6, dt.Rows(i)("sDuration"))
                                .SetData(r.Index, 7, dt.Rows(i)("sAmount"))
                                .SetData(r.Index, 8, dt.Rows(i)("sRefills"))
                                .SetData(r.Index, 9, dt.Rows(i)("sMethod"))
                                .SetData(r.Index, 10, dt.Rows(i)("eRxStatus"))
                                .SetData(r.Index, 11, dt.Rows(i)("eRxStatusMessage"))
                                .SetData(r.Index, 12, dt.Rows(i)("UserName"))
                                .SetData(r.Index, 13, dt.Rows(i)("AllowSubstitue"))
                                Dim str As String = dt.Rows(i)("sNotes")
                                .SetData(r.Index, 14, str.Replace(Environment.NewLine, " "))
                                .SetData(r.Index, 15, dt.Rows(i)("PrescriptionID"))
                                r = Nothing
                            Next
                        End If
                    End With
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

    Public Sub FillImplantableDevicesAudit(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()

        dt = New DataTable
        Try
            dt = oclsAudit.GetImplantableDevicesHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    c1Audit.Clear()
                    c1Audit.DataSource = dt.DefaultView
                    c1Audit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Audit.AllowSorting = True
                    c1Audit.Cols(0).Width = 0
                    c1Audit.Cols(1).Width = 0
                    c1Audit.Cols(2).Width = 0
                    c1Audit.Cols("Activity DateTime").Width = 125
                    c1Audit.Cols("Device ID").Width = 135
                    c1Audit.Cols("Implant Date").Width = 110
                    c1Audit.Cols("Issuing Agency").Width = 130
                    c1Audit.Cols("Brand Name").Width = 130
                    c1Audit.Cols("Company Name").Width = 130
                    c1Audit.Cols("Version/Model").Width = 130
                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try

    End Sub
    Public Sub FillPatientEducationAudit(ByVal nTransactionID As Int64, ByVal sActivityType As String)
        oclsAudit = New clsAuditHistory()
        dt = New DataTable
        Try
            dt = oclsAudit.GetPatientEducationHistory(nTransactionID, sActivityType)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then

                    c1Audit.Clear()
                    c1Audit.DataSource = dt.DefaultView
                    c1Audit.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
                    c1Audit.AllowSorting = True
                    c1Audit.Cols("Activity DateTime").Width = 135
                    c1Audit.Cols("Template Name").Width = 140
                    c1Audit.Cols("Document Url").Width = 160
                    c1Audit.Cols("Resource Category").Width = 130

                Else
                    MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MessageBox.Show("No Audit History found for this record.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try

    End Sub
#End Region
    Private Sub c1Audit_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1Audit.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub c1PatientAudit_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles c1PatientAudit.MouseMove
        Try
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, CType(sender, C1.Win.C1FlexGrid.C1FlexGrid), e.Location)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tsbtn_Close_Click(sender As System.Object, e As System.EventArgs) Handles tsbtn_Close.Click
        Try
            If dt IsNot Nothing Then
                dt.Clear()
                dt.Dispose()
                dt = Nothing
            End If

            If oclsAudit IsNot Nothing Then
                oclsAudit = Nothing
            End If
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub

  
End Class