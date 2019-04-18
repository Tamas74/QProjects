Imports gloUserControlLibrary
Imports System.Windows.Forms
Imports gloSettings
Imports gloCCDLibrary
Imports System.Data.SqlClient

Public Class frmHxForecastReconcileList

#Region "Variable Declaration"
    Dim _nPatientID As Long
    Dim _ModuleName As String = ""
    Dim _IsFormLoading As Boolean = False
    Dim _UserName As String = String.Empty
    Dim _UserID As Long = 0
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip
    Dim _MedicationVisitID As Long = 0
    Dim _isForecastReconciled As Boolean = False

    Dim _dtDuplicate As New DataTable


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


    '------------Immunization History Coloumns----------

    Private Const COL_ImmHx_Select As Integer = 0
    Private Const COL_ImmHx_Dup As Integer = 1
    Private Const COL_ImmHx_TransactionId As Integer = 2
    Private Const COL_ImmHx_PatientId As Integer = 3
    Private Const COL_ImmHx_Source As Integer = 4
    Private Const COL_ImmHx_TransactionDate As Integer = 5
    Private Const COL_ImmHx_Status As Integer = 6
    Private Const COL_ImmHx_TradeName As Integer = 7
    Private Const COL_ImmHx_Vaccine As Integer = 8
    Private Const COL_ImmHx_Manufacturer As Integer = 9
    Private Const COL_ImmHx_LotNumber As Integer = 10
    Private Const COL_ImmHx_DosageGiven As Integer = 11
    Private Const COL_ImmHx_AmountGiven As Integer = 12
    Private Const COL_ImmHx_Unit As Integer = 13
    Private Const COL_ImmHx_Site As Integer = 14
    Private Const COL_ImmHx_Route As Integer = 15
    Private Const COL_ImmHx_Comments As Integer = 16

    '------------Immunization Forecast Coloumns----------

    Private Const COL_ImmForecast_Select As Integer = 0
    Private Const COL_ImmForecast_Dup As Integer = 1
    Private Const COL_ImmForecast_ForecastId As Integer = 2
    Private Const COL_ImmForecast_AdministrationDate As Integer = 3
    Private Const COL_ImmForecast_PatientID As Integer = 4
    Private Const COL_ImmForecast_Source As Integer = 5
    Private Const COL_ImmForecast_Vaccine As Integer = 6
    Private Const COL_ImmForecast_ImmunizationScheduleUsed As Integer = 7
    Private Const COL_ImmForecast_VaccinationDueDate As Integer = 8
    Private Const COL_ImmForecast_EarliestDateToGive As Integer = 9

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

        RemoveHandler chkSelectAll.CheckedChanged, AddressOf chkSelectAll_CheckedChanged
        chkSelectAll.Checked = True
        AddHandler chkSelectAll.CheckedChanged, AddressOf chkSelectAll_CheckedChanged
        Set_PatientDetailStrip()
        CreateC1Styles()
        c1Reconciliation.AllowSorting = False
        C1ConsolidatedList.AllowSorting = True

        cmbListType.Items.Add("Immunization History")
        cmbListType.Items.Add("Immunization Forecast")
        cmbListType.Text = "Immunization History"

        _dtDuplicate.Columns.Add("Select")
        _dtDuplicate.Columns.Add("Source")
        _dtDuplicate.Columns.Add("TransactionId")

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


        With gloUC_PatientStrip1


            .ShowDetail(_nPatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.None)

            .DTPEnabled = False
        End With

        '01-Mar-16 Aniket: Resolving Bug #93843: gloEMR->View Reconcilliation list-> patient banner is displaying above toolstrip
        Me.Controls.Add(gloUC_PatientStrip1)

        gloUC_PatientStrip1.Dock = DockStyle.Top
        gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
        gloUC_PatientStrip1.BringToFront()

        pnlMain.BringToFront()


    End Sub
#End Region

#Region "Design Grid and C1 Styles"

    Private Sub DesignReconciliationGrid()

        c1Reconciliation.Redraw = False
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(GetConnectionString())
        Try

            c1Reconciliation.AllowEditing = False
            c1Reconciliation.AllowDragging = False

            If cmbListType.Text = "Immunization History" Then
                c1Reconciliation.Cols.Count = 17
                c1Reconciliation.Cols(COL_ImmHx_Dup).AllowResizing = False
                c1Reconciliation.Cols(COL_ImmHx_Dup).Caption = "Dup"
                c1Reconciliation.Cols(COL_ImmHx_Dup).AllowEditing = False

                c1Reconciliation.Cols(COL_ImmHx_TransactionId).Caption = "Transaction Id"
                c1Reconciliation.Cols(COL_ImmHx_PatientId).Caption = "Patient Id"
                c1Reconciliation.Cols(COL_ImmHx_Source).Caption = "Source"
                c1Reconciliation.Cols(COL_ImmHx_TransactionDate).Caption = "Transaction Date"
                c1Reconciliation.Cols(COL_ImmHx_Status).Caption = "Status"
                c1Reconciliation.Cols(COL_ImmHx_TradeName).Caption = "Trade Name"
                c1Reconciliation.Cols(COL_ImmHx_Vaccine).Caption = "Vaccine"
                c1Reconciliation.Cols(COL_ImmHx_Manufacturer).Caption = "Manufacturer"
                c1Reconciliation.Cols(COL_ImmHx_LotNumber).Caption = "Lot Number"
                c1Reconciliation.Cols(COL_ImmHx_DosageGiven).Caption = "Dosage Given"
                c1Reconciliation.Cols(COL_ImmHx_AmountGiven).Caption = "Amount Given"
                c1Reconciliation.Cols(COL_ImmHx_Unit).Caption = "Unit"
                c1Reconciliation.Cols(COL_ImmHx_Site).Caption = "Site"
                c1Reconciliation.Cols(COL_ImmHx_Route).Caption = "Route"
                c1Reconciliation.Cols(COL_ImmHx_Comments).Caption = "Comments"

                c1Reconciliation.Cols(COL_ImmHx_TransactionId).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_PatientId).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Source).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_TransactionDate).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Status).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_TradeName).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Vaccine).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Manufacturer).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_LotNumber).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_DosageGiven).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_AmountGiven).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Unit).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Site).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Route).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmHx_Comments).AllowEditing = False

                c1Reconciliation.Cols(COL_ImmHx_TransactionId).Visible = False
                c1Reconciliation.Cols(COL_ImmHx_PatientId).Visible = False

                c1Reconciliation.Name = "Immunization_History_ReconcileList"

                c1Reconciliation.Cols(COL_ImmHx_Select).Width = 0
                c1Reconciliation.Cols(COL_ImmHx_Dup).Width = 30
                c1Reconciliation.Cols(COL_ImmHx_TransactionId).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_PatientId).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_Source).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_TransactionDate).Width = 120
                c1Reconciliation.Cols(COL_ImmHx_Status).Width = 150
                c1Reconciliation.Cols(COL_ImmHx_TradeName).Width = 160
                c1Reconciliation.Cols(COL_ImmHx_Vaccine).Width = 300
                c1Reconciliation.Cols(COL_ImmHx_Manufacturer).Width = 220
                c1Reconciliation.Cols(COL_ImmHx_LotNumber).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_DosageGiven).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_AmountGiven).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_Unit).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_Site).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_Route).Width = 100
                c1Reconciliation.Cols(COL_ImmHx_Comments).Width = 100

            End If

            If cmbListType.Text = "Immunization Forecast" Then
                c1Reconciliation.Cols.Count = 10
                c1Reconciliation.Cols(COL_ImmForecast_Dup).AllowResizing = False
                c1Reconciliation.Cols(COL_ImmForecast_Dup).Caption = "Dup"
                c1Reconciliation.Cols(COL_ImmForecast_Dup).AllowEditing = False

                c1Reconciliation.Cols(COL_ImmForecast_ForecastId).Caption = "Forecast Id"
                c1Reconciliation.Cols(COL_ImmForecast_AdministrationDate).Caption = "Administration Date"
                c1Reconciliation.Cols(COL_ImmForecast_PatientID).Caption = "Patient ID"
                c1Reconciliation.Cols(COL_ImmForecast_Source).Caption = "Source"
                c1Reconciliation.Cols(COL_ImmForecast_Vaccine).Caption = "Vaccine"
                c1Reconciliation.Cols(COL_ImmForecast_ImmunizationScheduleUsed).Caption = "Immunization Schedule Used"
                c1Reconciliation.Cols(COL_ImmForecast_VaccinationDueDate).Caption = "Vaccination Due Date"
                c1Reconciliation.Cols(COL_ImmForecast_EarliestDateToGive).Caption = "Earliest Date To Give"

                c1Reconciliation.Cols(COL_ImmForecast_ForecastId).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmForecast_AdministrationDate).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmForecast_PatientID).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmForecast_Source).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmForecast_Vaccine).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmForecast_ImmunizationScheduleUsed).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmForecast_VaccinationDueDate).AllowEditing = False
                c1Reconciliation.Cols(COL_ImmForecast_EarliestDateToGive).AllowEditing = False

                c1Reconciliation.Cols(COL_ImmForecast_ForecastId).Visible = False
                c1Reconciliation.Cols(COL_ImmForecast_PatientID).Visible = False

                c1Reconciliation.Name = "Immunization_Forecast_ReconcileList"

                c1Reconciliation.Cols(COL_ImmForecast_ForecastId).Width = 0
                c1Reconciliation.Cols(COL_ImmForecast_Select).Width = 0
                c1Reconciliation.Cols(COL_ImmForecast_Dup).Width = 30
                c1Reconciliation.Cols(COL_ImmForecast_AdministrationDate).Width = 120
                c1Reconciliation.Cols(COL_ImmForecast_PatientID).Width = 100
                c1Reconciliation.Cols(COL_ImmForecast_Source).Width = 100
                c1Reconciliation.Cols(COL_ImmForecast_Vaccine).Width = 300
                c1Reconciliation.Cols(COL_ImmForecast_ImmunizationScheduleUsed).Width = 220
                c1Reconciliation.Cols(COL_ImmForecast_VaccinationDueDate).Width = 120
                c1Reconciliation.Cols(COL_ImmForecast_EarliestDateToGive).Width = 120

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
        Dim ogloSettings As gloSettings.GeneralSettings = New GeneralSettings(GetConnectionString())
        Try
            C1ConsolidatedList.AllowDragging = False

            If cmbListType.Text = "Immunization History" Then
                C1ConsolidatedList.Cols.Count = 17

                C1ConsolidatedList.Cols(COL_ImmHx_Select).AllowEditing = True
                C1ConsolidatedList.Cols(COL_ImmHx_Select).Caption = "Select"
                C1ConsolidatedList.Cols(COL_ImmHx_Select).DataType = GetType(Boolean)

                C1ConsolidatedList.Cols(COL_ImmHx_Dup).AllowResizing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Dup).Caption = "Dup"
                C1ConsolidatedList.Cols(COL_ImmHx_Dup).AllowEditing = False

                C1ConsolidatedList.Cols(COL_ImmHx_TransactionId).Caption = "Transaction Id"
                C1ConsolidatedList.Cols(COL_ImmHx_PatientId).Caption = "Patient Id"
                C1ConsolidatedList.Cols(COL_ImmHx_Source).Caption = "Source"
                C1ConsolidatedList.Cols(COL_ImmHx_TransactionDate).Caption = "Transaction Date"
                C1ConsolidatedList.Cols(COL_ImmHx_Status).Caption = "Status"
                C1ConsolidatedList.Cols(COL_ImmHx_TradeName).Caption = "Trade Name"
                C1ConsolidatedList.Cols(COL_ImmHx_Vaccine).Caption = "Vaccine"
                C1ConsolidatedList.Cols(COL_ImmHx_Manufacturer).Caption = "Manufacturer"
                C1ConsolidatedList.Cols(COL_ImmHx_LotNumber).Caption = "Lot Number"
                C1ConsolidatedList.Cols(COL_ImmHx_DosageGiven).Caption = "Dosage Given"
                C1ConsolidatedList.Cols(COL_ImmHx_AmountGiven).Caption = "Amount Given"
                C1ConsolidatedList.Cols(COL_ImmHx_Unit).Caption = "Unit"
                C1ConsolidatedList.Cols(COL_ImmHx_Site).Caption = "Site"
                C1ConsolidatedList.Cols(COL_ImmHx_Route).Caption = "Route"
                C1ConsolidatedList.Cols(COL_ImmHx_Comments).Caption = "Comments"

                C1ConsolidatedList.Cols(COL_ImmHx_TransactionId).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_PatientId).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Source).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_TransactionDate).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Status).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_TradeName).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Vaccine).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Manufacturer).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_LotNumber).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_DosageGiven).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_AmountGiven).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Unit).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Site).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Route).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmHx_Comments).AllowEditing = False

                C1ConsolidatedList.Cols(COL_ImmHx_TransactionId).Visible = False
                C1ConsolidatedList.Cols(COL_ImmHx_PatientId).Visible = False

                C1ConsolidatedList.Name = "Immunization_History_Consolidated"

                If (ogloSettings.LoadGridColumnWidth(C1ConsolidatedList, ModuleOfGridColumn.Reconciliation, _UserID) = False) Then
                    C1ConsolidatedList.Cols(COL_ImmHx_Select).Width = 50
                    C1ConsolidatedList.Cols(COL_ImmHx_Dup).Width = 30
                    C1ConsolidatedList.Cols(COL_ImmHx_TransactionId).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_PatientId).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_Source).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_TransactionDate).Width = 120
                    C1ConsolidatedList.Cols(COL_ImmHx_Status).Width = 150
                    C1ConsolidatedList.Cols(COL_ImmHx_TradeName).Width = 160
                    C1ConsolidatedList.Cols(COL_ImmHx_Vaccine).Width = 300
                    C1ConsolidatedList.Cols(COL_ImmHx_Manufacturer).Width = 220
                    C1ConsolidatedList.Cols(COL_ImmHx_LotNumber).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_DosageGiven).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_AmountGiven).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_Unit).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_Site).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_Route).Width = 100
                    C1ConsolidatedList.Cols(COL_ImmHx_Comments).Width = 100
                End If

            End If

            If cmbListType.Text = "Immunization Forecast" Then
                C1ConsolidatedList.Cols.Count = 10

                C1ConsolidatedList.Cols(COL_ImmForecast_Select).AllowEditing = True
                C1ConsolidatedList.Cols(COL_ImmForecast_Select).Caption = "Select"
                C1ConsolidatedList.Cols(COL_ImmForecast_Select).DataType = GetType(Boolean)

                C1ConsolidatedList.Cols(COL_ImmForecast_Dup).AllowResizing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_Dup).Caption = "Dup"
                C1ConsolidatedList.Cols(COL_ImmForecast_Dup).AllowEditing = False

                C1ConsolidatedList.Cols(COL_ImmForecast_ForecastId).Caption = "Forecast Id"
                C1ConsolidatedList.Cols(COL_ImmForecast_AdministrationDate).Caption = "Administration Date"
                C1ConsolidatedList.Cols(COL_ImmForecast_PatientID).Caption = "Patient ID"
                C1ConsolidatedList.Cols(COL_ImmForecast_Source).Caption = "Source"
                C1ConsolidatedList.Cols(COL_ImmForecast_Vaccine).Caption = "Vaccine"
                C1ConsolidatedList.Cols(COL_ImmForecast_ImmunizationScheduleUsed).Caption = "Immunization Schedule Used"
                C1ConsolidatedList.Cols(COL_ImmForecast_VaccinationDueDate).Caption = "Vaccination Due Date"
                C1ConsolidatedList.Cols(COL_ImmForecast_EarliestDateToGive).Caption = "Earliest Date To Give"

                C1ConsolidatedList.Cols(COL_ImmForecast_ForecastId).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_AdministrationDate).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_PatientID).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_Source).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_Vaccine).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_ImmunizationScheduleUsed).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_VaccinationDueDate).AllowEditing = False
                C1ConsolidatedList.Cols(COL_ImmForecast_EarliestDateToGive).AllowEditing = False

                C1ConsolidatedList.Cols(COL_ImmForecast_ForecastId).Visible = False
                C1ConsolidatedList.Cols(COL_ImmForecast_PatientID).Visible = False

                C1ConsolidatedList.Name = "Immunization_Forecast_Consolidated"

                C1ConsolidatedList.Cols(COL_ImmForecast_Select).Width = 50
                C1ConsolidatedList.Cols(COL_ImmForecast_Dup).Width = 30
                C1ConsolidatedList.Cols(COL_ImmForecast_AdministrationDate).Width = 120
                C1ConsolidatedList.Cols(COL_ImmForecast_PatientID).Width = 100
                C1ConsolidatedList.Cols(COL_ImmForecast_Source).Width = 100
                C1ConsolidatedList.Cols(COL_ImmForecast_Vaccine).Width = 300
                C1ConsolidatedList.Cols(COL_ImmForecast_ImmunizationScheduleUsed).Width = 220
                C1ConsolidatedList.Cols(COL_ImmForecast_VaccinationDueDate).Width = 120
                C1ConsolidatedList.Cols(COL_ImmForecast_EarliestDateToGive).Width = 120

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

        'cStyle_Blue = c1Reconciliation.Styles.Add("StyleBlue")
        Try
            If (c1Reconciliation.Styles.Contains("StyleBlue")) Then
                cStyle_Blue = c1Reconciliation.Styles("StyleBlue")
            Else
                cStyle_Blue = c1Reconciliation.Styles.Add("StyleBlue")
                cStyle_Blue.ForeColor = Drawing.Color.Black
                cStyle_Blue.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
                cStyle_Blue.BackColor = System.Drawing.Color.FromArgb(215, 226, 253)
            End If
        Catch ex As Exception
            cStyle_Blue = c1Reconciliation.Styles.Add("StyleBlue")
            cStyle_Blue.ForeColor = Drawing.Color.Black
            cStyle_Blue.Font = gloGlobal.clsgloFont.gFont ' New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
            cStyle_Blue.BackColor = System.Drawing.Color.FromArgb(215, 226, 253)
        End Try



        'cStyle_Pink = c1Reconciliation.Styles.Add("StylePink")
        Try
            If (c1Reconciliation.Styles.Contains("StylePink")) Then
                cStyle_Pink = c1Reconciliation.Styles("StylePink")
            Else
                cStyle_Pink = c1Reconciliation.Styles.Add("StylePink")
                cStyle_Pink.ForeColor = Drawing.Color.Black
                cStyle_Pink.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
                cStyle_Pink.BackColor = System.Drawing.Color.White
            End If
        Catch ex As Exception
            cStyle_Pink = c1Reconciliation.Styles.Add("StylePink")
            cStyle_Pink.ForeColor = Drawing.Color.Black
            cStyle_Pink.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
            cStyle_Pink.BackColor = System.Drawing.Color.White
        End Try



        'cStyle_StrikoutFont = C1ConsolidatedList.Styles.Add("StyleStrikout")
        Try
            If (c1Reconciliation.Styles.Contains("StyleStrikout")) Then
                cStyle_StrikoutFont = c1Reconciliation.Styles("StyleStrikout")
            Else
                cStyle_StrikoutFont = c1Reconciliation.Styles.Add("StyleStrikout")
                cStyle_StrikoutFont.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT ' New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout)
                cStyle_StrikoutFont.ForeColor = Drawing.Color.DarkGray
            End If
        Catch ex As Exception
            cStyle_StrikoutFont = c1Reconciliation.Styles.Add("StyleStrikout")
            cStyle_StrikoutFont.Font = gloGlobal.clsgloFont.gFont_STRIKEOUT ' New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Strikeout)
            cStyle_StrikoutFont.ForeColor = Drawing.Color.DarkGray
        End Try
        ' cStyle_NormalFont = C1ConsolidatedList.Styles.Add("StyleRegular")
        Try
            If (c1Reconciliation.Styles.Contains("StyleRegular")) Then
                cStyle_NormalFont = c1Reconciliation.Styles("StyleRegular")
            Else
                cStyle_NormalFont = c1Reconciliation.Styles.Add("StyleRegular")
                cStyle_NormalFont.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
            End If
        Catch ex As Exception
            cStyle_NormalFont = c1Reconciliation.Styles.Add("StyleRegular")
            cStyle_NormalFont.Font = gloGlobal.clsgloFont.gFont  'New System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Regular)
        End Try




    End Sub

#End Region

#Region "Fill Grid Data"

    Private Sub FillReconciliationGrid(ByVal nListID As Long, ByVal sListType As String)

        Dim objgloCCDReconcilation As New gloImmunizationReconcilation
        Dim _dtExtractedList As DataTable = Nothing

        Try
            _dtExtractedList = objgloCCDReconcilation.GetExtractedList(nListID, sListType, _nPatientID)

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
        End Try

    End Sub

    Private Sub DefaultReconcialationList()

        Try
            c1Reconciliation.DataSource = Nothing
            c1Reconciliation.Rows.Count = 1

            FillReconciliationGrid(0, cmbListType.Text)

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
                If cmbListType.Text = "Immunization History" Or cmbListType.Text = "Immunization Forecast" Then
                    dt = c1Reconciliation.DataSource
                    _dtTemp = dt.Copy()
                    dv = _dtTemp.DefaultView
                    dv.RowFilter = "Source <> 'Current'"
                    dtCurrent = dv.ToTable.Copy()
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
                ' Me.Close()
                gloWord.WordDialogBoxBackgroundCloser.Close(Me)
        End Select

    End Sub
#End Region

#Region "Events"

    Private Sub cmbListType_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbListType.Enter
        SaveReconcileColumnWidth()

    End Sub


    Private Sub cmbListType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbListType.SelectedIndexChanged

        Try

            If _IsFormLoading = False Then
                ''the checkbox is checked coz all the checkboxes are checked on this event
                RemoveHandler chkSelectAll.CheckedChanged, AddressOf chkSelectAll_CheckedChanged
                chkSelectAll.Checked = True
                AddHandler chkSelectAll.CheckedChanged, AddressOf chkSelectAll_CheckedChanged
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

#End Region

#Region "Reconciliation List HighLight Exact Duplicates"

    Private Function IsExactImmHxExisits(ByVal _sVaccine As String, ByVal _DateGiven As String) As Boolean
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try

            dt = c1Reconciliation.DataSource
            _dtTemp = dt.Copy()
            dv = _dtTemp.DefaultView
            If cmbListType.Text = "Immunization History" Then

                dv.RowFilter = "Vaccine = '" & _sVaccine.Trim() & "' AND TransactionDate ='" & _DateGiven.Trim() & "'"

            ElseIf cmbListType.Text = "Immunization Forecast" Then
                dv.RowFilter = "Vaccine = '" & _sVaccine.Trim() & "' AND VaccinationDueDate ='" & _DateGiven.Trim() & "'"
            End If

            If dv.Count >= 2 Then
                Return True
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            Return Nothing
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

            Dim _sVaccine As String = ""
            Dim _DateGiven As String = ""

            For i = 1 To c1Reconciliation.Rows.Count - 1
                Select Case cmbListType.Text
                    Case "Immunization History"
                        _sVaccine = Convert.ToString(c1Reconciliation.GetData(i, COL_ImmHx_Vaccine))
                        _DateGiven = Convert.ToString(c1Reconciliation.GetData(i, COL_ImmHx_TransactionDate))

                        If IsExactImmHxExisits(_sVaccine, _DateGiven) Then
                            c1Reconciliation.SetCellImage(i, COL_ImmHx_Dup, ImgGrid.Images(2))
                            c1Reconciliation.SetData(i, COL_ImmHx_Dup, "  Duplicate")
                        Else
                            c1Reconciliation.SetData(i, COL_ImmHx_Dup, Nothing)
                            c1Reconciliation.SetCellImage(i, COL_ImmHx_Dup, Nothing)
                        End If
                    Case "Immunization Forecast"
                        _sVaccine = Convert.ToString(c1Reconciliation.GetData(i, COL_ImmForecast_Vaccine))
                        _DateGiven = Convert.ToString(c1Reconciliation.GetData(i, COL_ImmForecast_VaccinationDueDate))

                        If IsExactImmHxExisits(_sVaccine, _DateGiven) Then
                            c1Reconciliation.SetCellImage(i, COL_ImmForecast_Dup, ImgGrid.Images(2))
                            c1Reconciliation.SetData(i, COL_ImmForecast_Dup, "  Duplicate")
                        Else
                            c1Reconciliation.SetData(i, COL_ImmForecast_Dup, Nothing)
                            c1Reconciliation.SetCellImage(i, COL_ImmForecast_Dup, Nothing)
                        End If
                End Select
            Next

            'AlternateListStyle()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            c1Reconciliation.Redraw = True
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
            Dim objIMReconcilation As New gloImmunizationReconcilation

            Dim dtReconcile As New DataTable
            dtReconcile.Columns.Add("Select")
            dtReconcile.Columns.Add("Source")
            dtReconcile.Columns.Add("TransactionId")

            _isForecastReconciled = False

            If cmbListType.Text = "Immunization History" Then
                For i As Integer = 1 To C1ConsolidatedList.Rows.Count - 1
                    dtReconcile.Rows.Add(C1ConsolidatedList.GetData(i, COL_ImmHx_Select), C1ConsolidatedList.GetData(i, COL_ImmHx_Source), C1ConsolidatedList.GetData(i, COL_ImmHx_TransactionId))
                Next

                For i As Integer = 0 To _dtDuplicate.Rows.Count - 1
                    dtReconcile.Rows.Add(_dtDuplicate.Rows(i)("Select"), _dtDuplicate.Rows(i)("Source"), _dtDuplicate.Rows(i)("TransactionId"))
                Next
            ElseIf cmbListType.Text = "Immunization Forecast" Then
                For i As Integer = 1 To C1ConsolidatedList.Rows.Count - 1
                    dtReconcile.Rows.Add(C1ConsolidatedList.GetData(i, COL_ImmForecast_Select), C1ConsolidatedList.GetData(i, COL_ImmForecast_Source), C1ConsolidatedList.GetData(i, COL_ImmForecast_ForecastId))
                    If dtReconcile.Rows(dtReconcile.Rows.Count - 1)(0) = "1" And dtReconcile.Rows(dtReconcile.Rows.Count - 1)(1) = "Downloaded" Then
                        If _isForecastReconciled = False Then
                            _isForecastReconciled = True
                        End If
                    End If
                Next

                For i As Integer = 0 To _dtDuplicate.Rows.Count - 1
                    dtReconcile.Rows.Add(_dtDuplicate.Rows(i)("Select"), _dtDuplicate.Rows(i)("Source"), _dtDuplicate.Rows(i)("TransactionId"))
                Next
            End If

            objIMReconcilation.AddUpDelReconciledImmunization(dtReconcile, _nPatientID, cmbListType.Text)

            If _isForecastReconciled = True Then
                Dim AssignTo As Long = GetReconciliationTaskSetting()
                GenerateTasks(_nPatientID, "Immunization Forecast Reconciled", "Immunization forecast data is reconciled for the patient. Schedule appointment as per the forecast", 0, 34, AssignTo)
            End If


            DefaultReconcialationList()
            FillConsolidatedList()

            RemoveExactDuplicates()
            HighLightSimilarDuplicates()
            HighLightExactDuplicates()

            MessageBox.Show("Reconcilation Done...", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

            Dim _sVaccine As String()
            Dim _sVaccineCode As String = ""
            Dim _DateGiven As String = ""

            Dim _sVaccine1 As String()
            Dim _sVaccineCode1 As String = ""
            Dim _DateGiven1 As String = ""
            Dim _Source = ""

            For i = 1 To C1ConsolidatedList.Rows.Count - 1
                Select Case cmbListType.Text
                    Case "Immunization History"

                        _sVaccine = Split(Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmHx_Vaccine)).Trim, "-")
                        If _sVaccine.Length > 1 Then
                            _sVaccineCode = Convert.ToString(_sVaccine(0))
                        End If

                        _DateGiven = Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmHx_TransactionDate)).Trim

                        For j As Integer = i + 1 To C1ConsolidatedList.Rows.Count - 1
                            _sVaccine1 = Split(Convert.ToString(C1ConsolidatedList.GetData(j, COL_ImmHx_Vaccine)).Trim, "-")
                            If _sVaccine1.Length > 1 Then
                                _sVaccineCode1 = Convert.ToString(_sVaccine1(0))
                            End If

                            _DateGiven1 = Convert.ToString(C1ConsolidatedList.GetData(j, COL_ImmHx_TransactionDate)).Trim

                            If (_sVaccineCode.Trim() = _sVaccineCode1.Trim()) And (_DateGiven.Trim() = _DateGiven1.Trim()) Then
                                C1ConsolidatedList.Rows(j).Visible = False
                            End If
                        Next
                    Case "Immunization Forecast"
                        _sVaccine = Split(Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmForecast_Vaccine)).Trim, "-")
                        If _sVaccine.Length > 1 Then
                            _sVaccineCode = Convert.ToString(_sVaccine(0))
                        End If

                        _DateGiven = Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmForecast_VaccinationDueDate)).Trim

                        For j As Integer = i + 1 To C1ConsolidatedList.Rows.Count - 1
                            _sVaccine1 = Split(Convert.ToString(C1ConsolidatedList.GetData(j, COL_ImmForecast_Vaccine)).Trim, "-")
                            If _sVaccine1.Length > 1 Then
                                _sVaccineCode1 = Convert.ToString(_sVaccine1(0))
                            End If

                            _DateGiven1 = Convert.ToString(C1ConsolidatedList.GetData(j, COL_ImmForecast_VaccinationDueDate)).Trim

                            If (_sVaccineCode.Trim() = _sVaccineCode1.Trim()) And (_DateGiven.Trim() = _DateGiven1.Trim()) Then
                                C1ConsolidatedList.Rows(j).Visible = False
                            End If

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
        _dtDuplicate.Rows.Clear()
        Try
            Dim i As Int32 = 1

            Dim _sVaccine As String = ""
            Dim _DateGiven As String = ""

            If Not IsNothing(C1ConsolidatedList.DataSource) Then

                Dim dtConsolidate As DataTable
                dtConsolidate = C1ConsolidatedList.DataSource

                For k As Integer = dtConsolidate.Rows.Count - 1 To 0 Step -1
                    If C1ConsolidatedList.Rows(k + 1).IsVisible Then
                    Else
                        If cmbListType.Text = "Immunization History" Then
                            _dtDuplicate.Rows.Add("0", "Duplicate", dtConsolidate.Rows(k)("TransactionId"))
                        ElseIf cmbListType.Text = "Immunization Forecast" Then
                            _dtDuplicate.Rows.Add("0", "Duplicate", dtConsolidate.Rows(k)("ForecastId"))
                        End If


                        dtConsolidate.Rows.RemoveAt(k)
                    End If
                Next
                C1ConsolidatedList.DataSource = dtConsolidate

                For i = 1 To C1ConsolidatedList.Rows.Count - 1
                    Select Case cmbListType.Text
                        Case "Immunization History"
                            _sVaccine = Convert.ToString(C1ConsolidatedList.Rows(i)(COL_ImmHx_Vaccine))
                            _DateGiven = Convert.ToString(C1ConsolidatedList.Rows(i)(COL_ImmHx_TransactionDate))

                            If IsDuplicateExisits("", "", _sVaccine, _DateGiven) Then
                                C1ConsolidatedList.SetCellImage(i, COL_ImmHx_Dup, ImgGrid.Images(1))
                                C1ConsolidatedList.SetData(i, COL_ImmHx_Dup, "  Similar")
                            Else
                                C1ConsolidatedList.SetData(i, COL_ImmHx_Dup, Nothing)
                                C1ConsolidatedList.SetCellImage(i, COL_ImmHx_Dup, Nothing)
                            End If
                        Case "Immunization Forecast"
                            _sVaccine = Convert.ToString(C1ConsolidatedList.Rows(i)(COL_ImmForecast_Vaccine))
                            _DateGiven = Convert.ToString(C1ConsolidatedList.Rows(i)(COL_ImmForecast_VaccinationDueDate))

                            If IsDuplicateExisits("", "", _sVaccine, _DateGiven) Then
                                C1ConsolidatedList.SetCellImage(i, COL_ImmForecast_Dup, ImgGrid.Images(1))
                                C1ConsolidatedList.SetData(i, COL_ImmForecast_Dup, "  Similar")
                            Else
                                C1ConsolidatedList.SetData(i, COL_ImmForecast_Dup, Nothing)
                                C1ConsolidatedList.SetCellImage(i, COL_ImmForecast_Dup, Nothing)
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

    Private Function IsDuplicateExisits(ByVal _sItemSnomedCode As String, ByVal _sItemDiagnosis As String, ByVal _sVaccine As String, ByVal _DateGiven As String) As Boolean
        Dim dv As DataView = Nothing
        Dim _dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try

            dt = C1ConsolidatedList.DataSource
            _dtTemp = dt.Copy()
            dv = _dtTemp.DefaultView

            If cmbListType.Text = "Immunization History" Then
                If _sVaccine <> "" And _DateGiven <> "" Then
                    dv.RowFilter = "Vaccine='" & _sVaccine & "' OR TransactionDate ='" & _DateGiven & "'"
                ElseIf _sVaccine <> "" Then
                    dv.RowFilter = "Vaccine = '" & _sVaccine & "'"
                ElseIf _DateGiven <> "" Then
                    dv.RowFilter = "TransactionDate = '" & _DateGiven & "'"
                Else
                    Return False
                End If

            End If

            If cmbListType.Text = "Immunization Forecast" Then
                If _sVaccine <> "" And _DateGiven <> "" Then
                    dv.RowFilter = "Vaccine='" & _sVaccine & "' OR VaccinationDueDate ='" & _DateGiven & "'"
                ElseIf _sVaccine <> "" Then
                    dv.RowFilter = "Vaccine = '" & _sVaccine & "'"
                ElseIf _DateGiven <> "" Then
                    dv.RowFilter = "VaccinationDueDate = '" & _DateGiven & "'"
                Else
                    Return False
                End If
            End If

            If dv.Count >= 2 Then
                Return True
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
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

    Private Function GetReconciliationTaskSetting() As Long
        Dim cmd As SqlCommand = Nothing
        Dim conn As SqlConnection = Nothing
        Dim strQuery As String = ""
        Dim UserId As Long = 0
        Dim ProviderId As Long = 0
        Dim ProviderUserId As Long = 0
        Dim LoginName As String = ""

        Try
            cmd = New SqlCommand
            conn = New SqlConnection(GetConnectionString())

            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            strQuery = "SELECT sSettingsValue FROM dbo.settings WHERE sSettingsName = 'gloIM ForecastReconcileDone default user'"
            cmd.CommandText = strQuery

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            UserId = cmd.ExecuteScalar()

            If UserId = 0 Then
                strQuery = vbNullString
                strQuery = "SELECT nProviderID FROM  Patient WHERE nPatientId = " & _nPatientID
                cmd.CommandText = strQuery

                ProviderId = cmd.ExecuteScalar()

                strQuery = vbNullString
                strQuery = "SELECT sLoginName FROM User_Mst where nProviderID=" & ProviderId & ""
                cmd.CommandText = strQuery

                LoginName = cmd.ExecuteScalar()

                If LoginName <> "" Then
                    strQuery = vbNullString
                    strQuery = "SELECT nUserId FROM User_Mst where sLoginName='" & LoginName & "'"
                    cmd.CommandText = strQuery

                    ProviderUserId = cmd.ExecuteScalar()
                End If

                If ProviderUserId <> 0 Then
                    UserId = ProviderUserId
                End If

            End If

            Return UserId
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            ex = Nothing
            Return Nothing
        Finally
            If Not IsNothing(conn) Then ''connection close done
                If (conn.State = ConnectionState.Open) Then
                    conn.Close()
                End If
                conn.Dispose()
            End If
        End Try

    End Function

    Private Function GenerateTasks(ByVal _PatientID As Int64, ByVal _Subject As String, ByVal _Note As String, ByVal _ForecastId As Int64, ByVal _TaskType As Int32, ByVal _AssignTo As Long)
        Dim oTask As gloTaskMail.Task = Nothing
        Dim ogloTask As gloTaskMail.gloTask = Nothing
        Dim oTaskAssign As gloTaskMail.TaskAssign = Nothing

        Try
            Dim _TaskID As Long = 0

            '' Send the Task to The Users

            Dim dtDueDate As DateTime = Now
            Dim dtTaskDate As DateTime = Now
            Dim sPriority As String = "Medium"
            dtDueDate = Format(dtDueDate, "MM/dd/yyyy") & " " & Format(dtDueDate, "Short Time")
            dtTaskDate = Format(dtTaskDate, "MM/dd/yyyy") & " " & Format(dtTaskDate, "Short Time")

            oTask = New gloTaskMail.Task
            ogloTask = New gloTaskMail.gloTask(gloLibCCDGeneral.Connectionstring)
            oTaskAssign = New gloTaskMail.TaskAssign(gloLibCCDGeneral.Connectionstring)

            oTask.TaskID = 0
            oTask.UserID = gnLoginID
            oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTaskDate))
            oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTaskDate))
            oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtDueDate))
            oTask.Subject = _Subject
            oTask.PriorityID = 2 ''Medium
            oTask.Notes = _Note
            oTask.PatientID = _PatientID
            oTask.ReferenceID1 = _ForecastId
            oTask.ClinicID = gnClinicID
            oTask.OwnerID = gnLoginID
            oTask.TaskType = _TaskType

            oTaskAssign.AssignFromID = gnLoginID
            oTaskAssign.AssignFromName = gstrLoginName
            oTaskAssign.AssignToID = _AssignTo
            If oTaskAssign.AssignFromID = oTaskAssign.AssignToID Then
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept
            Else
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
            End If
            'oTaskAssign.AssignToName = ""
            oTask.Assignment.Add(oTaskAssign)
            'oTaskAssign.Dispose()

            ''Task Assign Properties
            ''Task Progress Values
            oTask.Progress.TaskID = 0
            oTask.Progress.Complete = 0
            oTask.Progress.Description = _Note
            oTask.Progress.StatusID = 1 ''Not Started
            oTask.Progress.DateTime = Now.Date
            oTask.Progress.ClinicID = gnClinicID
            _TaskID = ogloTask.Add(oTask)

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(oTask) Then
                oTask.Dispose()
                oTask = Nothing
            End If
            If Not IsNothing(ogloTask) Then
                ogloTask.Dispose()
                ogloTask = Nothing
            End If
            If Not IsNothing(oTaskAssign) Then
                oTaskAssign.Dispose()
                oTaskAssign = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Private Sub C1ConsolidatedList_CellChecked(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1ConsolidatedList.CellChecked
        Dim Result As Integer
        Dim rngRows As C1.Win.C1FlexGrid.CellRange
        Try
            gloImmunizationReconcilation.blnRemovePatientDataSetting = True

            If cmbListType.Text = "Immunization History" Then
                If Convert.ToString(C1ConsolidatedList.GetData(e.Row, COL_ImmHx_Source)) = "Current" Then
                    If C1ConsolidatedList.GetCellCheck(e.Row, COL_ImmHx_Select) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        If gloImmunizationReconcilation.blnRemovePatientDataSetting Then
                            Result = MessageBox.Show("This Record will be removed from the Patient’s Immunization. Are you sure you want to Continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            If Result = MsgBoxResult.Yes Then

                            ElseIf Result = MsgBoxResult.No Then
                                C1ConsolidatedList.SetCellCheck(e.Row, COL_ImmHx_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                Check()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            If cmbListType.Text = "Immunization Forecast" Then
                If Convert.ToString(C1ConsolidatedList.GetData(e.Row, COL_ImmForecast_Source)) = "Current" Then
                    If C1ConsolidatedList.GetCellCheck(e.Row, COL_ImmForecast_Select) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        If gloImmunizationReconcilation.blnRemovePatientDataSetting Then
                            Result = MessageBox.Show("This Record will be removed from the Patient’s Immunization. Are you sure you want to Continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                            If Result = MsgBoxResult.Yes Then

                            ElseIf Result = MsgBoxResult.No Then
                                C1ConsolidatedList.SetCellCheck(e.Row, COL_ImmForecast_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                Check()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

            Check()
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
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
        Dim _gloReconciliation As New gloImmHxReconciliation
        If cmbListType.Text = "Immunization History" Then
            c1Reconciliation.Name = "Immunization_History_ReconcileList"
            _gloReconciliation.SaveColumnWidth(False, c1Reconciliation, _UserID)
            C1ConsolidatedList.Name = "Immunization_History_ReconcileList"
            _gloReconciliation.SaveColumnWidth(False, C1ConsolidatedList, _UserID)
        ElseIf cmbListType.Text = "Immunization Forecast" Then
            c1Reconciliation.Name = "Immunization_Forecast_ReconcileList"
            _gloReconciliation.SaveColumnWidth(False, c1Reconciliation, _UserID)
            C1ConsolidatedList.Name = "Immunization_Forecast_ReconcileList"
            _gloReconciliation.SaveColumnWidth(False, C1ConsolidatedList, _UserID)
        End If

        If Not IsNothing(_gloReconciliation) Then
            _gloReconciliation.Dispose()
            _gloReconciliation = Nothing
        End If
    End Sub
#End Region

    Private Sub chkSelectAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If C1ConsolidatedList.DataSource IsNot Nothing AndAlso C1ConsolidatedList.Rows.Count > 0 Then
                If chkSelectAll.Checked = True Then
                    SelectUnselectAll(True)

                Else
                    SelectUnselectAll(False)
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    Private Sub SelectUnselectAll(IsSelect As Boolean)
        Try
            Dim iscurrentdata As Boolean = False
            Dim Result As Integer
            gloImmunizationReconcilation.blnRemovePatientDataSetting = True
            If C1ConsolidatedList.DataSource IsNot Nothing AndAlso C1ConsolidatedList.Rows.Count > 0 Then
                For i As Integer = 1 To C1ConsolidatedList.Rows.Count - 1
                    C1ConsolidatedList.Rows(i)(0) = IsSelect
                    If cmbListType.Text = "Immunization History" Then
                        If Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmHx_Source)) = "Current" Then
                            If C1ConsolidatedList.GetCellCheck(i, COL_ImmHx_Select) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                                If gloImmunizationReconcilation.blnRemovePatientDataSetting Then
                                    iscurrentdata = True
                                End If
                            End If
                        End If
                    End If

                    If cmbListType.Text = "Immunization Forecast" Then
                        If Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmForecast_Source)) = "Current" Then
                            If C1ConsolidatedList.GetCellCheck(i, COL_ImmForecast_Select) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                                If gloImmunizationReconcilation.blnRemovePatientDataSetting Then
                                    iscurrentdata = True
                                End If
                            End If
                        End If
                    End If

                    Dim rngRows As C1.Win.C1FlexGrid.CellRange
                    If C1ConsolidatedList.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        rngRows = C1ConsolidatedList.GetCellRange(i, 0, i, c1Reconciliation.Cols.Count - 1)
                        rngRows.Style = Nothing
                        rngRows.Style = cStyle_StrikoutFont
                    Else
                        rngRows = C1ConsolidatedList.GetCellRange(i, 0, i, c1Reconciliation.Cols.Count - 1)
                        rngRows.Style = Nothing
                        rngRows.Style = cStyle_NormalFont
                    End If
                Next
                If IsSelect = False Then
                    If iscurrentdata Then
                        Result = MessageBox.Show("The Records with the Source 'Current' will be removed from the Patient’s Immunization. Are you sure you want to Continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                        If Result = MsgBoxResult.Yes Then
                        ElseIf Result = MsgBoxResult.No Then
                            For i As Integer = 1 To C1ConsolidatedList.Rows.Count - 1
                                If cmbListType.Text = "Immunization History" Then
                                    If Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmHx_Source)) = "Current" Then
                                        C1ConsolidatedList.SetCellCheck(i, COL_ImmHx_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                        Dim rngRows As C1.Win.C1FlexGrid.CellRange

                                        rngRows = C1ConsolidatedList.GetCellRange(i, 0, i, c1Reconciliation.Cols.Count - 1)
                                        rngRows.Style = Nothing
                                        rngRows.Style = cStyle_NormalFont
                                    End If
                                End If

                                If cmbListType.Text = "Immunization Forecast" Then
                                    If Convert.ToString(C1ConsolidatedList.GetData(i, COL_ImmForecast_Source)) = "Current" Then
                                        C1ConsolidatedList.SetCellCheck(i, COL_ImmHx_Select, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                        Dim rngRows As C1.Win.C1FlexGrid.CellRange

                                        rngRows = C1ConsolidatedList.GetCellRange(i, 0, i, c1Reconciliation.Cols.Count - 1)
                                        rngRows.Style = Nothing
                                        rngRows.Style = cStyle_NormalFont
                                    End If
                                End If
                            Next
                            Check()
                            Exit Sub
                        End If
                    End If
                End If

            End If
        Catch generatedExceptionName As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(generatedExceptionName.ToString(), False)
        End Try
    End Sub
    Private Sub Check()
        Try
            Dim DisSelect As Boolean = False

            For i As Integer = 1 To C1ConsolidatedList.Rows.Count - 1
                If C1ConsolidatedList.Rows(i)(0) = False Then
                    DisSelect = True
                    Exit For
                End If
            Next
            RemoveHandler chkSelectAll.CheckedChanged, AddressOf chkSelectAll_CheckedChanged
            chkSelectAll.Checked = Not DisSelect
            AddHandler chkSelectAll.CheckedChanged, AddressOf chkSelectAll_CheckedChanged
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try

    End Sub
End Class
