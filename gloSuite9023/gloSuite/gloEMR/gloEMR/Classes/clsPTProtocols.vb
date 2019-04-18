Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class clsPTProtocols

    ' Private ds As New System.Data.DataSet
    Private dv As DataView
    Private dt As DataTable

    Public ReadOnly Property GetDataTable() As DataTable
        Get
            Return dt
        End Get
    End Property

    'Public ReadOnly Property DsDataSet() As DataSet
    '    Get
    '        Return ds
    '    End Get
    'End Property

    Public ReadOnly Property DsDataview() As DataView
        Get
            Return dv
        End Get
    End Property
    Private _PTProtocols As String = "PTProtocols"
    Public ReadOnly Property PatientPTProtocols() As String
        Get
            Return _PTProtocols
        End Get
    End Property

    '''' <summary>
    '''' To get All Protocols(s) for Selected Patient
    '''' </summary>
    '''' <param name="PatientID"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    Public Function GetAllPTProtocols(ByVal PatientID As Long) As DataView
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If

            dt = oDB.GetDataTable("gsp_ViewPTProtocol")
            If Not dv Is Nothing Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not dt Is Nothing Then
                dv = dt.Copy().DefaultView
                Return dv
            End If
            Return Nothing
        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    ' '' To Fill ComboBox Template
    'Public Function FillTemplates() As DataTable
    '    Try
    '        Dim da As New SqlDataAdapter
    '        Dim dt As New DataTable

    '        cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Con)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        Dim objParam As SqlParameter

    '        objParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
    '        objParam.Direction = ParameterDirection.Input
    '        objParam.Value = 12 '' to Fill 'PTProtocol' Templates

    '        da.SelectCommand = cmd
    '        da.Fill(dt)

    '        Return dt
    '    Catch ex As Exception
    '    Finally
    '        Con.Close()
    '    End Try

    'End Function

    Public Function SavePTProtocol(ByVal ProtocolID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal ProtocolDate As Date, ByVal strTempFilePath As String, ByVal strTemplateName As String, ByVal IsFinished As Boolean, ByVal TemplateName As String) As Long
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Protocol"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProtocolDate"
            oParamater.Value = ProtocolDate
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
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.BigInt
            'oParamater.Direction = ParameterDirection.InputOutput
            'oParamater.Name = "@ProtocolID"
            'oParamater.Value = ProtocolID
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing


            ''Sandip Darade 20100422
            ''Case GLO2010-0004880
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@ProtocolID"
            oParamater.Value = ProtocolID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            If IsFinished = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Finish, "PT Protocol finished", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                
            Else
                If ProtocolID = 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Add, "PT Protocol added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "PTProtocol Added", gstrLoginName, gstrClientMachineName, gnPatientID)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Modify, "PT Protocol modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "PTProtocol Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                End If

            End If


            ProtocolID = oDB.Add("gsp_InUpPTProtocol")
            Return ProtocolID

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Return 0
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    Public Function SavePTProtocolBytes(ByVal ProtocolID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal ProtocolDate As Date, ByVal bBytes As Object, ByVal strTemplateName As String, ByVal IsFinished As Boolean, ByVal TemplateName As String) As Long
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@PatientID"
            oParamater.Value = PatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Protocol"
            ' Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
            End If


            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            'objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProtocolDate"
            oParamater.Value = ProtocolDate
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
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            'oParamater = New DBParameter
            'oParamater.DataType = SqlDbType.BigInt
            'oParamater.Direction = ParameterDirection.InputOutput
            'oParamater.Name = "@ProtocolID"
            'oParamater.Value = ProtocolID
            'oDB.DBParametersCol.Add(oParamater)
            'oParamater = Nothing


            ''Sandip Darade 20100422
            ''Case GLO2010-0004880
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@ProtocolID"
            oParamater.Value = ProtocolID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            If IsFinished = True Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Finish, "PT Protocol finished", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            Else
                If ProtocolID = 0 Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Add, "PT Protocol added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "PTProtocol Added", gstrLoginName, gstrClientMachineName, gnPatientID)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Modify, "PT Protocol modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "PTProtocol Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                End If

            End If


            ProtocolID = oDB.Add("gsp_InUpPTProtocol")
            Return ProtocolID

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            Return 0
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    '''' <summary>
    '''' To Select Patient PTProtocol 
    '''' </summary>
    '''' <param name="ProtocolID"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    Public Function ScanPTProtocol(ByVal ProtocolID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProtocolID"
            oParamater.Value = ProtocolID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ScanPTProtocol")
            If Not dt Is Nothing Then
                Return dt
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    '''' <summary>
    ''''  To Delete Patient PTProtocol of ID LetterID
    '''' </summary>
    '''' <param name="ProtocolID"></param>
    '''' <param name="ProtocolDate"></param>
    '''' <param name="ProtocolName"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    Public Function DeletePTProtocol(ByVal ProtocolID As Long, ByVal ProtocolDate As String, ByVal ProtocolName As String, ByVal PatientID As Long)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ProtocolID"
            oParamater.Value = ProtocolID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oDB.Delete("gsp_DeletePTProtocol")

            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "'" & ProtocolName & "' PT Protocol Deleted Dated '" & ProtocolDate & "'", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.PTProtocol, gloAuditTrail.ActivityType.Delete, "PT Protocol deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'" & ProtocolName & "' PT Protocol Deleted Dated '" & ProtocolDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Dispose()
            oDB = Nothing

        End Try
        Return Nothing
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

    'Public Function GenerateVisitID(ByVal nPatientID As Long, ByVal ProtocolDate As Date) As Long
    '    Dim cmdVisits As SqlCommand
    '    Dim objParamFlag As SqlParameter
    '    Dim nVisitID As Long
    '    Dim objParam As SqlParameter
    '    cmdVisits = New SqlCommand("gsp_InsertVisits", Con)
    '    cmdVisits.CommandType = CommandType.StoredProcedure
    '    Con.ConnectionString = GetConnectionString()
    '    Con.Open()
    '    objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.Int)
    '    objParam.Direction = ParameterDirection.Input
    '    'objParam.Value = objPrescription.PatientID
    '    objParam.Value = nPatientID

    '    objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
    '    objParam.Direction = ParameterDirection.Input
    '    objParam.Value = ProtocolDate

    '    'Retrieve Appointment ID
    '    Dim nAppointmentID As Integer
    '    Dim objAppointmentID As New clsAppointments
    '    nAppointmentID = objAppointmentID.GetPatientAppointment(System.DateTime.Now, gnPatientID)
    '    objAppointmentID = Nothing


    '    objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.Int)
    '    objParam.Direction = ParameterDirection.Input
    '    objParam.Value = nAppointmentID

    '    objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '    objParam.Direction = ParameterDirection.Input
    '    objParam.Value = GetPrefixTransactionID

    '    objParam = cmdVisits.Parameters.Add("@VisitID", 0)
    '    objParam.Direction = ParameterDirection.Output
    '    'objParam.Value = 0

    '    objParamFlag = cmdVisits.Parameters.Add("@Flag", SqlDbType.Int)
    '    objParamFlag.Direction = ParameterDirection.Output

    '    cmdVisits.ExecuteNonQuery()
    '    nVisitID = objParam.Value
    '    If objParamFlag.Value = 0 Then
    '        Dim objAudit As New clsAudit
    '        objAudit.CreateLog(clsAudit.enmActivityType.Add, "Visit Added on " & CType(Now, String), gstrLoginName, gstrClientMachineName)
    '        Con.Close()
    '    Else
    '        'USe existing visit
    '    End If
    '    Con.Close()
    '    Return nVisitID
    'End Function
    Public Function Fill_LockPTProtocol(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Sub New()

    End Sub
    Public Sub Dispose()
        If Not dt Is Nothing Then
            dt.Dispose()
            dt = Nothing
        End If
        If Not dv Is Nothing Then
            dv.Dispose()
            dv = Nothing
        End If
    End Sub
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
            oParameter.Value = PatientPTProtocols
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
            oParamater.Value = PatientPTProtocols
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
