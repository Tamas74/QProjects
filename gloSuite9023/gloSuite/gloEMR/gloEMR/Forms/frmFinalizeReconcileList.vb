Imports System.Windows.Forms
Imports gloSettings
Imports gloCCDLibrary

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
    Public Enum FormAction
        Accepted
        Closed
        None
    End Enum
    Private ActionPerformed As FormAction = FormAction.None
#End Region

#Region "Constants--"

    Private Const COL_Med_Select As Integer = 0
    Private Const COL_Med_Dup As Integer = 1
    Private Const COL_Med_Source As Integer = 2
    Private Const Col_Med_Drug As Integer = 3
    Private Const Col_Med_sNDCCode As Integer = 4
    Private Const Col_Med_Amount As Integer = 5
    Private Const Col_Med_DrugForm As Integer = 6
    Private Const Col_Med_Frequency As Integer = 7

    Private Const Col_Med_DaysSupply As Integer = 8
    Private Const Col_Med_MedicationDate As Integer = 9
    Private Const Col_Med_StartDate As Integer = 10
    Private Const Col_Med_Refills As Integer = 11
    Private Const Col_Med_Status As Integer = 12
    Private Const Col_Med_AllowSub As Integer = 13


    Private Const Col_Med_Rx_sName As Integer = 14
    Private Const Col_Med_Rx_sNCPDPID As Integer = 15
    Private Const Col_Med_Rx_NPI As Integer = 16
    Private Const Col_Med_Rx_sAddressline1 As Integer = 17
    Private Const Col_Med_Rx_sAddressline2 As Integer = 18
    Private Const Col_Med_Rx_sCity As Integer = 19
    Private Const Col_Med_Rx_sState As Integer = 20
    Private Const Col_Med_Rx_sZip As Integer = 21
    Private Const Col_Med_Rx_sPhone As Integer = 22
    Private Const Col_Med_Rx_sFax As Integer = 23
    Private Const Col_Med_Rx_sEmail As Integer = 24
    Private Const PrescriberNPI As Integer = 25
    Private Const Col_Med_Direction As Integer = 26


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
    Public Sub New(ByVal dtReconcile As DataTable, ByVal ListType As String, ByVal PatientID As Int64, ByVal strListID As String, ByVal strSkippedID As String, ByVal LoginUser As String, ByVal LoginID As Long)


        InitializeComponent()
        _dtReconcile = dtReconcile
        _ListType = ListType
        _PatientID = PatientID
        _strListID = strListID
        _strSkippedID = strSkippedID
        _UserName = LoginUser
        _UserID = LoginID

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
    Public Sub LoadFinalizeRecords()
        Try
            c1ReconcileList.DataSource = _dtReconcile.DefaultView
            Dim k As Integer = 1

            For k = 1 To c1ReconcileList.Rows.Count - 1
                If Not k >= c1ReconcileList.Rows.Count Then

                    If Not IsDBNull(c1ReconcileList.GetData(k, COL_Med_Select)) Then
                        If Convert.ToBoolean(c1ReconcileList.GetData(k, 0)) = False Then
                            c1ReconcileList.Rows(k).Visible = False
                            ' c1ReconcileList.Rows.Remove(k)
                            ' k = k - 1
                        End If

                    End If
                End If

            Next

            DesignConsolidatedList()

            Me.Text = "Patients Active Medications"
            lblListType.Text = "Patients Active Medications"

            gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), "gloEMR")

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

        End Try
    End Sub
#End Region

#Region "Design Grid"

    Private Sub DesignConsolidatedList()

        c1ReconcileList.Redraw = False
        Try
            c1ReconcileList.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn


            'C1ConsolidatedList.Cols(COL_Med_Select).AllowEditing = True
            'C1ConsolidatedList.Cols(COL_Med_Select).Caption = "Select"
            'C1ConsolidatedList.Cols(COL_Med_Select).DataType = GetType(Boolean)
            'C1ConsolidatedList.Cols(COL_Med_Select).AllowSorting = True


            'C1ConsolidatedList.Cols(COL_Med_Dup).Caption = "Dup"
            'C1ConsolidatedList.Cols(COL_Med_Dup).AllowEditing = False
            'C1ConsolidatedList.Cols(COL_Med_Dup).AllowSorting = True

            'C1ConsolidatedList.Cols(COL_Med_Source).Caption = "Source"
            'C1ConsolidatedList.Cols(COL_Med_Source).AllowEditing = False
            'C1ConsolidatedList.Cols(COL_Med_Source).AllowSorting = True


            c1ReconcileList.Cols(Col_Med_Drug).Caption = "Drug"
            c1ReconcileList.Cols(Col_Med_Drug).AllowEditing = False
            c1ReconcileList.Cols(Col_Med_Drug).AllowSorting = True


            c1ReconcileList.Cols(Col_Med_sNDCCode).Caption = "NDCCode"
            c1ReconcileList.Cols(Col_Med_sNDCCode).AllowEditing = False


            c1ReconcileList.Cols(Col_Med_Amount).Caption = "Quantity"
            c1ReconcileList.Cols(Col_Med_Amount).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_DrugForm).Caption = "Drug Form"
            c1ReconcileList.Cols(Col_Med_DrugForm).AllowEditing = False


            c1ReconcileList.Cols(Col_Med_Frequency).Caption = "Frequency"
            c1ReconcileList.Cols(Col_Med_Frequency).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_DaysSupply).Caption = "DaysSupply"
            c1ReconcileList.Cols(Col_Med_DaysSupply).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_MedicationDate).Caption = "LastUpdated"
            c1ReconcileList.Cols(Col_Med_MedicationDate).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_StartDate).Caption = "Start Date"
            c1ReconcileList.Cols(Col_Med_StartDate).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Refills).Caption = "Refills"
            c1ReconcileList.Cols(Col_Med_Refills).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Status).Caption = "Status"
            c1ReconcileList.Cols(Col_Med_Status).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_AllowSub).Caption = "AllowSub" 'Extra Added
            c1ReconcileList.Cols(Col_Med_AllowSub).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sName).Caption = "PharmacyName"
            c1ReconcileList.Cols(Col_Med_Rx_sName).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sNCPDPID).Caption = "PharmacyNCPDPID"
            c1ReconcileList.Cols(Col_Med_Rx_sNCPDPID).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_NPI).Caption = "NPI" 'Extra Added
            c1ReconcileList.Cols(Col_Med_Rx_NPI).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sAddressline1).Caption = "PharmacyAddressline1"
            c1ReconcileList.Cols(Col_Med_Rx_sAddressline1).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sAddressline2).Caption = "PharmacyAddressline2"
            c1ReconcileList.Cols(Col_Med_Rx_sAddressline2).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sCity).Caption = "PharmacyCity"
            c1ReconcileList.Cols(Col_Med_Rx_sCity).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sState).Caption = "PharmacyState"
            c1ReconcileList.Cols(Col_Med_Rx_sState).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sZip).Caption = "PharmacyZip"
            c1ReconcileList.Cols(Col_Med_Rx_sZip).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sPhone).Caption = "PharmacyPhone"
            c1ReconcileList.Cols(Col_Med_Rx_sPhone).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sFax).Caption = "PharmacyFax"
            c1ReconcileList.Cols(Col_Med_Rx_sFax).AllowEditing = False

            c1ReconcileList.Cols(Col_Med_Rx_sEmail).Caption = "PharmacyEmail"
            c1ReconcileList.Cols(Col_Med_Rx_sEmail).AllowEditing = False

            c1ReconcileList.Cols(PrescriberNPI).Caption = "PrescriberNPI"
            c1ReconcileList.Cols(PrescriberNPI).AllowEditing = False


            c1ReconcileList.Cols(Col_Med_Direction).Caption = "Direction"
            c1ReconcileList.Cols(Col_Med_Direction).AllowEditing = False

            c1ReconcileList.Cols(COL_Med_Select).Width = 0
            c1ReconcileList.Cols(COL_Med_Dup).Width = 0
            c1ReconcileList.Cols(COL_Med_Source).Width = 0
            c1ReconcileList.Cols(Col_Med_Drug).Width = 100
            c1ReconcileList.Cols(Col_Med_sNDCCode).Width = 100
            c1ReconcileList.Cols(Col_Med_Amount).Width = 80
            c1ReconcileList.Cols(Col_Med_Frequency).Width = 80
            c1ReconcileList.Cols(Col_Med_DaysSupply).Width = 80
            c1ReconcileList.Cols(Col_Med_MedicationDate).Width = 80
            c1ReconcileList.Cols(Col_Med_StartDate).Width = 80
            c1ReconcileList.Cols(Col_Med_Refills).Width = 40
            c1ReconcileList.Cols(Col_Med_DrugForm).Width = 80
            c1ReconcileList.Cols(Col_Med_Status).Width = 80
            c1ReconcileList.Cols(Col_Med_AllowSub).Width = 0

            c1ReconcileList.Cols(Col_Med_Rx_sName).Width = 200
            c1ReconcileList.Cols(Col_Med_Rx_sNCPDPID).Width = 100
            c1ReconcileList.Cols(Col_Med_Rx_NPI).Width = 100
            c1ReconcileList.Cols(Col_Med_Rx_sAddressline1).Width = 0
            c1ReconcileList.Cols(Col_Med_Rx_sAddressline1).Width = 0
            c1ReconcileList.Cols(Col_Med_Rx_sCity).Width = 0
            c1ReconcileList.Cols(Col_Med_Rx_sState).Width = 0

            c1ReconcileList.Cols(Col_Med_Rx_sZip).Width = 0
            c1ReconcileList.Cols(Col_Med_Rx_sEmail).Width = 0
            c1ReconcileList.Cols(Col_Med_Rx_sFax).Width = 0
            c1ReconcileList.Cols(Col_Med_Rx_sPhone).Width = 0
            c1ReconcileList.Cols(PrescriberNPI).Width = 100
            c1ReconcileList.Cols(Col_Med_Direction).Width = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            c1ReconcileList.Redraw = True
        End Try
    End Sub
#End Region

    Public Function GetPatientsActiveMedication() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As DataTable = Nothing

        Try
            oDB.Connect(False)
            oParameters.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetMedication", oParameters, dt)
            Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        Finally
            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

#Region "Button Clicks"


    Private Sub tlbbtn_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Close.Click
        SelectedAction = FormAction.Closed
        Me.Close()
    End Sub

    Private Sub tlbbtn_Accept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tlbbtn_Accept.Click
        ''Save Data against patient from reconciliation lists
        AcceptReconcileLists()
        SelectedAction = FormAction.Accepted
        Me.Close()
    End Sub
#End Region

#Region "AcceptReconcileLists"
    Public Sub AcceptReconcileLists()
        Dim ogloPatientRegDBLayer As New gloPatientRegDBLayer(GetConnectionString())
        Dim _result As Boolean = False
        Try
            Dim oMedicationCol As MedicationsCol = Nothing
            If c1ReconcileList.Rows.Count > 1 Then
                oMedicationCol = New MedicationsCol
                For i As Int16 = 1 To c1ReconcileList.Rows.Count - 1
                    If c1ReconcileList.Rows(i)(COL_Med_Source) <> "Current" AndAlso Convert.ToBoolean(c1ReconcileList.GetData(i, 0)) <> False Then

                        Dim oMedication As Medication = Nothing
                        oMedication = New Medication
                        If Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_MedicationDate)) = Date.Now.ToString("MM/dd/yyyy") Then
                            oMedication.MedicationID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sNDCCode))
                        Else
                            oMedication.MedicationID = 0
                        End If

                        oMedication.User = _UserName
                        oMedication.PatientID = _PatientID
                        oMedication.GenericName = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Drug))
                        oMedication.DrugName = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Drug))

                        oMedication.RxNormCode = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sNDCCode))
                        oMedication.ProdCode = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_sNDCCode))

                        oMedication.DrugQuantity = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Amount))
                        oMedication.DrugForm = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_DrugForm))
                        'oMedication.Frequency = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Frequency))
                        oMedication.Days = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_DaysSupply))
                        oMedication.MedicationDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_MedicationDate))
                        oMedication.StartDate = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_StartDate))
                        If oMedication.Days <> "" Then
                            Dim dtendate As Date = Convert.ToDateTime(oMedication.StartDate).AddDays(oMedication.Days)
                            oMedication.EndDate = dtendate.ToString()
                        Else
                            oMedication.EndDate = ""
                        End If
                        oMedication.Refills = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Refills))
                        oMedication.Status = "Active"
                        oMedication.Rx_MaySubstitute = If(Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_AllowSub)) = "Yes", 1, 0)
                        oMedication.Rx_PhName = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sName))
                        oMedication.Rx_sNCPDPID = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sNCPDPID))
                        oMedication.Frequency = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Direction))
                        oMedication.Rx_sAddressline1 = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sAddressline1))
                        oMedication.Rx_sAddressline2 = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sAddressline2))
                        oMedication.Rx_sCity = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sCity))
                        oMedication.Rx_sState = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sState))
                        oMedication.Rx_sZip = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sZip))
                        oMedication.Rx_sPhone = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sPhone))
                        oMedication.Rx_sFax = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sFax))
                        oMedication.Rx_sEmail = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_sEmail))

                        'oMedication.FreeTextBrandName = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Direction))
                        ' oMedication.Route = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Direction))
                        oMedication.StrengthUnits = "" 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Direction))
                        oMedication.DrugStrength = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Amount))
                        oMedication.Reason = "" 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Reason))
                        oMedication._PrescriptionId = 0 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_PrescriptionID))
                        oMedication.Renewed = "" 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Renewed))
                        oMedication.IsNarcotics = 0 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_IsNarcotic))
                        If Convert.ToString(c1ReconcileList.Rows(i)(COL_Med_Source)) <> "Current" Then
                            oMedication.PBMSourceName = Convert.ToString(c1ReconcileList.Rows(i)(COL_Med_Source))
                        Else
                            oMedication.PBMSourceName = ""
                        End If

                        oMedication.RxMedDMSID = 0 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Rx_DMSID))
                        oMedication.Rx_Method = "" 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_Method))
                        oMedication.Rx_PrescriberNotes = "" 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_PrescriberNotes))
                        oMedication.Duration = Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_DaysSupply))
                        oMedication.Rx_Notes = "" 'Convert.ToString(c1ReconcileList.Rows(i)(Col_Med_RxNotes))
                        ' MedicationVisitID = gloReconciliation.GenerateVisitID(DateTime.Now, oMedication.PatientID)
                        oMedicationCol.Add(oMedication)
                        If IsNothing(oMedication) = False Then
                            oMedication.Dispose()
                            oMedication = Nothing
                        End If
                    End If

                Next
            End If
            If Not IsNothing(oMedicationCol) Then
                If oMedicationCol.Count > 0 Then
                    MedicationVisitID = gloReconciliation.GenerateVisitID(DateTime.Now, _PatientID)
                    _result = gloReconciliation.SaveMedication(oMedicationCol)
                End If
            End If
            If Not IsNothing(oMedicationCol) Then
                oMedicationCol.Dispose()
                oMedicationCol = Nothing
            End If
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.AcceptReconcileList, "Accepted " & _ListType & " Lists", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)

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


#Region "Save Column Width"
    Private Sub SaveReconcileColumnWidth()
        Dim _gloReconciliation As New gloReconciliation

        c1ReconcileList.Name = "Meds_FinalizeList"
        _gloReconciliation.SaveColumnWidth(False, c1ReconcileList, _UserID)

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
End Class
