Imports System.Windows.Forms
Imports gloSettings
Imports C1.Win.C1FlexGrid


Public Class frmFinalizeReconcileList
#Region "Variable Declaration"
    Private _PatientID As Int64
    Private _ListType As String
    Private _strListID As String
    Private _strSkippedID As String
    Dim _dtReconcile As New DataTable
    Dim _UserName As String = String.Empty
    Dim _UserID As Long = 0
    Dim _MedicationVisitID As Long = 0
    Dim flag As Boolean = False
    Dim _LoginProviderID As Long = 0

    Public Enum FormAction
        Accepted
        Closed
        None
    End Enum
    Private ActionPerformed As FormAction = FormAction.None


    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""
#End Region

#Region "Constants--"
    Private Const COL_Med_Select As Integer = 0
    Private Const COL_Med_Dup As Integer = 1
    Private Const COL_Med_ListName As Integer = 2
    Private Const Col_MedicationID As Integer = 3
    Private Const Col__Med_ListID As Integer = 4

    Private Const Col_PatientID As Integer = 5
    Private Const Col_Med_Source As Integer = 6
    Private Const Col_Med_MedicationDate As Integer = 7
    Private Const Col_Med_Medication As Integer = 8
    Private Const Col_Med_Drug As Integer = 9
    Private Const Col_Med_StartDate As Integer = 10
    Private Const Col_Med_EndDate As Integer = 11
    Private Const Col_Med_Frequency As Integer = 12

    Private Const Col_Med_Amount As Integer = 13
    Private Const Col_Med_Refills As Integer = 14
    Private Const Col_Med_DrugForm As Integer = 15
    Private Const Col_Med_Status As Integer = 16
    Private Const Col_Med_sNDCCode As Integer = 17
    Private Const Col_Med_sRxNormCodes As Integer = 18
    Private Const Col_Med_sGenericName As Integer = 19
    Private Const Col_Med_UserName As Integer = 20
    Private Const Col_Med_Skip As Integer = 21
    Private Const Col_Med_sDosage As Integer = 22
    Private Const Col_Med_sRoute As Integer = 23



    Private Const Col_Med_sStrengthUnit As Integer = 24
    Private Const Col_Med_VisitDate As Integer = 25
    ''
    Private Const Col_Med_Reason As Integer = 26
    Private Const Col_Med_Mpid As Integer = 27
    Private Const Col_Med_PrescriptionID As Integer = 28
    Private Const Col_Med_MedicationID As Integer = 29
    Private Const Col_Med_Renewed As Integer = 30
    Private Const Col_Med_IsNarcotic As Integer = 31
    Private Const Col_Med_PBMName As Integer = 32
    Private Const Col_Med_bMaySubstitute As Integer = 33
    Private Const Col_Med_Rx_DMSID As Integer = 34
    Private Const Col_Med_Method As Integer = 35
    Private Const Col_Med_PrescriberNotes As Integer = 36
    Private Const Col_Med_Duration As Integer = 37
    Private Const Col_Med_RxNotes As Integer = 38
    Private Const Col_Med_RxnDrugID As Integer = 39
    '--------------------------------------------------
    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Const Col_Med_Rx_sNCPDPID As Integer = 40
    Private Const Col_Med_Rx_sName As Integer = 41
    Private Const Col_Med_Rx_sAddressline1 As Integer = 42
    Private Const Col_Med_Rx_sAddressline2 As Integer = 43
    Private Const Col_Med_Rx_sCity As Integer = 44
    Private Const Col_Med_Rx_sState As Integer = 45
    Private Const Col_Med_Rx_sZip As Integer = 46
    Private Const Col_Med_Rx_sEmail As Integer = 47
    Private Const Col_Med_Rx_sPhone As Integer = 48
    Private Const Col_Med_Rx_sFax As Integer = 49

    '------------------------------------ --------------

    Private Const COL_Prob_Select As Integer = 0
    Private Const COL_Prob_Dup As Integer = 1
    Private Const COL_Prob_ListName As Integer = 2
    Private Const Col_nProblemID As Integer = 3
    Private Const COL_Prob_nListID As Integer = 4
    Private Const Col_Prob_nPatientID As Integer = 5
    Private Const Col_Prob_Source As Integer = 6
    Private Const Col_Prob_dtLastUpdated As Integer = 7
    Private Const Col_Prob_Complaint As Integer = 8
    Private Const Col_Prob_Diagnosis As Integer = 9
    Private Const Col_Prob_dtDOS As Integer = 10
    Private Const Col_Prob_Status As Integer = 11
    Private Const Col_Prob_sConceptID As Integer = 12
    Private Const Col_Prob_ResolveDate As Integer = 13
    Private Const Col_Prob_User As Integer = 14
    Private Const Col_Prob_Skipped As Integer = 15
    Private Const Col_Prob_UserID As Integer = 16
    Private Const Col_Prob_Immediacy As Integer = 17
    Private Const Col_Prob_nProblemStatus As Integer = 18
    Private Const Col_Prob_sICD9Code As Integer = 19
    Private Const Col_Prob_sICD9Desc As Integer = 20
    Private Const Col_Prob_nImmediacy As Integer = 21
    Private Const Col_Prob_ConcernStartDate As Integer = 22
    Private Const Col_Prob_ConcernEndDate As Integer = 23
    Private Const Col_Prob_ConcernStatus As Integer = 24




    Private Const COL_All_Select As Integer = 0
    Private Const COL_All_Dup As Integer = 1
    Private Const COL_All_ListName As Integer = 2
    Private Const Col_All_nHistoryID As Integer = 3
    Private Const COL_All_nListID As Integer = 4
    Private Const Col_All_nPatientID As Integer = 5
    Private Const Col_All_sSourceName As Integer = 6
    Private Const Col_All_dtLastUpdated As Integer = 7
    Private Const Col_All_sHistoryItem As Integer = 8
    Private Const Col_All_sReaction As Integer = 9
    Private Const Col_All_Status As Integer = 10
    Private Const Col_All_DOEAllergy As Integer = 11
    Private Const Col_All_sConceptID As Integer = 12
    Private Const Col_All_sTranID1 As Integer = 13
    Private Const Col_All_sUserName As Integer = 14
    Private Const Col_All_bIsSkipped As Integer = 15
    Private Const Col_All_sDrugName As Integer = 16
    Private Const Col_All_sNDCCode As Integer = 17
    Private Const Col_All_sICD9 As Integer = 18
    Private Const Col_All_sCPT As Integer = 19
    Private Const Col_All_sHistoryCategory As Integer = 20

    Private Const Col_All_dtOnsetDate As Integer = 21
    Private Const Col_All_nRowOrder As Integer = 22
    Private Const Col_All_VisitDate As Integer = 23
    Private Const Col_All_DrugID As Integer = 24
    Private Const Col_All_MedicalConditionID As Integer = 25
    Private Const Col_All_DescriptionID As Integer = 26
    Private Const Col_All_SnomedID As Integer = 27
    Private Const Col_All_Description As Integer = 28
    Private Const Col_All_HistoryType As Integer = 29
    Private Const Col_All_ConcernEndDate As Integer = 30
    Private Const Col_All_ObservationEndDate As Integer = 31
    Private Const Col_All_ConcernStatus As Integer = 32
    Private Const Col_All_Severity As Integer = 33
    ''
    Private Const Col_Rec_Source As Integer = 5
#End Region

    Public Property SelectedAction() As FormAction
        Get
            Return ActionPerformed
        End Get
        Set(ByVal value As FormAction)
            ActionPerformed = value
        End Set
    End Property
    Public Property MedicationVisitID() As Long
        Get
            Return _MedicationVisitID
        End Get
        Set(ByVal value As Long)
            _MedicationVisitID = value
        End Set
    End Property
#Region "Form Constructor"
    Public Sub New(ByVal dtReconcile As DataTable, ByVal ListType As String, ByVal PatientID As Int64, ByVal strListID As String, ByVal strSkippedID As String, ByVal LoginUser As String, ByVal LoginID As Long, ByVal LoginProviderID As Long)

        ' This call is required by the designer.
        InitializeComponent()
        _dtReconcile = dtReconcile
        _ListType = ListType
        _PatientID = PatientID
        _strListID = strListID
        _strSkippedID = strSkippedID
        _UserName = LoginUser
        _UserID = LoginID
        _LoginProviderID = LoginProviderID

        SelectedAction = FormAction.None

    End Sub
#End Region

#Region "Form_Load"

    Private Sub frmFinalizeReconcileList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveReconcileColumnWidth()
    End Sub
    Private Sub frmFinalizeReconcileList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        c1ReconcileList.AllowSorting = False
        c1ReconcileList.AllowDragging = False
        LoadFinalizeRecords()
    End Sub
#End Region

#Region "LoadFinalizeRecords"
    Private Sub LoadFinalizeRecords()

        ' Dim objgloCCDReconcilation As New gloCCDReconcilation
        Dim strReaction As String
        Dim strActive As String

        Dim arr() As String 'Srting Array
        Dim strRection_Status As String = ""
        Dim arrReaction As String()
        Try
            If (Not IsNothing(_dtReconcile)) Then
                If (_dtReconcile.Columns.Contains("Description")) Then
                    Dim drr As DataRow() = _dtReconcile.Select("TRIM(Description)<>'' and select=0")
                    If (drr.Length > 0) Then
                        'blnfoundnokwn = True
                        Dim inddesc As Integer = _dtReconcile.Columns.IndexOf("Description")
                        Dim indupdated As Integer = _dtReconcile.Columns.IndexOf("Last Updated")
                        If (inddesc < indupdated) Then
                            _dtReconcile.Columns("Description").SetOrdinal(indupdated)
                            _dtReconcile.Columns("Last Updated").SetOrdinal(inddesc)
                        End If
                    End If
                End If
            End If
            c1ReconcileList.DataSource = _dtReconcile.DefaultView

            Dim k As Integer = 1

            For k = 1 To c1ReconcileList.Rows.Count - 1
                If _ListType = "Medication" Then



                    If Convert.ToString(c1ReconcileList.GetData(k, COL_Med_Select)) = "0" Then
                        c1ReconcileList.Rows(k).Visible = False
                    End If

                ElseIf _ListType = "Problem" Then


                    If Convert.ToString(c1ReconcileList.GetData(k, COL_Prob_Select)) = "0" Then
                        c1ReconcileList.Rows(k).Visible = False
                    End If



                ElseIf _ListType = "Allergy" Then


                    If Convert.ToString(c1ReconcileList.GetData(k, COL_All_Select)) = "0" Then
                        c1ReconcileList.Rows(k).Visible = False
                    End If

                    strRection_Status = Convert.ToString(c1ReconcileList.GetData(k, Col_All_sReaction))
                    arr = Split(strRection_Status, "|")
                    If arr.Length = 2 Then
                        strReaction = arr.GetValue(0)
                        strActive = arr.GetValue(1)
                    Else
                        strReaction = strRection_Status
                        strActive = False
                    End If

                    arrReaction = strReaction.Split(vbNewLine)
                    'dsHistory.Tables("History").Rows(_Row)(Col_HsReaction) = strReaction
                    c1ReconcileList.SetData(k, Col_All_sReaction, strReaction)


                    c1ReconcileList.Rows(k).Height = c1ReconcileList.Rows.DefaultSize * arrReaction.Length - 1

                    ''''

                End If
                c1ReconcileList.Rows(k)("Last Updated") = DateTime.Now

                strReaction = Nothing
                strActive = Nothing

                arr = Nothing
                strRection_Status = Nothing
                arrReaction = Nothing
            Next

            DesignGrid()

            If _ListType = "Problem" Then
                Me.Text = "Patients Active Problems"
                lblListType.Text = "Patients Active Problems"
            ElseIf _ListType = "Medication" Then
                Me.Text = "Patients Active Medications"
                lblListType.Text = "Patients Active Medications"
                ReconcileListEditing()
            ElseIf _ListType = "Allergy" Then
                Me.Text = "Patients Active Med Allergies"
                lblListType.Text = "Patients Active Med Allergies"

            End If

            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, gloLibCCDGeneral.Connectionstring, "gloEMR")

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            'If IsNothing(objgloCCDReconcilation) = False Then
            '    objgloCCDReconcilation = Nothing
            'End If
        End Try
    End Sub
#End Region

#Region "Design Grid"
    Private Sub DesignGrid()
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(gloLibCCDGeneral.Connectionstring)
        c1ReconcileList.AllowEditing = False
        c1ReconcileList.AllowEditing = False
        Try


            If _ListType = "Medication" Then

                c1ReconcileList.Cols(Col_Med_Source).Caption = "Source"
                c1ReconcileList.Cols(Col_Med_MedicationDate).Caption = "Last Updated"
                c1ReconcileList.Cols(Col_Med_Drug).Caption = "Drug"
                c1ReconcileList.Cols(Col_Med_StartDate).Caption = "Start Date"
                c1ReconcileList.Cols(Col_Med_EndDate).Caption = "End Date"
                c1ReconcileList.Cols(Col_Med_Frequency).Caption = "Frequency"

                c1ReconcileList.Cols(Col_Med_Amount).Caption = "Amount"
                c1ReconcileList.Cols(Col_Med_Refills).Caption = "Refills"

                c1ReconcileList.Cols(Col_Med_Status).Caption = "Status"

                c1ReconcileList.Cols(Col_Med_UserName).Caption = "User"

                c1ReconcileList.Cols(Col_Med_sNDCCode).Caption = "NDC Code"
                c1ReconcileList.Cols(Col_Med_sRxNormCodes).Caption = "RxNorm Code"
                c1ReconcileList.Cols(Col_Med_sGenericName).Caption = "Generic Name"
                c1ReconcileList.Cols(Col_Med_Rx_sNCPDPID).Caption = "Rx_sNCPDPID"
                c1ReconcileList.Cols(Col_Med_Rx_sName).Caption = "Rx_sName"
                c1ReconcileList.Cols(Col_Med_Rx_sAddressline1).Caption = "Rx_sAddressline1"
                c1ReconcileList.Cols(Col_Med_Rx_sAddressline2).Caption = "Rx_sAddressline2"
                c1ReconcileList.Cols(Col_Med_Rx_sCity).Caption = "Rx_sCity"
                c1ReconcileList.Cols(Col_Med_Rx_sState).Caption = "Rx_sState"
                c1ReconcileList.Cols(Col_Med_Rx_sZip).Caption = "Rx_sZip"
                c1ReconcileList.Cols(Col_Med_Rx_sPhone).Caption = "Rx_sEmail"
                c1ReconcileList.Cols(Col_Med_Rx_sFax).Caption = "Rx_sFax"
                c1ReconcileList.Cols(Col_Med_Rx_sEmail).Caption = "Rx_sPhone"

                c1ReconcileList.Name = "Meds_FinalizeList"

                ''If new column is added in grid the add the column in if condition to set width 
                ''This code executes only ones and set width for the newly added column.
                If (c1ReconcileList.Cols.Count <> GetGridColumnWidthCount(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID)) Then
                    Dim _SettingWidthList As New List(Of String)
                    _SettingWidthList = GetGridColumnWidthList(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID)
                    If _SettingWidthList IsNot Nothing Then
                        If _SettingWidthList.Count > 0 Then
                            For i As Integer = 0 To c1ReconcileList.Cols.Count - 1
                                If i = Col_Med_sRxNormCodes Then
                                    _SettingWidthList.Insert(i, "90")
                                ElseIf i = Col_Med_sGenericName Then
                                    _SettingWidthList.Insert(i, "90")
                                End If
                            Next
                        End If
                    End If

                    If c1ReconcileList.Cols.Count = _SettingWidthList.Count Then
                        SaveGridColumnWidthList(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID, _SettingWidthList)
                    End If
                End If
                If (ogloSettings.LoadGridColumnWidth(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then
                    c1ReconcileList.Cols(COL_Med_ListName).Width = 0
                    c1ReconcileList.Cols(Col_Med_Skip).Width = 0
                    c1ReconcileList.Cols(Col_Med_sDosage).Width = 0
                    c1ReconcileList.Cols(Col_Med_sRoute).Width = 0
                    c1ReconcileList.Cols(Col_Med_sNDCCode).Width = 100
                    c1ReconcileList.Cols(COL_Med_Select).Width = 0
                    c1ReconcileList.Cols(COL_Med_Dup).Width = 0
                    c1ReconcileList.Cols(Col_MedicationID).Width = 0
                    c1ReconcileList.Cols(Col__Med_ListID).Width = 0
                    c1ReconcileList.Cols(Col_PatientID).Width = 0
                    c1ReconcileList.Cols(Col_Med_sStrengthUnit).Width = 0
                    c1ReconcileList.Cols(Col_Med_Amount).Width = 80
                    c1ReconcileList.Cols(Col_Med_Refills).Width = 80
                    c1ReconcileList.Cols(Col_Med_UserName).Width = 80
                    c1ReconcileList.Cols(Col_Med_StartDate).Width = 80
                    c1ReconcileList.Cols(Col_Med_EndDate).Width = 80
                    c1ReconcileList.Cols(Col_Med_Frequency).Width = 80
                    c1ReconcileList.Cols(Col_Med_VisitDate).Width = 0
                    c1ReconcileList.Cols(Col_Med_Reason).Width = 0
                    c1ReconcileList.Cols(Col_Med_Mpid).Width = 0
                    c1ReconcileList.Cols(Col_Med_PrescriptionID).Width = 0
                    c1ReconcileList.Cols(Col_Med_MedicationID).Width = 0
                    c1ReconcileList.Cols(Col_Med_Renewed).Width = 0
                    c1ReconcileList.Cols(Col_Med_IsNarcotic).Width = 0
                    c1ReconcileList.Cols(Col_Med_PBMName).Width = 0
                    c1ReconcileList.Cols(Col_Med_bMaySubstitute).Width = 0
                    c1ReconcileList.Cols(Col_Med_Rx_DMSID).Width = 0

                    c1ReconcileList.Cols(Col_Med_Method).Width = 0
                    c1ReconcileList.Cols(Col_Med_PrescriberNotes).Width = 0
                    c1ReconcileList.Cols(Col_Med_Duration).Width = 0
                    c1ReconcileList.Cols(Col_Med_RxNotes).Width = 0
                    c1ReconcileList.Cols(Col_Med_RxnDrugID).Width = 0
                    c1ReconcileList.Cols(Col_Med_Medication).Width = 0
                    c1ReconcileList.Cols(Col_Med_sRxNormCodes).Width = 90
                    c1ReconcileList.Cols(Col_Med_sGenericName).Width = 90
                End If

            ElseIf _ListType = "Problem" Then

                c1ReconcileList.Cols(Col_Prob_dtDOS).Caption = "DOS"
                c1ReconcileList.Cols(Col_Prob_Complaint).Caption = "Complaint"
                c1ReconcileList.Cols(Col_Prob_Diagnosis).Caption = "Diagnosis"
                c1ReconcileList.Cols(Col_Prob_Status).Caption = "Status"
                c1ReconcileList.Cols(Col_Prob_sConceptID).Caption = "SnoMed CT ID"
                c1ReconcileList.Cols(Col_Prob_User).Caption = "User"
                c1ReconcileList.Cols(Col_Prob_Skipped).Caption = "Keep"
                c1ReconcileList.Name = "Probs_FinalizeList"

                c1ReconcileList.Cols(Col_Prob_ConcernStartDate).Width = 0
                c1ReconcileList.Cols(Col_Prob_ConcernEndDate).Width = 0
                c1ReconcileList.Cols(Col_Prob_ConcernStatus).Width = 0

                If (ogloSettings.LoadGridColumnWidth(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then
                    c1ReconcileList.Cols(COL_Prob_ListName).Width = 0
                    c1ReconcileList.Cols(COL_Prob_Dup).Width = 0
                    c1ReconcileList.Cols(COL_Prob_Select).Width = 0
                    c1ReconcileList.Cols(Col_nProblemID).Width = 0
                    c1ReconcileList.Cols(COL_Prob_nListID).Width = 0
                    c1ReconcileList.Cols(Col_Prob_nPatientID).Width = 0
                    c1ReconcileList.Cols(Col_Prob_Skipped).Width = 0
                    c1ReconcileList.Cols(Col_Prob_Immediacy).Width = 0
                    c1ReconcileList.Cols(Col_Prob_nProblemStatus).Width = 0
                    c1ReconcileList.Cols(Col_Prob_sConceptID).Width = 200
                    c1ReconcileList.Cols(Col_Prob_sICD9Code).Width = 0
                    c1ReconcileList.Cols(Col_Prob_sICD9Desc).Width = 0
                    c1ReconcileList.Cols(Col_Prob_nImmediacy).Width = 0
                    c1ReconcileList.Cols(Col_Prob_UserID).Width = 0
                    c1ReconcileList.Cols(Col_Prob_Complaint).Width = 150
                    c1ReconcileList.Cols(Col_Prob_Diagnosis).Width = 200
                    c1ReconcileList.Cols(Col_Prob_sConceptID).Width = 100
                    c1ReconcileList.Cols(Col_Prob_dtDOS).Width = 80
                    c1ReconcileList.Cols(Col_Prob_ResolveDate).Width = 92
                    c1ReconcileList.Cols(Col_Prob_Status).Width = 80
                End If
            ElseIf _ListType = "Allergy" Then

                c1ReconcileList.Cols(Col_All_sTranID1).Caption = "RxNorm ID"


                c1ReconcileList.Cols(Col_All_sSourceName).Caption = "Source"
                c1ReconcileList.Cols(Col_All_sHistoryItem).Caption = "History"

                c1ReconcileList.Cols(Col_All_Status).Caption = "Status"
                c1ReconcileList.Cols(Col_All_DOEAllergy).Caption = "Date Entered"
                c1ReconcileList.Cols(Col_All_sConceptID).Caption = "SnoMed CT ID"
                c1ReconcileList.Cols(Col_All_sUserName).Caption = "User"
                c1ReconcileList.Cols(Col_All_Severity).Caption = "Allergy Severity"

                c1ReconcileList.Name = "Allergy_FinalizeList"

                ''If new column is added in grid the add the column in if condition to set width 
                ''This code executes only ones and set width for the newly added column.
                If (c1ReconcileList.Cols.Count <> GetGridColumnWidthCount(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID)) Then
                    Dim _SettingWidthList As New List(Of String)
                    _SettingWidthList = GetGridColumnWidthList(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID)
                    If _SettingWidthList IsNot Nothing Then
                        If _SettingWidthList.Count > 0 Then
                            For i As Integer = 0 To c1ReconcileList.Cols.Count - 1
                                If i = Col_All_ConcernEndDate Then
                                    _SettingWidthList.Insert(i, "0")
                                ElseIf i = Col_All_ObservationEndDate Then
                                    _SettingWidthList.Insert(i, "0")
                                ElseIf i = Col_All_ConcernStatus Then
                                    _SettingWidthList.Insert(i, "0")
                                ElseIf i = Col_All_Severity Then
                                    _SettingWidthList.Insert(i, "120")
                                End If
                            Next
                        End If
                    End If

                    If c1ReconcileList.Cols.Count = _SettingWidthList.Count Then
                        SaveGridColumnWidthList(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID, _SettingWidthList)
                    End If
                End If


                If (ogloSettings.LoadGridColumnWidth(c1ReconcileList, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then
                    c1ReconcileList.Cols(Col_All_dtOnsetDate).Width = 0
                    c1ReconcileList.Cols(Col_All_nRowOrder).Width = 0
                    c1ReconcileList.Cols(COL_All_ListName).Width = 0
                    c1ReconcileList.Cols(COL_All_Dup).Width = 0
                    c1ReconcileList.Cols(COL_All_Select).Width = 0
                    c1ReconcileList.Cols(Col_All_nHistoryID).Width = 0
                    c1ReconcileList.Cols(COL_All_nListID).Width = 0

                    c1ReconcileList.Cols(Col_All_nPatientID).Width = 0
                    c1ReconcileList.Cols(Col_All_bIsSkipped).Width = 0
                    c1ReconcileList.Cols(Col_All_sDrugName).Width = 0

                    c1ReconcileList.Cols(Col_All_sNDCCode).Width = 0

                    c1ReconcileList.Cols(Col_All_sConceptID).Width = 200

                    c1ReconcileList.Cols(Col_All_sICD9).Width = 0
                    c1ReconcileList.Cols(Col_All_sCPT).Width = 0

                    c1ReconcileList.Cols(Col_All_sHistoryCategory).Width = 0

                    c1ReconcileList.Cols(Col_All_sTranID1).Width = 100
                    c1ReconcileList.Cols(Col_All_sSourceName).Width = 230
                    c1ReconcileList.Cols(Col_All_sHistoryItem).Width = 230
                    c1ReconcileList.Cols(Col_All_sConceptID).Width = 100
                    c1ReconcileList.Cols(Col_All_dtLastUpdated).Width = 87
                    c1ReconcileList.Cols(Col_All_sReaction).Width = 100
                    c1ReconcileList.Cols(Col_All_VisitDate).Width = 0
                    ''
                    c1ReconcileList.Cols(Col_All_Status).Width = 60
                    c1ReconcileList.Cols(Col_All_DrugID).Width = 0
                    c1ReconcileList.Cols(Col_All_MedicalConditionID).Width = 0
                    c1ReconcileList.Cols(Col_All_DescriptionID).Width = 0
                    c1ReconcileList.Cols(Col_All_SnomedID).Width = 0
                    c1ReconcileList.Cols(Col_All_Description).Width = 0
                    c1ReconcileList.Cols(Col_All_HistoryType).Width = 0
                    c1ReconcileList.Cols(Col_All_ConcernEndDate).Width = 0
                    c1ReconcileList.Cols(Col_All_ObservationEndDate).Width = 0
                    c1ReconcileList.Cols(Col_All_ConcernStatus).Width = 0
                    c1ReconcileList.Cols(Col_All_Severity).Width = 120
                End If

                ''
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If Not IsNothing(ogloSettings) Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If
        End Try
    End Sub
    Public Function GetGridColumnWidthCount(oControlGrid As C1.Win.C1FlexGrid.C1FlexGrid, ModuleName As ModuleOfGridColumn, UserID As Int64) As Integer
        Dim _result As Integer = 0

        Dim _SettingValue As String = ""
        Dim _SettingValueObj As Object = Nothing
        Dim _SettingValueList As String() = Nothing
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(gloLibCCDGeneral.Connectionstring)
        Try

            Dim _SettingName As String = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Name.ToString().ToUpper()
            _SettingValue = ""
            ogloSettings.GetSetting(_SettingName, UserID, gloGlobal.gloPMGlobal.ClinicID, _SettingValueObj)

            If _SettingValueObj IsNot Nothing AndAlso _SettingValueObj.ToString().Trim() <> "" Then
                _SettingValue = _SettingValueObj.ToString()
            End If
            If _SettingValue.Trim().Length > 0 Then
                _SettingValueList = _SettingValue.Split(","c)
            End If

            If _SettingValueList.Length > 0 Then
                _result = _SettingValueList.Length
            End If
        Catch generatedExceptionName As Exception
            'ex.ToString();
            'ex = null;
            ' ex)
        Finally
            If Not IsNothing(ogloSettings) Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If
            _SettingValue = Nothing
            _SettingValueObj = Nothing
            _SettingValueList = Nothing
        End Try
        Return _result
    End Function

    Public Function GetGridColumnWidthList(oControlGrid As C1.Win.C1FlexGrid.C1FlexGrid, ModuleName As ModuleOfGridColumn, UserID As Int64) As List(Of String)
        Dim _result As Boolean = False

        Dim _SettingValue As String = ""
        Dim _SettingValueObj As Object = Nothing
        Dim _SettingValueList As String() = Nothing
        Dim lstSettingValue As New List(Of String)
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(gloLibCCDGeneral.Connectionstring)
        Try

            Dim _SettingName As String = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Name.ToString().ToUpper()
            _SettingValue = ""
            ogloSettings.GetSetting(_SettingName, UserID, gloGlobal.gloPMGlobal.ClinicID, _SettingValueObj)

            If _SettingValueObj IsNot Nothing AndAlso _SettingValueObj.ToString().Trim() <> "" Then
                _SettingValue = _SettingValueObj.ToString()
            End If
            If _SettingValue.Trim().Length > 0 Then
                _SettingValueList = _SettingValue.Split(","c)
            End If
            If _SettingValueList IsNot Nothing Then
                If _SettingValueList.Length > 0 Then
                    lstSettingValue.AddRange(_SettingValueList)
                End If
            End If
        Catch generatedExceptionName As Exception
            'ex.ToString();
            'ex = null;
            ' ex)
        Finally
            If Not IsNothing(ogloSettings) Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If
            _SettingValue = Nothing
            _SettingValueObj = Nothing
            _SettingValueList = Nothing
        End Try
        Return lstSettingValue
    End Function
    Public Sub SaveGridColumnWidthList(oControlGrid As C1.Win.C1FlexGrid.C1FlexGrid, ModuleName As ModuleOfGridColumn, UserID As Int64, lstWidthList As List(Of String))
        Dim _SettingValue As String = ""
        Dim _SettingName As String = ""
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(gloLibCCDGeneral.Connectionstring)
        Try
            _SettingName = ModuleName.ToString().ToUpper() + "_" + oControlGrid.Name.ToString().ToUpper()
            For index As Integer = 0 To lstWidthList.Count - 1
                If index = 0 Then
                    _SettingValue = lstWidthList(index).ToString()
                Else
                    _SettingValue = (_SettingValue & Convert.ToString(",")) + lstWidthList(index).ToString()
                End If

            Next

            Dim _result As Boolean = ogloSettings.AddSetting(_SettingName, _SettingValue, gloGlobal.gloPMGlobal.ClinicID, UserID, SettingFlag.User)
        Catch generatedExceptionName As Exception
            'ex.ToString();
            'ex = null;
            ' ex)
        Finally
            If Not IsNothing(ogloSettings) Then
                ogloSettings.Dispose()
                ogloSettings = Nothing
            End If
            _SettingValue = Nothing
            _SettingName = Nothing
        End Try
    End Sub

#End Region

#Region "Button Clicks"


    Private Sub tlbbtn_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        SelectedAction = FormAction.Closed
        Me.Close()
    End Sub

    Private Sub tlbbtn_Accept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Accept.Click
        ''Save Data against patient from reconciliation lists
        AcceptReconcileLists()
        SelectedAction = FormAction.Accepted
    End Sub
#End Region

#Region "AcceptReconcileLists"
    Private Sub AcceptReconcileLists()
        Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
        Dim _result As Boolean = False
        Try

            If c1ReconcileList.Rows.Count > 1 Then
                If _ListType = "Medication" Then
                    c1ReconcileList.Select()
                    flag = False
                    Dim datarow As DataRow() = Nothing
                    'Acceptchanges added below to check the startdate correctly in _dtreconcile
                    _dtReconcile.AcceptChanges()
                    datarow = _dtReconcile.Select("(startdate IS NULL) and (select = 1)")
                    If datarow.Length > 0 Then
                        Dim rowindex As Int64 = 0
                        Dim sMessage As String = "Medication 'Start Date' is not present for some Medication(s)." & vbNewLine & "Please select a valid 'Start Date'."
                        MessageBox.Show(sMessage, "gloEMR", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                        rowindex = c1ReconcileList.FindRow(Convert.ToInt64(datarow(0).ItemArray(Col_Med_MedicationID)), 1, Col_Med_MedicationID, False, True, False)
                        'c1ReconcileList.Select(rowindex, 0, True)
                        c1ReconcileList.Select(rowindex, Col_Med_StartDate, True)
                        Exit Sub
                    End If

                    Dim oMedicationCol As MedicationsCol = Nothing

                    Dim oMedication As Medication = Nothing

                    oMedicationCol = New MedicationsCol
                    '  oMedicationCol = SavePreviousMedication()
                    For i As Int16 = 1 To c1ReconcileList.Rows.Count - 1
                        If Convert.ToString(c1ReconcileList.GetData(i, Col_Med_Source)) = "Current" Or c1ReconcileList.Rows(i).IsVisible Then
                            oMedication = New Medication
                            If Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_VisitDate)) = Date.Now.ToString("MM/dd/yyyy") Then
                                oMedication.MedicationID = Convert.ToString(c1ReconcileList.Rows(i)(Col_MedicationID))
                            Else
                                oMedication.MedicationID = 0
                            End If
                            'oMedication.User = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_UserName))
                            oMedication.User = _UserName
                            ' oMedication.MedicationID = Convert.ToString(c1ReconcileList.Rows(i)(Col_MedicationID))
                            oMedication.PatientID = Convert.ToString(c1ReconcileList.Rows(i)(Col_PatientID))
                            oMedication.DrugName = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Medication))
                            oMedication.DrugQuantity = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sDosage))
                            oMedication.Route = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sRoute))
                            oMedication.MedicationDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_MedicationDate))

                            oMedication.ProdCode = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sNDCCode))
                            oMedication.RxNormCode = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sRxNormCodes))

                            oMedication.Frequency = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Frequency))
                            oMedication.StrengthUnits = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sStrengthUnit))
                            oMedication.Refills = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Refills))
                            'If Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_Source)) <> "Active" Then
                            oMedication.Rx_MaySubstitute = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_bMaySubstitute))

                            'End If

                            oMedication.DrugStrength = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Amount))
                            'Completed Medications are imported as Active if Enddate > SystemDate or Enddate =""'' by Namrata on 09/10/2015

                            Dim iscompleted As Boolean = False
                            If Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Status)).Equals("Completed", StringComparison.OrdinalIgnoreCase) Then
                                If Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_EndDate)) = "" Or IsDBNull(c1ReconcileList.Rows(i)(Col_Med_EndDate)) = True Then
                                    iscompleted = True
                                    ''Bug #110073: glo Medication >> CCDA >> Import CDA file with complete status medication saved as Active after reconciliation
                                    'ElseIf Date.Parse(Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_EndDate))) > Date.Parse(DateTime.Now.Date) Then
                                    '    iscompleted = True
                                End If

                            End If



                            If Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Source)) = "Current" And c1ReconcileList.Rows(i).Visible = False Then
                                oMedication.Status = "InActive"
                            ElseIf iscompleted = True Then
                                oMedication.Status = "Active"
                            Else
                                oMedication.Status = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Status))
                            End If

                            oMedication.DrugForm = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_DrugForm))
                            oMedication.StartDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_StartDate))
                            oMedication.EndDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_EndDate))

                            ''
                            oMedication.Reason = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Reason))
                            oMedication.mpid = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Mpid))
                            If oMedication.mpid = 0 Or oMedication.mpid = Nothing Then
                                Using oDIBHelper As New gloGlobal.DIB.gloGSHelper(gloLibCCDGeneral.sDIBServiceURL)
                                    oMedication.mpid = oDIBHelper.GetMarketedProductId(oMedication.ProdCode)
                                End Using
                            End If
                            oMedication._PrescriptionId = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_PrescriptionID))
                            ' oMedication.MedicationID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_MedicationID))
                            oMedication.Renewed = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Renewed))
                            oMedication.IsNarcotics = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_IsNarcotic))
                            oMedication.PBMSourceName = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_PBMName))
                            oMedication.RxMedDMSID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_DMSID))
                            oMedication.Rx_Method = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Method))
                            oMedication.Rx_PrescriberNotes = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_PrescriberNotes))
                            oMedication.Duration = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Duration))
                            oMedication.Rx_Notes = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_RxNotes))
                            oMedication.Rx_sNCPDPID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sNCPDPID))
                            oMedication.Rx_PhName = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sName))
                            oMedication.Rx_sAddressline1 = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sAddressline1))
                            oMedication.Rx_sAddressline2 = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sAddressline2))
                            oMedication.Rx_sCity = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sCity))
                            oMedication.Rx_sState = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sState))
                            oMedication.Rx_sZip = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sZip))
                            oMedication.Rx_sPhone = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sPhone))
                            oMedication.Rx_sFax = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sFax))
                            oMedication.Rx_sEmail = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sEmail))
                            'oMedication. = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_RxNotes))
                            ''


                            Try
                                MedicationVisitID = gloReconciliation.GenerateVisitID(DateTime.Now, oMedication.PatientID)
                            Catch ex As Exception

                            End Try

                            oMedicationCol.Add(oMedication)
                            If IsNothing(oMedication) = False Then
                                oMedication.Dispose()
                                oMedication = Nothing
                            End If
                        End If

                    Next
                    _result = gloReconciliation.SaveMedication(oMedicationCol, True)
                    'If _result = True Then
                    '    Me.DialogResult = DialogResult.OK
                    'End If
                    If IsNothing(oMedicationCol) Then
                        oMedicationCol.Dispose()
                        oMedicationCol = Nothing
                    End If

                ElseIf _ListType = "Allergy" Then
                    Dim ogloPatientHistory As gloPatientHistory = Nothing
                    Dim oHistoryCol As gloPatientHistoryCol = Nothing
                    'oHistoryCol = New gloPatientHistoryCol
                    Dim dv1 As DataView
                    Dim dv2 As DataView
                    Dim itemnumber As Int64 = 0
                    Dim _dtTemp As DataTable
                    ''  ''to get maxroworder
                    If c1ReconcileList.Rows.Count > 0 Then
                        dv1 = c1ReconcileList.DataSource
                        _dtTemp = dv1.ToTable
                        dv2 = _dtTemp.DefaultView
                        dv2.Sort = "nRowOrder ASC"
                        If dv2.ToTable.Rows.Count > 0 Then
                            itemnumber = dv2.ToTable.Rows(dv2.ToTable.Rows.Count - 1)("nRowOrder")
                        End If
                        If IsNothing(dv2) = False Then
                            dv2.Dispose()
                            dv2 = Nothing
                        End If
                    End If
                    oHistoryCol = SavePreviousHistory()
                    If oHistoryCol.Count > 0 Then
                        itemnumber = 0
                    End If
                    Dim _sReaction As String = ""
                    ' Dim _VisitID As Int64
                    For i As Int16 = 1 To c1ReconcileList.Rows.Count - 1

                        ' Insert New
                        'If Convert.ToString(c1ReconcileList.Rows(i)(Col_All_nHistoryID)) <> 0 Or Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sSourceName)) = "Active" Or c1ReconcileList.Rows(i).IsVisible Then
                        If c1ReconcileList.Rows(i).IsVisible Or Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sSourceName)) = "Current" Then

                            ogloPatientHistory = New gloPatientHistory

                            If Convert.ToString(c1ReconcileList.Rows(i)(Col_All_VisitDate)) = Date.Now.ToString("MM/dd/yyyy") Then
                                ogloPatientHistory.HistoryID = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_nHistoryID))

                            Else
                                ogloPatientHistory.HistoryID = 0
                                If c1ReconcileList.Rows(i).IsVisible Then
                                Else
                                    Continue For
                                End If

                            End If
                            ogloPatientHistory.Source = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sSourceName))
                            ogloPatientHistory.PatientID = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_nPatientID))
                            ogloPatientHistory.HistoryCategory = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sHistoryCategory))
                            ogloPatientHistory.HistoryItem = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sHistoryItem))
                            ogloPatientHistory.DrugName = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sDrugName))
                            ''
                            ''ogloPatientHistory.DrugID = Convert.ToString(c1ReconcileList.Rows(i)(col_all_dr))
                            ''
                            ogloPatientHistory.NDCCode = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sNDCCode))

                            ogloPatientHistory.DOEAllergy = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_DOEAllergy))
                            ogloPatientHistory.ConceptId = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sConceptID))
                            ogloPatientHistory.ICD9 = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sICD9))
                            'ogloPatientHistory.UserName = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sUserName))
                            ogloPatientHistory.UserName = _UserName
                            If Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sSourceName)) = "Current" And c1ReconcileList.Rows(i).Visible = False Then
                                'ogloPatientHistory.Reaction = Replace(Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sReaction)), "|Active", "|InActive")
                                ogloPatientHistory.IsDeleted = True
                                ogloPatientHistory.Status = "InActive"
                            ElseIf Convert.ToString(c1ReconcileList.Rows(i)(Col_All_Status)).Equals("Active", StringComparison.OrdinalIgnoreCase) Then
                                ogloPatientHistory.Status = "Active"

                            Else
                                ogloPatientHistory.Status = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_Status))
                            End If
                            ogloPatientHistory.Reaction = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sReaction))
                            ogloPatientHistory.RxNormCode = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_sTranID1))
                            ''
                            ogloPatientHistory.DrugID = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_DrugID))
                            ogloPatientHistory.Medicalconditionid = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_MedicalConditionID))
                            ogloPatientHistory.DescId = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_DescriptionID))
                            ogloPatientHistory.SnoMedId = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_SnomedID))
                            ogloPatientHistory.SnoDescription = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_Description))
                            ogloPatientHistory.HistoryType = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_HistoryType))
                            ogloPatientHistory.HistorySource = "gloEMR"
                            ogloPatientHistory.ConcernEndDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_ConcernEndDate))
                            ogloPatientHistory.ObservationEndDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_ObservationEndDate))
                            ogloPatientHistory.ConcernStatus = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_ConcernStatus))
                            ogloPatientHistory.OnsetDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_dtOnsetDate))
                            ogloPatientHistory.Severity = Convert.ToString(c1ReconcileList.Rows(i)(Col_All_Severity))
                            ''
                            oHistoryCol.Add(ogloPatientHistory)
                            If IsNothing(ogloPatientHistory) = False Then
                                ogloPatientHistory.Dispose()
                                ogloPatientHistory = Nothing
                            End If
                        End If
                    Next
                    _result = gloReconciliation.SavePatientHistory(oHistoryCol, itemnumber)
                    'If _result = True Then
                    '    Me.DialogResult = DialogResult.OK
                    'End If
                    If IsNothing(oHistoryCol) Then
                        oHistoryCol.Dispose()
                        oHistoryCol = Nothing
                    End If

                ElseIf _ListType = "Problem" Then
                    Dim oProblem As Problems

                    Dim oProblemCol As ProblemsCol
                    oProblemCol = New ProblemsCol
                    For i As Int16 = 1 To c1ReconcileList.Rows.Count - 1
                        If c1ReconcileList.Rows(i).IsVisible Or Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_Source)) = "Current" Then
                            oProblem = New Problems
                            oProblem.ProblemID = Convert.ToString(c1ReconcileList.Rows(i)(Col_nProblemID))
                            oProblem.DateOfService = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_dtDOS))
                            oProblem.ICD9Code = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_sICD9Code))
                            oProblem.ICD9 = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_sICD9Desc))
                            oProblem.Condition = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_Complaint))
                            If Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_Source)) = "Current" And c1ReconcileList.Rows(i).Visible = False Then
                                oProblem.ProblemStatus = 3
                            Else
                                oProblem.ProblemStatus = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_nProblemStatus))
                            End If

                            oProblem.Immediacy = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_nImmediacy))
                            'oProblem.User = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_User))
                            oProblem.User = _UserName
                            oProblem.ConceptID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_sConceptID))
                            oProblem.PatientID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_nPatientID))
                            'oProblem.UserID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_UserID))
                            oProblem.UserID = _UserID
                            oProblem.ResolvedDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_ResolveDate))
                            oProblem.ModifiedDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_dtLastUpdated))
                            oProblem.ConcernStartDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_ConcernStartDate))
                            oProblem.ConcernEndDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_ConcernEndDate))
                            oProblem.sConcernStatus = Convert.ToString(c1ReconcileList.Rows(i)(Col_Prob_ConcernStatus))
                            oProblemCol.Add(oProblem)
                            If IsNothing(oProblem) = False Then
                                oProblem.Dispose()
                                oProblem = Nothing
                            End If
                        End If
                    Next

                    _result = gloReconciliation.SaveProblemList(oProblemCol)
                    'If _result = True Then
                    '    Me.DialogResult = DialogResult.OK
                    'End If
                    If IsNothing(oProblemCol) Then
                        oProblemCol.Dispose()
                        oProblemCol = Nothing
                    End If
                End If

                Dim nPatientProviderId As Long = _LoginProviderID
                Dim IsstaffuserTransaction As Boolean = 0
                If nPatientProviderId = 0 Then
                    nPatientProviderId = gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(_PatientID)
                    IsstaffuserTransaction = 1
                End If

                If Not getProviderTaxID(nPatientProviderId) Then
                    Exit Sub
                End If

                ogloPatientRegDBLayer.UpdateStatus(0, _PatientID, _strListID, False, False, ListStatus.Finished, nPatientProviderId, IsstaffuserTransaction)

                Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nPatientProviderId)
                Dim oTransationType As gloGlobal.TIN.clsSelectProviderTaxID.TransactionType
                If _ListType = "Problem" Then
                    oTransationType = gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.CCDAReconciliationProblemList
                ElseIf _ListType = "Medication" Then
                    oTransationType = gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.CCDAReconciliationMedication
                ElseIf _ListType = "Allergy" Then
                    oTransationType = gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.CCDAReconciliationMedicationAllergy
                End If
                Dim IDs As String() = Nothing
                If _strListID <> "" Then
                    IDs = _strListID.Split(New Char() {","c})
                End If
                Dim ID As String
                If IDs IsNot Nothing Then
                    For Each ID In IDs
                        oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, Convert.ToInt64(ID), sProviderTaxID, nPatientProviderId, 0, oTransationType.GetHashCode())
                    Next
                End If
                oclsselectProviderTaxID = Nothing

                ogloPatientRegDBLayer.UpdateSkippedStatus(0, _PatientID, _strSkippedID, _ListType)
                'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, "Accepted " & _ListType & " Lists", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
            End If
            Me.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If IsNothing(ogloPatientRegDBLayer) = False Then
                ogloPatientRegDBLayer.Dispose()
                ogloPatientRegDBLayer = Nothing
            End If
        End Try
    End Sub
    Private Sub ReconcileListEditing()
        Try
            c1ReconcileList.AllowEditing = True
            c1ReconcileList.Cols(Col_Med_Source).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_MedicationDate).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Drug).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_EndDate).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Frequency).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Amount).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Refills).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_DrugForm).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Status).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_sNDCCode).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_UserName).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sNCPDPID).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sName).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sAddressline1).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sAddressline2).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sCity).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sState).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sZip).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sPhone).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sEmail).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Rx_sFax).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_StartDate).AllowEditing = True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Function SavePreviousHistory() As gloPatientHistoryCol
        Dim _VisitID As Int64
        Dim ogloPatientHistory As gloPatientHistory = Nothing
        Dim oHistoryCol As gloPatientHistoryCol = Nothing
        oHistoryCol = New gloPatientHistoryCol
        Dim dtHistory As DataTable
        Dim _PastVisitID As Int64
        Try
            Try
                _VisitID = gloReconciliation.GenerateVisitID(DateTime.Now, _PatientID)
            Catch ex As Exception

            End Try

            If _VisitID > 0 Then
                'To Check if Current History Exists
                dtHistory = gloReconciliation.Fill_History(_PatientID, _VisitID, 0)
                If dtHistory.Rows.Count > 0 Then
                    'History Exists for Current Date

                Else
                    'If History is Not Exist For Current Date then Check for the Previous Date
                    If Not IsNothing(dtHistory) Then
                        dtHistory.Dispose()
                        dtHistory = Nothing
                    End If
                    dtHistory = gloReconciliation.Fill_History(_PatientID, _VisitID, 1)
                    If dtHistory.Rows.Count > 0 Then
                        _PastVisitID = dtHistory.Rows(0)(0)
                        If Not IsNothing(dtHistory) Then
                            dtHistory.Dispose()
                            dtHistory = Nothing
                        End If
                        dtHistory = gloReconciliation.Fill_History(_PatientID, _PastVisitID, 2)
                    End If

                    With dtHistory
                        For i As Integer = 0 To dtHistory.Rows.Count - 1
                            ogloPatientHistory = New gloPatientHistory
                            ogloPatientHistory.Source = "Active"
                            ogloPatientHistory.HistoryID = 0
                            ogloPatientHistory.PatientID = _PatientID
                            'ogloPatientHistory.VisitID = _VisitID
                            ogloPatientHistory.HistoryCategory = .Rows(i)(0)
                            ogloPatientHistory.HistoryItem = .Rows(i)(1)
                            ogloPatientHistory.Comments = .Rows(i)("sComments")
                            ogloPatientHistory.Reaction = .Rows(i)("Reaction") ''Reaction 
                            ogloPatientHistory.VisitID = _VisitID

                            ogloPatientHistory.DrugName = .Rows(i)("sHistoryItem")
                            ogloPatientHistory.DrugID = .Rows(i)("nDrugID")  ''  DrugID
                            ogloPatientHistory.Medicalconditionid = .Rows(i)("MedicalCondition_Id")  ''  MedicalCondition ID 
                            ogloPatientHistory.Dosage = .Rows(i)("sDosage")
                            ogloPatientHistory.NDCCode = .Rows(i)("sNDCCode")
                            ogloPatientHistory.mpid = .Rows(i)("mpid")
                            ogloPatientHistory.OnsetDate = Convert.ToString(.Rows(i)("OnsetDate")).Replace("NULL", "")

                            ogloPatientHistory.DOEAllergy = .Rows(i)("DOE_Allergy")
                            ogloPatientHistory.ConceptId = .Rows(i)("sConceptID")
                            ogloPatientHistory.ICD9 = .Rows(i)("sICD9")
                            ogloPatientHistory.DescId = .Rows(i)("sDescriptionID")
                            ogloPatientHistory.SnoMedId = .Rows(i)("sSnoMedID")
                            ogloPatientHistory.SnoDescription = .Rows(i)("sDescription")
                            ogloPatientHistory.HistorySource = .Rows(i)("sHistorySource")
                            'ogloPatientHistory.Reaction = .Rows(i)(3) ''Reaction 
                            ogloPatientHistory.RxNormCode = .Rows(i)("sRxNormID")
                            ogloPatientHistory.CPT = .Rows(i)("sCPT")
                            ' ogloPatientHistory.OnsetDate = Convert.ToString(.Rows(i)("OnsetDate")).Replace("NULL", "")
                            ogloPatientHistory.Status = .Rows(i)("Status") ''Reaction 

                            ogloPatientHistory.HistoryType = .Rows(i)("sHistoryType")
                            ogloPatientHistory.UserName = _UserName
                            oHistoryCol.Add(ogloPatientHistory)
                            If IsNothing(ogloPatientHistory) = False Then
                                ogloPatientHistory.Dispose()
                                ogloPatientHistory = Nothing
                            End If
                        Next
                    End With

                    '   cls.AddNewHistory_New(0, mgnVisitID, m_PatientID, ArrLst, False)
                End If
                If IsNothing(dtHistory) = False Then
                    dtHistory.Dispose()
                    dtHistory = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            oHistoryCol = Nothing
        Finally
        End Try
        Return oHistoryCol
    End Function

#Region "Save Column Width"
    Private Sub SaveReconcileColumnWidth()
        Dim _gloReconciliation As New gloReconciliation
        If _ListType = "Medication" Then
            c1ReconcileList.Name = "Meds_FinalizeList"
            _gloReconciliation.SaveColumnWidth(False, c1ReconcileList, _UserID)

        ElseIf _ListType = "Problem" Then
            c1ReconcileList.Name = "Probs_FinalizeList"
            _gloReconciliation.SaveColumnWidth(False, c1ReconcileList, _UserID)

        ElseIf _ListType = "Allergy" Then
            c1ReconcileList.Name = "Allergy_FinalizeList"
            _gloReconciliation.SaveColumnWidth(False, c1ReconcileList, _UserID)

        End If
        If Not IsNothing(_gloReconciliation) Then
            _gloReconciliation.Dispose()
            _gloReconciliation = Nothing
        End If
    End Sub
#End Region
#End Region

#Region "Form_Closed"
    Private Sub frmFinalizeReconcileList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If IsNothing(Me) = False Then
            Try

                'Application.DoEvents()
                Me.Dispose()
            Catch exdispose As Exception

            End Try
        End If
    End Sub
#End Region







    'Private Sub c1ReconcileList_ChangeEdit(sender As Object, e As System.EventArgs) Handles c1ReconcileList.ChangeEdit
    '    Try
    '        'Dim SelectedDate As Date = Nothing
    '        'If e.Row > 0 Then
    '        '    If e.Col = Col_Med_StartDate Then
    '        '        SelectedDate = c1ReconcileList.Rows(e.Row)(e.Col)
    '        '    End If
    '        'End If

    '        Dim dtsource As DataTable = Nothing
    '        Dim dataview As DataView = Nothing
    '        Dim datarow As DataRow() = Nothing
    '        If c1ReconcileList.Rows.Count > 0 Then
    '            dataview = c1ReconcileList.DataSource
    '            dtsource = dataview.ToTable()
    '            datarow = dtsource.Select("startdate IS NULL")
    '            If datarow.Length > 0 Then
    '                Dim dlgResult As DialogResult
    '                Dim sMessage As String = "Would you like to update the selected 'Start Date' for other Medication(s) that are not having valid 'Start Date'"
    '                dlgResult = MessageBox.Show(sMessage, "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '                If (dlgResult = Windows.Forms.DialogResult.Yes) Then
    '                    For index As Integer = 0 To c1ReconcileList.Rows.Count - 1
    '                        If IsDBNull(c1ReconcileList.Rows(index)(Col_Med_StartDate)) = True Then
    '                            'c1ReconcileList.Rows(index)(Col_Med_StartDate) = SelectedDate
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub c1ReconcileList_AfterEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ReconcileList.AfterEdit
        Try
            If SelectedAction <> FormAction.Closed Then
                Dim SelectedDate As Date = Nothing
                If e.Row > 0 Then
                    If e.Col = Col_Med_StartDate Then
                        SelectedDate = c1ReconcileList.Rows(e.Row)(e.Col)
                    End If
                End If

                Dim dtsource As DataTable = Nothing
                Dim dataview As DataView = Nothing
                Dim datarow As DataRow() = Nothing
                If c1ReconcileList.Rows.Count > 0 Then
                    dataview = c1ReconcileList.DataSource
                    dtsource = dataview.ToTable()
                    datarow = dtsource.Select("startdate IS NULL and select = 1")
                    If datarow.Length > 0 Then
                        If flag = False Then
                            Dim dlgResult As DialogResult
                            Dim sMessage As String = "Do you want to update the selected 'Start Date' for other Medication(s) that are not having valid 'Start Date'?"
                            dlgResult = MessageBox.Show(sMessage, "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            flag = True
                            If (dlgResult = Windows.Forms.DialogResult.Yes) Then
                                For index As Integer = 0 To c1ReconcileList.Rows.Count - 1
                                    If IsDBNull(c1ReconcileList.Rows(index)(Col_Med_StartDate)) = True Then
                                        c1ReconcileList.Rows(index)(Col_Med_StartDate) = SelectedDate
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub c1ReconcileList_BeforeEdit(sender As Object, e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ReconcileList.BeforeEdit
        Try
            If e.Row > 0 Then
                If e.Col = Col_Med_StartDate AndAlso Convert.ToString(c1ReconcileList.Rows(e.Row)(Col_Med_Source)) = "Current" Then
                    e.Cancel = True
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Function getProviderTaxID(Optional ByVal nProviderID As Int64 = 0) As Boolean
        sProviderTaxID = ""
        nProviderAssociationID = 0
        Try
            Dim oResult As DialogResult = System.Windows.Forms.DialogResult.OK
            Dim oForm As New gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID))
            If oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count > 1 Then
                oForm.ShowDialog(Me)
                oResult = oForm.DialogResult
                nProviderAssociationID = oForm.nAssociationID
                sProviderTaxID = oForm.sProviderTaxID

                oForm = Nothing
            ElseIf oForm.dtProviderTaxIDs IsNot Nothing AndAlso oForm.dtProviderTaxIDs.Rows.Count = 1 Then
                ''oResult = oForm.DialogResult
                nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows(0)("nAssociationID"))
                sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows(0)("sTIN"))
                oForm = Nothing
            Else
                nProviderAssociationID = 0
                sProviderTaxID = ""
            End If

            If oResult = Windows.Forms.DialogResult.OK Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return False

        Finally
        End Try
    End Function

End Class
