Imports System.Data.SqlClient
Imports System.Drawing

Public Class gloLabUC_LabFlowSheet

#Region "Variables"

    Private Const COL_TESTNAME = 3
    Dim _nPatientId As Int64 = 0
    Dim _sConnectionString As String = String.Empty

    Public Event gUC_LabFlowSheet_Print(ByVal OrderId As Int64)
    Public Event gUC_LabFlowSheet_TestResultPrint(ByVal dtLabData As DataTable, ByVal dtPatientTable As DataTable, ByVal intrptPages As Int32, ByVal intLastColPrinted As Int32, ByVal MyText As CrystalDecisions.CrystalReports.Engine.TextObject, ByVal cntCol As Int32)
    Public Event gUC_LabFlowSheet_FlowSheetPrint(ByVal patID As Int64, ByVal fromdate As String, ByVal toDate As String, ByVal orderIds As String, ByVal testids As String, ByVal resultnames As String, ByVal strTestResultNumber As String, ByVal blnRange As Integer)
    Dim ht As C1.Win.C1FlexGrid.HitTestInfo = Nothing 'added by manoj jadhav on 20121127
#End Region

#Region "User defined methods"

    Public Sub SetData(ByVal nPatientId As Long, ByVal sConnectionString As String, ByVal dFromDate As DateTime, ByVal dToDate As DateTime)
        Dim dtFlowSheet As DataTable = Nothing
        Try
            _sConnectionString = sConnectionString
            _nPatientId = nPatientId

            If nPatientId < 0 Then
                Return
            End If

            dtpFromDate.Value = dFromDate
            dtpToDate.Value = dToDate

            dtFlowSheet = GetLabData(nPatientId, dtpFromDate.Value, dtpToDate.Value)
            'If IsNothing(dtFlowSheet) Or dtFlowSheet.Rows.Count <= 0 Then
            '    Return
            'End If

            DesignGrid()
            DesignData(dtFlowSheet)

        Catch ex As Exception

        Finally
            If Not IsNothing(dtFlowSheet) Then
                dtFlowSheet.Dispose()
                dtFlowSheet = Nothing
            End If
        End Try

    End Sub


    'New version
    Dim _dtOrderIDDataTable As New DataTable()

    Public Function GetLabData(ByVal nPatientID As Int64, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As DataTable
        Dim _dtLabData As DataTable = Nothing
        Dim _dtFormattedDataTable As New DataTable()
        If (IsNothing(_dtOrderIDDataTable) = False) Then
            _dtOrderIDDataTable.Dispose()
        End If
        _dtOrderIDDataTable = New DataTable()
        '_dtFormattedDataTable = New DataTable()
        Dim _strQuery As String = String.Empty

        Try
            '' "	COALESCE( convert(varchar, Lab_Order_Test_ResultDtl.labotrd_TestSpecimenCollectionDateTime), convert (varchar, Lab_Order_Test_ResultDtl.labotrd_ResultDateTime)) AS ResultDate,  " & _
            _strQuery = " SELECT  " & _
                         "	Lab_Order_Test_Result.labotr_TestName As TestName,Lab_Order_Test_ResultDtl.labotrd_ResultLineNo as TestResultLineNumber, " & _
                         "	Lab_Order_Test_Result.labotr_TestResultNumber as TestResultNumber, " & _
                         "	Lab_Order_Test_Result.labotr_TestResultName AS ResultGroupName, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultValue AS ResultValue, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultRange AS ResultRange, " & _
                          "	Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag AS ResultAbnormalFlag, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultUnit AS ResultUnit, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultName As ResultName, " & _
                         "	COALESCE( convert(varchar, Lab_Order_Test_ResultDtl.labotrd_TestSpecimenCollectionDateTime), convert (varchar, Lab_Order_Test_Result.labotr_ResultTransferDateTime)) AS ResultDate,  " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_OrderID AS OrderID,  " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_TestID AS TestID , " & _
                         "   Lab_Order_Test_ResultDtl.labotrd_ResultComment As ResultComment, " & _
                         "  Lab_Order_TestDtl.labotd_DMSID As DMSID " & _
                         " FROM    Lab_Order_MST INNER JOIN Lab_Order_Test_Result  " & _
                         "			ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID  " & _
                         "		INNER JOIN " & _
                         "			Lab_Order_Test_ResultDtl  " & _
                         "				ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID  " & _
            "          INNER JOIN Lab_Order_TestDtl ON dbo.Lab_Order_MST.labom_OrderID	= Lab_Order_TestDtl.labotd_OrderID AND	Lab_Order_TestDtl.labotd_TestID	= Lab_Order_Test_ResultDtl.labotrd_TestID " & _
                         "				AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID  " & _
                         "				AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber " & _
                         " Where (Lab_Order_MST.labom_PatientID='" & nPatientID.ToString() & "')  AND    ISNULL(Lab_Order_MST.OrderStatusNumber,0)<> 1002  " & _
            "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!=''" & _
                         "		AND Convert(DateTime,Convert(varchar,COALESCE(Lab_Order_Test_ResultDtl.labotrd_TestSpecimenCollectionDateTime,Lab_Order_Test_Result.labotr_ResultTransferDateTime),101))>='" & dtpFromDate.Text & "'  " & _
                         "	  AND Convert(DateTime,Convert(varchar,COALESCE(Lab_Order_Test_ResultDtl.labotrd_TestSpecimenCollectionDateTime,Lab_Order_Test_Result.labotr_ResultTransferDateTime) ,101)) <= '" & dtpToDate.Text & "' " & _
                         "	 AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber =  " & _
                         "		(  " & _
                         "			SELECT MAX(Lab_Order_Test_ResultDtl1.labotrd_TestResultNumber) " & _
                         "			FROM  Lab_Order_Test_ResultDtl As Lab_Order_Test_ResultDtl1 " & _
                         "			WHERE Lab_Order_Test_ResultDtl1.labotrd_OrderID=Lab_Order_Test_ResultDtl.labotrd_OrderID " & _
                         "				AND Lab_Order_Test_ResultDtl1.labotrd_TestID =Lab_Order_Test_ResultDtl.labotrd_TestID  " & _
                         "		 )  order by COALESCE(Lab_Order_Test_ResultDtl.labotrd_TestSpecimenCollectionDateTime,Lab_Order_Test_Result.labotr_ResultTransferDateTime) desc ,Lab_Order_TestDtl.labotd_LineNO Asc"

            ''AND ISNULL(Lab_Order_MST.OrderStatusNumber,0)<> 1002  added condition against Incident 00034902 to escape cancelled orders 

            Dim oDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
            oDbLayer.Connect(False)
            oDbLayer.Retrive_Query(_strQuery, _dtLabData)
            oDbLayer.Disconnect()
            oDbLayer.Dispose()
            oDbLayer = Nothing

            _dtFormattedDataTable.Columns.Add("IsResult")
            _dtOrderIDDataTable.Columns.Add("IsResult")
            _dtFormattedDataTable.Columns.Add("OrderId")
            _dtOrderIDDataTable.Columns.Add("OrderId")
            _dtFormattedDataTable.Columns.Add("TestId")
            _dtOrderIDDataTable.Columns.Add("TestId")
            _dtFormattedDataTable.Columns.Add("TestName")
            _dtOrderIDDataTable.Columns.Add("TestName")

            'Sanjog 
            _dtFormattedDataTable.Columns.Add("TestResultNumber")
            _dtOrderIDDataTable.Columns.Add("TestResultNumber")

            _dtFormattedDataTable.Columns.Add("ResultComment")
            _dtFormattedDataTable.Columns.Add("DMSID")
            'sanjog

            'Code for adding the columns...
            Dim _strCntQuery As String = String.Empty
            Dim _Cnt As Integer = 0

            Dim dvForDate As DataView = _dtLabData.DefaultView
            For Each oDataRow As DataRow In _dtLabData.Rows


                Dim strfilter As String = "ResultDate='" & Convert.ToString(oDataRow("ResultDate")) & "' AND TestName='" & Convert.ToString(oDataRow("TestName")).Replace("'", "''") & "' AND ResultName='" & Convert.ToString(oDataRow("ResultName")).Replace("'", "''") & "' "
                dvForDate.RowFilter = strfilter

                If dvForDate.Count = 1 Then
                    If Not _dtFormattedDataTable.Columns.Contains(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime)) Then
                        _dtFormattedDataTable.Columns.Add(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime))
                        _dtOrderIDDataTable.Columns.Add(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime))
                    End If
                Else
                    'Means there are more results for same testname
                    Dim strOrderID As String = ""
                    Dim cntOrderID As Integer = 0
                    For Each dr1 As DataRowView In dvForDate
                        Dim strDateTime As String = dr1("OrderID").ToString()
                        If strOrderID <> strDateTime Then
                            cntOrderID = cntOrderID + 1
                            strOrderID = dr1("OrderID").ToString()
                        End If
                    Next
                    'If cntOrderID is greater than one means there is more order which having same resultdatetime with same test & result name 
                    For k As Integer = 0 To dvForDate.Count - 1
                        If k = 0 Then
                            If Not _dtFormattedDataTable.Columns.Contains(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime)) Then
                                _dtFormattedDataTable.Columns.Add(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime))
                                _dtOrderIDDataTable.Columns.Add(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime))
                            End If
                        Else
                            If Not _dtFormattedDataTable.Columns.Contains(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime) & "(" & k.ToString() & ")") Then
                                _dtFormattedDataTable.Columns.Add(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime) & "(" & k.ToString() & ")")
                                _dtOrderIDDataTable.Columns.Add(FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.ShortDate) & vbCrLf & FormatDateTime(oDataRow("ResultDate").ToString(), DateFormat.LongTime) & "(" & k.ToString() & ")")
                            End If
                        End If

                    Next

                End If
                '''''''''''''Sanjog
            Next

            'Code for adding the data in table
            Dim arrTests As New System.Collections.ArrayList()
            ' For Each oDataRow As DataRow In dvLabDataFororder.ToTable().Rows
            For Each oDataRow As DataRow In _dtLabData.Rows
                If Not arrTests.Contains(Convert.ToString(oDataRow("TestName"))) Then
                    arrTests.Add(Convert.ToString(oDataRow("TestName")))
                    Dim dvTestData As New DataView()
                    dvTestData.Table = _dtLabData
                    'dvTestData.RowFilter = "TestName='" & Convert.ToString(oDataRow("TestName")) & "' And OrderId = " & strOrderId
                    'Sanjog - Added ON 2011 MAY 19 to handle "'" in test name 
                    dvTestData.RowFilter = "TestName='" & Convert.ToString(oDataRow("TestName")).Replace("'", "''") & "'"
                    'Sanjog - Added ON 2011 MAY 19 to handle "'" in test name 
                    ' Adding test name in seperate row
                    Dim drFormatted As DataRow = _dtFormattedDataTable.NewRow()
                    Dim drOrderIdrow As DataRow = _dtOrderIDDataTable.NewRow()
                    drFormatted("IsResult") = "False"
                    drFormatted("TestName") = Convert.ToString(oDataRow("TestName"))
                    drFormatted("OrderId") = Convert.ToString(oDataRow("OrderId"))
                    drFormatted("TestId") = Convert.ToString(oDataRow("TestId"))
                    'sanjog
                    drFormatted("TestResultNumber") = Convert.ToString(oDataRow("TestResultNumber"))

                    drFormatted("DMSID") = Convert.ToString(oDataRow("DMSID"))
                    'sanjog
                    _dtFormattedDataTable.Rows.Add(drFormatted)
                    _dtOrderIDDataTable.Rows.Add(drOrderIdrow)

                    'Adding data in formatted datatable
                    Dim arrResults As New System.Collections.ArrayList()
                    Dim _dvTestResultData As New DataView()
                    For rCnt As Integer = 0 To dvTestData.Count - 1
                        If Not arrResults.Contains(Convert.ToString(dvTestData(rCnt)("ResultName"))) Then
                            arrResults.Add(Convert.ToString(dvTestData(rCnt)("ResultName")))
                            _dvTestResultData.Table = _dtLabData

                            _dvTestResultData.RowFilter = "TestName='" & Convert.ToString(oDataRow("TestName")).Replace("'", "''") & "' AND ResultName= '" & Convert.ToString(dvTestData(rCnt)("ResultName")).Replace("'", "''") & "'"

                            If _dvTestResultData.Count = 1 Then
                                drFormatted = _dtFormattedDataTable.NewRow()
                                drOrderIdrow = _dtOrderIDDataTable.NewRow()
                                drFormatted("TestName") = Convert.ToString(dvTestData(rCnt)("ResultName"))
                                drFormatted("IsResult") = "True"
                                drFormatted("OrderId") = Convert.ToString(dvTestData(rCnt)("OrderId"))

                                drFormatted("TestResultNumber") = Convert.ToString(dvTestData(rCnt)("TestResultNumber"))

                                drFormatted("TestId") = Convert.ToString(dvTestData(rCnt)("TestId"))

                                drFormatted("ResultComment") = Convert.ToString(dvTestData(rCnt)("ResultComment"))

                                ''start of code by manoj on 20121127 for making Result value as hyper link
                                'drFormatted(FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")), DateFormat.ShortDate) & vbCrLf & FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")), DateFormat.LongTime)) = Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & Convert.ToString(dvTestData(rCnt)("ResultUnit")) & "|" & Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) & "|" & Convert.ToString(dvTestData(rCnt)("ResultRange"))
                                'check is result value is hyperlink
                                If gloGlobal.gloLabGenral.IsResultisHyperLink(Convert.ToString(dvTestData(rCnt)("ResultValue"))) Then
                                    'do not append unit,NormalAbnoralflag and Ranage
                                    drFormatted(FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")), DateFormat.ShortDate) & vbCrLf & FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")), DateFormat.LongTime)) = Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag"))
                                Else
                                    'append  unit,NormalAbnoralflag and Ranage to result
                                    drFormatted(FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")), DateFormat.ShortDate) & vbCrLf & FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")), DateFormat.LongTime)) = Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & Convert.ToString(dvTestData(rCnt)("ResultUnit")) & "|" & Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) & "|" & Convert.ToString(dvTestData(rCnt)("ResultRange"))
                                End If
                                ''End of code by manoj on 20121127 for making Result value as hyper link

                                drOrderIdrow(FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")), DateFormat.ShortDate) & vbCrLf & FormatDateTime(Convert.ToDateTime(dvTestData(rCnt)("ResultDate")).ToString(), DateFormat.LongTime)) = Convert.ToString(dvTestData(rCnt)("OrderId"))
                                _dtFormattedDataTable.Rows.Add(drFormatted)
                                _dtOrderIDDataTable.Rows.Add(drOrderIdrow)

                            ElseIf _dvTestResultData.Count > 1 Then

                                Dim arrResultDates As New ArrayList()
                                drFormatted = _dtFormattedDataTable.NewRow()

                                drOrderIdrow = _dtOrderIDDataTable.NewRow()
                                For oRow As Integer = 0 To _dvTestResultData.Count - 1

                                    If Not arrResultDates.Contains(_dvTestResultData(oRow)("ResultDate")) Then
                                        arrResultDates.Add(_dvTestResultData(oRow)("ResultDate"))
                                        Dim dvResultDatesData As New DataView()
                                        dvResultDatesData.Table = _dvTestResultData.ToTable()
                                        dvResultDatesData.RowFilter = "ResultDate= '" & Convert.ToString(_dvTestResultData(oRow)("ResultDate")) & "'"
                                        For introwCnt As Integer = 0 To dvResultDatesData.Count - 1

                                            drFormatted("TestName") = Convert.ToString(dvResultDatesData(introwCnt)("ResultName"))
                                            drFormatted("TestId") = Convert.ToString(dvResultDatesData(introwCnt)("TestId"))
                                            drFormatted("OrderId") = Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))

                                            drFormatted("TestResultNumber") = Convert.ToString(dvResultDatesData(introwCnt)("TestResultNumber"))

                                            drFormatted("IsResult") = "True"

                                            If Convert.ToString(drFormatted("ResultComment")).Trim() <> "" Then
                                                drFormatted("ResultComment") = Convert.ToString(dvResultDatesData(introwCnt)("ResultComment"))
                                            End If

                                            If introwCnt = 0 Then
                                                'drFormatted(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime))) = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))
                                                If gloGlobal.gloLabGenral.IsResultisHyperLink(Convert.ToString(dvResultDatesData(introwCnt)("ResultValue"))) Then
                                                    drFormatted(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime))) = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag"))
                                                Else
                                                    drFormatted(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime))) = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))
                                                End If

                                                drOrderIdrow(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime))) = Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))

                                            Else
                                                'drFormatted(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime)) & "(" & introwCnt.ToString() & ")") = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))
                                                If gloGlobal.gloLabGenral.IsResultisHyperLink(Convert.ToString(dvResultDatesData(introwCnt)("ResultValue"))) Then
                                                    drFormatted(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime)) & "(" & introwCnt.ToString() & ")") = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag"))
                                                Else
                                                    drFormatted(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime)) & "(" & introwCnt.ToString() & ")") = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & "|" & Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))
                                                End If


                                                drOrderIdrow(Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.ShortDate)) & vbCrLf & Convert.ToString(FormatDateTime(dvResultDatesData(introwCnt)("ResultDate").ToString(), DateFormat.LongTime)) & "(" & introwCnt.ToString() & ")") = Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))
                                            End If

                                        Next
                                        dvResultDatesData.Dispose()
                                        dvResultDatesData = Nothing
                                    End If
                                Next
                                _dtFormattedDataTable.Rows.Add(drFormatted)
                                _dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                            End If
                        End If
                        
                    Next
                    'Added code for Problem 00000929: Flowsheet not work as expected
                    _dvTestResultData.Dispose()
                    _dvTestResultData = Nothing
                    dvTestData.Dispose()
                    dvTestData = Nothing
                End If
            Next
            _dtLabData.Dispose()
            _dtLabData = Nothing
            dvForDate.Dispose()
            dvForDate = Nothing
        Catch ex As Exception

        End Try
        _dtFormattedDataTable.Columns(_dtFormattedDataTable.Columns.IndexOf("TestName")).ColumnName = "Test Name"
        Return _dtFormattedDataTable
    End Function


    Public Function GetLabPrintData(ByVal nPatientID As Int64, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime) As DataTable
        Dim _dtLabData As DataTable = Nothing
        Dim _dtFormattedDataTable As New DataTable()
        If (IsNothing(_dtOrderIDDataTable) = False) Then
            _dtOrderIDDataTable.Dispose()
        End If
        _dtOrderIDDataTable = New DataTable()
        '_dtFormattedDataTable = New DataTable()
        Dim _strQuery As String = String.Empty

        Try

            _strQuery = " SELECT  " & _
                         "	Lab_Order_Test_Result.labotr_TestName As TestName,Lab_Order_Test_ResultDtl.labotrd_ResultLineNo as TestResultLineNumber, " & _
                         "	Lab_Order_Test_Result.labotr_TestResultNumber as TestResultNumber, " & _
                         "	Lab_Order_Test_Result.labotr_TestResultName AS ResultGroupName, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultValue AS ResultValue, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultRange AS ResultRange, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag AS ResultAbnormalFlag, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultUnit AS ResultUnit, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_ResultName As ResultName, " & _
                         "	Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101) AS ResultDate, " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_OrderID AS OrderID,  " & _
                         "	Lab_Order_Test_ResultDtl.labotrd_TestID AS TestID  " & _
                         " FROM    Lab_Order_MST INNER JOIN Lab_Order_Test_Result  " & _
                         "			ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID  " & _
                         "		INNER JOIN " & _
                         "			Lab_Order_Test_ResultDtl  " & _
                         "				ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID  " & _
                         "				AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID  " & _
                         "				AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber " & _
                         " Where (Lab_Order_Test_ResultDtl.labotrd_ResultType = 'F') AND (Lab_Order_MST.labom_PatientID='" & nPatientID.ToString() & "')  " & _
            "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!=''" & _
                         "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL  AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!=''" & _
                         "		AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101))>='" & dtpFromDate.Text & "'  " & _
                         "	  AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime ,101)) <= '" & dtpToDate.Text & "' " & _
                         "	 AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber =  " & _
                         "		(  " & _
                         "			SELECT MAX(Lab_Order_Test_ResultDtl1.labotrd_TestResultNumber) " & _
                         "			FROM  Lab_Order_Test_ResultDtl As Lab_Order_Test_ResultDtl1 " & _
                         "			WHERE Lab_Order_Test_ResultDtl1.labotrd_OrderID=Lab_Order_Test_ResultDtl.labotrd_OrderID " & _
                         "				AND Lab_Order_Test_ResultDtl1.labotrd_TestID =Lab_Order_Test_ResultDtl.labotrd_TestID  " & _
                         "		 )  order by Lab_Order_Test_ResultDtl.labotrd_ResultDateTime desc"


            Dim oDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
            oDbLayer.Connect(False)
            oDbLayer.Retrive_Query(_strQuery, _dtLabData)
            oDbLayer.Disconnect()
            oDbLayer.Dispose()
            oDbLayer = Nothing
            _dtFormattedDataTable.Columns.Add("IsResult")
            _dtOrderIDDataTable.Columns.Add("IsResult")
            _dtFormattedDataTable.Columns.Add("OrderId")
            _dtOrderIDDataTable.Columns.Add("OrderId")
            _dtFormattedDataTable.Columns.Add("TestId")
            _dtOrderIDDataTable.Columns.Add("TestId")
            _dtFormattedDataTable.Columns.Add("TestName")
            _dtOrderIDDataTable.Columns.Add("TestName")

            'Code for adding the columns...
            Dim _strCntQuery As String = String.Empty
            Dim _Cnt As Integer = 0
            For Each oDataRow As DataRow In _dtLabData.Rows


                _strCntQuery = "With LabData As ( " & _
                                " SELECT " & _
                                "	Lab_Order_Test_Result.labotr_TestName As TestName,Lab_Order_Test_ResultDtl.labotrd_TestResultNumber," & _
                                "	Lab_Order_Test_Result.labotr_TestResultNumber as TestResultNumber," & _
                                "	Lab_Order_Test_Result.labotr_TestResultName AS ResultGroupName," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultValue AS ResultValue," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultRange AS ResultRange," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag AS ResultAbnormalFlag," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultUnit AS ResultUnit," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_ResultName As ResultName," & _
                                "	Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101) AS ResultDate," & _
                                "	Lab_Order_Test_ResultDtl.labotrd_OrderID AS OrderID, " & _
                                "	Lab_Order_Test_ResultDtl.labotrd_TestID AS TestID " & _
                                "FROM    Lab_Order_MST INNER JOIN Lab_Order_Test_Result " & _
                                "			ON Lab_Order_MST.labom_OrderID = Lab_Order_Test_Result.labotr_OrderID " & _
                                "		INNER JOIN " & _
                                "			Lab_Order_Test_ResultDtl " & _
                                "				ON Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID " & _
                                "				AND Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " & _
                                "				AND Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber " & _
                                "Where (Lab_Order_Test_ResultDtl.labotrd_ResultType = 'F') AND (Lab_Order_MST.labom_PatientID='" & nPatientID.ToString() & "') " & _
                "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!=''" & _
                                "  AND Lab_Order_Test_ResultDtl.labotrd_ResultValue IS NOT NULL  AND Ltrim(rtrim(Lab_Order_Test_ResultDtl.labotrd_ResultValue ))!='' " & _
                                "		AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime,101))>='" & dtpFromDate.Text & "' " & _
                                  " AND Convert(DateTime,Convert(varchar,Lab_Order_Test_ResultDtl.labotrd_ResultDateTime ,101)) <= '" & dtpToDate.Text & "' " & _
                                 " AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = " & _
                                 "	( " & _
                                 "		SELECT MAX(Lab_Order_Test_ResultDtl1.labotrd_TestResultNumber)" & _
                                 "		FROM  Lab_Order_Test_ResultDtl As Lab_Order_Test_ResultDtl1 " & _
                                 "		WHERE Lab_Order_Test_ResultDtl1.labotrd_OrderID=Lab_Order_Test_ResultDtl.labotrd_OrderID " & _
                                 "			AND Lab_Order_Test_ResultDtl1.labotrd_TestID =Lab_Order_Test_ResultDtl.labotrd_TestID " & _
                                 "	))select count(ResultName) from LabData where ResultDate='" & Convert.ToString(oDataRow("ResultDate")) & "' AND TestName='" & Convert.ToString(oDataRow("TestName")) & "' group  by ResultName having count(ResultName) >= All(select count(ResultName) from LabData where ResultDate='" & Convert.ToString(oDataRow("ResultDate")) & "' AND TestName='" & Convert.ToString(oDataRow("TestName")) & "' group  by ResultName) "

                _Cnt = Convert.ToInt32(oDbLayer.ExecuteScalar_Query(_strCntQuery))
                For i As Integer = 0 To _Cnt - 1
                    If i = 0 Then
                        If Not _dtFormattedDataTable.Columns.Contains(oDataRow("ResultDate").ToString()) Then
                            _dtFormattedDataTable.Columns.Add(oDataRow("ResultDate").ToString())
                            _dtOrderIDDataTable.Columns.Add(oDataRow("ResultDate").ToString())
                        End If
                    Else
                        If Not _dtFormattedDataTable.Columns.Contains(oDataRow("ResultDate").ToString() & "(" & i.ToString() & ")") Then
                            _dtFormattedDataTable.Columns.Add(oDataRow("ResultDate").ToString() & "(" & i.ToString() & ")")
                            _dtOrderIDDataTable.Columns.Add(oDataRow("ResultDate").ToString() & "(" & i.ToString() & ")")
                        End If
                    End If
                Next
            Next

            ''*** I am writing from scratch
            'Code for adding the data in table
            Dim arrTests As New System.Collections.ArrayList()
            ' For Each oDataRow As DataRow In dvLabDataFororder.ToTable().Rows
            For Each oDataRow As DataRow In _dtLabData.Rows
                If Not arrTests.Contains(Convert.ToString(oDataRow("TestName"))) Then
                    arrTests.Add(Convert.ToString(oDataRow("TestName")))
                    Dim dvTestData As New DataView()
                    dvTestData.Table = _dtLabData
                    'dvTestData.RowFilter = "TestName='" & Convert.ToString(oDataRow("TestName")) & "' And OrderId = " & strOrderId
                    dvTestData.RowFilter = "TestName='" & Convert.ToString(oDataRow("TestName")) & "'"
                    ' Adding test name in seperate row
                    Dim drFormatted As DataRow = _dtFormattedDataTable.NewRow()
                    Dim drOrderIdrow As DataRow = _dtOrderIDDataTable.NewRow()
                    drFormatted("IsResult") = "False"
                    drFormatted("TestName") = Convert.ToString(oDataRow("TestName"))
                    drFormatted("OrderId") = Convert.ToString(oDataRow("OrderId"))
                    drFormatted("TestId") = Convert.ToString(oDataRow("TestId"))

                    _dtFormattedDataTable.Rows.Add(drFormatted)
                    _dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                    Dim valToPrint As String
                    'Adding data in formatted datatable
                    Dim arrResults As New System.Collections.ArrayList()
                    Dim _dvTestResultData As New DataView()
                    For rCnt As Integer = 0 To dvTestData.Count - 1
                        If Not arrResults.Contains(Convert.ToString(dvTestData(rCnt)("ResultName"))) Then
                            arrResults.Add(Convert.ToString(dvTestData(rCnt)("ResultName")))
                            _dvTestResultData.Table = _dtLabData
                            '_dvTestResultData.RowFilter = "TestName='" & Convert.ToString(oDataRow("TestName")) & "' AND ResultName= '" & Convert.ToString(dvTestData(rCnt)("ResultName")) & "' AND OrderID=" & strOrderId
                            _dvTestResultData.RowFilter = "TestName='" & Convert.ToString(oDataRow("TestName")) & "' AND ResultName= '" & Convert.ToString(dvTestData(rCnt)("ResultName")) & "'"
                            If _dvTestResultData.Count = 1 Then
                                drFormatted = _dtFormattedDataTable.NewRow()
                                drOrderIdrow = _dtOrderIDDataTable.NewRow()
                                drFormatted("TestName") = Convert.ToString(dvTestData(rCnt)("ResultName"))
                                'drOrderIdrow("TestName") = Convert.ToString(dvTestData(rCnt)("ResultName"))
                                drFormatted("IsResult") = "True"
                                drFormatted("OrderId") = Convert.ToString(dvTestData(rCnt)("OrderId"))

                                drFormatted("TestId") = Convert.ToString(dvTestData(rCnt)("TestId"))
                                valToPrint = String.Empty
                                valToPrint = Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & Convert.ToString(dvTestData(rCnt)("ResultUnit"))
                                If Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) <> "" AndAlso Not IsNothing(Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag"))) Then
                                    valToPrint &= "(" & Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) & ")"
                                End If
                                If Convert.ToString(dvTestData(rCnt)("ResultRange")) <> "" AndAlso Not IsNothing(Convert.ToString(dvTestData(rCnt)("ResultRange"))) Then
                                    valToPrint &= " [" & Convert.ToString(dvTestData(rCnt)("ResultRange")) & "]"
                                End If
                                'drFormatted(Convert.ToString(dvTestData(rCnt)("ResultDate"))) = Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & Convert.ToString(dvTestData(rCnt)("ResultUnit")) & "(" & Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) & ") [" & Convert.ToString(dvTestData(rCnt)("ResultRange")) & "]"
                                drFormatted(Convert.ToString(dvTestData(rCnt)("ResultDate"))) = valToPrint
                                drOrderIdrow(Convert.ToString(dvTestData(rCnt)("ResultDate"))) = Convert.ToString(dvTestData(rCnt)("OrderId"))
                                _dtFormattedDataTable.Rows.Add(drFormatted)
                                _dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                                '_dtOrderIDDataTable.Rows(_dtOrderIDDataTable.Rows.Count - 1)(Convert.ToString(dvTestData(rCnt)("ResultDate"))) = Convert.ToString(dvTestData(rCnt)("OrderId"))

                            ElseIf _dvTestResultData.Count > 1 Then

                                '' From New View
                                'Dim fiRst As Int16 = 0
                                'For _dvRowIndex As Int16 = 0 To _dvTestResultData.Count - 1

                                '    If fiRst = 0 Then
                                '        fiRst = 1
                                '        drFormatted = _dtFormattedDataTable.NewRow()
                                '        drFormatted("TestName") = Convert.ToString(dvTestData(rCnt)("ResultName"))
                                '        drFormatted("IsResult") = "True"
                                '        drFormatted("OrderId") = Convert.ToString(dvTestData(rCnt)("OrderId"))
                                '        drFormatted("TestId") = Convert.ToString(dvTestData(rCnt)("TestId"))
                                '        drFormatted(Convert.ToString(dvTestData(rCnt)("ResultDate"))) = Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & Convert.ToString(dvTestData(rCnt)("ResultUnit")) & "|" & Convert.ToString(dvTestData(rCnt)("ResultAbnormalFlag")) & "|" & Convert.ToString(dvTestData(rCnt)("ResultRange"))
                                '        _dtFormattedDataTable.Rows.Add(drFormatted)
                                '    Else
                                '        If _dvTestResultData(_dvRowIndex - 1)("ResultDate") <> _dvTestResultData(_dvRowIndex)("ResultDate") Then
                                '            _dtFormattedDataTable.Rows(_dtFormattedDataTable.Rows.Count - 1)(Convert.ToString(oDataRow("ResultDate"))) = Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultValue")) & " " & Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultAbnormalFlag"))
                                '            fiRst = 1
                                '        Else

                                '            _dtFormattedDataTable.Rows(_dtFormattedDataTable.Rows.Count - 1)(Convert.ToString(oDataRow("ResultDate")) & "(" & fiRst.ToString() & ")") = Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultValue")) & " " & Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(_dvRowIndex)("ResultAbnormalFlag"))
                                '            fiRst = fiRst + 1
                                '        End If

                                '        'fiRst = fiRst + 1
                                '    End If

                                'Next


                                ''** From New View

                                Dim arrResultDates As New ArrayList()
                                drFormatted = _dtFormattedDataTable.NewRow()

                                drOrderIdrow = _dtOrderIDDataTable.NewRow()
                                For oRow As Integer = 0 To _dvTestResultData.Count - 1
                                    'Dim _Col = 0
                                    'If oRow = 0 Then
                                    '    drFormatted = _dtFormattedDataTable.NewRow()
                                    '    'drFormatted("TestName") = Convert.ToString(dvTestData(rCnt)("ResultName"))
                                    '    'drFormatted("TestId") = Convert.ToString(dvTestData(rCnt)("TestId"))
                                    '    'drFormatted("OrderId") = Convert.ToString(dvTestData(rCnt)("OrderId"))
                                    '    drFormatted("TestName") = Convert.ToString(_dvTestResultData(oRow)("ResultName"))
                                    '    drFormatted("TestId") = Convert.ToString(_dvTestResultData(oRow)("TestId"))
                                    '    drFormatted("OrderId") = Convert.ToString(_dvTestResultData(oRow)("OrderId"))
                                    '    drFormatted("IsResult") = "True"
                                    '    'drFormatted(Convert.ToString(oDataRow("ResultDate"))) = Convert.ToString(dvTestData(rCnt)("ResultValue")) & " " & Convert.ToString(dvTestData(rCnt)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultRange"))
                                    '    'drFormatted(Convert.ToString(oDataRow("ResultDate"))) = Convert.ToString(_dvTestResultData(oRow)("ResultValue")) & " " & Convert.ToString(_dvTestResultData(oRow)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultRange"))
                                    '    drFormatted(Convert.ToString(_dvTestResultData(oRow)("ResultDate"))) = Convert.ToString(_dvTestResultData(oRow)("ResultValue")) & " " & Convert.ToString(_dvTestResultData(oRow)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultRange"))
                                    '    _dtFormattedDataTable.Rows.Add(drFormatted)
                                    'ElseIf oRow > 0 Then
                                    '    If Convert.ToDateTime(_dvTestResultData(oRow - 1)("ResultDate")) = Convert.ToDateTime(_dvTestResultData(oRow)("ResultDate")) Then
                                    '        _Col = _Col + 1
                                    '        _dtFormattedDataTable.Rows(_dtFormattedDataTable.Rows.Count - 1)(Convert.ToString(oDataRow("ResultDate")) & "(" & _Col.ToString() & ")") = Convert.ToString(_dvTestResultData(oRow)("ResultValue")) & " " & Convert.ToString(_dvTestResultData(oRow)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultAbnormalFlag"))
                                    '    Else
                                    '        _Col = 0
                                    '        ' _dtFormattedDataTable.Rows(_dtFormattedDataTable.Rows.Count - 1)(Convert.ToString(dvTestData(oRow)("ResultDate"))) = Convert.ToString(_dvTestResultData(oRow)("ResultValue")) & " " & Convert.ToString(_dvTestResultData(oRow)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultRange"))
                                    '        _dtFormattedDataTable.Rows(_dtFormattedDataTable.Rows.Count - 1)(Convert.ToString(_dvTestResultData(oRow)("ResultDate"))) = Convert.ToString(_dvTestResultData(oRow)("ResultValue")) & " " & Convert.ToString(_dvTestResultData(oRow)("ResultUnit")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultAbnormalFlag")) & "|" & Convert.ToString(_dvTestResultData(oRow)("ResultRange"))
                                    '    End If

                                    'End If

                                    'New one 

                                    If Not arrResultDates.Contains(_dvTestResultData(oRow)("ResultDate")) Then
                                        arrResultDates.Add(_dvTestResultData(oRow)("ResultDate"))
                                        Dim dvResultDatesData As New DataView()
                                        dvResultDatesData.Table = _dvTestResultData.ToTable()
                                        dvResultDatesData.RowFilter = "ResultDate= '" & Convert.ToString(_dvTestResultData(oRow)("ResultDate")) & "'"
                                        For introwCnt As Integer = 0 To dvResultDatesData.Count - 1

                                            drFormatted("TestName") = Convert.ToString(dvResultDatesData(introwCnt)("ResultName"))
                                            drFormatted("TestId") = Convert.ToString(dvResultDatesData(introwCnt)("TestId"))
                                            drFormatted("OrderId") = Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))
                                            drFormatted("IsResult") = "True"

                                            valToPrint = String.Empty
                                            If introwCnt = 0 Then
                                                valtoPrint = String.Empty
                                                valtoPrint = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit"))
                                                If Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) <> "" AndAlso Not IsNothing(Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag"))) Then
                                                    valToPrint &= "(" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ")"
                                                End If
                                                If Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) <> "" AndAlso Not IsNothing(Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))) Then
                                                    valToPrint &= " [" & Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) & "]"
                                                End If
                                                'drFormatted(Convert.ToString(dvResultDatesData(introwCnt)("ResultDate"))) = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "(" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ") [" & Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) & " ]"
                                                drFormatted(Convert.ToString(dvResultDatesData(introwCnt)("ResultDate"))) = valtoPrint
                                                drOrderIdrow(Convert.ToString(dvResultDatesData(introwCnt)("ResultDate"))) = Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))
                                            Else
                                                valtoPrint = String.Empty
                                                valtoPrint = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit"))
                                                If Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) <> "" AndAlso Not IsNothing(Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag"))) Then
                                                    valToPrint &= "(" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ")"
                                                End If
                                                If Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) <> "" AndAlso Not IsNothing(Convert.ToString(dvResultDatesData(introwCnt)("ResultRange"))) Then
                                                    valToPrint &= " [" & Convert.ToString(dvResultDatesData(introwCnt)("ResultRange")) & "]"
                                                End If
                                                'drFormatted(Convert.ToString(dvResultDatesData(introwCnt)("ResultDate")) & "(" & introwCnt.ToString() & ")") = Convert.ToString(dvResultDatesData(introwCnt)("ResultValue")) & " " & Convert.ToString(dvResultDatesData(introwCnt)("ResultUnit")) & "(" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & ") [" & Convert.ToString(dvResultDatesData(introwCnt)("ResultAbnormalFlag")) & "]"
                                                drFormatted(Convert.ToString(dvResultDatesData(introwCnt)("ResultDate")) & "(" & introwCnt.ToString() & ")") = valtoPrint
                                                drOrderIdrow(Convert.ToString(dvResultDatesData(introwCnt)("ResultDate")) & "(" & introwCnt.ToString() & ")") = Convert.ToString(dvResultDatesData(introwCnt)("OrderId"))
                                            End If

                                            '                            Next


                                            '                        End If
                                            '                        '**New one 

                                            '                    Next
                                            '                    _dtFormattedDataTable.Rows.Add(drFormatted)
                                            '                    _dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                                            '                End If
                                            '            End If
                                            '        Next
                                            '    End If
                                            'Next
                                        Next
                                        dvResultDatesData.Dispose()
                                        dvResultDatesData = Nothing
                                    End If
                                Next
                                _dtFormattedDataTable.Rows.Add(drFormatted)
                                _dtOrderIDDataTable.Rows.Add(drOrderIdrow)
                            End If
                        End If
                        
                    Next
                    'Added code for Problem 00000929: Flowsheet not work as expected
                    _dvTestResultData.Dispose()
                    _dvTestResultData = Nothing
                    dvTestData.Dispose()
                    dvTestData = Nothing
                End If
            Next
            _dtLabData.Dispose()
            _dtLabData = Nothing
            

        Catch ex As Exception

        End Try
        _dtFormattedDataTable.Columns(_dtFormattedDataTable.Columns.IndexOf("TestName")).ColumnName = "Test Name"
        Return _dtFormattedDataTable
    End Function

#End Region

#Region "Design & Styles"

    Private Sub DesignGrid()

        c1LabFlowSheet.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        'c1LabFlowSheet.AllowEditing = False
        'sanjog
        c1LabFlowSheet.Cols(0).AllowEditing = True
        c1LabFlowSheet.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
        'sanjog
        SetGridStyle(c1LabFlowSheet)

        c1LabFlowSheet.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw

    End Sub
    Private Shared underLineFont As Font = Nothing
    Private Sub SetGridStyle(ByVal oFlex As C1.Win.C1FlexGrid.C1FlexGrid)
        With oFlex
            .ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
            .Cols.Count = 0
            .Cols.Fixed = 0
            .Rows.Count = 1
            .Rows.Fixed = 1

            .Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            .SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            .BackColor = System.Drawing.Color.FromArgb(240, 247, 255)


            .Styles.Fixed.BackColor = Color.FromArgb(81, 126, 211)
            .Styles.Fixed.ForeColor = Color.White
            .Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)

            .Styles.Alternate.BackColor = Color.FromArgb(222, 231, 250) '' Color.LightBlue
            .Styles.Alternate.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Alternate.Border.Color = Color.FromArgb(159, 181, 221)

            .Styles.Normal.BackColor = Color.FromArgb(240, 247, 255)
            .Styles.Normal.ForeColor = Color.FromArgb(31, 73, 125)
            .Styles.Normal.Border.Color = Color.FromArgb(159, 181, 221)

            .Styles.Highlight.BackColor = Color.FromArgb(254, 207, 102)
            .Styles.Highlight.ForeColor = Color.Black

            .Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)
            .Styles.Focus.ForeColor = Color.Black

            .Styles.EmptyArea.BackColor = Color.White


            Dim csHeader As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_Header")
            Try
                If (.Styles.Contains("CS_Header")) Then
                    csHeader = .Styles("CS_Header")
                Else
                    csHeader = .Styles.Add("CS_Header")
                    With csHeader
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                        .ForeColor = Color.White
                        .BackColor = System.Drawing.Color.FromArgb(86, 126, 211)  ''change color to c1flexgrid header color for bugid  #70147:
                        .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                    End With
                End If
            Catch ex As Exception
                csHeader = .Styles.Add("CS_Header")
                With csHeader
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                    .ForeColor = Color.White
                    .BackColor = System.Drawing.Color.FromArgb(86, 126, 211)  ''change color to c1flexgrid header color for bugid  #70147:
                    .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = Type.GetType("System.String")
                End With
            End Try
           

            Dim csRecord As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_Record")
            Try
                If (.Styles.Contains("CS_Record")) Then
                    csRecord = .Styles("CS_Record")
                Else
                    csRecord = .Styles.Add("CS_Record")
                    With csRecord
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackColor = Color.FromArgb(222, 231, 250)
                        '.DisplayEnum.Stack()
                        .DataType = Type.GetType("System.String")
                    End With
                End If
            Catch ex As Exception
                csRecord = .Styles.Add("CS_Record")
                With csRecord
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Bold)
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackColor = Color.FromArgb(222, 231, 250)
                    '.DisplayEnum.Stack()
                    .DataType = Type.GetType("System.String")
                End With
            End Try
           

            Dim csComboList As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_ComboList")
            Try
                If (.Styles.Contains("CS_ComboList")) Then
                    csComboList = .Styles("CS_ComboList")
                Else
                    csComboList = .Styles.Add("CS_ComboList")
                    With csComboList
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackColor = Color.FromArgb(222, 231, 250)
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.String")
                        .ComboList = "..."
                    End With
                End If
            Catch ex As Exception
                csComboList = .Styles.Add("CS_ComboList")
                With csComboList
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackColor = Color.FromArgb(222, 231, 250)
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = Type.GetType("System.String")
                    .ComboList = "..."
                End With
            End Try

           

            Dim csCheckBox As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_CheckBox")
            Try
                If (.Styles.Contains("CS_CheckBox")) Then
                    csCheckBox = .Styles("CS_CheckBox")
                Else
                    csCheckBox = .Styles.Add("CS_CheckBox")
                    With csCheckBox
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackColor = Color.FromArgb(240, 247, 255)
                        .ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                        .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        .DataType = Type.GetType("System.Boolean")
                    End With
                End If
            Catch ex As Exception
                csCheckBox = .Styles.Add("CS_CheckBox")
                With csCheckBox
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackColor = Color.FromArgb(240, 247, 255)
                    .ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.CenterCenter
                    .TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    .DataType = Type.GetType("System.Boolean")
                End With
            End Try
           

            Dim csNotNormal As C1.Win.C1FlexGrid.CellStyle '= .Styles.Add("CS_NotNormal")
            Try
                If (.Styles.Contains("CS_NotNormal")) Then
                    csNotNormal = .Styles("CS_NotNormal")
                Else
                    csNotNormal = .Styles.Add("CS_NotNormal")
                      With csNotNormal
                        .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                        .ForeColor = Color.FromArgb(31, 73, 125)
                        .BackColor = Color.FromArgb(240, 247, 255)
                        '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                        '.DataType = Type.GetType("System.Boolean")
                    End With
                End If
            Catch ex As Exception
                csNotNormal = .Styles.Add("CS_NotNormal")
                  With csNotNormal
                    .Font = gloGlobal.clsgloFont.getFontFromExistingSource(Font, FontStyle.Regular)
                    .ForeColor = Color.FromArgb(31, 73, 125)
                    .BackColor = Color.FromArgb(240, 247, 255)
                    '.Display = C1.Win.C1FlexGrid.DisplayEnum.Stack
                    '.DataType = Type.GetType("System.Boolean")
                End With
            End Try
         

            Dim style As C1.Win.C1FlexGrid.CellStyle

            '  style = c1LabFlowSheet.Styles.Add("Node")
            Try
                If (c1LabFlowSheet.Styles.Contains("Node")) Then
                    style = c1LabFlowSheet.Styles("Node")
                Else
                    style = c1LabFlowSheet.Styles.Add("Node")
                    style.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    style.BackColor = System.Drawing.Color.FromArgb(131, 167, 200)
                    style.ForeColor = Color.White
                    style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215)
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                    style.Border.Width = 1
                End If
            Catch ex As Exception
                style = c1LabFlowSheet.Styles.Add("Node")
                style.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                style.BackColor = System.Drawing.Color.FromArgb(131, 167, 200)
                style.ForeColor = Color.White
                style.Border.Color = System.Drawing.Color.FromArgb(131, 167, 215)
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                style.Border.Width = 1
            End Try
          

            ' style = c1LabFlowSheet.Styles.Add("Default")
            Try
                If (c1LabFlowSheet.Styles.Contains("Default")) Then
                    style = c1LabFlowSheet.Styles("Default")
                Else
                    style = c1LabFlowSheet.Styles.Add("Default")
                    style.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    style.BackColor = System.Drawing.Color.FromArgb(240, 247, 255) ''(255, 255, 255)
                    style.ForeColor = Color.White
                    style.Border.Color = System.Drawing.Color.FromArgb(240, 247, 255) ''(255, 255, 255)
                    style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                    style.Border.Width = 1
                End If
            Catch ex As Exception
                style = c1LabFlowSheet.Styles.Add("Default")
                style.Font = gloGlobal.clsgloFont.gFont 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                style.BackColor = System.Drawing.Color.FromArgb(240, 247, 255) ''(255, 255, 255)
                style.ForeColor = Color.White
                style.Border.Color = System.Drawing.Color.FromArgb(240, 247, 255) ''(255, 255, 255)
                style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                style.Border.Width = 1
            End Try
           

            '  style = c1LabFlowSheet.Styles.Add("AbnormalRange")
            Try
                If (c1LabFlowSheet.Styles.Contains("AbnormalRange")) Then
                    style = c1LabFlowSheet.Styles("AbnormalRange")
                Else
                    style = c1LabFlowSheet.Styles.Add("AbnormalRange")
                    style.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
                    style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                    style.BackColor = Color.Tomato
                    style.ForeColor = Color.White
                    'style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255)
                    'style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                    style.Border.Width = 1
                End If
            Catch ex As Exception
                style = c1LabFlowSheet.Styles.Add("AbnormalRange")
                style.Font = gloGlobal.clsgloFont.gFont_BOLD 'New System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte(0))
                style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                style.BackColor = Color.Tomato
                style.ForeColor = Color.White
                'style.Border.Color = System.Drawing.Color.FromArgb(255, 255, 255)
                'style.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                style.Border.Width = 1
            End Try
        


        End With

        ' create custom style for links
        Dim csNewLink As C1.Win.C1FlexGrid.CellStyle '= c1LabFlowSheet.Styles.Add("NewLink")
        Try
            If (c1LabFlowSheet.Styles.Contains("NewLink")) Then
                csNewLink = c1LabFlowSheet.Styles("NewLink")
            Else
                csNewLink = c1LabFlowSheet.Styles.Add("NewLink")
                If IsNothing(underLineFont) Then
                    underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
                End If
                csNewLink.Font = underLineFont
                csNewLink.ForeColor = Color.Blue
            End If
        Catch ex As Exception
            csNewLink = c1LabFlowSheet.Styles.Add("NewLink")
            If IsNothing(underLineFont) Then
                underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
            End If
            csNewLink.Font = underLineFont
            csNewLink.ForeColor = Color.Blue
        End Try
       


        Dim aNewLink As C1.Win.C1FlexGrid.CellStyle '= c1LabFlowSheet.Styles.Add("AbNewLink")
        Try
            If (c1LabFlowSheet.Styles.Contains("AbNewLink")) Then
                aNewLink = c1LabFlowSheet.Styles("AbNewLink")
            Else
                aNewLink = c1LabFlowSheet.Styles.Add("AbNewLink")
                If IsNothing(underLineFont) Then
                    underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
                End If
                aNewLink.Font = underLineFont
                aNewLink.ForeColor = Color.Blue
                aNewLink.BackColor = Color.Tomato
            End If
        Catch ex As Exception
            aNewLink = c1LabFlowSheet.Styles.Add("AbNewLink")
            If IsNothing(underLineFont) Then
                underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
            End If
            aNewLink.Font = underLineFont
            aNewLink.ForeColor = Color.Blue
            aNewLink.BackColor = Color.Tomato
        End Try

       


        Dim csOldLink As C1.Win.C1FlexGrid.CellStyle '= c1LabFlowSheet.Styles.Add("OldLink")
        Try
            If (c1LabFlowSheet.Styles.Contains("OldLink")) Then
                csOldLink = c1LabFlowSheet.Styles("OldLink")
            Else
                csOldLink = c1LabFlowSheet.Styles.Add("OldLink")
                If IsNothing(underLineFont) Then
                    underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
                End If
                csOldLink.Font = underLineFont
                csOldLink.ForeColor = Color.Purple
            End If
        Catch ex As Exception
            csOldLink = c1LabFlowSheet.Styles.Add("OldLink")
            If IsNothing(underLineFont) Then
                underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
            End If
            csOldLink.Font = underLineFont
            csOldLink.ForeColor = Color.Purple
        End Try
       


        Dim acsOldLink As C1.Win.C1FlexGrid.CellStyle '= c1LabFlowSheet.Styles.Add("AbOldLink")
        Try
            If (c1LabFlowSheet.Styles.Contains("AbOldLink")) Then
                acsOldLink = c1LabFlowSheet.Styles("AbOldLink")
            Else
                acsOldLink = c1LabFlowSheet.Styles.Add("AbOldLink")
                If IsNothing(underLineFont) Then
                    underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
                End If
                acsOldLink.Font = underLineFont
                acsOldLink.ForeColor = Color.Purple
                acsOldLink.BackColor = Color.Tomato
            End If
        Catch ex As Exception
            acsOldLink = c1LabFlowSheet.Styles.Add("AbOldLink")
            If IsNothing(underLineFont) Then
                underLineFont = New Font(c1LabFlowSheet.Font, FontStyle.Underline)
            End If
            acsOldLink.Font = underLineFont
            acsOldLink.ForeColor = Color.Purple
            acsOldLink.BackColor = Color.Tomato
        End Try
    
        If Not IsNothing(underLineFont) Then
            underLineFont.Dispose()
            underLineFont = Nothing
        End If
        '.BackColor = Color.GhostWhite


    End Sub

    Private Sub DesignData(ByVal dtLabData As DataTable)

        Try
            c1LabFlowSheet.BeginUpdate()
            ChkSelectAll.Checked = False
            ''Set c1Flexgrid Columns.
            'sanjog
            c1LabFlowSheet.Cols().Add(dtLabData.Columns.Count + 1)
            c1LabFlowSheet.Cols(0).Width = 50
            c1LabFlowSheet.SetCellStyle(0, 0, "CS_Header")
            c1LabFlowSheet.Cols(0).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            c1LabFlowSheet.SetData(0, 0, "Select")
            c1LabFlowSheet.Cols(0).AllowEditing = True
            'sanjog
            ''c1LabFlowSheet.Cols().Add(dtLabData.Columns.Count)
            ''c1LabFlowSheet.Cols(0).Width = 0
            c1LabFlowSheet.Cols(1).Width = 0
            c1LabFlowSheet.Cols(2).Width = 0
            c1LabFlowSheet.Cols(3).Width = 0
            c1LabFlowSheet.Cols(5).Width = 0

            ''Set column visibility..
            'sanjog
            'c1LabFlowSheet.Cols(0).Visible = False
            c1LabFlowSheet.Cols(1).Visible = False
            c1LabFlowSheet.Cols(2).Visible = False
            c1LabFlowSheet.Cols(3).Visible = False
            c1LabFlowSheet.Cols(5).Visible = False

            c1LabFlowSheet.Cols(6).Visible = False
            c1LabFlowSheet.Cols(7).Visible = False
            '  c1LabFlowSheet.Cols(c1LabFlowSheet.Cols.Count - 1).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

            'sanjog

            ''Set tree values.. 
            'sanjog
            c1LabFlowSheet.Tree.Column = 4
            c1LabFlowSheet.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            c1LabFlowSheet.Tree.Indent = 15

            ' ''Set rows..
            c1LabFlowSheet.Rows.Fixed = 1

            'Sanjog
            For iColumn As Integer = 1 To dtLabData.Columns.Count
                c1LabFlowSheet.SetCellStyle(0, iColumn, "CS_Header")
                c1LabFlowSheet.SetData(0, iColumn, dtLabData.Columns(iColumn - 1).ColumnName)
                c1LabFlowSheet.Cols(iColumn).Width = dtLabData.Columns(iColumn - 1).ColumnName.Length + 100
                c1LabFlowSheet.Cols(iColumn).AllowEditing = False
            Next
            'sanjog
            'For iColumn As Integer = 0 To dtLabData.Columns.Count - 1
            '    c1LabFlowSheet.SetCellStyle(0, iColumn, "CS_Header")
            '    c1LabFlowSheet.SetData(0, iColumn, dtLabData.Columns(iColumn).ColumnName)
            '    c1LabFlowSheet.Cols(iColumn).Width = dtLabData.Columns(iColumn).ColumnName.Length + 100
            'Next

            ''Set column sizes
            c1LabFlowSheet.Cols(4).Width = 200
            c1LabFlowSheet.Cols(5).Width = 0
            c1LabFlowSheet.Rows(0).Height = 50

            c1LabFlowSheet.Cols(6).Width = 0
            c1LabFlowSheet.Cols(7).Width = 0


            ''Set c1Flegrid rows.
            For iRow As Integer = 0 To dtLabData.Rows.Count - 1
                c1LabFlowSheet.Rows.Add()
                If (dtLabData.Rows(iRow)("IsResult")) = "False" Then



                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).IsNode = True
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).ImageAndText = True
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Style = c1LabFlowSheet.Styles("CS_Record")
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Level = 0
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Height = 22
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Image = ImgTest.Image
                    '00000170 : Data migration brought over lab results from Centricity. The Provider can view the results in the lab order button but wants to see it as a flowsheet.
                    'DMSID field validated for NULL or Empty.
                    If Not String.IsNullOrEmpty(dtLabData.Rows(iRow)("DMSID")) AndAlso Convert.ToInt64(dtLabData.Rows(iRow)("DMSID")) > 0 Then
                        c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Image = ImgAttachment.Image
                    End If
                    'sanjog
                    For iColRow As Integer = 1 To c1LabFlowSheet.Cols.Count - 1
                        c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, dtLabData.Rows(iRow)(iColRow - 1).ToString())
                    Next
                    'sanjog
                Else

                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).IsNode = True
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).ImageAndText = True
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Style = c1LabFlowSheet.Styles("CS_NotNormal")
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Level = 1
                    c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Height = 22
                    'c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Image = ImgResult.Image

                    If Not IsNothing(dtLabData.Rows(iRow)("ResultComment")) Then
                        If Convert.ToString(dtLabData.Rows(iRow)("ResultComment")) <> "" Then
                            c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Image = ImgResult_Comment.Image
                        Else
                            c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Image = ImgResult_Flask.Image
                        End If
                    Else
                        c1LabFlowSheet.Rows(c1LabFlowSheet.Rows.Count - 1).Node.Image = ImgResult_Flask.Image
                    End If
                    Dim FlagCode As String()
                    'sanjog
                    c1LabFlowSheet.SetCellCheck(c1LabFlowSheet.Rows.Count - 1, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)

                    For iColRow As Integer = 1 To c1LabFlowSheet.Cols.Count - 1
                        'sanjog
                        Dim sValue As String = String.Empty

                        sValue = dtLabData.Rows(iRow)(iColRow - 1).ToString()

                        If sValue.Length <= 0 Then
                            Continue For
                        End If

                        'start of code Added by manoj on 20121130 for showing hyperlink in result value
                        If IsColumnIsResultValue(dtLabData.Columns(iColRow - 1).ColumnName) AndAlso gloGlobal.gloLabGenral.IsResultisHyperLink(sValue) Then
                            SetHyperlinkResult(c1LabFlowSheet.Rows.Count - 1, iColRow, sValue)
                            Continue For
                        End If
                        'end of code Added by manoj on 20121130 for showing hyperlink in result value
                        c1LabFlowSheet.SetCellStyle(c1LabFlowSheet.Rows.Count - 1, 0, c1LabFlowSheet.Styles("CS_CheckBox"))
                        ''ResultValue|Unit|Range

                        Dim ssValue As String() = sValue.Split("|"c)

                        If ssValue.Length = 1 Then
                            'sanjog
                            If iColRow = 5 Then ''To concatenate the resultnumber
                                Dim str As String = ""
                                str = c1LabFlowSheet.GetData(c1LabFlowSheet.Rows.Count - 1, iColRow)
                                If str = "" Then
                                    c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString())
                                Else
                                    str = str & "$" & ssValue(0).ToString()
                                End If
                            Else ''To concatenate the resultnumber
                                c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString())
                            End If
                            'sanjog
                        ElseIf ssValue.Length = 2 Then
                            ''Highlight
                            If ssValue(1) = "A" Or ssValue(1) = "L" Or ssValue(1) = "H" Or ssValue(1) = "LL" Or ssValue(1) = "HH" Or ssValue(1) = "AA" Then
                                c1LabFlowSheet.SetCellStyle(c1LabFlowSheet.Rows.Count - 1, iColRow, "AbnormalRange")
                            End If

                            If ssValue(1) <> "" And ssValue(1).Length > 0 Then
                                c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString() & " (" & ssValue(1).ToString() & ")  ")
                            Else
                                c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString())
                            End If

                        ElseIf ssValue.Length = 3 Then

                            FlagCode = ssValue(1).Split(",")
                            If FlagCode.Length > 1 Then 'This means we have two flag one is from system and another one is for pat. spec range calculated flag
                                If FlagCode(0) = "A" Or FlagCode(0) = "L" Or FlagCode(0) = "H" Or FlagCode(0) = "LL" Or FlagCode(0) = "HH" Or FlagCode(0) = "AA" Or FlagCode(0) = ",A" Or FlagCode(0) = ",L" Or FlagCode(0) = ",H" Or FlagCode(0) = ",LL" Or FlagCode(0) = ",HH" Or FlagCode(0) = ",AA" Or FlagCode(1) = "A" Or FlagCode(1) = "L" Or FlagCode(1) = "H" Or FlagCode(1) = "LL" Or FlagCode(1) = "HH" Or FlagCode(1) = "AA" Or FlagCode(1) = ",A" Or FlagCode(1) = ",L" Or FlagCode(1) = ",H" Or FlagCode(1) = ",LL" Or FlagCode(1) = ",HH" Or FlagCode(1) = ",AA" Then
                                    c1LabFlowSheet.SetCellStyle(c1LabFlowSheet.Rows.Count - 1, iColRow, "AbnormalRange")
                                End If
                            Else
                                If ssValue(1) = "A" Or ssValue(1) = "L" Or ssValue(1) = "H" Or ssValue(1) = "LL" Or ssValue(1) = "HH" Or ssValue(1) = "AA" Or ssValue(1) = ",A" Or ssValue(1) = ",L" Or ssValue(1) = ",H" Or ssValue(1) = ",LL" Or ssValue(1) = ",HH" Or ssValue(1) = ",AA" Then
                                    c1LabFlowSheet.SetCellStyle(c1LabFlowSheet.Rows.Count - 1, iColRow, "AbnormalRange")
                                End If
                            End If


                            If chkRange.Checked Then

                                If ssValue(1).Trim() <> "" And ssValue(1).Trim().Length > 0 And ssValue(2).Trim() <> "" And ssValue(2).Trim().Length > 0 Then
                                    c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString() & " (" & ssValue(1).ToString() & ")  " & " [" & ssValue(2).ToString() & "] ")
                                ElseIf ssValue(1).Trim() <> "" And ssValue(1).Trim().Length > 0 And ssValue(2).Trim() = "" And ssValue(2).Trim().Length = 0 Then
                                    c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString() & " (" & ssValue(1).ToString() & ")  ")
                                ElseIf ssValue(2).Trim() <> "" And ssValue(2).Trim().Length <> 0 And ssValue(1).Trim() = "" And ssValue(1).Trim().Length = 0 Then
                                    c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString() & " [" & ssValue(2).ToString() & "] ")
                                Else
                                    '23-Oct-13 Aniket: Resolving Bug #58717. Show result even if there is no result range. 
                                    c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString())
                                End If

                            Else
                                If ssValue(1) <> "" And ssValue(1).Length > 0 Then
                                    c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString() & " (" & ssValue(1).ToString() & ")  ")

                                Else
                                    c1LabFlowSheet.SetData(c1LabFlowSheet.Rows.Count - 1, iColRow, ssValue(0).ToString())

                                End If
                            End If
                        End If

                    Next
                End If
            Next

            ''Column resize
            For index As Integer = 1 To c1LabFlowSheet.Cols.Count - 1
                c1LabFlowSheet.AutoSizeCol(index)
            Next

            c1LabFlowSheet.EndUpdate()
        Catch ex As Exception
            '00000170 : Data migration brought over lab results from Centricity. The Provider can view the results in the lab order button but wants to see it as a flowsheet.
            'Exception handled to log the exception.
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try


    End Sub

#End Region

#Region "Events"
    'Dim objRpt As gloEMRReports.rptLabFlowSheetReport
    Dim cntCol As Integer = 0
    Dim intrptPages As Integer = 0
    Dim intLastColPrinted As Integer = 2
    Dim MyText As CrystalDecisions.CrystalReports.Engine.TextObject

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Dim _dtLabdata As DataTable = GetLabPrintData(_nPatientId, dtpFromDate.Value, dtpToDate.Value)
        Dim dtPatientTable As DataTable = GetPatientInformation(_nPatientId, _sConnectionString)

        RaiseEvent gUC_LabFlowSheet_TestResultPrint(_dtLabdata, dtPatientTable, intrptPages, intLastColPrinted, MyText, cntCol)
        intLastColPrinted = 2
        'sanjog
        PrintFlowSheet()
        If (IsNothing(_dtLabdata) = False) Then
            _dtLabdata.Dispose()
            _dtLabdata = Nothing
        End If
        If (IsNothing(dtPatientTable) = False) Then
            dtPatientTable.Dispose()
            dtPatientTable = Nothing
        End If
        'sanjog
        ''c1LabFlowSheet.PrintGrid("ddoc", C1.Win.C1FlexGrid.PrintGridFlags.ActualSize, "Lab flowsheet", "End")
        ''PrintGrid("Doc", C1.Win.C1FlexGrid.PrintGridFlags.ShowPrintDialog)

        'Try


        '    Dim dtPatientTable As DataTable = GetPatientInformation(_nPatientId, _sConnectionString)
        '    If Not IsNothing(dtPatientTable) Then
        '        'Dim objLab As New clsLabDBLayer()
        '        Dim intReportpart As Integer = 1
        '        Dim _dtLabdata As DataTable = GetLabPrintData(_nPatientI d, dtpFromDate.Value, dtpToDate.Value)
        '        '_dtLabdata.Columns.Remove("IsResult")
        '        If Not IsNothing(_dtLabdata) Then

        '            _dtLabdata.Columns.Remove("TestId")
        '            _dtLabdata.Columns.Remove("OrderId")

        '            cntCol = _dtLabdata.Columns.Count

        '            intrptPages = Convert.ToInt32(cntCol / 6)
        '            If intrptPages = 0 Then
        '                intrptPages = 1
        '            End If
        '            While (intrptPages <> 0)
        '                Dim ds As New gloEMRReports.DataSet1()
        '                Dim dummyTable As DataTable = ds.Tables(1)
        '                'Assigning the names of columns
        '                dummyTable.Columns(0).ColumnName = "TestName"
        '                Dim Cnt As Integer = 1
        '                For nCnt As Integer = intLastColPrinted To intLastColPrinted + 5
        '                    If nCnt = _dtLabdata.Columns.Count Then
        '                        Exit For
        '                    End If
        '                    dummyTable.Columns(Cnt).ColumnName = _dtLabdata.Columns(nCnt).ColumnName.Replace("/", "-")
        '                    Cnt += 1
        '                Next



        '                objRpt = New gloEMRReports.rptLabFlowSheetReport()
        '                'Assigning Patient Data to report
        '                MyText = CType(objRpt.ReportDefinition.ReportObjects("TxtPatientname"), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                MyText.Text = Convert.ToString(dtPatientTable.Rows(0)("Patient Name"))

        '                MyText = CType(objRpt.ReportDefinition.ReportObjects("TxtDOB"), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                MyText.Text = Convert.ToString(Convert.ToDateTime(dtPatientTable.Rows(0)("DOB")).ToShortDateString())

        '                MyText = CType(objRpt.ReportDefinition.ReportObjects("TxtGender"), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                MyText.Text = Convert.ToString(dtPatientTable.Rows(0)("Gender"))


        '                MyText = CType(objRpt.ReportDefinition.ReportObjects("TxtClinicName"), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                MyText.Text = Convert.ToString(dtPatientTable.Rows(0)("Practice Name"))

        '                MyText = CType(objRpt.ReportDefinition.ReportObjects("TxtAge"), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                MyText.Text = Convert.ToString(dtPatientTable.Rows(0)("Age"))


        '                'For Each col As DataColumn In dummyTable.Columns
        '                '    MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & i), TextObject)
        '                '    MyText.Text = col.ColumnName.Replace("/", "-")
        '                '    i += 1
        '                'Next

        '                For tempCnt As Int32 = 1 To dummyTable.Columns.Count - 1
        '                    MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & tempCnt), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                    MyText.Text = dummyTable.Columns(tempCnt - 1).ColumnName.Replace("/", "-")
        '                Next

        '                If Cnt < 7 Then
        '                    While Cnt <> 7
        '                        MyText = CType(objRpt.ReportDefinition.ReportObjects("Text" & Cnt + 1), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                        MyText.Text = ""
        '                        Cnt += 1
        '                    End While
        '                End If

        '                'Reading Data to report
        '                For Each dr As DataRow In _dtLabdata.Rows
        '                    Dim newRow As DataRow = dummyTable.NewRow()
        '                    For Each dataCol As DataColumn In dummyTable.Columns
        '                        If _dtLabdata.Columns.Contains(dataCol.ColumnName.Replace("-", "/")) Then
        '                            newRow(dataCol.ColumnName) = Convert.ToString(dr(dataCol.ColumnName.Replace("-", "/"))).Replace("|", " ")
        '                        End If
        '                        If dataCol.ColumnName = "DataColumn8" Then
        '                            newRow("DataColumn8") = Convert.ToString(dr("IsResult"))
        '                        End If
        '                    Next
        '                    dummyTable.Rows.Add(newRow)
        '                Next
        '                If Not dummyTable.Columns.Contains("DataColumn1") Then
        '                    dummyTable.Columns(0).ColumnName = "DataColumn1"
        '                End If
        '                If Not dummyTable.Columns.Contains("DataColumn2") Then
        '                    dummyTable.Columns(1).ColumnName = "DataColumn2"
        '                End If
        '                If Not dummyTable.Columns.Contains("DataColumn3") Then
        '                    dummyTable.Columns(2).ColumnName = "DataColumn3"
        '                End If
        '                If Not dummyTable.Columns.Contains("DataColumn4") Then
        '                    dummyTable.Columns(3).ColumnName = "DataColumn4"
        '                End If
        '                If Not dummyTable.Columns.Contains("DataColumn5") Then
        '                    dummyTable.Columns(4).ColumnName = "DataColumn5"
        '                End If
        '                If Not dummyTable.Columns.Contains("DataColumn6") Then
        '                    dummyTable.Columns(4).ColumnName = "DataColumn6"
        '                End If
        '                If Not dummyTable.Columns.Contains("DataColumn7") Then
        '                    dummyTable.Columns(4).ColumnName = "DataColumn7"
        '                End If


        '                objRpt.SetDataSource(ds.Tables(1))

        '                intLastColPrinted += (dummyTable.Columns.Count - 2)

        '                MyText = CType(objRpt.ReportDefinition.ReportObjects("TxtreportPage"), CrystalDecisions.CrystalReports.Engine.TextObject)
        '                MyText.Text = Convert.ToString(intReportpart.ToString())

        '                intrptPages -= 1
        '                intReportpart += 1
        '                'If intrptPages = 0 Then
        '                'btnShowReport.Enabled = False
        '                'End If

        '                objRpt.PrintToPrinter(1, False, 1, 0)
        '            End While
        '        End If
        '    End If
        'Catch ex As Exception

        'Finally

        '    intLastColPrinted = 2
        'End Try

    End Sub
    Public Sub PrintFlowSheet()
        Try
            Dim row As Int16 = 0
            Dim dtrow As Int16 = 0
            Dim str1 As String = String.Empty
            Dim blnRange As Int16
            Dim strTestIds As String = String.Empty
            Dim strOrderIDs As String = String.Empty
            Dim strResultname As String = String.Empty
            Dim strResultTestNumber As String = String.Empty
            Dim chkval As C1.Win.C1FlexGrid.CheckEnum
            For row = 0 To c1LabFlowSheet.Rows.Count - 1
                chkval = c1LabFlowSheet.GetCellCheck(row, 0)
                If chkval = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                    'For dtrow = 0 To _dtOrderIDDataTable.Columns.Count - 1
                    '    If _dtOrderIDDataTable.Rows(row - 1)(dtrow).ToString() <> "" Then
                    '        If str1 = "" Then
                    '            str1 = _dtOrderIDDataTable.Rows(row - 1)(dtrow).ToString()
                    '        Else
                    '            str1 = str1 & "$" & _dtOrderIDDataTable.Rows(row - 1)(dtrow).ToString()
                    '        End If
                    '    End If
                    'Next
                    If strTestIds = String.Empty Then
                        strOrderIDs = c1LabFlowSheet.GetData(row, 2).ToString()
                        strTestIds = c1LabFlowSheet.GetData(row, 3).ToString()
                        strResultname = c1LabFlowSheet.GetData(row, 4).ToString().Replace(",", "$")
                        strResultTestNumber = c1LabFlowSheet.GetData(row, 5).ToString()
                    Else
                        strOrderIDs = strOrderIDs & "~" & c1LabFlowSheet.GetData(row, 2).ToString()
                        strTestIds = strTestIds & "~" & c1LabFlowSheet.GetData(row, 3).ToString()
                        strResultname = strResultname & "~" & c1LabFlowSheet.GetData(row, 4).ToString().Replace(",", "$")
                        strResultTestNumber = strResultTestNumber & "~" & c1LabFlowSheet.GetData(row, 5).ToString()
                    End If
                    str1 = String.Empty
                End If
            Next
            If strTestIds = "" Then
                MessageBox.Show("Select at least one result", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If chkRange.Checked Then
                blnRange = 1
            Else
                blnRange = 0
            End If
            RaiseEvent gUC_LabFlowSheet_FlowSheetPrint(_nPatientId, dtpFromDate.Value.ToString(), dtpToDate.Value.ToString(), strOrderIDs, strTestIds, strResultname, strResultTestNumber, blnRange)


        Catch ex As Exception

        End Try
    End Sub
    Public Function GetPatientInformation(ByVal nPatinetId As Int64, ByVal strConnection As String) As DataTable
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(_sConnectionString)
        Try
            oDbLayer.Connect(False)
            Dim oPatientDataTable As DataTable = Nothing
            oDbLayer.Retrive_Query("SELECT dbo.GET_NAME(Patient.sFirstName, Patient.sMiddleName, Patient.sLastName) As 'Patient Name',Convert(Datetime,Patient.dtDOB,101)As DOB, datediff(yy,Patient.dtDOB,dbo.gloGetDate()) AS 'Age',Patient.sGender As Gender,  Clinic_MST.sClinicName 'Practice Name' FROM Clinic_MST INNER JOIN Patient ON Clinic_MST.nClinicID = Patient.nClinicID Where Patient.nPatientId=" + nPatinetId.ToString(), oPatientDataTable)
            Return oPatientDataTable
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(oDbLayer) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
    End Function

    Private Sub dtpFromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged
        Dim dtFlowSheet As DataTable = Nothing

        If _nPatientId < 0 Then
            Return
        End If

        c1LabFlowSheet.DataSource = Nothing

        dtFlowSheet = GetLabData(_nPatientId, dtpFromDate.Text, dtpToDate.Text)
        ''Sanjog -Commented on 11-Jan-2011 to show blank result also
        'If IsNothing(dtFlowSheet) Or dtFlowSheet.Rows.Count <= 0 Then
        '    Return
        'End If
        ''Sanjog -Commented on 11-Jan-2011 to show blank result also

        DesignGrid()
        DesignData(dtFlowSheet)
        'DesignData()
    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
        Dim dtFlowSheet As DataTable = Nothing

        If _nPatientId < 0 Then
            Return
        End If

        c1LabFlowSheet.DataSource = Nothing

        dtFlowSheet = GetLabData(_nPatientId, dtpFromDate.Text, dtpToDate.Text)
        ''Sanjog -Commented on 11-Jan-2011 to show blank result also
        'If IsNothing(dtFlowSheet) Or dtFlowSheet.Rows.Count <= 0 Then
        '    Return
        'End If
        ''Sanjog -Commented on 11-Jan-2011 to show blank result also
        DesignGrid()
        DesignData(dtFlowSheet)
        'DesignData()
    End Sub

    Private Sub chkRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRange.CheckedChanged
        Dim dtFlowSheet As DataTable = Nothing

        If _nPatientId < 0 Then
            Return
        End If

        c1LabFlowSheet.DataSource = Nothing

        dtFlowSheet = GetLabData(_nPatientId, dtpFromDate.Text, dtpToDate.Text)
        If IsNothing(dtFlowSheet) OrElse dtFlowSheet.Rows.Count <= 0 Then
            Return
        End If

        DesignGrid()
        DesignData(dtFlowSheet)

    End Sub

    Private Sub c1LabFlowSheet_BeforeEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles c1LabFlowSheet.BeforeEdit
        Try
            If c1LabFlowSheet.ColSel = 0 Then
                Dim chkval As C1.Win.C1FlexGrid.CheckEnum
                chkval = c1LabFlowSheet.GetCellCheck(c1LabFlowSheet.RowSel, c1LabFlowSheet.ColSel)
                If chkval = C1.Win.C1FlexGrid.CheckEnum.None Then
                    e.Cancel = True
                Else

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub c1LabFlowSheet_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles c1LabFlowSheet.MouseDoubleClick
        Dim nOrderID As Int64 = 0
        Dim iRowSel As Int16 = 0
        Dim iColSel As Int16 = 1

        Try
            If c1LabFlowSheet.ColSel() <> COL_TESTNAME And c1LabFlowSheet.RowSel() <> 1 Then

                If (c1LabFlowSheet.GetData(c1LabFlowSheet.RowSel, 1)).ToString() <> "False" AndAlso (c1LabFlowSheet.GetData(c1LabFlowSheet.RowSel, c1LabFlowSheet.ColSel)) <> Nothing Then

                    iRowSel = c1LabFlowSheet.RowSel - 1
                    iColSel = c1LabFlowSheet.ColSel

                    If iRowSel <= 0 And iColSel <= 0 Then
                        Exit Sub
                    End If

                    If Not IsNothing(_dtOrderIDDataTable) AndAlso _dtOrderIDDataTable.Rows.Count > 0 Then
                        If Not IsNothing(_dtOrderIDDataTable.Rows(iRowSel)(iColSel - 1)) Then
                            nOrderID = Convert.ToInt64(_dtOrderIDDataTable.Rows(iRowSel)(iColSel - 1).ToString())
                        End If
                    End If

                    If nOrderID > 0 Then
                        RaiseEvent gUC_LabFlowSheet_Print(nOrderID)
                    End If

                End If

            End If
        Catch ex As Exception

        Finally
            nOrderID = 0
            iRowSel = 0
            iColSel = 1

        End Try

    End Sub

#End Region

    Private Sub btnPrint_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.MouseHover
        tpPrintLabFlowSheet.SetToolTip(btnPrint, "Print Lab Flow Sheet")
    End Sub

    'Sanjog Added on 2011 march 2 to check all 
    Private Sub ChkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkSelectAll.CheckedChanged
        Dim chkval As C1.Win.C1FlexGrid.CheckEnum
        Dim row As Integer = 0
        Dim cnt As Integer = 0
        Try
            For row = 0 To c1LabFlowSheet.Rows.Count - 1
                chkval = c1LabFlowSheet.GetCellCheck(row, 0)
                If chkval = C1.Win.C1FlexGrid.CheckEnum.Checked Or chkval = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                    If ChkSelectAll.Checked Then
                        c1LabFlowSheet.SetCellCheck(row, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
                    Else
                        c1LabFlowSheet.SetCellCheck(row, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                    End If
                    cnt = cnt + 1
                End If
            Next
            If cnt = 0 Then
                MessageBox.Show("There is no result to select", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Sanjog Added on 2011 march 2 to check all 



    'Added by manoj jadhav on 20121127 for making Reuslt value as hyper link
    Private Sub c1LabFlowSheet_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1LabFlowSheet.MouseDown
        Try
            If Cursor Is Cursors.Hand Then
                ht = c1LabFlowSheet.HitTest(e.X, e.Y)
                If ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell AndAlso TypeOf c1LabFlowSheet(ht.Row, ht.Column) Is Hyperlink Then
                    c1LabFlowSheet(ht.Row, ht.Column).Activate()
                End If
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            ht = Nothing
        End Try
    End Sub

    'Added by manoj jadhav on 20121127 for making Reuslt value as hyper link
    Private Sub c1LabFlowSheet_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles c1LabFlowSheet.MouseMove
        Try
            ht = c1LabFlowSheet.HitTest(e.X, e.Y)
            If ht.Type = C1.Win.C1FlexGrid.HitTestTypeEnum.Cell AndAlso TypeOf c1LabFlowSheet(ht.Row, ht.Column) Is Hyperlink Then
                Cursor = Cursors.Hand
            Else
                Cursor = Cursors.Default
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            ht = Nothing
        End Try
    End Sub

    'Added by manoj jadhav on 20121127 for making Reuslt value as hyper link
    Private Sub c1LabFlowSheet_OwnerDrawCell(sender As Object, e As C1.Win.C1FlexGrid.OwnerDrawCellEventArgs) Handles c1LabFlowSheet.OwnerDrawCell
        Try
            If Not c1LabFlowSheet(e.Row, e.Col) Is Nothing AndAlso TypeOf c1LabFlowSheet(e.Row, e.Col) Is Hyperlink Then
                If c1LabFlowSheet(e.Row, e.Col).Visited Then
                    If String.Compare(c1LabFlowSheet.GetCellStyle(e.Row, e.Col).Name, "AbNewLink", False) = 0 Then
                        e.Style = c1LabFlowSheet.Styles("AbOldLink")
                    Else
                        e.Style = c1LabFlowSheet.Styles("OldLink")
                    End If
                End If
            End If
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub

    'Added by manoj jadhav on 20121129 for making Reuslt value as hyper link
    Private Function IsColumnIsResultValue(ByVal ColmnName As String) As Boolean
        IsColumnIsResultValue = False
        Try
            If String.IsNullOrEmpty(ColmnName) Then
                IsColumnIsResultValue = False
            ElseIf String.Compare(ColmnName, "IsResult", False) = 0 Then
                IsColumnIsResultValue = False
            ElseIf String.Compare(ColmnName, "OrderId", False) = 0 Then
                IsColumnIsResultValue = False
            ElseIf String.Compare(ColmnName, "Test Name", False) = 0 Then
                IsColumnIsResultValue = False
            ElseIf String.Compare(ColmnName, "TestResultNumber", False) = 0 Then
                IsColumnIsResultValue = False
            ElseIf String.Compare(ColmnName, "ResultComment", False) = 0 Then
                IsColumnIsResultValue = False
            ElseIf String.Compare(ColmnName, "ResultAbnormalFlag", False) = 0 Then
                IsColumnIsResultValue = False
            Else
                IsColumnIsResultValue = True
            End If
        Catch ex As Exception
            ex = Nothing
            IsColumnIsResultValue = False
        End Try

    End Function

    'Added by manoj jadhav on 20121129 for making Reuslt value as hyper link
    Private Function GetResultValue(ByVal ResultValue As String) As String
        Dim ResultValueArray As String()
        Try
            If String.IsNullOrEmpty(ResultValue) Then
                GetResultValue = String.Empty
                Exit Function
            End If

            ResultValueArray = ResultValue.Split(" ")

            If ResultValueArray.Length = 0 Then
                GetResultValue = ResultValueArray(0)
                Exit Function
            End If
            ResultValueArray(ResultValueArray.Length - 1) = ""
            GetResultValue = String.Join(" ", ResultValueArray).Trim()
        Catch ex As Exception
            ex = Nothing
            GetResultValue = String.Empty
        Finally
            ResultValueArray = Nothing
        End Try
    End Function

    'Added by manoj jadhav on 20121129 for making Reuslt value as hyper link
    Private Function GetResultFlag(ByVal ResultValue As String) As String
        Dim ResultValueArray As String()
        Try
            If String.IsNullOrEmpty(ResultValue) Then
                GetResultFlag = ""
                Exit Function
            End If
            ResultValueArray = ResultValue.Split(" ")
            If ResultValueArray.Length = 0 Then
                GetResultFlag = ""
                Exit Function
            End If
            GetResultFlag = ResultValueArray(ResultValueArray.Length - 1)
        Catch ex As Exception
            ex = Nothing
            GetResultFlag = ""
        End Try
    End Function

    'Added by manoj jadhav on 20121129 for making Reuslt value as hyper link
    Private Sub SetHyperlinkResult(ByVal Irow As Integer, ByVal Icol As Integer, ByVal ResultValue As String)
        Dim AbnormalFlag As String = String.Empty
        Try
            c1LabFlowSheet.Item(Irow, Icol) = New Hyperlink(GetResultValue(ResultValue))
            AbnormalFlag = GetResultFlag(ResultValue)
            If AbnormalFlag = "A" Or AbnormalFlag = "L" Or AbnormalFlag = "H" Or AbnormalFlag = "LL" Or AbnormalFlag = "HH" Or AbnormalFlag = "AA" Then
                c1LabFlowSheet.SetCellStyle(Irow, Icol, "AbNewLink")
            Else
                c1LabFlowSheet.SetCellStyle(Irow, Icol, "NewLink")
            End If
        Catch ex As Exception
            ex = Nothing
        Finally
            AbnormalFlag = String.Empty
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class


