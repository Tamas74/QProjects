Public Class frmEMResult
    Public strEmCode As String

    Private _EMCode As String
    Private _Result As String
    Private _emResult As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtGenerateResult
    Private Shared selectionFont As Font = Nothing

    Public Sub New(ByVal emResult As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtGenerateResult)
        _emResult = emResult
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmEMResult_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If _emResult.EMCode <> "" Then
                lblEMCode.Text = _emResult.EMCode
                strEmCode = _emResult.EMCode
            Else
                tlb_Accept.Enabled = False
                lblEMCode.Text = "None"
            End If

            ''intergated by Mayuri:20120816 from glosuite7010- Added new Alpha II SDK
            ' Dim EMGenerate As New AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtGenerate
            lblCodeType.Text = GetCodeTypeString(_emResult.EvalMgmtGenerateSelections.CodeType)

            lblHistoryLevel.Text = _emResult.HistoryLevel.ToString()
            lblExamLevel.Text = _emResult.ExamLevel.ToString()
            lblMedicalComplexityLevel.Text = _emResult.MedCompLevel.ToString()
            rchtxtEMResult.ResetText()
            If (IsNothing(selectionFont)) Then
                selectionFont = New Font(Me.rchtxtEMResult.Font.FontFamily, Me.rchtxtEMResult.Font.Size + 1.0F, FontStyle.Underline Or FontStyle.Bold)
            End If
            For Each Edit As AlphaII.CodeWizard.Objects.Edit In _emResult.EvalMgmtEdits
                Me.rchtxtEMResult.SelectionFont = selectionFont
                Me.rchtxtEMResult.AppendText(Edit.Rule.Name & vbNewLine)
                Me.rchtxtEMResult.AppendText(Edit.Rule.Description & vbNewLine & vbNewLine)

            Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Property EMCode() As String
        Get
            Return _EMCode
        End Get
        Set(ByVal value As String)
            _EMCode = value
        End Set
    End Property
    Public Property Result() As String
        Get
            Return _Result
        End Get
        Set(ByVal value As String)
            _Result = value
        End Set
    End Property
    Private Function GetCodeTypeString(ByVal codeType As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType) As String
        Select Case codeType
            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcNew
                Return "Office or Other Outpatient Services - NEW"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.OfficeOutpatientSvcEstablished
                Return "Office of Other Outpatient Services - ESTABLISHED"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospObservationSvc
                Return "Hospital Observation Services"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospObservationSvcWAdmissionDischarge
                Return "Hospital Observation Services w/ Admission and Discharge"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospInpatientSvcInitialCare
                Return "Hospital Inpatient Services - INITIAL CARE"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HospInpatientSvcSubsequentCare
                Return "Hospital Inpatient Services - SUBSEQUENT CARE"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.EmergencyDeptSvc
                Return "Emergency Department Services"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.ConsultOfficeOutpatient
                Return "Consultations: Office of Other Outpatient"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.ConsultInitialInpatient
                Return "Consultations: Inpatient"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.NursingFacilityInitialCompAssesment
                Return "Nursing Facility: INITIAL Comprehensive Assesment"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.NursingFacilitySubsequentCompAssessment
                Return "Nursing Facility: SUBSEQUENT"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HomeServicesNew
                Return "Home Services - New"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.HomeServicesEstablished
                Return "Home Services - Established"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.DomRestHomeCustCareServicesNew
                Return "Domiciliary, Rest Home, Custodial Care Services - New"

            Case AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtCodeType.DomRestHomeCustCareServicesEstb
                Return "Domiciliary, Rest Home, Custodial Care Services - Estb"
        End Select
        Return "None"
    End Function

    Private Sub btnEMClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEMClose.Click
        Me.Close()
    End Sub

    Private Sub tlsDictionary_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsDictionary.ItemClicked
        Try
            Select Case e.ClickedItem.Tag.ToString()
                Case "Accept"
                    EMCode = strEmCode
                    Result = e.ClickedItem.Tag.ToString()

                    'Audit Trail
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "EM Code Accepted", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "EM Code Accepted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "EM Code Accepted", gstrLoginName, gstrClientMachineName, 0)
                    Me.Close()

                Case "Reject"
                    Result = e.ClickedItem.Tag.ToString()

                    'Audit Trail
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "EM Code Rejected", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Delete, "EM Code Rejected", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "EM Code Rejected", gstrLoginName, gstrClientMachineName, 0)
                    Me.Close()

                Case "Print"

                Case "Close"
                    Result = e.ClickedItem.Tag.ToString()
                    Me.Close()
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

End Class