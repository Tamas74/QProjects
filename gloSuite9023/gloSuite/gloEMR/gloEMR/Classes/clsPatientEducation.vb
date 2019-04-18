Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase


Public Class clsPatientEducation
    Implements IDisposable


    Private _PatientID As Long
    Private Ds As System.Data.DataSet
    Private Dv As DataView
    Private Tb As DataTable
    'Private _getEducationMaterialUsingTempalteID As DataTable
    'Private _getEducationMaterialUsingTempalteID1 As DataTable
    Public _nPatientEducationID As Long = 0


    Public ReadOnly Property DsDataSet() As DataSet
        Get

            Return Ds

        End Get
    End Property

    Public ReadOnly Property DsDataview() As DataView
        Get

            Return Dv

        End Get

    End Property

  

    Public Function GetAllEducationByDataTable(ByVal PatientID As Long) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID

            oDB.DBParametersCol.Add(oParamater)

            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ViewPatientEducation")

            Return oResultTable
            'If Not oResultTable Is Nothing Then
            '    Dv = oResultTable.DefaultView
            '    Return Dv
            'End If
            'Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

            'If IsNothing(oResultTable) = False Then
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If

        End Try

    End Function

    Public Function GetAllEducations(ByVal PatientID As Long) As DataView

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID

            oDB.DBParametersCol.Add(oParamater)

            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_ViewPatientEducation")


            If Not oResultTable Is Nothing Then
                Dv = oResultTable.DefaultView
                Return Dv
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

            If IsNothing(oResultTable) = False Then
                oResultTable.Dispose()
                oResultTable = Nothing
            End If

        End Try

    End Function

    Public Function GetSelectedExamEducation(ByVal PatientId As Int64, ByVal VisitId As Int64) As DataTable

        Dim oDB As New DataBaseLayer
        Dim strEducationID As String = ""
        Dim EducationID As Long
        'Dim oParamater As DBParameter
        Dim _strSQL As String

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable

        Try
            _strSQL = " Select DISTINCT nEducationID FROM ExamEducation WHERE (nPatientID =  " & PatientId & ") " _
                   & " AND (nVisitID = " & VisitId & ")"
            strEducationID = oDB.GetRecord_Query(_strSQL)

            If strEducationID <> "" Then
                EducationID = CType(strEducationID, Int64)
            Else
                EducationID = 0
            End If

            If EducationID > 0 Then

                _strSQL = "SELECT ISNULL(sPENotes,'') AS sPENotes, sTemplateName FROM ExamEducation WHERE nEducationID = " & EducationID

                oResultTable = oDB.GetDataTable_Query(_strSQL)
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Public Function GetPatientEduction(ByVal nPatientID As Int64, ByVal nExamID As Int64, ByVal nVisitID As Int64) As DataTable
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            Dim _Query As String
            Dim dtEducation As DataTable = Nothing

            _Query = " SELECT nEducationID, ISNULL(sTemplateName,'') AS sTemplateName, sPENotes FROM ExamEducation " _
                            & " WHERE nPatientID = " & nPatientID & " AND nVisitID = " & nVisitID & " AND isnull(nResourceType,0) =0"

            oDB.Connect(False)
            oDB.Retrive_Query(_Query, dtEducation)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing

            If dtEducation IsNot Nothing Then
                Return dtEducation
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function

    Public Function GetPatientEductionArray(ByVal nPatientID As Int64, ByVal nExamID As Int64, ByVal nVisitID As Int64) As ArrayList

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim dtEducation As DataTable = Nothing

        Try

            Dim arrEducation As New ArrayList
            Dim oList As myList

            dtEducation = GetPatientEduction(nPatientID, nExamID, nVisitID)
            If dtEducation IsNot Nothing Then
                If dtEducation.Rows.Count > 0 Then

                    For iRow As Int32 = 0 To dtEducation.Rows.Count - 1
                        oList = New myList
                        oList.ID = CType(dtEducation.Rows(iRow)("nEducationID"), Int64)
                        oList.Description = dtEducation.Rows(iRow)("sTemplateName")
                        oList.TemplateResult = CType(dtEducation.Rows(iRow)("sPENotes"), Object)
                        arrEducation.Add(oList)
                    Next
                End If
            End If

            Return arrEducation

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(dtEducation) = False Then
                dtEducation.Dispose()
                dtEducation = Nothing
            End If

        End Try

    End Function

    Public Function GetEducation(ByVal EducationID As Int64, ByVal PatientID As Int64) As DataTable

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim dt As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        'Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@EducationID"
            oParamater.Value = EducationID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            dt = oDB.GetDataTable("gsp_ScanPatientEducationDoc")
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Public Function DeleteEducations(ByVal EducationID As Long, ByVal VisitDate As String, ByVal PatientID As Long)

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        'Dim oResultTable As New DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nEducationID"
            oParamater.Value = EducationID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            Dim Status As Boolean = oDB.Delete("gsp_DeletePatientEducation")

            '26-Apr-13 Aniket: Resolving Memory Leaks
            'If Status Then

            '    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Delete, "Patient Education deleted", gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
            '    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Patient Education Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
            'End If

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Delete, "Patient Education deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If


        End Try
        Return Nothing
    End Function

    Public Function FillTemplates() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 100 '' to Fill Patient Education Templates and MU Patient Education
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_FillTemplateGallery_MST")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function



    Public Function FillPatientEducationTemplates(PatientID As Long, Age As Decimal) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Decimal
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Age"
            oParamater.Value = Age
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("Education_GetAllTemplates")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function


    ''00000803: Patient Education. New Parameter added for modify education scenario.
    Public Function SaveExamEducation(ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal nExamID As Int64, ByVal oTemplateResult As Object, ByVal sTemplateName As String, Optional ByVal Source As Integer = 0, Optional ByVal Resourcecategory As Integer = 1, Optional ByVal ResourceType As Integer = 0, Optional ByVal DocumentURL As String = "", Optional ByVal sBibliography As String = "", Optional ByVal sBibliographyDeveloper As String = "", Optional ByVal nModifyEducationId As Long = 0, Optional ByVal ProviderID As Long = 0) As Boolean


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = nVisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = nPatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ExamID"
            oParamater.Value = nExamID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = sTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sPENotes"
            oParamater.Value = oTemplateResult
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Source"
            oParamater.Value = Source
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ResourceCategory"
            oParamater.Value = Resourcecategory
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ResourceType"
            oParamater.Value = ResourceType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DocumentURL"
            oParamater.Value = DocumentURL
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Bibliography"
            If IsNothing(sBibliography) Then
                sBibliography = ""
            End If
            oParamater.Value = sBibliography
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@BibliographyDeveloper"
            If IsNothing(sBibliographyDeveloper) Then
                sBibliographyDeveloper = ""
            End If
            oParamater.Value = sBibliographyDeveloper
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@EducationID"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProviderID"
            oParamater.Value = ProviderID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''00000803: Patient Education. New Parameter added for modify education scenario.
            If nModifyEducationId <> 0 Then
                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Name = "@nModifyEducationId"
                oParamater.Value = nModifyEducationId
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing
            End If

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Output
            oParamater.Name = "@nPatientEducationID"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            _nPatientEducationID = oDB.Add("gsp_InUpExamEducation")



            If nExamID = 0 Then
                If ResourceType = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Add, "Provider Reference Document added", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'Else
                    '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education added", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            Else
                If ResourceType = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Add, "Provider Reference Document viewed", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

        Return Nothing
    End Function

    Public Function SaveExamEducationSmart(ByVal nVisitID As Long, ByVal nPatientID As Long, ByVal nExamID As Int64, ByVal oTemplateResult As Object, ByVal sTemplateName As String, Optional ByVal Source As Integer = 0, Optional ByVal Resourcecategory As Integer = 1, Optional ByVal ResourceType As Integer = 0, Optional ByVal DocumentURL As String = "", Optional ByVal sBibliography As String = "", Optional ByVal sBibliographyDeveloper As String = "") As Boolean

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = nVisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = nPatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ExamID"
            oParamater.Value = nExamID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = sTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sPENotes"
            oParamater.Value = oTemplateResult
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Source"
            oParamater.Value = Source
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ResourceCategory"
            oParamater.Value = Resourcecategory
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ResourceType"
            oParamater.Value = ResourceType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@DocumentURL"
            oParamater.Value = DocumentURL
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Bibliography"
            If IsNothing(sBibliography) Then
                sBibliography = ""
            End If
            oParamater.Value = sBibliography
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@BibliographyDeveloper"
            If IsNothing(sBibliographyDeveloper) Then
                sBibliographyDeveloper = ""
            End If
            oParamater.Value = sBibliographyDeveloper
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@EducationID"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Add("gsp_InUpExamEducationSmart")

            If nExamID = 0 Then
                If ResourceType = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Add, "Provider Reference Document added", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'Else
                    '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.Add, "Patient Education added", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            Else
                If ResourceType = 2 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ProviderReference, gloAuditTrail.ActivityType.Add, "Provider Reference Document viewed", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientEducation, gloAuditTrail.ActivityType.View, "Patient Education viewed", nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            '26-Apr-13 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

        Return Nothing
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

                If IsNothing(Ds) = False Then
                    Ds.Dispose()
                    Ds = Nothing
                End If

                If IsNothing(Dv) = False Then
                    Dv.Dispose()
                    Dv = Nothing
                End If

                If IsNothing(Tb) = False Then
                    Tb.Dispose()
                    Tb = Nothing
                End If

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region


    Function GetSpeNotesFromMaster(TemplateID As Long) As Object
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        '26-Apr-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As Object = Nothing

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oResultTable = oDB.GetDataValue("GetSpeNotesFromMasterUsingTemplateID")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally


            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Public Function GetEducationMaterialUsingTempalteID(TemplateID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

          

            oResultTable = oDB.GetDataTable("GetEducationMaterialUsingTempalteID")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally


            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Function CheckISPresentInExamEducation(PatientID As Long, TemplateName As String, VisitID As Long, Source As Int16, ResourceCategory As Int16, ResourceType As Int16) As Boolean

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter


        Dim oResultTable As Boolean = False

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sTemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nVisitID"
            oParamater.Value = VisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nSource"
            oParamater.Value = Source
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nResourceCategory"
            oParamater.Value = ResourceCategory
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nResourceType"
            oParamater.Value = ResourceType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = Convert.ToBoolean(oDB.GetDataValue("CheckISPresentInExamEducation"))
            Return oResultTable

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally


            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Function GetEducationMaterialUsingTempalteID(PatientID As Long, TemplateName As String, VisitID As Long, Source As Integer, ResourceCategory As Integer, ResourceType As Integer) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter


        Dim oResultTable As DataTable

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sTemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nVisitID"
            oParamater.Value = VisitID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nSource"
            oParamater.Value = Source
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nResourceCategory"
            oParamater.Value = ResourceCategory
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nResourceType"
            oParamater.Value = ResourceType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

           

            oResultTable = oDB.GetDataTable("GetEducationMaterialUsingAllInforamtion")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally


            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Function GetEducationMaterialUsingEducationID(EducationID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nEducationID"
            oParamater.Value = EducationID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("GetEducationMaterialUsingEducationID")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally


            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function

End Class
