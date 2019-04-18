Imports gloCommon

Public Class frmAddSummaryCareRecord
    Dim _patientID As Int64 = 0
    Dim _VisitID As Long
    Dim _IsNewSummary As Boolean = False
    Dim _TransactionDate As DateTime
    Dim _bRecordUnavailable As Boolean = False
    Dim _bRecordRemark As String = String.Empty
    Dim _SummaryOfCareRecordStatusID As Int64 = 0
    Dim _dtSCare As DataTable
    Private _c1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid
    Public sumofcare_ID As Long = 0
    Dim nProviderAssociationID As Int64 = 0
    Dim sProviderTaxID As String = ""

   

   

    Public Property patientID As Long
        Get

            Return _patientID
        End Get
        Set(ByVal Value As Long)
            _patientID = Value
        End Set
    End Property
    Public Property IsNewSummary() As Boolean
        Get
            Return _IsNewSummary
        End Get
        Set(ByVal value As Boolean)
            _IsNewSummary = value
        End Set
    End Property
    Public Property SummaryOfCareRecordStatusID As Int64
        Get

            Return _SummaryOfCareRecordStatusID
        End Get
        Set(ByVal Value As Int64)
            _SummaryOfCareRecordStatusID = Value
        End Set
    End Property
    Public Property bRecordUnavailable As Boolean
        Get

            Return _bRecordUnavailable
        End Get
        Set(ByVal Value As Boolean)
            _bRecordUnavailable = Value
        End Set
    End Property
    Public Property strRecordRemark As String
        Get

            Return _bRecordRemark
        End Get
        Set(ByVal Value As String)
            _bRecordRemark = Value
        End Set
    End Property
    Public Property TransactionDate As String
        Get

            Return _TransactionDate
        End Get
        Set(ByVal Value As String)
            _TransactionDate = Value
        End Set
    End Property
    Public Property dtSCare() As DataTable
        Get
            Return _dtSCare
        End Get
        Set(ByVal value As DataTable)
            _dtSCare = value
        End Set
    End Property

    Public Sub New(ByVal PatientID As Long)

        InitializeComponent()
        _patientID = PatientID


    End Sub
    Private Sub frmAddSummaryCareRecord_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim scheme As gloCommon.Cls_TabIndexSettings.TabScheme = gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst
        Dim tom As New Cls_TabIndexSettings(Me)
        tom.SetTabOrder(scheme)
        tom = Nothing


        dtSummaryCareRecordDate.Value = TransactionDate
        chkSummaryCareRecord.Checked = bRecordUnavailable
        txtRemarks.Text = strRecordRemark

    End Sub

    Private Sub ts_btnClose_Click(sender As Object, e As System.EventArgs) Handles ts_btnClose.Click
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SummaryCareRecord, gloAuditTrail.ActivityCategory.SummaryCareRecord, gloAuditTrail.ActivityType.View, "Summary Care Record Viewed", patientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Me.Close()
    End Sub

    Private Sub ts_btnSaveClose_Click(sender As Object, e As System.EventArgs) Handles ts_btnSaveClose.Click
        Dim blnrecexist As Boolean = False
        Dim objSummaryCareRecord As New clsViewSummaryCareRecord
        Dim nProviderID As Int64 = IIf(gnLoginProviderID <> 0, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(patientID))
        If Not getProviderTaxID(nProviderID) Then
            Exit Sub
        End If

        sumofcare_ID = objSummaryCareRecord.SummaryCareRecordDetails(patientID, SummaryOfCareRecordStatusID, Convert.ToDateTime(dtSummaryCareRecordDate.Text), chkSummaryCareRecord.Checked, txtRemarks.Text)
        If sumofcare_ID = -1 Then
            MessageBox.Show("Record  Already Exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            blnrecexist = True
        Else
            Dim oclsselectProviderTaxID As New gloGlobal.TIN.clsSelectProviderTaxID(nProviderID)
            oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, sumofcare_ID, sProviderTaxID, nProviderID, gnLoginProviderID, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.SummaryCareRecord.GetHashCode())
            oclsselectProviderTaxID = Nothing
        End If
        If (blnrecexist = True) Then
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SummaryCareRecord, gloAuditTrail.ActivityCategory.SummaryCareRecord, gloAuditTrail.ActivityType.View, "Summary Care Record Viewed", patientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Else
            If sumofcare_ID = SummaryOfCareRecordStatusID Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SummaryCareRecord, gloAuditTrail.ActivityCategory.SummaryCareRecord, gloAuditTrail.ActivityType.Modify, "Summary Care Record Modified", patientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SummaryCareRecord, gloAuditTrail.ActivityCategory.SummaryCareRecord, gloAuditTrail.ActivityType.Save, "Summary Care Record Added", patientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End If
        End If
        


        objSummaryCareRecord.Dispose()
        objSummaryCareRecord = Nothing
        Me.Close()

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