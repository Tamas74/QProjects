Imports System.IO
Imports System.Data.SqlClient
Namespace gloStream

    
    'Document Class
    Public Class Document
        Private _IsPublisher As Boolean
        Private _ErrorMessage As String
        Public Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
            Set(ByVal Value As String)
                _ErrorMessage = Value
            End Set
        End Property

        Public Function IsExistInExplorer(ByVal RootPath As String, ByVal oDocument As gloStream.CategorisedDocument) As Boolean
            Dim oSupport As New gloStream.Supporting.PathsAndFolders
            Try
                IsExistInExplorer = False
                If Not oDocument Is Nothing Then
                    Dim _TempPath As String = oSupport.GetDMSPath(RootPath) & oDocument.SystemFolder & "\" & oDocument.Container & "\" & oDocument.Category & "\" & oDocument.PatientID & "\" & oDocument.Year & "\" & oDocument.Month & "\" & oDocument.FileName & "." & oDocument.Extension
                    If File.Exists(_TempPath) = True Then
                        Return True
                    End If
                End If
            Catch objError As Exception
                Exit Function
            Finally
                oSupport = Nothing
            End Try
        End Function

        'Public Function PageCountFromFile(ByVal DocumentPath As String) As Integer
        '    Dim _PageCount As Integer = 0
        '    'easyPDFSDK Object
        '    Dim oEasyPDFProcessor As BEPPROCLib.PDFProcessor
        '    oEasyPDFProcessor = CreateObject("easyPdfSdk.PDFProcessor")
        '    oEasyPDFProcessor.LicenseKey = "474D-F1FD-B32F-2209-FE5E"
        '    Try
        '        If File.Exists(DocumentPath) = True Then
        '            _PageCount = oEasyPDFProcessor.GetPageCount(DocumentPath)
        '            Return _PageCount
        '        End If
        '    Catch ex As Exception
        '        Exit Function
        '    Finally
        '        oEasyPDFProcessor = Nothing
        '    End Try
        'End Function

        Public Function HasNote(ByVal DocumentID As Long) As Boolean
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlDataReader                        ' Data Reader

            Dim _SQLQuery As String
            Dim _DocumentID As Long
            Dim _Records As Integer = 0
            Dim _Result As Boolean = False

            Try
                _DocumentID = DocumentID

                'Set Note with Document ID
                If _DocumentID <> 0 Then
                    If _IsPublisher = True Then
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Publisher))
                    Else
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Subscriber))
                    End If
                    'Document ID
                    oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)

                    _Records = CInt(oDB.ExecuteScaler("gsp_DMS_HasNote"))
                    If _Records > 0 Then
                        _Result = True
                    End If
                    oDB.Disconnect()
                End If

                Return _Result
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB.Disconnect()
                oDB = Nothing
                oDataReader = Nothing

                _SQLQuery = Nothing
                _DocumentID = Nothing
            End Try
        End Function

        Public Function AddNote(ByVal Note As String, ByVal PageNo As Integer, ByVal DocumentID As Long) As Boolean
            Dim oSupporting As New gloStream.Supporting.Supporting
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlDataReader                        ' Data Reader

            Dim _SQLQuery As String
            Dim _DocumentID As Long
            Dim _NoteID As Integer = 1
            Dim _NoteDateTime As String = oSupporting.NewDocumentName(0, "", Supporting.enumDocumentType.None, Supporting.ConnectionType.Publisher)
            Dim _Result As Boolean = False

            Try
                _DocumentID = DocumentID

                'Set Note with Document ID
                If _DocumentID <> 0 Then
                    If _IsPublisher = True Then
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Publisher))
                    Else
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Subscriber))
                    End If
                    'Document ID
                    oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Page ID
                    oDB.DBParameters.Add("@PageID", PageNo, ParameterDirection.Input, SqlDbType.Int)
                    'NoteDateTime
                    oDB.DBParameters.Add("@NoteDateTime", _NoteDateTime, ParameterDirection.Input, SqlDbType.VarChar)
                    'Note
                    oDB.DBParameters.Add("@Note", Note, ParameterDirection.Input, SqlDbType.VarChar)

                    If oDB.ExecuteNonQuery("gsp_DMS_AddNote") = True Then
                        _Result = True
                    End If
                    oDB.Disconnect()
                End If

                Return _Result
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB.Disconnect()
                oSupporting = Nothing
                oDB = Nothing
                oDataReader = Nothing

                _SQLQuery = Nothing
                _DocumentID = Nothing
                _NoteID = Nothing
                _NoteDateTime = Nothing
            End Try
        End Function

        Public Function DeleteNote(ByVal PageNo As Integer, ByVal DocumentID As Long) As Boolean
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlDataReader                        ' Data Reader

            Dim _SQLQuery As String
            Dim _DocumentID As Long
            Dim _Result As Boolean = False

            Try
                _DocumentID = DocumentID

                'Set Note with Document ID
                If _DocumentID <> 0 Then
                    If _IsPublisher = True Then
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Publisher))
                    Else
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Subscriber))
                    End If
                    'Document ID
                    oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Page ID
                    oDB.DBParameters.Add("@PageID", PageNo, ParameterDirection.Input, SqlDbType.Int)

                    If oDB.ExecuteNonQuery("gsp_DMS_DeleteNote") = True Then
                        _Result = True
                    End If
                    oDB.Disconnect()
                End If

                Return _Result
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB.Disconnect()
                oDB = Nothing
                oDataReader = Nothing

                _SQLQuery = Nothing
                _DocumentID = Nothing
            End Try
        End Function

        Public Function ViewNote(ByVal PageNo As Integer, ByVal DocumentID As Long) As String
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlDataReader                        ' Data Reader
            Dim _SQLQuery As String
            Dim _DocumentID As Long
            Dim _Note As String = ""
            Try
                _DocumentID = DocumentID

                'Set Note with Document ID
                If _DocumentID <> 0 Then
                    If _IsPublisher = True Then
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Publisher))
                    Else
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Subscriber))
                    End If
                    'Document ID
                    oDB.DBParameters.Add("@DocumentID", _DocumentID, ParameterDirection.Input, SqlDbType.BigInt)
                    'Page ID
                    oDB.DBParameters.Add("@PageID", PageNo, ParameterDirection.Input, SqlDbType.Int)
                    oDataReader = oDB.ReadRecords("gsp_DMS_ViewNote")
                    If oDataReader.HasRows = True Then
                        While oDataReader.Read
                            _Note = Trim(oDataReader.Item(0) & "")
                        End While
                    End If
                    oDB.Disconnect()
                End If
                Return _Note
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB.Disconnect()
                oDB = Nothing
                oDataReader = Nothing
                _SQLQuery = Nothing
                _DocumentID = Nothing
                _Note = Nothing
            End Try
        End Function

        Public Function GetCategorisedDocument(ByVal Path As String, ByVal SynchronizationInfo As Boolean, Optional ByVal Archived As Boolean = False) As CategorisedDocument
            Dim _CategorisedDocument As New gloStream.CategorisedDocument(_IsPublisher)
            Dim _PathFolder As New gloStream.Supporting.PathsAndFolders
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlDataReader                        ' Data Reader
            'DMS System\Container\Corresponds\388335464231046001\2006\April\388335464231046001.PDF
            Dim _Path As String
            Dim _DMSPath As String = ""
            If _IsPublisher = True Then
                _DMSPath = _PathFolder.GetDMSPath(DMSPublisherRootPath)
            Else
                _DMSPath = _PathFolder.GetDMSPath(DMSSubscriberRootPath)
            End If

            Dim _PathSplitter() As String
            Dim i As Integer

            Dim _Name As String = "", _FileName As String = "", _Extension As String = "", _Month As String = "", _Year As String = "", _Category As String = "", _Container As String = "", _SystemFolder As String = ""
            Dim _PatientID As Long = 0
            Dim _SQLQuery As String = ""

            Try
                If File.Exists(Path) = True Then
                    _Path = Replace(Path, _DMSPath, "")
                    If Trim(_Path) <> "" Then
                        _PathSplitter = Split(_Path, "\")
                        'Split Path into Properties
                        For i = 0 To UBound(_PathSplitter)
                            Select Case i
                                Case 0 ' System Folder
                                    _SystemFolder = Trim(_PathSplitter(i))
                                Case 1 ' Container
                                    _Container = Trim(_PathSplitter(i))
                                Case 2 ' Category
                                    _Category = Trim(_PathSplitter(i))
                                Case 3 ' PatientID
                                    _PatientID = CLng(_PathSplitter(i))
                                Case 4 ' Year
                                    _Year = Trim(_PathSplitter(i))
                                Case 5 ' Month
                                    _Month = Trim(_PathSplitter(i))
                                Case 6 ' Document Name
                                    _FileName = Mid(Trim(_PathSplitter(i)), 1, InStr(Trim(_PathSplitter(i)), ".") - 1)
                                    'Case 6 ' Document Extension
                                    _Extension = Mid(Trim(_PathSplitter(i)), InStr(Trim(_PathSplitter(i)), ".") + 1)
                            End Select
                        Next
                        'Pick Record From Database for varification
                        If Archived = True Then
                            _SQLQuery = "SELECT * FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 1 AND DocumentType = 2 "
                        Else
                            _SQLQuery = "SELECT * FROM DMS_MST WHERE SystemFolder = '" & _SystemFolder & "' AND Container = '" & _Container & "' AND Category = '" & _Category & "' AND PatientID = " & _PatientID & " AND [Year] = '" & _Year & "' AND [Month] = '" & _Month & "' AND DocumentFileName  = '" & _FileName & "' AND Extension = '" & _Extension & "' AND DocumentID <> 0 AND ArchiveStatus = 0 AND DocumentType = 2 "
                        End If

                        If _IsPublisher = True Then
                            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Publisher))
                        Else
                            oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Subscriber))
                        End If
                        oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                        If oDataReader.HasRows = True Then
                            While oDataReader.Read
                                With _CategorisedDocument
                                    .FileName = Trim(oDataReader.Item("DocumentFileName") & "")
                                    .Name = Trim(oDataReader.Item("DocumentName") & "")
                                    .Extension = Trim(oDataReader.Item("Extension") & "")
                                    .SourceMachine = Trim(oDataReader.Item("SourceMachine") & "")
                                    .SystemFolder = Trim(oDataReader.Item("SystemFolder") & "")
                                    .Container = Trim(oDataReader.Item("Container") & "")
                                    .Category = Trim(oDataReader.Item("Category") & "")
                                    .PatientID = CLng(oDataReader.Item("PatientID") & "")
                                    .Year = Trim(oDataReader.Item("Year") & "")
                                    .Month = Trim(oDataReader.Item("Month") & "")
                                    .Format = Val(oDataReader.Item("DocumentFormat") & "")
                                    .SourceBin = Val(oDataReader.Item("SourceBin") & "")
                                    .Pages = Val(oDataReader.Item("Pages") & "")
                                    .Archived = oDataReader.Item("ArchiveStatus")
                                    .ArchiveDescription = Trim(oDataReader.Item("ArchiveDescription") & "")
                                    .InUsed = oDataReader.Item("UsedStatus")
                                    .UsedMachine = Trim(oDataReader.Item("UsedMachine") & "")
                                    .UsedType = Val(oDataReader.Item("UsedType") & "")
                                    .Type = Val(oDataReader.Item("DocumentType") & "")
                                    .MachineID = CLng(oDataReader.Item("MachineID") & "")
                                End With
                            End While
                        End If
                        oDB.Disconnect()
                        Return _CategorisedDocument
                    End If
                End If
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB.Disconnect()
                _CategorisedDocument = Nothing
                _PathFolder = Nothing
                _Path = Nothing
                _DMSPath = Nothing
                _PathSplitter = Nothing
                _FileName = Nothing : _Name = Nothing : _Extension = Nothing : _Month = Nothing : _Year = Nothing : _Category = Nothing : _Container = Nothing : _SystemFolder = Nothing
                _PatientID = Nothing
                oDB = Nothing
                oDataReader = Nothing
                _SQLQuery = Nothing
            End Try
        End Function

        'Its use to find document with same already exists or not in given criteria, 
        'as per new logic for DocumentFileName its not necessary, bcz its already cover in NewDocumentFileNameOrID function
        'and document name will not be duplicate if document is archived, so this field not compare in sql query
        Public Function FindDocument(ByVal DocumentName As String, ByVal PatientID As Long, ByVal Category As String, ByVal DocumentType As gloStream.Supporting.enumDocumentType) As Boolean
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlDataReader                        ' Data Reader
            Dim _SQLQuery As String = ""
            FindDocument = True

            Try
                'Pick Record From Database for varification
                _SQLQuery = "SELECT DocumentName FROM DMS_MST WHERE DocumentName = '" & DocumentName & "' AND PatientID = " & PatientID & " AND Category = '" & Category & "' AND DocumentType = " & DocumentType & ""
                If _IsPublisher = True Then
                    oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Publisher))
                Else
                    oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Subscriber))
                End If
                oDataReader = oDB.ReadQueryRecords(_SQLQuery)
                If oDataReader.HasRows = False Then
                    Return False
                Else
                    Return True
                End If
                oDB.Disconnect()
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB.Disconnect()
                oDB = Nothing
                oDataReader = Nothing
                _SQLQuery = Nothing
            End Try
        End Function


        Public Function SetVersionAndModifyDateTime(ByVal DocumentID As Long, ByVal VersionNo As Int16, ByVal ModifiedDateTime As DateTime) As Boolean
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oDataReader As SqlDataReader                        ' Data Reader
            Dim _SQLQuery As String
            Dim _Result As Boolean = False
            Try
                _SQLQuery = "UPDATE DMS_MST SET VersionNo = " & VersionNo & ", ModifyDateTime = '" & ModifiedDateTime & "' WHERE DocumentID = " & DocumentID & " "
                If _IsPublisher = True Then
                    oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Publisher))
                Else
                    oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(Supporting.ConnectionType.Subscriber))
                End If
                _Result = oDB.ExecuteQueryNonQuery(_SQLQuery)
                oDB.Disconnect()

                Return _Result
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB.Disconnect()
                oDB = Nothing
                oDataReader = Nothing
                _SQLQuery = Nothing
            End Try
        End Function


        Public Sub New(ByVal IsPublisher As Boolean)
            MyBase.new()
            _IsPublisher = IsPublisher
        End Sub

        Protected Overrides Sub Finalize()
            _IsPublisher = Nothing
            MyBase.Finalize()
        End Sub

    End Class

    'Category Functions
    Public Class DocumentCategory
        Private _ErrorMessage As String
        Private _IsPublisher As Boolean

        Public Property ErrorMessage() As String
            Get
                Return _ErrorMessage
            End Get
            Set(ByVal Value As String)
                _ErrorMessage = Value
            End Set
        End Property

        'Add Category
        Public Function Add(ByVal Name As String) As Boolean
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            Dim oPathFolder As New gloStream.Supporting.PathsAndFolders
            Dim _Result As Boolean = False
            Dim _RecordAdd As Boolean = False

            Dim _Category As String = ""
            Dim _CategoryPath As String = ""
            Dim _CategoryRecycledPath As String = ""

            Dim _ContainerPath As String = ""
            Dim _RecycledPath As String = ""

            Dim i As Integer = 0

            Try
                If Not Trim(Name) = "" Then
                    '1. Save category database
                    If _IsPublisher = True Then
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
                    Else
                        oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Subscriber))
                    End If
                    'Category Name
                    oDB.DBParameters.Add("@CategoryName", Name, ParameterDirection.Input, SqlDbType.VarChar)
                    'Modify Name
                    oDB.DBParameters.Add("@ModifyName", "", ParameterDirection.Input, SqlDbType.VarChar)
                    'Excute Query
                    _RecordAdd = oDB.ExecuteNonQuery("gsp_DMS_SaveCategory")
                    oDB.Disconnect()

                    '2. Make Category in Explorer
                    If _RecordAdd = True Then
                        'Paths
                        _Category = Name
                        If _IsPublisher = True Then
                            _ContainerPath = oPathFolder.GetContainerPath(DMSPublisherRootPath)
                            _RecycledPath = oPathFolder.GetRecyclePath(DMSPublisherRootPath)
                        Else
                            _ContainerPath = oPathFolder.GetContainerPath(DMSSubscriberRootPath)
                            _RecycledPath = oPathFolder.GetRecyclePath(DMSSubscriberRootPath)
                        End If

                        'Category Folder Path
                        _CategoryPath = _ContainerPath & "\" & _Category
                        While Directory.Exists(_CategoryPath) = True
                            i = i + 1
                            _CategoryPath = _ContainerPath & "\" & _Category & i
                        End While

                        'Recycle Folder Path
                        i = 0
                        _CategoryRecycledPath = _RecycledPath & "\" & _Category
                        While Directory.Exists(_CategoryRecycledPath) = True
                            i = i + 1
                            _CategoryRecycledPath = _RecycledPath & "\" & _Category & i
                        End While

                        'If necesary then rename folders
                        If _CategoryPath <> _ContainerPath & "\" & _Category Then
                            Rename(_ContainerPath & "\" & _Category, _CategoryPath)
                        End If
                        If _CategoryRecycledPath <> _RecycledPath & "\" & _Category Then
                            Rename(_RecycledPath & "\" & _Category, _CategoryRecycledPath)
                        End If

                        'Create New Category Folders
                        MkDir(_ContainerPath & "\" & _Category)
                        MkDir(_RecycledPath & "\" & _Category)

                        _Result = True
                    End If

                End If
                Return _Result
            Catch oError As Exception
                _ErrorMessage = oError.Message
                Exit Function
            Finally
                oDB = Nothing
                oPathFolder = Nothing
                _Result = Nothing
                _RecordAdd = Nothing
                _Category = Nothing
                _CategoryPath = Nothing
                _CategoryRecycledPath = Nothing
                _ContainerPath = Nothing
                _RecycledPath = Nothing
                i = Nothing
            End Try

        End Function

        Public Sub New(ByVal IsPublisher As Boolean)
            MyBase.new()
            _IsPublisher = IsPublisher
        End Sub

        Protected Overrides Sub Finalize()
            _IsPublisher = Nothing
            MyBase.Finalize()
        End Sub
    End Class

    'Categorised Document
    Public Class CategorisedDocument
        Private _FileName As String
        Private _Name As String
        Private _Extension As String
        Private _SourceMachine As String
        Private _SystemFolder As String
        Private _Container As String
        Private _Category As String
        Private _PatientID As Long
        Private _Year As String
        Private _Month As String
        Private _Format As Supporting.enumDocumentFormat
        Private _SourceBin As Supporting.enumDocumentSourceBin
        'Private _Blocked As Boolean
        Private _Pages As Long
        Private _Archived As Boolean
        Private _ArchiveDescription As String
        Private _InUsed As Boolean
        Private _UsedMachine As String
        Private _UsedType As Supporting.enumDocumentUsedType
        Private _Type As Supporting.enumDocumentType
        Private _Path As String ' to retrive document, not for store
        'Private _SynchronizationParameters As gloStream.gloDMS.Document.SynchronizationParameters
        Private _MachineID As Int16
        Private _IsPublisher As Boolean

        Public Property FileName() As String
            Get
                Return _FileName
            End Get
            Set(ByVal Value As String)
                _FileName = Value
            End Set
        End Property

        'Document Name - Maximum Length - 60
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

        'Document Extension - Maximum Length - 3
        Public Property Extension() As String
            Get
                Return _Extension
            End Get
            Set(ByVal Value As String)
                _Extension = Value
            End Set
        End Property

        'Document Added in DMS Source Machine - Maximum Length - 255
        Public Property SourceMachine() As String
            Get
                Return _SourceMachine
            End Get
            Set(ByVal Value As String)
                _SourceMachine = Value
            End Set
        End Property

        'Document Added in DMS, this Source Machine - Maximum Length - 255
        Public Property SystemFolder() As String
            Get
                Return _SystemFolder
            End Get
            Set(ByVal Value As String)
                _SystemFolder = Value
            End Set
        End Property

        'Document Added in DMS, this Source Machine - Maximum Length - 255
        Public Property Container() As String
            Get
                Return _Container
            End Get
            Set(ByVal Value As String)
                _Container = Value
            End Set
        End Property

        'Document Category, From Category Master - Maximum Length - 50
        Public Property Category() As String
            Get
                Return _Category
            End Get
            Set(ByVal Value As String)
                _Category = Value
            End Set
        End Property

        'Patient ID, from Patient Master - Long
        Public Property PatientID() As Long
            Get
                Return _PatientID
            End Get
            Set(ByVal Value As Long)
                _PatientID = Value
            End Set
        End Property

        'Year of Document, Maximum Length - 4
        Public Property Year() As String
            Get
                Return _Year
            End Get
            Set(ByVal Value As String)
                _Year = Value
            End Set
        End Property

        'Month of Document, Maximum Length - 20
        Public Property Month() As String
            Get
                Return _Month
            End Get
            Set(ByVal Value As String)
                _Month = Value
            End Set
        End Property

        'Document Format - means in PDF format, TIF format, etc
        Public Property Format() As Supporting.enumDocumentFormat
            Get
                Return _Format
            End Get
            Set(ByVal Value As Supporting.enumDocumentFormat)
                _Format = Value
            End Set
        End Property

        'From where document is placed in DMS, eg - general bin, scanner, OMRICROCR, etc
        Public Property SourceBin() As Supporting.enumDocumentSourceBin
            Get
                Return _SourceBin
            End Get
            Set(ByVal Value As Supporting.enumDocumentSourceBin)
                _SourceBin = Value
            End Set
        End Property

        ''is blocked, means not deleted but remove from list
        'Public Property Blocked() As Boolean
        '    Get
        '        Return _Blocked
        '    End Get
        '    Set(ByVal Value As Boolean)
        '        _Blocked = Value
        '    End Set
        'End Property

        'No of pages contain in document
        Public Property Pages() As Long
            Get
                Return _Pages
            End Get
            Set(ByVal Value As Long)
                _Pages = Value
            End Set
        End Property

        'is this document is archived, means shift from current location to another, or its not in use from longer period
        Public Property Archived() As Boolean
            Get
                Return _Archived
            End Get
            Set(ByVal Value As Boolean)
                _Archived = Value
            End Set
        End Property

        'If archive then archive path for imergency access
        Public Property ArchiveDescription() As String
            Get
                Return _ArchiveDescription
            End Get
            Set(ByVal Value As String)
                _ArchiveDescription = Value
            End Set
        End Property

        'Is this file in used by any user, on any machine
        Public Property InUsed() As Boolean
            Get
                Return _InUsed
            End Get
            Set(ByVal Value As Boolean)
                _InUsed = Value
            End Set
        End Property

        'If used then machine name
        Public Property UsedMachine() As String
            Get
                Return _UsedMachine
            End Get
            Set(ByVal Value As String)
                _UsedMachine = Value
            End Set
        End Property

        'If used then its for only view purpose or modification prupose
        Public Property UsedType() As Supporting.enumDocumentUsedType
            Get
                Return _UsedType
            End Get
            Set(ByVal Value As Supporting.enumDocumentUsedType)
                _UsedType = Value
            End Set
        End Property

        'Document Type, means its Categorized, Uncategorized, or scan doucment, etc
        Public Property Type() As Supporting.enumDocumentType
            Get
                Return _Type
            End Get
            Set(ByVal Value As Supporting.enumDocumentType)
                _Type = Value
            End Set
        End Property

        'Document Path
        Public ReadOnly Property Path() As String
            Get
                Dim _TempPath As String
                Dim oSupport As New gloStream.Supporting.PathsAndFolders
                If _IsPublisher = True Then
                    _TempPath = oSupport.GetDMSPath(DMSPublisherRootPath) & _SystemFolder & "\" & _Container & "\" & _Category & "\" & _PatientID & "\" & _Year & "\" & _Month & "\" & _FileName & "." & _Extension
                Else
                    _TempPath = oSupport.GetDMSPath(DMSSubscriberRootPath) & _SystemFolder & "\" & _Container & "\" & _Category & "\" & _PatientID & "\" & _Year & "\" & _Month & "\" & _FileName & "." & _Extension
                End If
                oSupport = Nothing
                Return _TempPath
            End Get
        End Property

        'Machine ID
        Public Property MachineID() As Integer
            Get
                Return _MachineID
            End Get
            Set(ByVal Value As Integer)
                _MachineID = Value
            End Set
        End Property

        ''Synchronization Parameter
        'Public Property SynchronizationParameters() As gloStream.gloDMS.Document.SynchronizationParameters
        '    Get
        '        Return _SynchronizationParameters
        '    End Get
        '    Set(ByVal Value As gloStream.gloDMS.Document.SynchronizationParameters)
        '        _SynchronizationParameters = Value
        '    End Set
        'End Property

        Public Sub New(ByVal IsPublisher As Boolean)
            MyBase.new()
            _IsPublisher = IsPublisher
            '_SynchronizationParameters = New gloStream.gloDMS.Document.SynchronizationParameters
        End Sub

        Protected Overrides Sub Finalize()
            ' _SynchronizationParameters = Nothing
            _ispublisher = Nothing
            MyBase.Finalize()
        End Sub
    End Class

    Namespace Supporting

#Region "Enum Declaration"


        'File Types Enum
        Public Enum enumDocumentFormat
            PDF = 0
            TIF = 1
        End Enum

        'Document Extension
        Public Enum enumDocumentExtension
            PDF = 0
            TIF = 1
        End Enum


        'Document Type
        Public Enum enumDocumentType
            None = 0
            UnCategorisedDocument = 1
            CategorisedDocument = 2
            UnCategorisedDeletedDocument = 3
            CategorisedDeletedDocument = 4
        End Enum

        'Document Source Type
        Public Enum enumDocumentSourceBin
            None = 0
            GeneralBin = 1
            Category = 2
            Scanner = 3
            ExternalPDF = 4
            Migration = 5
            OMRICROCR = 6
            Fax = 7
        End Enum

        'Document Used Type
        Public Enum enumDocumentUsedType
            None = 0
            ScanDocument = 1
            ViewDocument = 2
        End Enum

        'Document Criteria
        Public Enum enumDocumentCriteria
            None = 0
            NewDocument = 1
            MergeDocument = 2
            DeleteDocument = 3
            Print = 4
            Fax = 5
            PrintAll = 6
            FaxAll = 7
            PrintFax = 8
        End Enum

        'Document Modifiy Activity
        Public Enum ModifyActivity
            None = 0

            'Import and Scan Doucment
            ReceivedToGBFromImport = 1
            ReceivedToCatFromImport = 2
            ReceivedToGBFromScan = 3
            ReceivedToCatFromScan = 4

            'Send Document From General Bin
            SendToNewDocumentInCatFormGB = 5
            SendToExtDocumentInCatFormGB = 6
            SendToRecycleFromGB = 7

            'Received Document From General Bin
            ReceivedToCatFromGBInNewDocument = 8
            ReceivedToCatFromGBInExtDocument = 9
            ReceivedToRecycleFromGB = 10

            'Send Document From Category
            SendToNewDocumentToCatFromCat = 11
            SendToExtDocumentToCatFromCat = 12
            SendToRecycleFromCat = 13

            'Received Document From Category
            ReceivedToCatFromCatInNewDocument = 14
            ReceivedToCatFromCatInExtDocument = 15
            ReceivedToRecycleFromCat = 16

            'Synchronization Document
            NewFromPublisherToSubscriber = 17
            NewFromSubscriberToPublisher = 18
            MergeFromPublisherToSubscriber = 19
            MergeFromSubscriberToPublisher = 20
        End Enum

        'Send/Received Flag
        Public Enum SendRecivedFlag
            None = 0
            SendTo = 1
            ReceivedFrom = 2
        End Enum

        'Document Month
        Public Enum enumMonth
            None = 0
            January = 1
            February = 2
            March = 3
            April = 4
            May = 5
            June = 6
            July = 7
            August = 8
            September = 9
            October = 10
            November = 11
            December = 12
            All = 13
        End Enum

#End Region

#Region "Enum Declaration for Publisher & Subscribers"
        Public Enum enmConnectionAuthentication
            WindowsAuthentication
            SQLAuthentication
        End Enum

        Public Enum ConnectionType
            None = 0
            Publisher = 1
            Subscriber = 2
        End Enum

        Public Enum SynchrnoizeDataType
            None = 0
            NotInPublisher = 1
            NotInSubscriber = 2
            ModifiedNotInPublisher = 3
            ModifiedNotInSubscriber = 4
        End Enum

        Public Enum SendType
            None = 0
            PublisherToSubscriber = 1
            SubscriberToPublisher = 2
        End Enum

        Public Enum IfExistsThen
            None = 0
            Replace = 1
            Merge = 2
        End Enum

        Public Enum SynchronizationMode
            None = 0
            Winner = 1
            Merge = 2
        End Enum

        Public Enum SynchronizeWinner
            None = 0
            Publisher = 1
            Subscriber = 2
        End Enum

        Public Enum TroubleShootingType
            None = 0
            Publisher = 1
            Subscriber = 2
            NotInPublisher = 3
            NotInSubscriber = 4
            ModNotInPublisher = 5
            ModNotInSubscriber = 6
            ConflictPublisherToSubscriber = 7
            ConflictSubscriberToPublisher = 8
        End Enum
#End Region


        Public Class Supporting

            Private _ErrorMessage As String
            Public Property ErrorMessage() As String
                Get
                    Return _ErrorMessage
                End Get
                Set(ByVal Value As String)
                    _ErrorMessage = Value
                End Set
            End Property

            Public Function MonthAsString(ByVal MonthNo As enumMonth) As String
                MonthAsString = MonthName(MonthNo)
            End Function

            'Extension As String
            Public Function ExtensionAsString(ByVal Extension As enumDocumentExtension) As String
                Dim _Extension As String = ""
                Select Case Extension
                    Case enumDocumentExtension.PDF
                        _Extension = "PDF"
                    Case enumDocumentExtension.TIF
                        _Extension = "TIF"
                End Select
                Return _Extension
            End Function

            'Check Valid DMS Path
            Public Function IsDMSSystem(ByVal Path As String) As Boolean
                Dim oFolderPaths As New gloStream.Supporting.PathsAndFolders
                Dim _RootPath As String
                Dim _SystemFolder As String
                Dim _ContainerFolder As String
                Dim _RecycleFolder As String
                Dim _ScanFolder As String
                Try
                    IsDMSSystem = False
                    'DMS Root Path
                    _RootPath = oFolderPaths.GetDMSPath(Path)
                    If Directory.Exists(_RootPath) = False Then Exit Function
                    'Check For System Folder
                    _SystemFolder = _RootPath & oFolderPaths.SystemFolder
                    If Directory.Exists(_SystemFolder) = False Then Exit Function
                    'Check For Container Folder
                    _ContainerFolder = _SystemFolder & "\" & oFolderPaths.ContainerFolder
                    If Directory.Exists(_ContainerFolder) = False Then Exit Function
                    'Check For Recycle Folder
                    _RecycleFolder = _SystemFolder & "\" & oFolderPaths.RecycleFolder
                    If Directory.Exists(_RecycleFolder) = False Then Exit Function
                    'Check For Scan Folder
                    _ScanFolder = _SystemFolder & "\" & oFolderPaths.ScanFolder
                    If Directory.Exists(_ScanFolder) = False Then Exit Function
                    'Set Tru
                    IsDMSSystem = True
                Catch oError As Exception
                    Exit Function
                Finally
                    oFolderPaths = Nothing
                    _RootPath = Nothing
                    _SystemFolder = Nothing
                    _ContainerFolder = Nothing
                    _RecycleFolder = Nothing
                    _ScanFolder = Nothing
                End Try
            End Function

            'Check Item exists in collection
            Public Function IsItemExists(ByVal ItemValue As String, ByVal ItemList As Collection) As Boolean
                IsItemExists = False
                Dim i As Integer
                For i = 1 To ItemList.Count
                    If Trim(ItemValue) = ItemList(i) Then
                        Return True
                        Exit Function
                    End If
                Next
            End Function

            'Find Difference Categories
            Public Function Categories(ByVal SynchDataType As gloStream.Supporting.SynchrnoizeDataType) As Collection
                Try
                    Dim dtPublisher As New DataTable
                    Dim dtSubscriber As New DataTable
                    Dim objDB As New gloStream.gloDataBase.gloDataBase
                    Dim oCollection As New Collection

                    'Publisher
                    objDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Publisher))
                    dtPublisher = objDB.ReadQueryData("SELECT * FROM DMS_Category_MST Where IsDeleted = 0 AND CategoryName IS NOT NULL ORDER BY CategoryName")
                    objDB.Disconnect()

                    'Subscriber
                    objDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(gloStream.Supporting.ConnectionType.Subscriber))
                    dtSubscriber = objDB.ReadQueryData("SELECT * FROM DMS_Category_MST Where IsDeleted = 0 AND CategoryName IS NOT NULL ORDER BY CategoryName")
                    objDB.Disconnect()


                    Dim dtPublisherNew As New DataTable
                    Dim dtSubscriberNew As New DataTable
                    dtPublisherNew = objDB.FindDifferenceTable(dtSubscriber, dtPublisher, "CategoryName", False)
                    dtSubscriberNew = objDB.FindDifferenceTable(dtPublisher, dtSubscriber, "CategoryName", False)

                    If SynchDataType = SynchrnoizeDataType.NotInPublisher Then
                        For i As Int16 = 0 To dtPublisherNew.Rows.Count - 1
                            oCollection.Add(dtPublisherNew.Rows(i)(1))
                        Next
                    ElseIf SynchDataType = SynchrnoizeDataType.NotInSubscriber Then
                        For i As Int16 = 0 To dtSubscriberNew.Rows.Count - 1
                            oCollection.Add(dtSubscriberNew.Rows(i)(1))
                        Next
                    End If
                    Return oCollection
                Catch oError As Exception
                    _ErrorMessage = oError.Message
                End Try
            End Function

            Public ReadOnly Property NewDocumentName(ByVal PatientID As Long, ByVal Category As String, ByVal DocumentType As gloStream.Supporting.enumDocumentType, ByVal PublisherSubscriber As gloStream.Supporting.ConnectionType) As String
                Get
                    Threading.Thread.Sleep(1000)
                    Dim _NewDocumentName As String = ""
                    Dim _Date As String = Format(Date.Now, "MM/dd/yyyy")
                    Dim _Time As String = Format(Date.Now, "hh:mm:ss tt")
                    Dim i As Integer = 0
                    Dim oDocument As gloStream.Document
                    Select Case PublisherSubscriber
                        Case ConnectionType.Publisher
                            oDocument = New gloStream.Document(True)
                        Case ConnectionType.Subscriber
                            oDocument = New gloStream.Document(False)
                    End Select

                    _NewDocumentName = Replace(_Date, "/", " ") & " - " & Replace(_Time, ":", " ")
                    While oDocument.FindDocument(_NewDocumentName, PatientID, Category, DocumentType) = True
                        i = i + 1
                        _NewDocumentName = Replace(_Date, "/", " ") & " - " & Replace(_Time, ":", " ") & "-" & i
                    End While

                    Return _NewDocumentName
                End Get
            End Property

            Public ReadOnly Property NewDocumentNameFromExisting(ByVal ExistingDocumentName As String, ByVal PatientID As Long, ByVal Category As String, ByVal DocumentType As gloStream.Supporting.enumDocumentType, ByVal PublisherSubscriber As gloStream.Supporting.ConnectionType) As String
                Get
                    Dim _NewDocumentName As String = ""
                    Dim i As Integer = 0
                    Dim oDocument As gloStream.Document
                    Select Case PublisherSubscriber
                        Case ConnectionType.Publisher
                            oDocument = New gloStream.Document(True)
                        Case ConnectionType.Subscriber
                            oDocument = New gloStream.Document(False)
                    End Select

                    _NewDocumentName = ExistingDocumentName
                    While oDocument.FindDocument(_NewDocumentName, PatientID, Category, DocumentType) = True
                        i = i + 1
                        _NewDocumentName = ExistingDocumentName & "-" & i
                    End While

                    Return _NewDocumentName
                End Get
            End Property

            Public ReadOnly Property NewDocumentFileNameOrID(ByVal PatientID As Long, ByVal GetDocID As Boolean, ByVal PublisherSubscriber As gloStream.Supporting.ConnectionType) As String
                Get
                    Dim _ReturnID As Long = 0
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(gloEMRAdmin.mdlgloDMS.GetConnectionString_DMS(PublisherSubscriber))
                    oDB.DBParameters.Add("@MachineID", mdlgloDMS.GetPrefixTransactionID(PatientID, PublisherSubscriber), ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@GetID", GetDocID, ParameterDirection.Input, SqlDbType.BigInt)
                    oDB.DBParameters.Add("@ReturnID", _ReturnID, ParameterDirection.Output, SqlDbType.BigInt)
                    _ReturnID = oDB.ExecuteNonQueryForOutput("gsp_DMS_GetNewDocumentIDAndFileName")
                    oDB.Disconnect()
                    Return _ReturnID
                End Get
            End Property


            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class

        Public Class PathsAndFolders
            '----FOLDERS----
            Public Const SystemFolder As String = "DMS System"
            Public Const ContainerFolder As String = "Container"
            Public Const RecycleFolder As String = "Recycle Bin"
            Public Const ScanFolder As String = "Scan"
            Public Const GeneralFolder As String = "General"
            Public Const TempExtractFolder As String = "Temp"
            Public Const NoteDocumentFile As String = "Note.Doc"

            Private _TempProcessFolder As String

            Public ReadOnly Property TempProcessFolder() As String
                Get
                    _TempProcessFolder = Application.StartupPath & "\DMSTempProcess"
                    If Directory.Exists(_TempProcessFolder) = False Then
                        MkDir(_TempProcessFolder)
                    End If
                    Return _TempProcessFolder
                End Get
            End Property
            '----PATHS----
            'Public DMSRootPath As String

            Public Function GetDMSPath(ByVal RootPath As String) As String
                Try
                    GetDMSPath = ""
                    Dim _TempPath As String = RootPath
                    If Mid(RootPath, Len(RootPath) - 1) = ":\" Then
                        _TempPath = RootPath
                    ElseIf Mid(RootPath, Len(RootPath)) = ":" Then
                        _TempPath = RootPath & "\"
                    Else
                        If Mid(RootPath, Len(RootPath)) = "\" Then
                            _TempPath = RootPath
                        Else
                            _TempPath = RootPath & "\"
                        End If
                    End If
                    Return _TempPath
                Catch objError As Exception
                    Exit Function
                End Try
            End Function

            Public Function GetContainerPath(ByVal DMSSystemRootPath As String) As String
                Try
                    GetContainerPath = ""
                    Dim _TempPath As String = DMSSystemRootPath
                    If Mid(DMSSystemRootPath, Len(DMSSystemRootPath) - 1) = ":\" Then
                        _TempPath = DMSSystemRootPath
                    ElseIf Mid(DMSSystemRootPath, Len(DMSSystemRootPath)) = ":" Then
                        _TempPath = DMSSystemRootPath & "\"
                    Else
                        If Mid(DMSSystemRootPath, Len(DMSSystemRootPath)) = "\" Then
                            _TempPath = DMSSystemRootPath
                        Else
                            _TempPath = DMSSystemRootPath & "\"
                        End If
                    End If

                    If Directory.Exists(_TempPath) = True Then
                        Return _TempPath & SystemFolder & "\" & ContainerFolder
                    End If

                Catch objError As Exception
                    Exit Function
                End Try
            End Function

            Public Function GetScanPath(ByVal DMSSystemRootPath As String) As String

            End Function

            Public Function GetRecyclePath(ByVal DMSSystemRootPath As String) As String
                Try
                    GetRecyclePath = ""
                    Dim _TempPath As String = DMSSystemRootPath
                    If Mid(DMSSystemRootPath, Len(DMSSystemRootPath) - 1) = ":\" Then
                        _TempPath = DMSSystemRootPath
                    ElseIf Mid(DMSSystemRootPath, Len(DMSSystemRootPath)) = ":" Then
                        _TempPath = DMSSystemRootPath & "\"
                    Else
                        If Mid(DMSSystemRootPath, Len(DMSSystemRootPath)) = "\" Then
                            _TempPath = DMSSystemRootPath
                        Else
                            _TempPath = DMSSystemRootPath & "\"
                        End If
                    End If

                    If Directory.Exists(_TempPath) = True Then
                        Return _TempPath & SystemFolder & "\" & RecycleFolder
                    End If

                Catch objError As Exception
                    Exit Function
                End Try
            End Function

            Public Function GetRecycleScanPath(ByVal DMSSystemRootPath As String) As String

            End Function

            Public Function GetRecycleGeneralPath(ByVal DMSSystemRootPath As String) As String

            End Function

            Public Sub New()
                MyBase.new()
            End Sub

            Protected Overrides Sub Finalize()
                MyBase.Finalize()
            End Sub
        End Class
    End Namespace
End Namespace