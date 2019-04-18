Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid
Imports gloUserControlLibrary
Public Class frmPatientSummary
    Private SelectedPatientDetail As PatientDetails
    Private blnIsExam As Boolean = True
    Dim oImage_D As New frmDMS_Support
    Private m_ExamFilter As Boolean = False
    Private m_ResetFlag As Boolean = False
    Private WithEvents _PatientStrip As gloUC_PatientStrip
    Dim WithEvents oShowDocument As gloEDocumentV3.gloEDocV3Management

    Private WithEvents oSearchProblemListCtl As gloUCGeneralSearch
    Private WithEvents oSearchMedicationsCtl As gloUCGeneralSearch
    Private WithEvents oSearchAllergiesCtl As gloUCGeneralSearch
    'Private WithEvents oSearchMedicationsCtl As gloUCGeneralSearch
    Public Enum PatientDetails
        History = 1
        Medication = 2
        Prescription = 3
        ProblemList = 4
        ViewDocs = 5
        Orders = 6
        Messages = 7
        PastExams = 8
        NewExam = 9
        FAX_Pending = 10
        FAX_Sent = 11
        AuditTrail = 12
        Procedures = 13
    End Enum

    Private Const COL_D_CAT_ID = 0 ' ID
    Private Const COL_D_CAT_NAME = 1 ' Name
    Private Const COL_D_CAT_NOTEFLAG = 2 ' Note Flag
    Private Const COL_D_CAT_EXTRAFLAG = 3 ' Extra Col
    Private Const COL_D_CAT_SOURCEMACHINE = 4 ' Source Machine
    Private Const COL_D_CAT_SYSTEMFOLDER = 5 ' System Folder
    Private Const COL_D_CAT_CONTAINER = 6 ' Container
    Private Const COL_D_CAT_CATEGORY = 7 ' Category
    Private Const COL_D_CAT_PATIENTID = 8 ' Patient ID
    Private Const COL_D_CAT_YEAR = 9 ' Year
    Private Const COL_D_CAT_MONTH = 10 ' Month
    Private Const COL_D_CAT_SOURCEBIN = 11 ' Source Bin
    Private Const COL_D_CAT_INUSED = 12 ' In Used
    Private Const COL_D_CAT_USEDMACHINE = 13 ' Used Machine
    Private Const COL_D_CAT_USEDTYPE = 14 ' Used Type
    Private Const COL_D_CAT_PATH = 15 ' Path
    Private Const COL_D_CAT_COLTYPE = 16
    Private Const COL_D_CAT_FILENAME = 17 ' File Name
    Private Const COL_D_CAT_MACHINEID = 18 ' File Name
    Private Const COL_D_CAT_VERSIONNO = 19 ' Version No
    Private Const COL_D_CAT_ISREVIWED = 20 ' Reviwed
    Private Const COL_D_CAT_REVIWEDFLAG = 21 ' Reviwed
    'Private Const COL_D_CAT_COUNT = 22
    Private Const COL_View_CategoryHidden = 22
    Private Const COL_View_Category = 23
    Private Const COL_View_Month = 24
    Private Const COL_View_DocumentName = 25
    Private Const COL_View_NOTEFLAG = 26
    Private Const COL_View_REVIWEDFLAG = 27

    Private Const Col_view_Count = 28


    Dim strDia As String = " "

    Private Const COLUM_NAME = 0
    Private Const COLUM_IDENTITY = 1
    Private Const COLUM_NUMVALUE = 2
    Private Const COLUM_UNIT = 3
    Private Const COLUM_ID = 4
    Private Const COLUM_TESTGROUPFLAG = 5
    Private Const COLUM_LEVELNO = 6
    Private Const COLUM_GROUPNO = 7
    Private Const COLUM_TEMPLATEID = 8
    ''''
    Private Const COLUM_DIAGNOSIS = 9
    Private Const COLUM_DIAGNOSISBUTTON = 10
    ''''
    Private Const COLUM_BUTTON = 11
    Private Const COLUM_ISFINISHED = 12
    Private Const COLUM_DMSID = 13
    '' No of Columns
    Private Const COLUM_COUNT = 14


    Private Function PopulateProbemList()
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim strsql As String = ""
        Dim sqladpt As SqlDataAdapter
        Dim dt As New DataTable
        Try
            conn = New SqlConnection
            cmd = New SqlCommand

            conn.ConnectionString = GetConnectionString()
            conn.Open()
            strsql = "select isnull(sCheifComplaint,'') from ProblemList where nPatientID=" & gnPatientID
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text

            cmd.Connection = conn
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd

            sqladpt.Fill(dt)
            Dim icnt As Int32 = 0

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    trProblemList.Nodes.Clear()
                    trProblemList.Nodes.Add("Problem List")
                    Dim myNode As TreeNode
                    myNode = trProblemList.Nodes.Item(0)
                    myNode.ImageIndex = 0
                    myNode.SelectedImageIndex = 0
                    For icnt = 0 To dt.Rows.Count - 1
                        Dim mychildnode As TreeNode
                        mychildnode = New TreeNode(CType(dt.Rows.Item(icnt)(0), String))
                        mychildnode.ImageIndex = 6
                        mychildnode.SelectedImageIndex = 6
                        myNode.Nodes.Add(mychildnode)
                    Next
                End If
            End If

            trProblemList.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Private Function PopulateRadiology()
        Try


            ''Dim dtPatientDetails As DataTable
            Dim dtCategory As DataTable
            Dim dtGroup As DataTable
            Dim dtTest As DataTable
            Dim objPatientDetail As New clsPatientDetails
            Dim strSQL = ""
            strSQL = " SELECT  DISTINCT  LM_Category.lm_category_Description " _
                    & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                    & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & gnPatientID & ") " _
                    & " ORDER BY lm_category_Description "
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtCategory = oDB.ReadQueryDataTable(strSQL)

            'dtPatientDetails = objPatientDetail.Fill_PatientOrders(gnPatientID)
            Dim icnt As Int32 = 0

            If Not IsNothing(dtCategory) Then
                If dtCategory.Rows.Count > 0 Then
                    trImaging.Nodes.Clear()
                    trImaging.Nodes.Add("Radiology")
                    Dim myCategoryNode As TreeNode
                    Dim myGroupNode As TreeNode
                    Dim myTestNode As TreeNode


                    For icnt = 0 To dtCategory.Rows.Count - 1

                        myCategoryNode = New TreeNode()
                        myCategoryNode.Text = dtCategory.Rows(icnt)(0)
                        myCategoryNode.Tag = dtCategory.Rows(icnt)(0)
                        myCategoryNode.ImageIndex = 5
                        myCategoryNode.SelectedImageIndex = 5
                        trImaging.Nodes(0).Nodes.Add(myCategoryNode)


                        strSQL = " SELECT DISTINCT LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName " _
                       & " FROM LM_Test LEFT OUTER JOIN " _
                       & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                       & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
                       & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                       & " WHERE (LM_Orders.lm_Patient_ID =" & gnPatientID & ")  AND  " _
                       & " (LM_Category.lm_category_Description = '" & Replace(myCategoryNode.Text, "'", "''") & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
                       & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "
                        dtGroup = oDB.ReadQueryDataTable(strSQL)

                        Dim j As Int16
                        For j = 0 To dtGroup.Rows.Count - 1
                            myGroupNode = New TreeNode()
                            myGroupNode.Text = dtGroup.Rows(j)(1)
                            ''myGroupNode.Tag = dtCategory.Rows(icnt)(0)
                            myGroupNode.ImageIndex = 5
                            myGroupNode.SelectedImageIndex = 5
                            myCategoryNode.Nodes.Add(myGroupNode)

                            strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
                            & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, LM_Orders.lm_Status, " _
                            & " LM_Test.lm_test_Template_ID , LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description " _
                            & " FROM  LM_Test INNER JOIN " _
                            & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
                            & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                            & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & gnPatientID & ")  AND  " _
                            & " (LM_Category.lm_category_Description = '" & Replace(myCategoryNode.Text, "'", "''") & "') AND (LM_Test.lm_test_GroupNo =" & dtGroup.Rows(j)(0) & ") " _
                            & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

                            dtTest = oDB.ReadQueryDataTable(strSQL)

                            Dim k As Int16
                            For k = 0 To dtTest.Rows.Count - 1
                                myTestNode = New TreeNode()
                                myTestNode.Text = dtTest.Rows(k)(0)
                                ''myTestNode.Tag = dtCategory.Rows(icnt)(0)
                                myTestNode.ImageIndex = 6
                                myTestNode.SelectedImageIndex = 6
                                myGroupNode.Nodes.Add(myTestNode)
                            Next
                        Next

                    Next
                End If
            End If
            trImaging.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Function

    'Private Function PopulateRadiology()
    '    Try


    '        Dim dtPatientDetails As DataTable
    '        Dim objPatientDetail As New clsPatientDetails
    '        dtPatientDetails = objPatientDetail.Fill_PatientOrders(gnPatientID)
    '        Dim icnt As Int32 = 0

    '        If Not IsNothing(dtPatientDetails) Then
    '            If dtPatientDetails.Rows.Count > 0 Then
    '                trImaging.Nodes.Clear()
    '                trImaging.Nodes.Add("Radiology")
    '                Dim myNode As TreeNode
    '                Dim mychildnode As TreeNode
    '                Dim mychildnode1 As TreeNode

    '                myNode = trImaging.Nodes.Item(0)
    '                myNode.ImageIndex = 5
    '                myNode.SelectedImageIndex = 5
    '                For icnt = 0 To dtPatientDetails.Rows.Count - 1
    '                    For Each childnode As TreeNode In myNode.Nodes
    '                        If childnode.Text = dtPatientDetails.Rows(icnt)(2) Then
    '                            mychildnode = childnode
    '                            Exit For
    '                        End If
    '                    Next
    '                    If IsNothing(mychildnode) Then
    '                        mychildnode = New TreeNode(dtPatientDetails.Rows(icnt)(2))
    '                        mychildnode.ImageIndex = 5
    '                        mychildnode.SelectedImageIndex = 5
    '                        myNode.Nodes.Add(mychildnode)
    '                    End If
    '                    mychildnode1 = New TreeNode(dtPatientDetails.Rows(icnt)(3))
    '                    mychildnode1.ImageIndex = 6
    '                    mychildnode1.SelectedImageIndex = 6
    '                    mychildnode.Nodes.Add(mychildnode1)

    '                    mychildnode1 = Nothing
    '                    mychildnode = Nothing
    '                Next
    '            End If
    '        End If
    '        trImaging.ExpandAll()
    '    Catch ex As Exception

    '    End Try
    'End Function
    Private Function PopulateLatestMedications()

        Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter

        Try
            cnn.ConnectionString = GetConnectionString()
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GetLatestMedication"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = gnPatientID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            _table.Load(cmd.ExecuteReader())


            If Not IsNothing(_table) Then
                If _table.Rows.Count > 0 Then
                    trMedications.Nodes.Clear()
                    Dim mynode As New TreeNode("Medications")
                    mynode.ImageIndex = 1
                    mynode.SelectedImageIndex = 1
                    trMedications.Nodes.Add(mynode)
                    Dim icnt As Int32 = 0
                    For icnt = 0 To _table.Rows.Count - 1
                        Dim mychildNode As New TreeNode
                        mychildNode.Text = _table.Rows(icnt)("sMedication") & " " & _table.Rows(icnt)("sDosage")
                        mychildNode.ImageIndex = 6
                        mychildNode.SelectedImageIndex = 6
                        mynode.Nodes.Add(mychildNode)
                    Next
                End If
            End If
            trMedications.ExpandAll()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try


    End Function

    Public Function PopulateLabs()

        Dim _table As New DataTable
        Dim osqladpt As SqlDataAdapter
        Dim cmd As SqlCommand
        Dim cnn As New SqlConnection()
        Dim osqlpara As SqlParameter

        Try
            cnn.ConnectionString = GetConnectionString()
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "Labs_GetLabsforPatient"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@npatientid"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = gnPatientID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
           
            _table.Load(cmd.ExecuteReader)
            If Not IsNothing(_table) Then
                Dim icnt As Int32
                If _table.Rows.Count > 0 Then
                    
                    Dim mynode As New TreeNode("Labs")
                    trLabs.Nodes.Clear()
                    trLabs.Nodes.Add(mynode)
                    Dim mychildnode1 As TreeNode

                    mynode.ImageIndex = 4
                    mynode.SelectedImageIndex = 4

                    For icnt = 0 To _table.Rows.Count - 1
                        mychildnode1 = New TreeNode()
                        mychildnode1.Tag = _table.Rows(icnt)(0).ToString & "-" & _table.Rows(icnt)(1).ToString
                        mychildnode1.Text = _table.Rows(icnt)(2).ToString
                        If Not IsDBNull(_table.Rows(icnt)(2)) Then
                            mychildnode1.Text = mychildnode1.Text & " (" & _table.Rows(icnt)(3).ToString & ")"
                        End If
                        mychildnode1.ImageIndex = 6
                        mychildnode1.SelectedImageIndex = 6
                        mynode.Nodes.Add(mychildnode1)
                        mychildnode1 = Nothing
                    Next

                End If
            End If


            _table.Rows.Clear()

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            Dim orderid As Int64 = 0
            Dim testid As Int64 = 0

            If trLabs.Nodes.Item(0).Nodes.Count > 0 Then
                Dim mynode As TreeNode
                mynode = trLabs.Nodes.Item(0)

                Dim strSQL As String = ""
                For Each childnode As TreeNode In mynode.Nodes
                    Splitdata(childnode.Tag, orderid, testid)
                    cmd = New SqlCommand
                    cmd.Connection = cnn
                    cmd.CommandType = CommandType.Text
                    strSQL = "select isnull(labotrd_ResultName,'')+ Space(1)+ isnull(labotrd_ResultValue,'')+ " _
                                    & " space(1) + isnull(labotrd_ResultUnit,'') from dbo.Lab_Order_Test_ResultDtl " _
                                    & " where labotrd_OrderID=" & orderid & " and labotrd_TestID=" & testid & ""
                    cmd.CommandText = strSQL
                    _table = Nothing

                    _table = New DataTable

                    _table.Load(cmd.ExecuteReader)

                    Dim icnt As Int32

                    For icnt = 0 To _table.Rows.Count - 1
                        Dim mychildnode1 As New TreeNode
                        mychildnode1.Text = _table.Rows(icnt)(0).ToString
                        mychildnode1.ImageIndex = 6
                        mychildnode1.SelectedImageIndex = 6
                        childnode.Nodes.Add(mychildnode1)
                    Next
                    cmd.Dispose()
                    cmd = Nothing
                    If Not IsNothing(_table) Then
                        _table.Clear()

                    End If
                    orderid = 0
                    testid = 0
                Next

            End If
            trLabs.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(osqladpt) Then
                osqladpt.Dispose()
                osqladpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
    End Function

    Private Function Splitdata(ByVal strdata As String, ByRef orderid As Int64, ByRef testid As Int64)
        Try
            Dim arr() As String = strdata.Split("-")
            If arr.Length > 0 Then
                orderid = arr(0)
            End If
            If arr.Length > 1 Then
                testid = arr(1)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Function

    Public Function PopulateLatestHistory()

        Dim strSQl As String = ""
        Dim _table As New DataTable
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand
        Dim cnn As New SqlConnection()

        Dim osqlpara As SqlParameter

        Try

            cnn.ConnectionString = GetConnectionString()
            cnn.Open()
            cmd = New SqlCommand
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "sp_CCDGetLatestAllergies"

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = gnPatientID

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@Category"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ""

            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            cmd.Parameters.Add(osqlpara)

            oDataReader = cmd.ExecuteReader()

            If Not IsNothing(oDataReader) Then
                Dim myNode As New TreeNode("History")
                myNode.ImageIndex = 2
                myNode.SelectedImageIndex = 2
                Dim mychildnode As TreeNode
                Dim mychildnode1 As TreeNode
                trHistory.Nodes.Clear()
                trHistory.Nodes.Add(myNode)

                While oDataReader.Read
                    If oDataReader.HasRows() Then
                        For Each childnode As TreeNode In myNode.Nodes
                            If childnode.Text = oDataReader.Item("sHistoryCategory") Then
                                mychildnode = childnode
                                Exit For
                            End If
                        Next
                        If IsNothing(mychildnode) Then
                            mychildnode = New TreeNode(oDataReader.Item("sHistoryCategory"))
                            mychildnode.ImageIndex = 2
                            mychildnode.SelectedImageIndex = 2
                            myNode.Nodes.Add(mychildnode)
                        End If
                        mychildnode1 = New TreeNode(oDataReader.Item("HistoryItem"))
                        mychildnode1.ImageIndex = 6
                        mychildnode1.SelectedImageIndex = 6
                        mychildnode.Nodes.Add(mychildnode1)

                        mychildnode1 = Nothing
                        mychildnode = Nothing
                    End If
                End While
                oDataReader.Close()
            End If
            trHistory.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(cnn) Then
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                cnn.Dispose()
                cnn = Nothing
            End If
        End Try
    End Function

    Public Function PopulateProcedures()
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim strsql As String = ""
        Dim sqladpt As SqlDataAdapter
        Dim dt As New DataTable
        Try
            conn = New SqlConnection
            cmd = New SqlCommand

            conn.ConnectionString = GetConnectionString()
            conn.Open()
            strsql = "select isnull(sCPTcode,'') + space(1) + isnull(sCPTDescription,'') ,'(' +  convert(varchar(50),dtdos,101) + ')' from ExamICD9CPT inner join patientexams on examicd9cpt.nexamid=patientexams.nexamid where examicd9cpt.nPatientID=" & gnPatientID & " and sCPTCode is not null"
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text

            cmd.Connection = conn
            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd

            sqladpt.Fill(dt)
            Dim icnt As Int32 = 0

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    trProcedures.Nodes.Clear()
                    trProcedures.Nodes.Add("Procedures")
                    Dim myNode As TreeNode
                    myNode = trProcedures.Nodes.Item(0)
                    myNode.ImageIndex = 3
                    myNode.SelectedImageIndex = 3
                    For icnt = 0 To dt.Rows.Count - 1
                        If Trim(CType(dt.Rows.Item(icnt)(0), String)) <> "" Then
                            Dim mychildnode As New TreeNode()
                            mychildnode.Text = CType(dt.Rows.Item(icnt)(0), String) & CType(dt.Rows.Item(icnt)(1), String)
                            mychildnode.ImageIndex = 6
                            mychildnode.SelectedImageIndex = 6
                            myNode.Nodes.Add(mychildnode)
                        End If
                    Next
                End If
            End If
            trProcedures.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
    End Function

    Private Sub frmPatientSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        gloC1FlexStyle.Style(c1CategorisedDocuments)
        gloC1FlexStyle.Style(C1dgPatientDetails)
        gloC1FlexStyle.Style(C1OrderDetails)
        gloC1FlexStyle.Style(c1ProblemList)
        Try
            oSearchProblemListCtl = New gloUCGeneralSearch
            oSearchProblemListCtl.Dock = DockStyle.Right
            oSearchProblemListCtl.BringToFront()
            pnlSearchproblemList.Controls.Add(oSearchProblemListCtl)

            oSearchMedicationsCtl = New gloUCGeneralSearch
            oSearchMedicationsCtl.Dock = DockStyle.Right
            oSearchMedicationsCtl.BringToFront()
            pnlSearchMedications.Controls.Add(oSearchMedicationsCtl)

            'oSearchProblemListCtl = New gloUCGeneralSearch
            'pnlSearchproblemList.Controls.Add(oSearchProblemListCtl)

            PopulateProbemList()
            PopulateLatestHistory()
            PopulateLatestMedications()
            PopulateProcedures()
            PopulateRadiology()
            PopulateLabs()
            dgExams.Visible = True
            blnIsExam = True
            FillExamDMS()
            FillExamProviderCombo()
            PopulateSearchCombo()
            cmbSearch.Text = "User Tag"
            lblsearch.Text = "User Tag"

            _PatientStrip = New gloUC_PatientStrip
            pnlMain.Controls.Add(_PatientStrip)
            _PatientStrip.ShowDetail(gnPatientID, gloUC_PatientStrip.enumFormName.None)
            _PatientStrip.HideButton = False
            _PatientStrip.Dock = DockStyle.Top
            _PatientStrip.Padding = New Padding(3, 0, 3, 0)
            _PatientStrip.Visible = True
            FillExamTypeCombo()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try

    End Sub

    Private Sub PopulateSearchCombo()
        
        Try
            cmbSearch.Items.Add("User Tag")
            cmbSearch.Items.Add("Notes")
            cmbSearch.Items.Add("Acknowledge")
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub ShowPatientDetails()
        Try
            dgPatientDetails.DataSource = Nothing
            'dgPatientDetails.
            C1dgPatientDetails.DataSource = Nothing
            C1dgPatientDetails.Clear(ClearFlags.All)


            If gnPatientID = 0 Then Exit Sub

            Select Case SelectedPatientDetail


                'Case PatientDetails.NewExam


                '    '''' Fill New Exams
                '    Call Fill_NewExams()


                Case PatientDetails.Medication
                    C1dgPatientDetails.Visible = False
                    dgPatientDetails.Visible = True
                    'trvPatientDetails.Visible = False

                    Dim dtPatientDetails As DataTable
                    Dim objPatientDetail As New clsPatientDetails
                    dtPatientDetails = objPatientDetail.Fill_Medication(gnPatientID)
                    'dtMedicationDate, sMedication ,sDosage ,sRoute, sFrequency, sDuration, sAmount, sStatus, dtStartDate, dtEndDate, UserID , UserName 
                    objPatientDetail = Nothing
                    dgPatientDetails.Enabled = False
                    dgPatientDetails.DataSource = dtPatientDetails.DefaultView
                    dgPatientDetails.Enabled = True
                    Dim grdColStyleDate As New DataGridTextBoxColumn
                    With grdColStyleDate
                        .HeaderText = "Date"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("dtMedicationDate").ColumnName
                        .NullText = ""
                        .Width = 0.13 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleMedication As New DataGridTextBoxColumn
                    With grdColStyleMedication
                        .HeaderText = "Medication"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("sMedication").ColumnName
                        .NullText = ""
                        .Width = 0.2 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleDosage As New DataGridTextBoxColumn
                    With grdColStyleDosage
                        .HeaderText = "Dosage"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("sDosage").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleRoute As New DataGridTextBoxColumn
                    With grdColStyleRoute
                        .HeaderText = "Route"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("sRoute").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleFrequency As New DataGridTextBoxColumn
                    With grdColStyleFrequency
                        .HeaderText = "Frequency"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("sFrequency").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleDuration As New DataGridTextBoxColumn
                    With grdColStyleDuration
                        .HeaderText = "Duration"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("sDuration").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleAmount As New DataGridTextBoxColumn
                    With grdColStyleAmount
                        .HeaderText = "Amount"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("sAmount").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleStatus As New DataGridTextBoxColumn
                    With grdColStyleStatus
                        .HeaderText = "Status"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("sStatus").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleStartDate As New DataGridTextBoxColumn
                    With grdColStyleStartDate
                        .HeaderText = "Start Date"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("dtStartDate").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    Dim grdColStyleEndDate As New DataGridTextBoxColumn
                    With grdColStyleEndDate
                        .HeaderText = "End Date"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("dtEndDate").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    'Dim grdColStyleUserID As New DataGridTextBoxColumn
                    'With grdColStyleUserID
                    '    .HeaderText = "UserID"
                    '    .Alignment = HorizontalAlignment.Left
                    '    .MappingName = dtPatientDetails.Columns("UserID").ColumnName
                    '    .NullText = ""
                    '    .Width = 0 * C1dgPatientDetails.Width
                    'End With

                    Dim grdColStyleUserName As New DataGridTextBoxColumn
                    With grdColStyleUserName
                        .HeaderText = "UserName"
                        .Alignment = HorizontalAlignment.Left
                        .MappingName = dtPatientDetails.Columns("UserName").ColumnName
                        .NullText = ""
                        .Width = 0.1 * C1dgPatientDetails.Width
                    End With

                    ''dtMedicationDate, sMedication ,sDosage ,sRoute, sFrequency, sDuration, sAmount, sStatus, dtStartDate, dtEndDate, UserName 
                    dgPatientDetails.TableStyles.Clear()
                    Dim grdTableStyle As New clsDataGridTableStyle(dtPatientDetails.TableName)
                    grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleDate, grdColStyleMedication, grdColStyleDosage, grdColStyleRoute, grdColStyleFrequency, grdColStyleDuration, grdColStyleAmount, grdColStyleStatus, grdColStyleStartDate, grdColStyleEndDate, grdColStyleUserName})
                    dgPatientDetails.TableStyles.Add(grdTableStyle)
                    oSearchMedicationsCtl.IntialiseDatatable(dtPatientDetails)
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient medication viewed from DashBoard", gloAuditTrail.ActivityOutCome.Success)

                Case PatientDetails.Orders
                    FillCategoryTestGroups()
                    ''trvPatientDetails.Visible = False
                    'Dim dtPatientDetails As DataTable
                    'Dim objPatientDetail As New clsPatientDetails
                    'dtPatientDetails = objPatientDetail.Fill_PatientOrders(gnPatientID)
                    ' ''lm_Visit_ID, lm_OrderDate, lm_category_Description, lm_test_Name,lm_NumericResult()
                    ''dgImaging = Nothing
                    'dgImaging.Enabled = False
                    'dgImaging.DataSource = dtPatientDetails.DefaultView
                    'dgImaging.Enabled = True
                    'dgImaging.TableStyles.Clear()

                    'Dim grdColStyleID As New DataGridTextBoxColumn
                    'With grdColStyleID
                    '    .HeaderText = "ID"
                    '    .Alignment = HorizontalAlignment.Left
                    '    .MappingName = dtPatientDetails.Columns(0).ColumnName
                    '    .NullText = ""
                    '    .Width = 0
                    'End With

                    'Dim grdColStyleDate As New DataGridTextBoxColumn
                    'With grdColStyleDate
                    '    .HeaderText = "Date"
                    '    .Alignment = HorizontalAlignment.Left
                    '    .MappingName = dtPatientDetails.Columns(1).ColumnName
                    '    .NullText = ""
                    '    .Width = 0.2 * C1dgPatientDetails.Width
                    'End With

                    'Dim grdColStyleExamName As New DataGridTextBoxColumn
                    'With grdColStyleExamName
                    '    .HeaderText = "Category"
                    '    .Alignment = HorizontalAlignment.Left
                    '    .MappingName = dtPatientDetails.Columns(2).ColumnName
                    '    .NullText = ""
                    '    .Width = 0.2 * C1dgPatientDetails.Width
                    'End With

                    'Dim grdColStyleFinished As New DataGridTextBoxColumn
                    'With grdColStyleFinished
                    '    .HeaderText = "Test"
                    '    .Alignment = HorizontalAlignment.Left
                    '    .MappingName = dtPatientDetails.Columns(3).ColumnName
                    '    .NullText = ""
                    '    .Width = 0.25 * C1dgPatientDetails.Width
                    'End With

                    ''''' 20070119 By Mahesh 
                    '''''  Lab Module For Numeric Result Added 
                    'Dim grdColStyleNumResult As New DataGridTextBoxColumn
                    'With grdColStyleNumResult
                    '    .HeaderText = "Numeric Result"
                    '    .Alignment = HorizontalAlignment.Left
                    '    .MappingName = dtPatientDetails.Columns(4).ColumnName
                    '    .NullText = ""
                    '    .Width = 0 * C1dgPatientDetails.Width - 5
                    'End With

                    'Dim grdColStyleDia As New DataGridTextBoxColumn
                    'With grdColStyleDia
                    '    .HeaderText = "Diagnosis"
                    '    .Alignment = HorizontalAlignment.Left
                    '    .MappingName = dtPatientDetails.Columns("ICD9").ColumnName
                    '    .NullText = ""
                    '    .Width = 0.23 * C1dgPatientDetails.Width - 5
                    'End With

                    'dgImaging.TableStyles.Clear()
                    'Dim grdTableStyle As New clsDataGridTableStyle(dtPatientDetails.TableName)
                    'grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleID, grdColStyleDate, grdColStyleExamName, grdColStyleFinished, grdColStyleNumResult, grdColStyleDia})
                    'dgImaging.TableStyles.Add(grdTableStyle)
                    '' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PatientRecordViewed, "Patient Orders viewed from DashBoard", gstrLoginName, gstrClientMachineName, gnPatientID)



                Case PatientDetails.ProblemList
                    'C1dgPatientDetails.Visible = True
                    'dgPatientDetails.Visible = False
                    'trvPatientDetails.Visible = False
                    Call Fill_ProblemList()
                Case PatientDetails.History
                    'C1dgPatientDetails.Visible = True
                    'dgPatientDetails.Visible = False

                    '''' Fill Patient History
                    Call Fill_PatientHistory(gnPatientID)
                Case PatientDetails.Procedures
                    FillCPTTreeView()
            End Select


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub FillExamProviderCombo()
        Dim conn As New SqlConnection
        Dim cmd As SqlCommand
        Dim otable As New DataTable
        Try
            cmd = New SqlCommand
            conn.ConnectionString = GetConnectionString()
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            Dim strSelect As String = "select nProviderID,isnull(sFirstName,'') + ' ' + isnull(sLastName,'') as Name from Provider_MST"
            cmd.CommandText = strSelect

            conn.Open()
            otable.Load(cmd.ExecuteReader)
            Dim r As DataRow
            r = otable.NewRow
            r.Item("Name") = "All"
            r.Item("nProviderID") = 0
            otable.Rows.InsertAt(r, 0)

            Dim strProviderName As String = ""
            ' cmbExamProvider.Items.Clear()
            cmbExamProvider.DataSource = otable
            cmbExamProvider.DisplayMember = otable.Columns("Name").ToString()
            cmbExamProvider.ValueMember = otable.Columns("nProviderID").ToString()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.Provider, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally

            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try


    End Sub
    Private Sub FillExamDMS()
        Try
            If blnIsExam Then



                Dim dtPatientDetails As DataTable
                Dim objPatientDetail As New clsPatientDetails
                If m_ExamFilter Then
                    dtPatientDetails = objPatientDetail.Fill_PastExams(gnPatientID, cmbExamProvider.SelectedValue, dtpFrom.Value, dtpTo.Value, cmbExamtype.SelectedValue)
                Else
                    dtPatientDetails = objPatientDetail.Fill_PastExams(gnPatientID)
                End If

                'dgExams = Nothing
                dgExams.Enabled = False
                dgExams.DataSource = dtPatientDetails.DefaultView
                dgExams.Enabled = True
                dgExams.TableStyles.Clear()
                Dim grdColStyleExamID As New DataGridTextBoxColumn

                With grdColStyleExamID
                    .HeaderText = "Exam ID"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientDetails.Columns(0).ColumnName
                    .NullText = ""
                    .Width = 0
                End With
                Dim grdColStyleVisitID As New DataGridTextBoxColumn
                With grdColStyleVisitID
                    .HeaderText = "Visit ID"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientDetails.Columns(1).ColumnName
                    .NullText = ""
                    .Width = 0
                End With

                Dim grdColStyleDate As New DataGridTextBoxColumn

                With grdColStyleDate
                    .HeaderText = "DOS"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientDetails.Columns(2).ColumnName
                    .NullText = ""
                    .Width = 0.1 * C1dgPatientDetails.Width
                End With

                Dim grdColStyleExamName As New DataGridTextBoxColumn
                With grdColStyleExamName
                    .HeaderText = "Exam Name"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientDetails.Columns(3).ColumnName
                    .NullText = ""
                    .Width = 0.32 * C1dgPatientDetails.Width
                End With

                Dim grdColStyleFinished As New DataGridTextBoxColumn
                With grdColStyleFinished
                    .HeaderText = "Finished"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientDetails.Columns(4).ColumnName
                    .NullText = ""
                    .Width = 0.1 * C1dgPatientDetails.Width - 5
                End With

                Dim grdColStyleProvider As New DataGridTextBoxColumn
                With grdColStyleProvider
                    .HeaderText = "Provider"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientDetails.Columns("ProviderName").ColumnName
                    .NullText = ""
                    .Width = 0.18 * C1dgPatientDetails.Width - 5
                End With

                Dim grdColStyleReview As New DataGridTextBoxColumn
                With grdColStyleReview
                    .HeaderText = "Reviewed By"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientDetails.Columns("ReviewedBy").ColumnName
                    .NullText = ""
                    .Width = 0.3 * C1dgPatientDetails.Width - 5
                End With

                dgExams.TableStyles.Clear()
                Dim grdTableStyle As New clsDataGridTableStyle(dtPatientDetails.TableName)
                grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleExamID, grdColStyleVisitID, grdColStyleDate, grdColStyleExamName, grdColStyleFinished, grdColStyleProvider, grdColStyleReview})
                dgExams.TableStyles.Add(grdTableStyle)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient Past Exam viewed from DashBoard", gloAuditTrail.ActivityOutCome.Success)
            Else
                Fill_PatientSacnedDocuments(gnPatientID)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub Fill_ProblemList()
        Dim dtProblemList As DataTable
        Dim objProblemList As New clsPatientProblemList
        dtProblemList = objProblemList.Fill_ProblemLists(gnPatientID)
        objProblemList = Nothing

        'dgPatientDetails.Visible = False

        With c1ProblemList
            .Visible = True
            .BringToFront()
            .Cols.Count = 8
            .Rows.Count = 1
            '' Set Fixed Rows
            .SetData(0, 0, "ProblemID")
            .SetData(0, 1, "DOS")
            .SetData(0, 2, "Chief Complaint")
            .SetData(0, 3, "Diagnosis")
            .SetData(0, 4, "Prescription")
            .SetData(0, 5, "VisitID")
            .SetData(0, 6, "Status")
            .SetData(0, 7, "User")
            '''' 
            .Cols(0).Width = 0
            .Cols(1).Width = .Width * 0.1
            .Cols(2).Width = .Width * 0.22
            .Cols(3).Width = .Width * 0.4
            .Cols(4).Width = .Width * 0.5
            .Cols(5).Width = 0
            .Cols(6).Width = .Width * 0.09
            .Cols(7).Width = .Width * 0.09
            '''' 
            .Cols(0).TextAlign = TextAlignEnum.LeftCenter
            .Cols(1).TextAlign = TextAlignEnum.LeftCenter
            .Cols(2).TextAlign = TextAlignEnum.LeftCenter
            .Cols(3).TextAlign = TextAlignEnum.LeftCenter
            .Cols(4).TextAlign = TextAlignEnum.LeftCenter
            .Cols(5).TextAlign = TextAlignEnum.LeftCenter
            .Cols(6).TextAlign = TextAlignEnum.LeftCenter
            .Cols(7).TextAlign = TextAlignEnum.LeftCenter
            ' ''
            If IsNothing(dtProblemList) = False Then
                For i As Int16 = 0 To dtProblemList.Rows.Count - 1
                    Dim forecolor As Color
                    Dim backcolor As Color
                    Dim status As String = ""
                    If dtProblemList.Rows(i)("Status") = frmProblemList.Status.Active Then
                        forecolor = Color.Red
                        ' backcolor = Color.White
                        status = "Active"
                    ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Resolved Then
                        forecolor = Color.Green
                        ' backcolor = Color.White
                        status = "Resolved"
                    ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Inactive Then
                        forecolor = Color.Blue
                        ' backcolor = Color.White
                        status = "Inactive"
                    ElseIf dtProblemList.Rows(i)("Status") = frmProblemList.Status.Chronic Then
                        forecolor = Color.Black
                        ' backcolor = Color.White
                        status = "Chronic"
                    End If
                    Dim r As C1.Win.C1FlexGrid.Row
                    r = .Rows.Add()
                    r.StyleNew.ForeColor = forecolor
                    '  r.StyleNew.BackColor = backcolor
                    r.Height = 20

                    .SetData(r.Index, 0, dtProblemList.Rows(i)("nProblemID"))
                    .SetData(r.Index, 1, dtProblemList.Rows(i)("dtDOS"))
                    .SetData(r.Index, 2, dtProblemList.Rows(i)("Complaint"))
                    .SetData(r.Index, 3, dtProblemList.Rows(i)("Diagnosis"))
                    .SetData(r.Index, 4, dtProblemList.Rows(i)("Prescription"))
                    .SetData(r.Index, 5, dtProblemList.Rows(i)("VisitID"))
                    .SetData(r.Index, 6, status)
                    .SetData(r.Index, 7, dtProblemList.Rows(i)("UserName"))

                Next
            End If
            oSearchProblemListCtl.IntialiseDatatable(dtProblemList)
        End With
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient Past Exam viewed from DashBoard", gloAuditTrail.ActivityOutCome.Success)
    End Sub
    Private Sub Fill_PatientHistory(ByVal PatientID As Long)
        Dim objDashBoard As New clsDoctorsDashBoard
        Dim dt As New DataTable
        Dim strdetails As String

        'If gnVisitID = 0 Then
        gnVisitID = GetVisitID(Now)
        'End If
        If gnVisitID > 0 Then
            '''' To Check if Current History Exists
            dt = objDashBoard.Fill_History(PatientID, gnVisitID, 0)
        End If


        If dt.Rows.Count > 0 Then
            '''' History Exists for Current Date

        Else
            '''' If History is Not Exist For Current Date then Check for the Previous Date
            dt = objDashBoard.Fill_History(PatientID, gnVisitID, 1)
            'dt(0) = VisitID
            'dt(1) = VisitDate
            If dt.Rows.Count > 0 Then
                '' If there Exist a Visit of History for Previous Date Then 
                '''' Get the History for that Date
                dt = objDashBoard.Fill_History(PatientID, dt.Rows(0)(0), 2)
            Else
            End If
        End If

        Fill_History(dt)
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient history viewed from DashBoard", gloAuditTrail.ActivityOutCome.Success)
    End Sub
    Private Sub Fill_History(ByVal dt As DataTable)
        ''sHistoryCategory, sHistoryItem, sComments, nVisitID, dtVisitDate, nDrugID
        Try

            With C1dgPatientDetails

                .AllowSorting = AllowSortingEnum.None
                .Visible = True
                .BringToFront()
                .Cols.Count = 10
                .Rows.Count = 1
                '' Set Fixed Rows
                .SetData(0, 0, "VisitID")
                .Cols(0).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 1, "Category_Hidden")
                .Cols(1).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 2, "Visit Date_Hidden")
                .Cols(2).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 3, "Visit Date")
                .Cols(3).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 4, "Category")
                .Cols(4).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 5, "Item")
                .Cols(5).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 6, "Comments")
                .Cols(6).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 7, "Reaction")
                .Cols(7).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 8, "Active")
                .Cols(8).TextAlign = TextAlignEnum.GeneralCenter
                .SetData(0, 9, "DrugID")
                .Cols(9).TextAlign = TextAlignEnum.GeneralCenter

                '''' 
                '(0, "VisitID")(1, "Category_Hidden")(2, "Visit Date_Hidden")(3, "Visit Date")(4, "Category")
                '(5, "History")(6, "Comments")(7,"Reaction")(8,"Status")(9, "DrugID")
                .Cols(0).Width = .Width * 0
                .Cols(1).Width = .Width * 0
                .Cols(2).Width = .Width * 0
                .Cols(3).Width = .Width * 0.16
                .Cols(4).Width = .Width * 0.19
                .Cols(5).Width = .Width * 0.22
                .Cols(6).Width = .Width * 0.23
                .Cols(7).Width = .Width * 0.11
                .Cols(8).Width = .Width * 0.06
                .Cols(9).Width = 0

                '''' 
                .BeginInit()
                If IsNothing(dt) = False Then
                    For i As Int16 = 0 To dt.Rows.Count - 1
                        'p Dim r As C1.Win.C1FlexGrid.Row
                        'p r = .Rows.Add()
                        'r.StyleNew.ForeColor = forecolor
                        '  r.StyleNew.BackColor = backcolor
                        'p r.Height = 20
                        'Pramod start
                        '.SetData(r.Index, 0, dt.Rows(i)("nVisitID"))
                        '.SetData(r.Index, 1, dt.Rows(i)("sHistoryCategory"))
                        '.SetData(r.Index, 2, dt.Rows(i)("dtVisitDate"))
                        '.SetData(r.Index, 3, dt.Rows(i)("sHistoryCategory"))
                        '.SetData(r.Index, 4, dt.Rows(i)("sHistoryItem"))
                        '.SetData(r.Index, 5, dt.Rows(i)("sComments"))
                        '.SetData(r.Index, 6, dt.Rows(i)("nDrugID"))
                        'Pramod END 
                        ''''''''''''''''Pramod 

                        Dim _Row As Integer = 0
                        Dim _tempID As Long
                        For j As Int16 = 1 To .Rows.Count - 1
                            If .GetData(j, 1) = dt.Rows(i)("sHistoryCategory") Then

                                '' TO Insert the New Item At the END of the CAtegory
                                Try
                                    If .GetData(j, 1) <> .GetData(j + 1, 1) Then
                                        '''' If The Current Category ID Is Not Matchs with the thw Category Name  at Next ROW 
                                        '' Then Add new Row at Just After the Current Row i.e At the END of the Category
                                        .Rows.Insert(j + 1)
                                        _Row = j + 1
                                        .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                                        Exit For
                                    End If
                                Catch ex As Exception
                                    '''' If The System Does Not Get the ROW At (i+1) Position then it Throws the Exception
                                    '' i.e we ahve to add the Row at the End 
                                    .Rows.Insert(j + 1)
                                    _Row = j + 1
                                    .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                                    Exit For
                                End Try
                            End If
                        Next
                        '(0, "VisitID")(1, "Category_Hidden")(2, "Visit Date_Hidden")(3, "Visit Date")(4, "Category")
                        '(5, "History")(6, "Comments")(7,"Reaction")(8,"Status")(9, "DrugID")
                        If _Row = 0 Then ''  Category Is Not exists
                            .Rows.Add()
                            _Row = .Rows.Count - 1
                            .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                            .SetData(_Row, 1, dt.Rows(i)("sHistoryCategory"))
                            .SetData(_Row, 2, dt.Rows(i)("dtVisitDate"))
                            If _Row = 1 Then
                                .SetData(_Row, 3, dt.Rows(i)("dtVisitDate"))
                            End If
                            .SetData(_Row, 4, dt.Rows(i)("sHistoryCategory"))
                            .Rows.Insert(_Row + 1)
                            _Row = _Row + 1
                        End If
                        '(0, "VisitID")(1, "Category_Hidden")(2, "Visit Date_Hidden")(3, "Visit Date")(4, "Category")
                        '(5, "History")(6, "Comments")(7,"Reaction")(8,"Status")(9, "DrugID")
                        .SetData(_Row, 0, dt.Rows(i)("nVisitID"))
                        .SetData(_Row, 1, dt.Rows(i)("sHistoryCategory"))
                        .SetData(_Row, 2, dt.Rows(i)("dtVisitDate"))
                        .SetData(_Row, 5, dt.Rows(i)("sHistoryItem"))
                        .SetData(_Row, 6, dt.Rows(i)("sComments"))
                        .SetData(_Row, 7, "")

                        Dim cStyle As C1.Win.C1FlexGrid.CellStyle
                        Dim rgReaction As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, 7, _Row, 7)
                        Dim rgActive As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_Row, 8, _Row, 8)
                        ''If the category is allergy then insert combox and checkbox in flexgrid 

                        If InStr(dt.Rows(i)("sHistoryCategory"), "Allerg", CompareMethod.Text) = 1 Then
                            Dim strReaction As String = ""
                            Dim strActive As String = ""
                            If dt.Rows(i)("sReaction") <> "" Then
                                Dim arr() As String 'Srting Array
                                arr = Split(dt.Rows(i)("sReaction"), "|")
                                strReaction = arr.GetValue(0)
                                strActive = arr.GetValue(1)
                            End If

                            Dim strReactions As String = " "
                            Dim objclsPatientHistory As New clsPatientHistory

                            Dim dtReaction As DataTable
                            dtReaction = objclsPatientHistory.GetAllCategory("Reaction")

                            If IsNothing(dtReaction) = False Then
                                For k As Int16 = 0 To dtReaction.Rows.Count - 1
                                    strReactions = strReactions & "|" & dtReaction.Rows(k)(1)
                                Next
                            End If

                            cStyle = .Styles.Add("Reaction")
                            cStyle.ComboList = strReactions


                            rgReaction.Style = cStyle
                            rgActive.StyleNew.DataType = GetType(Boolean)
                            rgActive.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                            rgActive.StyleNew.ImageAlign = ImageAlignEnum.CenterCenter

                            .SetData(_Row, 7, strReaction)

                            If strActive = "Active" Then
                                .SetCellCheck(_Row, 8, CheckEnum.Checked)
                            End If


                            'count = _Row
                            'If dt.Rows.Count > 0 Then
                            'cmbReactions.SelectedIndex = 0
                            'End If

                            objclsPatientHistory = Nothing
                        Else
                            rgReaction.Style = Nothing
                            rgActive.Style = Nothing

                        End If

                        .SetData(_Row, 9, dt.Rows(i)("nDrugID"))
                        '''''''''''''''''''' End Pramod
                        .Row = _Row
                    Next
                End If
                .EndInit()
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tbSummary_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbSummary.SelectedIndexChanged
        Select Case tbSummary.SelectedIndex


            Case 1
                SelectedPatientDetail = PatientDetails.ProblemList
                ShowPatientDetails()
            Case 2
                SelectedPatientDetail = PatientDetails.Medication
                ShowPatientDetails()
            Case 3
                SelectedPatientDetail = PatientDetails.History
                ShowPatientDetails()
            Case 4
                SelectedPatientDetail = PatientDetails.Procedures
                ShowPatientDetails()
            Case 6
                SelectedPatientDetail = PatientDetails.Orders
                ShowPatientDetails()
            Case 5
                GloUC_TransactionHistory1.LoadPreviousLabs(gnPatientID, DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"))
                'If pnlTransactionHistory.Visible = False Then
                '    pnlTransactionHistory.Visible = True
                '    spltTransactionHistory.Visible = True
                GloUC_TransactionHistory1.DesignTestGrid()
                GloUC_TransactionHistory1.SetDataByDate(DateTime.Now.Date, DateTime.Now.Date)
                GloUC_TransactionHistory1.cmbCriteria.Text = "Date"
                'Else

                'pnlTransactionHistory.Visible = False
                'spltTransactionHistory.Visible = False
                'GloUC_TransactionHistory.DesignTestGrid()
                'GloUC_TransactionHistory.cmbCriteria.Text = ""
                'GloUC_TransactionHistory.dtpFrom.Text = DateTime.Now.Date
                'GloUC_TransactionHistory.dtpToDate.Text = DateTime.Now.Date
                'GloUC_TransactionHistory.cmbType.Text = ""
                'GloUC_TransactionHistory.cmbType.Items.Clear()
                'End If
        End Select
    End Sub
    Private Sub tbSummary_TabStopChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbSummary.TabStopChanged

    End Sub

    Private Sub tbExamDMS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbExamDMS.SelectedIndexChanged
        Select Case tbExamDMS.SelectedIndex
            Case 0
                blnIsExam = True
                FillExamDMS()
            Case 1
                blnIsExam = False
                FillExamDMS()
        End Select
    End Sub
    Private Sub Fill_PatientSacnedDocuments(ByVal PatientID As Long)
        Dim oCategories As New gloEDocumentV3.Common.Categories()
        Dim oList As New gloEDocumentV3.eDocManager.eDocGetList()
        Dim oDocuments As New gloEDocumentV3.Document.BaseDocuments()

        Try
           
            '' 'Design Category Grid

            DesignCategorisedDocument(c1CategorisedDocuments)
           
            With c1CategorisedDocuments

                oCategories = oList.GetCategories(gClinicID)
                If Not oCategories Is Nothing Then
                    For i As Int16 = 0 To oCategories.Count - 1
                        oDocuments = New gloEDocumentV3.Document.BaseDocuments()
                        oDocuments = oList.GetBaseDocuments(gnPatientID, oCategories(i).CategoryName, gClinicID)

                        If Not oDocuments Is Nothing Then
                            For k As Int16 = 0 To oDocuments.Count - 1
                                'oDocuments(d).ContainerID
                                'oDocuments(d).DocumentID
                                'oDocuments(d).Category
                                'oDocuments(d).Year
                                'oDocuments(d).Month
                                'oDocuments(d).ClinicID
                                'oDocuments(d).DocumentName

                                .Rows.Add()
                                .Cols(COL_View_DocumentName).TextAlign = TextAlignEnum.LeftCenter
                                Dim rgStyle As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, COL_View_DocumentName, .Rows.Count - 1, COL_View_DocumentName)
                                rgStyle.StyleNew.DataType = GetType(String)
                                .SetData(.Rows.Count - 1, COL_View_DocumentName, oDocuments(k).DocumentName)
                                .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEMACHINE, "")  ' Source Machine
                                .SetData(.Rows.Count - 1, COL_D_CAT_SYSTEMFOLDER, "")    ' System Folder
                                .SetData(.Rows.Count - 1, COL_D_CAT_CONTAINER, oDocuments(k).EContainers(0).EContainerID)          ' Container
                                .SetData(.Rows.Count - 1, COL_View_Category, oDocuments(k).Category)            ' Category
                                .SetData(.Rows.Count - 1, COL_D_CAT_PATIENTID, oDocuments(k).PatientID)          ' Patient ID
                                .SetData(.Rows.Count - 1, COL_D_CAT_YEAR, oDocuments(k).Year)                    ' Year
                                .SetData(.Rows.Count - 1, COL_D_CAT_MONTH, oDocuments(k).Month)                  ' Month
                                .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEBIN, "")          ' Source Bin
                                .SetData(.Rows.Count - 1, COL_D_CAT_INUSED, "")                ' In Used
                                .SetData(.Rows.Count - 1, COL_D_CAT_USEDMACHINE, "")      ' Used Machine
                                .SetData(.Rows.Count - 1, COL_D_CAT_USEDTYPE, "")            ' Used Type
                                .SetData(.Rows.Count - 1, COL_D_CAT_PATH, "")                    ' Path
                                .SetData(.Rows.Count - 1, COL_D_CAT_COLTYPE, CType(enumColType.Document, Integer))
                                .SetData(.Rows.Count - 1, COL_D_CAT_FILENAME, oDocuments(k).EDocumentID) '' DocumentID
                                .SetData(.Rows.Count - 1, COL_D_CAT_MACHINEID, "")
                                .SetData(.Rows.Count - 1, COL_D_CAT_VERSIONNO, "")

                                If oDocuments(k).HasNote = True Then
                                    .SetCellImage(.Rows.Count - 1, COL_View_NOTEFLAG, oImage_D.Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                End If
                                If oDocuments(k).IsAcknowledge = True Then
                                    .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 1)
                                    '  .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File")
                                    .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, oImage_D.Img_Reviwed.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                    .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                                Else
                                    .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 0)
                                    ' .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File_UnReviwed")
                                    .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, oImage_D.Img_Blanck.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                    .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                                End If
                                'nmonths = nmonths + 1
                            Next
                        End If
                        oDocuments.Dispose()
                    Next
                End If

            End With

            ' blnFormLoad = False

            If c1CategorisedDocuments.Rows.Count > 1 Then
                pnlScannedDocs.Visible = True
                'c1CategorisedDocuments.Visible = True
            Else
                'c1CategorisedDocuments.Visible = False
                pnlScannedDocs.Visible = False
                'lbl_ViewDocuments.Visible = True
            End If


        Catch objError As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, objError.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ' blnFormLoad = False
            MessageBox.Show(objError.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        Finally

        End Try
    End Sub
    Private Sub SearchDMS()
        Dim oCategories As New gloEDocumentV3.Common.Categories()
        Dim oList As New gloEDocumentV3.eDocManager.eDocGetList()

        
        Dim otable As DataTable
        Try

            '' 'Design Category Grid

            DesignCategorisedDocument(c1CategorisedDocuments)
            With c1CategorisedDocuments
                Dim searchcriteria As String = txtSearchCriteria.Text.Trim
                oCategories = oList.GetCategories(gClinicID)
                If Not oCategories Is Nothing Then
                    For i As Int16 = 0 To oCategories.Count - 1

                        Dim categoryname As String = oCategories(i).CategoryName
                        Dim clinicid As Int64 = gClinicID
                        Dim conn As SqlConnection
                        Dim cmd As SqlCommand
                        Dim strSQL As String = ""
                        If txtSearchCriteria.Text.Trim <> "" Then
                            Try
                                conn = New SqlConnection
                                conn.ConnectionString = GetConnectionString()
                                conn.Open()
                                cmd = New SqlCommand
                                cmd.CommandType = CommandType.Text
                                Select Case cmbSearch.Text
                                    Case "Notes"
                                        strSQL = "SELECT ed.eContainerID,ed.eDocumentID,ed.DocumentName,ed.Category," _
                                                & " ed.PatientID,Year,Month,ed.PageCounts,ed.CreatedDateTime,ed.IsAcknowledge,ed.HasNote," _
                                                & " ed.ClinicID FROM eDocument_Details ed inner join eDocument_Notes edn " _
                                                & " on ed.eDocumentID=edn.eDocumentID " _
                                                & " WHERE ed.PatientID = " & gnPatientID & " and ed.category='" & categoryname & "' and ed.clinicid= " & clinicid & " AND ed.eContainerID <> 0 AND ed.eDocumentID <> 0 AND " _
                                                & " ed.DocumentName IS NOT NULL AND ed.Category IS NOT NULL " _
                                                & " AND ed.PatientID IS NOT NULL AND Year IS NOT NULL AND Month IS NOT NULL AND ed.PageCounts IS NOT NULL " _
                                                & " and edn.notedescription like '%" & searchcriteria & "%'" _
                                                & " ORDER BY Year,Month,ed.DocumentName,ed.CreatedDateTime desc"
                                    Case "User Tag"
                                        strSQL = "SELECT ed.eContainerID,ed.eDocumentID,ed.DocumentName,ed.Category," _
                                                & " ed.PatientID,Year,Month,ed.PageCounts,ed.CreatedDateTime,ed.IsAcknowledge,ed.HasNote," _
                                                & " ed.ClinicID FROM eDocument_Details ed inner join eDocument_UserTags edn " _
                                                & " on ed.eDocumentID=edn.eDocumentID " _
                                                & " WHERE ed.PatientID =  " & gnPatientID & " and ed.category='" & categoryname & "' and ed.clinicid= " & clinicid & " AND ed.eContainerID <> 0 AND ed.eDocumentID <> 0 AND " _
                                                & " ed.DocumentName IS NOT NULL AND ed.Category IS NOT NULL " _
                                                & " AND ed.PatientID IS NOT NULL AND Year IS NOT NULL AND Month IS NOT NULL AND ed.PageCounts IS NOT NULL " _
                                                & " and edn.UserTagDescription like '%" & searchcriteria & "%'" _
                                                & " ORDER BY Year,Month,ed.DocumentName,ed.CreatedDateTime desc"
                                    Case "Acknowledge"
                                        strSQL = "SELECT ed.eContainerID,ed.eDocumentID,ed.DocumentName,ed.Category," _
                                               & " ed.PatientID,Year,Month,ed.PageCounts,ed.CreatedDateTime,ed.IsAcknowledge,ed.HasNote," _
                                               & " ed.ClinicID FROM eDocument_Details ed inner join eDocument_Acknowledge edn " _
                                               & " on ed.eDocumentID=edn.eDocumentID " _
                                               & " WHERE ed.PatientID =  " & gnPatientID & " and ed.category='" & categoryname & "' and ed.clinicid= " & clinicid & " AND ed.eContainerID <> 0 AND ed.eDocumentID <> 0 AND " _
                                               & " ed.DocumentName IS NOT NULL AND ed.Category IS NOT NULL " _
                                               & " AND ed.PatientID IS NOT NULL AND Year IS NOT NULL AND Month IS NOT NULL AND ed.PageCounts IS NOT NULL " _
                                               & " and edn.AcknowledgeDescription like '%" & searchcriteria & "%'" _
                                               & " ORDER BY Year,Month,ed.DocumentName,ed.CreatedDateTime desc"
                                End Select
                                cmd.Connection = conn
                                cmd.CommandText = strSQL
                                otable = New DataTable
                                otable.Load(cmd.ExecuteReader)
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                            End Try

                            If Not otable Is Nothing Then
                                For k As Int16 = 0 To otable.Rows.Count - 1

                                    .Rows.Add()
                                    .Cols(COL_View_DocumentName).TextAlign = TextAlignEnum.LeftCenter
                                    Dim rgStyle As C1.Win.C1FlexGrid.CellRange = .GetCellRange(.Rows.Count - 1, COL_View_DocumentName, .Rows.Count - 1, COL_View_DocumentName)
                                    rgStyle.StyleNew.DataType = GetType(String)
                                    .SetData(.Rows.Count - 1, COL_View_DocumentName, otable.Rows(k)(2).ToString)
                                    .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEMACHINE, "")  ' Source Machine
                                    .SetData(.Rows.Count - 1, COL_D_CAT_SYSTEMFOLDER, "")    ' System Folder
                                    .SetData(.Rows.Count - 1, COL_D_CAT_CONTAINER, otable.Rows(k)(0))          ' Container
                                    .SetData(.Rows.Count - 1, COL_View_Category, otable.Rows(k)(3))            ' Category
                                    .SetData(.Rows.Count - 1, COL_D_CAT_PATIENTID, otable.Rows(k)(4))          ' Patient ID
                                    .SetData(.Rows.Count - 1, COL_D_CAT_YEAR, otable.Rows(k)(5))                    ' Year
                                    .SetData(.Rows.Count - 1, COL_D_CAT_MONTH, otable.Rows(k)(6))                  ' Month
                                    .SetData(.Rows.Count - 1, COL_D_CAT_SOURCEBIN, "")          ' Source Bin
                                    .SetData(.Rows.Count - 1, COL_D_CAT_INUSED, "")                ' In Used
                                    .SetData(.Rows.Count - 1, COL_D_CAT_USEDMACHINE, "")      ' Used Machine
                                    .SetData(.Rows.Count - 1, COL_D_CAT_USEDTYPE, "")            ' Used Type
                                    .SetData(.Rows.Count - 1, COL_D_CAT_PATH, "")                    ' Path
                                    '.SetData(.Rows.Count - 1, COL_D_CAT_COLTYPE, CType(enumColType.Document, Integer))
                                    .SetData(.Rows.Count - 1, COL_D_CAT_FILENAME, CType(otable.Rows(k)(1), Int64)) '' DocumentID
                                    .SetData(.Rows.Count - 1, COL_D_CAT_MACHINEID, "")
                                    .SetData(.Rows.Count - 1, COL_D_CAT_VERSIONNO, "")

                                    'If otable.Rows(k)(10) = True Then
                                    '    .SetCellImage(.Rows.Count - 1, COL_View_NOTEFLAG, oImage_D.Img_Note.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                    'End If
                                    'If otable.Rows(k)(9) = True Then
                                    '    .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 1)
                                    '    '  .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File")
                                    '    .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, oImage_D.Img_Reviwed.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                    '    .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                                    'Else
                                    '    .SetData(.Rows.Count - 1, COL_D_CAT_ISREVIWED, 0)
                                    '    ' .Rows(.Rows.Count - 1).Style = FillControl.Styles("CS_File_UnReviwed")
                                    '    .SetCellImage(.Rows.Count - 1, COL_View_REVIWEDFLAG, oImage_D.Img_Blanck.Image)  'Image.FromFile("D:\Vinayak M3XP\gloEMR\gloEMR\bin\Images\DMS\Flag1.JPG"))
                                    '    .Cols(COL_View_REVIWEDFLAG).ImageAlign = ImageAlignEnum.CenterCenter
                                    'End If
                                    'nmonths = nmonths + 1
                                Next
                            End If
                        Else

                            Fill_PatientSacnedDocuments(gnPatientID)
                        End If
                       

                    Next
                End If
            End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        
    End Sub
    Private Sub DesignCategorisedDocument(ByVal DesignControl As C1FlexGrid)
        '''' change by Pramod to view document in bydefault without month view on 10182007
        'If DMS_ScanDoc_MonthView = False Then
        With DesignControl
            .AllowSorting = AllowSortingEnum.None
            .Visible = True
            .BringToFront()
            .Cols.Count = Col_view_Count
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 0


            .Cols(COL_D_CAT_CATEGORY).Width = 0                 ' ID
            .Cols(COL_D_CAT_NAME).Width = 0    '225             ' Name
            .Cols(COL_D_CAT_NOTEFLAG).Width = 0 '15          ' Note Flag
            .Cols(COL_D_CAT_EXTRAFLAG).Width = 0          ' Extra Col
            .Cols(COL_D_CAT_SOURCEMACHINE).Width = 0      ' Source Machine
            .Cols(COL_D_CAT_SYSTEMFOLDER).Width = 0       ' System Folder
            .Cols(COL_D_CAT_CONTAINER).Width = 0          ' Container
            .Cols(COL_D_CAT_CATEGORY).Width = 0           ' Category
            .Cols(COL_D_CAT_PATIENTID).Width = 0          ' Patient ID
            .Cols(COL_D_CAT_YEAR).Width = 0               ' Year
            .Cols(COL_D_CAT_MONTH).Width = 0              ' Month
            .Cols(COL_D_CAT_SOURCEBIN).Width = 0          ' Source Bin
            .Cols(COL_D_CAT_INUSED).Width = 0             ' In Used
            .Cols(COL_D_CAT_USEDMACHINE).Width = 0        ' Used Machine
            .Cols(COL_D_CAT_USEDTYPE).Width = 0           ' Used Type
            .Cols(COL_D_CAT_PATH).Width = 0               ' Path
            .Cols(COL_D_CAT_FILENAME).Width = 0           ' File Name
            .Cols(COL_D_CAT_MACHINEID).Width = 0               ' Machine ID
            .Cols(COL_D_CAT_COLTYPE).Width = 0           ' Col Type
            .Cols(COL_D_CAT_VERSIONNO).Width = 0           ' Version No
            .Cols(COL_D_CAT_MACHINEID).Width = 0               ' Machine ID
            .Cols(COL_D_CAT_COLTYPE).Width = 0           ' Col Type
            .Cols(COL_D_CAT_REVIWEDFLAG).Width = 0 '15           ' Col Type

            ''Pramod 06162007 View Documents START
            .Cols(COL_View_CategoryHidden).Width = .Width * 0
            .Cols(COL_View_Category).Width = .Width * 0.47  ' Category Name
            .Cols(COL_View_Month).Width = .Width * 0          'Month
            .Cols(COL_View_DocumentName).Width = .Width * 0.47   'Document Name 
            .Cols(COL_View_NOTEFLAG).Width = 15  'Acnowlegements 
            .Cols(COL_View_REVIWEDFLAG).Width = 15           'review
            ''Pramod 06162007 View Documents END

            .Cols(COL_D_CAT_ID).Visible = False           ' ID
            .Cols(COL_D_CAT_NAME).Visible = False 'True          ' Name
            .Cols(COL_D_CAT_NOTEFLAG).Visible = False 'True      ' Note Flag
            .Cols(COL_D_CAT_EXTRAFLAG).Visible = False    ' Extra Col
            .Cols(COL_D_CAT_SOURCEMACHINE).Visible = False ' Source Machine
            .Cols(COL_D_CAT_SYSTEMFOLDER).Visible = False ' System Folder
            .Cols(COL_D_CAT_CONTAINER).Visible = False    ' Container
            .Cols(COL_D_CAT_CATEGORY).Visible = False     ' Category
            .Cols(COL_D_CAT_PATIENTID).Visible = False    ' Patient ID
            .Cols(COL_D_CAT_YEAR).Visible = False         ' Year
            .Cols(COL_D_CAT_MONTH).Visible = False        ' Month
            .Cols(COL_D_CAT_SOURCEBIN).Visible = False    ' Source Bin
            .Cols(COL_D_CAT_INUSED).Visible = False       ' In Used
            .Cols(COL_D_CAT_USEDMACHINE).Visible = False  ' Used Machine
            .Cols(COL_D_CAT_USEDTYPE).Visible = False     ' Used Type
            .Cols(COL_D_CAT_PATH).Visible = False         ' Path
            .Cols(COL_D_CAT_FILENAME).Visible = False           ' File Name
            .Cols(COL_D_CAT_MACHINEID).Visible = False               ' Machine ID
            .Cols(COL_D_CAT_COLTYPE).Visible = False      ' Col Type
            .Cols(COL_D_CAT_VERSIONNO).Visible = False      ' Version No
            .Cols(COL_D_CAT_VERSIONNO).Visible = False      ' Col Type
            .Cols(COL_D_CAT_ISREVIWED).Visible = False      ' Col Type
            .Cols(COL_D_CAT_REVIWEDFLAG).Visible = False ' True      ' Col Type

            ''Pramod 06162007 View Documents START
            .Cols(COL_View_CategoryHidden).Visible = False

            .Cols(COL_View_Category).Visible = True       ' Category Name
            .Cols(COL_View_Category).AllowEditing = False
            .Cols(COL_View_Category).DataType = GetType(String)
            .Cols(COL_View_Category).TextAlign = TextAlignEnum.LeftCenter


            .Cols(COL_View_Month).Visible = False          'Month
            .Cols(COL_View_Month).AllowEditing = False

            .Cols(COL_View_DocumentName).Visible = True   'Document Name  
            .Cols(COL_View_DocumentName).AllowEditing = False
            .Cols(COL_View_DocumentName).DataType = GetType(String)
            .Cols(COL_View_DocumentName).TextAlign = TextAlignEnum.LeftCenter



            .Cols(COL_View_NOTEFLAG).Visible = True  'Acnowlegements 
            .Cols(COL_View_NOTEFLAG).AllowEditing = False


            .Cols(COL_View_REVIWEDFLAG).Visible = True           'review
            .Cols(COL_View_REVIWEDFLAG).AllowEditing = False

            ''Pramod 06162007 View Documents END

            '.Cols(COL_CAT_NOTEFLAG).ComboList = "..."

            ''Pramod 06162007 View Documents START
            .SetData(0, COL_View_CategoryHidden, "Category")
            .Cols(COL_View_CategoryHidden).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, COL_View_Category, "Category")
            .Cols(COL_View_Category).TextAlign = TextAlignEnum.LeftCenter
            .SetData(0, COL_View_DocumentName, "Document Name")
            .Cols(COL_View_DocumentName).TextAlign = TextAlignEnum.GeneralCenter
            '.SetData(0, COL_View_NOTEFLAG, "Note")
            '.Cols(COL_View_NOTEFLAG).TextAlign = TextAlignEnum.GeneralCenter
            '.SetData(0, COL_View_REVIWEDFLAG, "Review")
            '.Cols(COL_View_REVIWEDFLAG).TextAlign = TextAlignEnum.GeneralCenter
            ''Pramod 06162007 View Documents END

        End With

    End Sub
    Private Function GetPatientCodefromID(ByVal nPatientID As Long) As String
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Dim strSQL As String
        Dim PatCode As String
        'strSQL = "SELECT nPatientID FROM Patient WHERE  sPatientCode = '" & PatientCode & "'"
        oDB.Connect(GetConnectionString)
        PatCode = oDB.ExecuteQueryScaler("SELECT  sPatientCode FROM Patient where nPatientID = " & nPatientID)

        oDB.Disconnect()
        oDB = Nothing

        Return PatCode
        '
    End Function
    Private Sub dgExams_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgExams.MouseDoubleClick
        Dim ptPoint As Point = New Point(e.X, e.Y)
        Dim htInfo As DataGrid.HitTestInfo = dgPatientDetails.HitTest(ptPoint)
        If Not htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

            If SelectedPatientDetail <> 0 Then
                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                If CheckPatientStatus(gnPatientID) = False Then
                    Exit Sub
                End If
                '''''<><><><><> Check Patient Status <><><><><><>''''

                '' "Past Exams"

                If dgExams.CurrentRowIndex <> -1 Then


                    Dim nPastExamID As Long
                    Dim nVisitID As Long
                    Dim dtDOS As DateTime
                    Dim strExamName As String
                    Dim bIsReviewed As Boolean

                    ''Dim em As System.Windows.Forms.MouseEventArgs
                    nPastExamID = dgExams.Item(dgExams.CurrentRowIndex, 0)
                    nVisitID = dgExams.Item(dgExams.CurrentRowIndex, 1)
                    dtDOS = dgExams.Item(dgExams.CurrentRowIndex, 2)
                    strExamName = dgExams.Item(dgExams.CurrentRowIndex, 3)

                    Dim blnFinished As Boolean
                    If dgExams.Item(dgExams.CurrentRowIndex, 4) = "Yes" Then
                        blnFinished = True
                    Else
                        blnFinished = False
                    End If

                    ShowPastExam(nPastExamID, gnPatientID, nVisitID, dtDOS, strExamName, blnFinished, gstrPatientCode)


                    ' '' To check the Provider Associated for the Exam is same as provider Selected
                    'If Not blnFinished Then
                    '    Dim objExam As New clsPatientExams
                    '    objExam.SetProviderExam(nPastExamID)
                    '    objExam = Nothing
                    'End If

                    'Me.Cursor = Cursors.WaitCursor
                    'Dim frm As New frmPatientExam

                    'With frm
                    '    .Hide()
                    '    .blnModify = True
                    '    .Text = "Past Exams"
                    '    .cmdPastExam_Click(sender, e)

                    '    .PatientID = gnPatientID

                    '    .pnlPastExam.Visible = True
                    '    '.wdPastExam.Visible = False
                    '    .chkShowPreview.Visible = True
                    '    '.pnlPastExamView.Visible = True
                    '    If .OpenPastExam(nPastExamID, nVisitID, dtDOS, strExamName, blnFinished) = True Then
                    '        '''' Hide Tool Bar Mahesh 20070424
                    '        Me.pnlMenu.Visible = False

                    '        '''' User Want to Open Exam
                    '        Me.pnlLeft.Visible = False
                    '        'Me.pnlRights.Visible = False
                    '        Me.Splitter1.Visible = False
                    '        .MdiParent = Me
                    '        .IsPastExam = True
                    '        .Show()
                    '        Dim objAudit As New clsAudit
                    '        objAudit.CreateLog(clsAudit.enmActivityType.Other, "Exam Opened.", gstrLoginName, gstrClientMachineName, gnPatientID, True)
                    '        objAudit = Nothing
                    '    Else
                    '        '''' Show Tool Bar Mahesh 20070424
                    '        Me.pnlMenu.Visible = True

                    '        '''' User Dont Want to Open Exam 
                    '        Me.pnlLeft.Visible = True
                    '        'Me.pnlRights.Visible = True
                    '        Me.Splitter1.Visible = True
                    '    End If
                    '    '.dgExams.CurrentRowIndex = dgPatientDetails.CurrentRowIndex
                    '    '.dgExams_DoubleClick(sender, e)

                    'End With
                    'Me.Cursor = Cursors.Default

                End If

            End If
        End If
    End Sub
    Private Sub ShowPastExam(ByVal ExamID As Long, ByVal PatientId As Int64, ByVal VisitID As Long, ByVal DOS As String, ByVal ExamName As String, ByVal blnFinished As Boolean, Optional ByVal PatientCode As String = "")
        Try

            'gstrPatientCode = PatientCode
            'gnPatientID = GetPatientID(gstrPatientCode)

            gnPatientID = PatientId

            If PatientCode <> "" Then
                gstrPatientCode = PatientCode
            Else
                gstrPatientCode = GetPatientCodefromID(PatientId)
            End If

            'txtSearchPatient.Text = ""
            dgExams.ResetSelectedRows()

            'If ShowDefaultPatientDetails() = False Then
            '    ShowDefaultPatientDetails(gnPatientID)
            'End If
            ' 
            dgPatientDetails.DataSource = Nothing

            If Trim(gstrPatientFirstName) <> "" Then

                '''''<><><><><> Check Patient Status <><><><><><>''''
                ''''' 20070125 -Mahesh 
                If CheckPatientStatus(gnPatientID) = False Then
                    Exit Sub
                End If
                '''''<><><><><> Check Patient Status <><><><><><>''''

                If Not blnFinished Then
                    Dim objExam As New clsPatientExams
                    objExam.SetProviderExam(ExamID)
                    objExam = Nothing
                End If

                Me.Cursor = Cursors.WaitCursor

                Dim frm As New frmPatientExam

                With frm
                    .Hide()
                    .blnModify = True
                    .Text = "Past Exams"
                    Dim sender As Object
                    Dim e As System.EventArgs
                    .cmdPastExam_Click(sender, e)
                    .PatientID = gnPatientID
                    .chkShowPreview.Visible = True
                    .pnlPastExam.Visible = True
                    If (.OpenPastExam(ExamID, VisitID, Convert.ToDateTime(DOS), ExamName.Trim, blnFinished) = True) Then

                       
                        'Me.Splitter1.Visible = False
                        .MdiParent = Me.MdiParent
                        .IsPastExam = True
                        .Show()
                        If .ExamViewMode Then
                            .ViewExam(ExamID)
                        Else
                            .OpenPastExamContents(ExamID, blnFinished)
                        End If

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Patient Past Exam opened.  ", gloAuditTrail.ActivityOutCome.Success)

                    Else
                        'Dim et As New System.Windows.Forms.ToolStripItemClickedEventArgs(tsbtn_PastExam)

                        'ts_PatientDetails_ItemClicked(sender, et)
                    End If
                End With
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MessageBox.Show("Please select the Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub c1CategorisedDocuments_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles c1CategorisedDocuments.DoubleClick
        Try
            If (c1CategorisedDocuments.RowSel) > 0 Then
                If c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_CAT_COLTYPE) = enumColType.Document Then
                    Dim _ContID As Long = 0
                    Dim _DocID As Long = 0
                    Dim _Year As String = ""
                    Try
                        _DocID = Convert.ToInt64(c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_D_CAT_FILENAME).ToString())
                        _Year = c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_D_CAT_YEAR).ToString()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                    End Try

                    If _DocID > 0 And _Year <> "" Then

                        If IsNothing(oShowDocument) Then
                            oShowDocument = New gloEDocumentV3.gloEDocV3Management
                        End If

                        oShowDocument.ShowEDocument(gnPatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, CType(Me.ParentForm, MainMenu), gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewPatientSummary, _DocID)
                        oShowDocument.Dispose()
                        ShowPatientDetails()
                    End If


                    Exit Sub


                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function FillICDCPTMOD(ByVal patientid As Int64) As gloCPTs
        Dim conn As New SqlConnection(GetConnectionString)
        Dim sqladpt As SqlDataAdapter
        Dim sql As SqlCommand

        Dim dtICD9 As New DataTable
        Dim dtCPT As New DataTable
        Dim dtMOD As New DataTable
        Dim CPTCol As New gloCPTs


        Dim _ICD9 As gloICD9
        Dim _CPT As gloCPT
        Dim _Modifier As gloModifier
        conn.Open()

        Try

            Dim nICD9 As Int16
            Dim nCPT As Int16
            Dim nMOD As Int16

            Dim strselecrICD9Qry As String
            Dim strselectCPTQry As String
            Dim strselectMODQry As String

            'Query for selecting ICD9 for current exam 
            'If ExamID = 0 Then
            'strselectCPTQry = "SELECT distinct isnull(i.sCPTCode,'') as sCPTCode,isnull(i.sCPTDescription,'') as sCPTDescription, " _
            '                   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
            '                   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),'ExamID'=p.nexamid ,'Unit' =isnull(nUnit,0) FROM ExamICD9CPT i inner join " _
            '                   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
            '                   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
            '                   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
            '                   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
            '                   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "' and v.npatientid = " & patientid
            strselectCPTQry = "SELECT distinct isnull(i.sCPTCode,'') as sCPTCode,isnull(i.sCPTDescription,'') as sCPTDescription, " _
                              & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
                              & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),'ExamID'=p.nexamid ,'Unit' =isnull(nUnit,0) FROM ExamICD9CPT i inner join " _
                              & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
                              & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
                              & " v.npatientid = " & patientid
            'Else
            'strselectCPTQry = "SELECT distinct isnull(i.sCPTCode,'') as sCPTCode,isnull(i.sCPTDescription,'') as sCPTDescription, " _
            '   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
            '   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),'ExamID'=p.nexamid ,'Unit' =isnull(nUnit,0) FROM ExamICD9CPT i inner join " _
            '   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
            '   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
            '   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
            '   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
            '   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "' and v.npatientid = " & patientid & " and p.nexamid = " & ExamID & ""

            'End If
            sql = New SqlCommand
            sql.Connection = conn
            sql.CommandType = CommandType.Text
            sql.CommandText = strselectCPTQry
            sqladpt = New SqlDataAdapter(sql)

            sqladpt.Fill(dtCPT)
            sql.Dispose()
            sql = Nothing

            With dtCPT
                If IsNothing(dtCPT) = False Then
                    For nCPT = 0 To .Rows.Count - 1
                        If nCPT = 0 Then
                            '_Visit.ProviderDoctor.Code = dtCPT.Rows(0)("ExternalCode")
                            '_Visit.ProviderDoctor.FirstName = dtCPT.Rows(0)("Firstname")
                            '_Visit.ProviderDoctor.MiddleName = dtCPT.Rows(0)("Middlename")
                            '_Visit.ProviderDoctor.LastName = dtCPT.Rows(0)("Lastname")
                        End If
                        '   Dim count As Integer = nCPT + 1
                        If CStr(dtCPT.Rows(nCPT)("sCPTCode")).Trim <> "" Then

                            _CPT = New gloCPT

                            _CPT.Code = dtCPT.Rows(nCPT)("sCPTCode")
                            _CPT.Description = dtCPT.Rows(nCPT)("sCPTDescription")
                            _CPT.ExamID = dtCPT.Rows(nCPT)("ExamId")
                            _CPT.Unit = dtCPT.Rows(nCPT)("Unit")
                            Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTCode")


                            'Query for selecting CPT for current exam
                            'strselecrICD9Qry = "SELECT Distinct isnull(i.sICD9Code,'') as sICD9Code,isnull(i.sICD9Description,'') as sICD9Description, " _
                            '   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
                            '   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,'') FROM ExamICD9CPT i inner join " _
                            '   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
                            '   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
                            '   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
                            '   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
                            '   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "'" _
                            '   & " and v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID
                            strselecrICD9Qry = "SELECT Distinct isnull(i.sICD9Code,'') as sICD9Code,isnull(i.sICD9Description,'') as sICD9Description, " _
                               & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
                               & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,'') FROM ExamICD9CPT i inner join " _
                               & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
                               & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
                               & " v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID

                            dtICD9.Clear()
                            sql = New SqlCommand
                            sql.Connection = conn
                            sql.CommandType = CommandType.Text
                            sql.CommandText = strselecrICD9Qry
                            sqladpt = New SqlDataAdapter(sql)

                            sqladpt.Fill(dtICD9)
                            sql.Dispose()
                            sql = Nothing
                            'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nICD9)("sCPTcode"))

                            With dtICD9
                                If IsNothing(dtICD9) = False Then

                                    For nICD9 = 0 To .Rows.Count - 1

                                        Dim strCurrentICD9 As String = dtICD9.Rows(nICD9)("sICD9Code")
                                        If strCurrentCPT.Trim <> "" Then
                                            _ICD9 = New gloICD9

                                            _ICD9.Code = dtICD9.Rows(nICD9)("sICD9Code")
                                            _ICD9.Description = dtICD9.Rows(nICD9)("sICD9Description")
                                            _CPT.ICD9Col.Add(_ICD9)
                                            _ICD9 = Nothing '' With dtMOD
                                        End If
                                    Next '' For nCPT = 0 To .Rows.Count - 1
                                End If
                            End With '' With dtCPT
                            'Query for selecting Modifier for current exam 
                            'strselectMODQry = "SELECT Distinct isnull(i.sModCode,'') as sModCode,isnull(i.sModDescription,'') as sModDescription, " _
                            '   & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
                            '   & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),isnull(nUnit,0) as nUnit FROM ExamICD9CPT i inner join " _
                            '   & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
                            '   & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
                            '   & " convert(datetime,convert (varchar(50),datepart(mm,v.dtvisitdate)) + '/'+ " _
                            '   & " convert(varchar(50),datepart(dd,v.dtvisitdate))+'/'+  " _
                            '   & " convert(varchar(50),datepart(yy,v.dtvisitdate)))= '" & VisitDate & "'" _
                            '   & " and v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID
                            strselectMODQry = "SELECT Distinct isnull(i.sModCode,'') as sModCode,isnull(i.sModDescription,'') as sModDescription, " _
                            & " 'DateofService'=p.dtDOS,'Firstname'=isnull(pm.sfirstname,''),'Middlename'=isnull(pm.smiddlename,''), " _
                            & " 'Lastname'=isnull(pm.slastname,''),'ExternalCode'=isnull(pm.sexternalcode,''),isnull(nUnit,0) as nUnit FROM ExamICD9CPT i inner join " _
                            & " PatientExams p  on i.nexamid = p.nexamid inner join visits v on p.nvisitid=v.nvisitid inner join " _
                            & " provider_mst pm on v.nproviderid=pm.nproviderid WHERE " _
                            & " v.npatientid = " & patientid & " and i.sCPTCode= '" & strCurrentCPT & "' and p.nexamid= " & _CPT.ExamID

                            dtMOD.Clear()
                            sql = New SqlCommand
                            sql.Connection = conn
                            sql.CommandType = CommandType.Text
                            sql.CommandText = strselectMODQry
                            sqladpt = New SqlDataAdapter(sql)

                            sqladpt.Fill(dtMOD)
                            sql.Dispose()
                            sql = Nothing
                            With dtMOD
                                If IsNothing(dtMOD) = False Then
                                    For nMOD = 0 To .Rows.Count - 1
                                        Dim strCurrentMod As String = dtMOD.Rows(nMOD)("sModCode")
                                        If strCurrentMod.Trim <> "" Then
                                            'set the properties for newly added row
                                            _Modifier = New gloEMR.gloModifier
                                            If Not IsDBNull(dtMOD.Rows(nMOD)("sModCode")) Then
                                                _Modifier.Code = dtMOD.Rows(nMOD)("sModCode")
                                            Else
                                                _Modifier.Code = ""
                                            End If
                                            If Not IsDBNull(dtMOD.Rows(nMOD)("sModDescription")) Then
                                                _Modifier.Description = dtMOD.Rows(nMOD)("sModDescription")
                                            Else
                                                _Modifier.Description = ""
                                            End If
                                            'If Not IsDBNull(dtMOD.Rows(nMOD)("nUnit")) Then
                                            '    _Modifier.Unit = dtMOD.Rows(nMOD)("nUnit")
                                            'Else
                                            '    _Modifier.Unit = 0
                                            'End If

                                            _CPT.ModfierCol.Add(_Modifier)
                                            _Modifier = Nothing
                                        End If
                                    Next
                                End If
                            End With
                            CPTCol.Add(_CPT)
                            _CPT = Nothing
                        End If  '' If CStr(dtICD9.Rows(nICD9)("sICD9Code")).Trim <> "" Then
                    Next ''For nICD9 = 0 To .Rows.Count - 1
                End If  '' If IsNothing(dtICD9) = False Then
            End With '' With dtICD9
            dtICD9 = Nothing
            dtCPT = Nothing
            dtMOD = Nothing

            Return CPTCol
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn = Nothing
        End Try
    End Function
    Private Function FillCPTTreeView() As Boolean
        Dim oCPTs As gloCPTs
        Dim oCPT As gloCPT
        Dim oICD9s As gloICD9s
        Dim oICD9 As gloICD9
        Dim oMods As gloModifiers
        Dim oMod As gloModifier

        Try

            oCPTs = New gloEMR.gloCPTs
            oCPTs = FillICDCPTMOD(gnPatientID)


            Dim myNode As TreeNode
            trProcedureDetails.Nodes.Clear()
            For i As Integer = 0 To oCPTs.Count - 1
                oCPT = oCPTs.Item(i)

                myNode = New TreeNode
                myNode.Tag = oCPT.Code
                myNode.Text = oCPT.Code & " - " & oCPT.Description & "-" & oCPT.Unit
                myNode.ImageIndex = 3
                myNode.SelectedImageIndex = 3
                trProcedureDetails.Nodes.Add(myNode)

                oICD9s = oCPT.ICD9Col

                Dim childNode As New TreeNode
                If oICD9s.Count > 0 Then
                    childNode.Text = "Diagnosis"
                    myNode.Nodes.Add(childNode)
                End If

                Dim tempNode As TreeNode

                For j As Integer = 0 To oICD9s.Count - 1
                    oICD9 = New gloEMR.gloICD9

                    oICD9 = oICD9s.Item(j)

                    tempNode = New TreeNode
                    tempNode.Tag = oICD9.Code
                    tempNode.Text = oICD9.Code & " - " & oICD9.Description
                    tempNode.ImageIndex = 7
                    tempNode.SelectedImageIndex = 7
                    childNode.Nodes.Add(tempNode)
                Next

                oMods = oCPT.ModfierCol

                Dim childNode1 As New TreeNode
                If oMods.Count > 0 Then
                    childNode1.Text = "Modifiers"
                    myNode.Nodes.Add(childNode1)
                End If

                For j As Integer = 0 To oMods.Count - 1
                    oMod = New gloEMR.gloModifier

                    oMod = oMods.Item(j)

                    tempNode = New TreeNode
                    tempNode.Tag = oMod.Code
                    tempNode.Text = oMod.Code & " - " & oMod.Description
                    tempNode.ImageIndex = 8
                    tempNode.SelectedImageIndex = 8
                    childNode1.Nodes.Add(tempNode)

                Next
            Next
            trProcedureDetails.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
        End Try
    End Function
    Public Sub FillExamTypeCombo()

        Dim dtStatus As New DataTable
        Dim MyRow As DataRow

        dtStatus.Columns.Add(New DataColumn("StatusID", GetType(Short)))
        dtStatus.Columns.Add(New DataColumn("ExamStatus", GetType(String)))

        MyRow = dtStatus.NewRow()
        MyRow(0) = 2
        MyRow(1) = "All"
        dtStatus.Rows.Add(MyRow)

        MyRow = dtStatus.NewRow()
        MyRow(0) = 1
        MyRow(1) = "Finished"
        dtStatus.Rows.Add(MyRow)

        MyRow = dtStatus.NewRow()
        MyRow(0) = 0
        MyRow(1) = "UnFinished"
        dtStatus.Rows.Add(MyRow)
        ' cmbExamtype.Items.Clear()
        cmbExamtype.DataSource = dtStatus
        cmbExamtype.DisplayMember = dtStatus.Columns("ExamStatus").ToString()
        cmbExamtype.ValueMember = dtStatus.Columns("StatusID").ToString()


    End Sub

#Region " Patient Orders"


    Private Sub FillCategoryTestGroups()
        Dim oDB As gloStream.gloDataBase.gloDataBase
        'Dim oDataReader As SqlClient.SqlDataReader
        Dim ds As New DataSet
        Dim _strSQL As String
        Dim _Categories As New Collection
        Dim _Groups As New Collection
        Dim _Tests As New Collection

        Dim _SubTests As New Collection

        Dim oFindNode As C1.Win.C1FlexGrid.Node
        Dim oTempNode As C1.Win.C1FlexGrid.Node
        Dim _tmpRow As Integer
        Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        With C1OrderDetails
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Count = COLUM_COUNT
            .Cols.Fixed = 0
            .Rows(.Rows.Count - 1).Height = 21

            .Tree.Column = COLUM_NAME
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.LineStyle = Drawing2D.DashStyle.Solid
            .Tree.Indent = 15

            '.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Nodes
            '.Redraw = False

            'Fill Categories
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            'oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '1' AND lm_category_Description IS NOT NULL ")
            _strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
                    & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                    & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & gnPatientID & ") " _
                    & " ORDER BY lm_category_Description "
            ds = oDB.ReadCatRecords(_strSQL)
            oDB.Disconnect()
            oDB = Nothing

            If IsNothing(ds) = False Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If IsDBNull(ds.Tables(0).Rows(i)("lm_category_Description")) = False Then
                        .Rows.Add()
                        With .Rows(.Rows.Count - 1)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            ''.Style = FillControl.Styles("CS_Category")
                            .Node.Level = 0
                            .Node.Data = ds.Tables(0).Rows(i)("lm_category_Description")
                            .Node.Key = ds.Tables(0).Rows(i)("lm_test_CategoryID")
                        End With

                        .SetData(.Rows.Count - 1, COLUM_IDENTITY, "C" & ds.Tables(0).Rows(i)("lm_test_CategoryID"))
                        .SetData(.Rows.Count - 1, COLUM_NUMVALUE, Nothing)
                        .SetData(.Rows.Count - 1, COLUM_ID, ds.Tables(0).Rows(i)("lm_test_CategoryID"))
                        .SetData(.Rows.Count - 1, COLUM_TESTGROUPFLAG, "C")
                        .SetData(.Rows.Count - 1, COLUM_LEVELNO, 0)
                        .SetData(.Rows.Count - 1, COLUM_GROUPNO, 0)
                        _Categories.Add(ds.Tables(0).Rows(i)("lm_category_Description"))
                    End If
                Next
            End If


            'Fill Groups
            For i As Int16 = 1 To _Categories.Count
                _strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
                        & " FROM LM_Test LEFT OUTER JOIN " _
                        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                        & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
                        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                        & " WHERE (LM_Orders.lm_Patient_ID =" & gnPatientID & ") AND  " _
                        & " (LM_Category.lm_category_Description = '" & Replace(_Categories(i), "'", "''") & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
                        & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "

                oDB = New gloStream.gloDataBase.gloDataBase
                oDB.Connect(GetConnectionString)
                ds = oDB.ReadQueryRecordAsDataSet(_strSQL)
                oDB.Disconnect()
                oDB = Nothing

                If IsNothing(ds) = False Then
                    For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        'If ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString = 0 Then
                        oFindNode = GetC1UniqueNode("C" & ds.Tables(0).Rows(j)("lm_test_CategoryID").ToString, C1OrderDetails)
                        If Not oFindNode Is Nothing Then

                            '' Check For Duplicate Nodes Under the same Group
                            oTempNode = GetC1UniqueNode(ds.Tables(0).Rows(j)("lm_test_TestGroupFlag").ToString & ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString, C1OrderDetails)
                            If IsNothing(oTempNode) = True Then
                                '' If Group Node is Not Exist then Add the Group Node
                                oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, ds.Tables(0).Rows(j)("GroupName"))
                                '//.Style = FillControl.Styles("CS_Category")
                                _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                If Not _tmpRow = -1 Then
                                    .Rows(_tmpRow).AllowEditing = False
                                    .Rows(_tmpRow).ImageAndText = True
                                    .Rows(_tmpRow).Height = 24
                                    .SetData(_tmpRow, COLUM_IDENTITY, ds.Tables(0).Rows(j)("lm_test_TestGroupFlag").ToString & ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString)
                                    .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                    .SetData(_tmpRow, COLUM_ID, ds.Tables(0).Rows(j)("lm_test_GroupNo"))
                                    .SetData(_tmpRow, COLUM_TESTGROUPFLAG, ds.Tables(0).Rows(j)("lm_test_TestGroupFlag"))
                                    '.SetData(_tmpRow, COLUM_LEVELNO, ds.Tables(0).Rows(j)("lm_test_LevelNo"))
                                    '.SetData(_tmpRow, COLUM_GROUPNO, ds.Tables(0).Rows(j)("lm_test_GroupNo"))

                                    _Groups.Add(ds.Tables(0).Rows(j)("lm_test_GroupNo"))

                                    If ds.Tables(0).Rows(j)("lm_test_TestGroupFlag") = "T" Then
                                        ''  IF is Test then 
                                        .Rows(_tmpRow).AllowEditing = True
                                        .Cols(COLUM_NAME).AllowEditing = False
                                        ''  SET TemplateID
                                        .SetData(_tmpRow, COLUM_TEMPLATEID, ds.Tables(0).Rows(j)("lm_test_Template_ID"))
                                        '' For the Numeric Value
                                        C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                                        ''  Insert CheckBox
                                        '.SetCellCheck(_tmpRow, COLUM_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)
                                        .SetCellImage(_tmpRow, COLUM_BUTTON, imgTreeView.Images(4))
                                        ''  SET ShowComment Button
                                        .SetData(_tmpRow, COLUM_BUTTON, "")
                                        cStyle = .Styles.Add("BubbleValues")
                                        cStyle.ComboList = "..."
                                        '.CellButtonImage = imgTreeView.Images(0)
                                        Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
                                        rgBubbleValues.Style = cStyle

                                        ''''' 20070129 For Fill Diagnosis '
                                        Dim csDia As CellStyle = .Styles.Add("Dia")
                                        '' Fill Values In ComboBox
                                        csDia.ComboList = strDia
                                        '''''
                                        .Cols(COLUM_DIAGNOSIS).Style = csDia

                                        Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
                                        rgDig.Style = cStyle

                                    End If

                                    _tmpRow = -1
                                End If
                            End If
                            '''''



                            '''''' Add Test 
                            Dim dsTest As New DataSet
                            _strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
                                        & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, LM_Orders.lm_Status, " _
                                        & " LM_Test.lm_test_Template_ID , LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description " _
                                    & " FROM  LM_Test INNER JOIN " _
                                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
                                    & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
                                    & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & gnPatientID & ") AND  " _
                                    & " (LM_Category.lm_category_Description = '" & Replace(_Categories(i), "'", "''") & "') AND (LM_Test.lm_test_GroupNo =" & ds.Tables(0).Rows(j)("lm_test_GroupNo") & ") " _
                                    & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

                            oDB = New gloStream.gloDataBase.gloDataBase
                            oDB.Connect(GetConnectionString)
                            dsTest = oDB.ReadQueryRecordAsDataSet(_strSQL)
                            oDB.Disconnect()
                            oDB = Nothing

                            If IsNothing(dsTest) = False Then
                                For l As Integer = 0 To dsTest.Tables(0).Rows.Count - 1
                                    'If ds.Tables(0).Rows(j)("lm_test_GroupNo").ToString = 0 Then
                                    oFindNode = GetC1UniqueNode("G" & ds.Tables(0).Rows(j)("lm_test_GroupNo"), C1OrderDetails)
                                    If Not oFindNode Is Nothing Then
                                        '' Check For Duplicate Nodes Under the same Group
                                        oTempNode = GetC1UniqueNode(dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag").ToString & dsTest.Tables(0).Rows(l)("lm_test_ID").ToString, C1OrderDetails)
                                        If IsNothing(oTempNode) = False Then
                                            '' If Node is Alredy Exixst then Exit For
                                            Exit For
                                        End If
                                        '''''

                                        oFindNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, dsTest.Tables(0).Rows(l)("lm_test_Name"))
                                        '//.Style = FillControl.Styles("CS_Category")
                                        _tmpRow = oFindNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index
                                        If Not _tmpRow = -1 Then
                                            .Rows(_tmpRow).AllowEditing = False
                                            .Rows(_tmpRow).ImageAndText = True
                                            .Rows(_tmpRow).Height = 24
                                            .SetData(_tmpRow, COLUM_IDENTITY, dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag").ToString & dsTest.Tables(0).Rows(l)("lm_test_ID").ToString)
                                            .SetData(_tmpRow, COLUM_NUMVALUE, Nothing)
                                            .SetData(_tmpRow, COLUM_ID, dsTest.Tables(0).Rows(l)("lm_test_ID"))
                                            .SetData(_tmpRow, COLUM_TESTGROUPFLAG, dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag"))
                                            '.SetData(_tmpRow, COLUM_LEVELNO, ds.Tables(0).Rows(j)("lm_test_LevelNo"))
                                            ' .SetData(_tmpRow, COLUM_GROUPNO, ds.Tables(0).Rows(j)("lm_test_GroupNo"))
                                            .SetData(_tmpRow, COLUM_UNIT, dsTest.Tables(0).Rows(l)("lm_test_Dimension"))

                                            _Tests.Add(dsTest.Tables(0).Rows(l)("lm_test_Name"))

                                            If dsTest.Tables(0).Rows(l)("lm_test_TestGroupFlag") = "T" Then
                                                ''  IF is Test then 
                                                .Rows(_tmpRow).AllowEditing = True
                                                .Cols(COLUM_NAME).AllowEditing = False
                                                '' For the Numeric Value
                                                C1OrderDetails.Cols(COLUM_NUMVALUE).Format = Format("##0.000")
                                                ''  SET TemplateID
                                                .SetData(_tmpRow, COLUM_TEMPLATEID, dsTest.Tables(0).Rows(l)("lm_test_Template_ID"))

                                                ''  Insert CheckBox
                                                '.SetCellCheck(_tmpRow, COLUM_NAME, C1.Win.C1FlexGrid.CheckEnum.Checked)

                                                .SetCellImage(_tmpRow, COLUM_BUTTON, imgTreeView.Images(4))

                                                ''  SET ShowComment Button
                                                .SetData(_tmpRow, COLUM_BUTTON, "")
                                                cStyle = .Styles.Add("BubbleValues")
                                                cStyle.ComboList = "..."

                                                Dim rgBubbleValues As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_BUTTON, _tmpRow, COLUM_BUTTON)
                                                rgBubbleValues.Style = cStyle

                                                ''''' 20070129 For Fill Diagnosis
                                                Dim csDia As CellStyle = .Styles.Add("Dia")
                                                '' Fill Values In ComboBox
                                                csDia.ComboList = strDia
                                                '''''
                                                .Cols(COLUM_DIAGNOSIS).Style = csDia

                                                Dim rgDig As C1.Win.C1FlexGrid.CellRange = .GetCellRange(_tmpRow, COLUM_DIAGNOSISBUTTON, _tmpRow, COLUM_DIAGNOSISBUTTON)
                                                rgDig.Style = cStyle
                                                '' Set Associated Diagnosis with this Order
                                                .SetData(_tmpRow, COLUM_DIAGNOSIS, dsTest.Tables(0).Rows(l)("lm_sICD9Code") & "-" & dsTest.Tables(0).Rows(l)("lm_sICD9Description"))
                                                ''''''''''''''

                                                If IsDBNull(dsTest.Tables(0).Rows(l)("lm_Result")) = False Then
                                                    If IsNothing(dsTest.Tables(0).Rows(l)("lm_Result")) = False Then
                                                        ''''' If Order comments are entered then Indicate it by ForeColor as RED
                                                        .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Red
                                                    Else
                                                        ''''' If Order comments are NOT entered then Indicate it by ForeColor as GREEN
                                                        .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
                                                    End If
                                                Else
                                                    .Rows(_tmpRow).StyleDisplay.ForeColor = Color.Green
                                                End If

                                            End If

                                            _tmpRow = -1
                                        End If
                                    End If
                                    ' End If
                                Next
                            End If
                            '' Add Test Close
                        End If
                        ' End If
                    Next
                End If
            Next

            .Cols(COLUM_NAME).AllowEditing = False
            .Cols(COLUM_IDENTITY).AllowEditing = False
            .Cols(COLUM_NUMVALUE).AllowEditing = False
            .Cols(COLUM_ID).AllowEditing = False
            .Cols(COLUM_TESTGROUPFLAG).AllowEditing = False
            .Cols(COLUM_LEVELNO).AllowEditing = False
            .Cols(COLUM_GROUPNO).AllowEditing = False
            .Cols(COLUM_ISFINISHED).AllowEditing = False
            .Cols(COLUM_UNIT).AllowEditing = False
            .Cols(COLUM_DIAGNOSIS).AllowEditing = False
            .Cols(COLUM_DMSID).AllowEditing = False

            .SetData(0, COLUM_NAME, "Tests")
            .SetData(0, COLUM_IDENTITY, "Identity")
            .SetData(0, COLUM_NUMVALUE, "Value")
            .SetData(0, COLUM_BUTTON, "Comments")
            .SetData(0, COLUM_ID, "ID")
            .SetData(0, COLUM_TESTGROUPFLAG, "Flag")
            .SetData(0, COLUM_LEVELNO, "Level No")
            .SetData(0, COLUM_GROUPNO, "Group No")
            .SetData(0, COLUM_UNIT, "Unit")
            .SetData(0, COLUM_DIAGNOSIS, "Diagnosis")
            .SetData(0, COLUM_DMSID, "DMS ID")

            .Cols(COLUM_NAME).Width = .Width * 0.98 '((.Width / 5) * 2.5) - 20
            .Cols(COLUM_NUMVALUE).Width = .Width * 0.0  '((.Width / 5) * 0.5)
            .Cols(COLUM_BUTTON).Width = .Width * 0 '((.Width / 5) * 0.2)
            .Cols(COLUM_UNIT).Width = .Width * 0  '((.Width / 5) * 0.2)
            .Cols(COLUM_DIAGNOSIS).Width = .Width * 0
            .Cols(COLUM_DIAGNOSISBUTTON).Width = 0 ''.Width * 0.03


            .Cols(COLUM_NAME).Visible = True
            .Cols(COLUM_IDENTITY).Visible = False
            .Cols(COLUM_NUMVALUE).Visible = False
            .Cols(COLUM_ID).Visible = False
            .Cols(COLUM_TESTGROUPFLAG).Visible = False
            .Cols(COLUM_LEVELNO).Visible = False
            .Cols(COLUM_GROUPNO).Visible = False
            .Cols(COLUM_TEMPLATEID).Visible = False
            .Cols(COLUM_ISFINISHED).Visible = False
            .Cols(COLUM_DIAGNOSIS).Visible = True
            .Cols(COLUM_DMSID).Visible = False
            'c1OrderDetails.getData(0, 2)

            '' For the Numeric Value
            .Cols(COLUM_NUMVALUE).Format = Format("##0.000")
            .Cols(COLUM_NUMVALUE).DataType = System.Type.GetType("System.Decimal")
        End With

    End Sub

    Private Function GetC1UniqueNode(ByVal FindData As String, ByVal _C1FlexGrid As C1.Win.C1FlexGrid.C1FlexGrid) As C1.Win.C1FlexGrid.Node
        Dim _Node As C1.Win.C1FlexGrid.Node = Nothing
        Dim _FindRow As Integer = _C1FlexGrid.FindRow(FindData, 0, COLUM_IDENTITY, False, True, True)
        If _FindRow > 0 Then
            _Node = _C1FlexGrid.Rows(_FindRow).Node
        End If
        Return _Node
    End Function

#End Region
    Private Sub cmbExamProvider_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbExamProvider.SelectionChangeCommitted
        m_ExamFilter = True
        blnIsExam = True
        FillExamDMS()
        m_ExamFilter = False
    End Sub

    Private Sub cmbExamtype_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbExamtype.SelectionChangeCommitted
        m_ExamFilter = True
        blnIsExam = True
        FillExamDMS()
        m_ExamFilter = False
    End Sub

    Private Sub DTPFrom_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged
        If Not m_ResetFlag Then
            m_ExamFilter = True
            blnIsExam = True
            FillExamDMS()
            m_ExamFilter = False
        End If
    End Sub

    Private Sub DTPTo_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpTo.ValueChanged
        If Not m_ResetFlag Then
            m_ExamFilter = True
            blnIsExam = True
            FillExamDMS()
            m_ExamFilter = False
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'Fill_PatientSacnedDocuments(gnPatientID)
            txtSearchCriteria.Text = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub cmbSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSearch.SelectedIndexChanged
        lblsearch.Text = cmbSearch.Text
        If txtSearchCriteria.Text.Trim.Length = 0 Then
            Fill_PatientSacnedDocuments(gnPatientID)
        Else
            SearchDMS()
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Try
            'If txtSearchCriteria.Text.Trim.Length = 0 Then
            '    m_ExamFilter = True
            '    blnIsExam = True
            '    SearchExams()
            '    m_ExamFilter = False
            'Else
            '    m_ExamFilter = True
            '    blnIsExam = True

            '    FillExamDMS()
            '    m_ExamFilter = False
            'End If

            cmbExamtype.SelectedIndex = 0
            cmbExamProvider.SelectedIndex = 0
            dtpFrom.Value = Now.Date
            dtpTo.Value = Now.Date
            m_ExamFilter = True
            blnIsExam = True
            FillExamDMS()
            m_ExamFilter = False

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub SearchExams()
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim strSQL As String = ""
        Dim dtPatientdetails As DataTable
        Try
            conn = New SqlConnection
            conn.ConnectionString = GetConnectionString()
            conn.Open()
            cmd = New SqlCommand
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            strSQL = " SELECT     PatientExams.nExamID, PatientExams.nVisitID, PatientExams.dtDOS AS DOS, PatientExams.sExamName AS [Exam Name],     " _
          & " CASE PatientExams.bIsFinished WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END AS Finished, ISNULL(Provider_MST.sFirstName, '')     " _
          & " + ' ' + ISNULL(Provider_MST.sMiddleName, '') + ' ' + ISNULL(Provider_MST.sLastName, '') AS ProviderName, ISNULL(Provider_MST_1.sFirstName, '') " _
          & " + ' ' + ISNULL(Provider_MST_1.sMiddleName, '') + ' ' + ISNULL(Provider_MST_1.sLastName, '') + ' ' + ISNULL(' On ' + convert(varchar,PatientExams.dtReviewDate,100),'') AS ReviewedBy   FROM   PatientExams INNER JOIN " _
          & " Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID left OUTER JOIN " _
          & " Provider_MST AS Provider_MST_1 ON PatientExams.nReviewerID = Provider_MST_1.nProviderID " _
          & " WHERE     (PatientExams.nPatientID = '" & gnPatientID & "') and PatientExams.sExamName like '%" & txtExamName.Text & "%' ORDER BY DOS DESC, PatientExams.nExamID DESC  "
            cmd.CommandText = strSQL

            dtPatientdetails = New DataTable
            dtPatientdetails.Load(cmd.ExecuteReader)
            If Not IsNothing(dtPatientdetails) Then
                'dgExams = Nothing
                dgExams.Enabled = False
                dgExams.DataSource = dtPatientdetails.DefaultView
                dgExams.Enabled = True
                dgExams.TableStyles.Clear()
                Dim grdColStyleExamID As New DataGridTextBoxColumn

                With grdColStyleExamID
                    .HeaderText = "Exam ID"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientdetails.Columns(0).ColumnName
                    .NullText = ""
                    .Width = 0
                End With
                Dim grdColStyleVisitID As New DataGridTextBoxColumn
                With grdColStyleVisitID
                    .HeaderText = "Visit ID"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientdetails.Columns(1).ColumnName
                    .NullText = ""
                    .Width = 0
                End With

                Dim grdColStyleDate As New DataGridTextBoxColumn

                With grdColStyleDate
                    .HeaderText = "DOS"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientdetails.Columns(2).ColumnName
                    .NullText = ""
                    .Width = 0.1 * C1dgPatientDetails.Width
                End With

                Dim grdColStyleExamName As New DataGridTextBoxColumn
                With grdColStyleExamName
                    .HeaderText = "Exam Name"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientdetails.Columns(3).ColumnName
                    .NullText = ""
                    .Width = 0.32 * C1dgPatientDetails.Width
                End With

                Dim grdColStyleFinished As New DataGridTextBoxColumn
                With grdColStyleFinished
                    .HeaderText = "Finished"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientdetails.Columns(4).ColumnName
                    .NullText = ""
                    .Width = 0.1 * C1dgPatientDetails.Width - 5
                End With

                Dim grdColStyleProvider As New DataGridTextBoxColumn
                With grdColStyleProvider
                    .HeaderText = "Provider"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientdetails.Columns("ProviderName").ColumnName
                    .NullText = ""
                    .Width = 0.18 * C1dgPatientDetails.Width - 5
                End With

                Dim grdColStyleReview As New DataGridTextBoxColumn
                With grdColStyleReview
                    .HeaderText = "Reviewed By"
                    .Alignment = HorizontalAlignment.Left
                    .MappingName = dtPatientdetails.Columns("ReviewedBy").ColumnName
                    .NullText = ""
                    .Width = 0.3 * C1dgPatientDetails.Width - 5
                End With

                dgExams.TableStyles.Clear()
                Dim grdTableStyle As New clsDataGridTableStyle(dtPatientdetails.TableName)
                grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleExamID, grdColStyleVisitID, grdColStyleDate, grdColStyleExamName, grdColStyleFinished, grdColStyleProvider, grdColStyleReview})
                dgExams.TableStyles.Add(grdTableStyle)
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Patient Past Exam viewed from DashBoard", gloAuditTrail.ActivityOutCome.Success)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtExamName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExamName.TextChanged
        Try
            If txtExamName.Text.Trim.Length = 0 Then
                m_ExamFilter = True
                blnIsExam = True

                FillExamDMS()
                m_ExamFilter = False
            Else
                m_ExamFilter = True
                blnIsExam = True

                SearchExams()
                m_ExamFilter = False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtSearchCriteria_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchCriteria.TextChanged
        Try
            If txtSearchCriteria.Text.Trim.Length = 0 Then
                Fill_PatientSacnedDocuments(gnPatientID)
            Else
                SearchDMS()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub oShowDocument_EvnRefreshDocuments() Handles oShowDocument.EvnRefreshDocuments
        '''' Write any code here after view document form
    End Sub

    Private Sub tlsbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnClose.Click
        Me.Close()
    End Sub

    Private Sub oSearchProblemListCtl_AfterTextSearch(ByVal dv As System.Data.DataView) Handles oSearchProblemListCtl.AfterTextSearch
        If Not IsNothing(dv) Then
            MsgBox(dv.ToTable.Rows.Count)
        End If
    End Sub

    Private Sub oSearchMedicationsCtl_AfterTextSearch(ByVal dv As System.Data.DataView) Handles oSearchMedicationsCtl.AfterTextSearch
        If Not IsNothing(dv) Then
            MsgBox(dv.ToTable.Rows.Count)
        End If
    End Sub

    Private Sub oSearchAllergiesCtl_AfterTextSearch(ByVal dv As System.Data.DataView) Handles oSearchAllergiesCtl.AfterTextSearch
        If Not IsNothing(dv) Then
            MsgBox(dv.ToTable.Rows.Count)
        End If
    End Sub
End Class