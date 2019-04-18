Imports gloEMR.gloEMRWord
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.Data.SqlClient

Public Class clsMessage
    Implements IDisposable

    '08-May-13 Aniket: Resolving Memory Leaks
    'Private ds As New DataSet
    Private dt As DataTable
    Private dv As DataView
    Private disposed As Boolean = False

    '08-May-13 Aniket: Resolving Memory Leaks
    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get

    '        Return ds

    '    End Get
    'End Property

    Public ReadOnly Property GetDataTable() As DataTable
        Get

            Return dt

        End Get
    End Property

    Public ReadOnly Property GetDataview() As DataView
        Get

            Return dv

        End Get
    End Property

    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '%" & txtSearch & "%'"
        End If

        Return dv
    End Function

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '%" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If

    End Sub

    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]" & strSortOrder
        End If

    End Sub

    Public Function Fill_PatientMessges(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetMesseges", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            '08-May-13 Aniket: Resolving Memory Leaks
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function SelectMessage(ByVal MsgID As Long, ByVal UserID As Long, ByVal dtMsgDate As Date) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MsgID"
            oParamater.Value = MsgID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@UserID"
            oParamater.Value = UserID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dtMsgDate"
            oParamater.Value = Format(dtMsgDate, "MM/dd/yyyy") & " " & Format(dtMsgDate, "Medium Time")   'MsgDate

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            ''Slr free previous memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ScanMessage")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oParamater) Then 'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetAllMessages(ByVal strFromTo As String, Optional ByVal UserID As Long = 0) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@UserID"
            oParamater.Value = UserID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FromTo"
            oParamater.Value = strFromTo

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''Slr free previous memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            dt = oDB.GetDataTable("gsp_ViewMessage")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error while establishing connection with the server", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            UpdateLog(" Patient Messages - GetAllMessages -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("Error while establishing connection with the server", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            UpdateLog(" Patient Messages - GetAllMessages -- " & ex.ToString)
            Return Nothing
            'MessageBox.Show(ex.ToString, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oParamater) Then 'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetAllMessagesList(ByVal strFromTo As String, Optional ByVal UserID As Long = 0) As DataTable  ''added for solving bug 69505 rearrance all columns name
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@UserID"
            oParamater.Value = UserID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FromTo"
            oParamater.Value = strFromTo

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''Slr free previous memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If

            dt = oDB.GetDataTable("gsp_ViewMessageList")
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error while establishing connection with the server", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            UpdateLog(" Patient Messages - GetAllMessages -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("Error while establishing connection with the server", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            UpdateLog(" Patient Messages - GetAllMessages -- " & ex.ToString)
            Return Nothing
            'MessageBox.Show(ex.ToString, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oParamater) Then 'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetTopMessages(ByVal strFromTo As String, ByVal RowCount As Int64, ByVal UserID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        '07-May-13 Aniket: Resolving Memory Leaks
        'Dim da As SqlDataAdapter
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@UserID"
            oParamater.Value = UserID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FromTo"
            oParamater.Value = strFromTo

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@RowCount"
            oParamater.Value = RowCount

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            ''Slr free previous memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ViewMessageOnDashboard")

            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error while establishing connection with the server", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            UpdateLog(" Patient Messages - GetAllMessages -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("Error while establishing connection with the server", "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            UpdateLog(" Patient Messages - GetAllMessages -- " & ex.ToString)
            Return Nothing

        Finally
            If Not IsNothing(oParamater) Then 'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetAllPatients() As DataTable

        Dim oDB As New DataBaseLayer

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable


        Try
            oResultTable = oDB.GetDataTable("gsp_viewPatient_msg")
            ''Slr free previous memory
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            If Not oResultTable Is Nothing Then
                dv = oResultTable.DefaultView
                Return oResultTable
            Else
                Return Nothing
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            '08-May-13 Aniket: Resolving Memory Leaks
            'If Not IsNothing(oResultTable) Then 'Disposed by mitesh
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetAllUser() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Dim oResultTable As DataTable  ''Slr new not needed 
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 1 '' to Fill All Users
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            ''Slr free previous memory
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            oResultTable = oDB.GetDataTable("gsp_FillUsers")

            If Not oResultTable Is Nothing Then
              
                dv = oResultTable.DefaultView
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            '08-May-13 Aniket: Resolving Memory Leaks
            'If Not IsNothing(oResultTable) Then 'Disposed by mitesh
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function AddNewMessage(ByVal ReplyMsgID As Long, ByVal MessageID As Long, ByVal FromID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal MsgDate As Date, ByVal strTempFilePath As String, ByVal IsFinished As CheckState, ByVal IsReplied As Boolean, ByVal ArrUser As Array, ByVal TemplateName As String, ByVal messagePriority As Integer, ByVal bisUnscheduledCare As Boolean, ByVal nCommunicationType As Int64, ByVal strsubject As String) As Long        ''@ID,@FromID,@PatientID,@dtMsgDate, @Result,@bIsFinished , @bIsReplied

        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FromID"
            oParamater.Value = FromID
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
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dtMsgDate"
            'Problem 00000024 : date column displays the date that the message was last "Saved" rather than "Created." 
            'oParamater.Value = frmMessages.m_MsgDate
            ''Bug #70506: 00000722: Patient messages issue. Changes for Problem 00000024 are made in script.
            oParamater.Value = MsgDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@bIsFinished"

            If IsFinished = CheckState.Checked Then
                oParamater.Value = 1
            ElseIf IsFinished = CheckState.Unchecked Then
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Result"
            Dim objword As New clsWordDocument
            '' To convert from Object to Binary Format
            oParamater.Value = objword.ConvertFiletoBinary(strTempFilePath)

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            '08-May-13 Aniket: Resolving Memory Leaks
            objword = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachineID"
            oParamater.Value = GetPrefixTransactionID()
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sTemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            '' Added on 20090622
            '' to Set the Priority of Message
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPriority"
            oParamater.Value = messagePriority
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@bisUnscheduledCare"
            oParamater.Value = bisUnscheduledCare
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nCommunicationType"
            oParamater.Value = nCommunicationType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing




            ''added subject field for 8022 prd changes
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sSubject"
            oParamater.Value = strsubject
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            '' MessageID should be at last 
            Dim oIDParamater As New DBParameter
            oIDParamater.DataType = SqlDbType.BigInt
            oIDParamater.Direction = ParameterDirection.InputOutput
            oIDParamater.Name = "@MessageID"
            oIDParamater.Value = MessageID
            oDB.DBParametersCol.Add(oIDParamater)
            oIDParamater = Nothing
            MessageID = oDB.Add("gsp_InUpMessage")

            '08-May-13 Aniket: Resolving Memory Leaks
            oDB.Dispose()
            oDB = Nothing

            If IsReplied = False Then
                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MessageID"
                oParamater.Value = MessageID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtMessage"
                oParamater.Value = MsgDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Delete("gsp_DeleteMessage_DTL")

                '08-May-13 Aniket: Resolving Memory Leaks
                oDB.Dispose()
                oDB = Nothing
            Else
                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MessageID"
                oParamater.Value = MessageID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtMessage"
                ''Bug #70506: 00000722: Patient messages issue
                'oParamater.Value = frmMessages.m_MsgDate
                oParamater.Value = MsgDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Bit
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@IsReplied"
                oParamater.Value = 1
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("gsp_UpdateMessage_DTL")

                '08-May-13 Aniket: Resolving Memory Leaks
                oDB.Dispose()
                oDB = Nothing
            End If

            Dim i As Integer
            For i = 0 To ArrUser.Length - 1
                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MessageID"
                oParamater.Value = MessageID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ToID"
                oParamater.Value = ArrUser(i)
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@FromID"
                oParamater.Value = FromID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtMsgDate"
                oParamater.Value = MsgDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Bit
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@IsReplied"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("gsp_InUpMessage_DTL")

                '08-May-13 Aniket: Resolving Memory Leaks
                oDB.Dispose()
                oDB = Nothing
            Next
            If IsFinished = CheckState.Checked Then

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Finish, "Patient Message finished", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf IsFinished = CheckState.Checked AndAlso frmVWMessages.blnModify = True Then

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Finish, "Patient Message finished", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf frmVWMessages.blnModify = True Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Message Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Modify, "Patient Message modified", gloAuditTrail.ActivityOutCome.Success)


                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Modify, "Patient Message modified", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                ' trMessage.Commit()
                Return MessageID
            Else
                ''Bug #70506: 00000722: Patient messages issue
                If IsReplied = False Then
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message added", gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message added", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    '  trMessage.Commit()
                    Return MessageID
                Else
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message replied", gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message replied", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    '  trMessage.Commit()
                    Return MessageID
                End If
            End If
            'objAudit = Nothing
            'End If
            Return MessageID

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(oParamater) Then  'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
    Public Function AddNewMessageBytes(ByVal ReplyMsgID As Long, ByVal MessageID As Long, ByVal FromID As Long, ByVal PatientID As Long, ByVal TemplateID As Long, ByVal MsgDate As Date, ByVal bBytes As Object, ByVal IsFinished As CheckState, ByVal IsReplied As Boolean, ByVal ArrUser As Array, ByVal TemplateName As String, ByVal messagePriority As Integer, ByVal bisUnscheduledCare As Boolean, ByVal nCommunicationType As Int64, ByVal strsubject As String) As Long        ''@ID,@FromID,@PatientID,@dtMsgDate, @Result,@bIsFinished , @bIsReplied

        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Try
            oDB = New DataBaseLayer
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@FromID"
            oParamater.Value = FromID
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
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@TemplateID"
            oParamater.Value = TemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@dtMsgDate"
            'Problem 00000024 : date column displays the date that the message was last "Saved" rather than "Created." 
            'oParamater.Value = frmMessages.m_MsgDate
            ''Bug #70506: 00000722: Patient messages issue. Changes for Problem 00000024 are made in script.
            oParamater.Value = MsgDate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@bIsFinished"

            If IsFinished = CheckState.Checked Then
                oParamater.Value = 1
            ElseIf IsFinished = CheckState.Unchecked Then
                oParamater.Value = 0
            End If

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Image
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Result"
            
            If (IsNothing(bBytes) = False) Then
                oParamater.Value = bBytes
            Else
                oParamater.Value = DBNull.Value
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

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sTemplateName"
            oParamater.Value = TemplateName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            '' Added on 20090622
            '' to Set the Priority of Message
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPriority"
            oParamater.Value = messagePriority
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Bit
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@bisUnscheduledCare"
            oParamater.Value = bisUnscheduledCare
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nCommunicationType"
            oParamater.Value = nCommunicationType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing




            ''added subject field for 8022 prd changes
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sSubject"
            oParamater.Value = strsubject
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            '' MessageID should be at last 
            Dim oIDParamater As New DBParameter
            oIDParamater.DataType = SqlDbType.BigInt
            oIDParamater.Direction = ParameterDirection.InputOutput
            oIDParamater.Name = "@MessageID"
            oIDParamater.Value = MessageID
            oDB.DBParametersCol.Add(oIDParamater)
            oIDParamater = Nothing
            MessageID = oDB.Add("gsp_InUpMessage")

            '08-May-13 Aniket: Resolving Memory Leaks
            oDB.Dispose()
            oDB = Nothing

            If IsReplied = False Then
                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MessageID"
                oParamater.Value = MessageID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtMessage"
                oParamater.Value = MsgDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Delete("gsp_DeleteMessage_DTL")

                '08-May-13 Aniket: Resolving Memory Leaks
                oDB.Dispose()
                oDB = Nothing
            Else
                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MessageID"
                oParamater.Value = MessageID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtMessage"
                ''Bug #70506: 00000722: Patient messages issue
                'oParamater.Value = frmMessages.m_MsgDate
                oParamater.Value = MsgDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Bit
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@IsReplied"
                oParamater.Value = 1
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("gsp_UpdateMessage_DTL")

                '08-May-13 Aniket: Resolving Memory Leaks
                oDB.Dispose()
                oDB = Nothing
            End If

            Dim i As Integer
            For i = 0 To ArrUser.Length - 1
                oDB = New DataBaseLayer

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@MessageID"
                oParamater.Value = MessageID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing


                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@ToID"
                oParamater.Value = ArrUser(i)
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.BigInt
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@FromID"
                oParamater.Value = FromID
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.DateTime
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@dtMsgDate"
                oParamater.Value = MsgDate
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oParamater = New DBParameter
                oParamater.DataType = SqlDbType.Bit
                oParamater.Direction = ParameterDirection.Input
                oParamater.Name = "@IsReplied"
                oParamater.Value = 0
                oDB.DBParametersCol.Add(oParamater)
                oParamater = Nothing

                oDB.Add("gsp_InUpMessage_DTL")

                '08-May-13 Aniket: Resolving Memory Leaks
                oDB.Dispose()
                oDB = Nothing
            Next
            If IsFinished = CheckState.Checked Then

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Finish, "Patient Message finished", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf IsFinished = CheckState.Checked AndAlso frmVWMessages.blnModify = True Then

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Finish, "Patient Message finished", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ElseIf frmVWMessages.blnModify = True Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Message Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Modify, "Patient Message modified", gloAuditTrail.ActivityOutCome.Success)


                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Modify, "Patient Message modified", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                ' trMessage.Commit()
                Return MessageID
            Else
                ''Bug #70506: 00000722: Patient messages issue
                If IsReplied = False Then
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message added", gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message added", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

                    '  trMessage.Commit()
                    Return MessageID
                Else
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message replied", gloAuditTrail.ActivityOutCome.Success)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, "Patient Message replied", PatientID, MessageID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    '  trMessage.Commit()
                    Return MessageID
                End If
            End If
            'objAudit = Nothing
            'End If
            Return MessageID

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If Not IsNothing(oParamater) Then  'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Sub AddMessage_DTL(ByVal MessageID As Long, ByVal UserID As Long)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MessageID"
            oParamater.Value = MessageID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@UserID"
            oParamater.Value = UserID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Add("gsp_InUpMessage_DTL")

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oParamater) Then  'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Public Sub DeleteMessage_DTL(ByVal MessageID As Long)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MessageID"
            oParamater.Value = MessageID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oDB.Delete("gsp_DeleteMessage_DTL")


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oParamater) Then  'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Public Function GetUserID(ByVal LoginName As String) As Long
        Dim oDB As New DataBaseLayer

        Try
            Dim nUserID As Long
            Dim _strSQL As String
            _strSQL = "select nUserId from User_Mst where sLoginName='" & LoginName.Replace("'", "''") & "'"

            nUserID = CType(oDB.GetRecord_Query(_strSQL), Long)

            Return nUserID

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(oDB) Then  'Disposed by mitesh
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetProviderUserID(ByVal PatientID As Long) As Long
        Dim oDB As New DataBaseLayer

        Try
            Dim nUserID As Long
            Dim _strSQL As String
            _strSQL = "SELECT User_MST.nUserID FROM Patient INNER JOIN User_MST ON Patient.nProviderID = User_MST.nProviderID"
            If PatientID = 0 Then
                nUserID = 0
            Else
                nUserID = CType(oDB.GetRecord_Query(_strSQL), Long)
            End If
            Return nUserID


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then  'Disposed by mitesh
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Sub DeleteMessages(ByVal MsgID As Long, ByVal MessagesDate As String, ByVal PatientID As Long)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MsgID"
            oParamater.Value = MsgID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oDB.Delete("gsp_DeleteMessage")
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.PatientMessages, gloAuditTrail.ActivityType.Delete, "Patient Message deleted", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then  'Disposed by mitesh
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Public Function SelectMessage(ByVal MsgID As Long) As DataTable
        '' RETURNS Table 
        '''' (0)MessageID
        '''' (1)MessageDate 
        '''' (2)From(Login Name)
        '''' (3)To(Login Name)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        '08-May-13 Aniket: Resolving Memory Leaks
        'Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MsgID"
            oParamater.Value = MsgID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            ''Slr free previous memory
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            dt = oDB.GetDataTable("gsp_ViewMessageHistory")

            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            '08-May-13 Aniket: Resolving Memory Leaks
            'If Not IsNothing(oResultTable) Then 'Disposed by mitesh
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function FillTemplates() As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@flag"
            oParamater.Value = 8 '' to Fill Patient Messages
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
            '08-May-13 Aniket: Resolving Memory Leaks
            'If Not IsNothing(oResultTable) Then 'Disposed by mitesh
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function FillReminderforUnscheduledCare(Optional ByVal _nTemplateID As Int64 = 0)
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        Try


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sType"
            oParamater.Value = "Messages"
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTemplateID"
            oParamater.Value = _nTemplateID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            Dim _ds As DataSet

            _ds = oDB.GetDataSet("gsp_FillReminderforUnscheduledCare", True)




            If Not _ds Is Nothing Then
                Return _ds
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(oParamater) Then
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function Fill_LockMessages(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim oResultTable As DataTable
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
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oParamater) Then  'Disposed by mitesh
                oParamater = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function IsContainHighPriorityMessage(ByVal nUserID As Int64) As Boolean

        '08-May-13 Aniket: Resolving Memory Leaks
        Dim oResult As Int32

        Try

            Dim Con As New SqlClient.SqlConnection(GetConnectionString)


            'Dim _Query As String
            '_Query = " SELECT COUNT(*) FROM Message INNER JOIN Patient ON Message.nPatientID = Patient.nPatientID " _
            '    & " INNER JOIN Message_DTL ON Message_DTL.nMessageID = Message.nMessageID " _
            '    & " WHERE Message_DTL.nToID = " & nUserID & "  AND  Message.bIsFinished = 0 " _
            '    & " AND  Message_DTL.bIsReplied = 0 AND nPriority = 3"

            '02-Dec-14 Aniket: Patient switching optimization
            Dim cmd As New SqlClient.SqlCommand("gsp_GetAllPatientMessages", Con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            cmd.Parameters("@nUserID").Value = nUserID

            Con.Open()
            oResult = cmd.ExecuteScalar
            Con.Close()

            If Not IsNothing(cmd) Then   'Disposed by mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If


            If oResult > 0 Then
                Return True
            End If

            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            '08-May-13 Aniket: Resolving Memory Leaks
            oResult = Nothing

        End Try

    End Function

    Public Sub New()

    End Sub

    ' Implement IDisposable.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                ' Free other state (managed objects).
                disposed = True
            End If
            ''slr free dv
            If Not IsNothing(dv) Then
                dv.Dispose()
                dv = Nothing
            End If
            'If Not IsNothing(ds) Then
            '    ds.Dispose()
            '    ds = Nothing
            'End If

            'slr free dt
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        ' Simply call Dispose(False).
        Dispose(False)
    End Sub

End Class
