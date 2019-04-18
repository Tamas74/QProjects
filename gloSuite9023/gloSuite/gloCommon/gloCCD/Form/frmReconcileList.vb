Imports gloUserControlLibrary
Imports System.Windows.Forms
Imports gloSettings

Public Class frmReconcileList
#Region "Variable Declaration"
    Dim _nPatientID As Long
    Dim _ModuleName As String = ""
    Dim _IsFormLoading As Boolean = False
    Dim _UserName As String = String.Empty
    Dim _UserID As Long = 0
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip
    Dim _MedicationVisitID As Long = 0


#End Region

#Region "Property"
    Public Property LoginUser() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public Property LoginID() As Long
        Get
            Return _UserID
        End Get
        Set(ByVal value As Long)
            _UserID = value
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
#End Region
#Region "Column Constants"

    Private Const COL_Select As Integer = 0
    Private Const COL_ListID As Integer = 1
    Private Const COL_CCDID As Integer = 2
    Private Const COL_ListName As Integer = 3
    Private Const COL_Source As Integer = 4
    Private Const COL_ImportDate As Integer = 5
    Private Const COL_UserName As Integer = 6
    Private Const COL_ListStatus As Integer = 7
    Private Const COL_ListType As Integer = 8

    '------------Medication Coloumns----------
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
    Private Const Col_Med_UserName As Integer = 18
    Private Const Col_Med_Skip As Integer = 19
    Private Const Col_Med_sDosage As Integer = 20
    Private Const Col_Med_sRoute As Integer = 21



    Private Const Col_Med_sStrengthUnit As Integer = 22
    Private Const Col_Med_VisitDate As Integer = 23
    ''
    Private Const Col_Med_Reason As Integer = 24
    Private Const Col_Med_DDID As Integer = 25
    Private Const Col_Med_PrescriptionID As Integer = 26
    Private Const Col_Med_MedicationID As Integer = 27
    Private Const Col_Med_Renewed As Integer = 28
    Private Const Col_Med_IsNarcotic As Integer = 29
    Private Const Col_Med_PBMName As Integer = 30
    Private Const Col_Med_bMaySubstitute As Integer = 31
    Private Const Col_Med_Rx_DMSID As Integer = 32
    Private Const Col_Med_Method As Integer = 33
    Private Const Col_Med_PrescriberNotes As Integer = 34
    Private Const Col_Med_Duration As Integer = 35
    Private Const Col_Med_RxNotes As Integer = 36
    Private Const Col_Med_RxnDrugID As Integer = 37
    '--------------------------------------------------


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




    Private Const COL_All_Select As Integer = 0
    Private Const COL_All_Dup As Integer = 1
    Private Const COL_All_ListName As Integer = 2
    Private Const Col_All_nProblemID As Integer = 3
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
    Private Const Col_Rec_Source As Integer = 5


#End Region

#Region "C1 Styles"

    Dim cStyle_Blue As C1.Win.C1FlexGrid.CellStyle
    Dim cStyle_Pink As C1.Win.C1FlexGrid.CellStyle
    Dim cStyle_StrikoutFont As C1.Win.C1FlexGrid.CellStyle
    Dim cStyle_NormalFont As C1.Win.C1FlexGrid.CellStyle

#End Region

#Region "Constuctor and Form Load"

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal PatientID As Long, Optional ByVal ModuleName As String = "")
        _nPatientID = PatientID
        _ModuleName = ModuleName
        InitializeComponent()

    End Sub

    Private Sub frmReconcileList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        cStyle_Pink = Nothing
        cStyle_Blue = Nothing
        cStyle_StrikoutFont = Nothing
        cStyle_NormalFont = Nothing

        If IsNothing(gloUC_PatientStrip1) = False Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
    End Sub

    Private Sub frmReconcileList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveReconcileColumnWidth()

    End Sub

    Private Sub frmReconcileList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        _IsFormLoading = True

        Set_PatientDetailStrip()
        CreateC1Styles()

        c1ListType.AllowDragging = False
        c1Reconciliation.AllowSorting = False
        C1ConsolidatedList.AllowSorting = False
        cmbListType.Items.Add("Problem")
        cmbListType.Items.Add("Medication")
        cmbListType.Items.Add("Med Allergy")

        If _ModuleName = "Problem" Then
            cmbListType.Text = "Problem"
        ElseIf _ModuleName = "Medication" Then
            cmbListType.Text = "Medication"
        ElseIf _ModuleName = "Allergy" Then
            cmbListType.Text = "Med Allergy"
        Else
            cmbListType.Text = "Medication"
        End If

        FillListTypeGrid()
        DesignListTypeGrid()
        DefaultReconcialationList()
        FillConsolidatedList()

        RemoveExactDuplicates()
        HighLightSimilarDuplicates()

        HighLightExactDuplicates()

        _IsFormLoading = False

    End Sub

#End Region

#Region "Load Patient Strip"
    Private Sub Set_PatientDetailStrip()

        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip
        Me.Controls.Add(gloUC_PatientStrip1)

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            .ShowDetail(_nPatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.None)
            .BringToFront()
            ' .DTPValue = Format(Date.Now, "MM/dd/yyyy")
            ' .DTPFormat = DateTimePickerFormat.Short
            .DTPEnabled = False
            ' .Height = 32
            '.btnDownEnable = False
            '.btnUpEnable = False
        End With
        pnlMain.BringToFront()


    End Sub
#End Region

#Region "Design Grid and C1 Styles"

    Private Sub DesignListTypeGrid()

        c1ListType.Redraw = False
        Try

            c1ListType.Cols.Count = 9

            c1ListType.Cols(COL_Select).Width = 50
            c1ListType.Cols(COL_Select).AllowEditing = True
            c1ListType.Cols(COL_Select).Caption = "Select"
            c1ListType.Cols(COL_Select).DataType = GetType(Boolean)


            c1ListType.Cols(COL_ListID).Width = 0
            c1ListType.Cols(COL_ListID).AllowEditing = False


            c1ListType.Cols(COL_CCDID).Width = 0
            c1ListType.Cols(COL_CCDID).AllowEditing = False


            c1ListType.Cols(COL_ListName).Width = 500
            c1ListType.Cols(COL_ListName).AllowEditing = False
            c1ListType.Cols(COL_ListName).Caption = "List Name"


            c1ListType.Cols(COL_Source).Width = 400
            c1ListType.Cols(COL_Source).AllowEditing = False
            c1ListType.Cols(COL_Source).Caption = "Source"

            c1ListType.Cols(COL_ImportDate).Width = 90
            c1ListType.Cols(COL_ImportDate).AllowEditing = False
            c1ListType.Cols(COL_ImportDate).Caption = "Import Date"

            c1ListType.Cols(COL_ListStatus).Width = 50
            c1ListType.Cols(COL_ListStatus).AllowEditing = False
            c1ListType.Cols(COL_ListStatus).Caption = "List Status"

            c1ListType.Cols(COL_UserName).Width = 0
            c1ListType.Cols(COL_UserName).AllowEditing = False

            c1ListType.Cols(COL_ListType).Width = 0
            c1ListType.Cols(COL_ListType).AllowEditing = False '  c1ListType.Cols(Col_PatientID).Width = 0

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            c1ListType.Redraw = True
        End Try


    End Sub

    Private Sub DesignReconciliationGrid()

        c1Reconciliation.Redraw = False
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(gloLibCCDGeneral.Connectionstring)
        Try

            c1Reconciliation.AllowEditing = False
            c1Reconciliation.AllowDragging = False
            If cmbListType.Text = "Medication" Then
                c1Reconciliation.Cols.Count = 38
                c1Reconciliation.Cols(COL_Med_Dup).AllowResizing = False
                c1Reconciliation.Cols(COL_Med_Dup).Caption = "Dup"
                c1Reconciliation.Cols(COL_Med_Dup).AllowEditing = False



                c1Reconciliation.Cols(Col_Med_Source).Caption = "Source"
                c1Reconciliation.Cols(Col_Med_MedicationDate).Caption = "Last Updated"
                c1Reconciliation.Cols(Col_Med_Drug).Caption = "Drug"
                c1Reconciliation.Cols(Col_Med_StartDate).Caption = "Start Date"
                c1Reconciliation.Cols(Col_Med_EndDate).Caption = "End Date"
                c1Reconciliation.Cols(Col_Med_Frequency).Caption = "Frequency"
                c1Reconciliation.Cols(Col_Med_Amount).Caption = "Amount"
                c1Reconciliation.Cols(Col_Med_Refills).Caption = "Refills"
                c1Reconciliation.Cols(Col_Med_DrugForm).Caption = "Drug Form"
                c1Reconciliation.Cols(Col_Med_Status).Caption = "Status"
                c1Reconciliation.Cols(Col_Med_UserName).Caption = "User"
                c1Reconciliation.Cols(Col_Med_sNDCCode).Caption = "NDC Code"
                c1Reconciliation.Cols(Col_Med_Medication).Caption = "Medication"

                c1Reconciliation.Cols(COL_Med_ListName).Caption = "List Name"


                c1Reconciliation.Cols(Col_Med_Source).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_MedicationDate).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_Drug).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_StartDate).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_EndDate).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_Frequency).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_Amount).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_DrugForm).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_Refills).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_Status).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_UserName).AllowEditing = False
                c1Reconciliation.Cols(Col_Med_sNDCCode).AllowEditing = False


                ''Width
                c1Reconciliation.Name = "Meds_ReconcileList"

                If (ogloSettings.LoadGridColumnWidth(c1Reconciliation, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then




                    c1Reconciliation.Cols(COL_Med_Select).Width = 0
                    c1Reconciliation.Cols(COL_Med_Dup).Width = 30
                    c1Reconciliation.Cols(Col_MedicationID).Width = 0
                    c1Reconciliation.Cols(Col__Med_ListID).Width = 0
                    c1Reconciliation.Cols(Col_PatientID).Width = 0
                    c1Reconciliation.Cols(Col_Med_Medication).Width = 0
                    c1Reconciliation.Cols(COL_Med_ListName).Width = 200
                    c1Reconciliation.Cols(Col_Med_Source).Width = 80
                    c1Reconciliation.Cols(Col_Med_MedicationDate).Width = 90
                    c1Reconciliation.Cols(Col_Med_Drug).Width = 180
                    c1Reconciliation.Cols(Col_Med_StartDate).Width = 72
                    c1Reconciliation.Cols(Col_Med_EndDate).Width = 72
                    c1Reconciliation.Cols(Col_Med_Frequency).Width = 82
                    c1Reconciliation.Cols(Col_Med_Amount).Width = 70
                    c1Reconciliation.Cols(Col_Med_Refills).Width = 70
                    c1Reconciliation.Cols(Col_Med_DrugForm).Width = 82
                    c1Reconciliation.Cols(Col_Med_Status).Width = 82
                    c1Reconciliation.Cols(Col_Med_UserName).Width = 50
                    c1Reconciliation.Cols(Col_Med_sNDCCode).Width = 100

                    c1Reconciliation.Cols(Col_Med_Skip).Width = 0
                    c1Reconciliation.Cols(Col_Med_sDosage).Width = 0
                    c1Reconciliation.Cols(Col_Med_sRoute).Width = 0
                    c1Reconciliation.Cols(Col_Med_sStrengthUnit).Width = 0
                    c1Reconciliation.Cols(Col_Med_VisitDate).Width = 0
                    ''
                    c1Reconciliation.Cols(Col_Med_Reason).Width = 0
                    c1Reconciliation.Cols(Col_Med_DDID).Width = 0
                    c1Reconciliation.Cols(Col_Med_PrescriptionID).Width = 0
                    c1Reconciliation.Cols(Col_Med_MedicationID).Width = 0
                    c1Reconciliation.Cols(Col_Med_Renewed).Width = 0
                    c1Reconciliation.Cols(Col_Med_IsNarcotic).Width = 0
                    c1Reconciliation.Cols(Col_Med_PBMName).Width = 0
                    c1Reconciliation.Cols(Col_Med_bMaySubstitute).Width = 0
                    c1Reconciliation.Cols(Col_Med_Rx_DMSID).Width = 0

                    c1Reconciliation.Cols(Col_Med_Method).Width = 0
                    c1Reconciliation.Cols(Col_Med_PrescriberNotes).Width = 0
                    c1Reconciliation.Cols(Col_Med_Duration).Width = 0
                    c1Reconciliation.Cols(Col_Med_RxNotes).Width = 0
                    c1Reconciliation.Cols(Col_Med_RxnDrugID).Width = 0
                End If
                ''
                ''
            ElseIf cmbListType.Text = "Problem" Then

                c1Reconciliation.Cols.Count = 22
                c1Reconciliation.Cols(COL_Prob_Dup).AllowResizing = False
                c1Reconciliation.Cols(COL_Prob_Dup).AllowEditing = False
                c1Reconciliation.Cols(COL_Prob_Dup).Caption = "Dup"

                c1Reconciliation.Cols(COL_Prob_ListName).Caption = "List Name"

                c1Reconciliation.Cols(Col_Prob_dtDOS).Caption = "DOS"
                c1Reconciliation.Cols(Col_Prob_Complaint).Caption = "Complaint"
                c1Reconciliation.Cols(Col_Prob_Diagnosis).Caption = "Diagnosis"
                c1Reconciliation.Cols(Col_Prob_Status).Caption = "Status"
                c1Reconciliation.Cols(Col_Prob_sConceptID).Caption = "SnoMed CT ID"
                c1Reconciliation.Cols(Col_Prob_User).Caption = "User"
                c1Reconciliation.Cols(Col_Prob_Skipped).Caption = "Keep"
                c1Reconciliation.Cols(Col_Prob_Source).Caption = "Source"
                c1Reconciliation.Cols(Col_Prob_ResolveDate).Caption = "Resolved Date"
                c1Reconciliation.Cols(Col_Prob_dtLastUpdated).Caption = "Last Updated"


                ''width
                c1Reconciliation.Name = "Probs_ReconcileList"

                If (ogloSettings.LoadGridColumnWidth(c1Reconciliation, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then


                    c1Reconciliation.Cols(COL_Prob_ListName).Width = 200
                    c1Reconciliation.Cols(COL_Prob_Select).Width = 0

                    c1Reconciliation.Cols(COL_Prob_Dup).Width = 30
                    c1Reconciliation.Cols(Col_Prob_Source).Width = 100
                    c1Reconciliation.Cols(Col_Prob_dtDOS).Width = 85
                    c1Reconciliation.Cols(Col_Prob_Complaint).Width = 200
                    c1Reconciliation.Cols(Col_Prob_Diagnosis).Width = 200
                    c1Reconciliation.Cols(Col_Prob_Status).Width = 85
                    c1Reconciliation.Cols(Col_Prob_sConceptID).Width = 95
                    c1Reconciliation.Cols(Col_Prob_ResolveDate).Width = 100
                    c1Reconciliation.Cols(Col_Prob_User).Width = 50
                    c1Reconciliation.Cols(Col_Prob_dtLastUpdated).Width = 85

                    c1Reconciliation.Cols(Col_Prob_sICD9Code).Width = 0
                    c1Reconciliation.Cols(Col_Prob_sICD9Desc).Width = 0
                    c1Reconciliation.Cols(Col_Prob_nImmediacy).Width = 0
                    c1Reconciliation.Cols(Col_nProblemID).Width = 0
                    c1Reconciliation.Cols(COL_Prob_nListID).Width = 0
                    c1Reconciliation.Cols(Col_Prob_UserID).Width = 0
                    c1Reconciliation.Cols(Col_Prob_nPatientID).Width = 0
                    c1Reconciliation.Cols(Col_Prob_Skipped).Width = 0
                    c1Reconciliation.Cols(Col_Prob_Immediacy).Width = 0
                    c1Reconciliation.Cols(Col_Prob_nProblemStatus).Width = 0
                End If

                c1Reconciliation.Cols(Col_Prob_sConceptID).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_Source).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_dtDOS).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_Status).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_Complaint).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_Diagnosis).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_ResolveDate).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_User).AllowEditing = False
                c1Reconciliation.Cols(Col_Prob_dtLastUpdated).AllowEditing = False

                ElseIf cmbListType.Text = "Med Allergy" Then

                    c1Reconciliation.Cols.Count = 30
                c1Reconciliation.Cols(COL_All_Dup).AllowResizing = False
                    c1Reconciliation.Cols(COL_All_Dup).AllowEditing = False
                    c1Reconciliation.Cols(COL_All_Dup).Caption = "Dup"

                    c1Reconciliation.Cols(COL_All_ListName).Caption = "List Name"

                    c1Reconciliation.Cols(Col_All_sSourceName).Caption = "Source"
                    c1Reconciliation.Cols(Col_All_sHistoryItem).Caption = "History"
                    c1Reconciliation.Cols(Col_All_Status).Caption = "Status"
                    c1Reconciliation.Cols(Col_All_DOEAllergy).Caption = "Date Entered"
                    c1Reconciliation.Cols(Col_All_sConceptID).Caption = "SnoMed CT ID"
                    c1Reconciliation.Cols(Col_All_sUserName).Caption = "User"
                    c1Reconciliation.Cols(Col_All_dtLastUpdated).Caption = "Last Updated"
                c1Reconciliation.Cols(Col_All_sTranID1).Caption = "RxNorm ID"

                ''width
                c1Reconciliation.Name = "Allergy_ReconcileList"
                If (ogloSettings.LoadGridColumnWidth(c1Reconciliation, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then




                    c1Reconciliation.Cols(COL_All_Select).Width = 0
                    c1Reconciliation.Cols(COL_All_ListName).Width = 200
                    c1Reconciliation.Cols(COL_All_Dup).Width = 30
                    c1Reconciliation.Cols(Col_All_nProblemID).Width = 0
                    c1Reconciliation.Cols(COL_All_nListID).Width = 0
                    c1Reconciliation.Cols(Col_All_nPatientID).Width = 0
                    c1Reconciliation.Cols(Col_All_bIsSkipped).Width = 0
                    c1Reconciliation.Cols(Col_All_sDrugName).Width = 0
                    c1Reconciliation.Cols(Col_All_sNDCCode).Width = 0
                    c1Reconciliation.Cols(Col_All_sReaction).Width = 85
                    c1Reconciliation.Cols(Col_All_sICD9).Width = 0
                    c1Reconciliation.Cols(Col_All_sCPT).Width = 0
                    c1Reconciliation.Cols(Col_All_sHistoryCategory).Width = 0
                    c1Reconciliation.Cols(Col_All_sTranID1).Width = 100
                    c1Reconciliation.Cols(Col_All_dtOnsetDate).Width = 0
                    c1Reconciliation.Cols(Col_All_nRowOrder).Width = 0


                    c1Reconciliation.Cols(Col_All_sSourceName).Width = 100
                    c1Reconciliation.Cols(Col_All_sHistoryItem).Width = 250
                    c1Reconciliation.Cols(Col_All_Status).Width = 80
                    c1Reconciliation.Cols(Col_All_DOEAllergy).Width = 100
                    c1Reconciliation.Cols(Col_All_sConceptID).Width = 100
                    c1Reconciliation.Cols(Col_All_sUserName).Width = 50
                    c1Reconciliation.Cols(Col_All_dtLastUpdated).Width = 100


                    c1Reconciliation.Cols(Col_All_sSourceName).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_sHistoryItem).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_Status).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_DOEAllergy).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_sConceptID).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_sUserName).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_dtLastUpdated).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_sReaction).AllowEditing = False
                    c1Reconciliation.Cols(Col_All_sTranID1).AllowEditing = False

                    c1Reconciliation.Cols(Col_All_VisitDate).Width = 0

                    ''
                    c1Reconciliation.Cols(Col_All_DrugID).Width = 0
                    c1Reconciliation.Cols(Col_All_MedicalConditionID).Width = 0
                    c1Reconciliation.Cols(Col_All_DescriptionID).Width = 0
                    c1Reconciliation.Cols(Col_All_SnomedID).Width = 0
                    c1Reconciliation.Cols(Col_All_Description).Width = 0
                    c1Reconciliation.Cols(Col_All_HistoryType).Width = 0
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
            c1Reconciliation.Redraw = True
        End Try


    End Sub

    Private Sub DesignConsolidatedList()

        C1ConsolidatedList.Redraw = False
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(gloLibCCDGeneral.Connectionstring)
        Try
            C1ConsolidatedList.AllowDragging = False
            If cmbListType.Text = "Medication" Then
                C1ConsolidatedList.Cols.Count = 38

                C1ConsolidatedList.Cols(COL_Med_Select).AllowEditing = True
                C1ConsolidatedList.Cols(COL_Med_Select).Caption = "Select"
                C1ConsolidatedList.Cols(COL_Med_Select).DataType = GetType(Boolean)


                C1ConsolidatedList.Cols(COL_Med_Dup).Caption = "Dup"
                C1ConsolidatedList.Cols(COL_Med_Dup).AllowEditing = False
                C1ConsolidatedList.Cols(COL_Med_Dup).AllowResizing = False
              
                C1ConsolidatedList.Cols(Col_Med_Source).Caption = "Source"
                C1ConsolidatedList.Cols(Col_Med_MedicationDate).Caption = "Last Updated"
                C1ConsolidatedList.Cols(Col_Med_Drug).Caption = "Drug"
                C1ConsolidatedList.Cols(Col_Med_StartDate).Caption = "Start Date"
                C1ConsolidatedList.Cols(Col_Med_EndDate).Caption = "End Date"
                C1ConsolidatedList.Cols(Col_Med_Frequency).Caption = "Frequency"
                C1ConsolidatedList.Cols(Col_Med_Amount).Caption = "Amount"
                C1ConsolidatedList.Cols(Col_Med_Refills).Caption = "Refills"
                C1ConsolidatedList.Cols(Col_Med_DrugForm).Caption = "Drug Form"
                C1ConsolidatedList.Cols(Col_Med_Status).Caption = "Status"
                C1ConsolidatedList.Cols(Col_Med_UserName).Caption = "User"
                C1ConsolidatedList.Cols(Col_Med_sNDCCode).Caption = "NDC Code"
               
                C1ConsolidatedList.Cols(Col_Med_Source).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_MedicationDate).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_Drug).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_StartDate).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_EndDate).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_Frequency).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_Amount).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_DrugForm).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_Refills).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_Status).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_UserName).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Med_sNDCCode).AllowEditing = False

                C1ConsolidatedList.Name = "Meds_ConsolidatedList"

                If (ogloSettings.LoadGridColumnWidth(C1ConsolidatedList, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then
                    C1ConsolidatedList.Cols(Col_MedicationID).Width = 0
                    C1ConsolidatedList.Cols(Col__Med_ListID).Width = 0
                    C1ConsolidatedList.Cols(Col_PatientID).Width = 0

                    C1ConsolidatedList.Cols(COL_Med_Dup).Width = 30
                    C1ConsolidatedList.Cols(Col_Med_Medication).Width = 0
                    C1ConsolidatedList.Cols(COL_Med_Select).Width = 50
                    C1ConsolidatedList.Cols(Col_Med_Source).Width = 120
                    C1ConsolidatedList.Cols(Col_Med_MedicationDate).Width = 85
                    C1ConsolidatedList.Cols(Col_Med_Drug).Width = 200
                    C1ConsolidatedList.Cols(Col_Med_StartDate).Width = 72
                    C1ConsolidatedList.Cols(Col_Med_EndDate).Width = 72
                    C1ConsolidatedList.Cols(Col_Med_Frequency).Width = 80
                    C1ConsolidatedList.Cols(Col_Med_Amount).Width = 65
                    C1ConsolidatedList.Cols(Col_Med_Refills).Width = 65
                    C1ConsolidatedList.Cols(Col_Med_DrugForm).Width = 80
                    C1ConsolidatedList.Cols(Col_Med_Status).Width = 80
                    C1ConsolidatedList.Cols(Col_Med_UserName).Width = 50
                    C1ConsolidatedList.Cols(Col_Med_sNDCCode).Width = 100

                    C1ConsolidatedList.Cols(Col_Med_Skip).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_sDosage).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_sRoute).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_sStrengthUnit).Width = 0

                    C1ConsolidatedList.Cols(COL_Med_ListName).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_VisitDate).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_Reason).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_DDID).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_PrescriptionID).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_MedicationID).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_Renewed).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_IsNarcotic).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_PBMName).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_bMaySubstitute).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_Rx_DMSID).Width = 0

                    C1ConsolidatedList.Cols(Col_Med_Method).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_PrescriberNotes).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_Duration).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_RxNotes).Width = 0
                    C1ConsolidatedList.Cols(Col_Med_RxnDrugID).Width = 0
                End If
              


                ''
            ElseIf cmbListType.Text = "Problem" Then

                C1ConsolidatedList.Cols.Count = 22

                C1ConsolidatedList.Cols(COL_Prob_Select).AllowEditing = True
                C1ConsolidatedList.Cols(COL_Prob_Select).Caption = "Select"
                C1ConsolidatedList.Cols(COL_Prob_Select).DataType = GetType(Boolean)


                C1ConsolidatedList.Cols(COL_Prob_Dup).AllowEditing = False
                C1ConsolidatedList.Cols(COL_Prob_Dup).Caption = "Dup"
                C1ConsolidatedList.Cols(COL_Prob_Dup).AllowResizing = False


                C1ConsolidatedList.Cols(Col_Prob_dtDOS).Caption = "DOS"
                C1ConsolidatedList.Cols(Col_Prob_Complaint).Caption = "Complaint"
                C1ConsolidatedList.Cols(Col_Prob_Diagnosis).Caption = "Diagnosis"
                C1ConsolidatedList.Cols(Col_Prob_Status).Caption = "Status"
                C1ConsolidatedList.Cols(Col_Prob_sConceptID).Caption = "SnoMed CT ID"
                C1ConsolidatedList.Cols(Col_Prob_User).Caption = "User"
                C1ConsolidatedList.Cols(Col_Prob_Skipped).Caption = "Keep"
                C1ConsolidatedList.Cols(Col_Prob_Source).Caption = "Source"
                C1ConsolidatedList.Cols(Col_Prob_ResolveDate).Caption = "Resolved Date"
                C1ConsolidatedList.Cols(Col_Prob_dtLastUpdated).Caption = "Last Updated"


                C1ConsolidatedList.Name = "Probs_ConsolidatedList"

                If (ogloSettings.LoadGridColumnWidth(C1ConsolidatedList, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then
                    C1ConsolidatedList.Cols(COL_Prob_ListName).Width = 0
                    C1ConsolidatedList.Cols(COL_Prob_Dup).Width = 30
                    C1ConsolidatedList.Cols(COL_Prob_Select).Width = 50
                    C1ConsolidatedList.Cols(Col_Prob_Source).Width = 120
                    C1ConsolidatedList.Cols(Col_Prob_dtDOS).Width = 85
                    C1ConsolidatedList.Cols(Col_Prob_Complaint).Width = 220
                    C1ConsolidatedList.Cols(Col_Prob_Diagnosis).Width = 220
                    C1ConsolidatedList.Cols(Col_Prob_Status).Width = 75
                    C1ConsolidatedList.Cols(Col_Prob_sConceptID).Width = 95
                    C1ConsolidatedList.Cols(Col_Prob_ResolveDate).Width = 92
                    C1ConsolidatedList.Cols(Col_Prob_User).Width = 50
                    C1ConsolidatedList.Cols(Col_Prob_dtLastUpdated).Width = 85

                    C1ConsolidatedList.Cols(Col_Prob_sICD9Code).Width = 0
                    C1ConsolidatedList.Cols(Col_Prob_sICD9Desc).Width = 0
                    C1ConsolidatedList.Cols(Col_Prob_nImmediacy).Width = 0
                    C1ConsolidatedList.Cols(Col_nProblemID).Width = 0
                    C1ConsolidatedList.Cols(COL_Prob_nListID).Width = 0
                    C1ConsolidatedList.Cols(Col_Prob_UserID).Width = 0
                    C1ConsolidatedList.Cols(Col_Prob_nPatientID).Width = 0
                    C1ConsolidatedList.Cols(Col_Prob_Skipped).Width = 0
                    C1ConsolidatedList.Cols(Col_Prob_Immediacy).Width = 0
                    C1ConsolidatedList.Cols(Col_Prob_nProblemStatus).Width = 0
                End If
              


                C1ConsolidatedList.Cols(Col_Prob_sConceptID).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_Source).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_dtDOS).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_Status).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_Complaint).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_Diagnosis).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_ResolveDate).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_User).AllowEditing = False
                C1ConsolidatedList.Cols(Col_Prob_dtLastUpdated).AllowEditing = False

            ElseIf cmbListType.Text = "Med Allergy" Then

                C1ConsolidatedList.Cols.Count = 30

                C1ConsolidatedList.Cols(COL_All_Select).AllowEditing = True
                C1ConsolidatedList.Cols(COL_All_Select).Caption = "Select"
                C1ConsolidatedList.Cols(COL_All_Select).DataType = GetType(Boolean)


                C1ConsolidatedList.Cols(COL_All_Dup).AllowEditing = False
                C1ConsolidatedList.Cols(COL_All_Dup).Caption = "Dup"
                C1ConsolidatedList.Cols(COL_All_Dup).AllowResizing = False


                C1ConsolidatedList.Cols(Col_All_sSourceName).Caption = "Source"
                C1ConsolidatedList.Cols(Col_All_sHistoryItem).Caption = "History"
                C1ConsolidatedList.Cols(Col_All_Status).Caption = "Status"
                C1ConsolidatedList.Cols(Col_All_DOEAllergy).Caption = "Date Entered"
                C1ConsolidatedList.Cols(Col_All_sConceptID).Caption = "SnoMed CT ID"
                C1ConsolidatedList.Cols(Col_All_sUserName).Caption = "User"
                C1ConsolidatedList.Cols(Col_All_dtLastUpdated).Caption = "Last Updated"
                C1ConsolidatedList.Cols(Col_All_sTranID1).Caption = "RxNorm ID"
               


                C1ConsolidatedList.Cols(Col_All_sSourceName).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_sHistoryItem).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_Status).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_DOEAllergy).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_sConceptID).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_sUserName).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_dtLastUpdated).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_sReaction).AllowEditing = False
                C1ConsolidatedList.Cols(Col_All_sTranID1).AllowEditing = False



                C1ConsolidatedList.Name = "Allergy_ConsolidatedList"

                If (ogloSettings.LoadGridColumnWidth(C1ConsolidatedList, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then
                    C1ConsolidatedList.Cols(COL_All_Select).Width = 50
                    C1ConsolidatedList.Cols(COL_All_Dup).Width = 30
                    C1ConsolidatedList.Cols(COL_All_ListName).Width = 0
                    C1ConsolidatedList.Cols(Col_All_VisitDate).Width = 0
                    ''
                    C1ConsolidatedList.Cols(Col_All_DrugID).Width = 0
                    C1ConsolidatedList.Cols(Col_All_MedicalConditionID).Width = 0
                    C1ConsolidatedList.Cols(Col_All_DescriptionID).Width = 0
                    C1ConsolidatedList.Cols(Col_All_SnomedID).Width = 0
                    C1ConsolidatedList.Cols(Col_All_Description).Width = 0
                    C1ConsolidatedList.Cols(Col_All_HistoryType).Width = 0
                    C1ConsolidatedList.Cols(Col_All_nProblemID).Width = 0
                    C1ConsolidatedList.Cols(COL_All_nListID).Width = 0
                    C1ConsolidatedList.Cols(Col_All_nPatientID).Width = 0
                    C1ConsolidatedList.Cols(Col_All_bIsSkipped).Width = 0
                    C1ConsolidatedList.Cols(Col_All_sDrugName).Width = 0
                    C1ConsolidatedList.Cols(Col_All_sNDCCode).Width = 0
                    C1ConsolidatedList.Cols(Col_All_sReaction).Width = 100
                    C1ConsolidatedList.Cols(Col_All_sICD9).Width = 0
                    C1ConsolidatedList.Cols(Col_All_sCPT).Width = 0
                    C1ConsolidatedList.Cols(Col_All_sHistoryCategory).Width = 0
                    C1ConsolidatedList.Cols(Col_All_sTranID1).Width = 0
                    C1ConsolidatedList.Cols(Col_All_dtOnsetDate).Width = 0
                    C1ConsolidatedList.Cols(Col_All_nRowOrder).Width = 0
                    C1ConsolidatedList.Cols(Col_All_sTranID1).Width = 100

                    C1ConsolidatedList.Cols(Col_All_sSourceName).Width = 120
                    C1ConsolidatedList.Cols(Col_All_sHistoryItem).Width = 300
                    C1ConsolidatedList.Cols(Col_All_Status).Width = 100
                    C1ConsolidatedList.Cols(Col_All_DOEAllergy).Width = 120
                    C1ConsolidatedList.Cols(Col_All_sConceptID).Width = 120
                    C1ConsolidatedList.Cols(Col_All_sUserName).Width = 50
                    C1ConsolidatedList.Cols(Col_All_dtLastUpdated).Width = 120
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
            C1ConsolidatedList.Redraw = True
        End Try


    End Sub

    Private Sub CreateC1Styles()

        cStyle_Blue = c1Reconciliation.Styles.Add("StyleBlue")
        cStyle_Blue.ForeColor = Drawing.Color.Black
        cStyle_Blue.Font = New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
        cStyle_Blue.BackColor = System.Drawing.Color.FromArgb(215, 226, 253)


        cStyle_Pink = c1Reconciliation.Styles.Add("StylePink")
        cStyle_Pink.ForeColor = Drawing.Color.Black
        cStyle_Pink.Font = New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
        cStyle_Pink.BackColor = System.Drawing.Color.White


        cStyle_StrikoutFont = C1ConsolidatedList.Styles.Add("StyleStrikout")
        cStyle_StrikoutFont.Font = New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout)
        cStyle_StrikoutFont.ForeColor = Drawing.Color.DarkGray

        cStyle_NormalFont = C1ConsolidatedList.Styles.Add("StyleRegular")
        cStyle_NormalFont.Font = New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)

    End Sub

#End Region

#Region "Fill Grid Data"

    Private Sub FillListTypeGrid()

        Dim objgloCCDReconcilation As New gloCCDReconcilation
        Dim _dtList As DataTable = Nothing
        Dim Dv As DataView = Nothing
        Try


            _dtList = objgloCCDReconcilation.GetReconcilationList(_nPatientID)


            If Not IsNothing(_dtList) Then

                'Bind and show slected List Type
                Dv = _dtList.DefaultView()
                If cmbListType.Text = "Med Allergy" Then
                    Dv.RowFilter = "sListType = '" & "Allergy" & "'"
                Else
                    Dv.RowFilter = "sListType = '" & cmbListType.Text & "'"
                End If

                c1ListType.DataSource = Dv.ToTable()
                If Not IsNothing(Dv) Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                'Create and Display list ready mesage
                If _dtList.Rows.Count > 0 Then
                    Dim StrWarning As String = ""
                    Dim i As Integer = 0
                    For i = 0 To cmbListType.Items.Count - 1
                        Dv = New DataView(_dtList)
                        If cmbListType.Items(i).ToString() = "Med Allergy" Then
                            Dv.RowFilter = "sListType = '" & "Allergy" & "' And  sListStatus='Ready'"
                        Else
                            Dv.RowFilter = "sListType = '" & cmbListType.Items(i).ToString() & "' And  sListStatus='Ready'"
                        End If

                        If (Dv.ToTable().Rows.Count) > 0 Then
                            If StrWarning = "" Then
                                'If cmbListType.Items(i).ToString() = "Allergy" Then
                                '    StrWarning = "Med Allergy"
                                'Else
                                StrWarning = cmbListType.Items(i).ToString()
                                'End If

                            Else
                                'If cmbListType.Items(i).ToString() = "Allergy" Then
                                '    StrWarning = StrWarning & ", " & "Med Allergy"
                                'Else
                                StrWarning = StrWarning & ", " & cmbListType.Items(i).ToString()

                                'End If

                            End If
                        End If
                        If Not IsNothing(Dv) Then
                            Dv.Dispose()
                            Dv = Nothing
                        End If
                    Next
                    If StrWarning <> "" Then
                        StrWarning = "Patient has " & StrWarning & " Lists. "
                    Else
                        StrWarning = "Patient has " & "No Ready" & " Lists. "
                    End If

                    lblBottomNote.Text = StrWarning
                Else

                    lblBottomNote.Text = ""
                End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If Not IsNothing(objgloCCDReconcilation) Then
                objgloCCDReconcilation.Dispose()
                objgloCCDReconcilation = Nothing
            End If

            If Not IsNothing(_dtList) Then
                _dtList.Dispose()
                _dtList = Nothing
            End If
            If Not IsNothing(Dv) Then
                Dv.Dispose()
                Dv = Nothing
            End If
        End Try

    End Sub

    Private Sub FillReconciliationGrid(ByVal nListID As Long, ByVal sListType As String)

        Dim objgloCCDReconcilation As New gloCCDReconcilation
        Dim _dtExtractedList As DataTable = Nothing

        Try



            If Not IsNothing(c1Reconciliation.DataSource) Then
                ''Fetch Only Selected list Type details
                _dtExtractedList = objgloCCDReconcilation.GetExtractedList(nListID, sListType, 0)
            Else
                ''Fetch EMR Patientchart and Selected list Type details
                _dtExtractedList = objgloCCDReconcilation.GetExtractedList(nListID, sListType, _nPatientID)
            End If

            If _dtExtractedList.Rows.Count > 0 Then
                BindToReconciliationGrid(_dtExtractedList)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If Not IsNothing(objgloCCDReconcilation) Then
                objgloCCDReconcilation.Dispose()
                objgloCCDReconcilation = Nothing
            End If
            ''dont dispose this becoz we have bind it as datasource.
            'If Not IsNothing(_dtExtractedList) Then
            '    _dtExtractedList.Dispose()
            '    _dtExtractedList = Nothing
            'End If
        End Try

    End Sub

    Private Sub DefaultReconcialationList()



        Try
            c1Reconciliation.DataSource = Nothing
            c1Reconciliation.Rows.Count = 1
            If cmbListType.Text = "Medication" Then
                chkHideCurrentData.Text = "Hide Patient’s Discontinued and Completed Medications"
            ElseIf cmbListType.Text = "Problem" Then
                chkHideCurrentData.Text = "Hide Patient’s Resolved Problems"
            ElseIf cmbListType.Text = "Med Allergy" Then
                chkHideCurrentData.Text = "Hide Patient’s Inactive Med Allergies"
            End If

            If c1ListType.Rows.Count > 1 Then
                Dim k As Integer = 1
                For k = 1 To c1ListType.Rows.Count - 1
                    If c1ListType.GetData(k, COL_Select) = 1 Then
                        If cmbListType.Text = "Med Allergy" Then
                            FillReconciliationGrid(c1ListType.GetData(k, COL_ListID), "Allergy")
                        Else
                            FillReconciliationGrid(c1ListType.GetData(k, COL_ListID), cmbListType.Text)
                        End If
                    Else
                        ''Added by Mayuri:20130516-Fixed issue#50643
                        ''Fetch EMR Patientchart
                        If cmbListType.Text = "Med Allergy" Then
                            FillReconciliationGrid(0, "Allergy")
                        Else
                            FillReconciliationGrid(0, cmbListType.Text)
                        End If
                    End If
                Next
            Else
                ''Fetch EMR Patientchart
                If cmbListType.Text = "Med Allergy" Then
                    FillReconciliationGrid(0, "Allergy")
                Else
                    FillReconciliationGrid(0, cmbListType.Text)
                End If

            End If

            DesignReconciliationGrid()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

        End Try

    End Sub


    Private Sub BindToReconciliationGrid(ByVal dtSelectedListDetails As DataTable)
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Dim dtFileData As DataTable = Nothing
        Dim dtCurrent As DataTable = Nothing
        Dim _dtGridDataSource As DataTable

        Try
            ''

            If Not IsNothing(c1Reconciliation.DataSource) Then
                _dtGridDataSource = c1Reconciliation.DataSource
                _dtGridDataSource.Merge(dtSelectedListDetails)
                c1Reconciliation.DataSource = _dtGridDataSource
            Else
                c1Reconciliation.DataSource = dtSelectedListDetails
            End If

            If chkHideCurrentData.Checked Then
                If cmbListType.Text = "Medication" Then
                    dt = c1Reconciliation.DataSource
                    _dtTemp = dt.Copy()
                    dv = _dtTemp.DefaultView
                    dv.RowFilter = "Source <> 'Current'"
                    dtFileData = dv.ToTable.Copy()
                    dv.RowFilter = ""
                    dv.RowFilter = "Source ='Current'"
                    dtCurrent = dv.ToTable.Copy()
                    dv.RowFilter = ""
                    dv = dtCurrent.DefaultView

                    dv.RowFilter = "Status <> 'completed' and Status <> 'discontinued'"
                    dtCurrent = dv.ToTable.Copy()
                    dtCurrent.Merge(dtFileData)
                    c1Reconciliation.DataSource = dtCurrent

                ElseIf cmbListType.Text = "Problem" Then
                    dt = c1Reconciliation.DataSource
                    _dtTemp = dt.Copy()
                    dv = _dtTemp.DefaultView
                    dv.RowFilter = "Source <> 'Current'"
                    dtFileData = dv.ToTable.Copy()
                    dv.RowFilter = ""
                    dv.RowFilter = "Source ='Current'"
                    dtCurrent = dv.ToTable.Copy()
                    dv.RowFilter = ""
                    dv = dtCurrent.DefaultView

                    dv.RowFilter = "Status <> 'resolved'"
                    dtCurrent = dv.ToTable.Copy()
                    dtCurrent.Merge(dtFileData)
                    c1Reconciliation.DataSource = dtCurrent
                ElseIf cmbListType.Text = "Med Allergy" Then
                    dt = c1Reconciliation.DataSource
                    _dtTemp = dt.Copy()
                    dv = _dtTemp.DefaultView
                    dv.RowFilter = "Source <> 'Current'"
                    dtFileData = dv.ToTable.Copy()
                    dv.RowFilter = ""
                    dv.RowFilter = "Source ='Current'"
                    dtCurrent = dv.ToTable.Copy()
                    dv.RowFilter = ""
                    dv = dtCurrent.DefaultView

                    dv.RowFilter = "Status <> 'inactive'"
                    dtCurrent = dv.ToTable.Copy()
                    dtCurrent.Merge(dtFileData)
                    c1Reconciliation.DataSource = dtCurrent
                End If
            End If



        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(dtFileData) Then
                dtFileData.Dispose()
                dtFileData = Nothing
            End If
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
        End Try

    End Sub

#End Region

#Region "Toolstrip Button"
    Private Sub tsTop_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tsTop.ItemClicked
        tsTop.Select()
        Select Case e.ClickedItem.Tag
            Case "Finalize"
                FinalizeReconcileList()
            Case "Close"
                Me.Close()
        End Select

    End Sub
#End Region

#Region "Events"
    Private Sub c1ListType_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ListType.AfterEdit
        If e.Col = 0 Then
            Try
                SaveReconcileColumnWidth()
                If c1ListType.GetData(e.Row, e.Col) = 1 Then
                    If cmbListType.Text = "Med Allergy" Then
                        FillReconciliationGrid(c1ListType.GetData(e.Row, COL_ListID), "Allergy")
                    Else
                        FillReconciliationGrid(c1ListType.GetData(e.Row, COL_ListID), cmbListType.Text)
                    End If

                Else

                    ''On unselect Remove List details from grid 
                    Dim ListId As Long = c1ListType.GetData(e.Row, COL_ListID)
                    Dim DvGirdNow As DataView
                    If Not IsNothing(c1Reconciliation.DataSource) Then
                        DvGirdNow = CType((c1Reconciliation.DataSource), DataTable).DefaultView
                        DvGirdNow.RowFilter = "nListID <> " & ListId
                        If (DvGirdNow.Count > 0) Then
                            c1Reconciliation.DataSource = Nothing
                            c1Reconciliation.DataSource = DvGirdNow.ToTable()
                        Else
                            c1Reconciliation.DataSource = Nothing

                        End If
                    End If

                End If

                DesignReconciliationGrid()

            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            End Try

        End If
        FillConsolidatedList()

        RemoveExactDuplicates()
        HighLightSimilarDuplicates()
        HighLightExactDuplicates()


    End Sub

    Private Sub cmbListType_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbListType.Enter
        SaveReconcileColumnWidth()

    End Sub


    Private Sub cmbListType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbListType.SelectedIndexChanged

        Try

            If _IsFormLoading = False Then

                FillListTypeGrid()
                DesignListTypeGrid()
                c1Reconciliation.DataSource = Nothing
                c1Reconciliation.Rows.Count = 1
                C1ConsolidatedList.DataSource = Nothing
                C1ConsolidatedList.Rows.Count = 1
                DefaultReconcialationList()

                FillConsolidatedList()


                RemoveExactDuplicates()
                HighLightSimilarDuplicates()
                HighLightExactDuplicates()



            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

        End Try

    End Sub


    Private Sub c1ListType_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1ListType.BeforeEdit
        If e.Row > 0 Then
            If Convert.ToString(c1ListType.GetData(e.Row, COL_ListStatus)) = "Finished" Then
                e.Cancel = True

            End If
        End If
    End Sub


#End Region

#Region "Reconciliation List HighLight Exact Duplicates"

    Private Function IsExactMedExisits(ByVal DrugName As String, ByVal Frequency As String, ByVal Dosage As String, ByVal NDCCode As String, ByVal Status As String) As Boolean
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try

            dt = c1Reconciliation.DataSource
            _dtTemp = dt.Copy()
            dv = _dtTemp.DefaultView
            If cmbListType.Text = "Medication" Then

                dv.RowFilter = "MedicationName = '" & DrugName & "' AND Frequency ='" & Frequency & "' AND sDosage='" & Dosage & "' AND NDCCode='" & NDCCode & "' AND Status='" & Status & "' "

            End If

            If dv.Count >= 2 Then
                Return True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(_dtTemp) Then
                _dtTemp.Dispose()
                _dtTemp = Nothing
            End If

        End Try

    End Function

    Private Function IsExactProbExisits(ByVal Complaint As String, ByVal SnoMedCode As String, ByVal DOS As String, ByVal ResolveDate As String, ByVal Status As String) As Boolean
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try

            dt = c1Reconciliation.DataSource
            _dtTemp = dt.Copy()
            dv = _dtTemp.DefaultView
            If cmbListType.Text = "Problem" Then
                If ResolveDate.Trim <> "" Then
                    dv.RowFilter = "Complaint = '" & Complaint & "' AND [Snomed CT ID] ='" & SnoMedCode & "' AND DOS ='" & DOS & "' AND [Resolved Date]='" & ResolveDate & "' AND Status ='" & Status & "'"
                Else
                    dv.RowFilter = "Complaint = '" & Complaint & "' AND [Snomed CT ID] ='" & SnoMedCode & "' AND DOS ='" & DOS & "' AND Status ='" & Status & "'" ' AND [Resolved Date] IS NULL"
                End If

            End If

            If dv.Count >= 2 Then
                Return True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(_dtTemp) Then
                _dtTemp.Dispose()
                _dtTemp = Nothing
            End If

        End Try
    End Function

    Private Function IsExactAllergyExisits(ByVal RxNorm As String, ByVal Allergy As String, ByVal Reaction As String, ByVal Status As String) As Boolean
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try

            dt = c1Reconciliation.DataSource
            _dtTemp = dt.Copy()
            dv = _dtTemp.DefaultView
            If cmbListType.Text = "Med Allergy" Then

                dv.RowFilter = "History ='" & Allergy & "' AND [RxNorm ID] ='" & RxNorm & "' AND Reaction ='" & Reaction & "' AND Status='" & Status & "'"

            End If

            If dv.Count >= 2 Then
                Return True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(_dtTemp) Then
                _dtTemp.Dispose()
                _dtTemp = Nothing
            End If

        End Try

    End Function

    Private Sub HighLightExactDuplicates()
        c1Reconciliation.Redraw = False

        Try

            Dim i As Int32 = 1
            Dim _nActiveRowCount As Int32 = 0
            Dim _bActiveFound As Boolean = False
            Dim _sItemSnomedCode As String = ""
            Dim _sItemComplaint As String = ""
            Dim _sItemRxNormCode As String = ""
            Dim _sItemAllergy As String = ""
            Dim _sItemDrugName As String = ""
            Dim _sItemFrequency As String = ""
            Dim _sItemDosage As String = ""
            Dim _sItemNDCCode As String = ""
            Dim _sItemReaction As String = ""
            Dim _sItemStatus As String = ""
            Dim _sItemDos As String = ""
            Dim _sItemResolveDate As String = ""
            For i = 1 To c1Reconciliation.Rows.Count - 1
                Select Case cmbListType.Text
                    Case "Medication"


                        _sItemDrugName = Convert.ToString(c1Reconciliation.GetData(i, Col_Med_Medication))
                        _sItemFrequency = Convert.ToString(c1Reconciliation.GetData(i, Col_Med_Frequency))
                        _sItemDosage = Convert.ToString(c1Reconciliation.GetData(i, Col_Med_sDosage))
                        _sItemNDCCode = Convert.ToString(c1Reconciliation.GetData(i, Col_Med_sNDCCode))
                        _sItemStatus = Convert.ToString(c1Reconciliation.GetData(i, Col_Med_Status))
                        If IsExactMedExisits(_sItemDrugName, _sItemFrequency, _sItemDosage, _sItemNDCCode, _sItemStatus) Then
                            c1Reconciliation.SetCellImage(i, COL_Med_Dup, ImgGrid.Images(2))
                            c1Reconciliation.SetData(i, COL_Med_Dup, "  Duplicate")
                        Else
                            c1Reconciliation.SetData(i, COL_Med_Dup, Nothing)
                            c1Reconciliation.SetCellImage(i, COL_Med_Dup, Nothing)
                        End If



                    Case "Problem"

                        _sItemComplaint = Convert.ToString(c1Reconciliation.GetData(i, Col_Prob_Complaint))
                        _sItemSnomedCode = Convert.ToString(c1Reconciliation.GetData(i, Col_Prob_sConceptID))
                        If Convert.ToString(c1Reconciliation.Rows(i)(Col_Prob_dtDOS)) <> "" Then
                            '_sItemDos = Date.Parse(c1Reconciliation.Rows(i)(Col_Prob_dtDOS)).Date()
                            _sItemDos = Convert.ToString(c1Reconciliation.Rows(i)(Col_Prob_dtDOS))
                        Else
                            _sItemDos = ""
                        End If
                        If Convert.ToString(c1Reconciliation.Rows(i)(Col_Prob_ResolveDate)) <> "" Then
                            '_sItemResolveDate = Date.Parse(c1Reconciliation.Rows(i)(Col_Prob_ResolveDate)).Date()
                            _sItemResolveDate = Convert.ToString(c1Reconciliation.Rows(i)(Col_Prob_ResolveDate))
                        Else
                            _sItemResolveDate = ""
                        End If
                        _sItemStatus = Convert.ToString(c1Reconciliation.GetData(i, Col_Prob_Status))
                        If IsExactProbExisits(_sItemComplaint, _sItemSnomedCode, _sItemDos, _sItemResolveDate, _sItemStatus) Then
                            c1Reconciliation.SetCellImage(i, COL_Prob_Dup, ImgGrid.Images(2))
                            c1Reconciliation.SetData(i, COL_Prob_Dup, "  Duplicate")
                        Else
                            c1Reconciliation.SetData(i, COL_Prob_Dup, Nothing)
                            c1Reconciliation.SetCellImage(i, COL_Prob_Dup, Nothing)
                        End If



                    Case "Med Allergy"
                        ''''
                        Dim strReaction As String


                        'Dim arr() As String 'Srting Array
                        'Dim strRection_Status As String = ""
                        'strRection_Status = Convert.ToString(c1Reconciliation.GetData(i, Col_All_sReaction))
                        'arr = Split(strRection_Status, "|")
                        'If arr.Length = 2 Then
                        '    strReaction = arr.GetValue(0)
                        '    strActive = arr.GetValue(1)
                        'Else
                        '    strReaction = strRection_Status
                        '    strActive = False
                        'End If

                        'Dim arrReaction As String()
                        'arrReaction = strReaction.Split(vbNewLine)
                        ''dsHistory.Tables("History").Rows(_Row)(Col_HsReaction) = strReaction
                        'c1Reconciliation.SetData(i, Col_All_sReaction, strReaction)



                        Dim strRection_Status As String = ""
                        strReaction = Convert.ToString(c1Reconciliation.GetData(i, Col_All_sReaction))

                        Dim arrReaction As String()
                        arrReaction = strReaction.Split(vbNewLine)
                        c1Reconciliation.Rows(i).Height = c1Reconciliation.Rows.DefaultSize * arrReaction.Length - 1

                        ''''

                        _sItemRxNormCode = Convert.ToString(c1Reconciliation.GetData(i, Col_All_sTranID1))
                        _sItemAllergy = Convert.ToString(c1Reconciliation.GetData(i, Col_All_sHistoryItem))
                        _sItemReaction = Convert.ToString(c1Reconciliation.GetData(i, Col_All_sReaction))
                        _sItemStatus = Convert.ToString(c1Reconciliation.GetData(i, Col_All_Status))
                        If IsExactAllergyExisits(_sItemRxNormCode, _sItemAllergy, _sItemReaction, _sItemStatus) Then
                            c1Reconciliation.SetCellImage(i, COL_All_Dup, ImgGrid.Images(2))
                            c1Reconciliation.SetData(i, COL_All_Dup, "  Duplicate")
                        Else
                            c1Reconciliation.SetData(i, COL_All_Dup, Nothing)
                            c1Reconciliation.SetCellImage(i, COL_All_Dup, Nothing)
                        End If


                End Select
            Next


            AlternateListStyle()
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            c1Reconciliation.Redraw = True

        End Try
    End Sub

    Private Sub AlternateListStyle()
        c1Reconciliation.Redraw = False
        Dim dv As DataView = Nothing

        Dim dt As DataTable
        Dim strcolumns(0) As String

        Dim dtOriginal As DataTable
        Dim _toprow As Int32
        Dim _bottomrow As Int32
        Dim _cellStyle As C1.Win.C1FlexGrid.CellStyle = Nothing
        Try

            strcolumns.SetValue("nListID", 0)

            If Not IsNothing(c1Reconciliation.DataSource) Then


                dt = c1Reconciliation.DataSource
                dtOriginal = c1Reconciliation.DataSource

                dv = dtOriginal.Copy().DefaultView
                dt = dt.Copy().DefaultView.ToTable(True, strcolumns)

                Dim rngRows As New C1.Win.C1FlexGrid.CellRange

                For j As Integer = 0 To dt.Rows.Count - 1
                    dv.RowFilter = "nListID='" & dt.Rows(j)(0) & "'"
                    _toprow = _bottomrow + 1
                    _bottomrow = _bottomrow + dv.Count

                    If (j Mod 2) = 0 Then
                        rngRows = c1Reconciliation.GetCellRange(_toprow, 0, _bottomrow, c1Reconciliation.Cols.Count - 1)
                        rngRows.Style = Nothing
                        rngRows.Style = cStyle_Blue
                    Else
                        rngRows = c1Reconciliation.GetCellRange(_toprow, 0, _bottomrow, c1Reconciliation.Cols.Count - 1)
                        rngRows.Style = Nothing
                        rngRows.Style = cStyle_Pink

                    End If
                Next
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            c1Reconciliation.Redraw = True
            'If Not IsNothing(dv) Then
            '    dv.Dispose()
            '    dv = Nothing
            'End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            'If Not IsNothing(dtOriginal) Then
            '    dtOriginal.Dispose()
            '    dtOriginal = Nothing
            'End If

        End Try


    End Sub


    Private Sub c1Reconciliation_BeforeSelChange(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles c1Reconciliation.BeforeSelChange
        Try
            If e.NewRange.r1 > 0 Then
                If Not IsNothing(e.NewRange.Style) Then
                    c1Reconciliation.Styles.Highlight.BackColor = e.NewRange.Style.BackColor
                    c1Reconciliation.Styles.Focus.BackColor = e.NewRange.Style.BackColor
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Finalize List"
    Private Sub FinalizeReconcileList()

        Try


            Dim strListID As String = String.Empty
            Dim nTableCount As Integer
            For nTableCount = 1 To c1ListType.Rows.Count - 1
                If c1ListType.GetData(nTableCount, COL_Select) = 1 Then
                    If strListID = "" Then
                        strListID = c1ListType.Rows(nTableCount).Item(COL_ListID)
                    Else
                        strListID = strListID & "," & c1ListType.Rows(nTableCount).Item(COL_ListID)
                    End If
                End If
            Next
            Dim strSkippedID As String = String.Empty
            Dim nTblCount As Integer
            Dim _Cnt As Int32 = 0
            For nTblCount = 1 To C1ConsolidatedList.Rows.Count - 1
                If cmbListType.Text = "Medication" Then
                    If Not IsDBNull(C1ConsolidatedList.GetData(nTblCount, COL_Med_Select)) Then
                        If C1ConsolidatedList.GetData(nTblCount, COL_Med_Select) <> 1 Then
                            If strSkippedID = "" Then
                                strSkippedID = C1ConsolidatedList.Rows(nTblCount).Item(Col_MedicationID)
                            Else
                                strSkippedID = strSkippedID & "," & C1ConsolidatedList.Rows(nTblCount).Item(Col_MedicationID)
                            End If
                            _Cnt = _Cnt + 1
                        End If
                    End If
                ElseIf cmbListType.Text = "Problem" Then
                    If Not IsDBNull(C1ConsolidatedList.GetData(nTblCount, COL_Prob_Select)) Then
                        If C1ConsolidatedList.GetData(nTblCount, COL_Prob_Select) <> 1 Then
                            If strSkippedID = "" Then
                                strSkippedID = C1ConsolidatedList.Rows(nTblCount).Item(Col_nProblemID)
                            Else
                                strSkippedID = strSkippedID & "," & C1ConsolidatedList.Rows(nTblCount).Item(Col_nProblemID)
                            End If
                            _Cnt = _Cnt + 1
                        End If
                    End If
                ElseIf cmbListType.Text = "Med Allergy" Then
                    If Not IsDBNull(C1ConsolidatedList.GetData(nTblCount, COL_All_Select)) Then
                        If C1ConsolidatedList.GetData(nTblCount, COL_All_Select) <> "1" Then
                            If strSkippedID = "" Then
                                strSkippedID = C1ConsolidatedList.Rows(nTblCount).Item(Col_All_nProblemID)
                            Else
                                strSkippedID = strSkippedID & "," & C1ConsolidatedList.Rows(nTblCount).Item(Col_All_nProblemID)
                            End If
                            _Cnt = _Cnt + 1
                        End If
                    End If
                End If

            Next

            If Not IsNothing(C1ConsolidatedList.Rows) AndAlso C1ConsolidatedList.Rows.Count > 0 Then
                If _Cnt = C1ConsolidatedList.Rows.Count - 1 Then
                    MessageBox.Show("Please select item to finalize", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If

            Dim dtReconcile As DataTable
            If IsNothing(C1ConsolidatedList.DataSource) = False Then
                dtReconcile = C1ConsolidatedList.DataSource
                Dim objfrmFinalizReconcile As gloCCDLibrary.frmFinalizeReconcileList
                If cmbListType.Text = "Med Allergy" Then
                    objfrmFinalizReconcile = New gloCCDLibrary.frmFinalizeReconcileList(dtReconcile.Copy(), "Allergy", _nPatientID, strListID, strSkippedID, _UserName, _UserID)
                Else
                    objfrmFinalizReconcile = New gloCCDLibrary.frmFinalizeReconcileList(dtReconcile.Copy(), cmbListType.Text, _nPatientID, strListID, strSkippedID, _UserName, _UserID)
                End If

                objfrmFinalizReconcile.ShowDialog(Me)

                'If objfrmFinalizReconcile.ShowDialog(Me) = DialogResult.OK Then
                '    Me.DialogResult = DialogResult.OK
                'End If
                MedicationVisitID = objfrmFinalizReconcile.MedicationVisitID
                If IsNothing(objfrmFinalizReconcile) = False Then
                    objfrmFinalizReconcile.Dispose()
                    objfrmFinalizReconcile = Nothing
                End If

                Dim Dt As DataTable = c1ListType.DataSource
                Dim dv As DataView = Dt.DefaultView
                If cmbListType.Text = "Med Allergy" Then
                    cmbListType.Text = "Medication"
                    cmbListType_SelectedIndexChanged(Nothing, Nothing)
                ElseIf cmbListType.Text = "Medication" Then
                    cmbListType.Text = "Problem"
                    cmbListType_SelectedIndexChanged(Nothing, Nothing)
                ElseIf cmbListType.Text = "Problem" Then
                    cmbListType.Text = "Med Allergy"
                    cmbListType_SelectedIndexChanged(Nothing, Nothing)
                End If


                If lblBottomNote.Text = "Patient has No Ready Lists. " Then
                    MessageBox.Show(lblBottomNote.Text, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If

                SaveReconcileColumnWidth()
                FillListTypeGrid()
                If _IsFormLoading = False Then
                    If lblBottomNote.Text = "" Then
                        MessageBox.Show("Patient has " & "No Ready" & " Lists. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    End If
                  
                End If
               
                DesignListTypeGrid()
                DefaultReconcialationList()
                FillConsolidatedList()

                RemoveExactDuplicates()
                HighLightSimilarDuplicates()
                HighLightExactDuplicates()



            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Reconcile, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
#End Region

#Region "Marked as Finished and Ready"

    Private Sub c1ListType_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1ListType.MouseDown
        Try
            mnuMarkedFinished.Visible = False
            mnuMarkasReady.Visible = False
            If e.Button = Windows.Forms.MouseButtons.Right Then

                Dim r As Integer = c1ListType.HitTest(e.X, e.Y).Row
                If r > 0 Then
                    c1ListType.Select(r, True)

                    c1ListType.ContextMenuStrip = cntListType
                    If Convert.ToString(c1ListType.GetData(r, COL_ListStatus)) = "Ready" Then
                        mnuMarkedFinished.Visible = True
                        mnuMarkasReady.Visible = False
                    Else
                        mnuMarkedFinished.Visible = False
                        mnuMarkasReady.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Reconcile, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub mnuMarkedFinished_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMarkedFinished.Click

        Try


            Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
            Dim r As Integer = c1ListType.RowSel
            Dim _strListID As String = ""
            _strListID = Convert.ToString(c1ListType.GetData(r, COL_ListID))
            If r > 0 Then
                ogloPatientRegDBLayer.UpdateStatus(0, _nPatientID, _strListID, False, False, ListStatus.Finished)
            End If
            If IsNothing(ogloPatientRegDBLayer) = False Then
                ogloPatientRegDBLayer.Dispose()
                ogloPatientRegDBLayer = Nothing
            End If
            SaveReconcileColumnWidth()
            FillListTypeGrid()
            DesignListTypeGrid()
            DefaultReconcialationList()
            FillConsolidatedList()

            RemoveExactDuplicates()
            HighLightSimilarDuplicates()
            HighLightExactDuplicates()



        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Reconcile, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub mnuMarkasReady_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMarkasReady.Click

        Try
            Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(gloLibCCDGeneral.Connectionstring)
            Dim r As Integer = c1ListType.RowSel
            Dim _strListID As String = ""
            _strListID = Convert.ToString(c1ListType.GetData(r, COL_ListID))
            If r > 0 Then
                ogloPatientRegDBLayer.UpdateStatus(0, _nPatientID, _strListID, False, True)
            End If
            If IsNothing(ogloPatientRegDBLayer) = False Then
                ogloPatientRegDBLayer.Dispose()
                ogloPatientRegDBLayer = Nothing
            End If
            SaveReconcileColumnWidth()
            FillListTypeGrid()
            DesignListTypeGrid()
            DesignReconciliationGrid()
            DefaultReconcialationList()
            FillConsolidatedList()

            RemoveExactDuplicates()
            HighLightSimilarDuplicates()
            HighLightExactDuplicates()


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Reconcile, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Consolidate List Fill and HighLight Similar Duplicates and Remove Exact Duplicates"

    Private Sub FillConsolidatedList()
        Dim dtConsolidated As DataTable
        C1ConsolidatedList.DataSource = Nothing
        C1ConsolidatedList.Rows.Count = 1
        Try
            If Not IsNothing(c1Reconciliation.DataSource) Then
                If c1Reconciliation.Rows.Count > 0 Then
                    dtConsolidated = c1Reconciliation.DataSource

                    C1ConsolidatedList.DataSource = dtConsolidated.Copy()

                End If

            End If
            DesignConsolidatedList()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

        End Try

    End Sub

    Private Sub RemoveExactDuplicates()
        C1ConsolidatedList.Redraw = False

        Try

            Dim i As Int32 = 1

            Dim _sItemSnomedCode As String = ""

            Dim _sItemSnomedCode1 As String = ""


            Dim _sItemComplaint As String = ""
            Dim _sItemComplaint1 As String = ""
            Dim _sItemRxNormCode As String = ""
            Dim _sItemRemoveRxNormCode As String = ""
            Dim _sItemAllergy As String = ""
            Dim _sItemRemoveAllergy As String = ""

            Dim _sItemReaction As String = ""
            Dim _sItemRemoveReaction As String = ""

            Dim _sItemDrugName As String = ""
            Dim _sItemDrugName1 As String = ""
            Dim _sItemFrequency As String = ""
            Dim _sItemFrequency1 As String = ""
            Dim _sItemDosage As String = ""
            Dim _sItemDosage1 As String = ""

            Dim _sItemNDCCode As String = ""
            Dim _sItemNDCCode1 As String = ""

            Dim _sItemStatus As String = ""
            Dim _sItemStatus1 As String = ""
            Dim _sItemDos As Date
            Dim _sItemResolveDate As Date
            Dim _sItemRemoveDos As Date
            Dim _sItemRemoveResolveDate As Date

            For i = 1 To C1ConsolidatedList.Rows.Count - 1

                Select Case cmbListType.Text
                    Case "Medication"


                        _sItemDrugName = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_Medication)).Trim
                        _sItemFrequency = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_Frequency)).Trim
                        _sItemDosage = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_sDosage)).Trim
                        _sItemNDCCode = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_sNDCCode)).Trim
                        _sItemStatus = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Med_Status)).Trim

                        For j As Integer = i + 1 To C1ConsolidatedList.Rows.Count - 1
                            _sItemDrugName1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_Medication)).Trim
                            _sItemFrequency1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_Frequency)).Trim
                            _sItemDosage1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_sDosage)).Trim
                            _sItemNDCCode1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_sNDCCode)).Trim
                            _sItemStatus1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_Med_Status)).Trim
                            '  If _sItemNDCCode1 <> "" And _sItemNDCCode <> "" And _sItemDrugName1 <> "" And _sItemDrugName <> "" And _sItemFrequency1 <> "" And _sItemFrequency <> "" And _sItemDosage1 <> "" And _sItemDosage <> "" Then
                            If (_sItemNDCCode.ToUpper = _sItemNDCCode1.ToUpper) And (_sItemDrugName1.ToUpper = _sItemDrugName.ToUpper) And (_sItemFrequency1.ToUpper = _sItemFrequency.ToUpper) And (_sItemDosage1.ToUpper = _sItemDosage.ToUpper) And (_sItemStatus.ToUpper = _sItemStatus1.ToUpper) Then
                                C1ConsolidatedList.Rows(j).Visible = False
                                'C1ConsolidatedList.Rows.Remove(j)

                            End If
                            ' End If
                        Next

                    Case "Problem"

                        _sItemComplaint = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_Complaint)).Trim
                        _sItemSnomedCode = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_sConceptID)).Trim

                        If Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_dtDOS)).Trim <> "" Then
                            '_sItemDos = Date.Parse(C1ConsolidatedList.Rows(i)(Col_Prob_dtDOS)).Date()
                            _sItemDos = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_dtDOS)).Trim
                        Else
                            _sItemDos = Nothing
                        End If


                        If Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_ResolveDate)).Trim <> "" Then
                            ' _sItemResolveDate = Date.Parse(Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_ResolveDate))).Date()
                            _sItemResolveDate = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_ResolveDate)).Trim
                        Else
                            _sItemResolveDate = Nothing
                        End If
                        _sItemStatus = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Prob_Status)).Trim

                        For j As Integer = i + 1 To C1ConsolidatedList.Rows.Count - 1
                            _sItemComplaint1 = Convert.ToString(C1ConsolidatedList.Rows(j)(Col_Prob_Complaint)).Trim
                            _sItemSnomedCode1 = Convert.ToString(C1ConsolidatedList.Rows(j)(Col_Prob_sConceptID)).Trim
                            _sItemStatus1 = Convert.ToString(C1ConsolidatedList.Rows(j)(Col_Prob_Status)).Trim
                            If Convert.ToString(C1ConsolidatedList.Rows(j)(Col_Prob_dtDOS)).Trim <> "" Then
                                _sItemRemoveDos = Date.Parse(C1ConsolidatedList.Rows(j)(Col_Prob_dtDOS)).Date()
                            Else
                                _sItemRemoveDos = Nothing
                            End If
                            If Convert.ToString(C1ConsolidatedList.Rows(j)(Col_Prob_ResolveDate)).Trim <> "" Then
                                _sItemRemoveResolveDate = Date.Parse(C1ConsolidatedList.Rows(j)(Col_Prob_ResolveDate)).Date()
                            Else
                                _sItemRemoveResolveDate = Nothing
                            End If

                            If (_sItemComplaint.ToUpper = _sItemComplaint1.ToUpper) And (_sItemSnomedCode.ToUpper = _sItemSnomedCode1.ToUpper) And (_sItemDos = _sItemRemoveDos) And (_sItemResolveDate = _sItemRemoveResolveDate) And (_sItemStatus.ToUpper = _sItemStatus1.ToUpper) Then
                                'If (_sItemComplaint = _sItemComplaint1) Then
                                C1ConsolidatedList.Rows(j).Visible = False


                            End If

                        Next


                    Case "Med Allergy"

                        _sItemRxNormCode = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_sTranID1)).Trim
                        _sItemAllergy = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_sHistoryItem)).Trim
                        _sItemReaction = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_sReaction)).Trim
                        _sItemStatus = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_Status)).Trim
                        For j As Integer = i + 1 To C1ConsolidatedList.Rows.Count - 1
                            _sItemRemoveRxNormCode = Convert.ToString(C1ConsolidatedList.GetData(j, Col_All_sTranID1)).Trim
                            _sItemRemoveAllergy = Convert.ToString(C1ConsolidatedList.GetData(j, Col_All_sHistoryItem)).Trim
                            _sItemRemoveReaction = Convert.ToString(C1ConsolidatedList.GetData(j, Col_All_sReaction)).Trim
                            _sItemStatus1 = Convert.ToString(C1ConsolidatedList.GetData(j, Col_All_Status)).Trim
                            '  If _sItemRxNormCode <> "" And _sItemRemoveRxNormCode <> "" And _sItemAllergy <> "" And _sItemRemoveAllergy <> "" Then
                            If (_sItemRxNormCode.ToUpper = _sItemRemoveRxNormCode.ToUpper) And (_sItemAllergy.ToUpper = _sItemRemoveAllergy.ToUpper) And (_sItemReaction.ToUpper = _sItemRemoveReaction.ToUpper) And (_sItemStatus.ToUpper = _sItemStatus1.ToUpper) Then
                                C1ConsolidatedList.Rows(j).Visible = False


                            End If
                            ' End If
                        Next


                End Select
            Next




        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            C1ConsolidatedList.Redraw = True

        End Try
    End Sub

    Private Sub HighLightSimilarDuplicates()
        C1ConsolidatedList.Redraw = False

        Try

            Dim i As Int32 = 1


            Dim _sItemSnomedCode As String = ""
            Dim _sItemComplaint As String = ""
            Dim _sItemRxNormCode As String = ""
            Dim _sItemAllergy As String = ""
            Dim _sItemDrugName As String = ""
            Dim _sItemFrequency As String = ""
            Dim _sItemDosage As String = ""
            Dim _sItemNDCCode As String = ""

            If Not IsNothing(C1ConsolidatedList.DataSource) Then


                Dim dtConsolidate As DataTable
                dtConsolidate = C1ConsolidatedList.DataSource

                For k As Integer = dtConsolidate.Rows.Count - 1 To 0 Step -1
                    If C1ConsolidatedList.Rows(k + 1).IsVisible Then
                    Else

                        dtConsolidate.Rows.RemoveAt(k)
                    End If
                Next
                C1ConsolidatedList.DataSource = dtConsolidate

                For i = 1 To C1ConsolidatedList.Rows.Count - 1

                    Select Case cmbListType.Text
                        Case "Medication"
                            _sItemDrugName = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_Medication))
                            _sItemFrequency = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_Frequency))
                            _sItemDosage = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_sDosage))
                            _sItemNDCCode = Convert.ToString(C1ConsolidatedList.Rows(i)(Col_Med_sNDCCode))

                            If IsDuplicateExisits("", "", _sItemNDCCode, _sItemDrugName) Then
                                C1ConsolidatedList.SetCellImage(i, COL_Med_Dup, ImgGrid.Images(1))
                                C1ConsolidatedList.SetData(i, COL_Med_Dup, "  Similar")
                            Else
                                C1ConsolidatedList.SetData(i, COL_Med_Dup, Nothing)
                                C1ConsolidatedList.SetCellImage(i, COL_Med_Dup, Nothing)
                            End If


                        Case "Problem"



                            _sItemComplaint = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Prob_Complaint))
                            _sItemSnomedCode = Convert.ToString(C1ConsolidatedList.GetData(i, Col_Prob_sConceptID))
                            If IsDuplicateExisits(_sItemSnomedCode, "", "", _sItemComplaint) Then
                                C1ConsolidatedList.SetCellImage(i, COL_Prob_Dup, ImgGrid.Images(1))
                                C1ConsolidatedList.SetData(i, COL_Prob_Dup, "  Similar")
                            Else
                                C1ConsolidatedList.SetData(i, COL_Prob_Dup, Nothing)
                                C1ConsolidatedList.SetCellImage(i, COL_Prob_Dup, Nothing)
                            End If

                        Case "Med Allergy"


                            ''''
                            'Dim strReaction As String
                            'Dim strActive As String

                            'Dim arr() As String 'Srting Array
                            'Dim strRection_Status As String = ""
                            'strRection_Status = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_sReaction))
                            'arr = Split(strRection_Status, "|")
                            'If arr.Length = 2 Then
                            '    strReaction = arr.GetValue(0)
                            '    strActive = arr.GetValue(1)
                            'Else
                            '    strReaction = strRection_Status
                            '    strActive = False
                            'End If

                            'Dim arrReaction As String()
                            'arrReaction = strReaction.Split(vbNewLine)

                            'C1ConsolidatedList.SetData(i, Col_All_sReaction, strReaction)


                            'C1ConsolidatedList.Rows(i).Height = C1ConsolidatedList.Rows.DefaultSize * arrReaction.Length - 1
                            Dim strReaction As String

                            Dim strRection_Status As String = ""
                            strReaction = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_sReaction))

                            Dim arrReaction As String()
                            arrReaction = strReaction.Split(vbNewLine)

                            C1ConsolidatedList.Rows(i).Height = C1ConsolidatedList.Rows.DefaultSize * arrReaction.Length - 1

                            _sItemRxNormCode = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_sTranID1))
                            _sItemAllergy = Convert.ToString(C1ConsolidatedList.GetData(i, Col_All_sHistoryItem))
                            If IsDuplicateExisits("", "", _sItemRxNormCode, _sItemAllergy) Then
                                C1ConsolidatedList.SetCellImage(i, COL_All_Dup, ImgGrid.Images(1))
                                C1ConsolidatedList.SetData(i, COL_All_Dup, "  Similar")
                            Else
                                C1ConsolidatedList.SetData(i, COL_All_Dup, Nothing)
                                C1ConsolidatedList.SetCellImage(i, COL_All_Dup, Nothing)
                            End If
                    End Select

                Next

            End If
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally

            C1ConsolidatedList.Redraw = True

        End Try
    End Sub

    Private Function IsDuplicateExisits(ByVal _sItemSnomedCode As String, ByVal _sItemDiagnosis As String, ByVal _sItemNDCCode As String, ByVal _sItemDrugName As String) As Boolean
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try

            dt = C1ConsolidatedList.DataSource
            _dtTemp = dt.Copy()
            dv = _dtTemp.DefaultView
            If cmbListType.Text = "Medication" Then
                If _sItemNDCCode <> "" And _sItemDrugName <> "" Then
                    dv.RowFilter = "NDCCode = '" & _sItemNDCCode & "' OR MedicationName ='" & _sItemDrugName & "'"
                ElseIf _sItemNDCCode <> "" Then
                    dv.RowFilter = "NDCCode = '" & _sItemNDCCode & "'"
                ElseIf _sItemDrugName <> "" Then
                    dv.RowFilter = "MedicationName ='" & _sItemDrugName & "'"
                Else
                    Return False
                End If

            ElseIf cmbListType.Text = "Problem" Then
                If _sItemDrugName <> "" And _sItemSnomedCode <> "" Then
                    dv.RowFilter = "Complaint = '" & _sItemDrugName & "' OR [Snomed CT ID] ='" & _sItemSnomedCode & "'"
                ElseIf _sItemDrugName <> "" Then
                    dv.RowFilter = "Complaint = '" & _sItemDrugName & "'"
                ElseIf _sItemSnomedCode <> "" Then
                    dv.RowFilter = "[Snomed CT ID] ='" & _sItemSnomedCode & "'"
                Else
                    Return False
                End If

            ElseIf cmbListType.Text = "Med Allergy" Then
                If _sItemNDCCode <> "" And _sItemDrugName <> "" Then
                    dv.RowFilter = "NDCCode ='" & _sItemNDCCode & "' OR [History] ='" & _sItemDrugName & "'"
                ElseIf _sItemNDCCode <> "" Then
                    dv.RowFilter = "NDCCode ='" & _sItemNDCCode & "'"
                ElseIf _sItemDrugName <> "" Then
                    dv.RowFilter = "[History] ='" & _sItemDrugName & "'"
                Else
                    Return False
                End If

            End If

            If dv.Count >= 2 Then
                Return True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(_dtTemp) Then
                _dtTemp.Dispose()
                _dtTemp = Nothing
            End If

        End Try

    End Function

    Private Sub C1ConsolidatedList_CellChecked(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1ConsolidatedList.CellChecked
        Dim Result As Integer
        Dim rngRows As New C1.Win.C1FlexGrid.CellRange
        Try
            ' gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnRemovePatientDataSetting = True
            gloLibCCDGeneral.blnRemovePatientDataSetting = True


            If cmbListType.Text = "Medication" Then
                If Convert.ToString(C1ConsolidatedList.GetData(e.Row, Col_Med_Source)) = "Current" Then
                    If C1ConsolidatedList.GetCellCheck(e.Row, COL_Med_Select) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        If gloLibCCDGeneral.blnRemovePatientDataSetting Then
                            Result = MessageBox.Show("This Record will be removed from the Patient’s Chart. Are you sure you want to Continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            If Result = MsgBoxResult.Yes Then

                            ElseIf Result = MsgBoxResult.No Then
                                C1ConsolidatedList.SetCellCheck(e.Row, COL_Med_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Patient Records may not be removed during Clinical Reconciliation. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                            C1ConsolidatedList.SetCellCheck(e.Row, COL_Med_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            Exit Sub
                        End If

                    End If

                End If
            ElseIf cmbListType.Text = "Problem" Then
                If Convert.ToString(C1ConsolidatedList.GetData(e.Row, Col_Prob_Source)) = "Current" Then
                    If C1ConsolidatedList.GetCellCheck(e.Row, COL_Prob_Select) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        If gloLibCCDGeneral.blnRemovePatientDataSetting Then
                            Result = MessageBox.Show("This Record will be removed from the Patient’s Chart. Are you sure you want to Continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            If Result = MsgBoxResult.Yes Then

                            ElseIf Result = MsgBoxResult.No Then
                                C1ConsolidatedList.SetCellCheck(e.Row, COL_Prob_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Patient Records may not be removed during Clinical Reconciliation. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                            C1ConsolidatedList.SetCellCheck(e.Row, COL_Prob_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            Exit Sub
                        End If

                    End If
                End If

            ElseIf cmbListType.Text = "Med Allergy" Then
                If Convert.ToString(C1ConsolidatedList.GetData(e.Row, Col_All_sSourceName)) = "Current" Then
                    If C1ConsolidatedList.GetCellCheck(e.Row, COL_All_Select) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        If gloLibCCDGeneral.blnRemovePatientDataSetting Then
                            Result = MessageBox.Show("This Record will be removed from the Patient’s Chart. Are you sure you want to Continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            If Result = MsgBoxResult.Yes Then

                            ElseIf Result = MsgBoxResult.No Then
                                C1ConsolidatedList.SetCellCheck(e.Row, COL_All_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Patient Records may not be removed during Clinical Reconciliation. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                            C1ConsolidatedList.SetCellCheck(e.Row, COL_All_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                            Exit Sub
                        End If

                    End If
                End If
            End If

            If C1ConsolidatedList.GetCellCheck(e.Row, e.Col) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                rngRows = C1ConsolidatedList.GetCellRange(e.Row, 0, e.Row, c1Reconciliation.Cols.Count - 1)
                rngRows.Style = Nothing
                rngRows.Style = cStyle_StrikoutFont
            Else
                rngRows = C1ConsolidatedList.GetCellRange(e.Row, 0, e.Row, c1Reconciliation.Cols.Count - 1)
                rngRows.Style = Nothing
                rngRows.Style = cStyle_NormalFont
            End If

        Catch ex As Exception

        End Try
        rngRows = Nothing

    End Sub

#End Region

    Private Sub chkHideCurrentData_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHideCurrentData.CheckedChanged
        '   HidePatientsCurrentData()
        SaveReconcileColumnWidth()
        DefaultReconcialationList()
        FillConsolidatedList()
        RemoveExactDuplicates()
        HighLightSimilarDuplicates()

        HighLightExactDuplicates()

    End Sub

#Region "Save Column Width"
    Private Sub SaveReconcileColumnWidth()
        Dim _gloReconciliation As New gloReconciliation
        If cmbListType.Text = "Medication" Then
            c1Reconciliation.Name = "Meds_ReconcileList"
            _gloReconciliation.SaveColumnWidth(False, c1Reconciliation, _UserID)
            C1ConsolidatedList.Name = "Meds_ConsolidatedList"
            _gloReconciliation.SaveColumnWidth(False, C1ConsolidatedList, _UserID)
        ElseIf cmbListType.Text = "Problem" Then
            c1Reconciliation.Name = "Probs_ReconcileList"
            _gloReconciliation.SaveColumnWidth(False, c1Reconciliation, _UserID)
            C1ConsolidatedList.Name = "Probs_ConsolidatedList"
            _gloReconciliation.SaveColumnWidth(False, C1ConsolidatedList, _UserID)
        ElseIf cmbListType.Text = "Med Allergy" Then
            c1Reconciliation.Name = "Allergy_ReconcileList"
            _gloReconciliation.SaveColumnWidth(False, c1Reconciliation, _UserID)
            C1ConsolidatedList.Name = "Allergy_ConsolidatedList"
            _gloReconciliation.SaveColumnWidth(False, C1ConsolidatedList, _UserID)
        End If
        If Not IsNothing(_gloReconciliation) Then
            _gloReconciliation.Dispose()
            _gloReconciliation = Nothing
        End If
    End Sub
#End Region
   
End Class
