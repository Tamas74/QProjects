Imports System.Data.SqlClient

Public Class ClsICD9AssociationDBLayer
    Implements IDisposable
    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

    End Sub
    Private Conn As SqlConnection
    'Private Dv As DataView
    '  Private Cmd As System.Data.SqlClient.SqlCommand
    ' Private ArrMedicationCol As New ArrayList
    Public Function AddData(ByVal ICD9code As Long, ByVal ICD9Name As String, ByVal arrlist As ArrayList, Optional ByVal strICDIds As String = "", Optional ByVal TreatmentPlanName As String = "", Optional ByVal nProviderID As Long = 0, Optional ByVal IsCopySmartDx As Boolean = False, Optional ByVal strICDIsName As String = "") As Boolean
        Conn.Open()
        Dim trICD9Association As SqlTransaction
        trICD9Association = Conn.BeginTransaction
        Dim cmddelete As SqlCommand = Nothing
        Dim objparam As SqlParameter
        Try
            Dim i As Integer


            ''Commented Rahul on 20101013
            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteICD9CPT", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trICD9Association

            'objparam = cmddelete.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = ICD9code



            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()


            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteICD9Drugs", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trICD9Association

            'objparam = cmddelete.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = ICD9code


            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()


            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteICD9PE", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trICD9Association

            'objparam = cmddelete.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = ICD9code

            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()

            '''''' By Mahesh
            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteICD9SN", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trICD9Association

            'objparam = cmddelete.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = ICD9code

            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()
            ''

            ''Added Rahul for Delete all Association on 20101013
            Dim Cmd As SqlCommand = Nothing
            If ICD9code > 0 AndAlso Not IsCopySmartDx Then

                cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteFromAllICD9", Conn)
                cmddelete.CommandType = CommandType.StoredProcedure
                cmddelete.Transaction = trICD9Association

                objparam = cmddelete.Parameters.Add("@nICD9ID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = ICD9code

                cmddelete.ExecuteNonQuery()
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Modify, "'" & TreatmentPlanName & "'  smartdiagnosis setup added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            Else
                cmddelete = New System.Data.SqlClient.SqlCommand("gsp_InsertSmartDxMaster", Conn)
                cmddelete.CommandType = CommandType.StoredProcedure
                cmddelete.Transaction = trICD9Association

                objparam = cmddelete.Parameters.Add("@sSmartDxName", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = TreatmentPlanName

                objparam = cmddelete.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = nProviderID

                objparam = cmddelete.Parameters.Add("@nSmartDxID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.InputOutput
                objparam.Value = 0

                cmddelete.ExecuteNonQuery()
                If cmddelete.Parameters(2).Value IsNot Nothing Then
                    ICD9code = Convert.ToInt64(cmddelete.Parameters(2).Value)
                End If
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
                If IsCopySmartDx Then
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Copy, "'" & TreatmentPlanName & "'  smartdiagnosis setup added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                Else
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "'" & TreatmentPlanName & "'  smartdiagnosis setup added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                End If

            End If

            ''''Insert SmartDxICDAssocaitions


            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertSmartDxICDAssociation", Conn)

            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Transaction = trICD9Association

            objparam = Cmd.Parameters.Add("@nSmartDxID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ICD9code

            objparam = Cmd.Parameters.Add("@sICD9ID", SqlDbType.VarChar, System.Int32.MaxValue)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = strICDIds

            objparam = Cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = nProviderID
            'Insert data in ICD9Drugs
            objparam = Cmd.Parameters.Add("@sSmartDxName", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = TreatmentPlanName
            Cmd.ExecuteNonQuery()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "'" & strICDIsName & "'  ICD Added into '" & TreatmentPlanName & "' smartdiagnosis setup", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to CPT", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009

            ''
            ''''ENDInsert SmartDxICDAssocaitions

            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If




            For i = 0 To arrlist.Count - 1
                Dim objmylist As myList
                objmylist = CType(arrlist.Item(i), myList)

                'Dim Cmd As SqlCommand = Nothing

                'Insert data in ICD9CPT
                If objmylist.Description = "c" Then

                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9CPT", Conn)

                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@nCPTID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                    'Insert data in ICD9Drugs

                    Cmd.ExecuteNonQuery()

                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to CPT", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to CPT for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "ICD9 Association Added to CPT", gstrLoginName, gstrClientMachineName)

                ElseIf objmylist.Description = "d" Then

                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9Drugs", Conn)

                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@nDrugsID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    '\\added by suraj on 20081226
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

                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.ItemChecked
                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                    'For De-Normalization

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Drugs", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Drugs for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "ICD9 Association Added to Drugs", gstrLoginName, gstrClientMachineName)
                    'Insert data in ICD9Patient Education
                ElseIf objmylist.Description = "p" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9PE", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@nPEID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011

                    Cmd.ExecuteNonQuery()

                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Patient Education", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Patient Education for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "ICD9 Association Added to Patient Education", gstrLoginName, gstrClientMachineName)
                    'Insert data in ICD9 Short Notes (ICD9SN)
                ElseIf objmylist.Description = "t" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9SN", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@nSNID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                    objparam = Cmd.Parameters.Add("@flag", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                    '' Chetan Added on 13-Nov 2010
                    objparam = Cmd.Parameters.Add("@sTemplateName", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Comments
                    '' Chetan Added on 13-Nov 2010
                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Short Note", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Association Added to Short Note for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "ICD9 Associatin Added to Short Note", gstrLoginName, gstrClientMachineName)

                    ''Added Rahul on 20101013
                ElseIf objmylist.Description = "f" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9Flowsheet", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@nflowsheetid", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Flowsheet", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Flowsheet for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, True)
                    ''

                ElseIf objmylist.Description = "l" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9LabOrders", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@labtm_ID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Flowsheet", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Flowsheet for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, True)
                    ''

                ElseIf objmylist.Description = "r" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9Referral_Letter", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@ntemplateID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type
                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Referral Letter", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Referral Letter for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, True)
                    ''

                ElseIf objmylist.Description = "o" Then
                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertICD9_Lm_Test", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trICD9Association

                    objparam = Cmd.Parameters.Add("@nICD9Id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ICD9code

                    objparam = Cmd.Parameters.Add("@lm_test_id", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Index

                    objparam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = objmylist.Type

                    Cmd.ExecuteNonQuery()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Referral Letter", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, "ICD9 Associatin Added to Referral Letter for '" & TreatmentPlanName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, True)
                    ''
                    ''
                End If
                If (IsNothing(Cmd) = False) Then
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If


            Next

            'If intMode = 1 Then 'Add
            '    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Medication for Date " & Now & " Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            'ElseIf intMode = 2 Then 'Modify
            '    objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Medication for Date " & objMedication.PrescriptionDate & " Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            'End If
            'objAudit = Nothing

            trICD9Association.Commit()
            Conn.Close()
            Return True

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsICD9AssociationDBLayer - AddData : " & ex.ToString)
            trICD9Association.Rollback()
            '            trICD9Association = Nothing
            '   Cmd = Nothing
            '  cmddelete = Nothing
            Conn.Close()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'trMedication.Rollback()
            trICD9Association.Rollback()
            '           trICD9Association = Nothing
            ' Cmd = Nothing
            'cmddelete = Nothing
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(cmddelete) Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            If (IsNothing(trICD9Association) = False) Then
                trICD9Association.Dispose()
                trICD9Association = Nothing
            End If
            objparam = Nothing
        End Try
    End Function
    'Public ReadOnly Property DsDataview() As DataView
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return Dv
    '        'Return Ds
    '    End Get

    'End Property
    Public Sub SortDataview(ByVal strsort As String)
        '  Dv.Sort = strsort
    End Sub
    'Public Function ClearCol()
    '    ArrMedicationCol.Clear()
    'End Function
    Public Sub ClearCol()
        'ArrMedicationCol.Clear()
    End Sub
    Public Function FillControls(ByVal id As Long, ByVal strsearch As String) As DataTable
        Dim adpt As New SqlDataAdapter

        Dim Cmd As SqlCommand = Nothing
        Try
            If id = 0 Then
                ''Cmd = New SqlCommand("gsp_FillDrugs_Mst", Conn) '' this  SP pulls top 40 records 
                Cmd = New SqlCommand("gsp_FillAllDrugs_Mst", Conn)  '' Now Pull all records

                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter

                objParam = Cmd.Parameters.Add("@drugletter", SqlDbType.Char)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = LCase(strsearch)

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                'objParam.Value = 1 ''4

                'For De-Normalization
                objParam.Value = 16
                'For De-Normalization

                objParam = Nothing
            ElseIf id = 1 Then
                Cmd = New SqlCommand("gsp_FillCPTCategory_MST", Conn)

                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@Flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                If strsearch = "DESC" Then
                    objParam.Value = 1 '' Sort Dy Description
                Else
                    objParam.Value = 0 '' Sort Dy CODE (DEFAULT)
                End If
                objParam = Nothing

            ElseIf id = 2 Then
                ''''' For Fill Templates of Patient Education
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 99  '' flag to fill Patient education
                objParam = Nothing
            ElseIf id = 4 Then  '' For Tags
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 4   '' flag to fill Tags
                objParam = Nothing
                ''Added Rahul on 20101013
            ElseIf id = 10 Then  '' For refferal letter 
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 10   '' flag to fill Tags
                objParam = Nothing
            ElseIf id = 11 Then  '' For flowsheet

                Dim strquery As String = " SELECT DISTINCT nFlowSheetID AS nFlowSheetID, sFlowSheetName FROM FlowSheet_MST " _
                  & "  ORDER BY sFlowSheetName"
                ' & " UNION SELECT DISTINCT 0 AS nFlowSheetID, sFlowSheetName FROM FlowSheet1 " _
                ' & " WHERE sFlowSheetName not in (SELECT DISTINCT  sFlowSheetName FROM FlowSheet_MST) " _

                ' Cmd = New SqlCommand("select nFlowSheetId,sFlowSheetName from FlowSheet_MST", Conn)

                Cmd = New SqlCommand(strquery, Conn)
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

            ElseIf id = 12 Then  '' For Orders (Radiology Orders)

                Dim strSQL As String
                strSQL = " SELECT   LM_Test.lm_test_ID as lm_test_ID, LM_Test.lm_test_Name as lm_test_Name FROM   LM_Test INNER JOIN " _
                & "LM_Test AS LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID INNER JOIN " _
                & " LM_Category ON LM_Test_1.lm_test_CategoryID = LM_Category.lm_category_ID "

                Cmd = New SqlCommand(strSQL, Conn)
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

            ElseIf id = 13 Then  '' Template

                Dim strSQL As String
                strSQL = "SELECT nTemplateID,sTemplateName FROM TemplateGallery_MST " _
                         & "where sCategoryName ='SOAP' "
                '"select nCategoryID,sDescription from Category_Mst " _
                '             & " where sCategoryType='Template' and  nCategoryID<>-1 " _
                '             & " ORDER BY sDescription "

                Cmd = New SqlCommand(strSQL, Conn)
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

            ElseIf id = 14 Then  '' Lab Orders 

                Dim strSQL As String
                strSQL = "SELECT  labtm_id, labtm_Name FROM Lab_Test_Mst "
                Cmd = New SqlCommand(strSQL, Conn)
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd
                ''
            ElseIf id = 15 Then  'ICD10

                Cmd = New SqlCommand("gsp_Diagnosis_Search", Conn)

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@SearchString", SqlDbType.Text)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = strsearch

                Dim objParam1 As SqlParameter
                objParam1 = Cmd.Parameters.Add("@nICDRevision", SqlDbType.Int)
                objParam1.Direction = ParameterDirection.Input
                objParam1.Value = gloGlobal.gloICD.CodeRevision.ICD10

                Cmd.CommandType = CommandType.StoredProcedure                
                adpt.SelectCommand = Cmd

                objParam = Nothing
                objParam1 = Nothing

              
            ElseIf id = 16 Then
                ''''' For Fill Templates of Patient Education
                Cmd = New SqlCommand("gsp_FillTemplateGallery_MST", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 101  '' flag to fill Patient education
                objParam = Nothing

            ElseIf id = 21 Then
                Cmd = New SqlCommand("CO_GetInsurancePlans", Conn)

                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                Dim objParam As SqlParameter
                objParam = Cmd.Parameters.Add("@nClinicID", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                If strsearch = "DESC" Then
                    objParam.Value = 1
                Else
                    objParam.Value = 1
                End If
                objParam = Nothing

            Else
                'Cmd = New SqlCommand("gsp_FillICD9", Conn)

                'Dim objParam As SqlParameter
                'objParam = Cmd.Parameters.Add("@flag1", SqlDbType.Int)
                'objParam.Direction = ParameterDirection.Input
                'If strsearch = "DESC" Then
                '    objParam.Value = 1 '' Sort Dy Description
                'Else
                '    objParam.Value = 0 '' Sort Dy CODE (DEFAULT)
                'End If
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
                Cmd.CommandTimeout = 0
                adpt.SelectCommand = Cmd

                objParam = Nothing
                objParam1 = Nothing
            End If

            Dim dt As New DataTable
            adpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

        End Try

    End Function
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
    Public Function FetchICD9forUpdate(ByVal id As Long) As DataTable
        Dim sqladpt As New SqlDataAdapter
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_scanICD9Association", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nICD9ID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            Dim dt As New DataTable
            sqladpt.Fill(dt)
            Conn.Close()

            objParam = Nothing
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, "ClsICD9AssociationDBLayer - FetchICD9forUpdate : " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsICD9AssociationDBLayer - FetchICD9forUpdate : " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, "ClsICD9AssociationDBLayer - FetchICD9forUpdate : " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        Finally

            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function
    'Shubhangi 20091211
    'Function to fetch Associated data.
    Public Function FetchAssociatedICD9() As DataTable
        Dim ad As SqlDataAdapter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            'Check connection states
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_AssociatedICD9", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            ad = New SqlDataAdapter(Cmd)
            Dim dt As New DataTable
            ad.Fill(dt)
            ''Sanjog 2011 jan 14 to handle con.open() error
            Conn.Close()
            ''Sanjog 2011 jan 14 to handle con.open() error
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        Finally
           
            If Not IsNothing(ad) Then
                ad.Dispose()
                ad = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function
    Public Function FetchAssociatedICD9forSmartDx(ByVal id As Long) As DataTable
        Dim sqladpt As New SqlDataAdapter
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_GetAssociatedDx", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nSmartDxId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            Dim dt As New DataTable
            sqladpt.Fill(dt)
            Conn.Close()

            objParam = Nothing
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, "ClsICD9AssociationDBLayer - FetchICD9forUpdate : " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsICD9AssociationDBLayer - FetchICD9forUpdate : " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, "ClsICD9AssociationDBLayer - FetchICD9forUpdate : " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        Finally

            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function
    Public Function FetchAssociatedICD10(ByVal SearchString As String, ByVal IsAssociated As Int16) As DataTable
        Dim ad As SqlDataAdapter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            'Check connection states            
            Cmd = New System.Data.SqlClient.SqlCommand("ICD10_GetDiagnosisCodes", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            ad = New SqlDataAdapter(Cmd)

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@SearchText", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SearchString



            objParam = Cmd.Parameters.Add("@IsAssociated", SqlDbType.SmallInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = IsAssociated


            Dim dt As New DataTable
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            ad.Fill(dt)
            ''Sanjog 2011 jan 14 to handle con.open() error
            Conn.Close()
            ''Sanjog 2011 jan 14 to handle con.open() error
            objParam = Nothing
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        Finally

            If Not IsNothing(ad) Then
                ad.Dispose()
                ad = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function FetchAssociatedICD9ICD10(ByVal ICDRevision As Int16, ByVal SearchString As String, ByVal IsAssociated As Int16) As DataTable
        Dim ad As SqlDataAdapter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            'Check connection states
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_AssociatedICD9ICD10", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            ad = New SqlDataAdapter(Cmd)

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@ICDRevision", SqlDbType.SmallInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICDRevision

            objParam = Cmd.Parameters.Add("@SearchString", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SearchString


            objParam = Cmd.Parameters.Add("@IsAssociated", SqlDbType.SmallInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = IsAssociated
            Dim dt As New DataTable
            ad.Fill(dt)
            ''Sanjog 2011 jan 14 to handle con.open() error
            Conn.Close()
            ''Sanjog 2011 jan 14 to handle con.open() error
            objParam = Nothing
            Return dt
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        Finally
        
            If Not IsNothing(ad) Then
                ad.Dispose()
                ad = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function
    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If Not IsNothing(Conn) Then
                    Conn.Dispose()
                    Conn = Nothing
                End If

            End If
            disposed = True
        End If


    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class


Public Class smartDx
#Region "Private Variables"
    Public _DataBaseConnectionString As String
    Private _ClinicID As String = 1
    Private _UserID As String
    Private _MessageBoxCaption As String
    Private oDB As gloDatabaseLayer.DBLayer
    Private oDBPara As gloDatabaseLayer.DBParameters
    Private disposed As Boolean = False
#End Region
#Region "Constuctor and Dispose Methods"
    Public Sub New()
        _ClinicID = gloGlobal.gloPMGlobal.ClinicID
        _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString
        _UserID = gloGlobal.gloPMGlobal.UserID
        _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption
    End Sub
    Public Sub Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If
                If Not IsNothing(oDBPara) Then
                    oDBPara.Dispose()
                    oDBPara = Nothing
                End If
            End If
            disposed = True
        End If


    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region
#Region "Public Method"
    Public Function GetSmartDxList() As DataTable

        Dim _strSQL As String = ""
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())

        Try

            If oDB IsNot Nothing Then
                If oDB.Connect(False) Then
                    oDB.Retrive("gsp_GetSmartDxList", _dt)

                End If
            End If

        Catch ex As SqlException
           gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Catch ex As Exception
           gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        Return _dt
    End Function
    Public Function DeleteSmartDx(ByVal nSmartDxID As Int64, ByVal sSmartDxName As String) As Integer
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim OParameters As New gloDatabaseLayer.DBParameters()
        Dim result As Integer
        Try

            If oDB IsNot Nothing AndAlso OParameters IsNot Nothing Then
                oDB.Connect(False)
                OParameters.Add("@nSmartDxID", nSmartDxID, ParameterDirection.Input, SqlDbType.BigInt)
                result = oDB.Execute("gsp_DeleteSmartDx", OParameters)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Delete, "'" & sSmartDxName & "'  smartdiagnosis setup deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.Delete, "'" & sSmartDxName & "'  smartdiagnosis setup deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If OParameters IsNot Nothing Then
                OParameters.Dispose()
                OParameters = Nothing
            End If
        End Try
        Return result
    End Function
    Public Function MergeSmartDx(ByVal nMergeInToSmartDxId As Int64, ByVal sMergeFromSmartDxIds As String, ByVal sSmartDxName As String) As Integer
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim OParameters As New gloDatabaseLayer.DBParameters()
        Dim result As Integer
        Try

            If oDB IsNot Nothing AndAlso OParameters IsNot Nothing Then
                oDB.Connect(False)
                OParameters.Add("@nMergeInToSmartDxId", nMergeInToSmartDxId, ParameterDirection.Input, SqlDbType.BigInt)
                OParameters.Add("@sMergeFromSmartDxIds", sMergeFromSmartDxIds, ParameterDirection.Input, SqlDbType.VarChar)
                OParameters.Add("@sSmartDxName", sSmartDxName, ParameterDirection.Input, SqlDbType.VarChar)
                result = oDB.Execute("gsp_SmartDxMergeSetup", OParameters)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.merge, "smartdiagnosis setup merged into '" & sSmartDxName & "'   ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, True)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.merge, "smartdiagnosis setup merged into '" & sSmartDxName & "'  ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.smartdiagnosis, gloAuditTrail.ActivityCategory.smartdiagnosissetup, gloAuditTrail.ActivityType.merge, "smartdiagnosis setup merged into '" & sSmartDxName & "' ", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR, True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If OParameters IsNot Nothing Then
                OParameters.Dispose()
                OParameters = Nothing
            End If

        End Try
        Return result
    End Function

    Public Function getAssociatedSmartDxID(ByVal nICDID As Int64, ByVal nExamID As Int64, ByVal nVisitID As Int64) As DataTable
        Dim _dt As DataTable = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim OParameters As New gloDatabaseLayer.DBParameters()
        Try

            If oDB IsNot Nothing AndAlso OParameters IsNot Nothing Then
                If oDB.Connect(False) Then
                    OParameters.Add("@nProviderID", gnLoginProviderID, ParameterDirection.Input, SqlDbType.BigInt)
                    OParameters.Add("@nICDID", nICDID, ParameterDirection.Input, SqlDbType.BigInt)
                    OParameters.Add("@nExamID", nExamID, ParameterDirection.Input, SqlDbType.BigInt)
                    OParameters.Add("@nVisitID", nVisitID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.Retrive("gsp_selectSmartDx", OParameters, _dt)

                End If
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return _dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return _dt
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If OParameters IsNot Nothing Then
                OParameters.Dispose()
                OParameters = Nothing
            End If

        End Try
        Return _dt
    End Function

    Public Function IsSmartDxNameExist(ByVal nSmartDxID As Int64, ByVal sSmartDxName As String, ByVal ISCopyICDSmarDx As Boolean) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim OParameters As New gloDatabaseLayer.DBParameters()
        Dim result As Object = Nothing
        Try

            If oDB IsNot Nothing AndAlso OParameters IsNot Nothing Then
                If oDB.Connect(False) Then
                    OParameters.Add("@sSmartDxName", sSmartDxName.Trim(), ParameterDirection.Input, SqlDbType.VarChar)
                    OParameters.Add("@nsmartDxID", nSmartDxID, ParameterDirection.Input, SqlDbType.BigInt)
                    OParameters.Add("@bIsCopy", ISCopyICDSmarDx, ParameterDirection.Input, SqlDbType.Bit)
                    result = oDB.ExecuteScalar("gsp_IsSmartDxnameExist", OParameters)

                End If
            End If
            If result IsNot Nothing Then
                If Convert.ToString(result).ToUpper() = sSmartDxName.Trim().ToUpper() Then
                    Return True
                End If

            Else
                Return False
            End If
            Return False
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return True
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If OParameters IsNot Nothing Then
                OParameters.Dispose()
                OParameters = Nothing
            End If

        End Try

    End Function

    Public Function getAssociatedSmartDxName(ByVal nSmartDXID As Int64) As String
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim result As Object = Nothing
        Dim strQuery As String = ""
        Try

            If oDB IsNot Nothing Then
                oDB.Connect(False)
                strQuery = "SELECT sSmartDxName FROM dbo.SmartDxMaster WHERE nSmartDxId=" + Convert.ToString(nSmartDXID)
                result = oDB.ExecuteScalar_Query(strQuery)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
            Return ""
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        If result IsNot Nothing Then
            Return Convert.ToString(result)
        Else
            Return ""
        End If

    End Function
    Public Function ExamAssociatedSmartDxInsetion(ByVal nSmartDxID As String, ByVal nExamID As Int64, ByVal nVisiteID As Int64, ByVal nICDID As Int64, ByVal nICDRevision As Integer, ByVal bIsDelete As Boolean) As Integer
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim OParameters As New gloDatabaseLayer.DBParameters()
        Dim result As Integer
        Try

            If oDB IsNot Nothing AndAlso OParameters IsNot Nothing Then
                oDB.Connect(False)
                OParameters.Add("@nSmartDxID", nSmartDxID, ParameterDirection.Input, SqlDbType.BigInt)
                OParameters.Add("@nExamID", nExamID, ParameterDirection.Input, SqlDbType.BigInt)
                OParameters.Add("@nVisiteID", nVisiteID, ParameterDirection.Input, SqlDbType.BigInt)
                OParameters.Add("@nICDId", nICDID, ParameterDirection.Input, SqlDbType.BigInt)
                OParameters.Add("@nICDRevision", nICDRevision, ParameterDirection.Input, SqlDbType.BigInt)
                OParameters.Add("@bIsDelete", bIsDelete, ParameterDirection.Input, SqlDbType.Bit)
                result = oDB.Execute("gsp_InsertExamSmartDxICDAssociation", OParameters)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If OParameters IsNot Nothing Then
                OParameters.Dispose()
                OParameters = Nothing
            End If

        End Try
        Return result
    End Function
    Public Function ExamAssociatedDeletion(ByVal nExamID As Int64, ByVal nVisiteID As Int64) As Integer
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim OParameters As New gloDatabaseLayer.DBParameters()
        Dim result As Integer
        Try

            If oDB IsNot Nothing AndAlso OParameters IsNot Nothing Then
                oDB.Connect(False)

                OParameters.Add("@nExamID", nExamID, ParameterDirection.Input, SqlDbType.BigInt)
                OParameters.Add("@nVisitID", nVisiteID, ParameterDirection.Input, SqlDbType.BigInt)
                result = oDB.Execute("gsp_DeleteExamSmartDxAssociation", OParameters)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If OParameters IsNot Nothing Then
                OParameters.Dispose()
                OParameters = Nothing
            End If

        End Try
        Return result
    End Function
#End Region
End Class


