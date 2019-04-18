Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Namespace gloStream

    Namespace gloImportExport

        Public Class gloImportExport

            Private _CSV_AppName As String = "[-gloEMR-]"
            Private _CSV_PatientInformation As String = "[Patient Information]"
            Private _CSV_LabTestResult As String = "[Lab Test Result]"
            Private _CSV_DotLine As String = "-----------------------------------------------------"
            Private _CSV_ParaLine As String = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
            Private _CSV_EnterResult As String = "<Enter Result Here using TAB seprator...with deleting this line>"
            Private _CSV_PatientInfo_Name As String = "Name = "
            Private _CSV_PatientInfo_Code As String = "Code = "
            Private _CSV_PatientInfo_VisitDate As String = "Visit Date = "

            Private _ErrorMessage As String

            Public Property ErrorMessage() As String
                Get
                    Return _ErrorMessage
                End Get
                Set(ByVal Value As String)
                    _ErrorMessage = Value
                End Set
            End Property

            Public Function ExportLabResultTemplate(ByVal oExportDetail As gloStream.gloImportExport.Supporting.ExportDetail, ByVal FlowSheetID As Long, ByVal ExportRootPath As String) As Boolean
                Dim _Result As Boolean = False
                Dim _ExportFilePath As String
                Dim _FlowSheetData As String
                Dim _PrerequisiteData As String = ""
                Dim _ColumnHeadingData As String = ""
                Dim oFlowSheet As gloStream.gloImportExport.Supporting.FlowSheet

                Try
                    If Not oExportDetail Is Nothing Then
                        _ExportFilePath = GetNewFileName(oExportDetail.PatientCode, oExportDetail.PatientName, ExportRootPath)
                        If File.Exists(_ExportFilePath) = False Then
                            _PrerequisiteData = _CSV_AppName & vbCrLf & vbCrLf & _CSV_PatientInformation & vbCrLf & _CSV_ParaLine & vbCrLf
                            _PrerequisiteData = _PrerequisiteData & _CSV_PatientInfo_Name & oExportDetail.PatientName & vbCrLf
                            _PrerequisiteData = _PrerequisiteData & _CSV_PatientInfo_Code & oExportDetail.PatientCode & vbCrLf
                            _PrerequisiteData = _PrerequisiteData & _CSV_PatientInfo_VisitDate & oExportDetail.VisitDate & vbCrLf & vbCrLf
                            _PrerequisiteData = _PrerequisiteData & _CSV_LabTestResult & vbCrLf & _CSV_ParaLine & vbCrLf


                            oFlowSheet = oExportDetail.FlowSheet(FlowSheetID)

                            _FlowSheetData = "[" & oFlowSheet.Name & "]" & vbCrLf & _CSV_DotLine & vbCrLf

                            For i As Int16 = 1 To oFlowSheet.Columns.Count
                                If i = 1 Then
                                    _ColumnHeadingData = oFlowSheet.Columns(i).Heading
                                Else
                                    _ColumnHeadingData = _ColumnHeadingData & vbTab & oFlowSheet.Columns(i).Heading
                                End If

                            Next

                            _FlowSheetData = _FlowSheetData & _ColumnHeadingData & vbCrLf & _CSV_DotLine & vbCrLf & _CSV_EnterResult & vbCrLf & _CSV_DotLine & vbCrLf

                            _FlowSheetData = _FlowSheetData & "[" & oFlowSheet.Name & "]"

                            If oFlowSheet.Columns.Count > 0 Then
                                Dim sw As StreamWriter = New StreamWriter(_ExportFilePath)
                                sw.Write(_PrerequisiteData & _FlowSheetData)
                                sw.Close()
                                sw.Dispose()
                                sw = Nothing

                                If File.Exists(_ExportFilePath) = True Then
                                    Return True
                                End If
                            Else
                                _ErrorMessage = "Flowsheet is not valid to enter test result"
                                ExportLabResultTemplate = Nothing
                                Exit Function
                            End If
                            oFlowSheet = Nothing
                        End If

                    End If
                Catch oError As Exception
                    _ErrorMessage = oError.Message
                End Try
                Return Nothing
            End Function

            Public Function ImportLabResultTemplate(ByVal ImportFilePath As String) As gloStream.gloImportExport.Supporting.ImportDetail
                Dim _Result As Boolean = False
                '    Dim _FileAllData As String
                '   Dim _FileTestData As String
                Dim _Line As String
                Dim LineCount As Int16 = 1

                Dim oFlowSheet As gloStream.gloImportExport.Supporting.FlowSheet = Nothing
                Dim oSupporting As New gloStream.gloImportExport.Supporting.Supporting
                Dim oImportDetail As New gloStream.gloImportExport.Supporting.ImportDetail

                Try
                    If File.Exists(ImportFilePath) = True Then
                        Dim sr1 As System.IO.StreamReader = New System.IO.StreamReader(ImportFilePath)
                        Dim _PreviousTest As String = ""
                        Dim _TestID As Long = 0
                        Dim _ColumnHeading As String = ""
                        Dim _RowNo As Int16 = 0
                        Dim _ColumnCount As Int16 = 0
                        Dim _FlowSheetLoad As Boolean = False
                        Dim _TestIDFound As Boolean = False

                        Dim _TestStart As Boolean = False

                        Do
                            _Line = sr1.ReadLine
                            If Not _Line Is Nothing Then
                                If _TestStart = False Then
                                    If Mid(_Line, 1, Len(_CSV_PatientInfo_Name)) = _CSV_PatientInfo_Name Then
                                        oImportDetail.PatientName = Mid(_Line, Len(_CSV_PatientInfo_Name))
                                    End If
                                    If Mid(_Line, 1, Len(_CSV_PatientInfo_Code)) = _CSV_PatientInfo_Code Then
                                        oImportDetail.PatientCode = Mid(_Line, Len(_CSV_PatientInfo_Code))
                                    End If
                                    If Mid(_Line, 1, Len(_CSV_PatientInfo_VisitDate)) = _CSV_PatientInfo_VisitDate Then
                                        oImportDetail.VisitDate = Mid(_Line, Len(_CSV_PatientInfo_VisitDate))
                                    End If
                                End If

                                If _Line.Trim = _CSV_LabTestResult Then
                                    _TestStart = True
                                End If

                                If _TestStart = True Then
                                    If _TestIDFound = False Then
                                        _PreviousTest = Mid(_Line, 2, Len(_Line) - 2)
                                        _TestID = GetFlowSheetIDFromTestName(_Line)
                                        If _TestID > 0 Then
                                            _TestIDFound = True
                                            oImportDetail.FlowSheetName = _PreviousTest
                                        End If

                                    End If

                                    If _TestID > 0 Then

                                        If _FlowSheetLoad = False Then
                                            oFlowSheet = oSupporting.GetFlowSheet(_TestID)
                                            _RowNo = 0
                                            _ColumnCount = oFlowSheet.NoOfColumns
                                            _FlowSheetLoad = True
                                        End If

                                        If Not oFlowSheet Is Nothing Then
                                            If _ColumnHeading.Trim = "" Then
                                                For i As Int16 = 1 To oFlowSheet.Columns.Count
                                                    If i = 1 Then
                                                        _ColumnHeading = oFlowSheet.Columns(i).Heading
                                                    Else
                                                        _ColumnHeading = _ColumnHeading & vbTab & oFlowSheet.Columns(i).Heading
                                                    End If
                                                Next
                                            End If

                                            If _Line <> _CSV_ParaLine AndAlso _Line <> _CSV_DotLine AndAlso _Line <> _ColumnHeading AndAlso _Line <> "[" & _PreviousTest & "]" Then
                                                If _Line.Trim <> "" Then
                                                    Dim oRow As New gloStream.gloImportExport.Supporting.FlowSheetRow

                                                    With oRow
                                                        .No = _RowNo + 1
                                                        Dim _DataArray As String() = Split(_Line, vbTab)
                                                        If Not _DataArray Is Nothing Then
                                                            For i As Int16 = 0 To _DataArray.Length - 1
                                                                If i + 1 <= _ColumnCount Then
                                                                    .Columns.Add(oFlowSheet.Columns(i + 1).No, oFlowSheet.Columns(i + 1).Heading, oFlowSheet.Columns(i + 1).DataType, oFlowSheet.Columns(i + 1).Width, _DataArray(i))
                                                                End If
                                                            Next
                                                        End If
                                                    End With

                                                    If Not oRow Is Nothing Then
                                                        oImportDetail.Rows.Add(oRow)
                                                    End If

                                                    oRow = Nothing

                                                End If
                                            End If
                                        End If

                                    End If

                                End If
                            End If

                        Loop Until _Line Is Nothing
                        sr1.Close()
                        sr1.Dispose()
                        sr1 = Nothing
                        oFlowSheet = Nothing

                        Return oImportDetail
                    Else
                        oImportDetail.Dispose()
                        oImportDetail = Nothing
                        Return oImportDetail
                    End If

                Catch oError As Exception
                    _ErrorMessage = oError.Message
                    Return Nothing
                Finally
                    oSupporting = Nothing
                End Try
            End Function

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub

            Private Function GetNewFileName(ByVal PatientCode As String, ByVal TestName As String, ByVal ExportPath As String) As String
                Try
                    Dim _ExportPath As String = ExportPath
                    Dim _DateTimeStamp As String = Format(Date.Now, "yyyymmddhhmm")
                    _ExportPath = _ExportPath & "\" & "gloTest_" & _DateTimeStamp & "_" & PatientCode & "_" & TestName
                    If File.Exists(_ExportPath) = True Then
                        File.Delete(_ExportPath)
                    End If
                    Return _ExportPath & ".txt"
                Catch oError As Exception
                    _ErrorMessage = oError.Message
                    Return Nothing
                End Try
            End Function

            Private Function GetFlowSheetIDFromTestName(ByVal TestName As String) As Long
                Dim _Result As Long = 0
                Dim _TempReturn As String = ""
                Try

                    If TestName.Trim <> "" Then
                        If Mid(TestName, 1, 1) = "[" AndAlso Mid(TestName, Len(TestName)) = "]" Then
                            Dim _TestName As String = Mid(TestName, 2, Len(TestName) - 2)
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            oDB.Connect(GetConnectionString)
                            _TempReturn = oDB.ExecuteQueryScaler("SELECT DISTINCT nFlowSheetID FROM LM_LABRESULT_MST WHERE sFlowSheetName = '" & _TestName & "'") & ""
                            If Val(_TempReturn) > 0 Then
                                _Result = CLng(_TempReturn)
                            End If
                            oDB.Disconnect()
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    End If

                    Return _Result
                Catch ex As Exception
                    Return Nothing
                End Try
            End Function

        End Class

        Namespace Supporting

            Public Class Supporting

                Public Function GetFlowSheet(ByVal FlowSheetID As Long) As gloStream.gloImportExport.Supporting.FlowSheet

                    Dim dt As DataTable
                    Dim ocls_LM_LabResult As New cls_LM_LabResult
                  

                    '''' Get FlowSheet Details
                    ocls_LM_LabResult.SelectFlowSheet(FlowSheetID)
                    dt = ocls_LM_LabResult.GetDataview.Table
                    ocls_LM_LabResult.Dispose()
                    ocls_LM_LabResult = Nothing

                    If IsNothing(dt) Then
                        GetFlowSheet = Nothing
                        Exit Function
                    End If
                    Dim oFlowSheet As New gloStream.gloImportExport.Supporting.FlowSheet
                    '   Dim oFlowSheetColumns As New gloStream.gloImportExport.Supporting.FlowSheetColumns
                    Dim oFlowSheetColumn As gloStream.gloImportExport.Supporting.FlowSheetColumn
                    Dim i As Integer
                    '' 0-sFlowSheetName ,1-nCols ,2-nColNumber ,3-sColumnName ,4-sFormat ,5-dWidth ,6-sFontName ,7-nFontSize ,
                    '' 8-nForeColor, 9-bIsBold, 10-bIsItalic, 11-bIsUnderline, 12-sAlignment, 13-nBackColor
                    If dt.Rows.Count > 0 Then
                        With oFlowSheet
                            .ID = FlowSheetID
                            .Name = CType(dt.Rows(0)(0), String)  ''FlowSheet Name
                            .NoOfColumns = dt.Rows.Count  ''Total No Of Columns in Flowsheet

                            For i = 0 To dt.Rows.Count - 1
                                oFlowSheetColumn = New gloStream.gloImportExport.Supporting.FlowSheetColumn
                                With oFlowSheetColumn
                                    .No = dt.Rows(i)(2) '' Column No
                                    .Heading = CType(dt.Rows(i)(3), String) ''Column Name
                                    .DataType = GetDataType(dt.Rows(i)(4)) ''Format
                                    .Width = CType(dt.Rows(i)(5), Integer) ''Column Width

                                    oFlowSheet.Columns.Add(oFlowSheetColumn)
                                End With
                                oFlowSheetColumn = Nothing
                            Next
                        End With
                    End If
                    dt.Dispose()
                    dt = Nothing
                    Return oFlowSheet
                End Function

                Private Function GetDataType(ByVal StrType As String) As Type

                    Select Case StrType
                        Case "General"
                            Return GetType(System.String)
                            ''cmbFormat.Items.Add("1234") ''Int64
                        Case "1234"
                            Return GetType(System.Int64)
                        Case "1234.00"
                            Return GetType(System.Double)
                        Case "-1,234.00"
                            Return GetType(System.Double)
                        Case "(1,234.00)"
                            Return GetType(System.Double)
                        Case "(1234.00)"
                            Return GetType(System.Double)
                        Case "-1234.00"
                            Return GetType(System.Double)
                        Case "-$1234.00"
                            Return GetType(System.Double)
                        Case "$1,234.00"
                            Return GetType(System.Double)

                        Case "Percentage"
                            Return GetType(System.Double)

                        Case "MM/DD/YYYY"
                            Return GetType(System.DateTime)
                        Case "DD/MM/YYYY"
                            Return GetType(System.DateTime)

                        Case "DD/MMMM/YYYY"
                            Return GetType(System.DateTime)

                        Case "MMMM DD/YYYY"
                            Return GetType(System.DateTime)

                        Case "HH:MM:ss"
                            Return GetType(System.DateTime)
                        Case "HH:MM"
                            Return GetType(System.DateTime)
                        Case "HH:MM:ss PM"
                            Return GetType(System.DateTime)
                        Case "HH:MM PM"
                            Return GetType(System.DateTime)
                        Case "Masked"
                            Return GetType(System.String)
                        Case Else
                            Return Nothing
                    End Select
                End Function

                Private Function GetMask(ByVal StrType As String) As String
                    Select Case StrType

                        Case "1234"
                            Return "####"
                        Case "1234.00"
                            Return "####.##"
                        Case "-1,234.00"
                            Return "-#,###.##"
                        Case "(1,234.00)"
                            Return "(#,###.##)"
                        Case "(1234.00)"
                            Return "(####.##)"

                        Case "-1234.00"
                            Return "-####.##"
                        Case "-$1234.00"
                            Return "-$####.##"
                        Case "$1,234.00"
                            Return "$#,###.##"

                        Case "Percentage"
                            Return "0%"

                        Case "MM/DD/YYYY"
                            Return "MM/dd/yyyy"
                        Case "DD/MM/YYYY"
                            Return "dd/MM/yyyy"
                        Case "MMMM DD/YYYY"
                            Return "MMMM dd/yyyy"
                        Case "DD/MMMM/YYYY"
                            Return "dd/MMMM/yyyy"

                        Case "HH:MM:ss"
                            Return "hh:mm:ss"
                        Case "HH:MM"
                            Return "hh:mm"
                        Case "HH:MM:ss PM"
                            Return "hh:mm:ss tt"
                        Case "HH:MM PM"
                            Return "hh:mm tt"
                        Case StrType
                            Return StrType
                        Case Else
                            Return Nothing
                    End Select
                End Function

                Public Shared Function WriteFile(ByVal ApplicationName As String, ByVal ElementName As String, ByVal Value As String, ByVal FileName As String) As Boolean
                    Dim _WriteData As String = ""
                    Dim _Line As String
                    Dim LineCount As Int16 = 1

                    'Read Data
                    If File.Exists(FileName) Then
                        Dim sr1 As System.IO.StreamReader = New System.IO.StreamReader(FileName)
                        Do
                            _Line = sr1.ReadLine
                            If Not _Line Is Nothing Then
                                If LineCount = 1 Then
                                    If _Line <> "[" & ApplicationName & "]" Then
                                        Exit Do
                                    Else
                                        _WriteData = "[" & ApplicationName & "]" & vbCrLf
                                    End If
                                Else
                                    If _Line.Trim <> "" Then
                                        If ElementName <> Mid(_Line, 1, InStr(_Line, "=") - 1) Then
                                            _WriteData = _WriteData & _Line & vbCrLf
                                        End If
                                    End If
                                End If
                            End If
                            LineCount = LineCount + 1
                        Loop Until _Line Is Nothing
                        sr1.Close()
                        sr1.Dispose()
                        sr1 = Nothing
                        If _WriteData <> "" Then
                            _WriteData = _WriteData & ElementName & "=" & Value
                        End If
                    Else
                        _WriteData = "[" & ApplicationName & "]" & vbCrLf
                        _WriteData = _WriteData & ElementName & "=" & Value
                    End If

                    'Write File
                    If File.Exists(FileName) Then
                        File.Delete(FileName)
                    End If

                    Dim sw As StreamWriter = New StreamWriter(FileName)
                    sw.Write(_WriteData)
                    sw.Close()
                    sw.Dispose()
                    sw = Nothing

                    Return Nothing
                End Function

                Public Shared Function ReadFile(ByVal ApplicationName As String, ByVal ElementName As String, ByVal FileName As String) As String
                    Dim _Line As String
                    Dim LineCount As Int16 = 1
                    Dim _Result As String = ""

                    'Read Data
                    If File.Exists(FileName) Then
                        Dim sr1 As System.IO.StreamReader = New System.IO.StreamReader(FileName)
                        Do
                            _Line = sr1.ReadLine
                            If Not _Line Is Nothing Then
                                If LineCount = 1 Then
                                    If _Line <> "[" & ApplicationName & "]" Then
                                        Exit Do
                                    End If
                                Else
                                    If _Line.Trim <> "" Then
                                        If ElementName = Mid(_Line, 1, InStr(_Line, "=") - 1) Then
                                            _Result = Mid(_Line, InStr(_Line, "=") + 1)
                                            Exit Do
                                        End If
                                    End If
                                End If
                            End If
                            LineCount = LineCount + 1
                        Loop Until _Line Is Nothing
                        sr1.Close()
                        sr1.Dispose()
                        sr1 = Nothing
                    End If

                    Return _Result
                End Function

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

            End Class

            Public Class ImportDetail
                Private _Code As String
                Private _Name As String
                Private _VisitDate As String
                Private _FlowSheetName As String
                Private _Rows As gloStream.gloImportExport.Supporting.FlowSheetRows
                Private bAssigned As Boolean = False
                Public Sub Dispose()
                    If (bAssigned) Then
                        _Rows.Dispose()
                        bAssigned = False
                    End If
                End Sub

                Public Property PatientCode() As String
                    Get
                        Return _Code
                    End Get
                    Set(ByVal Value As String)
                        _Code = Value
                    End Set
                End Property

                Public Property PatientName() As String
                    Get
                        Return _Name
                    End Get
                    Set(ByVal Value As String)
                        _Name = Value
                    End Set
                End Property

                Public Property VisitDate() As String
                    Get
                        Return _VisitDate
                    End Get
                    Set(ByVal Value As String)
                        _VisitDate = Value
                    End Set
                End Property

                Public Property FlowSheetName() As String
                    Get
                        Return _FlowSheetName
                    End Get
                    Set(ByVal Value As String)
                        _FlowSheetName = Value
                    End Set
                End Property

                Public Property Rows() As gloStream.gloImportExport.Supporting.FlowSheetRows
                    Get
                        Return _Rows
                    End Get
                    Set(ByVal Value As gloStream.gloImportExport.Supporting.FlowSheetRows)
                        If (bAssigned) Then
                            _Rows.Dispose()
                            bAssigned = False
                        End If
                        _Rows = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _Rows = New gloStream.gloImportExport.Supporting.FlowSheetRows
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _Rows = Nothing
                    MyBase.Finalize()
                End Sub

            End Class

            Public Class ExportDetail
                Private _Code As String
                Private _Name As String
                Private _VisitDate As String

                Public Property PatientCode() As String
                    Get
                        Return _Code
                    End Get
                    Set(ByVal Value As String)
                        _Code = Value
                    End Set
                End Property

                Public Property PatientName() As String
                    Get
                        Return _Name
                    End Get
                    Set(ByVal Value As String)
                        _Name = Value
                    End Set
                End Property

                Public Property VisitDate() As String
                    Get
                        Return _VisitDate
                    End Get
                    Set(ByVal Value As String)
                        _VisitDate = Value
                    End Set
                End Property

                Public ReadOnly Property FlowSheet(ByVal FlowSheetID As Long) As gloStream.gloImportExport.Supporting.FlowSheet
                    Get
                        Dim oSupporting As New gloStream.gloImportExport.Supporting.Supporting
                        Dim _FlowSheet As gloStream.gloImportExport.Supporting.FlowSheet = oSupporting.GetFlowSheet(FlowSheetID)

                        oSupporting = Nothing
                        Return _FlowSheet
                    End Get
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub
            End Class

            '// FloSheet Details //
            Public Class FlowSheet
                Private _ID As Long
                Private _Name As String
                Private _NoOfColumns As Integer
                Private _Columns As gloStream.gloImportExport.Supporting.FlowSheetColumns
                Private bAssigned As Boolean = False
                Public Sub Dispose()
                    If (bAssigned) Then
                        _Columns.Dispose()
                        bAssigned = False
                    End If
                End Sub
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

                Public Property NoOfColumns() As Integer
                    Get
                        Return _NoOfColumns
                    End Get
                    Set(ByVal Value As Integer)
                        _NoOfColumns = Value
                    End Set
                End Property

                Public Property Columns() As gloStream.gloImportExport.Supporting.FlowSheetColumns
                    Get
                        Return _Columns
                    End Get
                    Set(ByVal Value As gloStream.gloImportExport.Supporting.FlowSheetColumns)
                        If (bAssigned) Then
                            _Columns.Dispose()
                            bAssigned = False
                        End If
                        _Columns = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _Columns = New gloStream.gloImportExport.Supporting.FlowSheetColumns
                    bAssigned = True
                End Sub

                Protected Overrides Sub Finalize()
                    _Columns = Nothing
                    MyBase.Finalize()
                End Sub
            End Class

            '// Flowsheet Column Detail //
            Public Class FlowSheetColumn
                Private _No As Integer
                Private _Heading As String
                Private _DataType As System.Type
                Private _Width As Double

                Public Property No() As Integer
                    Get
                        Return _No
                    End Get
                    Set(ByVal Value As Integer)
                        _No = Value
                    End Set
                End Property

                Public Property Heading() As String
                    Get
                        Return _Heading
                    End Get
                    Set(ByVal Value As String)
                        _Heading = Value
                    End Set
                End Property

                Public Property DataType() As System.Type
                    Get
                        Return _DataType
                    End Get
                    Set(ByVal Value As System.Type)
                        _DataType = Value
                    End Set
                End Property

                Public Property Width() As Double
                    Get
                        Return _Width
                    End Get
                    Set(ByVal Value As Double)
                        _Width = Value
                    End Set
                End Property


                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

            End Class

            '// Column Collection //
            Public Class FlowSheetColumns
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByVal No As Integer, ByVal Heading As String, ByVal DataType As System.Type, ByVal Width As Double) As gloStream.gloImportExport.Supporting.FlowSheetColumn
                    Dim objNewMember As gloStream.gloImportExport.Supporting.FlowSheetColumn
                    objNewMember = New gloStream.gloImportExport.Supporting.FlowSheetColumn
                    objNewMember.No = No
                    objNewMember.Heading = Heading
                    objNewMember.DataType = DataType
                    objNewMember.Width = Width
                    mCol.Add(objNewMember)
                    Add = objNewMember
                    objNewMember = Nothing
                End Function

                Public Function Add(ByVal objNewMember As gloStream.gloImportExport.Supporting.FlowSheetColumn) As gloStream.gloImportExport.Supporting.FlowSheetColumn
                    mCol.Add(objNewMember)
                    Add = objNewMember
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.gloImportExport.Supporting.FlowSheetColumn
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

            '*********IMPORT RELATED OBJECTS***************

            '// Flowsheet Data(Import) Column Detail //
            Public Class FlowSheetDataColumn
                Private _No As Integer
                Private _Heading As String
                Private _DataType As System.Type
                Private _Width As Double
                Private _Data As String

                Public Property No() As Integer
                    Get
                        Return _No
                    End Get
                    Set(ByVal Value As Integer)
                        _No = Value
                    End Set
                End Property

                Public Property Heading() As String
                    Get
                        Return _Heading
                    End Get
                    Set(ByVal Value As String)
                        _Heading = Value
                    End Set
                End Property

                Public Property DataType() As System.Type
                    Get
                        Return _DataType
                    End Get
                    Set(ByVal Value As System.Type)
                        _DataType = Value
                    End Set
                End Property

                Public Property Width() As Double
                    Get
                        Return _Width
                    End Get
                    Set(ByVal Value As Double)
                        _Width = Value
                    End Set
                End Property

                Public Property Data() As String
                    Get
                        Return _Data
                    End Get
                    Set(ByVal Value As String)
                        _Data = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                End Sub

                Protected Overrides Sub Finalize()
                    MyBase.Finalize()
                End Sub

            End Class

            '// Flowsheet Data Column Collection //
            Public Class FlowSheetDataColumns
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByVal No As Integer, ByVal Heading As String, ByVal DataType As System.Type, ByVal Width As Double, Optional ByVal Data As String = "") As gloStream.gloImportExport.Supporting.FlowSheetDataColumn
                    Dim objNewMember As gloStream.gloImportExport.Supporting.FlowSheetDataColumn
                    objNewMember = New gloStream.gloImportExport.Supporting.FlowSheetDataColumn
                    objNewMember.No = No
                    objNewMember.Heading = Heading
                    objNewMember.DataType = DataType
                    objNewMember.Width = Width
                    objNewMember.Data = Data
                    mCol.Add(objNewMember)
                    Add = objNewMember
                    objNewMember = Nothing
                End Function

                Public Function Add(ByVal objNewMember As gloStream.gloImportExport.Supporting.FlowSheetDataColumn) As gloStream.gloImportExport.Supporting.FlowSheetDataColumn
                    mCol.Add(objNewMember)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.gloImportExport.Supporting.FlowSheetDataColumn
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

            '// Flowsheet Data(Import) Row Detail //
            Public Class FlowSheetRow
                Private _No As Integer
                Private _Columns As gloStream.gloImportExport.Supporting.FlowSheetDataColumns
                Private bAssinged As Boolean = False
                Public Sub Dispose()
                    If (bAssinged) Then
                        _Columns.Dispose()
                        bAssinged = False
                    End If
                End Sub
                

                Public Property No() As Integer
                    Get
                        Return _No
                    End Get
                    Set(ByVal Value As Integer)
                        _No = Value
                    End Set
                End Property

                Public Property Columns() As gloStream.gloImportExport.Supporting.FlowSheetDataColumns
                    Get
                        Return _Columns
                    End Get
                    Set(ByVal Value As gloStream.gloImportExport.Supporting.FlowSheetDataColumns)
                        If (bAssinged) Then
                            _Columns.Dispose()
                            bAssinged = False
                        End If
                        _Columns = Value
                    End Set
                End Property

                Public Sub New()
                    MyBase.new()
                    _Columns = New gloStream.gloImportExport.Supporting.FlowSheetDataColumns
                    bAssinged = True
                End Sub

                Protected Overrides Sub Finalize()
                    _Columns = Nothing
                    MyBase.Finalize()
                End Sub

            End Class

            '// Flowsheet Row Collection //
            Public Class FlowSheetRows
                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByVal objNewMember As gloStream.gloImportExport.Supporting.FlowSheetRow) As gloStream.gloImportExport.Supporting.FlowSheetRow
                    mCol.Add(objNewMember)
                    Return Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As gloStream.gloImportExport.Supporting.FlowSheetRow
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

            '*********IMPORT RELATED OBJECTS***************



        End Namespace

    End Namespace
End Namespace
