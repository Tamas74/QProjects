Imports System.Data.SqlClient
Public Class clsCPTICD9Association

    Public Sub New()
        Try
            Dim sqlconn As String
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPTICD9Association -- New -- " & ex.ToString)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            UpdateLog("clsCPTICD9Association -- New -- " & ex.ToString)
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Conn As SqlConnection = Nothing
    'Private Dv As DataView = Nothing
    ' Private Cmd As System.Data.SqlClient.SqlCommand = Nothing
    Public Sub Dispose()

       
        'slr free Con
        If Not IsNothing(Conn) Then
            Conn.Dispose()
            Conn = Nothing
        End If

    End Sub
    Public Function AddData(ByVal CPTID As Long, ByVal CPTName As String, ByVal arrlist As ArrayList) As Boolean
        '' Delete Insert Method

        Conn.Open()
        Dim trCPTAssociation As SqlTransaction
        trCPTAssociation = Conn.BeginTransaction
        Dim cmddelete As SqlCommand = Nothing
        Dim Cmd As SqlCommand = Nothing

        Dim objparam As SqlParameter = Nothing
        Try
            Dim i As Integer


            ''Commented Rahul on 20101014
            ' '' Delete CPTICD9 of selected CPT
            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteCPTICD9", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trCPTAssociation

            'objparam = cmddelete.Parameters.Add("@nCPTID", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = CPTID

            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()
            ''''''''''
            ' '' Delete CPTDrugs of selected CPT
            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteCPTDrugs", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trCPTAssociation

            'objparam = cmddelete.Parameters.Add("@nCPTID", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = CPTID

            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()
            ''''''''''''''''''''''''''
            ' '' Delete CPTPE of selected CPT
            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteCPTPE", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trCPTAssociation

            'objparam = cmddelete.Parameters.Add("@nCPTID", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = CPTID

            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()
            '''''''''''''''''''''
            ' '' Delete CPTSN of selected CPT
            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteCPTSN", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trCPTAssociation

            'objparam = cmddelete.Parameters.Add("@nCPTID", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = CPTID

            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()
            '''''

            ''Added Rahul on 20101014
            '' Delete CPTICD9 of selected CPT
            cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteFromAllCPT", Conn) 'gsp_DeleteCPTICD9
            cmddelete.CommandType = CommandType.StoredProcedure
            cmddelete.Transaction = trCPTAssociation

            objparam = cmddelete.Parameters.Add("@nCPTID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = CPTID

            cmddelete.ExecuteNonQuery()
            cmddelete.Parameters.Clear()
            cmddelete.Dispose()
            cmddelete = Nothing
            '''''''''
            '' Delete CPTDrugs of selected CPT
            ''End

            ''''' Insert Data from ArrayList
            
            For i = 0 To arrlist.Count - 1
                Dim objmylist As myList
                objmylist = CType(arrlist.Item(i), myList)

                'Insert data in CPTICD9
                If objmylist.Description = "i" Then

                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTICD9", Conn)

                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@nICD9ID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    objparam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.nICDRevision

                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "ICD9 Associated with CPT " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101008
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "ICD9 Associated with CPT " & CType(CPTName, String), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'Dim objAudit As New clsAudit
                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "ICD9 Associated with CPT " & CType(CPTName, String), gstrLoginName, gstrClientMachineName)
                    'objAudit = Nothing

                    ''''' Insert Data in CPTDrugs
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                ElseIf objmylist.Description = "d" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTDrugs", Conn)

                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@nDrugsID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    'For De-Normalization
                    objparam = Cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.DrugName

                    objparam = Cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Dosage

                    objparam = Cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.DrugForm

                    'Route
                    objparam = Cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Route

                    'Frequency
                    objparam = Cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Frequency

                    'NDCCode
                    objparam = Cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.NDCCode

                    'IsNarcotic
                    objparam = Cmd.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.IsNarcotic

                    'Duration
                    objparam = Cmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Duration

                    objparam = Cmd.Parameters.Add("@mpid", SqlDbType.Int)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.mpid

                    'DrugQtyQualifier
                    objparam = Cmd.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.DrugQtyQualifier
                    'For De-Normalization

                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.ItemChecked
                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Drugs Associated with CPT " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101008
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Drugs Associated with CPT " & CType(CPTName, String), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'Dim objAudit As New clsAudit
                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Drugs Associated with CPT " & CType(CPTName, String), gstrLoginName, gstrClientMachineName)
                    'objAudit = Nothing

                    ''''' Insert Data in CPTPatient Education
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                ElseIf objmylist.Description = "p" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTPE", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@nPEID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Patient Education Associated with Patient Education " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101008
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Patient Education Associated with Patient Education " & CType(CPTName, String), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'Dim objAudit As New clsAudit
                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Patient Education Associated with Patient Education " & CType(CPTName, String), gstrLoginName, gstrClientMachineName)
                    'objAudit = Nothing

                    ''''' Insert Data in CPTSN Short Notes
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                ElseIf objmylist.Description = "t" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTSN", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@nSNID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    ''''''''''''''' Added by Ujwala - Smart Treatment Changes  - as on 20101012
                    '' Chetan Added on 13-Nov 2010
                    objparam = Cmd.Parameters.Add("@sTemplateName", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Comments
                    '' Chetan Added on 13-Nov 2010

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101008
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'Dim objAudit As New clsAudit
                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), gstrLoginName, gstrClientMachineName)
                    'objAudit = Nothing

                    ''Added Rahul on 20101014
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                ElseIf objmylist.Description = "f" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTFlowsheet", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@nFlowsheetID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index


                    objparam = Cmd.Parameters.Add("@Status", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                ElseIf objmylist.Description = "r" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTReff_Letter", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index


                    objparam = Cmd.Parameters.Add("@Status", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing

                ElseIf objmylist.Description = "l" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTLab_Order", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@labtm_ID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index


                    objparam = Cmd.Parameters.Add("@Status", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing

                ElseIf objmylist.Description = "o" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTLm_Test", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trCPTAssociation

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CPTID

                    objparam = Cmd.Parameters.Add("@lm_test_id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index


                    objparam = Cmd.Parameters.Add("@Status", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), 0, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''

                    'Dim objAudit As New clsAudit
                    'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Short Notes Associated with CPT " & CType(CPTName, String), gstrLoginName, gstrClientMachineName)
                    'objAudit = Nothing
                    ''End
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If
                'Cmd.Parameters.Clear()
            Next

            'If intMode = 1 Then 'Add
            '    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Medication for Date " & Now & " Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            'ElseIf intMode = 2 Then 'Modify
            '    objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Medication for Date " & objMedication.PrescriptionDate & " Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            'End If
            'objAudit = Nothing

            trCPTAssociation.Commit()
            Conn.Close()
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPTICD9Association -- AddData -- " & ex.ToString)
            'trMedication.Rollback()
            trCPTAssociation.Rollback()
            trCPTAssociation = Nothing
            Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPTICD9Association -- AddData -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'trMedication.Rollback()
            trCPTAssociation.Rollback()
            trCPTAssociation = Nothing
            Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            Return False
        Finally
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Conn.Close()
            If Not IsNothing(trCPTAssociation) Then
                trCPTAssociation.Dispose()
                trCPTAssociation = Nothing
            End If

            If objparam IsNot Nothing Then
                objparam = Nothing
            End If
        End Try
    End Function
    'Public ReadOnly Property DsDataview() As DataView
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return Dv
    '        'Return Ds
    '    End Get

    'End Property

    'Public Sub SortDataview(ByVal strsort As String)
    '    Dv.Sort = strsort
    'End Sub

    Public Function GetBillableICD10Codes(ByVal ParentICDCode As String) As DataTable

        Dim Cmd As SqlCommand = Nothing        
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("ICD10_GetBillableCodesUnderParent", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@ParentCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ParentICDCode

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            If (IsNothing(sqladpt) = False) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPTICD9Association -- FetchICD9forUpdate -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPTICD9Association -- FetchICD9forUpdate -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Conn.Close()
        End Try

    End Function

    Public Function FillControls(ByVal id As frmCPTICD9Association.Associates, Optional ByVal strsearch As String = "") As DataTable
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim Cmd As SqlCommand = Nothing

        Try

            If id = frmCPTICD9Association.Associates.Drugs Then
                ''Sandip darade 20090525
                '' 'gsp_FillDrugs_Mst' pulls top 40 records replace it with 'gsp_FillAllDrugs_Mst' pulling all records

                ''Cmd = New SqlCommand("gsp_FillDrugs_Mst", Conn)

                Cmd = New SqlCommand("gsp_FillAllDrugs_Mst", Conn)

                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = LCase(strsearch)

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                'For De-Normalization
                objParam.Value = 16  ''4
                'For De-Normalization
            ElseIf id = frmCPTICD9Association.Associates.CPT Then
                Cmd = New SqlCommand("gsp_FillCPTCategory_MST", Conn)

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                If strsearch = "DESC" Then
                    objParam.Value = 1 '' Sort Dy Description
                Else
                    objParam.Value = 0 '' Sort Dy CODE (DEFAULT)
                End If

                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

            ElseIf id = frmCPTICD9Association.Associates.MUPE Then
                ''''' For Fill Templates of Patient Education
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 101  '' flag to fill MU Patient education
            ElseIf id = frmCPTICD9Association.Associates.PE Then
                ''''' For Fill Templates of Patient Education
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 99  '' flag to fill Patient education

            ElseIf id = frmCPTICD9Association.Associates.Tags Then  '' For Tags
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 4   '' flag to fill Tags

                ''Added Rahul for new Association (Referral Letter,Order,LabOrder,Flowsheet) on 20101014

            ElseIf id = frmCPTICD9Association.Associates.Referral Then  '' For refferal letter 
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 10   '' flag to fill refferal letter

            ElseIf id = frmCPTICD9Association.Associates.Flow Then  '' For flowsheet
                ' Cmd = New SqlCommand("select nFlowSheetId,sFlowSheetName from FlowSheet_MST", Conn)

                Dim strquery As String = " SELECT DISTINCT nFlowSheetID AS nFlowSheetID, sFlowSheetName FROM FlowSheet_MST " _
                  & "  ORDER BY sFlowSheetName"
                '& " UNION SELECT DISTINCT 0 AS nFlowSheetID, sFlowSheetName FROM FlowSheet1 " _
                '& " WHERE sFlowSheetName not in (SELECT DISTINCT  sFlowSheetName FROM FlowSheet_MST) " _

                Cmd = New SqlCommand(strquery, Conn)
                adpt.SelectCommand = Cmd

            ElseIf id = frmCPTICD9Association.Associates.Order Then  '' For Orders (Radiology Orders)

                Dim strSQL As String
                strSQL = " SELECT   LM_Test.lm_test_ID as lm_test_ID, LM_Test.lm_test_Name as lm_test_Name FROM   LM_Test INNER JOIN " _
                & "LM_Test AS LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID INNER JOIN " _
                & " LM_Category ON LM_Test_1.lm_test_CategoryID = LM_Category.lm_category_ID "

                Cmd = New SqlCommand(strSQL, Conn)
                adpt.SelectCommand = Cmd

            ElseIf id = frmCPTICD9Association.Associates.LabOrder Then  '' Lab Orders

                Dim strSQL As String
                strSQL = "SELECT  labtm_id, labtm_Name FROM Lab_Test_Mst "

                Cmd = New SqlCommand(strSQL, Conn)
                adpt.SelectCommand = Cmd
                ''End
            ElseIf id = frmCPTICD9Association.Associates.ICD9 Then  '' For ICD9

                Cmd = New SqlCommand("gsp_Diagnosis_Search", Conn)

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@SearchString", SqlDbType.Text)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strsearch

                Dim objParam1 As SqlParameter
                objParam1 = Cmd.Parameters.Add("@nICDRevision", SqlDbType.Int)
                objParam1.Direction = ParameterDirection.Input
                objParam1.Value = gloGlobal.gloICD.CodeRevision.ICD9


                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd

                'Cmd = New SqlCommand("gsp_FillICD9", Conn)

                'Dim objParam As SqlParameter
                'objParam = Cmd.Parameters.Add("@flag1", SqlDbType.Int)
                'objParam.Direction = ParameterDirection.Input
                'If strsearch = "DESC" Then
                '    objParam.Value = 1 '' Sort Dy Description
                'Else
                '    objParam.Value = 0 '' Sort Dy CODE (DEFAULT)
                'End If

                'Cmd.CommandType = CommandType.StoredProcedure
                'adpt.SelectCommand = Cmd
            ElseIf id = frmCPTICD9Association.Associates.ICD10 Then  '' For ICD9

                Cmd = New SqlCommand("ICD10_GetTreatmentCodes", Conn)
                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@SearchText", SqlDbType.Text)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strsearch
                'Dim objParam1 As SqlParameter
                'objParam1 = Cmd.Parameters.Add("@nICDRevision", SqlDbType.Int)
                'objParam1.Direction = ParameterDirection.Input
                'objParam1.Value = gloGlobal.gloICD.CodeRevision.ICD10
                Cmd.CommandType = CommandType.StoredProcedure
                adpt.SelectCommand = Cmd
                'objParam = Nothing
                'objParam1 = Nothing

            End If

            adpt.Fill(dt)
            Conn.Close()
            Return dt

            'Dim dreader As SqlDataReader
            'Conn.Open()
            'dreader = Cmd.ExecuteReader()

            'Do While dreader.Read
            '    Dim i As Integer
            '    i = dreader("nSpecialtyID")

            'Loop

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPTICD9Association -- FillControls -- " & ex.ToString)
            Return Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPTICD9Association -- FillControls -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(adpt) = False) Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Conn.Close()
        End Try

    End Function
    Public Function FetchICD9forUpdate(ByVal CPTID As Long) As DataTable
        Dim Cmd As SqlCommand = Nothing

        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanCPTICD9Association", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@CPTID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPTID

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            If (IsNothing(sqladpt) = False) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("clsCPTICD9Association -- FetchICD9forUpdate -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsCPTICD9Association -- FetchICD9forUpdate -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Conn.Close()
        End Try

    End Function
    'Shubhangi 20091212
    'Retrive all associated CPTs ...
    Public Function FetchassociatedCPT() As DataTable
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable
            Dim ad As SqlDataAdapter
            'Check connection states
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_AssociatedCPT", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            ad = New SqlDataAdapter(Cmd)
            ad.Fill(dt)
            ''Sanjog 2011 jan 14 to handle con.open() error
            Conn.Close()
            ''Sanjog 2011 jan 14 to handle con.open() error
            If (IsNothing(ad) = False) Then
                ad.Dispose()
                ad = Nothing
            End If
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        Finally
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Conn.Close()
        End Try
    End Function



End Class
