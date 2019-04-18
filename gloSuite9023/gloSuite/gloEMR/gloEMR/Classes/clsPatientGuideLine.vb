Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord

Public Class clsPatientGuideLine

  
    'Private Ds As System.Data.DataSet
    'Private Dv As DataView
    'Private Tb As DataTable


    'Public ReadOnly Property DsDataSet() As DataSet
    '    Get
    '        Return Ds
    '    End Get
    'End Property

    'Public ReadOnly Property DsDataview() As DataView
    '    Get
    '        Return Dv
    '    End Get
    'End Property
    'function commented as not in use.
    'Public Function GetAllEducations() As DataView

    '    '' I/P =PatientID
    '    '' O/P = Dataview
    '    ''      nEducationID
    '    ''      sTemplateName
    '    ''      nVisitID
    '    ''      dtVisitDate

    '    Dim oDB As New DataBaseLayer
    '    Dim oParamater As DBParameter

    '    Dim oResultTable As New DataTable
    '    Try
    '        oParamater = New DBParameter
    '        oParamater.DataType = SqlDbType.BigInt
    '        oParamater.Direction = ParameterDirection.Input
    '        oParamater.Name = "@PatientID"
    '        oParamater.Value = gnPatientID
    '        oDB.DBParametersCol.Add(oParamater)
    '        oParamater = Nothing
    '        oResultTable = oDB.GetDataTable("gsp_ViewPatientEducation")
    '        If Not oResultTable Is Nothing Then
    '            Dv = New DataView(oResultTable)
    '            Return Dv
    '        Else
    '            Return Nothing
    '        End If
    '    Catch ex As Exception

    '        MessageBox.Show(ex.ToString, "Patient Guidelines", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    Finally
    '        oDB.Dispose()
    '    End Try
    'End Function

    Public Sub DeleteEducations(ByVal EducationID As Long, ByVal VisitDate As String)

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nEducationID"
            oParamater.Value = EducationID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeletePatientEducation")

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Delete, VisitDate & " Dated Patient Education Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Delete, VisitDate & " Dated Patient Education Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, VisitDate & " Dated Patient Education Deleted", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Guidelines", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub

    Public Function FillTemplates(ByVal Type As frmPatientGuideline.MaterialType) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            If Type = frmPatientGuideline.MaterialType.GuideLine Then
                oParamater.Value = 13 '' @flag =13 for "Wellness Guidelines" 
            ElseIf Type = frmPatientGuideline.MaterialType.DiseaseManagement Then
                oParamater.Value = 14 '' @flag =14 for "Disease Management" 
            End If
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("gsp_FillTemplateGallery_MST")
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
        End Try
    End Function

    'Public Function GetTemplate(ByVal TemplateID As Long) As DataTable
    '    Try
    '        Dim adpt As New SqlDataAdapter
    '        Dim dt As New DataTable

    '        Cmd = New SqlCommand("gsp_GetExamContents", Con)
    '        Cmd.CommandType = CommandType.StoredProcedure

    '        Dim objParam As SqlParameter
    '        objParam = Cmd.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = TemplateID

    '        adpt.SelectCommand = Cmd
    '        adpt.Fill(dt)
    '        Con.Close()
    '        Return dt
    '    Catch ex As Exception
    '        If Con.State = ConnectionState.Open Then
    '            Con.Close()
    '        End If
    '    End Try
    'End Function

    Public Sub SaveExamGuidelineBytes(ByVal TransID As Long, ByVal VisitID As Long, ByVal PatientID As Long, ByVal bBytes As Object, ByVal strTemplateName As String, ByVal Type As frmPatientGuideline.MaterialType)
        '' Save Patient Material 
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = VisitID
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
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Template"
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If
            

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Type"
            oParamater.Value = Type
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Add("gsp_InUpPatientMaterial")


            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Material Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Material Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Patient Material Added", gstrLoginName, gstrClientMachineName, gnPatientID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Guidelines", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub
    Public Sub SaveExamGuideline(ByVal TransID As Long, ByVal VisitID As Long, ByVal PatientID As Long, ByVal strTempFilePath As String, ByVal strTemplateName As String, ByVal Type As frmPatientGuideline.MaterialType)
        '' Save Patient Material 
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@VisitID"
            oParamater.Value = VisitID
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
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Template"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)
            objword = Nothing

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = strTemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Type"
            oParamater.Value = Type
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Add("gsp_InUpPatientMaterial")


            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Material Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Guidelines, gloAuditTrail.ActivityType.Add, "Patient Material Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Patient Material Added", gstrLoginName, gstrClientMachineName, gnPatientID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Guidelines", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
        End Try
    End Sub

    'Public Function getData(ByVal strFields As String) As String
    '    Try
    '        Dim strData As String
    '        Dim objCmd As New SqlCommand
    '        Dim objSQLDataReader As SqlDataReader
    '        Dim sqlParam As SqlParameter
    '        objCmd.CommandType = CommandType.StoredProcedure
    '        objCmd.CommandText = "gsp_GetFieldsdata"
    '        objCmd.Parameters.Clear()
    '        'cn.ConnectionString = GetConnectionString()
    '        Con.Open()
    '        sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = strFields

    '        sqlParam = objCmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = gnPatientID

    '        sqlParam = objCmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = 0

    '        objCmd.Connection = Con

    '        objSQLDataReader = objCmd.ExecuteReader
    '        If objSQLDataReader.HasRows = True Then
    '            objSQLDataReader.Read()
    '            If IsDBNull(objSQLDataReader.Item(0)) = False Then
    '                strData = objSQLDataReader.Item(0)
    '            End If
    '        End If
    '        objSQLDataReader.Close()
    '        Con.Close()
    '        Return strData
    '    Catch ex As Exception
    '        If Con.State = ConnectionState.Open Then
    '            Con.Close()
    '        End If
    '    End Try
    'End Function
    Public Function FetchTemplateforGuideLine(ByVal PatientID As Int64, ByVal VisitId As Int64, ByVal Type As Int32) As DataTable

        Dim oDB As New DataBaseLayer
        Dim strEducationID As String = ""


        Dim _strSQL As String
        Dim oResultTable As DataTable = Nothing
        Try
            _strSQL = " Select  nID, ISNULL(sTemplate,'') AS sTemplate , sTemplateName FROM PatientMaterial WHERE nPatientID =  " & PatientID & " " _
                & " AND nVisitID = " & VisitId & " AND nType = " & Type & ""


            oResultTable = oDB.GetDataTable_Query(_strSQL)
            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Guideline", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

End Class

