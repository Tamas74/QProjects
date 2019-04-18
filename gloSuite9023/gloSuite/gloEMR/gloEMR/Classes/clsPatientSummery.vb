Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class clsPatientSummery
  

    Public Function GetExamforPatient(ByVal PatientID As Int64) As DataTable
        Dim ODB As DataBaseLayer = New DataBaseLayer
        Dim dt As DataTable
        Try


            ''Dim strSelect As String = "Select nExamID, nVisitID, nPatientID, sExamName, sPatientNotes, bIsFinished, dtDOS, bIsOpen, sUserName, sMachineName, nProviderID FROM PatientExams WHERE nPatientID = '" & PatientID & "'"

            Dim strSelect As String = "SELECT PatientExams.nExamID,PatientExams.nVisitID,PatientExams.nPatientID, " _
                                      & "PatientExams.sExamName,PatientExams.sPatientNotes,PatientExams.bIsFinished, PatientExams.dtDOS, " _
                                      & "PatientExams.bIsOpen,PatientExams.sUserName,PatientExams.sMachineName,PatientExams.nProviderID, " _
                                      & "isnull(Provider_MST.sFirstName,'') as FirstName ,isnull(Provider_MST.sMiddleName,'') as MiddleName ,isnull(Provider_MST.sLastName,'') as LastName,PatientExams.sTemplateName " _
                                      & "FROM PatientExams INNER JOIN Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID WHERE nPatientID = '" & PatientID & "'"
            dt = ODB.GetDataTable_Query(strSelect)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
            ODB = Nothing
        End Try
    End Function

    Public Function GetLabOrderforPatient(ByVal PatientID As Int64) As DataTable
        Dim ODB As DataBaseLayer = New DataBaseLayer
        Dim dt As DataTable
        Try


            'Dim _strSql As String = "SELECT DISTINCT Lab_Order_MST.labom_OrderID,Lab_Order_MST.labom_OrderNoPrefix,Lab_Order_MST.labom_OrderNoID,Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID,Lab_Order_MST.labom_TransactionDate,Lab_Order_MST.labom_VisitID" _
            '     & " FROM Lab_Order_TestDtl INNER JOIN " _
            '     & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
            '     & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID " _
            '     & " WHERE Lab_Order_MST.labom_PatientID = " & PatientID & ""

            Dim _strSql As String = "SELECT DISTINCT Lab_Order_MST.labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix, Lab_Order_MST.labom_OrderNoID, Lab_Test_Mst.labtm_Name, " _
                        & "Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID, Lab_Order_MST.labom_TransactionDate, " _
                        & "Lab_Order_MST.labom_VisitID, Lab_Order_MST.labom_ProviderID, isnull(Provider_MST.sFirstName,'') as FirstName,isnull(Provider_MST.sMiddleName,'') as MiddleName, " _
                        & "isnull(Provider_MST.sLastName,'') as LastName FROM Lab_Order_TestDtl INNER JOIN " _
                        & "Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                        & "Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID INNER JOIN " _
                        & "Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID " _
                        & " WHERE Lab_Order_MST.labom_PatientID = " & PatientID & ""
            dt = ODB.GetDataTable_Query(_strSql)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
            ODB = Nothing
        End Try

    End Function

    Public Function GetRadiologyOrderforPatient(ByVal PatientID As Int64) As DataTable
        Dim ODB As DataBaseLayer = New DataBaseLayer
        Try
            '    Try

            'Dim oDataReader As SqlClient.SqlDataReader
            ' Dim dt As New DataTable
            Dim _strSQL As String
            'Dim _Categories As New Collection
            'Dim _Groups As New Collection
            'Dim _Tests As New Collection

            'Dim _SubTests As New Collection

            'Dim oFindNode As C1.Win.C1FlexGrid.Node
            'Dim oTempNode As C1.Win.C1FlexGrid.Node
            'Dim _tmpRow As Integer
            'Dim cStyle As C1.Win.C1FlexGrid.CellStyle
            Dim dsTest As DataTable


            '        'oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '1' AND lm_category_Description IS NOT NULL ")
            '        _strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
            '                & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
            '                & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
            '                & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & PatientID & ") " _
            '                & " ORDER BY lm_category_Description "
            '        ''AND (LM_Orders.lm_OrderDate = '" & _VisitDate & "') " _





            '        dt = ODB.GetDataTable_Query(_strSQL)



            '        If IsNothing(dt) = False Then
            '            For i As Integer = 0 To dt.Rows.Count - 1
            '                If IsDBNull(dt.Rows(i)("lm_category_Description")) = False Then
            '                    _Categories.Add(dt.Rows(i)("lm_category_Description"))



            '                End If
            '            Next
            '        End If


            '        'Fill Groups
            '        For i As Int16 = 1 To _Categories.Count
            '            _strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
            '                    & " FROM LM_Test LEFT OUTER JOIN " _
            '                    & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
            '                    & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
            '                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
            '                    & " WHERE (LM_Orders.lm_Patient_ID =" & PatientID & ") AND  " _
            '                    & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
            '                    & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "
            '            ''AND (LM_Orders.lm_OrderDate = '" & _VisitDate & "') 




            '            dt = New DataTable
            '            dt = ODB.GetDataTable_Query(_strSQL)



            '            If IsNothing(dt) = False Then
            '                For j As Integer = 0 To dt.Rows.Count - 1


            '                    _Groups.Add(dt.Rows(j)("lm_test_GroupNo"))


            '                    '''''' Add Test 
            '                    Dim dsTest As New DataTable
            '                    '_strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
            '                    '            & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, LM_Orders.lm_Status, " _
            '                    '            & " LM_Test.lm_test_Template_ID , LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description " _
            '                    '        & " FROM  LM_Test INNER JOIN " _
            '                    '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
            '                    '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
            '                    '        & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & PatientID & ")  AND  " _
            '                    '        & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_GroupNo =" & dt.Rows(j)("lm_test_GroupNo") & ") " _
            '                    '        & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

            '                    _strSQL = "SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, "

            '                         & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, " _
            '                         & "LM_Orders.lm_Status, LM_Test.lm_test_Template_ID, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, " _
            '                         & "LM_Orders.lm_sICD9Description, isnull(Provider_MST.sFirstName,'') as FirstName, isnull(Provider_MST.sMiddleName,'') as MiddleName, isnull(Provider_MST.sLastName,'') as LastName " _
            '                         & "FROM LM_Test INNER JOIN " _
            '                         & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
            '                         & "LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID INNER JOIN " _
            '                         & "Provider_MST ON LM_Orders.lm_Provider_ID = Provider_MST.nProviderID " _
            '                         & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & PatientID & ")  AND  " _
            '                         & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_GroupNo =" & dt.Rows(j)("lm_test_GroupNo") & ") " _
            '                         & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "
            '                    ''AND (LM_Orders.lm_OrderDate = '" & _VisitDate & "')
            '                    dsTest = ODB.GetDataTable_Query(_strSQL)
            '                    Return dsTest

            '                    'If IsNothing(dsTest) = False Then
            '                    '    For l As Integer = 0 To dsTest.Rows.Count - 1
            '                    '        _Tests.Add(dsTest.Rows(l)("lm_test_Name"))
            '                    '        ' End If
            '                    '    Next
            '                    'End If


            '                    ' End If
            '                Next
            '            End If
            '        Next


            'SHUBHANGI 20100818 RESOLVE FOR NOT RETRIVING ORDER FOR PATIENTSUMMARY
            _strSQL = " SELECT LM_Orders.LM_ORDERDATE,LM_Orders.lm_sTestName AS lm_test_Name,LM_Orders.lm_Order_ID,LM_Orders.lm_Visit_ID,LM_Orders.lm_IsFinished,Provider_MST.sFirstName AS FirstName,Provider_MST.sMiddleName AS MiddleName,Provider_MST.sLastName " _
                      & " as LastName FROM lm_oRDERS INNER JOIN Provider_MST ON LM_Orders.lm_Provider_ID = Provider_MST.nProviderID WHERE ( lm_Patient_ID = " & PatientID & " )"

            ''AND (LM_Orders.lm_OrderDate = '" & _VisitDate & "')
            dsTest = ODB.GetDataTable_Query(_strSQL)
            Return dsTest

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ODB.Dispose()
            ODB = Nothing
        End Try


    End Function

    Public Function GetScanDocumentforPatient(ByVal PatientID As Int64) As DataTable
        Dim ODB As DataBaseLayer = New DataBaseLayer
        Dim dt As DataTable
        Try

            Dim strQry As String = "Select DocumentID, DocumentName, Extension, SourceMachine, SystemFolder, Container, Category, PatientID, Year, Month, DocumentFormat, SourceBin, Pages, ArchiveStatus, ArchiveDescription, UsedStatus, UsedMachine, UsedType, DocumentType, DocumentFileName, MachineID, Modified, Synchronized, VersionNo, ModifyDateTime, IsReviewed from DMS_MST WHERE PatientID = '" & PatientID & "'"

            dt = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function InsertAssociation(ByVal PaitentID As Int64, ByVal ArrExam As ArrayList, ByVal ArrRadiology As ArrayList, ByVal ArrLabs As ArrayList, ByVal ArrScanDocu As ArrayList)

        Try
            Dim ExamID As Int64
            Dim ExamName As String
            Dim lst As myList
            '  Dim dt As DataTable
            ' Dim _strQRY As String
            Dim _strInsertQry As String
            Dim _strDeleteQry As String

            Dim strRecordPresent As String = ""
            Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
            conn.Open()
            _strDeleteQry = "Delete from PatientSummery WHERE nPatientID = '" & PaitentID & "'"
            Dim cmd As SqlCommand = New SqlCommand(_strDeleteQry, conn)
            cmd.ExecuteNonQuery()
            conn.Close()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            If Not IsNothing(ArrExam) Then
                conn.Open()
                For i As Integer = 0 To ArrExam.Count - 1
                    'lst = New myList
                    lst = CType(ArrExam.Item(i), myList)
                    ExamID = lst.ID
                    ExamName = lst.ParameterName
                    '_strQRY = "SELECT  nAssociateID FROM PatientSummery WHERE nPatientID = '" & PaitentID & "' AND nAssociateID = '" & ExamID & "' AND nAssociateType = '" & PatientAssociatType.Exam & " '"
                    'strRecordPresent = ODB.GetRecord_Query(_strQRY)
                    'If strRecordPresent = "" Then
                    ''Above line commented by Sandip Darade 20100114 Bug ID 5783 added line below 
                    _strInsertQry = "Insert Into PatientSummery (nPatientID, nAssociateID, nAssociateType)  values ('" & PaitentID & "','" & ExamID & "','" & PatientAssociatType.Exam & "')"
                    cmd = New SqlCommand(_strInsertQry, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    'End If
                Next
                conn.Close()
            End If

            Dim RadiologyID As Int64
            Dim RadiologyName As String
            If Not IsNothing(ArrRadiology) Then
                conn.Open()
                For i As Integer = 0 To ArrRadiology.Count - 1
                    'lst = New myList
                    lst = CType(ArrRadiology.Item(i), myList)
                    RadiologyID = lst.ID
                    RadiologyName = lst.ParameterName
                    '_strQRY = "SELECT  nAssociateID FROM PatientSummery WHERE nPatientID = '" & PaitentID & "' AND nAssociateID = '" & RadiologyID & "' AND nAssociateType = '" & PatientAssociatType.Radiology & " '"
                    'strRecordPresent = ODB.GetRecord_Query(_strQRY)
                    'If strRecordPresent = "" Then
                    ''Above line commented by Sandip Darade 20100114 Bug ID 5783 added line below 
                    _strInsertQry = "Insert Into PatientSummery (nPatientID, nAssociateID, nAssociateType)  values('" & PaitentID & "','" & RadiologyID & "','" & PatientAssociatType.Radiology & "')"
                    cmd = New SqlCommand(_strInsertQry, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    'End If
                Next
                conn.Close()
            End If

            Dim LabID As Int64
            Dim LabName As String
            If Not IsNothing(ArrLabs) Then
                conn.Open()
                For i As Integer = 0 To ArrLabs.Count - 1
                    'lst = New myList
                    lst = CType(ArrLabs.Item(i), myList)
                    LabID = lst.ID
                    LabName = lst.ParameterName

                    '_strQRY = "SELECT  nAssociateID FROM PatientSummery WHERE nPatientID = '" & PaitentID & "' AND nAssociateID = '" & LabID & "' AND nAssociateType = '" & PatientAssociatType.Labs & " '"
                    'strRecordPresent = ODB.GetRecord_Query(_strQRY)
                    'If strRecordPresent = "" Then
                    ''Above line commented by Sandip Darade 20100114 Bug ID 5783 added line below 
                    _strInsertQry = "Insert Into PatientSummery (nPatientID, nAssociateID, nAssociateType)  values('" & PaitentID & "','" & LabID & "','" & PatientAssociatType.Labs & "')"
                    cmd = New SqlCommand(_strInsertQry, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    'End If
                Next
                conn.Close()
            End If

            Dim ScanDocID As Int64
            Dim ScanDocName As String
            If Not IsNothing(ArrScanDocu) Then
                conn.Open()
                For i As Integer = 0 To ArrScanDocu.Count - 1
                    'lst = New myList
                    lst = CType(ArrScanDocu.Item(i), myList)
                    ScanDocID = lst.ID
                    ScanDocName = lst.ParameterName

                    '_strQRY = "SELECT  nAssociateID FROM PatientSummery WHERE nPatientID = '" & PaitentID & "' AND nAssociateID = '" & ScanDocID & "' AND nAssociateType = '" & PatientAssociatType.ScanDocument & " '"
                    'strRecordPresent = ODB.GetRecord_Query(_strQRY)
                    'If strRecordPresent = "" Then
                    '' _strInsertQry = "Insert Into PatientSummery values('" & PaitentID & "','" & ScanDocID & "','" & PatientAssociatType.ScanDocument & "')"
                    ''Above line commented by Sandip Darade 20100114 Bug ID 5783 added line below 
                    _strInsertQry = "Insert Into PatientSummery (nPatientID, nAssociateID, nAssociateType) values('" & PaitentID & "','" & ScanDocID & "','" & PatientAssociatType.ScanDocument & "')"
                    cmd = New SqlCommand(_strInsertQry, conn)
                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    'End If
                Next
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
        End Try
        Return Nothing
    End Function

    Public Function GetPatientAssociation(ByVal PatientID As Int64) As DataTable
        Dim ODB As DataBaseLayer = New DataBaseLayer
        Dim dt As DataTable
        Try

            Dim strSelectQry As String = "Select nPatientID, nAssociateID, nAssociateType FROM PatientSummery WHERE nPatientID = '" & PatientID & "' order by nAssociateType"

            dt = ODB.GetDataTable_Query(strSelectQry)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            ODB.Dispose()
            ODB = Nothing
        End Try
    End Function
    'Public Function GetPatientExam(ByVal PateintID As Int64, ByVal ExamID As Int64)

    '    Dim strSelectQry As String = "Select sExamName FROM PatientExams WHERE nPatientID = '" & PatientID & "' AND nExamID = '" & ExamID & "'" '
    '    Dim strExamName As String
    '    conn = New SqlConnection(GetConnectionString)


    '    Return dt
    'End Function
End Class
