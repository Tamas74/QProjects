Imports System.Data.SqlClient


Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class clscv_echocardiogram

    

    'Public Function insert_echocardiogram(ByVal lstEcho As Cls_Echocardiograms) '(ByVal nExamID As Long, ByVal strExamName As String, ByVal dtModiDt As DateTime, ByVal strComment As String, ByVal strEvent As String, ByVal arrobj As ArrayList)

    '    Try

    '        'For Len As Integer = 0 To arr.Count
    '        '    Dim cls As Cls_Echocardiograms = CType(arr(Len), Cls_Echocardiograms)
    '        '    For Each cls2 As Cls_Echocardiogram In cls.Item(Len)

    '        '    Next



    '        'Next
    '        Dim objCls_Echocardiogram As Cls_Echocardiogram

    '        For i As Int16 = 0 To lstEcho.Count - 1
    '            objCls_Echocardiogram = New Cls_Echocardiogram
    '            objCls_Echocardiogram = lstEcho.Item(i)
    '            Dim sqlParam As SqlParameter



    '            Dim cmd As New SqlCommand("gsp_insertEchocardiogram", Con)
    '            cmd.CommandType = CommandType.StoredProcedure

    '            If Con.State = ConnectionState.Closed Then Con.Open()


    '            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.nPatientID

    '            sqlParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.dtprocdate

    '            sqlParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.scptcode


    '            sqlParam = cmd.Parameters.Add("@sProcedures", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.sprocedures



    '            sqlParam = cmd.Parameters.Add("@Aortic", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.aortic

    '            sqlParam = cmd.Parameters.Add("@Mitral", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.mitral

    '            sqlParam = cmd.Parameters.Add("@LAArea", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.laarea

    '            sqlParam = cmd.Parameters.Add("@AVArea", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.avarea

    '            sqlParam = cmd.Parameters.Add("@MVArea", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.mvarea

    '            'sqlParam = cmd.Parameters.Add("@LVDiastvol", SqlDbType.VarChar)
    '            'sqlParam.Direction = ParameterDirection.Input
    '            'sqlParam.Value = objCls_Echocardiogram.cvdiastvol
    '            sqlParam = cmd.Parameters.Add("@LVsystvol", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.lvsystvol

    '            sqlParam = cmd.Parameters.Add("@LVMass", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.lvmass

    '            sqlParam = cmd.Parameters.Add("@IDofintrepertingphys", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.IDofinterpatphys

    '            sqlParam = cmd.Parameters.Add("@NarrativeSummary", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.Narrativesummary

    '            sqlParam = cmd.Parameters.Add("@Lvedd", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.lvedd

    '            sqlParam = cmd.Parameters.Add("@LVDiastolic", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.LVDiastolic

    '            sqlParam = cmd.Parameters.Add("@Lvesd", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.lvesdvc

    '            sqlParam = cmd.Parameters.Add("@Lvpostwallthick", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.lvpostwallthik

    '            sqlParam = cmd.Parameters.Add("@septalthick", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.septalthik

    '            sqlParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.ExamID


    '            sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objCls_Echocardiogram.VisitID


    '            cmd.ExecuteNonQuery()
    '            cmd.Dispose()
    '            cmd = Nothing
    '        Next

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, "Record EChocardiogram", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Con.Close()
    '        Con.Dispose()
    '        Con = Nothing
    '    End Try

    'End Function

    Public Function insert_echocardiogram(ByVal lstEcho As Cls_Echocardiograms, Optional ByVal nTransaction As Integer = 0) '(ByVal nExamID As Long, ByVal strExamName As String, ByVal dtModiDt As DateTime, ByVal strComment As String, ByVal strEvent As String, ByVal arrobj As ArrayList)

        Try

            'For Len As Integer = 0 To arr.Count
            '    Dim cls As Cls_Echocardiograms = CType(arr(Len), Cls_Echocardiograms)
            '    For Each cls2 As Cls_Echocardiogram In cls.Item(Len)

            '    Next



            'Next
            If (IsNothing(Con)) Then
                Con = New SqlConnection(conString)
            End If
            Dim objCls_Echocardiogram As Cls_Echocardiogram = Nothing

            For i As Int16 = 0 To lstEcho.Count - 1
                '            objCls_Echocardiogram = New Cls_Echocardiogram
                objCls_Echocardiogram = lstEcho.Item(i)
                Dim sqlParam As SqlParameter



                Dim cmd As New SqlCommand("gsp_insertEchocardiogram", Con)
                cmd.CommandType = CommandType.StoredProcedure

                If Con.State = ConnectionState.Closed Then Con.Open()


                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.nPatientID

                sqlParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.dtprocdate

                sqlParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.scptcode


                sqlParam = cmd.Parameters.Add("@sProcedures", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.sprocedures



                sqlParam = cmd.Parameters.Add("@Aortic", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.aortic

                sqlParam = cmd.Parameters.Add("@Mitral", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.mitral

                sqlParam = cmd.Parameters.Add("@LAArea", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.laarea

                sqlParam = cmd.Parameters.Add("@AVArea", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.avarea

                sqlParam = cmd.Parameters.Add("@MVArea", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.mvarea

                'sqlParam = cmd.Parameters.Add("@LVDiastvol", SqlDbType.VarChar)
                'sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = objCls_Echocardiogram.cvdiastvol
                sqlParam = cmd.Parameters.Add("@LVsystvol", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.lvsystvol

                sqlParam = cmd.Parameters.Add("@LVMass", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.lvmass

                sqlParam = cmd.Parameters.Add("@IDofintrepertingphys", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.IDofinterpatphys

                sqlParam = cmd.Parameters.Add("@NarrativeSummary", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.Narrativesummary

                sqlParam = cmd.Parameters.Add("@Lvedd", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.lvedd

                sqlParam = cmd.Parameters.Add("@LVDiastolic", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.LVDiastolic

                sqlParam = cmd.Parameters.Add("@Lvesd", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.lvesdvc

                sqlParam = cmd.Parameters.Add("@Lvpostwallthick", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.lvpostwallthik

                sqlParam = cmd.Parameters.Add("@septalthick", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.septalthik

                sqlParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.ExamID


                sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = objCls_Echocardiogram.VisitID

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = GetPrefixTransactionID()

                cmd.ExecuteNonQuery()
                '           cmd.Dispose()

                sqlParam = Nothing

                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

            Next

            If nTransaction = 1 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, "Echocardiogram added", objCls_Echocardiogram.nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf nTransaction = 2 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Modify, "Echocardiogram modified", objCls_Echocardiogram.nPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Record EChocardiogram", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
        End Try
        Return Nothing
    End Function

    Private Con As SqlConnection
    Private conString As String
    'Private _controls As New C1.Win.C1FlexGrid.C1FlexGrid
    Public Sub Dispose()

       

        'slr free Con
        If Not IsNothing(Con) Then
            Con.Dispose()
            Con = Nothing
        End If

    End Sub
    Public Sub New()
        Try
            ' Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "EChocardiogram", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "EChocardiogram", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

End Class
