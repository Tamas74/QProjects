Imports System.Data.SqlClient
Namespace gloStream
    Namespace Immunization

        Public Class Transaction

            Private _errorMessage As String

            Public Property ErrorMessage() As String
                Get
                    Return _errorMessage
                End Get
                Set(ByVal Value As String)
                    _errorMessage = Value
                End Set
            End Property

            Public Function Add_OLD(ByVal oTransaction As gloStream.Immunization.Supporting.ImmunizationTransaction) As Boolean
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)

                ' Dim oTranLines As New gloStream.Immunization.Supporting.ImmunizationTransactionLines
                Dim oTranLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine

                'Dim oDataReader As SqlClient.SqlDataReader
                Dim _strSQL As String = ""
                Dim _Result As Boolean = False


                Dim myTrans As SqlClient.SqlTransaction = Nothing
                Dim cmdItemTrans As SqlClient.SqlCommand = Nothing

                Dim MachineID As Long
                Dim Trans_ID As Long

                Try

                    'table used --- IM_Trn_Mst
                    conn.Open()

                    myTrans = conn.BeginTransaction

                    cmdItemTrans = conn.CreateCommand
                    cmdItemTrans.Transaction = myTrans

                    With cmdItemTrans
                        .Connection = conn
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "IM_InsUPIMTransRec"
                    End With

                    Dim objparam As New SqlClient.SqlParameter("@im_trn_mst_Id", SqlDbType.BigInt)
                    With cmdItemTrans.Parameters
                        objparam.Direction = ParameterDirection.InputOutput
                        .Add(objparam)
                        .Add("@MachineID", SqlDbType.BigInt)
                        .Add("@im_trn_mst_nPatientID", SqlDbType.BigInt)
                    End With

                    With cmdItemTrans
                        MachineID = GetPrefixTransactionID()
                        objparam.Value = Trans_ID
                        .Parameters("@MachineID").Value = MachineID
                        .Parameters("@im_trn_mst_nPatientID").Value = oTransaction.PatientID
                    End With

                    cmdItemTrans.ExecuteNonQuery()


                    If Not IsNothing(objparam) Then
                        Trans_ID = objparam.Value
                        '//MsgBox(Criteria_ID)
                    End If

                    If Trans_ID > 0 Then
                        'insert record in detail table IM_Trn_Dtl

                        For i As Integer = 1 To oTransaction.TransactionLines.Count

                            '    oTranLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
                            oTranLine = oTransaction.TransactionLines(i)

                            cmdItemTrans.Parameters.Clear()
                            Dim bln1 As Boolean
                            Dim bln2 As Boolean
                            Dim bln3 As Boolean
                            Dim bln4 As Boolean
                            With cmdItemTrans
                                .Connection = conn
                                .CommandType = CommandType.StoredProcedure
                                .CommandText = "IM_InsUPIMTransDtl"
                            End With

                            With cmdItemTrans.Parameters
                                .Add("@im_trn_mst_Id", SqlDbType.BigInt)
                                .Add("@im_trn_Date", SqlDbType.DateTime)
                                .Add("@im_trn_nVisitID", SqlDbType.BigInt)
                                .Add("@im_trn_ItemID", SqlDbType.BigInt)
                                .Add("@im_trn_CounterID", SqlDbType.BigInt)
                                .Add("@im_trn_Dose", SqlDbType.VarChar)
                                .Add("@im_trn_Dategiven", SqlDbType.DateTime)
                                .Add("@im_trn_Route", SqlDbType.VarChar)
                                .Add("@im_trn_Lotnumber", SqlDbType.VarChar)
                                .Add("@im_trn_Expirationdate", SqlDbType.DateTime)
                                .Add("@im_trn_Manufacturer", SqlDbType.VarChar)
                                .Add("@im_trn_Userid", SqlDbType.BigInt)
                                .Add("@im_trn_Notes", SqlDbType.VarChar)

                                '' modification on 20070425 for CCHIT 2007
                                .Add("@im_trn_Site", SqlDbType.VarChar)
                                ''

                                ''code added by Bipin ON 20070205
                                .Add("@im_trn_duedate", SqlDbType.DateTime)
                                .Add("@bln1", SqlDbType.Bit)
                                .Add("@bln2", SqlDbType.Bit)
                                .Add("@bln3", SqlDbType.Bit)
                                .Add("@bln4", SqlDbType.Bit)
                                ''''
                            End With

                            With cmdItemTrans
                                .Parameters("@im_trn_mst_Id").Value = Trans_ID
                                If oTranLine.TransactionDate <> "#12:00:00 AM#" AndAlso IsDate(oTranLine.TransactionDate) Then
                                    .Parameters("@im_trn_Date").Value = oTranLine.TransactionDate
                                Else
                                    .Parameters("@im_trn_Date").Value = Now
                                    bln1 = True
                                End If

                                .Parameters("@im_trn_nVisitID").Value = oTranLine.VisitID
                                .Parameters("@im_trn_ItemID").Value = oTranLine.ItemID
                                .Parameters("@im_trn_CounterID").Value = oTranLine.ItemCounterID
                                .Parameters("@im_trn_Dose").Value = oTranLine.Dose
                                If IsDate(oTranLine.DateGiven) AndAlso oTranLine.DateGiven <> "#12:00:00 AM#" Then
                                    .Parameters("@im_trn_Dategiven").Value = oTranLine.DateGiven
                                Else
                                    .Parameters("@im_trn_Dategiven").Value = Now
                                    bln2 = True
                                End If

                                .Parameters("@im_trn_Route").Value = oTranLine.Route
                                .Parameters("@im_trn_Lotnumber").Value = oTranLine.LotNumber
                                If IsDate(oTranLine.ExpiryDate) AndAlso oTranLine.ExpiryDate <> "#12:00:00 AM#" Then
                                    .Parameters("@im_trn_Expirationdate").Value = oTranLine.ExpiryDate
                                Else
                                    .Parameters("@im_trn_Expirationdate").Value = Now
                                    bln3 = True
                                End If

                                .Parameters("@im_trn_Manufacturer").Value = oTranLine.Manufacturer
                                .Parameters("@im_trn_Userid").Value = oTranLine.UserID
                                .Parameters("@im_trn_Notes").Value = oTranLine.Notes
                                ''code added by Bipin ON 20070205
                                If IsDate(oTranLine.DueDate) AndAlso oTranLine.DueDate <> "#12:00:00 AM#" Then
                                    .Parameters("@im_trn_duedate").Value = oTranLine.DueDate
                                Else
                                    .Parameters("@im_trn_duedate").Value = Now
                                    bln4 = True
                                End If

                                '' modification on 20070425 for CCHIT 2007
                                .Parameters("@im_trn_Site").Value = oTranLine.Site
                                ''

                                .Parameters("@bln1").Value = bln1
                                .Parameters("@bln2").Value = bln2
                                .Parameters("@bln3").Value = bln3
                                .Parameters("@bln4").Value = bln4
                                '.Parameters("@duedate").Value = oTranLine.DueDate
                                ''
                            End With

                            If cmdItemTrans.ExecuteNonQuery() > 0 Then

                            End If

                            oTranLine = Nothing
                        Next
                    End If

                    myTrans.Commit()
                    _Result = True
                    objparam = Nothing
                    Return _Result

                Catch ex As Exception
                    Try
                        myTrans.Rollback()
                    Catch ex1 As SqlClient.SqlException

                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        If Not myTrans.Connection Is Nothing Then
                            'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                            '" was encountered while attempting to roll back the transaction.")
                            _errorMessage = ex.Message
                        End If
                    End Try

                    _errorMessage = ex.Message
                    _errorMessage = "Neither record was written to database."
                    Return False
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                        conn.Dispose()
                        conn = Nothing
                    End If
                    If Not IsNothing(cmdItemTrans) Then
                        cmdItemTrans.Parameters.Clear()
                        cmdItemTrans.Dispose()
                        cmdItemTrans = Nothing
                    End If
                    If Not IsNothing(myTrans) Then
                        myTrans.Dispose()
                        myTrans = Nothing
                    End If
                End Try
            End Function

            Public Function Add(ByVal oTransaction As gloStream.Immunization.Supporting.ImmunizationTransaction) As Boolean
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _Result As Boolean = False
                'Dim MachineID As Long
                Dim Trans_ID As Long = 0

                Try

                    oDB.Connect(GetConnectionString)

                    oDB.DBParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@im_trn_mst_nPatientID", oTransaction.PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@im_trn_mst_Id", 0, ParameterDirection.InputOutput, SqlDbType.BigInt)
                    Trans_ID = oDB.ExecuteNonQueryForOutput("IM_InsUPIMTransRec")


                    If Trans_ID > 0 Then
                        For i As Int16 = 1 To oTransaction.TransactionLines.Count
                            oDB.DBParameters.Clear()
                            Dim _bln1 As Boolean = 0
                            Dim _bln2 As Boolean = 0
                            Dim _bln3 As Boolean = 0
                            Dim _bln4 As Boolean = 0

                            With oTransaction.TransactionLines(i)
                                oDB.DBParameters.Add("@im_trn_mst_Id", Trans_ID, ParameterDirection.Input, SqlDbType.BigInt)
                                If .TransactionDate <> "#12:00:00 AM#" AndAlso IsDate(.TransactionDate) Then
                                    oDB.DBParameters.Add("@im_trn_Date", .TransactionDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Date", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln1 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_nVisitID", .VisitID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_ItemID", .ItemID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_CounterID", .ItemCounterID, ParameterDirection.Input, SqlDbType.Int)
                                oDB.DBParameters.Add("@im_trn_Dose", .Dose, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .DateGiven <> "#12:00:00 AM#" AndAlso IsDate(.DateGiven) Then
                                    oDB.DBParameters.Add("@im_trn_Dategiven", .DateGiven, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Dategiven", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln2 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_Timegiven", .TimeGiven, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Route", .Route, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Lotnumber", .LotNumber, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .ExpiryDate <> "#12:00:00 AM#" AndAlso IsDate(.ExpiryDate) Then
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", .ExpiryDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln3 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_Manufacturer", .Manufacturer, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Userid", .UserID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_Notes", .Notes, ParameterDirection.Input, SqlDbType.VarChar, 255)
                                If .DueDate <> "#12:00:00 AM#" AndAlso IsDate(.DueDate) Then
                                    oDB.DBParameters.Add("@im_trn_duedate", .DueDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_duedate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln4 = 1
                                End If

                                '' modification on 20070425 for CCHIT 2007
                                oDB.DBParameters.Add("@im_trn_Site", .Site, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                ''
                                oDB.DBParameters.Add("@bln1", _bln1, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln2", _bln2, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln3", _bln3, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln4", _bln4, ParameterDirection.Input, SqlDbType.Bit)
                                ''
                                oDB.DBParameters.Add("@im_reminder", .IsReminder, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@im_vaccine_eligibilitycode", .EligibilityCode, ParameterDirection.Input, SqlDbType.VarChar, 10)


                                '*******5 new parameters added namely im_item_name, im_item_count, im_vaccine_code, im_cpt_code, im_reasonfor_nonadmin

                                oDB.DBParameters.Add("@im_item_name", .ItemName, ParameterDirection.Input, SqlDbType.VarChar, 50)


                                'fetch the im_item_Count value from the IM_MST table and pass it to the @im_item_Count feild in the Im_Trn_Dtl table.
                                'line modify and commented by dipak to fix bug no 4752 :While Generating Immunization Due Report it generates connection error
                                'Dim strSQL As String = "select im_item_Count from IM_MST where im_item_Id = " & .ItemID & " and im_item_Name = " & "'" & .ItemName & "'" & ""
                                Dim strSQL As String = "select im_item_Count from IM_MST where im_item_Id = " & .ItemID & " and im_item_Name = " & "'" & Replace(.ItemName, "'", "''") & "'" & ""
                                Dim ItemCount As String = oDB.ExecuteQueryScaler(strSQL)
                                Dim IM_Mst_ItemCount As Int16 = Convert.ToInt16(ItemCount)

                                oDB.DBParameters.Add("@im_item_count", IM_Mst_ItemCount, ParameterDirection.Input, SqlDbType.Int)

                                'query the itemId and get the vaccine code for that item select im_vaccine_codefrom IM_MST where im_item_Id = .itemId



                                'pass the selected CPT code
                                'If IsDBNull(oDataTable.Rows(0)("im_cpt_code")) Then
                                '    CPTCode = ""
                                'Else
                                '    CPTCode = oDataTable.Rows(0)("im_cpt_code") 'here we take Zeroth row becaz the query will return only one row

                                'End If

                                'CPTCode = oDataTable.Rows(0)("im_cpt_code") 'here we take Zeroth row becaz the query will return only one row



                                oDB.DBParameters.Add("@im_vaccine_code", .VaccineCode, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@im_cpt_code", .CPTCode, ParameterDirection.Input, SqlDbType.VarChar)

                                oDB.DBParameters.Add("@im_reasonfor_nonadmin", .ReasonForNonAdmin, ParameterDirection.Input, SqlDbType.VarChar)

                                '**************************************************************************

                            End With
                            oDB.ExecuteNonQuery("IM_InsUPIMTransDtl")
                           
                        Next
                    End If

                    oDB.Disconnect()

               
                    _Result = True

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Add, "Immunization Sheet Added", oTransaction.PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Immunization Sheet Added", gstrLoginName, gstrClientMachineName, gnPatientID)

                    Return _Result

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return False
                Finally
                    If Not IsNothing(oDB) Then    'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
            End Function

            Public Function Add_SnoMed(ByVal oTransaction As gloStream.Immunization.Supporting.ImmunizationTransaction) As Boolean
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _Result As Boolean = False
                'Dim MachineID As Long
                Dim Trans_ID As Long = 0

                Try

                    oDB.Connect(GetConnectionString)

                    oDB.DBParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@im_trn_mst_nPatientID", oTransaction.PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@im_trn_mst_Id", 0, ParameterDirection.InputOutput, SqlDbType.BigInt)
                    Trans_ID = oDB.ExecuteNonQueryForOutput("IM_InsUPIMTransRec")


                    If Trans_ID > 0 Then
                        For i As Int16 = 1 To oTransaction.TransactionLines.Count
                            oDB.DBParameters.Clear()
                            Dim _bln1 As Boolean = 0
                            Dim _bln2 As Boolean = 0
                            Dim _bln3 As Boolean = 0
                            Dim _bln4 As Boolean = 0

                            With oTransaction.TransactionLines(i)
                                oDB.DBParameters.Add("@im_trn_mst_Id", Trans_ID, ParameterDirection.Input, SqlDbType.BigInt)
                                If .TransactionDate <> "#12:00:00 AM#" AndAlso IsDate(.TransactionDate) Then
                                    oDB.DBParameters.Add("@im_trn_Date", .TransactionDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Date", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln1 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_nVisitID", .VisitID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_ItemID", .ItemID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_CounterID", .ItemCounterID, ParameterDirection.Input, SqlDbType.Int)
                                oDB.DBParameters.Add("@im_trn_Dose", .Dose, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .DateGiven <> "#12:00:00 AM#" AndAlso IsDate(.DateGiven) Then
                                    oDB.DBParameters.Add("@im_trn_Dategiven", .DateGiven, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Dategiven", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln2 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_Timegiven", .TimeGiven, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Route", .Route, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Lotnumber", .LotNumber, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .ExpiryDate <> "#12:00:00 AM#" AndAlso IsDate(.ExpiryDate) Then
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", .ExpiryDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln3 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_Manufacturer", .Manufacturer, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Userid", .UserID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_Notes", .Notes, ParameterDirection.Input, SqlDbType.VarChar, 255)
                                If .DueDate <> "#12:00:00 AM#" AndAlso IsDate(.DueDate) Then
                                    oDB.DBParameters.Add("@im_trn_duedate", .DueDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_duedate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln4 = 1
                                End If

                                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110

                                oDB.DBParameters.Add("@im_trn_Reaction", .Reaction, ParameterDirection.Input, SqlDbType.VarChar, 255)
                                If .Reaction.Trim() <> "" Then
                                    If .ReactionDT <> "#12:00:00 AM#" AndAlso IsDate(.ReactionDT) Then
                                        oDB.DBParameters.Add("@im_trn_ReactionDT", .ReactionDT, ParameterDirection.Input, SqlDbType.DateTime)
                                    Else
                                        oDB.DBParameters.Add("@im_trn_ReactionDT", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    End If
                                Else
                                    oDB.DBParameters.Add("@im_trn_ReactionDT", "01/01/1900", ParameterDirection.Input, SqlDbType.DateTime)
                                End If
                                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                                '' modification on 20070425 for CCHIT 2007

                                oDB.DBParameters.Add("@im_trn_Site", .Site, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                ''
                                oDB.DBParameters.Add("@bln1", _bln1, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln2", _bln2, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln3", _bln3, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln4", _bln4, ParameterDirection.Input, SqlDbType.Bit)
                                ''
                                oDB.DBParameters.Add("@im_reminder", .IsReminder, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@im_vaccine_eligibilitycode", .EligibilityCode, ParameterDirection.Input, SqlDbType.VarChar, 10)


                                '*******5 new parameters added namely im_item_name, im_item_count, im_vaccine_code, im_cpt_code, im_reasonfor_nonadmin

                                oDB.DBParameters.Add("@im_item_name", .ItemName, ParameterDirection.Input, SqlDbType.VarChar, 50)


                                'fetch the im_item_Count value from the IM_MST table and pass it to the @im_item_Count feild in the Im_Trn_Dtl table.
                                'line modify and commented by dipak to fix bug no 4752 :While Generating Immunization Due Report it generates connection error
                                'Dim strSQL As String = "select im_item_Count from IM_MST where im_item_Id = " & .ItemID & " and im_item_Name = " & "'" & .ItemName & "'" & ""
                                'Dim strSQL As String = "select im_item_Count from IM_MST where im_item_Id = " & .ItemID & " and im_item_Name = " & "'" & Replace(.ItemName, "'", "''") & "'" & ""
                                'Dim ItemCount As String = oDB.ExecuteQueryScaler(strSQL)
                                Dim IM_Mst_ItemCount As Int16 = 1 'Convert.ToInt16(ItemCount)

                                oDB.DBParameters.Add("@im_item_count", IM_Mst_ItemCount, ParameterDirection.Input, SqlDbType.Int)

                                'query the itemId and get the vaccine code for that item select im_vaccine_codefrom IM_MST where im_item_Id = .itemId



                                'pass the selected CPT code
                                'If IsDBNull(oDataTable.Rows(0)("im_cpt_code")) Then
                                '    CPTCode = ""
                                'Else
                                '    CPTCode = oDataTable.Rows(0)("im_cpt_code") 'here we take Zeroth row becaz the query will return only one row

                                'End If

                                'CPTCode = oDataTable.Rows(0)("im_cpt_code") 'here we take Zeroth row becaz the query will return only one row



                                oDB.DBParameters.Add("@im_vaccine_code", .VaccineCode, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@im_cpt_code", .CPTCode, ParameterDirection.Input, SqlDbType.VarChar)

                                oDB.DBParameters.Add("@im_reasonfor_nonadmin", .ReasonForNonAdmin, ParameterDirection.Input, SqlDbType.VarChar)


                                oDB.DBParameters.Add("@sConceptID", .ConceptID, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sDescriptionID", .DescriptionID, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sSnoMedID", .SnoMedID, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sDescription", "", ParameterDirection.Input, SqlDbType.VarChar)
                                'Sanjog - Added on 2011 March 19 to add the Dose unit value when adding new Immunization
                                oDB.DBParameters.Add("@sTranID1", .DoseUnit, ParameterDirection.Input, SqlDbType.VarChar)
                                'Sanjog - Added on 2011 March 19 to add Dose unit value when adding new Immunization
                                oDB.DBParameters.Add("@sTranID2", .AdminStatus, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sTranID3", "", ParameterDirection.Input, SqlDbType.VarChar)

                                '**************************************************************************

                            End With
                            oDB.ExecuteNonQuery("IM_InsUPIMTransDtl_SnoMed")

                        Next
                        'Code Start-Added by kanchan on 20101029 for HL7 setting for immunization
                        Dim oGeneralInterface As New clsGeneralInterface()



                        '   If gblnSendChargesToHL7 = True Then
                        If gblnHL7SENDOUTBOUNDGLOEMR = True AndAlso gblnSendImmunization = True Then
                            oGeneralInterface.SendImmunization("V04", Trans_ID, oTransaction.PatientID)
                        End If

                        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolIMRegistryHL7Format Then
                            oGeneralInterface.SendImmunization("V04IR", Trans_ID, oTransaction.PatientID)
                        End If
                        oGeneralInterface.Dispose()
                        oGeneralInterface = Nothing
                        'End If
                    End If

                    oDB.Disconnect()

                    _Result = True

                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Add, "Immunization Sheet Added", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Add, "Immunization Sheet Added", oTransaction.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Immunization Sheet Added", gstrLoginName, gstrClientMachineName, gnPatientID)

                    Return _Result

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return False
                Finally
                    If Not IsNothing(oDB) Then    'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
            End Function

            Public Function Modify_SnoMed(ByVal ModifyID As Long, ByVal oTransaction As gloStream.Immunization.Supporting.ImmunizationTransaction) As Boolean
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _Result As Boolean = False
                Dim Trans_ID As Long = ModifyID

                Try
                    oDB.Connect(GetConnectionString)
                    Dim _strSQL As String = "delete from IM_Trn_Dtl where im_trn_mst_Id = " & ModifyID
                    oDB.ExecuteNonSQLQuery(_strSQL)


                    If Trans_ID > 0 Then
                        For i As Int16 = 1 To oTransaction.TransactionLines.Count
                            oDB.DBParameters.Clear()
                            Dim _bln1 As Boolean = 0
                            Dim _bln2 As Boolean = 0
                            Dim _bln3 As Boolean = 0
                            Dim _bln4 As Boolean = 0

                            With oTransaction.TransactionLines(i)
                                oDB.DBParameters.Add("@im_trn_mst_Id", Trans_ID, ParameterDirection.Input, SqlDbType.BigInt)
                                If .TransactionDate <> "#12:00:00 AM#" AndAlso IsDate(.TransactionDate) Then
                                    oDB.DBParameters.Add("@im_trn_Date", .TransactionDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Date", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln1 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_nVisitID", .VisitID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_ItemID", .ItemID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_CounterID", .ItemCounterID, ParameterDirection.Input, SqlDbType.Int)
                                oDB.DBParameters.Add("@im_trn_Dose", .Dose, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .DateGiven <> "#12:00:00 AM#" AndAlso IsDate(.DateGiven) Then
                                    oDB.DBParameters.Add("@im_trn_Dategiven", .DateGiven, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Dategiven", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln2 = 1
                                End If

                                If IsNothing(.TimeGiven) Then
                                    oDB.DBParameters.Add("@im_trn_Timegiven", "", ParameterDirection.Input, SqlDbType.VarChar, 50)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Timegiven", .TimeGiven, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                End If

                                oDB.DBParameters.Add("@im_trn_Route", .Route, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Lotnumber", .LotNumber, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .ExpiryDate <> "#12:00:00 AM#" AndAlso IsDate(.ExpiryDate) Then
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", .ExpiryDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln3 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_Manufacturer", .Manufacturer, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Userid", .UserID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_Notes", .Notes, ParameterDirection.Input, SqlDbType.VarChar, 255)
                                If .DueDate <> "#12:00:00 AM#" AndAlso IsDate(.DueDate) Then
                                    oDB.DBParameters.Add("@im_trn_duedate", .DueDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_duedate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln4 = 1
                                End If
                                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                                oDB.DBParameters.Add("@im_trn_Reaction", .Reaction, ParameterDirection.Input, SqlDbType.VarChar, 255)
                                If .Reaction.Trim() <> "" Then
                                    If .ReactionDT <> "#12:00:00 AM#" AndAlso IsDate(.ReactionDT) Then
                                        oDB.DBParameters.Add("@im_trn_ReactionDT", .ReactionDT, ParameterDirection.Input, SqlDbType.DateTime)
                                    Else
                                        oDB.DBParameters.Add("@im_trn_ReactionDT", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    End If
                                Else
                                    oDB.DBParameters.Add("@im_trn_ReactionDT", "01/01/1900", ParameterDirection.Input, SqlDbType.DateTime)
                                End If
                                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 220110
                                '' modification on 20070425 for CCHIT 2007
                                oDB.DBParameters.Add("@im_trn_Site", .Site, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                ''
                                oDB.DBParameters.Add("@bln1", _bln1, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln2", _bln2, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln3", _bln3, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln4", _bln4, ParameterDirection.Input, SqlDbType.Bit)
                                ''
                                oDB.DBParameters.Add("@im_reminder", .IsReminder, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@im_vaccine_eligibilitycode", .EligibilityCode, ParameterDirection.Input, SqlDbType.VarChar, 10)
                                '*******5 new parameters added namely im_item_name, im_item_count, im_vaccine_code, im_cpt_code, im_reasonfor_nonadmin

                                oDB.DBParameters.Add("@im_item_name", .ItemName, ParameterDirection.Input, SqlDbType.VarChar, 50)

                                'fetch the im_item_Count value from the IM_MST table and pass it to the @im_item_Count feild in the Im_Trn_Dtl table.
                                'Dim strSQL As String = "select im_item_Count from IM_MST where im_item_Id = " & .ItemID & " and im_item_Name = " & "'" & .ItemName.Replace("'", "''") & "'" & ""
                                'Dim ItemCount As String = oDB.ExecuteQueryScaler(strSQL)
                                Dim IM_Mst_ItemCount As Int16 = 1 'Convert.ToInt16(ItemCount)

                                oDB.DBParameters.Add("@im_item_count", IM_Mst_ItemCount, ParameterDirection.Input, SqlDbType.Int)

                                'query the itemId and get the vaccine code for that item select im_vaccine_codefrom IM_MST where im_item_Id = .itemId
                                'Dim SQLGetVaccineCode As String = "select im_vaccine_code from IM_MST where im_item_Id = " & .ItemID

                                'Dim VaccineCode As String = "" 'since in the database table the datatype is taken as string

                                'VaccineCode = oDB.ExecuteQueryScaler(SQLGetVaccineCode)

                                'If IsDBNull(VaccineCode) Then
                                '    VaccineCode = ""
                                'Else
                                '    VaccineCode = VaccineCode
                                'End If


                                'pass the selected CPT code
                                'If IsDBNull(oDataTable.Rows(0)("im_cpt_code")) Then
                                '    CPTCode = ""
                                'Else
                                '    CPTCode = oDataTable.Rows(0)("im_cpt_code") 'here we take Zeroth row becaz the query will return only one row

                                'End If

                                'CPTCode = oDataTable.Rows(0)("im_cpt_code") 'here we take Zeroth row becaz the query will return only one row

                                'Code commented & added by kanchan on 20100914 for immunization MU
                                'oDB.DBParameters.Add("@im_vaccine_code", 0, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@im_vaccine_code", .VaccineCode, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@im_cpt_code", .CPTCode, ParameterDirection.Input, SqlDbType.VarChar)

                                oDB.DBParameters.Add("@im_reasonfor_nonadmin", .ReasonForNonAdmin, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sConceptID", .ConceptID, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sDescriptionID", .DescriptionID, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sSnoMedID", .SnoMedID, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sDescription", "", ParameterDirection.Input, SqlDbType.VarChar)
                                ''Changed By Shweta 20100915 for MU
                                'oDB.DBParameters.Add("@sTranID1", "", ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sTranID1", .DoseUnit, ParameterDirection.Input, SqlDbType.VarChar)
                                ''END-Changed By Shweta 20100915 for MU
                                oDB.DBParameters.Add("@sTranID2", .AdminStatus, ParameterDirection.Input, SqlDbType.VarChar)
                                oDB.DBParameters.Add("@sTranID3", "", ParameterDirection.Input, SqlDbType.VarChar)
                                '**************************************************************************

                            End With
                            oDB.ExecuteNonQuery("IM_InsUPIMTransDtl_SnoMed")
                        Next
                        'Code Start-Added by kanchan on 20101111 for HL7 setting for immunization
                        Dim oGeneralInterface As New clsGeneralInterface()

                        '  If gblnSendChargesToHL7 = True Then
                        If gblnHL7SENDOUTBOUNDGLOEMR = True AndAlso gblnSendImmunization = True Then
                            oGeneralInterface.SendImmunization("V04", Trans_ID, oTransaction.PatientID)
                        End If

                        If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolIMRegistryHL7Format Then
                            oGeneralInterface.SendImmunization("V04IR", Trans_ID, oTransaction.PatientID)
                        End If
                        oGeneralInterface.Dispose()
                        oGeneralInterface = Nothing
                        'End If

                    End If

                    oDB.Disconnect()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Modify, "Immunization Sheet Modify", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 201000915
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Modify, "Immunization Sheet Modify", oTransaction.PatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    'Dim objAudit As New clsAudit
                    'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Immunization Sheet Modify", gstrLoginName, gstrClientMachineName, gnPatientID)
                    'objAudit = Nothing

                    Return True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally
                    If Not IsNothing(oDB) Then    'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try


            End Function

            Public Function Modify_OLD(ByVal ModifyID As Long, ByVal oTransaction As gloStream.Immunization.Supporting.ImmunizationTransaction) As Boolean
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _Result As Boolean = False
                Dim Trans_ID As Long = ModifyID

                Try
                    oDB.Connect(GetConnectionString)
                    Dim _strSQL As String = "delete from IM_Trn_Dtl where im_trn_mst_Id = " & ModifyID
                    oDB.ExecuteNonSQLQuery(_strSQL)


                    If Trans_ID > 0 Then
                        For i As Int16 = 1 To oTransaction.TransactionLines.Count
                            oDB.DBParameters.Clear()
                            Dim _bln1 As Boolean = 0
                            Dim _bln2 As Boolean = 0
                            Dim _bln3 As Boolean = 0
                            Dim _bln4 As Boolean = 0

                            With oTransaction.TransactionLines(i)
                                oDB.DBParameters.Add("@im_trn_mst_Id", Trans_ID, ParameterDirection.Input, SqlDbType.BigInt)
                                If .TransactionDate <> "#12:00:00 AM#" AndAlso IsDate(.TransactionDate) Then
                                    oDB.DBParameters.Add("@im_trn_Date", .TransactionDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Date", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln1 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_nVisitID", .VisitID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_ItemID", .ItemID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_CounterID", .ItemCounterID, ParameterDirection.Input, SqlDbType.Int)
                                oDB.DBParameters.Add("@im_trn_Dose", .Dose, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .DateGiven <> "#12:00:00 AM#" AndAlso IsDate(.DateGiven) Then
                                    oDB.DBParameters.Add("@im_trn_Dategiven", .DateGiven, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Dategiven", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln2 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_Route", .Route, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Lotnumber", .LotNumber, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                If .ExpiryDate <> "#12:00:00 AM#" AndAlso IsDate(.ExpiryDate) Then
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", .ExpiryDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_Expirationdate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln3 = 1
                                End If
                                oDB.DBParameters.Add("@im_trn_Manufacturer", .Manufacturer, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                oDB.DBParameters.Add("@im_trn_Userid", .UserID, ParameterDirection.Input, SqlDbType.BigInt)
                                oDB.DBParameters.Add("@im_trn_Notes", .Notes, ParameterDirection.Input, SqlDbType.VarChar, 255)
                                If .DueDate <> "#12:00:00 AM#" AndAlso IsDate(.DueDate) Then
                                    oDB.DBParameters.Add("@im_trn_duedate", .DueDate, ParameterDirection.Input, SqlDbType.DateTime)
                                Else
                                    oDB.DBParameters.Add("@im_trn_duedate", Date.Now, ParameterDirection.Input, SqlDbType.DateTime)
                                    _bln4 = 1
                                End If

                                '' modification on 20070425 for CCHIT 2007
                                oDB.DBParameters.Add("@im_trn_Site", .Site, ParameterDirection.Input, SqlDbType.VarChar, 50)
                                ''
                                oDB.DBParameters.Add("@bln1", _bln1, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln2", _bln2, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln3", _bln3, ParameterDirection.Input, SqlDbType.Bit)
                                oDB.DBParameters.Add("@bln4", _bln4, ParameterDirection.Input, SqlDbType.Bit)
                            End With
                            oDB.ExecuteNonQuery("IM_InsUPIMTransDtl")
                        Next
                    End If

                    oDB.Disconnect()
                    Return True
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally
                    If (IsNothing(oDB) = False) Then
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                '/// due date problem


                'Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                'Dim cmdItemTrans As SqlClient.SqlCommand
                'Dim oDataReader As SqlClient.SqlDataReader
                'Dim _strSQL As String = ""
                'Dim _Result As Boolean = False
                'Dim oTranLines As New gloStream.Immunization.Supporting.ImmunizationTransactionLines
                'Dim oTranLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine


                'Dim myTrans As SqlClient.SqlTransaction

                ''Dim Trans_ID As Long

                'Try
                '    conn.Open()

                '    myTrans = conn.BeginTransaction

                '    cmdItemTrans = conn.CreateCommand
                '    cmdItemTrans.Transaction = myTrans

                '    _strSQL = "delete from IM_Trn_Dtl where im_trn_mst_Id = " & ModifyID

                '    With cmdItemTrans
                '        .Connection = conn
                '        .CommandType = CommandType.Text
                '        .CommandText = _strSQL
                '    End With

                '    cmdItemTrans.ExecuteNonQuery()

                '    If ModifyID > 0 Then
                '        'insert record in detail table IM_Trn_Dtl

                '        For i As Integer = 1 To oTransaction.TransactionLines.Count

                '            oTranLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
                '            oTranLine = oTransaction.TransactionLines(i)

                '            cmdItemTrans.Parameters.Clear()
                '            Dim bln1 As Boolean
                '            Dim bln2 As Boolean
                '            Dim bln3 As Boolean
                '            Dim bln4 As Boolean
                '            With cmdItemTrans
                '                .Connection = conn
                '                .CommandType = CommandType.StoredProcedure
                '                .CommandText = "IM_InsUPIMTransDtl"
                '            End With

                '            With cmdItemTrans.Parameters
                '                .Add("@im_trn_mst_Id", SqlDbType.BigInt)
                '                .Add("@im_trn_Date", SqlDbType.DateTime)
                '                .Add("@im_trn_nVisitID", SqlDbType.BigInt)
                '                .Add("@im_trn_ItemID", SqlDbType.BigInt)
                '                .Add("@im_trn_CounterID", SqlDbType.BigInt)
                '                .Add("@im_trn_Dose", SqlDbType.VarChar)
                '                .Add("@im_trn_Dategiven", SqlDbType.DateTime)
                '                .Add("@im_trn_Route", SqlDbType.VarChar)
                '                .Add("@im_trn_Lotnumber", SqlDbType.VarChar)
                '                .Add("@im_trn_Expirationdate", SqlDbType.DateTime)
                '                .Add("@im_trn_Manufacturer", SqlDbType.VarChar)
                '                .Add("@im_trn_Userid", SqlDbType.BigInt)
                '                .Add("@im_trn_Notes", SqlDbType.VarChar)
                '                ''code added by Bipin ON 20070205
                '                .Add("@im_trn_duedate", SqlDbType.DateTime)
                '                .Add("@bln1", SqlDbType.Bit)
                '                .Add("@bln2", SqlDbType.Bit)
                '                .Add("@bln3", SqlDbType.Bit)
                '                .Add("@bln4", SqlDbType.Bit)
                '                ''''
                '            End With

                '            With cmdItemTrans
                '                .Parameters("@im_trn_mst_Id").Value = ModifyID
                '                If oTranLine.TransactionDate <> "#12:00:00 AM#" And IsDate(oTranLine.TransactionDate) Then
                '                    .Parameters("@im_trn_Date").Value = oTranLine.TransactionDate
                '                Else
                '                    .Parameters("@im_trn_Date").Value = Now
                '                    bln1 = True
                '                End If

                '                .Parameters("@im_trn_nVisitID").Value = oTranLine.VisitID
                '                .Parameters("@im_trn_ItemID").Value = oTranLine.ItemID
                '                .Parameters("@im_trn_CounterID").Value = oTranLine.ItemCounterID
                '                .Parameters("@im_trn_Dose").Value = oTranLine.Dose
                '                If IsDate(oTranLine.DateGiven) And oTranLine.DateGiven <> "#12:00:00 AM#" Then
                '                    .Parameters("@im_trn_Dategiven").Value = oTranLine.DateGiven
                '                Else
                '                    .Parameters("@im_trn_Dategiven").Value = Now
                '                    bln2 = True
                '                End If

                '                .Parameters("@im_trn_Route").Value = oTranLine.Route
                '                .Parameters("@im_trn_Lotnumber").Value = oTranLine.LotNumber
                '                If IsDate(oTranLine.ExpiryDate) And oTranLine.ExpiryDate <> "#12:00:00 AM#" Then
                '                    .Parameters("@im_trn_Expirationdate").Value = oTranLine.ExpiryDate
                '                Else
                '                    .Parameters("@im_trn_Expirationdate").Value = Now
                '                    bln3 = True
                '                End If

                '                .Parameters("@im_trn_Manufacturer").Value = oTranLine.Manufacturer
                '                .Parameters("@im_trn_Userid").Value = oTranLine.UserID
                '                .Parameters("@im_trn_Notes").Value = oTranLine.Notes

                '                ''code added by Bipin ON 20070205
                '                ' .Parameters("@duedate").Value = oTranLine.DueDate
                '                If IsDate(oTranLine.DueDate) And oTranLine.DueDate <> "#12:00:00 AM#" Then
                '                    .Parameters("@im_trn_duedate").Value = oTranLine.DueDate
                '                Else
                '                    .Parameters("@im_trn_duedate").Value = Now
                '                    bln4 = True
                '                End If
                '                .Parameters("@bln1").Value = bln1
                '                .Parameters("@bln2").Value = bln2
                '                .Parameters("@bln3").Value = bln3
                '                .Parameters("@bln4").Value = bln4
                '                ''''
                '            End With
                '            If cmdItemTrans.ExecuteNonQuery() > 0 Then

                '            End If

                '            oTranLine = Nothing
                '        Next
                '    End If

                '    myTrans.Commit()
                '    _Result = True
                '    Return _Result
                'Catch ex As Exception
                '    Try
                '        myTrans.Rollback()
                '    Catch ex1 As SqlClient.SqlException
                '        If Not myTrans.Connection Is Nothing Then
                '            'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                '            '" was encountered while attempting to roll back the transaction.")
                '            _errorMessage = ex.Message
                '        End If
                '    End Try

                '    _errorMessage = ex.Message
                '    _errorMessage = "Neither record was written to database."
                '    Return False
                'Finally
                '    If conn.State = ConnectionState.Open Then
                '        conn.Close()
                '    End If
                'End Try
                'Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                'Dim cmdItemTrans As SqlClient.SqlCommand
                'Dim oDataReader As SqlClient.SqlDataReader
                'Dim _strSQL As String = ""
                'Dim _Result As Boolean = False
                'Dim oTranLines As New gloStream.Immunization.Supporting.ImmunizationTransactionLines
                'Dim oTranLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine


                'Dim myTrans As SqlClient.SqlTransaction

                ''Dim Trans_ID As Long

                'Try
                '    conn.Open()

                '    myTrans = conn.BeginTransaction

                '    cmdItemTrans = conn.CreateCommand
                '    cmdItemTrans.Transaction = myTrans

                '    _strSQL = "delete from IM_Trn_Dtl where im_trn_mst_Id = " & ModifyID

                '    With cmdItemTrans
                '        .Connection = conn
                '        .CommandType = CommandType.Text
                '        .CommandText = _strSQL
                '    End With

                '    cmdItemTrans.ExecuteNonQuery()

                '    If ModifyID > 0 Then
                '        'insert record in detail table IM_Trn_Dtl

                '        For i As Integer = 1 To oTransaction.TransactionLines.Count

                '            oTranLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
                '            oTranLine = oTransaction.TransactionLines(i)

                '            cmdItemTrans.Parameters.Clear()
                '            Dim bln1 As Boolean
                '            Dim bln2 As Boolean
                '            Dim bln3 As Boolean
                '            Dim bln4 As Boolean
                '            With cmdItemTrans
                '                .Connection = conn
                '                .CommandType = CommandType.StoredProcedure
                '                .CommandText = "IM_InsUPIMTransDtl"
                '            End With

                '            With cmdItemTrans.Parameters
                '                .Add("@im_trn_mst_Id", SqlDbType.BigInt)
                '                .Add("@im_trn_Date", SqlDbType.DateTime)
                '                .Add("@im_trn_nVisitID", SqlDbType.BigInt)
                '                .Add("@im_trn_ItemID", SqlDbType.BigInt)
                '                .Add("@im_trn_CounterID", SqlDbType.BigInt)
                '                .Add("@im_trn_Dose", SqlDbType.VarChar)
                '                .Add("@im_trn_Dategiven", SqlDbType.DateTime)
                '                .Add("@im_trn_Route", SqlDbType.VarChar)
                '                .Add("@im_trn_Lotnumber", SqlDbType.VarChar)
                '                .Add("@im_trn_Expirationdate", SqlDbType.DateTime)
                '                .Add("@im_trn_Manufacturer", SqlDbType.VarChar)
                '                .Add("@im_trn_Userid", SqlDbType.BigInt)
                '                .Add("@im_trn_Notes", SqlDbType.VarChar)
                '                ''code added by Bipin ON 20070205
                '                .Add("@im_trn_duedate", SqlDbType.DateTime)
                '                .Add("@bln1", SqlDbType.Bit)
                '                .Add("@bln2", SqlDbType.Bit)
                '                .Add("@bln3", SqlDbType.Bit)
                '                .Add("@bln4", SqlDbType.Bit)
                '                ''''
                '            End With

                '            With cmdItemTrans
                '                .Parameters("@im_trn_mst_Id").Value = ModifyID
                '                If oTranLine.TransactionDate <> "#12:00:00 AM#" And IsDate(oTranLine.TransactionDate) Then
                '                    .Parameters("@im_trn_Date").Value = oTranLine.TransactionDate
                '                Else
                '                    .Parameters("@im_trn_Date").Value = Now
                '                    bln1 = True
                '                End If

                '                .Parameters("@im_trn_nVisitID").Value = oTranLine.VisitID
                '                .Parameters("@im_trn_ItemID").Value = oTranLine.ItemID
                '                .Parameters("@im_trn_CounterID").Value = oTranLine.ItemCounterID
                '                .Parameters("@im_trn_Dose").Value = oTranLine.Dose
                '                If IsDate(oTranLine.DateGiven) And oTranLine.DateGiven <> "#12:00:00 AM#" Then
                '                    .Parameters("@im_trn_Dategiven").Value = oTranLine.DateGiven
                '                Else
                '                    .Parameters("@im_trn_Dategiven").Value = Now
                '                    bln2 = True
                '                End If

                '                .Parameters("@im_trn_Route").Value = oTranLine.Route
                '                .Parameters("@im_trn_Lotnumber").Value = oTranLine.LotNumber
                '                If IsDate(oTranLine.ExpiryDate) And oTranLine.ExpiryDate <> "#12:00:00 AM#" Then
                '                    .Parameters("@im_trn_Expirationdate").Value = oTranLine.ExpiryDate
                '                Else
                '                    .Parameters("@im_trn_Expirationdate").Value = Now
                '                    bln3 = True
                '                End If

                '                .Parameters("@im_trn_Manufacturer").Value = oTranLine.Manufacturer
                '                .Parameters("@im_trn_Userid").Value = oTranLine.UserID
                '                .Parameters("@im_trn_Notes").Value = oTranLine.Notes

                '                ''code added by Bipin ON 20070205
                '                ' .Parameters("@duedate").Value = oTranLine.DueDate
                '                If IsDate(oTranLine.DueDate) And oTranLine.DueDate <> "#12:00:00 AM#" Then
                '                    .Parameters("@im_trn_duedate").Value = oTranLine.DueDate
                '                Else
                '                    .Parameters("@im_trn_duedate").Value = Now
                '                    bln4 = True
                '                End If
                '                .Parameters("@bln1").Value = bln1
                '                .Parameters("@bln2").Value = bln2
                '                .Parameters("@bln3").Value = bln3
                '                .Parameters("@bln4").Value = bln4
                '                ''''
                '            End With
                '            If cmdItemTrans.ExecuteNonQuery() > 0 Then

                '            End If

                '            oTranLine = Nothing
                '        Next
                '    End If

                '    myTrans.Commit()
                '    _Result = True
                '    Return _Result
                'Catch ex As Exception
                '    Try
                '        myTrans.Rollback()
                '    Catch ex1 As SqlClient.SqlException
                '        If Not myTrans.Connection Is Nothing Then
                '            'Console.WriteLine("An exception of type " & ex1.GetType().ToString() & _
                '            '" was encountered while attempting to roll back the transaction.")
                '            _errorMessage = ex.Message
                '        End If
                '    End Try

                '    _errorMessage = ex.Message
                '    _errorMessage = "Neither record was written to database."
                '    Return False
                'Finally
                '    If conn.State = ConnectionState.Open Then
                '        conn.Close()
                '    End If
                'End Try
                Return Nothing
            End Function


            Public Function Modify(ByVal ModifyID As Long, ByVal Name As String, ByVal HowMany As Integer, ByVal CPTCode As String, ByVal VaccineCode As String, ByVal _ConceptID As String, ByVal _DescriptionID As String, ByVal _SnoMedID As String, ByVal _Icd9 As String) As Boolean
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                Dim cmdModifyItemMst As New SqlClient.SqlCommand
                Dim _strSQL As String = ""
                ' Dim ID As Long
                Dim _result As Boolean

                Try
                    conn.Open()

                    With cmdModifyItemMst
                        .Connection = conn
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "IM_InsUpdItemMast"
                    End With

                    With cmdModifyItemMst
                        .Parameters.Add("@im_item_Id", SqlDbType.BigInt)
                        .Parameters.Add("@MachineID", SqlDbType.BigInt)
                        .Parameters.Add("@im_item_Name", SqlDbType.VarChar)
                        .Parameters.Add("@im_item_Count", SqlDbType.Int)
                        .Parameters.Add("@im_cpt_code", SqlDbType.VarChar)
                        .Parameters.Add("@im_vaccine_code", SqlDbType.VarChar)

                        'Code Start-Added by kanchan on 20100902 as per new workflow
                        .Parameters.Add("@im_sConceptID", SqlDbType.VarChar)
                        .Parameters.Add("@im_sDescriptionID", SqlDbType.VarChar)
                        .Parameters.Add("@im_sSnoMedID", SqlDbType.VarChar)
                        .Parameters.Add("@im_sICD9", SqlDbType.VarChar)
                        'Code End-Added by kanchan on 20100902 as per new workflow

                    End With

                    With cmdModifyItemMst '' .Parameters("@im_item_Id").Value = ID changes done by Bipin on 2007/01/22
                        .Parameters("@im_item_Id").Value = ModifyID
                        .Parameters("@MachineID").Value = 0
                        .Parameters("@im_item_Name").Value = Name
                        .Parameters("@im_item_Count").Value = HowMany
                        .Parameters("@im_cpt_code").Value = CPTCode
                        .Parameters("@im_vaccine_code").Value = VaccineCode
                        'Code Start-Added by kanchan on 20100902 as per new workflow
                        .Parameters("@im_sConceptID").Value = _ConceptID
                        .Parameters("@im_sDescriptionID").Value = _DescriptionID
                        .Parameters("@im_sSnoMedID").Value = _SnoMedID
                        .Parameters("@im_sICD9").Value = _Icd9
                        'Code End-Added by kanchan on 20100902 as per new workflow

                    End With






                    If cmdModifyItemMst.ExecuteNonQuery() > 0 Then
                        _result = True
                    End If

                    If _result Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Immunization Setup Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Immunization Setup Modified", gstrLoginName, gstrClientMachineName)
                    End If
                    Return _result

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return False
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not IsNothing(cmdModifyItemMst) Then    'obj Disposed by mitesh
                        cmdModifyItemMst.Parameters.Clear()
                        cmdModifyItemMst.Dispose()
                        cmdModifyItemMst = Nothing
                    End If
                    If Not IsNothing(conn) Then    'obj Disposed by mitesh
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try
            End Function


            Public Function TransactionDetail(ByVal PatientID As Long) As gloStream.Immunization.Supporting.ImmunizationTransaction
                Dim oItemTrans As New gloStream.Immunization.Supporting.ImmunizationTransaction
                Dim oItemTransLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine = Nothing
                Dim oDataReader As SqlClient.SqlDataReader = Nothing

                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim _strSQL As String = ""
                Dim _TransactionID As Long = 0

                Try
                    'connect to the database
                    ODB.Connect(GetConnectionString)
                    'set the qry string
                    _strSQL = "select im_trn_mst_Id from IM_Trn_Mst where im_trn_mst_nPatientID = " & PatientID & ""
                    'execute the qry and return a datareader
                    Dim myObject = ODB.ExecuteQueryScaler(_strSQL)
                    If Not myObject Is Nothing Then
                        Dim _strTransID As String = myObject & ""
                        If _strTransID = "" Then
                            _TransactionID = 0
                        Else
                            _TransactionID = CType(_strTransID, Long)
                        End If
                    End If
                    If _TransactionID > 0 Then

                        oItemTrans.TransactionID = _TransactionID
                        oItemTrans.PatientID = PatientID
                        'detail
                        ''_strSQL = "select * from   IM_Trn_Dtl where im_trn_mst_Id = " & _TransactionID

                        '' modification on 20070425 for CCHIT 2007
                        '_strSQL = " SELECT *, User_MST.sLoginName AS sLoginName FROM IM_Trn_Dtl LEFT OUTER JOIN User_MST ON IM_Trn_Dtl.im_trn_Userid = User_MST.nUserID WHERE (IM_Trn_Dtl.im_trn_mst_Id = " & _TransactionID & ")"
                        _strSQL = " SELECT im_trn_mst_Id,im_trn_Date,im_trn_nVisitID,im_trn_ItemID,im_trn_CounterID,im_trn_Dose," _
                                & " im_trn_Dategiven,im_trn_Timegiven,im_trn_Route,im_trn_Lotnumber,im_trn_Expirationdate," _
                                & " im_trn_Manufacturer,im_trn_Userid,im_trn_Notes,im_trn_Duedate,im_trn_Site," _
                                & " im_reminder,im_vaccine_eligibilitycode,im_reasonfor_nonadmin,im_cpt_code,im_vaccine_code," _
                                & " im_item_name,im_trn_Reaction,im_trn_ReactionDT,sConceptID,sDescriptionID,sSnoMedID," _
                                & " sTranID1,sTranID2,User_MST.sLoginName AS sLoginName" _
                                & " FROM IM_Trn_Dtl LEFT OUTER JOIN User_MST ON IM_Trn_Dtl.im_trn_Userid = User_MST.nUserID " _
                                & " WHERE (IM_Trn_Dtl.im_trn_mst_Id = " & _TransactionID & ")"
                        oDataReader = ODB.ReadQueryRecords(_strSQL)

                        '//
                        'for loop
                        If Not oDataReader Is Nothing Then
                            If oDataReader.HasRows = True Then
                                While oDataReader.Read

                                    oItemTransLine = New gloStream.Immunization.Supporting.ImmunizationTransactionLine
                                    If Not IsDBNull(oDataReader.Item("im_trn_mst_Id")) Then
                                        'oItemTransLine.Tra = oDataReader.Item("im_trn_mst_Id")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Date")) Then
                                        oItemTransLine.TransactionDate = oDataReader.Item("im_trn_Date")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_nVisitID")) Then
                                        oItemTransLine.VisitID = oDataReader.Item("im_trn_nVisitID")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_ItemID")) Then
                                        oItemTransLine.ItemID = oDataReader.Item("im_trn_ItemID")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_CounterID")) Then
                                        oItemTransLine.ItemCounterID = oDataReader.Item("im_trn_CounterID")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Dose")) Then
                                        oItemTransLine.Dose = oDataReader.Item("im_trn_Dose")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Dategiven")) Then
                                        oItemTransLine.DateGiven = oDataReader.Item("im_trn_Dategiven")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Timegiven")) Then
                                        oItemTransLine.TimeGiven = oDataReader.Item("im_trn_Timegiven")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Route")) Then
                                        oItemTransLine.Route = oDataReader.Item("im_trn_Route")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Lotnumber")) Then
                                        oItemTransLine.LotNumber = oDataReader.Item("im_trn_Lotnumber")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Expirationdate")) Then
                                        oItemTransLine.ExpiryDate = oDataReader.Item("im_trn_Expirationdate")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Manufacturer")) Then
                                        oItemTransLine.Manufacturer = oDataReader.Item("im_trn_Manufacturer")
                                    End If

                                    If Not IsDBNull(oDataReader.Item("im_trn_Userid")) Then
                                        oItemTransLine.UserID = oDataReader.Item("im_trn_Userid")
                                    End If

                                    If Not IsDBNull(oDataReader.Item("im_trn_Notes")) Then
                                        oItemTransLine.Notes = oDataReader.Item("im_trn_Notes")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_Duedate")) Then
                                        oItemTransLine.DueDate = oDataReader.Item("im_trn_Duedate")
                                    End If

                                    ''Code added om 20070425 for CCHIT 2007
                                    If Not IsDBNull(oDataReader.Item("im_trn_Site")) Then
                                        oItemTransLine.Site = oDataReader.Item("im_trn_Site")
                                    End If

                                    If Not IsDBNull(oDataReader.Item("sLoginName")) Then
                                        oItemTransLine.UserName = oDataReader.Item("sLoginName")
                                    End If
                                    ''''

                                    If Not IsDBNull(oDataReader.Item("im_reminder")) Then
                                        oItemTransLine.IsReminder = Convert.ToBoolean(oDataReader.Item("im_reminder"))
                                    End If

                                    If Not IsDBNull(oDataReader.Item("im_vaccine_eligibilitycode")) Then
                                        oItemTransLine.EligibilityCode = oDataReader.Item("im_vaccine_eligibilitycode")
                                    End If

                                    If Not IsDBNull(oDataReader.Item("im_reasonfor_nonadmin")) Then
                                        oItemTransLine.ReasonForNonAdmin = oDataReader.Item("im_reasonfor_nonadmin")
                                    End If

                                    If Not IsDBNull(oDataReader.Item("im_cpt_code")) Then
                                        oItemTransLine.CPTCode = oDataReader.Item("im_cpt_code")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_vaccine_code")) Then
                                        oItemTransLine.VaccineCode = oDataReader.Item("im_vaccine_code")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_item_name")) Then
                                        oItemTransLine.ItemName = oDataReader.Item("im_item_name")
                                    End If

                                    ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007

                                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  

                                    If Not IsDBNull(oDataReader.Item("im_trn_Reaction")) Then
                                        oItemTransLine.Reaction = oDataReader.Item("im_trn_Reaction")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("im_trn_ReactionDT")) Then
                                        If oDataReader.Item("im_trn_ReactionDT") <> "1/1/1900" Then
                                            oItemTransLine.ReactionDT = oDataReader.Item("im_trn_ReactionDT")
                                        End If
                                    End If
                                    ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  
                                    If Not IsDBNull(oDataReader.Item("sConceptID")) Then
                                        oItemTransLine.ConceptID = oDataReader.Item("sConceptID")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("sDescriptionID")) Then
                                        oItemTransLine.DescriptionID = oDataReader.Item("sDescriptionID")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("sSnoMedID")) Then
                                        oItemTransLine.SnoMedID = oDataReader.Item("sSnoMedID")
                                    End If
                                    If Not IsDBNull(oDataReader.Item("sTranID1")) Then
                                        oItemTransLine.DoseUnit = oDataReader.Item("sTranID1")
                                    End If


                                    ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007
                                    'Sanjog For admin status
                                    If Not IsDBNull(oDataReader.Item("sTranID2")) Then
                                        oItemTransLine.AdminStatus = oDataReader.Item("sTranID2")
                                    End If
                                    '
                                    oItemTrans.TransactionLines.Add(oItemTransLine)
                                End While
                            End If
                            oDataReader.Close()

                        End If

                    End If

                    Return oItemTrans

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally

                    If Not IsNothing(ODB) Then    'obj Disposed by mitesh
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                    If Not IsNothing(oDataReader) Then
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                    If Not IsNothing(oItemTransLine) Then
                        oItemTransLine = Nothing
                    End If
                    If Not IsNothing(oItemTrans) Then
                        oItemTrans = Nothing
                    End If

                End Try
            End Function


            Public Function IsImmunizationExists(ByVal PatientID As Long) As Long
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                Dim _Counter As Long
                Dim _Result As String = ""

                oDB.Connect(GetConnectionString)
                _Result = oDB.ExecuteQueryScaler("SELECT im_trn_mst_Id FROM IM_Trn_Mst WHERE im_trn_mst_nPatientID = " & PatientID & " ")
                If Val(_Result) > 0 Then
                    _Counter = CLng(_Result)
                Else
                    _Counter = 0
                End If

                oDB.Disconnect()
                If Not IsNothing(oDB) Then    'obj Disposed by mitesh
                    oDB.Dispose()
                    oDB = Nothing
                End If
                Return _Counter
            End Function


            Public Function Fill_LockImmunization(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable

                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Con)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Dim objParam As System.Data.SqlClient.SqlParameter

                    objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = MachinName

                    objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = TransactionType

                    objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
                    objParam.Direction = ParameterDirection.Input
                    objParam.Value = 0

                    sqladpt.SelectCommand = Cmd
                    Dim dt As New DataTable
                    sqladpt.Fill(dt)

                    Con.Close()
                    objParam = Nothing
                    Return dt
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then     'obj Disposed by mitesh
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If
                    'If Not IsNothing(dt) Then
                    '    dt.Dispose()
                    '    dt = Nothing
                    'End If

                End Try
            End Function

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class ItemSetup
            Private _errorMessage As String

            Public Property ErrorMessage() As String
                Get
                    Return _errorMessage
                End Get
                Set(ByVal Value As String)
                    _errorMessage = Value
                End Set
            End Property
            'Parameter CPTCode added by Dipak 20090910 to pass CPTCode
            'Public Function Add(ByVal HowMany As Integer, ByVal CPTCode As String, ByVal VaccineCode As String, ByVal _ConceptID As String, ByVal _DescriptionID As String, ByVal _SnoMedID As String, ByVal _SnomedDescription As String, ByVal _RxnormID As String, ByVal _NDCCode As String, ByVal _IM_strSnomedDescription As String, ByVal _IM_SKU As String, ByVal _ReceivedDate As DateTime, ByVal _IM_Active As String, ByVal _IM_CVXCode As String, ByVal _IM_CVXDescription As String, ByVal _MVXCode As String, ByVal _MVXDescription As String, ByVal _IM_TradeName As String, ByVal _IM_LotNo As String, ByVal _IM_ExpiryDate As DateTime, ByVal _Vialcount As Integer, ByVal _IM_DosesPerVial As Integer, ByVal _IM_AvailableDoses As Integer, ByVal _IM_VIS As String, ByVal _IM_publicationdate As DateTime, ByVal DiagnosisCode As String, ByVal _IM_NDCCode As String, ByVal _IM_FundingSource As String, ByVal _IM_Commnets As String) As Boolean

            '    Dim conn As New SqlClient.SqlConnection(GetConnectionString)
            '    Dim cmdItemMst As New SqlClient.SqlCommand
            '    Dim _strSQL As String = ""
            '    Dim _result As Boolean = False

            '    Dim MachineID As Long
            '    Dim IMTrn_ID As Long

            '    Try
            '        'Table used -- IM_MST
            '        'add the Item master record in IM_MST

            '        conn.Open()

            '        With cmdItemMst
            '            .Connection = conn
            '            .CommandType = CommandType.StoredProcedure
            '            .CommandText = "IM_InsUpdItemMast"
            '        End With

            '        MachineID = GetPrefixTransactionID()

            '        With cmdItemMst
            '            .Parameters.Add("@im_item_Id", SqlDbType.BigInt)
            '            .Parameters.Add("@MachineID", SqlDbType.BigInt)
            '            .Parameters.Add("@im_item_Name", SqlDbType.VarChar)
            '            .Parameters.Add("@im_item_Count", SqlDbType.Int)

            '            .Parameters.Add("@im_cpt_code", SqlDbType.VarChar)
            '            .Parameters.Add("@im_vaccine_code", SqlDbType.VarChar)


            '            .Parameters.Add("@im_sConceptID", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sDescriptionID", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sSnoMedID", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sICD9", SqlDbType.VarChar)

            '            .Parameters.Add("@im_sSnomedDescription", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sTranID1", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sTranID2", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sSnomedDefination", SqlDbType.VarChar)


            '            .Parameters.Add("@im_sSKU", SqlDbType.NVarChar)
            '            .Parameters.Add("@im_dtReceivedDate", SqlDbType.DateTime)
            '            .Parameters.Add("@im_sActive", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sCVXCode", SqlDbType.NVarChar)
            '            .Parameters.Add("@im_sShortDescription", SqlDbType.NVarChar)
            '            .Parameters.Add("@im_sMVXCode", SqlDbType.NVarChar)
            '            .Parameters.Add("@im_sManufacturer", SqlDbType.NVarChar)
            '            .Parameters.Add("@im_sTradeName", SqlDbType.NVarChar)
            '            .Parameters.Add("@im_LotNumber", SqlDbType.VarChar)
            '            .Parameters.Add("@im_dtExpiration", SqlDbType.DateTime)
            '            .Parameters.Add("@im_item_VialCount", SqlDbType.Int)
            '            .Parameters.Add("@im_DosesperVial", SqlDbType.Int)
            '            .Parameters.Add("@im_AvailableDoses", SqlDbType.Int)
            '            .Parameters.Add("@im_sVIS", SqlDbType.VarChar)
            '            .Parameters.Add("@im_dtPublication", SqlDbType.DateTime)
            '            .Parameters.Add("@im_Diagnosis_Code", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sNDCCode", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sFundingSource", SqlDbType.VarChar)
            '            .Parameters.Add("@im_sComments", SqlDbType.VarChar)


            '        End With

            '        With cmdItemMst
            '            .Parameters("@im_item_Id").Value = IMTrn_ID
            '            .Parameters("@MachineID").Value = MachineID
            '            .Parameters("@im_item_Name").Value = ""
            '            .Parameters("@im_item_Count").Value = HowMany

            '            .Parameters("@im_cpt_code").Value = CPTCode
            '            .Parameters("@im_vaccine_code").Value = VaccineCode


            '            .Parameters("@im_sConceptID").Value = _ConceptID
            '            .Parameters("@im_sDescriptionID").Value = _DescriptionID
            '            .Parameters("@im_sSnoMedID").Value = _SnoMedID

            '            .Parameters("@im_sSnomedDescription").Value = _IM_strSnomedDescription
            '            .Parameters("@im_sTranID1").Value = _RxnormID
            '            .Parameters("@im_sTranID2").Value = _NDCCode
            '            .Parameters("@im_sSnomedDefination").Value = _SnomedDescription

            '            .Parameters("@im_sSKU").Value = _IM_SKU
            '            .Parameters("@im_dtReceivedDate").Value = _ReceivedDate
            '            .Parameters("@im_sActive").Value = _IM_Active
            '            .Parameters("@im_sCVXCode").Value = _IM_CVXCode
            '            .Parameters("@im_sShortDescription").Value = _IM_CVXDescription
            '            .Parameters("@im_sMVXCode").Value = _MVXCode
            '            .Parameters("@im_sManufacturer").Value = _MVXDescription
            '            .Parameters("@im_sTradeName").Value = _IM_TradeName
            '            .Parameters("@im_LotNumber").Value = _IM_LotNo
            '            .Parameters("@im_dtExpiration").Value = _IM_ExpiryDate
            '            .Parameters("@im_item_VialCount").Value = _Vialcount
            '            .Parameters("@im_DosesperVial").Value = _IM_DosesPerVial
            '            .Parameters("@im_AvailableDoses").Value = _IM_AvailableDoses
            '            .Parameters("@im_sVIS").Value = _IM_VIS
            '            .Parameters("@im_dtPublication").Value = _IM_publicationdate
            '            .Parameters("@im_Diagnosis_Code").Value = DiagnosisCode
            '            .Parameters("@im_sNDCCode").Value = _IM_NDCCode
            '            .Parameters("@im_sFundingSource").Value = _IM_FundingSource
            '            .Parameters("@im_sComments").Value = _IM_Commnets



            '        End With

            '        If cmdItemMst.ExecuteNonQuery() > 0 Then
            '            _result = True
            '        End If

            '        If _result = True Then
            '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Immunization Setup Added", gloAuditTrail.ActivityOutCome.Success)
            '            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Immunization Setup Added", gstrLoginName, gstrClientMachineName)
            '        End If
            '        Return _result

            '    Catch ex As Exception
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        _errorMessage = ex.Message
            '        Return False
            '    Finally
            '        If conn.State = ConnectionState.Open Then
            '            conn.Close()
            '        End If
            '        If Not IsNothing(cmdItemMst) Then    'obj Disposed by mitesh
            '            cmdItemMst.Dispose()
            '            cmdItemMst = Nothing
            '        End If
            '        If Not IsNothing(conn) Then
            '            conn.Dispose()
            '            conn = Nothing
            '        End If
            '    End Try

            'End Function

            Public Function AddData(ByVal HowMany As Integer, ByVal CPTCode As String, ByVal VaccineCode As String, ByVal _ConceptID As String, ByVal _DescriptionID As String, ByVal _SnoMedID As String, ByVal _SnomedDescription As String, ByVal _RxnormID As String, ByVal _NDCCode As String, ByVal _IM_strSnomedDescription As String, ByVal _IM_SKU As String, ByVal _ReceivedDate As String, ByVal _IM_Active As String, ByVal _IM_CVXDescription As String, ByVal _MVXDescription As String, ByVal _IM_TradeName As String, ByVal _IM_LotNo As String, ByVal _IM_ExpiryDate As String, ByVal _Vialcount As Decimal, ByVal _IM_DosesPerVial As Decimal, ByVal _IM_AvailableDoses As Decimal, ByVal _IM_VIS As String, ByVal _IM_publicationdate As String, ByVal DiagnosisCode As String, ByVal _IM_NDCCode As String, ByVal _IM_FundingSource As String, ByVal _IM_Commnets As String, ByVal _EditID As Long, ByVal _nDocumentID As Long, ByVal _Location As String, _bTrackInventory As Boolean, CategoryID As Long, ByVal dtVIS As DataTable, Optional ByVal nICDRevision As Integer = 9) As Long

                Dim oDB As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
                Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter = Nothing


                Dim MachineID As Long
                'Dim IMTrn_ID As Long

                Try

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@MachineID"
                    oParamater.Value = MachineID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_item_Name"
                    oParamater.Value = ""
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.Int
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_item_Count"
                    oParamater.Value = HowMany
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_cpt_code"
                    oParamater.Value = CPTCode
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_vaccine_code"
                    oParamater.Value = VaccineCode
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sConceptID"
                    oParamater.Value = _ConceptID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sDescriptionID"
                    oParamater.Value = _DescriptionID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sSnoMedID"
                    oParamater.Value = _SnoMedID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sICD9"
                    oParamater.Value = ""
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sSnomedDescription"
                    oParamater.Value = _SnomedDescription
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sTranID1"
                    oParamater.Value = _RxnormID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sTranID2"
                    oParamater.Value = _NDCCode
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sSnomedDefination"
                    oParamater.Value = ""
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.NVarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sSKU"
                    If _IM_SKU = "" Then
                        oParamater.Value = System.DBNull.Value
                    Else
                        oParamater.Value = _IM_SKU
                    End If
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.DateTime
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_dtReceivedDate"
                    oParamater.Value = _ReceivedDate
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sActive"
                    oParamater.Value = _IM_Active
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.NVarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sVaccine"
                    oParamater.Value = _IM_CVXDescription
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.NVarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sManufacturer"
                    oParamater.Value = _MVXDescription
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.NVarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sTradeName"
                    oParamater.Value = _IM_TradeName
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_LotNumber"
                    oParamater.Value = _IM_LotNo
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.DateTime
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_dtExpiration"
                    If _IM_ExpiryDate = "" Then
                        oParamater.Value = System.DBNull.Value
                    Else
                        oParamater.Value = _IM_ExpiryDate
                    End If

                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.Decimal
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_item_VialCount"
                    oParamater.Value = _Vialcount
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing


                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.Decimal
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_DosesperVial"
                    oParamater.Value = _IM_DosesPerVial
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.Decimal
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_AvailableDoses"
                    oParamater.Value = _IM_AvailableDoses
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing


                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sVIS"
                    oParamater.Value = _IM_VIS
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.DateTime
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_dtPublication"
                    If _IM_publicationdate = "" Then
                        oParamater.Value = System.DBNull.Value
                    Else
                        oParamater.Value = _IM_publicationdate
                    End If
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_Diagnosis_Code"
                    oParamater.Value = DiagnosisCode
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS
                    oParamater.DataType = SqlDbType.NVarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sNDCCode"
                    If _IM_NDCCode = "" Then
                        oParamater.Value = System.DBNull.Value
                    Else
                        oParamater.Value = _IM_NDCCode
                    End If
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sFundingSource"
                    oParamater.Value = _IM_FundingSource
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sComments"
                    oParamater.Value = _IM_Commnets
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sEntryUser"
                    oParamater.Value = gstrLoginName
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_nDocumentID"
                    oParamater.Value = _nDocumentID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_sLocation"
                    oParamater.Value = _Location
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    'Added by Amit - Vaccine Inventory Track
                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.VarChar
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_bTrackInventory"
                    oParamater.Value = _bTrackInventory
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing



                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_nCategoryID"
                    oParamater.Value = CategoryID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    ''nICDRevision parameter added for ICD10
                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.SmallInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@nICDRevision"
                    oParamater.Value = nICDRevision
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing


                    'Aniket: This always has to be the last parameter.
                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.InputOutput
                    oParamater.Name = "@im_item_Id"
                    oParamater.Value = _EditID
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    Dim id As Long
                    id = oDB.Add("IM_InsUpdItemMast")

                    If Not IsNothing(oDB) Then    'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If

                    oDB = New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.BigInt
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@im_item_Id"
                    oParamater.Value = id
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
                    oParamater.DataType = SqlDbType.Structured
                    oParamater.Direction = ParameterDirection.Input
                    oParamater.Name = "@TVP_VIS_Mapping"
                    oParamater.Value = dtVIS
                    oDB.DBParametersCol.Add(oParamater)
                    oParamater = Nothing

                    Dim Output As Long
                    Output = oDB.Add("IN_UP_DEL_VIS_Mapping")

                    'SLR: Check whether ID is returning properly. Since as per ADD, it is the last value...,
                    Return id
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                    ' Return False
                Finally

                    If Not IsNothing(oDB) Then    'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                    If Not IsNothing(oParamater) Then
                        oParamater = Nothing
                    End If
                End Try


            End Function
            'Parameter CPTCode added by Dipak 20090910 to pass CPTCode
            'Public Function Modify(ByVal HowMany As Integer, ByVal CPTCode As String, ByVal VaccineCode As String, ByVal _ConceptID As String, ByVal _DescriptionID As String, ByVal _SnoMedID As String, ByVal _SnomedDescription As String, ByVal _RxnormID As String, ByVal _NDCCode As String, ByVal _IM_strSnomedDescription As String, ByVal _IM_SKU As String, ByVal _ReceivedDate As DateTime, ByVal _IM_Active As String, ByVal _IM_CVXCode As String, ByVal _IM_CVXDescription As String, ByVal _MVXCode As String, ByVal _MVXDescription As String, ByVal _IM_TradeName As String, ByVal _IM_LotNo As String, ByVal _IM_ExpiryDate As DateTime, ByVal _Vialcount As Integer, ByVal _IM_DosesPerVial As Integer, ByVal _IM_AvailableDoses As Integer, ByVal _IM_VIS As String, ByVal _IM_publicationdate As DateTime, ByVal DiagnosisCode As String, ByVal _IM_NDCCode As String, ByVal _IM_FundingSource As String, ByVal _IM_Commnets As String, ByVal oldSKU As String) As Long
            '    Dim conn As New SqlClient.SqlConnection(GetConnectionString)
            '    Dim cmdModifyItemMst As New SqlClient.SqlCommand
            '    Dim _strSQL As String = String.Empty
            '    Dim ID As Long
            '    'Dim _result As Boolean
            '    Dim ReturnID As Long
            '    Try
            '        If conn.State = ConnectionState.Closed Then
            '            conn.Open()
            '        End If

            '        With cmdModifyItemMst
            '            .Connection = conn
            '            .CommandType = CommandType.Text

            '            '20090819 For saving Name along with single quote in name
            '            .CommandText = "SELECT im_item_Id FROM IM_MST where im_sSKU = '" & oldSKU.Replace("'", "''") & "'"
            '        End With

            '        ID = cmdModifyItemMst.ExecuteScalar

            '        ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007

            '        ReturnID = Modify(ID, HowMany, CPTCode, VaccineCode, _ConceptID, _DescriptionID, _SnoMedID, _SnomedDescription, _RxnormID, _NDCCode, _IM_strSnomedDescription, _IM_SKU, _ReceivedDate, _IM_Active, _IM_CVXCode, _IM_CVXDescription, _MVXCode, _MVXDescription, _IM_TradeName, _IM_LotNo, _IM_ExpiryDate, _Vialcount, _IM_DosesPerVial, _IM_AvailableDoses, _IM_VIS, _IM_publicationdate, DiagnosisCode, _IM_NDCCode, _IM_FundingSource, _IM_Commnets)
            '        ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007

            '        conn.Close()
            '        Return ReturnID
            '    Catch ex As Exception
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '    Finally

            '        If conn.State = ConnectionState.Open Then
            '            conn.Close()
            '        End If
            '        If Not IsNothing(cmdModifyItemMst) Then    'obj Disposed by mitesh
            '            cmdModifyItemMst.Dispose()
            '            cmdModifyItemMst = Nothing
            '        End If
            '        If Not IsNothing(conn) Then
            '            conn.Dispose()
            '            conn = Nothing
            '        End If

            '    End Try
            'End Function


            'Public Function Modify(ByVal ModifyID As Long, ByVal HowMany As Integer, ByVal CPTCode As String, ByVal VaccineCode As String, ByVal _ConceptID As String, ByVal _DescriptionID As String, ByVal _SnoMedID As String, ByVal _SnomedDescription As String, ByVal _RxnormID As String, ByVal _NDCCode As String, ByVal _IM_strSnomedDescription As String, ByVal _IM_SKU As String, ByVal _ReceivedDate As DateTime, ByVal _IM_Active As String, ByVal _IM_CVXCode As String, ByVal _IM_CVXDescription As String, ByVal _MVXCode As String, ByVal _MVXDescription As String, ByVal _IM_TradeName As String, ByVal _IM_LotNo As String, ByVal _IM_ExpiryDate As DateTime, ByVal _Vialcount As Integer, ByVal _IM_DosesPerVial As Integer, ByVal _IM_AvailableDoses As Integer, ByVal _IM_VIS As String, ByVal _IM_publicationdate As DateTime, ByVal DiagnosisCode As String, ByVal _IM_NDCCode As String, ByVal _IM_FundingSource As String, ByVal _IM_Commnets As String) As Long

            '    Dim ReturnID As Long
            '    Dim oDB As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
            '    Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter

            '    Try




            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.BigInt
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@MachineID"
            '        oParamater.Value = 0
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_item_Name"
            '        oParamater.Value = ""
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.Int
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_item_Count"
            '        oParamater.Value = HowMany
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_cpt_code"
            '        oParamater.Value = CPTCode
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_vaccine_code"
            '        oParamater.Value = VaccineCode
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sConceptID"
            '        oParamater.Value = _ConceptID
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sDescriptionID"
            '        oParamater.Value = _DescriptionID
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sSnoMedID"
            '        oParamater.Value = _SnoMedID
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sICD9"
            '        oParamater.Value = ""
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing


            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sSnomedDescription"
            '        oParamater.Value = _SnomedDescription
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sTranID1"
            '        oParamater.Value = _RxnormID
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sTranID2"
            '        oParamater.Value = _NDCCode
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sSnomedDefination"
            '        oParamater.Value = ""
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.NVarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sSKU"
            '        oParamater.Value = _IM_SKU
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.DateTime
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_dtReceivedDate"
            '        oParamater.Value = _ReceivedDate
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sActive"
            '        oParamater.Value = _IM_Active
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.NVarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sCVXCode"
            '        oParamater.Value = _IM_CVXCode
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.NVarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sShortDescription"
            '        oParamater.Value = _IM_CVXDescription
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing


            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.NVarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sMVXCode"
            '        oParamater.Value = _MVXCode
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.NVarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sManufacturer"
            '        oParamater.Value = _MVXDescription
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.NVarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sTradeName"
            '        oParamater.Value = _IM_TradeName
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_LotNumber"
            '        oParamater.Value = _IM_LotNo
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.DateTime
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_dtExpiration"
            '        oParamater.Value = _IM_ExpiryDate
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.Int
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_item_VialCount"
            '        oParamater.Value = _Vialcount
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing


            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.Int
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_DosesperVial"
            '        oParamater.Value = _IM_DosesPerVial
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.Int
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_AvailableDoses"
            '        oParamater.Value = _IM_AvailableDoses
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing


            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sVIS"
            '        oParamater.Value = _IM_VIS
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.DateTime
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_dtPublication"
            '        oParamater.Value = _IM_publicationdate
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_Diagnosis_Code"
            '        oParamater.Value = DiagnosisCode
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sNDCCode"
            '        oParamater.Value = _IM_NDCCode
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing


            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sFundingSource"
            '        oParamater.Value = _IM_FundingSource
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sComments"
            '        oParamater.Value = _IM_Commnets
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.VarChar
            '        oParamater.Direction = ParameterDirection.Input
            '        oParamater.Name = "@im_sEntryUser"
            '        oParamater.Value = gstrLoginName
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            '        oParamater.DataType = SqlDbType.BigInt
            '        oParamater.Direction = ParameterDirection.InputOutput
            '        oParamater.Name = "@im_item_Id"
            '        oParamater.Value = ModifyID
            '        oDB.DBParametersCol.Add(oParamater)
            '        oParamater = Nothing

            '        ReturnID = oDB.Add("IM_InsUpdItemMast")


            '        Return ReturnID

            '        If ReturnID > 0 Then
            '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Immunization Setup Modified", gloAuditTrail.ActivityOutCome.Success)
            '            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Immunization Setup Modified", gstrLoginName, gstrClientMachineName)
            '        End If
            '        Return ReturnID

            '    Catch ex As Exception
            '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '        _errorMessage = ex.Message
            '        oDB.Dispose()
            '        oDB = Nothing
            '        Return ReturnID
            '    Finally
            '        oDB.Dispose()
            '        oDB = Nothing

            '    End Try
            'End Function

            'Added New funcation by manoj jadhav on 20130928 Mu2 Transfer to immunization Registry-Sharing uncertain Formulation CVX Details
            Public Function GetUncertainVaccincationFromCVXCode(ByVal Vaccine As String, ByVal nTranscationId As Long, ByVal isHistory As Boolean) As DataTable
                Dim oDBLayer As gloDatabaseLayer.DBLayer = Nothing
                Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
                Dim dtResult As DataTable = Nothing
                Try
                    oDBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())
                    oDBParameters = New gloDatabaseLayer.DBParameters()
                    oDBParameters.Add("@im_trn_mst_Id", nTranscationId, ParameterDirection.Input, SqlDbType.BigInt)
                    oDBParameters.Add("@Vaccine", Vaccine, ParameterDirection.Input, SqlDbType.VarChar, 500)
                    oDBParameters.Add("@IsHistory", isHistory, ParameterDirection.Input, SqlDbType.Bit)
                    oDBLayer.Connect(False)
                    oDBLayer.Retrive("IM_GetUncertainFormulationCVX", oDBParameters, dtResult)
                    oDBLayer.Disconnect()
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Finally
                    If Not oDBParameters Is Nothing Then
                        oDBParameters.Dispose()
                        oDBParameters = Nothing
                    End If
                    If Not IsNothing(oDBLayer) Then
                        oDBLayer.Dispose()
                        oDBLayer = Nothing
                    End If
                End Try
                Return dtResult
            End Function


            Public Function Delete(ByVal Name As String) As Boolean
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                Dim cmdDelItemMst As New SqlClient.SqlCommand
                Dim _strSQL As String = ""
                Dim ID As Long
                'Dim _result As Boolean

                Try
                    conn.Open()

                    With cmdDelItemMst
                        .Connection = conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT im_item_Id FROM IM_MST where im_item_Name = '" & Name & "'"
                    End With

                    ID = cmdDelItemMst.ExecuteScalar
                    Delete(ID, Name)

                    conn.Close()
                    Return Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return False
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not IsNothing(cmdDelItemMst) Then    'obj Disposed by mitesh
                        cmdDelItemMst.Parameters.Clear()
                        cmdDelItemMst.Dispose()
                        cmdDelItemMst = Nothing
                    End If
                    If Not IsNothing(conn) Then
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try
            End Function
            Public Function DeactivateStatus(ByVal ID As Long) As Boolean
                Dim conn As SqlConnection = Nothing
                Dim cmd As SqlCommand = Nothing
                Dim _result As Boolean
                Try
                    conn = New SqlConnection(GetConnectionString())
                    conn.Open()
                    cmd = New SqlCommand("IM_DeactivateStatus", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt)
                    cmd.Parameters("@ID").Value = ID
                    'cmd.ExecuteNonQuery()
                    If cmd.ExecuteNonQuery() > 0 Then
                        _result = True
                    End If

                    If cmd IsNot Nothing Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If

                    Return _result
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                Finally
                    conn.Close()
                    If IsNothing(conn) = False Then
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try
            End Function

            Public Function CheckVaccineisValidaginstTradeName(ByVal TradeName As String, ByVal Vaccine As String) As DataSet
                Try
                    Dim objCon As New SqlConnection
                    Dim da As SqlDataAdapter = Nothing

                    Dim cmd As SqlCommand = Nothing
                    Try


                        objCon.ConnectionString = GetConnectionString()
                        cmd = New SqlCommand("IM_CheckValidTradeNameandCVXEnterd", objCon)
                        cmd.CommandType = CommandType.StoredProcedure
                        Dim IMParam As SqlParameter



                        IMParam = cmd.Parameters.Add("@TradeName", SqlDbType.NVarChar)
                        IMParam.Direction = ParameterDirection.Input
                        IMParam.Value = TradeName

                        IMParam = cmd.Parameters.Add("@Vaccine", SqlDbType.NVarChar)
                        IMParam.Direction = ParameterDirection.Input
                        IMParam.Value = Vaccine

                        objCon.Open()
                        da = New SqlDataAdapter
                        da.SelectCommand = cmd
                        Dim ds As New DataSet
                        da.Fill(ds)
                        objCon.Close()
                        ds.Tables(0).TableName = "Exists"
                        ds.Tables(1).TableName = "CVX"

                        IMParam = Nothing

                        Return ds

                    Catch ex As Exception

                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If Not IsNothing(da) Then    'obj Disposed by mitesh
                            da.Dispose()
                            da = Nothing
                        End If
                        If Not IsNothing(cmd) Then
                            cmd.Parameters.Clear()
                            cmd.Dispose()
                            cmd = Nothing
                        End If
                        If Not IsNothing(objCon) Then
                            'objCon.Close()
                            objCon.Dispose()
                            objCon = Nothing
                        End If
                    End Try
                Catch ex As Exception
                    Return Nothing
                Finally
                End Try
            End Function
            Public Function GetCVXMvxFromTradeName(ByVal TradeName As String) As DataSet
                Try
                    Dim objCon As New SqlConnection
                    Dim da As SqlDataAdapter = Nothing

                    Dim cmd As SqlCommand = Nothing
                    Try


                        objCon.ConnectionString = GetConnectionString()
                        cmd = New SqlCommand("IM_GetCVXMVXFromTradeName", objCon)
                        cmd.CommandType = CommandType.StoredProcedure
                        Dim IMParam As SqlParameter



                        IMParam = cmd.Parameters.Add("@TradeName", SqlDbType.NVarChar)
                        IMParam.Direction = ParameterDirection.Input
                        IMParam.Value = TradeName


                        objCon.Open()
                        da = New SqlDataAdapter
                        da.SelectCommand = cmd
                        Dim ds As New DataSet
                        da.Fill(ds)



                        objCon.Close()
                        ds.Tables(0).TableName = "Vaccine"
                        ds.Tables(1).TableName = "Manufacturer"
                        IMParam = Nothing
                        Return ds

                    Catch ex As Exception

                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If Not IsNothing(da) Then    'obj Disposed by mitesh
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
                Catch ex As Exception
                    Return Nothing
                Finally
                End Try
            End Function
            Public Function GetCPTFromCVXCode(ByVal Vaccine As String) As DataTable
                Try
                    Dim objCon As New SqlConnection
                    Dim da As SqlDataAdapter = Nothing

                    Dim cmd As SqlCommand = Nothing
                    Try


                        objCon.ConnectionString = GetConnectionString()
                        cmd = New SqlCommand("IM_GetCPTCodeFromCVX", objCon)
                        cmd.CommandType = CommandType.StoredProcedure
                        Dim IMParam As SqlParameter



                        IMParam = cmd.Parameters.Add("@Vaccine", SqlDbType.VarChar)
                        IMParam.Direction = ParameterDirection.Input
                        IMParam.Value = Vaccine


                        objCon.Open()
                        da = New SqlDataAdapter
                        da.SelectCommand = cmd
                        Dim dt As New DataTable
                        da.Fill(dt)
                        objCon.Close()
                        IMParam = Nothing
                        Return dt

                    Catch ex As Exception

                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return Nothing
                    Finally
                        If Not IsNothing(da) Then    'obj Disposed by mitesh
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
                Catch ex As Exception
                    Return Nothing
                Finally
                End Try
            End Function

            Public Function Delete(ByVal ID As Long, ByVal Name As String) As Boolean
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                Dim cmdDeleteItemMst As New SqlClient.SqlCommand

                Dim _strSQL As String = ""
                Dim _result As Boolean

                Try
                    conn.Open()

                    With cmdDeleteItemMst
                        .Connection = conn
                        .CommandType = CommandType.StoredProcedure

                        .Parameters.Add("@im_item_Id", SqlDbType.BigInt)
                        .Parameters("@im_item_Id").Value = ID
                        .CommandText = "IM_DeleteItemMastRec"
                    End With

                    If cmdDeleteItemMst.ExecuteNonQuery() > 0 Then
                        _result = True
                    End If

                    If _result = True Then
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Immunization Setup Deleted", gloAuditTrail.ActivityOutCome.Success)
                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Immunization Setup Deleted", gstrLoginName, gstrClientMachineName)
                    End If

                    Return _result

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return False
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not IsNothing(cmdDeleteItemMst) Then    'obj Disposed by mitesh
                        cmdDeleteItemMst.Parameters.Clear()
                        cmdDeleteItemMst.Dispose()
                        cmdDeleteItemMst = Nothing
                    End If
                    If Not IsNothing(conn) Then
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try
            End Function

            Public Function ItemDetail(ByVal Name As String) As gloStream.Immunization.Supporting.ImmunizationItem
                Dim oMstItem As gloStream.Immunization.Supporting.ImmunizationItem = Nothing
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                Dim cmdGetItemMst As New SqlClient.SqlCommand
                Dim _strSQL As String = ""
                Dim ID As Long


                Try
                    conn.Open()

                    With cmdGetItemMst
                        .Connection = conn
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT im_item_Id FROM IM_MST where im_item_Name = '" & Name & "'"
                    End With

                    ID = cmdGetItemMst.ExecuteScalar
                    oMstItem = ItemDetail(ID)

                    Return oMstItem
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Return Nothing
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not IsNothing(cmdGetItemMst) Then    'obj Disposed by mitesh
                        cmdGetItemMst.Parameters.Clear()
                        cmdGetItemMst.Dispose()
                        cmdGetItemMst = Nothing
                    End If
                    If Not IsNothing(conn) Then
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try

            End Function



            Public Function getCPTCodes_old(ByVal itemId As Long, ByVal vaccCode As Int16) As String
                'Dim oMstItem As New gloStream.Immunization.Supporting.ImmunizationItem
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                Dim cmdGetCPTCodes As New SqlClient.SqlCommand
                Dim _strSQL As String = ""
                Dim CPTCodes As String = ""


                Try
                    conn.Open()

                    With cmdGetCPTCodes
                        .Connection = conn
                        .CommandType = CommandType.Text
                        .CommandText = "select im_cpt_code from IM_MST where im_item_Id = " & itemId & " and im_vaccine_code = " & vaccCode
                    End With

                    CPTCodes = cmdGetCPTCodes.ExecuteScalar
                    If IsDBNull(CPTCodes) OrElse CPTCodes = "" Then
                        Return ""
                    Else
                        Return CPTCodes
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Return Nothing
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not IsNothing(cmdGetCPTCodes) Then    'obj Disposed by mitesh
                        cmdGetCPTCodes.Parameters.Clear()
                        cmdGetCPTCodes.Dispose()
                        cmdGetCPTCodes = Nothing
                    End If
                    If Not IsNothing(conn) Then
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try

            End Function
            'Aniket Fixing Issues in GLO2010-0006676
            ''Sandip Darade  20100809
            '' Created function to avoid losing cptcodes from im transactions 
            Public Function getCPTCodes(ByVal vaccCode As String, ByVal itemname As String, ByVal patientid As Int64) As String
                'Dim oMstItem As New gloStream.Immunization.Supporting.ImmunizationItem
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                Dim cmdGetCPTCodes As New SqlClient.SqlCommand
                Dim _strSQL As String = ""
                Dim CPTCodes As String = ""


                Try
                    conn.Open()

                    With cmdGetCPTCodes
                        .Connection = conn
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "IM_GetCPTCodes"
                        .Parameters.Add("@Patientid", SqlDbType.BigInt)
                        .Parameters("@Patientid").Value = patientid
                        .Parameters.Add("@ItemName", SqlDbType.VarChar)
                        .Parameters("@ItemName").Value = itemname
                        .Parameters.Add("@Vaccinecode", SqlDbType.VarChar)
                        .Parameters("@Vaccinecode").Value = vaccCode
                    End With

                    CPTCodes = cmdGetCPTCodes.ExecuteScalar
                    If IsDBNull(CPTCodes) OrElse CPTCodes = "" Then
                        Return ""
                    Else
                        Return CPTCodes
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Return Nothing
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not IsNothing(cmdGetCPTCodes) Then    'obj Disposed by mitesh
                        cmdGetCPTCodes.Parameters.Clear()
                        cmdGetCPTCodes.Dispose()
                        cmdGetCPTCodes = Nothing
                    End If
                    If Not IsNothing(conn) Then
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try

            End Function
            ''Sanjog-Added on 20101117 for dont show the given Immunization in combo
            Public Function PatientImmunization(ByVal _patientId As Int64) As DataTable
                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim oDataTable As DataTable = Nothing
                Dim _strSQL As String = ""
                Try
                    ODB.Connect(GetConnectionString)
                    _strSQL = "SELECT IM_MST.im_item_Name, IM_Trn_Dtl.im_trn_CounterID FROM IM_Trn_Mst INNER JOIN IM_Trn_Dtl ON IM_Trn_Mst.im_trn_mst_Id = IM_Trn_Dtl.im_trn_mst_Id INNER JOIN IM_MST ON IM_Trn_Dtl.im_trn_ItemID = IM_MST.im_item_Id WHERE IM_Trn_Mst.im_trn_mst_nPatientID=" & _patientId & ""
                    oDataTable = ODB.ReadQueryDataTable(_strSQL)
                    If Not oDataTable Is Nothing Then
                        Return oDataTable
                    Else
                        Return Nothing
                    End If
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally

                    _strSQL = " "
                    'If Not IsNothing(oDataTable) Then    'obj Disposed by mitesh
                    '    oDataTable.Dispose()
                    '    oDataTable = Nothing
                    'End If
                    If Not IsNothing(ODB) Then
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                End Try
            End Function
            ''Sanjog-Added on 20101117 for dont show the given Immunization in combo


            Public Function ItemDetail(ByVal dt As DataTable) As gloStream.Immunization.Supporting.ImmunizationItem
                Dim oMstItem As New gloStream.Immunization.Supporting.ImmunizationItem

                Try
                    If IsNothing(dt) = False Then
                        If dt.Rows.Count > 0 Then
                            If Not IsDBNull(dt.Rows(0).Item("im_item_Id")) Then
                                oMstItem.ID = dt.Rows(0).Item("im_item_Id")
                            End If

                            If Not IsDBNull(dt.Rows(0).Item("im_item_Name")) Then
                                oMstItem.Name = dt.Rows(0).Item("im_item_Name")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_item_Count")) Then
                                oMstItem.HowMany = dt.Rows(0).Item("im_item_Count")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_cpt_code")) Then
                                oMstItem.CPTCode = dt.Rows(0).Item("im_cpt_code")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_vaccine_code")) Then
                                oMstItem.VaccineCode = dt.Rows(0).Item("im_vaccine_code")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sConceptID")) Then
                                oMstItem.ConceptID = dt.Rows(0).Item("im_sConceptID")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sDescriptionID")) Then
                                oMstItem.DescriptionID = dt.Rows(0).Item("im_sDescriptionID")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sSnoMedID")) Then
                                oMstItem.SnoMedID = dt.Rows(0).Item("im_sSnoMedID")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sICD9")) Then
                                oMstItem.ICD9 = dt.Rows(0).Item("im_sICD9")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sSnomedDescription")) Then
                                oMstItem.SnomedDescription = dt.Rows(0).Item("im_sSnomedDescription")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sSnomedDefination")) Then
                                oMstItem.SnomedDefinition = dt.Rows(0).Item("im_sSnomedDefination")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sTranID1")) Then
                                oMstItem.RxnormID = dt.Rows(0).Item("im_sTranID1")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sTranID2")) Then
                                oMstItem.NDCCode = dt.Rows(0).Item("im_sTranID2")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sSKU")) Then
                                oMstItem.SKU = dt.Rows(0).Item("im_sSKU")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_dtReceivedDate")) Then
                                oMstItem.ReceivedDate = dt.Rows(0).Item("im_dtReceivedDate")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sActive")) Then
                                oMstItem.Active = dt.Rows(0).Item("im_sActive")
                            End If
                            'If Not IsDBNull(dt.Rows(0).Item("im_sCVXCode")) Then
                            '    oMstItem.CVXCode = dt.Rows(0).Item("im_sCVXCode")
                            'End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sVaccine")) Then
                                oMstItem.Vaccine = dt.Rows(0).Item("im_sVaccine")
                            End If
                            'If Not IsDBNull(dt.Rows(0).Item("im_sMVXCode")) Then
                            '    oMstItem.MVXCode = dt.Rows(0).Item("im_sMVXCode")
                            'End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sManufacturer")) Then
                                oMstItem.Manufacturer = dt.Rows(0).Item("im_sManufacturer")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sTradeName")) Then
                                oMstItem.TradeName = dt.Rows(0).Item("im_sTradeName")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_LotNumber")) Then
                                oMstItem.LotNo = dt.Rows(0).Item("im_LotNumber")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_dtExpiration")) Then
                                oMstItem.ExpiryDate = dt.Rows(0).Item("im_dtExpiration")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_item_VialCount")) Then
                                oMstItem.VialCount = dt.Rows(0).Item("im_item_VialCount")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_DosesperVial")) Then
                                oMstItem.DosesPerVial = dt.Rows(0).Item("im_DosesperVial")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_AvailableDoses")) Then
                                oMstItem.AvailableDoses = dt.Rows(0).Item("im_AvailableDoses")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sVIS")) Then
                                oMstItem.VIS = dt.Rows(0).Item("im_sVIS")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_dtPublication")) Then
                                oMstItem.PublicationDate = dt.Rows(0).Item("im_dtPublication")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_Diagnosis_Code")) Then
                                oMstItem.DiagnosisCode = dt.Rows(0).Item("im_Diagnosis_Code")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sNDCCode")) Then
                                oMstItem.NDCCode1 = dt.Rows(0).Item("im_sNDCCode")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sFundingSource")) Then
                                oMstItem.FundingSource = dt.Rows(0).Item("im_sFundingSource")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sComments")) Then
                                oMstItem.Comments = dt.Rows(0).Item("im_sComments")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_nDocumentID")) Then
                                oMstItem.DocumentID = dt.Rows(0).Item("im_nDocumentID")
                            End If
                            If Not IsDBNull(dt.Rows(0).Item("im_sLocation")) Then
                                oMstItem.Location = dt.Rows(0).Item("im_sLocation")
                            End If

                            If Not IsDBNull(dt.Rows(0).Item("im_bTrackInventory")) Then
                                oMstItem.bTrackInventory = dt.Rows(0).Item("im_bTrackInventory")
                            End If

                            If Not IsDBNull(dt.Rows(0).Item("CategoryID")) Then
                                oMstItem.CategoryID = dt.Rows(0).Item("CategoryID")
                            End If

                        End If
                    End If

                    If Not oMstItem Is Nothing Then
                        Return oMstItem
                    Else
                        Return Nothing
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally
                    'If IsNothing(dt) = False Then
                    '    dt.Dispose()
                    '    dt = Nothing
                    'End If
                    If IsNothing(oMstItem) = False Then
                        oMstItem = Nothing
                    End If
                End Try


            End Function
            'old logic to populate Immunization details
            Public Function ItemDetailsforView() As gloStream.Immunization.Supporting.ImmunizationItems
                Dim _ItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems
                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing
                Dim _strSQL As String = ""

                Try

                    ODB.Connect(GetConnectionString)

                    'set the qry
                    _strSQL = "select im_item_Id,im_item_Name,im_item_Count from IM_MST"

                    'execute the query and return a datareader
                    oDataReader = ODB.ReadQueryRecords(_strSQL)

                    'get the data from datareader and assign it to ImmunizationItem object
                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If IsDBNull(oDataReader.Item("im_item_Id")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Name")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Count")) = False Then
                                    _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count"))
                                End If
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    Return _ItemDetails

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally
                    ODB.Disconnect()
                    If Not IsNothing(oDataReader) Then    'obj Disposed by mitesh
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                    If Not IsNothing(ODB) Then
                        ODB.Disconnect()
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                End Try
            End Function
            'old logic to populate Immunization details commented by supriya 


            Public Function ItemDetails(ByVal PatientID As Long) As gloStream.Immunization.Supporting.ImmunizationItems
                Dim _ItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems
                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing
                Dim _strProcedureName As String = ""

                Try

                    ODB.Connect(GetConnectionString)

                    'set the qry
                    _strProcedureName = "Im_GetImmunizationDetails"

                    ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    'ODB.DBParameters.Add("@npatientid", gnPatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    ODB.DBParameters.Add("@npatientid", PatientID, ParameterDirection.Input, SqlDbType.BigInt)
                    'end modification

                    'execute the query and return a datareader
                    oDataReader = ODB.ReadRecords(_strProcedureName)



                    'get the data from datareader and assign it to ImmunizationItem object
                    If Not oDataReader Is Nothing Then

                        If oDataReader.VisibleFieldCount = 4 Then 'will return number of columns  = 5, therefore will execute the else part of the selected stored procedure "Im_GetImmunizationDetails"
                            If oDataReader.HasRows = True Then

                                While oDataReader.Read
                                    If IsDBNull(oDataReader.Item("im_item_Id")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Name")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Count")) = False Then
                                        _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                    End If
                                End While
                            End If
                            oDataReader.Close()

                        Else 'will return number of columns  = 6, therefore will execute the if part of the selected stored procedure "Im_GetImmunizationDetails"

                            If Not oDataReader Is Nothing Then
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        'If Not (IsDBNull(oDataReader.Item("im_item_Id")) And IsDBNull(oDataReader.Item("im_item_Name")) And IsDBNull(oDataReader.Item("im_item_Count1"))) Then
                                        '    _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count1"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                        'Else
                                        '    _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count2"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                        'End If
                                        ''Sandip Darade  20100805 
                                        ''get the bigger count for immunization 
                                        If IsDBNull(oDataReader.Item("im_item_Id")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Name")) = False Then
                                            If (oDataReader.Item("im_item_Count1") > oDataReader.Item("im_item_Count2")) Then
                                                _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count1"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                            Else
                                                _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count2"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                            End If
                                        End If
                                    End While
                                End If
                                oDataReader.Close()
                            End If

                        End If ' If oDataReader.VisibleFieldCount

                    End If

                    Return _ItemDetails

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally
                    ODB.Disconnect()
                    If Not IsNothing(oDataReader) Then    'obj Disposed by mitesh
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                    If Not IsNothing(ODB) Then
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                End Try
            End Function

            Public Function ItemDetails() As gloStream.Immunization.Supporting.ImmunizationItems
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                Dim _ItemDetails As New gloStream.Immunization.Supporting.ImmunizationItems
                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing
                Dim _strProcedureName As String = ""

                Try

                    ODB.Connect(GetConnectionString)

                    'set the qry
                    'Code Start-Added & commenetd by kanchan on 20100904
                    '_strProcedureName = "Im_GetImmunizationDetails"
                    _strProcedureName = "Im_GetImmunization"

                    'ODB.DBParameters.Add("@npatientid", gnPatientID, ParameterDirection.Input, SqlDbType.BigInt)

                    'execute the query and return a datareader
                    oDataReader = ODB.ReadRecords(_strProcedureName)



                    'get the data from datareader and assign it to ImmunizationItem object
                    If Not oDataReader Is Nothing Then

                        If oDataReader.VisibleFieldCount = 4 Then 'will return number of columns  = 5, therefore will execute the else part of the selected stored procedure "Im_GetImmunizationDetails"
                            If oDataReader.HasRows = True Then

                                While oDataReader.Read
                                    If IsDBNull(oDataReader.Item("im_item_Id")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Name")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Count")) = False Then
                                        _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                    End If
                                End While
                            End If
                            oDataReader.Close()

                        Else 'will return number of columns  = 6, therefore will execute the if part of the selected stored procedure "Im_GetImmunizationDetails"

                            If Not oDataReader Is Nothing Then
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        If IsDBNull(oDataReader.Item("im_item_Id")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Name")) = False AndAlso IsDBNull(oDataReader.Item("im_item_Count1")) = False Then
                                            _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count1"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                        Else
                                            _ItemDetails.Add(oDataReader.Item("im_item_Id"), oDataReader.Item("im_item_Name"), oDataReader.Item("im_item_Count2"), oDataReader.Item("im_vaccine_code")) ', oDataReader.Item("im_cpt_code")
                                        End If
                                    End While
                                End If
                                oDataReader.Close()
                            End If

                        End If ' If oDataReader.VisibleFieldCount

                    End If

                    Return _ItemDetails

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                Finally
                    ODB.Disconnect()
                    If Not IsNothing(oDataReader) Then    'obj Disposed by mitesh
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                    If Not IsNothing(ODB) Then
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                End Try
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
            End Function

            Public Function getCPTCodes(ByVal itemId As Long, ByVal vaccCode As Int16) As String
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                'Dim oMstItem As New gloStream.Immunization.Supporting.ImmunizationItem
                Dim conn As New SqlClient.SqlConnection(GetConnectionString)
                Dim cmdGetCPTCodes As New SqlClient.SqlCommand
                Dim _strSQL As String = ""
                Dim CPTCodes As String = ""


                Try
                    conn.Open()

                    With cmdGetCPTCodes
                        .Connection = conn
                        .CommandType = CommandType.Text
                        ' .CommandText = "select im_cpt_code from IM_MST where im_item_Id = " & itemId & " and im_vaccine_code = " & vaccCode
                        If vaccCode <> -1 Then
                            .CommandText = "select im_cpt_code from IM_MST where im_item_Id = " & itemId & " and  im_vaccine_code = " & vaccCode
                        Else
                            .CommandText = "select im_cpt_code from IM_MST where im_item_Id = " & itemId & " "

                        End If

                    End With

                    CPTCodes = cmdGetCPTCodes.ExecuteScalar
                    If IsDBNull(CPTCodes) OrElse CPTCodes = "" Then
                        Return ""
                    Else
                        Return CPTCodes
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Return Nothing
                Finally
                    If conn.State = ConnectionState.Open Then
                        conn.Close()
                    End If
                    If Not IsNothing(cmdGetCPTCodes) Then    'obj Disposed by mitesh
                        cmdGetCPTCodes.Parameters.Clear()
                        cmdGetCPTCodes.Dispose()
                        cmdGetCPTCodes = Nothing
                    End If
                    If Not IsNothing(conn) Then
                        conn.Dispose()
                        conn = Nothing
                    End If
                End Try
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
            End Function


            Public Function ImmunizationList_old(ByVal PatientID As Long) As DataTable
                Dim objCon As New SqlConnection
                Dim da As SqlDataAdapter = Nothing

                Dim cmd As SqlCommand = Nothing
                Try
                    objCon.ConnectionString = GetConnectionString()
                    cmd = New SqlCommand("Im_GetImmunizationDetails_New", objCon)
                    cmd.CommandType = CommandType.StoredProcedure
                    Dim IMParam As SqlParameter

                    IMParam = cmd.Parameters.Add("@npatientid", SqlDbType.BigInt)
                    IMParam.Direction = ParameterDirection.Input
                    IMParam.Value = PatientID

                    objCon.Open()
                    da = New SqlDataAdapter
                    da.SelectCommand = cmd
                    Dim dt As DataTable = Nothing
                    dt = New DataTable
                    da.Fill(dt)
                    objCon.Close()
                    IMParam = Nothing
                    Return dt

                Catch ex As Exception

                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(da) Then    'obj Disposed by mitesh
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
            Public Function ImmunizationList(ByVal Status As String, ByVal Location As String) As DataTable
                Dim objCon As New SqlConnection
                Dim da As SqlDataAdapter = Nothing

                Dim cmd As SqlCommand = Nothing
                Try


                    objCon.ConnectionString = GetConnectionString()
                    cmd = New SqlCommand("Im_GetImmunizationDetails_New", objCon)
                    cmd.CommandType = CommandType.StoredProcedure
                    Dim IMParam As SqlParameter

                    IMParam = cmd.Parameters.Add("@Status", SqlDbType.VarChar)
                    IMParam.Direction = ParameterDirection.Input
                    IMParam.Value = Status.Replace("'", "''")
                    IMParam = Nothing

                    IMParam = cmd.Parameters.Add("@Location", SqlDbType.VarChar)
                    IMParam.Direction = ParameterDirection.Input
                    If IsNothing(Location) Then
                        IMParam.Value = ""
                    Else
                        IMParam.Value = Location
                    End If

                    IMParam = Nothing

                    objCon.Open()
                    da = New SqlDataAdapter
                    da.SelectCommand = cmd
                    Dim dt As DataTable = Nothing
                    dt = New DataTable
                    da.Fill(dt)
                    objCon.Close()
                    Return dt

                Catch ex As Exception

                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(da) Then    'obj Disposed by mitesh
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

            Public Function GetSKUList() As DataTable
                Dim objCon As New SqlConnection
                Dim da As SqlDataAdapter = Nothing

                Dim cmd As SqlCommand = Nothing
                Try
                    objCon.ConnectionString = GetConnectionString()
                    cmd = New SqlCommand("Im_GetSKUList", objCon)
                    cmd.CommandType = CommandType.StoredProcedure
                    objCon.Open()
                    da = New SqlDataAdapter
                    da.SelectCommand = cmd
                    Dim dt As DataTable = Nothing
                    dt = New DataTable
                    da.Fill(dt)
                    objCon.Close()
                    Return dt

                Catch ex As Exception
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(da) Then    'obj Disposed by mitesh
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

            Public Function FillImmunization(ByVal sFilter As String) As DataTable
                Dim objCon As New SqlConnection
                Dim da As SqlDataAdapter = Nothing

                Dim cmd As SqlCommand = Nothing
                Try


                    objCon.ConnectionString = GetConnectionString()
                    cmd = New SqlCommand("Im_FillImmunization", objCon)
                    cmd.CommandType = CommandType.StoredProcedure
                    Dim IMParam As SqlParameter



                    IMParam = cmd.Parameters.Add("@sFilter", SqlDbType.VarChar)
                    IMParam.Direction = ParameterDirection.Input
                    IMParam.Value = sFilter


                    objCon.Open()
                    da = New SqlDataAdapter
                    da.SelectCommand = cmd
                    Dim dt As DataTable = Nothing
                    dt = New DataTable
                    da.Fill(dt)
                    objCon.Close()
                    IMParam = Nothing
                    Return dt

                Catch ex As Exception

                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(da) Then    'obj Disposed by mitesh
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

            Public Function IsExists(ByVal _EditID As Long, ByVal Vaccine As String, ByVal LotNo As String, ByVal Location As String) As Boolean
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                'Dim oDataReader As SqlClient.SqlDataReader

                'Dim cmd As SqlCommand
                Dim objresult As Object = Nothing
                Try

                    'connect to the database
                    oDB.Connect(GetConnectionString)
                    oDB.DBParameters.Add("@Vaccine", Vaccine, ParameterDirection.Input, SqlDbType.NVarChar)
                    oDB.DBParameters.Add("@LotNo", LotNo, ParameterDirection.Input, SqlDbType.VarChar)
                    oDB.DBParameters.Add("@EditID", _EditID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@Location", Location, ParameterDirection.Input, SqlDbType.VarChar)

                    objresult = oDB.ExecuteScaler("IM_CheckVacccineExists")

                    'check if there is any data in the datareader
                    If Val(objresult) > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally

                    oDB.Disconnect()
                    If Not IsNothing(oDB) Then  'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                'Return True
            End Function
            Public Function IsCustomTradeNameOrCVX(ByVal _TradeName As String, ByVal Vaccine As String, ByVal _type As Integer) As Boolean
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                'Dim oDataReader As SqlClient.SqlDataReader

                ' Dim cmd As SqlCommand
                Dim objresult As Object = Nothing
                Try

                    'connect to the database
                    oDB.Connect(GetConnectionString)
                    oDB.DBParameters.Add("@TradeName", _TradeName, ParameterDirection.Input, SqlDbType.NVarChar)
                    oDB.DBParameters.Add("@Vaccine", Vaccine, ParameterDirection.Input, SqlDbType.NVarChar)
                    oDB.DBParameters.Add("@Type", _type, ParameterDirection.Input, SqlDbType.Int)


                    objresult = oDB.ExecuteScaler("IM_IsExistsCutomTradeNameOrCVX")

                    'check if there is any data in the datareader
                    If Val(objresult) > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally

                    oDB.Disconnect()
                    If Not IsNothing(oDB) Then  'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                'Return True
            End Function

            Public Function IsExistsCPTCode(ByVal sCPTCode As String) As Boolean
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                'Dim oDataReader As SqlClient.SqlDataReader

                Dim objresult As Object = Nothing

                Try

                    'connect to the database
                    oDB.Connect(GetConnectionString)

                    'extract the criteria id from the table for the given criteria name
                    _strSQL = "SELECT count(nCPTID) from CPT_MST WHERE nCLINICID =1 AND sCPTCode= '" & sCPTCode.Replace("'", "''") & "'"
                    'execute the query and return a datareader
                    objresult = oDB.ExecuteQueryScaler(_strSQL)

                    'check if there is any data in the datareader
                    If Val(objresult) > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                    ' Return _Result
                Finally

                    oDB.Disconnect()
                    If Not IsNothing(oDB) Then  'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                'Return True
            End Function

            Public Function IsExistsLabellerCode(ByVal LabellerCode As String, ByVal ProductCode As String) As Boolean
                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                'Dim oDataReader As SqlClient.SqlDataReader

                Dim objresult As Object = Nothing

                Try

                    'connect to the database
                    oDB.Connect(GetConnectionString)
                    oDB.DBParameters.Add("@LabellerCode", LabellerCode, ParameterDirection.Input, SqlDbType.NVarChar)
                    oDB.DBParameters.Add("@ProductCode", ProductCode, ParameterDirection.Input, SqlDbType.NVarChar)


                    objresult = oDB.ExecuteScaler("IM_ISExistsLabellerCode")

                    ''extract the criteria id from the table for the given criteria name
                    '_strSQL = "SELECT count(LabellerCode) from dbo.IMNdcToCvxMvx WHERE LabellerCode= '" & LabellerCode.Replace("'", "''") & "' AND ProductCode= '" & ProductCode & "' "
                    'execute the query and return a datareader


                    'check if there is any data in the datareader
                    If Val(objresult) > 0 Then
                        Return True
                    Else
                        Return False
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    ' Return _Result
                    Return Nothing
                Finally

                    oDB.Disconnect()
                    If Not IsNothing(oDB) Then  'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                'Return True
            End Function
            Public Function IsDelete(ByVal Name As String) As Boolean

                Dim _strSQL As String = ""
                Dim oDB As New gloStream.gloDataBase.gloDataBase
                'Dim oDataReader As SqlClient.SqlDataReader
                'Dim IMTran_ID As Int64
                Dim _Result As String = ""
                Dim _Count As Long = 0

                Try

                    'connect to the database
                    oDB.Connect(GetConnectionString)

                    'extract the criteria id from the table for the given criteria name
                    _strSQL = "SELECT IM_Trn_Dtl.im_trn_ItemID FROM IM_Trn_Dtl INNER JOIN IM_MST ON IM_Trn_Dtl.im_trn_ItemID = IM_MST.im_item_Id " _
& " WHERE(IM_MST.im_item_Name = '" & Name.Replace("'", "''") & "')"

                    'execute the query and return a datareader
                    _Result = oDB.ExecuteQueryScaler(_strSQL)

                    'check if there is any data in the datareader
                    If Val(_Result) > 0 Then
                        Return False
                    Else
                        Return True
                    End If

                    Return _Result
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _errorMessage = ex.Message
                    Return Nothing
                    ' Return _Result
                Finally
                    oDB.Disconnect()
                    If Not IsNothing(oDB) Then  'obj Disposed by mitesh
                        oDB.Dispose()
                        oDB = Nothing
                    End If
                End Try
                'Return True
            End Function

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub

            ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
            'Code Start-Added by kanchan on 20100904 for snomed implementation in immunization
            Public Function GetSnoMedids(ByVal _ItemID As Long) As DataTable
                Dim objCon As New SqlConnection
                Dim da As SqlDataAdapter = Nothing

                Try
                    objCon.ConnectionString = GetConnectionString()
                    objCon.Open()

                    Dim cmd As New SqlCommand()
                    cmd.Connection = objCon
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = "Select isnull(im_sConceptID,'') as im_sConceptID,isnull(im_sDescriptionID,'') as im_sDescriptionID,isnull(im_sSnoMedID,'') as im_sSnoMedID from IM_MST where im_item_Id=" & _ItemID & ""
                    da = New SqlDataAdapter
                    da.SelectCommand = cmd
                    Dim dt As DataTable = Nothing
                    dt = New DataTable
                    da.Fill(dt)

                    If cmd IsNot Nothing Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If

                    Return dt
                Catch ex As Exception
                    Return Nothing
                Finally
                    If Not IsNothing(da) Then    'obj Disposed by mitesh
                        da.Dispose()
                        da = Nothing
                    End If

                    If Not IsNothing(objCon) Then
                        objCon.Dispose()
                        objCon = Nothing
                    End If
                End Try
            End Function
            'Code End-Added by kanchan on 20100904 for snomed implementation in immunization

            'Code Start-Added by kanchan on 20100906 for reaction & history interaction
            Public Function GetSnoMedids(ByVal _ItemName As String) As DataTable
                Dim objCon As New SqlConnection
                Dim da As SqlDataAdapter = Nothing

                Try
                    objCon.ConnectionString = GetConnectionString()
                    objCon.Open()
                    da = New SqlDataAdapter
                    Dim cmd As New SqlCommand()
                    cmd.Connection = objCon
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = "Select isnull(im_sConceptID,'') as im_sConceptID,isnull(im_sDescriptionID,'') as im_sDescriptionID,isnull(im_sSnoMedID,'') as im_sSnoMedID,ISNULL(im_sSnomedDescription,'') AS sSnomedDescription,ISNULL(im_sTranID1,'') AS sTranID1,ISNULL(im_sTranID2,'') AS sTranID2,ISNULL(im_sICD9,'') AS sICD9,ISNULL(im_sSnomedDefination,'') AS im_sSnomedDefination from IM_MST where im_item_Name='" & _ItemName & "'"
                    da.SelectCommand = cmd
                    Dim dt As DataTable = Nothing
                    dt = New DataTable
                    da.Fill(dt)

                    If cmd IsNot Nothing Then
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If

                    Return dt
                Catch ex As Exception
                    Return Nothing
                Finally
                    If Not IsNothing(da) Then    'obj Disposed by mitesh
                        da.Dispose()
                        da = Nothing
                    End If

                    If Not IsNothing(objCon) Then
                        objCon.Dispose()
                        objCon = Nothing
                    End If
                End Try
            End Function
            'Code End-Added by kanchan on 20100906 for reaction & history interaction

            Public Function getVaccineTypes(ByVal EditID As Long) As DataSet
                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    Cmd = New System.Data.SqlClient.SqlCommand("IM_GetVaccineTypes", Con)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Parameters.Add("@EditID", SqlDbType.BigInt)
                    Cmd.Parameters("@EditID").Value = EditID

                    sqladpt.SelectCommand = Cmd
                    Dim ds As New DataSet
                    sqladpt.Fill(ds)

                    Con.Close()

                    ds.Tables(0).TableName = "FundingSource"
                    ds.Tables(1).TableName = "Location"
                    ds.Tables(2).TableName = "Category"

                    If ds.Tables.Count = 4 Then
                        ds.Tables(3).TableName = "Master"
                    End If

                    Return ds
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If

                End Try

            End Function


            Public Function getNDCCode(ByVal _labellercode As String, ByVal _productcode As String) As DataTable
                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    ''Cmd = New System.Data.SqlClient.SqlCommand("SELECT DISTINCT N.CodeList,N.[CvxCode],N.[MvxCode],N.[ManufacturerName],N.[ShortDescription], T.[CdcProductName] FROM dbo.IMNdcToCvxMvx N INNER JOIN dbo.IMTradeName T ON n.[CvxCode]= T.[CvxCode]  WHERE LabellerCode = '" & _labellercode & "' and ProductCode= '" & _productcode & "' ", Con)
                    Cmd = New System.Data.SqlClient.SqlCommand("IM_GetNDCCodeFromBarcode", Con)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Parameters.Add("@LabellerCode", SqlDbType.NVarChar)
                    Cmd.Parameters("@LabellerCode").Value = _labellercode



                    Cmd.Parameters.Add("@ProductCode", SqlDbType.NVarChar)
                    Cmd.Parameters("@ProductCode").Value = _productcode



                    sqladpt.SelectCommand = Cmd
                    Dim dt As New DataTable

                    sqladpt.Fill(dt)

                    Con.Close()

                    Return dt
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If


                End Try

            End Function
            Public Function getNDCCode1(ByVal _SKU As String) As DataSet

                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    ''Cmd = New System.Data.SqlClient.SqlCommand("SELECT DISTINCT N.CodeList,N.[CvxCode],N.[MvxCode],N.[ManufacturerName],N.[ShortDescription], T.[CdcProductName] FROM dbo.IMNdcToCvxMvx N INNER JOIN dbo.IMTradeName T ON n.[CvxCode]= T.[CvxCode]  WHERE LabellerCode = '" & _labellercode & "' and ProductCode= '" & _productcode & "' ", Con)
                    Cmd = New System.Data.SqlClient.SqlCommand("IM_GetNDCCodeFromBarcodeNew", Con)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Parameters.Add("@SKU", SqlDbType.NVarChar)
                    Cmd.Parameters("@SKU").Value = _SKU
                    sqladpt.SelectCommand = Cmd
                    Dim ds As New DataSet
                    sqladpt.Fill(ds)
                    If IsNothing(ds) = False Then
                        If ds.Tables.Count > 0 Then
                            ds.Tables(0).TableName = "CPT"
                            ds.Tables(1).TableName = "NDCInfo"
                        End If


                    End If

                    Con.Close()

                    Return ds
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If
                    'If Not IsNothing(dt) Then
                    '    dt.Dispose()
                    '    dt = Nothing
                    'End If

                End Try

            End Function

            Public Function getInformation(ByVal _CVXCode As String) As DataTable

                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    Cmd = New System.Data.SqlClient.SqlCommand("SELECT C.[CptCode] FROM dbo.IMCptToCvx  C where C.[CvxCode] = '" & _CVXCode & "' ", Con)
                    Cmd.CommandType = CommandType.Text


                    sqladpt.SelectCommand = Cmd
                    Dim dt As New DataTable
                    sqladpt.Fill(dt)

                    Con.Close()

                    Return dt
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If


                End Try

            End Function
            Public Function getLocation() As DataTable

                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    Cmd = New System.Data.SqlClient.SqlCommand("SELECT nLocationID, sLocation,bIsDefault FROM AB_Location WHERE bIsBlocked = 0 ORDER By SLocation", Con)
                    Cmd.CommandType = CommandType.Text


                    sqladpt.SelectCommand = Cmd
                    Dim dt As New DataTable
                    sqladpt.Fill(dt)

                    Con.Close()

                    Return dt
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If


                End Try

            End Function
            Public Function getVaccineType(ByVal _CVXCode As String) As DataTable

                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    Cmd = New System.Data.SqlClient.SqlCommand("IM_GetVaccineType", Con)
                    Cmd.CommandType = CommandType.StoredProcedure

                    Dim IMParam As SqlParameter

                    IMParam = Cmd.Parameters.Add("@CVXCode", SqlDbType.NVarChar)
                    IMParam.Direction = ParameterDirection.Input
                    IMParam.Value = _CVXCode
                    sqladpt.SelectCommand = Cmd
                    Dim dt As New DataTable
                    sqladpt.Fill(dt)

                    Con.Close()
                    IMParam = Nothing
                    Return dt
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If


                End Try

            End Function
            Public Function getManufacturerTypes() As DataTable

                Dim Con As New System.Data.SqlClient.SqlConnection(GetConnectionString())
                Dim sqladpt As New System.Data.SqlClient.SqlDataAdapter
                Dim Cmd As New System.Data.SqlClient.SqlCommand

                Try

                    Cmd = New System.Data.SqlClient.SqlCommand("IM_GetVaccineTypes", Con)
                    Cmd.CommandType = CommandType.StoredProcedure
                    'Dim IMParam As New SqlParameter


                    sqladpt.SelectCommand = Cmd
                    Dim dt As New DataTable
                    sqladpt.Fill(dt)

                    Con.Close()

                    Return dt
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If Not IsNothing(Cmd) Then
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                    End If
                    If Not IsNothing(sqladpt) Then
                        sqladpt.Dispose()
                        sqladpt = Nothing
                    End If
                    If Not IsNothing(Con) Then
                        Con.Dispose()
                        Con = Nothing
                    End If


                End Try

            End Function


        End Class
        

        Public Class Common

            Private _ErrorMessage As String
            '''' solving case- GLO2010-0004659
            Private _CategoryType_Manufacturer As String = "Manufacturer"
            ''end
            Private _CategoryType_Site As String = "Site"
            Private _CategoryType_Route As String = "Route"

            Public Property ErrorMessage() As String
                Get
                    Return _ErrorMessage
                End Get
                Set(ByVal Value As String)
                    _ErrorMessage = Value
                End Set
            End Property

            Public ReadOnly Property CategoryType_Manufacurer() As String
                Get
                    Return _CategoryType_Manufacturer
                End Get
            End Property

       
            Public Function Manufacturers() As gloStream.Immunization.Supporting.ItemDetails
                Dim sqlconn As SqlConnection
                Dim _MItemDetails As New gloStream.Immunization.Supporting.ItemDetails
                Dim sqlcmd As SqlCommand = Nothing
                Dim strsqlconn As String = ""
                strsqlconn = GetConnectionString()
                sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
                sqlconn.Open()
                Dim strsql As String = ""
                Dim dr As SqlDataReader = Nothing
                Try
                    strsql = "select isnull(sManufacturerCode,''),isnull(sManufacturer,'') from  Im_Manufacturers where sCodeType ='Manufacturer'"
                    sqlcmd = New SqlCommand(strsql, sqlconn)
                    dr = sqlcmd.ExecuteReader
                    If Not IsNothing(dr) Then
                        While dr.Read
                            _MItemDetails.Add(dr.Item(1))
                        End While
                        dr.Close()
                    End If
                    Return _MItemDetails

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    Throw New ClsImmunizationException(ex.ToString)
                Finally
                   
                    If Not IsNothing(dr) Then

                        dr.Close()
                        dr = Nothing
                    End If
                   
                    If Not IsNothing(sqlcmd) Then
                        sqlcmd.Parameters.Clear()
                        sqlcmd.Dispose()
                        sqlcmd = Nothing
                    End If
                    If Not IsNothing(sqlconn) Then
                        If sqlconn.State = ConnectionState.Open Then
                            sqlconn.Close()
                        End If
                        sqlconn.Dispose()
                        sqlconn = Nothing
                    End If
                End Try
            End Function
            Public Function EligibilityCodes() As gloStream.Immunization.Supporting.ItemDetails
                Dim sqlconn As SqlConnection
                Dim _MItemDetails As New gloStream.Immunization.Supporting.ItemDetails
                Dim sqlcmd As SqlCommand = Nothing
                Dim strsqlconn As String = ""
                strsqlconn = GetConnectionString()
                sqlconn = New System.Data.SqlClient.SqlConnection(strsqlconn)
                sqlconn.Open()
                Dim strsql As String = ""
                Dim dr As SqlDataReader = Nothing
                Try
                    strsql = "select isnull(sManufacturerCode,''),isnull(sManufacturer,'') from  Im_Manufacturers where sCodeType ='VaccineEligibility'"
                    sqlcmd = New SqlCommand(strsql, sqlconn)
                    dr = sqlcmd.ExecuteReader
                    If Not IsNothing(dr) Then
                        While dr.Read
                            _MItemDetails.Add(dr.Item(1))
                        End While
                        dr.Close()
                    End If
                    Return _MItemDetails

                Catch ex As Exception
                    Throw New ClsImmunizationException(ex.ToString)

                Finally
                    If Not IsNothing(dr) Then
                        dr.Close()
                        dr = Nothing
                    End If
                    If Not IsNothing(sqlcmd) Then
                        sqlcmd.Parameters.Clear()
                        sqlcmd.Dispose()
                        sqlcmd = Nothing
                    End If
                    If Not IsNothing(sqlconn) Then
                        If sqlconn.State = ConnectionState.Open Then
                            sqlconn.Close()
                        End If
                        sqlconn.Dispose()
                        sqlconn = Nothing
                    End If
                End Try
            End Function
            '' FOr selection of Sites added on 20070425 
            Public Function Sites() As gloStream.Immunization.Supporting.ItemDetails
                'From Category_Mst where type = 'Site' (Hard Coded)

                Dim _MSiteDetails As New gloStream.Immunization.Supporting.ItemDetails
                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing
                Dim _strSQL As String = ""

                Try
                    'connect to the database
                    ODB.Connect(GetConnectionString)

                    'set the query
                    _strSQL = "select   nCategoryID, sDescription from Category_Mst where sCategoryType = '" & _CategoryType_Site & "'"

                    'Execute the qry and return a datareader
                    oDataReader = ODB.ReadQueryRecords(_strSQL)

                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If IsDBNull(oDataReader.Item("nCategoryID")) = False AndAlso IsDBNull(oDataReader.Item("sDescription")) = False Then
                                    _MSiteDetails.Add(oDataReader.Item("nCategoryID"), oDataReader.Item("sDescription"))
                                End If
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    Return _MSiteDetails

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = ex.Message
                    Return Nothing
                Finally
                    ODB.Disconnect()
                    If Not oDataReader.IsClosed Then
                        oDataReader.Close()
                    End If
                    If Not IsNothing(oDataReader) Then   'obj Disposed by mitesh
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                    If Not IsNothing(ODB) Then
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                End Try

            End Function

            Public Function Route() As gloStream.Immunization.Supporting.ItemDetails
                'From Category_Mst where type = 'Route' (Hard Coded)

                Dim _MRouteDetails As New gloStream.Immunization.Supporting.ItemDetails
                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing
                Dim _strSQL As String = ""

                Try
                    'connect to the database
                    ODB.Connect(GetConnectionString)

                    'set the query
                    _strSQL = "select   nCategoryID, sDescription from Category_Mst where sCategoryType = '" & _CategoryType_Route & "'"

                    'Execute the qry and return a datareader
                    oDataReader = ODB.ReadQueryRecords(_strSQL)

                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If IsDBNull(oDataReader.Item("nCategoryID")) = False AndAlso IsDBNull(oDataReader.Item("sDescription")) = False Then
                                    _MRouteDetails.Add(oDataReader.Item("nCategoryID"), oDataReader.Item("sDescription"))
                                End If
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    Return _MRouteDetails

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = ex.Message
                    Return Nothing
                Finally
                    ODB.Disconnect()
                    If Not oDataReader.IsClosed Then
                        oDataReader.Close()
                    End If
                    If Not IsNothing(oDataReader) Then   'obj Disposed by mitesh
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                    If Not IsNothing(ODB) Then
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                End Try
            End Function


            
            Public Function CPTCodes() As gloStream.Immunization.Supporting.ItemDetails
                'From Category_Mst where type = 'Route' (Hard Coded)

                Dim _MCPTCodes As New gloStream.Immunization.Supporting.ItemDetails
                Dim ODB As New gloStream.gloDataBase.gloDataBase
                Dim oDataReader As SqlClient.SqlDataReader = Nothing
                Dim _strSQL As String = ""

                Try
                    'connect to the database
                    ODB.Connect(GetConnectionString)

                    'set the query
                    _strSQL = "select isnull(im_cpt_code,'') as CPTCodes from IM_MST"

                    'Execute the qry and return a datareader
                    oDataReader = ODB.ReadQueryRecords(_strSQL)

                    If Not oDataReader Is Nothing Then
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                If Not IsDBNull(oDataReader.Item("CPTCodes")) Then
                                    _MCPTCodes.Add(oDataReader.Item("CPTCodes"))

                                End If
                            End While
                        End If
                        oDataReader.Close()
                    End If

                    Return _MCPTCodes

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Validate, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    _ErrorMessage = ex.Message
                    Return Nothing
                Finally
                    ODB.Disconnect()
                    If Not oDataReader.IsClosed Then
                        oDataReader.Close()
                    End If
                    If Not IsNothing(oDataReader) Then   'obj Disposed by mitesh
                        oDataReader.Dispose()
                        oDataReader = Nothing
                    End If
                    If Not IsNothing(ODB) Then
                        ODB.Dispose()
                        ODB = Nothing
                    End If
                End Try
            End Function

          

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Namespace Supporting

            Public Class ImmunizationItem
                Private _ID As Long
                Private _Name As String
                Private _HowMany As Integer
                Private _CPTCode As String
                Private _VaccineCode As String


                ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007
                Private _SnoMedID As String = ""
                Private _ConceptID As String = ""
                Private _DescriptionID As String = ""
                Private _ICD9 As String = ""
                Private _SnomedDescription As String = ""
                Private _RxnormID As String = ""
                Private _NDCCode As String = ""
                Private _SnomedDefinition As String = ""

                Private _SKU As String
                Private _ReceivedDate As DateTime
                Private _Active As String = ""
                'Private _CVXCode As String = ""
                'Private _ShortDescription As String = ""
                'Private _MVXCode As String = ""
                Private _Vaccine As String = ""
                Private _Manufacturer As String = ""
                Private _TradeName As String = ""
                Private _LotNo As String = ""
                Private _ExpiryDate As DateTime
                Private _VialCount As Decimal
                Private _DosesPerVial As Decimal
                Private _AvailableDoses As Decimal
                Private _VIS As String = ""
                Private _PublicationDate As DateTime
                Private _DiagnosisCode As String = ""
                ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS
                Private _NDCCode1 As String = ""
                Private _FundingSource As String = ""
                Private _Comments As String = ""
                Private _DocumentID As Long
                Private _Location As String = ""
                Private _bTrackInventory As Boolean
                Private _CategoryID As Long

                Public Property SnoMedID() As String
                    Get
                        Return _SnoMedID
                    End Get
                    Set(ByVal Value As String)
                        _SnoMedID = Value
                    End Set
                End Property
                Public Property ConceptID() As String
                    Get
                        Return _ConceptID
                    End Get
                    Set(ByVal Value As String)
                        _ConceptID = Value
                    End Set
                End Property
                Public Property DescriptionID() As String
                    Get
                        Return _DescriptionID
                    End Get
                    Set(ByVal Value As String)
                        _DescriptionID = Value
                    End Set
                End Property
                Public Property ICD9() As String
                    Get
                        Return _ICD9
                    End Get
                    Set(ByVal Value As String)
                        _ICD9 = Value
                    End Set
                End Property
                Public Property SnomedDescription() As String
                    Get
                        Return _SnomedDescription
                    End Get
                    Set(ByVal Value As String)
                        _SnomedDescription = Value
                    End Set
                End Property
                Public Property SnomedDefinition() As String
                    Get
                        Return _SnomedDefinition
                    End Get
                    Set(ByVal Value As String)
                        _SnomedDefinition = Value
                    End Set
                End Property
                Public Property RxnormID() As String
                    Get
                        Return _RxnormID
                    End Get
                    Set(ByVal Value As String)
                        _RxnormID = Value
                    End Set
                End Property
                Public Property NDCCode() As String
                    Get
                        Return _NDCCode
                    End Get
                    Set(ByVal Value As String)
                        _NDCCode = Value
                    End Set
                End Property
                Public Property SKU() As String
                    Get
                        Return _SKU
                    End Get
                    Set(ByVal Value As String)
                        _SKU = Value
                    End Set
                End Property
                Public Property ReceivedDate() As DateTime
                    Get
                        Return _ReceivedDate
                    End Get
                    Set(ByVal Value As DateTime)
                        _ReceivedDate = Value
                    End Set
                End Property
                Public Property Active() As String
                    Get
                        Return _Active
                    End Get
                    Set(ByVal Value As String)
                        _Active = Value
                    End Set
                End Property
                'Public Property CVXCode() As String
                '    Get
                '        Return _CVXCode
                '    End Get
                '    Set(ByVal Value As String)
                '        _CVXCode = Value
                '    End Set
                'End Property
                'Public Property ShortDescription() As String
                '    Get
                '        Return _ShortDescription
                '    End Get
                '    Set(ByVal Value As String)
                '        _ShortDescription = Value
                '    End Set
                'End Property
                Public Property Vaccine() As String
                    Get
                        Return _Vaccine
                    End Get
                    Set(ByVal Value As String)
                        _Vaccine = Value
                    End Set
                End Property
                'Public Property MVXCode() As String
                '    Get
                '        Return _MVXCode
                '    End Get
                '    Set(ByVal Value As String)
                '        _MVXCode = Value
                '    End Set
                'End Property
                Public Property Manufacturer() As String
                    Get
                        Return _Manufacturer
                    End Get
                    Set(ByVal Value As String)
                        _Manufacturer = Value
                    End Set
                End Property
                Public Property TradeName() As String
                    Get
                        Return _TradeName
                    End Get
                    Set(ByVal Value As String)
                        _TradeName = Value
                    End Set
                End Property
                Public Property LotNo() As String
                    Get
                        Return _LotNo
                    End Get
                    Set(ByVal Value As String)
                        _LotNo = Value
                    End Set
                End Property
                Public Property ExpiryDate() As DateTime
                    Get
                        Return _ExpiryDate
                    End Get
                    Set(ByVal Value As DateTime)
                        _ExpiryDate = Value
                    End Set
                End Property
                Public Property VialCount() As Decimal
                    Get
                        Return _VialCount
                    End Get
                    Set(ByVal Value As Decimal)
                        _VialCount = Value
                    End Set
                End Property
                Public Property DosesPerVial() As Decimal
                    Get
                        Return _DosesPerVial
                    End Get
                    Set(ByVal Value As Decimal)
                        _DosesPerVial = Value
                    End Set
                End Property
                Public Property AvailableDoses() As Decimal
                    Get
                        Return _AvailableDoses
                    End Get
                    Set(ByVal Value As Decimal)
                        _AvailableDoses = Value
                    End Set
                End Property
                Public Property VIS() As String
                    Get
                        Return _VIS
                    End Get
                    Set(ByVal Value As String)
                        _VIS = Value
                    End Set
                End Property
                Public Property PublicationDate() As DateTime
                    Get
                        Return _PublicationDate
                    End Get
                    Set(ByVal Value As DateTime)
                        _PublicationDate = Value
                    End Set
                End Property
                Public Property DiagnosisCode() As String
                    Get
                        Return _DiagnosisCode
                    End Get
                    Set(ByVal Value As String)
                        _DiagnosisCode = Value
                    End Set
                End Property
                ''Bug #80928: 00000521 : NDC CODES WITH LEADING ZEROS
                Public Property NDCCode1() As String
                    Get
                        Return _NDCCode1
                    End Get
                    Set(ByVal Value As String)
                        _NDCCode1 = Value
                    End Set
                End Property
                Public Property FundingSource() As String
                    Get
                        Return _FundingSource
                    End Get
                    Set(ByVal Value As String)
                        _FundingSource = Value
                    End Set
                End Property
                Public Property Comments() As String
                    Get
                        Return _Comments
                    End Get
                    Set(ByVal Value As String)
                        _Comments = Value
                    End Set
                End Property
                Public Property Location() As String
                    Get
                        Return _Location
                    End Get
                    Set(ByVal Value As String)
                        _Location = Value
                    End Set
                End Property

                Public Property bTrackInventory() As Boolean
                    Get
                        Return _bTrackInventory
                    End Get
                    Set(ByVal Value As Boolean)
                        _bTrackInventory = Value
                    End Set
                End Property


                Public Property DocumentID() As Long
                    Get
                        Return _DocumentID
                    End Get
                    Set(ByVal Value As Long)
                        _DocumentID = Value
                    End Set
                End Property
               
                ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007

                Public Property VaccineCode() As String
                    Get
                        Return _VaccineCode
                    End Get
                    Set(ByVal Value As String)
                        _VaccineCode = Value
                    End Set
                End Property
                Public Property CPTCode() As String
                    Get
                        Return _CPTCode
                    End Get
                    Set(ByVal Value As String)
                        _CPTCode = Value
                    End Set
                End Property
                Public Property ID() As Long
                    Get
                        Return _ID
                    End Get
                    Set(ByVal Value As Long)
                        _ID = Value
                    End Set
                End Property

                Public Property Name() As String
                    Get
                        Return _Name
                    End Get
                    Set(ByVal Value As String)
                        _Name = Value
                    End Set
                End Property

                Public Property HowMany() As Integer
                    Get
                        Return _HowMany
                    End Get
                    Set(ByVal Value As Integer)
                        _HowMany = Value
                    End Set
                End Property

                '_CategoryID
                Public Property CategoryID() As Long
                    Get
                        Return _CategoryID
                    End Get
                    Set(ByVal Value As Long)
                        _CategoryID = Value
                    End Set
                End Property


                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

            End Class

            Public Class ImmunizationItems
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oImmunizationItem As gloStream.Immunization.Supporting.ImmunizationItem) As gloStream.Immunization.Supporting.ImmunizationItem
                    mCol.Add(oImmunizationItem)
                    Return Nothing
                End Function


                Public Function Add(ByVal ID As Long, ByVal Name As String, ByVal HowMany As Integer, Optional ByVal VaccineCode As String = "") As gloStream.Immunization.Supporting.ImmunizationItem
                    Dim objNewMember As gloStream.Immunization.Supporting.ImmunizationItem
                    objNewMember = New gloStream.Immunization.Supporting.ImmunizationItem
                    objNewMember.ID = ID
                    objNewMember.Name = Name
                    objNewMember.HowMany = HowMany
                    'objNewMember.CPTCode = CPTCode
                    objNewMember.VaccineCode = VaccineCode
                    mCol.Add(objNewMember)
                    Add = objNewMember
                    objNewMember = Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.Immunization.Supporting.ImmunizationItem
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

            Public Class ImmunizationTransaction
                Private _ID As Long
                Private _PatientID As Long
                Private _TransactionLines As gloStream.Immunization.Supporting.ImmunizationTransactionLines
                Private bAssigned As Boolean = False
                Public Sub Dispose()
                    If (bAssigned) Then
                        _TransactionLines.Dispose()
                        bAssigned = False
                    End If
                End Sub

                Public Property TransactionID() As Long
                    Get
                        Return _ID
                    End Get
                    Set(ByVal Value As Long)
                        _ID = Value
                    End Set
                End Property

                Public Property PatientID() As Long
                    Get
                        Return _PatientID
                    End Get
                    Set(ByVal Value As Long)
                        _PatientID = Value
                    End Set
                End Property

                Public Property TransactionLines() As gloStream.Immunization.Supporting.ImmunizationTransactionLines
                    Get
                        Return _TransactionLines
                    End Get
                    Set(ByVal Value As gloStream.Immunization.Supporting.ImmunizationTransactionLines)
                        If (bAssigned) Then
                            _TransactionLines.Dispose()
                            bAssigned = False
                        End If
                        _TransactionLines = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _TransactionLines = New gloStream.Immunization.Supporting.ImmunizationTransactionLines
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _TransactionLines = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class ImmunizationTransactionLine
                Private _TrnDate As Date
                Private _VisitID As Long = -1
                Private _ItemID As Long = 0
                Private _ItemName As String = ""
                Private _ItemCounterID As Integer = 0
                Private _Dose As String = ""

                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                Private _DoseUnit As String = ""
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006

                Private _AdminStatus As String = ""

                Private _DateGiven As Date
                Private _TimeGiven As String = ""
                Private _Route As String = ""
                Private _LotNumber As String = ""
                Private _ExpiryDate As Date
                Private _Manufacturer As String = ""

                Private _UserID As Long = 0
                Private _Notes As String = ""
                Private _DueDate As Date

                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 20101006
                Private _Reaction As String = ""
                Private _ReactionDT As Date
                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 20101006

                '' modification on 20070425 CCHIT2007
                Private _Site As String = ""
                Private _CPTCode As String = ""
                Private _UserName As String = ""
                ''
                'Sarika 20080604
                Private _EligibilityCode As String = ""
                Private _IsReminder As Boolean = False
                Private _ReasonForNonAdmin As String = ""
                Private _VaccineCode As String = ""
                Private _ItemCount As Integer = 0 ''20100804 Sandip Darade

                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                Private _SnoMedID As String
                Private _ConceptID As String
                Private _DescriptionID As String
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006

                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                Public Property SnoMedID() As String
                    Get
                        Return _SnoMedID
                    End Get
                    Set(ByVal value As String)
                        _SnoMedID = value
                    End Set
                End Property

                Public Property ConceptID() As String
                    Get
                        Return _ConceptID
                    End Get
                    Set(ByVal value As String)
                        _ConceptID = value
                    End Set
                End Property

                Public Property DescriptionID() As String
                    Get
                        Return _DescriptionID
                    End Get
                    Set(ByVal value As String)
                        _DescriptionID = value
                    End Set
                End Property
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006

                Public Property VaccineCode() As String
                    Get
                        Return _VaccineCode
                    End Get
                    Set(ByVal Value As String)
                        _VaccineCode = Value
                    End Set
                End Property
                Public Property TransactionDate() As Date
                    Get
                        Return _TrnDate
                    End Get
                    Set(ByVal Value As Date)
                        _TrnDate = Value
                    End Set
                End Property

                Public Property VisitID() As Long
                    Get
                        Return _VisitID
                    End Get
                    Set(ByVal Value As Long)
                        _VisitID = Value
                    End Set
                End Property

                Public Property ItemID() As Long
                    Get
                        Return _ItemID
                    End Get
                    Set(ByVal Value As Long)
                        _ItemID = Value
                    End Set
                End Property

                Public Property ItemName() As String
                    Get
                        Return _ItemName
                    End Get
                    Set(ByVal Value As String)
                        _ItemName = Value
                    End Set
                End Property
                Public Property ItemCount() As Integer
                    Get
                        Return _ItemCount
                    End Get
                    Set(ByVal Value As Integer)
                        _ItemCount = Value
                    End Set
                End Property

                Public Property ItemCounterID() As Integer
                    Get
                        Return _ItemCounterID
                    End Get
                    Set(ByVal Value As Integer)
                        _ItemCounterID = Value
                    End Set
                End Property

                Public Property Dose() As String
                    Get
                        Return _Dose
                    End Get
                    Set(ByVal Value As String)
                        _Dose = Value
                    End Set
                End Property


                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                Public Property DoseUnit() As String
                    Get
                        Return _DoseUnit
                    End Get
                    Set(ByVal Value As String)
                        _DoseUnit = Value
                    End Set
                End Property
                ''''''''''Added by Ujwala - Immunization Integration - as on 20101006

                ''Sanjog - Added on 2011 March 19 to add admin status in immunization
                Public Property AdminStatus() As String
                    Get
                        Return _AdminStatus
                    End Get
                    Set(ByVal Value As String)
                        _AdminStatus = Value
                    End Set
                End Property
                ''Sanjog - Added on 2011 March 19 to add admin status in immunization

                Public Property DateGiven() As Date
                    Get
                        Return _DateGiven
                    End Get
                    Set(ByVal Value As Date)
                        _DateGiven = Value
                    End Set
                End Property
                Public Property TimeGiven() As String
                    Get
                        Return _TimeGiven
                    End Get
                    Set(ByVal value As String)
                        _TimeGiven = value
                    End Set
                End Property
                Public Property Route() As String
                    Get
                        Return _Route
                    End Get
                    Set(ByVal Value As String)
                        _Route = Value
                    End Set
                End Property

                Public Property LotNumber() As String
                    Get
                        Return _LotNumber
                    End Get
                    Set(ByVal Value As String)
                        _LotNumber = Value
                    End Set
                End Property

                Public Property ExpiryDate() As Date
                    Get
                        Return _ExpiryDate
                    End Get
                    Set(ByVal Value As Date)
                        _ExpiryDate = Value
                    End Set
                End Property

                Public Property Manufacturer() As String
                    Get
                        Return _Manufacturer
                    End Get
                    Set(ByVal Value As String)
                        _Manufacturer = Value
                    End Set
                End Property

                Public Property Notes() As String
                    Get
                        Return _Notes
                    End Get
                    Set(ByVal Value As String)
                        _Notes = Value
                    End Set
                End Property

                Public ReadOnly Property IsLock() As Boolean
                    Get
                        Return False '// Remark
                    End Get
                End Property

                Public Property UserID() As Long
                    Get
                        Return _UserID
                    End Get
                    Set(ByVal Value As Long)
                        _UserID = Value
                    End Set
                End Property

                Public Property DueDate() As Date
                    Get
                        Return _DueDate
                    End Get
                    Set(ByVal Value As Date)
                        _DueDate = Value
                    End Set
                End Property

                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 20101006
                Public Property Reaction() As String
                    Get
                        Return _Reaction
                    End Get
                    Set(ByVal Value As String)
                        _Reaction = Value
                    End Set
                End Property
                Public Property ReactionDT() As Date
                    Get
                        Return _ReactionDT
                    End Get
                    Set(ByVal Value As Date)
                        _ReactionDT = Value
                    End Set
                End Property
                ''''''''''''' Added by Ujwala Atre - to add Raction & Reaction Date  - as on 20101006

                ''''modification on 20070425 CCHIT2007
                Public Property Site() As String
                    Get
                        Return _Site
                    End Get
                    Set(ByVal Value As String)
                        _Site = Value
                    End Set
                End Property

                Public Property CPTCode() As String
                    Get
                        Return _CPTCode
                    End Get
                    Set(ByVal Value As String)
                        _CPTCode = Value
                    End Set
                End Property

                Public Property UserName() As String
                    Get
                        Return _UserName
                    End Get
                    Set(ByVal Value As String)
                        _UserName = Value
                    End Set
                End Property
                ''''
                Public Property IsReminder() As Boolean
                    Get
                        Return _IsReminder
                    End Get
                    Set(ByVal Value As Boolean)
                        _IsReminder = Value
                    End Set
                End Property

                Public Property EligibilityCode() As String
                    Get
                        Return _EligibilityCode
                    End Get
                    Set(ByVal Value As String)
                        _EligibilityCode = Value
                    End Set
                End Property


                Public Property ReasonForNonAdmin() As String
                    Get
                        Return _ReasonForNonAdmin
                    End Get
                    Set(ByVal Value As String)
                        _ReasonForNonAdmin = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _VisitID = -1
                    _ItemID = 0
                    _ItemName = ""
                    _ItemCounterID = 0
                    _Dose = ""

                    ''''''''''Added by Ujwala - Immunization Integration - as on 20101006
                    _DoseUnit = ""
                    ''''''''''Added by Ujwala - Immunization Integration - as on 20101006

                    _TimeGiven = ""
                    _Route = ""
                    _LotNumber = ""

                    _Manufacturer = ""

                    _UserID = 0
                    _Notes = ""


                    _Site = ""
                    _CPTCode = ""
                    _UserName = ""
                    ''
                    'Sarika 20080604
                    _EligibilityCode = ""
                    _IsReminder = False
                    _ReasonForNonAdmin = ""
                    _VaccineCode = ""

                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class ImmunizationTransactionLines
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oImmunizationTransactionLine As gloStream.Immunization.Supporting.ImmunizationTransactionLine) As gloStream.Immunization.Supporting.ImmunizationTransactionLine
                    mCol.Add(oImmunizationTransactionLine)
                    Return Nothing
                End Function


                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.Immunization.Supporting.ImmunizationTransactionLine
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

            '//<<<<<<<<<<<<<< COMMON ITEM ID AND NAME >>>>>>>>>>>>>>>>>>//

            Public Class ItemDetail
                Private _ItemID As Long
                Private _ItemDescription As String

                Public Property ID() As Long
                    Get
                        Return _ItemID
                    End Get
                    Set(ByVal Value As Long)
                        _ItemID = Value
                    End Set
                End Property

                Public Property Description() As String
                    Get
                        Return _ItemDescription
                    End Get
                    Set(ByVal Value As String)
                        _ItemDescription = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            Public Class ItemDetails
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByRef oItemDetail As gloStream.Immunization.Supporting.ItemDetail) As gloStream.Immunization.Supporting.ItemDetail
                    mCol.Add(oItemDetail)
                    Return Nothing
                End Function

                Public Function Add(ByVal oID As Long, ByVal oDescription As String) As gloStream.Immunization.Supporting.ItemDetail
                    'create a new object
                    Dim objNewMember As gloStream.Immunization.Supporting.ItemDetail
                    objNewMember = New gloStream.Immunization.Supporting.ItemDetail
                    objNewMember.ID = oID
                    objNewMember.Description = oDescription
                    mCol.Add(objNewMember)
                    Add = objNewMember
                    objNewMember = Nothing
                End Function
                Public Function Add(ByVal oDescription As String) As gloStream.Immunization.Supporting.ItemDetail
                    'create a new object
                    Dim objNewMember As gloStream.Immunization.Supporting.ItemDetail
                    objNewMember = New gloStream.Immunization.Supporting.ItemDetail

                    objNewMember.Description = oDescription
                    mCol.Add(objNewMember)
                    Add = objNewMember
                    objNewMember = Nothing
                End Function
                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.Immunization.Supporting.ItemDetail
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
