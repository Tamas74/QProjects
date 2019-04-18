Imports System.Data.SqlClient
Public Class ClsSmartorderDBLayer
    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Public Sub Dispose()

        If Conn IsNot Nothing Then
            Conn.Dispose()
            Conn = Nothing

        End If
        'If IsNothing(dv) = False Then
        '    dv.Dispose()
        '    dv = Nothing
        'End If
        'If IsNothing(Ds) = False Then
        '    Ds.Dispose()
        '    Ds = Nothing
        'End If
    End Sub
    Private Conn As SqlConnection
    'Private Cmd As SqlCommand
    'Private sda As New SqlDataAdapter
    ''' <summary>
    ''' Fill The Control Like OrderSet,Labs,Radiology and Template
    ''' </summary>
    ''' <param name="ID"></param>
    ''' <returns> Oderset,Labs,Radiology and Template</returns>
    ''' <remarks> ID will decide which data will this function will return</remarks>
    Public Function FillControl(ByVal ID As Long) As DataTable
        Conn.Open()
        Dim dt As New DataTable
        Dim Cmd As SqlCommand = New SqlCommand("gsp_FillOderset", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim sda As New SqlDataAdapter
        sda.SelectCommand = Cmd

        Dim objParam As SqlParameter
        objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = ID  '' fla

        sda.Fill(dt)
        Conn.Close()

        If Cmd IsNot Nothing Then
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
        End If
        sda.Dispose()
        sda = Nothing

        objParam = Nothing
        Return dt

    End Function

    '' COMMENT BY SUDHIR 20090429 '' SMART ORDER DENORMALIZATION ''
    'Public Function AddData(ByVal OrderID As Long, ByVal OrderName As String, ByVal arrlist As ArrayList) As Boolean
    '    If Conn.State = ConnectionState.Closed Then
    '        Conn.Open()
    '    End If

    '    Dim trOrderAssociation As SqlTransaction
    '    trOrderAssociation = Conn.BeginTransaction
    '    Dim cmddelete As SqlCommand
    '    Try
    '        Dim i As Integer
    '        Dim objparam As SqlParameter

    '        cmddelete = New System.Data.SqlClient.SqlCommand("sp_DeleteOrders", Conn)
    '        cmddelete.CommandType = CommandType.StoredProcedure
    '        cmddelete.Transaction = trOrderAssociation

    '        objparam = cmddelete.Parameters.Add("@nOrderID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = OrderID


    '        cmddelete.ExecuteNonQuery()
    '        cmddelete.Parameters.Clear()


    '        For i = 0 To arrlist.Count - 1
    '            Dim objmylist As myList
    '            objmylist = CType(arrlist.Item(i), myList)


    '            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertOrders", Conn)

    '            Cmd.CommandType = CommandType.StoredProcedure
    '            Cmd.Transaction = trOrderAssociation

    '            objparam = Cmd.Parameters.Add("@nOrderID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = OrderID

    '            objparam = Cmd.Parameters.Add("@nAssociateID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = objmylist.Index


    '            objparam = Cmd.Parameters.Add("@AssociateType", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            If objmylist.Description = "L" Then
    '                objparam.Value = "L"
    '            ElseIf objmylist.Description = "R" Then
    '                objparam.Value = "R"
    '            ElseIf objmylist.Description = "T" Then
    '                objparam.Value = "T"
    '            ElseIf objmylist.Description = "D" Then
    '                objparam.Value = "D"
    '            End If

    '            ''Sandip Darade 20090401
    '            '' Add name of the associate ..
    '            objparam = Cmd.Parameters.Add("@sAssociateName", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = objmylist.ParameterName

    '            '''' Insert Order Lab 

    '            Cmd.ExecuteNonQuery()
    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, "Order Association Added to Lab for " & CType(OrderName, String), gloAuditTrail.ActivityOutCome.Success)
    '            'Dim objAudit As New clsAudit
    '            'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Order Association Added to Lab for " & CType(OrderName, String), gstrLoginName, gstrClientMachineName)
    '            'objAudit = Nothing


    '            Cmd.Parameters.Clear()

    '        Next



    '        trOrderAssociation.Commit()
    '        Conn.Close()
    '        Return True

    '    Catch ex As SqlException
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        'UpdateLog("ClsSmartorderDBLayer - AddData : " & ex.ToString)
    '        trOrderAssociation.Rollback()
    '        trOrderAssociation = Nothing
    '        Cmd = Nothing
    '        cmddelete = Nothing
    '        Conn.Close()
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        'trMedication.Rollback()
    '        trOrderAssociation.Rollback()
    '        trOrderAssociation = Nothing
    '        Cmd = Nothing
    '        cmddelete = Nothing
    '        Conn.Close()
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function

    Public Function AddData(ByVal OrderID As Long, ByVal OrderName As String, ByVal arrlist As ArrayList) As Boolean
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        Dim trOrderAssociation As SqlTransaction
        trOrderAssociation = Conn.BeginTransaction
        Dim cmddelete As SqlCommand = Nothing
        Dim oParam As SqlParameter
        Try
            Dim i As Integer


            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteOrders_Patient", Conn)  ''Rename sp_DeleteOrders to gsp_DeleteOrders_Patient
            cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteOrders", Conn)  ''Rename sp_DeleteOrders to gsp_DeleteOrders_Patient
            cmddelete.CommandType = CommandType.StoredProcedure
            cmddelete.Transaction = trOrderAssociation

            oParam = cmddelete.Parameters.Add("@nOrderID", SqlDbType.BigInt)
            oParam.Direction = ParameterDirection.Input
            oParam.Value = OrderID


            cmddelete.ExecuteNonQuery()
            cmddelete.Parameters.Clear()


            For i = 0 To arrlist.Count - 1
                Dim oMyList As myList
                oMyList = CType(arrlist.Item(i), myList)


                Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InsertOrders", Conn)

                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Transaction = trOrderAssociation

                oParam = Cmd.Parameters.Add("@nOrderID", SqlDbType.BigInt)
                oParam.Value = OrderID

                oParam = Cmd.Parameters.Add("@nAssociateID", SqlDbType.BigInt)
                oParam.Value = oMyList.Index

                oParam = Cmd.Parameters.Add("@AssociateType", SqlDbType.VarChar)
                oParam.Value = oMyList.Description

                ''Sandip Darade 20090401
                '' Add name of the associate ..
                oParam = Cmd.Parameters.Add("@sAssociateName", SqlDbType.VarChar)
                If oMyList.Description = "D" Then ''''means Drug, then save the oMyList.Drugname val in sAssociateName field, else save the oMyList.ParameterName, becaz when the drug is shown in Rx-Meds for it shows with concated drugname and Drug Form
                    oParam.Value = oMyList.DrugName
                Else
                    oParam.Value = oMyList.ParameterName
                End If

                oParam = Cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                oParam.Value = oMyList.Dosage

                oParam = Cmd.Parameters.Add("@sDrugForm", SqlDbType.VarChar)
                oParam.Value = oMyList.DrugForm

                oParam = Cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
                If IsNothing(oMyList.Route) Then
                    oParam.Value = ""
                Else
                    oParam.Value = oMyList.Route
                End If

                oParam = Cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
                If IsNothing(oMyList.Frequency) Then
                    oParam.Value = ""
                Else
                    oParam.Value = oMyList.Frequency
                End If

                oParam = Cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                If IsNothing(oMyList.NDCCode) Then
                    oParam.Value = ""
                Else
                    oParam.Value = oMyList.NDCCode
                End If

                oParam = Cmd.Parameters.Add("@nIsNarcotics", SqlDbType.Int)
                If IsNothing(oMyList.IsNarcotic) Then
                    oParam.Value = ""
                Else
                    oParam.Value = oMyList.IsNarcotic
                End If

                oParam = Cmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
                If IsNothing(oMyList.Duration) Then
                    oParam.Value = ""
                Else
                    oParam.Value = oMyList.Duration
                End If

                oParam = Cmd.Parameters.Add("@mpid", SqlDbType.Int)
                If IsNothing(oMyList.mpid) Then
                    oParam.Value = 0
                Else
                    oParam.Value = oMyList.mpid
                End If

                oParam = Cmd.Parameters.Add("@sDrugQtyQualifier", SqlDbType.VarChar)
                If IsNothing(oMyList.DrugQtyQualifier) Then
                    oParam.Value = ""
                Else
                    oParam.Value = oMyList.DrugQtyQualifier
                End If
                '''' Insert Order Lab 

                ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                oParam = Cmd.Parameters.Add("@bStatus", SqlDbType.Bit)
                oParam.Direction = ParameterDirection.Input
                oParam.Value = oMyList.ItemChecked                
                ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012

                Cmd.ExecuteNonQuery()
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, "Order Association Added to Lab for " & CType(OrderName, String), gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, "Order Association Added to Lab for " & CType(OrderName, String), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'Dim objAudit As New clsAudit
                'objAudit.CreateLog(clsAudit.enmActivityType.Add, "Order Association Added to Lab for " & CType(OrderName, String), gstrLoginName, gstrClientMachineName)
                'objAudit = Nothing
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            Next

            trOrderAssociation.Commit()
            Conn.Close()
            Return True

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsSmartorderDBLayer - AddData : " & ex.ToString)
            trOrderAssociation.Rollback()
            trOrderAssociation = Nothing
            '   Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'trMedication.Rollback()
            trOrderAssociation.Rollback()
            trOrderAssociation = Nothing
            '       Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If cmddelete IsNot Nothing Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            If trOrderAssociation IsNot Nothing Then

                trOrderAssociation.Dispose()
                trOrderAssociation = Nothing
            End If

            oParam = Nothing
        End Try
    End Function

    Public Function FillFlowsheet() As DataTable
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Dim strquery As String = " SELECT DISTINCT nFlowSheetID AS nFlowSheetID, sFlowSheetName FROM FlowSheet_MST " _
                         & "  ORDER BY sFlowSheetName"

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand(strquery, Conn)

            sqladpt.SelectCommand = Cmd





            sqladpt.Fill(dt)
            Conn.Close()
            sqladpt.Dispose()
            sqladpt = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsSmartorderDBLayer - FetchOrderforUpdate : " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        End Try
    End Function

    Public Function FetchOrderforUpdate(ByVal id As Long) As DataTable
        Dim objParam As SqlParameter
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanOrderAssociation", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            objParam = Cmd.Parameters.Add("@nOrderID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            sqladpt.Fill(dt)
            Conn.Close()
            sqladpt.Dispose()
            sqladpt = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("ClsSmartorderDBLayer - FetchOrderforUpdate : " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        Finally
            objParam = Nothing
        End Try
    End Function


    Public Function FetchLabName(ByVal ID As Long) As String
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim cmd As SqlCommand = Nothing
            Dim LabName As String = ""
            Dim strSelectQry As String = "Select labtm_Name From Lab_Test_Mst where labtm_ID = '" & ID & "'"
            cmd = New SqlCommand(strSelectQry, Conn)
            LabName = Convert.ToString(cmd.ExecuteScalar())

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return LabName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''connection state close
            If Not IsNothing(Conn) Then
                If (Conn.State = ConnectionState.Open) Then
                    Conn.Close()
                End If
            End If

        End Try
    End Function
   
    Public Function FetchTemplateName(ByVal ID As Long) As String
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim cmd As SqlCommand = Nothing
            Dim TemplateName As String = ""
            Dim strSelectQry As String = "Select sTemplateName From TemplateGallery_MST where nTemplateID = '" & ID & "'"
            cmd = New SqlCommand(strSelectQry, Conn)

            TemplateName = Convert.ToString(cmd.ExecuteScalar())
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return TemplateName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ''connection state close
            If Not IsNothing(Conn) Then
                If (Conn.State = ConnectionState.Open) Then
                    Conn.Close()
                End If
            End If

        End Try
    End Function

    Public Function FetchRadiologyName(ByVal ID As Long) As String
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim cmd As SqlCommand = Nothing
            Dim RadiologyName As String = ""
            Dim strSelectQry As String = "Select lm_test_Name From LM_Test where lm_test_ID = '" & ID & "'"
            cmd = New SqlCommand(strSelectQry, Conn)

            RadiologyName = Convert.ToString(cmd.ExecuteScalar())
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return RadiologyName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            '' connection state closed
            If Not IsNothing(Conn) Then
                If (Conn.State = ConnectionState.Open) Then
                    Conn.Close()
                End If
            End If

        End Try
    End Function

    Public Function FetchDrugName(ByVal ID As Long) As String
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim cmd As SqlCommand = Nothing
            Dim DrugName As String = ""
            Dim strSelectQry As String = "Select sDrugName From Drugs_MST where nDrugsID = '" & ID & "'"
            cmd = New SqlCommand(strSelectQry, Conn)

            DrugName = Convert.ToString(cmd.ExecuteScalar())
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Return DrugName
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartOrders, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class
