Imports System.Data
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class clsLiquidData
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Private _PatientID As Int64
    Private _mgnVisitID As Int64
    Private _examid As Int64
    Private _m_elementId As Int64
    Private _HelpText As Int64
    Private _m_datatype As String
    Private _Text_Field As Microsoft.Office.Interop.Word.Range
    Private _ArrText_Field As ArrayList
    Private _Title As String
    Private _Category As String
    Private _HistoryCount As Int16
    Public Property HistoryCount() As Int16
        Get
            Return _HistoryCount
        End Get
        Set(ByVal value As Int16)
            _HistoryCount = value
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
    Public Property mgnVisitID() As Int64
        Get
            Return _mgnVisitID
        End Get
        Set(ByVal value As Int64)
            _mgnVisitID = value
        End Set
    End Property
    Public Property examid() As Int64
        Get
            Return _examid
        End Get
        Set(ByVal value As Int64)
            _examid = value
        End Set
    End Property
    Public Property m_elementId() As Int64
        Get
            Return _m_elementId
        End Get
        Set(ByVal value As Int64)
            _m_elementId = value
        End Set
    End Property
    Public Property HelpText() As Int64
        Get
            Return _HelpText
        End Get
        Set(ByVal value As Int64)
            _HelpText = value
        End Set
    End Property
    Public Property m_datatype() As String
        Get
            Return _m_datatype
        End Get
        Set(ByVal value As String)
            _m_datatype = value
        End Set
    End Property
    Public Property Text_Field() As Microsoft.Office.Interop.Word.Range
        Get
            Return _Text_Field
        End Get
        Set(ByVal value As Microsoft.Office.Interop.Word.Range)
            _Text_Field = value
        End Set
    End Property
    Public Property ArrText_Field() As ArrayList
        Get
            Return _ArrText_Field
        End Get
        Set(ByVal value As ArrayList)
            _ArrText_Field = value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property

    Public Property Category() As String
        Get
            Return _Category
        End Get
        Set(ByVal value As String)
            _Category = value
        End Set
    End Property
End Class
Public Class CollLiquidData
    Inherits CollectionBase

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    'Remove Item at specified index
    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a widget at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no object exists, a messagebox is shown and the operation is 
            ' cancelled.
            'System.Windows.Forms.MessageBox.Show("Index not valid!")
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As clsLiquidData
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), clsLiquidData)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As clsLiquidData)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                Me.Clear()
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class
Public Class clsAssocatedLiquidData
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Private _Group_ID As Int64 ''ElementGroup_Id
    Private _GroupTitle As String ''Title
    Private _GrouppropertyId As String ''Id 

    Private _ItemGroup_ID As Int64 ''SubElementGroup_Id
    Private _ItemTitle As String ''Text
    Private _ItempropertyId As String ''Id

    Private _SubElementGroup_ID As Int64
    Private _SubElementTitle As String
    Private _SubElementpropertyId As String

    Public Property Group_ID() As Int64
        Get
            Return _Group_ID
        End Get
        Set(ByVal value As Int64)
            _Group_ID = value
        End Set
    End Property
    Public Property GroupTitle() As String
        Get
            Return _GroupTitle
        End Get
        Set(ByVal value As String)
            _GroupTitle = value
        End Set
    End Property
    Public Property GrouppropertyId() As String
        Get
            Return _GrouppropertyId
        End Get
        Set(ByVal value As String)
            _GrouppropertyId = value
        End Set
    End Property



    Public Property ItemGroup_ID() As Int64
        Get
            Return _ItemGroup_ID
        End Get
        Set(ByVal value As Int64)
            _ItemGroup_ID = value
        End Set
    End Property
    Public Property ItemTitle() As String
        Get
            Return _ItemTitle
        End Get
        Set(ByVal value As String)
            _ItemTitle = value
        End Set
    End Property
    Public Property ItempropertyId() As String
        Get
            Return _ItempropertyId
        End Get
        Set(ByVal value As String)
            _ItempropertyId = value
        End Set
    End Property




    Public Property SubElementGroup_ID() As Int64
        Get
            Return _SubElementGroup_ID
        End Get
        Set(ByVal value As Int64)
            _SubElementGroup_ID = value
        End Set
    End Property
    Public Property SubElementTitle() As String
        Get
            Return _SubElementTitle
        End Get
        Set(ByVal value As String)
            _SubElementTitle = value
        End Set
    End Property
    Public Property SubElementproperty() As String
        Get
            Return _SubElementpropertyId
        End Get
        Set(ByVal value As String)
            _SubElementpropertyId = value
        End Set
    End Property
End Class
Public Class ColAssociatedLiquidData
    Inherits CollectionBase

    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    'Remove Item at specified index
    Public Sub Remove(ByVal index As Integer)
        ' Check to see if there is a widget at the supplied index.
        If index > Count - 1 Or index < 0 Then
            ' If no object exists, a messagebox is shown and the operation is 
            ' cancelled.
            'System.Windows.Forms.MessageBox.Show("Index not valid!")
        Else
            ' Invokes the RemoveAt method of the List object.
            List.RemoveAt(index)
        End If
    End Sub
    ' This line declares the Item property as ReadOnly, and 
    ' declares that it will return a SentFax object.
    Public ReadOnly Property Item(ByVal index As Integer) As clsAssocatedLiquidData
        Get
            ' The appropriate item is retrieved from the List object and 
            ' explicitly cast to the SentFax type, then returned to the 
            ' caller.
            Return CType(List.Item(index), clsAssocatedLiquidData)
        End Get
    End Property
    ' Restricts to SentFax types, items that can be added to the collection.
    Public Sub Add(ByVal oConditionfield As clsAssocatedLiquidData)
        ' Invokes Add method of the List object to add a SentFax.
        List.Add(oConditionfield)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                Me.Clear()
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub
End Class
Public Class clsLiquiddbLayer
    Public Sub New()

    End Sub
   
    Public Function IsHistoryPresent(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal CategoryName As String) As Boolean
        Dim oconn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing
        Try
            ' Dim ODB As New ClsDBLayer slr not used 
            Dim _result As Object
            oconn = New System.Data.SqlClient.SqlConnection(GetConnectionString())
            '   Dim strQRy As String = "SELECT Count(nHistoryID) from History WHERE nPatientID = " & PatientID & " AND nVisitID = " & VisitID & " AND sHistoryCategory = '" & CategoryName & "'"
            Dim strQRy As String = ""
            If CategoryName <> "Past Medical History" Then
                strQRy = "SELECT Count(nHistoryID) from History WHERE nPatientID = " & PatientID & " AND nVisitID = " & VisitID & " AND sHistoryCategory = '" & CategoryName & "'"
            Else
                strQRy = "SELECT Count(nHistoryID) from History WHERE nPatientID = " & PatientID & " AND nVisitID = " & VisitID & " AND sHistoryCategory <>'Family History' And sHistoryCategory <>'Social History'  "

            End If

            oCmd = New SqlCommand(strQRy, oconn)
            If oconn.State = ConnectionState.Closed Then
                oconn.Open()
            End If

            _result = oCmd.ExecuteScalar()

            If Convert.ToInt64(_result) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If oconn.State = ConnectionState.Open Then
                oconn.Close()
            End If
            If Not IsNothing(oconn) Then
                oconn.Dispose()
            End If
            oconn = Nothing
            If Not IsNothing(oCmd) Then  ''slr free oconn, ocmd
                oCmd.Parameters.Clear()
                oCmd.Dispose()
            End If
            oCmd = Nothing
        End Try
    End Function

    ''Shubhangi 20090228''
    Public Function GetPatientFlowSheet(ByVal PatientID As Int64, ByVal VisitID As Int64) As DataTable
        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable = Nothing
        Try
            '  dt = New DataTable  ''slr new not needed 
            With odb
                Dim _strSql As String = "SELECT DISTINCT FlowSheet1.sFlowSheetName, FlowSheet1.sFieldName, FlowSheet1.sValue, FlowSheet1.sDataType, FlowSheet1.nRowIndex, FlowSheet1.sUserName, " _
                                      & "FlowSheet1.sMachineName, AssociatedEMField.sAssociatedEMName,AssociatedEMField.sAssociatedEMCategory, FlowSheet1.nVisitID, FlowSheet1.nPatientID,AssociatedEMField.sStatus  " _
                                      & "FROM FlowSheet1 INNER JOIN " _
                                      & "FlowSheet_MST ON FlowSheet1.sFlowSheetName = FlowSheet_MST.sFlowSheetName INNER JOIN " _
                                      & "AssociatedEMField ON FlowSheet_MST.nFlowSheetID = AssociatedEMField.nFieldID " _
                                      & "WHERE nVisitID = " & VisitID & " And nPatientID = " & PatientID & " "
                dt = odb.GetDataTable_Query(_strSql)
            End With
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then  ''slr free odb
                odb.Dispose()
            End If
            odb = Nothing
        End Try
    End Function

    ''End Shubhangi''

    Public Function IsChiefComplentPresentProblemList(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal dos As DateTime) As Boolean
        Dim oconn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing
        Try
            'Dim ODB As New ClsDBLayer  slr odb not needed 
            Dim _result As Object
            oconn = New System.Data.SqlClient.SqlConnection(GetConnectionString())
            Dim strQRy As String = "Select COUNT(*) FROM ProblemList WHERE nPatientID = " & PatientID & " AND dtDOS = '" & dos & "'" ''nVisitID = " & VisitID & " AND 
            oCmd = New SqlCommand(strQRy, oconn)
            If oconn.State = ConnectionState.Closed Then
                oconn.Open()
            End If

            _result = oCmd.ExecuteScalar()
            If Convert.ToUInt16(_result) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If oconn.State = ConnectionState.Open Then  ''slr free cmd,con
                oconn.Close()
            End If
            If Not IsNothing(oconn) Then
                oconn.Dispose()
            End If
            oconn = Nothing

            If Not IsNothing(oCmd) Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
            End If
            oCmd = Nothing
        End Try
    End Function
    'Shubhangi 20090305'

    Public Function IsChiefComplentPresent(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal ExamID As Int64, ByVal VisitDate As DateTime) As Boolean
        Dim oconn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing
        Try
            '   Dim ODB As New ClsDBLayer   slr not used
            Dim _result As Object
            oconn = New System.Data.SqlClient.SqlConnection(GetConnectionString())
            Dim strQRy As String = "Select COUNT(*) FROM PatientChiefComplaint WHERE nPatientID = '" & PatientID & "' AND nVisitID = '" & VisitID & "'" '' AND nExamID = '" & ExamID & "' AND dtVisitDate = '" & VisitDate & "'"
            oCmd = New SqlCommand(strQRy, oconn)
            If oconn.State = ConnectionState.Closed Then
                oconn.Open()
            End If
            _result = oCmd.ExecuteScalar()
            If Convert.ToUInt16(_result) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If oconn.State = ConnectionState.Open Then  ''slr free oconn,ocmd
                oconn.Close()
            End If
            If Not IsNothing(oconn) Then
                oconn.Dispose()
            End If
            oconn = Nothing
            If Not IsNothing(oCmd) Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
            End If
            oCmd = Nothing

        End Try
    End Function
    'End Shubhangi'

    Public Function IsEstablishedPatient(ByVal PatientID As Int64, ByVal dos As DateTime) As Boolean
        Dim oconn As SqlConnection = Nothing
        Dim oCmd As SqlCommand = Nothing
        Try
            ' Dim ODB As New ClsDBLayer  ''slr free ODB
            Dim _result As Object
            oconn = New System.Data.SqlClient.SqlConnection(GetConnectionString())
            Dim strQRy As String = "SELECT COUNT(nVisitID) FROM Visits WHERE nPatientID = " & PatientID & " AND convert(varchar,dtVisitDate,101) <> '" & dos.ToString("MM/dd/yyyy") & "'"
            oCmd = New SqlCommand(strQRy, oconn)
            oconn.Open()
            _result = oCmd.ExecuteScalar()
            If Convert.ToUInt16(_result) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If oconn.State = ConnectionState.Open Then  ''slr free oconn,ocmd
                oconn.Close()
            End If
            If Not IsNothing(oconn) Then
                oconn.Dispose()
            End If
            oconn = Nothing
            If Not IsNothing(oCmd) Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
            End If
            oCmd = Nothing

        End Try
    End Function

    Public Function GetPatientLabsOrder(ByVal PatientId As Long, ByVal _Date As Date, ByVal FieldType As Integer) As DataTable

        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable

        Try
            ' dt = New DataTable  slr not required 
            With odb
                ''Retrived AssociatedEMField.sStatus field also
                'Dim _strSql As String = " SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID,AssociatedEMField.sAssociatedEMName,ISNULL(AssociatedEMField.sAssociatedEMCategory,'') as  sAssociatedEMCategory,AssociatedEMField.sStatus  " _
                '                        & " FROM Lab_Order_TestDtl INNER JOIN " _
                '                        & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                '                        & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID INNER JOIN " _
                '                        & " AssociatedEMField ON Lab_Order_TestDtl.labotd_TestID = AssociatedEMField.nFieldID " _
                '                        & " WHERE Lab_Order_MST.labom_PatientID = " & PatientId & " AND AssociatedEMField.nFieldType = " & FieldType & ""

                Dim _strSql As String = " SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID,AssociatedEMField.sAssociatedEMName,ISNULL(AssociatedEMField.sAssociatedEMCategory,'') as  sAssociatedEMCategory, " _
                 & " AssociatedEMField.sStatus,convert(varchar(10),labom_TransactionDate ,121) as TrancDate " _
                 & " FROM Lab_Order_TestDtl INNER Join Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER Join " _
                 & "Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID INNER JOIN  AssociatedEMField ON Lab_Order_TestDtl.labotd_TestID = AssociatedEMField.nFieldID " _
                 & "WHERE Lab_Order_MST.labom_PatientID = " & PatientId & " And AssociatedEMField.nFieldType = " & FieldType & " And convert(varchar(10),labom_TransactionDate ,121) = '" & _Date.ToString("yyyy-MM-dd") & "'"
                ''And convert(varchar(10),labom_TransactionDate ,121) = convert(varchar(10),'" & _Date & "',121)"

                dt = odb.GetDataTable_Query(_strSql)
            End With
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then  ''slr free odb
                odb.Dispose()
            End If
            odb = Nothing
        End Try



    End Function
    Public Function GetPatientRadiologyOrders(ByVal PatientId As Long, ByVal _Date As Date, ByVal FieldType As Integer) As DataTable

        Dim odb As gloStream.gloDataBase.gloDataBase = Nothing ''slr new not needed 
        Dim dt As DataTable = Nothing
        Dim _strSQL As String
        Dim ds As DataSet = Nothing  ''slr new not needed 
        'Dim _Categories As New Collection
        'Dim _Groups As New Collection
        '   Dim dsTest As New DataSet  slr not used 
        '  Dim dtTemp As New DataTable  slr  not used
        Try
            odb = New gloStream.gloDataBase.gloDataBase
            odb.Connect(GetConnectionString)
            'oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '1' AND lm_category_Description IS NOT NULL ")
            '_strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
            '        & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
            '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
            '        & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & gnPatientID & ") AND (convert(varchar,LM_Orders.lm_OrderDate,101) = '" & _Date.ToString("MM/dd/yyyy") & "') " _
            '        & " ORDER BY lm_category_Description "

            '()
            'lm_test_Name("")
            'sAssociatedEMName()

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '_strSQL = "select ISNULL(AssociatedEMField.NFieldID,0) as lm_category_ID, ISNULL(AssociatedEMField.sAssociatedEMName,'') as lm_test_Name, ISNULL(AssociatedEMField.sAssociatedEMName,'') as sAssociatedEMName,ISNULL(AssociatedEMField.sAssociatedEMCategory,'') as  sAssociatedEMCategory,AssociatedEMField.sStatus from AssociatedEMField " _
            '        & " inner join LM_Test on AssociatedEMField.nFieldid=LM_Test.lm_test_ID inner join   lm_orders " _
            '        & " on LM_Test.lm_test_name=lm_orders.lm_sTestName where " _
            '        & " (LM_Test.lm_test_Name IS NOT NULL) " _
            '        & " AND (LM_Orders.lm_Patient_ID = " & gnPatientID & ") AND (convert(varchar,LM_Orders.lm_OrderDate,101) = '" & _Date.ToString("MM/dd/yyyy") & "') "

            _strSQL = "select ISNULL(AssociatedEMField.NFieldID,0) as lm_category_ID, ISNULL(AssociatedEMField.sAssociatedEMName,'') as lm_test_Name, ISNULL(AssociatedEMField.sAssociatedEMName,'') as sAssociatedEMName,ISNULL(AssociatedEMField.sAssociatedEMCategory,'') as  sAssociatedEMCategory,AssociatedEMField.sStatus from AssociatedEMField " _
                    & " inner join LM_Test on AssociatedEMField.nFieldid=LM_Test.lm_test_ID inner join   lm_orders " _
                    & " on LM_Test.lm_test_name=lm_orders.lm_sTestName where " _
                    & " (LM_Test.lm_test_Name IS NOT NULL) " _
                    & " AND (LM_Orders.lm_Patient_ID = " & PatientId & ") AND (convert(varchar,LM_Orders.lm_OrderDate,101) = '" & _Date.ToString("MM/dd/yyyy") & "') "
            'end modification
            ds = odb.ReadCatRecords(_strSQL)


            odb.Disconnect()
            odb.Dispose()
            odb = Nothing
            If IsNothing(ds) = False Then
                dt = ds.Tables(0).Copy()
                Return dt
            Else
                Return Nothing

            End If
        Catch ex As Exception
            ' gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then  ''slr free odb, ds
                odb.Dispose()
            End If
            odb = Nothing

            If Not IsNothing(ds) Then
                ds.Dispose()
            End If
            ds = Nothing
        End Try



    End Function
    Public Function GetPatientLabsTagsOrder(ByVal PatientId As Long, ByVal _Date As Date) As DataTable

        Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim dt As DataTable  ''slr new not needed

        Try
            ' dt = New DataTable  not required 
            With odb
                Dim _strSql As String = " SELECT DISTINCT Lab_Test_Mst.labtm_Name, Lab_Order_TestDtl.labotd_TestID, Lab_Test_Mst.labtm_Code, Lab_Order_MST.labom_PatientID,AssociatedEMField.sAssociatedEMName,AssociatedEMField.sStatus  " _
                                        & " FROM Lab_Order_TestDtl INNER JOIN " _
                                        & " Lab_Order_MST ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_MST.labom_OrderID INNER JOIN " _
                                        & " Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID INNER JOIN " _
                                        & " AssociatedEMField ON Lab_Order_TestDtl.labotd_TestID = AssociatedEMField.nFieldID " _
                                        & " WHERE Lab_Order_MST.labom_PatientID = " & PatientId & " AND AssociatedEMField.nFieldType = " & 4 & ""
                dt = odb.GetDataTable_Query(_strSql)
            End With
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(odb) Then  ''slr free odb
                odb.Dispose()
            End If
            odb = Nothing
        End Try



    End Function

    Public Function GetPatientRadiologyOrder(ByVal PatientId As Long, ByVal _Date As Date, ByVal FieldType As Integer) As DataTable

        Dim odb As New gloStream.gloDataBase.gloDataBase
        'Dim dt As DataTable
        Dim _strSQL As String
        Dim ds As DataSet = Nothing
        Dim _Categories As New Collection
        Dim _Groups As New Collection
        ' Dim dsTest As New DataSet
        Dim dtTemp As DataTable = Nothing
        Try
            odb = New gloStream.gloDataBase.gloDataBase
            odb.Connect(GetConnectionString)
            'oDataReader = oDB.ReadQueryRecords("SELECT lm_category_ID,lm_category_Description,lm_category_CategoryType FROM LM_Category WHERE lm_category_CategoryType = '1' AND lm_category_Description IS NOT NULL ")
            '_strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
            '        & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
            '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
            '        & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & gnPatientID & ") AND (convert(varchar,LM_Orders.lm_OrderDate,101) = '" & _Date.ToString("MM/dd/yyyy") & "') " _
            '        & " ORDER BY lm_category_Description "

            'RETRIVE THE CATEGORY DESCRIPTION OF THE OREDRS I.E. RETRIVE THE ONLY CATEGORIES OFTODAY'S DATE'S ORDER

            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '_strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
            '        & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
            '        & " LM_Orders ON LM_Test.lm_test_Name = LM_Orders.lm_sTestName " _
            '        & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & gnPatientID & ") AND (lm_test_testGroupFlag='T') AND (convert(varchar,LM_Orders.lm_OrderDate,101) = '" & _Date.ToString("MM/dd/yyyy") & "') " _
            '        & " ORDER BY lm_category_Description "
            _strSQL = " SELECT  DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description " _
                  & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                  & " LM_Orders ON LM_Test.lm_test_Name = LM_Orders.lm_sTestName " _
                  & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = " & PatientId & ") AND (lm_test_testGroupFlag='T') AND (convert(varchar,LM_Orders.lm_OrderDate,101) = '" & _Date.ToString("MM/dd/yyyy") & "') " _
                  & " ORDER BY lm_category_Description "
            'end modification

            '_strSQL = " SELECT   DISTINCT LM_Test.lm_test_CategoryID, LM_Category.lm_category_Description  " _
            '            " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
            '            " LEFT OUTER JOIN  LM_Orders ON LM_Test.lm_test_Name = LM_Orders.lm_sTestName  " _
            '            " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & gnPatientID & ")  AND (convert(varchar,LM_Orders.lm_OrderDate,101) = '" & _Date.ToString("MM/dd/yyyy") & "')" _
            '            " ORDER BY lm_category_Description "
            ds = odb.ReadCatRecords(_strSQL)

            If IsNothing(ds) = False Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If IsDBNull(ds.Tables(0).Rows(i)("lm_category_Description")) = False Then
                        _Categories.Add(ds.Tables(0).Rows(i)("lm_category_Description"))
                    End If
                Next
                ds.Dispose()
                ds = Nothing
            End If
            odb.Disconnect()
            odb.Dispose()
            odb = Nothing
            For i As Int16 = 1 To _Categories.Count
                '_strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
                '        & " FROM LM_Test LEFT OUTER JOIN " _
                '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                '        & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
                '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                '        & " WHERE (LM_Orders.lm_Patient_ID =" & gnPatientID & ") AND (convert(varchar,LM_Orders.lm_OrderDate ,101) = '" & _Date.ToString("MM/dd/yyyy") & "') AND  " _
                '        & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
                '        & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "


                'RETRIVE THE GROUP DETAILS OF THAT CATEGORY (WHICH WE GET FROM PEVIOUS QUERY)

                ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                '_strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, " _
                '          & " LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
                '          & " FROM LM_Test LEFT OUTER JOIN  LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN  " _
                '          & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN  LM_Orders ON " _
                '          & " LM_Test.lm_test_Name = LM_Orders.lm_sTestName  WHERE (LM_Orders.lm_Patient_ID =" & gnPatientID & ") AND (convert(varchar,LM_Orders.lm_OrderDate ,101) = '" & _Date.ToString("MM/dd/yyyy") & "') AND  " _
                '          & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
                '          & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "
                _strSQL = " SELECT DISTINCT  LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName, " _
                          & " LM_Test.lm_test_CategoryID, LM_Test_1.lm_test_TestGroupFlag, LM_Test.lm_test_Template_ID " _
                          & " FROM LM_Test LEFT OUTER JOIN  LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN  " _
                          & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN  LM_Orders ON " _
                          & " LM_Test.lm_test_Name = LM_Orders.lm_sTestName  WHERE (LM_Orders.lm_Patient_ID =" & PatientId & ") AND (convert(varchar,LM_Orders.lm_OrderDate ,101) = '" & _Date.ToString("MM/dd/yyyy") & "') AND  " _
                          & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
                          & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "
                'end modificattion
                odb = New gloStream.gloDataBase.gloDataBase
                odb.Connect(GetConnectionString)
                ds = odb.ReadQueryRecordAsDataSet(_strSQL)
                odb.Disconnect()
                odb.Dispose()
                odb = Nothing

                If IsNothing(ds) = False Then
                    For j As Integer = 0 To ds.Tables(0).Rows.Count - 1


                        _Groups.Add(ds.Tables(0).Rows(j)("lm_test_GroupNo"))

                        'RETRIVE THE  DETAILS OF THAT GROUP (WHICH WE GET FROM PEVIOUS QUERY)

                        ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        '_strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
                        '         & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, " _
                        '         & " LM_Orders.lm_Status, LM_Test.lm_test_Template_ID, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, " _
                        '         & " LM_Orders.lm_sICD9Description, AssociatedEMField.sAssociatedEMName,AssociatedEMField.sAssociatedEMCategory,AssociatedEMField.sStatus FROM  LM_Test INNER JOIN " _
                        '         & " LM_Orders ON LM_Test.lm_test_Name = LM_Orders.lm_sTestName INNER JOIN " _
                        '         & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID INNER JOIN " _
                        '         & " AssociatedEMField ON LM_Test.lm_test_ID = AssociatedEMField.nFieldID " _
                        '         & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & gnPatientID & ") AND (convert(varchar,LM_Orders.lm_OrderDate ,101) = '" & _Date.ToString("MM/dd/yyyy") & "') AND  " _
                        '         & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_GroupNo =" & ds.Tables(0).Rows(j)("lm_test_GroupNo") & ") " _
                        '         & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

                        _strSQL = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
                                & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, " _
                                & " LM_Orders.lm_Status, LM_Test.lm_test_Template_ID, LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, " _
                                & " LM_Orders.lm_sICD9Description, AssociatedEMField.sAssociatedEMName,AssociatedEMField.sAssociatedEMCategory,AssociatedEMField.sStatus FROM  LM_Test INNER JOIN " _
                                & " LM_Orders ON LM_Test.lm_test_Name = LM_Orders.lm_sTestName INNER JOIN " _
                                & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID INNER JOIN " _
                                & " AssociatedEMField ON LM_Test.lm_test_ID = AssociatedEMField.nFieldID " _
                                & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & PatientId & ") AND (convert(varchar,LM_Orders.lm_OrderDate ,101) = '" & _Date.ToString("MM/dd/yyyy") & "') AND  " _
                                & " (LM_Category.lm_category_Description = '" & _Categories(i) & "') AND (LM_Test.lm_test_GroupNo =" & ds.Tables(0).Rows(j)("lm_test_GroupNo") & ") " _
                                & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "
                        'end modification

                        odb = New gloStream.gloDataBase.gloDataBase
                        odb.Connect(GetConnectionString)
                        Dim dsTest As DataSet = odb.ReadQueryRecordAsDataSet(_strSQL)
                        If (IsNothing(dsTest) = False) Then


                            If j = 0 Then
                                dtTemp = dsTest.Tables(0).Clone()
                            Else
                                If (IsNothing(dtTemp)) Then
                                    dtTemp = dsTest.Tables(0).Clone()
                                End If
                            End If
                            Dim row As DataRow
                            For cnt As Integer = 0 To dsTest.Tables(0).Rows.Count - 1
                                row = dtTemp.NewRow()
                                For colcnt As Integer = 0 To dsTest.Tables(0).Columns.Count - 1
                                    row(colcnt) = dsTest.Tables(0).Rows(cnt)(colcnt)
                                Next
                                dtTemp.Rows.Add(row)
                            Next
                        End If
                        odb.Disconnect()
                        odb.Dispose()
                        odb = Nothing
                        dsTest.Dispose()
                        dsTest = Nothing
                    Next
                    ds.Dispose()
                    ds = Nothing
                End If
            Next
            If (IsNothing(dtTemp)) Then
                Return Nothing
            End If

            If dtTemp.Rows.Count > 0 Then
                Return dtTemp
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(odb) = False) Then
                odb.Disconnect()
                odb.Dispose()
                odb = Nothing
            End If
            _Categories.Clear()
            _Groups.Clear()

        End Try



    End Function

    Public Function GetPatientOTC(ByVal PatientId As Long, ByVal _Date As DateTime) As DataTable

        Dim _result As DataTable = Nothing

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParam As New gloDatabaseLayer.DBParameters
        Try

            oDB.Connect(False)
            oParam.Add("@PatientID", PatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@VisitDate", _Date, ParameterDirection.Input, SqlDbType.DateTime)
            oDB.Retrive("ENM_GetOTCDrug", oParam, _result)
            Return _result

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.EnM, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            oParam.Dispose()
            oParam = Nothing
        End Try

    End Function
    Public Function AddEMCode(ByVal nEM_ID As Int64, ByVal nExamID As Int64, ByVal nVisitID As Int64, ByVal nPatientID As Int64, ByVal nGroupID As Int64, ByVal sCode As String, ByVal sParentCat As String, ByVal sChildCat As String, ByVal sItem As String, ByVal sUserItemName As String) As Int64
        Dim oDB As DataBaseLayer = Nothing
        Dim oParamater As DBParameter = Nothing
        Dim ElementID As Int64 = 0
        Try
            oDB = New DataBaseLayer




            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nExamID"
            oParamater.Value = nExamID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nVisitID"
            oParamater.Value = nVisitID


            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"
            oParamater.Value = nPatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nGroupID"
            oParamater.Value = nGroupID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing



            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sCode"
            oParamater.Value = sCode
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sParentCat"
            oParamater.Value = sParentCat
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sChildCat"
            oParamater.Value = sChildCat
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing




            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sItem"
            oParamater.Value = sItem
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sUserItemName"
            oParamater.Value = sUserItemName
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
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.InputOutput
            oParamater.Name = "@nEM_ID"
            oParamater.Value = nEM_ID

            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing


            ElementID = oDB.Add("Insert_EMDetails")
            Return ElementID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return 0
        Finally
            If Not IsNothing(oDB) Then  ''slr free oDB

                oDB.Dispose()
            End If
            oDB = Nothing
        End Try
    End Function
End Class