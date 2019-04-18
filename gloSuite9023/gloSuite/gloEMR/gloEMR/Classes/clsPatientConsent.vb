Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class clsPatientConsent


   
    ' Private ds As New System.Data.DataSet
    Private dv As DataView
    Private dt As DataTable

    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
        'If Not IsNothing(ds) Then
        '    ds.Dispose()
        '    ds = Nothing
        'End If

        'slr free Con
        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If

    End Sub
    Public ReadOnly Property GetDataTable() As DataTable
        Get
            Return dt
        End Get
    End Property
    Private _PatientConsent As String = "PatientConsent"
    Public ReadOnly Property PatientConsent() As String
        Get
            Return _PatientConsent
        End Get
    End Property
    Public ReadOnly Property GetDataView() As DataView
        Get
            Return dv
        End Get
    End Property

    '''' To get All Consent(s) for Selected Patient
    Public Function GetAllPatientConsents(ByVal PatientID As Long) As DataView


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        ' Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ViewPatientConsent")
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not dt Is Nothing Then
                dv = New DataView(dt.Copy())
             
                Return dv
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    ' '' to Fill Description of template from Template Gallery
    'Public Function GetTemplate(ByVal TemplateID As Long) As DataTable
    '    Try
    '        Dim adpt As New SqlDataAdapter
    '        Dim dt As New DataTable

    '        cmd = New SqlCommand("gsp_GetExamContents", Con)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        Dim objParam As SqlParameter
    '        objParam = cmd.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = TemplateID

    '        adpt.SelectCommand = cmd
    '        adpt.Fill(dt)
    '        Con.Close()
    '        Return dt
    '    Catch ex As Exception
    '        If Con.State = ConnectionState.Open Then
    '            Con.Close()
    '        End If
    '    End Try
    'End Function

    '' To Select Patient Letter 
    Public Function ScanPatientConsent(ByVal LetterID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '        Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ConsentID"
            oParamater.Value = LetterID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If

            dt = oDB.GetDataTable("gsp_ScanPatientConsent")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    ' To Fill ComboBox Template
    Public Function FillTemplates() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '        Dim oResultTable As New DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 16 '' to Fill Patient Consent Templates
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If

            dt = oDB.GetDataTable("gsp_FillTemplateGallery_MST")

            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Function SavePatientConsent(ByVal LetterID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal LetterDate As Date, ByVal strTempFilePath As String, ByVal strTemplateName As String, ByVal IsFinished As Boolean) As Long
        '' Save Patient Consent for  
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ConsentDate"
            oParamater.Value = LetterDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsFinished"

            If IsFinished = True Then
                oParamater.Value = 1
            Else
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientConsent"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.BigInt
            'oParamater.Direction = ParameterDirection.InputOutput
            'oParamater.Name = "@ConsentID"
            'oParamater.Value = LetterID
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing


            ''Sandip Darade 20100422
            ''Case GLO2010-0004880
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@ConsentID"
            oParamater.Value = LetterID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            If IsFinished = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.Finish, "Patient Consent finished", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                If LetterID = 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.Add, "Patient Consent added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Patient Consent Added", gstrLoginName, gstrClientMachineName, gnPatientID)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.Modify, "Patient Consent modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Patient Consent Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                End If

            End If

            LetterID = oDB.Add("gsp_InUpPatientConsent")

            Return LetterID


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            oDB.Dispose()
        End Try
    End Function
    Public Function SavePatientConsentBytes(ByVal LetterID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal LetterDate As Date, ByVal bBytes As Object, ByVal strTemplateName As String, ByVal IsFinished As Boolean) As Long
        '' Save Patient Consent for  
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ConsentDate"
            oParamater.Value = LetterDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@IsFinished"

            If IsFinished = True Then
                oParamater.Value = 1
            Else
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientConsent"
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If



            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            '  objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.BigInt
            'oParamater.Direction = ParameterDirection.InputOutput
            'oParamater.Name = "@ConsentID"
            'oParamater.Value = LetterID
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing


            ''Sandip Darade 20100422
            ''Case GLO2010-0004880
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@ConsentID"
            oParamater.Value = LetterID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            If IsFinished = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.Finish, "Patient Consent finished", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Else
                If LetterID = 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.Add, "Patient Consent added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Patient Consent Added", gstrLoginName, gstrClientMachineName, gnPatientID)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.Modify, "Patient Consent modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Patient Consent Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                End If

            End If

            LetterID = oDB.Add("gsp_InUpPatientConsent")

            Return LetterID


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            oDB.Dispose()
        End Try
    End Function

    '' to Delete Patient Letter of ID LetterID
    Public Sub DeletePatientConsent(ByVal ConsentID As Long, ByVal Consentdate As String, ByVal ConsentHeader As String, ByVal PatientID As Long)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ConsentID"
            oParamater.Value = ConsentID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeletePatientConsent")

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.PatientConsent, gloAuditTrail.ActivityType.Delete, "Patient Consent deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Patient Consent Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Function Fill_LockPatientConsent(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As New DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sMachinName"
            oParamater.Value = MachinName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTrnType"
            oParamater.Value = TransactionType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nMachinID"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_Select_UnLock_Record")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    Public Sub fill_widthofExam(ByRef pnlGloUC_TemplateTreeControl As Panel)
        Dim oDB As DataBaseLayer = Nothing
        Dim oParameter As DBParameter = Nothing
        Dim sDrugForm As String = ""
        Try


            oDB = New DataBaseLayer
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nUserID"
            oParameter.Value = gnLoginID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@SettingsName"
            oParameter.Value = PatientConsent
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.Int
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@Flag"
            oParameter.Value = 1
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            sDrugForm = oDB.GetDataValue("gsp_TemplatePanelWidth", True)

            If IsNumeric(sDrugForm) Then
                pnlGloUC_TemplateTreeControl.Width = sDrugForm
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(oParameter) Then
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub

    Public Sub SaveWidthInDatabase(ByVal nUserId As String, ByVal value As Integer)


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Flag"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nUserID"
            oParamater.Value = nUserId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SettingsName"
            oParamater.Value = PatientConsent
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SettingsValue"
            oParamater.Value = value
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachinName"
            oParamater.Value = ""
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oDB.Add("gsp_TemplatePanelWidth")

        Catch ex As Exception

        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub
End Class
