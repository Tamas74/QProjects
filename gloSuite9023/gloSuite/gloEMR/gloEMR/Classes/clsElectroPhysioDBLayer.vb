Imports System.Data.SqlClient

Public Class clsElectroPhysioDBLayer

    'Public Function SaveElectroPhysio(ByVal ArrList As ArrayList) As Boolean
    '    Dim Con As New SqlConnection(GetConnectionString)
    '    Dim cmd As SqlCommand
    '    Dim trEjectionFraction As SqlTransaction
    '    If Con.State = ConnectionState.Closed Then
    '        Con.Open()
    '    End If
    '    trEjectionFraction = Con.BeginTransaction()

    '    Try
    '        Dim objEjectionFraction As clsEjectionFraction
    '        For i As Int16 = 0 To ArrList.Count - 1
    '            objEjectionFraction = New clsEjectionFraction
    '            objEjectionFraction = CType(ArrList(i), clsEjectionFraction)
    '            Dim sqlParam As SqlParameter
    '            Dim EjectionParam As SqlParameter

    '            If objEjectionFraction.EjectionFractionID <> 0 Then
    '                '''' if chief Complaints are not inserted then delete that problem list
    '                cmd = New SqlCommand("CV_DeleteEjectionFraction", Con)
    '                cmd.CommandType = CommandType.StoredProcedure
    '                cmd.Transaction = trEjectionFraction

    '                EjectionParam = cmd.Parameters.Add("@nEjectionFractionID", objEjectionFraction.EjectionFractionID)
    '                EjectionParam.Direction = ParameterDirection.Input

    '                If Con.State = ConnectionState.Closed Then
    '                    Con.Open()
    '                End If

    '                If cmd.ExecuteNonQuery() > 0 Then
    '                    Dim objAudit As New clsAudit
    '                    'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & objEjectionFraction.VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
    '                    objAudit = Nothing
    '                End If
    '            Else
    '                '' Insert Or Update problem List
    '                cmd = New SqlCommand("CV_InUpEjectionFraction", Con)
    '                cmd.CommandType = CommandType.StoredProcedure
    '                cmd.Transaction = trEjectionFraction

    '                EjectionParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.PatientID

    '                EjectionParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.ExamID

    '                EjectionParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.VisitID

    '                EjectionParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.ClinicID

    '                EjectionParam = cmd.Parameters.Add("@dtDateOfTest", SqlDbType.DateTime)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.TestDate

    '                EjectionParam = cmd.Parameters.Add("@sModalityTest", SqlDbType.VarChar, 100)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.ModalityTest

    '                EjectionParam = cmd.Parameters.Add("@sQuantityPercent", SqlDbType.VarChar, 50)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.QuantityPercent

    '                EjectionParam = cmd.Parameters.Add("@sQuantityDesc", SqlDbType.VarChar, 100)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = objEjectionFraction.QuantityDescription

    '                EjectionParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '                EjectionParam.Direction = ParameterDirection.Input
    '                EjectionParam.Value = GetPrefixTransactionID()


    '                EjectionParam = cmd.Parameters.Add("@nEjectionFractionID", objEjectionFraction.EjectionFractionID)
    '                EjectionParam.Direction = ParameterDirection.InputOutput
    '                EjectionParam.Value = objEjectionFraction.EjectionFractionID

    '                If Con.State = ConnectionState.Closed Then
    '                    Con.Open()
    '                End If
    '                cmd.ExecuteNonQuery()

    '                If objEjectionFraction.EjectionFractionID = 0 Then
    '                    gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Ejection Fraction  Added", gstrLoginName, gstrClientMachineName, gnPatientID)
    '                Else
    '                    gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Ejection Fraction Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
    '                End If
    '            End If
    '        Next
    '        trEjectionFraction.Commit()
    '        Return True
    '    Catch ex As SqlException
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        trEjectionFraction.Rollback()
    '        Return False
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        trEjectionFraction.Rollback()
    '        Return False
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function

    Public Function SaveElectroPhysioTest(ByVal ArrList As ArrayList) As Boolean
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand
        Dim trElectroPhysio As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trElectroPhysio = Con.BeginTransaction()

        Dim objclsElectroPhysio As clsElectroPhysio = Nothing

        Try
            For i As Int16 = 0 To ArrList.Count - 1
                'objclsElectroPhysio = New clsElectroPhysio
                objclsElectroPhysio = CType(ArrList(i), clsElectroPhysio)
                'Dim sqlParam As SqlParameter
                Dim ElecPhysioParam As SqlParameter

                'If objclsElectroPhysio.ElectroPhysiologyID <> 0 Then
                '    cmd = New SqlCommand("gsp_CV_DeleteElectroPhysio", Con)
                '    cmd.CommandType = CommandType.StoredProcedure
                '    cmd.Transaction = trElectroPhysio

                '    ElecPhysioParam = cmd.Parameters.Add("@nElectroPhysiologyID", objclsElectroPhysio.ElectroPhysiologyID)
                '    ElecPhysioParam.Direction = ParameterDirection.Input

                '    If Con.State = ConnectionState.Closed Then
                '        Con.Open()
                '    End If

                '    If cmd.ExecuteNonQuery() > 0 Then
                '        Dim objAudit As New clsAudit
                '        'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & objStressTest.VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                '        objAudit = Nothing
                '    End If
                'Else
                '' Insert Or Update problem List
                cmd = New SqlCommand("gsp_InUpCVElectroPhysio", Con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trElectroPhysio

                ElecPhysioParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = objclsElectroPhysio.PatientID

                ElecPhysioParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = objclsElectroPhysio.ExamID

                ElecPhysioParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = objclsElectroPhysio.VisitID

                ElecPhysioParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = objclsElectroPhysio.ClinicID

                ElecPhysioParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = objclsElectroPhysio.dtProcedureDate

                If Not IsNothing(CType(objclsElectroPhysio.CPTCode, String)) Then
                    ElecPhysioParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar, 50)
                    ElecPhysioParam.Direction = ParameterDirection.Input
                    ElecPhysioParam.Value = objclsElectroPhysio.CPTCode
                Else
                    ElecPhysioParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar, 50)
                    ElecPhysioParam.Direction = ParameterDirection.Input
                    ElecPhysioParam.Value = ""
                End If


                ElecPhysioParam = cmd.Parameters.Add("@sProcedures", SqlDbType.VarChar, 100)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = objclsElectroPhysio.Procedures

                ElecPhysioParam = cmd.Parameters.Add("@sUserProvider", SqlDbType.VarChar, 2000)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = objclsElectroPhysio.UserProvider


                ElecPhysioParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                ElecPhysioParam.Direction = ParameterDirection.Input
                ElecPhysioParam.Value = GetPrefixTransactionID()


                ElecPhysioParam = cmd.Parameters.AddWithValue("@nElectroPhysiologyID", objclsElectroPhysio.ElectroPhysiologyID)
                ElecPhysioParam.Direction = ParameterDirection.InputOutput
                ElecPhysioParam.Value = objclsElectroPhysio.ElectroPhysiologyID

                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If
                cmd.ExecuteNonQuery()
                objclsElectroPhysio.ElectroPhysiologyID = ElecPhysioParam.Value
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                If objclsElectroPhysio.ElectroPhysiologyID = 0 Then
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Electro Physio Test Added", gstrLoginName, gstrClientMachineName, gnPatientID)
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electro Physio Test Added", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electro Physio Test Added", objclsElectroPhysio.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                Else
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Electro Physio Test Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Modify, "Electro Physio Test Modified", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101009
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Modify, "Electro Physio Test Modified", objclsElectroPhysio.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                End If
                'End If

                ElecPhysioParam = Nothing
            Next
            trElectroPhysio.Commit()
            Return True
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objclsElectroPhysio.ElectroPhysiologyID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Electro Physiology Test could not be added", gstrLoginName, gstrClientMachineName, gnPatientID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electro Physiology Test could not be added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electro Physiology Test could not be added", objclsElectroPhysio.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Electro Physiology Test could not be Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Modify, "Electro Physiology Test could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Modify, "Electro Physiology Test could not be Modified", objclsElectroPhysio.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trElectroPhysio.Rollback()
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objclsElectroPhysio.ElectroPhysiologyID = 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Electro Physiology Test could not be added", gstrLoginName, gstrClientMachineName, gnPatientID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electro Physiology Test could not be added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electro Physiology Test could not be added", objclsElectroPhysio.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Electro Physiology Test could not be Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Modify, "Electro Physiology Test could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Modify, "Electro Physiology Test could not be Modifie", objclsElectroPhysio.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trElectroPhysio.Rollback()
            Return False
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If Not IsNothing(trElectroPhysio) Then
                trElectroPhysio.Dispose()
                trElectroPhysio = Nothing
            End If
        End Try
    End Function




    Public Sub DeleteElectroPhysioTest(ByVal mPatientID As Int64, ByVal mVisitId As Int64, ByVal mDateofProc As Date)
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand
        Dim trElecPhysioTest As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trElecPhysioTest = Con.BeginTransaction()
        'Dim sqlParam As SqlParameter
        Dim StressParam As SqlParameter
        Try
            cmd = New SqlCommand("CV_DeleteElectroPhysioTest", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trElecPhysioTest

            StressParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = mPatientID


            StressParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = mVisitId

            StressParam = cmd.Parameters.Add("@DateofProc", SqlDbType.DateTime)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = mDateofProc



            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then
                'Dim objAudit As New clsAudit
                ''objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & objStressTest.VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                'objAudit = Nothing
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            trElecPhysioTest.Commit()


            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Electro Physiology Test deleted.", gstrLoginName, gstrClientMachineName, gnPatientID)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, "Electro Physiology Test deleted.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, "Electro Physiology Test deleted.", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Electro Physiology Test could not be deleted.", gstrLoginName, gstrClientMachineName, gnPatientID)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, "Electro Physiology Test could not be deleted.", gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Delete, "Electro Physiology Test could not be deleted.", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
        Finally
            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
            If Not IsNothing(trElecPhysioTest) Then
                trElecPhysioTest.Dispose()
                trElecPhysioTest = Nothing
            End If

            StressParam = Nothing
        End Try

    End Sub




End Class
