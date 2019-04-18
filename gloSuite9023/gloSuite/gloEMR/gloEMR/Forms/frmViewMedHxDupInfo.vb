Imports System.Text.StringBuilder
Imports gloEMRGeneralLibrary.gloEMRActors

Public Class frmViewMedHxDupInfo

    ''Dim _strbldDupMedHxDrugs As System.Text.StringBuilder
    Dim oDrugs As New gloEMRGeneralLibrary.gloEMRActors.Drugs


    Private sPatientName As String
    Public Property PatientName() As String
        Get
            Return sPatientName
        End Get
        Set(ByVal value As String)
            sPatientName = value
        End Set
    End Property


    Public Sub New(ByVal Drugs As gloEMRGeneralLibrary.gloEMRActors.Drugs)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ''If Not IsNothing(DuplicateMedHxDrugs) Then
        ''    _strbldDupMedHxDrugs = DuplicateMedHxDrugs ''assign the DuplicateMedHxDrugs value to form level string bulder variable _strbldDupMedHxDrugs

        ''Else
        ''    _strbldDupMedHxDrugs = Nothing
        ''End If

        ''DuplicateMedHxDrugs = Nothing ''since there is no dipose set the value to nothing
        'oDrugs = New gloEMRGeneralLibrary.gloEMRActors.Drugs
        If Not IsNothing(Drugs) Then
            oDrugs = Drugs
        Else
            oDrugs = Nothing
        End If

        'Drugs.Dispose()
        'Drugs = Nothing

    End Sub



    Private Sub frmViewMedHxDupInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Dim strDrugInfo As String = ""
            'If Not IsNothing(_strbldDupMedHxDrugs) Then
            '    If _strbldDupMedHxDrugs.Length > 0 Then
            '        _strbldDupMedHxDrugs.Replace("{", "")
            '        _strbldDupMedHxDrugs.Replace("}", "")
            '        strDrugInfo = _strbldDupMedHxDrugs.ToString()

            '    End If
            'End If

            If Not IsNothing(oDrugs) Then
                If oDrugs.Count > 0 Then
                    lblInformationMessage.Text = "The following drugs are already in the medications list for """ & sPatientName & """ and will not be imported"
                    For nDrgCnt As Integer = 0 To oDrugs.Count - 1
                        MedHxDupDrugFlexgrid.Rows.Add()
                        MedHxDupDrugFlexgrid.SetData(nDrgCnt + 1, "ColDrugName", oDrugs.Item(nDrgCnt).DrugsName.ToString)
                        MedHxDupDrugFlexgrid.SetData(nDrgCnt + 1, "ColStartDate", oDrugs.Item(nDrgCnt).Duration.ToString)
                    Next

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally
            If Not IsNothing(oDrugs) Then
                oDrugs.Dispose()
                oDrugs = Nothing
            End If

        End Try
    End Sub

    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Me.Close()
    End Sub



End Class