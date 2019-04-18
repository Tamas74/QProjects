Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord
Imports System.Data.SqlClient

Namespace gloStream
    Namespace gloEMR
        Namespace Triage
            Public Class clsTriage

                Public Function FillTemplates() As DataTable
                    Dim oDB As New DataBaseLayer
                    Dim oParamater As DBParameter

                    Dim oResultTable As DataTable = Nothing
                    Try

                        oParamater = New DBParameter
                        oParamater.DataType = SqlDbType.Int
                        oParamater.Direction = ParameterDirection.Input
                        oParamater.Name = "@flag"
                        oParamater.Value = 20 '' to Fill Triage
                        oDB.DBParametersCol.Add(oParamater)
                        oParamater = Nothing

                        oResultTable = oDB.GetDataTable("gsp_FillTemplateGallery_MST")

                        If Not oResultTable Is Nothing Then
                            Return oResultTable
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

                Public Function SaveTriage(ByVal oTriage As gloStream.gloEMR.Triage.Supportings.Triage) As Int64
                    Dim con As New SqlConnection(GetConnectionString)
                    Dim oWord As New clsWordDocument

                    'declare a transaction object
                    Dim myTrans As SqlTransaction = Nothing
                    Dim cmdTriage As SqlCommand = Nothing

                    Dim triageID As Long
                    Dim machineID As Long
                    Dim oParameter As SqlParameter = Nothing

                    Try
                        con.Open()
                        myTrans = con.BeginTransaction
                        cmdTriage = con.CreateCommand
                        cmdTriage.Transaction = myTrans

                        '' SAVING MASTER RECORDS ''
                        cmdTriage.Connection = con
                        cmdTriage.CommandType = CommandType.StoredProcedure
                        cmdTriage.CommandText = "gsp_InUpTriage"

                        oParameter = New SqlParameter("@nTriageID", SqlDbType.BigInt)
                        oParameter.Direction = ParameterDirection.InputOutput

                        cmdTriage.Parameters.Add(oParameter)
                        cmdTriage.Parameters.Add("@MachineID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@nFromID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@sTemplateName", SqlDbType.VarChar)
                        cmdTriage.Parameters.Add("@dtDate", SqlDbType.DateTime)
                        cmdTriage.Parameters.Add("@sResult", SqlDbType.Image)
                        cmdTriage.Parameters.Add("@bIsFinished", SqlDbType.Bit)
                        cmdTriage.Parameters.Add("@sUserName", SqlDbType.VarChar)
                        cmdTriage.Parameters.Add("@sMachineName", SqlDbType.VarChar)

                        machineID = GetPrefixTransactionID()

                        oParameter.Value = oTriage.TriageID
                        cmdTriage.Parameters("@MachineID").Value = machineID
                        cmdTriage.Parameters("@nFromID").Value = oTriage.FromID
                        cmdTriage.Parameters("@nPatientID").Value = oTriage.PatientID
                        cmdTriage.Parameters("@nTemplateID").Value = oTriage.TemplateID
                        cmdTriage.Parameters("@sTemplateName").Value = oTriage.TemplateName
                        cmdTriage.Parameters("@dtDate").Value = oTriage.TriageDate
                        cmdTriage.Parameters("@sResult").Value = oWord.ConvertFiletoBinary(oTriage.TriageFileName)
                        cmdTriage.Parameters("@bIsFinished").Value = oTriage.IsFinished
                        cmdTriage.Parameters("@sUserName").Value = gstrLoginName
                        cmdTriage.Parameters("@sMachineName").Value = gstrClientMachineName

                        cmdTriage.ExecuteNonQuery()

                        If IsNothing(oParameter.Value) = False Then
                            triageID = oParameter.Value
                        End If

                        If triageID > 0 Then
                            '' DELETE EXISTING DETAILS IN CASE OF UPDATE
                            cmdTriage.Parameters.Clear()
                            cmdTriage.CommandType = CommandType.Text
                            cmdTriage.CommandText = "DELETE FROM Triage_DTL WHERE nTriageID = " & triageID & ""
                            cmdTriage.ExecuteNonQuery()

                            '' SAVING TRIAGE DETAILS FOR MULTILPLE USERS
                            cmdTriage.Parameters.Clear()
                            cmdTriage.CommandType = CommandType.StoredProcedure
                            cmdTriage.CommandText = "gsp_InTriageDTL"

                            For i As Integer = 1 To oTriage.TriageDetails.Count
                                cmdTriage.Parameters.Add("@nTriageID", SqlDbType.BigInt)
                                cmdTriage.Parameters.Add("@nToID", SqlDbType.BigInt)
                                cmdTriage.Parameters.Add("@nFromID", SqlDbType.BigInt)
                                cmdTriage.Parameters.Add("@dtTriage", SqlDbType.DateTime)
                                cmdTriage.Parameters.Add("@bIsReplied", SqlDbType.Bit)

                                cmdTriage.Parameters("@nTriageID").Value = triageID
                                cmdTriage.Parameters("@nToID").Value = oTriage.TriageDetails(i).ToID
                                cmdTriage.Parameters("@nFromID").Value = oTriage.TriageDetails(i).FromID
                                cmdTriage.Parameters("@dtTriage").Value = oTriage.TriageDetails(i).TriageDate
                                cmdTriage.Parameters("@bIsReplied").Value = oTriage.TriageDetails(i).IsReplied

                                cmdTriage.ExecuteNonQuery()
                                cmdTriage.Parameters.Clear()
                            Next

                            myTrans.Commit()
                        Else
                            myTrans.Rollback()
                            Return 0
                        End If
                        If oTriage.IsFinished = True Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.Finish, "Triage finished", oTriage.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ElseIf frmVWTriage.blnmodify = True Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.Modify, "Triage modified", oTriage.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Else

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.Add, "Triage added", oTriage.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)

                    Catch ex As Exception
                        myTrans.Rollback()
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If (IsNothing(con) = False) Then
                            con.Close()
                            con.Dispose()
                            con = Nothing
                        End If
                        Return 0
                    Finally
                        If cmdTriage IsNot Nothing Then
                            cmdTriage.Parameters.Clear()
                            cmdTriage.Dispose()
                            cmdTriage = Nothing
                        End If
                        If myTrans IsNot Nothing Then

                            myTrans.Dispose()
                            myTrans = Nothing
                        End If
                        If (IsNothing(con) = False) Then
                            con.Close()
                            con.Dispose()
                            con = Nothing
                        End If
                        
                        oParameter = Nothing
                    End Try
                    oWord = Nothing
                    Return triageID
                End Function
                Public Function SaveTriageBytes(ByVal oTriage As gloStream.gloEMR.Triage.Supportings.Triage) As Int64
                    Dim con As New SqlConnection(GetConnectionString)
                    ' Dim oWord As New clsWordDocument

                    'declare a transaction object
                    Dim myTrans As SqlTransaction = Nothing
                    Dim cmdTriage As SqlCommand = Nothing

                    Dim triageID As Long
                    Dim machineID As Long
                    Dim oParameter As SqlParameter = Nothing

                    Try
                        con.Open()
                        myTrans = con.BeginTransaction
                        cmdTriage = con.CreateCommand
                        cmdTriage.Transaction = myTrans

                        '' SAVING MASTER RECORDS ''
                        cmdTriage.Connection = con
                        cmdTriage.CommandType = CommandType.StoredProcedure
                        cmdTriage.CommandText = "gsp_InUpTriage"

                        oParameter = New SqlParameter("@nTriageID", SqlDbType.BigInt)
                        oParameter.Direction = ParameterDirection.InputOutput

                        cmdTriage.Parameters.Add(oParameter)
                        cmdTriage.Parameters.Add("@MachineID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@nFromID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
                        cmdTriage.Parameters.Add("@sTemplateName", SqlDbType.VarChar)
                        cmdTriage.Parameters.Add("@dtDate", SqlDbType.DateTime)
                        cmdTriage.Parameters.Add("@sResult", SqlDbType.Image)
                        cmdTriage.Parameters.Add("@bIsFinished", SqlDbType.Bit)
                        cmdTriage.Parameters.Add("@sUserName", SqlDbType.VarChar)
                        cmdTriage.Parameters.Add("@sMachineName", SqlDbType.VarChar)

                        machineID = GetPrefixTransactionID()

                        oParameter.Value = oTriage.TriageID
                        cmdTriage.Parameters("@MachineID").Value = machineID
                        cmdTriage.Parameters("@nFromID").Value = oTriage.FromID
                        cmdTriage.Parameters("@nPatientID").Value = oTriage.PatientID
                        cmdTriage.Parameters("@nTemplateID").Value = oTriage.TemplateID
                        cmdTriage.Parameters("@sTemplateName").Value = oTriage.TemplateName
                        cmdTriage.Parameters("@dtDate").Value = oTriage.TriageDate
                        If (IsNothing(oTriage.TriageObject) = False) Then
                            cmdTriage.Parameters("@sResult").Value = oTriage.TriageObject
                        Else
                            cmdTriage.Parameters("@sResult").Value = DBNull.Value
                        End If

                        cmdTriage.Parameters("@bIsFinished").Value = oTriage.IsFinished
                        cmdTriage.Parameters("@sUserName").Value = gstrLoginName
                        cmdTriage.Parameters("@sMachineName").Value = gstrClientMachineName

                        cmdTriage.ExecuteNonQuery()

                        If IsNothing(oParameter.Value) = False Then
                            triageID = oParameter.Value
                        End If

                        If triageID > 0 Then
                            '' DELETE EXISTING DETAILS IN CASE OF UPDATE
                            cmdTriage.Parameters.Clear()
                            cmdTriage.CommandType = CommandType.Text
                            cmdTriage.CommandText = "DELETE FROM Triage_DTL WHERE nTriageID = " & triageID & ""
                            cmdTriage.ExecuteNonQuery()

                            '' SAVING TRIAGE DETAILS FOR MULTILPLE USERS
                            cmdTriage.Parameters.Clear()
                            cmdTriage.CommandType = CommandType.StoredProcedure
                            cmdTriage.CommandText = "gsp_InTriageDTL"

                            For i As Integer = 1 To oTriage.TriageDetails.Count
                                cmdTriage.Parameters.Add("@nTriageID", SqlDbType.BigInt)
                                cmdTriage.Parameters.Add("@nToID", SqlDbType.BigInt)
                                cmdTriage.Parameters.Add("@nFromID", SqlDbType.BigInt)
                                cmdTriage.Parameters.Add("@dtTriage", SqlDbType.DateTime)
                                cmdTriage.Parameters.Add("@bIsReplied", SqlDbType.Bit)

                                cmdTriage.Parameters("@nTriageID").Value = triageID
                                cmdTriage.Parameters("@nToID").Value = oTriage.TriageDetails(i).ToID
                                cmdTriage.Parameters("@nFromID").Value = oTriage.TriageDetails(i).FromID
                                cmdTriage.Parameters("@dtTriage").Value = oTriage.TriageDetails(i).TriageDate
                                cmdTriage.Parameters("@bIsReplied").Value = oTriage.TriageDetails(i).IsReplied

                                cmdTriage.ExecuteNonQuery()
                                cmdTriage.Parameters.Clear()
                            Next

                            myTrans.Commit()
                        Else
                            myTrans.Rollback()
                            Return 0
                        End If
                        If oTriage.IsFinished = True Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.Finish, "Triage finished", oTriage.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ElseIf frmVWTriage.blnmodify = True Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.Modify, "Triage modified", oTriage.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Else

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.Add, "Triage added", oTriage.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                        'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, gnPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)

                    Catch ex As Exception
                        myTrans.Rollback()
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        If (IsNothing(con) = False) Then
                            con.Close()
                            con.Dispose()
                            con = Nothing
                        End If
                        Return 0
                    Finally
                        If cmdTriage IsNot Nothing Then
                            cmdTriage.Parameters.Clear()
                            cmdTriage.Dispose()
                            cmdTriage = Nothing
                        End If
                        If myTrans IsNot Nothing Then

                            myTrans.Dispose()
                            myTrans = Nothing
                        End If
                        If (IsNothing(con) = False) Then
                            con.Close()
                            con.Dispose()
                            con = Nothing
                        End If

                        oParameter = Nothing
                    End Try
                    'oWord = Nothing
                    Return triageID
                End Function
                Public Function GetTriage(ByVal triageID As Int64, Optional ByVal generateTriageFile As Boolean = True) As gloStream.gloEMR.Triage.Supportings.Triage
                    Dim oDB As New DataBaseLayer
                    Dim dtTriage As DataTable
                    Dim oWord As New clsWordDocument
                    Dim oTriage As New gloStream.gloEMR.Triage.Supportings.Triage
                    Dim oTriageDetails As New gloStream.gloEMR.Triage.Supportings.TriageDetails
                    Dim oTriageDetail As gloStream.gloEMR.Triage.Supportings.TriageDetail

                    Dim query As String = ""
                    Try
                        DataBaseLayer.ConnectionString = GetConnectionString()
                        '' FETCHING MASTER RECORDS
                        query = " SELECT ISNULL(nFromID,0) AS nFromID, ISNULL(nPatientID,0) AS nPatientID, ISNULL(nTemplateID,0) AS nTemplateID, " _
                                & " ISNULL(sTemplateName,'') AS sTemplateName, dtDate, sResult, ISNULL(bIsFinished,0) AS bIsFinished " _
                                & " FROM Triage WHERE nTriageID = " & triageID & ""
                        dtTriage = oDB.GetDataTable_Query(query)




                        If Not IsNothing(dtTriage) Then
                            If dtTriage.Rows.Count > 0 Then
                                oTriage.TriageID = triageID
                                oTriage.FromID = Convert.ToInt64(dtTriage.Rows(0)("nFromID"))
                                oTriage.PatientID = Convert.ToInt64(dtTriage.Rows(0)("nPatientID"))
                                oTriage.TemplateID = Convert.ToInt64(dtTriage.Rows(0)("nTemplateID"))
                                oTriage.TemplateName = dtTriage.Rows(0)("sTemplateName")
                                oTriage.TriageDate = Convert.ToDateTime(dtTriage.Rows(0)("dtDate"))
                                If generateTriageFile Then
                                    oTriage.TriageObject = dtTriage.Rows(0)("sResult")
                                    Dim strFileName As String = ExamNewDocumentName
                                    oTriage.TriageFileName = oWord.GenerateFile(dtTriage.Rows(0)("sResult"), strFileName) '' Create Word File in Temp ''
                                End If
                                oTriage.IsFinished = Convert.ToBoolean(dtTriage.Rows(0)("bIsFinished"))
                            End If
                            dtTriage.Dispose()
                            dtTriage = Nothing
                        End If

                        '' FETCHING DETAIL RECORDS
                        query = ""
                        query = " SELECT ISNULL(nToID,0) AS nToID, ISNULL(nFromID,0) AS nFromID, dtTriage, ISNULL(bIsReplied,0) AS bIsReplied " _
                            & " FROM Triage_DTL WHERE nTriageID = " & triageID & ""
                        ' dtTriage = New DataTable
                        dtTriage = oDB.GetDataTable_Query(query)

                        If Not IsNothing(dtTriage) Then
                            For i As Integer = 0 To dtTriage.Rows.Count - 1
                                oTriageDetail = New Supportings.TriageDetail
                                oTriageDetail.TriageID = triageID
                                oTriageDetail.ToID = Convert.ToInt64(dtTriage.Rows(i)("nToID"))
                                oTriageDetail.FromID = Convert.ToInt64(dtTriage.Rows(i)("nFromID"))
                                oTriageDetail.TriageDate = Convert.ToDateTime(dtTriage.Rows(i)("dtTriage"))
                                oTriageDetail.IsReplied = Convert.ToBoolean(dtTriage.Rows(i)("bIsReplied"))
                                oTriageDetails.Add(oTriageDetail)
                                oTriageDetail = Nothing
                            Next
                            dtTriage.Dispose()
                            dtTriage = Nothing
                        End If

                        oTriage.TriageDetails = oTriageDetails

                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If Not IsNothing(oDB) Then
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                        If Not IsNothing(oWord) Then
                            oWord = Nothing
                        End If
                    End Try

                    Return oTriage
                End Function

                Public Function GetUserTriage(ByVal userID As Int64, Optional ByVal blnOnlyUnfinished As Boolean = False) As Supportings.Triages
                    Dim oTriages As New Supportings.Triages
                    Dim oTriage As Supportings.Triage
                    Dim oDB As New DataBaseLayer
                    Dim dtTriage As DataTable
                    Dim query As String = ""
                    Try
                        DataBaseLayer.ConnectionString = GetConnectionString()
                        '' To get only unfinished Triage(s) 
                        If blnOnlyUnfinished = True Then
                            query = " SELECT DISTINCT Triage_DTL.nTriageID FROM Triage_DTL INNER JOIN Triage ON Triage_DTL.nTriageID = Triage.nTriageID WHERE (ISNULL(Triage.bIsFinished,0) = 0) AND nToID = " & userID & ""
                        Else
                            ' To Get all Triages
                            query = "SELECT DISTINCT nTriageID FROM Triage_DTL WHERE nToID = " & userID & ""
                        End If

                        dtTriage = oDB.GetDataTable_Query(query)
                        If Not IsNothing(dtTriage) Then
                            For iRow As Integer = 0 To dtTriage.Rows.Count - 1
                                oTriage = New Supportings.Triage
                                oTriage = GetTriage(Convert.ToInt64(dtTriage.Rows(iRow)("nTriageID")), False)
                                oTriages.Add(oTriage)
                                oTriage = Nothing
                            Next
                            dtTriage.Dispose()
                            dtTriage = Nothing
                        End If
                        Return oTriages
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function
                ' ''20100525
                'Public Function GetUserTriages(ByVal userID As Int64) As Supportings.Triages
                '    Dim oTriages As New Supportings.Triages
                '    Dim oTriage As Supportings.Triage
                '    Dim oDB As New DataBaseLayer
                '    Dim dtTriage As DataTable
                '    Dim query As String = ""
                '    Try
                '        oDB.ConnectionString = GetConnectionString()
                '        query = "SELECT Triage_DTL.nTriageID, Triage.nTemplateID FROM Triage_DTL INNER JOIN Triage ON Triage_DTL.nTriageID = Triage.nTriageID WHERE nToID = " & userID & ""
                '        dtTriage = oDB.GetDataTable_Query(query)
                '        If Not IsNothing(dtTriage) Then
                '            For iRow As Integer = 0 To dtTriage.Rows.Count - 1
                '                oTriage = New Supportings.Triage
                '                oTriage = GetTriage(Convert.ToInt64(dtTriage.Rows(iRow)("nTriageID")), False)
                '                oTriages.Add(oTriage)
                '                oTriage = Nothing
                '            Next
                '        End If
                '        Return oTriages
                '    Catch ex As Exception
                '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    End Try
                'End Function
                ' ''End 20100525
                Public Function GetSentTriage(ByVal userID As Int64) As Supportings.Triages
                    Dim oTriages As New Supportings.Triages
                    Dim oTriage As Supportings.Triage
                    Dim oDB As New DataBaseLayer
                    Dim dtTriage As DataTable
                    Dim query As String = ""
                    Try
                        DataBaseLayer.ConnectionString = GetConnectionString()
                        query = "SELECT nTriageID FROM Triage WHERE nFromID = " & userID & " "
                        dtTriage = oDB.GetDataTable_Query(query)
                        If Not IsNothing(dtTriage) Then
                            For iRow As Integer = 0 To dtTriage.Rows.Count - 1
                                oTriage = New Supportings.Triage
                                oTriage = GetTriage(Convert.ToInt64(dtTriage.Rows(iRow)("nTriageID")), False)
                                oTriages.Add(oTriage)
                                oTriage = Nothing
                            Next
                            dtTriage.Dispose()
                            dtTriage = Nothing
                        End If
                        Return oTriages
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        oDB.Dispose()
                        oDB = Nothing
                    End Try
                End Function

                Public Function DeleteTriage(ByVal triageID As Int64, ByVal PatientID As Long) As Boolean
                    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
                    Dim query As String = ""
                    Try
                        query = " DELETE FROM Triage_DTL where nTriageID = " & triageID & " ; " _
                                & " DELETE FROM  Triage where nTriageID = " & triageID & ""
                        oDB.Connect(False)
                        oDB.Execute_Query(query)
                        oDB.Disconnect()
                        oDB.Dispose()
                        oDB = Nothing
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Triage, gloAuditTrail.ActivityCategory.Triage, gloAuditTrail.ActivityType.Delete, "Triage deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Return True
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End Try
                End Function

                ''To Display Trigae details on Patientdetails Box
                ''20090911
                Public Function Fill_Triage(ByVal nPatientID As Long) As DataTable
                    Try
                        Dim Con As New SqlConnection(GetConnectionString)

                        Dim cmd As New SqlCommand("gsp_GetTriage", Con)
                        cmd.CommandType = CommandType.StoredProcedure
                        Dim objParam As SqlParameter

                        objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
                        objParam.Direction = ParameterDirection.Input
                        objParam.Value = nPatientID

                        Dim da As New SqlDataAdapter
                        Dim dt As New DataTable
                        da.SelectCommand = cmd
                        da.Fill(dt)
                        If cmd IsNot Nothing Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If
                        objParam = Nothing
                        Con.Close()
                        Con.Dispose()
                        Con = Nothing
                        da.Dispose()
                        da = Nothing
                        Return dt

                    Catch ex As SqlException
                        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        UpdateLog("clsTriage-- Fill_Triage -- " & ex.ToString)
                        Return Nothing
                    Catch ex As Exception
                        UpdateLog("clsTriage -- Fill_Triage -- " & ex.ToString)
                        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing

                    End Try
                End Function
                ''

                Public Sub New()

                End Sub
            End Class
            ''Mayuri:20090911
            'Public Function Fill_Triage(ByVal nPatientID As Long) As DataTable
            '    Try
            '        Dim Con As New SqlConnection(GetConnectionString)

            '        Dim cmd As New SqlCommand("gsp_GetTriageID", Con)
            '        cmd.CommandType = CommandType.StoredProcedure
            '        Dim objParam As SqlParameter

            '        objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            '        objParam.Direction = ParameterDirection.Input
            '        objParam.Value = nPatientID




            '        Dim da As New SqlDataAdapter
            '        Dim dt As New DataTable
            '        da.SelectCommand = cmd
            '        da.Fill(dt)
            '        Return dt

            '    Catch ex As SqlException
            '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        UpdateLog("clsDoctorsDashBoard -- Fill_Triage -- " & ex.ToString)

            '    Catch ex As Exception
            '        UpdateLog("clsDoctorsDashBoard -- Fill_Triage -- " & ex.ToString)
            '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '    End Try
            'End Function
            ''

            Namespace Supportings
                Public Class Triage
                    Private _TriageID As Long = 0
                    Private _FromID As Long = 0
                    Private _PatientID As Long = 0
                    Private _TemplateID As Long = 0
                    Private _TemplateName As String = ""
                    Private _TriageDate As DateTime
                    Private _TriageFileName As String
                    Private _TriageObject As Object
                    Private _IsFinished As Boolean = False
                    Private _TriageDetails As gloStream.gloEMR.Triage.Supportings.TriageDetails
                    Private bTraigeDetails As Boolean = False
                    Public Sub Dispose()
                        If (bTraigeDetails) Then
                            _TriageDetails.Dispose()
                            bTraigeDetails = False
                        End If
                    End Sub

                    Public Property TriageID() As Int64
                        Get
                            Return _TriageID
                        End Get
                        Set(ByVal value As Int64)
                            _TriageID = value
                        End Set
                    End Property
                    Public Property FromID() As Int64
                        Get
                            Return _FromID
                        End Get
                        Set(ByVal value As Int64)
                            _FromID = value
                        End Set
                    End Property
                    Public Property PatientID() As Int64
                        Get
                            Return _PatientID
                        End Get
                        Set(ByVal value As Int64)
                            _PatientID = value
                        End Set
                    End Property
                    Public Property TemplateID() As Int64
                        Get
                            Return _TemplateID
                        End Get
                        Set(ByVal value As Int64)
                            _TemplateID = value
                        End Set
                    End Property
                    Public Property TemplateName() As String
                        Get
                            Return _TemplateName
                        End Get
                        Set(ByVal value As String)
                            _TemplateName = value
                        End Set
                    End Property
                    Public Property TriageDate() As DateTime
                        Get
                            Return _TriageDate
                        End Get
                        Set(ByVal value As DateTime)
                            _TriageDate = value
                        End Set
                    End Property
                    Public Property TriageObject() As Object
                        Get
                            Return _TriageObject
                        End Get
                        Set(ByVal value As Object)
                            _TriageObject = value
                        End Set
                    End Property
                    Public Property TriageFileName() As String
                        Get
                            Return _TriageFileName
                        End Get
                        Set(ByVal value As String)
                            _TriageFileName = value
                        End Set
                    End Property
                    Public Property IsFinished() As Boolean
                        Get
                            Return _IsFinished
                        End Get
                        Set(ByVal value As Boolean)
                            _IsFinished = value
                        End Set
                    End Property
                    Public Property TriageDetails() As gloStream.gloEMR.Triage.Supportings.TriageDetails
                        Get
                            Return _TriageDetails
                        End Get
                        Set(ByVal value As gloStream.gloEMR.Triage.Supportings.TriageDetails)
                            If (bTraigeDetails) Then
                                _TriageDetails.Dispose()
                                bTraigeDetails = False
                            End If
                            _TriageDetails = value
                        End Set
                    End Property

                    Public Sub New()
                        MyBase.new()
                        _TriageDetails = New gloStream.gloEMR.Triage.Supportings.TriageDetails
                        bTraigeDetails = True
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub
                End Class

                Public Class Triages
                    Implements System.Collections.IEnumerable
                    Private mCol As Collection
                    Public Sub Dispose()
                        If (IsNothing(mCol) = False) Then
                            mCol.Clear()
                            mCol = Nothing
                        End If
                    End Sub
                    Public Function Add(ByRef oTriage As gloStream.gloEMR.Triage.Supportings.Triage) As gloStream.gloEMR.Triage.Supportings.Triage
                        mCol.Add(oTriage)
                        Return Nothing
                    End Function

                    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.gloEMR.Triage.Supportings.Triage
                        Get
                            Item = mCol.Item(vntIndexKey)
                        End Get
                    End Property

                    Public ReadOnly Property Count() As Integer
                        Get
                            Count = mCol.Count()
                        End Get
                    End Property

                    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                        'GetEnumerator = mCol.GetEnumerator
                        Return Nothing
                    End Function

                    Public Sub Remove(ByRef vntIndexKey As Object)
                        mCol.Remove(vntIndexKey)
                    End Sub

                    Public Sub New()
                        MyBase.New()
                        mCol = New Collection
                    End Sub

                    Protected Overrides Sub Finalize()
                        Clear()
                        mCol = Nothing
                        MyBase.Finalize()
                    End Sub

                    Public Sub Clear()
                        If mCol Is Nothing Then Exit Sub ' Shouldn't happen, but just in case.

                        Dim i As Short
                        For i = mCol.Count() To 1 Step -1
                            mCol.Remove(i)
                        Next i
                    End Sub

                End Class

                Public Class TriageDetail

                    Private _TriageID As Long = 0
                    Private _ToID As Long = 0
                    Private _FromID As Long = 0
                    Private _TriageDate As DateTime
                    Private _IsReplied As Boolean = False

                    Public Property TriageID() As Long
                        Get
                            Return _TriageID
                        End Get
                        Set(ByVal value As Long)
                            _TriageID = value
                        End Set
                    End Property
                    Public Property ToID() As Long
                        Get
                            Return _ToID
                        End Get
                        Set(ByVal value As Long)
                            _ToID = value
                        End Set
                    End Property
                    Public Property FromID() As Long
                        Get
                            Return _FromID
                        End Get
                        Set(ByVal value As Long)
                            _FromID = value
                        End Set
                    End Property
                    Public Property TriageDate() As DateTime
                        Get
                            Return _TriageDate
                        End Get
                        Set(ByVal value As DateTime)
                            _TriageDate = value
                        End Set
                    End Property
                    Public Property IsReplied() As Boolean
                        Get
                            Return _IsReplied
                        End Get
                        Set(ByVal value As Boolean)
                            _IsReplied = value
                        End Set
                    End Property

                    Public Sub New()
                        MyBase.new()
                    End Sub

                    Protected Overrides Sub Finalize()
                        MyBase.Finalize()
                    End Sub

                End Class

                Public Class TriageDetails
                    Implements System.Collections.IEnumerable
                    Private mCol As Collection
                    Public Sub Dispose()
                        If (IsNothing(mCol) = False) Then
                            mCol.Clear()
                            mCol = Nothing
                        End If
                    End Sub
                    Public Function Add(ByRef oTriageDetail As gloStream.gloEMR.Triage.Supportings.TriageDetail) As gloStream.gloEMR.Triage.Supportings.TriageDetail
                        mCol.Add(oTriageDetail)
                        Return Nothing
                    End Function

                    Public Function Add(ByVal triageID As Int64, ByVal toID As Int64, ByVal fromID As Int64, ByVal triageDate As DateTime, ByVal isReplied As Boolean) As gloStream.gloEMR.Triage.Supportings.TriageDetail
                        'create a new object
                        Dim oTriageDetail As gloStream.gloEMR.Triage.Supportings.TriageDetail
                        Try
                            oTriageDetail = New gloStream.gloEMR.Triage.Supportings.TriageDetail
                            oTriageDetail.TriageID = triageID
                            oTriageDetail.ToID = toID
                            oTriageDetail.FromID = fromID
                            oTriageDetail.TriageDate = triageDate
                            oTriageDetail.IsReplied = isReplied
                            mCol.Add(oTriageDetail)
                            Add = oTriageDetail
                            oTriageDetail = Nothing
                        Catch ex As Exception
                            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Add = Nothing
                        End Try
                    End Function

                    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.gloEMR.Triage.Supportings.TriageDetail
                        Get
                            Item = mCol.Item(vntIndexKey)
                        End Get
                    End Property

                    Public ReadOnly Property Count() As Integer
                        Get
                            Count = mCol.Count()
                        End Get
                    End Property

                    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                        'GetEnumerator = mCol.GetEnumerator
                        Return Nothing
                    End Function

                    Public Sub Remove(ByRef vntIndexKey As Object)
                        mCol.Remove(vntIndexKey)
                    End Sub

                    Public Sub New()
                        MyBase.New()
                        mCol = New Collection
                    End Sub

                    Protected Overrides Sub Finalize()
                        Clear()
                        mCol = Nothing
                        MyBase.Finalize()
                    End Sub

                    Public Sub Clear()
                        If mCol Is Nothing Then Exit Sub ' Shouldn't happen, but just in case.

                        Dim i As Short
                        For i = mCol.Count() To 1 Step -1
                            mCol.Remove(i)
                        Next i
                    End Sub

                End Class
            End Namespace
        End Namespace
    End Namespace
End Namespace
