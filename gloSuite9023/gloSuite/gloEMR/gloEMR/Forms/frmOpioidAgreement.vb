Imports gloEDocumentV3
Imports gloOffice
Imports gloEMRGeneralLibrary
Imports gloDatabaseLayer


Public Class frmOpioidAgreement

   

    Dim Rx As gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer = Nothing

    Dim _PatientID As Long = 0
    Dim AgreementID As Long = 0
    Dim dtTemplateDetails As DataTable = Nothing
    'Public Property patientID As Long
    '    Get
    '        Return _patientID
    '    End Get
    '    Set(ByVal Value As Long)
    '        _patientID = Value
    '    End Set
    'End Property
    Public Sub New(ByVal PatientID As Long)

        InitializeComponent()
        _PatientID = PatientID


    End Sub
    Private Sub frmOpioidAgreement_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try
            Rx = New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(_PatientID)
            Dim ds As DataSet = Rx.getOPIDAgreement()
            If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
                If ds.Tables("AgreementVerified").Rows.Count > 0 Then
                    AgreementID = ds.Tables("AgreementVerified").Rows(0)("nOpo_ID")
                    chkSignedAgreement.Checked = Convert.ToBoolean(ds.Tables("AgreementVerified").Rows(0)("IsSignedAgreement"))
                    txtNotes.Text = Convert.ToString(ds.Tables("AgreementVerified").Rows(0)("sInternalnotes"))
                    dtSignedVerifiedDate.Value = Convert.ToDateTime(ds.Tables("AgreementVerified").Rows(0)("dtSignedDate"))
                End If
                If ds.Tables("AgreementScanned").Rows.Count > 0 Then
                    If ds.Tables("AgreementScanned").Rows(0)("nPatientid") <> 0 AndAlso IsDBNull(ds.Tables("AgreementScanned").Rows(0)("nPatientid")) = False Then
                        lnkSignedAgreement.Text = "View Agreement"

                    Else
                        lnkSignedAgreement.Text = "Print Agreement to obtain signature"
                    End If
                Else
                    lnkSignedAgreement.Text = "Print Agreement to obtain signature"
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub




    Private Sub tlsbtnSave_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnSave.Click

        Try

            Rx = New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(_PatientID)
            Rx.SaveOPIDAgreement(_PatientID, chkSignedAgreement.Checked, dtSignedVerifiedDate.Value, txtNotes.Text.Trim(), AgreementID)

        Catch ex As Exception
        Finally
            If Not IsNothing(Rx) Then
                Rx.Dispose()
            End If

        End Try
        Me.Close()


    End Sub

    Private Sub tlsbtnClose_Click(sender As System.Object, e As System.EventArgs) Handles tlsbtnClose.Click

        Me.Close()

    End Sub



    Private Sub chkMedication_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSignedAgreement.CheckedChanged
        'If chkMedication.Checked = True Then
        '    chkSummary.Checked = True
        'Else
        '    chkSummary.Checked = False
        'End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSignedAgreement.LinkClicked
        'Dim frm As gloEDocumentV3.Forms.frmEDocumentViewer = Nothing
        'frm = New gloEDocumentV3.Forms.frmEDocumentViewer
        'frm.ShowDialog()
        'MainMenu.Scan_Documents(True)
        If lnkSignedAgreement.Text.Trim() = "Print Agreement to obtain signature" Then
            PrintTemplate()
            'Dim ogloTemplate As New gloOffice.gloTemplate(GetConnectionString())
            'ogloTemplate.CategoryID = 27
            'ogloTemplate.CategoryName = "Tags"
            'ogloTemplate.TemplateID = 4090203112836301
            'ogloTemplate.TemplateName = "Subject Shoes"
            'ogloTemplate.PatientID = _PatientID
            'ogloTemplate.ClinicID = gnClinicID
            ''ogloTemplate.VisitID = gnVisitID


        Else
            MainMenu.Scan_Documents(True)

        End If





    End Sub
    Private Sub PrintTemplate()
        Dim templateList As List(Of gloOffice.gloTemplate) = New List(Of gloOffice.gloTemplate)()
        getOpioidTemplate()
        Dim template As gloOffice.gloTemplate = Nothing
        If Not IsNothing(dtTemplateDetails) AndAlso dtTemplateDetails.Rows.Count > 0 Then
            Dim _TemplateID As Int64 = dtTemplateDetails.Rows(0)("nTemplateID")
            Dim _TemplateName As String = dtTemplateDetails.Rows(0)("sTemplateName")
            Dim _CategoryID As Int64 = dtTemplateDetails.Rows(0)("nCategoryID")
            Dim _CategoryName As String = dtTemplateDetails.Rows(0)("sCategoryName")
            Dim _ProviderID As Int64 = 0
            gloOffice.Supporting.DataBaseConnectionString = GetConnectionString()
            gloOffice.Supporting.PrimaryID = _TemplateID
            gloOffice.Supporting.isFromBatchPrint = True
            Dim fileName As String = gloOffice.Supporting.GenerateDocumentFile()
            template = New gloTemplate(GetConnectionString())
            template.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString("MM/dd/yyyy"))
            template.AppointmentID = 0
            template.CategoryID = _CategoryID
            template.CategoryName = _CategoryName
            template.TemplateID = _TemplateID
            template.TemplateName = _TemplateName
            template.PrimeryID = 0
            template.ClinicID = 1
            template.DocumentCategory = 0
            template.VisitID = 0
            template.PatientID = _PatientID
            template.TemplateFilePath = fileName
            templateList.Add(template)
            If templateList.Count > 0 Then

                Try
                    Dim PatientTemplateHelper As gloOffice.clsPatientTemplate = New gloOffice.clsPatientTemplate(GetConnectionString())
                    PatientTemplateHelper.ParentForm = Me
                    PatientTemplateHelper.Print(templateList, PrintDocument1)
                Catch ex As Exception
                    'MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Finally
                End Try
            End If
        End If

       

    End Sub
    Private Sub getOpioidTemplate()
        Dim datalayer As gloDatabaseLayer.DBLayer = New DBLayer(GetConnectionString())
        dtTemplateDetails = New DataTable()
        datalayer.Connect(False)
        datalayer.Retrive("gsp_getOpioidTreatmentTemplate", dtTemplateDetails)


    End Sub

    'Public Sub RefreshAgreementIcon()
    '    Rx = New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(_PatientID)
    '    Dim ds As DataSet = Rx.getOPIDAgreement()
    '    If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
    '        ds.Tables(0).TableName = "AgreementVerified"
    '        ds.Tables(1).TableName = "AgreementScanned"
    '        Dim AgreementVerified As Boolean = False
    '        Dim AgreementScanned As Boolean = False
    '        If ds.Tables("AgreementVerified").Rows.Count > 0 Then
    '            AgreementVerified = Convert.ToBoolean(ds.Tables("AgreementVerified").Rows(0)("IsSignedAgreement"))
    '        End If
    '        If ds.Tables("AgreementScanned").Rows.Count > 0 Then
    '            If ds.Tables("AgreementScanned").Rows(0)("nPatientid") <> 0 AndAlso IsDBNull(ds.Tables("AgreementScanned").Rows(0)("nPatientid")) = False Then
    '                AgreementScanned = True
    '            End If
    '        End If
    '        If AgreementVerified AndAlso AgreementScanned Then
    '            ChangeAgreementIcon(True)
    '        End If

    '    End If
    'End Sub
    
End Class